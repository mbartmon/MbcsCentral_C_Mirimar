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
	internal class clsPayrollReport
	{
		private frmPayrollReport frm;
		private bool first;
		private clsReport rpt;
		private short pg;
		private short ln;
//UPGRADE_WARNING: Arrays in structure tblChecks may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private object[] IndexArray;
		private DAO.Recordset tblChecks;
		private int xdpt;
		private int xcmp;
		private int xVnd;
		private short r;
		private decimal[] amt = new decimal[10];
		private decimal[] vamt = new decimal[10];
		private decimal[] yamt = new decimal[10];
//UPGRADE_WARNING: Arrays in structure tblAccounts may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblAccounts;
//local variable(s) to hold property value(s)
			//local copy
		private System.DateTime mvarStartDate;
			//local copy
		private System.DateTime mvarEndDate;
			//local copy
		private bool mvarYTD;
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
		public bool YTD {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.YTD
			get { return mvarYTD; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.YTD = 5
			set { mvarYTD = value; }
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
		public void print_report()
		{

			string xsql = null;
			string x = null;
			short i = 0;
			System.DateTime sdate = default(System.DateTime);
			decimal net = default(decimal);

			tblAccounts = WinglGlobal.GLdb.OpenRecordset("PRaccounts");
			tblAccounts.MoveFirst();

			first = true;
			pg = 0;
			if (YTD) {
				sdate = Convert.ToDateTime("01/01/" + DateAndTime.Year(EndDate));
			} else {
				sdate = StartDate;
			}
			xsql = "SELECT Company, Department, CheckNumber, [Vendor/Account] FROM APChecks, Vendors WHERE APChecks.Sequence = 0 AND APChecks.Date BETWEEN #" + sdate + "# AND #" + EndDate + "# AND Vendors.VendorNumber = APChecks.[Vendor/Account] AND Vendors.Payroll = True ORDER BY APChecks.Company, APChecks.Department, APChecks.[Vendor/Account], APChecks.Date";
			tblChecks = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblChecks.RecordCount > 0) {
				tblChecks.MoveLast();
				//ReDim IndexArray(tblChecks.RecordCount - 1, 3)
				tblChecks.MoveFirst();
				r = 0;
				while (!tblChecks.EOF) {
					for (i = 0; i <= 3; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//IndexArray(r, i) = tblChecks.Fields(i).Value
					}
					r = r + 1;
					tblChecks.MoveNext();
				}
				tblChecks.Close();
			} else {
				return;
			}

			for (r = 0; r <= Information.UBound(IndexArray, 1); r++) {

				if (first) {
					//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					System.Array.Clear(amt, 0, amt.Length);
					//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					System.Array.Clear(vamt, 0, vamt.Length);
					//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					System.Array.Clear(yamt, 0, yamt.Length);
					//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//xcmp = IndexArray(r, 0)
					//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//xdpt = IndexArray(r, 1)
					//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//xVnd = IndexArray(r, 3)
					heading();
					dpt_heading();
					vnd_heading();
					first = false;
				}

				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If xcmp <> IndexArray(r, 0) Then
				//Call vtotal()
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xcmp = IndexArray(r, 0)
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xdpt = IndexArray(r, 1)
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xVnd = IndexArray(r, 3)
				//rpt.NewPage()
				//Call heading()
				//Call dpt_heading()
				//Call vnd_heading()
				//End If

				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If xdpt <> IndexArray(r, 1) Then
				vtotal();
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xdpt = IndexArray(r, 1)
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xVnd = IndexArray(r, 3)
				//Call dpt_heading()
				//Call vnd_heading()
				//End If

				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If xVnd <> IndexArray(r, 3) Then
				//Call vtotal()
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//xVnd = IndexArray(r, 3)
				//Call vnd_heading()
				//End If

				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(r, 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblChecks = GLdb.OpenRecordset("SELECT * FROM APChecks WHERE Company = " & IndexArray(r, 0) & " AND Department = " & IndexArray(r, 1) & " AND CheckNumber = " & IndexArray(r, 2) & " AND Sequence <> 0")
				if (tblChecks.RecordCount > 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object IndexArray(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = VB6.Format(tblChecks.Fields("Date").Value, "mm/dd/yy") & " " & VB6.Format(IndexArray(r, 2), "000000")
					while (!tblChecks.EOF) {
						i = getaccount(ref tblChecks.Fields["Vendor/Account"].Value);
						if (tblChecks.Fields["Date"].Value >= StartDate) {
							amt[i] = amt[i] + tblChecks.Fields["Amount"].Value;
							vamt[i] = vamt[i] + tblChecks.Fields["Amount"].Value;
							yamt[i] = yamt[i] + tblChecks.Fields["Amount"].Value;
						} else {
							yamt[i] = yamt[i] + tblChecks.Fields["Amount"].Value;
						}
						tblChecks.MoveNext();
					}
					net = 0;
					for (i = 0; i <= 9; i++) {
						x = x + " " + WinglGlobal.fmt(ref amt[i], ref 9);
						net = net + amt[i];
					}
					if (net != 0) {
						if (ln > rpt.PageSize - 11) {
							heading();
						}
						x = x + " " + WinglGlobal.fmt(ref net, ref 9);
						rpt.Send(x);
						ln = ln + 1;
					}
					//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					System.Array.Clear(amt, 0, amt.Length);
				}
			}
			vtotal();
			rpt.EndDoc();

		}
		public void vtotal()
		{

			short i = 0;
			string x = null;
			decimal net = default(decimal);

			net = 0;
			x = Strings.Space(7) + "TOTALS=>";
			for (i = 0; i <= 9; i++) {
				x = x + WinglGlobal.fmt(ref vamt[i], ref 10);
				net = net + vamt[i];
			}
			x = x + WinglGlobal.fmt(ref net, ref 10);
			rpt.Send();
			rpt.Send(x);
			if (YTD) {
				net = 0;
				x = Strings.Space(3) + "YTD TOTALS=>";
				for (i = 0; i <= 9; i++) {
					x = x + WinglGlobal.fmt(ref yamt[i], ref 10);
					net = net + yamt[i];
				}
				x = x + WinglGlobal.fmt(ref net, ref 10);
				rpt.Send(x);
				ln = ln + 1;
			}
			rpt.Send();
			rpt.Send();
			ln = ln + 4;
			//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			System.Array.Clear(amt, 0, amt.Length);
			//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			System.Array.Clear(vamt, 0, vamt.Length);
			//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			System.Array.Clear(yamt, 0, yamt.Length);

		}
		private void heading()
		{

			pg = pg + 1;
			rpt.Send(Strings.Space(55) + "PAYROLL REPORT        page " + pg);
			rpt.Send(Strings.Space(55) + StartDate + " - " + EndDate);
			rpt.Send();
			rpt.Send(Strings.Space(55) + "COMPANY: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xcmp, "##0") + " " + funcCompanyName(ref xcmp));
			rpt.Send();
			rpt.Send();
			ln = 6 + WinglGlobal.tmargin;

		}
		private void dpt_heading()
		{

			rpt.Send("Department: " + xdpt + " " + funcDepartmentName(ref xdpt));
			rpt.Send();
			ln = ln + 2;

		}
		private void vnd_heading()
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT VendorName, Street, City, State, Zip, FederalID FROM Vendors WHERE VendorNumber = " + xVnd);
			if (tbl.RecordCount > 0) {
				rpt.Send("VENDOR #" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xVnd, "#####"), ref 5) + "  Name: " + WinGlobal.rtpad(ref WinGlobal.noNulls(ref tbl.Fields["VendorName"]), ref 50) + " S.S.#:" + tbl.Fields["FederalID"].Value);
				rpt.Send(Strings.Space(21) + WinGlobal.noNulls(ref tbl.Fields["Street"]));
				rpt.Send(Strings.Space(21) + WinGlobal.noNulls(ref Strings.Trim(tbl.Fields["City"].Value)) + ", " + WinGlobal.noNulls(ref Strings.Trim(tbl.Fields["State"].Value)) + " " + WinGlobal.noNulls(ref Strings.Trim(tbl.Fields["Zip"].Value)));
				rpt.Send();
				ln = ln + 4;
				tbl.Close();
			} else {
				rpt.Send("*VENDOR NOT ON FILE *");
				ln = ln + 1;
			}
			Columns();

		}
		private void Columns()
		{

			rpt.Send("  Date   Check#    Salary      S.S.  Medicare   Federal     State      City   DIS/u/i  Yonk/Fam  Med/Empl     Other       Net");
			ln = ln + 1;

		}
		public string funcCompanyName(ref int c)
		{
			string functionReturnValue = null;

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Company WHERE CompanyNumber = " + c);
			if (tbl.RecordCount > 0) {
				functionReturnValue = tbl.Fields["Name"].Value;
				tbl.Close();
			} else {
				functionReturnValue = "* Not Found *";
			}
			return functionReturnValue;

		}
		public string funcDepartmentName(ref int d)
		{
			string functionReturnValue = null;

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Department WHERE [Department Number] = " + d);
			if (tbl.RecordCount > 0) {
				functionReturnValue = tbl.Fields["Name"].Value;
				tbl.Close();
			} else {
				functionReturnValue = "* Not Found *";
			}
			return functionReturnValue;

		}
		public int getaccount(ref int a)
		{
			int functionReturnValue = 0;

			short i = 0;

			for (i = 0; i <= 17; i++) {
				if (a == tblAccounts.Fields["Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value) {
					functionReturnValue = Convert.ToInt16(i / 2);
					return functionReturnValue;
				}
			}
			functionReturnValue = 9;
			return functionReturnValue;

		}
		public void PayrollReport()
		{

			frm = new frmPayrollReport();
			frm.clas = this;
			frm.ShowDialog();
			//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			frm = null;
			if (!Cancel) {
				rpt = new clsReport();
				rpt.Target = 0;
				rpt.ReportName = "PayrollReport";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
			}

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsPayrollReport();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.PayrollReport. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.PayrollReport();
				//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				xrpt = null;
			}

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
	}
}
