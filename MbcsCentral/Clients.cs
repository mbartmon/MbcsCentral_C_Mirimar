using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;

namespace MbcsCentral
{
    public class Clients : clsDB, INotifyPropertyChanged
    {

	    public event PropertyChangedEventHandler PropertyChanged;


	    static string TABLE = "Clients";
	    public const string SQL_COL_CLIENT_ID = "clientId";
	    public const string SQL_COL_CLIENT_NAME = "clientName";
	    public const string SQL_COL_CLIENT_ADDRESS1 = "clientAddress1";
	    public const string SQL_COL_CLIENT_ADDRESS2 = "clientAddress2";
	    public const string SQL_COL_CLIENT_CITY = "clientCity";
	    public const string SQL_COL_CLIENT_STATE = "clientState";
	    public const string SQL_COL_CLIENT_ZIP = "clientZip";
        public const string SQL_COL_CLIENT_EMAILS = "Emails";
        public const string SQL_COL_WINGL_ENABLED = "WinGLEnabled";

	    public const string SQL_COL_CLIENT_PHONE = "clientPhone";

	    static string SQL_LOAD = " SELECT " + 
                                 "[" + SQL_COL_CLIENT_ID + "]," + 
                                 "[" + SQL_COL_CLIENT_NAME + "]," + 
                                 "[" + SQL_COL_CLIENT_ADDRESS1 + "]," + 
                                 "[" + SQL_COL_CLIENT_ADDRESS2 + "]," + 
                                 "[" + SQL_COL_CLIENT_CITY + "]," + 
                                 "[" + SQL_COL_CLIENT_STATE + "]," + 
                                 "[" + SQL_COL_CLIENT_ZIP + "]," + 
                                 "[" + SQL_COL_CLIENT_PHONE + "]," + 
                                 "[" + SQL_COL_CLIENT_EMAILS + "]," +
                                 "[" + SQL_COL_WINGL_ENABLED + "]" +
                                 " FROM " + TABLE + 
                                 " WHERE " + SQL_COL_CLIENT_ID + " = ?;";

	    static string SQL_LOAD_ALL = " SELECT " + 
                                     "[" + SQL_COL_CLIENT_ID + "]," + 
                                     "[" + SQL_COL_CLIENT_NAME + "]," + 
                                     "[" + SQL_COL_CLIENT_ADDRESS1 + "]," + 
                                     "[" + SQL_COL_CLIENT_ADDRESS2 + "]," + 
                                     "[" + SQL_COL_CLIENT_CITY + "]," + 
                                     "[" + SQL_COL_CLIENT_STATE + "]," + 
                                     "[" + SQL_COL_CLIENT_ZIP + "]," + 
                                     "[" + SQL_COL_CLIENT_PHONE + "]," +
                                     "[" + SQL_COL_WINGL_ENABLED + "]" +
                                     " FROM " + TABLE + ";";

	    static string SQL_INSERT = " INSERT INTO " + TABLE + 
                                    " (" + "[" + SQL_COL_CLIENT_NAME + "]," + 
                                    "[" + SQL_COL_CLIENT_ADDRESS1 + "]," + 
                                    "[" + SQL_COL_CLIENT_ADDRESS2 + "]," + 
                                    "[" + SQL_COL_CLIENT_CITY + "]," + 
                                    "[" + SQL_COL_CLIENT_STATE + "]," + 
                                    "[" + SQL_COL_CLIENT_ZIP + "]," + 
                                    "[" + SQL_COL_CLIENT_PHONE + "]," + 
                                    "[" + SQL_COL_CLIENT_EMAILS + "]," +
                                    "[" + SQL_COL_WINGL_ENABLED + "]" +
                                    ") VALUES ('?', '?', '?', '?', '?', '?', '?', '?', ?);";

	    static string SQL_UPDATE = " UPDATE " + TABLE + 
                                    " SET " + 
                                    "[" + SQL_COL_CLIENT_NAME + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_ADDRESS1 + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_ADDRESS2 + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_CITY + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_STATE + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_ZIP + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_PHONE + "]='?'," + 
                                    "[" + SQL_COL_CLIENT_EMAILS + "]='?'," +
                                    "[" + SQL_COL_WINGL_ENABLED + "]= ?" +
                                    " WHERE " + SQL_COL_CLIENT_ID + " = ?;";
	    protected long m_clientId;
	    protected string m_clientName;
	    protected string m_clientAddress1;
	    protected string m_clientAddress2;
	    protected string m_clientCity;
	    protected string m_clientState;
	    protected string m_clientZip;
	    protected string m_clientPhone;
        protected string m_Emails;
        protected bool m_WinGlEnabled = false;

	    protected bool m_isChanged;
	    public Clients() : base(TABLE, SQL_COL_CLIENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		
	    }

	    public Clients(int clientId, string clientName, string clientAddress1, string clientAddress2, string clientCity, string clientState, string clientZip, string clientPhone, string emails, bool winglEnabled) 
            : base(TABLE, SQL_COL_CLIENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		
		    m_clientId = clientId;
		    m_clientName = clientName;
		    m_clientAddress1 = clientAddress1;
		    m_clientAddress2 = clientAddress2;
		    m_clientCity = clientCity;
		    m_clientState = clientState;
		    m_clientZip = clientZip;
		    m_clientPhone = clientPhone;
            m_Emails = emails;
            m_WinGlEnabled = winglEnabled;
	    }

	    protected Clients fill(DataRow dr)
	    {

		    try {
			    m_clientId = (long) dr[SQL_COL_CLIENT_ID];
			    m_clientName = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_NAME]) ? "" : (string)dr[SQL_COL_CLIENT_NAME];
			    m_clientAddress1 = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_ADDRESS1]) ? "" : (string) dr[SQL_COL_CLIENT_ADDRESS1];
			    m_clientAddress2 = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_ADDRESS2]) ? "" : (string)dr[SQL_COL_CLIENT_ADDRESS2];
			    m_clientCity = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_CITY]) ? "" : (string) dr[SQL_COL_CLIENT_CITY];
			    m_clientState = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_STATE]) ? "" : (string) dr[SQL_COL_CLIENT_STATE];
			    m_clientZip = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_ZIP]) ? "" : (string)dr[SQL_COL_CLIENT_ZIP];
			    m_clientPhone = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_PHONE]) ? "" : (string) dr[SQL_COL_CLIENT_PHONE];
                m_Emails = System.DBNull.Value.Equals(dr[SQL_COL_CLIENT_EMAILS]) ? "" : (string)dr[SQL_COL_CLIENT_EMAILS];
                m_WinGlEnabled = (bool)dr[SQL_COL_WINGL_ENABLED];
		    } catch (OleDbException ex) {
		    } catch (System.Exception exg) {
		    }
		    return this;

	    }

	    public static Clients load(long m_clientId)
	    {

		    Clients obj = new Clients();
		    string[] opt = {m_clientId.ToString() };
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                string s = clsDB.prepareStatement(SQL_LOAD, opt);
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not find Client Record <" + m_clientId.ToString() + "> QueryString = " + s, false);
                return null;
            }
		    DataRow dr = ds.Tables[0].Rows[0];

		    return obj.fill(dr);

	    }

	    public static Clients[] load()
	    {

		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Clients obj = new Clients();

		    ds = obj.loadAll();
		    obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
			    obj = new Clients();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Clients[])list.ToArray(typeof(Clients));
		    } catch (OleDbException ex) {
			    return null;
		    }
                                                                        
	    }

	    	/// <summary>
	/// Save or Update a record
	/// </summary>
	/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	    {

		    if (m_clientId > 0) {
			    string[] values = {
				    m_clientName,
				    m_clientAddress1,
				    m_clientAddress2,
				    m_clientCity,
				    m_clientState,
				    m_clientZip,
				    m_clientPhone,
                    m_Emails,
				    m_clientId.ToString(),
                    m_WinGlEnabled.ToString()
			    };
			    if (this.updateRecord(m_clientId.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {
				    m_clientName,
				    m_clientAddress1,
				    m_clientAddress2,
				    m_clientCity,
				    m_clientState,
				    m_clientZip,
				    m_clientPhone,
                    m_Emails, 
                    m_WinGlEnabled.ToString()
			    };
			    m_clientId = this.insertRecord(values);
			    if (m_clientId >= 0) {
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
			    cmd.CommandText = "CREATE TABLE Clients (clientId AUTONUMBER,clientName TEXT(50),clientAddress1 TEXT(50),clientAddress2 TEXT(50),clientCity TEXT(50),clientState TEXT(2),clientZip TEXT(10),clientPhone TEXT(50))";
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
	    public bool isChanged {
		    get { return m_isChanged; }
		    set { m_isChanged = value; }
	    }
	    public long clientId {
		    get { return m_clientId; }
	    }

	    public string clientName {
		    get { return m_clientName; }
		    set {
			    m_clientName = value;
			    OnPropertyChanged("clientName");
		    }
	    }

	    public string clientAddress1 {
		    get { return m_clientAddress1; }
		    set {
			    m_clientAddress1 = value;
			    OnPropertyChanged("clientAddress1");
		    }
	    }

	    public string clientAddress2 {
		    get { return m_clientAddress2; }
		    set {
			    m_clientAddress2 = value;
			    OnPropertyChanged("clientAddress2");
		    }
	    }

	    public string clientCity {
		    get { return m_clientCity; }
		    set {
			    m_clientCity = value;
			    OnPropertyChanged("clientCity");
		    }
	    }

	    public string clientState {
		    get { return m_clientState; }
		    set {
			    m_clientState = value;
			    OnPropertyChanged("clientState");
		    }
	    }

	    public string clientZip {
		    get { return m_clientZip; }
		    set {
			    m_clientZip = value;
			    OnPropertyChanged("clientZip");
		    }
	    }

	    public string clientPhone {
		    get { return m_clientPhone; }
		    set {
			    m_clientPhone = value;
			    OnPropertyChanged("clientPhone");
		    }
	    }

        public string Emails
        {
            get { return m_Emails; }
            set { m_Emails = value;
                OnPropertyChanged(Emails);
            }
        }

        public bool WinGlEnabled
        {
            get { return m_WinGlEnabled; }
            set { m_WinGlEnabled = value;
                OnPropertyChanged("WinGlEnabled");
            }
        }

        public ArrayList EmailsAsList
        {
            get
            {
                if (Emails != null && !Emails.Trim().Equals(""))
                {
                    string[] s = Emails.Split('|');
                    if (s.Length > 0)
                    {
                        string[] sp;
                        ArrayList list = new ArrayList();
                        foreach (string st in s)
                        {
                            sp = st.Split(',');
                            if (sp.Length > 1)
                                list.Add(new ClientEmails(sp[0], sp[1]));
                        }
                        if (list.Count > 0)
                            return list;
                    }
                }
                return null;
            }
        }

        public ArrayList EmailsFromList
        {
            set
            {
                if (value == null || value.Count == 0)
                    return;
                Emails = "";
                string separator = "";
                foreach (ClientEmails email in value)
                {
                    Emails += separator + email.MailTag + "," + email.EmailAddress;
                    separator = "|";
                }
            }
        }
    }
}