using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

using MbcsUtils;
using System.Runtime.CompilerServices;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmTenantDocuments.xaml
    /// </summary>
    public partial class frmTenantDocuments : Window
    {
        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        LeaseLetterParms parms = null;
        string className = ""; //"LeaseLetterParms";
        Dictionary<string, string> configs = new Dictionary<string, string>();
        DictUtils config;
        Control cont;
        bool OneFlag = false;
        PrinterQueues Printers;
        System.Printing.PrintQueue SelectedPrinter = null;
        System.Printing.PrintCapabilities printerCapabilities;
        bool CanDuplex = false;
        bool doDuplex = false;
        Classes cls;
        string fromEmail = "";
        DocumentLayoutList[] docs = null;
        enum DocumentClasses { BankForms = 1 };


        bool CurrentlyPrinting = false;

        bool email = false;
        string mailTag = "";
        Email.SmtpConfig smtp;

        public int DocumentClass { get; set; } = 0;
        public Tenants[] PassedTenants { get; set; } = null;
        public bool AchNotices { get; set; } = false;
        public bool EmailStmts { get; set; } = false;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmTenantDocuments()
        {
            InitializeComponent();
            Expander.Visibility = System.Windows.Visibility.Collapsed;
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = LeaseLetterParms.DeSerializeObject(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new LeaseLetterParms();

            Main.getForm(this);
            MainGrid.DataContext = parms;
            if (EmailStmts)
            {
                object obj = new object();
                CmdEmail_Click(obj, new RoutedEventArgs());
            }
        }

        public frmTenantDocuments(string app)
        {
            InitializeComponent();
            ThisApp = app;
            Expander.Visibility = System.Windows.Visibility.Collapsed;
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = LeaseLetterParms.DeSerializeObject(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new LeaseLetterParms();

            Main.getForm(this);
            MainGrid.DataContext = parms;
            if (EmailStmts)
            {
                object obj = new object();
                CmdEmail_Click(obj, new RoutedEventArgs());
            }

        }

        public void DoAch()
        {
            if ((AchNotices || EmailStmts) && PassedTenants != null)
            {
                email = true;
                CmdEmail_Click(null, new RoutedEventArgs());
            }
            this.Close();

        } 
        private void frmTenantDocuments_Loaded(object sender, RoutedEventArgs e)
        {
            if (EmailStmts)
            {
                object obj = new object();
                CmdEmail_Click(obj, new RoutedEventArgs());
                return;
            }

            Main.getForm(this);
            this.Width = colMain.Width.Value + colAction.Width.Value + 25;
            cont = Control.load(clientId);
            DateTime dt = System.DateTime.Now;
            dtLetter.SelectedDate = dt;
            dtLetter.DisplayDate = dt;
            dtFrom.SelectedDate = new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddDays(-1);
            dtFrom.DisplayDate = dtFrom.SelectedDate.Value;
            dt = new DateTime(dt.Year, dt.Month, 1).AddMonths(4);
            dtTo.SelectedDate = dt.AddDays(-1);
            dtTo.DisplayDate = dtTo.SelectedDate.Value;
            Types[] TypeDesc = Types.loadAll(clientId, true);
            lstType.ItemsSource = TypeDesc;
            pnlPercents.DataContext = cont;
            pnlMinimumRents.DataContext = cont;
            pnlMinimumYears.DataContext = cont;
            if (doDuplex)
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Red;
            else
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;

            Printers = new PrinterQueues();
            PrinterList.ItemsSource = Printers.Printers;

            /*
            for (int i = 0; i < PrinterList.Items.Count; i++)
            {
                if (PrinterList.Items[i].ToString().Equals(parms.PrinterQueue))
                {
                    PrinterList.SelectedIndex = i;
                    SelectedPrinter = Printers.Queue(i);
                    GetPrinterCapabilities(SelectedPrinter);
                    break;
                }
            }
            */
            docs = DocumentLayoutList.LoadByCategory("2, 3", true);

            string name = "";
            lstDocuments.ItemsSource = docs;
            lstDocuments.Focus();

        }

        private void frmTenantDocuments_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenantNumber.Text.Trim() != "")
                lstTenants.Items.Add(txtTenantNumber.Text.ToUpper());
            txtTenantNumber.Text = "";
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstTenants.SelectedIndex > -1)
                lstTenants.Items.RemoveAt(lstTenants.SelectedIndex);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            lstTenants.Items.Clear();
        }

        private void PrinterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPrinter = Printers.Queue(PrinterList.SelectedIndex);
            GetPrinterCapabilities(SelectedPrinter);
        }

        private void GetPrinterCapabilities(System.Printing.PrintQueue printer)
        {
            printerCapabilities = printer.GetPrintCapabilities();
            if (printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedLongEdge) || printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedShortEdge))
            {
                CanDuplex = true;
                cmdDuplex.IsEnabled = true;
            }
            else
            {
                CanDuplex = false;
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;
                cmdDuplex.IsEnabled = false;
            }
        }

        private void txtTenantNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmdTenants_Click(object sender, RoutedEventArgs e)
        {
            if (Expander.Visibility == System.Windows.Visibility.Collapsed)
            {
                Expander.Visibility = System.Windows.Visibility.Visible;
                cmdTenants.Content = "<<TENANTS";
                this.Width = colMain.Width.Value + colAction.Width.Value + colTenant.Width.Value + 25;
                cmdAdd.IsEnabled = true;
                OneFlag = true;
                txtTenantNumber.Focus();
            }
            else
            {
                Expander.Visibility = System.Windows.Visibility.Collapsed;
                cmdTenants.Content = "TENANTS>>";
                this.Width = colMain.Width.Value + colAction.Width.Value + 25;
                cmdAdd.IsEnabled = false;
                OneFlag = false;
            }

        }
        private void cmdDuplex_Click(object sender, RoutedEventArgs e)
        {
            if (CanDuplex)
            {
                doDuplex = !doDuplex;
                if (doDuplex)
                    cmdDuplex.Foreground = System.Windows.Media.Brushes.Red;
                else
                    cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentlyPrinting)
            {
                StackTrace trace = new StackTrace();

                if (MessageBoxCustom.Show("A batch of letters is printing (called from: " + trace.GetFrame(1).GetMethod().Name + ".\r\n Do you want to start another batch now?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (DocumentClass == (int)DocumentClasses.BankForms)
                PrintBank();
            else
                PrintTenant();


            //CurrentlyPrinting = true;
            //ListItem queue = null;
            //// For Testing
            ////NewTenantDocumentsPrint rpt = new NewTenantDocumentsPrint();
            //BankTenantDocumentsPrint rpt = new BankTenantDocumentsPrint();
            //if (lstDocuments.SelectedItem != null)
            //    rpt.LeaseDocument = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
            //if (!email && PrinterList.SelectedIndex < 0)
            //{
            //    MessageBox.Show("You have not selected a printer.");
            //    return;
            //}
            //if (!email)
            //{
            //    queue = (ListItem)PrinterList.SelectedItem;
            //    rpt.Queue = Printers.Queue(queue.ValueItem);
            //}



            //Status status = new Status();
            //if (!AchNotices)
            //{
            //    txtStatus.DataContext = status;
            //    BindingExpression be = txtStatus.GetBindingExpression(TextBlock.TextProperty);

            //    status.Text = "Preparing to " + (email ? "Email" : "Print");
            //    be.UpdateTarget();
            //    rpt.LetterDate = dtLetter.SelectedDate.Value;
            //    rpt.BldStart = txtStartBuilding.Text;
            //    rpt.BldEnd = txtEndBuilding.Text;
            //    rpt.ExpFrom = dtFrom.SelectedDate.Value;
            //    rpt.ExpTo = dtTo.SelectedDate.Value;
            //    rpt.OneYear = decimal.Parse(txtOneYear.Text);
            //    rpt.TwoYear = decimal.Parse(txtTwoYear.Text);
            //    rpt.IndividualFlag = OneFlag;
            //    rpt.GuidelinesUnknown = chkGuidlines.IsChecked.Value;
            //    rpt.DuplexEnabled = (bool)(CanDuplex & doDuplex);
            //    int copies = int.Parse(txtCopies.Text);
            //    rpt.Copies = (copies == 0 ? 1 : copies);
            //    string filename;
            //    if (lstDocuments.SelectedIndex > 0 || email)
            //    {
            //        if (lstDocuments.SelectedIndex <= 0)
            //            filename = null;
            //        else
            //            filename = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
            //        rpt.DocumentName = filename;
            //    }
            //    else
            //    {
            //        MessageBox.Show("A DOCUMENT must be selected.");
            //        return;
            //    }
            //    //Types t = null;
            //    if (lstType.SelectedItems.Count > 0)
            //    {
            //        string[] t = new string[lstType.SelectedItems.Count];
            //        for (int i = 0; i < lstType.SelectedItems.Count; i++)
            //            t[i] = ((Types)lstType.SelectedItems[i]).Type;
            //        rpt.TypeList = t;
            //    }
            //    if (OneFlag)
            //    {
            //        if (lstTenants.Items.Count == 0)
            //        {
            //            MessageBox.Show("You have chosen to " + (email ? "email" : "print") + " individual tenant Documents but no Tenant Numbers have been entered.");
            //            CurrentlyPrinting = false;
            //            email = false;
            //            return;
            //        }

            //        string[] s = new string[lstTenants.Items.Count];
            //        for (int i = 0; i < lstTenants.Items.Count; i++)
            //            s[i] = (string)lstTenants.Items[i];
            //        rpt.TenantKeys = s;
            //    }
            //    rpt.UseStatementOptions = chkStatementOption.IsChecked.Value;
            //    rpt.UseLeaseDates = chkDates.IsChecked.Value;
            //}
            //if (email)
            //{
            //    frmNoticesEmail frm = new frmNoticesEmail() { EmailAccount = smtp.emailAddress, displayname = smtp.description, TenantACH = AchNotices };

            //    Tenants[] tenants;
            //    if (AchNotices)
            //        tenants = PassedTenants;
            //    else
            //        tenants = rpt.RetrieveTenants();
            //    frm.tenants = tenants;

            //    //frm.displayname = fromEmail;
            //    frm.LateNotices = false;
            //    frm.ShowDialog();
            //    if (frm.tenants == null)
            //    {
            //        CurrentlyPrinting = false;
            //        email = false;
            //        this.Cursor = System.Windows.Input.Cursors.Arrow;
            //        return;
            //    }
            //    else
            //    {

            //        if (frm.SendCopy && tenants != null && tenants.Length > 0)
            //        {
            //            Array.Resize<Tenants>(ref tenants, frm.tenants.Length + 1);
            //            int l = tenants.Length - 1;
            //            tenants[l] = new Tenants()
            //            { BuildingNumber = tenants[l - 1].BuildingNumber, TenantNumber = "0000", TenantName = frm.DisplayName, Apartment = "", Email1 = fromEmail, CanSendEmail = true, SendEmail = true, UseEmail1 = true };
            //            frm.tenants = tenants;
            //        }

            //        rpt.DisplayName = frm.displayname;
            //        rpt.EmailSubject = frm.EmailSubject;
            //        rpt.Message = frm.Message;
            //        rpt.ReturnReceipt = frm.ReturnReceipt;
            //        rpt.OneFlag = OneFlag;
            //        rpt.Test = frm.Test;
            //        rpt.Smtp = smtp;
            //        rpt.ReplyTo = frm.ReturnAddress;
            //        rpt.FromEmail = fromEmail;
            //        rpt.EmailBatchLimit = frm.BatchLimit;
            //        rpt.EmailBatchDelay = frm.BatchDelay;

            //        status.Text = "Emailing Tenant Documents";
            //        rpt.PrintLetters(chkBackground.IsChecked.Value, email, frm.tenants, AchNotices);
            //        this.Cursor = System.Windows.Input.Cursors.Arrow;
            //        CurrentlyPrinting = false;
            //        status.Text = "DONE";
            //    }

            //}
            //else
            //{
            //    status.Text = "Printing Tenant Documents";
            //    rpt.PrintLetters(chkBackground.IsChecked.Value, email, rpt.RetrieveTenants());
            //    status.Text = "DONE";
            //}
            //CurrentlyPrinting = false;
            //email = false;
        }

        private void PrintBank()
        { 
            CurrentlyPrinting = true;
            ListItem queue = null;
            // For Testing
            //NewTenantDocumentsPrint rpt = new NewTenantDocumentsPrint();
            BankTenantDocumentsPrint rpt = new BankTenantDocumentsPrint();
            if (lstDocuments.SelectedItem != null)
                rpt.LeaseDocument = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
            if (!email && PrinterList.SelectedIndex < 0)
            {
                MessageBox.Show("You have not selected a printer.");
                return;
            }
            if (!email)
            {
                queue = (ListItem)PrinterList.SelectedItem;
                rpt.Queue = Printers.Queue(queue.ValueItem);
            }



            Status status = new Status();
            if (!AchNotices)
            {
                txtStatus.DataContext = status;
                BindingExpression be = txtStatus.GetBindingExpression(TextBlock.TextProperty);

                status.Text = "Preparing to " + (email ? "Email" : "Print");
                be.UpdateTarget();
                rpt.LetterDate = dtLetter.SelectedDate.Value;
                rpt.BldStart = txtStartBuilding.Text;
                rpt.BldEnd = txtEndBuilding.Text;
                rpt.ExpFrom = dtFrom.SelectedDate.Value;
                rpt.ExpTo = dtTo.SelectedDate.Value;
                rpt.OneYear = decimal.Parse(txtOneYear.Text);
                rpt.TwoYear = decimal.Parse(txtTwoYear.Text);
                rpt.IndividualFlag = OneFlag;
                rpt.GuidelinesUnknown = chkGuidlines.IsChecked.Value;
                rpt.DuplexEnabled = (bool)(CanDuplex & doDuplex);
                int copies = int.Parse(txtCopies.Text);
                rpt.Copies = (copies == 0 ? 1 : copies);
                string filename;
                if (lstDocuments.SelectedIndex > 0 || email)
                {
                    if (lstDocuments.SelectedIndex <= 0)
                        filename = null;
                    else
                        filename = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
                    rpt.DocumentName = filename;
                }
                else
                {
                    MessageBox.Show("A DOCUMENT must be selected.");
                    return;
                }
                //Types t = null;
                if (lstType.SelectedItems.Count > 0)
                {
                    string[] t = new string[lstType.SelectedItems.Count];
                    for (int i = 0; i < lstType.SelectedItems.Count; i++)
                        t[i] = ((Types)lstType.SelectedItems[i]).Type;
                    rpt.TypeList = t;
                }
                if (OneFlag)
                {
                    if (lstTenants.Items.Count == 0)
                    {
                        MessageBox.Show("You have chosen to " + (email ? "email" : "print") + " individual tenant Documents but no Tenant Numbers have been entered.");
                        CurrentlyPrinting = false;
                        email = false;
                        return;
                    }

                    string[] s = new string[lstTenants.Items.Count];
                    for (int i = 0; i < lstTenants.Items.Count; i++)
                        s[i] = (string)lstTenants.Items[i];
                    rpt.TenantKeys = s;
                }
                rpt.UseStatementOptions = chkStatementOption.IsChecked.Value;
                rpt.UseLeaseDates = chkDates.IsChecked.Value;
            }
            if (email)
            {
                frmNoticesEmail frm = new frmNoticesEmail() { EmailAccount = smtp.emailAddress, displayname = smtp.description, TenantACH = AchNotices };

                Tenants[] tenants;
                if (AchNotices)
                    tenants = PassedTenants;
                else
                    tenants = rpt.RetrieveTenants();
                frm.tenants = tenants;

                //frm.displayname = fromEmail;
                frm.LateNotices = false;
                frm.ShowDialog();
                if (frm.tenants == null)
                {
                    CurrentlyPrinting = false;
                    email = false;
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    return;
                }
                else
                {

                    if (frm.SendCopy && tenants != null && tenants.Length > 0)
                    {
                        Array.Resize<Tenants>(ref tenants, frm.tenants.Length + 1);
                        int l = tenants.Length - 1;
                        tenants[l] = new Tenants()
                        { BuildingNumber = tenants[l - 1].BuildingNumber, TenantNumber = "0000", TenantName = frm.DisplayName, Apartment = "", Email1 = fromEmail, CanSendEmail = true, SendEmail = true, UseEmail1 = true };
                        frm.tenants = tenants;
                    }

                    rpt.DisplayName = frm.displayname;
                    rpt.EmailSubject = frm.EmailSubject;
                    rpt.Message = frm.Message;
                    rpt.ReturnReceipt = frm.ReturnReceipt;
                    rpt.OneFlag = OneFlag;
                    rpt.Test = frm.Test;
                    rpt.Smtp = smtp;
                    rpt.ReplyTo = frm.ReturnAddress;
                    rpt.FromEmail = fromEmail;
                    rpt.EmailBatchLimit = frm.BatchLimit;
                    rpt.EmailBatchDelay = frm.BatchDelay;

                    status.Text = "Emailing Tenant Documents";
                    rpt.PrintLetters(chkBackground.IsChecked.Value, email, frm.tenants, AchNotices);
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    CurrentlyPrinting = false;
                    status.Text = "DONE";
                }

            }
            else
            {
                status.Text = "Printing Tenant Documents";
                rpt.PrintLetters(chkBackground.IsChecked.Value, email, rpt.RetrieveTenants());
                status.Text = "DONE";
            }
            CurrentlyPrinting = false;
            email = false;
        }

        private void PrintTenant()
        {

            CurrentlyPrinting = true;
            ListItem queue = null;
            NewTenantDocumentsPrint rpt = new NewTenantDocumentsPrint();
            if (lstDocuments.SelectedItem != null)
                rpt.LeaseDocument = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
            if (!email && PrinterList.SelectedIndex < 0)
            {
                MessageBox.Show("You have not selected a printer.");
                return;
            }
            if (!email)
            {
                queue = (ListItem)PrinterList.SelectedItem;
                rpt.Queue = Printers.Queue(queue.ValueItem);
            }



            Status status = new Status();
            if (!AchNotices  && !EmailStmts)
            {
                txtStatus.DataContext = status;
                BindingExpression be = txtStatus.GetBindingExpression(TextBlock.TextProperty);

                status.Text = "Preparing to " + (email ? "Email" : "Print");
                be.UpdateTarget();
                try
                {
                    rpt.LetterDate = dtLetter.SelectedDate.Value;
                }
                catch (Exception ex)
                {
                    
                }
                rpt.BldStart = txtStartBuilding.Text;
                rpt.BldEnd = txtEndBuilding.Text;
                rpt.ExpFrom = dtFrom.SelectedDate.Value;
                rpt.ExpTo = dtTo.SelectedDate.Value;
                rpt.OneYear = decimal.Parse(txtOneYear.Text);
                rpt.TwoYear = decimal.Parse(txtTwoYear.Text);
                rpt.IndividualFlag = OneFlag;
                rpt.GuidelinesUnknown = chkGuidlines.IsChecked.Value;
                rpt.DuplexEnabled = (bool)(CanDuplex & doDuplex);
                int copies = int.Parse(txtCopies.Text);
                rpt.Copies = (copies == 0 ? 1 : copies);
                string filename;
                if (lstDocuments.SelectedIndex > 0 || email)
                {
                    if (lstDocuments.SelectedIndex <= 0)
                        filename = null;
                    else
                        filename = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
                    rpt.DocumentName = filename;
                }
                else
                {
                    MessageBox.Show("A DOCUMENT must be selected.");
                    return;
                }
                //Types t = null;
                if (lstType.SelectedItems.Count > 0)
                {
                    string[] t = new string[lstType.SelectedItems.Count];
                    for (int i = 0; i < lstType.SelectedItems.Count; i++)
                        t[i] = ((Types)lstType.SelectedItems[i]).Type;
                    rpt.TypeList = t;
                }
                if (OneFlag)
                {
                    if (lstTenants.Items.Count == 0)
                    {
                        MessageBox.Show("You have chosen to " + (email ? "email" : "print") + " individual tenant Documents but no Tenant Numbers have been entered.");
                        CurrentlyPrinting = false;
                        email = false;
                        return;
                    }

                    string[] s = new string[lstTenants.Items.Count];
                    for (int i = 0; i < lstTenants.Items.Count; i++)
                        s[i] = (string)lstTenants.Items[i];
                    rpt.TenantKeys = s;
                }
                rpt.UseStatementOptions = chkStatementOption.IsChecked.Value;
                rpt.UseLeaseDates = chkDates.IsChecked.Value;
            }
            if (email)
            {
                frmNoticesEmail frm = new frmNoticesEmail() { EmailAccount = smtp.emailAddress, displayname = smtp.description, TenantACH = (AchNotices || EmailStmts ? true : false) };

                Tenants[] tenants;
                if (AchNotices || EmailStmts)
                    tenants = PassedTenants;
                else
                    tenants = rpt.RetrieveTenants();
                frm.tenants = tenants;

                //frm.displayname = fromEmail;
                frm.LateNotices = false;
                frm.ShowDialog();
                if (frm.tenants == null)
                {
                    CurrentlyPrinting = false;
                    email = false;
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    return;
                }
                else
                {

                    if (frm.SendCopy && tenants != null && tenants.Length > 0)
                    {
                        Array.Resize<Tenants>(ref tenants, frm.tenants.Length + 1);
                        int l = tenants.Length - 1;
                        tenants[l] = new Tenants()
                        { BuildingNumber = tenants[l - 1].BuildingNumber, TenantNumber = "0000", TenantName = frm.DisplayName, Apartment = "", Email1 = fromEmail, CanSendEmail = true, SendEmail = true, UseEmail1 = true };
                        frm.tenants = tenants;
                    }

                    rpt.DisplayName = frm.displayname;
                    rpt.EmailSubject = frm.EmailSubject;
                    rpt.Message = frm.Message;
                    rpt.ReturnReceipt = frm.ReturnReceipt;
                    rpt.OneFlag = OneFlag;
                    rpt.Test = frm.Test;
                    rpt.Smtp = smtp;
                    rpt.ReplyTo = frm.ReturnAddress;
                    rpt.FromEmail = fromEmail;
                    rpt.EmailBatchLimit = frm.BatchLimit;
                    rpt.EmailBatchDelay = frm.BatchDelay;

                    status.Text = "Emailing Tenant Documents";
                    rpt.PrintLetters(chkBackground.IsChecked.Value, email, frm.tenants, AchNotices,  EmailStmts );
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    CurrentlyPrinting = false;
                    status.Text = "DONE";
                }

            }
            else
            {
                status.Text = "Printing Tenant Documents";
                rpt.PrintLetters(chkBackground.IsChecked.Value, email, rpt.RetrieveTenants());
                status.Text = "DONE";
            }
            CurrentlyPrinting = false;
            email = false;
        }
        private void FocusObtained(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                txt.SelectAll();
            }
            catch { }
        }

        private void CmdEmail_Click(object sender, RoutedEventArgs e)
        {
            if (!AchNotices && MessageBoxCustom.Show("Send Documents via EMAIL?", "", MessageBoxButton.YesNo, null) == MessageBoxResult.No)
                return;
            email = true;
            if (AchNotices)
                mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.ACHNotices];
            else if (EmailStmts)
                mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.RentStatements];
            else
                mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.TenantDirect];
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
            System.Windows.Input.Cursor cursor = this.Cursor;
            this.Cursor = System.Windows.Input.Cursors.Wait;
            MbcsMail.Smtp.Config smtpConfig = new MbcsMail.Smtp.Config() { description = smtp.description, emailAddress = smtp.emailAddress, userName = smtp.userName, password = smtp.password, server = smtp.server, port = smtp.port, SSL = smtp.SSL, TSL = smtp.TSL, authentication = smtp.authentication };

            cmdPrint_Click(sender, e);
        }

        private void lstDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDocuments.SelectedIndex > 0)
            {
                var filename = ((DocumentLayoutList)lstDocuments.SelectedItem).DocumentName;
                var document = docs.Where(t => t.DocumentName == filename).FirstOrDefault();
                if (document != null)
                {
                    DocumentClass = document.DocumentClass;
                    if (document.DocumentClass == (int)DocumentClasses.BankForms)
                    {
                        chkBackground.Visibility = Visibility.Hidden;
                        lblExpFrom.Visibility = Visibility.Hidden;
                        dtFrom.Visibility = Visibility.Hidden;
                        lblto.Visibility = Visibility.Hidden;
                        dtTo.Visibility = Visibility.Hidden;
                        chkDates.Visibility = Visibility.Hidden;
                        lblPer.Visibility= Visibility.Hidden;
                        txtOneYear.Visibility = Visibility.Hidden;
                        txtTwoYear.Visibility = Visibility.Hidden;
                        lbloneyr.Visibility = Visibility.Hidden;
                        lbltwoyr.Visibility = Visibility.Hidden;
                        lblletter.Visibility = Visibility.Hidden;
                        dtLetter.Visibility = Visibility.Hidden;
                        chkGuidlines.Visibility = Visibility.Hidden;
                        chkMinimum.Visibility = Visibility.Hidden;
                        lbl1year.Visibility = Visibility.Hidden;
                        txtMinimum1.Visibility = Visibility.Hidden;
                        lbl2year.Visibility = Visibility.Hidden;
                        txtMinimum2.Visibility = Visibility.Hidden;
                        txtMaximumYears.Visibility = Visibility.Hidden;
                        lblmxmyear.Visibility = Visibility.Hidden;
                        txtMaximumYears.Visibility= Visibility.Hidden;
                        mxmrent.Visibility = Visibility.Hidden;
                        txtMaximumRent.Visibility = Visibility.Hidden;
                        lblCopies.Visibility = Visibility.Hidden;
                        txtCopies.Visibility = Visibility.Hidden;
                        chkStatementOption.Visibility = Visibility.Hidden;
                        lblEnd.Visibility = Visibility.Hidden;
                        txtEndBuilding.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lblExpFrom.Visibility = Visibility.Visible;
                        dtFrom.Visibility = Visibility.Visible;
                        lblto.Visibility = Visibility.Hidden;
                        dtTo.Visibility = Visibility.Visible;
                        chkDates.Visibility = Visibility.Visible;
                        lblPer.Visibility=Visibility.Visible;
                        txtOneYear.Visibility = Visibility.Visible;
                        txtTwoYear.Visibility = Visibility.Visible;
                        lbloneyr.Visibility= Visibility.Visible;  
                        lbltwoyr.Visibility= Visibility.Visible; 
                        lblletter.Visibility= Visibility.Visible;
                        dtLetter.Visibility= Visibility.Visible;  
                        chkGuidlines.Visibility= Visibility.Visible;  
                        chkMinimum.Visibility= Visibility.Visible;
                        lbl1year.Visibility= Visibility.Visible;   
                        txtMinimum1.Visibility= Visibility.Visible; 
                        lbl2year.Visibility= Visibility.Visible;
                        txtMinimum2.Visibility= Visibility.Visible;
                        txtMaximumYears.Visibility= Visibility.Visible;
                        lblmxmyear.Visibility= Visibility.Visible;
                        txtMaximumYears.Visibility =Visibility.Visible;
                        mxmrent.Visibility= Visibility.Visible;
                        txtMaximumRent.Visibility= Visibility.Visible;
                        lblCopies.Visibility= Visibility.Visible;
                        txtCopies.Visibility  = Visibility.Visible;
                        chkStatementOption.Visibility= Visibility.Visible;
                        lblEnd.Visibility= Visibility.Visible;
                        txtEndBuilding.Visibility= Visibility.Visible;  
                    }
                }
            }
        }
    }
}
