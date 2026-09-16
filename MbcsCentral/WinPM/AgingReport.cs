using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace MbcsCentral.WinPM
{
    class AgingReport : ReportBase, IDisposable
    {
        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", (float)10.5);
        Font headerFont = new Font("Arial", 24);
        Font dateFont = new Font("Arial", 18);
        int page = 1;
        int Arrearscnt;
        int Totalcnt;

        struct ChargeDetail
        {
            public string code;
            public decimal amount;
        };
        List<ChargeDetail> chargeDetails = new List<ChargeDetail>();

        string[] header = { "Tenant Number", "tab(15)", "Tenant Name" ,"tab(32)","Apt", "tab(37)","Current Due", "tab(53)","30Days", "tab(64)","60Days", "tab(74)"
            ,"90Days","tab(85)","120Days","tab(96)","Total Arrears" };

        private decimal[] totals = new decimal[2];
        private bool BldFlag;
        private int xseq = 0;
        private string xbld;
        private string xten;
        private decimal tchg;
        private decimal Bal;
        private string xname;
        private string xapt;
        private string xdue;
        private string xtype;
        private string xpager;
        private string xlegal;
        private string xEmail1;
        private string xhome2;
        private string xEmail2;
        private string xbus2;
        private decimal xrent;
        private decimal xarrears;
        private decimal xpaid;
        private int target;
        private string startBld;
        private bool addCharges;
        private decimal currentRent;
        private decimal currentCharges;
        private decimal baseAmount;
        private decimal totalOverdue;
        private decimal noOfMonthsOwned;
        private decimal Aging30Days = 0;
        private decimal Aging60Days = 0;
        private decimal Aging90Days = 0;
        private decimal Aging120Days = 0;
        private decimal totalAging30Days = 0;
        private decimal totalAging60Days = 0;
        private decimal totalAging90Days = 0;
        private decimal totalAging120Days = 0;
        private decimal Overalltotal = 0;
        System.Printing.PrintQueue printer;
        public AgingReport() { }

        public AgingReport(string startBld, int target, System.Printing.PrintQueue printer = null, bool AddCharges = true)
        {
            this.startBld = startBld;
            this.printer = printer;
            this.target = target;
            this.addCharges = AddCharges;
        }

        public void Dispose()
        {
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
            rpt.Dispose();
        }
        public override void Process()
        {
            Charges[] chgs;
            Tenants[] tens = Tenants.loadForStatements(startBld, startBld, clientId);
            if (tens == null || tens.Length == 0)
                return;
            Totalcnt = tens.Length;
            rpt = new Report("Tenant Aging Report1", "Courier New", 8);
            rpt.SetFont = fnt;
            rpt.LineHeight = .15 * 96;
            rpt.LeftMargin = (float)5;
            rpt.TopMargin = (float)5;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)50;
            bool first = true;
            string additionalCharges = "";
            List<string> addList = new List<string>();
            decimal RentDue;
            decimal RentPaid;
            foreach (Tenants ten in tens)
            {
                // For Testing
                //if (ten.TenantNumber != "ST4")
                //    continue;
                SetTenant(ten);
                ChargeDetail chgDetail;
                // RentDue = ten.Rent + ten.Arrears;
                RentDue = ten.Arrears;
                bool chgFound = false;
                chargeDetails.Clear();
                decimal ChargesPaid = 0;
                tchg = 0;
                chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                if (chgs != null && chgs.Length > 0)
                {
                    foreach (Charges chg in chgs)
                    {
                        if (chg.StartingDate <= DateTime.Now)
                        {
                            // tchg += chg.Amount + chg.Arrears;
                            tchg += chg.Arrears;
                            // ChargesPaid += chg.Paid;
                            currentCharges += chg.Amount;

                        }
                    }
                    chgs = null;
                }


                if (currentCharges < 0)
                    currentCharges = 0;
                baseAmount = currentCharges + xrent;
                totalOverdue = xarrears + tchg;
                Bal = baseAmount - totalOverdue;
                if (totalOverdue > 0)
                {
                    // if noOfMonthsOwned is 0 then tenant do not have anything pending
                    noOfMonthsOwned = totalOverdue / baseAmount;

                    if (noOfMonthsOwned > 0)
                    {
                        if (noOfMonthsOwned <= 1)
                            Aging30Days = totalOverdue;
                        else if (noOfMonthsOwned <= 2)
                        {
                            Aging30Days = baseAmount;
                            decimal currentDue = totalOverdue - baseAmount;
                            Aging60Days = currentDue;
                        }
                        else if (noOfMonthsOwned <= 3)
                        {
                            Aging30Days = baseAmount;
                            Aging60Days = baseAmount;
                            decimal currentDue = totalOverdue - baseAmount * 2;
                            Aging90Days = currentDue;

                        }
                        else if (noOfMonthsOwned <= 4)
                        {
                            Aging30Days = baseAmount;
                            Aging60Days = baseAmount;
                            Aging90Days = baseAmount;
                            decimal currentDue = totalOverdue - baseAmount * 3;
                            Aging120Days = currentDue;

                        }
                    }

                    addList = null;
                    additionalCharges = "";
                    if (first)
                    {
                        Heading();
                        xbld = ten.BuildingNumber;
                        first = false;
                    }
                  
                    PrintTenant(addList);
                    totalAging30Days += Aging30Days;
                    totalAging60Days += Aging60Days;
                    totalAging90Days += Aging90Days;
                    totalAging120Days += Aging120Days;
                    Overalltotal += totalOverdue;
                }
                

            }
            PrintTotals();
                if (target == 0)
                    rpt.DisplayDocument();
                else
                    rpt.PrintDocument(printer);
        }

        private void PrintTenant(List<string> additionalCharges = null)
        {

            // TntHeading();
            string[] data1 = { xten, "tab(15)", xname, "tab(30)", xapt.PadLeft(6), "tab(37)", totalOverdue.ToString("###,##0.00").PadLeft(11), "tab(49)", Aging30Days.ToString("###,##0.00").PadLeft(10),
                "tab(60)", Aging60Days.ToString("###,##0.00").PadLeft(10), "tab(70)",Aging90Days.ToString("###,##0.00").PadLeft(10), "tab(81)",Aging120Days.ToString("###,##0.00").PadLeft(11), "tab(92)"
            ,totalOverdue.ToString("###,##0.00").PadLeft(17)};
            rpt.PrintPageLine(data1);




            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
            }
            rpt.PrintPageLine("");
            if (rpt.PrintLine(2, 80, null, false))
            {
                rpt.NewPage();
                Heading();
            }
            Arrearscnt++;

        }

        private void PrintTotals()
        {
            rpt.PrintPageLine("Totals for Report ", false);
            string[] data =  { "tab(37)", Overalltotal.ToString("###,##0.00").PadLeft(11), "tab(49)", totalAging30Days.ToString("###,##0.00").PadLeft(10),
                "tab(60)", totalAging60Days.ToString("###,##0.00").PadLeft(10), "tab(70)",totalAging90Days.ToString("###,##0.00").PadLeft(10), "tab(81)",totalAging120Days.ToString("###,##0.00").PadLeft(11), "tab(92)"
            ,Overalltotal.ToString("###,##0.00").PadLeft(17)};
            rpt.PrintPageLine(data);
            rpt.FontBold = false;
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
            }
        }

        private void Heading()
        {
            rpt.FontBold = true;
            rpt.PrintPageLine("TENANT AGING REPORT", headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "     page " + (page++).ToString());
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine(header);
            rpt.FontUnderline = false;
        }

        private void BldHeading()
        {
            string OwnerName = "";
            Building bld = Building.loadByBuildingNumber(xbld, clientId);
            Owners own = Owners.loadByOwnerNumber(bld.OwnerNumber, clientId);
            if (own != null)
                OwnerName = own.OwnerName;
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
                rpt.PrintPageLine("");
            }
            string[] data = { "Building Address", "tab(40)", "MDR       Owner Name" };
            rpt.FontBold = true;
            rpt.PrintPageLine(data);
            string[] data2 = { xbld + "      " + bld.Address1, "tab(37)", bld.MultipleDwellingRegistrationNumber, "tab(50)", OwnerName };
            rpt.PrintPageLine(data2);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            BldFlag = true;
        }

        private void TntHeading()
        {

            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
                rpt.PrintPageLine("");
            }
            string[] data = { "Tenant Number", "tab(10)", "Tenant Name" ,"tab(10)","Apt", "tab(10)","Current Due", "tab(10)","30Days", "tab(10)","60Days", "tab(10)"
            ,"90Days","tab(10)","120Days","tab(10)","Total Arrears"};
            rpt.FontBold = true;
            rpt.PrintPageLine(data);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
        }

        private void SetTenant(Tenants tbl)
        {
            xseq = tbl.SequenceNumber;
            xten = tbl.BuildingNumber + " " + tbl.TenantNumber;
            xname = (tbl.TenantName == null ? "" : tbl.TenantName);
            xapt = (tbl.Apartment == null ? "" : tbl.Apartment);
            xtype = (tbl.Type == null ? "" : tbl.Type);
            xlegal = (tbl.LegalCode == null ? "" : tbl.LegalCode);
            xrent = tbl.Rent;
            xarrears = tbl.Arrears;
            xpaid = tbl.Paid;
            xEmail1 = (tbl.Email1 == null ? "" : tbl.Email1);
            //xhome2 = (tbl.HomePhone2 == null ? "" : tbl.HomePhone2);
            xEmail2 = (tbl.Email2 == null ? "" : tbl.Email2);
            //xbus2 = (tbl.BusPhone2 == null ? "" : tbl.BusPhone2);
            //xpager = (tbl.Pager == null ? "" : tbl.Pager);
            xdue = (tbl.DueDate == null ? "" : tbl.DueDate);
        }
    }
}
