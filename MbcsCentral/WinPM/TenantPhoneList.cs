using System;
using System.Drawing;

namespace MbcsCentral.WinPM
{
    class TenantPhoneList  : IDisposable
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", (float)7.5);
        Font headerFont = new Font("Arial", 20);
        int page = 1;
        private string[] header = { "Tenant Name", "tab(32)", "Tenant#", "tab(40)", "Apt", "tab(45)", "Home Phones", "tab(59)", "Business Phones", "tab(79)", "Mobile", "tab(93)","Emails" };

        private string startBld;
        private string endBld;
        private bool byBuilding = false;
        private bool Alpha = true;
        private int target;
        System.Printing.PrintQueue printer;

        public TenantPhoneList(){}

        public TenantPhoneList(string startBld, string endBld, bool byBuilding, bool Alpha, int target, System.Printing.PrintQueue printer = null)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.byBuilding = byBuilding;
            this.Alpha = Alpha;
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
                           " AND [Building Number] <= " + endBld + " AND [Tenant Name] NOT LIKE('%Vacant%')" +
                           " ORDER BY ";
            if (byBuilding)
                xsql += " [Building Number],";
            if (Alpha)
                xsql += "[Tenant Name]";
            else
                xsql += "[Tenant Number]";
            Tenants[] tens = Tenants.Query(xsql, GlobalShared.NULL_DATE);
            if (tens == null || tens.Length == 0)
                return;
            rpt = new Report("Tenant Master List", fnt.Name, fnt.Size) { PageOrientation = (int)Report.Orientation.Landscape };
            rpt.LineHeight = .165 * 96;
            rpt.LeftMargin = (float)7;
            rpt.TopMargin = (float) 5;
            rpt.BottomMargin = (float) 25;
            rpt.BottomOffset = (float) 50;
          
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
                string[] data = { ten.TenantName, "tab(32)", ten.BuildingTenant, "tab(40)", ten.Apartment, "tab(45)", ten.HomePhone1, "tab(59)", ten.BusPhone1, "tab(79)", ten.Pager, "tab(93)", ten.Email1.Trim(), (!ten.Email2.Trim().Equals("") ? " / " + ten.Email2 : "") };
                rpt.PrintPageLine(data);
                if (ten.HomePhone2.Trim() != "" || ten.BusPhone2.Trim() != "" || ten.Cell2 != "")
                {
                    Array.Resize<string>(ref data, 6);
                    data[0] = "tab(45)";
                    data[1] = ten.HomePhone2;
                    data[2] = "tab(59)";
                    data[3] = ten.BusPhone2;
                    data[4] = "tab(79)";
                    data[5] = ten.Cell2;
                    rpt.PrintPageLine(data);
                }
                if (rpt.PrintPageLine(""))
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
            rpt.PrintPageLine("Tenant Telephone List", headerFont);
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
            set { target = value; }
        }
    }
}
