using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace MbcsCentral
{
    public class Table
    {

        public const string PROPERTY_PREFIX = "m_";
        private string Name;

        private columns[] Cols = null;
        Dictionary<string, columns> dict;

        public Table( string name, OleDbConnection cn )
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader reader;
            DataTable dt;
            dict = new Dictionary<string, columns>();
            try
            {
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM [" + name + "]";
                reader = cmd.ExecuteReader( CommandBehavior.KeyInfo );
                dt = reader.GetSchemaTable();
            }
            catch
            {
                return;
            }

            this.Name = name;
            int c = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize( ref Cols, c + 1 );
                Cols[ c ] = new columns( (System.String) dr[ "ColumnName" ], dr[ "DataType" ].ToString(), (System.Boolean) dr[ "IsKey" ], (System.Boolean) dr[ "isAutoIncrement" ], (int) dr[ "ColumnSize" ] );
                dict.Add( dr["ColumnName"].ToString(), Cols[c] );
                c++;
            }

        }

        public Table( string name, SqlConnection cn )
        {
            string xsql = "SELECT c.name 'ColumnName', t.Name 'DataType', c.max_length 'MaxLength', c.precision, c.scale, c.is_nullable," + " ISNULL(i.is_primary_key, 0) 'PrimaryKey' FROM sys.columns c INNER JOIN sys.types t ON c.system_type_id = t.system_type_id" + " LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id" + " LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id" + " WHERE c.object_id = OBJECT_ID('" + name + "')";
            SqlDataAdapter da = new SqlDataAdapter( xsql, cn );
            DataSet ds = new DataSet( name );
            dict = new Dictionary<string, columns>();
            try
            {
                da.Fill( ds, name );
            }
            catch
            {
                return;
            }

            this.Name = name;
            int c = 0;
            foreach (DataRow dr in ds.Tables[ 0 ].Rows)
            {
                if (!dict.ContainsKey( dr[ "ColumnName" ].ToString() ) && dr[ "ColumnName" ] != "upsize_ts")
                {
                    Array.Resize( ref Cols, c + 1 );
                    Cols[ c ] = new columns( (System.String) dr["ColumnName" ], dr[ "DataType" ].ToString(), (System.Boolean) dr[ "PrimaryKey" ], (System.Boolean) dr[ "PrimaryKey" ], short.Parse( dr[ "MaxLength" ].ToString()) );
                    dict.Add( dr[ "ColumnName" ].ToString(), Cols[c] );
                    c++;
                }
            }

        }
        public string getName()
        {
            return this.Name;
        }
        public columns[] getCols()
        {
            return Cols;
        }
        public columns getCol( int index )
        {
            return Cols[ index ];
        }

        public columns getColByName(string name)
        {
            if (dict.ContainsKey( name ))
                return dict[ name ];
            else
                return null;
        }

    }

    public class columns
    {
        private string Name;
        private string dbType;
        private bool isKey = false;
        private bool isAutoNumber = false;

        private int columnSize = 0;

        public columns()
        {
        }

        protected internal columns( string name, string type, bool isKey, bool isAutoNumber, int columnSize )
        {
            this.Name = name;
            this.dbType = type;
            this.isKey = isKey;
            this.isAutoNumber = isAutoNumber;
            this.columnSize = columnSize;
        }

        public void setName( string name )
        {
            this.Name = name;
        }
        public string getName()
        {
            return this.Name;
        }
        public void setDbType( string type )
        {
            this.dbType = type;
        }
        public string getDbType()
        {
            return this.dbType;
        }
        public void setIsKey( bool isKey )
        {
            this.isKey = isKey;
        }
        public bool getIsKey()
        {
            return this.isKey;
        }
        public void setIsAutoNumber( bool isAutoNumber )
        {
            this.isAutoNumber = isAutoNumber;
        }
        public bool getIsAutoNumber()
        {
            return this.isAutoNumber;
        }
        public void setColumnSize( int value )
        {
            this.columnSize = value;
        }
        public int getColumnSize()
        {
            return this.columnSize;
        }

    }
}
