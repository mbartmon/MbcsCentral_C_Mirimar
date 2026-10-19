using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Text;
using MbcsCentral;
using ExtensionMethods;
using CSharp;
using MbcsCentral.WinDoc;
using Microsoft.VisualBasic;
using Spire.Pdf.Barcode;

namespace MbcsCentral.WinGL
{
    class WinglGlobal
    {

        public const string BASE_INVOICE_DISTRIBUTION_TABLE = "gl_InvoiceDistributions";
        public const string BASE_INVOICE_TABLE = "gl_Invoices";

        public struct PostInvoice_type
        {
            public string invnumber;
            public System.DateTime date;
            public System.DateTime dueDate;
            public int Company;
            public int Department;
            public int vendor;
            public bool payroll;
            public decimal amountpaid;
            public decimal amounttopay;
            public int Batch;
            public int CheckNumber;
            public System.DateTime datelastpaid;
            public System.DateTime CheckDate;
            public short payflag;
            public string vName;
        }

        //************************************
        //* Master Chart Structure variables *
        //************************************
        public struct Entry_type
        {
            public string type;
            public string Description;
            public string key;
        }
        public static Entry_type[,,] LH;
        private static int LH_Dim1 = 6;
        private static int LH_Dim2 = 11;
        private static int LH_Dim3 = 250; 
        public static object[,] CatTotals = new object[6, 3];

        public static string[] TransEditListSorts = { "Amount", "CheckNumber", "CompanyNumber", "DepartmentNumber", "Description", "Source" };
        public static PostInvoice_type invpGlobal;
        public static int GLCashAcct;
        private static ArrayList Success = new ArrayList();
        private static ArrayList Failure = new ArrayList();
        private static int InvoicesIn = 0;
        private static int ChecksPosted = 0;
        private static int ChecksNotPosted = 0;
        private static int Batch = 0;
        private static System.Windows.Window results = null;
        public const short tmargin = 5;
        public const int MAXROWS = 150;

        // Batch Report Variables
        // Used to save batch report parameters
        public static object[] BatchRptSave = new object[17];
        public static string BatchListName;
        public static System.DateTime bStartDate;
        public static System.DateTime bEndDate;
        public static short bMonth;
        public static short bYear;
        public const string USE_PRIVATE = "_private";

        // Variables added for Windoc interface
        public static bool scan;
        public static bool PostChecks = false;
        public static string WinDocPath;
        public static object rss;
        static OleDbConnection CN = null;
        //static RootList DocList = new RootList();
        static OleDbDataAdapter da;
        static readonly object dummynode = null;
        public static object cnn;
        public static string uniqueID;
        public static string vName;
        public static int PID;
        public static object rsd;
        public static bool InvoiceBatch;
        public const string SCAN_PAID = "paid";
        public const string SCAN_UNPAID = "unpaid";
        public const string SCAN_MANUAL = "manual";

        //*************************
        //* Check Register Layout *
        //*************************
        public struct details_type
        {
            public int CompanyNumber;
            public int DepartmentNumber;
            public int AccountNumber;
            public short FiscalYear;
            public System.DateTime date;
            public string Journal;
            public string Source;
            public string Description;
            public string VendorNumber;
            public int CheckNumber;
            public decimal amount;
            public short MajorCategory;
            public short MinorCategory;
            public int IndexNumber;
            public bool Flag;
        }
        public struct Register_type
        {
            public int comp;
            public int dept;
            public int chk;
            public System.DateTime date_Renamed;
            public int vend;
            public decimal amt;
            public details_type[] details;

            public void Initialize()
            {
                details = new details_type[WinglGlobal.MAXROWS + 1];
            }
        }
        public static Register_type cr =  new Register_type();

        //**********************
        // Journal Report Fields
        //**********************
        public static string[] JournalFields = { "Transaction Date (8)", "Account Name (50)", "Transaction Amount (11)", "Transaction Description (50)", "Check Number (6)", "Source (32)", "Department (50)" };
        public static string[] JournalFieldTypes = { "", "C", "D" };

        //*********************************************
        // DataTable for use by Bank Reconcialtion form
        //*********************************************
        public static DataTable BankReconDT = null;

        // Check Memo Line - length of each printed line
        public static int MEMO_LINE_LENGTH = 60;

        public static void FillHierarchy(string subKey, ref List<Category> list)
        {
            Category[] cats = Category.loadBySubKey(subKey);
            if (cats != null && cats.Count() > 0)
            {
                list = new List<Category>();
                foreach (Category cat in cats)
                {
                    List<Category> newlist = new List<Category>();
                    FillHierarchy(cat.Key, ref newlist);
                    list.Add(cat);
                }
            }
        }

        private static void InitializeLH()
        {
            for (int a = 0; a <= LH.GetUpperBound(0); a++)
            {
                for (int b = 0; b <= LH.GetUpperBound(1); b++)
                    for (int c = 0; c <= LH.GetUpperBound(2); c++)
                    {
                        LH[a, b, c].Description = " ";
                        LH[a,b,c].key = " ";
                        LH[a, b, c].type = " ";
                   }
            }
        }
        public static void getLedgerStructure(bool print = true)
        {
            //***************************************************************************
            //* This routine returns an array that contains the categories and accounts *
            //* in the same sequence as the Category/Account screen. (LH)               *
            //*                                                                         *
            //* It also returns an array that contains the Category descriptions.       *
            //*                                                                         *
            //* IMPORTANT: ERASE LH when finished using it to conserve memory           *
            //***************************************************************************
            Category[] tblcats = null;
            short l = 0;
            int m = 0;

            LH = new Entry_type[LH_Dim1, LH_Dim2, LH_Dim3];
            InitializeLH();
            tblcats = Category.loadBySubKey("0_");
            if (tblcats != null && tblcats.Length > 0)
            {
                foreach (Category tblcat in tblcats)
                {
                    m = int.Parse(tblcat.Key.Substring(0,3));
                    LH[m, 0, 0].type = "C";
                    LH[m, 0, 0].Description = tblcat.CategoryDescription;
                    LH[m, 0, 0].key = tblcat.Key.Trim();
                    CatTotals[m, 2] = tblcat.CategoryDescription;
                    l = 1;
                    get_Accounts(tblcat.Key, ref l, m);
                    get_subCategories(tblcat.Key, l, m);
                }
            }
            tblcats = null;

            /*
            if (print)
            {
                //System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
                //string printerName = ps.PrinterName;
                string fontname = "Courier New";
                float fontsize = 8;
                int last = 0;

                Report rpt = new Report("Ledger Structure", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };

                for (int a = 0; a <= LH.GetUpperBound(0); a++)
                    for (int b = 0; b <= LH.GetUpperBound(1); b++)
                        for (int c = 0; c <= LH.GetUpperBound(2); c++)
                        {
                            if (!LH[a, b, c].Description.Trim().Equals(""))
                            {
                                if (a != last)
                                {
                                    rpt.PrintPageLine("");
                                    last = a;   
                                }
                                if (rpt.PrintPageLine(String.Format("{0}/{1}/{2} {3}; {4}; {5}", a, b, c, LH[a, b, c].Description, LH[a, b, c].type, LH[a, b, c].key)))
                                    rpt.NewPage();

                            }
                        }
                rpt.DisplayDocument();
            }
            */

        }
        private static void get_Accounts( string k,  ref short lv,  int m,  bool activeOnly = true)
        {
            object xsql = null;

            Accounts[] tblaccts = null;
            string s = null;
            short i = 0;

            tblaccts = Accounts.loadByCategory(k, true);
            i = 0;
            if (tblaccts != null && tblaccts.Length > 0)
            {
                foreach (Accounts tblacct in tblaccts)
                {
                    i++;
                    LH[m, lv, i].type = "A";
                    LH[m, lv, i].Description = tblacct.Description.Trim();
                    LH[m, lv, i].key = tblacct.Key.ToString("00000000");
                }
                lv++;
            }
            tblaccts = null;
        }
        //******************************************************************************
        private static void get_subCategories(string k, short lv, int m)
        {
            object xsql = null;
            //******************************************************************************

            Category[] tblcats = Category.loadBySubKey(k);

            if (tblcats != null && tblcats.Length > 0)
            {
                foreach (Category tblcat in tblcats)
                {
                    LH[m, lv, 0].type = "S";
                    LH[m, lv, 0].Description = tblcat.CategoryDescription;
                    LH[m, lv, 0].key = tblcat.Key;
                    lv++;
                    get_Accounts(tblcat.Key, ref lv, m);
                    get_subCategories(tblcat.Key, lv, m);
                }
            }
            tblcats = null;

        }

    public static DateTime CurrentQuarter(int cmp, DateTime dte)
        {
            int m = dte.Month;
            int y = dte.Year;
            int qys;
            DateTime fys;
            int q;

            switch (m)
            {
                case 1:
                case 2:
                case 3:
                    q = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    q = 4;
                    break;
                case 7:
                case 8:
                case 9:
                    q = 7;
                    break;
                default:
                    q = 10;
                    break;
            }

            return new DateTime(y, q, 1);
        }

        /*
        public static DateTime CurrentQuarter( int cmp,  DateTime dte)
        {
            DateTime functionReturnValue = default(System.DateTime);
            int i;
            int m;
            int fsy;
            int y;

            int qys = 0;
            DateTime fys = default(System.DateTime);
            short[] q = new short[5];

            fys = CurrentFiscalYear( cmp);
            if (dte >= fys)
            {
                y = fys.Year;
            }
            else
            {
                y = dte.Year;
            }
            m = DateTime.Month(fsy);
            for (i = 1; i <= 4; i++)
            {
                q[i] = m;
                m = m + 3;
                m = m % 12;
            }
            for (i = 1; i <= 4; i++)
            {
                if (m - q[i] >= 0 & m - q[i] < 3)
                {
                    functionReturnValue = new DateTime(y, q[i], 1);
                    return functionReturnValue;
                }
            }
            return functionReturnValue;

        }
        */
        public static DateTime CurrentFiscalYear(int cmp = 0)
        {
            int cfy = DateTime.Now.Year;
            int fye = 12;
            int fys;

            Control[] ctrl = Control.load();
            if (ctrl != null && ctrl.Length > 0)
                cfy = ctrl[0].CurrentFiscalYear;

            if (cmp > 0)
            {
                Company company = Company.loadByCompany(cmp);
                if (company != null)
                {
                    cfy = company.CurrentFiscalYear;
                    fye = company.FiscalYearEnd;
                }
            }
            fys = (int)(fye == 12 ? 1 : fye + 1);
            if (cfy < 2000)
                cfy = 2000;
            return new DateTime(cfy, fys, 1);
        }

        /// <summary>
        /// This routine will iterate through the Invoices table and post any invoice
        /// with a non-zero check number and valid check date
        /// 
        /// </summary>
        public static void Post_Invoices(bool batchProcess = false, bool EFT = false, bool ACH = false)
        {
            if (EFT && !GlobalShared.MethodApp.Trim().Equals("") && !GlobalShared.user.hasAccessPermission(GlobalShared.MethodApp))
            {
                MessageBoxCustom.Show("You do not have permission to post EFT invoices.");
                return;
            }
            if (ACH && !GlobalShared.MethodApp.Trim().Equals("") && !GlobalShared.user.hasAccessPermission(GlobalShared.MethodApp))
            {
                   MessageBoxCustom.Show("You do not have permission to post ACH invoices.");
                return;
            }
            GC.Collect();

            Invoices[] tblInvoice = null;
            Invoices inv = null;
            string desc = null;
            PostInvoice_type invp = new PostInvoice_type();
            short postflag = 0;
            InvoiceDistributions[] tblDist = null;
            tblInvoice = Invoices.load(EFT, ACH, "gl_Invoices");
            if (tblInvoice == null || tblInvoice.Length == 0)
            {
                MessageBoxCustom.Show("There are no invoices to process.");
                return;
            }

            InvoicesIn = 0;
            Control[] control = Control.load();
            Control tblControl = control[0];
            postflag = tblControl.GLMethod;
            Batch = tblControl.GLBatchNumber + 1;
            desc = tblControl.GLBatchDesc;
            GLCashAcct = tblControl.GLCashAccount;
            scan = tblControl.Scan;
            string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + tblControl.WindocLocation.Trim() + "\\windoc.mdb" + ";Mode=Share Deny None";
            tblControl.GLBatchNumber = Batch;
            tblControl.save();
            if (scan) {
                WinDocPath = tblControl.WindocLocation;
                if (Main.VerifyWindocLocation(WinDocPath))
                {
                    CN = new OleDbConnection(WindocConnectionString);
                    try
                    {
                        CN.Open();
                    }
                    catch (OleDbException ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not open connection for documents: " + ex.Message, false);
                        return;
                    }
                    uniqueID = DateTime.Now.ToString();
                    da = new OleDbDataAdapter("SELECT * FROM documents", WindocConnectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = CN;
                    cmd.CommandText = "SELECT *" +
                                      " FROM interface";

                    DataSet ds = new DataSet("Windoc");
                    da.SelectCommand = cmd;
                    ds = new DataSet("Windoc");
                    try
                    {
                        da.Fill(ds, "Documents");
                    }
                    catch (OleDbException ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Windoc Database not specified. Paid invoices will not be posted to Windoc: " + ex.Message, true);
                        return;
                    }
                    catch (SystemException syEx)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Windoc Database not specified. Paid invoices will not be posted to Windoc: " + syEx.Message, true);
                        return;
                    }
                    try
                    {
                        CN.Close();
                    }
                    catch { }

                }
                else
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Windoc Database not specified. Paid invoices will not be posted to Windoc.", System.Windows.Forms.MessageBoxButtons.OK, "", false);
                    scan = false;
                }
            }

            foreach (Invoices invoice in tblInvoice)
            {
                if ((ACH && invoice.PayFlag == 4) || (!ACH && invoice.CheckNumber > 0 && invoice.CheckDate != null && invoice.CheckDate > GlobalShared.NULL_DATE && invoice.AllocationFlag))
                {
                    if (invoice.Amount == 0)
                    {
                        MessageBoxCustom.Show("A Zero amount invoice is about to be posted:\r\n\r\nVendor: " + invoice.VendorName + "\r\nCompany/Department:" + invoice.Company.ToString() + "/" + invoice.DepartmentName + "\r\nInvoice Number: " + invoice.InvoiceNumber + "\r\nCheck Number: " + invoice.CheckNumber.ToString());
                        continue;
                    }
                    invp.amountpaid = invoice.Amount - invoice.DiscountAmount;
                    invp.amounttopay = 0;
                    invp.Batch = Batch;
                    invp.invnumber = invoice.InvoiceNumber;
                    invp.vendor = invoice.VendorNumber;
                    invp.CheckNumber = invoice.CheckNumber;
                    invp.datelastpaid = invoice.CheckDate;
                    invp.CheckDate = invoice.CheckDate;
                    invp.payflag = 4;
                    invp.vName = invoice.VendorName;
                    Post_Invoice(invoice.InvoiceKey, postflag, Batch, GLCashAcct, desc, invp, true, scan);
                    InvoicesIn += 1;
                }
            }
            tblInvoice = null;
            // Delete any distribution with a -1 InvoiceKey
            // This happens to unsaved new invoices
            tblDist = InvoiceDistributions.load();
            InvoiceDistributions.DeleteAll(tblDist, -1);

            // Delete unused Memos (WHERE VendorNumber is negative ANDD BatchNumber is 0)
            //       
            Memos memo = new Memos();
            memo.DeleteUnused(false);
            /*
            foreach (InvoiceDistributions dist in tblDist)
            {
                inv = Invoices.load(tblDist[0].InvoiceKey);
                if (tblInvoice == null || tblInvoice.Length == 0) {
                    dist.delete(dist.Id.ToString());
                }
                inv = null;
            }
            tblDist = null;
            */

            #region Reporting Window
            {
                // Window definition
                results = new System.Windows.Window();
                results.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                results.Title = "Posting Results";
                results.Width = 750;
                results.Height = 450;
                results.Background = System.Windows.Media.Brushes.Bisque;
                TextBlock.SetFontWeight(results, FontWeights.Bold);
                TextBlock.SetFontSize(results, 14);

                Border border = new Border() { BorderBrush = System.Windows.Media.Brushes.Blue, BorderThickness = new Thickness(3), CornerRadius = new CornerRadius(5) };

                // Grid definition
                #region Grid Definition
                Grid grid = new Grid();
                TextBlock.SetFontSize(grid, 12);
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                border.Child = grid;
                #endregion

                // Top border and panel: Invoices Processed
                #region Top border and panel: Invoices Processed
                Border bdr1 = new Border();
                bdr1.BorderBrush = System.Windows.Media.Brushes.Blue;
                bdr1.BorderThickness = new Thickness(2);
                Grid.SetRow(bdr1, 0);
                grid.Children.Add(bdr1);

                StackPanel pnlTop = new StackPanel();
                pnlTop.Orientation = Orientation.Horizontal;
                pnlTop.Background = System.Windows.Media.Brushes.AliceBlue;

                bdr1.Child = pnlTop;

                TextBlock txt1 = new TextBlock();
                txt1.HorizontalAlignment = HorizontalAlignment.Left;
                txt1.VerticalAlignment = VerticalAlignment.Center;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.FontWeight = FontWeights.Bold;
                txt1.Background = System.Windows.Media.Brushes.AliceBlue;
                txt1.Margin = new Thickness(10, 0, 0, 0);
                txt1.Text = (EFT ? "EFT " : "") + "Invoices Processed";
                pnlTop.Children.Add(txt1);

                TextBox txtNumberProcessed = new TextBox();
                txtNumberProcessed.Width = 48;
                txtNumberProcessed.Margin = new Thickness(15, 0, 0, 0);
                txtNumberProcessed.VerticalAlignment = VerticalAlignment.Center;
                txtNumberProcessed.Background = System.Windows.Media.Brushes.AliceBlue;
                txtNumberProcessed.BorderBrush = System.Windows.Media.Brushes.Black;
                txtNumberProcessed.BorderThickness = new Thickness(2);
                pnlTop.Children.Add(txtNumberProcessed);

                txtNumberProcessed.Text = InvoicesIn.ToString("####0");
                #endregion

                // Middle border and panel: Checks Posted
                #region Middle border and panel: Checks Posted
                Border bdr2 = new Border();
                bdr2.BorderBrush = System.Windows.Media.Brushes.Blue;
                bdr2.BorderThickness = new Thickness(2);
                Grid.SetRow(bdr2, 1);
                grid.Children.Add(bdr2);

                StackPanel pnlMiddle = new StackPanel();
                pnlMiddle.Orientation = Orientation.Horizontal;
                pnlMiddle.Background = System.Windows.Media.Brushes.AliceBlue;
                bdr2.Child = pnlMiddle;

                TextBlock txt2 = new TextBlock();
                txt2.HorizontalAlignment = HorizontalAlignment.Left;
                txt2.VerticalAlignment = VerticalAlignment.Center;
                txt2.TextAlignment = TextAlignment.Center;
                txt2.FontWeight = FontWeights.Bold;
                txt2.Background = System.Windows.Media.Brushes.AliceBlue;
                txt2.Margin = new Thickness(10, 0, 0, 0);
                txt2.Text = "Checks Posted";
                pnlMiddle.Children.Add(txt2);

                TextBox txtChecksPosted = new TextBox();
                txtChecksPosted.Width = 48;
                txtChecksPosted.Margin = new Thickness(15, 0, 0, 0);
                txtChecksPosted.VerticalAlignment = VerticalAlignment.Center;
                txtChecksPosted.Background = System.Windows.Media.Brushes.AliceBlue;
                txtChecksPosted.BorderBrush = System.Windows.Media.Brushes.Black;
                txtChecksPosted.BorderThickness = new Thickness(2);
                pnlMiddle.Children.Add(txtChecksPosted);

                txtChecksPosted.Text = ChecksPosted.ToString("####0");
                #endregion

                // Bottom border and panel: Checks not posted count and list
                #region Bottom border and panel: Checks not posted count and list
                Border bdr3 = new Border();
                bdr3.BorderBrush = System.Windows.Media.Brushes.Blue;
                bdr3.BorderThickness = new Thickness(2);
                Grid.SetRow(bdr3, 2);
                grid.Children.Add(bdr3);

                // Bottom panel
                #region Bottom panel
                StackPanel pnlBottom = new StackPanel();
                pnlBottom.Orientation = Orientation.Vertical;
                pnlBottom.Background = System.Windows.Media.Brushes.MediumOrchid;

                bdr3.Child = pnlBottom;
                #endregion

                StackPanel pnlBottom1 = new StackPanel();
                pnlBottom1.Orientation = Orientation.Horizontal;
                pnlBottom1.Background = System.Windows.Media.Brushes.MediumOrchid;
                pnlBottom.Children.Add(pnlBottom1);

                TextBlock txt3 = new TextBlock();
                txt3.HorizontalAlignment = HorizontalAlignment.Left;
                txt3.VerticalAlignment = VerticalAlignment.Center;
                txt3.TextAlignment = TextAlignment.Center;
                txt3.FontWeight = FontWeights.Bold;
                txt3.Background = System.Windows.Media.Brushes.AliceBlue;
                txt3.Margin = new Thickness(10, 0, 0, 0);
                txt3.Text = "Checks Not Posted";
                pnlBottom1.Children.Add(txt3);

                TextBox txtNotPosted = new TextBox() { FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center };
                txtNotPosted.Background = System.Windows.Media.Brushes.AliceBlue;
                txtNotPosted.BorderBrush = System.Windows.Media.Brushes.Black;
                txtNotPosted.BorderThickness = new Thickness(2);
                pnlBottom1.Children.Add(txtNotPosted);

                txtNotPosted.Text = ChecksNotPosted.ToString("####0");

                StackPanel pnlBottom2 = new StackPanel();
                pnlBottom2.Orientation = Orientation.Horizontal;
                pnlBottom2.Background = System.Windows.Media.Brushes.MediumOrchid;
                pnlBottom.Children.Add(pnlBottom2);

                ListBox lstNotPosted = new ListBox();
                lstNotPosted.HorizontalAlignment = HorizontalAlignment.Stretch;
                lstNotPosted.VerticalAlignment = VerticalAlignment.Stretch;
                lstNotPosted.Background = System.Windows.Media.Brushes.LightBlue;
                //lstNotPosted.ItemsSource = Failure;
                //pnlBottom2.Children.Add(lstNotPosted);
                #endregion

                /*
                TextBlock txt2 = new TextBlock();
                txt2.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt2.VerticalAlignment = VerticalAlignment.Stretch;
                txt2.TextAlignment = TextAlignment.Center;
                txt2.FontWeight = FontWeights.Bold;
                txt2.Text = "EMAILS NOT SENT";
                pnlBottom.Children.Add(txt2);

                txtNumberNotSent = new TextBox();
                txtNumberNotSent.Width = 48;
                txtNumberNotSent.Margin = new Thickness(15, 0, 0, 0);
                txtNumberNotSent.VerticalAlignment = VerticalAlignment.Center;
                txtNumberNotSent.Background = System.Windows.Media.Brushes.AliceBlue;
                txtNumberNotSent.BorderBrush = System.Windows.Media.Brushes.Black;
                txtNumberNotSent.BorderThickness = new Thickness(2);
                pnlBottom.Children.Add(txtNumberNotSent);

                lstNotSent = new ListBox();
                lstNotSent.HorizontalAlignment = HorizontalAlignment.Stretch;
                lstNotSent.VerticalAlignment = VerticalAlignment.Stretch;
                lstNotSent.ItemsSource = emailsNotSent;
                Grid.SetRow(lstNotSent, 3);
                grid.Children.Add(lstNotSent);
                */

                // Command button panel
                #region Command button panel
                StackPanel pnlCommand = new StackPanel();
                pnlCommand.Orientation = Orientation.Horizontal;
                Grid.SetRow(pnlCommand, 3);
                grid.Children.Add(pnlCommand);

                Button cmdPrint = new Button() { Content = "PRINT REPORT", Height = 30 };
                cmdPrint.Click += cmdPrint_Click;
                pnlCommand.Children.Add(cmdPrint);

                Button cmdClose = new Button() { Content = "CLOSE", Height = 30, Margin = new Thickness(10, 0, 0, 0) };
                cmdClose.Click += cmdClose_Click;
                pnlCommand.Children.Add(cmdClose);

                results.Content = border;
                results.ShowDialog();
                results.InvalidateVisual();
                results.UpdateLayout();
                #endregion
            }
            #endregion
            /*
            MessageBoxResult result = MessageBoxCustom.Show("Print the Transaction Edit List?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    object[] obj = null;
                    frmReportDispatcher frm = new frmReportDispatcher("MbcsCentral.WinGL.TransactionEditList", obj);
                    break;
                case MessageBoxResult.No:
                    break;
            }
            */

            tblControl = null;
            control = null;

            if (scan && PostChecks)
            {
                bool r = false;
                System.Diagnostics.Process exProc = new System.Diagnostics.Process();
                exProc.StartInfo.FileName = WinDocPath.Trim() + "\\windoc.exe";
                exProc.StartInfo.Arguments = "PostChecks";
                try
                {
                    r = exProc.Start();
                }
                catch (System.Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to post checks to Windoc: " + ex.Message, false);
                    MessageBoxCustom.Show("Unable to post checks to Windoc: " + ex.Message);
                }
                if (!r)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to start Windoc for check posting.", false);
                    MessageBoxCustom.Show("Unable to start Windoc for check posting.");
                }

            }

            //GlobalShared.Log.Log((int)LogClass.logType.Info, Convert.ToString(InvoicesIn) + " Invoices posted.",System.Windows.Forms.MessageBoxButtons.OK,"",false);
        }

        public static void PostTransactions()
        {
            Transactiontemp[] TempTrans = Transactiontemp.load();
            Transactions transaction;
            int cnt = 0;
            int notPosted = 0;
            foreach (Transactiontemp tran in TempTrans)
            {
                transaction = new Transactions();
                CopyObjects.CopyProperties(tran, transaction);
                if (transaction.save())
                {
                    cnt++;
                    tran.delete(tran.IndexNumber.ToString());
                }
                else
                    notPosted++;
            }
            string msg = cnt.ToString() + " transactions posted to G/L";
            if (notPosted > 0)
                msg += "\r\n" + notPosted.ToString() + "transactions NOT posted.";
            MessageBoxCustom.Show(msg);
        }
        static void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string[] s;

            if (InvoicesIn > 0)
            {
                Report rpt = new Report("Invoice Posting Report", "Courier New", 12);
                rpt.LeftMargin = 10;
                rpt.TopMargin = 10;
                rpt.PrintPageLine("INVOICE POSTING REPORT");
                rpt.PrintPageLine(DateTime.Now.ToShortDateString());
                rpt.PrintPageLine(""); rpt.PrintPageLine("");
                rpt.PrintPageLine("");
                rpt.PrintPageLine("Invoices Processed - Count: " + InvoicesIn.ToString());
                rpt.PrintPageLine("============");

                rpt.PrintPageLine(""); rpt.PrintPageLine("");

                rpt.PrintPageLine("CHECKS NOT PROCESSED - Count: " + ChecksNotPosted.ToString());
                rpt.PrintPageLine("============");
                if (Failure.Count > 0)
                {
                    for (int i = 0; i < Failure.Count; i++)
                    {
                        {
                            s = ((string)Failure[i]).Split('|');
                            rpt.PrintPageLine(s);
                            /*
                            if (s.Length >= 5)
                            {
                                rpt.PrintPageLine(s[0].PadRight(9), false);
                                rpt.PrintPageLine(s[1].PadRight(32), false);
                                rpt.PrintPageLine(s[2].PadRight(21), false);
                                rpt.PrintPageLine(s[3]);
                                if (s.Length == 5)
                                    rpt.PrintPageLine(s[4]);
                                if (s.Length == 6)
                                    rpt.PrintPageLine(s[5]);
                                rpt.PrintPageLine("");
                            }
                            */
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.DisplayDocument();
            }

        }
        public static void scanImmediate(bool PrePaid, WinglGlobal.PostInvoice_type invpGlobal, string WindocLocation, bool journalScan = false)
        {
            bool r;
            int p = GlobalShared.FindRunningProcess("windoc");
            if (p > 0)
            {
                if (MessageBoxCustom.Show("Windoc is already running. Close it now?.", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (!GlobalShared.CloseRunningProcess(p))
                    {
                        MessageBoxCustom.Show("Unable to close Windoc.");
                        return;
                    }
                }
                else
                    return;
            }
            string uniqueID = DateTime.Now.ToString("MMddyyyyhhmm");
            System.Diagnostics.Process exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = WindocLocation.Trim() + "\\windoc.exe";
            exProc.StartInfo.Arguments = String.Format("{0} \"{1}\" \"{2}\"", "wingl-runOnce", uniqueID, WinglGlobal.scanInvoice(uniqueID, invpGlobal, false, PrePaid, false, "", journalScan));
            try
            {
                r = exProc.Start();
            }
            catch (System.Exception)
            {
            }

        }

        public static string scanInvoice(string uniqueID, PostInvoice_type invpGlobal, bool Batch = false, bool PrePaid = false, bool db = true, string WindocDatabase = "", bool journalScan = false)
        {
            //*****************************************
            string s = null;

            s = invpGlobal.Company.ToString("00") + ";" + invpGlobal.Department.ToString("000") + ";" + (journalScan ? SCAN_MANUAL : (PrePaid ? SCAN_PAID : SCAN_UNPAID)) + ";" + invpGlobal.vendor.ToString();
            s += ";" + invpGlobal.invnumber.Trim() + ";" + invpGlobal.date.ToShortDateString();
            s += ";" + (string)(PrePaid ? invpGlobal.CheckDate.ToShortDateString() : invpGlobal.dueDate.ToShortDateString()) + ";.bmp;" + invpGlobal.vName.Trim() + ": " + invpGlobal.amountpaid.ToString("###,###,##0.00") + (PrePaid ? ": CHK " + invpGlobal.CheckNumber.ToString() : "");
            if (db)
            {
                string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocDatabase + ";Mode=Share Deny None";
                OleDbConnection CN = new OleDbConnection(WindocConnectionString);
                DocInterface rss = new DocInterface(CN);

                rss.prog_id = "wingl" + (Batch ? "-batch" : "");
                rss.app_id = uniqueID;
                rss.desc = s;
                if (!rss.save())
                    MessageBoxCustom.Show("Failed to save WinDOC Interface record.");
                rss = null;
            }

            return s;
        }

        static void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            results.Close();
        }

        //******************************************************************************
        /// <summary>
        /// Takes the InvoiceKey of one invoice and does all of the
        /// posting: GL, Check Register, etc.
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="postflag"></param>
        /// <param name="Batch"></param>
        /// <param name="CashAcct"></param>
        /// <param name="desc"></param>
        /// <param name="Invoice"></param>
        /// <param name="docOpen"></param>
        /// <param name="scan"></param>
        /// <returns>0 if good, -1 means no distribution records were found, and
        ///           -2 means another error occured</returns>
        public static short Post_Invoice(int inv, short postflag, int Batch, int CashAcct, string desc, PostInvoice_type Invoice, bool docOpen, bool scan, bool batchProcess = false)
        {
            object i = null;
            object fi = null;
            //******************************************************************************


            InvoiceDistributions[] tbInvDist = null;
            Invoices tbInvoice = null;
            Vendors tbVendors = null;
            ApChecks tbCheckReg = null;
            InvoiceHistory tbInvHist = null;
            string xsql = null;
            short rc = 0;
            int seq = 0;
            decimal totCredit = default(decimal);
            int xdpt = 0;
            int xcmp = 0;
            decimal disc = default(decimal);
            string xjrn = null;
            string GLDesc = null;

            if (Invoice.payroll)
                postflag = 1;
            postflag = 1;
            totCredit = 0;
            seq = 0;
            tbInvoice = Invoices.load(inv);
            if (tbInvoice != null  && Invoice.CheckNumber > 0 && Invoice.CheckDate != null && Invoice.CheckDate > GlobalShared.NULL_DATE) {
                tbVendors = Vendors.load(tbInvoice.VendorNumber);
                if (tbVendors != null)
                {
                    GLDesc = tbVendors.VendorName.Trim();
                    tbVendors.CurrentPurchases += Invoice.amountpaid;
                    tbVendors.save();
                } else {
                    GLDesc = "Vendor# " + tbInvoice.VendorNumber;
                }
                GLDesc = GLDesc + ", Invoice# " + (tbInvoice.Prefix != null && !tbInvoice.Prefix.Trim().Equals("") ? tbInvoice.Prefix.Trim() + " " : "") + tbInvoice.InvoiceNumber + (tbInvoice.Suffix != null && !tbInvoice.Suffix.Trim().Equals("") ? " " + tbInvoice.Suffix.Trim() : "");
                tbVendors = null;
                disc = tbInvoice.DiscountAmount;
                xsql = "SELECT * FROM InvoiceDistributions WHERE InvoiceKey = " + inv;
                tbInvDist = InvoiceDistributions.loadByInvoice(inv, "gl_InvoiceDistributions");
                if (tbInvDist != null && tbInvDist.Length > 0) {

                   if (!ApChecks.TestForDupeSequence(tbInvoice.Company, tbInvoice.Department, tbInvoice.CheckNumber, 0))
                        PostCheck(tbInvoice, ref seq);
                    InvoiceDistributions dist = null;
                    foreach (InvoiceDistributions Dist in tbInvDist)
                    {
                        dist = Dist;
                        xdpt = Dist.Department;
                        xcmp = Dist.Company;
                        xjrn = (Dist.Journal == null ? " " : Dist.Journal).PadRight(20);
                        if (xjrn == null || xjrn.Trim().Equals(""))
                            xjrn = "*";
                        PostDebit(Dist, tbInvoice, xjrn, Batch, GLDesc, disc, ref seq);                     // Credit for each debit?
                        if (postflag == 0) {
                            PostCredit(Dist, tbInvoice, xjrn, Batch, GLDesc, disc, seq, GLCashAcct, postflag, totCredit);
                        } else {
                            // accumulate credits
                            totCredit += Dist.Amount - disc;
                        }
                        disc = 0;
                        Dist.delete(Dist.Id.ToString());
                    }
                    if (postflag == 1) {
                        PostCredit(dist, tbInvoice, xjrn, Batch, GLDesc, disc, seq, GLCashAcct, postflag, totCredit);
                    }
                    tbInvoice.AmountPaid = Invoice.amountpaid;
                    tbInvoice.AmountToPay = Invoice.amounttopay;
                    tbInvoice.Batch = Invoice.Batch;
                    tbInvoice.CheckNumber = Invoice.CheckNumber;
                    tbInvoice.DateLastPaid = Invoice.datelastpaid;
                    tbInvoice.CheckDate = Invoice.CheckDate;
                    tbInvoice.PayFlag = Invoice.payflag;
                    tbInvoice.save(true);
                    if (tbInvoice.AmountPaid == tbInvoice.Amount - tbInvoice.DiscountAmount)
                    {
                        tbInvHist = new InvoiceHistory();
                        CopyObjects.CopyProperties(tbInvoice, tbInvHist);

                        tbInvHist.save();

                        tbInvHist = null;
                        if (scan && !batchProcess && WinDocPath != null)
                        {
                            docInvoice(tbInvoice, true, WinDocPath.Trim() + "\\windoc.mdb", true,"", Invoice);
                            PostChecks = true;
                        }

                        // Moved from frmAPLaser 10/27/2017
                        // Test for Batch Invoice 06/06/2019
                        if (tbInvoice.Batch == null || tbInvoice.Batch <= 0)
                        {
                            Memos tblMemo = Memos.load(tbInvoice.MemoId);
                            if (tblMemo != null && !tblMemo.Keep)
                            {
                                tblMemo.delete(tblMemo.MemoId.ToString());
                                tblMemo = null;
                            }
                        }
                        //

                        tbInvoice.delete(tbInvoice.InvoiceKey.ToString());
                    }
                    rc = 0;
                } else {
                    xsql = " NOT POSTED (no distribution records)" + "\r\nInvoice# " + Invoice.invnumber + " InvoiceKey " + inv.ToString() + ", Vendor# " + Invoice.vendor;
                    xsql = xsql + " Check# " + Invoice.CheckNumber + " Company " + tbInvoice.Company + ", Department " + tbInvoice.Department;
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, xsql, false);
                    Failure.Add(xsql);
                    rc = -1;
                }
            }

            return rc;

        }

        static private void PostDebit(InvoiceDistributions tbInvDist, Invoices Invoice, string xjrn, int Batch, string GLDesc, decimal disc, ref int seq)
        {

            // to GL
            ApTrans tbTran = new ApTrans();
            tbTran.BatchNumber = Batch;
            tbTran.Company = (short)tbInvDist.Company;
            tbTran.Department = (short)tbInvDist.Department;
            tbTran.Account = tbInvDist.Account;
            tbTran.Date = Invoice.CheckDate;
            tbTran.Amount = tbInvDist.Amount - disc;
            tbTran.Description = GLDesc.Left(60);
            tbTran.Journal = xjrn;
            tbTran.CheckNumber = Invoice.CheckNumber;
            tbTran.save();
            tbTran = null;

            // to Checks
            ApChecks tbCheckReg = new ApChecks();
            tbCheckReg.Company = (short)Invoice.Company;
            tbCheckReg.Department = (short)Invoice.Department;
            tbCheckReg.DistCompany = (short)tbInvDist.Company;
            tbCheckReg.DistDepartment = (short)tbInvDist.Department;
            tbCheckReg.CheckNumber = Invoice.CheckNumber;
            tbCheckReg.Sequence = (short)seq;
            tbCheckReg.Date = Invoice.CheckDate;
            tbCheckReg.Amount = tbInvDist.Amount - disc;
            tbCheckReg.VendorAccount = tbInvDist.Account;

            int count = 0;
            int max = 20;

            while (ApChecks.TestForDupeSequence(tbCheckReg.Company, tbCheckReg.Department, tbCheckReg.CheckNumber, seq))
            {
                seq++;
                count++;
                if (count > max)
                {
                    string msg = "DISTRIBUTION NOT POSTED: Max retries exceeded" + "\r\nInvoice# " + Invoice.InvoiceNumber + ", VendorNumber " + Invoice.VendorNumber;
                    msg += "\r\nCheck# " + Invoice.CheckNumber + ", Company " + Invoice.Company + ", Departement " + Invoice.Department;
                    msg += "Distribution Account " + ", Description ";
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, msg, false);
                    Failure.Add(msg);
                    tbCheckReg = null;
                    return;
                }
            }
            tbCheckReg.Sequence = (short)seq;
            tbCheckReg.save();
            tbCheckReg = null;
            seq = seq + 1;
        }

    static private void PostCredit(InvoiceDistributions tbInvDist, Invoices Invoice, string xjrn, int Batch, string GLDesc, decimal disc, int seq, int CashAcct, int postflag, decimal totCredit)
    {

        ApTrans tbTran = new ApTrans();
        tbTran.BatchNumber = Batch;
        tbTran.Company = (short)Invoice.Company;
        tbTran.Department = (short)Invoice.Department;
        tbTran.Account = CashAcct;
        tbTran.Date = Invoice.CheckDate;
        if (postflag == 0)
        {
            tbTran.Amount = -(tbInvDist.Amount - disc);
        }
        else
        {
            tbTran.Amount = -(totCredit - disc);
            // ?? xsql = desc; ??
            totCredit = 0;
        }
        tbTran.Description = GLDesc.Left(60);
        tbTran.Journal = xjrn;
        tbTran.CheckNumber = Invoice.CheckNumber;
        tbTran.save();
        tbTran = null;
    }

    static private void PostCheck(Invoices Invoice, ref int seq)
    {


        ApChecks tbCheckReg = new ApChecks();
        tbCheckReg.Company = (short)Invoice.Company;
        tbCheckReg.Department = (short)Invoice.Department;
        tbCheckReg.CheckNumber = Invoice.CheckNumber;
        tbCheckReg.Sequence = (short)seq;
        tbCheckReg.Date = Invoice.CheckDate;
        tbCheckReg.Amount = Invoice.AmountPaid;
        tbCheckReg.VendorAccount = Invoice.VendorNumber;

        int count = 0;
        int max = 20;
        bool notSaved = true;
        while (notSaved && count < max)
        {
            if (ApChecks.TestForDupeSequence(Invoice.Company, Invoice.Department, Invoice.CheckNumber, seq))
            {
                seq++;
                count++;
                /*
                // add amount to existing check record
                string xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE Company = " + Invoice.Company.ToString() + " AND Department = " + Invoice.Department.ToString();
                xsql = xsql + " AND CheckNumber = " + Invoice.CheckNumber + " AND Sequence = 0";
                ApChecks[] tbCheckRegDup = ApChecks.LoadQuery(xsql);
                if (tbCheckRegDup != null && tbCheckRegDup.Length > 0)
                {
                    tbCheckRegDup[0].Net += Invoice.AmountPaid;
                    tbCheckRegDup[0].Date = Invoice.CheckDate;
                    tbCheckRegDup[0].save();
                    tbCheckRegDup = null;
                    tbCheckReg.IsDupe = false;
                }
                else
                {
                    string msg = " CHECK NOT POSTED. (Duplicate check number found, error occured trying to update)" + "\r\nInvoice# " + Invoice.InvoiceNumber + ", Vendor# " + Invoice.VendorName;
                    msg += " Check# " + Invoice.CheckNumber + "\r\nCompany " + Invoice.Company.ToString() + ", Department " + Invoice.Department.ToString();
                    //GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, msg, false);
                    Failure.Add(msg);
                }*/
        }
                else
                {
                    tbCheckReg.save();
                    notSaved = false;
                }
            }
            if (notSaved)
            {
                string msg = " CHECK NOT POSTED. (Sequence exceeded Maximum of " + max.ToString() + ") \r\nInvoice# " + Invoice.InvoiceNumber + ", Vendor# " + Invoice.VendorName;
                msg += " Check# " + Invoice.CheckNumber + "\r\nCompany " + Invoice.Company.ToString() + ", Department " + Invoice.Department.ToString();
                //GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, msg, false);
                Failure.Add(msg);

            }
            tbCheckReg = null;
            seq++;

        }

        public static void docInvoice(Invoices tbl, bool paid, string WindocDatabase, bool edit = false, string oldInvoice = "", PostInvoice_type invp = new PostInvoice_type())
        {
            object rc = null;
            string s = null;


            string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocDatabase + ";Mode=Share Deny None";
            OleDbConnection CN = new OleDbConnection(WindocConnectionString);
            DocInterface rss = new DocInterface(CN);
            uniqueID = Convert.ToString(DateTime.Now);

            //string invoiceNumber = (tbl.Prefix != null && !tbl.Prefix.Trim().Equals("") ? tbl.Prefix.Trim() + " " : "") + tbl.InvoiceNumber + (tbl.Suffix != null && !tbl.Suffix.Trim().Equals("") ? " " + tbl.Suffix.Trim() : "");

            rss = new DocInterface(CN);
            rss.prog_id = "wingl";
            rss.app_id = uniqueID;
            if (edit == false || paid == true)
            {
                // Add check number to description
                s = tbl.Company + ";" + tbl.Department + ";" + (paid ? "paid;" : "unpaid;") + tbl.VendorNumber;
                s += ";" + tbl.FullInvoiceNumber.Trim() + ";" + tbl.Date;
                s += ";" + tbl.PayDueDate + ";.bmp;" + tbl.CheckNumber + ";" + invp.vName.Trim() + ":" + (tbl.Amount - tbl.DiscountAmount).ToString("###,###,##0.00");
            }
            else
            {
                s = tbl.Company + ";" + tbl.Department + ";" + "edit" + ";" + tbl.VendorNumber;
                s = s + ";" + tbl.FullInvoiceNumber.Trim() + ";" + oldInvoice;
            }
            rss.desc = s;
            rss.save();
            rss = null;

        }

        public static void deleteDocuments(int chkNum, int vndNum, short cmp, short dpt)
        {
            OleDbConnection CN = null;
            Control tblControl = Control.loadOne();
            if (tblControl == null)
                return;
            scan = tblControl.Scan;
            string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + tblControl.WindocLocation.Trim() + "\\windoc.mdb" + ";Mode=Share Deny None";
            if (scan)
            {
                WinDocPath = tblControl.WindocLocation;
                if (Main.VerifyWindocLocation(WinDocPath))
                {
                    CN = Main.GetWinDocConnection(WinDocPath, out WindocConnectionString);
                    if (CN == null || WindocConnectionString.Trim().Equals(""))
                        return;
                }
                else
                    return;

                long docNum = 0;

                InvoiceHistory[] history = InvoiceHistory.LoadByCheck(chkNum, vndNum, cmp, dpt);
                if (history != null && history.Length > 0)
                {
                    foreach (InvoiceHistory tblHist in history)
                    {
                        docNum = getDocument(tblHist.InvoiceNumber, tblHist.Prefix.Trim() + " " + tblHist.InvoiceNumber.Trim() + " " + tblHist.Suffix.Trim(),  vndNum, CN);
                        if (docNum > 0)
                        {
                            deleteDocument(docNum, WinDocPath, CN);
                        }
                    }
                    history = null;
                }
            }

        }

        public static long getDocument(string inv, string fullInv, int v, OleDbConnection CN)
        {
            Documents rsd;

            rsd = Documents.loadByInvoice(v, fullInv.Trim(), CN);
            if (rsd != null)
                return rsd.DocumentId;

            rsd = Documents.loadByInvoice(v, inv.Trim(), CN);
            if (rsd != null)
                return rsd.DocumentId;
            else
                return 0;
        }

        public static void deleteDocument(long docNum, string docPath, OleDbConnection CN)
        {
            object xsql = null;

            string filename = null;

            if (docPath.Trim().Equals(""))
            {
                return;
            }
            
            xsql = "SELECT * FROM Documents WHERE document_id = " + docNum;
            Documents[] rsd = Documents.loadById(docNum, CN);
            if (rsd == null || rsd.Length == 0)
                return;
            foreach (Documents doc in rsd)
            {
                filename = docPath + doc.Filename;
                try
                {
                    FileSystem.Kill(filename);
                } catch (Exception ex) { }
            }
            rsd = null;
            clsDbOle.QueryNoReturn("DELETE FROM " + Documents.TABLE + " WHERE " + Documents.SQL_COL_DOCUMENT_ID + " = " + docNum.ToString(), CN);
        }

        public static void PostAccountsPayable()
        {
            if (MessageBoxCustom.Show("Post Accounts Payable?\r\nThis is not reversible.", "Post Accounts Payable", System.Windows.Media.Brushes.LightCoral, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            ApTrans[] tblApTrans = null;
            Transactiontemp tblTransTemp = null;
            decimal total = default(decimal);
            object xrpt = null;
            Control[] tblControl = null;
            short GLMethod = 0;
            int CashAcct = 0;
            short xcmp = 0;
            short xdpt = 0;
            string xjrn = null;
            string BatchDesc = null;
            System.DateTime xdate = default(System.DateTime);


            tblControl = Control.load();
            GLMethod = tblControl[0].GLMethod;
            if (GLMethod == 1) {
                CashAcct = tblControl[0].GLCashAccount;
                if (CashAcct == -1) {
                    MessageBox.Show("Cannot load Accounts Payable with BATCH option without a valid Cash Account.");
                    return;
                }
                BatchDesc = (tblControl[0].GLBatchDesc == null ? "Payments" : tblControl[0].GLBatchDesc);
            }
            tblControl = null;

            total = 0;

            tblApTrans = ApTrans.load();

            if (tblApTrans == null || tblApTrans.Length == 0) {
                MessageBox.Show("No Transactions to Post.");
                return;
            }
            xcmp = tblApTrans[0].Company;
            xdpt = tblApTrans[0].Department;
            xdate = tblApTrans[0].Date;
            xjrn = tblApTrans[0].Journal;
            foreach (ApTrans tran in tblApTrans)
            {
                if (GLMethod == 1 && (xcmp != tran.Company || xdpt != tran.Department))
                {
                    if (total != 0) {
                        tblTransTemp = new Transactiontemp();
                        tblTransTemp.CompanyNumber = xcmp;
                        tblTransTemp.DepartmentNumber = xdpt;
                        tblTransTemp.AccountNumber = CashAcct;
                        tblTransTemp.Date = xdate;
                        tblTransTemp.Journal = xjrn;
                        tblTransTemp.Source = "Accounts Payable";
                        tblTransTemp.Description = BatchDesc;
                        tblTransTemp.CheckNumber = 0;
                        tblTransTemp.Amount = total;
                        tblTransTemp.save();
                    }
                    xcmp = tran.Company;
                    xdpt = tran.Department;
                    xdate = tran.Date;
                    total = 0;
                }
                if (GLMethod == 1 && tran.Account == CashAcct)
                {
                    total = total + tran.Amount;
                    xjrn = tran.Journal;
                } else {
                    tblTransTemp = new Transactiontemp();
                    tblTransTemp.CompanyNumber = tran.Company;
                    tblTransTemp.DepartmentNumber = tran.Department;
                    tblTransTemp.AccountNumber = tran.Account;
                    tblTransTemp.Date = tran.Date;
                    tblTransTemp.Journal = tran.Journal.PadLeft(20);
                    tblTransTemp.Source = "Accounts Payable";
                    tblTransTemp.Description = tran.Description;
                    tblTransTemp.CheckNumber = tran.CheckNumber;
                    tblTransTemp.Amount = tran.Amount;
                    tblTransTemp.save();
                }
                tran.delete(tran.Key.ToString());
            }
            if (GLMethod == 1 & total != 0) {
                tblTransTemp = new Transactiontemp();
                tblTransTemp.CompanyNumber = xcmp;
                tblTransTemp.DepartmentNumber = xdpt;
                tblTransTemp.AccountNumber = CashAcct;
                tblTransTemp.Date = xdate;
                tblTransTemp.Journal = xjrn;
                tblTransTemp.Source = "Accounts Payable";
                tblTransTemp.Description = BatchDesc;
                tblTransTemp.CheckNumber = 0;
                tblTransTemp.Amount = total;
                tblTransTemp.save();
            }
            tblApTrans = null;
            tblTransTemp = null;

            /*
             * Print Transaction Edit List
             */
            //TransactionEditList rpt = new TransactionEditList();
            frmReportDispatcher rpt = new frmReportDispatcher("gl_transactioneditlist", "MbcsCentral.WinGL.TransactionEditList");

        }

        public static void PostTenantReceivables()
        {
            if (MessageBoxCustom.Show("Post Tenants Receivables?\r\nThis is not reversible.", "Post Tenants Receivable", System.Windows.Media.Brushes.LightCoral, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            object xsql = null;

            Transactiontemp tblTransTemp = null;
            object xrpt = null;
            short fi = 0;
            string jrn = null;
            ArTrans[] tenTrans = ArTrans.load();
            if (tenTrans == null || tenTrans.Length == 0)
            {
                MessageBoxCustom.Show("There are no Tenant Receivables to post.");
                return;
            }

            Control tblControl = Control.loadOne();
            jrn = (tblControl.GLARJournal== null ? "*" : tblControl.GLARJournal);
            tblControl = null;

            DateTime lastDate = GlobalShared.NULL_DATE;
            short lastCompany = 0;
            short lastDepartment = 0;
            string JournalSource = "";

            foreach (ArTrans tenTran in tenTrans)
            {
                if (tenTran.Amount != 0)
                {
                    if (tenTran.Company != lastCompany)
                    {
                        JournalSource = GetNextJournalNumber();
                        lastCompany = tenTran.Company;
                        lastDepartment = tenTran.Department;
                        lastDate = tenTran.Date;

                    }
                    else if (tenTran.Department != lastDepartment)
                    {
                        JournalSource = GetNextJournalNumber();
                        lastDepartment = tenTran.Department;
                        lastDate = tenTran.Date;
                    }
                    else if (tenTran.Date.Date != lastDate.Date)
                    {
                        JournalSource = GetNextJournalNumber();
                        lastDate = tenTran.Date;
                    }
                    tblTransTemp = new Transactiontemp();
                    tblTransTemp.CompanyNumber = tenTran.Company;
                    tblTransTemp.DepartmentNumber = tenTran.Department;
                    tblTransTemp.AccountNumber = tenTran.Account;
                    tblTransTemp.Date = tenTran.Date;
                    tblTransTemp.Journal = jrn;
                    tblTransTemp.Source = JournalSource;
                    tblTransTemp.Description = tenTran.Description;
                    tblTransTemp.Amount = tenTran.Amount;
                    tblTransTemp.save();
                }
                tenTran.delete(tenTran.Key.ToString());
            }

            // Print Transaction Edit List
            frmReportDispatcher rpt = new frmReportDispatcher("gl_transactioneditlist", "MbcsCentral.WinGL.TransactionEditList");

        }

        public static string GetNextJournalNumber()
        {
            Control ctrl = Control.loadOne();
            if (ctrl != null)
            {
                int sNum = ctrl.GlSourceNumber;
                sNum++;
                ctrl.GlSourceNumber = sNum;
                ctrl.save();
                return "J-" + sNum.ToString("000000");
            }
            return "";
        }


public static decimal CheckTotal( int cmp,  int dpt,  int chk)
        {
            decimal functionReturnValue = default(decimal);
            string xsql = "";

            ApChecks tblCheck = null;
            ApChecks[] tblChecks = null;

            tblCheck = ApChecks.LoadCheckTotal(cmp, dpt, chk);
            if (tblCheck != null)
            {
                tblChecks = ApChecks.load(true, cmp, dpt, chk);
                if (tblChecks != null && tblChecks.Length > 0)
                {
                    tblChecks[0].Amount = tblCheck.CheckTotal;
                    tblChecks[0].save();
                    tblChecks = null;
                }
            }
            return tblCheck.CheckTotal;


        }

        public static int GetCheckNumber(int cmp, int dpt, bool ACH = false)
        {
            int checkNumber = 0;

            CheckNumbers tblcheck = CheckNumbers.load(cmp, dpt);
            if (tblcheck != null)
            {
                if (ACH)
                {
                    if (tblcheck.ACHCheck == null || tblcheck.ACHCheck <= 0)
                        tblcheck.ACHCheck = 900000;
                    checkNumber = tblcheck.ACHCheck;
                    tblcheck.ACHCheck++;
                    tblcheck.save();

                }
                else
                {
                    if (tblcheck.BatchCheck == null || tblcheck.BatchCheck <= 0)
                        tblcheck.BatchCheck = 800000;
                    checkNumber = tblcheck.BatchCheck;
                    tblcheck.BatchCheck++;
                    tblcheck.save();
                }
            }
            else
            {
                if (ACH)
                {
                    checkNumber = 900000;

                    tblcheck = null;

                    tblcheck = new CheckNumbers();
                    tblcheck.Company = (short)cmp;
                    tblcheck.Department = (short)dpt;
                    tblcheck.CheckNumber = 0;
                    tblcheck.ACHCheck = 900001;
                    tblcheck.save();

                }
                else
                {
                    checkNumber = 800000;

                    tblcheck = null;

                    tblcheck = new CheckNumbers();
                    tblcheck.Company = (short)cmp;
                    tblcheck.Department = (short)dpt;
                    tblcheck.CheckNumber = 0;
                    tblcheck.BatchCheck = 800001;
                    tblcheck.save();
                }
            }
            tblcheck = null;

            return checkNumber;


        }

        /*
         public static void docInvoice(Invoices tbl,  bool paid, bool docOpen, bool edit = false, string oldInvoice = "")
         {
             int rc = 0;
             string s = null;

             if (!docOpen)
             {
                 Control[] ctrl = Control.load();
                 if (ctrl[0].WindocLocation.Trim().Equals(""))
                     return;
                 Control tblControl = control[0];
                 //lst.Items.Clear();
                 string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + ctrl[0].WindocLocation + ";Mode=Share Deny None";
                 CN = new OleDbConnection(WindocConnectionString);
                 try
                 {
                     CN.Open();
                 }
                 catch (OleDbException ex)
                 {
                     GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not open connection for documents: " + ex.Message, false);
                     return;
                 }
                 uniqueID = DateTime.Now.ToString();
                 da = new OleDbDataAdapter();
                 OleDbCommand cmd = new OleDbCommand();
                 cmd.Connection = CN;
                 cmd.CommandText = "SELECT *" +
                                   " FROM interface";

                 DataSet ds = new DataSet("Windoc");
                 rss.Open("SELECT * FROM interface", cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
                         if (Err().Number != 0)
                         {
                             //UPGRADE_WARNING: Couldn't resolve default property of object cnn.CloseDB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                             cnn.CloseDB();
                             goto docInvoice_exit;
                         }
                     }
                 }
                 else
                 {
                     goto docInvoice_exit;
                 }
             }

             //UPGRADE_WARNING: Couldn't resolve default property of object rss.AddNew. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
             rss.AddNew();
             //UPGRADE_WARNING: Couldn't resolve default property of object rss(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
             rss("prog_id") = "wingl";
             //UPGRADE_WARNING: Couldn't resolve default property of object rss(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
             rss("app_id") = uniqueID;
             if (edit == false)
             {
                 // Add check number to description
                 s = tbl.Fields["Company"].Value + ";" + tbl.Fields["Department"].Value + ";" + (paid ? "paid;" : "unpaid;") + tbl.Fields["VendorNumber"].Value;
                 s = s + ";" + Strings.Trim(tbl.Fields["InvoiceNumber"].Value) + ";" + tbl.Fields["Date"].Value;
                 s = s + ";" + tbl.Fields["PayDueDate"].Value + ";.bmp;" + tbl.Fields["CheckNumber"].Value + ";" + Strings.Trim(vName) + ":" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["Net"].Value - tbl.Fields["DiscountAmount"].Value, "###,###,##0.00");
             }
             else
             {
                 s = tbl.Fields["Company"].Value + ";" + tbl.Fields["Department"].Value + ";" + "edit" + ";" + tbl.Fields["VendorNumber"].Value;
                 s = s + ";" + Strings.Trim(tbl.Fields["InvoiceNumber"].Value) + ";" + oldInvoice;
             }
             //UPGRADE_WARNING: Couldn't resolve default property of object rss(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
             rss("desc") = s;
             //UPGRADE_WARNING: Couldn't resolve default property of object rss.update. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
             rss.update();
             if (!docOpen)
             {
                 //UPGRADE_WARNING: Couldn't resolve default property of object cnn.CloseDB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                 cnn.CloseDB();
             }
             goto docInvoice_exit;
             docInvoice_error:

             LogError.MsgBox_Renamed("Error Saving Windoc interface record: " + Err().Description);
             // ERROR: Not supported in C#: ResumeStatement

             docInvoice_exit:



         }
         */
    }

    public class TransEditSortClass
    {
        public TransEditSortClass()
        { }
        public string Name { get; set; }
    }
}
