using System.Windows.Forms;
namespace WinTRM
{

	public partial class frmAccount
	{

		public long acctId;
		Accounts acct;
		private void OK_Button_Click(System.Object sender, System.EventArgs e)
		{
			if (!GlobalShared.user.hasManagerPermission()) {
				Interaction.MsgBox("You do not have permission to perform this function");
				return;
			}
			if (!txtPassword.Text.Equals(txtConfirm.Text)) {
				Interaction.MsgBox("Passwords do not match.");
				return;
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			if (acctId == -1) {
				acct = new Accounts();
			}
			acct.setEmail(txtEmail.Text);
			acct.setLogin(txtLogin.Text);
			acct.setName(txtName.Text);
			acct.setPassword(txtPassword.Text);
			acct.setSms(txtSMS.Text);
			acct.setPermissions(getPermission());
			acct.setAccountType((btnInOffice.Checked ? 0 : 1));
			acct.save();
			this.Close();
		}
		private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
		public void setAccountId(long acctId)
		{
			this.acctId = acctId;
		}
		public Accounts getAccount()
		{
			return this.acct;
		}

		private void frmAccount_Shown(object sender, System.EventArgs e)
		{
			if (acctId == -1)
				return;
			acct = Accounts.load(this.acctId);
			if ((acct == null)) {
				Interaction.MsgBox("Account not found");
				this.Close();
			}
			txtAccountID.Text = acctId;
			txtLogin.Text = acct.getLogin();
			txtName.Text = acct.getName();
			txtPassword.Text = acct.getPassword();
			txtConfirm.Text = acct.getPassword();
			txtEmail.Text = acct.getEmail();
			txtSMS.Text = acct.getSms();
			switch (acct.getPermissions()) {
				case 0:
					btnView.Checked = true;
					break;
				case 1:
					btnUser.Checked = true;
					break;
				case 2:
					btnAccount.Checked = true;
					break;
				case 3:
					btnAdmin.Checked = true;
					break;
				default:
					btnView.Checked = true;
					break;
			}
			if (acct.getAccountType() == 0) {
				btnInOffice.Checked = true;
			} else {
				btnOutOfOffice.Checked = true;
			}
			if (!GlobalShared.user.hasManagerPermission()) {
				OK_Button.Enabled = false;
			}

		}

		private int getPermission()
		{
			if (btnAdmin.Checked) {
				return 3;
			} else if (btnAccount.Checked) {
				return 2;
			} else if (btnUser.Checked) {
				return 1;
			} else {
				return 0;
			}
		}
		public frmAccount()
		{
			Shown += frmAccount_Shown;
		}
	}
}
