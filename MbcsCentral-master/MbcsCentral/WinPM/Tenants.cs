using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using Microsoft.VisualBasic;
using Xceed.Wpf.DataGrid.FilterCriteria;
namespace MbcsCentral.WinPM
{

    public class Tenants : clsDB, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public static string TABLE = "pm_Tenants";

	    public const string SQL_COL_FUTURE_DATE_1 = "Future Date 1";
	    public const string SQL_COL_FUTURE_DATE_2 = "Future Date 2";
	    public const string SQL_COL_FUTURE_DATE_3 = "Future Date 3";
	    public const string SQL_COL_MOVE_IN_DATE = "Move-in Date";
	    public const string SQL_COL_LEASE_START_DATE = "Lease Start Date";
	    public const string SQL_COL_LEASE_EXPIRATION_DATE = "Lease Expiration Date";
	    public const string SQL_COL_LAST_PAID_DATE = "Last Paid Date";
	    public const string SQL_COL_DHCR_DATE_ONE = "dhcrDateOne";
	    public const string SQL_COL_DHCR_DATE_TWO = "dhcrDateTwo";
	    public const string SQL_COL_EXEMPT_DATE = "ExemptDate";
	    public const string SQL_COL_DHCR_SUBMITTED_DATE_ONE = "dhcrSubmittedDateOne";
	    public const string SQL_COL_DHCR_SUBMITTED_DATE_TWO = "dhcrSubmittedDateTwo";
	    public const string SQL_COL_OPT_TENANT1 = "OptTenant1";
	    public const string SQL_COL_OPT_TENANT2 = "OptTenant2";
	    public const string SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD = "ExemptHighRentVacanyPeriod";
	    public const string SQL_COL_EXEMPT421A_MEDIAN_INCOME = "Exempt421aMedianIncome";
	    public const string SQL_COL_SEQUENCE_NUMBER = "Sequence Number";
	    public const string SQL_COL_OLD_HISTORY_FILE_POINTER = "Old History File Pointer";
	    public const string SQL_COL_REPLACING_TENANT = "Replacing Tenant";
	    public const string SQL_COL_CLIENT_ID = "clientId";
	    public const string SQL_COL_PAYMENT_DAY = "PaymentDay";
	    public const string SQL_COL_ROOMS = "Rooms";
	    public const string SQL_COL_SQUARE_FEET = "Square Feet";
	    public const string SQL_COL_SHARES = "Shares";
	    public const string SQL_COL_SEC_MULTIPLE = "SecMultiple";
	    public const string SQL_COL_RENT = "Rent";
	    public const string SQL_COL_SECURITY = "Security";
	    public const string SQL_COL_ARREARS = "Arrears";
	    public const string SQL_COL_PAID = "Paid";
	    public const string SQL_COL_FUTURE_RENT_1 = "Future Rent 1";
	    public const string SQL_COL_FUTURE_RENT_2 = "Future Rent 2";
	    public const string SQL_COL_DECONTROLLED_LEGAL = "DecontrolledLegal";
	    public const string SQL_COL_DHCR_TOTAL_COST_ONE = "dhcrTotalCostOne";
	    public const string SQL_COL_DHCR_TOTAL_COST_TWO = "dhcrTotalCostTwo";
	    public const string SQL_COL_PROMPT_PAY1 = "PromptPay1";
	    public const string SQL_COL_PROPOSED_RENT2 = "ProposedRent2";
	    public const string SQL_COL_PROMPT_PAY2 = "PromptPay2";
	    public const string SQL_COL_PROPOSED_LEGAL2 = "ProposedLegal2";
	    public const string SQL_COL_PROPOSED_LEGAL = "ProposedLegal";
	    public const string SQL_COL_EXEMPT_LAST_LEGAL_PAID = "ExemptLastLegalPaid";
	    public const string SQL_COL_LATE_BASE = "LateBase";
	    public const string SQL_COL_RENT_LEGAL = "RentLegal";
	    public const string SQL_COL_FUTURE_LEGAL_1 = "Future Legal 1";
	    public const string SQL_COL_FUTURE_LEGAL_2 = "Future Legal 2";
	    public const string SQL_COL_FUTURE_LEGAL_3 = "Future Legal 3";
	    public const string SQL_COL_PROPOSED_RENT = "ProposedRent";
	    public const string SQL_COL_FUTURE_RENT_3 = "Future Rent 3";
	    public const string SQL_COL_M_B_R = "MBR";
	    public const string SQL_COL_TOTAL_CHARGES = "Total Charges";
	    public const string SQL_COL_DHCR_RENT_ONE = "dhcrRentOne";
	    public const string SQL_COL_DHCR_RENT_TWO = "dhcrRentTwo";
	    public const string SQL_COL_LATE_FEE = "LateFee";
	    public const string SQL_COL_NEW_TENANT_FLAG = "New Tenant Flag";
	    public const string SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG = "Alternate Billing Address Flag";
	    public const string SQL_COL_ADDITIONAL_NAME_FLAG = "Additional Name Flag";
	    public const string SQL_COL_LEASE_LETTER_FLAG = "Lease Letter Flag";
	    public const string SQL_COL_LOW_RENT_FLAG = "Low Rent Flag";
	    public const string SQL_COL_DHCR_STOVE_ONE = "dhcrStoveOne";
	    public const string SQL_COL_LATE_FEE_PERCENT = "LatePercent";
	    public const string SQL_COL_DHCR_SUBMITTED_ONE = "dhcrSubmittedOne";
	    public const string SQL_COL_DHCR_SUBMITTED_TWO = "dhcrSubmittedTwo";
	    public const string SQL_COL_DHCR_INFORMED_CONSENT_ONE = "dhcrInformedConsentOne";
	    public const string SQL_COL_DHCR_INFORMED_CONSENT_TWO = "dhcrInformedConsentTwo";
	    public const string SQL_COL_USE_A_C_H = "UseACH";
	    public const string SQL_COL_EXEMPT_SEC11243 = "ExemptSec11243";
	    public const string SQL_COL_EXEMPT_OTHER = "ExemptOther";
	    public const string SQL_COL_EXEMPT_EXP_SEC608 = "ExemptExpSec608";
	    public const string SQL_COL_EXEMPT_EXP421A = "ExemptExp421a";
	    public const string SQL_COL_EXEMPT421A_MARKET_RATE = "Exempt421aMarketRate";
	    public const string SQL_COL_EXEMPT421A_INCOME = "Exempt421aIncome";
	    public const string SQL_COL_TEMP_EXEMPT_OTHER = "TempExemptOther";
	    public const string SQL_COL_EXEMPT_HIGH_RENT_VACANCY = "ExemptHighRentVacancy";
	    public const string SQL_COL_EXEMPT_HIGH_RENT_INCOME = "ExemptHighRentIncome";
	    public const string SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO = "ExemptCommercialProfessionalCo";
	    public const string SQL_COL_EXEMPT_SUBSTANTIAL_REHAB = "ExemptSubstantialRehab";
	    public const string SQL_COL_EXEMPT_COOP_CONDO_OWNER = "ExemptCoopCondoOwner";
	    public const string SQL_COL_RENT_OVERCHARGE = "RentOvercharge";
	    public const string SQL_COL_FAIR_MARKET_APPEAL = "FairMarketAppeal";
	    public const string SQL_COL_TRANSIENT_OCCUPANT = "TransientOccupant";
	    public const string SQL_COL_OWNER_OCCUPIED = "OwnerOccupied";
	    public const string SQL_COL_COMMERCIAL_PROFESSIONAL = "CommercialProfessional";
	    public const string SQL_COL_NOT_PRIME_RESIDENCE = "NotPrimeResidence";
	    public const string SQL_COL_LEGAL_FEE_NO = "LegalFeeNo";
	    public const string SQL_COL_LATE_FEE_YES = "LateFeeYes";
	    public const string SQL_COL_LATE_FEE_NO = "LateFeeNo";
	    public const string SQL_COL_SUCCEEDED = "Succeeded";
	    public const string SQL_COL_SECOND_SUCCESSION = "SecondSuccession";
	    public const string SQL_COL_421A = "421a";
	    public const string SQL_COL_DHCR_REFRIGERATOR_TWO = "dhcrRefrigeratorTwo";
	    public const string SQL_COL_DHCR_DISHWASHER_TWO = "dhcrDishwasherTwo";
	    public const string SQL_COL_DHCR_A_C_TWO = "dhcrACTwo";
	    public const string SQL_COL_DHCR_WINDOWS_TWO = "dhcrWindowsTwo";
	    public const string SQL_COL_DHCR_OTHER_TWO = "dhcrOtherTwo";
	    public const string SQL_COL_LEGAL_FEE = "LegalFee";
	    public const string SQL_COL_DHCR_REFRIGERATOR_ONE = "dhcrRefrigeratorOne";
	    public const string SQL_COL_DHCR_DISHWASHER_ONE = "dhcrDishwasherOne";
	    public const string SQL_COL_DHCR_A_C_ONE = "dhcrACOne";
	    public const string SQL_COL_DHCR_WINDOWS_ONE = "dhcrWindowsOne";
	    public const string SQL_COL_DHCR_OTHER_ONE = "dhcrOtherOne";
	    public const string SQL_COL_DHCR_STOVE_TWO = "dhcrStoveTwo";
	    public const string SQL_COL_BUILDING_NUMBER = "Building Number";
	    public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
	    public const string SQL_COL_TENANT_NAME = "Tenant Name";
	    public const string SQL_COL_APARTMENT = "Apartment";
	    public const string SQL_COL_DUE_DATE = "Due Date";
	    public const string SQL_COL_TYPE = "Type";
	    //public const string SQL_COL_TEMP1 = "Temp1";
	    //public const string SQL_COL_TEMP2 = "Temp2";
	    public const string SQL_COL_SECURITY_ACCOUNT = "SecurityAccount";
	    public const string SQL_COL_SECURITY_BUILDING = "SecurityBuilding";
	    public const string SQL_COL_EXEMPT_OTHER_DESC = "ExemptOtherDesc";
	    public const string SQL_COL_PAGER = "Pager";
	    public const string SQL_COL_DHCR_OTHER_TEXT_ONE = "dhcrOtherTextOne";
	    public const string SQL_COL_DHCR_OTHER_TEXT_TWO = "dhcrOtherTextTwo";
	    public const string SQL_COL_CELL2 = "Cell2";
	    public const string SQL_COL_EMAIL1 = "Email1";
	    public const string SQL_COL_EMAIL2 = "Email2";
	    public const string SQL_COL_ALT_BILL_ADD3 = "AltBillAdd3";
	    public const string SQL_COL_ADD_NAME = "AddName";
	    public const string SQL_COL_BANK = "Bank";
	    public const string SQL_COL_INTEREST_INFO = "InterestInfo";
	    public const string SQL_COL_COMMENT = "Comment";
	    public const string SQL_COL_SENIORCIT = "Seniorcit";
	    public const string SQL_COL_HOME_PHONE2 = "HomePhone2";
	    public const string SQL_COL_BUS_PHONE1 = "BusPhone1";
	    public const string SQL_COL_BUS_PHONE2 = "BusPhone2";
	    public const string SQL_COL_ALT_BILL_NAME = "AltBillName";
	    public const string SQL_COL_ALT_BILL_ADD1 = "AltBillAdd1";
	    public const string SQL_COL_ALT_BILL_ADD2 = "AltBillAdd2";
	    public const string SQL_COL_LEGAL_CODE = "Legal Code";
	    public const string SQL_COL_LEGAL_CODE_2 = "Legal Code 2";
	    public const string SQL_COL_ADDITIONAL_NAME = "Additional Name";
	    public const string SQL_COL_S_S_NUMBER1 = "SSNumber1";
	    public const string SQL_COL_S_S_NUMBER2 = "SSNumber2";
	    public const string SQL_COL_HOME_PHONE1 = "HomePhone1";
        public const string SQL_COL_SAVINGS_ACCOUNT = "SavingsAccount";
        public const string SQL_COL_EMPLOYEE_OCCUPIED = "EmployeeOccupied";
        public const string SQL_COL_XRF_TESTED = "XrfTested";
        public const string SQL_COL_XRF_EXEMPT = "xrfExempt";
        public const string SQL_COL_XRF_TESTED_DATE = "XrfTestedDate";
        public const string SQL_COL_XRF_EXEMPT_DATE = "XrfExemptDate";
        public const string SQL_COL_DIRECT_PAY = "DirectPay";
        public const string SQL_COL_XRF_WORK_COMPLETE = "XrfWorkComplete";
        public const string SQL_COL_XRF_WORK_COMPLETE_DATE = "XrfWorkCompleteDate";
        public const string SQL_COL_PROPOSED_RENT2_1 = "ProposedRent2_1";
        public const string SQL_COL_MASTER_ACCOUNT = "MasterAccount";
        public const string SQL_COL_OWNER_ADDRESS1 = "Address 1";
        public const string SQL_COL_OWNER_ADDRESS2 = "Address 2";

# region Query Statments

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                "[" + SQL_COL_MOVE_IN_DATE + "]," +
                "[" + SQL_COL_LEASE_START_DATE + "]," +
                "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                "[" + SQL_COL_LAST_PAID_DATE + "]," +
                "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                "[" + SQL_COL_EXEMPT_DATE + "]," +
                "[" + SQL_COL_OPT_TENANT1 + "]," +
                "[" + SQL_COL_OPT_TENANT2 + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                "[" + SQL_COL_ROOMS + "]," +
                "[" + SQL_COL_SQUARE_FEET + "]," +
                "[" + SQL_COL_SHARES + "]," +
                "[" + SQL_COL_SEC_MULTIPLE + "]," +
                "[" + SQL_COL_RENT + "]," +
                "[" + SQL_COL_SECURITY + "]," +
                "[" + SQL_COL_ARREARS + "]," +
                "[" + SQL_COL_PAID + "]," +
                "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                "[" + SQL_COL_PROMPT_PAY1 + "]," +
                "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                "[" + SQL_COL_PROMPT_PAY2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                "[" + SQL_COL_LATE_BASE + "]," +
                "[" + SQL_COL_RENT_LEGAL + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                "[" + SQL_COL_PROPOSED_RENT + "]," +
                "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                "[" + SQL_COL_M_B_R + "]," +
                "[" + SQL_COL_TOTAL_CHARGES + "]," +
                "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                "[" + SQL_COL_LATE_FEE + "]," +
                "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                "[" + SQL_COL_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                "[" + SQL_COL_LATE_FEE_YES + "]," +
                "[" + SQL_COL_LATE_FEE_NO + "]," +
                "[" + SQL_COL_SUCCEEDED + "]," +
                "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                "[" + SQL_COL_421A + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                "[" + SQL_COL_LEGAL_FEE + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                "[" + SQL_COL_REPLACING_TENANT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NAME + "]," +
                "[" + SQL_COL_APARTMENT + "]," +
                "[" + SQL_COL_DUE_DATE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                "[" + SQL_COL_SECURITY_BUILDING + "]," +
                "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                "[" + SQL_COL_PAGER + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                "[" + SQL_COL_CELL2 + "]," +
                "[" + SQL_COL_EMAIL1 + "]," +
                "[" + SQL_COL_EMAIL2 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                "[" + SQL_COL_ADD_NAME + "]," +
                "[" + SQL_COL_BANK + "]," +
                "[" + SQL_COL_INTEREST_INFO + "]," +
                "[" + SQL_COL_COMMENT + "]," +
                "[" + SQL_COL_SENIORCIT + "]," +
                "[" + SQL_COL_HOME_PHONE2 + "]," +
                "[" + SQL_COL_BUS_PHONE1 + "]," +
                "[" + SQL_COL_BUS_PHONE2 + "]," +
                "[" + SQL_COL_ALT_BILL_NAME + "]," +
                "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                "[" + SQL_COL_S_S_NUMBER1 + "]," +
                "[" + SQL_COL_S_S_NUMBER2 + "]," +
                "[" + SQL_COL_HOME_PHONE1 + "]," +
                "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                "[" + SQL_COL_USE_A_C_H +"]," +
                "[" + SQL_COL_PAYMENT_DAY + "]," +
                "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                "[" + SQL_COL_EMPLOYEE_OCCUPIED +"]," +
                "[" + SQL_COL_XRF_TESTED + "]," +
                "[" + SQL_COL_XRF_EXEMPT + "]," +
                "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                "[" + SQL_COL_XRF_EXEMPT_DATE + "]," + 
                "[" + SQL_COL_DIRECT_PAY + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                " FROM " + TABLE +
                " WHERE " + SQL_COL_SEQUENCE_NUMBER + " = ?;";

        static string SQL_LOAD_Non_ACH = "SELECT " +
                "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                "[" + SQL_COL_MOVE_IN_DATE + "]," +
                "[" + SQL_COL_LEASE_START_DATE + "]," +
                "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                "[" + SQL_COL_LAST_PAID_DATE + "]," +
                "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                "[" + SQL_COL_EXEMPT_DATE + "]," +
                "[" + SQL_COL_OPT_TENANT1 + "]," +
                "[" + SQL_COL_OPT_TENANT2 + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                "[" + SQL_COL_ROOMS + "]," +
                "[" + SQL_COL_SQUARE_FEET + "]," +
                "[" + SQL_COL_SHARES + "]," +
                "[" + SQL_COL_SEC_MULTIPLE + "]," +
                "[" + SQL_COL_RENT + "]," +
                "[" + SQL_COL_SECURITY + "]," +
                "[" + SQL_COL_ARREARS + "]," +
                "[" + SQL_COL_PAID + "]," +
                "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                "[" + SQL_COL_PROMPT_PAY1 + "]," +
                "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                "[" + SQL_COL_PROMPT_PAY2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                "[" + SQL_COL_LATE_BASE + "]," +
                "[" + SQL_COL_RENT_LEGAL + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                "[" + SQL_COL_PROPOSED_RENT + "]," +
                "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                "[" + SQL_COL_M_B_R + "]," +
                "[" + SQL_COL_TOTAL_CHARGES + "]," +
                "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                "[" + SQL_COL_LATE_FEE + "]," +
                "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                "[" + SQL_COL_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                "[" + SQL_COL_LATE_FEE_YES + "]," +
                "[" + SQL_COL_LATE_FEE_NO + "]," +
                "[" + SQL_COL_SUCCEEDED + "]," +
                "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                "[" + SQL_COL_421A + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                "[" + SQL_COL_LEGAL_FEE + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                "[" + SQL_COL_REPLACING_TENANT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NAME + "]," +
                "[" + SQL_COL_APARTMENT + "]," +
                "[" + SQL_COL_DUE_DATE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                "[" + SQL_COL_SECURITY_BUILDING + "]," +
                "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                "[" + SQL_COL_PAGER + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                "[" + SQL_COL_CELL2 + "]," +
                "[" + SQL_COL_EMAIL1 + "]," +
                "[" + SQL_COL_EMAIL2 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                "[" + SQL_COL_ADD_NAME + "]," +
                "[" + SQL_COL_BANK + "]," +
                "[" + SQL_COL_INTEREST_INFO + "]," +
                "[" + SQL_COL_COMMENT + "]," +
                "[" + SQL_COL_SENIORCIT + "]," +
                "[" + SQL_COL_HOME_PHONE2 + "]," +
                "[" + SQL_COL_BUS_PHONE1 + "]," +
                "[" + SQL_COL_BUS_PHONE2 + "]," +
                "[" + SQL_COL_ALT_BILL_NAME + "]," +
                "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                "[" + SQL_COL_S_S_NUMBER1 + "]," +
                "[" + SQL_COL_S_S_NUMBER2 + "]," +
                "[" + SQL_COL_HOME_PHONE1 + "]," +
                "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                "[" + SQL_COL_USE_A_C_H + "]," +
                "[" + SQL_COL_PAYMENT_DAY + "]," +
                "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                "[" + SQL_COL_XRF_TESTED + "]," +
                "[" + SQL_COL_XRF_EXEMPT + "]," +
                "[" + SQL_COL_XRF_TESTED_DATE + "]," +
                "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                "[" + SQL_COL_DIRECT_PAY + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                " FROM " + TABLE +
               " WHERE [" + SQL_COL_USE_A_C_H + "] = 0" +
               " AND ([" + SQL_COL_EMAIL1 + "] != ''"+
               " OR [" + SQL_COL_EMAIL2 + "] != '')";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                "[" + SQL_COL_MOVE_IN_DATE + "]," +
                "[" + SQL_COL_LEASE_START_DATE + "]," +
                "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                "[" + SQL_COL_LAST_PAID_DATE + "]," +
                "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                "[" + SQL_COL_EXEMPT_DATE + "]," +
                "[" + SQL_COL_OPT_TENANT1 + "]," +
                "[" + SQL_COL_OPT_TENANT2 + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                "[" + SQL_COL_ROOMS + "]," +
                "[" + SQL_COL_SQUARE_FEET + "]," +
                "[" + SQL_COL_SHARES + "]," +
                "[" + SQL_COL_SEC_MULTIPLE + "]," +
                "[" + SQL_COL_RENT + "]," +
                "[" + SQL_COL_SECURITY + "]," +
                "[" + SQL_COL_ARREARS + "]," +
                "[" + SQL_COL_PAID + "]," +
                "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                "[" + SQL_COL_PROMPT_PAY1 + "]," +
                "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                "[" + SQL_COL_PROMPT_PAY2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                "[" + SQL_COL_LATE_BASE + "]," +
                "[" + SQL_COL_RENT_LEGAL + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                "[" + SQL_COL_PROPOSED_RENT + "]," +
                "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                "[" + SQL_COL_M_B_R + "]," +
                "[" + SQL_COL_TOTAL_CHARGES + "]," +
                "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                "[" + SQL_COL_LATE_FEE + "]," +
                "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                "[" + SQL_COL_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                "[" + SQL_COL_LATE_FEE_YES + "]," +
                "[" + SQL_COL_LATE_FEE_NO + "]," +
                "[" + SQL_COL_SUCCEEDED + "]," +
                "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                "[" + SQL_COL_421A + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                "[" + SQL_COL_LEGAL_FEE + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                "[" + SQL_COL_REPLACING_TENANT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NAME + "]," +
                "[" + SQL_COL_APARTMENT + "]," +
                "[" + SQL_COL_DUE_DATE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                //"[" + SQL_COL_TEMP1 + "]," +
                //"[" + SQL_COL_TEMP2 + "]," +
                "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                "[" + SQL_COL_SECURITY_BUILDING + "]," +
                "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                "[" + SQL_COL_PAGER + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                "[" + SQL_COL_CELL2 + "]," +
                "[" + SQL_COL_EMAIL1 + "]," +
                "[" + SQL_COL_EMAIL2 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                "[" + SQL_COL_ADD_NAME + "]," +
                "[" + SQL_COL_BANK + "]," +
                "[" + SQL_COL_INTEREST_INFO + "]," +
                "[" + SQL_COL_COMMENT + "]," +
                "[" + SQL_COL_SENIORCIT + "]," +
                "[" + SQL_COL_HOME_PHONE2 + "]," +
                "[" + SQL_COL_BUS_PHONE1 + "]," +
                "[" + SQL_COL_BUS_PHONE2 + "]," +
                "[" + SQL_COL_ALT_BILL_NAME + "]," +
                "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                "[" + SQL_COL_S_S_NUMBER1 + "]," +
                "[" + SQL_COL_S_S_NUMBER2 + "]," +
                "[" + SQL_COL_HOME_PHONE1 + "]," +
                "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                "[" + SQL_COL_USE_A_C_H + "]," +
                "[" + SQL_COL_PAYMENT_DAY + "]," +
                "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                "[" + SQL_COL_EMPLOYEE_OCCUPIED +"]," +
                "[" + SQL_COL_XRF_TESTED + "]," +
                "[" + SQL_COL_XRF_EXEMPT + "]," +
                "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                "[" + SQL_COL_DIRECT_PAY + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +

                " FROM " + TABLE +
                " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                "[" + SQL_COL_MOVE_IN_DATE + "]," +
                "[" + SQL_COL_LEASE_START_DATE + "]," +
                "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                "[" + SQL_COL_LAST_PAID_DATE + "]," +
                "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                "[" + SQL_COL_EXEMPT_DATE + "]," +
                "[" + SQL_COL_OPT_TENANT1 + "]," +
                "[" + SQL_COL_OPT_TENANT2 + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                "[" + SQL_COL_ROOMS + "]," +
                "[" + SQL_COL_SQUARE_FEET + "]," +
                "[" + SQL_COL_SHARES + "]," +
                "[" + SQL_COL_SEC_MULTIPLE + "]," +
                "[" + SQL_COL_RENT + "]," +
                "[" + SQL_COL_SECURITY + "]," +
                "[" + SQL_COL_ARREARS + "]," +
                "[" + SQL_COL_PAID + "]," +
                "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                "[" + SQL_COL_PROMPT_PAY1 + "]," +
                "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                "[" + SQL_COL_PROMPT_PAY2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                "[" + SQL_COL_LATE_BASE + "]," +
                "[" + SQL_COL_RENT_LEGAL + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                "[" + SQL_COL_PROPOSED_RENT + "]," +
                "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                "[" + SQL_COL_M_B_R + "]," +
                "[" + SQL_COL_TOTAL_CHARGES + "]," +
                "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                "[" + SQL_COL_LATE_FEE + "]," +
                "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                "[" + SQL_COL_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                "[" + SQL_COL_LATE_FEE_YES + "]," +
                "[" + SQL_COL_LATE_FEE_NO + "]," +
                "[" + SQL_COL_SUCCEEDED + "]," +
                "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                "[" + SQL_COL_421A + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                "[" + SQL_COL_LEGAL_FEE + "]," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                "[" + SQL_COL_REPLACING_TENANT + "]," +
                "[" + SQL_COL_CLIENT_ID + "]," +
                "[" + SQL_COL_BUILDING_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NUMBER + "]," +
                "[" + SQL_COL_TENANT_NAME + "]," +
                "[" + SQL_COL_APARTMENT + "]," +
                "[" + SQL_COL_DUE_DATE + "]," +
                "[" + SQL_COL_TYPE + "]," +
                //"[" + SQL_COL_TEMP1 + "]," +
                //"[" + SQL_COL_TEMP2 + "]," +
                "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                "[" + SQL_COL_SECURITY_BUILDING + "]," +
                "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                "[" + SQL_COL_PAGER + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                "[" + SQL_COL_CELL2 + "]," +
                "[" + SQL_COL_EMAIL1 + "]," +
                "[" + SQL_COL_EMAIL2 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                "[" + SQL_COL_ADD_NAME + "]," +
                "[" + SQL_COL_BANK + "]," +
                "[" + SQL_COL_INTEREST_INFO + "]," +
                "[" + SQL_COL_COMMENT + "]," +
                "[" + SQL_COL_SENIORCIT + "]," +
                "[" + SQL_COL_HOME_PHONE2 + "]," +
                "[" + SQL_COL_BUS_PHONE1 + "]," +
                "[" + SQL_COL_BUS_PHONE2 + "]," +
                "[" + SQL_COL_ALT_BILL_NAME + "]," +
                "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                "[" + SQL_COL_LEGAL_CODE + "]," +
                "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                "[" + SQL_COL_S_S_NUMBER1 + "]," +
                "[" + SQL_COL_S_S_NUMBER2 + "]," +
                "[" + SQL_COL_HOME_PHONE1 + "]," +
                "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                "[" + SQL_COL_USE_A_C_H + "]," +
                "[" + SQL_COL_PAYMENT_DAY + "]," +
                "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                "[" + SQL_COL_EMPLOYEE_OCCUPIED +"]," +
                "[" + SQL_COL_XRF_TESTED + "]," +
                "[" + SQL_COL_XRF_EXEMPT + "]," +
                "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                "[" + SQL_COL_DIRECT_PAY + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +

                ") VALUES ('?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                            "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                            "?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', " +
                            "'?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,'?' ,'?' , '?', ?, '?', ?);";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_FUTURE_DATE_1 + "]='?'," +
                "[" + SQL_COL_FUTURE_DATE_2 + "]='?'," +
                "[" + SQL_COL_FUTURE_DATE_3 + "]='?'," +
                "[" + SQL_COL_MOVE_IN_DATE + "]='?'," +
                "[" + SQL_COL_LEASE_START_DATE + "]='?'," +
                "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]='?'," +
                "[" + SQL_COL_LAST_PAID_DATE + "]='?'," +
                "[" + SQL_COL_DHCR_DATE_ONE + "]='?'," +
                "[" + SQL_COL_DHCR_DATE_TWO + "]='?'," +
                "[" + SQL_COL_EXEMPT_DATE + "]='?'," +
                "[" + SQL_COL_OPT_TENANT1 + "]=?," +
                "[" + SQL_COL_OPT_TENANT2 + "]=?," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]=?," +
                "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]=?," +
                "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]=?," +
                "[" + SQL_COL_ROOMS + "]=?," +
                "[" + SQL_COL_SQUARE_FEET + "]=?," +
                "[" + SQL_COL_SHARES + "]=?," +
                "[" + SQL_COL_SEC_MULTIPLE + "]=?," +
                "[" + SQL_COL_RENT + "]=?," +
                "[" + SQL_COL_SECURITY + "]=?," +
                "[" + SQL_COL_ARREARS + "]=?," +
                "[" + SQL_COL_PAID + "]=?," +
                "[" + SQL_COL_FUTURE_RENT_1 + "]=?," +
                "[" + SQL_COL_FUTURE_RENT_2 + "]=?," +
                "[" + SQL_COL_PROMPT_PAY1 + "]=?," +
                "[" + SQL_COL_PROPOSED_RENT2 + "]=?," +
                "[" + SQL_COL_PROMPT_PAY2 + "]=?," +
                "[" + SQL_COL_PROPOSED_LEGAL2 + "]=?," +
                "[" + SQL_COL_PROPOSED_LEGAL + "]=?," +
                "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]=?," +
                "[" + SQL_COL_LATE_BASE + "]=?," +
                "[" + SQL_COL_RENT_LEGAL + "]=?," +
                "[" + SQL_COL_FUTURE_LEGAL_1 + "]=?," +
                "[" + SQL_COL_FUTURE_LEGAL_2 + "]=?," +
                "[" + SQL_COL_FUTURE_LEGAL_3 + "]=?," +
                "[" + SQL_COL_PROPOSED_RENT + "]=?," +
                "[" + SQL_COL_FUTURE_RENT_3 + "]=?," +
                "[" + SQL_COL_M_B_R + "]=?," +
                "[" + SQL_COL_TOTAL_CHARGES + "]=?," +
                "[" + SQL_COL_DHCR_RENT_ONE + "]=?," +
                "[" + SQL_COL_DHCR_RENT_TWO + "]=?," +
                "[" + SQL_COL_LATE_FEE + "]=?," +
                "[" + SQL_COL_NEW_TENANT_FLAG + "]=?," +
                "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]=?," +
                "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]=?," +
                "[" + SQL_COL_LEASE_LETTER_FLAG + "]=?," +
                "[" + SQL_COL_LOW_RENT_FLAG + "]=?," +
                "[" + SQL_COL_DHCR_STOVE_ONE + "]=?," +
                "[" + SQL_COL_EXEMPT_SEC11243 + "]=?," +
                "[" + SQL_COL_EXEMPT_OTHER + "]=?," +
                "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]=?," +
                "[" + SQL_COL_EXEMPT_EXP421A + "]=?," +
                "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]=?," +
                "[" + SQL_COL_EXEMPT421A_INCOME + "]=?," +
                "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]=?," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]=?," +
                "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]=?," +
                "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]=?," +
                "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]=?," +
                "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]=?," +
                "[" + SQL_COL_RENT_OVERCHARGE + "]=?," +
                "[" + SQL_COL_FAIR_MARKET_APPEAL + "]=?," +
                "[" + SQL_COL_TRANSIENT_OCCUPANT + "]=?," +
                "[" + SQL_COL_OWNER_OCCUPIED + "]=?," +
                "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]=?," +
                "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]=?," +
                "[" + SQL_COL_LEGAL_FEE_NO + "]=?," +
                "[" + SQL_COL_LATE_FEE_YES + "]=?," +
                "[" + SQL_COL_LATE_FEE_NO + "]=?," +
                "[" + SQL_COL_SUCCEEDED + "]=?," +
                "[" + SQL_COL_SECOND_SUCCESSION + "]=?," +
                "[" + SQL_COL_421A + "]=?," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]=?," +
                "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]=?," +
                "[" + SQL_COL_DHCR_A_C_TWO + "]=?," +
                "[" + SQL_COL_DHCR_WINDOWS_TWO + "]=?," +
                "[" + SQL_COL_DHCR_OTHER_TWO + "]=?," +
                "[" + SQL_COL_LEGAL_FEE + "]=?," +
                "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]=?," +
                "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]=?," +
                "[" + SQL_COL_DHCR_A_C_ONE + "]=?," +
                "[" + SQL_COL_DHCR_WINDOWS_ONE + "]=?," +
                "[" + SQL_COL_DHCR_OTHER_ONE + "]=?," +
                "[" + SQL_COL_DHCR_STOVE_TWO + "]=?," +
                "[" + SQL_COL_REPLACING_TENANT + "]=?," +
                "[" + SQL_COL_CLIENT_ID + "]=?," +
                "[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
                "[" + SQL_COL_TENANT_NUMBER + "]='?'," +
                "[" + SQL_COL_TENANT_NAME + "]='?'," +
                "[" + SQL_COL_APARTMENT + "]='?'," +
                "[" + SQL_COL_DUE_DATE + "]='?'," +
                "[" + SQL_COL_TYPE + "]='?'," +
                //"[" + SQL_COL_TEMP1 + "]='?'," +
                //"[" + SQL_COL_TEMP2 + "]='?'," +
                "[" + SQL_COL_SECURITY_ACCOUNT + "]='?'," +
                "[" + SQL_COL_SECURITY_BUILDING + "]='?'," +
                "[" + SQL_COL_EXEMPT_OTHER_DESC + "]='?'," +
                "[" + SQL_COL_PAGER + "]='?'," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]='?'," +
                "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]='?'," +
                "[" + SQL_COL_CELL2 + "]='?'," +
                "[" + SQL_COL_EMAIL1 + "]='?'," +
                "[" + SQL_COL_EMAIL2 + "]='?'," +
                "[" + SQL_COL_ALT_BILL_ADD3 + "]='?'," +
                "[" + SQL_COL_ADD_NAME + "]='?'," +
                "[" + SQL_COL_BANK + "]='?'," +
                "[" + SQL_COL_INTEREST_INFO + "]='?'," +
                "[" + SQL_COL_COMMENT + "]='?'," +
                "[" + SQL_COL_SENIORCIT + "]='?'," +
                "[" + SQL_COL_HOME_PHONE2 + "]='?'," +
                "[" + SQL_COL_BUS_PHONE1 + "]='?'," +
                "[" + SQL_COL_BUS_PHONE2 + "]='?'," +
                "[" + SQL_COL_ALT_BILL_NAME + "]='?'," +
                "[" + SQL_COL_ALT_BILL_ADD1 + "]='?'," +
                "[" + SQL_COL_ALT_BILL_ADD2 + "]='?'," +
                "[" + SQL_COL_LEGAL_CODE + "]='?'," +
                "[" + SQL_COL_LEGAL_CODE_2 + "]='?'," +
                "[" + SQL_COL_ADDITIONAL_NAME + "]='?'," +
                "[" + SQL_COL_S_S_NUMBER1 + "]='?'," +
                "[" + SQL_COL_S_S_NUMBER2 + "]='?'," +
                "[" + SQL_COL_HOME_PHONE1 + "]='?'," +
                "[" + SQL_COL_DECONTROLLED_LEGAL + "]=?," +
                "[" + SQL_COL_LATE_FEE_PERCENT + "]=?," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]='?'," +
                "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]='?'," +
                "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]=?," +
                "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]=?," +
                "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]=?," +
                "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]=?," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]=?," +
                "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]=?," +
                "[" + SQL_COL_USE_A_C_H +"]=?," +
                "[" + SQL_COL_PAYMENT_DAY + "]=?," +
                "[" + SQL_COL_SAVINGS_ACCOUNT + "]=?," +
                "[" + SQL_COL_EMPLOYEE_OCCUPIED +"]=?," +
                "[" + SQL_COL_XRF_TESTED + "]=?," +
                "[" + SQL_COL_XRF_EXEMPT + "]=?," +
                "[" + SQL_COL_XRF_TESTED_DATE + "]='?'," +
                "[" + SQL_COL_XRF_EXEMPT_DATE + "]='?'," +
                "[" + SQL_COL_DIRECT_PAY + "]=?," +
                "[" + SQL_COL_XRF_WORK_COMPLETE + "]=?," +
                "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]='?'," +
                "[" + SQL_COL_PROPOSED_RENT2_1 + "]=?" +
                " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?";

        static string SQL_UPDATE_PAYMENT = "UPDATE " + TABLE + " SET " +
               "[" + SQL_COL_LAST_PAID_DATE + "]='?'," +
               "[" + SQL_COL_SECURITY + "]=?," +
               "[" + SQL_COL_LEGAL_CODE + "]='?'," +
               "[" + SQL_COL_PAID + "]=?," +
               "[" + SQL_COL_TOTAL_CHARGES + "]=?," +
               "[" + SQL_COL_M_B_R + "]=?" +
               " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?;";

        static string SQL_LOAD_BY_BUILDING_TENANT = "SELECT " +
                                                     "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                                     "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                                     "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                                     "[" + SQL_COL_LEASE_START_DATE + "]," +
                                                     "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                                     "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                                     "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                                     "[" + SQL_COL_EXEMPT_DATE + "]," +
                                                     "[" + SQL_COL_OPT_TENANT1 + "]," +
                                                     "[" + SQL_COL_OPT_TENANT2 + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                                     "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                                     "[" + SQL_COL_ROOMS + "]," +
                                                     "[" + SQL_COL_SQUARE_FEET + "]," +
                                                     "[" + SQL_COL_SHARES + "]," +
                                                     "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                                     "[" + SQL_COL_RENT + "]," +
                                                     "[" + SQL_COL_SECURITY + "]," +
                                                     "[" + SQL_COL_ARREARS + "]," +
                                                     "[" + SQL_COL_PAID + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                                     "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                                     "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                                     "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                                     "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                                     "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                                     "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                                     "[" + SQL_COL_LATE_BASE + "]," +
                                                     "[" + SQL_COL_RENT_LEGAL + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                                     "[" + SQL_COL_PROPOSED_RENT + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                                     "[" + SQL_COL_M_B_R + "]," +
                                                     "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                                     "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                                     "[" + SQL_COL_LATE_FEE + "]," +
                                                     "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                                     "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                                     "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                                     "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                                     "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                                     "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                                     "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                                     "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                                     "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                                     "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                                     "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                                     "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                                     "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                                     "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                                     "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                                     "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                                     "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                                     "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                                     "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                                     "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                                     "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                                     "[" + SQL_COL_LATE_FEE_YES + "]," +
                                                     "[" + SQL_COL_LATE_FEE_NO + "]," +
                                                     "[" + SQL_COL_SUCCEEDED + "]," +
                                                     "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                                     "[" + SQL_COL_421A + "]," +
                                                     "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                                     "[" + SQL_COL_LEGAL_FEE + "]," +
                                                     "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                                     "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                                     "[" + SQL_COL_REPLACING_TENANT + "]," +
                                                     "[" + SQL_COL_CLIENT_ID + "]," +
                                                     "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                                     "[" + SQL_COL_TENANT_NUMBER + "]," +
                                                     "[" + SQL_COL_TENANT_NAME + "]," +
                                                     "[" + SQL_COL_APARTMENT + "]," +
                                                     "[" + SQL_COL_DUE_DATE + "]," +
                                                     "[" + SQL_COL_TYPE + "]," +
                                                     //"[" + SQL_COL_TEMP1 + "]," +
                                                     //"[" + SQL_COL_TEMP2 + "]," +
                                                     "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                                     "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                                     "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                                     "[" + SQL_COL_PAGER + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                                     "[" + SQL_COL_CELL2 + "]," +
                                                     "[" + SQL_COL_EMAIL1 + "]," +
                                                     "[" + SQL_COL_EMAIL2 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                                     "[" + SQL_COL_ADD_NAME + "]," +
                                                     "[" + SQL_COL_BANK + "]," +
                                                     "[" + SQL_COL_INTEREST_INFO + "]," +
                                                     "[" + SQL_COL_COMMENT + "]," +
                                                     "[" + SQL_COL_SENIORCIT + "]," +
                                                     "[" + SQL_COL_HOME_PHONE2 + "]," +
                                                     "[" + SQL_COL_BUS_PHONE1 + "]," +
                                                     "[" + SQL_COL_BUS_PHONE2 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                                     "[" + SQL_COL_LEGAL_CODE + "]," +
                                                     "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                                     "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                                     "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                                     "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                                     "[" + SQL_COL_HOME_PHONE1 + "]," +
                                                     "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                                     "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                                     "[" + SQL_COL_USE_A_C_H + "]," +
                                                     "[" + SQL_COL_PAYMENT_DAY + "]," +
                                                     "[" + SQL_COL_SAVINGS_ACCOUNT + "],"  +
                                                     "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                                     "[" + SQL_COL_XRF_TESTED + "]," +
                                                     "[" + SQL_COL_XRF_EXEMPT + "]," +
                                                    "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                                    "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                                    "[" + SQL_COL_DIRECT_PAY + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                                    "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                                     " FROM " + TABLE +
                                                     " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" +
                                                     " AND [" + SQL_COL_TENANT_NUMBER + "] = '?'";

        static string SQL_LOAD_BY_BUILDING = "SELECT " +
                                                     "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                                     "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                                     "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                                     "[" + SQL_COL_LEASE_START_DATE + "]," +
                                                     "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                                     "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                                     "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                                     "[" + SQL_COL_EXEMPT_DATE + "]," +
                                                     "[" + SQL_COL_OPT_TENANT1 + "]," +
                                                     "[" + SQL_COL_OPT_TENANT2 + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                                     "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                                     "[" + SQL_COL_ROOMS + "]," +
                                                     "[" + SQL_COL_SQUARE_FEET + "]," +
                                                     "[" + SQL_COL_SHARES + "]," +
                                                     "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                                     "[" + SQL_COL_RENT + "]," +
                                                     "[" + SQL_COL_SECURITY + "]," +
                                                     "[" + SQL_COL_ARREARS + "]," +
                                                     "[" + SQL_COL_PAID + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                                     "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                                     "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                                     "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                                     "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                                     "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                                     "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                                     "[" + SQL_COL_LATE_BASE + "]," +
                                                     "[" + SQL_COL_RENT_LEGAL + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                                     "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                                     "[" + SQL_COL_PROPOSED_RENT + "]," +
                                                     "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                                     "[" + SQL_COL_M_B_R + "]," +
                                                     "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                                     "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                                     "[" + SQL_COL_LATE_FEE + "]," +
                                                     "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                                     "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                                     "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                                     "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                                     "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                                     "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                                     "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                                     "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                                     "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                                     "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                                     "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                                     "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                                     "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                                     "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                                     "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                                     "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                                     "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                                     "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                                     "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                                     "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                                     "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                                     "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                                     "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                                     "[" + SQL_COL_LATE_FEE_YES + "]," +
                                                     "[" + SQL_COL_LATE_FEE_NO + "]," +
                                                     "[" + SQL_COL_SUCCEEDED + "]," +
                                                     "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                                     "[" + SQL_COL_421A + "]," +
                                                     "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                                     "[" + SQL_COL_LEGAL_FEE + "]," +
                                                     "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                                     "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                                     "[" + SQL_COL_REPLACING_TENANT + "]," +
                                                     "[" + SQL_COL_CLIENT_ID + "]," +
                                                     "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                                     "[" + SQL_COL_TENANT_NUMBER + "]," +
                                                     "[" + SQL_COL_TENANT_NAME + "]," +
                                                     "[" + SQL_COL_APARTMENT + "]," +
                                                     "[" + SQL_COL_DUE_DATE + "]," +
                                                     "[" + SQL_COL_TYPE + "]," +
                                                     //"[" + SQL_COL_TEMP1 + "]," +
                                                     //"[" + SQL_COL_TEMP2 + "]," +
                                                     "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                                     "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                                     "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                                     "[" + SQL_COL_PAGER + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                                     "[" + SQL_COL_CELL2 + "]," +
                                                     "[" + SQL_COL_EMAIL1 + "]," +
                                                     "[" + SQL_COL_EMAIL2 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                                     "[" + SQL_COL_ADD_NAME + "]," +
                                                     "[" + SQL_COL_BANK + "]," +
                                                     "[" + SQL_COL_INTEREST_INFO + "]," +
                                                     "[" + SQL_COL_COMMENT + "]," +
                                                     "[" + SQL_COL_SENIORCIT + "]," +
                                                     "[" + SQL_COL_HOME_PHONE2 + "]," +
                                                     "[" + SQL_COL_BUS_PHONE1 + "]," +
                                                     "[" + SQL_COL_BUS_PHONE2 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                                     "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                                     "[" + SQL_COL_LEGAL_CODE + "]," +
                                                     "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                                     "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                                     "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                                     "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                                     "[" + SQL_COL_HOME_PHONE1 + "]," +
                                                     "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                                     "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                                     "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                                     "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                                     "[" + SQL_COL_USE_A_C_H + "]," +
                                                     "[" + SQL_COL_PAYMENT_DAY + "]," +
                                                     "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                                     "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                                     "[" + SQL_COL_XRF_TESTED + "]," +
                                                     "[" + SQL_COL_XRF_EXEMPT + "]," +
                                                    "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                                    "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                                    "[" + SQL_COL_DIRECT_PAY + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                                    "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                                     " FROM " + TABLE +
                                                     " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" +
                                                     " ORDER BY [" + SQL_COL_TENANT_NUMBER + "]";


        static string SQL_LOAD_FOR_STATEMENTS = "SELECT " +
                                                   "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                                   "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                                   "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                                   "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                                   "[" + SQL_COL_LEASE_START_DATE + "]," +
                                                   "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                                   "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                                   "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                                   "[" + SQL_COL_EXEMPT_DATE + "]," +
                                                   "[" + SQL_COL_OPT_TENANT1 + "]," +
                                                   "[" + SQL_COL_OPT_TENANT2 + "]," +
                                                   "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                                   "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                                   "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                                   "[" + SQL_COL_ROOMS + "]," +
                                                   "[" + SQL_COL_SQUARE_FEET + "]," +
                                                   "[" + SQL_COL_SHARES + "]," +
                                                   "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                                   "[" + SQL_COL_RENT + "]," +
                                                   "[" + SQL_COL_SECURITY + "]," +
                                                   "[" + SQL_COL_ARREARS + "]," +
                                                   "[" + SQL_COL_PAID + "]," +
                                                   "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                                   "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                                   "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                                   "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                                   "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                                   "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                                   "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                                   "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                                   "[" + SQL_COL_LATE_BASE + "]," +
                                                   "[" + SQL_COL_RENT_LEGAL + "]," +
                                                   "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                                   "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                                   "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                                   "[" + SQL_COL_PROPOSED_RENT + "]," +
                                                   "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                                   "[" + SQL_COL_M_B_R + "]," +
                                                   "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                                   "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                                   "[" + SQL_COL_LATE_FEE + "]," +
                                                   "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                                   "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                                   "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                                   "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                                   "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                                   "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                                   "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                                   "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                                   "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                                   "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                                   "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                                   "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                                   "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                                   "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                                   "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                                   "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                                   "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                                   "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                                   "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                                   "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                                   "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                                   "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                                   "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                                   "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                                   "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                                   "[" + SQL_COL_LATE_FEE_YES + "]," +
                                                   "[" + SQL_COL_LATE_FEE_NO + "]," +
                                                   "[" + SQL_COL_SUCCEEDED + "]," +
                                                   "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                                   "[" + SQL_COL_421A + "]," +
                                                   "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                                   "[" + SQL_COL_LEGAL_FEE + "]," +
                                                   "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                                   "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                                   "[" + SQL_COL_REPLACING_TENANT + "]," +
                                                   "[" + SQL_COL_CLIENT_ID + "]," +
                                                   "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                                   "[" + SQL_COL_TENANT_NUMBER + "]," +
                                                   "[" + SQL_COL_TENANT_NAME + "]," +
                                                   "[" + SQL_COL_APARTMENT + "]," +
                                                   "[" + SQL_COL_DUE_DATE + "]," +
                                                   "[" + SQL_COL_TYPE + "]," +
                                                   //"[" + SQL_COL_TEMP1 + "]," +
                                                   //"[" + SQL_COL_TEMP2 + "]," +
                                                   "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                                   "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                                   "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                                   "[" + SQL_COL_PAGER + "]," +
                                                   "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                                   "[" + SQL_COL_CELL2 + "]," +
                                                   "[" + SQL_COL_EMAIL1 + "]," +
                                                   "[" + SQL_COL_EMAIL2 + "]," +
                                                   "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                                   "[" + SQL_COL_ADD_NAME + "]," +
                                                   "[" + SQL_COL_BANK + "]," +
                                                   "[" + SQL_COL_INTEREST_INFO + "]," +
                                                   "[" + SQL_COL_COMMENT + "]," +
                                                   "[" + SQL_COL_SENIORCIT + "]," +
                                                   "[" + SQL_COL_HOME_PHONE2 + "]," +
                                                   "[" + SQL_COL_BUS_PHONE1 + "]," +
                                                   "[" + SQL_COL_BUS_PHONE2 + "]," +
                                                   "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                                   "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                                   "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                                   "[" + SQL_COL_LEGAL_CODE + "]," +
                                                   "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                                   "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                                   "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                                   "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                                   "[" + SQL_COL_HOME_PHONE1 + "]," +
                                                   "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                                   "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                                   "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                                   "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                                   "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                                   "[" + SQL_COL_USE_A_C_H + "]," +
                                                   "[" + SQL_COL_PAYMENT_DAY + "]," +
                                                   "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                                   "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                                   "[" + SQL_COL_XRF_TESTED + "]," +
                                                   "[" + SQL_COL_XRF_EXEMPT + "]," +
                                                    "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                                    "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                                    "[" + SQL_COL_DIRECT_PAY + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                                    "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +

                                                   " FROM " + TABLE; //+
                                                   /*
                                                   " WHERE [" + SQL_COL_BUILDING_NUMBER + "] >= '?'" +
                                                   " AND [" + SQL_COL_BUILDING_NUMBER + "] <= '?'" +
                                                   " AND [" + SQL_COL_NEW_TENANT_FLAG + "] = 0 " +
                                                   " AND [" + SQL_COL_TYPE + "] <> '#'" +
                                                   " AND [" + SQL_COL_TENANT_NAME + "] NOT LIKE ('*%')" +
                                                   " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";
                                                   */

        static string SQL_LOAD_FOR_STATEMENTS_ALL = " WHERE [" + SQL_COL_BUILDING_NUMBER + "] >= '?'" +
                                                    " AND [" + SQL_COL_BUILDING_NUMBER + "] <= '?'" +
                                                    " AND [" + SQL_COL_NEW_TENANT_FLAG + "] = 0 " +
                                                    " AND [" + SQL_COL_TYPE + "] <> '#'" +
                                                    " AND [" + SQL_COL_TENANT_NAME + "] NOT LIKE ('*%')" +
                                                    " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";


        static string SQL_LOAD_FOR_STATEMENTS_ONE = " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] IN (<keys>)" +
                                                    " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";


        static string SQL_LOAD_FOR_BATCH = "SELECT " +
                                           "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                           "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                           "[" + SQL_COL_LEASE_START_DATE + "]," +
                                           "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                           "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                           "[" + SQL_COL_EXEMPT_DATE + "]," +
                                           "[" + SQL_COL_OPT_TENANT1 + "]," +
                                           "[" + SQL_COL_OPT_TENANT2 + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                           "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                           "[" + SQL_COL_ROOMS + "]," +
                                           "[" + SQL_COL_SQUARE_FEET + "]," +
                                           "[" + SQL_COL_SHARES + "]," +
                                           "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                           "[" + SQL_COL_RENT + "]," +
                                           "[" + SQL_COL_SECURITY + "]," +
                                           "[" + SQL_COL_ARREARS + "]," +
                                           "[" + SQL_COL_PAID + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                           "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                           "[" + SQL_COL_LATE_BASE + "]," +
                                           "[" + SQL_COL_RENT_LEGAL + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                           "[" + SQL_COL_M_B_R + "]," +
                                           "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                           "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                           "[" + SQL_COL_LATE_FEE + "]," +
                                           "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                           "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                           "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                           "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                           "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                           "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                           "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                           "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                           "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                           "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                           "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                           "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                           "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                           "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                           "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                           "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                           "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                           "[" + SQL_COL_LATE_FEE_YES + "]," +
                                           "[" + SQL_COL_LATE_FEE_NO + "]," +
                                           "[" + SQL_COL_SUCCEEDED + "]," +
                                           "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                           "[" + SQL_COL_421A + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                           "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                           "[" + SQL_COL_LEGAL_FEE + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                           "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                           "[" + SQL_COL_REPLACING_TENANT + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NAME + "]," +
                                           "[" + SQL_COL_APARTMENT + "]," +
                                           "[" + SQL_COL_DUE_DATE + "]," +
                                           "[" + SQL_COL_TYPE + "]," +
                                           //"[" + SQL_COL_TEMP1 + "]," +
                                           //"[" + SQL_COL_TEMP2 + "]," +
                                           "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                           "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                           "[" + SQL_COL_PAGER + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                           "[" + SQL_COL_CELL2 + "]," +
                                           "[" + SQL_COL_EMAIL1 + "]," +
                                           "[" + SQL_COL_EMAIL2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                           "[" + SQL_COL_ADD_NAME + "]," +
                                           "[" + SQL_COL_BANK + "]," +
                                           "[" + SQL_COL_INTEREST_INFO + "]," +
                                           "[" + SQL_COL_COMMENT + "]," +
                                           "[" + SQL_COL_SENIORCIT + "]," +
                                           "[" + SQL_COL_HOME_PHONE2 + "]," +
                                           "[" + SQL_COL_BUS_PHONE1 + "]," +
                                           "[" + SQL_COL_BUS_PHONE2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                           "[" + SQL_COL_LEGAL_CODE + "]," +
                                           "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                           "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                           "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                           "[" + SQL_COL_HOME_PHONE1 + "]," +
                                           "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                           "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                           "[" + SQL_COL_USE_A_C_H + "]," +
                                           "[" + SQL_COL_PAYMENT_DAY + "]," +
                                           "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                           "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                           "[" + SQL_COL_XRF_TESTED + "]," +
                                           "[" + SQL_COL_XRF_EXEMPT + "]," +
                                            "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                            "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                            "[" + SQL_COL_DIRECT_PAY + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                            "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                           " FROM " + TABLE +
                                           " WHERE [" + SQL_COL_BUILDING_NUMBER + "] >= '?'" +
                                           " AND [" + SQL_COL_BUILDING_NUMBER + "] <= '?'" +
                                           " AND [" + SQL_COL_TENANT_NAME + "] NOT LIKE ('*%')";


        static string SQL_LOAD_FOR_EOM = "SELECT " +
                                           "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                           "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                           "[" + SQL_COL_LEASE_START_DATE + "]," +
                                           "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                           "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                           "[" + SQL_COL_EXEMPT_DATE + "]," +
                                           "[" + SQL_COL_OPT_TENANT1 + "]," +
                                           "[" + SQL_COL_OPT_TENANT2 + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                           "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                           "[" + SQL_COL_ROOMS + "]," +
                                           "[" + SQL_COL_SQUARE_FEET + "]," +
                                           "[" + SQL_COL_SHARES + "]," +
                                           "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                           "[" + SQL_COL_RENT + "]," +
                                           "[" + SQL_COL_SECURITY + "]," +
                                           "[" + SQL_COL_ARREARS + "]," +
                                           "[" + SQL_COL_PAID + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                           "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                           "[" + SQL_COL_LATE_BASE + "]," +
                                           "[" + SQL_COL_RENT_LEGAL + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                           "[" + SQL_COL_M_B_R + "]," +
                                           "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                           "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                           "[" + SQL_COL_LATE_FEE + "]," +
                                           "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                           "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                           "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                           "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                           "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                           "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                           "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                           "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                           "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                           "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                           "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                           "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                           "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                           "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                           "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                           "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                           "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                           "[" + SQL_COL_LATE_FEE_YES + "]," +
                                           "[" + SQL_COL_LATE_FEE_NO + "]," +
                                           "[" + SQL_COL_SUCCEEDED + "]," +
                                           "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                           "[" + SQL_COL_421A + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                           "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                           "[" + SQL_COL_LEGAL_FEE + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                           "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                           "[" + SQL_COL_REPLACING_TENANT + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NAME + "]," +
                                           "[" + SQL_COL_APARTMENT + "]," +
                                           "[" + SQL_COL_DUE_DATE + "]," +
                                           "[" + SQL_COL_TYPE + "]," +
                                           //"[" + SQL_COL_TEMP1 + "]," +
                                           //"[" + SQL_COL_TEMP2 + "]," +
                                           "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                           "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                           "[" + SQL_COL_PAGER + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                           "[" + SQL_COL_CELL2 + "]," +
                                           "[" + SQL_COL_EMAIL1 + "]," +
                                           "[" + SQL_COL_EMAIL2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                           "[" + SQL_COL_ADD_NAME + "]," +
                                           "[" + SQL_COL_BANK + "]," +
                                           "[" + SQL_COL_INTEREST_INFO + "]," +
                                           "[" + SQL_COL_COMMENT + "]," +
                                           "[" + SQL_COL_SENIORCIT + "]," +
                                           "[" + SQL_COL_HOME_PHONE2 + "]," +
                                           "[" + SQL_COL_BUS_PHONE1 + "]," +
                                           "[" + SQL_COL_BUS_PHONE2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                           "[" + SQL_COL_LEGAL_CODE + "]," +
                                           "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                           "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                           "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                           "[" + SQL_COL_HOME_PHONE1 + "]," +
                                           "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                           "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                           "[" + SQL_COL_USE_A_C_H + "]," +
                                           "[" + SQL_COL_PAYMENT_DAY + "]," +
                                           "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                           "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                           "[" + SQL_COL_XRF_TESTED + "]," +
                                           "[" + SQL_COL_XRF_EXEMPT + "]," +
                                            "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                            "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                            "[" + SQL_COL_DIRECT_PAY + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                            "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                            " FROM " + TABLE +
                                           " WHERE [" + SQL_COL_TYPE + "] <> '#'" +
                                           " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";

        static string SQL_LOAD_FOR_NAMES = " SELECT " +
                                           "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NUMBER + "]," +
                                           "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NAME + "]," +
                                           "[" + SQL_COL_APARTMENT + "]," +
                                           "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                           "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                           "[" + SQL_COL_TYPE + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_RENT + "]," +
                                           "[" + SQL_COL_ARREARS + "]," +
                                           "[" + SQL_COL_PAID + "]," +
                                           "[" + SQL_COL_USE_A_C_H + "]" +
                                           " FROM " + TABLE;

        static string SQL_LOAD_FOR_NAMES_NO_VACANT = " SELECT " +
                                           "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NUMBER + "]," +
                                           "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NAME + "]," +
                                           "[" + SQL_COL_APARTMENT + "]," +
                                           "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                           "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                           "[" + SQL_COL_TYPE + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_RENT + "]," +
                                           "[" + SQL_COL_ARREARS + "]," +
                                           "[" + SQL_COL_PAID + "]," +
                                           "[" + SQL_COL_USE_A_C_H + "]" +
                                           " FROM " + TABLE +
                                           " WHERE [" + SQL_COL_TENANT_NAME + "] NOT LIKE('*%') ";

        static string ORDER_BY_BUILDING = " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";

        static string ORDER_BY_NAME = " ORDER BY [" + SQL_COL_TENANT_NAME + "]";


        static string SQL_LOAD_FOR_DELETION = " SELECT " +
                                              "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                              "[" + SQL_COL_TENANT_NUMBER + "]," +
                                              "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                              "[" + SQL_COL_TENANT_NAME + "]," +
                                              "[" + SQL_COL_APARTMENT + "]" +
                                              " FROM " + TABLE +
                                              " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'";

        static string SQL_LOAD_BY_SEQUENCE_NUMBER = " SELECT " + 
                                                    "[" + SQL_COL_BUILDING_NUMBER + "]," + 
                                                    "[" + SQL_COL_TENANT_NUMBER + "]," + 
                                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," + 
                                                    "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," + 
                                                    "[" + SQL_COL_NEW_TENANT_FLAG + "]," + 
                                                    "[" + SQL_COL_REPLACING_TENANT + "]," + 
                                                    "[" + SQL_COL_TENANT_NAME + "]," + 
                                                    "[" + SQL_COL_APARTMENT + "]," + 
                                                    "[" + SQL_COL_DUE_DATE + "]," + 
                                                    "[" + SQL_COL_TYPE + "]," + 
                                                    "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," + 
                                                    "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," + 
                                                    "[" + SQL_COL_LEASE_LETTER_FLAG + "]," + 
                                                    "[" + SQL_COL_LOW_RENT_FLAG + "]," + "[" + SQL_COL_LEGAL_CODE + "]," + 
                                                    "[" + SQL_COL_LEGAL_CODE_2 + "]," + "[" + SQL_COL_RENT + "]," + 
                                                    "[" + SQL_COL_SECURITY + "]," + "[" + SQL_COL_ARREARS + "]," + 
                                                    "[" + SQL_COL_PAID + "]," + "[" + SQL_COL_FUTURE_RENT_1 + "]," + 
                                                    "[" + SQL_COL_FUTURE_RENT_2 + "]," + 
                                                    "[" + SQL_COL_FUTURE_RENT_3 + "]," + 
                                                    "[" + SQL_COL_M_B_R + "]," + 
                                                    "[" + SQL_COL_FUTURE_DATE_1 + "]," + 
                                                    "[" + SQL_COL_FUTURE_DATE_2 + "]," + 
                                                    "[" + SQL_COL_FUTURE_DATE_3 + "]," + 
                                                    "[" + SQL_COL_ROOMS + "]," + 
                                                    "[" + SQL_COL_SQUARE_FEET + "]," + 
                                                    "[" + SQL_COL_MOVE_IN_DATE + "]," + 
                                                    "[" + SQL_COL_LEASE_START_DATE + "]," + 
                                                    "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," + 
                                                    "[" + SQL_COL_LAST_PAID_DATE + "]," + 
                                                    "[" + SQL_COL_ADDITIONAL_NAME + "]," + 
                                                    "[" + SQL_COL_S_S_NUMBER1 + "]," + 
                                                    "[" + SQL_COL_S_S_NUMBER2 + "]," + 
                                                    "[" + SQL_COL_HOME_PHONE1 + "]," + 
                                                    "[" + SQL_COL_HOME_PHONE2 + "]," + 
                                                    "[" + SQL_COL_BUS_PHONE1 + "]," + 
                                                    "[" + SQL_COL_BUS_PHONE2 + "]," + 
                                                    "[" + SQL_COL_ALT_BILL_NAME + "]," + 
                                                    "[" + SQL_COL_ALT_BILL_ADD1 + "]," + 
                                                    "[" + SQL_COL_ALT_BILL_ADD2 + "]," + 
                                                    "[" + SQL_COL_ALT_BILL_ADD3 + "]," + 
                                                    "[" + SQL_COL_ADD_NAME + "]," + 
                                                    "[" + SQL_COL_BANK + "]," + 
                                                    "[" + SQL_COL_INTEREST_INFO + "]," + 
                                                    "[" + SQL_COL_SHARES + "]," + 
                                                    "[" + SQL_COL_COMMENT + "]," + 
                                                    "[" + SQL_COL_SENIORCIT + "]," + 
                                                    "[" + SQL_COL_TOTAL_CHARGES + "]," + 
                                                    "[" + SQL_COL_PAGER + "]," + 
                                                    "[" + SQL_COL_DHCR_DATE_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_RENT_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_STOVE_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_A_C_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_OTHER_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," + 
                                                    "[" + SQL_COL_DHCR_DATE_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_RENT_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_STOVE_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_A_C_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_OTHER_TWO + "]," + 
                                                    "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," + 
                                                    "[" + SQL_COL_LEGAL_FEE + "]," + 
                                                    "[" + SQL_COL_LATE_FEE + "]," + 
                                                    "[" + SQL_COL_LATE_BASE + "]," + 
                                                    "[" + SQL_COL_LEGAL_FEE_NO + "]," + 
                                                    "[" + SQL_COL_LATE_FEE_YES + "]," + 
                                                    "[" + SQL_COL_LATE_FEE_NO + "]," + 
                                                    "[" + SQL_COL_RENT_LEGAL + "]," + 
                                                    "[" + SQL_COL_FUTURE_LEGAL_1 + "]," + 
                                                    "[" + SQL_COL_FUTURE_LEGAL_2 + "]," + 
                                                    "[" + SQL_COL_FUTURE_LEGAL_3 + "]," + 
                                                    "[" + SQL_COL_PROPOSED_RENT + "]," + 
                                                    "[" + SQL_COL_PROMPT_PAY1 + "]," + 
                                                    "[" + SQL_COL_PROPOSED_RENT2 + "]," + 
                                                    "[" + SQL_COL_PROMPT_PAY2 + "]," + 
                                                    "[" + SQL_COL_PROPOSED_LEGAL2 + "]," + 
                                                    "[" + SQL_COL_PROPOSED_LEGAL + "]," + 
                                                    "[" + SQL_COL_CELL2 + "]," + 
                                                    "[" + SQL_COL_SEC_MULTIPLE + "]," + 
                                                    "[" + SQL_COL_EMAIL1 + "]," + 
                                                    "[" + SQL_COL_EMAIL2 + "]," + 
                                                    //"[" + SQL_COL_TEMP1 + "]," + 
                                                    //"[" + SQL_COL_TEMP2 + "]," + 
                                                    "[" + SQL_COL_SECURITY_ACCOUNT + "]," + 
                                                    "[" + SQL_COL_OPT_TENANT1 + "]," + 
                                                    "[" + SQL_COL_OPT_TENANT2 + "]," + 
                                                    "[" + SQL_COL_SECURITY_BUILDING + "]," + 
                                                    "[" + SQL_COL_SUCCEEDED + "]," + 
                                                    "[" + SQL_COL_SECOND_SUCCESSION + "]," + 
                                                    "[" + SQL_COL_421A + "]," + 
                                                    "[" + SQL_COL_RENT_OVERCHARGE + "]," + 
                                                    "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," + 
                                                    "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," + 
                                                    "[" + SQL_COL_OWNER_OCCUPIED + "]," + 
                                                    "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," + 
                                                    "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," + 
                                                    "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," + 
                                                    "[" + SQL_COL_EXEMPT_DATE + "]," + 
                                                    "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," + 
                                                    "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," + 
                                                    "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," + 
                                                    "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," + 
                                                    "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," + 
                                                    "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," + 
                                                    "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," + 
                                                    "[" + SQL_COL_EXEMPT_SEC11243 + "]," + 
                                                    "[" + SQL_COL_EXEMPT_OTHER + "]," + 
                                                    "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," + 
                                                    "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," + 
                                                    "[" + SQL_COL_EXEMPT_EXP421A + "]," + 
                                                    "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," + 
                                                    "[" + SQL_COL_EXEMPT421A_INCOME + "]," + 
                                                    "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                                    "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                                    "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                                    "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                                    "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                                    "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                                    "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                                    "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                                    "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                                    "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                                    "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                                    "[" + SQL_COL_USE_A_C_H +"]," +
                                                    "[" + SQL_COL_PAYMENT_DAY + "]," +
                                                    "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                                    "[" + SQL_COL_CLIENT_ID + "]," +
                                                    "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                                    "[" + SQL_COL_XRF_TESTED + "]," +
                                                    "[" + SQL_COL_XRF_EXEMPT + "]," +
                                                    "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                                    "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                                    "[" + SQL_COL_DIRECT_PAY + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                                    "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                                    "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                                    " FROM " + TABLE +
                                                    " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "] = ?";

        static string SQL_LOAD_FOR_STATUS = "SELECT " + TABLE + ".*" +
                                            " FROM " + TABLE +
                                            " INNER JOIN " + Building.TABLE +
                                            " ON " + TABLE + ".[" + SQL_COL_BUILDING_NUMBER + "] = " +
                                            Building.TABLE + ".[" + Building.SQL_COL_BUILDING_NUMBER + "]" +
                                            " ORDER BY " + Building.TABLE + ".[" + Building.SQL_COL_OWNER_NUMBER + "]," +
                                            Building.TABLE + ".[" + Building.SQL_COL_BUILDING_NUMBER + "]," +
                                            TABLE + ".[" + Tenants.SQL_COL_TENANT_NUMBER + "]";

        public static string SQL_LOAD_FOR_PAYMENTS = "SELECT " +
                                           "[" + SQL_COL_FUTURE_DATE_1 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_2 + "]," +
                                           "[" + SQL_COL_FUTURE_DATE_3 + "]," +
                                           "[" + SQL_COL_MOVE_IN_DATE + "]," +
                                           "[" + SQL_COL_LEASE_START_DATE + "]," +
                                           "[" + SQL_COL_LEASE_EXPIRATION_DATE + "]," +
                                           "[" + SQL_COL_LAST_PAID_DATE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DATE_TWO + "]," +
                                           "[" + SQL_COL_EXEMPT_DATE + "]," +
                                           "[" + SQL_COL_OPT_TENANT1 + "]," +
                                           "[" + SQL_COL_OPT_TENANT2 + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MEDIAN_INCOME + "]," +
                                           "[" + SQL_COL_OLD_HISTORY_FILE_POINTER + "]," +
                                           "[" + SQL_COL_ROOMS + "]," +
                                           "[" + SQL_COL_SQUARE_FEET + "]," +
                                           "[" + SQL_COL_SHARES + "]," +
                                           "[" + SQL_COL_SEC_MULTIPLE + "]," +
                                           "[" + SQL_COL_RENT + "]," +
                                           "[" + SQL_COL_SECURITY + "]," +
                                           "[" + SQL_COL_ARREARS + "]," +
                                           "[" + SQL_COL_PAID + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_1 + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY1 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT2 + "]," +
                                           "[" + SQL_COL_PROMPT_PAY2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL2 + "]," +
                                           "[" + SQL_COL_PROPOSED_LEGAL + "]," +
                                           "[" + SQL_COL_EXEMPT_LAST_LEGAL_PAID + "]," +
                                           "[" + SQL_COL_LATE_BASE + "]," +
                                           "[" + SQL_COL_RENT_LEGAL + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_1 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_2 + "]," +
                                           "[" + SQL_COL_FUTURE_LEGAL_3 + "]," +
                                           "[" + SQL_COL_PROPOSED_RENT + "]," +
                                           "[" + SQL_COL_FUTURE_RENT_3 + "]," +
                                           "[" + SQL_COL_M_B_R + "]," +
                                           "[" + SQL_COL_TOTAL_CHARGES + "]," +
                                           "[" + SQL_COL_DHCR_RENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_RENT_TWO + "]," +
                                           "[" + SQL_COL_LATE_FEE + "]," +
                                           "[" + SQL_COL_NEW_TENANT_FLAG + "]," +
                                           "[" + SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME_FLAG + "]," +
                                           "[" + SQL_COL_LEASE_LETTER_FLAG + "]," +
                                           "[" + SQL_COL_LOW_RENT_FLAG + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_ONE + "]," +
                                           "[" + SQL_COL_EXEMPT_SEC11243 + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP_SEC608 + "]," +
                                           "[" + SQL_COL_EXEMPT_EXP421A + "]," +
                                           "[" + SQL_COL_EXEMPT421A_MARKET_RATE + "]," +
                                           "[" + SQL_COL_EXEMPT421A_INCOME + "]," +
                                           "[" + SQL_COL_TEMP_EXEMPT_OTHER + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_VACANCY + "]," +
                                           "[" + SQL_COL_EXEMPT_HIGH_RENT_INCOME + "]," +
                                           "[" + SQL_COL_EXEMPT_COMMERCIAL_PROFESSIONAL_CO + "]," +
                                           "[" + SQL_COL_EXEMPT_SUBSTANTIAL_REHAB + "]," +
                                           "[" + SQL_COL_EXEMPT_COOP_CONDO_OWNER + "]," +
                                           "[" + SQL_COL_RENT_OVERCHARGE + "]," +
                                           "[" + SQL_COL_FAIR_MARKET_APPEAL + "]," +
                                           "[" + SQL_COL_TRANSIENT_OCCUPANT + "]," +
                                           "[" + SQL_COL_OWNER_OCCUPIED + "]," +
                                           "[" + SQL_COL_COMMERCIAL_PROFESSIONAL + "]," +
                                           "[" + SQL_COL_NOT_PRIME_RESIDENCE + "]," +
                                           "[" + SQL_COL_LEGAL_FEE_NO + "]," +
                                           "[" + SQL_COL_LATE_FEE_YES + "]," +
                                           "[" + SQL_COL_LATE_FEE_NO + "]," +
                                           "[" + SQL_COL_SUCCEEDED + "]," +
                                           "[" + SQL_COL_SECOND_SUCCESSION + "]," +
                                           "[" + SQL_COL_421A + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_TWO + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_TWO + "]," +
                                           "[" + SQL_COL_DHCR_A_C_TWO + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_TWO + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TWO + "]," +
                                           "[" + SQL_COL_LEGAL_FEE + "]," +
                                           "[" + SQL_COL_DHCR_REFRIGERATOR_ONE + "]," +
                                           "[" + SQL_COL_DHCR_DISHWASHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_A_C_ONE + "]," +
                                           "[" + SQL_COL_DHCR_WINDOWS_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_ONE + "]," +
                                           "[" + SQL_COL_DHCR_STOVE_TWO + "]," +
                                           "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                           "[" + SQL_COL_REPLACING_TENANT + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NUMBER + "]," +
                                           "[" + SQL_COL_TENANT_NAME + "]," +
                                           "[" + SQL_COL_APARTMENT + "]," +
                                           "[" + SQL_COL_DUE_DATE + "]," +
                                           "[" + SQL_COL_TYPE + "]," +
                                           //"[" + SQL_COL_TEMP1 + "]," +
                                           //"[" + SQL_COL_TEMP2 + "]," +
                                           "[" + SQL_COL_SECURITY_ACCOUNT + "]," +
                                           "[" + SQL_COL_SECURITY_BUILDING + "]," +
                                           "[" + SQL_COL_EXEMPT_OTHER_DESC + "]," +
                                           "[" + SQL_COL_PAGER + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_OTHER_TEXT_TWO + "]," +
                                           "[" + SQL_COL_CELL2 + "]," +
                                           "[" + SQL_COL_EMAIL1 + "]," +
                                           "[" + SQL_COL_EMAIL2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD3 + "]," +
                                           "[" + SQL_COL_ADD_NAME + "]," +
                                           "[" + SQL_COL_BANK + "]," +
                                           "[" + SQL_COL_INTEREST_INFO + "]," +
                                           "[" + SQL_COL_COMMENT + "]," +
                                           "[" + SQL_COL_SENIORCIT + "]," +
                                           "[" + SQL_COL_HOME_PHONE2 + "]," +
                                           "[" + SQL_COL_BUS_PHONE1 + "]," +
                                           "[" + SQL_COL_BUS_PHONE2 + "]," +
                                           "[" + SQL_COL_ALT_BILL_NAME + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD1 + "]," +
                                           "[" + SQL_COL_ALT_BILL_ADD2 + "]," +
                                           "[" + SQL_COL_LEGAL_CODE + "]," +
                                           "[" + SQL_COL_LEGAL_CODE_2 + "]," +
                                           "[" + SQL_COL_ADDITIONAL_NAME + "]," +
                                           "[" + SQL_COL_S_S_NUMBER1 + "]," +
                                           "[" + SQL_COL_S_S_NUMBER2 + "]," +
                                           "[" + SQL_COL_HOME_PHONE1 + "]," +
                                           "[" + SQL_COL_DECONTROLLED_LEGAL + "]," +
                                           "[" + SQL_COL_LATE_FEE_PERCENT + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_DATE_TWO + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_ONE + "]," +
                                           "[" + SQL_COL_DHCR_TOTAL_COST_TWO + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_ONE + "]," +
                                           "[" + SQL_COL_DHCR_SUBMITTED_TWO + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_ONE + "]," +
                                           "[" + SQL_COL_DHCR_INFORMED_CONSENT_TWO + "]," +
                                           "[" + SQL_COL_USE_A_C_H + "]," +
                                           "[" + SQL_COL_PAYMENT_DAY + "]," +
                                           "[" + SQL_COL_SAVINGS_ACCOUNT + "]," +
                                           "[" + SQL_COL_EMPLOYEE_OCCUPIED + "]," +
                                           "[" + SQL_COL_XRF_TESTED + "]," +
                                           "[" + SQL_COL_XRF_EXEMPT + "]," +
                                           "[" + SQL_COL_XRF_TESTED_DATE +"]," +
                                           "[" + SQL_COL_XRF_EXEMPT_DATE + "]," +
                                           "[" + SQL_COL_DIRECT_PAY + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE + "]," +
                                            "[" + SQL_COL_XRF_WORK_COMPLETE_DATE + "]," +
                                            "[" + SQL_COL_PROPOSED_RENT2_1 + "]" +
                                           " FROM " + TABLE;

        public static string SQL_LOAD_FOR_BATCH_ADD = " SELECT " +
                                    "[" + SQL_COL_BUILDING_NUMBER + "]," +
                                    "[" + SQL_COL_TENANT_NUMBER + "]," +
                                    "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                                    "[" + SQL_COL_CLIENT_ID + "]," +
                                    "[" + SQL_COL_TENANT_NAME + "]," +
                                    "[" + SQL_COL_APARTMENT + "]," +
                                    "[" + SQL_COL_RENT + "]," +
                                    "[" + SQL_COL_ARREARS + "]," +
                                    "[" + SQL_COL_PAID + "]" +
                                    " FROM " + TABLE;

        public static string SQL_LOAD_FOR_PAYMENT_ENTRY = " SELECT " +
                            "[" + SQL_COL_BUILDING_NUMBER + "]," +
                            "[" + SQL_COL_TENANT_NUMBER + "]," +
                            "[" + SQL_COL_SEQUENCE_NUMBER + "]," +
                            "[" + SQL_COL_CLIENT_ID + "]," +
                            "[" + SQL_COL_TENANT_NAME + "]," +
                            "[" + SQL_COL_APARTMENT + "]," +
                            "[" + SQL_COL_RENT + "]," +
                            "[" + SQL_COL_ARREARS + "]," +
                            "[" + SQL_COL_SECURITY + "]," +
                            "[" + SQL_COL_LAST_PAID_DATE + "]," +
                            "[" + SQL_COL_PAID + "]," +
                            "[" + SQL_COL_TOTAL_CHARGES + "]," +
                            "[" + SQL_COL_M_B_R + "]," +
                            "[" + SQL_COL_LEGAL_CODE + "]" +
                            " FROM " + TABLE +
                            " WHERE [" + SQL_COL_SEQUENCE_NUMBER + "]=?";

        //static string SQL_LOAD_FOR_STATUS = "SELECT " + TABLE + ".*" +
        //    " FROM " + TABLE +
        //    " INNER JOIN " + Building.TABLE +
        //    " ON " + TABLE +".[" + SQL_COL_BUILDING_NUMBER + "] = " + 
        //    Building.TABLE +".[" + Building.SQL_COL__BUILDING__NUMBER + "]" +
        //    " ORDER BY " + Building.TABLE +".[" + Building.SQL_COL__OWNER__NUMBER +"]," + 
        //    Building.TABLE + ".[" + Building.SQL_COL__BUILDING__NUMBER + "]," + 
        //    TABLE + ".[" + Tenants.SQL_COL_TENANT_NUMBER + "]";
#endregion

        public enum HighRentPeriod
        {
            Monthly,
            Weekly
        };

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

        public enum FillTypes : int
        {
            Default,
            Short,
            BankForms

        }

		protected DateTime m_FutureDate1;
		protected DateTime m_FutureDate2;
		protected DateTime m_FutureDate3;
		protected DateTime m_MoveinDate;
		protected DateTime m_LeaseStartDate;
		protected DateTime m_LeaseExpirationDate;
		protected DateTime m_LastPaidDate;
		protected DateTime m_dhcrDateOne;
		protected DateTime m_dhcrDateTwo;
		protected DateTime m_ExemptDate;
		protected DateTime m_dhcrSubmittedDateOne;
		protected DateTime m_dhcrSubmittedDateTwo;
		protected short m_OptTenant1;
		protected short m_OptTenant2;
		protected short m_ExemptHighRentVacanyPeriod;
		protected short m_Exempt421aMedianIncome;
		protected int m_SequenceNumber;
		protected int m_OldHistoryFilePointer;
		protected int m_ReplacingTenant;
		protected long m_clientId;
		protected float m_Rooms;
		protected float m_SquareFeet;
		protected float m_Shares;
		protected float m_SecMultiple;
		protected decimal m_Rent;
		protected decimal m_Security;
		protected decimal m_Arrears;
		protected decimal m_Paid;
		protected decimal m_FutureRent1;
		protected decimal m_FutureRent2;
		protected decimal m_DecontrolledLegal;
		protected decimal m_dhcrTotalCostOne;
		protected decimal m_dhcrTotalCostTwo;
		protected decimal m_PromptPay1;
		protected decimal m_ProposedRent2;
		protected decimal m_PromptPay2;
		protected decimal m_ProposedLegal2;
		protected decimal m_ProposedLegal;
		protected decimal m_ExemptLastLegalPaid;
		protected decimal m_LateBase;
		protected decimal m_RentLegal;
		protected decimal m_FutureLegal1;
		protected decimal m_FutureLegal2;
		protected decimal m_FutureLegal3;
		protected decimal m_ProposedRent;
		protected decimal m_FutureRent3;
		protected decimal m_MBR;
		protected decimal m_TotalCharges;
		protected decimal m_dhcrRentOne;
		protected decimal m_dhcrRentTwo;
		protected decimal m_LateFee;
		protected bool m_NewTenantFlag;
		protected bool m_AlternateBillingAddressFlag;
		protected bool m_AdditionalNameFlag;
		protected bool m_LeaseLetterFlag;
		protected bool m_LowRentFlag;
		protected bool m_dhcrStoveOne;
		protected bool m_LatePercent;
		protected bool m_dhcrSubmittedOne;
		protected bool m_dhcrSubmittedTwo;
		protected bool m_dhcrInformedConsentOne;
		protected bool m_dhcrInformedConsentTwo;
		protected bool m_ExemptSec11243;
		protected bool m_ExemptOther;
		protected bool m_ExemptExpSec608;
		protected bool m_ExemptExp421a;
		protected bool m_Exempt421aMarketRate;
		protected bool m_Exempt421aIncome;
		protected bool m_TempExemptOther;
		protected bool m_ExemptHighRentVacancy;
		protected bool m_ExemptHighRentIncome;
		protected bool m_ExemptCommercialProfessionalCo;
		protected bool m_ExemptSubstantialRehab;
		protected bool m_ExemptCoopCondoOwner;
		protected bool m_RentOvercharge;
		protected bool m_FairMarketAppeal;
		protected bool m_TransientOccupant;
		protected bool m_OwnerOccupied;
		protected bool m_CommercialProfessional;
		protected bool m_NotPrimeResidence;
		protected bool m_LegalFeeNo;
		protected bool m_LateFeeYes;
		protected bool m_LateFeeNo;
		protected bool m_Succeeded;
		protected bool m_SecondSuccession;
		protected bool m_421a;
		protected bool m_dhcrRefrigeratorTwo;
		protected bool m_dhcrDishwasherTwo;
		protected bool m_dhcrACTwo;
		protected bool m_dhcrWindowsTwo;
		protected bool m_dhcrOtherTwo;
		protected bool m_LegalFee;
		protected bool m_dhcrRefrigeratorOne;
		protected bool m_dhcrDishwasherOne;
		protected bool m_dhcrACOne;
		protected bool m_dhcrWindowsOne;
		protected bool m_dhcrOtherOne;
		protected bool m_dhcrStoveTwo;
		protected string m_BuildingNumber;
		protected string m_TenantNumber;
		protected string m_TenantName;
		protected string m_Apartment;
		protected string m_DueDate;
		protected string m_Type;
		//protected string m_Temp1;
		//protected string m_Temp2;
		protected string m_SecurityAccount;
		protected string m_SecurityBuilding;
		protected string m_ExemptOtherDesc;
		protected string m_Pager;
		protected string m_dhcrOtherTextOne;
		protected string m_dhcrOtherTextTwo;
		protected string m_Cell2;
		protected string m_Email1;
		protected string m_Email2;
		protected string m_AltBillAdd3;
		protected string m_AddName;
		protected string m_Bank;
		protected string m_InterestInfo;
		protected string m_Comment = " ";
		protected string m_Seniorcit;
		protected string m_HomePhone2;
		protected string m_BusPhone1;
		protected string m_BusPhone2;
		protected string m_AltBillName = " ";
		protected string m_AltBillAdd1 = " ";
		protected string m_AltBillAdd2 = " ";
		protected string m_LegalCode;
		protected string m_LegalCode2;
		protected string m_AdditionalName = " ";
		protected string m_SSNumber1;
		protected string m_SSNumber2;
		protected string m_HomePhone1;
        protected bool m_UseACH;
        protected int m_PaymentDay;
        protected bool m_SavingsAccount;
        protected bool m_EmployeeOccupied;
        protected bool m_XrfTested;
        protected bool m_XrfExempt;
        protected DateTime m_xrfTestedDate;
        protected DateTime m_xrfExemptDate;
        protected bool m_DirectPay;
		protected bool m_isChanged;

        protected DateTime m_DatePaid;
        protected decimal m_Payment;
        protected decimal m_TotalPaid;
        protected bool m_Post;
        protected bool m_ChargeLateFee;
        protected bool m_xrfWorkComplete;
        protected DateTime m_xrfWorkCompleteDate;
        protected decimal m_ProposedRent2_1;

        protected bool m_SendEmail;
        protected bool m_IsEmail1;
        protected bool m_UseEmail1;
        protected bool m_IsEmail2;
        protected bool m_UseEmail2;

        // For Bank Forms
        protected decimal m_ChargesPaid;
        protected string m_BuildingAddress;
        protected string m_BuildingAccount;
        protected string m_OwnersName;
        protected string m_OwnerAddr1;
        protected string m_OwnerAddr2;


        public Tenants(DateTime FutureDate1,DateTime FutureDate2,DateTime FutureDate3,DateTime MoveinDate,DateTime LeaseStartDate,DateTime LeaseExpirationDate,DateTime LastPaidDate,DateTime dhcrDateOne,DateTime dhcrDateTwo,DateTime ExemptDate,DateTime dhcrSubmittedDateOne,DateTime dhcrSubmittedDateTwo,short OptTenant1,short OptTenant2,short ExemptHighRentVacanyPeriod,short Exempt421aMedianIncome,int SequenceNumber,int OldHistoryFilePointer,int ReplacingTenant,int clientId,float Rooms,float SquareFeet,float Shares,float SecMultiple,decimal Rent,decimal Security,decimal Arrears,decimal Paid,decimal FutureRent1,decimal FutureRent2,decimal DecontrolledLegal,decimal dhcrTotalCostOne,decimal dhcrTotalCostTwo,decimal PromptPay1,decimal ProposedRent2,decimal PromptPay2,decimal ProposedLegal2,decimal ProposedLegal,decimal ExemptLastLegalPaid,decimal LateBase,decimal RentLegal,decimal FutureLegal1,decimal FutureLegal2,decimal FutureLegal3,decimal ProposedRent,decimal FutureRent3,decimal MBR,decimal TotalCharges,decimal dhcrRentOne,decimal dhcrRentTwo,decimal LateFee,bool NewTenantFlag,bool AlternateBillingAddressFlag,bool AdditionalNameFlag,bool LeaseLetterFlag,bool LowRentFlag,bool dhcrStoveOne,bool LatePercent,bool dhcrSubmittedOne,bool dhcrSubmittedTwo,bool dhcrInformedConsentOne,bool dhcrInformedConsentTwo,bool ExemptSec11243,bool ExemptOther,bool ExemptExpSec608,bool ExemptExp421a,bool Exempt421aMarketRate,bool Exempt421aIncome,bool TempExemptOther,bool ExemptHighRentVacancy,bool ExemptHighRentIncome,bool ExemptCommercialProfessionalCo,bool ExemptSubstantialRehab,bool ExemptCoopCondoOwner,bool RentOvercharge,bool FairMarketAppeal,bool TransientOccupant,bool OwnerOccupied,bool CommercialProfessional,bool NotPrimeResidence,bool LegalFeeNo,bool LateFeeYes,bool LateFeeNo,bool Succeeded,bool SecondSuccession,bool x421a,bool dhcrRefrigeratorTwo,bool dhcrDishwasherTwo,bool dhcrACTwo,bool dhcrWindowsTwo,bool dhcrOtherTwo,bool LegalFee,bool dhcrRefrigeratorOne,bool dhcrDishwasherOne,bool dhcrACOne,bool dhcrWindowsOne,bool dhcrOtherOne,bool dhcrStoveTwo,string BuildingNumber,string TenantNumber,string TenantName,string Apartment,string DueDate,string Type,string SecurityAccount,string SecurityBuilding,string ExemptOtherDesc,string Pager,string dhcrOtherTextOne,string dhcrOtherTextTwo,string Cell2,string Email1,string Email2,string AltBillAdd3,string AddName,string Bank,string InterestInfo,string Comment,string Seniorcit,string HomePhone2,string BusPhone1,string BusPhone2,string AltBillName,string AltBillAdd1,string AltBillAdd2,string LegalCode,string LegalCode2,string AdditionalName,string SSNumber1,string SSNumber2,string HomePhone1) : 
				base(TABLE, SQL_COL_SEQUENCE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_FutureDate1 = FutureDate1;
			m_FutureDate2 = FutureDate2;
			m_FutureDate3 = FutureDate3;
			m_MoveinDate = MoveinDate;
			m_LeaseStartDate = LeaseStartDate;
			m_LeaseExpirationDate = LeaseExpirationDate;
			m_LastPaidDate = LastPaidDate;
			m_dhcrDateOne = dhcrDateOne;
			m_dhcrDateTwo = dhcrDateTwo;
			m_ExemptDate = ExemptDate;
			m_dhcrSubmittedDateOne = dhcrSubmittedDateOne;
			m_dhcrSubmittedDateTwo = dhcrSubmittedDateTwo;
			m_OptTenant1 = OptTenant1;
			m_OptTenant2 = OptTenant2;
			m_ExemptHighRentVacanyPeriod = ExemptHighRentVacanyPeriod;
			m_Exempt421aMedianIncome = Exempt421aMedianIncome;
			m_SequenceNumber = SequenceNumber;
			m_OldHistoryFilePointer = OldHistoryFilePointer;
			m_ReplacingTenant = ReplacingTenant;
			m_clientId = clientId;
			m_Rooms = Rooms;
			m_SquareFeet = SquareFeet;
			m_Shares = Shares;
			m_SecMultiple = SecMultiple;
			m_Rent = Rent;
			m_Security = Security;
			m_Arrears = Arrears;
			m_Paid = Paid;
			m_FutureRent1 = FutureRent1;
			m_FutureRent2 = FutureRent2;
			m_DecontrolledLegal = DecontrolledLegal;
			m_dhcrTotalCostOne = dhcrTotalCostOne;
			m_dhcrTotalCostTwo = dhcrTotalCostTwo;
			m_PromptPay1 = PromptPay1;
			m_ProposedRent2 = ProposedRent2;
			m_PromptPay2 = PromptPay2;
			m_ProposedLegal2 = ProposedLegal2;
			m_ProposedLegal = ProposedLegal;
			m_ExemptLastLegalPaid = ExemptLastLegalPaid;
			m_LateBase = LateBase;
			m_RentLegal = RentLegal;
			m_FutureLegal1 = FutureLegal1;
			m_FutureLegal2 = FutureLegal2;
			m_FutureLegal3 = FutureLegal3;
			m_ProposedRent = ProposedRent;
			m_FutureRent3 = FutureRent3;
			m_MBR = MBR;
			m_TotalCharges = TotalCharges;
			m_dhcrRentOne = dhcrRentOne;
			m_dhcrRentTwo = dhcrRentTwo;
			m_LateFee = LateFee;
			m_NewTenantFlag = NewTenantFlag;
			m_AlternateBillingAddressFlag = AlternateBillingAddressFlag;
			m_AdditionalNameFlag = AdditionalNameFlag;
			m_LeaseLetterFlag = LeaseLetterFlag;
			m_LowRentFlag = LowRentFlag;
			m_dhcrStoveOne = dhcrStoveOne;
			m_LatePercent = LatePercent;
			m_dhcrSubmittedOne = dhcrSubmittedOne;
			m_dhcrSubmittedTwo = dhcrSubmittedTwo;
			m_dhcrInformedConsentOne = dhcrInformedConsentOne;
			m_dhcrInformedConsentTwo = dhcrInformedConsentTwo;
			m_ExemptSec11243 = ExemptSec11243;
			m_ExemptOther = ExemptOther;
			m_ExemptExpSec608 = ExemptExpSec608;
			m_ExemptExp421a = ExemptExp421a;
			m_Exempt421aMarketRate = Exempt421aMarketRate;
			m_Exempt421aIncome = Exempt421aIncome;
			m_TempExemptOther = TempExemptOther;
			m_ExemptHighRentVacancy = ExemptHighRentVacancy;
			m_ExemptHighRentIncome = ExemptHighRentIncome;
			m_ExemptCommercialProfessionalCo = ExemptCommercialProfessionalCo;
			m_ExemptSubstantialRehab = ExemptSubstantialRehab;
			m_ExemptCoopCondoOwner = ExemptCoopCondoOwner;
			m_RentOvercharge = RentOvercharge;
			m_FairMarketAppeal = FairMarketAppeal;
			m_TransientOccupant = TransientOccupant;
			m_OwnerOccupied = OwnerOccupied;
			m_CommercialProfessional = CommercialProfessional;
			m_NotPrimeResidence = NotPrimeResidence;
			m_LegalFeeNo = LegalFeeNo;
			m_LateFeeYes = LateFeeYes;
			m_LateFeeNo = LateFeeNo;
			m_Succeeded = Succeeded;
			m_SecondSuccession = SecondSuccession;
			m_421a = x421a;
			m_dhcrRefrigeratorTwo = dhcrRefrigeratorTwo;
			m_dhcrDishwasherTwo = dhcrDishwasherTwo;
			m_dhcrACTwo = dhcrACTwo;
			m_dhcrWindowsTwo = dhcrWindowsTwo;
			m_dhcrOtherTwo = dhcrOtherTwo;
			m_LegalFee = LegalFee;
			m_dhcrRefrigeratorOne = dhcrRefrigeratorOne;
			m_dhcrDishwasherOne = dhcrDishwasherOne;
			m_dhcrACOne = dhcrACOne;
			m_dhcrWindowsOne = dhcrWindowsOne;
			m_dhcrOtherOne = dhcrOtherOne;
			m_dhcrStoveTwo = dhcrStoveTwo;
			m_BuildingNumber = BuildingNumber;
			m_TenantNumber = TenantNumber;
			m_TenantName = TenantName;
			m_Apartment = Apartment;
			m_DueDate = DueDate;
			m_Type = Type;
			//m_Temp1 = Temp1;
			//m_Temp2 = Temp2;
			m_SecurityAccount = SecurityAccount;
			m_SecurityBuilding = SecurityBuilding;
			m_ExemptOtherDesc = ExemptOtherDesc;
			m_Pager = Pager;
			m_dhcrOtherTextOne = dhcrOtherTextOne;
			m_dhcrOtherTextTwo = dhcrOtherTextTwo;
			m_Cell2 = Cell2;
			m_Email1 = Email1;
			m_Email2 = Email2;
			m_AltBillAdd3 = AltBillAdd3;
			m_AddName = AddName;
			m_Bank = Bank;
			m_InterestInfo = InterestInfo;
			m_Comment = Comment;
			m_Seniorcit = Seniorcit;
			m_HomePhone2 = HomePhone2;
			m_BusPhone1 = BusPhone1;
			m_BusPhone2 = BusPhone2;
			m_AltBillName = AltBillName;
			m_AltBillAdd1 = AltBillAdd1;
			m_AltBillAdd2 = AltBillAdd2;
			m_LegalCode = LegalCode;
			m_LegalCode2 = LegalCode2;
			m_AdditionalName = AdditionalName;
			m_SSNumber1 = SSNumber1;
			m_SSNumber2 = SSNumber2;
			m_HomePhone1 = HomePhone1;


		}


        public static readonly DateTime DefaultDate = new DateTime(1900, 1, 1);

        public Tenants() : base(TABLE, SQL_COL_SEQUENCE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
        {
            m_Post = true;
        }

        public Tenants(string BuildingNumber, string TenantNumber, int SequenceNumber, Int32 OldHistoryFilePointer, bool NewTenantFlag, Int32 ReplacingTenant, string TenantName, string Apartment, string DueDate, string Type,
        bool AlternateBillingAddressFlag, bool AdditionalNameFlag, bool LeaseLetterFlag, bool LowRentFlag, string LegalCode, string LegalCode2, decimal Rent, decimal Security, decimal Arrears, decimal Paid,
        decimal FutureRent1, decimal FutureRent2, decimal FutureRent3, decimal MBR, DateTime FutureDate1, DateTime FutureDate2, DateTime FutureDate3, float Rooms, float SquareFeet, DateTime MoveinDate,
        DateTime LeaseStartDate, DateTime LeaseExpirationDate, DateTime LastPaidDate, string AdditionalName, string SSNumber1, string SSNumber2, string HomePhone1, string HomePhone2, string BusPhone1, string BusPhone2,
        string AltBillName, string AltBillAdd1, string AltBillAdd2, string AltBillAdd3, string AddName, string Bank, string InterestInfo, float Shares, string Comment, string Seniorcit,
        decimal TotalCharges, string Pager, DateTime dhcrDateOne, decimal dhcrRentOne, bool dhcrStoveOne, bool dhcrRefrigeratorOne, bool dhcrDishwasherOne, bool dhcrACOne, bool dhcrWindowsOne, bool dhcrOtherOne,
        string dhcrOtherTextOne, DateTime dhcrDateTwo, decimal dhcrRentTwo, bool dhcrStoveTwo, bool dhcrRefrigeratorTwo, bool dhcrDishwasherTwo, bool dhcrACTwo, bool dhcrWindowsTwo, bool dhcrOtherTwo, string dhcrOtherTextTwo,
        bool LegalFee, decimal LateFee, decimal LateBase, bool LegalFeeNo, bool LateFeeYes, bool LateFeeNo, decimal RentLegal, decimal FutureLegal1, decimal FutureLegal2, decimal FutureLegal3,
        decimal ProposedRent, decimal PromptPay1, decimal ProposedRent2, decimal PromptPay2, decimal ProposedLegal2, decimal ProposedLegal, string Cell2, float SecMultiple, string Email1, string Email2,
        string SecurityAccount, Int16 OptTenant1, Int16 OptTenant2, string SecurityBuilding, bool Succeeded, bool SecondSuccession, bool x421a, bool RentOvercharge,
        bool FairMarketAppeal, bool TransientOccupant, bool OwnerOccupied, bool CommercialProfessional, bool NotPrimeResidence, bool TempExemptOther, DateTime ExemptDate, bool ExemptHighRentVacancy, decimal ExemptLastLegalPaid, Int16 ExemptHighRentVacanyPeriod,
        bool ExemptHighRentIncome, bool ExemptCommercialProfessionalCo, bool ExemptSubstantialRehab, bool ExemptCoopCondoOwner, bool ExemptSec11243, bool ExemptOther, string ExemptOtherDesc, bool ExemptExpSec608, bool ExemptExp421a, bool Exempt421aMarketRate,
        bool Exempt421aIncome, Int16 Exempt421aMedianIncome, decimal DecontrolledLegal, long clientId) : base(TABLE, SQL_COL_SEQUENCE_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
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
            m_MBR = MBR;
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
            //m_Temp1 = Temp1;
            //m_Temp2 = Temp2;
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
            m_DecontrolledLegal = DecontrolledLegal;
            m_clientId = clientId;

            m_Post = true;
        }

        protected Tenants fill(DataRow dr, bool showTotalPaid = false, decimal totalPaid = 0)
        {

            try {
                m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
                m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
                m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
                m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
                m_SSNumber1 = (string)(Information.IsDBNull(dr[SQL_COL_S_S_NUMBER1]) ? "" : dr[SQL_COL_S_S_NUMBER1]);
                m_SSNumber2 = (string)(Information.IsDBNull(dr[SQL_COL_S_S_NUMBER2]) ? "" : dr[SQL_COL_S_S_NUMBER2]);
                m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
                m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                m_OldHistoryFilePointer = (int)(Information.IsDBNull(dr[SQL_COL_OLD_HISTORY_FILE_POINTER]) ? 0 : dr[SQL_COL_OLD_HISTORY_FILE_POINTER]);
                m_NewTenantFlag = (bool)dr[SQL_COL_NEW_TENANT_FLAG];
                m_ReplacingTenant = (int)(Information.IsDBNull(dr[SQL_COL_REPLACING_TENANT]) ? 0 : dr[SQL_COL_REPLACING_TENANT]);
                m_DueDate = (string)(Information.IsDBNull(dr[SQL_COL_DUE_DATE]) ? "" : dr[SQL_COL_DUE_DATE]);
                m_AlternateBillingAddressFlag = (bool)dr[SQL_COL_ALTERNATE_BILLING_ADDRESS_FLAG];
                m_AdditionalNameFlag = (bool)dr[SQL_COL_ADDITIONAL_NAME_FLAG];
                m_LeaseLetterFlag = (bool)dr[SQL_COL_LEASE_LETTER_FLAG];
                m_LowRentFlag = (bool)dr[SQL_COL_LOW_RENT_FLAG];
                m_LegalCode = (string)(Information.IsDBNull(dr[SQL_COL_LEGAL_CODE]) ? "" : dr[SQL_COL_LEGAL_CODE]);
                m_LegalCode2 = (string)(Information.IsDBNull(dr[SQL_COL_LEGAL_CODE_2]) ? "" : dr[SQL_COL_LEGAL_CODE_2]);
                m_Rent = (decimal)dr[SQL_COL_RENT];
                m_Security = (decimal)dr[SQL_COL_SECURITY];
                m_Arrears = (decimal)dr[SQL_COL_ARREARS];
                if (showTotalPaid)
                    m_Paid = totalPaid;
                else
                    m_Paid = (decimal)dr[SQL_COL_PAID];
                /*
                m_FutureRent1 = (decimal)dr[SQL_COL_FUTURE_RENT_1];
                m_FutureRent2 = (decimal)dr[SQL_COL_FUTURE_RENT_2];
                m_FutureRent3 = (decimal)dr[SQL_COL_FUTURE_RENT_3];
                */
                m_MBR = (Information.IsDBNull(dr[SQL_COL_M_B_R]) ? (decimal)0.0 : (decimal)dr[SQL_COL_M_B_R]);
                /*
                m_FutureDate1 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_1]) ? GlobalShared.NULL_DATE : dr[SQL_COL_FUTURE_DATE_1]);
                m_FutureDate2 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_2]) ? GlobalShared.NULL_DATE : dr[SQL_COL_FUTURE_DATE_2]);
                m_FutureDate3 = (DateTime)(Information.IsDBNull(dr[SQL_COL_FUTURE_DATE_3]) ? GlobalShared.NULL_DATE : dr[SQL_COL_FUTURE_DATE_3]);
                */
                m_Rooms = (float)dr[SQL_COL_ROOMS];
                m_SquareFeet = (float)dr[SQL_COL_SQUARE_FEET];
                m_MoveinDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_MOVE_IN_DATE]) ? GlobalShared.NULL_DATE : dr[SQL_COL_MOVE_IN_DATE]);
                m_LeaseStartDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LEASE_START_DATE]) ? GlobalShared.NULL_DATE : dr[SQL_COL_LEASE_START_DATE]);
                m_LeaseExpirationDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LEASE_EXPIRATION_DATE]) ? GlobalShared.NULL_DATE : dr[SQL_COL_LEASE_EXPIRATION_DATE]);
                m_LastPaidDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_LAST_PAID_DATE]) ? GlobalShared.NULL_DATE : dr[SQL_COL_LAST_PAID_DATE]);
                m_AdditionalName = (string)(Information.IsDBNull(dr[SQL_COL_ADDITIONAL_NAME]) ? "" : dr[SQL_COL_ADDITIONAL_NAME]);
                m_HomePhone1 = (string)(Information.IsDBNull(dr[SQL_COL_HOME_PHONE1]) ? "" : dr[SQL_COL_HOME_PHONE1]);
                m_HomePhone2 = (string)(Information.IsDBNull(dr[SQL_COL_HOME_PHONE2]) ? "" : dr[SQL_COL_HOME_PHONE2]);
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
                m_dhcrDateOne = (DateTime)(Information.IsDBNull(dr[SQL_COL_DHCR_DATE_ONE]) ? GlobalShared.NULL_DATE : dr[SQL_COL_DHCR_DATE_ONE]);
                DateTime temp;
                if (!DateTime.TryParse(m_dhcrDateOne.ToString(), out temp) || m_dhcrDateOne < GlobalShared.NULL_DATE)
                    m_dhcrDateOne = GlobalShared.NULL_DATE;
                m_dhcrRentOne = (decimal)(Information.IsDBNull(dr[SQL_COL_DHCR_RENT_ONE]) ? 0 : dr[SQL_COL_DHCR_RENT_ONE]);
                m_dhcrStoveOne = (bool)dr[SQL_COL_DHCR_STOVE_ONE];
                m_dhcrRefrigeratorOne = (bool)dr[SQL_COL_DHCR_REFRIGERATOR_ONE];
                m_dhcrDishwasherOne = (bool)dr[SQL_COL_DHCR_DISHWASHER_ONE];
                m_dhcrACOne = (bool)dr[SQL_COL_DHCR_A_C_ONE];
                m_dhcrWindowsOne = (bool)dr[SQL_COL_DHCR_WINDOWS_ONE];
                m_dhcrOtherOne = (bool)dr[SQL_COL_DHCR_OTHER_ONE];
                m_dhcrOtherTextOne = (string)(Information.IsDBNull(dr[SQL_COL_DHCR_OTHER_TEXT_ONE]) ? "" : dr[SQL_COL_DHCR_OTHER_TEXT_ONE]);
                m_dhcrDateTwo = (DateTime)(Information.IsDBNull(dr[SQL_COL_DHCR_DATE_TWO]) ? GlobalShared.NULL_DATE : dr[SQL_COL_DHCR_DATE_TWO]);
                if (!DateTime.TryParse(m_dhcrDateTwo.ToString(), out temp) || m_dhcrDateTwo < GlobalShared.NULL_DATE)
                    m_dhcrDateTwo = GlobalShared.NULL_DATE;
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
                m_ProposedRent = (Information.IsDBNull(dr[SQL_COL_PROPOSED_RENT]) ? (decimal)0 : (decimal)dr[SQL_COL_PROPOSED_RENT]);
                m_PromptPay1 = (Information.IsDBNull(dr[SQL_COL_PROMPT_PAY1]) ? (decimal)0 : (decimal)dr[SQL_COL_PROMPT_PAY1]);
                m_ProposedRent2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_RENT2]) ? (decimal)0 : (decimal)dr[SQL_COL_PROPOSED_RENT2]);
                m_PromptPay2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROMPT_PAY2]) ? (decimal)0 : (decimal)dr[SQL_COL_PROMPT_PAY2]);
                m_ProposedLegal2 = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_LEGAL2]) ? (decimal)0 : (decimal)dr[SQL_COL_PROPOSED_LEGAL2]);
                m_ProposedLegal = (decimal)(Information.IsDBNull(dr[SQL_COL_PROPOSED_LEGAL]) ? (decimal)0 : (decimal)dr[SQL_COL_PROPOSED_LEGAL]);
                m_Cell2 = (string)(Information.IsDBNull(dr[SQL_COL_CELL2]) ? "" : dr[SQL_COL_CELL2]);
                m_SecMultiple = (float)(Information.IsDBNull(dr[SQL_COL_SEC_MULTIPLE]) ? 1 : dr[SQL_COL_SEC_MULTIPLE]);
                m_Email1 = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL1]) ? "" : dr[SQL_COL_EMAIL1]);
                m_Email2 = (string)(Information.IsDBNull(dr[SQL_COL_EMAIL2]) ? "" : dr[SQL_COL_EMAIL2]);
                //m_Temp1 = (string)(Information.IsDBNull(dr[SQL_COL_TEMP1]) ? "" : dr[SQL_COL_TEMP1]);
                //m_Temp2 = (string)(Information.IsDBNull(dr[SQL_COL_TEMP2]) ? "" : dr[SQL_COL_TEMP2]);
                m_SecurityAccount = (string)(Information.IsDBNull(dr[SQL_COL_SECURITY_ACCOUNT]) ? "" : dr[SQL_COL_SECURITY_ACCOUNT]);
                m_OptTenant1 = (Information.IsDBNull(dr[SQL_COL_OPT_TENANT1]) ? (short)0 : (short)dr[SQL_COL_OPT_TENANT1]);
                m_OptTenant2 = (short)(Information.IsDBNull(dr[SQL_COL_OPT_TENANT2]) ? (short)0 : (short)dr[SQL_COL_OPT_TENANT2]);
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
                m_ExemptDate = (DateTime)(Information.IsDBNull(dr[SQL_COL_EXEMPT_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_EXEMPT_DATE]);
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
                m_clientId = (int)dr[SQL_COL_CLIENT_ID];
                m_BusPhone1 = (string)(Information.IsDBNull(dr[SQL_COL_BUS_PHONE1]) ? "" : dr[SQL_COL_BUS_PHONE1]);
                m_BusPhone2 = (string)(Information.IsDBNull(dr[SQL_COL_BUS_PHONE2]) ? "" : dr[SQL_COL_BUS_PHONE2]);
                m_DecontrolledLegal = (decimal)dr[SQL_COL_DECONTROLLED_LEGAL];
                this.UseDC = false;
                m_LatePercent = (bool)dr[SQL_COL_LATE_FEE_PERCENT];

                m_dhcrSubmittedDateOne = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DHCR_SUBMITTED_DATE_ONE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DHCR_SUBMITTED_DATE_ONE]);
                m_dhcrSubmittedDateTwo = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DHCR_SUBMITTED_DATE_TWO]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DHCR_SUBMITTED_DATE_TWO]);
                m_dhcrTotalCostOne = (decimal)dr[SQL_COL_DHCR_TOTAL_COST_ONE];
                m_dhcrTotalCostTwo = (decimal)dr[SQL_COL_DHCR_TOTAL_COST_TWO];
                m_dhcrSubmittedOne = (bool)dr[SQL_COL_DHCR_SUBMITTED_ONE];
                m_dhcrSubmittedTwo = (bool)dr[SQL_COL_DHCR_SUBMITTED_TWO];
                m_dhcrInformedConsentOne = (bool)dr[SQL_COL_DHCR_INFORMED_CONSENT_ONE];
                m_dhcrInformedConsentTwo = (bool)dr[SQL_COL_DHCR_INFORMED_CONSENT_TWO];
                m_UseACH = (bool)dr[SQL_COL_USE_A_C_H];
                m_PaymentDay = (int)dr[SQL_COL_PAYMENT_DAY];
                m_SavingsAccount = (bool)dr[SQL_COL_SAVINGS_ACCOUNT];
                m_EmployeeOccupied = (bool)dr[SQL_COL_EMPLOYEE_OCCUPIED];
                m_XrfTested = (bool)dr[SQL_COL_XRF_TESTED];
                m_XrfExempt = (bool)dr[SQL_COL_XRF_EXEMPT];
                m_DirectPay = (bool)dr[SQL_COL_DIRECT_PAY];
                m_ProposedRent2_1 = (decimal)dr[SQL_COL_PROPOSED_RENT2_1];
                try
                {
                    m_xrfTestedDate = (DateTime)dr[SQL_COL_XRF_TESTED_DATE];
                    //m_xrfTestedDate = (DateTime)(Convert.IsDBNull(dr[SQL_COL_XRF_TESTED_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_XRF_TESTED_DATE]);
                }
                catch (Exception ex) 
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "XrfTestedDate: " + ((DateTime)dr[SQL_COL_XRF_TESTED_DATE]).ToString() + "] " + ex.Message, false);
                }
                try
                {
                    m_xrfExemptDate = (DateTime)dr[SQL_COL_XRF_EXEMPT_DATE];
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,"XrfTestedDate: " + ((DateTime)dr[SQL_COL_XRF_EXEMPT_DATE]).ToString() + "] " + ex.Message, false);
                }
                try
                {
                    m_xrfWorkCompleteDate = (DateTime)dr[SQL_COL_XRF_WORK_COMPLETE_DATE];
                }
                catch (Exception ex)
                {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,"XrfWorkCompletedDate: " + ((DateTime)dr[SQL_COL_XRF_WORK_COMPLETE_DATE]).ToString() + "] " + ex.Message, false);

                }
                    try
                {
                    Types type = Types.load(1, m_Type);
                    if (type != null && type.UseDC)
                        this.UseDC = true;
                }
                catch { }
                m_xrfWorkComplete = (bool)dr[SQL_COL_XRF_WORK_COMPLETE];

            }

            catch (SqlException ex)
            {
                MessageBoxCustom.Show("XRF: " + ex.Message);
                return this;
            } catch (System.Exception exg) {
                MessageBoxCustom.Show("XRF: " + exg.Message);
                return this;
            }
            return this;

        }

        protected Tenants fillShort(DataRow dr)
        {

            try {
                m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
                m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
                m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
                m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
                m_Paid = (decimal)(Information.IsDBNull(dr[SQL_COL_PAID]) ? 0 : dr[SQL_COL_PAID]);
                m_Rent = (decimal)(Information.IsDBNull(dr[SQL_COL_RENT]) ? 0 : dr[SQL_COL_RENT]);
                m_Arrears = (decimal)(Information.IsDBNull(dr[SQL_COL_ARREARS]) ? 0 : dr[SQL_COL_ARREARS]);
                m_clientId = (int)(Information.IsDBNull(dr[SQL_COL_CLIENT_ID]) ? 0 : dr[SQL_COL_CLIENT_ID]);
                m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
                m_UseACH = (bool)(Information.IsDBNull(dr[SQL_COL_USE_A_C_H]) ? false : dr[SQL_COL_USE_A_C_H]) ;
            }
            catch (SqlException ex)
            {
                return this;
            } catch (System.Exception exg) {
                return this;
            }
            return this;

        }

        protected Tenants fillBankForms(DataRow dr)
        {

            try
            {
                m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
                m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
                //m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
                m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
                m_SecurityAccount = (string)(Information.IsDBNull(dr[SQL_COL_SECURITY_ACCOUNT]) ? "" : dr[SQL_COL_SECURITY_ACCOUNT]);
                m_ChargesPaid = (decimal)(Information.IsDBNull(dr[Charges.SQL_COL_PAID]) ? 0 : dr[Charges.SQL_COL_PAID]);
                m_BuildingAddress = (string)(Information.IsDBNull(dr[Building.SQL_COL_ADDRESS_1]) ? "" : dr[Building.SQL_COL_ADDRESS_1]);
                m_BuildingAccount = (string)(Information.IsDBNull(dr[Building.SQL_COL_MASTER_ACCOUNT]) ? "" : dr[Building.SQL_COL_MASTER_ACCOUNT]);
                m_OwnersName = (string)(Information.IsDBNull(dr[Owners.SQL_COL__OWNER__NAME]) ? "" : dr[Owners.SQL_COL__OWNER__NAME]);
                m_OwnerAddr1 = (string)(Information.IsDBNull(dr[SQL_COL_OWNER_ADDRESS1]) ? "" : dr[SQL_COL_OWNER_ADDRESS1]);
                m_OwnerAddr2 = (string)(Information.IsDBNull(dr[SQL_COL_OWNER_ADDRESS2]) ? "" : dr[SQL_COL_OWNER_ADDRESS2]);
            }
            catch (SqlException ex)
            {
                return this;
            }
            catch (System.Exception exg)
            {
                return this;
            }
            return this;

        }


        protected Tenants fillPaymentEntry(DataRow dr, decimal totalpaid)
        {

            try
            {
                m_BuildingNumber = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
                m_TenantNumber = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NUMBER]) ? "" : dr[SQL_COL_TENANT_NUMBER]);
                m_SequenceNumber = (int)dr[SQL_COL_SEQUENCE_NUMBER];
                m_TenantName = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_NAME]) ? "" : dr[SQL_COL_TENANT_NAME]);
                m_Apartment = (string)(Information.IsDBNull(dr[SQL_COL_APARTMENT]) ? "" : dr[SQL_COL_APARTMENT]);
                m_Rent = (decimal)(Information.IsDBNull(dr[SQL_COL_RENT]) ? "0" : dr[SQL_COL_RENT]);
                m_Arrears = (decimal)(Information.IsDBNull(dr[SQL_COL_ARREARS]) ? "0" : dr[SQL_COL_ARREARS]);
                //m_Type = (string)(Information.IsDBNull(dr[SQL_COL_TYPE]) ? "" : dr[SQL_COL_TYPE]);
                m_TotalPaid = totalpaid;
                m_Paid = totalpaid;
                //m_TotalPaid = (decimal)(Information.IsDBNull(dr[SQL_COL_PAID]) ? "0" : dr[SQL_COL_PAID]);
                m_DatePaid = (DateTime)(Information.IsDBNull(dr[SQL_COL_LAST_PAID_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_LAST_PAID_DATE]);
                m_Security = (decimal)(Information.IsDBNull(dr[SQL_COL_SECURITY]) ? "0" : dr[SQL_COL_SECURITY]);
                m_TotalCharges = (decimal)(Information.IsDBNull(dr[SQL_COL_TOTAL_CHARGES]) ? "0" : dr[SQL_COL_TOTAL_CHARGES]);
                m_LegalCode = (string)(Information.IsDBNull(dr[SQL_COL_LEGAL_CODE]) ? "" : dr[SQL_COL_LEGAL_CODE]);
                m_MBR = (decimal)(Information.IsDBNull(dr[SQL_COL_M_B_R]) ? "0" : dr[SQL_COL_M_B_R]);
            }
            catch (SqlException ex)
            {
                return this;
            }
            catch (System.Exception exg)
            {
                return this;
            }
            return this;

        }

        public static Tenants load(int seq, bool TotalPaid = false)
        {

            Tenants obj = new Tenants();
            string[] opt = { seq.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_SEQUENCE_NUMBER, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                decimal totalPaid = 0;
                if (TotalPaid)
                {
                    totalPaid = Payments.loadForTotalPaid(seq);
                }
                DataRow dr = ds.Tables[0].Rows[0];

                return obj.fill(dr, TotalPaid, totalPaid);
            }
            else
                return null;
        }

        public static Tenants loadForPaymentEntry(int id)
        {

            Tenants obj = new Tenants();
            string[] opt = {id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_FOR_PAYMENT_ENTRY, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                decimal totalPaid = 0;
                totalPaid = Payments.loadForTotalPaid(id);
                DataRow dr = ds.Tables[0].Rows[0];

                return obj.fillPaymentEntry(dr, totalPaid);
            }
            else
                return null;
        }

        public static Tenants[] load(string m_BuildingNumber, string m_TenantNumber, long clientId = 1)
        {

            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();
            string[] opt = {
                m_BuildingNumber,
                m_TenantNumber
            };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_BUILDING_TENANT, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0) {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr );
                obj.Paid = Payments.loadForTotalPaid(obj.SequenceNumber);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Tenants[])list.ToArray(typeof(Tenants));
            } catch (Exception ex) {
                return null;
            }

        }


        public static Tenants[] load(string m_BuildingNumber, long clientId = 1)
        {

            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();
            string[] opt = {
                m_BuildingNumber
            };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_BUILDING, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                obj.Paid = Payments.loadForTotalPaid(obj.SequenceNumber);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static Tenants[] loadForNames(bool vacants = true, bool ByName = true)
        {
            string query = "";
            if (vacants)
                query = SQL_LOAD_FOR_NAMES;
            else
                query = SQL_LOAD_FOR_NAMES_NO_VACANT;

            if (ByName)
                query += ORDER_BY_NAME;
            else
                query += ORDER_BY_BUILDING;

            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();
            string[] opt = null;
            DataSet ds = new DataSet();
            ds = obj.load(query, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fillShort(dr);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Tenants[])list.ToArray(typeof(Tenants));
            } catch (SqlException ex) {
                return null;
            }
        }

        public static Tenants[] loadForStatements(string startBld, string endBld, long clientId, DateTime? lease = null, ArrayList tenantList = null, bool IsNacha = false)
        {
            string[] opt = null;
            string query = SQL_LOAD_FOR_STATEMENTS;
            if (tenantList != null && tenantList.Count > 0)
            {
                string keys = tenantList[0].ToString();
                for (int i = 1; i < tenantList.Count; i++)
                    keys += ", " + tenantList[i].ToString();
                query += SQL_LOAD_FOR_STATEMENTS_ONE;
                query = query.Replace("<keys>", keys);
            }
            else
            {
                query += SQL_LOAD_FOR_STATEMENTS_ALL;
                if (IsNacha)
                    query += ",  [" + SQL_COL_PAYMENT_DAY + "]";
                opt = new string[] { startBld, endBld };
            }
            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();

            DataSet ds = new DataSet();
            ds = obj.load(query, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                obj.Paid = Payments.loadForTotalPaid(obj.SequenceNumber);
                obj.isChanged = false;
                
                if (lease != null)
                {
                    if (obj.LeaseExpirationDate.Date != lease.Value.Date)
                        continue;
                }
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }


        public static Tenants[] loadForBatch(string startBld, string endBld, long clientId, string type, bool future = false)
        {

            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();

            string sql = SQL_LOAD_FOR_BATCH;
            string[] opt = { startBld, endBld };
            if (type.Trim() != "" && !type.ToLower().Contains("<all>"))
            {
                sql += " AND [" + SQL_COL_TYPE + "] = '?'";
                opt = new string[] { startBld, endBld, type };
            }
            sql += " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "]";
            DataSet ds = new DataSet();
            ds = obj.load(sql, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);

                if (future)
                {
                    FutureRents[] rents = WinPM.FutureRents.loadBySequence(obj.SequenceNumber);
                    if (rents == null)
                    {
                        rents = new FutureRents[2];
                        rents[0] = new FutureRents() { TenantSequence = obj.m_SequenceNumber };
                        rents[1] = new FutureRents() { TenantSequence = obj.m_SequenceNumber };
                    }
                    else if (rents.Length == 1)
                    {
                        Array.Resize<FutureRents>(ref rents, 2);
                        rents[1] = new FutureRents() { TenantSequence = obj.m_SequenceNumber };
                    }
                    obj.Future1 = rents[0];
                    obj.Future2 = rents[1];
                }
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static Tenants[] loadForBatchAdd(string sql)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();
            string[] opt = null;

            ds = obj.load(sql, opt);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Tenants();
                obj.fillShort(dr);
                obj.Paid = Payments.loadForTotalPaid(obj.SequenceNumber);
                obj.chg = new Charges();
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static Tenants[] loadForEom(long clientId)
        {
            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();

            string[] opt = null;
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_FOR_EOM, opt);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Tenants();
                obj.fill(dr);
                obj.Paid = Payments.loadForTotalPaid(obj.SequenceNumber);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        public static Tenants[] loadForDeletion(string bld)
        {

            ArrayList list = new ArrayList();
            Tenants obj = new Tenants();
            string[] opt = { bld };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_FOR_DELETION, opt);
            if (ds.Tables[0].Rows.Count == 0) {
                return null;
            }

            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fillShort(dr);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Tenants[])list.ToArray(typeof(Tenants));
            } catch (SqlException ex) {
                return null;
            }

        }

        public static Tenants[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();

            ds = obj.loadAll();
            obj = null;
            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Tenants[])list.ToArray(typeof(Tenants));
            } catch (SqlException ex) {
                return null;
            }

        }

        public static Tenants[] loadNonACH()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();
            string[] opt = null;
            ds = obj.load(SQL_LOAD_Non_ACH,opt);
            obj = null;
            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static Tenants[] load(short orderBy)
        {

            if (orderBy < 0 | orderBy > orderBySql.Length - 1) {
                return null;
            }
            string[] opt = null;
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();

            ds = obj.load(SQL_LOAD_ALL + orderBySql[orderBy], opt);
            obj = null;
            DataRow dr = null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Tenants[])list.ToArray(typeof(Tenants));
            } catch (SqlException ex) {
                return null;
            }
        }

        public static Tenants[] loadForStatus(long clientId)
        {
            string[] opt = null;
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();

            ds = obj.load(SQL_LOAD_FOR_STATUS, opt);
            obj = null;
            DataRow dr = null;
            if (ds == null)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[Tenants.loadForStatus]: No records found.", false);
            }
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }

            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static Tenants[] Query(string stmt, DateTime dt, int fillType = 0)
        {
            string[] opt = null;
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Tenants obj = new Tenants();

            ds = obj.load(stmt, opt);
            obj = null;
            DataRow dr = null;
            if (ds == null)
                return null;
            foreach (DataRow dr_loopVariable in ds.Tables[0].Rows)
            {
                dr = dr_loopVariable;
                obj = new Tenants();
                switch (fillType)
                {
                    case (int)FillTypes.Default:
                        obj.fill(dr);
                        break;
                    case (int)FillTypes.Short:
                        obj.fillShort(dr); 
                        break;
                    case (int)FillTypes.BankForms:
                        obj.fillBankForms(dr);
                        break;
                }
                if (dt > GlobalShared.NULL_DATE)
                    obj.DatePaid = dt;
                obj.Payment = obj.Balance;
                list.Add(obj);
                obj = null;
            }

            try
            {
                return (Tenants[])list.ToArray(typeof(Tenants));
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
	public bool save(bool debug = false)
        {

            m_TenantName = m_TenantName.Replace("'", "`");
            m_AdditionalName = m_AdditionalName.Replace("'", "`");
            m_AltBillName = m_AltBillName.Replace("'", "`");
            m_AltBillAdd1 = m_AltBillAdd1.Replace("'", "`");
            m_AltBillAdd2 = m_AltBillAdd2.Replace("'", "`");
            m_Comment = m_Comment.Replace("'", "`");
            if (m_SequenceNumber > 0) {
                string[] values = { m_FutureDate1.ToString(), m_FutureDate2.ToString(), m_FutureDate3.ToString(), m_MoveinDate.ToString(), m_LeaseStartDate.ToString(), m_LeaseExpirationDate.ToString(), m_LastPaidDate.ToString(), m_dhcrDateOne.ToString(), m_dhcrDateTwo.ToString(), m_ExemptDate.ToString(),
                                    m_OptTenant1.ToString(), m_OptTenant2.ToString(), m_ExemptHighRentVacanyPeriod.ToString(), m_Exempt421aMedianIncome.ToString(), m_OldHistoryFilePointer.ToString(), m_Rooms.ToString(), m_SquareFeet.ToString(), m_Shares.ToString(), m_SecMultiple.ToString(),
                                    m_Rent.ToString(), m_Security.ToString(), m_Arrears.ToString(), m_Paid.ToString(), m_FutureRent1.ToString(), m_FutureRent2.ToString(), m_PromptPay1.ToString(), m_ProposedRent2.ToString(), m_PromptPay2.ToString(), m_ProposedLegal2.ToString(),
                                    m_ProposedLegal.ToString(), m_ExemptLastLegalPaid.ToString(), m_LateBase.ToString(), m_RentLegal.ToString(), m_FutureLegal1.ToString(), m_FutureLegal2.ToString(), m_FutureLegal3.ToString(), m_ProposedRent.ToString(), m_FutureRent3.ToString(), m_MBR.ToString(),
                                    m_TotalCharges.ToString(), m_dhcrRentOne.ToString(), m_dhcrRentTwo.ToString(), m_LateFee.ToString(), m_NewTenantFlag.ToString(), m_AlternateBillingAddressFlag.ToString(), m_AdditionalNameFlag.ToString(), m_LeaseLetterFlag.ToString(), m_LowRentFlag.ToString(),
                                    m_dhcrStoveOne.ToString(), m_ExemptSec11243.ToString(), m_ExemptOther.ToString(), m_ExemptExpSec608.ToString(), m_ExemptExp421a.ToString(), m_Exempt421aMarketRate.ToString(), m_Exempt421aIncome.ToString(), m_TempExemptOther.ToString(),
                                    m_ExemptHighRentVacancy.ToString(), m_ExemptHighRentIncome.ToString(), m_ExemptCommercialProfessionalCo.ToString(), m_ExemptSubstantialRehab.ToString(), m_ExemptCoopCondoOwner.ToString(), m_RentOvercharge.ToString(), m_FairMarketAppeal.ToString(),
                                    m_TransientOccupant.ToString(), m_OwnerOccupied.ToString(), m_CommercialProfessional.ToString(), m_NotPrimeResidence.ToString(), m_LegalFeeNo.ToString(), m_LateFeeYes.ToString(), m_LateFeeNo.ToString(), m_Succeeded.ToString(), m_SecondSuccession.ToString(),
                                    m_421a.ToString(), m_dhcrRefrigeratorTwo.ToString(), m_dhcrDishwasherTwo.ToString(), m_dhcrACTwo.ToString(), m_dhcrWindowsTwo.ToString(), m_dhcrOtherTwo.ToString(), m_LegalFee.ToString(), m_dhcrRefrigeratorOne.ToString(), m_dhcrDishwasherOne.ToString(),
                                    m_dhcrACOne.ToString(), m_dhcrWindowsOne.ToString(), m_dhcrOtherOne.ToString(), m_dhcrStoveTwo.ToString(), m_ReplacingTenant.ToString(), m_clientId.ToString(), m_BuildingNumber, m_TenantNumber, m_TenantName, m_Apartment, m_DueDate, m_Type, 
                                    m_SecurityAccount, m_SecurityBuilding, m_ExemptOtherDesc, m_Pager, m_dhcrOtherTextOne, m_dhcrOtherTextTwo, m_Cell2, m_Email1, m_Email2, m_AltBillAdd3, m_AddName, m_Bank, m_InterestInfo, m_Comment, m_Seniorcit, m_HomePhone2, m_BusPhone1, m_BusPhone2,
                                    m_AltBillName, m_AltBillAdd1, m_AltBillAdd2, m_LegalCode, m_LegalCode2, m_AdditionalName, m_SSNumber1, m_SSNumber2, m_HomePhone1, m_DecontrolledLegal.ToString(), m_LatePercent.ToString(),
                                    m_dhcrSubmittedDateOne.ToString(), m_dhcrSubmittedDateTwo.ToString(), m_dhcrTotalCostOne.ToString(), m_dhcrTotalCostTwo.ToString(), m_dhcrSubmittedOne.ToString(), m_dhcrSubmittedTwo.ToString(), m_dhcrInformedConsentOne.ToString(), 
                                    m_dhcrInformedConsentTwo.ToString(), m_UseACH.ToString(), m_PaymentDay.ToString(), m_SavingsAccount.ToString(), m_EmployeeOccupied.ToString(), m_XrfTested.ToString(), m_XrfExempt.ToString(), m_xrfTestedDate.ToString(), m_xrfExemptDate.ToString(), m_DirectPay.ToString(),
                                    m_xrfWorkComplete.ToString(), m_xrfWorkCompleteDate.ToString(), m_ProposedRent2_1.ToString(), m_SequenceNumber.ToString() };
                if (this.updateRecord(m_SequenceNumber.ToString(), values, "Tenants", debug))
                {
                    return true;
                } else {
                    return false;
                }
            } else {
                string[] values = { m_FutureDate1.ToString(), m_FutureDate2.ToString(), m_FutureDate3.ToString(), m_MoveinDate.ToString(), m_LeaseStartDate.ToString(), m_LeaseExpirationDate.ToString(), m_LastPaidDate.ToString(), m_dhcrDateOne.ToString(), m_dhcrDateTwo.ToString(),
                                    m_ExemptDate.ToString(), m_OptTenant1.ToString(), m_OptTenant2.ToString(), m_ExemptHighRentVacanyPeriod.ToString(), m_Exempt421aMedianIncome.ToString(), m_OldHistoryFilePointer.ToString(), m_Rooms.ToString(), m_SquareFeet.ToString(), m_Shares.ToString(),
                                    m_SecMultiple.ToString(), m_Rent.ToString(), m_Security.ToString(), m_Arrears.ToString(), m_Paid.ToString(), m_FutureRent1.ToString(), m_FutureRent2.ToString(), m_PromptPay1.ToString(), m_ProposedRent2.ToString(), m_PromptPay2.ToString(), m_ProposedLegal2.ToString(),
                                    m_ProposedLegal.ToString(), m_ExemptLastLegalPaid.ToString(), m_LateBase.ToString(), m_RentLegal.ToString(), m_FutureLegal1.ToString(), m_FutureLegal2.ToString(), m_FutureLegal3.ToString(), m_ProposedRent.ToString(), m_FutureRent3.ToString(), m_MBR.ToString(),
                                    m_TotalCharges.ToString(), m_dhcrRentOne.ToString(), m_dhcrRentTwo.ToString(), m_LateFee.ToString(), m_NewTenantFlag.ToString(), m_AlternateBillingAddressFlag.ToString(), m_AdditionalNameFlag.ToString(), m_LeaseLetterFlag.ToString(), m_LowRentFlag.ToString(),
                                    m_dhcrStoveOne.ToString(), m_ExemptSec11243.ToString(), m_ExemptOther.ToString(), m_ExemptExpSec608.ToString(), m_ExemptExp421a.ToString(), m_Exempt421aMarketRate.ToString(), m_Exempt421aIncome.ToString(), m_TempExemptOther.ToString(),
                                    m_ExemptHighRentVacancy.ToString(), m_ExemptHighRentIncome.ToString(), m_ExemptCommercialProfessionalCo.ToString(), m_ExemptSubstantialRehab.ToString(), m_ExemptCoopCondoOwner.ToString(), m_RentOvercharge.ToString(), m_FairMarketAppeal.ToString(),
                                    m_TransientOccupant.ToString(), m_OwnerOccupied.ToString(), m_CommercialProfessional.ToString(), m_NotPrimeResidence.ToString(), m_LegalFeeNo.ToString(), m_LateFeeYes.ToString(), m_LateFeeNo.ToString(), m_Succeeded.ToString(), m_SecondSuccession.ToString(),
                                    m_421a.ToString(), m_dhcrRefrigeratorTwo.ToString(), m_dhcrDishwasherTwo.ToString(), m_dhcrACTwo.ToString(), m_dhcrWindowsTwo.ToString(), m_dhcrOtherTwo.ToString(), m_LegalFee.ToString(), m_dhcrRefrigeratorOne.ToString(), m_dhcrDishwasherOne.ToString(),
                                    m_dhcrACOne.ToString(), m_dhcrWindowsOne.ToString(), m_dhcrOtherOne.ToString(), m_dhcrStoveTwo.ToString(), m_ReplacingTenant.ToString(), m_clientId.ToString(), m_BuildingNumber, m_TenantNumber, m_TenantName, m_Apartment, m_DueDate, m_Type, 
                                    m_SecurityAccount, m_SecurityBuilding, m_ExemptOtherDesc, m_Pager, m_dhcrOtherTextOne, m_dhcrOtherTextTwo, m_Cell2, m_Email1, m_Email2, m_AltBillAdd3, m_AddName, m_Bank, m_InterestInfo, m_Comment, m_Seniorcit, m_HomePhone2, m_BusPhone1, m_BusPhone2, m_AltBillName,
                                    m_AltBillAdd1, m_AltBillAdd2, m_LegalCode, m_LegalCode2, m_AdditionalName, m_SSNumber1, m_SSNumber2, m_HomePhone1, m_DecontrolledLegal.ToString(), m_LatePercent.ToString(),
                                    m_dhcrSubmittedDateOne.ToString(), m_dhcrSubmittedDateTwo.ToString(), m_dhcrTotalCostOne.ToString(), m_dhcrTotalCostTwo.ToString(), m_dhcrSubmittedOne.ToString(), m_dhcrSubmittedTwo.ToString(), m_dhcrInformedConsentOne.ToString(),
                                    m_dhcrInformedConsentTwo.ToString(), m_UseACH.ToString(), m_Payment.ToString(), m_SavingsAccount.ToString(), m_EmployeeOccupied.ToString(), m_XrfTested.ToString(), m_XrfExempt.ToString(), m_xrfTestedDate.ToString(), m_xrfExemptDate.ToString(), m_DirectPay.ToString(),
                                    m_xrfWorkComplete.ToString(), m_xrfWorkCompleteDate.ToString(), m_ProposedRent2_1.ToString()};
                m_SequenceNumber = this.insertRecord(values);
                if (m_SequenceNumber >= 0) {
                    return true;
                }
            }
            return false;

        }

        public bool saveForPaymentEntry(bool debug = false)
        {
            string[] values = { m_LastPaidDate.ToString(), m_Security.ToString(), m_LegalCode, m_Paid.ToString(), m_TotalCharges.ToString(), m_MBR.ToString(), m_SequenceNumber.ToString() };
            if (this.updateRecordSpecial(m_SequenceNumber.ToString(), values, SQL_UPDATE_PAYMENT, "Tenants", debug))
            {
                return true;
            }
            else
            {
                return false;
            }

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
                Tenants[] tens = Tenants.loadForDeletion(bld);
                if ((tens != null) && tens.Length > 0) {
                    Charges[] chgs = null;
                    Legal[] lgls = null;
                    Occupants[] ocps = null;
                    foreach (Tenants ten in tens) {
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
                Interaction.MsgBox("Error deleting Tenants for Building " + bld + ". Type: " + ex.GetType().ToString() + ", message: " + ex.Message);
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

        public long[] TenantDeleteAll()
        {
            long[] counts = { 0, 0, 0 };
            int key = this.SequenceNumber;
            this.delete(key.ToString());

            // History
            Histmast histMast = Histmast.load(key);
            if (histMast != null)
                histMast.delete(histMast.ID.ToString());
            HistChg chg = new HistChg();
            counts[0] = chg.queryWithCount("DELETE FROM " + HistChg.TABLE + " WHERE [" + HistChg.SQL_COL_SEQUENCE_NUMBER + "] = " + key.ToString(), null);
            HistPay pay = new HistPay();
            counts[1] = pay.queryWithCount("DELETE FROM " + HistPay.TABLE + " WHERE [" + HistPay.SQL_COL_SEQUENCE_NUMBER + "] = " + key.ToString(), null);

            // Security
            STTenants ten = STTenants.loadBySequence(key);
            if (ten != null)
                ten.delete(ten.ID.ToString());
            STTrans tran = new STTrans();
            counts[2] = tran.queryWithCount("DELETE FROM " + STTrans.TABLE + " WHERE [" + STTrans.SQL_COL_SEQUENCE_NUMBER + "] = " + key.ToString(), null);

            return counts;
        }

        public void ClearDHCRFields(int set)
        {
            if (set == 0)
            {
                dhcrDateOne = GlobalShared.NULL_DATE.Date;
                dhcrACOne = false;
                dhcrDishwasherOne = false;
                dhcrOtherOne = false;
                dhcrOtherTextOne = "";
                dhcrRefrigeratorOne = false;
                dhcrRentOne = 0;
                dhcrStoveOne = false;
                dhcrWindowsOne = false;
                dhcrInformedConsentOne = false;
                dhcrSubmittedDateOne = GlobalShared.NULL_DATE.Date;
                dhcrSubmittedOne = false;
                dhcrTotalCostOne = 0;
                this.save();
            }
            else
            {
                dhcrDateTwo = GlobalShared.NULL_DATE.Date;
                dhcrACTwo = false;
                dhcrDishwasherTwo = false;
                dhcrOtherTwo = false;
                dhcrOtherTextTwo = "";
                dhcrRefrigeratorTwo = false;
                dhcrRentTwo = 0;
                dhcrStoveTwo = false;
                dhcrWindowsTwo = false;
                dhcrInformedConsentTwo = false;
                dhcrSubmittedDateTwo = GlobalShared.NULL_DATE.Date;
                dhcrSubmittedTwo = false;
                dhcrTotalCostTwo = 0;
                this.save();
            }

        }

        public Tenants Clone()
        {

            return (Tenants)this.MemberwiseClone();

        }

        public TenantsArchive copyToArchive()
        {
            TenantsArchive tenArch = new TenantsArchive();

            try
            {
                tenArch.BuildingNumber = this.BuildingNumber;
                tenArch.TenantNumber = this.TenantNumber;
                tenArch.SequenceNumber = this.SequenceNumber;
                tenArch.OldHistoryFilePointer = this.OldHistoryFilePointer;
                tenArch.NewTenantFlag = this.NewTenantFlag;
                tenArch.ReplacingTenant = this.ReplacingTenant;
                tenArch.TenantName = this.TenantName;
                tenArch.Apartment = this.Apartment;
                tenArch.DueDate = this.DueDate;
                tenArch.Type = this.Type;
                tenArch.AlternateBillingAddressFlag = this.AlternateBillingAddressFlag;
                tenArch.AdditionalNameFlag = this.AdditionalNameFlag;
                tenArch.LeaseLetterFlag = this.LeaseLetterFlag;
                tenArch.LowRentFlag = this.LowRentFlag;
                tenArch.LegalCode = this.LegalCode;
                tenArch.LegalCode2 = this.LegalCode2;
                tenArch.Rent = this.Rent;
                tenArch.Security = this.Security;
                tenArch.Arrears = this.Arrears;
                tenArch.Paid = this.Paid;
                //tenArch.FutureRent1 = this.FutureRent1;
                //tenArch.FutureRent2 = this.FutureRent2;
                //tenArch.FutureRent3 = this.FutureRent3;
                tenArch.MBR = this.MBR;
                //tenArch.FutureDate1 = this.FutureDate1;
                //tenArch.FutureDate2 = this.FutureDate2;
                //tenArch.FutureDate3 = this.FutureDate3;
                tenArch.Rooms = this.Rooms;
                tenArch.SquareFeet = this.SquareFeet;
                tenArch.MoveinDate = this.MoveinDate;
                tenArch.LeaseStartDate = this.LeaseStartDate;
                tenArch.LeaseExpirationDate = this.LeaseExpirationDate;
                tenArch.LastPaidDate = this.LastPaidDate;
                tenArch.AdditionalName = this.AdditionalName;
                tenArch.SSNumber1 = this.SSNumber1;
                tenArch.SSNumber2 = this.SSNumber2;
                tenArch.HomePhone1 = this.HomePhone1;
                tenArch.HomePhone2 = this.HomePhone2;
                tenArch.BusPhone1 = this.BusPhone1;
                tenArch.BusPhone2 = this.BusPhone2;
                tenArch.AltBillName = this.AltBillName;
                tenArch.AltBillAdd1 = this.AltBillAdd1;
                tenArch.AltBillAdd2 = this.AltBillAdd2;
                tenArch.AltBillAdd3 = this.AltBillAdd3;
                tenArch.AddName = this.AddName;
                tenArch.Bank = this.Bank;
                tenArch.InterestInfo = this.InterestInfo;
                tenArch.Shares = this.Shares;
                tenArch.Comment = this.Comment;
                tenArch.Seniorcit = this.Seniorcit;
                tenArch.TotalCharges = this.TotalCharges;
                tenArch.Pager = this.Pager;
                tenArch.dhcrDateOne = this.m_dhcrDateOne;
                tenArch.dhcrRentOne = this.dhcrRentOne;
                tenArch.dhcrStoveOne = this.dhcrStoveOne;
                tenArch.dhcrRefrigeratorOne = this.dhcrRefrigeratorOne;
                tenArch.dhcrDishwasherOne = this.dhcrDishwasherOne;
                tenArch.dhcrACOne = this.dhcrACOne;
                tenArch.dhcrWindowsOne = this.dhcrWindowsOne;
                tenArch.dhcrOtherOne = this.dhcrOtherOne;
                tenArch.dhcrOtherTextOne = this.dhcrOtherTextOne;
                tenArch.dhcrDateTwo = this.dhcrDateTwo;
                tenArch.dhcrRentTwo = this.dhcrRentTwo;
                tenArch.dhcrStoveTwo = this.dhcrStoveTwo;
                tenArch.dhcrRefrigeratorTwo = this.dhcrRefrigeratorTwo;
                tenArch.dhcrDishwasherTwo = this.dhcrDishwasherTwo;
                tenArch.dhcrACTwo = this.dhcrACTwo;
                tenArch.dhcrWindowsTwo = this.dhcrWindowsTwo;
                tenArch.dhcrOtherTwo = this.dhcrOtherTwo;
                tenArch.dhcrOtherTextTwo = this.dhcrOtherTextTwo;
                tenArch.LegalFee = this.LegalFee;
                tenArch.LateFee = this.LateFee;
                tenArch.LateBase = this.LateBase;
                tenArch.LegalFeeNo = this.LegalFeeNo;
                tenArch.LateFeeYes = this.LateFeeYes;
                tenArch.LateFeeNo = this.LateFeeNo;
                tenArch.RentLegal = this.RentLegal;
                //tenArch.FutureLegal1 = this.FutureLegal1;
                //tenArch.FutureLegal2 = this.FutureLegal2;
                //tenArch.FutureLegal3 = this.FutureLegal3;
                tenArch.ProposedRent = this.ProposedRent;
                tenArch.PromptPay1 = this.PromptPay1;
                tenArch.ProposedRent2 = this.ProposedRent2;
                tenArch.PromptPay2 = this.PromptPay2;
                tenArch.ProposedLegal2 = this.ProposedLegal2;
                tenArch.ProposedLegal = this.ProposedLegal;
                tenArch.Cell2 = this.Cell2;
                tenArch.SecMultiple = this.SecMultiple;
                tenArch.Email1 = this.Email1;
                tenArch.Email2 = this.Email2;
                //tenArch.Temp1 = this.Temp1;
                //tenArch.Temp2 = this.Temp2;
                tenArch.SecurityAccount = this.SecurityAccount;
                tenArch.OptTenant1 = this.OptTenant1;
                tenArch.OptTenant2 = this.OptTenant2;
                tenArch.SecurityBuilding = this.SecurityBuilding;
                tenArch.Succeeded = this.Succeeded;
                tenArch.SecondSuccession = this.SecondSuccession;
                tenArch.x421a = this.x421a;
                tenArch.RentOvercharge = this.RentOvercharge;
                tenArch.FairMarketAppeal = this.FairMarketAppeal;
                tenArch.TransientOccupant = this.TransientOccupant;
                tenArch.OwnerOccupied = this.OwnerOccupied;
                tenArch.CommercialProfessional = this.CommercialProfessional;
                tenArch.NotPrimeResidence = this.NotPrimeResidence;
                tenArch.TempExemptOther = this.TempExemptOther;
                tenArch.ExemptDate = this.ExemptDate;
                tenArch.ExemptHighRentVacancy = this.ExemptHighRentVacancy;
                tenArch.ExemptLastLegalPaid = this.ExemptLastLegalPaid;
                tenArch.ExemptHighRentVacanyPeriod = this.ExemptHighRentVacanyPeriod;
                tenArch.ExemptHighRentIncome = this.ExemptHighRentIncome;
                tenArch.ExemptCommercialProfessionalCo = this.ExemptCommercialProfessionalCo;
                tenArch.ExemptSubstantialRehab = this.ExemptSubstantialRehab;
                tenArch.ExemptCoopCondoOwner = this.ExemptCoopCondoOwner;
                tenArch.ExemptSec11243 = this.ExemptSec11243;
                tenArch.ExemptOther = this.ExemptOther;
                tenArch.ExemptOtherDesc = this.ExemptOtherDesc;
                tenArch.ExemptExpSec608 = this.ExemptExpSec608;
                tenArch.ExemptExp421a = this.ExemptExp421a;
                tenArch.Exempt421aMarketRate = this.Exempt421aMarketRate;
                tenArch.Exempt421aIncome = this.Exempt421aIncome;
                tenArch.Exempt421aMedianIncome = this.Exempt421aMedianIncome;
                
            }
            catch (SqlException sqlex)
            {
                MessageBoxCustom.Show("SQL Error archiving tenant: " + sqlex.Message);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Error archiving tenant: " + ex.Message);
            }

            return tenArch;
        }

        public static bool CreateTable(System.Data.SqlClient.SqlConnection CN)
        {

            try {
                CN.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE Tenants (Building Number TEXT(3),Tenant Number TEXT(4),Sequence Number AUTONUMBER,Old History File Pointer LONG,New Tenant Flag YESNO,Replacing Tenant LONG,Tenant Name TEXT(30),Apartment TEXT(4),Due Date TEXT(2),Type TEXT(2),Alternate Billing Address Flag YESNO,Additional Name Flag YESNO,Lease Letter Flag YESNO,Low Rent Flag YESNO,Legal Code TEXT(1),Legal Code 2 TEXT(1),Rent CURRENCY,Security CURRENCY,Arrears CURRENCY,Paid CURRENCY,Future Rent 1 CURRENCY,Future Rent 2 CURRENCY,Future Rent 3 CURRENCY,MBR CURRENCY,Future Date 1 DATETIME,Future Date 2 DATETIME,Future Date 3 DATETIME,Rooms SINGLE,Square Feet SINGLE,Move-in Date DATETIME,Lease Start Date DATETIME,Lease Expiration Date DATETIME,Last Paid Date DATETIME,Additional Name TEXT(30),SSNumber1 TEXT(11),SSNumber2 TEXT(11),HomePhone1 TEXT(12),HomePhone2 TEXT(12),BusPhone1 TEXT(21),BusPhone2 TEXT(21),AltBillName TEXT(30),AltBillAdd1 TEXT(30),AltBillAdd2 TEXT(30),AltBillAdd3 TEXT(30),AddName TEXT(30),Bank TEXT(60),InterestInfo TEXT(18),Shares SINGLE,Comment MEMO,Seniorcit TEXT(18),Total Charges CURRENCY,Pager TEXT(12),dhcrDateOne DATETIME,dhcrRentOne CURRENCY,dhcrStoveOne YESNO,dhcrRefrigeratorOne YESNO,dhcrDishwasherOne YESNO,dhcrACOne YESNO,dhcrWindowsOne YESNO,dhcrOtherOne YESNO,dhcrOtherTextOne TEXT(16),dhcrDateTwo DATETIME,dhcrRentTwo CURRENCY,dhcrStoveTwo YESNO,dhcrRefrigeratorTwo YESNO,dhcrDishwasherTwo YESNO,dhcrACTwo YESNO,dhcrWindowsTwo YESNO,dhcrOtherTwo YESNO,dhcrOtherTextTwo TEXT(16),LegalFee YESNO,LateFee CURRENCY,LateBase CURRENCY,LegalFeeNo YESNO,LateFeeYes YESNO,LateFeeNo YESNO,RentLegal CURRENCY,Future Legal 1 CURRENCY,Future Legal 2 CURRENCY,Future Legal 3 CURRENCY,ProposedRent CURRENCY,PromptPay1 CURRENCY,ProposedRent2 CURRENCY,PromptPay2 CURRENCY,ProposedLegal2 CURRENCY,ProposedLegal CURRENCY,Cell2 TEXT(12),SecMultiple SINGLE,Email1 TEXT(64),Email2 TEXT(64),SecurityAccount TEXT(32),OptTenant1 INTEGER,OptTenant2 INTEGER,SecurityBuilding TEXT(32))";
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

        public long ClientId
        {
            get { return m_clientId; }
            set
            {
                m_clientId = value;
                OnPropertyChanged(("ClientId"));
            }
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
                m_TenantNumber = value.ToUpper();
                OnPropertyChanged("TenantNumber");
            }
        }

        public string BuildingTenantNumber
        {
            get { return BuildingNumber + TenantNumber; }
        }

        public int SequenceNumber {
            get { return m_SequenceNumber; }
            set { m_SequenceNumber = value; }
        }

        public Int32 OldHistoryFilePointer {
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
                LateFeePlusRent = 0;
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
                if (m_Arrears != value) // Added because frmTenant was causing NumberEntry.OnLoad to be fired.
                {
                    m_Arrears = value;
                    OnPropertyChanged("Arrears");
                }
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
            get { return Future1.FutureAmount; }
            set {
                m_FutureRent1 = value;
                OnPropertyChanged("FutureRent1");
            }
        }

        public decimal FutureRent2 {
            get { return Future2.FutureAmount; }
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
            get { if (Future1 == null || Future1.FutureDate == null)
                    return GlobalShared.NULL_DATE;
                return Future1.FutureDate; }
            set {
                m_FutureDate1 = value;
                OnPropertyChanged("FutureDate1");
            }
        }

        public DateTime FutureDate2 {
            get
            {
                if (Future2 == null || Future2.FutureDate == null)
                    return GlobalShared.NULL_DATE;
                return Future2.FutureDate; }
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
                if (GlobalShared.IsChanged)
                    OnPropertyChanged("dhcrDateOne");
            }
        }
        /*
        public string dhcrDateOne
        {
            get { return m_dhcrDateOne.ToString(); }
            set
            {
                m_dhcrDateOne = DateTime.Parse(value);
                if (GlobalShared.IsChanged)
                    OnPropertyChanged("dhcrDateOne");
            }
        }
        */
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
                if (GlobalShared.IsChanged)
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
                LateFeePlusRent = 0;
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
            get { return Future1.FutureLegal; }
            set {
                m_FutureLegal1 = value;
                OnPropertyChanged("FutureLegal1");
            }
        }

        public decimal FutureLegal2 {
            get { return Future2.FutureLegal; }
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

        /*
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
        */

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

        public string ExemptDateText
        {
            get { return string.Format("{0:MM/dd/yyyy}", m_ExemptDate); }
            set { m_ExemptDate = DateTime.Parse(value); }
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

        public bool isHighRentVacancyPeriodWeekly
        {
            get { return ExemptHighRentVacanyPeriod == (int)HighRentPeriod.Weekly ? true : false; }
            set { ExemptHighRentVacanyPeriod = (short)(value ? (int)HighRentPeriod.Weekly: (int)HighRentPeriod.Monthly);
                  OnPropertyChanged("isHighRentVacanyPeriodWeekly");
            }
        }

        public bool isHighRentVacancyPeriodMonthly
        {
            get { return ExemptHighRentVacanyPeriod == (int)HighRentPeriod.Monthly ? true : false; }
            set
            {   ExemptHighRentVacanyPeriod = (short)(value ? (int)HighRentPeriod.Monthly : (int)HighRentPeriod.Weekly);
                OnPropertyChanged("isHighRentVacanyPeriodMonthly");
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

        public string BuildingTenant
        {
            get { return m_BuildingNumber + m_TenantNumber; }
        }

        public decimal Balance
        {
            get { return m_Rent + m_Arrears + m_TotalCharges - m_Paid; }
        }
        public DateTime DatePaid
        {
            get { return m_DatePaid; }
            set
            {
                m_DatePaid = value;
                OnPropertyChanged("DatePaid");
            }
        }
        public decimal Payment
        {
            get { return m_Payment; }
            set
            {
                m_Payment = value;
                OnPropertyChanged("Payment");
            }
        }
        public decimal DecontrolledLegal
        {
            get { return m_DecontrolledLegal; }
            set
            {
                m_DecontrolledLegal = value;
                OnPropertyChanged("DecontrolledLegal");
            }
        }
        public bool Post
        {
            get { return m_Post; }
            set
            {
                m_Post = value;
                OnPropertyChanged("Post");
            }
        }
        public decimal LateFeePlusRent
        {
            get { return Rent + LateFee; }
            set { OnPropertyChanged("LateFeePlusRent"); }
        }
        public bool LatePercent
        {
            get { return m_LatePercent; }
            set
            {
                m_LatePercent = value;
                OnPropertyChanged("LatePercent");
            }
        }

        public DateTime dhcrSubmittedDateOne
        {
            set
            {
                m_dhcrSubmittedDateOne = value;
                OnPropertyChanged("dhcrSubmittedDateOne");
            }
            get { return m_dhcrSubmittedDateOne; }
        }

        public DateTime dhcrSubmittedDateTwo
        {
            set
            {
                m_dhcrSubmittedDateTwo = value;
                OnPropertyChanged("dhcrSubmittedDateTwo");
            }
            get { return m_dhcrSubmittedDateTwo; }
        }

        public decimal dhcrTotalCostOne
        {
            set
            {
                m_dhcrTotalCostOne = value;
                OnPropertyChanged("dhcrTotalCostOne");
            }
            get { return m_dhcrTotalCostOne; }
        }

        public decimal dhcrTotalCostTwo
        {
            set
            {
                m_dhcrTotalCostTwo = value;
                OnPropertyChanged("dhcrTotalCostTwo");
            }
            get { return m_dhcrTotalCostTwo; }
        }

        public bool dhcrSubmittedOne
        {
            set
            {
                m_dhcrSubmittedOne = value;
                OnPropertyChanged("dhcrSubmittedOne");
            }
            get { return m_dhcrSubmittedOne; }
        }

        public bool dhcrSubmittedTwo
        {
            set
            {
                m_dhcrSubmittedTwo = value;
                OnPropertyChanged("dhcrSubmittedTwo");
            }
            get { return m_dhcrSubmittedTwo; }
        }

        public bool dhcrInformedConsentOne
        {
            set
            {
                m_dhcrInformedConsentOne = value;
                OnPropertyChanged("dhcrInformedConsentOne");
            }
            get { return m_dhcrInformedConsentOne; }
        }

        public bool dhcrInformedConsentTwo
        {
            set
            {
                m_dhcrInformedConsentTwo = value;
                OnPropertyChanged("dhcrInformedConsentTwo");
            }
            get { return m_dhcrInformedConsentTwo; }
        }

        public bool UseACH
        {
            set
            {
                m_UseACH = value;
                OnPropertyChanged("UseACH");
            }
            get { return m_UseACH; }
        }

        public int PaymentDay
        {
            set
            {
                m_PaymentDay = value;
                OnPropertyChanged("PaymentDay");
            }
            get { return m_PaymentDay; }
        }

        public bool SavingsAccount
        {
            set
            {
                m_SavingsAccount = value;
                OnPropertyChanged("SavingsAccount");
            }
            get { return m_SavingsAccount; }
        }

        public bool EmployeeOccupied
        {
            set
            {
                m_EmployeeOccupied = value;
                OnPropertyChanged("EmployeeOccupied");
            }
            get { return m_EmployeeOccupied; }
        }

        public bool XrfTested
        {
            set
            {
                m_XrfTested = value;
                OnPropertyChanged("XrfTested");
            }
            get { return m_XrfTested; }
        }

        public bool XrfExempt
        {
            set
            {
                m_XrfExempt = value;
                OnPropertyChanged("XrfExempt");
            }
            get { return m_XrfExempt; }
        }

        public DateTime XrfTestedDate
        {
            set
            {
                m_xrfTestedDate = value;
                OnPropertyChanged("XrfTestedDate");
            }
            get { return m_xrfTestedDate; }
        }

        public DateTime XrfExemptDate
        {
            set
            {
                m_xrfExemptDate = value;
                OnPropertyChanged("XrfExemptDate");
            }
            get { return m_xrfExemptDate; }
        }


        public bool DirectPay
        {
            get { return m_DirectPay; }
            set 
            { 
                m_DirectPay = value;
                OnPropertyChanged("DirectPay");
            }
        }

        public bool XrfWorkComplete
        {
            get
            {
                return m_xrfWorkComplete;
            }
            set
            {
                m_xrfWorkComplete = value;
                OnPropertyChanged("XrfWorkComplete");
            }
        }

        public DateTime XrfWorkCompleteDate
        {
            get
            {
                return m_xrfWorkCompleteDate;
                OnPropertyChanged("XrfWorkCompleteDate");
            }
            set
            {
                m_xrfWorkCompleteDate = value;
                OnPropertyChanged("XffWorkCompleteDate");
            }
        }

        public decimal ProposedRent2_1
        {
            get
            {
                return m_ProposedRent2_1;
            }
            set
            {
                m_ProposedRent2_1 = value;
                OnPropertyChanged("ProposedRent2_1");
            }
        }
        public decimal CalculatedLateFee
        {
            get; set;
        } = 0;

        public Charges chg { get; set; }

        public string NumberName { get { return BuildingNumber + TenantNumber + ": " + TenantName; } }

        [XmlIgnore]
        public FutureRents Future1 { get; set; } = new FutureRents();
        [XmlIgnore]
        public FutureRents Future2 { get; set; } = new FutureRents();

        public decimal TotalArrears { get; set; } = 0;

        public bool CanSendEmail { get; set; } = false;

        public bool SendEmail 
        { 
            get { return m_SendEmail; } 
            set 
            { 
                m_SendEmail = value;
                OnPropertyChanged("SendEmail");  
            } 
        }

        public bool IsEmail1 { get; set; } = false;

        public bool UseEmail1 
        {
            get { return m_UseEmail1; }
            set
            {
                m_UseEmail1 = value;
                OnPropertyChanged("UseEmail1");
            }
        }

        public bool IsEmail2 { get; set; } = false;

        public bool UseEmail2
        {
            get { return m_UseEmail2; }
            set
            {
                m_UseEmail2 = value;
                OnPropertyChanged("UseEmail2");
            }
        }

        public bool UseDC { get; set; } = false;
       
        //NACHA

        public bool ChargeLateFee
        {
            get { return m_ChargeLateFee; }
            set
            {
                m_ChargeLateFee = value;
                OnPropertyChanged("ChargeLateFee");
            }
        }

        // Bank Forms
        public decimal ChargesPaid 
        {
            get { return m_ChargesPaid; }
            set { m_ChargesPaid = value; } 
        
        }
        public string BuildingAddress
        {
            get { return m_BuildingAddress; }
            set { m_BuildingAddress = value; }
        }
        public string BuildingAccount
        {
            get { return m_BuildingAccount; }
            set { m_BuildingAccount = value; }
        }
        public string OwnersName
        {
            get { return m_OwnersName; }
            set { m_OwnersName = value; }
        }
        public string OwnerAddress1
        {
            get { return m_OwnerAddr1; }
            set { m_OwnerAddr1 = value; }
        }
        public string OwnerAddress2
        {
            get { return m_OwnerAddr2; }
            set { m_OwnerAddr2 = value; }
        }
        public string MasterAccount
        {
            get { return m_BuildingAccount; }
            set { m_BuildingAccount = value; }
        }


        public decimal CalculatedArrears { get; set; }


        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString().Replace("\r", " ").Replace("\n", "");

        }

        public static Tenants DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tenants));
            StringReader rdr = new StringReader(inputString);
            Tenants parms = (Tenants)serializer.Deserialize(rdr);
            return parms;
        }
    }


}
                     