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


namespace MbcsCentral.WinPM.SpecialReports
{
    /// <summary>
    /// Interaction logic for frmRentRollMortgages.xaml
    /// </summary>
    public partial class frmRentRollMortgages : Window
    {
        public frmRentRollMortgages()
        {
            InitializeComponent();
        }

        private void frmRentRollMortgages_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

        }

        private void frmRentRollMortgages_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
