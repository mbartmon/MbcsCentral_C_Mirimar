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
    /// Interaction logic for frmCheckNumbers.xaml
    /// </summary>
    public partial class frmCheckNumbers : Window
    {
        CheckNumbers[] checks = null;

        private string App;

        public frmCheckNumbers()
        {
            InitializeComponent();
        }

        public frmCheckNumbers(string app)
        {
            InitializeComponent();
            App = app;
        }

        private void frmCheckNumbers_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            checks = CheckNumbers.load(true, true);
            grdCheckNumbers.ItemsSource = checks;
        }

        private void frmCheckNumbers_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(App))
            {
                MessageBoxCustom.Show("You do not have permission to do this.");
                return;
            }

            foreach(CheckNumbers check in checks)
            {
                if (check.IsChanged)
                    check.save();
            }
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            grdCheckNumbers.ItemsSource = null;
            checks = CheckNumbers.load(true, true);
            grdCheckNumbers.ItemsSource = checks;

        }
    }
}
