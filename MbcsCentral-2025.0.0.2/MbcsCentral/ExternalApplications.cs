using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class ExternalApplications : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "ExternalApplications";

	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_SEQUENCE = "Sequence";
	public const string SQL_COL_MENU_NAME = "MenuName";
	public const string SQL_COL_APPLICATION_PATH = "ApplicationPath";
	public const string SQL_COL_MENU_CONTENT = "MenuContent";
	public const string SQL_COL_MENU_TYPE = "MenuType";
	public const string SQL_COL_APPLICATION_NAME = "ApplicationName";
	public const string SQL_COL_APPLICATION_APP_NAME = "ApplicationAppName";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_SEQUENCE + "]," +
			"[" + SQL_COL_MENU_NAME + "]," +
			"[" + SQL_COL_APPLICATION_PATH + "]," +
			"[" + SQL_COL_MENU_CONTENT + "]," +
			"[" + SQL_COL_MENU_TYPE + "]," +
			"[" + SQL_COL_APPLICATION_NAME + "]," +
			"[" + SQL_COL_APPLICATION_APP_NAME + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_SEQUENCE + "]," +
			"[" + SQL_COL_MENU_NAME + "]," +
			"[" + SQL_COL_APPLICATION_PATH + "]," +
			"[" + SQL_COL_MENU_CONTENT + "]," +
			"[" + SQL_COL_MENU_TYPE + "]," +
			"[" + SQL_COL_APPLICATION_NAME + "]," +
			"[" + SQL_COL_APPLICATION_APP_NAME + "]" +
			" FROM " + TABLE + 
            " ORDER BY " + SQL_COL_SEQUENCE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_SEQUENCE + "]," +
			"[" + SQL_COL_MENU_NAME + "]," +
			"[" + SQL_COL_APPLICATION_PATH + "]," +
			"[" + SQL_COL_MENU_CONTENT + "]," +
			"[" + SQL_COL_MENU_TYPE + "]," +
			"[" + SQL_COL_APPLICATION_NAME + "]," +
			"[" + SQL_COL_APPLICATION_APP_NAME + "]" +
			") VALUES (?, '?', '?', '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_SEQUENCE + "]=?," +
			"[" + SQL_COL_MENU_NAME + "]='?'," +
			"[" + SQL_COL_APPLICATION_PATH + "]='?'," +
			"[" + SQL_COL_MENU_CONTENT + "]='?'," +
			"[" + SQL_COL_MENU_TYPE + "]='?'," +
			"[" + SQL_COL_APPLICATION_NAME + "]='?'," +
			"[" + SQL_COL_APPLICATION_APP_NAME + "]='?'" +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

	protected long m_Id;
	protected int m_Sequence;
	protected string m_MenuName;
	protected string m_ApplicationPath;
	protected string m_MenuContent;
	protected string m_MenuType;
	protected string m_ApplicationName;
	protected string m_ApplicationAppName;
	protected bool m_isChanged;
    protected bool m_Delete;

	public ExternalApplications() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public ExternalApplications(long Id,int Sequence,string MenuName,string ApplicationPath,string MenuContent,string MenuType,string ApplicationName,string ApplicationAppName) : 
			base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Id = Id;
		m_Sequence = Sequence;
		m_MenuName = MenuName;
		m_ApplicationPath = ApplicationPath;
		m_MenuContent = MenuContent;
		m_MenuType = MenuType;
		m_ApplicationName = ApplicationName;
		m_ApplicationAppName = ApplicationAppName;
	}

	protected ExternalApplications fill(DataRow dr)
	{
		try {
			m_Id = (long) dr[SQL_COL_ID];
			m_Sequence = (int) dr[SQL_COL_SEQUENCE];
			m_MenuName = (string) (Convert.IsDBNull(dr[SQL_COL_MENU_NAME]) ? "" : dr[SQL_COL_MENU_NAME]);
			m_ApplicationPath = (string) (Convert.IsDBNull(dr[SQL_COL_APPLICATION_PATH]) ? "" : dr[SQL_COL_APPLICATION_PATH]);
			m_MenuContent = (string) (Convert.IsDBNull(dr[SQL_COL_MENU_CONTENT]) ? "" : dr[SQL_COL_MENU_CONTENT]);
			m_MenuType = (string) (Convert.IsDBNull(dr[SQL_COL_MENU_TYPE]) ? "" : dr[SQL_COL_MENU_TYPE]);
			m_ApplicationName = (string) (Convert.IsDBNull(dr[SQL_COL_APPLICATION_NAME]) ? "" : dr[SQL_COL_APPLICATION_NAME]);
			m_ApplicationAppName = (string) (Convert.IsDBNull(dr[SQL_COL_APPLICATION_APP_NAME]) ? "" : dr[SQL_COL_APPLICATION_APP_NAME]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static ExternalApplications load( int m_Id)
	{
		ExternalApplications obj  = new ExternalApplications();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static ExternalApplications[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		ExternalApplications obj = new ExternalApplications();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new ExternalApplications();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (ExternalApplications[])list.ToArray(typeof(ExternalApplications));
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
		if (m_Id > 0 ) {
			string[] values = {m_Sequence.ToString(), m_MenuName, m_ApplicationPath, m_MenuContent, m_MenuType, m_ApplicationName, m_ApplicationAppName, m_Id.ToString()};
			if (this.updateRecord(m_Id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Sequence.ToString(), m_MenuName, m_ApplicationPath, m_MenuContent, m_MenuType, m_ApplicationName, m_ApplicationAppName};
			m_Id = this.insertRecord(values);
			if (m_Id >= 0) {
				return true;
			}
		}
		return false;

	}

    public static int Sort(ExternalApplications x, ExternalApplications y)
    {
        if (x == null)
        {
            if (y == null)
            {
                // equal
                return 0;
            }
            else
            {
                // y is greater
                return -1;
            }

        }
        else
        {
            if (y == null)
            {
                // x is greater
                return 1;
            }
            else
            {
                return x.Sequence.CompareTo(y.Sequence);
            }
        }
    }

	private void OnPropertyChanged(string info) {
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			m_isChanged = true;
		}
	}

	// Properties
	public long Id {
		get { return m_Id; }
	}

	public int Sequence {
		set{ m_Sequence = value;
		OnPropertyChanged("Sequence"); }
		get { return m_Sequence; }
	}

	public string MenuName {
		set{ m_MenuName = value;
		OnPropertyChanged("MenuName"); }
		get { return m_MenuName; }
	}

	public string ApplicationPath {
		set{ m_ApplicationPath = value;
		OnPropertyChanged("ApplicationPath"); }
		get { return m_ApplicationPath; }
	}

	public string MenuContent {
		set{ m_MenuContent = value;
		OnPropertyChanged("MenuContent"); }
		get { return m_MenuContent; }
	}

	public string MenuType {
		set{ m_MenuType = value;
		OnPropertyChanged("MenuType"); }
		get { return m_MenuType; }
	}

	public string ApplicationName {
		set{ m_ApplicationName = value;
		OnPropertyChanged("ApplicationName"); }
		get { return m_ApplicationName; }
	}

	public string ApplicationAppName {
		set{ m_ApplicationAppName = value;
		OnPropertyChanged("ApplicationAppName"); }
		get { return m_ApplicationAppName; }
	}


    public bool Delete
    {
        get { return m_Delete; }
        set
        {
            m_Delete = value;
            OnPropertyChanged("Delete");
        }
    }

    public bool IsChanged
    { get { return m_isChanged; }
        set { m_isChanged = value; }
    }
}
}
