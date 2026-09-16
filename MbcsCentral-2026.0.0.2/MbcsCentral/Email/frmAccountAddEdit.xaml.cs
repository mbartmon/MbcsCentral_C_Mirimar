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


namespace MbcsCentral.Email
{
    /// <summary>
    /// Interaction logic for frmAccountAddEdit.xaml
    /// </summary>
    public partial class frmAccountAddEdit : Window
    {
        public frmAccountAddEdit()
        {
            InitializeComponent();
        }

        private void frmAccountAddEdit_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            this.DataContext = EmailAccount;
            MailTags[] mailTags = MailTags.load();
            System.Windows.Data.CollectionViewSource mailTagSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("MailTags")));
            mailTagSource.Source = mailTags;

            /*
            if (EmailAccount != null && EmailAccount.MailTag != null)
            {
                for(int i = 0; i <  lstMailTag.Items.Count; i++)
                    if (EmailAccount.MailTag.Equals(((MailTags)lstMailTag.Items[i]).MailTag))
                    {
                        lstMailTag.SelectedIndex = i;
                        break;
                    }
            }
            */

            if (EmailAccount.SSL && EmailAccount.TSL)
            {
                lstSecurityType.SelectedIndex = 4;
            }
            else if (EmailAccount.SSL)
                lstSecurityType.SelectedIndex = 2;
            else if (EmailAccount.TSL)
                lstSecurityType.SelectedIndex = 3;

            if (EmailAccount.in_SSL && EmailAccount.in_TSL)
            {
                lstInSecurityType.SelectedIndex = 4;
            }
            else if (EmailAccount.in_SSL)
                lstInSecurityType.SelectedIndex = 2;
            else if (EmailAccount.in_TSL)
                lstInSecurityType.SelectedIndex = 3;


        }

        private void frmAccountAddEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

            SetSecurityTypes();

            if (EmailAccount.IsChanged)
                EmailAccount.save();
            this.Close();
        }

        private void SetSecurityTypes()
        {
            switch (lstSecurityType.SelectedIndex)
            {
                case 0:
                    EmailAccount.SSL = false;
                    EmailAccount.TSL = false;
                    break;
                case 1:
                    break;
                case 2:
                    EmailAccount.SSL = true;
                    EmailAccount.TSL = false;
                    break;
                case 3:
                    EmailAccount.SSL = false;
                    EmailAccount.TSL = true;
                    break;
                case 4:
                    EmailAccount.SSL = true;
                    EmailAccount.TSL = true;
                    break;
            }

            switch (lstInSecurityType.SelectedIndex)
            {
                case 0:
                    EmailAccount.in_SSL = false;
                    EmailAccount.in_TSL = false;
                    break;
                case 1:
                    break;
                case 2:
                    EmailAccount.in_SSL = true;
                    EmailAccount.in_TSL = false;
                    break;
                case 3:
                    EmailAccount.in_SSL = false;
                    EmailAccount.in_TSL = true;
                    break;
                case 4:
                    EmailAccount.in_SSL = true;
                    EmailAccount.in_TSL = true;
                    break;
            }

        }
        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetSecurityTypes();

            Chilkat.MailMan mail = new Chilkat.MailMan();
            bool success = mail.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
            if (!success)
            {
                MessageBox.Show(mail.LastErrorText);
                return;
            }
            mail.AutoFix = false;
            mail.SmtpHost = txtSmtpServer.Text;
            mail.SmtpPort = int.Parse(txtOutPort.Text);
            if (EmailAccount.SSL)
                mail.SmtpSsl = true;
            if (EmailAccount.TSL)
                mail.StartTLS = true;
            mail.SmtpUsername = txtUserName.Text;
            mail.SmtpPassword = txtPassword.Text;
            //mail.SmtpPort = int.Parse(PortInTextBox.Text);

            Chilkat.Email email = new Chilkat.Email();

            email.Subject = "Test From MbcsCentral";
            email.Body = "This is a test message generated by MbcsCentral\n A product of MBCS";
            email.From = "MbcsCentral Support <" + txtEmailAddress.Text + ">";
            email.AddTo(GlobalShared.user.getName, txtEmailAddress.Text);
            success = mail.SendEmail(email);
            if (!success)
            {
                switch (mail.LastSmtpStatus)
                {
                    case 535:
                        MessageBoxCustom.Show("Authentication Failed: Bad username or password.");
                        break;
                    default:
                        MessageBoxCustom.Show("ERROR SMTP STATUS = " + mail.LastErrorText);
                        break;
                }
            }
            else
            {
                success = mail.CloseSmtpConnection();
                MessageBoxCustom.Show("Message successfully sent");
            }
        }
        public SmtpConfig EmailAccount { get; set; }

        private void txtEmailAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == null || txtUserName.Text.Trim().Equals(""))
                txtUserName.Text = txtEmailAddress.Text;
        }
    }
}
