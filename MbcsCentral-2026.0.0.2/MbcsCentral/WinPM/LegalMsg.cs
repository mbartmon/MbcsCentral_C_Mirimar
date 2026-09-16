using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
	public class LegalMsg : clsDB	, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		static string TABLE = "pm_LegalMsg";

		public const string SQL_COL_CLIENT_ID = "clientId";
		public const string SQL_COL_I_D = "ID";
		public const string SQL_COL_CODE = "Code";
		public const string SQL_COL_MESSAGE = "Message";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				" FROM " + TABLE +
				" WHERE " + SQL_COL_I_D + " = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				" FROM " + TABLE;

		static string SQL_LOAD_ALL_BY_CLIENTID = "SELECT " +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				" FROM " + TABLE +
				" WHERE [" + SQL_COL_CLIENT_ID + "] = ?";

			static string SQL_LOAD_BY_CODE = "SELECT " +
				 "[" + SQL_COL_CLIENT_ID + "]," +
				 "[" + SQL_COL_I_D + "]," +
				 "[" + SQL_COL_CODE + "]," +
				 "[" + SQL_COL_MESSAGE + "]" +
				 " FROM " + TABLE +
				 " WHERE " + SQL_COL_CODE + " = '?';";

			static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
					   "[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				") VALUES (?, '?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_CLIENT_ID + "]=?," +
				"[" + SQL_COL_CODE + "]='?'," +
				"[" + SQL_COL_MESSAGE + "]='?'" +
				" WHERE " + SQL_COL_I_D + " = ?;";

		protected long m_clientId;
		protected long m_ID;
		protected string m_Code;
		protected string m_Message;
		protected bool m_isChanged;

		public LegalMsg() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public LegalMsg(long clientId, string Code,string Message) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_clientId = clientId;
			m_Code = Code;
			m_Message = Message;
		}

		protected LegalMsg fill(DataRow dr)
		{
			try {
				m_clientId = (int) dr[SQL_COL_CLIENT_ID];
				m_ID = (int) dr[SQL_COL_I_D];
				m_Code = (string) (Convert.IsDBNull(dr[SQL_COL_CODE]) ? "" : dr[SQL_COL_CODE]);
				m_Message = (string) (Convert.IsDBNull(dr[SQL_COL_MESSAGE]) ? "" : dr[SQL_COL_MESSAGE]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static LegalMsg load( long m_ID)
		{
			LegalMsg obj  = new LegalMsg();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static LegalMsg[] loadAll(long clientId)
		{
			ArrayList list = new ArrayList();

			string[] opt = { clientId.ToString() };
			DataSet ds = new DataSet();
			LegalMsg obj = new LegalMsg();

			ds = obj.load(SQL_LOAD_ALL_BY_CLIENTID,opt);
			if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new LegalMsg();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (LegalMsg[])list.ToArray(typeof(LegalMsg));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static LegalMsg loadByCode(string m_Code)
		{
			LegalMsg obj = new LegalMsg();
			string[] opt = { m_Code };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_CODE, opt);
			if (ds != null)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			}
			else
				return null;

		}

				/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
		{
		if (m_ID > 0 ) {
			string[] values = {m_clientId.ToString(), m_Code, m_Message, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_clientId.ToString(), m_Code, m_Message};
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
		public long clientId {
			set{ m_clientId = value;
			OnPropertyChanged("clientId"); }
			get { return m_clientId; }
		}

		public long ID {
			get { return m_ID; }
		}

		public string Code {
			set{ m_Code = value;
			OnPropertyChanged("Code"); }
			get { return m_Code; }
		}

		public string Message {
			set{ m_Message = value;
			OnPropertyChanged("Message"); }
			get { return m_Message; }
		}

	}
}
