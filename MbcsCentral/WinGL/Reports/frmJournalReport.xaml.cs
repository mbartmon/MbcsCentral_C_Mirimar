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
    /// Interaction logic for frmJournalReport.xaml
    /// </summary>
    public partial class frmJournalReport : Window
    {
        public bool Batch { get; set; } = false;
        public bool BatchRun { get; set; } = false;
        public int BatchKey { get; set; } = 0;
        public string BatchType { get; set; } = "GL";
        public bool NewBatch { get; set; } = false; 
        public bool Cancel { get; set; } = true;
        public BatchReports BatchReport { get; set; } = null;

        private BatchReports batch = null;

        JournalReportParms parms = null;
        System.Printing.PrintQueue printer;
        string className = "";
        JournalReport rpt = null;
        Classes cls;

        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;

        private string thisApp;
        
        public frmJournalReport()
        {
            InitializeComponent();
        }

        public frmJournalReport(string thisApp)
        {
            InitializeComponent();
            thisApp = this.thisApp;
        }

        private void frmJournalReport_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            Company[] companies = Company.load();
            JournalNames[] journals = JournalNames.load();
            Journals[] layouts = Journals.load(true);
            lstCompanies.ItemsSource = companies;
            lstJournals.ItemsSource = journals;
            lstLayouts.ItemsSource = layouts;

            // Batch Report?
            if (Batch)
            {
                if (BatchKey > 0 && !NewBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = JournalReport.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this G/L batch report.");
                        return;
                    }
                    parms = new JournalReportParms();
                    CopyBatchParms();
                    BatchReport = batch;
                }
                else
                {
                    rpt = new JournalReport();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = JournalReportParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new JournalReportParms();
                    BatchReport = new BatchReports();
                }
                cmdPrint.Content = "SAVE";
                cmdSave.Visibility = Visibility.Collapsed;
            }
            else
            {
                rpt = new JournalReport();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = JournalReportParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new JournalReportParms();
            }
            this.DataContext = parms;
        }

        private void frmJournalReport_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Company cmp = (Company)lstCompanies.SelectedItem;
            Journals journal = (Journals)lstLayouts.SelectedItem;
            parms.Company = cmp.CompanyNumber;
            parms.CompanyName = cmp.Name;
            parms.Layout = journal.Layout;
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
            cmdPrint_Click(sender, e); 
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.Company = parms.Company;
                rpt.Department = parms.Department;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.CompanyName = parms.CompanyName;
                rpt.StartDate = parms.StartDate;
                rpt.EndDate = parms.EndDate;
                rpt.Consolidate = parms.Consolidate;
                rpt.ReportTitle = parms.ReportTitle;
                rpt.PrintSingleDepartment = parms.PrintSingleDepartment;
                rpt.JournalName = parms.JournalName;
                rpt.Layout = parms.Layout;
                rpt.SourceSearch = parms.SourceSearch;
                rpt.PrintTotals = parms.PrintTotals;
            }
            else
            {
                parms.Company = rpt.Company;
                parms.Department = rpt.Department;
                parms.DepartmentName = rpt.DepartmentName;
                parms.CompanyName = rpt.CompanyName;
                parms.StartDate = rpt.StartDate;
                parms.EndDate = rpt.EndDate;
                parms.Consolidate = rpt.Consolidate;
                parms.ReportTitle = rpt.ReportTitle;
                parms.PrintSingleDepartment = rpt.PrintSingleDepartment;
                parms.JournalName = rpt.JournalName;
                parms.Layout = rpt.Layout;
                parms.SourceSearch = rpt.SourceSearch;
                parms.PrintTotals = rpt.PrintTotals;
            }
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp == null)
            {
                MessageBoxCustom.Show("Company not selected");
                return;
            }
            Journals journal = (Journals)lstLayouts.SelectedItem;
            if (journal == null)
            {
                MessageBoxCustom.Show("Layout not selected");
                return;
            }
            parms.Company = cmp.CompanyNumber;
            parms.CompanyName = cmp.Name;
            parms.Layout = journal.Layout;
            parms.ReportTitle = txtReportTitle.Text;
            rpt.Consolidate = parms.Consolidate;
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (rpt.Consolidate || dpt == null)
            {
                parms.Department = 0;
                parms.DepartmentName = "";
            }
            else
            {
                parms.Department = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }

            rpt.Company = parms.Company;
            rpt.CompanyName = parms.CompanyName;
            rpt.Department = parms.Department;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.StartDate = parms.StartDate;
            rpt.EndDate = parms.EndDate;
            rpt.ReportTitle = parms.ReportTitle;
            rpt.PrintSingleDepartment = parms.PrintSingleDepartment;
            rpt.JournalName = parms.JournalName;
            rpt.Layout = parms.Layout;
            rpt.SourceSearch = parms.SourceSearch;
            rpt.PrintTotals = parms.PrintTotals;

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
            if (button.Content.ToString().ToLower().Equals("print"))
                rpt.Target = 1;
            else
                rpt.Target = 0;
            rpt.process(printer);
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp == null)
                return;

            Department[] depts = Department.loadByCompany(cmp.CompanyNumber, true);
            lstDepartments.ItemsSource = null;
            lstDepartments.ItemsSource = depts;
            lstDepartments.SelectedIndex = 1;
        }

        private void lstLayouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLayouts.SelectedIndex >= 0)
            {
                txtReportTitle.Text = ((Journals)lstLayouts.SelectedItem).Layout;
            }
        }
    }
}
