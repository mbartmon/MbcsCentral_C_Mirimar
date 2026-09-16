using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using ExtensionMethods;

namespace MbcsCentral
{
	public class NACHA_Layout : clsDB, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public static string TABLE = "NACHA_Layout";

		public enum Record_Types
		{
			ControlData = 0,
			FileHeader = 1,
			BatchHeader = 5,
			DetailRecord = 6,
			AddendaRecord = 7,
			BatchControl = 8,
			FileControl = 9,
			BatchVendorHeader = 20,
			BatchVendorControl = 21
		};

		public static string[] Requirements = { " ", "M", "O", "R" };

		public const int VendorControlOffset = 13;

		public enum Control_Types // Position Field
		{
			BatchNumber = 1,
			FileDirectory = 2,
			FilePrefix = 3,
			FileID = 4,
			OffsettingTransactionCode = 5,
			CheckingDebit = 6,
			SavingsDebit = 7,
			StatementMessage = 8,

			VendorBatchNumber = 21,
			VendorFileDirectory = 22,
			VendorFilePrefix = 23,
			VendorFileID = 24,
			VendorOffsettingTransactionCode = 25,
			VendorCheckingCredit = 26,
			VendorSavingsCredit = 27,
			VendorEmailMessage = 28
		}

		public enum Sec_Code_Types
		{
			PPD = 0,
			CCD = 1,
			CTX = 2,
			TEL = 3,
			WEB = 4
		}

		public static string[] Sec_Codes = {"PPD", "CCD", "CTX", "TEL", "WEB"};

		public const string SQL_COL_ID = "Id";
		public const string SQL_COL_POSITION = "Position";
		public const string SQL_COL_LENGTH = "Length";
		public const string SQL_COL_CONTENTS = "Contents";
		public const string SQL_COL_NAME = "Name";
		public const string SQL_COL_RECORD_TYPE = "Record_Type";
		public const string SQL_COL_REQUIREMENT = "Requirement";
		public const string SQL_COL_DATATYPE = "DataType";

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_POSITION + "]," +
				"[" + SQL_COL_LENGTH + "]," +
				"[" + SQL_COL_CONTENTS + "]," +
				"[" + SQL_COL_NAME + "]," +
				"[" + SQL_COL_RECORD_TYPE + "]," +
				"[" + SQL_COL_REQUIREMENT + "]," +
				"[" + SQL_COL_DATATYPE + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_ID + "]," +
				"[" + SQL_COL_POSITION + "]," +
				"[" + SQL_COL_LENGTH + "]," +
				"[" + SQL_COL_CONTENTS + "]," +
				"[" + SQL_COL_NAME + "]," +
				"[" + SQL_COL_RECORD_TYPE + "]," +
				"[" + SQL_COL_REQUIREMENT + "]," +
				"[" + SQL_COL_DATATYPE + "]" +
				" FROM " + TABLE + ";" +
				" ORDER BY [" + SQL_COL_RECORD_TYPE +"], [" + SQL_COL_POSITION + "]";

		static string SQL_LOAD_BY_TYPE = "SELECT " +
					"[" + SQL_COL_ID + "]," +
					"[" + SQL_COL_POSITION + "]," +
					"[" + SQL_COL_LENGTH + "]," +
					"[" + SQL_COL_CONTENTS + "]," +
					"[" + SQL_COL_NAME + "]," +
					"[" + SQL_COL_RECORD_TYPE + "]," +
					"[" + SQL_COL_REQUIREMENT + "]," +
					"[" + SQL_COL_DATATYPE + "]" +
					" FROM " + TABLE +
					" WHERE [" + SQL_COL_RECORD_TYPE + "] = '?'" +
					" ORDER BY [" + SQL_COL_POSITION + "];";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
						"[" + SQL_COL_POSITION + "]," +
				"[" + SQL_COL_LENGTH + "]," +
				"[" + SQL_COL_CONTENTS + "]," +
				"[" + SQL_COL_NAME + "]," +
				"[" + SQL_COL_RECORD_TYPE + "]," +
				"[" + SQL_COL_REQUIREMENT + "]," +
				"[" + SQL_COL_DATATYPE + "]" +
				") VALUES (?, ?, '?', '?', '?', '?', ?);";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_POSITION + "]=?," +
				"[" + SQL_COL_LENGTH + "]=?," +
				"[" + SQL_COL_CONTENTS + "]='?'," +
				"[" + SQL_COL_NAME + "]='?'," +
				"[" + SQL_COL_RECORD_TYPE + "]='?'," +
				"[" + SQL_COL_REQUIREMENT + "]='?'," +
				"[" + SQL_COL_DATATYPE + "]=?" +
				" WHERE " + "[" + SQL_COL_ID + "] = ?;";

		protected int m_Id;
		protected int m_Position;
		protected int m_Length;
		protected string m_Contents;
		protected string m_Name;
		protected string m_Record_Type;
		protected string m_Requirement;
		protected int m_DataType;
		protected bool m_isChanged;
		protected bool m_Delete;

		public NACHA_Layout() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public NACHA_Layout(int Id, int Position, int Length, string Contents, string Name, string Record_Type, string Requirement, int DataType) :
				base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_Id = Id;
			m_Position = Position;
			m_Length = Length;
			m_Contents = Contents;
			m_Name = Name;
			m_Record_Type = Record_Type;
			m_Requirement = Requirement;
			m_DataType = DataType;
		}

		protected NACHA_Layout fill(DataRow dr)
		{
			try
			{
				m_Id = (int)dr[SQL_COL_ID];
				m_Position = (int)dr[SQL_COL_POSITION];
				m_Length = (int)dr[SQL_COL_LENGTH];
				m_Contents = (string)(Convert.IsDBNull(dr[SQL_COL_CONTENTS]) ? "" : dr[SQL_COL_CONTENTS]);
				m_Name = (string)(Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
				m_Record_Type = (string)(Convert.IsDBNull(dr[SQL_COL_RECORD_TYPE]) ? "" : dr[SQL_COL_RECORD_TYPE]);
				m_Requirement = (string)(Convert.IsDBNull(dr[SQL_COL_REQUIREMENT]) ? "" : dr[SQL_COL_REQUIREMENT]);
				m_DataType = (int)dr[SQL_COL_DATATYPE];
			}
			catch (SqlException ex)
			{
			}
			return this;

		}

		public static NACHA_Layout load(int m_Id)
		{
			NACHA_Layout obj = new NACHA_Layout();
			string[] opt = { m_Id.ToString() };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			}
			else
				return null;

		}

		public static NACHA_Layout[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			NACHA_Layout obj = new NACHA_Layout();

			ds = obj.loadAll();
			if (ds == null)
				return null;
			obj = null;
			int f = 1;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new NACHA_Layout();
				obj.fill(dr);
				obj.Field = f++;
				list.Add(obj);
				obj = null;
			}
			try
			{
				return (NACHA_Layout[])list.ToArray(typeof(NACHA_Layout));
			}
			catch (SqlException ex)
			{
				return null;
			}

		}

		public static NACHA_Layout[] loadByType(int recordType)
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			NACHA_Layout obj = new NACHA_Layout();
			string[] opts = { recordType.ToString() };
			ds = obj.load(SQL_LOAD_BY_TYPE, opts);
			if (ds == null)
				return null;
			obj = null;
			int f = 1;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new NACHA_Layout();
				obj.fill(dr);
				obj.Field = f++;
				list.Add(obj);
				obj = null;
			}
			try
			{
				return (NACHA_Layout[])list.ToArray(typeof(NACHA_Layout));
			}
			catch (SqlException ex)
			{
				return null;
			}

		}

		public static NACHA_Layout[] loadAllTypes()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			NACHA_Layout obj = new NACHA_Layout();

			ds = obj.loadAll();
			if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				obj = new NACHA_Layout();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try
			{
				return (NACHA_Layout[])list.ToArray(typeof(NACHA_Layout));
			}
			catch (SqlException ex)
			{
				return null;
			}

		}

		public bool save()
		{
			if (m_Id > 0)
			{
				string[] values = { m_Position.ToString(), m_Length.ToString(), m_Contents, m_Name, m_Record_Type, m_Requirement, m_DataType.ToString(), m_Id.ToString() };
				if (this.updateRecord(m_Id.ToString(), values))
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
				string[] values = { m_Position.ToString(), m_Length.ToString(), m_Contents, m_Name, m_Record_Type, m_Requirement, m_DataType.ToString() };
				m_Id = this.insertRecord(values);
				if (m_Id >= 0)
				{
					return true;
				}
			}
			return false;

		}

		public static string record(DataRow dr)
		{
			string result = "";

			for (int i = 0; i < dr.ItemArray.Length; i++)
			{
				result += dr[i].ToString().Left(dr.Table.Columns[i].MaxLength, dr.Table.Columns[i].MaxLength);
			}
			return result;
		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				m_isChanged = true;
			}
		}

		// Properties
		public int Id
		{
			get { return m_Id; }
		}

		public int Position
		{
			set
			{
				m_Position = value;
				OnPropertyChanged("Position");
			}
			get { return m_Position; }
		}

		public int Length
		{
			set
			{
				m_Length = value;
				OnPropertyChanged("Length");
			}
			get { return m_Length; }
		}

		public string Contents
		{
			set
			{
				m_Contents = value;
				OnPropertyChanged("Contents");
			}
			get { return m_Contents; }
		}

		public string Name
		{
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
			get { return m_Name; }
		}

		public string Record_Type
		{
			set
			{
				m_Record_Type = value;
				OnPropertyChanged("Record_Type");
			}
			get { return m_Record_Type; }
		}

		public string Requirement
		{
			set
			{
				m_Requirement = value;
				OnPropertyChanged("Requirement");
			}
			get { return m_Requirement; }
		}

		public int DataType
		{
			set
			{
				m_DataType = value;
				OnPropertyChanged("DataType");
			}
			get { return m_DataType; }
		}
		public bool IsChanged
		{
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}

		public bool Delete
		{
			get { return m_Delete; }
			set
			{
				m_Delete = value;
				OnPropertyChanged("Delete");
			}
		}

		public int Field { get; set; }


		public static DataSet BuildRecordTypes()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("FileHeader");
			dt.Columns.Add(new DataColumn("RecordTypeCode", type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn("PriorityCode", type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn("ImmediateDestination", type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn("ImmediateOrigin", type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn("FileCreationDate", type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn("FileCreationTime", type) { MaxLength = 4 });
			dt.Columns.Add(new DataColumn("FileID", type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn("RecordSize", type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn("BlockingFactor", type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn("FormatCode", type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn("ImmediateDestinationName", type) { MaxLength = 23 });
			dt.Columns.Add(new DataColumn("ImmediateOriginName", type) { MaxLength = 23 });
			dt.Columns.Add(new DataColumn("Filler", type) { MaxLength = 8 });
			ds.Tables.Add(dt);
			return ds;
		}

	}

	public class NachaFileHeader
	{
		public const string COL_RECORD_ID = "RecordTypeCode";
		public const string COL_PRIORITY_CODE = "PriorityCode";
		public const string COL_IMMEDIATE_DESITNATION = "ImmediateDestination";
		public const string COL_IMMEDIATE_ORIGIN = "ImmediateOrigin";
		public const string COL_FILE_CREATION_DATE = "FileCreationDate";
		public const string COL_FILE_CREATION_TIME = "FileCreationTime";
		public const string COL_FILE_ID = "FileIDModifier";
		public const string COL_RECORD_SIZE = "RecordSize";
		public const string COL_BLOCKING_FACTOR = "BlockingFactor";
		public const string COL_FORMAT_CODE = "FormatCode";
		public const string COL_IMMEDIATE_DESTINATION_NAME = "ImmediateDestinationName";
		public const string COL_IMMEDIATE_ORIGIN_NAME = "ImmediateOriginName";

		protected DataRow dr;

		public NachaFileHeader()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("FileHeader");
			dt.Columns.Add(new DataColumn(COL_RECORD_ID, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_PRIORITY_CODE, type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn(COL_IMMEDIATE_DESITNATION, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_IMMEDIATE_ORIGIN, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_FILE_CREATION_DATE, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_FILE_CREATION_TIME, type) { MaxLength = 4 });
			dt.Columns.Add(new DataColumn(COL_FILE_ID, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_RECORD_SIZE, type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn(COL_BLOCKING_FACTOR, type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn(COL_FORMAT_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_IMMEDIATE_DESTINATION_NAME, type) { MaxLength = 23 });
			dt.Columns.Add(new DataColumn(COL_IMMEDIATE_ORIGIN_NAME, type) { MaxLength = 23 });
			dt.Columns.Add(new DataColumn("Filler", type) { MaxLength = 8, DefaultValue = " " });
			ds.Tables.Add(dt);
			dr = dt.NewRow();
		}


		public string RecordTypeCode { set { dr[COL_RECORD_ID] = value; } }
		public string PriorityCode { set { dr[COL_PRIORITY_CODE] = value; } }
		public string ImmediateDestination { set { dr[COL_IMMEDIATE_DESITNATION] = value; } }
		public string ImmediateOrigin { set { dr[COL_IMMEDIATE_ORIGIN] = value; } }
		public string FileCreationDate { set { dr[COL_FILE_CREATION_DATE] = value; } }
		public string FileCreationTime { set { dr[COL_FILE_CREATION_TIME] = value; } }
		public string FileId { set { dr[COL_FILE_ID] = value; } }
		public string RecordSize { set { dr[COL_RECORD_SIZE] = value; } }
		public string BlockingFactor { set { dr[COL_BLOCKING_FACTOR] = value; } }
		public string FormatCode { set { dr[COL_FORMAT_CODE] = value; } }
		public string ImmediateDestinationName { set { dr[COL_IMMEDIATE_DESTINATION_NAME] = value; } }
		public string ImmediateOriginName { set { dr[COL_IMMEDIATE_ORIGIN_NAME] = value; } }
		public DataRow DR { get { return dr; } }
	}


	public class NachaBatchTenantHeader
	{
		public const string COL_RECORD_CODE = "RecordTypeCode";
		public const string COL_SERVICE_CLASS_CODE = "ServiceClassCode";
		public const string COL_COMPANY_NAME = "CompanyName";
		public const string COL_COMPANY_DISCRETIONARY_DATA = "CompanyDiscretionaryData";
		public const string COL_COMPANY_IDENTIFICATION = "CompanyIdentification";
		public const string COL_STANDARD_ENTRY_CLASS_CODE = "StandardEntryClassCode";
		public const string COL_COMPANY_ENTRY_DESCRIPTION = "CompanyEntryDescription";
		public const string COL_COMPANY_DESCRIPTIVE_DATE = "CompanyDescriptiveDate";
		public const string COL_EFFECTIVE_DATE = "EffectiveDate";
		public const string COL_SETTLEMENT_DATE = "SettlementDate";
		public const string COL_ORIGINATOR_STATUS_CODE = "OriginatorStatusCode";
		public const string COL_ORIGINATING_DFI_IDENTIICATION = "OriginatingDfiIdentification";
		public const string COL_BATCH_NUMBER = "BatchNumber";

		protected DataRow dr;

		public NachaBatchTenantHeader()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("BatchTenantHeader");
			dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_SERVICE_CLASS_CODE, type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_NAME, type) { MaxLength = 16 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_DISCRETIONARY_DATA, type) { MaxLength = 20 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_IDENTIFICATION, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_STANDARD_ENTRY_CLASS_CODE, type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_ENTRY_DESCRIPTION, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_DESCRIPTIVE_DATE, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_EFFECTIVE_DATE, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_SETTLEMENT_DATE, type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn(COL_ORIGINATOR_STATUS_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_ORIGINATING_DFI_IDENTIICATION, type) { MaxLength = 8 });
			dt.Columns.Add(new DataColumn(COL_BATCH_NUMBER, type) { MaxLength = 7 });
			ds.Tables.Add(dt);
			dr = dt.NewRow();

		}

		public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
		public string ServiceClassCode { set { dr[COL_SERVICE_CLASS_CODE] = value; } }
		public string CompanyName { set { dr[COL_COMPANY_NAME] = value; } }
		public string CompanyDiscretionaryData { set { dr[COL_COMPANY_DISCRETIONARY_DATA] = value; } }
		public string CompanyIdentification { set { dr[COL_COMPANY_IDENTIFICATION] = value; } }
		public string StandardEntryClassCode { set { dr[COL_STANDARD_ENTRY_CLASS_CODE] = value; } }
		public string CompanyEntryDescription { set { dr[COL_COMPANY_ENTRY_DESCRIPTION] = value; } }
		public string CompanyDescriptiveDate { set { dr[COL_COMPANY_DESCRIPTIVE_DATE] = value; } }
		public string EffectiveDate { set { dr[COL_EFFECTIVE_DATE] = value; } }
		public string SettlementDate { set { dr[COL_SETTLEMENT_DATE] = value; } }
		public string OriginatorStatusCode { set { dr[COL_ORIGINATOR_STATUS_CODE] = value; } }
		public string OriginatingDfiIdentification { set { dr[COL_ORIGINATING_DFI_IDENTIICATION] = value; } }
		public string BatchNumber { set { dr[COL_BATCH_NUMBER] = value; } get { return (string)dr[COL_BATCH_NUMBER]; } }
		public DataRow DR { get { return dr; } }
	}

    public class NachaBatchVendorHeader
    {
        public const string COL_RECORD_CODE = "RecordTypeCode";
        public const string COL_SERVICE_CLASS_CODE = "ServiceClassCode";
        public const string COL_COMPANY_NAME = "CompanyName";
        public const string COL_COMPANY_DISCRETIONARY_DATA = "CompanyDiscretionaryData";
        public const string COL_COMPANY_IDENTIFICATION = "CompanyIdentification";
        public const string COL_STANDARD_ENTRY_CLASS_CODE = "StandardEntryClassCode";
        public const string COL_COMPANY_ENTRY_DESCRIPTION = "CompanyEntryDescription";
        public const string COL_COMPANY_DESCRIPTIVE_DATE = "CompanyDescriptiveDate";
        public const string COL_EFFECTIVE_DATE = "EffectiveDate";
        public const string COL_SETTLEMENT_DATE = "SettlementDate";
        public const string COL_ORIGINATOR_STATUS_CODE = "OriginatorStatusCode";
        public const string COL_ORIGINATING_DFI_IDENTIICATION = "OriginatingDfiIdentification";
        public const string COL_BATCH_NUMBER = "BatchNumber";

        protected DataRow dr;

        public NachaBatchVendorHeader()
        {
            System.Type type = System.Type.GetType("System.String");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("BatchVendorHeader");
            dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
            dt.Columns.Add(new DataColumn(COL_SERVICE_CLASS_CODE, type) { MaxLength = 3 });
            dt.Columns.Add(new DataColumn(COL_COMPANY_NAME, type) { MaxLength = 16 });
            dt.Columns.Add(new DataColumn(COL_COMPANY_DISCRETIONARY_DATA, type) { MaxLength = 20 });
            dt.Columns.Add(new DataColumn(COL_COMPANY_IDENTIFICATION, type) { MaxLength = 10 });
            dt.Columns.Add(new DataColumn(COL_STANDARD_ENTRY_CLASS_CODE, type) { MaxLength = 3 });
            dt.Columns.Add(new DataColumn(COL_COMPANY_ENTRY_DESCRIPTION, type) { MaxLength = 10 });
            dt.Columns.Add(new DataColumn(COL_COMPANY_DESCRIPTIVE_DATE, type) { MaxLength = 6 });
            dt.Columns.Add(new DataColumn(COL_EFFECTIVE_DATE, type) { MaxLength = 6 });
            dt.Columns.Add(new DataColumn(COL_SETTLEMENT_DATE, type) { MaxLength = 3 });
            dt.Columns.Add(new DataColumn(COL_ORIGINATOR_STATUS_CODE, type) { MaxLength = 1 });
            dt.Columns.Add(new DataColumn(COL_ORIGINATING_DFI_IDENTIICATION, type) { MaxLength = 8 });
            dt.Columns.Add(new DataColumn(COL_BATCH_NUMBER, type) { MaxLength = 7 });
            ds.Tables.Add(dt);
            dr = dt.NewRow();

        }

        public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
        public string ServiceClassCode { set { dr[COL_SERVICE_CLASS_CODE] = value; } }
        public string CompanyName { set { dr[COL_COMPANY_NAME] = value; } }
        public string CompanyDiscretionaryData { set { dr[COL_COMPANY_DISCRETIONARY_DATA] = value; } }
        public string CompanyIdentification { set { dr[COL_COMPANY_IDENTIFICATION] = value; } }
        public string StandardEntryClassCode { set { dr[COL_STANDARD_ENTRY_CLASS_CODE] = value; } }
        public string CompanyEntryDescription { set { dr[COL_COMPANY_ENTRY_DESCRIPTION] = value; } }
        public string CompanyDescriptiveDate { set { dr[COL_COMPANY_DESCRIPTIVE_DATE] = value; } }
        public string EffectiveDate { set { dr[COL_EFFECTIVE_DATE] = value; } }
        public string SettlementDate { set { dr[COL_SETTLEMENT_DATE] = value; } }
        public string OriginatorStatusCode { set { dr[COL_ORIGINATOR_STATUS_CODE] = value; } }
        public string OriginatingDfiIdentification { set { dr[COL_ORIGINATING_DFI_IDENTIICATION] = value; } }
        public string BatchNumber { set { dr[COL_BATCH_NUMBER] = value; } get { return (string)dr[COL_BATCH_NUMBER]; } }
        public DataRow DR { get { return dr; } }
    }

    public class NachaDetail
	{
		public const string COL_RECORD_CODE = "RecordTypeCode";
		public const string COL_TRANSACTION_CODE = "TransactionCode";
		public const string COL_RECEIVING_DFI_IDENTIFICATION = "ReceivingDfiIdentification";
		public const string COL_CHECK_DIGIT = "CheckDigit";
		public const string COL_DFI_ACCOUNT_NUMBER = "DfiAccountNumber";
		public const string COL_AMOUNT = "Amount";
		public const string COL_INDIVIDUAL_IDENTIFICATION_NUMBER = "IndividualIdentificationNumber";
		public const string COL_INDIVIDUAL_NAME = "IndividualName";
		public const string COL_DISCRETIONARY_DATA = "DiscretionaryData";
		public const string COL_ADDENDA_RECORD_INDICATOR = "AddendaRecordIndicator";
		public const string COL_TRACE_NUMBER = "TraceNumber";

		protected DataRow dr;

		public NachaDetail()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("Detail");
			dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_TRANSACTION_CODE, type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn(COL_RECEIVING_DFI_IDENTIFICATION, type) { MaxLength = 8 });
			dt.Columns.Add(new DataColumn(COL_CHECK_DIGIT, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_DFI_ACCOUNT_NUMBER, type) { MaxLength = 17 });
			dt.Columns.Add(new DataColumn(COL_AMOUNT, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_INDIVIDUAL_IDENTIFICATION_NUMBER) { MaxLength = 15 });
			dt.Columns.Add(new DataColumn(COL_INDIVIDUAL_NAME, type) { MaxLength = 22 });
			dt.Columns.Add(new DataColumn(COL_DISCRETIONARY_DATA, type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn(COL_ADDENDA_RECORD_INDICATOR, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_TRACE_NUMBER, type) { MaxLength = 15 });
			ds.Tables.Add(dt);
			dr = dt.NewRow();
		}

		public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
		public string TransactionCode { set { dr[COL_TRANSACTION_CODE] = value; } }
		public string ReceivingDfiIdentification { set { dr[COL_RECEIVING_DFI_IDENTIFICATION] = value; } }
		public string CheckDigit { set { dr[COL_CHECK_DIGIT] = value; } }
		public string DfiAccountNumber { set { dr[COL_DFI_ACCOUNT_NUMBER] = value; } }
		public string Amount { set { dr[COL_AMOUNT] = value; } }
		public string IndividualIdentificationNumber { set { dr[COL_INDIVIDUAL_IDENTIFICATION_NUMBER] = value; } }
		public string IndividualName { set { dr[COL_INDIVIDUAL_NAME] = value; } }
		public string DiscretionaryData { set { dr[COL_DISCRETIONARY_DATA] = value; } }
		public string AddendaRecordIndicator { set { dr[COL_ADDENDA_RECORD_INDICATOR] = value; } }
		public string TraceNumber
		{
			set { dr[COL_TRACE_NUMBER] = value; }
			get { if (dr[COL_TRACE_NUMBER] != null) return dr[COL_TRACE_NUMBER].ToString();
				else
					return "";
			}
		}
		public DataRow DR { get { return dr; } }

	}

	public class NachaAddenda
	{
		public const string COL_RECORD_CODE = "RecordTypeCode";
		public const string COL_TRANSACTION_CODE = "TransactionCode";
		public const string COL_DATA = "Data";
		public const string COL_ADDENDA_SEQUENCE_NUMBER = "AddendaSequenceNumber";
		public const string COL_DETAIL_SEQUENCE_NUMBER = "DetailSequenceNumber";

		protected DataRow dr;

		public NachaAddenda()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("Addenda");
			dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_TRANSACTION_CODE, type) { MaxLength = 2 });
			dt.Columns.Add(new DataColumn(COL_DATA, type) { MaxLength = 80 });
			dt.Columns.Add(new DataColumn(COL_ADDENDA_SEQUENCE_NUMBER, type) { MaxLength = 4 });
			dt.Columns.Add(new DataColumn(COL_DETAIL_SEQUENCE_NUMBER, type) { MaxLength = 7 });
            ds.Tables.Add(dt);
            dr = dt.NewRow();
        }

        public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
		public string TransactionCode { set { dr[COL_TRANSACTION_CODE] = value; } }
		public string Data { set { dr[COL_DATA] = value; } }
		public string AddendaSequenceNumber { set { dr[COL_ADDENDA_SEQUENCE_NUMBER] = value; } }
		public string DetailSequenceNumber { set { dr[COL_DETAIL_SEQUENCE_NUMBER] = value; } }
		public DataRow DR { get { return dr; } }
	}
	public class NachaBatchControl
	{
		public const string COL_RECORD_CODE = "RecordTypeCode";
		public const string COL_SERVICE_CLASS_CODE = "ServiceClassCode";
		public const string COL_ENTRY_ADDENDA_COUNT = "EntryAddendaCount";
		public const string COL_ENTRY_HASH = "EntryHash";
		public const string COL_TOTAL_DEBIT_AMOUNT = "TotalDebitAmount";
		public const string COL_TOTAL_CREDIT_AMOUNT = "TotalCreditAmount";
		public const string COL_COMPANY_IDENTIFICATION = "ComapanyIdentification";
		public const string COL_MESSAGE_AUTHENTICATION_CODE = "MessageAuthenticationCode";
		public const string COL_RESERVED = "Reserved";
		public const string COL_ORIGINATING_DFI_IDENTIFICATION = "OriginatingDfiIdentification";
		public const string COL_BATCH_NUMBER = "BatchNumber";

		protected DataRow dr;

		public NachaBatchControl()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("BatchControl");
			dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_SERVICE_CLASS_CODE, type) { MaxLength = 3 });
			dt.Columns.Add(new DataColumn(COL_ENTRY_ADDENDA_COUNT, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_ENTRY_HASH, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_TOTAL_DEBIT_AMOUNT, type) { MaxLength = 12 });
			dt.Columns.Add(new DataColumn(COL_TOTAL_CREDIT_AMOUNT, type) { MaxLength = 12 });
			dt.Columns.Add(new DataColumn(COL_COMPANY_IDENTIFICATION, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_MESSAGE_AUTHENTICATION_CODE, type) { MaxLength = 19 });
			dt.Columns.Add(new DataColumn(COL_RESERVED, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_ORIGINATING_DFI_IDENTIFICATION, type) { MaxLength = 8 });
			dt.Columns.Add(new DataColumn(COL_BATCH_NUMBER, type) { MaxLength = 7 });
			ds.Tables.Add(dt);
			dr = dt.NewRow();
		}

		public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
		public string ServiceClassCode { set { dr[COL_SERVICE_CLASS_CODE] = value; } }
		public string EntryAddendaCount { set { dr[COL_ENTRY_ADDENDA_COUNT] = value; } }
		public string EntryHash { set { dr[COL_ENTRY_HASH] = value; } }
		public string TotalDebitAmount { set { dr[COL_TOTAL_DEBIT_AMOUNT] = value; } }
		public string TotalCreditAmount { set { dr[COL_TOTAL_CREDIT_AMOUNT] = value; } }
		public string CompanyIdentification { set { dr[COL_COMPANY_IDENTIFICATION] = value; } }
		public string MessageAuthenticationCode { set { dr[COL_MESSAGE_AUTHENTICATION_CODE] = value; } }
		public string Reserved { set { dr[COL_RESERVED] = value; } }
		public string OriginatingDfiIdentification { set { dr[COL_ORIGINATING_DFI_IDENTIFICATION] = value; } }
		public string BatchNumber { set { dr[COL_BATCH_NUMBER] = value; } }
		public DataRow DR { get { return dr; } }

	}

	public class NachaFileControl
	{
		public const string COL_RECORD_CODE = "RecordTypeCode";
		public const string COL_BATCH_COUNT = "BatchCount";
		public const string COL_BLOCK_COUNT = "BlockCount";
		public const string COL_ENTRY_ADDENDA_COUNT = "EntryAddendaCount";
		public const string COL_ENTRY_HASH = "EntryHash";
		public const string COL_TOTAL_DEBIT_AMOUNT = "TotalDebitAmount";
		public const string COL_TOTAL_CREDIT_AMOUNT = "TotalCreditAmount";
		public const string COL_RESERVED = "Reserved";

		protected DataRow dr;

		public NachaFileControl()
		{
			System.Type type = System.Type.GetType("System.String");
			DataSet ds = new DataSet();
			DataTable dt = new DataTable("FileControl");
			dt.Columns.Add(new DataColumn(COL_RECORD_CODE, type) { MaxLength = 1 });
			dt.Columns.Add(new DataColumn(COL_BATCH_COUNT, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_BLOCK_COUNT, type) { MaxLength = 6 });
			dt.Columns.Add(new DataColumn(COL_ENTRY_ADDENDA_COUNT, type) { MaxLength = 8 });
			dt.Columns.Add(new DataColumn(COL_ENTRY_HASH, type) { MaxLength = 10 });
			dt.Columns.Add(new DataColumn(COL_TOTAL_DEBIT_AMOUNT, type) { MaxLength = 12 });
			dt.Columns.Add(new DataColumn(COL_TOTAL_CREDIT_AMOUNT, type) { MaxLength = 12 });
			dt.Columns.Add(new DataColumn(COL_RESERVED, type) { MaxLength = 39 });
			ds.Tables.Add(dt);
			dr = dt.NewRow();
		}

		public string RecordTypeCode { set { dr[COL_RECORD_CODE] = value; } }
		public string BatchCount { set { dr[COL_BATCH_COUNT] = value; } }
		public string BlockCount { set { dr[COL_BLOCK_COUNT] = value; } }
		public string EntryAddendaCount { set { dr[COL_ENTRY_ADDENDA_COUNT] = value; } }
		public string EntryHash { set { dr[COL_ENTRY_HASH] = value; } }
		public string TotalDebitAmount { set { dr[COL_TOTAL_DEBIT_AMOUNT] = value; } }
		public string TotalCreditAmount { set { dr[COL_TOTAL_CREDIT_AMOUNT] = value; } }
		public string Reserved { set { dr[COL_RECORD_CODE] = value; } }
		public DataRow DR { get { return dr; } }
	}

}

namespace BindingEnums
{
	public enum DataTypes
	{
		Alphanumeric = 1,
		Numeric = 2,
		Currency = 3,
		Date = 4,
		Time = 5
	}

}
