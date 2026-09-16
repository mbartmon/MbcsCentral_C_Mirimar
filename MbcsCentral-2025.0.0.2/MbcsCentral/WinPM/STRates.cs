using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
public class STRates : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	static string TABLE = "pm_STRates";

	public const string SQL_COL_CLIENT_ID = "clientId";
	public const string SQL_COL_RATE = "Rate";
	public const string SQL_COL_DATE = "Date";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_BUILDING_NUMBER = "Building Number";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_RATE + "]," +
			"[" + SQL_COL_DATE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]" +
			" FROM " + TABLE +
			" WHERE " + SQL_COL_I_D + " = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_RATE + "]," +
			"[" + SQL_COL_DATE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_RATE + "]," +
			"[" + SQL_COL_DATE + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]" +
			") VALUES (?, ?, ?, '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_RATE + "]=?," +
			"[" + SQL_COL_DATE + "]='?'," +
			"[" + SQL_COL_BUILDING_NUMBER + "]=?" +
			" WHERE " + SQL_COL_I_D + " = ?;";

	protected long m_clientId;
	protected float m_Rate;
	protected DateTime m_Date;
	protected int m_ID;
	protected string m_BuildingNumber;
	protected bool m_isChanged;

	public STRates() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public STRates(long clientId,float Rate,DateTime Date,int ID,string BuildingNumber) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_clientId = clientId;
		m_Rate = Rate;
		m_Date = Date;
		m_ID = ID;
		m_BuildingNumber = BuildingNumber;
	}

	protected STRates fill(DataRow dr)
	{
		try {
			m_clientId = (long) dr[SQL_COL_CLIENT_ID];
			m_Rate = (float) dr[SQL_COL_RATE];
			m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
			m_ID = (int) dr[SQL_COL_I_D];
			m_BuildingNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static STRates load( int m_ID)
	{
		STRates obj  = new STRates();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static STRates[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		STRates obj = new STRates();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new STRates();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (STRates[])list.ToArray(typeof(STRates));
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
			string[] values = {m_clientId.ToString(), m_Rate.ToString(), m_Date.ToString(), m_BuildingNumber, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_clientId.ToString(), m_Rate.ToString(), m_Date.ToString(), m_BuildingNumber};
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

	public float Rate {
		set{ m_Rate = value;
		OnPropertyChanged("Rate"); }
		get { return m_Rate; }
	}

	public DateTime Date {
		set{ m_Date = value;
		OnPropertyChanged("Date"); }
		get { return m_Date; }
	}

	public int ID {
		get { return m_ID; }
	}

	public string BuildingNumber {
		set{ m_BuildingNumber = value;
		OnPropertyChanged("BuildingNumber"); }
		get { return m_BuildingNumber; }
	}

}
}
