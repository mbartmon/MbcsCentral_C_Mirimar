using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    public class STTenants : clsDB, INotifyPropertyChanged
    {

        private PropertyChangedEventHandler PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Combine( PropertyChangedEvent, value );
            }
            remove
            {
                PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Remove( PropertyChangedEvent, value );
            }
        }


        static string TABLE = "pm_STTenants";

        public const string SQL_COL_BUILDING_NUMBER = "Building Number";
        public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
        public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
        public const string SQL_COL_APARTMENT = "Apartment";
        public const string SQL_COL_TENANT_NAME = "Tenant Name";
        public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_SOCIAL_SECURITY_ONE = "Social Security One";
        public const string SQL_COL_SOCIAL_SECURITY_TWO = "Social Security Two";
        public const string SQL_COL_INTEREST = "Interest";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_ID = "ID";

        static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                "[" + SQL_COL_APARTMENT + "]," + 
                                "[" + SQL_COL_TENANT_NAME + "]," + 
                                "[" + SQL_COL_AMOUNT + "]," + 
                                "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]," + 
                                "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]," + 
                                "[" + SQL_COL_INTEREST + "]," + 
                                "[" + SQL_COL_CLIENT_ID +"]," +
                                "[" + SQL_COL_ID + "]" + 
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_ID + " = ?;";

        static string SQL_LOAD_ALL = " SELECT " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_APARTMENT + "]," + 
                                    "[" + SQL_COL_TENANT_NAME + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]," + 
                                    "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]," + 
                                    "[" + SQL_COL_INTEREST + "]," +
                                    "[" + SQL_COL_CLIENT_ID + "]," +
                                    "[" + SQL_COL_ID + "]" +
                                    " FROM " + TABLE + ";";

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_APARTMENT + "]," + 
                                    "[" + SQL_COL_TENANT_NAME + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]," + 
                                    "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]," + 
                                    "[" + SQL_COL_INTEREST + "]," +
                                    "[" + SQL_COL_CLIENT_ID + "]" +
                                    ") VALUES (\'?\', \'?\', ?, \'?\', \'?\', ?, \'?\', \'?\', \'?\', ?);";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " +
                                    "[" + SQL_COL_BUILDING_NUMBER + "]=\'?\'," +
                                    "[" + SQL_COL_TENANT_NUMBER + "]=\'?\'," +
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
                                    "[" + SQL_COL_APARTMENT + "]=\'?\'," +
                                    "[" + SQL_COL_TENANT_NAME + "]=\'?\'," +
                                    "[" + SQL_COL_AMOUNT + "]=?," +
                                    "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]=\'?\'," +
                                    "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]=\'?\'," +
                                    "[" + SQL_COL_INTEREST + "]='?'," +
                                    "[" + SQL_COL_CLIENT_ID + "]=?" +
                                   " WHERE " + SQL_COL_ID + " = ?;";

        static string SQL_LOAD_BY_SEQUENCE = " SELECT " +
                              "[" + SQL_COL_BUILDING_NUMBER + "]," +
                              "[" + SQL_COL_TENANT_NUMBER + "]," +
                              "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                              "[" + SQL_COL_APARTMENT + "]," +
                              "[" + SQL_COL_TENANT_NAME + "]," +
                              "[" + SQL_COL_AMOUNT + "]," +
                              "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]," +
                              "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]," +
                              "[" + SQL_COL_INTEREST + "]," +
                              "[" + SQL_COL_CLIENT_ID + "]," +
                              "[" + SQL_COL_ID + "]" +
                              " FROM " + TABLE +
                              " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?;";

        static string SQL_LOAD_BY_TENANT = " SELECT " +
                              "[" + SQL_COL_BUILDING_NUMBER + "]," +
                              "[" + SQL_COL_TENANT_NUMBER + "]," +
                              "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                              "[" + SQL_COL_APARTMENT + "]," +
                              "[" + SQL_COL_TENANT_NAME + "]," +
                              "[" + SQL_COL_AMOUNT + "]," +
                              "[" + SQL_COL_SOCIAL_SECURITY_ONE + "]," +
                              "[" + SQL_COL_SOCIAL_SECURITY_TWO + "]," +
                              "[" + SQL_COL_INTEREST + "]," +
                              "[" + SQL_COL_CLIENT_ID + "]," +
                              "[" + SQL_COL_ID + "]" +
                              " FROM " + TABLE +
                              " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" +
                              " AND [" + SQL_COL_TENANT_NUMBER + "] = '?'" +
                              " ORDER BY [" + SQL_COL_SEQUENCE_NUMBER + "] DESC";
                                                                   
        protected string m_BuildingNumber;
        protected string m_TenantNumber;
        protected int m_SequenceNumber;
        protected string m_Apartment;
        protected string m_TenantName;
        protected decimal m_Amount;
        protected string m_SocialSecurityOne;
        protected string m_SocialSecurityTwo;
        protected string m_Interest;
        protected long m_clientId;
        protected int m_ID;
        protected bool m_isChanged;

        public STTenants()
            : base( TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
        }

        public STTenants( string BuildingNumber, string TenantNumber, int SequenceNumber, string Apartment, string TenantName, decimal Amount, string SocialSecurityOne, string SocialSecurityTwo, string Interest, long clientId = 0 )
            : base( TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
            m_BuildingNumber = BuildingNumber;
            m_TenantNumber = TenantNumber;
            m_SequenceNumber = SequenceNumber;
            m_Apartment = Apartment;
            m_TenantName = TenantName;
            m_Amount = Amount;
            m_SocialSecurityOne = SocialSecurityOne;
            m_SocialSecurityTwo = SocialSecurityTwo;
            m_Interest = Interest;
            if (clientId == 0 && GlobalShared.user != null)
                m_clientId = (int) GlobalShared.user.Account.clientId;
            else
                m_clientId = clientId;
        }

        protected STTenants fill( DataRow dr )
        {

            try
            {
                m_BuildingNumber = (string) ((Information.IsDBNull( dr[ SQL_COL_BUILDING_NUMBER ] )) ? "" : (dr[ SQL_COL_BUILDING_NUMBER ]));
                m_TenantNumber = (string) ((Information.IsDBNull( dr[ SQL_COL_TENANT_NUMBER ] )) ? "" : (dr[ SQL_COL_TENANT_NUMBER ]));
                m_SequenceNumber = (int) dr[ SQL_COL_SEQUENCE_NUMBER ] ;
                m_Apartment = (string) ((Information.IsDBNull( dr[ SQL_COL_APARTMENT ] )) ? "" : (dr[ SQL_COL_APARTMENT ]));
                m_TenantName = (string) ((Information.IsDBNull( dr[ SQL_COL_TENANT_NAME ] )) ? "" : (dr[ SQL_COL_TENANT_NAME ]));
                m_Amount = System.Convert.ToDecimal( dr[ SQL_COL_AMOUNT ] );
                m_SocialSecurityOne = (string) ((Information.IsDBNull( dr[ SQL_COL_SOCIAL_SECURITY_ONE ] )) ? "" : (dr[ SQL_COL_SOCIAL_SECURITY_ONE ]));
                m_SocialSecurityTwo = (string) ((Information.IsDBNull( dr[ SQL_COL_SOCIAL_SECURITY_TWO ] )) ? "" : (dr[ SQL_COL_SOCIAL_SECURITY_TWO ]));
                m_Interest = (string) ((Information.IsDBNull( dr[ SQL_COL_INTEREST ] )) ? "" : (dr[ SQL_COL_INTEREST ]));
                m_ID = (int)(dr[SQL_COL_ID]);
                m_clientId = (int)(dr[SQL_COL_CLIENT_ID]);
            }
            catch (SqlException)
            {
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static STTenants load( int id )
        {

            STTenants obj = new STTenants();
            string[] opt = new string[] {  id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load( SQL_LOAD, opt );
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill( dr );

        }

        public static STTenants loadBySequence( int id )
        {

            STTenants obj = new STTenants();
            string[] opt = new string[] { id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load( SQL_LOAD_BY_SEQUENCE , opt );
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill( dr );

        }

        public static STTenants[] loadByTenant(string buildingNumber, string tenantNumber)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            STTenants obj = new STTenants();
            string[] opts = { buildingNumber, tenantNumber };

            ds = obj.load(SQL_LOAD_BY_TENANT, opts);
            obj = null;

            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new STTenants();
                obj.fill( dr );
                list.Add( obj );
                obj = null;
            }
            try
            {
                return ((STTenants[]) (list.ToArray( typeof( STTenants ) )));
            }
            catch (SqlException)
            {
                return null;
            }

        }
        public static STTenants[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            STTenants obj = new STTenants();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new STTenants();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((STTenants[])(list.ToArray(typeof(STTenants))));
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

            if (m_ID > 0)
            {
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_Apartment, m_TenantName, m_Amount.ToString(), m_SocialSecurityOne, m_SocialSecurityTwo, m_Interest,m_clientId.ToString(), m_ID.ToString() };
                if (this.updateRecord( m_BuildingNumber, values ))
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
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_Apartment, m_TenantName, m_Amount.ToString(), m_SocialSecurityOne, m_SocialSecurityTwo, m_Interest, m_clientId.ToString() };
                m_ID = (int) this.insertRecord( values );
                if (m_ID >= 0)
                {
                    return true;
                }
            }
            return false;

        }


        public static bool CreateTable( System.Data.SqlClient.SqlConnection CN )
        {

            try
            {
                CN.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE STTenants (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Apartment TEXT(4),Tenant Name TEXT(64),Amount CURRENCY,Social Security One TEXT(11),Social Security Two TEXT(11),Interest TEXT(1))";
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

        private void OnPropertyChanged( string info )
        {
            if (PropertyChangedEvent != null)
                PropertyChangedEvent( this, new PropertyChangedEventArgs( info ) );
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
                m_isChanged = System.Convert.ToBoolean( value );
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
                OnPropertyChanged( "BuildingNumber" );
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
                OnPropertyChanged( "TenantNumber" );
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
                OnPropertyChanged( "SequenceNumber" );
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
                OnPropertyChanged( "Apartment" );
            }
        }

        public string TenantName
        {
            get
            {
                return m_TenantName;
            }
            set
            {
                m_TenantName = value;
                OnPropertyChanged( "TenantName" );
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
                OnPropertyChanged( "Amount" );
            }
        }

        public string SocialSecurityOne
        {
            get
            {
                return m_SocialSecurityOne;
            }
            set
            {
                m_SocialSecurityOne = value;
                OnPropertyChanged( "SocialSecurityOne" );
            }
        }

        public string SocialSecurityTwo
        {
            get
            {
                return m_SocialSecurityTwo;
            }
            set
            {
                m_SocialSecurityTwo = value;
                OnPropertyChanged( "SocialSecurityTwo" );
            }
        }

        public string Interest
        {
            get
            {
                return m_Interest;
            }
            set
            {
                m_Interest = value;
                OnPropertyChanged( "Interest" );
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
    }
}