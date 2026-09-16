using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    public class STCodes : clsDB, INotifyPropertyChanged
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


        static string TABLE = "pm_STCodes";

        public const string SQL_COL_CODE = "Code";
        public const string SQL_COL_KEY = "Key";
        public const string SQL_COL_DESCRIPTION = "Description";

        static string SQL_LOAD = " SELECT " +
                                "[" + SQL_COL_CODE + "]," +
                                "[" + SQL_COL_KEY + "]," +
                                "[" + SQL_COL_DESCRIPTION + "]" +
                                " FROM " + TABLE +
                                " WHERE " + SQL_COL_CODE + " = \'?\';";

        static string SQL_LOAD_ALL = " SELECT " +
                                    "[" + SQL_COL_CODE + "]," +
                                    "[" + SQL_COL_KEY + "]," +
                                    "[" + SQL_COL_DESCRIPTION + "]" +
                                    " FROM " + TABLE;

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" +
                                    "[" + SQL_COL_CODE + "]," +
                                    "[" + SQL_COL_DESCRIPTION + "]" +
                                    ") VALUES (\'?\', \'?\');";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " +
                                    "[" + SQL_COL_CODE + "]=\'?\'," +
                                    "[" + SQL_COL_DESCRIPTION + "]=\'?\'" +
                                    " WHERE [" + SQL_COL_KEY + "] = ?";

        static string SQL_LOAD_BY_KEY = " SELECT " +
                               "[" + SQL_COL_CODE + "]," +
                               "[" + SQL_COL_KEY + "]," +
                               "[" + SQL_COL_DESCRIPTION + "]" +
                               " FROM " + TABLE +
                               " WHERE [" + SQL_COL_KEY + "] = ?;";

        protected string m_Code = "";
        protected int m_Key;
        protected string m_Description;
        protected bool m_isChanged;

        public STCodes()
            : base( TABLE, SQL_COL_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
        }

        public STCodes( string Code, int Key, string Description )
            : base( TABLE, SQL_COL_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN )
        {
            m_Code = Code;
            m_Key = Key;
            m_Description = Description;
        }

        protected STCodes fill( DataRow dr )
        {

            try
            {
                m_Code = (string) ((Information.IsDBNull( dr[SQL_COL_CODE] )) ? "" : (dr[SQL_COL_CODE]));
                m_Key = (int)( dr[SQL_COL_KEY] );
                m_Description = (string) ((Information.IsDBNull( dr[SQL_COL_DESCRIPTION] )) ? "" : (dr[SQL_COL_DESCRIPTION]));
            }
            catch (SqlException)
            {
            }
            catch (System.Exception)
            {
            }
            return this;

        }

        public static STCodes load( string m_Code )
        {

            STCodes obj = new STCodes();
            string[] opt = new string[] { m_Code };
            DataSet ds = new DataSet();
            ds = obj.load( SQL_LOAD, opt );
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill( dr );

        }

        public static STCodes load(int key)
        {

            STCodes obj = new STCodes();
            string[] opt = new string[] { key.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_KEY, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static STCodes[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            STCodes obj = new STCodes();

            ds = obj.loadAll();
            obj = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new STCodes();
                obj.fill( dr );
                list.Add( obj );
                obj = null;
            }
            try
            {
                return ((STCodes[]) (list.ToArray( typeof( STCodes ) )));
            }
            catch (SqlException)
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

            if (m_Key != 0)
            {
                string[] values = new string[] { m_Code, m_Description, m_Key.ToString() };
                if (this.updateRecord( m_Code, values ))
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
                string[] values = new string[] { m_Code, m_Description };
                this.insertRecord( values );
                if (m_Code != null)
                {
                    return true;
                }
            }
            return false;

        }


        public static bool CreateTable( System.Data.SqlClient.SqlConnection CN )
        {

            try
            {
                CN.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE STCodes (Code TEXT(1),Key AUTONUMBER,Description TEXT(32))";
                cmd.Connection = CN;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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
        public string Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                m_Code = value;
                OnPropertyChanged( "Code" );
            }
        }

        public int Key
        {
            get
            {
                return m_Key;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
                OnPropertyChanged( "Description" );
            }
        }

        public string FullDescription
        {
            get { return Code.Trim() + " " + Description; }
        }

        public string IsNew { get; set; } = "False";
    }	
}