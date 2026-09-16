using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using MbcsCentral;

namespace MbcsCentral
{
    public partial class frmDbTransfer : Form
    {
        private System.Windows.Forms.OpenFileDialog frm = new System.Windows.Forms.OpenFileDialog();
        private const string  DBLocation = "SOFTWARE\\MBCS\\Winpm\\Options\\DefaultDatabase";
        private const string defaultWinpmDB = "c:\\pmsys\\winpm.mdb";
        private string WinpmDB;
        private string WinpmPath;
        const string TRANSFER_APP = "MbcsCentralDataConverter.exe";
        private string accessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=<database>";
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmDbTransfer()
        {
            InitializeComponent();

            cmdProcess.Click += cmdProcess_Click;
            cmdExit.Click += cmdExit_Click;
            cmdWinpm.Click += cmdWinpm_Click;
            cmdWingl.Click += cmdWingl_Click;
            cmdWindoc.Click += cmdWindoc_Click;
            cmdWinpr.Click += cmdWinpr_Click;
            cmdWintrm.Click += cmdWintrm_Click;
            cmdLeasemaker.Click += cmdLeasemaker_Click;
            //WinpmDB = (string) Main.GetRegistryEntry( DBLocation, defaultWinpmDB );
            //WinpmPath = Path.GetDirectoryName( WinpmDB );
            txtWinpm.Text = WinpmPath + "\\winpm.mdb";
            txtWingl.Text = WinpmPath + "\\wingl.mdb";
            txtWinpr.Text = WinpmPath + "\\winpr.mdb";
        }
        public frmDbTransfer(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            cmdProcess.Click += cmdProcess_Click;
            cmdExit.Click += cmdExit_Click;
            cmdWinpm.Click += cmdWinpm_Click;
            cmdWingl.Click += cmdWingl_Click;
            cmdWindoc.Click += cmdWindoc_Click;
            cmdWinpr.Click += cmdWinpr_Click;
            cmdWintrm.Click += cmdWintrm_Click;
            cmdLeasemaker.Click += cmdLeasemaker_Click;
            //WinpmDB = (string)Main.GetRegistryEntry(DBLocation, defaultWinpmDB);
            //WinpmPath = Path.GetDirectoryName(WinpmDB);
            txtWinpm.Text = WinpmPath + "\\winpm.mdb";
            txtWingl.Text = WinpmPath + "\\wingl.mdb";
            txtWinpr.Text = WinpmPath + "\\winpr.mdb";
        }
        // Form Button Events
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (chkWinpm.Checked)
            {
                if (!ProcessDB(txtWinpm.Text, "WinpmTables.xml", chkDelWinpm.Checked))
                    return;
            }
            else if (chkWingl.Checked)
            {
                if (!ProcessDB(txtWingl.Text, "WinglTables.xml", chkDelWingl.Checked))
                    return;
            }
        }
        private bool ProcessDB(string dbName, string tableFile, bool delete)
        {

            MigrationWindow frm = new MigrationWindow();
            frm.AccessConnectionString = accessConnectionString.Replace("<database>", dbName);
            frm.SqlConnectionString = GlobalShared.SqlConnectionString;
            frm.ClientId = GlobalShared.user.Account.clientId;
            frm.DeleteFirst = delete;
            frm.TableFile = tableFile;
            frm.Show();
            return true;

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Browse Button Events
        private void cmdWinpm_Click(object sender, EventArgs e)
        {
            frm.DefaultExt = "mdb";
            frm.Filter = "Access 7 |*.mdb|Access 2003+|*.accdb";
            frm.FilterIndex = 1;
            frm.FileName = "Winpm.mdb";
            frm.InitialDirectory = Path.GetDirectoryName( txtWinpm.Text );
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.FileName != null)
                    txtWinpm.Text = frm.FileName;
            }
        }
        private void cmdWingl_Click(object sender, EventArgs e)
        {
            frm.DefaultExt = "mdb";
            frm.FileName = "Wingl.mdb";
            frm.Filter = "Access 7 |*.mdb|Access 2003+|*.accdb";
            frm.FilterIndex = 1;
            frm.InitialDirectory = Path.GetDirectoryName( txtWingl.Text );
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.FileName != null)
                    txtWingl.Text = frm.FileName;
            }
        }
        private void cmdWinpr_Click(object sender, EventArgs e)
        {
            frm.DefaultExt = ".mdb";
            frm.FileName = "Winpr.mdb";
            frm.Filter = "Access 7 |*.mdb|Access 2003+|*.accdb";
            frm.FilterIndex = 1;
            frm.InitialDirectory = Path.GetDirectoryName( txtWinpr.Text );
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.FileName != null)
                    txtWinpr.Text = frm.FileName;
            }
        }
        private void cmdWindoc_Click(object sender, EventArgs e)
        {}
        private void cmdWintrm_Click(object sender, EventArgs e)
        {}
        private void cmdLeasemaker_Click(object sender, EventArgs e)
        {}

        private void cmdBatchReports_Click(object sender, EventArgs e)
        {
            if (chkDelWingl.Checked)
            {
                WinGL.BatchRptList.Query("DELETE FROM " + WinGL.BatchRptList.TABLE, GlobalShared.CN);
                WinGL.BatchReports.Query("DELETE FROM " + WinGL.BatchReports.TABLE, GlobalShared.CN);
            }

            int BatchKey;
            string directoryname = "\\BatchReportObjects";
            string dir = Path.GetDirectoryName(txtWingl.Text) + directoryname;
            string[] files = Directory.GetFiles(dir, "*.xml");
            string pattern = ">\"\r\n";
            foreach(string file in files)
            {
                WinGL.BatchRptList list = new WinGL.BatchRptList() { ListName = Path.GetFileName( file).Replace(".xml", "")};
                list.save();
                BatchKey = list.Key;
                using (StreamReader sr = File.OpenText(Path.GetFullPath(file)))
                {
                    string s = "";
                    s = sr.ReadToEnd();
                    string[] objects = Regex.Split(s, pattern);
                    foreach (string st in objects)
                    {
                        if (st != null && !st.Trim().Equals(""))
                            ParseAndMigrateBatchReport(st + ">", BatchKey);
                    }
                }
            }
        }

        private void ParseAndMigrateBatchReport(string cls, int key)
        {
            string reportType = "";
            cls = cls.Replace("cls", "").Replace("True", "true").Replace("False", "false");
            string t = cls.Substring(2, cls.IndexOf(" ") - 2);
            if (t == "Ledger")
                t = "GeneralLedgerReport";
            int firstline = cls.IndexOf(">") +1;
            cls = cls.Remove(0, firstline);

            string xmlns = "<?xml version=\"1.0\" encoding=\"utf - 16\"?>\r\n" + "<[Name] xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" ;
            cls = xmlns.Replace("[Name]", t) + cls;
            switch (t)
            {
                case "BalanceSheet":
                    reportType = "BS";
                    cls = cls.Replace("rpt", "");
                    break;
                case "CheckRegister":
                    reportType = "CR";
                    cls = cls.Replace("<Company>", "<CompanyNumber>").Replace("</Company>", "</CompanyNumber>");
                    cls = cls.Replace("<Department>", "<DepartmentNumber>").Replace("</Department>", "</DepartmentNumber>");
                    break;
                case "DisbRpt":
                    reportType = "DR";
                    break;
                case "Journals":
                    reportType = "JR";
                    cls = cls.Replace("<Journals ", "<JournalReport ").Replace("</Journals>", "</JournalReport>");
                    cls = cls.Replace("ReportName", "ReportTitle").Replace("layout", "Layout");
                    break;
                case "GeneralLedgerReport":
                    reportType = "GL";
                    cls = cls.Replace("</Ledger>", "</GeneralLedgerReport>");
                    break;
                case "OperatingStatement":
                    reportType = "OS";
                    cls = cls.Replace("ReportName", "StatementName").Replace("rptYear", "Year").Replace("rptMonth", "Month").Replace("<StmtName/>", "").Replace("OpStmt:", "").Replace("<Company>", "<CompanyNumber>").Replace("</Company>", "</CompanyNumber>");
                    cls = cls.Replace("<Department>", "<DepartmentNumber>").Replace("</Department>", "</DepartmentNumber>");
                    break; 
                case "TrialBalance":
                    reportType = "TB";
                    cls = cls.Replace("rpt", "");
                    break;
                default:
                    return;
                    break;
            }
            WinGL.BatchReports rpt = new WinGL.BatchReports() { BatchKey = key, ReportType = reportType, Report = cls };
            rpt.save();
        }
    }
}
