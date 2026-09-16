using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class MessageSchedule : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_MessageSchedule";

	public const string SQL_COL_CREATED = "created";
	public const string SQL_COL_LAST_SENT = "last_sent";
	public const string SQL_COL_START_DATE = "start_date";
	public const string SQL_COL_END_DATE = "end_date";
	public const string SQL_COL_MSG_SCHED_ID = "msg_sched_id";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_SCHEDULE_ID = "schedule_id";
	public const string SQL_COL_CONTENT_ID = "content_id";
	public const string SQL_COL_STATUS = "status";
	public const string SQL_COL_NUMBER_TO_PROCESS = "number_to_process";
	public const string SQL_COL_PROCESS_COUNT = "process_count";
	public const string SQL_COL_DAY_TO_PROCESS = "day_to_process";
	public const string SQL_COL_DAY_TO_PROCESS_2 = "day_to_process_2";
	public const string SQL_COL_FREQUENCY = "frequency";
	public const string SQL_COL_EXCEPTION_OVERRIDE = "exception_override";
	public const string SQL_COL_SMTP_ACCT_ID = "smtp_acct_id";
	public const string SQL_COL_NO_END_DATE = "no_end_date";
	public const string SQL_COL_RETURN_RECEIPT = "returnReceipt";
	public const string SQL_COL_USE_WEEKENDS = "useWeekends";
	public const string SQL_COL_USE_HOLIDAYS = "useHolidays";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_DESCRIPTION = "description";
	public const string SQL_COL_RECIPIENTS = "recipients";
	public const string SQL_COL_FROM = "from";
	public const string SQL_COL_SUBJECT = "subject";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_LAST_SENT + "]," +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_MSG_SCHED_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_SCHEDULE_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_EXCEPTION_OVERRIDE + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_RETURN_RECEIPT + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_RECIPIENTS + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_MSG_SCHED_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_LAST_SENT + "]," +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_MSG_SCHED_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_SCHEDULE_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_EXCEPTION_OVERRIDE + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_RETURN_RECEIPT + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_RECIPIENTS + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CREATED + "]," +
			"[" + SQL_COL_LAST_SENT + "]," +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_SCHEDULE_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_STATUS + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_EXCEPTION_OVERRIDE + "]," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_RETURN_RECEIPT + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_RECIPIENTS + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_SUBJECT + "]" +
			") VALUES ('?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CREATED + "]='?'," +
			"[" + SQL_COL_LAST_SENT + "]='?'," +
			"[" + SQL_COL_START_DATE + "]='?'," +
			"[" + SQL_COL_END_DATE + "]='?'," +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_SCHEDULE_ID + "]=?," +
			"[" + SQL_COL_CONTENT_ID + "]=?," +
			"[" + SQL_COL_STATUS + "]=?," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]=?," +
			"[" + SQL_COL_PROCESS_COUNT + "]=?," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]=?," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]=?," +
			"[" + SQL_COL_FREQUENCY + "]=?," +
			"[" + SQL_COL_EXCEPTION_OVERRIDE + "]=?," +
			"[" + SQL_COL_SMTP_ACCT_ID + "]=?," +
			"[" + SQL_COL_NO_END_DATE + "]=?," +
			"[" + SQL_COL_RETURN_RECEIPT + "]=?," +
			"[" + SQL_COL_USE_WEEKENDS + "]=?," +
			"[" + SQL_COL_USE_HOLIDAYS + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_DESCRIPTION + "]='?'," +
			"[" + SQL_COL_RECIPIENTS + "]='?'," +
			"[" + SQL_COL_FROM + "]='?'," +
			"[" + SQL_COL_SUBJECT + "]='?'" +
			" WHERE " + "[" + SQL_COL_MSG_SCHED_ID + "] = ?;";

	protected DateTime m_created;
	protected DateTime m_last_sent;
	protected DateTime m_start_date;
	protected DateTime m_end_date;
	protected int m_msg_sched_id;
	protected int m_account_id;
	protected int m_schedule_id;
	protected int m_content_id;
	protected int m_status;
	protected int m_number_to_process;
	protected int m_process_count;
	protected int m_day_to_process;
	protected int m_day_to_process_2;
	protected int m_frequency;
	protected int m_exception_override;
	protected int m_smtp_acct_id;
	protected bool m_no_end_date;
	protected bool m_returnReceipt;
	protected bool m_useWeekends;
	protected bool m_useHolidays;
	protected DateTime m_SSMA_TimeStamp;
	protected string m_description;
	protected string m_recipients;
	protected string m_from;
	protected string m_subject;
	protected bool m_isChanged;

	public MessageSchedule() : base(TABLE, SQL_COL_MSG_SCHED_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
	{
	}

	public MessageSchedule(DateTime created,DateTime last_sent,DateTime start_date,DateTime end_date,int msg_sched_id,int account_id,int schedule_id,int content_id,int status,int number_to_process,int process_count,int day_to_process,int day_to_process_2,int frequency,int exception_override,int smtp_acct_id,bool no_end_date,bool returnReceipt,bool useWeekends,bool useHolidays,DateTime SSMA_TimeStamp,string description,string recipients,string from,string subject) : 
			base(TABLE, SQL_COL_MSG_SCHED_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
	{
		m_created = created;
		m_last_sent = last_sent;
		m_start_date = start_date;
		m_end_date = end_date;
		m_msg_sched_id = msg_sched_id;
		m_account_id = account_id;
		m_schedule_id = schedule_id;
		m_content_id = content_id;
		m_status = status;
		m_number_to_process = number_to_process;
		m_process_count = process_count;
		m_day_to_process = day_to_process;
		m_day_to_process_2 = day_to_process_2;
		m_frequency = frequency;
		m_exception_override = exception_override;
		m_smtp_acct_id = smtp_acct_id;
		m_no_end_date = no_end_date;
		m_returnReceipt = returnReceipt;
		m_useWeekends = useWeekends;
		m_useHolidays = useHolidays;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_description = description;
		m_recipients = recipients;
		m_from = from;
		m_subject = subject;
	}

	protected MessageSchedule fill(DataRow dr)
	{
		try {
			m_created = (DateTime) (Convert.IsDBNull(dr[SQL_COL_CREATED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_CREATED]);
			m_last_sent = (DateTime) (Convert.IsDBNull(dr[SQL_COL_LAST_SENT]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_SENT]);
			m_start_date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_START_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_START_DATE]);
			m_end_date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_END_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_END_DATE]);
			m_msg_sched_id = (int) dr[SQL_COL_MSG_SCHED_ID];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_schedule_id = (int) dr[SQL_COL_SCHEDULE_ID];
			m_content_id = (int) dr[SQL_COL_CONTENT_ID];
			m_status = (int) dr[SQL_COL_STATUS];
			m_number_to_process = (int) dr[SQL_COL_NUMBER_TO_PROCESS];
			m_process_count = (int) dr[SQL_COL_PROCESS_COUNT];
			m_day_to_process = (int) dr[SQL_COL_DAY_TO_PROCESS];
			m_day_to_process_2 = (int) dr[SQL_COL_DAY_TO_PROCESS_2];
			m_frequency = (int) dr[SQL_COL_FREQUENCY];
			m_exception_override = (int) dr[SQL_COL_EXCEPTION_OVERRIDE];
			m_smtp_acct_id = (int) dr[SQL_COL_SMTP_ACCT_ID];
			m_no_end_date = (bool) dr[SQL_COL_NO_END_DATE];
			m_returnReceipt = (bool) dr[SQL_COL_RETURN_RECEIPT];
			m_useWeekends = (bool) dr[SQL_COL_USE_WEEKENDS];
			m_useHolidays = (bool) dr[SQL_COL_USE_HOLIDAYS];
			m_SSMA_TimeStamp = (DateTime) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			m_recipients = (string) (Convert.IsDBNull(dr[SQL_COL_RECIPIENTS]) ? "" : dr[SQL_COL_RECIPIENTS]);
			m_from = (string) (Convert.IsDBNull(dr[SQL_COL_FROM]) ? "" : dr[SQL_COL_FROM]);
			m_subject = (string) (Convert.IsDBNull(dr[SQL_COL_SUBJECT]) ? "" : dr[SQL_COL_SUBJECT]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static MessageSchedule load( int m_msg_sched_id)
	{
		MessageSchedule obj  = new MessageSchedule();
		string[] opt = {m_msg_sched_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static MessageSchedule[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		MessageSchedule obj = new MessageSchedule();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new MessageSchedule();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (MessageSchedule[])list.ToArray(typeof(MessageSchedule));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_msg_sched_id > 0 ) {
			string[] values = {m_created.ToString(), m_last_sent.ToString(), m_start_date.ToString(), m_end_date.ToString(), m_account_id.ToString(), m_schedule_id.ToString(), m_content_id.ToString(), m_status.ToString(), m_number_to_process.ToString(), m_process_count.ToString(), m_day_to_process.ToString(), m_day_to_process_2.ToString(), m_frequency.ToString(), m_exception_override.ToString(), m_smtp_acct_id.ToString(), m_no_end_date.ToString(), m_returnReceipt.ToString(), m_useWeekends.ToString(), m_useHolidays.ToString(), m_SSMA_TimeStamp.ToString(), m_description, m_recipients, m_from, m_subject, m_msg_sched_id.ToString()};
			if (this.updateRecord(m_msg_sched_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_created.ToString(), m_last_sent.ToString(), m_start_date.ToString(), m_end_date.ToString(), m_account_id.ToString(), m_schedule_id.ToString(), m_content_id.ToString(), m_status.ToString(), m_number_to_process.ToString(), m_process_count.ToString(), m_day_to_process.ToString(), m_day_to_process_2.ToString(), m_frequency.ToString(), m_exception_override.ToString(), m_smtp_acct_id.ToString(), m_no_end_date.ToString(), m_returnReceipt.ToString(), m_useWeekends.ToString(), m_useHolidays.ToString(), m_SSMA_TimeStamp.ToString(), m_description, m_recipients, m_from, m_subject};
			m_msg_sched_id = this.insertRecord(values);
			if (m_msg_sched_id >= 0) {
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
	public DateTime created {
		set{ m_created = value;
		OnPropertyChanged("created"); }
		get { return m_created; }
	}

	public DateTime last_sent {
		set{ m_last_sent = value;
		OnPropertyChanged("last_sent"); }
		get { return m_last_sent; }
	}

	public DateTime start_date {
		set{ m_start_date = value;
		OnPropertyChanged("start_date"); }
		get { return m_start_date; }
	}

	public DateTime end_date {
		set{ m_end_date = value;
		OnPropertyChanged("end_date"); }
		get { return m_end_date; }
	}

	public int msg_sched_id {
		get { return m_msg_sched_id; }
	}

	public int account_id {
		set{ m_account_id = value;
		OnPropertyChanged("account_id"); }
		get { return m_account_id; }
	}

	public int schedule_id {
		set{ m_schedule_id = value;
		OnPropertyChanged("schedule_id"); }
		get { return m_schedule_id; }
	}

	public int content_id {
		set{ m_content_id = value;
		OnPropertyChanged("content_id"); }
		get { return m_content_id; }
	}

	public int status {
		set{ m_status = value;
		OnPropertyChanged("status"); }
		get { return m_status; }
	}

	public int number_to_process {
		set{ m_number_to_process = value;
		OnPropertyChanged("number_to_process"); }
		get { return m_number_to_process; }
	}

	public int process_count {
		set{ m_process_count = value;
		OnPropertyChanged("process_count"); }
		get { return m_process_count; }
	}

	public int day_to_process {
		set{ m_day_to_process = value;
		OnPropertyChanged("day_to_process"); }
		get { return m_day_to_process; }
	}

	public int day_to_process_2 {
		set{ m_day_to_process_2 = value;
		OnPropertyChanged("day_to_process_2"); }
		get { return m_day_to_process_2; }
	}

	public int frequency {
		set{ m_frequency = value;
		OnPropertyChanged("frequency"); }
		get { return m_frequency; }
	}

	public int exception_override {
		set{ m_exception_override = value;
		OnPropertyChanged("exception_override"); }
		get { return m_exception_override; }
	}

	public int smtp_acct_id {
		set{ m_smtp_acct_id = value;
		OnPropertyChanged("smtp_acct_id"); }
		get { return m_smtp_acct_id; }
	}

	public bool no_end_date {
		set{ m_no_end_date = value;
		OnPropertyChanged("no_end_date"); }
		get { return m_no_end_date; }
	}

	public bool returnReceipt {
		set{ m_returnReceipt = value;
		OnPropertyChanged("returnReceipt"); }
		get { return m_returnReceipt; }
	}

	public bool useWeekends {
		set{ m_useWeekends = value;
		OnPropertyChanged("useWeekends"); }
		get { return m_useWeekends; }
	}

	public bool useHolidays {
		set{ m_useHolidays = value;
		OnPropertyChanged("useHolidays"); }
		get { return m_useHolidays; }
	}

	public DateTime SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string description {
		set{ m_description = value;
		OnPropertyChanged("description"); }
		get { return m_description; }
	}

	public string recipients {
		set{ m_recipients = value;
		OnPropertyChanged("recipients"); }
		get { return m_recipients; }
	}

	public string from {
		set{ m_from = value;
		OnPropertyChanged("from"); }
		get { return m_from; }
	}

	public string subject {
		set{ m_subject = value;
		OnPropertyChanged("subject"); }
		get { return m_subject; }
	}

}
}
