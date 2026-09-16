using System.Data;
using System.Windows.Forms;
using System;

namespace MbcsCentralLogCleaner
{
    public class clsDB : IDisposable
    {
        LogClass Log = GlobalShared.Log;

        protected const string DS_DATA_SET = "MbcsCentralLogCleaner";
        protected string loadStmt;
        public string LoadStmt { set { loadStmt = value; } }
        protected string loadAllStmt;
        public string LoadAllStmt { set { loadAllStmt = value; } }
        protected string insertStmt;
        public string InsertStmt { set { insertStmt = value; } }
        protected string updateStmt;
        public string UpdateStmt { set { updateStmt = value; } }
        protected string localTable;
        public string LocalTable { set { localTable = value; } }
        protected string priKeyName;
        public string PriKeyName { set { priKeyName = value; } }
        protected System.Data.SqlClient.SqlConnection localCN;
        // Deprecated?
        protected string connectionString;
        public string ConnectionString { set { connectionString = value; } }

        protected string SQL_DELETE;

        public clsDB() { }

        public clsDB(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.SqlClient.SqlConnection cn)
        {
            SQL_DELETE = "DELETE FROM " + localTable + " WHERE " + priKeyName + " = ?;";


            try
            {
                this.localTable = table;
                this.priKeyName = priKeyName;
                this.loadStmt = loadStmt;
                this.loadAllStmt = loadAllStmt;
                this.insertStmt = insertStmt;
                this.updateStmt = updateStmt;
                this.localCN = cn;
                this.connectionString = cn.ConnectionString;
                SQL_DELETE = "DELETE FROM " + localTable + " WHERE [" + priKeyName + "] = ?;";
            }
            catch (System.Exception)
            {
                return;
            }
        }

        public void Dispose()
        {

        }
        protected static string prepareStatement(string sql, string[] options)
        {
            string s = "";
            int i;
            int n = 0;

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
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.ConnectionString);
                DataSet ds = new DataSet(DS_DATA_SET);
                da.Fill(ds, DS_DATA_SET);

                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while retrieving record from " + localTable + ": " + ex.Message);
                return null;
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieveing record from " + localTable + ": " + ex.Message, false);
                return null;
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

        }

        protected DataSet loadAll()
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
                //localCN.ConnectionString = connectionString;
                localCN.ConnectionString = localCN.ConnectionString.Replace("mbartmon;", "mbartmon; Password=Vietnam65;");
                localCN.ConnectionString = localCN.ConnectionString.Replace("*", ",1433");
                if (!localCN.ConnectionString.Contains(",1433"))
                    localCN.ConnectionString = localCN.ConnectionString.Replace(";Initial", ",1433;Initial");
                Log.Log((int)LogClass.logType.Info, "loadAll: ", false);
                Log.Log((int)LogClass.logType.Info, "         " + localCN.ConnectionString, false);
                Log.Log((int)LogClass.logType.Info, "            " + loadAllStmt, false);
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(loadAllStmt, this.localCN); // GlobalShared.ConnectionString);
                DataSet ds = new DataSet(DS_DATA_SET);
                da.Fill(ds, DS_DATA_SET);
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false);
                return null;
            }
            catch (System.Exception ex)
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false);
            }
            return null;

        }

        protected DataSet load(string stmt, string[] options)
        {

            if (options != null)
            {
                if (options.Length > 0 && options[0].Trim() != "")
                {
                    stmt = prepareStatement(stmt, options);
                    //MessageBox.Show(stmt + " : " + options[0] + " " + options[1]);

                }
            }
            DataSet ds = null;
            System.Data.SqlClient.SqlDataAdapter da = null;
            try
            {

                //MessageBox.Show("1 -  Connection String: " + GlobalShared.ConnectionString);
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                //MessageBox.Show("2 -  Connection String: " + GlobalShared.ConnectionString);
                da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.ConnectionString);
                ds = new DataSet(DS_DATA_SET);
            }
            catch (Exception qex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + qex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);
                return null;
            }

            try
            {
                da.Fill(ds, DS_DATA_SET);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + ex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);
                return null;
            }
            return ds;

        }

        protected bool updateRecord(string key, string[] values)
        {
            try
            {
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.ConnectionString);
                    localCN.Open();
                }
                catch { }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.updateStmt, values);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while updating record: " + ex.Message);
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

        protected int insertRecord(string[] values)
        {
            int id = -1;
            try
            {
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.ConnectionString);
                    localCN.Open();
                }
                catch (System.Exception )
                {

                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.insertStmt, values);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity";
                id = System.Convert.ToInt32(cmd.ExecuteScalar());
                //id = 0
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.ToLower().IndexOf("duplicate") + 1 > 0)
                {
                    MessageBox.Show("Attempt to add a duplicate record.");
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

        public bool delete(string key)
        {

            try
            {
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.ConnectionString);
                localCN.Open();
                string[] values = new string[] { key };
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

        protected long queryWithCount(string stmt, string[] options)
        {
            long count = -1;

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.ConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
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

        public static DataSet Query(string stmt, System.Data.SqlClient.SqlConnection localCN)
        {

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.ConnectionString);
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
            catch (System.Data.SqlClient.SqlException)
            {
                return null;
            }
            return ds;

        }

        public System.Data.SqlClient.SqlConnection LocalCN
        {
            get { return localCN; }
            set
            {
                localCN = value;
                this.connectionString = LocalCN.ConnectionString;
            }
        }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

    }
}