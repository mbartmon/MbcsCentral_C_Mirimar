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
	internal partial class frmOperatingStatement : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.1  2009/08/21 12:36:14  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:07  mike
//* Copied from wingl folder
//*
		bool dirty;
		bool AddEdit;
//UPGRADE_WARNING: Arrays in structure tblOpStmt may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		DAO.Recordset tblOpStmt;
		string rptName;
		public clsOperatingStatement clas;
//******************************************************************************
//UPGRADE_WARNING: Event chkPercent.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkPercent_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			dirty = true;

		}
//******************************************************************************
//UPGRADE_WARNING: Event chkPrior.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkPrior_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			dirty = true;

		}
//******************************************************************************
//UPGRADE_WARNING: Event chkYTD.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkYTD_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			dirty = true;

		}
//******************************************************************************
//UPGRADE_WARNING: Event chkZero.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkZero_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			dirty = true;

		}
//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object r = null;
			object i = null;
			//******************************************************************************

			switch (Index) {

				case 0:
					// PRINT
					save_settings();
					lstCompanies.Row = lstCompanies.ListIndex;
					lstCompanies.Col = 1;
					clas.Company = Conversion.Val(lstCompanies.ColList);
					lstCompanies.Col = 0;
					clas.CompanyName = lstCompanies.ColList;
					clas.Consolidated = chkConsolidated.CheckState;
					clas.StmtName = lstStatement.Text;
					if (!chkConsolidated.CheckState) {
						if (lstDepartments.ListIndex == -1) {
							Interaction.MsgBox("You must select a department when printing a non-consolidated report.", , this.Name);
							return;
						} else {
							lstDepartments.Row = lstDepartments.ListIndex;
							lstDepartments.Col = 1;
							clas.Department = Conversion.Val(lstDepartments.ColList);
							lstDepartments.Col = 0;
							clas.DepartmentName = lstDepartments.ColList;
						}
					}
					for (i = 0; i <= 2; i++) {
						if (optCurrent[i].Checked) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							clas.CurrentQtdYtd = i;
							break; // TODO: might not be correct. Was : Exit For
						}
					}

					for (i = 0; i <= 2; i++) {
						if (optDetail[i].Checked) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							clas.DetailSummaryTotals = i;
							break; // TODO: might not be correct. Was : Exit For
						}
					}

					clas.rptMonth = lstMonth.SelectedIndex + 1;
					clas.rptYear = Conversion.Val(lstYear.Text);

					clas.ShowYTD = chkYTD.CheckState;
					clas.ShowPercentage = chkPercent.CheckState;
					clas.ShowPriorYear = chkPrior.CheckState;
					clas.ShowZero = chkZero.CheckState;
					clas.ReportName = "OpStmt:" + Strings.Trim(lstStatement.Text);
					this.Close();
					break;

				case 1:
					// CANCEL
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					if (dirty) {
						//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						r = Interaction.MsgBox("Save changes?", MsgBoxStyle.YesNoCancel);
						switch (r) {
							case MsgBoxResult.Yes:
								save_settings();
								this.Close();
								break;
							case MsgBoxResult.No:
								this.Close();
								break;
						}
					} else {
						this.Close();
					}
					break;

				case 2:
					// CLEAR
					clear_settings();
					break;

				case 3:
					// NEW
					clear_settings();
					AddEdit = true;
					break;

				case 4:
					// DELETE
					if (Interaction.MsgBox("Delete this Operating Statement?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
						 // ERROR: Not supported in C#: OnErrorStatement

						tblOpStmt.Delete();
						clear_settings();
					}
					break;

			}

		}
//******************************************************************************
		private void cmdExpense_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			//UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
			//Load(AcctSelection)
			My.MyProject.Forms.AcctSelection.AccountSelection(ref "OpStmt", ref (lstStatement.Text), ref 2, ref "001_", ref "005_");
			My.MyProject.Forms.AcctSelection.ShowDialog();
			dirty = true;

		}
//******************************************************************************
		private void cmdIncome_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			//UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
			//Load(AcctSelection)
			My.MyProject.Forms.AcctSelection.AccountSelection("OpStmt", (lstStatement.Text), 1, "004_");
			My.MyProject.Forms.AcctSelection.ShowDialog();
			dirty = true;

		}
//******************************************************************************
		private void frmOperatingStatement_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			short yr = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2));
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2));

			yr = DateAndTime.Year(DateAndTime.Today);
			//Set tblOpStmt = GLdb.OpenRecordset("Control")
			//If tblOpStmt.RecordCount > 0 Then
			//    On Error Resume Next
			//    yr = tblOpStmt("CurrentFiscalYear")
			//    tblOpStmt.Close
			//End If
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr - 5, "0000"));
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr - 4, "0000"));
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr - 3, "0000"));
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr - 2, "0000"));
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr - 1, "0000"));
			lstYear.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr, "0000"));

			WinglGlobal.RecallForm(ref this);
			lstYear.SelectedIndex = lstYear.Items.Count - 1;
			lstMonth.SelectedIndex = DateAndTime.Month(DateAndTime.Today) - 1;

			WinglGlobal.GetCompanies(ref this);

			Err().Clear();
			lstStatement.Items.Clear();
			tblOpStmt = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM OpStmt");
			if (tblOpStmt.RecordCount > 0) {
				if (Err().Number == 0) {
					while (!tblOpStmt.EOF) {
						lstStatement.Items.Add(tblOpStmt.Fields["ReportName"].Value);
						tblOpStmt.MoveNext();
					}
					tblOpStmt.Close();
				}
			}


			if (clas.Company > 0) {
				lstCompanies.SearchText = Convert.ToString(clas.Company);
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				if (lstCompanies.ListIndex > -1) {
					lstCompanies.Col = 1;
					lstCompanies.Row = lstCompanies.ListIndex;
					WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
				}
				chkConsolidated.CheckState = (clas.Consolidated ? 1 : 0);
				if (!clas.Consolidated & clas.Department > 0) {
					lstDepartments.SearchText = Convert.ToString(clas.Department);
					lstDepartments.ColumnSearch = 1;
					lstDepartments.Action = LpLib.enumAction.ActionSearch;
					lstDepartments.ListIndex = lstDepartments.SearchIndex;
				}
				for (i = 0; i <= 2; i++) {
					optCurrent[i].Checked = false;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (clas.CurrentQtdYtd == i)
						optCurrent[i].Checked = true;
					optDetail[i].Checked = false;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (clas.DetailSummaryTotals == i)
						optDetail[i].Checked = true;
				}
				lstMonth.SelectedIndex = clas.rptMonth - 1;
				for (i = 0; i <= lstYear.Items.Count - 1; i++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (Conversion.Val(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstYear, i)) == clas.rptYear) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstYear.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				chkYTD.CheckState = (clas.ShowYTD ? 1 : 0);
				chkPercent.CheckState = (clas.ShowPercentage ? 1 : 0);
				chkPrior.CheckState = (clas.ShowPriorYear ? 1 : 0);
				chkZero.CheckState = (clas.ShowZero ? 1 : 0);
				for (i = 0; i <= lstStatement.Items.Count - 1; i++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (Strings.Mid(clas.ReportName, 8) == Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstStatement, i)) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstStatement.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}

			dirty = false;

		}
//******************************************************************************
		private void frmOperatingStatement_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			//******************************************************************************

			WinglGlobal.StoreForm(ref this);

		}
//******************************************************************************
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			short yr = 0;

			lstCompanies.Row = lstCompanies.ListIndex;
			lstCompanies.Col = 1;
			WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
			lstDepartments.ListIndex = 0;
			//i = IIf(lstYear.ListIndex > -1, lstYear.ListIndex, 0)
			//lstYear.Clear
			//lstYear.AddItem Format(Year(CurrentFiscalYear(lstCompanies.ColList)) - 1, "0000")
			//lstYear.AddItem Format(Year(CurrentFiscalYear(lstCompanies.ColList)), "0000")
			//lstYear.ListIndex = i

		}
//******************************************************************************
//UPGRADE_WARNING: Event optCurrent.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void optCurrent_CheckedChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (eventSender.Checked) {
				short Index = optCurrent.GetIndex(eventSender);
				//******************************************************************************

				dirty = true;

			}
		}
//UPGRADE_WARNING: Event optDetail.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void optDetail_CheckedChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (eventSender.Checked) {
				short Index = optDetail.GetIndex(eventSender);

				dirty = true;

			}
		}
//******************************************************************************
		private void lstStatement_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			object idxIndex = null;
			object fldField = null;
			object defTable = null;
			//******************************************************************************

			 // ERROR: Not supported in C#: OnErrorStatement

			if (string.IsNullOrEmpty(Strings.Trim(lstStatement.Text)))
				goto lstStatement_exit;
			tblOpStmt = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM OpStmt WHERE ReportName = '" + Strings.Trim(lstStatement.Text) + "'");
			if (tblOpStmt.RecordCount > 0) {
				//optDetail(tblOpStmt.Fields("Detail")).Checked = True
				//optCurrent(tblOpStmt.Fields("Current")).Checked = True
				chkPrior.CheckState = tblOpStmt.Fields["Prior"].Value;
				chkPercent.CheckState = tblOpStmt.Fields["Percent"].Value;
				chkYTD.CheckState = tblOpStmt.Fields["YTD"].Value;
				chkZero.CheckState = tblOpStmt.Fields["Zero"].Value;
				AddEdit = false;
			} else {
				lstStatement_continue:
				if (Interaction.MsgBox("No Report with that name.  Create it?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
					AddEdit = true;
				} else {
					return;
				}
			}
			goto lstStatement_exit;
			lstStatement_error:

			if (Err().Number == 3078) {
				// create table
				defTable = WinglGlobal.GLdb.CreateTableDef("OpStmt");
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("ReportName", DAO.DataTypeEnum.dbText, 32);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Detail", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				WinglGlobal.GLdb.TableDefs.Append(defTable);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Current", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Prior", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Percent", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("YTD", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Zero", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);


				// create index
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex = defTable.CreateIndex("PrimaryKey");
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Unique. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Unique = true;
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Primary. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Primary = true;
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = idxIndex.CreateField("ReportName");
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Indexes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Indexes.Append(idxIndex);

				tblOpStmt = WinglGlobal.GLdb.OpenRecordset("OpStmt");
				dirty = true;
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
				 // ERROR: Not supported in C#: ResumeStatement

			}
			lstStatement_exit:


		}
//******************************************************************************
		private void save_settings()
		{
			object i = null;
			//******************************************************************************

			if (AddEdit) {
				tblOpStmt.AddNew();
				tblOpStmt.Fields["ReportName"].Value = Strings.Trim(lstStatement.Text);
			} else {
				tblOpStmt.Edit();
			}
			for (i = 0; i <= 2; i++) {
				if (optDetail[i].Checked) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblOpStmt.Fields["Detail"].Value = i;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			for (i = 0; i <= 2; i++) {
				if (optCurrent[i].Checked) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblOpStmt.Fields["Current"].Value = i;
				}
			}
			tblOpStmt.Fields["Prior"].Value = chkPrior.CheckState;
			tblOpStmt.Fields["Percent"].Value = chkPercent.CheckState;
			tblOpStmt.Fields["YTD"].Value = chkYTD.CheckState;
			tblOpStmt.Fields["Zero"].Value = chkZero.CheckState;

			tblOpStmt.Update();

		}
//******************************************************************************
		private void clear_settings()
		{
			//******************************************************************************

			lstStatement.SelectedIndex = -1;
			frmOperatingStatement_Load(this, new System.EventArgs());
			dirty = false;

		}
	}
}
