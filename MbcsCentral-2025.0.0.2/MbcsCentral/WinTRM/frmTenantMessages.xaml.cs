using System.ComponentModel;
namespace WinTRM
{

	public class frmTenantMessages
	{

		//Public master As frmIssue = Nothing
		public System.Windows.Forms.Form master = null;
		int formType;
		public enum formEnum : int
		{
			tenant,
			building
		}

		EmailMessage[] incoming;

		EmailMessage[] outgoing;
		private void frmTenantMessages_Activated(object sender, System.EventArgs e)
		{
			this.Top = master.Top;
			this.Left = master.Left + master.Width;
		}


		private void frmTenantMessages_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			long issueId = 0;

			if (master is frmIssue) {
				issueId = ((frmIssue)master).issueId;
			} else {
				issueId = ((frmIssueBuilding)master).issueId;
			}
			try {
				incoming = getEmailMessages(true, issueId);
				grdIncoming.ItemsSource = incoming;
				grdIncoming.Items.SortDescriptions.Add(new SortDescription("receiveDate", ListSortDirection.Descending));
			} catch (System.Exception ex) {
			}
			try {
				outgoing = getEmailMessages(false, issueId);
				grdOutgoing.DataContext = outgoing;
				grdOutgoing.Items.SortDescriptions.Add(new SortDescription("receiveDate", ListSortDirection.Descending));

			} catch (System.Exception ex) {
			}
		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Forms.Button cmdButton = default(System.Windows.Forms.Button);
			if (master is frmIssue) {
				cmdButton = ((frmIssue)master).cmdMessages;
			} else {
				cmdButton = ((frmIssueBuilding)master).cmdMessages;
			}
			try {
				//IIf(formType = 0, CType(master, frmIssue).cmdMessages.Image = My.Resources.ARW03RT, CType(master, frmIssue).cmdMessages.Image = My.Resources.ARW03RT)
				cmdButton.Image = WinTRM.My.Resources.ARW03RT;
			} catch {
			}
			this.Visibility = Windows.Visibility.Collapsed;

		}


		private void grdOutgoing_Click(object sender, MouseButtonEventArgs e)
		{
			DependencyObject dep = (DependencyObject)e.OriginalSource;

			ListView obj = (ListView)sender;

			while ((dep != null) & !(dep) is ListViewItem) {
				dep = VisualTreeHelper.GetParent(dep);
			}

			if ((dep == null)) {
				return;
			}

			EmailMessage itm = (EmailMessage)obj.ItemContainerGenerator.ItemFromContainer(dep);
			itm = parseEmailFile(itm.filename, true);
			if ((itm != null)) {
				frmEmailMessage frm = new frmEmailMessage();
				frm.Background = obj.Background;
				frm.Top = this.Top;
				frm.Left = this.Left;
				frm.fromAddr = itm.fromAddr;
				frm.toAddr = itm.toAddr;
				frm.Bcc = itm.bcc;
				frm.emailDate = itm.receiveDate;
				frm.Subject = itm.subject;
				frm.Message = itm.message;
				frm.Show();
			} else {
				Interaction.MsgBox("Could not find email message file.");
			}
		}
		public frmTenantMessages()
		{
			Loaded += frmTenantMessages_Loaded;
			Activated += frmTenantMessages_Activated;
		}
	}
}
