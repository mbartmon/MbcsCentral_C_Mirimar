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
	internal class clsCheckRecon
	{
//* $Header$
//* $Log$
//* Revision 1.1  2009/08/21 12:36:11  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:07  mike
//* Copied from wingl folder
//*
//* 
//*   Auto-added
//* 
//* Revision 2.17  by: normal  Rev date: 1/24/2003 10:27:04 AM
//*   Check Reconciliation - Bank Error Handling
//* 
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:33:40 AM
//*   Initial revision.
//* 
//* $Endlog$
//* $Endlog$
		private clsReport rpt;
		private short pg;
		private short ln;
		private CheckRecon frm;
		private bool cleared;

//local variable(s) to hold property value(s)
			//local copy
		private int mvarCompany;
			//local copy
		private int mvarDepartment;
			//local copy
		private bool mvarCancel;
			//local copy
		private string mvarCompanyName;
			//local copy
		private string mvarDepartmentName;
			//local copy
		private System.DateTime mvarStatementDate;
		public System.DateTime StatementDate {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.StatementDate
			get { return mvarStatementDate; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.StatementDate = 5
			set { mvarStatementDate = value; }
		}
		public string DepartmentName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.DepartmentName
			get { return mvarDepartmentName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.DepartmentName = 5
			set { mvarDepartmentName = value; }
		}
		public string CompanyName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CompanyName
			get { return mvarCompanyName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.CompanyName = 5
			set { mvarCompanyName = value; }
		}
		public bool Cancel {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Cancel
			get { return mvarCancel; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Cancel = 5
			set { mvarCancel = value; }
		}
		public int Department {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Department
			get { return mvarDepartment; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Department = 5
			set { mvarDepartment = value; }
		}
		public int Company {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Company
			get { return mvarCompany; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Company = 5
			set { mvarCompany = value; }
		}
//******************************************************************************
		public void print_report()
		{
			//******************************************************************************

			decimal dt = default(decimal);
			decimal aT = default(decimal);
			decimal ot = default(decimal);
			decimal ct = default(decimal);
			decimal gt = default(decimal);
			decimal ft = default(decimal);
			decimal adj = default(decimal);
			short i = 0;
			short j = 0;
			bool bnkheader = false;

			pg = 0;
			aT = 0;
			ot = 0;
			dt = 0;
			ct = 0;
			ft = 0;
			gt = 0;
			adj = 0;
			cleared = false;
			bnkheader = false;
			heading();
			rpt.FontBold(ref true);
			rpt.Send();
			for (i = 1; i <= frm.grdChecks.MaxRows; i++) {
				frm.grdChecks.Row = i;
				frm.grdChecks.Col = 6;
				if (frm.grdChecks.Value == "0") {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value == "BnkErr+") {
						if (!bnkheader) {
							rpt.Send("tab(9)", "Bank Errors In Your Favor");
							rpt.Send();
							rpt.FontBold(ref false);
							rpt.Send("tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount");
							rpt.Send("tab(6)", "-------- ------- -------------------------- -----------");
							ln = ln + 4;
							bnkheader = true;
						}
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(15)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						ft = ft + Convert.ToDouble(frm.grdChecks.Value);
						ln = ln + 1;
					}
				} else {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value == "BnkErr+") {
						if (!bnkheader) {
							rpt.Send("tab(9)", "Bank Errors In Your Favor");
							rpt.Send();
							rpt.FontBold(ref false);
							rpt.Send("tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount");
							rpt.Send("tab(6)", "-------- ------- -------------------------- -----------");
							ln = ln + 4;
							bnkheader = true;
						}
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(15)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						ft = ft + Convert.ToDouble(frm.grdChecks.Value);
						ln = ln + 1;
					}
					frm.grdChecks.Col = 7;
					if (frm.grdChecks.TypeButtonText == "DEL") {
						frm.grdChecks.Col = 1;
						if (frm.grdChecks.Value == "DEPOSIT") {
							frm.grdChecks.Col = 4;
							adj = adj + Convert.ToDouble(frm.grdChecks.Value);
						} else if (frm.grdChecks.Value == "DEBIT") {
							frm.grdChecks.Col = 4;
							adj = adj - Convert.ToDouble(frm.grdChecks.Value);
						}
					}
				}
			}
			if (bnkheader) {
				rpt.Send("tab(42)", "Total=>", "tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ft, "#######0.00"), ref 11));
				rpt.Send();
				ln = ln + 2;
			}
			rpt.FontBold(ref true);
			rpt.Send();
			bnkheader = false;
			for (i = 1; i <= frm.grdChecks.MaxRows; i++) {
				frm.grdChecks.Row = i;
				frm.grdChecks.Col = 6;
				//If frm.grdChecks.Value = "0" Then
				frm.grdChecks.Col = 1;
				if (frm.grdChecks.Value == "BnkErr-") {
					if (!bnkheader) {
						rpt.Send("tab(9)", "Bank Errors In Bank's Favor");
						rpt.Send();
						rpt.FontBold(ref false);
						rpt.Send("tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount");
						rpt.Send("tab(6)", "-------- ------- -------------------------- -----------");
						ln = ln + 4;
						bnkheader = true;
					}
					if (ln > rpt.PageSize - 12) {
						rpt.NewPage();
						heading();
					}
					frm.grdChecks.Col = 2;
					rpt.Send("tab(6)", frm.grdChecks.Value, ";");
					frm.grdChecks.Col = 5;
					rpt.Send("tab(15)", frm.grdChecks.Value, ";");
					frm.grdChecks.Col = 4;
					rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
					gt = gt + Convert.ToDouble(frm.grdChecks.Value);
					ln = ln + 1;
				}
				//End If
			}
			if (bnkheader) {
				rpt.Send("tab(42)", "Total=>", "tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(gt, "#######0.00"), ref 11));
				rpt.Send();
				ln = ln + 2;
			}
			rpt.FontBold(ref true);
			rpt.Send("tab(9)", "Outstanding Deposits & Credits");
			rpt.FontBold(ref false);
			rpt.Send();
			rpt.Send("tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount");
			rpt.Send("tab(6)", "-------- ------- -------------------------- -----------");
			ln = ln + 4;
			for (i = 1; i <= frm.grdChecks.MaxRows; i++) {
				frm.grdChecks.Row = i;
				frm.grdChecks.Col = 6;
				if (frm.grdChecks.Value == "0") {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value == "DEPOSIT") {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(15)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						aT = aT + Convert.ToDouble(frm.grdChecks.Value);
						ln = ln + 1;
					}
				}
			}
			rpt.Send("tab(42)", "Total=>", "tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(aT, "#######0.00"), ref 11));
			rpt.Send();
			rpt.FontBold(ref true);
			rpt.FontBold(ref false);
			rpt.Send();
			rpt.FontBold(ref true);
			rpt.Send("tab(9)", "Outstanding Checks & Debits");
			rpt.FontBold(ref false);
			rpt.Send();
			rpt.Send("tab(6)", "  Date", "tab(18)", "Chk#", "tab(23)", "Description", "tab(55)", "Amount");
			rpt.Send("tab(6)", "-------- ------- -------------------------- -----------");
			ln = ln + 7;

			ot = 0;
			for (j = 1; j <= frm.grdChecks.MaxRows; j++) {
				frm.grdChecks.Row = j;
				frm.grdChecks.Col = 6;
				if (frm.grdChecks.Value == "0") {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value != "DEPOSIT" & Strings.Left(frm.grdChecks.Value, 6) != "BnkErr") {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "mm/dd/yy"), ";");
						frm.grdChecks.Col = 1;
						rpt.Send("tab(15)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(21)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						ln = ln + 1;
						ot = ot + Convert.ToDouble(frm.grdChecks.Value);
					}
				}
			}
			rpt.Send("tab(43)", "Total=>", "tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ot, "#######0.00"), ref 11));
			rpt.Send();
			rpt.Send();
			//adj = adj + CCur(frm.txtBook(1).Text)
			rpt.FontBold(ref true);
			//rpt.Send "tab(63)", "-----------"
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			rpt.Send("tab(30)", "  Statement Balance=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Convert.ToDecimal(frm.txtStatement.CtlText), "#######0.00"), ref 11));
			rpt.Send("tab(30)", "+ Total Outstanding Deposits=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(aT, "#######0.00"), ref 11));
			rpt.Send("tab(30)", "- Total Outstanding Checks=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ot, "#######0.00"), ref 11));
			rpt.Send("tab(63)", "-----------");
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			rpt.Send("tab(30)", "= Adjusted Statement Balance=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Convert.ToDecimal(frm.txtStatement.CtlText) + aT - ot, "#######0.00"), ref 11));
			rpt.Send("tab(63)", "===========");
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			rpt.Send("tab(30)", "  Book Balance=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Convert.ToDecimal(frm.txtBook[0].CtlText), "#######0.00"), ref 11));
			rpt.Send("tab(30)", "+ Bank Errors In Your Favor=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ft, "#######0.00"), ref 11));
			rpt.Send("tab(30)", "- Bank Errors In Bank's Favor=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(gt, "#######0.00"), ref 11));
			rpt.Send("tab(30)", "+ Other Adjustments =>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(adj, "#######0.00"), ref 11));
			rpt.Send("tab(63)", "===========");
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			rpt.Send("tab(38)", "Adjusted Book Balance=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Convert.ToDecimal(frm.txtBook[0].CtlText) + ft - gt + adj, "#######0.00"), ref 11));
			rpt.FontBold(ref false);
			rpt.NewPage();
			cleared = true;
			heading();
			rpt.Send();
			rpt.FontBold(ref true);
			rpt.Send("tab(9)", "Cleared Checks & Debits");
			rpt.FontBold(ref false);
			rpt.Send();
			ln = ln + 4;
			for (j = 1; j <= frm.grdChecks.MaxRows; j++) {
				frm.grdChecks.Row = j;
				frm.grdChecks.Col = 6;
				if (frm.grdChecks.Value != "0") {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value != "DEPOSIT") {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "mm/dd/yy"), ";");
						frm.grdChecks.Col = 1;
						rpt.Send("tab(16)", WinGlobal.lpad(ref (frm.grdChecks.Value), ref 6), ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(23)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 3;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11), ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						dt = dt + Convert.ToDouble(frm.grdChecks.Value);
						ln = ln + 1;
					}
				}
			}
			rpt.Send();
			rpt.Send("tab(50)", "Total=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dt, "#######0.00"), ref 11));
			rpt.Send();
			ln = ln + 3;

			rpt.FontBold(ref true);
			rpt.Send("tab(9)", "Cleared Credits & Deposits");
			rpt.FontBold(ref false);
			rpt.Send();
			ln = ln + 2;
			for (j = 1; j <= frm.grdChecks.MaxRows; j++) {
				frm.grdChecks.Row = j;
				frm.grdChecks.Col = 6;
				if (frm.grdChecks.Value != "0") {
					frm.grdChecks.Col = 1;
					if (frm.grdChecks.Value == "DEPOSIT") {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						frm.grdChecks.Col = 2;
						rpt.Send("tab(6)", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "mm/dd/yy"), ";");
						frm.grdChecks.Col = 1;
						rpt.Send("tab(15)", WinGlobal.lpad(ref (frm.grdChecks.Value), ref 6), ";");
						frm.grdChecks.Col = 5;
						rpt.Send("tab(21)", frm.grdChecks.Value, ";");
						frm.grdChecks.Col = 3;
						rpt.Send("tab(50)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11), ";");
						frm.grdChecks.Col = 4;
						rpt.Send("tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(frm.grdChecks.Value, "#######0.00"), ref 11));
						ct = ct + Convert.ToDouble(frm.grdChecks.Value);
						ln = ln + 1;
					}
				}
			}
			rpt.Send();
			rpt.Send("tab(50)", "Total=>", "tab(63)", WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ct, "#######0.00"), ref 11));

			rpt.EndDoc();

		}
//******************************************************************************
		public void heading()
		{
			//******************************************************************************

			pg = pg + 1;
			rpt.FontBold(ref true);
			rpt.Send("tab(28)", "CHECK RECONCILIATION REPORT                 page " + pg);
			rpt.FontBold(ref false);
			rpt.Send("");
			rpt.Send("tab(6)", "Company:", "tab(22)", CompanyName);
			rpt.Send("tab(6)", "Department:", "tab(22)", DepartmentName);
			rpt.Send("tab(6)", "Statement Date:", "tab(22)", StatementDate);
			rpt.Send();
			rpt.Send();
			ln = 7 + WinglGlobal.tmargin;
			if (cleared) {
				rpt.Send("tab(8)", "Date", "tab(18)", "Chk#", "tab(23)", "Payee/Description", "tab(55)", "Amount", "tab(65)", "Bank Stmt");
				rpt.Send("tab(6)", "-------- ------- -------------------------- ----------- ------------");
				ln = ln + 2;
			}

		}
//******************************************************************************
		public void CheckRecon(ref bool Batch)
		{
			object i = null;
			//******************************************************************************

			short r = 0;

			r = 0;
			//For i = 0 To My.Application.OpenForms.Count - 1
			//If My.Application.OpenForms.Item(i).Name = "CheckRecon" Then
			//UPGRADE_ISSUE: Control grdChecks could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//r = My.Application.OpenForms.Item(i).grdChecks.MAXROWS
			//frm = My.Application.OpenForms.Item(i)
			//Exit For
			//End If
			//Next 
			if (r == 0)
				return;

			rpt = new clsReport();
			rpt.Target = 0;
			rpt.ReportName = "CheckRecon";
			rpt.rptObject = this;
			rpt.FontName(ref "Courier New");
			rpt.FontSize(ref 12);
			print_report();

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{

			frm.Visible = true;
			if (frm.success)
				frm.Post_Recon();

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
	}
}
