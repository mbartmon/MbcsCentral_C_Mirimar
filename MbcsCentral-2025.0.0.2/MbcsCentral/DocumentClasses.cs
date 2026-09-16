using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
	public class DocumentClasses : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "DocumentClasses";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CLASS_NAME = "ClassName";
	public const string SQL_COL_DESCRIPTION = "Description";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_CLASS_NAME + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_CLASS_NAME + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_CLASS_NAME + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				") VALUES ('?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_CLASS_NAME + "]='?'," +
				"[" + SQL_COL_DESCRIPTION + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		protected int m_ID;
		protected string m_ClassName;
		protected string m_Description;
		protected bool m_isChanged;

		public DocumentClasses() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public DocumentClasses(int ID,string ClassName,string Description) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_ID = ID;
			m_ClassName = ClassName;
			m_Description = Description;
		}

		protected DocumentClasses fill(DataRow dr)
		{
			try {
				m_ID = (int) dr[SQL_COL_I_D];
				m_ClassName = (string) (Convert.IsDBNull(dr[SQL_COL_CLASS_NAME]) ? "" : dr[SQL_COL_CLASS_NAME]);
				m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static DocumentClasses load( int m_ID)
		{
			DocumentClasses obj  = new DocumentClasses();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static DocumentClasses[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			DocumentClasses obj = new DocumentClasses();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new DocumentClasses();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (DocumentClasses[])list.ToArray(typeof(DocumentClasses));
			} catch (SqlException ex) {
				return null;
			}

		}

		public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_ClassName, m_Description, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_ClassName, m_Description};
				m_ID = this.insertRecord(values);
				if (m_ID >= 0) {
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
		public int ID {
			get { return m_ID; }
			set { m_ID = value;}
		}

		public string ClassName {
			set{ m_ClassName = value;
			OnPropertyChanged("ClassName"); }
			get { return m_ClassName; }
		}

		public string Description {
			set{ m_Description = value;
			OnPropertyChanged("Description"); }
			get { return m_Description; }
		}

}
}
