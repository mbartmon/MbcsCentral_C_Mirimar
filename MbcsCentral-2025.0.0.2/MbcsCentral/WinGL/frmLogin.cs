using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	internal partial class frmLogin : System.Windows.Forms.Form
	{
		[DllImport("advapi32.dll", EntryPoint = "GetUserNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int GetUserName(string lpbuffer, ref int nSize);


		public bool OK;
		private void frmLogin_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			string sBuffer = null;
			int lSize = 0;

			sBuffer = Strings.Space(255);
			lSize = Strings.Len(sBuffer);
			GetUserName(sBuffer, ref lSize);
			if (lSize > 0) {
				txtUserName.Text = Strings.Left(sBuffer, lSize);
			} else {
				txtUserName.Text = Constants.vbNullString;
			}
			cmdOK_Click(cmdOK, new System.EventArgs());

		}
		private void cmdCancel_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			OK = false;
			this.Hide();
		}
		private void cmdOK_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			//ToDo: create test for correct password
			//check for correct password
			//If txtPassword.Text = "" Then
			OK = true;
			this.Hide();
			//Else
			//    MsgBox "Invalid Password, try again!", , "Login"
			//    txtPassword.SetFocus
			//    txtPassword.SelStart = 0
			//    txtPassword.SelLength = Len(txtPassword.Text)
			//End If
		}
	}
}
