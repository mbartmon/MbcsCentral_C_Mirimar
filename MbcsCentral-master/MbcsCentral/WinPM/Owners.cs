using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
namespace MbcsCentral
{
	public class Owners : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		public static string TABLE = "pm_Owner";
		public const string SQL_COL__OWNER__NUMBER = "Owner Number";
		public const string SQL_COL__I_D = "ID";
		public const string SQL_COL_CLIENT_ID = "clientId";
		public const string SQL_COL__ADDRESS_1 = "Address 1";
		public const string SQL_COL__ADDRESS_2 = "Address 2";
		public const string SQL_COL__TELEPHONE = "Telephone";
		public const string SQL_COL__FEDERAL_I_D = "FederalID";
		public const string SQL_COL__OWNER__NAME = "Owner Name";
		public const string SQL_COL__BILLING__NAME = "Billing Name";
        public const string SQL_COLUMN_INACTIVE = "InActive";
		public const string SQL_COL_BANK_ROUTING_NUMBER = "BankRoutingNumber";
		public const string SQL_COL_BANK_ACCOUNT_NUMBER = "BankAccountNumber";

		public const string SQL_COL_EMAIL = "email";

		static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL__OWNER__NUMBER + "]," + 
                                "[" + SQL_COL__I_D + "]," + 
                                "[" + SQL_COL_CLIENT_ID + "]," + 
                                "[" + SQL_COL__ADDRESS_1 + "]," + 
                                "[" + SQL_COL__ADDRESS_2 + "]," + 
                                "[" + SQL_COL__TELEPHONE + "]," + 
                                "[" + SQL_COL__FEDERAL_I_D + "]," + 
                                "[" + SQL_COL__OWNER__NAME + "]," +
                                "[" + SQL_COL__BILLING__NAME + "]," + 
                                "[" + SQL_COL_EMAIL + "]," + 
                                "[" + SQL_COLUMN_INACTIVE + "]," +
								"[" + SQL_COL_BANK_ROUTING_NUMBER + "]," +
								"[" + SQL_COL_BANK_ACCOUNT_NUMBER + "]" +
                                " FROM " + TABLE + " WHERE " + SQL_COL__I_D + " = ?;";

		static string SQL_LOAD_ALL = " SELECT " + 
                                     "[" + SQL_COL__OWNER__NUMBER + "]," + 
                                     "[" + SQL_COL__I_D + "]," + 
                                     "[" + SQL_COL_CLIENT_ID + "]," + 
                                     "[" + SQL_COL__ADDRESS_1 + "]," + 
                                     "[" + SQL_COL__ADDRESS_2 + "]," + 
                                     "[" + SQL_COL__TELEPHONE + "]," + 
                                     "[" + SQL_COL__FEDERAL_I_D + "]," + 
                                     "[" + SQL_COL__OWNER__NAME + "]," + 
                                     "[" + SQL_COL__BILLING__NAME + "]," + 
                                     "[" + SQL_COL_EMAIL + "]," + 
                                     "[" + SQL_COLUMN_INACTIVE + "]," +
									 "[" + SQL_COL_BANK_ROUTING_NUMBER + "]," +
									 "[" + SQL_COL_BANK_ACCOUNT_NUMBER + "]" +
                                     " FROM " + TABLE + 
                                     " WHERE " + SQL_COL_CLIENT_ID + "=?" +
                                     " ORDER BY [" + SQL_COL__OWNER__NAME + "]";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                "[" + SQL_COL__OWNER__NUMBER + "]," + 
                                "[" + SQL_COL_CLIENT_ID + "]," + 
                                "[" + SQL_COL__ADDRESS_1 + "]," + 
                                "[" + SQL_COL__ADDRESS_2 + "]," + 
                                "[" + SQL_COL__TELEPHONE + "]," + 
                                "[" + SQL_COL__FEDERAL_I_D + "]," + 
                                "[" + SQL_COL__OWNER__NAME + "]," + 
                                "[" + SQL_COL__BILLING__NAME + "]," + 
                                "[" + SQL_COL_EMAIL + "]," +
                                "[" + SQL_COLUMN_INACTIVE + "]," +
								"[" + SQL_COL_BANK_ROUTING_NUMBER + "]," +
								"[" + SQL_COL_BANK_ACCOUNT_NUMBER + "]" +
                                ") VALUES (?, ?, '?', '?', '?', '?', '?', '?', '?', ?, '?', '?');";

		static string SQL_UPDATE = " UPDATE " + TABLE +
									" SET " +
									"[" + SQL_COL__OWNER__NUMBER + "]=?," +
									"[" + SQL_COL_CLIENT_ID + "]=?," +
									"[" + SQL_COL__ADDRESS_1 + "]='?'," +
									"[" + SQL_COL__ADDRESS_2 + "]='?'," +
									"[" + SQL_COL__TELEPHONE + "]='?'," +
									"[" + SQL_COL__FEDERAL_I_D + "]='?'," +
									"[" + SQL_COL__OWNER__NAME + "]='?'," +
									"[" + SQL_COL__BILLING__NAME + "]='?'," +
									"[" + SQL_COL_EMAIL + "]='?'," +
									"[" + SQL_COLUMN_INACTIVE + "]=?," +
									"[" + SQL_COL_BANK_ROUTING_NUMBER + "]='?'," +
									"[" + SQL_COL_BANK_ACCOUNT_NUMBER + "]='?'" +
                                    " WHERE " + SQL_COL__I_D + " = ?;";

        static string SQL_LOAD_BY_OWNER_NUMBER = " SELECT " +
                                                 "[" + SQL_COL__OWNER__NUMBER + "]," +
                                                 "[" + SQL_COL__I_D + "]," +
                                                 "[" + SQL_COL_CLIENT_ID + "]," +
                                                 "[" + SQL_COL__ADDRESS_1 + "]," +
                                                 "[" + SQL_COL__ADDRESS_2 + "]," +
                                                 "[" + SQL_COL__TELEPHONE + "]," +
                                                 "[" + SQL_COL__FEDERAL_I_D + "]," +
                                                 "[" + SQL_COL__OWNER__NAME + "]," +
                                                 "[" + SQL_COL__BILLING__NAME + "]," +
                                                 "[" + SQL_COL_EMAIL + "]," +
                                                 "[" + SQL_COLUMN_INACTIVE + "]," +
												 "[" + SQL_COL_BANK_ROUTING_NUMBER + "]," +
												 "[" + SQL_COL_BANK_ACCOUNT_NUMBER + "]" +
                                                 " FROM " + TABLE +
                                                 " WHERE [" + SQL_COL__OWNER__NUMBER + "] = ?" +
                                                 " AND " + SQL_COL_CLIENT_ID + "=?";
		protected short m_OwnerNumber;
		protected int m_ID;
		protected long m_clientId;
		protected string m_Address1;
		protected string m_Address2;
		protected string m_Telephone;
		protected string m_FederalID;
		protected string m_OwnerName;
		protected string m_BillingName;
		protected string m_email;
        protected bool m_inActive = false;
		protected string m_BankRoutingNumber;
		protected string m_BankAccountNumber;

		protected bool m_changed = false;
		public Owners() : base(TABLE, SQL_COL__I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Owners(short OwnerNumber, int ID, long clientId, string Address1, string Address2, string Telephone, string FederalID, string OwnerName, string BillingName, string email) : base(TABLE, SQL_COL__I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_OwnerNumber = OwnerNumber;
			m_ID = ID;
			m_clientId = clientId;
			m_Address1 = Address1;
			m_Address2 = Address2;
			m_Telephone = Telephone;
			m_FederalID = FederalID;
			m_OwnerName = OwnerName;
			m_BillingName = BillingName;
			m_email = email;
		}

		protected Owners fill(DataRow dr)
		{

			try {
				m_OwnerNumber = (short)dr[SQL_COL__OWNER__NUMBER];
				m_ID = (int)dr[SQL_COL__I_D];
				m_clientId = (int)dr[SQL_COL_CLIENT_ID];
				m_Address1 = (string)(Information.IsDBNull(dr[SQL_COL__ADDRESS_1]) ? "" : dr[SQL_COL__ADDRESS_1]);
				m_Address2 = (string)(Information.IsDBNull(dr[SQL_COL__ADDRESS_2]) ? "" : dr[SQL_COL__ADDRESS_2]);
				m_Telephone = (string)(Information.IsDBNull(dr[SQL_COL__TELEPHONE]) ? "" : dr[SQL_COL__TELEPHONE]);
				m_FederalID = (string)(Information.IsDBNull(dr[SQL_COL__FEDERAL_I_D]) ? "" : dr[SQL_COL__FEDERAL_I_D]);
				m_OwnerName = (string)(Information.IsDBNull(dr[SQL_COL__OWNER__NAME]) ? "" : dr[SQL_COL__OWNER__NAME]);
				m_BillingName = (string)(Information.IsDBNull(dr[SQL_COL__BILLING__NAME]) ? "" : dr[SQL_COL__BILLING__NAME]);
				m_email = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL]) ? "" : dr[SQL_COL_EMAIL]);
                m_inActive = (bool)dr[SQL_COLUMN_INACTIVE];
				m_BankRoutingNumber = (string)dr[SQL_COL_BANK_ROUTING_NUMBER];
				m_BankAccountNumber = (string)dr[SQL_COL_BANK_ACCOUNT_NUMBER];
			} catch (SqlException ex) {
			}
			return this;

		}

        public static Owners loadOne(int m_ID)
        {

            Owners obj = new Owners();
            string[] opt = { m_ID.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

		public static Owners[] load(long clientId = 1)
		{

            string[] opt = { clientId.ToString() };
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Owners obj = new Owners();

			ds = obj.load(SQL_LOAD_ALL,opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Owners();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Owners[])list.ToArray(typeof(Owners));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Owners loadByOwnerNumber(long ownerNumber, long clientId)
		{

			Owners obj = new Owners();
            string[] opt = { ownerNumber.ToString(), clientId.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_OWNER_NUMBER, opt);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				DataRow dr = ds.Tables[0].Rows[0];

				if ((dr != null)) {
					return obj.fill(dr);
				} else {
					return null;
				}
			} else {
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
					m_OwnerNumber.ToString(),
					m_clientId.ToString(),
					m_Address1,
					m_Address2,
					m_Telephone,
					m_FederalID,
					m_OwnerName,
					m_BillingName,
					m_email,
					m_inActive.ToString(),
					m_BankRoutingNumber,
					m_BankAccountNumber,
					m_ID.ToString()
				};
                if (this.updateRecord(m_ID.ToString(), values))
                {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_OwnerNumber.ToString(),
					m_clientId.ToString(),
					m_Address1,
					m_Address2,
					m_Telephone,
					m_FederalID,
					m_OwnerName,
					m_BillingName,
					m_email,
                    m_inActive.ToString(),
					m_BankRoutingNumber,
					m_BankAccountNumber
				};
				m_ID = this.insertRecord(values);
				if (m_ID >= 0) {
					return true;
				}
			}
			return false;

		}

		private void OnPropertyChanged(string info)
		{
			m_changed = true;
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        // Property Routines
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            get { return m_changed; }
			set { m_changed = value; }
		}
		public short OwnerNumber {
			get { return m_OwnerNumber; }
			set {
				m_OwnerNumber = value;
				OnPropertyChanged("OwnerNumber");
			}
		}

		public int ID {
			get { return m_ID; }
		}

		public long clientId {
			get { return m_clientId; }
			set {
				m_clientId = value;
				OnPropertyChanged("clientId");
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

		public string email {
			get { return m_email; }
			set {
				m_email = value;
				OnPropertyChanged("email");
			}
		}

        public bool InActive
        {
            get { return m_inActive; }
            set
            {
                m_inActive = value;
                OnPropertyChanged("Inactive");
            }
        }

		public string BankRoutingNumber
		{
			get { return m_BankRoutingNumber; }
			set
			{
				m_BankRoutingNumber = value;
				OnPropertyChanged("BankRoutingNumber");
			}
		}

		public string BankAccountNumber
		{
			get { return m_BankAccountNumber; }
			set
			{
				m_BankAccountNumber = value;
				OnPropertyChanged("BankAccountNumber");
			}
		}
	}
}
