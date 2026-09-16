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

	public class Occupants : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		public static string TABLE = "pm_Occupants";
		public const string SQL_COL_OCCUPANT_ID = "occupantId";
		public const string SQL_COL_I_D = "ID";
		public const string SQL_COL_NAME = "Name";
		public const string SQL_COL_RELATIONSHIP = "Relationship";
        public const string SQL_COL_CLIENTID = "clientId";

		public const string SQL_COL_D_O_B = "DOB";

		static string SQL_LOAD = " SELECT " + 
                                 "[" + SQL_COL_OCCUPANT_ID + "]," + 
                                 "[" + SQL_COL_I_D + "]," + 
                                 "[" + SQL_COL_NAME + "]," + 
                                 "[" + SQL_COL_RELATIONSHIP + "]," + 
                                 "[" + SQL_COL_D_O_B + "]" +
                                 " FROM " + TABLE + 
                                 " WHERE " + SQL_COL_OCCUPANT_ID + " = ?";

		static string SQL_LOAD_ALL = " SELECT " + 
                                    "[" + SQL_COL_OCCUPANT_ID + "]," + 
                                    "[" + SQL_COL_I_D + "]," + 
                                    "[" + SQL_COL_NAME + "]," + 
                                    "[" + SQL_COL_RELATIONSHIP + "]," + 
                                    "[" + SQL_COL_D_O_B + "]" + 
                                    " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + 
                                    " (" + "[" + SQL_COL_I_D + "]," + 
                                    "[" + SQL_COL_NAME + "]," + 
                                    "[" + SQL_COL_RELATIONSHIP + "]," + 
                                    "[" + SQL_COL_D_O_B + "]," + 
                                    "[" + SQL_COL_CLIENTID + "]" +
                                    ") VALUES (?, '?', '?', '?', ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
                                    " SET " + "[" + SQL_COL_I_D + "]=?," + 
                                    "[" + SQL_COL_NAME + "]='?'," + 
                                    "[" + SQL_COL_RELATIONSHIP + "]='?'," + 
                                    "[" + SQL_COL_D_O_B + "]='?'" + 
                                    " WHERE " + SQL_COL_OCCUPANT_ID + " = ?";

		static string SQL_LOAD_BY_SEQUENCE_NUMBER = " SELECT " +    
                                                    "[" + SQL_COL_OCCUPANT_ID + "]," + 
                                                    "[" + SQL_COL_I_D + "]," + 
                                                    "[" + SQL_COL_NAME + "]," + 
                                                    "[" + SQL_COL_RELATIONSHIP + "]," + 
                                                    "[" + SQL_COL_D_O_B + "]" + 
                                                    " FROM " + TABLE + 
                                                    " WHERE " + SQL_COL_I_D + " = ?";

		protected int m_occupantId;
		protected int m_ID;
		protected string m_Name;
		protected string m_Relationship;
		protected DateTime m_DOB;
        protected int m_ClientId = 1;

		protected bool m_isChanged;
		public Occupants() : base(TABLE, SQL_COL_OCCUPANT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Occupants(int ID, string Name, string Relationship, DateTime DOB) : base(TABLE, SQL_COL_OCCUPANT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_ID = ID;
			m_Name = Name;
			m_Relationship = Relationship;
			m_DOB = DOB;
		}

		protected Occupants fill(DataRow dr)
		{

			try {
				m_occupantId = (int)dr[SQL_COL_OCCUPANT_ID];
				m_ID = (int)dr[SQL_COL_I_D];
				m_Name = (string)(Information.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
				m_Relationship = (string)(Information.IsDBNull(dr[SQL_COL_RELATIONSHIP]) ? "" : dr[SQL_COL_RELATIONSHIP]);
				m_DOB = (DateTime)dr[SQL_COL_D_O_B];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Occupants load(int id)
		{

			Occupants obj = new Occupants();
            string[] opt = { id.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Occupants[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Occupants obj = new Occupants();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Occupants();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Occupants[])list.ToArray(typeof(Occupants));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Occupants[] loadBySequenceNumber(int id)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Occupants obj = new Occupants();
			string[] opt = { id .ToString()};

			ds = obj.load(SQL_LOAD_BY_SEQUENCE_NUMBER, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Occupants();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Occupants[])list.ToArray(typeof(Occupants));
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

			if (m_occupantId > 0) {
				string[] values = {
					m_ID.ToString(),
					m_Name.ToString(),
					m_Relationship.ToString(),
					m_DOB.ToString(),
					m_occupantId.ToString()
				};
                if (this.updateRecord(m_occupantId.ToString(), values))
                {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_ID.ToString(),
					m_Name.ToString(),
					m_Relationship,
					m_DOB.ToString(),
                    m_ClientId.ToString()
				};
				m_occupantId = this.insertRecord(values);
				if (m_occupantId >= 0) {
					return true;
				}
			}
			return false;

		}

		public bool delete()
		{

			if (m_occupantId > 0) {
                return this.delete(m_occupantId.ToString());
			}
			return false;

		}


		public static bool CreateTable(SqlConnection CN)
		{

			try {
				CN.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Occupants (ID LONG,Name TEXT(30),Relationship TEXT(16),DOB DATETIME)";
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
		public int occupantId {
			get { return m_occupantId; }
			set {
				m_occupantId = value;
				OnPropertyChanged("occupantId");
			}
		}
		public int ID {
			get { return m_ID; }
			set {
				m_ID = value;
				OnPropertyChanged("ID");
			}
		}

		public string Name {
			get { return m_Name; }
			set {
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}

		public string Relationship {
			get { return m_Relationship; }
			set {
				m_Relationship = value;
				OnPropertyChanged("Relationship");
			}
		}

		public DateTime DOB {
			get { return m_DOB; }
			set {
				m_DOB = value;
				OnPropertyChanged("DOB");
			}
		}

	}
}
