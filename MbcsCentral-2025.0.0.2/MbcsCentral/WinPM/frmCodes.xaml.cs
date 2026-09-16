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

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmCodes.xaml
    /// </summary>
    public partial class frmCodes : Window
    {
        Codes[] cds;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }     
        public frmCodes()
        {
            InitializeComponent();
        }
        public frmCodes(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void formCodes_Loaded_1( object sender, RoutedEventArgs e )
        {
            Main.getForm( this );
            System.Windows.Data.CollectionViewSource codesViewSource = ((System.Windows.Data.CollectionViewSource) (this.FindResource( "codesViewSource" )));
            cds = Codes.load();
            // Load data by setting the CollectionViewSource.Source property:
            codesViewSource.Source = cds;
        }

        private void cmdClose_Click_1( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void cmdSave_Click_1( object sender, RoutedEventArgs e )
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } foreach (Codes cd in cds)
                if (cd.isChanged)
                    cd.save();

            this.Close();
        }

        private void formCodes_Closed_1( object sender, EventArgs e )
        {
            Main.saveForm( this );
        }
    }
}
