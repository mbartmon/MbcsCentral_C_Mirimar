using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class DocumentLayoutList : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "DocumentLayoutList";

	public const string SQL_COL_PAGES = "Pages";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_CATEGORY_ID = "CategoryId";
	public const string SQL_COL_DOCUMENT_NAME = "DocumentName";
	public const string SQL_COL_DOCUMENT_LOCATION = "DocumentLocation";
	public const string SQL_COL_DEFAULT_FONT = "DefaultFont";
	public const string SQL_COL_DOCUMENT_CLASS = "DocumentClass";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_PAGES + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_DOCUMENT_NAME + "]," +
			"[" + SQL_COL_DOCUMENT_LOCATION + "]," +
			"[" + SQL_COL_DEFAULT_FONT + "]," +
			"[" + SQL_COL_DOCUMENT_CLASS + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_PAGES + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_DOCUMENT_NAME + "]," +
			"[" + SQL_COL_DOCUMENT_LOCATION + "]," +
			"[" + SQL_COL_DEFAULT_FONT + "]," +
			"[" + SQL_COL_DOCUMENT_CLASS + "]"+
			" FROM " + TABLE +
			" ORDER BY " + SQL_COL_DOCUMENT_NAME + ";";

        static string SQL_LOAD_BY_CATEGORY = "SELECT " +
        "[" + SQL_COL_PAGES + "]," +
        "[" + SQL_COL_I_D + "]," +
        "[" + SQL_COL_CATEGORY_ID + "]," +
        "[" + SQL_COL_DOCUMENT_NAME + "]," +
        "[" + SQL_COL_DOCUMENT_LOCATION + "]," +
        "[" + SQL_COL_DEFAULT_FONT + "]," +
		"[" + SQL_COL_DOCUMENT_CLASS + "]"+
        " FROM " + TABLE + 
        " WHERE [" + SQL_COL_CATEGORY_ID + "] IN (?)" +
		" ORDER BY [" + SQL_COL_DOCUMENT_NAME + "];";


        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_PAGES + "]," +
			"[" + SQL_COL_CATEGORY_ID + "]," +
			"[" + SQL_COL_DOCUMENT_NAME + "]," +
			"[" + SQL_COL_DOCUMENT_LOCATION + "]," +
			"[" + SQL_COL_DEFAULT_FONT + "]," +
			"[" + SQL_COL_DOCUMENT_CLASS +	"]" +
			") VALUES (?, ?, '?', '?', '?', ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_PAGES + "]=?," +
			"[" + SQL_COL_CATEGORY_ID + "]=?," +
			"[" + SQL_COL_DOCUMENT_NAME + "]='?'," +
			"[" + SQL_COL_DOCUMENT_LOCATION + "]='?'," +
			"[" + SQL_COL_DEFAULT_FONT + "]='?'," +
			"[" + SQL_COL_DOCUMENT_CLASS + "] = ?" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

    static string SQL_LOAD_BY_DOCUMENT = "SELECT " +
            "[" + SQL_COL_PAGES + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_CATEGORY_ID + "]," +
            "[" + SQL_COL_DOCUMENT_NAME + "]," +
            "[" + SQL_COL_DOCUMENT_LOCATION + "]," +
            "[" + SQL_COL_DEFAULT_FONT + "]," +
			"[" + SQL_COL_DOCUMENT_CLASS + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_DOCUMENT_NAME + "] = '?';";

    protected int m_Pages;
	protected int m_ID;
	protected int m_CategoryId;
	protected string m_DocumentName;
	protected string m_DocumentLocation;           
	protected string m_DefaultFont;
	protected int m_DocumentClass = 0;
	protected bool m_isChanged;

	public DocumentLayoutList() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public DocumentLayoutList(int Pages,int ID,int CategoryId,string DocumentName,string DocumentLocation,string DefaultFont) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Pages = Pages;
		m_ID = ID;
		m_CategoryId = CategoryId;
		m_DocumentName = DocumentName;
		m_DocumentLocation = DocumentLocation;
		m_DefaultFont = DefaultFont;
	}

	protected DocumentLayoutList fill(DataRow dr)
	{
		try {
			m_Pages = (int) dr[SQL_COL_PAGES];
			m_ID = (int) dr[SQL_COL_I_D];
			m_CategoryId = (int) dr[SQL_COL_CATEGORY_ID];
			m_DocumentName = (string) (Convert.IsDBNull(dr[SQL_COL_DOCUMENT_NAME]) ? "" : dr[SQL_COL_DOCUMENT_NAME]);
			m_DocumentLocation = (string) (Convert.IsDBNull(dr[SQL_COL_DOCUMENT_LOCATION]) ? "" : dr[SQL_COL_DOCUMENT_LOCATION]);
			m_DefaultFont = (string) (Convert.IsDBNull(dr[SQL_COL_DEFAULT_FONT]) ? "" : dr[SQL_COL_DEFAULT_FONT]);
			m_DocumentClass = (int)(Convert.IsDBNull(dr[SQL_COL_DOCUMENT_CLASS]) ? 0 : dr[SQL_COL_DOCUMENT_CLASS]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static DocumentLayoutList load( long m_ID)
	{
		DocumentLayoutList obj  = new DocumentLayoutList();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static DocumentLayoutList[] load(bool MainOnly = false, bool UseNone = false)
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		DocumentLayoutList obj = new DocumentLayoutList();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
        if (UseNone)
        {
            list.Add(new DocumentLayoutList() { DocumentName = " " });
        }
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new DocumentLayoutList();
            if (MainOnly)
            {
                string s = obj.DocumentName;
                if (s != null && s.IndexOf(".") < 0)
                    continue;
            }
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (DocumentLayoutList[])list.ToArray(typeof(DocumentLayoutList));
		} catch (SqlException ex) {
			return null;
		}
        return null;
	}

    public static DocumentLayoutList[] LoadByCategory(string cats, bool UseNone = false)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        DocumentLayoutList obj = new DocumentLayoutList();
        string[] opts = { cats };

        ds = obj.load(SQL_LOAD_BY_CATEGORY, opts);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        obj = null;
        if (UseNone)
        {
            list.Add(new DocumentLayoutList() { DocumentName = " " });
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new DocumentLayoutList();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (DocumentLayoutList[])list.ToArray(typeof(DocumentLayoutList));
        }
        catch (SqlException ex)
        {
            return null;
        }
        return null;
    }



    public static DocumentLayoutList loadByDocument(string name)
    {
        DocumentLayoutList obj = new DocumentLayoutList();
        string[] opt = { name};
        DataSet ds = new DataSet();
        ds = obj.load(SQL_LOAD_BY_DOCUMENT, opt);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
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
		if (m_ID > 0 ) {
			string[] values = {m_Pages.ToString(), m_CategoryId.ToString(), m_DocumentName, m_DocumentLocation, m_DefaultFont, m_DocumentClass.ToString(), m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Pages.ToString(), m_CategoryId.ToString(), m_DocumentName, m_DocumentLocation, m_DefaultFont, m_DocumentClass.ToString()};
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
	public int Pages {
		set{ m_Pages = value;
		OnPropertyChanged("Pages"); }
		get { return m_Pages; }
	}

	public int ID {
		get { return m_ID; }
	}

	public int CategoryId {
		set{ m_CategoryId = value;
		OnPropertyChanged("CategoryId"); }
		get { return m_CategoryId; }
	}

	public string DocumentName {
		set{ m_DocumentName = value;
		OnPropertyChanged("DocumentName"); }
		get { return m_DocumentName; }
	}

	public string DocumentLocation {
		set{ m_DocumentLocation = value;
		OnPropertyChanged("DocumentLocation"); }
		get { return m_DocumentLocation; }
	}

	public string DefaultFont {
		set{ m_DefaultFont = value;
		OnPropertyChanged("DefaultFont"); }
		get { return m_DefaultFont; }
	}

		public int DocumentClass 
		{ 
			set { m_DocumentClass = value; 
			      OnPropertyChanged("DocumentClass"); } 
            get { return (int)m_DocumentClass; }
		}

}
}
