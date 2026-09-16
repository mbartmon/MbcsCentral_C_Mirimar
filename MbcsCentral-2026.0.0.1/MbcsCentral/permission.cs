using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class permission : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "permission";

	public const string SQL_COL_PERMISSION_TYPE = "permission_type";
	public const string SQL_COL_PERMISSION_ID = "permission_id";
	public const string SQL_COL_PERMISSION_NAME = "permission_name";
	public const string SQL_COL_PERMISSION_DESCRIPTION = "permission_description";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_PERMISSION_TYPE + "]," +
			"[" + SQL_COL_PERMISSION_ID + "]," +
			"[" + SQL_COL_PERMISSION_NAME + "]," +
			"[" + SQL_COL_PERMISSION_DESCRIPTION + "]" +
			" FROM " + TABLE +
			" WHERE [" + SQL_COL_PERMISSION_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_PERMISSION_TYPE + "]," +
			"[" + SQL_COL_PERMISSION_ID + "]," +
			"[" + SQL_COL_PERMISSION_NAME + "]," +
			"[" + SQL_COL_PERMISSION_DESCRIPTION + "]" +
			" FROM " + TABLE + 
            " ORDER BY " + SQL_COL_PERMISSION_NAME + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_PERMISSION_TYPE + "]," +
			"[" + SQL_COL_PERMISSION_NAME + "]," +
			"[" + SQL_COL_PERMISSION_DESCRIPTION + "]" +
			") VALUES (?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_PERMISSION_TYPE + "]=?," +
			"[" + SQL_COL_PERMISSION_NAME + "]='?'," +
			"[" + SQL_COL_PERMISSION_DESCRIPTION + "]='?'" +
			" WHERE [" + SQL_COL_PERMISSION_ID + "] = ?;";

    static string SQL_LOAD_BY_NAME = "SELECT " +
            "[" + SQL_COL_PERMISSION_TYPE + "]," +
            "[" + SQL_COL_PERMISSION_ID + "]," +
            "[" + SQL_COL_PERMISSION_NAME + "]," +
            "[" + SQL_COL_PERMISSION_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_PERMISSION_NAME + "] = '?';";

    protected int m_permission_type;
	protected long m_permission_id;
	protected string m_permission_name;
	protected string m_permission_description;
	protected bool m_isChanged;

    public enum PermissionTypes
    {
        Application = 0,
        Function = 1,
        Admin = 2
    };
    public string[] PermissionDescriptions = { "Application Level", "Function Level", "Administration Level" };

	public permission() : base(TABLE, SQL_COL_PERMISSION_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public permission(int permission_type,long permission_id,string permission_name,string permission_description) : 
			base(TABLE, SQL_COL_PERMISSION_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_permission_type = permission_type;
		m_permission_id = permission_id;
		m_permission_name = permission_name;
		m_permission_description = permission_description;
	}

	protected permission fill(DataRow dr)
	{
		try {
			m_permission_type = (int) dr[SQL_COL_PERMISSION_TYPE];
			m_permission_id = (long) dr[SQL_COL_PERMISSION_ID];
			m_permission_name = (string) (Convert.IsDBNull(dr[SQL_COL_PERMISSION_NAME]) ? "" : dr[SQL_COL_PERMISSION_NAME]);
			m_permission_description = (string) (Convert.IsDBNull(dr[SQL_COL_PERMISSION_DESCRIPTION]) ? "" : dr[SQL_COL_PERMISSION_DESCRIPTION]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static permission load( long m_permission_id)
	{
		permission obj  = new permission();
		string[] opt = {m_permission_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static permission[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		permission obj = new permission();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new permission();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (permission[])list.ToArray(typeof(permission));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static permission loadByName(string name)
    {
        permission obj = new permission();
        string[] opt = { name };
        DataSet ds = new DataSet();
        ds = obj.load(SQL_LOAD_BY_NAME, opt);
        if (ds != null)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            return obj.fill(dr);
        }
        else
            return null;

    }

    	/// <summary>
	/// Save or Update a record
	/// </summary>
	/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	{
		if (m_permission_id > 0 ) {
			string[] values = {m_permission_type.ToString(), m_permission_name, m_permission_description, m_permission_id.ToString()};
			if (this.updateRecord(m_permission_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_permission_type.ToString(), m_permission_name, m_permission_description};
			m_permission_id = this.insertRecord(values);
			if (m_permission_id >= 0) {
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
	public int permission_type {
		set{ m_permission_type = value;
		OnPropertyChanged("permission_type"); }
		get { return m_permission_type; }
	}

	public long permission_id {
		get { return m_permission_id; }
	}

	public string permission_name {
		set{ m_permission_name = value;
		OnPropertyChanged("permission_name"); }
		get { return m_permission_name; }
	}

	public string permission_description {
		set{ m_permission_description = value;
		OnPropertyChanged("permission_description"); }
		get { return m_permission_description; }
	}

}
}
