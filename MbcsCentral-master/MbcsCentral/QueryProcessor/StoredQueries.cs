using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
	public class StoredQueries : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "StoredQueries";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_PARAMETERS = "Parameters";
	public const string SQL_COL_QUERY = "Query";
	public const string SQL_COL_NAME = "Name";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_PARAMETERS + "]," +
				"[" + SQL_COL_QUERY + "]," +
				"[" + SQL_COL_NAME + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_PARAMETERS + "]," +
				"[" + SQL_COL_QUERY + "]," +
				"[" + SQL_COL_NAME + "]" +
				" FROM " + TABLE + 
                " ORDER BY [" + SQL_COL_NAME + "];";

        static string SQL_INSERT = "SET QUOTED_IDENTIFIER OFF; " +
                "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_PARAMETERS + "]," +
				"[" + SQL_COL_QUERY + "]," +
				"[" + SQL_COL_NAME + "]" +
				") VALUES ('?', \"?\", '?');" +    
                " SET QUOTED_IDENTIFIER ON; ";

        static string SQL_UPDATE = "SET QUOTED_IDENTIFIER OFF; " +
                "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_PARAMETERS + "]='?'," +
                "[" + SQL_COL_QUERY + "]= \"?\"," +
                "[" + SQL_COL_NAME + "]='?'" +
                " WHERE " + "[" + SQL_COL_I_D + "] = ?;" +
                " SET QUOTED_IDENTIFIER ON; ";

        static string SQL_LOAD_BY_NAME = "SELECT " +
        "[" + SQL_COL_I_D + "]," +
        "[" + SQL_COL_PARAMETERS + "]," +
        "[" + SQL_COL_QUERY + "]," +
        "[" + SQL_COL_NAME + "]" +
        " FROM " + TABLE +
        " WHERE " + "[" + SQL_COL_NAME + "] = '?';";


        protected int m_ID;
		protected string m_Parameters;
		protected string m_Query;
		protected string m_Name;
		protected bool m_isChanged;

		public StoredQueries() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public StoredQueries(int ID,string Parameters,string Query,string Name) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_ID = ID;
			m_Parameters = Parameters;
			m_Query = Query;
			m_Name = Name;
		}

		protected StoredQueries fill(DataRow dr)
		{
			try {
				m_ID = (int) dr[SQL_COL_I_D];
				m_Parameters = (string) (Convert.IsDBNull(dr[SQL_COL_PARAMETERS]) ? "" : dr[SQL_COL_PARAMETERS]);
				m_Query = (string) (Convert.IsDBNull(dr[SQL_COL_QUERY]) ? "" : dr[SQL_COL_QUERY]);
				m_Name = (string) (Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static StoredQueries load( int m_ID)
		{
			StoredQueries obj  = new StoredQueries();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;
		}

        public static StoredQueries load(string name)
        {
            StoredQueries obj = new StoredQueries();
            string[] opt = {name };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_NAME, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public static StoredQueries[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			StoredQueries obj = new StoredQueries();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new StoredQueries();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (StoredQueries[])list.ToArray(typeof(StoredQueries));
			} catch (SqlException ex) {
				return null;
			}

		}

		public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_Parameters, m_Query, m_Name, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_Parameters, m_Query, m_Name};
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
            set { m_ID = value; }
		}

		public string Parameters {
			set{ m_Parameters = value;
			OnPropertyChanged("Parameters"); }
			get { return m_Parameters; }
		}

		public string Query {
			set{ m_Query = value;
			OnPropertyChanged("Query"); }
			get { return m_Query; }
		}

		public string Name {
			set{ m_Name = value;
			OnPropertyChanged("Name"); }
			get { return m_Name; }
		}

}
}
