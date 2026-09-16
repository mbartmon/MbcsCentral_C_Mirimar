using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MbcsCentral.WinGL
{
    public class BalanceSheet
    {
        private int xYear;
        private Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 8;

        public string PLAccountName { get; set; }
        public string CompanyName { get; set; }
        public bool Cancel { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int PLAccount { get; set; }
        public short Department { get; set; }
        public string DepartmentName { get; set; }
        public bool Consolidated { get; set; }
        public short Company { get; set; }
        public int Target { get; set; }
       // public FixedDocument Document { get; set; }

        public FixedDocument print_report(PrintQueue printer = null)
        {
            object sd = null;

            string xsql = null;
            short m = 0;
            decimal a = default(decimal);
            decimal lTotal = default(decimal);
            decimal eTotal = default(decimal);
            decimal iTotal = default(decimal);
            decimal aTotal = default(decimal);
            decimal qTotal = default(decimal);
            decimal iTotal2 = default(decimal);
            decimal eTotal2 = default(decimal);
            string fldName = null;
            string aname = "";
            decimal s = default(decimal);
            int j = 0;
            int i = 0;
            int k = 0;
            int index;
            bool hdflag = false;
            System.DateTime StartDate = default(System.DateTime);
            System.DateTime EndDate = default(System.DateTime);

            //*************************
            // Get Profit/Loss amount *
            //*************************
            WinglGlobal.getLedgerStructure();
            Month++;
            int year = 0;
            if (Year == 0 || Year > 1)
            {
                if (Year > 1)
                    year = Year;
                else
                    year = DateTime.Now.Year;

                StartDate = new DateTime(year, 1, 1);
                EndDate = new DateTime(year, Month, DateTime.DaysInMonth(year, Month));
                xYear = DateTime.Now.Year;
            }
            else
            {
                StartDate = new DateTime(DateTime.Now.Year - 1, 1, 1);
                EndDate = new DateTime(DateTime.Now.Year - 1, Month- 1, DateTime.DaysInMonth(DateTime.Now.Year - 1, Month - 1 ));
                xYear = DateTime.Now.Year - 1;
            }

            rpt = new Report("Balance Sheet", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
            // Get Income total
            iTotal = 0;
            iTotal2 = 0;
            for (j = 0; j <= WinglGlobal.LH.GetUpperBound(1); j++)
            {
                for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                {
                    if (WinglGlobal.LH[4, j, k].type.Equals("A"))
                    {
                        index = 0;
                        int.TryParse(WinglGlobal.LH[4, j, k].key.Replace("_", ""), out index);
                        if (index == 0)
                            continue;
                        if (Consolidated)
                            iTotal += Transactions.AccountBalance(Company, index, StartDate, EndDate);
                        else
                            iTotal += Transactions.AccountBalance(Company, index, StartDate, EndDate, Department);
                    }
                }
            }
            // Get Expense total
            eTotal = 0;
            eTotal2 = 0;
            for (j = 0; j <= WinglGlobal.LH.GetUpperBound(1); j++)
            {
                for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                {
                    if (WinglGlobal.LH[5, j, k].type.Equals("A"))
                    {
                        index = 0;
                        int.TryParse(WinglGlobal.LH[5, j, k].key.Replace("_", ""), out index);
                        if (index == 0)
                            continue;
                        if (Consolidated)
                            eTotal += Transactions.AccountBalance(Company, index, StartDate, EndDate);
                        else
                            eTotal += Transactions.AccountBalance(Company, index, StartDate, EndDate, Department);
                    }
                }
            }

            //***********************
            // Now print the report *
            //***********************
            heading();
            // The Assets
            aTotal = 0;
            hdflag = false;
            rpt.PrintPageLine(Strings.Space(35) + "ASSETS");
            rpt.PrintPageLine("");
            for (j = 0; j <= WinglGlobal.LH.GetUpperBound(1); j++)
            {
                for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                {
                    if (WinglGlobal.LH[1, j, k].Description.Trim().Equals(""))
                        continue;
                    if (WinglGlobal.LH[1, j, k].type.Equals("S"))
                    {
                        if (s != 0)
                        {
                            rpt.PrintPageLine(Strings.Space(36) + "==============");
                            rpt.PrintPageLine(Strings.Space(36) + s.ToString("###,###,##0.00").PadLeft(14));
                            rpt.PrintPageLine("");
                            if (rpt.PrintPageLine(""))
                            {
                                rpt.NewPage();
                                heading();
                            }
                            hdflag = false;
                        }
                        s = 0;
                        sd = WinglGlobal.LH[1, j, k].Description;
                    }
                    if (WinglGlobal.LH[1, j, k].type.Equals("A"))
                    {
                        a = 0;
                        index = 0;
                        int.TryParse(WinglGlobal.LH[1, j, k].key, out index);
                        if (index == 0)
                            continue;
                        if (Consolidated)
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate);
                        else
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate, Department);
                        aname = WinglGlobal.LH[1, j, k].Description;
                        if (a != 0)
                        {
                            if (!hdflag)
                            {
                                rpt.PrintPageLine(Strings.Space(9) + sd);
                                if (rpt.PrintPageLine(""))
                                {
                                    rpt.NewPage();
                                    heading();
                                }
                                hdflag = true;
                            }
                            if (rpt.PrintPageLine(Strings.Space(5) + aname.PadRight(31) + a.ToString("###,###,##0.00").PadLeft(14)))
                            {
                                rpt.NewPage();
                                heading();
                            }
                            aTotal = aTotal + a;
                            s = s + a;
                        }
                    }
                }
            }
            if (s != 0)
            {
                rpt.PrintPageLine(Strings.Space(36) + "==============");
                rpt.PrintPageLine(Strings.Space(36) + s.ToString("###,###,##0.00").PadLeft(14));
                s = 0;
                rpt.PrintPageLine("");
            }
            rpt.PrintPageLine(Strings.Space(51) + "==============");
            rpt.PrintPageLine(Strings.Space(51) + aTotal.ToString("###,###,##0.00").PadLeft(14));
            rpt.PrintPageLine("");
            rpt.PrintPageLine(Strings.Space(65) + "==============");
            rpt.PrintPageLine(Strings.Space(65) + aTotal.ToString("###,###,##0.00").PadLeft(14));
            rpt.PrintPageLine("");
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                heading();
            }

            // The Liabilities
            rpt.PrintPageLine(Strings.Space(40) + "LIABILITIES");
            rpt.PrintPageLine("");
            lTotal = 0;
            hdflag = false;
            for (j = 0; j <= WinglGlobal.LH.GetUpperBound(1); j++)
            {
                for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                {
                    if (WinglGlobal.LH[2, j, k].Description.Trim().Equals(""))
                        continue;
                    if (WinglGlobal.LH[2, j, k].type.Equals("S"))
                    {
                        if (s != 0)
                        {
                            rpt.PrintPageLine(Strings.Space(36) + "==============");
                            rpt.PrintPageLine(Strings.Space(36) + s.ToString("###,###,##0.00").PadLeft(14));
                            rpt.PrintPageLine("");
                            if (rpt.PrintPageLine(""))
                            {
                                rpt.NewPage();
                                heading();
                            }

                            hdflag = false;
                        }
                        s = 0;
                        sd = WinglGlobal.LH[2, j, k].Description;
                    }
                    if (WinglGlobal.LH[2, j, k].type.Equals("A"))
                    {
                        index = 0;
                        int.TryParse(WinglGlobal.LH[2, j, k].key.Replace("_", ""), out index);
                        if (index == 0)
                            continue;
                        if (Consolidated)
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate);
                        else
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate, Department);
                        aname = WinglGlobal.LH[2, j, k].Description;
                        if (int.Parse(WinglGlobal.LH[2, j, k].key.Replace("_", "")) == PLAccount)
                            a = a + iTotal - eTotal;
                        if (a != 0)
                        {
                            if (!hdflag)
                            {
                                rpt.PrintPageLine(Strings.Space(9) + sd);
                                if (rpt.PrintPageLine(""))
                                {
                                    rpt.NewPage();
                                    heading();
                                }

                                hdflag = true;
                            }
                            if (rpt.PrintPageLine(Strings.Space(5) + aname.PadRight(31) + (-a).ToString("###,###,##0.00").PadLeft(14)))
                            {
                                rpt.NewPage();
                                heading();
                            }

                            lTotal += a;
                            s = s + a;
                        }
                    }
                }
            }
            if (s != 0)
            {
                rpt.PrintPageLine(Strings.Space(36) + "==============");
                rpt.PrintPageLine(Strings.Space(36) + (-s).ToString("###,###,##0.00").PadLeft(14));
                s = 0;
                rpt.PrintPageLine("");
            }
            rpt.PrintPageLine(Strings.Space(51) + "==============");
            rpt.PrintPageLine(Strings.Space(51) + (-lTotal).ToString("###,###,##0.00").PadLeft(14));
            rpt.PrintPageLine("");
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                heading();
            }

            // The Equity
            rpt.PrintPageLine(Strings.Space(35) + "EQUITY");
            rpt.PrintPageLine("");
            qTotal = 0;
            hdflag = false;
            int act;
            for (j = 0; j < 10; j++)
            {
                for (k = 0; k <= WinglGlobal.LH.GetUpperBound(2); k++)
                {
                    if (WinglGlobal.LH[3, j, k].Description.Trim().Equals(""))
                        continue;
                    a = 0;
                    if (WinglGlobal.LH[3, j, k].type.Equals("S"))
                    {
                        if (s != 0)
                        {
                            rpt.PrintPageLine(Strings.Space(36) + "==============");
                            rpt.PrintPageLine(Strings.Space(36) + s.ToString("###,###,##0.00").PadLeft(14));
                            rpt.PrintPageLine("");

                            hdflag = false;
                        }
                        s = 0;
                        sd = WinglGlobal.LH[3, j, k].Description;
                    }
                    if (WinglGlobal.LH[3, j, k].type.Equals("A"))
                    {
                        index = 0;
                        int.TryParse(WinglGlobal.LH[3, j, k].key.Replace("_", ""), out index);
                        if (index == 0)
                            continue;
                        if (Consolidated)
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate);
                        else
                            a = Transactions.AccountBalance(Company, index, StartDate, EndDate, Department);
                        aname = WinglGlobal.LH[3, j, k].Description;
                    }
                    act = 0;
                    int.TryParse(WinglGlobal.LH[3, j, k].key.Replace("_", ""), out act);
                    if (!WinglGlobal.LH[3, j, k].key.Contains("_") && act == PLAccount)
                    {
                        a = a - ((-iTotal) - eTotal);
                        aname = PLAccountName;
                    }
                    if (a != 0)
                    {
                        if (rpt.PrintPageLine(""))
                        {
                            rpt.NewPage();
                            heading();
                        }
                        if (!hdflag)
                        {
                            rpt.PrintPageLine(Strings.Space(9) + sd);
                            if (rpt.PrintPageLine(""))
                            {
                                rpt.NewPage();
                                heading();
                            }

                            hdflag = true;
                        }
                        rpt.PrintPageLine(Strings.Space(5) + aname.PadRight(31) + (-a).ToString("###,###,##0.00").PadLeft(14));
                        qTotal = qTotal + a;
                        s = s + a;
                    }
                }
            }
 
            if (s != 0)
            {
                rpt.PrintPageLine(Strings.Space(36) + "==============");
                rpt.PrintPageLine(Strings.Space(36) + (-s).ToString("###,###,##0.00").PadLeft(14));
                if (rpt.PrintPageLine(""))
                    if (rpt.PrintPageLine(""))
                    {
                        rpt.NewPage();
                        heading();
                    }

                s = 0;
            }

            rpt.PrintPageLine(Strings.Space(51) + "==============");
            rpt.PrintPageLine(Strings.Space(51) + (-qTotal).ToString("###,###,##0.00").PadLeft(14));
            rpt.PrintPageLine("");
            rpt.PrintPageLine(Strings.Space(65) + "==============");
            rpt.PrintPageLine(Strings.Space(65) + (-(lTotal + qTotal)).ToString("###,###,##0.00").PadLeft(14));

            // Out of Balance?
            if (aTotal != -(lTotal + qTotal))
            {
                string x = (string)(aTotal > -(lTotal + qTotal) ? "Assets " + (aTotal - -(lTotal + qTotal)).ToString("#,###,##0.00") + " greater than Liab/Equity" : "Liab/Equity " + (-(aTotal - -(lTotal + qTotal))).ToString("#,###,##0.00") + " greater than Assets");
                rpt.PrintPageLine();
                rpt.FontBold = true;
                rpt.PrintPageLine(Strings.Space(51) + x);
                rpt.FontBold = false;
            }

            if (Target == 1 && printer != null)
                rpt.PrintDocument(printer);
            else if(Target == 3)
            {
                rpt.NewPage(true);
                return rpt.Document;
            }
            else
                rpt.DisplayDocument();
            return null;
        }

        private void heading()
        {
            rpt.PrintPageLine(Strings.Space(30) + "BALANCE SHEET         page " + rpt.PageTag);
            rpt.PrintPageLine(Strings.Space(30) + CompanyName);
            rpt.PrintPageLine(Strings.Space(30) + "For Month of " + new DateTime(xYear, Month- 1, 1).ToString("MMMM, yyyy"));
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static BalanceSheet DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheet));
            StringReader rdr = new StringReader(inputString);
            BalanceSheet parms = (BalanceSheet)serializer.Deserialize(rdr);
            return parms;
        }

        public void setDates(int month, int year)
        {
            Month = month - 1;
            Year = year;
        }
    }
}