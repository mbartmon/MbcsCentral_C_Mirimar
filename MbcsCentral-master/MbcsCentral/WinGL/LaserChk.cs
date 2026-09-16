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
	internal partial class LaserChk : System.Windows.Forms.Form
	{

		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Hide();

		}

		private void LaserChk_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2));
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2 - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2));
			//Me.Width = 5295: Me.Height = 2475
		}
	}
}
