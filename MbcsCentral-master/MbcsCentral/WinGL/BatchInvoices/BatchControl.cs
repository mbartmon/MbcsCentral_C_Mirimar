using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class BatchControl : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_BatchControl";

	    public const string SQL_COL_LAST_DATE = "LastDate";
	    public const string SQL_COL_INVOICE_OPTION = "InvoiceOption";
	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_NAME = "Name";
        public const string SQL_COL_COMMENT = "Comment";
		public const string SQL_COL_PRIVATE = "Private";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_LAST_DATE + "]," +
                "[" + SQL_COL_INVOICE_OPTION + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_PRIVATE + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_LAST_DATE + "]," +
                "[" + SQL_COL_INVOICE_OPTION + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_PRIVATE + "]" +
                 " FROM " + TABLE +
                 " ORDER BY " + SQL_COL_NAME;

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_LAST_DATE + "]," +
			    "[" + SQL_COL_INVOICE_OPTION + "]," +
                "[" + SQL_COL_NAME + "]," +
                "[" + SQL_COL_COMMENT + "]," +
				"[" + SQL_COL_PRIVATE + "]" +
                ") VALUES ('?', ?, '?', '?', ?);";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_LAST_DATE + "]='?'," +
			    "[" + SQL_COL_INVOICE_OPTION + "]=?," +
			    "[" + SQL_COL_NAME + "]='?'," +
                "[" + SQL_COL_COMMENT + "]='?'," +
				"[" + SQL_COL_PRIVATE + "]=?" +
			    " WHERE " + "[" + SQL_COL_KEY + "] = ?;";

	    protected DateTime m_LastDate;
	    protected short m_InvoiceOption;
	    protected int m_Key;
	    protected string m_Name;
        protected string m_optionName;
        protected string m_comment;
		protected bool m_private;
	    protected bool m_isChanged;

		protected bool usePrivate = false;

        BatchInvoiceOptions[] options = BatchInvoiceOptions.Options();
	    public BatchControl() : base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public BatchControl(DateTime LastDate,short InvoiceOption,int Key,string Name) : 
			    base(TABLE, SQL_COL_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_LastDate = LastDate;
		    m_InvoiceOption = InvoiceOption;
		    m_Key = Key;
		    m_Name = Name;
	    }

	        protected BatchControl fill(DataRow dr)
	        {
		        try {
			        m_LastDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_LAST_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_LAST_DATE]);
			        m_InvoiceOption = (short) dr[SQL_COL_INVOICE_OPTION];
			        m_Key = (int) dr[SQL_COL_KEY];
			        m_Name = (string) (Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
                    m_comment = (string)(Convert.IsDBNull(dr[SQL_COL_COMMENT]) ? "" : dr[SQL_COL_COMMENT]);
					m_private = (dr[SQL_COL_PRIVATE] is DBNull ? false : (bool)dr[SQL_COL_PRIVATE]);
                    if (m_InvoiceOption >= 0)
                    {
                        m_optionName = options[m_InvoiceOption].OptionName;
                        Invoices[] invoices = Invoices.loadForBatch(m_Key, InvoicesBatch.TABLE);
                        NumberOfInvoices = invoices == null ? 0 : invoices.Length;
                    }
		        } catch (SqlException ex) {
		        }
		        return this;

	        }

	    public static BatchControl load( int m_Key, bool Private = false)
	    {
				
		    BatchControl obj  = new BatchControl();
		    string[] opt = {m_Key.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static BatchControl[] load(bool usePrivate = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    BatchControl obj = new BatchControl();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) 
			{
				if (!usePrivate && (bool)dr[SQL_COL_PRIVATE])
					continue;
			    obj = new BatchControl();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (BatchControl[])list.ToArray(typeof(BatchControl));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

	    public bool save()
	    {
		    if (m_Key > 0 ) {
			    string[] values = {m_LastDate.ToString(), m_InvoiceOption.ToString(), m_Name, m_comment, m_private.ToString(), m_Key.ToString()};
			    if (this.updateRecord(m_Key.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_LastDate.ToString(), m_InvoiceOption.ToString(), m_Name, m_comment, m_private.ToString()};
			    m_Key = this.insertRecord(values);
			    if (m_Key >= 0) {
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
	    public DateTime LastDate {
		    set{ m_LastDate = value;
		    OnPropertyChanged("LastDate"); }
		    get { return m_LastDate; }
	    }

	    public short InvoiceOption {
		    set{ m_InvoiceOption = value;
		    OnPropertyChanged("InvoiceOption"); }
		    get { return m_InvoiceOption; }
	    }

	    public int Key {
		    get { return m_Key; }
	    }

	    public string BatchName {
		    set{ m_Name = value;
		    OnPropertyChanged("Name"); }
		    get { return m_Name; }
	    }

        public string OptionName
        {
            get { return m_optionName; }
            set
                {
                    m_optionName = value;
                    OnPropertyChanged("OptionName");
                }
        }

        public string Comment
        {
            get { return m_comment; }
            set
            {
                m_comment = value;
                OnPropertyChanged("Comment");
            }
        }

		public bool Private
		{
			get { return m_private; }
			set { 
					m_private = value;
				OnPropertyChanged("Private");
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

            public bool Deleted { get; set; }

            private bool selected = false;
            public bool Selected
            {
                get { return selected; }
                set
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                }
            }
            public int NumberOfInvoices { get; set; }
    }
}
