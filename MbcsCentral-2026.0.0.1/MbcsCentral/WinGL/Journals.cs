using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class Journals : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Journals";

	    public const string SQL_COL_COLUMN = "Column";
	    public const string SQL_COL_FIELD = "Field";
	    public const string SQL_COL_TYPE = "Type";
	    public const string SQL_COL_SIZE = "Size";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_RANGE = "Range";
	    public const string SQL_COL_PRINT_FLAG = "PrintFlag";
	    public const string SQL_COL_LAYOUT = "Layout";
	    public const string SQL_COL_HEADING = "Heading";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_LAYOUT + "]," +
                "[" + SQL_COL_COLUMN + "]," +
                "[" + SQL_COL_FIELD + "]," +
                "[" + SQL_COL_RANGE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_HEADING + "]," +
                "[" + SQL_COL_PRINT_FLAG + "]," +
                "[" + SQL_COL_SIZE + "]," +
                "[" + SQL_COL_ID + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_LAYOUT + "]," +
                "[" + SQL_COL_COLUMN + "]," +
                "[" + SQL_COL_FIELD + "]," +
                "[" + SQL_COL_RANGE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_HEADING + "]," +
                "[" + SQL_COL_PRINT_FLAG + "]," +
                "[" + SQL_COL_SIZE + "]," +
                "[" + SQL_COL_ID + "]" +
                " FROM " + TABLE + ";";

            static string SQL_LOAD_BY_LAYOUT = "SELECT " +
                    "[" + SQL_COL_LAYOUT + "]," +
                    "[" + SQL_COL_COLUMN + "]," +
                    "[" + SQL_COL_FIELD + "]," +
                    "[" + SQL_COL_RANGE + "]," +
                    "[" + SQL_COL_TYPE + "]," +
                    "[" + SQL_COL_HEADING + "]," +
                    "[" + SQL_COL_PRINT_FLAG + "]," +
                    "[" + SQL_COL_SIZE + "]," +
                    "[" + SQL_COL_ID + "]" +
                    " FROM " + TABLE +
                    " WHERE " + "[" + SQL_COL_LAYOUT + "] = '?'" +
                    " ORDER BY [" + SQL_COL_COLUMN + "]";

            static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_COLUMN + "]," +
			    "[" + SQL_COL_FIELD + "]," +
			    "[" + SQL_COL_TYPE + "]," +
			    "[" + SQL_COL_SIZE + "]," +
			    "[" + SQL_COL_RANGE + "]," +
			    "[" + SQL_COL_PRINT_FLAG + "]," +
			    "[" + SQL_COL_LAYOUT + "]," +
			    "[" + SQL_COL_HEADING + "]" +
			    ") VALUES (?, ?, ?, ?, ?, ?, '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_COLUMN + "]=?," +
			    "[" + SQL_COL_FIELD + "]=?," +
			    "[" + SQL_COL_TYPE + "]=?," +
			    "[" + SQL_COL_SIZE + "]=?," +
			    "[" + SQL_COL_RANGE + "]=?," +
			    "[" + SQL_COL_PRINT_FLAG + "]=?," +
			    "[" + SQL_COL_LAYOUT + "]='?'," +
			    "[" + SQL_COL_HEADING + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    protected short m_Column;
	    protected short m_Field;
	    protected short m_Type;
	    protected short m_Size;
	    protected int m_Id;
	    protected bool m_Range;
	    protected bool m_PrintFlag;
	    protected string m_Layout;
	    protected string m_Heading;
	    protected bool m_isChanged;

        static int m_ColumnCount;

        public static string[] Columns = { "Date", "AccountDescription", "Amount", "TransactionDescription", "CheckNumber", "Source", "DepartmentName", "AccouontNumber", "DepartmentNumber" };
        public static string[] LayoutColumns = { SQL_COL_LAYOUT, SQL_COL_COLUMN, SQL_COL_FIELD, SQL_COL_RANGE, SQL_COL_TYPE, SQL_COL_HEADING, SQL_COL_PRINT_FLAG, SQL_COL_SIZE, SQL_COL_ID };

        public Journals() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Journals(short Column,short Field,short Type,short Size,int Id,bool Range,bool PrintFlag,string Layout,string Heading) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Column = Column;
		    m_Field = Field;
		    m_Type = Type;
		    m_Size = Size;
		    m_Id = Id;
		    m_Range = Range;
		    m_PrintFlag = PrintFlag;
		    m_Layout = Layout;
		    m_Heading = Heading;
	    }

	    protected Journals fill(DataRow dr, int columncount = 0)
	    {
		    try {
			    m_Column = (short) dr[SQL_COL_COLUMN];
			    m_Field = (short) dr[SQL_COL_FIELD];
			    m_Type = (short) dr[SQL_COL_TYPE];
			    m_Size = (short) dr[SQL_COL_SIZE];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_Range = (bool) dr[SQL_COL_RANGE];
			    m_PrintFlag = (bool) dr[SQL_COL_PRINT_FLAG];
			    m_Layout = (string) (Convert.IsDBNull(dr[SQL_COL_LAYOUT]) ? "" : dr[SQL_COL_LAYOUT]);
			    m_Heading = (string) (Convert.IsDBNull(dr[SQL_COL_HEADING]) ? "" : dr[SQL_COL_HEADING]);
                ColumnCount = columncount;
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Journals load( int m_Id)
	    {
		    Journals obj  = new Journals();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Journals[] load(bool firstRowOnly = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Journals obj = new Journals();

		    ds = obj.loadAll();
		    if (ds == null || ds.Tables[0].Rows.Count == 0)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Journals();
			    obj.fill(dr);
                if (firstRowOnly && obj.Column != 0)
                    continue;
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Journals[])list.ToArray(typeof(Journals));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

            public static Journals[] load(string layout)
            {
                ArrayList list = new ArrayList();
                string[] opts = { layout };

                DataSet ds = new DataSet();
                Journals obj = new Journals();

                ds = obj.load(SQL_LOAD_BY_LAYOUT, opts);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                obj = null;
                m_ColumnCount = ds.Tables[0].Columns.Count;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new Journals();
                    obj.fill(dr, m_ColumnCount);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (Journals[])list.ToArray(typeof(Journals));
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
		    if (m_Id > 0 ) {
			    string[] values = {m_Column.ToString(), m_Field.ToString(), m_Type.ToString(), m_Size.ToString(), m_Range.ToString(), m_PrintFlag.ToString(), m_Layout, m_Heading, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Column.ToString(), m_Field.ToString(), m_Type.ToString(), m_Size.ToString(), m_Range.ToString(), m_PrintFlag.ToString(), m_Layout, m_Heading};
			    m_Id = this.insertRecord(values);
			    if (m_Id >= 0) {
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
	    public short Column {
		    set{ m_Column = value;
		    OnPropertyChanged("Column"); }
		    get { return m_Column; }
	    }

	    public short Field {
		    set{ m_Field = value;
		    OnPropertyChanged("Field"); }
		    get { return m_Field; }
	    }

	    public short Type {
		    set{ m_Type = value;
		    OnPropertyChanged("Type"); }
		    get { return m_Type; }
	    }

	    public short Size {
		    set{ m_Size = value;
		    OnPropertyChanged("Size"); }
		    get { return m_Size; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public bool Range {
		    set{ m_Range = value;
		    OnPropertyChanged("Range"); }
		    get { return m_Range; }
	    }

	    public bool PrintFlag {
		    set{ m_PrintFlag = value;
		    OnPropertyChanged("PrintFlag"); }
		    get { return m_PrintFlag; }
	    }

	    public string Layout {
		    set{ m_Layout = value;
		    OnPropertyChanged("Layout"); }
		    get { return m_Layout; }
	    }

	    public string Heading {
		    set{ m_Heading = value;
		    OnPropertyChanged("Heading"); }
		    get { return m_Heading; }
	    }

        public string Item
        {
            get
            {
                return WinglGlobal.JournalFields[Field];
            }
        }
}
}
