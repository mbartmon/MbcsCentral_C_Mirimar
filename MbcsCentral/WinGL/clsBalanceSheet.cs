using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6;
namespace WinGL
{
	internal class clsBalanceSheet
	{
		private frmBalanceSheet frm;
		private short xYear;
		private clsReport rpt;
		private short pg;
		private short ln;

//local variable(s) to hold property value(s)
			//local copy
		private int mvarCompany;
			//local copy
		private bool mvarConsolidated;
			//local copy
		private int mvarDepartment;
			//local copy
		private int mvarPLAccount;
			//local copy
		private short mvarMonth;
			//local copy
		private short mvarYear;
			//local copy
		private bool mvarCancel;
			//local copy
		private string mvarCompanyName;
			//local copy
		private string mvarPLAccountName;
		public string PLAccountName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.PLAccountName
			get { return mvarPLAccountName; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.PLAccountName = 5
			set { mvarPLAccountName = value; }
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
		public int PLAccount {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.PLAccount
			get { return mvarPLAccount; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.PLAccount = 5
			set { mvarPLAccount = value; }
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
			object sd = null;

			//UPGRADE_WARNING: Arrays in structure tblBalance may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblBalance = null;
			string xsql = null;
			short m = 0;
			decimal a = default(decimal);
			decimal lTotal = default(decimal);
			decimal eTotal = default(decimal);
			decimal iTotal = default(decimal);
			decimal aTotal = default(decimal);
			decimal qTotal = default(decimal);
			decimal iTotal2 = default(decimal);
			decimal eTotal2 = default(decimal);
			string fldName = null;
			Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString aname = new Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString(30);
			decimal s = default(decimal);
			short j = 0;
			short i = 0;
			short k = 0;
			bool hdflag = false;
			System.DateTime StartDate = default(System.DateTime);
			System.DateTime EndDate = default(System.DateTime);

			//*************************
			// Get Profit/Loss amount *
			//*************************
			if (rptYear == -1) {
				StartDate = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), 1, 1);
				EndDate = WinGlobal.LastOfThisMonth(ref DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), rptMonth, 1));
				fldName = "C_Month";
				xYear = DateAndTime.Year(DateAndTime.Today);
			} else {
				StartDate = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today) - 1, 1, 1);
				EndDate = WinGlobal.LastOfThisMonth(ref DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today) - 1, rptMonth, 1));
				fldName = "P_Month";
				xYear = DateAndTime.Year(DateAndTime.Today) - 1;
			}
			// Get Income total
			iTotal = 0;
			iTotal2 = 0;
			for (j = 0; j <= 10; j++) {
				for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
					//If InStr("A", LH(4, j, k).type) > 0 Then
					if (Consolidated) {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//iTotal = iTotal + AcctBal(Company, Val(LH(4, j, k).key), StartDate, EndDate)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT * FROM Balances WHERE CompanyNumber = " & Company & " AND AccountNumber = " & LH(4, j, k).key
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//iTotal = iTotal + AcctBal(Company, Val(LH(4, j, k).key), StartDate, EndDate, Department)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT * FROM Balances WHERE CompanyNumber = " & Company & " AND DepartmentNumber = " & Department & " AND AccountNumber = " & LH(4, j, k).key
					}
					//Set tblBalance = GLdb.OpenRecordset(xsql)
					//If tblBalance.RecordCount > 0 Then
					//    While Not tblBalance.EOF
					//        For i = 1 To rptMonth
					//            iTotal2 = iTotal2 + tblBalance(fldName & Format(i, "00"))
					//        Next
					//        tblBalance.MoveNext
					//    Wend
					//End If
					//End If
				}
			}
			// Get Expense total
			eTotal = 0;
			eTotal2 = 0;
			for (j = 0; j <= 10; j++) {
				for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
					//If InStr("A", LH(5, j, k).type) > 0 Then
					if (Consolidated) {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//eTotal = eTotal + AcctBal(Company, Val(LH(5, j, k).key), StartDate, EndDate)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT * FROM Balances WHERE CompanyNumber = " & Company & " AND AccountNumber = " & LH(5, j, k).key
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//eTotal = eTotal + AcctBal(Company, Val(LH(5, j, k).key), StartDate, EndDate, Department)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT * FROM Balances WHERE CompanyNumber = " & Company & " AND DepartmentNumber = " & Department & " AND AccountNumber = " & LH(5, j, k).key
					}
					//Set tblBalance = GLdb.OpenRecordset(xsql)
					//If tblBalance.RecordCount > 0 Then
					//    While Not tblBalance.EOF
					//        For i = 1 To rptMonth
					//            eTotal2 = eTotal2 + tblBalance(fldName & Format(i, "00"))
					//        Next
					//        tblBalance.MoveNext
					//    Wend
					//End If
					//End If
				}
			}

			//***********************
			// Now print the report *
			//***********************
			pg = 0;
			heading();
			// The Assets
			aTotal = 0;
			hdflag = false;
			rpt.Send(Strings.Space(35) + "ASSETS");
			rpt.Send();
			for (j = 0; j <= 10; j++) {
				for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
					//If InStr("S", LH(1, j, k).type) > 0 Then
					if (s != 0) {
						rpt.Send(Strings.Space(36) + "==============");
						rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(s, "###,###,##0.00"), ref 14));
						rpt.Send();
						rpt.Send();
						ln = ln + 4;
						hdflag = false;
					}
					s = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//sd = LH(1, j, k).Description
					//End If
					//If InStr("A", LH(1, j, k).type) > 0 Then
					a = 0;
					if (Consolidated) {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(1, j, k).key), StartDate, EndDate)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(1, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND Balances.AccountNumber = " & LH(1, j, k).key & " AND Accounts.Key = " & LH(1, j, k).key
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(1, j, k).key), StartDate, EndDate, Department)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(1, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND DepartmentNumber = " & Department & " AND AccountNumber = " & LH(1, j, k).key & " AND Accounts.Key = " & LH(1, j, k).key
					}
					//Set tblBalance = GLdb.OpenRecordset(xsql)
					//If tblBalance.RecordCount > 0 Then
					//aname.Value = LH(1, j, k).Description
					//    While Not tblBalance.EOF
					//        For i = 1 To rptMonth
					//            a = a + tblBalance(fldName & Format(i, "00"))
					//        Next
					//        tblBalance.MoveNext
					//    Wend
					if (a != 0) {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						if (!hdflag) {
							//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							rpt.Send(Strings.Space(9) + sd);
							// LH(1, j, k).description
							rpt.Send();
							ln = ln + 2;
							hdflag = true;
						}
						rpt.Send(Strings.Space(5) + WinGlobal.rtpad(ref aname.Value, ref 31) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,###,##0.00"), ref 14));
						aTotal = aTotal + a;
						s = s + a;
						ln = ln + 1;
					}
					//End If
					//End If
				}
			}
			if (s != 0) {
				rpt.Send(Strings.Space(36) + "==============");
				rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(s, "###,###,##0.00"), ref 14));
				s = 0;
				rpt.Send();
				ln = ln + 3;
			}
			rpt.Send(Strings.Space(51) + "==============");
			rpt.Send(Strings.Space(51) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(aTotal, "###,###,##0.00"), ref 14));
			rpt.Send();
			rpt.Send(Strings.Space(65) + "==============");
			rpt.Send(Strings.Space(65) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(aTotal, "###,###,##0.00"), ref 14));
			rpt.Send();
			rpt.Send();
			ln = ln + 7;

			// The Liabilities
			if (ln > rpt.PageSize - 20) {
				rpt.NewPage();
				heading();
			}
			rpt.Send(Strings.Space(40) + "LIABILITIES");
			rpt.Send();
			lTotal = 0;
			hdflag = false;
			for (j = 0; j <= 10; j++) {
				for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
					//If InStr("S", LH(2, j, k).type) > 0 Then
					if (s != 0) {
						rpt.Send(Strings.Space(36) + "==============");
						rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(s, "###,###,##0.00"), ref 14));
						rpt.Send();
						rpt.Send();
						ln = ln + 4;
						hdflag = false;
					}
					s = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//sd = LH(2, j, k).Description
					//End If
					//If InStr("A", LH(2, j, k).type) > 0 Then
					if (Consolidated) {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(2, j, k).key), StartDate, EndDate)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(2, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND AccountNumber = " & LH(2, j, k).key & " AND Accounts.Key = " & LH(2, j, k).key
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(2, j, k).key), StartDate, EndDate, Department)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(2, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND DepartmentNumber = " & Department & " AND AccountNumber = " & LH(2, j, k).key & " AND Accounts.Key = " & LH(2, j, k).key
					}
					//Set tblBalance = GLdb.OpenRecordset(xsql)
					//If tblBalance.RecordCount > 0 Then
					//aname.Value = LH(2, j, k).Description
					//    While Not tblBalance.EOF
					//        For i = 1 To rptMonth
					//            a = a + tblBalance(fldName & Format(i, "00"))
					//        Next
					//        tblBalance.MoveNext
					//    Wend
					//UPGRADE_WARNING: Couldn't resolve default property of object LH(2, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If LH(2, j, k).key = PLAccount Then
					a = a + iTotal - eTotal;
					//End If
					if (a != 0) {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						if (!hdflag) {
							//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							rpt.Send(Strings.Space(9) + sd);
							// LH(2, j, k).description
							rpt.Send();
							ln = ln + 2;
							hdflag = true;
						}
						rpt.Send(Strings.Space(5) + WinGlobal.rtpad(ref aname.Value, ref 31) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-a, "###,###,##0.00"), ref 14));
						lTotal = lTotal + a;
						s = s + a;
						ln = ln + 1;
					}
					//End If
					//End If
				}
			}
			if (s != 0) {
				rpt.Send(Strings.Space(36) + "==============");
				rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-s, "###,###,##0.00"), ref 14));
				s = 0;
				rpt.Send();
				ln = ln + 3;
			}
			rpt.Send(Strings.Space(51) + "==============");
			rpt.Send(Strings.Space(51) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-lTotal, "###,###,##0.00"), ref 14));
			rpt.Send();
			rpt.Send();
			ln = ln + 4;

			// The Equity
			if (ln > rpt.PageSize - 20) {
				rpt.NewPage();
				heading();
			}
			rpt.Send(Strings.Space(35) + "EQUITY");
			rpt.Send();
			qTotal = 0;
			hdflag = false;
			for (j = 0; j <= 10; j++) {
				for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
					a = 0;
					//If InStr("S", LH(3, j, k).type) > 0 Then
					if (s != 0) {
						rpt.Send(Strings.Space(36) + "==============");
						rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(s, "###,###,##0.00"), ref 14));
						rpt.Send();
						rpt.Send();
						ln = ln + 4;
						hdflag = false;
					}
					s = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//sd = LH(3, j, k).Description
					//End If
					//If InStr("A", LH(3, j, k).type) > 0 Then
					if (Consolidated) {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(3, j, k).key), StartDate, EndDate)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(3, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND AccountNumber = " & LH(3, j, k).key & " AND Accounts.Key = " & LH(3, j, k).key
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//a = AcctBal(Company, Val(LH(3, j, k).key), StartDate, EndDate, Department)
						//UPGRADE_WARNING: Couldn't resolve default property of object LH(3, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object LH().key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = "SELECT Balances.*, Accounts.Description FROM Balances, Accounts WHERE CompanyNumber = " & Company & " AND DepartmentNumber = " & Department & " AND AccountNumber = " & LH(3, j, k).key & " AND Accounts.Key = " & LH(3, j, k).key
					}
					//Set tblBalance = GLdb.OpenRecordset(xsql)
					//If tblBalance.RecordCount > 0 Then
					//aname.Value = LH(3, j, k).Description
					//    While Not tblBalance.EOF
					//        For i = 1 To rptMonth
					//            a = a + tblBalance(fldName & Format(i, "00"))
					//        Next
					//        tblBalance.MoveNext
					//    Wend
					//End If
					//UPGRADE_WARNING: Couldn't resolve default property of object LH(3, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If LH(3, j, k).key = PLAccount Then
					a = a - ((-iTotal) - eTotal);
					aname.Value = PLAccountName;
					//End If
					if (a != 0) {
						if (ln > rpt.PageSize - 12) {
							rpt.NewPage();
							heading();
						}
						if (!hdflag) {
							//UPGRADE_WARNING: Couldn't resolve default property of object sd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							rpt.Send(Strings.Space(9) + sd);
							// LH(3, j, k).description
							rpt.Send();
							ln = ln + 2;
							hdflag = true;
						}
						rpt.Send(Strings.Space(5) + WinGlobal.rtpad(ref aname.Value, ref 31) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-a, "###,###,##0.00"), ref 14));
						ln = ln + 1;
						qTotal = qTotal + a;
						s = s + a;
						ln = ln + 1;
					}
					//End If
				}
			}
			if (s != 0) {
				rpt.Send(Strings.Space(36) + "==============");
				rpt.Send(Strings.Space(36) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-s, "###,###,##0.00"), ref 14));
				rpt.Send();
				ln = ln + 3;
				s = 0;
			}
			if (ln > rpt.PageSize - 13) {
				rpt.NewPage();
				heading();
			}
			rpt.Send(Strings.Space(51) + "==============");
			rpt.Send(Strings.Space(51) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-qTotal, "###,###,##0.00"), ref 14));
			rpt.Send();
			rpt.Send(Strings.Space(65) + "==============");
			rpt.Send(Strings.Space(65) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(-(lTotal + qTotal), "###,###,##0.00"), ref 14));
			rpt.EndDoc();

		}
		public void heading()
		{

			if (rpt.Target == 0 & pg > 0)
				return;
			pg = pg + 1;
			rpt.Send(Strings.Space(30) + "BALANCE SHEET         page " + pg);
			rpt.Send(Strings.Space(30) + CompanyName);
			rpt.Send(Strings.Space(30) + "For Month of " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.DateSerial(xYear, rptMonth, 1), "mmmm, yyyy"));
			rpt.Send();
			rpt.Send();
			rpt.Send();
			ln = 6 + WinglGlobal.tmargin;

		}
		public void BalanceSheet(ref bool Batch)
		{

			Printer prt = null;

			if (Batch) {
				WinglGlobal.getLedgerStructure();
				rpt = new clsReport();
				rpt.Batch = true;
				rpt.Target = 1;
				rpt.ReportName = "BalanceSheet";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmBalanceSheet();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					WinglGlobal.getLedgerStructure();
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "BalanceSheet";
					rpt.rptObject = this;
					rpt.FontName(ref "Courier New");
					rpt.FontSize(ref 12);
					print_report();
				}
			}

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsBalanceSheet();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.BalanceSheet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.BalanceSheet(false);
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
		public void Serialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[0] = "BS";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = Consolidated;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = PLAccount;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = rptMonth;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = rptYear;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = PLAccountName;

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
			PLAccount = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptMonth = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptYear = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CompanyName = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			PLAccountName = WinglGlobal.BatchRptSave[8];

		}
		public void setDates()
		{

			rptMonth = WinglGlobal.bMonth;
			//rptYear = bYear

		}
	}
}
