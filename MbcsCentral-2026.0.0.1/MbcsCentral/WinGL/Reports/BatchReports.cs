using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
	public class BatchReports : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_BatchReports";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_BATCH_KEY = "BatchKey";
	public const string SQL_COL_REPORT = "Report";
	public const string SQL_COL_REPORT_TYPE = "ReportType";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_BATCH_KEY + "]," +
				"[" + SQL_COL_REPORT + "]," +
				"[" + SQL_COL_REPORT_TYPE + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_BATCH_KEY + "]," +
				"[" + SQL_COL_REPORT + "]," +
				"[" + SQL_COL_REPORT_TYPE + "]" +
				" FROM " + TABLE + ";";

        static string SQL_LOAD_BY_KEY = "SELECT " +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_BATCH_KEY + "]," +
                "[" + SQL_COL_REPORT + "]," +
                "[" + SQL_COL_REPORT_TYPE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_BATCH_KEY + "] = ?;";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_BATCH_KEY + "]," +
				"[" + SQL_COL_REPORT + "]," +
				"[" + SQL_COL_REPORT_TYPE + "]" +
				") VALUES (?, '?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_BATCH_KEY + "]=?," +
				"[" + SQL_COL_REPORT + "]='?'," +
				"[" + SQL_COL_REPORT_TYPE + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		protected int m_ID;
		protected int m_BatchKey;
		protected string m_Report;
		protected string m_ReportType;
		protected bool m_isChanged;

		public BatchReports() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public BatchReports(int ID,int BatchKey,string Report,string ReportType) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_ID = ID;
			m_BatchKey = BatchKey;
			m_Report = Report;
			m_ReportType = ReportType;
		}

		protected BatchReports fill(DataRow dr)
		{
			try {
				m_ID = (int) dr[SQL_COL_I_D];
				m_BatchKey = (int) dr[SQL_COL_BATCH_KEY];
				m_Report = (string) (Convert.IsDBNull(dr[SQL_COL_REPORT]) ? "" : dr[SQL_COL_REPORT]);
				m_ReportType = (string) (Convert.IsDBNull(dr[SQL_COL_REPORT_TYPE]) ? "" : dr[SQL_COL_REPORT_TYPE]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static BatchReports load( int m_ID)
		{
			BatchReports obj  = new BatchReports();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static BatchReports[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			BatchReports obj = new BatchReports();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new BatchReports();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (BatchReports[])list.ToArray(typeof(BatchReports));
			} catch (SqlException ex) {
				return null;
			}

		}

        public static BatchReports[] loadByKey(int key)
        {
            ArrayList list = new ArrayList();
            string[] opts = { key.ToString() };
            DataSet ds = new DataSet();
            BatchReports obj = new BatchReports();

            ds = obj.load(SQL_LOAD_BY_KEY, opts);
            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new BatchReports();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (BatchReports[])list.ToArray(typeof(BatchReports));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_BatchKey.ToString(), m_Report, m_ReportType, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_BatchKey.ToString(), m_Report, m_ReportType};
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
		}

		public int BatchKey {
			set{ m_BatchKey = value;
			OnPropertyChanged("BatchKey"); }
			get { return m_BatchKey; }
		}

		public string Report {
			set{ m_Report = value;
			OnPropertyChanged("Report"); }
			get { return m_Report; }
		}

		public string ReportType {
			set{ m_ReportType = value;
			OnPropertyChanged("ReportType"); }
			get { return m_ReportType; }
		}

}
}
