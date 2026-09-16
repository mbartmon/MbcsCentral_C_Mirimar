Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class TenantsArchive
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "TenantsArchive"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"
	Public Const SQL_COL__OLD__HISTORY__FILE__POINTER AS String = "Old History File Pointer"
	Public Const SQL_COL__NEW__TENANT__FLAG AS String = "New Tenant Flag"
	Public Const SQL_COL__REPLACING__TENANT AS String = "Replacing Tenant"
	Public Const SQL_COL__TENANT__NAME AS String = "Tenant Name"
	Public Const SQL_COL__APARTMENT AS String = "Apartment"
	Public Const SQL_COL__DUE__DATE AS String = "Due Date"
	Public Const SQL_COL__TYPE AS String = "Type"
	Public Const SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG AS String = "Alternate Billing Address Flag"
	Public Const SQL_COL__ADDITIONAL__NAME__FLAG AS String = "Additional Name Flag"
	Public Const SQL_COL__LEASE__LETTER__FLAG AS String = "Lease Letter Flag"
	Public Const SQL_COL__LOW__RENT__FLAG AS String = "Low Rent Flag"
	Public Const SQL_COL__LEGAL__CODE AS String = "Legal Code"
	Public Const SQL_COL__LEGAL__CODE_2 AS String = "Legal Code 2"
	Public Const SQL_COL__RENT AS String = "Rent"
	Public Const SQL_COL__SECURITY AS String = "Security"
	Public Const SQL_COL__ARREARS AS String = "Arrears"
	Public Const SQL_COL__PAID AS String = "Paid"
	Public Const SQL_COL__FUTURE__RENT_1 AS String = "Future Rent 1"
	Public Const SQL_COL__FUTURE__RENT_2 AS String = "Future Rent 2"
	Public Const SQL_COL__FUTURE__RENT_3 AS String = "Future Rent 3"
	Public Const SQL_COL__M_B_R AS String = "MBR"
	Public Const SQL_COL__FUTURE__DATE_1 AS String = "Future Date 1"
	Public Const SQL_COL__FUTURE__DATE_2 AS String = "Future Date 2"
	Public Const SQL_COL__FUTURE__DATE_3 AS String = "Future Date 3"
	Public Const SQL_COL__ROOMS AS String = "Rooms"
	Public Const SQL_COL__SQUARE__FEET AS String = "Square Feet"
	Public Const SQL_COL__MOVE_IN__DATE AS String = "Move-in Date"
	Public Const SQL_COL__LEASE__START__DATE AS String = "Lease Start Date"
	Public Const SQL_COL__LEASE__EXPIRATION__DATE AS String = "Lease Expiration Date"
	Public Const SQL_COL__LAST__PAID__DATE AS String = "Last Paid Date"
	Public Const SQL_COL__ADDITIONAL__NAME AS String = "Additional Name"
	Public Const SQL_COL__S_S_NUMBER1 AS String = "SSNumber1"
	Public Const SQL_COL__S_S_NUMBER2 AS String = "SSNumber2"
	Public Const SQL_COL__HOME_PHONE1 AS String = "HomePhone1"
	Public Const SQL_COL__HOME_PHONE2 AS String = "HomePhone2"
	Public Const SQL_COL__BUS_PHONE1 AS String = "BusPhone1"
	Public Const SQL_COL__BUS_PHONE2 AS String = "BusPhone2"
	Public Const SQL_COL__ALT_BILL_NAME AS String = "AltBillName"
	Public Const SQL_COL__ALT_BILL_ADD1 AS String = "AltBillAdd1"
	Public Const SQL_COL__ALT_BILL_ADD2 AS String = "AltBillAdd2"
	Public Const SQL_COL__ALT_BILL_ADD3 AS String = "AltBillAdd3"
	Public Const SQL_COL__ADD_NAME AS String = "AddName"
	Public Const SQL_COL__BANK AS String = "Bank"
	Public Const SQL_COL__INTEREST_INFO AS String = "InterestInfo"
	Public Const SQL_COL__SHARES AS String = "Shares"
	Public Const SQL_COL__COMMENT AS String = "Comment"
	Public Const SQL_COL__SENIORCIT AS String = "Seniorcit"
	Public Const SQL_COL__TOTAL__CHARGES AS String = "Total Charges"
	Public Const SQL_COL__PAGER AS String = "Pager"
	Public Const SQL_COL_DHCR_DATE_ONE AS String = "dhcrDateOne"
	Public Const SQL_COL_DHCR_RENT_ONE AS String = "dhcrRentOne"
	Public Const SQL_COL_DHCR_STOVE_ONE AS String = "dhcrStoveOne"
	Public Const SQL_COL_DHCR_REFRIGERATOR_ONE AS String = "dhcrRefrigeratorOne"
	Public Const SQL_COL_DHCR_DISHWASHER_ONE AS String = "dhcrDishwasherOne"
	Public Const SQL_COL_DHCR_A_C_ONE AS String = "dhcrACOne"
	Public Const SQL_COL_DHCR_WINDOWS_ONE AS String = "dhcrWindowsOne"
	Public Const SQL_COL_DHCR_OTHER_ONE AS String = "dhcrOtherOne"
	Public Const SQL_COL_DHCR_OTHER_TEXT_ONE AS String = "dhcrOtherTextOne"
	Public Const SQL_COL_DHCR_DATE_TWO AS String = "dhcrDateTwo"
	Public Const SQL_COL_DHCR_RENT_TWO AS String = "dhcrRentTwo"
	Public Const SQL_COL_DHCR_STOVE_TWO AS String = "dhcrStoveTwo"
	Public Const SQL_COL_DHCR_REFRIGERATOR_TWO AS String = "dhcrRefrigeratorTwo"
	Public Const SQL_COL_DHCR_DISHWASHER_TWO AS String = "dhcrDishwasherTwo"
	Public Const SQL_COL_DHCR_A_C_TWO AS String = "dhcrACTwo"
	Public Const SQL_COL_DHCR_WINDOWS_TWO AS String = "dhcrWindowsTwo"
	Public Const SQL_COL_DHCR_OTHER_TWO AS String = "dhcrOtherTwo"
	Public Const SQL_COL_DHCR_OTHER_TEXT_TWO AS String = "dhcrOtherTextTwo"
	Public Const SQL_COL__LEGAL_FEE AS String = "LegalFee"
	Public Const SQL_COL__LATE_FEE AS String = "LateFee"
	Public Const SQL_COL__LATE_BASE AS String = "LateBase"
	Public Const SQL_COL__LEGAL_FEE_NO AS String = "LegalFeeNo"
	Public Const SQL_COL__LATE_FEE_YES AS String = "LateFeeYes"
	Public Const SQL_COL__LATE_FEE_NO AS String = "LateFeeNo"
	Public Const SQL_COL__RENT_LEGAL AS String = "RentLegal"
	Public Const SQL_COL__FUTURE__LEGAL_1 AS String = "Future Legal 1"
	Public Const SQL_COL__FUTURE__LEGAL_2 AS String = "Future Legal 2"
	Public Const SQL_COL__FUTURE__LEGAL_3 AS String = "Future Legal 3"
	Public Const SQL_COL__PROPOSED_RENT AS String = "ProposedRent"
	Public Const SQL_COL__PROMPT_PAY1 AS String = "PromptPay1"
	Public Const SQL_COL__PROPOSED_RENT2 AS String = "ProposedRent2"
	Public Const SQL_COL__PROMPT_PAY2 AS String = "PromptPay2"
	Public Const SQL_COL__PROPOSED_LEGAL2 AS String = "ProposedLegal2"
	Public Const SQL_COL__PROPOSED_LEGAL AS String = "ProposedLegal"
	Public Const SQL_COL__CELL2 AS String = "Cell2"
	Public Const SQL_COL__SEC_MULTIPLE AS String = "SecMultiple"
	Public Const SQL_COL__EMAIL1 AS String = "Email1"
	Public Const SQL_COL__EMAIL2 AS String = "Email2"
	Public Const SQL_COL__TEMP1 AS String = "Temp1"
	Public Const SQL_COL__TEMP2 AS String = "Temp2"
	Public Const SQL_COL__SECURITY_ACCOUNT AS String = "SecurityAccount"
	Public Const SQL_COL__OPT_TENANT1 AS String = "OptTenant1"
	Public Const SQL_COL__OPT_TENANT2 AS String = "OptTenant2"
	Public Const SQL_COL__SECURITY_BUILDING AS String = "SecurityBuilding"
	Public Const SQL_COL__SUCCEEDED AS String = "Succeeded"
	Public Const SQL_COL__SECOND_SUCCESSION AS String = "SecondSuccession"
	Public Const SQL_COL_421A AS String = "421a"
	Public Const SQL_COL__RENT_OVERCHARGE AS String = "RentOvercharge"
	Public Const SQL_COL__FAIR_MARKET_APPEAL AS String = "FairMarketAppeal"
	Public Const SQL_COL__TRANSIENT_OCCUPANT AS String = "TransientOccupant"
	Public Const SQL_COL__OWNER_OCCUPIED AS String = "OwnerOccupied"
	Public Const SQL_COL__COMMERCIAL_PROFESSIONAL AS String = "CommercialProfessional"
	Public Const SQL_COL__NOT_PRIME_RESIDENCE AS String = "NotPrimeResidence"
	Public Const SQL_COL__TEMP_EXEMPT_OTHER AS String = "TempExemptOther"
	Public Const SQL_COL__EXEMPT_DATE AS String = "ExemptDate"
	Public Const SQL_COL__EXEMPT_HIGH_RENT_VACANCY AS String = "ExemptHighRentVacancy"
	Public Const SQL_COL__EXEMPT_LAST_LEGAL_PAID AS String = "ExemptLastLegalPaid"
	Public Const SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD AS String = "ExemptHighRentVacanyPeriod"
	Public Const SQL_COL__EXEMPT_HIGH_RENT_INCOME AS String = "ExemptHighRentIncome"
	Public Const SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO AS String = "ExemptCommercialProfessionalCo"
	Public Const SQL_COL__EXEMPT_SUBSTANTIAL_REHAB AS String = "ExemptSubstantialRehab"
	Public Const SQL_COL__EXEMPT_COOP_CONDO_OWNER AS String = "ExemptCoopCondoOwner"
	Public Const SQL_COL__EXEMPT_SEC11243 AS String = "ExemptSec11243"
	Public Const SQL_COL__EXEMPT_OTHER AS String = "ExemptOther"
	Public Const SQL_COL__EXEMPT_OTHER_DESC AS String = "ExemptOtherDesc"
	Public Const SQL_COL__EXEMPT_EXP_SEC608 AS String = "ExemptExpSec608"
	Public Const SQL_COL__EXEMPT_EXP421A AS String = "ExemptExp421a"
	Public Const SQL_COL__EXEMPT421A_MARKET_RATE AS String = "Exempt421aMarketRate"
	Public Const SQL_COL__EXEMPT421A_INCOME AS String = "Exempt421aIncome"
	Public Const SQL_COL__EXEMPT421A_MEDIAN_INCOME AS String = "Exempt421aMedianIncome"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__OLD__HISTORY__FILE__POINTER & "]," & _
			"[" & SQL_COL__NEW__TENANT__FLAG & "]," & _
			"[" & SQL_COL__REPLACING__TENANT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__DUE__DATE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME__FLAG & "]," & _
			"[" & SQL_COL__LEASE__LETTER__FLAG & "]," & _
			"[" & SQL_COL__LOW__RENT__FLAG & "]," & _
			"[" & SQL_COL__LEGAL__CODE & "]," & _
			"[" & SQL_COL__LEGAL__CODE_2 & "]," & _
			"[" & SQL_COL__RENT & "]," & _
			"[" & SQL_COL__SECURITY & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__FUTURE__RENT_1 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_2 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_3 & "]," & _
			"[" & SQL_COL__M_B_R & "]," & _
			"[" & SQL_COL__FUTURE__DATE_1 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_2 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_3 & "]," & _
			"[" & SQL_COL__ROOMS & "]," & _
			"[" & SQL_COL__SQUARE__FEET & "]," & _
			"[" & SQL_COL__MOVE_IN__DATE & "]," & _
			"[" & SQL_COL__LEASE__START__DATE & "]," & _
			"[" & SQL_COL__LEASE__EXPIRATION__DATE & "]," & _
			"[" & SQL_COL__LAST__PAID__DATE & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME & "]," & _
			"[" & SQL_COL__S_S_NUMBER1 & "]," & _
			"[" & SQL_COL__S_S_NUMBER2 & "]," & _
			"[" & SQL_COL__HOME_PHONE1 & "]," & _
			"[" & SQL_COL__HOME_PHONE2 & "]," & _
			"[" & SQL_COL__BUS_PHONE1 & "]," & _
			"[" & SQL_COL__BUS_PHONE2 & "]," & _
			"[" & SQL_COL__ALT_BILL_NAME & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD1 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD2 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD3 & "]," & _
			"[" & SQL_COL__ADD_NAME & "]," & _
			"[" & SQL_COL__BANK & "]," & _
			"[" & SQL_COL__INTEREST_INFO & "]," & _
			"[" & SQL_COL__SHARES & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__SENIORCIT & "]," & _
			"[" & SQL_COL__TOTAL__CHARGES & "]," & _
			"[" & SQL_COL__PAGER & "]," & _
			"[" & SQL_COL_DHCR_DATE_ONE & "]," & _
			"[" & SQL_COL_DHCR_RENT_ONE & "]," & _
			"[" & SQL_COL_DHCR_STOVE_ONE & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_ONE & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_A_C_ONE & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_ONE & "]," & _
			"[" & SQL_COL_DHCR_DATE_TWO & "]," & _
			"[" & SQL_COL_DHCR_RENT_TWO & "]," & _
			"[" & SQL_COL_DHCR_STOVE_TWO & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_TWO & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_A_C_TWO & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_TWO & "]," & _
			"[" & SQL_COL__LEGAL_FEE & "]," & _
			"[" & SQL_COL__LATE_FEE & "]," & _
			"[" & SQL_COL__LATE_BASE & "]," & _
			"[" & SQL_COL__LEGAL_FEE_NO & "]," & _
			"[" & SQL_COL__LATE_FEE_YES & "]," & _
			"[" & SQL_COL__LATE_FEE_NO & "]," & _
			"[" & SQL_COL__RENT_LEGAL & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_1 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_2 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_3 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT & "]," & _
			"[" & SQL_COL__PROMPT_PAY1 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT2 & "]," & _
			"[" & SQL_COL__PROMPT_PAY2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL & "]," & _
			"[" & SQL_COL__CELL2 & "]," & _
			"[" & SQL_COL__SEC_MULTIPLE & "]," & _
			"[" & SQL_COL__EMAIL1 & "]," & _
			"[" & SQL_COL__EMAIL2 & "]," & _
			"[" & SQL_COL__TEMP1 & "]," & _
			"[" & SQL_COL__TEMP2 & "]," & _
			"[" & SQL_COL__SECURITY_ACCOUNT & "]," & _
			"[" & SQL_COL__OPT_TENANT1 & "]," & _
			"[" & SQL_COL__OPT_TENANT2 & "]," & _
			"[" & SQL_COL__SECURITY_BUILDING & "]," & _
			"[" & SQL_COL__SUCCEEDED & "]," & _
			"[" & SQL_COL__SECOND_SUCCESSION & "]," & _
			"[" & SQL_COL_421A & "]," & _
			"[" & SQL_COL__RENT_OVERCHARGE & "]," & _
			"[" & SQL_COL__FAIR_MARKET_APPEAL & "]," & _
			"[" & SQL_COL__TRANSIENT_OCCUPANT & "]," & _
			"[" & SQL_COL__OWNER_OCCUPIED & "]," & _
			"[" & SQL_COL__COMMERCIAL_PROFESSIONAL & "]," & _
			"[" & SQL_COL__NOT_PRIME_RESIDENCE & "]," & _
			"[" & SQL_COL__TEMP_EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_DATE & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANCY & "]," & _
			"[" & SQL_COL__EXEMPT_LAST_LEGAL_PAID & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_INCOME & "]," & _
			"[" & SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO & "]," & _
			"[" & SQL_COL__EXEMPT_SUBSTANTIAL_REHAB & "]," & _
			"[" & SQL_COL__EXEMPT_COOP_CONDO_OWNER & "]," & _
			"[" & SQL_COL__EXEMPT_SEC11243 & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER_DESC & "]," & _
			"[" & SQL_COL__EXEMPT_EXP_SEC608 & "]," & _
			"[" & SQL_COL__EXEMPT_EXP421A & "]," & _
			"[" & SQL_COL__EXEMPT421A_MARKET_RATE & "]," & _
			"[" & SQL_COL__EXEMPT421A_INCOME & "]," & _
			"[" & SQL_COL__EXEMPT421A_MEDIAN_INCOME & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__OLD__HISTORY__FILE__POINTER & "]," & _
			"[" & SQL_COL__NEW__TENANT__FLAG & "]," & _
			"[" & SQL_COL__REPLACING__TENANT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__DUE__DATE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME__FLAG & "]," & _
			"[" & SQL_COL__LEASE__LETTER__FLAG & "]," & _
			"[" & SQL_COL__LOW__RENT__FLAG & "]," & _
			"[" & SQL_COL__LEGAL__CODE & "]," & _
			"[" & SQL_COL__LEGAL__CODE_2 & "]," & _
			"[" & SQL_COL__RENT & "]," & _
			"[" & SQL_COL__SECURITY & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__FUTURE__RENT_1 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_2 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_3 & "]," & _
			"[" & SQL_COL__M_B_R & "]," & _
			"[" & SQL_COL__FUTURE__DATE_1 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_2 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_3 & "]," & _
			"[" & SQL_COL__ROOMS & "]," & _
			"[" & SQL_COL__SQUARE__FEET & "]," & _
			"[" & SQL_COL__MOVE_IN__DATE & "]," & _
			"[" & SQL_COL__LEASE__START__DATE & "]," & _
			"[" & SQL_COL__LEASE__EXPIRATION__DATE & "]," & _
			"[" & SQL_COL__LAST__PAID__DATE & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME & "]," & _
			"[" & SQL_COL__S_S_NUMBER1 & "]," & _
			"[" & SQL_COL__S_S_NUMBER2 & "]," & _
			"[" & SQL_COL__HOME_PHONE1 & "]," & _
			"[" & SQL_COL__HOME_PHONE2 & "]," & _
			"[" & SQL_COL__BUS_PHONE1 & "]," & _
			"[" & SQL_COL__BUS_PHONE2 & "]," & _
			"[" & SQL_COL__ALT_BILL_NAME & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD1 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD2 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD3 & "]," & _
			"[" & SQL_COL__ADD_NAME & "]," & _
			"[" & SQL_COL__BANK & "]," & _
			"[" & SQL_COL__INTEREST_INFO & "]," & _
			"[" & SQL_COL__SHARES & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__SENIORCIT & "]," & _
			"[" & SQL_COL__TOTAL__CHARGES & "]," & _
			"[" & SQL_COL__PAGER & "]," & _
			"[" & SQL_COL_DHCR_DATE_ONE & "]," & _
			"[" & SQL_COL_DHCR_RENT_ONE & "]," & _
			"[" & SQL_COL_DHCR_STOVE_ONE & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_ONE & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_A_C_ONE & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_ONE & "]," & _
			"[" & SQL_COL_DHCR_DATE_TWO & "]," & _
			"[" & SQL_COL_DHCR_RENT_TWO & "]," & _
			"[" & SQL_COL_DHCR_STOVE_TWO & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_TWO & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_A_C_TWO & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_TWO & "]," & _
			"[" & SQL_COL__LEGAL_FEE & "]," & _
			"[" & SQL_COL__LATE_FEE & "]," & _
			"[" & SQL_COL__LATE_BASE & "]," & _
			"[" & SQL_COL__LEGAL_FEE_NO & "]," & _
			"[" & SQL_COL__LATE_FEE_YES & "]," & _
			"[" & SQL_COL__LATE_FEE_NO & "]," & _
			"[" & SQL_COL__RENT_LEGAL & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_1 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_2 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_3 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT & "]," & _
			"[" & SQL_COL__PROMPT_PAY1 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT2 & "]," & _
			"[" & SQL_COL__PROMPT_PAY2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL & "]," & _
			"[" & SQL_COL__CELL2 & "]," & _
			"[" & SQL_COL__SEC_MULTIPLE & "]," & _
			"[" & SQL_COL__EMAIL1 & "]," & _
			"[" & SQL_COL__EMAIL2 & "]," & _
			"[" & SQL_COL__TEMP1 & "]," & _
			"[" & SQL_COL__TEMP2 & "]," & _
			"[" & SQL_COL__SECURITY_ACCOUNT & "]," & _
			"[" & SQL_COL__OPT_TENANT1 & "]," & _
			"[" & SQL_COL__OPT_TENANT2 & "]," & _
			"[" & SQL_COL__SECURITY_BUILDING & "]," & _
			"[" & SQL_COL__SUCCEEDED & "]," & _
			"[" & SQL_COL__SECOND_SUCCESSION & "]," & _
			"[" & SQL_COL_421A & "]," & _
			"[" & SQL_COL__RENT_OVERCHARGE & "]," & _
			"[" & SQL_COL__FAIR_MARKET_APPEAL & "]," & _
			"[" & SQL_COL__TRANSIENT_OCCUPANT & "]," & _
			"[" & SQL_COL__OWNER_OCCUPIED & "]," & _
			"[" & SQL_COL__COMMERCIAL_PROFESSIONAL & "]," & _
			"[" & SQL_COL__NOT_PRIME_RESIDENCE & "]," & _
			"[" & SQL_COL__TEMP_EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_DATE & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANCY & "]," & _
			"[" & SQL_COL__EXEMPT_LAST_LEGAL_PAID & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_INCOME & "]," & _
			"[" & SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO & "]," & _
			"[" & SQL_COL__EXEMPT_SUBSTANTIAL_REHAB & "]," & _
			"[" & SQL_COL__EXEMPT_COOP_CONDO_OWNER & "]," & _
			"[" & SQL_COL__EXEMPT_SEC11243 & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER_DESC & "]," & _
			"[" & SQL_COL__EXEMPT_EXP_SEC608 & "]," & _
			"[" & SQL_COL__EXEMPT_EXP421A & "]," & _
			"[" & SQL_COL__EXEMPT421A_MARKET_RATE & "]," & _
			"[" & SQL_COL__EXEMPT421A_INCOME & "]," & _
			"[" & SQL_COL__EXEMPT421A_MEDIAN_INCOME & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__OLD__HISTORY__FILE__POINTER & "]," & _
			"[" & SQL_COL__NEW__TENANT__FLAG & "]," & _
			"[" & SQL_COL__REPLACING__TENANT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__DUE__DATE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME__FLAG & "]," & _
			"[" & SQL_COL__LEASE__LETTER__FLAG & "]," & _
			"[" & SQL_COL__LOW__RENT__FLAG & "]," & _
			"[" & SQL_COL__LEGAL__CODE & "]," & _
			"[" & SQL_COL__LEGAL__CODE_2 & "]," & _
			"[" & SQL_COL__RENT & "]," & _
			"[" & SQL_COL__SECURITY & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__FUTURE__RENT_1 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_2 & "]," & _
			"[" & SQL_COL__FUTURE__RENT_3 & "]," & _
			"[" & SQL_COL__M_B_R & "]," & _
			"[" & SQL_COL__FUTURE__DATE_1 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_2 & "]," & _
			"[" & SQL_COL__FUTURE__DATE_3 & "]," & _
			"[" & SQL_COL__ROOMS & "]," & _
			"[" & SQL_COL__SQUARE__FEET & "]," & _
			"[" & SQL_COL__MOVE_IN__DATE & "]," & _
			"[" & SQL_COL__LEASE__START__DATE & "]," & _
			"[" & SQL_COL__LEASE__EXPIRATION__DATE & "]," & _
			"[" & SQL_COL__LAST__PAID__DATE & "]," & _
			"[" & SQL_COL__ADDITIONAL__NAME & "]," & _
			"[" & SQL_COL__S_S_NUMBER1 & "]," & _
			"[" & SQL_COL__S_S_NUMBER2 & "]," & _
			"[" & SQL_COL__HOME_PHONE1 & "]," & _
			"[" & SQL_COL__HOME_PHONE2 & "]," & _
			"[" & SQL_COL__BUS_PHONE1 & "]," & _
			"[" & SQL_COL__BUS_PHONE2 & "]," & _
			"[" & SQL_COL__ALT_BILL_NAME & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD1 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD2 & "]," & _
			"[" & SQL_COL__ALT_BILL_ADD3 & "]," & _
			"[" & SQL_COL__ADD_NAME & "]," & _
			"[" & SQL_COL__BANK & "]," & _
			"[" & SQL_COL__INTEREST_INFO & "]," & _
			"[" & SQL_COL__SHARES & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__SENIORCIT & "]," & _
			"[" & SQL_COL__TOTAL__CHARGES & "]," & _
			"[" & SQL_COL__PAGER & "]," & _
			"[" & SQL_COL_DHCR_DATE_ONE & "]," & _
			"[" & SQL_COL_DHCR_RENT_ONE & "]," & _
			"[" & SQL_COL_DHCR_STOVE_ONE & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_ONE & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_A_C_ONE & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_ONE & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_ONE & "]," & _
			"[" & SQL_COL_DHCR_DATE_TWO & "]," & _
			"[" & SQL_COL_DHCR_RENT_TWO & "]," & _
			"[" & SQL_COL_DHCR_STOVE_TWO & "]," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_TWO & "]," & _
			"[" & SQL_COL_DHCR_DISHWASHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_A_C_TWO & "]," & _
			"[" & SQL_COL_DHCR_WINDOWS_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TWO & "]," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_TWO & "]," & _
			"[" & SQL_COL__LEGAL_FEE & "]," & _
			"[" & SQL_COL__LATE_FEE & "]," & _
			"[" & SQL_COL__LATE_BASE & "]," & _
			"[" & SQL_COL__LEGAL_FEE_NO & "]," & _
			"[" & SQL_COL__LATE_FEE_YES & "]," & _
			"[" & SQL_COL__LATE_FEE_NO & "]," & _
			"[" & SQL_COL__RENT_LEGAL & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_1 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_2 & "]," & _
			"[" & SQL_COL__FUTURE__LEGAL_3 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT & "]," & _
			"[" & SQL_COL__PROMPT_PAY1 & "]," & _
			"[" & SQL_COL__PROPOSED_RENT2 & "]," & _
			"[" & SQL_COL__PROMPT_PAY2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL2 & "]," & _
			"[" & SQL_COL__PROPOSED_LEGAL & "]," & _
			"[" & SQL_COL__CELL2 & "]," & _
			"[" & SQL_COL__SEC_MULTIPLE & "]," & _
			"[" & SQL_COL__EMAIL1 & "]," & _
			"[" & SQL_COL__EMAIL2 & "]," & _
			"[" & SQL_COL__TEMP1 & "]," & _
			"[" & SQL_COL__TEMP2 & "]," & _
			"[" & SQL_COL__SECURITY_ACCOUNT & "]," & _
			"[" & SQL_COL__OPT_TENANT1 & "]," & _
			"[" & SQL_COL__OPT_TENANT2 & "]," & _
			"[" & SQL_COL__SECURITY_BUILDING & "]," & _
			"[" & SQL_COL__SUCCEEDED & "]," & _
			"[" & SQL_COL__SECOND_SUCCESSION & "]," & _
			"[" & SQL_COL_421A & "]," & _
			"[" & SQL_COL__RENT_OVERCHARGE & "]," & _
			"[" & SQL_COL__FAIR_MARKET_APPEAL & "]," & _
			"[" & SQL_COL__TRANSIENT_OCCUPANT & "]," & _
			"[" & SQL_COL__OWNER_OCCUPIED & "]," & _
			"[" & SQL_COL__COMMERCIAL_PROFESSIONAL & "]," & _
			"[" & SQL_COL__NOT_PRIME_RESIDENCE & "]," & _
			"[" & SQL_COL__TEMP_EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_DATE & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANCY & "]," & _
			"[" & SQL_COL__EXEMPT_LAST_LEGAL_PAID & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD & "]," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_INCOME & "]," & _
			"[" & SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO & "]," & _
			"[" & SQL_COL__EXEMPT_SUBSTANTIAL_REHAB & "]," & _
			"[" & SQL_COL__EXEMPT_COOP_CONDO_OWNER & "]," & _
			"[" & SQL_COL__EXEMPT_SEC11243 & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER & "]," & _
			"[" & SQL_COL__EXEMPT_OTHER_DESC & "]," & _
			"[" & SQL_COL__EXEMPT_EXP_SEC608 & "]," & _
			"[" & SQL_COL__EXEMPT_EXP421A & "]," & _
			"[" & SQL_COL__EXEMPT421A_MARKET_RATE & "]," & _
			"[" & SQL_COL__EXEMPT421A_INCOME & "]" & _
			"[" & SQL_COL__EXEMPT421A_MEDIAN_INCOME & "]" & _
			") VALUES ('?', '?', ?, ?, ?, ?, '?', '?', '?', '?', ?, ?, ?, ?, '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, #?#, #?#, #?#, ?, ?, #?#, #?#, #?#, #?#, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?', '?', ?, '?', #?#, ?, ?, ?, ?, ?, ?, ?, '?', #?#, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, '?', '?', '?', '?', '?', ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, #?#, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ??);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?," & _
			"[" & SQL_COL__OLD__HISTORY__FILE__POINTER & "]=?," & _
			"[" & SQL_COL__NEW__TENANT__FLAG & "]=?," & _
			"[" & SQL_COL__REPLACING__TENANT & "]=?," & _
			"[" & SQL_COL__TENANT__NAME & "]='?'," & _
			"[" & SQL_COL__APARTMENT & "]='?'," & _
			"[" & SQL_COL__DUE__DATE & "]='?'," & _
			"[" & SQL_COL__TYPE & "]='?'," & _
			"[" & SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG & "]=?," & _
			"[" & SQL_COL__ADDITIONAL__NAME__FLAG & "]=?," & _
			"[" & SQL_COL__LEASE__LETTER__FLAG & "]=?," & _
			"[" & SQL_COL__LOW__RENT__FLAG & "]=?," & _
			"[" & SQL_COL__LEGAL__CODE & "]='?'," & _
			"[" & SQL_COL__LEGAL__CODE_2 & "]='?'," & _
			"[" & SQL_COL__RENT & "]=?," & _
			"[" & SQL_COL__SECURITY & "]=?," & _
			"[" & SQL_COL__ARREARS & "]=?," & _
			"[" & SQL_COL__PAID & "]=?," & _
			"[" & SQL_COL__FUTURE__RENT_1 & "]=?," & _
			"[" & SQL_COL__FUTURE__RENT_2 & "]=?," & _
			"[" & SQL_COL__FUTURE__RENT_3 & "]=?," & _
			"[" & SQL_COL__M_B_R & "]=?," & _
			"[" & SQL_COL__FUTURE__DATE_1 & "]=#?#," & _
			"[" & SQL_COL__FUTURE__DATE_2 & "]=#?#," & _
			"[" & SQL_COL__FUTURE__DATE_3 & "]=#?#," & _
			"[" & SQL_COL__ROOMS & "]=?," & _
			"[" & SQL_COL__SQUARE__FEET & "]=?," & _
			"[" & SQL_COL__MOVE_IN__DATE & "]=#?#," & _
			"[" & SQL_COL__LEASE__START__DATE & "]=#?#," & _
			"[" & SQL_COL__LEASE__EXPIRATION__DATE & "]=#?#," & _
			"[" & SQL_COL__LAST__PAID__DATE & "]=#?#," & _
			"[" & SQL_COL__ADDITIONAL__NAME & "]='?'," & _
			"[" & SQL_COL__S_S_NUMBER1 & "]='?'," & _
			"[" & SQL_COL__S_S_NUMBER2 & "]='?'," & _
			"[" & SQL_COL__HOME_PHONE1 & "]='?'," & _
			"[" & SQL_COL__HOME_PHONE2 & "]='?'," & _
			"[" & SQL_COL__BUS_PHONE1 & "]='?'," & _
			"[" & SQL_COL__BUS_PHONE2 & "]='?'," & _
			"[" & SQL_COL__ALT_BILL_NAME & "]='?'," & _
			"[" & SQL_COL__ALT_BILL_ADD1 & "]='?'," & _
			"[" & SQL_COL__ALT_BILL_ADD2 & "]='?'," & _
			"[" & SQL_COL__ALT_BILL_ADD3 & "]='?'," & _
			"[" & SQL_COL__ADD_NAME & "]='?'," & _
			"[" & SQL_COL__BANK & "]='?'," & _
			"[" & SQL_COL__INTEREST_INFO & "]='?'," & _
			"[" & SQL_COL__SHARES & "]=?," & _
			"[" & SQL_COL__COMMENT & "]='?'," & _
			"[" & SQL_COL__SENIORCIT & "]='?'," & _
			"[" & SQL_COL__TOTAL__CHARGES & "]=?," & _
			"[" & SQL_COL__PAGER & "]='?'," & _
			"[" & SQL_COL_DHCR_DATE_ONE & "]=#?#," & _
			"[" & SQL_COL_DHCR_RENT_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_STOVE_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_DISHWASHER_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_A_C_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_WINDOWS_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_OTHER_ONE & "]=?," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_ONE & "]='?'," & _
			"[" & SQL_COL_DHCR_DATE_TWO & "]=#?#," & _
			"[" & SQL_COL_DHCR_RENT_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_STOVE_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_REFRIGERATOR_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_DISHWASHER_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_A_C_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_WINDOWS_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_OTHER_TWO & "]=?," & _
			"[" & SQL_COL_DHCR_OTHER_TEXT_TWO & "]='?'," & _
			"[" & SQL_COL__LEGAL_FEE & "]=?," & _
			"[" & SQL_COL__LATE_FEE & "]=?," & _
			"[" & SQL_COL__LATE_BASE & "]=?," & _
			"[" & SQL_COL__LEGAL_FEE_NO & "]=?," & _
			"[" & SQL_COL__LATE_FEE_YES & "]=?," & _
			"[" & SQL_COL__LATE_FEE_NO & "]=?," & _
			"[" & SQL_COL__RENT_LEGAL & "]=?," & _
			"[" & SQL_COL__FUTURE__LEGAL_1 & "]=?," & _
			"[" & SQL_COL__FUTURE__LEGAL_2 & "]=?," & _
			"[" & SQL_COL__FUTURE__LEGAL_3 & "]=?," & _
			"[" & SQL_COL__PROPOSED_RENT & "]=?," & _
			"[" & SQL_COL__PROMPT_PAY1 & "]=?," & _
			"[" & SQL_COL__PROPOSED_RENT2 & "]=?," & _
			"[" & SQL_COL__PROMPT_PAY2 & "]=?," & _
			"[" & SQL_COL__PROPOSED_LEGAL2 & "]=?," & _
			"[" & SQL_COL__PROPOSED_LEGAL & "]=?," & _
			"[" & SQL_COL__CELL2 & "]='?'," & _
			"[" & SQL_COL__SEC_MULTIPLE & "]=?," & _
			"[" & SQL_COL__EMAIL1 & "]='?'," & _
			"[" & SQL_COL__EMAIL2 & "]='?'," & _
			"[" & SQL_COL__TEMP1 & "]='?'," & _
			"[" & SQL_COL__TEMP2 & "]='?'," & _
			"[" & SQL_COL__SECURITY_ACCOUNT & "]='?'," & _
			"[" & SQL_COL__OPT_TENANT1 & "]=?," & _
			"[" & SQL_COL__OPT_TENANT2 & "]=?," & _
			"[" & SQL_COL__SECURITY_BUILDING & "]='?'," & _
			"[" & SQL_COL__SUCCEEDED & "]=?," & _
			"[" & SQL_COL__SECOND_SUCCESSION & "]=?," & _
			"[" & SQL_COL_421A & "]=?," & _
			"[" & SQL_COL__RENT_OVERCHARGE & "]=?," & _
			"[" & SQL_COL__FAIR_MARKET_APPEAL & "]=?," & _
			"[" & SQL_COL__TRANSIENT_OCCUPANT & "]=?," & _
			"[" & SQL_COL__OWNER_OCCUPIED & "]=?," & _
			"[" & SQL_COL__COMMERCIAL_PROFESSIONAL & "]=?," & _
			"[" & SQL_COL__NOT_PRIME_RESIDENCE & "]=?," & _
			"[" & SQL_COL__TEMP_EXEMPT_OTHER & "]=?," & _
			"[" & SQL_COL__EXEMPT_DATE & "]=#?#," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANCY & "]=?," & _
			"[" & SQL_COL__EXEMPT_LAST_LEGAL_PAID & "]=?," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD & "]=?," & _
			"[" & SQL_COL__EXEMPT_HIGH_RENT_INCOME & "]=?," & _
			"[" & SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO & "]=?," & _
			"[" & SQL_COL__EXEMPT_SUBSTANTIAL_REHAB & "]=?," & _
			"[" & SQL_COL__EXEMPT_COOP_CONDO_OWNER & "]=?," & _
			"[" & SQL_COL__EXEMPT_SEC11243 & "]=?," & _
			"[" & SQL_COL__EXEMPT_OTHER & "]=?," & _
			"[" & SQL_COL__EXEMPT_OTHER_DESC & "]='?'," & _
			"[" & SQL_COL__EXEMPT_EXP_SEC608 & "]=?," & _
			"[" & SQL_COL__EXEMPT_EXP421A & "]=?," & _
			"[" & SQL_COL__EXEMPT421A_MARKET_RATE & "]=?," & _
			"[" & SQL_COL__EXEMPT421A_INCOME & "]=?" & _
			"[" & SQL_COL__EXEMPT421A_MEDIAN_INCOME & "]=?" & _
			" WHERE " & SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_SequenceNumber As Int32
	Protected m_OldHistoryFilePointer As Int32
	Protected m_NewTenantFlag As Boolean
	Protected m_ReplacingTenant As Int32
	Protected m_TenantName As String
	Protected m_Apartment As String
	Protected m_DueDate As String
	Protected m_Type As String
	Protected m_AlternateBillingAddressFlag As Boolean
	Protected m_AdditionalNameFlag As Boolean
	Protected m_LeaseLetterFlag As Boolean
	Protected m_LowRentFlag As Boolean
	Protected m_LegalCode As String
	Protected m_LegalCode2 As String
	Protected m_Rent As Decimal
	Protected m_Security As Decimal
	Protected m_Arrears As Decimal
	Protected m_Paid As Decimal
	Protected m_FutureRent1 As Decimal
	Protected m_FutureRent2 As Decimal
	Protected m_FutureRent3 As Decimal
	Protected m_MBR As Decimal
	Protected m_FutureDate1 As DateTime
	Protected m_FutureDate2 As DateTime
	Protected m_FutureDate3 As DateTime
	Protected m_Rooms As Single
	Protected m_SquareFeet As Single
	Protected m_MoveinDate As DateTime
	Protected m_LeaseStartDate As DateTime
	Protected m_LeaseExpirationDate As DateTime
	Protected m_LastPaidDate As DateTime
	Protected m_AdditionalName As String
	Protected m_SSNumber1 As String
	Protected m_SSNumber2 As String
	Protected m_HomePhone1 As String
	Protected m_HomePhone2 As String
	Protected m_BusPhone1 As String
	Protected m_BusPhone2 As String
	Protected m_AltBillName As String
	Protected m_AltBillAdd1 As String
	Protected m_AltBillAdd2 As String
	Protected m_AltBillAdd3 As String
	Protected m_AddName As String
	Protected m_Bank As String
	Protected m_InterestInfo As String
	Protected m_Shares As Single
	Protected m_Comment As String
	Protected m_Seniorcit As String
	Protected m_TotalCharges As Decimal
	Protected m_Pager As String
	Protected m_dhcrDateOne As DateTime
	Protected m_dhcrRentOne As Decimal
	Protected m_dhcrStoveOne As Boolean
	Protected m_dhcrRefrigeratorOne As Boolean
	Protected m_dhcrDishwasherOne As Boolean
	Protected m_dhcrACOne As Boolean
	Protected m_dhcrWindowsOne As Boolean
	Protected m_dhcrOtherOne As Boolean
	Protected m_dhcrOtherTextOne As String
	Protected m_dhcrDateTwo As DateTime
	Protected m_dhcrRentTwo As Decimal
	Protected m_dhcrStoveTwo As Boolean
	Protected m_dhcrRefrigeratorTwo As Boolean
	Protected m_dhcrDishwasherTwo As Boolean
	Protected m_dhcrACTwo As Boolean
	Protected m_dhcrWindowsTwo As Boolean
	Protected m_dhcrOtherTwo As Boolean
	Protected m_dhcrOtherTextTwo As String
	Protected m_LegalFee As Boolean
	Protected m_LateFee As Decimal
	Protected m_LateBase As Decimal
	Protected m_LegalFeeNo As Boolean
	Protected m_LateFeeYes As Boolean
	Protected m_LateFeeNo As Boolean
	Protected m_RentLegal As Decimal
	Protected m_FutureLegal1 As Decimal
	Protected m_FutureLegal2 As Decimal
	Protected m_FutureLegal3 As Decimal
	Protected m_ProposedRent As Decimal
	Protected m_PromptPay1 As Decimal
	Protected m_ProposedRent2 As Decimal
	Protected m_PromptPay2 As Decimal
	Protected m_ProposedLegal2 As Decimal
	Protected m_ProposedLegal As Decimal
	Protected m_Cell2 As String
	Protected m_SecMultiple As Single
	Protected m_Email1 As String
	Protected m_Email2 As String
	Protected m_Temp1 As String
	Protected m_Temp2 As String
	Protected m_SecurityAccount As String
	Protected m_OptTenant1 As Int32
	Protected m_OptTenant2 As Int32
	Protected m_SecurityBuilding As String
	Protected m_Succeeded As Boolean
	Protected m_SecondSuccession As Boolean
	Protected m_421a As Boolean
	Protected m_RentOvercharge As Boolean
	Protected m_FairMarketAppeal As Boolean
	Protected m_TransientOccupant As Boolean
	Protected m_OwnerOccupied As Boolean
	Protected m_CommercialProfessional As Boolean
	Protected m_NotPrimeResidence As Boolean
	Protected m_TempExemptOther As Boolean
	Protected m_ExemptDate As DateTime
	Protected m_ExemptHighRentVacancy As Boolean
	Protected m_ExemptLastLegalPaid As Decimal
	Protected m_ExemptHighRentVacanyPeriod As Int32
	Protected m_ExemptHighRentIncome As Boolean
	Protected m_ExemptCommercialProfessionalCo As Boolean
	Protected m_ExemptSubstantialRehab As Boolean
	Protected m_ExemptCoopCondoOwner As Boolean
	Protected m_ExemptSec11243 As Boolean
	Protected m_ExemptOther As Boolean
	Protected m_ExemptOtherDesc As String
	Protected m_ExemptExpSec608 As Boolean
	Protected m_ExemptExp421a As Boolean
	Protected m_Exempt421aMarketRate As Boolean
	Protected m_Exempt421aIncome As Boolean
	Protected m_Exempt421aMedianIncome As Int32

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__SEQUENCE__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal SequenceNumber As Int32,ByVal OldHistoryFilePointer As Int32,ByVal NewTenantFlag As Boolean,ByVal ReplacingTenant As Int32,ByVal TenantName As String,ByVal Apartment As String,ByVal DueDate As String,ByVal Type As String,ByVal AlternateBillingAddressFlag As Boolean,ByVal AdditionalNameFlag As Boolean,ByVal LeaseLetterFlag As Boolean,ByVal LowRentFlag As Boolean,ByVal LegalCode As String,ByVal LegalCode2 As String,ByVal Rent As Decimal,ByVal Security As Decimal,ByVal Arrears As Decimal,ByVal Paid As Decimal,ByVal FutureRent1 As Decimal,ByVal FutureRent2 As Decimal,ByVal FutureRent3 As Decimal,ByVal MBR As Decimal,ByVal FutureDate1 As DateTime,ByVal FutureDate2 As DateTime,ByVal FutureDate3 As DateTime,ByVal Rooms As Single,ByVal SquareFeet As Single,ByVal MoveinDate As DateTime,ByVal LeaseStartDate As DateTime,ByVal LeaseExpirationDate As DateTime,ByVal LastPaidDate As DateTime,ByVal AdditionalName As String,ByVal SSNumber1 As String,ByVal SSNumber2 As String,ByVal HomePhone1 As String,ByVal HomePhone2 As String,ByVal BusPhone1 As String,ByVal BusPhone2 As String,ByVal AltBillName As String,ByVal AltBillAdd1 As String,ByVal AltBillAdd2 As String,ByVal AltBillAdd3 As String,ByVal AddName As String,ByVal Bank As String,ByVal InterestInfo As String,ByVal Shares As Single,ByVal Comment As String,ByVal Seniorcit As String,ByVal TotalCharges As Decimal,ByVal Pager As String,ByVal dhcrDateOne As DateTime,ByVal dhcrRentOne As Decimal,ByVal dhcrStoveOne As Boolean,ByVal dhcrRefrigeratorOne As Boolean,ByVal dhcrDishwasherOne As Boolean,ByVal dhcrACOne As Boolean,ByVal dhcrWindowsOne As Boolean,ByVal dhcrOtherOne As Boolean,ByVal dhcrOtherTextOne As String,ByVal dhcrDateTwo As DateTime,ByVal dhcrRentTwo As Decimal,ByVal dhcrStoveTwo As Boolean,ByVal dhcrRefrigeratorTwo As Boolean,ByVal dhcrDishwasherTwo As Boolean,ByVal dhcrACTwo As Boolean,ByVal dhcrWindowsTwo As Boolean,ByVal dhcrOtherTwo As Boolean,ByVal dhcrOtherTextTwo As String,ByVal LegalFee As Boolean,ByVal LateFee As Decimal,ByVal LateBase As Decimal,ByVal LegalFeeNo As Boolean,ByVal LateFeeYes As Boolean,ByVal LateFeeNo As Boolean,ByVal RentLegal As Decimal,ByVal FutureLegal1 As Decimal,ByVal FutureLegal2 As Decimal,ByVal FutureLegal3 As Decimal,ByVal ProposedRent As Decimal,ByVal PromptPay1 As Decimal,ByVal ProposedRent2 As Decimal,ByVal PromptPay2 As Decimal,ByVal ProposedLegal2 As Decimal,ByVal ProposedLegal As Decimal,ByVal Cell2 As String,ByVal SecMultiple As Single,ByVal Email1 As String,ByVal Email2 As String,ByVal Temp1 As String,ByVal Temp2 As String,ByVal SecurityAccount As String,ByVal OptTenant1 As Int32,ByVal OptTenant2 As Int32,ByVal SecurityBuilding As String,ByVal Succeeded As Boolean,ByVal SecondSuccession As Boolean,ByVal 421a As Boolean,ByVal RentOvercharge As Boolean,ByVal FairMarketAppeal As Boolean,ByVal TransientOccupant As Boolean,ByVal OwnerOccupied As Boolean,ByVal CommercialProfessional As Boolean,ByVal NotPrimeResidence As Boolean,ByVal TempExemptOther As Boolean,ByVal ExemptDate As DateTime,ByVal ExemptHighRentVacancy As Boolean,ByVal ExemptLastLegalPaid As Decimal,ByVal ExemptHighRentVacanyPeriod As Int32,ByVal ExemptHighRentIncome As Boolean,ByVal ExemptCommercialProfessionalCo As Boolean,ByVal ExemptSubstantialRehab As Boolean,ByVal ExemptCoopCondoOwner As Boolean,ByVal ExemptSec11243 As Boolean,ByVal ExemptOther As Boolean,ByVal ExemptOtherDesc As String,ByVal ExemptExpSec608 As Boolean,ByVal ExemptExp421a As Boolean,ByVal Exempt421aMarketRate As Boolean,ByVal Exempt421aIncome As Boolean,ByVal Exempt421aMedianIncome As Int32)
		MyBase.New(TABLE, SQL_COL__SEQUENCE__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_SequenceNumber = SequenceNumber
		m_OldHistoryFilePointer = OldHistoryFilePointer
		m_NewTenantFlag = NewTenantFlag
		m_ReplacingTenant = ReplacingTenant
		m_TenantName = TenantName
		m_Apartment = Apartment
		m_DueDate = DueDate
		m_Type = Type
		m_AlternateBillingAddressFlag = AlternateBillingAddressFlag
		m_AdditionalNameFlag = AdditionalNameFlag
		m_LeaseLetterFlag = LeaseLetterFlag
		m_LowRentFlag = LowRentFlag
		m_LegalCode = LegalCode
		m_LegalCode2 = LegalCode2
		m_Rent = Rent
		m_Security = Security
		m_Arrears = Arrears
		m_Paid = Paid
		m_FutureRent1 = FutureRent1
		m_FutureRent2 = FutureRent2
		m_FutureRent3 = FutureRent3
		m_MBR = MBR
		m_FutureDate1 = FutureDate1
		m_FutureDate2 = FutureDate2
		m_FutureDate3 = FutureDate3
		m_Rooms = Rooms
		m_SquareFeet = SquareFeet
		m_MoveinDate = MoveinDate
		m_LeaseStartDate = LeaseStartDate
		m_LeaseExpirationDate = LeaseExpirationDate
		m_LastPaidDate = LastPaidDate
		m_AdditionalName = AdditionalName
		m_SSNumber1 = SSNumber1
		m_SSNumber2 = SSNumber2
		m_HomePhone1 = HomePhone1
		m_HomePhone2 = HomePhone2
		m_BusPhone1 = BusPhone1
		m_BusPhone2 = BusPhone2
		m_AltBillName = AltBillName
		m_AltBillAdd1 = AltBillAdd1
		m_AltBillAdd2 = AltBillAdd2
		m_AltBillAdd3 = AltBillAdd3
		m_AddName = AddName
		m_Bank = Bank
		m_InterestInfo = InterestInfo
		m_Shares = Shares
		m_Comment = Comment
		m_Seniorcit = Seniorcit
		m_TotalCharges = TotalCharges
		m_Pager = Pager
		m_dhcrDateOne = dhcrDateOne
		m_dhcrRentOne = dhcrRentOne
		m_dhcrStoveOne = dhcrStoveOne
		m_dhcrRefrigeratorOne = dhcrRefrigeratorOne
		m_dhcrDishwasherOne = dhcrDishwasherOne
		m_dhcrACOne = dhcrACOne
		m_dhcrWindowsOne = dhcrWindowsOne
		m_dhcrOtherOne = dhcrOtherOne
		m_dhcrOtherTextOne = dhcrOtherTextOne
		m_dhcrDateTwo = dhcrDateTwo
		m_dhcrRentTwo = dhcrRentTwo
		m_dhcrStoveTwo = dhcrStoveTwo
		m_dhcrRefrigeratorTwo = dhcrRefrigeratorTwo
		m_dhcrDishwasherTwo = dhcrDishwasherTwo
		m_dhcrACTwo = dhcrACTwo
		m_dhcrWindowsTwo = dhcrWindowsTwo
		m_dhcrOtherTwo = dhcrOtherTwo
		m_dhcrOtherTextTwo = dhcrOtherTextTwo
		m_LegalFee = LegalFee
		m_LateFee = LateFee
		m_LateBase = LateBase
		m_LegalFeeNo = LegalFeeNo
		m_LateFeeYes = LateFeeYes
		m_LateFeeNo = LateFeeNo
		m_RentLegal = RentLegal
		m_FutureLegal1 = FutureLegal1
		m_FutureLegal2 = FutureLegal2
		m_FutureLegal3 = FutureLegal3
		m_ProposedRent = ProposedRent
		m_PromptPay1 = PromptPay1
		m_ProposedRent2 = ProposedRent2
		m_PromptPay2 = PromptPay2
		m_ProposedLegal2 = ProposedLegal2
		m_ProposedLegal = ProposedLegal
		m_Cell2 = Cell2
		m_SecMultiple = SecMultiple
		m_Email1 = Email1
		m_Email2 = Email2
		m_Temp1 = Temp1
		m_Temp2 = Temp2
		m_SecurityAccount = SecurityAccount
		m_OptTenant1 = OptTenant1
		m_OptTenant2 = OptTenant2
		m_SecurityBuilding = SecurityBuilding
		m_Succeeded = Succeeded
		m_SecondSuccession = SecondSuccession
		m_421a = 421a
		m_RentOvercharge = RentOvercharge
		m_FairMarketAppeal = FairMarketAppeal
		m_TransientOccupant = TransientOccupant
		m_OwnerOccupied = OwnerOccupied
		m_CommercialProfessional = CommercialProfessional
		m_NotPrimeResidence = NotPrimeResidence
		m_TempExemptOther = TempExemptOther
		m_ExemptDate = ExemptDate
		m_ExemptHighRentVacancy = ExemptHighRentVacancy
		m_ExemptLastLegalPaid = ExemptLastLegalPaid
		m_ExemptHighRentVacanyPeriod = ExemptHighRentVacanyPeriod
		m_ExemptHighRentIncome = ExemptHighRentIncome
		m_ExemptCommercialProfessionalCo = ExemptCommercialProfessionalCo
		m_ExemptSubstantialRehab = ExemptSubstantialRehab
		m_ExemptCoopCondoOwner = ExemptCoopCondoOwner
		m_ExemptSec11243 = ExemptSec11243
		m_ExemptOther = ExemptOther
		m_ExemptOtherDesc = ExemptOtherDesc
		m_ExemptExpSec608 = ExemptExpSec608
		m_ExemptExp421a = ExemptExp421a
		m_Exempt421aMarketRate = Exempt421aMarketRate
		m_Exempt421aIncome = Exempt421aIncome
		m_Exempt421aMedianIncome = Exempt421aMedianIncome
	End Sub

	Protected Function fill(ByVal dr as DataRow) As TenantsArchive

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
			m_OldHistoryFilePointer = dr(SQL_COL__OLD__HISTORY__FILE__POINTER)
			m_NewTenantFlag = dr(SQL_COL__NEW__TENANT__FLAG)
			m_ReplacingTenant = dr(SQL_COL__REPLACING__TENANT)
			m_TenantName = dr(SQL_COL__TENANT__NAME)
			m_Apartment = dr(SQL_COL__APARTMENT)
			m_DueDate = dr(SQL_COL__DUE__DATE)
			m_Type = dr(SQL_COL__TYPE)
			m_AlternateBillingAddressFlag = dr(SQL_COL__ALTERNATE__BILLING__ADDRESS__FLAG)
			m_AdditionalNameFlag = dr(SQL_COL__ADDITIONAL__NAME__FLAG)
			m_LeaseLetterFlag = dr(SQL_COL__LEASE__LETTER__FLAG)
			m_LowRentFlag = dr(SQL_COL__LOW__RENT__FLAG)
			m_LegalCode = dr(SQL_COL__LEGAL__CODE)
			m_LegalCode2 = dr(SQL_COL__LEGAL__CODE_2)
			m_Rent = dr(SQL_COL__RENT)
			m_Security = dr(SQL_COL__SECURITY)
			m_Arrears = dr(SQL_COL__ARREARS)
			m_Paid = dr(SQL_COL__PAID)
			m_FutureRent1 = dr(SQL_COL__FUTURE__RENT_1)
			m_FutureRent2 = dr(SQL_COL__FUTURE__RENT_2)
			m_FutureRent3 = dr(SQL_COL__FUTURE__RENT_3)
			m_MBR = dr(SQL_COL__M_B_R)
			m_FutureDate1 = dr(SQL_COL__FUTURE__DATE_1)
			m_FutureDate2 = dr(SQL_COL__FUTURE__DATE_2)
			m_FutureDate3 = dr(SQL_COL__FUTURE__DATE_3)
			m_Rooms = dr(SQL_COL__ROOMS)
			m_SquareFeet = dr(SQL_COL__SQUARE__FEET)
			m_MoveinDate = dr(SQL_COL__MOVE_IN__DATE)
			m_LeaseStartDate = dr(SQL_COL__LEASE__START__DATE)
			m_LeaseExpirationDate = dr(SQL_COL__LEASE__EXPIRATION__DATE)
			m_LastPaidDate = dr(SQL_COL__LAST__PAID__DATE)
			m_AdditionalName = dr(SQL_COL__ADDITIONAL__NAME)
			m_SSNumber1 = dr(SQL_COL__S_S_NUMBER1)
			m_SSNumber2 = dr(SQL_COL__S_S_NUMBER2)
			m_HomePhone1 = dr(SQL_COL__HOME_PHONE1)
			m_HomePhone2 = dr(SQL_COL__HOME_PHONE2)
			m_BusPhone1 = dr(SQL_COL__BUS_PHONE1)
			m_BusPhone2 = dr(SQL_COL__BUS_PHONE2)
			m_AltBillName = dr(SQL_COL__ALT_BILL_NAME)
			m_AltBillAdd1 = dr(SQL_COL__ALT_BILL_ADD1)
			m_AltBillAdd2 = dr(SQL_COL__ALT_BILL_ADD2)
			m_AltBillAdd3 = dr(SQL_COL__ALT_BILL_ADD3)
			m_AddName = dr(SQL_COL__ADD_NAME)
			m_Bank = dr(SQL_COL__BANK)
			m_InterestInfo = dr(SQL_COL__INTEREST_INFO)
			m_Shares = dr(SQL_COL__SHARES)
			m_Comment = dr(SQL_COL__COMMENT)
			m_Seniorcit = dr(SQL_COL__SENIORCIT)
			m_TotalCharges = dr(SQL_COL__TOTAL__CHARGES)
			m_Pager = dr(SQL_COL__PAGER)
			m_dhcrDateOne = dr(SQL_COL_DHCR_DATE_ONE)
			m_dhcrRentOne = dr(SQL_COL_DHCR_RENT_ONE)
			m_dhcrStoveOne = dr(SQL_COL_DHCR_STOVE_ONE)
			m_dhcrRefrigeratorOne = dr(SQL_COL_DHCR_REFRIGERATOR_ONE)
			m_dhcrDishwasherOne = dr(SQL_COL_DHCR_DISHWASHER_ONE)
			m_dhcrACOne = dr(SQL_COL_DHCR_A_C_ONE)
			m_dhcrWindowsOne = dr(SQL_COL_DHCR_WINDOWS_ONE)
			m_dhcrOtherOne = dr(SQL_COL_DHCR_OTHER_ONE)
			m_dhcrOtherTextOne = dr(SQL_COL_DHCR_OTHER_TEXT_ONE)
			m_dhcrDateTwo = dr(SQL_COL_DHCR_DATE_TWO)
			m_dhcrRentTwo = dr(SQL_COL_DHCR_RENT_TWO)
			m_dhcrStoveTwo = dr(SQL_COL_DHCR_STOVE_TWO)
			m_dhcrRefrigeratorTwo = dr(SQL_COL_DHCR_REFRIGERATOR_TWO)
			m_dhcrDishwasherTwo = dr(SQL_COL_DHCR_DISHWASHER_TWO)
			m_dhcrACTwo = dr(SQL_COL_DHCR_A_C_TWO)
			m_dhcrWindowsTwo = dr(SQL_COL_DHCR_WINDOWS_TWO)
			m_dhcrOtherTwo = dr(SQL_COL_DHCR_OTHER_TWO)
			m_dhcrOtherTextTwo = dr(SQL_COL_DHCR_OTHER_TEXT_TWO)
			m_LegalFee = dr(SQL_COL__LEGAL_FEE)
			m_LateFee = dr(SQL_COL__LATE_FEE)
			m_LateBase = dr(SQL_COL__LATE_BASE)
			m_LegalFeeNo = dr(SQL_COL__LEGAL_FEE_NO)
			m_LateFeeYes = dr(SQL_COL__LATE_FEE_YES)
			m_LateFeeNo = dr(SQL_COL__LATE_FEE_NO)
			m_RentLegal = dr(SQL_COL__RENT_LEGAL)
			m_FutureLegal1 = dr(SQL_COL__FUTURE__LEGAL_1)
			m_FutureLegal2 = dr(SQL_COL__FUTURE__LEGAL_2)
			m_FutureLegal3 = dr(SQL_COL__FUTURE__LEGAL_3)
			m_ProposedRent = dr(SQL_COL__PROPOSED_RENT)
			m_PromptPay1 = dr(SQL_COL__PROMPT_PAY1)
			m_ProposedRent2 = dr(SQL_COL__PROPOSED_RENT2)
			m_PromptPay2 = dr(SQL_COL__PROMPT_PAY2)
			m_ProposedLegal2 = dr(SQL_COL__PROPOSED_LEGAL2)
			m_ProposedLegal = dr(SQL_COL__PROPOSED_LEGAL)
			m_Cell2 = dr(SQL_COL__CELL2)
			m_SecMultiple = dr(SQL_COL__SEC_MULTIPLE)
			m_Email1 = dr(SQL_COL__EMAIL1)
			m_Email2 = dr(SQL_COL__EMAIL2)
			m_Temp1 = dr(SQL_COL__TEMP1)
			m_Temp2 = dr(SQL_COL__TEMP2)
			m_SecurityAccount = dr(SQL_COL__SECURITY_ACCOUNT)
			m_OptTenant1 = dr(SQL_COL__OPT_TENANT1)
			m_OptTenant2 = dr(SQL_COL__OPT_TENANT2)
			m_SecurityBuilding = dr(SQL_COL__SECURITY_BUILDING)
			m_Succeeded = dr(SQL_COL__SUCCEEDED)
			m_SecondSuccession = dr(SQL_COL__SECOND_SUCCESSION)
			m_421a = dr(SQL_COL_421A)
			m_RentOvercharge = dr(SQL_COL__RENT_OVERCHARGE)
			m_FairMarketAppeal = dr(SQL_COL__FAIR_MARKET_APPEAL)
			m_TransientOccupant = dr(SQL_COL__TRANSIENT_OCCUPANT)
			m_OwnerOccupied = dr(SQL_COL__OWNER_OCCUPIED)
			m_CommercialProfessional = dr(SQL_COL__COMMERCIAL_PROFESSIONAL)
			m_NotPrimeResidence = dr(SQL_COL__NOT_PRIME_RESIDENCE)
			m_TempExemptOther = dr(SQL_COL__TEMP_EXEMPT_OTHER)
			m_ExemptDate = dr(SQL_COL__EXEMPT_DATE)
			m_ExemptHighRentVacancy = dr(SQL_COL__EXEMPT_HIGH_RENT_VACANCY)
			m_ExemptLastLegalPaid = dr(SQL_COL__EXEMPT_LAST_LEGAL_PAID)
			m_ExemptHighRentVacanyPeriod = dr(SQL_COL__EXEMPT_HIGH_RENT_VACANY_PERIOD)
			m_ExemptHighRentIncome = dr(SQL_COL__EXEMPT_HIGH_RENT_INCOME)
			m_ExemptCommercialProfessionalCo = dr(SQL_COL__EXEMPT_COMMERCIAL_PROFESSIONAL_CO)
			m_ExemptSubstantialRehab = dr(SQL_COL__EXEMPT_SUBSTANTIAL_REHAB)
			m_ExemptCoopCondoOwner = dr(SQL_COL__EXEMPT_COOP_CONDO_OWNER)
			m_ExemptSec11243 = dr(SQL_COL__EXEMPT_SEC11243)
			m_ExemptOther = dr(SQL_COL__EXEMPT_OTHER)
			m_ExemptOtherDesc = dr(SQL_COL__EXEMPT_OTHER_DESC)
			m_ExemptExpSec608 = dr(SQL_COL__EXEMPT_EXP_SEC608)
			m_ExemptExp421a = dr(SQL_COL__EXEMPT_EXP421A)
			m_Exempt421aMarketRate = dr(SQL_COL__EXEMPT421A_MARKET_RATE)
			m_Exempt421aIncome = dr(SQL_COL__EXEMPT421A_INCOME)
			m_Exempt421aMedianIncome = dr(SQL_COL__EXEMPT421A_MEDIAN_INCOME)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_SequenceNumber As Int32) As TenantsArchive

		Dim obj As TenantsArchive = New TenantsArchive
		Dim opt() as String = {m_SequenceNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As TenantsArchive()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As TenantsArchive = New TenantsArchive

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New TenantsArchive
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(TenantsArchive)), TenantsArchive())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_SequenceNumber > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_OldHistoryFilePointer, m_NewTenantFlag, m_ReplacingTenant, m_TenantName, m_Apartment, m_DueDate, m_Type, m_AlternateBillingAddressFlag, m_AdditionalNameFlag, m_LeaseLetterFlag, m_LowRentFlag, m_LegalCode, m_LegalCode2, m_Rent, m_Security, m_Arrears, m_Paid, m_FutureRent1, m_FutureRent2, m_FutureRent3, m_MBR, m_FutureDate1, m_FutureDate2, m_FutureDate3, m_Rooms, m_SquareFeet, m_MoveinDate, m_LeaseStartDate, m_LeaseExpirationDate, m_LastPaidDate, m_AdditionalName, m_SSNumber1, m_SSNumber2, m_HomePhone1, m_HomePhone2, m_BusPhone1, m_BusPhone2, m_AltBillName, m_AltBillAdd1, m_AltBillAdd2, m_AltBillAdd3, m_AddName, m_Bank, m_InterestInfo, m_Shares, m_Comment, m_Seniorcit, m_TotalCharges, m_Pager, m_dhcrDateOne, m_dhcrRentOne, m_dhcrStoveOne, m_dhcrRefrigeratorOne, m_dhcrDishwasherOne, m_dhcrACOne, m_dhcrWindowsOne, m_dhcrOtherOne, m_dhcrOtherTextOne, m_dhcrDateTwo, m_dhcrRentTwo, m_dhcrStoveTwo, m_dhcrRefrigeratorTwo, m_dhcrDishwasherTwo, m_dhcrACTwo, m_dhcrWindowsTwo, m_dhcrOtherTwo, m_dhcrOtherTextTwo, m_LegalFee, m_LateFee, m_LateBase, m_LegalFeeNo, m_LateFeeYes, m_LateFeeNo, m_RentLegal, m_FutureLegal1, m_FutureLegal2, m_FutureLegal3, m_ProposedRent, m_PromptPay1, m_ProposedRent2, m_PromptPay2, m_ProposedLegal2, m_ProposedLegal, m_Cell2, m_SecMultiple, m_Email1, m_Email2, m_Temp1, m_Temp2, m_SecurityAccount, m_OptTenant1, m_OptTenant2, m_SecurityBuilding, m_Succeeded, m_SecondSuccession, m_421a, m_RentOvercharge, m_FairMarketAppeal, m_TransientOccupant, m_OwnerOccupied, m_CommercialProfessional, m_NotPrimeResidence, m_TempExemptOther, m_ExemptDate, m_ExemptHighRentVacancy, m_ExemptLastLegalPaid, m_ExemptHighRentVacanyPeriod, m_ExemptHighRentIncome, m_ExemptCommercialProfessionalCo, m_ExemptSubstantialRehab, m_ExemptCoopCondoOwner, m_ExemptSec11243, m_ExemptOther, m_ExemptOtherDesc, m_ExemptExpSec608, m_ExemptExp421a, m_Exempt421aMarketRate, m_Exempt421aIncome, m_Exempt421aMedianIncome, m_SequenceNumber}
			If Me.updateRecord(m_SequenceNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_OldHistoryFilePointer, m_NewTenantFlag, m_ReplacingTenant, m_TenantName, m_Apartment, m_DueDate, m_Type, m_AlternateBillingAddressFlag, m_AdditionalNameFlag, m_LeaseLetterFlag, m_LowRentFlag, m_LegalCode, m_LegalCode2, m_Rent, m_Security, m_Arrears, m_Paid, m_FutureRent1, m_FutureRent2, m_FutureRent3, m_MBR, m_FutureDate1, m_FutureDate2, m_FutureDate3, m_Rooms, m_SquareFeet, m_MoveinDate, m_LeaseStartDate, m_LeaseExpirationDate, m_LastPaidDate, m_AdditionalName, m_SSNumber1, m_SSNumber2, m_HomePhone1, m_HomePhone2, m_BusPhone1, m_BusPhone2, m_AltBillName, m_AltBillAdd1, m_AltBillAdd2, m_AltBillAdd3, m_AddName, m_Bank, m_InterestInfo, m_Shares, m_Comment, m_Seniorcit, m_TotalCharges, m_Pager, m_dhcrDateOne, m_dhcrRentOne, m_dhcrStoveOne, m_dhcrRefrigeratorOne, m_dhcrDishwasherOne, m_dhcrACOne, m_dhcrWindowsOne, m_dhcrOtherOne, m_dhcrOtherTextOne, m_dhcrDateTwo, m_dhcrRentTwo, m_dhcrStoveTwo, m_dhcrRefrigeratorTwo, m_dhcrDishwasherTwo, m_dhcrACTwo, m_dhcrWindowsTwo, m_dhcrOtherTwo, m_dhcrOtherTextTwo, m_LegalFee, m_LateFee, m_LateBase, m_LegalFeeNo, m_LateFeeYes, m_LateFeeNo, m_RentLegal, m_FutureLegal1, m_FutureLegal2, m_FutureLegal3, m_ProposedRent, m_PromptPay1, m_ProposedRent2, m_PromptPay2, m_ProposedLegal2, m_ProposedLegal, m_Cell2, m_SecMultiple, m_Email1, m_Email2, m_Temp1, m_Temp2, m_SecurityAccount, m_OptTenant1, m_OptTenant2, m_SecurityBuilding, m_Succeeded, m_SecondSuccession, m_421a, m_RentOvercharge, m_FairMarketAppeal, m_TransientOccupant, m_OwnerOccupied, m_CommercialProfessional, m_NotPrimeResidence, m_TempExemptOther, m_ExemptDate, m_ExemptHighRentVacancy, m_ExemptLastLegalPaid, m_ExemptHighRentVacanyPeriod, m_ExemptHighRentIncome, m_ExemptCommercialProfessionalCo, m_ExemptSubstantialRehab, m_ExemptCoopCondoOwner, m_ExemptSec11243, m_ExemptOther, m_ExemptOtherDesc, m_ExemptExpSec608, m_ExemptExp421a, m_Exempt421aMarketRate, m_Exempt421aIncome, m_Exempt421aMedianIncome}
			m_SequenceNumber = Me.insertRecord(values)
			If m_SequenceNumber >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property BuildingNumber As String
		Get
			Return m_BuildingNumber
		End Get
		Set(ByVal value As String)
			m_BuildingNumber = value
		End Set
	End Property

	Public  Property TenantNumber As String
		Get
			Return m_TenantNumber
		End Get
		Set(ByVal value As String)
			m_TenantNumber = value
		End Set
	End Property

	Public  Property SequenceNumber As Int32
		Get
			Return m_SequenceNumber
		End Get
		Set(ByVal value As Int32)
			m_SequenceNumber = value
		End Set
	End Property

	Public  Property OldHistoryFilePointer As Int32
		Get
			Return m_OldHistoryFilePointer
		End Get
		Set(ByVal value As Int32)
			m_OldHistoryFilePointer = value
		End Set
	End Property

	Public  Property NewTenantFlag As Boolean
		Get
			Return m_NewTenantFlag
		End Get
		Set(ByVal value As Boolean)
			m_NewTenantFlag = value
		End Set
	End Property

	Public  Property ReplacingTenant As Int32
		Get
			Return m_ReplacingTenant
		End Get
		Set(ByVal value As Int32)
			m_ReplacingTenant = value
		End Set
	End Property

	Public  Property TenantName As String
		Get
			Return m_TenantName
		End Get
		Set(ByVal value As String)
			m_TenantName = value
		End Set
	End Property

	Public  Property Apartment As String
		Get
			Return m_Apartment
		End Get
		Set(ByVal value As String)
			m_Apartment = value
		End Set
	End Property

	Public  Property DueDate As String
		Get
			Return m_DueDate
		End Get
		Set(ByVal value As String)
			m_DueDate = value
		End Set
	End Property

	Public  Property Type As String
		Get
			Return m_Type
		End Get
		Set(ByVal value As String)
			m_Type = value
		End Set
	End Property

	Public  Property AlternateBillingAddressFlag As Boolean
		Get
			Return m_AlternateBillingAddressFlag
		End Get
		Set(ByVal value As Boolean)
			m_AlternateBillingAddressFlag = value
		End Set
	End Property

	Public  Property AdditionalNameFlag As Boolean
		Get
			Return m_AdditionalNameFlag
		End Get
		Set(ByVal value As Boolean)
			m_AdditionalNameFlag = value
		End Set
	End Property

	Public  Property LeaseLetterFlag As Boolean
		Get
			Return m_LeaseLetterFlag
		End Get
		Set(ByVal value As Boolean)
			m_LeaseLetterFlag = value
		End Set
	End Property

	Public  Property LowRentFlag As Boolean
		Get
			Return m_LowRentFlag
		End Get
		Set(ByVal value As Boolean)
			m_LowRentFlag = value
		End Set
	End Property

	Public  Property LegalCode As String
		Get
			Return m_LegalCode
		End Get
		Set(ByVal value As String)
			m_LegalCode = value
		End Set
	End Property

	Public  Property LegalCode2 As String
		Get
			Return m_LegalCode2
		End Get
		Set(ByVal value As String)
			m_LegalCode2 = value
		End Set
	End Property

	Public  Property Rent As Decimal
		Get
			Return m_Rent
		End Get
		Set(ByVal value As Decimal)
			m_Rent = value
		End Set
	End Property

	Public  Property Security As Decimal
		Get
			Return m_Security
		End Get
		Set(ByVal value As Decimal)
			m_Security = value
		End Set
	End Property

	Public  Property Arrears As Decimal
		Get
			Return m_Arrears
		End Get
		Set(ByVal value As Decimal)
			m_Arrears = value
		End Set
	End Property

	Public  Property Paid As Decimal
		Get
			Return m_Paid
		End Get
		Set(ByVal value As Decimal)
			m_Paid = value
		End Set
	End Property

	Public  Property FutureRent1 As Decimal
		Get
			Return m_FutureRent1
		End Get
		Set(ByVal value As Decimal)
			m_FutureRent1 = value
		End Set
	End Property

	Public  Property FutureRent2 As Decimal
		Get
			Return m_FutureRent2
		End Get
		Set(ByVal value As Decimal)
			m_FutureRent2 = value
		End Set
	End Property

	Public  Property FutureRent3 As Decimal
		Get
			Return m_FutureRent3
		End Get
		Set(ByVal value As Decimal)
			m_FutureRent3 = value
		End Set
	End Property

	Public  Property MBR As Decimal
		Get
			Return m_MBR
		End Get
		Set(ByVal value As Decimal)
			m_MBR = value
		End Set
	End Property

	Public  Property FutureDate1 As DateTime
		Get
			Return m_FutureDate1
		End Get
		Set(ByVal value As DateTime)
			m_FutureDate1 = value
		End Set
	End Property

	Public  Property FutureDate2 As DateTime
		Get
			Return m_FutureDate2
		End Get
		Set(ByVal value As DateTime)
			m_FutureDate2 = value
		End Set
	End Property

	Public  Property FutureDate3 As DateTime
		Get
			Return m_FutureDate3
		End Get
		Set(ByVal value As DateTime)
			m_FutureDate3 = value
		End Set
	End Property

	Public  Property Rooms As Single
		Get
			Return m_Rooms
		End Get
		Set(ByVal value As Single)
			m_Rooms = value
		End Set
	End Property

	Public  Property SquareFeet As Single
		Get
			Return m_SquareFeet
		End Get
		Set(ByVal value As Single)
			m_SquareFeet = value
		End Set
	End Property

	Public  Property MoveinDate As DateTime
		Get
			Return m_MoveinDate
		End Get
		Set(ByVal value As DateTime)
			m_MoveinDate = value
		End Set
	End Property

	Public  Property LeaseStartDate As DateTime
		Get
			Return m_LeaseStartDate
		End Get
		Set(ByVal value As DateTime)
			m_LeaseStartDate = value
		End Set
	End Property

	Public  Property LeaseExpirationDate As DateTime
		Get
			Return m_LeaseExpirationDate
		End Get
		Set(ByVal value As DateTime)
			m_LeaseExpirationDate = value
		End Set
	End Property

	Public  Property LastPaidDate As DateTime
		Get
			Return m_LastPaidDate
		End Get
		Set(ByVal value As DateTime)
			m_LastPaidDate = value
		End Set
	End Property

	Public  Property AdditionalName As String
		Get
			Return m_AdditionalName
		End Get
		Set(ByVal value As String)
			m_AdditionalName = value
		End Set
	End Property

	Public  Property SSNumber1 As String
		Get
			Return m_SSNumber1
		End Get
		Set(ByVal value As String)
			m_SSNumber1 = value
		End Set
	End Property

	Public  Property SSNumber2 As String
		Get
			Return m_SSNumber2
		End Get
		Set(ByVal value As String)
			m_SSNumber2 = value
		End Set
	End Property

	Public  Property HomePhone1 As String
		Get
			Return m_HomePhone1
		End Get
		Set(ByVal value As String)
			m_HomePhone1 = value
		End Set
	End Property

	Public  Property HomePhone2 As String
		Get
			Return m_HomePhone2
		End Get
		Set(ByVal value As String)
			m_HomePhone2 = value
		End Set
	End Property

	Public  Property BusPhone1 As String
		Get
			Return m_BusPhone1
		End Get
		Set(ByVal value As String)
			m_BusPhone1 = value
		End Set
	End Property

	Public  Property BusPhone2 As String
		Get
			Return m_BusPhone2
		End Get
		Set(ByVal value As String)
			m_BusPhone2 = value
		End Set
	End Property

	Public  Property AltBillName As String
		Get
			Return m_AltBillName
		End Get
		Set(ByVal value As String)
			m_AltBillName = value
		End Set
	End Property

	Public  Property AltBillAdd1 As String
		Get
			Return m_AltBillAdd1
		End Get
		Set(ByVal value As String)
			m_AltBillAdd1 = value
		End Set
	End Property

	Public  Property AltBillAdd2 As String
		Get
			Return m_AltBillAdd2
		End Get
		Set(ByVal value As String)
			m_AltBillAdd2 = value
		End Set
	End Property

	Public  Property AltBillAdd3 As String
		Get
			Return m_AltBillAdd3
		End Get
		Set(ByVal value As String)
			m_AltBillAdd3 = value
		End Set
	End Property

	Public  Property AddName As String
		Get
			Return m_AddName
		End Get
		Set(ByVal value As String)
			m_AddName = value
		End Set
	End Property

	Public  Property Bank As String
		Get
			Return m_Bank
		End Get
		Set(ByVal value As String)
			m_Bank = value
		End Set
	End Property

	Public  Property InterestInfo As String
		Get
			Return m_InterestInfo
		End Get
		Set(ByVal value As String)
			m_InterestInfo = value
		End Set
	End Property

	Public  Property Shares As Single
		Get
			Return m_Shares
		End Get
		Set(ByVal value As Single)
			m_Shares = value
		End Set
	End Property

	Public  Property Comment As String
		Get
			Return m_Comment
		End Get
		Set(ByVal value As String)
			m_Comment = value
		End Set
	End Property

	Public  Property Seniorcit As String
		Get
			Return m_Seniorcit
		End Get
		Set(ByVal value As String)
			m_Seniorcit = value
		End Set
	End Property

	Public  Property TotalCharges As Decimal
		Get
			Return m_TotalCharges
		End Get
		Set(ByVal value As Decimal)
			m_TotalCharges = value
		End Set
	End Property

	Public  Property Pager As String
		Get
			Return m_Pager
		End Get
		Set(ByVal value As String)
			m_Pager = value
		End Set
	End Property

	Public  Property dhcrDateOne As DateTime
		Get
			Return m_dhcrDateOne
		End Get
		Set(ByVal value As DateTime)
			m_dhcrDateOne = value
		End Set
	End Property

	Public  Property dhcrRentOne As Decimal
		Get
			Return m_dhcrRentOne
		End Get
		Set(ByVal value As Decimal)
			m_dhcrRentOne = value
		End Set
	End Property

	Public  Property dhcrStoveOne As Boolean
		Get
			Return m_dhcrStoveOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrStoveOne = value
		End Set
	End Property

	Public  Property dhcrRefrigeratorOne As Boolean
		Get
			Return m_dhcrRefrigeratorOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrRefrigeratorOne = value
		End Set
	End Property

	Public  Property dhcrDishwasherOne As Boolean
		Get
			Return m_dhcrDishwasherOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrDishwasherOne = value
		End Set
	End Property

	Public  Property dhcrACOne As Boolean
		Get
			Return m_dhcrACOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrACOne = value
		End Set
	End Property

	Public  Property dhcrWindowsOne As Boolean
		Get
			Return m_dhcrWindowsOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrWindowsOne = value
		End Set
	End Property

	Public  Property dhcrOtherOne As Boolean
		Get
			Return m_dhcrOtherOne
		End Get
		Set(ByVal value As Boolean)
			m_dhcrOtherOne = value
		End Set
	End Property

	Public  Property dhcrOtherTextOne As String
		Get
			Return m_dhcrOtherTextOne
		End Get
		Set(ByVal value As String)
			m_dhcrOtherTextOne = value
		End Set
	End Property

	Public  Property dhcrDateTwo As DateTime
		Get
			Return m_dhcrDateTwo
		End Get
		Set(ByVal value As DateTime)
			m_dhcrDateTwo = value
		End Set
	End Property

	Public  Property dhcrRentTwo As Decimal
		Get
			Return m_dhcrRentTwo
		End Get
		Set(ByVal value As Decimal)
			m_dhcrRentTwo = value
		End Set
	End Property

	Public  Property dhcrStoveTwo As Boolean
		Get
			Return m_dhcrStoveTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrStoveTwo = value
		End Set
	End Property

	Public  Property dhcrRefrigeratorTwo As Boolean
		Get
			Return m_dhcrRefrigeratorTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrRefrigeratorTwo = value
		End Set
	End Property

	Public  Property dhcrDishwasherTwo As Boolean
		Get
			Return m_dhcrDishwasherTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrDishwasherTwo = value
		End Set
	End Property

	Public  Property dhcrACTwo As Boolean
		Get
			Return m_dhcrACTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrACTwo = value
		End Set
	End Property

	Public  Property dhcrWindowsTwo As Boolean
		Get
			Return m_dhcrWindowsTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrWindowsTwo = value
		End Set
	End Property

	Public  Property dhcrOtherTwo As Boolean
		Get
			Return m_dhcrOtherTwo
		End Get
		Set(ByVal value As Boolean)
			m_dhcrOtherTwo = value
		End Set
	End Property

	Public  Property dhcrOtherTextTwo As String
		Get
			Return m_dhcrOtherTextTwo
		End Get
		Set(ByVal value As String)
			m_dhcrOtherTextTwo = value
		End Set
	End Property

	Public  Property LegalFee As Boolean
		Get
			Return m_LegalFee
		End Get
		Set(ByVal value As Boolean)
			m_LegalFee = value
		End Set
	End Property

	Public  Property LateFee As Decimal
		Get
			Return m_LateFee
		End Get
		Set(ByVal value As Decimal)
			m_LateFee = value
		End Set
	End Property

	Public  Property LateBase As Decimal
		Get
			Return m_LateBase
		End Get
		Set(ByVal value As Decimal)
			m_LateBase = value
		End Set
	End Property

	Public  Property LegalFeeNo As Boolean
		Get
			Return m_LegalFeeNo
		End Get
		Set(ByVal value As Boolean)
			m_LegalFeeNo = value
		End Set
	End Property

	Public  Property LateFeeYes As Boolean
		Get
			Return m_LateFeeYes
		End Get
		Set(ByVal value As Boolean)
			m_LateFeeYes = value
		End Set
	End Property

	Public  Property LateFeeNo As Boolean
		Get
			Return m_LateFeeNo
		End Get
		Set(ByVal value As Boolean)
			m_LateFeeNo = value
		End Set
	End Property

	Public  Property RentLegal As Decimal
		Get
			Return m_RentLegal
		End Get
		Set(ByVal value As Decimal)
			m_RentLegal = value
		End Set
	End Property

	Public  Property FutureLegal1 As Decimal
		Get
			Return m_FutureLegal1
		End Get
		Set(ByVal value As Decimal)
			m_FutureLegal1 = value
		End Set
	End Property

	Public  Property FutureLegal2 As Decimal
		Get
			Return m_FutureLegal2
		End Get
		Set(ByVal value As Decimal)
			m_FutureLegal2 = value
		End Set
	End Property

	Public  Property FutureLegal3 As Decimal
		Get
			Return m_FutureLegal3
		End Get
		Set(ByVal value As Decimal)
			m_FutureLegal3 = value
		End Set
	End Property

	Public  Property ProposedRent As Decimal
		Get
			Return m_ProposedRent
		End Get
		Set(ByVal value As Decimal)
			m_ProposedRent = value
		End Set
	End Property

	Public  Property PromptPay1 As Decimal
		Get
			Return m_PromptPay1
		End Get
		Set(ByVal value As Decimal)
			m_PromptPay1 = value
		End Set
	End Property

	Public  Property ProposedRent2 As Decimal
		Get
			Return m_ProposedRent2
		End Get
		Set(ByVal value As Decimal)
			m_ProposedRent2 = value
		End Set
	End Property

	Public  Property PromptPay2 As Decimal
		Get
			Return m_PromptPay2
		End Get
		Set(ByVal value As Decimal)
			m_PromptPay2 = value
		End Set
	End Property

	Public  Property ProposedLegal2 As Decimal
		Get
			Return m_ProposedLegal2
		End Get
		Set(ByVal value As Decimal)
			m_ProposedLegal2 = value
		End Set
	End Property

	Public  Property ProposedLegal As Decimal
		Get
			Return m_ProposedLegal
		End Get
		Set(ByVal value As Decimal)
			m_ProposedLegal = value
		End Set
	End Property

	Public  Property Cell2 As String
		Get
			Return m_Cell2
		End Get
		Set(ByVal value As String)
			m_Cell2 = value
		End Set
	End Property

	Public  Property SecMultiple As Single
		Get
			Return m_SecMultiple
		End Get
		Set(ByVal value As Single)
			m_SecMultiple = value
		End Set
	End Property

	Public  Property Email1 As String
		Get
			Return m_Email1
		End Get
		Set(ByVal value As String)
			m_Email1 = value
		End Set
	End Property

	Public  Property Email2 As String
		Get
			Return m_Email2
		End Get
		Set(ByVal value As String)
			m_Email2 = value
		End Set
	End Property

	Public  Property Temp1 As String
		Get
			Return m_Temp1
		End Get
		Set(ByVal value As String)
			m_Temp1 = value
		End Set
	End Property

	Public  Property Temp2 As String
		Get
			Return m_Temp2
		End Get
		Set(ByVal value As String)
			m_Temp2 = value
		End Set
	End Property

	Public  Property SecurityAccount As String
		Get
			Return m_SecurityAccount
		End Get
		Set(ByVal value As String)
			m_SecurityAccount = value
		End Set
	End Property

	Public  Property OptTenant1 As Int32
		Get
			Return m_OptTenant1
		End Get
		Set(ByVal value As Int32)
			m_OptTenant1 = value
		End Set
	End Property

	Public  Property OptTenant2 As Int32
		Get
			Return m_OptTenant2
		End Get
		Set(ByVal value As Int32)
			m_OptTenant2 = value
		End Set
	End Property

	Public  Property SecurityBuilding As String
		Get
			Return m_SecurityBuilding
		End Get
		Set(ByVal value As String)
			m_SecurityBuilding = value
		End Set
	End Property

	Public  Property Succeeded As Boolean
		Get
			Return m_Succeeded
		End Get
		Set(ByVal value As Boolean)
			m_Succeeded = value
		End Set
	End Property

	Public  Property SecondSuccession As Boolean
		Get
			Return m_SecondSuccession
		End Get
		Set(ByVal value As Boolean)
			m_SecondSuccession = value
		End Set
	End Property

	Public  Property 421a As Boolean
		Get
			Return m_421a
		End Get
		Set(ByVal value As Boolean)
			m_421a = value
		End Set
	End Property

	Public  Property RentOvercharge As Boolean
		Get
			Return m_RentOvercharge
		End Get
		Set(ByVal value As Boolean)
			m_RentOvercharge = value
		End Set
	End Property

	Public  Property FairMarketAppeal As Boolean
		Get
			Return m_FairMarketAppeal
		End Get
		Set(ByVal value As Boolean)
			m_FairMarketAppeal = value
		End Set
	End Property

	Public  Property TransientOccupant As Boolean
		Get
			Return m_TransientOccupant
		End Get
		Set(ByVal value As Boolean)
			m_TransientOccupant = value
		End Set
	End Property

	Public  Property OwnerOccupied As Boolean
		Get
			Return m_OwnerOccupied
		End Get
		Set(ByVal value As Boolean)
			m_OwnerOccupied = value
		End Set
	End Property

	Public  Property CommercialProfessional As Boolean
		Get
			Return m_CommercialProfessional
		End Get
		Set(ByVal value As Boolean)
			m_CommercialProfessional = value
		End Set
	End Property

	Public  Property NotPrimeResidence As Boolean
		Get
			Return m_NotPrimeResidence
		End Get
		Set(ByVal value As Boolean)
			m_NotPrimeResidence = value
		End Set
	End Property

	Public  Property TempExemptOther As Boolean
		Get
			Return m_TempExemptOther
		End Get
		Set(ByVal value As Boolean)
			m_TempExemptOther = value
		End Set
	End Property

	Public  Property ExemptDate As DateTime
		Get
			Return m_ExemptDate
		End Get
		Set(ByVal value As DateTime)
			m_ExemptDate = value
		End Set
	End Property

	Public  Property ExemptHighRentVacancy As Boolean
		Get
			Return m_ExemptHighRentVacancy
		End Get
		Set(ByVal value As Boolean)
			m_ExemptHighRentVacancy = value
		End Set
	End Property

	Public  Property ExemptLastLegalPaid As Decimal
		Get
			Return m_ExemptLastLegalPaid
		End Get
		Set(ByVal value As Decimal)
			m_ExemptLastLegalPaid = value
		End Set
	End Property

	Public  Property ExemptHighRentVacanyPeriod As Int32
		Get
			Return m_ExemptHighRentVacanyPeriod
		End Get
		Set(ByVal value As Int32)
			m_ExemptHighRentVacanyPeriod = value
		End Set
	End Property

	Public  Property ExemptHighRentIncome As Boolean
		Get
			Return m_ExemptHighRentIncome
		End Get
		Set(ByVal value As Boolean)
			m_ExemptHighRentIncome = value
		End Set
	End Property

	Public  Property ExemptCommercialProfessionalCo As Boolean
		Get
			Return m_ExemptCommercialProfessionalCo
		End Get
		Set(ByVal value As Boolean)
			m_ExemptCommercialProfessionalCo = value
		End Set
	End Property

	Public  Property ExemptSubstantialRehab As Boolean
		Get
			Return m_ExemptSubstantialRehab
		End Get
		Set(ByVal value As Boolean)
			m_ExemptSubstantialRehab = value
		End Set
	End Property

	Public  Property ExemptCoopCondoOwner As Boolean
		Get
			Return m_ExemptCoopCondoOwner
		End Get
		Set(ByVal value As Boolean)
			m_ExemptCoopCondoOwner = value
		End Set
	End Property

	Public  Property ExemptSec11243 As Boolean
		Get
			Return m_ExemptSec11243
		End Get
		Set(ByVal value As Boolean)
			m_ExemptSec11243 = value
		End Set
	End Property

	Public  Property ExemptOther As Boolean
		Get
			Return m_ExemptOther
		End Get
		Set(ByVal value As Boolean)
			m_ExemptOther = value
		End Set
	End Property

	Public  Property ExemptOtherDesc As String
		Get
			Return m_ExemptOtherDesc
		End Get
		Set(ByVal value As String)
			m_ExemptOtherDesc = value
		End Set
	End Property

	Public  Property ExemptExpSec608 As Boolean
		Get
			Return m_ExemptExpSec608
		End Get
		Set(ByVal value As Boolean)
			m_ExemptExpSec608 = value
		End Set
	End Property

	Public  Property ExemptExp421a As Boolean
		Get
			Return m_ExemptExp421a
		End Get
		Set(ByVal value As Boolean)
			m_ExemptExp421a = value
		End Set
	End Property

	Public  Property Exempt421aMarketRate As Boolean
		Get
			Return m_Exempt421aMarketRate
		End Get
		Set(ByVal value As Boolean)
			m_Exempt421aMarketRate = value
		End Set
	End Property

	Public  Property Exempt421aIncome As Boolean
		Get
			Return m_Exempt421aIncome
		End Get
		Set(ByVal value As Boolean)
			m_Exempt421aIncome = value
		End Set
	End Property

	Public  Property Exempt421aMedianIncome As Int32
		Get
			Return m_Exempt421aMedianIncome
		End Get
		Set(ByVal value As Int32)
			m_Exempt421aMedianIncome = value
		End Set
	End Property

End Class
