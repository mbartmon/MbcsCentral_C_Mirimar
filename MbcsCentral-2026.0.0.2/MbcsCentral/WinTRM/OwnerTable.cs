using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
namespace WinTRM
{

	public class OwnerTable : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;


		static string TABLE = "Owner";
		public const string SQL_COL_OWNER_NUMBER = "Owner Number";
		public const string SQL_COL_ADDRESS_1 = "Address 1";
		public const string SQL_COL_ADDRESS_2 = "Address 2";
		public const string SQL_COL_TELEPHONE = "Telephone";
		public const string SQL_COL_FEDERAL_I_D = "FederalID";
		public const string SQL_COL_OWNER_NAME = "Owner Name";

		public const string SQL_COL_BILLING_NAME = "Billing Name";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_TELEPHONE + "]," + "[" + SQL_COL_FEDERAL_I_D + "]," + "[" + SQL_COL_OWNER_NAME + "]," + "[" + SQL_COL_BILLING_NAME + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_OWNER_NUMBER + " = ?;";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_TELEPHONE + "]," + "[" + SQL_COL_FEDERAL_I_D + "]," + "[" + SQL_COL_OWNER_NAME + "]," + "[" + SQL_COL_BILLING_NAME + "]" + " FROM " + TABLE + " ORDER BY [" + SQL_COL_OWNER_NAME + "];";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_TELEPHONE + "]," + "[" + SQL_COL_FEDERAL_I_D + "]," + "[" + SQL_COL_OWNER_NAME + "]" + "[" + SQL_COL_BILLING_NAME + "]" + ") VALUES (?, '?', '?', '?', '?', '?''?');";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_OWNER_NUMBER + "]=?," + "[" + SQL_COL_ADDRESS_1 + "]='?'," + "[" + SQL_COL_ADDRESS_2 + "]='?'," + "[" + SQL_COL_TELEPHONE + "]='?'," + "[" + SQL_COL_FEDERAL_I_D + "]='?'," + "[" + SQL_COL_OWNER_NAME + "]='?'" + "[" + SQL_COL_BILLING_NAME + "]='?'" + " WHERE " + SQL_COL_OWNER_NUMBER + " = ?;";
		protected Int16 m_OwnerNumber;
		protected string m_Address1;
		protected string m_Address2;
		protected string m_Telephone;
		protected string m_FederalID;
		protected string m_OwnerName;
		protected string m_BillingName;

		protected bool m_isChanged;
		public OwnerTable() : base(TABLE, SQL_COL_OWNER_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
		}

		public OwnerTable(Int16 OwnerNumber, string Address1, string Address2, string Telephone, string FederalID, string OwnerName, string BillingName) : base(TABLE, SQL_COL_OWNER_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
			m_OwnerNumber = OwnerNumber;
			m_Address1 = Address1;
			m_Address2 = Address2;
			m_Telephone = Telephone;
			m_FederalID = FederalID;
			m_OwnerName = OwnerName;
			m_BillingName = BillingName;
		}

		protected OwnerTable fill(DataRow dr)
		{

			try {
				m_OwnerNumber = dr(SQL_COL_OWNER_NUMBER);
				m_Address1 = (Information.isDBnull(dr(SQL_COL_ADDRESS_1)) ? "" : dr(SQL_COL_ADDRESS_1));
				m_Address2 = (Information.isDBnull(dr(SQL_COL_ADDRESS_2)) ? "" : dr(SQL_COL_ADDRESS_2));
				m_Telephone = (Information.isDBnull(dr(SQL_COL_TELEPHONE)) ? "" : dr(SQL_COL_TELEPHONE));
				m_FederalID = (Information.isDBnull(dr(SQL_COL_FEDERAL_I_D)) ? "" : dr(SQL_COL_FEDERAL_I_D));
				m_OwnerName = (Information.isDBnull(dr(SQL_COL_OWNER_NAME)) ? "" : dr(SQL_COL_OWNER_NAME));
				m_BillingName = (Information.isDBnull(dr(SQL_COL_BILLING_NAME)) ? "" : dr(SQL_COL_BILLING_NAME));
			} catch (OleDbException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static OwnerTable load(Int16 m_OwnerNumber)
		{

			OwnerTable obj = new OwnerTable();
			string[] opt = { m_OwnerNumber };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables(0).Rows(0);

			return obj.fill(dr);

		}

		public static OwnerTable[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			OwnerTable obj = new OwnerTable();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = default(DataRow);
			foreach ( dr in ds.Tables(0).Rows) {
				obj = new OwnerTable();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (OwnerTable[])list.ToArray(typeof(OwnerTable));
			} catch (OleDbException ex) {
				return null;
			}

		}

		public bool save()
		{

			if (m_OwnerNumber > 0) {
				string[] values = {
					m_OwnerNumber,
					m_Address1,
					m_Address2,
					m_Telephone,
					m_FederalID,
					m_OwnerName,
					m_BillingName,
					m_OwnerNumber
				};
				if (this.updateRecord(m_OwnerNumber, values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_OwnerNumber,
					m_Address1,
					m_Address2,
					m_Telephone,
					m_FederalID,
					m_OwnerName,
					m_BillingName
				};
				m_OwnerNumber = this.insertRecord(values);
				if (m_OwnerNumber >= 0) {
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
				cmd.CommandText = "CREATE TABLE Owner (Owner Number INTEGER,Address 1 TEXT(30),Address 2 TEXT(30),Telephone TEXT(12),FederalID TEXT(11),Owner Name TEXT(50),Billing Name TEXT(80))";
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
		public Int16 OwnerNumber {
			get { return m_OwnerNumber; }
			set {
				m_OwnerNumber = value;
				OnPropertyChanged("OwnerNumber");
			}
		}

		public string Address1 {
			get { return m_Address1; }
			set {
				m_Address1 = value;
				OnPropertyChanged("Address1");
			}
		}

		public string Address2 {
			get { return m_Address2; }
			set {
				m_Address2 = value;
				OnPropertyChanged("Address2");
			}
		}

		public string Telephone {
			get { return m_Telephone; }
			set {
				m_Telephone = value;
				OnPropertyChanged("Telephone");
			}
		}

		public string FederalID {
			get { return m_FederalID; }
			set {
				m_FederalID = value;
				OnPropertyChanged("FederalID");
			}
		}

		public string OwnerName {
			get { return m_OwnerName; }
			set {
				m_OwnerName = value;
				OnPropertyChanged("OwnerName");
			}
		}

		public string BillingName {
			get { return m_BillingName; }
			set {
				m_BillingName = value;
				OnPropertyChanged("BillingName");
			}
		}

	}
}
