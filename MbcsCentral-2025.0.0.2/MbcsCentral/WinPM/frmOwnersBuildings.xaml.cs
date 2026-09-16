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
	public partial class frmOwnersBuildings
	{

		Owners[] ownerRecs;

		Building[] bldRecs;
		Owners ownerRec;

		Building buildingRec;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmOwnersBuildings()
        {
            InitializeComponent();
            this.Loaded += frmOwnersBuildings_Loaded;
            this.Closing += frmOwnersBuildings_Closing;
        }

        public frmOwnersBuildings(string parm)
        {
            InitializeComponent();
            this.Loaded += frmOwnersBuildings_Loaded;
            this.Closing += frmOwnersBuildings_Closing;
            m_ThisApp = (string)parm;
        }
        
        private void frmOwnersBuildings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}


		private void frmOwnersBuildings_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			ownerRecs = Owners.load(GlobalShared.user.Account.clientId);
			if ((ownerRecs != null)) {
				lstOwners.ItemsSource = ownerRecs;
				lstOwners.DisplayMemberPath = "OwnerName";
			}

		}


		private void lstOwners_SelectionChanged(System.Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (lstOwners.SelectedIndex >= 0) {
				getBuildings();
			}

		}

		private void getBuildings()
		{
            lstBuildings.ItemsSource = null;
			ownerRec = (Owners)lstOwners.SelectedItem;
			if ((ownerRec != null)) {
				bldRecs = Building.loadByOwnerNumber(ownerRec.OwnerNumber,GlobalShared.user.Account.clientId);
				if ((bldRecs != null)) {
					lstBuildings.ItemsSource = bldRecs;
					lstBuildings.DisplayMemberPath = "Address1";
				}
			}

		}

		private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

		private void cmdRemoveOwner_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            ownerRec = (Owners)lstOwners.SelectedItem;
			if ((ownerRec == null)) {
				Interaction.MsgBox("You must select an owner to remove.");
				return;
			}
			Building[] blds = Building.loadByOwnerNumber(ownerRec.OwnerNumber,GlobalShared.user.Account.clientId);
			if ((blds != null) && blds.Length > 0) {
				Interaction.MsgBox("You must Move or Remove all Buildings for this Owner before you can Remove the owner record.");
				return;
			}
			if (Interaction.MsgBox("Do you really want to remove <" + ownerRec.OwnerName + ">?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
				ownerRec.delete(ownerRec.ID.ToString());
				lstOwners.ItemsSource = null;
				ownerRecs = Owners.load(GlobalShared.user.Account.clientId);
				lstOwners.ItemsSource = ownerRecs;
			}
		}

		private void cmdRemoveBuilding_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Building bld = (Building)lstBuildings.SelectedItem;
			if ((bld == null)) {
				Interaction.MsgBox("You must select a Building to remove.");
				return;
			}
			if (Interaction.MsgBox("Do you really want to remove Building #" + bld.BuildingNumber + " at <" + bld.Address1 + "> and all its Tenants?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
				deleteBuilding(bld);
				lstBuildings.ItemsSource = null;
				bldRecs = Building.loadByOwnerNumber(bld.OwnerNumber,GlobalShared.user.Account.clientId);
			}
		}

		private void deleteBuilding(Building bld)
		{
            int TenantCount = -1;
			int OccupantCount = 0;
			int LegalCount = 0;
			int ChargeCount = 0;
            if (GlobalShared.CN.State == System.Data.ConnectionState.Open)
                GlobalShared.CN.Close();
            GlobalShared.CN.ConnectionString = GlobalShared.SqlConnectionString;
			System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(WinpmGlobal.SP_DELETE_BUILDING, MbcsCentral.GlobalShared.CN);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@BuildingNumber", System.Data.SqlDbType.NVarChar).Value = bld.BuildingNumber;
			cmd.Parameters.Add("@NumberOfTenants", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfOccupants", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfLegal", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfCharges", System.Data.SqlDbType.Int);
            cmd.Parameters["@NumberOfTenants"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfOccupants"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfLegal"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfCharges"].Direction = System.Data.ParameterDirection.Output;

			try {
				MbcsCentral.GlobalShared.CN.Open();
				cmd.ExecuteNonQuery();
				TenantCount = (int)cmd.Parameters["@NumberOfTenants"].Value;
                OccupantCount = (int)cmd.Parameters["@NumberOfOccupants"].Value;
                LegalCount = (int)cmd.Parameters["@NumberOfLegal"].Value;
                ChargeCount = (int)cmd.Parameters["@NumberOfCharges"].Value;
			} catch (System.Data.SqlClient.SqlException ex) {
				Interaction.MsgBox("Could not remove building: " + ex.Message);
				TenantCount = -1;
			} catch (Exception ex) {
				TenantCount = -1;
			}

			if (TenantCount == 0) {
					Interaction.MsgBox("Building removed.  It contained no tenants");
            }
			else {
                if (TenantCount > 0) {
					Interaction.MsgBox("Building removed along with: " + Constants.vbCrLf + Strings.Format(TenantCount, "##,##0").PadLeft(7) + " tenants" + Strings.Format(ChargeCount, "##,##0").PadLeft(7) + " charges" + Strings.Format(OccupantCount, "##,##0").PadLeft(7) + " occupants" + Strings.Format(LegalCount, "##,##0").PadLeft(7) + " legal records");
                }
                else
					Interaction.MsgBox("Building not removed due to error condition.");
			}
		}

		private void cmdEditOwner_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (lstOwners.SelectedIndex >= 0)
            {
				ownerRec = (Owners)lstOwners.SelectedItem;
				frmOwner frm = new frmOwner(m_ThisApp);
				frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
				frm.ID = ownerRec.ID;
				frm.ShowDialog();
				lstOwners.ItemsSource = null;
				ownerRecs = Owners.load(GlobalShared.user.Account.clientId);
				lstOwners.ItemsSource = ownerRecs;
			}

		}

		private void cmdAddOwner_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            frmOwner frm = new frmOwner(m_ThisApp);
			frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frm.Add = true;
			frm.ShowDialog();
			lstOwners.ItemsSource = null;
			ownerRecs = Owners.load(GlobalShared.user.Account.clientId);
			lstOwners.ItemsSource = ownerRecs;

		}

		private void cmdEditBuilding_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if ((ownerRec == null))
            {
				Interaction.MsgBox("Owner not selected.");
				return;
			}
			if (lstBuildings.SelectedIndex >= 0) {
				buildingRec = (Building)lstBuildings.SelectedItem;
				frmBuilding frm = new frmBuilding(m_ThisApp);
				frm.BuildingRecord = buildingRec;
				frm.OwnerRecord = ownerRec;
				frm.ShowDialog();
				getBuildings();
			}

		}

		private void lstBuildings_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEditBuilding_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void lstOwners_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEditOwner_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void cmdMoveBuilding_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            if (lstBuildings.SelectedIndex >= 0)
            {
				Building bld = (Building)lstBuildings.SelectedItem;
				frmMoveBuilding frm = new frmMoveBuilding();
				frm.BuildingRecord = bld;
				frm.Owners = ownerRecs;
				frm.ShowDialog();
				getBuildings();
			}

		}


		private void cmdAddBuilding_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if ((ownerRec != null))
            {
				frmBuilding frm = new frmBuilding(m_ThisApp);
				frm.BuildingRecord = null;
				frm.OwnerRecord = ownerRec;
				frm.ShowDialog();
				getBuildings();
			}

		}

        private void cmdAddTenants_Click(object sender, RoutedEventArgs e)
        {
            if (lstBuildings.SelectedIndex >= 0)
            {
                Building bld = (Building)lstBuildings.SelectedItem;
                WinPM.frmAddTenant frm = new WinPM.frmAddTenant() { BuildingNumber = bld.BuildingNumber.Trim(), ThisApp = m_ThisApp };
                frm.Show();
            }
        }

	}
}
