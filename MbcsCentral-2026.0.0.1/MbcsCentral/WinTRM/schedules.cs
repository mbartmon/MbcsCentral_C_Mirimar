using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class schedules : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_schedules";

	public const string SQL_COL_START_DATE = "start_date";
	public const string SQL_COL_END_DATE = "end_date";
	public const string SQL_COL_LAST_PROCESSED = "last_processed";
	public const string SQL_COL_SCHEDULE_ID = "schedule_id";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_CONTENT_ID = "content_id";
	public const string SQL_COL_NUMBER_TO_PROCESS = "number_to_process";
	public const string SQL_COL_PROCESS_COUNT = "process_count";
	public const string SQL_COL_FREQUENCY = "frequency";
	public const string SQL_COL_DAY_TO_PROCESS = "day_to_process";
	public const string SQL_COL_DAY_TO_PROCESS_2 = "day_to_process_2";
	public const string SQL_COL_ACTIVE = "active";
	public const string SQL_COL_NO_END_DATE = "no_end_date";
	public const string SQL_COL_USE_WEEKENDS = "useWeekends";
	public const string SQL_COL_USE_HOLIDAYS = "useHolidays";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_SCHEDULE_NAME = "schedule_name";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_LAST_PROCESSED + "]," +
			"[" + SQL_COL_SCHEDULE_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_ACTIVE + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_SCHEDULE_NAME + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_SCHEDULE_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_LAST_PROCESSED + "]," +
			"[" + SQL_COL_SCHEDULE_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_ACTIVE + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_SCHEDULE_NAME + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_START_DATE + "]," +
			"[" + SQL_COL_END_DATE + "]," +
			"[" + SQL_COL_LAST_PROCESSED + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]," +
			"[" + SQL_COL_PROCESS_COUNT + "]," +
			"[" + SQL_COL_FREQUENCY + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]," +
			"[" + SQL_COL_ACTIVE + "]," +
			"[" + SQL_COL_NO_END_DATE + "]," +
			"[" + SQL_COL_USE_WEEKENDS + "]," +
			"[" + SQL_COL_USE_HOLIDAYS + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_SCHEDULE_NAME + "]" +
			") VALUES ('?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_START_DATE + "]='?'," +
			"[" + SQL_COL_END_DATE + "]='?'," +
			"[" + SQL_COL_LAST_PROCESSED + "]='?'," +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_CONTENT_ID + "]=?," +
			"[" + SQL_COL_NUMBER_TO_PROCESS + "]=?," +
			"[" + SQL_COL_PROCESS_COUNT + "]=?," +
			"[" + SQL_COL_FREQUENCY + "]=?," +
			"[" + SQL_COL_DAY_TO_PROCESS + "]=?," +
			"[" + SQL_COL_DAY_TO_PROCESS_2 + "]=?," +
			"[" + SQL_COL_ACTIVE + "]=?," +
			"[" + SQL_COL_NO_END_DATE + "]=?," +
			"[" + SQL_COL_USE_WEEKENDS + "]=?," +
			"[" + SQL_COL_USE_HOLIDAYS + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_SCHEDULE_NAME + "]='?'" +
			" WHERE " + "[" + SQL_COL_SCHEDULE_ID + "] = ?;";

	protected DateTime m_start_date;
	protected DateTime m_end_date;
	protected DateTime m_last_processed;
	protected int m_schedule_id;
	protected int m_account_id;
	protected int m_content_id;
	protected int m_number_to_process;
	protected int m_process_count;
	protected int m_frequency;
	protected int m_day_to_process;
	protected int m_day_to_process_2;
	protected bool m_active;
	protected bool m_no_end_date;
	protected bool m_useWeekends;
	protected bool m_useHolidays;
	protected TimeStamp m_SSMA_TimeStamp;
	protected string m_schedule_name;
	protected bool m_isChanged;

	public schedules() : base(TABLE, SQL_COL_SCHEDULE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public schedules(DateTime start_date,DateTime end_date,DateTime last_processed,int schedule_id,int account_id,int content_id,int number_to_process,int process_count,int frequency,int day_to_process,int day_to_process_2,bool active,bool no_end_date,bool useWeekends,bool useHolidays,TimeStamp SSMA_TimeStamp,string schedule_name) : 
			base(TABLE, SQL_COL_SCHEDULE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_start_date = start_date;
		m_end_date = end_date;
		m_last_processed = last_processed;
		m_schedule_id = schedule_id;
		m_account_id = account_id;
		m_content_id = content_id;
		m_number_to_process = number_to_process;
		m_process_count = process_count;
		m_frequency = frequency;
		m_day_to_process = day_to_process;
		m_day_to_process_2 = day_to_process_2;
		m_active = active;
		m_no_end_date = no_end_date;
		m_useWeekends = useWeekends;
		m_useHolidays = useHolidays;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_schedule_name = schedule_name;
	}

	protected schedules fill(DataRow dr)
	{
		try {
			m_start_date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_START_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_START_DATE]);
			m_end_date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_END_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_END_DATE]);
			m_last_processed = (DateTime) (Convert.IsDBNull(dr[SQL_COL_LAST_PROCESSED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_PROCESSED]);
			m_schedule_id = (int) dr[SQL_COL_SCHEDULE_ID];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_content_id = (int) dr[SQL_COL_CONTENT_ID];
			m_number_to_process = (int) dr[SQL_COL_NUMBER_TO_PROCESS];
			m_process_count = (int) dr[SQL_COL_PROCESS_COUNT];
			m_frequency = (int) dr[SQL_COL_FREQUENCY];
			m_day_to_process = (int) dr[SQL_COL_DAY_TO_PROCESS];
			m_day_to_process_2 = (int) dr[SQL_COL_DAY_TO_PROCESS_2];
			m_active = (bool) dr[SQL_COL_ACTIVE];
			m_no_end_date = (bool) dr[SQL_COL_NO_END_DATE];
			m_useWeekends = (bool) dr[SQL_COL_USE_WEEKENDS];
			m_useHolidays = (bool) dr[SQL_COL_USE_HOLIDAYS];
			m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_schedule_name = (string) (Convert.IsDBNull(dr[SQL_COL_SCHEDULE_NAME]) ? "" : dr[SQL_COL_SCHEDULE_NAME]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static schedules load( int m_schedule_id)
	{
		schedules obj  = new schedules();
		string[] opt = {m_schedule_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static schedules[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		schedules obj = new schedules();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new schedules();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (schedules[])list.ToArray(typeof(schedules));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_schedule_id > 0 ) {
			string[] values = {m_start_date.ToString(), m_end_date.ToString(), m_last_processed.ToString(), m_account_id.ToString(), m_content_id.ToString(), m_number_to_process.ToString(), m_process_count.ToString(), m_frequency.ToString(), m_day_to_process.ToString(), m_day_to_process_2.ToString(), m_active.ToString(), m_no_end_date.ToString(), m_useWeekends.ToString(), m_useHolidays.ToString(), m_SSMA_TimeStamp.ToString(), m_schedule_name, m_schedule_id.ToString()};
			if (this.updateRecord(m_schedule_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_start_date.ToString(), m_end_date.ToString(), m_last_processed.ToString(), m_account_id.ToString(), m_content_id.ToString(), m_number_to_process.ToString(), m_process_count.ToString(), m_frequency.ToString(), m_day_to_process.ToString(), m_day_to_process_2.ToString(), m_active.ToString(), m_no_end_date.ToString(), m_useWeekends.ToString(), m_useHolidays.ToString(), m_SSMA_TimeStamp.ToString(), m_schedule_name};
			m_schedule_id = this.insertRecord(values);
			if (m_schedule_id >= 0) {
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

	public DateTime last_processed {
		set{ m_last_processed = value;
		OnPropertyChanged("last_processed"); }
		get { return m_last_processed; }
	}

	public int schedule_id {
		get { return m_schedule_id; }
	}

	public int account_id {
		set{ m_account_id = value;
		OnPropertyChanged("account_id"); }
		get { return m_account_id; }
	}

	public int content_id {
		set{ m_content_id = value;
		OnPropertyChanged("content_id"); }
		get { return m_content_id; }
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

	public int frequency {
		set{ m_frequency = value;
		OnPropertyChanged("frequency"); }
		get { return m_frequency; }
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

	public bool active {
		set{ m_active = value;
		OnPropertyChanged("active"); }
		get { return m_active; }
	}

	public bool no_end_date {
		set{ m_no_end_date = value;
		OnPropertyChanged("no_end_date"); }
		get { return m_no_end_date; }
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

	public TimeStamp SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string schedule_name {
		set{ m_schedule_name = value;
		OnPropertyChanged("schedule_name"); }
		get { return m_schedule_name; }
	}

}
}
