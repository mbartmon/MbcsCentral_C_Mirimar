using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace MbcsCentral.WinTrm
{
	public partial class frmRecipListSelect
	{

		private long[] m_ListIds;

		private RecipLists lists = new RecipLists();

		private void frmRecipListSelect_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((m_ListIds == null)) {
				this.Close();
			}

			RecipientMaster list = default(RecipientMaster);
			foreach (int l in m_ListIds) {
				list = RecipientMaster.load(l);
				if ((list != null)) {
					lists.Add(list);
				}
			}
			if ((lists != null)) {
				lstLists.DataContext = lists;
			} else {
				Interaction.MsgBox("Could not retrieve any lists");
				this.Close();
			}

		}


		private void lstLists_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
            RecipientMaster lst = (RecipientMaster)lstLists.SelectedValue;

			if (lst.listId > 0) {
				frmRecipientEdit frm = new frmRecipientEdit();
				frm.listId = lst.listId;
				frm.Show();
			}

		}


		private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

		public long[] ListIds {
			set { m_ListIds = value; }
		}
		public frmRecipListSelect()
		{
			Loaded += frmRecipListSelect_Loaded;
		}

	}
}
namespace MbcsCentral.WinTrm
{

	public class RecipLists : List<RecipientMaster>
	{


		public RecipLists()
		{
		}

		public RecipLists(RecipientMaster recip)
		{
			this.Add(recip);
		}

		public void Add(RecipientMaster recip)
		{
			base.Add(recip);
		}
	}
}
namespace MbcsCentral.WinTrm
{

	public class RecipList
	{

		private long m_listId;

		private string m_listName;

		public RecipList()
		{
		}

		public RecipList(long listId, string listName)
		{
			m_listId = listId;
			m_listName = listName;
		}

		public long ListId {
			get { return m_listId; }
			set { m_listId = value; }
		}
		public string ListName {
			get { return m_listName; }
			set { m_listName = value; }
		}

	}
}
