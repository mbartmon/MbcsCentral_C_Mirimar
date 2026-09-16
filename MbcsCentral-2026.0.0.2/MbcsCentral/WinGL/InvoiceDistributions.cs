using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class InvoiceDistributions : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_InvoiceDistributions";

	    public const string SQL_COL_INVOICE_KEY = "InvoiceKey";
	    public const string SQL_COL_COMPANY = "Company";
	    public const string SQL_COL_DEPARTMENT = "Department";
	    public const string SQL_COL_ACCOUNT = "Account";
        public const string SQL_COL_BATCH = "Batch";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_JOURNAL = "Journal";

        public const string SQL_COL_COMPANY_NAME = "CompanyName";
        public const string SQL_COL_DEPARTMENT_NAME = "DepartmentName";
        public const string SQL_COL_ACCOUNT_NAME = "AccountName";

        private static string SQL_LOAD = "SELECT " +
                                         "[" + SQL_COL_INVOICE_KEY + "]," +
                                         "[" + SQL_COL_COMPANY + "]," +
                                         "[" + SQL_COL_DEPARTMENT + "]," +
                                         "[" + SQL_COL_ACCOUNT + "]," +
                                         "[" + SQL_COL_BATCH + "]," +
                                         "[" + SQL_COL_ID + "]," +
                                         "[" + SQL_COL_AMOUNT + "]," +
                                         "[" + SQL_COL_JOURNAL + "]" +
                                         " FROM " + TABLE +
                                         " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_INVOICE_KEY + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_INVOICE_KEY + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    ") VALUES (?, ?, ?, ?, ?, ?, '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_INVOICE_KEY + "]=?," +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_ACCOUNT + "]=?," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_JOURNAL + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        private static string SQL_LOAD_BY_INVOICE_old = "SELECT * FROM (SELECT " +
                                         "[" + SQL_COL_INVOICE_KEY + "]," +
                                         "[" + SQL_COL_COMPANY + "]," +
                                         "[" + SQL_COL_DEPARTMENT + "]," +
                                         "[" + SQL_COL_ACCOUNT + "]," +
                                         "[" + SQL_COL_BATCH + "]," +
                                         TABLE + ".[" + SQL_COL_ID + "]," +
                                         "[" + SQL_COL_AMOUNT + "]," +
                                         "[" + SQL_COL_JOURNAL + "], " +
                                         WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
                                         WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
                                         WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_DESCRIPTION + "] AccountName, " +
                                         "ROW_NUMBER() OVER(PARTITION BY InvoiceKey, Company, Department, Account ORDER BY Company, Department, Account ) rn " +
                                         " FROM " + TABLE + ", " +
                                         WinGL.Accounts.TABLE + ", " +
                                         WinGL.Company.TABLE + ", " +
                                         WinGL.Department.TABLE +
                                         " WHERE " + WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_KEY + "] = " + SQL_COL_ACCOUNT +
                                         " AND " + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + SQL_COL_COMPANY +
                                         " AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + SQL_COL_DEPARTMENT +
                                         " AND [" + SQL_COL_INVOICE_KEY + "] = ?) a" +
                                         " WHERE rn = 1" + 
                                         " ORDER BY " + SQL_COL_ID;

        private static string SQL_LOAD_BY_INVOICE = "SELECT * FROM (SELECT " +
                                         "[" + SQL_COL_INVOICE_KEY + "]," +
                                         "[" + SQL_COL_COMPANY + "]," +
                                         "[" + SQL_COL_DEPARTMENT + "]," +
                                         "[" + SQL_COL_ACCOUNT + "]," +
                                         "[" + SQL_COL_BATCH + "]," +
                                         TABLE + ".[" + SQL_COL_ID + "]," +
                                         "[" + SQL_COL_AMOUNT + "]," +
                                         "[" + SQL_COL_JOURNAL + "], " +
                                         WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
                                         WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
                                         WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_DESCRIPTION + "] AccountName, " +
                                         "ROW_NUMBER() OVER(PARTITION BY InvoiceKey, Company, Department, Account ORDER BY Company, Department, Account ) rn " +
                                         " FROM " + TABLE + ", " +
                                         WinGL.Accounts.TABLE + ", " +
                                         WinGL.Company.TABLE + ", " +
                                         WinGL.Department.TABLE +
                                         " WHERE " + WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_KEY + "] = " + SQL_COL_ACCOUNT +
                                         " AND " + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + SQL_COL_COMPANY +
                                         " AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + SQL_COL_DEPARTMENT +
                                         " AND [" + SQL_COL_INVOICE_KEY + "] = ?) a" +
                                         " ORDER BY " + SQL_COL_ID;

        protected int m_InvoiceKey;
	    protected short m_Company;
	    protected short m_Department;
	    protected int m_Account;
        protected int m_Batch;
	    protected int m_Id;
	    protected decimal m_Amount;
	    protected string m_Journal;
	    protected bool m_isChanged;

        public InvoiceDistributions(string table = "")
        {
            if (!table.Trim().Equals(""))
                TABLE = table;
            base.localTable = TABLE;
            base.priKeyName = SQL_COL_ID;
            base.connectionString = GlobalShared.CN.ConnectionString;
            BuildSqlStatements();
        }

        /*
	    public InvoiceDistributions() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }
        */

	    public InvoiceDistributions(int InvoiceKey,short Company,short Department,int Account, int Batch,int Id,decimal Amount,string Journal) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_InvoiceKey = InvoiceKey;
		    m_Company = Company;
		    m_Department = Department;
		    m_Account = Account;
	        m_Batch = Batch;
		    m_Id = Id;
		    m_Amount = Amount;
		    m_Journal = Journal;
	    }

        public void SetTable(string table)
        {
            TABLE = table;
            BuildSqlStatements();
        }
        private void BuildSqlStatements()
        {     
            SQL_LOAD = "SELECT " +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";
            base.LoadStmt = SQL_LOAD;

	    SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_INVOICE_KEY + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    " FROM " + TABLE + ";";
        base.loadAllStmt = SQL_LOAD_ALL;

	    SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_INVOICE_KEY + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    ") VALUES (?, ?, ?, ?, ?, ?, '?');";
        base.insertStmt = SQL_INSERT;

	    SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_INVOICE_KEY + "]=?," +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_ACCOUNT + "]=?," +
                "[" + SQL_COL_BATCH + "]=?," +
                "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_JOURNAL + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";
        base.updateStmt = SQL_UPDATE;

        SQL_DELETE = "DELETE FROM " + TABLE + " WHERE " + priKeyName + " = ?;";

/*
        SQL_LOAD_BY_INVOICE = "SELECT * FROM (SELECT " +
            "[" + SQL_COL_INVOICE_KEY + "]," +
            "[" + SQL_COL_COMPANY + "]," +
            "[" + SQL_COL_DEPARTMENT + "]," +
            "[" + SQL_COL_ACCOUNT + "]," +
            "[" + SQL_COL_BATCH + "]," +
            TABLE + ".[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_JOURNAL + "], " +
            WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
            WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
            WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_DESCRIPTION + "] AccountName, " +
            "ROW_NUMBER() OVER(PARTITION BY InvoiceKey, Company, Department, Account ORDER BY Company, Department, Account ) rn " +
            " FROM " + TABLE + ", " +
            WinGL.Accounts.TABLE + ", " +
            WinGL.Company.TABLE + ", " +
            WinGL.Department.TABLE +
            " WHERE " + WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_KEY + "] = " + SQL_COL_ACCOUNT +
            " AND " + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + SQL_COL_COMPANY +
            " AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + SQL_COL_DEPARTMENT +
            " AND [" + SQL_COL_INVOICE_KEY + "] = ?) a" +
            " WHERE rn = 1" +
            " ORDER BY " + SQL_COL_ID;
*/
            SQL_LOAD_BY_INVOICE = "SELECT * FROM (SELECT " +
                                         "[" + SQL_COL_INVOICE_KEY + "]," +
                                         "[" + SQL_COL_COMPANY + "]," +
                                         "[" + SQL_COL_DEPARTMENT + "]," +
                                         "[" + SQL_COL_ACCOUNT + "]," +
                                         "[" + SQL_COL_BATCH + "]," +
                                         TABLE + ".[" + SQL_COL_ID + "]," +
                                         "[" + SQL_COL_AMOUNT + "]," +
                                         "[" + SQL_COL_JOURNAL + "], " +
                                         WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
                                         WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
                                         WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_DESCRIPTION + "] AccountName, " +
                                         "ROW_NUMBER() OVER(PARTITION BY InvoiceKey, Company, Department, Account ORDER BY Company, Department, Account ) rn " +
                                         " FROM " + TABLE + ", " +
                                         WinGL.Accounts.TABLE + ", " +
                                         WinGL.Company.TABLE + ", " +
                                         WinGL.Department.TABLE +
                                         " WHERE " + WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_KEY + "] = " + SQL_COL_ACCOUNT +
                                         " AND " + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + SQL_COL_COMPANY +
                                         " AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + SQL_COL_DEPARTMENT +
                                         " AND [" + SQL_COL_INVOICE_KEY + "] = ?) a" +
                                         " ORDER BY " + SQL_COL_ID;
        }
	    protected InvoiceDistributions fill(DataRow dr, bool detail = false)
	    {
		    try {
			    m_InvoiceKey = (int) dr[SQL_COL_INVOICE_KEY];
			    m_Company = Convert.ToInt16(dr[SQL_COL_COMPANY]);
			    m_Department =  Convert.ToInt16(dr[SQL_COL_DEPARTMENT]);
			    m_Account = (int) dr[SQL_COL_ACCOUNT];
		        m_Batch = (int) dr[SQL_COL_BATCH];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_Amount = (decimal) dr[SQL_COL_AMOUNT];
			    m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
                if (detail)
                {
                    try
                    {
                        AccountName = (string) (Convert.IsDBNull(dr[SQL_COL_ACCOUNT_NAME]) ? "" : dr[SQL_COL_ACCOUNT_NAME]);
                        CompanyName = (string) (Convert.IsDBNull(dr[SQL_COL_COMPANY_NAME]) ? "" : dr[SQL_COL_COMPANY_NAME]);
                        DepartmentName = (string) (Convert.IsDBNull(dr[SQL_COL_DEPARTMENT_NAME]) ? "" : dr[SQL_COL_DEPARTMENT_NAME]);
                    }
                    catch { }
                }
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static InvoiceDistributions load( int m_Id)
	    {
		    InvoiceDistributions obj  = new InvoiceDistributions();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static InvoiceDistributions[] load(string instanceTable = "")
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
            InvoiceDistributions obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable));

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
                obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable));
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (InvoiceDistributions[])list.ToArray(typeof(InvoiceDistributions));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static InvoiceDistributions[] loadByInvoice(int key, string instanceTable = "", bool LogZero = false)
        {
            ArrayList list = new ArrayList();
            string[] opt = {key.ToString()};

            DataSet ds = new DataSet();
            InvoiceDistributions obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable));

            ds = obj.load(SQL_LOAD_BY_INVOICE, opt, LogZero);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable));
                obj.fill(dr, true);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (InvoiceDistributions[])list.ToArray(typeof(InvoiceDistributions));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static ArrayList loadByInvoiceAsList(int key, string instanceTable = "", bool LogZero = false)
        {
            ArrayList list = new ArrayList();
            string[] opt = { key.ToString() };

            DataSet ds = new DataSet();
            InvoiceDistributions obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable));

            ds = obj.load(SQL_LOAD_BY_INVOICE, opt, LogZero);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            Company[] companies = WinGL.Company.load();
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = (instanceTable.Trim() == "" ? new InvoiceDistributions() : new InvoiceDistributions(instanceTable)); ;
                obj.fill(dr, true);
                obj.CompanyList = companies;
                list.Add(obj);
                obj = null;
            }
            return list;
        }


        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

	    {
		    if (m_Id > 0 ) {
			    string[] values = {m_InvoiceKey.ToString(), m_Company.ToString(), m_Department.ToString(), m_Account.ToString(), m_Batch.ToString(), m_Amount.ToString(), m_Journal, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_InvoiceKey.ToString(), m_Company.ToString(), m_Department.ToString(), m_Account.ToString(), m_Batch.ToString() , m_Amount.ToString(), m_Journal};
			    m_Id = this.insertRecord(values);
			    if (m_Id >= 0) {
				    return true;
			    }
		    }
		    return false;

	    }

        public static void DeleteAll(InvoiceDistributions[] distributions, int ID = 0)
        {
            foreach (InvoiceDistributions distribution in distributions)
            {
                if (ID != 0)
                    if (distribution.InvoiceKey != ID)
                        continue;
                distribution.delete(distribution.Id.ToString());
            }
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
        }
	    public int InvoiceKey {
		    set{ m_InvoiceKey = value;
		    OnPropertyChanged("InvoiceKey"); }
		    get { return m_InvoiceKey; }
	    }

	    public short Company {
		    set{ m_Company = value;
		    OnPropertyChanged("Company"); }
		    get { return m_Company; }
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

        public int Batch
        {
            set
            {
                m_Batch = value;
                OnPropertyChanged("Batch");
            }
            get { return m_Batch; }
        }
	    public int Id {
		    get { return m_Id; }
	    }

	    public decimal Amount {
		    set{ m_Amount = value;
		    OnPropertyChanged("Amount"); }
		    get { return m_Amount; }
	    }

	    public string Journal {
		    set{ m_Journal = value;
		    OnPropertyChanged("Journal"); }
		    get { return m_Journal; }
	    }

        // Additional Properties
        public string AccountName {get; set;}
        public string CompanyName {get; set;}
        public string DepartmentName {get; set;}
        public bool Deleted { get; set; }
        public Department[] DepartmentList { get; set; }
        public Company[] CompanyList { get; set; }
        public Accounts[] AccountList { get; set; }
        public JournalNames[] JournalList { get; set; }
        public Department SelectedDepartment { get; set; }
    }
}
