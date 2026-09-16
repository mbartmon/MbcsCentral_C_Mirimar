using System.Drawing;
using System.Collections;
using System.Windows.Controls;

namespace MbcsCentral.WinTrm
{

	public partial class frmRecipientImports
	{

		public MessageSchedule msgSched;

		private BindingClasses.bindAccountEmails[] m_emails = null;
		public BindingClasses.bindAccountEmails[] emails {
			get { return m_emails; }
		}


		private void frmRecipientImports_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			WaitWindow frmW = new WaitWindow();
			frmW.Owner = this;
            frmW.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frmW.Show();
            // Get Accounts
            BindingClasses.bindAccountEmails[] accts = Utils.getAccountEmails();
			lstAccounts.ItemsSource = accts;
			lstAccounts.DisplayMemberPath = "Name";
			lstAcctRecips.DisplayMemberPath = "Name";

			//Get Tenants
			Utils.getTenants(lstTenants);

            // Get Outlook contacts
            //BindingClasses.bindAccountEmails[] olContacts = Utils.getOutlookContacts();
            //lstOutlook.ItemsSource = olContacts;\
            Utils.GetOutlookContacts(lstOutlook);
			lstOutlook.DisplayMemberPath = "Name";
			lstOutRecips.DisplayMemberPath = "Name";

			frmW.Close();
			//If msgSched.recipients.Trim <> "" Then
			//Dim t() As String = msgSched.recipients.Split("|")
			//If t.Length > 0 Then
			//For Each s As String In t
			//Dim x() As String = s.Split(";")
			//Select Case x(0)
			//   Case "A"
			//lstAcctRecips.Items.Add(New bindAccountEmails(CDbl(x(1)), x(2), x(3), x(4)))
			//   Case "T"
			//lstTenRecips.Items.Add(String.Format("{0}: {1} {2}", x(1), x(2), x(3)))
			//End Select
			//Next
			//End If
			//End If

		}


		private void cmdAcctAddOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstAcctRecips.Items.IndexOf(lstAccounts.SelectedItem) < 0) {
				lstAcctRecips.Items.Add(lstAccounts.SelectedItem);
			}

		}


		private void cmdAcctDelOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstAcctRecips.SelectedIndex >= 0) {
				lstAcctRecips.Items.Remove(lstAcctRecips.SelectedItem);
			}

		}


		private void cmdAcctAddAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			lstAcctRecips.Items.Clear();
			lstAcctRecips.ItemsSource = lstAccounts.Items;

		}


		private void cmdAcctDelAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			lstAcctRecips.Items.Clear();

		}


		private void cmdTenAddOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			TreeViewItem t = (TreeViewItem)lstTenants.SelectedItem;
			if ((t.Tag == null))
				return;
			if (lstTenRecips.Items.IndexOf(t.Header) < 0) {
				lstTenRecips.Items.Add(t.Header);
			}

		}


		private void cmdTenDelOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstTenRecips.SelectedIndex >= 0) {
				lstTenRecips.Items.RemoveAt(lstTenRecips.SelectedIndex);
			}

		}


		private void cmdTenDelAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			lstTenRecips.Items.Clear();

		}


		private void cmdOK_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			m_emails = null;
			ArrayList list = new ArrayList();
			int l = 0;

			if (lstAcctRecips.Items.Count > 0) {
				foreach (BindingClasses.bindAccountEmails acct in lstAcctRecips.Items) {
					l = acct.lastName.IndexOf(":");
					if (l > 0) {
						acct.lastName = acct.lastName.Substring(0, l - 1);
					}
					list.Add(acct);
					//s &= String.Format("A;{0};{1};{2};{3}|", acct.Id, acct.firstName, acct.AddressType, acct.Email)
				}
			}
			if (lstTenRecips.Items.Count > 0) {
				string x = null;
				string[] names = null;
				string email = null;
				for (int i = 0; i <= lstTenRecips.Items.Count - 1; i++) {
					x = (string)lstTenRecips.Items[i];
					// Import an entire building
					if (x.ToLower().StartsWith("b:")) {

					} else {
						//s &= String.Format("T;{0}", x.Substring(0, x.IndexOf(":")))
						x = x.Substring(x.IndexOf(":") + 1);
						//s &= String.Format(";{0}", x.Substring(0, x.IndexOf("<")))
						names = Utils.parseName(x.Substring(0, x.IndexOf("<")));
						x = x.Substring(x.IndexOf("<") + 1);
						email = x.TrimEnd('>');
						//s &= String.Format(";{0}|", x)
						list.Add(new BindingClasses.bindAccountEmails(0, names[0].Trim(), names[1].Trim(), "E", email));
					}
				}
			}
			if (lstOutRecips.Items.Count > 0) {
				foreach (BindingClasses.bindAccountEmails ol in lstOutRecips.Items) {
					list.Add(ol);
				}
			}
			//msgSched.recipients = s.Substring(0, s.LastIndexOf("|"))
			m_emails = (BindingClasses.bindAccountEmails[])list.ToArray(typeof(BindingClasses.bindAccountEmails));

			this.Close();

		}


		private void cmdOutAddOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstOutRecips.Items.IndexOf(lstOutlook.SelectedItem) < 0) {
				lstOutRecips.Items.Add(lstOutlook.SelectedItem);
			}

		}


		private void cmdOutDelOne_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstOutRecips.SelectedIndex >= 0) {
				lstOutRecips.Items.Remove(lstOutRecips.SelectedItem);
			}

		}


		private void cmdOutAddAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			lstOutRecips.Items.Clear();
			lstOutRecips.ItemsSource = lstOutlook.Items;

		}


		private void cmdOutDelAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			try {
				lstOutRecips.Items.Clear();
			} catch {
			}

		}

		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}
		public frmRecipientImports()
		{
            InitializeComponent();
			Loaded += frmRecipientImports_Loaded;
		}
	}
}
