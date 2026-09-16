using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using ExtensionMethods;

namespace MbcsCentral.WinPM
{
    public class PreferredReport : IDisposable
    {
 
        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = new Report("Preferential Rent Report");
        Font fnt = new Font("Courier New", (float)9);
        Font headerFont = new Font("Arial", 20);
        Font dateFont = new Font("Arial", 16);
        int page = 1;
        static string filename = GlobalShared.appPath + "\\leaseletters.ser";
        LeaseLetterParms parms = LeaseLetterParms.DeSerialize(filename);

        float pctOne;
        float pctTwo;
        float pctTwoOne;
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

        public PreferredReport(string startBld, string endBld, DateTime dt, string code, int target, System.Printing.PrintQueue printer = null)
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
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
            rpt.Dispose();
        }
        public void Process()
        {
            DateTime dateFrom = new DateTime(leaseDate.Year, leaseDate.Month, 1);
            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);
            DateTime moveInDate;
            decimal newRent1;
            decimal newRent2;
            decimal newRent2_1;
            decimal newRentSec1;
            decimal newRentSec2;
            decimal newSecurity1;
            decimal newSecurity2;
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
                pctTwoOne = ctl.PercentTwoYr1;
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
                rpt.PrintPageLine( ten.BuildingNumber + ten.TenantNumber + " " + ten.TenantName.Left(20), false);
                string[] l1 = {"tab(29)", ten.Apartment , "tab(32)", ten.RentLegal.ToString("######0.00").PadLeft(10)};
                rpt.PrintPageLine(l1, false);
                if (ten.RentLegal != ten.Rent)
                {
                    string[] l2 = {"tab(45)", ten.Rent.ToString("######0.00")};
                    rpt.PrintPageLine(l2, false);
                }
                chgs = Charges.loadByChargeCode(ten.SequenceNumber, prefCode);
                if (chgs != null && chgs.Length > 0 )
                {
                    string[] l3 = {"tab(57)", chgs[0].Amount.ToString("######0.00").PadLeft(10)};
                    rpt.PrintPageLine(l3, false);
                }
                moveInDate = ten.MoveinDate;
                if (moveInDate == GlobalShared.NULL_DATE)
                    moveInDate = DateTime.Now;
                string[] l4 = {"tab(63)", NewRent(ten.RentLegal, pctOne, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease1)};
                rpt.PrintPageLine(l4, false);
                l4[0] = "tab(74)";
                l4[1] = NewRent(ten.RentLegal, pctTwoOne, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2);
                rpt.PrintPageLine(l4, false);
                l4[0] = "tab(85)";
                l4[1] = NewRent(ten.RentLegal, pctTwo, years, ten.LeaseExpirationDate, moveInDate, increaseRent,minimumIncrease2);
                rpt.PrintPageLine(l4 , false);
                newRent1 = ten.ProposedRent;
                newRent2_1 = ten.ProposedRent2_1;
                newRent2 = ten.ProposedRent2;
                newRentSec1 = ((decimal)ten.ProposedRent == 0 ? decimal.Parse(NewRent(ten.RentLegal, pctOne, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease1)) : ten.ProposedRent);
                newRentSec2 = ((decimal)ten.ProposedRent2 == 0 ? decimal.Parse(NewRent(ten.RentLegal, pctTwo, years, ten.LeaseExpirationDate, moveInDate, increaseRent, minimumIncrease2)) : ten.ProposedRent2);
                newSecurity1 = newRentSec1 - ten.Security;
                if (newSecurity1 < 0)
                    newSecurity1 = 0;
                newSecurity2 = newRentSec2 - ten.Security;
                if (newSecurity2 < 0)
                    newSecurity2 = 0;
                l4[0] = "tab(97)";
                l4[1] = newRent1.ToString("######0.00").PadLeft(10);
                rpt.PrintPageLine(l4,false);
                l4[0] = "tab(108)";
                l4[1] = newRent2_1.ToString("#####0.00").PadLeft(10);
                rpt.PrintPageLine (l4,false);
                l4[0] = "tab(119)";
                l4[1] = newRent2.ToString("######0.00").PadLeft(10);
                rpt.PrintPageLine(l4,false);
                l4[0] = "tab(130)";
                l4[1] = ten.PromptPay1.ToString("######0.00").PadLeft(10) + " " +ten.PromptPay2.ToString("######0.00").PadLeft(10) + " " + ten.Security.ToString("######0.00").PadLeft(10) + " " + newSecurity1.ToString("######0.00").PadLeft(10) + " " + newSecurity2.ToString("######0.00").PadLeft(10);
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
                rpt.PrintDocument(printer);
	{

	}
        }

        private void Heading()
        {
            rpt.PrintPageLine("PREFERENTIAL RENT REPORT for Leases Expiring on " + leaseDate.ToString("MMMM d, yyyy"), headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "     page " + (page++).ToString(), dateFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            string[] h1 = { "Tenant#", "tab(9)", "Tenant Name", "tab(29)", "Apt " };
            rpt.PrintPageLine(h1, false);
            string[] h2 = { "tab(35)", "LegalRent PreRent  PromptPay   1yrLegal   2yrLgl-1   2yrLegal    1yrPref   2yrPrf-1    2yrPref" };
            rpt.PrintPageLine(h2, false);
            h2[0] = "tab(131)";
            h2[1] = "1yrPrompt  2yrPrompt   Security   AddSec-1   AddSec-2";
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
	        } else {
		        return (r + (r * (decimal)p * (decimal)0.01)).ToString("######0.00").PadLeft(10);
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

