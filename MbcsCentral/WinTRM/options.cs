using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class options : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_options";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_AUTO_DELETE_AGE = "autoDeleteAge";
	public const string SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL = "messageCreationCheckInterval";
	public const string SQL_COL_RETRY_QUEUE_AT_STARTUP = "retryQueueAtStartup";
	public const string SQL_COL_AUTO_DELETE_MESSAGES = "autoDeleteMessages";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_COMPANY_NAME = "companyName";
	public const string SQL_COL_EMAIL_SIGNATURE = "emailSignature";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_AUTO_DELETE_AGE + "]," +
			"[" + SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL + "]," +
			"[" + SQL_COL_RETRY_QUEUE_AT_STARTUP + "]," +
			"[" + SQL_COL_AUTO_DELETE_MESSAGES + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_COMPANY_NAME + "]," +
			"[" + SQL_COL_EMAIL_SIGNATURE + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_AUTO_DELETE_AGE + "]," +
			"[" + SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL + "]," +
			"[" + SQL_COL_RETRY_QUEUE_AT_STARTUP + "]," +
			"[" + SQL_COL_AUTO_DELETE_MESSAGES + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_COMPANY_NAME + "]," +
			"[" + SQL_COL_EMAIL_SIGNATURE + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_AUTO_DELETE_AGE + "]," +
			"[" + SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL + "]," +
			"[" + SQL_COL_RETRY_QUEUE_AT_STARTUP + "]," +
			"[" + SQL_COL_AUTO_DELETE_MESSAGES + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_COMPANY_NAME + "]," +
			"[" + SQL_COL_EMAIL_SIGNATURE + "]" +
			") VALUES (?, ?, ?, ?, ?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_AUTO_DELETE_AGE + "]=?," +
			"[" + SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL + "]=?," +
			"[" + SQL_COL_RETRY_QUEUE_AT_STARTUP + "]=?," +
			"[" + SQL_COL_AUTO_DELETE_MESSAGES + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_COMPANY_NAME + "]='?'," +
			"[" + SQL_COL_EMAIL_SIGNATURE + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected int m_ID;
	protected int m_autoDeleteAge;
	protected int m_messageCreationCheckInterval;
	protected bool m_retryQueueAtStartup;
	protected bool m_autoDeleteMessages;
	protected TimeStamp m_SSMA_TimeStamp;
	protected string m_companyName;
	protected string m_emailSignature;
	protected bool m_isChanged;

	public options() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public options(int ID,int autoDeleteAge,int messageCreationCheckInterval,bool retryQueueAtStartup,bool autoDeleteMessages,TimeStamp SSMA_TimeStamp,string companyName,string emailSignature) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_ID = ID;
		m_autoDeleteAge = autoDeleteAge;
		m_messageCreationCheckInterval = messageCreationCheckInterval;
		m_retryQueueAtStartup = retryQueueAtStartup;
		m_autoDeleteMessages = autoDeleteMessages;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_companyName = companyName;
		m_emailSignature = emailSignature;
	}

	protected options fill(DataRow dr)
	{
		try {
			m_ID = (int) dr[SQL_COL_I_D];
			m_autoDeleteAge = (int) dr[SQL_COL_AUTO_DELETE_AGE];
			m_messageCreationCheckInterval = (int) dr[SQL_COL_MESSAGE_CREATION_CHECK_INTERVAL];
			m_retryQueueAtStartup = (bool) dr[SQL_COL_RETRY_QUEUE_AT_STARTUP];
			m_autoDeleteMessages = (bool) dr[SQL_COL_AUTO_DELETE_MESSAGES];
			m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_companyName = (string) (Convert.IsDBNull(dr[SQL_COL_COMPANY_NAME]) ? "" : dr[SQL_COL_COMPANY_NAME]);
			m_emailSignature = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL_SIGNATURE]) ? "" : dr[SQL_COL_EMAIL_SIGNATURE]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static options load( int m_ID)
	{
		options obj  = new options();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static options[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		options obj = new options();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new options();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (options[])list.ToArray(typeof(options));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_autoDeleteAge.ToString(), m_messageCreationCheckInterval.ToString(), m_retryQueueAtStartup.ToString(), m_autoDeleteMessages.ToString(), m_SSMA_TimeStamp.ToString(), m_companyName, m_emailSignature, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_autoDeleteAge.ToString(), m_messageCreationCheckInterval.ToString(), m_retryQueueAtStartup.ToString(), m_autoDeleteMessages.ToString(), m_SSMA_TimeStamp.ToString(), m_companyName, m_emailSignature};
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

	public int autoDeleteAge {
		set{ m_autoDeleteAge = value;
		OnPropertyChanged("autoDeleteAge"); }
		get { return m_autoDeleteAge; }
	}

	public int messageCreationCheckInterval {
		set{ m_messageCreationCheckInterval = value;
		OnPropertyChanged("messageCreationCheckInterval"); }
		get { return m_messageCreationCheckInterval; }
	}

	public bool retryQueueAtStartup {
		set{ m_retryQueueAtStartup = value;
		OnPropertyChanged("retryQueueAtStartup"); }
		get { return m_retryQueueAtStartup; }
	}

	public bool autoDeleteMessages {
		set{ m_autoDeleteMessages = value;
		OnPropertyChanged("autoDeleteMessages"); }
		get { return m_autoDeleteMessages; }
	}

	public TimeStamp SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string companyName {
		set{ m_companyName = value;
		OnPropertyChanged("companyName"); }
		get { return m_companyName; }
	}

	public string emailSignature {
		set{ m_emailSignature = value;
		OnPropertyChanged("emailSignature"); }
		get { return m_emailSignature; }
	}

}
}
