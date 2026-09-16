using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL
{
    public class Control : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Control";

	    public const string SQL_COL_BATCH_DATE = "BatchDate";
	    public const string SQL_COL_STMT_DATE = "StmtDate";
	    public const string SQL_COL_CLEAR_DATE = "ClearDate";
	    public const string SQL_COL_CHECK_VOUCHER = "CheckVoucher";
	    public const string SQL_COL_CHECK_CHANGE = "CheckChange";
	    public const string SQL_COL_G_L_METHOD = "GLMethod";
	    public const string SQL_COL_CURRENT_FISCAL_YEAR = "CurrentFiscalYear";
	    public const string SQL_COL_G_L_PROFIT_LOSS_ACCT = "GLProfitLossAcct";
	    public const string SQL_COL_RECUR_STAGING = "RecurStaging";
	    public const string SQL_COL_RECUR_INVOICE = "RecurInvoice";
	    public const string SQL_COL_G_L_BATCH_NUMBER = "GLBatchNumber";
	    public const string SQL_COL_GL_SOURCE_NUMBER = "GlSourceNumber";
	    public const string SQL_COL_G_L_CASH_ACCOUNT = "GLCashAccount";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_BATCH_ON = "BatchOn"; // Re-purposed as Strict accounting compliance 02/07/2017
	    public const string SQL_COL_CMP_DPT_MATCH = "ImmediateEftPost";
	    public const string SQL_COL_G_L_BATCH_DESC = "GLBatchDesc";
	    public const string SQL_COL_G_L_A_R_JOURNAL = "GLARJournal";
	    public const string SQL_COL_ED_LIST_SORT1_FLD = "EdListSort1_fld";
	    public const string SQL_COL_ED_LIST_SORT1_DIR = "EdListSort1_dir";
	    public const string SQL_COL_ED_LIST_SORT2_FLD = "EdListSort2_fld";
	    public const string SQL_COL_ED_LIST_SORT2_DIR = "EdListSort2_dir";
	    public const string SQL_COL_ED_LIST_SORT6_FLD = "EdListSort6_fld";
	    public const string SQL_COL_ED_LIST_SORT6_DIR = "EdListSort6_dir";
	    public const string SQL_COL_MEMO = "Memo";
	    public const string SQL_COL_ED_LIST_SORT3_FLD = "EdListSort3_fld";
	    public const string SQL_COL_ED_LIST_SORT3_DIR = "EdListSort3_dir";
	    public const string SQL_COL_ED_LIST_SORT4_FLD = "EdListSort4_fld";
	    public const string SQL_COL_ED_LIST_SORT4_DIR = "EdListSort4_dir";
	    public const string SQL_COL_ED_LIST_SORT5_FLD = "EdListSort5_fld";
	    public const string SQL_COL_ED_LIST_SORT5_DIR = "EdListSort5_dir";
        public const string SQL_COL_SCAN = "Scan";
        public const string SQL_COL_WINDOC_LOCATION = "Windoc_Location";
             
        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_BATCH_DATE + "]," +
                "[" + SQL_COL_STMT_DATE + "]," +
                "[" + SQL_COL_CLEAR_DATE + "]," +
                "[" + SQL_COL_CHECK_VOUCHER + "]," +
                "[" + SQL_COL_CHECK_CHANGE + "]," +
                "[" + SQL_COL_G_L_METHOD + "]," +
                "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                "[" + SQL_COL_G_L_PROFIT_LOSS_ACCT + "]," +
                "[" + SQL_COL_RECUR_STAGING + "]," +
                "[" + SQL_COL_RECUR_INVOICE + "]," +
                "[" + SQL_COL_G_L_BATCH_NUMBER + "]," +
                "[" + SQL_COL_GL_SOURCE_NUMBER + "]," +
                "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_BATCH_ON + "]," +
                "[" + SQL_COL_CMP_DPT_MATCH + "]," +
                "[" + SQL_COL_G_L_BATCH_DESC + "]," +
                "[" + SQL_COL_G_L_A_R_JOURNAL + "]," +
                "[" + SQL_COL_ED_LIST_SORT1_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT1_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT2_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT2_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT6_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT6_DIR + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_ED_LIST_SORT3_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT3_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT4_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT4_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_DIR + "]," +
                "[" + SQL_COL_SCAN + "]," +
                "[" + SQL_COL_WINDOC_LOCATION + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        static string SQL_LOAD_ONE = "SELECT " +
                "[" + SQL_COL_BATCH_DATE + "]," +
                "[" + SQL_COL_STMT_DATE + "]," +
                "[" + SQL_COL_CLEAR_DATE + "]," +
                "[" + SQL_COL_CHECK_VOUCHER + "]," +
                "[" + SQL_COL_CHECK_CHANGE + "]," +
                "[" + SQL_COL_G_L_METHOD + "]," +
                "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
                "[" + SQL_COL_G_L_PROFIT_LOSS_ACCT + "]," +
                "[" + SQL_COL_RECUR_STAGING + "]," +
                "[" + SQL_COL_RECUR_INVOICE + "]," +
                "[" + SQL_COL_G_L_BATCH_NUMBER + "]," +
                "[" + SQL_COL_GL_SOURCE_NUMBER + "]," +
                "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_BATCH_ON + "]," +
                "[" + SQL_COL_CMP_DPT_MATCH + "]," +
                "[" + SQL_COL_G_L_BATCH_DESC + "]," +
                "[" + SQL_COL_G_L_A_R_JOURNAL + "]," +
                "[" + SQL_COL_ED_LIST_SORT1_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT1_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT2_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT2_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT6_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT6_DIR + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_ED_LIST_SORT3_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT3_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT4_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT4_DIR + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_DIR + "]," +
                "[" + SQL_COL_SCAN + "]," +
                "[" + SQL_COL_WINDOC_LOCATION + "]" +
                " FROM " + TABLE;

        static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_BATCH_DATE + "]," +
			    "[" + SQL_COL_STMT_DATE + "]," +
			    "[" + SQL_COL_CLEAR_DATE + "]," +
			    "[" + SQL_COL_CHECK_VOUCHER + "]," +
			    "[" + SQL_COL_CHECK_CHANGE + "]," +
			    "[" + SQL_COL_G_L_METHOD + "]," +
			    "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
			    "[" + SQL_COL_G_L_PROFIT_LOSS_ACCT + "]," +
			    "[" + SQL_COL_RECUR_STAGING + "]," +
			    "[" + SQL_COL_RECUR_INVOICE + "]," +
			    "[" + SQL_COL_G_L_BATCH_NUMBER + "]," +
			    "[" + SQL_COL_GL_SOURCE_NUMBER + "]," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
			    "[" + SQL_COL_ID + "]," +
			    "[" + SQL_COL_BATCH_ON + "]," +
			    "[" + SQL_COL_CMP_DPT_MATCH + "]," +
			    "[" + SQL_COL_G_L_BATCH_DESC + "]," +
			    "[" + SQL_COL_G_L_A_R_JOURNAL + "]," +
			    "[" + SQL_COL_ED_LIST_SORT1_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT1_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT2_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT2_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT6_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT6_DIR + "]," +
			    "[" + SQL_COL_MEMO + "]," +
			    "[" + SQL_COL_ED_LIST_SORT3_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT3_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT4_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT4_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT5_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_DIR + "]," +
                "[" + SQL_COL_SCAN + "]," +
                "[" + SQL_COL_WINDOC_LOCATION + "]" +
                " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_BATCH_DATE + "]," +
			    "[" + SQL_COL_STMT_DATE + "]," +
			    "[" + SQL_COL_CLEAR_DATE + "]," +
			    "[" + SQL_COL_CHECK_VOUCHER + "]," +
			    "[" + SQL_COL_CHECK_CHANGE + "]," +
			    "[" + SQL_COL_G_L_METHOD + "]," +
			    "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]," +
			    "[" + SQL_COL_G_L_PROFIT_LOSS_ACCT + "]," +
			    "[" + SQL_COL_RECUR_STAGING + "]," +
			    "[" + SQL_COL_RECUR_INVOICE + "]," +
			    "[" + SQL_COL_G_L_BATCH_NUMBER + "]," +
			    "[" + SQL_COL_GL_SOURCE_NUMBER + "]," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," +
			    "[" + SQL_COL_BATCH_ON + "]," +
			    "[" + SQL_COL_CMP_DPT_MATCH + "]," +
			    "[" + SQL_COL_G_L_BATCH_DESC + "]," +
			    "[" + SQL_COL_G_L_A_R_JOURNAL + "]," +
			    "[" + SQL_COL_ED_LIST_SORT1_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT1_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT2_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT2_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT6_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT6_DIR + "]," +
			    "[" + SQL_COL_MEMO + "]," +
			    "[" + SQL_COL_ED_LIST_SORT3_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT3_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT4_FLD + "]," +
			    "[" + SQL_COL_ED_LIST_SORT4_DIR + "]," +
			    "[" + SQL_COL_ED_LIST_SORT5_FLD + "]," +
                "[" + SQL_COL_ED_LIST_SORT5_DIR + "]," +
                "[" + SQL_COL_SCAN + "]," +
                "[" + SQL_COL_WINDOC_LOCATION + "]" +
                ") VALUES ('?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_BATCH_DATE + "]='?'," +
			    "[" + SQL_COL_STMT_DATE + "]='?'," +
			    "[" + SQL_COL_CLEAR_DATE + "]='?'," +
			    "[" + SQL_COL_CHECK_VOUCHER + "]=?," +
			    "[" + SQL_COL_CHECK_CHANGE + "]=?," +
			    "[" + SQL_COL_G_L_METHOD + "]=?," +
			    "[" + SQL_COL_CURRENT_FISCAL_YEAR + "]=?," +
			    "[" + SQL_COL_G_L_PROFIT_LOSS_ACCT + "]=?," +
			    "[" + SQL_COL_RECUR_STAGING + "]=?," +
			    "[" + SQL_COL_RECUR_INVOICE + "]=?," +
			    "[" + SQL_COL_G_L_BATCH_NUMBER + "]=?," +
			    "[" + SQL_COL_GL_SOURCE_NUMBER + "]=?," +
			    "[" + SQL_COL_G_L_CASH_ACCOUNT + "]=?," +
			    "[" + SQL_COL_BATCH_ON + "]=?," +
			    "[" + SQL_COL_CMP_DPT_MATCH + "]=?," +
			    "[" + SQL_COL_G_L_BATCH_DESC + "]='?'," +
			    "[" + SQL_COL_G_L_A_R_JOURNAL + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT1_FLD + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT1_DIR + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT2_FLD + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT2_DIR + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT6_FLD + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT6_DIR + "]='?'," +
			    "[" + SQL_COL_MEMO + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT3_FLD + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT3_DIR + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT4_FLD + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT4_DIR + "]='?'," +
			    "[" + SQL_COL_ED_LIST_SORT5_FLD + "]='?'," +
                "[" + SQL_COL_ED_LIST_SORT5_DIR + "]='?'," +
                "[" + SQL_COL_SCAN + "]=?," +
                "[" + SQL_COL_WINDOC_LOCATION + "]='?'" +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

	    protected DateTime m_BatchDate;
	    protected DateTime m_StmtDate;
	    protected DateTime m_ClearDate;
	    protected short m_CheckVoucher;
	    protected short m_CheckChange;
	    protected short m_GLMethod;
	    protected short m_CurrentFiscalYear;
	    protected short m_GLProfitLossAcct;
	    protected short m_RecurStaging;
	    protected short m_RecurInvoice;
	    protected int m_GLBatchNumber;
	    protected int m_GlSourceNumber;
	    protected int m_GLCashAccount;
	    protected int m_Id;
	    protected bool m_Strict;
	    protected bool m_CmpDptMatch;
	    protected string m_GLBatchDesc;
	    protected string m_GLARJournal;
	    protected string m_EdListSort1_fld;
	    protected string m_EdListSort1_dir;
	    protected string m_EdListSort2_fld;
	    protected string m_EdListSort2_dir;
	    protected string m_EdListSort6_fld;
	    protected string m_EdListSort6_dir;
	    protected string m_Memo;
	    protected string m_EdListSort3_fld;
	    protected string m_EdListSort3_dir;
	    protected string m_EdListSort4_fld;
	    protected string m_EdListSort4_dir;
	    protected string m_EdListSort5_fld;
	    protected string m_EdListSort5_dir;
        protected bool m_Scan;
        protected string m_WindocLocation;
	    protected bool m_isChanged;

	    public Control() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Control(DateTime BatchDate,DateTime StmtDate,DateTime ClearDate,short CheckVoucher,short CheckChange,short GLMethod,short CurrentFiscalYear,short GLProfitLossAcct,short RecurStaging,short RecurInvoice,int GLBatchNumber,int GlSourceNumber,int GLCashAccount,int Id,bool Strict,bool ImmediateEftPost,string GLBatchDesc,string GLARJournal,string EdListSort1_fld,string EdListSort1_dir,string EdListSort2_fld,string EdListSort2_dir,string EdListSort6_fld,string EdListSort6_dir,string Memo,string EdListSort3_fld,string EdListSort3_dir,string EdListSort4_fld,string EdListSort4_dir,string EdListSort5_fld,string EdListSort5_dir, bool scan, string windocLocation) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_BatchDate = BatchDate;
		    m_StmtDate = StmtDate;
		    m_ClearDate = ClearDate;
		    m_CheckVoucher = CheckVoucher;
		    m_CheckChange = CheckChange;
		    m_GLMethod = GLMethod;
		    m_CurrentFiscalYear = CurrentFiscalYear;
		    m_GLProfitLossAcct = GLProfitLossAcct;
		    m_RecurStaging = RecurStaging;
		    m_RecurInvoice = RecurInvoice;
		    m_GLBatchNumber = GLBatchNumber;
		    m_GlSourceNumber = GlSourceNumber;
		    m_GLCashAccount = GLCashAccount;
		    m_Id = Id;
		    m_Strict = Strict;
		    m_CmpDptMatch = ImmediateEftPost;
		    m_GLBatchDesc = GLBatchDesc;
		    m_GLARJournal = GLARJournal;
		    m_EdListSort1_fld = EdListSort1_fld;
		    m_EdListSort1_dir = EdListSort1_dir;
		    m_EdListSort2_fld = EdListSort2_fld;
		    m_EdListSort2_dir = EdListSort2_dir;
		    m_EdListSort6_fld = EdListSort6_fld;
		    m_EdListSort6_dir = EdListSort6_dir;
		    m_Memo = Memo;
		    m_EdListSort3_fld = EdListSort3_fld;
		    m_EdListSort3_dir = EdListSort3_dir;
		    m_EdListSort4_fld = EdListSort4_fld;
		    m_EdListSort4_dir = EdListSort4_dir;
		    m_EdListSort5_fld = EdListSort5_fld;
		    m_EdListSort5_dir = EdListSort5_dir;
            m_Scan = scan;
            m_WindocLocation = windocLocation;
	    }

	    protected Control fill(DataRow dr)
	    {
		    try {
			    m_BatchDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_BATCH_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_BATCH_DATE]);
			    m_StmtDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_STMT_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_STMT_DATE]);
			    m_ClearDate = (DateTime) (Convert.IsDBNull(dr[SQL_COL_CLEAR_DATE]) ? new DateTime(1900,1,1,0,0,0) : dr[SQL_COL_CLEAR_DATE]);
			    m_CheckVoucher = (short) dr[SQL_COL_CHECK_VOUCHER];
			    m_CheckChange = (short) dr[SQL_COL_CHECK_CHANGE];
			    m_GLMethod = (short) dr[SQL_COL_G_L_METHOD];
			    m_CurrentFiscalYear = (short) dr[SQL_COL_CURRENT_FISCAL_YEAR];
			    m_GLProfitLossAcct = (short) dr[SQL_COL_G_L_PROFIT_LOSS_ACCT];
			    m_RecurStaging = (short) dr[SQL_COL_RECUR_STAGING];
			    m_RecurInvoice = (short) dr[SQL_COL_RECUR_INVOICE];
			    m_GLBatchNumber = (int) dr[SQL_COL_G_L_BATCH_NUMBER];
			    m_GlSourceNumber = (int) dr[SQL_COL_GL_SOURCE_NUMBER];
			    m_GLCashAccount = (int) dr[SQL_COL_G_L_CASH_ACCOUNT];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_Strict = (bool) dr[SQL_COL_BATCH_ON];
			    m_CmpDptMatch = (bool) dr[SQL_COL_CMP_DPT_MATCH];
			    m_GLBatchDesc = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_BATCH_DESC]) ? "" : dr[SQL_COL_G_L_BATCH_DESC]);
			    m_GLARJournal = (string) (Convert.IsDBNull(dr[SQL_COL_G_L_A_R_JOURNAL]) ? "" : dr[SQL_COL_G_L_A_R_JOURNAL]);
			    m_EdListSort1_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT1_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT1_FLD]);
			    m_EdListSort1_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT1_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT1_DIR]);
			    m_EdListSort2_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT2_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT2_FLD]);
			    m_EdListSort2_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT2_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT2_DIR]);
			    m_EdListSort6_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT6_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT6_FLD]);
			    m_EdListSort6_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT6_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT6_DIR]);
			    m_Memo = (string) (Convert.IsDBNull(dr[SQL_COL_MEMO]) ? "" : dr[SQL_COL_MEMO]);
			    m_EdListSort3_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT3_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT3_FLD]);
			    m_EdListSort3_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT3_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT3_DIR]);
			    m_EdListSort4_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT4_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT4_FLD]);
			    m_EdListSort4_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT4_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT4_DIR]);
			    m_EdListSort5_fld = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT5_FLD]) ? "" : dr[SQL_COL_ED_LIST_SORT5_FLD]);
			    m_EdListSort5_dir = (string) (Convert.IsDBNull(dr[SQL_COL_ED_LIST_SORT5_DIR]) ? "" : dr[SQL_COL_ED_LIST_SORT5_DIR]);
                m_Scan = (bool) dr[SQL_COL_SCAN];
                m_WindocLocation = (string)(dr[SQL_COL_WINDOC_LOCATION] is DBNull ? "" : dr[SQL_COL_WINDOC_LOCATION]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Control load( int m_Id)
	    {
		    Control obj  = new Control();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

        public static Control loadOne()
        {
            Control obj = new Control();
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_ONE, null);
            if (ds != null)
            {
				try
				{
					DataRow dr = ds.Tables[0].Rows[0];
					return obj.fill(dr);

				}
				catch { return null; }
            }
            else
                return null;
        }
	    public static Control[] load(bool debug = false)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Control obj = new Control();

		    ds = obj.loadAll(debug);
		    if (ds == null && ds.Tables[0].Rows.Count == 0)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Control();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Control[])list.ToArray(typeof(Control));
		    } catch (SqlException ex) {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Control: " + ex.Message, false);
                return null;
		    }

	    }

	    		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

	    {
		    if (m_Id > 0 ) {
			    string[] values = {m_BatchDate.ToString(), m_StmtDate.ToString(), m_ClearDate.ToString(), m_CheckVoucher.ToString(), m_CheckChange.ToString(), m_GLMethod.ToString(), m_CurrentFiscalYear.ToString(), m_GLProfitLossAcct.ToString(), m_RecurStaging.ToString(), m_RecurInvoice.ToString(), m_GLBatchNumber.ToString(), m_GlSourceNumber.ToString(), m_GLCashAccount.ToString(), m_Strict.ToString(), m_CmpDptMatch.ToString(), m_GLBatchDesc, m_GLARJournal, m_EdListSort1_fld, m_EdListSort1_dir, m_EdListSort2_fld, m_EdListSort2_dir, m_EdListSort6_fld, m_EdListSort6_dir, m_Memo, m_EdListSort3_fld, m_EdListSort3_dir, m_EdListSort4_fld, m_EdListSort4_dir, m_EdListSort5_fld, m_EdListSort5_dir, m_Scan.ToString(), m_WindocLocation, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = { m_BatchDate.ToString(), m_StmtDate.ToString(), m_ClearDate.ToString(), m_CheckVoucher.ToString(), m_CheckChange.ToString(), m_GLMethod.ToString(), m_CurrentFiscalYear.ToString(), m_GLProfitLossAcct.ToString(), m_RecurStaging.ToString(), m_RecurInvoice.ToString(), m_GLBatchNumber.ToString(), m_GlSourceNumber.ToString(), m_GLCashAccount.ToString(), m_Strict.ToString(), m_CmpDptMatch.ToString(), m_GLBatchDesc, m_GLARJournal, m_EdListSort1_fld, m_EdListSort1_dir, m_EdListSort2_fld, m_EdListSort2_dir, m_EdListSort6_fld, m_EdListSort6_dir, m_Memo, m_EdListSort3_fld, m_EdListSort3_dir, m_EdListSort4_fld, m_EdListSort4_dir, m_EdListSort5_fld, m_EdListSort5_dir, m_Scan.ToString(), m_WindocLocation };
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

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        // Properties
        public DateTime BatchDate {
		    set{ m_BatchDate = value;
		    OnPropertyChanged("BatchDate"); }
		    get { return m_BatchDate; }
	    }

	    public DateTime StmtDate {
		    set{ m_StmtDate = value;
		    OnPropertyChanged("StmtDate"); }
		    get { return m_StmtDate; }
	    }

	    public DateTime ClearDate {
		    set{ m_ClearDate = value;
		    OnPropertyChanged("ClearDate"); }
		    get { return m_ClearDate; }
	    }

	    public short CheckVoucher {
		    set{ m_CheckVoucher = value;
		    OnPropertyChanged("CheckVoucher"); }
		    get { return m_CheckVoucher; }
	    }

	    public short CheckChange {
		    set{ m_CheckChange = value;
		    OnPropertyChanged("CheckChange"); }
		    get { return m_CheckChange; }
	    }

	    public short GLMethod {
		    set{ m_GLMethod = value;
		    OnPropertyChanged("GLMethod"); }
		    get { return m_GLMethod; }
	    }

	    public short CurrentFiscalYear {
		    set{ m_CurrentFiscalYear = value;
		    OnPropertyChanged("CurrentFiscalYear"); }
		    get { return m_CurrentFiscalYear; }
	    }

	    public short GLProfitLossAcct {
		    set{ m_GLProfitLossAcct = value;
		    OnPropertyChanged("GLProfitLossAcct"); }
		    get { return m_GLProfitLossAcct; }
	    }

	    public short RecurStaging {
		    set{ m_RecurStaging = value;
		    OnPropertyChanged("RecurStaging"); }
		    get { return m_RecurStaging; }
	    }

	    public short RecurInvoice {
		    set{ m_RecurInvoice = value;
		    OnPropertyChanged("RecurInvoice"); }
		    get { return m_RecurInvoice; }
	    }

	    public int GLBatchNumber {
		    set{ m_GLBatchNumber = value;
		    OnPropertyChanged("GLBatchNumber"); }
		    get { return m_GLBatchNumber; }
	    }

	    public int GlSourceNumber {
		    set{ m_GlSourceNumber = value;
		    OnPropertyChanged("GlSourceNumber"); }
		    get { return m_GlSourceNumber; }
	    }

	    public int GLCashAccount {
		    set{ m_GLCashAccount = value;
		    OnPropertyChanged("GLCashAccount"); }
		    get { return m_GLCashAccount; }
	    }

	    public int Id {
		    get { return m_Id; }
	    }

	    public bool Strict {
		    set{ m_Strict = value;
		    OnPropertyChanged("Strict"); }
		    get { return m_Strict; }
	    }

	    public bool ImmediateEftPost {
		    set{ m_CmpDptMatch = value;
		    OnPropertyChanged("ImmediateEftPost"); }
		    get { return m_CmpDptMatch; }
	    }

	    public string GLBatchDesc {
		    set{ m_GLBatchDesc = value;
		    OnPropertyChanged("GLBatchDesc"); }
		    get { return m_GLBatchDesc; }
	    }

	    public string GLARJournal {
		    set{ m_GLARJournal = value;
		    OnPropertyChanged("GLARJournal"); }
		    get { return m_GLARJournal; }
	    }

	    public string EdListSort1_fld {
		    set{ m_EdListSort1_fld = value;
		    OnPropertyChanged("EdListSort1_fld"); }
		    get { return m_EdListSort1_fld; }
	    }

	    public string EdListSort1_dir {
		    set{ m_EdListSort1_dir = value;
		    OnPropertyChanged("EdListSort1_dir"); }
		    get { return m_EdListSort1_dir; }
	    }

	    public string EdListSort2_fld {
		    set{ m_EdListSort2_fld = value;
		    OnPropertyChanged("EdListSort2_fld"); }
		    get { return m_EdListSort2_fld; }
	    }

	    public string EdListSort2_dir {
		    set{ m_EdListSort2_dir = value;
		    OnPropertyChanged("EdListSort2_dir"); }
		    get { return m_EdListSort2_dir; }
	    }

	    public string EdListSort6_fld {
		    set{ m_EdListSort6_fld = value;
		    OnPropertyChanged("EdListSort6_fld"); }
		    get { return m_EdListSort6_fld; }
	    }

	    public string EdListSort6_dir {
		    set{ m_EdListSort6_dir = value;
		    OnPropertyChanged("EdListSort6_dir"); }
		    get { return m_EdListSort6_dir; }
	    }

	    public string Memo {
		    set{ m_Memo = value;
		    OnPropertyChanged("Memo"); }
		    get { return m_Memo; }
	    }

	    public string EdListSort3_fld {
		    set{ m_EdListSort3_fld = value;
		    OnPropertyChanged("EdListSort3_fld"); }
		    get { return m_EdListSort3_fld; }
	    }

	    public string EdListSort3_dir {
		    set{ m_EdListSort3_dir = value;
		    OnPropertyChanged("EdListSort3_dir"); }
		    get { return m_EdListSort3_dir; }
	    }

	    public string EdListSort4_fld {
		    set{ m_EdListSort4_fld = value;
		    OnPropertyChanged("EdListSort4_fld"); }
		    get { return m_EdListSort4_fld; }
	    }

	    public string EdListSort4_dir {
		    set{ m_EdListSort4_dir = value;
		    OnPropertyChanged("EdListSort4_dir"); }
		    get { return m_EdListSort4_dir; }
	    }

	    public string EdListSort5_fld {
		    set{ m_EdListSort5_fld = value;
		    OnPropertyChanged("EdListSort5_fld"); }
		    get { return m_EdListSort5_fld; }
	    }

	    public string EdListSort5_dir {
		    set{ m_EdListSort5_dir = value;
		    OnPropertyChanged("EdListSort5_dir"); }
		    get { return m_EdListSort5_dir; }
	    }

        public bool Scan
        {
            set { m_Scan = value;
                OnPropertyChanged("Scan");
            }
            get { return m_Scan; }
        }

        public string WindocLocation
        {
            set { m_WindocLocation = value;
                OnPropertyChanged("WindocLocation");
            }
            get { return m_WindocLocation; }
        }
    }
}
