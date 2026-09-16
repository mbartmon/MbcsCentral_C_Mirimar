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
    /// Interaction logic for frmInvoiceSelections.xaml
    /// </summary>
    public partial class frmInvoiceSelections : Window
    {

        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmInvoiceSelections(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        public frmInvoiceSelections()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            if (dtDueDate.SelectedDate != null)
            {
                try
                {
                    InvoiceSelection select = new InvoiceSelection() { processDate = dtDueDate.SelectedDate.Value, forceDiscounts = chkTakeDiscount.IsChecked.Value, clearSelections = optClear.IsChecked.Value };
                    select.SelectInvoices();
                    InvoiceSelectionReport.print_report();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.Show("An exception occured during SelectInvoices: " + ex.Message + "\r\nCheck the application log for more information.");
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "An exception occured during SelectInvoices: " + ex.Message, false);
                }
            }
            else
                MessageBox.Show("Please select a DUE DATE.");

        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void formInvoiceSelections_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            dtDueDate.SelectedDate = DateTime.Now;
        }

        private void formInvoiceSelections_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }
    }
}
