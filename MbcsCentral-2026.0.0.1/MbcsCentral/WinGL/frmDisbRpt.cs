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
	internal partial class frmDisbRpt : System.Windows.Forms.Form
	{
		public clsDisbRpt clas;
//UPGRADE_WARNING: Arrays in structure DisbRptRec may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		DisbRpt_type DisbRptRec;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object c = null;
			object r = null;

			short c1 = 0;
			short i = 0;
			short c2 = 0;

			switch (Index) {

				case 0:
					// PRINT
					i = lstCompanies.ListIndex;
					if (i < 0) {
						Interaction.MsgBox("You must select a Company to print.", , "Disbursement Report");
						return;
					}
					lstCompanies.Row = i;
					lstCompanies.Col = 0;
					clas.CompanyName = lstCompanies.ColList;
					lstCompanies.Col = 1;
					clas.Company = Conversion.Val(lstCompanies.ColList);

					i = lstDepartments.ListIndex;
					if (i < 0) {
						Interaction.MsgBox("You must select a Department to print.", , "Disbursement Report");
						return;
					}
					lstDepartments.Row = i;
					lstDepartments.Col = 0;
					clas.DepartmentName = lstDepartments.ColList;
					lstDepartments.Col = 1;
					clas.Department = Conversion.Val(lstDepartments.ColList);

					i = lstBalAccount.ListIndex;
					if (i < 0) {
						Interaction.MsgBox("You must select a Balance Account to print.", , "Disbursement Report");
						return;
					}
					lstBalAccount.Row = i;
					lstBalAccount.Col = 1;
					clas.BalanceAccount = Conversion.Val(lstBalAccount.ColList);

					c1 = lstColAcct1.ListIndex;
					c2 = lstColAcct2.ListIndex;
					if (c1 < 0 & c2 < 0) {
						Interaction.MsgBox("You must select at least one Collection Account to print.", , "Disbursement Report");
						return;
					}
					lstColAcct1.Row = c1;
					lstColAcct1.Col = 1;
					clas.CollectionAccount1 = Conversion.Val(lstColAcct1.ColList);
					lstColAcct2.Row = c2;
					lstColAcct2.Col = 1;
					clas.CollectionAccount2 = Conversion.Val(lstColAcct2.ColList);

					i = 1;
					for (r = 0; r <= 1; r++) {
						for (c = 0; c <= 4; c++) {
							//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							grdJournals.TargetCell(c, r);
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							WinglGlobal.Journals[i] = grdJournals.CtlText;
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							DisbRptRec.jrns(i) = grdJournals.CtlText;
							i = i + 1;
						}
					}


					clas.rptMonth = lstMonth.SelectedIndex + 1;
					clas.rptYear = chkYear.CheckState;

					lstBalAccount.Row = lstBalAccount.ListIndex;
					lstBalAccount.Col = 1;
					DisbRptRec.balacct = "*" + Strings.Trim(lstBalAccount.ColList);
					lstColAcct1.Row = lstColAcct1.ListIndex;
					lstColAcct1.Col = 1;
					DisbRptRec.colacct1 = "*" + Strings.Trim(lstColAcct1.ColList);
					lstColAcct2.Row = lstColAcct2.ListIndex;
					lstColAcct2.Col = 1;
					DisbRptRec.colacct2 = "*" + Strings.Trim(lstColAcct2.ColList);
					break;

				case 1:
					// CANCEL
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();

		}
		private void frmDisbRpt_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object c = null;
			object r = null;
			object i = null;
			object xsql = null;

			//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short fi = 0;
			DAO.Recordset tblacct = null;

			fi = FreeFile();
			FileSystem.FileOpen(fi, WinglGlobal.appath + "gldsbrpt.dat", OpenMode.Random);
			//UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileSystem.FileGet(1, DisbRptRec);
			FileSystem.FileClose(fi);

			 // ERROR: Not supported in C#: OnErrorStatement

			if (Strings.Left(DisbRptRec.balacct, 1) != "*") {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = "SELECT Key FROM Accounts WHERE OldAccountNumber = '" + Strings.Left(DisbRptRec.balacct, 4);
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + "' AND OldSubNumber = '" + Strings.Right(DisbRptRec.balacct, 3) + "'";
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tblacct = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblacct.RecordCount > 0) {
					DisbRptRec.balacct = "*" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblacct.Fields["Key"].Value, "######");
					tblacct.Close();
				}
			}

			if (Strings.Left(DisbRptRec.colacct1, 1) != "*") {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = "SELECT Key FROM Accounts WHERE OldAccountNumber = '" + Strings.Left(DisbRptRec.colacct1, 4);
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + "' AND OldSubNumber = '" + Strings.Right(DisbRptRec.colacct1, 3) + "'";
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tblacct = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblacct.RecordCount > 0) {
					DisbRptRec.colacct1 = "*" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblacct.Fields["Key"].Value, "######");
					tblacct.Close();
				}
			}

			if (Strings.Left(DisbRptRec.colacct2, 1) != "*") {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = "SELECT Key FROM Accounts WHERE OldAccountNumber = '" + Strings.Left(DisbRptRec.colacct2, 4);
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + "' AND OldSubNumber = '" + Strings.Right(DisbRptRec.colacct2, 3) + "'";
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tblacct = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblacct.RecordCount > 0) {
					DisbRptRec.colacct2 = "*" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblacct.Fields["Key"].Value, "######");
					tblacct.Close();
				}
			}

			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(lstBalAccount, 0, "001_");
			WinglGlobal.getAccounts(lstColAcct1, 0, "004_");
			WinglGlobal.getAccounts(lstColAcct2, 0, "004_");

			if (Strings.Left(DisbRptRec.balacct, 1) == "*") {
				lstBalAccount.SearchText = Strings.Trim(Strings.Mid(DisbRptRec.balacct, 2));
				lstBalAccount.ColumnSearch = 1;
				lstBalAccount.Action = LpLib.enumAction.ActionSearch;
				if (lstBalAccount.SearchIndex >= 0) {
					lstBalAccount.ListIndex = lstBalAccount.SearchIndex;
				}
			}

			if (Strings.Left(DisbRptRec.colacct1, 1) == "*") {
				lstColAcct1.SearchText = Strings.Trim(Strings.Mid(DisbRptRec.colacct1, 2));
				lstColAcct1.ColumnSearch = 1;
				lstColAcct1.Action = LpLib.enumAction.ActionSearch;
				if (lstColAcct1.SearchIndex >= 0) {
					lstColAcct1.ListIndex = lstColAcct1.SearchIndex;
				}
			}

			if (Strings.Left(DisbRptRec.colacct2, 1) == "*") {
				lstColAcct2.SearchText = Strings.Trim(Strings.Mid(DisbRptRec.colacct2, 2));
				lstColAcct2.ColumnSearch = 1;
				lstColAcct2.Action = LpLib.enumAction.ActionSearch;
				if (lstColAcct2.SearchIndex >= 0) {
					lstColAcct2.ListIndex = lstColAcct2.SearchIndex;
				}
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = 1;
			for (r = 0; r <= 1; r++) {
				for (c = 0; c <= 4; c++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					grdJournals.TargetCell(c, r);
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					grdJournals.CtlText = DisbRptRec.jrns(i);
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					i = i + 1;
				}
			}

			lstMonth.SelectedIndex = DateAndTime.Month(DateAndTime.Now) - 1;

			if (clas.Company > 0) {
				lstCompanies.SearchText = Convert.ToString(clas.Company);
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				if (clas.Department > 0) {
					lstDepartments.SearchText = Convert.ToString(clas.Department);
					lstDepartments.ColumnSearch = 1;
					lstDepartments.Action = LpLib.enumAction.ActionSearch;
					lstDepartments.ListIndex = lstDepartments.SearchIndex;
				}
				lstMonth.SelectedIndex = clas.rptMonth - 1;
				chkYear.CheckState = (clas.rptYear ? 1 : 0);
			}
		}
		private void frmDisbRpt_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			object fi = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object fi. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			fi = FreeFile();
			//UPGRADE_WARNING: Couldn't resolve default property of object fi. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			FileSystem.FileOpen(fi, WinglGlobal.appath + "gldsbrpt.dat", OpenMode.Random);
			//UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileSystem.FilePut(1, DisbRptRec);
			//UPGRADE_WARNING: Couldn't resolve default property of object fi. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			FileSystem.FileClose(fi);

		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			lstCompanies.Row = lstCompanies.ListIndex;
			lstCompanies.Col = 1;
			WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
			lstDepartments.ListIndex = 0;

		}
	}
}
