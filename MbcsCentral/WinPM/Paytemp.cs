using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    public class Paytemp : clsDB, INotifyPropertyChanged
    {
	
	    private PropertyChangedEventHandler PropertyChangedEvent;
	    public event PropertyChangedEventHandler PropertyChanged
	    {
		    add
		    {
			    PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Combine(PropertyChangedEvent, value);
		    }
		    remove
		    {
			    PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Remove(PropertyChangedEvent, value);
		    }
	    }
	
	
	    static string TABLE = "pm_Paytemp";
	
	    public const string SQL_COL_USER_I_D = "UserID";
	    public const string SQL_COL_BUILDING_NUMBER = "Building Number";
	    public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_CODE = "Code";
	    public const string SQL_COL_NAME = "Name";
	    public const string SQL_COL_APARTMENT = "Apartment";
	    public const string SQL_COL_CLIENT_ID="clientId";
        public const string SQL_COL_ID = "ID";
        public const string SQL_COL_NOTES = "Notes";

        static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_USER_I_D + "]," + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_DATE + "]," + 
                                "[" + SQL_COL_AMOUNT + "]," + 
                                "[" + SQL_COL_CODE + "]," + 
                                "[" + SQL_COL_NAME + "]," + 
                                "[" + SQL_COL_APARTMENT + "]," + 
                                SQL_COL_CLIENT_ID + "," +
                                SQL_COL_ID + "," +
                                SQL_COL_NOTES +
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_ID + "=?";

	    static string SQL_LOAD_ALL = " SELECT " + 
                                    "[" + SQL_COL_USER_I_D + "]," + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_CODE + "]," + 
                                    "[" + SQL_COL_NAME + "]," + 
                                    "[" + SQL_COL_APARTMENT + "]," + 
                                    SQL_COL_CLIENT_ID + "," +
                                    SQL_COL_ID + "," +
                                    SQL_COL_NOTES +
                                    " FROM " + TABLE + ";";
	
	    static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_USER_I_D + "]," + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_CODE + "]," + 
                                    "[" + SQL_COL_NAME + "]," + 
                                    "[" + SQL_COL_APARTMENT + "]," + 
                                    SQL_COL_CLIENT_ID  + "," +
                                    "[" + SQL_COL_NOTES + "]" +
                                    ") VALUES (\'?\', \'?\', \'?\', '?', ?, \'?\', \'?\', \'?\', ?, \'?\');";
	
	    static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
                                    "[" + SQL_COL_USER_I_D + "]=\'?\'," + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]=\'?\'," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]=\'?\'," + 
                                    "[" + SQL_COL_DATE + "]='?'," + 
                                    "[" + SQL_COL_AMOUNT + "]=?," + 
                                    "[" + SQL_COL_CODE + "]=\'?\'," + 
                                    "[" + SQL_COL_NAME + "]=\'?\'" + 
                                    "[" + SQL_COL_APARTMENT + "]=\'?\'," + 
                                    SQL_COL_CLIENT_ID + "=?," +
								    "[" + SQL_COL_NOTES + "]=\'?\'" +
                                    " WHERE " + SQL_COL_ID + "=?";

        static string SQL_LOAD_BY_ACCOUNT = " SELECT " +
                        "[" + SQL_COL_USER_I_D + "]," +
                        "[" + SQL_COL_BUILDING_NUMBER + "]," +
                        "[" + SQL_COL_TENANT_NUMBER + "]," +
                        "[" + SQL_COL_DATE + "]," +
                        "[" + SQL_COL_AMOUNT + "]," +
                        "[" + SQL_COL_CODE + "]," +
                        "[" + SQL_COL_NAME + "]," +
                        "[" + SQL_COL_APARTMENT + "]," +
                        SQL_COL_CLIENT_ID + "," +
                        SQL_COL_ID + "," +
                       "[" + SQL_COL_NOTES + "]" +
                        " FROM " + TABLE +
                        " WHERE " + SQL_COL_USER_I_D + "=?"+
                        " AND " + SQL_COL_CLIENT_ID + "=?";

        static string SQL_LOAD_FOR_REGISTER = " SELECT " +
                        "[" + SQL_COL_USER_I_D + "]," +
                        TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "]," +
                        TABLE + ".[" + SQL_COL_TENANT_NUMBER + "]," +
                        "[" + SQL_COL_DATE + "]," +
                        "[" + SQL_COL_AMOUNT + "]," +
                        TABLE + ".[" + SQL_COL_CODE + "]," +
                        "[" + SQL_COL_NAME + "]," +
                        TABLE + ".[" + SQL_COL_APARTMENT + "]," +
                        ChargeCodes.TABLE + ".[" + ChargeCodes.SQL_COL_CHARGE_DESCRIPTION + "]," +
                        Building.TABLE + ".[" + Building.SQL_COL_ADDRESS_1 + "]" +
                        " FROM " + TABLE + ", " +
                        ChargeCodes.TABLE + ", " +
                        Building.TABLE +
                        " WHERE " + ChargeCodes.TABLE + ".[" + ChargeCodes.SQL_COL_CHARGE_CODE + "] = " + TABLE + "." + SQL_COL_CODE +
                        " AND " + Building.TABLE + ".[" + Building.SQL_COL_BUILDING_NUMBER + "] = " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "]" +
                        " AND " + SQL_COL_USER_I_D + "=?" +
                        " AND " + TABLE + "." + SQL_COL_CLIENT_ID + "=?" + 
                        " ORDER BY " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "], " +
                        TABLE + ".[" + SQL_COL_TENANT_NUMBER + "], " +
                        TABLE + ".[" + SQL_COL_CODE + "]";

	    protected string m_UserID;
	    protected string m_BuildingNumber;
	    protected string m_TenantNumber;
	    protected DateTime m_Date;
	    protected decimal m_Amount;
	    protected string m_Code;
	    protected string m_Name;
	    protected string m_Apartment;
        protected int m_clientId;
        protected int m_ID;
        protected string m_Notes;
        protected bool m_isChanged;

        // Fields for Register
        protected string m_Description = "";
        protected string m_Address1 = "";

		protected DataTable m_dataTable = null;

	    public Paytemp() : base(TABLE,SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }
	
	    public Paytemp(string UserID, string BuildingNumber, string TenantNumber, DateTime Date, decimal Amount, string Code, string Name, string Apartment, int clientId = 0) : base(TABLE,SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_UserID = UserID;
		    m_BuildingNumber = BuildingNumber;
		    m_TenantNumber = TenantNumber;
		    m_Date = Date;
		    m_Amount = Amount;
		    m_Code = Code;
		    m_Name = Name;
		    m_Apartment = Apartment;
            if (clientId == 0 && GlobalShared.user != null)
                m_clientId = (int) GlobalShared.user.Account.clientId;
            else
                m_clientId = clientId;
        }
	
	    protected Paytemp fill(DataRow dr, bool register = false)
	    {
		
		    try
		    {
			    m_UserID = (string) ((Information.IsDBNull(dr[SQL_COL_USER_I_D])) ? "" : (dr[SQL_COL_USER_I_D]));
			    m_BuildingNumber = (string) ((Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER])) ? "" : (dr[SQL_COL_BUILDING_NUMBER]));
			    m_TenantNumber = (string) ((Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER])) ? "" : (dr[SQL_COL_TENANT_NUMBER]));
			    m_Date = (DateTime)dr[SQL_COL_DATE];
			    m_Amount = System.Convert.ToDecimal(dr[SQL_COL_AMOUNT]);
			    m_Code = (string) ((Information.IsDBNull(dr[SQL_COL_CODE])) ? "" : (dr[SQL_COL_CODE]));
			    m_Name = (string) ((Information.IsDBNull(dr[SQL_COL_NAME])) ? "" : (dr[SQL_COL_NAME]));
			    m_Apartment = (string) ((Information.IsDBNull(dr[SQL_COL_APARTMENT])) ? "" : (dr[SQL_COL_APARTMENT]));
                if (register)
                {
                    m_Description = (string) (Information.IsDBNull(dr[ChargeCodes.SQL_COL_CHARGE_DESCRIPTION]) ? "" : dr[ChargeCodes.SQL_COL_CHARGE_DESCRIPTION] );
                    m_Address1 = (string) (Information.IsDBNull(dr[Building.SQL_COL_ADDRESS_1]) ? "" : dr[Building.SQL_COL_ADDRESS_1]);
                }
		    }
		    catch (SqlException)
		    {
		    }
		    catch (System.Exception)
		    {
		    }
		    return this;
		
	    }
	
		protected void fill_table(DataTable dt)
        {
			try
			{
				m_dataTable = dt;
			}
			catch (Exception ex)
			{ }
        }
	    public static Paytemp load(int id)
	    {
		
		    Paytemp obj = new Paytemp();
		    string[] opt = new string[] {id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    DataRow dr = ds.Tables[0].Rows[0];
		
		    return obj.fill(dr);
		
	    }
	
	    public static Paytemp[] load()
	    {
		
		    ArrayList list = new ArrayList();
		
		    DataSet ds = new DataSet();
		    Paytemp obj = new Paytemp();
		
		    ds = obj.loadAll();
		    obj = null;
		
		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
			    obj = new Paytemp();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try
		    {
			    return ((Paytemp[]) (list.ToArray(typeof(Paytemp))));
		    }
		    catch (SqlException)
		    {
			    return null;
		    }
		
	    }

        public static Paytemp[] loadByUser(bool returnTable = false)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Paytemp obj = new Paytemp();
            string[] opt = { GlobalShared.user.Account.accountId.ToString(), GlobalShared.user.Account.clientId.ToString() };

            ds = obj.load(SQL_LOAD_BY_ACCOUNT,opt);
            obj = null;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
			if (returnTable)
			{
				obj = new Paytemp();
				obj.fill_table(ds.Tables[0]);
				list.Add(obj);
			}
			else
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					obj = new Paytemp();
					obj.fill(dr);
					list.Add(obj);
					obj = null;
				}
			}
            try
            {
                return ((Paytemp[]) (list.ToArray( typeof( Paytemp ) )));
            }
            catch (SqlException)
            {
                return null;
            }

        }

        public static Paytemp[] loadForRegister()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Paytemp obj = new Paytemp();
            string[] opt = { GlobalShared.user.Account.accountId.ToString(), GlobalShared.user.Account.clientId.ToString() };

            ds = obj.load( SQL_LOAD_FOR_REGISTER, opt );
            if (ds == null)
                return null;
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Paytemp();
                obj.fill( dr, true );
                list.Add( obj );
                obj = null;
            }
            try
            {
                return ((Paytemp[]) (list.ToArray( typeof( Paytemp ) )));
            }
            catch (SqlException)
            {
                return null;
            }

        }
        
        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	    {

            if (m_clientId == 0 && GlobalShared.user != null)
                m_clientId = (int) GlobalShared.user.Account.clientId;

            if (m_ID > 0)
		    {
			    string[] values = new string[] {m_UserID, m_BuildingNumber, m_TenantNumber, m_Date.ToString(), m_Amount.ToString(), m_Code, m_Name, m_Apartment,m_clientId.ToString(),m_Notes,m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values))
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
			    string[] values = new string[] {m_UserID, m_BuildingNumber, m_TenantNumber, m_Date.ToString(), m_Amount.ToString(), m_Code, m_Name, m_Apartment,m_clientId.ToString(),m_Notes};
			    m_ID = this.insertRecord(values);
			    if (m_ID >= 0)
			    {
				    return true;
			    }
		    }
		    return false;
		
	    }
	
	    public static bool delete(long seqnum)
        {
            Paytemp obj = new Paytemp();
            string[] opts = { seqnum.ToString() };
            string sql = "DELETE FROM " + TABLE + " WHERE " + SQL_COL_USER_I_D + " = ?";
            return obj.query(sql, opts);
        }
	    public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
	    {
		
		    try
		    {
			    CN.Open();
			    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
			    cmd.CommandType = CommandType.Text;
			    cmd.CommandText = "CREATE TABLE Paytemp (UserID TEXT(18),Building Number TEXT(3),Tenant Number TEXT(4),Date DATETIME,Amount CURRENCY,Code TEXT(1),Name TEXT(30),Apartment TEXT(4))";
			    cmd.Connection = CN;
			    cmd.ExecuteNonQuery();
		    }
		    catch (SqlException)
		    {
			    return false;
		    }
		    finally
		    {
			    try
			    {
				    CN.Close();
			    }
			    catch
			    {
			    }
		    }
		
		    return true;
		
	    }
	
	    private void OnPropertyChanged(string info)
	    {
		    if (PropertyChangedEvent != null)
			    PropertyChangedEvent(this, new PropertyChangedEventArgs(info));
		    m_isChanged = true;
	    }

        // Property Routines
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool isChanged
        {
		    get
		    {
			    return m_isChanged;
		    }
		    set
		    {
			    m_isChanged = System.Convert.ToBoolean(value);
		    }
	    }
	    public string UserID
	    {
		    get
		    {
			    return m_UserID;
		    }
		    set
		    {
			    m_UserID = value;
			    OnPropertyChanged("UserID");
		    }
	    }
	
	    public string BuildingNumber
	    {
		    get
		    {
			    return m_BuildingNumber;
		    }
		    set
		    {
			    m_BuildingNumber = value;
			    OnPropertyChanged("BuildingNumber");
		    }
	    }
	
	    public string TenantNumber
	    {
		    get
		    {
			    return m_TenantNumber;
		    }
		    set
		    {
			    m_TenantNumber = value;
			    OnPropertyChanged("TenantNumber");
		    }
	    }
	
	    public DateTime Date
	    {
		    get
		    {
			    return m_Date;
		    }
		    set
		    {
			    m_Date = value;
			    OnPropertyChanged("Date");
		    }
	    }
	
	    public decimal Amount
	    {
		    get
		    {
			    return m_Amount;
		    }
		    set
		    {
			    m_Amount = value;
			    OnPropertyChanged("Amount");
		    }
	    }
	
	    public string Code
	    {
		    get
		    {
			    return m_Code;
		    }
		    set
		    {
			    m_Code = value;
			    OnPropertyChanged("Code");
		    }
	    }
	
	    public string Name
	    {
		    get
		    {
			    return m_Name;
		    }
		    set
		    {
			    m_Name = value;
			    OnPropertyChanged("Name");
		    }
	    }
	
	    public string Apartment
	    {
		    get
		    {
			    return m_Apartment;
		    }
		    set
		    {
			    m_Apartment = value;
			    OnPropertyChanged("Apartment");
		    }
	    }

        public string Notes
        {
            get
            {
                return m_Notes;
            }
            set
            {
                m_Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public int ClientId
        {
            get { return m_clientId; }
            set { m_clientId = value;
            OnPropertyChanged( "ClientId" );
            }
        }

        public int ID
        {
            get { return m_ID; }
        }

        // Register only properties
        public string Description
        {
            get { return m_Description; }
        }
        public string Address1
        {
            get { return m_Address1; }
        }

		// DataTable for those who need it
		public DataTable dataTable
        {
			get { return m_dataTable; }
        }
    }

}