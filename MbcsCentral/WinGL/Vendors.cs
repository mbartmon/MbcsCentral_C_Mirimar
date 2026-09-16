using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL 
{
	public class Vendors : clsDB	, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public static string TABLE = "gl_Vendors";

		public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
		public const string SQL_COL_CURRENT_PURCHASES = "CurrentPurchases";
		public const string SQL_COL_PRIOR_YEAR_PURCHASES = "PriorYearPurchases";
		public const string SQL_COL_PAYROLL = "Payroll";
		public const string SQL_COL_CORPORATION = "Corporation";
		public const string SQL_COL_INACTIVE = "Inactive";
		public const string SQL_COL_VENDOR_NAME = "VendorName";
		public const string SQL_COL_NAME2 = "Name2";
		public const string SQL_COL_STREET = "Street";
		public const string SQL_COL_CITY = "City";
		public const string SQL_COL_STATE = "State";
		public const string SQL_COL_ZIP = "Zip";
		public const string SQL_COL_OLD_NUMBER = "OldNumber";
		public const string SQL_COL_COMMENT = "Comment";
		public const string SQL_COL_TELEPHONE = "Telephone";
		public const string SQL_COL_FAX = "Fax";
		public const string SQL_COL_EMAIL = "Email";
		public const string SQL_COL_FEDERAL_I_D = "FederalID";
		public const string SQL_COL_STATE_I_D = "StateID";
		public const string SQL_COL_JOURNAL = "Journal";
		public const string SQL_COL_ACH = "ACH";
		public const string SQL_COL_ROUTING_NUMBER = "RoutingNumber";
		public const string SQL_COL_BANK_ACCOUNT = "BankAccount";
		public const string SQL_COL_ACH_EMAIL = "ACHEmail";
		public const string SQL_COL_SEND_ACH_EMAIL = "SendACHEmail";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_VENDOR_NUMBER + "]," +
				"[" + SQL_COL_CURRENT_PURCHASES + "]," +
				"[" + SQL_COL_PRIOR_YEAR_PURCHASES + "]," +
				"[" + SQL_COL_PAYROLL + "]," +
				"[" + SQL_COL_CORPORATION + "]," +
				"[" + SQL_COL_INACTIVE + "]," +
				"[" + SQL_COL_VENDOR_NAME + "]," +
				"[" + SQL_COL_NAME2 + "]," +
				"[" + SQL_COL_STREET + "]," +
				"[" + SQL_COL_CITY + "]," +
				"[" + SQL_COL_STATE + "]," +
				"[" + SQL_COL_ZIP + "]," +
				"[" + SQL_COL_OLD_NUMBER + "]," +
				"[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_TELEPHONE + "]," +
				"[" + SQL_COL_FAX + "]," +
				"[" + SQL_COL_EMAIL + "]," +
				"[" + SQL_COL_FEDERAL_I_D + "]," +
				"[" + SQL_COL_STATE_I_D + "]," +
				"[" + SQL_COL_JOURNAL + "]," +
				"[" + SQL_COL_ACH + "]," +
				"[" + SQL_COL_ROUTING_NUMBER + "]," +
				"[" + SQL_COL_BANK_ACCOUNT + "]," +
				"[" + SQL_COL_ACH_EMAIL + "]," +
				"[" + SQL_COL_SEND_ACH_EMAIL + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_VENDOR_NUMBER + "]," +
				"[" + SQL_COL_CURRENT_PURCHASES + "]," +
				"[" + SQL_COL_PRIOR_YEAR_PURCHASES + "]," +
				"[" + SQL_COL_PAYROLL + "]," +
				"[" + SQL_COL_CORPORATION + "]," +
				"[" + SQL_COL_INACTIVE + "]," +
				"[" + SQL_COL_VENDOR_NAME + "]," +
				"[" + SQL_COL_NAME2 + "]," +
				"[" + SQL_COL_STREET + "]," +
				"[" + SQL_COL_CITY + "]," +
				"[" + SQL_COL_STATE + "]," +
				"[" + SQL_COL_ZIP + "]," +
				"[" + SQL_COL_OLD_NUMBER + "]," +
				"[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_TELEPHONE + "]," +
				"[" + SQL_COL_FAX + "]," +
				"[" + SQL_COL_EMAIL + "]," +
				"[" + SQL_COL_FEDERAL_I_D + "]," +
				"[" + SQL_COL_STATE_I_D + "]," +
				"[" + SQL_COL_JOURNAL + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ROUTING_NUMBER + "]," +
                "[" + SQL_COL_BANK_ACCOUNT + "]," +
                "[" + SQL_COL_ACH_EMAIL + "]," +
                "[" + SQL_COL_SEND_ACH_EMAIL + "]" +
                " FROM " + TABLE +
                " ORDER BY [" + SQL_COL_VENDOR_NAME + "]";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_CURRENT_PURCHASES + "]," +
				"[" + SQL_COL_PRIOR_YEAR_PURCHASES + "]," +
				"[" + SQL_COL_PAYROLL + "]," +
				"[" + SQL_COL_CORPORATION + "]," +
				"[" + SQL_COL_INACTIVE + "]," +
				"[" + SQL_COL_VENDOR_NAME + "]," +
				"[" + SQL_COL_NAME2 + "]," +
				"[" + SQL_COL_STREET + "]," +
				"[" + SQL_COL_CITY + "]," +
				"[" + SQL_COL_STATE + "]," +
				"[" + SQL_COL_ZIP + "]," +
				"[" + SQL_COL_OLD_NUMBER + "]," +
				"[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_TELEPHONE + "]," +
				"[" + SQL_COL_FAX + "]," +
				"[" + SQL_COL_EMAIL + "]," +
				"[" + SQL_COL_FEDERAL_I_D + "]," +
				"[" + SQL_COL_STATE_I_D + "]," +
				"[" + SQL_COL_JOURNAL + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ROUTING_NUMBER + "]," +
                "[" + SQL_COL_BANK_ACCOUNT + "]," +
                "[" + SQL_COL_ACH_EMAIL + "]," +
                "[" + SQL_COL_SEND_ACH_EMAIL + "]" +
                ") VALUES (?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?', '?', '?', ?);";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_CURRENT_PURCHASES + "]=?," +
				"[" + SQL_COL_PRIOR_YEAR_PURCHASES + "]=?," +
				"[" + SQL_COL_PAYROLL + "]=?," +
				"[" + SQL_COL_CORPORATION + "]=?," +
				"[" + SQL_COL_INACTIVE + "]=?," +
				"[" + SQL_COL_VENDOR_NAME + "]='?'," +
				"[" + SQL_COL_NAME2 + "]='?'," +
				"[" + SQL_COL_STREET + "]='?'," +
				"[" + SQL_COL_CITY + "]='?'," +
				"[" + SQL_COL_STATE + "]='?'," +
				"[" + SQL_COL_ZIP + "]='?'," +
				"[" + SQL_COL_OLD_NUMBER + "]='?'," +
				"[" + SQL_COL_COMMENT + "]='?'," +
				"[" + SQL_COL_TELEPHONE + "]='?'," +
				"[" + SQL_COL_FAX + "]='?'," +
				"[" + SQL_COL_EMAIL + "]='?'," +
				"[" + SQL_COL_FEDERAL_I_D + "]='?'," +
				"[" + SQL_COL_STATE_I_D + "]='?'," +
				"[" + SQL_COL_JOURNAL + "]='?'," +
                "[" + SQL_COL_ACH + "]=?," +
                "[" + SQL_COL_ROUTING_NUMBER + "]='?'," +
                "[" + SQL_COL_BANK_ACCOUNT + "]='?'," +
                "[" + SQL_COL_ACH_EMAIL + "]='?'," +
                "[" + SQL_COL_SEND_ACH_EMAIL + "]=?" +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?;";

        static string SQL_LOAD_BY_NAME = "SELECT " +
			"[" + SQL_COL_VENDOR_NUMBER + "]," +
			"[" + SQL_COL_CURRENT_PURCHASES + "]," +
			"[" + SQL_COL_PRIOR_YEAR_PURCHASES + "]," +
			"[" + SQL_COL_PAYROLL + "]," +
			"[" + SQL_COL_CORPORATION + "]," +
			"[" + SQL_COL_INACTIVE + "]," +
			"[" + SQL_COL_VENDOR_NAME + "]," +
			"[" + SQL_COL_NAME2 + "]," +
			"[" + SQL_COL_STREET + "]," +
			"[" + SQL_COL_CITY + "]," +
			"[" + SQL_COL_STATE + "]," +
			"[" + SQL_COL_ZIP + "]," +
			"[" + SQL_COL_OLD_NUMBER + "]," +
			"[" + SQL_COL_COMMENT + "]," +
			"[" + SQL_COL_TELEPHONE + "]," +
			"[" + SQL_COL_FAX + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_FEDERAL_I_D + "]," +
			"[" + SQL_COL_STATE_I_D + "]," +
			"[" + SQL_COL_JOURNAL + "]," +
            "[" + SQL_COL_ACH + "]," +
            "[" + SQL_COL_ROUTING_NUMBER + "]," +
            "[" + SQL_COL_BANK_ACCOUNT + "]," +
            "[" + SQL_COL_ACH_EMAIL + "]," +
            "[" + SQL_COL_SEND_ACH_EMAIL + "]" +
            " FROM " + TABLE +
            " WHERE " + "LOWER ([" + SQL_COL_VENDOR_NAME + "]) LIKE ('?%')";



		protected int m_VendorNumber;
		protected decimal m_CurrentPurchases;
		protected decimal m_PriorYearPurchases;
		protected bool m_Payroll;
		protected bool m_Corporation;
		protected bool m_Inactive;
		protected string m_VendorName;
		protected string m_Name2;
		protected string m_Street;
		protected string m_City;
		protected string m_State;
		protected string m_Zip;
		protected string m_OldNumber;
		protected string m_Comment;
		protected string m_Telephone;
		protected string m_Fax;
		protected string m_Email;
		protected string m_FederalID;
		protected string m_StateID;
		protected string m_Journal;
		protected bool m_isChanged;
		protected bool m_ACH;
		protected string m_ACHEmail;
		protected bool m_SendACHEmail;
		protected string m_RoutingNumber;
		protected string m_BankAccount;


		public Vendors() : base(TABLE, SQL_COL_VENDOR_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public Vendors(int VendorNumber,decimal CurrentPurchases,decimal PriorYearPurchases,bool Payroll,bool Corporation,bool Inactive,string VendorName,string Name2,string Street,string City,string State,string Zip,string OldNumber,string Comment,string Telephone,string Fax,string Email,string FederalID,string StateID,string Journal) : 
				base(TABLE, SQL_COL_VENDOR_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_VendorNumber = VendorNumber;
			m_CurrentPurchases = CurrentPurchases;
			m_PriorYearPurchases = PriorYearPurchases;
			m_Payroll = Payroll;
			m_Corporation = Corporation;
			m_Inactive = Inactive;
			m_VendorName = VendorName;
			m_Name2 = Name2;
			m_Street = Street;
			m_City = City;
			m_State = State;
			m_Zip = Zip;
			m_OldNumber = OldNumber;
			m_Comment = Comment;
			m_Telephone = Telephone;
			m_Fax = Fax;
			m_Email = Email;
			m_FederalID = FederalID;
			m_StateID = StateID;
			m_Journal = Journal;
		}

		protected Vendors fill(DataRow dr)
		{
			try {
				m_VendorNumber = (int) dr[SQL_COL_VENDOR_NUMBER];
				m_CurrentPurchases = (decimal) dr[SQL_COL_CURRENT_PURCHASES];
				m_PriorYearPurchases = (decimal) dr[SQL_COL_PRIOR_YEAR_PURCHASES];
				m_Payroll = (bool) dr[SQL_COL_PAYROLL];
				m_Corporation = (bool) dr[SQL_COL_CORPORATION];
				m_Inactive = (bool) dr[SQL_COL_INACTIVE];
				m_VendorName = (string) (Convert.IsDBNull(dr[SQL_COL_VENDOR_NAME]) ? "" : dr[SQL_COL_VENDOR_NAME]);
				m_Name2 = (string) (Convert.IsDBNull(dr[SQL_COL_NAME2]) ? "" : dr[SQL_COL_NAME2]);
				m_Street = (string) (Convert.IsDBNull(dr[SQL_COL_STREET]) ? "" : dr[SQL_COL_STREET]);
				m_City = (string) (Convert.IsDBNull(dr[SQL_COL_CITY]) ? "" : dr[SQL_COL_CITY]);
				m_State = (string) (Convert.IsDBNull(dr[SQL_COL_STATE]) ? "" : dr[SQL_COL_STATE]);
				m_Zip = (string) (Convert.IsDBNull(dr[SQL_COL_ZIP]) ? "" : dr[SQL_COL_ZIP]);
				m_OldNumber = (string) (Convert.IsDBNull(dr[SQL_COL_OLD_NUMBER]) ? "" : dr[SQL_COL_OLD_NUMBER]);
				m_Comment = (string) (Convert.IsDBNull(dr[SQL_COL_COMMENT]) ? "" : dr[SQL_COL_COMMENT]);
				m_Telephone = (string) (Convert.IsDBNull(dr[SQL_COL_TELEPHONE]) ? "" : dr[SQL_COL_TELEPHONE]);
				m_Fax = (string) (Convert.IsDBNull(dr[SQL_COL_FAX]) ? "" : dr[SQL_COL_FAX]);
				m_Email = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL]) ? "" : dr[SQL_COL_EMAIL]);
				m_FederalID = (string) (Convert.IsDBNull(dr[SQL_COL_FEDERAL_I_D]) ? "" : dr[SQL_COL_FEDERAL_I_D]);
				m_StateID = (string) (Convert.IsDBNull(dr[SQL_COL_STATE_I_D]) ? "" : dr[SQL_COL_STATE_I_D]);
				m_Journal = (string) (Convert.IsDBNull(dr[SQL_COL_JOURNAL]) ? "" : dr[SQL_COL_JOURNAL]);
				m_ACH = (bool)dr[SQL_COL_ACH];
				m_ACHEmail = (string)(Convert.IsDBNull(dr[SQL_COL_ACH_EMAIL]) ? "" : dr[SQL_COL_ACH_EMAIL]);
				m_SendACHEmail = (bool)dr[SQL_COL_SEND_ACH_EMAIL];
				m_BankAccount = (string)(Convert.IsDBNull(dr[SQL_COL_BANK_ACCOUNT]) ? "" : dr[SQL_COL_BANK_ACCOUNT]);
				m_RoutingNumber = (string)(Convert.IsDBNull(dr[SQL_COL_ROUTING_NUMBER]) ? "" : dr[SQL_COL_ROUTING_NUMBER]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static Vendors load( int m_VendorNumber)
		{
			Vendors obj  = new Vendors();
			string[] opt = {m_VendorNumber.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static Vendors[] load(bool activeOnly = false)
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Vendors obj = new Vendors();

			ds = obj.loadAll();
			if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new Vendors();
				obj.fill(dr);
				if (!activeOnly || (activeOnly && !obj.Inactive))
					list.Add(obj);
				obj = null;
			}
			try {
				return (Vendors[])list.ToArray(typeof(Vendors));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Vendors[] loadByName(string name)
		{
			ArrayList list = new ArrayList();
			Vendors obj = new Vendors();
			string[] opt = { name.ToLower() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_NAME, opt);
			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					obj = new Vendors();
					obj.fill(dr);
					list.Add(obj);
					obj = null;

				}
			}
			else
				return null;
			try
			{
				return (Vendors[])list.ToArray(typeof(Vendors));
			}
			catch (SqlException ex)
			{
				return null;
			}

		}

			public static Vendors[] LoadQuery(string query, bool add = false)
			{
				ArrayList list = new ArrayList();

				DataSet ds = new DataSet();
				Vendors obj = new Vendors();
				string[] opt = null;

				ds = obj.load(query, opt);
				if (ds == null || ds.Tables[0].Rows.Count == 0)
					return null;
				obj = null;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					obj = new Vendors();
					obj.fill(dr);
					list.Add(obj);
					obj = null;
				}
				try
				{
					return (Vendors[])list.ToArray(typeof(Vendors));
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
			if (m_VendorNumber > 0 ) {
				string[] values = {m_CurrentPurchases.ToString(), m_PriorYearPurchases.ToString(), m_Payroll.ToString(), m_Corporation.ToString(), m_Inactive.ToString(), m_VendorName, m_Name2, m_Street, m_City, m_State, m_Zip, m_OldNumber, m_Comment, m_Telephone, m_Fax, m_Email, m_FederalID, m_StateID, m_Journal,
				 m_ACH.ToString(), m_RoutingNumber, m_BankAccount, m_ACHEmail, m_SendACHEmail.ToString(), m_VendorNumber.ToString()};
				if (this.updateRecord(m_VendorNumber.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_CurrentPurchases.ToString(), m_PriorYearPurchases.ToString(), m_Payroll.ToString(), m_Corporation.ToString(), m_Inactive.ToString(), m_VendorName, m_Name2, m_Street, m_City, m_State, m_Zip, m_OldNumber, m_Comment, m_Telephone, m_Fax, m_Email, m_FederalID, m_StateID, m_Journal,
				m_ACH.ToString(), m_RoutingNumber, m_BankAccount, m_ACHEmail, m_SendACHEmail.ToString()};
				m_VendorNumber = this.insertRecord(values);
				if (m_VendorNumber >= 0) {
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
		/// <summary>
		/// Has record been modified?
		/// </summary>
		public bool IsChanged

		{
			set { m_isChanged = value;}
			get { return m_isChanged; }
		}

		public int VendorNumber {
			get { return m_VendorNumber; }
		}

		public decimal CurrentPurchases {
			set{ m_CurrentPurchases = value;
			OnPropertyChanged("CurrentPurchases"); }
			get { return m_CurrentPurchases; }
		}

		public decimal PriorYearPurchases {
			set{ m_PriorYearPurchases = value;
			OnPropertyChanged("PriorYearPurchases"); }
			get { return m_PriorYearPurchases; }
		}

		public bool Payroll {
			set{ m_Payroll = value;
			OnPropertyChanged("Payroll"); }
			get { return m_Payroll; }
		}

		public bool Corporation {
			set{ m_Corporation = value;
			OnPropertyChanged("Corporation"); }
			get { return m_Corporation; }
		}

		public bool Inactive {
			set{ m_Inactive = value;
			OnPropertyChanged("Inactive"); }
			get { return m_Inactive; }
		}

		public string VendorName {
			set{ m_VendorName = value;
			OnPropertyChanged("VendorName"); }
			get { return m_VendorName; }
		}

		public string Name2 {
			set{ m_Name2 = value;
			OnPropertyChanged("Name2"); }
			get { return m_Name2; }
		}

		public string Street {
			set{ m_Street = value;
			OnPropertyChanged("Street"); }
			get { return m_Street; }
		}

		public string City {
			set{ m_City = value;
			OnPropertyChanged("City"); }
			get { return m_City; }
		}

		public string State {
			set{ m_State = value;
			OnPropertyChanged("State"); }
			get { return m_State; }
		}

		public string Zip {
			set{ m_Zip = value;
			OnPropertyChanged("Zip"); }
			get { return m_Zip; }
		}

		public string OldNumber {
			set{ m_OldNumber = value;
			OnPropertyChanged("OldNumber"); }
			get { return m_OldNumber; }
		}

		public string Comment {
			set{ m_Comment = value;
			OnPropertyChanged("Comment"); }
			get { return m_Comment; }
		}

		public string Telephone {
			set{ m_Telephone = value;
			OnPropertyChanged("Telephone"); }
			get { return m_Telephone; }
		}

		public string Fax {
			set{ m_Fax = value;
			OnPropertyChanged("Fax"); }
			get { return m_Fax; }
		}

		public string Email {
			set{ m_Email = value;
			OnPropertyChanged("Email"); }
			get { return m_Email; }
		}

		public string FederalID {
			set{ m_FederalID = value;
			OnPropertyChanged("FederalID"); }
			get { return m_FederalID; }
		}

		public string StateID {
			set{ m_StateID = value;
			OnPropertyChanged("StateID"); }
			get { return m_StateID; }
		}

		public string AdditionalInfo
		{
			set
			{
				m_StateID = value;
				OnPropertyChanged("StateID");
			}
			get { return m_StateID; }
		}

		public string Journal {
			set{ m_Journal = value;
			OnPropertyChanged("Journal"); }
			get { return m_Journal; }
		}

		public bool ACH
		{
			set { m_ACH = value;
				OnPropertyChanged("ACH");
			}
			get { return m_ACH; }	
		}

		public string RoutingNumber
		{
			set { m_RoutingNumber = value;
				OnPropertyChanged("RoutingNumber");
			}
			get { return m_RoutingNumber; }
		}

		public string BankAccount
		{
			set
			{
				m_BankAccount = value;
				OnPropertyChanged("BankAccount");
			}
			get { return m_BankAccount; }
		}

		public string ACHEmail
		{
			set
			{
				m_ACHEmail = value;
				OnPropertyChanged("ACHEmail");
			}
			get { return m_ACHEmail; }
		}

		public bool SendACHEmail
		{
			set { m_SendACHEmail = value;
				OnPropertyChanged("SendACHEmail");
			}
			get { return m_SendACHEmail; }	
		}

		public string NumberName
		{
			get { return  VendorNumber.ToString().PadLeft(5) + ": " + VendorName; }
		}

		public string FullName
		{
				get { return VendorName.Trim() + (StateID.Replace("\0", "").Trim() != "" ? " {" + StateID.Replace("\0","").Trim() + "}" : ""); }
		}
	}
}
