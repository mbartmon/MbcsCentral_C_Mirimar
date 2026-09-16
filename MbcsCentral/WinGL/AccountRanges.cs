using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class AccountRanges : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_AccountRanges";

	    public const string SQL_COL_REPORT_SECTION = "ReportSection";
	    public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_REPORT_NAME = "ReportName";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_REPORT_SECTION + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_REPORT_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_REPORT_SECTION + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_REPORT_NAME + "]" +
			    " FROM " + TABLE + ";";

        static string SQL_LOAD_REPORT_RANGE = "SELECT " +
                "[" + SQL_COL_REPORT_SECTION + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_REPORT_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_REPORT_NAME + "] = '?'" +
                " AND [" + SQL_COL_REPORT_SECTION + "] = ?" +
                " AND [" + SQL_COL_ACCOUNT_NUMBER + "] = ?";

        static string SQL_LOAD_BY_REPORT = "SELECT " +
                "[" + SQL_COL_REPORT_SECTION + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_REPORT_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_REPORT_NAME + "] = '?';";

        static string SQL_LOAD_BY_REPORT_SECTION = "SELECT " +
                "[" + SQL_COL_REPORT_SECTION + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_REPORT_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_REPORT_NAME + "] = '?'" +
                " AND [" + SQL_COL_REPORT_SECTION + "] = ?";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_REPORT_SECTION + "]," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			    "[" + SQL_COL_REPORT_NAME + "]" +
			    ") VALUES (?, ?, '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_REPORT_SECTION + "]=?," +
			    "[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			    "[" + SQL_COL_REPORT_NAME + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    protected int m_ReportSection;
	    protected int m_AccountNumber;
	    protected int m_Id;
	    protected string m_ReportName;
	    protected bool m_isChanged;

	    public AccountRanges() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public AccountRanges(int ReportSection,int AccountNumber,int Id,string ReportName) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_ReportSection = ReportSection;
		    m_AccountNumber = AccountNumber;
		    m_Id = Id;
		    m_ReportName = ReportName;
	    }

	    protected AccountRanges fill(DataRow dr)
	    {
		    try {
			    m_ReportSection = (int) dr[SQL_COL_REPORT_SECTION];
			    m_AccountNumber = (int) dr[SQL_COL_ACCOUNT_NUMBER];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_ReportName = (string) (Convert.IsDBNull(dr[SQL_COL_REPORT_NAME]) ? "" : dr[SQL_COL_REPORT_NAME]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static AccountRanges load( int m_Id)
	    {
		    AccountRanges obj  = new AccountRanges();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static AccountRanges[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    AccountRanges obj = new AccountRanges();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new AccountRanges();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (AccountRanges[])list.ToArray(typeof(AccountRanges));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static AccountRanges[] load(string reportname)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            AccountRanges obj = new AccountRanges();
            string[] opts = { reportname };

            ds = obj.load(SQL_LOAD_BY_REPORT, opts);
            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new AccountRanges();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (AccountRanges[])list.ToArray(typeof(AccountRanges));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static AccountRanges[] load(string reportname, int reportsection = 0)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            AccountRanges obj = new AccountRanges();
            string[] opts = { reportname, reportsection.ToString() };

            if (reportsection == 0)
                ds = obj.load(SQL_LOAD_BY_REPORT, opts);
            else
                ds = obj.load(SQL_LOAD_BY_REPORT_SECTION, opts);

            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new AccountRanges();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (AccountRanges[])list.ToArray(typeof(AccountRanges));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static AccountRanges load(string reportName, int reportSection, int accountNumber)
        {
            AccountRanges obj = new AccountRanges();
            string[] opt = {reportName, reportSection.ToString(), accountNumber.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_REPORT_RANGE, opt);
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
			    string[] values = {m_ReportSection.ToString(), m_AccountNumber.ToString(), m_ReportName, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_ReportSection.ToString(), m_AccountNumber.ToString(), m_ReportName};
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
	    public int ReportSection {
		    set{ m_ReportSection = value;
		    OnPropertyChanged("ReportSection"); }
		    get { return m_ReportSection; }
	    }

	    public int AccountNumber {
		    set{ m_AccountNumber = value;
		    OnPropertyChanged("AccountNumber"); }
		    get { return m_AccountNumber; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public string ReportName {
		    set{ m_ReportName = value;
		    OnPropertyChanged("ReportName"); }
		    get { return m_ReportName; }
	    }

    }
}
