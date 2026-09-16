using System;
using System.Drawing;

namespace MbcsCentral.WinPM
{
    class TenantMaster : IDisposable
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New",12);
        Font headerFont = new Font("Arial", 20);
        int page = 1;
        private string[] header = {"Tenant Name", "tab(32)","Tenant#","tab(40)","Apt","tab(45)","Type","tab(50)","SSNumber1","tab(64)","SSNumber2   "};

        private string startBld;
        private string endBld;
        private bool byBuilding = false;
        private int target;
        private System.Printing.PrintQueue printer;

        public TenantMaster() {}

        public TenantMaster(string startBld, string endBld, bool byBuilding, int target, System.Printing.PrintQueue printer = null)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.byBuilding = byBuilding;
            this.target = target;
            this.printer = printer;
        }

        public void Dispose()
        {
            fnt.Dispose();
            headerFont.Dispose();
        }
        public void Process()
        {
            string bld = "";
            string xsql = "SELECT * FROM pm_Tenants WHERE ClientId = " + 
                           clientId.ToString() + " AND [Building Number] >= " + startBld +
                           " AND [Building Number] <= " + endBld + " ORDER BY ";
            if (byBuilding)
                xsql += " [Building Number],";
            xsql += "[Tenant Name]";
            Tenants[] tens = Tenants.Query(xsql, GlobalShared.NULL_DATE);
            if (tens == null || tens.Length == 0 )
                return;

            rpt = new Report("Tenant Master List", fnt.Name, fnt.Size);
            rpt.LineHeight = .165 * 96;
            rpt.TopMargin = (float) 5;
            rpt.BottomMargin = (float) 50;
            rpt.BottomOffset = (float)55;
            rpt.LeftMargin = (float)8;
            Heading();
            foreach (Tenants ten in tens)
            {
                if (byBuilding)
                {
                    if (bld.Trim() != "" && !ten.BuildingNumber.Equals(bld))
                    {
                        rpt.PrintPageLine("");
                        bld = ten.BuildingNumber;
                    }
                    else
                        bld = ten.BuildingNumber;
                }
                string[] data = { ten.TenantName, "tab(32)", ten.BuildingTenant, "tab(40)", ten.Apartment, "tab(46)", ten.Type, "tab(50)", ten.SSNumber1, "tab(64)", ten.SSNumber2 };
                if (rpt.PrintPageLine(data))
                {
                    rpt.NewPage();
                    Heading();
                }
            }
            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        private void Heading()
        {
            rpt.FontBold = true;
            rpt.PrintPageLine("Tenant Social Security List", headerFont);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "       Page - " + (page++).ToString());
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontBold = true;
            rpt.FontUnderline = true;
            rpt.PrintPageLine(header);
            rpt.FontBold = false;
            rpt.FontUnderline = false;
        }

        public string StartBld
        {
            set { startBld = value; }
        }
        public string EndBld
        {
            set { endBld = value; }
        }
        public bool ByBuilding
        {
            set { byBuilding = value; }
        }
        public int Target
        {
            set {target = value;}
        }
    }
}
