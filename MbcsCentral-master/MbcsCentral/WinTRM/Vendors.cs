using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class Vendors : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_Vendors";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_VENDOR_NAME = "VendorName";
	public const string SQL_COL_ADDRESS1 = "Address1";
	public const string SQL_COL_ADDRESS2 = "Address2";
	public const string SQL_COL_CITY = "City";
	public const string SQL_COL_STATE = "State";
	public const string SQL_COL_ZIP = "Zip";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_VENDOR_NAME + "]," +
			"[" + SQL_COL_ADDRESS1 + "]," +
			"[" + SQL_COL_ADDRESS2 + "]," +
			"[" + SQL_COL_CITY + "]," +
			"[" + SQL_COL_STATE + "]," +
			"[" + SQL_COL_ZIP + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_VENDOR_NAME + "]," +
			"[" + SQL_COL_ADDRESS1 + "]," +
			"[" + SQL_COL_ADDRESS2 + "]," +
			"[" + SQL_COL_CITY + "]," +
			"[" + SQL_COL_STATE + "]," +
			"[" + SQL_COL_ZIP + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_VENDOR_NAME + "]," +
			"[" + SQL_COL_ADDRESS1 + "]," +
			"[" + SQL_COL_ADDRESS2 + "]," +
			"[" + SQL_COL_CITY + "]," +
			"[" + SQL_COL_STATE + "]," +
			"[" + SQL_COL_ZIP + "]" +
			") VALUES ('?', '?', '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_VENDOR_NAME + "]='?'," +
			"[" + SQL_COL_ADDRESS1 + "]='?'," +
			"[" + SQL_COL_ADDRESS2 + "]='?'," +
			"[" + SQL_COL_CITY + "]='?'," +
			"[" + SQL_COL_STATE + "]='?'," +
			"[" + SQL_COL_ZIP + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected int m_ID;
	protected string m_VendorName;
	protected string m_Address1;
	protected string m_Address2;
	protected string m_City;
	protected string m_State;
	protected string m_Zip;
	protected bool m_isChanged;

	public Vendors() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Vendors(int ID,string VendorName,string Address1,string Address2,string City,string State,string Zip) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_ID = ID;
		m_VendorName = VendorName;
		m_Address1 = Address1;
		m_Address2 = Address2;
		m_City = City;
		m_State = State;
		m_Zip = Zip;
	}

	protected Vendors fill(DataRow dr)
	{
		try {
			m_ID = (int) dr[SQL_COL_I_D];
			m_VendorName = (string) (Convert.IsDBNull(dr[SQL_COL_VENDOR_NAME]) ? "" : dr[SQL_COL_VENDOR_NAME]);
			m_Address1 = (string) (Convert.IsDBNull(dr[SQL_COL_ADDRESS1]) ? "" : dr[SQL_COL_ADDRESS1]);
			m_Address2 = (string) (Convert.IsDBNull(dr[SQL_COL_ADDRESS2]) ? "" : dr[SQL_COL_ADDRESS2]);
			m_City = (string) (Convert.IsDBNull(dr[SQL_COL_CITY]) ? "" : dr[SQL_COL_CITY]);
			m_State = (string) (Convert.IsDBNull(dr[SQL_COL_STATE]) ? "" : dr[SQL_COL_STATE]);
			m_Zip = (string) (Convert.IsDBNull(dr[SQL_COL_ZIP]) ? "" : dr[SQL_COL_ZIP]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Vendors load( int m_ID)
	{
		Vendors obj  = new Vendors();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Vendors[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Vendors obj = new Vendors();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Vendors();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Vendors[])list.ToArray(typeof(Vendors));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_VendorName, m_Address1, m_Address2, m_City, m_State, m_Zip, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_VendorName, m_Address1, m_Address2, m_City, m_State, m_Zip};
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

	public string VendorName {
		set{ m_VendorName = value;
		OnPropertyChanged("VendorName"); }
		get { return m_VendorName; }
	}

	public string Address1 {
		set{ m_Address1 = value;
		OnPropertyChanged("Address1"); }
		get { return m_Address1; }
	}

	public string Address2 {
		set{ m_Address2 = value;
		OnPropertyChanged("Address2"); }
		get { return m_Address2; }
	}

	public string City {
		set{ m_City = value;
		OnPropertyChanged("City"); }
		get { return m_City; }
	}

	public string State {
		set{ m_State = value;
		OnPropertyChanged("State"); }
		get { return m_State; }
	}

	public string Zip {
		set{ m_Zip = value;
		OnPropertyChanged("Zip"); }
		get { return m_Zip; }
	}

}
}
