using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
	public class BatchRptList : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_BatchRptList";

	public const string SQL_COL_KEY = "Key";
	public const string SQL_COL_LIST_NAME = "ListName";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_KEY + "]," +
				"[" + SQL_COL_LIST_NAME + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_KEY + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_KEY + "]," +
				"[" + SQL_COL_LIST_NAME + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_LIST_NAME + "]" +
				") VALUES ('?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_LIST_NAME + "]='?'" +
				" WHERE " + "[" + SQL_COL_KEY + "] = ?;";

		protected int m_Key;
		protected string m_ListName;
		protected bool m_isChanged;

		public BatchRptList() : base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public BatchRptList(int Key,string ListName) : 
				base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_Key = Key;
			m_ListName = ListName;
		}

		protected BatchRptList fill(DataRow dr)
		{
			try {
				m_Key = (int) dr[SQL_COL_KEY];
				m_ListName = (string) (Convert.IsDBNull(dr[SQL_COL_LIST_NAME]) ? "" : dr[SQL_COL_LIST_NAME]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static BatchRptList load( int m_Key)
		{
			BatchRptList obj  = new BatchRptList();
			string[] opt = {m_Key.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static BatchRptList[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			BatchRptList obj = new BatchRptList();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new BatchRptList();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (BatchRptList[])list.ToArray(typeof(BatchRptList));
			} catch (SqlException ex) {
				return null;
			}

		}

		public bool save()
		{
			if (m_Key > 0 ) {
				string[] values = {m_ListName, m_Key.ToString()};
				if (this.updateRecord(m_Key.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_ListName};
				m_Key = this.insertRecord(values);
				if (m_Key >= 0) {
					return true;
				}
			}
			return false;

		}

		private void OnPropertyChanged(string info) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				m_isChanged = true;
			}
		}

		// Properties
		public int Key {
			get { return m_Key; }
		}

		public string ListName {
			set{ m_ListName = value;
			OnPropertyChanged("ListName"); }
			get { return m_ListName; }
		}

}
}
