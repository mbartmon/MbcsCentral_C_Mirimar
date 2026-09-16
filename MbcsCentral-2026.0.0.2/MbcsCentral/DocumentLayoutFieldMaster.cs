using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class DocumentLayoutFieldMaster : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "DocumentLayoutFieldMaster";

	public const string SQL_COL_SIZE = "Size";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CATEGORY_ID = "CategoryId";
	public const string SQL_COL_FIELD_NAME = "FieldName";
	public const string SQL_COL_DESCRIPTION = "Description";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_SIZE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_FIELD_NAME + "]," +
			"[" + SQL_COL_DESCRIPTION + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_SIZE + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_FIELD_NAME + "]," +
			"[" + SQL_COL_DESCRIPTION + "]" +
			" FROM " + TABLE +  
            " ORDER BY" + SQL_COL_FIELD_NAME + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_SIZE + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_FIELD_NAME + "]," +
			"[" + SQL_COL_DESCRIPTION + "]" +
			") VALUES (?, ?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_SIZE + "]=?," +
			"[" + SQL_COL_CATEGORY_ID + "]=?," +
			"[" + SQL_COL_FIELD_NAME + "]='?'," +
			"[" + SQL_COL_DESCRIPTION + "]='?'" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

    static string SQL_LOAD_BY_CATEGORY = "SELECT " +
            "[" + SQL_COL_SIZE + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_CATEGORY_ID + "]," +
            "[" + SQL_COL_FIELD_NAME + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_CATEGORY_ID + "] = ?" +
            " ORDER BY " + SQL_COL_FIELD_NAME;

    protected int m_Size;
	protected int m_ID;
	protected int m_CategoryId;
	protected string m_FieldName;
	protected string m_Description;
	protected bool m_isChanged;

	public DocumentLayoutFieldMaster() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public DocumentLayoutFieldMaster(int Size,int ID,int CategoryId,string FieldName,string Description) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Size = Size;
		m_ID = ID;
		m_CategoryId = CategoryId;
		m_FieldName = FieldName;
		m_Description = Description;
	}

	protected DocumentLayoutFieldMaster fill(DataRow dr)
	{
		try {
			m_Size = (int) dr[SQL_COL_SIZE];
			m_ID = (int) dr[SQL_COL_I_D];
			m_CategoryId = (int) dr[SQL_COL_CATEGORY_ID];
			m_FieldName = (string) (Convert.IsDBNull(dr[SQL_COL_FIELD_NAME]) ? "" : dr[SQL_COL_FIELD_NAME]);
			m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static DocumentLayoutFieldMaster load( long m_ID)
	{
		DocumentLayoutFieldMaster obj  = new DocumentLayoutFieldMaster();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static DocumentLayoutFieldMaster[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		DocumentLayoutFieldMaster obj = new DocumentLayoutFieldMaster();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new DocumentLayoutFieldMaster();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (DocumentLayoutFieldMaster[])list.ToArray(typeof(DocumentLayoutFieldMaster));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static DocumentLayoutFieldMaster[] loadByCategory(long CatId)
    {
        ArrayList list = new ArrayList();

        string[] opt = { CatId.ToString() };
        DataSet ds = new DataSet();
        DocumentLayoutFieldMaster obj = new DocumentLayoutFieldMaster();

        ds = obj.load(SQL_LOAD_BY_CATEGORY, opt);
        if (ds == null)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new DocumentLayoutFieldMaster();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (DocumentLayoutFieldMaster[])list.ToArray(typeof(DocumentLayoutFieldMaster));
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
		if (m_ID > 0 ) {
			string[] values = {m_Size.ToString(), m_CategoryId.ToString(), m_FieldName, m_Description, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Size.ToString(), m_CategoryId.ToString(), m_FieldName, m_Description};
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
	public int Size {
		set{ m_Size = value;
		OnPropertyChanged("Size"); }
		get { return m_Size; }
	}

	public int ID {
		get { return m_ID; }
	}

	public int CategoryId {
		set{ m_CategoryId = value;
		OnPropertyChanged("CategoryId"); }
		get { return m_CategoryId; }
	}

	public string FieldName {
		set{ m_FieldName = value;
		OnPropertyChanged("FieldName"); }
		get { return m_FieldName; }
	}

	public string Description {
		set{ m_Description = value;
		OnPropertyChanged("Description"); }
		get { return m_Description; }
	}

    public string FullDescription
    {
        get { return m_FieldName + ": " + m_Description; }
    }

}
}
