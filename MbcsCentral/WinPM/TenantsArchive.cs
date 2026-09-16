using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
namespace MbcsCentral.WinPM
{
    public class TenantsArchive : clsDB,INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		static string TABLE = "pm_TenantsArchive";
        public const string SQL_COL_CLIENT_ID = "clientId";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
		public const string SQL_COL_OLD_HISTORY_FILE_POINTER = "Old History File Pointer";
		public const string SQL_COL_NEW_TENANT_FLAG = "New Tenant Flag";
		public const string SQL_COL_REPLACING_TENANT = "Replacing Tenant";
		public const string SQL_COL_TENANT_NAME = "Tenant Name";
		public const string SQL_COL_APARTMENT = "Apartment";
		public const string SQL_COL_DUE_DATE = "Due Date";
		public const string SQL_COL_TYPE = "Type";
		public const string SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG = "Alternate Billing Address Flag";
		public const string SQL_COL_ADDITIONAL_NAME_FLAG = "Additional Name Flag";
		public const string SQL_COL_LEASE_LETTER_FLAG = "Lease Letter Flag";
		public const string SQL_COL_LOW_RENT_FLAG = "Low Rent Flag";
		public const string SQL_COL_LEGAL_CODE = "Legal Code";
		public const string SQL_COL_LEGAL_CODE_2 = "Legal Code 2";
		public const string SQL_COL_RENT = "Rent";
		public const string SQL_COL_SECURITY = "Security";
		public const string SQL_COL_ARREARS = "Arrears";
		public const string SQL_COL_PAID = "Paid";
		public const string SQL_COL_FUTURE_RENT_1 = "Future Rent 1";
		public const string SQL_COL_FUTURE_RENT_2 = "Future Rent 2";
		public const string SQL_COL_FUTURE_RENT_3 = "Future Rent 3";
		public const string SQL_COL_M_B_R = "MBR";
		public const string SQL_COL_FUTURE_DATE_1 = "Future Date 1";
		public const string SQL_COL_FUTURE_DATE_2 = "Future Date 2";
		public const string SQL_COL_FUTURE_DATE_3 = "Future Date 3";
		public const string SQL_COL_ROOMS = "Rooms";
		public const string SQL_COL_SQUARE_FEET = "Square Feet";
		public const string SQL_COL_MOVE_IN_DATE = "Move-in Date";
		public const string SQL_COL_LEASE_START_DATE = "Lease Start Date";
		public const string SQL_COL_LEASE_EXPIRATION_DATE = "Lease Expiration Date";
		public const string SQL_COL_LAST_PAID_DATE = "Last Paid Date";
		public const string SQL_COL_ADDITIONAL_NAME = "Additional Name";
		public const string SQL_COL_S_S_NUMBER1 = "SSNumber1";
		public const string SQL_COL_S_S_NUMBER2 = "SSNumber2";
		public const string SQL_COL_HOME_PHONE1 = "HomePhone1";
		public const string SQL_COL_HOME_PHONE2 = "HomePhone2";
		public const string SQL_COL_BUS_PHONE1 = "BusPhone1";
		public const string SQL_COL_BUS_PHONE2 = "BusPhone2";
		public const string SQL_COL_ALT_BILL_NAME = "AltBillName";
		public const string SQL_COL_ALT_BILL_ADD1 = "AltBillAdd1";
		public const string SQL_COL_ALT_BILL_ADD2 = "AltBillAdd2";
		public const string SQL_COL_ALT_BILL_ADD3 = "AltBillAdd3";
		public const string SQL_COL_ADD_NAME = "AddName";
		public const string SQL_COL_BANK = "Bank";
		public const string SQL_COL_INTEREST_INFO = "InterestInfo";
		public const string SQL_COL_SHARES = "Shares";
		public const string SQL_COL_COMMENT = "Comment";
		public const string SQL_COL_SENIORCIT = "Seniorcit";
		public const string SQL_COL_TOTAL_CHARGES = "Total Charges";
		public const string SQL_COL_PAGER = "Pager";
		public const string SQL_COL_DHCR_DATE_ONE = "dhcrDateOne";
		public const string SQL_COL_DHCR_RENT_ONE = "dhcrRentOne";
		public const string SQL_COL_DHCR_STOVE_ONE = "dhcrStoveOne";
		public const string SQL_COL_DHCR_REFRIGERATOR_ONE = "dhcrRefrigeratorOne";
		public const string SQL_COL_DHCR_DISHWASHER_ONE = "dhcrDishwasherOne";
		public const string SQL_COL_DHCR_A_C_ONE = "dhcrACOne";
		public const string SQL_COL_DHCR_WINDOWS_ONE = "dhcrWindowsOne";
		public const string SQL_COL_DHCR_OTHER_ONE = "dhcrOtherOne";
		public const string SQL_COL_DHCR_OTHER_TEXT_ONE = "dhcrOtherTextOne";
		public const string SQL_COL_DHCR_DATE_TWO = "dhcrDateTwo";
		public const string SQL_COL_DHCR_RENT_TWO = "dhcrRentTwo";
		public const string SQL_COL_DHCR_STOVE_TWO = "dhcrStoveTwo";
		public const string SQL_COL_DHCR_REFRIGERATOR_TWO = "dhcrRefrigeratorTwo";
		public const string SQL_COL_DHCR_DISHWASHER_TWO = "dhcrDishwasherTwo";
		public const string SQL_COL_DHCR_A_C_TWO = "dhcrACTwo";
		public const string SQL_COL_DHCR_WINDOWS_TWO = "dhcrWindowsTwo";
		public const string SQL_COL_DHCR_OTHER_TWO = "dhcrOtherTwo";
		public const string SQL_COL_DHCR_OTHER_TEXT_TWO = "dhcrOtherTextTwo";
		public const string SQL_COL_LEGAL_FEE = "LegalFee";
		public const string SQL_COL_LATE_FEE = "LateFee";
		public const string SQL_COL_LATE_BASE = "LateBase";
		public const string SQL_COL_LEGAL_FEE_NO = "LegalFeeNo";
		public const string SQL_COL_LATE_FEE_YES = "LateFeeYes";
		public const string SQL_COL_LATE_FEE_NO = "LateFeeNo";
		public const string SQL_COL_RENT_LEGAL = "RentLegal";
		public const string SQL_COL_FUTURE_LEGAL_1 = "Future Legal 1";
		public const string SQL_COL_FUTURE_LEGAL_2 = "Future Legal 2";
		public const string SQL_COL_FUTURE_LEGAL_3 = "Future Legal 3";
		public const string SQL_COL_PROPOSED_RENT = "ProposedRent";
		public const string SQL_COL_PROMPT_PAY1 = "PromptPay1";
		public const string SQL_COL_PROPOSED_RENT2 = "ProposedRent2";
		public const string SQL_COL_PROMPT_PAY2 = "PromptPay2";
		public const string SQL_COL_PROPOSED_LEGAL2 = "ProposedLegal2";
		public const string SQL_COL_PROPOSED_LEGAL = "ProposedLegal";
		public const string SQL_COL_CELL2 = "Cell2";
		public const string SQL_COL_SEC_MULTIPLE = "SecMultiple";
		public const string SQL_COL_EMAIL1 = "Email1";
		public const string SQL_COL_EMAIL2 = "Email2";
		public const string SQL_COL_TEMP1 = "Temp1";
		public const string SQL_COL_TEMP2 = "Temp2";
		public const string SQL_COL_SECURITY_ACCOUNT = "SecurityAccount";
		public const string SQL_COL_OPT_TENANT1 = "OptTenant1";
		public const string SQL_COL_OPT_TENANT2 = "OptTenant2";
		public const string SQL_COL_SECURITY_BUILDING = "SecurityBuilding";
		public const string SQL_COL_SUCCEEDED = "Succeeded";
		public const string SQL_COL_SECOND_SUCCESSION = "SecondSuccession";
		public const string SQL_COL_421A = "421a";
		public const string SQL_COL_RENT_OVERCHARGE = "RentOvercharge";
		public const string SQL_COL_FAIR_MARKET_APPEAL = "FairMarketAppeal";
		public const string SQL_COL_TRANSIENT_OCCUPANT = "TransientOccupant";
		public const string SQL_COL_OWNER_OCCUPIED = "OwnerOccupied";
		public const string SQL_COL_COMMERCIAL_PROFESSIONAL = "CommercialProfessional";
		public const string SQL_COL_NOT_PRIME_RESIDENCE = "NotPrimeResidence";
		public const string SQL_COL_TEMP_EXEMPT_OTHER = "TempExemptOther";
		public const string SQL_COL_EXEMPT_DATE = "ExemptDate";
		public const string SQL_COL_EXEMPT_HIGH_RENT_VACANCY = "ExemptHighRentVacancy";
		public const string SQL_COL_EXEMPT_LAST_LEGAL_PAID = "ExemptLastLegalPaid";
		public const string SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD = "ExemptHighRentVacanyPeriod";
		public const string SQL_COL_EXEMPT_HIGH_RENT_INCOME = "ExemptHighRentIncome";
		public const string SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO = "ExemptCommercialProfessionalCo";
		public const string SQL_COL_EXEMPT_SUBSTANTIAL_REHAB = "ExemptSubstantialRehab";
		public const string SQL_COL_EXEMPT_COOP_CONDO_OWNER = "ExemptCoopCondoOwner";
		public const string SQL_COL_EXEMPT_SEC11243 = "ExemptSec11243";
		public const string SQL_COL_EXEMPT_OTHER = "ExemptOther";
		public const string SQL_COL_EXEMPT_OTHER_DESC = "ExemptOtherDesc";
		public const string SQL_COL_EXEMPT_EXP_SEC608 = "ExemptExpSec608";
		public const string SQL_COL_EXEMPT_EXP421A = "ExemptExp421a";
		public const string SQL_COL_EXEMPT421A_MARKET_RATE = "Exempt421aMarketRate";
		public const string SQL_COL_EXEMPT421A_INCOME = "Exempt421aIncome";
		public const string SQL_COL_EXEMPT421A_MEDIAN_INCOME = "Exempt421aMedianIncome";
        public const string SQL_COL_PROPOSED_RENT2_1 = "ProposedRent2_1";

        static string SQL_LOAD = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," + "[" + SQL_COL_NEW_TENANT_FLAG + "]," + "[" + SQL_COL_REPLACING_TENANT + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]," + "[" + SQL_COL_DUE_DATE + "]," + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," + "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," + "[" + SQL_COL_LEASE_LETTER_FLAG + "]," + "[" + SQL_COL_LOW_RENT_FLAG + "]," + "[" + SQL_COL_LEGAL_CODE + "]," + "[" + SQL_COL_LEGAL_CODE_2 + "]," + "[" + SQL_COL_RENT + "]," + "[" + SQL_COL_SECURITY + "]," + "[" + SQL_COL_ARREARS + "]," + "[" + SQL_COL_PAID + "]," + "[" + SQL_COL_FUTURE_RENT_1 + "]," + "[" + SQL_COL_FUTURE_RENT_2 + "]," + "[" + SQL_COL_FUTURE_RENT_3 + "]," + "[" + SQL_COL_M_B_R + "]," + "[" + SQL_COL_FUTURE_DATE_1 + "]," + "[" + SQL_COL_FUTURE_DATE_2 + "]," + "[" + SQL_COL_FUTURE_DATE_3 + "]," + "[" + SQL_COL_ROOMS + "]," + "[" + SQL_COL_SQUARE_FEET + "]," + "[" + SQL_COL_MOVE_IN_DATE + "]," + "[" + SQL_COL_LEASE_START_DATE + "]," + "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," + "[" + SQL_COL_LAST_PAID_DATE + "]," + "[" + SQL_COL_ADDITIONAL_NAME + "]," + "[" + SQL_COL_S_S_NUMBER1 + "]," + "[" + SQL_COL_S_S_NUMBER2 + "]," + "[" + SQL_COL_HOME_PHONE1 + "]," + "[" + SQL_COL_HOME_PHONE2 + "]," + "[" + SQL_COL_BUS_PHONE1 + "]," + "[" + SQL_COL_BUS_PHONE2 + "]," + "[" + SQL_COL_ALT_BILL_NAME + "]," + "[" + SQL_COL_ALT_BILL_ADD1 + "]," + "[" + SQL_COL_ALT_BILL_ADD2 + "]," + "[" + SQL_COL_ALT_BILL_ADD3 + "]," + "[" + SQL_COL_ADD_NAME + "]," + "[" + SQL_COL_BANK + "]," + "[" + SQL_COL_INTEREST_INFO + "]," + "[" + SQL_COL_SHARES + "]," + "[" + SQL_COL_COMMENT + "]," + "[" + SQL_COL_SENIORCIT + "]," + "[" + SQL_COL_TOTAL_CHARGES + "]," + "[" + SQL_COL_PAGER + "]," + "[" + SQL_COL_DHCR_DATE_ONE + "]," + "[" + SQL_COL_DHCR_RENT_ONE + "]," + "[" + SQL_COL_DHCR_STOVE_ONE + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," + "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," + "[" + SQL_COL_DHCR_A_C_ONE + "]," + "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," + "[" + SQL_COL_DHCR_DATE_TWO + "]," + "[" + SQL_COL_DHCR_RENT_TWO + "]," + "[" + SQL_COL_DHCR_STOVE_TWO + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," + "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," + "[" + SQL_COL_DHCR_A_C_TWO + "]," + "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," + "[" + SQL_COL_LEGAL_FEE + "]," + "[" + SQL_COL_LATE_FEE + "]," + "[" + SQL_COL_LATE_BASE + "]," + "[" + SQL_COL_LEGAL_FEE_NO + "]," + "[" + SQL_COL_LATE_FEE_YES + "]," + "[" + SQL_COL_LATE_FEE_NO + "]," + "[" + SQL_COL_RENT_LEGAL + "]," + "[" + SQL_COL_FUTURE_LEGAL_1 + "]," + "[" + SQL_COL_FUTURE_LEGAL_2 + "]," + "[" + SQL_COL_FUTURE_LEGAL_3 + "]," + "[" + SQL_COL_PROPOSED_RENT + "]," + "[" + SQL_COL_PROMPT_PAY1 + "]," + "[" + SQL_COL_PROPOSED_RENT2 + "]," + "[" + SQL_COL_PROMPT_PAY2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL + "]," + "[" + SQL_COL_CELL2 + "]," + "[" + SQL_COL_SEC_MULTIPLE + "]," + "[" + SQL_COL_EMAIL1 + "]," + "[" + SQL_COL_EMAIL2 + "]," + "[" + SQL_COL_TEMP1 + "]," + "[" + SQL_COL_TEMP2 + "]," + "[" + SQL_COL_SECURITY_ACCOUNT + "]," + "[" + SQL_COL_OPT_TENANT1 + "]," + "[" + SQL_COL_OPT_TENANT2 + "]," + "[" + SQL_COL_SECURITY_BUILDING + "]," + "[" + SQL_COL_SUCCEEDED + "]," + "[" + SQL_COL_SECOND_SUCCESSION + "]," + "[" + SQL_COL_421A + "]," + "[" + SQL_COL_RENT_OVERCHARGE + "]," + "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," + "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," + "[" + SQL_COL_OWNER_OCCUPIED + "]," + "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," + "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," + "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_DATE + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," + "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," + "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," + "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," + "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," + "[" + SQL_COL_EXEMPT_SEC11243 + "]," + "[" + SQL_COL_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," + "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," + "[" + SQL_COL_EXEMPT_EXP421A + "]," + "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," + "[" + SQL_COL_EXEMPT421A_INCOME + "]," + "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?' AND [" + SQL_COL_TENANT_NUMBER + "] = '?'";

		static string SQL_LOAD_FOR_NAMES = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]" + " FROM " + TABLE + " ORDER BY [" + SQL_COL_TENANT_NAME + "]";

		static string SQL_LOAD_FOR_DELETION = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'";

		static string SQL_LOAD_BY_SEQUENCE_NUMBER = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," + "[" + SQL_COL_NEW_TENANT_FLAG + "]," + "[" + SQL_COL_REPLACING_TENANT + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]," + "[" + SQL_COL_DUE_DATE + "]," + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," + "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," + "[" + SQL_COL_LEASE_LETTER_FLAG + "]," + "[" + SQL_COL_LOW_RENT_FLAG + "]," + "[" + SQL_COL_LEGAL_CODE + "]," + "[" + SQL_COL_LEGAL_CODE_2 + "]," + "[" + SQL_COL_RENT + "]," + "[" + SQL_COL_SECURITY + "]," + "[" + SQL_COL_ARREARS + "]," + "[" + SQL_COL_PAID + "]," + "[" + SQL_COL_FUTURE_RENT_1 + "]," + "[" + SQL_COL_FUTURE_RENT_2 + "]," + "[" + SQL_COL_FUTURE_RENT_3 + "]," + "[" + SQL_COL_M_B_R + "]," + "[" + SQL_COL_FUTURE_DATE_1 + "]," + "[" + SQL_COL_FUTURE_DATE_2 + "]," + "[" + SQL_COL_FUTURE_DATE_3 + "]," + "[" + SQL_COL_ROOMS + "]," + "[" + SQL_COL_SQUARE_FEET + "]," + "[" + SQL_COL_MOVE_IN_DATE + "]," + "[" + SQL_COL_LEASE_START_DATE + "]," + "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," + "[" + SQL_COL_LAST_PAID_DATE + "]," + "[" + SQL_COL_ADDITIONAL_NAME + "]," + "[" + SQL_COL_S_S_NUMBER1 + "]," + "[" + SQL_COL_S_S_NUMBER2 + "]," + "[" + SQL_COL_HOME_PHONE1 + "]," + "[" + SQL_COL_HOME_PHONE2 + "]," + "[" + SQL_COL_BUS_PHONE1 + "]," + "[" + SQL_COL_BUS_PHONE2 + "]," + "[" + SQL_COL_ALT_BILL_NAME + "]," + "[" + SQL_COL_ALT_BILL_ADD1 + "]," + "[" + SQL_COL_ALT_BILL_ADD2 + "]," + "[" + SQL_COL_ALT_BILL_ADD3 + "]," + "[" + SQL_COL_ADD_NAME + "]," + "[" + SQL_COL_BANK + "]," + "[" + SQL_COL_INTEREST_INFO + "]," + "[" + SQL_COL_SHARES + "]," + "[" + SQL_COL_COMMENT + "]," + "[" + SQL_COL_SENIORCIT + "]," + "[" + SQL_COL_TOTAL_CHARGES + "]," + "[" + SQL_COL_PAGER + "]," + "[" + SQL_COL_DHCR_DATE_ONE + "]," + "[" + SQL_COL_DHCR_RENT_ONE + "]," + "[" + SQL_COL_DHCR_STOVE_ONE + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," + "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," + "[" + SQL_COL_DHCR_A_C_ONE + "]," + "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," + "[" + SQL_COL_DHCR_DATE_TWO + "]," + "[" + SQL_COL_DHCR_RENT_TWO + "]," + "[" + SQL_COL_DHCR_STOVE_TWO + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," + "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," + "[" + SQL_COL_DHCR_A_C_TWO + "]," + "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," + "[" + SQL_COL_LEGAL_FEE + "]," + "[" + SQL_COL_LATE_FEE + "]," + "[" + SQL_COL_LATE_BASE + "]," + "[" + SQL_COL_LEGAL_FEE_NO + "]," + "[" + SQL_COL_LATE_FEE_YES + "]," + "[" + SQL_COL_LATE_FEE_NO + "]," + "[" + SQL_COL_RENT_LEGAL + "]," + "[" + SQL_COL_FUTURE_LEGAL_1 + "]," + "[" + SQL_COL_FUTURE_LEGAL_2 + "]," + "[" + SQL_COL_FUTURE_LEGAL_3 + "]," + "[" + SQL_COL_PROPOSED_RENT + "]," + "[" + SQL_COL_PROMPT_PAY1 + "]," + "[" + SQL_COL_PROPOSED_RENT2 + "]," + "[" + SQL_COL_PROMPT_PAY2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL + "]," + "[" + SQL_COL_CELL2 + "]," + "[" + SQL_COL_SEC_MULTIPLE + "]," + "[" + SQL_COL_EMAIL1 + "]," + "[" + SQL_COL_EMAIL2 + "]," + "[" + SQL_COL_TEMP1 + "]," + "[" + SQL_COL_TEMP2 + "]," + "[" + SQL_COL_SECURITY_ACCOUNT + "]," + "[" + SQL_COL_OPT_TENANT1 + "]," + "[" + SQL_COL_OPT_TENANT2 + "]," + "[" + SQL_COL_SECURITY_BUILDING + "]," + "[" + SQL_COL_SUCCEEDED + "]," + "[" + SQL_COL_SECOND_SUCCESSION + "]," + "[" + SQL_COL_421A + "]," + "[" + SQL_COL_RENT_OVERCHARGE + "]," + "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," + "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," + "[" + SQL_COL_OWNER_OCCUPIED + "]," + "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," + "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," + "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_DATE + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," + "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," + "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," + "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," + "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," + "[" + SQL_COL_EXEMPT_SEC11243 + "]," + "[" + SQL_COL_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," + "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," + "[" + SQL_COL_EXEMPT_EXP421A + "]," + "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," + "[" + SQL_COL_EXEMPT421A_INCOME + "]," + "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]" + " FROM " + TABLE + " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?";

		static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," + "[" + SQL_COL_NEW_TENANT_FLAG + "]," + "[" + SQL_COL_REPLACING_TENANT + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]," + "[" + SQL_COL_DUE_DATE + "]," + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," + "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," + "[" + SQL_COL_LEASE_LETTER_FLAG + "]," + "[" + SQL_COL_LOW_RENT_FLAG + "]," + "[" + SQL_COL_LEGAL_CODE + "]," + "[" + SQL_COL_LEGAL_CODE_2 + "]," + "[" + SQL_COL_RENT + "]," + "[" + SQL_COL_SECURITY + "]," + "[" + SQL_COL_ARREARS + "]," + "[" + SQL_COL_PAID + "]," + "[" + SQL_COL_FUTURE_RENT_1 + "]," + "[" + SQL_COL_FUTURE_RENT_2 + "]," + "[" + SQL_COL_FUTURE_RENT_3 + "]," + "[" + SQL_COL_M_B_R + "]," + "[" + SQL_COL_FUTURE_DATE_1 + "]," + "[" + SQL_COL_FUTURE_DATE_2 + "]," + "[" + SQL_COL_FUTURE_DATE_3 + "]," + "[" + SQL_COL_ROOMS + "]," + "[" + SQL_COL_SQUARE_FEET + "]," + "[" + SQL_COL_MOVE_IN_DATE + "]," + "[" + SQL_COL_LEASE_START_DATE + "]," + "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," + "[" + SQL_COL_LAST_PAID_DATE + "]," + "[" + SQL_COL_ADDITIONAL_NAME + "]," + "[" + SQL_COL_S_S_NUMBER1 + "]," + "[" + SQL_COL_S_S_NUMBER2 + "]," + "[" + SQL_COL_HOME_PHONE1 + "]," + "[" + SQL_COL_HOME_PHONE2 + "]," + "[" + SQL_COL_BUS_PHONE1 + "]," + "[" + SQL_COL_BUS_PHONE2 + "]," + "[" + SQL_COL_ALT_BILL_NAME + "]," + "[" + SQL_COL_ALT_BILL_ADD1 + "]," + "[" + SQL_COL_ALT_BILL_ADD2 + "]," + "[" + SQL_COL_ALT_BILL_ADD3 + "]," + "[" + SQL_COL_ADD_NAME + "]," + "[" + SQL_COL_BANK + "]," + "[" + SQL_COL_INTEREST_INFO + "]," + "[" + SQL_COL_SHARES + "]," + "[" + SQL_COL_COMMENT + "]," + "[" + SQL_COL_SENIORCIT + "]," + "[" + SQL_COL_TOTAL_CHARGES + "]," + "[" + SQL_COL_PAGER + "]," + "[" + SQL_COL_DHCR_DATE_ONE + "]," + "[" + SQL_COL_DHCR_RENT_ONE + "]," + "[" + SQL_COL_DHCR_STOVE_ONE + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," + "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," + "[" + SQL_COL_DHCR_A_C_ONE + "]," + "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," + "[" + SQL_COL_DHCR_DATE_TWO + "]," + "[" + SQL_COL_DHCR_RENT_TWO + "]," + "[" + SQL_COL_DHCR_STOVE_TWO + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," + "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," + "[" + SQL_COL_DHCR_A_C_TWO + "]," + "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," + "[" + SQL_COL_LEGAL_FEE + "]," + "[" + SQL_COL_LATE_FEE + "]," + "[" + SQL_COL_LATE_BASE + "]," + "[" + SQL_COL_LEGAL_FEE_NO + "]," + "[" + SQL_COL_LATE_FEE_YES + "]," + "[" + SQL_COL_LATE_FEE_NO + "]," + "[" + SQL_COL_RENT_LEGAL + "]," + "[" + SQL_COL_FUTURE_LEGAL_1 + "]," + "[" + SQL_COL_FUTURE_LEGAL_2 + "]," + "[" + SQL_COL_FUTURE_LEGAL_3 + "]," + "[" + SQL_COL_PROPOSED_RENT + "]," + "[" + SQL_COL_PROMPT_PAY1 + "]," + "[" + SQL_COL_PROPOSED_RENT2 + "]," + "[" + SQL_COL_PROMPT_PAY2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL + "]," + "[" + SQL_COL_CELL2 + "]," + "[" + SQL_COL_SEC_MULTIPLE + "]," + "[" + SQL_COL_EMAIL1 + "]," + "[" + SQL_COL_EMAIL2 + "]," + "[" + SQL_COL_TEMP1 + "]," + "[" + SQL_COL_TEMP2 + "]," + "[" + SQL_COL_SECURITY_ACCOUNT + "]," + "[" + SQL_COL_OPT_TENANT1 + "]," + "[" + SQL_COL_OPT_TENANT2 + "]," + "[" + SQL_COL_SECURITY_BUILDING + "]," + "[" + SQL_COL_SUCCEEDED + "]," + "[" + SQL_COL_SECOND_SUCCESSION + "]," + "[" + SQL_COL_421A + "]," + "[" + SQL_COL_RENT_OVERCHARGE + "]," + "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," + "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," + "[" + SQL_COL_OWNER_OCCUPIED + "]," + "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," + "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," + "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_DATE + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," + "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," + "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," + "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," + "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," + "[" + SQL_COL_EXEMPT_SEC11243 + "]," + "[" + SQL_COL_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," + "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," + "[" + SQL_COL_EXEMPT_EXP421A + "]," + "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," + "[" + SQL_COL_EXEMPT421A_INCOME + "]," + "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]" + 
                                     " FROM " + TABLE + 
                                     " WHERE " + SQL_COL_CLIENT_ID + "=?" +         
                                     " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]" ;

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_SEQUENCE_NUMBER + "]," + "[" + SQL_COL_BUILDING_NUMBER + "]," + "[" + SQL_COL_TENANT_NUMBER + "]," + "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," + "[" + SQL_COL_NEW_TENANT_FLAG + "]," + "[" + SQL_COL_REPLACING_TENANT + "]," + "[" + SQL_COL_TENANT_NAME + "]," + "[" + SQL_COL_APARTMENT + "]," + "[" + SQL_COL_DUE_DATE + "]," + "[" + SQL_COL_TYPE + "]," + "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," + "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," + "[" + SQL_COL_LEASE_LETTER_FLAG + "]," + "[" + SQL_COL_LOW_RENT_FLAG + "]," + "[" + SQL_COL_LEGAL_CODE + "]," + "[" + SQL_COL_LEGAL_CODE_2 + "]," + "[" + SQL_COL_RENT + "]," + "[" + SQL_COL_SECURITY + "]," + "[" + SQL_COL_ARREARS + "]," + "[" + SQL_COL_PAID + "]," + "[" + SQL_COL_FUTURE_RENT_1 + "]," + "[" + SQL_COL_FUTURE_RENT_2 + "]," + "[" + SQL_COL_FUTURE_RENT_3 + "]," + "[" + SQL_COL_M_B_R + "]," + "[" + SQL_COL_FUTURE_DATE_1 + "]," + "[" + SQL_COL_FUTURE_DATE_2 + "]," + "[" + SQL_COL_FUTURE_DATE_3 + "]," + "[" + SQL_COL_ROOMS + "]," + "[" + SQL_COL_SQUARE_FEET + "]," + "[" + SQL_COL_MOVE_IN_DATE + "]," + "[" + SQL_COL_LEASE_START_DATE + "]," + "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," + "[" + SQL_COL_LAST_PAID_DATE + "]," + "[" + SQL_COL_ADDITIONAL_NAME + "]," + "[" + SQL_COL_S_S_NUMBER1 + "]," + "[" + SQL_COL_S_S_NUMBER2 + "]," + "[" + SQL_COL_HOME_PHONE1 + "]," + "[" + SQL_COL_HOME_PHONE2 + "]," + "[" + SQL_COL_BUS_PHONE1 + "]," + "[" + SQL_COL_BUS_PHONE2 + "]," + "[" + SQL_COL_ALT_BILL_NAME + "]," + "[" + SQL_COL_ALT_BILL_ADD1 + "]," + "[" + SQL_COL_ALT_BILL_ADD2 + "]," + "[" + SQL_COL_ALT_BILL_ADD3 + "]," + "[" + SQL_COL_ADD_NAME + "]," + "[" + SQL_COL_BANK + "]," + "[" + SQL_COL_INTEREST_INFO + "]," + "[" + SQL_COL_SHARES + "]," + "[" + SQL_COL_COMMENT + "]," + "[" + SQL_COL_SENIORCIT + "]," + "[" + SQL_COL_TOTAL_CHARGES + "]," + "[" + SQL_COL_PAGER + "]," + "[" + SQL_COL_DHCR_DATE_ONE + "]," + "[" + SQL_COL_DHCR_RENT_ONE + "]," + "[" + SQL_COL_DHCR_STOVE_ONE + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," + "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," + "[" + SQL_COL_DHCR_A_C_ONE + "]," + "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_ONE + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," + "[" + SQL_COL_DHCR_DATE_TWO + "]," + "[" + SQL_COL_DHCR_RENT_TWO + "]," + "[" + SQL_COL_DHCR_STOVE_TWO + "]," + "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," + "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," + "[" + SQL_COL_DHCR_A_C_TWO + "]," + "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TWO + "]," + "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," + "[" + SQL_COL_LEGAL_FEE + "]," + "[" + SQL_COL_LATE_FEE + "]," + "[" + SQL_COL_LATE_BASE + "]," + "[" + SQL_COL_LEGAL_FEE_NO + "]," + "[" + SQL_COL_LATE_FEE_YES + "]," + "[" + SQL_COL_LATE_FEE_NO + "]," + "[" + SQL_COL_RENT_LEGAL + "]," + "[" + SQL_COL_FUTURE_LEGAL_1 + "]," + "[" + SQL_COL_FUTURE_LEGAL_2 + "]," + "[" + SQL_COL_FUTURE_LEGAL_3 + "]," + "[" + SQL_COL_PROPOSED_RENT + "]," + "[" + SQL_COL_PROMPT_PAY1 + "]," + "[" + SQL_COL_PROPOSED_RENT2 + "]," + "[" + SQL_COL_PROMPT_PAY2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL2 + "]," + "[" + SQL_COL_PROPOSED_LEGAL + "]," + "[" + SQL_COL_CELL2 + "]," + "[" + SQL_COL_SEC_MULTIPLE + "]," + "[" + SQL_COL_EMAIL1 + "]," + "[" + SQL_COL_EMAIL2 + "]," + "[" + SQL_COL_TEMP1 + "]," + "[" + SQL_COL_TEMP2 + "]," + "[" + SQL_COL_SECURITY_ACCOUNT + "]," + "[" + SQL_COL_OPT_TENANT1 + "]," + "[" + SQL_COL_OPT_TENANT2 + "]," + "[" + SQL_COL_SECURITY_BUILDING + "]," + "[" + SQL_COL_SUCCEEDED + "]," + "[" + SQL_COL_SECOND_SUCCESSION + "]," + "[" + SQL_COL_421A + "]," + "[" + SQL_COL_RENT_OVERCHARGE + "]," + "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," + "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," + "[" + SQL_COL_OWNER_OCCUPIED + "]," + "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," + "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," + "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_DATE + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," + "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," + "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," + "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," + "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," + "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," + "[" + SQL_COL_EXEMPT_SEC11243 + "]," + "[" + SQL_COL_EXEMPT_OTHER + "]," + "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," + "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," + "[" + SQL_COL_EXEMPT_EXP421A + "]," + "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," + "[" + SQL_COL_EXEMPT421A_INCOME + "]," + "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]" + ") VALUES (?, '?', '?', ?, ?, ?, '?', '?', '?', '?', ?, ?, ?, ?, '?', '?', ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?', '?', ?, '?', '?', ?, ?, ?, ?, ?, ?, ?, '?', '?', ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, '?', '?', '?', '?', '?', ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', ?, ?, ?, ?, ?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_BUILDING_NUMBER + "]='?'," + "[" + SQL_COL_TENANT_NUMBER + "]='?'," + "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]=?," + "[" + SQL_COL_NEW_TENANT_FLAG + "]=?," + "[" + SQL_COL_REPLACING_TENANT + "]=?," + "[" + SQL_COL_TENANT_NAME + "]='?'," + "[" + SQL_COL_APARTMENT + "]='?'," + "[" + SQL_COL_DUE_DATE + "]='?'," + "[" + SQL_COL_TYPE + "]='?'," + "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]=?," + "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]=?," + "[" + SQL_COL_LEASE_LETTER_FLAG + "]=?," + "[" + SQL_COL_LOW_RENT_FLAG + "]=?," + "[" + SQL_COL_LEGAL_CODE + "]='?'," + "[" + SQL_COL_LEGAL_CODE_2 + "]='?'," + "[" + SQL_COL_RENT + "]=?," + "[" + SQL_COL_SECURITY + "]=?," + "[" + SQL_COL_ARREARS + "]=?," + "[" + SQL_COL_PAID + "]=?," + "[" + SQL_COL_FUTURE_RENT_1 + "]=?," + "[" + SQL_COL_FUTURE_RENT_2 + "]=?," + "[" + SQL_COL_FUTURE_RENT_3 + "]=?," + "[" + SQL_COL_M_B_R + "]=?," + "[" + SQL_COL_FUTURE_DATE_1 + "]='?'," + "[" + SQL_COL_FUTURE_DATE_2 + "]='?'," + "[" + SQL_COL_FUTURE_DATE_3 + "]='?'," + "[" + SQL_COL_ROOMS + "]=?," + "[" + SQL_COL_SQUARE_FEET + "]=?," + "[" + SQL_COL_MOVE_IN_DATE + "]='?'," + "[" + SQL_COL_LEASE_START_DATE + "]='?'," + "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]='?'," + "[" + SQL_COL_LAST_PAID_DATE + "]='?'," + "[" + SQL_COL_ADDITIONAL_NAME + "]='?'," + "[" + SQL_COL_S_S_NUMBER1 + "]='?'," + "[" + SQL_COL_S_S_NUMBER2 + "]='?'," + "[" + SQL_COL_HOME_PHONE1 + "]='?'," + "[" + SQL_COL_HOME_PHONE2 + "]='?'," + "[" + SQL_COL_BUS_PHONE1 + "]='?'," + "[" + SQL_COL_BUS_PHONE2 + "]='?'," + "[" + SQL_COL_ALT_BILL_NAME + "]='?'," + "[" + SQL_COL_ALT_BILL_ADD1 + "]='?'," + "[" + SQL_COL_ALT_BILL_ADD2 + "]='?'," + "[" + SQL_COL_ALT_BILL_ADD3 + "]='?'," + "[" + SQL_COL_ADD_NAME + "]='?'," + "[" + SQL_COL_BANK + "]='?'," + "[" + SQL_COL_INTEREST_INFO + "]='?'," + "[" + SQL_COL_SHARES + "]=?," + "[" + SQL_COL_COMMENT + "]='?'," + "[" + SQL_COL_SENIORCIT + "]='?'," + "[" + SQL_COL_TOTAL_CHARGES + "]=?," + "[" + SQL_COL_PAGER + "]='?'," + "[" + SQL_COL_DHCR_DATE_ONE + "]='?'," + "[" + SQL_COL_DHCR_RENT_ONE + "]=?," + "[" + SQL_COL_DHCR_STOVE_ONE + "]=?," + "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]=?," + "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]=?," + "[" + SQL_COL_DHCR_A_C_ONE + "]=?," + "[" + SQL_COL_DHCR_WINDOWS_ONE + "]=?," + "[" + SQL_COL_DHCR_OTHER_ONE + "]=?," + "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]='?'," + "[" + SQL_COL_DHCR_DATE_TWO + "]='?'," + "[" + SQL_COL_DHCR_RENT_TWO + "]=?," + "[" + SQL_COL_DHCR_STOVE_TWO + "]=?," + "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]=?," + "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]=?," + "[" + SQL_COL_DHCR_A_C_TWO + "]=?," + "[" + SQL_COL_DHCR_WINDOWS_TWO + "]=?," + "[" + SQL_COL_DHCR_OTHER_TWO + "]=?," + "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]='?'," + "[" + SQL_COL_LEGAL_FEE + "]=?," + "[" + SQL_COL_LATE_FEE + "]=?," + "[" + SQL_COL_LATE_BASE + "]=?," + "[" + SQL_COL_LEGAL_FEE_NO + "]=?," + "[" + SQL_COL_LATE_FEE_YES + "]=?," + "[" + SQL_COL_LATE_FEE_NO + "]=?," + "[" + SQL_COL_RENT_LEGAL + "]=?," + "[" + SQL_COL_FUTURE_LEGAL_1 + "]=?," + "[" + SQL_COL_FUTURE_LEGAL_2 + "]=?," + "[" + SQL_COL_FUTURE_LEGAL_3 + "]=?," + "[" + SQL_COL_PROPOSED_RENT + "]=?," + "[" + SQL_COL_PROMPT_PAY1 + "]=?," + "[" + SQL_COL_PROPOSED_RENT2 + "]=?," + "[" + SQL_COL_PROMPT_PAY2 + "]=?," + "[" + SQL_COL_PROPOSED_LEGAL2 + "]=?," + "[" + SQL_COL_PROPOSED_LEGAL + "]=?," + "[" + SQL_COL_CELL2 + "]='?'," + "[" + SQL_COL_SEC_MULTIPLE + "]=?," + "[" + SQL_COL_EMAIL1 + "]='?'," + "[" + SQL_COL_EMAIL2 + "]='?'," + "[" + SQL_COL_TEMP1 + "]='?'," + "[" + SQL_COL_TEMP2 + "]='?'," + "[" + SQL_COL_SECURITY_ACCOUNT + "]='?'," + "[" + SQL_COL_OPT_TENANT1 + "]=?," + "[" + SQL_COL_OPT_TENANT2 + "]=?," + "[" + SQL_COL_SECURITY_BUILDING + "]='?'," + "[" + SQL_COL_SUCCEEDED + "]=?," + "[" + SQL_COL_SECOND_SUCCESSION + "]=?," + "[" + SQL_COL_421A + "]=?," + "[" + SQL_COL_RENT_OVERCHARGE + "]=?," + "[" + SQL_COL_FAIR_MARKET_APPEAL + "]=?," + "[" + SQL_COL_TRANSIENT_OCCUPANT + "]=?," + "[" + SQL_COL_OWNER_OCCUPIED + "]=?," + "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]=?," + "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]=?," + "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]=?," + "[" + SQL_COL_EXEMPT_DATE + "]='?'," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]=?," + "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]=?," + "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]=?," + "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]=?," + "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]=?," + "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]=?," + "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]=?," + "[" + SQL_COL_EXEMPT_SEC11243 + "]=?," + "[" + SQL_COL_EXEMPT_OTHER + "]=?," + "[" + SQL_COL_EXEMPT_OTHER_DESC + "]='?'," + "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]=?," + "[" + SQL_COL_EXEMPT_EXP421A + "]=?," + "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]=?," + "[" + SQL_COL_EXEMPT421A_INCOME + "]=?," + "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]=?" + " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?;";
     
        public enum orderByEnum : int
		{
			tenantName,
			buildingTenant,
			buildingApartment
		}
		public string[] orderBy = {
			"Tenant Name",
			"Building/Tenant Numbers",
			"Building/Apartment"
		};
		static string[] orderBySql = {
			" ORDER BY [" + SQL_COL_TENANT_NAME + "]",
			" ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]",
			" ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], " + SQL_COL_APARTMENT

		};
		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected int m_SequenceNumber;
		protected int m_OldHistoryFilePointer;
		protected bool m_NewTenantFlag;
		protected int m_ReplacingTenant;
		protected string m_TenantName;
		protected string m_Apartment;
		protected string m_DueDate;
		protected string m_Type;
		protected bool m_AlternateBillingAddressFlag;
		protected bool m_AdditionalNameFlag;
		protected bool m_LeaseLetterFlag;
		protected bool m_LowRentFlag;
		protected string m_LegalCode;
		protected string m_LegalCode2;
		protected decimal m_Rent;
		protected decimal m_Security;
		protected decimal m_Arrears;
		protected decimal m_Paid;
		protected decimal m_FutureRent1;
		protected decimal m_FutureRent2;
		protected decimal m_FutureRent3;
		protected decimal m_MBR;
		protected DateTime m_FutureDate1;
		protected DateTime m_FutureDate2;
		protected DateTime m_FutureDate3;
		protected float m_Rooms;
		protected float m_SquareFeet;
		protected DateTime m_MoveinDate;
		protected DateTime m_LeaseStartDate;
		protected DateTime m_LeaseExpirationDate;
		protected DateTime m_LastPaidDate;
		protected string m_AdditionalName;
		protected string m_SSNumber1;
		protected string m_SSNumber2;
		protected string m_HomePhone1;
		protected string m_HomePhone2;
		protected string m_BusPhone1;
		protected string m_BusPhone2;
		protected string m_AltBillName;
		protected string m_AltBillAdd1;
		protected string m_AltBillAdd2;
		protected string m_AltBillAdd3;
		protected string m_AddName;
		protected string m_Bank;
		protected string m_InterestInfo;
		protected float m_Shares;
		protected string m_Comment;
		protected string m_Seniorcit;
		protected decimal m_TotalCharges;
		protected string m_Pager;
		protected DateTime m_dhcrDateOne;
		protected decimal m_dhcrRentOne;
		protected bool m_dhcrStoveOne;
		protected bool m_dhcrRefrigeratorOne;
		protected bool m_dhcrDishwasherOne;
		protected bool m_dhcrACOne;
		protected bool m_dhcrWindowsOne;
		protected bool m_dhcrOtherOne;
		protected string m_dhcrOtherTextOne;
		protected DateTime m_dhcrDateTwo;
		protected decimal m_dhcrRentTwo;
		protected bool m_dhcrStoveTwo;
		protected bool m_dhcrRefrigeratorTwo;
		protected bool m_dhcrDishwasherTwo;
		protected bool m_dhcrACTwo;
		protected bool m_dhcrWindowsTwo;
		protected bool m_dhcrOtherTwo;
		protected string m_dhcrOtherTextTwo;
		protected bool m_LegalFee;
		protected decimal m_LateFee;
		protected decimal m_LateBase;
		protected bool m_LegalFeeNo;
		protected bool m_LateFeeYes;
		protected bool m_LateFeeNo;
		protected decimal m_RentLegal;
		protected decimal m_FutureLegal1;
		protected decimal m_FutureLegal2;
		protected decimal m_FutureLegal3;
		protected decimal m_ProposedRent;
		protected decimal m_PromptPay1;
		protected decimal m_ProposedRent2;
		protected decimal m_PromptPay2;
		protected decimal m_ProposedLegal2;
		protected decimal m_ProposedLegal;
		protected string m_Cell2;
		protected float m_SecMultiple;
		protected string m_Email1;
		protected string m_Email2;
		protected string m_Temp1;
		protected string m_Temp2;
		protected string m_SecurityAccount;
		protected Int16 m_OptTenant1;
		protected Int16 m_OptTenant2;
		protected string m_SecurityBuilding;
		protected bool m_Succeeded;
		protected bool m_SecondSuccession;
		protected bool m_421a;
		protected bool m_RentOvercharge;
		protected bool m_FairMarketAppeal;
		protected bool m_TransientOccupant;
		protected bool m_OwnerOccupied;
		protected bool m_CommercialProfessional;
		protected bool m_NotPrimeResidence;
		protected bool m_TempExemptOther;
		protected DateTime m_ExemptDate;
		protected bool m_ExemptHighRentVacancy;
		protected decimal m_ExemptLastLegalPaid;
		protected Int16 m_ExemptHighRentVacanyPeriod;
		protected bool m_ExemptHighRentIncome;
		protected bool m_ExemptCommercialProfessionalCo;
		protected bool m_ExemptSubstantialRehab;
		protected bool m_ExemptCoopCondoOwner;
		protected bool m_ExemptSec11243;
		protected bool m_ExemptOther;
		protected string m_ExemptOtherDesc;
		protected bool m_ExemptExpSec608;
		protected bool m_ExemptExp421a;
		protected bool m_Exempt421aMarketRate;
		protected bool m_Exempt421aIncome;
		protected Int16 m_Exempt421aMedianIncome;

		protected bool m_isChanged;
        protected bool m_NewOnly = false;

		public TenantsArchive() : base(TABLE, SQL_COL_SEQUENCE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public TenantsArchive(string BuildingNumber, string TenantNumber, int SequenceNumber, Int32 OldHistoryFilePointer, bool NewTenantFlag, int ReplacingTenant, string TenantName, string Apartment, string DueDate, string Type,
		bool AlternateBillingAddressFlag, bool AdditionalNameFlag, bool LeaseLetterFlag, bool LowRentFlag, string LegalCode, string LegalCode2, decimal Rent, decimal Security, decimal Arrears, decimal Paid,
		decimal FutureRent1, decimal FutureRent2, decimal FutureRent3, decimal MBR, DateTime FutureDate1, DateTime FutureDate2, DateTime FutureDate3, float Rooms, float SquareFeet, DateTime MoveinDate,
		DateTime LeaseStartDate, DateTime LeaseExpirationDate, DateTime LastPaidDate, string AdditionalName, string SSNumber1, string SSNumber2, string HomePhone1, string HomePhone2, string BusPhone1, string BusPhone2,
		string AltBillName, string AltBillAdd1, string AltBillAdd2, string AltBillAdd3, string AddName, string Bank, string InterestInfo, float Shares, string Comment, string Seniorcit,
		decimal TotalCharges, string Pager, DateTime dhcrDateOne, decimal dhcrRentOne, bool dhcrStoveOne, bool dhcrRefrigeratorOne, bool dhcrDishwasherOne, bool dhcrACOne, bool dhcrWindowsOne, bool dhcrOtherOne,
		string dhcrOtherTextOne, DateTime dhcrDateTwo, decimal dhcrRentTwo, bool dhcrStoveTwo, bool dhcrRefrigeratorTwo, bool dhcrDishwasherTwo, bool dhcrACTwo, bool dhcrWindowsTwo, bool dhcrOtherTwo, string dhcrOtherTextTwo,
		bool LegalFee, decimal LateFee, decimal LateBase, bool LegalFeeNo, bool LateFeeYes, bool LateFeeNo, decimal RentLegal, decimal FutureLegal1, decimal FutureLegal2, decimal FutureLegal3,
		decimal ProposedRent, decimal PromptPay1, decimal ProposedRent2, decimal PromptPay2, decimal ProposedLegal2, decimal ProposedLegal, string Cell2, float SecMultiple, string Email1, string Email2,
		string Temp1, string Temp2, string SecurityAccount, Int16 OptTenant1, Int16 OptTenant2, string SecurityBuilding, bool Succeeded, bool SecondSuccession, bool x421a, bool RentOvercharge,
		bool FairMarketAppeal, bool TransientOccupant, bool OwnerOccupied, bool CommercialProfessional, bool NotPrimeResidence, bool TempExemptOther, DateTime ExemptDate, bool ExemptHighRentVacancy, decimal ExemptLastLegalPaid, Int16 ExemptHighRentVacanyPeriod,
		bool ExemptHighRentIncome, bool ExemptCommercialProfessionalCo, bool ExemptSubstantialRehab, bool ExemptCoopCondoOwner, bool ExemptSec11243, bool ExemptOther, string ExemptOtherDesc, bool ExemptExpSec608, bool ExemptExp421a, bool Exempt421aMarketRate,
		bool Exempt421aIncome, Int16 Exempt421aMedianIncome) : base(TABLE, SQL_COL_SEQUENCE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_SequenceNumber = SequenceNumber;
			m_OldHistoryFilePointer = OldHistoryFilePointer;
			m_NewTenantFlag = NewTenantFlag;
			m_ReplacingTenant = ReplacingTenant;
			m_TenantName = TenantName;
			m_Apartment = Apartment;
			m_DueDate = DueDate;
			m_Type = Type;
			m_AlternateBillingAddressFlag = AlternateBillingAddressFlag;
			m_AdditionalNameFlag = AdditionalNameFlag;
			m_LeaseLetterFlag = LeaseLetterFlag;
			m_LowRentFlag = LowRentFlag;
			m_LegalCode = LegalCode;
			m_LegalCode2 = LegalCode2;
			m_Rent = Rent;
			m_Security = Security;
			m_Arrears = Arrears;
			m_Paid = Paid;
			m_FutureRent1 = FutureRent1;
			m_FutureRent2 = FutureRent2;
			m_FutureRent3 = FutureRent3;
			m_MBR =  MBR;
			m_FutureDate1 = FutureDate1;
			m_FutureDate2 = FutureDate2;
			m_FutureDate3 = FutureDate3;
			m_Rooms = Rooms;
			m_SquareFeet = SquareFeet;
			m_MoveinDate = MoveinDate;
			m_LeaseStartDate = LeaseStartDate;
			m_LeaseExpirationDate = LeaseExpirationDate;
			m_LastPaidDate = LastPaidDate;
			m_AdditionalName = AdditionalName;
			m_SSNumber1 = SSNumber1;
			m_SSNumber2 = SSNumber2;
			m_HomePhone1 = HomePhone1;
			m_HomePhone2 = HomePhone2;
			m_BusPhone1 = BusPhone1;
			m_BusPhone2 = BusPhone2;
			m_AltBillName = AltBillName;
			m_AltBillAdd1 = AltBillAdd1;
			m_AltBillAdd2 = AltBillAdd2;
			m_AltBillAdd3 = AltBillAdd3;
			m_AddName = AddName;
			m_Bank = Bank;
			m_InterestInfo = InterestInfo;
			m_Shares = Shares;
			m_Comment = Comment;
			m_Seniorcit = Seniorcit;
			m_TotalCharges = TotalCharges;
			m_Pager = Pager;
			m_dhcrDateOne = dhcrDateOne;
			m_dhcrRentOne = dhcrRentOne;
			m_dhcrStoveOne = dhcrStoveOne;
			m_dhcrRefrigeratorOne = dhcrRefrigeratorOne;
			m_dhcrDishwasherOne = dhcrDishwasherOne;
			m_dhcrACOne = dhcrACOne;
			m_dhcrWindowsOne = dhcrWindowsOne;
			m_dhcrOtherOne = dhcrOtherOne;
			m_dhcrOtherTextOne = dhcrOtherTextOne;
			m_dhcrDateTwo = dhcrDateTwo;
			m_dhcrRentTwo = dhcrRentTwo;
			m_dhcrStoveTwo = dhcrStoveTwo;
			m_dhcrRefrigeratorTwo = dhcrRefrigeratorTwo;
			m_dhcrDishwasherTwo = dhcrDishwasherTwo;
			m_dhcrACTwo = dhcrACTwo;
			m_dhcrWindowsTwo = dhcrWindowsTwo;
			m_dhcrOtherTwo = dhcrOtherTwo;
			m_dhcrOtherTextTwo = dhcrOtherTextTwo;
			m_LegalFee = LegalFee;
			m_LateFee = LateFee;
			m_LateBase = LateBase;
			m_LegalFeeNo = LegalFeeNo;
			m_LateFeeYes = LateFeeYes;
			m_LateFeeNo = LateFeeNo;
			m_RentLegal = RentLegal;
			m_FutureLegal1 = FutureLegal1;
			m_FutureLegal2 = FutureLegal2;
			m_FutureLegal3 = FutureLegal3;
			m_ProposedRent = ProposedRent;
			m_PromptPay1 = PromptPay1;
			m_ProposedRent2 = ProposedRent2;
			m_PromptPay2 = PromptPay2;
			m_ProposedLegal2 = ProposedLegal2;
			m_ProposedLegal = ProposedLegal;
			m_Cell2 = Cell2;
			m_SecMultiple = SecMultiple;
			m_Email1 = Email1;
			m_Email2 = Email2;
			m_Temp1 = Temp1;
			m_Temp2 = Temp2;
			m_SecurityAccount = SecurityAccount;
			m_OptTenant1 = OptTenant1;
			m_OptTenant2 = OptTenant2;
			m_SecurityBuilding = SecurityBuilding;
			m_Succeeded = Succeeded;
			m_SecondSuccession = SecondSuccession;
			m_421a = x421a;
			m_RentOvercharge = RentOvercharge;
			m_FairMarketAppeal = FairMarketAppeal;
			m_TransientOccupant = TransientOccupant;
			m_OwnerOccupied = OwnerOccupied;
			m_CommercialProfessional = CommercialProfessional;
			m_NotPrimeResidence = NotPrimeResidence;
			m_TempExemptOther = TempExemptOther;
			m_ExemptDate = ExemptDate;
			m_ExemptHighRentVacancy = ExemptHighRentVacancy;
			m_ExemptLastLegalPaid = ExemptLastLegalPaid;
			m_ExemptHighRentVacanyPeriod = ExemptHighRentVacanyPeriod;
			m_ExemptHighRentIncome = ExemptHighRentIncome;
			m_ExemptCommercialProfessionalCo = ExemptCommercialProfessionalCo;
			m_ExemptSubstantialRehab = ExemptSubstantialRehab;
			m_ExemptCoopCondoOwner = ExemptCoopCondoOwner;
			m_ExemptSec11243 = ExemptSec11243;
			m_ExemptOther = ExemptOther;
			m_ExemptOtherDesc = ExemptOtherDesc;
			m_ExemptExpSec608 = ExemptExpSec608;
			m_ExemptExp421a = ExemptExp421a;
			m_Exempt421aMarketRate = Exempt421aMarketRate;
			m_Exempt421aIncome = Exempt421aIncome;
			m_Exempt421aMedianIncome = Exempt421aMedianIncome;
		}

		protected TenantsArchive fill(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_SequenceNumber = (int) dr[SQL_COL_SEQUENCE_NUMBER];
				m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
				m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
				m_OldHistoryFilePointer = (int)(Information.IsDBNull(dr[SQL_COL_OLD_HISTORY_FILE_POINTER]) ? 0 : dr[SQL_COL_OLD_HISTORY_FILE_POINTER]);
				m_NewTenantFlag = (bool)dr[SQL_COL_NEW_TENANT_FLAG];
				m_ReplacingTenant = (int)(Information.IsDBNull(dr[SQL_COL_REPLACING_TENANT]) ? 0 : dr[SQL_COL_REPLACING_TENANT]);
				m_DueDate = (string)(Information.IsDBNull(dr[SQL_COL_DUE_DATE]) ? "" : dr[SQL_COL_DUE_DATE]);
				m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
				m_AlternateBillingAddressFlag = (bool) dr[SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG];
				m_AdditionalNameFlag = (bool)dr[SQL_COL_ADDITIONAL_NAME_FLAG];
				m_LeaseLetterFlag = (bool)dr[SQL_COL_LEASE_LETTER_FLAG];
				m_LowRentFlag = (bool)dr[SQL_COL_LOW_RENT_FLAG];
				m_LegalCode = (string)(Information.IsDBNull(dr[SQL_COL_LEGAL_CODE]) ? "" : dr[SQL_COL_LEGAL_CODE]);
				m_LegalCode2 = (string)(Information.IsDBNull(dr[SQL_COL_LEGAL_CODE_2]) ? "" : dr[SQL_COL_LEGAL_CODE_2]);
				m_Rent = (decimal)dr[SQL_COL_RENT];
				m_Security = (decimal)dr[SQL_COL_SECURITY];
				m_Arrears = (decimal)dr[SQL_COL_ARREARS];
				m_Paid = (decimal)dr[SQL_COL_PAID];
				m_FutureRent1 = (decimal)dr[SQL_COL_FUTURE_RENT_1];
				m_FutureRent2 = (decimal)dr[SQL_COL_FUTURE_RENT_2];
				m_FutureRent3 = (decimal)dr[SQL_COL_FUTURE_RENT_3];
				m_MBR = (decimal)(Information.IsDBNull(dr[SQL_COL_M_B_R]) ? 0 : dr[SQL_COL_M_B_R]);
				m_FutureDate1 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_1]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_FUTURE_DATE_1]);
				m_FutureDate2 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_2]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_FUTURE_DATE_2]);
				m_FutureDate3 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_3]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_FUTURE_DATE_3]);
				m_Rooms = (float)dr[SQL_COL_ROOMS];
				m_SquareFeet = (float)dr[SQL_COL_SQUARE_FEET];
				m_MoveinDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_MOVE_IN_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_MOVE_IN_DATE]);
				m_LeaseStartDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LEASE_START_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LEASE_START_DATE]);
				m_LeaseExpirationDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LEASE_EXPIRATION_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LEASE_EXPIRATION_DATE]);
				m_LastPaidDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LAST_PAID_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_PAID_DATE]);
				m_AdditionalName = (string)(Information.IsDBNull(dr[SQL_COL_ADDITIONAL_NAME]) ? "" : dr[SQL_COL_ADDITIONAL_NAME]);
				m_SSNumber1 = (string)(Information.IsDBNull(dr[SQL_COL_S_S_NUMBER1]) ? "" : dr[SQL_COL_S_S_NUMBER1]);
				m_SSNumber2 = (string)(Information.IsDBNull(dr[SQL_COL_S_S_NUMBER2]) ? "" : dr[SQL_COL_S_S_NUMBER2]);
				m_HomePhone1 = (string)(Information.IsDBNull(dr[SQL_COL_HOME_PHONE1]) ? "" : dr[SQL_COL_HOME_PHONE1]);
				m_HomePhone2 = (string)(Information.IsDBNull(dr[SQL_COL_HOME_PHONE2]) ? "" : dr[SQL_COL_HOME_PHONE2]);
				m_BusPhone1 = (string)(Information.IsDBNull(dr[SQL_COL_BUS_PHONE1]) ? "" : dr[SQL_COL_BUS_PHONE1]);
				m_BusPhone2 = (string)(Information.IsDBNull(dr[SQL_COL_BUS_PHONE2]) ? "" : dr[SQL_COL_BUS_PHONE2]);
				m_AltBillName = (string)(Information.IsDBNull(dr[SQL_COL_ALT_BILL_NAME]) ? "" : dr[SQL_COL_ALT_BILL_NAME]);
				m_AltBillAdd1 = (string)(Information.IsDBNull(dr[SQL_COL_ALT_BILL_ADD1]) ? "" : dr[SQL_COL_ALT_BILL_ADD1]);
				m_AltBillAdd2 = (string)(Information.IsDBNull(dr[SQL_COL_ALT_BILL_ADD2]) ? "" : dr[SQL_COL_ALT_BILL_ADD2]);
				m_AltBillAdd3 = (string)(Information.IsDBNull(dr[SQL_COL_ALT_BILL_ADD3]) ? "" : dr[SQL_COL_ALT_BILL_ADD3]);
				m_AddName = (string)(Information.IsDBNull(dr[SQL_COL_ADD_NAME]) ? "" : dr[SQL_COL_ADD_NAME]);
				m_Bank = (string)(Information.IsDBNull(dr[SQL_COL_BANK]) ? "" : dr[SQL_COL_BANK]);
				m_InterestInfo = (string)(Information.IsDBNull(dr[SQL_COL_INTEREST_INFO]) ? "" : dr[SQL_COL_INTEREST_INFO]);
				m_Shares = (float)(Information.IsDBNull(dr[SQL_COL_SHARES]) ? 0 : dr[SQL_COL_SHARES]);
				m_Comment = (string)(Information.IsDBNull(dr[SQL_COL_COMMENT]) ? "" : dr[SQL_COL_COMMENT]);
				m_Seniorcit = (string)(Information.IsDBNull(dr[SQL_COL_SENIORCIT]) ? "" : dr[SQL_COL_SENIORCIT]);
				m_TotalCharges = (decimal)(Information.IsDBNull(dr[SQL_COL_TOTAL_CHARGES]) ? 0 : dr[SQL_COL_TOTAL_CHARGES]);
				m_Pager = (string)(Information.IsDBNull(dr[SQL_COL_PAGER]) ? "" : dr[SQL_COL_PAGER]);
				m_dhcrDateOne = (DateTime)(Information.IsDBNull(dr[SQL_COL_DHCR_DATE_ONE]) ? new DateTime(1900,1,1,0,0,0): dr[SQL_COL_DHCR_DATE_ONE]);
				m_dhcrRentOne = (decimal)(Information.IsDBNull(dr[SQL_COL_DHCR_RENT_ONE]) ? 0 : dr[SQL_COL_DHCR_RENT_ONE]);
				m_dhcrStoveOne = (bool)dr[SQL_COL_DHCR_STOVE_ONE];
				m_dhcrRefrigeratorOne = (bool)dr[SQL_COL_DHCR_REFRIGERATOR_ONE];
				m_dhcrDishwasherOne = (bool)dr[SQL_COL_DHCR_DISHWASHER_ONE];
				m_dhcrACOne = (bool)dr[SQL_COL_DHCR_A_C_ONE];
				m_dhcrWindowsOne = (bool)dr[SQL_COL_DHCR_WINDOWS_ONE];
				m_dhcrOtherOne = (bool)dr[SQL_COL_DHCR_OTHER_ONE];
				m_dhcrOtherTextOne = (string)(Information.IsDBNull(dr[SQL_COL_DHCR_OTHER_TEXT_ONE]) ? "" : dr[SQL_COL_DHCR_OTHER_TEXT_ONE]);
				m_dhcrDateTwo = (DateTime)(Information.IsDBNull(dr[SQL_COL_DHCR_DATE_TWO]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DHCR_DATE_TWO]);
				m_dhcrRentTwo = (decimal)(Information.IsDBNull(dr[SQL_COL_DHCR_RENT_TWO]) ? 0 : dr[SQL_COL_DHCR_RENT_TWO]);
				m_dhcrStoveTwo = (bool)dr[SQL_COL_DHCR_STOVE_TWO];
				m_dhcrRefrigeratorTwo = (bool)dr[SQL_COL_DHCR_REFRIGERATOR_TWO];
				m_dhcrDishwasherTwo = (bool)dr[SQL_COL_DHCR_DISHWASHER_TWO];
				m_dhcrACTwo = (bool)dr[SQL_COL_DHCR_A_C_TWO];
				m_dhcrWindowsTwo = (bool)dr[SQL_COL_DHCR_WINDOWS_TWO];
				m_dhcrOtherTwo = (bool)dr[SQL_COL_DHCR_OTHER_TWO];
				m_dhcrOtherTextTwo = (string)(Information.IsDBNull(dr[SQL_COL_DHCR_OTHER_TEXT_TWO]) ? "" : dr[SQL_COL_DHCR_OTHER_TEXT_TWO]);
				m_LegalFee = (bool)dr[SQL_COL_LEGAL_FEE];
				m_LateFee = (decimal)(Information.IsDBNull(dr[SQL_COL_LATE_FEE]) ? 0 : dr[SQL_COL_LATE_FEE]);
				m_LateBase = (decimal)(Information.IsDBNull(dr[SQL_COL_LATE_BASE]) ? 0 : dr[SQL_COL_LATE_BASE]);
				m_LegalFeeNo = (bool)dr[SQL_COL_LEGAL_FEE_NO];
				m_LateFeeYes = (bool)dr[SQL_COL_LATE_FEE_YES];
				m_LateFeeNo = (bool)dr[SQL_COL_LATE_FEE_NO];
				m_RentLegal = (decimal)(Information.IsDBNull(dr[SQL_COL_RENT_LEGAL]) ? 0 : dr[SQL_COL_RENT_LEGAL]);
				m_FutureLegal1 = (decimal)(Information.IsDBNull(dr[SQL_COL_FUTURE_LEGAL_1]) ? 0 : dr[SQL_COL_FUTURE_LEGAL_1]);
				m_FutureLegal2 = (decimal)(Information.IsDBNull(dr[SQL_COL_FUTURE_LEGAL_2]) ? 0 : dr[SQL_COL_FUTURE_LEGAL_2]);
				m_FutureLegal3 = (decimal)(Information.IsDBNull(dr[SQL_COL_FUTURE_LEGAL_3]) ? 0 : dr[SQL_COL_FUTURE_LEGAL_3]);
				m_ProposedRent = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_RENT]) ? 0 : dr[SQL_COL_PROPOSED_RENT]);
				m_PromptPay1 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROMPT_PAY1]) ? 0 : dr[SQL_COL_PROMPT_PAY1]);
				m_ProposedRent2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_RENT2]) ? 0 : dr[SQL_COL_PROPOSED_RENT2]);
				m_PromptPay2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROMPT_PAY2]) ? 0 : dr[SQL_COL_PROMPT_PAY2]);
				m_ProposedLegal2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_LEGAL2]) ? 0 : dr[SQL_COL_PROPOSED_LEGAL2]);
				m_ProposedLegal = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_LEGAL]) ? 0 : dr[SQL_COL_PROPOSED_LEGAL]);
				m_Cell2 = (string)(Information.IsDBNull(dr[SQL_COL_CELL2]) ? "" : dr[SQL_COL_CELL2]);
				m_SecMultiple = (float)(Information.IsDBNull(dr[SQL_COL_SEC_MULTIPLE]) ? 1 : dr[SQL_COL_SEC_MULTIPLE]);
				m_Email1 = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL1]) ? "" : dr[SQL_COL_EMAIL1]);
				m_Email2 = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL2]) ? "" : dr[SQL_COL_EMAIL2]);
				m_Temp1 = (string)(Information.IsDBNull(dr[SQL_COL_TEMP1]) ? "" : dr[SQL_COL_TEMP1]);
				m_Temp2 = (string)(Information.IsDBNull(dr[SQL_COL_TEMP2]) ? "" : dr[SQL_COL_TEMP2]);
				m_SecurityAccount = (string)(Information.IsDBNull(dr[SQL_COL_SECURITY_ACCOUNT]) ? "" : dr[SQL_COL_SECURITY_ACCOUNT]);
				m_OptTenant1 = (short)(Information.IsDBNull(dr[SQL_COL_OPT_TENANT1]) ? 0 : dr[SQL_COL_OPT_TENANT1]);
				m_OptTenant2 = (short)(Information.IsDBNull(dr[SQL_COL_OPT_TENANT2]) ? 0 : dr[SQL_COL_OPT_TENANT2]);
				m_SecurityBuilding = (string)(Information.IsDBNull(dr[SQL_COL_SECURITY_BUILDING]) ? "" : dr[SQL_COL_SECURITY_BUILDING]);
				m_Succeeded = (bool)dr[SQL_COL_SUCCEEDED];
				m_SecondSuccession = (bool)dr[SQL_COL_SECOND_SUCCESSION];
				m_421a = (bool)dr[SQL_COL_421A];
				m_RentOvercharge = (bool)dr[SQL_COL_RENT_OVERCHARGE];
				m_FairMarketAppeal = (bool)dr[SQL_COL_FAIR_MARKET_APPEAL];
				m_TransientOccupant = (bool)dr[SQL_COL_TRANSIENT_OCCUPANT];
				m_OwnerOccupied = (bool)dr[SQL_COL_OWNER_OCCUPIED];
				m_CommercialProfessional = (bool)dr[SQL_COL_COMMERCIAL_PROFESSIONAL];
				m_NotPrimeResidence = (bool)dr[SQL_COL_NOT_PRIME_RESIDENCE];
				m_TempExemptOther = (bool)dr[SQL_COL_TEMP_EXEMPT_OTHER];
                m_ExemptDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_EXEMPT_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0)  : dr[SQL_COL_EXEMPT_DATE]);
				m_ExemptHighRentVacancy = (bool)dr[SQL_COL_EXEMPT_HIGH_RENT_VACANCY];
				m_ExemptLastLegalPaid = (decimal)dr[SQL_COL_EXEMPT_LAST_LEGAL_PAID];
				m_ExemptHighRentVacanyPeriod = (short)dr[SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD];
				m_ExemptHighRentIncome = (bool)dr[SQL_COL_EXEMPT_HIGH_RENT_INCOME];
				m_ExemptCommercialProfessionalCo = (bool)dr[SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO];
				m_ExemptSubstantialRehab = (bool)dr[SQL_COL_EXEMPT_SUBSTANTIAL_REHAB];
				m_ExemptCoopCondoOwner = (bool)dr[SQL_COL_EXEMPT_COOP_CONDO_OWNER];
				m_ExemptSec11243 = (bool)dr[SQL_COL_EXEMPT_SEC11243];
				m_ExemptOther = (bool)dr[SQL_COL_EXEMPT_OTHER];
				m_ExemptOtherDesc = (string)(Information.IsDBNull(dr[SQL_COL_EXEMPT_OTHER_DESC]) ? "" : dr[SQL_COL_EXEMPT_OTHER_DESC]);
				m_ExemptExpSec608 = (bool)dr[SQL_COL_EXEMPT_EXP_SEC608];
				m_ExemptExp421a = (bool)dr[SQL_COL_EXEMPT_EXP421A];
				m_Exempt421aMarketRate = (bool)dr[SQL_COL_EXEMPT421A_MARKET_RATE];
				m_Exempt421aIncome = (bool)dr[SQL_COL_EXEMPT421A_INCOME];
				m_Exempt421aMedianIncome = (short)dr[SQL_COL_EXEMPT421A_MEDIAN_INCOME];
			} catch (SqlException ex) {
				return this;
			} catch (System.Exception exg) {
				return this;
			}
			return this;

		}

		protected TenantsArchive fillShort(DataRow dr)
		{

			try {
				m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
				m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
				m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
				m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
			} catch (SqlException ex) {
				return this;
			} catch (System.Exception exg) {
				return this;
			}
			return this;

		}

		public static TenantsArchive load(int seq)
		{

			TenantsArchive obj = new TenantsArchive();
			string[] opt = { seq.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_BY_SEQUENCE_NUMBER, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static TenantsArchive[] load(string m_BuildingNumber, string m_TenantNumber)
		{

			ArrayList list = new ArrayList();
			TenantsArchive obj = new TenantsArchive();
			string[] opt = {
				m_BuildingNumber,
				m_TenantNumber
			};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds.Tables[0].Rows.Count == 0) {
				return null;
			}

			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new TenantsArchive();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (TenantsArchive[])list.ToArray(typeof(TenantsArchive));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static TenantsArchive[] loadForNames()
		{

			ArrayList list = new ArrayList();
			TenantsArchive obj = new TenantsArchive();
			string[] opt = null;
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_FOR_NAMES, opt);
			if (ds.Tables[0].Rows.Count == 0) {
				return null;
			}

			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new TenantsArchive();
				obj.fillShort(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (TenantsArchive[])list.ToArray(typeof(TenantsArchive));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static TenantsArchive[] loadForDeletion(string bld)
		{

			ArrayList list = new ArrayList();
			TenantsArchive obj = new TenantsArchive();
			string[] opt = { bld };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD_FOR_DELETION, opt);
			if (ds.Tables[0].Rows.Count == 0) {
				return null;
			}

			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new TenantsArchive();
				obj.fillShort(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (TenantsArchive[])list.ToArray(typeof(TenantsArchive));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static TenantsArchive[] loadAll(long clientId, bool vacants = true)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			TenantsArchive obj = new TenantsArchive();

            string[] opt = { clientId.ToString() };
			ds = obj.load(SQL_LOAD_ALL, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new TenantsArchive();
				obj.fill(dr);
                if (!vacants)
                {
                    if (obj.TenantName.ToLower().Contains("vacant") || obj.TenantName.StartsWith("*"))
                        continue;
                }
                list.Add(obj);
				obj = null;
			}
			try {
				return (TenantsArchive[])list.ToArray(typeof(TenantsArchive));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static TenantsArchive[] load(short orderBy)
		{

			if (orderBy < 0 | orderBy > orderBySql.Length - 1) {
				return null;
			}
			string[] opt = null;
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			TenantsArchive obj = new TenantsArchive();

			ds = obj.load(SQL_LOAD_ALL + orderBySql[orderBy], opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new TenantsArchive();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (TenantsArchive[])list.ToArray(typeof(TenantsArchive));
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

			if (m_SequenceNumber > 0 && !m_NewOnly) {
				string[] values = {
					m_BuildingNumber,
					m_TenantNumber,
					m_OldHistoryFilePointer.ToString(),
					m_NewTenantFlag.ToString(),
					m_ReplacingTenant.ToString(),
					m_TenantName,
					m_Apartment,
					m_DueDate,
					m_Type,
					m_AlternateBillingAddressFlag.ToString(),
					m_AdditionalNameFlag.ToString(),
					m_LeaseLetterFlag.ToString(),
					m_LowRentFlag.ToString(),
					m_LegalCode,
					m_LegalCode2,
					m_Rent.ToString(),
					m_Security.ToString(),
					m_Arrears.ToString(),
					m_Paid.ToString(),
					m_FutureRent1.ToString(),
					m_FutureRent2.ToString(),
					m_FutureRent3.ToString(),
					m_MBR.ToString(),
					m_FutureDate1.ToString(),
					m_FutureDate2.ToString(),
					m_FutureDate3.ToString(),
					m_Rooms.ToString(),
					m_SquareFeet.ToString(),
					m_MoveinDate.ToString(),
					m_LeaseStartDate.ToString(),
					m_LeaseExpirationDate.ToString(),
					m_LastPaidDate.ToString(),
					m_AdditionalName,
					m_SSNumber1,
					m_SSNumber2,
					m_HomePhone1,
					m_HomePhone2,
					m_BusPhone1,
					m_BusPhone2,
					m_AltBillName,
					m_AltBillAdd1,
					m_AltBillAdd2,
					m_AltBillAdd3,
					m_AddName,
					m_Bank,
					m_InterestInfo,
					m_Shares.ToString(),
					m_Comment,
					m_Seniorcit,
					m_TotalCharges.ToString(),
					m_Pager,
					m_dhcrDateOne.ToString(),
					m_dhcrRentOne.ToString(),
					m_dhcrStoveOne.ToString(),
					m_dhcrRefrigeratorOne.ToString(),
					m_dhcrDishwasherOne.ToString(),
					m_dhcrACOne.ToString(),
					m_dhcrWindowsOne.ToString(),
					m_dhcrOtherOne.ToString(),
					m_dhcrOtherTextOne,
					m_dhcrDateTwo.ToString(),
					m_dhcrRentTwo.ToString(),
					m_dhcrStoveTwo.ToString(),
					m_dhcrRefrigeratorTwo.ToString(),
					m_dhcrDishwasherTwo.ToString(),
					m_dhcrACTwo.ToString(),
					m_dhcrWindowsTwo.ToString(),
					m_dhcrOtherTwo.ToString(),
					m_dhcrOtherTextTwo,
					m_LegalFee.ToString(),
					m_LateFee.ToString(),
					m_LateBase.ToString(),
					m_LegalFeeNo.ToString(),
					m_LateFeeYes.ToString(),
					m_LateFeeNo.ToString(),
					m_RentLegal.ToString(),
					m_FutureLegal1.ToString(),
					m_FutureLegal2.ToString(),
					m_FutureLegal3.ToString(),
					m_ProposedRent.ToString(),
					m_PromptPay1.ToString(),
					m_ProposedRent2.ToString(),
					m_PromptPay2.ToString(),
					m_ProposedLegal2.ToString(),
					m_ProposedLegal.ToString(),
					m_Cell2,
					m_SecMultiple.ToString(),
					m_Email1,
					m_Email2,
					m_Temp1,
					m_Temp2,
					m_SecurityAccount,
					m_OptTenant1.ToString(),
					m_OptTenant2.ToString(),
					m_SecurityBuilding,
					m_Succeeded.ToString(),
					m_SecondSuccession.ToString(),
					m_421a.ToString(),
					m_RentOvercharge.ToString(),
					m_FairMarketAppeal.ToString(),
					m_TransientOccupant.ToString(),
					m_OwnerOccupied.ToString(),
					m_CommercialProfessional.ToString(),
					m_NotPrimeResidence.ToString(),
					m_TempExemptOther.ToString(),
					m_ExemptDate.ToString(),
					m_ExemptHighRentVacancy.ToString(),
					m_ExemptLastLegalPaid.ToString(),
					m_ExemptHighRentVacanyPeriod.ToString(),
					m_ExemptHighRentIncome.ToString(),
					m_ExemptCommercialProfessionalCo.ToString(),
					m_ExemptSubstantialRehab.ToString(),
					m_ExemptCoopCondoOwner.ToString(),
					m_ExemptSec11243.ToString(),
					m_ExemptOther.ToString(),
					m_ExemptOtherDesc,
					m_ExemptExpSec608.ToString(),
					m_ExemptExp421a.ToString(),
					m_Exempt421aMarketRate.ToString(),
					m_Exempt421aIncome.ToString(),
					m_Exempt421aMedianIncome.ToString(),
					m_SequenceNumber.ToString()
				};
                if (this.updateRecord(m_SequenceNumber.ToString(), values))
                {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
                    m_SequenceNumber.ToString(),
					m_BuildingNumber,
					m_TenantNumber,
					m_OldHistoryFilePointer.ToString(),
					m_NewTenantFlag.ToString(),
					m_ReplacingTenant.ToString(),
					m_TenantName,
					m_Apartment,
					m_DueDate,
					m_Type,
					m_AlternateBillingAddressFlag.ToString(),
					m_AdditionalNameFlag.ToString(),
					m_LeaseLetterFlag.ToString(),
					m_LowRentFlag.ToString(),
					m_LegalCode,
					m_LegalCode2,
					m_Rent.ToString(),
					m_Security.ToString(),
					m_Arrears.ToString(),
					m_Paid.ToString(),
					m_FutureRent1.ToString(),
					m_FutureRent2.ToString(),
					m_FutureRent3.ToString(),
					m_MBR.ToString(),
					m_FutureDate1.ToString(),
					m_FutureDate2.ToString(),
					m_FutureDate3.ToString(),
					m_Rooms.ToString(),
					m_SquareFeet.ToString(),
					m_MoveinDate.ToString(),
					m_LeaseStartDate.ToString(),
					m_LeaseExpirationDate.ToString(),
					m_LastPaidDate.ToString(),
					m_AdditionalName,
					m_SSNumber1,
					m_SSNumber2,
					m_HomePhone1,
					m_HomePhone2,
					m_BusPhone1,
					m_BusPhone2,
					m_AltBillName,
					m_AltBillAdd1,
					m_AltBillAdd2,
					m_AltBillAdd3,
					m_AddName,
					m_Bank,
					m_InterestInfo,
					m_Shares.ToString(),
					m_Comment,
					m_Seniorcit,
					m_TotalCharges.ToString(),
					m_Pager,
					m_dhcrDateOne.ToString(),
					m_dhcrRentOne.ToString(),
					m_dhcrStoveOne.ToString(),
					m_dhcrRefrigeratorOne.ToString(),
					m_dhcrDishwasherOne.ToString(),
					m_dhcrACOne.ToString(),
					m_dhcrWindowsOne.ToString(),
					m_dhcrOtherOne.ToString(),
					m_dhcrOtherTextOne,
					m_dhcrDateTwo.ToString(),
					m_dhcrRentTwo.ToString(),
					m_dhcrStoveTwo.ToString(),
					m_dhcrRefrigeratorTwo.ToString(),
					m_dhcrDishwasherTwo.ToString(),
					m_dhcrACTwo.ToString(),
					m_dhcrWindowsTwo.ToString(),
					m_dhcrOtherTwo.ToString(),
					m_dhcrOtherTextTwo,
					m_LegalFee.ToString(),
					m_LateFee.ToString(),
					m_LateBase.ToString(),
					m_LegalFeeNo.ToString(),
					m_LateFeeYes.ToString(),
					m_LateFeeNo.ToString(),
					m_RentLegal.ToString(),
					m_FutureLegal1.ToString(),
					m_FutureLegal2.ToString(),
					m_FutureLegal3.ToString(),
					m_ProposedRent.ToString(),
					m_PromptPay1.ToString(),
					m_ProposedRent2.ToString(),
					m_PromptPay2.ToString(),
					m_ProposedLegal2.ToString(),
					m_ProposedLegal.ToString(),
					m_Cell2,
					m_SecMultiple.ToString(),
					m_Email1,
					m_Email2,
					m_Temp1,
					m_Temp2,
					m_SecurityAccount,
					m_OptTenant1.ToString(),
					m_OptTenant2.ToString(),
					m_SecurityBuilding,
					m_Succeeded.ToString(),
					m_SecondSuccession.ToString(),
					m_421a.ToString(),
					m_RentOvercharge.ToString(),
					m_FairMarketAppeal.ToString(),
					m_TransientOccupant.ToString(),
					m_OwnerOccupied.ToString(),
					m_CommercialProfessional.ToString(),
					m_NotPrimeResidence.ToString(),
					m_TempExemptOther.ToString(),
					m_ExemptDate.ToString(),
					m_ExemptHighRentVacancy.ToString(),
					m_ExemptLastLegalPaid.ToString(),
					m_ExemptHighRentVacanyPeriod.ToString(),
					m_ExemptHighRentIncome.ToString(),
					m_ExemptCommercialProfessionalCo.ToString(),
					m_ExemptSubstantialRehab.ToString(),
					m_ExemptCoopCondoOwner.ToString(),
					m_ExemptSec11243.ToString(),
					m_ExemptOther.ToString(),
					m_ExemptOtherDesc,
					m_ExemptExpSec608.ToString(),
					m_ExemptExp421a.ToString(),
					m_Exempt421aMarketRate.ToString(),
					m_Exempt421aIncome.ToString(),
					m_Exempt421aMedianIncome.ToString()
				};

                try
                {
                    m_SequenceNumber = this.insertRecord(values);

                    if (m_SequenceNumber >= 0)
                    {
                        return true;
                    }
				}
                catch (SqlException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Tenant Archive Insert Error: " + ex.Message + "\r\n\n" + m_BuildingNumber + m_TenantNumber + " / " + m_Apartment + " / seq:" + m_SequenceNumber.ToString(), false);
                }
			}
			return false;

		}

		public static int deleteTenantsByBuilding(string bld)
		{

			SqlCommand cmd = MbcsCentral.GlobalShared.CN.CreateCommand();
			SqlTransaction transaction = null;
			int count = 0;

			transaction = MbcsCentral.GlobalShared.CN.BeginTransaction();
			cmd.Connection = MbcsCentral.GlobalShared.CN;
			cmd.Transaction = transaction;

			try {
				TenantsArchive[] tens = TenantsArchive.loadForDeletion(bld);
				if ((tens != null) && tens.Length > 0) {
					Charges[] chgs = null;
					Legal[] lgls = null;
					Occupants[] ocps = null;
					foreach (TenantsArchive ten in tens) {
						// Charges
						chgs = Charges.loadBySeqNum(ten.SequenceNumber);
						if ((chgs != null) && chgs.Length > 0) {
							foreach (Charges chg in chgs) {
								chg.delete(chg.ChargeNumber.ToString());
							}
						}
						chgs = null;
						// Legal
						lgls = Legal.loadById(ten.SequenceNumber);
						if ((lgls != null) && lgls.Length > 0) {
							foreach (Legal lgl in lgls) {
								lgl.delete();
							}
						}
						lgls = null;
						// Occupants
						ocps = Occupants.loadBySequenceNumber(ten.SequenceNumber);
						if ((ocps != null) && ocps.Length > 0) {
							foreach (Occupants ocp in ocps) {
								ocp.delete();
							}
						}
						ocps = null;
						// Delete Tenant Record
						ten.delete(ten.SequenceNumber.ToString());
						count += 1;
					}
				}
				transaction.Commit();
			} catch (Exception ex) {
				Interaction.MsgBox("Error deleting TenantsArchive for Building " + bld + ". Type: " + ex.GetType().ToString() + ", message: " + ex.Message);
				try {
					transaction.Rollback();
					return -1;
				} catch (Exception ex2) {
					Interaction.MsgBox("Rollback Error.  Type: " + ex2.GetType().ToString() + ", message: " + ex2.Message);
					return -1;
				}
			}
			return count;

		}

		public TenantsArchive Clone()
		{

			return (TenantsArchive)this.MemberwiseClone();

		}
		public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
		{

			try {
				CN.Open();
				System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE TenantsArchive (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number AUTONUMBER,Old History File Pointer LONG,New Tenant Flag YESNO,Replacing Tenant LONG,Tenant Name TEXT(30),Apartment TEXT(4),Due Date TEXT(2),Type TEXT(2),Alternate Billing Address Flag YESNO,Additional Name Flag YESNO,Lease Letter Flag YESNO,Low Rent Flag YESNO,Legal Code TEXT(1),Legal Code 2 TEXT(1),Rent CURRENCY,Security CURRENCY,Arrears CURRENCY,Paid CURRENCY,Future Rent 1 CURRENCY,Future Rent 2 CURRENCY,Future Rent 3 CURRENCY,MBR CURRENCY,Future Date 1 DATETIME,Future Date 2 DATETIME,Future Date 3 DATETIME,Rooms SINGLE,Square Feet SINGLE,Move-in Date DATETIME,Lease Start Date DATETIME,Lease Expiration Date DATETIME,Last Paid Date DATETIME,Additional Name TEXT(30),SSNumber1 TEXT(11),SSNumber2 TEXT(11),HomePhone1 TEXT(12),HomePhone2 TEXT(12),BusPhone1 TEXT(21),BusPhone2 TEXT(21),AltBillName TEXT(30),AltBillAdd1 TEXT(30),AltBillAdd2 TEXT(30),AltBillAdd3 TEXT(30),AddName TEXT(30),Bank TEXT(60),InterestInfo TEXT(18),Shares SINGLE,Comment MEMO,Seniorcit TEXT(18),Total Charges CURRENCY,Pager TEXT(12),dhcrDateOne DATETIME,dhcrRentOne CURRENCY,dhcrStoveOne YESNO,dhcrRefrigeratorOne YESNO,dhcrDishwasherOne YESNO,dhcrACOne YESNO,dhcrWindowsOne YESNO,dhcrOtherOne YESNO,dhcrOtherTextOne TEXT(16),dhcrDateTwo DATETIME,dhcrRentTwo CURRENCY,dhcrStoveTwo YESNO,dhcrRefrigeratorTwo YESNO,dhcrDishwasherTwo YESNO,dhcrACTwo YESNO,dhcrWindowsTwo YESNO,dhcrOtherTwo YESNO,dhcrOtherTextTwo TEXT(16),LegalFee YESNO,LateFee CURRENCY,LateBase CURRENCY,LegalFeeNo YESNO,LateFeeYes YESNO,LateFeeNo YESNO,RentLegal CURRENCY,Future Legal 1 CURRENCY,Future Legal 2 CURRENCY,Future Legal 3 CURRENCY,ProposedRent CURRENCY,PromptPay1 CURRENCY,ProposedRent2 CURRENCY,PromptPay2 CURRENCY,ProposedLegal2 CURRENCY,ProposedLegal CURRENCY,Cell2 TEXT(12),SecMultiple SINGLE,Email1 TEXT(64),Email2 TEXT(64),Temp1 TEXT(64),Temp2 TEXT(64),SecurityAccount TEXT(32),OptTenant1 INTEGER,OptTenant2 INTEGER,SecurityBuilding TEXT(32))";
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
				OnPropertyChanged("BuildigNumber");
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
			set { m_SequenceNumber = value; }
		}

		public int OldHistoryFilePointer {
			get { return m_OldHistoryFilePointer; }
			set {
				m_OldHistoryFilePointer = value;
				OnPropertyChanged("OldHistoryFilePointer");
			}
		}

		public bool NewTenantFlag {
			get { return m_NewTenantFlag; }
			set {
				m_NewTenantFlag = value;
				OnPropertyChanged("NewTenantFlag");
			}
		}

		public int ReplacingTenant {
			get { return m_ReplacingTenant; }
			set {
				m_ReplacingTenant = value;
				OnPropertyChanged("ReplacingTenant");
			}
		}

		public string TenantName {
			get { return m_TenantName; }
			set {
				m_TenantName = value;
				OnPropertyChanged("TenantName");
			}
		}

		public string Apartment {
			get { return m_Apartment; }
			set {
				m_Apartment = value;
				OnPropertyChanged("Apartment");
			}
		}

		public string DueDate {
			get { return m_DueDate; }
			set {
				m_DueDate = value;
				OnPropertyChanged("DueDate");
			}
		}

		public string Type {
			get { return m_Type; }
			set {
				m_Type = value;
				OnPropertyChanged("Type");
			}
		}

		public bool AlternateBillingAddressFlag {
			get { return m_AlternateBillingAddressFlag; }
			set {
				m_AlternateBillingAddressFlag = value;
				OnPropertyChanged("AlternateBillingAddressFlag");
			}
		}

		public bool AdditionalNameFlag {
			get { return m_AdditionalNameFlag; }
			set {
				m_AdditionalNameFlag = value;
				OnPropertyChanged("AdditionalNameFlag");
			}
		}

		public bool LeaseLetterFlag {
			get { return m_LeaseLetterFlag; }
			set {
				m_LeaseLetterFlag = value;
				OnPropertyChanged("LeaseLetterFlag");
			}
		}

		public bool LowRentFlag {
			get { return m_LowRentFlag; }
			set {
				m_LowRentFlag = value;
				OnPropertyChanged("LowRentFlag");
			}
		}

		public string LegalCode {
			get { return m_LegalCode; }
			set {
				m_LegalCode = value;
				OnPropertyChanged("LegalCode");
			}
		}

		public string LegalCode2 {
			get { return m_LegalCode2; }
			set {
				m_LegalCode2 = value;
				OnPropertyChanged("LegalCode2");
			}
		}

		public decimal Rent {
			get { return m_Rent; }
			set {
				m_Rent = value;
				OnPropertyChanged("Rent");
			}
		}

		public decimal Security {
			get { return m_Security; }
			set {
				m_Security = value;
				OnPropertyChanged("Security");
			}
		}

		public decimal Arrears {
			get { return m_Arrears; }
			set {
				m_Arrears = value;
				OnPropertyChanged("Arrears");
			}
		}

		public decimal Paid {
			get { return m_Paid; }
			set {
				m_Paid = value;
				OnPropertyChanged("Paid");
			}
		}

		public decimal FutureRent1 {
			get { return m_FutureRent1; }
			set {
				m_FutureRent1 = value;
				OnPropertyChanged("FutureRent1");
			}
		}

		public decimal FutureRent2 {
			get { return m_FutureRent2; }
			set {
				m_FutureRent2 = value;
				OnPropertyChanged("FutureRent2");
			}
		}

		public decimal FutureRent3 {
			get { return m_FutureRent3; }
			set {
				m_FutureRent3 = value;
				OnPropertyChanged("FutureRent3");
			}
		}

		public decimal MBR {
			get { return m_MBR; }
			set {
				m_MBR = value;
				OnPropertyChanged("MBR");
			}
		}

		public DateTime FutureDate1 {
			get { return m_FutureDate1; }
			set {
				m_FutureDate1 = value;
				OnPropertyChanged("FutureDate1");
			}
		}

		public DateTime FutureDate2 {
			get { return m_FutureDate2; }
			set {
				m_FutureDate2 = value;
				OnPropertyChanged("FutureDate2");
			}
		}

		public DateTime FutureDate3 {
			get { return m_FutureDate3; }
			set {
				m_FutureDate3 = value;
				OnPropertyChanged("FutureDate3");
			}
		}

		public float Rooms {
			get { return m_Rooms; }
			set {
				m_Rooms = value;
				OnPropertyChanged("Rooms");
			}
		}

		public float SquareFeet {
			get { return m_SquareFeet; }
			set {
				m_SquareFeet = value;
				OnPropertyChanged("SquareFeet");
			}
		}

		public DateTime MoveinDate {
			get { return m_MoveinDate; }
			set {
				m_MoveinDate = value;
				OnPropertyChanged("MoveinDate");
			}
		}

		public DateTime LeaseStartDate {
			get { return m_LeaseStartDate; }
			set {
				m_LeaseStartDate = value;
				OnPropertyChanged("LeaseStartDate");
			}
		}

		public DateTime LeaseExpirationDate {
			get { return m_LeaseExpirationDate; }
			set {
				m_LeaseExpirationDate = value;
				OnPropertyChanged("LeaseExpirationDate");
			}
		}

		public DateTime LastPaidDate {
			get { return m_LastPaidDate; }
			set {
				m_LastPaidDate = value;
				OnPropertyChanged("LastPaidDate");
			}
		}

		public string AdditionalName {
			get { return m_AdditionalName; }
			set {
				m_AdditionalName = value;
				OnPropertyChanged("AdditionalName");
			}
		}

		public string SSNumber1 {
			get { return m_SSNumber1; }
			set {
				m_SSNumber1 = value;
				OnPropertyChanged("SSNumber1");
			}
		}

		public string SSNumber2 {
			get { return m_SSNumber2; }
			set {
				m_SSNumber2 = value;
				OnPropertyChanged("SSNumber2");
			}
		}

		public string HomePhone1 {
			get { return m_HomePhone1; }
			set {
				m_HomePhone1 = value;
				OnPropertyChanged("HomePhone1");
			}
		}

		public string HomePhone2 {
			get { return m_HomePhone2; }
			set {
				m_HomePhone2 = value;
				OnPropertyChanged("HomePhone2");
			}
		}

		public string BusPhone1 {
			get { return m_BusPhone1; }
			set {
				m_BusPhone1 = value;
				OnPropertyChanged("BusPhone1");
			}
		}

		public string BusPhone2 {
			get { return m_BusPhone2; }
			set {
				m_BusPhone2 = value;
				OnPropertyChanged("BusPhone2");
			}
		}

		public string AltBillName {
			get { return m_AltBillName; }
			set {
				m_AltBillName = value;
				OnPropertyChanged("AltBillName");
			}
		}

		public string AltBillAdd1 {
			get { return m_AltBillAdd1; }
			set {
				m_AltBillAdd1 = value;
				OnPropertyChanged("AltBillAdd1");
			}
		}

		public string AltBillAdd2 {
			get { return m_AltBillAdd2; }
			set {
				m_AltBillAdd2 = value;
				OnPropertyChanged("AltBillAdd2");
			}
		}

		public string AltBillAdd3 {
			get { return m_AltBillAdd3; }
			set {
				m_AltBillAdd3 = value;
				OnPropertyChanged("AltBillAdd3");
			}
		}

		public string AddName {
			get { return m_AddName; }
			set {
				m_AddName = value;
				OnPropertyChanged("AddName");
			}
		}

		public string Bank {
			get { return m_Bank; }
			set {
				m_Bank = value;
				OnPropertyChanged("Bank");
			}
		}

		public string InterestInfo {
			get { return m_InterestInfo; }
			set {
				m_InterestInfo = value;
				OnPropertyChanged("InterestInfo");
			}
		}

		public float Shares {
			get { return m_Shares; }
			set {
				m_Shares = value;
				OnPropertyChanged("Shares");
			}
		}

		public string Comment {
			get { return m_Comment; }
			set {
				m_Comment = value;
				OnPropertyChanged("Comment");
			}
		}

		public string Seniorcit {
			get { return m_Seniorcit; }
			set {
				m_Seniorcit = value;
				OnPropertyChanged("Seniorcit");
			}
		}

		public decimal TotalCharges {
			get { return m_TotalCharges; }
			set {
				m_TotalCharges = value;
				OnPropertyChanged("TotalCharges");
			}
		}

		public string Pager {
			get { return m_Pager; }
			set {
				m_Pager = value;
				OnPropertyChanged("Pager");
			}
		}

		public DateTime dhcrDateOne {
			get { return m_dhcrDateOne; }
			set {
				m_dhcrDateOne = value;
				OnPropertyChanged("dhcrDateOne");
			}
		}

		public decimal dhcrRentOne {
			get { return m_dhcrRentOne; }
			set {
				m_dhcrRentOne = value;
				OnPropertyChanged("dhcrRentOne");
			}
		}

		public bool dhcrStoveOne {
			get { return m_dhcrStoveOne; }
			set {
				m_dhcrStoveOne = value;
				OnPropertyChanged("dhcrStoveOne");
			}
		}

		public bool dhcrRefrigeratorOne {
			get { return m_dhcrRefrigeratorOne; }
			set {
				m_dhcrRefrigeratorOne = value;
				OnPropertyChanged("dhcrRefrigeratorOne");
			}
		}

		public bool dhcrDishwasherOne {
			get { return m_dhcrDishwasherOne; }
			set {
				m_dhcrDishwasherOne = value;
				OnPropertyChanged("dhcrDishwasherOne");
			}
		}

		public bool dhcrACOne {
			get { return m_dhcrACOne; }
			set {
				m_dhcrACOne = value;
				OnPropertyChanged("dhcrACOne");
			}
		}

		public bool dhcrWindowsOne {
			get { return m_dhcrWindowsOne; }
			set {
				m_dhcrWindowsOne = value;
				OnPropertyChanged("dhcrWindowsOne");
			}
		}

		public bool dhcrOtherOne {
			get { return m_dhcrOtherOne; }
			set {
				m_dhcrOtherOne = value;
				OnPropertyChanged("dhcrOtherOne");
			}
		}

		public string dhcrOtherTextOne {
			get { return m_dhcrOtherTextOne; }
			set {
				m_dhcrOtherTextOne = value;
				OnPropertyChanged("dhcrOtherTextOne");
			}
		}

		public DateTime dhcrDateTwo {
			get { return m_dhcrDateTwo; }
			set {
				m_dhcrDateTwo = value;
				OnPropertyChanged("dhcrDateTwo");
			}
		}

		public decimal dhcrRentTwo {
			get { return m_dhcrRentTwo; }
			set {
				m_dhcrRentTwo = value;
				OnPropertyChanged("dhcrRentTwo");
			}
		}

		public bool dhcrStoveTwo {
			get { return m_dhcrStoveTwo; }
			set {
				m_dhcrStoveTwo = value;
				OnPropertyChanged("dhcrStoveTwo");
			}
		}

		public bool dhcrRefrigeratorTwo {
			get { return m_dhcrRefrigeratorTwo; }
			set {
				m_dhcrRefrigeratorTwo = value;
				OnPropertyChanged("dhcrRefrigeratorTwo");
			}
		}

		public bool dhcrDishwasherTwo {
			get { return m_dhcrDishwasherTwo; }
			set {
				m_dhcrDishwasherTwo = value;
				OnPropertyChanged("dhcrDishwasherTwo");
			}
		}

		public bool dhcrACTwo {
			get { return m_dhcrACTwo; }
			set {
				m_dhcrACTwo = value;
				OnPropertyChanged("dhcrACTwo");
			}
		}

		public bool dhcrWindowsTwo {
			get { return m_dhcrWindowsTwo; }
			set {
				m_dhcrWindowsTwo = value;
				OnPropertyChanged("dhcrWindowsTwo");
			}
		}

		public bool dhcrOtherTwo {
			get { return m_dhcrOtherTwo; }
			set {
				m_dhcrOtherTwo = value;
				OnPropertyChanged("dhcrOtherTwo");
			}
		}

		public string dhcrOtherTextTwo {
			get { return m_dhcrOtherTextTwo; }
			set {
				m_dhcrOtherTextTwo = value;
				OnPropertyChanged("dhcrOtherTextTwo");
			}
		}

		public bool LegalFee {
			get { return m_LegalFee; }
			set {
				m_LegalFee = value;
				OnPropertyChanged("LegalFee");
			}
		}

		public decimal LateFee {
			get { return m_LateFee; }
			set {
				m_LateFee = value;
				OnPropertyChanged("LateFee");
			}
		}

		public decimal LateBase {
			get { return m_LateBase; }
			set {
				m_LateBase = value;
				OnPropertyChanged("LateBase");
			}
		}

		public bool LegalFeeNo {
			get { return m_LegalFeeNo; }
			set {
				m_LegalFeeNo = value;
				OnPropertyChanged("LegalFeeNo");
			}
		}

		public bool LateFeeYes {
			get { return m_LateFeeYes; }
			set {
				m_LateFeeYes = value;
				OnPropertyChanged("LateFeeYes");
			}
		}

		public bool LateFeeNo {
			get { return m_LateFeeNo; }
			set {
				m_LateFeeNo = value;
				OnPropertyChanged("LateFeeNo");
			}
		}

		public decimal RentLegal {
			get { return m_RentLegal; }
			set {
				m_RentLegal = value;
				OnPropertyChanged("RentLegal");
			}
		}

		public decimal FutureLegal1 {
			get { return m_FutureLegal1; }
			set {
				m_FutureLegal1 = value;
				OnPropertyChanged("FutureLegal1");
			}
		}

		public decimal FutureLegal2 {
			get { return m_FutureLegal2; }
			set {
				m_FutureLegal2 = value;
				OnPropertyChanged("FutureLegal2");
			}
		}

		public decimal FutureLegal3 {
			get { return m_FutureLegal3; }
			set {
				m_FutureLegal3 = value;
				OnPropertyChanged("FutureLegal3");
			}
		}

		public decimal ProposedRent {
			get { return m_ProposedRent; }
			set {
				m_ProposedRent = value;
				OnPropertyChanged("ProposedRent");
			}
		}

		public decimal PromptPay1 {
			get { return m_PromptPay1; }
			set {
				m_PromptPay1 = value;
				OnPropertyChanged("PromptPay1");
			}
		}

		public decimal ProposedRent2 {
			get { return m_ProposedRent2; }
			set {
				m_ProposedRent2 = value;
				OnPropertyChanged("ProposedRent2");
			}
		}

		public decimal PromptPay2 {
			get { return m_PromptPay2; }
			set {
				m_PromptPay2 = value;
				OnPropertyChanged("PromptPay2");
			}
		}

		public decimal ProposedLegal2 {
			get { return m_ProposedLegal2; }
			set {
				m_ProposedLegal2 = value;
				OnPropertyChanged("ProposedLegal2");
			}
		}

		public decimal ProposedLegal {
			get { return m_ProposedLegal; }
			set {
				m_ProposedLegal = value;
				OnPropertyChanged("ProposedLegal");
			}
		}

		public string Cell2 {
			get { return m_Cell2; }
			set {
				m_Cell2 = value;
				OnPropertyChanged("Cell2");
			}
		}

		public float SecMultiple {
			get { return m_SecMultiple; }
			set {
				m_SecMultiple = value;
				OnPropertyChanged("SecMultiple");
			}
		}

		public string Email1 {
			get { return m_Email1; }
			set {
				m_Email1 = value;
				OnPropertyChanged("Email1");
			}
		}

		public string Email2 {
			get { return m_Email2; }
			set {
				m_Email2 = value;
				OnPropertyChanged("Email2");
			}
		}

		public string Temp1 {
			get { return m_Temp1; }
			set {
				m_Temp1 = value;
				OnPropertyChanged("Temp1");
			}
		}

		public string Temp2 {
			get { return m_Temp2; }
			set {
				m_Temp2 = value;
				OnPropertyChanged("Temp2");
			}
		}

		public string SecurityAccount {
			get { return m_SecurityAccount; }
			set {
				m_SecurityAccount = value;
				OnPropertyChanged("SecurityAccount");
			}
		}

		public Int16 OptTenant1 {
			get { return m_OptTenant1; }
			set {
				m_OptTenant1 = value;
				OnPropertyChanged("OptTenant1");
			}
		}

		public Int16 OptTenant2 {
			get { return m_OptTenant2; }
			set {
				m_OptTenant2 = value;
				OnPropertyChanged("OptTenant2");
			}
		}

		public string SecurityBuilding {
			get { return m_SecurityBuilding; }
			set {
				m_SecurityBuilding = value;
				OnPropertyChanged("SecurityBuilding");
			}
		}
		public bool Succeeded {
			get { return m_Succeeded; }
			set {
				m_Succeeded = value;
				OnPropertyChanged("Succeeded");
			}
		}

		public bool SecondSuccession {
			get { return m_SecondSuccession; }
			set {
				m_SecondSuccession = value;
				OnPropertyChanged("SecondSuccession");
			}
		}

		public bool x421a {
			get { return m_421a; }
			set {
				m_421a = value;
				OnPropertyChanged("421a");
			}
		}

		public bool RentOvercharge {
			get { return m_RentOvercharge; }
			set {
				m_RentOvercharge = value;
				OnPropertyChanged("RentOvercharge");
			}
		}

		public bool FairMarketAppeal {
			get { return m_FairMarketAppeal; }
			set {
				m_FairMarketAppeal = value;
				OnPropertyChanged("FairMarketAppeal");
			}
		}

		public bool TransientOccupant {
			get { return m_TransientOccupant; }
			set {
				m_TransientOccupant = value;
				OnPropertyChanged("TransientOccupant");
			}
		}

		public bool OwnerOccupied {
			get { return m_OwnerOccupied; }
			set {
				m_OwnerOccupied = value;
				OnPropertyChanged("OwnerOccupied");
			}
		}

		public bool CommercialProfessional {
			get { return m_CommercialProfessional; }
			set {
				m_CommercialProfessional = value;
				OnPropertyChanged("CommercialProfessional");
			}
		}

		public bool NotPrimeResidence {
			get { return m_NotPrimeResidence; }
			set {
				m_NotPrimeResidence = value;
				OnPropertyChanged("NotPrimeResidence");
			}
		}

		public bool TempExemptOther {
			get { return m_TempExemptOther; }
			set {
				m_TempExemptOther = value;
				OnPropertyChanged("TempExemptOther");
			}
		}

		public DateTime ExemptDate {
			get { return m_ExemptDate; }
			set {
				m_ExemptDate = value;
				OnPropertyChanged("ExemptDate");
			}
		}

		public bool ExemptHighRentVacancy {
			get { return m_ExemptHighRentVacancy; }
			set {
				m_ExemptHighRentVacancy = value;
				OnPropertyChanged("ExemptHighRentVacancy");
			}
		}

		public decimal ExemptLastLegalPaid {
			get { return m_ExemptLastLegalPaid; }
			set {
				m_ExemptLastLegalPaid = value;
				OnPropertyChanged("ExemptLastLegalPaid");
			}
		}

		public Int16 ExemptHighRentVacanyPeriod {
			get { return m_ExemptHighRentVacanyPeriod; }
			set {
				m_ExemptHighRentVacanyPeriod = value;
				OnPropertyChanged("ExemptHighRentVacanyPeriod");
			}
		}

		public bool ExemptHighRentIncome {
			get { return m_ExemptHighRentIncome; }
			set {
				m_ExemptHighRentIncome = value;
				OnPropertyChanged("ExemptHighRentIncome");
			}
		}

		public bool ExemptCommercialProfessionalCo {
			get { return m_ExemptCommercialProfessionalCo; }
			set {
				m_ExemptCommercialProfessionalCo = value;
				OnPropertyChanged("ExemptCommercialProfessionalCo");
			}
		}

		public bool ExemptSubstantialRehab {
			get { return m_ExemptSubstantialRehab; }
			set {
				m_ExemptSubstantialRehab = value;
				OnPropertyChanged("ExemptSubstantialRehab");
			}
		}

		public bool ExemptCoopCondoOwner {
			get { return m_ExemptCoopCondoOwner; }
			set {
				m_ExemptCoopCondoOwner = value;
				OnPropertyChanged("ExemptCoopCondoOwner");
			}
		}

		public bool ExemptSec11243 {
			get { return m_ExemptSec11243; }
			set {
				m_ExemptSec11243 = value;
				OnPropertyChanged("ExemptSec11243");
			}
		}

		public bool ExemptOther {
			get { return m_ExemptOther; }
			set {
				m_ExemptOther = value;
				OnPropertyChanged("ExemptOther");
			}
		}

		public string ExemptOtherDesc {
			get { return m_ExemptOtherDesc; }
			set {
				m_ExemptOtherDesc = value;
				OnPropertyChanged("ExemptOtherDesc");
			}
		}

		public bool ExemptExpSec608 {
			get { return m_ExemptExpSec608; }
			set {
				m_ExemptExpSec608 = value;
				OnPropertyChanged("ExemptExpSec608");
			}
		}

		public bool ExemptExp421a {
			get { return m_ExemptExp421a; }
			set {
				m_ExemptExp421a = value;
				OnPropertyChanged("ExemptExp421a");
			}
		}

		public bool Exempt421aMarketRate {
			get { return m_Exempt421aMarketRate; }
			set {
				m_Exempt421aMarketRate = value;
				OnPropertyChanged("Exempt421aMarketRate");
			}
		}

		public bool Exempt421aIncome {
			get { return m_Exempt421aIncome; }
			set {
				m_Exempt421aIncome = value;
				OnPropertyChanged("Exempt421aIncome");
			}
		}

		public Int16 Exempt421aMedianIncome {
			get { return m_Exempt421aMedianIncome; }
			set {
				m_Exempt421aMedianIncome = value;
				OnPropertyChanged("Exempt421aMedianIncome");
			}
		}

        public bool NewOnly
        {
            set { m_NewOnly = value; }
        }
	}
}
