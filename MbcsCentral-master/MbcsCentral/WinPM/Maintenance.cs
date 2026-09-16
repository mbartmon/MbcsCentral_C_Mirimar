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

	public class Maintenance : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_Maintenance";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_DATE = "Date";
		public const string SQL_COL_LINE_NUMBER = "Line Number";
		public const string SQL_COL_DESCRIPTION = "Description";
		public const string SQL_COL_VENDOR = "Vendor";
		public const string SQL_COL_COST = "Cost";
        public const string SQL_COL_CLIENT_ID = "clientId";
		public const string SQL_COL_XRF_TESTED = "XrfTested";
		public const string SQL_COL_XRF_PASSED = "XrfPassed";
		public const string SQL_COL_XRF_FAILED = "XrfFailed";

		public const string SQL_COL_INSPECTED = "Inspected";

		static string SQL_LOAD = "SELECT " +
								 "[" + SQL_COL_BUILDING_NUMBER + "]," +
								 "[" + SQL_COL_TENANT_NUMBER + "]," +
								 "[" + SQL_COL_DATE + "]," +
								 "[" + SQL_COL_LINE_NUMBER + "]," +
								 "[" + SQL_COL_DESCRIPTION + "]," +
								 "[" + SQL_COL_VENDOR + "]," +
								 "[" + SQL_COL_COST + "]," +
                                 "[" + SQL_COL_INSPECTED + "]," +
                                 "[" + SQL_COL_XRF_TESTED + "]," +
                                 "[" + SQL_COL_XRF_PASSED + "]," +
                                 "[" + SQL_COL_XRF_FAILED + "]" +
                                 " FROM " + TABLE +
                                 " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" + 
								 " AND [" + SQL_COL_TENANT_NUMBER + "] = '?'" + 
								 " AND [" + SQL_COL_DATE + "] = ?" + 
								 " AND [" + SQL_COL_LINE_NUMBER + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " + 
									 "[" + SQL_COL_BUILDING_NUMBER + "]," + "" +
									 "[" + SQL_COL_TENANT_NUMBER + "]," + 
									 "[" + SQL_COL_DATE + "]," + 
									 "[" + SQL_COL_LINE_NUMBER + "]," + 
									 "[" + SQL_COL_DESCRIPTION + "]," + 
									 "[" + SQL_COL_VENDOR + "]," + 
									 "[" + SQL_COL_COST + "]," +
									 "[" + SQL_COL_INSPECTED + "]," +
									 "[" + SQL_COL_XRF_TESTED + "]," +
									 "[" + SQL_COL_XRF_PASSED + "]," +
									 "[" + SQL_COL_XRF_FAILED + "]" +
                                     " FROM " + TABLE + ";";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" + 
                                   "[" + SQL_COL_BUILDING_NUMBER + "]," + 
									"[" + SQL_COL_TENANT_NUMBER + "]," + 
									"[" + SQL_COL_DATE + "]," + 
									"[" + SQL_COL_DESCRIPTION + "]," + 
									"[" + SQL_COL_VENDOR + "]," + 
									"[" + SQL_COL_COST + "]," +
									"[" + SQL_COL_INSPECTED + "]," +
									"[" + SQL_COL_XRF_TESTED + "]," +
									"[" + SQL_COL_XRF_PASSED + "]," +
									"[" + SQL_COL_XRF_FAILED + "]," +
                                    SQL_COL_CLIENT_ID +
                                   ") VALUES ('?', '?', '?', '?', '?', ?, ?, ?, ?, ?, ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
                                   " SET " + 
                                   "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + 
									"[" + SQL_COL_TENANT_NUMBER + "]='?'," + 
									"[" + SQL_COL_DATE + "]='?'," + 
									"[" + SQL_COL_DESCRIPTION + "]='?'," + 
									"[" + SQL_COL_VENDOR + "]='?'," + 
									"[" + SQL_COL_COST + "]=?," +
                                    "[" + SQL_COL_INSPECTED + "]=?," +
									"[" + SQL_COL_XRF_TESTED + "]=?," +
									"[" + SQL_COL_XRF_PASSED + "]=?," +
									"[" + SQL_COL_XRF_FAILED + "]=?," +
                                    SQL_COL_CLIENT_ID + "=?" +
                                   " WHERE [" + SQL_COL_LINE_NUMBER + "] = ?;";

		static string SQL_LOAD_BY_TENANT_NUMBER = " SELECT " + 
                                                  "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                  "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                  "[" + SQL_COL_DATE + "]," + 
                                                  "[" + SQL_COL_LINE_NUMBER + "]," + 
                                                  "[" + SQL_COL_DESCRIPTION + "]," + 
                                                  "[" + SQL_COL_VENDOR + "]," + 
                                                  "[" + SQL_COL_COST + "]," +
												  "[" + SQL_COL_INSPECTED + "]," +
												  "[" + SQL_COL_XRF_TESTED + "]," +
												  "[" + SQL_COL_XRF_PASSED + "]," +
												  "[" + SQL_COL_XRF_FAILED + "]" +
                                                  " FROM " + TABLE +
                                                  " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" + 
                                                  " AND [" + SQL_COL_TENANT_NUMBER + "] = '?'" + 
                                                  " AND " + SQL_COL_CLIENT_ID + "=?" +
                                                  " ORDER BY [" + SQL_COL_DATE + "] ASC";

		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected DateTime m_Date;
		protected int m_LineNumber;
		protected string m_Description;
		protected string m_Vendor;
		protected decimal m_Cost;
		protected bool m_Inspected;
        protected long m_clientId;
		protected bool m_XrfTested;
		protected bool m_XrfPassed;
		protected bool m_XrfFailed;

		protected bool m_isChanged;
		public Maintenance() : base(TABLE, SQL_COL_LINE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
            m_clientId = GlobalShared.user.Account.clientId;
		}

		public Maintenance(string BuildingNumber, string TenantNumber, DateTime mDate, int LineNumber, string Description, string Vendor, decimal Cost, bool Inspected) : base(TABLE, SQL_COL_LINE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_Date = mDate;
			m_LineNumber = LineNumber;
			m_Description = Description;
			m_Vendor = Vendor;
			m_Cost = Cost;
			m_Inspected = Inspected;
            m_clientId = GlobalShared.user.Account.clientId;
		}

		protected Maintenance fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_Date = (DateTime)dr[SQL_COL_DATE];
				m_LineNumber = (int)dr[SQL_COL_LINE_NUMBER];
				m_Description = (string)(Information.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
				m_Vendor = (string)(Information.IsDBNull(dr[SQL_COL_VENDOR]) ? "" : dr[SQL_COL_VENDOR]);
				m_Cost =(decimal) dr[SQL_COL_COST];
				m_Inspected = (bool)dr[SQL_COL_INSPECTED];
				m_XrfTested = (bool)dr[SQL_COL_XRF_TESTED];
				m_XrfPassed = (bool)dr[SQL_COL_XRF_PASSED];
				m_XrfFailed = (bool)dr[SQL_COL_XRF_FAILED];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Maintenance load(string m_BuildingNumber, string m_TenantNumber, DateTime m_Date, int m_LineNumber)
		{

			Maintenance obj = new Maintenance();
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

		public static Maintenance[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Maintenance obj = new Maintenance();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Maintenance();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Maintenance[])list.ToArray(typeof(Maintenance));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Maintenance[] load(string buildingNumber, string tenantNumber, long clientId)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Maintenance obj = new Maintenance();
			string[] opt = {
				buildingNumber,
				tenantNumber,
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
				obj = new Maintenance();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Maintenance[])list.ToArray(typeof(Maintenance));
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
            m_Description = m_Description.Replace("'", "\"");
			if (m_LineNumber > 0) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_Date.ToString(),
					m_Description,
					m_Vendor,
					m_Cost.ToString(),
					m_Inspected.ToString(),
					m_XrfTested.ToString(),
					m_XrfPassed.ToString(),
					m_XrfFailed.ToString(),
                    m_clientId.ToString(),
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
					m_Description,
					m_Vendor,
					m_Cost.ToString(),
					m_Inspected.ToString(),
					m_XrfTested.ToString(),
					m_XrfPassed.ToString(),
					m_XrfFailed.ToString(),
                    m_clientId.ToString()
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
				cmd.CommandText = "CREATE TABLE Maintenance (Building Number TEXT(3),Tenant Number TEXT(3),Date DATETIME,Line Number AUTONUMBER,Description MEMO,Vendor TEXT(15),Cost CURRENCY,Inspected YESNO)";
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

		public DateTime MaintenanceDate {
			get { return m_Date; }
			set {
				m_Date = value;
				OnPropertyChanged("Date");
			}
		}

		public int LineNumber {
			get { return m_LineNumber; }
		}

		public string Description {
			get { return m_Description; }
			set {
				m_Description = value;
				OnPropertyChanged("Description");
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

		public bool Inspected {
			get { return m_Inspected; }
			set {
				m_Inspected = value;
				OnPropertyChanged("Inspected");
			}
		}

		public bool XrfTested
        {
			get { return m_XrfTested; }
			set
            {
				m_XrfTested = value;
				OnPropertyChanged("XrfTested");
            }
        }

		public bool XrfPassed
        {
			get { return m_XrfPassed; }
			set
            {
				m_XrfPassed = value;
				OnPropertyChanged("XrfPassed");
            }
        }

		public bool XrfFailed
        {
			get { return m_XrfFailed; }
			set
            {
				m_XrfFailed = value;
				OnPropertyChanged("XrfFailed");
            }
        }


	}
}
