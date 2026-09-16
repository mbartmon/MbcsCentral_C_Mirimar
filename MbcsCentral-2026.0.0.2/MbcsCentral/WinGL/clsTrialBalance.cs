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
	internal class clsTrialBalance
	{
		private frmTrialBalance frm;
		private short xYear;
		private clsReport rpt;
		private short pg;
		private short ln;
//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tbl;
		private decimal credits;
		private decimal debits;
		private System.DateTime StartDate;
		private System.DateTime EndDate;

//local variable(s) to hold property value(s)
			//local copy
		private int mvarCompany;
			//local copy
		private string mvarCompanyName;
			//local copy
		private int mvarDepartment;
			//local copy
		private string mvarDepartmentName;
			//local copy
		private short mvarrMonth;
			//local copy
		private short mvarrYear;
			//local copy
		private bool mvarConsolidated;
			//local copy
		private bool mvarZeroBalances;
			//local copy
		private bool mvarXWorkArea;
			//local copy
		private bool mvarCancel;
			//local copy
		private bool mvarPriorYear;
		public bool PriorYear {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.PriorYear
			get { return mvarPriorYear; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.PriorYear = 5
			set { mvarPriorYear = value; }
		}
		public bool Cancel {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Cancel
			get { return mvarCancel; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Cancel = 5
			set { mvarCancel = value; }
		}
		public bool XWorkArea {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.XWorkArea
			get { return mvarXWorkArea; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.XWorkArea = 5
			set { mvarXWorkArea = value; }
		}
		public bool ZeroBalances {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ZeroBalances
			get { return mvarZeroBalances; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ZeroBalances = 5
			set { mvarZeroBalances = value; }
		}
		public bool Consolidated {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Consolidated
			get { return mvarConsolidated; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Consolidated = 5
			set { mvarConsolidated = value; }
		}
		public short rptYear {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.rptYear
			get { return mvarrYear; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.rptYear = 5
			set { mvarrYear = value; }
		}
		public short rptMonth {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.rptMonth
			get { return mvarrMonth; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.rptMonth = 5
			set { mvarrMonth = value; }
		}
		public string DepartmentName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.DepartmentName
			get { return mvarDepartmentName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.DepartmentName = 5
			set { mvarDepartmentName = value; }
		}
		public int Department {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Department
			get { return mvarDepartment; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Department = 5
			set { mvarDepartment = value; }
		}
		public string CompanyName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CompanyName
			get { return mvarCompanyName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.CompanyName = 5
			set { mvarCompanyName = value; }
		}
		public int Company {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Company
			get { return mvarCompany; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Company = 5
			set { mvarCompany = value; }
		}
		public void print_report()
		{
			object k = null;
			object j = null;
			object i = null;

			if (!PriorYear) {
				xYear = DateAndTime.Year(DateAndTime.Today);
			} else {
				xYear = DateAndTime.Year(DateAndTime.Today) - 1;
			}
			StartDate = DateAndTime.DateSerial(xYear, 1, 1);
			EndDate = DateAndTime.DateSerial(xYear, rptMonth + 1, 1 - 1);
			credits = 0;
			debits = 0;
			pg = 0;
			heading();
			for (i = 1; i <= 5; i++) {
				for (j = 0; j <= 10; j++) {
					for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//If InStr("C", LH(i, j, k).type) > 0 Then
						//If ln > rpt.PageSize - 14 Then
						//rpt.NewPage()
						//Call heading()
						//End If
						//rpt.FontBold(True)
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//rpt.Send(Space(5) & Trim(UCase(LH(i, j, k).Description)))
						//rpt.Send()
						//ln = ln + 2
						//rpt.FontBold(False)
						//End If
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//If InStr("A", LH(i, j, k).type) > 0 Then
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//Call ProcessAccount(LH(i, j, k).Description, CInt(LH(i, j, k).key))
						//End If
					}
				}
			}
			rpt.Send(Strings.Space(41) + "============= =============");
			rpt.Send(Strings.Space(5) + "TOTALS=>" + Strings.Space(28) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(debits, "#########0.00"), ref 13) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-credits, "#########0.00"), ref 14));
			rpt.EndDoc();

		}
		private void ProcessAccount(ref string desc, ref int key)
		{

			string xsql = null;
			string x = null;
			decimal bal = default(decimal);
			short i = 0;
			string ds = null;
			string dp = null;
			string z = null;

			ds = WinGlobal.rtpad(ref Strings.Left(Strings.Trim(desc), 30), ref 31);
			bal = 0;
			if (Consolidated) {
				bal = WinglGlobal.AcctBal(Company, key, StartDate, EndDate);
				dp = "     ";
				print_line(ref ds, ref dp, ref bal);
			} else {
				if (Department <= 0) {
					tbl = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Department WHERE [Company Number] = " + Company);
					while (!tbl.EOF) {
						bal = WinglGlobal.AcctBal(ref Company, ref key, ref StartDate, ref EndDate, ref tbl.Fields["Department Number"].Value);
						dp = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["Department Number"].Value, "###0"), ref 4) + " ";
						print_line(ref ds, ref dp, ref bal);
						ds = Strings.Space(31);
						tbl.MoveNext();
					}
				} else {
					bal = WinglGlobal.AcctBal(ref Company, ref key, ref StartDate, ref EndDate, ref Department);
					dp = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Department, "###0"), ref 4) + " ";
					print_line(ref ds, ref dp, ref bal);
				}
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			tbl.Close();

		}
		private void print_line(ref string ds, ref string dp, ref decimal bal)
		{

			string x = null;

			if (ZeroBalances | bal != 0) {
				x = Strings.Space(5) + ds + dp;

				switch (bal) {
					case  // ERROR: Case labels with binary operators are unsupported : LessThan
0:
						x = x + Strings.Space(14) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-bal, "#########0.00"), ref 13);
						credits = credits + bal;
						break;
					case 0:
						x = x + Strings.Space(27);
						break;
					case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
0:
						x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(bal, "#########0.00"), ref 13) + Strings.Space(14);
						debits = debits + bal;
						break;
				}

				x = x + " _____________ _____________";
				if (XWorkArea) {
					x = x + " _____________ _____________";
				}
				if (ln > rpt.PageSize - 12) {
					rpt.NewPage();
					heading();
				}
				rpt.Send(x);
				rpt.Send();
				ln = ln + 2;
			}

		}
		private void heading()
		{
			object x = null;

			pg = pg + 1;
			rpt.Send(Strings.Space(5) + "TRIAL BALANCE as of " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateSerial(xYear, rptMonth, 1), "mmmm, yyyy"));
			rpt.Send(Strings.Space(5) + CompanyName + Strings.Space(25) + DateAndTime.Today + Strings.Space(5) + "page " + pg);
			rpt.Send();
			rpt.Send();
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(5) + "Account" + Strings.Space(25) + "Dpt        debits       credits";
			if (XWorkArea) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + Strings.Space(8) + "Balance Sheet" + Strings.Space(16) + "Profit/Loss";
			}
			rpt.Send(x);
			rpt.Send(Strings.Space(5) + new string("-", 119));
			ln = 6 + WinglGlobal.tmargin;

		}
		public void TrialBalance(ref bool Batch)
		{

			if (Batch) {
				WinglGlobal.getLedgerStructure();
				rpt = new clsReport();
				rpt.Batch = true;
				rpt.Target = 1;
				rpt.ReportName = "TrialBalance";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmTrialBalance();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					WinglGlobal.getLedgerStructure();
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "TrialBalance";
					rpt.rptObject = this;
					rpt.FontName(ref "Courier New");
					rpt.FontSize(ref 12);
					print_report();
				}
			}

		}
		public void Serialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[0] = "TB";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = rptMonth;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = rptYear;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = Consolidated;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = ZeroBalances;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = XWorkArea;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[9] = PriorYear;

		}
		public void Unserialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Company = WinglGlobal.BatchRptSave[1];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CompanyName = WinglGlobal.BatchRptSave[2];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Department = WinglGlobal.BatchRptSave[3];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptMonth = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptYear = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Consolidated = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ZeroBalances = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			XWorkArea = WinglGlobal.BatchRptSave[8];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			PriorYear = WinglGlobal.BatchRptSave[9];

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsTrialBalance();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.TrialBalance. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.TrialBalance(false);
				//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				xrpt = null;
			} else {
				WinglGlobal.LH = null;
			}

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
		public void setDates()
		{

			rptMonth = WinglGlobal.bMonth;
			rptYear = WinglGlobal.bYear;

		}
	}
}
