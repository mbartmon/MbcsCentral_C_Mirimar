using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace MbcsCentralLogCleaner
{
    // TODO: Ensure that client Control records are created at client startup

    public enum ReturnAddress
    {
        OwnerNameAddress,
        OwnerNameThisAddress,
        ThisNameAddress
    }
    public enum PayTo
    {
        Owner,
        ThisName
    }
    public enum Telephone
    {
        Owner,
        This
    }
    public class Control : clsDB, INotifyPropertyChanged
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


        static string TABLE = "pm_Control";

        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_STATUS_REPORT_GROUPING = "Status Report Grouping";
        public const string SQL_COL_CLOSING_DATE = "Closing Date";
        public const string SQL_COL_BILLING_DATE = "Billing date";
        public const string SQL_COL_LAST_REORG_DATE = "Last Reorg Date";
        public const string SQL_COL_LAST_ROLL_DATE = "Last Roll Date";
        public const string SQL_COL_SECURITY_TRACKER = "Security Tracker";
        public const string SQL_COL_GENERAL_LEDGER = "General Ledger";
        public const string SQL_COL_RETURN_ADDRESS_FLAG = "Return Address Flag";
        public const string SQL_COL_RETURN_ADDRESS_A1 = "Return Address A1";
        public const string SQL_COL_RETURN_ADDRESS_A2 = "Return Address A2";
        public const string SQL_COL_RETURN_ADDRESS_A3 = "Return Address A3";
        public const string SQL_COL_RETURN_ADDRESS_BNAME = "Return Address Bname";
        public const string SQL_COL_RETURN_ADDRESS_B1 = "Return Address B1";
        public const string SQL_COL_RETURN_ADDRESS_B2 = "Return Address B2";
        public const string SQL_COL_RETURN_ADDRESS_B3 = "Return Address B3";
        public const string SQL_COL_PAY_TO_FLAG = "Pay To Flag";
        public const string SQL_COL_PAY_TO_NAME = "Pay To Name";
        public const string SQL_COL_STATEMENT_MESSAGE_1 = "Statement Message 1";
        public const string SQL_COL_STATEMENT_MESSAGE_2 = "Statement Message 2";
        public const string SQL_COL_STATEMENT_MESSAGE_3 = "Statement Message 3";
        public const string SQL_COL_GENERAL_MSG_LOCATION = "General Msg Location";
        public const string SQL_COL_LEGAL_MSG_LOCATION = "Legal Msg Location";
        public const string SQL_COL_TYPE_MSG_LOCATION = "Type Msg Location";
        public const string SQL_COL_TELEPHONE_FLAG = "Telephone Flag";
        public const string SQL_COL_TELEPHONE_NUMBER = "Telephone Number";
        public const string SQL_COL_SEPARATE_CHARGE_CODES = "Separate Charge Codes";
        public const string SQL_COL_LOW_RENT_CUT_OFF = "Low Rent Cut-Off";
        public const string SQL_COL_LOW_RENT_ALLOWANCE = "Low Rent Allowance";
        public const string SQL_COL_LOW_RENT_MAX_1 = "Low Rent Max 1";
        public const string SQL_COL_LOW_RENT_MAX_2 = "Low Rent Max 2";
        public const string SQL_COL_CLEAN_FLAG = "CleanFlag";
        public const string SQL_COL_CLEAN_DAYS = "CleanDays";
        public const string SQL_COL_ERROR_LOG_FLAG = "ErrorLogFlag";
        public const string SQL_COL_ERROR_LOG_DAYS = "ErrorLogDays";
        public const string SQL_COL_HISTORY_FLAG = "HistoryFlag";
        public const string SQL_COL_HISTORY_YEARS = "HistoryYears";
        public const string SQL_COL_MAINT_FLAG = "MaintFlag";
        public const string SQL_COL_MAINT_YEARS = "MaintYears";
        public const string SQL_COL_CLEAN_DATE = "CleanDate";
        public const string SQL_COL_PERCENT_ONE = "Percent One";
        public const string SQL_COL_PERCENT_TWO = "Percent Two";
        public const string SQL_COL_TYPE_COLOR = "TypeColor";
        public const string SQL_COL_LEGAL_COLOR = "LegalColor";
        public const string SQL_COL_GENERAL_COLOR = "GeneralColor";
        public const string SQL_COL_MINIMUM_INCREASE1 = "MinimumIncrease1";
        public const string SQL_COL_MINIMUM_INCREASE2 = "MinimumIncrease2";
        public const string SQL_COL_MINIMUM_INCREASE_RENT = "MinimumIncreaseRent";
        public const string SQL_COL_MINIMUM_INCREASE_YEARS = "MinimumIncreaseYears";
        public const string SQL_COL_DUPLEX_FILE = "duplexFile";
        public const string SQL_COL_DUPLEX_ENABLED = "duplexEnabled";
        public const string SQL_COL_ALERT_MESSAGES = "alertMessages";
        public const string SQL_COL_SCAN = "scan";
        public const string SQL_COL_SENIOR_CITIZEN_CODE = "Senior Citizen Code";
        public const string SQL_COL_WINDOC_DATABASE = "WinDocDatabase";
        public const string SQL_COL_WINGL_DATABASE = "WinGlDatabase";
        public const string SQL_COL_USE_WINDOC = "UseWindoc";
        public const string SQL_COL_USE_WINGL = "UseWingl";
        public const string SQL_COL_SECURITY_CODE = "SecurityCode";

        public static char[] CHARGE_CODE_SEPARATOR = { ',' };

        static string SQL_LOAD = (string)(" SELECT " +
            SQL_COL_CLIENT_ID + "," +
            "[" + SQL_COL_STATUS_REPORT_GROUPING + "]," +
            "[" + SQL_COL_CLOSING_DATE + "]," +
            "[" + SQL_COL_BILLING_DATE + "]," +
            "[" + SQL_COL_LAST_REORG_DATE + "]," +
            "[" + SQL_COL_LAST_ROLL_DATE + "]," +
            "[" + SQL_COL_SECURITY_TRACKER + "]," +
            "[" + SQL_COL_GENERAL_LEDGER + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_FLAG + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A3 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_BNAME + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B3 + "]," +
            "[" + SQL_COL_PAY_TO_FLAG + "]," +
            "[" + SQL_COL_PAY_TO_NAME + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_1 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_2 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_3 + "]," +
            "[" + SQL_COL_GENERAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_LEGAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_TYPE_MSG_LOCATION + "]," +
            "[" + SQL_COL_TELEPHONE_FLAG + "]," +
            "[" + SQL_COL_TELEPHONE_NUMBER + "]," +
            "[" + SQL_COL_SEPARATE_CHARGE_CODES + "]," +
            "[" + SQL_COL_LOW_RENT_CUT_OFF + "]," +
            "[" + SQL_COL_LOW_RENT_ALLOWANCE + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_1 + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_2 + "]," +
            "[" + SQL_COL_CLEAN_FLAG + "]," +
            "[" + SQL_COL_CLEAN_DAYS + "]," +
            "[" + SQL_COL_ERROR_LOG_FLAG + "]," +
            "[" + SQL_COL_ERROR_LOG_DAYS + "]," +
            "[" + SQL_COL_HISTORY_FLAG + "]," +
            "[" + SQL_COL_HISTORY_YEARS + "]," +
            "[" + SQL_COL_MAINT_FLAG + "]," +
            "[" + SQL_COL_MAINT_YEARS + "]," +
            "[" + SQL_COL_CLEAN_DATE + "]," +
            "[" + SQL_COL_PERCENT_ONE + "]," +
            "[" + SQL_COL_PERCENT_TWO + "]," +
            "[" + SQL_COL_TYPE_COLOR + "]," +
            "[" + SQL_COL_LEGAL_COLOR + "]," +
            "[" + SQL_COL_GENERAL_COLOR + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE1 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE2 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_RENT + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_YEARS + "]," +
            "[" + SQL_COL_DUPLEX_FILE + "]," +
            "[" + SQL_COL_DUPLEX_ENABLED + "]," +
            "[" + SQL_COL_ALERT_MESSAGES + "]," +
            "[" + SQL_COL_SCAN + "]," +
            "[" + SQL_COL_SENIOR_CITIZEN_CODE + "]," +
            "[" + SQL_COL_WINDOC_DATABASE + "]," +
            "[" + SQL_COL_WINGL_DATABASE + "]," +
            "[" + SQL_COL_USE_WINDOC + "]," +
            "[" + SQL_COL_USE_WINGL + "]," +
            "[" + SQL_COL_SECURITY_CODE  + "]" +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_CLIENT_ID + "=?;");

        static string SQL_LOAD_ALL = " SELECT " +
            SQL_COL_CLIENT_ID + "," +
            "[" + SQL_COL_STATUS_REPORT_GROUPING + "]," +
            "[" + SQL_COL_CLOSING_DATE + "]," +
            "[" + SQL_COL_BILLING_DATE + "]," +
            "[" + SQL_COL_LAST_REORG_DATE + "]," +
            "[" + SQL_COL_LAST_ROLL_DATE + "]," +
            "[" + SQL_COL_SECURITY_TRACKER + "]," +
            "[" + SQL_COL_GENERAL_LEDGER + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_FLAG + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A3 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_BNAME + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B3 + "]," +
            "[" + SQL_COL_PAY_TO_FLAG + "]," +
            "[" + SQL_COL_PAY_TO_NAME + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_1 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_2 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_3 + "]," +
            "[" + SQL_COL_GENERAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_LEGAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_TYPE_MSG_LOCATION + "]," +
            "[" + SQL_COL_TELEPHONE_FLAG + "]," +
            "[" + SQL_COL_TELEPHONE_NUMBER + "]," +
            "[" + SQL_COL_SEPARATE_CHARGE_CODES + "]," +
            "[" + SQL_COL_LOW_RENT_CUT_OFF + "]," +
            "[" + SQL_COL_LOW_RENT_ALLOWANCE + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_1 + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_2 + "]," +
            "[" + SQL_COL_CLEAN_FLAG + "]," +
            "[" + SQL_COL_CLEAN_DAYS + "]," +
            "[" + SQL_COL_ERROR_LOG_FLAG + "]," +
            "[" + SQL_COL_ERROR_LOG_DAYS + "]," +
            "[" + SQL_COL_HISTORY_FLAG + "]," +
            "[" + SQL_COL_HISTORY_YEARS + "]," +
            "[" + SQL_COL_MAINT_FLAG + "]," +
            "[" + SQL_COL_MAINT_YEARS + "]," +
            "[" + SQL_COL_CLEAN_DATE + "]," +
            "[" + SQL_COL_PERCENT_ONE + "]," +
            "[" + SQL_COL_PERCENT_TWO + "]," +
            "[" + SQL_COL_TYPE_COLOR + "]," +
            "[" + SQL_COL_LEGAL_COLOR + "]," +
            "[" + SQL_COL_GENERAL_COLOR + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE1 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE2 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_RENT + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_YEARS + "]," +
            "[" + SQL_COL_DUPLEX_FILE + "]," +
            "[" + SQL_COL_DUPLEX_ENABLED + "]," +
            "[" + SQL_COL_ALERT_MESSAGES + "]," +
            "[" + SQL_COL_SCAN + "]," +
            "[" + SQL_COL_SENIOR_CITIZEN_CODE + "]," +
            "[" + SQL_COL_WINDOC_DATABASE + "]," +
            "[" + SQL_COL_WINGL_DATABASE + "]," +
            "[" + SQL_COL_USE_WINDOC + "]," +
            "[" + SQL_COL_USE_WINGL + "]," +
            "[" + SQL_COL_SECURITY_CODE + "]" +
            " FROM " + TABLE + ";";

        static string SQL_INSERT = " INSERT INTO " +
            TABLE + " (" +
            "[" + SQL_COL_STATUS_REPORT_GROUPING + "]," +
            "[" + SQL_COL_CLOSING_DATE + "]," +
            "[" + SQL_COL_BILLING_DATE + "]," +
            "[" + SQL_COL_LAST_REORG_DATE + "]," +
            "[" + SQL_COL_LAST_ROLL_DATE + "]," +
            "[" + SQL_COL_SECURITY_TRACKER + "]," +
            "[" + SQL_COL_GENERAL_LEDGER + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_FLAG + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_A3 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_BNAME + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B1 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B2 + "]," +
            "[" + SQL_COL_RETURN_ADDRESS_B3 + "]," +
            "[" + SQL_COL_PAY_TO_FLAG + "]," +
            "[" + SQL_COL_PAY_TO_NAME + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_1 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_2 + "]," +
            "[" + SQL_COL_STATEMENT_MESSAGE_3 + "]," +
            "[" + SQL_COL_GENERAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_LEGAL_MSG_LOCATION + "]," +
            "[" + SQL_COL_TYPE_MSG_LOCATION + "]," +
            "[" + SQL_COL_TELEPHONE_FLAG + "]," +
            "[" + SQL_COL_TELEPHONE_NUMBER + "]," +
            "[" + SQL_COL_SEPARATE_CHARGE_CODES + "]," +
            "[" + SQL_COL_LOW_RENT_CUT_OFF + "]," +
            "[" + SQL_COL_LOW_RENT_ALLOWANCE + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_1 + "]," +
            "[" + SQL_COL_LOW_RENT_MAX_2 + "]," +
            "[" + SQL_COL_CLEAN_FLAG + "]," +
            "[" + SQL_COL_CLEAN_DAYS + "]," +
            "[" + SQL_COL_ERROR_LOG_FLAG + "]," +
            "[" + SQL_COL_ERROR_LOG_DAYS + "]," +
            "[" + SQL_COL_HISTORY_FLAG + "]," +
            "[" + SQL_COL_HISTORY_YEARS + "]," +
            "[" + SQL_COL_MAINT_FLAG + "]," +
            "[" + SQL_COL_MAINT_YEARS + "]," +
            "[" + SQL_COL_CLEAN_DATE + "]," +
            "[" + SQL_COL_PERCENT_ONE + "]," +
            "[" + SQL_COL_PERCENT_TWO + "]," +
            "[" + SQL_COL_TYPE_COLOR + "]," +
            "[" + SQL_COL_LEGAL_COLOR + "]," +
            "[" + SQL_COL_GENERAL_COLOR + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE1 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE2 + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_RENT + "]," +
            "[" + SQL_COL_MINIMUM_INCREASE_YEARS + "]," +
            "[" + SQL_COL_DUPLEX_FILE + "]," +
            "[" + SQL_COL_DUPLEX_ENABLED + "]," +
            "[" + SQL_COL_ALERT_MESSAGES + "]," +
            "[" + SQL_COL_SCAN + "]," +
            "[" + SQL_COL_SENIOR_CITIZEN_CODE + "]," +
            "[" + SQL_COL_WINDOC_DATABASE + "]," +
            "[" + SQL_COL_WINGL_DATABASE + "]," +
            "[" + SQL_COL_USE_WINDOC + "]," +
            "[" + SQL_COL_USE_WINGL + "]," +
            "[" + SQL_COL_SECURITY_CODE + "]" +
            ") VALUES (\'?\', '?', '?', '?', '?', ?, ?, ?, \'?\', \'?\', \'?\', \'?\', \'?\', \'?\', \'?\', ?, \'?\', \'?\', \'?\', \'?\', ?, ?, ?, ?, \'?\', \'?\', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, \'?\', ?, ?, ?, \'?\', '?', '?', ?, ?, '?');";

        static string SQL_UPDATE = (string)(" UPDATE " +
            TABLE + " SET " +
            "[" + SQL_COL_STATUS_REPORT_GROUPING + "]=\'?\'," +
            "[" + SQL_COL_CLOSING_DATE + "]='?'," +
            "[" + SQL_COL_BILLING_DATE + "]='?'," +
            "[" + SQL_COL_LAST_REORG_DATE + "]='?'," +
            "[" + SQL_COL_LAST_ROLL_DATE + "]='?'," +
            "[" + SQL_COL_SECURITY_TRACKER + "]=?," +
            "[" + SQL_COL_GENERAL_LEDGER + "]=?," +
            "[" + SQL_COL_RETURN_ADDRESS_FLAG + "]=?," +
            "[" + SQL_COL_RETURN_ADDRESS_A1 + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_A2 + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_A3 + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_BNAME + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_B1 + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_B2 + "]=\'?\'," +
            "[" + SQL_COL_RETURN_ADDRESS_B3 + "]=\'?\'," +
            "[" + SQL_COL_PAY_TO_FLAG + "]=?," +
            "[" + SQL_COL_PAY_TO_NAME + "]=\'?\'," +
            "[" + SQL_COL_STATEMENT_MESSAGE_1 + "]=\'?\'," +
            "[" + SQL_COL_STATEMENT_MESSAGE_2 + "]=\'?\'," +
            "[" + SQL_COL_STATEMENT_MESSAGE_3 + "]=\'?\'," +
            "[" + SQL_COL_GENERAL_MSG_LOCATION + "]=?," +
            "[" + SQL_COL_LEGAL_MSG_LOCATION + "]=?," +
            "[" + SQL_COL_TYPE_MSG_LOCATION + "]=?," +
            "[" + SQL_COL_TELEPHONE_FLAG + "]=?," +
            "[" + SQL_COL_TELEPHONE_NUMBER + "]=\'?\'," +
            "[" + SQL_COL_SEPARATE_CHARGE_CODES + "]=\'?\'," +
            "[" + SQL_COL_LOW_RENT_CUT_OFF + "]=?," +
            "[" + SQL_COL_LOW_RENT_ALLOWANCE + "]=?," +
            "[" + SQL_COL_LOW_RENT_MAX_1 + "]=?," +
            "[" + SQL_COL_LOW_RENT_MAX_2 + "]=?," +
            "[" + SQL_COL_CLEAN_FLAG + "]=?," +
            "[" + SQL_COL_CLEAN_DAYS + "]=?," +
            "[" + SQL_COL_ERROR_LOG_FLAG + "]=?," +
            "[" + SQL_COL_ERROR_LOG_DAYS + "]=?," +
            "[" + SQL_COL_HISTORY_FLAG + "]=?," +
            "[" + SQL_COL_HISTORY_YEARS + "]=?," +
            "[" + SQL_COL_MAINT_FLAG + "]=?," +
            "[" + SQL_COL_MAINT_YEARS + "]=?," +
            "[" + SQL_COL_CLEAN_DATE + "]='?'," +
            "[" + SQL_COL_PERCENT_ONE + "]=?," +
            "[" + SQL_COL_PERCENT_TWO + "]=?," +
            "[" + SQL_COL_TYPE_COLOR + "]=?," +
            "[" + SQL_COL_LEGAL_COLOR + "]=?," +
            "[" + SQL_COL_GENERAL_COLOR + "]=?," +
            "[" + SQL_COL_MINIMUM_INCREASE1 + "]=?," +
            "[" + SQL_COL_MINIMUM_INCREASE2 + "]=?," +
            "[" + SQL_COL_MINIMUM_INCREASE_RENT + "]=?," +
            "[" + SQL_COL_MINIMUM_INCREASE_YEARS + "]=?," +
            "[" + SQL_COL_DUPLEX_FILE + "]=\'?\'," +
            "[" + SQL_COL_DUPLEX_ENABLED + "]=?," +
            "[" + SQL_COL_ALERT_MESSAGES + "]=?," +
            "[" + SQL_COL_SCAN + "]=?," +
            "[" + SQL_COL_SENIOR_CITIZEN_CODE + "]='?'," +
            "[" + SQL_COL_WINDOC_DATABASE + "]='?'," +
            "[" + SQL_COL_WINGL_DATABASE + "]='?'," +
            "[" + SQL_COL_USE_WINDOC + "]=?," +
            "[" + SQL_COL_USE_WINGL + "]=?," +
            "[" + SQL_COL_SECURITY_CODE + "]='?'" +
           " WHERE " + SQL_COL_CLIENT_ID + "=?");

        protected int m_clientId;
        protected string m_StatusReportGrouping;
        protected DateTime m_ClosingDate = GlobalShared.NULL_DATE;
        protected DateTime m_Billingdate = GlobalShared.NULL_DATE;
        protected DateTime m_LastReorgDate = GlobalShared.NULL_DATE;
        protected DateTime m_LastRollDate = GlobalShared.NULL_DATE;
        protected bool m_SecurityTracker;
        protected bool m_GeneralLedger;
        protected short m_ReturnAddressFlag;
        protected string m_ReturnAddressA1;
        protected string m_ReturnAddressA2;
        protected string m_ReturnAddressA3;
        protected string m_ReturnAddressBname;
        protected string m_ReturnAddressB1;
        protected string m_ReturnAddressB2;
        protected string m_ReturnAddressB3;
        protected short m_PayToFlag;
        protected string m_PayToName;
        protected string m_StatementMessage1;
        protected string m_StatementMessage2;
        protected string m_StatementMessage3;
        protected short m_GeneralMsgLocation;
        protected short m_LegalMsgLocation;
        protected short m_TypeMsgLocation;
        protected short m_TelephoneFlag;
        protected string m_TelephoneNumber;
        protected string m_SeparateChargeCodes;
        protected decimal m_LowRentCutOff;
        protected decimal m_LowRentAllowance;
        protected decimal m_LowRentMax1;
        protected decimal m_LowRentMax2;
        protected bool m_CleanFlag;
        protected short m_CleanDays;
        protected bool m_ErrorLogFlag;
        protected short m_ErrorLogDays;
        protected bool m_HistoryFlag;
        protected short m_HistoryYears;
        protected bool m_MaintFlag;
        protected short m_MaintYears;
        protected DateTime m_CleanDate = GlobalShared.NULL_DATE;
        protected float m_PercentOne;
        protected float m_PercentTwo;
        protected int m_TypeColor;
        protected int m_LegalColor;
        protected int m_GeneralColor;
        protected decimal m_MinimumIncrease1;
        protected decimal m_MinimumIncrease2;
        protected decimal m_MinimumIncreaseRent;
        protected short m_MinimumIncreaseYears;
        protected string m_duplexFile;
        protected bool m_duplexEnabled;
        protected bool m_alertMessages;
        protected bool m_scan;
        protected string m_SeniorCitizenCode;
        protected string m_WindocDatabase;
        protected string m_WinglDatabase;
        protected bool m_UseWindoc;
        protected bool m_UseWingl;
        protected string m_SecurityCode;
        protected bool m_isChanged;

        protected string[] leaseChargeCodes;

        public Control()
            : base(TABLE, null, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
        }

        public Control(string StatusReportGrouping, DateTime ClosingDate, DateTime Billingdate, DateTime LastReorgDate, 
                       DateTime LastRollDate, bool SecurityTracker, bool GeneralLedger, short ReturnAddressFlag, string ReturnAddressA1, 
                       string ReturnAddressA2, string ReturnAddressA3, string ReturnAddressBname, string ReturnAddressB1, 
                       string ReturnAddressB2, string ReturnAddressB3, short PayToFlag, string PayToName, string StatementMessage1, 
                       string StatementMessage2, string StatementMessage3, short GeneralMsgLocation, short LegalMsgLocation, 
                       short TypeMsgLocation, short TelephoneFlag, string TelephoneNumber, string SeparateChargeCodes, 
                       decimal LowRentCutOff, decimal LowRentAllowance, decimal LowRentMax1, decimal LowRentMax2, bool CleanFlag, 
                       short CleanDays, bool ErrorLogFlag, short ErrorLogDays, bool HistoryFlag, short HistoryYears, bool MaintFlag, 
                       short MaintYears, DateTime CleanDate, float PercentOne, float PercentTwo, int TypeColor, int LegalColor, 
                       int GeneralColor, decimal MinimumIncrease1, decimal MinimumIncrease2, decimal MinimumIncreaseRent, 
                       short MinimumIncreaseYears, string duplexFile, bool duplexEnabled, bool alertMessages, bool scan, 
                       string SeniorCitizenCode, string WindocDatabase, string WinglDatabase, bool UseWinDoc, bool UseWingl, string SecurityCode)
            : base(TABLE, null, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            m_StatusReportGrouping = StatusReportGrouping;
            m_ClosingDate = ClosingDate;
            m_Billingdate = Billingdate;
            m_LastReorgDate = LastReorgDate;
            m_LastRollDate = LastRollDate;
            m_SecurityTracker = SecurityTracker;
            m_GeneralLedger = GeneralLedger;
            m_ReturnAddressFlag = ReturnAddressFlag;
            m_ReturnAddressA1 = ReturnAddressA1;
            m_ReturnAddressA2 = ReturnAddressA2;
            m_ReturnAddressA3 = ReturnAddressA3;
            m_ReturnAddressBname = ReturnAddressBname;
            m_ReturnAddressB1 = ReturnAddressB1;
            m_ReturnAddressB2 = ReturnAddressB2;
            m_ReturnAddressB3 = ReturnAddressB3;
            m_PayToFlag = PayToFlag;
            m_PayToName = PayToName;
            m_StatementMessage1 = StatementMessage1;
            m_StatementMessage2 = StatementMessage2;
            m_StatementMessage3 = StatementMessage3;
            m_GeneralMsgLocation = GeneralMsgLocation;
            m_LegalMsgLocation = LegalMsgLocation;
            m_TypeMsgLocation = TypeMsgLocation;
            m_TelephoneFlag = TelephoneFlag;
            m_TelephoneNumber = TelephoneNumber;
            m_SeparateChargeCodes = SeparateChargeCodes;
            m_LowRentCutOff = LowRentCutOff;
            m_LowRentAllowance = LowRentAllowance;
            m_LowRentMax1 = LowRentMax1;
            m_LowRentMax2 = LowRentMax2;
            m_CleanFlag = CleanFlag;
            m_CleanDays = CleanDays;
            m_ErrorLogFlag = ErrorLogFlag;
            m_ErrorLogDays = ErrorLogDays;
            m_HistoryFlag = HistoryFlag;
            m_HistoryYears = HistoryYears;
            m_MaintFlag = MaintFlag;
            m_MaintYears = MaintYears;
            m_CleanDate = CleanDate;
            m_PercentOne = PercentOne;
            m_PercentTwo = PercentTwo;
            m_TypeColor = TypeColor;
            m_LegalColor = LegalColor;
            m_GeneralColor = GeneralColor;
            m_MinimumIncrease1 = MinimumIncrease1;
            m_MinimumIncrease2 = MinimumIncrease2;
            m_MinimumIncreaseRent = MinimumIncreaseRent;
            m_MinimumIncreaseYears = MinimumIncreaseYears;
            m_duplexFile = duplexFile;
            m_duplexEnabled = duplexEnabled;
            m_alertMessages = alertMessages;
            m_scan = scan;
            m_SeniorCitizenCode = SeniorCitizenCode;
            m_WindocDatabase = WindocDatabase;
            m_WinglDatabase = WinglDatabase;
            m_UseWindoc = UseWinDoc;
            m_UseWingl = UseWingl;
            m_SecurityCode = SecurityCode;
        }

        protected Control fill(DataRow dr)
        {

            try
            {
                m_clientId = (int)dr[SQL_COL_CLIENT_ID];
                m_StatusReportGrouping = (string)((Information.IsDBNull(dr[SQL_COL_STATUS_REPORT_GROUPING])) ? "" : (dr[SQL_COL_STATUS_REPORT_GROUPING]));
                m_ClosingDate = (Information.IsDBNull((DateTime)dr[SQL_COL_CLOSING_DATE]) || (DateTime)dr[SQL_COL_CLOSING_DATE] < GlobalShared.NULL_DATE ? GlobalShared.NULL_DATE : (DateTime)dr[SQL_COL_CLOSING_DATE]);
                m_Billingdate = (Information.IsDBNull((DateTime)dr[SQL_COL_BILLING_DATE]) || (DateTime)dr[SQL_COL_BILLING_DATE] < GlobalShared.NULL_DATE ? GlobalShared.NULL_DATE : (DateTime)dr[SQL_COL_BILLING_DATE]);
                m_LastReorgDate = (Information.IsDBNull((DateTime)dr[SQL_COL_LAST_REORG_DATE]) || (DateTime)dr[SQL_COL_LAST_REORG_DATE] < GlobalShared.NULL_DATE ? GlobalShared.NULL_DATE : (DateTime)dr[SQL_COL_LAST_REORG_DATE]);
                m_LastRollDate = (Information.IsDBNull((DateTime)dr[SQL_COL_LAST_ROLL_DATE]) || (DateTime)dr[SQL_COL_LAST_ROLL_DATE] < GlobalShared.NULL_DATE ? GlobalShared.NULL_DATE : (DateTime)dr[SQL_COL_LAST_ROLL_DATE]);
                m_SecurityTracker = System.Convert.ToBoolean(dr[SQL_COL_SECURITY_TRACKER]);
                m_GeneralLedger = System.Convert.ToBoolean(dr[SQL_COL_GENERAL_LEDGER]);
                m_ReturnAddressFlag = System.Convert.ToInt16(dr[SQL_COL_RETURN_ADDRESS_FLAG]);
                m_ReturnAddressA1 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_A1])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_A1]));
                m_ReturnAddressA2 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_A2])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_A2]));
                m_ReturnAddressA3 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_A3])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_A3]));
                m_ReturnAddressBname = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_BNAME])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_BNAME]));
                m_ReturnAddressB1 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_B1])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_B1]));
                m_ReturnAddressB2 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_B2])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_B2]));
                m_ReturnAddressB3 = (string)((Information.IsDBNull(dr[SQL_COL_RETURN_ADDRESS_B3])) ? "" : (dr[SQL_COL_RETURN_ADDRESS_B3]));
                m_PayToFlag = System.Convert.ToInt16(dr[SQL_COL_PAY_TO_FLAG]);
                m_PayToName = (string)((Information.IsDBNull(dr[SQL_COL_PAY_TO_NAME])) ? "" : (dr[SQL_COL_PAY_TO_NAME]));
                m_StatementMessage1 = (string)((Information.IsDBNull(dr[SQL_COL_STATEMENT_MESSAGE_1])) ? "" : (dr[SQL_COL_STATEMENT_MESSAGE_1]));
                m_StatementMessage2 = (string)((Information.IsDBNull(dr[SQL_COL_STATEMENT_MESSAGE_2])) ? "" : (dr[SQL_COL_STATEMENT_MESSAGE_2]));
                m_StatementMessage3 = (string)((Information.IsDBNull(dr[SQL_COL_STATEMENT_MESSAGE_3])) ? "" : (dr[SQL_COL_STATEMENT_MESSAGE_3]));
                m_GeneralMsgLocation = System.Convert.ToInt16(dr[SQL_COL_GENERAL_MSG_LOCATION]);
                m_LegalMsgLocation = System.Convert.ToInt16(dr[SQL_COL_LEGAL_MSG_LOCATION]);
                m_TypeMsgLocation = System.Convert.ToInt16(dr[SQL_COL_TYPE_MSG_LOCATION]);
                m_TelephoneFlag = System.Convert.ToInt16(dr[SQL_COL_TELEPHONE_FLAG]);
                m_TelephoneNumber = (string)((Information.IsDBNull(dr[SQL_COL_TELEPHONE_NUMBER])) ? "" : (dr[SQL_COL_TELEPHONE_NUMBER]));
                m_SeparateChargeCodes = (string)((Information.IsDBNull(dr[SQL_COL_SEPARATE_CHARGE_CODES])) ? "" : (dr[SQL_COL_SEPARATE_CHARGE_CODES]));
                m_LowRentCutOff = System.Convert.ToDecimal(dr[SQL_COL_LOW_RENT_CUT_OFF]);
                m_LowRentAllowance = System.Convert.ToDecimal(dr[SQL_COL_LOW_RENT_ALLOWANCE]);
                m_LowRentMax1 = System.Convert.ToDecimal(dr[SQL_COL_LOW_RENT_MAX_1]);
                m_LowRentMax2 = System.Convert.ToDecimal(dr[SQL_COL_LOW_RENT_MAX_2]);
                m_CleanFlag = System.Convert.ToBoolean(dr[SQL_COL_CLEAN_FLAG]);
                m_CleanDays = System.Convert.ToInt16(dr[SQL_COL_CLEAN_DAYS]);
                m_ErrorLogFlag = System.Convert.ToBoolean(dr[SQL_COL_ERROR_LOG_FLAG]);
                m_ErrorLogDays = System.Convert.ToInt16(dr[SQL_COL_ERROR_LOG_DAYS]);
                m_HistoryFlag = System.Convert.ToBoolean(dr[SQL_COL_HISTORY_FLAG]);
                m_HistoryYears = System.Convert.ToInt16(dr[SQL_COL_HISTORY_YEARS]);
                m_MaintFlag = System.Convert.ToBoolean(dr[SQL_COL_MAINT_FLAG]);
                m_MaintYears = System.Convert.ToInt16(dr[SQL_COL_MAINT_YEARS]);
                m_CleanDate = (Information.IsDBNull((DateTime)dr[SQL_COL_CLEAN_DATE]) || (DateTime)dr[SQL_COL_CLEAN_DATE] < GlobalShared.NULL_DATE ? GlobalShared.NULL_DATE : (DateTime)dr[SQL_COL_CLEAN_DATE]);
                m_PercentOne = System.Convert.ToSingle(dr[SQL_COL_PERCENT_ONE]);
                m_PercentTwo = System.Convert.ToSingle(dr[SQL_COL_PERCENT_TWO]);
                m_TypeColor = System.Convert.ToInt32(dr[SQL_COL_TYPE_COLOR]);
                m_LegalColor = System.Convert.ToInt32(dr[SQL_COL_LEGAL_COLOR]);
                m_GeneralColor = System.Convert.ToInt32(dr[SQL_COL_GENERAL_COLOR]);
                m_MinimumIncrease1 = System.Convert.ToDecimal(dr[SQL_COL_MINIMUM_INCREASE1]);
                m_MinimumIncrease2 = System.Convert.ToDecimal(dr[SQL_COL_MINIMUM_INCREASE2]);
                m_MinimumIncreaseRent = System.Convert.ToDecimal(dr[SQL_COL_MINIMUM_INCREASE_RENT]);
                m_MinimumIncreaseYears = System.Convert.ToInt16(dr[SQL_COL_MINIMUM_INCREASE_YEARS]);
                m_duplexFile = (string)((Information.IsDBNull(dr[SQL_COL_DUPLEX_FILE])) ? "" : (dr[SQL_COL_DUPLEX_FILE]));
                m_duplexEnabled = System.Convert.ToBoolean(dr[SQL_COL_DUPLEX_ENABLED]);
                m_alertMessages = System.Convert.ToBoolean(dr[SQL_COL_ALERT_MESSAGES]);
                m_scan = System.Convert.ToBoolean(dr[SQL_COL_SCAN]);
                m_SeniorCitizenCode = (string)((Information.IsDBNull(dr[SQL_COL_SENIOR_CITIZEN_CODE])) ? "" : (dr[SQL_COL_SENIOR_CITIZEN_CODE]));
                m_WindocDatabase = (string)((Convert.IsDBNull(dr[SQL_COL_WINDOC_DATABASE])) ? "" : dr[SQL_COL_WINDOC_DATABASE]);
                m_WinglDatabase = (string)((Convert.IsDBNull(dr[SQL_COL_WINGL_DATABASE])) ? "" : dr[SQL_COL_WINGL_DATABASE]);
                m_UseWindoc = System.Convert.ToBoolean(dr[SQL_COL_USE_WINDOC]);
                m_UseWingl = System.Convert.ToBoolean(dr[SQL_COL_USE_WINGL]);
                m_SecurityCode = (string)((Convert.IsDBNull(dr[SQL_COL_SECURITY_CODE])) ? "" : (dr[SQL_COL_SECURITY_CODE]));
                // Parse Lease Separate Charge Codes
                if (m_SeparateChargeCodes != null && m_SeparateChargeCodes.Length > 0)
                    leaseChargeCodes = m_SeparateChargeCodes.Split(CHARGE_CODE_SEPARATOR);
            }
            catch (OleDbException)
            {
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static Control load(long clientId, out string msg)
        {
            msg = "";
            Control obj = new Control();
            string[] opt = new string[] { clientId.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                return obj.fill(dr);
            }
            else {
                msg = SQL_LOAD;
                return null; }

        }

        public static Control[] load(out string msg)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Control obj = new Control();

            ds = obj.loadAll();
            obj = null;
            msg = "";

            if (ds == null || ds.Tables.Count == 0)
            {
                msg = SQL_LOAD_ALL;
                return null;
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Control();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((Control[])(list.ToArray(typeof(Control))));
            }
            catch (OleDbException)
            {
                return null;
            }

        }

        public bool save()
        {

            if (m_clientId > 0)
            {
                string[] values = new string[] { m_StatusReportGrouping, m_ClosingDate.ToString(), m_Billingdate.ToString(), m_LastReorgDate.ToString(), m_LastRollDate.ToString(), m_SecurityTracker.ToString(), m_GeneralLedger.ToString(), m_ReturnAddressFlag.ToString(), m_ReturnAddressA1, m_ReturnAddressA2, m_ReturnAddressA3, m_ReturnAddressBname, m_ReturnAddressB1, m_ReturnAddressB2, m_ReturnAddressB3, m_PayToFlag.ToString(), m_PayToName, m_StatementMessage1, m_StatementMessage2, m_StatementMessage3, m_GeneralMsgLocation.ToString(), m_LegalMsgLocation.ToString(), m_TypeMsgLocation.ToString(), m_TelephoneFlag.ToString(), m_TelephoneNumber, m_SeparateChargeCodes, m_LowRentCutOff.ToString(), m_LowRentAllowance.ToString(), m_LowRentMax1.ToString(), m_LowRentMax2.ToString(), m_CleanFlag.ToString(), m_CleanDays.ToString(), m_ErrorLogFlag.ToString(), m_ErrorLogDays.ToString(), m_HistoryFlag.ToString(), m_HistoryYears.ToString(), m_MaintFlag.ToString(), m_MaintYears.ToString(), m_CleanDate.ToString(), m_PercentOne.ToString(), m_PercentTwo.ToString(), m_TypeColor.ToString(), m_LegalColor.ToString(), m_GeneralColor.ToString(), m_MinimumIncrease1.ToString(), m_MinimumIncrease2.ToString(), m_MinimumIncreaseRent.ToString(), m_MinimumIncreaseYears.ToString(), m_duplexFile, m_duplexEnabled.ToString(), m_alertMessages.ToString(), m_scan.ToString(), m_SeniorCitizenCode, m_WindocDatabase, m_WinglDatabase, m_UseWindoc.ToString(), m_UseWingl.ToString(), m_SecurityCode, m_clientId.ToString() };
                if (this.updateRecord(m_clientId.ToString(), values))
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
                string[] values = new string[] { m_StatusReportGrouping, m_ClosingDate.ToString(), m_Billingdate.ToString(), m_LastReorgDate.ToString(), m_LastRollDate.ToString(), m_SecurityTracker.ToString(), m_GeneralLedger.ToString(), m_ReturnAddressFlag.ToString(), m_ReturnAddressA1, m_ReturnAddressA2, m_ReturnAddressA3, m_ReturnAddressBname, m_ReturnAddressB1, m_ReturnAddressB2, m_ReturnAddressB3, m_PayToFlag.ToString(), m_PayToName, m_StatementMessage1, m_StatementMessage2, m_StatementMessage3, m_GeneralMsgLocation.ToString(), m_LegalMsgLocation.ToString(), m_TypeMsgLocation.ToString(), m_TelephoneFlag.ToString(), m_TelephoneNumber, m_SeparateChargeCodes, m_LowRentCutOff.ToString(), m_LowRentAllowance.ToString(), m_LowRentMax1.ToString(), m_LowRentMax2.ToString(), m_CleanFlag.ToString(), m_CleanDays.ToString(), m_ErrorLogFlag.ToString(), m_ErrorLogDays.ToString(), m_HistoryFlag.ToString(), m_HistoryYears.ToString(), m_MaintFlag.ToString(), m_MaintYears.ToString(), m_CleanDate.ToString(), m_PercentOne.ToString(), m_PercentTwo.ToString(), m_TypeColor.ToString(), m_LegalColor.ToString(), m_GeneralColor.ToString(), m_MinimumIncrease1.ToString(), m_MinimumIncrease2.ToString(), m_MinimumIncreaseRent.ToString(), m_MinimumIncreaseYears.ToString(), m_duplexFile, m_duplexEnabled.ToString(), m_alertMessages.ToString(), m_scan.ToString(), m_SeniorCitizenCode, m_WindocDatabase, m_WinglDatabase, m_UseWindoc.ToString(), m_UseWingl.ToString(), m_SecurityCode };

                if (m_clientId >= 0)
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
                cmd.CommandText = "CREATE TABLE Control (Status Report Grouping TEXT(1),Closing Date DATETIME,Billing date DATETIME,Last Reorg Date DATETIME,Last Roll Date DATETIME,Security Tracker YESNO,General Ledger YESNO,Return Address Flag INTEGER,Return Address A1 TEXT(30),Return Address A2 TEXT(30),Return Address A3 TEXT(30),Return Address Bname TEXT(30),Return Address B1 TEXT(30),Return Address B2 TEXT(30),Return Address B3 TEXT(30),Pay To Flag INTEGER,Pay To Name TEXT(30),Statement Message 1 TEXT(60),Statement Message 2 TEXT(60),Statement Message 3 TEXT(60),General Msg Location INTEGER,Legal Msg Location INTEGER,Type Msg Location INTEGER,Telephone Flag INTEGER,Telephone Number TEXT(12),Separate Charge Codes TEXT(10),Low Rent Cut-Off CURRENCY,Low Rent Allowance CURRENCY,Low Rent Max 1 CURRENCY,Low Rent Max 2 CURRENCY,CleanFlag YESNO,CleanDays INTEGER,ErrorLogFlag YESNO,ErrorLogDays INTEGER,HistoryFlag YESNO,HistoryYears INTEGER,MaintFlag YESNO,MaintYears INTEGER,CleanDate DATETIME,Percent One SINGLE,Percent Two SINGLE,TypeColor LONG,LegalColor LONG,GeneralColor LONG,MinimumIncrease1 CURRENCY,MinimumIncrease2 CURRENCY,MinimumIncreaseRent CURRENCY,MinimumIncreaseYears INTEGER,duplexFile TEXT(128),duplexEnabled YESNO,alertMessages YESNO,scan YESNO,Senior Citizen Code TEXT(1))";
                cmd.Connection = CN;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.OleDb.OleDbException)
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

        public int ClientId
        {
            get { return m_clientId; }
            set 
            { 
                m_clientId = value;
                OnPropertyChanged("ClientId");
            }
        }
        public bool GeneralLedger
        {
            get { return m_GeneralLedger; }
            set
            {
                m_GeneralLedger = value;
                OnPropertyChanged("GeneralLedger");
            }
        }
        public string StatusReportGrouping
        {
            get
            {
                return m_StatusReportGrouping;
            }
            set
            {
                m_StatusReportGrouping = value;
                OnPropertyChanged("StatusReportGrouping");
            }
        }

        public DateTime ClosingDate
        {
            get
            {
                return m_ClosingDate;
            }
            set
            {
                m_ClosingDate = value;
                OnPropertyChanged("ClosingDate");
            }
        }

        public DateTime BillingDate
        {
            get
            {
                return m_Billingdate;
            }
            set
            {
                m_Billingdate = value;
                OnPropertyChanged("Billingdate");
            }
        }

        public DateTime LastReorgDate
        {
            get
            {
                return m_LastReorgDate;
            }
            set
            {
                m_LastReorgDate = value;
                OnPropertyChanged("LastReorgDate");
            }
        }

        public DateTime LastRollDate
        {
            get
            {
                return m_LastRollDate;
            }
            set
            {
                m_LastRollDate = value;
                OnPropertyChanged("LastRollDate");
            }
        }

        public bool SecurityTracker
        {
            get
            {
                return m_SecurityTracker;
            }
            set
            {
                m_SecurityTracker = value;
                OnPropertyChanged("SecurityTracker");
            }
        }

        public short ReturnAddressFlag
        {
            get
            {
                return m_ReturnAddressFlag;
            }
            set
            {
                m_ReturnAddressFlag = value;
                OnPropertyChanged("ReturnAddressFlag");
            }
        }

        public string ReturnAddressA1
        {
            get
            {
                return m_ReturnAddressA1;
            }
            set
            {
                m_ReturnAddressA1 = value;
                OnPropertyChanged("ReturnAddressA1");
            }
        }

        public string ReturnAddressA2
        {
            get
            {
                return m_ReturnAddressA2;
            }
            set
            {
                m_ReturnAddressA2 = value;
                OnPropertyChanged("ReturnAddressA2");
            }
        }

        public string ReturnAddressA3
        {
            get
            {
                return m_ReturnAddressA3;
            }
            set
            {
                m_ReturnAddressA3 = value;
                OnPropertyChanged("ReturnAddressA3");
            }
        }

        public string ReturnAddressBname
        {
            get
            {
                return m_ReturnAddressBname;
            }
            set
            {
                m_ReturnAddressBname = value;
                OnPropertyChanged("ReturnAddressBname");
            }
        }

        public string ReturnAddressB1
        {
            get
            {
                return m_ReturnAddressB1;
            }
            set
            {
                m_ReturnAddressB1 = value;
                OnPropertyChanged("ReturnAddressB1");
            }
        }

        public string ReturnAddressB2
        {
            get
            {
                return m_ReturnAddressB2;
            }
            set
            {
                m_ReturnAddressB2 = value;
                OnPropertyChanged("ReturnAddressB2");
            }
        }

        public string ReturnAddressB3
        {
            get
            {
                return m_ReturnAddressB3;
            }
            set
            {
                m_ReturnAddressB3 = value;
                OnPropertyChanged("ReturnAddressB3");
            }
        }

        public short PayToFlag
        {
            get
            {
                return m_PayToFlag;
            }
            set
            {
                m_PayToFlag = value;
                OnPropertyChanged("PayToFlag");
            }
        }

        public string PayToName
        {
            get
            {
                return m_PayToName;
            }
            set
            {
                m_PayToName = value;
                OnPropertyChanged("PayToName");
            }
        }

        public string StatementMessage1
        {
            get
            {
                return m_StatementMessage1;
            }
            set
            {
                m_StatementMessage1 = value;
                OnPropertyChanged("StatementMessage1");
            }
        }

        public string StatementMessage2
        {
            get
            {
                return m_StatementMessage2;
            }
            set
            {
                m_StatementMessage2 = value;
                OnPropertyChanged("StatementMessage2");
            }
        }

        public string StatementMessage3
        {
            get
            {
                return m_StatementMessage3;
            }
            set
            {
                m_StatementMessage3 = value;
                OnPropertyChanged("StatementMessage3");
            }
        }

        public short GeneralMsgLocation
        {
            get
            {
                return m_GeneralMsgLocation;
            }
            set
            {
                m_GeneralMsgLocation = value;
                OnPropertyChanged("GeneralMsgLocation");
            }
        }

        public short LegalMsgLocation
        {
            get
            {
                return m_LegalMsgLocation;
            }
            set
            {
                m_LegalMsgLocation = value;
                OnPropertyChanged("LegalMsgLocation");
            }
        }

        public short TypeMsgLocation
        {
            get
            {
                return m_TypeMsgLocation;
            }
            set
            {
                m_TypeMsgLocation = value;
                OnPropertyChanged("TypeMsgLocation");
            }
        }

        public short TelephoneFlag
        {
            get
            {
                return m_TelephoneFlag;
            }
            set
            {
                m_TelephoneFlag = value;
                OnPropertyChanged("TelephoneFlag");
            }
        }

        public string TelephoneNumber
        {
            get
            {
                return m_TelephoneNumber;
            }
            set
            {
                m_TelephoneNumber = value;
                OnPropertyChanged("TelephoneNumber");
            }
        }

        public string SeparateChargeCodes
        {
            get
            {
                return m_SeparateChargeCodes;
            }
            set
            {
                m_SeparateChargeCodes = value;
                OnPropertyChanged("SeparateChargeCodes");
            }
        }

        public decimal LowRentCutOff
        {
            get
            {
                return m_LowRentCutOff;
            }
            set
            {
                m_LowRentCutOff = value;
                OnPropertyChanged("LowRentCutOff");
            }
        }

        public decimal LowRentAllowance
        {
            get
            {
                return m_LowRentAllowance;
            }
            set
            {
                m_LowRentAllowance = value;
                OnPropertyChanged("LowRentAllowance");
            }
        }

        public decimal LowRentMax1
        {
            get
            {
                return m_LowRentMax1;
            }
            set
            {
                m_LowRentMax1 = value;
                OnPropertyChanged("LowRentMax1");
            }
        }

        public decimal LowRentMax2
        {
            get
            {
                return m_LowRentMax2;
            }
            set
            {
                m_LowRentMax2 = value;
                OnPropertyChanged("LowRentMax2");
            }
        }

        public bool CleanFlag
        {
            get
            {
                return m_CleanFlag;
            }
            set
            {
                m_CleanFlag = value;
                OnPropertyChanged("CleanFlag");
            }
        }

        public short CleanDays
        {
            get
            {
                return m_CleanDays;
            }
            set
            {
                m_CleanDays = value;
                OnPropertyChanged("CleanDays");
            }
        }

        public bool ErrorLogFlag
        {
            get
            {
                return m_ErrorLogFlag;
            }
            set
            {
                m_ErrorLogFlag = value;
                OnPropertyChanged("ErrorLogFlag");
            }
        }

        public short ErrorLogDays
        {
            get
            {
                return m_ErrorLogDays;
            }
            set
            {
                m_ErrorLogDays = value;
                OnPropertyChanged("ErrorLogDays");
            }
        }

        public bool HistoryFlag
        {
            get
            {
                return m_HistoryFlag;
            }
            set
            {
                m_HistoryFlag = value;
                OnPropertyChanged("HistoryFlag");
            }
        }

        public short HistoryYears
        {
            get
            {
                return m_HistoryYears;
            }
            set
            {
                m_HistoryYears = value;
                OnPropertyChanged("HistoryYears");
            }
        }

        public bool MaintFlag
        {
            get
            {
                return m_MaintFlag;
            }
            set
            {
                m_MaintFlag = value;
                OnPropertyChanged("MaintFlag");
            }
        }

        public short MaintYears
        {
            get
            {
                return m_MaintYears;
            }
            set
            {
                m_MaintYears = value;
                OnPropertyChanged("MaintYears");
            }
        }

        public DateTime CleanDate
        {
            get
            {
                return m_CleanDate;
            }
            set
            {
                m_CleanDate = value;
                OnPropertyChanged("CleanDate");
            }
        }

        public float PercentOne
        {
            get
            {
                return m_PercentOne;
            }
            set
            {
                m_PercentOne = value;
                OnPropertyChanged("PercentOne");
            }
        }

        public float PercentTwo
        {
            get
            {
                return m_PercentTwo;
            }
            set
            {
                m_PercentTwo = value;
                OnPropertyChanged("PercentTwo");
            }
        }

        public int TypeColor
        {
            get
            {
                return m_TypeColor;
            }
            set
            {
                m_TypeColor = value;
                OnPropertyChanged("TypeColor");
            }
        }

        public int LegalColor
        {
            get
            {
                return m_LegalColor;
            }
            set
            {
                m_LegalColor = value;
                OnPropertyChanged("LegalColor");
            }
        }

        public int GeneralColor
        {
            get
            {
                return m_GeneralColor;
            }
            set
            {
                m_GeneralColor = value;
                OnPropertyChanged("GeneralColor");
            }
        }

        public decimal MinimumIncrease1
        {
            get
            {
                return m_MinimumIncrease1;
            }
            set
            {
                m_MinimumIncrease1 = value;
                OnPropertyChanged("MinimumIncrease1");
            }
        }

        public decimal MinimumIncrease2
        {
            get
            {
                return m_MinimumIncrease2;
            }
            set
            {
                m_MinimumIncrease2 = value;
                OnPropertyChanged("MinimumIncrease2");
            }
        }

        public decimal MinimumIncreaseRent
        {
            get
            {
                return m_MinimumIncreaseRent;
            }
            set
            {
                m_MinimumIncreaseRent = value;
                OnPropertyChanged("MinimumIncreaseRent");
            }
        }

        public short MinimumIncreaseYears
        {
            get
            {
                return m_MinimumIncreaseYears;
            }
            set
            {
                m_MinimumIncreaseYears = value;
                OnPropertyChanged("MinimumIncreaseYears");
            }
        }

        public string duplexFile
        {
            get
            {
                return m_duplexFile;
            }
            set
            {
                m_duplexFile = value;
                OnPropertyChanged("duplexFile");
            }
        }

        public bool duplexEnabled
        {
            get
            {
                return m_duplexEnabled;
            }
            set
            {
                m_duplexEnabled = value;
                OnPropertyChanged("duplexEnabled");
            }
        }

        public bool alertMessages
        {
            get
            {
                return m_alertMessages;
            }
            set
            {
                m_alertMessages = value;
                OnPropertyChanged("alertMessages");
            }
        }

        public bool scan
        {
            get
            {
                return m_scan;
            }
            set
            {
                m_scan = value;
                OnPropertyChanged("scan");
            }
        }

        public string SeniorCitizenCode
        {
            get { return m_SeniorCitizenCode;}
            set
            {
                m_SeniorCitizenCode = value;
                OnPropertyChanged("SeniorCitizenCode");
            }
        }

        // Separate Charge Code Properties
        public string ChargeCode0
        {
            get { return leaseChargeCodes[0]; }
            set 
            {
                if (leaseChargeCodes.Length < 1)
                    Array.Resize(ref leaseChargeCodes, 1);
                leaseChargeCodes[0] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode1
        {
            get { return leaseChargeCodes[1]; }
            set
            {
                if (leaseChargeCodes.Length < 2)
                    Array.Resize(ref leaseChargeCodes, 2);
                leaseChargeCodes[1] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode2
        {
            get { return leaseChargeCodes[2]; }
            set
            {
                if (leaseChargeCodes.Length < 3)
                    Array.Resize(ref leaseChargeCodes, 3);
                leaseChargeCodes[2] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode3
        {
            get { return leaseChargeCodes[3]; }
            set
            {
                if (leaseChargeCodes.Length < 4)
                    Array.Resize(ref leaseChargeCodes, 4);
                leaseChargeCodes[3] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode4
        {
            get { return leaseChargeCodes[4]; }
            set
            {
                if (leaseChargeCodes.Length < 5)
                    Array.Resize(ref leaseChargeCodes, 5);
                leaseChargeCodes[4] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode5
        {
            get { return leaseChargeCodes[5]; }
            set
            {
                if (leaseChargeCodes.Length < 6)
                    Array.Resize(ref leaseChargeCodes, 6);
                leaseChargeCodes[5] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode6
        {
            get { return leaseChargeCodes[6]; }
            set
            {
                if (leaseChargeCodes.Length < 7)
                    Array.Resize(ref leaseChargeCodes, 7);
                leaseChargeCodes[6] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode7
        {
            get { return leaseChargeCodes[7]; }
            set
            {
                if (leaseChargeCodes.Length < 8)
                    Array.Resize(ref leaseChargeCodes, 8);
                leaseChargeCodes[7] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode8
        {
            get { return leaseChargeCodes[8]; }
            set
            {
                if (leaseChargeCodes.Length < 9)
                    Array.Resize(ref leaseChargeCodes, 9);
                leaseChargeCodes[8] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string ChargeCode9
        {
            get { return leaseChargeCodes[9]; }
            set
            {
                if (leaseChargeCodes.Length < 10)
                    Array.Resize(ref leaseChargeCodes, 10);
                leaseChargeCodes[9] = value;
                m_SeparateChargeCodes = string.Join(",", leaseChargeCodes);
                OnPropertyChanged("ChargeCode0");
            }
        }
        public string WindocDatabase
        {
            get {return m_WindocDatabase;}
            set {
                m_WindocDatabase = value;
                OnPropertyChanged("WindocDatabase");
            }
        }
        public string WinglDatabase
        {
            get { return m_WinglDatabase; }
            set { m_WinglDatabase = value;
            OnPropertyChanged("WinglDatabase");
            }
        }
        public bool UseWindoc
        {
            set
            {
                m_UseWindoc = value;
                OnPropertyChanged("UseWindoc");
            }
            get { return m_UseWindoc; }
        }

        public bool UseWingl
        {
            set
            {
                m_UseWingl = value;
                OnPropertyChanged("UseWingl");
            }
            get { return m_UseWingl; }
        }

        public string SecurityCode
        {
            set
            {
                m_SecurityCode = value;
                OnPropertyChanged("SecurityCode");
            }
            get { return m_SecurityCode;}
        }

    }
}