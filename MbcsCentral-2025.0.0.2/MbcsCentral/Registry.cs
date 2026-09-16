using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class Registry : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "Registry";

	public const string SQL_COL_DATA_TYPE = "dataType";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CLIENT_ID = "clientId";
	public const string SQL_COL_ACCT_ID = "acctId";
	public const string SQL_COL_VALUE = "value";
    public const string SQL_COL_VALUE_STRING = "valuestring";
    public const string SQL_COL_MAIN_KEY = "mainKey";
	public const string SQL_COL_SUB_KEY = "subKey";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_DATA_TYPE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_VALUE + "]," +
            "[" + SQL_COL_VALUE_STRING + "]," +
			"[" + SQL_COL_MAIN_KEY + "]," +
			"[" + SQL_COL_SUB_KEY + "]" +
			" FROM " + TABLE +
			" WHERE " + SQL_COL_I_D + "= ?";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_DATA_TYPE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_VALUE + "]," +
            "[" + SQL_COL_VALUE_STRING + "]," +
            "[" + SQL_COL_MAIN_KEY + "]," +
			"[" + SQL_COL_SUB_KEY + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_DATA_TYPE + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_VALUE + "]," +
            "[" + SQL_COL_VALUE_STRING + "]," +
            "[" + SQL_COL_MAIN_KEY + "]," +
			"[" + SQL_COL_SUB_KEY + "]" +
			") VALUES (?, ?, ?, ?, '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_DATA_TYPE + "]=?," +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_ACCT_ID + "]=?," +
			"[" + SQL_COL_VALUE + "]=?," +
            "[" + SQL_COL_VALUE_STRING + "]='?'," +
            "[" + SQL_COL_MAIN_KEY + "]='?'," +
			"[" + SQL_COL_SUB_KEY + "]='?'" +
			" WHERE " + SQL_COL_I_D + "= ?" ;

    static string SQL_LOAD_BY_KEYS = "SELECT " +
            "[" + SQL_COL_DATA_TYPE + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_CLIENT_ID + "]," +
            "[" + SQL_COL_ACCT_ID + "]," +
            "[" + SQL_COL_VALUE + "]," +
            "[" + SQL_COL_VALUE_STRING + "]," +
            "[" + SQL_COL_MAIN_KEY + "]," +
            "[" + SQL_COL_SUB_KEY + "]" +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_MAIN_KEY + "= '?'" +
            " AND " + SQL_COL_SUB_KEY + "= '?'" +
            " AND " + SQL_COL_ACCT_ID + "= ?";

    static string SQL_LOAD_BY_USER = "SELECT " +
           "[" + SQL_COL_DATA_TYPE + "]," +
           "[" + SQL_COL_I_D + "]," +
           "[" + SQL_COL_CLIENT_ID + "]," +
           "[" + SQL_COL_ACCT_ID + "]," +
           "[" + SQL_COL_VALUE + "]," +
           "[" + SQL_COL_VALUE_STRING + "]," +
           "[" + SQL_COL_MAIN_KEY + "]," +
           "[" + SQL_COL_SUB_KEY + "]" +
           " FROM " + TABLE +
           " WHERE " + SQL_COL_ACCT_ID + "= ?";

    public enum DataTypes
    {
        String = 0,
        Dword = 1,
        Qword = 2,
        Binary = 3
    };

	protected int m_dataType;
	protected long m_ID;
	protected long m_clientId;
	protected long m_acctId;
	protected object m_value = 0;
    protected string m_valuestring = "";
	protected string m_mainKey;
	protected string m_subKey;
	protected bool m_isChanged;

	public Registry() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Registry(int dataType,long ID,long clientId,long acctId,object value,string mainKey,string subKey) : 
			base(TABLE, SQL_COL_I_D , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_dataType = dataType;
		m_ID = ID;
		m_clientId = clientId;
		m_acctId = acctId;
        if (dataType == (int)DataTypes.String)
            m_valuestring = (string)value;
        else
		    m_value = value;
		m_mainKey = mainKey;
		m_subKey = subKey;
	}

	protected Registry fill(DataRow dr)
	{
		try {
			m_dataType = (int) dr[SQL_COL_DATA_TYPE];
			m_ID = (long) dr[SQL_COL_I_D];
			m_clientId = (long) dr[SQL_COL_CLIENT_ID];
			m_acctId = (long) dr[SQL_COL_ACCT_ID];
			m_value = (object) dr[SQL_COL_VALUE];
            m_valuestring = (string)(Convert.IsDBNull(dr[SQL_COL_VALUE_STRING]) ? " " : dr[SQL_COL_VALUE_STRING]);
			m_mainKey = (string) (Convert.IsDBNull(dr[SQL_COL_MAIN_KEY]) ? "" : dr[SQL_COL_MAIN_KEY]);
			m_subKey = (string) (Convert.IsDBNull(dr[SQL_COL_SUB_KEY]) ? "" : dr[SQL_COL_SUB_KEY]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Registry load(long id)
	{
		Registry obj  = new Registry();
		string[] opt = {id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null && ds.Tables[0].Rows.Count > 0) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;
	}

    public static Registry load(string mainKey, string subKey, long acctId)
    {
        Registry obj = new Registry();
        string[] opt = {mainKey, subKey, acctId.ToString() };
        DataSet ds = new DataSet();
        ds = obj.load(SQL_LOAD_BY_KEYS, opt);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            return obj.fill(dr);
        }
        else
            return null;
    }

    public static Registry[] loadEntries(string mainKey, string subKey, long acctId)
    {
        ArrayList list = new ArrayList();
        Registry obj = new Registry();
        string[] opt = { mainKey, subKey, acctId.ToString() };
        DataSet ds = new DataSet();
        ds = obj.load(SQL_LOAD_BY_KEYS, opt);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
                obj = new Registry();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
        }
        try
        {
            return (Registry[])list.ToArray(typeof(Registry));
        }
        catch (SqlException ex)
        {
            return null;
        }
    }

        public static Registry[] loadByUser(long acctId)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        Registry obj = new MbcsCentral.Registry();
        string[] opt = { acctId.ToString() };

        ds = obj.load(SQL_LOAD_BY_USER, opt);
        if (ds == null)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Registry();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (Registry[])list.ToArray(typeof(Registry));
        }
        catch (SqlException ex)
        {
            return null;
        }
    }
	public static Registry[] loads()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Registry obj = new MbcsCentral.Registry();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Registry();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Registry[])list.ToArray(typeof(Registry));
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
        if (m_value != null && m_valuestring != null)
        {
            if (m_dataType == (int)DataTypes.String)
            {
                //m_valuestring = (string)m_value;
                m_value = 0;

            }
            if (m_ID > 0)
            {
                string[] values = { m_dataType.ToString(), m_clientId.ToString(), m_acctId.ToString(), m_value.ToString(),m_valuestring, m_mainKey, m_subKey, m_ID.ToString() };
                if (this.updateRecord(m_ID.ToString(), values))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string[] values = { m_dataType.ToString(), m_clientId.ToString(), m_acctId.ToString(), m_value.ToString(), m_valuestring, m_mainKey, m_subKey };
                m_ID = this.insertRecord(values);
                if (m_ID >= 0)
                {
                    return true;
                }
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

    public double GetValueDouble()
    {
        double d = 0;

        if (BitConverter.IsLittleEndian)
            Array.Reverse((byte[])m_value);
        d = (double)BitConverter.ToInt32((byte[])m_value, 0);

        return d;
    }

    public string GetValueString()
    {
        string s = "";

        s = (m_valuestring == null ? " " : m_valuestring);

        return s;
    }

	// Properties
	public int dataType {
		set{ m_dataType = value;
		OnPropertyChanged("dataType"); }
		get { return m_dataType; }
	}

	public long ID {
		set{ m_ID = value;
		OnPropertyChanged("ID"); }
		get { return m_ID; }
	}

	public long clientId {
		set{ m_clientId = value;
		OnPropertyChanged("clientId"); }
		get { return m_clientId; }
	}

	public long acctId {
		set{ m_acctId = value;
		OnPropertyChanged("acctId"); }
		get { return m_acctId; }
	}

	public object value {
		set{
            if (m_dataType == (int)DataTypes.String)
                m_valuestring = (string)value;
            else
                m_value = value;
		OnPropertyChanged("value"); }
		get { return m_value; }
	}

	public string mainKey {
		set{ m_mainKey = value;
		OnPropertyChanged("mainKey"); }
		get { return m_mainKey; }
	}

	public string subKey {
		set{ m_subKey = value;
		OnPropertyChanged("subKey"); }
		get { return m_subKey; }
	}

}
}
