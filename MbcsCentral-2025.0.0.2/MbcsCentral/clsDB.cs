using System.Data;
using System.Windows.Forms;
using MbcsCentral;
using System;
using System.Xml.Serialization;

namespace MbcsCentral
{
    /// <summary>
    /// Base class for all SQL Server Database table classes
    /// </summary>
    public class clsDB
    {
        LogClass Log = LogClass.instance;

        protected static string DS_DATA_SET = "DataSet";
        protected string loadStmt;

        [XmlIgnore]
        public string LoadStmt;
        [XmlIgnore]
        public string loadAllStmt;
        [XmlIgnore]
        public string insertStmt;
        [XmlIgnore]
        public string updateStmt;
        [XmlIgnore]
        public string localTable;
        [XmlIgnore]
        public string priKeyName;
        [XmlIgnore]
        public string connectionString;
        [XmlIgnore]
        public string DataSetName = "DataSet";

        protected System.Data.SqlClient.SqlConnection localCN;
        protected string SQL_DELETE;

        public clsDB() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="table">Database Table Name</param>
        /// <param name="priKeyName">Primary Key Column Name</param>
        /// <param name="loadStmt">SQL Statement for retrieving record by Primary Key</param>
        /// <param name="loadAllStmt">SQL Statement for retrieving all records in Table</param>
        /// <param name="insertStmt">SQL Statement for inserting new records</param>
        /// <param name="updateStmt">SQL Statement for updating a record</param>
        /// <param name="cn">SQL Server connection string</param>
        /// <param name="DataSetName">Optional name to apply to DataSet object</param>
        public clsDB(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.SqlClient.SqlConnection cn, string DataSetName = "DataSet")
        {
            SQL_DELETE = "DELETE FROM " + table + " WHERE " + priKeyName + " = ?;";


            try
            {
                this.DataSetName = DataSetName;
                this.localTable = table;
                this.priKeyName = priKeyName;
                this.loadStmt = loadStmt;
                this.loadAllStmt = loadAllStmt;
                this.insertStmt = insertStmt;
                this.updateStmt = updateStmt;
                //this.localCN = cn;
                this.connectionString = cn.ConnectionString;
                SQL_DELETE = "DELETE FROM " + localTable + " WHERE [" + priKeyName + "] = ?;";
            }
            catch (System.Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Inserts options (parameters) in an SQL string to prepare it for execution.
        /// 
        /// Options are indicated by question marks (? for numeric and '?' for strings) in the raw sql.
        /// </summary>
        /// <param name="sql">Raw SQL string with optional parameter inserts included</param>
        /// <param name="options">Values for insertion in the SQL string. NOTE: the order of passed parameters must match the order of insertion point in the SQL string.</param>
        /// <returns>Prepared SQL string</returns>
        protected static string prepareStatement(string sql, string[] options)
        {
            string s = "";
            int i;
            int n = 0;

            if (sql == null || sql.Length == 0)
                return s;

            if (options == null || (options.Length - 1) < 0)
            {
                s = sql;
            }
            else
            {
                for (i = 1; i <= sql.Length; i++)
                {
                    if (sql.Substring(i - 1, 1) == "?")
                    {
                        if (n <= (options.Length - 1))
                        {
                            if (!(options[n] == null))
                            {
                                if (options[n].ToLower().Equals("true"))
                                {
                                    options[n] = "1";
                                }
                                else if (options[n].ToLower().Equals("false"))
                                {
                                    options[n] = "0";
                                }
                            }
                            s += options[n];
                            n++;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        s += sql.Substring(i - 1, 1);
                    }
                }
            }
            return s;

        }

        /// <summary>
        /// Load a record by Primary Key
        /// </summary>
        /// <param name="key">Primary Key value</param>
        /// <returns>Dataset of retrieved record or null if not found</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected DataSet loadByPrimaryKey(string key)
        {

            string[] options = new string[2];
            options[0] = key;
            try
            {
                //localCN.Open();
                //localCN.ConnectionString = connectionString;
                string stmt = prepareStatement(loadStmt, options);
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.SqlConnectionString);
                //DataSet ds = new DataSet(DS_DATA_SET);
                DataSet ds = new DataSet(DataSetName);

                da.Fill(ds, DS_DATA_SET);
                ColumnCount = ds.Tables[0].Columns.Count;
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while retrieving record from " + localTable + ": " + ex.Message);
                return null;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieveing record from " + localTable + ": " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    if (localCN != null && localCN.State == ConnectionState.Open)
                        localCN.Close();
                }
                catch
                {
                }
            }

        }

        /// <summary>
        /// Retrieve all records in a table
        /// </summary>
        /// <param name="debug">If true, logs extended error message if retrieval fails.</param>
        /// <returns>Dataset of records found or null if nothing found</returns>
        protected DataSet loadAll(bool debug = false)
        {

            /* if (localCN.State != ConnectionState.Open)
            {
                try
                {
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + "\r\n" + "   " + loadAllStmt + "\r\n" + "   " + ex.Message, false, MessageBoxButtons.OK);
                    return null;
                }
                catch (System.Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + "\r\n" + "   " + loadAllStmt + "\r\n" + "   " + ex.Message);
                    return null;
                }
            }   */
            try
            {
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [ConnectionString= " + GlobalShared.SqlConnectionString + "]", false);
                    GlobalShared.Log.debug = false;
                }
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [" + loadAllStmt + "]", false);
                    GlobalShared.Log.debug = false;
                }
                //localCN.ConnectionString = connectionString;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(loadAllStmt, GlobalShared.SqlConnectionString);
                DataSet ds = new DataSet(DataSetName);
                da.Fill(ds, DS_DATA_SET);
                ColumnCount = ds.Tables[0].Columns.Count;
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [ColumnCount= " + ColumnCount.ToString() + "]", false);
                    GlobalShared.Log.debug = false;
                }
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MessageBoxButtons.OK);
                return null;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MessageBoxButtons.OK);
            }
            return null;

        }

        /// <summary>
        /// Retrieve records from a table using the passes SQL string
        /// </summary>
        /// <param name="stmt">SQL string to use</param>
        /// <param name="options">Any parameters needed to prepare the statement</param>
        /// <param name="LogZero">Log if no records found</param>
        /// <returns>Dataset of retrieved records or null if nothing found</returns>
        protected DataSet load(string stmt, string[] options, bool LogZero = false)
        {

            if (options != null)
            {
                if (options.Length > 0 && options[0] != null && options[0].Trim() != "")
                {
                    stmt = prepareStatement(stmt, options);
                }
            }
            //MessageBoxCustom.Show("Query = " + stmt);

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.SqlConnectionString);
            DataSet ds = new DataSet(DS_DATA_SET);
            try
            {
                da.Fill(ds, (localTable == null || localTable.Trim().Equals("") ? "TABLE" : localTable));
                ColumnCount = ds.Tables[0].Columns.Count;
                if (LogZero && (ds == null || ds.Tables[0].Rows.Count == 0))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Warning, "No records returned [clsDb.load(stmt, options)]: columns=" + ColumnCount.ToString() + ", " + stmt, false);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                try
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + ex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);
                }
                catch { }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                try
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + ex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);

                }
                catch { }
                return null;
            }

            return ds;

        }

        /// <summary>
        /// Updates records using update statement passed with child class instantiation
        /// </summary>
        /// <param name="key">Primary Key of record to update</param>
        /// <param name="values">Values for insertion into SQL statement</param>
        /// <param name="tableName">Optional table name for logging</param>
        /// <param name="debug">Optionally include extended error info for logging</param>
        /// <returns>True if successful, False if not</returns>
        protected bool updateRecord(string key, string[] values, string tableName = "", bool debug = false)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Error while openning DB connection: " + ex.Message);
                    return false;
                }
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.updateStmt, values);
                if (!tableName.Trim().Equals(""))
                    GlobalShared.Log.Log((int)LogClass.logType.Info, tableName + ": update <" + cmd.CommandText + ">", false);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while updating record: " + ex.Message + " <" + cmd.CommandText + ">", false);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }

        /// <summary>
        /// Updates records using passed update statement
        /// </summary>
        /// <param name="key">Primary Key of record to be updated</param>
        /// <param name="values">Values for insertion into SQL statement</param>
        /// <param name="updateStatement">Update statement that overrides instantiated one</param>
        /// <param name="tableName">Optional table name for logging</param>
        /// <param name="debug">Optionally include extended error info for logging</param>
        /// <returns>True if successful, False if not</returns>
        protected bool updateRecordSpecial(string key, string[] values, string updateStatement, string tableName = "", bool debug = false)
        {
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Error while openning DB connection: " + ex.Message);
                    return false;
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(updateStatement, values);
                int rc = cmd.ExecuteNonQuery();
                if (!tableName.Trim().Equals("") && debug)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, tableName + ": update <" + cmd.CommandText + ">", false);
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, rc.ToString() + " rows updated.", false);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while updating record: " + ex.Message, false);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }
        /// <summary>
        /// Inserts a new record using instantiated Insert statement
        /// </summary>
        /// <param name="values">Values to be inserted</param>
        /// <returns>Primary Key values of inserted record. -1 if insert failed</returns>
        protected int insertRecord(string[] values)
        {
            int id = -1;
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                    localCN.Open();
                }
                catch (System.Exception ex)
                {

                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.insertStmt, values);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity";
                try
                {
                    id = System.Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (InvalidCastException ex)
                { }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.ToLower().IndexOf("duplicate") + 1 > 0)
                {
                    IsDupe = true;
                    ErrorMessage = ex.Message;
                    //MessageBox.Show("Attempt to add a duplicate record.");
                }
                else
                {
                    MessageBox.Show("Error while inserting record into " + localTable + ": " + ex.Message);
                }
                return id;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return id;
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="key">Primary Key of record to be deleted</param>
        /// <returns>True if successful, False if not</returns>
        public bool delete(string key)
        {

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                localCN.Open();
                string[] values = new string[] { key };

                // Added on 11/08/2018 to capture user infor for pm_tenant deletion
                if (SQL_DELETE.ToLower().Contains("pm_tenants"))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Warning, "Tenant <" + values[0] + "> DELETED by <" + GlobalShared.user.getName + "><" + GlobalShared.user.Account.ToString() + ">", false);
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(SQL_DELETE, values);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while deleting record: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }

        /// <summary>
        /// Execute custom query. Used mainly for special deletion scenarios
        /// </summary>
        /// <param name="stmt">SQL statement of query to be performed</param>
        /// <param name="options">Parameters to be inserted in SQL statement</param>
        /// <returns>True if successful execution, False if not</returns>
        protected bool query(string stmt, string[] options)
        {

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for query <" + stmt + ">: " + ex.Message, true);
                return false;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(stmt, options);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Executing Query <" + stmt + ">: " + ex.Message, true);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch { }
            }

            return true;
        }

        /// <summary>
        /// Custome query for getting record counts
        /// </summary>
        /// <param name="stmt">SQL statement to perform</param>
        /// <param name="options">Values to be inserted in SQL Statement</param>
        /// <returns>Count of records retrieved</returns>
        public long queryWithCount(string stmt, string[] options)
        {
            long count = -1;

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for query <" + stmt + ">: " + ex.Message, true);
                return -1;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(stmt, options);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@ROWCOUNT";
                count = System.Convert.ToInt64(cmd.ExecuteScalar());

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Executing Query <" + stmt + ">: " + ex.Message, true);
                return -1;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch { }
            }

            return count;

        }

        /// <summary>
        /// Performs custom queries using ConnectionString
        /// </summary>
        /// <param name="stmt">SQL Statement to perform</param>
        /// <param name="localCN">Database connection to use</param>
        /// <returns>Dataset of reords retrieved or null if none found</returns>
        public static DataSet Query(string stmt, System.Data.SqlClient.SqlConnection localCN)
        {

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.SqlConnectionString);
            DataSet ds = new DataSet(DS_DATA_SET);
            try
            {
                da.Fill(ds, DS_DATA_SET);
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            return ds;

        }

        /// <summary>
        /// Performs custom queries using SQLConnection
        /// </summary>
        /// <param name="stmt">SQL Statement to perform</param>
        /// <param name="localCN">Database connection to use</param>
        /// <returns>Dataset of reords retrieved or null if none found</returns>

        public static DataSet Query2(string stmt, System.Data.SqlClient.SqlConnection localCN)
        {

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, localCN);
            DataSet ds = new DataSet(DS_DATA_SET);
            try
            {
                da.Fill(ds, DS_DATA_SET);
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            return ds;

        }


        /* Unneeded?
        public System.Data.SqlClient.SqlConnection LocalCN
        {
            get { return localCN; }
            set { localCN = value;
                try
                {
                    this.connectionString = LocalCN.ConnectionString;
                } catch { }
            }
        }
        */

        /// <summary>
        /// Generic method for getting values of property by names. Useful when iterating through columns in a record
        /// </summary>
        /// <param name="propertyName">Name of Property value to be retrieved</param>
        /// <returns>Property Value</returns>
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        /// <summary>
        /// Finds if specified Column name is in table. Datarows must have already been fetched.
        /// </summary>
        /// <param name="name">Column name to find</param>
        /// <returns>Datarow if column name found, null if not</returns>
        public System.Data.DataRow GetColumn(string name)
        {
            DataTable table = GetTableSchema();
            foreach (DataRow dr in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (dr[col].ToString().ToLower().Equals(name.ToLower()))
                    {
                        return dr;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Gets a table schema
        /// </summary>
        /// <returns>DataTable with schema</returns>
        public DataTable GetTableSchema()
        {
            System.Data.SqlClient.SqlCommand cmd;
            System.Data.SqlClient.SqlDataReader reader;
            DataTable schema;
            string query;

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.SqlConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for <GetTableSchema>: " + ex.Message, true);
                return null;
            }
            query = String.Format(System.Globalization.CultureInfo.InvariantCulture, "SELECT * FROM {0}", this.localTable);
            cmd = new System.Data.SqlClient.SqlCommand(GlobalShared.SqlConnectionString);
            cmd.CommandText = query;
            cmd.Connection = localCN;
            reader = cmd.ExecuteReader();

            schema = reader.GetSchemaTable();

            foreach (System.Data.DataRow row in schema.Rows)
            {
                foreach (System.Data.DataColumn col in schema.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }

            try { localCN.Close(); } catch { }
            return schema;
        }
        /// <summary>
        /// Set true when a duplicate record is found on insertion
        /// </summary>
        public bool IsDupe { get; set; } = false;
        /// <summary>
        /// Reports column count
        /// </summary>
        public int ColumnCount { get; set; }
        /// <summary>
        /// Used to retrieve internal error messages
        /// </summary>
        public string ErrorMessage { get; set; } = "";

    }
}