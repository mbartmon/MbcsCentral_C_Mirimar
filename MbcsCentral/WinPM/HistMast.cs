using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
namespace MbcsCentral.WinPM
{
	public class Histmast : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_Histmast";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
		public const string SQL_COL_TENANT_NAME = "Tenant Name";
		public const string SQL_COL_APARTMENT = "Apartment";
        public const string SQL_COL_CLIENTID = "clientId";

		public const string SQL_COL_ID = "ID";

		static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NAME + "]," + 
                                "[" + SQL_COL_APARTMENT + "]," + 
                                "[" + SQL_COL_ID + "]" + 
                                " FROM " + TABLE + " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?;";

		static string SQL_LOAD_ALL = " SELECT " + 
                                     "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                     "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                     "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                     "[" + SQL_COL_TENANT_NAME + "]," + 
                                     "[" + SQL_COL_APARTMENT + "]," + 
                                     "[" + SQL_COL_ID + "]" + 
                                     " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NAME + "]," + 
                                    "[" + SQL_COL_APARTMENT + "]," + 
                                    "[" + SQL_COL_CLIENTID + "]" +
                                    ") VALUES ('?', '?', ?, '?', '?', ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," + 
                                    "[" + SQL_COL_TENANT_NAME + "]='?'," + 
                                    "[" + SQL_COL_APARTMENT + "]='?'" + 
                                    " WHERE [" + SQL_COL_ID + "]=?";

		static string SQL_LOAD_BY_BUILDING_TENANT = " SELECT " + 
                                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NAME + "]," + 
                                                    "[" + SQL_COL_APARTMENT + "]," + 
                                                    "[" + SQL_COL_ID + "]" + 
                                                    " FROM " + TABLE + 
                                                    " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" +
                                                    " AND [" + SQL_COL_TENANT_NUMBER + "] = '?';";

		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected int m_SequenceNumber;
		protected string m_TenantName;
		protected string m_Apartment;
        protected int m_clientId = 1;
		protected int m_ID;

		protected bool m_isChanged;
		public Histmast() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Histmast(string BuildingNumber, string TenantNumber, int SequenceNumber, string TenantName, string Apartment) : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_SequenceNumber = SequenceNumber;
			m_TenantName = TenantName;
			m_Apartment = Apartment;
		}

		protected Histmast fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
				m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
				m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
				m_ID = (int)dr[SQL_COL_ID];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Histmast load(int m_SequenceNumber)
		{

			try {
				Histmast obj = new Histmast();
				string[] opt = { m_SequenceNumber.ToString() };
				DataSet ds = new DataSet();
				ds = obj.load(SQL_LOAD, opt);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
				DataRow dr = ds.Tables[0].Rows[0];

				return obj.fill(dr);
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Histmast[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Histmast obj = new Histmast();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Histmast();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Histmast[])list.ToArray(typeof(Histmast));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Histmast[] load(string buildingNumber, string tenantNumber)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Histmast obj = new Histmast();
			string[] opt = {
				buildingNumber,
				tenantNumber
			};

			ds = obj.load(SQL_LOAD_BY_BUILDING_TENANT, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Histmast();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Histmast[])list.ToArray(typeof(Histmast));
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

			if (m_ID > 0) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_TenantName,
					m_Apartment,
					m_BuildingNumber,
					m_TenantNumber,
					m_ID.ToString()
				};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_TenantName,
					m_Apartment,
                    m_clientId.ToString()
				};
				m_ID = this.insertRecord(values);
				if (m_ID >= 0) {
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
				cmd.CommandText = "CREATE TABLE Histmast (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Tenant Name TEXT(30),Apartment TEXT(4))";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (SqlException ex) {
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
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool isChanged
        {
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

		public int SequenceNumber {
			get { return m_SequenceNumber; }
			set {
				m_SequenceNumber = value;
				OnPropertyChanged("SequenceNumber");
			}
		}

		public string TenantName {
			get { return m_TenantName; }
			set {
				m_TenantName = value;
				OnPropertyChanged("TenantName");
			}
		}

		public string Apartment {
			get { return m_Apartment; }
			set {
				m_Apartment = value;
				OnPropertyChanged("Apartment");
			}
		}

		public int ID {
			get { return m_ID; }
		}

	}
}
