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

	public class Improvements : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_Improvements";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_DATE = "Date";
		public const string SQL_COL_LINE_NUMBER = "Line Number";
		public const string SQL_COL_TYPE = "Type";
		public const string SQL_COL_BRAND = "Brand";
		public const string SQL_COL_MODEL = "model";
		public const string SQL_COL_SIZE = "size";
		public const string SQL_COL_COLOR = "Color";
		public const string SQL_COL_VENDOR = "Vendor";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_RENT_INCREASE_AMOUNT = "RentIncreaseAmount";
        public const string SQL_COL_RENT_INCREASE_DATE = "RentIncreaseDate";

		public const string SQL_COL_COST = "Cost";

		static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_DATE + "]," + 
                                "[" + SQL_COL_LINE_NUMBER + "]," + 
                                "[" + SQL_COL_TYPE + "]," + 
                                "[" + SQL_COL_BRAND + "]," + 
                                "[" + SQL_COL_MODEL + "]," + 
                                "[" + SQL_COL_SIZE + "]," + 
                                "[" + SQL_COL_COLOR + "]," + 
                                "[" + SQL_COL_VENDOR + "]," +
                                "[" + SQL_COL_COST + "]," + 
                                "[" + SQL_COL_RENT_INCREASE_AMOUNT + "]," +
                                "[" + SQL_COL_RENT_INCREASE_DATE + "]" +
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_BUILDING_NUMBER + " = '?' " + 
                                " AND " + SQL_COL_TENANT_NUMBER + " = '?'" + 
                                " AND " + SQL_COL_DATE + " = ?" + 
                                " AND " + SQL_COL_LINE_NUMBER + " = ?;";

        static string SQL_LOAD_ALL = " SELECT " +
                                     "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                     "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                     "[" + SQL_COL_DATE + "]," + 
                                     "[" + SQL_COL_LINE_NUMBER + "]," + 
                                     "[" + SQL_COL_TYPE + "]," + 
                                     "[" + SQL_COL_BRAND + "]," + 
                                     "[" + SQL_COL_MODEL + "]," + 
                                     "[" + SQL_COL_SIZE + "]," +     
                                     "[" + SQL_COL_COLOR + "]," + 
                                     "[" + SQL_COL_VENDOR + "]," +
                                     "[" + SQL_COL_COST + "]," +
                                     "[" + SQL_COL_RENT_INCREASE_AMOUNT + "]," +
                                     "[" + SQL_COL_RENT_INCREASE_DATE + "]" +
                                     " FROM " + TABLE +
                                     " WHERE " + SQL_COL_CLIENT_ID + "=?";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" +
                                   "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_TYPE + "]," + 
                                    "[" + SQL_COL_BRAND + "]," + 
                                    "[" + SQL_COL_MODEL + "]," + 
                                    "[" + SQL_COL_SIZE + "]," + 
                                    "[" + SQL_COL_COLOR + "]," + 
                                    "[" + SQL_COL_VENDOR + "]," + 
                                    "[" + SQL_COL_COST + "]," + 
                                    "[" + SQL_COL_CLIENT_ID + "]," + 
                                   "[" + SQL_COL_RENT_INCREASE_AMOUNT + "]," +
                                   "[" + SQL_COL_RENT_INCREASE_DATE + "]" +

                                   ") VALUES ('?', '?', '?', '?', '?', '?', '?', '?', '?', ?, ?, ?, '?');";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
                                   " SET " + 
                                   "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_DATE + "]='?'," + 
                                    "[" + SQL_COL_TYPE + "]='?'," + 
                                    "[" + SQL_COL_BRAND + "]='?'," +     
                                    "[" + SQL_COL_MODEL + "]='?'," + 
                                    "[" + SQL_COL_SIZE + "]='?'," + 
                                    "[" + SQL_COL_COLOR + "]='?'," + 
                                    "[" + SQL_COL_VENDOR + "]='?'," +
                                    "[" + SQL_COL_COST + "]=?," + 
                                    "[" + SQL_COL_CLIENT_ID + "]=?," +
                                   "[" + SQL_COL_RENT_INCREASE_AMOUNT + "]=?," +
                                   "[" + SQL_COL_RENT_INCREASE_DATE + "]='?'" +

                                   " WHERE [" + SQL_COL_LINE_NUMBER + "] = ?;";

		static string SQL_LOAD_BY_TENANT_NUMBER = " SELECT " + 
                                                  "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                  "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                  "[" + SQL_COL_DATE + "]," + 
                                                  "[" + SQL_COL_LINE_NUMBER + "]," + 
                                                  "[" + SQL_COL_TYPE + "]," + 
                                                  "[" + SQL_COL_BRAND + "]," + 
                                                  "[" + SQL_COL_MODEL + "]," + 
                                                  "[" + SQL_COL_SIZE + "]," + 
                                                  "[" + SQL_COL_COLOR + "]," + 
                                                  "[" + SQL_COL_VENDOR + "]," +
                                                  "[" + SQL_COL_COST + "]," +
                                                  "[" + SQL_COL_RENT_INCREASE_AMOUNT + "]," +
                                                  "[" + SQL_COL_RENT_INCREASE_DATE + "]" +
                                                  " FROM " + TABLE +
                                                  " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" + 
                                                  " AND [" + SQL_COL_TENANT_NUMBER + "] = '?'" + 
                                                  " AND " + SQL_COL_CLIENT_ID + "=?" +
                                                  " ORDER BY [" + SQL_COL_DATE + "] ASC";
		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected DateTime m_Date;
		protected int m_LineNumber;
		protected string m_Type;
		protected string m_Brand = "";
		protected string m_model;
		protected string m_size;
		protected string m_Color;
		protected string m_Vendor;
		protected decimal m_Cost;
        protected decimal m_RentIncreaseAmount;
        protected DateTime m_RentIncreaseDate;
        protected long m_clientId;

		protected bool m_isChanged;
		public Improvements() : base(TABLE, SQL_COL_LINE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
            m_clientId = GlobalShared.user.Account.clientId;
		}

		public Improvements(string BuildingNumber, string TenantNumber, DateTime mDate, int LineNumber, string Type, string Brand, string model, string size, string Color, string Vendor,

		decimal Cost) : base(TABLE, SQL_COL_LINE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_Date = mDate;
			m_LineNumber = LineNumber;
			m_Type = Type;
			m_Brand = Brand;
			m_model = model;
			m_size = size;
			m_Color = Color;
			m_Vendor = Vendor;
			m_Cost = Cost;
            m_clientId = GlobalShared.user.Account.clientId;

		}

		protected Improvements fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_Date = (DateTime)dr[SQL_COL_DATE];
				m_LineNumber = (int)dr[SQL_COL_LINE_NUMBER];
				m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
				m_Brand = (string)(Information.IsDBNull(dr[SQL_COL_BRAND]) ? "" : dr[SQL_COL_BRAND]);
				m_model = (string)(Information.IsDBNull(dr[SQL_COL_MODEL]) ? "" : dr[SQL_COL_MODEL]);
				m_size = (string)(Information.IsDBNull(dr[SQL_COL_SIZE]) ? "" : dr[SQL_COL_SIZE]);
				m_Color = (string)(Information.IsDBNull(dr[SQL_COL_COLOR]) ? "" : dr[SQL_COL_COLOR]);
				m_Vendor = (string)(Information.IsDBNull(dr[SQL_COL_VENDOR]) ? "" : dr[SQL_COL_VENDOR]);
				m_Cost = (decimal)dr[SQL_COL_COST];
                m_RentIncreaseAmount = (decimal)dr[SQL_COL_RENT_INCREASE_AMOUNT];
                m_RentIncreaseDate = (DateTime)dr[SQL_COL_RENT_INCREASE_DATE];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Improvements load(string m_BuildingNumber, string m_TenantNumber, DateTime m_Date, int m_LineNumber)
		{

			Improvements obj = new Improvements();
			string[] opt = {
				m_BuildingNumber,
				m_TenantNumber,
				m_Date.ToString(),
				m_LineNumber.ToString()
			};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Improvements[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Improvements obj = new Improvements();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Improvements();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Improvements[])list.ToArray(typeof(Improvements));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Improvements[] load(string buildingNumber, string tenantnumber, long clientId)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Improvements obj = new Improvements();
			string[] opt = {
				buildingNumber,
				tenantnumber,
                clientId.ToString()
			};
			ds = obj.load(SQL_LOAD_BY_TENANT_NUMBER, opt);
			obj = null;
			DataRow dr = null;
			if ((ds == null) || ds.Tables[0].Rows.Count == 0) {
				return null;
			}
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Improvements();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Improvements[])list.ToArray(typeof(Improvements));
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
            m_Brand = m_Brand.Replace("'", "\"");
			if (m_LineNumber > 0) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_Date.ToString(),
					m_Type,
					m_Brand,
					m_model,
					m_size,
					m_Color,
					m_Vendor,
					m_Cost.ToString(),
                    m_clientId.ToString(),
                    m_RentIncreaseAmount.ToString(),
                    m_RentIncreaseDate.ToString(),
                    m_LineNumber.ToString()
                };
				if (this.updateRecord(m_LineNumber.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
                string[] values = {
                    m_BuildingNumber,
                    m_TenantNumber,
                    m_Date.ToString(),
                    m_Type,
                    m_Brand,
                    m_model,
                    m_size,
                    m_Color,
                    m_Vendor,
                    m_Cost.ToString(),
                    m_clientId.ToString(),
                    m_RentIncreaseAmount.ToString(),
                    m_RentIncreaseDate.ToString(),
				};
				m_LineNumber = this.insertRecord(values);
				if (m_LineNumber >= 0) {
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
				cmd.CommandText = "CREATE TABLE Improvements (Building Number TEXT(3),Tenant Number TEXT(3),Date DATETIME,Line Number AUTONUMBER,Type TEXT(16),Brand TEXT(15),model TEXT(15),size TEXT(10),Color TEXT(10),Vendor TEXT(15),Cost CURRENCY)";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (System.Data.OleDb.OleDbException ex) {
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

		public DateTime ImprovementDate {
			get { return m_Date; }
			set {
				m_Date = value;
				OnPropertyChanged("Date");
			}
		}

		public int LineNumber {
			get { return m_LineNumber; }
		}

		public string Type {
			get { return m_Type; }
			set {
				m_Type = value;
				OnPropertyChanged("Type");
			}
		}

		public string Brand {
			get { return m_Brand; }
			set {
				m_Brand = value;
				OnPropertyChanged("Brand");
			}
		}

		public string Model {
			get { return m_model; }
			set {
				m_model = value;
				OnPropertyChanged("model");
			}
		}

		public string Size {
			get { return m_size; }
			set {
				m_size = value;
				OnPropertyChanged("size");
			}
		}

		public string Color {
			get { return m_Color; }
			set {
				m_Color = value;
				OnPropertyChanged("Color");
			}
		}

		public string Vendor {
			get { return m_Vendor; }
			set {
				m_Vendor = value;
				OnPropertyChanged("Vendor");
			}
		}

		public decimal Cost {
			get { return m_Cost; }
			set {
				m_Cost = value;
				OnPropertyChanged("Cost");
			}
		}

        public decimal RentIncreaseAmount
        {
            get { return m_RentIncreaseAmount; }
            set
            {
                m_RentIncreaseAmount = value;
                OnPropertyChanged("RentIncreaseAmount");
            }
        }

        public DateTime RentIncreaseDate
        {
            get { return m_RentIncreaseDate; }
            set
            {
                m_RentIncreaseDate = value;
                OnPropertyChanged("RentIncreaseDate");
            }
        }

	}
}
