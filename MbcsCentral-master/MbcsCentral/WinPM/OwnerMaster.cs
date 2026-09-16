using System;
using System.Drawing;

namespace MbcsCentral.WinPM
{
    class OwnerMaster :IDisposable
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", 11);
        Font headerFont = new Font("Arial", 14);
        int page = 1;
        string[] header = { " #","tab(4)","OWNER NAME/Address/FedID","tab(32)","Phone","tab(45)","Bld Address","tab(76)","Agent/Super","tab(95)","Phone" };

        private int target;
        public int Target
        {
            set { target = value; }
        }

        public OwnerMaster() {}

        public OwnerMaster(int target)
        {
            this.target = target;
        }

        public void Dispose()
        {
            rpt.Dispose();
            fnt.Dispose();
            headerFont.Dispose();
        }

        public void Process(System.Printing.PrintQueue printer = null)
        {
            Owners[] owns = Owners.load(clientId);
            if (owns == null || owns.Length == 0)
                return;

            double FirstY;
            double LastY = 0;
            double LineCharPos;
            rpt = new Report("Owner Master List", "Courier New", 11);
            rpt.SetFont = fnt;
            rpt.LeftMargin = (float) .25 * 96;
            rpt.TopMargin = (float) .25 * 96;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)50;
            Heading();
            Building[] blds = null;
            foreach (Owners own in owns)
            {
                blds = Building.loadByOwnerNumber(own.OwnerNumber,clientId);
                FirstY = rpt.CurrentHeight;
                string[] data = {own.OwnerNumber.ToString("00"),"tab(4)", own.OwnerName,"tab(32)",own.Telephone};
                rpt.PrintPageLine(data);
                Array.Resize<string>(ref data, 2);
                data[0]="tab(4)";
                data[1]=own.Address1;
                rpt.PrintPageLine(data);
                data[1] = own.Address2;
                rpt.PrintPageLine(data);
                data[1] = own.FederalID;
                if (rpt.PrintPageLine(data) && (blds == null || blds.Length == 0))
                {
                    rpt.NewPage();
                    Heading();
                }
                LastY = rpt.CurrentHeight;

                if (blds != null && blds.Length >0)
                {
                    rpt.CurrentHeight = FirstY;
                    foreach (Building bld in blds)
                    {
                        Array.Resize<string>(ref data,6);
                        data[0] = "tab(45)";
                        data[1] = bld.Address1;
                        data[2] = "tab(76)";
                        data[3] = bld.AgentName;
                        rpt.PrintPageLine(data);
                        data[1] = bld.Address2;
                        data[3] = bld.SuperintendantName;
                        data[4] = "tab(95)";
                        data[5] = bld.SuperintendantPhone;
                        rpt.PrintPageLine(data);
                        if (rpt.PrintPageLine(""))
                        {
                            rpt.NewPage();
                            Heading();
                            LastY = rpt.CurrentHeight;
                        }
                        if (rpt.CurrentHeight > LastY)
                            LastY = rpt.CurrentHeight;
                    }
                }
                rpt.CurrentHeight = LastY;
                if (rpt.PrintPageLine(""))
                {
                    rpt.NewPage();
                    Heading();
                }
                else
                {
                    rpt.PrintPageLine("");
                    rpt.PrintLine(5, 80);
                }
            }

            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        public void Heading()
        {
            rpt.FontBold = true;

            rpt.PrintPageLine("OWNER MASTER LIST", headerFont);
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "      page " + (page++).ToString(), headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine(header);
            rpt.FontUnderline = false;
            rpt.FontBold = false;
        }
    }
}
