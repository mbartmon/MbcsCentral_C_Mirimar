using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection;

namespace MbcsCentral.WinGL {
public class Balances : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_Balances";

	public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
	public const string SQL_COL_DEPARTMENT_NUMBER = "DepartmentNumber";
	public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_C_MONTH01 = "C_Month01";
	public const string SQL_COL_C_MONTH02 = "C_Month02";
	public const string SQL_COL_C_MONTH03 = "C_Month03";
	public const string SQL_COL_C_MONTH04 = "C_Month04";
	public const string SQL_COL_C_MONTH05 = "C_Month05";
	public const string SQL_COL_C_MONTH06 = "C_Month06";
	public const string SQL_COL_P_MONTH07 = "P_Month07";
	public const string SQL_COL_P_MONTH08 = "P_Month08";
	public const string SQL_COL_P_MONTH09 = "P_Month09";
	public const string SQL_COL_P_MONTH10 = "P_Month10";
	public const string SQL_COL_P_MONTH11 = "P_Month11";
	public const string SQL_COL_P_MONTH12 = "P_Month12";
	public const string SQL_COL_P_MONTH01 = "P_Month01";
	public const string SQL_COL_P_MONTH02 = "P_Month02";
	public const string SQL_COL_P_MONTH03 = "P_Month03";
	public const string SQL_COL_P_MONTH04 = "P_Month04";
	public const string SQL_COL_P_MONTH05 = "P_Month05";
	public const string SQL_COL_P_MONTH06 = "P_Month06";
	public const string SQL_COL_C_MONTH07 = "C_Month07";
	public const string SQL_COL_C_MONTH08 = "C_Month08";
	public const string SQL_COL_C_MONTH09 = "C_Month09";
	public const string SQL_COL_C_MONTH10 = "C_Month10";
	public const string SQL_COL_C_MONTH11 = "C_Month11";
	public const string SQL_COL_C_MONTH12 = "C_Month12";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_C_MONTH01 + "]," +
            "[" + SQL_COL_C_MONTH02 + "]," +
            "[" + SQL_COL_C_MONTH03 + "]," +
            "[" + SQL_COL_C_MONTH04 + "]," +
            "[" + SQL_COL_C_MONTH05 + "]," +
            "[" + SQL_COL_C_MONTH06 + "]," +
            "[" + SQL_COL_P_MONTH07 + "]," +
            "[" + SQL_COL_P_MONTH08 + "]," +
            "[" + SQL_COL_P_MONTH09 + "]," +
            "[" + SQL_COL_P_MONTH10 + "]," +
            "[" + SQL_COL_P_MONTH11 + "]," +
            "[" + SQL_COL_P_MONTH12 + "]," +
            "[" + SQL_COL_P_MONTH01 + "]," +
            "[" + SQL_COL_P_MONTH02 + "]," +
            "[" + SQL_COL_P_MONTH03 + "]," +
            "[" + SQL_COL_P_MONTH04 + "]," +
            "[" + SQL_COL_P_MONTH05 + "]," +
            "[" + SQL_COL_P_MONTH06 + "]," +
            "[" + SQL_COL_C_MONTH07 + "]," +
            "[" + SQL_COL_C_MONTH08 + "]," +
            "[" + SQL_COL_C_MONTH09 + "]," +
            "[" + SQL_COL_C_MONTH10 + "]," +
            "[" + SQL_COL_C_MONTH11 + "]," +
            "[" + SQL_COL_C_MONTH12 + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_C_MONTH01 + "]," +
			"[" + SQL_COL_C_MONTH02 + "]," +
			"[" + SQL_COL_C_MONTH03 + "]," +
			"[" + SQL_COL_C_MONTH04 + "]," +
			"[" + SQL_COL_C_MONTH05 + "]," +
			"[" + SQL_COL_C_MONTH06 + "]," +
			"[" + SQL_COL_P_MONTH07 + "]," +
			"[" + SQL_COL_P_MONTH08 + "]," +
			"[" + SQL_COL_P_MONTH09 + "]," +
			"[" + SQL_COL_P_MONTH10 + "]," +
			"[" + SQL_COL_P_MONTH11 + "]," +
			"[" + SQL_COL_P_MONTH12 + "]," +
			"[" + SQL_COL_P_MONTH01 + "]," +
			"[" + SQL_COL_P_MONTH02 + "]," +
			"[" + SQL_COL_P_MONTH03 + "]," +
			"[" + SQL_COL_P_MONTH04 + "]," +
			"[" + SQL_COL_P_MONTH05 + "]," +
			"[" + SQL_COL_P_MONTH06 + "]," +
			"[" + SQL_COL_C_MONTH07 + "]," +
			"[" + SQL_COL_C_MONTH08 + "]," +
			"[" + SQL_COL_C_MONTH09 + "]," +
			"[" + SQL_COL_C_MONTH10 + "]," +
			"[" + SQL_COL_C_MONTH11 + "]," +
			"[" + SQL_COL_C_MONTH12 + "]" +
			" FROM " + TABLE + ";";

        static string SQL_LOAD_COMPANY_DEPARTMENT_ACCOUNT = "SELECT " +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
                "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_C_MONTH01 + "]," +
                "[" + SQL_COL_C_MONTH02 + "]," +
                "[" + SQL_COL_C_MONTH03 + "]," +
                "[" + SQL_COL_C_MONTH04 + "]," +
                "[" + SQL_COL_C_MONTH05 + "]," +
                "[" + SQL_COL_C_MONTH06 + "]," +
                "[" + SQL_COL_P_MONTH07 + "]," +
                "[" + SQL_COL_P_MONTH08 + "]," +
                "[" + SQL_COL_P_MONTH09 + "]," +
                "[" + SQL_COL_P_MONTH10 + "]," +
                "[" + SQL_COL_P_MONTH11 + "]," +
                "[" + SQL_COL_P_MONTH12 + "]," +
                "[" + SQL_COL_P_MONTH01 + "]," +
                "[" + SQL_COL_P_MONTH02 + "]," +
                "[" + SQL_COL_P_MONTH03 + "]," +
                "[" + SQL_COL_P_MONTH04 + "]," +
                "[" + SQL_COL_P_MONTH05 + "]," +
                "[" + SQL_COL_P_MONTH06 + "]," +
                "[" + SQL_COL_C_MONTH07 + "]," +
                "[" + SQL_COL_C_MONTH08 + "]," +
                "[" + SQL_COL_C_MONTH09 + "]," +
                "[" + SQL_COL_C_MONTH10 + "]," +
                "[" + SQL_COL_C_MONTH11 + "]," +
                "[" + SQL_COL_C_MONTH12 + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_COMPANY_NUMBER + "] = ?" +
                " AND [" + SQL_COL_DEPARTMENT_NUMBER + "] = ?" +
                " AND [" + SQL_COL_ACCOUNT_NUMBER + "] = ?";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_C_MONTH01 + "]," +
			"[" + SQL_COL_C_MONTH02 + "]," +
			"[" + SQL_COL_C_MONTH03 + "]," +
			"[" + SQL_COL_C_MONTH04 + "]," +
			"[" + SQL_COL_C_MONTH05 + "]," +
			"[" + SQL_COL_C_MONTH06 + "]," +
			"[" + SQL_COL_P_MONTH07 + "]," +
			"[" + SQL_COL_P_MONTH08 + "]," +
			"[" + SQL_COL_P_MONTH09 + "]," +
			"[" + SQL_COL_P_MONTH10 + "]," +
			"[" + SQL_COL_P_MONTH11 + "]," +
			"[" + SQL_COL_P_MONTH12 + "]," +
			"[" + SQL_COL_P_MONTH01 + "]," +
			"[" + SQL_COL_P_MONTH02 + "]," +
			"[" + SQL_COL_P_MONTH03 + "]," +
			"[" + SQL_COL_P_MONTH04 + "]," +
			"[" + SQL_COL_P_MONTH05 + "]," +
			"[" + SQL_COL_P_MONTH06 + "]," +
			"[" + SQL_COL_C_MONTH07 + "]," +
			"[" + SQL_COL_C_MONTH08 + "]," +
			"[" + SQL_COL_C_MONTH09 + "]," +
			"[" + SQL_COL_C_MONTH10 + "]," +
			"[" + SQL_COL_C_MONTH11 + "]," +
			"[" + SQL_COL_C_MONTH12 + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_COMPANY_NUMBER + "]=?," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			"[" + SQL_COL_C_MONTH01 + "]=?," +
			"[" + SQL_COL_C_MONTH02 + "]=?," +
			"[" + SQL_COL_C_MONTH03 + "]=?," +
			"[" + SQL_COL_C_MONTH04 + "]=?," +
			"[" + SQL_COL_C_MONTH05 + "]=?," +
			"[" + SQL_COL_C_MONTH06 + "]=?," +
			"[" + SQL_COL_P_MONTH07 + "]=?," +
			"[" + SQL_COL_P_MONTH08 + "]=?," +
			"[" + SQL_COL_P_MONTH09 + "]=?," +
			"[" + SQL_COL_P_MONTH10 + "]=?," +
			"[" + SQL_COL_P_MONTH11 + "]=?," +
			"[" + SQL_COL_P_MONTH12 + "]=?," +
			"[" + SQL_COL_P_MONTH01 + "]=?," +
			"[" + SQL_COL_P_MONTH02 + "]=?," +
			"[" + SQL_COL_P_MONTH03 + "]=?," +
			"[" + SQL_COL_P_MONTH04 + "]=?," +
			"[" + SQL_COL_P_MONTH05 + "]=?," +
			"[" + SQL_COL_P_MONTH06 + "]=?," +
			"[" + SQL_COL_C_MONTH07 + "]=?," +
			"[" + SQL_COL_C_MONTH08 + "]=?," +
			"[" + SQL_COL_C_MONTH09 + "]=?," +
			"[" + SQL_COL_C_MONTH10 + "]=?," +
			"[" + SQL_COL_C_MONTH11 + "]=?," +
			"[" + SQL_COL_C_MONTH12 + "]=?" +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

	protected short m_CompanyNumber;
	protected short m_DepartmentNumber;
	protected short m_AccountNumber;
	protected int m_Id;
	protected decimal m_C_Month01;
	protected decimal m_C_Month02;
	protected decimal m_C_Month03;
	protected decimal m_C_Month04;
	protected decimal m_C_Month05;
	protected decimal m_C_Month06;
	protected decimal m_P_Month07;
	protected decimal m_P_Month08;
	protected decimal m_P_Month09;
	protected decimal m_P_Month10;
	protected decimal m_P_Month11;
	protected decimal m_P_Month12;
	protected decimal m_P_Month01;
	protected decimal m_P_Month02;
	protected decimal m_P_Month03;
	protected decimal m_P_Month04;
	protected decimal m_P_Month05;
	protected decimal m_P_Month06;
	protected decimal m_C_Month07;
	protected decimal m_C_Month08;
	protected decimal m_C_Month09;
	protected decimal m_C_Month10;
	protected decimal m_C_Month11;
	protected decimal m_C_Month12;
	protected bool m_isChanged;

	public Balances() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Balances(short CompanyNumber,short DepartmentNumber,short AccountNumber,int Id,decimal C_Month01,decimal C_Month02,decimal C_Month03,decimal C_Month04,decimal C_Month05,decimal C_Month06,decimal P_Month07,decimal P_Month08,decimal P_Month09,decimal P_Month10,decimal P_Month11,decimal P_Month12,decimal P_Month01,decimal P_Month02,decimal P_Month03,decimal P_Month04,decimal P_Month05,decimal P_Month06,decimal C_Month07,decimal C_Month08,decimal C_Month09,decimal C_Month10,decimal C_Month11,decimal C_Month12) : 
			base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_CompanyNumber = CompanyNumber;
		m_DepartmentNumber = DepartmentNumber;
		m_AccountNumber = AccountNumber;
		m_Id = Id;
		m_C_Month01 = C_Month01;
		m_C_Month02 = C_Month02;
		m_C_Month03 = C_Month03;
		m_C_Month04 = C_Month04;
		m_C_Month05 = C_Month05;
		m_C_Month06 = C_Month06;
		m_P_Month07 = P_Month07;
		m_P_Month08 = P_Month08;
		m_P_Month09 = P_Month09;
		m_P_Month10 = P_Month10;
		m_P_Month11 = P_Month11;
		m_P_Month12 = P_Month12;
		m_P_Month01 = P_Month01;
		m_P_Month02 = P_Month02;
		m_P_Month03 = P_Month03;
		m_P_Month04 = P_Month04;
		m_P_Month05 = P_Month05;
		m_P_Month06 = P_Month06;
		m_C_Month07 = C_Month07;
		m_C_Month08 = C_Month08;
		m_C_Month09 = C_Month09;
		m_C_Month10 = C_Month10;
		m_C_Month11 = C_Month11;
		m_C_Month12 = C_Month12;
	}

	protected Balances fill(DataRow dr)
	{
		try {
			m_CompanyNumber = (short) dr[SQL_COL_COMPANY_NUMBER];
			m_DepartmentNumber = (short) dr[SQL_COL_DEPARTMENT_NUMBER];
			m_AccountNumber = (short) dr[SQL_COL_ACCOUNT_NUMBER];
			m_Id = (int) dr[SQL_COL_ID];
			m_C_Month01 = (decimal) dr[SQL_COL_C_MONTH01];
			m_C_Month02 = (decimal) dr[SQL_COL_C_MONTH02];
			m_C_Month03 = (decimal) dr[SQL_COL_C_MONTH03];
			m_C_Month04 = (decimal) dr[SQL_COL_C_MONTH04];
			m_C_Month05 = (decimal) dr[SQL_COL_C_MONTH05];
			m_C_Month06 = (decimal) dr[SQL_COL_C_MONTH06];
			m_P_Month07 = (decimal) dr[SQL_COL_P_MONTH07];
			m_P_Month08 = (decimal) dr[SQL_COL_P_MONTH08];
			m_P_Month09 = (decimal) dr[SQL_COL_P_MONTH09];
			m_P_Month10 = (decimal) dr[SQL_COL_P_MONTH10];
			m_P_Month11 = (decimal) dr[SQL_COL_P_MONTH11];
			m_P_Month12 = (decimal) dr[SQL_COL_P_MONTH12];
			m_P_Month01 = (decimal) dr[SQL_COL_P_MONTH01];
			m_P_Month02 = (decimal) dr[SQL_COL_P_MONTH02];
			m_P_Month03 = (decimal) dr[SQL_COL_P_MONTH03];
			m_P_Month04 = (decimal) dr[SQL_COL_P_MONTH04];
			m_P_Month05 = (decimal) dr[SQL_COL_P_MONTH05];
			m_P_Month06 = (decimal) dr[SQL_COL_P_MONTH06];
			m_C_Month07 = (decimal) dr[SQL_COL_C_MONTH07];
			m_C_Month08 = (decimal) dr[SQL_COL_C_MONTH08];
			m_C_Month09 = (decimal) dr[SQL_COL_C_MONTH09];
			m_C_Month10 = (decimal) dr[SQL_COL_C_MONTH10];
			m_C_Month11 = (decimal) dr[SQL_COL_C_MONTH11];
			m_C_Month12 = (decimal) dr[SQL_COL_C_MONTH12];
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Balances load( int m_Id)
	{
		Balances obj  = new Balances();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Balances[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Balances obj = new Balances();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Balances();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Balances[])list.ToArray(typeof(Balances));
		} catch (SqlException ex) {
			return null;
		}

	}

        public static Balances load(int company, int department, int account)
        {
            Balances obj = new Balances();
            string[] opt = { company.ToString(), department.ToString(), account.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_COMPANY_DEPARTMENT_ACCOUNT, opt);
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
			string[] values = {m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_AccountNumber.ToString(), m_C_Month01.ToString(), m_C_Month02.ToString(), m_C_Month03.ToString(), m_C_Month04.ToString(), m_C_Month05.ToString(), m_C_Month06.ToString(), m_P_Month07.ToString(), m_P_Month08.ToString(), m_P_Month09.ToString(), m_P_Month10.ToString(), m_P_Month11.ToString(), m_P_Month12.ToString(), m_P_Month01.ToString(), m_P_Month02.ToString(), m_P_Month03.ToString(), m_P_Month04.ToString(), m_P_Month05.ToString(), m_P_Month06.ToString(), m_C_Month07.ToString(), m_C_Month08.ToString(), m_C_Month09.ToString(), m_C_Month10.ToString(), m_C_Month11.ToString(), m_C_Month12.ToString(), m_Id.ToString()};
			if (this.updateRecord(m_Id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_AccountNumber.ToString(), m_C_Month01.ToString(), m_C_Month02.ToString(), m_C_Month03.ToString(), m_C_Month04.ToString(), m_C_Month05.ToString(), m_C_Month06.ToString(), m_P_Month07.ToString(), m_P_Month08.ToString(), m_P_Month09.ToString(), m_P_Month10.ToString(), m_P_Month11.ToString(), m_P_Month12.ToString(), m_P_Month01.ToString(), m_P_Month02.ToString(), m_P_Month03.ToString(), m_P_Month04.ToString(), m_P_Month05.ToString(), m_P_Month06.ToString(), m_C_Month07.ToString(), m_C_Month08.ToString(), m_C_Month09.ToString(), m_C_Month10.ToString(), m_C_Month11.ToString(), m_C_Month12.ToString()};
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

    public object this[string name]
        {
            get
            {
                Type myType = this.GetType();
                PropertyInfo myPropInfo = myType.GetProperty(name);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = this.GetType();
                PropertyInfo myPropInfo = myType.GetProperty(name);
                myPropInfo.SetValue(this, value, null);
                OnPropertyChanged(name);

            }
        }

        // Properties
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

	public short AccountNumber {
		set{ m_AccountNumber = value;
		OnPropertyChanged("AccountNumber"); }
		get { return m_AccountNumber; }
	}

	public int Id {
		get { return m_Id; }
	}

	public decimal C_Month01 {
		set{ m_C_Month01 = value;
		OnPropertyChanged("C_Month01"); }
		get { return m_C_Month01; }
	}

	public decimal C_Month02 {
		set{ m_C_Month02 = value;
		OnPropertyChanged("C_Month02"); }
		get { return m_C_Month02; }
	}

	public decimal C_Month03 {
		set{ m_C_Month03 = value;
		OnPropertyChanged("C_Month03"); }
		get { return m_C_Month03; }
	}

	public decimal C_Month04 {
		set{ m_C_Month04 = value;
		OnPropertyChanged("C_Month04"); }
		get { return m_C_Month04; }
	}

	public decimal C_Month05 {
		set{ m_C_Month05 = value;
		OnPropertyChanged("C_Month05"); }
		get { return m_C_Month05; }
	}

	public decimal C_Month06 {
		set{ m_C_Month06 = value;
		OnPropertyChanged("C_Month06"); }
		get { return m_C_Month06; }
	}

	public decimal P_Month07 {
		set{ m_P_Month07 = value;
		OnPropertyChanged("P_Month07"); }
		get { return m_P_Month07; }
	}

	public decimal P_Month08 {
		set{ m_P_Month08 = value;
		OnPropertyChanged("P_Month08"); }
		get { return m_P_Month08; }
	}

	public decimal P_Month09 {
		set{ m_P_Month09 = value;
		OnPropertyChanged("P_Month09"); }
		get { return m_P_Month09; }
	}

	public decimal P_Month10 {
		set{ m_P_Month10 = value;
		OnPropertyChanged("P_Month10"); }
		get { return m_P_Month10; }
	}

	public decimal P_Month11 {
		set{ m_P_Month11 = value;
		OnPropertyChanged("P_Month11"); }
		get { return m_P_Month11; }
	}

	public decimal P_Month12 {
		set{ m_P_Month12 = value;
		OnPropertyChanged("P_Month12"); }
		get { return m_P_Month12; }
	}

	public decimal P_Month01 {
		set{ m_P_Month01 = value;
		OnPropertyChanged("P_Month01"); }
		get { return m_P_Month01; }
	}

	public decimal P_Month02 {
		set{ m_P_Month02 = value;
		OnPropertyChanged("P_Month02"); }
		get { return m_P_Month02; }
	}

	public decimal P_Month03 {
		set{ m_P_Month03 = value;
		OnPropertyChanged("P_Month03"); }
		get { return m_P_Month03; }
	}

	public decimal P_Month04 {
		set{ m_P_Month04 = value;
		OnPropertyChanged("P_Month04"); }
		get { return m_P_Month04; }
	}

	public decimal P_Month05 {
		set{ m_P_Month05 = value;
		OnPropertyChanged("P_Month05"); }
		get { return m_P_Month05; }
	}

	public decimal P_Month06 {
		set{ m_P_Month06 = value;
		OnPropertyChanged("P_Month06"); }
		get { return m_P_Month06; }
	}

	public decimal C_Month07 {
		set{ m_C_Month07 = value;
		OnPropertyChanged("C_Month07"); }
		get { return m_C_Month07; }
	}

	public decimal C_Month08 {
		set{ m_C_Month08 = value;
		OnPropertyChanged("C_Month08"); }
		get { return m_C_Month08; }
	}

	public decimal C_Month09 {
		set{ m_C_Month09 = value;
		OnPropertyChanged("C_Month09"); }
		get { return m_C_Month09; }
	}

	public decimal C_Month10 {
		set{ m_C_Month10 = value;
		OnPropertyChanged("C_Month10"); }
		get { return m_C_Month10; }
	}

	public decimal C_Month11 {
		set{ m_C_Month11 = value;
		OnPropertyChanged("C_Month11"); }
		get { return m_C_Month11; }
	}

	public decimal C_Month12 {
		set{ m_C_Month12 = value;
		OnPropertyChanged("C_Month12"); }
		get { return m_C_Month12; }
	}

}
}
