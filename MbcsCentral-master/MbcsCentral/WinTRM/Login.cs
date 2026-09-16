namespace WinTRM
{
	public partial class Login
	{

		// TODO: Insert code to perform custom authentication using the provided username and password 
		// (See http://go.microsoft.com/fwlink/?LinkId=35339).  
		// The custom principal can then be attached to the current thread's principal as follows: 
		//     My.User.CurrentPrincipal = CustomPrincipal
		// where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
		// Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
		// such as the username, display name, etc.


		private void OK_Click(System.Object sender, System.EventArgs e)
		{
			GlobalShared.user = Accounts.load(txtLogin.Text);
			if ((GlobalShared.user == null)) {
				Interaction.MsgBox("Invalid user name");
				txtLogin.Focus();
				return;
			}
			if (!GlobalShared.user.getPassword().Equals(txtPassword.Text)) {
				Interaction.MsgBox("Invalid password");
				txtPassword.Focus();
				return;
			}
			if (!GlobalShared.user.hasViewPermission()) {
				Interaction.MsgBox("You do not have permission to use this application");
				this.Close();
				System.Environment.Exit(0);
			}
			this.Close();
		}

		private void Cancel_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			System.Environment.Exit(0);
		}

	}
}
