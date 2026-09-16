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
	internal partial class CheckRecon : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.1  2009/08/21 12:36:11  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:06  mike
//* Copied from wingl folder
//*
//*
//*   Auto-added
//*
//* Revision 2.17  by: normal  Rev date: 1/24/2003 10:27:02 AM
//*   Check Reconciliation - Bank Error Handling
//*
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:33:38 AM
//*   Initial revision.
//*
//* $Endlog$
//* $Endlog$
		DAO.Recordset tblChecks;
		bool update_Renamed;
		short FixedRows;
		clsCheckRecon xrpt;
		int cmp;
		int dpt;
		short rw;
		decimal bbalance;
		decimal sbalance;
		decimal badj;
		bool activated_Renamed;
		public bool JournalSet;
		public bool success;
		public string chkJournal;
//******************************************************************************
		private void cmdAdj_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdAdj.GetIndex(eventSender);
			object x = null;
			//******************************************************************************

			FixedRows = FixedRows + 1;
			grdChecks.MaxRows = grdChecks.MaxRows + 1;
			grdChecks.Row = 1;
			grdChecks.Action = FPSpread.ActionConstants.ActionInsertRow;
			grdChecks.Row = 1;
			grdChecks.Col = 1;
			switch (Index) {
				case 0:
					grdChecks.Value = "DEPOSIT";
										x = "Deposit";
					break;
				case 1:
					grdChecks.Value = "DEBIT";
										x = "Debit";
					break;
				case 2:
					grdChecks.Value = "BnkErr-";
										x = "Bank Error Against";
					break;
				case 3:
					grdChecks.Value = "BnkErr+";
										x = "Bank Error In Favor";
					break;
			}
			grdChecks.Col = 2;
						grdChecks.Value = dtStatement.Value;
			grdChecks.Lock = false;
			grdChecks.Col = 3;
			grdChecks.Lock = false;
			grdChecks.Col = 4;
			grdChecks.Lock = true;
			grdChecks.Col = 5;
			grdChecks.Lock = false;
						grdChecks.Value = x;
			grdChecks.Col = 6;
						if (Strings.Left(x, 10) == "Bank Error") {
				grdChecks.Lock = true;
			} else {
				grdChecks.Value = Convert.ToString(false);
			}
			grdChecks.Row = 1;
			grdChecks.Col = 7;
			grdChecks.TypeButtonText = "DEL";
			grdChecks.Lock = false;
			grdChecks.Col = 8;
			grdChecks.Value = Convert.ToString(0);
			grdChecks.Col = 3;
			grdChecks.Action = FPSpread.ActionConstants.ActionActiveCell;
			grdChecks.Focus();
			lstAccounts.ListIndex = -1;
			lstAccounts.Enabled = true;

		}
//******************************************************************************
		private void cmdChecks_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

									DAO.Recordset tblChecks = null;
			DAO.Recordset tblTrans = null;
			short r = 0;

			lstCompanies.Row = lstCompanies.ListIndex;
			lstCompanies.Col = 1;
			cmp = Conversion.Val(lstCompanies.ColList);
			lstDepartments.Row = lstDepartments.ListIndex;
			lstDepartments.Col = 1;
			dpt = Conversion.Val(lstDepartments.ColList);
			FixedRows = 0;

			get_CheckData();

			get_BookBalance();

			txtStatement.Focus();

		}
//******************************************************************************
		private void cmdClear_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object x = null;
			//******************************************************************************

			short r = 0;

			if (LogError.MsgBox_Renamed("Are you sure you want to do this?", MsgBoxStyle.YesNo, this.Name) == MsgBoxResult.Yes) {
				update_Renamed = true;
				for (r = 1; r <= grdChecks.MaxRows; r++) {
					grdChecks.Row = r;
					grdChecks.Col = 2;
										if (DateAndTime.DateValue(grdChecks.Value) <= DateAndTime.DateValue(dtClear.Value)) {
						grdChecks.Col = 6;
						grdChecks.Value = Convert.ToString(true);
					} else {
												x = grdChecks.Value;
					}
				}
				update_Renamed = false;
				grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));
			}

		}
//******************************************************************************
		private void cmdFinish_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdFinish.GetIndex(eventSender);
			//******************************************************************************

			decimal d = default(decimal);
			string s = null;
			short op = 0;

			switch (Index) {

				case 0:
					// Accept
					if (grdChecks.MaxRows == 0) {
						return;
					}
										d = Convert.ToDecimal(txtTotal[6].Text) - Convert.ToDecimal(txtBook[2].CtlText);
					switch (d) {
						case  // ERROR: Case labels with binary operators are unsupported : LessThan
0:
							s = "The reconciliation is NOT successful. " + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "The Adjusted Statement Balance is " + Constants.vbCrLf;
							s = s + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(System.Math.Abs(d), "##,###,##0.00") + " LESS than the Book Balance." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "You need to spend more or receive less.";
							op = MsgBoxStyle.OkOnly + MsgBoxStyle.Critical;
							break;
						case  // ERROR: Case labels with binary operators are unsupported : Equality
0:
							s = "The reconciliation IS successful." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "You will now be given the opportunity" + Constants.vbCrLf;
							s = s + "to post the results of the reconciliation" + Constants.vbLf;
							s = s + "or abandon them." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "If you choose to post the results, all cleared" + Constants.vbCrLf;
							s = s + "Checks will be permanently marked as having" + Constants.vbCrLf;
							s = s + "been cleared and all Adjustments that have" + Constants.vbCrLf;
							s = s + "been assigned G/L Accounts will be posted" + Constants.vbLf;
							s = s + "to the General Ledger." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "You may also choose to continue with the" + Constants.vbCrLf;
							s = s + "reconciliation, make any changes you wish," + Constants.vbCrLf;
							s = s + "and return to this point later." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "Do you want to post the results?";
							op = MsgBoxStyle.YesNo + MsgBoxStyle.Question;
							success = true;
							break;
						case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
0:
							s = "The reconciliation is NOT successful. " + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "The Adjusted Statement Balance is " + Constants.vbCrLf;
							s = s + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(System.Math.Abs(d), "##,###,##0.00") + " MORE than the Book Balance." + Constants.vbCrLf + Constants.vbCrLf;
							s = s + "You need to receive more or spend less.";
							op = MsgBoxStyle.OkOnly + MsgBoxStyle.Critical;
							break;
					}
					if (Interaction.MsgBox(s, op, this.Name) == MsgBoxResult.Yes) {
						if (Interaction.MsgBox("Display the Reconciliation report?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, this.Name) == MsgBoxResult.Yes) {
							cmdFinish_Click(cmdFinish[4], new System.EventArgs());
						} else {
							Post_Recon();
						}
					}
					break;

				case 1:
					// Save
					if (grdChecks.MaxRows == 0) {
						return;
					}
					dlgSaveOpen.Title = "Save a Reconciliation - RightClick pops up a menu";
					dlgSaveSave.Title = "Save a Reconciliation - RightClick pops up a menu";
					dlgSaveOpen.DefaultExt = WinglGlobal.SPext;
					dlgSaveSave.DefaultExt = WinglGlobal.SPext;
										dlgSaveOpen.Filter = "*." + WinglGlobal.SPext;
					dlgSaveSave.Filter = "*." + WinglGlobal.SPext;
					lstCompanies.Col = 1;
					lstDepartments.Col = 1;
					dlgSaveOpen.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "000_") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtStatement.Value, "yyyymmdd");
					dlgSaveSave.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "000_") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtStatement.Value, "yyyymmdd");
					 // ERROR: Not supported in C#: OnErrorStatement

					Err().Clear();
					dlgSaveSave.ShowDialog();
					dlgSaveOpen.FileName = dlgSaveSave.FileName;
					if (Err().Number == 0 & !string.IsNullOrEmpty(dlgSaveOpen.FileName)) {
						grdChecks.Row = 1;
						grdChecks.Col = 9;
						grdChecks.Lock = false;
						grdChecks.CellType = FPSpread.CellTypeConstants.CellTypeFloat;
												grdChecks.CtlEditMode = true;
												grdChecks.Value = Convert.ToString(Convert.ToDecimal(txtStatement.CtlText));
						grdChecks.SaveToFile(dlgSaveOpen.FileName, false);
					}
					break;

				case 2:
					// Reload
					if (grdChecks.MaxRows > 0) {
						if (Interaction.MsgBox("Are you sure that you want to abandon this reconciliation?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, this.Name) == MsgBoxResult.No) {
							return;
						}
					}
					dlgSaveOpen.Title = "Saved Reconciliations - Choose one or CANCEL - RightClick pops up a menu";
					dlgSaveSave.Title = "Saved Reconciliations - Choose one or CANCEL - RightClick pops up a menu";
										dlgSaveOpen.Filter = "*." + WinglGlobal.SPext;
					dlgSaveSave.Filter = "*." + WinglGlobal.SPext;
					dlgSaveOpen.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*." + WinglGlobal.SPext;
					dlgSaveSave.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*." + WinglGlobal.SPext;
					 // ERROR: Not supported in C#: OnErrorStatement

					Err().Clear();
					dlgSaveOpen.ShowDialog();
					dlgSaveSave.FileName = dlgSaveOpen.FileName;
					if (Err().Number == 0 & !string.IsNullOrEmpty(dlgSaveOpen.FileName)) {
						Reload();
					}
					break;

				case 3:
					// Export
					if (grdChecks.MaxRows == 0) {
						return;
					}
					dlgSaveOpen.Title = "Export to Excel Format - RightClick pops up a menu";
					dlgSaveSave.Title = "Export to Excel Format - RightClick pops up a menu";
					dlgSaveOpen.DefaultExt = "xls";
					dlgSaveSave.DefaultExt = "xls";
										dlgSaveOpen.Filter = "*.xls";
					dlgSaveSave.Filter = "*.xls";
					dlgSaveOpen.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*.xls";
					dlgSaveSave.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*.xls";
					 // ERROR: Not supported in C#: OnErrorStatement

					Err().Clear();
					dlgSaveSave.ShowDialog();
					dlgSaveOpen.FileName = dlgSaveSave.FileName;
					if (Err().Number == 0 & !string.IsNullOrEmpty(dlgSaveOpen.FileName)) {
						grdChecks.ExportToExcel(dlgSaveOpen.FileName, "Bank Rec", "");
					}
					break;

				case 4:
					// Print
					this.Visible = false;
					//rptCancel = False
					xrpt = new clsCheckRecon();
					lstCompanies.Col = 0;
					xrpt.CompanyName = lstCompanies.ColList;
					lstDepartments.Col = 0;
					xrpt.DepartmentName = lstDepartments.ColList;
										xrpt.StatementDate = dtStatement.Value;
					xrpt.CheckRecon(ref false);
										xrpt = null;
					break;

				case 5:
					// Exit
					if (grdChecks.MaxRows > 0) {
						if (Interaction.MsgBox("Are you sure that you want to abandon this reconciliation?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, this.Name) == MsgBoxResult.No) {
							return;
						}
					}
					this.Close();
					break;

			}

		}
//******************************************************************************
		private void dtStatement_Change(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			get_BookBalance();

		}
//******************************************************************************
		private void CheckRecon_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

						string x = null;
			DAO.Recordset tblControl = null;

			if (grdChecks.MaxRows > 0 | activated_Renamed)
				return;

			activated_Renamed = true;

						x = FileSystem.Dir(WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*." + WinglGlobal.SPext);
			if (!string.IsNullOrEmpty(x)) {
				if (Interaction.MsgBox("There are incomplete reconciliations.  Would you like to see the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, this.Name) == MsgBoxResult.Yes) {
					dlgSaveOpen.DefaultExt = WinglGlobal.SPext;
					dlgSaveSave.DefaultExt = WinglGlobal.SPext;
					dlgSaveOpen.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*." + WinglGlobal.SPext;
					dlgSaveSave.FileName = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\*." + WinglGlobal.SPext;
										dlgSaveOpen.Filter = "*." + WinglGlobal.SPext;
					dlgSaveSave.Filter = "*." + WinglGlobal.SPext;
					dlgSaveOpen.Title = "Saved Reconciliations - Choose one or CANCEL - RightClick pops up a menu";
					dlgSaveSave.Title = "Saved Reconciliations - Choose one or CANCEL - RightClick pops up a menu";
					 // ERROR: Not supported in C#: OnErrorStatement

					Err().Clear();
					dlgSaveOpen.ShowDialog();
					dlgSaveSave.FileName = dlgSaveOpen.FileName;
					if (Err().Number == 0 & !string.IsNullOrEmpty(dlgSaveOpen.FileName)) {
						Reload();
						return;
					}
				}
			}

			lstCompanies.Focus();

		}
//******************************************************************************
		private void CheckRecon_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

						DAO.Recordset tblControl = null;

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop + 1090);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft + 125);
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6885);

			dtStatement.Value = DateAndTime.Today;
			dtClear.Value = System.Date.FromOADate(DateAndTime.Today.ToOADate() - 30);
			tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			 // ERROR: Not supported in C#: OnErrorStatement

			dtStatement.Value = tblControl.Fields["StmtDate"];
			dtClear.Value = tblControl.Fields["ClearDate"];
			tblControl.Close();

			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(lstAccounts);
			update_Renamed = false;

		}
//******************************************************************************
		private void CheckRecon_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			success = false;
			grdChecks.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 3510);
			Label2.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Height) + 50);
			Label9.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Height) + 50);
			txtTotal[0].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Height));
			txtTotal[1].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Height));
			txtTotal[3].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdChecks.Height));
			txtTotal[2].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[1].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[1].Height));
			txtTotal[4].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[3].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[3].Height));
			txtTotal[5].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[4].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[4].Height));
			txtTotal[6].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[5].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[5].Height));
			Label3.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[0].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[0].Height) + 20);
			Label7.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label9.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label9.Height) + 20);
			Label8[0].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label7.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label7.Height) + 20);
			Label8[1].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label8[0].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label8[0].Height) + 20);
			cmdAdj[0].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label2.Top) + 270);
			cmdAdj[2].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Label2.Top) + 270);
			cmdAdj[1].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Height));
			cmdAdj[3].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Height));
			cmdClear.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(cmdAdj[0].Height) * 2 + 150);
			lstAccounts.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[2].Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(txtTotal[2].Height));
			Label10.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(lstAccounts.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(lstAccounts.Height) + 50);
			dtClear.Top = cmdClear.Top;
			for (i = 0; i <= 5; i++) {
				cmdFinish[i].Top = cmdClear.Top;
			}

		}
//******************************************************************************
		private void CheckRecon_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			//******************************************************************************

						DAO.Recordset tblControl = null;

			tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			tblControl.Edit();
						tblControl.Fields["StmtDate"].Value = dtStatement.Value;
						tblControl.Fields["ClearDate"].Value = dtClear.Value;
			tblControl.Update();
			tblControl.Close();

		}
//******************************************************************************
		private void grdChecks_Change(System.Object eventSender, AxFPSpread._DSpreadEvents_ChangeEvent eventArgs)
		{
			//******************************************************************************

			decimal x = default(decimal);

			//rw = Row
			//If Col = 6 Then Exit Sub
			//grdChecks.Row = Row
			//If Col = 3 Then
			//grdChecks.Col = 3
			//x = CDec(grdChecks.Value)
			//grdChecks.Col = 4
			//grdChecks.Value = CStr(x)
			//End If
			grdChecks.Col = 1;
			if (grdChecks.Value == "DEPOSIT" | grdChecks.Value == "DEBIT") {
				grdChecks.Col = 8;
				if (string.IsNullOrEmpty(grdChecks.Value) | Conversion.Val(grdChecks.Value) == 0) {
					LogError.MsgBox_Renamed("Please select an account for this ADJUSTMENT", , this.Name);
					lstAccounts.Enabled = true;
					lstAccounts.Focus();
				} else {
					if (Convert.ToDouble(grdChecks.Value) > 0) {
						grdChecks.Col = 3;
						x = Convert.ToDecimal(grdChecks.Value);
						grdChecks.Col = 4;
						if (x != Convert.ToDouble(grdChecks.Value)) {
							LogError.MsgBox_Renamed("Please select an account for the DIFFERENCE", , this.Name);
							lstAccounts.Enabled = true;
							lstAccounts.Focus();
						}
					}
				}
			}
			grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));

		}
//******************************************************************************
		private void grdChecks_LeaveCell(System.Object eventSender, AxFPSpread._DSpreadEvents_LeaveCellEvent eventArgs)
		{
			//******************************************************************************

			object x = null;

			grdChecks.Col = 1;
			if (eventArgs.newRow != -1) {
				rw = eventArgs.newRow;
			} else {
				//rw = Row
			}
			//If NewRow <> Row Then
			//    grdChecks.Row = Row
			//    grdChecks.Col = 1
			//    If grdChecks.Value = "DEPOSIT" Or grdChecks.Value = "DEBIT" Then
			//        grdChecks.Col = 8
			//        If grdChecks.Value = "" Or Val(grdChecks.Value) = 0 Then
			//            Label10.Visible = True
			//            lstAccounts.Enabled = True
			//            lstAccounts.SetFocus
			//        End If
			//    End If
			//End If
			//Label10.Visible = False
			//lstAccounts.Enabled = False
			grdChecks.Row = rw;
			grdChecks.Col = 1;
			if (grdChecks.Value == "DEPOSIT" | grdChecks.Value == "DEBIT") {
				grdChecks.Col = 8;
				if (Conversion.Val(grdChecks.Value) < 0) {
					lstAccounts.ColumnSearch = 1;
					lstAccounts.SearchText = -grdChecks.Value;
					lstAccounts.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
					lstAccounts.Action = LpLib.enumAction.ActionSearch;
					lstAccounts.ListIndex = lstAccounts.SearchIndex;
					lstAccounts.SearchMethod = LpLib.enumSearchMethod.SearchMethodGreaterOrEqual;
					Label10.Visible = true;
					lstAccounts.Enabled = true;
				} else {
					// code for existing deposit difference
					grdChecks.Col = 3;
										x = grdChecks.Value;
					grdChecks.Col = 4;
										if (x != grdChecks.Value) {
												x = x - grdChecks.Value;
						grdChecks.Col = 9;
						lstAccounts.ColumnSearch = 1;
						lstAccounts.SearchText = grdChecks.Value;
						lstAccounts.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
						lstAccounts.Action = LpLib.enumAction.ActionSearch;
						lstAccounts.ListIndex = lstAccounts.SearchIndex;
						lstAccounts.SearchMethod = LpLib.enumSearchMethod.SearchMethodGreaterOrEqual;
						Label10.Visible = true;
						lstAccounts.Enabled = true;
					} else {
						lstAccounts.ListIndex = -1;
					}
				}
				//If Val(grdChecks.Value) <= 0 Then
				//    Label10.Visible = True
				//    lstAccounts.Enabled = True
				//Else
				//    Label10.Visible = False
				//    lstAccounts.Enabled = False
				//End If
			} else {
				//grdChecks.Row = Row
				//If (grdChecks.Value = "DEPOSIT" Or grdChecks.Value = "DEBIT") And eventArgs.NewRow <> Row Then
				//lstAccounts.ListIndex = -1
				//lstAccounts.Enabled = False
				//Label10.Visible = False
				//End If
			}



		}
//******************************************************************************
		private void lstAccounts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			int k = 0;
			short c = 0;

			Label10.Visible = false;
			lstAccounts.Row = lstAccounts.ListIndex;
			lstAccounts.Col = 1;
			k = (!string.IsNullOrEmpty(lstAccounts.ColList) ? lstAccounts.ColList : 0);
			c = grdChecks.Col;
			if (k > 0) {
				grdChecks.Row = rw;
				grdChecks.Col = 8;
				if (Convert.ToDouble(grdChecks.Value) <= 0) {
					grdChecks.Value = Convert.ToString(-k);
					if (k > 0) {
						grdChecks.Col = 6;
						grdChecks.Value = Convert.ToString(true);
					}
				} else {
					grdChecks.Col = 9;
					grdChecks.Value = Convert.ToString(k);
				}
				grdChecks.Col = c;
			}
			//grdChecks.SetFocus

		}
//******************************************************************************
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short i = 0;

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			if (Conversion.Val(lstCompanies.ColList) > 0) {
				WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
				lstDepartments.ListIndex = 0;
				i = lstDepartments.ListIndex;
				lstDepartments.Row = i;
			}

		}
//******************************************************************************
		private void grdChecks_ButtonClicked(System.Object eventSender, AxFPSpread._DSpreadEvents_ButtonClickedEvent eventArgs)
		{
			object ClrFlag = null;
			object x = null;
			object d = null;
			object i = null;
			object dtot = null;
			object ctot = null;
			//******************************************************************************

			decimal ct = default(decimal);
			decimal ot = default(decimal);
			decimal aT = default(decimal);
			decimal dt = default(decimal);
			bool AdjFlag = false;
			decimal amt = default(decimal);
			decimal bamt = default(decimal);

			if (update_Renamed)
				return;

			//rw = Row
			switch (0) {
				//Col
				case 6:
					// CLEARED
					ctot = 0;
					dtot = 0;
					for (i = 1; i <= grdChecks.MaxRows; i++) {
						grdChecks.Row = i;
						grdChecks.Col = 6;
						if (Convert.ToBoolean(grdChecks.Value)) {
							grdChecks.Col = 3;
							dtot = dtot + (string.IsNullOrEmpty(grdChecks.Value) ? 0 : grdChecks.Value);
							grdChecks.Col = 4;
							ctot = ctot + (string.IsNullOrEmpty(grdChecks.Value) ? 0 : grdChecks.Value);
						}
					}

					txtTotal[0].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtot, "##,###,##0.00"), ref 13);
					txtTotal[1].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ctot, "##,###,##0.00"), ref 13);
					d = ctot - dtot;
					txtTotal[2].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(d, "##,###,##0.00"), ref 13);

					break;
				case 7:
					// EDIT / DELETE
					//grdChecks.Row = Row
					grdChecks.Col = 1;

					if (grdChecks.Value == "DEPOSIT" | grdChecks.Value == "DEBIT" | Strings.Left(grdChecks.Value, 6) == "BnkErr") {
						grdChecks.Action = 5;
						grdChecks.MaxRows = grdChecks.MaxRows - 1;
						grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));
						return;
					}
					x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(grdChecks.Value, "000000");
					My.MyProject.Forms.CheckRegister.Tag = x + "(*" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "000") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "0000") + "*)";
					My.MyProject.Forms.CheckRegister.ShowDialog();
					update_Renamed = true;
					get_CheckData();
					update_Renamed = false;

					break;
			}

			ot = 0;
			ct = 0;
			dt = 0;
			badj = 0;
			for (i = 1; i <= grdChecks.MaxRows; i++) {
				grdChecks.Row = i;
				grdChecks.Col = 7;
				if (grdChecks.TypeButtonText == "DEL") {
					AdjFlag = true;
				} else {
					AdjFlag = false;
				}
				grdChecks.Col = 1;
				x = Strings.Trim(grdChecks.Value);
				grdChecks.Col = 6;
				ClrFlag = (grdChecks.Value == "0" ? false : true);
				grdChecks.Col = 4;
				if (!string.IsNullOrEmpty(grdChecks.Value)) {
					amt = Convert.ToDecimal(grdChecks.Value);
					grdChecks.Col = 3;
					bamt = Convert.ToDecimal(grdChecks.Value);
					switch (x) {
						case "DEPOSIT":
							if (!ClrFlag) {
								ct = ct + amt;
							} else {
								if (amt != bamt) {
									badj = badj + (amt - bamt);
								}
							}
							if (AdjFlag) {
								badj = badj + amt;
							}
							break;
						case "DEBIT":
							if (!ClrFlag) {
								dt = dt + amt;
							} else {
								if (amt != bamt) {
									badj = badj + (amt - bamt);
								}
							}
							if (AdjFlag) {
								badj = badj - amt;
							}
							break;
						case "BnkErr-":
							badj = badj - amt;
							break;
						case "BnkErr+":
							badj = badj + amt;
							break;
						default:
							if (!ClrFlag) {
								ot = ot + amt;
							} else {
								badj = badj + (bamt - amt);
							}
							break;
					}
				}
			}
			txtBook[1].CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(badj, "##,###,##0.00"), ref 13);
			txtBook[2].CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(bbalance + badj, "##,###,##0.00"), ref 13);
			txtTotal[3].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ct, "##,###,##0.00"), ref 13);
			txtTotal[4].Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dt, "##,###,##0.00");
			txtTotal[5].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ot, "##,###,##0.00"), ref 13);
			txtTotal[6].Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(sbalance + ct - dt - ot, "##,###,##0.00"), ref 13);

		}
//******************************************************************************
		private void get_BookBalance()
		{
			//******************************************************************************

						DAO.Recordset tblComp = null;
			short m = 0;
			short i = 0;
			short y = 0;

			bbalance = 0;
			tblComp = WinglGlobal.GLdb.OpenRecordset("SELECT FiscalYearEnd FROM Company WHERE CompanyNumber = " + cmp);
			m = 12;
			if (tblComp.RecordCount > 0) {
				if (tblComp.Fields["FiscalYearEnd"].Value >= 1 & tblComp.Fields["FiscalYearEnd"].Value <= 12) {
					m = tblComp.Fields["FiscalYearEnd"].Value;
				}
				tblComp.Close();
			}
			m = m + 1;
			if (m > 12)
				m = 1;
			y = DateAndTime.Year(dtStatement.Value);
			if (m > DateAndTime.Month(dtStatement.Value)) {
				y = y - 1;
			}
			bbalance = WinglGlobal.AcctBal(ref cmp, ref WinglGlobal.GLCashAcct, ref DateAndTime.DateSerial(y, m, 1), ref (dtStatement.Value), ref dpt);
						txtBook[0].CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(bbalance, "##,###,##0.00"), ref 13);
						txtBook[1].CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(badj, "##,###,##0.00"), ref 13);
						txtBook[2].CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(bbalance + badj, "##,###,##0.00"), ref 13);

		}
//******************************************************************************
		private void get_CheckData()
		{
			object tblTrans = null;
			object xsql = null;
			object i = null;
			//******************************************************************************

			short r = 0;
			decimal c = default(decimal);

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			grdChecks.ReDraw = false;
			badj = 0;
			for (i = FixedRows + 1; i <= grdChecks.MaxRows; i++) {
				grdChecks.Row = FixedRows + 1;
				grdChecks.Action = 5;
			}
			grdChecks.MaxRows = FixedRows;

			grdChecks.Row = 0;
			r = FixedRows;
						xsql = "SELECT * FROM Transactions WHERE CompanyNumber = " + cmp + " AND ";
						xsql = xsql + "DepartmentNumber = " + dpt + " AND AccountNumber = " + WinglGlobal.GLCashAcct;
						xsql = xsql + " AND CheckNumber = 0 AND Date <= #" + dtStatement.Value + "# AND RecFlag = 0 ORDER BY Date";
						tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tblTrans.RecordCount > 0) {
								while (!tblTrans.EOF) {
					r = r + 1;
					grdChecks.Row = r;
					grdChecks.MaxRows = r + FixedRows;
					if (FixedRows > 0)
						grdChecks.Action = FPSpread.ActionConstants.ActionInsertRow;
					grdChecks.Row = r;
					grdChecks.Col = 1;
										grdChecks.Text = (tblTrans("Amount") < 0 ? "DEBIT" : "DEPOSIT");
					grdChecks.Col = 2;
										grdChecks.Text = Convert.ToString(tblTrans("Date"));
					grdChecks.Col = 3;
										grdChecks.Value = tblTrans("Amount");
					grdChecks.Col = 4;
										grdChecks.Value = tblTrans("Amount");
					grdChecks.Lock = false;
					grdChecks.Col = 5;
										grdChecks.Value = tblTrans("Description");
					grdChecks.Col = 7;
					grdChecks.CellType = FPSpread.CellTypeConstants.CellTypeEdit;
					grdChecks.Lock = true;
					grdChecks.Col = 8;
										grdChecks.Value = tblTrans("IndexNumber");
					grdChecks.Col = 9;
					grdChecks.CellType = FPSpread.CellTypeConstants.CellTypeInteger;
					grdChecks.Lock = false;
										tblTrans.MoveNext();
				}
								tblTrans.Close();
			}

						xsql = "SELECT ApChecks.*, Vendors.VendorName FROM ApChecks, Vendors WHERE ApChecks.Company = " + lstCompanies.ColList + " AND ";
						xsql = xsql + " Department = " + lstDepartments.ColList + " AND Sequence = 0 AND Date <= #" + dtStatement.Value + "# AND";
						xsql = xsql + " ReconFlag = False AND Vendors.VendorNumber = ApChecks.[Vendor/Account] ORDER BY CheckNumber";
						tblChecks = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblChecks.RecordCount > 0) {
				while (!tblChecks.EOF) {
					c = WinglGlobal.CheckTotal(ref Convert.ToInt32(lstCompanies.ColList), ref Convert.ToInt32(lstDepartments.ColList), ref tblChecks.Fields["CheckNumber"].Value);
					r = r + 1;
					grdChecks.Row = r;
					grdChecks.MaxRows = r + FixedRows;
					if (FixedRows > 0)
						grdChecks.Action = FPSpread.ActionConstants.ActionInsertRow;
					grdChecks.Row = r;
					grdChecks.Col = 1;
					grdChecks.Value = tblChecks.Fields["CheckNumber"].Value;
					grdChecks.Col = 2;
					grdChecks.Text = Convert.ToString(tblChecks.Fields["Date"].Value);
					grdChecks.Col = 3;
					grdChecks.Value = Convert.ToString(c);
					grdChecks.Col = 4;
					grdChecks.Value = Convert.ToString(c);
					grdChecks.Lock = false;
					grdChecks.Col = 5;
					grdChecks.Value = tblChecks.Fields["VendorName"].Value;
					grdChecks.Col = 7;
					grdChecks.CellType = FPSpread.CellTypeConstants.CellTypeEdit;
					grdChecks.Lock = true;
					tblChecks.MoveNext();
				}
				tblChecks.Close();
			}

						txtStatement.CtlText = "0.00";

			grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));

			grdChecks.ReDraw = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;

		}
//******************************************************************************
		private void txtStatement_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

						txtStatement.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Convert.ToDecimal(txtStatement.CtlText), "##,###,##0.00"), ref 13);
						sbalance = Convert.ToDecimal(txtStatement.CtlText);
			grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(3, 1, 1));

		}
//******************************************************************************
		public void Post_Recon()
		{
			object tblBudget = null;
			object tblBalance = null;
			object i = null;
			object xsql = null;
			object s = null;
			//******************************************************************************

			short r = 0;
			object x = null;
			object xc = null;
			int AcctNumber = 0;
			decimal BalAmount = default(decimal);
									DAO.Recordset tblChecks = null;
			DAO.Recordset tblTrans = null;
			int idx = 0;
			decimal orgamt = default(decimal);
						System.DateTime chkDate = default(System.DateTime);
			string ds = null;
			DAO.Recordset tbl = null;

						s = "Ready to post Reconciliation." + Constants.vbCrLf + Constants.vbCrLf;
						s = s + "If you have changed the Statement Amount" + Constants.vbCrLf;
						s = s + "for any check you will be shown an edit screen." + Constants.vbCrLf;
						s = s + "If you do NOT make changes there, the item will" + Constants.vbCrLf;
						s = s + "post as originally booked (the change will be" + Constants.vbCrLf;
						s = s + "discarded." + Constants.vbCrLf + Constants.vbCrLf;
						s = s + "Do you want to continue posting the reconciliation?";
			if (Interaction.MsgBox(s, MsgBoxStyle.YesNo + MsgBoxStyle.Question, this.Name) == MsgBoxResult.No) {
				goto Post_Recon_exit;
			}

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			DAODBEngine_definst.Workspaces(0).BeginTrans();
			 // ERROR: Not supported in C#: OnErrorStatement

			lstCompanies.Col = 1;
			lstDepartments.Col = 1;
			for (r = 1; r <= grdChecks.MaxRows; r++) {
				grdChecks.Row = r;
				grdChecks.Col = 6;
				if (grdChecks.Value != "0") {
					grdChecks.Col = 2;
					chkDate = Convert.ToDateTime(grdChecks.Value);
					grdChecks.Col = 1;
										xc = Strings.Trim(grdChecks.Value);
					grdChecks.Col = 8;
					idx = (Information.IsNumeric(grdChecks.Value) ? grdChecks.Value : 0);
					//************************************
					switch (xc) {
						case "DEPOSIT":
						case "DEBIT":
						case "BnkErr+":
						case "BnkErr-":
							if (idx < 0) {
								tblTrans = WinglGlobal.GLdb.OpenRecordset("Transactions");
								grdChecks.Col = 5;
								ds = grdChecks.Value;
								grdChecks.Col = 4;
																BalAmount = (xc == "DEPOSIT" | xc == "BnkErr+" ? -grdChecks.Value : grdChecks.Value);
								if (!JournalSet) {
																		My.MyProject.Forms.ChkRecJrn.lblInfo.Text = xc + ": " + chkDate + ": " + ds + Constants.vbCrLf + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(BalAmount, "$###,###,##0.00");
									My.MyProject.Forms.ChkRecJrn.ShowDialog();
								}
								// CREDIT SIDE
								tblTrans.AddNew();
								tblTrans.Fields["CompanyNumber"].Value = Conversion.Val(lstCompanies.ColList);
								tblTrans.Fields["DepartmentNumber"].Value = Conversion.Val(lstDepartments.ColList);
								tblTrans.Fields["AccountNumber"].Value = -idx;
								tblTrans.Fields["FiscalYear"].Value = DateAndTime.Year(chkDate);
								tblTrans.Fields["Date"].Value = chkDate;
								tblTrans.Fields["Journal"].Value = chkJournal;
								tblTrans.Fields["Source"].Value = " ";
								tblTrans.Fields["Description"].Value = ds;
								tblTrans.Fields["Amount"].Value = BalAmount;
								tblTrans.Fields["RecFlag"].Value = true;
								tblTrans.Update();
								AcctNumber = -idx;
																//GoSub Post_Balance
								// DEBIT SIDE
								tblTrans.AddNew();
								tblTrans.Fields["CompanyNumber"].Value = Conversion.Val(lstCompanies.ColList);
								tblTrans.Fields["DepartmentNumber"].Value = Conversion.Val(lstDepartments.ColList);
								tblTrans.Fields["AccountNumber"].Value = WinglGlobal.GLCashAcct;
								grdChecks.Col = 2;
								chkDate = Convert.ToDateTime(grdChecks.Value);
								tblTrans.Fields["FiscalYear"].Value = DateAndTime.Year(chkDate);
								tblTrans.Fields["Date"].Value = chkDate;
								tblTrans.Fields["Journal"].Value = chkJournal;
								tblTrans.Fields["Source"].Value = " ";
								grdChecks.Col = 5;
								tblTrans.Fields["Description"].Value = grdChecks.Value;
								grdChecks.Col = 4;
																BalAmount = (xc == "DEPOSIT" | xc == "BnkErr+" ? grdChecks.Value : -grdChecks.Value);
								tblTrans.Fields["Amount"].Value = BalAmount;
								tblTrans.Fields["RecFlag"].Value = true;
								tblTrans.Update();
								tblTrans.Close();
								AcctNumber = WinglGlobal.GLCashAcct;
																//GoSub Post_Balance
							} else {
								grdChecks.Col = 3;
								orgamt = Convert.ToDecimal(grdChecks.Value);
																xsql = "SELECT * FROM Transactions WHERE IndexNumber = " + idx;
																xsql = xsql + " AND CompanyNumber = " + Conversion.Val(lstCompanies.ColList) + " AND Amount = " + orgamt;
								// COULD BE FAILING ON 'AMOUNT' MATCH !!!!!!!!!!!!!!!!!!
																tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
								if (tblTrans.RecordCount > 0) {
									tblTrans.Edit();
									grdChecks.Col = 4;
									BalAmount = Convert.ToDouble(grdChecks.Value) - orgamt;
									tblTrans.Fields["Amount"].Value = grdChecks.Value;
									tblTrans.Fields["RecFlag"].Value = true;
									idx = tblTrans.Fields["AccountNumber"].Value;
									tblTrans.Update();
									if (BalAmount != 0) {
										AcctNumber = idx;
																				//GoSub Post_Balance
										grdChecks.Col = 9;
										idx = (Information.IsNumeric(grdChecks.Value) ? grdChecks.Value : 0);
										if (idx > 0) {
											tbl = WinglGlobal.GLdb.OpenRecordset("Transactions");
											tbl.AddNew();
											for (i = 0; i <= tblTrans.Fields.Count - 1; i++) {
												tbl.Fields[i].Value = tblTrans.Fields[i].Value;
											}
											tbl.Fields["AccountNumber"].Value = idx;
											tbl.Fields["Date"].Value = chkDate;
											//grdChecks.Col = 4
											//BalAmount = grdChecks.Value - orgamt
											tbl.Fields["Amount"].Value = -BalAmount;
											tbl.Fields["RecFlag"].Value = true;
											tbl.Update();
											tbl.Close();
											AcctNumber = idx;
																						//GoSub Post_Balance
										}
									}
								} else {
																											LogError.LogError("RECON: " + xsql, 0, WinglGlobal.sProcID);
								}
								tblTrans.Close();
							}
							break;
						default:
							grdChecks.Col = 3;
														xc = grdChecks.Value;
							grdChecks.Col = 1;
														xsql = "SELECT ReconFlag FROM ApChecks WHERE Company = " + lstCompanies.ColList;
														xsql = xsql + " AND Department = " + lstDepartments.ColList + " AND ";
														xsql = xsql + " CheckNumber = " + grdChecks.Value + " AND Sequence = 0";
														tblChecks = WinglGlobal.GLdb.OpenRecordset(xsql);
							if (tblChecks.RecordCount > 0) {
								tblChecks.Edit();
								tblChecks.Fields["ReconFlag"].Value = true;
								tblChecks.Update();
								tblChecks.Close();
							} else {
																								LogError.LogError("RECON: " + xsql, 0, WinglGlobal.sProcID);
							}
							grdChecks.Col = 4;
														if (xc != grdChecks.Value) {
								grdChecks.Col = 1;
																xc = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(grdChecks.Value, "000000");
																My.MyProject.Forms.CheckRegister.Tag = xc + "(*" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "000") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "0000") + "*)";
								My.MyProject.Forms.CheckRegister.ShowDialog();
							}
							break;
					}
					//***********************************
				}
			}
			DAODBEngine_definst.Workspaces(0).CommitTrans();
			 // ERROR: Not supported in C#: OnErrorStatement

			FileSystem.Kill(WinGL.My.MyProject.Application.Info.DirectoryPath + "\\" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "000_") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtStatement.Value, "yyyymmdd") + "." + WinglGlobal.SPext);
			grdChecks.MaxRows = 0;
						txtBook[0].CtlText = "0.00";
						txtStatement.CtlText = "0.00";
			sbalance = 0;
			grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));
			goto Post_Recon_exit;
			Post_Balance:

			// Account Balances
						xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + lstCompanies.ColList;
						xsql = xsql + " AND DepartmentNumber = " + lstDepartments.ColList + " AND ";
						xsql = xsql + "AccountNumber = " + AcctNumber;
						tblBalance = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tblBalance.RecordCount > 0) {
								tblBalance.edit();
			} else {
				tblBalance = WinglGlobal.GLdb.OpenRecordset("Balances");
								tblBalance.AddNew();
								tblBalance("CompanyNumber") = lstCompanies.ColList;
								tblBalance("DepartmentNumber") = lstDepartments.ColList;
								tblBalance("AccountNumber") = AcctNumber;
			}
			if (chkDate < WinglGlobal.CurrentFiscalYear(ref Convert.ToInt32(lstCompanies.ColList))) {
								x = "P_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(chkDate), "00");
			} else {
								x = "C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(chkDate), "00");
			}
						tblBalance(x) = tblBalance(x) + BalAmount;
						tblBalance.update();
						tblBalance.Close();

			// Budgets
			if (DateAndTime.Year(chkDate) < DateAndTime.Year(DateAndTime.Today)) {
								xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + lstCompanies.ColList;
								xsql = xsql + " AND DepartmentNumber = " + lstDepartments.ColList + " AND ";
								xsql = xsql + "AccountNumber = " + AcctNumber;
								tblBudget = WinglGlobal.GLdb.OpenRecordset(xsql);
								if (tblBudget.RecordCount > 0) {
										tblBudget.edit();
				} else {
					tblBudget = WinglGlobal.GLdb.OpenRecordset("Budgets");
										tblBudget.AddNew();
										tblBudget("CompanyNumber") = lstCompanies.ColList;
										tblBudget("DepartmentNumber") = lstDepartments.ColList;
										tblBudget("AccountNumber") = AcctNumber;
				}
								x = "Actual" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(chkDate), "00");
								tblBudget(x) = tblBudget(x) + BalAmount;
								tblBudget.update();
								tblBudget.Close();
			}
						return;
			Post_Recon_error:

			LogError.MsgBox_Renamed(Err().Description, MsgBoxStyle.Critical, this.Name);
			 // ERROR: Not supported in C#: OnErrorStatement

			DAODBEngine_definst.Workspaces(0).Rollback();
			 // ERROR: Not supported in C#: ResumeStatement

			Post_Recon_exit:

			this.Cursor = System.Windows.Forms.Cursors.Default;
			success = false;

		}
//******************************************************************************
		private void Reload()
		{
			//******************************************************************************
			update_Renamed = true;
			grdChecks.LoadFromFile(dlgSaveOpen.FileName);
			lstCompanies.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
			lstCompanies.ColumnSearch = 1;
						cmp = Conversion.Val(Strings.Left(dlgSaveOpen.FileName, 2));
			lstCompanies.SearchText = Convert.ToString(cmp);
			lstCompanies.Action = LpLib.enumAction.ActionSearch;
			lstCompanies.ListIndex = lstCompanies.SearchIndex;
			lstCompanies.SearchMethod = LpLib.enumSearchMethod.SearchMethodGreaterOrEqual;

			lstDepartments.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
			lstDepartments.ColumnSearch = 1;
						dpt = Conversion.Val(Strings.Mid(dlgSaveOpen.FileName, 3, 3));
			lstDepartments.SearchText = Convert.ToString(dpt);
			lstDepartments.Action = LpLib.enumAction.ActionSearch;
			lstDepartments.ListIndex = lstDepartments.SearchIndex;
			lstDepartments.SearchMethod = LpLib.enumSearchMethod.SearchMethodGreaterOrEqual;
			update_Renamed = false;

						dtStatement.Value = DateAndTime.DateSerial(Convert.ToInt32(Strings.Mid(dlgSaveOpen.FileName, 7, 4)), Convert.ToInt32(Strings.Mid(dlgSaveOpen.FileName, 11, 2)), Convert.ToInt32(Strings.Mid(dlgSaveOpen.FileName, 13, 2)));
			grdChecks.MaxCols = 9;
			grdChecks.Row = 1;
			grdChecks.Col = 9;
						txtStatement.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(grdChecks.Value, "##,###,##0.00"), ref 13);
						sbalance = Convert.ToDecimal(txtStatement.CtlText);

			get_BookBalance();

			grdChecks_ButtonClicked(grdChecks, new AxFPSpread._DSpreadEvents_ButtonClickedEvent(6, 1, 1));

		}
	}
}
