using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
namespace WinGL
{
	internal partial class frmSplash : System.Windows.Forms.Form
	{
		private void frmSplash_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			lblVersion.Text = "Version " + Version + WinglGlobal.SUBVER;
			lblProductName.Text = WinGL.My.MyProject.Application.Info.Title;
			lblPlatform.Text = "Windows 95/98/ME/NT/2000";
		}
	}
}
