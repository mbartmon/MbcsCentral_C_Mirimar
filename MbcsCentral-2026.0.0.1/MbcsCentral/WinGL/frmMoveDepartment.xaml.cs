using Microsoft.VisualBasic;
namespace MbcsCentral.WinGL
{
	public partial class frmMoveDepartment
	{

		private Department m_buildingRec;

		private Company[] m_companies;
		public Department DepartmentRecord {
			set { m_buildingRec = value; }
		}
		public Company[] Companies {
			set { m_companies = value; }
		}

        public frmMoveDepartment()
        {
            InitializeComponent();
        }

		private void frmMoveDepartment_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}

		private void frmMoveDepartment_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			if ((m_buildingRec != null)) {
				txtDepartment.DataContext = m_buildingRec;
			} else {
				Interaction.MsgBox("Department not set for this function.");
				this.Close();
			}
			if ((m_companies != null)) {
				lstCompanies.ItemsSource = m_companies;
				lstCompanies.DisplayMemberPath = "Name";
			} else {
				Interaction.MsgBox("Companies not set for this function.");
				this.Close();
			}

		}

		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstCompanies.SelectedIndex >= 0) {
				Company cmp = (Company)lstCompanies.SelectedItem;
				if ((cmp != null)) {
					m_buildingRec.CompanyNumber = cmp.CompanyNumber;
					if (m_buildingRec.save()) {
						Interaction.MsgBox("Department moved to new company");
						this.Close();
					} else {
						Interaction.MsgBox("There was a problem.  Department not moved.");
					}
				}
			} else {
				Interaction.MsgBox("You have not selected a new Company.");
			}

		}

		private void lstCompanies_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdSave_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}
	}
}
