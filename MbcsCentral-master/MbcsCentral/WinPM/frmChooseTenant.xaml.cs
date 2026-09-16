    using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;
namespace MbcsCentral.WinPM
{
	public partial class frmChooseTenant
	{

		private Tenants[] tens = null;
        private STTenants[] stens = null;
		private int seq = -1;
		public Tenants[] Tenants {
			set { tens = value; }
		}
        public STTenants[] SecTenants
        {
            set { stens = value; }
        }
		public int SequenceNumber {
			get { return seq; }
		}

        public frmChooseTenant()
        {
            InitializeComponent();
        }

		private void cmdSelect_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstTenants.SelectedIndex < 0) {
				Interaction.MsgBox("No tenant selected.");
				return;
			}

			ListItem ten = (ListItem)lstTenants.SelectedItem;
			seq = (int)ten.ValueItem;
			this.Close();

		}

		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			seq = -1;
			this.Close();

		}


		private void frmChooseTenant_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Topmost = true;
			if ((tens == null || tens.Length == 0) && (stens == null || stens.Length == 0)) {
				Interaction.MsgBox("No tenants from which to select.");
				this.Close();
                return;
			}
			ArrayList items = new ArrayList();
            if (tens != null && tens.Length > 0)
            {
			    foreach (Tenants ten in tens) 
                    items.Add(new ListItem(ten.TenantName, ten.SequenceNumber));
			}
            else
            {
                foreach (STTenants ten in stens)
                    items.Add(new ListItem(ten.TenantName, ten.SequenceNumber));
            }
			lstTenants.DataContext = items;

		}

        private void lstTenants_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cmdSelect_Click(sender, new System.Windows.RoutedEventArgs());
        }
	}
}
