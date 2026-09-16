using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    class SelectChargeReport : ReportBase
    {
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", 10);
        Font headerFont = new Font("Arial", 20);
        int page = 1;
        private string[] columns = {"Tenant# Tenant Name", "tab(31)", "Apt Type    Amount     Arrears       Total        Paid     Balance"};
        private string[] totalLines = {"tab(38)", "___________ ___________ ___________ ___________ ___________"};
 
	    private decimal[,] totals = new decimal[3, 5]; 
	    private string xdsc;
        private string xbld;
        private string xchg;
        private string xaddr; 
       // private = null;
        private DataSet ds = null;
        private string startBld;
        private string endBld;
        private string chargeCode;
        private bool skipPage = false;
        private int target;
        private System.Printing.PrintQueue printer;
        
        public SelectChargeReport() {}

        public SelectChargeReport(string startBld, string endBld, string chargeCode, bool skipPage, int target, System.Printing.PrintQueue printer)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.chargeCode = chargeCode;
            this.skipPage = skipPage;
            this.target = target;
            this.printer = printer;
        }

        public override void Process()
        {
            string xsql;
            int i;
            int c;

            rpt = new Report("Select Charge Report", fnt.Name, fnt.Size);
            rpt.LineHeight = .165 * 96;
            rpt.LeftMargin = (float)8;
            rpt.TopMargin = (float) 5;
            rpt.BottomMargin = (float) 50;
            rpt.BottomOffset = (float)70;

            c = 0;
            xsql = "SELECT pm_Charges.[Charge Code], pm_Charges.Amount, pm_Charges.Arrears, pm_Charges.Paid, [Starting Date], pm_Charges.[Type], ";
            xsql += "pm_Tenants.[Building Number], pm_Tenants.[Tenant Number], [Tenant Name], Apartment, pm_Building.[Address 1] FROM pm_Tenants RIGHT JOIN(";
            xsql += "pm_Charges LEFT JOIN pm_Building ON pm_Charges.[Building Number] = pm_Building.[Building Number]) ON ";
            xsql += "pm_Charges.[Sequence Number] = pm_Tenants.[Sequence Number] WHERE pm_Charges.[Building Number] >= '";
            xsql += startBld + "' AND pm_Charges.[Building Number] <= '" + endBld + "'";
            if (chargeCode != "")
            {
                xsql += " AND pm_Charges.[Charge Code] = '" + chargeCode + "'";
            }
            if (startBld == endBld)
            {
                xsql += " ORDER BY pm_Charges.[Charge Code], pm_Charges.[Building Number], pm_Charges.[Tenant Number]";
            }
            else
            {
                xsql += " ORDER BY pm_Charges.[Building Number], pm_Charges.[Charge Code], pm_Charges.[Tenant Number]";
            }

            ds = clsDB.Query(xsql, GlobalShared.CN);

            if (ds == null)
                return;
            
            bool first = true;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Building Number"] == null || dr["Building Number"] is DBNull || dr["Tenant Number"] == null || dr["Tenant Number"] is DBNull || dr["Tenant Name"] == null || dr["Tenant Name"] is DBNull)
                    continue;

                if (first)
                {
                    xbld = (string) dr["Building Number"];
                    xchg = (string) dr["Charge Code"];
                    xdsc = GetDescription(xchg);
                    xaddr = (string) dr["Address 1"];
                    first = false;
                    heading();
                }
                if (!Information.IsDBNull(dr["Building Number"]) && !((string)dr["Building Number"]).Equals(xbld))
                {
                    for (i = 0; i <= 4; i++)
                    {
                        totals[1, i] = totals[1, i] + totals[0, i];
                        totals[2, i] = totals[2, i] + totals[0, i];
                    }
                    if (chargeCode == "")
                        PrintTotals(0);
                    PrintTotals(1);
                    xbld =(string) dr["Building Number"];
                    xaddr = (string) dr["Address 1"];
                    xchg = (string) dr["Charge Code"];
                    xdsc = GetDescription(xchg);
                    if (skipPage)
                    {
                        rpt.NewPage();
                        heading();
                    }
                    else
                    {
                        rpt.PrintPageLine("");
                        rpt.PrintPageLine("");
                        rpt.FontBold = true;
                        rpt.PrintPageLine("Bldg: " + xbld + "     " + xaddr);
                        if (chargeCode == "")
                        {
                            rpt.FontUnderline = true;
                            rpt.PrintPageLine(xchg + "-" + xdsc.ToUpper());
                            rpt.FontUnderline = false;
                        }
                        rpt.FontBold = false;
                    }
                }
                if (!Information.IsDBNull(dr["Charge Code"]) && !((string)dr["Charge Code"]).Equals(xchg))
                {
                    for (i = 0; i <= 4; i++)
                    {
                        totals[1, i] = totals[1, i] + totals[0, i];
                        totals[2, i] = totals[2, i] + totals[0, i];
                    }
                    //If BuildingStart <> BuildingEnd Then Call PrintTotals(0)
                    if (c > 0) { PrintTotals(0); c = 0; }
                    xchg = (string)dr["Charge Code"];
                    xdsc = GetDescription(xchg);
                }
                if (!Information.IsDBNull(dr["Building Number"]) && ((DateTime)dr["Starting Date"] == GlobalShared.NULL_DATE || (DateTime)dr["Starting Date"] <= DateTime.Now))
                {
                    if (c == 0 & chargeCode == "")
                    {
                        rpt.FontBold = true;
                        rpt.PrintPageLine("");
                        rpt.FontBold = false;
                        rpt.FontUnderline  = true;
                        rpt.PrintPageLine(xchg + "-" + xdsc.ToUpper());
                        rpt.FontUnderline  = false;
                        rpt.FontBold = false;
                        rpt.FontBold = false;
                    }
                    string[] data0 = { (string)dr["Building Number"] + (string)dr["Tenant Number"], "tab(9)", (string)dr["Tenant Name"], "tab(31)", (string)dr["Apartment"], "tab(36)", (string)dr["Type"] };
                    rpt.PrintPageLine(data0, false);
                    string[] data1 = {"tab(37)", ((decimal)dr["Amount"]).ToString("##,##0.00").PadLeft(11)};
                    rpt.PrintPageLine(data1, false);
                    string[] data2 = {"tab(49)", ((decimal)dr["Arrears"]).ToString("##,##0.00").PadLeft(11) };
                    rpt.PrintPageLine(data2,false);
                    string[] data3 = {"tab(61)", ((decimal)dr["Amount"] + (decimal)dr["Arrears"]).ToString("##,##0.00").PadLeft(11)};
                    rpt.PrintPageLine(data3, false);
                    string[] data4 = {"tab(73)", ((decimal)dr["Paid"]).ToString("##,##0.00").PadLeft(11)} ;
                    rpt.PrintPageLine(data4,false);
                    string[] data5 = {"tab(85)", ((decimal)dr["Amount"] + (decimal)dr["Arrears"] - (decimal)dr["Paid"]).ToString("##,##0.00").PadLeft(11)};
                    if (rpt.PrintPageLine(data5))
                    {
                        rpt.NewPage();
                        heading();
                    }
                    c = c + 1;
                    totals[0, 0] = totals[0, 0] + (decimal) dr["Amount"];
                    totals[0, 1] = totals[0, 1] + (decimal)dr["Arrears"];
                    totals[0, 2] = totals[0, 0] + totals[0, 1];
                    totals[0, 3] = totals[0, 3] + (decimal)dr["Paid"];
                    totals[0, 4] = totals[0, 2] - totals[0, 3];
                }
            }
            for (i = 0; i <= 4; i++)
            {
                totals[1, i] = totals[1, i] + totals[0, i];
                totals[2, i] = totals[2, i] + totals[0, i];
            }
            if (chargeCode == "")
                PrintTotals(0);
            if (startBld != endBld)
                PrintTotals(1);
            PrintTotals(2);
            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }
        
    private void heading()
    {
	    rpt.PrintPageLine("");
	    rpt.PrintPageLine("");
	    rpt.FontBold = true;
	    if (chargeCode != "") {
		    rpt.PrintPageLine("SELECT REPORT FOR CHARGE TYPE: " + xchg + " " + xdsc);
	    } else {
		    rpt.PrintPageLine("SELECT REPORT FOR ALL CHARGES");
	    }
	    rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "     page " + (page++).ToString());
	    rpt.PrintPageLine("");
	    rpt.PrintPageLine("Bldg: " + xbld + "     " + xaddr);
	    //rpt.FontBold False: rpt.FontUnderline True: rpt.PrintPageLine xchg & "-" & UCase(xdsc): rpt.FontUnderline False: rpt.FontBold False
	    //rpt.FontUnderline True
	    rpt.PrintPageLine(columns);
	    rpt.FontBold = false;
	    rpt.FontUnderline  = false;
    }

    private void PrintTotals(int l)
    {
	    int i;
	    int j;
	    rpt.FontBold = true;
	    rpt.PrintPageLine(totalLines);
	    switch (l) {
		    case 0:
			    // Charge
			    rpt.PrintPageLine("Totals for Charge " + xchg + " " + xdsc, false);
			    break;
		    case 1:
			    // Building
			    rpt.PrintPageLine("Totals for Building " + xbld, false);
			    break;
		    case 2:
			    // Report
			    rpt.PrintPageLine("Totals for Report ", false);
			    break;
	    }
	    for (i = 0; i <= 4; i++) 
        {
            string[] data = {"tab(" + (i * 12 + 37).ToString("###") + ")", (totals[l, i]).ToString("##,##0.00").PadLeft(11)};
		    rpt.PrintPageLine(data,false);
		    for (j = 0; j <= l; j++) 
            {
			    totals[j, i] = 0;
		    }
	    }
	    rpt.PrintPageLine("");
        if (rpt.PrintPageLine(totalLines) && l != 2)
        {
            rpt.NewPage();
            heading();
        }
	    rpt.FontBold = false;
    }    

        private string GetDescription(string c)
        {
            ChargeCodes codes = ChargeCodes.load(c, clientId);
            if (codes != null)
                return codes.ChargeDescription;
            else
                return "";
        }

        public string StartBld
        {
            set { startBld = value; }
        }
        public string EndBld
        {
            set { endBld = value; }
        }
        public string ChargeCode
        {
            set { chargeCode = value; }
        }
        public bool SkipPage
        {
            set { skipPage = value; }
        }
        public int Target
        {
            set { target = value; }
        }
    }
}
