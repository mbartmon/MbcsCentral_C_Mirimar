using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using Microsoft.VisualBasic;
namespace MbcsCentral.WinTrm
{

    public class RecipientMaster : clsDB, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        public static string TABLE = "RecipientMaster";
        public const string SQL_COL_LIST_ID = "listId";
        public const string SQL_COL_ACCOUNT_ID = "accountId";
        public const string SQL_COL_LIST_NAME = "listName";
        public const string SQL_COL_COMPANY_IDS = "companyIds";
        public const string SQL_COL_BUILDING_IDS = "buildingIds";
        public const string SQL_COL_TENANT_TYPES = "tenantTypes";
        public const string SQL_COL_OWNER_FILTER = "ownerFilter";
        public const string SQL_COL_BUILDING_FILTER = "buildingFilter";
        public const string SQL_COL_TYPE_FILTER = "typeFilter";
        public const string SQL_COL_ARREARS_FILTER = "arrearsFilter";

        public const string SQL_COL_ARREARS_AMOUNT = "arrearsAmount";
        // * Filter variables
        // Change to conform with number of filter fields defined in record
        public const int NUM_FILTERS = 4;
        // Add names as filters are added
        public enum FilterTypes : int
        {
            companyIds,
            buildingIds,
            tenantTypes,
            ArrearsAmount
        }
        public static string[] FilterProperties = {
            "companyIds",
            "buildingIds",
            "tenantTypes",
            "ArrearsAmount"

        };

        static string SQL_LOAD = " SELECT " + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_LIST_NAME + "]," + "[" + SQL_COL_COMPANY_IDS + "]," + "[" + SQL_COL_BUILDING_IDS + "]," + "[" + SQL_COL_TENANT_TYPES + "]," + SQL_COL_OWNER_FILTER + "," + SQL_COL_BUILDING_FILTER + "," + SQL_COL_TYPE_FILTER + "," + SQL_COL_ARREARS_FILTER + "," + SQL_COL_ARREARS_AMOUNT + " FROM " + TABLE + " WHERE " + SQL_COL_LIST_ID + " = ?;";

        static string SQL_LOAD_BY_ACCOUNT = " SELECT " +
                                            "[" + SQL_COL_LIST_ID + "]," +
                                            "[" + SQL_COL_ACCOUNT_ID + "]," +
                                            "[" + SQL_COL_LIST_NAME + "]," +
                                            "[" + SQL_COL_COMPANY_IDS + "]," +
                                            "[" + SQL_COL_BUILDING_IDS + "]," +
                                            "[" + SQL_COL_TENANT_TYPES + "]," +
                                            SQL_COL_OWNER_FILTER + "," +
                                            SQL_COL_BUILDING_FILTER + "," +
                                            SQL_COL_TYPE_FILTER + "," +
                                            SQL_COL_ARREARS_FILTER + "," +
                                            SQL_COL_ARREARS_AMOUNT +
                                            " FROM " + TABLE +
                                            " WHERE " + SQL_COL_ACCOUNT_ID + " = ?;";


        static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_LIST_ID + "]," + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_LIST_NAME + "]," + "[" + SQL_COL_COMPANY_IDS + "]," + "[" + SQL_COL_BUILDING_IDS + "]," + "[" + SQL_COL_TENANT_TYPES + "]," + SQL_COL_OWNER_FILTER + "," + SQL_COL_BUILDING_FILTER + "," + SQL_COL_TYPE_FILTER + "," + SQL_COL_ARREARS_FILTER + "," + SQL_COL_ARREARS_AMOUNT + " FROM " + TABLE + " ORDER BY " + SQL_COL_LIST_NAME + ";";

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_LIST_NAME + "]," + "[" + SQL_COL_COMPANY_IDS + "]," + "[" + SQL_COL_BUILDING_IDS + "]," + "[" + SQL_COL_TENANT_TYPES + "]," + SQL_COL_OWNER_FILTER + "," + SQL_COL_BUILDING_FILTER + "," + SQL_COL_TYPE_FILTER + "," + SQL_COL_ARREARS_FILTER + "," + SQL_COL_ARREARS_AMOUNT + ") VALUES (?, '?', '?', '?', '?', ?, ?, ?, ?, ?);";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_ACCOUNT_ID + "]=?," + "[" + SQL_COL_LIST_NAME + "]='?'," + "[" + SQL_COL_COMPANY_IDS + "]='?'," + "[" + SQL_COL_BUILDING_IDS + "]='?'," + "[" + SQL_COL_TENANT_TYPES + "]='?'," + SQL_COL_OWNER_FILTER + "=?," + SQL_COL_BUILDING_FILTER + "=?," + SQL_COL_TYPE_FILTER + "=?," + SQL_COL_ARREARS_FILTER + "=?," + SQL_COL_ARREARS_AMOUNT + "=?" + " WHERE " + SQL_COL_LIST_ID + " = ?;";
        protected int m_listId;
        protected int m_accountId;
        protected string m_listName;
        protected string m_companyIds;
        protected string m_buildingIds;
        protected string m_tenantTypes;
        protected bool m_ownerFilter;
        protected bool m_buildingFilter;
        protected bool m_typeFilter;
        protected bool m_arrearsFilter;
        protected decimal m_arrearsAmount;

        protected bool m_isChanged = false;
        public RecipientMaster() : base(TABLE, SQL_COL_LIST_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
        {
        }

        public RecipientMaster(int listId, int accountId, string listName, string companyIds, string buildingIds, string tenantTypes, bool ownerFilter, bool buildingFilter, bool typeFilter, bool arrearsFilter,
        decimal arrearsAmount) : base(TABLE, SQL_COL_LIST_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
        {
            m_listId = listId;
            m_accountId = accountId;
            m_listName = listName;
            m_companyIds = companyIds;
            m_buildingIds = buildingIds;
            m_tenantTypes = tenantTypes;
            m_ownerFilter = ownerFilter;
            m_buildingFilter = buildingFilter;
            m_typeFilter = typeFilter;
            m_arrearsFilter = arrearsFilter;
            m_arrearsAmount = arrearsAmount;

        }

        protected RecipientMaster fill(DataRow dr)
        {

            try {
                m_listId = (int)dr[SQL_COL_LIST_ID];
                m_accountId = (int)dr[SQL_COL_ACCOUNT_ID];
                m_listName = (string)dr[SQL_COL_LIST_NAME];
                m_companyIds = (string)(Information.IsDBNull(dr[SQL_COL_COMPANY_IDS]) ? "" : dr[SQL_COL_COMPANY_IDS]);
                m_buildingIds = (string)(Information.IsDBNull(dr[SQL_COL_BUILDING_IDS]) ? "" : dr[SQL_COL_BUILDING_IDS]);
                m_tenantTypes = (string)(Information.IsDBNull(dr[SQL_COL_TENANT_TYPES]) ? "" : dr[SQL_COL_TENANT_TYPES]);
                m_ownerFilter = (bool)dr[SQL_COL_OWNER_FILTER];
                m_buildingFilter = (bool)dr[SQL_COL_BUILDING_FILTER];
                m_typeFilter = (bool)dr[SQL_COL_TYPE_FILTER];
                m_arrearsFilter = (bool)dr[SQL_COL_ARREARS_FILTER];
                m_arrearsAmount = (decimal)dr[SQL_COL_ARREARS_AMOUNT];
            } catch (OleDbException ex) {
            } catch (System.Exception exg) {
            }
            return this;

        }

        public static RecipientMaster load(int m_listId)
        {

            RecipientMaster obj = new RecipientMaster();
            string[] opt = { m_listId.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static RecipientMaster[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            RecipientMaster obj = new RecipientMaster();

            ds = obj.loadAll();
            obj = null;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new RecipientMaster();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (RecipientMaster[])list.ToArray(typeof(RecipientMaster));
			} catch (OleDbException ex) {
				return null;
			}

		}

        public static RecipientMaster[] loadByAccount(int acctId)
        {

            ArrayList list = new ArrayList();
            string[] opt = { acctId.ToString() };
            DataSet ds = new DataSet();
            RecipientMaster obj = new RecipientMaster();

            ds = obj.load(SQL_LOAD_BY_ACCOUNT, opt);
            obj = null;
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new RecipientMaster();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (RecipientMaster[])list.ToArray(typeof(RecipientMaster));
            }
            catch (OleDbException ex)
            {
                return null;
            }

        }



        public bool save()
		{

			if (m_listId > 0) {
				string[] values = {
					m_accountId.ToString(),
					m_listName,
					m_companyIds,
					m_buildingIds,
					m_tenantTypes,
					m_ownerFilter.ToString(),
					m_buildingFilter.ToString(),
					m_typeFilter.ToString(),
					m_arrearsFilter.ToString(),
					m_arrearsAmount.ToString(),
					m_listId.ToString()
                };
				if (this.updateRecord(m_listId.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_accountId.ToString(),
					m_listName,
					m_companyIds,
					m_buildingIds,
					m_tenantTypes,
					m_ownerFilter.ToString(),
					m_buildingFilter.ToString(),
					m_typeFilter.ToString(),
					m_arrearsFilter.ToString(),
					m_arrearsAmount.ToString()
                };
				m_listId = this.insertRecord(values);
				if (m_listId >= 0) {
					return true;
				}
			}
			return false;

		}

		public bool delete()
		{

            return this.delete(listId.ToString());

		}

		public Filter FilterByName(string name)
		{

			Filter[] flts = this.Filters;
			for (int i = 0; i <= NUM_FILTERS - 1; i++) {
				if (flts[i].PropertyName.Equals(name)) {
					return flts[i];
				}
			}
			return null;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_isChanged = true;
		}

		// Property Routines

		// Aggregate filters
		public Filter[] Filters {
			get {
				Filter[] flts = new Filter[NUM_FILTERS + 1];
				// Order is important, filters that reduce selection should come first
				flts[0] = new Filter(TypeFilter, FilterProperties[2]);
				flts[1] = new Filter(ArrearsFilter, FilterProperties[3], m_arrearsAmount.ToString());
				flts[2] = new Filter(OwnerFilter, FilterProperties[0], m_companyIds);
				flts[3] = new Filter(BuildingFilter, FilterProperties[1], m_buildingIds);
				return flts;
			}
		}
		public bool isChanged {
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}
		public int listId {
			get { return m_listId; }
		}

		public int accountId {
			get { return m_accountId; }
			set {
				m_accountId = value;
				OnPropertyChanged("accountId");
			}
		}

		public string listName {
			get { return m_listName; }
			set {
				m_listName = value;
				OnPropertyChanged("listName");
			}
		}

		public string companyIds {
			get { return m_companyIds; }
			set {
				m_companyIds = value;
				OnPropertyChanged("companyIds");
			}
		}

		public string buildingIds {
			get { return m_buildingIds; }
			set {
				m_buildingIds = value;
				OnPropertyChanged("buildingIds");
			}
		}

		public string tenantTypes {
			get { return m_tenantTypes; }
			set {
				m_tenantTypes = value;
				OnPropertyChanged("tenantTypes");
			}
		}

		public bool OwnerFilter {
			get { return m_ownerFilter; }
			set {
				m_ownerFilter = value;
				OnPropertyChanged("OwnerFilter");
			}
		}

		public bool BuildingFilter {
			get { return m_buildingFilter; }
			set {
				m_buildingFilter = value;
				OnPropertyChanged("BuildingFilter");
			}
		}

		public bool TypeFilter {
			get { return m_typeFilter; }
			set {
				m_typeFilter = value;
				OnPropertyChanged("TypeFilter");
			}
		}

		public bool ArrearsFilter {
			get { return m_arrearsFilter; }
			set {
				m_arrearsFilter = value;
				OnPropertyChanged("ArrearsFilter");
			}
		}

		public decimal ArrearsAmount {
			get { return m_arrearsAmount; }
			set {
				m_arrearsAmount = value;
				OnPropertyChanged("ArrearsAmount");
			}
		}

		public class Filter
		{

			private bool m_active;
			private string m_propertyName;

			private string[] m_values;
			public Filter(bool active, string propertyName)
			{
				m_active = active;
				m_propertyName = propertyName;
				m_values = null;
			}

			public Filter(bool active, string propertyName, string[] values)
			{
				m_active = active;
				m_propertyName = propertyName;
				if ((values != null) && values.Length > 0) {
					m_values = new string[values.Length];
					for (int i = 0; i <= values.Length - 1; i++) {
						m_values[i] = values[i];
					}
				}
			}

			public Filter(bool active, string propertyName, string values)
			{
				m_active = active;
				m_propertyName = propertyName;
				if ((values != null) && values.Length > 0) {
					string[] s = values.Split(',');
					if (s.Length > 0) {
						m_values = new string[s.Length];
						for (int i = 0; i <= s.Length - 1; i++) {
							m_values[i] = s[i];
						}
					}
				}
			}

			public bool Active {
				get { return m_active; }
			}
			public string PropertyName {
				get { return m_propertyName; }
			}
			public string[] Value {
				get { return m_values; }
				set { m_values = value; }
			}

		}
	}
}

