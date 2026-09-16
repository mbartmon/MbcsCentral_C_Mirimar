using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace MbcsCentral.WinPM
{
	public class PaymentRegister : IDisposable
	{
        private long clientId = GlobalShared.user.Account.clientId;
        static string title = "Payment Register";
		private Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 12;
        private Font mainFont;
        private object[,] headers = new object[6,2];
		private short pg;
		private short ln;
		private Paytemp[] tblC;
        private Payments[] tblD;
		private decimal[,] totals = new decimal[2, 2];
		private string xbld;
		private string xaddr;
		private float fontsz;
		private short Totalcnt;
		private short Arrearscnt;

        //local variable(s) to hold property value(s)
		private bool printTarget;
        private bool onDemand = false;
        private string startBld = "";
        private string endBld = "";
        private DateTime fromDate;
        private DateTime toDate;

        public void Dispose()
        {
            rpt.Dispose();
            mainFont.Dispose();
            this.Dispose();
        }
        public bool PrinterTarget
        {
            get { return printTarget; }
            set { printTarget = value; }
        }
        public bool OnDemand
        {
            get { return onDemand; }
            set { onDemand = value; }
        }
        public string StartBld
        {
            set { this.startBld = value; }
        }
        public string EndBld
        {
            set { this.endBld = value; }
        }
        public DateTime FromDate
        {
            set { this.fromDate = value; }
        }
        public DateTime ToDate
        {
            set { this.toDate = value; }
        }

        public string LockBox { get; set; } = "";

        public PaymentRegister() { }
        /// <summary>
        /// Set printer as target for report
        /// </summary>
        /// <param name="printerTarget"></param>
        public PaymentRegister(bool printerTarget, bool onDemand = false)
        {
            printTarget = printerTarget;
            this.onDemand = onDemand;
        }

		public void print_report(System.Printing.PrintQueue printer = null)
		{
			string xsql = null;
			short i = 0;

            rpt = new Report(title,fontname,fontsize) {LeftMargin = (Single) 7, TopMargin = (Single) 5, RightMargin = (Single) 5, BottomMargin = (Single) 40, BottomOffset = (Single)( 45)};
            mainFont = new Font(fontname, (float)(fontsize), FontStyle.Regular);
            rpt.SetFont = mainFont;
            System.Array.Clear( totals, 0, totals.Length );
            if (onDemand)
            {
                tblD = Payments.loadForRegister(startBld, endBld, fromDate, toDate, clientId);
                if (tblD == null || tblD.Length == 0)
                    return;
                xbld = tblD[0].BuildingNumber;
                xaddr = tblD[0].Address1;
                heading();
                rpt.HeaderLines = headers;
                rpt.PrintHeader();
                foreach (Payments tbl in tblD)
                {
                    if (tbl.BuildingNumber != xbld)
                    {
                        PrintTotals(0);
                        xbld = tbl.BuildingNumber;
                        xaddr = tbl.Address1;
                    }
                    string[] st = {tbl.BuildingNumber + tbl.TenantNumber + "   " + tbl.Name,
				                "tab(45)", tbl.Apartment, "tab(53)", tbl.DateofEntry.ToString("MM/dd/yyyy"),
                                "tab(65)", tbl.Amount.ToString("#,###,##0.00").PadLeft(12), 
				                "tab(80)", tbl.SecurityCode};
                    if (rpt.PrintPageLine(st))
                        PageSkip();
                    totals[0, 0] = totals[0, 0] + tbl.Amount;
                    totals[0, 1] = totals[0, 1] + 1;
                    totals[1, 0] = totals[1, 0] + tbl.Amount;
                    totals[1, 1] = totals[1, 1] + 1;
                }
            }
            else
            {
                tblC = Paytemp.loadForRegister();
                if (tblC == null || tblC.Length == 0)
                    return;
                xbld = tblC[0].BuildingNumber;
                xaddr = tblC[0].Address1;
                heading();
                rpt.HeaderLines = headers;
                rpt.PrintHeader();
                foreach (Paytemp tbl in tblC)
                {
                    if (tbl.BuildingNumber != xbld)
                    {
                        PrintTotals(0);
                        xbld = tbl.BuildingNumber;
                        xaddr = tbl.Address1;
                    }
                    string[] st = {tbl.BuildingNumber + tbl.TenantNumber + "  " + tbl.Name,
				                "tab(45)", tbl.Apartment, "tab(53)", tbl.Date.ToString("MM/dd/yyyy"),
                                "tab(65)", tbl.Amount.ToString("#,###,##0.00").PadLeft(12), 
				                "tab(81)", tbl.Code, "tab(84)", tbl.Description};
                    if (rpt.PrintPageLine(st))
                    {
                        PageSkip();
                    }
                    totals[0, 0] = totals[0, 0] + tbl.Amount;
                    totals[0, 1] = totals[0, 1] + 1;
                    totals[1, 0] = totals[1, 0] + tbl.Amount;
                    totals[1, 1] = totals[1, 1] + 1;
                }
            }
			PrintTotals(0);
			PrintTotals(1);
            if (printTarget)
                rpt.PrintDocument(printer);
            else
                rpt.DisplayDocument();
        }

        private void heading()
		{
            headers[0,0] = "PAYMENT REGISTER " + (!LockBox.Trim().Equals("") ? LockBox + " - " : "- ") + "page " + rpt.PageTag;
            headers[0,1] = new Font("Courier New",14, FontStyle.Bold);
            headers[1,0] = DateAndTime.Now + "   User: " + GlobalShared.user.Account.login;
            headers[3,0] = "Tenant#  Tenant Name                        Apt       Date Pd          Amount   " + (string)(onDemand ? "Payment Code" : "Code Description");
            headers[3,1] = new Font("Courier New",12,FontStyle.Underline);
		}
		private void PrintTotals(int l)
		{
			object i = null;
			short j = 0;
			decimal x =  totals[l, 1];
            string s = "";
			rpt.FontBold = true;
			switch (l) {
				case 0:
					// Building
                    rpt.PrintPageLine( new string[] { "tab(65)", "============" } );
					s = "Building " + xbld + "   " + xaddr;
					break;
				case 1:
					// Report
					rpt.PrintPageLine("");
					s="Grand Total ";
					break;
			}
            rpt.PrintPageLine(new string[] {s, "tab(65)", totals[l, 0].ToString("#,###,##0.00").PadLeft(12)});
            if (!onDemand)
            {
			    if (l == 1)
				    rpt.PrintPageLine(new string[] {"tab(65)", "============"});
			    rpt.FontUnderline = true;
			    //rpt.PrintPageLine(new string[] {"tab(17)", "Number of Payments:", "tab(65)", x.ToString("#,###,##0.00").PadLeft(12)});
            }
			rpt.FontBold = false;
			rpt.FontUnderline = false;
			rpt.PrintPageLine("");
            if (rpt.PrintPageLine(""))
                PageSkip();
			totals[l, 1] = 0;
            totals[l, 0] = 0;
		}

        private void PageSkip()
        {
            rpt.NewPage();
            rpt.CurrentHeight = 0;
            rpt.PrintHeader();
        }

	}
}
