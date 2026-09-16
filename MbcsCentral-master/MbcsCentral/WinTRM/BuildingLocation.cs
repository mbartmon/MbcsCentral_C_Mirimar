using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class BuildingLocation : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_BuildingLocation";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_LOCATION = "Location";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_LOCATION + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_LOCATION + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_LOCATION + "]" +
			") VALUES ('?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_LOCATION + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected int m_ID;
	protected string m_Location;
	protected bool m_isChanged;

	public BuildingLocation() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public BuildingLocation(int ID,string Location) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_ID = ID;
		m_Location = Location;
	}

	protected BuildingLocation fill(DataRow dr)
	{
		try {
			m_ID = (int) dr[SQL_COL_I_D];
			m_Location = (string) (Convert.IsDBNull(dr[SQL_COL_LOCATION]) ? "" : dr[SQL_COL_LOCATION]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static BuildingLocation load( int m_ID)
	{
		BuildingLocation obj  = new BuildingLocation();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static BuildingLocation[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		BuildingLocation obj = new BuildingLocation();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new BuildingLocation();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (BuildingLocation[])list.ToArray(typeof(BuildingLocation));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_Location, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Location};
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
	public int ID {
		get { return m_ID; }
	}

	public string Location {
		set{ m_Location = value;
		OnPropertyChanged("Location"); }
		get { return m_Location; }
	}

}
}
