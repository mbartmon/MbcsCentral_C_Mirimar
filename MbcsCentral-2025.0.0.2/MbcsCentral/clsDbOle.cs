using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MbcsCentral
{

	/// <summary>
	/// Used for interfacing with MS Access Databases
	/// </summary>
	public class clsDbOle
	{

		protected const string DS_DATA_SET = "wintrm";
		protected string loadStmt;
		protected string loadAllStmt;
		protected string insertStmt;
		protected string updateStmt;
		protected string localTable;
		protected string priKeyName;

		protected System.Data.OleDb.OleDbConnection localCN;

        protected string SQL_DELETE;

		public clsDbOle(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.OleDb.OleDbConnection cn)
		{
			this.localTable = table;
			this.priKeyName = priKeyName;
			this.loadStmt = loadStmt;
			this.loadAllStmt = loadAllStmt;
			this.insertStmt = insertStmt;
			this.updateStmt = updateStmt;
			this.localCN = cn;
			SQL_DELETE = "DELETE FROM " + localTable + " WHERE " + priKeyName + " = ?;";
		}

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

        protected DataSet loadByPrimaryKey(string key)
		{

			string[] options = new string[2];
			options[0] = key;
			try {
				localCN.Open();
				string stmt = prepareStatement(loadStmt, options);
                System.Data.OleDb.
				OleDbDataAdapter da = new OleDbDataAdapter(stmt, localCN.ConnectionString);
				DataSet ds = new DataSet(DS_DATA_SET);
				da.Fill(ds, DS_DATA_SET);

				return ds;
			} catch (OleDbException ex) {
				MessageBox.Show("Error while retrieving record from " + localTable + ": " + ex.Message);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieveing record from " + localTable + ": " + ex.Message, false);
				return null;
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}

		}

		protected DataSet loadAll()
		{

			if (localCN.State != ConnectionState.Open) {
				try {
					localCN.Open();
				} catch (OleDbException ex) {
					GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">\r\n   " + loadAllStmt + "\n\r   " + ex.Message, false);
					return null;
				} catch (System.Exception ex) {
					GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">\r\n   " + loadAllStmt + "\n\r   " + ex.Message, false);
					return null;
				}
			}
			try {
				OleDbDataAdapter da = new OleDbDataAdapter(loadAllStmt, localCN.ConnectionString);
				DataSet ds = new DataSet(DS_DATA_SET);
				da.Fill(ds, DS_DATA_SET);
				return ds;
			} catch (OleDbException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false);
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}
			return null;

		}

		protected DataSet load(string stmt, string[] options)
		{

			if ((options != null)) {
				if (options[0] != null && !options[0].Trim().Equals("")) {
					stmt = prepareStatement(stmt, options);
				}
			}
			if (localCN.State != ConnectionState.Open) {
                try
                {
                    localCN.Open();
				} catch (System.Exception ex) {
					return null;
				}
            }
            OleDbDataAdapter da = new OleDbDataAdapter(stmt, localCN.ConnectionString);
			DataSet ds = new DataSet(DS_DATA_SET) { EnforceConstraints = false } ;
			try {
                da.Fill(ds, DS_DATA_SET);
                try {
					localCN.Close();
				} catch {
				}
			} catch (OleDbException ex) {
				return null;
			}
            return ds;

        }
        protected int loadDocId(string stmt, string[] options)
        {
			int docid = 0;

            if ((options != null))
            {
                if (options[0] != null && !options[0].Trim().Equals(""))
                {
                    stmt = prepareStatement(stmt, options);
                }
            }
            if (localCN.State != ConnectionState.Open)
            {
                try
                {
                    localCN.Open();
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }

			OleDbCommand cmd = new OleDbCommand(stmt, localCN);
			GlobalShared.Log.Log((int)LogClass.logType.Info, "1 - Reading doc", false);
			OleDbDataReader rdr = cmd.ExecuteReader();
			GlobalShared.Log.Log((int)LogClass.logType.Info, "2 - Read doc", false);
			if (rdr.HasRows)
            {
				rdr.Read();
				docid = rdr.GetInt32(0);
            }
			try
            {
				localCN.Close();

            }
			catch { }
			return docid;
        }

        protected bool updateRecord(string key, string[] values)
		{
			try {
				localCN.Open();
				OleDbCommand cmd = new OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Connection = localCN;
				cmd.CommandText = prepareStatement(this.updateStmt, values);
				cmd.ExecuteNonQuery();
			} catch (OleDbException ex) {
				MessageBox.Show("Error while updating record: " + ex.Message);
				return false;
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}
			return true;
		}

		protected int insertRecord(string[] values)
		{
			int id = -1;
            try {
                localCN.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(loadAllStmt, localCN.ConnectionString);
                DataSet ds = new DataSet(DS_DATA_SET);
                da.Fill(ds, DS_DATA_SET);

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.insertStmt, values);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity";
                id = (int)(cmd.ExecuteScalar());
            }
            catch (OleDbException ex)
            {
                if (ex.Message.ToLower().Contains("duplicate"))
                {
                    MessageBox.Show("Attempt to add a duplicate record.");
                } else {
                    MessageBox.Show("Error while inserting record into " + localTable + ": " + ex.Message);
                }
                return id;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error while inserting record into " + localTable + ": " + ex.Message);
                return id;
			} finally {
				try 
                {
					localCN.Close();
				} catch {}
			}
			return id;
		}

		public bool delete(string key)
		{

			try {
				localCN.Open();
				string[] values = { key };
				OleDbCommand cmd = new OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Connection = localCN;
				cmd.CommandText = prepareStatement(SQL_DELETE, values);
				cmd.ExecuteNonQuery();
			} catch (OleDbException ex) {
				MessageBox.Show("Error while deleting record: " + ex.Message);
				return false;
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}
			return true;

		}
        public static DataSet Query(string stmt, System.Data.OleDb.OleDbConnection localCN)
        {

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(stmt, localCN.ConnectionString);
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
            catch (System.Data.OleDb.OleDbException ex)
            {
                return null;
            }
            return ds;

        }

		public static bool QueryNoReturn(string stmt, System.Data.OleDb.OleDbConnection localCN)
        {
            try
            {
                localCN.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
				cmd.CommandText = stmt;
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Error while retrieving record: " + ex.Message);
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
    }
}

