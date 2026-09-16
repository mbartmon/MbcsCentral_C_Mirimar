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
namespace MbcsCentral.WinPM
{
	public partial class frmCharge
	{

		private Charges m_charge;
		private Tenants m_tenant;

        DateTime BillDate;

		private bool m_add;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmCharge()
        {
            InitializeComponent();
            this.Loaded += frmCharge_Loaded;
            this.Closing += frmCharge_Closing;
        }

        public frmCharge(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmCharge_Loaded;
            this.Closing += frmCharge_Closing;
        }

		public Charges Charge {
			get { return m_charge; }
			set { m_charge = value; }
		}
		public Tenants Tenant {
			get { return m_tenant; }
			set { m_tenant = value; }
		}
		public bool Add {
			get { return m_add; }
			set { m_add = value; }
		}

		private ChargeCodes[] chgCodes;

		private void frmCharge_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}

		private void frmCharge_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((m_tenant == null)) {
				Interaction.MsgBox("Tenant not selected for this charge");
				this.Close();
			}

			Main.getForm(this);

            Control ctrl = Control.load(GlobalShared.user.Account.clientId);
            BillDate = ctrl.BillingDate;
            ctrl = null;

			chgCodes = ChargeCodes.loads(GlobalShared.user.Account.clientId);
			if (m_add) {
				m_charge = new Charges();
                m_charge.BuildingNumber = m_tenant.BuildingNumber;
                m_charge.TenantNumber = m_tenant.TenantNumber;
                m_charge.SequenceNumber = m_tenant.SequenceNumber;
                m_charge.Type = "0";
				m_charge.ChargeToTenant = true;
				m_charge.ShowOnBill = true;
			}
			pnlTenant.DataContext = m_tenant;
            if (m_charge.StartingDate > MbcsCentral.GlobalShared.NULL_DATE)
                chkStartDate.IsChecked = true;
            if (m_charge.EndingDate > MbcsCentral.GlobalShared.NULL_DATE)
                chkEndingDate.IsChecked = true;
            if (m_charge.FutureDate > MbcsCentral.GlobalShared.NULL_DATE)
                chkFutureDate.IsChecked = true;
            if (m_charge.Type.Equals(string.Format("{0}",(int)Charges.ChargeType.OneTime)))
                optOneTime.IsChecked = true;
            if (m_charge.Type.Equals(string.Format("{0}", (int)Charges.ChargeType.Recurring)))
                optRecurring.IsChecked = true;
            this.DataContext = m_charge;
            lstCodes.ItemsSource = chgCodes;
			lstCodes.Items.Refresh();
			foreach (ChargeCodes chg in lstCodes.Items) {
				if (chg.ChargeCode == m_charge.ChargeCode) {
					lstCodes.SelectedItem = chg;
				}
			}

		}

        private void cmdSave_Click_1(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            DateTime nowDate = DateTime.Now;
            DateTime origStartDate = GlobalShared.NULL_DATE;

            if (!optOneTime.IsChecked.Value && !optRecurring.IsChecked.Value)
            {
                MessageBox.Show("You must select a Frequency of either ONE TIME or RECURRING");
                return;
            }
            ChargeCodes chgCode = (ChargeCodes)lstCodes.SelectedItem;
            m_charge.ChargeCode = chgCode.ChargeCode;
            if (m_add)
            {
                m_charge.ClientId = GlobalShared.user.Account.clientId;
                m_charge.HistoryDate = BillDate;
            }
            else
                origStartDate = m_charge.StartingDate;

            if (!chkStartDate.IsChecked.Value)
                m_charge.StartingDate = MbcsCentral.GlobalShared.NULL_DATE;
            if (!chkEndingDate.IsChecked.Value)
                m_charge.EndingDate = MbcsCentral.GlobalShared.NULL_DATE;
            if (!chkFutureDate.IsChecked.Value)
                m_charge.FutureDate = MbcsCentral.GlobalShared.NULL_DATE;
            m_charge.Type = String.Format("{0}", optOneTime.IsChecked.Value ? (int)Charges.ChargeType.OneTime : (int)Charges.ChargeType.Recurring);
            if (m_charge.save())
            {
                if (!m_add && m_charge.StartingDate != GlobalShared.NULL_DATE && m_charge.StartingDate.Date != origStartDate.Date && m_charge.StartingDate.Date > nowDate.Date)
                    WinpmGlobal.HistChgUpdate(m_charge, false, true, m_charge.HistoryDate);

                if (m_charge.StartingDate <= nowDate)
                    WinpmGlobal.HistChgUpdate(m_charge, false, false, m_charge.HistoryDate);
            }
            this.Close();
        }

        private void cmdCancel_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Date_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DatePicker dt = (DatePicker)sender;

            if (dt.DisplayDate <= MbcsCentral.GlobalShared.NULL_DATE)
            {
                dt.DisplayDate = DateAndTime.Now.Date;
            }

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Main.PrintWindowBackground(this);
        }

	}
}
