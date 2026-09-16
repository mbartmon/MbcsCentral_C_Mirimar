using System.Data;
using System.Data.OleDb;
namespace WinTRM
{

	static class DButils
	{


		static OleDbCommand cmdobj = new OleDbCommand();

		static OleDbConnection CNN;
		public static bool dbFieldAppend(OleDbConnection CNN, string table, string field, string fldType, long fldLength, object value)
		{

			CNN.Open();
			cmdobj.Connection = CNN;
			cmdobj.CommandType = CommandType.Text;
			cmdobj.CommandText = "ALTER TABLE " + table + " ADD " + field + " " + fldType + (fldLength > 0 ? " " + fldLength : "").ToString + ";";
			try {
				cmdobj.ExecuteNonQuery();
			} catch (OleDbException ex) {
				return false;
			} finally {
				CNN.Close();
			}

			if ((value != null)) {
				CNN.Open();
				cmdobj.CommandText = "UPDATE " + table + " SET " + field + " =" + (value.GetType.Equals("String") ? "'" : "").ToString + value.ToString + (value.GetType.Equals("System.String") ? "'" : "").ToString;
				try {
					cmdobj.ExecuteNonQuery();
				} catch (OleDbException ex) {
					return false;
				} finally {
					CNN.Close();
				}
			}

			return true;

		}


		private static void dbFieldChange(string dbConnectionString, string table, string field, string fldType, long fldLength)
		{
		}

	}
}
