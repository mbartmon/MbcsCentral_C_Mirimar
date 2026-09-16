using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class Company : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Company";

        public const string SQL_COL_FISCAL_YEAR_END = "FiscalYearEnd";
        public const string SQL_COL_COMPANY_NUMBER = "CompanyNumber";
        public const string SQL_COL_CURRENT_FISCAL_YEAR = "CurrentFiscalYear";
        public const string SQL_COL_ID = "Id";
        public const string SQL_COL_IN_ACTIVE = "InActive";
        public const string SQL_COL_NAME = "Name";
        public const string SQL_COL_STREET_ADDRESS = "StreetAddress";
        public const string SQL_COL_CITY = "City";
        public const string SQL_COL_STATE = "State";
        public const string SQL_COL_ZIP = "Zip";
        public const string SQL_COL_TELEPHONE = "Telephone";
        public const string SQL_COL_FAX = "Fax";
        public const string SQL_COL_EMAIL = "Email";
        public const string SQL_COL_FEDERAL_I_D = "FederalID";
        public const string SQL_COL_STATE_I_D = "StateID";
        public const string SQL_COL_ROUTING_NUMBER = "RoutingNumber";
        public const string SQL_COL_BANK_ACCOUNT = "BankAccount";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_FISCAL_YEAR_END + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_IN_ACTIVE + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_STREET_ADDRESS + "]," +
                "[" + SQL_COL_CITY + "]," +
                "[" + SQL_COL_STATE + "]," +
                "[" + SQL_COL_ZIP + "]," +
                "[" + SQL_COL_TELEPHONE + "]," +
                "[" + SQL_COL_FAX + "]," +
                "[" + SQL_COL_EMAIL + "]," +
                "[" + SQL_COL_FEDERAL_I_D + "]," +
                "[" + SQL_COL_STATE_I_D + "]," +
                "[" + SQL_COL_ROUTING_NUMBER + "]," + 
                "[" + SQL_COL_BANK_ACCOUNT + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

            static string SQL_LOAD_ALL = "SELECT " +
                    "[" + SQL_COL_FISCAL_YEAR_END + "]," +
                    "[" + SQL_COL_COMPANY_NUMBER + "]," +
                    "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                    "[" + SQL_COL_ID + "]," +
                    "[" + SQL_COL_IN_ACTIVE + "]," +
                    "[" + SQL_COL_NAME + "]," +
                    "[" + SQL_COL_STREET_ADDRESS + "]," +
                    "[" + SQL_COL_CITY + "]," +
                    "[" + SQL_COL_STATE + "]," +
                    "[" + SQL_COL_ZIP + "]," +
                    "[" + SQL_COL_TELEPHONE + "]," +
                    "[" + SQL_COL_FAX + "]," +
                    "[" + SQL_COL_EMAIL + "]," +
                    "[" + SQL_COL_FEDERAL_I_D + "]," +
                    "[" + SQL_COL_STATE_I_D + "]," +
                    "[" + SQL_COL_ROUTING_NUMBER + "]," +
                    "[" + SQL_COL_BANK_ACCOUNT + "]" +
                    " FROM " + TABLE +
                    " ORDER BY " + SQL_COL_NAME;

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_FISCAL_YEAR_END + "]," +
                "[" + SQL_COL_COMPANY_NUMBER + "]," +
                "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                "[" + SQL_COL_IN_ACTIVE + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_STREET_ADDRESS + "]," +
                "[" + SQL_COL_CITY + "]," +
                "[" + SQL_COL_STATE + "]," +
                "[" + SQL_COL_ZIP + "]," +
                "[" + SQL_COL_TELEPHONE + "]," +
                "[" + SQL_COL_FAX + "]," +
                "[" + SQL_COL_EMAIL + "]," +
                "[" + SQL_COL_FEDERAL_I_D + "]," +
                "[" + SQL_COL_STATE_I_D + "]," +
                "[" + SQL_COL_ROUTING_NUMBER + "]," +
                "[" + SQL_COL_BANK_ACCOUNT + "]" +
               ") VALUES (?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?');";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_FISCAL_YEAR_END + "]=?," +
                "[" + SQL_COL_COMPANY_NUMBER + "]=?," +
                "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]=?," +
                "[" + SQL_COL_IN_ACTIVE + "]=?," +
                "[" + SQL_COL_NAME + "]='?'," +
                "[" + SQL_COL_STREET_ADDRESS + "]='?'," +
                "[" + SQL_COL_CITY + "]='?'," +
                "[" + SQL_COL_STATE + "]='?'," +
                "[" + SQL_COL_ZIP + "]='?'," +
                "[" + SQL_COL_TELEPHONE + "]='?'," +
                "[" + SQL_COL_FAX + "]='?'," +
                "[" + SQL_COL_EMAIL + "]='?'," +
                "[" + SQL_COL_FEDERAL_I_D + "]='?'," +
                "[" + SQL_COL_STATE_I_D + "]='?'," +
                "[" + SQL_COL_ROUTING_NUMBER + "]='?'," +
                "[" + SQL_COL_BANK_ACCOUNT + "]='?'" +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_BY_COMPANY = "SELECT " +
                 "[" + SQL_COL_FISCAL_YEAR_END + "]," +
                 "[" + SQL_COL_COMPANY_NUMBER + "]," +
                 "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                 "[" + SQL_COL_ID + "]," +
                 "[" + SQL_COL_IN_ACTIVE + "]," +
                 "[" + SQL_COL_NAME + "]," +
                 "[" + SQL_COL_STREET_ADDRESS + "]," +
                 "[" + SQL_COL_CITY + "]," +
                 "[" + SQL_COL_STATE + "]," +
                 "[" + SQL_COL_ZIP + "]," +
                 "[" + SQL_COL_TELEPHONE + "]," +
                 "[" + SQL_COL_FAX + "]," +
                 "[" + SQL_COL_EMAIL + "]," +
                 "[" + SQL_COL_FEDERAL_I_D + "]," +
                 "[" + SQL_COL_STATE_I_D + "]," +
                 "[" + SQL_COL_ROUTING_NUMBER + "]," +
                 "[" + SQL_COL_BANK_ACCOUNT + "]" +
                 " FROM " + TABLE +
                 " WHERE " + "[" + SQL_COL_COMPANY_NUMBER + "] = ?;";

        protected int m_FiscalYearEnd;
        protected short m_CompanyNumber;
        protected short m_CurrentFiscalYear;
        protected int m_Id;
        protected bool m_InActive;
        protected string m_Name;
        protected string m_StreetAddress;
        protected string m_City;
        protected string m_State;
        protected string m_Zip;
        protected string m_Telephone;
        protected string m_Fax;
        protected string m_Email;
        protected string m_FederalID;
        protected string m_StateID;
        protected string m_RoutingNumber;
        protected string m_BankAccount;
        protected bool m_isChanged;

	    public Company() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Company(int FiscalYearEnd,short CompanyNumber,short CurrentFiscalYear,int Id,string Name,string StreetAddress,string City,string State,string Zip,string Telephone,string Fax,string Email,string FederalID,string StateID) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_FiscalYearEnd = FiscalYearEnd;
		    m_CompanyNumber = CompanyNumber;
		    m_CurrentFiscalYear = CurrentFiscalYear;
		    m_Id = Id;
		    m_Name = Name;
		    m_StreetAddress = StreetAddress;
		    m_City = City;
		    m_State = State;
		    m_Zip = Zip;
		    m_Telephone = Telephone;
		    m_Fax = Fax;
		    m_Email = Email;
		    m_FederalID = FederalID;
		    m_StateID = StateID;
	    }

        protected Company fill(DataRow dr)
        {
            try
            {
                //m_FiscalYearEnd = (int)dr[SQL_COL_FISCAL_YEAR_END];
                m_CompanyNumber = (short)dr[SQL_COL_COMPANY_NUMBER];
                m_CurrentFiscalYear = (short)dr[SQL_COL_CURRENT_FISCAL_YEAR];
                m_Id = (int)dr[SQL_COL_ID];
                m_InActive = (bool)dr[SQL_COL_IN_ACTIVE];
                m_Name = (string)(Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
                m_StreetAddress = (string)(Convert.IsDBNull(dr[SQL_COL_STREET_ADDRESS]) ? "" : dr[SQL_COL_STREET_ADDRESS]);
                m_City = (string)(Convert.IsDBNull(dr[SQL_COL_CITY]) ? "" : dr[SQL_COL_CITY]);
                m_State = (string)(Convert.IsDBNull(dr[SQL_COL_STATE]) ? "" : dr[SQL_COL_STATE]);
                m_Zip = (string)(Convert.IsDBNull(dr[SQL_COL_ZIP]) ? "" : dr[SQL_COL_ZIP]);
                m_Telephone = (string)(Convert.IsDBNull(dr[SQL_COL_TELEPHONE]) ? "" : dr[SQL_COL_TELEPHONE]);
                m_Fax = (string)(Convert.IsDBNull(dr[SQL_COL_FAX]) ? "" : dr[SQL_COL_FAX]);
                m_Email = (string)(Convert.IsDBNull(dr[SQL_COL_EMAIL]) ? "" : dr[SQL_COL_EMAIL]);
                m_FederalID = (string)(Convert.IsDBNull(dr[SQL_COL_FEDERAL_I_D]) ? "" : dr[SQL_COL_FEDERAL_I_D]);
                m_StateID = (string)(Convert.IsDBNull(dr[SQL_COL_STATE_I_D]) ? "" : dr[SQL_COL_STATE_I_D]);
                m_RoutingNumber = (string)(Convert.IsDBNull(dr[SQL_COL_ROUTING_NUMBER]) ? "" : dr[SQL_COL_ROUTING_NUMBER]);
                m_BankAccount = (string)(Convert.IsDBNull(dr[SQL_COL_BANK_ACCOUNT]) ? "" : dr[SQL_COL_BANK_ACCOUNT]);

            }
            catch (SqlException ex)
            {
            }
            return this;

        }

	    public static Company load( int m_Id)
	    {
		    Company obj  = new Company();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null && ds.Tables[0].Rows.Count > 0) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

        public static Company loadByCompany(int m_Id)
        {
            Company obj = new Company();
            string[] opt = { m_Id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_COMPANY, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

	    public static Company[] load(bool addBlank = false, bool Inactive = false, bool debug = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Company obj = new Company();

            if (debug)
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Company.load [" + SQL_LOAD_ALL + "]",false);

		    ds = obj.loadAll(debug);
                if (ds == null)
                {
                    if (debug)
                        GlobalShared.Log.Log((int)LogClass.logType.Debug, "Company.load - Returned dataset is null", false);
                    return null;
                }
		    obj = null;
            if (addBlank)
                list.Add(new Company());
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Company();
			    obj.fill(dr);
                if (Inactive || (!Inactive && !obj.InActive))
			        list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Company[])list.ToArray(typeof(Company));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

        {
            if (m_Id > 0)
            {
                string[] values = { m_FiscalYearEnd.ToString(), m_CompanyNumber.ToString(), m_CurrentFiscalYear.ToString(), m_InActive.ToString(), m_Name, m_StreetAddress, m_City, m_State, m_Zip, m_Telephone, m_Fax, m_Email, m_FederalID, m_StateID,
                                    m_RoutingNumber, m_BankAccount,  m_Id.ToString() };
                if (this.updateRecord(m_Id.ToString(), values))
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
                string[] values = { m_FiscalYearEnd.ToString(), m_CompanyNumber.ToString(), m_CurrentFiscalYear.ToString(), m_InActive.ToString(), m_Name, m_StreetAddress, m_City, m_State, m_Zip, m_Telephone, m_Fax, m_Email, m_FederalID, m_StateID,
                                    m_RoutingNumber, m_BankAccount };
                m_Id = this.insertRecord(values);
                if (m_Id >= 0)
                {
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
	    public int FiscalYearEnd {
		    set{ m_FiscalYearEnd = value;
		    OnPropertyChanged("FiscalYearEnd"); }
		    get { return m_FiscalYearEnd; }
	    }

	    public short CompanyNumber {
		    set{ m_CompanyNumber = value;
		    OnPropertyChanged("CompanyNumber"); }
		    get { return m_CompanyNumber; }
	    }

	    public short CurrentFiscalYear {
		    set{ m_CurrentFiscalYear = value;
		    OnPropertyChanged("CurrentFiscalYear"); }
		    get { return m_CurrentFiscalYear; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public string Name {
		    set{ m_Name = value;
		    OnPropertyChanged("Name"); }
		    get { return m_Name; }
	    }

	    public string StreetAddress {
		    set{ m_StreetAddress = value;
		    OnPropertyChanged("StreetAddress"); }
		    get { return m_StreetAddress; }
	    }

	    public string City {
		    set{ m_City = value;
		    OnPropertyChanged("City"); }
		    get { return m_City; }
	    }

	    public string State {
		    set{ m_State = value;
		    OnPropertyChanged("State"); }
		    get { return m_State; }
	    }

	    public string Zip {
		    set{ m_Zip = value;
		    OnPropertyChanged("Zip"); }
		    get { return m_Zip; }
	    }

	    public string Telephone {
		    set{ m_Telephone = value;
		    OnPropertyChanged("Telephone"); }
		    get { return m_Telephone; }
	    }

	    public string Fax {
		    set{ m_Fax = value;
		    OnPropertyChanged("Fax"); }
		    get { return m_Fax; }
	    }

	    public string Email {
		    set{ m_Email = value;
		    OnPropertyChanged("Email"); }
		    get { return m_Email; }
	    }

	    public string FederalID {
		    set{ m_FederalID = value;
		    OnPropertyChanged("FederalID"); }
		    get { return m_FederalID; }
	    }

	    public string StateID {
		    set{ m_StateID = value;
		    OnPropertyChanged("StateID"); }
		    get { return m_StateID; }
	    }


        public string RoutingNumber
        {
            get { return m_RoutingNumber; }
            set { 
                m_RoutingNumber = value;
                OnPropertyChanged("RoutingNumber");
            }
        }

        public string BankAccount
        {
            get { return m_BankAccount; }
            set { m_BankAccount = value;
                OnPropertyChanged("BankAccount");  }
        }
        public bool InActive
        {
            set
            {
                m_InActive = value;
                OnPropertyChanged("InActive");
            }
            get { return m_InActive; }
        }
    }
}
