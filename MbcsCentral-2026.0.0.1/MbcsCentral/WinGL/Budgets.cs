using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class Budgets : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_Budgets";

	public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
	public const string SQL_COL_DEPARTMENT_NUMBER = "DepartmentNumber";
	public const string SQL_COL_ACCOUNT_NUMBER = "AccountNumber";
	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_BUDGET01 = "Budget01";
	public const string SQL_COL_BUDGET02 = "Budget02";
	public const string SQL_COL_BUDGET03 = "Budget03";
	public const string SQL_COL_BUDGET04 = "Budget04";
	public const string SQL_COL_BUDGET05 = "Budget05";
	public const string SQL_COL_BUDGET06 = "Budget06";
	public const string SQL_COL_ACTUAL07 = "Actual07";
	public const string SQL_COL_ACTUAL08 = "Actual08";
	public const string SQL_COL_ACTUAL09 = "Actual09";
	public const string SQL_COL_ACTUAL10 = "Actual10";
	public const string SQL_COL_ACTUAL11 = "Actual11";
	public const string SQL_COL_ACTUAL12 = "Actual12";
	public const string SQL_COL_ACTUAL01 = "Actual01";
	public const string SQL_COL_ACTUAL02 = "Actual02";
	public const string SQL_COL_ACTUAL03 = "Actual03";
	public const string SQL_COL_ACTUAL04 = "Actual04";
	public const string SQL_COL_ACTUAL05 = "Actual05";
	public const string SQL_COL_ACTUAL06 = "Actual06";
	public const string SQL_COL_BUDGET07 = "Budget07";
	public const string SQL_COL_BUDGET08 = "Budget08";
	public const string SQL_COL_BUDGET09 = "Budget09";
	public const string SQL_COL_BUDGET10 = "Budget10";
	public const string SQL_COL_BUDGET11 = "Budget11";
	public const string SQL_COL_BUDGET12 = "Budget12";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_ACCOUNT_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_BUDGET01 + "]," +
            "[" + SQL_COL_BUDGET02 + "]," +
            "[" + SQL_COL_BUDGET03 + "]," +
            "[" + SQL_COL_BUDGET04 + "]," +
            "[" + SQL_COL_BUDGET05 + "]," +
            "[" + SQL_COL_BUDGET06 + "]," +
            "[" + SQL_COL_ACTUAL07 + "]," +
            "[" + SQL_COL_ACTUAL08 + "]," +
            "[" + SQL_COL_ACTUAL09 + "]," +
            "[" + SQL_COL_ACTUAL10 + "]," +
            "[" + SQL_COL_ACTUAL11 + "]," +
            "[" + SQL_COL_ACTUAL12 + "]," +
            "[" + SQL_COL_ACTUAL01 + "]," +
            "[" + SQL_COL_ACTUAL02 + "]," +
            "[" + SQL_COL_ACTUAL03 + "]," +
            "[" + SQL_COL_ACTUAL04 + "]," +
            "[" + SQL_COL_ACTUAL05 + "]," +
            "[" + SQL_COL_ACTUAL06 + "]," +
            "[" + SQL_COL_BUDGET07 + "]," +
            "[" + SQL_COL_BUDGET08 + "]," +
            "[" + SQL_COL_BUDGET09 + "]," +
            "[" + SQL_COL_BUDGET10 + "]," +
            "[" + SQL_COL_BUDGET11 + "]," +
            "[" + SQL_COL_BUDGET12 + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_BUDGET01 + "]," +
			"[" + SQL_COL_BUDGET02 + "]," +
			"[" + SQL_COL_BUDGET03 + "]," +
			"[" + SQL_COL_BUDGET04 + "]," +
			"[" + SQL_COL_BUDGET05 + "]," +
			"[" + SQL_COL_BUDGET06 + "]," +
			"[" + SQL_COL_ACTUAL07 + "]," +
			"[" + SQL_COL_ACTUAL08 + "]," +
			"[" + SQL_COL_ACTUAL09 + "]," +
			"[" + SQL_COL_ACTUAL10 + "]," +
			"[" + SQL_COL_ACTUAL11 + "]," +
			"[" + SQL_COL_ACTUAL12 + "]," +
			"[" + SQL_COL_ACTUAL01 + "]," +
			"[" + SQL_COL_ACTUAL02 + "]," +
			"[" + SQL_COL_ACTUAL03 + "]," +
			"[" + SQL_COL_ACTUAL04 + "]," +
			"[" + SQL_COL_ACTUAL05 + "]," +
			"[" + SQL_COL_ACTUAL06 + "]," +
			"[" + SQL_COL_BUDGET07 + "]," +
			"[" + SQL_COL_BUDGET08 + "]," +
			"[" + SQL_COL_BUDGET09 + "]," +
			"[" + SQL_COL_BUDGET10 + "]," +
			"[" + SQL_COL_BUDGET11 + "]," +
			"[" + SQL_COL_BUDGET12 + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_COMPANY_NUMBER + "]," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]," +
			"[" + SQL_COL_BUDGET01 + "]," +
			"[" + SQL_COL_BUDGET02 + "]," +
			"[" + SQL_COL_BUDGET03 + "]," +
			"[" + SQL_COL_BUDGET04 + "]," +
			"[" + SQL_COL_BUDGET05 + "]," +
			"[" + SQL_COL_BUDGET06 + "]," +
			"[" + SQL_COL_ACTUAL07 + "]," +
			"[" + SQL_COL_ACTUAL08 + "]," +
			"[" + SQL_COL_ACTUAL09 + "]," +
			"[" + SQL_COL_ACTUAL10 + "]," +
			"[" + SQL_COL_ACTUAL11 + "]," +
			"[" + SQL_COL_ACTUAL12 + "]," +
			"[" + SQL_COL_ACTUAL01 + "]," +
			"[" + SQL_COL_ACTUAL02 + "]," +
			"[" + SQL_COL_ACTUAL03 + "]," +
			"[" + SQL_COL_ACTUAL04 + "]," +
			"[" + SQL_COL_ACTUAL05 + "]," +
			"[" + SQL_COL_ACTUAL06 + "]," +
			"[" + SQL_COL_BUDGET07 + "]," +
			"[" + SQL_COL_BUDGET08 + "]," +
			"[" + SQL_COL_BUDGET09 + "]," +
			"[" + SQL_COL_BUDGET10 + "]," +
			"[" + SQL_COL_BUDGET11 + "]," +
			"[" + SQL_COL_BUDGET12 + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_COMPANY_NUMBER + "]=?," +
			"[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
			"[" + SQL_COL_ACCOUNT_NUMBER + "]=?," +
			"[" + SQL_COL_BUDGET01 + "]=?," +
			"[" + SQL_COL_BUDGET02 + "]=?," +
			"[" + SQL_COL_BUDGET03 + "]=?," +
			"[" + SQL_COL_BUDGET04 + "]=?," +
			"[" + SQL_COL_BUDGET05 + "]=?," +
			"[" + SQL_COL_BUDGET06 + "]=?," +
			"[" + SQL_COL_ACTUAL07 + "]=?," +
			"[" + SQL_COL_ACTUAL08 + "]=?," +
			"[" + SQL_COL_ACTUAL09 + "]=?," +
			"[" + SQL_COL_ACTUAL10 + "]=?," +
			"[" + SQL_COL_ACTUAL11 + "]=?," +
			"[" + SQL_COL_ACTUAL12 + "]=?," +
			"[" + SQL_COL_ACTUAL01 + "]=?," +
			"[" + SQL_COL_ACTUAL02 + "]=?," +
			"[" + SQL_COL_ACTUAL03 + "]=?," +
			"[" + SQL_COL_ACTUAL04 + "]=?," +
			"[" + SQL_COL_ACTUAL05 + "]=?," +
			"[" + SQL_COL_ACTUAL06 + "]=?," +
			"[" + SQL_COL_BUDGET07 + "]=?," +
			"[" + SQL_COL_BUDGET08 + "]=?," +
			"[" + SQL_COL_BUDGET09 + "]=?," +
			"[" + SQL_COL_BUDGET10 + "]=?," +
			"[" + SQL_COL_BUDGET11 + "]=?," +
			"[" + SQL_COL_BUDGET12 + "]=?" +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

	protected short m_CompanyNumber;
	protected short m_DepartmentNumber;
	protected short m_AccountNumber;
	protected int m_Id;
	protected decimal m_Budget01;
	protected decimal m_Budget02;
	protected decimal m_Budget03;
	protected decimal m_Budget04;
	protected decimal m_Budget05;
	protected decimal m_Budget06;
	protected decimal m_Actual07;
	protected decimal m_Actual08;
	protected decimal m_Actual09;
	protected decimal m_Actual10;
	protected decimal m_Actual11;
	protected decimal m_Actual12;
	protected decimal m_Actual01;
	protected decimal m_Actual02;
	protected decimal m_Actual03;
	protected decimal m_Actual04;
	protected decimal m_Actual05;
	protected decimal m_Actual06;
	protected decimal m_Budget07;
	protected decimal m_Budget08;
	protected decimal m_Budget09;
	protected decimal m_Budget10;
	protected decimal m_Budget11;
	protected decimal m_Budget12;
	protected bool m_isChanged;

	public Budgets() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public Budgets(short CompanyNumber,short DepartmentNumber,short AccountNumber,int Id,decimal Budget01,decimal Budget02,decimal Budget03,decimal Budget04,decimal Budget05,decimal Budget06,decimal Actual07,decimal Actual08,decimal Actual09,decimal Actual10,decimal Actual11,decimal Actual12,decimal Actual01,decimal Actual02,decimal Actual03,decimal Actual04,decimal Actual05,decimal Actual06,decimal Budget07,decimal Budget08,decimal Budget09,decimal Budget10,decimal Budget11,decimal Budget12) : 
			base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_CompanyNumber = CompanyNumber;
		m_DepartmentNumber = DepartmentNumber;
		m_AccountNumber = AccountNumber;
		m_Id = Id;
		m_Budget01 = Budget01;
		m_Budget02 = Budget02;
		m_Budget03 = Budget03;
		m_Budget04 = Budget04;
		m_Budget05 = Budget05;
		m_Budget06 = Budget06;
		m_Actual07 = Actual07;
		m_Actual08 = Actual08;
		m_Actual09 = Actual09;
		m_Actual10 = Actual10;
		m_Actual11 = Actual11;
		m_Actual12 = Actual12;
		m_Actual01 = Actual01;
		m_Actual02 = Actual02;
		m_Actual03 = Actual03;
		m_Actual04 = Actual04;
		m_Actual05 = Actual05;
		m_Actual06 = Actual06;
		m_Budget07 = Budget07;
		m_Budget08 = Budget08;
		m_Budget09 = Budget09;
		m_Budget10 = Budget10;
		m_Budget11 = Budget11;
		m_Budget12 = Budget12;
	}

	protected Budgets fill(DataRow dr)
	{
		try {
			m_CompanyNumber = (short) dr[SQL_COL_COMPANY_NUMBER];
			m_DepartmentNumber = (short) dr[SQL_COL_DEPARTMENT_NUMBER];
			m_AccountNumber = (short) dr[SQL_COL_ACCOUNT_NUMBER];
			m_Id = (int) dr[SQL_COL_ID];
			m_Budget01 = (decimal) dr[SQL_COL_BUDGET01];
			m_Budget02 = (decimal) dr[SQL_COL_BUDGET02];
			m_Budget03 = (decimal) dr[SQL_COL_BUDGET03];
			m_Budget04 = (decimal) dr[SQL_COL_BUDGET04];
			m_Budget05 = (decimal) dr[SQL_COL_BUDGET05];
			m_Budget06 = (decimal) dr[SQL_COL_BUDGET06];
			m_Actual07 = (decimal) dr[SQL_COL_ACTUAL07];
			m_Actual08 = (decimal) dr[SQL_COL_ACTUAL08];
			m_Actual09 = (decimal) dr[SQL_COL_ACTUAL09];
			m_Actual10 = (decimal) dr[SQL_COL_ACTUAL10];
			m_Actual11 = (decimal) dr[SQL_COL_ACTUAL11];
			m_Actual12 = (decimal) dr[SQL_COL_ACTUAL12];
			m_Actual01 = (decimal) dr[SQL_COL_ACTUAL01];
			m_Actual02 = (decimal) dr[SQL_COL_ACTUAL02];
			m_Actual03 = (decimal) dr[SQL_COL_ACTUAL03];
			m_Actual04 = (decimal) dr[SQL_COL_ACTUAL04];
			m_Actual05 = (decimal) dr[SQL_COL_ACTUAL05];
			m_Actual06 = (decimal) dr[SQL_COL_ACTUAL06];
			m_Budget07 = (decimal) dr[SQL_COL_BUDGET07];
			m_Budget08 = (decimal) dr[SQL_COL_BUDGET08];
			m_Budget09 = (decimal) dr[SQL_COL_BUDGET09];
			m_Budget10 = (decimal) dr[SQL_COL_BUDGET10];
			m_Budget11 = (decimal) dr[SQL_COL_BUDGET11];
			m_Budget12 = (decimal) dr[SQL_COL_BUDGET12];
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Budgets load( int m_Id)
	{
		Budgets obj  = new Budgets();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Budgets[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Budgets obj = new Budgets();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Budgets();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Budgets[])list.ToArray(typeof(Budgets));
		} catch (SqlException ex) {
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
			string[] values = {m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_AccountNumber.ToString(), m_Budget01.ToString(), m_Budget02.ToString(), m_Budget03.ToString(), m_Budget04.ToString(), m_Budget05.ToString(), m_Budget06.ToString(), m_Actual07.ToString(), m_Actual08.ToString(), m_Actual09.ToString(), m_Actual10.ToString(), m_Actual11.ToString(), m_Actual12.ToString(), m_Actual01.ToString(), m_Actual02.ToString(), m_Actual03.ToString(), m_Actual04.ToString(), m_Actual05.ToString(), m_Actual06.ToString(), m_Budget07.ToString(), m_Budget08.ToString(), m_Budget09.ToString(), m_Budget10.ToString(), m_Budget11.ToString(), m_Budget12.ToString(), m_Id.ToString()};
			if (this.updateRecord(m_Id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_CompanyNumber.ToString(), m_DepartmentNumber.ToString(), m_AccountNumber.ToString(), m_Budget01.ToString(), m_Budget02.ToString(), m_Budget03.ToString(), m_Budget04.ToString(), m_Budget05.ToString(), m_Budget06.ToString(), m_Actual07.ToString(), m_Actual08.ToString(), m_Actual09.ToString(), m_Actual10.ToString(), m_Actual11.ToString(), m_Actual12.ToString(), m_Actual01.ToString(), m_Actual02.ToString(), m_Actual03.ToString(), m_Actual04.ToString(), m_Actual05.ToString(), m_Actual06.ToString(), m_Budget07.ToString(), m_Budget08.ToString(), m_Budget09.ToString(), m_Budget10.ToString(), m_Budget11.ToString(), m_Budget12.ToString()};
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

	public decimal Budget01 {
		set{ m_Budget01 = value;
		OnPropertyChanged("Budget01"); }
		get { return m_Budget01; }
	}

	public decimal Budget02 {
		set{ m_Budget02 = value;
		OnPropertyChanged("Budget02"); }
		get { return m_Budget02; }
	}

	public decimal Budget03 {
		set{ m_Budget03 = value;
		OnPropertyChanged("Budget03"); }
		get { return m_Budget03; }
	}

	public decimal Budget04 {
		set{ m_Budget04 = value;
		OnPropertyChanged("Budget04"); }
		get { return m_Budget04; }
	}

	public decimal Budget05 {
		set{ m_Budget05 = value;
		OnPropertyChanged("Budget05"); }
		get { return m_Budget05; }
	}

	public decimal Budget06 {
		set{ m_Budget06 = value;
		OnPropertyChanged("Budget06"); }
		get { return m_Budget06; }
	}

	public decimal Actual07 {
		set{ m_Actual07 = value;
		OnPropertyChanged("Actual07"); }
		get { return m_Actual07; }
	}

	public decimal Actual08 {
		set{ m_Actual08 = value;
		OnPropertyChanged("Actual08"); }
		get { return m_Actual08; }
	}

	public decimal Actual09 {
		set{ m_Actual09 = value;
		OnPropertyChanged("Actual09"); }
		get { return m_Actual09; }
	}

	public decimal Actual10 {
		set{ m_Actual10 = value;
		OnPropertyChanged("Actual10"); }
		get { return m_Actual10; }
	}

	public decimal Actual11 {
		set{ m_Actual11 = value;
		OnPropertyChanged("Actual11"); }
		get { return m_Actual11; }
	}

	public decimal Actual12 {
		set{ m_Actual12 = value;
		OnPropertyChanged("Actual12"); }
		get { return m_Actual12; }
	}

	public decimal Actual01 {
		set{ m_Actual01 = value;
		OnPropertyChanged("Actual01"); }
		get { return m_Actual01; }
	}

	public decimal Actual02 {
		set{ m_Actual02 = value;
		OnPropertyChanged("Actual02"); }
		get { return m_Actual02; }
	}

	public decimal Actual03 {
		set{ m_Actual03 = value;
		OnPropertyChanged("Actual03"); }
		get { return m_Actual03; }
	}

	public decimal Actual04 {
		set{ m_Actual04 = value;
		OnPropertyChanged("Actual04"); }
		get { return m_Actual04; }
	}

	public decimal Actual05 {
		set{ m_Actual05 = value;
		OnPropertyChanged("Actual05"); }
		get { return m_Actual05; }
	}

	public decimal Actual06 {
		set{ m_Actual06 = value;
		OnPropertyChanged("Actual06"); }
		get { return m_Actual06; }
	}

	public decimal Budget07 {
		set{ m_Budget07 = value;
		OnPropertyChanged("Budget07"); }
		get { return m_Budget07; }
	}

	public decimal Budget08 {
		set{ m_Budget08 = value;
		OnPropertyChanged("Budget08"); }
		get { return m_Budget08; }
	}

	public decimal Budget09 {
		set{ m_Budget09 = value;
		OnPropertyChanged("Budget09"); }
		get { return m_Budget09; }
	}

	public decimal Budget10 {
		set{ m_Budget10 = value;
		OnPropertyChanged("Budget10"); }
		get { return m_Budget10; }
	}

	public decimal Budget11 {
		set{ m_Budget11 = value;
		OnPropertyChanged("Budget11"); }
		get { return m_Budget11; }
	}

	public decimal Budget12 {
		set{ m_Budget12 = value;
		OnPropertyChanged("Budget12"); }
		get { return m_Budget12; }
	}

}
}
