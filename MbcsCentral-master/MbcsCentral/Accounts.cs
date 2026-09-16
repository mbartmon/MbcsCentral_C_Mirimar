using Microsoft.VisualBasic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


// $ID: Exp $
namespace MbcsCentral
{
    /// <summary>
	/// User Accounts for the application
	/// </summary>
	public class Accounts : clsDB  , INotifyPropertyChanged
    {
	
	    public const string TABLE = "Accounts";
	
	    public const string SQL_COL_ACCOUNT_ID = "accountId";
	    public const string SQL_COL_LOGIN = "login";
	    public const string SQL_COL_ACCOUNT_TYPE = "accountType"; // 0 = in-office, 1 = out-of-office
	    public const string SQL_COL_PERMISSIONS = "permissions";
	    public const string SQL_COL_PASSWORD = "password";
	    public const string SQL_COL_FIRST_NAME = "firstName";
	    public const string SQL_COL_LAST_NAME = "lastName";
	    public const string SQL_COL_PHONE = "phone";
	    public const string SQL_COL_MOBILE_PHONE = "mobilePhone";
	    public const string SQL_COL_EMAIL = "email";
	    public const string SQL_COL_SMS = "sms";
	    public const string SQL_COL_IP = "ip";
	    public const string SQL_COL_PORT = "port";
	    public const string SQL_COL_USE_OUTLOOK = "useOutlook";
	    public const string SQL_COL_OUTLOOK_FOLDERS = "outlookFolder";
	    public const string SQL_COL_PARENT = "parentAccount";
	    public const string SQL_COL_CLIENT_ID = "clientId";

	    static string SQL_LOAD = "SELECT " + 
                                SQL_COL_ACCOUNT_ID + ", " + 
                                SQL_COL_LOGIN + ", " + 
                                SQL_COL_ACCOUNT_TYPE + ", " + 
                                SQL_COL_PERMISSIONS + ", " + 
                                SQL_COL_PASSWORD + ", " + 
                                SQL_COL_FIRST_NAME + ", " + 
                                SQL_COL_LAST_NAME + ", " + 
                                SQL_COL_PHONE + ", " + 
                                SQL_COL_MOBILE_PHONE + ", " + 
                                SQL_COL_EMAIL + ", " + 
                                SQL_COL_SMS + ", " + 
                                SQL_COL_IP + ", " + 
                                SQL_COL_PORT + ", " + 
                                SQL_COL_USE_OUTLOOK + ", " + 
                                SQL_COL_OUTLOOK_FOLDERS + ", " + 
                                SQL_COL_PARENT + ", " + 
                                SQL_COL_CLIENT_ID + 
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_ACCOUNT_ID + "=?;";
	
	    static string SQL_LOAD_ALL = "SELECT " + 
                                    SQL_COL_ACCOUNT_ID + ", " + 
                                    SQL_COL_LOGIN + ", " + 
                                    SQL_COL_ACCOUNT_TYPE + ", " + 
                                    SQL_COL_PERMISSIONS + ", " + 
                                    SQL_COL_PASSWORD + ", " + 
                                    SQL_COL_FIRST_NAME + ", " + 
                                    SQL_COL_LAST_NAME + ", " + 
                                    SQL_COL_PHONE + ", " + 
                                    SQL_COL_MOBILE_PHONE + ", " + 
                                    SQL_COL_EMAIL + ", " + 
                                    SQL_COL_SMS + ", " + 
                                    SQL_COL_IP + ", " + 
                                    SQL_COL_PORT + ", " + 
                                    SQL_COL_USE_OUTLOOK + ", " + 
                                    SQL_COL_OUTLOOK_FOLDERS + ", " + 
                                    SQL_COL_PARENT + ", " + 
                                    SQL_COL_CLIENT_ID + 
                                    " FROM " + TABLE + ";";
	
	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" + 
                                    SQL_COL_LOGIN + ", " + 
                                    SQL_COL_ACCOUNT_TYPE + ", " + 
                                    SQL_COL_PERMISSIONS + ", [" + 
                                    SQL_COL_PASSWORD + "], " + 
                                    SQL_COL_FIRST_NAME + ", " + 
                                    SQL_COL_LAST_NAME + ", " + 
                                    SQL_COL_PHONE + ", " + 
                                    SQL_COL_MOBILE_PHONE + ", " + 
                                    SQL_COL_EMAIL + ", " + 
                                    SQL_COL_SMS + ", " + 
                                    SQL_COL_IP + ", " + 
                                    SQL_COL_PORT + ", " + 
                                    SQL_COL_USE_OUTLOOK + ", " + 
                                    SQL_COL_OUTLOOK_FOLDERS + ", " + 
                                    SQL_COL_PARENT + ", " + 
                                    SQL_COL_CLIENT_ID + 
                                    ") VALUES (\'?\', ?, ?, \'?\', \'?\', \'?\', \'?\', \'?\', \'?\', \'?\', \'?\', ?, ?, \'?\', ?, ?);";
	
	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " + 
                                    SQL_COL_LOGIN + "=\'?\', " + 
                                    SQL_COL_ACCOUNT_TYPE + "=?, " + 
                                    SQL_COL_PERMISSIONS + "=?, [" + 
                                    SQL_COL_PASSWORD + "]=\'?\', " + 
                                    SQL_COL_FIRST_NAME + "=\'?\', " + 
                                    SQL_COL_LAST_NAME + "=\'?\', " + 
                                    SQL_COL_PHONE + "=\'?\', " + 
                                    SQL_COL_MOBILE_PHONE + "=\'?\', " + 
                                    SQL_COL_EMAIL + "=\'?\', " + 
                                    SQL_COL_SMS + "=\'?\', " + 
                                    SQL_COL_IP + "=\'?\', " + 
                                    SQL_COL_PORT + "=?, " + 
                                    SQL_COL_USE_OUTLOOK + "=?, " + 
                                    SQL_COL_OUTLOOK_FOLDERS + "=\'?\', " + 
                                    SQL_COL_PARENT + "=?, " + 
                                    SQL_COL_CLIENT_ID + "=?" + 
                                    " WHERE " + SQL_COL_ACCOUNT_ID + "=?;";
	
	    static string SQL_LOAD_BY_LOGIN = "SELECT " + 
                                           SQL_COL_ACCOUNT_ID + ", " + 
                                           SQL_COL_LOGIN + ", " + 
                                           SQL_COL_ACCOUNT_TYPE + ", " + 
                                           SQL_COL_PERMISSIONS + ", " + 
                                           SQL_COL_PASSWORD + ", " + 
                                           SQL_COL_FIRST_NAME + ", " + 
                                           SQL_COL_LAST_NAME + ", " + 
                                           SQL_COL_PHONE + ", " + 
                                           SQL_COL_MOBILE_PHONE + ", " + 
                                           SQL_COL_EMAIL + ", " + 
                                           SQL_COL_SMS + ", " + 
                                           SQL_COL_IP + ", " + 
                                           SQL_COL_PORT + ", " + 
                                           SQL_COL_USE_OUTLOOK + ", " + 
                                           SQL_COL_OUTLOOK_FOLDERS + ", " + 
                                           SQL_COL_PARENT + ", " + 
                                           SQL_COL_CLIENT_ID + 
                                           " FROM " + TABLE + " WHERE " + SQL_COL_LOGIN + "=\'?\';";

        static string SQL_LOAD_BY_CLIENTID = "SELECT " +
                                       SQL_COL_ACCOUNT_ID + ", " +
                                       SQL_COL_LOGIN + ", " +
                                       SQL_COL_ACCOUNT_TYPE + ", " +
                                       SQL_COL_PERMISSIONS + ", " +
                                       SQL_COL_PASSWORD + ", " +
                                       SQL_COL_FIRST_NAME + ", " +
                                       SQL_COL_LAST_NAME + ", " +
                                       SQL_COL_PHONE + ", " +
                                       SQL_COL_MOBILE_PHONE + ", " +
                                       SQL_COL_EMAIL + ", " +
                                       SQL_COL_SMS + ", " +
                                       SQL_COL_IP + ", " +
                                       SQL_COL_PORT + ", " +
                                       SQL_COL_USE_OUTLOOK + ", " +
                                       SQL_COL_OUTLOOK_FOLDERS + ", " +
                                       SQL_COL_PARENT + ", " +
                                       SQL_COL_CLIENT_ID +
                                       " FROM " + TABLE + " WHERE " + SQL_COL_CLIENT_ID + "=?;";

	
	    /// <summary>
		/// Permission enumerations
		/// </summary>
        public enum AccountTypes
        {
	        ViewOnly = 0,
	        User = 1,
	        Manager = 2,
	        Administrator = 3
        };
	
	    // Fields
	    protected long m_accountId;
	    protected string m_login;
	    protected int m_accountType;
	    protected int m_permissions;
	    protected string m_password;
	    protected string m_firstName;
	    protected string m_lastName;
	    protected string m_phone;
	    protected string m_mobilePhone;
	    protected string m_email;
	    protected string m_sms;
	    protected string m_ip;
	    protected int m_port;
	    protected bool m_useOutlook;
	    protected string m_outlookFolders;
	    protected int m_parentAccount;
	    protected long m_clientId;
        protected bool m_isChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public Accounts() : base(TABLE, SQL_COL_ACCOUNT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }
	
	    public Accounts(string login, int accountType, int permissions, string password, string firstName, string lastName, string phone, string mobilePhone, string email, string sms, string ip, int port, bool useOutlook, string outlookFolders, int parent, long clientId) : base(TABLE, SQL_COL_ACCOUNT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		
		
		    this.m_login = login;
		    this.m_accountType = accountType;
		    this.m_permissions = permissions;
		    this.m_password = password;
		    this.m_firstName = firstName;
		    this.m_lastName = lastName;
		    this.m_phone = phone;
		    this.m_mobilePhone = mobilePhone;
		    this.m_email = email;
		    this.m_sms = sms;
		    this.m_ip = ip;
		    this.m_port = port;
		    this.m_useOutlook = useOutlook;
		    this.m_outlookFolders = outlookFolders;
		    this.m_parentAccount = parent;
		    this.m_clientId = clientId;
		
	    }
	    public Accounts(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.SqlClient.SqlConnection cn) : base(table, priKeyName, loadStmt, loadAllStmt, insertStmt, updateStmt, MbcsCentral.GlobalShared.CN)
	    {
		
	    }
	
	    /// <summary>
		/// Populates Application Accounts Properties from the database
		/// </summary>
		/// <param name="dr">Datarow read from database</param>
		/// <returns>Accounts object</returns>
		protected Accounts fill(DataRow dr)
	    {
		
		    try
		    {
			    this.m_accountId = (long)dr[SQL_COL_ACCOUNT_ID];
			    this.m_login = System.Convert.ToString(dr[SQL_COL_LOGIN]);
			    this.m_accountType = System.Convert.ToInt32(dr[SQL_COL_ACCOUNT_TYPE]);
			    this.m_permissions = System.Convert.ToInt32(dr[SQL_COL_PERMISSIONS]);
			    this.m_password = System.Convert.ToString(dr[SQL_COL_PASSWORD]);
			    this.m_firstName = (string) ((Information.IsDBNull(dr[SQL_COL_FIRST_NAME])) ? "" : (dr[SQL_COL_FIRST_NAME]));
			    this.m_lastName = (string) ((Information.IsDBNull(dr[SQL_COL_LAST_NAME])) ? "" : (dr[SQL_COL_LAST_NAME]));
			    this.m_phone = (string) ((Information.IsDBNull(dr[SQL_COL_PHONE])) ? "" : (dr[SQL_COL_PHONE]));
			    this.m_mobilePhone = (string) ((Information.IsDBNull(dr[SQL_COL_MOBILE_PHONE])) ? "" : (dr[SQL_COL_MOBILE_PHONE]));
			    this.m_email = (string) ((Information.IsDBNull(dr[SQL_COL_EMAIL])) ? "" : (dr[SQL_COL_EMAIL]));
			    this.m_sms = (string) ((Information.IsDBNull(dr[SQL_COL_SMS])) ? "" : (dr[SQL_COL_SMS]));
			    this.m_ip = (string) ((Information.IsDBNull(dr[SQL_COL_IP])) ? "" : (dr[SQL_COL_IP]));
			    this.m_port = System.Convert.ToInt32(dr[SQL_COL_PORT]);
			    this.m_outlookFolders = (string) ((Information.IsDBNull(dr[SQL_COL_OUTLOOK_FOLDERS])) ? "" : (dr[SQL_COL_OUTLOOK_FOLDERS]));
			    this.m_parentAccount = (int)dr[SQL_COL_PARENT];
			    this.m_clientId = (long)dr[SQL_COL_CLIENT_ID];
                if (m_clientId == 0)
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Client Id is zero for account <" + m_accountId.ToString(), false);
                if (!Information.IsDBNull(dr[SQL_COL_USE_OUTLOOK]))
                {
                    this.m_useOutlook = (bool)dr[SQL_COL_USE_OUTLOOK];
                }
            }
		    catch (System.Exception ex)
		    {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error filling Account object: " + ex.Message, false);
		    }
		    return this;
		
	    }
	/// <summary>
	/// Load by Primary Key
	/// </summary>
	/// <param name="id">Primary key value</param>
	/// <returns>Single Accounts object</returns>
	    public static Accounts load(long id)
	    {
		
		    DataSet ds = new DataSet();
		    Accounts acct = new Accounts();
		    ds = acct.loadByPrimaryKey(id.ToString());
		    DataRow dr;
		    try
		    {
			    dr = ds.Tables[0].Rows[0];
		    }
		    catch
		    {
			    return null;
		    }
		
		    return acct.fill(dr);
		
	    }
	
	    /// <summary>
		/// Load all Accounts from database
		/// </summary>
		/// <returns>An array of Accounts objects</returns>
		public static Accounts[] load()
	    {
		
		    ArrayList list = new ArrayList();
		    DataSet ds = new DataSet();
		    Accounts acct = new Accounts();
		
		    ds = acct.loadAll();
		    acct = null;
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
			    acct = new Accounts();
			    acct.fill(dr);
			    list.Add(acct);
			    acct = null;
		    }
		    try
		    {
			    return ((Accounts[]) (list.ToArray(typeof(Accounts))));
		    }
		    catch
		    {
			    return null;
		    }
		
	    }
        /// <summary>
		/// Load all Accounts for a specified Client ID
		/// </summary>
		/// <param name="clientId">The client ID to retrieve</param>
		/// <returns>An array of Accounts objects</returns>
		public static Accounts[] loadByClientId(long clientId)
        {

            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            Accounts acct = new Accounts();
            string[] opt = { clientId.ToString() };

            ds = acct.load(SQL_LOAD_BY_CLIENTID, opt );
            acct = null;

            if (ds == null || ds.Tables.Count == 0)
                return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                acct = new Accounts();
                acct.fill(dr);
                list.Add(acct);
                acct = null;
            }
            try
            {
                return ((Accounts[])(list.ToArray(typeof(Accounts))));
            }
            catch
            {
                return null;
            }
        }

		/// <summary>
		/// Load the Account object for a specified user account
		/// </summary>
		/// <param name="login">Login name for user</param>
		/// <returns>An Account object</returns>
	    public static Accounts load(string login)
	    {
		
		    Accounts acct = new Accounts();
		    DataSet ds = new DataSet();
		    DataRow dr = null;
		    ds = acct.load(SQL_LOAD_BY_LOGIN, new string[] {login});
		    try
		    {
			    dr = ds.Tables[0].Rows[0];
		    }
		    catch (System.IndexOutOfRangeException)
		    {
			    return null;
		    }
		    catch (System.Data.SqlClient.SqlException)
		    {
			    return null;
		    }
		    catch (System.Exception)
		    {
			    return null;
		    }
		    return acct.fill(dr);
		
	    }
	    
		public bool save()
	    {
		
		    if (m_accountId > 0)
		    {
                string[] values = new string[] { m_login, m_accountType.ToString(), m_permissions.ToString(), m_password, m_firstName, m_lastName, m_phone, m_mobilePhone, m_email, m_sms, m_ip, m_port.ToString(), m_useOutlook.ToString(), m_outlookFolders, m_parentAccount.ToString(), m_clientId.ToString(), m_accountId.ToString() };
			    if (this.updateRecord(m_accountId.ToString(), values))
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
                string[] values = new string[] { m_login, m_accountType.ToString(), m_permissions.ToString(), m_password, m_firstName, m_lastName, m_phone, m_mobilePhone, m_email, m_sms, m_ip, m_port.ToString(), m_useOutlook.ToString(), m_outlookFolders, m_parentAccount.ToString(), m_clientId.ToString() };
			    m_accountId = this.insertRecord(values);
			    if (m_accountId >= 0)
			    {
				    return true;
			    }
		    }
		    return false;
	    }
	
        /// <summary>
        /// Deletes an account and all group and permission ties
        /// </summary>
        /// <param name="acctId">Account Id</param>
        /// <returns>true, if successful</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public bool delete()
        {
            //System.Data.SqlClient.SqlConnection localCN = base.LocalCN;
            localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);

            if (localCN.State != ConnectionState.Open)
            {
                try
                {
                    localCN.Open();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Could not open DB connection.  Account not removed: " + ex.Message);
                    return false;
                }
            }
            SqlCommand cmd = localCN.CreateCommand();
            SqlTransaction trans;

            // Start of transaction
            trans = localCN.BeginTransaction("Remove Account");
            cmd.Connection = localCN;
            cmd.Transaction = trans;

            try
            {
                string query = "DELETE FROM " + TABLE + " WHERE " + SQL_COL_ACCOUNT_ID + " = " + m_accountId.ToString();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "DELETE FROM " + GroupMembers.TABLE + " WHERE " + GroupMembers.SQL_COL_ACCOUNT_ID + " = " + m_accountId.ToString();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "DELETE FROM " + user_permissions.TABLE + " WHERE " + user_permissions.SQL_COL_USER_ID + " = " + m_accountId.ToString();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                // Commit transaction
                trans.Commit();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error while attempting to commit Account removal. Type: " + ex.GetType() + "\r\n" + ex.Message);
                try
                {
                    trans.Rollback();
                    return false;
                }
                catch (System.Exception exr)
                {
                    MessageBox.Show("Could not Rollback Account removal.  Type: " + exr.GetType() + "\r\n" + exr.Message);
                    return false;
                }
            }
            finally
            {
                if (localCN != null && localCN.State == ConnectionState.Open)
                    localCN.Close();
            }
        }

	    //Public Function hasApplication(ByVal application As String) As Boolean
	
	    //   Dim app As Applications = Applications.loadByAcctApp(accountId, Application)
	
	    //        If IsNothing(app) Then
	    //            Return False
	    //        Else
	    //            Return True
	    //        End If
	
	    //    End Function

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                m_isChanged = true;
            }
        }

        // Properties
        public int accountType
        {
            set
            {
                m_accountType = value;
                OnPropertyChanged("accountType");
            }
            get { return m_accountType; }
        }

        public int permissions
        {
            set
            {
                m_permissions = value;
                OnPropertyChanged("permissions");
            }
            get { return m_permissions; }
        }

        public int port
        {
            set
            {
                m_port = value;
                OnPropertyChanged("port");
            }
            get { return m_port; }
        }

        public bool useOutlook
        {
            set
            {
                m_useOutlook = value;
                OnPropertyChanged("useOutlook");
            }
            get { return m_useOutlook; }
        }

        public long accountId
        {
            set
            {
                m_accountId = value;
                OnPropertyChanged("accountId");
            }
            get { return m_accountId; }
        }

        public int parentAccount
        {
            set
            {
                m_parentAccount = value;
                OnPropertyChanged("parentAccount");
            }
            get { return m_parentAccount; }
        }

        public long clientId
        {
            set
            {
                m_clientId = value;
                OnPropertyChanged("clientId");
            }
            get { return m_clientId; }
        }

        public string login
        {
            set
            {
                m_login = value;
                OnPropertyChanged("login");
            }
            get { return m_login; }
        }

        public string password
        {
            set
            {
                m_password = value;
                OnPropertyChanged("password");
            }
            get { return m_password; }
        }

        public string firstName
        {
            set
            {
                m_firstName = value;
                OnPropertyChanged("firstName");
            }
            get { return m_firstName; }
        }

        public string lastName
        {
            set
            {
                m_lastName = value;
                OnPropertyChanged("lastName");
            }
            get { return m_lastName; }
        }

        public string phone
        {
            set
            {
                m_phone = value;
                OnPropertyChanged("phone");
            }
            get { return m_phone; }
        }

        public string mobilePhone
        {
            set
            {
                m_mobilePhone = value;
                OnPropertyChanged("mobilePhone");
            }
            get { return m_mobilePhone; }
        }

        public string email
        {
            set
            {
                m_email = value;
                OnPropertyChanged("email");
            }
            get { return m_email; }
        }

        public string sms
        {
            set
            {
                m_sms = value;
                OnPropertyChanged("sms");
            }
            get { return m_sms; }
        }

        public string ip
        {
            set
            {
                m_ip = value;
                OnPropertyChanged("ip");
            }
            get { return m_ip; }
        }

        public string outlookFolder
        {
            set
            {
                m_outlookFolders = value;
                OnPropertyChanged("outlookFolder");
            }
            get { return m_outlookFolders; }
        }

        public string Name
        {
            get { return m_firstName.Trim() + " " + m_lastName.Trim(); }
        }

        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            get { return m_isChanged; }
        }

        /*
	    public void setLogin(string login)
	    {
		    this.login = login;
	    }
	
	    public void setAccountType(int accounttype)
	    {
		    this.accountType = accounttype;
	    }
	    public void setPermissions(int permissions)
	    {
		    this.permissions = permissions;
	    }
	    public void setPassword(string password)
	    {
		    this.password = password;
	    }
	    public void setFirstName(string name)
	    {
		    this.firstName = name;
	    }
	    public void setLastName(string name)
	    {
		    this.lastName = name;
	    }
	    public void setPhone(string phone)
	    {
		    this.phone = phone;
	    }
	    public void setMobilePhone(string mobilePhone)
	    {
		    this.mobilePhone = mobilePhone;
	    }
	    public void setEmail(string email)
	    {
		    this.email = email;
	    }
	    public void setSms(string sms)
	    {
		    this.sms = sms;
	    }
	    public void setIp(string ip)
	    {
		    this.ip = ip;
	    }
	    public void setPort(int port)
	    {
		    this.port = port;
	    }
	    public void setUseOutlook(bool useoutlook)
	    {
		    this.useOutlook = useoutlook;
	    }
	    public void setOutlookFolders(string outlookfolders)
	    {
		    this.outlookFolders = outlookfolders;
	    }
	    public void setOutlookFolders(string[] outlookfolders)
	    {
		    if (outlookfolders != null)
		    {
			    if (outlookfolders.Length > 0)
			    {
				    this.outlookFolders = "";
				    foreach (string s in outlookfolders)
				    {
					    if (this.outlookFolders.Length > 0)
					    {
						    this.outlookFolders += ",";
					    }
					    this.outlookFolders += s;
				    }
			    }
		    }
	    }
	    public void setParent(long parent)
	    {
		    this.parentAccount = parent;
	    }
	    public long getAccountId()
	    {
		    return accountId;
	    }
	    public string getLogin()
	    {
		    return login;
	    }
	    public int getAccountType()
	    {
		    return accountType;
	    }
	    public int getPermissions()
	    {
		    return permissions;
	    }
	    public string getPassword()
	    {
		    return password;
	    }
	    public string getFirstName()
	    {
		    return firstName;
	    }
	    public string getLastName()
	    {
		    return lastName;
	    }
	    public string getPhone()
	    {
		    return phone;
	    }
	    public string getMobilePhone()
	    {
		    return mobilePhone;
	    }
	    public string getEmail()
	    {
		    return email;
	    }
	    public string getSms()
	    {
		    return sms;
	    }
	    public string getIp()
	    {
		    return ip;
	    }
	    public int getPort()
	    {
		    return port;
	    }
	    public bool getUseOutlook()
	    {
		    return this.useOutlook;
	    }
	    public string getOutlookFolders()
	    {
		    return this.outlookFolders;
	    }
	    public string[] getOutlookFoldersAsArray()
	    {
		    string[] s = this.outlookFolders.Split(",".ToCharArray());
		    return s;
	    }
	    public long getParent()
	    {
		    return parentAccount;
	    }
	    public void setClientId(long clientId)
	    {
		    this.clientId = clientId;
	    }
	    public long getClientId()
	    {
		    return this.clientId;
	    }
         */

	    public bool hasViewPermission()
	    {
		    if (this.m_permissions >= 0)
		    {
			    return true;
		    }
		    else
		    {
			    return false;
		    }
	    }
	    public bool hasUserPermission()
	    {
		    if (this.m_permissions >= 1)
		    {
			    return true;
		    }
		    else
		    {
			    return false;
		    }
	    }
	    public bool hasManagerPermission()
	    {
		    if (this.m_permissions >= 2)
		    {
			    return true;
		    }
		    else
		    {
			    return false;
		    }
	    }
	    public bool hasAdminPermission()
	    {
		    if (this.m_permissions >= 3)
		    {
			    return true;
		    }
		    else
		    {
			    return false;
		    }
	    }
}
}