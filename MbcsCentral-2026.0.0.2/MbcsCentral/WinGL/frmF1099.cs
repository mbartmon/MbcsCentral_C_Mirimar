using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using Microsoft.VisualBasic.PowerPacks;
namespace WinGL
{
	internal partial class frmF1099 : System.Windows.Forms.Form
	{
		public clsF1099 clas;
		short wide;
		short narrow;
		private void AddVendor_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object vnum = null;

			if (lstVendors.ListIndex >= 0) {
				lstVendors.Col = 0;
				WinglGlobal.vName = lstVendors.ColText;
				lstVendors.Col = 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object vnum. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vnum = lstVendors.ColText;
				//UPGRADE_WARNING: Couldn't resolve default property of object vnum. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstVendPrint.AddItem(WinglGlobal.vName + Strings.Chr(9) + vnum);
			}

		}
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Command1.GetIndex(eventSender);
			object OneFlag = null;
			object vnum = null;
			object i = null;

			switch (Index) {

				case 0:
					//PRINT
					//UPGRADE_WARNING: Couldn't resolve default property of object dtDate().Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.StartDate = dtDate[0].Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtDate().Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.EndDate = dtDate[1].Value;
					clas.Detail = chkDetail.CheckState;
					clas.Order = (optOrder[0].Checked ? 0 : 1);
					clas.Address = chkAddress.CheckState;
					if (clas.Vendors.Count() > 0) {
						for (i = 0; i <= clas.Vendors.Count() - 1; i++) {
							clas.Vendors.Remove(i);
						}
					}
					if (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Width) == wide) {
						if (lstVendPrint.ListCount > 0) {
							for (i = 0; i <= lstVendPrint.ListCount - 1; i++) {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								lstVendPrint.ListIndex = i;
								lstVendPrint.Col = 0;
								WinglGlobal.vName = lstVendPrint.ColText;
								lstVendPrint.Col = 1;
								//UPGRADE_WARNING: Couldn't resolve default property of object vnum. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								vnum = lstVendPrint.ColText;
								//UPGRADE_WARNING: Couldn't resolve default property of object vnum. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								clas.Vendors.Add(Strings.Trim(WinglGlobal.vName) + Strings.Chr(9) + vnum);
							}
						} else {
							Interaction.MsgBox("There are no vendors in the print list.");
							goto Command1_Click_exit;
						}
					}
					break;

				case 1:
					//VENDORS
					if (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Width) == narrow) {
						Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(wide);
						//UPGRADE_WARNING: Couldn't resolve default property of object OneFlag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						OneFlag = true;
						Microsoft.VisualBasic.Compatibility.VB6.Support.SetDefault(Command1[0], false);
						Microsoft.VisualBasic.Compatibility.VB6.Support.SetDefault(AddVendor, true);
						Command1[1].Text = "<< VENDORS";
						lstVendors.Focus();
					} else {
						Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(narrow);
						//UPGRADE_WARNING: Couldn't resolve default property of object OneFlag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						OneFlag = false;
						Microsoft.VisualBasic.Compatibility.VB6.Support.SetDefault(Command1[0], true);
						Microsoft.VisualBasic.Compatibility.VB6.Support.SetDefault(AddVendor, false);
						Command1[1].Text = "VENDORS >>";
						dtDate[0].Focus();
					}
					goto Command1_Click_exit;
					break;

				case 2:
					//EXIT
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();
			Command1_Click_exit:


		}
		private void frmF1099_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			wide = 9090;
			narrow = 4120;
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(narrow);
			dtDate[0].Value = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), 1, 1);
			dtDate[1].Value = DateAndTime.Today;

			WinglGlobal.GetVendors(this);

			WinglGlobal.RecallForm(ref this);

		}
		private void frmF1099_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}
		private void lstVendors_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			//    Call AddVendor_Click

		}
		private void lstVendors_DblClick(System.Object eventSender, System.EventArgs eventArgs)
		{

			AddVendor_Click(AddVendor, new System.EventArgs());

		}
		private void Remove_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			if (lstVendPrint.ListIndex == -1) {
				Interaction.Beep();
				Interaction.MsgBox("You must select a Vendor from the list.");
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				i = lstVendPrint.ListIndex;
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstVendPrint.RemoveItem(i);
			}

		}
	}
}
