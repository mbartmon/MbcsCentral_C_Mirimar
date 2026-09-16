using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class CheckNumbers : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_CheckNumbers";

	    public const string SQL_COL_COMPANY = "Company";
	    public const string SQL_COL_DEPARTMENT = "Department";
	    public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
	    public const string SQL_COL_BATCH_CHECK = "BatchCheck";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
		public const string SQL_COL_ACH_CHECK = "ACHCheck";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_BATCH_CHECK + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
				"[" + SQL_COL_ACH_CHECK + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_BATCH_CHECK + "]," +
			    "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
				"[" + SQL_COL_ACH_CHECK + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_BATCH_CHECK + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
				"[" + SQL_COL_ACH_CHECK + "]" +
			    ") VALUES (?, ?, ?, ?, '?', ?);";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_CHECK_NUMBER + "]=?," +
			    "[" + SQL_COL_BATCH_CHECK + "]=?," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]='?'," +
				"[" + SQL_COL_ACH_CHECK + "]=?" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

            static string SQL_LOAD_BY_COMP_DEPT = "SELECT " +
                    "[" + SQL_COL_COMPANY + "]," +
                    "[" + SQL_COL_DEPARTMENT + "]," +
                    "[" + SQL_COL_CHECK_NUMBER + "]," +
                    "[" + SQL_COL_BATCH_CHECK + "]," +
                    "[" + SQL_COL_ID + "]," +
                    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
					"[" + SQL_COL_ACH_CHECK + "]" +
                    " FROM " + TABLE +
                    " WHERE " + "[" + SQL_COL_COMPANY + "] = ?" +
                    " AND [" + SQL_COL_DEPARTMENT + "] = ?";

        protected short m_Company;
	    protected short m_Department;
	    protected int m_CheckNumber;
	    protected int m_BatchCheck;
	    protected int m_Id;
	    protected string m_AccountNumber;
		protected int m_ACHCheck;
	    protected bool m_isChanged;

	    public CheckNumbers() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public CheckNumbers(short Company,short Department,int CheckNumber,int BatchCheck,int Id,string AccountNumber) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Company = Company;
		    m_Department = Department;
		    m_CheckNumber = CheckNumber;
		    m_BatchCheck = BatchCheck;
		    m_Id = Id;
		    m_AccountNumber = AccountNumber;
	    }

	    protected CheckNumbers fill(DataRow dr, bool names = false, bool numbers = false)
	    {
		    try {
			    m_Company = (short) dr[SQL_COL_COMPANY];
			    m_Department = (short) dr[SQL_COL_DEPARTMENT];
			    m_CheckNumber = (int) dr[SQL_COL_CHECK_NUMBER];
			    m_BatchCheck = (int) dr[SQL_COL_BATCH_CHECK];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_AccountNumber = (string) (Convert.IsDBNull(dr[SQL_COL_ACCOUNT_NUMBER]) ? "" : dr[SQL_COL_ACCOUNT_NUMBER]);
				m_ACHCheck = (int)dr[SQL_COL_ACH_CHECK];

                if (names)
                {
                    Company company = WinGL.Company.loadByCompany(m_Company);
                    if (company != null)
                    {
                        CompanyName = (numbers ? "{" + company.CompanyNumber.ToString("00") + "} " : "") + company.Name;
                    }
                    Department department = WinGL.Department.loadByDepartment(m_Department);
                    if (department != null)
                    {
                        DepartmentName = (numbers ? "{" + department.DepartmentNumber.ToString("000") + "} " : "") + department.Name;
                    }
                }
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static CheckNumbers load( int m_Id)
	    {
		    CheckNumbers obj  = new CheckNumbers();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static CheckNumbers[] load(bool names = false, bool numbers = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    CheckNumbers obj = new CheckNumbers();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new CheckNumbers();
			    obj.fill(dr, names, numbers);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (CheckNumbers[])list.ToArray(typeof(CheckNumbers));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static CheckNumbers load(int company, int department)
        {
            CheckNumbers obj = new CheckNumbers();
            string[] opt = { company.ToString(), department.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_COMP_DEPT, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;
        }

            		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

	    {
		    if (m_Id > 0 ) {
			    string[] values = {m_Company.ToString(), m_Department.ToString(), m_CheckNumber.ToString(), m_BatchCheck.ToString(), m_AccountNumber, m_ACHCheck.ToString(), m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Company.ToString(), m_Department.ToString(), m_CheckNumber.ToString(), m_BatchCheck.ToString(), m_AccountNumber, m_ACHCheck.ToString()};
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
        /// <summary>
		/// Has record been modified?
		/// </summary>
	public bool IsChanged

        {
            get { return m_isChanged; }
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

	    public int CheckNumber {
		    set{ m_CheckNumber = value;
		    OnPropertyChanged("CheckNumber"); }
		    get { return m_CheckNumber; }
	    }

	    public int BatchCheck {
		    set{ m_BatchCheck = value;
		    OnPropertyChanged("BatchCheck"); }
		    get { return m_BatchCheck; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public string AccountNumber {
		    set{ m_AccountNumber = value;
		    OnPropertyChanged("AccountNumber"); }
		    get { return m_AccountNumber; }
	    }

		public int ACHCheck
		{
			set { m_ACHCheck = value;
				OnPropertyChanged("ACHCheck");
			}
			get { return m_ACHCheck; }
		}
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
    }
}
