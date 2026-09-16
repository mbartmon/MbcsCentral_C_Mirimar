using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinGL
{
    class PayrollReport
    {
		private bool first;
		private Report rpt;
        string fontname = "Courier New";
        float fontsize = 10;
		private ApChecks[] tblChecks;
		private int xdpt;
		private int xcmp;
		private int xVnd;
		private short r;
		private decimal[] amt = new decimal[10];
		private decimal[] vamt = new decimal[10];
		private decimal[] yamt = new decimal[10];
		private PRaccounts tblAccounts;

        private System.DateTime mvarStartDate;
		private System.DateTime mvarEndDate;
		private bool mvarYTD;
		public bool YTD {
			get { return mvarYTD; }
			set { mvarYTD = value; }
		}
		public System.DateTime EndDate {
			get { return mvarEndDate; }
			set { mvarEndDate = value; }
		}
		public System.DateTime StartDate {
			get { return mvarStartDate; }
			set { mvarStartDate = value; }
		}
		public void print_report()
		{

			string xsql = "";
			string x = "";
			int i = 0;
			System.DateTime sdate = default(System.DateTime);
			decimal net = default(decimal);

            rpt = new Report("Payroll Report;" + StartDate.ToShortDateString() + " to " + EndDate.ToShortDateString() + (YTD ? ";Year to Date" : ""), fontname, fontsize) { LinesPerPage = 50, BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
            rpt.PageOrientation = (int)Report.Orientation.Landscape;

            tblAccounts = PRaccounts.load();

			first = true;
			if (YTD) {
                sdate = new DateTime(EndDate.Year, 1, 1);
			} else {
				sdate = StartDate;
			}
			xsql = "SELECT * FROM gl_APChecks, gl_Vendors WHERE gl_APChecks.Sequence = 0 AND gl_APChecks.Date BETWEEN '" + sdate + "' AND '" + EndDate + "' AND gl_Vendors.VendorNumber = gl_APChecks.[Vendor/Account] AND gl_Vendors.Payroll = 'True' ORDER BY gl_APChecks.Company, gl_APChecks.Department, gl_APChecks.[Vendor/Account], gl_APChecks.Date";
			tblChecks = ApChecks.LoadQuery(xsql);
            if (tblChecks != null && tblChecks.Length > 0)
            {

                ApChecks[] apChecks = null;
                foreach (ApChecks check in tblChecks)
                {

                    if (first)
                    {
                        System.Array.Clear(amt, 0, amt.Length);
                        System.Array.Clear(vamt, 0, vamt.Length);
                        System.Array.Clear(yamt, 0, yamt.Length);
                        xcmp = check.Company;
                        xdpt = check.Department;
                        xVnd = check.VendorAccount;
                        heading();
                        dpt_heading();
                        vnd_heading();
                        first = false;
                    }

                    if (xcmp != check.Company)
                    {
                        vtotal();
                        xcmp = check.Company;
                        xdpt = check.Department;
                        xVnd = check.VendorAccount;
                        rpt.NewPage();
                        heading();
                        dpt_heading();
                        vnd_heading();
                    }

                    if (xdpt != check.Department)
                    {
                        vtotal();
                        xdpt = check.Department;
                        xVnd = check.VendorAccount;
                        dpt_heading();
                        vnd_heading();
                    }

                    if (xVnd != check.VendorAccount)
                    {
                        vtotal();
                        xVnd = check.VendorAccount;
                        vnd_heading();
                    }

                    apChecks = ApChecks.LoadQuery("SELECT * FROM gl_APChecks WHERE Company = " + check.Company.ToString() + " AND Department = " + check.Department.ToString() + " AND CheckNumber = " + check.CheckNumber.ToString() + " AND Sequence <> 0");
                    if (apChecks != null && apChecks.Length > 0)
                    {
                        x = check.Date.ToString("MM/dd/yy") + " " + check.CheckNumber.ToString("000000");
                        foreach (ApChecks apcheck in apChecks)
                        {
                            i = getaccount(apcheck.VendorAccount);
                            if (apcheck.Date >= StartDate)
                            {
                                amt[i] += apcheck.Amount;
                                vamt[i] += apcheck.Amount;
                                yamt[i] += apcheck.Amount;
                            }
                            else
                            {
                                yamt[i] += apcheck.Amount;
                            }
                        }
                        net = 0;
                        for (i = 0; i <= 9; i++)
                        {
                            x = x + " " + amt[i].ToString("#####0.00").PadLeft(9);
                            net += amt[i];
                        }
                        if (net != 0)
                        {
                            x = x + " " + net.ToString("#####0.00").PadLeft(9);
                            if (rpt.PrintPageLine(x))
                            {
                                rpt.NewPage();
                                heading();
                            }

                       }
                        System.Array.Clear(amt, 0, amt.Length);
                    }
                }
            }
			vtotal();
			rpt.DisplayDocument();

		}
		public void vtotal()
		{

			short i = 0;
			string x = "";
			decimal net = default(decimal);

			net = 0;
			x = new string(' ', 7) + "TOTALS=>";
			for (i = 0; i <= 9; i++)
            {
				x += vamt[i].ToString("######0.00").PadLeft(10);
				net = net + vamt[i];
			}
			x += net.ToString("######0.00").PadLeft(10);
			rpt.PrintPageLine();
			rpt.PrintPageLine(x);
			if (YTD)
            {
				net = 0;
				x = "   YTD TOTALS=>";
				for (i = 0; i <= 9; i++)
                {
					x += yamt[i].ToString("######0.00").PadLeft(10);
					net +=yamt[i];
				}
				x += net.ToString("######0.00").PadLeft(10);
                rpt.PrintPageLine(x);
			}
            rpt.PrintPageLine();
			if (rpt.PrintPageLine())
            {
                rpt.NewPage();
                heading();
            }
			System.Array.Clear(amt, 0, amt.Length);
			System.Array.Clear(vamt, 0, vamt.Length);
			System.Array.Clear(yamt, 0, yamt.Length);

		}
		private void heading()
		{

			rpt.PrintPageLine(new string(' ', 50) + "PAYROLL REPORT        page " + rpt.PageTag);
			rpt.PrintPageLine(new string(' ', 50) + StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString());
			rpt.PrintPageLine();
			rpt.PrintPageLine(new string(' ', 50) + "COMPANY: " + xcmp.ToString("##0") + " " + funcCompanyName(xcmp));
			rpt.PrintPageLine();
			rpt.PrintPageLine();

		}
		private void dpt_heading()
		{

			rpt.PrintPageLine("Department: " + xdpt + " " + funcDepartmentName(xdpt));
			rpt.PrintPageLine();

		}
		private void vnd_heading()
		{

			Vendors tbl = Vendors.load(xVnd);
			if (tbl != null) {
				rpt.PrintPageLine("VENDOR #" + xVnd.ToString( "#####").PadLeft(5) + "  Name: " + tbl.VendorName.Trim() + " S.S.#:" + tbl.FederalID);
				rpt.PrintPageLine(new string(' ', 21) + tbl.Street);
				rpt.PrintPageLine(new string(' ', 21) + tbl.City + ", " + tbl.State + " " + tbl.Zip);
				if (rpt.PrintPageLine())
                {
                    rpt.NewPage();
                    heading();
                }
                tbl = null;
			} else {
				rpt.PrintPageLine("*VENDOR NOT ON FILE *");
			}
			Columns();

		}
		private void Columns()
		{

			rpt.PrintPageLine("  Date   Check#    Salary      S.S.  Medicare   Federal     State City/Yonk   DIS/u/i    Family  Med/Empl     Other       Net");

		}
		public string funcCompanyName(int c)
		{
			Company tbl = Company.loadByCompany(c);
			if (tbl!= null)
            {
				return tbl.Name;
			} else {
				return "* Not Found *";
			}

		}

		public string funcDepartmentName(int d)
		{
			Department tbl = Department.loadByDepartment( (short)d);
			if (tbl != null)
            {
				return tbl.Name;
			} else {
				return "* Not Found *";
			}

		}

		public int getaccount(int a)
		{
			int i = 0;

			for (i = 0; i <= 17; i++) {
				if (a == tblAccounts["Account" + i.ToString("00")])
                {
					return (int)Math.Round((double)(i / 2), MidpointRounding.AwayFromZero);
				}
			}
			return 9;

		}

    }
}
