using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Win32;
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
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Smo;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmDatabaseBackup.xaml
    /// </summary>
    public partial class frmDatabaseBackup : Window
    {

        string backupQuery = "SELECT " +
                    "bkSet.Backup_Start_Date as BackupStartDate," + 
                    "bkSet.Backup_Finish_Date as BackupEndDate," +
                    "MedFam.Physical_Device_Name AS BackupPath," +
                    "MedSet.Software_Name AS SoftwareUsedForBackup," +
                    "bkSet.User_Name AS BackupTakenBy," +
                    "bkSet.Server_Name AS ServerName," +
                    "bkSet.Database_Name As DatabaseName, " +
                    "CASE bkSet.Type " +    
                            "WHEN 'L' THEN 'TransactionLogBackup' " +
                            "WHEN 'D' THEN 'FullBackup' " +
                            "WHEN 'F' THEN 'FileBackup' " +
                            "WHEN 'I' THEN 'DifferentialBackup' " +
                        "WHEN 'G' THEN 'DifferentialFileBackup' " +
                        "WHEN 'P' THEN 'PartialBackup' " +
                        "WHEN 'Q' THEN 'DifferentialPartialBackup' " +
                        "ELSE NULL END AS BackupType, " +
                    "CAST((bkSet.Backup_Size/1048576) AS NUMERIC(10, 2)) AS BackupSizeInMB " +
                "FROM " +
                      "msdb.dbo.BackupMediaFamily MedFam " +
                "INNER JOIN " +
                      "msdb.dbo.BackupMediaSet MedSet " +
                "ON " +
                      "MedFam.Media_Set_ID = MedSet.Media_Set_ID " +
                "INNER JOIN " +
                      "msdb.dbo.BackupSet bkSet " +
                "ON " +
                      "bkSet.Media_Set_ID = MedSet.Media_Set_ID " +
                "WHERE " +
                      "bkSet.Database_Name = '<DbName>' " +
                "AND " +
                      //"bkSet.Backup_Finish_Date BETWEEN '<DateFrom>' AND '<DateTo>' " +
                      "bkSet.Backup_Finish_Date >= '<DateFrom>' " +
                "ORDER BY " +
                      "bkSet.Backup_Finish_Date DESC";

        DbControl dbcontrol;
        string databaseName = "";
        string currentDB = GlobalShared.InitialCatalog;
        private string m_thisApp;

        public frmDatabaseBackup()
        {
            InitializeComponent();
        }

        public frmDatabaseBackup(string app)
        {
            InitializeComponent();
            m_thisApp = app;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            object obj = null;
            Main.parms.getItem("baseCatalog", ref obj);
            databaseName = (string)obj;
            dbcontrol = DbControl.load();
            if (dbcontrol == null)
                dbcontrol = new DbControl();
            this.DataContext = dbcontrol;
            if (obj == null)
            {
                MessageBox.Show("The baseCatalog could not be found.");
            }
            else
            {
                string stmt = backupQuery.Replace("<DbName>", databaseName).Replace("<DateFrom>", DateTime.Now.AddDays(-dbcontrol.DbBackupPeriod).ToShortDateString()).Replace("<DateTo>", DateTime.Now.AddDays(1).ToShortDateString());
                SqlDataAdapter da = new SqlDataAdapter(stmt, GlobalShared.SqlConnectionString);
                System.Data.DataSet ds = new System.Data.DataSet("Backups");
                try
                {
                    da.Fill(ds, "Backups");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ArrayList rows = new ArrayList();
                        foreach (System.Data.DataRow dr in ds.Tables[0].Rows) 
                            rows.Add(new DbBackups(dr[0].ToString(), dr[2].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString()));
                        grdRestore.ItemsSource = ((DbBackups[])(rows.ToArray(typeof(DbBackups))));
                    }
                }
                catch (SqlException ex) { }
            }
            lstMappedDrive.ItemsSource = System.IO.DriveInfo.GetDrives();
            if (dbcontrol.DbMappedDrive != null && !dbcontrol.DbMappedDrive.Trim().Equals(""))
            {
                for (int i = 0; i < lstMappedDrive.Items.Count; i++)
                {
                    if (((System.IO.DriveInfo)lstMappedDrive.Items[i]).Name.ToLower().Equals(dbcontrol.DbMappedDrive.Trim().ToLower()))
                    {
                        lstMappedDrive.SelectedIndex = i;
                        break;
                    }
                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (lstMappedDrive.SelectedIndex >= 0)
                dbcontrol.DbMappedDrive = ((System.IO.DriveInfo)lstMappedDrive.SelectedItem).Name;
            dbcontrol.save();
        }

        private void cmdBackupManual_Click(object sender, RoutedEventArgs e)
        {
            if (txtManualLocation.Text.Trim().Equals(""))
            {
                MessageBox.Show("You must provide a backup location.");
                return;
            }
            if (lstMappedDrive.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a mapped SQL Server drive.");
                return;
            }
            string drive = ((System.IO.DriveInfo)lstMappedDrive.SelectedItem).Name;
            string backupLocation = txtManualLocation.Text;
            string backupFile = "";
            string doDatabase;

            if (chkCurrent.IsChecked.Value)
                doDatabase = currentDB;
            else
                doDatabase = databaseName;
            if (doDatabase.Trim().Equals(""))
            {
                MessageBox.Show("The database name could not be found.");
                return;
            }
            if (MessageBox.Show("Start DB Backup?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string savedLocation = GlobalShared.DbBackupLocation;
                if (!savedLocation.EndsWith("\\"))
                    savedLocation += "\\";
                string backupType = "F";
                if (GlobalShared.CN.State == System.Data.ConnectionState.Open)
                    GlobalShared.CN.Close();

                if (Main.BackupDatabase(GlobalShared.CN, doDatabase, backupType, savedLocation, out savedLocation, out backupFile, true, chkOverwrite.IsChecked.Value) && !savedLocation.Trim().Equals(""))
                {
                    // copy backup file to local directory
                    // savedLocation to backupLocation
                    if (!backupLocation.EndsWith("\\"))
                        backupLocation += "\\";
                    try
                    {
                        savedLocation = drive + savedLocation.Substring(2);
                        System.IO.File.Copy(savedLocation, backupLocation + backupFile, true);
                        string message = string.Format("{0}\n\n   {1}\n   {2}\n   {3}", "SUCCESSFUL backup of ", doDatabase, " TO ", backupLocation + backupFile);
                        MessageBox.Show(message);
                    }
                    catch (System.IO.IOException ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to copy backup file to <" + backupLocation + backupFile + ">: " + ex.Message, System.Windows.Forms.MessageBoxButtons.OK);
                        if (ex.InnerException != null)
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "    " + ex.InnerException.Message, System.Windows.Forms.MessageBoxButtons.OK);
                    }
                }
                else
                {
                    string message = string.Format("{0}\n\n   {1}\n   {2}\n   {3}", "UNSUCCESSFUL backup of ", doDatabase, " TO ", backupLocation + backupFile);
                    MessageBox.Show(message);
                }
            }
        }

        private void cmdRestoreManual_Click(object sender, RoutedEventArgs e)
        {
            if (databaseName.Trim().Equals(""))
            {
                MessageBox.Show("The baseCatalog could not be found.");
                return;
            }
            if (txtManualLocation.Text.Trim().Equals(""))
            {
                MessageBox.Show("You must provide a backup location.");
                return;
            }
            if (lstMappedDrive.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a mapped SQL Server drive.");
                return;
            }
            string drive = ((System.IO.DriveInfo)lstMappedDrive.SelectedItem).Name;
            string backupLocation = txtManualLocation.Text;
            if (!backupLocation.EndsWith("\\"))
                backupLocation += "\\";
            string backupFile = "";
            if (MessageBox.Show("IMPORTANT: Make sure that there are \n no users logged on to the database.\n\n Start DB Backup?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = backupLocation;
                dlg.Filter = "Backup Files(.bak|*.bak)";
                if (!(bool)dlg.ShowDialog())
                    return;
                if (dlg.FileName.Trim() == "")
                    return;
                backupLocation = dlg.FileName;
                if (GlobalShared.CN.State == System.Data.ConnectionState.Open)
                    GlobalShared.CN.Close();
                Main.RestoreDatabase(GlobalShared.SqlConnectionString, databaseName, backupLocation);
            }
        }

        private void cmdManualLocation_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            dlg.Description = "Select folder into which to save the DB backup file.";
            if (!txtManualLocation.Text.Trim().Equals(""))
            {
                string path = new System.IO.FileInfo(txtManualLocation.Text).Directory.Name;
                dlg.SelectedPath = path;
            }
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtManualLocation.Text = dlg.SelectedPath;

        }
        

        private void cmdAutoSave_Click(object sender, RoutedEventArgs e)
        {
            if (lstMappedDrive.SelectedIndex >= 0)
                dbcontrol.DbMappedDrive = ((System.IO.DriveInfo)lstMappedDrive.SelectedItem).Name;
            if (dbcontrol.save())
                MessageBox.Show("Auto Backup Settings saved.");
            else
                MessageBox.Show("Unable to save Auto Backup Settings.");
        }

        private void cmdMappedDrive_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdAutoLocation_Click(object sender, RoutedEventArgs e)
        {
            string backupLocation = "";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = GlobalShared.CN;
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.CommandText = "DECLARE @BackupDirectory NVARCHAR(100) EXEC master..xp_instance_regread @rootkey = 'HKEY_LOCAL_MACHINE', @key = 'Software\\Microsoft\\MSSQLServer\\MSSQLServer', @value_name = 'BackupDirectory', @BackupDirectory = @BackupDirectory OUTPUT;  SELECT @BackupDirectory";
            try
            {
                sqlCmd.Connection.Open();
                backupLocation = (string)sqlCmd.ExecuteScalar();
                sqlCmd.Connection.Close();
                txtAutoLocation.Text = backupLocation;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to find backup location: " + ex.Message);
            }
            
        }

        private void ctxStartRestore_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = grdRestore;
            if (grd != null && grd.SelectedIndex >= 0)
            {
                DbBackups dbDiff = null;
                DbBackups dbFull = null;
                DbBackups[] dbs = (DbBackups[])grd.ItemsSource;
                int index = grd.SelectedIndex;
                bool full = false;
                string str = String.Format("{0}\t:{1}<{2}>\n\n", "The following backup will be restored:", dbs[index].BackupStartDate, dbs[index].BackupType);
                if (dbs[index].BackupType.ToLower().Contains("full"))
                {
                    str += string.Format("\t:{0}", "Since it was a FULL backup, only that one backup file will be restored.");
                    dbFull = dbs[index];
                    full = true;
                }
                else
                {
                    if (index > 0)
                    {
                        for (int j = index; j >= 0; j++)
                        {
                            if (dbs[j].BackupType.ToLower().Contains("full"))
                            {
                                dbDiff = dbs[index];
                                dbFull = dbs[j];
                                break;
                            }
                        }
                    }
                    if (index == 0 || dbFull == null)
                    {
                        MessageBox.Show("There is no FULL backup found prior to this DIFFERENTIAL. Cannot restore.");
                        return;
                    }
                    try
                    {
                        Server srv = new Server("mike-pc\\mbcscentralsql");
                        Restore res = new Restore();
                        DataTable dtDiff = null;
                        DataTable dtFull = null;
                        res.Devices.AddDevice(dbDiff.BackupPath, DeviceType.File);
                        dtDiff = res.ReadBackupHeader(srv);
                        res = new Restore();
                        res.Devices.AddDevice(dbFull.BackupPath, DeviceType.File);
                        dtFull = res.ReadBackupHeader(srv);
                        if ((decimal)dtDiff.Rows[0].ItemArray[16] != (decimal)dtFull.Rows[0].ItemArray[15])
                        {
                            MessageBox.Show("There is no matching FULL <" + dtFull.Rows[0].ItemArray[15].ToString() + "> backup /n found prior to this DIFFERENTIAL <" + dtDiff.Rows[0].ItemArray[15].ToString() + ">. /nCannot restore. Try the most recent Full Backup.");
                            return;
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    catch ( Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    str += String.Format("{0}\n{1}", "Since it was a DIFERENTIAL backup, the previous FULL backup and", "that DIFFERENTIAL backup will be restored.");
                }
                if (MessageBox.Show(str, "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return;
                if (GlobalShared.CN.State == System.Data.ConnectionState.Open)
                    GlobalShared.CN.Close();
                if (full)
                    if (Main.RestoreDatabase(GlobalShared.SqlConnectionString, databaseName, dbs[index].BackupPath, true))
                        MessageBox.Show("Database RESTORATION completed successfully.");
                    else
                        MessageBox.Show("Database RESTORATION unsuccessfull.");
                else
                {
                    if (Main.RestoreDatabase(GlobalShared.SqlConnectionString, databaseName, dbFull.BackupPath, false, true, false))
                        if (Main.RestoreDatabase(GlobalShared.SqlBaseConnectionString, databaseName, dbDiff.BackupPath, true, false, true))
                            MessageBox.Show("Database RESTORATION completed successfully.");
                        else
                            MessageBox.Show("Database RESTORATION unsuccessfull.");
                    else
                        MessageBox.Show("Database RESTORATION unsuccessfull.");

                }
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

    public class DbBackups
    {
        public DbBackups() { }
        public DbBackups(string a, string b, string c, string d, string e, string f, string g)
        {
            BackupStartDate = a;
            BackupPath = b;
            BackupTakenBy = c;
            ServerName = d;
            DatabaseName = e;
            BackupType = f;
            BackupSizeInMB = g;
        }

        public string BackupStartDate { get; set; }
        public string BackupPath { get; set; }
        public string BackupTakenBy { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string BackupType { get; set; }
        public string BackupSizeInMB { get; set; }
    }
}
