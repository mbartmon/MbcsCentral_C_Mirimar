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
using System.Xml.Serialization;
using System.IO;


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmCheckRegSelection.xaml
    /// </summary>
    public partial class frmCheckRegSelection : Window
    {
        public bool Batch { get; set; } = false;
        public int BatchKey { get; set; } = 0;
        public string BatchType { get; set; } = "CR";
        public bool Cancel { get; set; } = true;
        public BatchReports BatchReport { get; set; } = null;
        public bool newBatch = false;

        BatchReports batch = null;

        string className = "";
        CheckRegisterParms parms = null;
        Classes cls;
        CheckRegister rpt = null;

        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;

        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmCheckRegSelection(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        public frmCheckRegSelection()
        {
            InitializeComponent();
        }

        private void frmCheckRegSelection_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            if (Batch)
            {
                if (BatchKey > 0 && !newBatch)
                {
                    batch = BatchReports.load(BatchKey);
                    rpt = CheckRegister.DeSerialize(batch.Report);
                    className = this.GetType().Name;
                    cls = new Classes();
                    if (rpt == null)
                    {
                        MessageBoxCustom.Show("Could not retrieve this Check Register batch report.");
                        return;
                    }
                    parms = new CheckRegisterParms();
                    CopyBatchParms();
                    BatchReport = batch;
                }
                else
                {
                    rpt = new CheckRegister();
                    className = this.GetType().Name;
                    cls = Classes.load(acctId, className);
                    if (cls != null)
                        parms = CheckRegisterParms.DeSerialize(cls.value);
                    else
                        cls = new Classes();
                    if (parms == null)
                        parms = new CheckRegisterParms();
                    BatchReport = new BatchReports();
                }
                cmdSave.Content = "SAVE";
            }
            else
            {
                className = this.GetType().Name;
                cls = Classes.load(acctId, className);
                if (cls != null)
                    parms = CheckRegisterParms.DeSerialize(cls.value);
                else
                {
                    cls = new Classes();
                    parms = new CheckRegisterParms();
                }
            }
            this.DataContext = parms;
            Company[] companies = Company.load();
            lstCompanies.ItemsSource = companies;
            if (parms.CompanyNumber > 0)
                for (int i = 0; i < lstCompanies.Items.Count; i++)
                {
                    if (((Company)lstCompanies.Items[i]).CompanyNumber ==  parms.CompanyNumber)
                    {
                        lstCompanies.SelectedIndex = i;
                        if (parms.DepartmentNumber > 0)
                        {
                            for (int j = 0; j <lstDepartments.Items.Count; j++)
                                if (((Department)lstDepartments.Items[j]).DepartmentNumber == parms.DepartmentNumber)
                                {
                                    lstDepartments.SelectedIndex = j;
                                    break;
                                }
                        }
                        break;
                    }
                }
        }

        private void CopyBatchParms(bool reverse = false)
        {
            if (reverse)
            {
                rpt.CompanyNumber = parms.CompanyNumber;
                rpt.DepartmentNumber = parms.DepartmentNumber;
                rpt.EndCheck = parms.EndCheck;
                rpt.EndDate = parms.EndDate;
                rpt.StartCheck = parms.StartCheck;
                rpt.StartDate = parms.StartDate;

            }
            else
            {
                parms.CompanyNumber = rpt.CompanyNumber;
                parms.DepartmentNumber = rpt.DepartmentNumber;
                parms.EndCheck = rpt.EndCheck;
                parms.EndDate = rpt.EndDate;
                parms.StartCheck = rpt.StartCheck;
                parms.StartDate = rpt.StartDate;
            }
        }

        private void frmCheckRegSelection_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                return;
            }
            if (lstCompanies.SelectedIndex > -1)
                parms.CompanyNumber = ((Company)lstCompanies.SelectedItem).CompanyNumber;
            if (lstDepartments.SelectedIndex > -1)
                parms.DepartmentNumber = ((Department)lstDepartments.SelectedItem).DepartmentNumber;
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
            parms.CompanyNumber = cmp.CompanyNumber;
            parms.CompanyName = cmp.Name;
            Department dpt = (Department)lstDepartments.SelectedItem;
            if (dpt == null)
            {
                parms.DepartmentNumber = 0;
                parms.DepartmentName = "";
            }
            else
            {
                parms.DepartmentNumber = dpt.DepartmentNumber;
                parms.DepartmentName = dpt.Name;
            }
            CheckRegister rpt = new CheckRegister();
            TimeSpan ts = new TimeSpan(0, 0, 0);
            rpt.StartDate = parms.StartDate.Date + ts;
            ts = new TimeSpan(23, 59, 59);
            rpt.EndDate = parms.EndDate.Date + ts;
            rpt.StartCheck = parms.StartCheck;
            rpt.EndCheck = parms.EndCheck;
            rpt.CompanyNumber = parms.CompanyNumber;
            rpt.CompanyName = parms.CompanyName;
            rpt.DepartmentNumber = parms.DepartmentNumber;
            rpt.DepartmentName = parms.DepartmentName;
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

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies.SelectedIndex < 0)
                return;
            short cmp = ((Company)lstCompanies.SelectedItem).CompanyNumber;
            lstDepartments.ItemsSource = null;
            Department[] depts = Department.loadByCompany(cmp, true);
            lstDepartments.ItemsSource = depts;
            lstDepartments.SelectedIndex = 1;
        }
    }
}
