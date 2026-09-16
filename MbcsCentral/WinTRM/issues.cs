using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class issues : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_issues";

	public const string SQL_COL_TIMESTAMP = "timestamp";
	public const string SQL_COL_DUE_DATE = "due_date";
	public const string SQL_COL_LAST_UPDATE = "last_update";
	public const string SQL_COL_REPORTED = "reported";
	public const string SQL_COL_ASSIGNED = "assigned";
	public const string SQL_COL_PRIORITY = "priority";
	public const string SQL_COL_STATUS = "status";
	public const string SQL_COL_ALERT_MODE = "alert_mode";
	public const string SQL_COL_ISSUE_ID = "issue_id";
	public const string SQL_COL_SEQUENCE_NUMBER = "sequence_number";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_PERSON_RESPONSIBLE = "person_responsible";
	public const string SQL_COL_VENDOR_ID = "vendor_id";
	public const string SQL_COL_LOCATION_ID = "location_id";
	public const string SQL_COL_NOTIFY_RESPONSIBLE = "notify_responsible";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_NOTE = "note";
	public const string SQL_COL_RECIPIENT_IDS = "recipient_ids";
	public const string SQL_COL_SUBJECT = "subject";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_DUE_DATE + "]," +
			"[" + SQL_COL_LAST_UPDATE + "]," +
			"[" + SQL_COL_REPORTED + "]," +
			"[" + SQL_COL_ASSIGNED + "]," +
			"[" + SQL_COL_PRIORITY + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_ALERT_MODE + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_PERSON_RESPONSIBLE + "]," +
			"[" + SQL_COL_VENDOR_ID + "]," +
			"[" + SQL_COL_LOCATION_ID + "]," +
			"[" + SQL_COL_NOTIFY_RESPONSIBLE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]," +
			"[" + SQL_COL_RECIPIENT_IDS + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_ISSUE_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_DUE_DATE + "]," +
			"[" + SQL_COL_LAST_UPDATE + "]," +
			"[" + SQL_COL_REPORTED + "]," +
			"[" + SQL_COL_ASSIGNED + "]," +
			"[" + SQL_COL_PRIORITY + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_ALERT_MODE + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_PERSON_RESPONSIBLE + "]," +
			"[" + SQL_COL_VENDOR_ID + "]," +
			"[" + SQL_COL_LOCATION_ID + "]," +
			"[" + SQL_COL_NOTIFY_RESPONSIBLE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]," +
			"[" + SQL_COL_RECIPIENT_IDS + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_DUE_DATE + "]," +
			"[" + SQL_COL_LAST_UPDATE + "]," +
			"[" + SQL_COL_REPORTED + "]," +
			"[" + SQL_COL_ASSIGNED + "]," +
			"[" + SQL_COL_PRIORITY + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_ALERT_MODE + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_PERSON_RESPONSIBLE + "]," +
			"[" + SQL_COL_VENDOR_ID + "]," +
			"[" + SQL_COL_LOCATION_ID + "]," +
			"[" + SQL_COL_NOTIFY_RESPONSIBLE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]," +
			"[" + SQL_COL_RECIPIENT_IDS + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			") VALUES ('?', '?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_TIMESTAMP + "]='?'," +
			"[" + SQL_COL_DUE_DATE + "]='?'," +
			"[" + SQL_COL_LAST_UPDATE + "]='?'," +
			"[" + SQL_COL_REPORTED + "]='?'," +
			"[" + SQL_COL_ASSIGNED + "]='?'," +
			"[" + SQL_COL_PRIORITY + "]=?," +
			"[" + SQL_COL_STATUS + "]=?," +
			"[" + SQL_COL_ALERT_MODE + "]=?," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_PERSON_RESPONSIBLE + "]=?," +
			"[" + SQL_COL_VENDOR_ID + "]=?," +
			"[" + SQL_COL_LOCATION_ID + "]=?," +
			"[" + SQL_COL_NOTIFY_RESPONSIBLE + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_NOTE + "]='?'," +
			"[" + SQL_COL_RECIPIENT_IDS + "]='?'," +
			"[" + SQL_COL_SUBJECT + "]='?'" +
			" WHERE " + "[" + SQL_COL_ISSUE_ID + "] = ?;";

	protected DateTime m_timestamp;
	protected DateTime m_due_date;
	protected DateTime m_last_update;
	protected DateTime m_reported;
	protected DateTime m_assigned;
	protected short m_priority;
	protected short m_status;
	protected short m_alert_mode;
	protected int m_issue_id;
	protected int m_sequence_number;
	protected int m_account_id;
	protected int m_person_responsible;
	protected int m_vendor_id;
	protected int m_location_id;
	protected bool m_notify_responsible;
	protected byte[] m_SSMA_TimeStamp;
	protected string m_note;
	protected string m_recipient_ids;
	protected string m_subject;
	protected bool m_isChanged;

	public issues() : base(TABLE, SQL_COL_ISSUE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public issues(DateTime timestamp,DateTime due_date,DateTime last_update,DateTime reported,DateTime assigned,short priority,short status,short alert_mode,int issue_id,int sequence_number,int account_id,int person_responsible,int vendor_id,int location_id,bool notify_responsible,byte[] SSMA_TimeStamp,string note,string recipient_ids,string subject) : 
			base(TABLE, SQL_COL_ISSUE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_timestamp = timestamp;
		m_due_date = due_date;
		m_last_update = last_update;
		m_reported = reported;
		m_assigned = assigned;
		m_priority = priority;
		m_status = status;
		m_alert_mode = alert_mode;
		m_issue_id = issue_id;
		m_sequence_number = sequence_number;
		m_account_id = account_id;
		m_person_responsible = person_responsible;
		m_vendor_id = vendor_id;
		m_location_id = location_id;
		m_notify_responsible = notify_responsible;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_note = note;
		m_recipient_ids = recipient_ids;
		m_subject = subject;
	}

	protected issues fill(DataRow dr)
	{
		try {
			m_timestamp = (DateTime) (Convert.IsDBNull(dr[SQL_COL_TIMESTAMP]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_TIMESTAMP]);
			m_due_date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DUE_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DUE_DATE]);
			m_last_update = (DateTime) (Convert.IsDBNull(dr[SQL_COL_LAST_UPDATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_UPDATE]);
			m_reported = (DateTime) (Convert.IsDBNull(dr[SQL_COL_REPORTED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_REPORTED]);
			m_assigned = (DateTime) (Convert.IsDBNull(dr[SQL_COL_ASSIGNED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_ASSIGNED]);
			m_priority = (short) dr[SQL_COL_PRIORITY];
			m_status = (short) dr[SQL_COL_STATUS];
			m_alert_mode = (short) dr[SQL_COL_ALERT_MODE];
			m_issue_id = (int) dr[SQL_COL_ISSUE_ID];
			m_sequence_number = (int) dr[SQL_COL_SEQUENCE_NUMBER];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_person_responsible = (int) dr[SQL_COL_PERSON_RESPONSIBLE];
			m_vendor_id = (int) dr[SQL_COL_VENDOR_ID];
			m_location_id = (int) dr[SQL_COL_LOCATION_ID];
			m_notify_responsible = (bool) dr[SQL_COL_NOTIFY_RESPONSIBLE];
			m_SSMA_TimeStamp = (byte[]) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_note = (string) (Convert.IsDBNull(dr[SQL_COL_NOTE]) ? "" : dr[SQL_COL_NOTE]);
			m_recipient_ids = (string) (Convert.IsDBNull(dr[SQL_COL_RECIPIENT_IDS]) ? "" : dr[SQL_COL_RECIPIENT_IDS]);
			m_subject = (string) (Convert.IsDBNull(dr[SQL_COL_SUBJECT]) ? "" : dr[SQL_COL_SUBJECT]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static issues load( int m_issue_id)
	{
		issues obj  = new issues();
		string[] opt = {m_issue_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static issues[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		issues obj = new issues();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new issues();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (issues[])list.ToArray(typeof(issues));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_issue_id > 0 ) {
			string[] values = {m_timestamp.ToString(), m_due_date.ToString(), m_last_update.ToString(), m_reported.ToString(), m_assigned.ToString(), m_priority.ToString(), m_status.ToString(), m_alert_mode.ToString(), m_sequence_number.ToString(), m_account_id.ToString(), m_person_responsible.ToString(), m_vendor_id.ToString(), m_location_id.ToString(), m_notify_responsible.ToString(), m_SSMA_TimeStamp.ToString(), m_note, m_recipient_ids, m_subject, m_issue_id.ToString()};
			if (this.updateRecord(m_issue_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_timestamp.ToString(), m_due_date.ToString(), m_last_update.ToString(), m_reported.ToString(), m_assigned.ToString(), m_priority.ToString(), m_status.ToString(), m_alert_mode.ToString(), m_sequence_number.ToString(), m_account_id.ToString(), m_person_responsible.ToString(), m_vendor_id.ToString(), m_location_id.ToString(), m_notify_responsible.ToString(), m_SSMA_TimeStamp.ToString(), m_note, m_recipient_ids, m_subject};
			m_issue_id = this.insertRecord(values);
			if (m_issue_id >= 0) {
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
	public DateTime timestamp {
		set{ m_timestamp = value;
		OnPropertyChanged("timestamp"); }
		get { return m_timestamp; }
	}

	public DateTime due_date {
		set{ m_due_date = value;
		OnPropertyChanged("due_date"); }
		get { return m_due_date; }
	}

	public DateTime last_update {
		set{ m_last_update = value;
		OnPropertyChanged("last_update"); }
		get { return m_last_update; }
	}

	public DateTime reported {
		set{ m_reported = value;
		OnPropertyChanged("reported"); }
		get { return m_reported; }
	}

	public DateTime assigned {
		set{ m_assigned = value;
		OnPropertyChanged("assigned"); }
		get { return m_assigned; }
	}

	public short priority {
		set{ m_priority = value;
		OnPropertyChanged("priority"); }
		get { return m_priority; }
	}

	public short status {
		set{ m_status = value;
		OnPropertyChanged("status"); }
		get { return m_status; }
	}

	public short alert_mode {
		set{ m_alert_mode = value;
		OnPropertyChanged("alert_mode"); }
		get { return m_alert_mode; }
	}

	public int issue_id {
		get { return m_issue_id; }
	}

	public int sequence_number {
		set{ m_sequence_number = value;
		OnPropertyChanged("sequence_number"); }
		get { return m_sequence_number; }
	}

	public int account_id {
		set{ m_account_id = value;
		OnPropertyChanged("account_id"); }
		get { return m_account_id; }
	}

	public int person_responsible {
		set{ m_person_responsible = value;
		OnPropertyChanged("person_responsible"); }
		get { return m_person_responsible; }
	}

	public int vendor_id {
		set{ m_vendor_id = value;
		OnPropertyChanged("vendor_id"); }
		get { return m_vendor_id; }
	}

	public int location_id {
		set{ m_location_id = value;
		OnPropertyChanged("location_id"); }
		get { return m_location_id; }
	}

	public bool notify_responsible {
		set{ m_notify_responsible = value;
		OnPropertyChanged("notify_responsible"); }
		get { return m_notify_responsible; }
	}

	public byte[] SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string note {
		set{ m_note = value;
		OnPropertyChanged("note"); }
		get { return m_note; }
	}

	public string recipient_ids {
		set{ m_recipient_ids = value;
		OnPropertyChanged("recipient_ids"); }
		get { return m_recipient_ids; }
	}

	public string subject {
		set{ m_subject = value;
		OnPropertyChanged("subject"); }
		get { return m_subject; }
	}

}
}
