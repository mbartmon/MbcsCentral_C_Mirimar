using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MbcsCentral.WinPM
{
    class LeaseExpirationReport : IDisposable
    {
        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", (float)14);
        Font headerFont = new Font("Arial", 24);
        Font dateFont = new Font("Arial", 18);
        int page = 1;

        string xaddr = "";
        DataSet ds = null;

        private string startBld;
        private string endBld;
        private DateTime fromDate;
        private DateTime toDate;
        private DateTime reportDate;
        private int target;
        System.Printing.PrintQueue printer;

        public LeaseExpirationReport() {}

        public void Dispose()
        {
            rpt.Dispose();
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
        }
        public LeaseExpirationReport(string startBld, string endBld, DateTime? fromDate, DateTime? toDate, DateTime? rptDate, int target, System.Printing.PrintQueue printer = null)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.fromDate = (DateTime)fromDate;
            this.toDate = (DateTime)toDate;
            this.reportDate = (DateTime)rptDate;
            this.target = target;
            this.printer = printer;
        }

        public void Process()
        {
            string xbld = "";
            Building bld;

            string xsql = "SELECT * FROM pm_Tenants WHERE [Building Number] >= '" + startBld;
            xsql += "' And [Building Number] <= '" + endBld + "' AND left([Tenant Name],1) <> '*'";
            xsql += " AND [New Tenant Flag] = 0 AND [Lease Expiration Date] >= '";
            xsql += fromDate.ToString("MM/dd/yyyy") + "' AND [Lease Expiration Date] <= '";
            xsql += toDate.ToString("MM/dd/yyyy") + "' AND [clientId] = " + clientId.ToString();
            xsql += " ORDER BY [Building Number], [Tenant Number]";
            ds = clsDB.Query(xsql, GlobalShared.CN);
            if (ds == null)
                return;

            rpt = new Report("Lease Expiration Report", "Courier New", 8);
            rpt.SetFont = fnt;
            rpt.LineHeight = .165 * 96;
            rpt.TopMargin = (float)7;
            rpt.LeftMargin = (float)10;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)60;

            Heading();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((string)dr["Building Number"] == "")
                    xbld = (string)dr["Building Number"];

                if ((string)dr["Building Number"] != xbld)
                {
                    xbld = (string)dr["Building Number"];
                    bld = Building.loadByBuildingNumber(xbld, clientId);
                    if (bld != null)
                        xaddr = bld.Address1;
                    else
                        xaddr = "";
                }
                PrintTenant(dr);
            }
            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        private void PrintTenant(DataRow dr)
        {
            rpt.PrintPageLine(((string)dr["Building Number"]).PadRight(3), false);
            rpt.PrintPageLine(((string)dr["Tenant Number"]).PadRight(4), false);
            rpt.PrintPageLine(((string)dr["Type"]).Trim().PadRight(5), false);
            rpt.PrintPageLine(((string)dr["Tenant Name"]).Trim().PadRight(31), false);
            rpt.PrintPageLine(((string)dr["Apartment"]).PadRight(5), false);
            rpt.PrintPageLine(((DateTime)dr["Lease Expiration Date"]).ToString("MM/dd/yyyy").PadRight(12), false);
            if (rpt.PrintPageLine(xaddr))
            {
                rpt.NewPage();
                Heading();
            }
        }

        private void Heading()
        {
            rpt.FontBold = true;
            rpt.PrintPageLine("Lease Expiration Report", headerFont);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            rpt.PrintPageLine(reportDate.ToString("MM/dd/yyyy") + "     page " + (page++).ToString());
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine("Tenant#".PadRight(8), false);
            rpt.PrintPageLine("Type".PadRight(5), false);
            rpt.PrintPageLine("Tenant Name".PadRight(31), false);
            rpt.PrintPageLine("Apt".PadRight(5), false);
            rpt.PrintPageLine("Exp Date".PadRight(12), false);
            rpt.PrintPageLine("Address");
            rpt.FontUnderline = false;
        }
    }
}
