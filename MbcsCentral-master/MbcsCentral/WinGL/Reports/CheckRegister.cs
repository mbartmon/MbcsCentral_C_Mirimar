using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Printing;
using System.Xml.Serialization;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
	public class CheckRegister
	{
        // private local variables
        PrintQueue printer;
        private frmReportList rpt;
		private short pg;
		private short ln;
		private string cname;
		private string dname;
		private int LastSeq;
		private Vendors tblVendors;
		private ApChecks[] checks;
		private Accounts tblAccounts;
		private Company tblCompany;
		private Department tblDepartment;
		private decimal ctotal;
		private decimal rtotal;
		private string outstanding;
        private string cmpDept = "";
        private string l1 = "";
        private string l2 = "";

        private int mvarCompany;
		private int mvarDepartment;
		private int mvarStartCheck;
		private int mvarEndCheck;
		private System.DateTime mvarStartDate;
		private System.DateTime mvarEndDate;
		private bool mvarCancel;
		private string mvarCompanyName;
		private string mvarDepartmentName;
		private struct invNum_Type
		{
			public string iNumber;
			public decimal amount;
			public bool used;
		}
		private invNum_Type[] invNums;
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
		public System.DateTime EndDate {
			get { return mvarEndDate; }
			set { mvarEndDate = value; }
		}
		public System.DateTime StartDate {
			get { return mvarStartDate; }
			set { mvarStartDate = value; }
		}
		public int EndCheck {
			get { return mvarEndCheck; }
			set { mvarEndCheck = value; }
		}
		public int StartCheck {
			get { return mvarStartCheck; }
			set { mvarStartCheck = value; }
		}
		public int DepartmentNumber {
			get { return mvarDepartment; }
			set { mvarDepartment = value; }
		}
		public int CompanyNumber {
			get { return mvarCompany; }
			set { mvarCompany = value; }
		}
        public int Target { get; set; } = 0;
        public string Printer { get; set; }

		public enum Targets
        {
			Printer = 1,
			Display = 2,
			Email = 3 
        }

        public FixedDocument print_report(int target)
        {
            Target = target;
            return print_report(null);
        }

		public FixedDocument print_report(PrintQueue queue = null)
		{
			string xsql = "";
            printer = queue;
			InvoiceHistory[] tblHist = null;
			string invNum = null;
			short inum = 0;
			short i = 0;
			int lastCheck = 0;
			short lastVend = 0;
			ApChecks[] tblLookup = null;

			pg = 0;
			checks = ApChecks.loadForRegister(CompanyNumber, DepartmentNumber, StartCheck, EndCheck == 0 ? 999999 : EndCheck, StartDate, EndDate);
			if (checks != null && checks.Length > 0) {
                rpt = new frmReportList("Courier New", 12, "Check Register") {TOP_MARGIN = 100, BOTTOM_MARGIN = 100, CR = this};
                rpt.title = "Check Register";
                rpt.EditClass = "MbcsCentral.WinGL.frmCheckRegisterEdit";
                rpt.IndexStart = 0;
                rpt.IndexLength = 7;
                rpt.PageSize = 72;
                rpt.FontSize = 9;
                rpt.FontBold = true;
                
                rpt.TOP_MARGIN = 8;
                rpt.LEFT_MARGIN = 8;
                tblCompany = Company.loadByCompany(CompanyNumber);
                if (tblCompany != null) {
					cname = tblCompany.Name;
				} else {
					cname = "Company #" + CompanyNumber + " Not on File";
				}
                tblCompany = null;
                if (DepartmentNumber > 0)
                {
                    tblDepartment = Department.loadByDepartment((short)DepartmentNumber);
                    if (tblDepartment != null)
                    {
                        dname = tblDepartment.Name;
                    }
                    else
                    {
                        dname = "Department #" + DepartmentNumber + " Not on File";
                    }
                }
                else
                    dname = "";
				heading();
                string vendorName = "";
				foreach (ApChecks tblChecks in checks) {
					if (tblChecks.Sequence == 0 || (lastCheck != tblChecks.CheckNumber)) {
						CheckTotal();
						if (tblChecks.Sequence != 0) {
							xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE Company = " + tblChecks.Company + " AND Department = " + tblChecks.Department;
							xsql += " AND CheckNumber = " + tblChecks.CheckNumber + " AND Sequence = 0";
							tblLookup = ApChecks.LoadQuery(xsql);
							if (tblLookup !=null && tblLookup.Length > 0) {
								lastVend = (short)tblLookup[0].VendorAccount;
							} else {
								lastVend = 0;
							}
						} else {
							lastVend = (short)tblChecks.VendorAccount;
						}
                        tblHist = InvoiceHistory.LoadByCheck(tblChecks.CheckNumber, lastVend, tblChecks.Company, tblChecks.Department);
                        if (invNums != null)
                            invNums.Initialize();
						if (tblHist != null) {
							invNums = new invNum_Type[tblHist.Length + 1];
							inum = 0;
							foreach (InvoiceHistory hist in tblHist) {
                                invNums[inum].iNumber = hist.FullInvoiceNumber;
								invNums[inum].amount = hist.AmountPaid;
								invNums[inum].used = false;
								inum++;
							}
                            tblHist = null;
						} else {
                            invNums = new invNum_Type[1];
                            invNums[0].iNumber = "* Not Found *";
						}

                        vendorName = CheckVendor(lastVend).Trim().Left(32);
                        l1 = tblChecks.ID.ToString("#####0").PadRight(9) + tblChecks.CheckNumber.ToString("000000 ") + tblChecks.Date.ToString("MM/dd/yyyy") + " " + vendorName.PadRight(32)  + " ";
						outstanding = (tblChecks.ReconFlag ? "*" : " ");
						cmpDept = "{" + CompanyNumber.ToString("000") + "/" + DepartmentNumber.ToString("000") + "}";
						if (tblChecks.Sequence != 0) {
                            print_detail(tblChecks);
						}
						lastCheck = tblChecks.CheckNumber;
					} else {
                        print_detail(tblChecks);
					}
				}
				CheckTotal();
				rpt.Add("");
				rpt.Add(Strings.Space(81) + "REPORT TOTAL=>" + rtotal.ToString("$###,###,##0.00").PadLeft(15));
                if (Target == 1 && printer != null)
                {
                    rpt.FontSize = 10;
                    rpt.FontBold = true;
                    rpt.TOP_MARGIN = 0;
                    rpt.LEFT_MARGIN = 0;
                    rpt.printer = printer;
                    rpt.Print();
                    return null;
                }
                else if (Target == 3)
                {
                    return rpt.PrepareDocument();
                }
                else
				    rpt.ShowDialog();
                if (rpt.Reprint)
                {
                    CheckRegister rpt = new CheckRegister();
                    rpt.StartDate = this.StartDate;
                    rpt.EndDate = this.EndDate;
                    rpt.StartCheck = this.StartCheck;
                    rpt.EndCheck = this.EndCheck;
                    rpt.CompanyNumber = this.CompanyNumber;
                    rpt.DepartmentNumber = this.DepartmentNumber;
                    rpt.print_report();
                }
                rtotal = 0;
			}
            return null;
		}

        private void print_detail( ApChecks tblChecks)
        {
            int l1Padding = 61;
            if (ln > rpt.PageSize )
            {
                rpt.NewPage();
                heading();
            }
            for (int i = 0; i <= Information.UBound(invNums, 1); i++)
            {
                if (!invNums[i].used & (Information.UBound(invNums, 1) == 1 | invNums[i].amount == tblChecks.Amount))
                {
                    l2 = " " + invNums[i].iNumber;
                    invNums[i].used = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
                else
                {
                    l2 = " ";
                }
            }
            rpt.Add(l1.PadRight(l1Padding) + tblChecks.DistDepartment.ToString("000") + " " + CheckAcct(tblChecks).PadRight(30) + tblChecks.Amount.ToString("$###,###,##0.00").PadLeft(15) + " " + outstanding + l2); // + Strings.Space(50) + cmpDept);
            ctotal = ctotal + tblChecks.Amount;
            rtotal = rtotal + tblChecks.Amount;
            l1 = Strings.Space(l1Padding);
            outstanding = " ";
            cmpDept = " ";
            LastSeq = tblChecks.Sequence;
            ln++;
            return;

        }
        public string CheckVendor(short vend)
		{
			tblVendors = Vendors.load(vend);
			if (tblVendors != null) {
				return tblVendors.VendorName.Left(46);
			} else {
				return "* Not On File *";
			}

		}
		public string CheckAcct(ApChecks check)
		{
			tblAccounts = Accounts.load(check.VendorAccount);
			if (tblAccounts != null) {
				return tblAccounts.Description.PadRight(30);
			} else {
				return "* Not On File *";
			}
		}
		public void CheckTotal()
		{

			if (LastSeq > 1) {
				rpt.Add(Strings.Space(95) + "===============");
				rpt.Add(Strings.Space(95) + ctotal.ToString("$###,###,##0.00").PadLeft(15));
				ln++;
                LastSeq = 0;
			}
			rpt.Add("");
            ln++;
            ctotal = 0;

		}
		public void heading()
		{

            pg++;
            rpt.Add("");
			rpt.Add(Strings.Space(7) + "ACCOUNTS PAYABLE CHECK REGISTER" + "     " + DateTime.Now + "     page " + pg);
			rpt.Add(Strings.Space(7) + cname + "   (" + CompanyNumber.ToString("00") + ")");
			rpt.Add(Strings.Space(7) + dname);
			rpt.Add(Strings.Space(7) + StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString());
			rpt.Add(Strings.Space(5) + new String('=', 130));
			rpt.Add(Strings.Space(5) + new String(' ', 4) + "CHECK#   DATE    VENDOR NAME                        DPT ACCOUNT DESCRIPTION                    AMOUNT * INVOICE NUMBER");
			rpt.Add(Strings.Space(5) + new String('=', 130));
			ln = 8 + WinglGlobal.tmargin;

		}

        /*
		public void Serialize()
		{

			WinglGlobal.BatchRptSave[0] = "CR";
			WinglGlobal.BatchRptSave[1] = CompanyNumber;
			WinglGlobal.BatchRptSave[2] = DepartmentNumber;
			WinglGlobal.BatchRptSave[3] = StartCheck;
			WinglGlobal.BatchRptSave[4] = EndCheck;
			WinglGlobal.BatchRptSave[5] = StartDate;
			WinglGlobal.BatchRptSave[6] = EndDate;
			WinglGlobal.BatchRptSave[7] = CompanyName;
			WinglGlobal.BatchRptSave[8] = DepartmentName;

		}
		public void Unserialize()
		{

			CompanyNumber = (int)WinglGlobal.BatchRptSave[1];
			DepartmentNumber = (int)WinglGlobal.BatchRptSave[2];
			StartCheck = (int)WinglGlobal.BatchRptSave[3];
			EndCheck = (int)WinglGlobal.BatchRptSave[4];
			StartDate = (DateTime)WinglGlobal.BatchRptSave[5];
			EndDate = (DateTime)WinglGlobal.BatchRptSave[6];
			CompanyName = (string)WinglGlobal.BatchRptSave[7];
			DepartmentName = (string)WinglGlobal.BatchRptSave[8];

		}
        */

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

        public static CheckRegister DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CheckRegister));
            StringReader rdr = new StringReader(inputString);
            CheckRegister parms = (CheckRegister)serializer.Deserialize(rdr);
            return parms;
        }

    }
}
