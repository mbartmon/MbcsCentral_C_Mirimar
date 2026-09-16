namespace WinTRM
{
	public partial class frmIssuesList
	{

		private void frmNotesList_Load(object sender, System.EventArgs e)
		{
			for (int i = 0; i <= lstNotes.Columns.Count - 2; i++) {
				lstNotes.Columns(i).AutoResize(Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
			}
		}


		private void lstNotes_DoubleClick(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ListView lst = (System.Windows.Forms.ListView)sender;
			System.Windows.Forms.ListView.SelectedListViewItemCollection itms = lst.SelectedItems;
			System.Windows.Forms.ListViewItem itm = itms(0);
			long key = 0;
			try {
				key = Convert.ToInt64(itm.SubItems(4).Text);
			} catch {
				return;
			}
			if (key > 0) {
				frmIssue frm = new frmIssue();
				frm.issueId = key;
				frm.BringToFront();
				frm.ShowDialog();
			}
		}
		public frmIssuesList()
		{
			Load += frmNotesList_Load;
		}
	}
}
