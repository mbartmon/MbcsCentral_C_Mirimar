using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
namespace WinTRM
{

	public class Building : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;


		public static string TABLE = "Building";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_OWNER_NUMBER = "Owner Number";
		public const string SQL_COL_ADDRESS_1 = "Address 1";
		public const string SQL_COL_ADDRESS_2 = "Address 2";
		public const string SQL_COL_AGENT_NAME = "Agent Name";
		public const string SQL_COL_BLOCK_NUMBER = "Block Number";
		public const string SQL_COL_LOT_NUMBER = "Lot Number";
		public const string SQL_COL_SECTION_NUMBER = "Section Number";
		public const string SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER = "Multiple Dwelling Registration Number";
		public const string SQL_COL_R_S_A = "RSA";
		public const string SQL_COL_SENIOR_CITIZEN_NUMBER = "Senior Citizen Number";
		public const string SQL_COL_SUPERINTENDANT_NAME = "Superintendant Name";
		public const string SQL_COL_SUPERINTENDANT_PHONE = "Superintendant Phone";
		public const string SQL_COL_REAL_ESTATE_TAXES = "Real Estate Taxes";
		public const string SQL_COL_MORTGAGE_INTEREST = "Mortgage Interest";
		public const string SQL_COL_TOTAL_SHARES = "Total Shares";
		public const string SQL_COL_MASTER_ACCOUNT = "MasterAccount";
		public const string SQL_COL_AUTH_REP = "AuthRep";
		public const string SQL_COL_FAX_NUMBER = "FaxNumber";

		public const string SQL_COL_AUTH_CODE = "AuthCode";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_AGENT_NAME + "]," + "[" + SQL_COL_BLOCK_NUMBER + "]," + "[" + SQL_COL_LOT_NUMBER + "]," + "[" + SQL_COL_SECTION_NUMBER + "]," + "[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," + "[" + SQL_COL_R_S_A + "]," + "[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," + "[" + SQL_COL_SUPERINTENDANT_NAME + "]," + "[" + SQL_COL_SUPERINTENDANT_PHONE + "]," + "[" + SQL_COL_REAL_ESTATE_TAXES + "]," + "[" + SQL_COL_MORTGAGE_INTEREST + "]," + "[" + SQL_COL_TOTAL_SHARES + "]," + "[" + SQL_COL_MASTER_ACCOUNT + "]," + "[" + SQL_COL_AUTH_REP + "]," + "[" + SQL_COL_FAX_NUMBER + "]," + "[" + SQL_COL_AUTH_CODE + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?';";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_AGENT_NAME + "]," + "[" + SQL_COL_BLOCK_NUMBER + "]," + "[" + SQL_COL_LOT_NUMBER + "]," + "[" + SQL_COL_SECTION_NUMBER + "]," + "[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," + "[" + SQL_COL_R_S_A + "]," + "[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," + "[" + SQL_COL_SUPERINTENDANT_NAME + "]," + "[" + SQL_COL_SUPERINTENDANT_PHONE + "]," + "[" + SQL_COL_REAL_ESTATE_TAXES + "]," + "[" + SQL_COL_MORTGAGE_INTEREST + "]," + "[" + SQL_COL_TOTAL_SHARES + "]," + "[" + SQL_COL_MASTER_ACCOUNT + "]," + "[" + SQL_COL_AUTH_REP + "]," + "[" + SQL_COL_FAX_NUMBER + "]," + "[" + SQL_COL_AUTH_CODE + "]" + " FROM " + TABLE + " ORDER BY [" + SQL_COL_ADDRESS_1 + "];";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_AGENT_NAME + "]," + "[" + SQL_COL_BLOCK_NUMBER + "]," + "[" + SQL_COL_LOT_NUMBER + "]," + "[" + SQL_COL_SECTION_NUMBER + "]," + "[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," + "[" + SQL_COL_R_S_A + "]," + "[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," + "[" + SQL_COL_SUPERINTENDANT_NAME + "]," + "[" + SQL_COL_SUPERINTENDANT_PHONE + "]," + "[" + SQL_COL_REAL_ESTATE_TAXES + "]," + "[" + SQL_COL_MORTGAGE_INTEREST + "]," + "[" + SQL_COL_TOTAL_SHARES + "]," + "[" + SQL_COL_MASTER_ACCOUNT + "]," + "[" + SQL_COL_AUTH_REP + "]," + "[" + SQL_COL_FAX_NUMBER + "]" + "[" + SQL_COL_AUTH_CODE + "]" + ") VALUES ('?', ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, ?, ?, '?', '?', '?''?');";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + "[" + SQL_COL_OWNER_NUMBER + "]=?," + "[" + SQL_COL_ADDRESS_1 + "]='?'," + "[" + SQL_COL_ADDRESS_2 + "]='?'," + "[" + SQL_COL_AGENT_NAME + "]='?'," + "[" + SQL_COL_BLOCK_NUMBER + "]='?'," + "[" + SQL_COL_LOT_NUMBER + "]='?'," + "[" + SQL_COL_SECTION_NUMBER + "]='?'," + "[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]='?'," + "[" + SQL_COL_R_S_A + "]='?'," + "[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]='?'," + "[" + SQL_COL_SUPERINTENDANT_NAME + "]='?'," + "[" + SQL_COL_SUPERINTENDANT_PHONE + "]='?'," + "[" + SQL_COL_REAL_ESTATE_TAXES + "]=?," + "[" + SQL_COL_MORTGAGE_INTEREST + "]=?," + "[" + SQL_COL_TOTAL_SHARES + "]=?," + "[" + SQL_COL_MASTER_ACCOUNT + "]='?'," + "[" + SQL_COL_AUTH_REP + "]='?'," + "[" + SQL_COL_FAX_NUMBER + "]='?'" + "[" + SQL_COL_AUTH_CODE + "]='?'" + " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?';";

		static string SQL_LOAD_BY_OWNER = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_OWNER_NUMBER + "]," + "[" + SQL_COL_ADDRESS_1 + "]," + "[" + SQL_COL_ADDRESS_2 + "]," + "[" + SQL_COL_AGENT_NAME + "]," + "[" + SQL_COL_BLOCK_NUMBER + "]," + "[" + SQL_COL_LOT_NUMBER + "]," + "[" + SQL_COL_SECTION_NUMBER + "]," + "[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," + "[" + SQL_COL_R_S_A + "]," + "[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," + "[" + SQL_COL_SUPERINTENDANT_NAME + "]," + "[" + SQL_COL_SUPERINTENDANT_PHONE + "]," + "[" + SQL_COL_REAL_ESTATE_TAXES + "]," + "[" + SQL_COL_MORTGAGE_INTEREST + "]," + "[" + SQL_COL_TOTAL_SHARES + "]," + "[" + SQL_COL_MASTER_ACCOUNT + "]," + "[" + SQL_COL_AUTH_REP + "]," + "[" + SQL_COL_FAX_NUMBER + "]," + "[" + SQL_COL_AUTH_CODE + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_OWNER_NUMBER + "] IN (?);";
		protected string m_BuildingNumber;
		protected Int16 m_OwnerNumber;
		protected string m_Address1;
		protected string m_Address2;
		protected string m_AgentName;
		protected string m_BlockNumber;
		protected string m_LotNumber;
		protected string m_SectionNumber;
		protected string m_MultipleDwellingRegistrationNumber;
		protected string m_RSA;
		protected string m_SeniorCitizenNumber;
		protected string m_SuperintendantName;
		protected string m_SuperintendantPhone;
		protected double m_RealEstateTaxes;
		protected double m_MortgageInterest;
		protected Int32 m_TotalShares;
		protected string m_MasterAccount;
		protected string m_AuthRep;
		protected string m_FaxNumber;
		protected string m_AuthCode;

		protected bool m_isChanged;
		public Building() : base(TABLE, SQL_COL_BUILDING_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
		}

		public Building(string BuildingNumber, Int16 OwnerNumber, string Address1, string Address2, string AgentName, string BlockNumber, string LotNumber, string SectionNumber, string MultipleDwellingRegistrationNumber, string RSA,
		string SeniorCitizenNumber, string SuperintendantName, string SuperintendantPhone, double RealEstateTaxes, double MortgageInterest, Int32 TotalShares, string MasterAccount, string AuthRep, string FaxNumber, string AuthCode) : base(TABLE, SQL_COL_BUILDING_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
			m_BuildingNumber = BuildingNumber;
			m_OwnerNumber = OwnerNumber;
			m_Address1 = Address1;
			m_Address2 = Address2;
			m_AgentName = AgentName;
			m_BlockNumber = BlockNumber;
			m_LotNumber = LotNumber;
			m_SectionNumber = SectionNumber;
			m_MultipleDwellingRegistrationNumber = MultipleDwellingRegistrationNumber;
			m_RSA = RSA;
			m_SeniorCitizenNumber = SeniorCitizenNumber;
			m_SuperintendantName = SuperintendantName;
			m_SuperintendantPhone = SuperintendantPhone;
			m_RealEstateTaxes = RealEstateTaxes;
			m_MortgageInterest = MortgageInterest;
			m_TotalShares = TotalShares;
			m_MasterAccount = MasterAccount;
			m_AuthRep = AuthRep;
			m_FaxNumber = FaxNumber;
			m_AuthCode = AuthCode;
		}

		protected Building fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (Information.isDBnull(dr(SQL_COL_BUILDING_NUMBER)) ? "" : dr(SQL_COL_BUILDING_NUMBER));
				m_OwnerNumber = dr(SQL_COL_OWNER_NUMBER);
				m_Address1 = (Information.isDBnull(dr(SQL_COL_ADDRESS_1)) ? "" : dr(SQL_COL_ADDRESS_1));
				m_Address2 = (Information.isDBnull(dr(SQL_COL_ADDRESS_2)) ? "" : dr(SQL_COL_ADDRESS_2));
				m_AgentName = (Information.isDBnull(dr(SQL_COL_AGENT_NAME)) ? "" : dr(SQL_COL_AGENT_NAME));
				m_BlockNumber = (Information.isDBnull(dr(SQL_COL_BLOCK_NUMBER)) ? "" : dr(SQL_COL_BLOCK_NUMBER));
				m_LotNumber = (Information.isDBnull(dr(SQL_COL_LOT_NUMBER)) ? "" : dr(SQL_COL_LOT_NUMBER));
				m_SectionNumber = (Information.isDBnull(dr(SQL_COL_SECTION_NUMBER)) ? "" : dr(SQL_COL_SECTION_NUMBER));
				m_MultipleDwellingRegistrationNumber = (Information.isDBnull(dr(SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER)) ? "" : dr(SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER));
				m_RSA = (Information.isDBnull(dr(SQL_COL_R_S_A)) ? "" : dr(SQL_COL_R_S_A));
				m_SeniorCitizenNumber = (Information.isDBnull(dr(SQL_COL_SENIOR_CITIZEN_NUMBER)) ? "" : dr(SQL_COL_SENIOR_CITIZEN_NUMBER));
				m_SuperintendantName = (Information.isDBnull(dr(SQL_COL_SUPERINTENDANT_NAME)) ? "" : dr(SQL_COL_SUPERINTENDANT_NAME));
				m_SuperintendantPhone = (Information.isDBnull(dr(SQL_COL_SUPERINTENDANT_PHONE)) ? "" : dr(SQL_COL_SUPERINTENDANT_PHONE));
				m_RealEstateTaxes = dr(SQL_COL_REAL_ESTATE_TAXES);
				m_MortgageInterest = dr(SQL_COL_MORTGAGE_INTEREST);
				m_TotalShares = dr(SQL_COL_TOTAL_SHARES);
				m_MasterAccount = (Information.isDBnull(dr(SQL_COL_MASTER_ACCOUNT)) ? "" : dr(SQL_COL_MASTER_ACCOUNT));
				m_AuthRep = (Information.isDBnull(dr(SQL_COL_AUTH_REP)) ? "" : dr(SQL_COL_AUTH_REP));
				m_FaxNumber = (Information.isDBnull(dr(SQL_COL_FAX_NUMBER)) ? "" : dr(SQL_COL_FAX_NUMBER));
				m_AuthCode = (Information.isDBnull(dr(SQL_COL_AUTH_CODE)) ? "" : dr(SQL_COL_AUTH_CODE));
			} catch (OleDbException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Building load(string m_BuildingNumber)
		{

			Building obj = new Building();
			string[] opt = { m_BuildingNumber };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables(0).Rows(0);

			return obj.fill(dr);

		}

		public static Building[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Building obj = new Building();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = default(DataRow);
			try {
				foreach ( dr in ds.Tables(0).Rows) {
					obj = new Building();
					obj.fill(dr);
					list.Add(obj);
					obj = null;
				}
			} catch (OleDbException ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
			}
			try {
				return (Building[])list.ToArray(typeof(Building));
			} catch (OleDbException ex) {
				return null;
			}

		}

		public static Building[] loadByOwner(int ownerId)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Building obj = new Building();
			string[] opt = null;

			opt = { Convert.ToString(ownerId) };
			ds = obj.load(SQL_LOAD_BY_OWNER, opt);
			obj = null;
			DataRow dr = default(DataRow);
			try {
				foreach ( dr in ds.Tables(0).Rows) {
					obj = new Building();
					obj.fill(dr);
					list.Add(obj);
					obj = null;
				}
			} catch (OleDbException ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				return null;
			}
			try {
				return (Building[])list.ToArray(typeof(Building));
			} catch (OleDbException ex) {
				return null;
			}

		}

		public static Building[] loadByOwner(string ownerIds)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Building obj = new Building();
			string[] opt = new string[1];
			opt(0) = ownerIds;
			ds = obj.load(SQL_LOAD_BY_OWNER, opt);
			obj = null;
			DataRow dr = default(DataRow);
			try {
				foreach ( dr in ds.Tables(0).Rows) {
					obj = new Building();
					obj.fill(dr);
					list.Add(obj);
					obj = null;
				}
			} catch (OleDbException ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				return null;
			}
			try {
				return (Building[])list.ToArray(typeof(Building));
			} catch (OleDbException ex) {
				return null;
			}

		}

		public bool save()
		{

			if (m_BuildingNumber > 0) {
				string[] values = {
					m_BuildingNumber,
					m_OwnerNumber,
					m_Address1,
					m_Address2,
					m_AgentName,
					m_BlockNumber,
					m_LotNumber,
					m_SectionNumber,
					m_MultipleDwellingRegistrationNumber,
					m_RSA,
					m_SeniorCitizenNumber,
					m_SuperintendantName,
					m_SuperintendantPhone,
					m_RealEstateTaxes,
					m_MortgageInterest,
					m_TotalShares,
					m_MasterAccount,
					m_AuthRep,
					m_FaxNumber,
					m_AuthCode,
					m_BuildingNumber
				};
				if (this.updateRecord(m_BuildingNumber, values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_BuildingNumber,
					m_OwnerNumber,
					m_Address1,
					m_Address2,
					m_AgentName,
					m_BlockNumber,
					m_LotNumber,
					m_SectionNumber,
					m_MultipleDwellingRegistrationNumber,
					m_RSA,
					m_SeniorCitizenNumber,
					m_SuperintendantName,
					m_SuperintendantPhone,
					m_RealEstateTaxes,
					m_MortgageInterest,
					m_TotalShares,
					m_MasterAccount,
					m_AuthRep,
					m_FaxNumber,
					m_AuthCode
				};
				m_BuildingNumber = this.insertRecord(values);
				if (m_BuildingNumber >= 0) {
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
				cmd.CommandText = "CREATE TABLE Building (Building Number TEXT(3),Owner Number INTEGER,Address 1 TEXT(30),Address 2 TEXT(30),Agent Name TEXT(12),Block Number TEXT(4),Lot Number TEXT(3),Section Number TEXT(3),Multiple Dwelling Registration Number TEXT(6),RSA TEXT(6),Senior Citizen Number TEXT(6),Superintendant Name TEXT(18),Superintendant Phone TEXT(12),Real Estate Taxes DOUBLE,Mortgage Interest DOUBLE,Total Shares LONG,MasterAccount TEXT(10),AuthRep TEXT(32),FaxNumber TEXT(12),AuthCode TEXT(10))";
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
		public string BuildingNumber {
			get { return m_BuildingNumber; }
			set {
				m_BuildingNumber = value;
				OnPropertyChanged("BuildingNumber");
			}
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

		public string AgentName {
			get { return m_AgentName; }
			set {
				m_AgentName = value;
				OnPropertyChanged("AgentName");
			}
		}

		public string BlockNumber {
			get { return m_BlockNumber; }
			set {
				m_BlockNumber = value;
				OnPropertyChanged("BlockNumber");
			}
		}

		public string LotNumber {
			get { return m_LotNumber; }
			set {
				m_LotNumber = value;
				OnPropertyChanged("LotNumber");
			}
		}

		public string SectionNumber {
			get { return m_SectionNumber; }
			set {
				m_SectionNumber = value;
				OnPropertyChanged("SectionNumber");
			}
		}

		public string MultipleDwellingRegistrationNumber {
			get { return m_MultipleDwellingRegistrationNumber; }
			set {
				m_MultipleDwellingRegistrationNumber = value;
				OnPropertyChanged("MultipleDwellingRegistrationNumber");
			}
		}

		public string RSA {
			get { return m_RSA; }
			set {
				m_RSA = value;
				OnPropertyChanged("RSA");
			}
		}

		public string SeniorCitizenNumber {
			get { return m_SeniorCitizenNumber; }
			set {
				m_SeniorCitizenNumber = value;
				OnPropertyChanged("SeniorCitizenNumber");
			}
		}

		public string SuperintendantName {
			get { return m_SuperintendantName; }
			set {
				m_SuperintendantName = value;
				OnPropertyChanged("SuperintendantName");
			}
		}

		public string SuperintendantPhone {
			get { return m_SuperintendantPhone; }
			set {
				m_SuperintendantPhone = value;
				OnPropertyChanged("SuperintendantPhone");
			}
		}

		public double RealEstateTaxes {
			get { return m_RealEstateTaxes; }
			set {
				m_RealEstateTaxes = value;
				OnPropertyChanged("RealEstateTaxes");
			}
		}

		public double MortgageInterest {
			get { return m_MortgageInterest; }
			set {
				m_MortgageInterest = value;
				OnPropertyChanged("MortgageInterest");
			}
		}

		public Int32 TotalShares {
			get { return m_TotalShares; }
			set {
				m_TotalShares = value;
				OnPropertyChanged("TotalShares");
			}
		}

		public string MasterAccount {
			get { return m_MasterAccount; }
			set {
				m_MasterAccount = value;
				OnPropertyChanged("MasterAccount");
			}
		}

		public string AuthRep {
			get { return m_AuthRep; }
			set {
				m_AuthRep = value;
				OnPropertyChanged("AuthRep");
			}
		}

		public string FaxNumber {
			get { return m_FaxNumber; }
			set {
				m_FaxNumber = value;
				OnPropertyChanged("FaxNumber");
			}
		}

		public string AuthCode {
			get { return m_AuthCode; }
			set {
				m_AuthCode = value;
				OnPropertyChanged("AuthCode");
			}
		}

	}
}
