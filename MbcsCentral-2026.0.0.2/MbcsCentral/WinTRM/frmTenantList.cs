using System.Windows.Forms;
namespace WinTRM
{

	public partial class frmTenantList
	{

		private ListItem[] _selectedTenants = null;
		public ListItem[] selectedTenants {
			get { return _selectedTenants; }
		}

		private bool _newIssue;
		public bool newIssue {
			set { _newIssue = value; }
		}

		private bool _bldOnly = false;
		public bool bldOnly {
			get { return _bldOnly; }
			set { _bldOnly = value; }
		}


		private void frmTenantList_Load(object sender, System.EventArgs e)
		{
			if (_newIssue) {
				this.Text = "Create New Issue For:";
				lstTenants.SelectionMode = SelectionMode.One;
			}

			switch (_bldOnly) {
				case true:
					Building[] blds = null;
					blds = Building.load();
					if ((blds != null)) {
						foreach (Building bld in blds) {
							lstTenants.Items.Add(new ListItem(bld.BuildingNumber + ": " + bld.Address1, bld.BuildingNumber));
						}
					}
					break;
				case false:
					Tenants[] tenant = null;

					tenant = Tenants.load(true, true);

					if ((tenant != null)) {
						foreach (Tenants ten in tenant) {
							lstTenants.Items.Add(new ListItem(ten.getBuildingNumber + ten.getTenantNumber + ": " + ten.getTenantName, ten.getSequenceNumber));
						}
						tenant = null;
					}
					break;
			}

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}


		private void cmdSelect_Click(object sender, System.EventArgs e)
		{
			if (lstTenants.SelectedItems.Count > 0) {
				ListBox.SelectedObjectCollection ids = lstTenants.SelectedItems;
				_selectedTenants = new ListItem[ids.Count];
				for (int i = 0; i <= ids.Count - 1; i++) {
					_selectedTenants(i) = ids.Item(i);
				}
			}

			this.Hide();

		}
		public frmTenantList()
		{
			Load += frmTenantList_Load;
		}
	}
}
