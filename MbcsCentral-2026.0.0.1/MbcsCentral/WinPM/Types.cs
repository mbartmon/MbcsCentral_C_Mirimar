using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM {
    public class Types : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    static string TABLE = "pm_Types";

	    public const string SQL_COL_PRINT_BILL = "Print Bill";
	    public const string SQL_COL_LEASE = "Lease";
	    public const string SQL_COL_PRINT_APT = "printApt";
	    public const string SQL_COL_CLIENT_ID = "clientId";
	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_TYPE = "Type";
	    public const string SQL_COL_DESCRIPTION = "Description";
	    public const string SQL_COL_BILLING_NAME = "Billing Name";
        public const string SQL_COL_LEASE_NAME = "LeaseName";
        public const string SQL_COL_LEASE_REVERSE = "LeaseReverse";
        public const string SQL_COL_USE_DC = "DecontrolledLegal";


        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_PRINT_BILL + "]," +
                "[" + SQL_COL_LEASE + "]," +
                "[" + SQL_COL_PRINT_APT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_BILLING_NAME + "]," +
                "[" + SQL_COL_LEASE_NAME + "]," +
                "[" + SQL_COL_USE_DC + "]," +
                "[" + SQL_COL_LEASE_REVERSE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_PRINT_BILL + "]," +
                "[" + SQL_COL_LEASE + "]," +
                "[" + SQL_COL_PRINT_APT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_BILLING_NAME + "]," +
                "[" + SQL_COL_LEASE_NAME + "]," +
                "[" + SQL_COL_USE_DC + "]," +
                "[" + SQL_COL_LEASE_REVERSE + "]" +
                " FROM " + TABLE + ";";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_PRINT_BILL + "]," +
                "[" + SQL_COL_LEASE + "]," +
                "[" + SQL_COL_PRINT_APT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_BILLING_NAME + "]," +
                "[" + SQL_COL_LEASE_NAME + "]," +
                "[" + SQL_COL_USE_DC + "]," +
                "[" + SQL_COL_LEASE_REVERSE + "]" +
                ") VALUES (?, ?, ?, ?, '?', '?', '?', '?', ?, '?');";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_PRINT_BILL + "]=?," +
                "[" + SQL_COL_LEASE + "]=?," +
                "[" + SQL_COL_PRINT_APT + "]=?," +
                "[" + SQL_COL_CLIENT_ID + "]=?," +
                "[" + SQL_COL_TYPE + "]='?'," +
                "[" + SQL_COL_DESCRIPTION + "]='?'," +
                "[" + SQL_COL_BILLING_NAME + "]='?'," +
                "[" + SQL_COL_LEASE_NAME + "]='?'," +
                "[" + SQL_COL_USE_DC + "]=?," +
                "[" + SQL_COL_LEASE_REVERSE + "]='?'" +
                " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_BY_TYPE = "SELECT " +
                "[" + SQL_COL_PRINT_BILL + "]," +
                "[" + SQL_COL_LEASE + "]," +
                "[" + SQL_COL_PRINT_APT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_BILLING_NAME + "]," +
                "[" + SQL_COL_LEASE_NAME + "]," +
                "[" + SQL_COL_USE_DC + "]," +
                "[" + SQL_COL_LEASE_REVERSE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_TYPE + "] = '?';";

	    protected bool m_PrintBill;
	    protected bool m_Lease;
	    protected bool m_printApt;
	    protected long m_clientId;
	    protected long m_ID;
	    protected string m_Type;
	    protected string m_Description;
	    protected string m_BillingName;
	    protected bool m_isChanged;
        protected string m_LeaseName;
        protected string m_LeaseReverse;
        protected bool m_deleteMe;
        protected bool m_UseDC;

	    public Types() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

        public Types(bool PrintBill, bool Lease, bool printApt, long clientId, long ID, string Type, string Description, string BillingName, string LeaseName, string LeaseReverse, bool UseDC) :
            base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
		    m_PrintBill = PrintBill;
		    m_Lease = Lease;
		    m_printApt = printApt;
		    m_clientId = clientId;
		    m_ID = ID;
		    m_Type = Type;
		    m_Description = Description;
		    m_BillingName = BillingName;
            m_LeaseName = LeaseName;
            m_LeaseReverse = LeaseReverse;
        }

	    protected Types fill(DataRow dr)
	    {
		    try {
			    m_PrintBill = (bool) dr[SQL_COL_PRINT_BILL];
			    m_Lease = (bool) (Information.IsDBNull(dr[SQL_COL_LEASE]) ? false : dr[SQL_COL_LEASE]);
			    m_printApt = (bool) (Information.IsDBNull(dr[SQL_COL_PRINT_APT]) ? true : dr[SQL_COL_PRINT_APT]);
			    m_clientId = (int) dr[SQL_COL_CLIENT_ID];
			    m_ID = (int) dr[SQL_COL_I_D];
			    m_Type = (string) (Convert.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			    m_BillingName = (string) (Convert.IsDBNull(dr[SQL_COL_BILLING_NAME]) ? "" : dr[SQL_COL_BILLING_NAME]);
                m_LeaseName = (string)(Convert.IsDBNull(dr[SQL_COL_LEASE_NAME]) ? "" : dr[SQL_COL_LEASE_NAME]);
                m_LeaseReverse = (string)(Convert.IsDBNull(dr[SQL_COL_LEASE_REVERSE]) ? "" : dr[SQL_COL_LEASE_REVERSE]);
                m_UseDC = (bool)(Information.IsDBNull(dr[SQL_COL_USE_DC]) ? false : dr[SQL_COL_USE_DC]);
            }
            catch (SqlException ex)
            {
		    }
		    return this;

	    }

	    public static Types load( long clientId, string type)
	    {
		    Types obj  = new Types();
		    string[] opt = { type};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD_BY_TYPE, opt);
		    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Types[] loadAll(long clientId = 1, bool IncludeAll = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Types obj = new Types();
            string[] opt = { clientId.ToString() };

		    ds = obj.load(SQL_LOAD_ALL,opt);
		    if (ds == null || ds.Tables.Count == 0)
			    return null;
		    obj = null;
            if (IncludeAll)
                list.Add( new Types(false,false,false,clientId,0,"","<ALL>","","",  "", false));
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Types();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Types[])list.ToArray(typeof(Types));
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
		    if (m_ID > 0 ) {
                string[] values = { m_PrintBill.ToString(), m_Lease.ToString(), m_printApt.ToString(), m_clientId.ToString(), m_Type, m_Description, m_BillingName, m_LeaseName, m_UseDC.ToString(), m_LeaseReverse, m_ID.ToString() };
                if (this.updateRecord(m_ID.ToString(), values))
                {
                    m_isChanged = false;
				    return true;
			    } else {
				    return false;
			    }
		    } else {
                string[] values = { m_PrintBill.ToString(), m_Lease.ToString(), m_printApt.ToString(), m_clientId.ToString(), m_Type, m_Description, m_BillingName, m_LeaseName, m_UseDC.ToString(), m_LeaseReverse };
                m_ID = this.insertRecord(values);
			    if (m_ID >= 0) {
                    m_isChanged = false;
				    return true;
			    }
		    }
		    return false;

	    }

	    private void OnPropertyChanged(string info) {
		    if (PropertyChanged != null) {
			    PropertyChanged(this, new PropertyChangedEventArgs(info));
                if (info != "IsChangedF")
			        m_isChanged = true;
		    }
	    }

        // Properties
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            set { m_isChanged = value;
            if (value)
                OnPropertyChanged("IsChanged");
            else
                OnPropertyChanged("IsChangedF");
            }
            get { return m_isChanged; }
        }

	    public bool PrintBill {
		    set{ m_PrintBill = value;
		    OnPropertyChanged("PrintBill"); }
		    get { return m_PrintBill; }
	    }

	    public bool Lease {
		    set{ m_Lease = value;
		    OnPropertyChanged("Lease"); }
		    get { return m_Lease; }
	    }

	    public bool printApt {
		    set{ m_printApt = value;
		    OnPropertyChanged("printApt"); }
		    get { return m_printApt; }
	    }

	    public long clientId {
		    set{ m_clientId = value;
		    OnPropertyChanged("clientId"); }
		    get { return m_clientId; }
	    }

	    public long ID {
		    get { return m_ID; }
	    }

	    public string Type {
		    set{ m_Type = value;
		    OnPropertyChanged("Type"); }
		    get { return m_Type; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

	    public string BillingName {
		    set{ m_BillingName = value;
		    OnPropertyChanged("BillingName"); }
		    get { return m_BillingName; }
	    }

        public string LeaseName
        {
            set
            {
                m_LeaseName = value;
                OnPropertyChanged("LeaseName");
            }
            get { return m_LeaseName; }
        }

        public string LeaseReverse
        {
            set
            {
                m_LeaseReverse = value;
                OnPropertyChanged("LeaseReverse");
            }
            get { return m_LeaseReverse; }
        }
    
        public bool DeleteMe
        {
            set {m_deleteMe = value;
            OnPropertyChanged("DeleteMe");
            }
            get{return m_deleteMe;}
        }

        public string DisplayDescription
        {
            get { return (!m_Type.Trim().Equals("") ? m_Type.PadLeft(2) + ": " : "") + m_Description.Trim(); } 
        }

        public bool UseDC
        {
            set { m_UseDC = value;
                OnPropertyChanged("UseDC"); }
            get { return m_UseDC; }
        }
    }
}
