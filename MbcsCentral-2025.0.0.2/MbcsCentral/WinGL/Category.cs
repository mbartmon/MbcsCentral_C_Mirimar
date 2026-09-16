using System;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MbcsCentral.WinGL {
    public class Category : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "gl_Category";

	    public const string SQL_COL_SEQUENCE = "Sequence";
	    public const string SQL_COL_ID = "Id";
	    public const string SQL_COL_KEY = "Key";
	    public const string SQL_COL_SUB_OF = "SubOf";
	    public const string SQL_COL_CATEGORY_DESCRIPTION = "CategoryDescription";
	    public const string SQL_COL_OLD_KEY = "OldKey";

        public static string compare = "";

        public static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_SUB_OF + "]," +
                "[" + SQL_COL_CATEGORY_DESCRIPTION + "]," +
                "[" + SQL_COL_OLD_KEY + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        public static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_SUB_OF + "]," +
                "[" + SQL_COL_CATEGORY_DESCRIPTION + "]," +
                "[" + SQL_COL_OLD_KEY + "]" +
                " FROM " + TABLE +
                " ORDER BY " + SQL_COL_SEQUENCE;

	    public static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_SEQUENCE + "]," +
			    "[" + SQL_COL_KEY + "]," +
			    "[" + SQL_COL_SUB_OF + "]," +
			    "[" + SQL_COL_CATEGORY_DESCRIPTION + "]," +
			    "[" + SQL_COL_OLD_KEY + "]" +
			    ") VALUES (?, '?', '?', '?', '?');";

	    public static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_SEQUENCE + "]=?," +
			    "[" + SQL_COL_KEY + "]='?'," +
			    "[" + SQL_COL_SUB_OF + "]='?'," +
			    "[" + SQL_COL_CATEGORY_DESCRIPTION + "]='?'," +
			    "[" + SQL_COL_OLD_KEY + "]='?'" +
			    " WHERE " + "[" + SQL_COL_ID + "] = ?;";

        public static string SQL_LOAD_BY_SUB_KEY = "SELECT " +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_SUB_OF + "]," +
                "[" + SQL_COL_CATEGORY_DESCRIPTION + "]," +
                "[" + SQL_COL_OLD_KEY + "]" +
                " FROM " + TABLE +
                " WHERE " + SQL_COL_SUB_OF + "<compare>" +
                " ORDER BY " + SQL_COL_SEQUENCE;


        public static string SQL_LOAD_BY_CATEGORY = "SELECT " +
                "[" + SQL_COL_SEQUENCE + "]," +
                "[" + SQL_COL_ID + "]," +
                "[" + SQL_COL_KEY + "]," +
                "[" + SQL_COL_SUB_OF + "]," +
                "[" + SQL_COL_CATEGORY_DESCRIPTION + "]," +
                "[" + SQL_COL_OLD_KEY + "]" +
                " FROM " + TABLE +
                " WHERE [" + SQL_COL_KEY + "] = '?'";

        static string SQL_DELETE_ALL = "DELETE FROM " + TABLE;

        static string SQL_GET_HIGHEST_KEY = "SELECT " +
                "[" + SQL_COL_KEY + "]" +
                " FROM " + TABLE +
                " ORDER BY [" + SQL_COL_KEY + "] DESC";

        static string SQL_GET_HIGHEST_SEQUENCE = "SELECT " +
                                                 "[" + SQL_COL_SEQUENCE + "]" +
                                                 " FROM " + TABLE +
                                                 " ORDER BY [" + SQL_COL_SEQUENCE + "] DESC";
       
        public enum Columns
        {
            Sequence,
            ID,
            Key,
            SubOf,
            CategoryDescription,
            OldKey
        }

	    protected short m_Sequence;
	    protected int m_Id;
	    protected string m_Key;
	    protected string m_SubOf;
	    protected string m_CategoryDescription;
	    protected string m_OldKey;
	    protected bool m_isChanged;

        public  enum CatTypes
        {
            Major,
            Sub,
            SubSub
        }

        public enum MajorTypes
        {
            All,
            Assets,
            Liabilities,
            Equities,
            Income,
            Expenses
        }
        public static string[] MajorKeys = {"", "001_", "002_", "003_", "004_", "005_" };

        private SubCat[] subCategories = null;

	    public Category() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public Category(short Sequence,int Id,string Key,string SubOf,string CategoryDescription,string OldKey) : 
			    base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_Sequence = Sequence;
		    m_Id = Id;
		    m_Key = Key;
		    m_SubOf = SubOf;
		    m_CategoryDescription = CategoryDescription;
		    m_OldKey = OldKey;
	    }

	    protected Category fill(DataRow dr)
	    {
		    try {
			    m_Sequence = (short) dr[SQL_COL_SEQUENCE];
			    m_Id = (int) dr[SQL_COL_ID];
			    m_Key = (string) (Convert.IsDBNull(dr[SQL_COL_KEY]) ? "" : dr[SQL_COL_KEY]);
			    m_SubOf = (string) (Convert.IsDBNull(dr[SQL_COL_SUB_OF]) ? "" : dr[SQL_COL_SUB_OF]);
			    m_CategoryDescription = (string) (Convert.IsDBNull(dr[SQL_COL_CATEGORY_DESCRIPTION]) ? "" : dr[SQL_COL_CATEGORY_DESCRIPTION]);
			    m_OldKey = (string) (Convert.IsDBNull(dr[SQL_COL_OLD_KEY]) ? "" : dr[SQL_COL_OLD_KEY]);
		    } catch (SqlException ex) {
		    }
		    return this;

	    }

	    public static Category load( int m_Id)
	    {
		    Category obj  = new Category();
		    string[] opt = {m_Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;

	    }

	    public static Category[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    Category obj = new Category();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
        
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new Category();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (Category[])list.ToArray(typeof(Category));
		    } catch (SqlException ex) {
			    return null;
		    }
	    }

            public static Category load(string category)
            {
                Category obj = new Category();
                string[] opt = { category };
                DataSet ds = new DataSet();
                ds = obj.load(SQL_LOAD_BY_CATEGORY, opt);
                if (ds != null)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    return obj.fill(dr);
                }
                else
                    return null;

            }


            public static DataSet loadDS()
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Category obj = new Category();

            ds = obj.loadAll();
            if (ds == null)
                return null;
            return ds;
        }

        public static Category[] loadBySubKey( string subKey, bool except = false)
        {
             ArrayList list = new ArrayList();
            string[] opt = { subKey };
            DataSet ds = new DataSet();
            Category obj = new Category();

            if (except)
                compare = " <> '?'";
            else
                compare = " = '?'";
            SQL_LOAD_BY_SUB_KEY = SQL_LOAD_BY_SUB_KEY.Replace("<compare>", compare);
            ds = obj.load(SQL_LOAD_BY_SUB_KEY, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Category();
                obj.fill(dr);
                #region sub category processing
                /*
                if (subCategories1 == null)
                {
                    Category[] subCats = loadBySubKey(obj.Key);
                    if (subCats != null)
                    {
                        subCategories1 = new List<Category>();
                        foreach (Category cat in subCats)
                        {
                            subCategories1.Add(cat);
                            if (subCategories2 == null)
                            {
                                Category[] subCats2 = loadBySubKey(cat.Key);
                                if (subCats2 != null)
                                {
                                    subCategories2 = new List<Category>();
                                    foreach (Category c in subCats2)
                                    {
                                        subCategories2.Add(c);
                                    }
                                    cat.SubCategories = subCategories2;
                                    subCategories2 = null;
                                }
                            }
                       }
                       obj.SubCategories = subCategories1;
                        subCategories1 = null;
                    }
                }
                */
                #endregion
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Category[])list.ToArray(typeof(Category));
            }
            catch (SqlException ex)
            {
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
			    string[] values = {m_Sequence.ToString(), m_Key, m_SubOf, m_CategoryDescription, m_OldKey, m_Id.ToString()};
			    if (this.updateRecord(m_Id.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values = {m_Sequence.ToString(), m_Key, m_SubOf, m_CategoryDescription, m_OldKey};
			    m_Id = this.insertRecord(values);
			    if (m_Id >= 0) {
				    return true;
			    }
		    }
		    return false;
	    }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static bool deleteAll()
        {
            GlobalShared.CN.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL_DELETE_ALL;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = GlobalShared.CN;
            try
            {
                cmd.ExecuteNonQuery();
                try
                {
                    GlobalShared.CN.Close();
                }
                catch { }
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetHighestKey()
        {
            Category obj = new Category();
            string[] opt = null;
            DataSet ds = new DataSet();
            ds = obj.load(SQL_GET_HIGHEST_KEY, opt);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return (string)dr[SQL_COL_KEY];
            }
            else
                return "000_";
        }

        public static short GetHighestSequence()
        {
            Category obj = new Category();
            string[] opt = null;
            DataSet ds = new DataSet();
            ds = obj.load(SQL_GET_HIGHEST_SEQUENCE, opt);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return (short)dr[SQL_COL_SEQUENCE];
            }
            else
                return 0;
        }

        private void OnPropertyChanged(string info)
        {
		    if (PropertyChanged != null) {
			    PropertyChanged(this, new PropertyChangedEventArgs(info));
			    m_isChanged = true;
		    }
	    }

	    // Properties
	    public short Sequence {
		    set{ m_Sequence = value;
		    OnPropertyChanged("Sequence"); }
		    get { return m_Sequence; }
	    }

	    public int Id {
		    get { return m_Id; }
            set { m_Id = value; }
	    }

	    public string Key {
		    set{ m_Key = value;
		    OnPropertyChanged("Key"); }
		    get { return m_Key; }
	    }

	    public string SubOf {
		    set{ m_SubOf = value;
		    OnPropertyChanged("SubOf"); }
		    get { return m_SubOf; }
	    }

	    public string CategoryDescription {
		    set{ m_CategoryDescription = value;
		    OnPropertyChanged("CategoryDescription"); }
		    get { return m_CategoryDescription; }
	    }

	    public string OldKey {
		    set{ m_OldKey = value;
		    OnPropertyChanged("OldKey"); }
		    get { return m_OldKey; }
	    }

        public object Parent { get; set; }

        public int CatType { get; set; }
    
        public SubCat[] SubCategories
        {
            get { return subCategories; }
            set { subCategories = value; }
        }

    }
}

