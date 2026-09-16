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
	internal class clsOperatingStatement
	{
// 1) Prior year total is same as current year
// 2) No totals for Difference column
// 3) Print starts with page 2
// 4) Heading: Include name of operating statement description
// 5) List of operating statements
		private frmOperatingStatement frm;
		private clsReport rpt;
		private short pg;
		private short ln;
//UPGRADE_WARNING: Arrays in structure tblrange may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private short q;
		private DAO.Recordset tblacct;
		private DAO.Recordset tblrange;
//UPGRADE_WARNING: Arrays in structure tblTrans may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblTrans;
		private System.DateTime PriorStartDate;
		private System.DateTime PriorEndDate;
		private System.DateTime CurrentStartDate;
		private System.DateTime CurrentEndDate;
		private decimal TotalIncome;
		private decimal TotalExpense;
		private decimal pITotal;
		private decimal pETtotal;
		private decimal YTD_I_Total;
		private decimal YTD_E_Total;
		private decimal YTDIncome;
		private decimal YTDExpense;
		private decimal T_I_Amount;
		private decimal T_E_Amount;
		private decimal T_I_YTD_Amount;
		private decimal T_E_YTD_Amount;

//local variable(s) to hold property value(s)
			//local copy
		private int mvarCompany;
			//local copy
		private bool mvarConsolidated;
			//local copy
		private int mvarDepartment;
			//local copy
		private short mvarMonth;
			//local copy
		private short mvarYear;
			//local copy
		private bool mvarCancel;
			//local copy
		private string mvarCompanyName;
			//local copy
		private short mvarDetailSummaryTotals;
			//local copy
		private short mvarCurrentQtdYtd;
			//local copy
		private bool mvarShowPriorYear;
			//local copy
		private bool mvarShowPercentage;
			//local copy
		private bool mvarShowYTD;
			//local copy
		private bool mvarShowZero;
			//local copy
		private string mvarReportName;
			//local copy
		private string mvarDepartmentName;
//local variable(s) to hold property value(s)
			//local copy
		private string mvarStmtName;
		public string StmtName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.StmtName
			get { return mvarStmtName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.StmtName = 5
			set { mvarStmtName = value; }
		}
		public string DepartmentName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.DepartmentName
			get { return mvarDepartmentName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.DepartmentName = 5
			set { mvarDepartmentName = value; }
		}
		public string ReportName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ReportName
			get { return mvarReportName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ReportName = 5
			set { mvarReportName = value; }
		}
		public bool ShowZero {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ShowZero
			get { return mvarShowZero; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ShowZero = 5
			set { mvarShowZero = value; }
		}
		public bool ShowYTD {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ShowYTD
			get { return mvarShowYTD; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ShowYTD = 5
			set { mvarShowYTD = value; }
		}
		public bool ShowPercentage {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ShowPercentage
			get { return mvarShowPercentage; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ShowPercentage = 5
			set { mvarShowPercentage = value; }
		}
		public bool ShowPriorYear {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ShowPriorYear
			get { return mvarShowPriorYear; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ShowPriorYear = 5
			set { mvarShowPriorYear = value; }
		}
		public short CurrentQtdYtd {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CurrentQtdYtd
			get { return mvarCurrentQtdYtd; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.CurrentQtdYtd = 5
			set { mvarCurrentQtdYtd = value; }
		}
		public short DetailSummaryTotals {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.DetailSummaryTotals
			get { return mvarDetailSummaryTotals; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.DetailSummaryTotals = 5
			set { mvarDetailSummaryTotals = value; }
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
		public short rptYear {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Year
			get { return mvarYear; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Year = 5
			set { mvarYear = value; }
		}
		public short rptMonth {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Month
			get { return mvarMonth; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Month = 5
			set { mvarMonth = value; }
		}
		public int Department {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Department
			get { return mvarDepartment; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Department = 5
			set { mvarDepartment = value; }
		}
		public bool Consolidated {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Consolidated
			get { return mvarConsolidated; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Consolidated = 5
			set { mvarConsolidated = value; }
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
			object peamount = null;
			object eamount = null;
			object x = null;
			object piamount = null;
			object iamount = null;
			object petotal = null;

			int r = 0;
			int i = 0;
			bool first = false;
			int xacct = 0;
			string xacctname = null;
			int[] Ia = null;
			int[] Ea = null;

			// Calculate current quarter
			q = Conversion.Fix(Convert.ToInt16(rptMonth - 1) / 3) + 1;

			// Build array for Income accounts
			tblrange = WinglGlobal.GLdb.OpenRecordset("SELECT AccountNumber FROM AccountRanges WHERE ReportName = '" + ReportName + "' AND ReportSection = 1");
			if (tblrange.RecordCount == 0) {
				Interaction.MsgBox("You have not entered Income accounts for this report.", , "Operating Statement");
				return;
			}
			tblrange.MoveLast();
			r = tblrange.RecordCount;
			tblrange.MoveFirst();
			i = 0;
			while (!tblrange.EOF) {
				if (tblrange.Fields["AccountNumber"].Value > 0) {
					i = i + 1;
					Array.Resize(ref Ia, i + 1);
					Ia[i] = tblrange.Fields["AccountNumber"].Value;
				}
				tblrange.MoveNext();
			}
			tblrange.Close();

			// Build array for Expense accounts
			tblrange = WinglGlobal.GLdb.OpenRecordset("SELECT AccountNumber FROM AccountRanges WHERE ReportName = '" + ReportName + "' AND ReportSection = 2");
			if (tblrange.RecordCount == 0) {
				Interaction.MsgBox("You have not entered Expense accounts for this report.", , "Operating Statement");
				return;
			}
			tblrange.MoveLast();
			r = tblrange.RecordCount;
			tblrange.MoveFirst();
			i = 0;
			while (!tblrange.EOF) {
				if (tblrange.Fields["AccountNumber"].Value > 0) {
					i = i + 1;
					Array.Resize(ref Ea, i + 1);
					Ea[i] = tblrange.Fields["AccountNumber"].Value;
				}
				tblrange.MoveNext();
			}
			tblrange.Close();

			if (rptYear != DateAndTime.Year(WinglGlobal.CurrentFiscalYear(ref Company))) {
				CurrentStartDate = DateAndTime.DateSerial(DateAndTime.Year(WinglGlobal.CurrentFiscalYear(ref Company)) - 1, rptMonth, 1);
				PriorStartDate = DateAndTime.DateSerial(DateAndTime.Year(WinglGlobal.CurrentFiscalYear(ref Company)) - 2, rptMonth, 1);
			} else {
				CurrentStartDate = DateAndTime.DateSerial(DateAndTime.Year(WinglGlobal.CurrentFiscalYear(ref Company)), rptMonth, 1);
				PriorStartDate = DateAndTime.DateSerial(DateAndTime.Year(WinglGlobal.CurrentFiscalYear(ref Company)) - 1, rptMonth, 1);
			}
			CurrentEndDate = DateAndTime.DateSerial(DateAndTime.Year(CurrentStartDate), rptMonth + 1, 1 - 1);
			PriorEndDate = DateAndTime.DateSerial(DateAndTime.Year(PriorStartDate), rptMonth + 1, 1 - 1);
			switch (CurrentQtdYtd) {
				case 0:
					// Current
					break;
				case 1:
					// QTD
					CurrentStartDate = WinglGlobal.CurrentQuarter(ref Company, ref CurrentStartDate);
					PriorStartDate = WinglGlobal.CurrentQuarter(ref Company, ref PriorStartDate);
					break;
				case 2:
					// YTD
					CurrentStartDate = DateAndTime.DateSerial(DateAndTime.Year(CurrentStartDate), DateAndTime.Month(WinglGlobal.CurrentFiscalYear(ref Company)), 1);
					PriorStartDate = DateAndTime.DateSerial(DateAndTime.Year(PriorStartDate), DateAndTime.Month(WinglGlobal.CurrentFiscalYear(ref Company)), 1);
					break;
			}
			TotalExpense = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			petotal = 0;
			YTD_E_Total = 0;
			for (i = 1; i <= Information.UBound(Ea, 1); i++) {
				TotalExpense = TotalExpense + WinglGlobal.AcctBal(ref Company, ref Ea[i], ref CurrentStartDate, ref CurrentEndDate, ref Department);
				//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				petotal = petotal + WinglGlobal.AcctBal(ref Company, ref Ea[i], ref PriorStartDate, ref PriorEndDate, ref Department);
			}
			TotalIncome = 0;
			pITotal = 0;
			YTD_I_Total = 0;
			for (i = 1; i <= Information.UBound(Ia, 1); i++) {
				TotalIncome = TotalIncome - WinglGlobal.AcctBal(ref Company, ref Ia[i], ref CurrentStartDate, ref CurrentEndDate, ref Department);
				pITotal = pITotal + (-WinglGlobal.AcctBal(ref Company, ref Ia[i], ref PriorStartDate, ref PriorEndDate, ref Department));
			}

			pg = 0;
			heading();

			// Process Income
			first = true;
			rpt.Send();
			rpt.Send(Strings.Space(5) + "INCOME");
			rpt.Send(Strings.Space(5) + "------");
			rpt.Send();
			ln = ln + 4;
			//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			iamount = 0;
			YTDIncome = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			piamount = 0;
			T_I_Amount = 0;
			T_I_YTD_Amount = 0;
			for (i = 1; i <= Information.UBound(Ia, 1); i++) {
				if (first) {
					xacct = Ia[i];
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT Description as AccountName FROM Accounts WHERE Key = " + Ia[i]);
					xacctname = Strings.Trim(Strings.Left(tblacct.Fields["AccountName"].Value, 32));
					tblacct.Close();
					first = false;
				}
				if (xacct != Ia[i]) {
					//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (ShowZero | iamount != 0 | (ShowPriorYear & piamount != 0)) {
						//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = Strings.Space(5) + WinGlobal.rtpad(ref xacctname, ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(iamount, "#########0.00"), ref 13);
						if (ShowPercentage) {
							if (TotalIncome != 0) {
								//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((iamount / TotalIncome) * 100, "##0.0"), ref 5);
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = x + "      ";
							}
						}
						if (ShowYTD) {
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(YTDIncome, "#########0.00"), ref 13);
						}
						if (ShowPriorYear) {
							//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(piamount, "#########0.00"), ref 13);
							if (ShowPercentage) {
								if (pITotal != 0) {
									//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((piamount / pITotal) * 100, "##0.0"), ref 5);
								} else {
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = x + "      ";
								}
							}
							//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(iamount - piamount, "#########0.00"), ref 13);
						}
						if (ln > rpt.PageSize - 10) {
							rpt.NewPage();
							heading();
						}
						rpt.Send(x);
						ln = ln + 1;
					}
					xacct = Ia[i];
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT Description as AccountName FROM Accounts WHERE Key = " + Ia[i]);
					xacctname = Strings.Trim(Strings.Left(tblacct.Fields["AccountName"].Value, 32));
					//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					T_I_Amount = T_I_Amount + iamount;
					T_I_YTD_Amount = T_I_YTD_Amount + YTDIncome;
					//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					iamount = 0;
					YTDIncome = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					piamount = 0;
				}

				//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				iamount = iamount + (-WinglGlobal.AcctBal(ref Company, ref Ia[i], ref CurrentStartDate, ref CurrentEndDate, ref Department));
				//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				piamount = piamount + (-WinglGlobal.AcctBal(ref Company, ref Ia[i], ref PriorStartDate, ref PriorEndDate, ref Department));
				YTDIncome = YTDIncome + (-WinglGlobal.AcctBal(ref Company, ref Ia[i], ref DateAndTime.DateSerial(DateAndTime.Year(CurrentStartDate), 1, 1), ref CurrentEndDate, ref Department));
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (ShowZero | iamount != 0 | (ShowPriorYear & piamount != 0) | (ShowYTD & YTDIncome != 0)) {
				//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = Strings.Space(5) + WinGlobal.rtpad(ref xacctname, ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(iamount, "#########0.00"), ref 13);
				if (ShowPercentage) {
					if (TotalIncome != 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((iamount / TotalIncome) * 100, "##0.0"), ref 5);
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + "      ";
					}
				}
				if (ShowYTD) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(YTDIncome, "#########0.00"), ref 13);
				}
				if (ShowPriorYear) {
					//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(piamount, "#########0.00"), ref 13);
					if (ShowPercentage) {
						if (pITotal != 0) {
							//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((piamount / pITotal) * 100, "##0.0"), ref 5);
						} else {
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + "      ";
						}
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object piamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(iamount - piamount, "#########0.00"), ref 13);
				}
				if (ln > rpt.PageSize - 10) {
					rpt.NewPage();
					heading();
				}
				rpt.Send(x);
				ln = ln + 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object iamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				T_I_Amount = T_I_Amount + iamount;
				T_I_YTD_Amount = T_I_YTD_Amount + YTDIncome;
			}

			rpt.Send();
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(5) + WinGlobal.lpad(ref "TOTAL INCOME", ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_I_Amount, "#########0.00"), ref 13);
			if (ShowPercentage) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + " 100.0";
			}
			if (ShowYTD) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_I_YTD_Amount, "#########0.00"), ref 14);
			}
			if (ShowPriorYear) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(pITotal, "#########0.00"), ref 13);
				if (ShowPercentage) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + Strings.Space(5);
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_I_Amount - pITotal, "#########0.00"), ref 13);
			}
			rpt.Send(x);
			ln = ln + 2;

			// Process Expense
			first = true;
			rpt.Send();
			rpt.Send(Strings.Space(5) + "EXPENSE");
			rpt.Send(Strings.Space(5) + "-------");
			rpt.Send();
			ln = ln + 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			eamount = 0;
			YTDExpense = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			peamount = 0;
			T_E_Amount = 0;
			T_E_YTD_Amount = 0;
			for (i = 1; i <= Information.UBound(Ea, 1); i++) {
				if (first) {
					xacct = Ea[i];
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT Description as AccountName FROM Accounts WHERE Key = " + Ea[i]);
					xacctname = Strings.Trim(Strings.Left(tblacct.Fields["AccountName"].Value, 32));
					first = false;
				}
				if (xacct != Ea[i]) {
					//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (ShowZero | eamount != 0 | (ShowPriorYear & peamount != 0) | (ShowYTD & YTDExpense != 0)) {
						//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = Strings.Space(5) + WinGlobal.rtpad(ref xacctname, ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(eamount, "#########0.00"), ref 13);
						if (ShowPercentage) {
							if (TotalExpense != 0) {
								//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((eamount / TotalExpense) * 100, "##0.0"), ref 5);
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = x + "      ";
							}
						}
						if (ShowYTD) {
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(YTDExpense, "#########0.00"), ref 13);
						}
						if (ShowPriorYear) {
							//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(peamount, "#########0.00"), ref 13);
							if (ShowPercentage) {
								//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								if (petotal != 0) {
									//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((peamount / petotal) * 100, "##0.0"), ref 5);
								} else {
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = x + "      ";
								}
							}
							//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(eamount - peamount, "#########0.00"), ref 13);
						}
						if (ln > rpt.PageSize - 10) {
							rpt.NewPage();
							heading();
						}
						rpt.Send(x);
						ln = ln + 1;
						//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						T_E_Amount = T_E_Amount + eamount;
						T_E_YTD_Amount = T_E_YTD_Amount + YTDExpense;
					}
					xacct = Ea[i];
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT Description as AccountName FROM Accounts WHERE Key = " + Ea[i]);
					xacctname = Strings.Trim(Strings.Left(tblacct.Fields["AccountName"].Value, 32));
					//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					eamount = 0;
					YTDExpense = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					peamount = 0;
				}

				//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				eamount = eamount + WinglGlobal.AcctBal(ref Company, ref Ea[i], ref CurrentStartDate, ref CurrentEndDate, ref Department);
				//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				peamount = peamount + WinglGlobal.AcctBal(ref Company, ref Ea[i], ref PriorStartDate, ref PriorEndDate, ref Department);
				YTDExpense = YTDExpense + WinglGlobal.AcctBal(ref Company, ref Ea[i], ref DateAndTime.DateSerial(DateAndTime.Year(CurrentStartDate), 1, 1), ref CurrentEndDate, ref Department);
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (ShowZero | eamount != 0 | (ShowPriorYear & peamount != 0)) {
				//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = Strings.Space(5) + WinGlobal.rtpad(ref xacctname, ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(eamount, "#########0.00"), ref 13);
				if (ShowPercentage) {
					if (TotalExpense != 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((eamount / TotalExpense) * 100, "##0.0"), ref 5);
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + "      ";
					}
				}
				if (ShowYTD) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(YTDExpense, "#########0.00"), ref 13);
				}
				if (ShowPriorYear) {
					//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(peamount, "#########0.00"), ref 13);
					if (ShowPercentage) {
						//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (petotal != 0) {
							//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((peamount / petotal) * 100, "##0.0"), ref 5);
						} else {
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = x + "      ";
						}
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object peamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(eamount - peamount, "#########0.00"), ref 13);
				}
				if (ln > rpt.PageSize - 10) {
					rpt.NewPage();
					heading();
				}
				rpt.Send(x);
				ln = ln + 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object eamount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				T_E_Amount = T_E_Amount + eamount;
				T_E_YTD_Amount = T_E_YTD_Amount + YTDExpense;
			}
			rpt.Send();
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(5) + WinGlobal.lpad(ref "TOTAL EXPENSE", ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_E_Amount, "#########0.00"), ref 13);
			if (ShowPercentage) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + " 100.0";
			}
			if (ShowYTD) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_E_YTD_Amount, "#########0.00"), ref 14);
			}
			if (ShowPriorYear) {
				//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(petotal, "#########0.00"), ref 13);
				if (ShowPercentage) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + Strings.Space(5);
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_E_Amount - petotal, "#########0.00"), ref 13);
			}
			rpt.Send(x);
			rpt.Send();
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(5) + WinGlobal.lpad(ref "PROFIT/LOSS", ref 32) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_I_Amount - T_E_Amount, "#########0.00"), ref 13);
			if (ShowPercentage) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + "      ";
			}
			if (ShowYTD) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(T_I_YTD_Amount - T_E_YTD_Amount, "#########0.00"), ref 14);
			}
			if (ShowPriorYear) {
				//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(pITotal - petotal, "#########0.00"), ref 13);
				if (ShowPercentage) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + Strings.Space(5);
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object petotal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format((T_I_Amount - T_E_Amount) - (pITotal - petotal), "#########0.00"), ref 13);
			}
			rpt.Send(x);

			rpt.EndDoc();

		}
		public void heading()
		{
			object tl = null;
			object x = null;

			pg = pg + 1;
			rpt.Send(Strings.Space(5) + StmtName);
			rpt.Send(Strings.Space(5) + CompanyName);
			rpt.Send(Strings.Space(5) + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Today, "mm/dd/yyyy") + Strings.Space(5) + "page " + pg);
			switch (CurrentQtdYtd) {
				case 0:
					// Current
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					rpt.Send(Strings.Space(5) + "For month of " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateValue(Convert.ToString(rptMonth) + "/01/2000"), "mmmm") + " - " + rptYear);
					break;
				case 1:
					// QTD
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					rpt.Send(Strings.Space(5) + "For " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateValue(Convert.ToString((q - 1) * 3 + 1) + "/01/2000"), "mmmm") + " to " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateValue(Convert.ToString(rptMonth) + "/01/2000"), "mmmm") + " - " + rptYear);
					break;
				case 2:
					// YTD
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					rpt.Send(Strings.Space(5) + "For January to " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateValue(Convert.ToString(rptMonth) + "/01/2000"), "mmmm") + " - " + rptYear);
					break;
			}
			ln = 4 + WinglGlobal.tmargin;
			if (Department != -1) {
				rpt.Send(Strings.Space(5) + DepartmentName);
				ln = ln + 1;
			}
			rpt.Send();
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(5) + "ACCOUNT" + Strings.Space(25);
			//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tl = 32;
			switch (CurrentQtdYtd) {
				case 0:
					// Current
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "CURRENT MONTH  ";
					//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tl = tl + 15;
					break;
				case 1:
					// QTD
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "   QTD AMOUNT  ";
					//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tl = tl + 14;
					break;
				case 2:
					// YTD
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "   YTD AMOUNT  ";
					//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tl = tl + 14;
					break;
			}
			if (ShowPercentage) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + "  %";
				//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tl = tl + 7;
			}
			if (ShowYTD) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = x + "   YEAR TO DATE";
				//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tl = tl + 17;
			}
			if (ShowPriorYear) {
				switch (CurrentQtdYtd) {
					case 0:
						// Current
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + " PRIOR YR MNTH";
						//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tl = tl + 14;
						break;
					case 1:
						// QTD
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + "  PRIOR YR QTR";
						//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tl = tl + 14;
						break;
					case 2:
						// YTD
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = x + "  PRIOR YEAR";
						//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tl = tl + 14;
						break;
				}
				if (ShowPercentage) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "      %";
					//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tl = tl + 7;
				}
				if (ShowPriorYear) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "    DIFFERENCE";
					//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tl = tl + 14;
				}
			}
			rpt.Send(x);
			//UPGRADE_WARNING: Couldn't resolve default property of object tl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rpt.Send(Strings.Space(5) + new string("=", tl));
			ln = ln + 2;

		}
		public void OperatingStatement(ref bool Batch)
		{

			if (Batch) {
				rpt = new clsReport();
				rpt.Batch = true;
				rpt.Target = 1;
				rpt.ReportName = "OperatingStatement";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmOperatingStatement();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "OperatingStatement";
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
			WinglGlobal.BatchRptSave[0] = "OS";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = Consolidated;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = rptMonth;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = rptYear;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = DetailSummaryTotals;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = CurrentQtdYtd;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[9] = ShowPriorYear;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(10). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[10] = ShowPercentage;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(11). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[11] = ShowYTD;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(12). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[12] = ShowZero;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(13). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[13] = ReportName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(14). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[14] = DepartmentName;

		}
		public void Unserialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Company = WinglGlobal.BatchRptSave[1];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Consolidated = WinglGlobal.BatchRptSave[2];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Department = WinglGlobal.BatchRptSave[3];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptMonth = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptYear = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CompanyName = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			DetailSummaryTotals = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CurrentQtdYtd = WinglGlobal.BatchRptSave[8];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ShowPriorYear = WinglGlobal.BatchRptSave[9];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(10). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ShowPercentage = WinglGlobal.BatchRptSave[10];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(11). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ShowYTD = WinglGlobal.BatchRptSave[11];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(12). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ShowZero = WinglGlobal.BatchRptSave[12];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(13). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ReportName = WinglGlobal.BatchRptSave[13];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(14). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			DepartmentName = WinglGlobal.BatchRptSave[14];

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsOperatingStatement();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.OperatingStatement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.OperatingStatement(false);
				//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				xrpt = null;
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
