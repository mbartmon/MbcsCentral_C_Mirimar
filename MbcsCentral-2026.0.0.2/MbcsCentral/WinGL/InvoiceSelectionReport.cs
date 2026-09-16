using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Printing;
using ExtensionMethods;
 // ERROR: Not supported in C#: OptionDeclaration
namespace MbcsCentral.WinGL
{
    public class InvoiceSelectionReport
    {
        static Report rpt;
        static PrintQueue printer = null;

        static short pg;
        static short ln;
        static Invoices[] tblInvoice;
        static Vendors tblVendor;
        static Accounts tblAccount;
        static Company tblCompany;
        static Department tblDepartment;
        static InvoiceDistributions[] tblInvoiceDist;
        static Memos Memo;
        static string memo2 = "";
        static string memo1 = "";
        static string xsql;
        static int xdpt;
        static int xcmp;
        static int xvend;
        static short xgrp;
        static int CheckNumber;
        static decimal disc;
        static decimal gross;
        static decimal alw;
        static decimal net;
        static decimal vDisc;
        static decimal vGross;
        static decimal vAlw;
        static decimal vNet;
        static decimal gDisc;
        static decimal gGross;
        static decimal gAlw;
        static decimal gNet;
        static decimal dDisc;
        static decimal dGross;
        static decimal dAlw;
        static decimal dNet;
        static decimal cDisc;
        static decimal cGross;
        static decimal cAlw;
        static decimal cNet;
        static decimal rDisc;
        static decimal rGross;
        static decimal rAlw;
        static decimal rNet;
        static string mvarMode;
        public static string Mode {
            get { return mvarMode; }
            set { mvarMode = value; }
        }
        public static bool EFT {get; set; } = false;
        public static bool ACH {get; set; } = false;


        static InvoiceSelectionReport()
        {

        }

        public static void print_EFT(bool eft = false, bool ach = false)
        {
            print_report(eft, ach, null);
        }



        public static void print_report(bool eft = false, bool ach = false, DateTime ?PayDate = null)
		{
            EFT = eft;
            ACH = ach;
            rpt = new Report("Invoice Selection Report", "Courier New", (float)11) {TopMargin = 5, BottomMargin = 30, LeftMargin = 0, BottomOffset=50 };

            xsql = Invoices.SQL_LOAD_FOR_SELECTION + " AND [" + Invoices.SQL_COL_CHECK_NUMBER + (EFT || ACH ? "] > 0 " : "]= 0 ");
            if (EFT)
            {
                xsql += "AND " + Invoices.SQL_COL_EFT + " = 1 ORDER BY Company, Department, VendorNumber, CheckGroup, InvoiceNumber";
            } 
            else if (ACH) 
            {
                xsql += "AND " + Invoices.TABLE + "." + Invoices.SQL_COL_ACH + " = 1 AND " + Invoices.SQL_COL_AMOUNT_PAID + " = 0 " + (PayDate != null ? (" AND " + Invoices.SQL_COL_PAY_DUE_DATE + " <= '" + ((DateTime)PayDate).Date.ToShortDateString()) + "' " : " ") + "ORDER BY Company, Department, VendorNumber, CheckGroup, InvoiceNumber";
            }
            else 
            {
				xsql += " AND PayFlag > 0 ORDER BY Company, Department, VendorNumber, CheckGroup, " + Invoices.SQL_COL_INVOICE_KEY;
			}
            try
            {
                tblInvoice = Invoices.LoadQuery(xsql, true);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Error printing InvoiceSelectionReport: " + ex.Message);
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Error printing InvoiceSelectionReport: " + ex.Message,false);
                return;
            }
            if (tblInvoice == null || tblInvoice.Length == 0)
            {
                MessageBox.Show("No invoices selected.");
                return;
            }
            if (MessageBoxCustom.Show("Print Invoice List?","",System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                xsql = Invoices.SQL_LOAD_ALL + " AND [" + Invoices.SQL_COL_CHECK_NUMBER + (EFT || ACH ? "] > 0 " : "]= 0 ");
                xsql += " AND PayFlag > 0 ORDER BY VendorName, " + WinGL.Department.SQL_COL_NAME + ", " + Invoices.SQL_COL_INVOICE_KEY;

                Invoices[] SummaryInvoices = Invoices.LoadQuery(xsql, true);
                InvoiceList rpt = new InvoiceList();
                rpt.process(SummaryInvoices);

            }
            xgrp = -1;
			report_header();
            DateTime dueDate;
			foreach (Invoices invoice in tblInvoice)
            {
                if (ACH && invoice.Amount - invoice.AmountPaid == 0)
                    continue;
                CheckNumber = invoice.CheckNumber;
				if (invoice.Company!= xcmp)
                {
					if (xcmp != 0) {
						group_totals();
						vendor_totals();
						department_totals();
						company_totals();
                        if (rpt.PrintPageLine("", false) || rpt.CurrentHeight > rpt.LineHeight * 50)
                        {
                            rpt.NewPage();
                            report_header();
                        }
                    }
                    company_header(invoice);
					department_header(invoice);
					vendor_header(invoice);
					group_header(invoice);
				}

				if (invoice.Department != xdpt)
                {
					if (xdpt != 0) {
						group_totals();
						vendor_totals();
						department_totals();
                        if (rpt.PrintPageLine("", false) || rpt.CurrentHeight > rpt.LineHeight * 50)
                        {
                            rpt.NewPage();
                            report_header();
                        }
                    }
                    department_header(invoice);
					vendor_header(invoice);
				}

				if (invoice.VendorNumber != xvend) {
					if (xvend != 0) {
						group_totals();
						vendor_totals();
                        if (rpt.PrintPageLine("", false) || rpt.CurrentHeight > rpt.LineHeight * 50)
                        {
                            rpt.NewPage();
                            report_header();
                        }
                    }
                    vendor_header(invoice);
				}

				if ((invoice.CheckGroup != xgrp && invoice.VendorNumber == xvend) || ACH)
                {
					if (xgrp != -1) {
						group_totals();
                        if (rpt.PrintPageLine("", false) || rpt.CurrentHeight > rpt.LineHeight * 50)
                        {
                            rpt.NewPage();
                            report_header();
                            vendor_header(invoice);
                        }
                    }
                    group_header(invoice);
				}
				Memo = Memos.load(invoice.MemoId);
                memo2 = "";
                memo1 = "";
                bool memoSplit = false;
                if (Memo != null)
                {
                    memo1 = Memo.Memo.Trim();

                    if (memo1.Length > WinglGlobal.MEMO_LINE_LENGTH)
                    {
                        for (int i = WinglGlobal.MEMO_LINE_LENGTH; i >= 1; i--)
                        {
                            if (memo1.Substring(i, 1).Equals(" ") || memo1.Substring(i, 1).Equals("\r\n") || memo1.Substring(i, 1).Equals("/"))
                            {
                                memo2 = memo1.Substring(i + 1).Replace("\r\n", "");
                                memo1 = memo1.Left(i);
                                memoSplit = true;
                                break;
                            }
                        }
                    }
                    if (!memoSplit)
                    {
                        if (memo1.Length > WinglGlobal.MEMO_LINE_LENGTH)
                        {
                            memo2 = memo1.Substring(WinglGlobal.MEMO_LINE_LENGTH + 1);
                            memo1 = memo1.Substring(0, WinglGlobal.MEMO_LINE_LENGTH);
                            memoSplit = true;
                        }
                    }
                }
                Memo = null;
				gross = invoice.Amount;
                alw = invoice.Allowance;
                disc = invoice.DiscountAmount;
                if (ACH)
                {
                    decimal amount;
                    amount = 0;
                    if (invoice.Discount != 0)
                    {
                        if (invoice.AmountToPay != 0)
                        {
                            amount = invoice.AmountToPay - invoice.Allowance;
                        }
                        else
                        {
                            amount = invoice.Amount - invoice.Allowance - invoice.AmountPaid;
                        }
                        if (invoice.DiscountDollar)
                            disc = invoice.Discount;
                        else
                            disc = System.Math.Round(amount * (invoice.Discount * (decimal)0.01), 2);
                    }
                    dueDate = invoice.CheckDate;
                }
                else
                    dueDate = invoice.PayDueDate;

                net = gross - disc;
                rpt.PrintPageLine(Strings.Space(13) + invoice.FullInvoiceNumber.Left(25, 26) + invoice.Date.ToString("MM/dd/yy ") + dueDate.ToString("MM/dd/yy").Left(9) + gross.ToString("$#,###,##0.00").LeftPad(16, 16) + alw.ToString("$###,###,##0.00").LeftPad(16, 16) + disc.ToString("$###,###,##0.00").LeftPad(16, 16) + net.ToString("$###,###,##0.00").LeftPad(16, 16), false);
                /*
				if (!memo1.Trim().Equals(""))
                {
                    rpt.PrintPageLine("");
					rpt.PrintPageLine(Strings.Space(15) + "MEMO: " + memo1.Trim(), false);
                    if (!memo2.Trim().Equals(""))
                    {
                        rpt.PrintPageLine();
                        rpt.PrintPageLine(Strings.Space(21) + memo2.Trim(), false);
                    }
				}
                */
                if (rpt.PrintPageLine(""))
                {
                    rpt.NewPage();
                    report_header();
                    vendor_header(invoice);
                }

                vGross = vGross + gross;
				vAlw = vAlw + alw;
				vDisc = vDisc + disc;
				vNet = vNet + net;
				gGross = gGross + gross;
				gAlw = gAlw + alw;
				gDisc = gDisc + disc;
				gNet = gNet + net;
				dGross = dGross + gross;
				dAlw = dAlw + alw;
				dDisc = dDisc + disc;
				dNet = dNet + net;
				cGross = cGross + gross;
				cAlw = cAlw + alw;
				cDisc = cDisc + disc;
				cNet = cNet + net;
				rGross = rGross + gross;
				rAlw = rAlw + alw;
				rDisc = rDisc + disc;
				rNet = rNet + net;
				get_allocations(invoice);
                if (rpt.PrintPageLine("") )
                {
                    rpt.NewPage();
                    report_header();
                    vendor_header(invoice);
                }
            }
            group_totals();
			vendor_totals();
			department_totals();
			company_totals();
			report_totals();
            if (printer != null)
                rpt.PrintDocument(printer);
            else
                rpt.DisplayDocument();

		}
		static void get_allocations(Invoices invoice)
		{
			object NewPage = null;

			string sAcct = null;
			string sCmp = null;
			string sDpt = null;
            string sJournal = "";
			string xsql = null;

            InvoiceDistributions[] Distributions = InvoiceDistributions.loadByInvoice(invoice.InvoiceKey, WinglGlobal.BASE_INVOICE_DISTRIBUTION_TABLE, true);

            if (Distributions != null &&  Distributions.Length > 0)
            {
				foreach (InvoiceDistributions tblInvoiceDist in Distributions)
                {
                    if (tblInvoiceDist.Amount == 0)
                        continue;
					sCmp = "* Not On File *";
					sDpt = sCmp;
					sAcct = sCmp;
					tblCompany = Company.loadByCompany(tblInvoiceDist.Company);
					if (tblCompany != null)
						sCmp = tblCompany.Name;
					tblDepartment = Department.loadByDepartment( (short)tblInvoiceDist.Department);
					if (tblDepartment != null)
						sDpt = tblDepartment.Name;
					tblAccount = Accounts.load(tblInvoiceDist.Account);
					if (tblAccount != null)
                    {
						sAcct =  tblAccount.Description.Trim().PadRight( 30);
					}
                    if (tblInvoiceDist.Journal == null)
                        sJournal = "";
                    else
                        sJournal = "(" + tblInvoiceDist.Journal + ")";

                    if (rpt.PrintPageLine(Strings.Space(16) + " Dpt: " + sDpt.Left(30, 30) + " Acct: " + (sAcct.Trim() + " " + sJournal).Left(44, 46) + (tblInvoiceDist.Amount - disc).ToString("$###,###,##0.00").LeftPad(15, 15)))
                    {
                        rpt.NewPage();
                        report_header();
                        vendor_header(invoice);
                    }
					disc = 0;
				}
				rpt.PrintPageLine("");
			}

		}
		static void report_header()
		{

			rpt.FontSize = 14;
			rpt.FontBold = true;
			if (EFT) 
            {
				rpt.PrintPageLine(Strings.Space(4) + "EFT PAYMENT REPORT");
			} 
            else if(ACH)
            {
                rpt.PrintPageLine(Strings.Space(4) + "ACH PAYMENT REPORT");
            }
            else 
            {
				rpt.PrintPageLine(Strings.Space(4) + "INVOICE PAYMENT SELECTION LIST");
			}
			rpt.FontSize = 11;
			rpt.FontBold = false;
			rpt.PrintPageLine(Strings.Space(5) + DateAndTime.Now + "   page " + rpt.PageNumber);
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
		}

		static void company_header(Invoices invoice)
		{
			string x = null;

            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
				rpt.NewPage();
				report_header();
			}
			xcmp = invoice.Company;
			tblCompany = Company.loadByCompany( xcmp);
			if (tblCompany != null) {
				x = tblCompany.Name;
			} else {
				x = "* NOT ON FILE *";
			}
			rpt.PrintPageLine(Strings.Space(5) + "COMPANY " + xcmp.ToString("000  ") + x);

		}
		static void department_header(Invoices invoice)
		{
			string x = null;

            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }
            xdpt = invoice.Department;
    		x = invoice.DepartmentName;
			rpt.PrintPageLine(Strings.Space(8) + "DEPARTMENT " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xdpt, "000  ") + x);

		}
		static void vendor_header(Invoices invoice)
		{
			string x = null;


            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }
            xvend = invoice.VendorNumber;
			tblVendor = Vendors.load(xvend);
			if (tblVendor != null) {
                if(tblVendor.AdditionalInfo!="")
				x = tblVendor.VendorName.Trim() + " " + tblVendor.Name2 + " {" + tblVendor.AdditionalInfo +"}"+ " [" + tblVendor.VendorNumber + "]";
                else
                    x = tblVendor.VendorName.Trim() + " " + tblVendor.Name2  + " [" + tblVendor.VendorNumber + "]";
            } else {
				x = "* NOT ON FILE *";
			}
			rpt.PrintPageLine(Strings.Space(11) + "VENDOR " + x);
			rpt.PrintPageLine(Strings.Space(13) + "InvoiceNumber               Date    Due             Gross       Allowance      DiscAmount             Net");
			rpt.PrintPageLine(Strings.Space(13) + "-------------               ----    ---             -----       ---------      ----------             ---");

		}
		static void vendor_totals()
		{

			rpt.PrintPageLine(Strings.Space(58) + "_______________ _______________ _______________ _______________");
			rpt.PrintPageLine(Strings.Space(11) + "Vendor Totals:".PadRight(44) + vGross.ToString("$###,###,##0.00").LeftPad(16,16) + vAlw.ToString("$###,###,##0.00").LeftPad(16,16) + vDisc.ToString("$###,###,##0.00").LeftPad(16,16) + vNet.ToString("$###,###,##0.00").LeftPad(16,16));
            rpt.PrintPageLine();
			vGross = 0;
			vAlw = 0;
			vDisc = 0;
			vNet = 0;
		}
		static void group_header(Invoices invoice)
		{

            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }
            xgrp = invoice.CheckGroup;
			rpt.PrintPageLine(Strings.Space(12) + "Check Group # " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xgrp, "#0"), false);
            if (EFT || ACH)
                rpt.PrintPageLine("  Check # " + CheckNumber.ToString());
            else
                rpt.PrintPageLine();
			rpt.PrintPageLine("");
		}

        static void group_totals()
		{
			object NewPage = null;

            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }

            if (!memo1.Trim().Equals(""))
            {
                rpt.PrintPageLine("");
                rpt.PrintPageLine(Strings.Space(15) + "MEMO: " + memo1.Trim());
                if (!memo2.Trim().Equals(""))
                {
                    rpt.PrintPageLine(Strings.Space(21) + memo2.Trim());
                }
                rpt.PrintPageLine();
            }
            memo1 = "";
            memo2 = "";

            rpt.PrintPageLine(Strings.Space(58) + "_______________ _______________ _______________ _______________");
			rpt.PrintPageLine(Strings.Space(11) + "Check Totals:".PadRight(44) + gGross.ToString("$###,###,##0.00").LeftPad(16,16) + gAlw.ToString("$###,###,##0.00").LeftPad(16,16) + gDisc.ToString("$###,###,##0.00").LeftPad(16,16) + gNet.ToString("$###,###,##0.00").LeftPad(16,16));
			rpt.PrintPageLine("");
			gGross = 0;
			gAlw = 0;
			gDisc = 0;
			gNet = 0;

		}
		static void department_totals()
		{
            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }

            rpt.PrintPageLine(Strings.Space(58) + "_______________ _______________ _______________ _______________");
			rpt.PrintPageLine(Strings.Space(11) + "Department Totals:".PadRight(44) + dGross.ToString("$###,###,##0.00").LeftPad(15,16) + dAlw.ToString("$###,###,##0.00").LeftPad(15,16) + dDisc.ToString("$###,###,##0.00").LeftPad(15,16) + dNet.ToString("$###,###,##0.00").LeftPad(15,16));
			rpt.PrintPageLine("");
			dGross = 0;
			dAlw = 0;
			dDisc = 0;
			dNet = 0;

		}
		static void company_totals()
		{
			object NewPage = null;

            if (rpt.CurrentHeight + rpt.BottomOffset >= rpt.MaxPageHeight ? true : false)
            {
                rpt.NewPage();
                report_header();
            }

            string name = "";
            Company cmp = Company.loadByCompany(xcmp);
            if (cmp != null)
                name = cmp.Name;
            else
                name = "* NOT ON FILE *";
            rpt.FontBold = true;
            rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(58) + "=============== =============== =============== ===============");
			rpt.PrintPageLine(Strings.Space(11) + (name + " TOTALS:").PadRight(44) + cGross.ToString("$###,###,##0.00").LeftPad(16,16) + cAlw.ToString("$###,###,##0.00").LeftPad(16,16) + cDisc.ToString("$###,###,##0.00").LeftPad(16,16) + cNet.ToString("$###,###,##0.00").LeftPad(16,16));
			rpt.PrintPageLine(new string('-', 125));
            rpt.FontBold = false;
			cGross = 0;
			cAlw = 0;
			cDisc = 0;
			cNet = 0;

		}
		static void report_totals()
		{
             if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                report_header();
                rpt.PrintPageLine("");
            }
			rpt.PrintPageLine(new String('=', 125));
			rpt.PrintPageLine(Strings.Space(11) + "Report Totals:".PadRight(44) + rGross.ToString("$###,###,##0.00").LeftPad(15,16) + rAlw.ToString("$###,###,##0.00").LeftPad(15,16) + rDisc.ToString("$###,###,##0.00").LeftPad(15,16) + rNet.ToString("$###,###,##0.00").LeftPad(15,16));
			rGross = 0;
			rAlw = 0;
			rDisc = 0;
			rNet = 0;
			xcmp = 0;
			xdpt = 0;
			xvend = 0;
			pg = 0;
			ln = 0;

		}
		static void InvoiceSelection(bool target, System.Printing.PrintQueue queue = null)
		{

            rpt = new Report("Invoice Payment List", "Courier New", 10) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100, PrintFlag = target };
            printer = queue;
			xcmp = 0;
			xdpt = 0;
			xvend = 0;
			print_report();

		}
	}
}
