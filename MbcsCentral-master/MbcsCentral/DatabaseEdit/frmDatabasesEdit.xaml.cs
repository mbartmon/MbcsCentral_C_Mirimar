using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;


/*
 * Configuration file usage:
 * 
 * baseServer       Production SQL Server
 * baseCatalog      Production Database
 * intialCatalog    Current Database
 * databaseSource   Current SQL Server
 * 
 */

namespace MbcsCentral
{
    /// <summary>
    /// Provides the ability to edit, copy, delete, selection and other operation on the SQL server
    /// </summary>
    public partial class frmDatabasesEdit : Window
    {
        private const string BASEDATABASE = "initialCatalog";
        private const string BASECATALOG = "baseCatalog";
        private string BaseServer;
        private string BaseDatabase;
        private Databases[] DBsources;
        private Dictionary<string, string> parms;
        private Registry[] folders;

        private string m_ThisApp;

        public frmDatabasesEdit()
        {
            InitializeComponent();
        }

        public frmDatabasesEdit(string thisapp)
        {
            m_ThisApp = thisapp;
            InitializeComponent();
        }

       /// <summary>
       /// Sets up the DatabaseEdit function
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void formDatabaseEdit_Loaded(object sender, RoutedEventArgs e)
        {
            object obj = null;

            Main.getForm(this);
            Main.parms.getItem(GlobalShared.CONFIG_BASE_SERVER, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Server.  Cannot create database.");
                return;
            }
            BaseServer = ((string)obj).Replace("**", ",1434").Replace("*", ",1433");
            Main.parms.getItem(GlobalShared.CONFIG_BASE_CATALOG, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Catalog.  Cannot create database.");
                return;
            }
            folders = Main.RetrieveDbFolders();
            BaseDatabase = (string)obj;
            string server = GlobalShared.DatabaseSource;
            int position = 0;
            string dbMachine = server.Substring(0, position);
            string dbSource = (dbMachine.Trim().Equals("") || dbMachine.Contains("*") ? "" : dbMachine + "\\") + server.Substring(position);


            SqlConnection conn = new SqlConnection(GlobalShared.SqlBaseConnectionString.Replace("<source>", dbSource).
                                                                        Replace("<catalog>", "master").
                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                        Replace("<password>", GlobalShared.DB_PASSWORD));

            DBsources = Databases.LoadDatabases(BASEDATABASE, BaseDatabase, conn);
            lstSources.ItemsSource = DBsources;
            lstTargets.ItemsSource = DBsources;
        }

        /// <summary>
        /// DEPRECATED
        /// </summary>
        private void LoadDictionary_OLD()
        {
            parms = Main.parms.GetDict;
            Dictionary<string, string>.KeyCollection keys = parms.Keys;
            ArrayList DatabaseList = new ArrayList();
            string value = "";
            string[] parts = null;
            bool current = false;

            foreach (string key in keys)
            {
                if (key.StartsWith(BASEDATABASE))
                {
                    value = parms[key];
                    if (value.ToLower().Equals(BaseDatabase.ToLower()))
                        current = true;
                    DatabaseList.Add(new Databases() { Key = key, DisplayName = value, Current = current, FullValue = value });
                    current = false;
                }
            }
            DBsources = (Databases[])DatabaseList.ToArray(typeof(Databases));
        }
        private void formDatabaseEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        /// <summary>
        /// Makes a copy of the current database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCopy_Click(object sender, RoutedEventArgs e)
        {
            Databases source = (Databases)lstSources.SelectedItem;
            Databases target = (Databases)lstTargets.SelectedItem;
            if (source == null)
            {
                MessageBox.Show("No SOURCE database selected.");
                return;
            }
            if (target == null)
            {
                MessageBox.Show("No TARGET database selected.");
                return;
            }

            if (source.DisplayName.ToLower().Equals(target.DisplayName.ToLower()))
            {
                MessageBox.Show("SOURCE and TARGET are the same.  Cannot perform copy.");
                return;
            }
            if (MessageBox.Show("This operation will erase all data in the target database.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            string connectionString = "Server=" + BaseServer + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection sourceConn = new SqlConnection(connectionString);
            try
            {
                sourceConn.Open();
            }
            catch (SqlException ex)
            {
                if (connectionString.Contains(",1433") || connectionString.Contains(",1434"))
                    connectionString = connectionString.Replace(",1433", "").Replace(",1434", "");
                else
                    connectionString = connectionString.Replace(";Initial", ",1433;Initial");
                try
                {
                    sourceConn = new SqlConnection(connectionString);
                }
                catch (SqlException qex)
                {
                    MessageBoxCustom.Show("Error connecting to source DB: " + qex.Message);
                    if (qex.InnerException != null)
                    {
                        MessageBoxCustom.Show("Inner Exception: " + qex.InnerException.Message);
                        return;
                    }

                }
                catch (Exception qex) { }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Unable to open source DB: " + ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBoxCustom.Show("Inner Exception: " + ex.InnerException.Message);
                    return;
                }
                return;
            }
            sourceConn.Close();
            connectionString = "Server=" + BaseServer + ";Initial Catalog=" + target.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(connectionString);
            try
            {
                targetConn.Open();
            }
            catch (SqlException ex)
            {
                if (connectionString.Contains(",1433") || connectionString.Contains(",1434"))
                    connectionString = connectionString.Replace(",1433", "").Replace(",1434", "");
                else
                    connectionString = connectionString.Replace(";Initial", ",1433;Initial");
                try
                {
                    targetConn = new SqlConnection(connectionString);
                }
                catch (SqlException qex)
                {
                    MessageBoxCustom.Show("Error connecting to target DB: " + qex.Message);
                    if (qex.InnerException != null)
                    {
                        MessageBoxCustom.Show("Inner Exception: " + qex.InnerException.Message);
                        return;
                    }

                }
                catch (Exception qex) { }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Unable to open target DB: " + ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBoxCustom.Show("Inner Exception: " + ex.InnerException.Message);
                    return;
                }
                return;
            }
            targetConn.Close();
            if (Main.CreateDatabase(ref targetConn, target.DisplayName, true))
                Main.CopyDatabase(sourceConn, targetConn);
            MessageBox.Show("Database copy complete.");

        }

        /// <summary>
        /// Deletes a selected database. The Production database cannot be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            Databases source = (Databases)lstSources.SelectedItem;
            if (source == null)
            {
                MessageBox.Show("No SOURCE database selected.");
                return;
            }
            if (source.DisplayName.ToLower().Equals(BaseDatabase.ToLower()))
            {
                MessageBox.Show("Cannot delete MAIN database.");
                return;
            }
            if (MessageBox.Show("About to DELETE <" + source.DisplayName + "> and all of its data.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            string server = Main.parms.GetString(GlobalShared.CONFIG_BASE_SERVER);
            server = server.Replace("**", ",1434").Replace("*", ",1433");
            string connectionString = "Server=" + server + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(connectionString);
            if (Main.CreateDatabase(ref targetConn, source.DisplayName, true, true) == false)
            {
                if (MessageBox.Show("Could not delete database <" + source.DisplayName + ">.  Delete list item anyway?","",MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            Main.parms.removeItemStartsWith(BASEDATABASE);

            string[] names = new string[DBsources.Length];
            int j = 0;
            foreach(Databases db in DBsources)
            {
                names[j++] = db.DisplayName;
            }
            Array.Sort(names);
            for (j = 0; j < names.Length; j++)
            {
                if (!names[j].ToLower().Equals(source.DisplayName.ToLower()))
                {
                    if (names[j].ToLower().Equals(BaseDatabase.ToLower()))
                    {
                        Main.parms.setItem(BASEDATABASE, names[j]);
                    }
                    else
                    {
                        Main.parms.setItem(string.Format("{0}{1:##}", BASEDATABASE, ++i), names[j]);
                    }
                }
                else
                    Main.parms.removeItem(source.Key);
            }
            Main.parms.setConfigs();
            Main.parms.getConfigs();
            DBsources = Databases.LoadDatabases(BASEDATABASE, BaseDatabase);
            lstSources.ItemsSource = DBsources;
            lstTargets.ItemsSource = DBsources;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Finds differences in schemas between two databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSchema_Click(object sender, RoutedEventArgs e)
        {
            Databases source = (Databases)lstSources.SelectedItem;
            Databases target = (Databases)lstTargets.SelectedItem;
            string sourceConnectionString;
            string targetConnectionString;

            List<DataColumnCollection> columnMain = new List<DataColumnCollection>();
            List<DataColumnCollection> columnMatch = new List<DataColumnCollection>();

            if (source == null)
            {
                MessageBox.Show("No SOURCE database selected.");
                return;
            }
            if (target == null)
            {
                MessageBox.Show("No TARGET database selected.");
                return;
            }

            if (source.DisplayName.ToLower().Equals(target.DisplayName.ToLower()))
            {
                MessageBox.Show("SOURCE and TARGET are the same.  Cannot perform Schema Match.");
                return;
            }
            sourceConnectionString = "Server=" + BaseServer + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection sourceConn = new SqlConnection(sourceConnectionString);
            try
            {
                sourceConn.Open();
            }
            catch (System.Exception ex)
            {
                if (BaseServer.Contains(",1433"))
                    BaseServer.Replace(",1433", "");
                else
                    BaseServer += ",1433";

                try
                {
                    sourceConnectionString = "Server=" + BaseServer + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
                    sourceConn = new SqlConnection(sourceConnectionString);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Unable to open Source Database: " + ex.Message);
                    return;
                }
            }
            sourceConn.Close();


            targetConnectionString = "Server=" + BaseServer + ";Initial Catalog=" + target.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(targetConnectionString);
            try
            {
                targetConn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unable to open Target Database: " + ex.Message);
                return;
            }
            targetConn.Close();
            List<string> list = GetTables(sourceConnectionString);
            List<string> results = new List<string>();
            string message = "";
            results.Add("MATCHING " + source.DisplayName + " with " + target.DisplayName); results.Add("");
            foreach (string table in list)
            {
                DataColumnCollection dcMain = GetTableSchema(table, sourceConnectionString, out message);
                if (dcMain == null)
                {
                    results.Add("TABLE " + table + " not found in MAIN database."); results.Add("");
                    continue;
                }
                DataColumnCollection dcMatch = GetTableSchema(table, targetConnectionString, out message);
                if (dcMatch == null)
                {
                    results.Add("TABLE " + table + " not found in MATCH database."); results.Add("");
                    continue;
                }

                List<string> matchResults = MatchSchemas(table, dcMain, dcMatch);
                foreach (string s in matchResults)
                    results.Add(s);
            }

            Report rpt = new Report("Database Match Report", "Courier New", 12);
            rpt.LineHeight = .165 * 96;
            rpt.LeftMargin = (float)8;
            rpt.TopMargin = (float)5;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)70;
            foreach (string line in results)
                if (rpt.PrintPageLine(line))
                    rpt.NewPage();
            rpt.DisplayDocument();

        }

        /// <summary>
        /// Gets schema for passed table
        /// </summary>
        /// <param name="table">DB table name</param>
        /// <param name="ConnectionsString">Connection String for database</param>
        /// <param name="message">Error message, if any</param>
        /// <returns>Schema for specified table</returns>
        private DataColumnCollection GetTableSchema(string table, string ConnectionsString, out string message)
        {
            message = "";
            SqlConnection Conn = new SqlConnection(ConnectionsString);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM " + table, Conn);
            try
            {
                da.FillSchema(ds, SchemaType.Mapped);
                return ds.Tables[0].Columns;
            }
            catch (SqlException ex)
            {
                message = (ex.Message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return null;
        }

        /// <summary>
        /// Gathers not matched results for tables
        /// </summary>
        /// <param name="table">Name of DB table</param>
        /// <param name="dcMain">Source database column collection</param>
        /// <param name="dcMatch">Match database column collection</param>
        /// <returns></returns>
        private List<string> MatchSchemas(string table, DataColumnCollection dcMain, DataColumnCollection dcMatch)
        {
            bool notMatched = false;
            Dictionary<string, DataColumn> dictMatch = new Dictionary<string, DataColumn>();
            foreach(DataColumn dc in dcMatch)
            {
                dictMatch.Add(dc.ColumnName, dc);
            }
            List<string> results = new List<string>();
            string str = "TABLE: " + table;
            DataColumn column = null;
            for (int i = 0; i < dcMain.Count; i++)
            {
                if (dictMatch.TryGetValue(dcMain[i].ColumnName, out column))
                {
                    if (dcMain[i].DataType.Equals(column.DataType))
                    {
                        if (!dcMain[i].MaxLength.Equals(column.MaxLength))
                        {
                            if(!notMatched)
                                results.Add(str);
                            results.Add (" - Column [" + dcMain[i].ColumnName + "] Maxlength does not match <" + dcMain[i].MaxLength.ToString() + "> <" +column.MaxLength.ToString() + ">");
                            notMatched = true;
                        }
                    }
                    else
                    {
                        if (!notMatched)
                            results.Add(str);
                        results.Add(" - Column [" + dcMain[i].ColumnName + "] DataType does not match <" + dcMain[i].DataType.ToString() + "> <" + column.DataType.ToString() + ">");
                        notMatched = true;

                    }
                }
                else
                {
                    if (!notMatched)
                        results.Add(str);
                    results.Add(" - Column [" + dcMain[i].ColumnName + "] not found in target.");
                    notMatched = true;
                }

            }
            if (notMatched)
            {
                results.Add("");
            }
            return results;
        }
        /// <summary>
        /// Get all table names fro a database
        /// </summary>
        /// <param name="connectionString">Connection string for database</param>
        /// <returns>List of table names</returns>
        private List<string> GetTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                List<string> TableNames = new List<string>();
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }
                return TableNames;
            }
        }

        /// <summary>
        /// Gets a list of SQL scripts from [Application Folder]\DbRepairScripts
        /// Allows for a selected script to be run against the current database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdScripts_Click(object sender, RoutedEventArgs e)
        {
            frmDatabaseScripts frm = new frmDatabaseScripts();
            frm.Show();
        }
    }
}
