using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class Memos : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_Memos";

	public const string SQL_COL_CHECK_GROUP = "CheckGroup";
	public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
	public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
	public const string SQL_COL_DEPARTMENT_NUMBER = "DepartmentNumber";
	public const string SQL_COL_BATCH_NUMBER = "BatchNumber";
	public const string SQL_COL_MEMO_ID = "MemoId";
	public const string SQL_COL_MEMO = "Memo";
    public const string SQL_COL_COMPANY_NAME = "CompanyName";
    public const string SQL_COL_DEPARTMENT_NAME = "DepartmentName";
    public const string SQL_COL_KEEP = "Keep";

        static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_CHECK_GROUP + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_BATCH_NUMBER + "]," +
            "[" + SQL_COL_MEMO_ID + "]," +
            "[" + SQL_COL_KEEP + "]," +
            "[" + SQL_COL_MEMO + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_MEMO_ID + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_KEEP + "]," +
                "[" + SQL_COL_MEMO + "]" +
                " FROM " + TABLE + ";";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_KEEP + "]," +
                "[" + SQL_COL_MEMO + "]" +
                ") VALUES (?, ?, ?, ?, ?, ?, '?');";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_CHECK_GROUP + "]=?," +
                "[" + SQL_COL_VENDOR_NUMBER + "]=?," +
                "[" + SQL_COL_COMPANY_NUMBER + "]=?," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
                "[" + SQL_COL_BATCH_NUMBER + "]=?," +
                "[" + SQL_COL_KEEP + "]=?," +
                "[" + SQL_COL_MEMO + "]='?'" +
                " WHERE " + "[" + SQL_COL_MEMO_ID + "] = ?;";

        static string SQL_DELETE_VENDOR_BATCH = "DELETE FROM " + TABLE +
                                                " WHERE [" + SQL_COL_VENDOR_NUMBER + "] < 0" +
                                                " AND [" + SQL_COL_BATCH_NUMBER + "] = 0";

        static string SQL_DELETE_UNUSED_NON_BATCH = "DELETE FROM " + TABLE + 
                                                    " WHERE [" + SQL_COL_VENDOR_NUMBER + "] > 0" +
                                                    " AND [" + SQL_COL_BATCH_NUMBER + "] = 0" +
                                                    " AND " + SQL_COL_KEEP + " = 'false'" +
                                                    " AND NOT EXISTS ( SELECT * FROM " + Invoices.TABLE + " i " + 
                                                    " WHERE " + TABLE + ".]" + SQL_COL_MEMO_ID + "] = " +
                                                    "i.[" + Invoices.SQL_COL_MEMO_ID + "] )";

        /* There is no apparant use for this query that only returns a single record per company
        static string SQL_LOAD_BY_VENDOR = "SELECT * FROM (SELECT " +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                TABLE + ".[" + SQL_COL_COMPANY_NUMBER + "]," +
                TABLE + ".[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + WinGL.Company.TABLE + "].[" + WinGL.Company.SQL_COL_NAME + "] CompanyName, " +
                "[" + WinGL.Department.TABLE + "].[" + WinGL.Department.SQL_COL_NAME + "] DepartmentName, " +
                "ROW_NUMBER() OVER(PARTITION BY VendorNumber, " + TABLE + "." + SQL_COL_COMPANY_NUMBER + ", " + TABLE + "." + SQL_COL_DEPARTMENT_NUMBER + " ORDER BY gl_Memos.MemoId ) rn " +
                " FROM " + TABLE + ", " +
                WinGL.Company.TABLE + ", " +
                WinGL.Department.TABLE +
                " WHERE (" + WinGL.Company.TABLE + ".[" + WinGL.Company.SQL_COL_COMPANY_NUMBER + "] = " + TABLE + "." + SQL_COL_COMPANY_NUMBER + " OR " + TABLE + "." + SQL_COL_COMPANY_NUMBER + " = 0)" +
                " AND (" + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + TABLE + "." + SQL_COL_DEPARTMENT_NUMBER + " OR " + TABLE + "." + SQL_COL_DEPARTMENT_NUMBER + " = 0)" +
                " AND " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?) a " +
                "WHERE rn = 1";
            */
        static string SQL_LOAD_BY_VENDOR = "SELECT " +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_KEEP + "]" +
                " FROM " + TABLE + 
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " OR [" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " ORDER BY [" + SQL_COL_COMPANY_NUMBER + "], [" + SQL_COL_DEPARTMENT_NUMBER + "], [" + SQL_COL_MEMO + "]";

        static string SQL_LOAD_FOR_INVOICE = "SELECT " +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_BATCH_NUMBER + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_KEEP + "]" +
                " FROM " + TABLE +
                " WHERE [" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND [" + SQL_COL_COMPANY_NUMBER + "] = ?" +
                " AND [" + SQL_COL_DEPARTMENT_NUMBER + "] = ?";

        protected short m_CheckGroup;
        protected int m_VendorNumber;
        protected int m_CompanyNumber;
        protected int m_DepartmentNumber;
        protected int m_BatchNumber;
        protected int m_MemoId;
        protected string m_Memo;
        protected bool m_Keep;
        protected bool m_isChanged;
        protected string m_CompanyName;
        protected string m_DepartmentName;
        private bool m_Delete = false;

	public Memos() : base(TABLE, SQL_COL_MEMO_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Memos(short CheckGroup,int VendorNumber,int CompanyNumber,int DepartmentNumber,int BatchNumber,int MemoId,string Memo) : 
			base(TABLE, SQL_COL_MEMO_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_CheckGroup = CheckGroup;
		m_VendorNumber = VendorNumber;
		m_CompanyNumber = CompanyNumber;
		m_DepartmentNumber = DepartmentNumber;
		m_BatchNumber = BatchNumber;
		m_MemoId = MemoId;
		m_Memo = Memo;
	}

	protected Memos fill(DataRow dr, bool IsDetail = false)
	{
		try {
			m_CheckGroup = (short) dr[SQL_COL_CHECK_GROUP];
			m_VendorNumber = (int) dr[SQL_COL_VENDOR_NUMBER];
			m_CompanyNumber = (int) dr[SQL_COL_COMPANY_NUMBER];
			m_DepartmentNumber = (int) dr[SQL_COL_DEPARTMENT_NUMBER];
			m_BatchNumber = (int) dr[SQL_COL_BATCH_NUMBER];
			m_MemoId = (int) dr[SQL_COL_MEMO_ID];
			m_Memo = (string) (Convert.IsDBNull(dr[SQL_COL_MEMO]) ? "" : dr[SQL_COL_MEMO]);
            m_Keep = (bool)dr[SQL_COL_KEEP];
            if (IsDetail)
            {

                if (m_CompanyNumber == 0)
                    m_CompanyName = "";
                else
                {
                    Company company = Company.loadByCompany(m_CompanyNumber);
                    if (company != null)
                        m_CompanyName = company.Name;
                }
                if (m_DepartmentNumber == 0)
                    m_DepartmentName = "";
                else
                {
                    Department department = Department.loadByDepartment((short)m_DepartmentNumber);
                    if (department != null)
                        m_DepartmentName = department.Name;
                }
            }
        }
        catch (SqlException ex)
        {
		}
		return this;

	}

	public static Memos load( int m_MemoId)
	{
		Memos obj  = new Memos();
		string[] opt = {m_MemoId.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null && ds.Tables[0].Rows.Count > 0) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Memos[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Memos obj = new Memos();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Memos();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Memos[])list.ToArray(typeof(Memos));
		} catch (SqlException ex) {
			return null;
		}
	}

    public static Memos[] loadByVendor(long vendorNumber)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        Memos obj = new Memos();
        string[] opts = { vendorNumber.ToString(), (-vendorNumber).ToString() };

        ds = obj.load(SQL_LOAD_BY_VENDOR, opts);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Memos();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (Memos[])list.ToArray(typeof(Memos));
        }
        catch (SqlException ex)
        {
            return null;
        }
    }

    public static ArrayList loadByVendorList(long vendorNumber)
    {
        ArrayList list = new ArrayList();
        string[] opts = { vendorNumber.ToString(), (-vendorNumber).ToString()};

        DataSet ds = new DataSet();
        Memos obj = new Memos();

        ds = obj.load(SQL_LOAD_BY_VENDOR, opts);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Memos();
            obj.fill(dr, true);
                list.Add(obj);
            //list.Add(new VendorMemos(m_CheckGroup, m_VendorNumber, m_CompanyNumber, m_DepartmentNumber, m_BatchNumber, m_MemoId, m_Memo, m_CompanyName, m_DepartmentName));
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

    public static Memos[] loadByVendorCompDept(long vendorNumber, int companyNumber, int departmentNumber, int checkGroup = 0, int batchNumber = 0)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        Memos obj = new Memos();
        string[] opts;
        string stmt = SQL_LOAD_FOR_INVOICE;
        if (batchNumber > 0)
        {
            opts = new string[] { vendorNumber.ToString(), companyNumber.ToString(), departmentNumber.ToString(), checkGroup.ToString(), batchNumber.ToString() };
            stmt += " AND " + SQL_COL_CHECK_GROUP + " = ?" +
                                    " AND " + SQL_COL_BATCH_NUMBER + " = ?";
        }
        else
            opts = new string[] { vendorNumber.ToString(), companyNumber.ToString(), departmentNumber.ToString() };

        ds = obj.load(stmt, opts);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Memos();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (Memos[])list.ToArray(typeof(Memos));
        }
        catch (SqlException ex)
        {
            return null;
        }
    }

        public void DeleteUnused(bool NonBatch = false)
        {
            base.query(SQL_DELETE_VENDOR_BATCH, null);
            if (NonBatch)
                base.query(SQL_DELETE_UNUSED_NON_BATCH, null);
        }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

        {
            if (m_MemoId > 0)
            {
                string[] values = { m_CheckGroup.ToString(), m_VendorNumber.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_BatchNumber.ToString(), m_Keep.ToString(), m_Memo, m_MemoId.ToString() };
                if (this.updateRecord(m_MemoId.ToString(), values))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string[] values = { m_CheckGroup.ToString(), m_VendorNumber.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_BatchNumber.ToString(), m_Keep.ToString(), m_Memo };
                m_MemoId = this.insertRecord(values);
                if (m_MemoId >= 0)
                {
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
	public short CheckGroup {
		set{ m_CheckGroup = value;
		OnPropertyChanged("CheckGroup"); }
		get { return m_CheckGroup; }
	}

	public int VendorNumber {
		set{ m_VendorNumber = value;
		OnPropertyChanged("VendorNumber"); }
		get { return m_VendorNumber; }
	}

	public int CompanyNumber {
		set{ m_CompanyNumber = value;
		OnPropertyChanged("CompanyNumber"); }
		get { return m_CompanyNumber; }
	}

	public int DepartmentNumber {
		set{ m_DepartmentNumber = value;
		OnPropertyChanged("DepartmentNumber"); }
		get { return m_DepartmentNumber; }
	}

	public int BatchNumber {
		set{ m_BatchNumber = value;
		OnPropertyChanged("BatchNumber"); }
		get { return m_BatchNumber; }
	}

	public int MemoId {
		get { return m_MemoId; }
        set { m_MemoId = value; }
	}

    public bool Keep
    {
        set
        {
            m_Keep = value;
            OnPropertyChanged("Keep");
        }
        get { return m_Keep; }
    }

        public string Memo {
		set{ m_Memo = value;
		OnPropertyChanged("Memo"); }
		get { return m_Memo; }
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
    /// <summary>
	/// Has record been modified?
	/// </summary>
	public bool IsChanged

    {
        get { return m_isChanged; }
        set
        {
            m_isChanged = value;
            OnPropertyChanged("IsChanged");
        }
    }
    public bool Delete
    {
        get { return m_Delete; }
        set
        {
            m_Delete = value;
            OnPropertyChanged("Delete");
        }
    }
}
}
