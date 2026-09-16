using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
namespace MbcsCentralLogCleaner
{

	public class HistPay : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_HistPay";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
		public const string SQL_COL_DATE = "Date";
		public const string SQL_COL_AMOUNT = "Amount";
		public const string SQL_COL_CODE = "Code";
		public const string SQL_COL_HIST_PAY_ID = "hist_pay_id";
        public const string SQL_COL_CLIENTID = "clientId";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_DATE + "]," + "[" + SQL_COL_AMOUNT + "]," + "[" + SQL_COL_CODE + "]" + "[" + SQL_COL_HIST_PAY_ID + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_HIST_PAY_ID + "=?";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_DATE + "]," + "[" + SQL_COL_AMOUNT + "]," + "[" + SQL_COL_CODE + "]," + "[" + SQL_COL_HIST_PAY_ID + "]" + " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
            "[" + SQL_COL_BUILDING_NUMBER + "]," + 
            "[" + SQL_COL_TENANT_NUMBER + "]," + 
            "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
            "[" + SQL_COL_DATE + "]," + 
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_CODE + "]," + 
            SQL_COL_CLIENTID + 
            ") VALUES ('?', '?', ?, '?', ?, '?', ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + "[" + SQL_COL_TENANT_NUMBER + "]='?'," + "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," + "[" + SQL_COL_DATE + "]=#?#," + "[" + SQL_COL_AMOUNT + "]=?" + "[" + SQL_COL_CODE + "]='?'" + " WHERE " + SQL_COL_HIST_PAY_ID + "=?";

		static string SQL_LOAD_BY_TENANT_DATE = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_DATE + "]," + "[" + SQL_COL_AMOUNT + "]," + "[" + SQL_COL_CODE + "]," + "[" + SQL_COL_HIST_PAY_ID + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?" + " AND " + SQL_COL_DATE + ">= '?'" + " AND " + SQL_COL_DATE + "<= '?'";

        static string SQL_LOAD_BY_DATE = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_DATE + "]," + "[" + SQL_COL_AMOUNT + "]," + "[" + SQL_COL_CODE + "]," + "[" + SQL_COL_HIST_PAY_ID + "]" + 
                                         " FROM " + TABLE + 
                                         " WHERE [" + SQL_COL_DATE + "<= '?'";

        static string SQL_DELETE_BY_DATE = "DELETE FROM " + TABLE +
                                  " WHERE [date] <= '?'";

        protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected long m_SequenceNumber;
		protected DateTime m_Date;
		protected decimal m_Amount;
		protected string m_Code;
		protected long m_histPayId;
        protected int m_clientId;

		protected bool m_isChanged;
		public HistPay() : base(TABLE, SQL_COL_HIST_PAY_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public HistPay(string BuildingNumber, string TenantNumber, long SequenceNumber, DateTime payDate, decimal Amount, string Code, int clientId = 0) : base(TABLE, SQL_COL_HIST_PAY_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_SequenceNumber = SequenceNumber;
			m_Date = payDate;
			m_Amount = Amount;
			m_Code = Code;
            if (clientId == 0)
                m_clientId = 1;
            else
                m_clientId = clientId;
        }

		protected HistPay fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_SequenceNumber = (long)dr[SQL_COL_SEQUENCE_NUMBER];
				m_Date = (DateTime)dr[SQL_COL_DATE];
				m_Amount = (decimal)dr[SQL_COL_AMOUNT];
				m_Code = (string)(Information.IsDBNull(dr[SQL_COL_CODE]) ? "" : dr[SQL_COL_CODE]);
				m_histPayId = (long)dr[SQL_COL_HIST_PAY_ID];
                m_clientId = (int) dr[SQL_COL_CLIENTID];
			} catch (SqlException ) {
			} catch (System.Exception ) {
			}
			return this;

		}

		public static HistPay load(long histPayId)
		{

			HistPay obj = new HistPay();
			string[] opt = { histPayId.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static HistPay[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			HistPay obj = new HistPay();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new HistPay();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (HistPay[])list.ToArray(typeof(HistPay));
			} catch (SqlException ) {
				return null;
			}

		}

		public static HistPay[] loadByTenantDate(long tenantID, System.DateTime startDate, System.DateTime endDate)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			HistPay obj = new HistPay();
			string[] opt = {
				tenantID.ToString(),
				startDate.ToString(),
				endDate.ToString()
			};

			ds = obj.load(SQL_LOAD_BY_TENANT_DATE, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new HistPay();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (HistPay[])list.ToArray(typeof(HistPay));
			} catch (SqlException ) {
				return null;
			}

		}

        public static HistPay[] loadByDate( System.DateTime testDate)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            HistPay obj = new HistPay();
            string[] opt = { testDate.ToString() };

            ds = obj.load(SQL_LOAD_BY_DATE, opt);
            obj = null;
            DataRow dr = null;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new HistPay();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (HistPay[])list.ToArray(typeof(HistPay));
            }
            catch (SqlException )
            {
                return null;
            }

        }

        public bool save()
		{

			if (m_histPayId > 0) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_Date.ToString(),
					m_Amount.ToString(),
					m_Code,
                    m_clientId.ToString(),
                    m_histPayId.ToString()
				};
				if (this.updateRecord(m_histPayId.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_Date.ToString(),
					m_Amount.ToString(),
					m_Code,
                    m_clientId.ToString()
				};
				m_histPayId = this.insertRecord(values);
				if (m_histPayId >= 0) {
					return true;
				}
			}
			return false;

		}

        public static long DeleteByDate(DateTime date)
        {
            HistPay obj = new HistPay();
            string[] s = { date.ToString("yyyyMMdd") };
            return obj.queryWithCount(SQL_DELETE_BY_DATE, s);
        }

        public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try {
				CN.Open();
				System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE HistPay (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Date DATETIME,Amount CURRENCY,Code TEXT(1))";
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

		public DateTime PayDate {
			get { return m_Date; }
			set {
				m_Date = value;
				OnPropertyChanged("Date");
			}
		}

		public decimal Amount {
			get { return m_Amount; }
			set {
				m_Amount = value;
				OnPropertyChanged("Amount");
			}
		}

		public string Code {
			get { return m_Code; }
			set {
				m_Code = value;
				OnPropertyChanged("Code");
			}
		}

        public int ClientId
        {
            get { return m_clientId; }
            set { m_clientId = value;
            OnPropertyChanged( "ClientId" );
            }
        }

        public long HistPayID
        {
            get { return m_histPayId; }
        }
	}
}
