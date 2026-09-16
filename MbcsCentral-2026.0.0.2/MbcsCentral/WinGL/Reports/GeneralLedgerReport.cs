using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Printing;
using System.Windows.Documents;
using System.Xml.Serialization;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
   public class GeneralLedgerReport
   {
        public short Company { get; set; } = 0;
        public short Department { get; set; } = 0;
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }
        public int Account { get; set; } = 0;
        public DateTime StartDate { get; set; } = GlobalShared.NULL_DATE;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool Consolidated { get; set; } = false;
        public bool PrintZeroBalances { get; set; } = false;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string Printer { get; set; }
        public int Target { get; set; }

        public bool SingleAccount { get; set; }
        public bool SingleDepartment { get; set; }

        public bool BatchReport { get; set; } = false;

        private const int DESCRIPTION_LENGTH = 26;
        Report rpt = null;
        PrintQueue printer;
        MajorCategoryList major;
        List<MajorCategories> majorList;
        Dictionary<string, Category> majorCats = new Dictionary<string, Category>();
        Accounts[] accounts;
        Department[] departments = new Department[1];

        DateTime cfystart;
        int fys = DateTime.Now.Month;

        // Total fields
        decimal crdTotal;
        decimal debTotal;
        decimal[] catTotals = new decimal[5];
        string fontname = "Courier New";
        float fontsize = 8;

        bool endOfPage = false;

        public FixedDocument process(PrintQueue printer = null)
        {
            rpt = new Report("General Ledger;" + CompanyName + ";" + StartDate.ToShortDateString() + " to " + EndDate.ToShortDateString(), fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
            Category[] cats = Category.loadBySubKey("0_");
            if (cats == null || cats.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Unable to retrieve Categories.");
                return null;
            }
            decimal catTotal; int padding;
            Category cat = null;
            for (short i = 0; i < 5; i++)
            {
                cat = cats[i];
                cat.Sequence = i;
                try
                {
                    majorCats.Add(cat.Key, cat);
                }
                catch { } 

            }
            this.printer = printer;
            major = new MajorCategoryList();
            majorList = major.GetCategories();

            GetCompanyInfo();

            if (!Consolidated)
            { 
                if (this.Department <= 0)
                {
                    departments = WinGL.Department.loadByCompany(this.Company);
                }
                else
                {
                    departments = new WinGL.Department[1];
                    departments[0] = new Department();
                    departments[0].DepartmentNumber = this.Department;
                    departments[0].Name = DepartmentName;
                }
            }
            ReportHeading();

            if (Account > 0)
            {
                // Print Single Account
                Accounts account = Accounts.load(Account);
                if (account != null)
                {
                    if (!Consolidated)
                    {
                        foreach (Department dpt in departments)
                        {
                            rpt.PrintPageLine("Dept: " + dpt.Name);
                            rpt.PrintPageLine("");
                            ProcessAccount(account.Description, account.Key, dpt.DepartmentNumber);
                        }
                    }
                    else
                    {
                        rpt.PrintPageLine("* CONSOLIDATED *");
                        rpt.PrintPageLine("");
                        ProcessAccount(account.Description, account.AccountNumber, 0);
                    }
                }
                else
                    rpt.PrintPageLine("     **** Account Not Found <key=" + Account.ToString() + "> *****");
            }
            else
            {
                string deptName = "";
                KeyValuePair<string, Category> keyValue;
                string s = "";
                // Print All Accounts
                if (!Consolidated)
                {
                    bool firstTime = true;

                    if (!firstTime)
                    {
                        rpt.NewPage();
                        ReportHeading();
                        firstTime = false;
                    }
                    foreach (Department dpt in departments)
                    {
                        rpt.PrintPageLine("Dept: " + dpt.Name);
                        rpt.PrintPageLine("");
                        IterateCategories(dpt.DepartmentNumber);
                        rpt.NewPage();
                        ReportHeading();
                        rpt.PrintPageLine("Dept: " + dpt.Name); rpt.PrintPageLine("");
                        for (int i = 0; i < 5; i++)
                        {
                            keyValue = majorCats.ElementAt(i);
                            deptName = ((Category)majorCats[keyValue.Key]).CategoryDescription;
                            catTotal = catTotals[i];
                            padding = 18;
                            if (catTotal >= 0)
                                s = catTotal.ToString("$###,###,##0.00");
                            else
                            {
                                s = (catTotal * -1).ToString("$###,###,##0.00CR");
                                padding = 16;
                            }
                            rpt.PrintPageLine(("TOTAL " + deptName).PadRight(40) + s.PadLeft(padding));
                            rpt.PrintPageLine("");
                        }
                    }
                }
                else
                {
                    rpt.PrintPageLine("* Consolidated *"); rpt.PrintPageLine("");
                    IterateCategories(0);
                    rpt.NewPage();
                    ReportHeading();
                    rpt.PrintPageLine("* CONSOLIDATED *"); rpt.PrintPageLine("");
                    for (int i = 0; i < 5; i++)
                    {
                        keyValue = majorCats.ElementAt(i);
                        deptName = ((Category)majorCats[keyValue.Key]).CategoryDescription;
                        catTotal = catTotals[i];
                        //padding = 18;
                        //if (catTotal >= 0)
                        //    s = catTotal.ToString("$###,###,##0.00");
                        //else
                        //{
                        //    s = (catTotal * -1).ToString("$###,###,##0.00CR");
                        //    padding = 16;
                        //}
                        rpt.PrintPageLine(("TOTAL " + deptName).PadRight(40) + catTotal.ToString("$###,###,##0.00;$###,###,##0.00CR").PadLeft(catTotal >= 0 ? 15 : 17));
                        rpt.PrintPageLine("");
                    }

                }
            }
            if (Target == 1)
                rpt.PrintDocument(printer);
            else if (Target == 3)
            {
                rpt.NewPage(true);
                return rpt.Document;
            }
            else
                rpt.DisplayDocument();
            return null;
        }
        
        void IterateCategories(short d)
        { 
            foreach (Category cat in majorList)
            {
                accounts = Accounts.loadByCategory(cat.Key, true);
                if (accounts != null && accounts.Length > 0)
                {
                   foreach(Accounts acct in accounts)
                    {
                        ProcessAccount(acct.Description, (short)acct.Key, d);
                        try
                        {
                            Category thisCat = majorCats[acct.MajorCategory];
                            catTotals[thisCat.Sequence] += (debTotal - crdTotal);
                        }
                        catch
                        { }
                    }
                }

                if (((MajorCategories)cat).SubCats != null)
                {
                    foreach (var subcat in ((MajorCategories)cat).SubCats)
                    {
                        accounts = Accounts.loadByCategory(subcat.Key, true);
                        if (accounts != null && accounts.Length > 0)
                        {
                            foreach (Accounts acct in accounts)
                            {
                                ProcessAccount(acct.Description, (short)acct.Key, d);
                                try
                                {
                                    Category thisCat = majorCats[acct.MajorCategory];
                                    catTotals[thisCat.Sequence] += (debTotal - crdTotal);
                                }
                                catch
                                { }
                            }
                        }

                        if (subcat.SubSubCats != null)
                        {
                            foreach (var subSubCat in subcat.SubSubCats)
                            {
                                accounts = Accounts.loadByCategory(subSubCat.Key,true);
                                if (accounts != null && accounts.Length > 0)
                                {

                                    foreach (Accounts acct in accounts)
                                    {
                                        ProcessAccount(acct.Description, (short)acct.Key, d);
                                        try
                                        {
                                            Category thisCat = majorCats[acct.MajorCategory];
                                            catTotals[thisCat.Sequence] += (debTotal - crdTotal);
                                        }
                                        catch
                                        { }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void ProcessAccount(string acctDescription, int key, short dept)
        {
            bool acctDesc = false;
            string transDesc; string transDesc1; string transDesc2; string transDesc3;
            bool full1; bool full2;
            string creditAmount = ""; string debitAmount = "";
            string line = "";
            decimal amount;

            if (endOfPage)
            {
                rpt.NewPage();
                ReportHeading();
                endOfPage = false;
            }

            crdTotal = 0; debTotal = 0;
            if (PrintZeroBalances)
            {
                AccountHeading(acctDescription);
                acctDesc = true;
            }

            string journal = "";
            Transactions[] trans = Transactions.loadForGL(this.Company, key, StartDate, EndDate, dept);
            if (trans != null && trans.Length > 0)
            {
                if (!acctDesc)
                {
                    AccountHeading(acctDescription);
                    acctDesc = true;
                }
                foreach (Transactions tran in trans)
                {
                    transDesc = tran.Description.Trim();
                    transDesc1 = "";
                    transDesc2 = "";
                    transDesc3 = "";
                    full1 = false;
                    full2 = false;
                    if (transDesc.Length > DESCRIPTION_LENGTH)
                    {
                        transDesc = transDesc.Replace("Invoice#", "Inv#").Replace("invoice#", "Inv#").Trim();
                        //string[] s = transDesc.Split(' ');
                        string[] s = transDesc.Wrap(26);
                        for (int i = 0; i < s.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    transDesc1 = s[i];
                                    break;
                                case 1:
                                    transDesc2 = s[i];
                                    break;
                                case 2:
                                    transDesc3 = s[i];
                                    break;
                            }
                        }
                    }
                    else
                        transDesc1 = transDesc;

                    if (tran.Amount < 0)
                    {
                        creditAmount = (-tran.Amount).ToString("########0.00");
                        debitAmount = " ";
                    }
                    else
                    {
                        debitAmount = tran.Amount.ToString("########0.00");
                        creditAmount = " ";
                    }
                    if (tran.Journal.Length > 8)
                        journal = tran.Journal.Substring(0,8);
                    else
                        journal = tran.Journal;

                    // line = string.Format("{0}  {1} {2} {3} {4} {5} | {6}", tran.Date.ToShortDateString().PadLeft(10), tran.CheckNumber.ToString("######").PadLeft(6), journal.Trim().Left(8,8),
                    //         tran.Source.Trim().Left(16, 17), transDesc.Left(26, 27), debitAmount.PadLeft(12), creditAmount.PadLeft(12));

                    line = string.Format("{0}  {1} {2} {3} {4} {5} | {6}", tran.Date.ToShortDateString().PadLeft(10), tran.CheckNumber.ToString("######").PadLeft(6), journal.Trim().Left(8, 8),
                            tran.Source.Trim().Left(16, 17), transDesc1.Left(DESCRIPTION_LENGTH, 27), debitAmount.PadLeft(12), creditAmount.PadLeft(12));

                    endOfPage = rpt.PrintPageLine(line);
                    if (transDesc2.Trim().Length > 0)
                    {
                        endOfPage = rpt.PrintPageLine(" ".PadLeft(46) + transDesc2.Left(DESCRIPTION_LENGTH, 27) + "|".PadLeft(15));
                    }
                    if (transDesc3.Trim().Length > 0)
                        endOfPage = rpt.PrintPageLine(" ".PadLeft(46) + transDesc3.Left(DESCRIPTION_LENGTH, 27) + "|".PadLeft(15));
                    if (endOfPage)
                    {
                        rpt.NewPage();
                        ReportHeading();
                        endOfPage = false;
                    }
                    if (decimal.TryParse(debitAmount, out amount))
                        debTotal += amount;
                    if (decimal.TryParse(creditAmount, out amount))
                        crdTotal += amount;
                }
                if (acctDesc)
                {
                    rpt.PrintPageLine("============ | ============".PadLeft(101));
                    line = string.Format(" {0}  {1} | {2} {3}", "ACCOUNT TOTALS=>".PadLeft(71), debTotal.ToString("########0.00").PadLeft(12), crdTotal.ToString("########0.00").PadLeft(12),
                                        (debTotal - crdTotal).ToString("##########0.00;########0.00CR").PadLeft((debTotal - crdTotal) >= 0 ? 15 : 17));
                    //(debTotal - crdTotal).ToString((string)(debTotal - crdTotal >= 0 ? "########0.00" : "########0.00CR")).PadLeft(14));
                    endOfPage = rpt.PrintPageLine(line);
                }
            }
        }

        void ReportHeading()
        {
            rpt.PrintPageLine(ReportDate.ToShortDateString() + "     Page " + Report.PAGE_TAG);
            rpt.PrintPageLine(CompanyName);
            rpt.PrintPageLine("GENERAL LEDGER");
            rpt.PrintPageLine(StartDate.ToShortDateString() + " to " + EndDate.ToShortDateString());
            rpt.PrintPageLine(""); rpt.PrintPageLine("");
        }

        void AccountHeading(string acctDescription)
        {
            rpt.FontBold = true;
            rpt.PrintPageLine(acctDescription); rpt.PrintPageLine("");
            rpt.FontBold = false;
            rpt.PrintPageLine("   date     check# journal  source            description                     debit    |    credit");
            rpt.PrintPageLine("                                                                          ------------ | ------------");
        }

        void GetCompanyInfo()
        {
            cfystart = WinglGlobal.CurrentFiscalYear(Company);
            fys = cfystart.Month;
            if (StartDate < cfystart)
            {
                if (StartDate.Month < fys)
                    cfystart = new DateTime(StartDate.Year - 1, fys, 1);
                else
                    cfystart = new DateTime(StartDate.Year, fys, 1);
            }
        }

        public void setDates(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static GeneralLedgerReport DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GeneralLedgerReport));
            StringReader rdr = new StringReader(inputString);
            GeneralLedgerReport parms = (GeneralLedgerReport)serializer.Deserialize(rdr);
            return parms;
        }
    }
}
