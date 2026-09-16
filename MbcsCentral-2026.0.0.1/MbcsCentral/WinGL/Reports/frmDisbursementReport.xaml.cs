using System;
using System.Collections.Generic;
using System.Linq;
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


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmDisbursementReport.xaml
    /// </summary>
    public partial class frmDisbursementReport : Window
    {
        public bool Batch { get; set; } = false;
        public int BatchKey { get; set; } = 0;
        public string BatchType { get; set; } = "DR";
        public BatchReports BatchReport { get; set; } = null;
        public bool NewBatch { get; set; } = false;
        public bool Cancel { get; set; } = true;
        public int BalanceAccount { get; set; }
        public int CollectionAccountOne { get ;  set; }
        public int CollectionAccountTwo { get ; set; } 
        public string[] Journals { get;  set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentNumber { get; set; }
        public string CompanyName { get; set; }
        public int CompanyNumber { get; set; }

        BatchReports batch = null;

        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        DisbursementReportParms parms = null;
        DisbursementReport rpt = null;
        System.Printing.PrintQueue printer;
        string className = "";
        Classes cls;

        public frmDisbursementReport()
        {
            InitializeComponent();
        }

        private string thisApp;
        public frmDisbursementReport(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frmDisbursementReport_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            lstMonths.SelectedIndex = DateTime.Now.Month - 1;
            Company[] cmp = Company.load();
            lstCompanies.ItemsSource = cmp;
            Accounts[] accounts = Accounts.load();
            lstBalanceAccount.ItemsSource = accounts;
            lstCollectionsOne.ItemsSource = accounts;
            lstCollectionsTwo.ItemsSource = accounts;
            JournalNames[] journalNames = JournalNames.load();
            for (int i = 0; i < 10; i++)
                ((ComboBox)this.FindName("lstJournal" + i.ToString())).ItemsSource = journalNames;
            // Batch Report?
            if (Batch && !NewBatch)
            {
                if (BatchKey > 0)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = DisbursementReport.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this Disbursement Report batch report.");
                        return;
                    }
                    parms = new DisbursementReportParms();
                    CopyBatchParms();
                    BatchReport = batch;
                    lstMonths.SelectedIndex = parms.Month - 1;
                }
                else
                {
                    rpt = new DisbursementReport();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = DisbursementReportParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new DisbursementReportParms();
                    BatchReport = new BatchReports();
                }
                cmdSave.Content = "SAVE";
            }
            else
            {
                rpt = new DisbursementReport();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = DisbursementReportParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new DisbursementReportParms();
            }

            this.DataContext = parms;
        }

        private void frmDisbursementReport_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);

            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp != null)
            {
                parms.Company = cmp.CompanyNumber;
                parms.CompanyName = cmp.Name;
            }
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (dpt != null )
            {
                parms.Department = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }
            if (Batch)
            {
                CopyBatchParms(true);
                batch.Report = rpt.Serialize();
                batch.save();
            }
            else
            {
                cls.value = parms.Serialize();
                cls.clientId = clientId;
                cls.acctId = acctId;
                cls.name = className;
                cls.save();
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp == null)
            {
                MessageBoxCustom.Show("Company not selected");
                return;
            }
            parms.Company = cmp.CompanyNumber;
            parms.CompanyName = cmp.Name;
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (dpt == null)
            {
                MessageBoxCustom.Show("Department not selected");
                return;
            }
            parms.Department = dpt.DepartmentNumber;
            parms.DepartmentName = dpt.Name;
            if (lstMonths.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("Month not selected.");
                return;
            }
            parms.Month = lstMonths.SelectedIndex + 1;
            if (chkPriorYear.IsChecked.Value)
                parms.Year = DateTime.Now.Year - 1;
            else
                parms.Year = DateTime.Now.Year;
            if (lstBalanceAccount.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("Balance Account not selected.");
                return;
            }
            parms.BalanceAccount = ((Accounts)lstBalanceAccount.SelectedItem).Key;
            if (lstCollectionsOne.SelectedIndex < 0 && lstCollectionsTwo.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("At least one collection account must be selected.");
                return;
            }
            parms.CollectionAccountOne = ((Accounts)lstCollectionsOne.SelectedItem).Key;
            parms.CollectionAccountTwo = ((Accounts)lstCollectionsTwo.SelectedItem).Key;
            rpt.BalanceAccount = parms.BalanceAccount;
            rpt.CollectionAccount1 = parms.CollectionAccountOne;
            rpt.CollectionAccount2 = parms.CollectionAccountTwo;
            rpt.Company = parms.Company;
            rpt.CompanyName = parms.CompanyName;
            rpt.Department = parms.Department;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.Journals = parms.Journals;
            rpt.rptMonth = parms.Month;
            rpt.rptYear = parms.Year;
            if (Batch)
            {
                if (batch == null)
                {
                    batch = new BatchReports();
                    batch.ReportType = BatchType;
                    batch.BatchKey = BatchKey;
                }
                batch.Report = rpt.Serialize();
                batch.save();
                BatchReport = batch;
                Cancel = false;
                this.Close();
            }
            Button button = (Button)sender;
            if (button.Content.ToString().ToLower().Equals("save"))
                return;
            rpt.print_report();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.BalanceAccount = parms.BalanceAccount;
                rpt.CollectionAccount1 = parms.CollectionAccountOne;
                rpt.CollectionAccount2 = parms.CollectionAccountTwo;
                rpt.Journals = parms.Journals;
                rpt.Company = parms.Company;
                rpt.CompanyName = parms.CompanyName;
                rpt.Department = parms.Department;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.rptYear = parms.Year;
                rpt.rptMonth = parms.Month;

            }
            else
            {
                parms.BalanceAccount = rpt.BalanceAccount;
                parms.CollectionAccountOne = rpt.CollectionAccount1;
                parms.CollectionAccountTwo = rpt.CollectionAccount2;
                parms.Journals = rpt.Journals;
                parms.Company = rpt.Company;
                parms.CompanyName = rpt.CompanyName;
                parms.Department = rpt.Department;
                parms.DepartmentName = rpt.DepartmentName;
                parms.Year = rpt.rptYear;
                parms.Month = rpt.rptMonth;
            }
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies.SelectedIndex >= 0)
            {
                Department[] depts = Department.loadByCompany((short)((Company)lstCompanies.SelectedItem).CompanyNumber);
                lstDepartments.ItemsSource = depts;
                if (depts != null)
                    lstDepartments.SelectedIndex = 0;
                    
            }
        }
    }
}
