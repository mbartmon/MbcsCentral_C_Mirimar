using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral {
    public class Classes : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "Classes";

	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_CLIENT_ID = "clientId";
	    public const string SQL_COL_ACCT_ID = "acctId";
	    public const string SQL_COL_NAME = "name";
	    public const string SQL_COL_VALUE = "value";

	    static string SQL_LOAD = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_CLIENT_ID + "]," +
			    "[" + SQL_COL_ACCT_ID + "]," +
			    "[" + SQL_COL_NAME + "]," +
			    "[" + SQL_COL_VALUE + "]" +
			    " FROM " + TABLE +
			    " WHERE " + SQL_COL_I_D + " = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_CLIENT_ID + "]," +
			    "[" + SQL_COL_ACCT_ID + "]," +
			    "[" + SQL_COL_NAME + "]," +
			    "[" + SQL_COL_VALUE + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_CLIENT_ID + "]," +
			    "[" + SQL_COL_ACCT_ID + "]," +
			    "[" + SQL_COL_NAME + "]," +
			    "[" + SQL_COL_VALUE + "]" +
			    ") VALUES (?, ?, '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_CLIENT_ID + "]=?," +
			    "[" + SQL_COL_ACCT_ID + "]=?," +
			    "[" + SQL_COL_NAME + "]='?'," +
			    "[" + SQL_COL_VALUE + "]='?'" +
			    " WHERE " + SQL_COL_I_D + " = ?;";

        static string SQL_LOAD_BY_KEYS = "SELECT " +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_ACCT_ID + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_VALUE + "]" +
                " FROM " + TABLE +
                " WHERE [" + SQL_COL_ACCT_ID + "] = ?" +
                " AND [" + SQL_COL_NAME + "] = '?'";

        static string SQL_LOAD_BY_CLIENT = "SELECT " +
                    "[" + SQL_COL_I_D + "]," +
                    "[" + SQL_COL_CLIENT_ID + "]," +
                    "[" + SQL_COL_ACCT_ID + "]," +
                    "[" + SQL_COL_NAME + "]," +
                    "[" + SQL_COL_VALUE + "]" +
                    " FROM " + TABLE +
                    " WHERE [" + SQL_COL_CLIENT_ID + "] = ?" +
                    " AND [" + SQL_COL_NAME + "] = '?'";

        static string SQL_LOAD_MULTIPLE = "SELECT " +
               "[" + SQL_COL_I_D + "]," +
               "[" + SQL_COL_CLIENT_ID + "]," +
               "[" + SQL_COL_ACCT_ID + "]," +
               "[" + SQL_COL_NAME + "]," +
               "[" + SQL_COL_VALUE + "]" +
               " FROM " + TABLE +
               " WHERE [" + SQL_COL_ACCT_ID + "] = ?" +
               " AND [" + SQL_COL_NAME + "] LIKE '%?%'";

        protected long m_ID;
	    protected int m_clientId;
	    protected long m_acctId;
	    protected string m_name;
	    protected string m_value;
	    protected bool m_isChanged;

	    public Classes() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Classes(long ID,int clientId,long acctId,string name,string value) : 
			    base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_ID = ID;
		    m_clientId = clientId;
		    m_acctId = acctId;
		    m_name = name;
		    m_value = value;
	    }

	    protected Classes fill(DataRow dr)
	    {
		    try {
			    m_ID = (long) dr[SQL_COL_I_D];
			    m_clientId = (int) dr[SQL_COL_CLIENT_ID];
			    m_acctId = (long) dr[SQL_COL_ACCT_ID];
			    m_name = (string) (Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
			    m_value = (string) (Convert.IsDBNull(dr[SQL_COL_VALUE]) ? "" : dr[SQL_COL_VALUE]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Classes load( long m_ID)
	    {
		    Classes obj  = new Classes();
		    string[] opt = {m_ID.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Classes[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Classes obj = new Classes();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Classes();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Classes[])list.ToArray(typeof(Classes));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static Classes load(long acctId, string name)
        {
            DataSet ds = new DataSet();
            Classes obj = new Classes();

            string[] opt = { acctId.ToString(), name };

            ds = obj.load(SQL_LOAD_BY_KEYS, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            obj = new Classes();
            obj.fill(ds.Tables[0].Rows[0]);
            if (obj != null)
                return obj;
            else
                return null;

        }

        /// <summary>
        /// Load using object and optional acctid
        /// </summary>
        /// <param name="cls">Name of class</param>
        /// <param name="acctId">Default to 0 for global class object</param>
        /// <returns></returns>
        public static Classes load(object cls, int acctId = 0)
        {
            DataSet ds = new DataSet();
            Classes obj = new Classes();

            string[] opt = { acctId.ToString(), cls.GetType().Name };

            ds = obj.load(SQL_LOAD_BY_KEYS, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            obj = new Classes();
            obj.fill(ds.Tables[0].Rows[0]);
            if (obj != null)
                return obj;
            else
                return null;

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads multiple classes with the same base name. </summary>
        ///
        /// <remarks>   Mike, 2017-03-12. </remarks>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="acctId">   (Optional)
        ///                         Default to 0 for global class object. </param>
        ///
        /// <returns>   An array of classes. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static Classes[] loadMultiple(string name, int acctId = 0)
        {
            DataSet ds = new DataSet();
            Classes obj = new Classes();

            ArrayList list = new ArrayList();

            string[] opt = { acctId.ToString(), name };

            ds = obj.load(SQL_LOAD_MULTIPLE, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = null;
                obj = new Classes();
                obj.fill(dr);
                list.Add(obj);
            }
            try
            {
                return (Classes[])list.ToArray(typeof(Classes));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }


        public static Classes loadByClient(int clientId, string name)
        {
            DataSet ds = new DataSet();
            Classes obj = new Classes();

            string[] opt = { clientId.ToString(), name };

            ds = obj.load(SQL_LOAD_BY_CLIENT, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            obj = new Classes();
            obj.fill(ds.Tables[0].Rows[0]);
            if (obj != null)
                return obj;
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
			    string[] values = {m_clientId.ToString(), m_acctId.ToString(), m_name, m_value, m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_clientId.ToString(), m_acctId.ToString(), m_name, m_value};
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
	    public long ID {
		    get { return m_ID; }
            set { m_ID = value; }
	    }

	    public int clientId {
		    set{ m_clientId = value;
		    OnPropertyChanged("clientId"); }
		    get { return m_clientId; }
	    }

	    public long acctId {
		    set{ m_acctId = value;
		    OnPropertyChanged("acctId"); }
		    get { return m_acctId; }
	    }

	    public string name {
		    set{ m_name = value;
		    OnPropertyChanged("name"); }
		    get { return m_name; }
	    }

	    public string value {
		    set{ m_value = value;
		    OnPropertyChanged("value"); }
		    get { return m_value; }
	    }

    }
}
