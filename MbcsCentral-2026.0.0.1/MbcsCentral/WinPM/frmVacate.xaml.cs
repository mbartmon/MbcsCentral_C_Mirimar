using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for frmVacate.xaml
    /// </summary>
    public partial class frmVacate : Window
    {
        private string m_TenantNumber;
        private string m_Apartment;
        private string m_TenantName;
        private decimal m_Rent;
        private Tenants m_ten;
        private Tenants o_ten;

        public string TenantNumber
        {
            set { m_TenantNumber = value; }
        }
        public string Apartment
        {
            set { m_Apartment = value; }
        }
        public string TenantName
        {
            set { m_TenantName = value; }
        }
        public decimal Rent
        {
            set { m_Rent = value; }
        }
        public Tenants Ten
        {
            set { o_ten = value; }
        }


        public frmVacate()
        {
            InitializeComponent();
            grdMain.DataContext = this;
        }

        private void cmdVacate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to vacate Tenant #" + txtTenantNumber.Text + "?", "Tenant Table", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Vacate_Tenant();            
            }

        }

  		private void Vacate_Tenant()
		{
			object i = null;

			string xsql = null;

            m_ten = new Tenants();
            m_ten.BuildingNumber = o_ten.BuildingNumber;
            m_ten.TenantNumber = o_ten.TenantNumber;
			//m_ten.SequenceNumber = o_ten.SequenceNumber;
			m_ten.Apartment = o_ten.Apartment;
			m_ten.Rooms = o_ten.Rooms;
			m_ten.SquareFeet = o_ten.SquareFeet;
            m_ten.XrfExempt = o_ten.XrfExempt;
            m_ten.XrfExemptDate = o_ten.XrfExemptDate;
            m_ten.XrfTested = o_ten.XrfTested;
            m_ten.XrfTestedDate = o_ten.XrfTestedDate;
            m_ten.XrfWorkComplete = o_ten.XrfWorkComplete;
            m_ten.XrfWorkCompleteDate = o_ten.XrfWorkCompleteDate;

            try
            {
                TenantsArchive tenArch = o_ten.copyToArchive();
                tenArch.NewOnly = true;
                tenArch.save();
                tenArch = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating new Tenant Archive record: " + ex.Message);
            }

            try
            {
                o_ten.delete(o_ten.SequenceNumber.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting old Tenant record: " + ex.Message);
            }

			m_ten.Rent = (decimal) Convert.ToDecimal(txtRentAmount.Text);
			// Security to MBR
            m_ten.Security=0;
            m_ten.Arrears=0;
            m_ten.Paid=0;
            m_ten.FutureRent1 = 0;
            m_ten.FutureRent2 = 0;
            m_ten.FutureRent3 = 0;
            m_ten.MBR = 0;

			m_ten.TenantName= "* VACANT";
            m_ten.ClientId = GlobalShared.user.Account.clientId;
			m_ten.save();

            /* REALLY do the following deletes from History?
			xsql = "DELETE * FROM HistChg WHERE [Building Number] = '" + bld + "' AND [Tenant Number] = '" + tnt;
			xsql = xsql + "' AND [Sequence Number] = " + sqn + " AND [Date] BETWEEN #" + DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), DateAndTime.Month(DateAndTime.Today), 1);
			xsql = xsql + "# AND #" + DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), DateAndTime.Month(DateAndTime.Today) + 1, 1 - 1) + "#";
			WinpmGlobal.PMSdb.Execute((xsql));
			xsql = "DELETE * FROM HistPay WHERE [Building Number] = '" + bld + "' AND [Tenant Number] = '" + tnt;
			xsql = xsql + "' AND [Sequence Number] = " + sqn + " AND [Date] BETWEEN #" + DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), DateAndTime.Month(DateAndTime.Today), 1);
			xsql = xsql + "# AND #" + DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), DateAndTime.Month(DateAndTime.Today) + 1, 1 - 1) + "#";
			WinpmGlobal.PMSdb.Execute((xsql));
            */
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void formVacate_Loaded(object sender, RoutedEventArgs e)
        {
            txtTenantNumber.Text = o_ten.TenantNumber;
            txtApartment.Text = o_ten.Apartment;
            txtTenantName.Text = o_ten.TenantName;
            txtRentAmount.Text = string.Format("{0:##,##0.00}", o_ten.Rent);
        }
    }
}
