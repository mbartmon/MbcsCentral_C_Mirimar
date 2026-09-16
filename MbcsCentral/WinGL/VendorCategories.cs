using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class VendorCategories : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_VendorCategories";

	public const string SQL_COL_COMPANY = "Company";
	public const string SQL_COL_DEPARTMENT = "Department";
	public const string SQL_COL_ORDER = "Order";
	public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
	public const string SQL_COL_ACCOUNT = "Account";
	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_AMOUNT = "Amount";
	public const string SQL_COL_JOURNAL_NAME = "JournalName";

        public const string SQL_COL_COMPANY_NAME = "CompanyName";
        public const string SQL_COL_DEPARTMENT_NAME = "DepartmentName";
        public const string SQL_COL_ACCOUNT_NAME = "AccountName";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_COMPANY + "]," +
				"[" + SQL_COL_DEPARTMENT + "]," +
				"[" + SQL_COL_ORDER + "]," +
				"[" + SQL_COL_VENDOR_NUMBER + "]," +
				"[" + SQL_COL_ACCOUNT + "]," +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_AMOUNT + "]," +
				"[" + SQL_COL_JOURNAL_NAME + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_ORDER + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_JOURNAL_NAME + "]" +
                " FROM " + TABLE +
                " ORDER BY [" + SQL_COL_VENDOR_NUMBER + "], [" + SQL_COL_ORDER + "]";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_COMPANY + "]," +
				"[" + SQL_COL_DEPARTMENT + "]," +
				"[" + SQL_COL_ORDER + "]," +
				"[" + SQL_COL_VENDOR_NUMBER + "]," +
				"[" + SQL_COL_ACCOUNT + "]," +
				"[" + SQL_COL_AMOUNT + "]," +
				"[" + SQL_COL_JOURNAL_NAME + "]" +
				") VALUES (?, ?, ?, ?, ?, ?, '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_COMPANY + "]=?," +
				"[" + SQL_COL_DEPARTMENT + "]=?," +
				"[" + SQL_COL_ORDER + "]=?," +
				"[" + SQL_COL_VENDOR_NUMBER + "]=?," +
				"[" + SQL_COL_ACCOUNT + "]=?," +
				"[" + SQL_COL_AMOUNT + "]=?," +
				"[" + SQL_COL_JOURNAL_NAME + "]='?'" +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";



        static string SQL_LOAD_BY_VENDOR = "SELECT " +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_ORDER + "]," +
                "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_JOURNAL_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " ORDER BY [" + SQL_COL_ORDER + "]";

        private static string SQL_LOAD_BY_VENDOR_WITH_DETAIL = "SELECT * FROM (SELECT " +
                                                               "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                               "[" + SQL_COL_COMPANY + "]," +
                                                               "[" + SQL_COL_DEPARTMENT + "]," +
                                                               "[" + SQL_COL_ORDER + "]," +
                                                               "[" + SQL_COL_ACCOUNT + "]," +
                                                               TABLE + ".[" + SQL_COL_ID + "]," +
                                                               "[" + SQL_COL_AMOUNT + "]," +
                                                               "[" + SQL_COL_JOURNAL_NAME + "]," +
                                                               "[" + WinGL.Company.TABLE + "].[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
                                                               "[" + WinGL.Department.TABLE + "].[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
                                                               "[" + WinGL.Accounts.TABLE + "].[" + WinGL.Accounts.SQL_COL_DESCRIPTION + "] AccountName," +
                                                               "ROW_NUMBER() OVER(PARTITION BY VendorNumber, Company, Department, Account ORDER BY gl_VendorCategories.Id ) rn " +
                                                               " FROM " + TABLE + ", " +
                                                               WinGL.Company.TABLE + ", " +
                                                               WinGL.Department.TABLE + ", " +
                                                               WinGL.Accounts.TABLE +
                                                               " WHERE (" + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + SQL_COL_COMPANY + " OR " + SQL_COL_COMPANY + " <= 0)" +
                                                               " AND (" + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + SQL_COL_DEPARTMENT + " OR " + SQL_COL_DEPARTMENT + " <= 0)" +
                                                               " AND " + WinGL.Accounts.TABLE + ".[" + WinGL.Accounts.SQL_COL_KEY + "] = " + SQL_COL_ACCOUNT +
                                                               " AND [" + SQL_COL_VENDOR_NUMBER + "] = ?) a " +
                                                               " WHERE rn = 1" +
                                                               " ORDER BY [" + SQL_COL_ORDER + "]";

		protected short m_Company;
		protected short m_Department;
		protected short m_Order;
		protected int m_VendorNumber;
		protected int m_Account;
		protected int m_Id;
		protected decimal m_Amount;
		protected string m_JournalName;
		protected bool m_isChanged;


        protected string m_CompanyName;
        protected string m_DepartmentName;
        protected string m_AccountName;

        protected Company cmp = null;
        protected Department dpt = null;

	    public VendorCategories() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public VendorCategories(int VendorNumber,Int16 Company,Int16 Department,int Account,int Id,decimal Amount,string JournalName) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_VendorNumber = VendorNumber;
		    m_Company = Company;
		    m_Department = Department;
		    m_Account = Account;
		    m_Id = Id;
		    m_Amount = Amount;
		    m_JournalName = JournalName;
	    }

	    protected VendorCategories fill(DataRow dr, bool IsDetail = false)
	    {
		    try {
			    m_VendorNumber = (int) dr[SQL_COL_VENDOR_NUMBER];
			    m_Company = (Int16) dr[SQL_COL_COMPANY];
				m_Order = (short) dr[SQL_COL_ORDER];
			    m_Department = (Int16) dr[SQL_COL_DEPARTMENT];
			    m_Account = (int) dr[SQL_COL_ACCOUNT];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_Amount = (decimal) dr[SQL_COL_AMOUNT];
			    m_JournalName = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL_NAME]) ? "" : dr[SQL_COL_JOURNAL_NAME]);

                if (IsDetail)
                {

                    if (m_Company == 0)
                        m_CompanyName = "";
                    else
                        m_CompanyName = (string)(Convert.IsDBNull(dr[SQL_COL_COMPANY_NAME]) ? "" : dr[SQL_COL_COMPANY_NAME]);
                    if (m_Department == 0)
                        m_DepartmentName = "";
                    else
                        m_DepartmentName = (string)(Convert.IsDBNull(dr[SQL_COL_DEPARTMENT_NAME]) ? "" : dr[SQL_COL_DEPARTMENT_NAME]);
                    m_AccountName = (string)(Convert.IsDBNull(dr[SQL_COL_ACCOUNT_NAME]) ? "" : dr[SQL_COL_ACCOUNT_NAME]);
                }
		    } catch (SqlException ex) {
		    }
		    return this;
	    }

	    public static VendorCategories load( int m_Id)
	    {
		    VendorCategories obj  = new VendorCategories();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static VendorCategories[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    VendorCategories obj = new VendorCategories();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new VendorCategories();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (VendorCategories[])list.ToArray(typeof(VendorCategories));
		    } catch (SqlException ex) {
			    return null;
		    }
	    }

        public static VendorCategories[] loadByVendor(int vendornumber, bool details = false)
        {
            ArrayList list = new ArrayList();
            string[] opts = { vendornumber.ToString() };

            DataSet ds = new DataSet();
            VendorCategories obj = new VendorCategories();

            if (details)
                ds = obj.load(SQL_LOAD_BY_VENDOR_WITH_DETAIL, opts);

            else
                ds = obj.load(SQL_LOAD_BY_VENDOR, opts);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new VendorCategories();
                obj.fill(dr, details);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (VendorCategories[])list.ToArray(typeof(VendorCategories));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static ArrayList loadByVendorList(int vendornumber)
        {
            ArrayList list = new ArrayList();
            string[] opts = { vendornumber.ToString() };

            DataSet ds = new DataSet();
            VendorCategories obj = new VendorCategories();

            ds = obj.load(SQL_LOAD_BY_VENDOR_WITH_DETAIL, opts);
            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new VendorCategories();
                //m_CompanyName = "";
                //m_DepartmentName = "";

                obj.fill(dr, true);
                list.Add(new VendorAllocations(obj.Id, obj.Company, obj.Order, obj.Department, obj.Account, obj.Amount, obj.JournalName, obj.CompanyName, obj.DepartmentName, obj.AccountName));
                obj = null;
            }
            try
            {
                return list;
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
				string[] values = {m_Company.ToString(), m_Department.ToString(), m_Order.ToString(), m_VendorNumber.ToString(), m_Account.ToString(), m_Amount.ToString(), m_JournalName, m_Id.ToString()};
				if (this.updateRecord(m_Id.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_Company.ToString(), m_Department.ToString(), m_Order.ToString(), m_VendorNumber.ToString(), m_Account.ToString(), m_Amount.ToString(), m_JournalName};
				m_Id = this.insertRecord(values);
				if (m_Id >= 0) {
					return true;
				}
			}		    return false;

	    }

        public static bool delete(int id)
        {
            VendorCategories v = new VendorCategories();
            return v.delete(id.ToString());

        }

	    private void OnPropertyChanged(string info) {
		    if (PropertyChanged != null) {
			    PropertyChanged(this, new PropertyChangedEventArgs(info));
			    m_isChanged = true;
		    }
	    }

	    // Properties

		public short Order {
			set{ m_Order = value;
			OnPropertyChanged("Order"); }
			get { return m_Order; }
		}

	    public int VendorNumber {
		    set{ m_VendorNumber = value;
		    OnPropertyChanged("VendorNumber"); }
		    get { return m_VendorNumber; }
	    }

	    public Int16 Company {
		    set{ m_Company = value;
		    OnPropertyChanged("Company"); }
		    get { return m_Company; }
	    }

	    public Int16 Department {
		    set{ m_Department = value;
		    OnPropertyChanged("Department"); }
		    get { return m_Department; }
	    }

	    public int Account {
		    set{ m_Account = value;
		    OnPropertyChanged("Account"); }
		    get { return m_Account; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public decimal Amount {
		    set{ m_Amount = value;
		    OnPropertyChanged("Amount"); }
		    get { return m_Amount; }
	    }

	    public string JournalName {
		    set{ m_JournalName = value;
		    OnPropertyChanged("JournalName"); }
		    get { return m_JournalName; }
	    }

        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }

        public string DepartmentName
        {
            get { return m_DepartmentName; }
            set { m_DepartmentName = value; }
        }
        public string AccountName
        {
            get { return m_AccountName; }
            set { m_AccountName = value;  }
        }

    }
}
