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
    /// Interaction logic for frmInvoiceSelection.xaml
    /// </summary>
    public partial class frmInvoiceSelection : Window
    {
        bool processed = false;
        protected string thisApp;

        public frmInvoiceSelection()
        {
            InitializeComponent();
        }
        public frmInvoiceSelection(string thisapp)
        {
            InitializeComponent();
            this.thisApp = thisapp;
            if (!GlobalShared.user.hasModifyPermission(thisapp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceSelection process = new InvoiceSelection() { clearSelections = optSelectClear.IsChecked.Value, forceDiscounts = chkDiscounts.IsChecked.Value, processDate = (DateTime)dtDate.SelectedDate };
                if (process.SelectInvoices())
                {
                    processed = true;
                    if (MessageBox.Show("Invoices successfully processed. Do you want to print the selection list?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        processed = false;
                    }
                }
                else
                {
                    MessageBox.Show("No Invoices Selected for Payment.");
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("An exception occured during SelectInvoices: " + ex.Message + "\r\nCheck the application log for more information.");
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "An exception occured during SelectInvoices: " + ex.Message, false);
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (MessageBox.Show("Invoices successfully processed. Do you want to print the selection list?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                processed = false;
            }
        }
    }
}
