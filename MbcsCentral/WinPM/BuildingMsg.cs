using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
	public class BuildingMsg : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "pm_BuildingMsg";

	public const string SQL_COL_CLIENT_ID = "clientId";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_BUILDING_NUMBER = "BuildingNumber";
	public const string SQL_COL_MESSAGE = "Message";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_BUILDING_NUMBER + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_BUILDING_NUMBER + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_CLIENT_ID + "]," +
				"[" + SQL_COL_BUILDING_NUMBER + "]," +
				"[" + SQL_COL_MESSAGE + "]" +
				") VALUES (?, '?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_CLIENT_ID + "]=?," +
				"[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
				"[" + SQL_COL_MESSAGE + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		protected int m_clientId;
		protected int m_ID;
		protected string m_BuildingNumber;
		protected string m_Message;
		protected bool m_isChanged;

		public BuildingMsg() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public BuildingMsg(int clientId,int ID,string BuildingNumber,string Message) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_clientId = clientId;
			m_ID = ID;
			m_BuildingNumber = BuildingNumber;
			m_Message = Message;
		}

		protected BuildingMsg fill(DataRow dr)
		{
			try {
				m_clientId = (int) dr[SQL_COL_CLIENT_ID];
				m_ID = (int) dr[SQL_COL_I_D];
				m_BuildingNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_Message = (string) (Convert.IsDBNull(dr[SQL_COL_MESSAGE]) ? "" : dr[SQL_COL_MESSAGE]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static BuildingMsg load( int m_ID)
		{
			BuildingMsg obj  = new BuildingMsg();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static BuildingMsg[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			BuildingMsg obj = new BuildingMsg();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new BuildingMsg();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (BuildingMsg[])list.ToArray(typeof(BuildingMsg));
			} catch (SqlException ex) {
				return null;
			}

		}

				/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_clientId.ToString(), m_BuildingNumber, m_Message, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_clientId.ToString(), m_BuildingNumber, m_Message};
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
		public int clientId {
			set{ m_clientId = value;
			OnPropertyChanged("clientId"); }
			get { return m_clientId; }
		}

		public int ID {
			get { return m_ID; }
		}

		public string BuildingNumber {
			set{ m_BuildingNumber = value;
			OnPropertyChanged("BuildingNumber"); }
			get { return m_BuildingNumber; }
		}

		public string Message {
			set{ m_Message = value;
			OnPropertyChanged("Message"); }
			get { return m_Message; }
		}

}
}
