namespace WinTRM
{
	public class ListBoxItem
	{
		private string myDisplayItem;

		private long myValueItem;
		public ListBoxItem(string displayitem, long valueitem)
		{
			this.myDisplayItem = displayitem;
			this.myValueItem = valueitem;
		}

		public string DisplayItem {
			get { return myDisplayItem; }
		}
		public long ValueItem {
			get { return myValueItem; }
		}
		public override string ToString()
		{
			return this.myDisplayItem;
		}
	}
}
