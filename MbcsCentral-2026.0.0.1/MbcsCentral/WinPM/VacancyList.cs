using System;
using System.Data;
using System.Drawing;

namespace MbcsCentral.WinPM
{
    class VacancyList
    {
        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", (float)14);
        Font headerFont = new Font("Arial", 24);
        Font dateFont = new Font("Arial", 18);
        int page = 1;
        private string xbld;
        private string xten;
        private string xrent;
        private string xapt;
        private string xaddr;
        private decimal total;
        private DataSet ds = null;

        private string startBld;
        private string endBld;
        private int target;
        private System.Printing.PrintQueue printer;

        public VacancyList(){}

        public VacancyList(string startBld, string endBld, int target, System.Printing.PrintQueue printer = null)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.target = target;
            this.printer = printer;
        }

        public void Process()
        {
            string xsql = "SELECT * FROM pm_Tenants WHERE [Building Number] >= '" + startBld + "' And [Building Number] <= '";
            xsql += endBld + "' AND (left([Tenant Name],1) = '*' OR LOWER([tenant name]) = 'vacant') AND [New Tenant Flag] = 0";
            xsql += " AND [clientId] = " + clientId.ToString() + " ORDER BY [Building Number], [Tenant Number]";
            ds = clsDB.Query(xsql, GlobalShared.CN);
            if (ds == null)
                return;

            rpt = new Report("Vacancy List", "Courier New", 8);
            rpt.SetFont = fnt;
            rpt.LineHeight = .165 * 96;
            rpt.LeftMargin = 10;
            rpt.TopMargin = (float)7;
            rpt.BottomMargin = (float)50;

            Heading();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Building bld = Building.loadByBuildingNumber((string)dr["Building Number"], clientId);
                xaddr = (bld == null ? "" : bld.Address1);
                bld = null;
                PrintTenant(dr);
            }
            string[] data = { "tab(59)", "=========" };
            rpt.PrintPageLine(data);
            data[1] = total.ToString("##,##0.00").PadLeft(9);
            rpt.PrintPageLine(data);
            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        private void PrintTenant(DataRow ten)
        {
            xbld = (string)ten["Building Number"];
            xten = (string)ten["Tenant Number"];
            xapt = ((string)ten["Apartment"] == null ? "" : (string)ten["Apartment"]);
            xrent = ((decimal)ten["Rent"]).ToString("#,##0.00").PadLeft(8);

            string[] data = { xbld + xten, "tab(10)", xapt, "tab(20)", xaddr, "tab(60)", xrent };
            total += decimal.Parse(xrent);
            if (rpt.PrintPageLine(data))
            {
                rpt.NewPage();
                Heading();
            }
        }

        private void Heading()
        {
            rpt.FontBold = true;
            rpt.PrintPageLine("VACANCY LIST", headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "     page " + (page++).ToString());
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            string[] data = { "Tenant#  Apartment Address", "tab(64)", "Rent" };
            rpt.PrintPageLine(data);
            rpt.PrintPageLine("");
            rpt.FontBold = false;
            rpt.FontUnderline = false;                 
        }

    }

}
