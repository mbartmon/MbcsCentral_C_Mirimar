using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;

namespace MbcsCentral.WinPM
{
    public class StatGroups : clsDB, INotifyPropertyChanged
    {

        private PropertyChangedEventHandler PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Combine( PropertyChangedEvent, value );
            }
            remove
            {
                PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Remove( PropertyChangedEvent, value );
            }
        }

        public static string TABLE = "pm_StatGroups";

        public const string SQL_COL_GROUP_NUMBER = "Group Number";
        public const string SQL_COL_FROM_GROUP = "From Group";
        public const string SQL_COL_TO_GROUP = "To Group";
        public const string SQL_COL_ID = "ID";
        public const string SQL_COL_CLIENT_ID = "clientId";

        static string SQL_LOAD = " SELECT " +
            "[" + SQL_COL_GROUP_NUMBER + "]," +
            "[" + SQL_COL_FROM_GROUP + "]," +
            "[" + SQL_COL_TO_GROUP + "]" +
            " FROM " + TABLE +
            " WHERE " + SQL_COL_ID + " = ?;";

        static string SQL_LOAD_ALL = " SELECT " +
            "[" + SQL_COL_GROUP_NUMBER + "]," +
            "[" + SQL_COL_FROM_GROUP + "]," +
            "[" + SQL_COL_TO_GROUP + "]" +
            " FROM " + TABLE + 
            " WHERE " + SQL_COL_CLIENT_ID + "=?";

        static string SQL_INSERT = " INSERT INTO " +
            TABLE + " (" +
            "[" + SQL_COL_GROUP_NUMBER + "]," +
            "[" + SQL_COL_FROM_GROUP + "]" +
            "[" + SQL_COL_TO_GROUP + "]," +
            "[" + SQL_COL_CLIENT_ID + "]" +
            ") VALUES (?, \'?\'\'?\',?);";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + 
            "[" + SQL_COL_GROUP_NUMBER + "]=?," +
            "[" + SQL_COL_FROM_GROUP + "]=\'?\'" +
            "[" + SQL_COL_TO_GROUP + "]=\'?\'," +
            "[" + SQL_COL_CLIENT_ID + "]=?" +
            " WHERE " + SQL_COL_ID + " = ?;";

        protected short m_GroupNumber;
        protected string m_FromGroup;
        protected string m_ToGroup;
        protected bool m_isChanged;
        protected long m_clientId;

        public StatGroups()
            : base( TABLE, SQL_COL_GROUP_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
            m_clientId = GlobalShared.user.Account.clientId;
        }

        public StatGroups( byte GroupNumber, string FromGroup, string ToGroup )
            : base( TABLE, SQL_COL_GROUP_NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
            m_GroupNumber = GroupNumber;
            m_FromGroup = FromGroup;
            m_ToGroup = ToGroup;
            m_clientId = GlobalShared.user.Account.clientId;
        }

        protected StatGroups fill( DataRow dr )
        {

            try
            {
                m_GroupNumber = (short) dr[SQL_COL_GROUP_NUMBER];
                m_FromGroup = (string) ((System.DBNull.Value.Equals( dr[SQL_COL_FROM_GROUP] )) ? "" : (dr[SQL_COL_FROM_GROUP]));
                m_ToGroup = (string)((System.DBNull.Value.Equals(dr[SQL_COL_TO_GROUP])) ? "" : (dr[SQL_COL_TO_GROUP]));
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static StatGroups load( int id )
        {

            StatGroups obj = new StatGroups();
            string[] opt = new string[] {id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load( SQL_LOAD, opt );
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill( dr );

        }

        public static StatGroups[] loads(long clientId)
        {

            ArrayList list = new ArrayList();

            string[] opt = { clientId.ToString() };
            DataSet ds = new DataSet();
            StatGroups obj = new StatGroups();

            ds = obj.load(SQL_LOAD_ALL,opt);
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new StatGroups();
                obj.fill( dr );
                list.Add( obj );
                obj = null;
            }
            try
            {
                return ((StatGroups[]) (list.ToArray( typeof( StatGroups ) )));
            }
            catch (OleDbException)
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

            if (m_GroupNumber > 0)
            {
                string[] values = new string[] { m_GroupNumber.ToString(), m_FromGroup, m_ToGroup,  };
                if (this.updateRecord( m_GroupNumber.ToString(), values ))
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
                string[] values = new string[] { m_GroupNumber.ToString(), m_FromGroup, m_ToGroup };
                m_GroupNumber = (byte) this.insertRecord( values );
                if (m_GroupNumber >= 0)
                {
                    return true;
                }
            }
            return false;

        }


        public static bool CreateTable( System.Data.OleDb.OleDbConnection CN )
        {

            try
            {
                CN.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE StatGroups (Group Number ,From Group TEXT(3),To Group TEXT(3))";
                cmd.Connection = CN;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.OleDb.OleDbException)
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

        private void OnPropertyChanged( string info )
        {
            if (PropertyChangedEvent != null)
                PropertyChangedEvent( this, new PropertyChangedEventArgs( info ) );
            m_isChanged = true;
        }

        // Property Routines
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool isChanged
        {
            get
            {
                return m_isChanged;
            }
            set
            {
                m_isChanged = System.Convert.ToBoolean( value );
            }
        }
        public short GroupNumber
        {
            get
            {
                return m_GroupNumber;
            }
            set
            {
                m_GroupNumber = value;
                OnPropertyChanged( "GroupNumber" );
            }
        }

        public string FromGroup
        {
            get
            {
                return m_FromGroup;
            }
            set
            {
                m_FromGroup = value;
                OnPropertyChanged( "FromGroup" );
            }
        }

        public string ToGroup
        {
            get
            {
                return m_ToGroup;
            }
            set
            {
                m_ToGroup = value;
                OnPropertyChanged( "ToGroup" );
            }
        }
    }	
}