using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class JournalNames : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_JournalNames";

	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_JOURNAL_NAME = "JournalName";
	    public const string SQL_COL_DESCRIPTION = "Description";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_JOURNAL_NAME + "]," +
                "[" + SQL_COL_DESCRIPTION + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_JOURNAL_NAME + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]" +
			    " FROM " + TABLE + " ORDER BY " + SQL_COL_JOURNAL_NAME;

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_JOURNAL_NAME + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]" +
			    ") VALUES ('?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_JOURNAL_NAME + "]='?'," +
			    "[" + SQL_COL_DESCRIPTION + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_BY_NAME = "SELECT " +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_JOURNAL_NAME + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_JOURNAL_NAME + "] = '?';";

	    protected int m_Id;
	    protected string m_JournalName;
	    protected string m_Description;
        protected bool m_Delete = false;
	    protected bool m_isChanged;

	    public JournalNames() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public JournalNames(int Id,string JournalName,string Description) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Id = Id;
		    m_JournalName = JournalName;
		    m_Description = Description;
	    }

	    protected JournalNames fill(DataRow dr)
	    {
		    try {
			    m_Id = (int) dr[SQL_COL_ID];
			    m_JournalName = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL_NAME]) ? "" : dr[SQL_COL_JOURNAL_NAME]);
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static JournalNames load( int m_Id)
	    {
		    JournalNames obj  = new JournalNames();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static JournalNames[] load(bool addBlank = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    JournalNames obj = new JournalNames();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
            if (addBlank)
                list.Add(new JournalNames());
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new JournalNames();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (JournalNames[])list.ToArray(typeof(JournalNames));
		    } catch (SqlException ex) {
			    return null;
		    }
	    }

        public static JournalNames load(string name)
        {
            ArrayList list = new ArrayList();
            string[] opt = {name};

            DataSet ds = new DataSet();
            JournalNames obj = new JournalNames();

            ds = obj.load(SQL_LOAD_BY_NAME, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = new JournalNames();
            obj.fill(ds.Tables[0].Rows[0]);
            try
            {
                return obj;
            }
            catch (Exception ex)
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
			    string[] values = {m_JournalName, m_Description, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_JournalName, m_Description};
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
	    public int Id {
		    get { return m_Id; }
	    }

	    public string JournalName {
		    set{ m_JournalName = value;
		    OnPropertyChanged("JournalName"); }
		    get { return m_JournalName; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

        public bool Delete {
            get { return m_Delete; }
            set { m_Delete = value;
                OnPropertyChanged("Delete"); }
        }

        /// <summary>
		/// Has record been modified?
		/// </summary>
		public bool IsChanged
		{ 
			get { return m_isChanged; }
			set { m_isChanged = value; }
        }
    }
}
