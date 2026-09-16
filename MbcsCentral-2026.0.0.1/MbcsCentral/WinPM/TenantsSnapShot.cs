using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinPM {
public class TenantsSnapShot : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public const string TABLE = "pm_TenantsSnapShot";

	public const string SQL_COL_LAST_PAID_DATE = "Last Paid Date";
	public const string SQL_COL_YEAR = "Year";
	public const string SQL_COL_MONTH = "Month";
	public const string SQL_COL_RENT = "Rent";
	public const string SQL_COL_SECURITY = "Security";
	public const string SQL_COL_ARREARS = "Arrears";
	public const string SQL_COL_PAID = "Paid";
	public const string SQL_COL_TOTAL_CHARGES = "Total Charges";
	public const string SQL_COL_RENT_LEGAL = "RentLegal";
	public const string SQL_COL_CLIENT_ID = "ClientId";
	public const string SQL_COL_I_D = "ID";
	public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
	public const string SQL_COL_BUILDING_NUMBER = "Building Number";
	public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
	public const string SQL_COL_TENANT_NAME = "Tenant Name";
	public const string SQL_COL_APARTMENT = "Apartment";
    public const string SQL_COL_DECONTROLLEDLEGAL = "DecontrolledLegal";

	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_LAST_PAID_DATE + "]," +
			"[" + SQL_COL_YEAR + "]," +
			"[" + SQL_COL_MONTH + "]," +
			"[" + SQL_COL_RENT + "]," +
			"[" + SQL_COL_SECURITY + "]," +
			"[" + SQL_COL_ARREARS + "]," +
			"[" + SQL_COL_PAID + "]," +
			"[" + SQL_COL_TOTAL_CHARGES + "]," +
			"[" + SQL_COL_RENT_LEGAL + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NAME + "]," +
			"[" + SQL_COL_APARTMENT + "]," +
            "[" + SQL_COL_DECONTROLLEDLEGAL + "]" +
			" FROM " + TABLE +
			" WHERE " + SQL_COL_I_D + " = ?;";

	static string SQL_LOAD_ALL = "SELECT " +
			"[" + SQL_COL_LAST_PAID_DATE + "]," +
			"[" + SQL_COL_YEAR + "]," +
			"[" + SQL_COL_MONTH + "]," +
			"[" + SQL_COL_RENT + "]," +
			"[" + SQL_COL_SECURITY + "]," +
			"[" + SQL_COL_ARREARS + "]," +
			"[" + SQL_COL_PAID + "]," +
			"[" + SQL_COL_TOTAL_CHARGES + "]," +
			"[" + SQL_COL_RENT_LEGAL + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NAME + "]," +
			"[" + SQL_COL_APARTMENT + "]," +
            "[" + SQL_COL_DECONTROLLEDLEGAL + "]" +
            " FROM " + TABLE + ";";

	static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			"[" + SQL_COL_LAST_PAID_DATE + "]," +
			"[" + SQL_COL_YEAR + "]," +
			"[" + SQL_COL_MONTH + "]," +
			"[" + SQL_COL_RENT + "]," +
			"[" + SQL_COL_SECURITY + "]," +
			"[" + SQL_COL_ARREARS + "]," +
			"[" + SQL_COL_PAID + "]," +
			"[" + SQL_COL_TOTAL_CHARGES + "]," +
			"[" + SQL_COL_RENT_LEGAL + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NUMBER + "]," +
			"[" + SQL_COL_TENANT_NAME + "]," +
			"[" + SQL_COL_APARTMENT + "]," +
            "[" + SQL_COL_DECONTROLLEDLEGAL + "]" +
            ") VALUES ('?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', ?);";

	static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			"[" + SQL_COL_LAST_PAID_DATE + "]='?'," +
			"[" + SQL_COL_YEAR + "]=?," +
			"[" + SQL_COL_MONTH + "]=?," +
			"[" + SQL_COL_RENT + "]=?," +
			"[" + SQL_COL_SECURITY + "]=?," +
			"[" + SQL_COL_ARREARS + "]=?," +
			"[" + SQL_COL_PAID + "]=?," +
			"[" + SQL_COL_TOTAL_CHARGES + "]=?," +
			"[" + SQL_COL_RENT_LEGAL + "]=?," +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
			"[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
			"[" + SQL_COL_TENANT_NUMBER + "]='?'," +
			"[" + SQL_COL_TENANT_NAME + "]='?'," +
			"[" + SQL_COL_APARTMENT + "]='?'," +
            "[" + SQL_COL_DECONTROLLEDLEGAL + "] = ?" +
            " WHERE " + SQL_COL_I_D + " = ?;";

        static string SQL_LOAD_BY_MONTH_YEAR = "SELECT " +
                "[" + SQL_COL_LAST_PAID_DATE + "]," +
                "[" + SQL_COL_YEAR + "]," +
                "[" + SQL_COL_MONTH + "]," +
                "[" + SQL_COL_RENT + "]," +
                "[" + SQL_COL_SECURITY + "]," +
                "[" + SQL_COL_ARREARS + "]," +
                "[" + SQL_COL_PAID + "]," +
                "[" + SQL_COL_TOTAL_CHARGES + "]," +
                "[" + SQL_COL_RENT_LEGAL + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NAME + "]," +
                "[" + SQL_COL_APARTMENT + "]," +
                "[" + SQL_COL_DECONTROLLEDLEGAL + "]" +
                " FROM " + TABLE +
                " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?" +
                " AND [" + SQL_COL_MONTH + "] = ?" +
                " AND [" + SQL_COL_YEAR + "] = ?";

    protected DateTime m_LastPaidDate;
	protected int m_Year;
	protected int m_Month;
	protected decimal m_Rent;
	protected decimal m_Security;
	protected decimal m_Arrears;
	protected decimal m_Paid;
	protected decimal m_TotalCharges;
	protected decimal m_RentLegal;
	protected long m_clientId;
	protected int m_ID;
	protected int m_SequenceNumber;
	protected string m_BuildingNumber;
	protected string m_TenantNumber;
	protected string m_TenantName;
	protected string m_Apartment;
	protected bool m_isChanged;
    protected decimal m_DecontrolledLegal;

    public static string[] SnapShotColumns = { SQL_COL_LAST_PAID_DATE.Replace(" ", string.Empty), SQL_COL_RENT, SQL_COL_SECURITY, SQL_COL_ARREARS, SQL_COL_PAID, SQL_COL_TOTAL_CHARGES.Replace(" ", string.Empty), SQL_COL_RENT_LEGAL, SQL_COL_CLIENT_ID, SQL_COL_SEQUENCE_NUMBER.Replace(" ", string.Empty), SQL_COL_BUILDING_NUMBER.Replace(" ", string.Empty), SQL_COL_TENANT_NUMBER.Replace(" ", string.Empty), SQL_COL_TENANT_NAME.Replace(" ", string.Empty), SQL_COL_APARTMENT, SQL_COL_DECONTROLLEDLEGAL };

	public TenantsSnapShot() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public TenantsSnapShot(DateTime LastPaidDate,int Year,int Month,decimal Rent,decimal Security,decimal Arrears,decimal Paid,decimal TotalCharges,decimal RentLegal,long clientId,int ID,int SequenceNumber,string BuildingNumber,string TenantNumber,string TenantName,string Apartment, decimal DecontrolledLegal) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_LastPaidDate = LastPaidDate;
		m_Year = Year;
		m_Month = Month;
		m_Rent = Rent;
		m_Security = Security;
		m_Arrears = Arrears;
		m_Paid = Paid;
		m_TotalCharges = TotalCharges;
		m_RentLegal = RentLegal;
		m_clientId = clientId;
		m_ID = ID;
		m_SequenceNumber = SequenceNumber;
		m_BuildingNumber = BuildingNumber;
		m_TenantNumber = TenantNumber;
		m_TenantName = TenantName;
		m_Apartment = Apartment;
        m_DecontrolledLegal = DecontrolledLegal;
	}

	protected TenantsSnapShot fill(DataRow dr)
	{
		try {
			m_LastPaidDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_LAST_PAID_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_PAID_DATE]);
			m_Year = (int) dr[SQL_COL_YEAR];
			m_Month = (int) dr[SQL_COL_MONTH];
			m_Rent = (decimal) dr[SQL_COL_RENT];
			m_Security = (decimal) dr[SQL_COL_SECURITY];
			m_Arrears = (decimal) dr[SQL_COL_ARREARS];
			m_Paid = (decimal) dr[SQL_COL_PAID];
			m_TotalCharges = (decimal) dr[SQL_COL_TOTAL_CHARGES];
			m_RentLegal = (decimal) dr[SQL_COL_RENT_LEGAL];
			m_clientId = (int) dr[SQL_COL_CLIENT_ID];
			m_ID = (int) dr[SQL_COL_I_D];
			m_SequenceNumber = (int) dr[SQL_COL_SEQUENCE_NUMBER];
			m_BuildingNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
			m_TenantNumber = (string) (Convert.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
			m_TenantName = (string) (Convert.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
			m_Apartment = (string) (Convert.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
            m_DecontrolledLegal = (decimal)dr[SQL_COL_DECONTROLLEDLEGAL];
		} catch (SqlException ex) {
		}
		return this;

	}

	    public static TenantsSnapShot load( int m_ID)
	    {
		    TenantsSnapShot obj  = new TenantsSnapShot();
		    string[] opt = {m_ID.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null && ds.Tables[0].Rows.Count > 0) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

        public static TenantsSnapShot load(int sequence, int month, int year)
        {
            TenantsSnapShot obj = new TenantsSnapShot();
            string[] opt = { sequence.ToString(), month.ToString(), year.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_MONTH_YEAR, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                    DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public static TenantsSnapShot[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		TenantsSnapShot obj = new TenantsSnapShot();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new TenantsSnapShot();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (TenantsSnapShot[])list.ToArray(typeof(TenantsSnapShot));
		} catch (SqlException ex) {
			return null;
		}
	}

    public static void MakeSnapShots(DateTime clsDate)
    {
        int i;
        Tenants[] tenants = Tenants.load();
        if (tenants != null && tenants.Length != 0)
        {
                TenantsSnapShot snapshot = null;
                foreach (Tenants tenant in tenants)
            {
                snapshot = new TenantsSnapShot();
                snapshot.Year = clsDate.Year;
                snapshot.Month = clsDate.Month;
                for (i = 0; i < SnapShotColumns.Length; i++)
                {
                    if (Convert.IsDBNull( tenant[SnapShotColumns[i]]))
                        snapshot[SnapShotColumns[i]] = 0;
                    else
                        snapshot[SnapShotColumns[i]] = tenant[SnapShotColumns[i]];
                }
                snapshot.save();
                snapshot= null;
            }
        }    
    }

			/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	{
		if (m_ID > 0 ) {
			string[] values = {m_LastPaidDate.ToString(), m_Year.ToString(), m_Month.ToString(), m_Rent.ToString(), m_Security.ToString(), m_Arrears.ToString(), m_Paid.ToString(), m_TotalCharges.ToString(), m_RentLegal.ToString(), m_clientId.ToString(), m_SequenceNumber.ToString(), m_BuildingNumber, m_TenantNumber, m_TenantName, m_Apartment, m_DecontrolledLegal.ToString(), m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_LastPaidDate.ToString(), m_Year.ToString(), m_Month.ToString(), m_Rent.ToString(), m_Security.ToString(), m_Arrears.ToString(), m_Paid.ToString(), m_TotalCharges.ToString(), m_RentLegal.ToString(), m_clientId.ToString(), m_SequenceNumber.ToString(), m_BuildingNumber, m_TenantNumber, m_TenantName, m_Apartment, m_DecontrolledLegal.ToString()};
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
	public DateTime LastPaidDate {
		set{ m_LastPaidDate = value;
		OnPropertyChanged("LastPaidDate"); }
		get { return m_LastPaidDate; }
	}

	public int Year {
		set{ m_Year = value;
		OnPropertyChanged("Year"); }
		get { return m_Year; }
	}

	public int Month {
		set{ m_Month = value;
		OnPropertyChanged("Month"); }
		get { return m_Month; }
	}

	public decimal Rent {
		set{ m_Rent = value;
		OnPropertyChanged("Rent"); }
		get { return m_Rent; }
	}

	public decimal Security {
		set{ m_Security = value;
		OnPropertyChanged("Security"); }
		get { return m_Security; }
	}

	public decimal Arrears {
		set{ m_Arrears = value;
		OnPropertyChanged("Arrears"); }
		get { return m_Arrears; }
	}

	public decimal Paid {
		set{ m_Paid = value;
		OnPropertyChanged("Paid"); }
		get { return m_Paid; }
	}

	public decimal TotalCharges {
		set{ m_TotalCharges = value;
		OnPropertyChanged("TotalCharges"); }
		get { return m_TotalCharges; }
	}

	public decimal RentLegal {
		set{ m_RentLegal = value;
		OnPropertyChanged("RentLegal"); }
		get { return m_RentLegal; }
	}

	public long ClientId {
		set{ m_clientId = value;
		OnPropertyChanged("clientId"); }
		get { return m_clientId; }
	}

	public int ID {
		get { return m_ID; }
	}

	public int SequenceNumber {
		set{ m_SequenceNumber = value;
		OnPropertyChanged("SequenceNumber"); }
		get { return m_SequenceNumber; }
	}

	public string BuildingNumber {
		set{ m_BuildingNumber = value;
		OnPropertyChanged("BuildingNumber"); }
		get { return m_BuildingNumber; }
	}

	public string TenantNumber {
		set{ m_TenantNumber = value;
		OnPropertyChanged("TenantNumber"); }
		get { return m_TenantNumber; }
	}

	public string TenantName {
		set{ m_TenantName = value;
		OnPropertyChanged("TenantName"); }
		get { return m_TenantName; }
	}

	public string Apartment {
		set{ m_Apartment = value;
		OnPropertyChanged("Apartment"); }
		get { return m_Apartment; }
	}

    public decimal DecontrolledLegal
        {
            set { m_DecontrolledLegal = value;
                OnPropertyChanged("DecontrolledLegal"); }
            get { return m_DecontrolledLegal; }
        }
}
}
