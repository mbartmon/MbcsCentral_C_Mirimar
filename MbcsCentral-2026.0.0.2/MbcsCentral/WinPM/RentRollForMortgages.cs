using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM
{
    public class RentRollForMortgages
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", 12);
        Font headerFont = new Font("Arial", 14);
        DateTime rptDate;
        int page = 1;
        string[] header = {"tab(4)", "Apartment", "tab(4)", " Rooms ", "tab(4)", " Type ", "tab(160)", "Tenant Name", "tab(15)", "MONTHLY RENT", "Lease Exp" };
        ArrayList parms = new ArrayList();
        DataTable dt = null;
        string ProcName = "";
        string Building = "";
        string[] address = new string[2];
        private int target;
        public int Target
        {
            set { target = value; }
        }

        public RentRollForMortgages(DateTime rptdate)
        {
            rptDate = rptdate;
        }

        public RentRollForMortgages(string building, DateTime rptdate, int target = 0)
        {
            this.rptDate = rptdate;
            this.target = target;
            Building = building;
        }

        public void Process(System.Printing.PrintQueue printer = null)
        {
            int BuildingNumber = 0;
            int.TryParse(Building, out BuildingNumber);
            if (BuildingNumber == 0)
            {
                MessageBoxCustom.Show("No Building selected.");
                return;
            }
            Building building = WinPM.Building.loadByBuildingNumber(Building, GlobalShared.user.Client.clientId);
            if (building != null)
            {
                address[0] = building.Address1;
                address[1] = building.Address2;
            }
            

            rpt = new Report("Rent Roll For Mortgages", "Courier New", 12);
            rpt.SetFont = fnt;
            rpt.LeftMargin = (float).25 * 96;
            rpt.TopMargin = 5; // (float).25 * 96;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)50;


            SqlConnection conn = new SqlConnection(GlobalShared.SqlConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            dt = new DataTable();

            SqlCommand cmd = new SqlCommand("mbcs_RENT_ROLL_FOR_MORTGAGES", conn);
            cmd.Parameters.Add(new SqlParameter("@BuildingNumber", Building));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            Heading();
            bool lineprinted = false;
            foreach(DataRow dr in dt.Rows)
            {
                rpt.PrintPageLine(new string[] { dr[Tenants.SQL_COL_APARTMENT].ToString(), "tab(13)", dr[Tenants.SQL_COL_ROOMS].ToString(), "tab(19)", dr[Tenants.SQL_COL_TYPE].ToString(), "Tab(24)", dr[Tenants.SQL_COL_TENANT_NAME].ToString(), "tab(57)", ((decimal)dr["Monthly Rent"]).ToString("$###,##0.00").PadLeft(12), "tab(71)", ((DateTime)dr[Tenants.SQL_COL_LEASE_EXPIRATION_DATE]).ToShortDateString() });
                lineprinted = true;
                if (rpt.PrintPageLine())
                {
                    rpt.PrintPageLine(new string[] { rptDate.ToString("dddd, MMMM dd, yyyy"), "tab(75)", "Page " + Report.PAGE_TAG });
                    rpt.NewPage();
                    Heading(false);
                    lineprinted = false;
                }
            }
            if (lineprinted)
            {
                rpt.PrintPageLine(new string[] { rptDate.ToString("dddd, MMMM dd, yyyy"), "tab(75)", "Page " + Report.PAGE_TAG }, true, rpt.BottomMargin );

            }
            rpt.DisplayDocument();
        }

        private void Heading(bool printTop = true)
        {
            float fontsize = rpt.FontSize;
            if (printTop)
            {
                rpt.FontSize = 48;
                rpt.FontBold = true;
                rpt.PrintPageLine("RENT ROLL", false);
                rpt.FontSize = fontsize;
                rpt.PrintPageLine(new string[] { "tab(60)", rptDate.ToShortDateString() });
                rpt.PrintPageLine();
                rpt.PrintPageLine();
                rpt.PrintPageLine();
                rpt.PrintPageLine("For: " + address[0] + ", " + address[1]);
                rpt.PrintPageLine();
                rpt.PrintPageLine();
            }
            rpt.PrintPageLine(header, System.Windows.Media.Brushes.LightBlue);
            rpt.PrintPageLine();
        }
    }
}
