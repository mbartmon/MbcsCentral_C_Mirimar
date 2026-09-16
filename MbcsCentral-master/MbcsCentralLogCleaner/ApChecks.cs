using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace MbcsCentralLogCleaner {
    public class ApChecks : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_ApChecks";

	    public const string SQL_COL_DATE = "Date";
	    public const string SQL_COL_COMPANY = "Company";
	    public const string SQL_COL_DEPARTMENT = "Department";
	    public const string SQL_COL_SEQUENCE = "Sequence";
	    public const string SQL_COL_DIST_COMPANY = "DistCompany";
	    public const string SQL_COL_DIST_DEPARTMENT = "DistDepartment";
	    public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
	    public const string SQL_COL_VENDOR_ACCOUNT = "Vendor/Account";
	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_AMOUNT = "Amount";
	    public const string SQL_COL_RECON_FLAG = "ReconFlag";
        public const string SQL_COL_JOURNAL = "Journal";
        public const string SQL_COL_CHECK_TOTAL = "CheckTotal";
        public const string SQL_COL_PURCHASES = "PurchaseTotal";
        public const string SQL_COL_YEAR = "DateYear";

        public const string SQL_COL_SEQUENCE_COUNT = "SequenceCount";

        public const string KEY_BY_CHECK = "IX_ApchecksByCheck";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_DIST_COMPANY + "]," +
                "[" + SQL_COL_DIST_DEPARTMENT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_SEQUENCE + "]," +
			    "[" + SQL_COL_DIST_COMPANY + "]," +
			    "[" + SQL_COL_DIST_DEPARTMENT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_DATE + "]," +
			    "[" + SQL_COL_COMPANY + "]," +
			    "[" + SQL_COL_DEPARTMENT + "]," +
			    "[" + SQL_COL_SEQUENCE + "]," +
			    "[" + SQL_COL_DIST_COMPANY + "]," +
			    "[" + SQL_COL_DIST_DEPARTMENT + "]," +
			    "[" + SQL_COL_CHECK_NUMBER + "]," +
			    "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
			    "[" + SQL_COL_AMOUNT + "]," +
			    "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
			    ") VALUES ('?', ?, ?, ?, ?, ?, ?, ?, ?, ?, '?');";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_DATE + "]='?'," +
			    "[" + SQL_COL_COMPANY + "]=?," +
			    "[" + SQL_COL_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_SEQUENCE + "]=?," +
			    "[" + SQL_COL_DIST_COMPANY + "]=?," +
			    "[" + SQL_COL_DIST_DEPARTMENT + "]=?," +
			    "[" + SQL_COL_CHECK_NUMBER + "]=?," +
			    "[" + SQL_COL_VENDOR_ACCOUNT + "]=?," +
			    "[" + SQL_COL_AMOUNT + "]=?," +
			    "[" + SQL_COL_RECON_FLAG + "]=?," +
                "[" + SQL_COL_JOURNAL + "]='?'" +
			    " WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_FOR_REGISTER = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_DIST_COMPANY + "]," +
                "[" + SQL_COL_DIST_DEPARTMENT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_COMPANY + "] = ?" +
                " AND " + "[" + SQL_COL_CHECK_NUMBER + "] BETWEEN ? AND ?" +
                " AND " + "[" + SQL_COL_DATE + "] BETWEEN '?' AND '?'";
        static string SQL_LOAD_FOR_REGISTER_WITH_DEPT = " AND " + "[" + SQL_COL_DEPARTMENT + "] = ?";
        static string SQL_LOAD_FOR_REGISTER_SUFFIX =
                " ORDER BY " + "[" + SQL_COL_DEPARTMENT + "], " +
                "[" + SQL_COL_CHECK_NUMBER + "]," + "[" + SQL_COL_SEQUENCE + "]";

        static string SQL_LOAD_FOR_CHECK_TOTAL_FIX = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
                "[" + SQL_COL_DIST_COMPANY + "]," +
                "[" + SQL_COL_DIST_DEPARTMENT + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_DATE + "] BETWEEN '?' AND '?'" +
                " AND [" + SQL_COL_SEQUENCE + "] = 0";


        static string SQL_LOAD_CHECK_TOTAL = "SELECT sum(" + ApChecks.SQL_COL_AMOUNT + ") as " + SQL_COL_CHECK_TOTAL +
                " FROM " + TABLE + 
                " WHERE [" + SQL_COL_COMPANY + "]= ?" +
                " AND [" + SQL_COL_DEPARTMENT + "] = ?" + 
                " AND [" + SQL_COL_CHECK_NUMBER + "]= ?" +  
                " AND [" + SQL_COL_SEQUENCE + "] <> 0";

        static string SQL_LOAD_ONE = "SELECT " +
        "[" + SQL_COL_DATE + "]," +
        "[" + SQL_COL_COMPANY + "]," +
        "[" + SQL_COL_DEPARTMENT + "]," +
        "[" + SQL_COL_SEQUENCE + "]," +
        "[" + SQL_COL_DIST_COMPANY + "]," +
        "[" + SQL_COL_DIST_DEPARTMENT + "]," +
        "[" + SQL_COL_CHECK_NUMBER + "]," +
        "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
        "[" + SQL_COL_I_D + "]," +
        "[" + SQL_COL_AMOUNT + "]," +
        "[" + SQL_COL_RECON_FLAG + "]," +
        "[" + SQL_COL_JOURNAL + "]" +
        " FROM " + TABLE +
        " WHERE [" + SQL_COL_COMPANY + "] = ?" +
        " AND [" + SQL_COL_DEPARTMENT + "] = ?" +
        " AND [" + SQL_COL_CHECK_NUMBER + "] =?" +
        " ORDER BY " + SQL_COL_SEQUENCE;

        public static string SQL_LOAD_FOR_1099 = "SELECT " +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "SUM ([" + SQL_COL_AMOUNT + "]) AS " + SQL_COL_AMOUNT + 
                " FROM " + TABLE;

        public static string SQL_LOAD_VENDOR_PURCHASES = "SELECT " +
            "DATEPART(YEAR, [" + SQL_COL_DATE + "]) AS [" + SQL_COL_YEAR + "]," +
            " [" + SQL_COL_VENDOR_ACCOUNT + "]," +
            " SUM ([" + SQL_COL_AMOUNT + "]) AS " + SQL_COL_PURCHASES +
            " FROM " + TABLE +
            " WHERE [" + SQL_COL_VENDOR_ACCOUNT + "] = (?)" +
            " AND DATEPART(YEAR, [" + SQL_COL_DATE + "]) IN  (?, ?)" +
            " AND [" + SQL_COL_SEQUENCE + "] = 0" +
            " GROUP BY DATEPART(YEAR, [" + SQL_COL_DATE + "]), [" + SQL_COL_VENDOR_ACCOUNT + "]";

        public static string SQL_LOAD_SEQUENCE_COUNT = "SELECT COUNT(*) AS " + SQL_COL_SEQUENCE_COUNT + ", " + SQL_COL_COMPANY + ", " + SQL_COL_DEPARTMENT + ", " + SQL_COL_CHECK_NUMBER +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_SEQUENCE + " = 0" +
            " GROUP BY " + SQL_COL_COMPANY + ", " + SQL_COL_DEPARTMENT + ", " + SQL_COL_CHECK_NUMBER +
            " ORDER BY " + SQL_COL_SEQUENCE_COUNT + " DESC";

        public static string SQL_LOAD_BY_CPM_DPT_CHECK = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_DIST_COMPANY + "]," +
                "[" + SQL_COL_DIST_DEPARTMENT + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_VENDOR_ACCOUNT + "]," +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_RECON_FLAG + "]," +
                "[" + SQL_COL_JOURNAL + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_COMPANY + "] = ?" +
                " AND " + SQL_COL_DEPARTMENT + " = ?" +
                " AND " + SQL_COL_CHECK_NUMBER + " = ?" +
                " ORDER BY " + SQL_COL_SEQUENCE + " DESC";

        public static string SQL_LOAD_DUPE_SEQ = "SELECT " +
            "[" + SQL_COL_I_D + "]" +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_COMPANY + "=? " +
            " AND " + SQL_COL_DEPARTMENT + "=? " +
            " AND " + SQL_COL_CHECK_NUMBER + "=? " +
            " AND " + SQL_COL_SEQUENCE + "=?";


        protected DateTime m_Date;
	    protected short m_Company;
	    protected short m_Department;
	    protected short m_Sequence;
	    protected short m_DistCompany;
	    protected short m_DistDepartment;
	    protected int m_CheckNumber;
	    protected int m_VendorAccount;
	    protected int m_ID = 0;
	    protected decimal m_Amount;
	    protected bool m_ReconFlag;
        protected string m_Journal = "";
        protected bool m_Delete;
        protected decimal m_Purchases = 0;
        protected int m_dateYear;
	    protected bool m_isChanged;

	    public ApChecks() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public ApChecks(DateTime Date,short Company,short Department,short Sequence,short DistCompany,short DistDepartment,int CheckNumber,int VendorAccount,int ID,decimal Amount,bool ReconFlag, string journal) : 
			    base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Date = Date;
		    m_Company = Company;
		    m_Department = Department;
		    m_Sequence = Sequence;
		    m_DistCompany = DistCompany;
		    m_DistDepartment = DistDepartment;
		    m_CheckNumber = CheckNumber;
		    m_VendorAccount = VendorAccount;
		    m_ID = ID;
		    m_Amount = Amount;
		    m_ReconFlag = ReconFlag;
            m_Journal = journal;
	    }

	    protected ApChecks fill(DataRow dr,bool purchasesonly = false, bool vendor = false, bool total = false, bool sum = false, bool names = false)
	    {
		    try {
                m_Company = (short)dr[SQL_COL_COMPANY];
                m_Department = (short)dr[SQL_COL_DEPARTMENT];
                m_CheckNumber = (int)dr[SQL_COL_CHECK_NUMBER];
                m_Amount = (decimal)dr[SQL_COL_AMOUNT];
                m_Sequence = (short)dr[SQL_COL_SEQUENCE];
                m_VendorAccount = (int)dr[SQL_COL_VENDOR_ACCOUNT];
                if (!sum)
                {
                    if (purchasesonly)
                    {
                        try
                        {
                            m_Purchases = (decimal)dr[SQL_COL_PURCHASES];
                            m_dateYear = (int)dr[SQL_COL_YEAR];
                        }
                        catch (Exception ex)
                        { }
                        return this;
                    }

                    m_Date = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DATE]);
                    m_DistCompany = (short)dr[SQL_COL_DIST_COMPANY];
                    m_DistDepartment = (short)dr[SQL_COL_DIST_DEPARTMENT];
                    m_ID = (int)dr[SQL_COL_I_D];
                    m_ReconFlag = (bool)dr[SQL_COL_RECON_FLAG];
                    m_Journal = (string)(dr[SQL_COL_JOURNAL] == null ? "" : dr[SQL_COL_JOURNAL]);
                    if (total)
                        CheckTotal = (decimal)dr[SQL_COL_CHECK_TOTAL];

                }
            } catch (SqlException ex) {
		    }
		    return this;

	    }

        protected ApChecks fillSequenceCount(DataRow dr)
        {
            try
            {
                SequenceCount = (int)dr[SQL_COL_SEQUENCE_COUNT];
                m_Company = (short)dr[SQL_COL_COMPANY];
                m_Department = (short)dr[SQL_COL_DEPARTMENT];
                m_CheckNumber = (int)dr[SQL_COL_CHECK_NUMBER];
            }
            catch (SqlException ex)
            {
            }
            return this;

        }


        public static ApChecks load( int m_ID)
	    {
		    ApChecks obj  = new ApChecks();
		    string[] opt = {m_ID.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static ApChecks[] load(bool one = false, int cmp = 0, int dpt = 0, int chk = 0)
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    ApChecks obj = new ApChecks();
            string[] opts = { cmp.ToString(), dpt.ToString(), chk.ToString() };
            if (!one)
                ds = obj.loadAll();
            else
                ds = obj.load(SQL_LOAD_ONE, opts);
		    if (ds == null || ds.Tables[0].Rows.Count == 0)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new ApChecks();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (ApChecks[])list.ToArray(typeof(ApChecks));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }



        public static ApChecks[] LoadForCheckTotalFix ( DateTime StartDate, DateTime EndDate)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApChecks obj = new ApChecks();
            string[] opts = { StartDate.ToString(), EndDate.ToString() };

            //MessageBox.Show(SQL_LOAD_FOR_CHECK_TOTAL_FIX + "\r\n" +  opts[0]+ " / " + opts[1]);
            ds = obj.load(SQL_LOAD_FOR_CHECK_TOTAL_FIX, opts);
            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApChecks();
                obj.fill(dr, false, true);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApChecks[])list.ToArray(typeof(ApChecks));
            }
            catch (SqlException ex)
            {
                return null;
            }


        }

        public static ApChecks LoadCheckTotal(int company, int department, int checkNumber)
        {
            DataSet ds = new DataSet();
            string[] opts = { company.ToString(), department.ToString(), checkNumber.ToString() };
            ApChecks obj = new ApChecks();
            ds = obj.load(SQL_LOAD_CHECK_TOTAL, opts);
            if (ds == null)
                return null;
            obj.CheckTotal = (decimal)ds.Tables[0].Rows[0].Field<decimal>(SQL_COL_CHECK_TOTAL);
            return obj;
        }


        public static ApChecks[] LoadVendorTotals(int vendornum, int ThisYear, int LastYear)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            string[] opts = { vendornum.ToString(), ThisYear.ToString(), LastYear.ToString() };
            ApChecks obj = new ApChecks();
            ds = obj.load(SQL_LOAD_VENDOR_PURCHASES, opts);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApChecks();
                obj.fill(dr, true);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApChecks[])list.ToArray(typeof(ApChecks));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static ApChecks[] LoadCmpDptCheck( int company, int department, int checknumber)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            string[] opts = { company.ToString(), department.ToString(), checknumber.ToString() };
            ApChecks obj = new ApChecks();
            ds = obj.load(SQL_LOAD_BY_CPM_DPT_CHECK, opts);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApChecks();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApChecks[])list.ToArray(typeof(ApChecks));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }


        public static ApChecks[] LoadQuery(string query, bool sum = false)
            {
                ArrayList list = new ArrayList();

                DataSet ds = new DataSet();
                ApChecks obj = new ApChecks();
                string[] opt = null;

                ds = obj.load(query, opt);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new ApChecks();
                    obj.fill(dr,  false, false, false, sum);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (ApChecks[])list.ToArray(typeof(ApChecks));
                }
                catch (SqlException ex)
                {
                    return null;
                }


            }

            public bool save()
	    {
		    if (m_ID > 0 ) {
			    string[] values = {m_Date.ToString(), m_Company.ToString(), m_Department.ToString(), m_Sequence.ToString(), m_DistCompany.ToString(), m_DistDepartment.ToString(), m_CheckNumber.ToString(), m_VendorAccount.ToString(), m_Amount.ToString(), m_ReconFlag.ToString(), m_Journal, m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Date.ToString(), m_Company.ToString(), m_Department.ToString(), m_Sequence.ToString(), m_DistCompany.ToString(), m_DistDepartment.ToString(), m_CheckNumber.ToString(), m_VendorAccount.ToString(), m_Amount.ToString(), m_ReconFlag.ToString(), m_Journal};
			    m_ID = this.insertRecord(values);
			    if (m_ID >= 0)
				    return true;
		    }
		    return false;

	    }

        public static ApChecks[] LoadSequenceCounts()
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApChecks obj = new ApChecks();

            ds = obj.load(SQL_LOAD_SEQUENCE_COUNT, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApChecks();
                obj.fillSequenceCount(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApChecks[])list.ToArray(typeof(ApChecks));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static bool TestForDupeSequence(int company, int department, int checknumber, int sequence)
        {
            string[] opts = { company.ToString(), department.ToString(), checknumber.ToString(), sequence.ToString() };
            DataSet ds = new DataSet();
            ApChecks obj = new ApChecks();
            ds = obj.load(SQL_LOAD_DUPE_SEQ, opts);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public static void CleanDupeMasterRecords()
        {
            ApChecks[] dupes = LoadSequenceCounts();
            if (dupes != null && dupes.Length > 0)
            {
                int dupeChecks = 0;
                int processed = 0;
                foreach (ApChecks dupe in dupes)
                {
                    if (dupe.SequenceCount > 1)
                    {
                        dupeChecks++;

                        ApChecks[] checks = ApChecks.LoadCmpDptCheck(dupe.Company, dupe.Department, dupe.CheckNumber);
                        if (checks != null && checks.Length > 0)
                        {
                            decimal total = 0;
                            bool seqused = false;
                            short sequence = 1;
                            foreach (ApChecks check in checks)
                            {
                                if (check.Sequence == 0)
                                {
                                    if (seqused)
                                        check.delete(check.ID.ToString());
                                    else
                                    {
                                        check.Amount = total;
                                        seqused = true;
                                        check.save();
                                    }

                                }

                                else
                                {
                                    total += check.Amount;
                                    check.Sequence = sequence++;
                                    check.save();
                                }
                            }
                            processed++;
                        }
                    }
                }
                string message = "Finished cleaning duplicate summary check records.\r\n\n" +
                                 "   " + dupeChecks.ToString().PadLeft(5) + " duplicate records found.\r\n" +
                                 "   " + processed.ToString().PadLeft(5) + " duplicated records processed.";
             }
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

	    public short Company {
		    set{ m_Company = value;
		    OnPropertyChanged("Company"); }
		    get { return m_Company; }
	    }

	    public short Department {
		    set{ m_Department = value;
		    OnPropertyChanged("Department"); }
		    get { return m_Department; }
	    }

	    public short Sequence {
		    set{ m_Sequence = value;
		    OnPropertyChanged("Sequence"); }
		    get { return m_Sequence; }
	    }

	    public short DistCompany {
		    set{ m_DistCompany = value;
		    OnPropertyChanged("DistCompany"); }
		    get { return m_DistCompany; }
	    }

	    public short DistDepartment {
		    set{ m_DistDepartment = value;
		    OnPropertyChanged("DistDepartment"); }
		    get { return m_DistDepartment; }
	    }

	    public int CheckNumber {
		    set{ m_CheckNumber = value;
		    OnPropertyChanged("CheckNumber"); }
		    get { return m_CheckNumber; }
	    }

	    public int VendorAccount {
		    set{ m_VendorAccount = value;
		    OnPropertyChanged("VendorAccount"); }
		    get { return m_VendorAccount; }
	    }

	    public int ID {
		    get { return m_ID; }
	    }

	    public decimal Amount {
		    set{ m_Amount = value;
		    OnPropertyChanged("Amount"); }
		    get { return m_Amount; }
	    }

	    public bool ReconFlag {
		    set{ m_ReconFlag = value;
		    OnPropertyChanged("ReconFlag"); }
		    get { return m_ReconFlag; }
	    }

        public string Journal
        {
            set { m_Journal = value;
                OnPropertyChanged("Journal");
            }
            get { return m_Journal; }
        }

        public decimal CheckTotal { get; set; } = 0;

        // Associated Transaction key
        public int Index { get; set; }
        // Associated Vendor Name
        public string VendorName { get; set; }

        public bool Delete {
            get { return m_Delete; }
            set
            {
                m_Delete = value;
                OnPropertyChanged("Delete");
            }
        }

        public decimal PurchaseTotal { get { return m_Purchases; } }
        public int DateYear { get { return m_dateYear; } }

        //public Company[] CompanyList { get; set; }
        public string CompanyName { get; set; }
        //public Company SelectedCompany { get; set; }
        //public Department[] DepartmentList { get; set; }
        public string DepartmentName { get; set; }
        //public Department SelectedDepartment { get; set; }
        public int SequenceCount { get; set; }
    }
}
