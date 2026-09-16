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

	public class Legal : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_Legal";
		public const string SQL_COL_LEGALID = "legalId";
		public const string SQL_COL_I_D = "ID";
		public const string SQL_COL_YEAR = "Year";
		public const string SQL_COL_AN_W_G_CHILDREN = "AnWGChildren";
		public const string SQL_COL_AN_W_G_NO_CHILDREN = "AnWGNoChildren";
		public const string SQL_COL_AN_W_G_WANT = "AnWGWant";
		public const string SQL_COL_AN_W_G_INSTALLED = "AnWGInstalled";
		public const string SQL_COL_AN_W_G_NOT_INSTALLED = "AnWGNotInstalled";
		public const string SQL_COL_AN_W_G_MAINTENANCE = "AnWGMaintenance";
		public const string SQL_COL_AN_W_G_NO_MAINTENANCE = "AnWGNoMaintenance";
		public const string SQL_COL_RN_W_G_CHILDREN = "RnWGChildren";
		public const string SQL_COL_RN_W_G_NO_CHILDREN = "RnWGNoChildren";
		public const string SQL_COL_RN_W_G_WANT = "RnWGWant";
		public const string SQL_COL_AN_L_P_CHILDREN = "AnLPChildren";
		public const string SQL_COL_RN_L_P_CHILDREN = "RnLPChildren";
		public const string SQL_COL_AN_L_P_CHILDREN_NO = "AnLPChildrenNo";
		public const string SQL_COL_RN_L_P_CHILDREN_NO = "RnLPChildrenNo";
		public const string SQL_COL_AnWGChildrenGrp = "AnWGChildrenGrp";
		public const string SQL_COL_AnWGInstalledGrp = "AnWGInstalledGrp";
		public const string SQL_COL_AnWGMaintenanceGrp = "AnWGMaintenanceGrp";
		public const string SQL_COL_RnWGChildrenGrp = "RnWGChildrenGrp";
		public const string SQL_COL_AnLPChildrenGrp = "AnLPChildrenGrp";

		public const string SQL_COL_RnLPChildrenGrp = "RnLPChildrenGrp";

		static string SQL_LOAD = " SELECT " + "[" + SQL_COL_LEGALID + "], " + "[" + SQL_COL_I_D + "]," + "[" + SQL_COL_YEAR + "]," + "[" + SQL_COL_AN_W_G_CHILDREN + "]," + "[" + SQL_COL_AN_W_G_NO_CHILDREN + "]," + "[" + SQL_COL_AN_W_G_WANT + "]," + "[" + SQL_COL_AN_W_G_INSTALLED + "]," + "[" + SQL_COL_AN_W_G_NOT_INSTALLED + "]," + "[" + SQL_COL_AN_W_G_MAINTENANCE + "]," + "[" + SQL_COL_AN_W_G_NO_MAINTENANCE + "]," + "[" + SQL_COL_RN_W_G_CHILDREN + "]," + "[" + SQL_COL_RN_W_G_NO_CHILDREN + "]," + "[" + SQL_COL_RN_W_G_WANT + "]," + "[" + SQL_COL_AN_L_P_CHILDREN + "]," + "[" + SQL_COL_RN_L_P_CHILDREN + "]," + "[" + SQL_COL_AN_L_P_CHILDREN_NO + "]," + "[" + SQL_COL_RN_L_P_CHILDREN_NO + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_LEGALID + "] = ?";


		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_LEGALID + "], " + "[" + SQL_COL_I_D + "]," + "[" + SQL_COL_YEAR + "]," + "[" + SQL_COL_AN_W_G_CHILDREN + "]," + "[" + SQL_COL_AN_W_G_NO_CHILDREN + "]," + "[" + SQL_COL_AN_W_G_WANT + "]," + "[" + SQL_COL_AN_W_G_INSTALLED + "]," + "[" + SQL_COL_AN_W_G_NOT_INSTALLED + "]," + "[" + SQL_COL_AN_W_G_MAINTENANCE + "]," + "[" + SQL_COL_AN_W_G_NO_MAINTENANCE + "]," + "[" + SQL_COL_RN_W_G_CHILDREN + "]," + "[" + SQL_COL_RN_W_G_NO_CHILDREN + "]," + "[" + SQL_COL_RN_W_G_WANT + "]," + "[" + SQL_COL_AN_L_P_CHILDREN + "]," + "[" + SQL_COL_RN_L_P_CHILDREN + "]," + "[" + SQL_COL_AN_L_P_CHILDREN_NO + "]," + "[" + SQL_COL_RN_L_P_CHILDREN_NO + "]" + " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
            "[" + SQL_COL_I_D + "]," + 
            "[" + SQL_COL_YEAR + "]," + 
            "[" + SQL_COL_AN_W_G_CHILDREN + "]," + 
            "[" + SQL_COL_AN_W_G_NO_CHILDREN + "]," + 
            "[" + SQL_COL_AN_W_G_WANT + "]," + 
            "[" + SQL_COL_AN_W_G_INSTALLED + "]," +
            "[" + SQL_COL_AN_W_G_NOT_INSTALLED + "]," + 
            "[" + SQL_COL_AN_W_G_MAINTENANCE + "]," + 
            "[" + SQL_COL_AN_W_G_NO_MAINTENANCE + "]," + 
            "[" + SQL_COL_RN_W_G_CHILDREN + "]," + 
            "[" + SQL_COL_RN_W_G_NO_CHILDREN + "]," + 
            "[" + SQL_COL_RN_W_G_WANT + "]," + 
            "[" + SQL_COL_AN_L_P_CHILDREN + "]," + 
            "[" + SQL_COL_RN_L_P_CHILDREN + "]," + 
            "[" + SQL_COL_AN_L_P_CHILDREN_NO + "]," + 
            "[" + SQL_COL_RN_L_P_CHILDREN_NO + "]" + 
            ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_I_D + "]=?," + "[" + SQL_COL_YEAR + "]=?," + "[" + SQL_COL_AN_W_G_CHILDREN + "]=?," + "[" + SQL_COL_AN_W_G_NO_CHILDREN + "]=?," + "[" + SQL_COL_AN_W_G_WANT + "]=?," + "[" + SQL_COL_AN_W_G_INSTALLED + "]=?," + "[" + SQL_COL_AN_W_G_NOT_INSTALLED + "]=?," + "[" + SQL_COL_AN_W_G_MAINTENANCE + "]=?," + "[" + SQL_COL_AN_W_G_NO_MAINTENANCE + "]=?," + "[" + SQL_COL_RN_W_G_CHILDREN + "]=?," + "[" + SQL_COL_RN_W_G_NO_CHILDREN + "]=?," + "[" + SQL_COL_RN_W_G_WANT + "]=?," + "[" + SQL_COL_AN_L_P_CHILDREN + "]=?," + "[" + SQL_COL_RN_L_P_CHILDREN + "]=?," + "[" + SQL_COL_AN_L_P_CHILDREN_NO + "]=?," + "[" + SQL_COL_RN_L_P_CHILDREN_NO + "]=?" + " WHERE [" + SQL_COL_LEGALID + "] = ?";

        static string SQL_LOAD_BY_ID = " SELECT " +
                                       "[" + SQL_COL_LEGALID + "], " +
                                       "[" + SQL_COL_I_D + "]," +
                                       "[" + SQL_COL_YEAR + "]," +
                                       "[" + SQL_COL_AN_W_G_CHILDREN + "]," +
                                       "[" + SQL_COL_AN_W_G_NO_CHILDREN + "]," +
                                       "[" + SQL_COL_AN_W_G_WANT + "]," +
                                       "[" + SQL_COL_AN_W_G_INSTALLED + "]," +
                                       "[" + SQL_COL_AN_W_G_NOT_INSTALLED + "]," +
                                       "[" + SQL_COL_AN_W_G_MAINTENANCE + "]," +
                                       "[" + SQL_COL_AN_W_G_NO_MAINTENANCE + "]," +
                                       "[" + SQL_COL_RN_W_G_CHILDREN + "]," +
                                       "[" + SQL_COL_RN_W_G_NO_CHILDREN + "]," +
                                       "[" + SQL_COL_RN_W_G_WANT + "]," +
                                       "[" + SQL_COL_AN_L_P_CHILDREN + "]," +
                                       "[" + SQL_COL_RN_L_P_CHILDREN + "]," +
                                       "[" + SQL_COL_AN_L_P_CHILDREN_NO + "]," +
                                       "[" + SQL_COL_RN_L_P_CHILDREN_NO + "]" +
                                       " FROM " + TABLE +
                                       " WHERE [" + SQL_COL_I_D + "] = ?" +
                                       " ORDER BY " + SQL_COL_YEAR + " DESC";

        protected int m_legalId;
		protected int m_ID;
		protected Int16 m_Year;
		protected bool m_AnWGChildren;
		protected bool m_AnWGNoChildren;
		protected bool m_AnWGWant;
		protected bool m_AnWGInstalled;
		protected bool m_AnWGNotInstalled;
		protected bool m_AnWGMaintenance;
		protected bool m_AnWGNoMaintenance;
		protected bool m_RnWGChildren;
		protected bool m_RnWGNoChildren;
		protected bool m_RnWGWant;
		protected bool m_AnLPChildren;
		protected bool m_RnLPChildren;
		protected bool m_AnLPChildrenNo;
		protected bool m_RnLPChildrenNo;
		protected bool m_isChanged;
		protected string m_AnWGChildrenGrp = "AnWGChildrenGrp";
		protected string m_AnWGInstalledGrp = "AnWGInstalledGrp";
		protected string m_AnWGMaintenanceGrp = "AnWGMaintenanceGrp";
		protected string m_RnWGChildrenGrp = "RnWGChildrenGrp";
		protected string m_AnLPChildrenGrp = "AnLPChildrenGrp";

		protected string m_RnLPChildrenGrp = "RnLPChildrenGrp";
		public Legal() : base(TABLE, SQL_COL_LEGALID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Legal(int ID, Int16 Year, bool AnWGChildren, bool AnWGNoChildren, bool AnWGWant, bool AnWGInstalled, bool AnWGNotInstalled, bool AnWGMaintenance, bool AnWGNoMaintenance, bool RnWGChildren,
		bool RnWGNoChildren, bool RnWGWant, bool AnLPChildren, bool RnLPChildren, bool AnLPChildrenNo, bool RnLPChildrenNo) : base(TABLE, SQL_COL_LEGALID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_ID = ID;
			m_Year = Year;
			m_AnWGChildren = AnWGChildren;
			m_AnWGNoChildren = AnWGNoChildren;
			m_AnWGWant = AnWGWant;
			m_AnWGInstalled = AnWGInstalled;
			m_AnWGNotInstalled = AnWGNotInstalled;
			m_AnWGMaintenance = AnWGMaintenance;
			m_AnWGNoMaintenance = AnWGNoMaintenance;
			m_RnWGChildren = RnWGChildren;
			m_RnWGNoChildren = RnWGNoChildren;
			m_RnWGWant = RnWGWant;
			m_AnLPChildren = AnLPChildren;
			m_RnLPChildren = RnLPChildren;
			m_AnLPChildrenNo = AnLPChildrenNo;
			m_RnLPChildrenNo = RnLPChildrenNo;
		}

		protected Legal fill(DataRow dr)
		{

			try {
				m_legalId = (int)dr[SQL_COL_LEGALID];
				m_ID = (int)dr[SQL_COL_I_D];
				m_Year = (short)dr[SQL_COL_YEAR];
				m_AnWGChildren = (bool)dr[SQL_COL_AN_W_G_CHILDREN];
                m_AnWGNoChildren = (bool)dr[SQL_COL_AN_W_G_NO_CHILDREN];
                m_AnWGWant = (bool)dr[SQL_COL_AN_W_G_WANT];
                m_AnWGInstalled = (bool)dr[SQL_COL_AN_W_G_INSTALLED];
                m_AnWGNotInstalled = (bool)dr[SQL_COL_AN_W_G_NOT_INSTALLED];
                m_AnWGMaintenance = (bool)dr[SQL_COL_AN_W_G_MAINTENANCE];
                m_AnWGNoMaintenance = (bool)dr[SQL_COL_AN_W_G_NO_MAINTENANCE];
                m_RnWGChildren = (bool)dr[SQL_COL_RN_W_G_CHILDREN];
                m_RnWGNoChildren = (bool)dr[SQL_COL_RN_W_G_NO_CHILDREN];
                m_RnWGWant = (bool)dr[SQL_COL_RN_W_G_WANT];
                m_AnLPChildren = (bool)dr[SQL_COL_AN_L_P_CHILDREN];
                m_RnLPChildren = (bool)dr[SQL_COL_RN_L_P_CHILDREN];
                m_AnLPChildrenNo = (bool)dr[SQL_COL_AN_L_P_CHILDREN_NO];
                m_RnLPChildrenNo = (bool)dr[SQL_COL_RN_L_P_CHILDREN_NO];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		protected bool toBoolean(int v)
		{

			if (v != 0) {
				return true;
			} else {
				return false;
			}

		}

		protected int fromBoolean(bool v)
		{

			if (v ) {
				return 0;
			} else {
				return 1;
			}

		}

		public static Legal load(long m_legalId)
		{

			Legal obj = new Legal();
			string[] opt = { m_legalId.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Legal[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Legal obj = new Legal();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Legal();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Legal[])list.ToArray(typeof(Legal));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Legal[] loadById(int id)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Legal obj = new Legal();
			string[] opt = { id.ToString() };

			ds = obj.load(SQL_LOAD_BY_ID, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Legal();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Legal[])list.ToArray(typeof(Legal));
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

			if (m_legalId > 0) {
				string[] values = {
					m_ID.ToString(),
					m_Year.ToString(),
					m_AnWGChildren.ToString(),
					m_AnWGNoChildren.ToString(),
					m_AnWGWant.ToString(),
					m_AnWGInstalled.ToString(),
					m_AnWGNotInstalled.ToString(),
					m_AnWGMaintenance.ToString(),
					m_AnWGNoMaintenance.ToString(),
					m_RnWGChildren.ToString(),
					m_RnWGNoChildren.ToString(),
					m_RnWGWant.ToString(),
					m_AnLPChildren.ToString(),
					m_RnLPChildren.ToString(),
					m_AnLPChildrenNo.ToString(),
					m_RnLPChildrenNo.ToString(),
					m_legalId.ToString()
				};
				if (this.updateRecord(m_legalId.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_ID.ToString(),
					m_Year.ToString(),
					m_AnWGChildren.ToString(),
					m_AnWGNoChildren.ToString(),
					m_AnWGWant.ToString(),
					m_AnWGInstalled.ToString(),
					m_AnWGNotInstalled.ToString(),
					m_AnWGMaintenance.ToString(),
					m_AnWGNoMaintenance.ToString(),
					m_RnWGChildren.ToString(),
					m_RnWGNoChildren.ToString(),
					m_RnWGWant.ToString(),
					m_AnLPChildren.ToString(),
					m_RnLPChildren.ToString(),
					m_AnLPChildrenNo.ToString(),
					m_RnLPChildrenNo.ToString()
				};
				m_legalId = this.insertRecord(values);
				if (m_legalId >= 0) {
					return true;
				}
			}
			return false;

		}

		public bool delete()
		{
			if (m_legalId > 0) {
                return this.delete(m_legalId.ToString());
			}
			return false;
		}
		public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
		{

			try {
				CN.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Legal (ID LONG,Year INTEGER,AnWGChildren YESNO,AnWGNoChildren YESNO,AnWGWant YESNO,AnWGInstalled YESNO,AnWGNotInstalled YESNO,AnWGMaintenance YESNO,AnWGNoMaintenance YESNO,RnWGChildren YESNO,RnWGNoChildren YESNO,RnWGWant YESNO,AnLPChildren YESNO,RnLPChildren YESNO,AnLPChildrenNo YESNO,RnLPChildrenNo YESNO)";
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
		public int legalId {
			get { return m_legalId; }
		}

		public int ID {
			get { return m_ID; }
			set {
				m_ID = value;
				OnPropertyChanged("ID");
			}
		}

		public Int16 Year {
			get { return m_Year; }
			set {
				m_Year = value;
				OnPropertyChanged("Year");
			}
		}

		public bool AnWGChildren {
			get { return m_AnWGChildren; }
			set {
				m_AnWGChildren = value;
				OnPropertyChanged("AnWGChildren");
			}
		}

		public bool AnWGNoChildren {
			get { return m_AnWGNoChildren; }
			set {
				m_AnWGNoChildren = value;
				OnPropertyChanged("AnWGNoChildren");
			}
		}

		public bool AnWGWant {
			get { return m_AnWGWant; }
			set {
				m_AnWGWant = value;
				OnPropertyChanged("AnWGWant");
			}
		}

		public bool AnWGInstalled {
			get { return m_AnWGInstalled; }
			set {
				m_AnWGInstalled = value;
				OnPropertyChanged("AnWGInstalled");
			}
		}

		public bool AnWGNotInstalled {
			get { return m_AnWGNotInstalled; }
			set {
				m_AnWGNotInstalled = value;
				OnPropertyChanged("AnWGNotInstalled");
			}
		}

		public bool AnWGMaintenance {
			get { return m_AnWGMaintenance; }
			set {
				m_AnWGMaintenance = value;
				OnPropertyChanged("AnWGMaintenance");
			}
		}

		public bool AnWGNoMaintenance {
			get { return m_AnWGNoMaintenance; }
			set {
				m_AnWGNoMaintenance = value;
				OnPropertyChanged("AnWGNoMaintenance");
			}
		}

		public bool RnWGChildren {
			get { return m_RnWGChildren; }
			set {
				m_RnWGChildren = value;
				OnPropertyChanged("RnWGChildren");
			}
		}

		public bool RnWGNoChildren {
			get { return m_RnWGNoChildren; }
			set {
				m_RnWGNoChildren = value;
				OnPropertyChanged("RnWGNoChildren");
			}
		}

		public bool RnWGWant {
			get { return m_RnWGWant; }
			set {
				m_RnWGWant = value;
				OnPropertyChanged("RnWGWant");
			}
		}

		public bool AnLPChildren {
			get { return m_AnLPChildren; }
			set {
				m_AnLPChildren = value;
				OnPropertyChanged("AnLPChildren");
			}
		}

		public bool RnLPChildren {
			get { return m_RnLPChildren; }
			set {
				m_RnLPChildren = value;
				OnPropertyChanged("RnLPChildren");
			}
		}

		public bool AnLPChildrenNo {
			get { return m_AnLPChildrenNo; }
			set {
				m_AnLPChildrenNo = value;
				OnPropertyChanged("AnLPChildrenNo");
			}
		}

		public bool RnLPChildrenNo {
			get { return m_RnLPChildrenNo; }
			set {
				m_RnLPChildrenNo = value;
				OnPropertyChanged("RnLPChildrenNo");
			}
		}
		public string AnWGChildrenGrp {
			get { return m_AnWGChildrenGrp; }
			set { m_AnWGChildrenGrp = value; }
		}
		public string AnWGInstalledGrp {
			get { return m_AnWGInstalledGrp; }
			set { m_AnWGInstalledGrp = value; }
		}
		public string AnWGMaintenanceGrp {
			get { return m_AnWGMaintenanceGrp; }
			set { m_AnWGMaintenanceGrp = value; }
		}
		public string RnWGChildrenGrp {
			get { return m_RnWGChildrenGrp; }
			set { m_RnWGChildrenGrp = value; }
		}
		public string AnLPChildrenGrp {
			get { return m_AnLPChildrenGrp; }
			set { m_AnLPChildrenGrp = value; }
		}
		public string RnLPChildrenGrp {
			get { return m_RnLPChildrenGrp; }
			set { m_RnLPChildrenGrp = value; }
		}

        public string AnWGWantGrp
        {
            get; set;
        }
        public string RnWGWantGrp { get; set; }
	}
}
