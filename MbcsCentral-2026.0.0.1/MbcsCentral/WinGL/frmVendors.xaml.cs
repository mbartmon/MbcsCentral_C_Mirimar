using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for frmVendors.xaml
    /// </summary>
    public partial class frmVendors : Window
    {
        Vendors[] vendors;
        Vendors vendor;
        int ThisYear = DateTime.Now.Year;
        int LastYear;
        string currentYear = "Use [year] for Current Purchases";
        bool displayed = false;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }              
        public frmVendors()
        {
            InitializeComponent();
        }

        public frmVendors(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }

        private void formVendors_Loaded(object sender, RoutedEventArgs e)
        {
            LastYear = ThisYear - 1;
            chkNextYear.Content = currentYear.Replace("[year]", (ThisYear + 1).ToString());
            this.SizeToContent = SizeToContent.Manual; 
            Detail.Visibility = System.Windows.Visibility.Collapsed;
            Main.getForm(this);
            object obj = Main.GetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\", "ActiveVendors", 0);
            //chkActiveOnly.IsChecked = (bool)((double)obj == 0 ? false : true);
            vendors = Vendors.load(chkActiveOnly.IsChecked.Value);
            lstVendors.ItemsSource = vendors;
            JournalNames[] jrns = JournalNames.load();
            lstJournals.ItemsSource = jrns;
            cmdMemos.IsEnabled = false;
            lstVendors.Focus();
        }

        private void formVendors_Closed(object sender, EventArgs e)
        {
            //Main.SetRegistryEntry(GlobalShared.APPNAME + "\\Wingl\\Options\\", "ActiveVendors", (double)(chkActiveOnly.IsChecked.Value ? 1 : 0), (int)Registry.DataTypes.Dword);
            Main.saveForm(this);
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            Detail.DataContext = null;
            vendor = new Vendors();
            cmdCategories.IsEnabled = false;
            cmdVendorCategories.IsEnabled = false;
            Detail.DataContext = vendor;
            Detail.Visibility = System.Windows.Visibility.Visible;
            txtVendorName.Focus();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
            	MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            if (MessageBox.Show("Delete this vendor?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                vendor.delete(vendor.VendorNumber.ToString());
                lstVendors.ItemsSource= null;
                vendors = Vendors.load(chkActiveOnly.IsChecked.Value);
                lstVendors.ItemsSource = vendors;
                Detail.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void cmdCategories_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendors.SelectedIndex < 0)
                return;
            frmVendorAllocations frm = new frmVendorAllocations(m_ThisApp);
            frm.VendorNumber = vendor.VendorNumber;
            frm.VendorName = vendor.VendorName;
            frm.Show();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdVendorSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            
            if (vendor != null)
            {
                if (vendor.VendorNumber == 0) // New Vendor?  Check for dupe name.
                {
                    string[] names = vendor.VendorName.Split(' ');
                    int parts = names.Length;
                    int count = 0;
                    bool found = false;
                    Vendors[] vends = Vendors.loadByName(names[0] + " "); 
                    if (vends != null && vends.Length > 0)
                    {
                        frmVendorDupes frm = new frmVendorDupes() { VendorList = vends };
                        frm.ShowDialog();
                        int result = frm.Result;
                        long key = frm.Key;
                        switch (result)
                        {
                            case (int)frmVendorDupes.Results.Add:
                                Vendors vend = null;
                                for(int i = 0; i < lstVendors.Items.Count; i++)
                                {
                                    vend = (Vendors)lstVendors.Items[i];
                                    if (vend.VendorNumber == key)
                                    {
                                        lstVendors.SelectedIndex = i;
                                        return;
                                    }
                                }
                                break;
                            case (int)frmVendorDupes.Results.Cancel:
                                return;
                                break;
                        }
                    }
                    // Now test for blank journal name
                    if (vendor.Journal == null || vendor.Journal.Trim().Equals(""))
                    {
                        if (MessageBoxCustom.Show("New vendor has not had a Journal assigned.\r\n\r\nEnter journal now?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
                            return;
                        
                    }
                    // Finally test for PAYROLL journal and payroll type not checked
                    if (vendor.Journal != null && (vendor.Journal.ToLower().Equals("payroll") && !vendor.Payroll))
                    {
                        MessageBoxResult result = MessageBoxCustom.Show("You have assigned the PAYROLL journal to this vendor but not checked the Payroll box\r\n\r\nDo you want to have that box checked now?", "", MessageBoxButton.YesNoCancel, this);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                vendor.Payroll = true;
                                break;
                            case MessageBoxResult.Cancel:
                                return;
                                break;
                        }
                    }
                }

                vendor.save();
            }
            lstVendors.ItemsSource = null;
            vendors = Vendors.load(chkActiveOnly.IsChecked.Value);
            lstVendors.ItemsSource = vendors;
            Detail.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void cmdVendorExit_Click(object sender, RoutedEventArgs e)
        {
            if (vendor.IsChanged)
            {
                MessageBoxResult r = MessageBox.Show("Changes made to this vendor. Save them?", "", MessageBoxButton.YesNoCancel);
                switch  (r)
                {
                    case MessageBoxResult.Yes:
                        cmdVendorSave_Click(sender, e);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                        break;
                }
            }
            Detail.Visibility = System.Windows.Visibility.Collapsed;
            lstVendors.SelectedIndex = -1;
            lstVendors.Focus();
        }

        private void lstVendors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            displayed = true;

            GetVendor();
        }

        private void GetVendor()
        {
            cmdMemos.IsEnabled = false;
            if (lstVendors.SelectedIndex >= 0)
            {
                vendor = (Vendors)lstVendors.SelectedItem;
                CurrentYear.Text = "0.00";
                PriorYear.Text = "0.00";

                // Get Vendor's total purchase amounts
                ApChecks[] checks = ApChecks.LoadVendorTotals(vendor.VendorNumber, ThisYear, LastYear);
                if (checks != null && checks.Length >= 1)
                {
                    if (checks[0].DateYear == ThisYear)
                        CurrentYear.Text = checks[0].PurchaseTotal.ToString("##,###,##0.00").PadLeft(12);
                    else
                        PriorYear.Text = checks[0].PurchaseTotal.ToString("##,###,##0.00").PadLeft(12);
                    if (checks.Length == 2)
                        if (checks[1].DateYear == ThisYear)
                            CurrentYear.Text = checks[1].PurchaseTotal.ToString("##,###,##0.00").PadLeft(12);
                        else
                            PriorYear.Text = checks[1].PurchaseTotal.ToString("##,###,##0.00").PadLeft(12);

                }
                Detail.DataContext = vendor;
                Detail.Visibility = System.Windows.Visibility.Visible;
                cmdMemos.IsEnabled = true;
                cmdVendorCategories.IsEnabled = true;
                cmdCategories.IsEnabled = true;
                txtVendorName.Focus();
            }

        }

            private void chkActiveOnly_Checked(object sender, RoutedEventArgs e)
        {
            vendors = Vendors.load(chkActiveOnly.IsChecked.Value);
            lstVendors.ItemsSource = vendors;
        }

        private void cmdMemos_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendors.SelectedIndex < 0)
                return;
            frmMemos frm = new frmMemos(m_ThisApp);
            frm.VendorNumber = vendor.VendorNumber;
            frm.VendorName = vendor.VendorName;
            frm.Show();
            
        }

        private void chkNextYear_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.IsChecked.Value)
            {
                ThisYear = DateTime.Now.Year + 1;
            }
            else
            {
                ThisYear = DateTime.Now.Year;
            }
            LastYear = ThisYear - 1;

            if (displayed)
                GetVendor();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
