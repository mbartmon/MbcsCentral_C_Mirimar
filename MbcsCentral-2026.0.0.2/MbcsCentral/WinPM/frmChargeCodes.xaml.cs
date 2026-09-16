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
using MbcsCentral.WinGL;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmChargeCodes.xaml
    /// </summary>
    public partial class frmChargeCodes : Window
    {
        ChargeCodes[] chgs = ChargeCodes.loads(GlobalShared.user.Account.clientId,false,true);
        WinGL.Accounts[] accts = WinGL.Accounts.load(true);

        bool dirty = false;
        ChargeCodes chargeCode;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmChargeCodes()
        {
            InitializeComponent();
        }
        public frmChargeCodes(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void formChargeCodes_Loaded_1( object sender, RoutedEventArgs e )
        {
            Main.getForm( this );
            lstCharges.ItemsSource = chgs;
            lstCreditAccount.ItemsSource = accts;
            lstDebitAccount.ItemsSource = accts;
            txtChargeCode.Focus();
        }

        private void lstCharges_SelectionChanged_1( object sender, SelectionChangedEventArgs e )
        {
            chargeCode = (ChargeCodes) lstCharges.SelectedItem;
            if (chargeCode != null)
            {
                pnlDetails.DataContext = chargeCode;
                cmdSave.Visibility = System.Windows.Visibility.Visible;
                cmdDelete.Visibility = System.Windows.Visibility.Visible;
                lstCreditAccount.SelectedIndex = FindAccountItem(lstCreditAccount, chargeCode.GLCreditName);
                lstDebitAccount.SelectedIndex = FindAccountItem(lstDebitAccount, chargeCode.GLDebitName);
            }
            else
            {
                pnlDetails.DataContext = null;
                cmdSave.Visibility = System.Windows.Visibility.Hidden;
                cmdDelete.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private int FindAccountItem(ComboBox box, string name)
        {
            MbcsCentral.WinGL.Accounts acct= null;
            for (int i = 0; i < box.Items.Count; i++)
            {
                acct = (MbcsCentral.WinGL.Accounts)box.Items[i];
                if (acct.Key > 0 && acct.Description.ToLower().Trim().Equals(name.ToLower().Trim()))
                {
                    return i;
                    break;
                }
            }
            return -1;
        }
        private void lstCharges_PreviewMouseDown_1( object sender, MouseButtonEventArgs e )
        {
            {

            }
        }

        private void cmdSave_Click_1( object sender, RoutedEventArgs e )
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (chargeCode != null)
            {
                if (lstCreditAccount.SelectedIndex >= 0)
                {
                    MbcsCentral.WinGL.Accounts acct = (MbcsCentral.WinGL.Accounts)lstCreditAccount.SelectedItem;
                    //chargeCode.GLCreditAccount = acct.Key.ToString();
                }
                else
                    //chargeCode.GLCreditAccount = "0";
                if (lstDebitAccount.SelectedIndex >= 0)
                {
                    MbcsCentral.WinGL.Accounts acct = (MbcsCentral.WinGL.Accounts)lstDebitAccount.SelectedItem;
                    //chargeCode.GLDebitAccount = acct.Key.ToString();
                }
                else
                    //chargeCode.GLDebitAccount = "0";
                chargeCode.save();
                chgs = null;
                lstCharges.ItemsSource = null;
                chgs = ChargeCodes.loads(GlobalShared.user.Account.clientId,false,true);
                lstCharges.IsEnabled = true;
                lstCharges.ItemsSource = chgs;
                pnlDetails.DataContext = null;
                cmdSave.Visibility = System.Windows.Visibility.Hidden;
                cmdAdd.Visibility = System.Windows.Visibility.Visible;
                cmdCancel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void cmdAdd_Click_1( object sender, RoutedEventArgs e )
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (dirty) // Last record changed but not saved
            {

            }
            chargeCode = new ChargeCodes();

            pnlDetails.DataContext = null;
            pnlDetails.DataContext = chargeCode;
            lstCharges.IsEnabled = false;
            cmdSave.Visibility = System.Windows.Visibility.Visible;
            cmdDelete.Visibility = System.Windows.Visibility.Hidden;
            cmdAdd.Visibility = System.Windows.Visibility.Hidden;
            cmdCancel.Visibility = System.Windows.Visibility.Visible;
            txtChargeCode.Focus();
        }

        private void cmdCancel_Click_1( object sender, RoutedEventArgs e )
        {
            lstCharges.IsEnabled = true;
            pnlDetails.DataContext = null;
            cmdSave.Visibility = System.Windows.Visibility.Hidden;
            cmdCancel.Visibility = System.Windows.Visibility.Hidden;
            cmdDelete.Visibility = System.Windows.Visibility.Visible;
            cmdAdd.Visibility = System.Windows.Visibility.Visible;
            lstCharges.IsEnabled = true;
            lstCharges.Focus();
        }

        private void cmdExit_Click_1( object sender, RoutedEventArgs e )
        {

            Main.saveForm( this );
            this.Close();
        }

        private void cmdDelete_Click_1( object sender, RoutedEventArgs e )
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (MessageBox.Show("Delete this Charge Description?", "", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                if (chargeCode.delete(chargeCode.ID.ToString()))
                {
                    lstCharges.ItemsSource=null;
                    chgs = ChargeCodes.loads(GlobalShared.user.Account.clientId);
                    lstCharges.ItemsSource = chgs;
                }
                else
                    MessageBox.Show( "There was an error attempting to delete the Charge Description" );
            }
        }

        private void lstCreditAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstDebitAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
