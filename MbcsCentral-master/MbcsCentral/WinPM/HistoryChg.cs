using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    public class HistChg : clsDB, INotifyPropertyChanged
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


        public static string TABLE = "pm_HistChg";

        public const string SQL_COL_BUILDING_NUMBER = "Building Number";
        public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
        public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
        public const string SQL_COL_CHARGE_NUMBER = "Charge Number";
        public const string SQL_COL_DATE = "Date";
        public const string SQL_COL_CODE = "Code";
        public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_ARREARS = "Arrears";
        public const string SQL_COL_HIST_CHARGE_ID = "hist_chg_id";
        public const string SQL_COL_CLIENTID = "clientId";

        static string SQL_LOAD = " SELECT " + 
                                "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                "[" + SQL_COL_DATE + "]," + 
                                "[" + SQL_COL_CODE + "]," + 
                                "[" + SQL_COL_AMOUNT + "]," + 
                                "[" + SQL_COL_ARREARS + "]," + 
                                "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
                                " FROM " + TABLE + 
                                " WHERE " + SQL_COL_HIST_CHARGE_ID + "=?;";

        static string SQL_LOAD_ALL = " SELECT " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_CODE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_ARREARS + "]," + 
                                    "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
                                    " FROM " + TABLE + ";";

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                    "[" + SQL_COL_DATE + "]," + 
                                    "[" + SQL_COL_CODE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_ARREARS + "]," +
                                    "[" + SQL_COL_CLIENTID + "]" +
                                ") VALUES ('?', '?', ?, ?, '?', '?', ?, ?, ?);";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " +
                                    "[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
                                    "[" + SQL_COL_TENANT_NUMBER + "]='?'," +
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," +
                                    "[" + SQL_COL_CHARGE_NUMBER + "]=?," +
                                    "[" + SQL_COL_DATE + "]='?'," +
                                    "[" + SQL_COL_CODE + "]='?'," +
                                    "[" + SQL_COL_AMOUNT + "]=?," +
                                    "[" + SQL_COL_ARREARS + "]=?" +
                                    " WHERE [" + SQL_COL_HIST_CHARGE_ID + "]=?";

        static string SQL_LOAD_BY_TENANT_ID_DATE = " SELECT " + 
                                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                                    "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                                    "[" + SQL_COL_DATE + "]," + 
                                                    "[" + SQL_COL_CODE + "]," + 
                                                    "[" + SQL_COL_AMOUNT + "]," + 
                                                    "[" + SQL_COL_ARREARS + "]," + 
                                                    "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
                                                    " FROM " + TABLE + 
                                                    " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?" + 
                                                    " AND " + SQL_COL_DATE + " >= \'?\'" + 
                                                    " AND " + SQL_COL_DATE + " <= \'?\'" + 
                                                    " ORDER BY " + SQL_COL_DATE;

        static string SQL_LOAD_BY_CHARGE_DATE = " SELECT " + 
                                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                                    "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                                    "[" + SQL_COL_DATE + "]," + 
                                                    "[" + SQL_COL_CODE + "]," + 
                                                    "[" + SQL_COL_AMOUNT + "]," + 
                                                    "[" + SQL_COL_ARREARS + "]," + 
                                                    "[" + SQL_COL_HIST_CHARGE_ID + "]" + 
                                                    " FROM " + TABLE + 
                                                    " WHERE [" + SQL_COL_CHARGE_NUMBER + "]=?" +
                                                    " AND YEAR(" + SQL_COL_DATE + ") = YEAR('?')" +
                                                    " AND MONTH(" + SQL_COL_DATE + ") = MONTH('?')";
                                                    //" AND cast(" + SQL_COL_DATE + " as date) = cast('?' as date)";

        static string SQL_LOAD_FOR_EOM = "SELECT * " +
                                         " FROM " + TABLE +
                                         " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ? " +
                                         " AND " + SQL_COL_CODE + "= '='" +
                                         " AND " + SQL_COL_DATE + " >= '?'" +
                                         " AND " + SQL_COL_DATE + " <= '?'";

        protected string m_BuildingNumber;
        protected string m_TenantNumber;
        protected int m_SequenceNumber;
        protected int m_ChargeNumber;
        protected DateTime m_Date;
        protected string m_Code;
        protected decimal m_Amount;
        protected decimal m_Arrears;
        protected int m_histChgId;
        protected int m_clientId = 1;
        protected bool m_isChanged;

        public HistChg()
            : base(TABLE, SQL_COL_HIST_CHARGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {


        }

        public HistChg(string BuildingNumber, string TenantNumber, int SequenceNumber, int ChargeNumber, DateTime chgDate, string Code, decimal Amount, decimal Arrears)
            : base(TABLE, SQL_COL_HIST_CHARGE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {

            m_BuildingNumber = BuildingNumber;
            m_TenantNumber = TenantNumber;
            m_SequenceNumber = SequenceNumber;
            m_ChargeNumber = ChargeNumber;
            m_Date = chgDate;
            m_Code = Code;
            m_Amount = Amount;
            m_Arrears = Arrears;

        }

        protected HistChg fill(DataRow dr)
        {

            try
            {
                m_BuildingNumber = (string)((Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER])) ? "" : (dr[SQL_COL_BUILDING_NUMBER]));
                m_TenantNumber = (string)((Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER])) ? "" : (dr[SQL_COL_TENANT_NUMBER]));
                m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                m_ChargeNumber = (int)dr[SQL_COL_CHARGE_NUMBER];
                m_Date = (DateTime)dr[SQL_COL_DATE];
                m_Code = (string)((Information.IsDBNull(dr[SQL_COL_CODE])) ? "" : (dr[SQL_COL_CODE]));
                m_Amount = System.Convert.ToDecimal(dr[SQL_COL_AMOUNT]);
                m_Arrears = System.Convert.ToDecimal(dr[SQL_COL_ARREARS]);
                m_histChgId = (int)dr[SQL_COL_HIST_CHARGE_ID];
            }
            catch (SqlException)
            {
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static HistChg load(int chargeNumber)
        {

            HistChg obj = new HistChg();
            string[] opt = new string[] { chargeNumber.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            if (ds == null)
                return null;
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static HistChg loadByChargeDate(int chargeNumber, DateTime dte)
        {
            HistChg obj = new HistChg();
            string[] opt = new string[] { chargeNumber.ToString(), dte.ToString(), dte.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_CHARGE_DATE, opt);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            } else {
                return null;
            }
           
        }

        public static HistChg[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            HistChg obj = new HistChg();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new HistChg();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((HistChg[])(list.ToArray(typeof(HistChg))));
            }
            catch (SqlException)
            {
                return null;
            }

        }

        public static HistChg[] loadByTenantIdDate(int tenantId, DateTime startDate, DateTime endDate, string notCode = null)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            HistChg obj = new HistChg();
            string[] opt = new string[] { tenantId.ToString(), startDate.ToString(), endDate.ToString() };

            ds = obj.load(SQL_LOAD_BY_TENANT_ID_DATE, opt);
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new HistChg();
                obj.fill(dr);
                if (notCode != null && !obj.Code.Equals(notCode))
                    continue;
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((HistChg[])(list.ToArray(typeof(HistChg))));
            }
            catch (SqlException)
            {
                return null;
            }

        }

        public static HistChg[] loadByTenantIdDateCode(int tenantId, DateTime startDate, DateTime endDate, string Code = null)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            HistChg obj = new HistChg();
            string[] opt = new string[] { tenantId.ToString(), startDate.ToString(), endDate.ToString() };

            ds = obj.load(SQL_LOAD_BY_TENANT_ID_DATE, opt);
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new HistChg();
                obj.fill(dr);
                if (Code != null && !obj.Code.Equals(Code))
                    continue;
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((HistChg[])(list.ToArray(typeof(HistChg))));
            }
            catch (SqlException)
            {
                return null;
            }

        }


        public static HistChg loadForEom(int tenantId, System.DateTime startDate, System.DateTime endDate)
        {
            HistChg obj = new HistChg();
            string[] opt = { tenantId.ToString(), startDate.ToString(), endDate.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_FOR_EOM, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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

            if (m_histChgId > 0)
            {
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_ChargeNumber.ToString(), m_Date.ToString(), m_Code, m_Amount.ToString(), m_Arrears.ToString(), m_histChgId.ToString() };
                if (this.updateRecord(m_histChgId.ToString(), values))
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
                string[] values = new string[] { m_BuildingNumber, m_TenantNumber, m_SequenceNumber.ToString(), m_ChargeNumber.ToString(), m_Date.ToString(), m_Code, m_Amount.ToString(), m_Arrears.ToString(), m_clientId.ToString() };
                m_histChgId = this.insertRecord(values);
                if (m_histChgId >= 0)
                {
                    return true;
                }
            }
            return false;

        }


        public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
        {

            try
            {
                CN.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE HistChg (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Charge Number LONG,Date DATETIME,Code TEXT(1),Amount CURRENCY,Arrears CURRENCY)";
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

        public int ChargeNumber
        {
            get
            {
                return m_ChargeNumber;
            }
            set
            {
                m_ChargeNumber = value;
                OnPropertyChanged("ChargeNumber");
            }
        }

        public DateTime ChargeDate
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

        public decimal Arrears
        {
            get
            {
                return m_Arrears;
            }
            set
            {
                m_Arrears = value;
                OnPropertyChanged("Arrears");
            }
        }

        public int HistChgId
        {
            get
            {
                return m_histChgId;
            }
        }

    }
}