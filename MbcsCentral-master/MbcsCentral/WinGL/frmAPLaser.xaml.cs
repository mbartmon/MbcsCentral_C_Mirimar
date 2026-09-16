using ExtensionMethods;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using MbcsCentral;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Markup;
using System.Linq.Expressions;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmAPLaser.xaml
    /// </summary>
    public partial class frmAPLaser : Window
    {
        public DateTime CheckDate { get; set; }
        public DateTime PayDueDate { get; set; }
        public short Iflag { get; set; }
        public System.Printing.PrintQueue printer { get; set; }
        public int payroll { get; set; }
        public bool ACH { get; set; } = false;
        public DataView dataView { get; set; }
        public string FilePath { get; set; }

        #region NACHA FILE VARIABLES
        // NACHA File Variables
        string baseFilePath = "";
        string filepath = "";
        string NachaFileName = "";
        NachaDetail nachaDetail;
        NACHA_Layout NachaControlBatchNumber;
        NachaBatchVendorHeader batchVendorHeader;
        NachaBatchControl nachaBatchControl;
        string NachaHeaderFedId = "";
        string NachaHeaderCompanyName = "";
        string NachaOffsettingTransactionCode = "";
        string NachaCheckingCredit = "";
        string NachaSavingDebit = "";
        string NachaTracenumber = "";
        NACHA_Layout[] NachaDetailRecord = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.DetailRecord);
        NACHA_Layout[] NachaAddendaRecord = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.AddendaRecord);
        string NachaDFI;
        int NachaSeq = 1;
        int LastPaymentDay = 0;
        int nachaBatchCount = 0;
        int nachaBatchCountTotal = 0;
        int nachaTotalBatches = 0;
        int nachaBatchHash = 0;
        int nachaFileHash = 0;
        double nachaRecordCount = 0;
        decimal[] nachaByDate = new decimal[31];
        DataTable nachaByBldDate = null;
        // Batch Control
        decimal nachaTotalDebitAmount = 0;
        decimal nachaTotalCreditAmount = 0;
        //File Control
        decimal nachaFileDebitAmount = 0;
        decimal nachaFileCreditAmount = 0;
        ArrayList NachaProcessed = new ArrayList();
        struct ProcessedInvoices
        {
            public string CompanyName;
            public string VendorName;
            public string PaymentDate;
            public string InvoiceNumber;
            public decimal Amount;
        }
        DataTable dt = new DataTable();
        DataRow dataRow;
        List<string> NachaBad = new List<string>();
        StreamWriter sw = null;
        StreamWriter swPR = null;


        NACHA_Layout[] NachaControl = null;

        public bool Nacha { get; set; } = false;
        public string NachaFileDir { get; set; }
        public string NachaFilePrefix { get; set; }
        public string NachaFileOptional { get; set; }
        public char NachaFileTag { get; set; }
        #endregion
        public ArrayList NachaResults { get { return NachaProcessed; } }




        DirectPrint rpt;
        System.Drawing.Brush br;
        Layout Checks3;
        const int CHECKS3_VOUCHER_COUNT = 10;
        const int TOTAL_GROSS = 1;
        const int TOTAL_ALLOW = 2;
        const int TOTAL_DISC = 3;
        const int TOTAL_NET = 4;
        bool first = false;
        bool printedYet = false;

        Invoices[] invoices;
        Invoices[] tblCount;
        Invoices tblinvoices;
        InvoiceDistributions[] InvDist;
        Department dept;
        Company company;
        Vendors tblVendors;
        Accounts account;
        CheckNumbers checknumbers;

        short rc;
        short l;
        short iflag;
        int cn;
        short fiLaser;
        bool printed;
        decimal[] v = new decimal[5];
        string DepartmentName;
        string CompanyName;
        int CompanyNumber;
        string tdate;
        decimal disc;
        decimal gross;
        decimal net;
        decimal allow;
        DateTime xdate;
        int xvnum;
        int xcmp;
        string xach = "";
        int xdpt;
        short xgrp;
        string xsql;
        short postflag;
        int Batch;
        string desc;
        int r;
        string[] amt_fmt = new string[4];
        short[] amt_len = new short[4];
        short vi;
        string DefaultPrinter;
        bool vndPR;
        bool noIncrement;
        bool aligned;
        bool PRformat;
        short fi;
        string fontname = "Courier New";
        float fontsize = 11;

        public frmAPLaser()
        {
            InitializeComponent();
        }

        private void formAPLaser_Loaded(object sender, RoutedEventArgs e)
        {
            int i;

            InvoiceDistributions.TABLE = WinglGlobal.BASE_INVOICE_DISTRIBUTION_TABLE;
            Invoices.TABLE = WinglGlobal.BASE_INVOICE_TABLE;
            GC.Collect();
            Control[] tblControl = null;
            tblControl = Control.load();
            if (tblControl != null && tblControl.Length > 0)
            {
                postflag = tblControl[0].GLMethod;
                Batch = tblControl[0].GLBatchNumber + 1;
                desc = tblControl[0].GLBatchDesc;
                tblControl[0].GLBatchNumber = Batch;
                tblControl[0].save();
                tblControl = null;
            }
            amt_fmt[0] = "####,##0.00";
            amt_fmt[1] = "####,##0.00";
            amt_fmt[2] = "###,###,##0.00";
            amt_len[0] = 11;
            amt_len[1] = 11;
            amt_len[2] = 14;

            iflag = this.Iflag;

            br = System.Drawing.Brushes.Black;

            Process_Checks();

        }

        private void formAPLaser_Closed(object sender, EventArgs e)
        {
            if (rc == 0)
            {
                if (ACH)
                    MessageBox.Show("ACH Invoices processed.");
                else if (MessageBox.Show("All Checks Printed. Do you wish to Post Invoices at this time?","", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    WinglGlobal.Post_Invoices();
            }

        }
        public void 
        Process_Checks()
        {
            string dpname;
            int invCount;

            string xsql = null;
            rc = 0;
            int recCount = 0;
            int dr = 0;
            decimal dd = default(decimal);
            Checks3 = new Layout();
            Checks3.voucher = new voucher[CHECKS3_VOUCHER_COUNT];
            rpt = new DirectPrint() {font = new System.Drawing.Font("Courier New", (float)12), brush = br };
            rpt.LineSpacing = 20.0F;
            invoices = Invoices.LoadForChecks(ACH, PayDueDate.Date);

            if (invoices == null || invoices.Length == 0)
            {
                MessageBox.Show("No Invoices to Process.");
                this.Close();
                return;
            }

            xcmp = 0;
            xdpt = 0;
            xvnum = 0;
            System.Array.Clear(v, 0, v.Length);

            first = true;

            GlobalShared.Log.Log((int)LogClass.logType.Info, "**************************************************************************************", false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "APCHECK " + (ACH ? ": ACH" : "") + " Processing. " + DateAndTime.Now, false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "**************************************************************************************", false);
            if (!ACH) 
                GlobalShared.Log.Log((int)LogClass.logType.Info, "   USING PRINTER: " + printer.Name, false);

            // NACHA
            if (ACH)
            {
                dt.Columns.Add("PaymentDate");
                dt.Columns.Add("CompanyNumber");
                dt.Columns.Add("CompanyName");
                dt.Columns.Add("SEC");
                dt.Columns.Add("VendorName");
                dt.Columns.Add("InvoiceNumber");
                DataColumn colAmount = new DataColumn("colAmount");
                colAmount.DataType = System.Type.GetType("System.Decimal");
                dt.Columns.Add(colAmount);
                NachaFileHeader_Write();
            }

            l = 0;
            InvoiceDistributions[] distTbl;
            xdpt = invoices[0].Department;
            xdate = invoices[0].CheckDate;
            xach = invoices[0].ACH_TYPE;

            tblVendors = Vendors.load(invoices[0].VendorNumber);

            foreach (Invoices invoice in invoices)
            {
                tblinvoices = invoice;
                if (!ACH && (tblinvoices.CheckNumber > 0 && !Information.IsDBNull(tblinvoices.CheckDate) && tblinvoices.AllocationFlag))
                    continue;
                if (ACH && (tblinvoices.AmountPaid > 0 || tblinvoices.PayDueDate > PayDueDate))
                    continue;

                if ((xcmp != tblinvoices.Company) || (ACH && xdate.Date != tblinvoices.CheckDate.Date || (ACH && xach != tblinvoices.ACH_TYPE)))
                {
                    if (!first && ACH)
                    {
                        NachaBatchControl_Write();
                        CheckDate = tblinvoices.CheckDate.Date;
                        xcmp = tblinvoices.Company;
                        xdate = tblinvoices.CheckDate.Date;
                        CompanyNumber = tblinvoices.Company;
                        if (company == null)
                        {
                            CompanyName = "* Not On File *";
                        }
                        else
                        {
                            CompanyName = company.Name;
                        }

                    }
                    if (!printed && !first)
                        Print_Check();

                    clearrecord();
                    xcmp = tblinvoices.Company;
                    company = Company.loadByCompany(tblinvoices.Company);
                    CompanyNumber = tblinvoices.Company;
                    if (company == null)
                    {
                        CompanyName = "* Not On File *";
                    }
                    else
                    {
                        CompanyName = company.Name;
                    }
                    if (ACH)
                    {
                        xdate = tblinvoices.CheckDate.Date;
                        xach = tblinvoices.ACH_TYPE;
                        CheckDate = tblinvoices.CheckDate.Date;
                        NachaBatchHeader_Write();
                    }
                    Checks3.lcmp = xcmp.ToString("00");

                    xdpt = 0;
                    xvnum = 0;

                }




                // Test for next Department
                if (!ACH && xdpt != tblinvoices.Department)
                {
                    if (!printed && !first)
                    {
                        Print_Check();
                        clearrecord();
                    }
                    xdpt = tblinvoices.Department;
                    Checks3.ldpt = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xdpt, "000");
                    getdpt(xcmp, xdpt);
                    aligned = false;
                    xvnum = 0;
                }
                

                // Test for next Vendor
                if (ACH || xvnum != tblinvoices.VendorNumber || xgrp != tblinvoices.CheckGroup)
                {
                    //**************************************************************
                    //* Account for stub overflow to determine actual check number *
                    //**************************************************************
                    if (first)
                    {
                        first = false;
                    }
                    else if(!printed)
                    {
                        if (!ACH)
                            Print_Check();

                        clearrecord();
                    }

                    recCount = cn;

                    xsql = "SELECT * FROM gl_Invoices WHERE VendorNumber = " + tblinvoices.VendorNumber + " AND Company = " + tblinvoices.Company;
                    xsql = xsql + " AND Department = " + tblinvoices.Department + " AND CheckGroup = " + tblinvoices.CheckGroup;
                    xsql = xsql + " AND (PayFlag = 1 OR PayFlag = 2 OR ACH = 1)";
                    tblCount = Invoices.LoadQuery(xsql);
                    invCount = tblCount.Length;

                    InvDist = InvoiceDistributions.loadByInvoice(tblinvoices.InvoiceKey,"", true);

                    r = 0;
                    for (int i = 0; i < invCount; i++)
                    {
                        distTbl = null;
                        try
                        {
                            distTbl = (InvoiceDistributions[])InvoiceDistributions.loadByInvoiceAsList(tblCount[i].InvoiceKey, "", true).ToArray(typeof(InvoiceDistributions));
                        } catch { }
                        if (distTbl != null && distTbl.Length > 0)
                            r = r + distTbl.Length;
                        // ELSE ABORT THIS INVOICE
                    }
                    recCount = cn + Conversion.Int((r - 1) / CHECKS3_VOUCHER_COUNT);
                    if (recCount > cn)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "Adjusting chk# from " + cn + " to " + recCount + " due to " + r + " stub items for vendor <" + tblinvoices.VendorNumber + ">", false);
                        cn = recCount;
                    }
                    else
                        GlobalShared.Log.Log((int) LogClass.logType.Info, "Using chk# " + cn + " with " + r + " stub items for vendor <" + tblinvoices.VendorNumber + ">", false);
                    // abort this check if there are no stub items
                    if (r == 0)
                        continue;
                    xvnum = tblinvoices.VendorNumber;
                    xgrp = tblinvoices.CheckGroup;
                    xsql = "SELECT * FROM " + Vendors.TABLE + " WHERE VendorNumber = " + xvnum;
                    tblVendors = Vendors.load(xvnum);
                    if (payroll > 0 && tblVendors.Payroll)
                        PRformat = true;
                    else
                        PRformat = false;
                    vndPR = tblVendors.Payroll;
                    Checks3.payee = tblVendors.VendorName.Trim();
                    if (tblVendors.Name2 != null && tblVendors.Name2.Trim() != "")
                    {
                        Checks3.addr1 = tblVendors.Name2.Trim();
                        Checks3.addr2 = tblVendors.Street.Trim();
                        if (tblVendors.City != null && tblVendors.City.Trim() != "" && tblVendors.State != null && tblVendors.State.Trim() != "")
                            Checks3.addr3 = tblVendors.City.Trim() + ", " + tblVendors.State.Trim() + " " + tblVendors.Zip;
                    }
                    else
                    {
                        Checks3.addr1 = tblVendors.Street.Trim();
                        if (tblVendors.City != null && tblVendors.City.Trim() != "" && tblVendors.State != null && tblVendors.State.Trim() != "")
                        {
                            Checks3.addr2 = tblVendors.City.Trim() + ", " + tblVendors.State.Trim() + " " + tblVendors.Zip;
                        }
                        Checks3.addr3 = " ";
                    }
                    Checks3.voucher = null;
                    Checks3.voucher = new voucher[CHECKS3_VOUCHER_COUNT];
                    printed = false;
                }

                Memos tblMemo = Memos.load(tblinvoices.MemoId);
                if (tblMemo != null)
                {
                    bool memoSplit = false;
                    string memo2 = "";
                    string memo1 = tblMemo.Memo.Trim();
                    string[] s = memo1.Split('\r');
                    if (s.Length > 1)
                    {
                        memo1 = s[0];
                        memo2 = s[1].Replace("\n", "");
                    }
                    if (memo1.Length > WinglGlobal.MEMO_LINE_LENGTH)
                    {
                        for (int i = WinglGlobal.MEMO_LINE_LENGTH + 1; i >= 1; i--)
                        {
                            if (memo1.Substring(i, 1).Equals(" ") || memo1.Substring(i, 1).Equals("\r\n") || memo1.Substring(i, 1).Equals("/"))
                            {
                                memo2 = memo1.Substring(i + 1).Replace("\r\n", "");
                                memo1 = memo1.Left(i);
                                memoSplit = true;
                                break;
                            }
                        }
                        if (!memoSplit)
                        {
                            memo2 = memo1.Substring(WinglGlobal.MEMO_LINE_LENGTH + 1);
                            memo1 = memo1.Substring(0, WinglGlobal.MEMO_LINE_LENGTH);
                            memoSplit = true;
                        }
                    }
                    
                    Checks3.memo = memo1;
                    Checks3.memo2 = memo2.Trim();
                }
                else
                {
                    Checks3.memo = "";
                    Checks3.memo2 = "";
                }

                gross = tblinvoices.Amount - tblinvoices.AmountPaid;
                if (tblinvoices.DiscountAmount != 0 || (ACH && tblinvoices.Discount != 0))
                {
                    if (ACH)
                    {
                        decimal amount;
                        amount = 0;
                        if (tblinvoices.Discount != 0)
                        {
                            if (invoice.AmountToPay != 0)
                            {
                                amount = tblinvoices.AmountToPay - tblinvoices.Allowance;
                            }
                            else
                            {
                                amount = invoice.Amount - tblinvoices.Allowance - invoice.AmountPaid;
                            }
                            if (invoice.DiscountDollar)
                                disc = invoice.Discount;
                            else
                                disc = System.Math.Round(amount * (tblinvoices.Discount * (decimal)0.01), 2);
                            tblinvoices.DiscountAmount = disc;
                        }
                    }
                    else
                        disc = tblinvoices.DiscountAmount;
                }
                if (tblinvoices.Allowance != 0)
                    allow = tblinvoices.Allowance;
                if (tblinvoices.AmountToPay != 0)
                    gross = tblinvoices.AmountToPay;
                net = gross - disc;
                if (gross != 0 && net != 0)
                {
                    InvDist = (InvoiceDistributions[])InvoiceDistributions.loadByInvoiceAsList(tblinvoices.InvoiceKey, "", true).ToArray(typeof(InvoiceDistributions));
                    if (InvDist != null && InvDist.Length > 0)
                        dr = InvDist.Length;
                    else
                    {
                        dr = 0;

                        // ABORT THIS CHECK
                    }
                    if (payroll == 0)
                    {
                        if (dr == 1)
                            dd = disc;
                        else
                        {
                            dd = 0;
                            recCount = cn + Conversion.Int((dr - 1) / CHECKS3_VOUCHER_COUNT);
                        }
                        foreach (InvoiceDistributions dist in InvDist)
                        {
                            dpname = "*";
                            dept = Department.loadByDepartment((short)dist.Department);
                            if (dept != null)
                            {
                                dpname = dept.Name;
                                dept = null;
                            }
                            pagechk();
                            if (l < 10)
                            {
                                Checks3.voucher[l] = new voucher();
                                Checks3.voucher[l].vInvoice = tblinvoices.InvoiceNumber;
                                Checks3.voucher[l].vDate = tblinvoices.Date.ToString("MM/dd/yy");
                                Checks3.voucher[l].vDesc = dpname;
                                Checks3.voucher[l].vGross = dist.Amount.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                Checks3.voucher[l].vDisc = dd.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                Checks3.voucher[l].vNet = (dist.Amount - dd).ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                            }
                            l++;
                        }
                        InvDist = null;
                        if (ACH) l = 0;
                    }
                    else
                    {
                        dr = InvDist.Length;
                        decimal discount = Math.Round(disc / 3, 2);
                        decimal discleft = disc;
                        foreach (InvoiceDistributions dist in InvDist)
                        {
                            pagechk();
                            if (l < 10)
                            {
                                Checks3.voucher[l] = new voucher();
                                Checks3.voucher[l].vInvoice = tblinvoices.InvoiceNumber;
                                Checks3.voucher[l].vDate = tblinvoices.Date.ToString("MM/dd/yy");
                            }
                            account = Accounts.load(dist.Account);
                            if (account != null)
                            {
                                if (l < 10)
                                    Checks3.voucher[l].vDesc = account.Description;
                            }
                            else
                                Checks3.voucher[l].vDesc = "*" + dist.Account.ToString("#####0") + "*";
                            if (payroll < 1 || !vndPR)
                            {
                                if (l < 10)
                                {
                                    if (dr == 1)
                                        Checks3.voucher[l].vDisc = disc.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                    else
                                    {
                                        if (discleft < discount)
                                            discount = discleft;
                                        Checks3.voucher[l].vDisc = discount.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                        discleft -= discount;
                                    }
                                    Checks3.voucher[l].vNet = (dist.Amount - decimal.Parse(Checks3.voucher[l].vDisc)).ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                    Checks3.voucher[l].vGross = dist.Amount.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                }
                                //l++;
                                //if (dr == 1) break;
                            }
                            else if (l < 10)
                            {
                                Checks3.voucher[l].vGross = Strings.Space(11);
                                Checks3.voucher[l].vDisc = Strings.Space(11);
                                Checks3.voucher[l].vNet = (dist.Amount ).ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                                //l++;
                            }
                            l++;
                        }
                        if (ACH) l = 0;
                    }
                    Checks3.amount = net.ToString();
                    if (ACH && invoice != null && invoices.Length > 0)
                    {

                        printACH();
                    }
                    InvDist = null;
                    v[TOTAL_GROSS] += gross;
                    v[TOTAL_ALLOW] += allow;
                    v[TOTAL_DISC] += disc;
                    v[TOTAL_NET] += net;
                    tblinvoices.AmountPaid += net;
                    if (!ACH) 
                        tblinvoices.CheckNumber = cn;
                    tblinvoices.CheckDate = CheckDate;
                    tblinvoices.PayFlag = 4;
                    tblinvoices.save(true);
                    disc = 0;
                }
            }

            if (v[TOTAL_NET] > 0)
                Print_Check();
            Process_Checks_exit:
            invoices = null;
            //Time to print the checks;
            try
            {
                GlobalShared.Log.Log((int)LogClass.logType.Info, "   Calling Print routine.", false);
                rpt.Print(printer);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "      Print Error: " + ex.Message, false);
            }
            if (ACH)
            {
                int pg = 0;
                NachaBatchControl_Write();
                NachaFileControl_Write();
                sw.Close();
                if (swPR != null)
                    swPR.Close();
                DataView view = new DataView(dt);
                dataView = view;
            }
            this.Close();
        }

        public string DateTransform(string plainDate)
        {
            if (plainDate == null || plainDate.Length < 8) return "";

            return plainDate.Substring(4, 2) + "/" + plainDate.Substring(6, 2) + "/" + plainDate.Substring(0, 4);
        }

        public void Print_Check()
        {
            int post;

            if (l > 0)
            {
                if (v[TOTAL_NET] != 0)
                {
                    Checks3.chkNum = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cn, "000000");
                    Checks3.chkDate = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tdate, "mm/dd/yyyy");
                    Checks3.amount = v[TOTAL_NET].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
                    post = 1;
                }
                else
                {
                    Checks3.voidx = "V";
                    l = 0;
                }
                if (printedYet)
                    rpt.NewPage();
                else
                    printedYet = true;
                printContinuous();

                Checks3.voidx = " ";
                l = 0;

                if (!noIncrement)
                {

                    // Update CheckNumbers table
                    cn = cn + 1;
                    checknumbers = CheckNumbers.load(xcmp, xdpt);
                    if (checknumbers != null)
                    {
                        checknumbers.CheckNumber = cn;
                    }
                    else
                    {
                        checknumbers = new CheckNumbers();
                        checknumbers.Company = (short)xcmp;
                        checknumbers.Department = (short)xdpt;
                        checknumbers.CheckNumber = cn;
                    }
                    checknumbers.save();
                    checknumbers = null;
                }

                /*
                tblinvoices.CheckNumber = cn;
                tblinvoices.CheckDate = CheckDate;
                tblinvoices.save();
                */
                printed = true;
            }

        }

        public void pagechk()
        {

            decimal v4 = default(decimal);

            if (l > CHECKS3_VOUCHER_COUNT - 1)
            {
                v4 = v[TOTAL_NET];
                v[TOTAL_NET] = 0;
                noIncrement = true;
                Print_Check();
                noIncrement = false;
                v[TOTAL_NET] = v4;
                l = 0;
                printed = false;
                Checks3.voucher = null;
                Checks3.voucher = new voucher[CHECKS3_VOUCHER_COUNT];
            }

        }

        public void getdpt(int c, int d)
        {

            string dname = null;

            if (d == 0)
            {
                dname = "";
            }
            else
            {

                Department department = Department.loadByDepartment((short)d);
                if (department == null)
                {
                    dname = "* Not on file *";
                }
                else
                {
                    dname = department.Name;
                }
            }

            tdate = CheckDate.ToShortDateString();
            txtCompany.Text = CompanyName;
            txtDepartment.Text = dname;
            txtCheckDate.Text = tdate;

            checknumbers = CheckNumbers.load((short)c, (short)d);
            if (checknumbers != null)
            {
                cn = checknumbers.CheckNumber;
            }
            checknumbers = null;
            txtCheckNumber.Text = cn.ToString();

            if (!ACH && (iflag == 0 || cn == 0))
            {
                frmLaserChk LaserChk = new frmLaserChk() { CompanyName = CompanyName, DepartmentName = dname, CheckNumber = cn, CheckDate = CheckDate };
                LaserChk.Owner = this;
                LaserChk.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                LaserChk.ShowDialog();
                txtCheckNumber.Text = LaserChk.CheckNumber.ToString();
                txtCheckDate.Text = LaserChk.CheckDate.ToShortDateString();
                CheckDate = LaserChk.CheckDate;
                tdate = LaserChk.CheckDate.ToShortDateString();
                cn = LaserChk.CheckNumber;
            }
        }

        public void printContinuous()
        {
            string cent = null;
            double cents = 0;
            double dollars = 0;
            decimal vNet;
            int i;

            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            //rpt.PrintPageLine("");
            l = 0;
            rpt.PrintPageLine(xcmp.ToString("00") + xdpt.ToString("000"));
            if (PRformat)
            {
                rpt.PrintPageLine("\tP", false);
            }
            //else
            //{
            //    rpt.PrintPageLine("");
            //}
            for (i = 0; i < CHECKS3_VOUCHER_COUNT; i++)
            {
                if (Checks3.voucher[i] != null)
                {
                    Decimal.TryParse(Checks3.voucher[i].vNet, out vNet);
                    if (vNet != 0)
                    {
                        if (!PRformat)
                        {
                            rpt.PrintPageLine(Checks3.voucher[i].vInvoice.Left(20, 20) + " " + Checks3.voucher[i].vDate + " " + Checks3.voucher[i].vDesc.Left(16, 16));
                        }
                        else
                        {
                            if (i == 0)
                            {
                                rpt.PrintPageLine(Checks3.voucher[i].vInvoice.Left(20, 20) + " " + Checks3.voucher[i].vDate + " " + Checks3.voucher[i].vDesc.PadRight(28));
                            }
                            else
                            {
                                rpt.PrintPageLine(Strings.Space(30) + Checks3.voucher[i].vDesc.Left(30).PadRight(28));
                            }
                        }
                        if (!PRformat)
                        {
                            rpt.PrintPageLine(" " + Checks3.voucher[i].vGross.PadLeft(amt_len[vi]) + Checks3.voucher[i].vDisc.Trim().PadLeft(amt_len[vi] - 2) + "  " + Checks3.voucher[i].vNet.Trim().PadLeft(amt_len[vi] - 1), false);
                        }
                        else
                        {
                            rpt.PrintPageLine(Checks3.voucher[i].vNet.PadLeft(amt_len[vi] + 3).PadLeft(21), false);
                        }
                        //rpt.PrintPageLine("");
                        l++;
                    }
                }
            }
            for (i = 0; i <= 10 - l; i++)
            {
                rpt.PrintPageLine("");
            }
            if (!PRformat)
            {
                rpt.PrintPageLine(" ".PadLeft(47) + v[TOTAL_GROSS].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]) + v[TOTAL_DISC].ToString(amt_fmt[vi]).PadLeft(amt_len[vi] - 2) + " " + v[TOTAL_NET].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]));
            }
            else
            {
                rpt.PrintPageLine(" ".PadLeft(46) + Strings.Space(amt_len[vi]) + Strings.Space(amt_len[vi]) + v[TOTAL_NET].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]));
            }
            for (i = 1; i <= 4; i++)
            {
                rpt.PrintPageLine("");
            }
            if (Checks3.voidx == "V")
            {
                //rpt.PrintPageLine("");
                //rpt.PrintPageLine("");
                rpt.PrintPageLine(" ".PadLeft(56) + "   V O I D    V O I D");
            }
            else
            {
                rpt.LineSpacing = 10F;
                rpt.PrintPageLine(new string('\t', 5) + Checks3.chkDate + "\t" + Checks3.chkNum + "\t" + v[TOTAL_NET].ToString("$####,###.00").PadLeft(12));
                rpt.LineSpacing = 20F;
            }
            rpt.PrintPageLine("");
            if (Checks3.voidx == "V")
            {
                //rpt.PrintPageLine(" ".PadLeft(10) + "V O I D    V O I D    V O I D");
                rpt.PrintPageLine("");
            }
            else
            {
                dollars = (double)Conversion.Fix(v[TOTAL_NET] + (decimal)0.005);
                cents = (double)(v[TOTAL_NET] - (decimal)dollars);
                cent = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Fix(cents * 100 + 0.5), "00");
                rpt.PrintPageLine(new string('\t', 44) + dollars.ToString("#####,###").PadLeft(9) + " dollars and " + cent + " cents");
            }
            if (Checks3.voidx == "V")
            {
                rpt.PrintPageLine(" ".PadLeft(10) + "V O I D    V O I D    V O I D");
            }
            else
                rpt.PrintPageLine("");
        
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine("\t" + Checks3.payee);
            rpt.PrintPageLine("\t" + Checks3.addr1);
            rpt.PrintPageLine("\t" + Checks3.addr2);
            if (!string.IsNullOrEmpty(Strings.Trim(Checks3.addr3)))
            {
                rpt.PrintPageLine("\t" + Checks3.addr3);
            }
            else
            {
                rpt.PrintPageLine(" ");
            }
            rpt.PrintPageLine("\t" + Checks3.memo);
            rpt.PrintPageLine("\t" + Checks3.memo2);

        }

        public void printACH()
        {
            string cent = null;
            double cents = 0;
            double dollars = 0;
            decimal vNet;
            int i;
            string dfi = tblVendors.RoutingNumber;
            string acct = tblVendors.BankAccount;
            if (dfi == null || dfi.Length != 9 || acct == null || acct.Trim().Equals(""))
            {
                NachaBad.Add(tblVendors.VendorName + "{" + tblVendors.VendorNumber + "}: Invalid bank account information <Routing: " + dfi + "> <Acct: " + acct + ">");
                return;
            }
            CheckDate = tblinvoices.CheckDate;
            if (batchVendorHeader == null)
                NachaBatchHeader_Write();

            Checks3.chkNum = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblinvoices.CheckNumber, "000000");
            Checks3.chkDate = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblinvoices.CheckDate, "mm/dd/yyyy");
            //Checks3.amount = v[TOTAL_NET].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);


            NachaDetailRecord_Write();
            int sequence = 1;

            decimal d = 0;
            decimal.TryParse(Checks3.amount, out d);
            if (!PRformat)
            {

                NachaAddendaRecord_Write(dfi, "*INV " + tblinvoices.InvoiceNumber.Trim() + "*" + tblinvoices.CheckDate.ToShortDateString() + "*CHK " + Checks3.chkNum + "*AMT " + d.ToString("####,###.00") + "\\", sequence);
            }
            else
            {
                for (i = 0; i < CHECKS3_VOUCHER_COUNT; i++)
                {
                    if (Checks3.voucher[i] != null)
                    {
                        Decimal.TryParse(Checks3.voucher[i].vNet, out vNet);
                        if (vNet != 0)
                        {
/*                            if (!PRformat)
                            {
                                NachaAddendaRecord_Write(dfi, "*INV " + Checks3.voucher[i].vInvoice.Trim() + "*CHK " + Checks3.chkNum + "*AMT " + Checks3.voucher[i].vNet.Trim() + "\\", sequence);

                            }
                            else
                            {
    */                      if (i == 0)
                            {
                                if (!PRformat)
                                    NachaAddendaRecord_Write(dfi, "*INV " + Checks3.voucher[i].vInvoice.Trim() + "*CHK " + Checks3.chkNum + "*AMT " + Checks3.voucher[i].vNet.Trim() + "\\", sequence);

                                else
                                {
                                    NachaAddendaRecord_Write(dfi, "*TRN    " + Checks3.voucher[i].vDate + "*PAY " + "Net Pay: ".PadRight(21) + " " + d.ToString("####,###.00") + "\\", sequence);

                                    if (swPR == null)
                                    {
                                        try
                                        {
                                            swPR = new StreamWriter(baseFilePath + ".dat", false);

                                        }
                                        catch (IOException ex)
                                        {
                                            swPR = null;
                                            MessageBoxCustom.Show("Unable to open NACHA Payroll file: " + ex.Message);
                                        }
                                    }
                                    swPR.WriteLine(tblVendors.VendorNumber.ToString() + "|".PadRight(4) + Checks3.voucher[i].vDate + " " + Checks3.voucher[i].vDesc.PadRight(25) + " " + Checks3.voucher[i].vNet.Trim().PadLeft(11)); // Checks3.voucher[i].vInvoice.Left(20, 20) + " " + Checks3.voucher[i].vDate + " " + Checks3.voucher[i].vDesc.PadRight(28)
                                }
                            }
                            else if (PRformat)
                            {
                                swPR.WriteLine(tblVendors.VendorNumber.ToString() + "|" + Strings.Space(12) + Checks3.voucher[i].vDesc.Left(25).PadRight(25) + " " + Checks3.voucher[i].vNet.PadLeft(11), sequence); 
                            }

                        }
                        sequence++;
                    }
                }
            }
            if (PRformat)
            {
                decimal net = 0;
                decimal.TryParse(Checks3.amount, out net);
                swPR.WriteLine(tblVendors.VendorNumber.ToString() + "|" + Strings.Space(12) + "Net Pay: ".PadRight(21) + " " + net.ToString("###,##0.00").PadLeft(15), sequence);
            }
        }
        public void NachaFileHeader_Write()
        {
            #region NACHA_FILE_HEADER
            // NACHA Open File if needed
            nachaByBldDate = new DataTable();
            DataColumn dcBuilding = new DataColumn("Building");
            dcBuilding.DataType = System.Type.GetType("System.String");
            nachaByBldDate.Columns.Add(dcBuilding);

            DataColumn dcDay = new DataColumn("Day");
            dcDay.DataType = System.Type.GetType("System.Int32");
            nachaByBldDate.Columns.Add(dcDay);

            DataColumn dcAmount = new DataColumn("Amount");
            dcAmount.DataType = System.Type.GetType("System.Decimal");
            nachaByBldDate.Columns.Add(dcAmount);

            NachaControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
            if (NachaControl == null || NachaControl.Length == 0)
            {
                MessageBoxCustom.Show("NACHA File Layout not found. Cannot continue.");
                return;
            }
            foreach (NACHA_Layout rec in NachaControl)
            {
                switch (rec.Position)
                {
                    case (int)NACHA_Layout.Control_Types.VendorBatchNumber:
                        NachaControlBatchNumber = rec;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorFileDirectory:
                        NachaFileDir = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorFilePrefix:
                        NachaFilePrefix = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorFileID:
                        if (rec.Contents == null || rec.Contents.Trim().Equals(""))
                            rec.Contents = " ";
                        NachaFileTag = HelperMethods.IncrementAlphaNumeric(char.Parse(rec.Contents.Substring(0,1) ));
                        rec.Contents = NachaFileTag.ToString();
                        rec.save();
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorOffsettingTransactionCode:
                        NachaOffsettingTransactionCode = rec.Contents.Trim();
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorCheckingCredit:
                        NachaCheckingCredit = rec.Contents.Trim();
                        break;
                        /*
                    case (int)NACHA_Layout.Control_Types.VendorSavingsDebit:
                        NachaSavingDebit = rec.Contents.Trim();
                        break;
                        */
                }
            }
            // Write File Header
            if (!NachaFileDir.EndsWith("\\"))
                NachaFileDir += "\\";
            /*
            if (!Directory.Exists(NachaFileDir))
            {
                if (MessageBoxCustom.Show("Destination folder does not exist. Do you want to create it?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(NachaFileDir);
                    }
                    catch(System.IO.IOException ex)
                    {
                        MessageBoxCustom.Show("Cannot create folder: " + ex.Message);
                        this.Close();
                    }
                }
                this.Close();
                return;
            }
            */
            baseFilePath = NachaFileDir + NachaFilePrefix + "_" + NachaFileOptional;
            baseFilePath += "_" + DateTime.Now.ToString("yyyyMMdd_");
            if (!NachaFileTag.Equals(' '))
                baseFilePath += NachaFileTag + "_";
            baseFilePath += DateTime.Now.ToString("hhmm");
            filepath = baseFilePath + ".txt";
            FilePath = filepath;
            try
            {
                sw = new StreamWriter(filepath, false);

            }
            catch (IOException ex)
            {
                sw = null;
                if (MessageBoxCustom.Show("Unable to open NACHA file: " + ex.Message + "\r\n\nContinuing Printing Bills?", "", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
                    return;
            }

            // NACHA File Header
            NACHA_Layout[] headers = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileHeader);
            NachaFileHeader header = new NachaFileHeader();
            foreach(NACHA_Layout rec in headers)
            {
                switch (rec.Name.Trim())
                {
                    case MbcsCentral.NachaFileHeader.COL_RECORD_ID:
                        header.RecordTypeCode = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_PRIORITY_CODE:
                        header.PriorityCode = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_IMMEDIATE_DESITNATION:
                        header.ImmediateDestination = rec.Contents.Trim().PadLeft(10);
                        NachaDFI = rec.Contents.Trim().Left(8);
                        break;
                    case MbcsCentral.NachaFileHeader.COL_FILE_ID:
                        header.FileId = NachaFileTag.ToString();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_RECORD_SIZE:
                        header.RecordSize = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_BLOCKING_FACTOR:
                        header.BlockingFactor = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_FORMAT_CODE:
                        header.FormatCode = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_IMMEDIATE_DESTINATION_NAME:
                        header.ImmediateDestinationName = rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_IMMEDIATE_ORIGIN:
                        header.ImmediateOrigin = "1" + rec.Contents.Trim();
                        break;
                    case MbcsCentral.NachaFileHeader.COL_IMMEDIATE_ORIGIN_NAME:
                        header.ImmediateOriginName = rec.Contents.Trim();
                        break;
                }
                header.FileCreationDate = DateTime.Now.Date.ToString("yyMMdd");
                header.FileCreationTime = DateTime.Now.TimeOfDay.ToString("hhmm");
            }
            header.RecordTypeCode = headers[0].Contents.Trim();
            nachaRecordCount++;

            sw.WriteLine(NACHA_Layout.record(header.DR));
            header = null;
            #endregion
        }

        public void NachaBatchHeader_Write()
        {
            #region NACHA_BATCH_HEADER
            NACHA_Layout[] BatchVendorHeaders = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchVendorHeader);
            batchVendorHeader = new NachaBatchVendorHeader();
            nachaBatchControl = new NachaBatchControl();
            foreach (NACHA_Layout rec in BatchVendorHeaders)
            {
                switch (rec.Name.Trim())
                {
                    case NachaBatchVendorHeader.COL_RECORD_CODE:
                        batchVendorHeader.RecordTypeCode = rec.Contents.Trim();
                        break;
                    case NachaBatchVendorHeader.COL_SERVICE_CLASS_CODE:
                        batchVendorHeader.ServiceClassCode = rec.Contents.Trim();
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_NAME:
                        batchVendorHeader.CompanyName = CompanyName.Left(16, 16);
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_DISCRETIONARY_DATA:
                        batchVendorHeader.CompanyDiscretionaryData = CompanyNumber.ToString(); // rec.Contents.Left(20, 20);
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_IDENTIFICATION:
                        batchVendorHeader.CompanyIdentification = "1" + company.FederalID.Replace("-", "").Trim().Left(9, 9);
                        nachaBatchControl.CompanyIdentification = "1" + company.FederalID.Replace("-", "").Trim().Left(9, 9);
                        break;
                    case NachaBatchVendorHeader.COL_STANDARD_ENTRY_CLASS_CODE:
                        if (xach.Trim().Equals(""))
                            batchVendorHeader.StandardEntryClassCode = NACHA_Layout.Sec_Codes[(int)NACHA_Layout.Sec_Code_Types.PPD];
                        else
                            batchVendorHeader.StandardEntryClassCode = xach;

                        //batchVendorHeader.StandardEntryClassCode = rec.Contents.Trim().Left(3, 3);
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_ENTRY_DESCRIPTION:
                        batchVendorHeader.CompanyEntryDescription = rec.Contents.Trim().Left(10, 10);
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_DESCRIPTIVE_DATE:
                        batchVendorHeader.CompanyDescriptiveDate = CheckDate.ToString("MMddyy");
                        break;
                    case NachaBatchVendorHeader.COL_EFFECTIVE_DATE:
                        batchVendorHeader.EffectiveDate = CheckDate.Year.ToString("0000").Substring(2, 2) + CheckDate.Month.ToString("00") + CheckDate.Day.ToString("00");
                        break;
                    case NachaBatchVendorHeader.COL_ORIGINATOR_STATUS_CODE:
                        batchVendorHeader.OriginatorStatusCode = rec.Contents.Trim();
                        break;
                    case NachaBatchVendorHeader.COL_ORIGINATING_DFI_IDENTIICATION:
                        batchVendorHeader.OriginatingDfiIdentification = rec.Contents.Trim().Left(8, 8);
                        break;
                    case NachaBatchVendorHeader.COL_BATCH_NUMBER:
                        batchVendorHeader.BatchNumber = (++NachaControlBatchNumber.Length).ToString("0000000").PadLeft(7);
                        nachaBatchControl.BatchNumber = batchVendorHeader.BatchNumber;
                        NachaControlBatchNumber.save();
                        break;
                }
            }

            nachaTotalBatches++;
            nachaRecordCount++;
            sw.WriteLine(NACHA_Layout.record(batchVendorHeader.DR));
            #endregion
        }

        public void NachaDetailRecord_Write(bool OffSetRecord = false)
        {
            #region NACHA DETAIL RECORD
            string dfi = "";
            string acct = "";
            if (OffSetRecord)
            {
                dfi = company.RoutingNumber;
                acct = company.BankAccount;
            }
            else
            {
                dfi = tblVendors.RoutingNumber;
                acct = tblVendors.BankAccount;
            }
            if (dfi == null || dfi.Length != 9 || acct == null || acct.Trim().Equals(""))
            {
                NachaBad.Add((company.CompanyNumber.ToString() + "/" + tblVendors.VendorNumber.ToString()).PadRight(8) + ": Invalid bank account information <Routing: " + dfi + "> <Acct: " + acct + ">");
                return;
            }

            nachaDetail = new NachaDetail();
            foreach (NACHA_Layout rec in NachaDetailRecord)
            {
                switch (rec.Name.Trim())
                {
                    case NachaDetail.COL_RECORD_CODE:
                        nachaDetail.RecordTypeCode = rec.Contents.Trim();
                        break;
                    /*
                    case NachaDetail.COL_ADDENDA_RECORD_INDICATOR:
                        nachaDetail.AddendaRecordIndicator = rec.Contents.Trim();
                        break;
                    */
                }
            }
            nachaDetail.AddendaRecordIndicator = (string)(OffSetRecord ? "0" : "1");
            nachaDetail.TransactionCode = OffSetRecord ? NachaOffsettingTransactionCode : NachaCheckingCredit;
            nachaDetail.ReceivingDfiIdentification = dfi.Substring(0, 8);
            nachaDetail.CheckDigit = dfi.Substring(8, 1);
            nachaDetail.DfiAccountNumber = acct;
            decimal d = 0;
            if (OffSetRecord)
            {
                d = nachaTotalDebitAmount;
                nachaFileDebitAmount += d;
            }
            else
            {
                d = 0;
                decimal.TryParse(Checks3.amount, out d);
                nachaTotalCreditAmount += d;
                nachaTotalDebitAmount += d;
                nachaFileCreditAmount += d;
            }
            nachaDetail.Amount = (d * 100).ToString("0000000000");
            nachaDetail.IndividualIdentificationNumber = OffSetRecord ? "" : tblVendors.VendorNumber.ToString();
            nachaDetail.IndividualName = OffSetRecord ? CompanyName : Checks3.payee.Left(22, 22);
            nachaDetail.TraceNumber = NachaDFI + (NachaSeq++).ToString("0000000").PadLeft(7, '0');
            NachaTracenumber = nachaDetail.TraceNumber;
            //NachaTracenumber = NachaDFI + (NachaSeq - 1).ToString("0000000").PadLeft(7, '0');
            sw.WriteLine(NACHA_Layout.record(nachaDetail.DR));
            nachaDetail = null;
            if (!OffSetRecord)
            {
                ProcessedInvoices invoice = new ProcessedInvoices();
                dataRow = dt.NewRow();
                dataRow["colAmount"] = d;
                dataRow["CompanyNumber"] = company.CompanyNumber;
                dataRow["SEC"] = tblinvoices.ACH_TYPE;
                dataRow["CompanyName"] = CompanyName;
                dataRow["VendorName"] = Checks3.payee;
                dataRow["PaymentDate"] = xdate.ToString("yyyyMMdd");
                dataRow["InvoiceNumber"] = tblinvoices.InvoiceNumber;

                dt.Rows.Add(dataRow);


                //NachaProcessed.Add(invoice);
            }
            int result = 0;
            int.TryParse(dfi.Substring(0, 8), out result);
            nachaBatchHash += result;
            nachaFileHash += result;
            nachaRecordCount++;
            nachaBatchCount++;
            nachaBatchCountTotal++;

/*             if  (OffSetRecord)
            {
                nachaTotalCreditAmount = 0;
                nachaTotalDebitAmount = 0;
            }
*/
            printed = true;
            #endregion
        }

        public void NachaAddendaRecord_Write(string dfi, string data, int sequence)
        {
            #region NachaAddendaRecord
            NachaAddenda nachaAddenda = new NachaAddenda();
            foreach (NACHA_Layout rec in NachaAddendaRecord)
                switch (rec.Name.Trim())
                {
                    case NachaAddenda.COL_RECORD_CODE:
                        nachaAddenda.RecordTypeCode = rec.Contents.Trim();
                        break;
                    case NachaAddenda.COL_TRANSACTION_CODE:
                        nachaAddenda.TransactionCode = rec.Contents.Trim();
                        break;
                }
            nachaAddenda.Data = data;
            nachaAddenda.AddendaSequenceNumber = sequence.ToString("0000").PadLeft(4, '0');
            nachaAddenda.DetailSequenceNumber = NachaTracenumber.Right(7); // ?????????????
            sw.WriteLine(NACHA_Layout.record(nachaAddenda.DR));
            nachaAddenda = null;

            int result = 0;
            int.TryParse(dfi.Substring(0, 8), out result);
            nachaRecordCount++;
            nachaBatchCount++;
            nachaBatchCountTotal++;

            #endregion
        }

        public void NachaBatchControl_Write()
        {
            NachaDetailRecord_Write(true);
            NACHA_Layout[] BatchControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchControl);
            foreach (NACHA_Layout rec in BatchControl)
            {
                switch (rec.Name.Trim())
                {
                    case NachaBatchControl.COL_RECORD_CODE:
                        nachaBatchControl.RecordTypeCode = rec.Contents.Trim();
                        break;
                    case NachaBatchControl.COL_SERVICE_CLASS_CODE:
                        nachaBatchControl.ServiceClassCode = rec.Contents.Trim().Left(3);
                        break;
                    case NachaBatchControl.COL_COMPANY_IDENTIFICATION:
                        nachaBatchControl.CompanyIdentification = rec.Contents.Trim();
                        break;
                    case NachaBatchControl.COL_ORIGINATING_DFI_IDENTIFICATION:
                        nachaBatchControl.OriginatingDfiIdentification = rec.Contents.Trim();
                        break;
                }
            }
            nachaBatchControl.EntryAddendaCount = nachaBatchCount.ToString("000000");
            nachaBatchControl.EntryHash = nachaBatchHash.ToString().Left(10).PadLeft(10, '0');
            nachaBatchControl.TotalDebitAmount = (nachaTotalDebitAmount * 100).ToString("000000000000");
            nachaBatchControl.TotalCreditAmount = (nachaTotalCreditAmount * 100).ToString("000000000000");
            nachaRecordCount++;
            sw.WriteLine(NACHA_Layout.record(nachaBatchControl.DR));
            nachaBatchControl = null;
            nachaTotalCreditAmount = 0;
            nachaTotalDebitAmount = 0;
            nachaBatchCount = 0;
            nachaBatchHash = 0;
        }

        public void NachaFileControl_Write()
        {
            NACHA_Layout[] FileControls = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileControl);
            NachaFileControl fileControl = new NachaFileControl();
            foreach (NACHA_Layout rec in FileControls)
            {
                switch (rec.Name.Trim())
                {
                    case NachaFileControl.COL_RECORD_CODE:
                        fileControl.RecordTypeCode = rec.Contents.Trim();
                        break;

                }
            }
            nachaRecordCount++;
            fileControl.BatchCount = nachaTotalBatches.ToString("000000");
            fileControl.BlockCount = Math.Ceiling(nachaRecordCount / 10).ToString("000000");
            fileControl.EntryAddendaCount = nachaBatchCountTotal.ToString("00000000");
            fileControl.EntryHash = nachaFileHash.ToString().Left(10).PadLeft(10, '0');
            fileControl.TotalDebitAmount = (nachaFileDebitAmount * 100).ToString("000000000000");
            fileControl.TotalCreditAmount = (nachaFileCreditAmount * 100).ToString("000000000000");
            sw.WriteLine(NACHA_Layout.record(fileControl.DR));
            fileControl = null;
        }
        public void clearrecord()
        {
            gross = 0;
            allow = 0;
            disc = 0;
            net = 0;
            System.Array.Clear(v, 0, v.Length);
            Checks3.voucher = null;
            Checks3.voucher = new voucher[CHECKS3_VOUCHER_COUNT];
            Checks3.lcmp = " ";
            Checks3.ldpt = " ";
            Checks3.voidx = " ";
            Checks3.chkNum = " ";
            Checks3.chkDate = "";
            Checks3.amount = "";
            Checks3.vendor = " ";
            Checks3.payee = " ";
            Checks3.addr1 = " ";
            Checks3.addr2 = " ";
            Checks3.addr3 = " ";
            Checks3.addr4 = " ";
            Checks3.memo = " ";

        }

    }

    public class voucher
    {
        public string vInvoice { get; set; } = "";
        public string vDate { get; set; } = "";
        public string vDesc { get; set; } = "";
        public string vGross { get; set; } = "";
        public string vDisc { get; set; } = "";
        public string vNet { get; set; } = "";
    }

    public class Layout
    {
        public string layout { get; set; }
        public string lcmp { get; set; }
        public string ldpt { get; set; }
        public string x1 { get; set; }
        public string voidx { get; set; }
        public string chkNum { get; set; }
        public string chkDate { get; set; }
        public string amount { get; set; }
        public string vendor { get; set; }
        public string payee { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string addr4 { get; set; }
        public voucher[] voucher { get; set; }
        public string memo { get; set; }
        public string memo2 { get; set; } = "";
        public string crlf { get; set; }
    }
}