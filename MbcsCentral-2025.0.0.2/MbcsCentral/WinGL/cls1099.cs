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
	internal class clsF1099
	{
		private frmF1099 frm;
		private clsReport rpt;
		private short pg;
		private short ln;
//UPGRADE_WARNING: Arrays in structure tblOneCheck may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblVendors may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblChecks may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblAccounts may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblAccounts;
		private DAO.Recordset tblChecks;
		private DAO.Recordset tblVendors;
		private DAO.Recordset tblOneCheck;
//UPGRADE_WARNING: Arrays in structure tblTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblTemp;
		private short tb;
		private string FedID;
		private string vName;
		private string CmpName;
		private int xDepartment;
		private int xVendor;
		private int xCompany;
		private decimal dTotal;
		private decimal ctotal;
		private decimal vtotal;
		private string Add3;
		private string Add1;
		private string Add2;
		private string Add4;
//local variable(s) to hold property value(s)
			//local copy
		private System.DateTime mvarStartDate;
			//local copy
		private System.DateTime mvarEndDate;
			//local copy
		private bool mvarCancel;
			//local copy
		private Collection mvarVendors = new Collection();
			//local copy
		private bool mvarDetail;
			//local copy
		private short mvarOrder;
		private bool mvarAddress;
		public bool Address {
			get { return mvarAddress; }
			set { mvarAddress = value; }
		}
		public short Order {
			get { return mvarOrder; }
			set { mvarOrder = value; }
		}
		public bool Detail {
			get { return mvarDetail; }
			set { mvarDetail = value; }
		}
		public Collection Vendors {
			get { return mvarVendors; }
			set { mvarVendors = value; }
		}
		public bool Cancel {
			get { return mvarCancel; }
			set { mvarCancel = value; }
		}
		public System.DateTime EndDate {
			get { return mvarEndDate; }
			set { mvarEndDate = value; }
		}
		public System.DateTime StartDate {
			get { return mvarStartDate; }
			set { mvarStartDate = value; }
		}
///******************************************************************************
		public void print_report()
		{
			object adesc = null;
			object l = null;
			object vendor = null;
			object xsql = null;
			///******************************************************************************

			if (rpt.Target == 0)
				tb = 0;
			else
				tb = 5;
			ctotal = 0;
			dTotal = 0;
			vtotal = 0;
			pg = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Apchecks WHERE Sequence = 0 AND Date BETWEEN #" + StartDate + "# AND #" + EndDate + "#";
			if (Vendors.Count() > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + "  AND (";
				foreach (object vendor_loopVariable in Vendors) {
					vendor = vendor_loopVariable;
					//UPGRADE_WARNING: Couldn't resolve default property of object vendor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object l. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					l = Strings.InStr(vendor, Strings.Chr(9));
					//UPGRADE_WARNING: Couldn't resolve default property of object l. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (l > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object l. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object vendor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + "[Vendor/Account] = " + Strings.Mid(vendor, l + 1) + " OR ";
					}
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = Strings.Left(xsql, Strings.Len(xsql) - 4) + ")";
			}

			if (Order == 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + " ORDER BY [Vendor/Account],Company, Department, CheckNumber";
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = xsql + " ORDER BY Company, Department, [Vendor/Account], CheckNumber";
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblChecks = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblChecks.RecordCount > 0) {
				heading();
				while (!tblChecks.EOF) {
					if (tblChecks.Fields["Company"].Value != xCompany) {
						if (ctotal != 0) {
							VendorTotal();
							DepartmentTotal();
							CompanyTotal();
						}
						//xVendor = tblChecks("[Vendor/Account]")
						//xDepartment = tblChecks("Department")
						xCompany = tblChecks.Fields["Company"].Value;
						CompanyHeader();
					}
					if (tblChecks.Fields["Department"].Value != xDepartment) {
						if (dTotal != 0) {
							VendorTotal();
							DepartmentTotal();
						}
						//xVendor = tblChecks("[Vendor/Account]")
						xDepartment = tblChecks.Fields["Department"].Value;
					}
					if (tblChecks.Fields[8].Value != xVendor) {
						if (vtotal != 0) {
							VendorTotal();
						}
						xVendor = tblChecks.Fields[8].Value;
						VendorHeader();
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT Company, Department, CheckNumber, SUM(Amount) AS cAmount FROM Apchecks WHERE Company = ";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + tblChecks.Fields["Company"].Value + " AND Department = " + tblChecks.Fields["Department"].Value + " AND CheckNumber = ";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + tblChecks.Fields["CheckNumber"].Value + " AND Sequence <> 0 GROUP BY Company, Department, CheckNumber";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblOneCheck = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tblOneCheck.RecordCount > 0) {
						if (Detail) {
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = "SELECT [Vendor/Account] FROM Apchecks WHERE Company = " + tblChecks.Fields["Company"].Value + " AND Department = ";
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = xsql + tblChecks.Fields["Department"].Value + " AND CheckNumber = " + tblChecks.Fields["CheckNumber"].Value + " AND Sequence = 1";
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblTemp = WinglGlobal.GLdb.OpenRecordset(xsql);
							if (tblTemp.RecordCount > 0) {
								tblAccounts = WinglGlobal.GLdb.OpenRecordset("SELECT Description FROM Accounts WHERE Key = " + tblTemp.Fields[0].Value);
								if (tblAccounts.RecordCount > 0) {
									//UPGRADE_WARNING: Couldn't resolve default property of object adesc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									adesc = tblAccounts.Fields["Description"].Value;
								} else {
									//UPGRADE_WARNING: Couldn't resolve default property of object adesc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									adesc = "* No Description *";
								}
							}
							if (ln >= rpt.PageSize - 12) {
								rpt.NewPage();
								heading();
							}
							//UPGRADE_WARNING: Couldn't resolve default property of object adesc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							rpt.Send(Strings.Space(tb) + WinGlobal.lpad(ref Convert.ToString(tblChecks.Fields["CheckNumber"].Value), ref 6) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblChecks.Fields["Date"].Value, "mm/dd/yyyy"), ref 16) + "  " + WinGlobal.rtpad(ref Convert.ToString(adesc), ref 62) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblOneCheck.Fields["cAmount"].Value, "#########.00"), ref 12));
							ln = ln + 1;
						}
						vtotal = vtotal + tblOneCheck.Fields["cAmount"].Value;
						dTotal = dTotal + tblOneCheck.Fields["cAmount"].Value;
						ctotal = ctotal + tblOneCheck.Fields["cAmount"].Value;
					}
					tblChecks.MoveNext();
				}
				VendorTotal();
				DepartmentTotal();
				CompanyTotal();
				rpt.EndDoc();
			}

		}
///******************************************************************************
		public void heading()
		{
			///******************************************************************************

			pg = pg + 1;
			rpt.Send(Strings.Space(tb) + "1099 REPORT   Page " + pg);
			rpt.Send(Strings.Space(tb) + DateAndTime.Today);
			rpt.Send("");
			rpt.Send("");
			rpt.Send(Strings.Space(tb) + "Check#          Date    Description                                                         Amount");
			rpt.Send(Strings.Space(tb) + "--------------------------------------------------------------------------------------------------");
			ln = 6 + WinglGlobal.tmargin;

		}
///******************************************************************************
		private void VendorHeader()
		{
			///******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			if (ln > rpt.PageSize - 16) {
				rpt.NewPage();
				heading();
			}

			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Vendors WHERE VendorNumber = " + xVendor);
			if (tbl.RecordCount > 0) {
				vName = tbl.Fields["VendorName"].Value;
				FedID = WinGlobal.noNulls(ref tbl.Fields["FederalID"]);
				Add1 = WinGlobal.noNulls(ref tbl.Fields["Street"]);
				Add2 = WinGlobal.noNulls(ref tbl.Fields["City"]);
				Add3 = WinGlobal.noNulls(ref tbl.Fields["State"]);
				Add4 = WinGlobal.noNulls(ref tbl.Fields["Zip"]);
			} else {
				vName = "* Vendor (" + xVendor + ") Not Found *";
				FedID = "";
			}

			rpt.Send(Strings.Space(tb) + "Vendor:" + vName);
			rpt.Send(Strings.Space(tb) + new string("-", 30));
			ln = ln + 2;

		}
///******************************************************************************
		private void VendorTotal()
		{
			///******************************************************************************

			if (ln > rpt.PageSize - 12) {
				rpt.NewPage();
				heading();
			}
			rpt.Send(Strings.Space(tb + 86) + "============");
			rpt.Send(Strings.Space(tb) + "Total/Vendor: " + WinGlobal.rtpad(ref Strings.Trim(vName) + "(" + FedID + ")", ref 72) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(vtotal, "#########.00"), ref 12));
			if (Address) {
				rpt.Send(Strings.Space(tb + 14) + Strings.Trim(Add1));
				rpt.Send(Strings.Space(tb + 14) + Strings.Trim(Add2) + ", " + Strings.Trim(Add3) + " " + Strings.Trim(Add4));
				ln = ln + 2;
			}
			rpt.Send(Strings.Space(tb + 14) + "for " + CmpName);
			rpt.Send("");
			vtotal = 0;
			ln = ln + 4;

		}
///******************************************************************************
		private void DepartmentTotal()
		{
			object x = null;
			///******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			if (ln > rpt.PageSize - 12) {
				rpt.NewPage();
				heading();
			}
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Department WHERE [Department Number] = " + xDepartment);
			if (tbl.RecordCount > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = tbl.Fields["Name"].Value;
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = "* Department (" + xDepartment + ") Not Found *";
			}
			rpt.Send(Strings.Space(tb + 86) + "------------");
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rpt.Send(Strings.Space(tb) + "Department: " + WinGlobal.rtpad(ref Convert.ToString(x), ref 74) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dTotal, "#########.00"), ref 12));
			rpt.Send("");
			dTotal = 0;
			ln = ln + 3;
		}
//******************************************************************************
		private void CompanyHeader()
		{
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			if (ln > rpt.PageSize - 16) {
				rpt.NewPage();
				heading();
			}
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Company WHERE CompanyNumber = " + xCompany);
			if (tbl.RecordCount > 0) {
				CmpName = tbl.Fields["Name"].Value;
			} else {
				CmpName = "* Company (" + xCompany + ") Not Found *";
			}
			rpt.Send();
			rpt.Send(Strings.Space(tb) + "Company: " + CmpName);
			rpt.Send(Strings.Space(tb) + "------------------------------");
			rpt.Send();
			ln = ln + 4;
			tbl.Close();
			//UPGRADE_NOTE: Object tbl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			tbl = null;

		}
///******************************************************************************
		private void CompanyTotal()
		{
			///******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			if (ln > rpt.PageSize - 12) {
				rpt.NewPage();
				heading();
			}
			rpt.Send(Strings.Space(tb + 86) + "------------");
			rpt.Send(Strings.Space(tb) + "Company: " + WinGlobal.rtpad(ref CmpName, ref 77) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(ctotal, "#########.00"), ref 12));
			rpt.Send(Strings.Space(tb) + new string("=", 98));
			ctotal = 0;
			ln = ln + 3;

		}
///******************************************************************************
		public void F1099(ref bool Batch)
		{
			///******************************************************************************

			if (Batch) {
				rpt = new clsReport();
				tb = 5;
				rpt.Target = 1;
				rpt.ReportName = "F1099";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				rpt.Batch = true;
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmF1099();
				frm.clas = this;
				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "F1099";
					rpt.rptObject = this;
					rpt.FontName(ref "Courier New");
					rpt.FontSize(ref 12);
					if (!Cancel) {
						print_report();
					}
				}
			}

		}
		public void Serialize()
		{
		}
		public void Unserialize()
		{
		}
		public void setDates()
		{
		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsF1099();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.F1099. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.F1099(false);
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
