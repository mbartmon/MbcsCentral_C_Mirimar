using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Printing;
using System.Windows.Documents;
using System.Xml.Serialization;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
    public class OperatingStatement
    {

        public string StatementName { get; set; }
        public int CompanyNumber { get; set; }
        public string CompanyName { get; set; }
        public int DepartmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public bool Consolidated { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Detail { get; set; }
        public int CurrentQtdYtd { get; set; }
        public bool PriorYear { get; set; }
        public bool Percentage { get; set; }
        public bool PrintYtd { get; set; }
        public bool PrintZero { get; set; }
        public int Target { get; set; }
		public bool CondensePrint { get; set; } = false;


        public bool BatchReport { get; set; } = false;

        private Report rpt = null;
        private PrintQueue printer;

        private enum CategoryType
        {
            Income = 1,
            Expense = 2
        }

        // 1) Prior year total is same as current year
        // 2) No totals for Difference column
        // 3) Print starts with page 2
        // 4) Heading: Include name of operating statement description
        // 5) List of operating statements
        private frmOperatingStatement frm;
        private short pg;
        private short ln;
        private int q;
        private Accounts tblacct;
        private AccountRanges[] tblrange;
        private Transactions[] tblTrans;
        private System.DateTime PriorStartDate;
        private System.DateTime PriorEndDate;
        private System.DateTime CurrentStartDate;
        private System.DateTime CurrentEndDate;
        private decimal TotalIncome;
        private decimal TotalExpense;
        private decimal pITotal;
        private decimal pETtotal;
        private decimal YTD_I_Total;
        private decimal YTD_E_Total;
        private decimal YTDIncome;
        private decimal YTDExpense;
        private decimal T_I_Amount;
        private decimal T_E_Amount;
        private decimal T_I_YTD_Amount;
        private decimal T_E_YTD_Amount;

        public FixedDocument process(int target)
        {
            Target = target;
            return process(null);
        }

        public FixedDocument process(PrintQueue printer)
        {
            string fontname = "Courier New";
			float fontsize;
			if (CondensePrint)

				fontsize = 8;
			else 
				fontsize = 12;
            this.printer = printer;
            decimal peamount;
            decimal eamount;
            string x = "";
            decimal piamount;
            decimal iamount;
            decimal petotal;

            int r = 0;
            int i = 0;
            bool first = false;
            int xacct = 0;
            string xacctname = null;
            ArrayList Ia = new ArrayList();
            ArrayList Ea = new ArrayList();

            rpt = new Report("Operating Statement", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };

            // Calculate current quarter
            q = ((Month - 1) / 3) + 1;

            // Build array for Income accounts
            tblrange = AccountRanges.load("opStmt:" + StatementName, (int)CategoryType.Income);
            if (tblrange == null || tblrange.Length == 0) {
				MessageBoxCustom.Show("You have not entered Income accounts for this report.", "Operating Statement", System.Windows.MessageBoxButton.OK);
				return null;
			}
			i = 0;
			foreach (AccountRanges range in tblrange)
            {
				if (range.AccountNumber > 0)
                {
					Ia.Add(range.AccountNumber);
				}
			}
			tblrange = null;

            // Build array for Expense accounts
            tblrange = AccountRanges.load("opStmt:" + StatementName, (int)CategoryType.Expense);
            if (tblrange == null || tblrange.Length == 0)
            {
                MessageBoxCustom.Show("You have not entered Expense accounts for this report.", "Operating Statement", System.Windows.MessageBoxButton.OK);
                return null;
            }
            i = 0;
            foreach (AccountRanges range in tblrange)
            {
                if (range.AccountNumber > 0)
                {
                    Ea.Add(range.AccountNumber);
                }
            }
            tblrange = null;

            if (Month < 12)
            {
                CurrentEndDate = new DateTime(Year, Month , DateTime.DaysInMonth(Year, Month));
                PriorEndDate = new DateTime(Year - 1, Month , DateTime.DaysInMonth(Year - 1, Month));
            }
            else
            {
                CurrentEndDate = new DateTime(Year, 12, 31);
                PriorEndDate = new DateTime(Year - 1, 12, 31);
            }
            switch (CurrentQtdYtd)
            {
                case 0:
                    // Current
                    CurrentStartDate = new DateTime(Year, Month, 1);
                    PriorStartDate = new DateTime(Year - 1, Month,1);
                    break;
                case 1:
                    // QTD
                    CurrentStartDate = WinglGlobal.CurrentQuarter(CompanyNumber, CurrentEndDate);
                    PriorStartDate = WinglGlobal.CurrentQuarter(CompanyNumber, PriorEndDate);
                    break;
                case 2:
                    // YTD
                    CurrentStartDate = new DateTime(CurrentEndDate.Year, 1, 1);
                    PriorStartDate = new DateTime(PriorEndDate.Year, 1, 1);
                    break;
            }

			TotalExpense = 0;
			petotal = 0;
			YTD_E_Total = 0;
			for (i = 0; i < Ea.Count; i++)
            {
				TotalExpense += Transactions.AccountBalance( CompanyNumber , (int)Ea[i], CurrentStartDate, CurrentEndDate, DepartmentNumber);
				petotal += Transactions.AccountBalance(CompanyNumber, (int)Ea[i], PriorStartDate, PriorEndDate, DepartmentNumber);
			}
			TotalIncome = 0;
			pITotal = 0;
			YTD_I_Total = 0;
			for (i = 0; i < Ia.Count; i++)
            {
                TotalIncome -= Transactions.AccountBalance( CompanyNumber,  (int)Ia[i],  CurrentStartDate,  CurrentEndDate,  DepartmentNumber);
				pITotal -= Transactions.AccountBalance( CompanyNumber,  (int)Ia[i],  PriorStartDate,  PriorEndDate,  DepartmentNumber);
			}

			pg = 0;
			heading();

			// Process Income
			first = true;
			rpt.PrintPageLine("");
			rpt.PrintPageLine(new string(' ',5) + "INCOME");
			rpt.PrintPageLine(new string(' ',5) + "------");
			rpt.PrintPageLine("");
			ln += 4;
			iamount = 0;
			YTDIncome = 0;
			piamount = 0;
			T_I_Amount = 0;
			T_I_YTD_Amount = 0;
			for (i = 0; i < Ia.Count; i++) {
				if (first) {
					xacct = (int)Ia[i];
					tblacct = Accounts.load((int)Ia[i]);
					xacctname = tblacct.Description.Left( 32);
                    tblacct = null;
					first = false;
				}
				if (xacct != (int)Ia[i])
                {
					if (PrintZero || iamount != 0 || (PriorYear && piamount != 0))
                    {
						x = new string(' ',5) + xacctname.PadRight(32) + iamount.ToString("#########0.00").PadLeft(13);
						if (Percentage)
                        {
							if (TotalIncome != 0) {
								x = x + " " + ((iamount / TotalIncome) * 100).ToString("##0.0").PadLeft(5);
							} else {
								x = x + "      ";
							}
						}
						if (PrintYtd)
                        {
							x = x + " " + YTDIncome.ToString("#########0.00").PadLeft(13);
						}
						if (PriorYear)
                        {
							x = x + piamount.ToString("#########0.00").PadLeft(13);
							if (Percentage)
                            {
								if (pITotal != 0) {
									x = x + ((piamount / pITotal) * 100).ToString(" ##0.0").PadLeft(6);
								} else {
									x = x + "       ";
								}
							}
							x = x + (iamount - piamount).ToString("#########0.00").PadLeft(13);
						}
						if (rpt.PrintPageLine(x))
                        {
                            rpt.NewPage();
                            heading();
                        }
					}
					xacct = (int)Ia[i];
					tblacct = Accounts.load((int)Ia[i]);
					xacctname = tblacct.Description.Left(32);
					T_I_Amount = T_I_Amount + iamount;
					T_I_YTD_Amount = T_I_YTD_Amount + YTDIncome;
					iamount = 0;
					YTDIncome = 0;
					piamount = 0;
				}

				iamount -= Transactions.AccountBalance(CompanyNumber, (int)Ia[i], CurrentStartDate, CurrentEndDate, DepartmentNumber);
				piamount -= Transactions.AccountBalance(CompanyNumber, (int)Ia[i], PriorStartDate, PriorEndDate, DepartmentNumber);
				YTDIncome -= Transactions.AccountBalance(CompanyNumber, (int)Ia[i], new DateTime(CurrentStartDate.Year, 1, 1), CurrentEndDate, DepartmentNumber);
			}
			if (PrintZero || iamount != 0 || (PriorYear && piamount != 0) || (PrintYtd && YTDIncome != 0))
            {
				x = new string(' ',5) + xacctname.PadRight(32) + iamount.ToString("#########0.00").PadLeft(13);
				if (Percentage)
                {
					if (TotalIncome != 0) {
						x = x + " " + ((iamount / TotalIncome) * 100).ToString("##0.0").PadLeft(5);
					} else {
						x = x + "      ";
					}
				}
				if (PrintYtd)
                {
					x = x + " " + YTDIncome.ToString("#########0.00").PadLeft(13);
				}
				if (PriorYear)
                {
					x = x + piamount.ToString("#########0.00").PadLeft(13);
					if (Percentage)
                    {
						if (pITotal != 0) {
							x = x + ((piamount / pITotal) * 100).ToString("##0.0").PadLeft(5);
						} else {
							x = x + "      ";
						}
					}
					x = x + (iamount - piamount).ToString("#########0.00").PadLeft(13);
				}
				if (rpt.PrintPageLine(x))
                {
                    rpt.NewPage();
                    heading();
                }
                ln += 1;
				T_I_Amount = T_I_Amount + iamount;
				T_I_YTD_Amount = T_I_YTD_Amount + YTDIncome;
			}

			rpt.PrintPageLine("");
			x = new string(' ',5) + "TOTAL INCOME".PadLeft(32) + T_I_Amount.ToString("#########0.00").PadLeft(13);
			if (Percentage) {
				x = x + " 100.0";
			}
			if (PrintYtd)
            {
				x = x + T_I_YTD_Amount.ToString("#########0.00").PadLeft(14);
			}
			if (PriorYear) {
				x = x + pITotal.ToString("#########0.00").PadLeft(13);
				if (Percentage)
                {
					x = x + new string(' ',5);
				}
				x = x + (T_I_Amount - pITotal).ToString("#########0.00").PadLeft(13);
			}
			rpt.PrintPageLine(x);
			ln += 2;

			// Process Expense
			first = true;
			rpt.PrintPageLine("");
			rpt.PrintPageLine(new string(' ',5) + "EXPENSE");
			rpt.PrintPageLine(new string(' ',5) + "-------");
			rpt.PrintPageLine("");
			ln += 1;
			eamount = 0;
			YTDExpense = 0;
			peamount = 0;
			T_E_Amount = 0;
			T_E_YTD_Amount = 0;
			for (i = 0; i < Ea.Count; i++)
            {
				if (first) {
					xacct = (int)Ea[i];
					tblacct = Accounts.load((int)Ea[i]);
					xacctname = tblacct.Description.Left( 32);
					first = false;
				}
				if (xacct != (int)Ea[i]) {
					if (PrintZero || eamount != 0 || (PriorYear && peamount != 0) || (PrintYtd && YTDExpense != 0))
                    {
						x = new string(' ',5) + xacctname.PadRight(32) + eamount.ToString("#########0.00").PadLeft(13);
						if (Percentage) {
							if (TotalExpense != 0) {
								x = x + " " + ((eamount / TotalExpense) * 100).ToString("##0.0").PadLeft(5);
							} else {
								x = x + "      ";
							}
						}
						if (PrintYtd) {
							x = x + " " + YTDExpense.ToString("#########0.00").PadLeft(13);
						}
						if (PriorYear) {
							x = x + peamount.ToString("#########0.00").PadLeft(13);
							if (Percentage) {
								if (petotal != 0) {
									x = x + ((peamount / petotal) * 100).ToString(" ##0.0").PadLeft(6);
								} else {
									x = x + "       ";
								}
							}
							x = x + (eamount - peamount).ToString("#########0.00").PadLeft(13);
						}
						if (rpt.PrintPageLine(x))
                            {
                                rpt.NewPage();
                                heading();
                            }
                        T_E_Amount = T_E_Amount + eamount;
						T_E_YTD_Amount = T_E_YTD_Amount + YTDExpense;
					}
					xacct = (int)Ea[i];
					tblacct = Accounts.load((int)Ea[i]);
					xacctname = tblacct.Description.Left( 32);
					eamount = 0;
					YTDExpense = 0;
					peamount = 0;
				}

				eamount += Transactions.AccountBalance( CompanyNumber,  (int)Ea[i],  CurrentStartDate,  CurrentEndDate,  DepartmentNumber);
				peamount += Transactions.AccountBalance( CompanyNumber,  (int)Ea[i],  PriorStartDate,  PriorEndDate,  DepartmentNumber);
				YTDExpense += Transactions.AccountBalance( CompanyNumber,  (int)Ea[i],  new DateTime(CurrentStartDate.Year, 1, 1),  CurrentEndDate,  DepartmentNumber);
			}
			if (PrintZero | eamount != 0 || (PriorYear & peamount != 0))
            {
				x = new string(' ',5) + xacctname.PadRight(32) + eamount.ToString("#########0.00").PadLeft(13);
				if (Percentage)
                {
					if (TotalExpense != 0) {
						x = x + " " + ((eamount / TotalExpense) * 100).ToString("##0.0").PadLeft( 5);
					} else {
						x = x + "      ";
					}
				}
				if (PrintYtd) {
					x = x + " " + YTDExpense.ToString("#########0.00").PadLeft(13);
				}
				if (PriorYear) {
					x = x + peamount.ToString("#########0.00").PadLeft(13);
					if (Percentage) {
						if (petotal != 0) {
							x = x + ((peamount / petotal) * 100).ToString(" ##0.0").PadLeft(6);
						} else {
							x = x + "       ";
						}
					}
					x = x + (eamount - peamount).ToString("#########0.00").PadLeft(13);
				}
				if (rpt.PrintPageLine(x))
                    {
                        rpt.NewPage();
                        heading();
                    }
                T_E_Amount = T_E_Amount + eamount;
				T_E_YTD_Amount = T_E_YTD_Amount + YTDExpense;
			}
			rpt.PrintPageLine("");
			x = new string(' ',5) + "TOTAL EXPENSE".PadLeft(32) + T_E_Amount.ToString("#########0.00").PadLeft(13);
			if (Percentage) {
				x = x + " 100.0";
			}
			if (PrintYtd) {
				x = x + T_E_YTD_Amount.ToString("#########0.00").PadLeft(14);
			}
			if (PriorYear) {
				x = x + petotal.ToString("#########0.00").PadLeft(13);
				if (Percentage) {
					x = x + new string(' ',6);
				}
				x = x + (T_E_Amount - petotal).ToString("#########0.00").PadLeft(13);
			}
			rpt.PrintPageLine(x);
			rpt.PrintPageLine("");
			x = new string(' ',5) + "PROFIT/LOSS".PadLeft(32) + (T_I_Amount - T_E_Amount).ToString("#########0.00").PadLeft(13);
			if (Percentage) {
				x = x + "      ";
			}
			if (PrintYtd) {
				x = x + (T_I_YTD_Amount - T_E_YTD_Amount).ToString("#########0.00").PadLeft(14);
			}
			if (PriorYear) {
				x = x + (pITotal - petotal).ToString("#########0.00").PadLeft(13);
				if (Percentage) {
					x = x + new string(' ',6);
				}
				x = x + ((T_I_Amount - T_E_Amount) - (pITotal - petotal)).ToString("#########0.00").PadLeft(13);
			}
			rpt.PrintPageLine(x);

            if (Target == 1 && printer != null)
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
		public void heading()
		{
			int tl;
			string x = "";

			pg += 1;
			rpt.PrintPageLine(new string(' ',5) + StatementName);
			rpt.PrintPageLine(new string(' ',5) + CompanyName);
			rpt.PrintPageLine(new string(' ',5) + DateTime.Now.ToShortDateString() + new string(' ',5) + "page " + pg);
			switch (CurrentQtdYtd)
            {
				case 0:
					// Current
					rpt.PrintPageLine(new string(' ',5) + "For month of " + new DateTime(Year, Month,  1).ToString("MMMM - yyyy"));
					break;
				case 1:
					// QTD
					rpt.PrintPageLine(new string(' ',5) + "For " + new DateTime(Year, (q - 1) * 3 + 1, 1).ToString("MMMM") + " to " + new DateTime(Year, Month, 1).ToString("MMMM") + " - " + Year);
					break;
				case 2:
					// YTD
					rpt.PrintPageLine(new string(' ',5) + "For January to " + new DateTime(Year, Month, 1).ToString( "MMMM") + " - " + Year);
					break;
			}
			ln = 4 + WinglGlobal.tmargin;
			if (DepartmentNumber != -1) {
				rpt.PrintPageLine(new string(' ',5) + DepartmentName);
				ln += 1;
			}
			rpt.PrintPageLine("");
			x = new string(' ',5) + "ACCOUNT" + new string(' ',25);
			tl = 32;
			switch (CurrentQtdYtd) {
				case 0:
					// Current
					x = x + "CURRENT MONTH  ";
					tl += 15;
					break;
				case 1:
					// QTD
					x = x + "   QTD AMOUNT  ";
					tl += 14;
					break;
				case 2:
					// YTD
					x = x + "   YTD AMOUNT  ";
					tl += 14;
					break;
			}
			if (Percentage) {
				x = x + "  %";
				tl += 7;
			}
			if (PrintYtd) {
				x = x + "   YEAR TO DATE";
				tl += 17;
			}
			if (PriorYear) {
				switch (CurrentQtdYtd) {
					case 0:
						// Current
						x = x + " PRIOR YR MNTH";
						tl += 14;
						break;
					case 1:
						// QTD
						x = x + "  PRIOR YR QTR";
						tl += 14;
						break;
					case 2:
						// YTD
						x = x + "  PRIOR YEAR";
						tl += 14;
						break;
				}
				if (Percentage) {
					x = x + "       %";
					tl += 8;
				}
				if (PriorYear) {
					x = x + "    DIFFERENCE";
					tl += 14;
				}
			}
			rpt.PrintPageLine(x);
			rpt.PrintPageLine(new string(' ',5) + new string('=', tl));
			ln += 2;

		}

        public void setDates(int month, int year)
        {
            Month = month;
            Year = year;
        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static OperatingStatement DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OperatingStatement));
            StringReader rdr = new StringReader(inputString);
            OperatingStatement parms = (OperatingStatement)serializer.Deserialize(rdr);
            return parms;
        }

    }
}

