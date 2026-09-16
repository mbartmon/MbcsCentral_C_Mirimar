using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using MbcsCentral;

namespace MbcsCentral.WinPM
{
    public class Payments : clsDB, INotifyPropertyChanged
    {

        private PropertyChangedEventHandler PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEvent = (PropertyChangedEventHandler)System.Delegate.Combine(PropertyChangedEvent, value);
            }
            remove
            {
                PropertyChangedEvent = (PropertyChangedEventHandler)System.Delegate.Remove(PropertyChangedEvent, value);
            }
        }


        static string TABLE = "pm_Payments";

        public const string SQL_COL_BUILDING_NUMBER = "Building Number";
        public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
        public const string SQL_COL_DATE_OF_ENTRY = "Date of Entry";
        public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_SECURITY_CODE = "Security Code";
        public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_ID = "ID";
        public const string SQL_COL_TOTAL_PAID = "TotalPaid";
        public const string SQL_COL_NOTES = "Notes";

        static string SQL_LOAD = "SELECT " +
                             "[" + SQL_COL_BUILDING_NUMBER + "]," +
                             "[" + SQL_COL_TENANT_NUMBER + "]," +
                             "[" + SQL_COL_DATE_OF_ENTRY + "]," +
                             "[" + SQL_COL_AMOUNT + "]," +
                             "[" + SQL_COL_SECURITY_CODE + "]," +
                             "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                             "[" + SQL_COL_ID + "]," +
                             SQL_COL_CLIENT_ID + "," +
                             "[" + SQL_COL_NOTES + "]" +
                             " FROM " + TABLE +
                             " WHERE " + SQL_COL_ID + "=?";

        static string SQL_LOAD_ALL = "SELECT " +
                                     "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                     "[" + SQL_COL_TENANT_NUMBER + "]," +
                                     "[" + SQL_COL_DATE_OF_ENTRY + "]," +
                                     "[" + SQL_COL_AMOUNT + "]," +
                                     "[" + SQL_COL_SECURITY_CODE + "]," +
                                     "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                     "[" + SQL_COL_ID + "]," +
                                     SQL_COL_CLIENT_ID + "," +
                                     "[" + SQL_COL_NOTES + "]" +
                                     " FROM " + TABLE + ";";

        static string SQL_LOAD_BY_SEQUENCE = "SELECT " +
                                             "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                             "[" + SQL_COL_TENANT_NUMBER + "]," +
                                             "[" + SQL_COL_DATE_OF_ENTRY + "]," +
                                             "[" + SQL_COL_AMOUNT + "]," +
                                             "[" + SQL_COL_SECURITY_CODE + "]," +
                                             "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                             "[" + SQL_COL_ID + "]," +
                                             SQL_COL_CLIENT_ID + "," +
                                             "[" + SQL_COL_NOTES + "]" +
                                             " FROM " + TABLE +
                                             " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?" +
                                             "ORDER BY " + SQL_COL_ID + " DESC";

        static string SQL_LOAD_TOTAL_PAID = "SELECT " +
                                            " SUM(" + SQL_COL_AMOUNT + ") AS " + SQL_COL_TOTAL_PAID +
                                            " FROM " + TABLE +
                                            " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]= ?";

        static string SQL_INSERT = "INSERT INTO " + TABLE +
                                   " (" + "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                   "[" + SQL_COL_TENANT_NUMBER + "]," +
                                   "[" + SQL_COL_DATE_OF_ENTRY + "]," +
                                   "[" + SQL_COL_AMOUNT + "]," +
                                   "[" + SQL_COL_SECURITY_CODE + "]," +
                                   "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                   SQL_COL_CLIENT_ID + "," +
                                   "[" + SQL_COL_NOTES + "]" +
                                   ") VALUES (\'?\', \'?\', '?', ?, \'?\', ?, ?,\'?\');";

        static string SQL_UPDATE = "UPDATE " + TABLE +
                                   " SET " +
                                   "[" + SQL_COL_BUILDING_NUMBER + "]=\'?\'," +
                                   "[" + SQL_COL_TENANT_NUMBER + "]=\'?\'," +
                                   "[" + SQL_COL_DATE_OF_ENTRY + "]='?'," +
                                   "[" + SQL_COL_AMOUNT + "]=?," +
                                   "[" + SQL_COL_SECURITY_CODE + "]=\'?\'," +
                                   "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
                                   SQL_COL_CLIENT_ID + "=?," +
                                  "[" + SQL_COL_NOTES + "]=\'?\'" +
                                   " WHERE " + SQL_COL_ID + "=?";

        static string SQL_LOAD_FOR_REGISTER = " SELECT " +
                    TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "]," +
                    TABLE + ".[" + SQL_COL_TENANT_NUMBER + "]," +
                    "[" + SQL_COL_DATE_OF_ENTRY + "]," +
                    "[" + SQL_COL_AMOUNT + "]," +
                    TABLE + ".[" + SQL_COL_SECURITY_CODE + "]," +
                    TABLE + ".[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                    TABLE + ".[" + SQL_COL_CLIENT_ID + "]," +
                    TABLE + ".[" + SQL_COL_NOTES + "]," +    
                    TABLE + ".[" + SQL_COL_ID + "]," +
                    Tenants.TABLE + ".[" + Tenants.SQL_COL_TENANT_NAME + "]," +
                    Tenants.TABLE + ".[" + Tenants.SQL_COL_APARTMENT + "]," +
                    Building.TABLE + ".[" + Building.SQL_COL_ADDRESS_1 + "]" +
                    " FROM " + TABLE + ", " +
                    Building.TABLE + ", " +
                    Tenants.TABLE +
                    " WHERE " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "] >= '?'" +
                    " AND " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "] <= '?'" +
                    " AND " + Building.TABLE + ".[" + Building.SQL_COL_BUILDING_NUMBER + "] = " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "]" +
                    " AND [" + SQL_COL_DATE_OF_ENTRY + "] BETWEEN '?'  AND '?'" +
                    " AND " + Tenants.TABLE + ".[" + Tenants.SQL_COL_SEQUENCE_NUMBER + "] = " + TABLE + ".[" + SQL_COL_SEQUENCE_NUMBER + "]" +
                    " AND " + TABLE + "." + SQL_COL_CLIENT_ID + "=?" +
                    " ORDER BY " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_DATE_OF_ENTRY + "], " + TABLE + ".[" + SQL_COL_TENANT_NUMBER + "]";


        static string SQL_REMOVE_PAYMENTS = "DELETE " +
                                            " FROM " + TABLE +
                                            " WHERE " + SQL_COL_CLIENT_ID + " = ?";

        protected string m_BuildingNumber;
        protected string m_TenantNumber;
        protected DateTime m_DateofEntry;
        protected decimal m_Amount;
        protected string m_SecurityCode;
        protected int m_SequenceNumber;
        protected int m_Id;
        protected long m_clientId;
        protected bool m_isChanged;
        protected string m_Notes;

        // For on-demand Payment Register
        protected string m_Address1 = "";
        protected string m_Name = "";
        protected string m_Apartment = "";

        public Payments() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
        }

        public Payments(string BuildingNumber, string TenantNumber, DateTime DateofEntry, decimal Amount, string SecurityCode, int SequenceNumber, int clientId = 0) : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            m_BuildingNumber = BuildingNumber;
            m_TenantNumber = TenantNumber;
            m_DateofEntry = DateofEntry;
            m_Amount = Amount;
            m_SecurityCode = SecurityCode;
            m_SequenceNumber = SequenceNumber;
            if (clientId == 0 && GlobalShared.user != null)
                m_clientId = (int)GlobalShared.user.Account.clientId;
            else
                m_clientId = clientId;
        }

        protected Payments fill(DataRow dr, bool register = false)
        {

            try
            {
                m_Id = (int)dr[SQL_COL_ID];
                m_BuildingNumber = (string)((Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER])) ? "" : (dr[SQL_COL_BUILDING_NUMBER]));
                m_TenantNumber = (string)((Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER])) ? "" : (dr[SQL_COL_TENANT_NUMBER]));
                m_DateofEntry = (DateTime)dr[SQL_COL_DATE_OF_ENTRY];
                m_Amount = System.Convert.ToDecimal(dr[SQL_COL_AMOUNT]);
                m_SecurityCode = (string)((Information.IsDBNull(dr[SQL_COL_SECURITY_CODE])) ? "" : (dr[SQL_COL_SECURITY_CODE]));
                m_SequenceNumber = (int)(dr[SQL_COL_SEQUENCE_NUMBER]);
                m_clientId = (int)dr[SQL_COL_CLIENT_ID];
                m_Notes = (string)((Information.IsDBNull(dr[SQL_COL_NOTES])) ? "" : (dr[SQL_COL_NOTES]));
                if (register)
                {
                    m_Name = (string)(dr[Tenants.SQL_COL_TENANT_NAME] == null ? "" : dr[Tenants.SQL_COL_TENANT_NAME]);
                    m_Apartment = (string)(dr[Tenants.SQL_COL_APARTMENT] == null ? "" : dr[Tenants.SQL_COL_APARTMENT]);
                    m_Address1 = (string)(dr[Building.SQL_COL_ADDRESS_1] == null ? "" : dr[Building.SQL_COL_ADDRESS_1]);
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

        public static Payments load(int Id)
        {

            Payments obj = new Payments();
            string[] opt = new string[] { Id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static Payments[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Payments obj = new Payments();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Payments();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((Payments[])(list.ToArray(typeof(Payments))));
            }
            catch (SqlException)
            {
                return null;
            }

        }

        public static Payments[] loadBySequence(int seqNum)
        {
            string[] opt = { seqNum.ToString() };
            DataSet ds = new DataSet();
            Payments obj = new Payments();

            ArrayList list = new ArrayList();

            ds = obj.load(SQL_LOAD_BY_SEQUENCE, opt);
            obj = null;

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new Payments();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
            }
            try
            {
                return ((Payments[])(list.ToArray(typeof(Payments))));
            }
            catch (SqlException)
            {
                return null;
            }

        }

        public static decimal loadForTotalPaid(int seqNum)
        {
            string[] opt = { seqNum.ToString() };
            DataSet ds = new DataSet();
            Payments obj = new Payments();
            decimal paid = 0;
            try
            {
                ds = obj.load(SQL_LOAD_TOTAL_PAID, opt);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    paid = 0;
                    decimal.TryParse(dr[SQL_COL_TOTAL_PAID].ToString(), out paid);
                    return paid;
                }
                else
                    return 0;
            }
            catch (SqlException)
            {
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Payments[] loadForRegister(string startBld, string endBld, DateTime fromDate, DateTime toDate, long clientId)
        {
            string[] opt = { startBld, endBld, fromDate.ToString(), toDate.ToString(), clientId.ToString() };
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Payments obj = new Payments();

            ds = obj.load(SQL_LOAD_FOR_REGISTER, opt);
            obj = null;

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new Payments();
                    obj.fill(dr, true);
                    list.Add(obj);
                    obj = null;
                }
            }
            try
            {
                return ((Payments[])(list.ToArray(typeof(Payments))));
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
                m_clientId = (int)GlobalShared.user.Account.clientId;

            if (m_Id > 0)
            {
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_DateofEntry.ToString(), m_Amount.ToString(), m_SecurityCode, m_SequenceNumber.ToString(), m_clientId.ToString(),m_Notes, m_Id.ToString() };
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
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_DateofEntry.ToString(), m_Amount.ToString(), m_SecurityCode, m_SequenceNumber.ToString(), m_clientId.ToString(),m_Notes };
                m_Id = this.insertRecord(values);
                if (m_Id >= 0)
                {
                    return true;
                }
            }
            return false;

        }

        public bool RemovePayments(long clientId)
        {
            if (clientId > 0)
            {
                string[] opts = { clientId.ToString() };
                return query(SQL_REMOVE_PAYMENTS, opts);
            }
            else
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Cannot remove payments for clientId 0", true);
                return false;
            }
        }

        public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
        {

            try
            {
                CN.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE Payments (Building Number TEXT(3),Tenant Number TEXT(4),Date of Entry DATETIME,Amount CURRENCY,Security Code TEXT(1),Sequence Number LONG)";
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

        public int ID
        {
            get { return m_Id; }
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

        public DateTime DateofEntry
        {
            get
            {
                return m_DateofEntry;
            }
            set
            {
                m_DateofEntry = value;
                OnPropertyChanged("DateofEntry");
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

        public string SecurityCode
        {
            get
            {
                return m_SecurityCode;
            }
            set
            {
                m_SecurityCode = value;
                OnPropertyChanged("SecurityCode");
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

        public long ClientId
        {
            get { return m_clientId; }
            set
            {
                m_clientId = value;
                OnPropertyChanged("ClientId");
            }
        }

        // For on-demand Payment Register
        public string Name
        {
            get { return m_Name; }
        }
        public string Apartment
        {
            get { return m_Apartment; }
        }
        public string Address1
        {
            get { return m_Address1; }
        }
    }
}