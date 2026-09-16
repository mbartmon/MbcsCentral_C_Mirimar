using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class DocumentCategories : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "DocumentCategories";

	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CATEGORY_NAME = "CategoryName";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_NAME + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_NAME + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_CATEGORY_NAME + "]" +
			") VALUES ('?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_CATEGORY_NAME + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	protected int m_ID;
	protected string m_CategoryName;
	protected bool m_isChanged;

	public DocumentCategories() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public DocumentCategories(int ID,string CategoryName) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_ID = ID;
		m_CategoryName = CategoryName;
	}

	protected DocumentCategories fill(DataRow dr)
	{
		try {
			m_ID = (int) dr[SQL_COL_I_D];
			m_CategoryName = (string) (Convert.IsDBNull(dr[SQL_COL_CATEGORY_NAME]) ? "" : dr[SQL_COL_CATEGORY_NAME]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static DocumentCategories load( long m_ID)
	{
		DocumentCategories obj  = new DocumentCategories();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static DocumentCategories[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		DocumentCategories obj = new DocumentCategories();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new DocumentCategories();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (DocumentCategories[])list.ToArray(typeof(DocumentCategories));
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
			string[] values = {m_CategoryName, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_CategoryName};
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

	public string CategoryName {
		set{ m_CategoryName = value;
		OnPropertyChanged("CategoryName"); }
		get { return m_CategoryName; }
	}

    public string Name
    {
        get { return m_CategoryName; }
    }
}
}
