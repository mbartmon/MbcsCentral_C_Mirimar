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
    /// Interaction logic for frmVendorDupes.xaml
    /// </summary>
    public partial class frmVendorDupes : Window
    {
        public   enum Results
        {
            Add,
            Edit,
            Cancel
        }

        public frmVendorDupes()
        {
            InitializeComponent();
        }

        private void frmVendorDupes_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            lstVendors.ItemsSource = VendorList;

        }

        private void frmVendorDupes_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Result = (int)Results.Add;
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Result = (int)Results.Cancel;
            this.Close();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstVendors.SelectedIndex < 0 )
            {
                MessageBoxCustom.Show("You have not selected a Vendor to EDIT.");
                return;
            }
            Result = (int)Results.Add;
            Key = ((Vendors)lstVendors.SelectedItem).VendorNumber;
            this.Close();
        }

        public Vendors[] VendorList { get; set; }
        public int Result { get; set; }
        public long Key { get; set; }
    }
}
