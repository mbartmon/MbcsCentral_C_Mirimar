using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class BatchJrnHeader : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_BatchJrnHeader";

	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_COMPANY = "Company";
	    public const string SQL_COL_BATCH_NAME = "BatchName";
	    public const string SQL_COL_JOURNAL = "Journal";
	    public const string SQL_COL_SOURCE = "Source";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_BATCH_NAME + "]," +
                "[" + SQL_COL_JOURNAL + "]," +
                "[" + SQL_COL_SOURCE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_BATCH_NAME + "]," +
			    "[" + SQL_COL_JOURNAL + "]," +
			    "[" + SQL_COL_SOURCE + "]" +
			    " FROM " + TABLE +
                " ORDER BY " + SQL_COL_BATCH_NAME;

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_BATCH_NAME + "]," +
			    "[" + SQL_COL_JOURNAL + "]," +
			    "[" + SQL_COL_SOURCE + "]" +
			    ") VALUES (?, '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_BATCH_NAME + "]='?'," +
			    "[" + SQL_COL_JOURNAL + "]='?'," +
			    "[" + SQL_COL_SOURCE + "]='?'" +
			    " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    protected int m_Key;
	    protected short m_Company;
	    protected string m_BatchName;
	    protected string m_Journal;
	    protected string m_Source;
	    protected bool m_isChanged;

	    public BatchJrnHeader() : base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public BatchJrnHeader(int Key,short Company,string BatchName,string Journal,string Source) : 
			    base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Key = Key;
		    m_Company = Company;
		    m_BatchName = BatchName;
		    m_Journal = Journal;
		    m_Source = Source;
	    }

	    protected BatchJrnHeader fill(DataRow dr)
	    {
		    try {
			    m_Key = (int) dr[SQL_COL_KEY];
                try
                {
                    m_Company = (short)dr[SQL_COL_COMPANY];
                }
                catch
                {
                    int cmp = (int)dr[SQL_COL_COMPANY];
                    try
                    {
                        m_Company = (short)cmp;
                    }
                    catch
                    {
                        m_Company = 0;
                    }
                }
			    m_BatchName = (string) (Convert.IsDBNull(dr[SQL_COL_BATCH_NAME]) ? "" : dr[SQL_COL_BATCH_NAME]);
			    m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
			    m_Source = (string) (Convert.IsDBNull(dr[SQL_COL_SOURCE]) ? "" : dr[SQL_COL_SOURCE]);
		    } catch (Exception ex) {
		    }
		    return this;

	    }

	    public static BatchJrnHeader load( int m_Key)
	    {
		    BatchJrnHeader obj  = new BatchJrnHeader();
		    string[] opt = {m_Key.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static BatchJrnHeader[] load(bool incudeBlankLine = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    BatchJrnHeader obj = new BatchJrnHeader();

		    ds = obj.loadAll();
		    if (ds == null || ds.Tables[0].Rows.Count == 0)
			    return null;
		    obj = null;
            if (incudeBlankLine)
                {
                    obj = new BatchJrnHeader();
                    list.Add(obj);
                }
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new BatchJrnHeader();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (BatchJrnHeader[])list.ToArray(typeof(BatchJrnHeader));
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
		    if (m_Key > 0 ) {
			    string[] values = {m_Company.ToString(), m_BatchName, m_Journal, m_Source, m_Key.ToString()};
			    if (this.updateRecord(m_Key.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Company.ToString(), m_BatchName, m_Journal, m_Source};
			    m_Key = this.insertRecord(values);
			    if (m_Key >= 0) {
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
	    public int Key {
		    get { return m_Key; }
	    }

	    public short Company {
		    set{ m_Company = value;
		    OnPropertyChanged("Company"); }
		    get { return m_Company; }
	    }

	    public string BatchName {
		    set{ m_BatchName = value;
		    OnPropertyChanged("BatchName"); }
		    get { return m_BatchName; }
	    }

	    public string Journal {
		    set{ m_Journal = value;
		    OnPropertyChanged("Journal"); }
		    get { return m_Journal; }
	    }

	    public string Source {
		    set{ m_Source = value;
		    OnPropertyChanged("Source"); }
		    get { return m_Source; }
	    }

    }
}
