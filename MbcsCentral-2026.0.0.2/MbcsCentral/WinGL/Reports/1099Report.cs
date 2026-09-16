using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
namespace MbcsCentral.WinGL
{
	public class Form1099
	{
		private Report rpt;
        string fontname = "Courier New";
        float fontsize = 8;
        private short pg;
		private short ln;
		private Accounts tblAccounts;
		private ApChecks[] tblChecks;
		private Vendors[] tblVendors;
		private ApChecks[] tblOneCheck;
		private ApChecks[] tblTemp;
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
		private System.DateTime mvarStartDate;
		private System.DateTime mvarEndDate;
		private bool mvarCancel;
        private Vendors[] mvarVendors = null;
		private bool mvarDetail;
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
		public Vendors[] vendors {
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
			string adesc = "";
			object l = null;
			string xsql = "";

            rpt = new Report("1099 Report", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 150 };
            tb = 5;
			ctotal = 0;
			dTotal = 0;
			vtotal = 0;
			xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE Sequence = 0 AND Date BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
			if (vendors != null && vendors.Length > 0)
            {
                bool first = true;
				xsql += "  AND [" + ApChecks.SQL_COL_VENDOR_ACCOUNT + "] IN (";
                foreach (Vendors vendor in vendors)
                {
                    xsql += (first ? "" : ", ") +  vendor.VendorNumber.ToString();
                    first = false;
                }
                xsql += ")";
			}

			if (Order == 0)
            {
				xsql = xsql + " ORDER BY [Vendor/Account], Company, Department, CheckNumber";
			} else {
				xsql = xsql + " ORDER BY Company, Department, [Vendor/Account], CheckNumber";
			}

			tblChecks = ApChecks.LoadQuery(xsql);
			if (tblChecks != null && tblChecks.Length > 0) {
				heading();
				foreach (ApChecks check in tblChecks)
                {
					if (check.Company != xCompany) {
						if (ctotal != 0) {
							VendorTotal();
							DepartmentTotal();
							CompanyTotal();
						}
                        //xVendor = check.VendorAccount;
                        //xDepartment = check.Department;
						xCompany = check.Company;
						CompanyHeader();
					}
					if (check.Department != xDepartment) {
						if (dTotal != 0) {
							VendorTotal();
							DepartmentTotal();
						}
                        //xVendor = check.VendorAccount;
						xDepartment = check.Department;
					}
					if (check.VendorAccount != xVendor) {
						if (vtotal != 0) {
							VendorTotal();
						}
						xVendor = check.VendorAccount;
						VendorHeader();
					}
					xsql = ApChecks.SQL_LOAD_FOR_1099 + " WHERE Company = ";
					xsql += check.Company.ToString() + " AND Department = " + check.Department.ToString() + " AND CheckNumber = ";
					xsql += check.CheckNumber.ToString() + " AND Sequence <> 0 GROUP BY Company, Department, CheckNumber";
					tblOneCheck = ApChecks.LoadQuery(xsql, true);
					if (tblOneCheck != null && tblOneCheck.Length > 0) {
						if (Detail)
                        {
							xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE Company = " + check.Company.ToString() + " AND Department = ";
							xsql += check.Department.ToString() + " AND CheckNumber = " + check.CheckNumber.ToString() + " AND Sequence = 1";
							tblTemp = ApChecks.LoadQuery(xsql);
							if (tblTemp.Length > 0)
                            {
								tblAccounts = Accounts.load(tblTemp[0].VendorAccount);
								if (tblAccounts != null)
                                {
									adesc = tblAccounts.Description;
								} else {
									adesc = "* No Description *";
								}
							}
							if (rpt.PrintPageLine(Strings.Space(tb) + check.CheckNumber.ToString("000000") + check.Date.ToString("MM/dd/yyyy").PadLeft(16) + "  " + adesc.PadRight(62) + tblOneCheck[0].Amount.ToString("#########.00").PadLeft(12)))
                            {
                                rpt.NewPage();
                                heading();
                            }

                        }
                        vtotal = vtotal + tblOneCheck[0].Amount;
						dTotal = dTotal + tblOneCheck[0].Amount;
						ctotal = ctotal + tblOneCheck[0].Amount;
					}
				}
				VendorTotal();
				DepartmentTotal();
				CompanyTotal();
				rpt.DisplayDocument();
			}

		}
        ///******************************************************************************
		public void heading()
		{
	    ///******************************************************************************

			rpt.PrintPageLine(Strings.Space(tb) + "1099 REPORT   Page " + Report.PAGE_TAG);
			rpt.PrintPageLine(Strings.Space(tb) + DateAndTime.Now.ToShortDateString());
            rpt.PrintPageLine(Strings.Space(tb) + "From " + StartDate.ToShortDateString() + " to " + EndDate.ToShortDateString());
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(tb) + "Check#          Date    Description                                                         Amount");
			rpt.PrintPageLine(Strings.Space(tb) + "--------------------------------------------------------------------------------------------------");

		}

        ///******************************************************************************
		private void VendorHeader()
		{
		///******************************************************************************

			Vendors tbl = Vendors.load(xVendor);
			if (tbl != null)
            {
				vName = tbl.VendorName;
				FedID = tbl.FederalID;
				Add1 = tbl.Street;
				Add2 = tbl.City;
				Add3 = tbl.State;
				Add4 = tbl.Zip;
			} else {
				vName = "* Vendor (" + xVendor + ") Not Found *";
				FedID = "";
			}

			rpt.PrintPageLine(Strings.Space(tb) + "Vendor:" + vName);
			rpt.PrintPageLine(Strings.Space(tb) + new string('-', 30));

		}
        ///******************************************************************************
		private void VendorTotal()
		{
		///******************************************************************************

			rpt.PrintPageLine(new string(' ', tb + 86) + "============");
			rpt.PrintPageLine(Strings.Space(tb) + "Total/Vendor: " + (vName.Trim() + " (" + FedID + ")").PadRight(72) + vtotal.ToString("#########.00").PadLeft(12));
			if (Address) {
				rpt.PrintPageLine(Strings.Space(tb + 14) + Strings.Trim(Add1));
				rpt.PrintPageLine(Strings.Space(tb + 14) + Strings.Trim(Add2) + ", " + Strings.Trim(Add3) + " " + Strings.Trim(Add4));
			}
			rpt.PrintPageLine(Strings.Space(tb + 14) + "for " + CmpName);
			rpt.PrintPageLine("");
			vtotal = 0;

		}
        ///******************************************************************************
		private void DepartmentTotal()
		{
			string x = "";

			Department tbl = Department.loadByDepartment((short)xDepartment);
			if (tbl != null)
				x = tbl.Name;
			else 
				x = "* Department (" + xDepartment + ") Not Found *";
			rpt.PrintPageLine(Strings.Space(tb + 86) + "------------");
			rpt.PrintPageLine(Strings.Space(tb) + "Department: " + x.PadRight(74) + dTotal.ToString("#########.00").PadLeft(12));
			if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                heading();
            }

            dTotal = 0;
		}
        //******************************************************************************
		private void CompanyHeader()
		{
		//******************************************************************************

			Company tbl = Company.loadByCompany( xCompany);
			if (tbl != null) {
				CmpName = tbl.Name;
			} else {
				CmpName = "* Company (" + xCompany + ") Not Found *";
			}
			rpt.PrintPageLine();
			rpt.PrintPageLine(Strings.Space(tb) + "Company: " + CmpName);
			rpt.PrintPageLine(Strings.Space(tb) + "------------------------------");
			rpt.PrintPageLine();
			tbl = null;

		}
        ///******************************************************************************
		private void CompanyTotal()
		{
		///******************************************************************************

			rpt.PrintPageLine(new string(' ', tb + 86) + "------------");
			rpt.PrintPageLine(new string(' ', tb) + "Company: " + CmpName.PadRight(77) + ctotal.ToString("#########.00").PadLeft(12));
			if (rpt.PrintPageLine(Strings.Space(tb) + new string('=', 98)))
            {
                rpt.NewPage();
                heading();
            }

            ctotal = 0;

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
	}
}
