using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
	public class LockboxHistory : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "pm_LockboxHistory";

	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_DATE = "Date";
	public const string SQL_COL_TIME_STAMP = "TimeStamp";
	public const string SQL_COL_FILE_NAME = "FileName";
	public const string SQL_COL_BATCHES = "Batches";
	public const string SQL_COL_USER = "User";
	public const string SQL_COL_TYPE = "Type";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_DATE + "]," +
				"[" + SQL_COL_TIME_STAMP + "]," +
				"[" + SQL_COL_FILE_NAME + "]," +
				"[" + SQL_COL_BATCHES + "]," +
				"[" + SQL_COL_USER + "]," +
				"[" + SQL_COL_TYPE + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_DATE + "]," +
				"[" + SQL_COL_TIME_STAMP + "]," +
				"[" + SQL_COL_FILE_NAME + "]," +
				"[" + SQL_COL_BATCHES + "]," +
				"[" + SQL_COL_USER + "]," +
				"[" + SQL_COL_TYPE + "]" + 
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_DATE + "]," +
				"[" + SQL_COL_TIME_STAMP + "]," +
				"[" + SQL_COL_FILE_NAME + "]," +
				"[" + SQL_COL_BATCHES + "]," +
				"[" + SQL_COL_USER + "]," +
				"[" + SQL_COL_TYPE + "]" + 
				") VALUES ('?', '?', '?', '?', '?', ?);";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_DATE + "]='?'," +
				"[" + SQL_COL_TIME_STAMP + "]='?'," +
				"[" + SQL_COL_FILE_NAME + "]='?'," +
				"[" + SQL_COL_BATCHES + "]='?'," +
				"[" + SQL_COL_USER + "]='?'," +
				"[" + SQL_COL_TYPE + "]=?" +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_BY_DATE_RANGE = "SELECT " +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_TIME_STAMP + "]," +
                "[" + SQL_COL_FILE_NAME + "]," +
                "[" + SQL_COL_BATCHES + "]," +
				"[" + SQL_COL_USER + "]," +
				"[" + SQL_COL_TYPE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_DATE + "]  BETWEEN '?' AND '?'" + 
			    " AND [" + SQL_COL_TYPE + "=?;";

		public enum PaymentType
		{
			Lockbox,
			AR,
			RPS,
			NACHA
		}

		public static string[] FileType = { "Lockbox", "One Pay", "RPS", "NACHA" };

        protected int m_Id;
        protected DateTime m_Date;
		protected DateTime m_TimeStamp;
		protected string m_FileName;
		protected string m_Batches;
		protected string m_User;
		protected int m_Type;
		protected bool m_isChanged;

         public struct BatchDetails_struct
        {
            public string BatchNumber;
            public decimal Amount;
            public string Count;
        }

		public LockboxHistory() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public LockboxHistory(int Id,DateTime Date,DateTime TimeStamp,string FileName,string Batches,string User, int Type) : 
				base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_Id = Id;
			m_Date = Date;
			m_TimeStamp = TimeStamp;
			m_FileName = FileName;
			m_Batches = Batches;
			m_User = User;
			m_Type = Type;
		}

		protected LockboxHistory fill(DataRow dr)
		{
			try {
				m_Id = (int) dr[SQL_COL_ID];
				m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
				m_TimeStamp = (DateTime) (Convert.IsDBNull(dr[SQL_COL_TIME_STAMP]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_TIME_STAMP]);
				m_FileName = (string) (Convert.IsDBNull(dr[SQL_COL_FILE_NAME]) ? "" : dr[SQL_COL_FILE_NAME]);
				m_Batches = (string) (Convert.IsDBNull(dr[SQL_COL_BATCHES]) ? "" : dr[SQL_COL_BATCHES]);
				m_User = (string) (Convert.IsDBNull(dr[SQL_COL_USER]) ? "" : dr[SQL_COL_USER]);
				m_Type = (int)dr[SQL_COL_TYPE];
			} catch (SqlException ex) {
			}
			return this;

		}

		public static LockboxHistory load( int m_Id)
		{
			LockboxHistory obj  = new LockboxHistory();
			string[] opt = {m_Id.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static LockboxHistory[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			LockboxHistory obj = new LockboxHistory();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new LockboxHistory();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (LockboxHistory[])list.ToArray(typeof(LockboxHistory));
			} catch (SqlException ex) {
				return null;
			}

		}

        public static LockboxHistory[] loadByDateRange(DateTime from, DateTime to, int type, bool monthOnly)
        {
            ArrayList list = new ArrayList();
            DateTime monthYear;
            DataSet ds = new DataSet();
            LockboxHistory obj = new LockboxHistory();
            if (monthOnly)
            {
                monthYear = new DateTime(from.Year, from.Month, 1);
                from = monthYear;
                monthYear = new DateTime(from.Year, from.Month, DateTime.DaysInMonth(from.Year, from.Month));
                to = monthYear;
            }
            string[] parms = { from.ToShortDateString(), to.ToShortDateString() };
            ds = obj.load(SQL_LOAD_BY_DATE_RANGE, parms);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new LockboxHistory();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (LockboxHistory[])list.ToArray(typeof(LockboxHistory));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
        {
			if (m_Id > 0 ) {
				string[] values = {m_Date.ToString(), m_TimeStamp.ToString(), m_FileName, m_Batches, m_User, m_Type.ToString(), m_Id.ToString()};
				if (this.updateRecord(m_Id.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_Date.ToString(), m_TimeStamp.ToString(), m_FileName, m_Batches, m_User, m_Type.ToString()};
				m_Id = this.insertRecord(values);
				if (m_Id >= 0) {
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
		public int Id {
			get { return m_Id; }
		}

		public DateTime Date {
			set{ m_Date = value;
			OnPropertyChanged("Date"); }
			get { return m_Date; }
		}

		public DateTime TimeStamp {
			set{ m_TimeStamp = value;
			OnPropertyChanged("TimeStamp"); }
			get { return m_TimeStamp; }
		}

		public string FileName {
			set{ m_FileName = value;
			OnPropertyChanged("FileName"); }
			get { return m_FileName; }
		}

		public string Batches {
			set{ m_Batches = value;
			OnPropertyChanged("Batches"); }
			get { return m_Batches; }
		}

		public string User {
			set{ m_User = value;
			OnPropertyChanged("User"); }
			get { return m_User; }
		}

		public int Type
		{
			set { m_Type = value;
				OnPropertyChanged("Type");
			}
		}

        public string[] UnPackBatches
        {
            get
            {
                string[] s = m_Batches.Split('|');
                return s;
            }
        }

        public BatchDetails_struct[] BatchDetails
        {
            get
            {
                string s = "";
                string item = "";
                decimal amount;
                string[] batches = UnPackBatches;
                if (batches == null || batches.Length == 0)
                    return null;
                BatchDetails_struct[] details; 
                details = new BatchDetails_struct[batches.Length];
                for (int i = 0; i < batches.Length; i++)
                {
                    item = batches[i].Substring(0, batches[i].IndexOf(':'));
                    batches[i] = batches[i].Substring(batches[i].IndexOf(':') + 1);
                    details[i].BatchNumber = item;
                    item = batches[i].Substring(0, batches[i].IndexOf('-'));
                    batches[i] = batches[i].Substring(batches[i].IndexOf('-') + 1);
                    amount = 0;
                    decimal.TryParse(item, out amount);
                    details[i].Amount = amount;
                    details[i].Count = batches[i];
                }
                return details;
            }
        }

        public static BatchDetails_struct[] GetBatchDetails(ArrayList batches)
        {
            string item = "";
            decimal amount;
            if (batches == null || batches.Count == 0)
                return null;
            BatchDetails_struct[] details;
            details = new BatchDetails_struct[batches.Count];
            for (int i = 0; i < batches.Count; i++)
            {
                string s = batches[i].ToString();
                item = s.Substring(0, s.IndexOf(':'));
                s = s.Substring(s.ToString().IndexOf(':') + 1);
                details[i].BatchNumber = item;
                item = s.Substring(0, s.IndexOf('-'));
                s = s.Substring(s.IndexOf('-') + 1);
                amount = 0;
                decimal.TryParse(item, out amount);
                details[i].Amount = amount;
                details[i].Count = s;
            }
            return details;
        }


    }
}
