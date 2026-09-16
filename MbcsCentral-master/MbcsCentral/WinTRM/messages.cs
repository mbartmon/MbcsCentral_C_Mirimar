using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
    public class messages : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "tr_messages";

	    public const string SQL_COL_DATE_CREATED = "date_created";
	    public const string SQL_COL_DATE_PROCESSED = "date_processed";
	    public const string SQL_COL_DATE_CONFIRMED = "date_confirmed";
	    public const string SQL_COL_MESSAGE_ID = "message_id";
	    public const string SQL_COL_ISSUE_ID = "issue_id";
	    public const string SQL_COL_ACCOUNT_ID = "account_id";
	    public const string SQL_COL_DIRECTION = "direction";
	    public const string SQL_COL_REQUEST_CONFIRM = "request_confirm";
	    public const string SQL_COL_CONFIRM_RECEIVED = "confirm_received";
	    public const string SQL_COL_READ = "read";
	    public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	    public const string SQL_COL_FILENAME = "filename";

	    static string SQL_LOAD = "SELECT " +
			    "[" + SQL_COL_DATE_CREATED + "]," +
			    "[" + SQL_COL_DATE_PROCESSED + "]," +
			    "[" + SQL_COL_DATE_CONFIRMED + "]," +
			    "[" + SQL_COL_MESSAGE_ID + "]," +
			    "[" + SQL_COL_ISSUE_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]," +
			    "[" + SQL_COL_DIRECTION + "]," +
			    "[" + SQL_COL_REQUEST_CONFIRM + "]," +
			    "[" + SQL_COL_CONFIRM_RECEIVED + "]," +
			    "[" + SQL_COL_READ + "]," +
			    "[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			    "[" + SQL_COL_FILENAME + "]" +
			    " FROM " + TABLE +
			    " WHERE " + "[" + SQL_COL_MESSAGE_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_DATE_CREATED + "]," +
			    "[" + SQL_COL_DATE_PROCESSED + "]," +
			    "[" + SQL_COL_DATE_CONFIRMED + "]," +
			    "[" + SQL_COL_MESSAGE_ID + "]," +
			    "[" + SQL_COL_ISSUE_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]," +
			    "[" + SQL_COL_DIRECTION + "]," +
			    "[" + SQL_COL_REQUEST_CONFIRM + "]," +
			    "[" + SQL_COL_CONFIRM_RECEIVED + "]," +
			    "[" + SQL_COL_READ + "]," +
			    "[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			    "[" + SQL_COL_FILENAME + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_DATE_CREATED + "]," +
			    "[" + SQL_COL_DATE_PROCESSED + "]," +
			    "[" + SQL_COL_DATE_CONFIRMED + "]," +
			    "[" + SQL_COL_ISSUE_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]," +
			    "[" + SQL_COL_DIRECTION + "]," +
			    "[" + SQL_COL_REQUEST_CONFIRM + "]," +
			    "[" + SQL_COL_CONFIRM_RECEIVED + "]," +
			    "[" + SQL_COL_READ + "]," +
			    "[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			    "[" + SQL_COL_FILENAME + "]" +
			    ") VALUES ('?', '?', '?', ?, ?, ?, ?, ?, ?, ?, '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_DATE_CREATED + "]='?'," +
			    "[" + SQL_COL_DATE_PROCESSED + "]='?'," +
			    "[" + SQL_COL_DATE_CONFIRMED + "]='?'," +
			    "[" + SQL_COL_ISSUE_ID + "]=?," +
			    "[" + SQL_COL_ACCOUNT_ID + "]=?," +
			    "[" + SQL_COL_DIRECTION + "]=?," +
			    "[" + SQL_COL_REQUEST_CONFIRM + "]=?," +
			    "[" + SQL_COL_CONFIRM_RECEIVED + "]=?," +
			    "[" + SQL_COL_READ + "]=?," +
			    "[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			    "[" + SQL_COL_FILENAME + "]='?'" +
			    " WHERE " + "[" + SQL_COL_MESSAGE_ID + "] = ?;";

	    protected DateTime m_date_created;
	    protected DateTime m_date_processed;
	    protected DateTime m_date_confirmed;
	    protected int m_message_id;
	    protected int m_issue_id;
	    protected int m_account_id;
	    protected int m_direction;
	    protected bool m_request_confirm;
	    protected bool m_confirm_received;
	    protected bool m_read;
	    protected TimeStamp m_SSMA_TimeStamp;
	    protected string m_filename;
	    protected bool m_isChanged;

	    public messages() : base(TABLE, SQL_COL_MESSAGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public messages(DateTime date_created,DateTime date_processed,DateTime date_confirmed,int message_id,int issue_id,int account_id,int direction,bool request_confirm,bool confirm_received,bool read,TimeStamp SSMA_TimeStamp,string filename) : 
			    base(TABLE, SQL_COL_MESSAGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_date_created = date_created;
		    m_date_processed = date_processed;
		    m_date_confirmed = date_confirmed;
		    m_message_id = message_id;
		    m_issue_id = issue_id;
		    m_account_id = account_id;
		    m_direction = direction;
		    m_request_confirm = request_confirm;
		    m_confirm_received = confirm_received;
		    m_read = read;
		    m_SSMA_TimeStamp = SSMA_TimeStamp;
		    m_filename = filename;
	    }

	    protected messages fill(DataRow dr)
	    {
		    try {
			    m_date_created = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE_CREATED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE_CREATED]);
			    m_date_processed = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE_PROCESSED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE_PROCESSED]);
			    m_date_confirmed = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE_CONFIRMED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE_CONFIRMED]);
			    m_message_id = (int) dr[SQL_COL_MESSAGE_ID];
			    m_issue_id = (int) dr[SQL_COL_ISSUE_ID];
			    m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			    m_direction = (int) dr[SQL_COL_DIRECTION];
			    m_request_confirm = (bool) dr[SQL_COL_REQUEST_CONFIRM];
			    m_confirm_received = (bool) dr[SQL_COL_CONFIRM_RECEIVED];
			    m_read = (bool) dr[SQL_COL_READ];
			    m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			    m_filename = (string) (Convert.IsDBNull(dr[SQL_COL_FILENAME]) ? "" : dr[SQL_COL_FILENAME]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static messages load( int m_message_id)
	    {
		    messages obj  = new messages();
		    string[] opt = {m_message_id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static messages[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    messages obj = new messages();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new messages();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (messages[])list.ToArray(typeof(messages));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

	    public bool save()
	    {
		    if (m_message_id > 0 ) {
			    string[] values = {m_date_created.ToString(), m_date_processed.ToString(), m_date_confirmed.ToString(), m_issue_id.ToString(), m_account_id.ToString(), m_direction.ToString(), m_request_confirm.ToString(), m_confirm_received.ToString(), m_read.ToString(), m_SSMA_TimeStamp.ToString(), m_filename, m_message_id.ToString()};
			    if (this.updateRecord(m_message_id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_date_created.ToString(), m_date_processed.ToString(), m_date_confirmed.ToString(), m_issue_id.ToString(), m_account_id.ToString(), m_direction.ToString(), m_request_confirm.ToString(), m_confirm_received.ToString(), m_read.ToString(), m_SSMA_TimeStamp.ToString(), m_filename};
			    m_message_id = this.insertRecord(values);
			    if (m_message_id >= 0) {
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
	    public DateTime date_created {
		    set{ m_date_created = value;
		    OnPropertyChanged("date_created"); }
		    get { return m_date_created; }
	    }

	    public DateTime date_processed {
		    set{ m_date_processed = value;
		    OnPropertyChanged("date_processed"); }
		    get { return m_date_processed; }
	    }

	    public DateTime date_confirmed {
		    set{ m_date_confirmed = value;
		    OnPropertyChanged("date_confirmed"); }
		    get { return m_date_confirmed; }
	    }

	    public int message_id {
		    get { return m_message_id; }
	    }

	    public int issue_id {
		    set{ m_issue_id = value;
		    OnPropertyChanged("issue_id"); }
		    get { return m_issue_id; }
	    }

	    public int account_id {
		    set{ m_account_id = value;
		    OnPropertyChanged("account_id"); }
		    get { return m_account_id; }
	    }

	    public int direction {
		    set{ m_direction = value;
		    OnPropertyChanged("direction"); }
		    get { return m_direction; }
	    }

	    public bool request_confirm {
		    set{ m_request_confirm = value;
		    OnPropertyChanged("request_confirm"); }
		    get { return m_request_confirm; }
	    }

	    public bool confirm_received {
		    set{ m_confirm_received = value;
		    OnPropertyChanged("confirm_received"); }
		    get { return m_confirm_received; }
	    }

	    public bool read {
		    set{ m_read = value;
		    OnPropertyChanged("read"); }
		    get { return m_read; }
	    }

	    public TimeStamp SSMA_TimeStamp {
		    set{ m_SSMA_TimeStamp = value;
		    OnPropertyChanged("SSMA_TimeStamp"); }
		    get { return m_SSMA_TimeStamp; }
	    }

	    public string filename {
		    set{ m_filename = value;
		    OnPropertyChanged("filename"); }
		    get { return m_filename; }
	    }

    }
}
