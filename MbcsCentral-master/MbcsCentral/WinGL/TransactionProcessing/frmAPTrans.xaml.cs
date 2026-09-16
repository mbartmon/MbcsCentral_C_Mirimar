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
    /// Interaction logic for frmAPTrans.xaml
    /// </summary>
    public partial class frmAPTrans : Window
    {


        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmAPTrans(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        public frmAPTrans()
        {
            InitializeComponent();
        }

        private void frmAPTrans_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            ApTrans[] trans = ApTrans.load();
            if (trans != null && trans.Length > 0)
                grdTransactions.ItemsSource = trans;
        }

        private void frmAPTrans_Closed(object sender, EventArgs e)
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (grdTransactions.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("No Row(s) selected.");
                return;
            }

            string msg = "Delete ";
            if (grdTransactions.SelectedItems.Count == 1)
                msg += "this Row?";
            else
                msg += "these " + grdTransactions.SelectedItems.Count.ToString() + " Rows?";

            if (MessageBox.Show(msg, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ApTrans tran = null;
                for (int i = 0; i < grdTransactions.SelectedItems.Count; i++)
                {
                    tran = (ApTrans)grdTransactions.SelectedItems[i];
                    tran.delete(tran.Key.ToString());
                }
                ApTrans[] trans = ApTrans.load();
                grdTransactions.ItemsSource = null;
                grdTransactions.ItemsSource = trans;
            }
        }
        }
}

