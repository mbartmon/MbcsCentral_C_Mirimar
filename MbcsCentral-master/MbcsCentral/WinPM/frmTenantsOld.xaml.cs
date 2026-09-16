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

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmTenantsOld.xaml
    /// </summary>
    public partial class frmTenantsOld : Window
    {
        long clientId = GlobalShared.user.Account.clientId;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmTenantsOld(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }       
        public frmTenantsOld()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            TenantsArchive[] tens = TenantsArchive.loadAll(clientId, chkVacant.IsChecked.Value);
            grdTenants.DataContext = tens;
        }

        private void chkVacant_Checked(object sender, RoutedEventArgs e)
        {
            TenantsArchive[] tens = TenantsArchive.loadAll(clientId, chkVacant.IsChecked.Value);
            grdTenants.DataContext = null;
            grdTenants.DataContext = tens;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            this.Close();
        }
    }
}
