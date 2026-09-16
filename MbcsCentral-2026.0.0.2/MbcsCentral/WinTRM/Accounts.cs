using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class Accounts : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_Accounts";

	public const string SQL_COL_ACCOUNT_TYPE = "account_type";
	public const string SQL_COL_PERMISSIONS = "permissions";
	public const string SQL_COL_PORT = "port";
	public const string SQL_COL_ACCOUNT_ID = "account_id";
	public const string SQL_COL_PARENT_ACCOUNT = "parent_account";
	public const string SQL_COL_USE_OUTLOOK = "use_outlook";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_LOGIN = "login";
	public const string SQL_COL_PASSWORD = "password";
	public const string SQL_COL_NAME = "name";
	public const string SQL_COL_EMAIL = "email";
	public const string SQL_COL_SMS = "sms";
	public const string SQL_COL_IP = "ip";
	public const string SQL_COL_OUTLOOK_FOLDER = "outlook_folder";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_ACCOUNT_TYPE + "]," +
			"[" + SQL_COL_PERMISSIONS + "]," +
			"[" + SQL_COL_PORT + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_PARENT_ACCOUNT + "]," +
			"[" + SQL_COL_USE_OUTLOOK + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_LOGIN + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_NAME + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_SMS + "]," +
			"[" + SQL_COL_IP + "]," +
			"[" + SQL_COL_OUTLOOK_FOLDER + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_ACCOUNT_ID + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_ACCOUNT_TYPE + "]," +
			"[" + SQL_COL_PERMISSIONS + "]," +
			"[" + SQL_COL_PORT + "]," +
			"[" + SQL_COL_ACCOUNT_ID + "]," +
			"[" + SQL_COL_PARENT_ACCOUNT + "]," +
			"[" + SQL_COL_USE_OUTLOOK + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_LOGIN + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_NAME + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_SMS + "]," +
			"[" + SQL_COL_IP + "]," +
			"[" + SQL_COL_OUTLOOK_FOLDER + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_ACCOUNT_TYPE + "]," +
			"[" + SQL_COL_PERMISSIONS + "]," +
			"[" + SQL_COL_PORT + "]," +
			"[" + SQL_COL_PARENT_ACCOUNT + "]," +
			"[" + SQL_COL_USE_OUTLOOK + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_LOGIN + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_NAME + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_SMS + "]," +
			"[" + SQL_COL_IP + "]," +
			"[" + SQL_COL_OUTLOOK_FOLDER + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_ACCOUNT_TYPE + "]=?," +
			"[" + SQL_COL_PERMISSIONS + "]=?," +
			"[" + SQL_COL_PORT + "]=?," +
			"[" + SQL_COL_PARENT_ACCOUNT + "]=?," +
			"[" + SQL_COL_USE_OUTLOOK + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_LOGIN + "]='?'," +
			"[" + SQL_COL_PASSWORD + "]='?'," +
			"[" + SQL_COL_NAME + "]='?'," +
			"[" + SQL_COL_EMAIL + "]='?'," +
			"[" + SQL_COL_SMS + "]='?'," +
			"[" + SQL_COL_IP + "]='?'," +
			"[" + SQL_COL_OUTLOOK_FOLDER + "]='?'" +
			" WHERE " + "[" + SQL_COL_ACCOUNT_ID + "] = ?;";

	protected short m_account_type;
	protected short m_permissions;
	protected short m_port;
	protected int m_account_id;
	protected int m_parent_account;
	protected bool m_use_outlook;
    protected DateTime m_SSMA_TimeStamp;
    protected string m_login;
	protected string m_password;
	protected string m_name;
	protected string m_email;
	protected string m_sms;
	protected string m_ip;
	protected string m_outlook_folder;
	protected bool m_isChanged;

	public Accounts() : base(TABLE, SQL_COL_ACCOUNT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
	{
	}

	public Accounts(short account_type,short permissions,short port,int account_id,int parent_account,bool use_outlook,string login,string password,string name,string email,string sms,string ip,string outlook_folder) : 
			base(TABLE, SQL_COL_ACCOUNT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
	{
		m_account_type = account_type;
		m_permissions = permissions;
		m_port = port;
		m_account_id = account_id;
		m_parent_account = parent_account;
		m_use_outlook = use_outlook;
		m_login = login;
		m_password = password;
		m_name = name;
		m_email = email;
		m_sms = sms;
		m_ip = ip;
		m_outlook_folder = outlook_folder;
	}

	protected Accounts fill(DataRow dr)
	{
		try {
			m_account_type = (short) dr[SQL_COL_ACCOUNT_TYPE];
			m_permissions = (short) dr[SQL_COL_PERMISSIONS];
			m_port = (short) dr[SQL_COL_PORT];
			m_account_id = (int) dr[SQL_COL_ACCOUNT_ID];
			m_parent_account = (int) dr[SQL_COL_PARENT_ACCOUNT];
			m_use_outlook = (bool) dr[SQL_COL_USE_OUTLOOK];
			m_SSMA_TimeStamp = (DateTime) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_login = (string) (Convert.IsDBNull(dr[SQL_COL_LOGIN]) ? "" : dr[SQL_COL_LOGIN]);
			m_password = (string) (Convert.IsDBNull(dr[SQL_COL_PASSWORD]) ? "" : dr[SQL_COL_PASSWORD]);
			m_name = (string) (Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
			m_email = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL]) ? "" : dr[SQL_COL_EMAIL]);
			m_sms = (string) (Convert.IsDBNull(dr[SQL_COL_SMS]) ? "" : dr[SQL_COL_SMS]);
			m_ip = (string) (Convert.IsDBNull(dr[SQL_COL_IP]) ? "" : dr[SQL_COL_IP]);
			m_outlook_folder = (string) (Convert.IsDBNull(dr[SQL_COL_OUTLOOK_FOLDER]) ? "" : dr[SQL_COL_OUTLOOK_FOLDER]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static Accounts load( int m_account_id)
	{
		Accounts obj  = new Accounts();
		string[] opt = {m_account_id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Accounts[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Accounts obj = new Accounts();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Accounts();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (Accounts[])list.ToArray(typeof(Accounts));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_account_id > 0 ) {
			string[] values = {m_account_type.ToString(), m_permissions.ToString(), m_port.ToString(), m_parent_account.ToString(), m_use_outlook.ToString(), m_SSMA_TimeStamp.ToString(), m_login, m_password, m_name, m_email, m_sms, m_ip, m_outlook_folder, m_account_id.ToString()};
			if (this.updateRecord(m_account_id.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_account_type.ToString(), m_permissions.ToString(), m_port.ToString(), m_parent_account.ToString(), m_use_outlook.ToString(), m_SSMA_TimeStamp.ToString(), m_login, m_password, m_name, m_email, m_sms, m_ip, m_outlook_folder};
			m_account_id = this.insertRecord(values);
			if (m_account_id >= 0) {
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
	public short account_type {
		set{ m_account_type = value;
		OnPropertyChanged("account_type"); }
		get { return m_account_type; }
	}

	public short permissions {
		set{ m_permissions = value;
		OnPropertyChanged("permissions"); }
		get { return m_permissions; }
	}

	public short port {
		set{ m_port = value;
		OnPropertyChanged("port"); }
		get { return m_port; }
	}

	public int account_id {
		get { return m_account_id; }
	}

	public int parent_account {
		set{ m_parent_account = value;
		OnPropertyChanged("parent_account"); }
		get { return m_parent_account; }
	}

	public bool use_outlook {
		set{ m_use_outlook = value;
		OnPropertyChanged("use_outlook"); }
		get { return m_use_outlook; }
	}

	public DateTime SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string login {
		set{ m_login = value;
		OnPropertyChanged("login"); }
		get { return m_login; }
	}

	public string password {
		set{ m_password = value;
		OnPropertyChanged("password"); }
		get { return m_password; }
	}

	public string name {
		set{ m_name = value;
		OnPropertyChanged("name"); }
		get { return m_name; }
	}

	public string email {
		set{ m_email = value;
		OnPropertyChanged("email"); }
		get { return m_email; }
	}

	public string sms {
		set{ m_sms = value;
		OnPropertyChanged("sms"); }
		get { return m_sms; }
	}

	public string ip {
		set{ m_ip = value;
		OnPropertyChanged("ip"); }
		get { return m_ip; }
	}

	public string outlook_folder {
		set{ m_outlook_folder = value;
		OnPropertyChanged("outlook_folder"); }
		get { return m_outlook_folder; }
	}

}
}
