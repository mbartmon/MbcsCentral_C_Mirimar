using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
public class GLTable : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "pm_GLTable";

	public const string SQL_COL_CLIENT_ID = "clientId";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_FROM = "From";
	public const string SQL_COL_TO = "To";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_TO + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_TO + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_FROM + "]," +
			"[" + SQL_COL_TO + "]" +
			") VALUES (?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_FROM + "]='?'," +
			"[" + SQL_COL_TO + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected long m_clientId;
	protected int m_ID;
	protected string m_From;
	protected string m_To;
	protected bool m_isChanged;

	public GLTable() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public GLTable(long clientId,int ID,string From,string To) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_clientId = clientId;
		m_ID = ID;
		m_From = From;
		m_To = To;
	}

	protected GLTable fill(DataRow dr)
	{
		try {
			m_clientId = (int) dr[SQL_COL_CLIENT_ID];
			m_ID = (int) dr[SQL_COL_I_D];
			m_From = (string) (Convert.IsDBNull(dr[SQL_COL_FROM]) ? "" : dr[SQL_COL_FROM]);
			m_To = (string) (Convert.IsDBNull(dr[SQL_COL_TO]) ? "" : dr[SQL_COL_TO]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static GLTable load( int m_ID)
	{
		GLTable obj  = new GLTable();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static GLTable[] load(bool WithDetail = false)
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		GLTable obj = new GLTable();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new GLTable();
			obj.fill(dr);
            if (WithDetail)
                GetBuildingAddresses(obj);   
			list.Add(obj);
			obj = null;
		}
		try {
			return (GLTable[])list.ToArray(typeof(GLTable));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static ArrayList loadList()
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        GLTable obj = new GLTable();

        ds = obj.loadAll();
        if (ds == null)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new GLTable();
            obj.fill(dr);
            GetBuildingAddresses(obj);
            list.Add(new GlTableList(obj.From, obj.To, obj.FromBuilding, obj.ToBuilding, obj));
            obj = null;
        }
        try
        {
            return list;
        }
        catch (SqlException ex)
        {
            return null;
        }

    }
    
    private static void GetBuildingAddresses(GLTable dr)
    {
        Building bld = null;
        bld = Building.loadByBuildingNumber(dr.From, GlobalShared.user.Account.clientId);
        if (bld != null)
            dr.FromBuilding = bld.Address1;
        bld = Building.loadByBuildingNumber(dr.To, GlobalShared.user.Account.clientId);
        if (bld != null)
            dr.ToBuilding = bld.Address1;
    }

			/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_clientId.ToString(), m_From, m_To, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_clientId.ToString(), m_From, m_To};
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
	public long clientId {
		set{ m_clientId = value;
		OnPropertyChanged("clientId"); }
		get { return m_clientId; }
	}

	public int ID {
		get { return m_ID; }
	}

	public string From {
		set{ m_From = value;
		OnPropertyChanged("From"); }
		get { return m_From; }
	}

	public string To {
		set{ m_To = value;
		OnPropertyChanged("To"); }
		get { return m_To; }
	}

    public string FromBuilding { get; set;}
    public string ToBuilding { get; set; }
}
}
