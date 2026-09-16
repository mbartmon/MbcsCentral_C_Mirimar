using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using MbcsCentral.WinPM;

namespace MbcsCentral.WinDoc {
    public class Documents : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "do_Documents";

	    public const string SQL_COL_PAGE_NUMBER = "PageNumber";
	    public const string SQL_COL_GROUP_ID1 = "GroupId1";
	    public const string SQL_COL_GROUP_ID2 = "GroupId2";
	    public const string SQL_COL_GROUP_ID3 = "GroupId3";
	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_DATE2 = "Date2";
	    public const string SQL_COL_CREATED = "Created";
	    public const string SQL_COL_DOCUMENT_ID = "DocumentId";
	    public const string SQL_COL_TENANT_SEQ = "TenantSeq";
	    public const string SQL_COL_ORIG_DOC_ID = "OrigDocId";
	    public const string SQL_COL_FILENAME = "Filename";
	    public const string SQL_COL_DESCRIPTION = "Description";
	    public const string SQL_COL_BUILDING = "Building";
	    public const string SQL_COL_TENANT = "Tenant";
	    public const string SQL_COL_VENDOR = "Vendor";
	    public const string SQL_COL_INVOICE = "Invoice";

	    static string SQL_LOAD = "SELECT " +
			    "[" + SQL_COL_PAGE_NUMBER + "]," +
			    "[" + SQL_COL_GROUP_ID1 + "]," +
			    "[" + SQL_COL_GROUP_ID2 + "]," +
			    "[" + SQL_COL_GROUP_ID3 + "]," +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_DATE2 + "]," +
			    "[" + SQL_COL_CREATED + "]," +
			    "[" + SQL_COL_DOCUMENT_ID + "]," +
			    "[" + SQL_COL_TENANT_SEQ + "]," +
			    "[" + SQL_COL_ORIG_DOC_ID + "]," +
			    "[" + SQL_COL_FILENAME + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_BUILDING + "]," +
			    "[" + SQL_COL_TENANT + "]," +
			    "[" + SQL_COL_VENDOR + "]," +
			    "[" + SQL_COL_INVOICE + "]" +
			    " FROM " + TABLE +
			    " WHERE " + "[" + SQL_COL_DOCUMENT_ID + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_PAGE_NUMBER + "]," +
			    "[" + SQL_COL_GROUP_ID1 + "]," +
			    "[" + SQL_COL_GROUP_ID2 + "]," +
			    "[" + SQL_COL_GROUP_ID3 + "]," +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_DATE2 + "]," +
			    "[" + SQL_COL_CREATED + "]," +
			    "[" + SQL_COL_DOCUMENT_ID + "]," +
			    "[" + SQL_COL_TENANT_SEQ + "]," +
			    "[" + SQL_COL_ORIG_DOC_ID + "]," +
			    "[" + SQL_COL_FILENAME + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_BUILDING + "]," +
			    "[" + SQL_COL_TENANT + "]," +
			    "[" + SQL_COL_VENDOR + "]," +
			    "[" + SQL_COL_INVOICE + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_PAGE_NUMBER + "]," +
			    "[" + SQL_COL_GROUP_ID1 + "]," +
			    "[" + SQL_COL_GROUP_ID2 + "]," +
			    "[" + SQL_COL_GROUP_ID3 + "]," +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_DATE2 + "]," +
			    "[" + SQL_COL_CREATED + "]," +
			    "[" + SQL_COL_TENANT_SEQ + "]," +
			    "[" + SQL_COL_ORIG_DOC_ID + "]," +
			    "[" + SQL_COL_FILENAME + "]," +
			    "[" + SQL_COL_DESCRIPTION + "]," +
			    "[" + SQL_COL_BUILDING + "]," +
			    "[" + SQL_COL_TENANT + "]," +
			    "[" + SQL_COL_VENDOR + "]," +
			    "[" + SQL_COL_INVOICE + "]" +
			    ") VALUES (?, ?, ?, ?, '?', '?', '?', ?, ?, '?', '?', '?', '?', '?', '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_PAGE_NUMBER + "]=?," +
			    "[" + SQL_COL_GROUP_ID1 + "]=?," +
			    "[" + SQL_COL_GROUP_ID2 + "]=?," +
			    "[" + SQL_COL_GROUP_ID3 + "]=?," +
			    "[" + SQL_COL_DATE + "]='?'," +
			    "[" + SQL_COL_DATE2 + "]='?'," +
			    "[" + SQL_COL_CREATED + "]='?'," +
			    "[" + SQL_COL_TENANT_SEQ + "]=?," +
			    "[" + SQL_COL_ORIG_DOC_ID + "]=?," +
			    "[" + SQL_COL_FILENAME + "]='?'," +
			    "[" + SQL_COL_DESCRIPTION + "]='?'," +
			    "[" + SQL_COL_BUILDING + "]='?'," +
			    "[" + SQL_COL_TENANT + "]='?'," +
			    "[" + SQL_COL_VENDOR + "]='?'," +
			    "[" + SQL_COL_INVOICE + "]='?'" +
			    " WHERE " + "[" + SQL_COL_DOCUMENT_ID + "] = ?;";

	    protected int m_PageNumber;
	    protected int m_GroupId1;
	    protected int m_GroupId2;
	    protected int m_GroupId3;
	    protected DateTime m_Date;
	    protected DateTime m_Date2;
	    protected DateTime m_Created;
	    protected long m_DocumentId;
	    protected long m_TenantSeq;
	    protected long m_OrigDocId;
	    protected string m_Filename;
	    protected string m_Description;
	    protected string m_Building;
	    protected string m_Tenant;
	    protected string m_Vendor;
	    protected string m_Invoice;
	    protected bool m_isChanged;

        private WinPM.SubDocument[] subDocuments = null;
	    public Documents() : base(TABLE, SQL_COL_DOCUMENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Documents(int PageNumber,int GroupId1,int GroupId2,int GroupId3,DateTime Date,DateTime Date2,DateTime Created,long DocumentId,long TenantSeq,long OrigDocId,string Filename,string Description,string Building,string Tenant,string Vendor,string Invoice) : 
			    base(TABLE, SQL_COL_DOCUMENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_PageNumber = PageNumber;
		    m_GroupId1 = GroupId1;
		    m_GroupId2 = GroupId2;
		    m_GroupId3 = GroupId3;
		    m_Date = Date;
		    m_Date2 = Date2;
		    m_Created = Created;
		    m_DocumentId = DocumentId;
		    m_TenantSeq = TenantSeq;
		    m_OrigDocId = OrigDocId;
		    m_Filename = Filename;
		    m_Description = Description;
		    m_Building = Building;
		    m_Tenant = Tenant;
		    m_Vendor = Vendor;
		    m_Invoice = Invoice;
	    }

        public Documents(DataRow dr) : base(TABLE, SQL_COL_DOCUMENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            this.fill(dr);
        }

	    protected Documents fill(DataRow dr)
	    {
		    try {
			    m_PageNumber = (int) dr[SQL_COL_PAGE_NUMBER];
			    m_GroupId1 = (int) dr[SQL_COL_GROUP_ID1];
			    m_GroupId2 = (int) dr[SQL_COL_GROUP_ID2];
			    m_GroupId3 = (int) dr[SQL_COL_GROUP_ID3];
			    m_Date = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE]);
			    m_Date2 = (DateTime) (Convert.IsDBNull(dr[SQL_COL_DATE2]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_DATE2]);
			    m_Created = (DateTime) (Convert.IsDBNull(dr[SQL_COL_CREATED]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_CREATED]);
			    m_DocumentId = (long) dr[SQL_COL_DOCUMENT_ID];
			    m_TenantSeq = (long) dr[SQL_COL_TENANT_SEQ];
			    m_OrigDocId = (long) dr[SQL_COL_ORIG_DOC_ID];
			    m_Filename = (string) (Convert.IsDBNull(dr[SQL_COL_FILENAME]) ? "" : dr[SQL_COL_FILENAME]);
			    m_Description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
			    m_Building = (string) (Convert.IsDBNull(dr[SQL_COL_BUILDING]) ? "" : dr[SQL_COL_BUILDING]);
			    m_Tenant = (string) (Convert.IsDBNull(dr[SQL_COL_TENANT]) ? "" : dr[SQL_COL_TENANT]);
			    m_Vendor = (string) (Convert.IsDBNull(dr[SQL_COL_VENDOR]) ? "" : dr[SQL_COL_VENDOR]);
			    m_Invoice = (string) (Convert.IsDBNull(dr[SQL_COL_INVOICE]) ? "" : dr[SQL_COL_INVOICE]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Documents load( long m_DocumentId)
	    {
		    Documents obj  = new Documents();
		    string[] opt = {m_DocumentId.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Documents[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Documents obj = new Documents();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Documents();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Documents[])list.ToArray(typeof(Documents));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

	    public bool save()
	    {
		    if (m_DocumentId > 0 ) {
			    string[] values = {m_PageNumber.ToString(), m_GroupId1.ToString(), m_GroupId2.ToString(), m_GroupId3.ToString(), m_Date.ToString(), m_Date2.ToString(), m_Created.ToString(), m_TenantSeq.ToString(), m_OrigDocId.ToString(), m_Filename, m_Description, m_Building, m_Tenant, m_Vendor, m_Invoice, m_DocumentId.ToString()};
			    if (this.updateRecord(m_DocumentId.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_PageNumber.ToString(), m_GroupId1.ToString(), m_GroupId2.ToString(), m_GroupId3.ToString(), m_Date.ToString(), m_Date2.ToString(), m_Created.ToString(), m_TenantSeq.ToString(), m_OrigDocId.ToString(), m_Filename, m_Description, m_Building, m_Tenant, m_Vendor, m_Invoice};
			    m_DocumentId = this.insertRecord(values);
			    if (m_DocumentId >= 0) {
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
	    public int PageNumber {
		    set{ m_PageNumber = value;
		    OnPropertyChanged("PageNumber"); }
		    get { return m_PageNumber; }
	    }

	    public int GroupId1 {
		    set{ m_GroupId1 = value;
		    OnPropertyChanged("GroupId1"); }
		    get { return m_GroupId1; }
	    }

	    public int GroupId2 {
		    set{ m_GroupId2 = value;
		    OnPropertyChanged("GroupId2"); }
		    get { return m_GroupId2; }
	    }

	    public int GroupId3 {
		    set{ m_GroupId3 = value;
		    OnPropertyChanged("GroupId3"); }
		    get { return m_GroupId3; }
	    }

	    public DateTime Date {
		    set{ m_Date = value;
		    OnPropertyChanged("Date"); }
		    get { return m_Date; }
	    }

	    public DateTime Date2 {
		    set{ m_Date2 = value;
		    OnPropertyChanged("Date2"); }
		    get { return m_Date2; }
	    }

	    public DateTime Created {
		    set{ m_Created = value;
		    OnPropertyChanged("Created"); }
		    get { return m_Created; }
	    }

	    public long DocumentId {
		    get { return m_DocumentId; }
            set { m_DocumentId = value; }
	    }

	    public long TenantSeq {
		    set{ m_TenantSeq = value;
		    OnPropertyChanged("TenantSeq"); }
		    get { return m_TenantSeq; }
	    }

	    public long OrigDocId {
		    set{ m_OrigDocId = value;
		    OnPropertyChanged("OrigDocId"); }
		    get { return m_OrigDocId; }
	    }

	    public string Filename {
		    set{ m_Filename = value;
		    OnPropertyChanged("Filename"); }
		    get { return m_Filename; }
	    }

	    public string Description {
		    set{ m_Description = value;
		    OnPropertyChanged("Description"); }
		    get { return m_Description; }
	    }

	    public string Building {
		    set{ m_Building = value;
		    OnPropertyChanged("Building"); }
		    get { return m_Building; }
	    }

	    public string Tenant {
		    set{ m_Tenant = value;
		    OnPropertyChanged("Tenant"); }
		    get { return m_Tenant; }
	    }

	    public string Vendor {
		    set{ m_Vendor = value;
		    OnPropertyChanged("Vendor"); }
		    get { return m_Vendor; }
	    }

	    public string Invoice {
		    set{ m_Invoice = value;
		    OnPropertyChanged("Invoice"); }
		    get { return m_Invoice; }
	    }

        public object Parent { get; set; }

        public WinPM.SubDocument[] SubDocuments 
        {
            get { return subDocuments; }
            set { subDocuments = value; }
        }

    }
}
