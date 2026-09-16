using Microsoft.VisualBasic;
using System.Data;
using System.Data.OleDb;
namespace WinTRM
{

	// $ID: Exp $
	public class Tenants : clsDB
	{


		public const string TABLE = "Tenants";
		public const string SQL_COL_ID = "Sequence Number";
		public const string SQL_COL_BUILDING_NUMBER = "Building Number";
		public const string SQL_COL_TENANT_NUMBER = "Tenant Number";
		public const string SQL_COL_TENANT_NAME = "Tenant Name";
		public const string SQL_COL_APARTMENT = "Apartment";
		public const string SQL_COL_EMAIL1 = "Email1";
		public const string SQL_COL_EMAIL2 = "Email2";
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
		public const string SQL_COL_COMMERICAL_PROFESSIONAL = "CommercialProfessional";
		public const string SQL_COL_NOT_PRIME_RESIDENCE = "NotPrimeResidence";
		public const string SQL_COL_TEMP_EXEMPT_OTHER = "TempExemptOther";
		public const string SQL_COL_EXEMPT_DATE = "ExemptDate";
		public const string SQL_COL_EXEMPT_HIGH_RENT_VACANCY = "ExemptHighRentVacancy";
		public const string SQL_COL_EXEMPT_LAST_LEGAL_PAID = "ExemptLastLegalPaid";
		public const string SQL_COL_EXEMPT_HIGH_RENT_VACANY_PERIOD = "ExemptHighRentVacanyPeriod";
		public const string SQL_COL_EXEMPT_HIGH_RENT_INCOME = "ExemptHighRentIncome";
		public const string SQL_COL_EXEMPT_COMMERICIAL_PROFESSIONAL_CO = "ExemptCommercialProfessionalCo";
		public const string SQL_COL_EXEMPT_SUBSTANTIAL_REHAB = "ExemptSubstantialRehab";
		public const string SQL_COL_EXEMPT_COOP_CONDO_OWNER = "ExemptCoopCondoOwner";
		public const string SQL_COL_EXEMPT_SEC_11243 = "ExemptSec11243";
		public const string SQL_COL_EXEMPT_OTHER = "ExemptOther";
		public const string SQL_COL_EXEMPT_OTHER_DESC = "ExemptOtherDesc";
		public const string SQL_COL_EXEMPT_EXP_SEC_608 = "ExemptExpSec608";
		public const string SQL_COL_EXEMPT_EXP_421A = "ExemptExp421a";
		public const string SQL_COL_EXEMPT_421A_MARKET_RATE = "Exempt421aMarketRate";
		public const string SQL_COL_EXEMPT_421A_INCOME = "Exempt421aIncome";

		public const string SQL_COL_EXEMPT_421A_MEDIAN_INCOME = "Exempt421aMedianIncome";

		static string SQL_LOAD = "SELECT * " + " FROM " + TABLE + " WHERE [" + SQL_COL_ID + "]=?";

		static string SQL_LOAD_ALL = "SELECT * " + " FROM " + TABLE + " ORDER BY [" + SQL_COL_BUILDING_NUMBER + "], [" + SQL_COL_TENANT_NUMBER + "];";

		static string SQL_LOAD_ALL_NAME = "SELECT * " + " FROM " + TABLE + " ORDER BY [" + SQL_COL_TENANT_NAME + "];";

		static string SQL_UPDATE = "place holder";

		static string SQL_INSERT = "place holder";

		static string SQL_LOAD_BY_BUILDING = "SELECT *" + " FROM " + TABLE + " WHERE [" + SQL_COL_BUILDING_NUMBER + "]='?'" + " ORDER BY [" + SQL_COL_TENANT_NAME + "];";
		static string SQL_LOAD_BY_TENANT_NUMBER = "SELECT *" + " FROM " + TABLE + " WHERE [" + SQL_COL_BUILDING_NUMBER + "]='?' AND [" + SQL_COL_TENANT_NUMBER + "]='?';";
		private Int32 sequence;
		private string buildingNumber;
		private string tenantNumber;
		private string tenantName;
		private string apartment;
		private string email1;

		private string email2;

		private DataRow dr;
		public Tenants() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
		}

		protected Tenants fill(DataRow dr)
		{
			//Me.dr = dr
			try {
				sequence = Convert.ToInt64(dr(SQL_COL_ID));
				buildingNumber = dr(SQL_COL_BUILDING_NUMBER);
				tenantNumber = dr(SQL_COL_TENANT_NUMBER);
				tenantName = dr(SQL_COL_TENANT_NAME);
				apartment = dr(SQL_COL_APARTMENT);
				email1 = (Information.IsDBNull(dr(SQL_COL_EMAIL1)) ? "" : dr(SQL_COL_EMAIL1));
				email2 = (Information.IsDBNull(dr(SQL_COL_EMAIL2)) ? "" : dr(SQL_COL_EMAIL2));
			} catch (System.Exception ex) {
			}
			return this;

		}

		protected Tenants fill(OleDb.OleDbDataReader dr)
		{

			try {
				sequence = dr(SQL_COL_ID);
				buildingNumber = dr(SQL_COL_BUILDING_NUMBER);
				tenantNumber = dr(SQL_COL_TENANT_NUMBER);
				tenantName = dr(SQL_COL_TENANT_NAME);
				apartment = dr(SQL_COL_APARTMENT);
				email1 = ((dr(SQL_COL_EMAIL1) == null) ? "" : dr(SQL_COL_EMAIL1));
				email2 = ((dr(SQL_COL_EMAIL2) == null) ? "" : dr(SQL_COL_EMAIL2));
			} catch (System.Exception ex) {
				string s = ex.Message;
			}
			return this;

		}

		public static Tenants[] load(bool lst = false, bool byName = false)
		{

			ArrayList list = new ArrayList();
			DataSet ds = new DataSet();
			Tenants ten = new Tenants();

			if (lst) {
				try {
					OleDb.OleDbCommand cmd = new OleDb.OleDbCommand {
						CommandType = CommandType.Text,
						CommandText = (byName ? SQL_LOAD_ALL_NAME : SQL_LOAD_ALL),
						Connection = GlobalShared.pmCN
					};
					GlobalShared.pmCN.Open();
					OleDb.OleDbDataReader dr = cmd.ExecuteReader;
					if (!dr.HasRows) {
						return null;
					}
					while (dr.Read) {
						ten = new Tenants();
						ten.fill(dr);
						list.Add(ten);
						ten = null;
					}
				} catch (System.Exception ex) {
					return null;
				} finally {
					GlobalShared.pmCN.Close();
				}
			} else {
				//pmCN.Open()
				ds = ten.loadAll();
				ten = null;
				DataRow drl = default(DataRow);
				try {
					foreach ( drl in ds.Tables(0).Rows) {
						ten = new Tenants();
						ten.fill(drl);
						list.Add(ten);
						ten = null;
					}
				} catch (System.Exception ex) {
					// Dim s As String = ex.Message
				}
			}
			try {
				return (Tenants[])list.ToArray(typeof(Tenants));
			} catch {
				return null;
			} finally {
				GlobalShared.pmCN.Close();
			}

		}
		public static Tenants load(long id)
		{

			DataSet ds = new DataSet();
			Tenants ten = new Tenants();
			DataRow drl = null;
			ds = ten.loadByPrimaryKey(id);
			if ((ds == null)) {
				return null;
			}
			if (ds.Tables(0).Rows.Count == 0) {
				return null;
			}
			drl = ds.Tables(0).Rows(0);
			return ten.fill(drl);

		}
		public static Tenants[] loadByBuilding(string bldNumber, bool lst = false)
		{

			ArrayList list = new ArrayList();
			DataSet ds = new DataSet();
			Tenants ten = new Tenants();
			string[] s = { bldNumber };

			if (lst) {
				try {
					string xsql = prepareStatement(SQL_LOAD_BY_BUILDING, s);
					OleDb.OleDbCommand cmd = new OleDb.OleDbCommand {
						CommandType = CommandType.Text,
						CommandText = xsql,
						Connection = GlobalShared.pmCN
					};
					GlobalShared.pmCN.Open();
					OleDb.OleDbDataReader dr = cmd.ExecuteReader;
					if (!dr.HasRows) {
						return null;
					}
					while (dr.Read) {
						ten = new Tenants();
						ten.fill(dr);
						list.Add(ten);
						ten = null;
					}
				} catch (System.Exception ex) {
					return null;
				} finally {
					GlobalShared.pmCN.Close();
				}
			} else {
				ds = ten.load(SQL_LOAD_BY_BUILDING, s);
				ten = null;
				DataRow drl = default(DataRow);
				try {
					foreach ( drl in ds.Tables(0).Rows) {
						ten = new Tenants();
						ten.fill(drl);
						list.Add(ten);
						ten = null;
					}
				} catch (OleDbException ex) {
					GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				} catch (System.Exception ex) {
					GlobalShared.Log.Log(LogClass.logType.Warning, ex.Message);
				}

			}
			try {
				return (Tenants[])list.ToArray(typeof(Tenants));
			} catch {
				return null;
			}

		}
		public string getBuildingNumber()
		{
			return buildingNumber;
		}
		public string getTenantNumber()
		{
			return tenantNumber;
		}
		public string getTenantName()
		{
			return tenantName;
		}
		public long getSequenceNumber()
		{
			return sequence;
		}
		public string getApartment()
		{
			return apartment;
		}
		public string getEmail1()
		{
			return email1;
		}
		public string getEmail2()
		{
			return email2;
		}
	}
}
