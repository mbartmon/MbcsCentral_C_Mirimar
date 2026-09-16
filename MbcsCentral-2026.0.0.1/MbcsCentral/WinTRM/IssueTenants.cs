using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class IssueTenants : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_IssueTenants";

	public const string SQL_COL_TIMESTAMP = "timestamp";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_ISSUE_ID = "issue_id";
	public const string SQL_COL_SEQUENCE_NUMBER = "sequence_number";
	public const string SQL_COL_BUILDING_NUMBER = "buildingNumber";
	public const string SQL_COL_TENANT_NUMBER = "tenantNumber";
	public const string SQL_COL_APARTMENT = "apartment";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_APARTMENT + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_APARTMENT + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_APARTMENT + "]" +
			") VALUES ('?', ?, ?, '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_TIMESTAMP + "]='?'," +
			"[" + SQL_COL_ISSUE_ID + "]=?," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
			"[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
			"[" + SQL_COL_TENANT_NUMBER + "]='?'," +
			"[" + SQL_COL_APARTMENT + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected DateTime m_timestamp;
	protected int m_ID;
	protected int m_issue_id;
	protected int m_sequence_number;
	protected string m_buildingNumber;
	protected string m_tenantNumber;
	protected string m_apartment;
	protected bool m_isChanged;

	public IssueTenants() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public IssueTenants(DateTime timestamp,int ID,int issue_id,int sequence_number,string buildingNumber,string tenantNumber,string apartment) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_timestamp = timestamp;
		m_ID = ID;
		m_issue_id = issue_id;
		m_sequence_number = sequence_number;
		m_buildingNumber = buildingNumber;
		m_tenantNumber = tenantNumber;
		m_apartment = apartment;
	}

	protected IssueTenants fill(DataRow dr)
	{
		try {
			m_timestamp = (DateTime) (Convert.IsDBNull(dr[SQL_COL_TIMESTAMP]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_TIMESTAMP]);
			m_ID = (int) dr[SQL_COL_I_D];
			m_issue_id = (int) dr[SQL_COL_ISSUE_ID];
			m_sequence_number = (int) dr[SQL_COL_SEQUENCE_NUMBER];
			m_buildingNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
			m_tenantNumber = (string) (Convert.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
			m_apartment = (string) (Convert.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static IssueTenants load( int m_ID)
	{
		IssueTenants obj  = new IssueTenants();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static IssueTenants[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		IssueTenants obj = new IssueTenants();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new IssueTenants();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (IssueTenants[])list.ToArray(typeof(IssueTenants));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_timestamp.ToString(), m_issue_id.ToString(), m_sequence_number.ToString(), m_buildingNumber, m_tenantNumber, m_apartment, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_timestamp.ToString(), m_issue_id.ToString(), m_sequence_number.ToString(), m_buildingNumber, m_tenantNumber, m_apartment};
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
	public DateTime timestamp {
		set{ m_timestamp = value;
		OnPropertyChanged("timestamp"); }
		get { return m_timestamp; }
	}

	public int ID {
		get { return m_ID; }
	}

	public int issue_id {
		set{ m_issue_id = value;
		OnPropertyChanged("issue_id"); }
		get { return m_issue_id; }
	}

	public int sequence_number {
		set{ m_sequence_number = value;
		OnPropertyChanged("sequence_number"); }
		get { return m_sequence_number; }
	}

	public string buildingNumber {
		set{ m_buildingNumber = value;
		OnPropertyChanged("buildingNumber"); }
		get { return m_buildingNumber; }
	}

	public string tenantNumber {
		set{ m_tenantNumber = value;
		OnPropertyChanged("tenantNumber"); }
		get { return m_tenantNumber; }
	}

	public string apartment {
		set{ m_apartment = value;
		OnPropertyChanged("apartment"); }
		get { return m_apartment; }
	}

}
}
