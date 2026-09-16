using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using MbcsCentral;

namespace MbcsCentral.WinDoc
{
	public class DocInterface : clsDB, INotifyPropertyChanged
	{
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "do_interface";

	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_DESC = "desc";
	    public const string SQL_COL_PROG_ID = "prog_id";
	    public const string SQL_COL_APP_ID = "app_id";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_DESC + "]," +
				"[" + SQL_COL_PROG_ID + "]," +
				"[" + SQL_COL_APP_ID + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_DESC + "]," +
				"[" + SQL_COL_PROG_ID + "]," +
				"[" + SQL_COL_APP_ID + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_DESC + "]," +
				"[" + SQL_COL_PROG_ID + "]," +
				"[" + SQL_COL_APP_ID + "]" +
				") VALUES ('?', '?', '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_DESC + "]='?'," +
				"[" + SQL_COL_PROG_ID + "]='?'," +
				"[" + SQL_COL_APP_ID + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		protected int m_ID;
		protected string m_desc;
		protected string m_prog_id;
		protected string m_app_id;
		protected bool m_isChanged;

        public DocInterface() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{

        }

        protected DocInterface fill(DataRow dr)
		{
			try 
            {
				m_ID = (int) dr[SQL_COL_I_D];
				m_desc = (string) (Convert.IsDBNull(dr[SQL_COL_DESC]) ? "" : dr[SQL_COL_DESC]);
				m_prog_id = (string) (Convert.IsDBNull(dr[SQL_COL_PROG_ID]) ? "" : dr[SQL_COL_PROG_ID]);
				m_app_id = (string) (Convert.IsDBNull(dr[SQL_COL_APP_ID]) ? "" : dr[SQL_COL_APP_ID]);
			}
            catch (SqlException ex)
            {
			}
			return this;

		}

        public static DocInterface load( int m_ID)
		{
            DocInterface obj = new DocInterface();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) 
            {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static DocInterface[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
            DocInterface obj = new DocInterface();

			ds = obj.loadAll();
		    if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) 
            {
				obj = new DocInterface();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try 
            {
				return (DocInterface[])list.ToArray(typeof(DocInterface));
			} 
            catch (SqlException ex) 
            {
				return null;
			}

		}

		public bool save()
		{
			if (m_ID > 0 )
            {
				string[] values = {m_desc, m_prog_id, m_app_id, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				}
                else
                {
					return false;
				}
			}
            else
            {
				string[] values = {m_desc, m_prog_id, m_app_id};
				m_ID = this.insertRecord(values);
				if (m_ID >= 0)
                {
					return true;
				}
			}
			return false;

		}

		private void OnPropertyChanged(string info)
        {
			if (PropertyChanged != null)
            {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				m_isChanged = true;
			}
		}

		// Properties
		public int ID
        {
			get { return m_ID; }
		}

		public string desc
        {
			set
            {   m_desc = value;
			    OnPropertyChanged("desc");
            }
			get { return m_desc; }
		}

		public string prog_id {
			set{
                    m_prog_id = value;
			        OnPropertyChanged("prog_id");
            }
			get { return m_prog_id; }
		}

		public string app_id
        {
			set
            {
                m_app_id = value;
			    OnPropertyChanged("app_id");
            }
			get { return m_app_id; }
		}

    }
}
