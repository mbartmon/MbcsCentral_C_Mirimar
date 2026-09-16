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
    /// Interaction logic for frmARTrans.xaml
    /// </summary>
    public partial class frmARTrans : Window
    {

        private string m_ThisApp;
        public string ThisApp
        {
            get { return m_ThisApp; }
            set { m_ThisApp = value; }
        }

        public frmARTrans()
        {
            InitializeComponent();
        }

        public frmARTrans(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        private void frmARTrans_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            ArTrans[] trans = ArTrans.load();
            if (trans != null && trans.Length > 0)
                grdTransactions.ItemsSource = trans;

        }

        private void frmARTrans_Closed(object sender, EventArgs e)
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
