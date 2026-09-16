using System;
using System.Collections;
using System.Collections.Generic;
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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmDatabasesEdit.xaml
    /// </summary>
    public partial class frmDatabasesEdit : Window
    {
        private const string BASEDATABASE = "Database";
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
            BaseServer = (string)obj;
            Main.parms.getItem(GlobalShared.CONFIG_BASE_CATALOG, ref obj);
            if (obj == null)
            {
                MessageBox.Show("Unable to find Base Catalog.  Cannot create database.");
                return;
            }
            folders = Main.RetrieveDbFolders();
            BaseDatabase = (string)obj;
            LoadDictionary();
            lstSources.ItemsSource = DBsources;
            lstTargets.ItemsSource = DBsources;
        }

        private void LoadDictionary()
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
                    parts = value.Split(';');
                    if (parts.Length == 2)
                    {
                        if (parts[0].ToLower().Equals(BaseServer.ToLower()) && parts[1].ToLower().Equals(BaseDatabase.ToLower()))
                            current = true;
                        DatabaseList.Add(new Databases() { Key = key, DisplayName = value, Current = current, FullValue = parts[1] });
                        current = false;
                    }
                }
            }
            DBsources = (Databases[])DatabaseList.ToArray(typeof(Databases));
        }
        private void formDatabaseEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

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

            if (MessageBox.Show("This operation will erase all data in the target database.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            string connectionString = "Server=" + source.FullValue + ";Initial Catalog=" + source.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection sourceConn = new SqlConnection(connectionString);
            try
            {
                sourceConn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unable to open Source Database: " + ex.Message);
                return;
            }
            sourceConn.Close();
            connectionString = "Server=" + target.FullValue + ";Initial Catalog=" + target.DisplayName + "; User Id=mbartmon; Password=Vietnam65; Encrypt=True; Trusted_Connection=False; TrustServerCertificate=True;Integrated Security=False; Connection Timeout=60";
            SqlConnection targetConn = new SqlConnection(connectionString);
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
            if (Main.CreateDatabase(targetConn, target.DisplayName, true))
                Main.CopyDatabase(sourceConn, targetConn);

        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
