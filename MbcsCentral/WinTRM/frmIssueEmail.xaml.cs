using Chilkat;
using Microsoft.Win32;
using MBCSsmtp.MbcsMail;
using System.Net.Mail;
using System.Windows.Forms.ListBox;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;
namespace WinTRM
{

	public class frmIssueEmail
	{

		private ListItem[] p_tenants;
		private ListItem[] p_accounts;
		private ListItem p_responsible;
		private long _issueId;

		private long _tenantId;
		private ItemSource itemsource;
		private string Username;
		private string Password;
		private string Host;
		private string popHost;
		private int Port;
		private int popPort;
		private string Email;
		private bool SSL;

		private bool TSL;
		private string companyName = null;

		private string emailSignature = null;
			// 0 = general, 1 = note add/change
		private int _emailType = eType.general;
		private ItemSource tenantRecipients = null;

		private ItemSource accountRecipients = null;

		private MbcsMail mail;
		public int emailType {
			get { return _emailType; }
			set { _emailType = value; }
		}
		public long issueId {
			set { _issueId = value; }
		}
		public long tenantId {
			set { _tenantId = value; }
		}
		public ObjectCollection tens {
			set {
				p_tenants = new ListItem[value.Count + 1];
				int i = 0;
				foreach (ListItem ten in value) {
					p_tenants(i) = new ListItem(ten.DisplayItem, ten.ValueItem);
					i += 1;
				}
			}
		}
		public SelectedObjectCollection users {
			set {
				p_accounts = new ListItem[value.Count + 1];
				int i = 0;
				foreach (ListItem acct in value) {
					p_accounts(i) = new ListItem(acct.DisplayItem, acct.ValueItem);
					i += 1;
				}
			}
		}
		public ListItem responsible {
			set {
				if ((value != null)) {
					p_responsible = new ListItem(value.DisplayItem, value.ValueItem);
				} else {
					p_responsible = null;
				}
			}
		}


		private void frmIssueEmail_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			SmtpConfig[] smtpAccts = SmtpConfig.load();
			SmtpConfig smtpAcct = null;
			bool smtpDefault = false;
			if ((smtpAccts == null)) {
				Interaction.MsgBox("You have no Email Accounts:  Cannot send email messages.");
				this.Close();
			}
			foreach (SmtpConfig smtpAcct_loopVariable in smtpAccts) {
				smtpAcct = smtpAcct_loopVariable;
				if (smtpAcct.defaultAccount) {
					smtpDefault = true;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			if (!smtpDefault) {
				Interaction.MsgBox("You have not designated a default Email Account.  You must do so before sending email");
				this.Close();
			}
			Username = smtpAcct.userName;
			Password = smtpAcct.password;
			Host = smtpAcct.server;
			Port = smtpAcct.port;
			Email = smtpAcct.emailAddress;
			SSL = smtpAcct.SSL;
			TSL = smtpAcct.TSL;
			smtpAcct = null;

			if (string.IsNullOrEmpty(Username.Trim) | string.IsNullOrEmpty(Host.Trim) | Port <= 0 | string.IsNullOrEmpty(Email.Trim)) {
				Interaction.MsgBox("Email configuration needs to be setup.");
				this.Close();
			}

			try {
				Options opt = Options.load;
				if ((opt != null)) {
					companyName = opt.companyName;
					emailSignature = opt.emailSignature;
				}
				opt = null;
			} catch (System.Exception ex) {
			}
			itemsource = new ItemSource();
			foreach (ListItem lst in p_tenants) {
				if ((lst != null))
					itemsource.Add(false, lst.DisplayItem, lst.ValueItem);
			}
			lstTenants.DataContext = itemsource;
			itemsource = new ItemSource();
			foreach (ListItem lst in p_accounts) {
				if ((lst != null))
					itemsource.Add((_emailType == eType.note ? true : false), lst.DisplayItem, lst.ValueItem);
			}
			lstAccounts.DataContext = itemsource;
			chkResponsible.Content = "";
			if ((p_responsible != null)) {
				chkResponsible.Content = p_responsible.DisplayItem;
			}
			if (string.IsNullOrEmpty(chkResponsible.Content.ToString.Trim)) {
				chkResponsible.Visibility = false;
				chkResponsible.IsChecked = false;
			}

		}

		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			try {
				mail.Dispose();
			} catch (System.Exception ex) {
				// MsgBox(ex.Message)
			}
			this.Close();

		}


		private void cmdSend_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Tenants ten = null;
			try {
				foreach (emailIssueRecord itm in lstTenants.Items) {
					if (itm.active) {
						ten = Tenants.load(itm.id);
						if ((ten != null)) {
							itm.email1 = ten.getEmail1.Trim;
							itm.email2 = ten.getEmail2.Trim;
						}
						if ((ten == null) | (string.IsNullOrEmpty(itm.email1.Trim) & string.IsNullOrEmpty(itm.email2.Trim))) {
							if (Interaction.MsgBox("Tenant <" + itm.name + "> has no email address.  Continue anyway?", MsgBoxStyle.YesNo) == MsgBoxResult.No) {
								this.Close();
							}
						} else {
							if ((tenantRecipients == null)) {
								tenantRecipients = new ItemSource();
							}
							tenantRecipients.Add(itm);
						}
						ten = null;
					}
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox("Tenants: " + ex.Message);
			}

			Accounts acct = null;
			try {
				if ((p_responsible != null) & chkResponsible.IsChecked) {
					emailIssueRecord emailR = new emailIssueRecord(true, p_responsible.DisplayItem, p_responsible.ValueItem);
					accountRecipients = new ItemSource();
					acct = Accounts.load(p_responsible.ValueItem);
					if ((acct != null)) {
						emailR.email1 = acct.getEmail().Trim;
					}
					if ((acct == null) | string.IsNullOrEmpty(emailR.email1.Trim)) {
						if (Interaction.MsgBox("Account <" + emailR.name + "> has no email address.  Continue anyway?", MsgBoxStyle.YesNo) == MsgBoxResult.No) {
							this.Close();
						}
					} else {
						if ((accountRecipients == null)) {
							accountRecipients = new ItemSource();
						}
						accountRecipients.Add(emailR);
					}
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox("Responsible: " + ex.Message);
			}
			try {
				foreach (emailIssueRecord itm in lstAccounts.Items) {
					if (itm.active) {
						acct = Accounts.load(itm.id);
						if ((acct != null)) {
							itm.email1 = acct.getEmail().Trim;
						}
						if ((acct == null) | string.IsNullOrEmpty(itm.email1.Trim)) {
							if (Interaction.MsgBox("Account <" + itm.name + "> has no email address.  Continue anyway?", MsgBoxStyle.YesNo) == MsgBoxResult.No) {
								this.Close();
							}
						} else {
							if ((accountRecipients == null)) {
								accountRecipients = new ItemSource();
							}
							accountRecipients.Add(itm);
						}
						acct = null;
					}
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox("Accounts: " + ex.Message);
			}

			if (string.IsNullOrEmpty(txtSubject.Text.Trim)) {
				Interaction.MsgBox("SUBJECT cannot be blank.");
				return;
			}
			if (string.IsNullOrEmpty(txtMessage.Text.Trim)) {
				Interaction.MsgBox("MESSAGE cannot be blank.");
				return;
			}

			if ((tenantRecipients == null) & (accountRecipients == null)) {
				Interaction.MsgBox("You have not selected any recipients for this email message");
				return;
			}

			try {
				if ((tenantRecipients != null)) {
					foreach (emailIssueRecord tenant in tenantRecipients) {
						doSend(tenant, ref accountRecipients);
					}
				} else {
					doSend(null, ref accountRecipients);
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox("Tenant Recips: " + ex.Message);
			} finally {
				cmdCancel_Click(sender, e);
			}

		}


		private void doSend(emailIssueRecord toAddr, ref ItemSource ccAddrs)
		{
			mail = new MbcsMail(Host, Port, 0, Password);
			if (TSL) {
				mail.StartTLS = true;
			} else if (SSL) {
				mail.SmtpSsl = true;
			}

			if ((toAddr != null)) {
				txtSubject.Text += string.Format(GlobalShared.subjectRef, _issueId, GlobalShared.user.getAccountId(), toAddr.id);
				if (!string.IsNullOrEmpty(toAddr.email1.Trim)) {
					mail.setMessage(Email, toAddr.email1, GlobalShared.user.getName(), txtSubject.Text, txtMessage.Text);
					mail.setHeader(GlobalShared.ISSUE_HEADER, string.Format(GlobalShared.subjectRef, _issueId, GlobalShared.user.getAccountId(), toAddr.id));
					if (!string.IsNullOrEmpty(toAddr.email2.Trim)) {
						mail.setAddTo(toAddr.email2, toAddr.name);
					}
				} else {
					mail.setMessage(Email, toAddr.email2, GlobalShared.user.getName(), txtSubject.Text, txtMessage.Text);
					mail.setHeader(GlobalShared.ISSUE_HEADER, string.Format(GlobalShared.subjectRef, _issueId, GlobalShared.user.getAccountId(), toAddr.id));
				}
				if ((ccAddrs != null)) {
					foreach (emailIssueRecord itm in ccAddrs) {
						mail.setAddCC(itm.email1);
					}
				}

				if ((p_responsible != null)) {
				}
			} else if ((ccAddrs == null)) {
				return;
			} else {
				int i = 0;
				txtSubject.Text += string.Format(GlobalShared.subjectRef, _issueId, GlobalShared.user.getAccountId(), _tenantId);
				foreach (emailIssueRecord itm in ccAddrs) {
					if (i == 0) {
						mail.setMessage(Email, itm.email1, GlobalShared.user.getName(), txtSubject.Text, txtMessage.Text);
						mail.setHeader("X-Ref", string.Format(GlobalShared.subjectRef, _issueId, GlobalShared.user.getAccountId(), _tenantId));
						mail.setHeader("Return-Receipt-To", "<" + GlobalShared.user.getEmail() + ">");
					}
					mail.setAddTo(itm.email1, itm.name);
					i += 1;
				}
			}
			try {
				mail.SmtpUsername = Username;
				mail.emailAddress = Email;
				mail.displayName = GlobalShared.user.getName();
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
				return;
			}

			bool rc = mail.Send;
			if (rc) {
				string filename = saveEmail(mail.getFullMessage, _issueId);
				Messages message = new Messages(0, _issueId, GlobalShared.user.getAccountId(), Now(), 0, Now(), false, false, null, true,
				filename);
				message.save();
				Interaction.MsgBox("Message successfully sent");
			} else {
				Interaction.MsgBox("ERROR: " + mail.LastErrorText);
				GlobalShared.Log.Log(LogClass.logType.Debug, mail.LastErrorText);
			}
			mail.Dispose();

		}

		protected override void Finalize()
		{
			try {
				base.Finalize();
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}
		}
		public frmIssueEmail()
		{
			Loaded += frmIssueEmail_Loaded;
		}

	}
}
namespace WinTRM
{

	public class ItemSource : ObservableCollection<emailIssueRecord>
	{


		public ItemSource()
		{
		}

		public void Add(bool active, string name, long id)
		{
			base.Add(new emailIssueRecord(active, name, id));
		}

		public void @add(emailIssueRecord rec)
		{
			base.Add(rec);
		}

	}
}
namespace WinTRM
{

	public class emailIssueRecord : INotifyPropertyChanged
	{

		private bool _active;
		private string _name;
		private long _id;
		private string _email1 = "";

		private string _email2 = "";

		public emailIssueRecord()
		{
		}

		public emailIssueRecord(bool active, string name, long id)
		{
			_active = active;
			_name = name;
			_id = id;
		}

		public emailIssueRecord(emailIssueRecord rec)
		{
			this._active = rec.active;
			this._name = rec.name;
			this._id = rec.id;
			this._email1 = rec.email1;
			this._email2 = rec.email2;
		}

		public void onPropertyChanged(string name)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public event PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged;
		public delegate void PropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

		public bool active {
			get { return _active; }
			set {
				_active = value;
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs("active"));
				}
			}
		}
		public string name {
			get { return _name; }
			set {
				_name = value;
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs("name"));
				}
			}
		}
		public long id {
			get { return _id; }
			set {
				_id = value;
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs("id"));
				}
			}
		}
		public string email1 {
			get { return _email1; }
			set {
				_email1 = value;
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs("email1"));
				}
			}
		}
		public string email2 {
			get { return _email2; }
			set {
				_email2 = value;
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs("email2"));
				}
			}
		}
	}
}
