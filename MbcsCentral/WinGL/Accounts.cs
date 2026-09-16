using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// General Ledger Account Class
    /// </summary>
    [Serializable]
    public class Accounts : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Accounts";

	    public const string SQL_COL_ACCOUNT_NUMBER = "Account Number";
	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_ACTIVE = "active";
	    public const string SQL_COL_MAJOR_CATEGORY = "MajorCategory";
	    public const string SQL_COL_MINOR_CATEGORY = "MinorCategory";
	    public const string SQL_COL_DESCRIPTION = "Description";
	    public const string SQL_COL_CATEGORY_PATH = "CategoryPath";
	    public const string SQL_COL_OLD_ACCOUNT_NUMBER = "OldAccountNumber";
	    public const string SQL_COL_OLD_SUB_NUMBER = "OldSubNumber";
	    public const string SQL_COL_OLD_CATEGORY = "OldCategory";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_ACTIVE + "]," +
                "[" + SQL_COL_MAJOR_CATEGORY + "]," +
                "[" + SQL_COL_MINOR_CATEGORY + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_CATEGORY_PATH + "]," +
                "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
                "[" + SQL_COL_OLD_CATEGORY + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_ACTIVE + "]," +
			    "[" + SQL_COL_MAJOR_CATEGORY + "]," +
			    "[" + SQL_COL_MINOR_CATEGORY + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_CATEGORY_PATH + "]," +
			    "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
			    "[" + SQL_COL_OLD_CATEGORY + "]" +
			    " FROM " + TABLE + " ORDER BY " + SQL_COL_DESCRIPTION;

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_ACTIVE + "]," +
			    "[" + SQL_COL_MAJOR_CATEGORY + "]," +
			    "[" + SQL_COL_MINOR_CATEGORY + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_CATEGORY_PATH + "]," +
			    "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
			    "[" + SQL_COL_OLD_CATEGORY + "]" +
			    ") VALUES (?, ?, '?', '?', '?', '?', '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			    "[" + SQL_COL_ACTIVE + "]=?," +
			    "[" + SQL_COL_MAJOR_CATEGORY + "]='?'," +
			    "[" + SQL_COL_MINOR_CATEGORY + "]='?'," +
			    "[" + SQL_COL_DESCRIPTION + "]='?'," +
			    "[" + SQL_COL_CATEGORY_PATH + "]='?'," +
			    "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]='?'," +
			    "[" + SQL_COL_OLD_SUB_NUMBER + "]='?'," +
			    "[" + SQL_COL_OLD_CATEGORY + "]='?'" +
			    " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

        static string SQL_LOAD_BY_CATEGORY = "SELECT " +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_ACTIVE + "]," +
                "[" + SQL_COL_MAJOR_CATEGORY + "]," +
                "[" + SQL_COL_MINOR_CATEGORY + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_CATEGORY_PATH + "]," +
                "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
                "[" + SQL_COL_OLD_CATEGORY + "]" +
                " FROM " + TABLE + 
                " WHERE " + SQL_COL_MAJOR_CATEGORY + "='?'" +
                " ORDER BY [" + SQL_COL_DESCRIPTION + "]";

        static string SQL_LOAD_BY_SUB_CATEGORY = "SELECT " +
               "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_KEY + "]," +
               "[" + SQL_COL_ACTIVE + "]," +
               "[" + SQL_COL_MAJOR_CATEGORY + "]," +
               "[" + SQL_COL_MINOR_CATEGORY + "]," +
               "[" + SQL_COL_DESCRIPTION + "]," +
               "[" + SQL_COL_CATEGORY_PATH + "]," +
               "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
               "[" + SQL_COL_OLD_CATEGORY + "]" +
               " FROM " + TABLE +
               " WHERE " + SQL_COL_MINOR_CATEGORY + "='?'" +
               " ORDER BY [" + SQL_COL_ACCOUNT_NUMBER + "]";

        static string SQL_LOAD_BY_OLD_ACCOUNT = "SELECT " +
               "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_KEY + "]," +
               "[" + SQL_COL_ACTIVE + "]," +
               "[" + SQL_COL_MAJOR_CATEGORY + "]," +
               "[" + SQL_COL_MINOR_CATEGORY + "]," +
               "[" + SQL_COL_DESCRIPTION + "]," +
               "[" + SQL_COL_CATEGORY_PATH + "]," +
               "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
               "[" + SQL_COL_OLD_CATEGORY + "]" +
               " FROM " + TABLE +
               " WHERE " + "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "] = '?';";

        static string SQL_LOAD_BY_CATEGORIES = "SELECT " +
               "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_KEY + "]," +
               "[" + SQL_COL_ACTIVE + "]," +
               "[" + SQL_COL_MAJOR_CATEGORY + "]," +
               "[" + SQL_COL_MINOR_CATEGORY + "]," +
               "[" + SQL_COL_DESCRIPTION + "]," +
               "[" + SQL_COL_CATEGORY_PATH + "]," +
               "[" + SQL_COL_OLD_ACCOUNT_NUMBER + "]," +
               "[" + SQL_COL_OLD_SUB_NUMBER + "]," +
               "[" + SQL_COL_OLD_CATEGORY + "]" +
               " FROM " + TABLE;
               
        static string SQL_LOAD_BY_CATEGORY_ORDER_BY = " ORDER BY [" +
                SQL_COL_MAJOR_CATEGORY + "], [" + SQL_COL_DESCRIPTION + "]";

        protected short m_AccountNumber;
	    protected int m_Key;
	    protected bool m_active;
	    protected string m_MajorCategory;
	    protected string m_MinorCategory;
	    protected string m_Description;
	    protected string m_CategoryPath;
	    protected string m_OldAccountNumber;
	    protected string m_OldSubNumber;
	    protected string m_OldCategory;

        protected bool m_RangeCatFlag;
        protected bool m_RangeUseFlag;
	    protected bool m_isChanged;

        /// <summary>
        /// Constructor
        /// </summary>
	    public Accounts() : base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="Key"></param>
        /// <param name="active"></param>
        /// <param name="MajorCategory"></param>
        /// <param name="MinorCategory"></param>
        /// <param name="Description"></param>
        /// <param name="CategoryPath"></param>
        /// <param name="OldAccountNumber"></param>
        /// <param name="OldSubNumber"></param>
        /// <param name="OldCategory"></param>
	    public Accounts(short AccountNumber,int Key,bool active,string MajorCategory,string MinorCategory,string Description,string CategoryPath,string OldAccountNumber,string OldSubNumber,string OldCategory) : 
			    base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_AccountNumber = AccountNumber;
		    m_Key = Key;
		    m_active = active;
		    m_MajorCategory = MajorCategory;
		    m_MinorCategory = MinorCategory;
		    m_Description = Description;
		    m_CategoryPath = CategoryPath;
		    m_OldAccountNumber = OldAccountNumber;
		    m_OldSubNumber = OldSubNumber;
		    m_OldCategory = OldCategory;
	    }

        /// <summary>
        /// Populates instantiated Account object from database
        /// </summary>
        /// <param name="dr">Datarow to parse</param>
        /// <returns>Populated Account object</returns>
	    protected Accounts fill(DataRow dr)
	    {
		    try {
			    m_AccountNumber = (short) dr[SQL_COL_ACCOUNT_NUMBER];
			    m_Key = (int) dr[SQL_COL_KEY];
			    m_active = (bool) dr[SQL_COL_ACTIVE];
			    m_MajorCategory = (string) (Convert.IsDBNull(dr[SQL_COL_MAJOR_CATEGORY]) ? "" : dr[SQL_COL_MAJOR_CATEGORY]);
			    m_MinorCategory = (string) (Convert.IsDBNull(dr[SQL_COL_MINOR_CATEGORY]) ? "" : dr[SQL_COL_MINOR_CATEGORY]);
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
                if (m_Description.Trim() == "") m_Description = "<no name>";
			    m_CategoryPath = (string) (Convert.IsDBNull(dr[SQL_COL_CATEGORY_PATH]) ? "" : dr[SQL_COL_CATEGORY_PATH]);
			    m_OldAccountNumber = (string) (Convert.IsDBNull(dr[SQL_COL_OLD_ACCOUNT_NUMBER]) ? "" : dr[SQL_COL_OLD_ACCOUNT_NUMBER]);
			    m_OldSubNumber = (string) (Convert.IsDBNull(dr[SQL_COL_OLD_SUB_NUMBER]) ? "" : dr[SQL_COL_OLD_SUB_NUMBER]);
			    m_OldCategory = (string) (Convert.IsDBNull(dr[SQL_COL_OLD_CATEGORY]) ? "" : dr[SQL_COL_OLD_CATEGORY]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

        /// <summary>
        /// Load from database
        /// </summary>
        /// <param name="m_Key">Record key to retrieve</param>
        /// <returns>Account object</returns>
	    public static Accounts load( int m_Key)
	    {
		    Accounts obj  = new Accounts();
		    string[] opt = {m_Key.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null && ds.Tables[0].Rows.Count > 0) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Accounts[] load(bool addBlank = false, bool Active = true)
	     {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Accounts obj = new Accounts();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
            if (addBlank)
                list.Add(new Accounts());
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Accounts();
			    obj.fill(dr);
                if (Active || (!Active && obj.active))
			        list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Accounts[])list.ToArray(typeof(Accounts));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static Accounts[] loadByCategory(string key, bool subCategory = false, bool activeOnly = true)
        {
            string[] opt = { key };
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Accounts obj = new Accounts();

            if (subCategory)
                ds = obj.load(SQL_LOAD_BY_SUB_CATEGORY, opt);
            else
                ds = obj.load(SQL_LOAD_BY_CATEGORY, opt);

            if (ds == null || ds.Tables == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Accounts();
                obj.fill(dr);
                if ((activeOnly && obj.active) || !activeOnly)
                    list.Add(obj);
                obj = null;
            }
            try
            {
                return (Accounts[])list.ToArray(typeof(Accounts));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        public static Accounts[] loadByCategories(string category1 = "", string category2 = "")
        {
            string xsql = SQL_LOAD_BY_CATEGORIES;
            string[] opt = null;
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Accounts obj = new Accounts();

            if (!category1.Trim().Equals(""))
                xsql += " WHERE [" + SQL_COL_MAJOR_CATEGORY + "]= '" + category1.ToString() + "'";
            if (!category2.Trim().Equals(""))
                xsql += " OR [" + SQL_COL_MAJOR_CATEGORY + "] ='" + category2.ToString() + "'";
            xsql += SQL_LOAD_BY_CATEGORY_ORDER_BY;
            ds = obj.load(xsql, opt);
            if (ds == null || ds.Tables == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Accounts();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Accounts[])list.ToArray(typeof(Accounts));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static Accounts loadByOldAccount(string Key)
        {
            Accounts obj = new Accounts();
            string[] opt = { Key };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_OLD_ACCOUNT, opt);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public static DataSet loadByCategoryDs(string key)
        {
            string[] opt = { key };
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Accounts obj = new Accounts();

            ds = obj.load(SQL_LOAD_BY_CATEGORY, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds;
        }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

	    {
		    if (m_Key > 0 ) {
			    string[] values = {m_AccountNumber.ToString(), m_active.ToString(), m_MajorCategory, m_MinorCategory, m_Description, m_CategoryPath, m_OldAccountNumber, m_OldSubNumber, m_OldCategory, m_Key.ToString()};
			    if (this.updateRecord(m_Key.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_AccountNumber.ToString(), m_active.ToString(), m_MajorCategory, m_MinorCategory, m_Description, m_CategoryPath, m_OldAccountNumber, m_OldSubNumber, m_OldCategory};
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
        /// <summary>
		/// Has record been modified?
		/// </summary>
	    public bool IsChanged

        {
            set { m_isChanged = value; }
            get { return m_isChanged; }
        }
	    public short AccountNumber {
		    set{ m_AccountNumber = value;
		    OnPropertyChanged("AccountNumber"); }
		    get { return m_AccountNumber; }
	    }

	    public int Key {
		    get { return m_Key; }
	    }

	    public bool active {
		    set{ m_active = value;
		    OnPropertyChanged("active"); }
		    get { return m_active; }
	    }

	    public string MajorCategory {
		    set{ m_MajorCategory = value;
		    OnPropertyChanged("MajorCategory"); }
		    get { return m_MajorCategory; }
	    }

	    public string MinorCategory {
		    set{ m_MinorCategory = value;
		    OnPropertyChanged("MinorCategory"); }
		    get { return m_MinorCategory; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

	    public string CategoryPath {
		    set{ m_CategoryPath = value;
		    OnPropertyChanged("CategoryPath"); }
		    get { return m_CategoryPath; }
	    }

	    public string OldAccountNumber {
		    set{ m_OldAccountNumber = value;
		    OnPropertyChanged("OldAccountNumber"); }
		    get { return m_OldAccountNumber; }
	    }

	    public string OldSubNumber {
		    set{ m_OldSubNumber = value;
		    OnPropertyChanged("OldSubNumber"); }
		    get { return m_OldSubNumber; }
	    }

	    public string OldCategory {
		    set{ m_OldCategory = value;
		    OnPropertyChanged("OldCategory"); }
		    get { return m_OldCategory; }
	    }

        public bool RangeUseFlag
        {
            get { return m_RangeUseFlag; }
            set { m_RangeUseFlag = value;
                OnPropertyChanged("RangeUseFlag");
            }
        }
        public bool RangeCatFlag
        {
            get { return m_RangeCatFlag; }
            set
            {
                m_RangeCatFlag = value;
                OnPropertyChanged("RangeCatFlag");
            }
        }
    }
}
