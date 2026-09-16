using System;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.VisualBasic;
namespace MbcsCentral.WinTrm
{

	public class Recipients : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "Recipients";
		public const string SQL_COL_RECIPIENT_ID = "recipient_Id";
		public const string SQL_COL_LIST_ID = "list_Id";
		public const string SQL_COL_FIRST_NAME = "first_name";
		public const string SQL_COL_LAST_NAME = "last_name";
		public const string SQL_COL_EMAIL_ADDRESS = "email_address";
		public const string SQL_COL_ADDRESS_ONE = "address_one";
		public const string SQL_COL_ADDRESS_TWO = "address_two";
		public const string SQL_COL_ADDRESS_THREE = "address_three";
		public const string SQL_COL_CITY = "city";
		public const string SQL_COL_STATE = "state";
		public const string SQL_COL_ZIP = "zip";
		public const string SQL_COL_PHONE = "phone";

		public const string SQL_COL_DATE_OF_BIRTH = "date_of_birth";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_RECIPIENT_ID + "]," + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_FIRST_NAME + "]," + "[" + SQL_COL_LAST_NAME + "]," + "[" + SQL_COL_EMAIL_ADDRESS + "]," + "[" + SQL_COL_ADDRESS_ONE + "]," + "[" + SQL_COL_ADDRESS_TWO + "]," + "[" + SQL_COL_ADDRESS_THREE + "]," + "[" + SQL_COL_CITY + "]," + "[" + SQL_COL_STATE + "]," + "[" + SQL_COL_ZIP + "]," + "[" + SQL_COL_PHONE + "]," + "[" + SQL_COL_DATE_OF_BIRTH + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_RECIPIENT_ID + " = ?;";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_RECIPIENT_ID + "]," + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_FIRST_NAME + "]," + "[" + SQL_COL_LAST_NAME + "]," + "[" + SQL_COL_EMAIL_ADDRESS + "]," + "[" + SQL_COL_ADDRESS_ONE + "]," + "[" + SQL_COL_ADDRESS_TWO + "]," + "[" + SQL_COL_ADDRESS_THREE + "]," + "[" + SQL_COL_CITY + "]," + "[" + SQL_COL_STATE + "]," + "[" + SQL_COL_ZIP + "]," + "[" + SQL_COL_PHONE + "]," + "[" + SQL_COL_DATE_OF_BIRTH + "]" + " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_FIRST_NAME + "]," + "[" + SQL_COL_LAST_NAME + "]," + "[" + SQL_COL_EMAIL_ADDRESS + "]," + "[" + SQL_COL_ADDRESS_ONE + "]," + "[" + SQL_COL_ADDRESS_TWO + "]," + "[" + SQL_COL_ADDRESS_THREE + "]," + "[" + SQL_COL_CITY + "]," + "[" + SQL_COL_STATE + "]," + "[" + SQL_COL_ZIP + "]," + "[" + SQL_COL_PHONE + "]," + "[" + SQL_COL_DATE_OF_BIRTH + "]" + ") VALUES (?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?');";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_LIST_ID + "]=?," + "[" + SQL_COL_FIRST_NAME + "]='?'," + "[" + SQL_COL_LAST_NAME + "]='?'," + "[" + SQL_COL_EMAIL_ADDRESS + "]='?'," + "[" + SQL_COL_ADDRESS_ONE + "]='?'," + "[" + SQL_COL_ADDRESS_TWO + "]='?'," + "[" + SQL_COL_ADDRESS_THREE + "]='?'," + "[" + SQL_COL_CITY + "]='?'," + "[" + SQL_COL_STATE + "]='?'," + "[" + SQL_COL_ZIP + "]='?'," + "[" + SQL_COL_PHONE + "]='?'," + "[" + SQL_COL_DATE_OF_BIRTH + "]='?'" + " WHERE " + SQL_COL_RECIPIENT_ID + " = ?;";
		// Addtional SQL Queries

		static string SQL_LOAD_BY_LIST = " SELECT " + "[" + SQL_COL_RECIPIENT_ID + "]," + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_FIRST_NAME + "]," + "[" + SQL_COL_LAST_NAME + "]," + "[" + SQL_COL_EMAIL_ADDRESS + "]," + "[" + SQL_COL_ADDRESS_ONE + "]," + "[" + SQL_COL_ADDRESS_TWO + "]," + "[" + SQL_COL_ADDRESS_THREE + "]," + "[" + SQL_COL_CITY + "]," + "[" + SQL_COL_STATE + "]," + "[" + SQL_COL_ZIP + "]," + "[" + SQL_COL_PHONE + "]," + "[" + SQL_COL_DATE_OF_BIRTH + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_LIST_ID + " = ?;";
		// Fields
		protected int m_recipient_Id;
		protected int m_list_Id;
		protected string m_first_name;
		protected string m_last_name;
		protected string m_email_address;
		protected string m_address_one;
		protected string m_address_two;
		protected string m_address_three;
		protected string m_city;
		protected string m_state;
		protected string m_zip;
		protected string m_phone;
		protected DateTime m_date_of_birth;

		protected bool m_isChanged = false;
		public Recipients() : base(TABLE, SQL_COL_RECIPIENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Recipients(int recipient_Id, int list_Id, string first_name, string last_name, string email_address, string address_one, string address_two, string address_three, string city, string state,
		string zip, string phone, DateTime date_of_birth) : base(TABLE, SQL_COL_RECIPIENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_recipient_Id = recipient_Id;
			m_list_Id = list_Id;
			m_first_name = first_name;
			m_last_name = last_name;
			m_email_address = email_address;
			m_address_one = address_one;
			m_address_two = address_two;
			m_address_three = address_three;
			m_city = city;
			m_state = state;
			m_zip = zip;
			m_phone = phone;
			m_date_of_birth = date_of_birth;
		}

		protected Recipients fill(DataRow dr)
		{

			try {
				m_recipient_Id = (int)dr[SQL_COL_RECIPIENT_ID];
				m_list_Id = (int)dr[SQL_COL_LIST_ID];
				m_first_name = (string)(Information.IsDBNull(dr[SQL_COL_FIRST_NAME]) ? "" : dr[SQL_COL_FIRST_NAME]);
				m_last_name = (string)(Information.IsDBNull(dr[SQL_COL_LAST_NAME]) ? "" : dr[SQL_COL_LAST_NAME]);
				m_email_address = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL_ADDRESS]) ? "" : dr[SQL_COL_EMAIL_ADDRESS]);
				m_address_one = (string)(Information.IsDBNull(dr[SQL_COL_ADDRESS_ONE]) ? "" : dr[SQL_COL_ADDRESS_ONE]);
				m_address_two = (string)(Information.IsDBNull(dr[SQL_COL_ADDRESS_TWO]) ? "" : dr[SQL_COL_ADDRESS_TWO]);
				m_address_three = (string)(Information.IsDBNull(dr[SQL_COL_ADDRESS_THREE]) ? "" : dr[SQL_COL_ADDRESS_THREE]);
				m_city = (string)(Information.IsDBNull(dr[SQL_COL_CITY]) ? "" : dr[SQL_COL_CITY]);
				m_state = (string)(Information.IsDBNull(dr[SQL_COL_STATE]) ? "" : dr[SQL_COL_STATE]);
				m_zip = (string)(Information.IsDBNull(dr[SQL_COL_ZIP]) ? "" : dr[SQL_COL_ZIP]);
				m_phone = (string)(Information.IsDBNull(dr[SQL_COL_PHONE]) ? "" : dr[SQL_COL_PHONE]);
				m_date_of_birth = (DateTime)(Information.IsDBNull(dr[SQL_COL_DATE_OF_BIRTH]) ? MbcsCentral.GlobalShared.NULL_DATE : dr[SQL_COL_DATE_OF_BIRTH]);
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Recipients load(int m_recipient_Id)
		{

			Recipients obj = new Recipients();
			string[] opt = { m_recipient_Id.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Recipients[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Recipients obj = new Recipients();

			try {
				ds = obj.loadAll();
			} catch (SqlException ex) {
				return null;
			} catch (System.Exception ex) {
				return null;
			}
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new Recipients();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Recipients[])list.ToArray(typeof(Recipients));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Recipients[] loadByList(int m_list_Id)
		{

			ArrayList list = new ArrayList();

			Recipients obj = new Recipients();
			string[] opt = { m_list_Id.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_LIST, opt);
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new Recipients();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}

			try {
				return (Recipients[])list.ToArray(typeof(Recipients));
			} catch (SqlException ex) {
				return null;
			}

		}

		public bool save()
		{

			if (m_recipient_Id > 0) {
				string[] values = {
					m_list_Id.ToString(),
					m_first_name,
					m_last_name,
					m_email_address,
					m_address_one,
					m_address_two,
					m_address_three,
					m_city,
					m_state,
					m_zip,
					m_phone,
					m_date_of_birth.ToString(),
					m_recipient_Id.ToString()
				};
				if (this.updateRecord(m_recipient_Id.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_list_Id.ToString(),
					m_first_name,
					m_last_name,
					m_email_address,
					m_address_one,
					m_address_two,
					m_address_three,
					m_city,
					m_state,
					m_zip,
					m_phone,
					m_date_of_birth.ToString()
				};
				m_recipient_Id = this.insertRecord(values);
				if (m_recipient_Id >= 0) {
					return true;
				}
			}
			return false;

		}

		public bool delete()
		{

			return this.delete(recipient_Id.ToString());

		}

		public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
		{

			try {
				CN.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Recipients (recipient_Id LONG,list_Id LONG,first_name TEXT(32),last_name TEXT(32),email_address TEXT(128),address_one TEXT(32),address_two TEXT(32),address_three TEXT(32),city TEXT(32),state TEXT(32),zip TEXT(10),phone TEXT(32),date_of_birth DATETIME)";
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

		public int recipient_Id {
			get { return m_recipient_Id; }
		}

		public int list_Id {
			get { return m_list_Id; }
			set {
				m_list_Id = value;
				OnPropertyChanged("list_id");
			}
		}

		public string first_name {
			get { return m_first_name; }
			set {
				m_first_name = value;
				OnPropertyChanged("first_name");
			}
		}

		public string last_name {
			get { return m_last_name; }
			set {
				m_last_name = value;
				OnPropertyChanged("last_name");
			}
		}

		public string email_address {
			get { return m_email_address; }
			set {
				m_email_address = value;
				OnPropertyChanged("email_address");
			}
		}

		public string address_one {
			get { return m_address_one; }
			set {
				m_address_one = value;
				OnPropertyChanged("address_one");
			}
		}

		public string address_two {
			get { return m_address_two; }
			set {
				m_address_two = value;
				OnPropertyChanged("address_two");
			}
		}

		public string address_three {
			get { return m_address_three; }
			set {
				m_address_three = value;
				OnPropertyChanged("address_three");
			}
		}


		public string city {
			get { return m_city; }
			set {
				m_city = value;
				OnPropertyChanged("city");
			}
		}

		public string state {
			get { return m_state; }
			set {
				m_state = value;
				OnPropertyChanged("state");
			}
		}

		public string zip {
			get { return m_zip; }
			set {
				m_zip = value;
				OnPropertyChanged("zip");
			}
		}

		public string phone {
			get { return m_phone; }
			set {
				m_phone = value;
				OnPropertyChanged("phone");
			}
		}

		public DateTime date_of_birth {
			get { return m_date_of_birth; }
			set {
				m_date_of_birth = value;
				OnPropertyChanged("date_of_birth");
			}
		}
		public bool isChanged {
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}

	}
}
