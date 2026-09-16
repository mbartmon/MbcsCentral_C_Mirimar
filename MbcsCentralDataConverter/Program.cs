
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using MbcsCentral;

namespace MbcsCentral
{
    class Program
    {
        private static string TABLE_FILE = "WinpmTables.xml";
        private static string sqlConnectionString = "Data Source=MIKE-PC\\SQLEXPRESSMBCS;Initial Catalog=MbcsCentral;Integrated Security=SSPI;";
        private static string accessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\\pmsys\\winpm.mdb";
        private static frmProgress frm = new frmProgress();
        private static string appPath;
        private static OleDbConnection oleDb = null;
        private static SqlConnection sqlDb = null;
        private static long clientId = 1;
        private static bool deleteFirst = false;

        static void Main( string[] args )
        {
            if (args.Length >= 2)
            {
                if (args[0].Trim() != "*")
                    accessConnectionString = args[0].ToString();
                sqlConnectionString = args[1].ToString();
                if (args.Length >= 3)
                    clientId = long.Parse( args[2] );
                if (args.Length >= 4)
                    deleteFirst = bool.Parse( args[3] );
                if (args.Length >= 5)
                    TABLE_FILE = args[4];
            }
            appPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //GlobalShared.appPath = (string)app.StartupUri.AbsolutePath;
            if (!appPath.EndsWith( "\\" ))
            {
                appPath += "\\";
            }
            frm.Show();
            importTables();
            frm.Hide();
            frm.ShowDialog();
        }

        static void importTables()
        {
            string sourceTable = "";
            string targetTable = "";
            string converter = "";
            bool identity = false;
    
            frm.write("Starting Data Conversion");
            frm.write("========================");
            frm.write("Access Database: " + accessConnectionString);
            frm.write("MS SQL Database:" + sqlConnectionString);
            frm.write("");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlReader = XmlReader.Create( appPath + TABLE_FILE, settings );
            while (xmlReader.Read())
            {
                if (xmlReader.Name.ToString().Equals( "Table" ) && xmlReader.NodeType.ToString().Equals( "Element" ))
                {
                    identity = false;
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            if (xmlReader.Name.Equals( "Source" ))
                                sourceTable = (string) xmlReader.Value;
                            if (xmlReader.Name.Equals( "Target" ))
                                targetTable = (string) xmlReader.Value;
                            if (xmlReader.Name.Equals( "Converter" ))
                                converter = (string) xmlReader.Value;
                            if (xmlReader.Name.Equals( "Identity" ))
                                identity = Boolean.Parse( xmlReader.Value );
                        }
                    }
                    if (!sourceTable.Trim().Equals( "" ) && !targetTable.Trim().Equals( "" ))
                    {
                        frm.write( "Input Table: " + sourceTable );
                        frm.write( "Output Table: " + targetTable );
                        convertTable( sourceTable, targetTable, converter, identity );
                        sourceTable = "";
                        targetTable = "";
                    }
                }
            }
        }

        static void convertTable(string sourceTable, string targetTable, string converter, bool identity)
        {
            long rowCount = 0;

            try
            {
                oleDb = new OleDbConnection( accessConnectionString );
                oleDb.Open();
                frm.write( "   Input Table Open" );
            } catch (OleDbException ex) {
                frm.write("   Input Table: " +ex.Message);
                return;
            }
            catch (Exception ex) {
                 frm.write("   Input Table: " +ex.Message);
                 return;
            }

            try
            {
                sqlDb = new SqlConnection( sqlConnectionString );
                sqlDb.Open();
                frm.write( "   Output Table Open" );
            }
            catch (SqlException ex)
            {
                frm.write("   Output Table: " +ex.Message);
                return;
            }
            catch (Exception ex)
            {
                frm.write("   Output Table: " +ex.Message);
                return;
            }

            Table inTable = new Table( sourceTable, oleDb );
            if (inTable == null)
            {
                frm.write("   Unable to open InPut Table");
                return;
            }
            Table outTable = new Table( targetTable, sqlDb );
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
                    frm.write( "   Deleting existing rows." );
                    cmd.CommandText = "DELETE FROM " + outTable.getName() + " WHERE clientId = " + clientId.ToString();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        frm.write( "    could not delete existing records. " + ex.Message );
                    }
                    catch (Exception ex)
                    {
                        frm.write( "    could not delete existing records. " + ex.Message );
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
                        frm.write( "   could not SET_INDENTITY ON" + ex.Message );
                    }
                    catch (Exception ex)
                    {
                        frm.write( "   could not SET_INDENTITY ON" + ex.Message );
                    }
                }
                string colGroup = " (";
                string valGroup = " VALUES (";
                frm.write( "   Converting Rows" );
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    convertRow(dr,inTable,outTable, ref rowCount,colGroup,valGroup);
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
                        frm.write( "   could not SET_INDENTITY OFF" + ex.Message );
                    }
                    catch (Exception ex)
                    {
                        frm.write( "   could not SET_INDENTITY OFF" + ex.Message );
                    }
                }

                frm.write("   " + rowCount.ToString() + " rows converted",false);
                ds.Dispose();
                da.Dispose();
                try
                {
                    oleDb.Close();
                    frm.write("   Input Table Closed");
                }
                catch (OleDbException ex) {
                    frm.write("   closing Input Table: " + ex.Message);
                }
                try
                {
                    sqlDb.Close();
                    frm.write("   Output Table Closed");
                }
                catch (SqlException ex) {
                    frm.write("   closing Ouput Table: " + ex.Message);
                }
            }
        }

        static void convertRow( DataRow dr, Table inTable, Table outTable, ref long rowCount ,string colGroup, string valGroup)
        {
            columns[] cols = inTable.getCols();
            columns outCol = null;
            string sql = "INSERT INTO " + outTable.getName();
            string x = "";

            foreach (columns col in cols)
            {
                outCol = outTable.getColByName( col.getName() );
                if (outCol != null)
                {
                    colGroup += "[" + outCol.getName() + "],";
                    x = outCol.getDbType().ToString().ToLower();
                    if (x.Contains( "char" ) || x.Contains( "date" ))
                        valGroup += "'" + dr[col.getName()].ToString().Replace("'","").Trim() + "',";
                    else if (x.Contains( "bool" ) || x.Contains( "bit" ))
                        valGroup += (dr[col.getName()].ToString().ToLower().Contains( "true" ) ? "1," : "0,");
                    else
                    {
                        x= dr[col.getName()].ToString().Replace( "'", "" ).Trim();
                        valGroup += (x == "" || x == null ? "0" : x) + ",";
                    }
                }
            }
            if (outTable.getColByName( "clientId" ) != null)
            {
                colGroup += "[clientId],";
                valGroup += clientId.ToString() + ",";
            }

            colGroup = colGroup.Substring( 0, colGroup.Length - 1 ) + ")";
            valGroup = valGroup.Substring( 0, valGroup.Length - 1 ) + ")";
            sql += colGroup + valGroup;
            if (rowCount == 0)
            {
                frm.write( "      columns=<" + colGroup + ">" );
                frm.write( "   " + rowCount.ToString() + " rows transferred." );
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
                    frm.write( "   " + rowCount.ToString() + " rows transferred.", false );
            }
            catch (SqlException ex)
            {
                frm.write( "      Row after " + rowCount.ToString() + ": " + ex.Message );
                frm.write( "   " + rowCount.ToString() + " rows transferred." );
            }
            catch (Exception ex)
            {
                frm.write( "      Row after " + rowCount.ToString() + ": " + ex.Message );
                frm.write( "   " + rowCount.ToString() + " rows transferred." );
            }
        }
    }
}
