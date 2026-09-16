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
    /// Interaction logic for frm1099Report.xaml
    /// </summary>
    public partial class frm1099Report : Window
    {
        string className = "";
        Classes cls;
        F1099Report parms = null;
        long acctId = GlobalShared.user.Account.accountId;
        int clientId = (int)GlobalShared.user.Account.clientId;
        bool OneFlag = false;
        Vendors[] vendors = null;
        Form1099 rpt;

        public frm1099Report()
        {
            InitializeComponent();
        }

        private string thisApp;

        public frm1099Report(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frm1099Report_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            vendors = Vendors.load();
            lstVendorNames.ItemsSource = vendors;
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = F1099Report.DeSerialize(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new F1099Report();

            grdMain.DataContext = parms;
            Expander.Visibility = System.Windows.Visibility.Collapsed;
            this.Width -= colVendor.Width.Value;

        }

        private void frm1099Report_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            parms.StartDate = (DateTime)dtStart.SelectedDate;
            parms.EndDate = (DateTime)dtEnd.SelectedDate;
            parms.PrintDetail = chkDetail.IsChecked.Value;
            parms.PrintVendorAddress = chkAddress.IsChecked.Value;
            parms.OrderByVendor = (bool)(optVendor.IsChecked.Value);
            cls.value = parms.Serialize();
            cls.clientId = clientId;
            cls.acctId = acctId;
            cls.name = className;
            cls.save();
            this.Cursor = Cursors.Arrow;
        }

        private void cmdDisplayClick(object sender, RoutedEventArgs e)
        {
            if (OneFlag)
            {
                if (lstVendors.Items.Count <= 0)
                {
                    MessageBoxCustom.Show("You have selected to print individual vendors but have not entered any vendor names.");
                    return;
                }
                string xsql = "SELECT * FROM " + Vendors.TABLE + " WHERE [" + Vendors.SQL_COL_VENDOR_NUMBER + "] IN (" + ((Vendors)lstVendors.Items[0]).VendorNumber;
                for (int i = 1; i < lstVendors.Items.Count; i++)
                    xsql += ", " + ((Vendors)lstVendors.Items[i]).VendorNumber;
                xsql += ") ORDER BY " + Vendors.SQL_COL_VENDOR_NAME;
                vendors = Vendors.LoadQuery(xsql);

                if (vendors == null || vendors.Length == 0)
                {
                    MessageBoxCustom.Show("No individual vendors retrieved for report.");
                    return;
                }
            }

            this.Cursor = Cursors.Wait;
            rpt = new Form1099();
            rpt.Address = chkAddress.IsChecked.Value;
            rpt.Order = (short)(optVendor.IsChecked.Value ? 0 : 1);
            rpt.Detail = chkDetail.IsChecked.Value;
            rpt.vendors = vendors;
            rpt.StartDate = ((DateTime)dtStart.SelectedDate).Date;
            rpt.EndDate = ((DateTime)dtEnd.SelectedDate).Date;

            rpt.print_report();
            this.Cursor = Cursors.Arrow;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdVendors_Click(object sender, RoutedEventArgs e)
        {
            if (Expander.Visibility == System.Windows.Visibility.Collapsed)
            {
                Expander.Visibility = System.Windows.Visibility.Visible;
                cmdVendors.Content = "<<VENDORS";
                this.Width = colMain.Width.Value + colAction.Width.Value + colVendor.Width.Value + 20;
                cmdAdd.IsEnabled = true;
                OneFlag = true;
                lstVendorNames.Focus();
            }
            else
            {
                Expander.Visibility = System.Windows.Visibility.Collapsed;
                cmdVendors.Content = "VENDORS>>";
                this.Width = colMain.Width.Value + colAction.Width.Value + 20;
                cmdAdd.IsEnabled = false;
                OneFlag = false;
            }

        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendorNames.SelectedItem != null)
                lstVendors.Items.Add(lstVendorNames.SelectedItem);
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendors.SelectedIndex > -1)
                lstVendors.Items.RemoveAt(lstVendors.SelectedIndex);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            lstVendors.Items.Clear();
        }

    }
}
