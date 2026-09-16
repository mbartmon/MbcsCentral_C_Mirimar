using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class contents : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_contents";

	public const string SQL_COL_CONTENT_ID = "content_id";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_CONTENT_TYPE = "content_type";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_DESCRIPTION = "description";
	public const string SQL_COL_CONTENT = "content";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_TYPE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_CONTENT + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_CONTENT_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_CONTENT_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_TYPE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_CONTENT + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_CONTENT_TYPE + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_CONTENT + "]" +
			") VALUES (?, ?, ?, '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_CONTENT_TYPE + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_DESCRIPTION + "]='?'," +
			"[" + SQL_COL_CONTENT + "]='?'" +
			" WHERE " + "[" + SQL_COL_CONTENT_ID + "] = ?;";

	protected int m_content_id;
	protected int m_account_id;
	protected int m_content_type;
	protected TimeStamp m_SSMA_TimeStamp;
	protected string m_description;
	protected string m_content;
	protected bool m_isChanged;

	public contents() : base(TABLE, SQL_COL_CONTENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public contents(int content_id,int account_id,int content_type,TimeStamp SSMA_TimeStamp,string description,string content) : 
			base(TABLE, SQL_COL_CONTENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_content_id = content_id;
		m_account_id = account_id;
		m_content_type = content_type;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_description = description;
		m_content = content;
	}

	protected contents fill(DataRow dr)
	{
		try {
			m_content_id = (int) dr[SQL_COL_CONTENT_ID];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_content_type = (int) dr[SQL_COL_CONTENT_TYPE];
			m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			m_content = (string) (Convert.IsDBNull(dr[SQL_COL_CONTENT]) ? "" : dr[SQL_COL_CONTENT]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static contents load( int m_content_id)
	{
		contents obj  = new contents();
		string[] opt = {m_content_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static contents[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		contents obj = new contents();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new contents();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (contents[])list.ToArray(typeof(contents));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_content_id > 0 ) {
			string[] values = {m_account_id.ToString(), m_content_type.ToString(), m_SSMA_TimeStamp.ToString(), m_description, m_content, m_content_id.ToString()};
			if (this.updateRecord(m_content_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_account_id.ToString(), m_content_type.ToString(), m_SSMA_TimeStamp.ToString(), m_description, m_content};
			m_content_id = this.insertRecord(values);
			if (m_content_id >= 0) {
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
	public int content_id {
		get { return m_content_id; }
	}

	public int account_id {
		set{ m_account_id = value;
		OnPropertyChanged("account_id"); }
		get { return m_account_id; }
	}

	public int content_type {
		set{ m_content_type = value;
		OnPropertyChanged("content_type"); }
		get { return m_content_type; }
	}

	public TimeStamp SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string description {
		set{ m_description = value;
		OnPropertyChanged("description"); }
		get { return m_description; }
	}

	public string content {
		set{ m_content = value;
		OnPropertyChanged("content"); }
		get { return m_content; }
	}

}
}
