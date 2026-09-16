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
	internal partial class CompanyDetail : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.1  2009/08/21 12:36:12  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:06  mike
//* Copied from wingl folder
//*
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			 // ERROR: Not supported in C#: OnErrorStatement

			switch (Index) {

				case 0:
					// SAVE
					if (WinglGlobal.AddFlag) {
						WinglGlobal.tblCompany.AddNew();
					} else {
						WinglGlobal.tblCompany.Edit();
					}
					WinGlobal.move_scr_tbl(ref WinglGlobal.tblCompany, ref this);
					WinglGlobal.tblCompany.Update();
					break;
				case 1:
					// EXIT
					break;

			}

			this.Close();
			goto Cmd_Click_exit;
			Cmd_Click_error:

			if (Err().Number == 3022) {
				Interaction.MsgBox("Duplicate Company Number... Re-enter.", , "Company Detail");
			} else {
				LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Company Detail");
			}
			 // ERROR: Not supported in C#: ResumeStatement

			Cmd_Click_exit:


		}
		private void CompanyDetail_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4695);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7080);
			//UPGRADE_NOTE: Enabled was upgraded to CtlEnabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtFields[0].CtlEnabled = true;
			if (!WinglGlobal.AddFlag) {
				WinGlobal.move_tbl_scr(ref WinglGlobal.tblCompany, ref this);
				//UPGRADE_NOTE: Enabled was upgraded to CtlEnabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtFields[0].CtlEnabled = false;
			}
			goto Form_Load_exit;
			Form_Load_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Company Detail");
			 // ERROR: Not supported in C#: ResumeStatement

			Form_Load_exit:


		}
		private void txtFields_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = txtFields.GetIndex(eventSender);

			string x = null;

			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			x = txtFields[Index].CtlText;
			if (txtFields[Index].Tag == "FiscalYearEnd") {
				if (!Information.IsNumeric(x) | Conversion.Val(x) < 0 | Conversion.Val(x) > 12) {
					Interaction.MsgBox("Invalid Fiscal Year End value.  Must be between 0 and 12");
					txtFields[Index].Focus();
				}
			}

			if (txtFields[Index].Tag == "CurrentFiscalYear") {
				if (!Information.IsNumeric(x) | (Conversion.Val(x) < 0 | Conversion.Val(x) > 2999)) {
					Interaction.MsgBox("Invalid Current Fiscal Year");
					txtFields[Index].Focus();
				}
			}

		}
	}
}
