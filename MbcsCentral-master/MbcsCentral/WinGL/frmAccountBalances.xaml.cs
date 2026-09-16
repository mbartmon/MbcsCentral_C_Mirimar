using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmAccountBalances.xaml
    /// </summary>
    public partial class frmAccountBalances : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Company[] comps = null;
        Department[] depts = null;
        Accounts[] accts = null;
        Company cmp;
        Department dpt;
        DateTime StartDate;
        DateTime EndDate;
        DateTime PriorStartDate;
        DateTime PriorEndDate;
        DateTime StartYear;
        DateTime EndYear;
        int[] Years = new int[19];
        double DefaultTab = 0;
        const string PROGRAM_REG_KEY = "\\AccountBalances\\";
        const string PROGRAM_REG_SUBKEY = "ExcludedCompanies";
        List<ExcludedCompanies> ExCmps = new List<ExcludedCompanies>();  
        bool Loading = true;
        string thisApp;

        public frmAccountBalances()
        {
            InitializeComponent();
        }

        public frmAccountBalances(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frmAccountBalances_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            txtExclusions.DataContext = this;

            DefaultTab = (double)Main.GetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "DefaultAccountBalance", 0);
            //System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyNegativePattern = 1;
            comps = Company.load(true);
            lstCompanies_Company.ItemsSource = comps;
            lstCompanies_CompanyDepartment.ItemsSource = comps;
            accts = Accounts.load(false, false);
            lstAccounts_Company.ItemsSource = accts;
            lstAccounts_CompanyDepartment.ItemsSource = accts;
            lstAccounts_Account.ItemsSource = accts;
            lstAccounts_Account2.ItemsSource = accts;

            string Ex_Companies = (string)Main.GetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, PROGRAM_REG_SUBKEY, "");
            if (Ex_Companies != null && !Ex_Companies.Trim().Equals(""))
            {
                string[] companies = Ex_Companies.Split(',');
                if (companies.Length > 0)
                {
                    foreach (string cmp in companies)
                    {
                        int c = 0;
                        int.TryParse(cmp, out c);
                        if (c > 0)
                        {
                            Company company = Company.loadByCompany(c);
                            if (company != null)
                                ExCmps.Add(new ExcludedCompanies() { CompanyName = company.Name, CompanyNumber = c });
                        }
                    }
                }
                txtNumExcluded.Text = ExCmps.Count == 0 ? "" : ExCmps.Count.ToString();
                if (ExCmps.Count > 0)
                {
                    lstCompanyExclusions.ItemsSource = ExCmps;
                    InUse = true;
                }
            }

            int i;

            int year = DateTime.Now.Year - 17;
            for (i = 0; i < 19; i++)
            {
                Years[i] = year++;
            }
            lstYears_Company.ItemsSource = Years;
            lstYears_Company.SelectedIndex = 17;
            lstBaseYears.ItemsSource = Years;
            lstBaseYears.SelectedIndex = 17;
            int month = DateTime.Now.Month;
            lstMonths_Account.SelectedIndex = month - 1;
            lstMonths_Account2.SelectedIndex = month - 1;

            DateTime? sy;
            DateTime? ey;
            Transactions.LoadByDateRange(out sy, out ey);
            DataGridTemplateColumn col;
            DataTemplate dataTemplate;
            var tb = new FrameworkElementFactory();
            Binding bind;
            if (sy != null && ey != null)
            {
                StartYear = (DateTime)sy;
                EndYear = (DateTime)ey;
                if (EndYear.Year < DateTime.Now.Year || EndYear.Year > DateTime.Now.Year)
                    EndYear = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                int startyear = StartYear.Year;
                int endyear = EndYear.Year;
                if (endyear - startyear > 10)
                    startyear = endyear - 9;
                i = 10;

                // Build years grid columns for By Account
                for (int y = endyear; y >= startyear; y--)
                {
                    col = new DataGridTemplateColumn();
                    col.Header = y.ToString("####");
                    col.Width = 100;
                    dataTemplate = new DataTemplate();
                    tb = new FrameworkElementFactory(typeof(TextBlock));
                    tb.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                    tb.SetValue(MarginProperty, new Thickness(0, 0, 4, 0));
                    bind = new Binding();
                    bind.Path = new System.Windows.PropertyPath("Year" + i.ToString("#0"));
                    bind.Converter = new CurrencyPaddingConverter();
                    bind.ConverterParameter = "14c";
                    tb.SetValue(TextBlock.TextProperty, bind);
                    dataTemplate.VisualTree = tb;
                    col.CellTemplate = dataTemplate;
                    grdAccount_Long.Columns.Add(col);
                    i--;
                }
            }

            // Build years grid columns for By Account 2 years
            i = 1;
            for (int y = DateTime.Now.Year; y >= DateTime.Now.Year - 1; y--)
            {
                col = new DataGridTemplateColumn(); 
                col.Header = y.ToString("####");
                col.Width = 100;
                dataTemplate = new DataTemplate();
                tb = new FrameworkElementFactory(typeof(TextBlock));
                tb.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                tb.SetValue(MarginProperty, new Thickness(0, 0, 4, 0));
                bind = new Binding();
                bind.Path = new System.Windows.PropertyPath("Year" + i.ToString("#0"));
                bind.Converter = new CurrencyPaddingConverter();
                bind.ConverterParameter = "14c";
                tb.SetValue(TextBlock.TextProperty, bind);
                dataTemplate.VisualTree = tb;
                col.CellTemplate = dataTemplate;
                grdAccount_Long2.Columns.Add(col);
                i++;
            }
            col = new DataGridTemplateColumn();
            col.Header = "Difference";
            col.Width = 100;
            dataTemplate = new DataTemplate();
            tb = new FrameworkElementFactory(typeof(TextBlock));
            tb.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
            tb.SetValue(MarginProperty, new Thickness(0, 0, 4, 0));
            bind = new Binding();
            bind.Path = new System.Windows.PropertyPath("Year3");
            bind.Converter = new CurrencyPaddingConverter();
            bind.ConverterParameter = "14c";
            tb.SetValue(TextBlock.TextProperty, bind);
            dataTemplate.VisualTree = tb;
            col.CellTemplate = dataTemplate;
            grdAccount_Long2.Columns.Add(col);

            for (int j = 0; j < tabBalances.Items.Count; j++)
            {
                if (j == DefaultTab)
                {
                    tabBalances.SelectedIndex = j;

                    switch (j)
                    {
                        case 0:
                            chkByCompany.IsChecked = true;
                            break;
                        case 1:
                            chkByCmpDept.IsChecked = true;
                            break;
                        case 2:
                            chkByAccount.IsChecked = true;
                            break;
                        case 3:
                            chkByAcct2.IsChecked = true;
                            break;
                    }
                }
            }
            Loading = false;
        }

        private void frmAccountBalances_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Main.SetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "DefaultAccountBalance", DefaultTab, (int)MbcsCentral.Registry.DataTypes.Dword);
            if (ExCmps == null || ExCmps.Count == 0)
            {
                Main.SetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, PROGRAM_REG_SUBKEY, "", (int)MbcsCentral.Registry.DataTypes.String);
            }
            else
            {
                string s = "";
                for(int i = 0; i < ExCmps.Count; i++)
                {
                    if (i > 0 )
                        s += ",";
                    s += ExCmps[i].CompanyNumber.ToString();
                }
                Main.SetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, PROGRAM_REG_SUBKEY, s, (int)MbcsCentral.Registry.DataTypes.String);
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstCompanies_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies_Company.SelectedIndex >= 0)
            {
                cmp = (Company)lstCompanies_Company.SelectedItem;
                depts = Department.loadByCompany(cmp.CompanyNumber);
                lstAccounts_Company_SelectionChanged(sender, e);
            }
        }
        private void lstMonths_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstAccounts_Company_SelectionChanged(sender, e);

        }

        private void lstAccounts_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccounts_Company.SelectedItem != null && depts.Length > 0)
            {
                if (lstMonths_Company.SelectedIndex < 0)
                {
                    return;
                }
                if (lstYears_Company.SelectedIndex < 0)
                {
                    return;
                }
                grdAccount.ItemsSource = null;
                int month = lstMonths_Company.SelectedIndex + 1;
                int year = (int)lstYears_Company.SelectedItem;
                Accounts acct = (Accounts)lstAccounts_Company.SelectedItem;
                BalanceRow[] balances = new BalanceRow[depts.Length];
                StartDate = new DateTime(year, 1, 1);
                EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                PriorStartDate = new DateTime(year - 1, 1, 1);
                PriorEndDate = new DateTime(year - 1, month, DateTime.DaysInMonth(year - 1, month));
                for (int i = 0; i < depts.Length; i++)
                {
                    balances[i] = new BalanceRow() { Department = depts[i].DepartmentNumber, Name = depts[i].Name };
                    balances[i].Current = Transactions.AccountBalance(cmp.CompanyNumber, acct.Key, StartDate, EndDate, depts[i].DepartmentNumber);
                    balances[i].Prior = Transactions.AccountBalance(cmp.CompanyNumber, acct.Key, PriorStartDate, PriorEndDate, depts[i].DepartmentNumber);
                }
                grdAccount.ItemsSource = balances;
            }
        }

        private void lstCompanies_CompanyDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies_CompanyDepartment.SelectedIndex >= 0)
            {
                cmp = (Company)lstCompanies_CompanyDepartment.SelectedItem;
                depts = Department.loadByCompany(cmp.CompanyNumber);
                lstDepartments_CompanyDepartment.ItemsSource = depts;
                lstDepartments_CompanyDepartment.SelectedIndex = 0;
                DisplayCompanyDepartmentGrid();
            }
        }

        private void lstDepartments_CompanyDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDepartments_CompanyDepartment.SelectedItem == null)
                return;
            dpt = (Department)lstDepartments_CompanyDepartment.SelectedItem;
            DisplayCompanyDepartmentGrid();
        }

        private void lstAccounts_CompanyDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccounts_CompanyDepartment.SelectedItem == null)
                return;
            DisplayCompanyDepartmentGrid();
        }

        private void DisplayCompanyDepartmentGrid()
        {
            if (lstCompanies_CompanyDepartment.SelectedItem == null || lstDepartments_CompanyDepartment.SelectedItem == null || lstAccounts_CompanyDepartment.SelectedItem == null)
                return;

            grdCompanyDepartment.ItemsSource = null;
            Accounts acct = (Accounts)lstAccounts_CompanyDepartment.SelectedItem;
            BalanceRow[] balances = new BalanceRow[12];
            decimal CurrentYtd = 0;
            decimal PriorYtd = 0;
            int year = DateTime.Now.Year;
            for (int m = 1; m < 13; m++)
            {
                StartDate = new DateTime(year, m, 1);
                EndDate = new DateTime(year, m, DateTime.DaysInMonth(year, m));
                PriorStartDate = new DateTime(year - 1, m, 1);
                PriorEndDate = new DateTime(year - 1, m, DateTime.DaysInMonth(year - 1, m));
                balances[m - 1] = new BalanceRow();
                balances[m - 1].Current = Transactions.AccountBalance(cmp.CompanyNumber, acct.Key, StartDate, EndDate, depts[0].DepartmentNumber);
                CurrentYtd += balances[m - 1].Current;
                balances[m - 1].CurrentYtd = CurrentYtd;
                balances[m - 1].Prior = Transactions.AccountBalance(cmp.CompanyNumber, acct.Key, PriorStartDate, PriorEndDate, depts[0].DepartmentNumber);
                PriorYtd += balances[m - 1].Prior;
                balances[m - 1].PriorYtd = PriorYtd;
                balances[m - 1].Month = new DateTime(year, m, 1).ToString("MMMM");
            }
            grdCompanyDepartment.ItemsSource = balances;
        }

        private void lstAccounts_Account_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccounts_Account.SelectedIndex >= 0)
                DisplayAccountGrid();
        }

        private void lstMonths_Account_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMonths_Account.SelectedIndex >= 0)
                DisplayAccountGrid();
        }

        private void DisplayAccountGrid()
        {
            if (lstAccounts_Account.SelectedItem == null || lstMonths_Account.SelectedItem == null)
                return;
            ComboBoxItem cb = (ComboBoxItem)lstMonths_Account.SelectedItem;
            string month = (string)cb.Content;
            int m = lstMonths_Account.SelectedIndex + 1;
            Accounts acct = (Accounts)lstAccounts_Account.SelectedItem;
            BalanceRow[] balances = new BalanceRow[0];
            BalanceRow balance = null;
            decimal[] totals = new decimal[10];
            decimal bal;
            int r = 0;
            foreach (Company comp in comps)
            {
                if (ExCmps.Find(c => c.CompanyNumber == comp.CompanyNumber) != null)
                    continue;

                depts = Department.loadByCompany(comp.CompanyNumber);
                if (depts == null || depts.Length == 0)
                    continue;
                foreach (Department dept in depts)
                {
                    Array.Resize<BalanceRow>(ref balances, ++r);
                    balances[r - 1] = new BalanceRow() { CompanyName = comp.Name, Department = dept.DepartmentNumber, Name = dept.Name, Month = month, CompanyNumber = comp.CompanyNumber };

                    int c = 10;
                    for (int y = EndYear.Year; y > StartYear.Year; y--)
                    {
                        if (c == 0)
                            break;
                        bal = Transactions.AccountBalance(comp.CompanyNumber, acct.Key, new DateTime(y, 1, 1), new DateTime(y, m, DateTime.DaysInMonth(y, m)), dept.DepartmentNumber);
                        balances[r-1].GetType().GetProperty("Year" + c.ToString("##")).SetValue(balances[r-1], bal);
                        totals[c - 1] += bal;
                        c--;
                    }
                }
            }
            int rows = balances.Length;
            Array.Resize<BalanceRow>(ref balances, rows + 1);
            balances[rows] = new BalanceRow() { Name = "TOTALS", Month = month };
            for (int c = 0; c < 10; c++)
                balances[rows].GetType().GetProperty("Year" + (c + 1).ToString("##")).SetValue(balances[rows], totals[c]);

            grdAccount_Long.ItemsSource = balances;
            grdAccount_Long.UpdateLayout();
            DataGridRow gRow = grdAccount_Long.ItemContainerGenerator.ContainerFromIndex(rows) as DataGridRow;
            if(gRow != null)
                gRow.Background = new SolidColorBrush(Colors.Bisque);

        }

        private void lstAccounts_Account2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccounts_Account2.SelectedItem != null)
            {
                int y = (int)lstBaseYears.SelectedItem;
                EndYear = new DateTime(y, 1, 1);
                DisplayAccount2Grid();
            }
        }

        private void lstMonths_Account2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMonths_Account2.SelectedItem != null)
                DisplayAccount2Grid();
        }

        private void DisplayAccount2Grid()
        {
            if (lstAccounts_Account2.SelectedItem == null || lstMonths_Account2.SelectedItem == null)
                return;
            ComboBoxItem cb = (ComboBoxItem)lstMonths_Account2.SelectedItem;
            string month = (string)cb.Content;
            int m = lstMonths_Account2.SelectedIndex + 1;
            Accounts acct = (Accounts)lstAccounts_Account2.SelectedItem;
            BalanceRow[] balances = new BalanceRow[0];
            BalanceRow balance = null;
            decimal balCurrent;
            decimal balPrior;
            decimal balCurrentTotal = 0;
            decimal balPriorTotal = 0;

            int r = 0;
            foreach (Company comp in comps)
            {
                if (ExCmps.Find(c => c.CompanyNumber == comp.CompanyNumber) != null)
                    continue;

                depts = Department.loadByCompany(comp.CompanyNumber);
                if (depts == null || depts.Length == 0)
                    continue;
                foreach (Department dept in depts)
                {
                    Array.Resize<BalanceRow>(ref balances, ++r);
                    balances[r - 1] = new BalanceRow() { CompanyName = comp.Name, Department = dept.DepartmentNumber, Name = dept.Name, Month = month, CompanyNumber = comp.CompanyNumber };

                    balCurrent = Transactions.AccountBalance(comp.CompanyNumber, acct.Key, new DateTime(EndYear.Year, 1, 1), new DateTime(EndYear.Year, m, DateTime.DaysInMonth(EndYear.Year, m)), dept.DepartmentNumber);
                    balCurrentTotal += balCurrent;
                    balances[r - 1].GetType().GetProperty("Year1").SetValue(balances[r - 1], balCurrent);

                    balPrior = Transactions.AccountBalance(comp.CompanyNumber, acct.Key, new DateTime(EndYear.Year - 1, 1, 1), new DateTime(EndYear.Year - 1, m, DateTime.DaysInMonth(EndYear.Year - 1, m)), dept.DepartmentNumber);
                    balPriorTotal += balPrior;
                    balances[r - 1].GetType().GetProperty("Year2").SetValue(balances[r - 1], balPrior);

                    balances[r - 1].GetType().GetProperty("Year3").SetValue(balances[r - 1], balCurrent - balPrior);
                }
            }
            Array.Resize<BalanceRow>(ref balances, ++r);
            balances[r - 1] = new BalanceRow() { CompanyName = "", Department = 0, Name = "TOTALS=>", Month = month };
            balances[r - 1].GetType().GetProperty("Year1").SetValue(balances[r - 1], balCurrentTotal);

            balances[r - 1].GetType().GetProperty("Year2").SetValue(balances[r - 1], balPriorTotal);

            balances[r - 1].GetType().GetProperty("Year3").SetValue(balances[r - 1], balCurrentTotal - balPriorTotal);
            grdAccount_Long2.ItemsSource = balances;

        }
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool _inUse = false;

        public bool InUse
        {
            get { return _inUse; }
            set 
            { 
                _inUse = value;
                OnPropertyChanged("InUse");
            }
        }
        private int _numInUse;

        public int NumInUse
        {
            get { return _numInUse; }
            set 
            { 
                _numInUse = value;
                OnPropertyChanged("NumInUse");
            }
        }



        public class BalanceRow
        {
            public string CompanyName { get; set; }
            public short Department { get; set; }
            public string Name { get; set; }
            public decimal Current { get; set; }
            public decimal CurrentYtd { get; set; }
            public decimal Prior { get; set; }
            public decimal PriorYtd { get; set; }
            public decimal Difference { get; set; }
            public decimal Year1 { get; set; }
            public decimal Year2 { get; set; }
            public decimal Year3 { get; set; }
            public decimal Year4 { get; set; }
            public decimal Year5 { get; set; }
            public decimal Year6 { get; set; }
            public decimal Year7 { get; set; }
            public decimal Year8 { get; set; }
            public decimal Year9 { get; set; }
            public decimal Year10 { get; set; }
            public string Month { get; set; }
            public int CompanyNumber { get; set; }  
        }

        public class ExcludedCompanies
        {
            public string CompanyName { get; set; }
            public int CompanyNumber { get; set; }
        }
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Main.PrintWindow(this, System.Printing.PageOrientation.Landscape);

        }

        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            comps = Company.load(true, true);
            lstCompanies_Company.ItemsSource = comps;
            lstCompanies_CompanyDepartment.ItemsSource = comps;
            TabItem item = (TabItem)tabBalances.SelectedItem;
            if (item.Name.Equals("tabAccount"))
                DisplayAccountGrid();
            if (item.Name.Equals("tabAccount2Years"))
                DisplayAccount2Grid();
                

        }

        private void chkActive_Unchecked(object sender, RoutedEventArgs e)
        {
            comps = Company.load(true);
            lstCompanies_Company.ItemsSource = comps;
            lstCompanies_CompanyDepartment.ItemsSource = comps;
            TabItem item = (TabItem)tabBalances.SelectedItem;
            if (item.Name.Equals("tabAccount"))
                DisplayAccountGrid();
            if (item.Name.Equals("tabAccount2Years"))
                DisplayAccount2Grid();

        }

        private void chkActiveAccounts_Checked(object sender, RoutedEventArgs e)
        {
            lstAccounts_Company.ItemsSource = null;
            lstAccounts_CompanyDepartment.ItemsSource = null;
            lstAccounts_Account.ItemsSource = null;
            lstAccounts_Account2.ItemsSource = null;
            accts = Accounts.load();
            lstAccounts_Company.ItemsSource = accts;
            lstAccounts_CompanyDepartment.ItemsSource = accts;
            lstAccounts_Account.ItemsSource = accts;
            lstAccounts_Account2.ItemsSource = accts;
        }

        private void chkActiveAccounts_Unchecked(object sender, RoutedEventArgs e)
        {
            lstAccounts_Company.ItemsSource = null;
            lstAccounts_CompanyDepartment.ItemsSource = null;
            lstAccounts_Account.ItemsSource = null;
            lstAccounts_Account2.ItemsSource = null;
            accts = Accounts.load(false, false);
            lstAccounts_Company.ItemsSource = accts;
            lstAccounts_CompanyDepartment.ItemsSource = accts;
            lstAccounts_Account.ItemsSource = accts;
            lstAccounts_Account2.ItemsSource = accts;

        }

        private void chkTab_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk != null && !chk.IsChecked.Value)
            {
                DefaultTab = 0;
                return;
            }
            if (chk != null && chk.IsChecked.Value)
            {
                switch (chk.Name.ToLower())
                {
                    case "chkbycompany":
                        
                        DefaultTab = 0;
                        break;
                    case "chkbycmpdept":
                        DefaultTab = 1;
                        break;
                    case "chkbyaccount":
                        DefaultTab = 2;
                        break;
                    case "chkbyacct2":
                        DefaultTab = 3;
                        break;
                }
                ClearOtherCheckBoxes(chk);
            }
        }

        private void ClearOtherCheckBoxes(CheckBox chk)
        {
            if (chk.Name != chkByCompany.Name)
                chkByCompany.IsChecked = false;
            if (chk.Name != chkByCmpDept.Name)
                chkByCmpDept.IsChecked = false;
            if (chk.Name != chkByAccount.Name)
                chkByAccount.IsChecked = false;
            if (chk.Name != chkByAcct2.Name)
                chkByAcct2.IsChecked = false;
        }

        private void lstYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Loading)
                return;
            int y = (int)lstBaseYears.SelectedItem;
            EndYear = new DateTime(y, 1, 1);
            for (int i = grdAccount_Long2.Columns.Count - 3; i < grdAccount_Long2.Columns.Count - 1; i++)
            {
                grdAccount_Long2.Columns[i].Header = y.ToString("####");
                y--;
            }
            DisplayAccount2Grid();
        }

        private void ctxExcludeLong_Click(object sender, RoutedEventArgs e)
        {
            BalanceRow row = (BalanceRow)grdAccount_Long.SelectedItem;
            if (ExCmps.Find(c => c.CompanyNumber == row.CompanyNumber) == null)
            {
                ExCmps.Add(new ExcludedCompanies() { CompanyNumber = row.CompanyNumber, CompanyName = row.CompanyName });
                lstCompanyExclusions.ItemsSource = null;
                lstCompanyExclusions.ItemsSource = ExCmps;
                InUse = true;   
                DisplayAccountGrid();
                if (grdAccount_Long2 != null && grdAccount_Long2.Items.Count > 0)
                    DisplayAccount2Grid();
            }
            txtNumExcluded.Text = ExCmps.Count == 0 ? "" : ExCmps.Count.ToString();
        }

        private void ctxExcludeLong2_Click(object sender, RoutedEventArgs e)
        {
            BalanceRow row = (BalanceRow)grdAccount_Long2.SelectedItem;
            if (ExCmps.Find(c => c.CompanyNumber == row.CompanyNumber) == null)
            {
                ExCmps.Add(new ExcludedCompanies() { CompanyNumber = row.CompanyNumber, CompanyName = row.CompanyName });
                lstCompanyExclusions.ItemsSource= null;
                lstCompanyExclusions.ItemsSource = ExCmps;
                DisplayAccount2Grid();
                InUse = true;
                if (grdAccount_Long != null && grdAccount_Long.Items.Count > 0)
                    DisplayAccountGrid();
            }
            txtNumExcluded.Text = ExCmps.Count == 0 ? "" : ExCmps.Count.ToString();

        }

        private void ctxUnExclude_Click(object sender, RoutedEventArgs e)
        {

            int i = lstCompanyExclusions.SelectedIndex;
            if (i >= 0)
            {
                ExCmps.RemoveAt(i);
                lstCompanyExclusions.ItemsSource = null;
                lstCompanyExclusions.ItemsSource = ExCmps;
                if (grdAccount_Long2 != null && grdAccount_Long2.Items.Count > 0)
                    DisplayAccount2Grid();
                if (grdAccount_Long != null && grdAccount_Long.Items.Count > 0)
                    DisplayAccountGrid();
            }
            txtNumExcluded.Text = ExCmps.Count == 0 ? "" : ExCmps.Count.ToString();
            if (ExCmps.Count == 0)
                InUse = false;
            else
                InUse = true;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] message = {"Right Click Company Name in result list to add to exclude list."," ", "Right Click Company in Exclude List to UN-exclude it."};
            MessageBoxCustom.Show(message, "Company Exclusion Help",Brushes.Wheat,MessageBoxButton.OK,this);
        }
    }
}
