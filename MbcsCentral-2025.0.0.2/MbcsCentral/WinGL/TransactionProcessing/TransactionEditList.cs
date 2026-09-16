using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using System.Windows.Documents;

namespace MbcsCentral.WinGL
{
    public class TransactionEditList
    {
        Transactiontemp[] temps;
		private short pg;
		private short ln;
		Accounts tblacct;
		private frmReportList rpt;


		public enum Targets
		{
			Printer = 1,
			Display = 2,
			Email = 3
		}
		public int Target { get; set; } = 0;

        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public TransactionEditList(string parm)
        {
            m_ThisApp = parm;
            print_report();
        }

        public TransactionEditList()
        {
            print_report();
        }
		public FixedDocument print_report(int target = 2)
		{
			Target = target;

			string a = null;
			string desc = null;
			string desc2 = null;
			string s = null;
			decimal total = 0;
			short cnt = 0;
			Control[] tblctls = null;
            Control tblctl = null;
			string xsql = null;

			tblctls = Control.load();
            if (tblctls == null)
                return null;
            tblctl = tblctls[0];

            xsql = "SELECT * FROM " + Transactiontemp.TABLE;
			if (tblctl.EdListSort1_fld != null)
            {
				xsql = xsql + " ORDER BY ";
                string st = "";
				for (int i = 1; i <= 6; i++)
                {
                    st = "EdListSort" + i.ToString("#") + "_fld";
                    if (tblctl[st] != null && !((string)tblctl[st]).Trim().Equals(""))
                    {
						if (i > 1) {
							xsql = xsql + ", ";
						}
                        st = "EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_fld";
                        xsql = xsql + tblctl[st] + " ";
                        st = "EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_dir";
                        xsql += tblctl[st];
					}
				}
			}
			temps = Transactiontemp.load(xsql);

			if (temps == null || temps.Length == 0) {
				MessageBox.Show("No Transactions to post.",  "Transaction Edit List");
				rpt = null;
			} else {
                rpt = new frmReportList("Courier New", 12) { TE=this};
                rpt.FontSize = 11;
                rpt.TOP_MARGIN = 10;
                rpt.LEFT_MARGIN = 10;
                rpt.title = "Transaction Edit List";
                rpt.EditClass = "MbcsCentral.WinGL.frmTransactionEdit";
                rpt.IndexStart = 0;
                rpt.IndexLength = 7;
                rpt.PageSize = 75;
                
				pg = 0;
				heading();
				foreach (Transactiontemp tblTransTemp in temps) {
					if (ln >= rpt.PageSize - 12) {
						rpt.NewPage();
						heading();
					}
					tblacct = Accounts.load(tblTransTemp.AccountNumber);
					a = "";
					if (tblacct != null) {
						a = tblacct.Description.TrimEnd() ;
					} else {
						a = "*NoAcct*";
					}
					desc2 = "";
					desc = tblTransTemp.Description.Trim();
					if (desc.Length > 30) {
						for (int i = 31; i >= 1; i += -1) {
							if (desc.Length > 31 && desc.Substring( i, 1) == " " )
                            {
								desc2 = desc.Substring( i + 1);
								desc = desc.Left( i);
								break; 
							}
						}
					}
    				s = tblTransTemp.IndexNumber.ToString( "####### ").PadLeft(8) + tblTransTemp.DepartmentNumber.ToString("000  ");
					s += tblTransTemp.Date.ToString("MM/dd/yy ") + tblTransTemp.CheckNumber.ToString("###### ").PadLeft(7);
					s += new string(' ', 23) + desc.PadRight(31);
					s = s + tblTransTemp.Amount.ToString("$###,###,##0.00").PadLeft(18) + " " + tblTransTemp.Journal.PadRight(20);

					rpt.Add(s);
					rpt.Add(Microsoft.VisualBasic.Strings.Space(8) + a.PadRight(44) + desc2.PadRight(50) + tblTransTemp.Source.PadRight(20));
					ln += 2;
					total = total + tblTransTemp.Amount;
					cnt += 1;
				}
				rpt.Add(Microsoft.VisualBasic.Strings.Space(86) + "===============");
				rpt.Add(Microsoft.VisualBasic.Strings.Space(10) + "Transactions = " + cnt.ToString("#,###").PadLeft(5) + Microsoft.VisualBasic.Strings.Space(49) + "TOTAL= " + total.ToString("$###,###,##0.00").PadLeft(15));

				if (Target == 3)
				{
					return rpt.PrepareDocument();
				}
				else
                    rpt.ShowDialog();

                if (rpt.Reprint)
                {
                    TransactionEditList rpt = new TransactionEditList();

                    return(null);
                }
                if (MessageBoxCustom.Show("Do you want to post these transactions to the G/L?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    WinglGlobal.PostTransactions();
			}
			return null;
		}
		public void heading()
		{
			rpt.Add("TRANSACTION EDIT LIST");
			rpt.Add(DateTime.Now + " page " + Report.PAGE_TAG);
			rpt.Add();
				rpt.Add(" Index# Dept   Date   Check#                        Description                                Amount Journal/Source");
			rpt.Add();
			ln = 5;

		}
  }
}
