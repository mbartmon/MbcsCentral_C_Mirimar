using Microsoft.VisualBasic;
namespace MbcsCentral.WinPM
{
	public partial class frmMoveBuilding
	{

		private Building m_buildingRec;

		private Owners[] m_owners;
		public Building BuildingRecord {
			set { m_buildingRec = value; }
		}
		public Owners[] Owners {
			set { m_owners = value; }
		}

        public frmMoveBuilding()
        {
            InitializeComponent();
        }

		private void frmMoveBuilding_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}

		private void frmMoveBuilding_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			if ((m_buildingRec != null)) {
				txtBuilding.DataContext = m_buildingRec;
			} else {
				Interaction.MsgBox("Building not set for this function.");
				this.Close();
			}
			if ((m_owners != null)) {
				lstOwners.ItemsSource = m_owners;
				lstOwners.DisplayMemberPath = "OwnerName";
			} else {
				Interaction.MsgBox("Owners not set for this function.");
				this.Close();
			}

		}

		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstOwners.SelectedIndex >= 0) {
				Owners own = (Owners)lstOwners.SelectedItem;
				if ((own != null)) {
					m_buildingRec.OwnerNumber = own.OwnerNumber;
					if (m_buildingRec.save()) {
						Interaction.MsgBox("Building moved to new owner");
						this.Close();
					} else {
						Interaction.MsgBox("There was a problem.  Building not moved.");
					}
				}
			} else {
				Interaction.MsgBox("You have not selected a new Owner.");
			}

		}

		private void lstOwners_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdSave_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}
	}
}
