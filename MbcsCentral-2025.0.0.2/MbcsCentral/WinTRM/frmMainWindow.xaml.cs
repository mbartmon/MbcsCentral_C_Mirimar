using System.IO.Pipes.NamedPipeClientStream;
using System.Windows;
using System.Windows.Interop;
using System.Net.Mail;
using System.Threading;
using Microsoft.Win32;
//Imports Email.Net.Common
using MBCSsmtp;
using Chilkat.Email;
namespace WinTRM
{

	public class frmMainWindow
	{
		private bool admin = false;
		private bool vAll = false;
		private int shwBy = ShowBy.ctxNotesShowEntered;
		private int emailBy = MailBy.Issue;
		private SmtpConfig[] smtpAccts;
		private SmtpConfig currSmtpAcct;
		private bool inMailBox = false;
		internal System.Threading.Timer getEmail;
		private mailObj mailObj = new mailObj();

		private System.Threading.Thread myThread;
		// Branch for evaluating Chilkat Email library


		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
		{
			frmMainWindow_Closed(sender, e);
			System.Environment.Exit(0);

		}


		private void mnuMessages_Click(object sender, EventArgs e)
		{
			frmContents frm = new frmContents();
			frm.setAccountId(GlobalShared.user.getAccountId());
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();

		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmIssuesDashboard frm = new frmIssuesDashboard();
			frm.ShowDialog();
		}


		private void mnuSchedules_Click(object sender, EventArgs e)
		{
			frmSchedules frm = new frmSchedules();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();

		}

		private void EditAccountsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Windows.Forms.Form frm = new frmAccounts();
			frm.Show();
		}


		private void frmMainWindow_Closed(object sender, System.EventArgs e)
		{
			serializeInbox(GlobalShared.inbox);
			Utils.saveForm(this);

		}


		private void frmMain_Load(object sender, System.EventArgs e)
		{
			// Check for application update
			try {
				WindowInteropHelper wih = new WindowInteropHelper(this);
				GlobalShared.mainHWND = wih.Handle;

				System.Diagnostics.ProcessStartInfo psInfo = new System.Diagnostics.ProcessStartInfo(GlobalShared.appPath + "twuxW.exe", "/qb /w:" + Convert.ToString(GlobalShared.mainHWND) + " " + GlobalShared.UPDATE_URL);
				psInfo.CreateNoWindow = false;
				System.Diagnostics.Process.Start(psInfo);
			} catch {
			}

			System.Drawing.Rectangle rect = Utils.getForm(this);
			this.Left = rect.Left;
			this.Top = rect.Top;
			this.Width = rect.Width;
			this.Height = rect.Height;

			GlobalShared.Log.Log(LogClass.logType.Config, "Starting main form");
			this.Title += "User: " + GlobalShared.user.getName + " <Version " + WinTRM.My.MyProject.Application.Info.Version.ToString + ">";
			GlobalShared.Log.Log(LogClass.logType.Config, "Loading tenant list");
			GlobalShared.pmFlag = Utils.getTenants(lstTenants);
			GlobalShared.Log.Log(LogClass.logType.Config, "Loading recipients list");
			Utils.getRecipients(lstRecipients);
			GlobalShared.Log.Log(LogClass.logType.Config, "Loading issues list");
			Utils.getAllNotes(lstIssues, shwBy, vAll);
			//inbox = Utils.retrieveInbox()
			//grdInbox.ItemsSource = Nothing
			//grdInbox.ItemsSource = inbox
			GlobalShared.Log.Log(LogClass.logType.Config, "Loading messages list");
			Utils.getListOfMessages(lstMessages, GlobalShared.user.getAccountId, emailBy);
			if (GlobalShared.user.hasAdminPermission | GlobalShared.user.hasManagerPermission) {
				chkAllAccounts.IsEnabled = true;
				admin = true;
			} else {
				chkAllAccounts.IsEnabled = false;
			}
			ctxNotesShowEntered.IsChecked = true;

			GlobalShared.inbox = new List<EmailMessage>();
			try {
				GlobalShared.inbox = unserializeInbox(GlobalShared.inbox);
				updateGrid();

			} catch (System.Exception ex) {
			}

			System.Windows.Data.CollectionViewSource SmtpConfigViewSource = default(System.Windows.Data.CollectionViewSource);
			SmtpConfigViewSource = null;
			SmtpConfigViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("SmtpConfigViewSource");
			smtpAccts = SmtpConfig.load;
			SmtpConfigViewSource.Source = smtpAccts;
			SmtpConfig itm = default(SmtpConfig);
			for (int i = 0; i <= lstEmailAccounts.Items.Count - 1; i++) {
				itm = lstEmailAccounts.Items(i);
				if (itm.defaultAccount) {
					lstEmailAccounts.SelectedIndex = i;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			TimerCallback tcb = new TimerCallback(retrieveInbox);
			//mailObj.tcb = tcb
			mailObj.inbox = GlobalShared.inbox;
			//mailObj.grd = grdInbox
			getEmail = new System.Threading.Timer(tcb, mailObj, GlobalShared.EMAIL_RETRIEVAL_INTERVAL, GlobalShared.EMAIL_RETRIEVAL_INTERVAL);
			GlobalShared.Log.Log(LogClass.logType.Config, "Main form started and ready");

		}


		private void ctxTenantsAddNotes_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstTenants;
			TreeViewItem node = lst.SelectedItem;
			if ((node == null))
				return;
			long key = 0;
			if (Information.IsNumeric(node.Tag)) {
				try {
					key = Convert.ToInt64(node.Tag);
				} catch {
					return;
				}
				if (key > 0) {
					Tenants tenant = Tenants.load(key);
					if ((tenant != null)) {
						frmIssue frm = new frmIssue();
						frm.tenant = tenant;
						frm.ShowDialog();
						tenant = null;
						Utils.getAllNotes(lstIssues, shwBy, vAll);
					}
				}
			} else {
				if (node.Tag.ToString.StartsWith("B:")) {
					// This is a building node
					Building bld = Building.load(node.Tag.ToString.Substring(2));
					if ((bld != null)) {
						frmIssueBuilding frm = new frmIssueBuilding();
						frm.bld = bld;
						frm.ShowDialog();
						bld = null;
						Utils.getAllNotes(lstIssues, shwBy, vAll);
					}
				}
			}

		}

		private void ctxNotesEdit_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstIssues;
			TreeViewItem node = lst.SelectedItem;
			if ((node == null))
				return;
			long key = 0;
			try {
				key = Convert.ToInt64(node.Tag);
			} catch {
				return;
			}
			if (key > 0) {
				if (node.Header.ToString.StartsWith("BLD:")) {
					frmIssueBuilding frm = new frmIssueBuilding();
					frm.issueId = key;
					frm.ShowDialog();
					frm.BringToFront();
				} else {
					frmIssue frm = new frmIssue();
					frm.issueId = key;
					frm.ShowDialog();
					frm.BringToFront();
				}
				Utils.getAllNotes(lstIssues, shwBy, vAll);
			}

		}


		private void ctxNotesShowBy_Click(object sender, System.EventArgs e)
		{
			MenuItem ctxItem = sender;
			MenuItem itm = default(MenuItem);
			ContextMenu ctx = ctxItem.Parent;
			int c = 0;
			foreach (object obj in ctx.Items) {
				if (obj is MenuItem) {
					itm = (MenuItem)obj;
					if (itm.IsCheckable) {
						if (itm.Equals(ctxItem)) {
							shwBy = c;
						} else {
							itm.IsChecked = false;
						}
						c += 1;
					}
				}
			}
			Utils.getAllNotes(lstIssues, shwBy, vAll);

		}


		private void lstNotes_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.MiddleButton == MouseButton.Right) {
				TreeViewItem itm = lstIssues.SelectedItem;
			}

		}


		private void ctxRecipientsEdit_Click(object sender, System.EventArgs e)
		{
			ListBox lst = lstRecipients;
			ListItem lstItem = lst.SelectedItem;
			long acctid = lstItem.ValueItem;
			if (acctid <= 0)
				return;
			frmAccount frm = new frmAccount();
			frm.acctId = acctid;
			//frm.MdiParent = Me
			frm.Show();

		}


		private void ctxRecipients_Opening(object sender, RoutedEventArgs e)
		{
			ListBox lst = lstRecipients;
			if (lstRecipients.SelectedIndex < 0) {
				ctxRecipientsEdit.IsEnabled = false;
				ctxRecipientsDelete.IsEnabled = false;
			}
		}


		private void ctxTenantsViewNotes_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstTenants;
			TreeViewItem node = lst.SelectedItem;
			if ((node == null))
				return;
			long key = 0;
			try {
				key = Convert.ToInt64(node.Tag);
			} catch {
				return;
			}
			if (key > 0) {
				Issues issue = default(Issues);
				Accounts acct = null;
				IssueTenants[] tens = null;
				tens = IssueTenants.loadByTenant(key);
				if ((tens != null) & tens.Length > 0) {
					frmIssuesList frm = new frmIssuesList();
					foreach (IssueTenants ten in tens) {
						issue = Issues.load(ten.getNoteId);
						if ((issue != null)) {
							Windows.Forms.ListViewItem item = new Windows.Forms.ListViewItem(node.Header.ToString);
							item.SubItems.Add(Strings.Format(issue.getTimestamp, "M/dd/yyyy"));
							acct = new Accounts();
							acct = Accounts.load(issue.getAccountId);
							item.SubItems.Add(acct.getName);
							item.SubItems.Add(issue.getSubject);
							item.SubItems.Add(issue.getNoteId);
							frm.lstNotes.Items.Add(item);
						}
					}
					frm.Show();
					frm.BringToFront();
				} else {
					Interaction.MsgBox("No Issues found for this tenant");
				}
			}

		}


		private void mnuDelivery_Click(object sender, System.EventArgs e)
		{
			frmMessageSchedules frm = new frmMessageSchedules();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();

		}


		private void OptionsToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			string pPath = GlobalShared.pmPath;
			frmOptions frm = new frmOptions();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();
			if (GlobalShared.pmPath != pPath) {
				WaitWindow frmW = new WaitWindow();
				frmW.Owner = this;
				frmW.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
				lstTenants.Items.Clear();
				Utils.getTenants(lstTenants);
				frmW.Close();
			}

		}


		private void viewAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkAllAccounts.IsChecked) {
				vAll = true;
			} else {
				vAll = false;
			}
			Utils.getAllNotes(lstIssues, shwBy, vAll);

		}


		private void lstMessages_Click(object sender, System.EventArgs e)
		{
			try {
				TreeView lst = lstMessages;
				TreeViewItem itm = lst.SelectedItem;
				frmEmailMessage frm = new frmEmailMessage();
				EmailMessage msg = parseEmailFile(itm.Tag);
				frm.Owner = this;
				frm.toAddr = msg.toAddr;
				frm.fromAddr = msg.fromAddr;
				frm.emailDate = msg.receiveDate;
				frm.Subject = msg.subject;
				frm.Message = itm.ToolTip;
				frm.Background = System.Windows.Media.Brushes.Aquamarine;
				frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
				frm.Show();
			} catch {
			}

		}


		private void ctxTenantsAddMessage_Click(object sender, System.EventArgs e)
		{
			try {
				TreeView lst = lstMessages;
				TreeViewItem itm = lst.SelectedItem;
				frmEmailMessage frm = new frmEmailMessage();
				//Dim msg As EmailMessage = parseEmailFile(itm.Tag)
				frm.Owner = this;
				//frm.toAddr = msg.toAddr
				//frm.fromAddr = msg.fromAddr
				//frm.emailDate = msg.receiveDate
				//frm.Subject = msg.subject
				//frm.Message = itm.ToolTip
				frm.Background = System.Windows.Media.Brushes.Aquamarine;
				frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
				frm.Show();
			} catch {
			}

		}



		private void ctxMessagesIssue_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstMessages;
			//Dim node As TreeNode = lst.SelectedNode
			//If IsNothing(node) Then Exit Sub
			emailBy = MailBy.Issue;
			Utils.getListOfMessages(lstMessages, vAll, emailBy);

		}


		private void ctxMessagesAccount_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstMessages;
			//Dim node As TreeNode = lst.SelectedNode
			//If IsNothing(node) Then Exit Sub
			emailBy = MailBy.Account;
			Utils.getListOfMessages(lstMessages, vAll, emailBy);

		}


		private void mnuCategories_Click(object sender, System.EventArgs e)
		{
			frmCategory frm = new frmCategory();
			frm.Show();

		}


		private void toolTenantCollapse_Click(object sender, System.EventArgs e)
		{
			foreach (TreeViewItem itm in lstTenants.Items) {
				itm.IsExpanded = false;
			}

		}


		private void ctxNoteDelete_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstIssues;
			TreeViewItem node = lst.SelectedItem;
			if ((node == null))
				return;
			long key = 0;
			try {
				key = Convert.ToInt64(node.Tag);
			} catch {
				return;
			}
			if (key > 0) {
				if (Interaction.MsgBox("Delete this issue <" + key + "><" + node.Header.ToString + ">?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
					Issues issue = new Issues();
					Notes[] note = null;
					long c = 0;
					long i = 0;
					note = Notes.loadByIssue(key);
					if ((note != null)) {
						foreach (Notes n in note) {
							try {
								n.delete(n.getnote_id);
								c += 1;
							} catch {
							}
						}
					}
					try {
						issue.delete(key);
						i = 1;
					} catch {
					}
					Interaction.MsgBox(i + " Issue deleted along with " + c + " notes.");
					Utils.getAllNotes(lstIssues, shwBy, vAll);
				}
			}

		}

		private void mnuRecipientLists_Click(object sender, System.EventArgs e)
		{
			frmRecipientMaster frm = new frmRecipientMaster();
			System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(frm);
			frm.WindowStartupLocation = WindowStartupLocation.Manual;
			frm.Top = this.Top;
			frm.Left = this.Left;
			frm.Show();
		}


		private void mnuMessageDeliverySMTP_Click(object sender, System.EventArgs e)
		{
			frmSmtpDelivery frm = new frmSmtpDelivery();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.Show();

		}


		private void mnuMessageService_Click(object sender, System.EventArgs e)
		{
			frmListener frm = new frmListener();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.Show();

		}


		private void ctxMessagesRefresh_Click(object sender, System.EventArgs e)
		{
			TreeView lst = lstMessages;
			Utils.getListOfMessages(lstMessages, vAll, emailBy);

		}


		private void lstIssues_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			TreeViewItem itm = (TreeViewItem)e.Source;
			if ((itm != null)) {
				itm.IsSelected = true;
			}
		}


		private void lstTenants_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			TreeViewItem itm = (TreeViewItem)e.Source;
			if ((itm != null)) {
				itm.IsSelected = true;
			}

		}


		private void mnuJobDashboard_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmJobDashboard frm = new frmJobDashboard();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.Show();

		}

		//*****************
		//* INBOX Methods *
		//*****************


		private void lstEmailAccounts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			currSmtpAcct = lstEmailAccounts.SelectedItem;

			ParameterizedThreadStart threadStart = new ParameterizedThreadStart(retrieveInbox);
			myThread = new Thread(retrieveInbox);
			myThread.Start(mailObj);

		}


		private void cmdInboxRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ParameterizedThreadStart threadStart = new ParameterizedThreadStart(retrieveInbox);
			myThread = new Thread(retrieveInbox);
			myThread.Start(mailObj);

		}


		private void updateGrid()
		{
			grdInbox.ItemsSource = null;
			GlobalShared.inbox.Sort(EmailMessage.sort);
			grdInbox.ItemsSource = GlobalShared.inbox;

		}


		private void serializeInbox(List<EmailMessage> list)
		{
			string filePath = System.Windows.Forms.Application.LocalUserAppDataPath;
			try {
				WinTRM.My.MyProject.Computer.FileSystem.CurrentDirectory = filePath;
			} catch (IO.FileNotFoundException ex) {
				try {
					WinTRM.My.MyProject.Computer.FileSystem.CreateDirectory(filePath);
				} catch (ArgumentException exc) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting creation of inbox serialization path <" + filePath + ">: " + ex.Message);
					return;
				} catch (IO.PathTooLongException exc) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting creation of inbox serialization path <" + filePath + ">: " + ex.Message);
					return;
				} catch (NotSupportedException exc) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting creation of inbox serialization path <" + filePath + ">: " + ex.Message);
					return;
				} catch (IO.IOException exc) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting creation of inbox serialization path <" + filePath + ">: " + ex.Message);
					return;
				} catch (UnauthorizedAccessException exc) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting creation of inbox serialization path <" + filePath + ">: " + ex.Message);
					return;
				}
			} catch (UnauthorizedAccessException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempted changing to inbox serialization path <" + filePath + ">: " + ex.Message);
				return;
			}
			using (System.IO.Stream str = System.IO.File.OpenWrite("Inbox.bin")) {
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				bf.Serialize(str, list);
			}

		}

		private List<EmailMessage> unserializeInbox(List<EmailMessage> list)
		{

			string filePath = System.Windows.Forms.Application.LocalUserAppDataPath;
			try {
				WinTRM.My.MyProject.Computer.FileSystem.CurrentDirectory = filePath;
			} catch (IO.FileNotFoundException ex) {
				return list;
			} catch (UnauthorizedAccessException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting change to inbox serialization path <" + filePath + ">: " + ex.Message);
				return list;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Attempting change to inbox serialization path <" + filePath + ">: " + ex.Message);
				return list;
			}
			if (WinTRM.My.MyProject.Computer.FileSystem.FileExists("Inbox.bin")) {
				using (System.IO.Stream str = System.IO.File.OpenRead("Inbox.bin")) {
					System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					list = bf.Deserialize(str);
				}
			}
			return list;

		}


		public void retrieveInbox(object obj)
		{
			GlobalShared.Log.Log(LogClass.logType.Config, "Retrieving inbox");
			try {
				if (inMailBox)
					return;
				inMailBox = true;
				getEmail.Change(Timeout.Infinite, Timeout.Infinite);
				mailObj mailobj = (mailObj)obj;
				try {
					mailobj.inbox.Clear();
				} catch (System.Exception ex) {
					GlobalShared.Log.Log(LogClass.logType.Warning, "Clearing inbox: " + ex.Message);
				}

				if ((currSmtpAcct != null)) {
					GlobalShared.Log.Log(LogClass.logType.Info, "Using smtp acct " + currSmtpAcct.description);
					Generic.SortedList<string, Chilkat.Email> messages = Utils.getSmtpMessages(currSmtpAcct);
					if ((messages != null)) {
						if (messages.Count > 0) {
							GlobalShared.Log.Log(LogClass.logType.Info, "About to process messages");
							foreach (KeyValuePair<string, Chilkat.Email> msg in messages) {
								mailobj.inbox.Add(new EmailMessage(msg.Key, msg.Value));
							}
						}
					} else {
						GlobalShared.Log.Log(LogClass.logType.Info, "No SMTP messages found");
					}
					grdInbox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateGrid));
				} else {
					GlobalShared.Log.Log(LogClass.logType.Warning, "No current SMTP account found.");
				}
				getEmail.Change(GlobalShared.EMAIL_RETRIEVAL_INTERVAL, GlobalShared.EMAIL_RETRIEVAL_INTERVAL);
			} catch (IO.FileNotFoundException ex) {
				GlobalShared.Log.Log(LogClass.logType.Debug, "File not found: " + ex.Message);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.Debug, "Error during retrieveInbox: " + ex.Message);
			} finally {
				inMailBox = false;
			}
		}
		//^^^^^^^^^^^^^^^^^
		//^ INBOX Methods ^
		//^^^^^^^^^^^^^^^^^


		private void mnuLog_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmLogViewer frm = new frmLogViewer();
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.Show();

		}


		private void optMessagesIssues_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			if (optMessagesIssues.IsChecked) {
				try {
					lstEmailAccounts.Visibility = Windows.Visibility.Collapsed;
					grdInbox.Visibility = Windows.Visibility.Collapsed;
					lstMessages.Visibility = Windows.Visibility.Visible;
					cmdInboxRefresh.Visibility = Windows.Visibility.Collapsed;
				} catch {
				}
			}

		}


		private void optMessagesInbox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			if (optMessagesInbox.IsChecked) {
				try {
					lstEmailAccounts.Visibility = Windows.Visibility.Visible;
					grdInbox.Visibility = Windows.Visibility.Visible;
					lstMessages.Visibility = Windows.Visibility.Collapsed;
					cmdInboxRefresh.Visibility = Windows.Visibility.Visible;
				} catch {
				}
			}

		}

		/// <summary>
		/// Handles Assignment of email to an existing issue
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>

		private void ctxInboxAssign_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EmailMessage itm = default(EmailMessage);
			try {
				itm = grdInbox.SelectedItem;
			} catch (System.Exception ex) {
				Interaction.MsgBox("Could not retrieve selected item: " + ex.Message);
				return;
			}
			if ((itm != null)) {
				itm.fetchMessage(currSmtpAcct);
				frmIssuesDashboard frm = new frmIssuesDashboard();
				frm.email = true;
				frm.ShowDialog();
				if (frm.issueId > 0) {
					if (string.IsNullOrEmpty(StringUtils.saveEmail(itm.emailMessage, frm.issueId, itm.UID).Trim)) {
						Interaction.MsgBox("Unable to save selected item.");
					} else {
						//grdInbox.Items.Remove(grdInbox.SelectedItem)
					}
				}
			} else {
				Interaction.MsgBox("Could not retrieve selected item");
			}

		}


		private void ctxInboxCreate_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EmailMessage itm = default(EmailMessage);
			try {
				itm = grdInbox.SelectedItem;
			} catch (System.Exception ex) {
				Interaction.MsgBox("Could not retrieve selected item: " + ex.Message);
				return;
			}
			if ((itm != null)) {
				frmTenantList frm = new frmTenantList();
				frm.newIssue = true;
				frm.ShowDialog();
				if ((frm.selectedTenants != null)) {
					if (frm.selectedTenants.Length > 0) {
						long tenantId = ((ListItem)frm.selectedTenants.GetValue(0)).ValueItem;
						frm.Dispose();
						if (tenantId > 0) {
							frmIssue frmI = new frmIssue();
							Tenants tenant = Tenants.load(tenantId);
							frmI.tenant = tenant;
							if ((tenant != null)) {
								frmI.ShowDialog();
								tenant = null;
								if (frmI.issueId > 0) {
									if (string.IsNullOrEmpty(StringUtils.saveEmail(itm.emailMessage, frmI.issueId, itm.UID).Trim)) {
										Interaction.MsgBox("Issue created but email could not be saved");
									} else {
										Interaction.MsgBox("Issue created and email saved");
									}
									Utils.getAllNotes(lstIssues, shwBy, vAll);
								} else {
									Interaction.MsgBox("Issue not created");
								}
							}
						} else {
							Interaction.MsgBox("Could not find tenant.  Issue not created");
						}
					}
				}
			}

		}


		private void ctcInboxDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EmailMessage itm = default(EmailMessage);
			try {
				itm = grdInbox.SelectedItem;
			} catch {
				return;
			}
			if (Interaction.MsgBox("Delete this message?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
				if (Utils.deleteMessage(currSmtpAcct, itm.emailMessage)) {
					Interaction.MsgBox("Message deleted");
					cmdInboxRefresh_Click(sender, e);
				} else {
					Interaction.MsgBox("Message could not be deleted");
				}
			}
		}


		private void ctxInboxOpen_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EmailMessage itm = default(EmailMessage);
			try {
				itm = grdInbox.SelectedItem;
			} catch {
				return;
			}
			itm.fetchMessage(currSmtpAcct);
			frmEmailMessage frm = new frmEmailMessage();
			frm.fromAddr = itm.fromAddr;
			frm.toAddr = itm.toAddr;
			frm.Bcc = itm.bcc;
			frm.emailDate = itm.receiveDate;
			frm.Subject = itm.subject;
			frm.Message = itm.message;
			frm.Show();

		}


		private void mnuEditBuildingLocations_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmBuildingLocations frm = new frmBuildingLocations();
			frm.Show();

		}


		private void mnuEditVendors_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmVendors frm = new frmVendors();
			frm.Show();

		}


		private void mnuToolsServices_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmServices frm = new frmServices();
			frm.Show();

		}


		private void mnuHelpHelp_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Help frm = new Help();
			frm.Show();

		}
		public frmMainWindow()
		{
			Loaded += frmMain_Load;
			Closed += frmMainWindow_Closed;
		}
	}
}


