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
	internal partial class BudgetMaint : System.Windows.Forms.Form
	{
		short numSelected;
		int c;
		int a;
		int d;
		short gCol;
		short gRow;
//******************************************************************************
		private void Cmd1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd1.GetIndex(eventSender);
			//******************************************************************************

			short pct = 0;
			string[] msg = new string[3];

			msg[1] = "ALL DEPARTMENTS";
			msg[0] = "SELECTED DEPARTMENT";
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			pct = Conversion.Val(txtIncrease.CtlText) * 10;
			if (pct == 0) {
				LogError.MsgBox_Renamed("Increase percentage must be non-zero", , this.Name);
			} else {
				if (LogError.MsgBox_Renamed("Change " + msg[Index] + " by " + pct / 10 + "%", MsgBoxStyle.YesNo, this.Name) == MsgBoxResult.No) {
					return;
				}
			}
			switch (Index) {
				case 0:
					// SELECTED DEPARTMENT
					grdCompDept.Col = 3;
					c = Convert.ToInt32(grdCompDept.Value);
					grdCompDept.Col = 4;
					d = Convert.ToInt32(grdCompDept.Value);
					ChangeDepartments(ref pct);
					break;
				case 1:
					// ALL DEPARTMENTS
					c = -1;
					d = -1;
					ChangeDepartments(ref pct);
					break;
			}

		}
//******************************************************************************
		private void Cmd2_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd2.GetIndex(eventSender);
			//******************************************************************************

			switch (Index) {
				case 0:
					// Edit/Save
					if (grdCompDept.Visible) {
						grdCompDept_DblClick(grdCompDept, new AxFPSpread._DSpreadEvents_DblClickEvent(grdCompDept.Col, grdCompDept.Row));
					} else {
						SaveBudget();
						grdBudgets.Visible = false;
						Cmd2[0].Text = "EDIT BUDGETS for Selected Company/Department";
						Cmd2[1].Text = "EXIT";
						grdCompDept.Visible = true;
						Frame1.Visible = true;
					}
					break;
				case 1:
					// Exit
					if (grdCompDept.Visible) {
						this.Close();
					} else {
						if (grdBudgets.Visible) {
							grdBudgets.Visible = false;
							Cmd2[0].Text = "EDIT BUDGETS for Selected Company/Department";
							Cmd2[1].Text = "EXIT";
							grdCompDept.Visible = true;
							Frame1.Visible = true;
						}
					}
					break;
			}

		}
//******************************************************************************
		private void Cmd3_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd3.GetIndex(eventSender);
			//******************************************************************************

			decimal amt = default(decimal);
			short i = 0;

			gCol = grdBudgets.Col;
			gRow = grdBudgets.Row;
			grdBudgets.Col = 3;
			switch (Index) {
				case 0:
					// copy
					amt = Convert.ToDecimal(grdBudgets.Value);
					for (i = 4; i <= 14; i++) {
						grdBudgets.Col = i;
						grdBudgets.Value = Convert.ToString(amt);
					}

					break;
				case 1:
					// divide
					amt = Convert.ToDouble(grdBudgets.Value) / 12;
					for (i = 3; i <= 14; i++) {
						grdBudgets.Col = i;
						grdBudgets.Value = Convert.ToString(amt);
					}

					break;
			}
			grdBudgets.Focus();

		}
//******************************************************************************
		private void BudgetMaint_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object g = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tblDept may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblComp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblComp = null;
			DAO.Recordset tblDept = null;
			short r = 0;
			bool ClrFlag = false;
			int BckClr = 0;

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop + 50);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6390);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(9370);
			gCol = 1;
			gRow = 1;

			tblComp = WinglGlobal.GLdb.OpenRecordset("SELECT CompanyNumber, Name FROM Company ORDER BY Name");
			if (tblComp.RecordCount > 0) {
				grdCompDept.Col = -1;
				grdCompDept.Row = -1;
				grdCompDept.BackColor = System.Drawing.ColorTranslator.FromOle(0xffffc0);
				r = 0;
				while (!tblComp.EOF) {
					r = r + 1;
					grdCompDept.MaxRows = r;
					grdCompDept.Row = r;
					grdCompDept.Col = 1;
					grdCompDept.Text = tblComp.Fields["Name"].Value;
					grdCompDept.Col = 3;
					grdCompDept.Value = tblComp.Fields["CompanyNumber"].Value;
					if (ClrFlag) {
						BckClr = 0xffffff;
						ClrFlag = false;
					} else {
						BckClr = 0xc0c000;
						ClrFlag = true;
					}
					grdCompDept.Col = -1;
					//grdCompDept.BackColor = BckClr
					tblDept = WinglGlobal.GLdb.OpenRecordset("SELECT [Department Number], Name FROM Department WHERE [Company Number] = " + tblComp.Fields["CompanyNumber"].Value + " ORDER BY Name");
					if (tblDept.RecordCount > 0) {
						while (!tblDept.EOF) {
							grdCompDept.MaxRows = r;
							grdCompDept.Row = r;
							grdCompDept.Col = -1;
							//grdCompDept.BackColor = BckClr
							grdCompDept.Col = 2;
							grdCompDept.Text = tblDept.Fields["Name"].Value;
							for (g = 2; g <= 4; g++) {
								//UPGRADE_WARNING: Couldn't resolve default property of object g. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								grdCompDept.Col = g;
								grdCompDept.CellBorderColor = System.Drawing.ColorTranslator.FromOle(0);
								grdCompDept.CellBorderStyle = FPSpread.CellBorderStyleConstants.CellBorderStyleFineSolid;
								grdCompDept.CellBorderType = 8;
								grdCompDept.Action = FPSpread.ActionConstants.ActionSetCellBorder;
							}
							grdCompDept.Col = 1;
							grdCompDept.Text = tblComp.Fields["Name"].Value;
							grdCompDept.Col = 3;
							grdCompDept.Value = tblComp.Fields["CompanyNumber"].Value;
							grdCompDept.Col = 4;
							grdCompDept.Value = tblDept.Fields["Department Number"].Value;
							tblDept.MoveNext();
							r = r + 1;
						}
						r = r - 1;
						tblDept.Close();
						grdCompDept.Col = -1;
						grdCompDept.CellBorderType = 8;
						grdCompDept.CellBorderStyle = FPSpread.CellBorderStyleConstants.CellBorderStyleSolid;
						grdCompDept.CellBorderColor = System.Drawing.ColorTranslator.FromOle(0x80000012);
						grdCompDept.Action = FPSpread.ActionConstants.ActionSetCellBorder;
					}
					tblComp.MoveNext();
				}
				tblComp.Close();
				WinglGlobal.getLedgerStructure();
			}

		}
//******************************************************************************
//UPGRADE_WARNING: Event BudgetMaint.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void BudgetMaint_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			grdCompDept.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1770);
			grdBudgets.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1770);
			grdBudgets.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - 220);
			Frame1.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCompDept.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCompDept.Height) + 50);
			Cmd3[0].Top = Frame1.Top;
			Cmd3[1].Top = Frame1.Top;

		}
//******************************************************************************
		private void BudgetMaint_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			//******************************************************************************

			WinglGlobal.LH = null;

		}

		private void grdAccounts_ButtonClicked(System.Object eventSender, AxFPSpread._DSpreadEvents_ButtonClickedEvent eventArgs)
		{

			grdAccounts.Visible = false;
			grdBudgets.Visible = true;

		}
//******************************************************************************
		private void grdBudgets_ClickEvent(System.Object eventSender, AxFPSpread._DSpreadEvents_ClickEvent eventArgs)
		{
			//******************************************************************************

			short c = 0;
			short r = 0;

			gCol = grdBudgets.Col;
			gRow = grdBudgets.Row;
			//For c = Col To grdBudgets.MaxCols
			//For r = Row To grdBudgets.MaxRows
			//grdBudgets.Col = c
			//grdBudgets.Row = r
			//If Not grdBudgets.Lock Then
			//grdBudgets.Action = FPSpread.ActionConstants.ActionActiveCell
			//Exit Sub
			//End If
			//Next
			//Next 

		}
//******************************************************************************
		private void grdBudgets_Enter(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			grdBudgets_ClickEvent(grdBudgets, new AxFPSpread._DSpreadEvents_ClickEvent(gCol, gRow));

		}
		private void grdBudgets_LeaveCell(System.Object eventSender, AxFPSpread._DSpreadEvents_LeaveCellEvent eventArgs)
		{

			if (eventArgs.newRow == -1 | eventArgs.newCol == -1)
				return;
			grdBudgets.Row = eventArgs.newRow;
			grdBudgets.Col = eventArgs.newCol;
			if (grdBudgets.Lock) {
				grdBudgets_ClickEvent(grdBudgets, new AxFPSpread._DSpreadEvents_ClickEvent(eventArgs.newCol, eventArgs.newRow));
			}

		}
		private void grdCompDept_ClickEvent(System.Object eventSender, AxFPSpread._DSpreadEvents_ClickEvent eventArgs)
		{

			//grdCompDept.Row = Row
			//grdCompDept.Col = Col

		}
//******************************************************************************
		private void grdCompDept_DblClick(System.Object eventSender, AxFPSpread._DSpreadEvents_DblClickEvent eventArgs)
		{
			//******************************************************************************

			//grdCompDept.Row = Row
			grdCompDept.Col = 3;
			c = Convert.ToInt32(grdCompDept.Value);
			grdCompDept.Col = 4;
			d = Convert.ToInt32(grdCompDept.Value);
			grdCompDept.Col = 1;
			grdCompDept.Col = 2;
			grdCompDept.Visible = false;
			Frame1.Visible = false;
			grdBudgets.Visible = true;
			Cmd2[0].Text = "SAVE Budget Changes";
			Cmd2[1].Text = "BACK";
			FillInAccounts();

		}
//******************************************************************************
		private void grdCompDept_Enter(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			grdCompDept.Row = 1;
			grdCompDept.Col = 3;
			grdCompDept.Action = FPSpread.ActionConstants.ActionActiveCell;

		}
//******************************************************************************
		private void ClearAll()
		{
			//******************************************************************************

			short i = 0;

			for (i = 1; i <= grdCompDept.MaxRows; i++) {
				grdCompDept.Row = i;
				grdCompDept.Col = 3;
				grdCompDept.Value = Convert.ToString(false);
			}

		}
//******************************************************************************
		private void FillInAccounts()
		{
			object xsql = null;
			object g = null;
			object k = null;
			object j = null;
			object i = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tblBudget may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short r = 0;
			DAO.Recordset tblBudget = null;
			bool ClrFlag = false;
			int BckClr = 0;
			int w = 0;
			short rowCnt = 0;
			bool catFlag = false;

			grdBudgets.ReDraw = false;
			grdCompDept.Col = 1;
			Label2.Text = "Company: " + Strings.Trim(grdCompDept.Text);
			grdCompDept.Col = 2;
			Label2.Text = Label2.Text + Constants.vbCrLf + "Department: " + grdCompDept.Text;
			r = 0;
			for (i = 1; i <= 5; i++) {
				for (j = 0; j <= 10; j++) {
					for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						// Trim(noNulls(LH(i, j, k).Description)) > "" Then
						if (0) {
							r = r + 1;
							grdBudgets.MaxRows = r;
							grdBudgets.Row = r;
							grdBudgets.Col = 15;
							grdBudgets.Value = Convert.ToString(0);
							grdBudgets.Col = 1;
							//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							switch (0) {
								//Trim(LH(i, j, k).type)
								case "C":
									// Major Category
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									if (i > 1 & rowCnt == 0) {
										r = r - 1;
										grdBudgets.MaxRows = r;
									}
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//grdBudgets.Text = LH(i, j, k).Description
									if (ClrFlag) {
										BckClr = 0xffffc0;
										ClrFlag = false;
									} else {
										BckClr = 0xffff00;
										ClrFlag = true;
									}
									grdBudgets.Col = 5;
									grdBudgets.CellType = FPSpread.CellTypeConstants.CellTypeEdit;
									grdBudgets.Col = -1;
									grdBudgets.BackColor = System.Drawing.ColorTranslator.FromOle(BckClr);
									grdBudgets.Lock = true;
									rowCnt = 0;
									catFlag = true;
									break;
								case "S":
									// Sub Category
									if (rowCnt == 0 & !catFlag) {
										r = r - 1;
										grdBudgets.MaxRows = r;
									}
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//grdBudgets.Text = "   " & LH(i, j, k).Description
									grdBudgets.Col = 5;
									grdBudgets.CellType = FPSpread.CellTypeConstants.CellTypeEdit;
									grdBudgets.Col = -1;
									grdBudgets.BackColor = System.Drawing.ColorTranslator.FromOle(BckClr);
									grdBudgets.Lock = true;
									grdBudgets.CellBorderStyle = FPSpread.CellBorderStyleConstants.CellBorderStyleSolid;
									grdBudgets.CellBorderColor = System.Drawing.ColorTranslator.FromOle(0x0);
									grdBudgets.CellBorderType = 4;
									//grdBudgets.Action = ActionSetCellBorder
									rowCnt = 0;
									catFlag = false;
									break;
								case "A":
									// Account
									grdBudgets.Col = 2;
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//grdBudgets.Text = LH(i, j, k).Description
									grdBudgets.Col = 15;
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//grdBudgets.Value = LH(i, j, k).key
									grdBudgets.Col = -1;
									grdBudgets.BackColor = System.Drawing.ColorTranslator.FromOle(BckClr);
									for (g = 2; g <= 5; g++) {
										//UPGRADE_WARNING: Couldn't resolve default property of object g. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
										grdBudgets.Col = g;
										grdBudgets.CellBorderStyle = FPSpread.CellBorderStyleConstants.CellBorderStyleSolid;
										grdBudgets.CellBorderColor = System.Drawing.ColorTranslator.FromOle(0x0);
										grdBudgets.CellBorderType = 8;
										grdBudgets.Action = FPSpread.ActionConstants.ActionSetCellBorder;
										grdBudgets.CellBorderType = 4;
										grdBudgets.Action = FPSpread.ActionConstants.ActionSetCellBorder;
									}

									//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + c + " AND ";
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//xsql = xsql & "DepartmentNumber = " & d & " AND AccountNumber = " & LH(i, j, k).key
									//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									tblBudget = WinglGlobal.GLdb.OpenRecordset(xsql);
									for (g = 1; g <= 12; g++) {
										//UPGRADE_WARNING: Couldn't resolve default property of object g. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
										grdBudgets.Col = g + 2;
										grdBudgets.Value = Convert.ToString(0);
										if (tblBudget.RecordCount > 0) {
											//UPGRADE_WARNING: Couldn't resolve default property of object g. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
											grdBudgets.Value = tblBudget.Fields["Budget" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(g, "00")].Value;
										}
										grdBudgets.CellBorderColor = System.Drawing.ColorTranslator.FromOle(0);
										grdBudgets.CellBorderStyle = FPSpread.CellBorderStyleConstants.CellBorderStyleSolid;
										grdBudgets.CellBorderType = 16;
										grdBudgets.Action = FPSpread.ActionConstants.ActionSetCellBorder;
									}

									if (tblBudget.RecordCount > 0)
										tblBudget.Close();
									rowCnt = rowCnt + 1;
									catFlag = false;
									break;
							}
						}
					}
				}
			}
			grdBudgets.Row = grdBudgets.MaxRows;
			grdBudgets.Col = 1;
			if (!string.IsNullOrEmpty(Strings.Trim(grdBudgets.Text))) {
				grdBudgets.MaxRows = grdBudgets.MaxRows - 1;
			}
			grdBudgets.ReDraw = true;
			gCol = 1;
			gRow = 1;
			grdBudgets.Focus();

		}
//******************************************************************************
		private void SaveBudget()
		{
			object xsql = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tblBudget may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short r = 0;
			short cl = 0;
			DAO.Recordset tblBudget = null;

			for (r = 1; r <= grdBudgets.MaxRows; r++) {
				grdBudgets.Row = r;
				grdBudgets.Col = 3;
				if (!grdBudgets.Lock) {
					grdBudgets.Col = 15;
					a = Convert.ToInt32(grdBudgets.Value);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + c + " AND ";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + "DepartmentNumber = " + d + " AND AccountNumber = " + a;
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblBudget = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tblBudget.RecordCount > 0) {
						tblBudget.Edit();
					} else {
						tblBudget = WinglGlobal.GLdb.OpenRecordset("Budgets");
						tblBudget.AddNew();
						tblBudget.Fields["CompanyNumber"].Value = c;
						tblBudget.Fields["DepartmentNumber"].Value = d;
						tblBudget.Fields["AccountNumber"].Value = a;
					}
					for (cl = 3; cl <= 14; cl++) {
						grdBudgets.Col = cl;
						tblBudget.Fields["Budget" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cl - 2, "00")].Value = grdBudgets.Value;
					}
					tblBudget.Update();
					tblBudget.Close();
				}
			}

		}
//******************************************************************************
		private void ChangeDepartments(ref short pct)
		{
			object x = null;
			object i = null;
			object xsql = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tblBudget may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblBudget = null;

			if (pct == 0 | System.Math.Abs(pct) > 1000)
				goto ChangeDepartments_exit;

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Budgets";
			if (c != -1) {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + " WHERE CompanyNumber = " + c + " AND DepartmentNumber = " + d;
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblBudget = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblBudget.RecordCount > 0) {
				while (!tblBudget.EOF) {
					tblBudget.Edit();
					for (i = 1; i <= 12; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = "Budget" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00");
						tblBudget.Fields[x].Value = tblBudget.Fields[x].Value + (tblBudget.Fields[x].Value * (pct / 1000));
					}
					tblBudget.Update();
					tblBudget.MoveNext();
				}
			}
			tblBudget.Close();
			goto ChangeDepartments_exit;
			ChangeDepartments_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, this.Name);
			 // ERROR: Not supported in C#: ResumeStatement

			ChangeDepartments_exit:


		}
	}
}
