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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmGeneralLedger.xaml
    /// </summary>
    public partial class frmGeneralLedger : Window
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
        GeneralLedgerParms parms = null;
        System.Printing.PrintQueue printer;
        string className = "";
        Classes cls;
        GeneralLedgerReport rpt = null;
        private string thisApp;

        public frmGeneralLedger()
        {
            InitializeComponent();
        }

        public frmGeneralLedger(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
        }

        private void formGeneralLedgerReport_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            dtStart.DisplayDate = DateTime.Now.AddMonths(-1);
            dtStart.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            dtEnd.DisplayDate = DateTime.Now;
            dtEnd.Text = DateTime.Now.ToShortDateString();

            // Batch Report?
            if (Batch)
            {
                if (BatchKey > 0 && !NewBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = GeneralLedgerReport.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this G/L batch report.");
                        return;
                    }
                    parms = new GeneralLedgerParms();
                    CopyBatchParms();
                    BatchReport = batch;
                }
                else
                {
                    rpt = new GeneralLedgerReport();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = GeneralLedgerParms.DeSerializeObject(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new GeneralLedgerParms();
                    BatchReport = new BatchReports();
                }
                cmdDisplay.Visibility = Visibility.Collapsed;
                cmdPrint.Content = "SAVE";
            }
            else
            {
                rpt = new GeneralLedgerReport();
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = GeneralLedgerParms.DeSerializeObject(cls.value);
                else
                    cls = new Classes();
                if (parms == null)
                    parms = new GeneralLedgerParms();
            }
            Company[] cmps = Company.load();
            lstCompany.ItemsSource = cmps;
            Accounts[] accts = Accounts.load();
            lstAccounts.ItemsSource = accts;
            InnerGrid.DataContext = parms;
            dtStart.Text = parms.StartDate.ToShortDateString();
            dtEnd.Text = parms.EndDate.ToShortDateString();
            dtReport.Text = DateTime.Now.ToShortDateString();

            if (parms.SingleAccount)
                foreach (Accounts accs in lstAccounts.Items)
                {
                    if (parms.Account == accs.Key)
                    {
                        lstAccounts.SelectedItem = accs;
                        break;
                    }
                }

        }

        private void formGeneralLedgerReport_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (!parms.SingleAccount)
                parms.Account = 0;
            if (Batch)
            {
                try
                {
                    CopyBatchParms(true);
                    batch.Report = rpt.Serialize();
                    batch.save();
                } catch { }
            }
            else
            {
                cls.value = parms.SerializeObject();
                cls.clientId = clientId;
                cls.acctId = acctId;
                cls.name = className;
                cls.save();
            }

        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            //rpt.Target = 0;
            //rpt.process();    
            cmdPrint_Click(sender, e);
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Company cmp = (Company)lstCompany.SelectedItem;
            if (cmp == null)
            {
                MessageBoxCustom.Show("Company not selected");
                return;
            }
            parms.ReportDate = (DateTime)dtReport.SelectedDate;
            parms.Company = (short)cmp.CompanyNumber;
            parms.CompanyName = cmp.Name;
            rpt.Company = parms.Company;
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (dpt == null)
            {
                parms.Department = 0;
                parms.DepartmentName = "";
            }
            else
            {
                parms.Department = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }
            Accounts account = (Accounts)lstAccounts.SelectedItem;
            if (chkSingleAccount.IsChecked.Value)
            {
                if (account == null)
                {
                    MessageBoxCustom.Show("You have checked 'SINGLE ACCOUNT' but have not selected an ACCOUNT.");
                    return;
                }
                parms.Account = account.Key;
            }
            else
                parms.Account = 0;
            if (chkSingleAccount.IsChecked.Value)
            {

            }
            rpt.Department = parms.Department;
            rpt.ReportDate = parms.ReportDate;
            rpt.CompanyName = parms.CompanyName;
            rpt.DepartmentName = parms.DepartmentName;
            rpt.Account = parms.Account;
            TimeSpan ts = new TimeSpan(0, 0, 0);
            rpt.StartDate = parms.StartDate.Date + ts;
            ts = new TimeSpan(23, 59, 59);
            rpt.EndDate = parms.EndDate.Date + ts;
            rpt.Consolidated = parms.Consolidated;
            rpt.PrintZeroBalances = parms.PrintZeroBalances;

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

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.Company = parms.Company;
                rpt.Department = parms.Department;
                rpt.DepartmentName = parms.DepartmentName;
                rpt.CompanyName = parms.CompanyName;
                rpt.SingleAccount = parms.SingleAccount;
                rpt.SingleDepartment = parms.SingleDepartment;
                rpt.Account = parms.Account;
                rpt.StartDate = parms.StartDate;
                rpt.EndDate = parms.EndDate;
                rpt.Consolidated = parms.Consolidated;
                rpt.PrintZeroBalances = parms.PrintZeroBalances;
                rpt.ReportDate = parms.ReportDate;
                rpt.Printer = parms.Printer;

            }
            else
            {
                parms.Company = rpt.Company;
                parms.Department = rpt.Department;
                parms.DepartmentName = rpt.DepartmentName;
                parms.CompanyName = rpt.CompanyName;
                parms.SingleDepartment = rpt.SingleDepartment;
                parms.SingleAccount = rpt.SingleAccount;
                parms.Account = rpt.Account;
                parms.StartDate = rpt.StartDate;
                parms.EndDate = rpt.EndDate;
                parms.Consolidated = rpt.Consolidated;
                parms.PrintZeroBalances = rpt.PrintZeroBalances;
                parms.ReportDate = rpt.ReportDate;
                parms.Printer = rpt.Printer;
            }
        }

        private void lstCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompany.SelectedItem == null)
                return;
            lstDepartments.ItemsSource = null;
            Department[] dpts = Department.loadByCompany(((Company)lstCompany.SelectedItem).CompanyNumber);
            lstDepartments.ItemsSource = dpts;
            lstDepartments.SelectedIndex = 0;

        }
    }

    /*
    public class GeneralLedgerParms
    {
        public short ReportCompany { get; set; } = 0;
        public short ReportDepartment { get; set; } = 0;
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }
        public int Account { get; set; } = 0;
        public DateTime StartDate { get; set; } = GlobalShared.NULL_DATE;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public bool Consolidated { get; set; } = false;
        public bool PrintZeroBalances { get; set; } = false;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string Printer { get; set; }
        public bool SingleDepartment { get; set; } = false;
        public bool SingleAccount { get; set; } = false;
        private DateTime endDate = DateTime.Now;

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static GeneralLedgerParms DeSerializeObject(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GeneralLedgerParms));
            StringReader rdr = new StringReader(inputString);
            GeneralLedgerParms parms = (GeneralLedgerParms)serializer.Deserialize(rdr);
            return parms;
        }
    }
    */

}
