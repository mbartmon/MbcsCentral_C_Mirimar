using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class PRaccounts : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_PRaccounts";

	public const string SQL_COL_ACCOUNT00 = "Account00";
	public const string SQL_COL_ACCOUNT01 = "Account01";
	public const string SQL_COL_ACCOUNT02 = "Account02";
	public const string SQL_COL_ACCOUNT03 = "Account03";
	public const string SQL_COL_ACCOUNT04 = "Account04";
	public const string SQL_COL_ACCOUNT05 = "Account05";
	public const string SQL_COL_ACCOUNT18 = "Account18";
	public const string SQL_COL_ACCOUNT19 = "Account19";
	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_ACCOUNT12 = "Account12";
	public const string SQL_COL_ACCOUNT13 = "Account13";
	public const string SQL_COL_ACCOUNT14 = "Account14";
	public const string SQL_COL_ACCOUNT15 = "Account15";
	public const string SQL_COL_ACCOUNT16 = "Account16";
	public const string SQL_COL_ACCOUNT17 = "Account17";
	public const string SQL_COL_ACCOUNT06 = "Account06";
	public const string SQL_COL_ACCOUNT07 = "Account07";
	public const string SQL_COL_ACCOUNT08 = "Account08";
	public const string SQL_COL_ACCOUNT09 = "Account09";
	public const string SQL_COL_ACCOUNT10 = "Account10";
	public const string SQL_COL_ACCOUNT11 = "Account11";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_ACCOUNT00 + "]," +
            "[" + SQL_COL_ACCOUNT01 + "]," +
            "[" + SQL_COL_ACCOUNT02 + "]," +
            "[" + SQL_COL_ACCOUNT03 + "]," +
            "[" + SQL_COL_ACCOUNT04 + "]," +
            "[" + SQL_COL_ACCOUNT05 + "]," +
            "[" + SQL_COL_ACCOUNT18 + "]," +
            "[" + SQL_COL_ACCOUNT19 + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_ACCOUNT12 + "]," +
            "[" + SQL_COL_ACCOUNT13 + "]," +
            "[" + SQL_COL_ACCOUNT14 + "]," +
            "[" + SQL_COL_ACCOUNT15 + "]," +
            "[" + SQL_COL_ACCOUNT16 + "]," +
            "[" + SQL_COL_ACCOUNT17 + "]," +
            "[" + SQL_COL_ACCOUNT06 + "]," +
            "[" + SQL_COL_ACCOUNT07 + "]," +
            "[" + SQL_COL_ACCOUNT08 + "]," +
            "[" + SQL_COL_ACCOUNT09 + "]," +
            "[" + SQL_COL_ACCOUNT10 + "]," +
            "[" + SQL_COL_ACCOUNT11 + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_ACCOUNT00 + "]," +
			"[" + SQL_COL_ACCOUNT01 + "]," +
			"[" + SQL_COL_ACCOUNT02 + "]," +
			"[" + SQL_COL_ACCOUNT03 + "]," +
			"[" + SQL_COL_ACCOUNT04 + "]," +
			"[" + SQL_COL_ACCOUNT05 + "]," +
			"[" + SQL_COL_ACCOUNT18 + "]," +
			"[" + SQL_COL_ACCOUNT19 + "]," +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_ACCOUNT12 + "]," +
			"[" + SQL_COL_ACCOUNT13 + "]," +
			"[" + SQL_COL_ACCOUNT14 + "]," +
			"[" + SQL_COL_ACCOUNT15 + "]," +
			"[" + SQL_COL_ACCOUNT16 + "]," +
			"[" + SQL_COL_ACCOUNT17 + "]," +
			"[" + SQL_COL_ACCOUNT06 + "]," +
			"[" + SQL_COL_ACCOUNT07 + "]," +
			"[" + SQL_COL_ACCOUNT08 + "]," +
			"[" + SQL_COL_ACCOUNT09 + "]," +
			"[" + SQL_COL_ACCOUNT10 + "]," +
			"[" + SQL_COL_ACCOUNT11 + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_ACCOUNT00 + "]," +
			"[" + SQL_COL_ACCOUNT01 + "]," +
			"[" + SQL_COL_ACCOUNT02 + "]," +
			"[" + SQL_COL_ACCOUNT03 + "]," +
			"[" + SQL_COL_ACCOUNT04 + "]," +
			"[" + SQL_COL_ACCOUNT05 + "]," +
			"[" + SQL_COL_ACCOUNT18 + "]," +
			"[" + SQL_COL_ACCOUNT19 + "]," +
			"[" + SQL_COL_ACCOUNT12 + "]," +
			"[" + SQL_COL_ACCOUNT13 + "]," +
			"[" + SQL_COL_ACCOUNT14 + "]," +
			"[" + SQL_COL_ACCOUNT15 + "]," +
			"[" + SQL_COL_ACCOUNT16 + "]," +
			"[" + SQL_COL_ACCOUNT17 + "]," +
			"[" + SQL_COL_ACCOUNT06 + "]," +
			"[" + SQL_COL_ACCOUNT07 + "]," +
			"[" + SQL_COL_ACCOUNT08 + "]," +
			"[" + SQL_COL_ACCOUNT09 + "]," +
			"[" + SQL_COL_ACCOUNT10 + "]," +
			"[" + SQL_COL_ACCOUNT11 + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_ACCOUNT00 + "]=?," +
			"[" + SQL_COL_ACCOUNT01 + "]=?," +
			"[" + SQL_COL_ACCOUNT02 + "]=?," +
			"[" + SQL_COL_ACCOUNT03 + "]=?," +
			"[" + SQL_COL_ACCOUNT04 + "]=?," +
			"[" + SQL_COL_ACCOUNT05 + "]=?," +
			"[" + SQL_COL_ACCOUNT18 + "]=?," +
			"[" + SQL_COL_ACCOUNT19 + "]=?," +
			"[" + SQL_COL_ACCOUNT12 + "]=?," +
			"[" + SQL_COL_ACCOUNT13 + "]=?," +
			"[" + SQL_COL_ACCOUNT14 + "]=?," +
			"[" + SQL_COL_ACCOUNT15 + "]=?," +
			"[" + SQL_COL_ACCOUNT16 + "]=?," +
			"[" + SQL_COL_ACCOUNT17 + "]=?," +
			"[" + SQL_COL_ACCOUNT06 + "]=?," +
			"[" + SQL_COL_ACCOUNT07 + "]=?," +
			"[" + SQL_COL_ACCOUNT08 + "]=?," +
			"[" + SQL_COL_ACCOUNT09 + "]=?," +
			"[" + SQL_COL_ACCOUNT10 + "]=?," +
			"[" + SQL_COL_ACCOUNT11 + "]=?" +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

	protected int m_Account00 = -1;
	protected int m_Account01 = -1;
	protected int m_Account02 = -1;
	protected int m_Account03 = -1;
	protected int m_Account04 = -1;
	protected int m_Account05 = -1;
	protected int m_Account18 = -1;
	protected int m_Account19 = -1;
	protected int m_Id;
	protected int m_Account12 = -1;
	protected int m_Account13 = -1;
	protected int m_Account14 = -1;
	protected int m_Account15 = -1;
	protected int m_Account16 = -1;
	protected int m_Account17 = -1;
	protected int m_Account06 = -1;
	protected int m_Account07 = -1;
	protected int m_Account08 = -1;
	protected int m_Account09 = -1;
	protected int m_Account10 = -1;
	protected int m_Account11 = -1;
	protected bool m_isChanged;

	public PRaccounts() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public PRaccounts(int Account00,int Account01,int Account02,int Account03,int Account04,int Account05,int Account18,int Account19,int Id,int Account12,int Account13,int Account14,int Account15,int Account16,int Account17,int Account06,int Account07,int Account08,int Account09,int Account10,int Account11) : 
			base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Account00 = Account00;
		m_Account01 = Account01;
		m_Account02 = Account02;
		m_Account03 = Account03;
		m_Account04 = Account04;
		m_Account05 = Account05;
		m_Account18 = Account18;
		m_Account19 = Account19;
		m_Id = Id;
		m_Account12 = Account12;
		m_Account13 = Account13;
		m_Account14 = Account14;
		m_Account15 = Account15;
		m_Account16 = Account16;
		m_Account17 = Account17;
		m_Account06 = Account06;
		m_Account07 = Account07;
		m_Account08 = Account08;
		m_Account09 = Account09;
		m_Account10 = Account10;
		m_Account11 = Account11;
	}

	protected PRaccounts fill(DataRow dr)
	{
		try {
			m_Account00 = (int) dr[SQL_COL_ACCOUNT00];
			m_Account01 = (int) dr[SQL_COL_ACCOUNT01];
			m_Account02 = (int) dr[SQL_COL_ACCOUNT02];
			m_Account03 = (int) dr[SQL_COL_ACCOUNT03];
			m_Account04 = (int) dr[SQL_COL_ACCOUNT04];
			m_Account05 = (int) dr[SQL_COL_ACCOUNT05];
			m_Account18 = (int) dr[SQL_COL_ACCOUNT18];
			m_Account19 = (int) dr[SQL_COL_ACCOUNT19];
			m_Id = (int) dr[SQL_COL_ID];
			m_Account12 = (int) dr[SQL_COL_ACCOUNT12];
			m_Account13 = (int) dr[SQL_COL_ACCOUNT13];
			m_Account14 = (int) dr[SQL_COL_ACCOUNT14];
			m_Account15 = (int) dr[SQL_COL_ACCOUNT15];
			m_Account16 = (int) dr[SQL_COL_ACCOUNT16];
			m_Account17 = (int) dr[SQL_COL_ACCOUNT17];
			m_Account06 = (int) dr[SQL_COL_ACCOUNT06];
			m_Account07 = (int) dr[SQL_COL_ACCOUNT07];
			m_Account08 = (int) dr[SQL_COL_ACCOUNT08];
			m_Account09 = (int) dr[SQL_COL_ACCOUNT09];
			m_Account10 = (int) dr[SQL_COL_ACCOUNT10];
			m_Account11 = (int) dr[SQL_COL_ACCOUNT11];
		} catch (SqlException ex) {
		}
		return this;

	}

	public static PRaccounts load( int m_Id)
	{
		PRaccounts obj  = new PRaccounts();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static PRaccounts load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		PRaccounts obj = new PRaccounts();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new PRaccounts();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (PRaccounts) list[0];
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
			string[] values = {m_Account00.ToString(), m_Account01.ToString(), m_Account02.ToString(), m_Account03.ToString(), m_Account04.ToString(), m_Account05.ToString(), m_Account18.ToString(), m_Account19.ToString(), m_Account12.ToString(), m_Account13.ToString(), m_Account14.ToString(), m_Account15.ToString(), m_Account16.ToString(), m_Account17.ToString(), m_Account06.ToString(), m_Account07.ToString(), m_Account08.ToString(), m_Account09.ToString(), m_Account10.ToString(), m_Account11.ToString(), m_Id.ToString()};
			if (this.updateRecord(m_Id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Account00.ToString(), m_Account01.ToString(), m_Account02.ToString(), m_Account03.ToString(), m_Account04.ToString(), m_Account05.ToString(), m_Account18.ToString(), m_Account19.ToString(), m_Account12.ToString(), m_Account13.ToString(), m_Account14.ToString(), m_Account15.ToString(), m_Account16.ToString(), m_Account17.ToString(), m_Account06.ToString(), m_Account07.ToString(), m_Account08.ToString(), m_Account09.ToString(), m_Account10.ToString(), m_Account11.ToString()};
			m_Id = this.insertRecord(values);
			if (m_Id >= 0) {
				return true;
			}
		}
		return false;

	}

	private void OnPropertyChanged(string info) {
		if (PropertyChanged != null)
        {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			m_isChanged = true;
		}
	}

	// Properties
	
    public int this[string propertyName]
        {
            get { return (int)this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

	public int Account00 {
		set{ m_Account00 = value;
		OnPropertyChanged("Account00"); }
		get { return m_Account00; }
	}

	public int Account01 {
		set{ m_Account01 = value;
		OnPropertyChanged("Account01"); }
		get { return m_Account01; }
	}

	public int Account02 {
		set{ m_Account02 = value;
		OnPropertyChanged("Account02"); }
		get { return m_Account02; }
	}

	public int Account03 {
		set{ m_Account03 = value;
		OnPropertyChanged("Account03"); }
		get { return m_Account03; }
	}

	public int Account04 {
		set{ m_Account04 = value;
		OnPropertyChanged("Account04"); }
		get { return m_Account04; }
	}

	public int Account05 {
		set{ m_Account05 = value;
		OnPropertyChanged("Account05"); }
		get { return m_Account05; }
	}

	public int Account18 {
		set{ m_Account18 = value;
		OnPropertyChanged("Account18"); }
		get { return m_Account18; }
	}

	public int Account19 {
		set{ m_Account19 = value;
		OnPropertyChanged("Account19"); }
		get { return m_Account19; }
	}

	public int Id {
		get { return m_Id; }
	}

	public int Account12 {
		set{ m_Account12 = value;
		OnPropertyChanged("Account12"); }
		get { return m_Account12; }
	}

	public int Account13 {
		set{ m_Account13 = value;
		OnPropertyChanged("Account13"); }
		get { return m_Account13; }
	}

	public int Account14 {
		set{ m_Account14 = value;
		OnPropertyChanged("Account14"); }
		get { return m_Account14; }
	}

	public int Account15 {
		set{ m_Account15 = value;
		OnPropertyChanged("Account15"); }
		get { return m_Account15; }
	}

	public int Account16 {
		set{ m_Account16 = value;
		OnPropertyChanged("Account16"); }
		get { return m_Account16; }
	}

	public int Account17 {
		set{ m_Account17 = value;
		OnPropertyChanged("Account17"); }
		get { return m_Account17; }
	}

	public int Account06 {
		set{ m_Account06 = value;
		OnPropertyChanged("Account06"); }
		get { return m_Account06; }
	}

	public int Account07 {
		set{ m_Account07 = value;
		OnPropertyChanged("Account07"); }
		get { return m_Account07; }
	}

	public int Account08 {
		set{ m_Account08 = value;
		OnPropertyChanged("Account08"); }
		get { return m_Account08; }
	}

	public int Account09 {
		set{ m_Account09 = value;
		OnPropertyChanged("Account09"); }
		get { return m_Account09; }
	}

	public int Account10 {
		set{ m_Account10 = value;
		OnPropertyChanged("Account10"); }
		get { return m_Account10; }
	}

	public int Account11 {
		set{ m_Account11 = value;
		OnPropertyChanged("Account11"); }
		get { return m_Account11; }
	}

}
}
