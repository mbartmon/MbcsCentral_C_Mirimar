using System;
using Microsoft.VisualBasic;
using System.Windows;

namespace MbcsCentral.WinTrm
{
	public partial class frmRecipientEdit
	{

		Recipients[] recips;

		int m_listId;
        string myApp;

        public frmRecipientEdit()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += frmRecipientEdit_Closing;
        }

        public frmRecipientEdit(string app)
        {
            myApp = app;
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += frmRecipientEdit_Closing;
        }

        private void frmRecipientEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}


		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			RecipientMaster master = RecipientMaster.load(m_listId);
			if ((master != null)) {
				this.Title = "LIST: " + master.listName;
			}
			System.Windows.Data.CollectionViewSource RecipientsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientsViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//RecipientsViewSource.Source = [generic data source]
			recips = Recipients.loadByList(m_listId);
			RecipientsViewSource.Source = recips;

		}


		private void getRecipients()
		{
			Main.getForm(this);
			System.Windows.Data.CollectionViewSource RecipientsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientsViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//RecipientsViewSource.Source = [generic data source]
			RecipientsViewSource.Source = null;
			recips = null;
			recips = Recipients.loadByList(m_listId);
			RecipientsViewSource.Source = recips;

		}
		public int listId {
			set { m_listId = value; }
		}


		private void cmdImport_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmRecipientImports frm = new frmRecipientImports();
			frm.Owner = this;
			frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();
			BindingClasses.bindAccountEmails[] r = frm.emails;
			if ((r != null)) {
				Recipients recp = default(Recipients);
				foreach (BindingClasses.bindAccountEmails a in r) {
					recp = new Recipients();
					recp.list_Id = m_listId;
					recp.first_name = a.firstName;
					recp.last_name = a.lastName;
					recp.email_address = a.Email;
					recp.save();
					recp = null;
				}
			}
			WaitWindow frmW = new WaitWindow();
			frmW.Owner = this;
			frmW.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frmW.Show();
			getRecipients();
			frmW.Close();

		}


		private void EditRecip(object sender, RoutedEventArgs e)
		{
			if (datRecipients.SelectedIndex < 0) {
				Interaction.MsgBox("No recipient selected");
				return;
			}
			Recipients recip = (Recipients)datRecipients.SelectedItem;
			if ((recip == null)) {
				Interaction.MsgBox("Unable to retrieve recipient ID");
				return;
			}
			frmRecipient frm = new frmRecipient();
			frm.listId = m_listId;
			frm.recipId = recip.recipient_Id;
			frm.Owner = this;
			frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();
			getRecipients();

		}


		private void DeleteRecip(object sender, RoutedEventArgs e)
		{
			if (recips.Length <= 0) {
				Interaction.MsgBox("There are no recipients in this list to delete.");
				return;
			}
			if (Interaction.MsgBox("Are you SURE that you want to DELETE all recipients for this list?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
				if (Interaction.MsgBox("You will not be able to restore these recipients once deleted.  Delete any way?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
					int c = 0;
					for (int i = 0; i <= recips.Length - 1; i++) {
						if (recips[i].delete()) {
							c += 1;
						}
					}
					Interaction.MsgBox(c + " recipients deleted.");
				}
			}

		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmRecipient frm = new frmRecipient();
			frm.listId = m_listId;
			frm.recipId = 0;
			frm.Owner = this;
			frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();
			getRecipients();

		}
	}
}
