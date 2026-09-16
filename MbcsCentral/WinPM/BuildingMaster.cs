using System;
using System.Drawing;

namespace MbcsCentral.WinPM
{
    class  BuildingMaster  : IDisposable
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", 11);
        Font headerFont = new Font("Arial", 24);
        Font dateFont = new Font("Arial", 18);
        int page = 1;
        string[] topHeader = { "tab(66)","RE TAXES" };
        string[] columnHeaders = { "No.", "tab(6)","Address", "tab(32)","Agent", "tab(45)","Block", "tab(51)","Lot", "tab(55)","Sec", "tab(59)","MDR", "tab(66)","Mort Int", "tab(75)","Shares", "tab(82)","Owner" };

        private int target;
        System.Printing.PrintQueue printer;
        public int Target
        {
            set { target = value; }
        }
        
        public BuildingMaster()
        {}

        public BuildingMaster(int target, System.Printing.PrintQueue printer = null)
        {
            this.target = target;
        }

        public void Dispose()
        {
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
            rpt.Dispose();
        }
        public void Process()
        {
            Owners own = null;
            Building[] blds = Building.load(clientId);
            if (blds == null || blds.Length == 0)
                return;

            rpt = new Report("Building Master", "Courier New", 8);
            rpt.SetFont = fnt;
            rpt.LineHeight = .15 * 96;
            rpt.LeftMargin = (float) 7;
            rpt.TopMargin = (float).25 * 96;
            Heading();

            foreach (Building bld in blds)
            {
                own = Owners.loadByOwnerNumber(bld.OwnerNumber, clientId);
                string[] data = {bld.BuildingNumber, "tab(6)",bld.Address1,"tab(32)", bld.AgentName,"tab(45)",bld.BlockNumber,"tab(51)",bld.LotNumber,"tab(55)",bld.SectionNumber,"tab(59)",bld.MultipleDwellingRegistrationNumber,"tab(66)",bld.RealEstateTaxes.ToString("##,##0.00").PadLeft(9),"tab(75)",bld.TotalShares.ToString("####0").PadLeft(5),"tab(82)",own.OwnerNumber.ToString("00")};
                rpt.PrintPageLine(data);
                Array.Resize<string>(ref data, 6);
                data[0] = "tab(6)";
                data[1] = bld.Address2;
                data[2] = "tab(66)";
                data[3] = bld.MortgageInterest.ToString("#,##0.##").PadLeft(9);
                data[4] = "tab(82)";
                data[5] = own.OwnerName;
                rpt.PrintPageLine(data);
                rpt.PrintPageLine("");
                data = null;
            }

            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        private void Heading()
        {
            rpt.FontBold = true;
            
            rpt.PrintPageLine("BUIDING MASTER LIST", headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "          Page - " + (page++).ToString(), dateFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine(topHeader);
            rpt.FontUnderline = true;
            rpt.PrintPageLine(columnHeaders);
            rpt.FontUnderline = false;
            rpt.FontBold = false;
        }
    }
}
