using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
	public class Lockbox : clsDB, INotifyPropertyChanged
	{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "Lockbox";

	public const string SQL_COL_ACCOUNT = "Account";
	public const string SQL_COL_CHARGE_NUMBER = "ChargeNumber";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_DUE = "Due";
	public const string SQL_COL_PAID = "Paid";
	public const string SQL_COL_AMOUNT = "Amount";
	public const string SQL_COL_CODE = "Code";
	public const string SQL_COL_DESCRIPTION = "Description";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_ACCOUNT + "]," +
				"[" + SQL_COL_CHARGE_NUMBER + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_DUE + "]," +
				"[" + SQL_COL_PAID + "]," +
				"[" + SQL_COL_AMOUNT + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_ACCOUNT + "]," +
				"[" + SQL_COL_CHARGE_NUMBER + "]," +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_DUE + "]," +
				"[" + SQL_COL_PAID + "]," +
				"[" + SQL_COL_AMOUNT + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_ACCOUNT + "]," +
				"[" + SQL_COL_CHARGE_NUMBER + "]," +
				"[" + SQL_COL_DUE + "]," +
				"[" + SQL_COL_PAID + "]," +
				"[" + SQL_COL_AMOUNT + "]," +
				"[" + SQL_COL_CODE + "]," +
				"[" + SQL_COL_DESCRIPTION + "]" +
				") VALUES (?, ?, ?, ?, ?, '?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_ACCOUNT + "]=?," +
				"[" + SQL_COL_CHARGE_NUMBER + "]=?," +
				"[" + SQL_COL_DUE + "]=?," +
				"[" + SQL_COL_PAID + "]=?," +
				"[" + SQL_COL_AMOUNT + "]=?," +
				"[" + SQL_COL_CODE + "]='?'," +
				"[" + SQL_COL_DESCRIPTION + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_CREATE_TABLE = "CREATE TABLE [dbo].[Lockbox](" +
                    "[Account][int] NULL, [Code] [varchar] (2) NULL, [Description] [varchar] (32) NULL,	[Due] [money] NULL,	[Paid] [money] NULL, [ChargeNumber] [int] NULL,	[Amount] [money] NULL, 	[ID] [int] IDENTITY(1,1) NOT NULL, " +
                    "CONSTRAINT[PK_Lockbox] PRIMARY KEY CLUSTERED " + 
                    "( [ID] ASC" +
                    ") WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                    ") ON[PRIMARY], " +
                    "CONSTRAINT[DF_Lockbox_Account]  DEFAULT((0)) FOR[Account], " +
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_Code]  DEFAULT('') FOR[Code], " +
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_Description]  DEFAULT('') FOR[Description], " +
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_Due]  DEFAULT((0)) FOR[Due], " +
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_Paid]  DEFAULT((0)) FOR[Paid], " + 
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_ChargeNumber]  DEFAULT((0)) FOR[ChargeNumber], " +
                    "ALTER TABLE[dbo].[Lockbox] ADD CONSTRAINT[DF_Lockbox_Amount]  DEFAULT((0)) FOR[Amount]";


        protected int m_Account;
		protected int m_ChargeNumber;
		protected int m_ID;
		protected decimal m_Due;
		protected decimal m_Paid;
		protected decimal m_Amount;
		protected string m_Code;
		protected string m_Description;
		protected bool m_isChanged;

		public Lockbox() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
		}

		public Lockbox(int Account,int ChargeNumber,int ID,decimal Due,decimal Paid,decimal Amount,string Code,string Description) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{
			m_Account = Account;
			m_ChargeNumber = ChargeNumber;
			m_ID = ID;
			m_Due = Due;
			m_Paid = Paid;
			m_Amount = Amount;
			m_Code = Code;
			m_Description = Description;
		}

		protected Lockbox fill(DataRow dr)
		{
			try {
				m_Account = (int) dr[SQL_COL_ACCOUNT];
				m_ChargeNumber = (int) dr[SQL_COL_CHARGE_NUMBER];
				m_ID = (int) dr[SQL_COL_I_D];
				m_Due = (decimal) dr[SQL_COL_DUE];
				m_Paid = (decimal) dr[SQL_COL_PAID];
				m_Amount = (decimal) dr[SQL_COL_AMOUNT];
				m_Code = (string) (Convert.IsDBNull(dr[SQL_COL_CODE]) ? "" : dr[SQL_COL_CODE]);
				m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			} catch (SqlException ex) {
			}
			return this;

		}

		public static Lockbox load( int m_ID)
		{
			Lockbox obj  = new Lockbox();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static Lockbox[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Lockbox obj = new Lockbox();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new Lockbox();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Lockbox[])list.ToArray(typeof(Lockbox));
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
			if (m_ID > 0 ) {
				string[] values = {m_Account.ToString(), m_ChargeNumber.ToString(), m_Due.ToString(), m_Paid.ToString(), m_Amount.ToString(), m_Code, m_Description, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_Account.ToString(), m_ChargeNumber.ToString(), m_Due.ToString(), m_Paid.ToString(), m_Amount.ToString(), m_Code, m_Description};
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
				m_isChanged = true;
			}
		}

		// Properties
		public int Account {
			set{ m_Account = value;
			OnPropertyChanged("Account"); }
			get { return m_Account; }
		}

		public int ChargeNumber {
			set{ m_ChargeNumber = value;
			OnPropertyChanged("ChargeNumber"); }
			get { return m_ChargeNumber; }
		}

		public int ID {
			get { return m_ID; }
		}

		public decimal Due {
			set{ m_Due = value;
			OnPropertyChanged("Due"); }
			get { return m_Due; }
		}

		public decimal Paid {
			set{ m_Paid = value;
			OnPropertyChanged("Paid"); }
			get { return m_Paid; }
		}

		public decimal Amount {
			set{ m_Amount = value;
			OnPropertyChanged("Amount"); }
			get { return m_Amount; }
		}

		public string Code {
			set{ m_Code = value;
			OnPropertyChanged("Code"); }
			get { return m_Code; }
		}

		public string Description {
			set{ m_Description = value;
			OnPropertyChanged("Description"); }
			get { return m_Description; }
		}

}
}
