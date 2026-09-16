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
using MbcsCentral;
using MbcsCentral.WinPM;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmPaymentDetails.xaml
    /// </summary>
    public partial class frmPaymentDetails : Window
    {
        private int m_TenantId = 0;
        public int TenantId
        {
            set { m_TenantId = value; }
        }
        private string m_TenantName;
        public string TenantName
        {
            set { m_TenantName = value; }
        }
        public frmPaymentDetails()
        {
            InitializeComponent();
        }

        private void formPaymentDetails_Loaded_1( object sender, RoutedEventArgs e )
        {
            Main.getForm( this );
            this.Title += m_TenantName;
            System.Windows.Data.CollectionViewSource paymentsViewSource = ((System.Windows.Data.CollectionViewSource) (this.FindResource( "paymentsViewSource" )));
            // Load data by setting the CollectionViewSource.Source property:
            if (m_TenantId > 0)
            {
                Payments[] pays =  Payments.loadBySequence( m_TenantId );
                if (pays != null && pays.Length > 0)
                    paymentsViewSource.Source = pays;
            }
        }

        private void cmdClose_Click_1( object sender, RoutedEventArgs e )
        {
            Main.saveForm( this );
            this.Close();
        }
    }
}
