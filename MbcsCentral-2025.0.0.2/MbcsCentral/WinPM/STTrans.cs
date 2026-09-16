using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    public class STTrans : clsDB, INotifyPropertyChanged
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
	
	
	    public static string TABLE = "pm_STTrans";
	
	    public const string SQL_COL_BUILDING_NUMBER = "Building Number";
	    public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
	    public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
	    public const string SQL_COL_TRANS_TYPE = "Trans Type";
	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_ID = "ID";
	
	    static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                "[" + SQL_COL_TRANS_TYPE + "]," + 
                                "[" + SQL_COL_DATE + "]," + 
                                "[" + SQL_COL_AMOUNT + "]," + 
                                "[" + SQL_COL_CLIENT_ID + "]," +
                                "[" + SQL_COL_ID + "]" +
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_ID + "=?";

	    static string SQL_LOAD_ALL = " SELECT " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_TRANS_TYPE + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_CLIENT_ID +"]," +
                                    "[" + SQL_COL_ID + "]" +
                                    " FROM " + TABLE + 
                                    " ORDER BY [" + SQL_COL_DATE + "]";
	
	    static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," +         
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_TRANS_TYPE + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_CLIENT_ID + "]" +
                                   ") VALUES (\'?\', \'?\', ?, ?, '?', ?, ?);";
	
	    static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]=\'?\'," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]=\'?\'," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," + 
                                    "[" + SQL_COL_TRANS_TYPE + "]=?," + 
                                    "[" + SQL_COL_DATE + "]='?'," + 
                                    "[" + SQL_COL_AMOUNT + "]=?," + 
                                    "[" + SQL_COL_CLIENT_ID + "]=?" +
                                    " WHERE " + SQL_COL_ID + "=?";

        static string SQL_LOAD_BY_SEQUENCE = " SELECT " +
                                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                "[" + SQL_COL_TENANT_NUMBER + "]," +
                                "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                "[" + SQL_COL_TRANS_TYPE + "]," +
                                "[" + SQL_COL_DATE + "]," +
                                "[" + SQL_COL_AMOUNT + "]," +
                                "[" + SQL_COL_CLIENT_ID + "]," +
                                "[" + SQL_COL_ID + "]" +
                                " FROM " + TABLE +
                                " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?" +
                                " ORDER BY [" + SQL_COL_DATE + "]";

        protected string m_BuildingNumber;
	    protected string m_TenantNumber;
	    protected int m_SequenceNumber;
	    protected int m_TransType;
	    protected DateTime m_Date;
	    protected decimal m_Amount;
        protected long m_clientId;
        protected int m_ID;
	    protected bool m_isChanged;

        protected string m_Description;
	
	    public STTrans() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }
	
	    public STTrans(string BuildingNumber, string TenantNumber, int SequenceNumber, int TransType, DateTime Date, decimal Amount, long clientId) : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_BuildingNumber = BuildingNumber;
		    m_TenantNumber = TenantNumber;
		    m_SequenceNumber = SequenceNumber;
		    m_TransType = TransType;
		    m_Date = Date;
		    m_Amount = Amount;
            m_clientId = clientId;
	    }
	
	    protected STTrans fill(DataRow dr)
	    {
		
		    try
		    {
			    m_BuildingNumber = (string) ((Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER])) ? "" : (dr[SQL_COL_BUILDING_NUMBER]));
			    m_TenantNumber = (string) ((Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER])) ? "" : (dr[SQL_COL_TENANT_NUMBER]));
			    m_SequenceNumber = (int) dr[SQL_COL_SEQUENCE_NUMBER];
			    m_TransType = System.Convert.ToInt32(dr[SQL_COL_TRANS_TYPE]);
			    m_Date = (DateTime)dr[SQL_COL_DATE];
			    m_Amount = System.Convert.ToDecimal(dr[SQL_COL_AMOUNT]);
                //m_clientId = (int) dr[SQL_COL_CLIENT_ID];
                m_ID = (int) dr[SQL_COL_ID];
                GetDescription();
		    }
		    catch (SqlException)
		    {
		    }
		    catch (System.Exception)
		    {
		    }
		    return this;
		
	    }
	
	    public static STTrans load(int id)
	    {
		
		    STTrans obj = new STTrans();
		    string[] opt = new string[] {id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    DataRow dr = ds.Tables[0].Rows[0];
		
		    return obj.fill(dr);
		
	    }

        public static STTrans[] loadBySequence( int seqNum )
        {

            ArrayList list = new ArrayList();
            STTrans obj = new STTrans();
            string[] opt = new string[] { seqNum.ToString() };
            DataSet ds = new DataSet();
            if (ds != null )
            {
                ds = obj.load( SQL_LOAD_BY_SEQUENCE, opt );
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                foreach (DataRow dr in ds.Tables[0].Rows) 
                { 
                    obj = new STTrans();
                    obj.fill( dr );
                    list.Add(obj);
                    obj = null;
                }
            }
            try
            {
  			    return ((STTrans[]) (list.ToArray(typeof(STTrans))));
            }
            catch (OleDbException)
            {
                    return null;
            }
        }

        public static ArrayList loadBySequenceList(int seqNum)
        {
            ArrayList list = new ArrayList();
            STTrans obj = new STTrans();
            string[] opt = new string[] { seqNum.ToString() };
            DataSet ds = new DataSet();
            if (ds != null)
            {
                ds = obj.load(SQL_LOAD_BY_SEQUENCE, opt);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new STTrans();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
            }
            try
            {
                return list;
            }
            catch (Exception)
            {
                return null;
            }

        }

	    public static STTrans[] load()
	    {

            ArrayList list = new ArrayList();
		
		    DataSet ds = new DataSet();
		    STTrans obj = new STTrans();
		
		    ds = obj.loadAll();
		    obj = null;
		
		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
			    obj = new STTrans();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try
		    {
			    return ((STTrans[]) (list.ToArray(typeof(STTrans))));
		    }
		    catch (OleDbException)
		    {
			    return null;
		    }
		
	    }

	    protected  void GetDescription()
        {
            STCodes code = STCodes.load(m_TransType);
            if (code == null)
            {
                m_Description = m_TransType.ToString();
            }
            else
                m_Description = code.Description;
            code = null;
        }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	    {
		
		    if (m_ID> 0)
		    {
			    string[] values = new string[] {m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_TransType.ToString(), m_Date.ToString(), m_Amount.ToString(), m_clientId.ToString(), m_ID.ToString()};
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
			    string[] values = new string[] {m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_TransType.ToString(), m_Date.ToString(), m_Amount.ToString(), m_clientId.ToString()};
                m_ID = (int)this.insertRecord(values);
			    if (m_ID >= 0)
			    {
				    return true;
			    }
		    }
		    return false;
		
	    }

        public static bool delete(int id)
        {
            STTrans tran = new STTrans();
            return tran.delete(id.ToString());
        }
	
	
	    public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
	    {
		
		    try
		    {
			    CN.Open();
			    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
			    cmd.CommandType = CommandType.Text;
			    cmd.CommandText = "CREATE TABLE STTrans (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Trans Type LONG,Date DATETIME,Amount CURRENCY)";
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
	
	    public int SequenceNumber
	    {
		    get
		    {
			    return m_SequenceNumber;
		    }
		    set
		    {
			    m_SequenceNumber = value;
			    OnPropertyChanged("SequenceNumber");
		    }
	    }
	
	    public int TransType
	    {
		    get
		    {
			    return m_TransType;
		    }
		    set
		    {
			    m_TransType = value;
			    OnPropertyChanged("TransType");
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

        public long ClientId
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

        public string Description
        {
            get
            {
                return m_Description;
            }
            set 
            {
                m_Description = value;
                OnPropertyChanged("Description");
            }
        }

}
}