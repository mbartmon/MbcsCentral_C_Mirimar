using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class Transactiontemp : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_Transactiontemp";

	public const string SQL_COL_DATE = "Date";
	public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
	public const string SQL_COL_DEPARTMENT_NUMBER = "DepartmentNumber";
	public const string SQL_COL_FISCAL_YEAR = "FiscalYear";
	public const string SQL_COL_MAJOR_CATEGORY = "MajorCategory";
	public const string SQL_COL_MINOR_CATEGORY = "MinorCategory";
	public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
	public const string SQL_COL_INDEX_NUMBER = "IndexNumber";
	public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
	public const string SQL_COL_AMOUNT = "Amount";
	public const string SQL_COL_JOURNAL = "Journal";
	public const string SQL_COL_SOURCE = "Source";
	public const string SQL_COL_DESCRIPTION = "Description";
	public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_FISCAL_YEAR + "]," +
            "[" + SQL_COL_MAJOR_CATEGORY + "]," +
            "[" + SQL_COL_MINOR_CATEGORY + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_INDEX_NUMBER + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_SOURCE + "]," +
            "[" + SQL_COL_DESCRIPTION + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_INDEX_NUMBER + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_DATE + "]," +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_FISCAL_YEAR + "]," +
			"[" + SQL_COL_MAJOR_CATEGORY + "]," +
			"[" + SQL_COL_MINOR_CATEGORY + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_INDEX_NUMBER + "]," +
			"[" + SQL_COL_CHECK_NUMBER + "]," +
			"[" + SQL_COL_AMOUNT + "]," +
			"[" + SQL_COL_JOURNAL + "]," +
			"[" + SQL_COL_SOURCE + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_VENDOR_NUMBER + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_DATE + "]," +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_FISCAL_YEAR + "]," +
			"[" + SQL_COL_MAJOR_CATEGORY + "]," +
			"[" + SQL_COL_MINOR_CATEGORY + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_CHECK_NUMBER + "]," +
			"[" + SQL_COL_AMOUNT + "]," +
			"[" + SQL_COL_JOURNAL + "]," +
			"[" + SQL_COL_SOURCE + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_VENDOR_NUMBER + "]" +
			") VALUES ('?', ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_DATE + "]='?'," +
			"[" + SQL_COL_COMPANY_NUMBER + "]=?," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
			"[" + SQL_COL_FISCAL_YEAR + "]=?," +
			"[" + SQL_COL_MAJOR_CATEGORY + "]=?," +
			"[" + SQL_COL_MINOR_CATEGORY + "]=?," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			"[" + SQL_COL_CHECK_NUMBER + "]=?," +
			"[" + SQL_COL_AMOUNT + "]=?," +
			"[" + SQL_COL_JOURNAL + "]='?'," +
			"[" + SQL_COL_SOURCE + "]='?'," +
			"[" + SQL_COL_DESCRIPTION + "]='?'," +
			"[" + SQL_COL_VENDOR_NUMBER + "]='?'" +
			" WHERE " + "[" + SQL_COL_INDEX_NUMBER + "] = ?;";

	protected DateTime m_Date;
	protected short m_CompanyNumber;
	protected short m_DepartmentNumber;
	protected short m_FiscalYear;
	protected short m_MajorCategory;
	protected short m_MinorCategory;
	protected int m_AccountNumber;
	protected int m_IndexNumber;
	protected int m_CheckNumber;
	protected decimal m_Amount;
	protected string m_Journal;
	protected string m_Source;
	protected string m_Description;
	protected string m_VendorNumber;
	protected bool m_isChanged;

	public Transactiontemp() : base(TABLE, SQL_COL_INDEX_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Transactiontemp(DateTime Date,short CompanyNumber,short DepartmentNumber,short FiscalYear,short MajorCategory,short MinorCategory,int AccountNumber,int IndexNumber,int CheckNumber,decimal Amount,string Journal,string Source,string Description,string VendorNumber) : 
			base(TABLE, SQL_COL_INDEX_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Date = Date;
		m_CompanyNumber = CompanyNumber;
		m_DepartmentNumber = DepartmentNumber;
		m_FiscalYear = FiscalYear;
		m_MajorCategory = MajorCategory;
		m_MinorCategory = MinorCategory;
		m_AccountNumber = AccountNumber;
		m_IndexNumber = IndexNumber;
		m_CheckNumber = CheckNumber;
		m_Amount = Amount;
		m_Journal = Journal;
		m_Source = Source;
		m_Description = Description;
		m_VendorNumber = VendorNumber;
	}

	protected Transactiontemp fill(DataRow dr)
	{
		try {
			m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
			m_CompanyNumber = (short) dr[SQL_COL_COMPANY_NUMBER];
			m_DepartmentNumber = (short) dr[SQL_COL_DEPARTMENT_NUMBER];
			m_FiscalYear = (short) dr[SQL_COL_FISCAL_YEAR];
			m_MajorCategory = (short) dr[SQL_COL_MAJOR_CATEGORY];
			m_MinorCategory = (short) dr[SQL_COL_MINOR_CATEGORY];
			m_AccountNumber = (int) dr[SQL_COL_ACCOUNT_NUMBER];
			m_IndexNumber = (int) dr[SQL_COL_INDEX_NUMBER];
			m_CheckNumber = (int) dr[SQL_COL_CHECK_NUMBER];
			m_Amount = (decimal) dr[SQL_COL_AMOUNT];
			m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
			m_Source = (string) (Convert.IsDBNull(dr[SQL_COL_SOURCE]) ? "" : dr[SQL_COL_SOURCE]);
			m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			m_VendorNumber = (string) (Convert.IsDBNull(dr[SQL_COL_VENDOR_NUMBER]) ? "" : dr[SQL_COL_VENDOR_NUMBER]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Transactiontemp load( int m_IndexNumber)
	{
		Transactiontemp obj  = new Transactiontemp();
		string[] opt = {m_IndexNumber.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Transactiontemp[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Transactiontemp obj = new Transactiontemp();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Transactiontemp();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Transactiontemp[])list.ToArray(typeof(Transactiontemp));
		} catch (SqlException ex) {
			return null;
		}

	}

        public static Transactiontemp[] load(string query)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Transactiontemp obj = new Transactiontemp();

            ds = obj.load(query, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Transactiontemp();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Transactiontemp[])list.ToArray(typeof(Transactiontemp));
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
            m_Description = m_Description.Replace("'", "`");
            if (m_IndexNumber > 0 ) {
			string[] values = {m_Date.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_FiscalYear.ToString(), m_MajorCategory.ToString(), m_MinorCategory.ToString(), m_AccountNumber.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_Journal, m_Source, m_Description, m_VendorNumber, m_IndexNumber.ToString()};
			if (this.updateRecord(m_IndexNumber.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Date.ToString(), m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_FiscalYear.ToString(), m_MajorCategory.ToString(), m_MinorCategory.ToString(), m_AccountNumber.ToString(), m_CheckNumber.ToString(), m_Amount.ToString(), m_Journal, m_Source, m_Description, m_VendorNumber};
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

	public short MajorCategory {
		set{ m_MajorCategory = value;
		OnPropertyChanged("MajorCategory"); }
		get { return m_MajorCategory; }
	}

	public short MinorCategory {
		set{ m_MinorCategory = value;
		OnPropertyChanged("MinorCategory"); }
		get { return m_MinorCategory; }
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

	public string Description {
		set{ m_Description = value;
		OnPropertyChanged("Description"); }
		get { return m_Description; }
	}

	public string VendorNumber {
		set{ m_VendorNumber = value;
		OnPropertyChanged("VendorNumber"); }
		get { return m_VendorNumber; }
	}

    public bool Delete { get; set; }

    /// <summary>
	/// Has record been modified?
	/// </summary>
	public bool IsChanged

    {
        get { return m_isChanged; }
        set { m_isChanged = m_isChanged; }
    }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }
    public int Index { get; set; }
}
}
