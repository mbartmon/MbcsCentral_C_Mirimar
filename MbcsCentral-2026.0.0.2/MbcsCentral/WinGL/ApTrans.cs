using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class ApTrans : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_ApTrans";

	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_COMPANY = "Company";
	    public const string SQL_COL_DEPARTMENT = "Department";
	    public const string SQL_COL_BATCH_NUMBER = "BatchNumber";
	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_ACCOUNT = "Account";
	    public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_DESCRIPTION = "Description";
	    public const string SQL_COL_JOURNAL = "Journal";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_ACCOUNT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_BATCH_NUMBER + "]," +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_BATCH_NUMBER + "]," +
			    "[" + SQL_COL_ACCOUNT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_JOURNAL + "]" +
			    ") VALUES ('?', ?, ?, ?, ?, ?, ?, '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_DATE + "]='?'," +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_BATCH_NUMBER + "]=?," +
			    "[" + SQL_COL_ACCOUNT + "]=?," +
			    "[" + SQL_COL_CHECK_NUMBER + "]=?," +
			    "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_DESCRIPTION + "]='?'," +
			    "[" + SQL_COL_JOURNAL + "]='?'" +
			    " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    protected DateTime m_Date;
	    protected short m_Company;
	    protected short m_Department;
	    protected int m_BatchNumber;
	    protected int m_Key;
	    protected int m_Account;
	    protected int m_CheckNumber;
	    protected decimal m_Amount;
	    protected string m_Description;
	    protected string m_Journal;
	    protected bool m_isChanged;

	    public ApTrans() : base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public ApTrans(DateTime Date,short Company,short Department,int BatchNumber,int Key,int Account,int CheckNumber,decimal Amount,string Description,string Journal) : 
			    base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Date = Date;
		    m_Company = Company;
		    m_Department = Department;
		    m_BatchNumber = BatchNumber;
		    m_Key = Key;
		    m_Account = Account;
		    m_CheckNumber = CheckNumber;
		    m_Amount = Amount;
		    m_Description = Description;
		    m_Journal = Journal;
	    }

	    protected ApTrans fill(DataRow dr)
	    {
		    try {
			    m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
			    m_Company = (short) dr[SQL_COL_COMPANY];
			    m_Department = (short) dr[SQL_COL_DEPARTMENT];
			    m_BatchNumber = (int) dr[SQL_COL_BATCH_NUMBER];
			    m_Key = (int) dr[SQL_COL_KEY];
			    m_Account = (int) dr[SQL_COL_ACCOUNT];
			    m_CheckNumber = (int) dr[SQL_COL_CHECK_NUMBER];
			    m_Amount = (decimal) dr[SQL_COL_AMOUNT];
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			    m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
                if (m_Company > 0)
                {
                    WinGL.Company cmp = WinGL.Company.loadByCompany(m_Company);
                    if (cmp != null)
                        CompanyName = cmp.Name.Trim();
                }
                if (m_Department > 0)
                {
                    WinGL.Department dpt = WinGL.Department.loadByDepartment(m_Department);
                    if (dpt != null)
                        DepartmentName = dpt.Name.Trim();
                }
                if (m_Account > 0)
                {
                    WinGL.Accounts acct = Accounts.load(m_Account);
                    if (acct != null)
                        AccountName = acct.Description.Trim();
                }
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static ApTrans load( int m_Key)
	    {
		    ApTrans obj  = new ApTrans();
		    string[] opt = {m_Key.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static ApTrans[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    ApTrans obj = new ApTrans();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new ApTrans();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (ApTrans[])list.ToArray(typeof(ApTrans));
		    } catch (SqlException ex) {
			    return null;
		    }
	    }

        public static ApTrans[] load(string query)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApTrans obj = new ApTrans();

            ds = obj.load(query, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApTrans();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApTrans[])list.ToArray(typeof(ApTrans));
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
		    if (m_Key > 0 ) {
			    string[] values = {m_Date.ToString(), m_Company.ToString(), m_Department.ToString(), m_BatchNumber.ToString(), m_Account.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_Description, m_Journal, m_Key.ToString()};
			    if (this.updateRecord(m_Key.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Date.ToString(), m_Company.ToString(), m_Department.ToString(), m_BatchNumber.ToString(), m_Account.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_Description, m_Journal};
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
	    public DateTime Date {
		    set{ m_Date = value;
		    OnPropertyChanged("Date"); }
		    get { return m_Date; }
	    }

	    public short Company {
		    set{ m_Company = value;
		    OnPropertyChanged("Company"); }
		    get { return m_Company; }
	    }

        public string CompanyName { get;set; }

	    public short Department {
		    set{ m_Department = value;
		    OnPropertyChanged("Department"); }
		    get { return m_Department; }
	    }

        public string DepartmentName { get; set; }

	    public int BatchNumber {
		    set{ m_BatchNumber = value;
		    OnPropertyChanged("BatchNumber"); }
		    get { return m_BatchNumber; }
	    }

	    public int Key {
		    get { return m_Key; }
	    }

	    public int Account {
		    set{ m_Account = value;
		    OnPropertyChanged("Account"); }
		    get { return m_Account; }
	    }

        public string AccountName { get; set; }

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

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

	    public string Journal {
		    set{ m_Journal = value;
		    OnPropertyChanged("Journal"); }
		    get { return m_Journal; }
	    }

    }
}
