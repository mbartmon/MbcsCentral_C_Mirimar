using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmDatabaseServers.xaml
    /// </summary>
    public partial class frmDatabaseServers : Window
    {
        DatabaseServers[] servers;
        DatabaseMachineNames[] machines;



        string thisApp;

        public frmDatabaseServers(string app)
        {
            thisApp = app;
            InitializeComponent();
        }


        public frmDatabaseServers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.parms.getConfigs();
            machines = DatabaseMachineNames.LoadDatabaseMachineNames(GlobalShared.MachineName);
            servers = DatabaseServers.LoadDatabaseServers(GlobalShared.DatabaseName.Replace("*","").Replace(",1433",""));
            lstDatabaseServers.ItemsSource = servers;
            lstDatabaseMachines.ItemsSource = machines;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            //Save = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
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

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstDatabaseServers.Visibility = Visibility.Visible;
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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            //Save = true;
            this.Close();
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
            if (lstDatabaseMachines.SelectedItem != null && lstDatabaseServers.SelectedItem != null)
            {
                string machine = ((DatabaseMachineNames)lstDatabaseMachines.SelectedItem).DisplayName;
                string server = (machine.Contains("*") ? "" : machine + "\\") + ((DatabaseServers)lstDatabaseServers.SelectedItem).DisplayName.Replace("*", ",1433");
                
                string connectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>",server).
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
                    if (connectionString.Contains(",1433"))
                        connectionString = connectionString.Replace(",1433", "");
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
                conn.Close();
                MessageBoxCustom.Show("Connection successful");
            }
        }
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
                    if (Name.ToLower().Trim().Replace("*","").Replace(",1433","").Equals(baseServer.ToLower().Trim()))
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
}
