using System;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;
namespace MbcsCentral.WinPM
{
	public partial class frmImprovementEdit
	{

		private Improvements imp = null;
		private bool m_add = false;

		private Tenants ten;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmImprovementEdit()
        {
            InitializeComponent();
            this.Loaded += frmImprovementEdit_Loaded;
            this.Closing += frmImprovementEdit_Closing;
        }
        public frmImprovementEdit(string parm)
        {
            InitializeComponent();
             m_ThisApp = (string) parm;
             this.Loaded += frmImprovementEdit_Loaded;
             this.Closing += frmImprovementEdit_Closing;
        }
		public Improvements ImprovmentRecord {
			get { return imp; }
			set { imp = value; }
		}

		public bool Add {
			get { return m_add; }
			set { m_add = value; }
		}

		public Tenants Tenant {
			set { ten = value; }
		}

		private void frmImprovementEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);
		}

		private void frmImprovementEdit_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((imp == null)) {
				this.Close();
			}

			Main.getForm(this);

			if (m_add) {
				txtBuildingNumber.Text = ten.BuildingNumber;
				txtTenantNumber.Text = ten.TenantNumber;
				txtApartment.Text = ten.Apartment;
				txtTenantName.Text = ten.TenantName;
				imp.ImprovementDate = DateAndTime.Now.Date;
                RentIncreaseDate.SelectedDate = DateTime.Now.Date;
			} else {
				txtBuildingNumber.Text = imp.BuildingNumber;
				txtTenantNumber.Text = imp.TenantNumber;
				Tenants[] tens = Tenants.load(imp.BuildingNumber, imp.TenantNumber, GlobalShared.user.Account.clientId);
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
			GridMain.DataContext = imp;

		}


		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (m_add)
            {
				imp.BuildingNumber = ten.BuildingNumber;
				imp.TenantNumber = ten.TenantNumber;
			}
			try {
				if (!imp.save()) {
					Interaction.MsgBox("Could not save Improvment Record.");
					return;
				}
				this.Close();
			} catch (Exception ex) {
				Interaction.MsgBox("Error saving Improvement Record: " + ex.Message);
			}

		}


		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			m_add = false;
			this.Close();

		}
	}
}
