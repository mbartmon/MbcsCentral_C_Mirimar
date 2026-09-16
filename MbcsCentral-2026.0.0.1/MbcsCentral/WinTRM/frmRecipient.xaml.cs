using System;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinTrm
{
	public partial class frmRecipient
	{
		private int m_recipId;
		private int m_listId;

		Recipients[] recip = new Recipients[2];

		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Data.CollectionViewSource RecipientsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientsViewSource");
			//Dim RecipientsViewSource As New Binding("RecipientsViewSource")
			//RecipientsViewSource.Source = [generic data source]
			if (m_recipId == 0) {
				recip[0] = new Recipients();
				recip[0].list_Id = m_listId;
			} else {
				try {
					recip[0] = Recipients.load(m_recipId);
					if ((recip == null)) {
						if (m_listId == 0) {
							Interaction.MsgBox("Invalid list");
							this.Close();
						} else {
							recip[0] = new Recipients();
							recip[0].list_Id = m_listId;
						}
					}
				} catch {
					recip[0] = new Recipients();
					recip[0].list_Id = m_listId;
				}
			}
			try {
				RecipientsViewSource.Source = recip;
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}

		}

		public int recipId {
			set { m_recipId = value; }
		}
		public int listId {
			set { m_listId = value; }
		}
		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}

		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			recip[0].save();
			this.Close();
		}
		public frmRecipient()
		{
			Loaded += Window_Loaded;
		}
	}
}
