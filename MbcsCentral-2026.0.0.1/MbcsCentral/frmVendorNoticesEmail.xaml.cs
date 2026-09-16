using ExtensionMethods;
using MbcsCentral.WinGL;
using MbcsCentral.WinPM;
using MbcsMail;
using ObjectDumper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmNoticesEmail.xaml
    /// </summary>
    public partial class frmVendorNoticesEmail : Window
    {
        const string MAIN_SPLIT = "|";
        const string SECONDARY_SPLIT = "^";
        string ClassName;
        Classes cls;
        Classes[] documentClasses;
        VendorNoticeEmailParms parms = null;
        VendorNoticeEmailParms[] documentParms = null;
        int clientId = (int)GlobalShared.user.Client.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        bool cancel = true;
        const string MESSAGE_PREFIX = "DocumentMessages:";
        PassedVariables PassedVariables = new PassedVariables();
        VendorEmail vendorEmail = new VendorEmail();
        ArrayList vendorEmails = new ArrayList();
        string VendorACHMessage = "";
        NACHA_Layout[] nachas = null;
        string mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.ACHNotices];
        Email.SmtpConfig smtp;
        string fromEmail = "";


        int emailsSentCount = 0;
        int emailsNotSentCount = 0;
        double emailCount = 0;
        Window results;
        Grid grid;
        TextBlock txtSubjectR;
        ListBox lstSent;
        ListBox lstNotSent;
        TextBox txtNumberSent;
        TextBox txtNumberNotSent;
        ArrayList emailsSent = new ArrayList();
        ArrayList emailsNotSent = new ArrayList();

        public bool rentNotices { get; set; } = false;
        public bool VendorACH { get; set; } = true;
        public bool PayrollVendor { get; set; } = false;

        DataTable PayStub = null;

        string thisApp;

        public frmVendorNoticesEmail(string app)
        {
            thisApp = app;
            if (!GlobalShared.user.hasAccessPermission(thisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            InitializeComponent();
            if (rentNotices)
                this.Title = "Rent Notices Email List";
        }

        public frmVendorNoticesEmail()
        {
            InitializeComponent();
        }

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            string address = lstReplyTo.Text.Trim();
            if (!chkReplyTo.IsChecked.Value)
                address = "";
            if (!address.Equals("") && !TestEmail.IsEmail(address))
            {
                MessageBoxCustom.Show("Invalid Reply-To Email Address.");
                return;
            }
            address = "<" + address + ">";

            // Bail out early so the popup never opens for an empty selection.
            if (vendorEmails == null || vendorEmails.Count == 0)
            {
                MessageBox.Show("No vendors have been selected.");
                return;
            }

            // Build the results window and its child lists BEFORE binding, then bind
            // the freshly-created ArrayLists. SendEmails (queued below) uses the same
            // field references, so per-vendor results appear live in the popup.
            emailsSent = new ArrayList();
            emailsNotSent = new ArrayList();
            emailsSentCount = 0;
            emailsNotSentCount = 0;
            SetupResultsWindow();

            lstSent.ItemsSource = emailsSent;
            lstNotSent.ItemsSource = emailsNotSent;
            results.Title = "Vendor ACH Notifications - Sending...";
            results.Focus();

            this.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    SendEmails(address);
                    txtNumberSent.Text = emailsSentCount.ToString().PadLeft(8);
                    txtNumberNotSent.Text = emailsNotSentCount.ToString().PadLeft(8);
                    results.Title = "Vendor ACH Notifications - Results";

                    // Summary popup after the send loop finishes.
                    string summary =
                        "Vendor ACH notifications complete." + Environment.NewLine + Environment.NewLine +
                        "Successfully sent:  " + emailsSentCount + Environment.NewLine +
                        "Failed to send:     " + emailsNotSentCount;
                    MessageBoxImage icon = emailsNotSentCount > 0
                        ? MessageBoxImage.Warning
                        : MessageBoxImage.Information;
                    MessageBox.Show(results, summary, "Email Send Summary", MessageBoxButton.OK, icon);
                }),
                System.Windows.Threading.DispatcherPriority.Background);

            results.ShowDialog();
            results.InvalidateVisual();
            results.UpdateLayout();
            foreach (string s in lstReplyTo.Items)
            {
                if (s.Equals(address))
                    return;
            }
            //lstReplyTo.Items.Add(address);
            if (lstReplyTo.SelectedIndex == -1)
            {
                if (parms.ReturnAddresses == null)
                    parms.ReturnAddresses = new List<string>();
                parms.ReturnAddresses.Add(address);
                //    parms.ReturnAddresses = new string[0];
                //string[] addresses = parms.ReturnAddresses;
                //Array.Resize(ref addresses, parms.ReturnAddresses.Length + 1);
                //addresses[addresses.Length - 1] = address;
                //parms.ReturnAddresses = addresses;

            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            tenants = null;
            this.Close();
        }

        private void formNoticesEmail_Loaded(object sender, RoutedEventArgs e)
        {
            if (VendorACH)
            {
                Main.getForm(this);
                this.Title = "Vendor ACH Notices";
                pnlSavedMessages.Visibility = Visibility.Collapsed;

                string message;
                if (!Email.Utils.GetEmailConfig(mailTag, out message, out smtp, out fromEmail, true))
                {
                    MessageBox.Show("Unable to retrieve Email account data <" + mailTag + ">: " + message);
                    return;
                }
                if (fromEmail.Trim().Equals(""))
                {
                    MessageBox.Show("A FROM address has not been selected.");
                }

                if (parms == null)
                    parms = new VendorNoticeEmailParms();
                ClassName = this.GetType().Name;
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = VendorNoticeEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                string[] vendorTypes = { "Regular Vendors", "Payroll", "CANCEL"};
                MessageBoxResult result = MessageBoxCustom.Show("Please choose the VENDOR TYPE below:", "Send ACH Notifications", MessageBoxButton.YesNoCancel, this, vendorTypes);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        PayrollVendor = false;
                        Title += " ** NON-PAYROLL **";
                        break;
                    case MessageBoxResult.No:
                        PayrollVendor = true;
                        Title += " *** PAYROLL ***";
                        break;
                    case MessageBoxResult.Cancel:
                        this.Close();
                        break;
                }
                EmailParms.DataContext = parms;
                MbcsMail.Smtp.Config smtpConfig = new MbcsMail.Smtp.Config() { description = smtp.description, emailAddress = smtp.emailAddress, userName = smtp.userName, password = smtp.password, server = smtp.server, port = smtp.port, SSL = smtp.SSL, TSL = smtp.TSL, authentication = smtp.authentication };
                Title += "   <" + smtp.emailAddress + ">";
                ShowAvailableFiles();
                lstVendors.ItemsSource = vendorEmails;
            }
            else
            {

                if (tenants == null)
                {
                    MessageBox.Show("No Tenant names were passed.");
                    this.Close();
                }
                Main.getForm(this);
                if (rentNotices)
                    this.Title = "Rent Notices Email List";

                lstTenants.ItemsSource = tenants;

                if (LateNotices)
                {
                    ClassName = this.GetType().Name;
                    cls = Classes.loadByClient(clientId, ClassName);
                    if (cls != null)
                        parms = VendorNoticeEmailParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                }
                else
                if (parms == null)
                    parms = new VendorNoticeEmailParms();
                if (!LateNotices)
                {
                    ClassName = MESSAGE_PREFIX;
                    documentClasses = Classes.loadMultiple(ClassName, (int)acctId);
                    lstMessages.Items.Clear();
                    lstMessages.Items.Add("");
                    if (documentClasses != null && documentClasses.Length > 0 && pnlSavedMessages.IsEnabled)
                    {
                        if (documentParms == null)
                            documentParms = new VendorNoticeEmailParms[documentClasses.Length];
                        else
                            Array.Resize<VendorNoticeEmailParms>(ref documentParms, documentClasses.Length);
                        //for (int i = 0; i < documentClasses.Length; i++)
                        for (int i = 0; i < documentClasses.Length; i++)
                        {
                            documentParms[i] = VendorNoticeEmailParms.DeSerialize(documentClasses[i].value);
                            try
                            {
                                lstMessages.Items.Add(documentClasses[i]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    parms.DisplayName = this.displayname;
                    this.Title = "Tenant Document Email List";
                    double emailCount = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, 0);
                    double emailDelay = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, 0);
                    txtLimit.Text = emailCount.ToString();
                    txtDelay.Text = emailDelay.ToString();
                }
                EmailParms.DataContext = parms;
                lstInsertionFields.ItemsSource = WinpmGlobal.NoticeInsertionFieldNames;
            }
        }

        private void SetupResultsWindow()
        {
                    results = new Window();
                    results.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    results.Title = "Vendor ACH Notifications - Results";
                    results.Width = 750;
                    results.Height = 450;
                    results.Background = System.Windows.Media.Brushes.Bisque;
                    TextBlock.SetFontWeight(results, FontWeights.Bold);
                    TextBlock.SetFontSize(results, 14);

                    System.Windows.Controls.Border border = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Blue, BorderThickness = new Thickness(3), CornerRadius = new CornerRadius(5) };

                    grid = new Grid();
                    TextBlock.SetFontSize(grid, 12);
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    border.Child = grid;

                    System.Windows.Controls.Border bdr1 = new System.Windows.Controls.Border();
                    bdr1.BorderBrush = System.Windows.Media.Brushes.Blue;
                    bdr1.BorderThickness = new Thickness(2);
                    Grid.SetRow(bdr1, 0);
                    grid.Children.Add(bdr1);

                    StackPanel pnlTop = new StackPanel();
                    pnlTop.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    pnlTop.Background = System.Windows.Media.Brushes.AliceBlue;

                    bdr1.Child = pnlTop;

                    TextBlock txt1 = new TextBlock();
                    txt1.HorizontalAlignment = HorizontalAlignment.Left;
                    txt1.VerticalAlignment = VerticalAlignment.Center;
                    txt1.TextAlignment = TextAlignment.Center;
                    txt1.FontWeight = FontWeights.Bold;
                    txt1.Background = System.Windows.Media.Brushes.AliceBlue;
                    txt1.Margin = new Thickness(10, 0, 0, 0);
                    txt1.Text = "EMAILS SENT: ";
                    pnlTop.Children.Add(txt1);

                    txtSubjectR = new TextBlock();
                    txtSubjectR.Margin = new Thickness(5, 0, 0, 0);
                    txtSubjectR.VerticalAlignment = VerticalAlignment.Center;
                    txtSubjectR.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtSubjectR.FontWeight = FontWeights.Bold;
                    pnlTop.Children.Add(txtSubjectR);

                    txtNumberSent = new TextBox();
                    txtNumberSent.Width = 48;
                    txtNumberSent.Margin = new Thickness(15, 0, 0, 0);
                    txtNumberSent.VerticalAlignment = VerticalAlignment.Center;
                    txtNumberSent.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtNumberSent.BorderBrush = System.Windows.Media.Brushes.Black;
                    txtNumberSent.BorderThickness = new Thickness(2);
                    pnlTop.Children.Add(txtNumberSent);

                    lstSent = new ListBox();
                    lstSent.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lstSent.VerticalAlignment = VerticalAlignment.Stretch;
                    lstSent.Background = System.Windows.Media.Brushes.LightBlue;
                    lstSent.ItemsSource = emailsSent;
                    Grid.SetRow(lstSent, 1);
                    grid.Children.Add(lstSent);

                    System.Windows.Controls.Border bdr2 = new System.Windows.Controls.Border();
                    bdr2.BorderBrush = System.Windows.Media.Brushes.Blue;
                    bdr2.BorderThickness = new Thickness(2);
                    Grid.SetRow(bdr2, 2);
                    grid.Children.Add(bdr2);

                    StackPanel pnlBottom = new StackPanel();
                    pnlBottom.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    pnlBottom.Background = System.Windows.Media.Brushes.MediumOrchid;

                    bdr2.Child = pnlBottom;

                    TextBlock txt2 = new TextBlock();
                    txt2.HorizontalAlignment = HorizontalAlignment.Stretch;
                    txt2.VerticalAlignment = VerticalAlignment.Stretch;
                    txt2.TextAlignment = TextAlignment.Center;
                    txt2.FontWeight = FontWeights.Bold;
                    txt2.Text = "EMAILS NOT SENT";
                    pnlBottom.Children.Add(txt2);

                    txtNumberNotSent = new TextBox();
                    txtNumberNotSent.Width = 48;
                    txtNumberNotSent.Margin = new Thickness(15, 0, 0, 0);
                    txtNumberNotSent.VerticalAlignment = VerticalAlignment.Center;
                    txtNumberNotSent.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtNumberNotSent.BorderBrush = System.Windows.Media.Brushes.Black;
                    txtNumberNotSent.BorderThickness = new Thickness(2);
                    pnlBottom.Children.Add(txtNumberNotSent);

                    lstNotSent = new ListBox();
                    lstNotSent.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lstNotSent.VerticalAlignment = VerticalAlignment.Stretch;
                    lstNotSent.ItemsSource = emailsNotSent;
                    Grid.SetRow(lstNotSent, 3);
                    grid.Children.Add(lstNotSent);

                    StackPanel pnlCommand = new StackPanel();
                    pnlCommand.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    Grid.SetRow(pnlCommand, 4);
                    grid.Children.Add(pnlCommand);

                    Button cmdPrint = new Button() { Content = "PRINT REPORT", Height = 30 };
                    cmdPrint.Click += cmdPrint_Click;
                    pnlCommand.Children.Add(cmdPrint);

                    Button cmdClose = new Button() { Content = "CLOSE", Height = 30, Margin = new Thickness(10, 0, 0, 0) };
                    cmdClose.Click += cmdClose_Click;
                    pnlCommand.Children.Add(cmdClose);

                    results.Content = border;

        }


        void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            results.Close();
        }

        void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string[] s;

            if (emailsSent.Count > 0 || emailsNotSent.Count > 0)
            {
                Report rpt = new Report("Vendor Notice Emails", "Courier New", 12) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
                rpt.LeftMargin = 10;
                rpt.TopMargin = 10;
                rpt.PrintPageLine("ACH Notification EMAIL REPORT: ");
                rpt.PrintPageLine(DateTime.Now.ToShortDateString());
                rpt.PrintPageLine(""); rpt.PrintPageLine("");
                rpt.PrintPageLine("Name                                 Email Address");
                rpt.PrintPageLine("");
                rpt.PrintPageLine("SENT DOCUMENTS - Count: " + emailsSent.Count.ToString());
                rpt.PrintPageLine("============");
                if (emailsSent.Count > 0)
                {
                    for (int i = 0; i < emailsSent.Count; i++)
                    {
                        {
                            s = ((string)emailsSent[i]).Split('|');
                            if (s.Length >= 2)
                            {
                                rpt.PrintPageLine(s[0].Left(31).PadRight(32), false);
                                rpt.PrintPageLine(s[1]);
                                //rpt.PrintPageLine(s[2].PadRight(32));
                            }
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.PrintPageLine(""); rpt.PrintPageLine("");

                rpt.PrintPageLine("DOCUMENTS NOT SENT - Count: " + emailsNotSent.Count.ToString());
                rpt.PrintPageLine("============");
                if (emailsNotSent.Count > 0)
                {
                    for (int i = 0; i < emailsNotSent.Count; i++)
                    {
                        {
                            s = ((string)emailsNotSent[i]).Split('|');
                            if (s.Length >= 2)
                            {
                                rpt.PrintPageLine(s[0].Left(31).PadRight(32), false);
                                rpt.PrintPageLine(s[1], false);
                            }
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.DisplayDocument();
            }
        }


        private void ShowAvailableFiles()
        {
            string NachaFilePath = "";
            string NachaFilePrefix = "";

            nachas = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
            if (nachas == null || nachas.Length == 0)
            {
                MessageBoxCustom.Show("Unable to retrieve ACH Control records.  Cannot continue.");
                return;
            }
            foreach (NACHA_Layout rec in nachas)
            {
                switch (rec.Position)
                {
                    case (int)NACHA_Layout.Control_Types.VendorFileDirectory:
                        NachaFilePath = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorFilePrefix:
                        NachaFilePrefix = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorCheckingCredit:
                        PassedVariables.NachaCheckingCredit = rec.Contents.Trim();
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorEmailMessage:
                        VendorACHMessage = rec.Contents;
                        break;
                }
            }
            if (NachaFilePrefix == null || NachaFilePrefix.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Unable to retrieve ACH File Prefix. Cannot continue.");
                return;
            }

            if (NachaFilePath == null || NachaFilePath.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Unable to retrieve ACH File Path. Cannot continue.");
                return;
            }
            PassedVariables.PayrollVendor = PayrollVendor;
            frmVendorEmail frm = new frmVendorEmail() { PassedVariables = PassedVariables, NachaFilePath = NachaFilePath, NachaFilePrefix = NachaFilePrefix, vendorEmail = vendorEmails };
            try
            {
                frm.CreateWindow();
                frm.Topmost = true;
                frm.Owner = this;

                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                frm.ShowDialog();
                this.lstVendors.ItemsSource = frm.vendorEmail;
                txtMessage.Text = VendorACHMessage;
                txtFromName.Text = fromEmail;
                lstInsertionFields.ItemsSource = GlobalShared.NoticeInsertionFieldNames;
            }
            catch(Exception ex) { } 
        }

        private void SendEmails(string replyTo = "")
        {
            decimal InvoicesTotal = 0;
            if (vendorEmails == null || vendorEmails.Count == 0)
            {
                MessageBox.Show("No vendors have been selected.");
                return;
            }
            // SetupResultsWindow + emailsSent/emailsNotSent initialization now happen in
            // cmdSend_Click *before* the popup is shown, so the bound lists update live as
            // each vendor is processed. Re-creating them here would stomp on those bindings.
            emailCount++;

            DataTable dt = PassedVariables.payrollRecords;
            DataRow record = null;
            if (PayrollVendor) dt.Select();
            bool found = false;
            ArrayList list = new ArrayList();
            // vendorEmails holds one entry per invoice (one per NACHA addenda record). Since each
            // email combines all of a vendor's invoices, we must send only ONE email per
            // vendor + company, otherwise a vendor with 3 invoices receives 3 identical emails.
            System.Collections.Generic.HashSet<string> emailedVendors = new System.Collections.Generic.HashSet<string>();
            int totalCount = vendorEmails.Count;
            int processedCount = 0;
            foreach (VendorEmail email in vendorEmails)
            {
                processedCount++;
                if (txtSubjectR != null)
                    txtSubjectR.Text = "Processing " + processedCount + " of " + totalCount +
                                       (email != null && email.VendorName != null
                                            ? "  -  " + email.VendorName
                                            : "");
                // Yield once so the progress label paints before the SMTP call blocks the UI.
                this.Dispatcher.Invoke(new Action(() => { }),
                    System.Windows.Threading.DispatcherPriority.Background);
                if (!email.Use)
                    continue;

                // Skip if this vendor + company was already emailed (combined into one notice).
                string vendorKey = email.VendorNumber + "|" + (email.CompanyNumber == null ? "" : email.CompanyNumber.Trim());
                if (emailedVendors.Contains(vendorKey))
                    continue;

                // Locate the matching record. The payroll .dat carries only the vendor number
                // (no Company or Invoice columns), so payroll matches on vendor number alone.
                // Regular records match on vendor + company + invoice.
                found = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (PayrollVendor)
                    {
                        if ((int)row["VendorNumber"] == email.VendorNumber)
                        { record = row; found = true; break; }
                    }
                    else
                    {
                        if ((int)row["VendorNumber"] == email.VendorNumber
                            && row["Company"].ToString().Trim() == email.CompanyNumber
                            && row["Invoice"].ToString().Trim() == email.InvoiceNumber.Trim())
                        { record = row; found = true; break; }
                    }
                }
                if (!found)
                    continue;

                emailedVendors.Add(vendorKey);
                int vendorNumber = email.VendorNumber;
                string message = txtMessage.Text;
                string subject = txtSubject.Text;

                message = VendorACHMessage + "\r\n\nPlease apply this amount as follows -\r\n";
                if (PayrollVendor)
                {
                    string[] stubs = ((string)record["stub"]).Split('|');
                    if (stubs.Length > 0)
                    {
                        foreach (string s in stubs)
                        {
                            message += "\r\n" + s;
                        }
                    }
                    else
                        continue;
                  
                }
                else
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "VendorNumber = " + email.VendorNumber + " AND Company = " + email.CompanyNumber ;
                    dv.Sort = "VendorNumber ASC, Company ASC, Invoice ASC";
                    foreach (DataRowView row in dv)
                    {
                        if ((int)row["VendorNumber"] == email.VendorNumber && row["Company"].ToString().Trim() == email.CompanyNumber) ; // && row["Invoice"].ToString().Trim() == email.InvoiceNumber.Trim())
                        {
                            decimal amount = 0;
                            decimal.TryParse(row["Amount"].ToString(), out amount);
                            message += "\r\nInvoice # " + (row["Invoice"].ToString().Trim() + " - ").PadRight(15) + amount.ToString("$###,###,##0.00") ;
                            InvoicesTotal += amount;
                        }
                    }
                    email.Amount = InvoicesTotal.ToString("C");
                    InvoicesTotal = 0;
                }
                for (int i = 0; i < GlobalShared.NoticeInsertionFieldNames.Length; i++)
                {
                    InsertEmailField(ref subject, GlobalShared.NoticeInsertionFieldNames[i], email);
                    InsertEmailField(ref message, GlobalShared.NoticeInsertionFieldNames[i], email);
                }
                list = MbcsCentral.Email.Utils.SendEmail(smtp, fromEmail, displayname, email.Email, subject, message, "", null, false, chkReceipt.IsChecked.Value, replyTo);

                if (list == null || list.Count == 0)
                {
                    emailsSentCount++;
                    emailsSent.Add(email.VendorName + " | " + email.Email + "|");
                    lstSent.Items.Refresh();
                    lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                    if (txtNumberSent != null)
                        txtNumberSent.Text = emailsSentCount.ToString().PadLeft(8);
                }
                else
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Email for <" + email.Email + "> not sent:\r\n", false);
                    foreach (string s in list)
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, s, false);
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "", false);
                    emailsNotSentCount++;
                    emailsNotSent.Add(email.VendorName + " | " + email.Email + " | " + email.Amount + "|" + list[0].ToString());
                    lstNotSent.Items.Refresh();
                    lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);
                    if (txtNumberNotSent != null)
                        txtNumberNotSent.Text = emailsNotSentCount.ToString().PadLeft(8);
                }

                // Yield so the list refresh and counter update actually paint before the next SMTP call.
                this.Dispatcher.Invoke(new Action(() => { }),
                    System.Windows.Threading.DispatcherPriority.Background);
            }

            if (txtSubjectR != null)
                txtSubjectR.Text = "Done - " + processedCount + " processed";
        }

        void InsertEmailField(ref string content, string field, VendorEmail email)
        {


            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.PayDate].ToLower()))
                content = content.Replace(field, email.Date);
            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.Invoice].ToLower()))
                content = content.Replace(field, email.InvoiceNumber);
            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.Amount].ToLower()))
                content = content.Replace(field, email.Amount);
            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.Account].ToLower()))
                content = content.Replace(field, email.Account);
            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.Company].ToLower()))
                content = content.Replace(field, email.Company);
            if (field.ToLower().Equals(GlobalShared.NoticeInsertionFieldNames[(int)GlobalShared.NoticeInsertionFields.VendorName].ToLower()))
                content = content.Replace(field, email.VendorName);

        }
        public class frmVendorEmail : System.Windows.Window
        {
            public PassedVariables PassedVariables { get;set; }
            public string NachaFilePath { get; set; }
            public string NachaFilePrefix { get; set; }
            public ArrayList vendorEmail { get; set; }    

            public frmVendorEmail() { }

            public void CreateWindow()
            {
                Grid grd = new Grid();
            
                this.Name = "frmVendorEmails";
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                 if (PassedVariables.PayrollVendor)
                    this.Title = "Choose ACH results to NOTIFY ***PAYROLL*** >";
                 else
                    this.Title = "Choose ACH results to NOTIFY";
                this.Height = 400;
                this.Width = 500;

            
                grd.RowDefinitions.Add(new RowDefinition());
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

                string[] files = Directory.GetFiles(NachaFilePath, NachaFilePrefix + "*.txt");
                if (files == null || files.Length == 0)
                {
                    MessageBoxCustom.Show("No ACH result files found.  There are no payments to post.");
                    return;
                }
                ListBox lst = new ListBox();
                lst.Tag = this;
                List<string> ls = files.ToList();
                lst.ItemsSource = ls;
                PassedVariables.lst = lst;
                this.Tag = PassedVariables;

                StackPanel pnl = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
                {
                    Button cmdNotify = new Button() { Content = "NOTIFY", Margin = new Thickness(10, 0, 0, 0) };
                    cmdNotify.Tag = this;
                    cmdNotify.Click += NachaNotify;
                    pnl.Children.Add(cmdNotify);
                }
                Button cmdDelete = new Button() { Content = "DELETE", Margin = new Thickness(10, 0, 0, 0) };
                cmdDelete.Click += NachaDelete;
                cmdDelete.Tag = lst;
                Button cmdExit = new Button() { Content = "EXIT", Margin = new Thickness(10, 0, 0, 0) };
                cmdExit.Click += NachaExit;
                cmdExit.Tag = this;

                pnl.Children.Add(cmdDelete);
                pnl.Children.Add(cmdExit);

                Grid.SetRow(pnl, 2);
                grd.Children.Add(pnl);

                Border bdr = new Border();
                bdr.BorderBrush = Brushes.Blue;
                bdr.BorderThickness = new Thickness(3, 3, 3, 3);
                bdr.CornerRadius = new CornerRadius(2);
                bdr.Child = lst;

                Grid.SetRow(bdr, 0);
                grd.Children.Add(bdr);

                this.Content = grd;
            }

        }
    public static void NachaNotify(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            frmVendorEmail frm = (frmVendorEmail)button.Tag;
            if (frm == null)
                return;
            PassedVariables p = frm.PassedVariables; // (PassedVariables)frm.Tag;
            ListBox lst = p.lst;
            if (lst == null)
                return;
            if (lst.SelectedItem == null)
            {
                MessageBoxCustom.Show("No ACH result file selected.","",MessageBoxButton.OK,frm);
                return;
            }
            if (MessageBoxCustom.Show("Notify of ACH Payments from file: " + lst.SelectedItem + "?",  "NOTIFY OF ACH PAYMENTS", MessageBoxButton.YesNo, frm) == MessageBoxResult.Yes)
            {

                ProcessList(p, frm.vendorEmail);
            }
            frm.Close();
        }

        public static void ProcessList(PassedVariables p, ArrayList vendorList)
        {
            int vendorNumberPosition = 0;
            int vendorNumberLength = 0;
            int invoicePosition = 0;
            int invoiceLength = 0;
            int amountPosition = 0;
            int amountLength = 0;
            int effectiveDatePosition = 0;
            int effectiveDateLength = 0;
            int companyNamePosition = 0;
            int companyNameLength = 0;
            int companyDiscretionaryDataPosition = 0;
            int companyDiscretionaryDataLength = 0;
            int vendorNumber = 0;
            Company company = null;
            string checkDate = "";
            Vendors vendor = null;
            bool vendorFound = false;
            VendorEmail vendorEmail = null;

            NACHA_Layout[] batchHeader = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchVendorHeader);
            foreach (NACHA_Layout rec in batchHeader)
            {
                switch (rec.Name.Trim())
                {
                    case NachaBatchVendorHeader.COL_EFFECTIVE_DATE:
                        effectiveDatePosition = rec.Position;
                        effectiveDateLength = rec.Length;
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_NAME:
                        companyNamePosition = rec.Position;
                        companyNameLength = rec.Length;
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_DISCRETIONARY_DATA:
                        companyDiscretionaryDataPosition = rec.Position;
                        companyDiscretionaryDataLength += rec.Length;
                        break;
                }
            }


            NACHA_Layout[] detailRecord = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.DetailRecord);
            foreach (NACHA_Layout rec in detailRecord)
            {
                switch (rec.Name.Trim())
                {
                    case NachaDetail.COL_INDIVIDUAL_IDENTIFICATION_NUMBER:
                        vendorNumberPosition = rec.Position;
                        vendorNumberLength = rec.Length;
                        break;
                    case NachaDetail.COL_AMOUNT:
                        amountPosition = rec.Position;
                        amountLength = rec.Length;
                        break;
                }
            }
            ListBox lst = p.lst;
            string filepath = (string)lst.SelectedItem;
            if (p.PayrollVendor)
            {
                //p.paystub = new DataTable("PayStub");
                TextReader pReader;
                try
                {
                    pReader = new StreamReader(filepath.Replace(".txt", ".dat"));
                }
                catch (IOException ex)
                {
                    MessageBoxCustom.Show("Unable to open Payroll Data File. Cannot continue.");
                    return;
                }
                string lastVendor = "";
                string s4 = "";
                
                p.payrollRecords = new DataTable("Payroll");
                DataColumn dc = new DataColumn("VendorNumber", System.Type.GetType("System.Int32"));
                p.payrollRecords.Columns.Add(dc);
                dc = new DataColumn("Company", System.Type.GetType("System.Int32"));
                p.payrollRecords.Columns.Add(dc);
                dc = new DataColumn("Stub", System.Type.GetType("System.String"));
                p.payrollRecords.Columns.Add(dc);
                DataRow dr;

                ArrayList stub = null;
                string lr = pReader.ReadLine();
                while (lr != null)
                {
                    string[] s = lr.Split('|');
                    if (s.Length > 1)
                    {
                        if (!lastVendor.Trim().Equals(s[0]))
                        {
                            if (lastVendor.Trim().Equals(""))
                            {
                                stub = new ArrayList();

                                lastVendor = s[0];
                            }
                            {
                                dr = p.payrollRecords.NewRow();
                                dr["VendorNumber"] = lastVendor;
                                s4 = "";
                                foreach (string s2 in stub)
                                {
                                    s4 += s2 + "|";
                                }
                                if (s4.EndsWith("|"))
                                    s4 = s4.Left(s4.LastIndexOf('|'));
                                dr["Stub"] = s4;
                                p.payrollRecords.Rows.Add(dr);
                            }
                            //p.payrollRecord = new PayrollRecord();
                            //p.payrollRecord.VendorNumber = int.Parse(s[0]);
                            stub = new ArrayList();
                            lastVendor = s[0];

                        }
                        stub.Add(s[1]);
                    }
                    lr = pReader.ReadLine();
                }
                //if (stub != null && stub.Count > 0)
                //{
                    dr = p.payrollRecords.NewRow();
                    dr["VendorNumber"] = lastVendor;
                    s4 = "";
                    foreach (string s2 in stub)
                    {
                        s4 += s2 + "|";
                    }
                    if (s4.EndsWith("|"))
                        s4 = s4.Left(s4.LastIndexOf('|'));
                    dr["Stub"] = s4;
                    p.payrollRecords.Rows.Add(dr);

                //}
                pReader.Close();
            }
            else
            {
                TextReader pReader;
                try
                {
                    pReader = new StreamReader(filepath.Replace(".txt", ".dat"));
                }
                catch (IOException ex)
                {
                    MessageBoxCustom.Show("Unable to open Vendor Data File. Cannot continue.");
                    return;
                }
                string lastVendor = "";
                string s4 = "Please apply this amount as follows - ";

                p.payrollRecords = new DataTable("Payroll");
                DataColumn dc = new DataColumn("VendorNumber", System.Type.GetType("System.Int32"));
                p.payrollRecords.Columns.Add(dc);
                dc = new DataColumn("Company", System.Type.GetType("System.Int32"));
                p.payrollRecords.Columns.Add(dc);
                dc = new DataColumn("Invoice", System.Type.GetType("System.String"));
                p.payrollRecords.Columns.Add( dc);
                dc = new DataColumn("Amount", System.Type.GetType("System.String"));
                p.payrollRecords.Columns.Add(dc);
                dc = new DataColumn("Stub", System.Type.GetType("System.String"));
                p.payrollRecords.Columns.Add(dc);
                DataRow dr;

                ArrayList stub = null;
                string lr = pReader.ReadLine();
                while (lr != null)
                {
                    string[] s = lr.Split(MAIN_SPLIT.ToCharArray());
                    // Guard: the .dat file may contain trailing lines with fewer pipe-separated
                    // columns (e.g. payroll Net Pay totals, blank lines). Skip those rather than crash.
                    if (s.Length > 2)
                    {
                        string[] s1 = s[2].Split(SECONDARY_SPLIT.ToCharArray());
                        if (s1.Length > 2)
                        {
                            dr = p.payrollRecords.NewRow();
                            dr["VendorNumber"] = s[0];
                            dr["Company"] = s[1];
                            dr["Invoice"] = s1[0];
                            // The amount is the last whitespace-separated field on the line. Take
                            // the last token instead of a fixed Substring(34): the previous fixed
                            // offset assumed a 25-char description and returned $0 whenever the
                            // description (e.g. a long building/company name like "SPRINGFIELD
                            // MANAGEMENT GROUP, INC") pushed the amount past position 34.
                            string[] amtParts = s1[2].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            s4 = amtParts.Length > 0 ? amtParts[amtParts.Length - 1] : "";
                            decimal d = 0;
                            decimal.TryParse(s4, out d);
                            dr["Amount"] = d;
                            s4 = "";
                        /*
                        foreach (string s2 in s1) 
                        {
                            if (s2.Length > 1)
                            {
                                s1 = s2.Split(SECONDARY_SPLIT.ToCharArray());
                                s4 += s2 + MAIN_SPLIT;
                            }
                        }
                        if (s4.EndsWith(MAIN_SPLIT))
                            s4 = s4.Left(s4.LastIndexOf(MAIN_SPLIT));
                        dr["Stub"] = s4;
                        */
                            p.payrollRecords.Rows.Add(dr);
                                //p.payrollRecord = new PayrollRecord();
                                //p.payrollRecord.VendorNumber = int.Parse(s[0]);
                                //stub = new ArrayList();
                                //lastVendor = s[0];

                        }
                    }
                    lr = pReader.ReadLine();
                }
                /*
                if (stub != null && stub.Count > 0)
                {
                dr = p.payrollRecords.NewRow();
                dr["VendorNumber"] = lastVendor;
                s4 = "";
                foreach (string s2 in stub)
                {
                    s4 += s2 + "|";
                }
                if (s4.EndsWith("|"))
                    s4 = s4.Left(s4.LastIndexOf('|'));
                dr["Stub"] = s4;
                p.payrollRecords.Rows.Add(dr);

                //}
                */
                pReader.Close();

            }
            TextReader tr = new StreamReader(filepath);
            string line = tr.ReadLine();
            string date = "";
            string companyName = "";
            string amount = "";
            int companyNumber = 0;
            while (line != null)
            {
                if (line.StartsWith("5"))
                {
                    string s = line.Substring(effectiveDatePosition - 1, effectiveDateLength);
                    if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                        date =  s.Substring(2,2) + "/" + s.Substring(4,2 ) + "/" + s.Substring(0,2);
                    s = line.Substring(companyDiscretionaryDataPosition - 1, companyDiscretionaryDataLength);
                    if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                    {
                        companyNumber = 0;
                        int.TryParse(s.Trim(), out companyNumber);
                        company = Company.loadByCompany(companyNumber);
                        if (company != null)
                        {
                            companyName = company.Name;
                        }
                        else
                        {
                            s = line.Substring(companyNamePosition - 1, companyNameLength);
                            if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                                companyName = s.Trim();
                        }
                    }
                }
                if (line.StartsWith("6" + p.NachaCheckingCredit))
                {
                    vendorNumber = 0;
                    int.TryParse(line.Substring(vendorNumberPosition - 1, vendorNumberLength), out vendorNumber);
                    if (vendorNumber > 0)
                    {
                        vendor = Vendors.load(vendorNumber);
                        if (vendor != null) // && vendor.ACH && vendor.ACHEmail != null && !vendor.ACHEmail.Trim().Equals(""))
                            vendorFound = true;
                    }
                    double d = 0;
                    amount = line.Substring(amountPosition - 1, amountLength).Trim();
                    double.TryParse(amount, out d);
                    amount = (d * 0.01).ToString("#,###,##0.00");

                }
                if (line.StartsWith("7") && vendorFound)
                {
                    //if ((!p.PayrollVendor && !line.Contains("*CHK")) || (p.PayrollVendor && line.Contains("*CHK")))
                    if (line.Contains("*INV") || (p.PayrollVendor && line.Contains("*TRN")))
                    {
                        string invoice = "";
                        int p1;
                        if (p.PayrollVendor )
                             p1 = line.IndexOf("*TRN");
                        else
                            p1 = line.IndexOf("*INV");
                        if (p1 >= 0)
                        {
                            int p2 = line.Substring(p1 + 1).IndexOf("*") ;
                            if (p2 > 0)
                            {
                                invoice = line.Substring(p1 + 4, p2 - p1).Trim();
                            }
                        }
                        vendorEmail = new VendorEmail();
                        vendorEmail.VendorNumber = vendorNumber;
                        vendorEmail.CompanyNumber = companyNumber.ToString() ;
                        vendorEmail.Date = date;
                        vendorEmail.VendorName = vendor.VendorName;
                        vendorEmail.Email = vendor.ACHEmail;
                        vendorEmail.InvoiceNumber = invoice;
                        vendorEmail.Amount = amount;
                        if (vendor.BankAccount.Length > 0)
                        {
                            vendorEmail.Account = "xxx" + vendor.BankAccount.Right(4);
                        }
                        vendorEmail.Company = companyName;
                        vendorEmail.Use = vendor.ACH;
                        vendorList.Add(vendorEmail);
                    }
                    vendorFound = false;
                }
                line = tr.ReadLine();
            }
            tr.Close();

        }

        private static bool ParseAddenda(string line, VendorEmail vendorEmail)
        {
            int i = line.IndexOf("INV");
            if (i == -1)
                return false;
            int j = line.IndexOf(line.Substring(i));
            if (j == -1) 
                return false;  
            string s = line.Substring(i, j - i);
            if (s == null || s.Length == 0 || s.Trim().Equals(""))
                return false;
            vendorEmail.InvoiceNumber = s.Trim();
            i = line.IndexOf("AMT");
            if (i == -1) 
                return false;  
            j = line.IndexOf('\\');
            if (j == -1) 
                return false;
            s = line.Substring(i, j - i);
            if (s == null || s.Length == 0 || s.Trim().Equals(""))
                return false;
            vendorEmail.Amount = s.Trim();

            return false;
        }

        public static void NachaDelete(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;
            if (button == null)
                return;
            ListBox lst = (ListBox)button.Tag;
            if (lst == null)
                return;
            if (lst.SelectedItem == null)
            {
                MessageBoxCustom.Show("No ACH result file selected for Deletion.","", MessageBoxButton.OK, (Window)lst.Tag);
                return;
            }
            string filepath = (string)lst.SelectedItem;
            List<string> files = (List<string>)lst.ItemsSource;
            int i = lst.SelectedIndex;
            if (MessageBoxCustom.Show("Delete ACH result file <" + filepath + ">?", "", MessageBoxButton.YesNo, (Window)lst.Tag) == MessageBoxResult.Yes)
            {
                if (File.Exists(filepath.Replace(".txt", ".dat")))
                {
                    File.Delete(filepath.Replace(".txt", ".dat"));
                }
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);

                    files.RemoveAt(i);
                    lst.ItemsSource = null;
                    lst.ItemsSource = files;
                }
                //frm.InvalidateVisual();
            }
        }

        private static void NachaExit(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            Window frm = (Window)button.Tag;
            if (frm != null)
                frm.Close();
        }

        private void formNoticesEmail_Closed(object sender, EventArgs e)
        {
            try
            {
            foreach (NACHA_Layout rec in nachas)
                if (rec.Position == (int)NACHA_Layout.Control_Types.VendorEmailMessage)
                {
                    rec.Contents = txtMessage.Text;
                    rec.save();
                }
            cls.value = parms.SerializeObject();
            cls.clientId = clientId;
            cls.acctId = acctId;
            cls.name = ClassName;
            cls.save();
            double result = 0;
            double.TryParse(txtLimit.Text, out result);
            Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, result, (int)Registry.DataTypes.Dword);
            this.BatchLimit = result;
            result = 0;
            double.TryParse(txtDelay.Text, out result);
            Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, result, (int)Registry.DataTypes.Dword);
            this.BatchDelay = result;

            }
            catch { }
        }

        public bool LateNotices { get; set; } = true;
        public Tenants[] tenants { get; set; }
        public string EmailSubject { get { return parms.EmailSubject; } }
        public string Message { get { return parms.Message; } }
        public bool ReturnReceipt { get { return parms.ReturnReceipt; } }
        public string DisplayName { get { return parms.DisplayName; }  }
        public bool AlsoPrint { get { return parms.AlsoPrint; }  }
        public bool Test { get; set; }

        // NOT Late Notices
        public string displayname { get; set; }
        public string MessageName { get; set; }
        public bool SendCopy { get; set; }
        public double BatchLimit { get; set; }
        public double BatchDelay { get; set; }

        private void cmdTest_Click(object sender, RoutedEventArgs e)
        {
            this.Test = true;
            cmdSend_Click(sender, e);
        }

        private void CmdSave_Click(object sender, RoutedEventArgs e)
        {
            int i = lstMessages.SelectedIndex;
            if (i > 0)
            {
                documentClasses[i - 1].value = parms.SerializeObject();
                documentClasses[i - 1].save();
            }
            else
            {
                string name = "";
                MessageBoxCustom.Show("New Message Name: ", out name, 32, null, this);
                parms.MessageName = name;
                cls = new Classes();
                cls.clientId = clientId;
                cls.acctId = acctId;
                cls.name = MESSAGE_PREFIX + name;
                cls.value = parms.SerializeObject();
                cls.save();

                if (documentClasses == null)
                    documentClasses = new Classes[1];
                else
                    Array.Resize<Classes>(ref documentClasses, documentClasses.Length + 1);
                documentClasses[documentClasses.Length - 1] = cls;
                if (documentParms == null)
                    documentParms = new VendorNoticeEmailParms[1];
                else
                    Array.Resize<VendorNoticeEmailParms>(ref documentParms, documentParms.Length + 1);
                documentParms[documentParms.Length - 1] = parms;
                lstMessages.Items.Add(parms.MessageName);
            }
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBox lst = (ListBox)sender;
            int i = lstMessages.SelectedIndex;
            if (i < 1)
            {
                MessageBoxCustom.Show("No message has been selected.", "", MessageBoxButton.OK, (Window)lst.Tag);
                return;
            }
            if (MessageBoxCustom.Show("Delete current message?", "", MessageBoxButton.YesNo, (Window)lst.Tag) == MessageBoxResult.No)
                return;


        }

        private void LstMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = lstMessages.SelectedIndex;
            if (i >= 0)
            {
                if (i == 0)
                {
                    parms = new VendorNoticeEmailParms();
                    EmailParms.DataContext = parms;
                }
                else
                {
                    Classes cls = (Classes)lstMessages.SelectedItem;
                    parms = VendorNoticeEmailParms.DeSerialize(cls.value);
                    EmailParms.DataContext = parms;
                }
            }
        }

        private void ChkSendToMe_Click(object sender, RoutedEventArgs e)
        {
            SendCopy = chkSendToMe.IsChecked.Value;
        }
    }


    public class PassedVariables
    {
        public PassedVariables() { }
        public string NachaCheckingCredit{ get; set; }
        public bool PayrollVendor { get; set; } = false;
        public ListBox lst { get; set; }
        public DataTable payStub { get; set; }
        //public PayrollRecord payrollRecord { get; set; }
        public DataTable payrollRecords { get; set; }
    }

    public class VendorEmail
    {
        public int VendorNumber { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public bool Use { get; set; } 
        public string InvoiceNumber { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string Company { get; set; }
        public string CompanyNumber { get; set; }
        public string Account { get; set; }
        public string SEC { get; set; } 
    }

    public class PayrollRecord
    {
        public int VendorNumber { get; set; }
        public ArrayList Stub { get; set; }
    }

    [Serializable]
    public class VendorNoticeEmailParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VendorNoticeEmailParms()
        { }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool alsoPrint = false;
        public string EmailSubject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string DisplayName { get; set; }
        public bool AlsoPrint
                    {
                        get { return alsoPrint; }
                        set { alsoPrint = value; OnPropertyChanged("AlsoPrint"); }
                    }

        public string MessageName { get; set; }
        public bool SendCopy { get; set; }

        //public string[] ReturnAddresses { get; set; }
        public List<string> ReturnAddresses { get; set; }
        public void Serialize(string filename)
        {
            try
            {
                Stream stream = File.Open(filename, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Close();
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving  VendorNoticeEmailParms: " + ex.Message, true);
            }
        }

        public static VendorNoticeEmailParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(VendorNoticeEmailParms));
            StringReader rdr = new StringReader(inputString);
            VendorNoticeEmailParms parms = (VendorNoticeEmailParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

}
