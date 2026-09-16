namespace WinTRM
{
	public class issuesObj
	{

		protected System.Windows.Controls.TreeView _lst;
		protected int _field;
		protected bool _vAll;
		public System.Windows.Controls.TreeView lst {
			get { return _lst; }
			set { _lst = value; }
		}
		public int field {
			get { return _field; }
			set { _field = value; }
		}
		public bool vAll {
			get { return _vAll; }
			set { _vAll = value; }
		}


		public issuesObj()
		{
		}

	}
}
