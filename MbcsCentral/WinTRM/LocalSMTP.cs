using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinTrm {
public class LocalSMTP : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "tr_LocalSMTP";

	public const string SQL_COL_ACCOUNT_I_D = "accountID";
	public const string SQL_COL_INCOMING_PORT = "IncomingPort";
	public const string SQL_COL_OUTGOING_PORT = "OutgoingPort";
	public const string SQL_COL_AUTHENTICATION_RECEIVE = "AuthenticationReceive";
	public const string SQL_COL_AUTHENTICATION = "Authentication";
	public const string SQL_COL_USE_IN_S_S_L = "UseInSSL";
	public const string SQL_COL_USE_IN_T_S_L = "UseInTSL";
	public const string SQL_COL_USE_OUT_S_S_L = "UseOutSSL";
	public const string SQL_COL_USE_OUT_T_S_L = "UseOutTSL";
	public const string SQL_COL_S_S_M_A_TIME_STAMP = "SSMA_TimeStamp";
	public const string SQL_COL_USER_NAME = "UserName";
	public const string SQL_COL_PASSWORD = "Password";
	public const string SQL_COL_INCOMING_HOST = "IncomingHost";
	public const string SQL_COL_OUTGOING_HOST = "OutgoingHost";
	public const string SQL_COL_EMAIL_ADDRESS = "EmailAddress";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_ACCOUNT_I_D + "]," +
			"[" + SQL_COL_INCOMING_PORT + "]," +
			"[" + SQL_COL_OUTGOING_PORT + "]," +
			"[" + SQL_COL_AUTHENTICATION_RECEIVE + "]," +
			"[" + SQL_COL_AUTHENTICATION + "]," +
			"[" + SQL_COL_USE_IN_S_S_L + "]," +
			"[" + SQL_COL_USE_IN_T_S_L + "]," +
			"[" + SQL_COL_USE_OUT_S_S_L + "]," +
			"[" + SQL_COL_USE_OUT_T_S_L + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_USER_NAME + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_INCOMING_HOST + "]," +
			"[" + SQL_COL_OUTGOING_HOST + "]," +
			"[" + SQL_COL_EMAIL_ADDRESS + "]" +
			" FROM " + TABLE +
			" WHERE " + "[" + SQL_COL_ACCOUNT_I_D + "] = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_ACCOUNT_I_D + "]," +
			"[" + SQL_COL_INCOMING_PORT + "]," +
			"[" + SQL_COL_OUTGOING_PORT + "]," +
			"[" + SQL_COL_AUTHENTICATION_RECEIVE + "]," +
			"[" + SQL_COL_AUTHENTICATION + "]," +
			"[" + SQL_COL_USE_IN_S_S_L + "]," +
			"[" + SQL_COL_USE_IN_T_S_L + "]," +
			"[" + SQL_COL_USE_OUT_S_S_L + "]," +
			"[" + SQL_COL_USE_OUT_T_S_L + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_USER_NAME + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_INCOMING_HOST + "]," +
			"[" + SQL_COL_OUTGOING_HOST + "]," +
			"[" + SQL_COL_EMAIL_ADDRESS + "]" +
			" FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_INCOMING_PORT + "]," +
			"[" + SQL_COL_OUTGOING_PORT + "]," +
			"[" + SQL_COL_AUTHENTICATION_RECEIVE + "]," +
			"[" + SQL_COL_AUTHENTICATION + "]," +
			"[" + SQL_COL_USE_IN_S_S_L + "]," +
			"[" + SQL_COL_USE_IN_T_S_L + "]," +
			"[" + SQL_COL_USE_OUT_S_S_L + "]," +
			"[" + SQL_COL_USE_OUT_T_S_L + "]," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]," +
			"[" + SQL_COL_USER_NAME + "]," +
			"[" + SQL_COL_PASSWORD + "]," +
			"[" + SQL_COL_INCOMING_HOST + "]," +
			"[" + SQL_COL_OUTGOING_HOST + "]," +
			"[" + SQL_COL_EMAIL_ADDRESS + "]" +
			") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?');";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_INCOMING_PORT + "]=?," +
			"[" + SQL_COL_OUTGOING_PORT + "]=?," +
			"[" + SQL_COL_AUTHENTICATION_RECEIVE + "]=?," +
			"[" + SQL_COL_AUTHENTICATION + "]=?," +
			"[" + SQL_COL_USE_IN_S_S_L + "]=?," +
			"[" + SQL_COL_USE_IN_T_S_L + "]=?," +
			"[" + SQL_COL_USE_OUT_S_S_L + "]=?," +
			"[" + SQL_COL_USE_OUT_T_S_L + "]=?," +
			"[" + SQL_COL_S_S_M_A_TIME_STAMP + "]=?," +
			"[" + SQL_COL_USER_NAME + "]='?'," +
			"[" + SQL_COL_PASSWORD + "]='?'," +
			"[" + SQL_COL_INCOMING_HOST + "]='?'," +
			"[" + SQL_COL_OUTGOING_HOST + "]='?'," +
			"[" + SQL_COL_EMAIL_ADDRESS + "]='?'" +
			" WHERE " + "[" + SQL_COL_ACCOUNT_I_D + "] = ?;";

	protected int m_accountID;
	protected int m_IncomingPort;
	protected int m_OutgoingPort;
	protected int m_AuthenticationReceive;
	protected int m_Authentication;
	protected bool m_UseInSSL;
	protected bool m_UseInTSL;
	protected bool m_UseOutSSL;
	protected bool m_UseOutTSL;
	protected TimeStamp m_SSMA_TimeStamp;
	protected string m_UserName;
	protected string m_Password;
	protected string m_IncomingHost;
	protected string m_OutgoingHost;
	protected string m_EmailAddress;
	protected bool m_isChanged;

	public LocalSMTP() : base(TABLE, SQL_COL_ACCOUNT_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public LocalSMTP(int accountID,int IncomingPort,int OutgoingPort,int AuthenticationReceive,int Authentication,bool UseInSSL,bool UseInTSL,bool UseOutSSL,bool UseOutTSL,TimeStamp SSMA_TimeStamp,string UserName,string Password,string IncomingHost,string OutgoingHost,string EmailAddress) : 
			base(TABLE, SQL_COL_ACCOUNT_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_accountID = accountID;
		m_IncomingPort = IncomingPort;
		m_OutgoingPort = OutgoingPort;
		m_AuthenticationReceive = AuthenticationReceive;
		m_Authentication = Authentication;
		m_UseInSSL = UseInSSL;
		m_UseInTSL = UseInTSL;
		m_UseOutSSL = UseOutSSL;
		m_UseOutTSL = UseOutTSL;
		m_SSMA_TimeStamp = SSMA_TimeStamp;
		m_UserName = UserName;
		m_Password = Password;
		m_IncomingHost = IncomingHost;
		m_OutgoingHost = OutgoingHost;
		m_EmailAddress = EmailAddress;
	}

	protected LocalSMTP fill(DataRow dr)
	{
		try {
			m_accountID = (int) dr[SQL_COL_ACCOUNT_I_D];
			m_IncomingPort = (int) dr[SQL_COL_INCOMING_PORT];
			m_OutgoingPort = (int) dr[SQL_COL_OUTGOING_PORT];
			m_AuthenticationReceive = (int) dr[SQL_COL_AUTHENTICATION_RECEIVE];
			m_Authentication = (int) dr[SQL_COL_AUTHENTICATION];
			m_UseInSSL = (bool) dr[SQL_COL_USE_IN_S_S_L];
			m_UseInTSL = (bool) dr[SQL_COL_USE_IN_T_S_L];
			m_UseOutSSL = (bool) dr[SQL_COL_USE_OUT_S_S_L];
			m_UseOutTSL = (bool) dr[SQL_COL_USE_OUT_T_S_L];
			m_SSMA_TimeStamp = (TimeStamp) dr[SQL_COL_S_S_M_A_TIME_STAMP];
			m_UserName = (string) (Convert.IsDBNull(dr[SQL_COL_USER_NAME]) ? "" : dr[SQL_COL_USER_NAME]);
			m_Password = (string) (Convert.IsDBNull(dr[SQL_COL_PASSWORD]) ? "" : dr[SQL_COL_PASSWORD]);
			m_IncomingHost = (string) (Convert.IsDBNull(dr[SQL_COL_INCOMING_HOST]) ? "" : dr[SQL_COL_INCOMING_HOST]);
			m_OutgoingHost = (string) (Convert.IsDBNull(dr[SQL_COL_OUTGOING_HOST]) ? "" : dr[SQL_COL_OUTGOING_HOST]);
			m_EmailAddress = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL_ADDRESS]) ? "" : dr[SQL_COL_EMAIL_ADDRESS]);
		} catch (SqlException ex) {
		}
		return this;

	}

	public static LocalSMTP load( int m_accountID)
	{
		LocalSMTP obj  = new LocalSMTP();
		string[] opt = {m_accountID.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static LocalSMTP[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		LocalSMTP obj = new LocalSMTP();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new LocalSMTP();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (LocalSMTP[])list.ToArray(typeof(LocalSMTP));
		} catch (SqlException ex) {
			return null;
		}

	}

	public bool save()
	{
		if (m_accountID > 0 ) {
			string[] values = {m_IncomingPort.ToString(), m_OutgoingPort.ToString(), m_AuthenticationReceive.ToString(), m_Authentication.ToString(), m_UseInSSL.ToString(), m_UseInTSL.ToString(), m_UseOutSSL.ToString(), m_UseOutTSL.ToString(), m_SSMA_TimeStamp.ToString(), m_UserName, m_Password, m_IncomingHost, m_OutgoingHost, m_EmailAddress, m_accountID.ToString()};
			if (this.updateRecord(m_accountID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_IncomingPort.ToString(), m_OutgoingPort.ToString(), m_AuthenticationReceive.ToString(), m_Authentication.ToString(), m_UseInSSL.ToString(), m_UseInTSL.ToString(), m_UseOutSSL.ToString(), m_UseOutTSL.ToString(), m_SSMA_TimeStamp.ToString(), m_UserName, m_Password, m_IncomingHost, m_OutgoingHost, m_EmailAddress};
			m_accountID = this.insertRecord(values);
			if (m_accountID >= 0) {
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
	public int accountID {
		get { return m_accountID; }
	}

	public int IncomingPort {
		set{ m_IncomingPort = value;
		OnPropertyChanged("IncomingPort"); }
		get { return m_IncomingPort; }
	}

	public int OutgoingPort {
		set{ m_OutgoingPort = value;
		OnPropertyChanged("OutgoingPort"); }
		get { return m_OutgoingPort; }
	}

	public int AuthenticationReceive {
		set{ m_AuthenticationReceive = value;
		OnPropertyChanged("AuthenticationReceive"); }
		get { return m_AuthenticationReceive; }
	}

	public int Authentication {
		set{ m_Authentication = value;
		OnPropertyChanged("Authentication"); }
		get { return m_Authentication; }
	}

	public bool UseInSSL {
		set{ m_UseInSSL = value;
		OnPropertyChanged("UseInSSL"); }
		get { return m_UseInSSL; }
	}

	public bool UseInTSL {
		set{ m_UseInTSL = value;
		OnPropertyChanged("UseInTSL"); }
		get { return m_UseInTSL; }
	}

	public bool UseOutSSL {
		set{ m_UseOutSSL = value;
		OnPropertyChanged("UseOutSSL"); }
		get { return m_UseOutSSL; }
	}

	public bool UseOutTSL {
		set{ m_UseOutTSL = value;
		OnPropertyChanged("UseOutTSL"); }
		get { return m_UseOutTSL; }
	}

	public TimeStamp SSMA_TimeStamp {
		set{ m_SSMA_TimeStamp = value;
		OnPropertyChanged("SSMA_TimeStamp"); }
		get { return m_SSMA_TimeStamp; }
	}

	public string UserName {
		set{ m_UserName = value;
		OnPropertyChanged("UserName"); }
		get { return m_UserName; }
	}

	public string Password {
		set{ m_Password = value;
		OnPropertyChanged("Password"); }
		get { return m_Password; }
	}

	public string IncomingHost {
		set{ m_IncomingHost = value;
		OnPropertyChanged("IncomingHost"); }
		get { return m_IncomingHost; }
	}

	public string OutgoingHost {
		set{ m_OutgoingHost = value;
		OnPropertyChanged("OutgoingHost"); }
		get { return m_OutgoingHost; }
	}

	public string EmailAddress {
		set{ m_EmailAddress = value;
		OnPropertyChanged("EmailAddress"); }
		get { return m_EmailAddress; }
	}

}
}
