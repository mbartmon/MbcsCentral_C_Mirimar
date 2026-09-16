namespace WinTRM
{
	public partial class frmAccounts
	{
		bool loading = true;
		private void frmAccounts_Load(object sender, System.EventArgs e)
		{
			loadAccounts();
		}

		private void loadAccounts()
		{
			loading = true;
			Accounts[] accts = Accounts.load();

			if ((accts != null)) {
				ArrayList listitems = new ArrayList();
				Accounts acct = null;
				foreach (Accounts acct_loopVariable in accts) {
					acct = acct_loopVariable;
					listitems.Add(new ListItem(acct.getName() + " <" + acct.getLogin() + ">", acct.getAccountId()));
					acct = null;
				}
				lstAccounts.DataSource = listitems;
				lstAccounts.DisplayMember = "DisplayItem";
				lstAccounts.ValueMember = "ValueItem";
			}
			loading = false;

		}


		private void lstAccounts_DoubleClick(System.Object sender, System.EventArgs e)
		{
			if (loading)
				return;

			System.Windows.Forms.ListBox lst = lstAccounts;

			if (lst.SelectedIndex >= 0) {
				ListItem item = lstAccounts.SelectedItem;
				frmAccount frm = new frmAccount();
				frm.setAccountId(item.ValueItem);
				frm.ShowDialog();
				loadAccounts();
			}
		}

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			if (!GlobalShared.user.hasManagerPermission()) {
				Interaction.MsgBox("You do not have permission to perform this function");
				return;
			}
			frmAccount frm = new frmAccount();
			frm.setAccountId(-1);
			frm.ShowDialog();
			loadAccounts();
		}

		private void cmdExit_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}
		public frmAccounts()
		{
			Load += frmAccounts_Load;
		}
	}
}
