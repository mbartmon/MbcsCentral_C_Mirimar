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
    /// Interaction logic for frmOperatingStatement.xaml
    /// </summary>
    public partial class frmOperatingStatement : Window
    {
        public int CompanyNumber { get; set; }
        public int DepartmentNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public bool Batch { get; set; } = false;
        public bool BatchRun { get; set; } = false;
        public int BatchKey { get; set; } = 0;
        public string BatchType { get; set; } = "OP";
        public bool Cancel { get; set; } = true;
        public BatchReports BatchReport { get; set; } = null;
        public bool NewBatch { get; set; } = false; 

        private BatchReports batch = null;


        private OperatingStatementParms parms = null;
        System.Printing.PrintQueue printer;
        string className = "";
        OperatingStatement rpt = null;
        Classes cls;

        bool AddEdit = false;

        private int acctId = (int)GlobalShared.user.Account.accountId;
        string thisApp;

        public frmOperatingStatement()
        {
            InitializeComponent();
        }

        public frmOperatingStatement(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
        }

        private void frmOperatingStatement_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            Company[] companies = Company.load();
            OpStmt[] statements = OpStmt.load();
            lstCompanies.ItemsSource = companies;
            lstStatements.ItemsSource = statements;
            List<int> years = new List<int>();
            // Batch Report?
            if (Batch)
            {
                if (BatchKey > 0 && !NewBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = OperatingStatement.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this G/L batch report.");
                        return;
                    }
                    parms = new OperatingStatementParms();
                    CopyBatchParms();
                    OpStmt opt = null;
                    BatchReport = batch;
                    for (int j = 0; j < lstStatements.Items.Count; j++)
                    {
                        opt = (OpStmt)lstStatements.Items[j];
                        if (opt.ReportName.Equals(parms.StatementName))
                        {
                            lstStatements.SelectedIndex = j;
                            break;
                        }

                    }
                }
                else
                {
                    rpt = new OperatingStatement();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = OperatingStatementParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new OperatingStatementParms();
                    BatchReport = new BatchReports();
                }
                cmdDisplay.Content = "SAVE";
                cmdDelete.Visibility = Visibility.Collapsed;
            }
            else
            {
                rpt = new OperatingStatement();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = OperatingStatementParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new OperatingStatementParms();
            }

            // Detail Group
            switch (parms.Detail)
            {
                case 0:
                    optDetail.IsChecked = true;
                    break;
                case 1:
                    optSummary.IsChecked = true;
                    break;
                default:
                    optTotals.IsChecked = true;
                    break;
            }
            // Current Group
            switch (parms.CurrentQtdYtd)
            {
                case 0:
                    optCurrent.IsChecked = true;
                    break;
                case 1:
                    optQTD.IsChecked = true;
                    break;
                default:
                    optYTD.IsChecked = true;
                    break;
            }
            if (parms.Month == null || parms.Month <= 0)
                lstMonths.SelectedIndex = DateTime.Now.Month - 1;
            else
                lstMonths.SelectedIndex = parms.Month - 1;
            int year = DateTime.Now.Year;
            int index = 0;
            int i = 0;
            for (int y = year - 5; y <= year; y++)
            {
                years.Add(y);
                if (y == parms.Year)
                    i = index;
                index++;
            }
            lstYears.ItemsSource = years;
            lstYears.SelectedIndex = i;
            this.DataContext = parms;

        }

        private void frmOperatingStatement_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (GlobalShared.user.hasModifyPermission(thisApp))
            {
                parms.CompanyNumber = (short)(lstCompanies.SelectedItem == null ? 0 : ((Company)lstCompanies.SelectedItem).CompanyNumber);
                parms.CompanyName = (lstCompanies.SelectedItem == null ? "" : ((Company)lstCompanies.SelectedItem).Name);
                parms.DepartmentNumber = (short)(lstDepartments.SelectedItem == null ? 0 : ((Department)lstDepartments.SelectedItem).DepartmentNumber);
                parms.DepartmentName = (lstDepartments.SelectedItem == null ? "" : ((Department)lstDepartments.SelectedItem).Name);
                parms.StatementName = (lstStatements.SelectedItem == null ? "" : ((OpStmt)lstStatements.SelectedItem).ReportName);
                parms.Month = lstMonths.SelectedIndex + 1;
                parms.Year = (int)lstYears.SelectedItem;
 
                // Detail Group
                if (optDetail.IsChecked.Value)
                    parms.Detail = 0;
                else if (optSummary.IsChecked.Value)
                    parms.Detail = 1;
                else
                    parms.Detail = 2;
                // Current Group
                if (optCurrent.IsChecked.Value)
                    parms.CurrentQtdYtd = 0;
                else if (optQTD.IsChecked.Value)
                    parms.CurrentQtdYtd = 1;
                else
                    parms.CurrentQtdYtd = 2;
                if (Batch)
                {
                    CopyBatchParms(true);
                    batch.Report = rpt.Serialize();
                    batch.save();
                }
                else
                {
                    cls.value = parms.Serialize();
                    cls.clientId = (int)GlobalShared.user.Account.clientId; ;
                    cls.acctId = acctId;
                    cls.name = className;
                    cls.save();
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            // Detail Group
            if (optDetail.IsChecked.Value)
                parms.Detail = 0;
            else if (optSummary.IsChecked.Value)
                parms.Detail = 1;
            else
                parms.Detail = 2;
            // Current Group
            if (optCurrent.IsChecked.Value)
                parms.CurrentQtdYtd = 0;
            else if (optQTD.IsChecked.Value)
                parms.CurrentQtdYtd = 1;
            else
                parms.CurrentQtdYtd = 2;
            rpt.StatementName = parms.StatementName;
            rpt.CompanyName = parms.CompanyName;
            rpt.CompanyNumber = parms.CompanyNumber;
            rpt.CurrentQtdYtd = parms.CurrentQtdYtd;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.DepartmentNumber = parms.DepartmentNumber;
            rpt.Detail = parms.Detail;
            rpt.Month = lstMonths.SelectedIndex + 1;
            rpt.Percentage = parms.Percentage;
            rpt.PrintYtd = parms.PrintYtd;
            rpt.PrintZero = parms.PrintZero;
            rpt.PriorYear = parms.PriorYear;
            rpt.Year = (int)lstYears.SelectedItem;
            rpt.CondensePrint = parms.CondensePrint;
            if (Batch)
            {
                //if (GlobalShared.user.hasModifyPermission(thisApp))
                //{
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
                //}
            }
            Button button = (Button)sender;
            if (button.Content.ToString().ToLower().Equals("save"))
                return;
            if (button.Content.ToString().ToLower().Equals("print"))
                rpt.Target = 1;
            else
                rpt.Target = 0;
            rpt.process(printer);

        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            string rptName = ""; 
            MessageBoxCustom.Show("NEW OPERATING STATEMENT", out rptName, 128);
            if (rptName.Trim().Equals(""))
            {
                MessageBoxCustom.Show("A report name was not entered.");
                return;
            }
            OpStmt stmt = new OpStmt() { ReportName = rptName };
            stmt.save();

            OpStmt[] statements = OpStmt.load();
            lstStatements.ItemsSource = null;
            lstStatements.ItemsSource = statements;
            OpStmt optStmt = OpStmt.load_MaxId();
            foreach(OpStmt op in lstStatements.Items)
            {
                if (op.Id == optStmt.Id)
                {
                    lstStatements.SelectedItem = op;
                    break;
                }
            }

            AddEdit = true;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdIncome_Click(object sender, RoutedEventArgs e)
        {
            OpStmt stmt = (OpStmt)lstStatements.SelectedItem;
            if (stmt == null)
            {
                MessageBoxCustom.Show("Select a Statment.");
                return;
            }
            frmAccountRanges frm = new frmAccountRanges(thisApp) { AccountCategories = 4, ReportType = "OpStmt", ReportName = stmt.ReportName, ReportSection = 1 };
            frm.ShowDialog();
        }

        private void cmdExpense_Click(object sender, RoutedEventArgs e)
        {
            OpStmt stmt = (OpStmt)lstStatements.SelectedItem;
            if (stmt == null)
            {
                MessageBoxCustom.Show("Select a Statment.");
                return;
            }
            frmAccountRanges frm = new frmAccountRanges(thisApp) { AccountCategories = 6, ReportType = "OpStmt", ReportName = stmt.ReportName, ReportSection = 2 };
            frm.ShowDialog();
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company company = (Company)lstCompanies.SelectedItem;
            if (company != null)
            {
                parms.CompanyName = company.Name;
                Department[] depts = Department.loadByCompany(company.CompanyNumber);
                lstDepartments.ItemsSource = depts;
                if (depts != null && depts.Length > 0)
                    lstDepartments.SelectedIndex = 0;
            }
        }

        private void lstDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department dept = (Department)lstDepartments.SelectedItem;
            if (dept != null)
            {
                parms.DepartmentNumber = dept.DepartmentNumber;
                parms.DepartmentName = dept.Name;
            }
        }


        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.CompanyNumber = (short)rpt.CompanyNumber;
                rpt.CompanyName = parms.CompanyName;
                rpt.CurrentQtdYtd = parms.CurrentQtdYtd;
                rpt.DepartmentNumber = (short)rpt.DepartmentNumber;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.Consolidated = parms.Consolidated;
                rpt.Detail = parms.Detail;
                rpt.Month = parms.Month;
                rpt.Percentage = parms.Percentage;
                rpt.PrintYtd = parms.PrintYtd;
                rpt.PrintZero = parms.PrintZero;
                rpt.PriorYear = parms.PriorYear;
                rpt.Year = parms.Year;
                rpt.StatementName = parms.StatementName;

            }
            else
            {
                parms.CompanyNumber = (short)rpt.CompanyNumber;
                parms.CompanyName = rpt.CompanyName;
                parms.CurrentQtdYtd = rpt.CurrentQtdYtd;
                parms.DepartmentNumber = (short)rpt.DepartmentNumber;
                parms.DepartmentName = rpt.DepartmentName;
                parms.Consolidated = rpt.Consolidated;
                parms.Detail = rpt.Detail;
                parms.Month = rpt.Month;
                parms.Percentage = rpt.Percentage;
                parms.PrintYtd = rpt.PrintYtd;
                parms.PrintZero = rpt.PrintZero;
                parms.PriorYear = rpt.PriorYear;
                parms.Year = rpt.Year;
                parms.StatementName = rpt.StatementName;
            }
        }

        private void lstStatements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstStatements.SelectedItem != null)
                parms.StatementName = ((OpStmt)lstStatements.SelectedItem).ReportName;
        }

    }
}
