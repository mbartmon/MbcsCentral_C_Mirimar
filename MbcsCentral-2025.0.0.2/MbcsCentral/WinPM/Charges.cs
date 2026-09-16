using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{

	public class Charges : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		public static string TABLE = "pm_Charges";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
		public const string SQL_COL_CHARGE_CODE = "Charge Code";
		public const string SQL_COL_TYPE = "Type";
		public const string SQL_COL_AMOUNT = "Amount";
		public const string SQL_COL_ARREARS = "Arrears";
		public const string SQL_COL_PAID = "Paid";
		public const string SQL_COL_STARTING_DATE = "Starting Date";
		public const string SQL_COL_ENDING_DATE = "Ending Date";
		public const string SQL_COL_FUTURE_AMOUNT = "Future Amount";
		public const string SQL_COL_FUTURE_DATE = "Future Date";
		public const string SQL_COL_CHARGE_NUMBER = "Charge Number";
		public const string SQL_COL_HISTORY_DATE = "History Date";
		public const string SQL_COL_COMMENT = "Comment";
		public const string SQL_COL_ON_BILL = "OnBill";
		public const string SQL_COL_CHARGE_TO_TENANT = "ChargeToTenant";
		public const string SQL_COL_LEGAL_RENT = "LegalRent";
		public const string SQL_COL_SHOW_ON_BILL = "ShowOnBill";
        public const string SQL_COL_CLIENT_ID = "clientId";

        public const string SQL_COL_TOTALCHARGES = "TotalCharges";
        public const string SQL_COL_TOTAL_PAID = "Paid";

        // For Batch Processing
        static string TEN_TABLE = Tenants.TABLE;

		static string SQL_LOAD = " SELECT " + 
								"[" + SQL_COL_BUILDING_NUMBER + "]," + 
								"[" + SQL_COL_TENANT_NUMBER + "]," + 
								"[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
								"[" + SQL_COL_CHARGE_CODE + "]," + 
								"[" + SQL_COL_TYPE + "]," + 
								"[" + SQL_COL_AMOUNT + "]," + 
								"[" + SQL_COL_ARREARS + "]," + 
								"[" + SQL_COL_PAID + "]," + 
								"[" + SQL_COL_STARTING_DATE + "]," + 
								"[" + SQL_COL_ENDING_DATE + "]," + 
								"[" + SQL_COL_FUTURE_AMOUNT + "]," +
								"[" + SQL_COL_FUTURE_DATE + "]," + 
								"[" + SQL_COL_CHARGE_NUMBER + "]," + 
								"[" + SQL_COL_HISTORY_DATE + "]," + 
								"[" + SQL_COL_COMMENT + "]," + 
								"[" + SQL_COL_ON_BILL + "]," + 
								"[" + SQL_COL_CHARGE_TO_TENANT + "]," + 
								"[" + SQL_COL_LEGAL_RENT + "]," + 
								"[" + SQL_COL_SHOW_ON_BILL + "]," + 
                                "[" + SQL_COL_CLIENT_ID + "]" +
								" FROM " + TABLE + 
								" WHERE [" + SQL_COL_CHARGE_NUMBER + "] = ?";

		// 11/16/13 = Is not currently used, adding WHERE clause with clientId
        static string SQL_LOAD_ALL = " SELECT " +
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                    "[" + SQL_COL_TENANT_NUMBER + "]," +
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                    "[" + SQL_COL_CHARGE_CODE + "]," +
                                    "[" + SQL_COL_TYPE + "]," +
                                    "[" + SQL_COL_AMOUNT + "]," +
                                    "[" + SQL_COL_ARREARS + "]," +
                                    "[" + SQL_COL_PAID + "]," +
                                    "[" + SQL_COL_STARTING_DATE + "]," +
                                    "[" + SQL_COL_ENDING_DATE + "]," +
                                    "[" + SQL_COL_FUTURE_AMOUNT + "]," +
                                    "[" + SQL_COL_FUTURE_DATE + "]," +
                                    "[" + SQL_COL_CHARGE_NUMBER + "]," +
                                    "[" + SQL_COL_HISTORY_DATE + "]," +
                                    "[" + SQL_COL_COMMENT + "]," +
                                    "[" + SQL_COL_ON_BILL + "]," +
                                    "[" + SQL_COL_CHARGE_TO_TENANT + "]," +
                                    "[" + SQL_COL_LEGAL_RENT + "]," +
                                    "[" + SQL_COL_SHOW_ON_BILL + "]," +
                                    "[" + SQL_COL_CLIENT_ID + "]" +
                                    " FROM " + TABLE;
                                    // 2014-08-16 do not need clientId
                                    //" WHERE "+ SQL_COL_CLIENT_ID + " = ?;";

		static string SQL_LOAD_BY_SEQUENCE_NUMBER = " SELECT " + 
			"[" + SQL_COL_BUILDING_NUMBER + "]," + 
			"[" + SQL_COL_TENANT_NUMBER + "]," + 
			"[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
			"[" + SQL_COL_CHARGE_CODE + "]," + 
			"[" + SQL_COL_TYPE + "]," + 
			"[" + SQL_COL_AMOUNT + "]," + 
			"[" + SQL_COL_ARREARS + "]," + 
			"[" + SQL_COL_PAID + "]," + 
			"[" + SQL_COL_STARTING_DATE + "]," + 
			"[" + SQL_COL_ENDING_DATE + "]," + 
			"[" + SQL_COL_FUTURE_AMOUNT + "]," + 
			"[" + SQL_COL_FUTURE_DATE + "]," + 
			"[" + SQL_COL_CHARGE_NUMBER + "]," + 
			"[" + SQL_COL_HISTORY_DATE + "]," + 
			"[" + SQL_COL_COMMENT + "]," + 
			"[" + SQL_COL_ON_BILL + "]," + 
			"[" + SQL_COL_CHARGE_TO_TENANT + "]," + 
			"[" + SQL_COL_LEGAL_RENT + "]," + 
			"[" + SQL_COL_SHOW_ON_BILL + "]," +
            "[" + SQL_COL_CLIENT_ID + "]" +
			" FROM " + TABLE + 
			" WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?";

        static string SQL_LOAD_BY_CHARGE_CODE = " SELECT " +
            "[" + SQL_COL_BUILDING_NUMBER + "]," +
            "[" + SQL_COL_TENANT_NUMBER + "]," +
            "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
            "[" + SQL_COL_CHARGE_CODE + "]," +
            "[" + SQL_COL_TYPE + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_ARREARS + "]," +
            "[" + SQL_COL_PAID + "]," +
            "[" + SQL_COL_STARTING_DATE + "]," +
            "[" + SQL_COL_ENDING_DATE + "]," +
            "[" + SQL_COL_FUTURE_AMOUNT + "]," +
            "[" + SQL_COL_FUTURE_DATE + "]," +
            "[" + SQL_COL_CHARGE_NUMBER + "]," +
            "[" + SQL_COL_HISTORY_DATE + "]," +
            "[" + SQL_COL_COMMENT + "]," +
            "[" + SQL_COL_ON_BILL + "]," +
            "[" + SQL_COL_CHARGE_TO_TENANT + "]," +
            "[" + SQL_COL_LEGAL_RENT + "]," +
            "[" + SQL_COL_SHOW_ON_BILL + "]," +
            "[" + SQL_COL_CLIENT_ID + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?" +
            " AND [" + SQL_COL_CHARGE_CODE + "] = '?'";

	   static string SQL_LOAD_FOR_SECURITY = " SELECT " +
												"[" + SQL_COL_BUILDING_NUMBER + "]," +
												"[" + SQL_COL_TENANT_NUMBER + "]," +
												"[" + SQL_COL_SEQUENCE_NUMBER + "]," +
												"[" + SQL_COL_CHARGE_CODE + "]," +
												"[" + SQL_COL_TYPE + "]," +
												"[" + SQL_COL_AMOUNT + "]," +
												"[" + SQL_COL_ARREARS + "]," +
												"[" + SQL_COL_PAID + "]," +
												"[" + SQL_COL_STARTING_DATE + "]," +
												"[" + SQL_COL_ENDING_DATE + "]," +
												"[" + SQL_COL_FUTURE_AMOUNT + "]," +
												"[" + SQL_COL_FUTURE_DATE + "]," +
												"[" + SQL_COL_CHARGE_NUMBER + "]," +
												"[" + SQL_COL_HISTORY_DATE + "]," +
												"[" + SQL_COL_COMMENT + "]," +
												"[" + SQL_COL_ON_BILL + "]," +
												"[" + SQL_COL_CHARGE_TO_TENANT + "]," +
												"[" + SQL_COL_LEGAL_RENT + "]," +
												"[" + SQL_COL_SHOW_ON_BILL + "]," +
                                                "[" + SQL_COL_CLIENT_ID + "]" +
												" FROM " + TABLE +
												" WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?" +
												" AND [" + SQL_COL_CHARGE_CODE + "] = '?'" +
												" AND [" + SQL_COL_STARTING_DATE + "] < '?'";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                    "[" + SQL_COL_CHARGE_CODE + "]," + 
                                    "[" + SQL_COL_TYPE + "]," + 
                                    "[" + SQL_COL_AMOUNT + "]," + 
                                    "[" + SQL_COL_ARREARS + "]," + 
                                    "[" + SQL_COL_PAID + "]," + 
                                    "[" + SQL_COL_STARTING_DATE + "]," + 
                                    "[" + SQL_COL_ENDING_DATE + "]," + 
                                    "[" + SQL_COL_FUTURE_AMOUNT + "]," + 
                                    "[" + SQL_COL_FUTURE_DATE + "]," + 
                                    "[" + SQL_COL_HISTORY_DATE + "]," + 
                                    "[" + SQL_COL_COMMENT + "]," + 
                                    "[" + SQL_COL_ON_BILL + "]," + 
                                    "[" + SQL_COL_CHARGE_TO_TENANT + "]," + 
                                    "[" + SQL_COL_LEGAL_RENT + "]," + 
                                    "[" + SQL_COL_SHOW_ON_BILL + "]," +
                                    "[" + SQL_COL_CLIENT_ID + "]" + 
                                    ") VALUES ('?', '?', ?, '?', '?', ?, ?, ?, '?', '?', ?, '?', '?', '?', ?, ?, ?, ?, ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
                                    "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_TENANT_NUMBER + "]='?'," + 
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]=?," + 
                                    "[" + SQL_COL_CHARGE_CODE + "]='?'," + 
                                    "[" + SQL_COL_TYPE + "]='?'," + 
                                    "[" + SQL_COL_AMOUNT + "]=?," + 
                                    "[" + SQL_COL_ARREARS + "]=?," + 
                                    "[" + SQL_COL_PAID + "]=?," + 
                                    "[" + SQL_COL_STARTING_DATE + "]='?'," + 
                                    "[" + SQL_COL_ENDING_DATE + "]='?'," + 
                                    "[" + SQL_COL_FUTURE_AMOUNT + "]=?," + 
                                    "[" + SQL_COL_FUTURE_DATE + "]='?'," + 
                                    "[" + SQL_COL_HISTORY_DATE + "]='?'," + 
                                    "[" + SQL_COL_COMMENT + "]='?'," + 
                                    "[" + SQL_COL_ON_BILL + "]=?," + 
                                    "[" + SQL_COL_CHARGE_TO_TENANT + "]=?," + 
                                    "[" + SQL_COL_LEGAL_RENT + "]=?," + 
                                    "[" + SQL_COL_SHOW_ON_BILL + "]=?," +
                                    "[" + SQL_COL_CLIENT_ID + "]=?" +
                                    " WHERE [" + SQL_COL_CHARGE_NUMBER + "] = ?";

		static string SQL_LOAD_WITH_DESCRIPTION = " SELECT " + 
                                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                                    "[" + TABLE + "].[" + SQL_COL_CHARGE_CODE + "]," + 
                                                    "[" + SQL_COL_TYPE + "]," + 
                                                    "[" + SQL_COL_AMOUNT + "]," + 
                                                    "[" + SQL_COL_ARREARS + "]," + 
                                                    "[" + SQL_COL_PAID + "]," + 
                                                    "[" + SQL_COL_STARTING_DATE + "]," + 
                                                    "[" + SQL_COL_ENDING_DATE + "]," + 
                                                    "[" + SQL_COL_FUTURE_AMOUNT + "]," + 
                                                    "[" + SQL_COL_FUTURE_DATE + "]," + 
                                                    "[" + SQL_COL_CHARGE_NUMBER + "]," + 
                                                    "[" + SQL_COL_HISTORY_DATE + "]," + 
                                                    "[" + SQL_COL_COMMENT + "]," + 
                                                    "[" + SQL_COL_ON_BILL + "]," + 
                                                    "[" + SQL_COL_CHARGE_TO_TENANT + "]," + 
                                                    "[" + SQL_COL_LEGAL_RENT + "]," + 
                                                    "[" + SQL_COL_SHOW_ON_BILL + "], " +
                                                    TABLE + ".[" + SQL_COL_CLIENT_ID + "]," +
                                                    "[" + ChargeCodes.TABLE + "].[" + ChargeCodes.SQL_COL_CHARGE_DESCRIPTION + "]" + 
                                                    " FROM " + TABLE + ", " + ChargeCodes.TABLE + 
                                                    " WHERE [" + ChargeCodes.TABLE + "].[" + ChargeCodes.SQL_COL_CHARGE_CODE + "] = [" + TABLE + "].[" + SQL_COL_CHARGE_CODE + "]" + 
                                                    " AND [" + SQL_COL_SEQUENCE_NUMBER + "] = ?";

        static string SQL_LOAD_FOR_TOTAL_CHARGES = "SELECT " +
                                                    "SUM(" + SQL_COL_AMOUNT + "+" + SQL_COL_ARREARS + "+" + SQL_COL_PAID + ") AS " + SQL_COL_TOTALCHARGES +
                                                    " FROM " + TABLE +
                                                    " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?" +
                                                    " AND [" + SQL_COL_STARTING_DATE + "] <= '?'"; 
                                                    //" AND FLOOR(CAST([" + SQL_COL_STARTING_DATE + "] as float)) as datetime " + " <= FLOOR(CAST(? as float))";

        public static string SQL_LOAD_FOR_BATCH = " SELECT " +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_BUILDING_NUMBER + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_TENANT_NUMBER + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_SEQUENCE_NUMBER + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_CLIENT_ID + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_TENANT_NAME + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_APARTMENT + "]," +
                                            TEN_TABLE + ".[" + Tenants.SQL_COL_PAID + "] AS TotalPaid" +
                                            " FROM " + TEN_TABLE +
                                            " WHERE " + TEN_TABLE + ".[" + Tenants.SQL_COL_CLIENT_ID + "]=?";
        
        public static string SQL_LOAD_FOR_BATCH_EDIT = "SELECT " +
                                                      TEN_TABLE + ".[" + Tenants.SQL_COL_BUILDING_NUMBER + "]," + 
                                                      TEN_TABLE + ".[" + Tenants.SQL_COL_TENANT_NUMBER + "]," +
                                                      TABLE + ".[" + Charges.SQL_COL_SEQUENCE_NUMBER + "]," +
                                                      "[" + Tenants.SQL_COL_TENANT_NAME + "]," + 
                                                      "[" + Tenants.SQL_COL_APARTMENT + "]," + 
                                                      TABLE + ".[" + SQL_COL_CHARGE_CODE + "]," +
                                                      TABLE + ".[" + SQL_COL_TYPE + "]," + 
                                                      "[" + SQL_COL_AMOUNT + "]," + 
                                                      TABLE + ".[" + SQL_COL_PAID + "]," +
                                                      TABLE + ".[" + SQL_COL_ARREARS + "]," +
                                                      "[" + SQL_COL_STARTING_DATE + "]," + 
                                                      "[" + SQL_COL_ENDING_DATE + "]," + 
                                                      "[" + SQL_COL_FUTURE_DATE + "]," +
                                                      "[" + SQL_COL_HISTORY_DATE + "]," +
                                                      "[" + SQL_COL_FUTURE_AMOUNT + "]," +
                                                      "[" + SQL_COL_CHARGE_NUMBER + "]," +
                                                      TABLE + ".[" + SQL_COL_COMMENT + "]," +
                                                      "[" + SQL_COL_ON_BILL + "]," +
                                                      "[" + SQL_COL_CHARGE_TO_TENANT + "]," +
                                                      "[" + SQL_COL_LEGAL_RENT + "]," +
                                                      "[" + SQL_COL_SHOW_ON_BILL + "]," +
                                                      TABLE + ".[" + SQL_COL_CLIENT_ID + "]" + " FROM " + TEN_TABLE +
                                                      " INNER JOIN " +
                                                      TABLE + " ON " + TEN_TABLE + ".[" + Tenants.SQL_COL_SEQUENCE_NUMBER + "] = " + TABLE + ".[" + SQL_COL_SEQUENCE_NUMBER + "]";                           

		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected int m_SequenceNumber;
		protected string m_ChargeCode;
		protected string m_Type;
		protected decimal m_Amount;
		protected decimal m_Arrears;
		protected decimal m_Paid;
		protected DateTime m_StartingDate = MbcsCentral.GlobalShared.NULL_DATE;
		protected DateTime m_EndingDate = MbcsCentral.GlobalShared.NULL_DATE;
		protected decimal m_FutureAmount;
		protected DateTime m_FutureDate = MbcsCentral.GlobalShared.NULL_DATE;
		protected int m_ChargeNumber;
		protected DateTime m_HistoryDate = MbcsCentral.GlobalShared.NULL_DATE;
		protected string m_Comment;
		protected bool m_OnBill;
		protected bool m_ChargeToTenant;
		protected bool m_LegalRent;
		protected bool m_ShowOnBill;
        protected long m_clientId;
		protected string m_Description;
		// Locally calculated field
		protected decimal m_Total;
		protected decimal m_Balance;
        // Batch fields
        protected string m_TenantName;
        protected string m_Apartment;
        protected bool m_DeleteCharge = false;
        protected decimal m_TotalPaid;
		protected decimal m_TotalCharges;
		protected bool m_isChanged;

		// Charge Type enum
		public  enum ChargeType { OneTime = 1, Recurring = 2  };

		public Charges() : base(TABLE, SQL_COL_CHARGE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Charges(string BuildingNumber, string TenantNumber, int SequenceNumber, string ChargeCode, string Type, decimal Amount, decimal Arrears, decimal Paid, DateTime StartingDate, DateTime EndingDate,
		decimal FutureAmount, DateTime FutureDate, Int32 ChargeNumber, DateTime HistoryDate, string Comment, bool OnBill, bool ChargeToTenant, bool LegalRent, bool ShowOnBill, long clientId) 
            : base(TABLE, SQL_COL_CHARGE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_SequenceNumber = SequenceNumber;
			m_ChargeCode = ChargeCode;
			m_Type = Type;
			m_Amount = Amount;
			m_Arrears = Arrears;
			m_Paid = Paid;
			m_StartingDate = StartingDate;
			m_EndingDate = EndingDate;
			m_FutureAmount = FutureAmount;
			m_FutureDate = FutureDate;
			m_ChargeNumber = ChargeNumber;
			m_HistoryDate = HistoryDate;
			m_Comment = Comment;
			m_OnBill = OnBill;
			m_ChargeToTenant = ChargeToTenant;
			m_LegalRent = LegalRent;
			m_ShowOnBill = ShowOnBill;
            m_clientId = clientId;
		}

		protected Charges fill(DataRow dr, bool batch = false, bool totalOnly = false, bool withDescription = false)
		{

            if (!batch)
            {
                if (totalOnly)
                {
                    try
                    {
                        m_Total = 0;
                        decimal.TryParse(dr[SQL_COL_TOTALCHARGES].ToString(), out m_Total);
                    }
                    catch
                    {
                        m_Total = 0;
                    }

                }
                else
                {
                    try
                    {
                        m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
                        m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
                        m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                        m_ChargeCode = (string)(Information.IsDBNull(dr[SQL_COL_CHARGE_CODE]) ? "" : dr[SQL_COL_CHARGE_CODE]);
                        m_Amount = (decimal)dr[SQL_COL_AMOUNT];
                        m_Arrears = (decimal)dr[SQL_COL_ARREARS];
                        m_Paid = (decimal)dr[SQL_COL_PAID];
                        m_StartingDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_STARTING_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_STARTING_DATE]);
                        m_EndingDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_ENDING_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_ENDING_DATE]);
                        m_FutureAmount = (Decimal)dr[SQL_COL_FUTURE_AMOUNT];
                        m_FutureDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_FUTURE_DATE]);
                        m_ChargeNumber = (int)dr[SQL_COL_CHARGE_NUMBER];
                        m_HistoryDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_HISTORY_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_HISTORY_DATE]);
                        m_Comment = (string)(Information.IsDBNull(dr[SQL_COL_COMMENT]) ? "" : dr[SQL_COL_COMMENT]);
                        m_OnBill = (bool)dr[SQL_COL_ON_BILL];
                        m_ChargeToTenant = (bool)dr[SQL_COL_CHARGE_TO_TENANT];
                        m_LegalRent = (bool)dr[SQL_COL_LEGAL_RENT];
                        m_ShowOnBill = (bool)dr[SQL_COL_SHOW_ON_BILL];
                        m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
						m_TotalCharges = (Decimal)dr[SQL_COL_TOTALCHARGES];
                    }
                    catch (SqlException ex)
                    {
                    }
                    catch (System.Exception exg)
                    {
                    }
                    if (withDescription)
                    {
                        try
                        {
                            m_Description = (string)(Information.IsDBNull(dr[ChargeCodes.SQL_COL_CHARGE_DESCRIPTION]) ? "" : dr[ChargeCodes.SQL_COL_CHARGE_DESCRIPTION]);
                        }
                        catch
                        {
                            m_Description = "";
                        }
                    }
                }
            }
            else
            {
                m_ChargeCode = (string)(Information.IsDBNull(dr[SQL_COL_CHARGE_CODE]) ? "" : dr[SQL_COL_CHARGE_CODE]);
                m_Amount = (decimal)dr[SQL_COL_AMOUNT];
                m_Arrears = (decimal)dr[SQL_COL_ARREARS];
                m_Paid = (decimal)dr[SQL_COL_PAID];
                m_StartingDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_STARTING_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_STARTING_DATE]);
                m_EndingDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_ENDING_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_ENDING_DATE]);
                m_FutureAmount = (Decimal)dr[SQL_COL_FUTURE_AMOUNT];
                m_FutureDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_FUTURE_DATE]);
                m_ChargeNumber = (int)dr[SQL_COL_CHARGE_NUMBER];
                m_BuildingNumber = (string)dr[Tenants.SQL_COL_BUILDING_NUMBER];
                m_TenantNumber = (string)dr[Tenants.SQL_COL_TENANT_NUMBER];
                m_SequenceNumber = (int)dr[Tenants.SQL_COL_SEQUENCE_NUMBER];
                m_HistoryDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_HISTORY_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_HISTORY_DATE]);
                m_Comment = (string)(Information.IsDBNull(dr[SQL_COL_COMMENT]) ? "" : dr[SQL_COL_COMMENT]);
                m_OnBill = (bool)dr[SQL_COL_ON_BILL];
                m_ChargeToTenant = (bool)dr[SQL_COL_CHARGE_TO_TENANT];
                m_LegalRent = (bool)dr[SQL_COL_LEGAL_RENT];
                m_ShowOnBill = (bool)dr[SQL_COL_SHOW_ON_BILL];
                m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
                m_clientId = (int)dr[SQL_COL_CLIENT_ID];
                m_TotalCharges = (Decimal)dr[SQL_COL_TOTALCHARGES];
                try
                {
                    m_TotalPaid = (decimal)dr["TotalPaid"];
                }
                catch
                {
                    m_TotalPaid = 0;
                }
                try
                {
                    m_TenantName = (string)(Information.IsDBNull(dr[Tenants.SQL_COL_TENANT_NAME]) ? "" : dr[Tenants.SQL_COL_TENANT_NAME]);
                }
                catch
                {
                    m_TenantName = "";
                }
                try
                {
                    m_Apartment = (string)(Information.IsDBNull(dr[Tenants.SQL_COL_APARTMENT]) ? "" : dr[Tenants.SQL_COL_APARTMENT]);

                }
                catch
                {
                    m_Apartment = "";
                }
            }

			//m_Total = m_Amount + m_Arrears;
			m_Balance = m_Amount + m_Arrears - m_Paid;
			return this;

		}

		public static Charges load(int chgNum)
		{

			Charges obj = new Charges();
			string[] opt = { chgNum.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			}
			else
				return null;
		}

        public static Charges loadTotal(int seqNumber, string date)
        {
            Charges obj = new Charges();
            string[] opt = { seqNumber.ToString(), date };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_FOR_TOTAL_CHARGES, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr, false, true);
            }
            else
                return null;
        }

        public static Charges[] loadByChargeCode(int seqnum, string code)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Charges obj = new Charges();
            string[] opt = { seqnum.ToString(), code };

            ds = obj.load(SQL_LOAD_BY_CHARGE_CODE, opt);
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Charges();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Charges[])list.ToArray(typeof(Charges));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

		public static Charges[] loadBySeqNum(int seqnum, bool withDescription = false)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Charges obj = new Charges();
			string[] opt = { seqnum.ToString() };

			if (withDescription) {
				ds = obj.load(SQL_LOAD_WITH_DESCRIPTION, opt);
			} else {
				ds = obj.load(SQL_LOAD_BY_SEQUENCE_NUMBER, opt);
			}
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Charges();
				obj.fill(dr,false,false,withDescription);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Charges[])list.ToArray(typeof(Charges));
			} catch (SqlException ex) {
				return null;
			}

		}


		public static Charges[] loadForSecurity( int seqnum, string chgCode, DateTime startDate)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Charges obj = new Charges();
			string[] opt = { seqnum.ToString(), chgCode,startDate.ToString()  };

			ds = obj.load( SQL_LOAD_FOR_SECURITY, opt );
			if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new Charges();
				obj.fill( dr );
				list.Add( obj );
				obj = null;
			}
			try
			{
				return (Charges[]) list.ToArray( typeof( Charges ) );
			}
			catch (SqlException ex)
			{
				return null;
			}
		}

		public static Charges[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Charges obj = new Charges();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Charges();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Charges[])list.ToArray(typeof(Charges));
			} catch (SqlException ex) {
				return null;
			}

		}

        public static Charges[] loadForBatch(string sql, long clientId)
        {
  			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Charges obj = new Charges();
			string[] opt = {clientId.ToString()};

			ds = obj.load( sql, opt );
			if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new Charges();
				obj.fill( dr , true);
				list.Add( obj );
				obj = null;
			}
			try
			{
				return (Charges[]) list.ToArray( typeof( Charges ) );
			}
			catch (SqlException ex)
			{
				return null;
			}

        }

        public static Charges[] Query(string sql, long clientId, bool batch = false)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Charges obj = new Charges();
            string[] opt = { clientId.ToString() };

            ds = obj.load(sql, opt);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Charges();
                obj.fill(dr, batch);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Charges[])list.ToArray(typeof(Charges));
            }
            catch (SqlException ex)
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

            if (m_ChargeNumber > 0)
            {
                string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_ChargeCode,
					m_Type,
					m_Amount.ToString(),
					m_Arrears.ToString(),
					m_Paid.ToString(),
					m_StartingDate.ToString(),
					m_EndingDate.ToString(),
					m_FutureAmount.ToString(),
					m_FutureDate.ToString(),
					m_HistoryDate.ToString(),
					m_Comment,
					m_OnBill.ToString(),
					m_ChargeToTenant.ToString(),
					m_LegalRent.ToString(),
					m_ShowOnBill.ToString(),
                    m_clientId.ToString(),
					m_ChargeNumber.ToString()
				};
                if (this.updateRecord(m_ChargeNumber.ToString(), values))
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
                string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_SequenceNumber.ToString(),
					m_ChargeCode,
					m_Type,
					m_Amount.ToString(),
					m_Arrears.ToString(),
					m_Paid.ToString(),
					m_StartingDate.ToString(),
					m_EndingDate.ToString(),
					m_FutureAmount.ToString(),
					m_FutureDate.ToString(),
					m_HistoryDate.ToString(),
					m_Comment,
					m_OnBill.ToString(),
					m_ChargeToTenant.ToString(),
					m_LegalRent.ToString(),
					m_ShowOnBill.ToString(),
                    m_clientId.ToString()
				};
                m_ChargeNumber = this.insertRecord(values);
                if (m_ChargeNumber >= 0)
                {
                    return true;
                }
            }
            return false;

        }

		public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try {
				CN.Open();
				System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Charges (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number LONG,Charge Code TEXT(1),Type TEXT(1),Amount CURRENCY,Arrears CURRENCY,Paid CURRENCY,Starting Date DATETIME,Ending Date DATETIME,Future Amount CURRENCY,Future Date DATETIME,Charge Number AUTONUMBER,History Date DATETIME,Comment TEXT(30),OnBill YESNO,ChargeToTenant YESNO,LegalRent YESNO,ShowOnBill YESNO)";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (SqlException ex) {
				return false;
			} finally {
				try {
					CN.Close();
				} catch {
				}
			}

			return true;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_isChanged = true;
		}

        // Property Routines
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool isChanged
        {
            get { return m_isChanged; }
			set { m_isChanged = value; }
		}
		public string BuildingNumber {
			get { return m_BuildingNumber; }
			set {
				m_BuildingNumber = value;
				OnPropertyChanged("BuildingNumber");
			}
		}

		public string TenantNumber {
			get { return m_TenantNumber; }
			set {
				m_TenantNumber = value;
				OnPropertyChanged("TenantNumber");
			}
		}

		public int SequenceNumber {
			get { return m_SequenceNumber; }
			set {
				m_SequenceNumber = value;
				OnPropertyChanged("SequenceNumber");
			}
		}

		public string ChargeCode {
			get { return m_ChargeCode; }
			set {
				m_ChargeCode = value.Trim();
				OnPropertyChanged("ChargeCode");
			}
		}

		public string Type {
			get { return m_Type; }
			set {
				m_Type = value;
				OnPropertyChanged("Type");
			}
		}

		public decimal Amount {
			get { return m_Amount; }
			set {
			m_Amount = value;
				this.Total = m_Amount + m_Arrears;
				this.Balance = m_Amount + m_Arrears - m_Paid;
				OnPropertyChanged("Amount");
			}
		}

		public decimal Arrears {
			get { return m_Arrears; }
			set {
				m_Arrears = value;
				this.Total = m_Amount + m_Arrears;
				this.Balance = m_Amount + m_Arrears - m_Paid;
				OnPropertyChanged("Arrears");
			}
		}

		public decimal Paid {
			get { return m_Paid; }
			set {
				m_Paid = value;
				this.Balance = m_Amount + m_Arrears - m_Paid;
				OnPropertyChanged("Paid");
			}
		}

		public DateTime StartingDate {
			get { return m_StartingDate; }
			set {
				m_StartingDate = value;
				OnPropertyChanged("StartingDate");
			}
		}

		public DateTime EndingDate {
			get { return m_EndingDate; }
			set {
				m_EndingDate = value;
				OnPropertyChanged("EndingDate");
			}
		}

		public decimal FutureAmount {
			get { return m_FutureAmount; }
			set {
				m_FutureAmount = value;
				OnPropertyChanged("FutureAmount");
			}
		}

		public DateTime FutureDate {
			get { return m_FutureDate; }
			set {
				m_FutureDate = value;
				OnPropertyChanged("FutureDate");
			}
		}

		public int ChargeNumber {
			get { return m_ChargeNumber; }
		}

		public DateTime HistoryDate {
			get { return m_HistoryDate; }
			set {
				m_HistoryDate = value;
				OnPropertyChanged("HistoryDate");
			}
		}

		public string Comment {
			get { return m_Comment; }
			set {
				m_Comment = value;
				OnPropertyChanged("Comment");
			}
		}

		public bool OnBill {
			get { return m_OnBill; }
			set {
				m_OnBill = value;
				OnPropertyChanged("OnBill");
			}
		}

		public bool ChargeToTenant {
			get { return m_ChargeToTenant; }
			set {
				m_ChargeToTenant = value;
				OnPropertyChanged("ChargeToTenant");
			}
		}

		public bool LegalRent {
			get { return m_LegalRent; }
			set {
				m_LegalRent = value;
				OnPropertyChanged("LegalRent");
			}
		}

		public bool ShowOnBill {
			get { return m_ShowOnBill; }
			set {
				m_ShowOnBill = value;
				OnPropertyChanged("ShowOnBill");
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
		public string Description {
			get { return m_Description; }
		}

		public decimal Balance {
			get { return m_Balance; }
			set { m_Balance = value; OnPropertyChanged("Balance"); }
		}

		public decimal Total
		{
			get { return m_Total; }
			set {m_Total = value; OnPropertyChanged("Total");}
		}

        // For Batch Processing
        public string BuildingTenantNumber
        {
            get { return m_BuildingNumber + m_TenantNumber; }
        }
        public string TenantName
        {
            get{return m_TenantName;}
        }
        public string Apartment
        {
            get{return m_Apartment;}
        }
        public bool DeleteCharge
        {
            get { return m_DeleteCharge; }
            set { 
                    m_DeleteCharge = value;
                    OnPropertyChanged("DeleteCharge");
                }
        }
        public decimal TotalPaid
        {
            get { return m_TotalPaid; }
        }
		public decimal TotalCharges
		{
			get { return m_TotalCharges; }
		}

	}
}
