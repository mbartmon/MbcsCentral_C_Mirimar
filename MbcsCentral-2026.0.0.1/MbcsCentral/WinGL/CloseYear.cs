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
	internal partial class CloseYear : System.Windows.Forms.Form
	{
		short nfy;
		short r;
		short fye;
		int pla;
		bool trn;
		System.DateTime StartDate;
		System.DateTime EndDate;
///******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			///******************************************************************************

			switch (Index) {

				case 0:
					// Close Year
					CloseYearSub();
					break;

				case 1:
					// Exit
					this.Close();
					break;

			}

		}
///******************************************************************************
		private void CloseYear_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			///******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8505);
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6165);
			this.Left = 0;
			this.Top = 0;

			//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			StatBar.Items[1].AutoSize = true;

			if (WinglGlobal.BalanceFwd) {
				this.Text = "Bring Prior Year Balances Forward";
				this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7300);
				Cmd[0].Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Cmd[0].Left) - 1200);
				Cmd[1].Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Cmd[1].Left) - 1200);
				XGrid1.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(XGrid1.Width) - 1200);
				XGrid1.TargetCol(2);
				XGrid1.ColVisible = false;
			}

			WinglGlobal.getAccounts(lstAccounts, , "003_");

			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT CompanyNumber, Name, CurrentFiscalYear FROM Company ORDER BY Name");
			if (tbl.RecordCount > 0) {
				tbl.MoveLast();
				XGrid1.Rows = tbl.RecordCount;
				tbl.MoveFirst();
				r = 0;
				while (!tbl.EOF) {
					XGrid1.TargetCell(4, r);
					XGrid1.Value = tbl.Fields["CompanyNumber"];
					XGrid1.TargetCell(0, r);
					XGrid1.Value = tbl.Fields["Name"];
					XGrid1.TargetCell(1, r);
					XGrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["CurrentFiscalYear"].Value, "0000");
					//If BalanceFwd Then
					//    XGrid1.TargetCell 2, r
					//    XGrid1.Enabled = False
					//End If
					tbl.MoveNext();
					r = r + 1;
				}
				tbl.Close();
			}
			XGrid1.TargetCol(4);
			XGrid1.ColVisible = false;

			for (i = DateAndTime.Year(DateAndTime.Today) - 2; i <= DateAndTime.Year(DateAndTime.Today) + 5; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "0000"));
			}
			nfy = DateAndTime.Year(DateAndTime.Today);

			 // ERROR: Not supported in C#: OnErrorStatement

			tbl = WinglGlobal.GLdb.OpenRecordset("Control");
			if (tbl.RecordCount > 0) {
				nfy = tbl.Fields["CurrentFiscalYear"].Value;
				pla = tbl.Fields["GLProfitLossAcct"].Value;
				tbl.Close();
				for (i = 0; i <= lstAccounts.ListCount - 1; i++) {
					lstAccounts.Col = 1;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstAccounts.Row = i;
					if (Conversion.Val(lstAccounts.ColList) == pla) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstAccounts.ListIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				for (i = 0; i <= lstYear.Items.Count - 1; i++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (Conversion.Val(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstYear, i)) == nfy) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstYear.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}

		}
		public void CloseYearSub()
		{
			object m = null;
			object xsql = null;

			//UPGRADE_WARNING: Lower bound of array profit was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			short i = 0;
			short c = 0;
			bool fwd = false;
			decimal total = default(decimal);
			decimal[] profit = new decimal[3];
			//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblBalance may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblComp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblComp = null;
			DAO.Recordset tblBalance = null;
			DAO.Recordset tblacct = null;
			int cmp = 0;
			//UPGRADE_WARNING: Arrays in structure tblTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblTemp = null;
			System.DateTime tdate = default(System.DateTime);
			int dpt = 0;
			int act = 0;
			short testcol = 0;

			if (WinglGlobal.BalanceFwd) {
				testcol = 3;
			} else {
				testcol = 2;
			}
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			for (i = 0; i <= r - 1; i++) {
				XGrid1.TargetCell(testcol, i);
				if (XGrid1.Value != 0) {
					XGrid1.TargetCell(4, i);
					cmp = Conversion.Val(XGrid1.Value);
					tdate = DateAndTime.DateSerial(nfy, DateAndTime.Month(WinglGlobal.CurrentFiscalYear(ref cmp)), 1);
					StartDate = DateAndTime.DateSerial(nfy - 1, DateAndTime.Month(tdate), 1);
					EndDate = DateAndTime.DateSerial(nfy, DateAndTime.Month(tdate), 1 - 1);
					XGrid1.TargetCell(0, i);
					//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					StatBar.Items[1].Text = "Processing Company: " + XGrid1.Value;
					XGrid1.TargetCell(1, i);
					switch (WinglGlobal.BalanceFwd) {
						case false:
							if (Conversion.Val(XGrid1.Value) >= nfy) {
								//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
								Interaction.MsgBox(Strings.Trim(Strings.Mid(StatBar.Items[1].Text, 21)) + " has already had it's year closed." + Constants.vbCrLf + "Cannot close year again.");
								goto CloseYearSub_next;
							}
							break;
						case true:
							if (Conversion.Val(XGrid1.Value) < nfy) {
								//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
								Interaction.MsgBox(Strings.Trim(Strings.Mid(StatBar.Items[1].Text, 21)) + " has not had it's year closed." + Constants.vbCrLf + "Cannot bring balances forward yet.");
								goto CloseYearSub_next;
							}
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = "SELECT * FROM Transactions WHERE CompanyNumber = " + cmp + " AND Date >= #" + tdate;
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = xsql + "# AND mid(Source, 1, 10) = 'Close Year'";
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblTemp = WinglGlobal.GLdb.OpenRecordset(xsql);
							if (tblTemp.RecordCount > 0) {
								//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
								Interaction.MsgBox(Strings.Trim(Strings.Mid(StatBar.Items[1].Text, 21)) + " has had it's balances brought forward." + Constants.vbCrLf + "Cannot bring balances forward again.");
								goto CloseYearSub_next;
								tblTemp.Close();
							}
							break;
					}
					XGrid1.TargetCell(3, i);
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					fwd = (XGrid1.Value == 0 | Information.IsDBNull(XGrid1.Value) ? false : true);
					XGrid1.TargetCell(4, i);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + XGrid1.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblBalance = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tblBalance.RecordCount > 0) {
						if (fwd) {
							tblTemp = WinglGlobal.GLdb.OpenRecordset("TransactionTemp");
							trn = true;
						}
						dpt = tblBalance.Fields["DepartmentNumber"].Value;
						while (!tblBalance.EOF) {
							DAODBEngine_definst.Workspaces(0).BeginTrans();
							if (!WinglGlobal.BalanceFwd)
								tblBalance.Edit();
							for (m = 1; m <= 12; m++) {
								if (!WinglGlobal.BalanceFwd) {
									//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									tblBalance.Fields["P_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value = tblBalance.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value;
									//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									tblBalance.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value = 0;
								}
								//If fwd Then
								//    act = tblBalance("AccountNumber")
								//    Set tblacct = GLdb.OpenRecordset("SELECT MajorCategory FROM Accounts WHERE Key = " & act)
								//    If tblacct.RecordCount > 0 Then
								//        c = Val(tblacct("MajorCategory"))
								//        If c <= 3 Then
								//            total = total + tblBalance("P_Month" & Format(m, "00"))
								//        Else
								//            profit(c - 3) = profit(c - 3) + tblBalance("P_Month" & Format(m, "00"))
								//        End If
								//        tblacct.Close
								//    End If
								//End If
							}

							if (fwd) {
								act = tblBalance.Fields["AccountNumber"].Value;
								tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT MajorCategory FROM Accounts WHERE Key = " + act);
								if (tblacct.RecordCount > 0) {
									c = Conversion.Val(tblacct.Fields["MajorCategory"].Value);
									if (c <= 3) {
										total = WinglGlobal.AcctBal(ref cmp, ref act, ref StartDate, ref EndDate, ref dpt);
									} else {
										profit[c - 3] = profit[c - 3] + WinglGlobal.AcctBal(ref cmp, ref act, ref StartDate, ref EndDate, ref dpt);
									}
								}
								tblacct.Close();
							}

							if (!WinglGlobal.BalanceFwd)
								tblBalance.Update();
							if (fwd & c <= 3 & total != 0) {
								tblTemp.AddNew();
								tblTemp.Fields["CompanyNumber"].Value = cmp;
								tblTemp.Fields["DepartmentNumber"].Value = dpt;
								tblTemp.Fields["AccountNumber"].Value = act;
								tblTemp.Fields["FiscalYear"].Value = nfy;
								tblTemp.Fields["Date"].Value = tdate;
								tblTemp.Fields["Journal"].Value = " ";
								tblTemp.Fields["Source"].Value = "Close Year";
								tblTemp.Fields["Description"].Value = "Openning Balance";
								tblTemp.Fields["Amount"].Value = total;
								tblTemp.Update();
								total = 0;
							}
							DAODBEngine_definst.Workspaces(0).CommitTrans();
							tblBalance.MoveNext();
							if (fwd & !tblBalance.EOF) {
								if (dpt != tblBalance.Fields["DepartmentNumber"].Value) {
									tblTemp.AddNew();
									tblTemp.Fields["CompanyNumber"].Value = cmp;
									tblTemp.Fields["DepartmentNumber"].Value = dpt;
									tblTemp.Fields["AccountNumber"].Value = pla;
									tblTemp.Fields["FiscalYear"].Value = nfy;
									tblTemp.Fields["Date"].Value = tdate;
									tblTemp.Fields["Journal"].Value = " ";
									tblTemp.Fields["Source"].Value = "Close Year";
									tblTemp.Fields["Description"].Value = "Openning Balance";
									tblTemp.Fields["Amount"].Value = -(-profit[1] - profit[2]);
									tblTemp.Update();
									profit[1] = 0;
									profit[2] = 0;
									dpt = tblBalance.Fields["DepartmentNumber"].Value;
								}
							}
						}
						if (fwd) {
							tblTemp.AddNew();
							tblTemp.Fields["CompanyNumber"].Value = cmp;
							tblTemp.Fields["DepartmentNumber"].Value = dpt;
							tblTemp.Fields["AccountNumber"].Value = pla;
							tblTemp.Fields["FiscalYear"].Value = nfy;
							tblTemp.Fields["Date"].Value = tdate;
							tblTemp.Fields["Journal"].Value = " ";
							tblTemp.Fields["Source"].Value = "Close Year";
							tblTemp.Fields["Description"].Value = "Openning Balance";
							tblTemp.Fields["Amount"].Value = -(-profit[1] - profit[2]);
							tblTemp.Update();
							profit[1] = 0;
							profit[2] = 0;
							tblTemp.Close();
						}
						tblBalance.Close();
						//UPGRADE_NOTE: Object tblBalance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						tblBalance = null;
					}
					tblComp = WinglGlobal.GLdb.OpenRecordset("SELECT CurrentFiscalYear FROM Company WHERE CompanyNumber = " + cmp);
					if (tblComp.RecordCount > 0) {
						tblComp.Edit();
						tblComp.Fields["CurrentFiscalYear"].Value = nfy;
						tblComp.Update();
						tblComp.Close();
					}
				}
				CloseYearSub_next:
			}
			if (fwd) {
				 // ERROR: Not supported in C#: OnErrorStatement

				tblTemp.Close();
			}
			//UPGRADE_WARNING: Lower bound of collection StatBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			StatBar.Items[1].Text = "Processing Completed";
			this.Cursor = System.Windows.Forms.Cursors.Default;

		}
		private void CloseYear_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			DAO.TableDef def = null;
			DAO.Field fld = null;
			clsTransEditList xrpt = null;
			Form_Unload_again:

			 // ERROR: Not supported in C#: OnErrorStatement

			tbl = WinglGlobal.GLdb.OpenRecordset("Control");
			if (tbl.RecordCount > 0) {
				tbl.Edit();
				tbl.Fields["CurrentFiscalYear"].Value = nfy;
				tbl.Fields["GLProfitLossAcct"].Value = pla;
				tbl.Update();
				tbl.Close();
			}

			goto Form_Unload_exit;
			Form_Unload_error:

			if (Err().Number == 3265) {
				 // ERROR: Not supported in C#: OnErrorStatement

				tbl.Close();
				def = WinglGlobal.GLdb.TableDefs["Control"];
				fld = def.CreateField("CurrentFiscalYear", DAO.DataTypeEnum.dbInteger, 2);
				def.Fields.Append(fld);
				fld = def.CreateField("GLProfitLossAcct", DAO.DataTypeEnum.dbInteger, 4);
				def.Fields.Append(fld);
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed(Conversion.ErrorToString(), , this.Text);
				 // ERROR: Not supported in C#: ResumeStatement

			}
			Form_Unload_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			if (trn) {
				WinglGlobal.rptCancel = false;
				xrpt = new clsTransEditList();
				//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				xrpt = null;
			}

		}
		private void lstAccounts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			lstAccounts.Row = lstAccounts.ListIndex;
			lstAccounts.Col = 1;
			pla = Conversion.Val(lstAccounts.ColList);

		}
//UPGRADE_WARNING: Event lstYear.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstYear_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			nfy = Conversion.Val(lstYear.Text);

		}
		private void XGrid1_ClickCell(System.Object eventSender, AxXGridLib.XGridEvents_ClickCellEvent eventArgs)
		{

			//UPGRADE_ISSUE: Control TargetCell could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//Me.ActiveControl.TargetCell(eventArgs.Col, eventArgs.Row)
			//UPGRADE_ISSUE: Control CellType could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//If Me.ActiveControl.CellType = XGridLib.xtCellType.xtctCheckBox Then
			//UPGRADE_ISSUE: Control Value could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			//Me.ActiveControl.Value = IIf(IsDBNull(Me.ActiveControl.Value) Or Me.ActiveControl.Value = False, True, False)
			//eventArgs.cancel = True
			//End If

		}
	}
}
