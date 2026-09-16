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
	internal class clsLedger
	{
// private local variables
		private frmLedger frm;
		private clsReport rpt;
		private short ln;
		private short pg;
		private short tb;
//UPGRADE_WARNING: Arrays in structure tblDepartment may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblCompany may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblCompany;
		private DAO.Recordset tblDepartment;
//UPGRADE_WARNING: Arrays in structure tblCats may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblTrans may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblAccount may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblAccount;
		private DAO.Recordset tblTrans;
		private DAO.Recordset tblCats;
		private short m;
		private short l;
		private object[] dpts;
		private decimal debtotal;
		private decimal crdtotal;
		private short cfy;
		private short fye;
		private System.DateTime cfystart;
//local variable(s) to hold property value(s)
			//local copy
		private int mvarCompany;
			//local copy
		private bool mvarConsolidated;
			//local copy
		private int mvarDepartment;
			//local copy
		private int mvarAccount;
			//local copy
		private System.DateTime mvarStartDate;
			//local copy
		private System.DateTime mvarEndDate;
			//local copy
		private bool mvarCancel;
			//local copy
		private bool mvarZeroBalances;
			//local copy
		private string mvarCompanyName;
			//local copy
		private string mvarDepartmentName;
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
		public bool ZeroBalances {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ZeroBalances
			get { return mvarZeroBalances; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.ZeroBalances = 5
			set { mvarZeroBalances = value; }
		}
		public bool Cancel {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Cancel
			get { return mvarCancel; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Cancel = 5
			set { mvarCancel = value; }
		}
		public System.DateTime EndDate {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.EndDate
			get { return mvarEndDate; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.EndDate = 5
			set { mvarEndDate = value; }
		}
		public System.DateTime StartDate {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.StartDate
			get { return mvarStartDate; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.StartDate = 5
			set { mvarStartDate = value; }
		}
		public int Account {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Account
			get { return mvarAccount; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Account = 5
			set { mvarAccount = value; }
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
			object k = null;
			object j = null;
			object i = null;

			bool acctfound = false;
			short d = 0;

			getcompany();
			acctfound = false;
			if (!Consolidated) {
				if (Department == -1) {
					getdepts();
				} else {
					//ReDim dpts(2, 1)
					//UPGRADE_WARNING: Couldn't resolve default property of object dpts(0, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//dpts(0, 0) = Department
					//UPGRADE_WARNING: Couldn't resolve default property of object dpts(1, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//dpts(1, 0) = DepartmentName
				}
			}
			pg = 0;
			reportHeading();
			if (Account != -1) {
				for (i = 1; i <= 5; i++) {
					for (j = 0; j <= 10; j++) {
						for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
							//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//If InStr("A", LH(i, j, k).type) > 0 Then
							//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object LH(i, j, k).key. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//If LH(i, j, k).key = Account Then
							//acctfound = True
							//Exit For
							//End If
							//End If
						}
						if (acctfound)
							break; // TODO: might not be correct. Was : Exit For
					}
					if (acctfound)
						break; // TODO: might not be correct. Was : Exit For
				}
				if (acctfound) {
					if (!Consolidated) {
						for (d = 0; d <= Information.UBound(dpts, 2) - 1; d++) {
							//UPGRADE_WARNING: Couldn't resolve default property of object dpts(1, d). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//rpt.Send(Space(tb * rpt.Target) & "Dept: " & dpts(1, d)) : rpt.Send() : l = l + 2
							//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//Call ProcessAccount(LH(i, j, k).Description, LH(i, j, k).key, LH(i, j, k).type, d)
						}
					} else {
						rpt.Send(Strings.Space(tb * rpt.Target) + "* CONSOLIDATED *");
						rpt.Send();
						l = l + 2;
						//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//Call ProcessAccount(LH(i, j, k).Description, LH(i, j, k).key, LH(i, j, k).type, -1)
					}
				} else {
					rpt.Send(Strings.Space(tb * rpt.Target) + "    **** Account Not Found ****");
					l = l + 1;
				}
			} else {
				if (!Consolidated) {
					for (d = 0; d <= Information.UBound(dpts, 2) - 1; d++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object dpts(1, d). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//rpt.Send(Space(tb * rpt.Target) & "Dept: " & dpts(1, d)) : rpt.Send() : l = l + 2
						for (i = 1; i <= 5; i++) {
							for (j = 0; j <= 10; j++) {
								for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//If InStr("A", LH(i, j, k).type) > 0 Then
									//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//Call ProcessAccount(LH(i, j, k).Description, LH(i, j, k).key, LH(i, j, k).type, d)
									//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(i, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//CatTotals(i, 1) = CatTotals(i, 1) + (debtotal - crdtotal)
									//End If
								}
							}
						}
						rpt.NewPage();
						reportHeading();
						//UPGRADE_WARNING: Couldn't resolve default property of object dpts(1, d). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//rpt.Send(Space(tb * rpt.Target) & "Dept: " & dpts(1, d)) : rpt.Send() : l = l + 2
						for (i = 1; i <= 5; i++) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							rpt.Send(Strings.Space(tb * rpt.Target) + "TOTAL " + WinGlobal.rtpad(ref Strings.Trim(WinglGlobal.CatTotals[i, 2]), ref 50) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.CatTotals[i, 1], "$###,###,##0.00  ;$###,###,##0.00CR"), ref 18));
							rpt.Send();
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(i, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							WinglGlobal.CatTotals[i, 1] = 0;
						}
						if (d != Information.UBound(dpts, 2) - 1) {
							rpt.NewPage();
							reportHeading();
						}
					}
				} else {
					rpt.Send("* CONSOLIDATED *");
					rpt.Send();
					l = l + 2;
					for (i = 1; i <= 5; i++) {
						for (j = 0; j <= 10; j++) {
							for (k = 0; k <= Information.UBound(WinglGlobal.LH, 3); k++) {
								//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//If InStr("A", LH(i, j, k).type) > 0 Then
								//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//Call ProcessAccount(LH(i, j, k).Description, LH(i, j, k).key, LH(i, j, k).type, -1)
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(i, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//CatTotals(i, 1) = CatTotals(i, 1) + (debtotal - crdtotal)
								//End If
							}
						}
					}
					rpt.NewPage();
					reportHeading();
					rpt.Send(Strings.Space(tb * rpt.Target) + "* CONSOLIDATED *");
					rpt.Send();
					l = l + 2;
					for (i = 1; i <= 5; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						rpt.Send(Strings.Space(tb * rpt.Target) + "TOTAL " + WinGlobal.rtpad(ref Strings.Trim(WinglGlobal.CatTotals[i, 2]), ref 50) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.CatTotals[i, 1], "$###,###,##0.00  ;$###,###,##0.00CR"), ref 18));
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object CatTotals(i, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.CatTotals[i, 1] = 0;
						rpt.Send();
					}
				}
			}
			rpt.EndDoc();

		}
		public void ProcessAccount(ref object d, ref object k, ref object t, ref object p)
		{
			object s = null;
			object i = null;

			string crd = null;
			string xsql = null;
			string deb = null;
			decimal startbalance = default(decimal);
			string desc = null;
			string desc2 = null;
			bool acctDesc = false;
			short n = 0;

			crdtotal = 0;
			debtotal = 0;
			acctDesc = false;
			//xsql = "SELECT SUM(Amount) as startbalance FROM Transactions WHERE CompanyNumber = " & Company
			//If p <> -1 Then
			//    xsql = xsql & " AND DepartmentNumber = " & dpts(0, p)
			//End If
			//xsql = xsql & " AND AccountNumber = " & Val(k)
			//xsql = xsql & " AND Date < #" & DateValue(StartDate) & "#"
			//xsql = xsql & " AND Date >= #" & cfystart & "#"
			//Set tblTrans = GLdb.OpenRecordset(xsql)
			//If Not IsNull(tblTrans("startbalance")) Then
			//    startbalance = tblTrans("startbalance")
			//Else
			//    startbalance = 0
			//End If
			//tblTrans.Close
			//If startbalance <> 0 Then
			//    If startbalance < 0 Then
			//        crd = lpad(Format(Abs(startbalance), "########0.00"), 12): deb = String(12, " ")
			//    Else
			//        deb = lpad(Format(startbalance, "########0.00"), 12): crd = String(12, " ")
			//    End If
			//    GoSub acctheading
			//    acctDesc = True
			//    rpt.Send Space(tb * rpt.Target) & Space(55) & "Opening Balance     " & deb & " | " & crd
			//    l = l + 1
			//    debtotal = Val(deb): crdtotal = Val(crd)
			//Else
			if (ZeroBalances) {
				//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
				//GoSub acctheading
				acctDesc = true;
			}
			//End If

			xsql = "SELECT * FROM Transactions WHERE CompanyNumber = " + Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (p != -1) {
				//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object dpts(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xsql = xsql & " AND DepartmentNumber = " & dpts(0, p)
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " AND AccountNumber = " + Conversion.Val(k);
			//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			xsql = xsql + " AND Date BETWEEN #" + DateAndTime.DateValue(Convert.ToString(StartDate)) + "# AND #" + DateAndTime.DateValue(Convert.ToString(EndDate)) + "#";
			xsql = xsql + " ORDER BY Date, DepartmentNumber";

			tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblTrans.RecordCount > 0) {
				if (!acctDesc) {
					//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
					//GoSub acctheading
					acctDesc = true;
				}
				while (!tblTrans.EOF) {
					desc = Strings.Trim(WinGlobal.noNulls(ref tblTrans.Fields["Description"]));
					desc2 = "";
					if (Strings.Len(desc) > 30) {
						n = Strings.InStr(Strings.LCase(desc), "invoice#");
						if (n > 0) {
							desc = Strings.Left(desc, n - 1) + "Inv#" + Strings.Mid(desc, n + 8);
						}
						for (i = 31; i >= 1; i += -1) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							if (Strings.Mid(desc, i, 1) == " ") {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								desc2 = Strings.Mid(desc, i + 1);
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								desc = Strings.Left(desc, i - 1);
								break; // TODO: might not be correct. Was : Exit For
							}
						}
					}
					if (tblTrans.Fields["Amount"].Value < 0) {
						crd = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(System.Math.Abs(tblTrans.Fields["Amount"].Value), "########0.00"), ref 12);
						deb = new string(" ", 12);
					} else {
						deb = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTrans.Fields["Amount"].Value, "########0.00"), ref 12);
						crd = new string(" ", 12);
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					s = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTrans.Fields["Date"].Value, "mm/dd/yy") + "  " + WinGlobal.lpad(ref WinGlobal.noNulls(ref tblTrans.Fields["CheckNumber"]), ref 6);
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					s = s + " " + WinGlobal.rtpad(ref Strings.Trim(WinGlobal.noNulls(ref tblTrans.Fields["Journal"])), ref 8) + " " + WinGlobal.rtpad(ref Strings.Trim(Strings.Left(WinGlobal.noNulls(ref tblTrans.Fields["Source"]), 17)), ref 17) + " " + WinGlobal.rtpad(ref Strings.Trim(desc), ref 30);
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					s = s + " " + deb + " | " + crd;
					if (l > rpt.PageSize - 12) {
						rpt.NewPage();
						reportHeading();
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					rpt.Send(Strings.Space(tb * rpt.Target) + s);
					l = l + 1;
					if (!string.IsNullOrEmpty(Strings.Trim(desc2))) {
						rpt.Send(Strings.Space(tb * rpt.Target) + Strings.Space(44) + WinGlobal.rtpad(ref desc2, ref 44) + "|");
						l = l + 1;
					}
					debtotal = debtotal + Conversion.Val(deb);
					crdtotal = crdtotal + Conversion.Val(crd);
					tblTrans.MoveNext();
				}
			}
			if (acctDesc | (ZeroBalances & acctDesc)) {
				if (!acctDesc) {
					//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
					//GoSub acctheading
				}
				rpt.Send(Strings.Space(tb * rpt.Target) + Strings.Space(75) + "============ | ============");
				//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				s = Strings.Space(58) + "ACCOUNT TOTALS=> " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(debtotal, "########0.00"), ref 12) + " | " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(crdtotal, "########0.00"), ref 12);
				//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				s = s + " " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(debtotal - crdtotal, "########0.00  ;########0.00CR"), ref 14);
				//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				rpt.Send(Strings.Space(tb * rpt.Target) + s);
				rpt.Send(new string("-", 132));
				l = l + 3;
			}
			goto ProcessAccount_exit;
			acctheading:

			if (l > rpt.PageSize - 14) {
				rpt.NewPage();
				reportHeading();
			}
			rpt.FontBold(ref true);
			//UPGRADE_WARNING: Couldn't resolve default property of object d. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rpt.Send(Strings.Space(tb * rpt.Target) + d);
			rpt.Send();
			rpt.FontBold(ref false);
			rpt.Send(Strings.Space(tb * rpt.Target) + "  date    check# journal  source            description                         debit   |   credit");
			rpt.Send(Strings.Space(tb * rpt.Target) + "                                                                           ------------ | ------------");
			l = l + 4;
			//UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			return;
			ProcessAccount_exit:


		}
		public void reportHeading()
		{

			pg = pg + 1;
			rpt.Send(Strings.Space(tb * rpt.Target) + DateAndTime.Today);
			rpt.Send(Strings.Space(tb * rpt.Target) + CompanyName);
			rpt.Send(Strings.Space(tb * rpt.Target) + "GENERAL LEDGER");
			rpt.Send(Strings.Space(tb * rpt.Target) + StartDate + " to " + EndDate);
			rpt.Send(Strings.Space(tb * rpt.Target) + "Page " + pg);
			rpt.Send();
			rpt.Send();
			l = 7 + WinglGlobal.tmargin;

		}
		public void getcompany()
		{
			object fys = null;

			System.DateTime sdate = default(System.DateTime);

			cfystart = WinglGlobal.CurrentFiscalYear(ref Company);
			//UPGRADE_WARNING: Couldn't resolve default property of object fys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			fys = DateAndTime.Month(cfystart);
			//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sdate = DateAndTime.DateValue(Convert.ToString(StartDate));
			if (sdate < cfystart) {
				//UPGRADE_WARNING: Couldn't resolve default property of object fys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (DateAndTime.Month(sdate) < fys) {
					//UPGRADE_WARNING: Couldn't resolve default property of object fys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					cfystart = DateAndTime.DateSerial(DateAndTime.Year(StartDate) - 1, fys, 1);
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object fys. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					cfystart = DateAndTime.DateSerial(DateAndTime.Year(StartDate), fys, 1);
				}
			}

		}
		public void getdepts()
		{

			//UPGRADE_WARNING: Arrays in structure tbldpt may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbldpt = null;
			short i = 0;

			tbldpt = WinglGlobal.GLdb.OpenRecordset("SELECT [Department Number], Name FROM Department WHERE [Company Number] = " + Company);
			if (tbldpt.RecordCount > 0) {
				tbldpt.MoveLast();
				//ReDim dpts(2, tbldpt.RecordCount)
				tbldpt.MoveFirst();
				i = 0;
				while (!tbldpt.EOF) {
					//UPGRADE_WARNING: Couldn't resolve default property of object dpts(0, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//dpts(0, i) = tbldpt.Fields("Department Number").Value
					//UPGRADE_WARNING: Couldn't resolve default property of object dpts(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//dpts(1, i) = tbldpt.Fields("Name").Value
					tbldpt.MoveNext();
					i = i + 1;
				}
				tbldpt.Close();
			}

		}
		public void Ledger(ref bool Batch)
		{

			WinglGlobal.getLedgerStructure();
			tb = 5;

			if (Batch) {
				rpt = new clsReport();
				rpt.Batch = true;
				rpt.Target = 1;
				rpt.ReportName = "GeneralLedger";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmLedger();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "GeneralLedger";
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
			WinglGlobal.BatchRptSave[0] = "GL";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = Consolidated;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = Account;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = StartDate;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = EndDate;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = ZeroBalances;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[9] = DepartmentName;

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
			Account = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			StartDate = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			EndDate = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ZeroBalances = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CompanyName = WinglGlobal.BatchRptSave[8];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			DepartmentName = WinglGlobal.BatchRptSave[9];

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsLedger();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.Ledger. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.Ledger(false);
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

			StartDate = WinglGlobal.bStartDate;
			EndDate = WinglGlobal.bEndDate;

		}
	}
}
