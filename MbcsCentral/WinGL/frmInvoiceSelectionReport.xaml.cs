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
    /// Interaction logic for frmInvoiceSelectionReport.xaml
    /// </summary>
    public partial class frmInvoiceSelectionReport : Window
    {
        private string thisApp;

        public frmInvoiceSelectionReport()
        {
            InitializeComponent();
        }

        public frmInvoiceSelectionReport(string thisapp)
        {
            InitializeComponent();
            thisApp = thisapp;
        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
