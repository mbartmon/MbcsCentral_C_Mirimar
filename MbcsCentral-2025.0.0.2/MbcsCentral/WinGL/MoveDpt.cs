using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	internal partial class MoveDpt : System.Windows.Forms.Form
	{
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Command1.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// SAVE
					if (lstCompanies.SelectedIndex == -1) {
						LogError.MsgBox_Renamed("You must select and Company.");
						goto Command1_Click_exit;
					}
					WinglGlobal.tblDepartment.Edit();
					WinglGlobal.tblDepartment.Fields["Company Number"].Value = Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3));
					WinglGlobal.tblDepartment.Update();
					break;

				case 1:
					// CANCEL
					break;

			}
			WinglGlobal.tblDepartment.Close();
			this.Close();
			Command1_Click_exit:


		}
		private void MoveDpt_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(3840);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7110);
			this.Top = My.MyProject.Forms.frmMain.Top;
			this.Left = My.MyProject.Forms.frmMain.Left;
			lblDepartment.Text = WinglGlobal.tblDepartment.Fields["Department Number"].Value + ":" + WinglGlobal.tblDepartment.Fields["Name"].Value;
			WinglGlobal.GetCompanies(ref this);

		}
	}
}
