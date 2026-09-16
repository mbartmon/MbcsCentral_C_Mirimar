using MbcsMail;
using System.Windows;
using System.Windows.Controls;
using Chilkat;
using Microsoft.Win32;

namespace MbcsCentral.Email
{
    /// <summary>
    /// Interaction logic for frmSmtpConfigurator.xaml
    /// </summary>
    public partial class frmSmtpConfigurator : Window
    {
        System.Windows.Data.CollectionViewSource SmtpConfigViewSource;
        SmtpConfig[] smtpAccts;
        long currentId = 0;
        bool isLoading = true;
        Window frmMail;
        Grid gridMail;
        bool all = false;
        private System.Windows.Controls.Button withEventsField_cmdTestClose;
        public int AccountId { get; set; } = -1;

        internal System.Windows.Controls.Button cmdTestClose
        {
            get { return withEventsField_cmdTestClose; }
            set
            {
                if (withEventsField_cmdTestClose != null)
                {
                    withEventsField_cmdTestClose.Click -= cmdTestClose_Click;
                }
                withEventsField_cmdTestClose = value;
                if (withEventsField_cmdTestClose != null)
                {
                    withEventsField_cmdTestClose.Click += cmdTestClose_Click;
                }
            }

        }
        string thisApp;
        public frmSmtpConfigurator()
        {
            InitializeComponent();
        }
        public frmSmtpConfigurator(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
        }

        public frmSmtpConfigurator(string thisApp, string parm)
        {
            InitializeComponent();
            this.thisApp = thisApp;
            if (parm.Trim().ToLower().Equals("account"))
                this.AccountId = (int)GlobalShared.user.Account.accountId;
            else if (parm.Trim().ToLower().Equals("all"))
                this.all = true;
        }
        
        private void formSmtpDelivery_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            AccountId = (int)GlobalShared.user.Account.accountId;
            //Load data by setting the CollectionViewSource.Source property:
            //SmtpConfigViewSource.Source = [generic data source]
            if (this.all)
                AccountId = -1;
            if (AccountId == -1)
                pnlAccount.Visibility = Visibility.Collapsed;
            else
            {
                Accounts acct = Accounts.load(AccountId);
                if (acct != null)
                    txtAccountName.Text = acct.Name;
                else
                    txtAccountName.Text = "* NOT FOUND *";
            }
            MailTags[] mailTags = MailTags.load();
            System.Windows.Data.CollectionViewSource mailTagSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("MailTags")));
            mailTagSource.Source = mailTags;
            getSmtpAccts();


        }


        private void getSmtpAccts()
		{
            SmtpConfigViewSource = null;
            SmtpConfigViewSource = ((System.Windows.Data.CollectionViewSource) (this.FindResource("SmtpConfigViewSource")));
            if (AccountId == -1)
                smtpAccts = SmtpConfig.load();
            else
                smtpAccts = SmtpConfig.loadByAccount(AccountId);
            SmtpConfigViewSource.Source = smtpAccts;
			//gridMain.DataContext = smtpAccts;

		}

        /*
		private void lstSmtpAccts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!this.IsLoaded) {
				if (smtpAccts[currentId].IsChanged) {
					if (MessageBox.Show("Save your changes?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
						smtpAccts[currentId].authentication = lstAuthentication.SelectedIndex;
						smtpAccts[currentId].in_authentication = lstAuthenticationIn.SelectedIndex;
						smtpAccts[currentId].save();
						if (smtpAccts[currentId].defaultAccount) {
							for (int i = 0; i <= smtpAccts.Length - 1; i++) {
								if (i != currentId & smtpAccts[i].defaultAccount) {
									smtpAccts[i].defaultAccount = false;
									smtpAccts[i].save();
									break; 
								}
							}
						}
					}
					smtpAccts[currentId].IsChanged = false;
				}
			}
			currentId = lstSmtpAccts.SelectedIndex;
			lstAuthentication.SelectedIndex = smtpAccts[currentId].authentication;
			lstAuthenticationIn.SelectedIndex = smtpAccts[currentId].in_authentication;

		}
        */
        /*
		private void cmdTest_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Chilkat.MailMan mail = new Chilkat.MailMan();
            bool success = mail.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
            if (!success)
            {
                MessageBox.Show(mail.LastErrorText);
                return;
            }
            mail.AutoFix = false;
            mail.SmtpHost = ServerTextBox.Text;
            mail.SmtpPort = int.Parse(PortTextBox.Text);
            if (SSLCheckBox.IsChecked.Value)
                mail.SmtpSsl = true;
            else if (TSLCheckBox.IsChecked.Value)
                mail.StartTLS = true;
            mail.SmtpUsername = UserNameTextBox.Text;
            mail.SmtpPassword = PasswordTextBox.Text;
            //mail.SmtpPort = int.Parse(PortInTextBox.Text);

            Chilkat.Email email = new Chilkat.Email();

            email.Subject = "Test From MbcsCentral";
            email.Body = "This is a test message generated by MbcsCentral\n A product of MBCS";
            email.From = "MbcsCentral Support <mbartmon@gmail.com>";
            email.AddTo(GlobalShared.user.getName, EmailAddressTextBox.Text);
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
            else {
                success = mail.CloseSmtpConnection();
                MessageBoxCustom.Show("Message successfully sent");
            }
            /*
			frmMail = new Window();
			frmMail.Title = "Email Account Test";
			frmMail.Owner = this;
			frmMail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			frmMail.Width = this.Width;
			frmMail.Height = this.Height;
			gridMail = new Grid();
			gridMail.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			gridMail.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35, GridUnitType.Pixel) });
			//Grid.SetColumn(gridMail, 0);
			//Grid.SetRow(gridMail, 0);
			//Grid.SetRowSpan(gridMail, 4);
           // gridMain.Children.Add(gridMail);

            System.Windows.Controls.TextBox txtMail = new System.Windows.Controls.TextBox();
			txtMail.HorizontalAlignment = HorizontalAlignment.Stretch;
			txtMail.VerticalAlignment = VerticalAlignment.Stretch;
			txtMail.BorderThickness = new Thickness(2);
			cmdTestClose = new System.Windows.Controls.Button {
				Name = "cmdTestClose",
				Content = "CLOSE",
				HorizontalAlignment = HorizontalAlignment.Center
			};
			gridMail.Children.Add(cmdTestClose);
			Grid.SetRow(cmdTestClose, 1);
			frmMail.Content = gridMail;
			frmMail.Show();
			frmMail.BringIntoView();

			MbcsMail.MbcsMail mail = null;
			try {
                mail = new MbcsMail.MbcsMail(); // (ServerTextBox.Text, int.Parse(PortTextBox.Text), null, PasswordTextBox.Text);
			} catch (System.Exception ex) {
				MessageBox.Show("Error during creation of mail object: " + ex.Message);
			}
			//mail.authentication = lstAuthentication.SelectedIndex;
            
            try {
            	mail.setMessage(EmailAddressTextBox.Text, EmailAddressTextBox.Text, UserNameTextBox.Text, "Test Message from MBCS Wintrm", "This is a test message generated by MbcsCentral\n A product of MBCS");
            } catch (System.Exception ex) {
            	MessageBox.Show("Error during setMessage method of mail object: " + ex.Message);
            }
            mail.SmtpHost = ServerTextBox.Text;
            mail.SmtpPort = int.Parse(PortInTextBox.Text);
            if (SSLCheckBox.IsChecked.Value)
                mail.SmtpSsl = true;
			mail.emailAddress = EmailAddressTextBox.Text;
			mail.SmtpUsername = UserNameTextBox.Text;
            mail.SmtpPassword = PasswordTextBox.Text;
            mail.SmtpPort = int.Parse(PortInTextBox.Text);
            mail.subject = "Test Message from MbcsCentral";
            mail.body = "This is a test.";
            mail.displayName = GlobalShared.user.getName;
			mail.setToAddr(EmailAddressTextBox.Text, GlobalShared.user.getName);

			bool rc = false;
			rc = mail.Send();
			if (!rc)
            {
                switch (mail.LastSmtpStatus)
                {
                    case 535:
                        MessageBox.Show("Authentication Failed: Bad username or password.");
                        break;
                    default:
				        MessageBox.Show("ERROR SMTP STATUS = " + mail.LastSmtpStatus);
                        break;
                }
            } else {
				txtMail.Text = "Message successfully sent";
				//MsgBox("Message successfully sent")
			}
        }
        */
    
        private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            SmtpConfig smtp = new SmtpConfig() { AccountId = (int)GlobalShared.user.Account.accountId };
            frmAccountAddEdit frm = new frmAccountAddEdit() { EmailAccount = smtp };
            frm.ShowDialog();
            getSmtpAccts();

            return;

			//StackFields.Visibility = Visibility.Collapsed;
			//StackFields_in.Visibility = Visibility.Collapsed;
			Grid1.Visibility = Visibility.Collapsed;
			Grid2.Visibility = Visibility.Collapsed;
            Grid10.Visibility = Visibility.Collapsed;
            pnlMailTag.Visibility = Visibility.Collapsed;
			StackAddSmtp.Visibility = Visibility.Visible;
			cmdSave.Visibility = Visibility.Hidden;
			cmdDelete.Visibility = Visibility.Hidden;
			//cmdTest.Visibility = Visibility.Hidden;
            DescriptionTextBox.Focus();
           
		}


		private void cmdCancelNewSmtp_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//StackFields.Visibility = Visibility.Visible;
			//StackFields_in.Visibility = Visibility.Visible;
			Grid1.Visibility = Visibility.Visible;
			Grid2.Visibility = Visibility.Visible;
            Grid10.Visibility = Visibility.Visible;
            pnlMailTag.Visibility = Visibility.Visible;
			StackAddSmtp.Visibility = Visibility.Collapsed;
			cmdSave.Visibility = Visibility.Visible;
			cmdDelete.Visibility = Visibility.Visible;
			//cmdTest.Visibility = Visibility.Visible;

		}


		private void cmdSaveNewSmtp_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            //SmtpConfig smtp = new SmtpConfig(0, NewSmtpAccount.Text, "", 0, "", "", 25, false, false, 0,"", "", "", 110, false, false, 0);
            SmtpConfig smtp = new SmtpConfig(0, 25, 0, 110, 0, false, false, false, false, false, "", NewSmtpAccount.Text, "", "", "", "", "", "", "");
            if (AccountId > -1)
                smtp.AccountId = this.AccountId;
			smtp.save();
			cmdCancelNewSmtp_Click(sender, e);
			getSmtpAccts();

		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			try {
				if (smtpAccts[currentId].IsChanged) {
					switch (MessageBox.Show("Save your changes?","", MessageBoxButton.YesNoCancel)) {
						case MessageBoxResult.Yes:
							//smtpAccts[currentId].authentication = lstAuthentication.SelectedIndex;
							//smtpAccts[currentId].in_authentication = lstAuthenticationIn.SelectedIndex;
							smtpAccts[currentId].save();
							if (smtpAccts[currentId].defaultAccount) {
								for (int i = 0; i <= smtpAccts.Length - 1; i++) {
									if (i != currentId & smtpAccts[i].defaultAccount) {
										smtpAccts[i].defaultAccount = false;
										smtpAccts[i].save();
										break; // TODO: might not be correct. Was : Exit For
									}
								}
							}
							break;
						case MessageBoxResult.Cancel:
							return;

							break;
					}
				}
			} catch {
			}
			Main.saveForm(this);
			this.Close();

		}


		private void cmdDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (MessageBox.Show("Do you want to DELETE the current SMTP Account?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
				smtpAccts[currentId].delete(smtpAccts[currentId].ID.ToString());
				getSmtpAccts();
			}

		}


		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (smtpAccts == null || smtpAccts.Length == 0)
            {
                
                MessageBox.Show("Click ADD NEW EMAIL ACCOUNT before clicking SAVE.");
                return;
            }
            //smtpAccts[currentId].authentication = lstAuthentication.SelectedIndex;
            //smtpAccts[currentId].in_authentication = lstAuthenticationIn.SelectedIndex;
            if (all)
            {
                if (MessageBoxCustom.Show("Save SMTP Dataset?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
                {
                    SaveFileDialog diag = new SaveFileDialog();
                    diag.FileName = "SmtpConfig";
                    diag.DefaultExt = ".xml";
                    diag.Filter = "XML (*.xml) | *.xml";
                    if (diag.ShowDialog() == true)
                    {
                        smtpAccts[0].Data.WriteXml(diag.FileName); 
                        return;
                    }
                }
            }
            smtpAccts[currentId].save();
            smtpAccts[currentId].IsChanged = false;

			if (smtpAccts[currentId].defaultAccount) {
				for (int i = 0; i <= smtpAccts.Length - 1; i++) {
					if (i != currentId & smtpAccts[i].defaultAccount) {
						smtpAccts[i].defaultAccount = false;
						smtpAccts[i].save();
						break; 
					}
				}
			}

		}

		private void cmdTestClose_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Main.saveForm(this);
			frmMail.Close();
		}

        private void EmailAddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!EmailAddressTextBox.Text.Trim().Equals(""))
            {
                Chilkat.MailMan mail = new Chilkat.MailMan();
                bool success = mail.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
                string hostName = mail.MxLookup(EmailAddressTextBox.Text);
            }
        }

        private void lstSmtpAccts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox lst = sender as ListBox;
            if (lst.SelectedIndex >= 0)
            {
                SmtpConfig smtp = lst.SelectedItem as SmtpConfig;
                if (smtp != null)
                {
                    frmAccountAddEdit frm = new frmAccountAddEdit { EmailAccount = smtp };
                    frm.ShowDialog();
                }
            }
        }
    }
}
