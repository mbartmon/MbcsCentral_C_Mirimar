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
using System.Collections;


namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmChargeCodesInternal.xaml
    /// </summary>
    public partial class frmChargeCodesInternal : Window
    {
        private string m_ThisApp;
        bool dirty = false;
        ChargeCodes chargeCode;
        ArrayList chgs = ChargeCodes.loadList(GlobalShared.user.Account.clientId, false);
        WinGL.Accounts[] accts = WinGL.Accounts.load(true);
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmChargeCodesInternal()
        {
            InitializeComponent();
        }

        public frmChargeCodesInternal(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
        }

        private void frmChargeCodesInternal_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            lstCharges.ItemsSource = chgs;
            CollectionViewSource acct = (System.Windows.Data.CollectionViewSource)this.FindResource("accounts");
            acct.Source = accts;

        }

        private void frmChargeCodesInternal_Closed(object sender, EventArgs e)
        {
            if (chgs == null)
                return;
            foreach(ChargeCodes code in chgs)
            {
                if (code.IsChanged)
                {
                    MessageBoxResult rc = MessageBoxCustom.Show("There are changes. Do you want to save them?", "", MessageBoxButton.YesNo);
                    switch (rc)
                    {
                        case MessageBoxResult.Yes:
                            cmdSave_Click(sender, new RoutedEventArgs());
                            break;
                    }
                }
            }
            Main.saveForm(this);
        }

        private void CellFocus(DataGrid grid, object chargeCode, int column)
        {
            DataGridCellInfo cell = new DataGridCellInfo(chargeCode, grid.Columns[column]);
            grid.CurrentCell = cell;
            grid.ScrollIntoView(chargeCode);
            grid.BeginEdit();

        }

        private void cmdAdd_Click( object sender, RoutedEventArgs e )
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            chargeCode = new ChargeCodes();
            chargeCode.clientId = GlobalShared.user.Client.clientId;
            lstCharges.ItemsSource = null;
            chgs.Add(chargeCode);
            lstCharges.ItemsSource = chgs;
            lstCharges.Focus();
            CellFocus(lstCharges, chargeCode, 0);
        }

        private void cmdExit_Click( object sender, RoutedEventArgs e )
        {

            Main.saveForm( this );
            this.Close();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            foreach(ChargeCodes code in chgs)
            {
                if (code.IsChanged)
                {
                    code.save();
                    code.IsChanged = false;
                }
                if (code.Delete)
                {
                    code.delete(code.ID.ToString());
                    code.Delete = false;
                    code.IsChanged = false;
                }
            }
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            ChargeCodes code = (ChargeCodes)lstCharges.SelectedItem;
            if (code == null)
                return;
            if (MessageBox.Show((code.Delete ? "Restore" : "Delete") + " this Charge Description?", "", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                code.Delete = !code.Delete;
            }

        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            cmdAdd_Click(sender, e);
        }


        private void lstCharges_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGridRow row = e.Row;
            if (((ChargeCodes)row.Item).ChargeCode == null || ((ChargeCodes)row.Item).ChargeCode.Trim().Equals(""))
            {
                MessageBoxCustom.Show("No Charge Code was entered.");
                e.Cancel = true;
                CellFocus(lstCharges, chargeCode, 0);
            }
            else if (((ChargeCodes)row.Item).ChargeDescription == null || ((ChargeCodes)row.Item).ChargeDescription.Trim().Equals(""))
            {
                MessageBoxCustom.Show("A Description must be entered.");
                e.Cancel = true;
                CellFocus(lstCharges, chargeCode, 1);
            }
            else
            {
                string txt = ((ChargeCodes)row.Item).ChargeCode == null ? "" : ((ChargeCodes)row.Item).ChargeCode.Trim().ToLower();
                int count = 0;
                foreach (ChargeCodes chg in chgs)
                {
                    if (chg.ChargeCode != null && chg.ChargeCode.Trim().ToLower().Equals(txt) && !chg.Delete)
                    {
                        count++;
                    }
                    if (count > 1)
                    {
                        MessageBoxCustom.Show("This is a DUPLICATE charge code");
                        e.Cancel = true;
                        CellFocus(lstCharges, chargeCode, 0);
                    }
                }
            }
        }
    }
}
