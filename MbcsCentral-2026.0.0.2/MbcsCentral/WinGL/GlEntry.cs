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
	internal partial class GLentry : System.Windows.Forms.Form
	{
		private decimal total;
		private bool postflag;
		private short ai;
		private object xrpt;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object i = null;

			bool entered = false;

			switch (Index) {

				case 0:
					// SAVE
					entered = false;
					for (i = 1; i <= grdEntries.MaxRows; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						grdEntries.Row = i;
						grdEntries.Col = 4;
						if (Conversion.Val(grdEntries.Value) != 0) {
							entered = true;
							break; // TODO: might not be correct. Was : Exit For
						}
					}

					if (!entered) {
						Interaction.MsgBox("No entries to save.", , "Manual Journal Entries");
						return;
					}
					if (lstCompanies.ListIndex == -1) {
						Interaction.MsgBox("No company selected.", , "Manual Journal Entries");
						return;
					}
					if (total != 0) {
						switch (Interaction.MsgBox("Entries do not balance.  Save anyway?", MsgBoxStyle.YesNoCancel, "Manual Journal Entries")) {
							case MsgBoxResult.Cancel:
								return;

								break;
							case MsgBoxResult.No:
								clearEntries();
								return;

								break;
						}
					}
					saveEntries();
					clearEntries();
					break;

				case 1:
					// CLEAR
					clearEntries();
					break;

				case 2:
					// EXIT
					this.Close();
					if (postflag) {
						xrpt = new clsTransEditList();
						//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						xrpt = null;
					}
					break;

			}

		}
		public void saveEntries()
		{

			//UPGRADE_WARNING: Arrays in structure tblTranTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			DAO.Recordset tblTranTemp = null;
			int s = 0;

			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			if (Conversion.Val(txtCheckNumber.CtlText) == 0 & string.IsNullOrEmpty(Strings.Trim(txtSource.CtlText))) {
				 // ERROR: Not supported in C#: OnErrorStatement

				WinglGlobal.tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
				WinglGlobal.tblControl.Edit();
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				s = (Information.IsDBNull(WinglGlobal.tblControl.Fields["GLSourceNumber"].Value) ? 0 : WinglGlobal.tblControl.Fields["GLSourceNumber"].Value);
				s = s + 1;
				WinglGlobal.tblControl.Fields["GLSourceNumber"].Value = s;
				WinglGlobal.tblControl.Update();
				WinglGlobal.tblControl.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblControl.LastModified);
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtSource.CtlText = "J-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(s, "000000");
				WinglGlobal.tblControl.Close();
			}
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			if (string.IsNullOrEmpty(Strings.Trim(txtSource.CtlText)))
				txtSource.CtlText = "*";

			 // ERROR: Not supported in C#: OnErrorStatement

			tblTranTemp = WinglGlobal.GLdb.OpenRecordset("TransactionTemp");

			lstCompanies.Col = 1;
			lstCompanies.Row = lstCompanies.ListIndex;
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				grdEntries.Row = i;
				grdEntries.Col = 4;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value)) {
					if (Conversion.Val(grdEntries.Value) != 0) {
						grdEntries.Col = 1;
						//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(Strings.Trim(grdEntries.Value))) {
							tblTranTemp.AddNew();
							tblTranTemp.Fields["CompanyNumber"].Value = lstCompanies.ColList;
							grdEntries.Col = 2;
							//UPGRADE_WARNING: Couldn't resolve default property of object SearchList(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblTranTemp.Fields["DepartmentNumber"].Value = WinglGlobal.SearchList(ref lstDepartments, ref Strings.Left(grdEntries.Text, 50), ref 2, ref 1);
							grdEntries.Col = 1;
							//UPGRADE_WARNING: Couldn't resolve default property of object SearchList(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblTranTemp.Fields["AccountNumber"].Value = WinglGlobal.SearchList(ref lstAccounts, ref Strings.Left(grdEntries.Text, 50), ref 2, ref 1);
							//UPGRADE_WARNING: Couldn't resolve default property of object dtDate.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblTranTemp.Fields["Date"].Value = dtDate.Value;
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							tblTranTemp.Fields["CheckNumber"].Value = Conversion.Val(txtCheckNumber.CtlText);
							lstJournals.Row = lstJournals.ListIndex;
							tblTranTemp.Fields["Journal"].Value = WinGlobal.lpad(ref (lstJournals.ColList), ref 20);
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							tblTranTemp.Fields["Source"].Value = txtSource.CtlText;
							grdEntries.Col = 3;
							tblTranTemp.Fields["Description"].Value = grdEntries.Value;
							grdEntries.Col = 4;
							tblTranTemp.Fields["Amount"].Value = Conversion.Val(grdEntries.Value);
							tblTranTemp.Update();
						}
					}
				}
			}

			tblTranTemp.Close();

			postflag = true;

		}
//UPGRADE_WARNING: Form event GLentry.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void GLentry_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{

			grdEntries.Col = 1;
			grdEntries.Row = 1;
			grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;
			lstCompanies.Focus();

		}
		private void GLentry_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object r = null;

			this.Top = 0;
			this.Left = 0;
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6000);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(10800);
			WinglGlobal.GetJrnBatches(ref this);
			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.GetJournals(ref this);
			WinglGlobal.getAccounts(lstAccounts, 0);
			grdEntries.Col = 1;
			for (r = 1; r <= grdEntries.MaxRows; r++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdEntries.Row = r;
				grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;
				grdEntries.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
				grdEntries.TypeComboBoxhWnd = lstAccounts.hWnd;
				grdEntries.TypeComboBoxWidth = 300;
			}
			lstAccounts.Visible = false;
			lstDepartments.Visible = false;

			dtDate.Value = DateAndTime.Today;

		}
//UPGRADE_WARNING: Event GLentry.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void GLentry_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			for (i = 0; i <= Cmd.Count() - 1; i++) {
				Cmd[i].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Cmd[i].Height) - 500);
			}

			grdEntries.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 2325);
			txtTotal.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdEntries.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdEntries.Height) + 50);
			Label5.Top = Cmd[0].Top;

		}
//******************************************************************************
		private void grdEntries_Change(System.Object eventSender, AxFPSpread._DSpreadEvents_ChangeEvent eventArgs)
		{
			//******************************************************************************

			decimal a = default(decimal);
			short i = 0;

			//If Col <> 4 Then Exit Sub
			total = 0;
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				grdEntries.Row = i;
				grdEntries.Col = 4;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(grdEntries.Value)) {
					total = total + Convert.ToDouble(grdEntries.Value);
				}
			}
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtTotal.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#########0.00"), ref 13);
			//grdEntries.SelectCell 0, grdEntries.SelStartRow + 1, False
			//grdEntries.Refresh

		}
		private void grdEntries_KeyDownEvent(System.Object eventSender, AxFPSpread._DSpreadEvents_KeyDownEvent eventArgs)
		{

			if (eventArgs.keyCode == 46) {
				if (LogError.MsgBox_Renamed("DELETE this transaction line?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
					grdEntries.DeleteRows(grdEntries.ActiveRow, 1);
				}
			}

		}
//******************************************************************************
		private void grdEntries_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			grdEntries.Refresh();
			total = 0;
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdEntries.Row = i;
				grdEntries.Col = 4;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(grdEntries.Value)) {
					total = total + Convert.ToDouble(grdEntries.Value);
				}
			}
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtTotal.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#########0.00"), ref 13);

		}
		private void grdEntries_LeaveCell(System.Object eventSender, AxFPSpread._DSpreadEvents_LeaveCellEvent eventArgs)
		{
			object x = null;

			if (eventArgs.newRow == -1 | eventArgs.newCol == -1)
				return;
			//If eventArgs.NewRow > Row + 1 Or eventArgs.NewRow < Row Then Exit Sub
			if (eventArgs.newRow > 1) {
				grdEntries.Row = eventArgs.newRow;
				grdEntries.Col = eventArgs.newCol;
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = grdEntries.Value;
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(x) | string.IsNullOrEmpty(Strings.Trim(x)) | x == -1) {
					//grdEntries.Row = Row - 1
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = grdEntries.Text;
					grdEntries.Row = eventArgs.newRow;
					switch (eventArgs.newCol) {
						case 1:
							// Account
							lstAccounts.Action = LpLib.enumAction.ActionClearSearchBuffer;
							break;
						case 2:
							// Department
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstDepartments.SearchText = Strings.Left(x, 50);
							lstDepartments.ColumnSearch = 0;
							lstDepartments.Action = LpLib.enumAction.ActionSearch;
							grdEntries.Value = Convert.ToString(lstDepartments.SearchIndex);
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							grdEntries.SetText(eventArgs.newCol, eventArgs.newRow, Strings.Left(x, 50));
							lstDepartments.Action = LpLib.enumAction.ActionClearSearchBuffer;
							break;
						case 3:
							// Description
							if (string.IsNullOrEmpty(Strings.Trim(grdEntries.Text))) {
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								grdEntries.Value = x;
							}
							break;
						case 4:
							// Amount
							break;
					}
					//grdEntries.Refresh
				}
			} else {
				 // ERROR: Not supported in C#: OnErrorStatement

				Err().Clear();
				lstAccounts.Action = LpLib.enumAction.ActionClearSearchBuffer;
				if (Err().Number == 0) {
					grdEntries.Col = eventArgs.newCol;
					if (eventArgs.newCol == 2) {
						if (Convert.ToDouble(grdEntries.Value) < 0) {
							grdEntries.Value = Convert.ToString(0);
							lstDepartments.Col = 0;
							lstDepartments.Row = 0;
							grdEntries.SetText(eventArgs.newCol, eventArgs.newRow, lstDepartments.ColList);
						}
					}
				}
			}

		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object r = null;
			object i = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstCompanies.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (i < 0)
				return;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstCompanies.Row = i;
			lstCompanies.Col = 1;

			// Department List
			WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
			grdEntries.Col = 2;
			for (r = 0; r <= grdEntries.MaxRows - 1; r++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdEntries.Row = r;
				grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;
				grdEntries.TypeComboBoxhWnd = lstDepartments.hWnd;
			}
			lstDepartments.ListIndex = 0;
			grdEntries.Col = 1;
			grdEntries.Row = 1;
			grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;

		}
		public void clearEntries()
		{
			object i = null;

			lstCompanies.ListIndex = -1;
			lstCompanies.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstJournals.ListIndex = -1;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtCheckNumber.CtlText = "";
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtSource.CtlText = "";
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdEntries.Row = i;
				grdEntries.Col = 1;
				grdEntries.Value = "";
				grdEntries.Col = 2;
				grdEntries.Value = "";
				grdEntries.Col = 3;
				grdEntries.Value = "";
				grdEntries.Col = 4;
				grdEntries.Value = "";
			}
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtTotal.CtlText = "";
			grdEntries.ClearRange(-1, -1, -1, -1, true);
			//lstBatch.ListIndex = -1
			lstCompanies.Focus();

		}
		private void lstBatch_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			int idx = 0;

			idx = lstBatch.ListIndex;
			if (idx < 0)
				return;
			grdEntries.ReDraw = false;
			clearEntries();
			lstBatch.Row = idx;
			lstBatch.Col = 1;
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM BatchJrnHeader WHERE Key = " + lstBatch.ColList);
			if (tbl.Fields["Company"].Value >= 0) {
				lstCompanies.ColumnSearch = 1;
				lstCompanies.SearchText = tbl.Fields["Company"].Value;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
			} else {
				lstCompanies.ListIndex = -1;
			}
			lstJournals.SearchIgnoreCase = true;
			lstJournals.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lstJournals.SearchText = (Information.IsDBNull(tbl.Fields["Journal"].Value) ? "" : tbl.Fields["Journal"].Value);
			lstJournals.Action = LpLib.enumAction.ActionSearch;
			lstJournals.ListIndex = lstJournals.SearchIndex;
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			txtSource.CtlText = (Information.IsDBNull(tbl.Fields["Source"].Value) ? "" : tbl.Fields["Source"].Value);
			WinglGlobal.GetJrnBatchEntries(ref this);
			lstBatch.ListIndex = idx;
			grdEntries.ReDraw = true;
		}
	}
}
