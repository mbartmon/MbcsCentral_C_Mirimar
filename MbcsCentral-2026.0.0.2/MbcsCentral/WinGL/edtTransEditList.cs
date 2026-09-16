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
	internal partial class edtTransEditList : System.Windows.Forms.Form
	{
//UPGRADE_WARNING: Arrays in structure tblTransTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblTransTemp;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object i = null;

			switch (Index) {

				case 0:
					// SAVE
					tblTransTemp.Edit();
					WinGlobal.move_scr_tbl(ref tblTransTemp, ref this);
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					i = lstAccounts.ListIndex;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstAccounts.Row = i;
					lstAccounts.Col = 1;
					tblTransTemp.Fields["AccountNumber"].Value = lstAccounts.ColList;
					tblTransTemp.Update();
					break;

				case 1:
					// DELETE
					if (LogError.MsgBox_Renamed("Are you sure you want to delete this transaction?", MsgBoxStyle.YesNo, "Edit Transaction List") == MsgBoxResult.Yes) {
						tblTransTemp.Delete();
					} else {
						return;
					}
					break;

				case 2:
					// CANCEL
					break;

			}

			this.Close();

		}
//UPGRADE_WARNING: Form event edtTransEditList.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void edtTransEditList_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			string x = null;
			int i = 0;
			int r = 0;

			x = this.Tag;

			Label2.Text = x;
			i = Conversion.Val(x);
			Label2.Refresh();
			tblTransTemp = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM TransactionTemp WHERE IndexNumber = " + i);

			WinGlobal.move_tbl_scr(ref tblTransTemp, ref this);
			r = WinglGlobal.getAccounts(lstAccounts, 0);
			lstAccounts.Col = 1;
			for (i = 0; i <= r - 1; i++) {
				lstAccounts.Row = i;
				if (Conversion.Val(lstAccounts.ColList) == tblTransTemp.Fields["AccountNumber"].Value) {
					lstAccounts.ListIndex = i;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
		}
	}
}
