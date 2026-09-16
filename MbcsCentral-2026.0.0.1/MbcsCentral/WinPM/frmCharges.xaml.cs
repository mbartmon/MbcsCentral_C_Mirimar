using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MbcsCentral;
namespace MbcsCentral.WinPM
{
	public partial class frmCharges
	{

		private Tenants m_Tenant;
        System.Windows.Data.CollectionViewSource ChargesViewSource;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmCharges()
        {
            InitializeComponent();
            this.Loaded += frmCharges_Loaded;
            this.Closing += frmCharges_Closing;
            ChargesViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ChargesViewSource");
        }
        public frmCharges(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmCharges_Loaded;
            this.Closing += frmCharges_Closing;
            ChargesViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ChargesViewSource");
        }
		public Tenants Tenant {
			set { m_Tenant = value; }
		}

		private void frmCharges_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}

		private void frmCharges_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			//Load data by setting the CollectionViewSource.Source property:
			Charges[] chgs = Charges.loadBySeqNum(m_Tenant.SequenceNumber, true);
            try
            {
                ChargesViewSource.Source = chgs;
            }
            catch { }
			pnlTenant.DataContext = m_Tenant;
			Main.getForm(this);

		}

		private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

		private void ChargesDataGrid_SelectionChanged(System.Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			Charges chg = (Charges)ChargesDataGrid.SelectedItem;
			if ((chg != null) && (chg.Comment != null) && !string.IsNullOrEmpty(chg.Comment.Trim())) {
				txtComment.Text = chg.Comment;
			}

		}

		private void ChargesDataGrid_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEdit_Click(sender, new System.Windows.RoutedEventArgs());

		}


		private void cmdEdit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			Charges chg = (Charges)ChargesDataGrid.SelectedItem;
			if ((chg != null)) {
				frmCharge frm = new frmCharge(m_ThisApp);
				frm.Charge = chg;
				frm.Tenant = m_Tenant;
				frm.Add = false;
                frm.ShowDialog();
                Charges[] chgs = Charges.loadBySeqNum(m_Tenant.SequenceNumber, true);
                ChargesViewSource.Source = null;
                ChargesViewSource.Source = chgs;

			}
		}


		private void cmdAdd_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			frmCharge frm = new frmCharge(m_ThisApp);
			frm.Add = true;
			frm.Tenant = m_Tenant;
            frm.ShowDialog();
            Charges[] chgs = Charges.loadBySeqNum(m_Tenant.SequenceNumber, true);
            ChargesViewSource.Source = null;
            ChargesViewSource.Source = chgs;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Charges chg = (Charges)ChargesDataGrid.SelectedItem;
            if ((chg != null) )
            {
                if (MessageBox.Show("Delete charge?", "DELETE CHARGE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    WinpmGlobal.HistChgUpdate(chg, false, true, chg.HistoryDate);
                    chg.delete(chg.ChargeNumber.ToString());
                    Charges[] chgs = Charges.loadBySeqNum(m_Tenant.SequenceNumber, true);
                    ChargesViewSource.Source = null;
                    ChargesViewSource.Source = chgs;
                }
            }
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Main.PrintWindowBackground(this, System.Printing.PageOrientation.Landscape);
        }

    }
}
