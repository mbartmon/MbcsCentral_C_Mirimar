public partial class LoginForm
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
		
		MbcsCentral.GlobalShared.userName = this.UsernameTextBox.Text;
		MbcsCentral.GlobalShared.password = this.PasswordTextBox.Text;
		
		this.Close();
	}
	
	private void Cancel_Click(System.Object sender, System.EventArgs e)
	{
		MbcsCentral.GlobalShared.userName = null;
		MbcsCentral.GlobalShared.password = null;
		this.Close();
	}
	
}