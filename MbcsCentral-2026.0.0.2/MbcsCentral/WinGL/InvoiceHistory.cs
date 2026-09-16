using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL
{
    public class InvoiceHistory : clsDB, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static string TABLE = "gl_InvoiceHistory";

        public const string SQL_COL_DATE = "Date";
        public const string SQL_COL_PAY_DUE_DATE = "PayDueDate";
        public const string SQL_COL_DATE_LAST_PAID = "DateLastPaid";
        public const string SQL_COL_CHECK_DATE = "CheckDate";
        public const string SQL_COL_CHECK_GROUP = "CheckGroup";
        public const string SQL_COL_PAY_FLAG = "PayFlag";
        public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
        public const string SQL_COL_BATCH = "Batch";
        public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
        public const string SQL_COL_COMPANY = "Company";
        public const string SQL_COL_DEPARTMENT = "Department";
        public const string SQL_COL_INVOICE_KEY = "InvoiceKey";
        public const string SQL_COL_ID = "Id";
        public const string SQL_COL_MEMO_ID = "MemoId";
        public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_ALLOWANCE = "Allowance";
        public const string SQL_COL_DISCOUNT = "Discount";
        public const string SQL_COL_DISCOUNT_AMOUNT = "DiscountAmount";
        public const string SQL_COL_AMOUNT_PAID = "AmountPaid";
        public const string SQL_COL_AMOUNT_TO_PAY = "AmountToPay";
        public const string SQL_COL_ALLOCATION_FLAG = "AllocationFlag";
        public const string SQL_COL_INVOICE_NUMBER = "InvoiceNumber";
        public const string SQL_COL_DESCRIPTION = "Description";
        public const string SQL_COL_MEMO = "Memo";
        public const string SQL_COL_PREFIX = "Prefix";
        public const string SQL_COL_SUFFIX = "Suffix";
        public const string SQL_COL_EFT = "EFT";
        public const string SQL_COL_DISCOUNT_DOLLAR = "DiscDollar";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE + ";";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                ") VALUES ('?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', ?, ?);";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_DATE + "]='?'," +
                "[" + SQL_COL_PAY_DUE_DATE + "]='?'," +
                "[" + SQL_COL_DATE_LAST_PAID + "]='?'," +
                "[" + SQL_COL_CHECK_DATE + "]='?'," +
                "[" + SQL_COL_CHECK_GROUP + "]=?," +
                "[" + SQL_COL_PAY_FLAG + "]=?," +
                "[" + SQL_COL_VENDOR_NUMBER + "]=?," +
                "[" + SQL_COL_BATCH + "]=?," +
                "[" + SQL_COL_CHECK_NUMBER + "]=?," +
                "[" + SQL_COL_COMPANY + "]=?," +
                "[" + SQL_COL_DEPARTMENT + "]=?," +
                "[" + SQL_COL_INVOICE_KEY + "]=?," +
                "[" + SQL_COL_MEMO_ID + "]=?," +
                "[" + SQL_COL_AMOUNT + "]=?," +
                "[" + SQL_COL_ALLOWANCE + "]=?," +
                "[" + SQL_COL_DISCOUNT + "]=?," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]=?," +
                "[" + SQL_COL_AMOUNT_PAID + "]=?," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]=?," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]=?," +
                "[" + SQL_COL_INVOICE_NUMBER + "]='?'," +
                "[" + SQL_COL_DESCRIPTION + "]='?'," +
                "[" + SQL_COL_MEMO + "]='?'," +
                "[" + SQL_COL_PREFIX + "]='?'," +
                "[" + SQL_COL_SUFFIX + "]='?'," +
                "[" + SQL_COL_EFT + "]=?," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]=?" +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_BY_INVOICE = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_INVOICE_NUMBER + "] = '?'";

        static string SQL_LOAD_BY_INVOICE_FULL = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND [" + SQL_COL_PREFIX + "] = '?'" +
                " AND [" + SQL_COL_SUFFIX +"] = '?'" +
                " AND " + "[" + SQL_COL_INVOICE_NUMBER + "] = '?'";
    

        static string SQL_LOAD_BY_VENDOR = "SELECT " +
         " TOP (?) " +
        "[" + SQL_COL_DATE + "]," +
        "[" + SQL_COL_PAY_DUE_DATE + "]," +
        "[" + SQL_COL_DATE_LAST_PAID + "]," +
        "[" + SQL_COL_CHECK_DATE + "]," +
        "[" + SQL_COL_CHECK_GROUP + "]," +
        "[" + SQL_COL_PAY_FLAG + "]," +
        "[" + SQL_COL_VENDOR_NUMBER + "]," +
        "[" + SQL_COL_BATCH + "]," +
        "[" + SQL_COL_CHECK_NUMBER + "]," +
        "[" + SQL_COL_COMPANY + "]," +
        "[" + SQL_COL_DEPARTMENT + "]," +
        "[" + SQL_COL_INVOICE_KEY + "]," +
        "[" + SQL_COL_ID + "]," +
        "[" + SQL_COL_MEMO_ID + "]," +
        "[" + SQL_COL_AMOUNT + "]," +
        "[" + SQL_COL_ALLOWANCE + "]," +
        "[" + SQL_COL_DISCOUNT + "]," +
        "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
        "[" + SQL_COL_AMOUNT_PAID + "]," +
        "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
        "[" + SQL_COL_ALLOCATION_FLAG + "]," +
        "[" + SQL_COL_INVOICE_NUMBER + "]," +
        "[" + SQL_COL_DESCRIPTION + "]," +
        "[" + SQL_COL_MEMO + "]," +
        "[" + SQL_COL_PREFIX + "]," +
        "[" + SQL_COL_SUFFIX + "]," +
        "[" + SQL_COL_EFT + "]," +
        "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
        " FROM " + TABLE +
        " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
        " ORDER BY [" + SQL_COL_ID + "] DESC";


        static string SQL_LOAD_BY_INVOICE_BY_CHECK = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_CHECK_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_COMPANY + "] = ?" +
                " AND " + "[" + SQL_COL_DEPARTMENT + "] = ?";

        static string SQL_LOAD_BY_CHECK = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_CHECK_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_COMPANY + "] = ?" +
                " AND " + "[" + SQL_COL_DEPARTMENT + "] = ?";

        protected DateTime m_Date;
		protected DateTime m_PayDueDate;
		protected DateTime m_DateLastPaid;
		protected DateTime m_CheckDate;
		protected short m_CheckGroup;
		protected short m_PayFlag;
		protected int m_VendorNumber;
		protected int m_Batch;
		protected int m_CheckNumber;
		protected int m_Company;
		protected int m_Department;
		protected int m_InvoiceKey;
		protected int m_Id;
		protected int m_MemoId;
		protected decimal m_Amount;
		protected decimal m_Allowance;
		protected decimal m_Discount;
		protected decimal m_DiscountAmount;
		protected decimal m_AmountPaid;
		protected decimal m_AmountToPay;
		protected bool m_AllocationFlag;
		protected string m_InvoiceNumber;
		protected string m_Description;
		protected string m_Memo;
        protected string m_Prefix;
        protected string m_Suffix;
        protected bool m_EFT;
        protected bool m_DiscountDollar;
        protected bool m_isChanged;
        protected bool m_PrePaid = false;

        // InvoiceList
        protected string m_VendorName;
        protected string m_DepartmentName;
        protected Vendors m_Vendor;

        public InvoiceHistory() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public InvoiceHistory(DateTime Date,DateTime PayDueDate,DateTime DateLastPaid,DateTime CheckDate,short CheckGroup,short PayFlag,int VendorNumber,int Batch,int CheckNumber,int Company,int Department,int InvoiceKey,int Id,int MemoId,decimal Amount,decimal Allowance,decimal Discount,decimal DiscountAmount,decimal AmountPaid,decimal AmountToPay,bool AllocationFlag,string InvoiceNumber,string Description,string Memo) : 
				base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_Date = Date;
			m_PayDueDate = PayDueDate;
			m_DateLastPaid = DateLastPaid;
			m_CheckDate = CheckDate;
			m_CheckGroup = CheckGroup;
			m_PayFlag = PayFlag;
			m_VendorNumber = VendorNumber;
			m_Batch = Batch;
			m_CheckNumber = CheckNumber;
			m_Company = Company;
			m_Department = Department;
			m_InvoiceKey = InvoiceKey;
			m_Id = Id;
			m_MemoId = MemoId;
			m_Amount = Amount;
			m_Allowance = Allowance;
			m_Discount = Discount;
			m_DiscountAmount = DiscountAmount;
			m_AmountPaid = AmountPaid;
			m_AmountToPay = AmountToPay;
			m_AllocationFlag = AllocationFlag;
			m_InvoiceNumber = InvoiceNumber;
			m_Description = Description;
			m_Memo = Memo;
    }

    protected InvoiceHistory fill(DataRow dr)
		{
			try {
				m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
				m_PayDueDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_PAY_DUE_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_PAY_DUE_DATE]);
				m_DateLastPaid = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE_LAST_PAID]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE_LAST_PAID]);
				m_CheckDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_CHECK_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_CHECK_DATE]);
				m_CheckGroup = (short) dr[SQL_COL_CHECK_GROUP];
				m_PayFlag = (short) dr[SQL_COL_PAY_FLAG];
				m_VendorNumber = (int) dr[SQL_COL_VENDOR_NUMBER];
				m_Batch = (int) dr[SQL_COL_BATCH];
				m_CheckNumber = (int) dr[SQL_COL_CHECK_NUMBER];
				m_Company = (int) dr[SQL_COL_COMPANY];
				m_Department = (int) dr[SQL_COL_DEPARTMENT];
				m_InvoiceKey = (int) dr[SQL_COL_INVOICE_KEY];
				m_Id = (int) dr[SQL_COL_ID];
				m_MemoId = (int) dr[SQL_COL_MEMO_ID];
				m_Amount = (decimal) dr[SQL_COL_AMOUNT];
				m_Allowance = (decimal) dr[SQL_COL_ALLOWANCE];
				m_Discount = (decimal) dr[SQL_COL_DISCOUNT];
				m_DiscountAmount = (decimal) dr[SQL_COL_DISCOUNT_AMOUNT];
				m_AmountPaid = (decimal) dr[SQL_COL_AMOUNT_PAID];
				m_AmountToPay = (decimal) dr[SQL_COL_AMOUNT_TO_PAY];
				m_AllocationFlag = (bool) dr[SQL_COL_ALLOCATION_FLAG];
				m_InvoiceNumber = (string) (Convert.IsDBNull(dr[SQL_COL_INVOICE_NUMBER]) ? "" : dr[SQL_COL_INVOICE_NUMBER]);
				m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
				m_Memo = (string) (Convert.IsDBNull(dr[SQL_COL_MEMO]) ? "" : dr[SQL_COL_MEMO]);
                m_Prefix = (string)dr[SQL_COL_PREFIX];
                m_Suffix = (string)dr[SQL_COL_SUFFIX];
                m_EFT = (dr[SQL_COL_EFT] is DBNull ? false : (bool)dr[SQL_COL_EFT]);
                m_DiscountDollar = (dr[SQL_COL_DISCOUNT_DOLLAR] is DBNull ? false : (bool)dr[SQL_COL_DISCOUNT_DOLLAR]);
        }
        catch (SqlException ex) {
			}
			return this;

		}

		public static InvoiceHistory load( int m_Id)		{
			InvoiceHistory obj  = new InvoiceHistory();
			string[] opt = {m_Id.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static InvoiceHistory[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			InvoiceHistory obj = new InvoiceHistory();

			ds = obj.loadAll();
		    if (ds == null || ds.Tables[0].Rows.Count == 0)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new InvoiceHistory();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (InvoiceHistory[])list.ToArray(typeof(InvoiceHistory));
			} catch (SqlException ex) {
				return null;
			}

		}

        public static InvoiceHistory LoadByInvoice(int vendor, string invoice)
        {
            InvoiceHistory obj = new InvoiceHistory();
            string[] opt = {vendor.ToString(), invoice };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_INVOICE, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public static InvoiceHistory[] LoadByVendor(int vendor, long top = 0x7fffffffffffffff)
        {
            ArrayList list = new ArrayList();
            InvoiceHistory obj = new InvoiceHistory();
            string[] opt = {top.ToString(), vendor.ToString()};
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_VENDOR, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new InvoiceHistory();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (InvoiceHistory[])list.ToArray(typeof(InvoiceHistory));
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
            else
                return null;

        }

        public static InvoiceHistory[] LoadByCheck(int check, int vendor, int company, int department)
        {
            ArrayList list = new ArrayList();

            InvoiceHistory obj = new InvoiceHistory();
            string[] opt = {check.ToString(), vendor.ToString(), company.ToString(), department.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_CHECK, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new InvoiceHistory();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (InvoiceHistory[])list.ToArray(typeof(InvoiceHistory));
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
            else
                return null;

        }

        public static InvoiceHistory[] LoadQuery(string query, bool add = false)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            InvoiceHistory obj = new InvoiceHistory();
            string[] opt = null;

            ds = obj.load(query, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new InvoiceHistory();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (InvoiceHistory[])list.ToArray(typeof(InvoiceHistory));
            }
            catch (SqlException ex)
            {
                return null;
            }


        }


        public static bool InHistory(long vnum, string Invoice, string prefix = "", string suffix = "")
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            InvoiceHistory obj = new InvoiceHistory();
            string[] opt = {vnum.ToString(), prefix, suffix, Invoice };


            bool inHistory = false;
            

            ds = obj.load(SQL_LOAD_BY_INVOICE_FULL, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return inHistory;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new InvoiceHistory();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }

            if (list.Count > 0)
            {
                InvoiceHistory tbl = (InvoiceHistory)list[0];
                if (tbl != null)
                {
                    string xsql = "This invoice may already have been paid to this vendor from:\r\n\n" + "Date: " + tbl.Date.ToString();
                    xsql += "\r\nCheck# " + tbl.CheckNumber + "\r\nAmount: " + tbl.AmountPaid.ToString("###,###,##0.00");
                    xsql += "\r\nCompany: " + tbl.Company.ToString() + "\r\nDepartment: " + tbl.Department;
                    xsql += "\r\n\nDo you wish to store this invoice anyway?";
                    if (MessageBoxCustom.Show(xsql, "", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
                        inHistory = true;
                }
            }
            return inHistory;
        }

        		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

		{
			if (m_Id > 0 ) {
				string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(),
                    m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(),
                    m_Department.ToString(), m_InvoiceKey.ToString(), m_MemoId.ToString(), m_Amount.ToString(), m_Allowance.ToString(),
                    m_Discount.ToString(), m_DiscountAmount.ToString(), m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(),
                    m_InvoiceNumber, m_Description, m_Memo, m_Prefix, m_Suffix, m_EFT.ToString(), m_DiscountDollar.ToString(), m_Id.ToString()};
				if (this.updateRecord(m_Id.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(),
                    m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(),
                    m_Department.ToString(), m_InvoiceKey.ToString(), m_MemoId.ToString(), m_Amount.ToString(), m_Allowance.ToString(),
                    m_Discount.ToString(), m_DiscountAmount.ToString(), m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(),
                    m_InvoiceNumber, m_Description, m_Memo, m_Prefix, m_Suffix, m_EFT.ToString(), m_DiscountDollar.ToString()};
				m_Id = this.insertRecord(values);
				if (m_Id >= 0) {
					return true;
				}
			}
			return false;

		}

		private void OnPropertyChanged(string info) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				m_isChanged = true;
			}
		}

		// Properties
		public DateTime Date {
			set{ m_Date = value;
			OnPropertyChanged("Date"); }
			get { return m_Date; }
		}

		public DateTime PayDueDate {
			set{ m_PayDueDate = value;
			OnPropertyChanged("PayDueDate"); }
			get { return m_PayDueDate; }
		}

		public DateTime DateLastPaid {
			set{ m_DateLastPaid = value;
			OnPropertyChanged("DateLastPaid"); }
			get { return m_DateLastPaid; }
		}

		public DateTime CheckDate {
			set{ m_CheckDate = value;
			OnPropertyChanged("CheckDate"); }
			get { return m_CheckDate; }
		}

		public short CheckGroup {
			set{ m_CheckGroup = value;
			OnPropertyChanged("CheckGroup"); }
			get { return m_CheckGroup; }
		}

		public short PayFlag {
			set{ m_PayFlag = value;
			OnPropertyChanged("PayFlag"); }
			get { return m_PayFlag; }
		}

		public int VendorNumber {
			set{ m_VendorNumber = value;
			OnPropertyChanged("VendorNumber"); }
			get { return m_VendorNumber; }
		}

		public int Batch {
			set{ m_Batch = value;
			OnPropertyChanged("Batch"); }
			get { return m_Batch; }
		}

		public int CheckNumber {
			set{ m_CheckNumber = value;
			OnPropertyChanged("CheckNumber"); }
			get { return m_CheckNumber; }
		}

		public int Company {
			set{ m_Company = value;
			OnPropertyChanged("Company"); }
			get { return m_Company; }
		}

		public int Department {
			set{ m_Department = value;
			OnPropertyChanged("Department"); }
			get { return m_Department; }
		}

		public int InvoiceKey {
			set{ m_InvoiceKey = value;
			OnPropertyChanged("InvoiceKey"); }
			get { return m_InvoiceKey; }
		}

		public int Id {
			get { return m_Id; }
		}

		public int MemoId {
			set{ m_MemoId = value;
			OnPropertyChanged("MemoId"); }
			get { return m_MemoId; }
		}

		public decimal Amount {
			set{ m_Amount = value;
			OnPropertyChanged("Amount"); }
			get { return m_Amount; }
		}

		public decimal Allowance {
			set{ m_Allowance = value;
			OnPropertyChanged("Allowance"); }
			get { return m_Allowance; }
		}

		public decimal Discount {
			set{ m_Discount = value;
			OnPropertyChanged("Discount"); }
			get { return m_Discount; }
		}

		public decimal DiscountAmount {
			set{ m_DiscountAmount = value;
			OnPropertyChanged("DiscountAmount"); }
			get { return m_DiscountAmount; }
		}

        public bool DiscountDollar
        {
            set { m_DiscountDollar = value;
                OnPropertyChanged("DiscountDollar");
            }
            get { return m_DiscountDollar; }
        }

		public decimal AmountPaid {
			set{ m_AmountPaid = value;
			OnPropertyChanged("AmountPaid"); }
			get { return m_AmountPaid; }
		}

		public decimal AmountToPay {
			set{ m_AmountToPay = value;
			OnPropertyChanged("AmountToPay"); }
			get { return m_AmountToPay; }
		}

		public bool AllocationFlag {
			set{ m_AllocationFlag = value;
			OnPropertyChanged("AllocationFlag"); }
			get { return m_AllocationFlag; }
		}

		public string InvoiceNumber {
			set{ m_InvoiceNumber = value;
			OnPropertyChanged("InvoiceNumber"); }
			get { return m_InvoiceNumber; }
		}

		public string Description {
			set{ m_Description = value;
			OnPropertyChanged("Description"); }
			get { return m_Description; }
		}

		public string Memo {
			set{ m_Memo = value;
			OnPropertyChanged("Memo"); }
			get { return m_Memo; }
		}

        public string Prefix
        {
            set
            {
                m_Prefix = value;
                OnPropertyChanged("Prefix");
            }
            get { return m_Prefix; }
        }

        public string Suffix
        {
            set
            {
                m_Suffix = value;
                OnPropertyChanged("Suffix");
            }
            get { return m_Suffix; }
        }

        public bool EFT
        {
            set { m_EFT = value;
                OnPropertyChanged("EFT");
            }
            get { return m_EFT; }
        }

        public string VendorName
        {
            get { return m_VendorName; }
        }

        public string DepartmentName
        {
            get { return m_DepartmentName; }
        }

        public Vendors Vendor
        {
            get { return m_Vendor; }
        }

        public bool PrePaid
        {
            get { return m_PrePaid; }
            set
            {
                m_PrePaid = value;
                OnPropertyChanged("PrePaid");
            }
        }

        /// <summary>
		/// Has record been modified?
		/// </summary>
	public bool IsChanged

        {
            get { return m_isChanged; }
            set { m_isChanged = value; }
        }
        public string FullInvoiceNumber { get { return m_Prefix.Trim() + " " + m_InvoiceNumber + " " + m_Suffix.TrimEnd(); } }
    }
}
