using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class Transactions : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Transactions";

	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
	    public const string SQL_COL_DEPARTMENT_NUMBER = "DepartmentNumber";
	    public const string SQL_COL_FISCAL_YEAR = "FiscalYear";
	    public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
	    public const string SQL_COL_INDEX_NUMBER = "IndexNumber";
	    public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_REC_FLAG = "RecFlag";
	    public const string SQL_COL_JOURNAL = "Journal";
	    public const string SQL_COL_SOURCE = "Source";
	    public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
	    public const string SQL_COL_DESCRIPTION = "Description";

        public const string SQL_COL_BALANCE = "Balance";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_FISCAL_YEAR + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_INDEX_NUMBER + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_REC_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]," +
                "[" + SQL_COL_SOURCE + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_INDEX_NUMBER + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY_NUMBER + "]," +
			    "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			    "[" + SQL_COL_FISCAL_YEAR + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_INDEX_NUMBER + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_REC_FLAG + "]," +
			    "[" + SQL_COL_JOURNAL + "]," +
			    "[" + SQL_COL_SOURCE + "]," +
			    "[" + SQL_COL_VENDOR_NUMBER + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY_NUMBER + "]," +
			    "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			    "[" + SQL_COL_FISCAL_YEAR + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_REC_FLAG + "]," +
			    "[" + SQL_COL_JOURNAL + "]," +
			    "[" + SQL_COL_SOURCE + "]," +
			    "[" + SQL_COL_VENDOR_NUMBER + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]" +
			    ") VALUES ('?', ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_DATE + "]='?'," +
			    "[" + SQL_COL_COMPANY_NUMBER + "]=?," +
			    "[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
			    "[" + SQL_COL_FISCAL_YEAR + "]=?," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			    "[" + SQL_COL_CHECK_NUMBER + "]=?," +
			    "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_REC_FLAG + "]=?," +
			    "[" + SQL_COL_JOURNAL + "]='?'," +
			    "[" + SQL_COL_SOURCE + "]='?'," +
			    "[" + SQL_COL_VENDOR_NUMBER + "]='?'," +
			    "[" + SQL_COL_DESCRIPTION + "]='?'" +
			    " WHERE " + "[" + SQL_COL_INDEX_NUMBER + "] = ?;";

        static string SQL_LOAD_BY_ACCOUNT = "SELECT " +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_FISCAL_YEAR + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_INDEX_NUMBER + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_REC_FLAG + "]," +
            "[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_SOURCE + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ACCOUNT_NUMBER + "] = ?;";

        static string SQL_LOAD_BY_ACCOUNT_DATES = "SELECT " +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_FISCAL_YEAR + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_INDEX_NUMBER + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_REC_FLAG + "]," +
            "[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_SOURCE + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ACCOUNT_NUMBER + "] = ?" +
            " AND " + "[" + SQL_COL_DATE + "] BETWEEN '?' AND '?'";

        static string SQL_LOAD_FOR_GL = "SELECT " +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_FISCAL_YEAR + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_INDEX_NUMBER + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_REC_FLAG + "]," +
            "[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_SOURCE + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_COMPANY_NUMBER + "]=?" +
            " AND [" + SQL_COL_ACCOUNT_NUMBER + "]=?" +
            " AND [" + SQL_COL_DATE + "] BETWEEN '?' AND '?'";

        static string SQL_LOAD_FOR_RECONCILIATION = "SELECT " +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_FISCAL_YEAR + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_INDEX_NUMBER + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_REC_FLAG + "]," +
            "[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_SOURCE + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_COMPANY_NUMBER + "]=?" +
            " AND [" + SQL_COL_DEPARTMENT_NUMBER + "]=?" +
            " AND [" + SQL_COL_ACCOUNT_NUMBER + "]=?" +
            " AND [" + SQL_COL_CHECK_NUMBER + "] = 0" +
            " AND [" + SQL_COL_DATE + "] <= '?'" +
            " AND [" + SQL_COL_REC_FLAG + "] = 0" +
            " ORDER BY [" + SQL_COL_DATE + "]";

        static string SQL_LOAD_ACCT_BALANCE = "SELECT " +
            "SUM(" + SQL_COL_AMOUNT + ") AS " + SQL_COL_BALANCE +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_COMPANY_NUMBER + "] = ?" +
            " AND [" + SQL_COL_ACCOUNT_NUMBER + "] = ?" +
            " AND [" + SQL_COL_DATE + "] BETWEEN '?' AND '?'";

        static string SQL_LOAD_ACCT_BALANCE_DEPT_SUFFIX =
            " AND [" + SQL_COL_DEPARTMENT_NUMBER + "] = ?";

        static string SQL_DELETE_BY_ACCOUNT = "DELETE " +
            " WHERE " + SQL_COL_ACCOUNT_NUMBER + " =?";

        static string SQL_DELETE_BY_CHECK = "DELETE FROM " + TABLE +
                " WHERE [" + SQL_COL_COMPANY_NUMBER + "] =?" +
                " AND [" + SQL_COL_DEPARTMENT_NUMBER + "] =?" +
                " AND [" + SQL_COL_CHECK_NUMBER + "] =?";

        static string SQL_LOAD_DISTINCT_DATE_RANGE = "SELECT DISTINCT [" + SQL_COL_DATE + "] FROM " + TABLE + " ORDER BY [" + SQL_COL_DATE + "]";

	    protected DateTime m_Date;
	    protected short m_CompanyNumber;
	    protected short m_DepartmentNumber;
	    protected short m_FiscalYear;
	    protected int m_AccountNumber;
	    protected int m_IndexNumber;
	    protected int m_CheckNumber;
	    protected decimal m_Amount;
	    protected bool m_RecFlag;
	    protected string m_Journal;
	    protected string m_Source;
	    protected string m_VendorNumber;
	    protected string m_Description;
	    protected bool m_isChanged;

	    public Transactions() : base(TABLE, SQL_COL_INDEX_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Transactions(DateTime Date,short CompanyNumber,short DepartmentNumber,short FiscalYear,int AccountNumber,int IndexNumber,int CheckNumber,decimal Amount,bool RecFlag,string Journal,string Source,string VendorNumber,string Description) : 
			    base(TABLE, SQL_COL_INDEX_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Date = Date;
		    m_CompanyNumber = CompanyNumber;
		    m_DepartmentNumber = DepartmentNumber;
		    m_FiscalYear = FiscalYear;
		    m_AccountNumber = AccountNumber;
		    m_IndexNumber = IndexNumber;
		    m_CheckNumber = CheckNumber;
		    m_Amount = Amount;
		    m_RecFlag = RecFlag;
		    m_Journal = Journal;
		    m_Source = Source;
		    m_VendorNumber = VendorNumber;
		    m_Description = Description;
	    }

        protected Transactions fill(DataRow dr, bool accountName = false)
	    {
		    try {
			    m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
			    m_CompanyNumber = (short) dr[SQL_COL_COMPANY_NUMBER];
			    m_DepartmentNumber = (short) dr[SQL_COL_DEPARTMENT_NUMBER];
			    m_FiscalYear = (short) dr[SQL_COL_FISCAL_YEAR];
			    m_AccountNumber = (int) dr[SQL_COL_ACCOUNT_NUMBER];
			    m_IndexNumber = (int) dr[SQL_COL_INDEX_NUMBER];
			    m_CheckNumber = (int) dr[SQL_COL_CHECK_NUMBER];
			    m_Amount = (decimal) dr[SQL_COL_AMOUNT];
			    m_RecFlag = (bool) dr[SQL_COL_REC_FLAG];
			    m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
			    m_Source = (string) (Convert.IsDBNull(dr[SQL_COL_SOURCE]) ? "" : dr[SQL_COL_SOURCE]);
			    m_VendorNumber = (string) (Convert.IsDBNull(dr[SQL_COL_VENDOR_NUMBER]) ? "" : dr[SQL_COL_VENDOR_NUMBER]);
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
                if (accountName)
                {
                    Accounts account = Accounts.load(m_AccountNumber);
                    if (account != null)
                        AccountName = account.Description;
                    else
                        AccountName = "";
                }
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Transactions load( int m_IndexNumber)
	    {
		    Transactions obj  = new Transactions();
		    string[] opt = {m_IndexNumber.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Transactions[] load(bool forRecon = false, string[] opts = null)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Transactions obj = new Transactions();
            if (!forRecon)
                ds = obj.loadAll();
            else
            {
                if (opts == null)
                    return null;
                ds = obj.load(SQL_LOAD_FOR_RECONCILIATION, opts);
            }
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Transactions();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Transactions[])list.ToArray(typeof(Transactions));
		    } catch (SqlException ex) {
			    return null;
		    }
	    }

        public static Transactions[] loadByAccount(int id)
        {
            return null;
        }

        public static Transactions[] loadByAccountDates(int id, DateTime start, DateTime end)
        {
            ArrayList list = new ArrayList { };

            string[] opt = { id.ToString(), start.ToString(), end.ToString()};

            DataSet ds = new DataSet();
            Transactions obj = new Transactions();

            ds = obj.load(SQL_LOAD_BY_ACCOUNT_DATES, opt);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Transactions();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Transactions[])list.ToArray(typeof(Transactions));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

            public static Transactions[] loadForGL(int company, int account, DateTime startDate, DateTime endDate, int dept = 0)
            {
                string query = SQL_LOAD_FOR_GL;
                string[] opt;
                if (dept > 0)
                {
                    query += " AND [" + SQL_COL_DEPARTMENT_NUMBER + "]=?";
                    opt = new string[] { company.ToString(), account.ToString(), startDate.ToString(), endDate.ToString(), dept.ToString() };
                }
                else
                    opt = new string[] { company.ToString(), account.ToString(), startDate.ToString(), endDate.ToString() };

                query += " ORDER BY [" + SQL_COL_DATE + "], [" + SQL_COL_DEPARTMENT_NUMBER + "]";

                ArrayList list = new ArrayList { };

                DataSet ds = new DataSet();
                Transactions obj = new Transactions();

                ds = obj.load(query, opt);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return null;
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new Transactions();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (Transactions[])list.ToArray(typeof(Transactions));
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }

            public static Transactions[] load(string query, bool accountName = false)
            {
                ArrayList list = new ArrayList();

                DataSet ds = new DataSet();
                Transactions obj = new Transactions();

                ds = obj.load(query, null);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new Transactions();
                    obj.fill(dr, accountName);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (Transactions[])list.ToArray(typeof(Transactions));
                }
                catch (SqlException ex)
                {
                    return null;
                }

            }

        public static void LoadByDateRange(out DateTime? StartDate, out DateTime? EndDate)
        {
            StartDate = null; EndDate = null;
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Transactions obj = new Transactions();

            ds = obj.load(SQL_LOAD_DISTINCT_DATE_RANGE, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;
            obj = null;
            bool first = false;
            DataRow dr = ds.Tables[0].Rows[0];
            StartDate = (DateTime)dr[SQL_COL_DATE];
            dr = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
            EndDate = (DateTime)dr[SQL_COL_DATE];

        }

        /// <summary>
        /// Retrieves SUM of all amounts for a single account for a data range
        /// </summary>
        /// <param name="Company">The CompanyNumber for which to retrieve balance</param>
        /// <param name="Account">The Account Key for which to retrieve balance</param>
        /// <param name="StartDate">The start of the date range</param>
        /// <param name="EndDate">The end of the date range</param>
        /// <param name="Department">The DepartmentNumber for which to retrieve balance</param>
        /// <returns>The decimal balance value</returns>
        public static decimal AccountBalance(int Company, int Account, DateTime StartDate, DateTime EndDate, int Department = 0)
        {
            string[] opt = { Company.ToString(), Account.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString() };
            string query = SQL_LOAD_ACCT_BALANCE;
            if (Department > 0)
            {
                query += SQL_LOAD_ACCT_BALANCE_DEPT_SUFFIX;
                opt = new string[] { Company.ToString(), Account.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString(), Department.ToString() };
            }
            DataSet ds = new DataSet();
            Transactions obj = new Transactions();

            ds = obj.load(query, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return 0;
            DataRow dr = ds.Tables[0].Rows[0];
            return (decimal)(Convert.IsDBNull(dr[SQL_COL_BALANCE])   ? (decimal)0 : dr[SQL_COL_BALANCE]);
        }

        /// <summary>
        /// Given a category, drill down the chain to determine
        /// if there are any transactions for the current or prior years.
        /// </summary>
        /// <param name="key">key for the highest level category to start checking.</param>
        /// <returns>true = found transactions, false = no transactions found</returns>
        public static bool FindCurrentPriorTrans(Category category)
        {
            int year = DateTime.Now.Year;
            DateTime start = new DateTime(year - 1, 1, 1);
            DateTime end = new DateTime(year, 12, 31, 23, 59, 59);
            Transactions[] trans = null;
            Category[] subcats = null;
            Category[] subsubcats = null;
            Accounts[] accts = Accounts.loadByCategory(category.Key);
            if (accts != null && accts.Length > 0)
            {
                foreach(Accounts acct in accts)
                {
                    trans = Transactions.loadByAccountDates(acct.AccountNumber, start, end);
                    if (trans != null && trans.Length > 0) 
                        return true;
                }
            }
            subcats = Category.loadBySubKey(category.Key);
            if (subcats != null && subcats.Length > 0)
            {
                foreach(Category subcat in subcats)
                {
                    accts = Accounts.loadByCategory(subcat.Key);
                    if (accts != null && accts.Length > 0)
                    {
                        foreach(Accounts acct in accts)
                        {
                            trans = Transactions.loadByAccountDates(acct.AccountNumber, start, end);
                            if (trans != null && trans.Length > 0)
                                return true;
                        }
                    }
                    subsubcats = Category.loadBySubKey(subcat.Key);
                    if (subsubcats != null && subsubcats.Length > 0)
                    {
                        foreach (Category subsubcat in subsubcats)
                        {
                            accts = Accounts.loadByCategory(subsubcat.Key);
                            if (accts != null && accts.Length > 0)
                            {
                                foreach (Accounts acct in accts)
                                {
                                    trans = Transactions.loadByAccountDates(acct.AccountNumber, start, end);
                                    if (trans != null && trans.Length > 0)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void DeleteByAccount(int acct)
        {
            SqlConnection localCN = null;
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                    localCN.Open();
                }
                catch { }
                string[] opt = { acct.ToString() };
                string stmt = prepareStatement(SQL_DELETE_BY_ACCOUNT, opt);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(stmt, opt);
                cmd.ExecuteNonQuery();
		    }
		        catch (System.Data.SqlClient.SqlException ex)
		    {
			    return;
		    }
		    finally
		    {
			    try
			    {
				    localCN.Close();
			    }
			    catch {	}
		    }
        }

            public static void DeleteByCheck(int company, int department, int checknumber)
            {
                SqlConnection localCN = null;
                try
                {
                    try
                    {
                        localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                        localCN.Open();
                    }
                    catch { }
                    string[] opt = { company.ToString(), department.ToString(), checknumber.ToString() };
                    string stmt = prepareStatement(SQL_DELETE_BY_CHECK, opt);
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = localCN;
                    cmd.CommandText = prepareStatement(stmt, opt);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    return;
                }
                finally
                {
                    try
                    {
                        localCN.Close();
                    }
                    catch { }
                }

            }

            		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

	    {
            m_Description = m_Description.Replace("'", "`");
            if (m_IndexNumber > 0 ) {
			    string[] values = {m_Date.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_FiscalYear.ToString(), m_AccountNumber.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_RecFlag.ToString(), m_Journal, m_Source, m_VendorNumber, m_Description, m_IndexNumber.ToString()};
			    if (this.updateRecord(m_IndexNumber.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Date.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_FiscalYear.ToString(), m_AccountNumber.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_RecFlag.ToString(), m_Journal, m_Source, m_VendorNumber, m_Description};
			    m_IndexNumber = this.insertRecord(values);
			    if (m_IndexNumber >= 0) {
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
            get { return m_isChanged; }
            set { m_isChanged = value; }
        }

	    public DateTime Date {
		    set{ m_Date = value;
		    OnPropertyChanged("Date"); }
		    get { return m_Date; }
	    }

	    public short CompanyNumber {
		    set{ m_CompanyNumber = value;
		    OnPropertyChanged("CompanyNumber"); }
		    get { return m_CompanyNumber; }
	    }

	    public short DepartmentNumber {
		    set{ m_DepartmentNumber = value;
		    OnPropertyChanged("DepartmentNumber"); }
		    get { return m_DepartmentNumber; }
	    }

	    public short FiscalYear {
		    set{ m_FiscalYear = value;
		    OnPropertyChanged("FiscalYear"); }
		    get { return m_FiscalYear; }
	    }

	    public int AccountNumber {
		    set{ m_AccountNumber = value;
		    OnPropertyChanged("AccountNumber"); }
		    get { return m_AccountNumber; }
	    }

	    public int IndexNumber {
		    get { return m_IndexNumber; }
	    }

	    public int CheckNumber {
		    set{ m_CheckNumber = value;
		    OnPropertyChanged("CheckNumber"); }
		    get { return m_CheckNumber; }
	    }

	    public decimal Amount {
		    set{ m_Amount = value;
		    OnPropertyChanged("Amount"); }
		    get { return m_Amount; }
	    }

	    public bool RecFlag {
		    set{ m_RecFlag = value;
		    OnPropertyChanged("RecFlag"); }
		    get { return m_RecFlag; }
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

	    public string VendorNumber {
		    set{ m_VendorNumber = value;
		    OnPropertyChanged("VendorNumber"); }
		    get { return m_VendorNumber; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

        public string AccountName { get; set; }
    }
}
