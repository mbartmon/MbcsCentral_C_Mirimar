using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class JobMaster : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_JobMaster";

	public const string SQL_COL_DELIVERY_TIME = "deliveryTime";
	public const string SQL_COL_CREATED = "created";
	public const string SQL_COL_FINISHED = "finished";
	public const string SQL_COL_STATUS = "status";
	public const string SQL_COL_JOB_ID = "jobId";
	public const string SQL_COL_ACCOUNT_ID = "accountId";
	public const string SQL_COL_CONTENT_ID = "contentId";
	public const string SQL_COL_MESSAGE_COUNT = "messageCount";
	public const string SQL_COL_SMTP_ACCT_ID = "smtpAcctId";
	public const string SQL_COL_MSG_SCHED_ID = "msgSchedId";
	public const string SQL_COL_JOB_NAME = "jobName";
	public const string SQL_COL_LIST_IDS = "listIds";
	public const string SQL_COL_LAST_ERROR = "lastError";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_DELIVERY_TIME + "]," +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_FINISHED + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_JOB_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_MESSAGE_COUNT + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_MSG_SCHED_ID + "]," +
			"[" + SQL_COL_JOB_NAME + "]," +
			"[" + SQL_COL_LIST_IDS + "]," +
			"[" + SQL_COL_LAST_ERROR + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_JOB_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_DELIVERY_TIME + "]," +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_FINISHED + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_JOB_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_MESSAGE_COUNT + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_MSG_SCHED_ID + "]," +
			"[" + SQL_COL_JOB_NAME + "]," +
			"[" + SQL_COL_LIST_IDS + "]," +
			"[" + SQL_COL_LAST_ERROR + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_DELIVERY_TIME + "]," +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_FINISHED + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_MESSAGE_COUNT + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_MSG_SCHED_ID + "]," +
			"[" + SQL_COL_JOB_NAME + "]," +
			"[" + SQL_COL_LIST_IDS + "]," +
			"[" + SQL_COL_LAST_ERROR + "]" +
			") VALUES ('?', '?', '?', ?, ?, ?, ?, ?, ?, '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_DELIVERY_TIME + "]='?'," +
			"[" + SQL_COL_CREATED + "]='?'," +
			"[" + SQL_COL_FINISHED + "]='?'," +
			"[" + SQL_COL_STATUS + "]=?," +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_CONTENT_ID + "]=?," +
			"[" + SQL_COL_MESSAGE_COUNT + "]=?," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]=?," +
			"[" + SQL_COL_MSG_SCHED_ID + "]=?," +
			"[" + SQL_COL_JOB_NAME + "]='?'," +
			"[" + SQL_COL_LIST_IDS + "]='?'," +
			"[" + SQL_COL_LAST_ERROR + "]='?'" +
			" WHERE " + "[" + SQL_COL_JOB_ID + "] = ?;";

	protected DateTime m_deliveryTime;
	protected DateTime m_created;
	protected DateTime m_finished;
	protected short m_status;
	protected int m_jobId;
	protected int m_accountId;
	protected int m_contentId;
	protected int m_messageCount;
	protected int m_smtpAcctId;
	protected int m_msgSchedId;
	protected string m_jobName;
	protected string m_listIds;
	protected string m_lastError;
	protected bool m_isChanged;

	public JobMaster() : base(TABLE, SQL_COL_JOB_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public JobMaster(DateTime deliveryTime,DateTime created,DateTime finished,short status,int jobId,int accountId,int contentId,int messageCount,int smtpAcctId,int msgSchedId,string jobName,string listIds,string lastError) : 
			base(TABLE, SQL_COL_JOB_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_deliveryTime = deliveryTime;
		m_created = created;
		m_finished = finished;
		m_status = status;
		m_jobId = jobId;
		m_accountId = accountId;
		m_contentId = contentId;
		m_messageCount = messageCount;
		m_smtpAcctId = smtpAcctId;
		m_msgSchedId = msgSchedId;
		m_jobName = jobName;
		m_listIds = listIds;
		m_lastError = lastError;
	}

	protected JobMaster fill(DataRow dr)
	{
		try {
			m_deliveryTime = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DELIVERY_TIME]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DELIVERY_TIME]);
			m_created = (DateTime) (Convert.IsDBNull(dr[SQL_COL_CREATED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_CREATED]);
			m_finished = (DateTime) (Convert.IsDBNull(dr[SQL_COL_FINISHED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_FINISHED]);
			m_status = (short) dr[SQL_COL_STATUS];
			m_jobId = (int) dr[SQL_COL_JOB_ID];
			m_accountId = (int) dr[SQL_COL_ACCOUNT_ID];
			m_contentId = (int) dr[SQL_COL_CONTENT_ID];
			m_messageCount = (int) dr[SQL_COL_MESSAGE_COUNT];
			m_smtpAcctId = (int) dr[SQL_COL_SMTP_ACCT_ID];
			m_msgSchedId = (int) dr[SQL_COL_MSG_SCHED_ID];
			m_jobName = (string) (Convert.IsDBNull(dr[SQL_COL_JOB_NAME]) ? "" : dr[SQL_COL_JOB_NAME]);
			m_listIds = (string) (Convert.IsDBNull(dr[SQL_COL_LIST_IDS]) ? "" : dr[SQL_COL_LIST_IDS]);
			m_lastError = (string) (Convert.IsDBNull(dr[SQL_COL_LAST_ERROR]) ? "" : dr[SQL_COL_LAST_ERROR]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static JobMaster load( int m_jobId)
	{
		JobMaster obj  = new JobMaster();
		string[] opt = {m_jobId.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static JobMaster[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		JobMaster obj = new JobMaster();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new JobMaster();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (JobMaster[])list.ToArray(typeof(JobMaster));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_jobId > 0 ) {
			string[] values = {m_deliveryTime.ToString(), m_created.ToString(), m_finished.ToString(), m_status.ToString(), m_accountId.ToString(), m_contentId.ToString(), m_messageCount.ToString(), m_smtpAcctId.ToString(), m_msgSchedId.ToString(), m_jobName, m_listIds, m_lastError, m_jobId.ToString()};
			if (this.updateRecord(m_jobId.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_deliveryTime.ToString(), m_created.ToString(), m_finished.ToString(), m_status.ToString(), m_accountId.ToString(), m_contentId.ToString(), m_messageCount.ToString(), m_smtpAcctId.ToString(), m_msgSchedId.ToString(), m_jobName, m_listIds, m_lastError};
			m_jobId = this.insertRecord(values);
			if (m_jobId >= 0) {
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
	public DateTime deliveryTime {
		set{ m_deliveryTime = value;
		OnPropertyChanged("deliveryTime"); }
		get { return m_deliveryTime; }
	}

	public DateTime created {
		set{ m_created = value;
		OnPropertyChanged("created"); }
		get { return m_created; }
	}

	public DateTime finished {
		set{ m_finished = value;
		OnPropertyChanged("finished"); }
		get { return m_finished; }
	}

	public short status {
		set{ m_status = value;
		OnPropertyChanged("status"); }
		get { return m_status; }
	}

	public int jobId {
		get { return m_jobId; }
	}

	public int accountId {
		set{ m_accountId = value;
		OnPropertyChanged("accountId"); }
		get { return m_accountId; }
	}

	public int contentId {
		set{ m_contentId = value;
		OnPropertyChanged("contentId"); }
		get { return m_contentId; }
	}

	public int messageCount {
		set{ m_messageCount = value;
		OnPropertyChanged("messageCount"); }
		get { return m_messageCount; }
	}

	public int smtpAcctId {
		set{ m_smtpAcctId = value;
		OnPropertyChanged("smtpAcctId"); }
		get { return m_smtpAcctId; }
	}

	public int msgSchedId {
		set{ m_msgSchedId = value;
		OnPropertyChanged("msgSchedId"); }
		get { return m_msgSchedId; }
	}

	public string jobName {
		set{ m_jobName = value;
		OnPropertyChanged("jobName"); }
		get { return m_jobName; }
	}

	public string listIds {
		set{ m_listIds = value;
		OnPropertyChanged("listIds"); }
		get { return m_listIds; }
	}

	public string lastError {
		set{ m_lastError = value;
		OnPropertyChanged("lastError"); }
		get { return m_lastError; }
	}

}
}
