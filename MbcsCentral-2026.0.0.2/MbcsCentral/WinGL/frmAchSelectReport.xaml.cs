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
    /// Interaction logic for frmAchSelectReport.xaml
    /// </summary>
    public partial class frmAchSelectReport : Window
    {
        private string m_ThisApp;
        public frmAchSelectReport(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        private void frmAchSelectReport_Loaded(object sender, RoutedEventArgs e)
        {
            //Main.getForm(this);
            if (!GlobalShared.user.hasPrintPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to use this item.");
                return;
            }

        }

        private void frmAchSelectReport_Closed(object sender, EventArgs e)
        {
            //Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            InvoiceSelectionReport.print_report(false, true, dtpPaymentDueDate.SelectedDate);
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
