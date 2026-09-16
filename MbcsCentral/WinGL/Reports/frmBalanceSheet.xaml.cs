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
    /// Interaction logic for frmBalanceSheet.xaml
    /// </summary>
    public partial class frmBalanceSheet : Window
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
        BalanceSheetParms parms = null;
        System.Printing.PrintQueue printer;
        string className = "";
        Classes cls;
        BalanceSheet rpt = null;
        private string thisApp;

        public frmBalanceSheet()
        {
            InitializeComponent();
        }

        public frmBalanceSheet(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
        }

        private void frmBalanceSheet_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            // Batch Report?
            if (Batch && !NewBatch)
            {
                if (BatchKey > 0 && !NewBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = BalanceSheet.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this Balance Sheet batch report.");
                        return;
                    }
                    parms = new BalanceSheetParms();
                    CopyBatchParms();
                    BatchReport = batch;
                }
                else
                {
                    rpt = new BalanceSheet();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = BalanceSheetParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new BalanceSheetParms();
                    BatchReport = new BatchReports();
                }
                cmdSave.Content = "SAVE";
            }
            else
            {
                rpt = new BalanceSheet();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = BalanceSheetParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new BalanceSheetParms();
            }
            Company[] cmps = Company.load();
            lstCompanies.ItemsSource = cmps;
            Accounts[] accts = Accounts.load();
            lstAccounts.ItemsSource = accts;
            this.DataContext = parms;
            lstMonths.SelectedIndex = Math.Max(parms.Month, -1) ;
            if (parms.Year == DateTime.Now.Year)
                optCurrentYear.IsChecked = true;
            else
                optPriorYear.IsChecked = true;
        }

        private void frmBalanceSheet_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp != null)
            {
                parms.Company = cmp.CompanyNumber;
                parms.CompanyName = cmp.Name;
            }
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (dpt == null && chkConsolidated.IsChecked.Value)
            {
                parms.Department = 0;
                parms.DepartmentName = "";
            }
            else if (dpt == null && !chkConsolidated.IsChecked.Value)
            {
            }
            else
            {
                parms.Department = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }
            if (optCurrentYear.IsChecked.Value)
                parms.Year = 0;
            else
                parms.Year = 1;
            parms.Month = (short)lstMonths.SelectedIndex;
            Accounts acct = (Accounts)lstAccounts.SelectedItem;
            if (acct != null)
            {
                parms.PLAccount = acct.Key;
                parms.PLAccountName = acct.Description;
            }
            else
                parms.PLAccount = 0;
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
            if (dpt == null && chkConsolidated.IsChecked.Value)
            {
                parms.Department = 0;
                parms.DepartmentName = "";
            }
            else if (dpt == null && !chkConsolidated.IsChecked.Value)
            {
                MessageBoxCustom.Show("Department not selected");
                return;
            }
            else
            {
                parms.Department = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }
            if (optCurrentYear.IsChecked.Value)
                parms.Year = 0;
            else
                parms.Year = 1;
            Accounts acct = (Accounts)lstAccounts.SelectedItem;
            if (acct != null)
            {
                parms.PLAccount = acct.Key;
                parms.PLAccountName = acct.Description;
            }
            else
                parms.PLAccount = 0;
            parms.Month = (short)lstMonths.SelectedIndex;
            //parms.Month++;
            
            rpt.Cancel = parms.Cancel;
            rpt.Company = parms.Company;
            rpt.CompanyName = parms.CompanyName;
            rpt.Consolidated = parms.Consolidated;
            rpt.Department = parms.Department;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.Month = parms.Month + 1;
            rpt.PLAccount = parms.PLAccount;
            rpt.PLAccountName = parms.PLAccountName;
            rpt.Year = parms.Year;
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
                rpt.Target = 0;
            rpt.print_report(printer);
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.Company = parms.Company;
                rpt.CompanyName = parms.CompanyName;
                rpt.Consolidated = parms.Consolidated;
                rpt.Department = parms.Department;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.Month  = parms.Month;
                rpt.PLAccount = parms.PLAccount;
                rpt.PLAccountName = parms.PLAccountName;
                rpt.Year = parms.Year;

            }
            else
            {
                parms.Company = rpt.Company;
                parms.CompanyName = rpt.CompanyName;
                parms.Consolidated = rpt.Consolidated;
                parms.Department = rpt.Department;
                parms.DepartmentName = rpt.DepartmentName;
                parms.Month = rpt.Month;
                parms.PLAccount = rpt.PLAccount;
                parms.PLAccountName = rpt.PLAccountName;
                parms.Year = rpt.Year;
            }
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox lst = (ComboBox)sender;
            if (lst != null && lst.SelectedItem != null)
            {
                lstDepartments.ItemsSource = null;
                Department[] depts = Department.loadByCompany(((Company)lst.SelectedItem).CompanyNumber);
                lstDepartments.ItemsSource = depts;
                lstDepartments.SelectedIndex = (int)(depts.Length == 0 ? -1 : 0);
            }
        }
    }
}
