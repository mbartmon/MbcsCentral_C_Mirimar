using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for frmCloseYear.xaml
    /// </summary>
    public partial class frmCloseYear : Window
    {
        CompanyList[] companyList = null;
        int nfy = DateTime.Now.Year;
        DateTime StartDate;
        DateTime EndDate;
        Company tblComp = null;

        string m_thisApp;

        public frmCloseYear()
        {
            InitializeComponent();
        }

        public frmCloseYear(string app)
        {
            InitializeComponent();
            m_thisApp = app;
        }
        private void frmCloseYear_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            Company[] companies = Company.load(false, true);
            if (companies == null || companies.Length == 0)
            {
                MessageBoxCustom.Show("No Companies found to process.");
                this.Close();
            }
            companyList = new CompanyList[companies.Length];
            int i = 0;
            foreach (Company company in companies)
            {
                companyList[i] = new CompanyList() { CompanyNumber = company.CompanyNumber, CompanyName = company.Name, CurrentYear = company.CurrentFiscalYear };
                if (!company.InActive && company.CurrentFiscalYear < nfy)
                    companyList[i].Active = true;
                i++;
            }
            lstCompanies.ItemsSource = companyList;
            for (int y = nfy - 2; y < nfy + 5; y++)
            {
                lstYears.Items.Add(y);
                if (y == nfy)
                    lstYears.SelectedItem = y;
            }
            Accounts[] accts = Accounts.loadByCategory("003_");

            lstAccounts.ItemsSource = accts;

            JournalNames[] journals = JournalNames.load(true);

            lstJournals.ItemsSource = journals;
            lstJournals.SelectedIndex = 1;
        }

        private void frmCloseYear_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_thisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            
            bool found = false;
            bool anyActive = false;
            if (lstYears.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select a New Fiscal Year.");
                return;
            }
            foreach (CompanyList company in companyList)
            {
                if (company.Close)
                {
                    found = true;
                    if (company.Active)
                        anyActive = true;
                }
            }
            if (!found)
            {
                MessageBoxCustom.Show("No Companies that have not already been closed are selected.");
                return;
            }
            if (lstJournals.SelectedIndex <= 0)
            {
                if (MessageBoxCustom.Show("A Journal has not been selected. Do you want to continue closing without a Journal?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (MessageBoxCustom.Show("Do you want to BRING BALANCES FORWARD for selected companies?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                CloseYearSub();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CloseYearSub()
		{
			object m = null;
			string xsql = null;

			short i = 0;
			int c = 0;
			bool fwd = false;
			decimal total = default(decimal);
			decimal[] profit = new decimal[3];
			Accounts[] accts = null;
			int cmp = 0;
            Department[] depts = null;
            Transactions[] trans = null;
			Transactiontemp tblTemp = null;
			System.DateTime tdate = default(System.DateTime);
            nfy = (int)lstYears.SelectedItem;
			int dpt = 0;
			int act = 0;
			short testcol = 0;
            int ProfitLossAccount = 0;

            string journalName = "";

            if (lstJournals.SelectedIndex > 0)
                journalName = ((JournalNames)lstJournals.SelectedItem).JournalName;

            if (lstYears.SelectedIndex >= 0)
                nfy = (int)lstYears.SelectedItem;

            if (lstAccounts.SelectedIndex >= 0)
                ProfitLossAccount = ((Accounts)lstAccounts.SelectedItem).Key;

            Cursor cursor = this.Cursor;
			this.Cursor = Cursors.Wait;
			foreach (CompanyList company in companyList)
            {
                if (company.Active && company.Close)
                {
                    cmp = company.CompanyNumber;
                    depts = Department.loadByCompany((short)cmp);
                    if (depts == null || depts.Length == 0)
                    {
                        MessageBoxCustom.Show("No departments found for company: " + company.CompanyName);
                        return;
                    }
                    tdate = new DateTime(nfy, WinglGlobal.CurrentFiscalYear(cmp).Month, 1);
                    StartDate = new DateTime(nfy - 1, tdate.Month, 1);
                    EndDate = new DateTime(nfy - 1, 12, DateTime.DaysInMonth(nfy, tdate.Month));
                    txtStatus.Text = "Processing Company: " + company.CompanyName;
                    if (company.CurrentYear >= nfy)
                    {
                        MessageBoxCustom.Show(company.CompanyName + " has already had it's year closed.\n\rCannot close year again.");
                        continue;
                    }
                    xsql = "SELECT * FROM " + Transactions.TABLE + " WHERE [" + Transactions.SQL_COL_COMPANY_NUMBER + "] = " + company.CompanyNumber.ToString();
                    xsql += " AND [" + Transactions.SQL_COL_DATE + "] >= '" + tdate + "' AND [" + Transactions.SQL_COL_SOURCE + "] LIKE '%Close Year%'";
                    Transactions tran = new Transactions();
                    long count = tran.queryWithCount(xsql, null);
                    tran = null;
                    if (count > 0)
                    {
                        MessageBoxCustom.Show(company.CompanyName + " has had it's balances brought forward.\n\rCannot bring balances forward again.");
                        continue;
                    }

                    accts = Accounts.load();
                    if (accts == null || accts.Length == 0)
                    {
                        MessageBoxCustom.Show("Unable to retrieve Chart of Accounts.  Cannot continue.");
                        return;
                    }

                    foreach (Department dept in depts)
                    {
                        dpt = dept.DepartmentNumber;
                        foreach (Accounts acct in accts)
                        {
                            c = GetCategory(acct.MajorCategory);
                            if (c != 0 && c <= 3)
                            {
                                total = Transactions.AccountBalance(cmp, acct.Key, StartDate, EndDate, dpt);
                            }
                            else
                            {
                                profit[c - 3] = profit[c - 3] + Transactions.AccountBalance(cmp, acct.Key, StartDate, EndDate, dpt);
                            }

                            if (c <= 3 && total != 0)
                            {
                                tblTemp = new Transactiontemp();
                                tblTemp.CompanyNumber = (short)cmp;
                                tblTemp.DepartmentNumber = (short)dpt;
                                tblTemp.AccountNumber = acct.Key;
                                tblTemp.FiscalYear = (short)nfy;
                                tblTemp.Date = tdate;
                                tblTemp.Journal = journalName;
                                tblTemp.Source = "Close Year";
                                tblTemp.Description = "Openning Balance";
                                tblTemp.Amount = total;
                                tblTemp.save();
                                total = 0;
                                tblTemp = null;
                            }
                        }
                        if (ProfitLossAccount > 0)
                        {
                            tblTemp = new Transactiontemp();
                            tblTemp.CompanyNumber = (short)cmp;
                            tblTemp.DepartmentNumber = (short)dpt;
                            tblTemp.AccountNumber = ProfitLossAccount;
                            tblTemp.FiscalYear = (short)nfy;
                            tblTemp.Date = tdate;
                            tblTemp.Journal = journalName;
                            tblTemp.Source = "Close Year";
                            tblTemp.Description = "Openning Balance";
                            tblTemp.Amount = -(-profit[1] - profit[2]);
                            tblTemp.save();
                            profit[1] = 0;
                            profit[2] = 0;
                            tblTemp = null;
                        }
                    }
                    {
                        tblComp = Company.loadByCompany(cmp);
                        tblComp.CurrentFiscalYear = (short)nfy;
                        tblComp.save();
                        company.Active = false;
                        company.Close = false;
                    }
                }
            }
            txtStatus.Text = "Finished Processing.";
            if (MessageBoxCustom.Show("Print Transaction Edit List?","",MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
            {
                object[] obj = null;
                frmReportDispatcher frm = new frmReportDispatcher("MbcsCentral.WinGL.TransactionEditList", obj);

            }
            lstCompanies.ItemsSource = null;
            lstCompanies.ItemsSource = companyList;
            this.Cursor = cursor;
        }
				
        private int GetCategory(string cat)
        {
            cat = cat.Replace("_", "");
            int c = 0;
            int.TryParse(cat, out c);
            return c;
        }
        private void lstYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /* MBCSCEN-I331
        private void cmdVendors_Click(object sender, RoutedEventArgs e)
        {
            bool closed = true;
            Vendors[] vendors = Vendors.load(true);
            foreach (Vendors vendor in vendors)
            {
                if (vendor.CurrentPurchases > 0)
                {
                    closed = false;
                    break;
                }
            }
            if (closed)
            {
                if (MessageBoxCustom.Show("All Vendors have ZERO Current Year Purchases.  Close anyway?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            foreach (Vendors vendor in vendors)
            {
                vendor.PriorYearPurchases = vendor.CurrentPurchases;
                vendor.CurrentPurchases = 0;
                vendor.save();
            }
            MessageBoxCustom.Show("Vendor year closed.");
        }
        */
    }

    public class CompanyList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool changed = false;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public int CompanyNumber { get; set; }
        public string CompanyName { get; set; }
        public int CurrentYear { get; set; }
        public bool Close { get { return changed; } set { changed = value; OnPropertyChanged("Close"); } }
    public bool Active { get; set; } = false;
    }
}
