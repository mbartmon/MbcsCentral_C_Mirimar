using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class parameters : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "parameters";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CLIENT_ID = "ClientId";
	public const string SQL_COL_ACCT_ID = "AcctId";
	public const string SQL_COL_PARAMETER_NAME = "ParameterName";
	public const string SQL_COL_VALUES = "Values";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_PARAMETER_NAME + "]," +
			"[" + SQL_COL_VALUES + "]" +
			" FROM " + TABLE +
			" WHERE [" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_PARAMETER_NAME + "]," +
			"[" + SQL_COL_VALUES + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_PARAMETER_NAME + "]," +
			"[" + SQL_COL_VALUES + "]" +
			") VALUES (?, ?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_ACCT_ID + "]=?," +
			"[" + SQL_COL_PARAMETER_NAME + "]='?'," +
			"[" + SQL_COL_VALUES + "]='?'" +
			" WHERE " + SQL_COL_I_D + " = ?;";

	static string SQL_LOAD_BY_ACCT_PARM = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_ACCT_ID + "]," +
			"[" + SQL_COL_PARAMETER_NAME + "]," +
			"[" + SQL_COL_VALUES + "]" +
			" FROM " + TABLE +
			" WHERE [" + SQL_COL_ACCT_ID + " = ?;" +
            " AND [" + SQL_COL_PARAMETER_NAME + "] = '?';";

	protected long m_ID;
	protected long m_ClientId;
	protected long m_AcctId;
	protected string m_ParameterName;
	protected string m_Values;
	protected bool m_isChanged;

	public parameters() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public parameters(long ID,long ClientId,long AcctId,string ParameterName,string Values) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_ID = ID;
		m_ClientId = ClientId;
		m_AcctId = AcctId;
		m_ParameterName = ParameterName;
		m_Values = Values;
	}

	protected parameters fill(DataRow dr)
	{
		try {
			m_ID = (long) dr[SQL_COL_I_D];
			m_ClientId = (long) dr[SQL_COL_CLIENT_ID];
			m_AcctId = (long) dr[SQL_COL_ACCT_ID];
			m_ParameterName = (string) (Convert.IsDBNull(dr[SQL_COL_PARAMETER_NAME]) ? "" : dr[SQL_COL_PARAMETER_NAME]);
			m_Values = (string) (Convert.IsDBNull(dr[SQL_COL_VALUES]) ? "" : dr[SQL_COL_VALUES]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static parameters load( long m_ID)
	{
		parameters obj  = new parameters();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static parameters[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		parameters obj = new parameters();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new parameters();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (parameters[])list.ToArray(typeof(parameters));
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
			string[] values = {m_ClientId.ToString(), m_AcctId.ToString(), m_ParameterName, m_Values, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_ClientId.ToString(), m_AcctId.ToString(), m_ParameterName, m_Values};
			m_ID = this.insertRecord(values);
			if (m_ID >= 0) {
				return true;
			}
		}
		return false;

	}

    public static string[] GetParms(long clientId, long AcctId, string parmName)
    {
        ArrayList list = new ArrayList();
		DataSet ds = new DataSet();
		parameters obj = new parameters();
        string[] opt = {AcctId.ToString(), parmName};

		ds = obj.load(SQL_LOAD_BY_ACCT_PARM, opt);
		if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			return null;
		obj.fill(ds.Tables[0].Rows[0]);
        
        string[] s = ((string) dr[SQL_COL_VALUES]).Split(",");
        bool done = false;
        int i = 0;

        while (!done)
        {
            if (i < s.Length - 1)
            {
                list.Add(s[i] + "," + s[i + 1]);
                i += 2;
            }        
            done = true;
        }

		try {
			return (string[])list.ToArray(typeof(string));
		} catch (SqlException ex) {
			return null;
		}


    }

	private void OnPropertyChanged(string info) {
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			m_isChanged = true;
		}
	}

	// Properties
	public long ID {
		get { return m_ID; }
	}

	public long ClientId {
		set{ m_ClientId = value;
		OnPropertyChanged("ClientId"); }
		get { return m_ClientId; }
	}

	public long AcctId {
		set{ m_AcctId = value;
		OnPropertyChanged("AcctId"); }
		get { return m_AcctId; }
	}

	public string ParameterName {
		set{ m_ParameterName = value;
		OnPropertyChanged("ParameterName"); }
		get { return m_ParameterName; }
	}

	public string Values {
		set{ m_Values = value;
		OnPropertyChanged("Values"); }
		get { return m_Values; }
	}

}
}
