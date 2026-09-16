Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Control
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Control"

	Public Const SQL_COL__STATUS__REPORT__GROUPING AS String = "Status Report Grouping"
	Public Const SQL_COL__CLOSING__DATE AS String = "Closing Date"
	Public Const SQL_COL__BILLING_DATE AS String = "Billing date"
	Public Const SQL_COL__LAST__REORG__DATE AS String = "Last Reorg Date"
	Public Const SQL_COL__LAST__ROLL__DATE AS String = "Last Roll Date"
	Public Const SQL_COL__SECURITY__TRACKER AS String = "Security Tracker"
	Public Const SQL_COL__GENERAL__LEDGER AS String = "General Ledger"
	Public Const SQL_COL__RETURN__ADDRESS__FLAG AS String = "Return Address Flag"
	Public Const SQL_COL__RETURN__ADDRESS__A1 AS String = "Return Address A1"
	Public Const SQL_COL__RETURN__ADDRESS__A2 AS String = "Return Address A2"
	Public Const SQL_COL__RETURN__ADDRESS__A3 AS String = "Return Address A3"
	Public Const SQL_COL__RETURN__ADDRESS__BNAME AS String = "Return Address Bname"
	Public Const SQL_COL__RETURN__ADDRESS__B1 AS String = "Return Address B1"
	Public Const SQL_COL__RETURN__ADDRESS__B2 AS String = "Return Address B2"
	Public Const SQL_COL__RETURN__ADDRESS__B3 AS String = "Return Address B3"
	Public Const SQL_COL__PAY__TO__FLAG AS String = "Pay To Flag"
	Public Const SQL_COL__PAY__TO__NAME AS String = "Pay To Name"
	Public Const SQL_COL__STATEMENT__MESSAGE_1 AS String = "Statement Message 1"
	Public Const SQL_COL__STATEMENT__MESSAGE_2 AS String = "Statement Message 2"
	Public Const SQL_COL__STATEMENT__MESSAGE_3 AS String = "Statement Message 3"
	Public Const SQL_COL__GENERAL__MSG__LOCATION AS String = "General Msg Location"
	Public Const SQL_COL__LEGAL__MSG__LOCATION AS String = "Legal Msg Location"
	Public Const SQL_COL__TYPE__MSG__LOCATION AS String = "Type Msg Location"
	Public Const SQL_COL__TELEPHONE__FLAG AS String = "Telephone Flag"
	Public Const SQL_COL__TELEPHONE__NUMBER AS String = "Telephone Number"
	Public Const SQL_COL__SEPARATE__CHARGE__CODES AS String = "Separate Charge Codes"
	Public Const SQL_COL__LOW__RENT__CUT__OFF AS String = "Low Rent Cut-Off"
	Public Const SQL_COL__LOW__RENT__ALLOWANCE AS String = "Low Rent Allowance"
	Public Const SQL_COL__LOW__RENT__MAX_1 AS String = "Low Rent Max 1"
	Public Const SQL_COL__LOW__RENT__MAX_2 AS String = "Low Rent Max 2"
	Public Const SQL_COL__CLEAN_FLAG AS String = "CleanFlag"
	Public Const SQL_COL__CLEAN_DAYS AS String = "CleanDays"
	Public Const SQL_COL__ERROR_LOG_FLAG AS String = "ErrorLogFlag"
	Public Const SQL_COL__ERROR_LOG_DAYS AS String = "ErrorLogDays"
	Public Const SQL_COL__HISTORY_FLAG AS String = "HistoryFlag"
	Public Const SQL_COL__HISTORY_YEARS AS String = "HistoryYears"
	Public Const SQL_COL__MAINT_FLAG AS String = "MaintFlag"
	Public Const SQL_COL__MAINT_YEARS AS String = "MaintYears"
	Public Const SQL_COL__CLEAN_DATE AS String = "CleanDate"
	Public Const SQL_COL__PERCENT__ONE AS String = "Percent One"
	Public Const SQL_COL__PERCENT__TWO AS String = "Percent Two"
	Public Const SQL_COL__TYPE_COLOR AS String = "TypeColor"
	Public Const SQL_COL__LEGAL_COLOR AS String = "LegalColor"
	Public Const SQL_COL__GENERAL_COLOR AS String = "GeneralColor"
	Public Const SQL_COL__MINIMUM_INCREASE1 AS String = "MinimumIncrease1"
	Public Const SQL_COL__MINIMUM_INCREASE2 AS String = "MinimumIncrease2"
	Public Const SQL_COL__MINIMUM_INCREASE_RENT AS String = "MinimumIncreaseRent"
	Public Const SQL_COL__MINIMUM_INCREASE_YEARS AS String = "MinimumIncreaseYears"
	Public Const SQL_COL_DUPLEX_FILE AS String = "duplexFile"
	Public Const SQL_COL_DUPLEX_ENABLED AS String = "duplexEnabled"
	Public Const SQL_COL_ALERT_MESSAGES AS String = "alertMessages"
	Public Const SQL_COL_SCAN AS String = "scan"
	Public Const SQL_COL__SENIOR__CITIZEN__CODE AS String = "Senior Citizen Code"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__STATUS__REPORT__GROUPING & "]," & _
			"[" & SQL_COL__CLOSING__DATE & "]," & _
			"[" & SQL_COL__BILLING_DATE & "]," & _
			"[" & SQL_COL__LAST__REORG__DATE & "]," & _
			"[" & SQL_COL__LAST__ROLL__DATE & "]," & _
			"[" & SQL_COL__SECURITY__TRACKER & "]," & _
			"[" & SQL_COL__GENERAL__LEDGER & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A3 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__BNAME & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B3 & "]," & _
			"[" & SQL_COL__PAY__TO__FLAG & "]," & _
			"[" & SQL_COL__PAY__TO__NAME & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_1 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_2 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_3 & "]," & _
			"[" & SQL_COL__GENERAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__LEGAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TYPE__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TELEPHONE__FLAG & "]," & _
			"[" & SQL_COL__TELEPHONE__NUMBER & "]," & _
			"[" & SQL_COL__SEPARATE__CHARGE__CODES & "]," & _
			"[" & SQL_COL__LOW__RENT__CUT__OFF & "]," & _
			"[" & SQL_COL__LOW__RENT__ALLOWANCE & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_1 & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_2 & "]," & _
			"[" & SQL_COL__CLEAN_FLAG & "]," & _
			"[" & SQL_COL__CLEAN_DAYS & "]," & _
			"[" & SQL_COL__ERROR_LOG_FLAG & "]," & _
			"[" & SQL_COL__ERROR_LOG_DAYS & "]," & _
			"[" & SQL_COL__HISTORY_FLAG & "]," & _
			"[" & SQL_COL__HISTORY_YEARS & "]," & _
			"[" & SQL_COL__MAINT_FLAG & "]," & _
			"[" & SQL_COL__MAINT_YEARS & "]," & _
			"[" & SQL_COL__CLEAN_DATE & "]," & _
			"[" & SQL_COL__PERCENT__ONE & "]," & _
			"[" & SQL_COL__PERCENT__TWO & "]," & _
			"[" & SQL_COL__TYPE_COLOR & "]," & _
			"[" & SQL_COL__LEGAL_COLOR & "]," & _
			"[" & SQL_COL__GENERAL_COLOR & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE1 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE2 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_RENT & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_YEARS & "]," & _
			"[" & SQL_COL_DUPLEX_FILE & "]," & _
			"[" & SQL_COL_DUPLEX_ENABLED & "]," & _
			"[" & SQL_COL_ALERT_MESSAGES & "]," & _
			"[" & SQL_COL_SCAN & "]," & _
			"[" & SQL_COL__SENIOR__CITIZEN__CODE & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__STATUS__REPORT__GROUPING & "]," & _
			"[" & SQL_COL__CLOSING__DATE & "]," & _
			"[" & SQL_COL__BILLING_DATE & "]," & _
			"[" & SQL_COL__LAST__REORG__DATE & "]," & _
			"[" & SQL_COL__LAST__ROLL__DATE & "]," & _
			"[" & SQL_COL__SECURITY__TRACKER & "]," & _
			"[" & SQL_COL__GENERAL__LEDGER & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A3 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__BNAME & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B3 & "]," & _
			"[" & SQL_COL__PAY__TO__FLAG & "]," & _
			"[" & SQL_COL__PAY__TO__NAME & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_1 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_2 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_3 & "]," & _
			"[" & SQL_COL__GENERAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__LEGAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TYPE__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TELEPHONE__FLAG & "]," & _
			"[" & SQL_COL__TELEPHONE__NUMBER & "]," & _
			"[" & SQL_COL__SEPARATE__CHARGE__CODES & "]," & _
			"[" & SQL_COL__LOW__RENT__CUT__OFF & "]," & _
			"[" & SQL_COL__LOW__RENT__ALLOWANCE & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_1 & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_2 & "]," & _
			"[" & SQL_COL__CLEAN_FLAG & "]," & _
			"[" & SQL_COL__CLEAN_DAYS & "]," & _
			"[" & SQL_COL__ERROR_LOG_FLAG & "]," & _
			"[" & SQL_COL__ERROR_LOG_DAYS & "]," & _
			"[" & SQL_COL__HISTORY_FLAG & "]," & _
			"[" & SQL_COL__HISTORY_YEARS & "]," & _
			"[" & SQL_COL__MAINT_FLAG & "]," & _
			"[" & SQL_COL__MAINT_YEARS & "]," & _
			"[" & SQL_COL__CLEAN_DATE & "]," & _
			"[" & SQL_COL__PERCENT__ONE & "]," & _
			"[" & SQL_COL__PERCENT__TWO & "]," & _
			"[" & SQL_COL__TYPE_COLOR & "]," & _
			"[" & SQL_COL__LEGAL_COLOR & "]," & _
			"[" & SQL_COL__GENERAL_COLOR & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE1 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE2 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_RENT & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_YEARS & "]," & _
			"[" & SQL_COL_DUPLEX_FILE & "]," & _
			"[" & SQL_COL_DUPLEX_ENABLED & "]," & _
			"[" & SQL_COL_ALERT_MESSAGES & "]," & _
			"[" & SQL_COL_SCAN & "]," & _
			"[" & SQL_COL__SENIOR__CITIZEN__CODE & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__STATUS__REPORT__GROUPING & "]," & _
			"[" & SQL_COL__CLOSING__DATE & "]," & _
			"[" & SQL_COL__BILLING_DATE & "]," & _
			"[" & SQL_COL__LAST__REORG__DATE & "]," & _
			"[" & SQL_COL__LAST__ROLL__DATE & "]," & _
			"[" & SQL_COL__SECURITY__TRACKER & "]," & _
			"[" & SQL_COL__GENERAL__LEDGER & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__A3 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__BNAME & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B1 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B2 & "]," & _
			"[" & SQL_COL__RETURN__ADDRESS__B3 & "]," & _
			"[" & SQL_COL__PAY__TO__FLAG & "]," & _
			"[" & SQL_COL__PAY__TO__NAME & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_1 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_2 & "]," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_3 & "]," & _
			"[" & SQL_COL__GENERAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__LEGAL__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TYPE__MSG__LOCATION & "]," & _
			"[" & SQL_COL__TELEPHONE__FLAG & "]," & _
			"[" & SQL_COL__TELEPHONE__NUMBER & "]," & _
			"[" & SQL_COL__SEPARATE__CHARGE__CODES & "]," & _
			"[" & SQL_COL__LOW__RENT__CUT__OFF & "]," & _
			"[" & SQL_COL__LOW__RENT__ALLOWANCE & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_1 & "]," & _
			"[" & SQL_COL__LOW__RENT__MAX_2 & "]," & _
			"[" & SQL_COL__CLEAN_FLAG & "]," & _
			"[" & SQL_COL__CLEAN_DAYS & "]," & _
			"[" & SQL_COL__ERROR_LOG_FLAG & "]," & _
			"[" & SQL_COL__ERROR_LOG_DAYS & "]," & _
			"[" & SQL_COL__HISTORY_FLAG & "]," & _
			"[" & SQL_COL__HISTORY_YEARS & "]," & _
			"[" & SQL_COL__MAINT_FLAG & "]," & _
			"[" & SQL_COL__MAINT_YEARS & "]," & _
			"[" & SQL_COL__CLEAN_DATE & "]," & _
			"[" & SQL_COL__PERCENT__ONE & "]," & _
			"[" & SQL_COL__PERCENT__TWO & "]," & _
			"[" & SQL_COL__TYPE_COLOR & "]," & _
			"[" & SQL_COL__LEGAL_COLOR & "]," & _
			"[" & SQL_COL__GENERAL_COLOR & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE1 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE2 & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_RENT & "]," & _
			"[" & SQL_COL__MINIMUM_INCREASE_YEARS & "]," & _
			"[" & SQL_COL_DUPLEX_FILE & "]," & _
			"[" & SQL_COL_DUPLEX_ENABLED & "]," & _
			"[" & SQL_COL_ALERT_MESSAGES & "]," & _
			"[" & SQL_COL_SCAN & "]" & _
			"[" & SQL_COL__SENIOR__CITIZEN__CODE & "]" & _
			") VALUES ('?', #?#, #?#, #?#, #?#, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', ?, '?', '?', '?', '?', ?, ?, ?, ?, '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, #?#, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?'?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__STATUS__REPORT__GROUPING & "]='?'," & _
			"[" & SQL_COL__CLOSING__DATE & "]=#?#," & _
			"[" & SQL_COL__BILLING_DATE & "]=#?#," & _
			"[" & SQL_COL__LAST__REORG__DATE & "]=#?#," & _
			"[" & SQL_COL__LAST__ROLL__DATE & "]=#?#," & _
			"[" & SQL_COL__SECURITY__TRACKER & "]=?," & _
			"[" & SQL_COL__GENERAL__LEDGER & "]=?," & _
			"[" & SQL_COL__RETURN__ADDRESS__FLAG & "]=?," & _
			"[" & SQL_COL__RETURN__ADDRESS__A1 & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__A2 & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__A3 & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__BNAME & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__B1 & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__B2 & "]='?'," & _
			"[" & SQL_COL__RETURN__ADDRESS__B3 & "]='?'," & _
			"[" & SQL_COL__PAY__TO__FLAG & "]=?," & _
			"[" & SQL_COL__PAY__TO__NAME & "]='?'," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_1 & "]='?'," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_2 & "]='?'," & _
			"[" & SQL_COL__STATEMENT__MESSAGE_3 & "]='?'," & _
			"[" & SQL_COL__GENERAL__MSG__LOCATION & "]=?," & _
			"[" & SQL_COL__LEGAL__MSG__LOCATION & "]=?," & _
			"[" & SQL_COL__TYPE__MSG__LOCATION & "]=?," & _
			"[" & SQL_COL__TELEPHONE__FLAG & "]=?," & _
			"[" & SQL_COL__TELEPHONE__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEPARATE__CHARGE__CODES & "]='?'," & _
			"[" & SQL_COL__LOW__RENT__CUT__OFF & "]=?," & _
			"[" & SQL_COL__LOW__RENT__ALLOWANCE & "]=?," & _
			"[" & SQL_COL__LOW__RENT__MAX_1 & "]=?," & _
			"[" & SQL_COL__LOW__RENT__MAX_2 & "]=?," & _
			"[" & SQL_COL__CLEAN_FLAG & "]=?," & _
			"[" & SQL_COL__CLEAN_DAYS & "]=?," & _
			"[" & SQL_COL__ERROR_LOG_FLAG & "]=?," & _
			"[" & SQL_COL__ERROR_LOG_DAYS & "]=?," & _
			"[" & SQL_COL__HISTORY_FLAG & "]=?," & _
			"[" & SQL_COL__HISTORY_YEARS & "]=?," & _
			"[" & SQL_COL__MAINT_FLAG & "]=?," & _
			"[" & SQL_COL__MAINT_YEARS & "]=?," & _
			"[" & SQL_COL__CLEAN_DATE & "]=#?#," & _
			"[" & SQL_COL__PERCENT__ONE & "]=?," & _
			"[" & SQL_COL__PERCENT__TWO & "]=?," & _
			"[" & SQL_COL__TYPE_COLOR & "]=?," & _
			"[" & SQL_COL__LEGAL_COLOR & "]=?," & _
			"[" & SQL_COL__GENERAL_COLOR & "]=?," & _
			"[" & SQL_COL__MINIMUM_INCREASE1 & "]=?," & _
			"[" & SQL_COL__MINIMUM_INCREASE2 & "]=?," & _
			"[" & SQL_COL__MINIMUM_INCREASE_RENT & "]=?," & _
			"[" & SQL_COL__MINIMUM_INCREASE_YEARS & "]=?," & _
			"[" & SQL_COL_DUPLEX_FILE & "]='?'," & _
			"[" & SQL_COL_DUPLEX_ENABLED & "]=?," & _
			"[" & SQL_COL_ALERT_MESSAGES & "]=?," & _
			"[" & SQL_COL_SCAN & "]=?" & _
			"[" & SQL_COL__SENIOR__CITIZEN__CODE & "]='?'" & _
			" WHERE " & 

	Protected m_StatusReportGrouping As String
	Protected m_ClosingDate As DateTime
	Protected m_Billingdate As DateTime
	Protected m_LastReorgDate As DateTime
	Protected m_LastRollDate As DateTime
	Protected m_SecurityTracker As Boolean
	Protected m_GeneralLedger As Boolean
	Protected m_ReturnAddressFlag As Int16
	Protected m_ReturnAddressA1 As String
	Protected m_ReturnAddressA2 As String
	Protected m_ReturnAddressA3 As String
	Protected m_ReturnAddressBname As String
	Protected m_ReturnAddressB1 As String
	Protected m_ReturnAddressB2 As String
	Protected m_ReturnAddressB3 As String
	Protected m_PayToFlag As Int16
	Protected m_PayToName As String
	Protected m_StatementMessage1 As String
	Protected m_StatementMessage2 As String
	Protected m_StatementMessage3 As String
	Protected m_GeneralMsgLocation As Int16
	Protected m_LegalMsgLocation As Int16
	Protected m_TypeMsgLocation As Int16
	Protected m_TelephoneFlag As Int16
	Protected m_TelephoneNumber As String
	Protected m_SeparateChargeCodes As String
	Protected m_LowRentCutOff As Decimal
	Protected m_LowRentAllowance As Decimal
	Protected m_LowRentMax1 As Decimal
	Protected m_LowRentMax2 As Decimal
	Protected m_CleanFlag As Boolean
	Protected m_CleanDays As Int16
	Protected m_ErrorLogFlag As Boolean
	Protected m_ErrorLogDays As Int16
	Protected m_HistoryFlag As Boolean
	Protected m_HistoryYears As Int16
	Protected m_MaintFlag As Boolean
	Protected m_MaintYears As Int16
	Protected m_CleanDate As DateTime
	Protected m_PercentOne As Single
	Protected m_PercentTwo As Single
	Protected m_TypeColor As Int32
	Protected m_LegalColor As Int32
	Protected m_GeneralColor As Int32
	Protected m_MinimumIncrease1 As Decimal
	Protected m_MinimumIncrease2 As Decimal
	Protected m_MinimumIncreaseRent As Decimal
	Protected m_MinimumIncreaseYears As Int16
	Protected m_duplexFile As String
	Protected m_duplexEnabled As Boolean
	Protected m_alertMessages As Boolean
	Protected m_scan As Boolean
	Protected m_SeniorCitizenCode As String

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal StatusReportGrouping As String,ByVal ClosingDate As DateTime,ByVal Billingdate As DateTime,ByVal LastReorgDate As DateTime,ByVal LastRollDate As DateTime,ByVal SecurityTracker As Boolean,ByVal GeneralLedger As Boolean,ByVal ReturnAddressFlag As Int16,ByVal ReturnAddressA1 As String,ByVal ReturnAddressA2 As String,ByVal ReturnAddressA3 As String,ByVal ReturnAddressBname As String,ByVal ReturnAddressB1 As String,ByVal ReturnAddressB2 As String,ByVal ReturnAddressB3 As String,ByVal PayToFlag As Int16,ByVal PayToName As String,ByVal StatementMessage1 As String,ByVal StatementMessage2 As String,ByVal StatementMessage3 As String,ByVal GeneralMsgLocation As Int16,ByVal LegalMsgLocation As Int16,ByVal TypeMsgLocation As Int16,ByVal TelephoneFlag As Int16,ByVal TelephoneNumber As String,ByVal SeparateChargeCodes As String,ByVal LowRentCutOff As Decimal,ByVal LowRentAllowance As Decimal,ByVal LowRentMax1 As Decimal,ByVal LowRentMax2 As Decimal,ByVal CleanFlag As Boolean,ByVal CleanDays As Int16,ByVal ErrorLogFlag As Boolean,ByVal ErrorLogDays As Int16,ByVal HistoryFlag As Boolean,ByVal HistoryYears As Int16,ByVal MaintFlag As Boolean,ByVal MaintYears As Int16,ByVal CleanDate As DateTime,ByVal PercentOne As Single,ByVal PercentTwo As Single,ByVal TypeColor As Int32,ByVal LegalColor As Int32,ByVal GeneralColor As Int32,ByVal MinimumIncrease1 As Decimal,ByVal MinimumIncrease2 As Decimal,ByVal MinimumIncreaseRent As Decimal,ByVal MinimumIncreaseYears As Int16,ByVal duplexFile As String,ByVal duplexEnabled As Boolean,ByVal alertMessages As Boolean,ByVal scan As Boolean,ByVal SeniorCitizenCode As String)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_StatusReportGrouping = StatusReportGrouping
		m_ClosingDate = ClosingDate
		m_Billingdate = Billingdate
		m_LastReorgDate = LastReorgDate
		m_LastRollDate = LastRollDate
		m_SecurityTracker = SecurityTracker
		m_GeneralLedger = GeneralLedger
		m_ReturnAddressFlag = ReturnAddressFlag
		m_ReturnAddressA1 = ReturnAddressA1
		m_ReturnAddressA2 = ReturnAddressA2
		m_ReturnAddressA3 = ReturnAddressA3
		m_ReturnAddressBname = ReturnAddressBname
		m_ReturnAddressB1 = ReturnAddressB1
		m_ReturnAddressB2 = ReturnAddressB2
		m_ReturnAddressB3 = ReturnAddressB3
		m_PayToFlag = PayToFlag
		m_PayToName = PayToName
		m_StatementMessage1 = StatementMessage1
		m_StatementMessage2 = StatementMessage2
		m_StatementMessage3 = StatementMessage3
		m_GeneralMsgLocation = GeneralMsgLocation
		m_LegalMsgLocation = LegalMsgLocation
		m_TypeMsgLocation = TypeMsgLocation
		m_TelephoneFlag = TelephoneFlag
		m_TelephoneNumber = TelephoneNumber
		m_SeparateChargeCodes = SeparateChargeCodes
		m_LowRentCutOff = LowRentCutOff
		m_LowRentAllowance = LowRentAllowance
		m_LowRentMax1 = LowRentMax1
		m_LowRentMax2 = LowRentMax2
		m_CleanFlag = CleanFlag
		m_CleanDays = CleanDays
		m_ErrorLogFlag = ErrorLogFlag
		m_ErrorLogDays = ErrorLogDays
		m_HistoryFlag = HistoryFlag
		m_HistoryYears = HistoryYears
		m_MaintFlag = MaintFlag
		m_MaintYears = MaintYears
		m_CleanDate = CleanDate
		m_PercentOne = PercentOne
		m_PercentTwo = PercentTwo
		m_TypeColor = TypeColor
		m_LegalColor = LegalColor
		m_GeneralColor = GeneralColor
		m_MinimumIncrease1 = MinimumIncrease1
		m_MinimumIncrease2 = MinimumIncrease2
		m_MinimumIncreaseRent = MinimumIncreaseRent
		m_MinimumIncreaseYears = MinimumIncreaseYears
		m_duplexFile = duplexFile
		m_duplexEnabled = duplexEnabled
		m_alertMessages = alertMessages
		m_scan = scan
		m_SeniorCitizenCode = SeniorCitizenCode
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Control

		Try
			m_StatusReportGrouping = dr(SQL_COL__STATUS__REPORT__GROUPING)
			m_ClosingDate = dr(SQL_COL__CLOSING__DATE)
			m_Billingdate = dr(SQL_COL__BILLING_DATE)
			m_LastReorgDate = dr(SQL_COL__LAST__REORG__DATE)
			m_LastRollDate = dr(SQL_COL__LAST__ROLL__DATE)
			m_SecurityTracker = dr(SQL_COL__SECURITY__TRACKER)
			m_GeneralLedger = dr(SQL_COL__GENERAL__LEDGER)
			m_ReturnAddressFlag = dr(SQL_COL__RETURN__ADDRESS__FLAG)
			m_ReturnAddressA1 = dr(SQL_COL__RETURN__ADDRESS__A1)
			m_ReturnAddressA2 = dr(SQL_COL__RETURN__ADDRESS__A2)
			m_ReturnAddressA3 = dr(SQL_COL__RETURN__ADDRESS__A3)
			m_ReturnAddressBname = dr(SQL_COL__RETURN__ADDRESS__BNAME)
			m_ReturnAddressB1 = dr(SQL_COL__RETURN__ADDRESS__B1)
			m_ReturnAddressB2 = dr(SQL_COL__RETURN__ADDRESS__B2)
			m_ReturnAddressB3 = dr(SQL_COL__RETURN__ADDRESS__B3)
			m_PayToFlag = dr(SQL_COL__PAY__TO__FLAG)
			m_PayToName = dr(SQL_COL__PAY__TO__NAME)
			m_StatementMessage1 = dr(SQL_COL__STATEMENT__MESSAGE_1)
			m_StatementMessage2 = dr(SQL_COL__STATEMENT__MESSAGE_2)
			m_StatementMessage3 = dr(SQL_COL__STATEMENT__MESSAGE_3)
			m_GeneralMsgLocation = dr(SQL_COL__GENERAL__MSG__LOCATION)
			m_LegalMsgLocation = dr(SQL_COL__LEGAL__MSG__LOCATION)
			m_TypeMsgLocation = dr(SQL_COL__TYPE__MSG__LOCATION)
			m_TelephoneFlag = dr(SQL_COL__TELEPHONE__FLAG)
			m_TelephoneNumber = dr(SQL_COL__TELEPHONE__NUMBER)
			m_SeparateChargeCodes = dr(SQL_COL__SEPARATE__CHARGE__CODES)
			m_LowRentCutOff = dr(SQL_COL__LOW__RENT__CUT__OFF)
			m_LowRentAllowance = dr(SQL_COL__LOW__RENT__ALLOWANCE)
			m_LowRentMax1 = dr(SQL_COL__LOW__RENT__MAX_1)
			m_LowRentMax2 = dr(SQL_COL__LOW__RENT__MAX_2)
			m_CleanFlag = dr(SQL_COL__CLEAN_FLAG)
			m_CleanDays = dr(SQL_COL__CLEAN_DAYS)
			m_ErrorLogFlag = dr(SQL_COL__ERROR_LOG_FLAG)
			m_ErrorLogDays = dr(SQL_COL__ERROR_LOG_DAYS)
			m_HistoryFlag = dr(SQL_COL__HISTORY_FLAG)
			m_HistoryYears = dr(SQL_COL__HISTORY_YEARS)
			m_MaintFlag = dr(SQL_COL__MAINT_FLAG)
			m_MaintYears = dr(SQL_COL__MAINT_YEARS)
			m_CleanDate = dr(SQL_COL__CLEAN_DATE)
			m_PercentOne = dr(SQL_COL__PERCENT__ONE)
			m_PercentTwo = dr(SQL_COL__PERCENT__TWO)
			m_TypeColor = dr(SQL_COL__TYPE_COLOR)
			m_LegalColor = dr(SQL_COL__LEGAL_COLOR)
			m_GeneralColor = dr(SQL_COL__GENERAL_COLOR)
			m_MinimumIncrease1 = dr(SQL_COL__MINIMUM_INCREASE1)
			m_MinimumIncrease2 = dr(SQL_COL__MINIMUM_INCREASE2)
			m_MinimumIncreaseRent = dr(SQL_COL__MINIMUM_INCREASE_RENT)
			m_MinimumIncreaseYears = dr(SQL_COL__MINIMUM_INCREASE_YEARS)
			m_duplexFile = dr(SQL_COL_DUPLEX_FILE)
			m_duplexEnabled = dr(SQL_COL_DUPLEX_ENABLED)
			m_alertMessages = dr(SQL_COL_ALERT_MESSAGES)
			m_scan = dr(SQL_COL_SCAN)
			m_SeniorCitizenCode = dr(SQL_COL__SENIOR__CITIZEN__CODE)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Control

		Dim obj As Control = New Control
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Control()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Control = New Control

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Control
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Control)), Control())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_StatusReportGrouping, m_ClosingDate, m_Billingdate, m_LastReorgDate, m_LastRollDate, m_SecurityTracker, m_GeneralLedger, m_ReturnAddressFlag, m_ReturnAddressA1, m_ReturnAddressA2, m_ReturnAddressA3, m_ReturnAddressBname, m_ReturnAddressB1, m_ReturnAddressB2, m_ReturnAddressB3, m_PayToFlag, m_PayToName, m_StatementMessage1, m_StatementMessage2, m_StatementMessage3, m_GeneralMsgLocation, m_LegalMsgLocation, m_TypeMsgLocation, m_TelephoneFlag, m_TelephoneNumber, m_SeparateChargeCodes, m_LowRentCutOff, m_LowRentAllowance, m_LowRentMax1, m_LowRentMax2, m_CleanFlag, m_CleanDays, m_ErrorLogFlag, m_ErrorLogDays, m_HistoryFlag, m_HistoryYears, m_MaintFlag, m_MaintYears, m_CleanDate, m_PercentOne, m_PercentTwo, m_TypeColor, m_LegalColor, m_GeneralColor, m_MinimumIncrease1, m_MinimumIncrease2, m_MinimumIncreaseRent, m_MinimumIncreaseYears, m_duplexFile, m_duplexEnabled, m_alertMessages, m_scan, m_SeniorCitizenCode}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_StatusReportGrouping, m_ClosingDate, m_Billingdate, m_LastReorgDate, m_LastRollDate, m_SecurityTracker, m_GeneralLedger, m_ReturnAddressFlag, m_ReturnAddressA1, m_ReturnAddressA2, m_ReturnAddressA3, m_ReturnAddressBname, m_ReturnAddressB1, m_ReturnAddressB2, m_ReturnAddressB3, m_PayToFlag, m_PayToName, m_StatementMessage1, m_StatementMessage2, m_StatementMessage3, m_GeneralMsgLocation, m_LegalMsgLocation, m_TypeMsgLocation, m_TelephoneFlag, m_TelephoneNumber, m_SeparateChargeCodes, m_LowRentCutOff, m_LowRentAllowance, m_LowRentMax1, m_LowRentMax2, m_CleanFlag, m_CleanDays, m_ErrorLogFlag, m_ErrorLogDays, m_HistoryFlag, m_HistoryYears, m_MaintFlag, m_MaintYears, m_CleanDate, m_PercentOne, m_PercentTwo, m_TypeColor, m_LegalColor, m_GeneralColor, m_MinimumIncrease1, m_MinimumIncrease2, m_MinimumIncreaseRent, m_MinimumIncreaseYears, m_duplexFile, m_duplexEnabled, m_alertMessages, m_scan, m_SeniorCitizenCode}
			 = Me.insertRecord(values)
			If  >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property StatusReportGrouping As String
		Get
			Return m_StatusReportGrouping
		End Get
		Set(ByVal value As String)
			m_StatusReportGrouping = value
		End Set
	End Property

	Public  Property ClosingDate As DateTime
		Get
			Return m_ClosingDate
		End Get
		Set(ByVal value As DateTime)
			m_ClosingDate = value
		End Set
	End Property

	Public  Property Billingdate As DateTime
		Get
			Return m_Billingdate
		End Get
		Set(ByVal value As DateTime)
			m_Billingdate = value
		End Set
	End Property

	Public  Property LastReorgDate As DateTime
		Get
			Return m_LastReorgDate
		End Get
		Set(ByVal value As DateTime)
			m_LastReorgDate = value
		End Set
	End Property

	Public  Property LastRollDate As DateTime
		Get
			Return m_LastRollDate
		End Get
		Set(ByVal value As DateTime)
			m_LastRollDate = value
		End Set
	End Property

	Public  Property SecurityTracker As Boolean
		Get
			Return m_SecurityTracker
		End Get
		Set(ByVal value As Boolean)
			m_SecurityTracker = value
		End Set
	End Property

	Public  Property GeneralLedger As Boolean
		Get
			Return m_GeneralLedger
		End Get
		Set(ByVal value As Boolean)
			m_GeneralLedger = value
		End Set
	End Property

	Public  Property ReturnAddressFlag As Int16
		Get
			Return m_ReturnAddressFlag
		End Get
		Set(ByVal value As Int16)
			m_ReturnAddressFlag = value
		End Set
	End Property

	Public  Property ReturnAddressA1 As String
		Get
			Return m_ReturnAddressA1
		End Get
		Set(ByVal value As String)
			m_ReturnAddressA1 = value
		End Set
	End Property

	Public  Property ReturnAddressA2 As String
		Get
			Return m_ReturnAddressA2
		End Get
		Set(ByVal value As String)
			m_ReturnAddressA2 = value
		End Set
	End Property

	Public  Property ReturnAddressA3 As String
		Get
			Return m_ReturnAddressA3
		End Get
		Set(ByVal value As String)
			m_ReturnAddressA3 = value
		End Set
	End Property

	Public  Property ReturnAddressBname As String
		Get
			Return m_ReturnAddressBname
		End Get
		Set(ByVal value As String)
			m_ReturnAddressBname = value
		End Set
	End Property

	Public  Property ReturnAddressB1 As String
		Get
			Return m_ReturnAddressB1
		End Get
		Set(ByVal value As String)
			m_ReturnAddressB1 = value
		End Set
	End Property

	Public  Property ReturnAddressB2 As String
		Get
			Return m_ReturnAddressB2
		End Get
		Set(ByVal value As String)
			m_ReturnAddressB2 = value
		End Set
	End Property

	Public  Property ReturnAddressB3 As String
		Get
			Return m_ReturnAddressB3
		End Get
		Set(ByVal value As String)
			m_ReturnAddressB3 = value
		End Set
	End Property

	Public  Property PayToFlag As Int16
		Get
			Return m_PayToFlag
		End Get
		Set(ByVal value As Int16)
			m_PayToFlag = value
		End Set
	End Property

	Public  Property PayToName As String
		Get
			Return m_PayToName
		End Get
		Set(ByVal value As String)
			m_PayToName = value
		End Set
	End Property

	Public  Property StatementMessage1 As String
		Get
			Return m_StatementMessage1
		End Get
		Set(ByVal value As String)
			m_StatementMessage1 = value
		End Set
	End Property

	Public  Property StatementMessage2 As String
		Get
			Return m_StatementMessage2
		End Get
		Set(ByVal value As String)
			m_StatementMessage2 = value
		End Set
	End Property

	Public  Property StatementMessage3 As String
		Get
			Return m_StatementMessage3
		End Get
		Set(ByVal value As String)
			m_StatementMessage3 = value
		End Set
	End Property

	Public  Property GeneralMsgLocation As Int16
		Get
			Return m_GeneralMsgLocation
		End Get
		Set(ByVal value As Int16)
			m_GeneralMsgLocation = value
		End Set
	End Property

	Public  Property LegalMsgLocation As Int16
		Get
			Return m_LegalMsgLocation
		End Get
		Set(ByVal value As Int16)
			m_LegalMsgLocation = value
		End Set
	End Property

	Public  Property TypeMsgLocation As Int16
		Get
			Return m_TypeMsgLocation
		End Get
		Set(ByVal value As Int16)
			m_TypeMsgLocation = value
		End Set
	End Property

	Public  Property TelephoneFlag As Int16
		Get
			Return m_TelephoneFlag
		End Get
		Set(ByVal value As Int16)
			m_TelephoneFlag = value
		End Set
	End Property

	Public  Property TelephoneNumber As String
		Get
			Return m_TelephoneNumber
		End Get
		Set(ByVal value As String)
			m_TelephoneNumber = value
		End Set
	End Property

	Public  Property SeparateChargeCodes As String
		Get
			Return m_SeparateChargeCodes
		End Get
		Set(ByVal value As String)
			m_SeparateChargeCodes = value
		End Set
	End Property

	Public  Property LowRentCutOff As Decimal
		Get
			Return m_LowRentCutOff
		End Get
		Set(ByVal value As Decimal)
			m_LowRentCutOff = value
		End Set
	End Property

	Public  Property LowRentAllowance As Decimal
		Get
			Return m_LowRentAllowance
		End Get
		Set(ByVal value As Decimal)
			m_LowRentAllowance = value
		End Set
	End Property

	Public  Property LowRentMax1 As Decimal
		Get
			Return m_LowRentMax1
		End Get
		Set(ByVal value As Decimal)
			m_LowRentMax1 = value
		End Set
	End Property

	Public  Property LowRentMax2 As Decimal
		Get
			Return m_LowRentMax2
		End Get
		Set(ByVal value As Decimal)
			m_LowRentMax2 = value
		End Set
	End Property

	Public  Property CleanFlag As Boolean
		Get
			Return m_CleanFlag
		End Get
		Set(ByVal value As Boolean)
			m_CleanFlag = value
		End Set
	End Property

	Public  Property CleanDays As Int16
		Get
			Return m_CleanDays
		End Get
		Set(ByVal value As Int16)
			m_CleanDays = value
		End Set
	End Property

	Public  Property ErrorLogFlag As Boolean
		Get
			Return m_ErrorLogFlag
		End Get
		Set(ByVal value As Boolean)
			m_ErrorLogFlag = value
		End Set
	End Property

	Public  Property ErrorLogDays As Int16
		Get
			Return m_ErrorLogDays
		End Get
		Set(ByVal value As Int16)
			m_ErrorLogDays = value
		End Set
	End Property

	Public  Property HistoryFlag As Boolean
		Get
			Return m_HistoryFlag
		End Get
		Set(ByVal value As Boolean)
			m_HistoryFlag = value
		End Set
	End Property

	Public  Property HistoryYears As Int16
		Get
			Return m_HistoryYears
		End Get
		Set(ByVal value As Int16)
			m_HistoryYears = value
		End Set
	End Property

	Public  Property MaintFlag As Boolean
		Get
			Return m_MaintFlag
		End Get
		Set(ByVal value As Boolean)
			m_MaintFlag = value
		End Set
	End Property

	Public  Property MaintYears As Int16
		Get
			Return m_MaintYears
		End Get
		Set(ByVal value As Int16)
			m_MaintYears = value
		End Set
	End Property

	Public  Property CleanDate As DateTime
		Get
			Return m_CleanDate
		End Get
		Set(ByVal value As DateTime)
			m_CleanDate = value
		End Set
	End Property

	Public  Property PercentOne As Single
		Get
			Return m_PercentOne
		End Get
		Set(ByVal value As Single)
			m_PercentOne = value
		End Set
	End Property

	Public  Property PercentTwo As Single
		Get
			Return m_PercentTwo
		End Get
		Set(ByVal value As Single)
			m_PercentTwo = value
		End Set
	End Property

	Public  Property TypeColor As Int32
		Get
			Return m_TypeColor
		End Get
		Set(ByVal value As Int32)
			m_TypeColor = value
		End Set
	End Property

	Public  Property LegalColor As Int32
		Get
			Return m_LegalColor
		End Get
		Set(ByVal value As Int32)
			m_LegalColor = value
		End Set
	End Property

	Public  Property GeneralColor As Int32
		Get
			Return m_GeneralColor
		End Get
		Set(ByVal value As Int32)
			m_GeneralColor = value
		End Set
	End Property

	Public  Property MinimumIncrease1 As Decimal
		Get
			Return m_MinimumIncrease1
		End Get
		Set(ByVal value As Decimal)
			m_MinimumIncrease1 = value
		End Set
	End Property

	Public  Property MinimumIncrease2 As Decimal
		Get
			Return m_MinimumIncrease2
		End Get
		Set(ByVal value As Decimal)
			m_MinimumIncrease2 = value
		End Set
	End Property

	Public  Property MinimumIncreaseRent As Decimal
		Get
			Return m_MinimumIncreaseRent
		End Get
		Set(ByVal value As Decimal)
			m_MinimumIncreaseRent = value
		End Set
	End Property

	Public  Property MinimumIncreaseYears As Int16
		Get
			Return m_MinimumIncreaseYears
		End Get
		Set(ByVal value As Int16)
			m_MinimumIncreaseYears = value
		End Set
	End Property

	Public  Property duplexFile As String
		Get
			Return m_duplexFile
		End Get
		Set(ByVal value As String)
			m_duplexFile = value
		End Set
	End Property

	Public  Property duplexEnabled As Boolean
		Get
			Return m_duplexEnabled
		End Get
		Set(ByVal value As Boolean)
			m_duplexEnabled = value
		End Set
	End Property

	Public  Property alertMessages As Boolean
		Get
			Return m_alertMessages
		End Get
		Set(ByVal value As Boolean)
			m_alertMessages = value
		End Set
	End Property

	Public  Property scan As Boolean
		Get
			Return m_scan
		End Get
		Set(ByVal value As Boolean)
			m_scan = value
		End Set
	End Property

	Public  Property SeniorCitizenCode As String
		Get
			Return m_SeniorCitizenCode
		End Get
		Set(ByVal value As String)
			m_SeniorCitizenCode = value
		End Set
	End Property

End Class
