using MbcsMail;
using MbcsCentral.WinTrm;
using NetEmail = Email.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using PdfSharp.Pdf;


namespace MbcsCentral.Email
{
    /// <summary>
    /// Interaction logic for frmEmailAttachment.xaml
    /// </summary>
    public partial class frmEmailAttachment : Window
    {
        Classes cls;
        EmailAttachmentParms parms = null;
        int AccountId;
        public string[] ReportInfo { get; set; }
        public FixedDocument Document { get; set; }
        public PdfSharp.Pdf.PdfDocument Pdf { get; set; }
        public bool IsPdf { get; set; } = false;
        public SmtpConfig MailAccount { get; set; }
        public ArrayList RecipientAddresses { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string[] ReportNames { get; set; }
        public List<FixedDocument> Documents { get; set; }
        public bool BCC { get; set; }
        public bool Batch { get; set; }
        public MbcsCentral.WinGL.frmBatchReports BatchReports { get; set; }

        public WaitWindow frmwait = null;

        private string myApp;

        public frmEmailAttachment()
        {
            InitializeComponent();
        }

        public frmEmailAttachment(string app)
        {
            myApp = app;
            InitializeComponent();
        }

        private void frmEmailAttachment_Loaded(object sender, RoutedEventArgs e)
        {
            myApp = this.Name;
            Main.getForm(this);

            if (ReportInfo != null && ReportInfo.Length > 0)
            {
                txtSubject.Text = ReportInfo[0];
                for (int i = 0; i < ReportInfo.Length; i++)
                    txtMessage.Text += ReportInfo[i] + "\r\n";
            }

            AccountId = (int)GlobalShared.user.Account.accountId;
            cls = Classes.load((int)GlobalShared.user.Account.accountId, this.GetType().Name );
            if (cls == null)
            {
                cls = new Classes();
                cls.clientId = 1;
                cls.acctId = GlobalShared.user.Account.accountId;
                cls.name = this.GetType().Name;
                parms = new EmailAttachmentParms();
            }
            else
            {
                parms = EmailAttachmentParms.DeSerializeObject(cls.value);
            }
            grdTop.DataContext = parms;
            Email.SmtpConfig[] smtps = Email.SmtpConfig.loadByAccount(AccountId);
            lstAccounts.ItemsSource = smtps;

            RecipientMaster[] lists = RecipientMaster.loadByAccount(AccountId);
            lstRecipientList.ItemsSource = lists;

        }

        private void frmEmailAttachment_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (parms != null)
            {
                cls.value = parms.SerializeObject();
                cls.save();    
            }

        }

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            if (MessageDataEntry())
            {
                if (Batch)
                {
                    SetProgress();
                    BatchReports.StartReportProcessing();
                    frmwait.Item = "Done Processing Reports.";
                    frmwait.Item = "";
                }
                MessageSend();
            }
        }

        private void SetProgress()
        {
            frmwait = new WaitWindow();
            frmwait.Owner = this;
            frmwait.Height = this.Height - 70;
            frmwait.CloseButton = true;
            frmwait.Heading = "Sending Documents";
            frmwait.Topmost = true;
            frmwait.Show();
            frmwait.Item = "Processing Reports.";

        }

        public bool MessageDataEntry()
        {
            RecipientAddresses = new ArrayList();
            if (lstAccounts.SelectedIndex == -1)
            {
                MessageBoxCustom.Show("Please select a FROM email account.");
                lstAccounts.Focus();
                return false;
            }
            if (txtSubject.Text.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Email message must have a SUBJECT");
                txtSubject.Focus();
                return false;
            }
            if (lstRecipients.Items.Count == 0)
            {
                MessageBoxCustom.Show("There are no TO email addresses.");
                return false;
            }
            for (int i = 0; i < lstRecipients.Items.Count; i++)
            {
                if (lstRecipients.Items[i].ToString().Trim().Equals(""))
                {
                    MessageBoxCustom.Show("There one or more a blank TO addresses. It should be removed.");
                    return false;
                }
                // Get all TO email addresses from lstRecipients
                foreach (string addr in lstRecipients.Items)
                {
                    // Using a Recipients List?
                    if (addr.StartsWith("["))
                    {
                        int EmailList = 0;
                        int.TryParse(addr.Substring(addr.LastIndexOf(']') + 1), out EmailList);
                        Recipients[] recips = Recipients.loadByList(EmailList);
                        if (recips != null && recips.Length > 0)
                        {
                            foreach (Recipients recip in recips)
                                if (recip.email_address != null && !recip.email_address.Trim().Equals(""))
                                    RecipientAddresses.Add(recip.email_address);
                        }
                    }
                    else

                        RecipientAddresses.Add(addr);
                }
            }
            MailAccount = (SmtpConfig)lstAccounts.SelectedItem;
            ReturnReceipt = chkReceipt.IsChecked.Value;
            Subject = txtSubject.Text;
            Message = txtMessage.Text;
            BCC = chkBCC.IsChecked.Value;
            //if (Batch)
            //    popProgress.IsOpen = true;
            return true;
        }

        public void MessageSend()
        {
            List<string> badFiles = null;
            string filename = "";
            if (!Batch)
            {
                try
                {
                    filename = System.IO.Path.GetTempPath();
                    filename += ReportInfo[0].Replace(" ", "") + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
                }
                catch (IOException ex)
                {
                    string message = ex.Message;
                    if (ex.InnerException != null)
                        message += "\r\n\r\n" + ex.InnerException.Message;
                    MessageBoxCustom.Show("There was an error creating attachment file: " + message);
                    return;
                }
                bool saved = false;
                if (IsPdf)
                {
                    Pdf.Save(filename);
                }
                else
                {
                    if (!Report.DocToPdf(Document, filename))
                    {
                        MessageBoxCustom.Show("Unable to create attachment PDF.");
                        return;
                    }
                }
            }
            else
            {
                if (ReportNames == null || ReportNames.Length == 0)
                {
                    MessageBoxCustom.Show("No Report Files were entered. Email Message{s) will not be sent.");
                    return;
                }
                string report = "";
                string newFile = "";
                badFiles = new List<string>();

                frmwait.Item = "Processing Report Attachments";
                frmwait.Item = new string('=', 20);

                for (int i = 0; i < ReportNames.Length; i++)
                {
                    if (Documents[i] == null)
                        continue;
                    frmwait.Item = ReportNames[i];

                    try
                    {
                        newFile = System.IO.Path.GetTempPath();
                        newFile += (ReportNames[i] + DateTime.Now.ToString("_ddyymmss")).Replace('.', ' ').Replace(',', ' ') + ".pdf";
                        if (i > 0)
                            filename += ",";
                        filename += newFile;
                    }
                    catch (IOException ex)
                    {
                        string message = ex.Message;
                        if (ex.InnerException != null)
                            message += "\r\n" + ex.InnerException.Message;
                        badFiles.Add(newFile +": " + message) ;
                        continue;
                    }
                    if (!Report.DocToPdf(Documents[i], newFile))
                    {
                        badFiles.Add(newFile + ": " + Report.LastError);
                    }
                }

            }

            if (badFiles != null && badFiles.Count > 0)
            {
                string message = "The following attachments were not processed. Send Email anyway?";
                foreach (string file in badFiles)
                    message += "\r\n\r\n" + file;
                if (MessageBoxCustom.Show(message, "", MessageBoxButton.YesNo) == MessageBoxResult.No) 
                    return;
            }

            txtFileName.Text = filename;
            SmtpConfig smtp;
            smtp = MailAccount;
            string fromEmail = smtp.emailAddress;
            MbcsMail.Smtp.Config smtpConfig = new MbcsMail.Smtp.Config() { description = smtp.DisplayName, emailAddress = fromEmail, userName = smtp.userName, password = smtp.password, server = smtp.server, port = smtp.port, SSL = smtp.SSL, TSL = smtp.TSL, authentication = smtp.authentication };
            int EmailList;

            // Format and send message
            string[] s = (string[])RecipientAddresses.ToArray(typeof(string));
            frmContent msg = new frmContent();
            if (Batch)
                msg.ReturnReceipt = ReturnReceipt;
            else
                msg.ReturnReceipt = chkReceipt.IsChecked.Value;
            if (!msg.FormatMessage(smtpConfig, Message, Subject, fromEmail, s, smtp.DisplayName.Trim(), filename, BCC, parms.MaxSize))
            {
                return;
            }
            if (filename != null && !filename.Trim().Equals(""))
            {
                string[] a = filename.Split(',');
                foreach (string file in a)
                    if (!file.Trim().Equals(""))
                        File.Delete(file);

            }
            if (!Batch)
                MessageBoxCustom.Show("Message sent successfully.");
            else
            {
                frmwait.Item = "";
                frmwait.Item = "Finished SENDING";
            }


        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetOutlookContacts()
        {
            WinTrm.Utils.GetOutlookContacts(lstOutlook);
            /*
            WinTrm.BindingClasses.bindAccountEmails[] olContacts = WinTrm.Utils.getOutlookContacts();
            lstOutlook.ItemsSource = olContacts;
            lstOutlook.DisplayMemberPath = "Name";
            */

        }
        private void chkOutlook_Checked(object sender, RoutedEventArgs e)
        {
            if (!chkOutlook.IsChecked.Value)
            {
                lstOutlook.ItemsSource = null;
                lstOutlook.IsEnabled = false;
                OutlookLeft.IsEnabled = false;
                OutlookRight.IsEnabled = false;
            }
            else
            {
                GetOutlookContacts();
                lstOutlook.IsEnabled = true;
                OutlookLeft.IsEnabled = true;
                OutlookRight.IsEnabled = true;
                /*
                  BackgroundWorker worker = new BackgroundWorker();
                  worker.DoWork += GetOutlookContacts;
                  object[] parms = { lstOutlook };
                  if (!worker.IsBusy)
                      worker.RunWorkerAsync(parms);
                      */
            }
        }

        private static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void lstEmailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lstEmailAddress.SelectedIndex == -2)
            {
                string address = lstEmailAddress.Text;
                if (!TestEmail.IsEmail(address))
                {
                    MessageBoxCustom.Show("Invalid Email Address.");
                    return;
                }
                if (parms.EmailAddresses == null)
                    parms.EmailAddresses = new string[0];
                string[] addresses = parms.EmailAddresses;
                Array.Resize(ref addresses, parms.EmailAddresses.Length + 1);
                addresses[addresses.Length - 1] = address;
                parms.EmailAddresses = addresses;
                grdTop.DataContext = null;
                grdTop.DataContext = parms;
            }

        }

        private void cmdSingleAddress_Click(object sender, RoutedEventArgs e)
        {
            string address = lstEmailAddress.Text;
            foreach (string s in lstRecipients.Items)
            {
                if (s.Equals(address))
                    return;
            }
            lstRecipients.Items.Add(address);
            if (lstEmailAddress.SelectedIndex == -1)
            {
                if (!TestEmail.IsEmail(address))
                {
                    MessageBoxCustom.Show("Invalid Email Address.");
                    return;
                }
                if (parms.EmailAddresses == null)
                    parms.EmailAddresses = new string[0];
                string[] addresses = parms.EmailAddresses;
                Array.Resize(ref addresses, parms.EmailAddresses.Length + 1);
                addresses[addresses.Length - 1] = address;
                parms.EmailAddresses = addresses;
                grdTop.DataContext = null;
                grdTop.DataContext = parms;
            }
        }

        private void ListRightArrow_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = lstRecipientList;
            if (list == null || list.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select an item in the list to move.");
                return;
            }
            RecipientMaster item = (RecipientMaster)list.SelectedItem;
            string entry = "[" + item.listName + "]" + item.listId.ToString();

            foreach (string s in lstRecipients.Items)
            {
                if (s.Equals(entry))
                    return;
            }
            lstRecipients.Items.Add(entry);

        }

        private void ListLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = lstRecipients;
            if (list == null || list.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select an item in the list to delete.");
                return;
            }
            list.Items.RemoveAt(list.SelectedIndex);
        }

        private void OutlookLeft_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = lstOutlook;
            if (list == null || list.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select an item in the list to move.");
                return;
            }
            WinTrm.BindingClasses.bindAccountEmails item = (WinTrm.BindingClasses.bindAccountEmails)list.SelectedItem;
            string entry = item.Email;

            foreach (string s in lstRecipients.Items)
            {
                if (s.Equals(entry))
                    return;
            }
            lstRecipients.Items.Add(entry);

        }

        private void OutlookRight_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = lstRecipients;
            if (list == null || list.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select an item in the list to delete.");
                return;
            }
            list.Items.RemoveAt(list.SelectedIndex);

        }

        private void cmdEmailConfig_Click(object sender, RoutedEventArgs e)
        {
            Email.frmSmtpConfigurator frm = new Email.frmSmtpConfigurator();
            frm.ShowDialog();
            lstAccounts.ItemsSource = null;
            Email.SmtpConfig[] smtps = Email.SmtpConfig.loadByAccount(AccountId);
            lstAccounts.ItemsSource = smtps;

        }
    }

    public class EmailAttachmentParms
    {
        // Properties
        public bool UseOutlook { get; set; }
        public string[] EmailAddresses { get; set; }
        public bool UseBCC { get; set; }
        public decimal MaxSize { get; set; } = 0;

        public static EmailAttachmentParms DeSerializeObject(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EmailAttachmentParms));
            StringReader rdr = new StringReader(inputString);
            EmailAttachmentParms parms = (EmailAttachmentParms)serializer.Deserialize(rdr);
            return parms;
        }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }
    }

}
