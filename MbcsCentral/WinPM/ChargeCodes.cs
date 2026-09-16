using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM {
    public class ChargeCodes : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "pm_ChargeCodes";

	    public const string SQL_COL_CLIENT_ID = "clientId";
	    public const string SQL_COL_LEGAL_CODE = "Legal Code";
	    public const string SQL_COL_RENT_FLAG = "rentFlag";
	    public const string SQL_COL_SECURITY_FLAG = "securityFlag";
	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_CHARGE_CODE = "Charge Code";
	    public const string SQL_COL_CHARGE_DESCRIPTION = "Charge Description";
	    public const string SQL_COL_G_L_CREDIT_ACCOUNT = "GL Credit Account";
	    public const string SQL_COL_G_L_CREDIT_SUB_ACCOUNT = "GL Credit Sub-Account";
	    public const string SQL_COL_G_L_DEBIT_ACCOUNT = "GL Debit Account";
	    public const string SQL_COL_G_L_DEBIT_SUB_ACCOUNT = "GL Debit Sub-Account";
	    public const string SQL_COL_G_L_CASH_ACCOUNT = "GL Cash Account";
	    public const string SQL_COL_GL_CASH_SUB_ACCOUNT = "Gl Cash Sub-Account";

	    static string SQL_LOAD = "SELECT " +
			    "[" + SQL_COL_CLIENT_ID + "]," +
			    "[" + SQL_COL_LEGAL_CODE + "]," +
			    "[" + SQL_COL_RENT_FLAG + "]," +
			    "[" + SQL_COL_SECURITY_FLAG + "]," +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_CHARGE_CODE + "]," +
			    "[" + SQL_COL_CHARGE_DESCRIPTION + "]," +
			    "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
			    "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]" +
			    " FROM " + TABLE +
			    " WHERE " + SQL_COL_I_D + " = ?" +
                " ORDER BY " + SQL_COL_CHARGE_DESCRIPTION;

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_RENT_FLAG + "]," +
                "[" + SQL_COL_SECURITY_FLAG + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_CHARGE_CODE + "]," +
                "[" + SQL_COL_CHARGE_DESCRIPTION + "]," +
                "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
                "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]" +
                " FROM " + TABLE +
                " WHERE " + SQL_COL_CLIENT_ID + "=?" +
                " ORDER BY [" + SQL_COL_CHARGE_DESCRIPTION + "]";

        static string SQL_LOAD_BY_CODE = "SELECT " +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_RENT_FLAG + "]," +
                "[" + SQL_COL_SECURITY_FLAG + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_CHARGE_CODE + "]," +
                "[" + SQL_COL_CHARGE_DESCRIPTION + "]," +
                "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," +
                "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
                "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]" +
                " FROM " + TABLE +
                " WHERE [" + SQL_COL_CHARGE_CODE + "]='?'" +
                " AND " + SQL_COL_CLIENT_ID + "= ?";

        static string SQL_LOAD_BY_LEGAL = "SELECT " +
               "[" + SQL_COL_CLIENT_ID + "]," +
               "[" + SQL_COL_LEGAL_CODE + "]," +
               "[" + SQL_COL_RENT_FLAG + "]," +
               "[" + SQL_COL_SECURITY_FLAG + "]," +
               "[" + SQL_COL_I_D + "]," +
               "[" + SQL_COL_CHARGE_CODE + "]," +
               "[" + SQL_COL_CHARGE_DESCRIPTION + "]," +
               "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," +
               "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," +
               "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," +
               "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," +
               "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
               "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]" +
               " FROM " + TABLE +
               " WHERE [" + SQL_COL_LEGAL_CODE + "]= 1" +
               " AND " + SQL_COL_CLIENT_ID + "= ?";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                 "[" + SQL_COL_CLIENT_ID + "]," +
			    "[" + SQL_COL_LEGAL_CODE + "]," +
			    "[" + SQL_COL_RENT_FLAG + "]," +
			    "[" + SQL_COL_SECURITY_FLAG + "]," +
			    "[" + SQL_COL_CHARGE_CODE + "]," +
			    "[" + SQL_COL_CHARGE_DESCRIPTION + "]," +
			    "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
			    "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]" +
			    ") VALUES (?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_CLIENT_ID + "]=?," +
			    "[" + SQL_COL_LEGAL_CODE + "]=?," +
			    "[" + SQL_COL_RENT_FLAG + "]=?," +
			    "[" + SQL_COL_SECURITY_FLAG + "]=?," +
			    "[" + SQL_COL_CHARGE_CODE + "]='?'," +
			    "[" + SQL_COL_CHARGE_DESCRIPTION + "]='?'," +
			    "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]='?'," +
			    "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]='?'," +
			    "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]='?'," +
			    "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]='?'," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]='?'," +
			    "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]='?'" +
			    " WHERE " + SQL_COL_I_D + " = ?;";

	    protected long m_clientId;
	    protected bool m_LegalCode;
	    protected bool m_rentFlag;
	    protected bool m_securityFlag;
	    protected int m_ID;
	    protected string m_ChargeCode;
	    protected string m_ChargeDescription;
	    protected string m_GLCreditAccount = "";
	    protected string m_GLCreditSubAccount = "";
	    protected string m_GLDebitAccount = "";
	    protected string m_GLDebitSubAccount = "";
	    protected string m_GLCashAccount = "";
	    protected string m_GlCashSubAccount = "";
        protected bool m_Delete = false;
	    protected bool m_isChanged = false;

        protected bool ExternalWinGL = false;

	    public ChargeCodes() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
            m_clientId = GlobalShared.user.Account.clientId;
            ExternalWinGL = GetWinGLlocation();
	    }

	    public ChargeCodes(long clientId,bool LegalCode,bool rentFlag,bool securityFlag,int ID,string ChargeCode,string ChargeDescription,string GLCreditAccount,string GLCreditSubAccount,string GLDebitAccount,string GLDebitSubAccount,string GLCashAccount,string GlCashSubAccount) : 
			    base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
            ExternalWinGL = GetWinGLlocation();
            m_clientId = clientId;
		    m_LegalCode = LegalCode;
		    m_rentFlag = rentFlag;
		    m_securityFlag = securityFlag;
		    m_ID = ID;
		    m_ChargeCode = ChargeCode;
		    m_ChargeDescription = ChargeDescription;
		    m_GLCreditAccount = GLCreditAccount;
		    m_GLCreditSubAccount = GLCreditSubAccount;
		    m_GLDebitAccount = GLDebitAccount;
		    m_GLDebitSubAccount = GLDebitSubAccount;
		    m_GLCashAccount = GLCashAccount;
		    m_GlCashSubAccount = GlCashSubAccount;
	    }

        protected bool GetWinGLlocation()
        {
            Control[] control = Control.load();
            if (control == null || control.Length == 0)
                return false;
            return control[0].UseExternalWinGL;
        }

	    protected ChargeCodes fill(DataRow dr)
	    {
		    try {
			    m_clientId = (int) dr[SQL_COL_CLIENT_ID];
			    m_LegalCode = (bool) dr[SQL_COL_LEGAL_CODE];
			    m_rentFlag = (bool) dr[SQL_COL_RENT_FLAG];
			    m_securityFlag = (bool) dr[SQL_COL_SECURITY_FLAG];
			    m_ID = (int) dr[SQL_COL_I_D];
			    m_ChargeCode = (string) (Convert.IsDBNull(dr[SQL_COL_CHARGE_CODE]) ? "" : dr[SQL_COL_CHARGE_CODE]);
			    m_ChargeDescription = (string) (Convert.IsDBNull(dr[SQL_COL_CHARGE_DESCRIPTION]) ? "" : dr[SQL_COL_CHARGE_DESCRIPTION]);
			    m_GLCreditAccount = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_CREDIT_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CREDIT_ACCOUNT]);
			    m_GLCreditSubAccount = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_CREDIT_SUB_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CREDIT_SUB_ACCOUNT]);
			    m_GLDebitAccount = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_DEBIT_ACCOUNT]) ? "" : dr[SQL_COL_G_L_DEBIT_ACCOUNT]);
			    m_GLDebitSubAccount = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_DEBIT_SUB_ACCOUNT]) ? "" : dr[SQL_COL_G_L_DEBIT_SUB_ACCOUNT]);
			    m_GLCashAccount = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_CASH_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CASH_ACCOUNT]);
			    m_GlCashSubAccount = (string) (Convert.IsDBNull(dr[SQL_COL_GL_CASH_SUB_ACCOUNT]) ? "" : dr[SQL_COL_GL_CASH_SUB_ACCOUNT]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static ChargeCodes load( int m_ID)
	    {
		    ChargeCodes obj  = new ChargeCodes();
		    string[] opt = {m_ID.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

        public static ChargeCodes[] loads(long clientId, bool IncludeAll = false, bool IncludeAcct = false)
	    {
		    ArrayList list = new ArrayList();

            string[] opt = {clientId.ToString() };
		    DataSet ds = new DataSet();
		    ChargeCodes obj = new ChargeCodes();

		    ds = obj.load(SQL_LOAD_ALL,opt);
		    if (ds == null)
			    return null;
            if (IncludeAll)
                list.Add(new ChargeCodes(clientId, false,false,false, 0, "", "<ALL>", "", "", "", "", "", ""));
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new ChargeCodes();
			    obj.fill(dr);
                if (IncludeAcct)
                    GetGlAccounts(obj);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (ChargeCodes[])list.ToArray(typeof(ChargeCodes));
		    } catch (SqlException ex) {
			    return null;
		    }
 	    }

        public static ChargeCodes[] loadLegal(long clientId, bool IncludeAll = false, bool IncludeAcct = false, bool IncludeBlank = false)
        {
            ArrayList list = new ArrayList();

            string[] opt = { clientId.ToString() };
            DataSet ds = new DataSet();
            ChargeCodes obj = new ChargeCodes();

            ds = obj.load(SQL_LOAD_BY_LEGAL, opt);
            if (ds == null)
                return null;
            if (IncludeBlank)
                list.Add(new ChargeCodes(clientId, false, false, false, 0, "", "", "", "", "", "", "", ""));
            if (IncludeAll)
                list.Add(new ChargeCodes(clientId, false, false, false, 0, "", "<ALL>", "", "", "", "", "", ""));
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ChargeCodes();
                obj.fill(dr);
                if (IncludeAcct)
                    GetGlAccounts(obj);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ChargeCodes[])list.ToArray(typeof(ChargeCodes));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static ArrayList loadList(long clientId, bool IncludeAcct = false)
        {
            ArrayList list = new ArrayList();

            string[] opt = { clientId.ToString() };
            DataSet ds = new DataSet();
            ChargeCodes obj = new ChargeCodes();

            ds = obj.load(SQL_LOAD_ALL, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ChargeCodes();
                obj.fill(dr);
                if (IncludeAcct)
                    GetGlAccounts(obj);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return list;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        private static void GetGlAccounts(ChargeCodes chg)
        {
            WinGL.Accounts acct = null;
            if (!chg.GLCreditAccount.Trim().Equals("") && Information.IsNumeric(chg.GLCreditAccount))
            {
                acct = WinGL.Accounts.load(int.Parse(chg.GLCreditAccount));
                if (acct != null)
                    chg.GLCreditName = acct.Description;
                else
                    chg.GLCreditName = "";
            }
            if (!chg.GLDebitAccount.Trim().Equals("") && Information.IsNumeric(chg.GLDebitAccount))
            {
                acct = WinGL.Accounts.load(int.Parse(chg.GLDebitAccount));
                if (acct != null)
                    chg.GLDebitName = acct.Description;
                else
                    chg.GLDebitName = "";
            }
        }
        public static ChargeCodes load(string code, long clientId)
        {
            ChargeCodes obj = new ChargeCodes();
            string[] opt = {code, clientId.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_CODE, opt);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

	    		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	    {
		    if (m_ID > 0 ) {
			    string[] values = {m_clientId.ToString(), m_LegalCode.ToString(), m_rentFlag.ToString(), m_securityFlag.ToString(), m_ChargeCode, m_ChargeDescription, m_GLCreditAccount, m_GLCreditSubAccount, m_GLDebitAccount, m_GLDebitSubAccount, m_GLCashAccount, m_GlCashSubAccount, m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_clientId.ToString(), m_LegalCode.ToString(), m_rentFlag.ToString(), m_securityFlag.ToString(), m_ChargeCode, m_ChargeDescription, m_GLCreditAccount, m_GLCreditSubAccount, m_GLDebitAccount, m_GLDebitSubAccount, m_GLCashAccount, m_GlCashSubAccount};
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
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            get { return m_isChanged; }
            set { m_isChanged = value; }
        }
	    public long clientId {
		    set{ m_clientId = value;
		    OnPropertyChanged("clientId"); }
		    get { return m_clientId; }
	    }

	    public bool LegalCode {
		    set{ m_LegalCode = value;
		    OnPropertyChanged("LegalCode"); }
		    get { return m_LegalCode; }
	    }

	    public bool rentFlag {
		    set{ m_rentFlag = value;
		    OnPropertyChanged("rentFlag"); }
		    get { return m_rentFlag; }
	    }

	    public bool securityFlag {
		    set{ m_securityFlag = value;
		    OnPropertyChanged("securityFlag"); }
		    get { return m_securityFlag; }
	    }

	    public int ID {
		    get { return m_ID; }
	    }

	    public string ChargeCode {
		    set{ m_ChargeCode = value;
		    OnPropertyChanged("ChargeCode"); }
		    get { return m_ChargeCode; }
	    }

	    public string ChargeDescription {
		    set{ m_ChargeDescription = value;
		    OnPropertyChanged("ChargeDescription"); }
		    get { return m_ChargeDescription; }
	    }

	    public string GLCreditAccount {
		    set{ m_GLCreditAccount = value;
		    OnPropertyChanged("GLCreditAccount"); }
		    get { return m_GLCreditAccount; }
	    }

        public int GLCreditAccountInt
        {
            get
            {
                int key = 0;
                int.TryParse(GLCreditAccount, out key);
                return key;
            }

            set
            {
                m_GLCreditAccount = value.ToString();
                OnPropertyChanged("GLCreditAccountInt");
            }
        }

        public string GLCreditAccountName
        {
            get
            {
                WinGL.Accounts acct = WinGL.Accounts.load(GLCreditAccountInt);
                if (acct == null)
                    return "";
                else
                    return acct.Description;
            }
        }
        public string GLCreditSubAccount {
		    set{ m_GLCreditSubAccount = value;
		    OnPropertyChanged("GLCreditSubAccount"); }
		    get { return m_GLCreditSubAccount; }
	    }

	    public string GLDebitAccount {
		    set{ m_GLDebitAccount = value;
		    OnPropertyChanged("GLDebitAccount"); }
		    get { return m_GLDebitAccount; }
	    }

        public int GLDebitAccountInt
        {
            get
            {
                int key = 0;
                int.TryParse(GLDebitAccount, out key);
                return key;
            }

            set
            {
                m_GLDebitAccount = value.ToString();
                OnPropertyChanged("GLDebitAccountInt");
            }
        }

        public string GLDebitAccountName
        {
            get
            {
                WinGL.Accounts acct = WinGL.Accounts.load(GLDebitAccountInt);
                if (acct == null)
                    return "";
                else
                    return acct.Description;
            }
        }

	    public string GLDebitSubAccount {
		    set{ m_GLDebitSubAccount = value;
		    OnPropertyChanged("GLDebitSubAccount"); }
		    get { return m_GLDebitSubAccount; }
	    }

        public string GLDebitName { get; set; }

	    public string GLCashAccount {
		    set{ m_GLCashAccount = value;
		    OnPropertyChanged("GLCashAccount"); }
		    get { return m_GLCashAccount; }
	    }

	    public string GlCashSubAccount {
		    set{ m_GlCashSubAccount = value;
		    OnPropertyChanged("GlCashSubAccount"); }
		    get { return m_GlCashSubAccount; }
	    }

        public string GLCreditName { get; set; }

        public string DisplayDescription
        {
            get { return (!m_ChargeCode.Trim().Equals("") ? m_ChargeCode.PadRight(2) + ": " : "") + m_ChargeDescription; }
        }

        public bool Delete {
            get { return m_Delete; }
            set { m_Delete = value;
                OnPropertyChanged("Delete");

            }
        }
    }
}
