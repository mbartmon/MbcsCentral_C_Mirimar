using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
    class CheckReconReport
    {
        private const string Statement = "Statement";
        private const string BankErrorFor = "BnkErr+";
        private const string BankErrorAgainst = "BnkErr-";

        private Report rpt = null;
        static System.Drawing.Font fnt = new System.Drawing.Font("Courier New", 16);
        private short pg;
        private short ln;
        private bool cleared;

        //local variable(s) to hold property value(s)
        private int mvarCompany;
        private int mvarDepartment;
        private bool mvarCancel;
        private string mvarCompanyName;
        private string mvarDepartmentName;
        private System.DateTime mvarStatementDate;

        public System.DateTime StatementDate {
            get { return mvarStatementDate; }
            set { mvarStatementDate = value; }
        }
        public string DepartmentName {
            get { return mvarDepartmentName; }
            set { mvarDepartmentName = value; }
        }
        public string CompanyName {
            get { return mvarCompanyName; }
            set { mvarCompanyName = value; }
        }
        public bool Cancel {
            get { return mvarCancel; }
            set { mvarCancel = value; }
        }
        public int Department {
            get { return mvarDepartment; }
            set { mvarDepartment = value; }
        }
        public int Company {
            get { return mvarCompany; }
            set { mvarCompany = value; }
        }

        public decimal BookBalance { get; set; }
        public decimal StatementAmount { get; set; }

        public System.Windows.Controls.DataGrid grdChecks { get; set; }

        //******************************************************************************
        public void print_report()
        {
            //******************************************************************************

            decimal dt = 0;
            decimal aT = 0;
            decimal ot = 0;
            decimal ct = 0;
            decimal gt = 0;
            decimal ft = 0;
            decimal adj = 0;
            short i = 0;
            short j = 0;
            bool bnkheader = false;

            rpt = new Report("Bank Reconciliation Report", "Courier New", 14) { BottomMargin = 50, BottomOffset = (Single)50, LineHeight = (Single)0.150 * 96 };
            rpt.SetFont = fnt;
            rpt.LeftMargin = (float).1 * 96;
            rpt.TopMargin = (float).25 * 96;

            cleared = false;
            bnkheader = false;
            heading();
            rpt.FontBold = true;
            rpt.PrintPageLine();
            foreach (BankReconciliation check in grdChecks.Items) {
                if (check.ReconFlag) {
                    if (check.Type == "BnkErr+") {
                        if (!bnkheader) {
                            rpt.PrintPageLine(new string[] { "tab(9)", "Bank Errors In Your Favor" });
                            rpt.PrintPageLine();
                            rpt.FontBold = false;
                            rpt.PrintPageLine(new string[] { "tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount" });
                            if (rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- -----------" }))
                            {
                                rpt.NewPage();
                                heading();

                            }
                            bnkheader = true;
                        }
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy") }, false);
                        rpt.PrintPageLine(new string[] { "tab(16)", check.Payee.Left(31) }, false);
                        rpt.PrintPageLine(new string[] { "tab(50)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                        ft += check.StatementAmount;
                    }
                } else {
                    if (check.Type == "BnkErr+") {
                        if (!bnkheader) {
                            rpt.PrintPageLine(new string[] { "tab(9)", "Bank Errors In Your Favor" });
                            rpt.PrintPageLine();
                            rpt.FontBold = false;
                            rpt.PrintPageLine(new string[] { "tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount" });
                            if (rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- -----------" }))
                            {
                                rpt.NewPage();
                                heading();
                            }

                            bnkheader = true;
                        }
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy")}, false);
                        rpt.PrintPageLine(new string[] { "tab(16)", check.Payee.Left(31) }, false);
                        rpt.PrintPageLine(new string[] { "tab(50)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                        ft += check.StatementAmount;
                    }
                    if (check.Manual)
                    {
                        if (check.Type.Equals("DEPOSIT"))
                        {
                            adj += check.StatementAmount;
                        } else if (check.Equals("DEBIT"))
                        {
                            adj -= check.StatementAmount;
                        }
                    }
                }
            }
            if (bnkheader) {
                rpt.PrintPageLine(new string[] { "tab(42)", "Total=>", "tab(50)", ft.ToString("#######0.00").PadLeft(11) });
                rpt.PrintPageLine();
            }
            rpt.FontBold = true;
            rpt.PrintPageLine();
            bnkheader = false;
            foreach (BankReconciliation check in grdChecks.Items)
            {
                if (check.Type.Equals("BnkErr-"))
                {
                    if (!bnkheader) {
                        rpt.PrintPageLine(new string[] { "tab(9)", "Bank Errors In Bank's Favor" });
                        rpt.PrintPageLine();
                        rpt.FontBold = false;
                        rpt.PrintPageLine(new string[] { "tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount" });
                        if (rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- -----------" }))
                        {
                            rpt.NewPage();
                            heading();
                        }
                        bnkheader = true;
                    }
                    rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy")}, false);
                    rpt.PrintPageLine(new string[] { "tab(16)", check.Type.PadLeft(6) }, false);
                    rpt.PrintPageLine(new string[] { "tab(50)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                    gt += check.StatementAmount;
                    if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
                    {
                        rpt.NewPage();
                        heading();
                    }
                }
            }
            if (bnkheader)
            {
                rpt.PrintPageLine(new string[] { "tab(42)", "Total=>", "tab(50)", gt.ToString("#######0.00").PadLeft(11) });
                rpt.PrintPageLine();
            }
            if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
            {
                rpt.NewPage();
                heading();
            }

            rpt.FontBold = true;
            rpt.PrintPageLine(new string[] { "tab(9)", "Outstanding Deposits & Credits" });
            rpt.FontBold = false;
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] { "tab(6)", "  Date", "tab(18)", "Type", "tab(23)", "Description", "tab(55)", "Amount" });
            rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- -----------" });
            foreach (BankReconciliation check in grdChecks.Items)
            {
                if (!check.ReconFlag)
                {
                    if (check.Type.Equals("DEPOSIT"))
                    {
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy") }, false);
                        rpt.PrintPageLine(new string[] { "tab(15)", check.Type.PadLeft(6)}, false);
                        rpt.PrintPageLine(new string[] { "tab(23)", check.Payee.Left(31) }, false);
                        if (rpt.PrintPageLine(new string[] { "tab(50)", check.StatementAmount.ToString("#######0.00").PadLeft(11) }) || rpt.CurrentHeight >= rpt.MaxPageHeight)
                        {
                            rpt.NewPage();
                            heading();
                        }
                        aT += check.StatementAmount;
                    }
                }
            }
            rpt.PrintPageLine(new string[] { "tab(42)", "Total=>", "tab(50)", aT.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine();
            rpt.PrintPageLine();
            if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
            {
                rpt.NewPage();
                heading();
            }

            rpt.FontBold = true;
            rpt.PrintPageLine(new string[] { "tab(9)", "Outstanding Checks & Debits" });
            rpt.FontBold = false;
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] { "tab(6)", "  Date", "tab(16)", "Chk#", "tab(23)", "Description", "tab(55)", "Amount" });
            if (rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- -----------" }))
            {
                rpt.NewPage();
                heading();
            }

            ot = 0;
            foreach (BankReconciliation check in grdChecks.Items)
            {
                if (!check.ReconFlag)
                {
                    if (check.Type != "DEPOSIT" && !check.Type.Contains("BnkErr") && !check.Type.Equals(Statement))
                    {
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy") }, false);
                        rpt.PrintPageLine(new string[] { "tab(16)", check.Type.PadLeft(6)}, false);
                        rpt.PrintPageLine(new string[] { "tab(23)", check.Payee.Left(31)}, false);
                        rpt.PrintPageLine(new string[] { "tab(50)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                        ot += check.StatementAmount;
                    }
                }
            }
            rpt.PrintPageLine(new string[] { "tab(43)", "Total=>", "tab(50)", ot.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine();
            rpt.PrintPageLine();
            //adj = adj + CCur(txtBook(1).Text)
            if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
            {
                rpt.NewPage();
                heading();
            }
            rpt.FontBold = true;
            //rpt.PrintPageLine "tab(63)", "-----------"
            rpt.PrintPageLine(new string[] { "tab(30)", "  Statement Balance=>", "tab(63)", StatementAmount.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(30)", "+ Total Outstanding Deposits=>", "tab(63)", aT.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(30)", "- Total Outstanding Checks=>", "tab(63)", ot.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(63)", "-----------" });
            rpt.PrintPageLine(new string[] { "tab(30)", "= Adjusted Statement Balance=>", "tab(63)", (StatementAmount + aT - ot).ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(63)", "===========" });
            rpt.PrintPageLine(new string[] { "tab(30)", "  Book Balance=>", "tab(63)", BookBalance.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(30)", "+ Bank Errors In Your Favor=>", "tab(63)", ft.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(30)", "- Bank Errors In Bank's Favor=>", "tab(63)", gt.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(30)", "+ Other Adjustments =>", "tab(63)", adj.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine(new string[] { "tab(63)", "===========" });
            rpt.PrintPageLine(new string[] { "tab(38)", "Adjusted Book Balance=>", "tab(63)", Convert.ToDecimal(BookBalance + ft - gt + adj).ToString("#######0.00").PadLeft(11) });
            rpt.FontBold = false;
            rpt.NewPage();
            cleared = true;
            heading();
            rpt.PrintPageLine();
            rpt.FontBold = true;
            rpt.PrintPageLine(new string[] { "tab(9)", "Cleared Checks & Debits" });
            rpt.FontBold = false;
            if (rpt.PrintPageLine())
            {
                rpt.NewPage();
                heading();

            }
            foreach (BankReconciliation check in grdChecks.Items)
            {
                if (check.ReconFlag)
                {
                    if (check.Type != "DEPOSIT") {
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy") }, false);
                        rpt.PrintPageLine(new string[] { "tab(16)", check.Type.PadLeft(6)}, false);
                        rpt.PrintPageLine(new string[] { "tab(23)", check.Payee.Left(31)}, false);
                        rpt.PrintPageLine(new string[] { "tab(50)", check.BookAmount.ToString("#######0.00").PadLeft(11)}, false);
                        rpt.PrintPageLine(new string[] { "tab(63)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                        dt += check.StatementAmount;
                        if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
                        {
                            rpt.NewPage();
                            heading();
                        }
                    }
                }
            }
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] { "tab(50)", "Total=>", "tab(63)", dt.ToString("#######0.00").PadLeft(11) });
            rpt.PrintPageLine();

            rpt.FontBold = true;
            rpt.PrintPageLine(new string[] { "tab(9)", "Cleared Credits & Deposits" });
            rpt.FontBold = false;
            if (rpt.PrintPageLine())
            {
                rpt.NewPage();
                heading();
            }
            foreach (BankReconciliation check in grdChecks.Items)
            {
                if (check.ReconFlag)
                {
                    if (check.Type == "DEPOSIT") {
                        rpt.PrintPageLine(new string[] { "tab(6)", check.Date.ToString("MM/dd/yy") }, false);
                        rpt.PrintPageLine(new string[] { "tab(16)", (check.Type).PadLeft(6) }, false);
                        rpt.PrintPageLine(new string[] { "tab(23)", check.Payee.Left(31) }, false);
                        rpt.PrintPageLine(new string[] { "tab(50)", check.BookAmount.ToString("#######0.00").PadLeft(11) }, false);
                        rpt.PrintPageLine(new string[] { "tab(63)", check.StatementAmount.ToString("#######0.00").PadLeft(11) });
                        ct += check.StatementAmount;
                        if (rpt.CurrentHeight >= rpt.MaxPageHeight - 50)
                        {
                            rpt.NewPage();
                            heading();
                        }
                    }
                }
            }
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] { "tab(50)", "Total=>", "tab(63)", ct.ToString("#######0.00").PadLeft(11)});

			rpt.DisplayDocument();

		}
        //******************************************************************************
		public void heading()
		{
			//******************************************************************************

			rpt.FontBold = true;
			rpt.PrintPageLine(new string[] { "tab(28)", "CHECK RECONCILIATION REPORT                 page " + rpt.PageTag });
			rpt.FontBold = false;
			rpt.PrintPageLine("");
			rpt.PrintPageLine(new string[] { "tab(6)", "Company:", "tab(22)", CompanyName });
			rpt.PrintPageLine(new string[] { "tab(6)", "Department:", "tab(22)", DepartmentName });
			rpt.PrintPageLine(new string[] { "tab(6)", "Statement Date:", "tab(22)", StatementDate.ToShortDateString() });
			rpt.PrintPageLine();
			rpt.PrintPageLine();
			if (cleared) {
				rpt.PrintPageLine(new string[] { "tab(8)", "Date", "tab(16)", "Chk#", "tab(23)", "Payee/Description", "tab(55)", "Amount", "tab(65)", "Bank Stmt" });
				rpt.PrintPageLine(new string[] { "tab(6)", "-------- ------- -------------------------- ----------- ------------" });
			}

		}
    }
}
