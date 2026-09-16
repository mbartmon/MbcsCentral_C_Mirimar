using SqlDbCloner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
    /// Interaction logic for frmDatabases.xaml
    /// </summary>
    public partial class frmDatabases : Window
    {
        private const string BaseServerName = "databaseSource";
        private const string BaseMachineName = "MachineName";
        private const string BaseInitialCatalog = "initialCatalog";
        private Dictionary<string, string> parms;
        private string BaseServer;
        private string BaseDatabase;
        private string baseCatalog = "";
        private Registry[] folders;
        private Registry[] names;
        private string InitialKey = "";
        private string InitialValue = "";
        private string NewKey = "";
        private string NewValue = "";
        private Databases[] Msources;
        private Databases[] ServerSources;
        private Databases[] DBsources;
        private string NewDatabaseName = "";
        private ClientRecord clientRecord;
        private TreeViewItem SourceServerItem = null;
        private int SourceIndex = 0;
        private TreeViewItem TargetServerItem = null;
        private int TargetIndex = 0;
        private bool Copying = false;

        private bool Save = false;

        DatabaseServers[] servers;
        DatabaseMachineNames[] machines;
        //string[] available;
        AvailableServers[] available;
        frmProgressWindow frmW = null;
        Thread prog = null;
        DatabaseSources databaseSources = new DatabaseSources();

        string machine;
        string server;
        bool testPassed;

        string newServer = "";
        string newInitialCatalog = "";
        string newConnectionString = "";

        bool cloudDB = false;

        public struct ProgressData
        {
            public bool cancel;
            public string database;
            public string from;
            public string to;
            public string toDB;

            public string message;
        }

        frmProgressWindow.ProgressData progressdata;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmDatabases()
        {
            InitializeComponent();
        }

        public frmDatabases(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            SetWindowTitle(GlobalShared.DatabaseSource);


            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            clientRecord = new ClientRecord() { InitialCatalog = GlobalShared.InitialCatalog, CreateDate = date };
            txtNewDatabaseName.DataContext = clientRecord;

            folders = Main.RetrieveDbFolders();
            names = Main.RetrieveDbNames();

            UpdateLists();
            this.DataContext = databaseSources;
        }

        private void UpdateLists()
        {
            Main.parms.getConfigs();

            object obj = null;
            Main.parms.getItem(GlobalShared.CONFIG_BASE_CATALOG, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Catalog.  Cannot create database.");
                return;
            }
            baseCatalog = (string)obj;
            Main.parms.getItem(GlobalShared.CONFIG_BASE_SERVER, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Server.  Cannot create database.");
                return;
            }
            BaseServer = (string)obj;
            databaseSources.productionServer = BaseServer;
            databaseSources.productionDatabase = baseCatalog;
            server = databaseSources.currentServer = GlobalShared.DatabaseSource;
            databaseSources.currentDatabase = GlobalShared.InitialCatalog;


            DBsources = Databases.GetDatabaseList(server);
            lstDatabases.ItemsSource = DBsources;
            machines = DatabaseMachineNames.LoadDatabaseMachineNames(GlobalShared.MachineName);
            //servers = DatabaseServers.LoadDatabaseServers(GlobalShared.DatabaseName.Replace("*", "").Replace(",1433", ""));
            servers = DatabaseServers.LoadDatabaseServers(baseCatalog);
            available = AvailableServers.LoadAvailableServers(BaseServer);
            lstDatabaseServers.ItemsSource = servers;
            lstDatabaseMachines.ItemsSource = machines;
            lstServersDatabases.ItemsSource = available;
            lstSourceServers.ItemsSource = available;
            lstTargetServers.ItemsSource = available;

        }
        /*
        private void cmdChangeMachine_Click(object sender, RoutedEventArgs e)
        {
            if (lstMachines.SelectedIndex >= 0)
            {
                int i = 0;
                Databases database = (Databases)lstMachines.SelectedItem;
                foreach (Databases data in Msources)
                {
                    data.Current = false;
                }
                database.Current = true;
                lstMachines.ItemsSource = null;
                lstMachines.ItemsSource = Msources;
            }
        }

        private void cmdChange_Click(object sender, RoutedEventArgs e)
        {
            if (lstServers.SelectedIndex >= 0)
            {
                int i = 0;
                Databases database = (Databases)lstServers.SelectedItem;
                foreach (Databases data in ServerSources)
                {
                    data.Current = false;
                }
                database.Current = true;
                lstServers.ItemsSource = null;
                lstServers.ItemsSource = ServerSources;
            }
        }
        */
        private void cmdChangeDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabases.SelectedItem != null)
            {
                int i = 0;
                Databases database = (Databases)lstDatabases.SelectedItem;
                foreach (Databases data in DBsources)
                {
                    data.Current = false;
                }
                database.Current = true;
                lstDatabases.ItemsSource = null;
                lstDatabases.ItemsSource = DBsources;
            }

        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Save = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);
            if (Save)
                SaveParms();
        }

        private void SaveParms()
        {
            //foreach (Databases data in DBsources)
            //{
            //    Main.parms.removeItem(data.Key);
            //}
            foreach (Databases db in DBsources)
            {
                if (db.Current)
                {
                    try
                    {
                        Manager.setUser(GlobalShared.userName, "*");

                    }
                    catch (Exception ex)
                    {
                        MessageBoxCustom.Show("Unable to use new database /r/n/n[" + ex.Message + "]");
                        return;
                    }
                    Manager.setUser(GlobalShared.userName, "*");
                    GlobalShared.InitialCatalog = db.DisplayName;

                    Main.SetDatabase(false);
                    GlobalShared.CN.ConnectionString = GlobalShared.SqlConnectionString;
                    Main.mSession.setItem("connectionString", GlobalShared.SqlConnectionString);
                    Main.GetAllRegistryEntries();
                    Main.setMainWindowTitle();
                    break;
                }
            }

            if ((bool)chkPermanent.IsChecked.Value)
            {
                Main.parms.removeItemStartsWith(BaseInitialCatalog);
                int i = 0;
                string newMachine;
                string newDB;

                // Set InitialCatalog
                string[] names = new string[DBsources.Length];
                string baseName = "";
                int j = 0;
                foreach (Databases db in DBsources)
                {
                    if (db.Current)
                        baseName = db.DisplayName;
                    names[j++] = db.DisplayName;
                }
                Array.Sort(names);
                for (j = 0; j < names.Length; j++)
                {
                    if (names[j].ToLower().Equals(baseName.ToLower()))
                    {
                        Main.parms.setItem(BaseInitialCatalog, names[j]);
                    }
                    else
                    {
                        Main.parms.setItem(string.Format("{0}{1:##}", BaseInitialCatalog, ++i), names[j]);
                    }
                }
                Main.parms.setConfigs();
                Main.parms.getConfigs();
                DBsources = Databases.LoadDatabases(GlobalShared.InitialCatalog);
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save = true;
            this.Close();
        }
        private void cmdSave_ClickServers(object sender, RoutedEventArgs e)
        {

            string oldServer = GlobalShared.DatabaseSource;
            string oldInitialCatalog = GlobalShared.InitialCatalog;
            string oldConnection = GlobalShared.SqlBaseConnectionString;

            GlobalShared.DatabaseSource = newServer;
            GlobalShared.InitialCatalog = newInitialCatalog;
            GlobalShared.SqlConnectionString = newConnectionString;
            Accounts acct = Accounts.load(GlobalShared.user.Account.login);
            if (acct == null)
            {
                GlobalShared.DatabaseSource = oldServer;
                GlobalShared.InitialCatalog = oldInitialCatalog;
                GlobalShared.SqlConnectionString = oldConnection.Replace("<source>", oldServer).Replace("<catalog>", oldInitialCatalog).Replace("<user>", GlobalShared.DB_USER_NAME).Replace("<password>", GlobalShared.DB_PASSWORD);

                MessageBoxCustom.Show("Your current login was not found in the new database.  Database will not be switched.");
                return;
            }

            ArrayList list = new ArrayList();

            for (int i = 0; i < lstSelectedDatabases.Items.Count; i++)
            {
                list.Add((Databases)lstSelectedDatabases.Items[i]);
            }
            DBsources = ((Databases[])(list.ToArray(typeof(Databases))));
            chkPermanent.IsChecked = chkPermanentServers.IsChecked;
            Save = true;
            this.Close();
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalShared.MachineName.Contains("*"))
            {
                //MessageBox.Show("Sorry, this operation cannot be performed on a Cloud Database.");
                //return;

                cloudDB = true;
            }
            lstDatabases.Visibility = Visibility.Collapsed;
            bdrDatabases.Visibility = Visibility.Collapsed;
            InputTextBox.Text = "";
            InputBox.Visibility = Visibility.Visible;
            InputBox.Focus();
            InputTextBox.Focus();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewDatabaseName.Text.Trim().Equals(""))
            {
                MessageBox.Show("You must enter a TAG.");
                return;
            }
            string server = Main.parms.GetString(GlobalShared.CONFIG_BASE_SERVER);
            if (server == null || server.Trim().Equals(""))
            {
                MessageBox.Show("Unable to find Base Server Name.  Cannot continue.");
                return;
            }
            string connectionString = "Server=" + server + ",1433;Initial Catalog=master; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            //string connectionString = "Server=" + server + ",1433;Initial Catalog=master; User Id=sa; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
            SqlConnection targetConn = null;
            try
            {
                targetConn = new SqlConnection(connectionString);
            }
            catch (SqlException ex)
            {
                if (connectionString.Contains(",1433"))
                    connectionString = connectionString.Replace(",1433", "");
                else
                    connectionString = connectionString.Replace(";Initial", ",1433;Initial");
                try
                {
                    targetConn = new SqlConnection(connectionString);
                }
                catch (SqlException qex)
                {
                    MessageBox.Show("Error connecting to SQL Server: " + qex.Message);
                    if (qex.InnerException != null)
                    {
                        MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                        return;
                    }

                }
                catch (Exception qex) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create DB Connection: " + ex.Message);
                return;
            }
            this.Cursor = Cursors.Wait;
            NewDatabaseName = txtNewDatabaseName.Text;
            if (cloudDB)
            {
                try
                {
                    targetConn.Open();
                }
                catch (SqlException ex)
                {
                    return;
                }
                string serverName = "";
                if (server.Contains("\\"))
                    serverName = server.Substring(server.LastIndexOf("\\") + 1);
                else
                    serverName = server.Substring(0, server.IndexOf("."));
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = targetConn;
                cmd.CommandText = "CREATE DATABASE [" + NewDatabaseName + "] AS COPY OF [" + serverName + "].[" + clientRecord.InitialCatalog + "]";
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return;
                }
                return;
            }
            if (Main.CreateDatabase(ref targetConn, NewDatabaseName))
            {
                //TODO: will drop tcp/ip port (1433) work on fishman?
                if (targetConn.ConnectionString.Contains(",1433"))
                    connectionString = "Server=" + server + ",1433;Initial Catalog=" + clientRecord.InitialCatalog + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
                else
                    connectionString = "Server=" + server + ";Initial Catalog=" + clientRecord.InitialCatalog + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";

                SqlConnection sourceConn = new SqlConnection(connectionString);
                if (connectionString.Contains(",1433"))
                {
                    connectionString = "Server=" + server + ",1433;Initial Catalog=" + NewDatabaseName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
                }
                else
                    connectionString = "Server=" + server + ";Initial Catalog=" + NewDatabaseName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";

                targetConn = new SqlConnection(connectionString);
                if (Main.CopyDatabase(sourceConn, targetConn))
                    MessageBox.Show("Database successfully copied.");
                else
                    MessageBox.Show("Could not copy database.");
                Array.Resize<Databases>(ref DBsources, DBsources.Length + 1);
                int i = DBsources.Length - 1;
                DBsources[i] = new Databases() { DisplayName = NewDatabaseName, Key = BaseInitialCatalog + "99", Current = false };
                bool chk = chkPermanent.IsChecked.Value;
                lstDatabases.ItemsSource = null;
                chkPermanent.IsChecked = true;
                SaveParms();
                chkPermanent.IsChecked = chk;
                lstDatabases.ItemsSource = DBsources;
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                lstDatabases.Visibility = Visibility.Visible;
                bdrDatabases.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Unable to create database.");
            this.Cursor = Cursors.Arrow;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstDatabases.Visibility = Visibility.Visible;
            bdrDatabases.Visibility = Visibility.Visible;
        }

        private void InputTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            frmDatabasesEdit frm = new frmDatabasesEdit(m_ThisApp);
            frm.ShowDialog();
            //DBsources = Databases.LoadDatabases(GlobalShared.InitialCatalog);
            DBsources = Databases.GetDatabaseList(GlobalShared.DatabaseSource);
            lstDatabases.ItemsSource = DBsources;
        }

        private void cmdBackup_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalShared.MachineName.Contains("*"))
            {
                MessageBox.Show("Sorry, this operation cannot be performed on a Cloud Database.");
                return;
            }
            frmDatabaseBackup frm = new frmDatabaseBackup(m_ThisApp);
            frm.ShowDialog();
        }

        private void SetWindowTitle(string sqlServer)
        {
            this.Title = "Database Utilites / Current Server = [" + sqlServer + "]";
        }
        private void YesButton_ClickServers(object sender, RoutedEventArgs e)
        {
            /*
            if (txtNewDatabaseName.Text.Trim().Equals(""))
            {
                MessageBox.Show("You must enter a TAG.");
                return;
            }
            string server = Main.parms.GetString(GlobalShared.CONFIG_BASE_SERVER);
            if (server == null || server.Trim().Equals(""))
            {
                MessageBox.Show("Unable to find Base Server Name.  Cannot continue.");
                return;
            }
            string connectionString = "Server=" + server + ",1433;Initial Catalog=master; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            //string connectionString = "Server=" + server + ",1433;Initial Catalog=master; User Id=sa; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
            SqlConnection targetConn = null;
            try
            {
                targetConn = new SqlConnection(connectionString);
            }
            catch (SqlException ex)
            {
                if (connectionString.Contains(",1433"))
                    connectionString = connectionString.Replace(",1433", "");
                else
                    connectionString = connectionString.Replace(";Initial", ",1433;Initial");
                try
                {
                    targetConn = new SqlConnection(connectionString);
                }
                catch (SqlException qex)
                {
                    MessageBox.Show("Error connecting to SQL Server: " + qex.Message);
                    if (qex.InnerException != null)
                    {
                        MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                        return;
                    }

                }
                catch (Exception qex) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create DB Connection: " + ex.Message);
                return;
            }
            this.Cursor = Cursors.Wait;
            NewDatabaseName = txtNewDatabaseName.Text;
            if (Main.CreateDatabase(ref targetConn, NewDatabaseName))
            {
                //TODO: will drop tcp/ip port (1433) work on fishman?
                if (targetConn.ConnectionString.Contains(",1433"))
                    connectionString = "Server=" + server + ",1433;Initial Catalog=" + clientRecord.InitialCatalog + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
                else
                    connectionString = "Server=" + server + ";Initial Catalog=" + clientRecord.InitialCatalog + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";

                SqlConnection sourceConn = new SqlConnection(connectionString);
                if (connectionString.Contains(",1433"))
                {
                    connectionString = "Server=" + server + ",1433;Initial Catalog=" + NewDatabaseName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
                }
                else
                    connectionString = "Server=" + server + ";Initial Catalog=" + NewDatabaseName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";

                targetConn = new SqlConnection(connectionString);
                if (Main.CopyDatabase(sourceConn, targetConn))
                    MessageBox.Show("Database successfully copied.");
                else
                    MessageBox.Show("Could not copy database.");
                Array.Resize<Databases>(ref DBsources, DBsources.Length + 1);
                int i = DBsources.Length - 1;
                DBsources[i] = new Databases() { DisplayName = NewDatabaseName, Key = BaseInitialCatalog + "99", Current = false };
                bool chk = chkPermanent.IsChecked.Value;
                lstDatabases.ItemsSource = null;
                chkPermanent.IsChecked = true;
                SaveParms();
                chkPermanent.IsChecked = chk;
                lstDatabases.ItemsSource = DBsources;
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                lstDatabases.Visibility = Visibility.Visible;
                bdrDatabases.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Unable to create database.");
            this.Cursor = Cursors.Arrow;
            */
        }

        private void NoButton_ClickServers(object sender, RoutedEventArgs e)
        {
            InputBoxServers.Visibility = System.Windows.Visibility.Collapsed;
            lstDatabaseServers.Visibility = Visibility.Visible;
            bdrDatabases.Visibility = Visibility.Visible;
        }

        private void InputTextBox_PreviewKeyDownServers(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CmdChangeDatabaseServer_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabaseServers.SelectedItem != null)
            {
                DatabaseServers dataServer = (DatabaseServers)lstDatabaseServers.SelectedItem;
                foreach (DatabaseServers data in servers)
                    data.Current = false;
                dataServer.Current = true;
                lstDatabaseServers.ItemsSource = null;
                lstDatabaseServers.ItemsSource = servers;
            }
        }

        private void CmdChangeDatabaseMachine_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabaseMachines.SelectedItem != null)
            {
                DatabaseMachineNames dataServer = (DatabaseMachineNames)lstDatabaseMachines.SelectedItem;
                foreach (DatabaseMachineNames data in machines)
                    data.Current = false;
                dataServer.Current = true;
                lstDatabaseMachines.ItemsSource = null;
                lstDatabaseMachines.ItemsSource = machines;
            }

        }

        private void CmdTest_Click(object sender, RoutedEventArgs e)
        {
            testPassed = false;
            // if (lstSelectedDatabases.SelectedItem == null)
            // {
            //    MessageBoxCustom.Show("Please select an initial database for this server.");
            //    return;          // }
            server = ((AvailableServers)lstServersDatabases.SelectedItem).DisplayName;
            if (server != null && !server.Trim().Equals(""))
            {
                int position = server.IndexOf(';');
                if (position >= 0)
                {
                    string dbMachine = "";
                    if (position > 0)
                        dbMachine = server.Substring(0, position);
                    string dbSource = (dbMachine.Contains("*") || dbMachine.Trim().Equals("") ? "" : dbMachine + "\\") + server.Substring(position + 1);


                    string connectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", dbSource.Replace("**", ",1434").Replace("*", ",1433")).
                                                                    Replace("<catalog>", "master").
                                                                    Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                    Replace("<password>", GlobalShared.DB_PASSWORD);
                    SqlConnection conn = new SqlConnection(connectionString);
                    try
                    {
                        conn.Open();
                    }
                    catch (SqlException ex)
                    {
                        if (connectionString.Contains(",1433") || connectionString.Contains(",1434"))
                            connectionString = connectionString.Replace(",1433", "").Replace(",1434", "");
                        try
                        {
                            conn = new SqlConnection(connectionString);
                            conn.Open();
                        }
                        catch (SqlException qex)
                        {
                            MessageBoxCustom.Show("Error connecting to new DB: " + qex.Message);
                            if (qex.InnerException != null)
                            {
                                MessageBoxCustom.Show("Inner Exception: " + qex.InnerException.Message);
                                return;
                            }

                        }
                        catch (Exception qex)
                        {
                            MessageBoxCustom.Show("Unable to open new DB: " + ex.Message);
                            if (ex.InnerException != null)
                                MessageBoxCustom.Show("Inner Exception: " + ex.InnerException.Message);
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCustom.Show("Unable to open new DB: " + ex.Message);
                        if (ex.InnerException != null)
                            MessageBoxCustom.Show("Inner Exception: " + ex.InnerException.Message);
                        return;
                    }
                    testPassed = true;
                    conn.Close();
                    MessageBoxCustom.Show("Connection successful");
                    return;
                }
            }
            return;
        }

        private void CtxDeleteServerDB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmdAddToAvailable_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabaseMachines.SelectedItem == null)
                return;
            machine = ((DatabaseMachineNames)lstDatabaseMachines.SelectedItem).DisplayName;
            server = ((DatabaseServers)lstDatabaseServers.SelectedItem).DisplayName;
            //CmdTest_Click(sender, e);  This test no longer applies in this situation.  Rewrite?
            testPassed = true;

            if (testPassed)
            {
                Main.parms.getConfigs();
                //string entry = (machine.Contains('*') ? "" : machine + ";") + server;
                string entry = machine + ";" + server;

                bool found = false;
                bool onlyItem = false;
                available = AvailableServers.LoadAvailableServers(BaseServer); //Main.parms.GetStrings(GlobalShared.CONFIG_AVAILABLE_DATABASES);
                //if (available.Length == 0 || available[0] == null)
                //{
                //    onlyItem = true;
                //}
                if (available != null && available.Length > 0)
                {
                    for (int i = 0; i < available.Length; i++)
                    {
                        if (available[i].DisplayName.Trim().ToLower().Equals(entry.Trim().ToLower()))
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    if (available != null)
                        Array.Resize<AvailableServers>(ref available, available.Length + 1);
                    else
                        available = new AvailableServers[1];
                    available[available.Length - 1] = new AvailableServers() { Key = GlobalShared.CONFIG_AVAILABLE_DATABASES, DisplayName = entry };
                    SaveAvailable();
                    Main.parms.setConfigs();
                    lstServersDatabases.ItemsSource = null;
                    lstServersDatabases.ItemsSource = available;
                }
            }
        }

        private void SaveAvailable()
        {
            try
            {
                Main.parms.removeItemStartsWith(GlobalShared.CONFIG_AVAILABLE_DATABASES);
            }
            catch (Exception ex)
            {
                // Do Nothing
            }
            for (int i = 0; i < available.Length; i++)
            {
                Main.parms.addItem(GlobalShared.CONFIG_AVAILABLE_DATABASES + i.ToString() + "," + available[i].DisplayName);
            }
            Main.parms.setConfigs();

        }

        private void CmdRemoveFromAvailable_Click(object sender, RoutedEventArgs e)
        {
            if (lstServersDatabases.SelectedItem != null)
            {
                ArrayList list = new ArrayList();
                available = (AvailableServers[])lstServersDatabases.ItemsSource;
                string item = ((AvailableServers)lstServersDatabases.SelectedItem).DisplayName;
                foreach (AvailableServers s in available)
                {
                    if (!s.DisplayName.Equals(item))
                        list.Add(s);
                }
                available = null;
                available = (AvailableServers[])list.ToArray(typeof(AvailableServers));
                lstServersDatabases.ItemsSource = null;
                lstServersDatabases.ItemsSource = available;
                SaveAvailable();
            }
        }

        private void CtxPeekDatabases_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CtxMakeCurrent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmdMakeAvailableCurrent_Click(object sender, RoutedEventArgs e)
        {
            AvailableServers server = (AvailableServers)lstServersDatabases.SelectedItem;
            newServer = server.DisplayName;
            if (lstSelectedDatabases.SelectedItem == null)
            {
                MessageBoxCustom.Show("Please select an initial database for this server.");
                return;
            }
            Databases db = (Databases)lstSelectedDatabases.SelectedItem;
            newInitialCatalog = db.DisplayName;
            if (newServer != null && !newServer.Trim().Equals(""))
            {
                int position = newServer.IndexOf(';');
                if (position > 0)
                {
                    string dbMachine = newServer.Substring(0, position);
                    newServer = (dbMachine.Contains("*") ? "" : dbMachine + "\\") + newServer.Substring(position + 1).Replace("**", ",1434").Replace("*", ",1433");

                    newConnectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", newServer).
                                                                        Replace("<catalog>", newInitialCatalog).
                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                        Replace("<password>", GlobalShared.DB_PASSWORD);
                    SqlConnection conn = new SqlConnection(newConnectionString);
                    try
                    {
                        conn.Open();
                    }
                    catch (SqlException sex)
                    {
                        MessageBoxCustom.Show("Could not connect to database (SQL): " + sex.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCustom.Show("Could not connect to database: " + ex.Message);
                        return;

                    }
                    conn.Close();
                    conn = null;

                    for (int i = 0; i < lstSelectedDatabases.Items.Count; i++)
                    {
                        Databases dbs = (Databases)lstSelectedDatabases.Items[i];
                        dbs.Current = false;
                    }

                    db.Current = true;


                    MessageBoxCustom.Show("Current server/database will be: " + newServer + "/" + newInitialCatalog + "\r\n\nWhen change is saved.");
                }
            }
        }

        private void CmdPeekDatabases_Click(object sender, RoutedEventArgs e)
        {
            if (lstServersDatabases.SelectedItem == null)
                return;

            string servers = (string)lstServersDatabases.SelectedItem;
            string[] s = servers.Split(';');
            if (s.Length < 2)
                return;
            machine = s[0];
            server = (machine.Contains("*") ? "" : machine + "\\") + s[1].Replace("**", ",1434").Replace("*", ",1433");

            string connectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", server).
                                                          Replace("<catalog>", "master").
                                                          Replace("<user>", GlobalShared.DB_USER_NAME).
                                                          Replace("<password>", GlobalShared.DB_PASSWORD);
            SqlConnection conn = new SqlConnection(connectionString);

            Databases[] db = Databases.LoadDatabases("", "", conn);
            if (db != null && db.Length > 0)
            {
                string dbs = "";
                for (int i = 0; i < db.Length; i++)
                {
                    dbs += db[i].DisplayName + "\r\n";
                }
                MessageBoxCustom.Show(dbs);
            }
        }

        private void LstServersDatabases_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView lst = (TreeView)sender;
            if (lst.SelectedItem == null)
                return;

            string thisServer = ((AvailableServers)lst.SelectedItem).DisplayName;
            string[] s = thisServer.Split(';');
            if (s.Length < 2)
                return;
            machine = s[0];
            newServer = (machine.Contains("*") ? "" : machine + "\\") + s[1].Replace("**", ",1434").Replace("*", ",1433");
            TreeView lstUpdate = null;
            switch (lst.Name)
            {
                case "lstServersDatabases":
                    lstUpdate = lstSelectedDatabases;
                    break;
                case "lstSourceServers":
                    lstUpdate = lstSourceDatabases;
                    ItemContainerGenerator icg = lstSourceServers.ItemContainerGenerator;

                    if (!Copying)
                    {
                        for (int i = 0; i < lstSourceServers.Items.Count; i++)
                        {
                            TreeViewItem item = icg.ContainerFromIndex(i) as TreeViewItem;
                            if (item.IsSelected)
                            {
                                SourceServerItem = item;
                                SourceIndex = i;
                                break;
                            }
                        }
                    }
                    break;
                case "lstTargetServers":
                    lstUpdate = lstTargetDatabases;
                    icg = lstTargetServers.ItemContainerGenerator;

                    if (!Copying)
                    {
                        for (int i = 0; i < lstTargetServers.Items.Count; i++)
                        {
                            TreeViewItem item = icg.ContainerFromIndex(i) as TreeViewItem;
                            if (item.IsSelected)
                            {
                                TargetServerItem = item;
                                TargetIndex = i;
                                //break;
                            }
                        }
                    }
                    break;
            }
            lstSelectedDatabases.ItemsSource = null;
            lstUpdate.ItemsSource = null;
            Databases[] db = Databases.GetDatabaseList(thisServer);
            if (db != null && db.Length > 0)
            {
                lstUpdate.ItemsSource = db;
            }

        }

        private void CmdClearTargetDB_Click(object sender, RoutedEventArgs e)
        {
            if (lstTargetDatabases != null)
                ClearTreeViewItemsControlSelection(lstTargetDatabases.Items, lstTargetDatabases.ItemContainerGenerator);
        }

        private void CmdclearSourceDB_Click(object sender, RoutedEventArgs e)
        {
            if (lstSourceDatabases != null)
                ClearTreeViewItemsControlSelection(lstSourceDatabases.Items, lstSourceDatabases.ItemContainerGenerator);
        }

        private static void ClearTreeViewItemsControlSelection(ItemCollection ic, ItemContainerGenerator icg)
        {
            if ((ic != null) && (icg != null))
                for (int i = 0; i < ic.Count; i++)
                {
                    TreeViewItem tvi = icg.ContainerFromIndex(i) as TreeViewItem;
                    if (tvi != null)
                    {
                        ClearTreeViewItemsControlSelection(tvi.Items, tvi.ItemContainerGenerator);
                        tvi.IsSelected = false;
                    }
                }
        }

        private void CmdCopyBetween_Click(object sender, RoutedEventArgs e)
        {
            bool drop = true;
            Copying = true;
            string dbSource = "";
            if (lstTargetServers.SelectedItem == null)
            {

                MessageBoxCustom.Show("A TARGET server must be selected.");
                Copying = false;
                return;
            }
            frmProgressWindow.ProgressData progressdata = new frmProgressWindow.ProgressData();
            //frmProgressWindow frmW = new frmProgressWindow();
            GlobalShared.DbCopyStatus = progressdata;
            //frmW.Show();
            Databases source = ((Databases)lstSourceDatabases.SelectedItem);
            Databases target = ((Databases)lstTargetDatabases.SelectedItem);
            //frmW.txtDatabase.Text = source.DisplayName;
            //frmW.txtFrom.Text = source.FullValue;
            GlobalShared.DbCopyStatus.database = source.DisplayName;
            GlobalShared.DbCopyStatus.from = source.FullValue;

            if (target == null)
            {
                //frmW.txtTo.Text = ((AvailableServers)lstTargetServers.SelectedItem).DisplayName;
                //frmW.txtToDB.Text = source.DisplayName;

                GlobalShared.DbCopyStatus.to = ((AvailableServers)lstTargetServers.SelectedItem).DisplayName;
                GlobalShared.DbCopyStatus.toDB = source.DisplayName;
            }
            else
            {
                //frmW.txtTo.Text = target.FullValue;
                //frmW.txtToDB.Text = target.DisplayName;
                GlobalShared.DbCopyStatus.to = target.FullValue;
                GlobalShared.DbCopyStatus.toDB = target.DisplayName;

            }
            server = ((AvailableServers)lstTargetServers.SelectedItem).DisplayName;
            cloudDB = false;
            if (server != null && (((AvailableServers)lstTargetServers.SelectedItem).Azure && ((AvailableServers)lstSourceServers.SelectedItem).Azure))
            {
                cloudDB = true;
            }
            else if (server == null || ((AvailableServers)lstTargetServers.SelectedItem).Azure || ((AvailableServers)lstSourceServers.SelectedItem).Azure)
            {
                MessageBoxCustom.Show("It is not possible to copy from a local server to an Azure database.");
                //frmW.Close();
                GlobalShared.DbCopyStatus.cancel = true;
                return;
            }
            int position = server.IndexOf(';');
            string dbMachine = (position <= 0 ? "" : server.Substring(0, position));
            if (target == null)
            {
                bool inTarget = false;
                for (int i = 0; i < lstTargetDatabases.Items.Count; i++)
                {
                    if (((Databases)lstTargetDatabases.Items[i]).DisplayName.Equals(source.DisplayName))
                    {
                        inTarget = true;
                    }
                }

                if (!inTarget && MessageBoxCustom.Show("Source database does not exist on target server.  Create it and continue copy?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    //frmW.Close();
                    GlobalShared.DbCopyStatus.cancel = true;
                    Copying = false;
                    return;
                }
                drop = false;
                dbSource = (dbMachine.Trim().Equals("") || dbMachine.Contains("*") ? "" : dbMachine + "\\") + server.Substring(position + 1);
                dbSource = dbSource.Replace("**", ",1434").Replace("*", ",1433");
                target = new Databases() { DisplayName = ((Databases)lstSourceDatabases.SelectedItem).DisplayName, FullValue = dbSource, Key = ((Databases)lstSourceDatabases.SelectedItem).DisplayName, Machine = dbMachine };
            }
            else
                target.Machine = dbMachine;
            GlobalShared.DbCopyStatus.message = "STARTING COPY";
            NewDatabaseName = txtNewDatabaseName.Text;
            if (cloudDB)
            {
                string connectionString = "Server=" + target.FullValue + ",1433;Initial Catalog=master; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
                //string connectionString = "Server=" + server + ",1433;Initial Catalog=master; User Id=sa; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=30";
                SqlConnection targetConn = null;
                try
                {
                    targetConn = new SqlConnection(connectionString);
                }
                catch (SqlException ex)
                {
                    if (connectionString.Contains(",1433"))
                        connectionString = connectionString.Replace(",1433", "");
                    else
                        connectionString = connectionString.Replace(";Initial", ",1433;Initial");
                    try
                    {
                        targetConn = new SqlConnection(connectionString);
                    }
                    catch (SqlException qex)
                    {
                        MessageBox.Show("Error connecting to SQL Server: " + qex.Message);
                        if (qex.InnerException != null)
                        {
                            MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                            return;
                        }

                    }
                    catch (Exception qex) { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create DB Connection: " + ex.Message);
                    return;
                }
                try
                {
                    targetConn.Open();
                }
                catch (SqlException ex)
                {
                    return;
                }
                string serverName = ((AvailableServers)lstSourceServers.SelectedItem).DisplayName.Replace("*Azure*;","");
                if (serverName.Contains("\\"))
                    serverName = serverName.Substring(serverName.LastIndexOf("\\") + 1);
                else
                    serverName = serverName.Substring(0, serverName.IndexOf(".") );
                string sql = "SELECT [state_desc] FROM sys.databases";
                DataSet ds = null;
                DataRow dr = null;
                string result = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = targetConn;
                cmd.CommandText = "CREATE DATABASE [" + target.DisplayName + "] AS COPY OF [" + serverName + "].[" + target.DisplayName + "]";
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                    ds = clsDB.Query2(sql, targetConn);
                    dr = null;
                    result = "";
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dr = ds.Tables[0].Rows[0];
                        result = dr[0].ToString();
                        ds = null;
                        dr = null;
                    }
                    else
                    {
                        MessageBoxCustom.Show("Database copy state cannot be determined.\r\nIt may still be processing.");
                        return;
                    }
                    if (result.ToLower().Equals("online"))
                        MessageBoxCustom.Show("Copy of database has completed.");
                }
                catch (SqlException ex)
                {
                    ds = clsDB.Query2(sql, targetConn);
                    dr = null;
                    result = "";
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dr = ds.Tables[0].Rows[0];
                        result = dr[0].ToString();
                        ds = null;
                        dr = null;
                    }
                    else
                    {
                        MessageBoxCustom.Show("Database copy state cannot be determined.\r\nIt may still be processing.");
                        return;
                    }
                    int count = 0;

                    if (result.ToLower().Equals("copying"))
                    {
                        WaitForCompletion(targetConn);
                    }
                    if (MessageBoxCustom.Show("Database copy may be running in background <" + result + ">\r\n" + "Do you want to wait for result?","",MessageBoxButton.YesNo,this) == MessageBoxResult.Yes)
                    {
                        WaitForCompletion(targetConn);
                    }
                    return;
                }
                return;
            }

            StartProgress();
            CopyDatabase((Databases)lstSourceDatabases.SelectedItem, target, false, drop);
            UpdateLists();
            //frmW.txtProgress.Text = "COPY COMPLETE.";
            //frmW.Close();
            GlobalShared.DbCopyStatus.message = "COPY COMPLETE.";
            GlobalShared.DbCopyStatus.cancel = true;
            prog.Abort();
            ItemContainerGenerator icg = lstSourceServers.ItemContainerGenerator;
            TreeViewItem item = icg.ContainerFromIndex(SourceIndex) as TreeViewItem;
            item.IsSelected = true;
            icg = lstTargetServers.ItemContainerGenerator;
            item = icg.ContainerFromIndex(TargetIndex) as TreeViewItem;
            item.IsSelected = true;
            Copying = false;
        }

        private void WaitForCompletion(SqlConnection targetConn)
        {
            string sql = "SELECT [state_desc] FROM sys.databases";
            DataSet ds = null;
            DataRow dr = null;
            string result = "";
            bool wait = true;
            while (wait)
            {
                Thread.Sleep(10000);
                ds = clsDB.Query2(sql, targetConn);
                dr = null;
                result = "";
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    result = dr[0].ToString();
                    ds = null;
                    dr = null;
                    if (result.ToLower().Equals("online"))
                    {
                        MessageBoxCustom.Show("Database copy successful.");
                        return;
                    }
                    if (result.ToLower().Equals("suspect"))
                    {
                        MessageBox.Show("Database copy failed.");
                        return;
                    }
                    if (MessageBoxCustom.Show("Database copy state <" + result + "> Continue waiting?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBoxCustom.Show("Database copy state cannot be determined.\r\nIt may still be processing.");
                    wait = false;
                }
            }

        }
        private void CmdMoveBetween_Click(object sender, RoutedEventArgs e)
        {
            CopyDatabase((Databases)lstSourceDatabases.SelectedItem, (Databases)lstTargetDatabases.SelectedItem, true);
            UpdateLists();
        }

        private void CopyDatabase(Databases source, Databases target, bool move = false, bool drop = true)
        {
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

            if (source.FullValue.ToLower().Equals(target.FullValue.ToLower()) && source.DisplayName.ToLower().Equals(target.DisplayName.ToLower()))
            {
                MessageBox.Show("SOURCE and TARGET are the same.  Cannot perform copy.");
                return;
            }
            if (drop && MessageBox.Show("This operation will erase all data in the target database.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            string connectionString = "Server=" + source.FullValue + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection sourceConn = new SqlConnection(connectionString);
            try
            {
                GlobalShared.COPY_DB_SOURCE_CONNECTIONSTRING = connectionString;
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
                    GlobalShared.COPY_DB_SOURCE_CONNECTIONSTRING = connectionString;
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
            Properties.Settings.Default.SourceServer = connectionString;
            connectionString = "Server=" + target.FullValue + ";Initial Catalog=master; User Id=mbartmon" + (target.Machine.Contains("Azure") ? "@" + target.FullValue.Substring(0, target.FullValue.IndexOf('.')) : "") + "; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(connectionString);
            try
            {
                GlobalShared.COPY_DB_TARGET_CONNECTIONSTRING = connectionString;
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
                    GlobalShared.COPY_DB_TARGET_CONNECTIONSTRING = connectionString;
                    targetConn = new SqlConnection(connectionString);
                    targetConn.Open();
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

            //CopyProgress.IsOpen = true;
            //connectionString = "Server=" + target.FullValue + ";Initial Catalog=master; User Id=mbartmon" + (target.Machine.Contains("Azure") ? "@" + target.FullValue.Substring(0, target.FullValue.IndexOf('.')) : "") + "; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=120";
            connectionString = "Server=" + target.FullValue + ";Initial Catalog=master; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=120";
            SqlConnection createConn = new SqlConnection(connectionString);
            connectionString = "Server=" + target.FullValue + ";Initial Catalog=" + target.DisplayName + "; User Id=mbartmon" + (target.Machine.Contains("Azure") ? "@" + target.FullValue.Substring(0, target.FullValue.IndexOf('.')) : "") + "; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            //connectionString = "Server=" + target.FullValue + ";Initial Catalog=" + target.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            GlobalShared.COPY_DB_TARGET_CONNECTIONSTRING = connectionString;
            Properties.Settings.Default.DestinationServer = connectionString;
            GlobalShared.DbCopyStatus.message = "CREATING TARGET DATABASE";
            frmW.txtProgress.Dispatcher.Invoke((Action)(() => frmW.txtProgress.Text = "CREATING TARGET DATABASE"));
            if (Main.CreateDatabase(ref createConn, target.DisplayName, drop))
            {
                //GlobalShared.DbCopyStatus.message = "TARGET DATABASE CREATED";
                frmW.txtProgress.Dispatcher.Invoke((Action)(() => frmW.txtProgress.Text = "TARGET DATABASE CREATED"));

                SqlCreateSchemaAndTables sqlCreator = new SqlCreateSchemaAndTables() { SourceDatabaseName = source.DisplayName, SourceServerName = source.FullValue, TargetDatabaseName = target.DisplayName, TargetServerName = target.FullValue, Progress = frmW };
                sqlCreator.Start();

                //MbcsCentral.Properties.Settings.Default.SourceServer = sourceConn.ConnectionString;
                //MbcsCentral.Properties.Settings.Default.DestinationServer = targetConn.ConnectionString;


                SqlDbCloner.ChooseConnections frm = new SqlDbCloner.ChooseConnections();
                frm.DoSchema = false;
                frmW.txtProgress.Dispatcher.Invoke((Action)(() => frmW.txtProgress.Text = "TRANSFERRING DATA"));
                frm.FormOnLoad();
                frmW.txtProgress.Dispatcher.Invoke((Action)(() => frmW.txtProgress.Text = "DATABASE COPY COMPLETE"));

                MessageBoxCustom.Show("Database copy complete.", "", MessageBoxButton.OK, this);
            }

        }
        private void CmdDeleteBetween_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string serverName = "";
            Databases source = (Databases)lstSourceDatabases.SelectedItem;

            AvailableServers server = (AvailableServers)lstSourceServers.SelectedItem;

            Copying = true;
            if (source.DisplayName.ToLower().Equals(baseCatalog.ToLower()))
            {
                MessageBox.Show("Cannot delete MAIN database.");
                Copying = false;
                return;
            }
            if (source == null || server == null)
            {
                MessageBox.Show("No SOURCE server/database selected.");
                Copying = false;
                return;
            }
            if (MessageBox.Show("About to DELETE <" + source.DisplayName + "> and all of its data.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                Copying = false;
                return;
            }
            bool wasCloudDb = cloudDB;
            if (server.DisplayName.ToLower().Contains("*azure*") || source.DisplayName.Contains("."))
            {
                //MessageBox.Show("Sorry, this operation cannot be performed on a Cloud Database.");
                //return;

                cloudDB = true;
                serverName = server.DisplayName.Replace("*Azure*", "").Replace(";","");
            }
            else
                serverName = server.DisplayName;
            Copying = true;
            string connectionString = "Server=" + serverName.Replace(";", "\\").Replace("**", ",1434").Replace("*", ",1433") + ";Initial Catalog=master; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(connectionString);
            if (Main.CreateDatabase(ref targetConn, source.DisplayName, true, true, cloudDB) == false)
            {
                cloudDB = wasCloudDb;
                if (MessageBox.Show("Could not delete database <" + source.DisplayName + ">.  Delete list item anyway?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    Copying = false;
                return;
            }
            cloudDB = wasCloudDb;
            UpdateLists();
            ItemContainerGenerator icg = lstSourceServers.ItemContainerGenerator;
            TreeViewItem item = icg.ContainerFromIndex(SourceIndex) as TreeViewItem;
            item.IsSelected = true;
            Copying = false;

            /*
            Main.parms.removeItemStartsWith(BASEDATABASE);

            string[] names = new string[DBsources.Length];
            int j = 0;
            foreach (Databases db in DBsources)
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
            DBsources = Databases.LoadDatabases(baseCatalog, BaseDatabase);
            lstSources.ItemsSource = DBsources;
            lstTargets.ItemsSource = DBsources;
            */


        }

        private void StartProgress()
        {
            prog = new Thread(ShowProgress);
            prog.SetApartmentState(ApartmentState.STA);
            prog.Start(GlobalShared.DbCopyStatus);
        }

        private void ShowProgress(object data)
        {
            frmProgressWindow.ProgressData progdata = (frmProgressWindow.ProgressData)data;
            frmW = new frmProgressWindow(progdata);
            frmW.Show();

        }

        private void cmdRepair_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabases.SelectedItem != null)
            {
                Databases database = (Databases)lstDatabases.SelectedItem;

                RepairDatabase(database, GlobalShared.DatabaseSource, this);

                /*
                if (MessageBoxCustom.Show("Repair <" + database.DisplayName + ">?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
                {
                    string ConnectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", GlobalShared.DatabaseSource).
                                                                                            Replace("<catalog>", database.DisplayName).
                                                                                            Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                                            Replace("<password>", GlobalShared.DB_PASSWORD);
                    string parms = "\"" + ConnectionString + "\"" + " \"" + GlobalShared.ScriptPaths + "\" \"" + GlobalShared.ScriptRepairLog + "\" x";
                    System.Diagnostics.Process exProc = new System.Diagnostics.Process();
                    exProc.StartInfo.FileName = "MbcsDbUpdate.exe";
                    exProc.StartInfo.Arguments = parms;
                    exProc.Start();
                    exProc.WaitForExit(30000);
                }
                */
            }
            else
                MessageBoxCustom.Show("No Database selected.");


        }

        public static void RepairDatabase(Databases database, string dbSource, Window me)
        {
            if (MessageBoxCustom.Show("Repair <" + database.DisplayName + ">?", "", MessageBoxButton.YesNo, me) == MessageBoxResult.Yes)
            {
                string ConnectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", dbSource).
                                                                                        Replace("<catalog>", database.DisplayName).
                                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                                        Replace("<password>", GlobalShared.DB_PASSWORD);
                string parms = "\"" + ConnectionString + "\"" + " \"" + GlobalShared.ScriptPaths + "\" \"" + GlobalShared.ScriptRepairLog + "\" x";
                System.Diagnostics.Process exProc = new System.Diagnostics.Process();
                exProc.StartInfo.FileName = "MbcsDbUpdate.exe";
                exProc.StartInfo.Arguments = parms;
                exProc.Start();
                exProc.WaitForExit(30000);
            }
        }

        private void cmdRepairDB_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedDatabases.SelectedItem != null)
            {
                Databases database = (Databases)lstSelectedDatabases.SelectedItem;
                string dbSource = database.FullValue;
                RepairDatabase(database, dbSource, this);
            }
            else
                MessageBoxCustom.Show("No Database selected.");
        }
    }

    public class DatabaseSources : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        string m_currentServer = "Current Server";
        string m_currentDatabase;
        string m_productionServer;
        string m_productionDatabase;

        public string currentServer
        {
            get { return m_currentServer; }
            set
            {
                m_currentServer = value;
                OnPropertyChanged("currentServer");

            }
        }
        public string currentDatabase
        {
            get { return m_currentDatabase; }
            set
            {
                m_currentDatabase = value;
                OnPropertyChanged("currentDatabase;");
            }
        }
        public string productionServer
        {
            get { return m_productionServer; }
            set
            {
                m_productionServer = value;
                OnPropertyChanged("productionServer");
            }
        }
        public string productionDatabase
        {
            get { return m_productionDatabase; }
            set
            {
                m_productionDatabase = value;
                OnPropertyChanged("productionDatabase");
            }
        }

    }

    public class Databases
    {
    public static Databases[] GetDatabaseList(string server)
    {
        int position = server.IndexOf(';');
        //if (position < 0) position = 0;
        string dbMachine = (position <= 0 ? "" :server.Substring(0, position));
        string dbSource = (dbMachine.Trim().Equals("") || dbMachine.Contains("*") ? "" : dbMachine + "\\") + server.Substring(position + 1);
        dbSource = dbSource.Replace("**", ",1434").Replace("*", ",1433");
        try
        {
            SqlConnection conn = new SqlConnection(GlobalShared.SqlBaseConnectionString.Replace("<source>", dbSource).
                                                                        Replace("<catalog>", "master").
                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                        Replace("<password>", GlobalShared.DB_PASSWORD));
            return LoadDatabases(GlobalShared.InitialCatalog, "", conn, dbSource);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static Databases[] LoadDatabases(string BaseInitialCatalog, string BaseDatabase = "", SqlConnection conn = null,  string fullname = "")
        {
            if (conn == null)
                conn = GlobalShared.CN;
            string Name;
            bool current = false;
            bool azure = false;
            ArrayList CatalogList = new ArrayList();

            DataSet ds = clsDB.Query2("SELECT Name FROM sys.databases WHERE Name LIKE '%MbcsCentral%' ORDER BY Name", conn);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (conn.ConnectionString.Contains("\\"))
                    azure = false;
                else if (conn.ConnectionString.Contains("."))
                    azure = true;
                string baseDb = (string)(BaseDatabase.Trim().Equals("") ? BaseInitialCatalog : BaseDatabase);
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    Name = (string)dr[0];
                    /*
                    if (Name.ToLower().Trim().Contains("azure"))
                        azure = true;
                    else
                        azure = false;
                    */
                    if (Name.Replace("*", "").ToLower().Equals(baseDb.Replace("*", "").ToLower()))
                    {
                        current = true;
                    CatalogList.Add(new Databases() { Key = BaseInitialCatalog, DisplayName = Name, Current = current, FullValue = fullname });
                    }
                    else
                        CatalogList.Add(new Databases() { Key = string.Format("{0}{1:##}", BaseInitialCatalog, ++i), DisplayName = Name, Current = current, FullValue = fullname, Azure = azure });
                    current = false;
                }
                return (Databases[])CatalogList.ToArray(typeof(Databases));
            }
            return null;
        }

        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string FullValue { get; set; }
        public string Machine { get; set; }
        public bool Azure { get; set; } = false;
        public bool Current { get; set; }
    }
    public class DatabaseServers
    {

        public static DatabaseServers[] LoadDatabaseServers(string baseServer)
        {
            string Name;
            bool current = false;
            ArrayList CatalogList = new ArrayList();
            string[] servers = Main.parms.GetStrings(GlobalShared.CONFIG_DATABASE_SOURCE);
            if (servers != null && servers.Length > 0)
            {
                int i = 0;
                foreach (string s in servers)
                {
                    Name = s;
                    if (Name.ToLower().Trim().Replace("*", "").Replace("**", "").Replace(",1433", "").Replace(",1434","").Equals(baseServer.ToLower().Trim().Replace("*", "").Replace("**", "").Replace(",1433", "").Replace(",1434", "")))
                    {
                        current = true;
                        CatalogList.Add(new DatabaseServers() { Key = GlobalShared.CONFIG_DATABASE_SOURCE, DisplayName = Name, Current = current });
                    }
                    else
                        CatalogList.Add(new DatabaseServers() { Key = string.Format("{0}{1:##}", GlobalShared.CONFIG_DATABASE_SOURCE, ++i), DisplayName = Name, Current = current });
                    current = false;
                }
                return (DatabaseServers[])CatalogList.ToArray(typeof(DatabaseServers));
            }
            return null;
        }

        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public bool Current { get; set; }
    }

    public class DatabaseMachineNames
    {
        public static DatabaseMachineNames[] LoadDatabaseMachineNames(string baseMachine)
        {
            string Name;
            bool current = false;
            ArrayList CatalogList = new ArrayList();
            string[] machines = Main.parms.GetStrings(GlobalShared.CONFIG_MACHINE_NAME);
            if (machines != null && machines.Length > 0)
            {
                int i = 0;
                foreach (string s in machines)
                {
                    Name = s;
                    if (Name.ToLower().Trim().Equals(baseMachine.ToLower().Trim()))
                    {
                        current = true;
                        CatalogList.Add(new DatabaseMachineNames() { Key = GlobalShared.CONFIG_MACHINE_NAME, DisplayName = Name, Current = current });
                    }
                    else
                        CatalogList.Add(new DatabaseMachineNames() { Key = string.Format("{0}{1:##}", GlobalShared.CONFIG_MACHINE_NAME, ++i), DisplayName = Name, Current = current });
                    current = false;
                }
                return (DatabaseMachineNames[])CatalogList.ToArray(typeof(DatabaseMachineNames));
            }
            return null;
        }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public bool Current { get; set; }

    }

    public class AvailableServers
    {

        public static AvailableServers[] LoadAvailableServers(string baseServer)
        {
            bool current = false;
            bool azure = false;
            string machine = "";
            string server = "";
            string[] s = baseServer.Split('\\');
            if (s.Length == 1 )
            {
                machine = "";
                server = s[0];
            }
            else if (s.Length == 2)
            {
                machine = s[0];
                server = s[1];
            }

            string[] available = Main.parms.GetStrings(GlobalShared.CONFIG_AVAILABLE_DATABASES);

            if (available == null || available.Length == 0)
                return null;
            ArrayList CatalogList = new ArrayList();

            foreach(string a in available)
            {
                string[] s2 = a.Split(';');
                if (s2.Length == 2)
                    if (s2[0].ToLower().Trim().Contains("azure"))
                        azure = true;
                    else
                        azure = false;
                    if (s2[0] == machine && s2[1].Replace("*", "") == server)
                        current = true;
                CatalogList.Add(new AvailableServers() { Key = GlobalShared.CONFIG_AVAILABLE_DATABASES, DisplayName = a, Current = current, Azure = azure });
                current = false;
            }
            return (AvailableServers[])CatalogList.ToArray(typeof(AvailableServers));
            return null;
        }
        public string Key { get; set; }
        public string DisplayName { set; get; }
        public string FullName { get; set; }
        public bool Azure { get; set; } = false;
        public bool Current { get; set; }
    }

        

}
