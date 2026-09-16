//TODO: Verify that Non-Batch Memos are kept separate from Batch memos and are, therefore, deleted once invoice is posted.  Also, that no Batch memo is also deleted.
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MbcsCentral.WinDoc;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmInvoice.xaml
    /// </summary>
    public partial class frmInvoice : Window
    {
        public int InvoiceKey { get; set; }
        public bool IsBatch { get; set; }
        public int BatchNumber { get; set; }
        public bool ShowAgain { get; set; } = true;
        public string InvoiceNumber { get; set; } = "";
        public bool InactiveVendorsOnly { get; set; } = true;
        public int LastVendor { get; set; }
        public short LastCompany { get; set; }
        public short LastDepartment { get; set; }
        public DateTime LastInvoiceDate { get; set; }
        public DateTime LastPayDate { get; set; }

        frmInvoice frm = null;
        private bool IsNew = true;

        private ArrayList _distributions;
        private ArrayList _departments;
        private InvoiceDistributions _distribution = null;
        private string distributionTable = InvoiceDistributions.TABLE;
        bool IsChanged = false;
        bool IsDistChanged = false;
        private string invoiceTable = Invoices.TABLE;
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

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmInvoice(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }
        public frmInvoice()
        {
            InitializeComponent();
        }

        private void FrmInvoice_OnLoaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            cmdScanDoc.Visibility = Visibility.Collapsed;
            cmdDisplayDoc.Visibility = Visibility.Collapsed;
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

                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "Got windoc location.", false);
                }
                control[0].save();
            }
            control = null;
            GlobalShared.Log.Log((int)LogClass.logType.Debug, "Ready to get all lists.", false);

            for (int i = 0; i < 100; i++)
                ListCheckGroup.Items.Add(i.ToString());
            ListCheckGroup.SelectedIndex = 0;

            GlobalShared.Log.Log((int)LogClass.logType.Debug, "About to get vendors.",false);
            Vendors[] vendors = Vendors.load(InactiveVendorsOnly);
            GlobalShared.Log.Log((int)LogClass.logType.Debug, "Got " + (vendors == null ? "null" : vendors.Length.ToString()) + " vendors.", false);
            ListVendors.ItemsSource = vendors;

            //tenants = WinPM.Tenants.loadForNames(false);
            //lstTenants.ItemsSource = tenants;

            Company[] companies = Company.load();
            ListCompanies.ItemsSource = companies;

            Accounts[] accounts = WinGL.Accounts.load(false,false);
            ListAccounts.ItemsSource = accounts;

            JournalNames[] journals = JournalNames.load();
            ListJournals.ItemsSource = journals;

            if (IsBatch)
            {
                //pnlFrequency.Visibility = Visibility.Visible;
                PnlPay.Visibility = Visibility.Collapsed;
                txtInvoiceDate.Text="Last Paid";
                TextDue.Text = "Next Due";
                this.Title = "Batch Invoice EDIT";

                //DateInvoice.Visibility = Visibility.Collapsed;
                //DateDue.Visibility = Visibility.Collapsed;
                txtInvoiceDate.Text="Last Paid";
                TextDue.Text = "Next Due";
                invoiceTable = InvoicesBatch.TABLE;
                distributionTable = InvBatchDistributions.TABLE;
            }
            else
            {
                pnlFrequency.Visibility = Visibility.Collapsed;
            }
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
                oldInvoice = _invoice.InvoiceNumber;
                GridMain.DataContext = _invoice;

                Invoices[] invoices = Invoices.loadByVendor(_invoice.VendorNumber, invoiceTable);
                if (_invoice.PayDueDate == GlobalShared.NULL_DATE)
                    CheckUseDue.IsChecked = false;
                for (int i = 0; i < invoices.Length; i++)
                {
                    ListInvoiceNumbers.Items.Add(invoices[i].InvoiceNumber);
                    if (_invoice.InvoiceNumber.Equals(invoices[i].InvoiceNumber))
                        ListInvoiceNumbers.SelectedIndex = i;
                }

                foreach (object t in ListVendors.Items)
                {
                    if (((Vendors) t).VendorNumber == _invoice.VendorNumber)
                    {
                        ListVendors.SelectedItem = t;
                        VendorName = ((Vendors)t).VendorName;
                        break;
                    }
                }
                foreach (object t in ListCompanies.Items)
                {
                    if (((Company) t).CompanyNumber == _invoice.Company)
                    {
                        ListCompanies.SelectedItem = t;
                        break;
                    }
                }
                if (_invoice.CheckGroup >= 0 && _invoice.CheckGroup < 100)
                    ListCheckGroup.SelectedIndex = _invoice.CheckGroup;
                GetMemo();
                if (!IsBatch)
                    ListPayFlag.SelectedIndex = _invoice.PayFlag;
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
                    if (scan)
                    {
                        rss = new Documents(CN);
                        rss = Documents.loadByInvoice(_invoice.VendorNumber, _invoice.InvoiceNumber, CN);
                        if (rss != null)
                        {
                            cmdDisplayDoc.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            cmdScanDoc.Visibility = Visibility.Visible;
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
                if (scan)
                {
                    string invoiceNumber = (_invoice.Prefix.Trim().Equals("") ? "" : _invoice.Prefix.Trim() + " ") + _invoice.InvoiceNumber + (_invoice.Suffix.Trim().Equals("") ? "" : " " + _invoice.Suffix.Trim());
                    rss = new Documents(CN);
                    rss = Documents.loadByInvoice(_invoice.VendorNumber, invoiceNumber, CN);
                    if (rss != null)
                    {
                        cmdDisplayDoc.Visibility = Visibility.Visible;
                        cmdScanDoc.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        cmdScanDoc.Visibility = Visibility.Visible;
                        cmdDisplayDoc.Visibility = Visibility.Collapsed;
                    }

                }
            }
            else
            {
                _invoice = new Invoices(invoiceTable) {Date = DateTime.Now, PayDueDate = DateTime.Now};
                if (LastCompany > 0)
                {
                    _invoice.Company = LastCompany;
                    foreach (object t in ListCompanies.Items)
                    {
                        if (((Company)t).CompanyNumber == _invoice.Company)
                        {
                            ListCompanies.SelectedItem = t;
                            break;
                        }
                    }
                }
                if (LastDepartment > 0)
                {
                    _invoice.Department = LastDepartment;
                }
                if (LastVendor > 0)
                    _invoice.VendorNumber = LastVendor;
                if (LastInvoiceDate > GlobalShared.NULL_DATE)
                    _invoice.Date = LastInvoiceDate;
                if (LastPayDate > GlobalShared.NULL_DATE)
                    _invoice.PayDueDate = LastPayDate;
                if (_invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
                {
                    _invoice.PrePaid = true;
                }
                if (!InvoiceNumber.Trim().Equals(""))
                {
                    _invoice.InvoiceNumber = InvoiceNumber;
                    chkSaveInvoiceNumber.IsChecked = true;
                }
                if (scan)
                    cmdScanDoc.Visibility = Visibility.Visible;

            }

            if (_invoice != null && _invoice.InvoiceNumber != null)
            {
                // Parse InvoiceNumber
                string invoiceNumber = _invoice.InvoiceNumber;
                short dpt = 0;
                string ten = "";

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
                            chkUseDepartmentNumber.IsChecked = true;
                        }
                    }

                }
                // Test for TenantNumber suffix

                if (_invoice.Suffix != null && !_invoice.Suffix.Trim().Equals(""))
                        chkUseSuffix.IsChecked = true;


            }

            GridMain.DataContext = _invoice;
            ListVendors.Focus();
        }

        private void LoadDistributions()
        {
            if (!IsNew)
            {
                if (IsBatch)
                    distributionTable = InvBatchDistributions.TABLE;
                TextVendorName.Text = (string)((Vendors)ListVendors.SelectedItem).VendorName;
                TextInvoiceNumber.Text = _invoice.InvoiceNumber;
                DateInvoiceDate.SelectedDate = _invoice.Date;
                AmountInvoice.Text = string.Format("{0:C}", _invoice.Amount);
                _distributions = InvoiceDistributions.loadByInvoiceAsList(_invoice.InvoiceKey, distributionTable);
            }
            Company[] cmps = Company.load();
            WinGL.Accounts[] acts = WinGL.Accounts.load();
            JournalNames[] journalNames = JournalNames.load();
            //VirtualizingStackPanel.SetIsVirtualizing(GridDistributions, false);

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
                }
                GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
                TotalDistributions();
            }

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

        }

        private void NewInvoiceDistributions()
        {
            //AllocatedFlag = False : TotalAmount = 0

            int r = 0;
            VendorCategories[] tblVendorCats = VendorCategories.loadByVendor(vendor.VendorNumber);
            if (tblVendorCats == null && tblVendorCats.Length == 0)
                return;

            _distributions = null;
            _distributions = new ArrayList();
            GridDistributions.ItemsSource = null;

            bool noMatch = true;
            r = tblVendorCats.Length;
            CheckSplit.IsChecked = (r > 1 ? true : false) ;
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
                if (cat.Account > 0)
                {
                    dist.Account = cat.Account;
                    Accounts acct = Accounts.load(cat.Account);
                    if (acct != null)
                        dist.AccountName = acct.Description;
                }
                dist.Department = (short)(cat.Department <= 0 ? 0 : cat.Department);
                dist.Amount = cat.Amount;
                dist.Account = cat.Account;
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
            if (frm != null)
                frm.ShowDialog();
        }

        private void ListCompanies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox lst = (ComboBox) sender;
            int i = lst.SelectedIndex;
            if (i >= 0)
            {
                company = (Company)lst.SelectedItem;
                GetDepartments(company.CompanyNumber);
                if (ListDepartments.Items.Count == 1)
                    ListDepartments.SelectedIndex = 0;
                if (_distributions != null || _distributions.Count == 1)
                    InitialDistribution();
            }
            else
                company = null;
        }
        private void ListDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox lst = (ComboBox)sender;
            int i = lst.SelectedIndex;
            if (i >= 0)
            {
                department = (Department)lst.SelectedItem;
                //if (InvoiceKey > 0)
                    GetMemo(true);
                if (_distributions == null || _distributions.Count == 1)
                    InitialDistribution();
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
        private void CmdShowSplits_OnClick(object sender, RoutedEventArgs e)
        {
            TabDistributions.Focus();
        }

        private void GetMemo(bool NewInvoice = false)
        {
            if ( company != null  && department != null)
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
                _invoice.PayFlag = (short)ListPayFlag.SelectedIndex;
            }

            if (!CheckUseDue.IsChecked.Value)
                _invoice.PayDueDate = GlobalShared.NULL_DATE;

            _invoice.Company = company.CompanyNumber;
            _invoice.Department = department.DepartmentNumber;

            if (CheckMemo.IsChecked.Value && TxtMemo.Text.Trim() != "")
            {
                if (memo == null)
                    memo = new Memos() { CheckGroup = _invoice.CheckGroup,
                        BatchNumber = _invoice.Batch, CompanyNumber = _invoice.Company,
                        DepartmentNumber = _invoice.Department, VendorNumber = _invoice.VendorNumber };
                memo.Memo = TxtMemo.Text;
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
                _invoice.AllocationFlag = false;

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
            if (IsNew && scan && CheckScan.IsChecked.Value)
            {
                scanImmediate((bool)(_invoice.CheckNumber > 0 && _invoice.CheckDate > GlobalShared.NULL_DATE ? true : false), WinglGlobal.invpGlobal);
            }
            if (!IsNew && CheckScan.IsChecked.Value && scan && !IsBatch && BatchNumber == 9)
            {
                WinglGlobal.docInvoice(_invoice, false, WindocLocation + "Windoc.mdb", false, oldInvoice);
            }
            if (chkEFT.IsChecked.Value && _invoice.CheckNumber == 0)
            {
                _invoice.CheckNumber = WinglGlobal.GetCheckNumber(_invoice.Company, _invoice.Department);
                if (_invoice.CheckDate <= GlobalShared.NULL_DATE)
                    _invoice.CheckDate = DateTime.Now;
                _invoice.save();
            }
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

            //CmdSaveDist_Click(sender, e);
            if (!IsBatch && ShowAgain)
            {
                frm = new frmInvoice(m_ThisApp) { LastVendor = _invoice.VendorNumber, LastCompany = (short)_invoice.Company, LastDepartment = (short)_invoice.Department };
                frm.LastInvoiceDate = _invoice.Date;
                if (CheckUseDue.IsChecked.Value)
                    frm.LastPayDate = _invoice.PayDueDate;
                else
                    frm.LastPayDate = GlobalShared.NULL_DATE;
                if (chkSaveInvoiceNumber.IsChecked.Value)
                {
                    frm.InvoiceNumber = this.ListInvoiceNumbers.Text;
                    chkSaveInvoiceNumber.IsChecked = true;
                }
                this.Close();
            }

            this.Close();
        }

        void scanImmediate(bool PrePaid, WinglGlobal.PostInvoice_type invpGlobal)
        {
            bool r;
            string uniqueID = DateTime.Now.ToString("MMddyyyyhhmm");
            System.Diagnostics.Process exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = WindocLocation.Trim() + "windoc.exe";
            exProc.StartInfo.Arguments = String.Format("{0} \"{1}\" \"{2}\"", "wingl-runOnce", uniqueID , WinglGlobal.scanInvoice(uniqueID, invpGlobal, false, PrePaid, false));
            try
            {
                r = exProc.Start();
            }
            catch (System.Exception)
            {
            }

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
                msg += "\r\n" + "Company: " + (company == null ? "<not found>" :company.Name) + "\r\n" + "Department: " + (dpt == null ? "<not found>" : dpt.Name);
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
            IsDistChanged = true;
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

        private void GridDistributions_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                IsDistChanged = true;
                var newItem = e.Row.DataContext as InvoiceDistributions;
                if (!_distributions.Contains(newItem))
                {
                    _distributions.Add(newItem);
                    TotalDistributions();
                }
            }
        }


        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            }
        }

        private void GridDistributions_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Amount":

                    if (TotalOfDistributions < _invoice.Amount)
                    {
                        for (int i = 0; i < _distributions.Count;i++ )
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
            if (_distributions == null)
            {
                _distributions = new ArrayList();
                _distributions.Add(new InvoiceDistributions());
            }

            switch (c.Tag)
            {
                case "ListAccountsDist":
                    ((InvoiceDistributions)_distributions[n]).AccountName = ((Accounts)c.SelectedItem).Description;
                    ((InvoiceDistributions)_distributions[n]).Account = ((Accounts)c.SelectedItem).Key;
                    break;
                case "ListDepartmentsDist":
                    ((InvoiceDistributions)_distributions[n]).DepartmentName = ((Department)c.SelectedItem).Name;
                    ((InvoiceDistributions)_distributions[n]).Department = ((Department)c.SelectedItem).DepartmentNumber;
                    break;
                case "ListCompaniesDist":
                    ((InvoiceDistributions)_distributions[n]).CompanyName = ((Company)c.SelectedItem).Name;
                    ((InvoiceDistributions)_distributions[n]).Company = ((Company)c.SelectedItem).CompanyNumber;
                    short cmp = ((Company)c.SelectedItem).CompanyNumber;
                    Department[] departments = Department.loadByCompany(cmp);
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
                        ((InvoiceDistributions)_distributions[n]).SelectedDepartment = departments[0];
                        
                    }
                    break;
                case "ListJournalsDist":
                    ((InvoiceDistributions)_distributions[n]).Journal = ((JournalNames)c.SelectedItem).JournalName;
                    break;
            }
            GridDistributions.ItemsSource = null;
            GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
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
            }

        }

        private void CtxAdd_Click(object sender, RoutedEventArgs e)
        {
            _distribution = new InvoiceDistributions(distributionTable);
            _distribution.InvoiceKey = InvoiceKey;
            if (_distributions == null)
                _distributions = new ArrayList();
            int count = _distributions.Count;
            if (count == 0)
            {
                if (ListCompanies.SelectedIndex >= 0)
                {
                    _distribution.Company = ((Company)ListCompanies.SelectedItem).CompanyNumber;
                    _distribution.CompanyName = ((Company)ListCompanies.SelectedItem).Name;
                }
                if (ListDepartments.SelectedIndex >= 0)
                {
                    _distribution.Department = ((Department)ListDepartments.SelectedItem).DepartmentNumber;
                    _distribution.DepartmentName = ((Department)ListDepartments.SelectedItem).Name;
                }
            }
            else
            {
                _distribution.Company = ((InvoiceDistributions)_distributions[count - 1]).Company;
                _distribution.CompanyName = ((InvoiceDistributions)_distributions[count - 1]).CompanyName;
                _distribution.Department = ((InvoiceDistributions)_distributions[count - 1]).Department;
                _distribution.DepartmentName = ((InvoiceDistributions)_distributions[count - 1]).DepartmentName;

            }
            Department[] departments = Department.loadByCompany(_distribution.Company);
            ((InvoiceDistributions)_distributions[count- 1]).DepartmentList = departments;

            _distributions.Add(_distribution);
            GridDistributions.ItemsSource = null;
            GridDistributions.ItemsSource = (InvoiceDistributions[])_distributions.ToArray(typeof(InvoiceDistributions));
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
                                dist.delete(dist.Id.ToString());
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
                            { InvoiceKey = InvoiceKey, Company = (short)_invoice.Company, 
                              Department = (short)_invoice.Department };
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
        }

        private void lstMemos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMemos.SelectedIndex >= 0)
            {
                memo = (Memos)lstMemos.SelectedItem;
                TxtMemo.Text = memo.Memo;
               // _invoice.MemoId = memo.MemoId;
                if (memoStatus == 0 || memoStatus == 2)
                    memoStatus = 4;
                else if (memoStatus == 1 || memoStatus == 3)
                    memoStatus = 5;

            }

        }

        private void cmdGoToMemos_Click(object sender, RoutedEventArgs e)
        {
            frmMemos frm = new frmMemos();
            frm.VendorNumber = _invoice.VendorNumber;
            frm.VendorName = _invoice.VendorName;
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
                r = exProc.Start();
            }
            catch (System.Exception)
            {
            }

        }

        private void cmdScanDoc_Click(object sender, RoutedEventArgs e)
        {
            WinglGlobal.invpGlobal.amountpaid = _invoice.Amount - _invoice.DiscountAmount;
            WinglGlobal.invpGlobal.amounttopay = _invoice.AmountToPay;
            WinglGlobal.invpGlobal.Batch = BatchNumber;
            WinglGlobal.invpGlobal.Company = _invoice.Company;
            WinglGlobal.invpGlobal.Department = _invoice.Department;
            WinglGlobal.invpGlobal.vendor = _invoice.VendorNumber;
            WinglGlobal.invpGlobal.vName = VendorName;
            WinglGlobal.invpGlobal.invnumber = _invoice.InvoiceNumber;
            WinglGlobal.invpGlobal.date = _invoice.Date;
            WinglGlobal.invpGlobal.CheckDate = _invoice.CheckDate;
            WinglGlobal.invpGlobal.dueDate = _invoice.PayDueDate;
            scanImmediate(false, WinglGlobal.invpGlobal);

        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
            {
                _invoice.PrePaid = true;
            }
            else
                _invoice.PrePaid = false;
        }

        private void DatePickerEx_SelectedDateChanged(object sender, EventArgs e)
        {
            if (_invoice.CheckNumber != null && _invoice.CheckNumber != 0 && _invoice.CheckDate > GlobalShared.NULL_DATE)
            {
                _invoice.PrePaid = true;
            }
            else
                _invoice.PrePaid = false;

        }

        private void ListVendors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListVendors.SelectedItem != null)
            {
                vendor = (Vendors)ListVendors.SelectedItem;
                txtComment.Text = vendor.Comment;
                NewInvoiceDistributions();
            }
        }

        private void TabDistributions_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }

}
