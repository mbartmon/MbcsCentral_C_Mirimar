using System.Data;
using System.Windows.Controls;
namespace WinTRM
{

	public class frmContents
	{
		contents[] conts = null;
		private long accountId = 0;

		private DataSet ds;

		private void fillGrid()
		{
			ds = new DataSet();
			try {
				DataTable dt = new DataTable();

				dt.Columns.Add("ID", System.Type.GetType("System.Int32"));
				dt.Columns.Add("Account", System.Type.GetType("System.String"));
				dt.Columns.Add("Description", System.Type.GetType("System.String"));
				ds.Tables.Add(dt);
			} catch (System.Exception ex) {
			}
			Accounts acct = null;
			foreach (contents c in conts) {
				DataRow dr = ds.Tables(0).NewRow;
				dr.Item(0) = c.getcontent_id();
				acct = Accounts.load(c.getaccount_id());
				dr.Item(1) = acct.getName();
				dr.Item(2) = c.getDescription();
				acct = null;
				ds.Tables(0).Rows.Add(dr);
			}
			lstContents.DataContext = ds.Tables(0).DefaultView;

		}
		public void setAccountId(long acctId)
		{
			accountId = acctId;
		}


		private void chkAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			CheckBox chk = (CheckBox)sender;

			if (chk.IsChecked) {
				conts = contents.load();
			} else {
				conts = contents.loadByAccount(accountId);
			}
			fillGrid();

		}


		private void lstContents_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			System.Windows.Controls.ListView lst = (System.Windows.Controls.ListView)sender;

			DataRowView item = lst.SelectedItem;

			editContent(item);

		}


		private void editContent(DataRowView item)
		{
			using (frmContent frm = new frmContent()) {
				frm.setContentId(item.Row(0));
				frm.ShowDialog();
			}

		}


		private void frmContents_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!GlobalShared.user.hasAdminPermission()) {
				chkAll.Visibility = Windows.Visibility.Hidden;
			}

			if (accountId == 0) {
				Interaction.MsgBox("No account passed.");
				this.Close();
			}

			conts = contents.loadByAccount(accountId);
			fillGrid();

		}


		private void cmdEdit_Click(object sender, System.Windows.RoutedEventArgs e)
		{

			if ((lstContents.SelectedItem != null)) {
				DataRowView item = lstContents.SelectedItem;

				editContent(item);

			}


		}


		private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			using (frmContent frm = new frmContent()) {
				frm.setContentId(0);
				frm.ShowDialog();
			}
			try {
				conts = null;
				conts = contents.loadByAccount(accountId);

			} catch (System.Exception ex) {
			}
			fillGrid();

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}
		public frmContents()
		{
			Loaded += frmContents_Loaded;
		}
	}
}
