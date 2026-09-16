using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class OpStmt : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_OpStmt";

	public const string SQL_COL_DETAIL = "Detail";
	public const string SQL_COL_CURRENT = "Current";
	public const string SQL_COL_PRIOR = "Prior";
	public const string SQL_COL_PERCENT = "Percent";
	public const string SQL_COL_Y_T_D = "YTD";
	public const string SQL_COL_ZERO = "Zero";
	public const string SQL_COL_ID = "Id";
	public const string SQL_COL_REPORT_NAME = "ReportName";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_DETAIL + "]," +
            "[" + SQL_COL_CURRENT + "]," +
            "[" + SQL_COL_PRIOR + "]," +
            "[" + SQL_COL_PERCENT + "]," +
            "[" + SQL_COL_Y_T_D + "]," +
            "[" + SQL_COL_ZERO + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_REPORT_NAME + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_DETAIL + "]," +
			"[" + SQL_COL_CURRENT + "]," +
			"[" + SQL_COL_PRIOR + "]," +
			"[" + SQL_COL_PERCENT + "]," +
			"[" + SQL_COL_Y_T_D + "]," +
			"[" + SQL_COL_ZERO + "]," +
			"[" + SQL_COL_ID + "]," +
			"[" + SQL_COL_REPORT_NAME + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_DETAIL + "]," +
			"[" + SQL_COL_CURRENT + "]," +
			"[" + SQL_COL_PRIOR + "]," +
			"[" + SQL_COL_PERCENT + "]," +
			"[" + SQL_COL_Y_T_D + "]," +
			"[" + SQL_COL_ZERO + "]," +
			"[" + SQL_COL_REPORT_NAME + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_DETAIL + "]=?," +
			"[" + SQL_COL_CURRENT + "]=?," +
			"[" + SQL_COL_PRIOR + "]=?," +
			"[" + SQL_COL_PERCENT + "]=?," +
			"[" + SQL_COL_Y_T_D + "]=?," +
			"[" + SQL_COL_ZERO + "]=?," +
			"[" + SQL_COL_REPORT_NAME + "]='?'" +
			" WHERE " + "[" + SQL_COL_ID + "] = ?;";

		static string SQL_LOAD_MAX_ID = "SELECT " +
				"[" + SQL_COL_DETAIL + "]," +
				"[" + SQL_COL_CURRENT + "]," +
				"[" + SQL_COL_PRIOR + "]," +
				"[" + SQL_COL_PERCENT + "]," +
				"[" + SQL_COL_Y_T_D + "]," +
				"[" + SQL_COL_ZERO + "]," +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_REPORT_NAME + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_ID + "] = " +
				"(SELECT [" + SQL_COL_ID + "] = " +
				"MAX(" + SQL_COL_ID + ") FROM " + TABLE + ")";	

		protected short m_Detail;
	protected short m_Current;
	protected short m_Prior;
	protected short m_Percent;
	protected short m_YTD;
	protected short m_Zero;
	protected int m_Id;
	protected string m_ReportName;
	protected bool m_isChanged;

	public OpStmt() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public OpStmt(short Detail,short Current,short Prior,short Percent,short YTD,short Zero,int Id,string ReportName) : 
			base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_Detail = Detail;
		m_Current = Current;
		m_Prior = Prior;
		m_Percent = Percent;
		m_YTD = YTD;
		m_Zero = Zero;
		m_Id = Id;
		m_ReportName = ReportName;
	}

	protected OpStmt fill(DataRow dr)
	{
		try {
			m_Detail = (short) dr[SQL_COL_DETAIL];
			m_Current = (short) dr[SQL_COL_CURRENT];
			m_Prior = (short) dr[SQL_COL_PRIOR];
			m_Percent = (short) dr[SQL_COL_PERCENT];
			m_YTD = (short) dr[SQL_COL_Y_T_D];
			m_Zero = (short) dr[SQL_COL_ZERO];
			m_Id = (int) dr[SQL_COL_ID];
			m_ReportName = (string) (Convert.IsDBNull(dr[SQL_COL_REPORT_NAME]) ? "" : dr[SQL_COL_REPORT_NAME]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static OpStmt load( int m_Id)
	{
		OpStmt obj  = new OpStmt();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static OpStmt[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		OpStmt obj = new OpStmt();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new OpStmt();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (OpStmt[])list.ToArray(typeof(OpStmt));
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
			string[] values = {m_Detail.ToString(), m_Current.ToString(), m_Prior.ToString(), m_Percent.ToString(), m_YTD.ToString(), m_Zero.ToString(), m_ReportName, m_Id.ToString()};
			if (this.updateRecord(m_Id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Detail.ToString(), m_Current.ToString(), m_Prior.ToString(), m_Percent.ToString(), m_YTD.ToString(), m_Zero.ToString(), m_ReportName};
			m_Id = this.insertRecord(values);
			if (m_Id >= 0) {
				return true;
			}
		}
		return false;

	}

		public static OpStmt load_MaxId()
		{
			OpStmt obj = new OpStmt();
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_MAX_ID, null);
			if (ds != null)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			}
			else
				return null;

		}



		private void OnPropertyChanged(string info) {
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			m_isChanged = true;
		}
	}

	// Properties
	public short Detail {
		set{ m_Detail = value;
		OnPropertyChanged("Detail"); }
		get { return m_Detail; }
	}

	public short Current {
		set{ m_Current = value;
		OnPropertyChanged("Current"); }
		get { return m_Current; }
	}

	public short Prior {
		set{ m_Prior = value;
		OnPropertyChanged("Prior"); }
		get { return m_Prior; }
	}

	public short Percent {
		set{ m_Percent = value;
		OnPropertyChanged("Percent"); }
		get { return m_Percent; }
	}

	public short YTD {
		set{ m_YTD = value;
		OnPropertyChanged("YTD"); }
		get { return m_YTD; }
	}

	public short Zero {
		set{ m_Zero = value;
		OnPropertyChanged("Zero"); }
		get { return m_Zero; }
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
