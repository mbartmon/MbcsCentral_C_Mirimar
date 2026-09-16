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
using MbcsCentral.WinPM;

namespace MbcsCentral
{
	public partial class frmBuilding
	{


		private bool Add = false;
		private Building m_buildingRec = null;

		private Owners m_ownerRec = null;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }       
        public frmBuilding()
        {
            InitializeComponent();
            this.Loaded += frmBuilding_Loaded;
            this.Closing += frmBuilding_Closing;
        }

        public frmBuilding(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmBuilding_Loaded;
            this.Closing += frmBuilding_Closing;
        }	
	
        public Building BuildingRecord {
			get { return m_buildingRec; }
			set { m_buildingRec = value; }
		}

        public Owners OwnerRecord {
			get { return m_ownerRec; }
			set { m_ownerRec = value; }
		}

		private void frmBuilding_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (m_buildingRec.IsChanged) {
				switch (Interaction.MsgBox("Save your changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						if (string.IsNullOrEmpty(m_buildingRec.BuildingNumber.Trim())) {
							Interaction.MsgBox("Building Number cannot be blank.");
							e.Cancel = true;
							return;
						}
						GetContactType();
						if (!m_buildingRec.save()) {
							Interaction.MsgBox("Could not save Building Record.");
							e.Cancel = true;
						}
						break;
					case MsgBoxResult.Cancel:
						e.Cancel = true;
						return;

						break;
				}
			}
			Main.saveForm(this);

		}

		private void frmBuilding_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((m_ownerRec == null)) {
				Interaction.MsgBox("Owner not set for this buiilding.  Cannot continue.");
				this.Close();
			}

			Main.getForm(this);
			if ((m_buildingRec == null)) {
				m_buildingRec = new Building();
				m_buildingRec.OwnerNumber = OwnerRecord.OwnerNumber;
				Add = true;
                cmdAddTenants.Visibility = System.Windows.Visibility.Collapsed;
				txtBuildingNumber.IsReadOnly = false;
				txtBuildingNumber.Focus();
			} else {
				txtBuildingNumber.IsReadOnly = true;
				txtAddress1.Focus();
			}

			txtOwnerName.DataContext = m_ownerRec;
			MainGrid.DataContext = m_buildingRec;
			SetContactType();

		}


		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (string.IsNullOrEmpty(m_buildingRec.BuildingNumber.Trim()))
            {
				Interaction.MsgBox("Building Number cannot be blank.");
				return;
			}

			GetContactType();
			if (!m_buildingRec.save()) {
				Interaction.MsgBox("Could not save Building Record.");
			} else 
			{
				m_buildingRec.IsChanged = false;
				Tenants[] tenants = Tenants.load(BuildingRecord.BuildingNumber);
                if ( tenants == null || tenants.Length == 0)
                {
                    
					if (MessageBox.Show("Add tenants at this time?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						WinPM.frmAddTenant frm = new WinPM.frmAddTenant(m_ThisApp) { BuildingNumber = m_buildingRec.BuildingNumber };
						frm.Show();
					}
                }
                this.Close();
            }

		}


		private void SetContactType()
		{
			switch (BuildingRecord.AgentIs) {
				case (int)Building.ContactType.IsOwner:
					optOwner.IsChecked = true;
					break;
				case (int)Building.ContactType.IsAgent:
					optAgent.IsChecked = true;
					break;
				case (int)Building.ContactType.IsCoopOwner:
					optCoop.IsChecked = true;
					break;
			}

		}


		private void GetContactType()
		{
			if ((bool)optOwner.IsChecked) {
				BuildingRecord.AgentIs = (int)Building.ContactType.IsOwner;
			} else if ((bool)optAgent.IsChecked) {
				BuildingRecord.AgentIs = (int)Building.ContactType.IsAgent;
			} else if ((bool)optCoop.IsChecked) {
				BuildingRecord.AgentIs = (int)Building.ContactType.IsCoopOwner;
			}

		}


		private void txtBuildingNumber_TextChanged(System.Object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (Add) {
				Building bld = null;
				bld = Building.loadByBuildingNumber(txtBuildingNumber.Text,GlobalShared.user.Account.clientId);
				if ((bld != null)) {
					Interaction.MsgBox("A building with that Building Number already exist.  Re-enter.");
					txtBuildingNumber.Text = "";
				}
			}

		}

        private void CheckCoopDate_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            switch (chk.Name)
            {
                case "CheckCoop1Date":
                    if (chk.IsChecked.Value)
                    {
                        DatCoop1Date.IsEnabled = true;
                        if (DatCoop1Date.SelectedDate <= GlobalShared.NULL_DATE)
                            DatCoop1Date.Text = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        DatCoop1Date.SelectedDate = GlobalShared.NULL_DATE;
                        DatCoop1Date.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        DatCoop1Date.IsEnabled = false;
                    }
                    break;
               case "CheckCoop2Date":
                    if (chk.IsChecked.Value)
                    {
                        DatCoop2Date.IsEnabled = true;
                        if (DatCoop2Date.SelectedDate <= GlobalShared.NULL_DATE)
                            DatCoop2Date.Text = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        DatCoop2Date.SelectedDate = GlobalShared.NULL_DATE;
                        DatCoop2Date.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        DatCoop2Date.IsEnabled = false;
                    }
                    break;
               case "CheckCoop3Date":
                    if (chk.IsChecked.Value)
                    {
                        DatCoop3Date.IsEnabled = true;
                        if (DatCoop3Date.SelectedDate <= GlobalShared.NULL_DATE)
                            DatCoop3Date.Text = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        DatCoop3Date.SelectedDate = GlobalShared.NULL_DATE;
                        DatCoop3Date.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        DatCoop3Date.IsEnabled = false;
                    }
                    break;
            }
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Main.PrintWindow(this, System.Printing.PageOrientation.Landscape);
        }

        private void cmdAddTenants_Click(object sender, RoutedEventArgs e)
        {
            WinPM.frmAddTenant frm = new WinPM.frmAddTenant() { BuildingNumber = txtBuildingNumber.Text.Trim(), ThisApp = m_ThisApp };
            frm.Show();
        }
	}
}
