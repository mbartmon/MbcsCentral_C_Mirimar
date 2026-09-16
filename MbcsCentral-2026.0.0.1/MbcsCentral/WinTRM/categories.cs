using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class categories : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_categories";

	public const string SQL_COL_CATEGORY_ID = "category_id";
	public const string SQL_COL_CATEGORY = "category";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_CATEGORY + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_CATEGORY_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_CATEGORY + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CATEGORY + "]" +
			") VALUES ('?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CATEGORY + "]='?'" +
			" WHERE " + "[" + SQL_COL_CATEGORY_ID + "] = ?;";

	protected int m_category_id;
	protected string m_category;
	protected bool m_isChanged;

	public categories() : base(TABLE, SQL_COL_CATEGORY_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public categories(int category_id,string category) : 
			base(TABLE, SQL_COL_CATEGORY_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_category_id = category_id;
		m_category = category;
	}

	protected categories fill(DataRow dr)
	{
		try {
			m_category_id = (int) dr[SQL_COL_CATEGORY_ID];
			m_category = (string) (Convert.IsDBNull(dr[SQL_COL_CATEGORY]) ? "" : dr[SQL_COL_CATEGORY]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static categories load( int m_category_id)
	{
		categories obj  = new categories();
		string[] opt = {m_category_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static categories[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		categories obj = new categories();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new categories();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (categories[])list.ToArray(typeof(categories));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_category_id > 0 ) {
			string[] values = {m_category, m_category_id.ToString()};
			if (this.updateRecord(m_category_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_category};
			m_category_id = this.insertRecord(values);
			if (m_category_id >= 0) {
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
	public int category_id {
		get { return m_category_id; }
	}

	public string category {
		set{ m_category = value;
		OnPropertyChanged("category"); }
		get { return m_category; }
	}

}
}
