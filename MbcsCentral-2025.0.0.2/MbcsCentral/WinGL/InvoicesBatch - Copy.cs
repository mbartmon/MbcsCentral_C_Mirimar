using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class InvoicesBatch : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_InvoicesBatch";

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

    private static string SQL_LOAD = "SELECT " +
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
                                     "[" + SQL_COL_AMOUNT + "]," +
                                     "[" + SQL_COL_ALLOWANCE + "]," +
                                     "[" + SQL_COL_DISCOUNT + "]," +
                                     "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                     "[" + SQL_COL_AMOUNT_PAID + "]," +
                                     "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                     "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                     "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                     "[" + SQL_COL_DESCRIPTION + "]," +
                                     "[" + SQL_COL_MEMO + "]" +
                                     " FROM " + TABLE +
                                     " WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";

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
			"[" + SQL_COL_AMOUNT + "]," +
			"[" + SQL_COL_ALLOWANCE + "]," +
			"[" + SQL_COL_DISCOUNT + "]," +
			"[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
			"[" + SQL_COL_AMOUNT_PAID + "]," +
			"[" + SQL_COL_AMOUNT_TO_PAY + "]," +
			"[" + SQL_COL_ALLOCATION_FLAG + "]," +
			"[" + SQL_COL_INVOICE_NUMBER + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_MEMO + "]" +
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
			"[" + SQL_COL_AMOUNT + "]," +
			"[" + SQL_COL_ALLOWANCE + "]," +
			"[" + SQL_COL_DISCOUNT + "]," +
			"[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
			"[" + SQL_COL_AMOUNT_PAID + "]," +
			"[" + SQL_COL_AMOUNT_TO_PAY + "]," +
			"[" + SQL_COL_ALLOCATION_FLAG + "]," +
			"[" + SQL_COL_INVOICE_NUMBER + "]," +
			"[" + SQL_COL_DESCRIPTION + "]," +
			"[" + SQL_COL_MEMO + "]" +
			") VALUES ('?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?');";

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
			"[" + SQL_COL_AMOUNT + "]=?," +
			"[" + SQL_COL_ALLOWANCE + "]=?," +
			"[" + SQL_COL_DISCOUNT + "]=?," +
			"[" + SQL_COL_DISCOUNT_AMOUNT + "]=?," +
			"[" + SQL_COL_AMOUNT_PAID + "]=?," +
			"[" + SQL_COL_AMOUNT_TO_PAY + "]=?," +
			"[" + SQL_COL_ALLOCATION_FLAG + "]=?," +
			"[" + SQL_COL_INVOICE_NUMBER + "]='?'," +
			"[" + SQL_COL_DESCRIPTION + "]='?'," +
			"[" + SQL_COL_MEMO + "]='?'" +
			" WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";

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
	protected bool m_isChanged;

	public InvoicesBatch() : base(TABLE, SQL_COL_INVOICE_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

	public InvoicesBatch(DateTime Date,DateTime PayDueDate,DateTime DateLastPaid,DateTime CheckDate,short CheckGroup,short PayFlag,int VendorNumber,int Batch,int CheckNumber,int Company,int Department,int InvoiceKey,decimal Amount,decimal Allowance,decimal Discount,decimal DiscountAmount,decimal AmountPaid,decimal AmountToPay,bool AllocationFlag,string InvoiceNumber,string Description,string Memo) : 
			base(TABLE, SQL_COL_INVOICE_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
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

	protected InvoicesBatch fill(DataRow dr)
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
		} catch (SqlException ex) {
		}
		return this;

	}

	public static InvoicesBatch load( int m_InvoiceKey)
	{
		InvoicesBatch obj  = new InvoicesBatch();
		string[] opt = {m_InvoiceKey.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static InvoicesBatch[] load()
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		InvoicesBatch obj = new InvoicesBatch();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new InvoicesBatch();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try {
			return (InvoicesBatch[])list.ToArray(typeof(InvoicesBatch));
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
		if (m_InvoiceKey > 0 ) {
			string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(), m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(), m_Department.ToString(), m_Amount.ToString(), m_Allowance.ToString(), m_Discount.ToString(), m_DiscountAmount.ToString(), m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(), m_InvoiceNumber, m_Description, m_Memo, m_InvoiceKey.ToString()};
			if (this.updateRecord(m_InvoiceKey.ToString(), values)) {
				return true;
			} else {
				return false;
			}
		} else {
			string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(), m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(), m_Department.ToString(), m_Amount.ToString(), m_Allowance.ToString(), m_Discount.ToString(), m_DiscountAmount.ToString(), m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(), m_InvoiceNumber, m_Description, m_Memo};
			m_InvoiceKey = this.insertRecord(values);
			if (m_InvoiceKey >= 0) {
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
		get { return m_InvoiceKey; }
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

}
}
