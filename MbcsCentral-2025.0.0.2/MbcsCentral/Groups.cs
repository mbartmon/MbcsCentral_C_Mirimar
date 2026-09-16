using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;

namespace MbcsCentral {
public class Groups : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "groups";

	public const string SQL_COL_CLIENT_ID = "clientId";
	public const string SQL_COL_GROUP_ID = "group_id";
	public const string SQL_COL_GROUP_NAME = "group_name";
	public const string SQL_COL_GROUP_DESCRIPTION = "group_description";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_GROUP_ID + "]," +
			"[" + SQL_COL_GROUP_NAME + "]," +
			"[" + SQL_COL_GROUP_DESCRIPTION + "]" +
			" FROM " + TABLE +
			" WHERE [" + SQL_COL_GROUP_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_GROUP_ID + "]," +
			"[" + SQL_COL_GROUP_NAME + "]," +
			"[" + SQL_COL_GROUP_DESCRIPTION + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_GROUP_NAME + "]," +
			"[" + SQL_COL_GROUP_DESCRIPTION + "]" +
			") VALUES (?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_GROUP_NAME + "]='?'," +
			"[" + SQL_COL_GROUP_DESCRIPTION + "]='?'" +
			" WHERE [" + SQL_COL_GROUP_ID + "] = ?;";

    static string SQL_LOAD_BY_CLIENTID = "SELECT " +
            "[" + SQL_COL_CLIENT_ID + "]," +
            "[" + SQL_COL_GROUP_ID + "]," +
            "[" + SQL_COL_GROUP_NAME + "]," +
            "[" + SQL_COL_GROUP_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_CLIENT_ID + "] = ?;";

	protected long m_clientId;
	protected long m_group_id;
	protected string m_group_name;
	protected string m_group_description;
	protected bool m_isChanged;

	public Groups() : base(TABLE, SQL_COL_GROUP_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Groups(int clientId,long group_id,string group_name,string group_description) : 
			base(TABLE, SQL_COL_GROUP_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_clientId = clientId;
		m_group_id = group_id;
		m_group_name = group_name;
		m_group_description = group_description;
	}

	protected Groups fill(DataRow dr)
	{
		try {
			m_clientId = (long) dr[SQL_COL_CLIENT_ID];
			m_group_id = (long) dr[SQL_COL_GROUP_ID];
			m_group_name = (string) (Convert.IsDBNull(dr[SQL_COL_GROUP_NAME]) ? "" : dr[SQL_COL_GROUP_NAME]);
			m_group_description = (string) (Convert.IsDBNull(dr[SQL_COL_GROUP_DESCRIPTION]) ? "" : dr[SQL_COL_GROUP_DESCRIPTION]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Groups load( long m_group_id)
	{
		Groups obj  = new Groups();
		string[] opt = {m_group_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Groups[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Groups obj = new Groups();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Groups();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Groups[])list.ToArray(typeof(Groups));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static Groups[] loadByClientId(long clientId)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        Groups obj = new Groups();

        string[] opt = {clientId.ToString()};

        ds = obj.load(SQL_LOAD_BY_CLIENTID, opt);
        if (ds == null)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Groups();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (Groups[])list.ToArray(typeof(Groups));
        }
        catch (SqlException ex)
        {
            return null;
        }

    }

		/// <summary>
	/// Save or Update a record
	/// </summary>
	/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	{
		if (m_group_id > 0 ) {
			string[] values = {m_clientId.ToString(), m_group_name, m_group_description, m_group_id.ToString()};
			if (this.updateRecord(m_group_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_clientId.ToString(), m_group_name, m_group_description};
			m_group_id = this.insertRecord(values);
			if (m_group_id >= 0) {
				return true;
			}
		}
		return false;

	}

    public bool delete()
    {
        System.Data.SqlClient.SqlConnection localCN = GlobalShared.CN;

        if (localCN.State != ConnectionState.Open)
        {
            try
            {
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Could not open DB connection.  Group not removed: " + ex.Message);
                return false;
            }
        }
        SqlCommand cmd = localCN.CreateCommand();
        SqlTransaction trans;

        // Start of transaction
        trans = localCN.BeginTransaction("Remove Group");
        cmd.Connection = localCN;
        cmd.Transaction = trans;

        try
        {
            string query = "DELETE FROM " + TABLE + " WHERE " + SQL_COL_GROUP_ID + " = " + m_group_id + " AND " + SQL_COL_CLIENT_ID + " = " + m_clientId.ToString();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            query = "DELETE FROM " + GroupMembers.TABLE + " WHERE " + GroupMembers.SQL_COL_GROUP_ID + " = " + m_group_id.ToString() + 
                    " AND " + GroupMembers.SQL_COL_ACCOUNT_ID + " IN ( SELECT " + Accounts.SQL_COL_ACCOUNT_ID + " FROM " + Accounts.TABLE + " WHERE " + Accounts.SQL_COL_CLIENT_ID + " = " + m_clientId.ToString() + ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            // Commit transaction
            trans.Commit();
            return true;
        }
        catch (System.Exception ex)
        {
            MessageBox.Show("Error while attempting to commit Group removal. Type: " + ex.GetType() + "\r\n" + ex.Message);
            try
            {
                trans.Rollback();
                return false;
            }
            catch (System.Exception exr)
            {
                MessageBox.Show("Could not Rollback Group removal.  Type: " + exr.GetType() + "\r\n" + exr.Message);
                return false;
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
	public long clientId {
		set{ m_clientId = value;
		OnPropertyChanged("clientId"); }
		get { return m_clientId; }
	}

	public long group_id {
		get { return m_group_id; }
	}

	public string group_name {
		set{ m_group_name = value;
		OnPropertyChanged("group_name"); }
		get { return m_group_name; }
	}

	public string group_description {
		set{ m_group_description = value;
		OnPropertyChanged("group_description"); }
		get { return m_group_description; }
	}

}
}
