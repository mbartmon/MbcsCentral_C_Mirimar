using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
public class DocumentLayoutFields : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "DocumentLayoutFields";

	public const string SQL_COL_SIZE = "Size";
	public const string SQL_COL_LEFT = "Left";
	public const string SQL_COL_TOP = "Top";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_DOCUMENT_ID = "DocumentId";
	public const string SQL_COL_FIELD_ID = "FieldId";
    public const string SQL_COL_CLASS = "Class";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_SIZE + "]," +
            "[" + SQL_COL_LEFT + "]," +
            "[" + SQL_COL_TOP + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_DOCUMENT_ID + "]," +
            "[" + SQL_COL_FIELD_ID + "]," +
            "[" + SQL_COL_CLASS + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

    static string SQL_LOAD_ALL = "SELECT " +
            "[" + SQL_COL_SIZE + "]," +
            "[" + SQL_COL_LEFT + "]," +
            "[" + SQL_COL_TOP + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_DOCUMENT_ID + "]," +
            "[" + SQL_COL_FIELD_ID + "]," +
            "[" + SQL_COL_CLASS + "]" +
            " FROM " + TABLE + ";";

    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
            "[" + SQL_COL_SIZE + "]," +
            "[" + SQL_COL_LEFT + "]," +
            "[" + SQL_COL_TOP + "]," +
            "[" + SQL_COL_DOCUMENT_ID + "]," +
            "[" + SQL_COL_FIELD_ID + "]," +
            "[" + SQL_COL_CLASS + "]" +
            ") VALUES (?, ?, ?, ?, ?, '?');";

    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
            "[" + SQL_COL_SIZE + "]=?," +
            "[" + SQL_COL_LEFT + "]=?," +
            "[" + SQL_COL_TOP + "]=?," +
            "[" + SQL_COL_DOCUMENT_ID + "]=?," +
            "[" + SQL_COL_FIELD_ID + "]=?," +
            "[" + SQL_COL_CLASS + "]='?'" +
            " WHERE " + "[" + SQL_COL_I_D + "] = ?;";
    static string SQL_LOAD_BY_DOCUMENT = "SELECT " +
            "[" + SQL_COL_SIZE + "]," +
            "[" + SQL_COL_LEFT + "]," +
            "[" + SQL_COL_TOP + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_DOCUMENT_ID + "]," +
            "[" + SQL_COL_FIELD_ID + "]," +
            "[" + SQL_COL_CLASS + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_DOCUMENT_ID + "] = ?" + 
            " ORDER BY [" + SQL_COL_TOP + "];";

    protected int m_Size;
	protected decimal m_Left;
	protected decimal m_Top;
	protected int m_ID;
	protected int m_DocumentId;
	protected int m_FieldId;
	protected bool m_isChanged;
    protected string m_Class;

	public DocumentLayoutFields() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

    public DocumentLayoutFields(int Size, decimal Left, decimal Top, int ID, int DocumentId, int FieldId, string Class) :
        base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
    {
        m_Size = Size;
        m_Left = Left;
        m_Top = Top;
        m_ID = ID;
        m_DocumentId = DocumentId;
        m_FieldId = FieldId;
        m_Class = Class;
    }

    protected DocumentLayoutFields fill(DataRow dr)
    {
        try
        {
            m_Size = (int)dr[SQL_COL_SIZE];
            m_Left = (decimal)dr[SQL_COL_LEFT];
            m_Top = (decimal)dr[SQL_COL_TOP];
            m_ID = (int)dr[SQL_COL_I_D];
            m_DocumentId = (int)dr[SQL_COL_DOCUMENT_ID];
            m_FieldId = (int)dr[SQL_COL_FIELD_ID];
            m_Class = (string)(Convert.IsDBNull(dr[SQL_COL_CLASS]) ? "" : dr[SQL_COL_CLASS]);
        }
        catch (SqlException ex)
        {
        }
        return this;

    }
	public static DocumentLayoutFields load( long m_ID)
	{
		DocumentLayoutFields obj  = new DocumentLayoutFields();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static DocumentLayoutFields[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		DocumentLayoutFields obj = new DocumentLayoutFields();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new DocumentLayoutFields();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (DocumentLayoutFields[])list.ToArray(typeof(DocumentLayoutFields));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static DocumentLayoutFields[] loadByDocument(long DocId)
    {
        ArrayList list = new ArrayList();

        string[] opt = { DocId.ToString() };
        DataSet ds = new DataSet();
        DocumentLayoutFields obj = new DocumentLayoutFields();

        ds = obj.load(SQL_LOAD_BY_DOCUMENT, opt);
        if (ds == null)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new DocumentLayoutFields();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (DocumentLayoutFields[])list.ToArray(typeof(DocumentLayoutFields));
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
        if (m_ID > 0)
        {
            string[] values = { m_Size.ToString(), m_Left.ToString(), m_Top.ToString(), m_DocumentId.ToString(), m_FieldId.ToString(), m_Class, m_ID.ToString() };
            if (this.updateRecord(m_ID.ToString(), values))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            string[] values = { m_Size.ToString(), m_Left.ToString(), m_Top.ToString(), m_DocumentId.ToString(), m_FieldId.ToString(), m_Class };
            m_ID = this.insertRecord(values);
            if (m_ID >= 0)
            {
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

	public decimal Left {
		set{ m_Left = value;
		OnPropertyChanged("Left"); }
		get { return m_Left; }
	}

	public decimal Top {
		set{ m_Top = value;
		OnPropertyChanged("Top"); }
		get { return m_Top; }
	}

	public int ID {
		get { return m_ID; }
        set { m_ID = value; }
	}

	public int DocumentId {
		set{ m_DocumentId = value;
		OnPropertyChanged("DocumentId"); }
		get { return m_DocumentId; }
	}

	public int FieldId {
		set{ m_FieldId = value;
		OnPropertyChanged("FieldId"); }
		get { return m_FieldId; }
	}
    public string Class
    {
        set
        {
            m_Class = value;
            OnPropertyChanged("Class");
        }
        get { return m_Class; }
    }
    public int Index
    {
        get;
        set;
    }

}
}
