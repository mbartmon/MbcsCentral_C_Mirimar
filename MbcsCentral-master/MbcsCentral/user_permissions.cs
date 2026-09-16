using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace MbcsCentral
{
    public class user_permissions : clsDB ,  INotifyPropertyChanged
    {
 	    public event PropertyChangedEventHandler PropertyChanged;

        public const string SQL_COL_PERMISSIONS_ID = "permissions_id";
        public const string SQL_COL_USER_ID = "user_id";
        public const string SQL_COL_PERMISSION_ID = "permission_id";
        public const string SQL_COL_PERMISSION_TYPE = "permission_type";
        public const string SQL_COL_ACCESS = "access";
        public const string SQL_COL_MODIFY = "modify";
        public const string SQL_COL_REMOVE = "remove";
        public const string SQL_COL_PRINT = "print";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_USER_TYPE = "userType";
        public const string SQL_COL_AS_PERM_TYPE = "PermissionType";

        public const string SQL_COL_ID = "permissions_id";
        public static string TABLE = "user_permissions";


        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_PERMISSION_TYPE + "]," +
                "[" + SQL_COL_USER_TYPE + "]," +
                "[" + SQL_COL_ACCESS + "]," +
                "[" + SQL_COL_MODIFY + "]," +
                "[" + SQL_COL_REMOVE + "]," +
                "[" + SQL_COL_PRINT + "]," +
                "[" + SQL_COL_PERMISSIONS_ID + "]," +
                "[" + SQL_COL_USER_ID + "]," +
                "[" + SQL_COL_PERMISSION_ID + "]," +
                "[" + SQL_COL_CLIENT_ID + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_PERMISSIONS_ID + "] = ?;";

        static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_PERMISSION_TYPE + "]," +
                "[" + SQL_COL_USER_TYPE + "]," +
                "[" + SQL_COL_ACCESS + "]," +
                "[" + SQL_COL_MODIFY + "]," +
                "[" + SQL_COL_REMOVE + "]," +
                "[" + SQL_COL_PRINT + "]," +
                "[" + SQL_COL_PERMISSIONS_ID + "]," +
                "[" + SQL_COL_USER_ID + "]," +
                "[" + SQL_COL_PERMISSION_ID + "]," +
                "[" + SQL_COL_CLIENT_ID + "]" +
                " FROM " + TABLE + ";";

        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
                "[" + SQL_COL_PERMISSION_TYPE + "]," +
                "[" + SQL_COL_USER_TYPE + "]," +
                "[" + SQL_COL_ACCESS + "]," +
                "[" + SQL_COL_MODIFY + "]," +
                "[" + SQL_COL_REMOVE + "]," +
                "[" + SQL_COL_PRINT + "]," +
                "[" + SQL_COL_USER_ID + "]," +
                "[" + SQL_COL_PERMISSION_ID + "]," +
                "[" + SQL_COL_CLIENT_ID + "]" +
                ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?);";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_PERMISSION_TYPE + "]=?," +
                "[" + SQL_COL_USER_TYPE + "]=?," +
                "[" + SQL_COL_ACCESS + "]=?," +
                "[" + SQL_COL_MODIFY + "]=?," +
                "[" + SQL_COL_REMOVE + "]=?," +
                "[" + SQL_COL_PRINT + "]=?," +
                "[" + SQL_COL_USER_ID + "]=?," +
                "[" + SQL_COL_PERMISSION_ID + "]=?," +
                "[" + SQL_COL_CLIENT_ID + "]=?" +
                " WHERE " + "[" + SQL_COL_PERMISSIONS_ID + "] = ?;";

        static string SQL_LOAD_BY_USER = " SELECT " + 
                                         "[" + SQL_COL_PERMISSIONS_ID + "]," + 
                                         "[" + SQL_COL_USER_ID + "]," + 
                                         "[" + SQL_COL_PERMISSION_ID + "]," + 
                                         "[" + SQL_COL_PERMISSION_TYPE + "]," + 
                                         "[" + SQL_COL_ACCESS + "]," + 
                                         "[" + SQL_COL_MODIFY + "]," + 
                                         "[" + SQL_COL_REMOVE + "]," +
                                         "[" + SQL_COL_PRINT + "]," +
                                         "[" + SQL_COL_CLIENT_ID + "]," +
                                         "[" + SQL_COL_USER_TYPE + "]" +
                                         " FROM " + TABLE +   
                                         " WHERE " + SQL_COL_USER_ID + " = ?" +
                                         " AND " + SQL_COL_USER_TYPE + " = ?;";

        static string SQL_LOAD_BY_USER_PERM = " SELECT " +
                                 "[" + SQL_COL_PERMISSIONS_ID + "]," +
                                 "[" + SQL_COL_USER_ID + "]," +
                                 TABLE + ".[" + SQL_COL_PERMISSION_ID + "]," +
                                 TABLE + ".[" + SQL_COL_PERMISSION_TYPE + "]," +
                                 "[" + SQL_COL_ACCESS + "]," +
                                 "[" + SQL_COL_MODIFY + "]," +
                                 "[" + SQL_COL_REMOVE + "]," +
                                 "[" + SQL_COL_PRINT + "]," +
                                 "[" + SQL_COL_CLIENT_ID + "]," +
                                 "[" + SQL_COL_USER_TYPE + "]," +
                                 permission.TABLE + "." + permission.SQL_COL_PERMISSION_DESCRIPTION + ", " +
                                 permission.TABLE + "." + permission.SQL_COL_PERMISSION_NAME +  ", " +
                                 permission.TABLE + "." + permission.SQL_COL_PERMISSION_TYPE + " AS " + SQL_COL_AS_PERM_TYPE +
                                 " FROM " + TABLE + ", " + permission.TABLE +
                                 " WHERE " + SQL_COL_USER_ID + " = ?" +
                                 " AND " + SQL_COL_USER_TYPE + " = ?" +
                                 " AND " + TABLE + "." + SQL_COL_PERMISSION_ID + " = ?" +
                                 " AND " + permission.TABLE + "." + permission.SQL_COL_PERMISSION_ID + " = " + TABLE + "." + SQL_COL_PERMISSION_ID;

        static string SQL_LOAD_BY_CLIENT = " SELECT " +
                                         "[" + SQL_COL_PERMISSIONS_ID + "]," +
                                         "[" + SQL_COL_USER_ID + "]," +
                                         "[" + SQL_COL_PERMISSION_ID + "]," +
                                         "[" + SQL_COL_PERMISSION_TYPE + "]," +
                                         "[" + SQL_COL_ACCESS + "]," +
                                         "[" + SQL_COL_MODIFY + "]," +
                                         "[" + SQL_COL_REMOVE + "]," +
                                        "[" + SQL_COL_PRINT + "]," +
                                         "[" + SQL_COL_CLIENT_ID + "]," +
                                         "[" + SQL_COL_USER_TYPE + "]" +
                                         " FROM " + TABLE +
                                         " WHERE " + SQL_COL_CLIENT_ID + " = ?";

        protected int ID = 0;
        protected long permissions_id;
        protected long user_id;
        protected long permission_id;
        protected int permission_type;
        protected bool access;
        protected bool modify;
        protected bool remove;
        protected bool print;
        protected long clientId;
        protected int userType;
        protected bool isChanged;

        protected string description;
        protected string name;
        protected int permissionType;

        public user_permissions()
            : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
        }

        public user_permissions(long permissions_id, long user_id, long permission_id, int permission_type, bool access, bool modify, bool remove, int userType, long clientId)
            : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            this.permissions_id = permissions_id;
            this.user_id = user_id;
            this.permission_id = permission_id;
            this.permission_type = permission_type;
            this.access = access;
            this.modify = modify;
            this.remove = remove;
            this.userType = userType;
            this.clientId = clientId;
        }

        protected user_permissions fill(DataRow dr, bool useDescription = false)
        {

            try
            {
                ID = 1;
                this.permissions_id = (long)dr[SQL_COL_PERMISSIONS_ID];
                this.user_id = (long)dr[SQL_COL_USER_ID];
                this.permission_id = (int)dr[SQL_COL_PERMISSION_ID];
                this.permission_type = System.Convert.ToInt32(dr[SQL_COL_PERMISSION_TYPE]);
                this.access = System.Convert.ToBoolean(dr[SQL_COL_ACCESS]);
                this.modify = System.Convert.ToBoolean(dr[SQL_COL_MODIFY]);
                this.remove = System.Convert.ToBoolean(dr[SQL_COL_REMOVE]);
                this.print = System.Convert.ToBoolean(dr[SQL_COL_PRINT]);
                this.clientId = (long)dr[SQL_COL_CLIENT_ID];
                this.userType = (int)dr[SQL_COL_USER_TYPE];
                if (useDescription)
                {
                    this.description = (string)dr[permission.SQL_COL_PERMISSION_DESCRIPTION];
                    this.name = (string)dr[permission.SQL_COL_PERMISSION_NAME];
                    this.permissionType = (int)dr[SQL_COL_AS_PERM_TYPE];
                }
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static user_permissions load(long permissions_id)
        {

            user_permissions obj = new user_permissions();
            string[] opt = new string[] { permissions_id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static user_permissions[] loadByUser(long userid, bool withGroups = false, int userType = 0)
        {

            if (withGroups && userType != 0)
                withGroups = false;

            string[] opt = new string[] { userid.ToString(), userType.ToString() };

            ArrayList list = new ArrayList();

            if (withGroups)
            {
                GroupMembers[] groups = GroupMembers.loadByAccount(userid);
                if (groups != null && groups.Length > 0)
                {
                    foreach (GroupMembers grp in groups)
                    {
                        user_permissions[] perms = user_permissions.loadByUser(grp.groupId, false, 1);
                        if (perms != null && perms.Length > 0)
                        {
                            foreach (user_permissions p in perms)
                                if (!SerachList(list, p.Permission_id))
                                    list.Add(p);
                        }
                    }
                }
            }
            DataSet ds = new DataSet();
            user_permissions obj = new user_permissions();

            ds = obj.load(SQL_LOAD_BY_USER, opt);
            obj = null;
            if (ds == null)
                return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new user_permissions();
                obj.fill(dr);
                if (!SerachList(list, obj.Permission_id))
                    list.Add(obj);
                obj = null;
            }
            try
            {
                return ((user_permissions[])(list.ToArray(typeof(user_permissions))));
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        private static bool SerachList(ArrayList lst, long id)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (((user_permissions)lst[i]).permission_id == id)
                    return true;
            }
            return false;
        }   
        
        public static user_permissions loadByUserPerm(long userid, long permId, int userType = 0)
        {

            string[] opt = new string[] { userid.ToString(), userType.ToString(), permId.ToString() };

            DataSet ds = new DataSet();
            user_permissions obj = new user_permissions();

            ds = obj.load(SQL_LOAD_BY_USER_PERM, opt);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
                return null;
            obj = null;

            obj = new user_permissions();
            obj.fill(ds.Tables[0].Rows[0],true);
            try
            {
                return obj;
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public static user_permissions[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            user_permissions obj = new user_permissions();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new user_permissions();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return ((user_permissions[])(list.ToArray(typeof(user_permissions))));
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public static user_permissions[] loadByClient(long clientId)
        {

            string[] opt = new string[] { clientId.ToString()};

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            user_permissions obj = new user_permissions();

            ds = obj.load(SQL_LOAD_BY_CLIENT, opt);
            obj = null;
            if (ds == null)
                return null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new user_permissions();
                obj.fill(dr);
                if (!SerachList(list, obj.Permission_id))
                    list.Add(obj);
                obj = null;
            }
            try
            {
                return ((user_permissions[])(list.ToArray(typeof(user_permissions))));
            }
            catch (System.Exception)
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

            if (permissions_id > 0)
            {
                string[] values = { permission_type.ToString(), userType.ToString(), access.ToString(), modify.ToString(), remove.ToString(), print.ToString(), user_id.ToString(), permission_id.ToString(), clientId.ToString(), permissions_id.ToString() }; 
                if (this.updateRecord(ID.ToString(), values))
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
                string[] values = { permission_type.ToString(), userType.ToString(), access.ToString(), modify.ToString(), remove.ToString(), print.ToString(), user_id.ToString(), permission_id.ToString(), clientId.ToString() }; 
                permissions_id = System.Convert.ToInt32(this.insertRecord(values));
                if (permissions_id >= 0)
                {
                    return true;
                }
            }
            return false;

        }

	    private void OnPropertyChanged(string info) {
		    if (PropertyChanged != null) {
			    PropertyChanged(this, new PropertyChangedEventArgs(info));
			    isChanged = true;
		    }
	    }

	    // Properties
	    public long ClientId {
		    set{ clientId = value;
		    OnPropertyChanged("ClientId"); }
		    get { return clientId; }
	    }

	    public int Permission_type {
		    set{ permission_type = value;
		    OnPropertyChanged("Permission_type"); }
		    get { return permission_type; }
	    }

	    public int UserType {
		    set{ userType = value;
		    OnPropertyChanged("UserType"); }
		    get { return userType; }
	    }

	    public bool Access {
		    set{ access = value;
		    OnPropertyChanged("Access"); }
		    get { return access; }
	    }

	    public bool Modify {
		    set{ modify = value;
		    OnPropertyChanged("Modify"); }
		    get { return modify; }
	    }

	    public bool Remove {
		    set{ remove = value;
		    OnPropertyChanged("Remove"); }
		    get { return remove; }
	    }

        public bool Print
        {
            set
            {
                print = value;
                OnPropertyChanged("print");
            }
            get { return print; }
        }

        public long Permissions_id
        {
		    get { return permissions_id; }
	    }

	    public long User_id {
		    set{ user_id = value;
		    OnPropertyChanged("User_id"); }
		    get { return user_id; }
	    }

	    public long Permission_id {
		    set{ permission_id = value;
		    OnPropertyChanged("Permission_id"); }
		    get { return permission_id; }
	    }

        public string Description
        {
            get { return description; }
            set 
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int PermissionType
        {
            get { return permissionType; }
            set
            {
                permissionType = value;
                OnPropertyChanged("PermissionType");
            }
        }
    }
}