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
    /// Interaction logic for frmVendorSpreadsheet.xaml
    /// </summary>
    public partial class frmVendorSpreadsheet : Window
    {
        Vendors[] vendors = null;
        string query = "SELECT * FROM " + Vendors.TABLE;
        bool loading = true;
        int ThisYear = DateTime.Now.Year;
        int LastYear;
        private string thisApp;

        public frmVendorSpreadsheet(string app)
        {
        	thisApp = app;
        	InitializeComponent();
        }

        public frmVendorSpreadsheet()
        {
            InitializeComponent();
        }

        private void frmVendorSpreadsheet_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            loading = false;
            object obj = Main.GetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\" + this.Name, "ActiveOnly", 1);
            if (obj != null)
                chkActiveOnly.IsChecked = (bool)((double)obj == 0 ? false : true);
            obj = Main.GetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\" + this.Name, "WithPurchases", 1);
            if (obj != null)
                chkActivity.IsChecked = (bool)((double)obj == 0 ? false : true);

            GetVendors();

        }

        private void GetVendors()
        {
            bool usedActive = false;
            string sql = query;
            if (chkActiveOnly.IsChecked.Value)
            {
                sql += " WHERE " + Vendors.SQL_COL_INACTIVE + " = 0 ";
                usedActive = true;
            }
            if (chkActivity.IsChecked.Value) 
                sql += (usedActive ? "AND " : " WHERE ") + Vendors.SQL_COL_CURRENT_PURCHASES + " = 0 AND " + Vendors.SQL_COL_PRIOR_YEAR_PURCHASES + " = 0";
            sql += " ORDER BY " + Vendors.SQL_COL_VENDOR_NAME;
            vendors = Vendors.LoadQuery(sql);
            foreach(Vendors vend in vendors)
            {
                // Get Vendor's total purchase amounts
                ApChecks[] checks = ApChecks.LoadVendorTotals(vend.VendorNumber, ThisYear, LastYear);
                if (checks != null && checks.Length >= 1)
                {
                    if (checks[0].DateYear == ThisYear)
                        vend.CurrentPurchases = checks[0].PurchaseTotal;
                    else
                        vend.PriorYearPurchases = checks[0].PurchaseTotal;
                    if (checks.Length == 2)
                        if (checks[1].DateYear == ThisYear)
                            vend.CurrentPurchases = checks[1].PurchaseTotal;
                        else
                            vend.PriorYearPurchases = checks[1].PurchaseTotal;

                }

            }
            lstVendors.ItemsSource = null;
            lstVendors.ItemsSource = vendors;
        }

        private void frmVendorSpreadsheet_Closed(object sender, EventArgs e)
        {
            Main.SetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\" + this.Name, "ActiveOnly",  (double)(chkActiveOnly.IsChecked.Value ? 1 : 0), (int)Registry.DataTypes.Dword);
            Main.SetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\" + this.Name, "WithPurchases", (double)(chkActivity.IsChecked.Value ? 1 : 0), (int)Registry.DataTypes.Dword);
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (vendors != null)
            {
                foreach (Vendors vendor in vendors)
                {
                    if (vendor.IsChanged)
                    {
                        vendor.save();
                        count++;
                    }
                }
            }
            MessageBoxCustom.Show(count.ToString() + " Vendors changed.", "", MessageBoxButton.OK, this);
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool ActiveOnly { get; set; }
        public bool WithPurchases { get; set; }

        private void chkActiveOnly_Checked(object sender, RoutedEventArgs e)
        {
            if (!loading)
                GetVendors();
        }

        private void chkActivity_Checked(object sender, RoutedEventArgs e)
        {
            if (!loading)
                GetVendors();
        }
    }
}
