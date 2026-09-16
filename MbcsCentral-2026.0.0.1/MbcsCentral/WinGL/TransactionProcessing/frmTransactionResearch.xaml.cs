using System;
using System.Collections;
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
    /// Interaction logic for frmTransactionResearch.xaml
    /// </summary>
    public partial class frmTransactionResearch : Window
    {
        string query = "";
        string savedQuery = "";
        ArrayList missing = new ArrayList();

        enum DrillDownType
        {
            Base,
            Date,
            Account,
            CheckNumber,
            Journal,
            Source,
            Description,
            Debits,
            Credits,
            Amounts
        }

        string[] DrillDownName = new string[Enum.GetNames(typeof(DrillDownType)).Length];
        string[] originalQuery = new string[Enum.GetNames(typeof(DrillDownType)).Length];

        private string ThisApp;

        public frmTransactionResearch(string app)
        {
            ThisApp = app;
            InitializeComponent();
        }

        public frmTransactionResearch()
        {
            InitializeComponent();
        }

        private void frmTransactionResearch_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            String date = (String)Main.GetRegistryEntry(GlobalShared.APPNAME + "\\Forms\\" + this.Name, "FromDate", DateTime.Now.ToString());
            if (date != null && !date.Trim().Equals(""))
            {
                dtFrom.SelectedDate = DateTime.Parse(date);
                dtFrom.Text = date;
            }
            date = (string)Main.GetRegistryEntry(GlobalShared.APPNAME + "\\Forms\\" + this.Name, "ToDate", DateTime.Now.ToString());
            if (date != null)
                dtTo.SelectedDate = DateTime.Parse(date);;

            Company[] companies = Company.load();
            lstCompanies.ItemsSource = companies;
            JournalNames[] journals = JournalNames.load();
            lstJournals.ItemsSource = journals;
            Accounts[] accounts = Accounts.load();
            lstAccounts.ItemsSource = accounts;
            //dtTo.SelectedDate = DateTime.Now;
            //dtFrom.SelectedDate = DateTime.Now;
        }

        private void frmTransactionResearch_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Main.SetRegistryEntry(GlobalShared.APPNAME + "\\Forms\\" + this.Name, "FromDate", dtFrom.SelectedDate.ToString(), (int)Registry.DataTypes.String);
            Main.SetRegistryEntry(GlobalShared.APPNAME + "\\Forms\\" + this.Name, "ToDate", dtTo.SelectedDate.ToString(), (int)Registry.DataTypes.String);
        }

        private bool Search(bool drillDown = false, Results result = null)
        {
            if (drillDown)
            {
                query = originalQuery[(int)DrillDownType.Base];

                if (ctxOriginalDates.IsChecked)
                    query += originalQuery[(int)DrillDownType.Date];
                else if (ctxDate.IsChecked)
                    query += " AND CAST([" + Transactions.SQL_COL_DATE + "] as date) = '" + result.Date.ToShortDateString() + "'";

                if (ctxOriginalAccount.IsChecked)
                    query += originalQuery[(int)DrillDownType.Account];
                else if (ctxAccount.IsChecked)
                    query += " AND [" + Transactions.SQL_COL_ACCOUNT_NUMBER + "] = " + result.AccountNumber.ToString();

                if (ctxOriginalCheckNumber.IsChecked)
                    query += originalQuery[(int)DrillDownType.CheckNumber];
                else if (ctxCheckNumber.IsChecked)
                    query += " AND [" + Transactions.SQL_COL_CHECK_NUMBER + "] = " + result.CheckNumber.ToString();

                if (ctxOriginalAmount.IsChecked)
                    query += originalQuery[(int)DrillDownType.Amounts];
                else if (ctxAmount.IsChecked)
                {
                    decimal amount1 = (result.Debit != 0 ? result.Debit : result.Credit);
                    decimal amount2 = -amount1;

                    query += " AND [" + Transactions.SQL_COL_AMOUNT + "] IN (" + amount1.ToString() + ", " + amount2.ToString() + ")";
                }

                query += " ORDER BY [" + Transactions.SQL_COL_DATE + "]";
                return true;
            }

            if (lstCompanies.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select a Company.", "", MessageBoxButton.OK, this);
                return false;
            }
            if (lstDepartments.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select a Department.", "", MessageBoxButton.OK, this);
                return false;
            }
            if (((DateTime)dtFrom.SelectedDate).Date == ((DateTime)dtTo.SelectedDate).Date )
            {
                if (MessageBoxCustom.Show("From and To dates are the same.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
            }
            bool first;
            // Companies
            query = "SELECT * FROM " + Transactions.TABLE + " WHERE ";
            if (lstCompanies.SelectedIndex >= 0)
                query += "[" + Transactions.SQL_COL_COMPANY_NUMBER + "] = " + ((Company)lstCompanies.SelectedItem).CompanyNumber.ToString();
            // Departments
            if (lstDepartments.SelectedIndex >=0)
                query += " AND [" + Transactions.SQL_COL_DEPARTMENT_NUMBER + "] = " + ((Department)lstDepartments.SelectedItem).DepartmentNumber;
            originalQuery[(int)DrillDownType.Base] = query;

            // Dates
            query += " AND CAST([" + Transactions.SQL_COL_DATE + "] as date) BETWEEN '" + ((DateTime)dtFrom.SelectedDate).ToShortDateString() + "' AND '" + ((DateTime)dtTo.SelectedDate).ToShortDateString() + "'";
            originalQuery[(int)DrillDownType.Date] = " AND CAST([" + Transactions.SQL_COL_DATE + "] as date) BETWEEN '" + ((DateTime)dtFrom.SelectedDate).ToShortDateString() + "' AND '" + ((DateTime)dtTo.SelectedDate).ToShortDateString() + "'";

            //Journals
            if (lstJournals.SelectedIndex >= 0)
            {
                query += " AND [" + Transactions.SQL_COL_JOURNAL + "] = '" + ((JournalNames)lstJournals.SelectedItem).JournalName + "'";
                originalQuery[(int)DrillDownType.Journal] = " AND [" + Transactions.SQL_COL_JOURNAL + "] = '" + ((JournalNames)lstJournals.SelectedItem).JournalName + "'";
            }

            // Accounts
            if (lstActiveAccounts.Items.Count > 0)
            {
                first = true;
                string sql = " AND [" + Transactions.SQL_COL_ACCOUNT_NUMBER + "] IN (";
                foreach(Accounts account in lstActiveAccounts.Items)
                {
                    if (!first)
                        sql += ", ";
                    sql += account.Key.ToString();
                    first = false;
                }
                sql += ")";
                query += sql;

                originalQuery[(int)DrillDownType.Account] = sql;
            }

            // Source
            if (!txtSources.Text.Trim().Equals(""))
            {
                string[] ranges = GetRanges(txtSources.Text);

                if (ranges == null)
                {
                    MessageBoxCustom.Show("Invalid Source ranges.", "", MessageBoxButton.OK, this);
                    return false;
                }
                first = true;
                string sql = " AND (";
                foreach (string s in ranges)
                {
                    if (!first)
                        sql += " OR ";
                    sql += Transactions.SQL_COL_SOURCE + " LIKE '%" + s + "%'";
                    first = false;
                }
                sql += ")";
                query += sql;

                originalQuery[(int)DrillDownType.Source] = sql;
            }

            // Description
            if (!txtDescrption.Text.Trim().Equals(""))
                query += " AND [" + Transactions.SQL_COL_DESCRIPTION + "] LIKE ('%" + txtDescrption.Text + "%')";

            // Check Numbers
            if (!txtChecks.Text.Trim().Equals(""))
            {
                string[] ranges = GetRanges(txtChecks.Text, true);
                if (ranges == null)
                {
                    MessageBoxCustom.Show("Invalid Checks Numbers.", "", MessageBoxButton.OK, this);
                    return false;
                }
                string sql = "";
                first = true;
                sql += " AND (";
                foreach (string s in ranges)
                {
                    if (!first)
                        sql += " OR ";
                    sql += "["+ Transactions.SQL_COL_CHECK_NUMBER + "] = " + s;
                    first = false;
                }
                sql += ")";
                query += sql;

                originalQuery[(int)DrillDownType.CheckNumber] = sql;
            }

            // Debits / Credits / Amounts
            if (chkDebits.IsChecked.Value)
            {
                string sql = " AND [" + Transactions.SQL_COL_AMOUNT + "] >= 0 ";
                query += sql;
                originalQuery[(int)DrillDownType.Debits] = sql;
            }
            else if (chkCredits.IsChecked.Value)
            {
                string sql = " AND [" + Transactions.SQL_COL_AMOUNT + "] <= 0";
                query += sql;

                originalQuery[(int)DrillDownType.Credits] = sql;
            }
            else if (!txtAmounts.Text.Trim().Equals(""))
            {
                string sql = "";
                string[] ranges = GetRanges(txtAmounts.Text, true, true);
                if (ranges == null)
                {
                    MessageBoxCustom.Show("Invalid Amount ranges.", "", MessageBoxButton.OK, this);
                    return false;
                }
                first = true;
                sql += " AND (";
                foreach (string s in ranges)
                {
                    if (!first)
                        sql += " OR ";
                    sql += "[" + Transactions.SQL_COL_AMOUNT + "] = " + s;
                    first = false;
                }
                sql += ")";
                query += sql;

                originalQuery[(int)DrillDownType.Amounts] = sql;
            }
            query += " ORDER BY [" + Transactions.SQL_COL_DATE + "]";
            return true;
        }

        private string[] GetRanges(string range, bool validate = false, bool MinusOnly = true)
        {
            if (range == null || range.Trim().Equals(""))
                return null;
            if (validate && !ValidateRange(range, MinusOnly))
                return null;

            ArrayList list = new ArrayList();

            int number;
            string[] ranges = range.Split(',');
            foreach (string s in ranges)
            {
                if (s.Contains("-"))
                {
                    string[] subS = s.Split('-');
                    if ((subS.Length != 2 || subS[0].Trim().Equals("")) && MinusOnly)
                        list.Add(s);
                    break;
                    if (validate)
                        list.Add(" BETWEEN " + subS[0] + " AND " + subS[1]);
                    else
                        list.Add(" BETWEEN '" + subS[0] + "' AND '" + subS[1] + "'");
                }
                else
                {
                    list.Add(s);
                }
            }
            return (string[])list.ToArray(typeof(String));
        }

        private bool ValidateRange(string range, bool MinusOnly = true)
        {
            if (range == null || range.Trim().Equals(""))
                return false;

            int number;
            string[] ranges = range.Split(',');
            foreach (string s in ranges)
            {
                if (!MinusOnly && s.Contains("-"))
                {
                    string[] subS = s.Split('-');
                    if (subS.Length != 2)
                        return false;
                    number = 0;
                    int.TryParse(subS[0], out number);
                    if (number == 0)
                        return false;
                    int.TryParse(subS[1], out number);
                    if (number == 0)
                        return false;
                }
                else
                {
                    number = 0;
                    int.TryParse(s, out number);
                    if (number == 0)
                        return false;
                }
            }
            return true;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAll_Click(object sender, RoutedEventArgs e)
        {
            if (!Search())
                return;
            savedQuery = query;
            GetResults();
        }

        private void GetResults()
        {
            Accounts account;
            Transactions[] trans = Transactions.load(query);
            if (trans == null)
            {
                MessageBoxCustom.Show("No Results.", "", MessageBoxButton.OK, this);
                return;
            }
            ArrayList list = new ArrayList();
            foreach (Transactions tran in trans)
            {
                Results result = new Results();
                result.IndexNumber = tran.IndexNumber;
                result.Date = tran.Date;
                account = Accounts.load(tran.AccountNumber);
                if (account != null)
                {
                    result.Account = account.Description;
                    result.AccountNumber = account.Key;
                }
                result.CheckNumber = tran.CheckNumber;
                result.Journal = tran.Journal;
                result.Source = tran.Source;
                result.Description = tran.Description;
                if (tran.Amount > 0)
                    result.Debit = tran.Amount;
                else
                    result.Credit = -tran.Amount;
                list.Add(result);
            }
            txtResultsType.Text = "General Transactions";
            grdResults.Columns[grdResults.Columns.Count - 1].Visibility = Visibility.Collapsed;
            grdResults.ItemsSource = null;
            grdResults.ItemsSource = list;
            tabResults.Focus();

        }

        private void cmdOrphaned_Click(object sender, RoutedEventArgs e)
        {
            if (GetOrphanAccts())
            {
                txtResultsType.Text = "Orpaned Transactions";
                grdResults.Columns[grdResults.Columns.Count - 1].Visibility = Visibility.Visible;
                grdResults.ItemsSource = null;
                grdResults.ItemsSource = missing;
                tabResults.Focus();
            }
        }

        public bool GetOrphanAccts()
		{
            if (lstCompanies.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select a Company.", "", MessageBoxButton.OK, this);
                return false;
            }
            if (lstDepartments.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You must select a Department.", "", MessageBoxButton.OK, this);
                return false;
            }

            object j = null;
			object t = null;
			object m = null;
			string vendorName = "";
			string xsql;

			ApChecks[] tblOrphanVend = null;
            decimal credit = 0;
            decimal debit = 0;

            Accounts[] accts = Accounts.load();
            string accounts = "";
            bool first = true;
            foreach(Accounts acct in accts)
            {
                if (!first)
                    accounts += ", ";
                accounts += acct.Key;
                first = false;
            }
            //From the Check Register
            int i = -1;
			xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE [" + ApChecks.SQL_COL_SEQUENCE + "] <> 0 AND [" + ApChecks.SQL_COL_COMPANY + "] = " + ((Company)lstCompanies.SelectedItem).CompanyNumber.ToString() + 
                    " AND [" + ApChecks.SQL_COL_DEPARTMENT + "] = " + ((Department)lstDepartments.SelectedItem).DepartmentNumber + " AND [" + ApChecks.SQL_COL_VENDOR_ACCOUNT + "] NOT IN (" + accounts + ")";
			ApChecks[] checks = ApChecks.LoadQuery(xsql);
            if (checks != null && checks.Length > 0)
            {
                Vendors tblVend;
                Accounts tblAccounts;
                bool found;
                foreach (ApChecks check in checks)
                {
                    xsql = "SELECT * FROM " + ApChecks.TABLE + " WHERE [" + ApChecks.SQL_COL_COMPANY + "] = " + check.Company.ToString();
                    xsql += " AND [" + ApChecks.SQL_COL_DEPARTMENT + "] = " + check.Department.ToString() + " AND [" + ApChecks.SQL_COL_CHECK_NUMBER + "]= ";
                    xsql += check.CheckNumber.ToString() + " AND Sequence = 0";
                    tblOrphanVend = ApChecks.LoadQuery(xsql);
                    if (tblOrphanVend != null && tblOrphanVend.Length > 0)
                    {
                        tblVend = Vendors.load(tblOrphanVend[0].VendorAccount);
                        if (tblVend != null)
                        {
                            vendorName = tblVend.VendorName;
                            tblVend = null;
                        }
                        else
                            vendorName = "";
                    }
                    found = false;
                    tblAccounts = Accounts.load(check.VendorAccount);
                    if (tblAccounts != null)
                    {
                        credit = 0;
                        debit = 0;
                        if (check.Amount > 0)
                            debit = check.Amount;
                        else
                            credit = -check.Amount;
                        foreach (Results result in missing)
                        {
                            if (result.AccountNumber == check.VendorAccount)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                        missing.Add(new Results() { AccountNumber = check.VendorAccount, Account = check.VendorAccount.ToString(), Journal = check.Journal, CheckNumber = check.CheckNumber, Date = check.Date, Description = "REG: " + vendorName, Credit = credit, Debit = debit });
                }
                checks = null;
            }
            //From the Transactions table
            xsql = "SELECT * FROM " + Transactions.TABLE + " WHERE [" +
                                    Transactions.SQL_COL_COMPANY_NUMBER + "] = " + ((Company)lstCompanies.SelectedItem).CompanyNumber.ToString() +
                                    " AND [" + Transactions.SQL_COL_DEPARTMENT_NUMBER + "] = " + ((Department)lstDepartments.SelectedItem).DepartmentNumber +
                                    " AND [" + Transactions.SQL_COL_ACCOUNT_NUMBER + "] NOT IN (" + accounts + ")";

            Transactions[] trans = Transactions.load(xsql);
			if (trans != null && trans.Length > 0) {
                bool found;
                foreach (Transactions tran in trans) {
					found = false;
					foreach (Results result in missing)
                    {
                        if (result.AccountNumber == tran.AccountNumber)
                        {
                            found = true;
                            break;
                        }
					}
					if (!found) {
                        credit = 0;
                        debit = 0;
                        if (tran.Amount > 0)
                            debit = tran.Amount;
                        else
                            credit = -tran.Amount;
                        missing.Add(new Results() { AccountNumber = tran.AccountNumber, Account = tran.AccountNumber.ToString(), Journal = tran.Journal, Source = tran.Source, CheckNumber = tran.CheckNumber, Date = tran.Date,  Description = "TRN: " + tran.Description, Credit = credit, Debit = debit });
                        found = true;
					}
				}
                trans = null;
            }
            return true;
        }


        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies.SelectedIndex >= 0)
            {
                Company company = (Company)lstCompanies.SelectedItem;
                if (company != null)
                {
                    Department[] departments = Department.loadByCompany(company.CompanyNumber);
                    lstDepartments.ItemsSource = null;
                    lstDepartments.ItemsSource = departments;
                    if (departments.Length == 1)
                        lstDepartments.SelectedIndex = 0;
                }
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            lstCompanies.SelectedIndex = -1;
            lstDepartments.SelectedIndex = -1;
            lstJournals.SelectedIndex = -1;
            lstActiveAccounts.Items.Clear();
            lstAccounts.SelectedIndex = -1;
            txtSources.Text = "";
            txtDescrption.Text = "";
            txtChecks.Text = "";
            txtAmounts.Text = "";
        }

        private void cmdAddAccount_Click(object sender, RoutedEventArgs e)
        {
            if (lstAccounts.SelectedIndex < 0)
                return;
            bool found = false;
            Accounts account = (Accounts)lstAccounts.SelectedItem;
            if (account != null)
            {
                if (lstActiveAccounts.Items.Count > 0)
                {
                    foreach (Accounts acct in lstActiveAccounts.Items)
                    {
                        if (account == acct)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                    lstActiveAccounts.Items.Add(account);
            }
        }

        private void cmdDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (lstActiveAccounts.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("No Account selected for removal.");
                return;
            }
            lstActiveAccounts.Items.RemoveAt(lstActiveAccounts.SelectedIndex);
        }

        private void ctxDate_Click(object sender, RoutedEventArgs e)
        {
            Results result = (Results)grdResults.SelectedItem;
            if (result != null)
            {
                string sql = originalQuery[(int)DrillDownType.Base];
                sql += query += "CAST([" + Transactions.SQL_COL_DATE + "] as date) = '" + result.Date.ToShortDateString();

            }
        }

        private void ctxRevert_Click(object sender, RoutedEventArgs e)
        {
            query = savedQuery;
            GetResults();
        }

        private void ctxFetch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Results result = (Results)grdResults.SelectedItem;
                if (result != null)
                    if (Search(true, result))
                        GetResults();
            }
            catch { }
        }

        private void ctxOriginalDates_Checked(object sender, RoutedEventArgs e)
        {
            if (ctxOriginalDates.IsChecked)
                ctxDate.IsChecked = false;
        }

        private void ctxDate_Checked(object sender, RoutedEventArgs e)
        {
            if (ctxDate.IsChecked)
                ctxOriginalDates.IsChecked = false;
        }

        private void ctxOriginalAccount_Checked(object sender, RoutedEventArgs e)
        {
            if (ctxOriginalAccount.IsChecked)
                ctxAccount.IsChecked = false;
        }

        private void ctxAccount_Click(object sender, RoutedEventArgs e)
        {
            if (ctxAccount.IsChecked)
                ctxOriginalAccount.IsChecked = false;
        }

        private void ctxAmount_Click(object sender, RoutedEventArgs e)
        {
            if (ctxAmount.IsChecked)
                ctxOriginalAmount.IsChecked = false;
        }

        private void ctxOriginalAmount_Click(object sender, RoutedEventArgs e)
        {
            if (ctxOriginalAmount.IsChecked)
                ctxAmount.IsChecked = false;
        }

        private void ctxCheckNumber_Click(object sender, RoutedEventArgs e)
        {
            if (ctxCheckNumber.IsChecked)
                ctxOriginalCheckNumber.IsChecked = false;
        }

        private void ctxOriginalCheckNumber_Click(object sender, RoutedEventArgs e)
        {
            if (ctxOriginalCheckNumber.IsChecked)
                ctxCheckNumber.IsChecked = false;
        }

        private void chkDebits_Checked(object sender, RoutedEventArgs e)
        {
            if (chkDebits.IsChecked.Value)
            {
                chkCredits.IsChecked = false;
                txtAmounts.Text = "";
            }
        }

        private void chkCredits_Checked(object sender, RoutedEventArgs e)
        {
            if (chkCredits.IsChecked.Value)
            {
                chkDebits.IsChecked = false;
                txtAmounts.Text = "";
            }
        }

        private void txtAmounts_TextChanged(object sender, TextChangedEventArgs e)
        {
            chkDebits.IsChecked = false;
            chkCredits.IsChecked = false;
        }

        private void CtxEdit_Click(object sender, RoutedEventArgs e)
        {
            if (grdResults.SelectedItem != null)
            {
                Results result = (Results)grdResults.SelectedItem;
                if (result != null)
                {
                    frmTransactionEdit frm = new frmTransactionEdit(result.IndexNumber.ToString()) { temp = false };
                    frm.ShowDialog();
                    GetResults();
                }
            }
        }
    }

    public class Results
    {
        public DateTime Date { get; set; }
        public int AccountNumber { get; set; }
        public string Account { get; set; }
        public int CheckNumber { get; set; }
        public string Journal { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public int IndexNumber { get; set; }

    }
}
