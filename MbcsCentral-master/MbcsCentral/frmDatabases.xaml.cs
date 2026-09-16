 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        private string InitialKey = "";
        private string InitialValue = "";
        private string NewKey = "";
        private string NewValue = "";
        private Databases[] Msources;
        private Databases[] ServerSources;
        private Databases[] DBsources;
        private string NewDatabaseName = "";
        private ClientRecord clientRecord;

        private bool Save = false;

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
            string baseCatalog = "";
            object obj = null;
            Main.parms.getItem(GlobalShared.CONFIG_BASE_CATALOG, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Catalog.  Cannot create database.");
                return;
            }
            baseCatalog = (string)obj;
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");
           
            clientRecord = new ClientRecord() { InitialCatalog = baseCatalog , CreateDate = date };
            txtNewDatabaseName.DataContext = clientRecord;
            LoadDictionary();
            lstMachines.ItemsSource = Msources;
            lstServers.ItemsSource = ServerSources;
            lstDatabases.ItemsSource = DBsources;
        }

        private void LoadDictionary()
        {
            parms = Main.parms.GetDict;
            Dictionary<string, string>.KeyCollection keys = parms.Keys;
            ArrayList Serverlist = new ArrayList();
            ArrayList Mlist = new ArrayList();
            ArrayList CatalogList = new ArrayList();
            string value = "";
            bool current = false;

            foreach (string key in keys)
            {
                if (key.StartsWith(BaseServerName))
                {
                    value = parms[key];
                    if (key.Equals(BaseServerName))
                        current = true;
                    Serverlist.Add(new Databases() { Key = key, DisplayName = value, Current = current });
                    current = false;
                }
                if (key.StartsWith(BaseMachineName))
                {
                    value = parms[key];
                    if (key.Equals(BaseMachineName))
                        current = true;
                    Mlist.Add(new Databases() { Key = key, DisplayName = value, Current = current });
                    current = false;
                }
                if (key.StartsWith(BaseInitialCatalog))
                {
                    value = parms[key];
                    if (key.Equals(BaseInitialCatalog))
                        current = true;
                    CatalogList.Add(new Databases() { Key = key, DisplayName = value, Current = current });
                    current = false;
                }
            }
            ServerSources = (Databases[])Serverlist.ToArray(typeof(Databases));
            Msources = (Databases[])Mlist.ToArray(typeof(Databases));
            DBsources = (Databases[])CatalogList.ToArray(typeof(Databases));
        }
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

        private void cmdChangeDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabases.SelectedIndex >= 0)
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
            if (!Save)
                return;
            SaveParms();
        }

        private void SaveParms()
        { 
            if ((bool)chkPermanent.IsChecked.Value)
            {
                foreach (Databases data in DBsources)
                {
                    Main.parms.removeItem(data.Key);

                }
            }
            int i = 0;
            string newMachine;
            string newDB;
            // Set MachineName
            foreach (Databases data in Msources)
            {
                if (data.Current)
                {
                    if ((bool)chkPermanent.IsChecked.Value)
                        Main.parms.setItem(BaseMachineName, data.DisplayName);
                    GlobalShared.MachineName = data.DisplayName;
                }
                else if ((bool)chkPermanent.IsChecked.Value)
                    Main.parms.setItem(string.Format("{0}{1:##}", BaseMachineName, ++i), data.DisplayName);
            }
            // Set Server
            foreach (Databases data in ServerSources)
            {
                if (data.Current)
                {
                    if ((bool)chkPermanent.IsChecked.Value)
                        Main.parms.setItem(BaseServerName, data.DisplayName);
                    GlobalShared.DatabaseSource = data.DisplayName;
                }
                else if ((bool)chkPermanent.IsChecked.Value)
                    Main.parms.setItem(string.Format("{0}{1:##}", BaseServerName, ++i), data.DisplayName);
            }
            // Set InitialCatalog
            foreach (Databases data in DBsources)
            {
                if (data.Current)
                {
                    if ((bool)chkPermanent.IsChecked.Value)
                        Main.parms.setItem(BaseInitialCatalog, data.DisplayName);
                    GlobalShared.InitialCatalog = data.DisplayName;
                }
                else if ((bool)chkPermanent.IsChecked.Value)
                    Main.parms.setItem(string.Format("{0}{1:##}", BaseInitialCatalog, ++i), data.DisplayName);
            }
            Main.SetDatabase(false);
            GlobalShared.CN.ConnectionString = GlobalShared.SqlConnectionString;
            Main.mSession.setItem("connectionString", GlobalShared.SqlConnectionString);
            Main.setMainWindowTitle();

            if ((bool)chkPermanent.IsChecked.Value)
                Main.parms.setConfigs();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save = true;
            this.Close();
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Create new Database?  All changes will be saved.", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            InputTextBox.Text = "";
            InputBox.Visibility = Visibility.Visible;
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
            SqlConnection targetConn = null;
            try
            {
                targetConn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create DB Connection: " + ex.Message);
                return;
            }
            NewDatabaseName = txtNewDatabaseName.Text;
            if (Main.CreateDatabase(targetConn, NewDatabaseName))
            {
                connectionString = "Server=" + server + ",1433;Initial Catalog=" + clientRecord.InitialCatalog + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
                SqlConnection sourceConn = new SqlConnection(connectionString);
                connectionString = "Server=" + server + ",1433;Initial Catalog=" + NewDatabaseName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
                targetConn = new SqlConnection(connectionString);
                Main.CopyDatabase(sourceConn, targetConn);
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                Array.Resize<Databases>(ref DBsources, DBsources.Length + 1);
                int i = DBsources.Length - 1;
                DBsources[i] = new Databases() { DisplayName = NewDatabaseName, Key = BaseInitialCatalog + "99" , Current= false };
                lstDatabases.ItemsSource = null;
                lstDatabases.ItemsSource = DBsources;
                SaveParms();
            }
            else
                MessageBox.Show("Unable to create database.");
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InputTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ( (e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) )
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
        }
    }
    public class Databases
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string FullValue { get; set; }
        public bool Current { get; set; }
    }
}
