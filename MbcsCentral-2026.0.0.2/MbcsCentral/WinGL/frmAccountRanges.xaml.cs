using System;
using System.Collections.Generic;
using System.Collections;
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
    /// Interaction logic for frmAccountRanges.xaml
    /// </summary>
    public partial class frmAccountRanges : Window
    {
        ArrayList acctList = new ArrayList();
        private string this_App;

        /// <summary>
        /// 0=All
        /// 1=Assets
        /// 2=Liabilities
        /// 3=Equities
        /// 4=Income
        /// 5=Expenses
        /// 6=Assets,Expenses
        /// 7=Assets,Income
        /// 8=Assets,Equity
        /// 9=Liability,Expenses
        /// </summary>
        public int AccountCategories { get; set; } = 0;

        public frmAccountRanges()
        {
            InitializeComponent();
        }

        public frmAccountRanges(string app)
        {
            InitializeComponent();
            this_App = app;
        }

        private void frmAccountRanges_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            GetAccounts();
        }

        private void frmAccountRanges_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void GetAccounts()
        {
            string xcat = "";
            Category cat;
            int r = 0;

            switch (AccountCategories)
            {
                case (int)Category.MajorTypes.Assets:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Assets];
                    break;
                case (int)Category.MajorTypes.Liabilities:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Liabilities];
                    break;
                case (int)Category.MajorTypes.Equities:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Equities];
                    break;
                case (int)Category.MajorTypes.Income:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Income];
                    break;
                case (int)Category.MajorTypes.Expenses:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Expenses];
                    break;
                case 6:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Assets];
                    category2 = Category.MajorKeys[(int)Category.MajorTypes.Expenses];
                    break;
                case 7:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Assets];
                    category2 = Category.MajorKeys[(int)Category.MajorTypes.Income];
                    break;
                case 8:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Assets];
                    category2 = Category.MajorKeys[(int)Category.MajorTypes.Equities];
                    break;
                case 9:
                    category1 = Category.MajorKeys[(int)Category.MajorTypes.Assets];
                    category2 = Category.MajorKeys[(int)Category.MajorTypes.Expenses];
                    break;
            }

            Accounts[] accts = Accounts.loadByCategories(category1, category2);
            if (accts == null || accts.Length == 0)
            {
                MessageBoxCustom.Show("No General Ledger Accounts found.");
                return;
            }
            foreach(Accounts acct in accts)
            {
                if (!xcat.Equals(acct.MajorCategory))
                {
                    cat = Category.load(acct.MajorCategory);
                    if (cat != null)
                    {
                        acctList.Add(new Accounts() { AccountNumber = 0, MajorCategory = cat.Key, Description = "<< ".PadLeft(30) + cat.CategoryDescription + " >>".PadRight(95), RangeCatFlag = true });

                    }
                    xcat = acct.MajorCategory;
                }
                AccountRanges range = AccountRanges.load(ReportType.Trim() + ":" + ReportName.Trim(), ReportSection, acct.Key);
                if (range != null)
                {
                    acct.RangeUseFlag = true;
                    Saved = true;
                }
                acctList.Add(acct);
            }
            lstAccounts.ItemsSource = acctList;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(this_App))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            Saved = false;
            AccountRanges[] ranges = AccountRanges.load(ReportType.Trim() + ":" + ReportName.Trim(), ReportSection);
            if (ranges != null && ranges.Length > 0)
                foreach (AccountRanges range in ranges)
                    range.delete(range.Id.ToString());
            foreach(Accounts acct in lstAccounts.Items)
            {
                if (!acct.RangeCatFlag && acct.RangeUseFlag)
                {
                    AccountRanges range = new AccountRanges() { ReportName = ReportType.Trim() + ":" + ReportName.Trim(), ReportSection = this.ReportSection, AccountNumber = acct.Key };
                    if (range.save())
                        Saved = true;
                }
            }
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdSelectAll_Click(object sender, RoutedEventArgs e)
        {
            lstAccounts.ItemsSource = null;
            foreach (Accounts acct in acctList)
                if (!acct.RangeCatFlag)
                    acct.RangeUseFlag = true;
            lstAccounts.ItemsSource = acctList;
        }

        private void cmdSelectClear_Click(object sender, RoutedEventArgs e)
        {
            lstAccounts.ItemsSource = null;
            foreach (Accounts acct in acctList)
                acct.RangeUseFlag = false;
            lstAccounts.ItemsSource = acctList;
        }

        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public int ReportSection { get; set; }
        public string category1 { get; set; } = "";
        public string category2 { get; set; } = "";

        public bool Saved { get; set; } = false;

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Accounts account = (Accounts)lstAccounts.SelectedItem;
            //if (account == null || account.RangeCatFlag)
            //    return;
            CheckBox chkbox = (CheckBox)sender;
            ContentPresenter parent = VisualTreeHelper.GetParent(chkbox) as ContentPresenter;
            bool chk = chkbox.IsChecked.Value;
            Accounts accts = (Accounts)parent.Content;
            if (!accts.RangeCatFlag)
                return;
            string majorCat = accts.MajorCategory;
            lstAccounts.ItemsSource = null;
            foreach(Accounts acct in acctList)
            {
                if (acct.MajorCategory.Equals(majorCat))
                    acct.RangeUseFlag = chk;
            }
            lstAccounts.ItemsSource = acctList;
        }
    }
}
