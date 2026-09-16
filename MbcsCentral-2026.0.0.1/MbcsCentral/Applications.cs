using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MbcsCentral
{
	public class Applications : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "Applications";
		public const string SQL_COL_APPLICATION_I_D = "applicationID";
		public const string SQL_COL_APPLICATION_NAME = "applicationName";

		static string SQL_LOAD = " SELECT " + 
					  "[" + SQL_COL_APPLICATION_I_D + "]," + 
					  "[" + SQL_COL_APPLICATION_NAME + "]" + 
					  " FROM " + TABLE + 
					  " WHERE " + SQL_COL_APPLICATION_I_D + " = ?;";

		static string SQL_LOAD_ALL = " SELECT " + 
									 "[" + SQL_COL_APPLICATION_I_D + "]," + 
									 "[" + SQL_COL_APPLICATION_NAME + "]" + 
									 " FROM " + TABLE + ";";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
								   "[" + SQL_COL_APPLICATION_NAME + "]" + ") VALUES ('?');";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
								   " SET " + "[" + SQL_COL_APPLICATION_NAME + "]='?'" + 
								   " WHERE " + SQL_COL_APPLICATION_I_D + " = ?;";

		static string SQL_LOAD_BY_NAME = " SELECT " +
					  "[" + SQL_COL_APPLICATION_I_D + "]," +
					  "[" + SQL_COL_APPLICATION_NAME + "]" +
					  " FROM " + TABLE +
					  " WHERE " + SQL_COL_APPLICATION_NAME + " = '?';";

		protected long m_applicationID;
		protected string m_applicationName;

		protected bool m_isChanged;
		public Applications()
			: base(TABLE, SQL_COL_APPLICATION_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{

		}

		public Applications(int applicationID, string applicationName)
			: base(TABLE, SQL_COL_APPLICATION_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
		{

			m_applicationID = applicationID;
			m_applicationName = applicationName;
		}

		protected Applications fill(DataRow dr)
		{

			try
			{
				m_applicationID = (long)dr[SQL_COL_APPLICATION_I_D];
				m_applicationName = (string)(Information.IsDBNull(dr[SQL_COL_APPLICATION_NAME]) ? "" : dr[SQL_COL_APPLICATION_NAME]);
			}
			catch (OleDbException ex)
			{
			}
			catch (System.Exception exg)
			{
			}
			return this;

		}

		public static Applications load(long m_applicationID)
		{

			Applications obj = new Applications();
			string[] opt = { m_applicationID.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Applications loadByName(string name)
		{

			Applications obj = new Applications();
			string[] opt = { name };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_NAME, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Applications[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Applications obj = new Applications();

			ds = obj.loadAll();
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new Applications();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try
			{
				return (Applications[])list.ToArray(typeof(Applications));
			}
			catch (OleDbException ex)
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

			if (m_applicationID > 0)
			{
				string[] values = {
				m_applicationName,
				m_applicationID.ToString()
			};
				if (this.updateRecord(m_applicationID.ToString(), values))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				string[] values = { m_applicationName };
				m_applicationID = this.insertRecord(values);
				if (m_applicationID >= 0)
				{
					return true;
				}
			}
			return false;

		}


		public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try
			{
				CN.Open();
				System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Applications (applicationID AUTONUMBER,applicationName TEXT(50))";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			}
			catch (System.Data.OleDb.OleDbException ex)
			{
				return false;
			}
			finally
			{
				try
				{
					CN.Close();
				}
				catch
				{
				}
			}

			return true;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_isChanged = true;
		}

		// Property Routines
		public bool isChanged
		{
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}
		public long applicationID
		{
			get { return m_applicationID; }
		}

		public string applicationName
		{
			get { return m_applicationName; }
			set
			{
				m_applicationName = value;
				OnPropertyChanged("applicationName");
			}
		}

	}
}