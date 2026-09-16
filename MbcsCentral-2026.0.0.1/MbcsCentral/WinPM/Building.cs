using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualBasic;
namespace MbcsCentral.WinPM
{
	public class Building : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		public static string TABLE = "pm_Building";
        public const string SQL_COL_NON_EVICTION_DATE = "NonEvictionDate";
        public const string SQL_COL_EVICTION_DATE = "EvictionDate";
        public const string SQL_COL_ACCEPTED_FOR_FILING_DATE = "AcceptedForFilingDate";
        public const string SQL_COL_OWNER_NUMBER = "Owner Number";
        public const string SQL_COL_AGENT_IS = "AgentIs";
        public const string SQL_COL_TOTAL_SHARES = "Total Shares";
        public const string SQL_COL_D_H_C_R_COUNTY = "DHCRCounty";
        public const string SQL_COL_D_H_C_R_MUNICIPALITY = "DHCRMunicipality";
        public const string SQL_COL_D_H_C_R_COUNTY2 = "DHCRCounty2";
        public const string SQL_COL_D_H_C_R_MUNICIPALITY2 = "DHCRMunicipality2";
        public const string SQL_COL_D_H_C_R_COUNTY3 = "DHCRCounty3";
        public const string SQL_COL_D_H_C_R_MUNICIPALITY3 = "DHCRMunicipality3";
        public const string SQL_COL_RENT_CHANGE_M_C_I = "RentChangeMCI";
        public const string SQL_COL_RENT_CHANGE_HARDSHIP = "RentChangeHardship";
        public const string SQL_COL_COOP_CONDO = "CoopCondo";
        public const string SQL_COL_REAL_ESTATE_TAXES = "Real Estate Taxes";
        public const string SQL_COL_MORTGAGE_INTEREST = "Mortgage Interest";
        public const string SQL_COL_I_D = "ID";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_BUILDING_NUMBER = "Building Number";
        public const string SQL_COL_ADDRESS_1 = "Address 1";
        public const string SQL_COL_ADDRESS_2 = "Address 2";
        public const string SQL_COL_AGENT_NAME = "Agent Name";
        public const string SQL_COL_BLOCK_NUMBER = "Block Number";
        public const string SQL_COL_LOT_NUMBER = "Lot Number";
        public const string SQL_COL_D_H_C_R_HIGH3 = "DHCRHigh3";
        public const string SQL_COL_D_H_C_R_STREET_NAME3 = "DHCRStreetName3";
        public const string SQL_COL_D_H_C_R_STREET_SUFFIX3 = "DHCRStreetSuffix3";
        public const string SQL_COL_D_H_C_R_CITY3 = "DHCRCity3";
        public const string SQL_COL_D_H_C_R_ZIP3 = "DHCRZip3";
        public const string SQL_COL_D_H_C_R_HIGH2 = "DHCRHigh2";
        public const string SQL_COL_D_H_C_R_STREET_NAME2 = "DHCRStreetName2";
        public const string SQL_COL_D_H_C_R_STREET_SUFFIX2 = "DHCRStreetSuffix2";
        public const string SQL_COL_D_H_C_R_CITY2 = "DHCRCity2";
        public const string SQL_COL_D_H_C_R_ZIP2 = "DHCRZip2";
        public const string SQL_COL_D_H_C_R_LOW3 = "DHCRLow3";
        public const string SQL_COL_D_H_C_R_HIGH = "DHCRHigh";
        public const string SQL_COL_D_H_C_R_STREET_NAME = "DHCRStreetName";
        public const string SQL_COL_D_H_C_R_STREET_SUFFIX = "DHCRStreetSuffix";
        public const string SQL_COL_D_H_C_R_CITY = "DHCRCity";
        public const string SQL_COL_D_H_C_R_ZIP = "DHCRZip";
        public const string SQL_COL_D_H_C_R_LOW2 = "DHCRLow2";
        public const string SQL_COL_MASTER_ACCOUNT = "MasterAccount";
        public const string SQL_COL_AUTH_REP = "AuthRep";
        public const string SQL_COL_FAX_NUMBER = "FaxNumber";
        public const string SQL_COL_AUTH_CODE = "AuthCode";
        public const string SQL_COL_EMAIL = "email";
        public const string SQL_COL_D_H_C_R_LOW = "DHCRLow";
        public const string SQL_COL_SECTION_NUMBER = "Section Number";
        public const string SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER = "Multiple Dwelling Registration Number";
        public const string SQL_COL_R_S_A = "RSA";
        public const string SQL_COL_SENIOR_CITIZEN_NUMBER = "Senior Citizen Number";
        public const string SQL_COL_SUPERINTENDANT_NAME = "Superintendant Name";
        public const string SQL_COL_SUPERINTENDANT_PHONE = "Superintendant Phone";
        public const string SQL_COL_DHCR_MUNICIPALITY = "DHCRMunicipality";
        public const string SQL_COL_DHCR_MUNICIPALITY2 = "DHCRMunicipality2";
        public const string SQL_COL_DHCR_MUNICIPALITY3 = "DHCRMunicipality3";
        public const string SQL_COLUMN_INACTIVE = "InActive";
		public const string SQL_COL_EMAIL2 = "Email2";


	static string SQL_LOAD = "SELECT " +
			"[" + SQL_COL_NON_EVICTION_DATE + "]," +
			"[" + SQL_COL_EVICTION_DATE + "]," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]," +
			"[" + SQL_COL_OWNER_NUMBER + "]," +
			"[" + SQL_COL_AGENT_IS + "]," +
			"[" + SQL_COL_TOTAL_SHARES + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]," +
			"[" + SQL_COL_COOP_CONDO + "]," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_ADDRESS_1 + "]," +
			"[" + SQL_COL_ADDRESS_2 + "]," +
			"[" + SQL_COL_AGENT_NAME + "]," +
			"[" + SQL_COL_BLOCK_NUMBER + "]," +
			"[" + SQL_COL_LOT_NUMBER + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]," +
			"[" + SQL_COL_D_H_C_R_CITY + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]," +
			"[" + SQL_COL_AUTH_REP + "]," +
			"[" + SQL_COL_FAX_NUMBER + "]," +
			"[" + SQL_COL_AUTH_CODE + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_D_H_C_R_LOW + "]," +
			"[" + SQL_COL_SECTION_NUMBER + "]," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," +
			"[" + SQL_COL_R_S_A + "]," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]," +
            "[" + SQL_COLUMN_INACTIVE + "]," +
			"[" + SQL_COL_EMAIL2 + "]" +
            " FROM " + TABLE +
			" WHERE [" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_ALL = " SELECT " +
			"[" + SQL_COL_NON_EVICTION_DATE + "]," +
			"[" + SQL_COL_EVICTION_DATE + "]," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]," +
			"[" + SQL_COL_OWNER_NUMBER + "]," +
			"[" + SQL_COL_AGENT_IS + "]," +
			"[" + SQL_COL_TOTAL_SHARES + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]," +
			"[" + SQL_COL_COOP_CONDO + "]," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_ADDRESS_1 + "]," +
			"[" + SQL_COL_ADDRESS_2 + "]," +
			"[" + SQL_COL_AGENT_NAME + "]," +
			"[" + SQL_COL_BLOCK_NUMBER + "]," +
			"[" + SQL_COL_LOT_NUMBER + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]," +
			"[" + SQL_COL_D_H_C_R_CITY + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]," +
			"[" + SQL_COL_AUTH_REP + "]," +
			"[" + SQL_COL_FAX_NUMBER + "]," +
			"[" + SQL_COL_AUTH_CODE + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_D_H_C_R_LOW + "]," +
			"[" + SQL_COL_SECTION_NUMBER + "]," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," +
			"[" + SQL_COL_R_S_A + "]," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]," +
            "[" + SQL_COLUMN_INACTIVE + "]," +
            "[" + SQL_COL_EMAIL2 + "]" +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_CLIENT_ID + "=?" +
            " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "]";

		static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + 
			"[" + SQL_COL_NON_EVICTION_DATE + "]," +
			"[" + SQL_COL_EVICTION_DATE + "]," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]," +
			"[" + SQL_COL_OWNER_NUMBER + "]," +
			"[" + SQL_COL_AGENT_IS + "]," +
			"[" + SQL_COL_TOTAL_SHARES + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]," +
			"[" + SQL_COL_COOP_CONDO + "]," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_ADDRESS_1 + "]," +
			"[" + SQL_COL_ADDRESS_2 + "]," +
			"[" + SQL_COL_AGENT_NAME + "]," +
			"[" + SQL_COL_BLOCK_NUMBER + "]," +
			"[" + SQL_COL_LOT_NUMBER + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]," +
			"[" + SQL_COL_D_H_C_R_CITY + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]," +
			"[" + SQL_COL_AUTH_REP + "]," +
			"[" + SQL_COL_FAX_NUMBER + "]," +
			"[" + SQL_COL_AUTH_CODE + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_D_H_C_R_LOW + "]," +
			"[" + SQL_COL_SECTION_NUMBER + "]," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," +
			"[" + SQL_COL_R_S_A + "]," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]," +
            "[" + SQL_COLUMN_INACTIVE + "]," +
            "[" + SQL_COL_EMAIL2 + "]" +
            ") VALUES ('?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?');";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
			"[" + SQL_COL_NON_EVICTION_DATE + "]='?'," +
			"[" + SQL_COL_EVICTION_DATE + "]='?'," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]='?'," +
			"[" + SQL_COL_OWNER_NUMBER + "]=?," +
			"[" + SQL_COL_AGENT_IS + "]=?," +
			"[" + SQL_COL_TOTAL_SHARES + "]=?," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]=?," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]=?," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]=?," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]=?," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]=?," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]=?," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]=?," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]=?," +
			"[" + SQL_COL_COOP_CONDO + "]=?," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]=?," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]=?," +
			"[" + SQL_COL_CLIENT_ID + "]=?," +
			"[" + SQL_COL_BUILDING_NUMBER + "]='?'," +
			"[" + SQL_COL_ADDRESS_1 + "]='?'," +
			"[" + SQL_COL_ADDRESS_2 + "]='?'," +
			"[" + SQL_COL_AGENT_NAME + "]='?'," +
			"[" + SQL_COL_BLOCK_NUMBER + "]='?'," +
			"[" + SQL_COL_LOT_NUMBER + "]='?'," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]='?'," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]='?'," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]='?'," +
			"[" + SQL_COL_D_H_C_R_CITY + "]='?'," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]='?'," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]='?'," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]='?'," +
			"[" + SQL_COL_AUTH_REP + "]='?'," +
			"[" + SQL_COL_FAX_NUMBER + "]='?'," +
			"[" + SQL_COL_AUTH_CODE + "]='?'," +
			"[" + SQL_COL_EMAIL + "]='?'," +
			"[" + SQL_COL_D_H_C_R_LOW + "]='?'," +
			"[" + SQL_COL_SECTION_NUMBER + "]='?'," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]='?'," +
			"[" + SQL_COL_R_S_A + "]='?'," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]='?'," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]='?'," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]='?'," +
            "[" + SQL_COLUMN_INACTIVE + "]=?," +
            "[" + SQL_COL_EMAIL2 + "]='?'" +
" WHERE " + SQL_COL_I_D + " = ?;";

        static string SQL_LOAD_BY_BUILDING_NUMBER = " SELECT " + 
			"[" + SQL_COL_NON_EVICTION_DATE + "]," +
			"[" + SQL_COL_EVICTION_DATE + "]," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]," +
			"[" + SQL_COL_OWNER_NUMBER + "]," +
			"[" + SQL_COL_AGENT_IS + "]," +
			"[" + SQL_COL_TOTAL_SHARES + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]," +
			"[" + SQL_COL_COOP_CONDO + "]," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_ADDRESS_1 + "]," +
			"[" + SQL_COL_ADDRESS_2 + "]," +
			"[" + SQL_COL_AGENT_NAME + "]," +
			"[" + SQL_COL_BLOCK_NUMBER + "]," +
			"[" + SQL_COL_LOT_NUMBER + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]," +
			"[" + SQL_COL_D_H_C_R_CITY + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]," +
			"[" + SQL_COL_AUTH_REP + "]," +
			"[" + SQL_COL_FAX_NUMBER + "]," +
			"[" + SQL_COL_AUTH_CODE + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_D_H_C_R_LOW + "]," +
			"[" + SQL_COL_SECTION_NUMBER + "]," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," +
			"[" + SQL_COL_R_S_A + "]," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]," +
            "[" + SQL_COLUMN_INACTIVE + "]," +
            "[" + SQL_COL_EMAIL2 + "]" +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_BUILDING_NUMBER + "] = '?'" +
            " AND " + SQL_COL_CLIENT_ID + "=?";

		static string SQL_LOAD_BY_OWNER_NUMBER = " SELECT " + 
			"[" + SQL_COL_NON_EVICTION_DATE + "]," +
			"[" + SQL_COL_EVICTION_DATE + "]," +
			"[" + SQL_COL_ACCEPTED_FOR_FILING_DATE + "]," +
			"[" + SQL_COL_OWNER_NUMBER + "]," +
			"[" + SQL_COL_AGENT_IS + "]," +
			"[" + SQL_COL_TOTAL_SHARES + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY2 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_COUNTY3 + "]," +
			"[" + SQL_COL_D_H_C_R_MUNICIPALITY3 + "]," +
			"[" + SQL_COL_RENT_CHANGE_M_C_I + "]," +
			"[" + SQL_COL_RENT_CHANGE_HARDSHIP + "]," +
			"[" + SQL_COL_COOP_CONDO + "]," +
			"[" + SQL_COL_REAL_ESTATE_TAXES + "]," +
			"[" + SQL_COL_MORTGAGE_INTEREST + "]," +
			"[" + SQL_COL_I_D + "]," +
			"[" + SQL_COL_CLIENT_ID + "]," +
			"[" + SQL_COL_BUILDING_NUMBER + "]," +
			"[" + SQL_COL_ADDRESS_1 + "]," +
			"[" + SQL_COL_ADDRESS_2 + "]," +
			"[" + SQL_COL_AGENT_NAME + "]," +
			"[" + SQL_COL_BLOCK_NUMBER + "]," +
			"[" + SQL_COL_LOT_NUMBER + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME3 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX3 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY3 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME2 + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX2 + "]," +
			"[" + SQL_COL_D_H_C_R_CITY2 + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP2 + "]," +
			"[" + SQL_COL_D_H_C_R_LOW3 + "]," +
			"[" + SQL_COL_D_H_C_R_HIGH + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_NAME + "]," +
			"[" + SQL_COL_D_H_C_R_STREET_SUFFIX + "]," +
			"[" + SQL_COL_D_H_C_R_CITY + "]," +
			"[" + SQL_COL_D_H_C_R_ZIP + "]," +
			"[" + SQL_COL_D_H_C_R_LOW2 + "]," +
			"[" + SQL_COL_MASTER_ACCOUNT + "]," +
			"[" + SQL_COL_AUTH_REP + "]," +
			"[" + SQL_COL_FAX_NUMBER + "]," +
			"[" + SQL_COL_AUTH_CODE + "]," +
			"[" + SQL_COL_EMAIL + "]," +
			"[" + SQL_COL_D_H_C_R_LOW + "]," +
			"[" + SQL_COL_SECTION_NUMBER + "]," +
			"[" + SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER + "]," +
			"[" + SQL_COL_R_S_A + "]," +
			"[" + SQL_COL_SENIOR_CITIZEN_NUMBER + "]," +
			"[" + SQL_COL_SUPERINTENDANT_NAME + "]," +
			"[" + SQL_COL_SUPERINTENDANT_PHONE + "]," +
            "[" + SQL_COLUMN_INACTIVE + "]," +
            "[" + SQL_COL_EMAIL2 + "]" +
			" FROM " + TABLE +
            " WHERE [" + SQL_COL_OWNER_NUMBER + "] = ?" +
            " AND " + SQL_COL_CLIENT_ID + "=?";

	protected DateTime m_NonEvictionDate;
	protected DateTime m_EvictionDate;
	protected DateTime m_AcceptedForFilingDate;
	protected short m_OwnerNumber;
	protected short m_AgentIs;
	protected int m_TotalShares;
	protected int m_DHCRCounty;
	protected int m_DHCRMunicipality;
	protected int m_DHCRCounty2;
	protected int m_DHCRMunicipality2;
	protected int m_DHCRCounty3;
	protected int m_DHCRMunicipality3;
	protected bool m_RentChangeMCI;
	protected bool m_RentChangeHardship;
	protected bool m_CoopCondo;
	protected decimal m_RealEstateTaxes;
	protected decimal m_MortgageInterest;
	protected int m_ID;
	protected long m_clientId;
	protected string m_BuildingNumber;
	protected string m_Address1;
	protected string m_Address2;
	protected string m_AgentName;
	protected string m_BlockNumber;
	protected string m_LotNumber;
	protected string m_DHCRHigh3;
	protected string m_DHCRStreetName3;
	protected string m_DHCRStreetSuffix3;
	protected string m_DHCRCity3;
	protected string m_DHCRZip3;
	protected string m_DHCRHigh2;
	protected string m_DHCRStreetName2;
	protected string m_DHCRStreetSuffix2;
	protected string m_DHCRCity2;
	protected string m_DHCRZip2;
	protected string m_DHCRLow3;
	protected string m_DHCRHigh;
	protected string m_DHCRStreetName;
	protected string m_DHCRStreetSuffix;
	protected string m_DHCRCity;
	protected string m_DHCRZip;
	protected string m_DHCRLow2;
	protected string m_MasterAccount;
	protected string m_AuthRep;
	protected string m_FaxNumber;
	protected string m_AuthCode;
	protected string m_email;
	protected string m_DHCRLow;
	protected string m_SectionNumber;
	protected string m_MultipleDwellingRegistrationNumber;
	protected string m_RSA;
	protected string m_SeniorCitizenNumber;
	protected string m_SuperintendantName;
	protected string m_SuperintendantPhone;
    protected bool m_inActive = false;
	protected string m_Email2;

    protected bool m_IsChanged = false;
		public enum ContactType : int
		{
			IsOwner,
			IsAgent,
			IsCoopOwner
		}

		public Building() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
            m_clientId = GlobalShared.user.Account.clientId;
		}

	public Building(DateTime NonEvictionDate,DateTime EvictionDate,DateTime AcceptedForFilingDate,short OwnerNumber,short AgentIs,int TotalShares,int DHCRCounty,int DHCRMunicipality,int DHCRCounty2,int DHCRMunicipality2,int DHCRCounty3,int DHCRMunicipality3,bool RentChangeMCI,bool RentChangeHardship,bool CoopCondo,decimal RealEstateTaxes,decimal MortgageInterest,int ID,long clientId,string BuildingNumber,string Address1,string Address2,string AgentName,string BlockNumber,string LotNumber,string DHCRHigh3,string DHCRStreetName3,string DHCRStreetSuffix3,string DHCRCity3,string DHCRZip3,string DHCRHigh2,string DHCRStreetName2,string DHCRStreetSuffix2,string DHCRCity2,string DHCRZip2,string DHCRLow3,string DHCRHigh,string DHCRStreetName,string DHCRStreetSuffix,string DHCRCity,string DHCRZip,string DHCRLow2,string MasterAccount,string AuthRep,string FaxNumber,string AuthCode,string email,string DHCRLow,string SectionNumber,string MultipleDwellingRegistrationNumber,string RSA,string SeniorCitizenNumber,string SuperintendantName,string SuperintendantPhone) : 
			base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
		m_NonEvictionDate = NonEvictionDate;
		m_EvictionDate = EvictionDate;
		m_AcceptedForFilingDate = AcceptedForFilingDate;
		m_OwnerNumber = OwnerNumber;
		m_AgentIs = AgentIs;
		m_TotalShares = TotalShares;
		m_DHCRCounty = DHCRCounty;
		m_DHCRMunicipality = DHCRMunicipality;
		m_DHCRCounty2 = DHCRCounty2;
		m_DHCRMunicipality2 = DHCRMunicipality2;
		m_DHCRCounty3 = DHCRCounty3;
		m_DHCRMunicipality3 = DHCRMunicipality3;
		m_RentChangeMCI = RentChangeMCI;
		m_RentChangeHardship = RentChangeHardship;
		m_CoopCondo = CoopCondo;
		m_RealEstateTaxes = RealEstateTaxes;
		m_MortgageInterest = MortgageInterest;
		m_ID = ID;
		m_clientId = clientId;
		m_BuildingNumber = BuildingNumber;
		m_Address1 = Address1;
		m_Address2 = Address2;
		m_AgentName = AgentName;
		m_BlockNumber = BlockNumber;
		m_LotNumber = LotNumber;
		m_DHCRHigh3 = DHCRHigh3;
		m_DHCRStreetName3 = DHCRStreetName3;
		m_DHCRStreetSuffix3 = DHCRStreetSuffix3;
		m_DHCRCity3 = DHCRCity3;
		m_DHCRZip3 = DHCRZip3;
		m_DHCRHigh2 = DHCRHigh2;
		m_DHCRStreetName2 = DHCRStreetName2;
		m_DHCRStreetSuffix2 = DHCRStreetSuffix2;
		m_DHCRCity2 = DHCRCity2;
		m_DHCRZip2 = DHCRZip2;
		m_DHCRLow3 = DHCRLow3;
		m_DHCRHigh = DHCRHigh;
		m_DHCRStreetName = DHCRStreetName;
		m_DHCRStreetSuffix = DHCRStreetSuffix;
		m_DHCRCity = DHCRCity;
		m_DHCRZip = DHCRZip;
		m_DHCRLow2 = DHCRLow2;
		m_MasterAccount = MasterAccount;
		m_AuthRep = AuthRep;
		m_FaxNumber = FaxNumber;
		m_AuthCode = AuthCode;
		m_email = email;
		m_DHCRLow = DHCRLow;
		m_SectionNumber = SectionNumber;
		m_MultipleDwellingRegistrationNumber = MultipleDwellingRegistrationNumber;
		m_RSA = RSA;
		m_SeniorCitizenNumber = SeniorCitizenNumber;
		m_SuperintendantName = SuperintendantName;
		m_SuperintendantPhone = SuperintendantPhone;
	}

		protected Building fill(DataRow dr)
		{

			try {
				m_NonEvictionDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_NON_EVICTION_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_NON_EVICTION_DATE]);
				m_EvictionDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_EVICTION_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_EVICTION_DATE]);
				m_AcceptedForFilingDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_ACCEPTED_FOR_FILING_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_ACCEPTED_FOR_FILING_DATE]);
				m_OwnerNumber = (short) dr[SQL_COL_OWNER_NUMBER];
				m_AgentIs = (short) dr[SQL_COL_AGENT_IS];
				m_TotalShares = (int) dr[SQL_COL_TOTAL_SHARES];
				m_DHCRCounty = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_COUNTY]) ? 0 : dr[SQL_COL_D_H_C_R_COUNTY]);
				m_DHCRMunicipality = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_MUNICIPALITY]) ? 0 : dr[SQL_COL_D_H_C_R_MUNICIPALITY]);
				m_DHCRCounty2 = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_COUNTY2]) ? 0 : dr[SQL_COL_D_H_C_R_COUNTY2]);
				m_DHCRMunicipality2 = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_MUNICIPALITY2]) ? 0 : dr[SQL_COL_D_H_C_R_MUNICIPALITY2]);
				m_DHCRCounty3 = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_COUNTY3]) ? 0 : dr[SQL_COL_D_H_C_R_COUNTY3]);
				m_DHCRMunicipality3 = (int) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_MUNICIPALITY3]) ? 0 :dr[SQL_COL_D_H_C_R_MUNICIPALITY3]);
				m_RentChangeMCI = (bool) dr[SQL_COL_RENT_CHANGE_M_C_I];
				m_RentChangeHardship = (bool) dr[SQL_COL_RENT_CHANGE_HARDSHIP];
				m_CoopCondo = (bool) (Convert.IsDBNull(dr[SQL_COL_COOP_CONDO]) ? false : dr[SQL_COL_COOP_CONDO]);
				m_RealEstateTaxes = (decimal) dr[SQL_COL_REAL_ESTATE_TAXES];
				m_MortgageInterest = (decimal) dr[SQL_COL_MORTGAGE_INTEREST];
				m_ID = (int) dr[SQL_COL_I_D];
				m_clientId = (int) dr[SQL_COL_CLIENT_ID];
				m_BuildingNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING_NUMBER]) ? "" : dr[SQL_COL_BUILDING_NUMBER]);
				m_Address1 = (string) (Convert.IsDBNull(dr[SQL_COL_ADDRESS_1]) ? "" : dr[SQL_COL_ADDRESS_1]);
				m_Address2 = (string) (Convert.IsDBNull(dr[SQL_COL_ADDRESS_2]) ? "" : dr[SQL_COL_ADDRESS_2]);
				m_AgentName = (string) (Convert.IsDBNull(dr[SQL_COL_AGENT_NAME]) ? "" : dr[SQL_COL_AGENT_NAME]);
				m_BlockNumber = (string) (Convert.IsDBNull(dr[SQL_COL_BLOCK_NUMBER]) ? "" : dr[SQL_COL_BLOCK_NUMBER]);
				m_LotNumber = (string) (Convert.IsDBNull(dr[SQL_COL_LOT_NUMBER]) ? "" : dr[SQL_COL_LOT_NUMBER]);
				m_DHCRHigh3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_HIGH3]) ? "" : dr[SQL_COL_D_H_C_R_HIGH3]);
				m_DHCRStreetName3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_NAME3]) ? "" : dr[SQL_COL_D_H_C_R_STREET_NAME3]);
				m_DHCRStreetSuffix3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_SUFFIX3]) ? "" : dr[SQL_COL_D_H_C_R_STREET_SUFFIX3]);
				m_DHCRCity3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_CITY3]) ? "" : dr[SQL_COL_D_H_C_R_CITY3]);
				m_DHCRZip3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_ZIP3]) ? "" : dr[SQL_COL_D_H_C_R_ZIP3]);
				m_DHCRHigh2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_HIGH2]) ? "" : dr[SQL_COL_D_H_C_R_HIGH2]);
				m_DHCRStreetName2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_NAME2]) ? "" : dr[SQL_COL_D_H_C_R_STREET_NAME2]);
				m_DHCRStreetSuffix2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_SUFFIX2]) ? "" : dr[SQL_COL_D_H_C_R_STREET_SUFFIX2]);
				m_DHCRCity2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_CITY2]) ? "" : dr[SQL_COL_D_H_C_R_CITY2]);
				m_DHCRZip2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_ZIP2]) ? "" : dr[SQL_COL_D_H_C_R_ZIP2]);
				m_DHCRLow3 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_LOW3]) ? "" : dr[SQL_COL_D_H_C_R_LOW3]);
				m_DHCRHigh = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_HIGH]) ? "" : dr[SQL_COL_D_H_C_R_HIGH]);
				m_DHCRStreetName = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_NAME]) ? "" : dr[SQL_COL_D_H_C_R_STREET_NAME]);
				m_DHCRStreetSuffix = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_STREET_SUFFIX]) ? "" : dr[SQL_COL_D_H_C_R_STREET_SUFFIX]);
				m_DHCRCity = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_CITY]) ? "" : dr[SQL_COL_D_H_C_R_CITY]);
				m_DHCRZip = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_ZIP]) ? "" : dr[SQL_COL_D_H_C_R_ZIP]);
				m_DHCRLow2 = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_LOW2]) ? "" : dr[SQL_COL_D_H_C_R_LOW2]);
				m_MasterAccount = (string) (Convert.IsDBNull(dr[SQL_COL_MASTER_ACCOUNT]) ? "" : dr[SQL_COL_MASTER_ACCOUNT]);
				m_AuthRep = (string) (Convert.IsDBNull(dr[SQL_COL_AUTH_REP]) ? "" : dr[SQL_COL_AUTH_REP]);
				m_FaxNumber = (string) (Convert.IsDBNull(dr[SQL_COL_FAX_NUMBER]) ? "" : dr[SQL_COL_FAX_NUMBER]);
				m_AuthCode = (string) (Convert.IsDBNull(dr[SQL_COL_AUTH_CODE]) ? "" : dr[SQL_COL_AUTH_CODE]);
				m_email = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL]) ? "" : dr[SQL_COL_EMAIL]);
				m_DHCRLow = (string) (Convert.IsDBNull(dr[SQL_COL_D_H_C_R_LOW]) ? "" : dr[SQL_COL_D_H_C_R_LOW]);
				m_SectionNumber = (string) (Convert.IsDBNull(dr[SQL_COL_SECTION_NUMBER]) ? "" : dr[SQL_COL_SECTION_NUMBER]);
				m_MultipleDwellingRegistrationNumber = (string) (Convert.IsDBNull(dr[SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER]) ? "" : dr[SQL_COL_MULTIPLE_DWELLING_REGISTRATION_NUMBER]);
				m_RSA = (string) (Convert.IsDBNull(dr[SQL_COL_R_S_A]) ? "" : dr[SQL_COL_R_S_A]);
				m_SeniorCitizenNumber = (string) (Convert.IsDBNull(dr[SQL_COL_SENIOR_CITIZEN_NUMBER]) ? "" : dr[SQL_COL_SENIOR_CITIZEN_NUMBER]);
				m_SuperintendantName = (string) (Convert.IsDBNull(dr[SQL_COL_SUPERINTENDANT_NAME]) ? "" : dr[SQL_COL_SUPERINTENDANT_NAME]);
				m_SuperintendantPhone = (string) (Convert.IsDBNull(dr[SQL_COL_SUPERINTENDANT_PHONE]) ? "" : dr[SQL_COL_SUPERINTENDANT_PHONE]);
				m_inActive = (bool)dr[SQL_COLUMN_INACTIVE];
				m_Email2 = (string)(Convert.IsDBNull(dr[SQL_COL_EMAIL2]) ? "" : dr[SQL_COL_EMAIL2]);
            }
            catch (System.InvalidCastException ex)
            {
            }
			catch (SqlException ex) {
			}
			return this;

		}

		public static Building[] load(long clientId = 1)
		{

			ArrayList list = new ArrayList();

            string[] opt = { clientId.ToString() };
			DataSet ds = new DataSet();
			Building obj = new Building();

			ds = obj.load(SQL_LOAD_ALL,opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Building();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Building[])list.ToArray(typeof(Building));
			} catch (SqlException ex) {
				return null;
			}

		}

		public static Building loadByBuildingNumber(string bldNumber, long clientId)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Building obj = new Building();
			string[] opt = { bldNumber, clientId.ToString() };

			ds = obj.load(SQL_LOAD_BY_BUILDING_NUMBER, opt);
			if ((ds != null) && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				DataRow dr = ds.Tables[0].Rows[0];
				if ((dr != null)) {
					return obj.fill(dr);
				} else {
					return null;
				}
			} else {
				return null;
			}

		}

		public static Building[] loadByOwnerNumber(int ownNumber, long clientId)
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Building obj = new Building();
			string[] opt = { ownNumber.ToString(), clientId.ToString() };

			ds = obj.load(SQL_LOAD_BY_OWNER_NUMBER, opt);
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Building();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Building[])list.ToArray(typeof(Building));
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
		if (m_ID > 0 ) {
			string[] values = {m_NonEvictionDate.ToString(), m_EvictionDate.ToString(), m_AcceptedForFilingDate.ToString(), m_OwnerNumber.ToString(), m_AgentIs.ToString(), m_TotalShares.ToString(), m_DHCRCounty.ToString(), m_DHCRMunicipality.ToString(), m_DHCRCounty2.ToString(),
					m_DHCRMunicipality2.ToString(), m_DHCRCounty3.ToString(), m_DHCRMunicipality3.ToString(), m_RentChangeMCI.ToString(), m_RentChangeHardship.ToString(), m_CoopCondo.ToString(), m_RealEstateTaxes.ToString(), m_MortgageInterest.ToString(), m_clientId.ToString(), 
					m_BuildingNumber, m_Address1, m_Address2, m_AgentName, m_BlockNumber, m_LotNumber, m_DHCRHigh3, m_DHCRStreetName3, m_DHCRStreetSuffix3, m_DHCRCity3, m_DHCRZip3, m_DHCRHigh2, m_DHCRStreetName2, m_DHCRStreetSuffix2, m_DHCRCity2, m_DHCRZip2, m_DHCRLow3, 
					m_DHCRHigh, m_DHCRStreetName, m_DHCRStreetSuffix, m_DHCRCity, m_DHCRZip, m_DHCRLow2, m_MasterAccount, m_AuthRep, m_FaxNumber, m_AuthCode, m_email, m_DHCRLow, m_SectionNumber, m_MultipleDwellingRegistrationNumber, m_RSA, m_SeniorCitizenNumber, 
					m_SuperintendantName, m_SuperintendantPhone, m_inActive.ToString(), m_Email2, m_ID.ToString()};
			if (this.updateRecord(m_ID.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_NonEvictionDate.ToString(), m_EvictionDate.ToString(), m_AcceptedForFilingDate.ToString(), m_OwnerNumber.ToString(), m_AgentIs.ToString(), m_TotalShares.ToString(), m_DHCRCounty.ToString(), m_DHCRMunicipality.ToString(), m_DHCRCounty2.ToString(),
					m_DHCRMunicipality2.ToString(), m_DHCRCounty3.ToString(), m_DHCRMunicipality3.ToString(), m_RentChangeMCI.ToString(), m_RentChangeHardship.ToString(), m_CoopCondo.ToString(), m_RealEstateTaxes.ToString(), m_MortgageInterest.ToString(), m_clientId.ToString(),
					m_BuildingNumber, m_Address1, m_Address2, m_AgentName, m_BlockNumber, m_LotNumber, m_DHCRHigh3, m_DHCRStreetName3, m_DHCRStreetSuffix3, m_DHCRCity3, m_DHCRZip3, m_DHCRHigh2, m_DHCRStreetName2, m_DHCRStreetSuffix2, m_DHCRCity2, m_DHCRZip2, m_DHCRLow3,
					m_DHCRHigh, m_DHCRStreetName, m_DHCRStreetSuffix, m_DHCRCity, m_DHCRZip, m_DHCRLow2, m_MasterAccount, m_AuthRep, m_FaxNumber, m_AuthCode, m_email, m_DHCRLow, m_SectionNumber, m_MultipleDwellingRegistrationNumber, m_RSA, m_SeniorCitizenNumber,
					m_SuperintendantName, m_SuperintendantPhone, m_inActive.ToString(), m_Email2};
			m_ID = this.insertRecord(values);
			if (m_ID >= 0) {
				return true;
			}
		}
		return false;

	}


	    public static void alterTable()
	    {
	        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
	        cmd.CommandType = CommandType.Text;
            GlobalShared.CN.Open();
	        cmd.Connection = GlobalShared.CN;
	        try
	        {
	            cmd.CommandText = "ALTER TABLE pm_building ADD [Building Number] varchar(3)";
	            cmd.ExecuteNonQuery();
	        } catch (System.Data.SqlClient.SqlException ex)
	        {
	        } catch (System.Exception ex)
	        {
	    
	        }

	        try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Owner Number] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Address 1] varchar(30)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Address 2] varchar(30)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Agent Name] varchar(12)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Block Number] varchar(4)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Lot Number] varchar(3)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Section Number] varchar(3)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Multiple Dwelling Registration Number] varchar(6)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [RSA] varchar(6)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Senior Citizen Number] varchar(6)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Superintendant Name] varchar(18)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Superintendant Phone] varchar(12)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Real Estate Taxes] real";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Mortgage Interest] real";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [Total Shares] bigint";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [AuthCode] varchar(10)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [MasterAccount] varchar(10)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [AuthRep] varchar(32)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [FaxNumber] varchar(12)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [email] varchar(50)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [RentChangeMCI] bit";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [RentChangeHardship] bit";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [AgentIs] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRLow] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRHigh] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetName] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetSuffix] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCity] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRZip] varchar(9)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCounty] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRMunicipality] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRLow2] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRHigh2] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetName2] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetSuffix2] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCity2] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRZip2] varchar(9)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCounty2] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRMunicipality2] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRLow3] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRHigh3] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetName3] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRStreetSuffix3] varchar(8)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCity3] varchar(25)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRZip3] varchar(9)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRCounty3] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [DHCRMunicipality3] int";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [CoopCondo] bit";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [NonEvictionDate] datetime2(0)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [EvictionDate] datetime2(0)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
		    try
		    {
		        cmd.CommandText = "ALTER TABLE pm_building ADD [AcceptedForFilingDate] datetime2(0)";
			    cmd.ExecuteNonQuery();
		    } catch (System.Data.SqlClient.SqlException ex) {}
            try
            {
                GlobalShared.CN.Close();
            } catch(System.Exception ex)
            {}
        }

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_IsChanged = true;
		}

        // Property Routines

        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            get { return m_IsChanged; }
			set { m_IsChanged = value; }
		}

        public string FullName
        {
            get { return m_BuildingNumber + " : " + m_Address1; }
        }
	public DateTime NonEvictionDate {
		set{ m_NonEvictionDate = value;
		OnPropertyChanged("NonEvictionDate"); }
		get { return m_NonEvictionDate; }
	}

	public DateTime EvictionDate {
		set{ m_EvictionDate = value;
		OnPropertyChanged("EvictionDate"); }
		get { return m_EvictionDate; }
	}

	public DateTime AcceptedForFilingDate {
		set{ m_AcceptedForFilingDate = value;
		OnPropertyChanged("AcceptedForFilingDate"); }
		get { return m_AcceptedForFilingDate; }
	}

	public short OwnerNumber {
		set{ m_OwnerNumber = value;
		OnPropertyChanged("OwnerNumber"); }
		get { return m_OwnerNumber; }
	}

	public short AgentIs {
		set{ m_AgentIs = value;
		OnPropertyChanged("AgentIs"); }
		get { return m_AgentIs; }
	}

	public int TotalShares {
		set{ m_TotalShares = value;
		OnPropertyChanged("TotalShares"); }
		get { return m_TotalShares; }
	}

	public int DHCRCounty {
		set{ m_DHCRCounty = value;
		OnPropertyChanged("DHCRCounty"); }
		get { return m_DHCRCounty; }
	}

	public int DHCRMunicipality {
		set{ m_DHCRMunicipality = value;
		OnPropertyChanged("DHCRMunicipality"); }
		get { return m_DHCRMunicipality; }
	}

	public int DHCRCounty2 {
		set{ m_DHCRCounty2 = value;
		OnPropertyChanged("DHCRCounty2"); }
		get { return m_DHCRCounty2; }
	}

	public int DHCRMunicipality2 {
		set{ m_DHCRMunicipality2 = value;
		OnPropertyChanged("DHCRMunicipality2"); }
		get { return m_DHCRMunicipality2; }
	}

	public int DHCRCounty3 {
		set{ m_DHCRCounty3 = value;
		OnPropertyChanged("DHCRCounty3"); }
		get { return m_DHCRCounty3; }
	}

	public int DHCRMunicipality3 {
		set{ m_DHCRMunicipality3 = value;
		OnPropertyChanged("DHCRMunicipality3"); }
		get { return m_DHCRMunicipality3; }
	}

	public bool RentChangeMCI {
		set{ m_RentChangeMCI = value;
		OnPropertyChanged("RentChangeMCI"); }
		get { return m_RentChangeMCI; }
	}

	public bool RentChangeHardship {
		set{ m_RentChangeHardship = value;
		OnPropertyChanged("RentChangeHardship"); }
		get { return m_RentChangeHardship; }
	}

	public bool CoopCondo {
		set{ m_CoopCondo = value;
		OnPropertyChanged("CoopCondo"); }
		get { return m_CoopCondo; }
	}

	public decimal RealEstateTaxes {
		set{ m_RealEstateTaxes = value;
		OnPropertyChanged("RealEstateTaxes"); }
		get { return m_RealEstateTaxes; }
	}

	public decimal MortgageInterest {
		set{ m_MortgageInterest = value;
		OnPropertyChanged("MortgageInterest"); }
		get { return m_MortgageInterest; }
	}

	public int ID {
		get { return m_ID; }
	}

	public long clientId {
		set{ m_clientId = value;
		OnPropertyChanged("clientId"); }
		get { return m_clientId; }
	}

	public string BuildingNumber {
		set{ m_BuildingNumber = value;
		OnPropertyChanged("BuildingNumber"); }
		get { return m_BuildingNumber; }
	}

	public string Address1 {
		set{ m_Address1 = value;
		OnPropertyChanged("Address1"); }
		get { return m_Address1; }
	}

	public string Address2 {
		set{ m_Address2 = value;
		OnPropertyChanged("Address2"); }
		get { return m_Address2; }
	}

	public string AgentName {
		set{ m_AgentName = value;
		OnPropertyChanged("AgentName"); }
		get { return m_AgentName; }
	}

	public string BlockNumber {
		set{ m_BlockNumber = value;
		OnPropertyChanged("BlockNumber"); }
		get { return m_BlockNumber; }
	}

	public string LotNumber {
		set{ m_LotNumber = value;
		OnPropertyChanged("LotNumber"); }
		get { return m_LotNumber; }
	}

	public string DHCRHigh3 {
		set{ m_DHCRHigh3 = value;
		OnPropertyChanged("DHCRHigh3"); }
		get { return m_DHCRHigh3; }
	}

	public string DHCRStreetName3 {
		set{ m_DHCRStreetName3 = value;
		OnPropertyChanged("DHCRStreetName3"); }
		get { return m_DHCRStreetName3; }
	}

	public string DHCRStreetSuffix3 {
		set{ m_DHCRStreetSuffix3 = value;
		OnPropertyChanged("DHCRStreetSuffix3"); }
		get { return m_DHCRStreetSuffix3; }
	}

	public string DHCRCity3 {
		set{ m_DHCRCity3 = value;
		OnPropertyChanged("DHCRCity3"); }
		get { return m_DHCRCity3; }
	}

	public string DHCRZip3 {
		set{ m_DHCRZip3 = value;
		OnPropertyChanged("DHCRZip3"); }
		get { return m_DHCRZip3; }
	}

	public string DHCRHigh2 {
		set{ m_DHCRHigh2 = value;
		OnPropertyChanged("DHCRHigh2"); }
		get { return m_DHCRHigh2; }
	}

	public string DHCRStreetName2 {
		set{ m_DHCRStreetName2 = value;
		OnPropertyChanged("DHCRStreetName2"); }
		get { return m_DHCRStreetName2; }
	}

	public string DHCRStreetSuffix2 {
		set{ m_DHCRStreetSuffix2 = value;
		OnPropertyChanged("DHCRStreetSuffix2"); }
		get { return m_DHCRStreetSuffix2; }
	}

	public string DHCRCity2 {
		set{ m_DHCRCity2 = value;
		OnPropertyChanged("DHCRCity2"); }
		get { return m_DHCRCity2; }
	}

	public string DHCRZip2 {
		set{ m_DHCRZip2 = value;
		OnPropertyChanged("DHCRZip2"); }
		get { return m_DHCRZip2; }
	}

	public string DHCRLow3 {
		set{ m_DHCRLow3 = value;
		OnPropertyChanged("DHCRLow3"); }
		get { return m_DHCRLow3; }
	}

	public string DHCRHigh {
		set{ m_DHCRHigh = value;
		OnPropertyChanged("DHCRHigh"); }
		get { return m_DHCRHigh; }
	}

	public string DHCRStreetName {
		set{ m_DHCRStreetName = value;
		OnPropertyChanged("DHCRStreetName"); }
		get { return m_DHCRStreetName; }
	}

	public string DHCRStreetSuffix {
		set{ m_DHCRStreetSuffix = value;
		OnPropertyChanged("DHCRStreetSuffix"); }
		get { return m_DHCRStreetSuffix; }
	}

	public string DHCRCity {
		set{ m_DHCRCity = value;
		OnPropertyChanged("DHCRCity"); }
		get { return m_DHCRCity; }
	}

	public string DHCRZip {
		set{ m_DHCRZip = value;
		OnPropertyChanged("DHCRZip"); }
		get { return m_DHCRZip; }
	}

	public string DHCRLow2 {
		set{ m_DHCRLow2 = value;
		OnPropertyChanged("DHCRLow2"); }
		get { return m_DHCRLow2; }
	}

	public string MasterAccount {
		set{ m_MasterAccount = value;
		OnPropertyChanged("MasterAccount"); }
		get { return m_MasterAccount; }
	}

	public string AuthRep {
		set{ m_AuthRep = value;
		OnPropertyChanged("AuthRep"); }
		get { return m_AuthRep; }
	}

	public string FaxNumber {
		set{ m_FaxNumber = value;
		OnPropertyChanged("FaxNumber"); }
		get { return m_FaxNumber; }
	}

	public string AuthCode {
		set{ m_AuthCode = value;
		OnPropertyChanged("AuthCode"); }
		get { return m_AuthCode; }
	}

	public string email {
		set{ m_email = value;
		OnPropertyChanged("email"); }
		get { return m_email; }
	}

	public string DHCRLow {
		set{ m_DHCRLow = value;
		OnPropertyChanged("DHCRLow"); }
		get { return m_DHCRLow; }
	}

	public string SectionNumber {
		set{ m_SectionNumber = value;
		OnPropertyChanged("SectionNumber"); }
		get { return m_SectionNumber; }
	}

	public string MultipleDwellingRegistrationNumber {
		set{ m_MultipleDwellingRegistrationNumber = value;
		OnPropertyChanged("MultipleDwellingRegistrationNumber"); }
		get { return m_MultipleDwellingRegistrationNumber; }
	}

	public string RSA {
		set{ m_RSA = value;
		OnPropertyChanged("RSA"); }
		get { return m_RSA; }
	}

	public string SeniorCitizenNumber {
		set{ m_SeniorCitizenNumber = value;
		OnPropertyChanged("SeniorCitizenNumber"); }
		get { return m_SeniorCitizenNumber; }
	}

	public string SuperintendantName {
		set{ m_SuperintendantName = value;
		OnPropertyChanged("SuperintendantName"); }
		get { return m_SuperintendantName; }
	}

	public string SuperintendantPhone {
		set{ m_SuperintendantPhone = value;
		OnPropertyChanged("SuperintendantPhone"); }
		get { return m_SuperintendantPhone; }
	}

        public bool InActive
        {
            get { return m_inActive; }
            set
            {
                m_inActive = value;
                OnPropertyChanged("InActive");
            }
        }

		public string Email2
		{
			get { return m_Email2; }
			set 
			{
				m_Email2 = value;
				OnPropertyChanged("Email2");
			}
		}

	}
}
