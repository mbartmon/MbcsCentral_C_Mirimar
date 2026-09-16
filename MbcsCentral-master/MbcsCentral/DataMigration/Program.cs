using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace MbcsCentral
{
    public class Program
    {
        public static string TABLE_FILE = "WinpmTables.xml";
        public static string sqlConnectionString = "Data Source=MIKE-PC\\SQLEXPRESSMBCS;Initial Catalog=MbcsCentral;Integrated Security=SSPI;";
        public static string accessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=<database>";
        private static OleDbConnection oleDb = null;
        private static SqlConnection sqlDb = null;
        public static long clientId = 1;
        public static bool deleteFirst = false;
        public static ObservableCollection<ProcessTables> tables;
        public static frmProgress frm;
        public static string m_AccessConnectionString;

        public static void importTables()
        {
            string sourceTable = "";
            string targetTable = "";
            string converter = "";
            bool identity = false;

            frm = new frmProgress();
            frm.Show();
            frm.write("Starting Data Conversion");
            frm.write("========================");
            frm.write("Access Database: " + m_AccessConnectionString);
            frm.write("MS SQL Database:" + sqlConnectionString);
            frm.write("");

            foreach (ProcessTables tbl in tables)
            {
                if (tbl.IsChecked)
                {
                    frm.write("Input Table: " + tbl.SourceTable);
                    frm.write("Output Table: " + tbl.TargetTable);
                    convertTable(tbl.SourceTable, tbl.TargetTable, tbl.Converter, tbl.Identity);
                    sourceTable = "";
                    targetTable = "";
                }
            }
        }

        static void convertTable(string sourceTable, string targetTable, string converter, bool identity)
        {
            long rowCount = 0;

            try
            {
                oleDb = new OleDbConnection(m_AccessConnectionString);
                oleDb.Open();
                frm.write("   Input Table Open");
            }
            catch (OleDbException ex)
            {
                frm.write("   Input Table: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                frm.write("   Input Table: " + ex.Message);
                return;
            }

            try
            {
                sqlDb = new SqlConnection(sqlConnectionString);
                sqlDb.Open();
                frm.write("   Output Table Open");
            }
            catch (SqlException ex)
            {
                frm.write("   Output Table: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                frm.write("   Output Table: " + ex.Message);
                return;
            }

            Table inTable = new Table(sourceTable, oleDb);
            if (inTable == null)
            {
                frm.write("   Unable to open InPut Table");
                return;
            }
            Table outTable = new Table(targetTable, sqlDb);
            if (outTable == null)
            {
                frm.write("   Unable to open OutputTable");
                return;
            }
            string sql = "SELECT * FROM " + inTable.getName();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, oleDb);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlDb;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                // Delete first?
                if (deleteFirst)
                {
                    frm.write("   Deleting existing rows.");
                    cmd.CommandText = "DELETE FROM " + outTable.getName() + " WHERE clientId = " + clientId.ToString();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        frm.write("    could not delete existing records. " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        frm.write("    could not delete existing records. " + ex.Message);
                    }
                }

                if (identity)
                {
                    cmd.CommandText = "SET IDENTITY_INSERT " + outTable.getName() + " ON";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        frm.write("   could not SET_INDENTITY ON" + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        frm.write("   could not SET_INDENTITY ON" + ex.Message);
                    }
                }
                string colGroup = " (";
                string valGroup = " VALUES (";
                frm.write("   Converting Rows");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    convertRow(dr, inTable, outTable, ref rowCount, colGroup, valGroup, converter);
                }

                if (identity)
                {
                    cmd.CommandText = "SET IDENTITY_INSERT " + outTable.getName() + " OFF";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        frm.write("   could not SET_INDENTITY OFF" + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        frm.write("   could not SET_INDENTITY OFF" + ex.Message);
                    }
                }

                frm.write("   " + rowCount.ToString() + " rows converted", false);
                ds.Dispose();
                da.Dispose();
                try
                {
                    oleDb.Close();
                    frm.write("   Input Table Closed");
                }
                catch (OleDbException ex)
                {
                    frm.write("   closing Input Table: " + ex.Message);
                }
                try
                {
                    sqlDb.Close();
                    frm.write("   Output Table Closed");
                }
                catch (SqlException ex)
                {
                    frm.write("   closing Ouput Table: " + ex.Message);
                }
            }
        }

        static void convertRow(DataRow dr, Table inTable, Table outTable, ref long rowCount, string colGroup, string valGroup, string converter)
        {
            columns[] cols = inTable.getCols();
            columns outCol = null;
            string sql = "INSERT INTO " + outTable.getName();
            string x = "";
            bool converted = false;

            foreach (columns col in cols)
            {
                outCol = outTable.getColByName(col.getName());
                if (outCol != null)
                {
                    converted = false;
                    colGroup += "[" + outCol.getName() + "],";
                    x = outCol.getDbType().ToString().ToLower();
                    if (converter != null && converter.Length > 0 && converter.Trim() != "")
                    {
                        converted = Convert(outTable.getName(), col, ref valGroup, dr);
                    }
                    if (!converted)
                    {
                        if (x.Contains("char") || x.Contains("date"))
                            valGroup += "'" + dr[col.getName()].ToString().Replace("'", "").Trim() + "',";
                        else if (x.Contains("bool") || x.Contains("bit"))
                            valGroup += (dr[col.getName()].ToString().ToLower().Contains("true") ? "1," : "0,");
                        else
                        {
                            x = dr[col.getName()].ToString().Replace("'", "").Trim();
                            valGroup += (x == "" || x == null ? "0" : x) + ",";
                        }
                    }
                }
            }
            if (outTable.getColByName("clientId") != null)
            {
                colGroup += "[clientId],";
                valGroup += clientId.ToString() + ",";
            }

            colGroup = colGroup.Substring(0, colGroup.Length - 1) + ")";
            valGroup = valGroup.Substring(0, valGroup.Length - 1) + ")";
            sql += colGroup + valGroup;
            if (rowCount == 0)
            {
                frm.write("      columns=<" + colGroup + ">");
                frm.write("   " + rowCount.ToString() + " rows transferred.");
            }
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlDb;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                rowCount++;
                if (rowCount % 10 == 0)
                    frm.write("   " + rowCount.ToString() + " rows transferred.", false);
            }
            catch (SqlException ex)
            {
                frm.write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                frm.write("   " + rowCount.ToString() + " rows transferred.");
            }
            catch (Exception ex)
            {
                frm.write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                frm.write("   " + rowCount.ToString() + " rows transferred.");
            }
        }

        public static bool Convert(string table, columns col, ref string valGroup, DataRow dr)
        {
            switch  (table)
                {
                }
            return false;
        }

    }
}
