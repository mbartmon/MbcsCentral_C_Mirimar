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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmReportViewer.xaml
    /// </summary>
    public partial class frmReportViewer : Window
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void cmdClose_Click_1(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
        }

        private void fromReportViewer_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
        }
    }
}
