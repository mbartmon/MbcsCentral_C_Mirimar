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
	internal partial class BatchJrnEdit : System.Windows.Forms.Form
	{
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object postflag = null;
			object xrpt = null;
			object total = null;

			bool entered = false;

			switch (Index) {

				case 0:
					// SAVE
					//entered = False
					//For i = 1 To grdEntries.MAXROWS
					//    grdEntries.Row = i
					//    grdEntries.Col = 4
					//    If Val(grdEntries.Value) <> 0 Then
					//        entered = True
					//        Exit For
					//    End If
					//Next
					//If Not entered Then
					//    VBA.Interaction.MsgBox "No entries to save.", , "Batch Journal Entries"
					//    Exit Sub
					//End If
					//If lstCompanies.ListIndex = -1 Then
					//    If VBA.Interaction.MsgBox("No company selected.  Continue", vbYesNo, "Batch Journal Entries") = vbNo Then
					//        Exit Sub
					//    End If
					//End If
					//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
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
					//UPGRADE_WARNING: Couldn't resolve default property of object postflag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
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

			//If Val(txtCheckNumber.Text) = 0 And Trim(txtSource.Text) = "" Then
			//    On Error Resume Next
			//    Set tblControl = GLdb.OpenRecordset("Control")
			//    tblControl.Edit
			//    s = IIf(IsNull(tblControl("GLSourceNumber")), 0, tblControl("GLSourceNumber"))
			//    s = s + 1
			//    tblControl("GLSourceNumber") = s
			//    tblControl.update
			//    tblControl.Bookmark = tblControl.LastModified
			//    txtSource.Text = "J-" & Format(s, "000000")
			//    tblControl.Close
			//End If
			//If Trim(txtSource.Text) = "" Then txtSource.Text = "*"

			 // ERROR: Not supported in C#: OnErrorStatement

			lstBatch.Col = 1;
			lstBatch.Row = lstBatch.ListIndex;
			DAODBEngine_definst.Workspaces(0).BeginTrans();
			tblTranTemp = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM BatchJrnHeader WHERE Key = " + lstBatch.ColList);
			if (tblTranTemp.RecordCount > 0) {
				tblTranTemp.Edit();
			} else {
				tblTranTemp.AddNew();
				tblTranTemp.Fields["Key"].Value = lstBatch.ColList;
			}
			lstCompanies.Col = 1;
			lstCompanies.Row = lstCompanies.ListIndex;
			tblTranTemp.Fields["Company"].Value = Conversion.Val(lstCompanies.ColList);
			if (lstJournals.ListIndex > -1) {
				lstJournals.Row = lstJournals.ListIndex;
				tblTranTemp.Fields["Journal"].Value = lstJournals.ColList;
			}
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			tblTranTemp.Fields["Source"].Value = (string.IsNullOrEmpty(txtSource.CtlText) ? " " : txtSource.CtlText);
			tblTranTemp.Update();
			tblTranTemp.Close();
			WinglGlobal.GLdb.Execute(("DELETE * FROM BatchJrnEntries WHERE Key = " + lstBatch.ColList));
			tblTranTemp = WinglGlobal.GLdb.OpenRecordset("BatchJrnEntries");
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				grdEntries.Row = i;
				grdEntries.Col = 1;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value)) {
					grdEntries.Col = 1;
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(Strings.Trim(grdEntries.Value))) {
						tblTranTemp.AddNew();
						tblTranTemp.Fields["Key"].Value = lstBatch.ColList;
						grdEntries.Col = 2;
						//UPGRADE_WARNING: Couldn't resolve default property of object SearchList(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tblTranTemp.Fields["Department"].Value = Conversion.Val(WinglGlobal.SearchList(ref lstDepartments, ref Strings.Left(grdEntries.Text, 50), ref 2, ref 1));
						grdEntries.Col = 1;
						//UPGRADE_WARNING: Couldn't resolve default property of object SearchList(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tblTranTemp.Fields["Account"].Value = WinglGlobal.SearchList(ref lstAccounts, ref Strings.Left(grdEntries.Text, 50), ref 2, ref 1);
						grdEntries.Col = 3;
						tblTranTemp.Fields["Description"].Value = grdEntries.Value;
						grdEntries.Col = 4;
						tblTranTemp.Fields["Amount"].Value = Conversion.Val(grdEntries.Value);
						tblTranTemp.Update();
					}
				}
			}
			DAODBEngine_definst.Workspaces(0).CommitTrans();
			tblTranTemp.Close();
			goto saveEntries_exit;
			saveEntries_error:

			 // ERROR: Not supported in C#: OnErrorStatement

			DAODBEngine_definst.Workspaces(0).Rollback();
			 // ERROR: Not supported in C#: ResumeStatement

			saveEntries_exit:


		}
		private void cmdBatch_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdBatch.GetIndex(eventSender);

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			switch (Index) {
				case 0:
					// Delete
					if (lstBatch.ListIndex == -1) {
						LogError.MsgBox_Renamed("You must select a batch to delete.");
						return;
					}
					if (LogError.MsgBox_Renamed("Do you want to delete this batch?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
						lstBatch.Row = lstBatch.ListIndex;
						lstBatch.Col = 1;
						 // ERROR: Not supported in C#: OnErrorStatement

						DAODBEngine_definst.Workspaces(0).BeginTrans();
						WinglGlobal.GLdb.Execute(("DELETE * FROM BatchJrnHeader WHERE Key = " + lstBatch.ColList));
						WinglGlobal.GLdb.Execute(("DELETE * FROM BatchJrnEntries WHERE Key = " + lstBatch.ColList));
						DAODBEngine_definst.Workspaces(0).CommitTrans();
					}
					WinglGlobal.GetJrnBatches(ref this);
					lstBatch.ListIndex = -1;
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					lstBatch.CtlText = "";
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					lstJournals.CtlText = "";
					lstJournals.ListIndex = -1;
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					txtSource.CtlText = "";
					grdEntries.ClearRange(-1, -1, -1, -1, true);
					break;
				case 1:
					// Add New
					lblBatch.Visible = true;
					txtBatch.Visible = true;
					cmdBatch[2].Visible = true;
					cmdBatch[3].Visible = true;
					txtBatch.Focus();
					break;
				case 2:
					// Save
					SaveNewBatch();
					lblBatch.Visible = false;
					txtBatch.Visible = false;
					cmdBatch[2].Visible = false;
					cmdBatch[3].Visible = false;
					break;
				case 3:
					// Cancel
					lblBatch.Visible = false;
					txtBatch.Visible = false;
					cmdBatch[2].Visible = false;
					cmdBatch[3].Visible = false;
					lstBatch.Focus();
					break;
			}
			goto cmdBatch_click_exit;
			db_error:

			 // ERROR: Not supported in C#: OnErrorStatement

			DAODBEngine_definst.Workspaces(0).Rollback();
			 // ERROR: Not supported in C#: ResumeStatement

			cmdBatch_click_exit:



		}
		private void SaveNewBatch()
		{
			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			tbl = WinglGlobal.GLdb.OpenRecordset("BatchJrnHeader");
			tbl.AddNew();
			tbl.Fields["BatchName"].Value = txtBatch.Text;
			tbl.Update();
			tbl.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(tbl.LastModified);
			string Batch = null;
			Batch = tbl.Fields["BatchName"].Value;
			tbl.Close();
			WinglGlobal.GetJrnBatches(ref this);
			lstBatch.SearchMethod = LpLib.enumSearchMethod.SearchMethodGreaterOrEqual;
			lstBatch.SearchText = Batch;
			lstBatch.Action = LpLib.enumAction.ActionSearch;
			lstBatch.ListIndex = lstBatch.SearchIndex;

		}
//UPGRADE_WARNING: Form event BatchJrnEdit.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void BatchJrnEdit_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{

			grdEntries.Col = 1;
			grdEntries.Row = 1;
			grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;
			lstBatch.Focus();

		}
		private void BatchJrnEdit_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object r = null;
			this.Top = 0;
			this.Left = 0;
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(5445);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(10800);
			WinglGlobal.GetJrnBatches(ref this);
			WinglGlobal.GetCompanies(ref this);
			lstCompanies.Row = -1;
			lstCompanies.AddItem(" " + Strings.Chr(9) + "0");
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
		}
//******************************************************************************
		private void grdEntries_Change(System.Object eventSender, AxFPSpread._DSpreadEvents_ChangeEvent eventArgs)
		{
			object total = null;
			//******************************************************************************

			decimal a = default(decimal);
			short i = 0;

			//If Col <> 4 Then Exit Sub
			//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			total = 0;
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				grdEntries.Row = i;
				grdEntries.Col = 4;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(grdEntries.Value)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					total = total + grdEntries.Value;
				}
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
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
			object total = null;
			//******************************************************************************

			//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			grdEntries.Refresh();
			//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			total = 0;
			for (i = 1; i <= grdEntries.MaxRows; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdEntries.Row = i;
				grdEntries.Col = 4;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(grdEntries.Value) & !string.IsNullOrEmpty(grdEntries.Value)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					total = total + grdEntries.Value;
				}
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object total. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
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
							if (grdEntries.Lock == true)
								return;

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
		private void lstBatch_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			int idx = 0;

			idx = lstBatch.ListIndex;
			if (idx < 0)
				return;
			grdEntries.ClearRange(-1, -1, -1, -1, true);
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
		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object r = null;
			object i = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstCompanies.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (i < 0) {
				lstDepartments.Clear();
				grdEntries.ClearRange(2, -1, 2, -1, true);
				grdEntries.Col = 2;
				grdEntries.Row = -1;
				grdEntries.Lock = true;
				return;
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstCompanies.Row = i;
			lstCompanies.Col = 1;

			// Department List
			if (lstCompanies.ColList == "0") {
				lstDepartments.Clear();
				grdEntries.ClearRange(2, -1, 2, -1, true);
				grdEntries.Col = 2;
				grdEntries.Row = -1;
				grdEntries.Lock = true;
			} else {
				WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
				grdEntries.Col = 2;
				for (r = 0; r <= grdEntries.MaxRows - 1; r++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					grdEntries.Row = r;
					grdEntries.Action = FPSpread.ActionConstants.ActionActiveCell;
					grdEntries.TypeComboBoxhWnd = lstDepartments.hWnd;
					grdEntries.Lock = false;
				}
				lstDepartments.ListIndex = 0;
			}
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
			//txtCheckNumber.Text = ""
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
			lstCompanies.Focus();

		}
//UPGRADE_WARNING: Event BatchJrnEdit.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void BatchJrnEdit_Resize(System.Object eventSender, System.EventArgs eventArgs)
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
	}
}
