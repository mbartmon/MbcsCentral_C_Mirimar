using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
namespace MbcsCentralLogCleaner
{

    public class HistChg : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_HistChg";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
		public const string SQL_COL_CHARGE_NUMBER = "Charge Number";
		public const string SQL_COL_DATE = "Date";
		public const string SQL_COL_CODE = "Code";
		public const string SQL_COL_AMOUNT = "Amount";
		public const string SQL_COL_ARREARS = "Arrears";

		public const string SQL_COL_HIST_CHARGE_ID = "hist_chg_id";

		static string SQL_LOAD = " SELECT " + 
								 "[" + SQL_COL_BUILDING_NUMBER + "]," + 
								 "[" + SQL_COL_TENANT_NUMBER + "]," + 
								 "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
								 "[" + SQL_COL_CHARGE_NUMBER + "]," + 
								 "[" + SQL_COL_DATE + "]," + 
								 "[" + SQL_COL_CODE + "]," + 
								 "[" + SQL_COL_AMOUNT + "]," + 
								 "[" + SQL_COL_ARREARS + "]," + 
								 "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
								 " FROM " + TABLE + 
								 " WHERE " + SQL_COL_HIST_CHARGE_ID + "=?;";

		static string SQL_LOAD_ALL = " SELECT " + 
									 "[" + SQL_COL_BUILDING_NUMBER + "]," + 
									 "[" + SQL_COL_TENANT_NUMBER + "]," + 
									 "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
									 "[" + SQL_COL_CHARGE_NUMBER + "]," + 
									 "[" + SQL_COL_DATE + "]," + 
									 "[" + SQL_COL_CODE + "]," + 
									 "[" + SQL_COL_AMOUNT + "]," + 
									 "[" + SQL_COL_ARREARS + "]," + 
									 "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
									 " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
								   "[" + SQL_COL_BUILDING_NUMBER + "]," + 
								   "[" + SQL_COL_TENANT_NUMBER + "]," + 
								   "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
								   "[" + SQL_COL_CHARGE_NUMBER + "]," + 
								   "[" + SQL_COL_DATE + "]," + 
								   "[" + SQL_COL_CODE + "]," + 
								   "[" + SQL_COL_AMOUNT + "]," + 
								   "[" + SQL_COL_ARREARS + "]" + 
								   ") VALUES ('?', '?', ?, ?, '?', '?', ?, ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
								   " SET " + 
								   "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + 
								   "[" + SQL_COL_TENANT_NUMBER + "]='?'," + 
								   "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," + 
								   "[" + SQL_COL_CHARGE_NUMBER + "]=?," + 
								   "[" + SQL_COL_DATE + "]='?'," + 
								   "[" + SQL_COL_CODE + "]='?'," + 
								   "[" + SQL_COL_AMOUNT + "]=?," + 
								   "[" + SQL_COL_ARREARS + "]=?" + 
								   " WHERE " + SQL_COL_CHARGE_NUMBER + " = ?";

		static string SQL_LOAD_BY_TENANT_ID_DATE = " SELECT " + 
												   "[" + SQL_COL_BUILDING_NUMBER + "]," + 
												   "[" + SQL_COL_TENANT_NUMBER + "]," + 
												   "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
												   "[" + SQL_COL_CHARGE_NUMBER + "]," + 
												   "[" + SQL_COL_DATE + "]," + 
												   "[" + SQL_COL_CODE + "]," + 
												   "[" + SQL_COL_AMOUNT + "]," + 
												   "[" + SQL_COL_ARREARS + "]," + 
												   "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
												   " FROM " + TABLE + 
												   " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?" + " AND " + SQL_COL_DATE + " >= '?'" + " AND " + SQL_COL_DATE + " <= '?'" + " ORDER BY " + SQL_COL_DATE;

		static string SQL_LOAD_FOR_EOM = "SELECT * " + 
										 " FROM " + TABLE +
										 " WHERE " + SQL_COL_SEQUENCE_NUMBER + " = ? " + 
										 " AND " + SQL_COL_CODE + "= '='" +
										 " AND " + SQL_COL_DATE + " BETWEEN '?'" +
                                         " AND '?'";

        static string SQL_LOAD_BY_DATE = " SELECT " +
                                                   "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                                   "[" + SQL_COL_TENANT_NUMBER + "]," +
                                                   "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                                   "[" + SQL_COL_CHARGE_NUMBER + "]," +
                                                   "[" + SQL_COL_DATE + "]," +
                                                   "[" + SQL_COL_CODE + "]," +
                                                   "[" + SQL_COL_AMOUNT + "]," +
                                                   "[" + SQL_COL_ARREARS + "]," +
                                                   "[" + SQL_COL_HIST_CHARGE_ID + "]" +
                                                   " FROM " + TABLE +
                                                   " WHERE [" + SQL_COL_DATE + "] <= '?'";

        static string SQL_DELETE_BY_DATE = "DELETE FROM " + TABLE +
                                   " WHERE [date] <= '?'"; 

        protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected long m_SequenceNumber;
		protected long m_ChargeNumber;
		protected DateTime m_Date;
		protected string m_Code;
		protected decimal m_Amount;
		protected decimal m_Arrears;
		protected long m_histChgId;

		protected bool m_isChanged;

		public HistChg() : base(TABLE, SQL_COL_HIST_CHARGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{

		}


		public HistChg(string BuildingNumber, string TenantNumber, int SequenceNumber, int ChargeNumber, DateTime chgDate, string Code, decimal Amount, decimal Arrears) : base(TABLE, SQL_COL_HIST_CHARGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_SequenceNumber = SequenceNumber;
			m_ChargeNumber = ChargeNumber;
			m_Date = chgDate;
			m_Code = Code;
			m_Amount = Amount;
			m_Arrears = Arrears;

		}

		protected HistChg fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_SequenceNumber = (long)dr[SQL_COL_SEQUENCE_NUMBER];
				m_ChargeNumber = (long)dr[SQL_COL_CHARGE_NUMBER];
				m_Date = (DateTime)dr[SQL_COL_DATE];
				m_Code = (string)(Information.IsDBNull(dr[SQL_COL_CODE]) ? "" : dr[SQL_COL_CODE]);
				m_Amount = (decimal)dr[SQL_COL_AMOUNT];
				m_Arrears = (decimal)dr[SQL_COL_ARREARS];
				m_histChgId = (long)dr[SQL_COL_HIST_CHARGE_ID];
			} catch (SqlException ) {
			} catch (System.Exception ) {
			}
			return this;

		}

		public static HistChg load(int chargeNumber)
		{

			HistChg obj = new HistChg();
			string[] opt = { chargeNumber.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static HistChg[] loadByDate()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			HistChg obj = new HistChg();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new HistChg();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (HistChg[])list.ToArray(typeof(HistChg));
			} catch (SqlException ) {
				return null;
			}

		}

		public static HistChg loadForEom(long tenantId, DateTime start, DateTime end)
		{
			HistChg obj = new HistChg();
			string[] opt = { tenantId.ToString(), start.ToString(), end.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_FOR_EOM, opt);
			if (ds != null)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			}
			else
				return null;
		}

		public static HistChg[] loadByTenantIdDate(long tenantId, System.DateTime startDate, System.DateTime endDate)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			HistChg obj = new HistChg();
			string[] opt = {
				tenantId.ToString(),
				startDate.ToString(),
				endDate.ToString()
			};

			ds = obj.load(SQL_LOAD_BY_TENANT_ID_DATE, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new HistChg();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (HistChg[])list.ToArray(typeof(HistChg));
			} catch (SqlException ) {
				return null;
			}

		}

        public static HistChg[] loadByDate(System.DateTime testDate)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            HistChg obj = new HistChg();
            string[] opt = { testDate.ToString() };

            ds = obj.load(SQL_LOAD_BY_DATE, opt);
            obj = null;
            DataRow dr = null;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new HistChg();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (HistChg[])list.ToArray(typeof(HistChg));
            }
            catch (SqlException )
            {
                return null;
            }

        }

        public static long DeleteByDate(DateTime date)
        {
            HistChg obj = new HistChg();
            string[] s = { date.ToString("yyyyMMdd") };
            return obj.queryWithCount(SQL_DELETE_BY_DATE, s);
        }

        public bool save()
		{

			if (m_histChgId > 0) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_ChargeNumber.ToString(),
					m_Date.ToString(),
					m_Code,
					m_Amount.ToString(),
					m_Arrears.ToString()
				};
				if (this.updateRecord(m_histChgId.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_ChargeNumber.ToString(),
					m_Date.ToString(),
					m_Code,
					m_Amount.ToString(),
					m_Arrears.ToString(),
					m_histChgId.ToString()
				};
				m_histChgId = this.insertRecord(values);
				if (m_histChgId >= 0) {
					return true;
				}
			}
			return false;

		}


		public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try {
				CN.Open();
				System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE HistChg (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Charge Number LONG,Date DATETIME,Code TEXT(1),Amount CURRENCY,Arrears CURRENCY)";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (SqlException ) {
				return false;
			} finally {
				try {
					CN.Close();
				} catch {
				}
			}

			return true;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_isChanged = true;
		}

// Property Routines
		public bool isChanged {
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}
		public string BuildingNumber {
			get { return m_BuildingNumber; }
			set {
				m_BuildingNumber = value;
				OnPropertyChanged("BuildingNumber");
			}
		}

		public string TenantNumber {
			get { return m_TenantNumber; }
			set {
				m_TenantNumber = value;
				OnPropertyChanged("TenantNumber");
			}
		}

		public long SequenceNumber {
			get { return m_SequenceNumber; }
			set {
				m_SequenceNumber = value;
				OnPropertyChanged("SequenceNumber");
			}
		}

		public long ChargeNumber {
			get { return m_ChargeNumber; }
			set {
				m_ChargeNumber = value;
				OnPropertyChanged("ChargeNumber");
			}
		}

		public DateTime ChargeDate {
			get { return m_Date; }
			set {
				m_Date = value;
				OnPropertyChanged("Date");
			}
		}

		public string Code {
			get { return m_Code; }
			set {
				m_Code = value;
				OnPropertyChanged("Code");
			}
		}

		public decimal Amount {
			get { return m_Amount; }
			set {
				m_Amount = value;
				OnPropertyChanged("Amount");
			}
		}

		public decimal Arrears {
			get { return m_Arrears; }
			set {
				m_Arrears = value;
				OnPropertyChanged("Arrears");
			}
		}

		public long HistChgId {
			get { return m_histChgId; }
		}

	}
}
