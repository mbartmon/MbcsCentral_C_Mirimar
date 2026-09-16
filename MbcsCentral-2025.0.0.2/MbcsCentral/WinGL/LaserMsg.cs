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
	internal partial class LaserMsg : System.Windows.Forms.Form
	{
		private void LaserMsg_Load(System.Object eventSender, System.EventArgs eventArgs)
		{


			this.Left = 0;
			this.RightToLeft = 0;
			this.Width = (int)Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(5295);
			this.Height = (int)Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(2475);
		}
	}
}
