using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
namespace WinTRM
{

	public class Types : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;


		static string TABLE = "Types";
		public const string SQL_COL_TYPE = "Type";
		public const string SQL_COL_DESCRIPTION = "Description";
		public const string SQL_COL_BILLING_NAME = "Billing Name";
		public const string SQL_COL_PRINT_BILL = "Print Bill";
		public const string SQL_COL_LEASE = "Lease";

		public const string SQL_COL_PRINT_APT = "PrintApt";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_DESCRIPTION + "]," + "[" + SQL_COL_BILLING_NAME + "]," + "[" + SQL_COL_PRINT_BILL + "]," + "[" + SQL_COL_LEASE + "]," + "[" + SQL_COL_PRINT_APT + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_TYPE + " = '?';";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_DESCRIPTION + "]," + "[" + SQL_COL_BILLING_NAME + "]," + "[" + SQL_COL_PRINT_BILL + "]," + "[" + SQL_COL_LEASE + "]," + "[" + SQL_COL_PRINT_APT + "]" + " FROM " + TABLE + " ORDER BY " + SQL_COL_DESCRIPTION + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_DESCRIPTION + "]," + "[" + SQL_COL_BILLING_NAME + "]," + "[" + SQL_COL_PRINT_BILL + "]," + "[" + SQL_COL_LEASE + "]" + "[" + SQL_COL_PRINT_APT + "]" + ") VALUES ('?', '?', '?', ?, ??);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_TYPE + "]='?'," + "[" + SQL_COL_DESCRIPTION + "]='?'," + "[" + SQL_COL_BILLING_NAME + "]='?'," + "[" + SQL_COL_PRINT_BILL + "]=?," + "[" + SQL_COL_LEASE + "]=?" + "[" + SQL_COL_PRINT_APT + "]=?" + " WHERE " + SQL_COL_TYPE + " = '?';";
		protected string m_Type;
		protected string m_Description;
		protected string m_BillingName;
		protected bool m_PrintBill;
		protected bool m_Lease;
		protected bool m_PrintApt;

		protected bool m_isChanged;
		public Types() : base(TABLE, SQL_COL_TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
		}

		public Types(string Type, string Description, string BillingName, bool PrintBill, bool Lease, bool PrintApt) : base(TABLE, SQL_COL_TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
			m_Type = Type;
			m_Description = Description;
			m_BillingName = BillingName;
			m_PrintBill = PrintBill;
			m_Lease = Lease;
			m_PrintApt = PrintApt;
		}

		protected Types fill(DataRow dr)
		{

			try {
				m_Type = (Information.isDBnull(dr(SQL_COL_TYPE)) ? "" : dr(SQL_COL_TYPE));
				m_Description = (Information.isDBnull(dr(SQL_COL_DESCRIPTION)) ? "" : dr(SQL_COL_DESCRIPTION));
				m_BillingName = (Information.isDBnull(dr(SQL_COL_BILLING_NAME)) ? "" : dr(SQL_COL_BILLING_NAME));
				m_PrintBill = dr(SQL_COL_PRINT_BILL);
				m_Lease = dr(SQL_COL_LEASE);
				m_PrintApt = dr(SQL_COL_PRINT_APT);
			} catch (OleDbException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Types load(string m_Type)
		{

			Types obj = new Types();
			string[] opt = { m_Type };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables(0).Rows(0);

			return obj.fill(dr);

		}

		public static Types[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Types obj = new Types();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = default(DataRow);
			foreach ( dr in ds.Tables(0).Rows) {
				obj = new Types();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Types[])list.ToArray(typeof(Types));
			} catch (OleDbException ex) {
				return null;
			}

		}

		public bool save()
		{

			if (m_Type > 0) {
				string[] values = {
					m_Type,
					m_Description,
					m_BillingName,
					m_PrintBill,
					m_Lease,
					m_PrintApt,
					m_Type
				};
				if (this.updateRecord(m_Type, values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_Type,
					m_Description,
					m_BillingName,
					m_PrintBill,
					m_Lease,
					m_PrintApt
				};
				m_Type = this.insertRecord(values);
				if (m_Type >= 0) {
					return true;
				}
			}
			return false;

		}


		public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try {
				CN.Open();
				OleDb.OleDbCommand cmd = new OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Types (Type TEXT(2),Description TEXT(18),Billing Name TEXT(18),Print Bill YESNO,Lease YESNO,PrintApt YESNO)";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (OleDb.OleDbException ex) {
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
		public string Type {
			get { return m_Type; }
			set {
				m_Type = value;
				OnPropertyChanged("Type");
			}
		}

		public string Description {
			get { return m_Description; }
			set {
				m_Description = value;
				OnPropertyChanged("Description");
			}
		}

		public string BillingName {
			get { return m_BillingName; }
			set {
				m_BillingName = value;
				OnPropertyChanged("BillingName");
			}
		}

		public bool PrintBill {
			get { return m_PrintBill; }
			set {
				m_PrintBill = value;
				OnPropertyChanged("PrintBill");
			}
		}

		public bool Lease {
			get { return m_Lease; }
			set {
				m_Lease = value;
				OnPropertyChanged("Lease");
			}
		}

		public bool PrintApt {
			get { return m_PrintApt; }
			set {
				m_PrintApt = value;
				OnPropertyChanged("PrintApt");
			}
		}

	}
}
