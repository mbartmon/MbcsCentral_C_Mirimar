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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public bool Cancelled = false;

        public frmLogin()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Initializing Login Window: " + ex.Message, false);
                this.Close();
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {

            MbcsCentral.GlobalShared.userName = txtUsername.Text;
            MbcsCentral.GlobalShared.password = txtPassword.Password;

            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            MbcsCentral.GlobalShared.userName = null;
            MbcsCentral.GlobalShared.password = null;
            Cancelled = true;
            this.Close();
        }

        private void formLogin_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.BringIntoView();
            this.Topmost = true;
            this.Focus();
            txtUsername.Focus();

        }
    }
}
