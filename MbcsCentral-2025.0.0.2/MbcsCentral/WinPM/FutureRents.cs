using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
public class FutureRents : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "pm_FutureRents";

	public const string SQL_COL_FUTURE_DATE = "FutureDate";
	public const string SQL_COL_FUTURE_AMOUNT = "FutureAmount";
	public const string SQL_COL_FUTURE_LEGAL = "FutureLegal";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_TENANT_SEQUENCE = "TenantSequence";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_FUTURE_DATE + "]," +
			"[" + SQL_COL_FUTURE_AMOUNT + "]," +
			"[" + SQL_COL_FUTURE_LEGAL + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_TENANT_SEQUENCE + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_FUTURE_DATE + "]," +
			"[" + SQL_COL_FUTURE_AMOUNT + "]," +
			"[" + SQL_COL_FUTURE_LEGAL + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_TENANT_SEQUENCE + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_FUTURE_DATE + "]," +
			"[" + SQL_COL_FUTURE_AMOUNT + "]," +
			"[" + SQL_COL_FUTURE_LEGAL + "]," +
			"[" + SQL_COL_TENANT_SEQUENCE + "]" +
			") VALUES ('?', ?, ?, ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_FUTURE_DATE + "]='?'," +
			"[" + SQL_COL_FUTURE_AMOUNT + "]=?," +
			"[" + SQL_COL_FUTURE_LEGAL + "]=?," +
			"[" + SQL_COL_TENANT_SEQUENCE + "]=?" +
			" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

    static string SQL_LOAD_BY_SEQUENCE = "SELECT " +
            "[" + SQL_COL_FUTURE_DATE + "]," +
            "[" + SQL_COL_FUTURE_AMOUNT + "]," +
            "[" + SQL_COL_FUTURE_LEGAL + "]," +
            "[" + SQL_COL_I_D + "]," +
            "[" + SQL_COL_TENANT_SEQUENCE + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_TENANT_SEQUENCE + "] = ?" +
            " ORDER BY " + SQL_COL_FUTURE_DATE;

    protected DateTime m_FutureDate;
	protected decimal m_FutureAmount;
	protected decimal m_FutureLegal;
	protected int m_ID;
	protected int m_TenantSequence;
	protected bool m_isChanged;

	public FutureRents() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public FutureRents(DateTime FutureDate,decimal FutureAmount,decimal FutureLegal,int ID,int TenantSequence) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_FutureDate = FutureDate;
		m_FutureAmount = FutureAmount;
		m_FutureLegal = FutureLegal;
		m_ID = ID;
		m_TenantSequence = TenantSequence;
	}

	protected FutureRents fill(DataRow dr)
	{
		try {
			m_FutureDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_FUTURE_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_FUTURE_DATE]);
			m_FutureAmount = (decimal) dr[SQL_COL_FUTURE_AMOUNT];
			m_FutureLegal = (decimal) dr[SQL_COL_FUTURE_LEGAL];
			m_ID = (int) dr[SQL_COL_I_D];
			m_TenantSequence = (int) dr[SQL_COL_TENANT_SEQUENCE];
		} catch (SqlException ex) {
		}
		return this;

	}

	public static FutureRents load( int m_ID)
	{
		FutureRents obj  = new FutureRents();
		string[] opt = {m_ID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static FutureRents[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		FutureRents obj = new FutureRents();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new FutureRents();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (FutureRents[])list.ToArray(typeof(FutureRents));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static FutureRents[] loadBySequence(int SequenceNumber)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        FutureRents obj = new FutureRents();

        string[] opt = { SequenceNumber.ToString() };
        ds = obj.load(SQL_LOAD_BY_SEQUENCE, opt);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return null;
        obj = null;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new FutureRents();
            obj.fill(dr);
            list.Add(obj);
            obj = null;
        }
        try
        {
            return (FutureRents[])list.ToArray(typeof(FutureRents));
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
		if (m_ID > 0 ) {
			string[] values = {m_FutureDate.ToString(), m_FutureAmount.ToString(), m_FutureLegal.ToString(), m_TenantSequence.ToString(), m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_FutureDate.ToString(), m_FutureAmount.ToString(), m_FutureLegal.ToString(), m_TenantSequence.ToString()};
			m_ID = this.insertRecord(values);
			if (m_ID >= 0) {
				return true;
			}
		}
		return false;

	}

	private void OnPropertyChanged(string info) {
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			IsChanged = true;
		}
	}

        // Properties
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged { get; set; }

        public DateTime FutureDate {
		set{ m_FutureDate = value;
		OnPropertyChanged("FutureDate"); }
		get { return m_FutureDate; }
	}

	public decimal FutureAmount {
		set{ m_FutureAmount = value;
		OnPropertyChanged("FutureAmount"); }
		get { return m_FutureAmount; }
	}

	public decimal FutureLegal {
		set{ m_FutureLegal = value;
		OnPropertyChanged("FutureLegal"); }
		get { return m_FutureLegal; }
	}

	public int ID {
		get { return m_ID; }
	}

	public int TenantSequence {
		set{ m_TenantSequence = value;
		OnPropertyChanged("TenantSequence"); }
		get { return m_TenantSequence; }
	}

}
}
