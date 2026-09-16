using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class notes : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_notes";

	public const string SQL_COL_TIMESTAMP = "timestamp";
	public const string SQL_COL_NOTE_ID = "note_id";
	public const string SQL_COL_ISSUE_ID = "issue_id";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_NOTE = "note";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_NOTE_ID + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_NOTE_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_NOTE_ID + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_TIMESTAMP + "]," +
			"[" + SQL_COL_ISSUE_ID + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_NOTE + "]" +
			") VALUES ('?', ?, ?, ?, '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_TIMESTAMP + "]='?'," +
			"[" + SQL_COL_ISSUE_ID + "]=?," +
			"[" + SQL_COL_ACCOUNT_ID + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_NOTE + "]='?'" +
			" WHERE " + "[" + SQL_COL_NOTE_ID + "] = ?;";

	protected DateTime m_timestamp;
	protected int m_note_id;
	protected int m_issue_id;
	protected int m_account_id;
	protected TimeStamp m_SSMA_TimeStamp;
	protected string m_note;
	protected bool m_isChanged;

	public notes() : base(TABLE, SQL_COL_NOTE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public notes(DateTime timestamp,int note_id,int issue_id,int account_id,TimeStamp SSMA_TimeStamp,string note) : 
			base(TABLE, SQL_COL_NOTE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_timestamp = timestamp;
		m_note_id = note_id;
		m_issue_id = issue_id;
		m_account_id = account_id;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_note = note;
	}

	protected notes fill(DataRow dr)
	{
		try {
			m_timestamp = (DateTime) (Convert.IsDBNull(dr[SQL_COL_TIMESTAMP]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_TIMESTAMP]);
			m_note_id = (int) dr[SQL_COL_NOTE_ID];
			m_issue_id = (int) dr[SQL_COL_ISSUE_ID];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_note = (string) (Convert.IsDBNull(dr[SQL_COL_NOTE]) ? "" : dr[SQL_COL_NOTE]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static notes load( int m_note_id)
	{
		notes obj  = new notes();
		string[] opt = {m_note_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static notes[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		notes obj = new notes();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new notes();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (notes[])list.ToArray(typeof(notes));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_note_id > 0 ) {
			string[] values = {m_timestamp.ToString(), m_issue_id.ToString(), m_account_id.ToString(), m_SSMA_TimeStamp.ToString(), m_note, m_note_id.ToString()};
			if (this.updateRecord(m_note_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_timestamp.ToString(), m_issue_id.ToString(), m_account_id.ToString(), m_SSMA_TimeStamp.ToString(), m_note};
			m_note_id = this.insertRecord(values);
			if (m_note_id >= 0) {
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
	public DateTime timestamp {
		set{ m_timestamp = value;
		OnPropertyChanged("timestamp"); }
		get { return m_timestamp; }
	}

	public int note_id {
		get { return m_note_id; }
	}

	public int issue_id {
		set{ m_issue_id = value;
		OnPropertyChanged("issue_id"); }
		get { return m_issue_id; }
	}

	public int account_id {
		set{ m_account_id = value;
		OnPropertyChanged("account_id"); }
		get { return m_account_id; }
	}

	public TimeStamp SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string note {
		set{ m_note = value;
		OnPropertyChanged("note"); }
		get { return m_note; }
	}

}
}
