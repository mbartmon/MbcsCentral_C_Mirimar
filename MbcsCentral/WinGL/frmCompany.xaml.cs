using System.Windows;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinGL
{
	public partial class frmCompany
	{

		private bool m_add;
		public bool Add {
			get { return m_add; }
			set { m_add = value; }
		}
		private int m_id;
		public int ID {
			set { m_id = value; }
		}

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
		private new Company company = null;

        public frmCompany()
        {
            InitializeComponent();
            this.Loaded += frmCompany_Loaded;
            this.Closing += frmCompany_Closing;
        }
        public frmCompany(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmCompany_Loaded;
            this.Closing += frmCompany_Closing;
        }

        private bool SaveCompany()
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return true;
            }
			if (m_add) {
				Company xCompany = Company.loadByCompany(company.CompanyNumber);
				if (xCompany != null) {
					Interaction.MsgBox("There is already a Company with that Company Number.");
					return false;
				}
			}
            company.IsChanged = false;
			return company.save();
		}

		private void frmCompany_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ((company != null) && company.IsChanged) {
				switch (Interaction.MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						if (!SaveCompany()) {
							Interaction.MsgBox("Company record not saved");
							e.Cancel = true;
							return;
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

		private void frmCompany_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			if (m_add) {
				company = new Company();
				txtCompanyNumber.Focus();
			} else {
				company = Company.loadByCompany(m_id);
				txtCompanyNumber.IsReadOnly = true;
				txtCompanyName.Focus();
			}

			MainGrid.DataContext = company;


		}


		private void cmdOk_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (!SaveCompany())
				Interaction.MsgBox("Company record not saved.");
            this.Close();
		}


		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}
	}
}
