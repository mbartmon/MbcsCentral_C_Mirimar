using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace MbcsCentral.WinPM
{
    public class PreferredRentWS : IDisposable
    {

        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = new Report("Preferential Worksheet");
        Font fnt = new Font("Courier New", (float)9);
        Font headerFont = new Font("Arial", 20);
        Font dateFont = new Font("Arial", 16);
        int page = 1;
        static string filename = GlobalShared.appPath + "\\leaseletters.ser";
        LeaseLetterParms parms = LeaseLetterParms.DeSerialize(filename);

        float pctOne;
        float pctTwo;
        float pctTwo1;
        int years;
        decimal increaseRent;
        decimal minimumIncrease1;
        decimal minimumIncrease2;
        bool chkMinimum;

        string startBld;
        string endBld;
        DateTime leaseDate;
        string prefCode = "";
        int target;
        System.Printing.PrintQueue printer;

        public PreferredRentWS(string startBld, string endBld, DateTime dt, string code, int target, System.Printing.PrintQueue printer = null)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.leaseDate = dt;
            this.prefCode = code;
            this.target = target;
            this.printer = printer;
        }

        public void Dispose()
        {
            rpt.Dispose();
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
        }

        public void Process()
        {
            DateTime dateFrom = new DateTime(leaseDate.Year, leaseDate.Month, 1);
            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);
            DateTime moveInDate;
            rpt.SetFont = fnt;
            rpt.TopMargin = (float)7;
            rpt.LeftMargin = (float)10;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)80;
            rpt.PageOrientation = (int)Report.Orientation.Landscape;
            Charges[] chgs;

            string xsql = "SELECT * FROM " + Tenants.TABLE + " WHERE [" + Tenants.SQL_COL_LEASE_EXPIRATION_DATE + "]" ;
            xsql += " BETWEEN '" + dateFrom.ToString() + "' AND '" + dateTo.ToString() + "'" ;
            xsql += " AND [" + Tenants.SQL_COL_BUILDING_NUMBER + "] BETWEEN '" + startBld + "' AND '" + endBld + "'" ;
            xsql += " ORDER BY [" + Tenants.SQL_COL_BUILDING_NUMBER + "], [" + Tenants.SQL_COL_TENANT_NUMBER + "]";
            Tenants[] tens = Tenants.Query(xsql,GlobalShared.NULL_DATE);
            if (tens == null || tens.Length == 0)
                return;
            
            Control ctl = new Control();
            ctl = Control.load(clientId);
            if (ctl != null)
            {
                pctOne = ctl.PercentOne;
                pctTwo = ctl.PercentTwo;
                pctTwo1 = ctl.PercentTwoYr1;
                years = ctl.MinimumIncreaseYears;
                minimumIncrease1 = ctl.MinimumIncrease1;
                minimumIncrease2 = ctl.MinimumIncrease2;
                increaseRent = ctl.MinimumIncreaseRent;
                ctl = null;
                if (parms != null)
                    chkMinimum = Convert.IsDBNull(parms.UseMinimumIncrease) ? false : parms.UseMinimumIncrease;
                parms = null;
            }

            rpt.FontBold = true;
            Heading();
            foreach (Tenants ten in tens)
            {
                rpt.PrintPageLine( ten.BuildingNumber + ten.TenantNumber + " " + ten.TenantName.Truncate(15), false);
                string[] l1 = {"tab(29)", ten.Apartment ,"tab(35)", ten.Type, "tab(38)", ten.RentLegal.ToString("######0.00").PadLeft(10)};
                rpt.PrintPageLine(l1, false);
                if (ten.RentLegal != ten.Rent)
                {
                    string[] l2 = {"tab(52)", ten.Rent.ToString("######0.00")};
                    rpt.PrintPageLine(l2, false);
                }
                chgs = Charges.loadByChargeCode(ten.SequenceNumber, prefCode);
                if (chgs != null && chgs.Length > 0 )
                {
                    string[] l3 = {"tab(65)", chgs[0].Amount.ToString("######0.00").PadLeft(10)};
                    rpt.PrintPageLine(l3, false);
                }
                moveInDate = ten.MoveinDate;
                if (moveInDate == GlobalShared.NULL_DATE)
                    moveInDate = DateTime.Now;
                string[] l4 = {"tab(70)", NewRent(ten.RentLegal, pctOne, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease1)};
                rpt.PrintPageLine(l4, false);
                l4[0] = "tab(81)";
                l4[1] = NewRent(ten.RentLegal, pctTwo1, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2);
                rpt.PrintPageLine(l4, false);
                l4[0] = "tab(91)";
                l4[1] = NewRent(ten.RentLegal, pctTwo, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2);
                rpt.PrintPageLine(l4, false);

                if (ten.RentLegal != ten.Rent)
                {
                    if (ten.RentLegal >= increaseRent)
                    {
                        l4[0] = "tab(100)";
                        l4[1] = NewRent( ten.Rent, pctOne, years, ten.LeaseExpirationDate, DateTime.Now, increaseRent, minimumIncrease1);
                        rpt.PrintPageLine(l4, false);
                        l4[0] = "tab(108)";
                        l4[1] = NewRent(ten.Rent, pctTwo1, years, ten.LeaseExpirationDate, DateTime.Now, increaseRent, minimumIncrease2);
                        rpt.PrintPageLine(l4,false);
                        l4[0] = "tab(117)";
                        l4[1] = NewRent(ten.Rent, pctTwo, years, ten.LeaseExpirationDate, DateTime.Now, increaseRent, minimumIncrease2);
                        rpt.PrintPageLine(l4, false);

                    }
                    else {
                        l4[0] = "tab(100)";
                        l4[1] = NewRent(ten.Rent, pctOne, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease1);
                        rpt.PrintPageLine(l4, false);
                        l4[0] = "tab(108)";
                        l4[1] = NewRent(ten.Rent, pctTwo1, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2);
                        rpt.PrintPageLine(l4,false);
                        l4[0] = "tab(117)";
                        l4[1] = NewRent(ten.Rent, pctTwo, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2);
                        rpt.PrintPageLine(l4, false);

                    }
                }
                l4[0] = "tab(129)";
                l4[1] = "|_________| |_________| |_________| |_________| |_________|";
                rpt.PrintPageLine(l4);
                if (rpt.PrintPageLine(""))
                {
                    rpt.NewPage();
                    Heading();
                }
            }
            if (target == 0)
                rpt.DisplayDocument();
            else
            {
                rpt.PrintDocument(printer);
            }
	{

	}
        }

        private void Heading()
        {
            rpt.PrintPageLine("PREFERENTIAL RENT WORKSHEET for Leases Expiring on " + leaseDate.ToString("MMMM d, yyyy"), headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine(leaseDate.ToString("MM/dd/yyyy") + "     page " + (page++).ToString(), dateFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            //string[] h1 = { "Tenant#", "tab(9)", "Tenant Name", "tab(35)", "Apt ", "tab(40)", "Type" };
            string[] h1 = { "Tenant#", "tab(9)", "Tenant Name", "tab(29)", "Apt ", "tab(35)", "Type" };
            rpt.PrintPageLine(h1, false);
            string[] h2 = { "tab(41)", "LegalRent  PreRent  PromptPay   1yrLegal   2yrLegal1 2yrLegal 1yrPref 2yrPref-1 2yrPref" };
            rpt.PrintPageLine(h2, false);
            h2[0] = "tab(129)";
            h2[1] = "1yrPrefAmt  2yrPref-1  2yrPrefAmt    1yrPrompt   2yrPrompt";
            rpt.PrintPageLine(h2);
            rpt.FontUnderline = false;
            rpt.PrintPageLine("");
        }

        private string NewRent(decimal r, float p, int years, System.DateTime leaseDate, System.DateTime moveindate, decimal increaseRent, decimal minimumIncrease)
        {
	        System.DateTime d1;
	        System.DateTime d2;
	        d1 = new DateTime(leaseDate.Year, leaseDate.Month,  leaseDate.Day).AddDays(1);
	        d2 = new DateTime(moveindate.Year + years, moveindate.Month, moveindate.Day);
	        if (chkMinimum && r < increaseRent && d2 <= d1) {
		        return (r + minimumIncrease).ToString("######0.00").PadLeft(10);
	        }
            else 
            {
                double p0 = (double)p;
                decimal d = (decimal)p0;
                d = Math.Round(d, 3);
                int x = (int)(d * 1000);
                d = (decimal)(x * .00001);
                r = Math.Round(r, 2);
                decimal i = Math.Round( r * d, 2);
		        return (r + i).ToString("######0.00").PadLeft(10);
	        }
        }

        public string StartBuilding
        {
            get { return startBld; }
            set { startBld = value; }
        }
        public string EndBuilding
        {
            get { return endBld; }
            set { endBld = value; }
        }
        public DateTime LeaseDate
        {
            get { return leaseDate; }
            set { leaseDate = value; }
        }
        public string PrefCode
        {
            get { return prefCode; }
            set { prefCode = value; }
        }
    }
}
