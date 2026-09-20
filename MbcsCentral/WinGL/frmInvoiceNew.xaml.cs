using ExtensionMethods;
using MbcsCentral.WinDoc;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmInvoice.xaml
    /// </summary>
    public partial class frmInvoiceNew : Window
    {

        public int InvoiceKey { get; set; }
        public bool IsBatch { get; set; }
        public int BatchNumber { get; set; }
        public bool ShowAgain { get; set; } = true;
        public string InvoiceNumber { get; set; } = "";
        public string Suffix { get; set; } = "";
        public bool InactiveVendorsOnly { get; set; } = true;

        public int LastVendor { get; set; }
        public short LastCompany { get; set; }
        public short LastDepartment { get; set; }
        public string LastInvoiceNumber { get; set; } = "";
        public DateTime LastInvoiceDate { get; set; }
        public DateTime LastPayDate { get; set; }
        public string LastMemo { get; set; }

        public bool ScanChecked { get; set; } = true;
        public bool BatchProcessing { get; set; } = false;
        public bool LastACH { get; set; } = false;


        public frmInvoiceNew frm { get; set; } = null;

        private bool IsNew = true;
        private bool IsLoading;

        private ArrayList _distributions;
        private ArrayList _departments;
        private InvoiceDistributions _distribution = null;
        private string distributionTable = "gl_InvoiceDistributions";
        bool IsChanged = false;
        bool IsDistChanged = false;
        private string invoiceTable = "gl_Invoices";
        private string oldInvoice = "";
        private Invoices _invoice = null;
        private Memos memo = null;
        private Memos[] memoList = null;
        private InvoiceDistributions[] m_distributions = null;
        private JournalNames _journalNames = null;
        WinPM.Tenants[] tenants;
        Vendors vendor = null;
        Company company = null;
        Department department = null;
        Department[] departments = null;
        Company[] companies = null;
        Accounts[] accounts = null;
        JournalNames[] journals = null;
        Accounts account = null;
        decimal TotalOfDistributions;
        int memoStatus = 0;
        bool scan;
        bool postEFT = false;
        string WindocLocation;
        OleDbConnection CN;
        Documents rss = null;
        short postflag;
        int GLCashAcct;
        int Batch;
        string desc;
        string VendorName = "";


        // Used for Adding a distribution line
        bool FillDistributions = true;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmInvoiceNew(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
        }
        public frmInvoiceNew()
        {
            InitializeComponent();
        }

        private void FrmInvoice_OnLoaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            this.BringIntoView();
            this.Topmost = true;
            cmdScanDoc.Visibility = Visibility.Collapsed;
            cmdDisplayDoc.Visibility = Visibility.Collapsed;
            cmdEditDoc.Visibility = Visibility.Collapsed;
            // Get Control table values
            Control[] control = Control.load();
            if (control != null && control.Length > 0)
            {
                postflag = control[0].GLMethod;
                GLCashAcct = control[0].GLCashAccount;
                Batch = control[0].GLBatchNumber + 1;
                desc = control[0].GLBatchDesc;
                postEFT = control[0].ImmediateEftPost;

                scan = control[0].Scan;
                if (!scan)
                {
                    CheckScan.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WindocLocation = control[0].WindocLocation.Trim();
                    if (WindocLocation == null || WindocLocation.Trim().Equals(""))
                    {
                        MessageBoxCustom.Show("Could not find Windoc Database <" + WindocLocation + ">. Invoices will not be scanned.");
                        scan = false;
                    }
                    else
                    {
                        if (!WindocLocation.EndsWith("\\"))
                            WindocLocation += "\\";
                        string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocLocation.Trim() + "Windoc.mdb" + ";Mode=Share Deny None";
                        CN = new OleDbConnection(WindocConnectionString);
                    }
                    CheckScan.IsChecked = ScanChecked;
                }
                control[0].save();
            }
            control = null;

            txtAmount.Text = "0";
            for (int i = 0; i < 100; i++)
                ListCheckGroup.Items.Add(i.ToString());
            ListCheckGroup.SelectedIndex = 0;

            Vendors[] vendors = Vendors.load(InactiveVendorsOnly);
            ListVendors.ItemsSource = vendors;

            //tenants = WinPM.Tenants.loadForNames(false);
            //lstTenants.ItemsSource = tenants;

            companies = Company.load(false, false, false);
            ListCompanies.ItemsSource = companies;

            accounts = WinGL.Accounts.load(false, false);
            ListAccounts.ItemsSource = accounts;

            journals = JournalNames.load();
            ListJournals.ItemsSource = journals;

            if (IsBatch)
            {
                //pnlFrequency.Visibility = Visibility.Visible;
                PnlPay.Visibility = Visibility.Collapsed;
                txtInvoiceDate.Text = "Last Paid";
                TextDue.Text = "Next Due";
                this.Title = "Batch Invoice EDIT";

                //DateInvoice.Visibility = Visibility.Collapsed;
                //DateDue.Visibility = Visibility.Collapsed;
                txtInvoiceDate.Text = "Last Paid";
                TextDue.Text = "Next Due";
                invoiceTable = InvoicesBatch.TABLE;
                distributionTable = InvBatchDistributions.TABLE;
            }
            else
            {
                pnlFrequency.Visibility = Visibility.Collapsed;
            }
            IsLoading = true;
            if (InvoiceKey > 0)
            {
                ShowAgain = false;
                IsNew = false;
                _invoice = Invoices.load(InvoiceKey, invoiceTable);
                if (_invoice == null)
                {
                    MessageBox.Show("Invoice not found.");
                    this.Close();
                    return;
                }
                CheckScan.IsChecked = false;
                CheckScan.IsEnabled = false;
                oldInvoice = _invoice.InvoiceNumber;
                GridMain.DataContext = _invoice;

                Invoices[] invoices = Invoices.loadByVendor(_invoice.VendorNumber, invoiceTable);
                if (_invoice.PayDueDate == GlobalShared.NULL_DATE)
                    CheckUseDue.IsChecked = false;
                ListInvoiceNumbers.ItemsSource = null;
                ListInvoiceNumbers.ItemsSource = invoices;
                if (!_invoice.Prefix.Trim().Equals(""))
                    chkUseDepartmentNumber.IsChecked = true;
                _invoice.InvoiceNumber = oldInvoice;
                foreach (object t in ListVendors.Items)
                {
                    if (((Vendors)t).VendorNumber == _invoice.VendorNumber)
                    {
                        ListVendors.SelectedItem = t;
                        VendorName = ((Vendors)t).VendorName;
                        ListJournals.SelectedItem = ((Vendors)t).Journal;
                        break;
                    }
                }
                foreach (object t in ListCompanies.Items)
                {
                    if (((Company)t).CompanyNumber == _invoice.Company)
                    {
                        ListCompanies.SelectedItem = t;
                        break;
                    }
                }
                if (_invoice.CheckGroup >= 0 && _invoice.CheckGroup < 100)
                    ListCheckGroup.SelectedIndex = _invoice.CheckGroup;
                GetMemo();
                //if (!IsBatch)
                // PayFlag = _invoice.PayFlag;
                LoadDistributions();


                if (_distributions != null && _distributions.Count == 1)
                {

                    //CheckSplit.IsChecked = false;
                    //ListAccounts.IsEnabled = true;
                    //ListJournals.IsEnabled = true;
                    //foreach (object t in ListAccounts.Items)
                    //{
                    //    if (((Accounts)t).Key == ((InvoiceDistributions)_distributions[0]).Account)
                    //    {
                    //        ListAccounts.SelectedItem = t;
                    //        break;
                    //    }
                    //}

                    //_journalNames = JournalNames.load(((InvoiceDistributions)_distributions[0]).Journal);
                    //if (_journalNames != null)
                    //{
                    //    foreach (object t in ListJournals.Items)
                    //    {
                    //        if (((JournalNames)t).JournalName.Trim().Equals(_journalNames.JournalName.Trim()))
                    //        {
                    //            ListJournals.SelectedItem = t;
                    //            break;
                    //        }
                    //    }
                    //}
                    /*
                    if (scan)
                    {

                        rss = new Documents(CN);
                        rss = Documents.loadByInvoice(_invoice.VendorNumber, _invoice.InvoiceNumber, CN);
                        if (rss != null)
                        {
                            cmdDisplayDoc.Visibility = Visibility.Visible;
                            cmdEditDoc.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            cmdScanDoc.Visibility = Visibility.Visible;
                        }

                    }
                    */
                }
                //else if (_distributions != null && _distributions.Count > 1)
                {
                    ListJournals.SelectedIndex = -1;
                    ListAccounts.SelectedIndex = -1;
                    ListAccounts.IsEnabled = false;
                    ListJournals.IsEnabled = false;
                    CheckSplit.IsChecked = true;
                }
                if (scan && !BatchProcessing)
                {
                    string invoiceNumber = (_invoice.Prefix.Trim().Equals("") ? "" : _invoice.Prefix.Trim() + " ") + _invoice.InvoiceNumber + (_invoice.Suffix.Trim().Equals("") ? "" : " " + _invoice.Suffix.Trim());
                    string xsql = "SELECT " + Documents.SQL_COL_DOCUMENT_ID + ", " + Documents.SQL_COL_VENDOR + "  FROM " + Documents.TABLE +
                                  " WHERE " + Documents.SQL_COL_INVOICE + " = '" + invoiceNumber + "'";
                    OleDbDataAdapter da;
                    DataSet ds = new DataSet();
                    try
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "1 - Get documents", false);
                        da = new OleDbDataAdapter(xsql, CN.ConnectionString);
                        da.Fill(ds);
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "2 - Got documents", false);
                    }
                    catch (Exception ex) { ds = null; }

                    rss = new Documents(CN);
                    //rss.DocumentId = Documents.loadByInvoiceDocId(_invoice.VendorNumber, invoiceNumber, CN);
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        bool found = false;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr[Documents.SQL_COL_VENDOR].Equals(_invoice.VendorNumber.ToString()))
                            {
                                rss.DocumentId = (int)dr[Documents.SQL_COL_DOCUMENT_ID];
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            cmdDisplayDoc.Visibility = Visibility.Visible;
                            cmdEditDoc.Visibility = Visibility.Visible;
                            cmdScanDoc.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            cmdScanDoc.Visibility = Visibility.Visible;
                            cmdDisplayDoc.Visibility = Visibility.Collapsed;
                            cmdEditDoc.Visibility = Visibility.Collapsed;

                        }
                    }
                    else
                    {
                        cmdScanDoc.Visibility = Visibility.Visible;
                        cmdDisplayDoc.Visibility = Visibility.Collapsed;
                        cmdEditDoc.Visibility = Visibility.Collapsed;
                    }

                }
            }
            else
            {
                ListAccounts.SelectedIndex = -1;
                LastInvoiceNumber = this.ListInvoiceNumbers.Text.Trim();
                ListJournals.SelectedIndex = -1;
                cmdScanDoc.Visibility = Visibility.Collapsed;
                string text = txtSuffix.Text;
                if (!text.Trim().Equals(""))
                {
                    chkUseSuffix.IsChecked = true;
                    chkSaveSuffix.IsChecked = true;
                }
                //if (LastDepartment > 0)
                //{
                //    chkHoldDepartment.IsChecked = true;
                //   chkUseDepartmentNumber.IsChecked = true;
                //}

                if (LastInvoiceDate <= GlobalShared.NULL_DATE)
                    LastInvoiceDate = DateTime.Now.Date;
                if (LastPayDate <= GlobalShared.NULL_DATE)
                    LastPayDate = DateTime.Now.Date;
                _invoice = new Invoices(invoiceTable) { Date = LastInvoiceDate, PayDueDate = LastPayDate };
                _invoice.Suffix = text;
                _invoice.Prefix = txtInvoicePrefix.Text;
                _invoice.InvoiceNumber = LastInvoiceNumber;
                if (chkDiscHold.IsChecked.Value) ;
                {
                    _invoice.DiscountDollar = chkDiscAmount.IsChecked.Value;
                    decimal discount = 0;
                    decimal.TryParse(txtDiscount.Text, out discount);
                    _invoice.Discount = discount;
                    txtDiscount.Text = discount.ToString();
                }
                if (LastCompany > 0)
                {
                    _invoice.Company = (int)LastCompany;
                    _invoice.Department = (int)LastDepartment;
                    foreach (object t in ListCompanies.Items)
                    {
                        if (((Company)t).CompanyNumber == _invoice.Company)
                        {
                            company = (Company)t;
                            ListCompanies.SelectedItem = t;
                            break;
                        }
                    }
                }
                if (LastVendor > 0)
                {
                    _invoice.VendorNumber = LastVendor;
                    NewInvoiceDistributions(LastVendor);
                }
                if (_invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
                {
                    _invoice.PrePaid = true;
                }

                /*
                if (LastDepartment > 0)
                {
                    _invoice.Department = LastDepartment;
                }
                if (LastVendor > 0)
                {
                    _invoice.VendorNumber = LastVendor;
                }
                if (LastInvoiceDate > GlobalShared.NULL_DATE)
                    _invoice.Date = LastInvoiceDate;
                if (LastPayDate > GlobalShared.NULL_DATE)
                    _invoice.PayDueDate = LastPayDate;

                if (!txtInvoicePrefix.Text.Trim().Equals(""))
                {
                    _invoice.Prefix = LastDepartment.ToString("000");
                    txtInvoicePrefix.Text = LastDepartment.ToString("000");
                }
                if (!InvoiceNumber.Trim().Equals(""))
                {
                    _invoice.InvoiceNumber = InvoiceNumber;
                    chkSaveInvoiceNumber.IsChecked = true;
                }
                if (!Suffix.Trim().Equals(""))
                {
                   _invoice.Suffix = Suffix;
                    //chkUseSuffix.IsChecked = true;
                    //chkSaveSuffix.IsChecked = true;
                }
                 */

                if (CheckMemo.IsChecked.Value && chkHoldMemo.IsChecked.Value)
                    TxtMemo.Text = LastMemo;

                if (scan)
                    CheckScan.IsEnabled = true;
                CheckScan.IsChecked = ScanChecked;
                chkACH.IsChecked = LastACH;
                _invoice.ACH = LastACH;
                DateDue.SelectedDate = _invoice.PayDueDate;
                chkACH_Checked(sender, e);
                //ListVendors.Focus();

            }

            if (_invoice != null && _invoice.InvoiceNumber != null)
            {
                // Parse InvoiceNumber
                string invoiceNumber = _invoice.InvoiceNumber;
                short dpt = 0;
                string ten = "";
                /*
                // Test for CompanyNumber prefix

                if (_invoice.Prefix != null && !_invoice.Prefix.Trim().Equals(""))
                {
                    short.TryParse(_invoice.Prefix, out dpt);
                    if (dpt > 0)
                    {
                        Department department = Array.Find(departments, c => c.DepartmentNumber == dpt);
                        if (company != null)
                        {
                            ListCompanies.SelectedItem = company;
                            //chkUseDepartmentNumber.IsChecked = true;
                        }
                    }

                }
                string[] s = invoiceNumber.Split(Invoices.CompanyDelimiter);
                switch (s.Length)
                {
                    case 2:
                        short.TryParse(s[0], out cmp);
                        if (cmp > 0)
                        {
                            invoiceNumber = invoiceNumber.Substring(s[0].Length + 1);
                            Company company = Array.Find(companies, c => c.CompanyNumber == cmp);
                            if (company != null)
                            {
                                ListCompanies.SelectedItem = company;
                                chkUseCompanyNumber.IsChecked = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
                */

                if (_invoice.Suffix != null && !_invoice.Suffix.Trim().Equals(""))
                    chkUseSuffix.IsChecked = true;


                IsLoading = false;
            }
            GlobalShared.Log.Log((int)LogClass.logType.Info, "7 - Finished loading.", false);

            GridMain.DataContext = _invoice;
            txtDistTable.Text = distributionTable;
            IsLoading = false;
        }

        private void LoadDistributions()
        {
            if (!IsNew)
            {
                if (IsBatch)
                    distributionTable = "gl_InvBatchDistributions";
                else
                    distributionTable = "gl_InvoiceDistributions";
                txtDistTable.Text = distributionTable;
                //TextVendorName.Text = (string)((Vendors)ListVendors.SelectedItem).VendorName;
                //TextInvoiceNumber.Text = _invoice.InvoiceNumber;
                //DateInvoiceDate.SelectedDate = _invoice.Date;
                //AmountInvoice.Text = string.Format("{0:C}", _invoice.Amount);
                _distributions = InvoiceDistributions.loadByInvoiceAsList(_invoice.InvoiceKey, distributionTable);
            }
            Company[] cmps = Company.load();
            WinGL.Accounts[] acts = WinGL.Accounts.load();
            JournalNames[] journalNames = JournalNames.load();
            VirtualizingStackPanel.SetIsVirtualizing(GridDistributions, false);

            System.Windows.Data.CollectionViewSource companies =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("Companies")));
            companies.Source = cmps;
            System.Windows.Data.CollectionViewSource accounts =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("Accounts")));
            accounts.Source = acts;
            System.Windows.Data.CollectionViewSource journals =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("Journals")));
            journals.Source = journalNames;

            if (_distributions != null && _distributions.Count != 0)
            {
                Department[] depts = null;
                foreach (InvoiceDistributions dist in _distributions)
                {
                    if (dist.Company != null && dist.Company > 0)
                    {
                        // if (_departments == null)
                        //    _departments = new ArrayList();
                        depts = Department.loadByCompany(dist.Company);
                        ((InvoiceDistributions)dist).DepartmentList = depts;
                    }
                    ((InvoiceDistributions)dist).CompanyList = cmps;
                    ((InvoiceDistributions)dist).AccountList = acts;
                    ((InvoiceDistributions)dist).JournalList = journalNames;
                }
                GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
                TotalDistributions();
            }


            /*
            if (_distributions != null && _distributions.Count == 1)
            {
                CheckSplit.IsChecked = false;
                ListAccounts.IsEnabled = true;
                ListJournals.IsEnabled = true;
                foreach (object t in ListAccounts.Items)
                {
                    if (((Accounts)t).Key == ((InvoiceDistributions)_distributions[0]).Account)
                    {
                        ListAccounts.SelectedItem = t;
                        break;
                    }
                }
                _journalNames = JournalNames.load(((InvoiceDistributions)_distributions[0]).Journal);
                if (_journalNames != null)
                {
                    foreach (object t in ListJournals.Items)
                    {
                        if (((JournalNames)t).JournalName.Trim().Equals(_journalNames.JournalName.Trim()))
                        {
                            ListJournals.SelectedItem = t;
                            break;
                        }
                    }
                }

            }
            */
            /*
            DataGridRow gr;
            var itemSource = GridDistributions.ItemsSource as IEnumerable;
            if (itemSource != null)
            {
                for (int i = 0; i < GridDistributions.Items.Count; i++)
                {

                    var row = GridDistributions.Items[i]; //.ItemContainerGenerator.ContainerFromIndex(i);
                    try
                    {
                        //object obj = row.FindResource("ListCompanies");
                    }
                    catch { }
                }
            }
            */

        }

        private void NewInvoiceDistributions(int vendorNumber = 0)
        {
            //AllocatedFlag = False : TotalAmount = 0

            int r = 0;
            if (vendorNumber == 0)
                vendorNumber = vendor.VendorNumber;
            VendorCategories[] tblVendorCats = VendorCategories.loadByVendor(vendorNumber, true);
            if (tblVendorCats == null || tblVendorCats.Length == 0)
            {
                _distributions = new ArrayList();
                _distribution = new InvoiceDistributions(distributionTable);
                _distribution.CompanyList = companies;
                _distribution.DepartmentList = departments;
                _distribution.AccountList = accounts;
                _distribution.JournalList = journals;
                if (ListCompanies.SelectedIndex >= 0)
                {
                    _distribution.CompanyName = ((Company)ListCompanies.SelectedItem).Name;
                    _distribution.Company = ((Company)ListCompanies.SelectedItem).CompanyNumber;
                }
                if (ListDepartments.SelectedIndex >= 0)
                {
                    _distribution.Department = ((Department)ListDepartments.SelectedItem).DepartmentNumber;
                    _distribution.DepartmentName = ((Department)ListDepartments.SelectedItem).Name;
                }
                if (ListAccounts.SelectedIndex >= 0)
                {
                    _distribution.Account = ((Accounts)ListAccounts.SelectedItem).AccountNumber;
                    _distribution.AccountName = ((Accounts)ListAccounts.SelectedItem).Description;
                }
                decimal amount = 0;
                decimal.TryParse(txtAmount.Text, out amount);
                _distribution.Amount = amount;
                _distributions.Add(_distribution);
                GridDistributions.ItemsSource = _distributions;
                return;
            }

            _distributions = null;
            _distributions = new ArrayList();
            GridDistributions.ItemsSource = null;

            bool noMatch = true;
            r = tblVendorCats.Length;
            CheckSplit.IsChecked = (r > 1 ? true : false);
            foreach (VendorCategories cat in tblVendorCats)
            {
                InvoiceDistributions dist = new InvoiceDistributions();
                dist.Company = (short)(cat.Company <= 0 ? 0 : cat.Company);
                if (dist.Company > 0)
                {
                    Company cmp = Company.loadByCompany((int)dist.Company);
                    if (cmp != null)
                        dist.CompanyName = cmp.Name;
                }
                if (dist.Department > 0)
                {
                    Department dpt = Department.loadByDepartment(dist.Department);
                    if (dpt != null)
                        dist.DepartmentName = dpt.Name;
                }
                dist.Account = cat.Account;
                dist.AccountName = cat.AccountName;
                dist.Department = (short)(cat.Department <= 0 ? 0 : cat.Department);
                dist.Amount = cat.Amount;
                if (cat.JournalName == null || cat.JournalName.Trim().Equals(""))
                {
                    dist.Journal = vendor.Journal;
                    cat.JournalName = vendor.Journal;
                    cat.save();
                }
                dist.Journal = cat.JournalName;
                _distributions.Add(dist);
            }
            //GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
            LoadDistributions();
        }

        private void GetDepartments(short cmp)
        {
            departments = Department.loadByCompany(cmp);
            ListDepartments.ItemsSource = departments;
            if (departments != null && departments.Length > 0 && _invoice != null)
            {
                foreach (object t in ListDepartments.Items)
                {
                    if (((Department)t).DepartmentNumber == _invoice.Department)
                    {
                        ListDepartments.SelectedItem = t;
                        break;
                    }
                }

            }
        }

        private void FrmInvoice_OnClosing(object sender, CancelEventArgs e)
        {
            if (IsChanged || IsDistChanged)
            {
                switch (MessageBox.Show("Changes have been made. Save them now?", "", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, new RoutedEventArgs());
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            Main.saveForm(this);
            InvoiceDistributions.TABLE = WinglGlobal.BASE_INVOICE_DISTRIBUTION_TABLE;
            //if (frm != null)
            //  frm.ShowDialog();
        }

        private void ListCompanies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox lst = (ComboBox)sender;
            int i = lst.SelectedIndex;
            if (i >= 0)
            {
                company = (Company)lst.SelectedItem;
                GetDepartments(company.CompanyNumber);
                if (ListDepartments.Items.Count == 1)
                    ListDepartments.SelectedIndex = 0;
                if (_distributions == null || _distributions.Count == 0)
                    InitialDistribution();

                foreach (InvoiceDistributions dist in _distributions)
                {
                    dist.Company = company.CompanyNumber;
                    dist.CompanyName = company.Name;
                    dist.DepartmentList = departments;
                    dist.AccountList = accounts;
                    dist.JournalList = journals;
                }
                string inv = ListInvoiceNumbers.Text;
                Invoices[] invoices = null;
                ListInvoiceNumbers.ItemsSource = null;
                if (vendor != null)
                {
                    invoices = Invoices.loadByVendor(vendor.VendorNumber, invoiceTable);
                    GetMemo(true);
                }
                ListInvoiceNumbers.ItemsSource = invoices;

                ListInvoiceNumbers.Text = inv;
            }
            else
                company = null;
        }

        private void ListDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!FillDistributions)
                return;
            ComboBox lst = (ComboBox)sender;
            int i = lst.SelectedIndex;
            if (i >= 0)
            {
                department = (Department)lst.SelectedItem;
                //if (InvoiceKey > 0)
                GetMemo(true);
                if (_distributions == null || _distributions.Count == 0)
                    InitialDistribution();
                foreach (InvoiceDistributions dist in _distributions)
                {
                    dist.Department = department.DepartmentNumber;
                    dist.DepartmentName = department.Name;
                }
                GridDistributions.ItemsSource = _distributions;
            }
            else
                department = null;
        }

        private void ListAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox lst = (ComboBox)sender;
            int i = lst.SelectedIndex;
            if (i >= 0)
            {
                account = (Accounts)lst.SelectedItem;
                if (_distributions == null || _distributions.Count == 1)
                    InitialDistribution();
            }
            else
                account = null;
        }

        private void InitialDistribution()
        {
            if (_distributions == null || _distributions.Count == 0)
            {
                if (_distributions == null)
                    _distributions = new ArrayList();
                _distributions.Add(new InvoiceDistributions());
            }
            if (_distributions.Count > 1)
                return;
            GridDistributions.ItemsSource = null;
            if (company != null)
            {
                ((InvoiceDistributions)_distributions[0]).Company = company.CompanyNumber;
                ((InvoiceDistributions)_distributions[0]).CompanyName = company.Name;
            }
            if (department != null)
            {
                ((InvoiceDistributions)_distributions[0]).Department = department.DepartmentNumber;
                ((InvoiceDistributions)_distributions[0]).DepartmentName = department.Name;
                ((InvoiceDistributions)_distributions[0]).SelectedDepartment = department;
            }
            if (account != null)
            {
                ((InvoiceDistributions)_distributions[0]).Account = account.Key;
                ((InvoiceDistributions)_distributions[0]).AccountName = account.Description;
            }
            ((InvoiceDistributions)_distributions[0]).Amount = _invoice.Amount;
            GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
            TotalDistributions();
        }
        private void GetMemo(bool NewInvoice = false)
        {
            if (company != null && department != null)
            {
                memoList = Memos.loadByVendorCompDept(_invoice.VendorNumber, company.CompanyNumber, department.DepartmentNumber);
                lstMemos.ItemsSource = memoList;
            }
            if (!NewInvoice)
            {
                memo = Memos.load(_invoice.MemoId);
                if (memo == null)
                    CheckMemo.IsChecked = false;
                else
                {
                    CheckMemo.IsChecked = true;
                    TxtMemo.Text = memo.Memo;
                    memoStatus = 1;
                }
            }
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            //WinglGlobal.PostInvoice_type invpGlobal = new WinglGlobal.PostInvoice_type();
            // Get Control table values
            Control[] control = Control.load();
            if (control == null || control.Length == 0)
            {
                MessageBoxCustom.Show("Control Table Error.  Cannot save any invoices...");
                return;
            }
            if (_invoice == null)
            {

                MessageBoxCustom.Show("Control Table Error.  Cannot save any invoices...");
                return;
            }
            short postflag = control[0].GLMethod;
            int GLCashAcct = control[0].GLCashAccount;
            control = null;

            decimal amount = 0;
            decimal.TryParse(txtAmount.Text, out amount);
            if (amount == 0)
            {
                if (MessageBoxCustom.Show("Invoice amount is zero. Save anyway?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                    return;
            }
            Vendors vendor = (Vendors)ListVendors.SelectedItem;
            if (vendor == null)
            {
                MessageBoxCustom.Show("Vendor not specified.");
                return;
            }
            if (company == null || department == null)
            {
                MessageBoxCustom.Show("Company and or Department not specified.");
                return;
            }
            if (IsBatch)
                _invoice.Batch = BatchNumber;
            else
            {
                if (InHistory())
                    return;
                //_invoice.PayFlag = (short)PayFlag; // (short)ListPayFlag.SelectedIndex;
            }

            if (!CheckUseDue.IsChecked.Value)
                _invoice.PayDueDate = GlobalShared.NULL_DATE;

            _invoice.Company = company.CompanyNumber;
            _invoice.Department = department.DepartmentNumber;

            if (CheckMemo.IsChecked.Value && TxtMemo.Text.Trim() != "")
            {
                if (memo == null || (chkKeepMemo.IsChecked.Value && !memo.Memo.Trim().Equals(TxtMemo.Text.Trim())))
                    memo = new Memos()
                    {
                        CheckGroup = _invoice.CheckGroup,
                        BatchNumber = _invoice.Batch,
                        CompanyNumber = _invoice.Company,
                        DepartmentNumber = _invoice.Department,
                        VendorNumber = _invoice.VendorNumber
                    };
                memo.Keep = chkKeepMemo.IsChecked.Value;
                memo.Memo = TxtMemo.Text.Left(120);
                memo.save();
                _invoice.MemoId = memo.MemoId;
            }
            else if (memo != null && _invoice.MemoId > 0)
            {
                memo.delete(_invoice.MemoId.ToString());
                _invoice.MemoId = 0;
                _invoice.Memo = TxtMemo.Text;
            }

            //_invoice.InvoiceNumber = (txtInvoicePrefix.Text.Trim().Length > 1 ? txtInvoicePrefix.Text.Trim() : "" ) + txtInvoiceProper.Text.Trim() + (txtInvoiceSuffix.Text.Trim().Length > 1 ? txtInvoiceSuffix.Text.Trim() : "") ;

            _invoice.Prefix = (chkUseDepartmentNumber.IsChecked.Value && txtInvoicePrefix.Text.Trim().Length > 0 ? txtInvoicePrefix.Text.Trim() : "");
            _invoice.Suffix = (chkUseSuffix.IsChecked.Value && txtInvoiceSuffix.Text.Trim().Length > 0 ? txtInvoiceSuffix.Text.Trim() : "");
            if (InvoiceHistory.InHistory(_invoice.VendorNumber, _invoice.InvoiceNumber, _invoice.Prefix, _invoice.Suffix))
                return;

            if (!_invoice.save())
            {
                if (_invoice.IsDupe)
                    MessageBoxCustom.Show("Duplicate Final Invoice Number.  Not saved.");
                else
                    MessageBoxCustom.Show("Error saving invoice: \r\n\r\n" + _invoice.ErrorMessage);
                return;
            }

            InvoiceKey = _invoice.InvoiceKey;
            CmdSaveDist_Click(sender, e);
            TotalDistributions();
            if (TotalOfDistributions == _invoice.Amount)
                _invoice.AllocationFlag = true;
            else
            {
                if (MessageBoxCustom.Show("Distributions are incomplete. Continue to save this invoice?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                    return;
                _invoice.AllocationFlag = false;
            }

            _invoice.save();
            WinglGlobal.invpGlobal.amountpaid = _invoice.Amount - _invoice.DiscountAmount;
            WinglGlobal.invpGlobal.amounttopay = _invoice.AmountToPay;
            WinglGlobal.invpGlobal.Batch = BatchNumber;
            WinglGlobal.invpGlobal.Company = _invoice.Company;
            WinglGlobal.invpGlobal.Department = _invoice.Department;
            WinglGlobal.invpGlobal.vendor = _invoice.VendorNumber;
            WinglGlobal.invpGlobal.vName = vendor.VendorName;
            WinglGlobal.invpGlobal.invnumber = (_invoice.Prefix != null && !_invoice.Prefix.Trim().Equals("") ? _invoice.Prefix.Trim() + " " : "") + _invoice.InvoiceNumber + (_invoice.Suffix != null && !_invoice.Suffix.Trim().Equals("") ? " " + _invoice.Suffix.Trim() : "");
            ;
            if (_invoice.Date > GlobalShared.NULL_DATE)
                WinglGlobal.invpGlobal.date = _invoice.Date;
            if (_invoice.CheckNumber > 0)
                WinglGlobal.invpGlobal.CheckNumber = _invoice.CheckNumber;
            if (_invoice.PayDueDate > GlobalShared.NULL_DATE)
                WinglGlobal.invpGlobal.dueDate = _invoice.PayDueDate;
            if (IsNew && scan && CheckScan.IsChecked.Value && !IsBatch)
            {
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
                }
                scanImmediate((bool)(_invoice.CheckNumber > 0 && _invoice.CheckDate > GlobalShared.NULL_DATE ? true : false), WinglGlobal.invpGlobal);
            }
            if (!IsNew && CheckScan.IsChecked.Value && scan && !IsBatch && BatchNumber == 9)
            {
                WinglGlobal.docInvoice(_invoice, false, WindocLocation + "Windoc.mdb", false, oldInvoice);
            }
            //if (!IsBatch && (chkEFT.IsChecked.Value || chkACH.IsChecked.Value) && IsNew)

            if ((chkEFT.IsChecked.Value || chkACH.IsChecked.Value) && (IsNew || _invoice.CheckNumber == 0))
            {
                _invoice.CheckNumber = WinglGlobal.GetCheckNumber(_invoice.Company, _invoice.Department, chkACH.IsChecked.Value);
                if (_invoice.CheckDate <= GlobalShared.NULL_DATE)
                    _invoice.CheckDate = DateTime.Now;

                if (chkACH.IsChecked.Value)
                {
                    if (vendor.Corporation)
                        _invoice.ACH_TYPE = NACHA_Layout.Sec_Codes[(int)NACHA_Layout.Sec_Code_Types.CCD];
                    else
                        _invoice.ACH_TYPE = NACHA_Layout.Sec_Codes[(int)NACHA_Layout.Sec_Code_Types.PPD];

                }
                _invoice.save();
            }
            /*
            if (!IsBatch && (postEFT || !chkEFT.IsChecked.Value))
            {
                if (_invoice.CheckNumber > 0 && _invoice.CheckDate > GlobalShared.NULL_DATE && _invoice.AllocationFlag)
                {
                    WinglGlobal.invpGlobal.datelastpaid = _invoice.CheckDate;
                    WinglGlobal.invpGlobal.CheckDate = _invoice.CheckDate;
                    WinglGlobal.invpGlobal.CheckNumber = _invoice.CheckNumber;
                    WinglGlobal.invpGlobal.payflag = 4;
                    WinglGlobal.Post_Invoice(_invoice.InvoiceKey, postflag, Batch, GLCashAcct, desc, WinglGlobal.invpGlobal, false, scan);
                }
            }
            */
            //CmdSaveDist_Click(sender, e);
            if (!IsBatch && ShowAgain)
            {
                frm = new frmInvoiceNew(m_ThisApp) { LastVendor = _invoice.VendorNumber, LastCompany = (short)_invoice.Company, LastDepartment = (short)_invoice.Department, LastInvoiceNumber = _invoice.InvoiceNumber, Suffix = txtSuffix.Text, LastACH = _invoice.ACH };

                frm.LastInvoiceDate = _invoice.Date;

                if (CheckUseDue.IsChecked.Value)
                    frm.LastPayDate = _invoice.PayDueDate;
                else
                    frm.LastPayDate = GlobalShared.NULL_DATE;

                frm.LastDepartment = 0;
                if (chkHoldDepartment.IsChecked.Value)
                {
                    frm.txtInvoicePrefix.Text = _invoice.Department.ToString("000");
                    frm.chkHoldDepartment.IsChecked = true;
                    if (chkUseDepartmentNumber.IsChecked.Value)
                        frm.chkUseDepartmentNumber.IsChecked = true;
                    frm.LastDepartment = (short)_invoice.Department;
                }

                frm.LastCompany = ((Company)ListCompanies.SelectedItem).CompanyNumber;
                //frm.LastDepartment = (short)_invoice.Department;
                if (chkSaveInvoiceNumber.IsChecked.Value)
                {
                    frm.InvoiceNumber = this.ListInvoiceNumbers.Text;
                    frm.ListInvoiceNumbers.Text = this.ListInvoiceNumbers.Text;
                    frm.chkSaveInvoiceNumber.IsChecked = true;
                    frm.LastVendor = ((Vendors)ListVendors.SelectedItem).VendorNumber;
                }

                frm.Suffix = "";
                if (chkSaveSuffix.IsChecked.Value)
                {
                    frm.txtSuffix.Text = txtSuffix.Text;
                    frm.chkSaveSuffix.IsChecked = true;
                    frm.chkUseSuffix.IsChecked = true;
                }

                if (CheckMemo.IsChecked.Value && chkHoldMemo.IsChecked.Value)
                {
                    frm.LastMemo = TxtMemo.Text;
                    frm.chkHoldMemo.IsChecked = true;
                    frm.CheckMemo.IsChecked = true;
                }

                if (chkDiscHold.IsChecked.Value)
                {
                    frm.txtDiscount.Text = this.txtDiscount.Text;
                    frm.chkDiscAmount.IsChecked = this.chkDiscAmount.IsChecked;
                    frm.chkDiscHold.IsChecked = this.chkDiscHold.IsChecked;
                }
                frm.CheckScan.IsChecked = CheckScan.IsChecked.Value;
                frm.LastACH = _invoice.ACH;
                chkACH_Checked(sender, e);
                //if (frm != null)
                //    frm.ShowDialog();
                this.Close();
            }

            try
            {
                this.Close();
            }
            catch { }
        }

        void scanImmediate(bool PrePaid, WinglGlobal.PostInvoice_type invpGlobal)
        {
            bool r;
            string uniqueID = DateTime.Now.ToString("MMddyyyyhhmm");
            System.Diagnostics.Process exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = WindocLocation.Trim() + "windoc.exe";
            exProc.StartInfo.Arguments = String.Format("{0} \"{1}\" \"{2}\"", "wingl-runOnce", uniqueID, WinglGlobal.scanInvoice(uniqueID, invpGlobal, false, PrePaid, false));
            try
            {
                r = exProc.Start();
            }
            catch (System.Exception ex)
            {
                return;
            }
            exProc.WaitForExit(30000);
        }


        private bool InHistory()
        {
            Invoices inv = Invoices.LoadByInvoice(_invoice.VendorNumber, _invoice.InvoiceNumber);
            if (inv != null)
            {
                Company company = Company.loadByCompany(_invoice.Company);
                Department dpt = Department.loadByDepartment((short)_invoice.Department);
                string msg = "This invoice may already have been paid to this vendor from:" + "\r\n" + "\r\n" + "Date: " + _invoice.Date.ToString() + "\r\n";
                msg += "Check# " + _invoice.CheckNumber.ToString() + "\r\n" + "Amount: " + string.Format("{0:C}", _invoice.AmountPaid);
                msg += "\r\n" + "Company: " + (company == null ? "<not found>" : company.Name) + "\r\n" + "Department: " + (dpt == null ? "<not found>" : dpt.Name);
                msg += "\r\n" + "\r\n" + "Do you wish to store this invoice anyway?";
                if (MessageBox.Show(msg, "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return true;
                else
                    return false;
            }
            return false;
        }
        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckUseDue_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckUseDue.IsChecked.Value == true && DateDue.DisplayDate == GlobalShared.NULL_DATE)
                DateDue.Text = DateTime.Now.ToShortDateString();
        }


        // *******************
        // Distribution Tab  *
        // *******************
        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //IsDistChanged = true;
            TotalDistributions();
        }

        private void TotalDistributions()
        {
            TotalOfDistributions = 0;
            if (_distributions == null || _distributions.Count == 0)
                return;
            foreach (InvoiceDistributions dist in _distributions)
            {
                if (!dist.Deleted)
                    TotalOfDistributions += dist.Amount;
            }
            TextTotal.Text = String.Format("{0:C}", TotalOfDistributions);
        }

        private void TextBoxBase_OnLostFocus(object sender, RoutedEventArgs e)
        {

            decimal amt = 0;
            decimal.TryParse(txtAmount.Text, out amt);
            if (amt != 0 && TotalOfDistributions != amt)
            {
                foreach (InvoiceDistributions dist in _distributions)
                {
                    if (dist.Amount == 0)
                    {
                        dist.Amount = amt - TotalOfDistributions;
                        break;
                    }
                }
            }

        }
        private void GridDistributions_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                IsDistChanged = true;
                var newItem = e.Row.DataContext as InvoiceDistributions;
                if (_distributions != null && !_distributions.Contains(newItem))
                {
                    _distributions.Add(newItem);
                    TotalDistributions();
                }
            }
        }


        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridCell cell = sender as DataGridCell;

                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                    //DataGridCell_Edit(sender as DataGridCell);
                }
            }
            catch (Exception)
            {
            }

        }

        private void DataGridCell_Edit(DataGridCell cell)
        {
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                            cell.IsSelected = true;
                    }
                    else
                    {
                        DataGridRow row = FindVisualParent<DataGridRow>(cell);
                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }

        }
        private void GridDistributions_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Amount":

                    if (TotalOfDistributions < _invoice.Amount)
                    {
                        for (int i = 0; i < _distributions.Count; i++)
                        {
                            if (((InvoiceDistributions)_distributions[i]).Amount == 0)
                            {
                                ((InvoiceDistributions)_distributions[i]).Amount = _invoice.Amount - TotalOfDistributions;
                                break;
                            }
                        }
                    }
                    break;
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DependencyObject dep = (DependencyObject)e.OriginalSource;
            //int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            ComboBox c = (ComboBox)sender;
            int n = GridDistributions.SelectedIndex;
            if (n < 0 || c.SelectedItem == null)
                return;

            switch (c.Tag)
            {
                case "ListAccountsDist":
                    if (_distributions == null)
                        CtxAdd_Click(sender, new RoutedEventArgs());
                    /*
                    if (_distributions.Count == 0)
                    {
                        _distributions.Add(new InvoiceDistributions());
                        Company company = (Company)ListCompanies.SelectedItem;
                        Department dept = (Department)ListDepartments.SelectedItem;
                        if (company != null)
                        {
                            ((InvoiceDistributions)_distributions[0]).Company = company.CompanyNumber;
                            ((InvoiceDistributions)_distributions[0]).CompanyName = company.Name;
                        }
                        if (dept != null)
                        {
                            ((InvoiceDistributions)_distributions[0]).Department = dept.DepartmentNumber;
                            ((InvoiceDistributions)_distributions[0]).DepartmentName = dept.Name;
                        }

                        n = 0;

                        GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
                    }
                    */
                    ((InvoiceDistributions)_distributions[n]).AccountName = ((Accounts)c.SelectedItem).Description;
                    ((InvoiceDistributions)_distributions[n]).Account = ((Accounts)c.SelectedItem).Key;
                    break;
                case "ListDepartmentsDist":
                    ((InvoiceDistributions)_distributions[n]).DepartmentName = ((Department)c.SelectedItem).Name;
                    ((InvoiceDistributions)_distributions[n]).Department = ((Department)c.SelectedItem).DepartmentNumber;
                    break;

                // Comment this case
                case "ListCompaniesDist":
                    ((InvoiceDistributions)_distributions[n]).CompanyName = ((Company)c.SelectedItem).Name;
                    ((InvoiceDistributions)_distributions[n]).Company = ((Company)c.SelectedItem).CompanyNumber;
                    short cmp = ((Company)c.SelectedItem).CompanyNumber;
                    Department[] departments = Department.loadByCompany(cmp);
                    //System.Windows.Data.CollectionViewSource departmentsSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Departments")));
                    //departmentsSource.Source = departments;
                    ((InvoiceDistributions)_distributions[n]).DepartmentList = departments;
                    if (departments == null || departments.Length == 0)
                    {

                        ((InvoiceDistributions)_distributions[n]).DepartmentName = "";
                        ((InvoiceDistributions)_distributions[n]).Department = 0;
                    }
                    else
                    {

                        ((InvoiceDistributions)_distributions[n]).DepartmentName = departments[0].Name;
                        ((InvoiceDistributions)_distributions[n]).Department = departments[0].DepartmentNumber;
                        //ListDepartments.SelectedIndex = 0;
                        ((InvoiceDistributions)_distributions[n]).SelectedDepartment = departments[0];

                    }
                    break;
                case "ListJournalsDist":
                    ((InvoiceDistributions)_distributions[n]).Journal = ((JournalNames)c.SelectedItem).JournalName;
                    break;
            }
        }

        private void CtxDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = GridDistributions.SelectedIndex;
            if (i >= 0)
            {
                if (MessageBox.Show(((((InvoiceDistributions)_distributions[i]).Deleted ? "Undelete" : "Delete")) + " this distribution?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GridDistributions.ItemsSource = null;
                    //_distributions.RemoveAt(i);
                    ((InvoiceDistributions)_distributions[i]).Deleted = !((InvoiceDistributions)_distributions[i]).Deleted;
                    GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
                    IsDistChanged = true;
                    TotalDistributions();
                }
                if (_distributions.Count == 1)
                {
                    CheckSplit.IsChecked = false;
                    ListAccounts.IsEnabled = true;
                    ListJournals.IsEnabled = true;
                    foreach (object t in ListAccounts.Items)
                    {
                        if (((Accounts)t).Key == ((InvoiceDistributions)_distributions[0]).Account)
                        {
                            ListAccounts.SelectedItem = t;
                            break;
                        }
                    }
                    _journalNames = JournalNames.load(((InvoiceDistributions)_distributions[0]).Journal);
                    if (_journalNames != null)
                    {
                        foreach (object t in ListJournals.Items)
                        {
                            if (((JournalNames)t).JournalName.Trim().Equals(_journalNames.JournalName.Trim()))
                            {
                                ListJournals.SelectedItem = t;
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void CtxAdd_Click(object sender, RoutedEventArgs e)
        {
            if (company == null)
                return;
            _distribution = new InvoiceDistributions(distributionTable);
            if (_distributions == null)
            {
                _distributions = new ArrayList();
                _distribution.InvoiceKey = InvoiceKey;
                _distribution.CompanyList = companies;
                _distribution.AccountList = accounts;
                _distribution.JournalList = journals;
                departments = Department.loadByCompany(company.CompanyNumber);
                FillDistributions = false;
                if (ListDepartments.Items.Count == 1)
                    ListDepartments.SelectedIndex = 0;
                _distribution.DepartmentList = departments;
                FillDistributions = true;
                //GridDistributions.ItemsSource = null;
                //GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
                //IsDistChanged = true;
                //return;
            }
            int c = _distributions.Count;

            if (c >= 1)
            {
                _distribution.Company = ((InvoiceDistributions)_distributions[c - 1]).Company;
                _distribution.CompanyName = ((InvoiceDistributions)_distributions[c - 1]).CompanyName;
                _distribution.Department = ((InvoiceDistributions)_distributions[c - 1]).Department;
                _distribution.DepartmentName = ((InvoiceDistributions)_distributions[c - 1]).DepartmentName;
                _distribution.DepartmentList = ((InvoiceDistributions)_distributions[c - 1]).DepartmentList;
                _distribution.InvoiceKey = InvoiceKey;
                _distribution.CompanyList = companies;
                _distribution.AccountList = accounts;
                _distribution.JournalList = journals;
            }
            else
            {
                if (ListCompanies.SelectedIndex >= 0)
                {
                    _distribution.CompanyName = ((Company)ListCompanies.SelectedItem).Name;
                    _distribution.Company = ((Company)ListCompanies.SelectedItem).CompanyNumber;
                }
                if (ListDepartments.SelectedIndex >= 0)
                {
                    _distribution.Department = ((Department)ListDepartments.SelectedItem).DepartmentNumber;
                    _distribution.DepartmentName = ((Department)ListDepartments.SelectedItem).Name;
                }
                if (ListAccounts.SelectedIndex >= 0)
                {
                    _distribution.Account = ((Accounts)ListAccounts.SelectedItem).AccountNumber;
                    _distribution.AccountName = ((Accounts)ListAccounts.SelectedItem).Description;
                }

            }
            _distributions.Add(_distribution);
            GridDistributions.ItemsSource = null;
            GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
            if (_distributions.Count > 1)
            {
                CheckSplit.IsChecked = true;
                ListAccounts.SelectedIndex = -1;
                ListJournals.SelectedIndex = -1;
            }
            IsDistChanged = true;
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            CtxDelete_Click(sender, e);
        }

        private void CmdAdd_Click(object sender, RoutedEventArgs e)
        {
            CtxAdd_Click(sender, e);
        }

        private void CmdSaveDist_Click(object sender, RoutedEventArgs e)
        {
            //InvoiceDistributions.DeleteAll((InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions)));
            InvoiceDistributions dist;
            JournalNames journal = (JournalNames)ListJournals.SelectedItem;
            if (_distributions != null && _distributions.Count > 0)
            {
                if (_distributions.Count == 1)
                {
                    dist = (InvoiceDistributions)_distributions[0];
                    if (dist.Deleted)
                    {
                        dist.delete(dist.Id.ToString());
                        ListAccounts.SelectedIndex = -1;
                    }
                    else
                    {
                        dist.Amount = _invoice.Amount;
                        dist.InvoiceKey = InvoiceKey;
                        if (dist.Company == 0)
                            dist.Company = (short)_invoice.Company;
                        if (dist.Department == 0)
                            dist.Department = (short)_invoice.Department;
                        if (journal != null)
                            dist.Journal = journal.JournalName;
                        dist.save();
                    }
                }
                else
                {
                    for (int i = 0; i < _distributions.Count; i++)
                    {
                        dist = (InvoiceDistributions)_distributions[i];
                        dist.InvoiceKey = InvoiceKey;
                        if (dist.Company == 0)
                            dist.Company = (short)_invoice.Company;
                        if (dist.Department == 0)
                            dist.Department = (short)_invoice.Department;
                        if (journal != null)
                            dist.Journal = journal.JournalName;
                        if (dist.Account != 0)
                        {
                            if (dist.Deleted)
                            {
                                if (dist.Id == 0)
                                {
                                    _distributions.RemoveAt(i);
                                    break;
                                }
                                else
                                    dist.delete(dist.Id.ToString());
                            }
                            else if (dist.IsChanged)
                                dist.save();
                        }
                    }
                }
            }
            else
            {
                if (ListAccounts.SelectedIndex >= 0)
                {
                    if (_distributions == null || _distributions.Count == 0)
                    {
                        if (_distributions == null)
                            _distributions = new ArrayList();
                        dist = new InvoiceDistributions()
                        {
                            InvoiceKey = InvoiceKey,
                            Company = (short)_invoice.Company,
                            Department = (short)_invoice.Department
                        };
                    }
                    else
                    {
                        dist = (InvoiceDistributions)_distributions[0];
                    }

                    Accounts acct = (Accounts)ListAccounts.SelectedItem;
                    if (acct != null)
                        dist.Account = acct.Key;
                    if (journal != null)
                        dist.Journal = journal.JournalName;
                    dist.Amount = _invoice.Amount;
                    dist.save();
                    _distributions.Add(dist);
                }
            }
            IsDistChanged = false;
            LoadDistributions();

            if (_distributions != null && _distributions.Count == 1)
            {
                CheckSplit.IsChecked = false;
                ListAccounts.IsEnabled = true;
                ListJournals.IsEnabled = true;
                foreach (object t in ListAccounts.Items)
                {
                    if (((Accounts)t).Key == ((InvoiceDistributions)_distributions[0]).Account)
                    {
                        ListAccounts.SelectedItem = t;
                        break;
                    }
                }
                _journalNames = JournalNames.load(((InvoiceDistributions)_distributions[0]).Journal);
                if (_journalNames != null)
                {
                    foreach (object t in ListJournals.Items)
                    {
                        if (((JournalNames)t).JournalName.Trim().Equals(_journalNames.JournalName.Trim()))
                        {
                            ListJournals.SelectedItem = t;
                            break;
                        }
                    }
                }
            }
            else if (_distributions != null && _distributions.Count > 1)
            {
                ListJournals.SelectedIndex = -1;
                ListAccounts.SelectedIndex = -1;
                ListAccounts.IsEnabled = false;
                ListJournals.IsEnabled = false;
                CheckSplit.IsChecked = true;
            }
            GridDistributions.ItemsSource = _distributions;

        }

        private void lstMemos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMemos.SelectedIndex >= 0)
            {
                //memo = (Memos)lstMemos.SelectedItem;
                //TxtMemo.Text = memo.Memo;
                memo = null;
                TxtMemo.Text = ((Memos)lstMemos.SelectedItem).Memo;
                // _invoice.MemoId = memo.MemoId;
                if (memoStatus == 0 || memoStatus == 2)
                    memoStatus = 4;
                else if (memoStatus == 1 || memoStatus == 3)
                    memoStatus = 5;

            }

        }

        private void cmdGoToMemos_Click(object sender, RoutedEventArgs e)
        {
            frmMemos frm = new frmMemos(m_ThisApp);
            frm.VendorNumber = _invoice.VendorNumber;
            frm.VendorName = ListVendors.Text;
            frm.ShowDialog();
        }

        private void TxtMemo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (memoStatus == 0 || memoStatus == 4)
                memoStatus = 2;
            else if (memoStatus == 1 || memoStatus == 5)
                memoStatus = 3;
        }

        private void cmdClearMemo_Click(object sender, RoutedEventArgs e)
        {
            TxtMemo.Text = "";
            _invoice.MemoId = 0;
            memoStatus = 0;
        }

        private void cmdDisplayDoc_Click(object sender, RoutedEventArgs e)
        {
            bool r;
            System.Diagnostics.Process exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = WindocLocation.Trim() + "WindocViewer.exe";
            exProc.StartInfo.Arguments = rss.DocumentId.ToString();

            try
            {
                Main.ShowWindowBack(this);
                r = exProc.Start();
            }
            catch (System.Exception)
            {
                return;
            }
            exProc.WaitForExit(5000);

        }

        private void CmdEditDoc_Click(object sender, RoutedEventArgs e)
        {

            Main.scanEdit(rss.DocumentId, WindocLocation);

        }


        private void cmdScanDoc_Click(object sender, RoutedEventArgs e)
        {
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

            WinglGlobal.invpGlobal.amountpaid = _invoice.Amount - _invoice.DiscountAmount;
            WinglGlobal.invpGlobal.amounttopay = _invoice.AmountToPay;
            WinglGlobal.invpGlobal.Batch = BatchNumber;
            WinglGlobal.invpGlobal.Company = _invoice.Company;
            WinglGlobal.invpGlobal.Department = _invoice.Department;
            WinglGlobal.invpGlobal.vendor = _invoice.VendorNumber;
            WinglGlobal.invpGlobal.vName = VendorName;
            WinglGlobal.invpGlobal.invnumber = _invoice.FullInvoiceNumber;
            WinglGlobal.invpGlobal.date = _invoice.Date;
            WinglGlobal.invpGlobal.CheckDate = _invoice.CheckDate;
            WinglGlobal.invpGlobal.dueDate = _invoice.PayDueDate;
            scanImmediate(false, WinglGlobal.invpGlobal);

        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_invoice != null && _invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
            {
                _invoice.PrePaid = true;
            }
            else if (_invoice != null)
                _invoice.PrePaid = false;

            decimal.TryParse(txtAmount.Text, out decimal amt);
            if (InvoiceKey > 0 && amt == 0)
            {
                txtAmount.Focus();
                txtAmount.SelectionStart = 0;
                txtAmount.SelectionLength = txtAmount.Text.Length;
                txtAmount.SelectAll();
            }
            NumberEntry num = sender as NumberEntry;
            decimal amount = 0;
            decimal.TryParse(num.Text, out amount);
            if (amount != 0)
                TabDistributions_GotFocus(sender, new RoutedEventArgs());

        }

        private void DatePickerEx_SelectedDateChanged(object sender, EventArgs e)
        {
            if (_invoice != null && _invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
            {
                _invoice.PrePaid = true;
            }
            else if (_invoice != null)
                _invoice.PrePaid = false;

        }

        private void ListVendors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListVendors.SelectedItem != null)
            {
                vendor = (Vendors)ListVendors.SelectedItem;
                txtComment.Text = vendor.Comment;
                ListAccounts.SelectedIndex = -1;
                ListJournals.SelectedIndex = -1;
                chkACH.IsChecked = vendor.ACH;
                if (vendor.ACH && DateDue.SelectedDate != null)
                    dtCheckDate.SelectedDate = (DateTime)DateDue.SelectedDate;
                NewInvoiceDistributions();
                GetMemo(true);
                for (int i = 0; i < ListJournals.Items.Count; i++)
                {
                    if (((JournalNames)ListJournals.Items[i]).JournalName.Equals(vendor.Journal))
                    {
                        ListJournals.SelectedIndex = i;
                        break;
                    }
                }
                if (InvoiceKey > 0 && IsLoading)
                {
                    txtAmount.Focus();
                    txtAmount.SelectionStart = 0;
                    txtAmount.SelectionLength = txtAmount.Text.Length;

                }
                if (vendor == null)
                    return;
                ListInvoiceNumbers.ItemsSource = null;
                Invoices[] invoices = Invoices.loadByVendor(vendor.VendorNumber, invoiceTable);
                //InvoiceHistory[] history = InvoiceHistory.LoadByVendor(vendor.VendorNumber);
                if (LastInvoiceNumber != null && !LastInvoiceNumber.Trim().Equals(""))
                {
                    if (invoices == null)
                    {
                        invoices = new Invoices[0];
                    }
                    Array.Resize<Invoices>(ref invoices, invoices.Length + 1);
                    invoices[invoices.Length - 1] = new Invoices { InvoiceNumber = LastInvoiceNumber };
                }
                ListInvoiceNumbers.ItemsSource = invoices;
                if (!LastInvoiceNumber.Trim().Equals(""))
                {
                    ListInvoiceNumbers.SelectedIndex = invoices.Length - 1;
                    ListInvoiceNumbers.Text = LastInvoiceNumber;
                    chkSaveInvoiceNumber.IsChecked = true;

                }
                LastInvoiceNumber = "";
            }
        }

        private void TabInvoice_GotFocus(object sender, RoutedEventArgs e)
        {
            return;
            /*
            if (_distributions != null && _distributions.Count == 1)
            {
                Accounts acct = Accounts.load(((InvoiceDistributions)_distributions[0]).Account);
                if (acct != null)
                {
                    ListAccounts.SelectedItem = acct;
                    ListAccounts.Text = acct.Description;
                }
                ListJournals.SelectedItem = ((InvoiceDistributions)_distributions[0]).Journal;
                ListJournals.Text = ((InvoiceDistributions)_distributions[0]).Journal;
            }
            */
        }

        private void TabDistributions_LostFocus(object sender, RoutedEventArgs e)
        {
            //return;
            if (_distributions != null && _distributions.Count == 1)
            {
                Accounts acct = Accounts.load(((InvoiceDistributions)_distributions[0]).Account);
                if (acct != null)
                {
                    ListAccounts.SelectedItem = acct;
                    ListAccounts.Text = acct.Description;
                }
                ListJournals.SelectedItem = ((InvoiceDistributions)_distributions[0]).Journal;
                ListJournals.Text = ((InvoiceDistributions)_distributions[0]).Journal;
            }

        }

        private void TabDistributions_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_distributions == null)
                CtxAdd_Click(sender, new RoutedEventArgs());
            if (_distributions.Count == 1)
            {
                ((InvoiceDistributions)_distributions[0]).Amount = decimal.Parse(txtAmount.Text);
                if (ListJournals.SelectedIndex >= 0)
                    ((InvoiceDistributions)_distributions[0]).Journal = ListJournals.Text;
            }

            return;
            DataGrid grd = GridDistributions;
            if (grd.Items.Count > 0 && grd.Columns.Count > 0)
            {
                //grd.Focus();
                DataGridCellInfo cellInfo = new DataGridCellInfo(grd.Items[0], grd.Columns[2]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                if (cellContent != null)
                {
                    DataGridCell cell = (DataGridCell)cellContent.Parent;
                    //grd.SelectedCells.Clear();
                    //grd.SelectedCells.Add(grd.CurrentCell);
                    cell.IsEditing = false;
                    DataGridCell_Edit(cell);
                    //cell.IsSelected = true;
                    //cell.CaptureMouse();
                    //cell.Focus();
                    //cell.IsEditing = true;
                    //grd.BeginEdit();
                }
            }

        }

        private void DateDue_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPayFlag.SelectedIndex = 0;
            if (chkACH.IsChecked.Value || chkEFT.IsChecked.Value)
                dtCheckDate.SelectedDate = (DateTime)(DateDue.SelectedDate);
        }

        private void NumberEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                NumberEntry num = sender as NumberEntry;

                DataGridCell cell = FindVisualParent<DataGridCell>(num);

                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                    //DataGridCell_Edit(sender as DataGridCell);
                }
            }
            catch (Exception)
            {
            }

        }

        private void TxtAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_distributions != null && _distributions.Count > 0)
            {
                bool found = false;
                for (int i = 0; i < _distributions.Count; i++)
                {
                    if (((InvoiceDistributions)_distributions[i]).Amount != 0)
                    {
                        found = true;
                        break;
                    }
                    decimal amount = 0;
                    decimal.TryParse(txtAmount.Text, out amount);
                    ((InvoiceDistributions)_distributions[0]).Amount = amount;
                }
            }

        }

        private void chkEFT_Checked(object sender, RoutedEventArgs e)
        {
            if (chkACH.IsChecked.Value)
            {
                chkACH.IsChecked = false;
                chkEFT.IsChecked = true;
            }
            if (chkEFT.IsChecked.Value && DateDue.SelectedDate != null)
                dtCheckDate.SelectedDate = (DateTime)DateDue.SelectedDate;
        }

        private void chkACH_Checked(object sender, RoutedEventArgs e)
        {
            if (chkEFT.IsChecked.Value)
            {
                chkEFT.IsChecked = false;
                chkACH.IsChecked = true;
            }
            if (chkACH.IsChecked.Value && DateDue.SelectedDate != null && _invoice != null)
            {
                _invoice.CheckDate = (DateTime)DateDue.SelectedDate;
            }

        }
    }
}
