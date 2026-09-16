using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM
{
    public class MaintenanceReport
    {
        public string BuildingNumber { get;set; }
        public string TenantNumber { get; set; }
        public string Apartment { get; set; }
        public string TenantName { get; set; }

        static string title = "Improvment/Maintenance Records";
        private Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 9.0F;

        string[] heading = new string[2];

        string[][] ImprovHeading = { new string[] { "APARTMENT IMPROVEMENT RECORDS" }, new string[] { "    date", "tab(13)", "type", "tab(30)", "brand", "tab(47)", "model", "tab(63)", "size", "tab(75)", "color", "tab(86)", "vendor", "tab(107)", "cost", "tab(112)", "IncreaseAmt", "tab(124)", "IncreaseDate" } }; 
        string[][] MaintHeading = {new string[] {"APARTMENT MAINTENANCE RECORDS"}, new string[] {"    date", "tab(13)", "description", "tab(104)", "vendor", "tab(119)", "cost   ins"}};

        Maintenance[] maints = null;
        Improvements[] improvs = null;

        public MaintenanceReport() 
        {
        }

        public void print_report()
        {
            rpt = new Report(title, fontname, fontsize) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)40, BottomOffset = (Single)(40)};
            Building bld = Building.loadByBuildingNumber(BuildingNumber, GlobalShared.user.Account.clientId);
            heading[0] = "Tenant# " + BuildingNumber + TenantNumber + " Apt: " + Apartment + "  Address: " + (bld != null ? bld.Address1 : "* Building Not Found");
            heading[1] = "Current Tenant: " + TenantName;
            improvs = Improvements.load(BuildingNumber, TenantNumber, GlobalShared.user.Account.clientId);
            maints = Maintenance.load(BuildingNumber, TenantNumber, GlobalShared.user.Account.clientId);
            if ((improvs == null || improvs.Length == 0) && (maints == null || maints.Length == 0) )
            {
                return;
            }
            rpt.PageOrientation = (int)Report.Orientation.Landscape;
            rpt.FontBold = true;
            ReportHeader();
            ImprovRecords();
            rpt.PrintPageLine("");
            MaintRecords();
            rpt.DisplayDocument();
        }

        void ImprovRecords()
        {
            if (improvs == null || improvs.Length == 0)
            {
                rpt.PrintPageLine(" - No records -");
            }
            else
            {
                ImprovHeader();
                foreach (Improvements improv in improvs)
                {
                    if (rpt.PrintPageLine(new string[]  {improv.ImprovementDate.ToShortDateString().PadLeft(11), "tab(13)", improv.Type.PadRight(10), "tab(30)", improv.Brand, "tab(47)", 
                        improv.Model, "tab(63)", improv.Size, "tab(75)", improv.Color, "tab(86)", improv.Vendor, "tab(102)", improv.Cost.ToString("##,##0.00").PadLeft(9), "tab(114)", 
                        improv.RentIncreaseAmount.ToString("##,##0.00").PadLeft(9), "tab(126)", (improv.RentIncreaseDate > GlobalShared.NULL_DATE ? improv.RentIncreaseDate.ToShortDateString() : "") }))
                    {
                        rpt.NewPage();
                        ReportHeader();
                        ImprovHeader();
                    }
                }
            }
        }

        void MaintRecords()
        {

            const int descLength = 90;

            if (maints == null || maints.Length == 0)
            {
                rpt.PrintPageLine(" - no records -");
            }
            else
            {
                bool newPage;
                MaintHeader();
                newPage = false;
                int j;
                string z = "";
                string[] dsc = new string[50];
                foreach (Maintenance maint in maints)
                {
                    dsc.Initialize();
                    string x = maint.Description;
                    if (x.Length <= descLength)
                    {
                        dsc[0] = x;
                        j = 0;
                    }
                    else
                    {
                        j = 0;
                        string str = "";
                        for (int i = 0; i < x.Length ; i += descLength )
                        {
                            z = " ";
                            //On Error Resume Next
                            try
                            {
                                z = x.Substring(i + descLength, 1);
                            }
                            catch { }
                            try
                            {
                                dsc[j] = MemoParse(str + x.Substring(i, Math.Min(x.Substring(i).Length, descLength)) + z, descLength, out str);
                            }
                            catch { }
                            j++;
                        }
                    }
                    rpt.PrintPageLine(new string[]  {maint.MaintenanceDate.ToShortDateString().PadLeft(11), "tab(13)", dsc[0], "tab(104)", maint.Vendor, "tab(115)", maint.Cost.ToString("#,###.00").PadLeft(8), "   ", (maint.Inspected ? "yes" : "")});
                    for (int i = 1; i < j; i++)
                    {
                        if (dsc[i].Trim() == "")
                            break;
                        if (rpt.PrintPageLine(new string[] {"tab(13)", dsc[i].Trim()}))
                            newPage = true;
                    }
                    if (newPage)
                    {
                        rpt.NewPage();
                        ReportHeader();
                        MaintHeader();
                    }
                }
            }
        }

        void ReportHeader()
        {
            rpt.PrintPageLine(heading[0]);
            rpt.PrintPageLine(heading[1]);
            rpt.PrintPageLine("");
        }

        void ImprovHeader()
        {
            rpt.PrintPageLine(ImprovHeading[0]);
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine(ImprovHeading[1]);
            rpt.FontUnderline = false;
        }

        void MaintHeader()
        {
            rpt.PrintPageLine(MaintHeading[0]);
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine(MaintHeading[1]);
            rpt.FontUnderline = false;
        }

        private string MemoParse(string subString, int maxlen, out string str)
        {  
            str = "";
            if (subString.Length <= maxlen)
                return subString;
            else
            {
                for (int i = maxlen; i > 1; i--)
                {
                    if (subString.Substring(i, 1) == " ")
                    {
                        //MemoParse = Left$(subString, i - 1)
                        str = subString.Substring( i , subString.Substring( i ).Length - 1);
                        return subString.Substring(0, i +1);
                    }
                }
                return subString;
            }
        }
    }

}
