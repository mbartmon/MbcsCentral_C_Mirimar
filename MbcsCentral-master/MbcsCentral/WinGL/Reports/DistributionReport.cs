using ExtensionMethods;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Printing;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Xml.Serialization;

// ERROR: Not supported in C#: OptionDeclaration
namespace MbcsCentral.WinGL
{
    public class DisbursementReport
	{
        // private local variables
        private Report rpt = null;
		private short pg;
		private short ln;
		private decimal ytotal;
		private decimal ctotal;
		private decimal gtotal;
		private decimal nb;
		private decimal ob;
		private decimal cb;
		private int xacct;
		private string cdesc;
		private bool posted;
        private string fontname = "Courier New";
        private float fontsize = 8;

        //local variable(s) to hold property value(s)
        private int mvarCompany;
		private string mvarCompanyName;
		private int mvarDepartment;
		private string mvarDepartmentName;
		private int mvarMonth;
		private int mvarYear;
		private int mvarBalanceAccount;
		private int mvarCollectionAccount1;
		private int mvarCollectionAccount2;
		private bool mvarCancel;
		public int CollectionAccount2 {
			get { return mvarCollectionAccount2; }
			set { mvarCollectionAccount2 = value; }
		}
		public int CollectionAccount1 {
			get { return mvarCollectionAccount1; }
			set { mvarCollectionAccount1 = value; }
		}
		public int BalanceAccount {
			get { return mvarBalanceAccount; }
			set { mvarBalanceAccount = value; }
		}
		public int rptYear {
			get { return mvarYear; }
			set { mvarYear = value; }
		}
		public int rptMonth {
			get { return mvarMonth; }
			set { mvarMonth = value; }
		}
		public string DepartmentName {
			get { return mvarDepartmentName; }
			set { mvarDepartmentName = value; }
		}
		public int Department {
			get { return mvarDepartment; }
			set { mvarDepartment = value; }
		}
		public string CompanyName {
			get { return mvarCompanyName; }
			set { mvarCompanyName = value; }
		}
		public int Company {
			get { return mvarCompany; }
			set { mvarCompany = value; }
		}

        public string[] Journals { get; set; }
        public bool BatchReport { get; set; } = false;
        public int Target { get; set; }

        public FixedDocument print_report(int target)
        {
            Target = target;
            return print_report(null);
        }

		public FixedDocument print_report(PrintQueue printer = null)
		{
			object screenwidth = null;
			string payto = "";
			string xsql = "";
            int i;
			int yr = 0;
			string stdate = null;
			string endate = null;
			bool first = false;
			Accounts tblacct = null;
			Transactions[] tbltrans = null;
			Balances tblbal = null;
			string amt = null;

            yr = rptYear;
			stdate = "01/01/" + yr.ToString("####");
			endate = rptMonth.ToString("00") + "/" + DateTime.DaysInMonth(yr, rptMonth).ToString("##") + "/" + yr.ToString("####");

			tblbal = Balances.load(Company, Department, BalanceAccount);
			ob = 0;
			if (tblbal != null) {
				if (rptMonth > 1) {
					ob = (decimal)tblbal["C_Month" + (rptMonth - 1).ToString("00")];
				} else {
					ob = tblbal.P_Month12;
				}
                tblbal = null;
			}
			cb = 0;
            tblbal = Balances.load(Company, Department, CollectionAccount1);
			if (tblbal != null)
            {
				cb = -(decimal)tblbal["C_Month" + rptMonth.ToString("00")];
                tblbal = null;
			}
			tblbal = Balances.load(Company, Department, CollectionAccount2);
			if (tblbal != null)
            {
                cb += cb - (decimal)tblbal["C_Month" + rptMonth.ToString("00")];
				tblbal = null;
			}

			xsql = "SELECT gl_Transactions.*, gl_Accounts.Description AS AccountName FROM gl_Transactions, gl_Accounts WHERE CompanyNumber = " + Company + " AND DepartmentNumber = " + Department;
			xsql = xsql + " AND Date BETWEEN '" + stdate + "' AND '" + endate + "' AND (";

			first = true;
			for (int j = 0; j <= 9; j++) {
			    if (Strings.Trim(Journals[j]) != "") {
					if (!first) {
				    	xsql = xsql + " OR ";
					}
					xsql = xsql + "Journal LIKE '%" + Strings.Trim(Journals[j]) + "'";
					first = false;
				}
			}
			xsql = xsql + ") AND gl_Accounts.[Key] = AccountNumber ORDER BY gl_Accounts.Description, CheckNumber";

			first = true;
			ctotal = 0;
			ytotal = 0;
			gtotal = 0;
			tbltrans = Transactions.load(xsql, true);
			if (tbltrans != null && tbltrans.Length > 0)
            {
                rpt = new Report("Balance Sheet", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
                pg = 0;
				heading();
				foreach (Transactions tbltran in tbltrans)
                {
					tblacct = Accounts.load(tbltran.AccountNumber);
					if (tblacct != null && tblacct.MajorCategory == "005_")
                    {
						if (!first & xacct != tbltran.AccountNumber)
                        {
							if (ctotal != 0 | ytotal != 0) {
								CategoryTotal();
							}
							xacct = tbltran.AccountNumber;
						}
						if (first) {
							xacct = tbltran.AccountNumber;
							first = false;
						}
						cdesc = tbltran.Description;
                        ytotal += tbltran.Amount;

						if (tbltran.Date.Month == rptMonth && tbltran.Amount != 0)
                        {
							payto = tbltran.Description.Trim();
						    i = payto.IndexOf(", Invoice");
							if (i > 0)
                            {
							    payto = Strings.Left(payto, i - 1);
							}
							payto = payto.Left(15, 15);
							xsql = Strings.Space(5) + tbltran.Date.ToString("MM/dd/yyyy").PadRight(11) + payto;
							xsql = xsql + tbltran.CheckNumber.ToString("######").PadLeft(6) + "  ";
							xsql = xsql + tbltran.AccountName.PadRight(30) + "  ";
							xsql = xsql + tbltran.Amount.ToString("########0.00").PadLeft(13);
							ctotal += tbltran.Amount;
							gtotal += tbltran.Amount;

							rpt.PrintPageLine(xsql);
						}
					}
				}
				if (ctotal != 0)
					CategoryTotal();

				nb = ob - gtotal + cb;
				rpt.PrintPageLine();
				rpt.PrintPageLine(Strings.Space(20) + "  old balance       collections     disbursements     new balance");
				rpt.PrintPageLine(Strings.Space(20) + ob.ToString("########0.00").PadLeft(13) + "  +  " + cb.ToString("#########0.00").PadLeft(13) + "  -  " + gtotal.ToString("#########0.00").PadLeft(13) + " = " + nb.ToString("#########0.00").PadLeft(13));
                if (Target == 1 && printer != null)
                    rpt.PrintDocument(printer);
                else if (Target == 3)
                {
                    rpt.NewPage(true);
                    return rpt.Document;
                }
                else
                    rpt.DisplayDocument();

                if (!BatchReport && !posted) {
					amt = Strings.Trim(Interaction.InputBox("The amount to post the Balance Account for the current month: ", "Disbursement Report", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(nb, "#########0.00")));
					if (amt != null && amt.Length > 0)
                    {
						tblbal = Balances.load(Company, Department, BalanceAccount);
						if (tblbal != null) {
							tblbal["C_Month" + rptMonth.ToString("00")] = decimal.Parse(amt);
							tblbal.save();
                            tblbal = null;
						} else {
                            tblbal = new Balances();
							tblbal.CompanyNumber = (short)Company;
							tblbal.DepartmentNumber = (short)Department;
							tblbal.AccountNumber = (short)BalanceAccount;
							tblbal["C_Month" + rptMonth.ToString("00")] = decimal.Parse(amt);
							tblbal.save();
                            tblbal = null; ;
						}
						posted = true;
					}
				}
			}
            return null;
		}
		private void heading()
		{

			rpt.PrintPageLine(Strings.Space(5) + "DISBURSEMENT REPORT" + Strings.Space(10) + "page " + pg);
			rpt.PrintPageLine();
			rpt.PrintPageLine(Strings.Space(5) + "month of    premises                                            prepared for");
			rpt.PrintPageLine(Strings.Space(5) + Conversion.Str(rptMonth) + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Year(DateAndTime.Now) - rptYear, "####") + "     " + DepartmentName.PadRight(50) + "  " + CompanyName);
			rpt.PrintPageLine();
			rpt.PrintPageLine(Strings.Space(5) + "  date     payable to     check#     explanation                    item-amount category-total annual-to-date");
			rpt.PrintPageLine(Strings.Space(5) + "---------------------------------------------------------------------------------------------------------------------");
			ln = 7 + WinglGlobal.tmargin;

		}
		private void CategoryTotal()
		{

			if (ctotal != 0) {
				rpt.PrintPageLine(Strings.Space(42) + cdesc.Left( 40, 40) + "  " +ctotal.ToString("#########0.00").PadLeft(13) + "  " + ytotal.ToString("#########0.00").PadLeft(13));
			}
			ctotal = 0;
			ytotal = 0;

		}

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static DisbursementReport DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DisbursementReport));
            StringReader rdr = new StringReader(inputString);
            DisbursementReport parms = (DisbursementReport)serializer.Deserialize(rdr);
            return parms;
        }

        public void setDates(int newMonth, int newYear)
		{

			rptMonth = newMonth;
			rptYear = newYear;

		}
	}
}
