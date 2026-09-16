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
	internal class clsDisbRpt
	{
// private local variables
		private frmDisbRpt frm;
		private clsReport rpt;
		private short pg;
		private short ln;
		private decimal ytotal;
		private decimal ctotal;
		private decimal gtotal;
		private decimal nb;
		private decimal ob;
		private decimal cb;
		private int xacct;
		private string cdesc;
		private bool posted;
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
		private short mvarMonth;
			//local copy
		private short mvarYear;
			//local copy
		private int mvarBalanceAccount;
			//local copy
		private int mvarCollectionAccount1;
			//local copy
		private int mvarCollectionAccount2;
			//local copy
		private bool mvarCancel;
		public bool Cancel {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Cancel
			get { return mvarCancel; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.Cancel = 5
			set { mvarCancel = value; }
		}
		public int CollectionAccount2 {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CollectionAccount2
			get { return mvarCollectionAccount2; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.CollectionAccount2 = 5
			set { mvarCollectionAccount2 = value; }
		}
		public int CollectionAccount1 {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CollectionAccount1
			get { return mvarCollectionAccount1; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.CollectionAccount1 = 5
			set { mvarCollectionAccount1 = value; }
		}
		public int BalanceAccount {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.BalanceAccoount
			get { return mvarBalanceAccount; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.BalanceAccoount = 5
			set { mvarBalanceAccount = value; }
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
			object screenwidth = null;
			object payto = null;
			object i = null;
			object xsql = null;

			short yr = 0;
			string stdate = null;
			string endate = null;
			bool first = false;
			//UPGRADE_WARNING: Arrays in structure tblbal may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tbltran may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblacct = null;
			DAO.Recordset tbltran = null;
			DAO.Recordset tblbal = null;
			string amt = null;

			if (rptYear == 0) {
				yr = DateAndTime.Year(DateAndTime.Today);
			} else {
				yr = DateAndTime.Year(DateAndTime.Now) - 1;
			}
			stdate = "#01/01/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr, "####") + "#";
			endate = "#" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(rptMonth, "00") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.md[WinglGlobal.leapyear(ref yr), rptMonth], "##") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(yr, "####") + "#";

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " AND AccountNumber = " + BalanceAccount;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblbal = WinglGlobal.GLdb.OpenRecordset(xsql);
			ob = 0;
			if (tblbal.RecordCount > 0) {
				if (rptMonth > 1) {
					ob = tblbal.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(rptMonth - 1, "00")].Value;
				} else {
					ob = tblbal.Fields["P_Month12"].Value;
				}
				tblbal.Close();
			}
			cb = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " AND AccountNumber = " + CollectionAccount1;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblbal = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblbal.RecordCount > 0) {
				cb = -(tblbal.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(rptMonth, "00")]).Value;
				tblbal.Close();
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " AND AccountNumber = " + CollectionAccount2;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblbal = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblbal.RecordCount > 0) {
				//cb = cb + (-(tblbal.Fields("C_Month" & VB6.Format(rptMonth, "00")))).Value
				tblbal.Close();
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT Transactions.*, Accounts.Description AS AccountName FROM Transactions, Accounts WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " AND Date BETWEEN " + stdate + " AND " + endate + " AND (";

			first = true;
			for (i = 0; i <= 9; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (Strings.Trim(WinglGlobal.Journals[i]) > "") {
					if (!first) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + " OR ";
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + "Journal LIKE '*" + Strings.Trim(WinglGlobal.Journals[i]) + "'";
					first = false;
				}
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + ") AND Accounts.Key = AccountNumber ORDER BY Accounts.Description, CheckNumber";

			first = true;
			ctotal = 0;
			ytotal = 0;
			gtotal = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tbltran = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tbltran.RecordCount >= 0) {
				pg = 0;
				heading();
				while (!tbltran.EOF) {
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT MajorCategory FROM Accounts WHERE Key = " + tbltran.Fields["AccountNumber"].Value);
					if (tblacct.RecordCount > 0 & tblacct.Fields["MajorCategory"].Value == "005_") {
						if (!first & xacct != tbltran.Fields["AccountNumber"].Value) {
							if (ctotal != 0 | ytotal != 0) {
								CategoryTotal();
							}
							xacct = tbltran.Fields["AccountNumber"].Value;
						}
						if (first) {
							xacct = tbltran.Fields["AccountNumber"].Value;
							first = false;
						}
						cdesc = tbltran.Fields["AccountName"].Value;
						if (ln > rpt.PageSize - 5) {
							rpt.NewPage();
							heading();
						}
						ytotal = ytotal + tbltran.Fields["Amount"].Value;

						if (DateAndTime.Month(tbltran.Fields["Date"].Value) == rptMonth & tbltran.Fields["Amount"].Value != 0) {
							//UPGRADE_WARNING: Couldn't resolve default property of object payto. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							payto = WinGlobal.noNulls(ref tbltran.Fields["Description"]);
							//UPGRADE_WARNING: Couldn't resolve default property of object payto. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							i = Strings.InStr(payto, ", Invoice");
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							if (i > 0) {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object payto. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								payto = Strings.Left(payto, i - 1);
							}
							//UPGRADE_WARNING: Couldn't resolve default property of object payto. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							payto = WinGlobal.rtpad(ref Strings.Left(payto, 15), ref 15);
							//UPGRADE_WARNING: Couldn't resolve default property of object payto. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = Strings.Space(5) + WinGlobal.rtpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbltran.Fields["Date"].Value, "mm/dd/yyyy"), ref 11) + payto;
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = xsql + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbltran.Fields["CheckNumber"].Value, "######"), ref 6) + "  ";
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = xsql + WinGlobal.rtpad(ref Strings.Left(tbltran.Fields["AccountName"].Value, 30), ref 30) + "  ";
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = xsql + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbltran.Fields["Amount"].Value, "########0.00"), ref 13);
							ctotal = ctotal + tbltran.Fields["Amount"].Value;
							gtotal = gtotal + tbltran.Fields["Amount"].Value;

							rpt.Send(xsql);
							ln = ln + 1;
						}
					}
					tbltran.MoveNext();
				}
				if (ctotal != 0)
					CategoryTotal();

				nb = ob - gtotal + cb;
				rpt.Send();
				rpt.Send(Strings.Space(20) + "  old balance       collections     disbursements     new balance");
				rpt.Send(Strings.Space(20) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ob, "########0.00"), ref 13) + "  +  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cb, "#########0.00"), ref 13) + "  -  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(gtotal, "#########0.00"), ref 13) + " = " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(nb, "#########0.00"), ref 13));
				rpt.EndDoc();

				if (!posted) {
					//UPGRADE_WARNING: Couldn't resolve default property of object screenwidth. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					amt = Strings.Trim(Interaction.InputBox("The amount to post the Balance Account for the current month: ", "Disbursement Report", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(nb, "#########0.00"), Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2), Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(screenwidth / 2)));
					if (!string.IsNullOrEmpty(amt)) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + " AND AccountNumber = " + BalanceAccount;
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tblbal = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tblbal.RecordCount > 0) {
							tblbal.Edit();
							tblbal.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(rptMonth, "00")].Value = Conversion.Val(amt);
							tblbal.Update();
							tblbal.Close();
						} else {
							tblbal = WinglGlobal.GLdb.OpenRecordset("Balances");
							tblbal.AddNew();
							tblbal.Fields["CompanyNumber"].Value = Company;
							tblbal.Fields["DepartmentNumber"].Value = Department;
							tblbal.Fields["AccountNumber"].Value = BalanceAccount;
							tblbal.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(rptMonth, "00")].Value = Conversion.Val(amt);
							tblbal.Update();
							tblbal.Close();
						}
						posted = true;
					}
				}
			}

		}
		private void heading()
		{

			pg = pg + 1;
			rpt.Send(Strings.Space(5) + "DISBURSEMENT REPORT" + Strings.Space(10) + "page " + pg);
			rpt.Send();
			rpt.Send(Strings.Space(5) + "month of    premises                                            prepared for");
			rpt.Send(Strings.Space(5) + Conversion.Str(rptMonth) + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Year(DateAndTime.Now) - rptYear, "####") + "     " + WinGlobal.rtpad(ref DepartmentName, ref 50) + "  " + CompanyName);
			rpt.Send();
			rpt.Send(Strings.Space(5) + "  date     payable to     check#     explanation                    item-amount category-total annual-to-date");
			rpt.Send(Strings.Space(5) + "---------------------------------------------------------------------------------------------------------------------");
			ln = 7 + WinglGlobal.tmargin;

		}
		private void CategoryTotal()
		{

			if (ctotal != 0) {
				rpt.Send(Strings.Space(42) + WinGlobal.rtpad(ref Strings.Left(cdesc, 40), ref 40) + "  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ctotal, "#########0.00"), ref 13) + "  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ytotal, "#########0.00"), ref 13));
				ln = ln + 1;
			}
			ctotal = 0;
			ytotal = 0;

		}
		public void DisbRpt(ref bool Batch)
		{

			if (Batch) {
				rpt = new clsReport();
				rpt.Target = 1;
				rpt.ReportName = "DisbRpt";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				rpt.Batch = true;
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmDisbRpt();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "DisbRpt";
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
			WinglGlobal.BatchRptSave[0] = "DR";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = DepartmentName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = rptMonth;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = rptYear;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = BalanceAccount;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = CollectionAccount1;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[9] = CollectionAccount2;

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
			DepartmentName = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptMonth = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptYear = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			BalanceAccount = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CollectionAccount1 = WinglGlobal.BatchRptSave[8];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CollectionAccount2 = WinglGlobal.BatchRptSave[9];

		}


//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{

			//    If Not rptCancel Then
			//        Set xrpt = New clsDisbRpt
			//        Call xrpt.DisbRpt(False)
			//        Set xrpt = Nothing
			//    End If

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
