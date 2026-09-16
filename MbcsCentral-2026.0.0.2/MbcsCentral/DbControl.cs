using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral
{
    public class DbControl : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "DbControl";

	    public const string SQL_COL_DB_BACKUP_LAST_DATE = "DbBackupLastDate";
	    public const string SQL_COL_DB_BACKUP_TIME = "DbBackupTime";
	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_DB_BACKUP_PERIOD = "DbBackupPeriod";
	    public const string SQL_COL_DB_BACKUP_CLEAR_DAYS = "DbBackupClearDays";
	    public const string SQL_COL_DB_BACKUP_ENABLED = "DbBackupEnabled";
	    public const string SQL_COL_DB_BACKUP_LOCATION = "DbBackupLocation";
	    public const string SQL_COL_DB_MANUAL_LOCATION = "DbManualLocation";
        public const string SQL_COL_DB_BACKUP_START_DATE = "DbBackupStartDate";
        public const string SQL_COL_DB_MAPPED_DRIVE = "DbMappedDrive";

            static string SQL_LOAD = "SELECT " +
                    "[" + SQL_COL_DB_BACKUP_LAST_DATE + "]," +
                    "[" + SQL_COL_DB_BACKUP_TIME + "]," +
                    "[" + SQL_COL_I_D + "]," +
                    "[" + SQL_COL_DB_BACKUP_PERIOD + "]," +
                    "[" + SQL_COL_DB_BACKUP_CLEAR_DAYS + "]," +
                    "[" + SQL_COL_DB_BACKUP_ENABLED + "]," +
                    "[" + SQL_COL_DB_BACKUP_LOCATION + "]," +
                    "[" + SQL_COL_DB_MANUAL_LOCATION + "]," +
                    "[" + SQL_COL_DB_BACKUP_START_DATE + "]," +
                    "[" + SQL_COL_DB_MAPPED_DRIVE + "]" +
                    " FROM " + TABLE;

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_DB_BACKUP_LAST_DATE + "]," +
			    "[" + SQL_COL_DB_BACKUP_TIME + "]," +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_DB_BACKUP_PERIOD + "]," +
			    "[" + SQL_COL_DB_BACKUP_CLEAR_DAYS + "]," +
			    "[" + SQL_COL_DB_BACKUP_ENABLED + "]," +
			    "[" + SQL_COL_DB_BACKUP_LOCATION + "]," +
			    "[" + SQL_COL_DB_MANUAL_LOCATION + "]," +
                "[" + SQL_COL_DB_BACKUP_START_DATE + "]," +
                "[" + SQL_COL_DB_MAPPED_DRIVE + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_DB_BACKUP_LAST_DATE + "]," +
			    "[" + SQL_COL_DB_BACKUP_TIME + "]," +
			    "[" + SQL_COL_DB_BACKUP_PERIOD + "]," +
			    "[" + SQL_COL_DB_BACKUP_CLEAR_DAYS + "]," +
			    "[" + SQL_COL_DB_BACKUP_ENABLED + "]," +
			    "[" + SQL_COL_DB_BACKUP_LOCATION + "]," +
			    "[" + SQL_COL_DB_MANUAL_LOCATION + "]," +
                "[" + SQL_COL_DB_BACKUP_START_DATE + "]," +
                "[" + SQL_COL_DB_MAPPED_DRIVE + "]" +
			    ") VALUES ('?', '?', ?, ?, ?, '?', '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_DB_BACKUP_LAST_DATE + "]='?'," +
			    "[" + SQL_COL_DB_BACKUP_TIME + "]='?'," +
			    "[" + SQL_COL_DB_BACKUP_PERIOD + "]=?," +
			    "[" + SQL_COL_DB_BACKUP_CLEAR_DAYS + "]=?," +
			    "[" + SQL_COL_DB_BACKUP_ENABLED + "]=?," +
			    "[" + SQL_COL_DB_BACKUP_LOCATION + "]='?'," +
			    "[" + SQL_COL_DB_MANUAL_LOCATION + "]='?'," +
                "[" + SQL_COL_DB_BACKUP_START_DATE + "]='?'," +
                "[" + SQL_COL_DB_MAPPED_DRIVE + "]='?'" +
			    " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	    protected DateTime m_DbBackupLastDate;
	    protected TimeSpan m_DbBackupTime;
	    protected int m_ID;
	    protected int m_DbBackupPeriod;
	    protected int m_DbBackupClearDays;
	    protected bool m_DbBackupEnabled;
	    protected string m_DbBackupLocation;
	    protected string m_DbManualLocation;
        protected DateTime m_DbBackupStartDate;
        protected string m_DbMappedDrive;
	    protected bool m_isChanged;

	    public DbControl() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public DbControl(DateTime DbBackupLastDate, TimeSpan DbBackupTime,int ID,int DbBackupPeriod,int DbBackupClearDays,bool DbBackupEnabled,string DbBackupLocation,string DbManualLocation, DateTime DbBackupStartDate, string DbMappedDrive) : 
			    base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_DbBackupLastDate = DbBackupLastDate;
		    m_DbBackupTime = DbBackupTime;
		    m_ID = ID;
		    m_DbBackupPeriod = DbBackupPeriod;
		    m_DbBackupClearDays = DbBackupClearDays;
		    m_DbBackupEnabled = DbBackupEnabled;
		    m_DbBackupLocation = DbBackupLocation;
		    m_DbManualLocation = DbManualLocation;
            m_DbBackupStartDate = DbBackupStartDate;
            m_DbMappedDrive = DbMappedDrive;
	    }

	    protected DbControl fill(DataRow dr)
	    {
            TimeSpan ts;
		    try
                {
			    m_DbBackupLastDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DB_BACKUP_LAST_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DB_BACKUP_LAST_DATE]);
                ts = (TimeSpan)dr[SQL_COL_DB_BACKUP_TIME];
                m_DbBackupTime = ts;
			    m_ID = (int) dr[SQL_COL_I_D];
			    m_DbBackupPeriod = (int) dr[SQL_COL_DB_BACKUP_PERIOD];
			    m_DbBackupClearDays = (int) dr[SQL_COL_DB_BACKUP_CLEAR_DAYS];
			    m_DbBackupEnabled = (bool) dr[SQL_COL_DB_BACKUP_ENABLED];
			    m_DbBackupLocation = (string) (Convert.IsDBNull(dr[SQL_COL_DB_BACKUP_LOCATION]) ? "" : dr[SQL_COL_DB_BACKUP_LOCATION]);
			    m_DbManualLocation = (string) (Convert.IsDBNull(dr[SQL_COL_DB_MANUAL_LOCATION]) ? "" : dr[SQL_COL_DB_MANUAL_LOCATION]);
                m_DbBackupStartDate = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DB_BACKUP_START_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DB_BACKUP_START_DATE]);
                m_DbMappedDrive = (string)(Convert.IsDBNull(dr[SQL_COL_DB_MAPPED_DRIVE]) ? "" : dr[SQL_COL_DB_MAPPED_DRIVE]);
            }
            catch (SqlException ex)
                {
		    }
		    return this;

	    }

	    public static DbControl load( )
	    {
		    DbControl obj  = new DbControl();
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, null);
		    if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }
    /*
	    public static DbControl[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    DbControl obj = new DbControl();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new DbControl();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (DbControl[])list.ToArray(typeof(DbControl));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }
        */
	    	/// <summary>
	/// Save or Update a record
	/// </summary>
	/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	    {
		    if (m_ID > 0 ) {
			    string[] values = {m_DbBackupLastDate.ToString(), m_DbBackupTime.ToString(), m_DbBackupPeriod.ToString(), m_DbBackupClearDays.ToString(), m_DbBackupEnabled.ToString(), m_DbBackupLocation, m_DbManualLocation, m_DbBackupStartDate.ToString(), m_DbMappedDrive, m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_DbBackupLastDate.ToString(), m_DbBackupTime.ToString(), m_DbBackupPeriod.ToString(), m_DbBackupClearDays.ToString(), m_DbBackupEnabled.ToString(), m_DbBackupLocation, m_DbManualLocation, m_DbBackupStartDate.ToString(), m_DbMappedDrive};
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
	    public DateTime DbBackupLastDate {
		    set{ m_DbBackupLastDate = value;
		    OnPropertyChanged("DbBackupLastDate"); }
		    get { return m_DbBackupLastDate; }
	    }

	    public TimeSpan  DbBackupTime {
		    set{ m_DbBackupTime = value;
		    OnPropertyChanged("DbBackupTime"); }
		    get { return m_DbBackupTime; }
	    }

        public DateTime DbBackupTime24
        {
            set { m_DbBackupTime = new TimeSpan(value.Hour, value.Minute, value.Second);
                OnPropertyChanged("DbBackupTime24");}
            get { return new DateTime(2000, 1, 1, m_DbBackupTime.Hours, m_DbBackupTime.Minutes, m_DbBackupTime.Seconds); }
        }

	    public int ID {
		    get { return m_ID; }
	    }

	    public int DbBackupPeriod {
		    set{ m_DbBackupPeriod = value;
		    OnPropertyChanged("DbBackupPeriod"); }
		    get { return m_DbBackupPeriod; }
	    }

	    public int DbBackupClearDays {
		    set{ m_DbBackupClearDays = value;
		    OnPropertyChanged("DbBackupClearDays"); }
		    get { return m_DbBackupClearDays; }
	    }

	    public bool DbBackupEnabled {
		    set{ m_DbBackupEnabled = value;
		    OnPropertyChanged("DbBackupEnabled"); }
		    get { return m_DbBackupEnabled; }
	    }

	    public string DbBackupLocation {
		    set{ m_DbBackupLocation = value;
		    OnPropertyChanged("DbBackupLocation"); }
		    get { return m_DbBackupLocation; }
	    }

	    public string DbManualLocation {
		    set{ m_DbManualLocation = value;
		    OnPropertyChanged("DbManualLocation"); }
		    get { return m_DbManualLocation; }
	    }

        public DateTime DbBackupStartDate
        {
            set { m_DbBackupStartDate = value;
                OnPropertyChanged("DbBackupStartDate");
            }
            get { return m_DbBackupStartDate; }
        }

        public string DbMappedDrive
        {
            set { m_DbMappedDrive = value;
                OnPropertyChanged("DbMappedDrive");
            }
            get { return m_DbMappedDrive; }
        }

    }
}
