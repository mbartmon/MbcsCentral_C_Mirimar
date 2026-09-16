using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class BatchJrnEntries : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_BatchJrnEntries";

	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_DEPARTMENT = "Department";
	    public const string SQL_COL_ACCOUNT = "Account";
	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_DESCRIPTION = "Description";
        public const string SQL_COL_AUTO_OFFSET = "AutoOffset";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_DESCRIPTION + "], " +
                SQL_COL_AUTO_OFFSET +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_I_D + "] = ? ";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_DESCRIPTION + "], " +
                SQL_COL_AUTO_OFFSET + 
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_DESCRIPTION + "], " +
                SQL_COL_AUTO_OFFSET +
			    ") VALUES (?, ?, ?, ?, '?', ?);";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_KEY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_ACCOUNT + "]=?," +
			    "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_DESCRIPTION + "]='?', " +
                SQL_COL_AUTO_OFFSET + " = ?" +
			    " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

            static string SQL_LOAD_BY_KEY = "SELECT " +
                    "[" + SQL_COL_KEY + "]," +
                    "[" + SQL_COL_DEPARTMENT + "]," +
                    "[" + SQL_COL_ACCOUNT + "]," +
                    "[" + SQL_COL_I_D + "]," +
                    "[" + SQL_COL_AMOUNT + "]," +
                    "[" + SQL_COL_DESCRIPTION + "], " +
                    SQL_COL_AUTO_OFFSET +
                    " FROM " + TABLE +
                    " WHERE " + "[" + SQL_COL_KEY + "] = ?" +
                    " ORDER BY " + SQL_COL_AUTO_OFFSET;


        protected int m_Key;
	        protected short m_Department;
	        protected int m_Account;
	        protected int m_ID;
	        protected decimal m_Amount;
            protected decimal m_Debit;
            protected decimal m_Credit;
	        protected string m_Description;
            protected bool m_AutoOffset;
	        protected bool m_isChanged;

	    public BatchJrnEntries() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public BatchJrnEntries(int Key,short Department,int Account,int ID,decimal Amount,string Description) : 
			    base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Key = Key;
		    m_Department = Department;
		    m_Account = Account;
		    m_ID = ID;
		    m_Amount = Amount;
		    m_Description = Description;
	    }

	    protected BatchJrnEntries fill(DataRow dr)
	    {
		    try {
			    m_Key = (int) dr[SQL_COL_KEY];
                try
                {
                    m_Department = (short)dr[SQL_COL_DEPARTMENT];
                }
                catch
                {
                    int dpt = (int)(Convert.IsDBNull(dr[SQL_COL_DEPARTMENT]) ? 0 : dr[SQL_COL_DEPARTMENT]);
                    try
                    {
                        m_Department = (short)dpt;
                    }
                    catch
                    {
                        m_Department = 0;
                    }
                }
			    m_Account = (int) dr[SQL_COL_ACCOUNT];
			    m_ID = (int) dr[SQL_COL_I_D];
			    m_Amount = (decimal) dr[SQL_COL_AMOUNT];
                if (m_Amount < 0)
                    this.Credit = Math.Abs(m_Amount);
                else
                    this.Debit = m_Amount;
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
                m_AutoOffset = (bool)(Convert.IsDBNull(dr[SQL_COL_AUTO_OFFSET]) ? false : dr[SQL_COL_AUTO_OFFSET]);
		    } catch (Exception ex) {
		    }
		    return this;

	    }

	    public static BatchJrnEntries load( int m_ID)
	    {
		    BatchJrnEntries obj  = new BatchJrnEntries();
		    string[] opt = {m_ID.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static BatchJrnEntries[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    BatchJrnEntries obj = new BatchJrnEntries();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new BatchJrnEntries();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (BatchJrnEntries[])list.ToArray(typeof(BatchJrnEntries));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }
            public static BatchJrnEntries[] loadByKey(int key)
            {
                ArrayList list = new ArrayList();

                DataSet ds = new DataSet();
                BatchJrnEntries obj = new BatchJrnEntries();
                string[] parms = { key.ToString() };

                ds = obj.load(SQL_LOAD_BY_KEY, parms);
                if (ds == null)
                    return null;
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new BatchJrnEntries();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (BatchJrnEntries[])list.ToArray(typeof(BatchJrnEntries));
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
			    string[] values = {m_Key.ToString(), m_Department.ToString(), m_Account.ToString(), m_Amount.ToString(), m_Description, m_AutoOffset.ToString(), m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Key.ToString(), m_Department.ToString(), m_Account.ToString(), m_Amount.ToString(), m_Description, m_AutoOffset.ToString()};
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
	    public int Key {
		    set{ m_Key = value;
		    OnPropertyChanged("Key"); }
		    get { return m_Key; }
	    }

	    public short Department {
		    set{ m_Department = value;
		    OnPropertyChanged("Department"); }
		    get { return m_Department; }
	    }

	    public int Account {
		    set{ m_Account = value;
		    OnPropertyChanged("Account"); }
		    get { return m_Account; }
	    }

	    public int ID {
		    get { return m_ID; }
            set { m_ID = value; }
	    }

	    public decimal Amount {
		    set{ m_Amount = value;
		    OnPropertyChanged("Amount"); }
		    get { return m_Amount; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

        public bool AutoOffset
        {
            get { return m_AutoOffset; }
            set
            {
                m_AutoOffset = value;
                OnPropertyChanged("AutoOffset");
            }
        }
        /// <summary>
		/// Has record been modified?
		/// </summary>
	public bool IsChanged

        {
                get { return m_isChanged; }
                set { m_isChanged = value; }
        }
        public decimal Debit
            {
                get; set;
            }
        public decimal Credit
            {
                get; set;
            }
        public int Index { get; set; }
    }
}
