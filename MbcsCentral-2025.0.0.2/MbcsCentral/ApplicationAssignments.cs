using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.OleDb;

namespace MbcsCentral
{
    public class ApplicationAssignments : clsDB, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        public static string TABLE = "ApplicationAssignments";
        public const string SQL_COL_I_D = "ID";
        public const string SQL_COL_CLIENT_ID = "clientId";
        public const string SQL_COL_APPLICATION_ID = "applicationId";

        public const string SQL_COL_ACTIVE = "active";

        static string SQL_LOAD = " SELECT " + "[" + SQL_COL_I_D + "]," + "[" + SQL_COL_CLIENT_ID + "]," + "[" + SQL_COL_APPLICATION_ID + "]," + "[" + SQL_COL_ACTIVE + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_I_D + " = ?;";

        static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_I_D + "]," + "[" + SQL_COL_CLIENT_ID + "]," + "[" + SQL_COL_APPLICATION_ID + "]," + "[" + SQL_COL_ACTIVE + "]" + " FROM " + TABLE + ";";

        static string SQL_LOAD_BY_CLIENT = " SELECT " + 
                                           "[" + SQL_COL_I_D + "]," + 
                                           "[" + SQL_COL_CLIENT_ID + "]," + 
                                           "[" + SQL_COL_APPLICATION_ID + "]," + 
                                           "[" + SQL_COL_ACTIVE + "]" + 
                                           " FROM " + TABLE + 
                                           " WHERE " + SQL_COL_CLIENT_ID + " = ?;";

        static string SQL_LOAD_BY_APP = " SELECT " +
                                           "[" + SQL_COL_I_D + "]," +
                                           "[" + SQL_COL_CLIENT_ID + "]," +
                                           "[" + SQL_COL_APPLICATION_ID + "]," +
                                           "[" + SQL_COL_ACTIVE + "]" +
                                           " FROM " + TABLE +
                                           " WHERE " + SQL_COL_APPLICATION_ID + " = ?;";

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_CLIENT_ID + "]," + "[" + SQL_COL_APPLICATION_ID + "]," + "[" + SQL_COL_ACTIVE + "]" + ") VALUES (?, ?, ?);";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_CLIENT_ID + "]=?," + "[" + SQL_COL_APPLICATION_ID + "]=?," + "[" + SQL_COL_ACTIVE + "]=?" + " WHERE " + SQL_COL_I_D + " = ?;";
        protected long m_ID;
        protected long m_clientId;
        protected long m_applicationId;
        protected bool m_active;

        protected bool m_isChanged;
        public ApplicationAssignments()
            : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {

        }

        public ApplicationAssignments(long ID, long clientId, long applicationId, bool active)
            : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            m_ID = ID;
            m_clientId = clientId;
            m_applicationId = applicationId;
            m_active = active;
        }

        protected ApplicationAssignments fill(DataRow dr)
        {

            try
            {
                m_ID = (long)dr[SQL_COL_I_D];
                m_clientId = (long)dr[SQL_COL_CLIENT_ID];
                m_applicationId = (long)dr[SQL_COL_APPLICATION_ID];
                m_active = (bool)dr[SQL_COL_ACTIVE];
            }
            catch (OleDbException ex)
            {
            }
            catch (System.Exception exg)
            {
            }
            return this;

        }

        public static ApplicationAssignments load(long m_ID)
        {

            ApplicationAssignments obj = new ApplicationAssignments();
            string[] opt = { m_ID.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static ApplicationAssignments[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApplicationAssignments obj = new ApplicationAssignments();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApplicationAssignments();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApplicationAssignments[])list.ToArray(typeof(ApplicationAssignments));
            }
            catch (OleDbException ex)
            {
                return null;
            }

        }

        public static ApplicationAssignments[] loadByClient(long clientId)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApplicationAssignments obj = new ApplicationAssignments();
            string[] values = { clientId.ToString() };

            ds = obj.load(SQL_LOAD_BY_CLIENT, values);
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApplicationAssignments();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApplicationAssignments[])list.ToArray(typeof(ApplicationAssignments));
            }
            catch (OleDbException ex)
            {
                return null;
            }

        }

        public static ApplicationAssignments[] loadByApp(long appId)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            ApplicationAssignments obj = new ApplicationAssignments();
            string[] values = { appId.ToString() };

            ds = obj.load(SQL_LOAD_BY_APP, values);
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new ApplicationAssignments();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (ApplicationAssignments[])list.ToArray(typeof(ApplicationAssignments));
            }
            catch (OleDbException ex)
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

            if (m_ID > 0)
            {
                string[] values = {
				m_clientId.ToString(),
				m_applicationId.ToString(),
				m_active.ToString(),
				m_ID.ToString()
			};
                if (this.updateRecord(m_ID.ToString(), values))
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
                string[] values = {
				m_clientId.ToString(),
				m_applicationId.ToString(),
				m_active.ToString()
			};
                m_ID = this.insertRecord(values);
                if (m_ID >= 0)
                {
                    return true;
                }
            }
            return false;

        }


        public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
        {

            try
            {
                CN.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE ApplicationAssignments (ID AUTONUMBER,clientId LONG,applicationId LONG,active YESNO)";
                cmd.Connection = CN;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                return false;
            }
            finally
            {
                try
                {
                    CN.Close();
                }
                catch
                {
                }
            }

            return true;

        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
            m_isChanged = true;
        }

        // Property Routines
        public bool isChanged
        {
            get { return m_isChanged; }
            set { m_isChanged = value; }
        }
        public long ID
        {
            get { return m_ID; }
        }

        public long clientId
        {
            get { return m_clientId; }
            set
            {
                m_clientId = value;
                OnPropertyChanged("clientId");
            }
        }

        public long applicationId
        {
            get { return m_applicationId; }
            set
            {
                m_applicationId = value;
                OnPropertyChanged("applicationId");
            }
        }

        public bool active
        {
            get { return m_active; }
            set
            {
                m_active = value;
                OnPropertyChanged("active");
            }
        }

    }
}