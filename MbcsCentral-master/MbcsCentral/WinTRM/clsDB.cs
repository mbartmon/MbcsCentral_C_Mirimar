using System.Data;
namespace WinTRM
{

	public class clsDB
	{

		protected const string DS_DATA_SET = "wintrm";
		protected string loadStmt;
		protected string loadAllStmt;
		protected string insertStmt;
		protected string updateStmt;
		protected string localTable;
		protected string priKeyName;

		protected System.Data.OleDb.OleDbConnection localCN;

		protected string SQL_DELETE = "DELETE FROM " + localTable + " WHERE " + priKeyName + " = ?;";

		public clsDB(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.OleDb.OleDbConnection cn)
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
			int i = 0;
			int n = 0;

			if (options == null | Information.UBound(options, 1) < 0) {
				s = sql;
			} else {
				for (i = 1; i <= Strings.Len(sql); i++) {
					if (Strings.Mid(sql, i, 1) == "?") {
						if (n <= Information.UBound(options, 1)) {
							s += options(n);
							n += 1;

						} else {
						}
					} else {
						s += Strings.Mid(sql, i, 1);
					}
				}
			}
			return s;

		}

		protected DataSet loadByPrimaryKey(string key)
		{

			string[] options = new string[2];
			options(0) = key;
			try {
				localCN.Open();
				string stmt = prepareStatement(loadStmt, options);
				OleDb.OleDbDataAdapter da = new OleDb.OleDbDataAdapter(stmt, localCN.ConnectionString);
				DataSet ds = new DataSet(DS_DATA_SET);
				da.Fill(ds, DS_DATA_SET);

				return ds;
			} catch (OleDb.OleDbException ex) {
				Interaction.MsgBox("Error while retrieving record from " + localTable + ": " + ex.Message);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error while retrieveing record from " + localTable + ": " + ex.Message);
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
				} catch (OleDb.OleDbException ex) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + Constants.vbCrLf + "   " + loadAllStmt + Constants.vbCrLf + "   " + ex.Message, false, MsgBoxStyle.OkOnly);
					return null;
				} catch (System.Exception ex) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + Constants.vbCrLf + "   " + loadAllStmt + Constants.vbCrLf + "   " + ex.Message);
					return null;
				}
			}
			try {
				OleDb.OleDbDataAdapter da = new OleDb.OleDbDataAdapter(loadAllStmt, localCN.ConnectionString);
				DataSet ds = new DataSet(DS_DATA_SET);
				da.Fill(ds, DS_DATA_SET);
				return ds;
			} catch (OleDb.OleDbException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MsgBoxStyle.OkOnly);
				return null;
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MsgBoxStyle.OkOnly);
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
				if (!string.IsNullOrEmpty(Strings.Trim(options(0)))) {
					stmt = prepareStatement(stmt, options);
				}
			}
			if (localCN.State != ConnectionState.Open) {
				try {
					localCN.Open();
				} catch (SystemException ex) {
					return null;
				}
			}
			OleDb.OleDbDataAdapter da = new OleDb.OleDbDataAdapter(stmt, localCN.ConnectionString);
			DataSet ds = new DataSet(DS_DATA_SET);
			try {
				da.Fill(ds, DS_DATA_SET);
				try {
					localCN.Close();
				} catch {
				}
			} catch (OleDb.OleDbException ex) {
				return null;
			}
			return ds;

		}

		protected bool updateRecord(string key, string[] values)
		{
			try {
				localCN.Open();
				OleDb.OleDbCommand cmd = new OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Connection = localCN;
				cmd.CommandText = prepareStatement(this.updateStmt, values);
				cmd.ExecuteNonQuery();
			} catch (OleDb.OleDbException ex) {
				Interaction.MsgBox("Error while updating record: " + ex.Message);
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
				OleDb.OleDbDataAdapter da = new OleDb.OleDbDataAdapter(loadAllStmt, localCN.ConnectionString);
				DataSet ds = new DataSet(DS_DATA_SET);
				da.Fill(ds, DS_DATA_SET);
				//Dim cmd As New OleDb.OleDbCommandBuilder(da)
				//cmd.QuotePrefix = "[" : cmd.QuoteSuffix = "]"
				//da.InsertCommand = cmd.getinsertcommand

				OleDb.OleDbCommand cmd = new OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Connection = localCN;
				cmd.CommandText = prepareStatement(this.insertStmt, values);
				//Dim parm As OleDb.OleDbParameter = New OleDb.OleDbParameter("@newID", OleDb.OleDbType.Integer, 4, priKeyName)
				//parm.Direction = ParameterDirection.Output
				//cmd.Parameters.Add(parm)
				cmd.ExecuteNonQuery();
				cmd.CommandText = "SELECT @@Identity";
				id = Convert.ToInt64(cmd.ExecuteScalar());
				//id = 0
			} catch (OleDb.OleDbException ex) {
				if (Strings.InStr(Strings.LCase(ex.Message), "duplicate") > 0) {
					Interaction.MsgBox("Attempt to add a duplicate record.");
				} else {
					Interaction.MsgBox("Error while inserting record into " + localTable + ": " + ex.Message);
				}
				return id;
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}
			return id;
		}

		public bool delete(string key)
		{

			try {
				localCN.Open();
				string[] values = { key };
				OleDb.OleDbCommand cmd = new OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Connection = localCN;
				cmd.CommandText = prepareStatement(SQL_DELETE, values);
				cmd.ExecuteNonQuery();
			} catch (OleDb.OleDbException ex) {
				Interaction.MsgBox("Error while deleting record: " + ex.Message);
				return false;
			} finally {
				try {
					localCN.Close();
				} catch {
				}
			}
			return true;

		}
	}
}
