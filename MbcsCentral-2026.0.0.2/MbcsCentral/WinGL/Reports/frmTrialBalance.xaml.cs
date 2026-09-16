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
    /// Interaction logic for frmTrialBalance.xaml
    /// </summary>
    public partial class frmTrialBalance : Window
    {
        public bool Batch { get; set; } = false;
        public bool BatchRun { get; set; } = false;
        public int BatchKey { get; set; } = 0;
        public string BatchType { get; set; } = "GL";
        public bool Cancel { get; set; } = true;
        public BatchReports BatchReport { get; set; } = null;
        public bool NewBatch { get; set; } = false;
        BatchReports batch = null;

        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        TrialBalanceParms parms = null;
        System.Printing.PrintQueue printer;
        string className = "";
        Classes cls;
        TrialBalance rpt = null;
        private string thisApp;

        public frmTrialBalance()
        {
            InitializeComponent();
        }

        public frmTrialBalance(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
        }

        private void frmTrialBalance_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            List<int> years = new List<int>();
            // Batch Report?
            if (Batch)
            {
                if (BatchKey > 0 && !NewBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = TrialBalance.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this Balance Sheet batch report.");
                        return;
                    }
                    parms = new TrialBalanceParms();
                    CopyBatchParms();
                    BatchReport = batch;
                }
                else
                {
                    rpt = new TrialBalance();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = TrialBalanceParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new TrialBalanceParms();
                    BatchReport = new BatchReports();
                }
                cmdDisplay.Content = "SAVE";
            }
            else
            {
                rpt = new TrialBalance();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = TrialBalanceParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new TrialBalanceParms();
            }
            Company[] companies = Company.load();
            lstCompanies.ItemsSource = companies;
            lstMonths.SelectedIndex = parms.Month - 1;
            this.DataContext = parms;
            int year = DateTime.Now.Year;
            int index = 15;
            for (int y = year - 15; y <= year; y++)
            {
                years.Add(y);
                if (y == parms.Year)
                    index = years.Count - 1;
            }
            lstYears.ItemsSource = years;
            lstYears.SelectedIndex = index;
        }

        private void frmTrialBalance_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
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

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            Company company = (Company)lstCompanies.SelectedItem;
            if (company == null)
            {
                MessageBoxCustom.Show("Company not selected");
                return;
            }
            parms.Company = company.CompanyNumber;
            parms.CompanyName = company.Name;
            Department department = (Department)lstDepartments.SelectedItem;
            if (department == null && chkConsolidated.IsChecked.Value)
            {
                parms.Department = 0;
                parms.DepartmentName = "";
            }
            else if (department == null && !chkConsolidated.IsChecked.Value)
            {
                MessageBoxCustom.Show("Department not selected");
                return;
            }
            else
            {
                parms.Department = department.DepartmentNumber;
                parms.DepartmentName = department.Name;
            }
            parms.Year = (int)lstYears.SelectedItem;
            parms.Month = (short)lstMonths.SelectedIndex;
            parms.Month++;

            rpt.Cancel = parms.Cancel;
            rpt.Company = parms.Company;
            rpt.CompanyName = parms.CompanyName;
            rpt.Consolidated = parms.Consolidated;
            rpt.Department = parms.Department;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.Month = parms.Month;
            rpt.PriorYear = parms.PriorYear;
            rpt.XWorkArea = parms.XWorkArea;
            rpt.Year = parms.Year;
            rpt.ZeroBalances = parms.ZeroBalances;
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
            if (button.Content.ToString().ToLower().Equals("display"))
                rpt.print_report(printer);
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company company = (Company)lstCompanies.SelectedItem;
            if (company != null)
            {
                lstDepartments.ItemsSource = null;
                Department[] depts = Department.loadByCompany(company.CompanyNumber);
                lstDepartments.ItemsSource = depts;
                lstDepartments.SelectedIndex = 0;
            }
        }

        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.Cancel = parms.Cancel;
                rpt.Company = parms.Company;
                rpt.CompanyName = parms.CompanyName;
                rpt.Consolidated = parms.Consolidated;
                rpt.Department = parms.Department;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.Month = parms.Month;
                rpt.PriorYear = parms.PriorYear;
                rpt.XWorkArea = parms.XWorkArea;
                rpt.Year = parms.Year;
                rpt.ZeroBalances = parms.ZeroBalances;
            }
            else
            {
                parms.Cancel = rpt.Cancel;
                parms.Company = rpt.Company;
                parms.CompanyName = rpt.CompanyName;
                parms.Consolidated = rpt.Consolidated;
                parms.Department = rpt.Department;
                parms.DepartmentName = rpt.DepartmentName;
                parms.Month = rpt.Month;
                parms.PriorYear = rpt.PriorYear;
                parms.XWorkArea = rpt.XWorkArea;
                parms.Year = rpt.Year;
                parms.ZeroBalances = rpt.ZeroBalances;
            }
        }
    }
}
