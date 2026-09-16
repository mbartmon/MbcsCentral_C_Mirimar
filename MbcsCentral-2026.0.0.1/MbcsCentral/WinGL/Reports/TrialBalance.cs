using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Printing;

namespace MbcsCentral.WinGL
{
    public class TrialBalance
    {
        private int xYear;
        private Report rpt;
        private short pg;
        private short ln;
        private Department[] tbl;
        private decimal credits;
        private decimal debits;
        private System.DateTime StartDate;
        private System.DateTime EndDate;

        public bool PriorYear { get; set; }
        public bool Cancel { get; set; }
        public bool XWorkArea { get; set; }
        public bool ZeroBalances { get; set; }
        public bool Consolidated { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string DepartmentName { get; set; }
        public short Department { get; set; }
        public string CompanyName { get; set; }
        public short Company { get; set; }

        public bool BatchReport { get; set; } = false;
        public int Target { get; set; }

        private string fontname = "Courier New";
        private float fontsize = 8;

        public FixedDocument print_report(int target)
        {
            Target = target;
            return print_report(null);
        }

        public FixedDocument print_report(PrintQueue printer = null)
        {
            int k;
            int j;
            int i;

            WinglGlobal.getLedgerStructure();
            xYear = Year;
            StartDate = new DateTime(xYear, 1, 1);
            EndDate = new DateTime(xYear, Month, DateTime.DaysInMonth(xYear, Month));
            credits = 0;
            debits = 0;
            rpt = new Report("Trial Balance", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 110 };
            if (XWorkArea)
                rpt.PageOrientation = (int)Report.Orientation.Landscape;
            int acctKey = 0;
            pg = 0;
            heading();
            for (i = 1; i <= 5; i++)
            {
                for (j = 0; j <= 10; j++)
                {
                    for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                    {
                        if ((string)WinglGlobal.LH[i, j, k].type == "C")
                        {
                            rpt.FontBold = true;
                            rpt.PrintPageLine(new string(' ', 5) + WinglGlobal.LH[i, j, k].Description.ToUpper().Trim());
                            if (rpt.PrintPageLine(""))
                            {
                                rpt.NewPage();
                                rpt.FontBold = false;
                                heading();
                            }
                            rpt.FontBold = false;
                        }
                        if ((string)WinglGlobal.LH[i, j, k].type == "A")
                        {
                            int.TryParse(WinglGlobal.LH[i, j, k].key, out acctKey);
                            ProcessAccount((string)WinglGlobal.LH[i, j, k].Description, acctKey);
                        }
                    }
                }
            }
            rpt.PrintPageLine(new string(' ', 41) + "============= =============");
            rpt.PrintPageLine(new string(' ', 5) + "TOTALS=>" + new string(' ', 28) + debits.ToString("#########0.00").PadLeft(13) + (-credits).ToString("#########0.00").PadLeft(14));
            if (Target == 3)
            {
                rpt.NewPage(true);
                return rpt.Document;
            }
            else if (printer != null)
            {
                rpt.NewPage(true);
                rpt.PrintDocument(printer);
            }
            else
                rpt.DisplayDocument();
            return null;
        }

        private void ProcessAccount(string desc, int key)
        {
            string xsql = null;
            string x = null;
            decimal bal = default(decimal);
            short i = 0;
            string ds = null;
            string dp = null;
            string z = null;

            ds = desc.Trim().Left(30).PadRight(31);
            bal = 0;
            if (Consolidated)
            {
                bal = Transactions.AccountBalance(Company, key, StartDate, EndDate);
                dp = "     ";
                print_line( ds,  dp,  bal);
            }
            else
            {
                if (Department <= 0)
                {
                    tbl = MbcsCentral.WinGL.Department.loadByCompany((short)Company);
                    foreach (Department dpt in tbl)
                    {
                        bal = Transactions.AccountBalance(Company, key, StartDate, EndDate, dpt.DepartmentNumber);
                        dp = dpt.DepartmentNumber.ToString("###0").PadLeft(4) + " ";
                        print_line( ds,  dp,  bal);
                        ds = new string(' ', 31);
                    }
                }
                else
                {
                    bal = Transactions.AccountBalance(Company, key, StartDate, EndDate, Department);
                    dp = Department.ToString("###0").PadLeft(4) + " ";
                    print_line( ds,  dp,  bal);
                }
            }
        }

        private void print_line( string ds,  string dp,  decimal bal)
        {
            string x = null;

            if (ZeroBalances || bal != 0)
            {
                x = new string(' ', 5) + ds + dp;

                if (bal < 0)
                {
                    x = x + new string(' ', 14) + (-bal).ToString("#########0.00").PadLeft(13);
                    credits = credits + bal;
                }
                else if (bal == 0)
                {
                    x = x + new string(' ', 27);
                }
                else
                {
                    x = x + bal.ToString("#########0.00").PadLeft(13) + new string(' ', 14);
                    debits = debits + bal;
                }

                x = x + " _____________ _____________";
                if (XWorkArea)
                {
                    x = x + " _____________ _____________";
                }
                rpt.PrintPageLine(x);
                if (rpt.PrintPageLine(""))
                {
                    rpt.NewPage();
                    heading();
                }
            }
        }

        private void heading()
        {
            string x = "";

            pg++;
            rpt.PrintPageLine(new string(' ', 5) + "TRIAL BALANCE as of " + new DateTime(xYear, Month, 1).ToString("MMMM, yyyy"));
            rpt.PrintPageLine(new string(' ', 5) + CompanyName + new string(' ', 25) + DateTime.Today.ToString("MM/dd/yyyy") + new string(' ', 5) + "page " + pg);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            x = new string(' ', 5) + "Account" + new string(' ', 25) + "Dpt        debits       credits";
            if (XWorkArea)
            {
                x = x + new string(' ', 8) + "Balance Sheet" + new string(' ', 16) + "Profit/Loss";
            }
            rpt.PrintPageLine(x);
            rpt.PrintPageLine(new string(' ', 5) + new string('-', 119));
            ln = 6 + WinglGlobal.tmargin;
        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static TrialBalance DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TrialBalance));
            StringReader rdr = new StringReader(inputString);
            TrialBalance parms = (TrialBalance)serializer.Deserialize(rdr);
            return parms;
        }

        public void setDates(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}