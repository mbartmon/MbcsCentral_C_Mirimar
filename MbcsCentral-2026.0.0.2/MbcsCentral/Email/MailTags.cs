using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.Email {
	public class MailTags : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

		public static string TABLE = "MailTags";

		public const string SQL_COL_I_D = "ID";
		public const string SQL_COL_MAIL_TAG = "MailTag";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_MAIL_TAG + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_MAIL_TAG + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_MAIL_TAG + "]" +
				") VALUES ('?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_MAIL_TAG + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_BY_TAG = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_MAIL_TAG + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_MAIL_TAG + "] = '?';";

		protected int m_ID;
		protected string m_MailTag;
	    protected bool m_isChanged;

	    public MailTags() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

	    public MailTags(int ID,string MailTag) : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_ID = ID;
			m_MailTag = MailTag;
	    }

		protected MailTags fill(DataRow dr)
	    {
			try {
				m_ID = (int) dr[SQL_COL_I_D];
				m_MailTag = (string) (Convert.IsDBNull(dr[SQL_COL_MAIL_TAG]) ? "" : dr[SQL_COL_MAIL_TAG]);
			} catch (SqlException ex) {
			}
			return this;

	    }

	    public static MailTags load( int m_ID)
		{
			MailTags obj  = new MailTags();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
		    	DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

		}

		public static MailTags[] load()
	    {
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			MailTags obj = new MailTags();

			ds = obj.loadAll();
		    if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new MailTags();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (MailTags[])list.ToArray(typeof(MailTags));
			} catch (SqlException ex) {
				return null;
			}

	    }

		public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_MailTag, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_MailTag};
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
		public int ID {
			get { return m_ID; }
		}

		public string MailTag {
			set{ m_MailTag = value;
		    OnPropertyChanged("MailTag"); }
			get { return m_MailTag; }
		}

    }
}
