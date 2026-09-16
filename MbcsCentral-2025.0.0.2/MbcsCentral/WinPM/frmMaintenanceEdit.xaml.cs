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
	public partial class frmMaintenanceEdit
	{

		private Maintenance mnt = null;
		private bool m_add = false;

		private Tenants ten;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmMaintenanceEdit()
        {
            InitializeComponent();
            this.Loaded += frmMaintenanceEdit_Loaded;
        }
        public frmMaintenanceEdit(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmMaintenanceEdit_Loaded;
        }
		public Maintenance MaintenantRecord {
			get { return mnt; }
			set { mnt = value; }
		}

		public bool Add {
			get { return m_add; }
			set { m_add = value; }
		}

		public Tenants Tenant {
			set { ten = value; }
		}

		private void frmMaintenanceEdit_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if ((mnt == null)) {
				this.Close();
			}

			Main.getForm(this);

			if (m_add) {
				txtBuildingNumber.Text = ten.BuildingNumber;
				txtTenantNumber.Text = ten.TenantNumber;
				txtApartment.Text = ten.Apartment;
				txtTenantName.Text = ten.TenantName;
				mnt.MaintenanceDate = DateAndTime.Now.Date;
			} else {
				txtBuildingNumber.Text = mnt.BuildingNumber;
				txtTenantNumber.Text = mnt.TenantNumber;
				Tenants[] tens = Tenants.load(mnt.BuildingNumber, mnt.TenantNumber, GlobalShared.user.Account.clientId);
				long seq = 0;
				Tenants ten = null;
				int j = -1;
				if ((tens != null) && tens.Length > 0) {
					for (int i = 0; i <= tens.Length - 1; i++) {
						if (tens[i].SequenceNumber > seq) {
							seq = tens[i].SequenceNumber;
							j = i;
						}
					}
				}
				if (j >= 0) {
					txtApartment.Text = tens[j].Apartment;
					txtTenantName.Text = tens[j].TenantName;
				} else {
					txtTenantName.Text = "* NO CURRENT TENANT *";
				}
			}
			GridMain.DataContext = mnt;

		}

		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (m_add)
            {
				mnt.BuildingNumber = ten.BuildingNumber;
				mnt.TenantNumber = ten.TenantNumber;
			}
			try {
				if (!mnt.save()) {
					Interaction.MsgBox("Could not save Maintenance Record.");
					return;
				}
				this.Close();
			} catch (Exception ex) {
				Interaction.MsgBox("Error saving Maintenance Record: " + ex.Message);
			}

		}

		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			m_add = false;
			this.Close();

		}

    }
}
