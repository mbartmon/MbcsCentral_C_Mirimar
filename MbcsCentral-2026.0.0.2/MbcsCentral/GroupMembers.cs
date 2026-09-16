using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral 
{
    public class GroupMembers : clsDB	, INotifyPropertyChanged
    {
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "GroupMembers";

	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_GROUP_ID = "groupId";
	    public const string SQL_COL_ACCOUNT_ID = "accountId";

	    static string SQL_LOAD = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_GROUP_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]" +
			    " FROM " + TABLE +
			    " WHERE " + SQL_COL_I_D + " = ?";

	    static string SQL_LOAD_ALL = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_GROUP_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]" +
			    " FROM " + TABLE + ";";

	    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
			    "[" + SQL_COL_GROUP_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]" +
			    ") VALUES (?, ?);";

	    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
			    "[" + SQL_COL_GROUP_ID + "]=?," +
			    "[" + SQL_COL_ACCOUNT_ID + "]=?" +
			    " WHERE " + SQL_COL_I_D + " = ?" ;

	    static string SQL_LOAD_BY_GROUP = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_GROUP_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]" +
			    " FROM " + TABLE +
			    " WHERE " + SQL_COL_GROUP_ID + " = ?";

	    static string SQL_LOAD_ACCOUNT = "SELECT " +
			    "[" + SQL_COL_I_D + "]," +
			    "[" + SQL_COL_GROUP_ID + "]," +
			    "[" + SQL_COL_ACCOUNT_ID + "]," +
                Groups.TABLE + "." + Groups.SQL_COL_GROUP_NAME +
			    " FROM " + TABLE + ", " + Groups.TABLE +
			    " WHERE " + SQL_COL_ACCOUNT_ID + " = ?" +
                " AND " + Groups.TABLE + "." + Groups.SQL_COL_GROUP_ID + " = " + TABLE + "." + SQL_COL_GROUP_ID;

	    protected long m_ID;
	    protected long m_groupId;
	    protected long m_accountId;
	    protected bool m_isChanged;

        protected enum AdditionalFields
        {
            GroupName = 0
        }

        // Group Table fields
        protected string m_groupName;

	    public GroupMembers() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
	    }

	    public GroupMembers(long groupId,long accountId) : 
			    base(TABLE,SQL_COL_I_D , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	    {
		    m_groupId = groupId;
		    m_accountId = accountId;
	    }

        public GroupMembers(long groupId, long accountId, string name) :
            base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            m_groupId = groupId;
            m_accountId = accountId;
            m_groupName = name;
        }

        protected GroupMembers fill(DataRow dr, int additional = -1)
	    {
		    try {
			    m_ID = (long) dr[SQL_COL_I_D];
			    m_groupId = (long) dr[SQL_COL_GROUP_ID];
			    m_accountId = (long) dr[SQL_COL_ACCOUNT_ID];
                if (additional == (int)AdditionalFields.GroupName)
                    m_groupName = (string)dr[Groups.SQL_COL_GROUP_NAME];
		    } catch (SqlException ex) {
		    }
		    return this;
	    }

	    public static GroupMembers load(long Id)
	    {
		    GroupMembers obj  = new GroupMembers();
		    string[] opt = {Id.ToString()};
		    DataSet ds = new DataSet();
		    ds = obj.load(SQL_LOAD, opt);
		    if (ds != null) 
            {
			    DataRow dr = ds.Tables[0].Rows[0];
			    return obj.fill(dr);
		    } else
			    return null;
	    }

	    public static GroupMembers[] load()
	    {
		    ArrayList list = new ArrayList();

		    DataSet ds = new DataSet();
		    GroupMembers obj = new GroupMembers();

		    ds = obj.loadAll();
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new GroupMembers();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (GroupMembers[])list.ToArray(typeof(GroupMembers));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

	    public static GroupMembers[] loadByGroup(long groupId)
	    {
		    ArrayList list = new ArrayList();
            string[] opt = {groupId.ToString()};

		    DataSet ds = new DataSet();
		    GroupMembers obj = new GroupMembers();

		    ds = obj.load(SQL_LOAD_BY_GROUP, opt);
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new GroupMembers();
			    obj.fill(dr);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (GroupMembers[])list.ToArray(typeof(GroupMembers));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

	    public static GroupMembers[] loadByAccount(long accountId)
	    {
		    ArrayList list = new ArrayList();
            string[] opt = {accountId.ToString()};

		    DataSet ds = new DataSet();
		    GroupMembers obj = new GroupMembers();

		    ds = obj.load(SQL_LOAD_ACCOUNT, opt);
		    if (ds == null)
			    return null;
		    obj = null;
		    foreach (DataRow dr in ds.Tables[0].Rows) {
			    obj = new GroupMembers();
			    obj.fill(dr, (int)AdditionalFields.GroupName);
			    list.Add(obj);
			    obj = null;
		    }
		    try {
			    return (GroupMembers[])list.ToArray(typeof(GroupMembers));
		    } catch (SqlException ex) {
			    return null;
		    }

	    }

        public static ArrayList loadByAccountArray(long accountId)
        {
            ArrayList list = new ArrayList();
            string[] opt = { accountId.ToString() };

            DataSet ds = new DataSet();
            GroupMembers obj = new GroupMembers();

            ds = obj.load(SQL_LOAD_ACCOUNT, opt);
            if (ds == null)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new GroupMembers();
                obj.fill(dr, (int)AdditionalFields.GroupName);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return list;
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
		    if (m_ID > 0 ) {
			    string[] values = { m_groupId.ToString(), m_accountId.ToString(), m_ID.ToString()};
			    if (this.updateRecord(m_ID.ToString(), values)) {
				    return true;
			    } else {
				    return false;
			    }
		    } else {
			    string[] values =  {m_groupId.ToString(), m_accountId.ToString()};
			    m_ID = this.insertRecord(values);
			    if (m_ID >= 0) {
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
	    public long ID {
		    set{ m_ID = value;
		    OnPropertyChanged("ID"); }
		    get { return m_ID; }
	    }

	    public long groupId {
		    set{ m_groupId = value;
		    OnPropertyChanged("groupId"); }
		    get { return m_groupId; }
	    }

	    public long accountId {
		    set{ m_accountId = value;
		    OnPropertyChanged("accountId"); }
		    get { return m_accountId; }
	    }

        public string GroupName
        {
            get { return m_groupName; }
            set
            {
                m_groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        public bool IsChanged
        {
            set { m_isChanged = value;
            OnPropertyChanged("IsChanged");}
            get {return m_isChanged;}
        }
    }
}
