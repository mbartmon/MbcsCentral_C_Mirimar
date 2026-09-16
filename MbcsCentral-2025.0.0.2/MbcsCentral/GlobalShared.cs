// ***********************************************************************
// Assembly         : MbcsCentral
// Author           : mike
// Created          : 11-14-2023
//
// Last Modified By : mike
// Last Modified On : 12-22-2023
// ***********************************************************************
// <copyright file="GlobalShared.cs" company="Michael Bartmon Computer Services LLC">
//     MBCS 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using MBCS.ControlLib;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;
//using Serilog;
//using Microsoft.Azure;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Table;


/// <summary>
/// The MbcsCentral namespace.
/// </summary>
namespace MbcsCentral
{
    /// <summary>
    /// Class GlobalShared.
    /// </summary>
    public static class GlobalShared
    {
        public static string MethodApp = "";

        /// <summary>
        /// The cloud application
        /// </summary>
        public static bool CloudApp;
        //public static CloudStorageAccount storageAccount;
        /// <summary>
        /// The validate connection string
        /// </summary>
        public static string ValidateConnectionString = "Provider=SQLOLEDB.1;Password=Vietnam65;Persist Security Info=True;User ID=mbartmon;Initial Catalog=MbcsMain;Data Source=MIKE-PC\\SQLEXPRESSMBCS";
        /// <summary>
        /// The connection type
        /// </summary>
        public static string ConnectionType = "AccessConnectionString";
        /// <summary>
        /// The SQL base connection string
        /// </summary>
        public static string SqlBaseConnectionString = "Data Source=<source>;Initial Catalog=<catalog>;User Id=<user>;Password=<password>;Encrypt=True;Trusted_Connection=False;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;";
        /// <summary>
        /// The SQL connection string
        /// </summary>
        public static string SqlConnectionString = "Data Source=MIKE-PC\\SQLEXPRESSMBCS;Initial Catalog=MbcsCentral;User Id=mike-PC\\mike;Password=Vietnam65;\"";
        /// <summary>
        /// The machine name
        /// </summary>
        public static string MachineName = "";
        /// <summary>
        /// The database source
        /// </summary>
        public static string DatabaseSource = "";
        /// <summary>
        /// The database name
        /// </summary>
        public static string DatabaseName = "";
        /// <summary>
        /// The initial catalog
        /// </summary>
        public static string InitialCatalog = "";
        /// <summary>
        /// The database backup location
        /// </summary>
        public static string DbBackupLocation = "";
        /// <summary>
        /// The win gl enabled
        /// </summary>
        public static bool WinGlEnabled = false;
        /// <summary>
        /// The database user name
        /// </summary>
        public const string DB_USER_NAME = "mbartmon";
        /// <summary>
        /// The database password
        /// </summary>
        public const string DB_PASSWORD = "Vietnam65";

        /// <summary>
        /// The cn
        /// </summary>
        public static SqlConnection CN;
        /// <summary>
        /// The application path
        /// </summary>
        public static string appPath;
        /// <summary>
        /// The base application path
        /// </summary>
        public static string baseAppPath;
        /// <summary>
        /// The client folder name
        /// </summary>
        public static string clientFolderName;
        /// <summary>
        /// The pm path
        /// </summary>
        public static string pmPath;
        /// <summary>
        /// The database path
        /// </summary>
        public static string dbPath;
        /// <summary>
        /// The form key loc
        /// </summary>
        public static string formKeyLoc = "SOFTWARE\\MBCS\\<assm>\\Forms\\";
        /// <summary>
        /// The SMTP key loc
        /// </summary>
        public const string smtpKeyLoc = "SOFTWARE\\MBCS\\MBCSCentral\\SMTP\\";
        /// <summary>
        /// The pop key loc
        /// </summary>
        public const string popKeyLoc = "SOFTWARE\\MBCS\\MBCSCentral\\POP\\";
        /// <summary>
        /// The options key loc
        /// </summary>
        public const string optionsKeyLoc = "SOFTWARE\\MBCS\\MBCSCentral\\Options\\";
        /// <summary>
        /// The validation key loc
        /// </summary>
        public const string validationKeyLoc = "SOFTWARE\\MBCS\\MbcsCentral\\Settings";
        /// <summary>
        /// The RDP folder
        /// </summary>
        public const string RdpFolder = "RdpFolder";
        /// <summary>
        /// The log
        /// </summary>
        public static MbcsUtils.LogClass Log;
        /// <summary>
        /// The central log
        /// </summary>
        public static MbcsUtils.LogClass CentralLog;

        /// <summary>
        /// The script paths
        /// </summary>
        public static string ScriptPaths = "DbRepairScripts\\CreateTables,DbRepairScripts\\AlterTables";
        /// <summary>
        /// The script repair log
        /// </summary>
        public static string ScriptRepairLog = "Logs\\DbRepair.log";
        //public static CSharp.NewLogClass Log;
        //public static CSharp.NewLogClass CentralLog;
        /// <summary>
        /// The log file path
        /// </summary>
        public static string logFilePath = "Logs\\";
        /// <summary>
        /// The log file name
        /// </summary>
        public static string logFileName = "MbcsCentral.log";
        // The master logged on user object *MUST BE INSTANTIATED BEFORE ANY OTHER OPERATIONS CAN BE PERFORMED*
        /// <summary>
        /// The user
        /// </summary>
        public static Users user;
        // values needed by CreatNewUser
        /// <summary>
        /// The client name
        /// </summary>
        public static string clientName = "";   // description
        /// <summary>
        /// The FQN
        /// </summary>
        public static string FQN = ""; // server
        /// <summary>
        /// The domain
        /// </summary>
        public static string domain = ""; // domain
        /// <summary>
        /// The user name
        /// </summary>
        public static string userName = "";
        /// <summary>
        /// The password
        /// </summary>
        public static string password = "[lRmw9F,.*";
        /// <summary>
        /// The null date
        /// </summary>
        public static System.DateTime NULL_DATE = new DateTime(1900, 1, 1, 0, 0, 0); // "1/1/1900 12:00:00 AM";
        /// <summary>
        /// The is changed
        /// </summary>
        public static bool IsChanged = true;
        /// <summary>
        /// The history
        /// </summary>
        public static ListCollectionView history;
        /// <summary>
        /// The menu font famliy
        /// </summary>
        public static string menuFontFamliy = "Microsoft Sans Serif";
        /// <summary>
        /// The main window
        /// </summary>
        public static MbcsCentral.MainWindow mainWindow = null;
        /// <summary>
        /// The copy database source connectionstring
        /// </summary>
        public static string COPY_DB_SOURCE_CONNECTIONSTRING = "";
        /// <summary>
        /// The copy database target connectionstring
        /// </summary>
        public static string COPY_DB_TARGET_CONNECTIONSTRING = "";

        // Primary Screen size
        /// <summary>
        /// The screen height
        /// </summary>
        public static double Screen_Height;
        /// <summary>
        /// The screen width
        /// </summary>
        public static double Screen_Width;

        /// <summary>
        /// For use when instantiating DB table classes with alternate table names in static constructor
        /// </summary>
        public static string Table = "";

        // ***************************
        // * Configuration Constants *
        // ***************************
        /// <summary>
        /// The configuration first time file
        /// </summary>
        public const string CONFIG_FIRST_TIME_FILE = "FirstTime.txt";
        /// <summary>
        /// The configuration cloud file name
        /// </summary>
        public const string CONFIG_CLOUD_FILE_NAME = "CloudApp.config";
        /// <summary>
        /// The configuration local user folder name
        /// </summary>
        public const string CONFIG_LOCAL_USER_FOLDER_NAME = "UserConfig";
        /// <summary>
        /// The configuration main filename
        /// </summary>
        public const string CONFIG_MAIN_FILENAME = "MbcsCentral.config";
        /// <summary>
        /// The configuration database backup filename
        /// </summary>
        public const string CONFIG_DB_BACKUP_FILENAME = "SqlBackup.bat";
        /// <summary>
        /// The configuration base catalog
        /// </summary>
        public const string CONFIG_BASE_CATALOG = "baseCatalog";
        /// <summary>
        /// The configuration base server
        /// </summary>
        public const string CONFIG_BASE_SERVER = "baseServer";
        /// <summary>
        /// The configuration database reg key
        /// </summary>
        public const string CONFIG_DATABASE_REG_KEY = "Database";
        /// <summary>
        /// The configuration database source
        /// </summary>
        public const string CONFIG_DATABASE_SOURCE = "databaseSource";
        /// <summary>
        /// The configuration machine name
        /// </summary>
        public const string CONFIG_MACHINE_NAME = "MachineName";
        /// <summary>
        /// The configuration available databases
        /// </summary>
        public const string CONFIG_AVAILABLE_DATABASES = "availableDatabases";
        /// <summary>
        /// The configuration database reg folder
        /// </summary>
        public const string CONFIG_DATABASE_REG_FOLDER = "Folder";
        /// <summary>
        /// The configuration database reg name
        /// </summary>
        public const string CONFIG_DATABASE_REG_NAME = "Name";
        /// <summary>
        /// The configuration winpm default printer
        /// </summary>
        public const string CONFIG_WINPM_DEFAULT_PRINTER = "winpm.DefaultPrinter";
        /// <summary>
        /// The configuration winpm charge summaries
        /// </summary>
        public const string CONFIG_WINPM_CHARGE_SUMMARIES = "winpm.Charge";
        /// <summary>
        /// The configuration winpm collected from
        /// </summary>
        public const string CONFIG_WINPM_COLLECTED_FROM = "winpm.Collected";
        /// <summary>
        /// The configuration winpm condense print
        /// </summary>
        public const string CONFIG_WINPM_CONDENSE_PRINT = "winpm.CondensePrint";
        /// <summary>
        /// The configuration winpm short form
        /// </summary>
        public const string CONFIG_WINPM_SHORT_FORM = "winpm.ShortForm";
        /// <summary>
        /// The configuration winpm print quality
        /// </summary>
        public const string CONFIG_WINPM_PRINT_QUALITY = "winpm.PrintQuality";
        /// <summary>
        /// The configuration winpm bold print
        /// </summary>
        public const string CONFIG_WINPM_BOLD_PRINT = "winpm.BoldPrint";
        /// <summary>
        /// The configuration winpm grand totals
        /// </summary>
        public const string CONFIG_WINPM_GRAND_TOTALS = "winpm.GrandTotals";
        /// <summary>
        /// The configuration winpm unit totals
        /// </summary>
        public const string CONFIG_WINPM_UNIT_TOTALS = "winpm.UnitTotals";
        /// <summary>
        /// The configuration winpm fontsize
        /// </summary>
        public const string CONFIG_WINPM_FONTSIZE = "winpm.Fontsize";
        /// <summary>
        /// The configuration winpm fontname
        /// </summary>
        public const string CONFIG_WINPM_FONTNAME = "winpm.Fontname";
        /// <summary>
        /// The configuration winpm pagesize
        /// </summary>
        public const string CONFIG_WINPM_PAGESIZE = "winpm.Pagesize";
        /// <summary>
        /// The configuration winpm lease letter
        /// </summary>
        public const string CONFIG_WINPM_LEASE_LETTER = "WinPM.LeaseLetter";
        /// <summary>
        /// The configuration winpm duplex file
        /// </summary>
        public const string CONFIG_WINPM_DUPLEX_FILE = "WinPM.DuplexFile";
        /// <summary>
        /// The configuration winpm prefrent file
        /// </summary>
        public const string CONFIG_WINPM_PREFRENT_FILE = "WinPM.PrefRentDoc";

        // *******************
        // * Other Constants *
        // *******************
        /// <summary>
        /// The call form parms class
        /// </summary>
        public const string CALL_FORM_PARMS_CLASS = "CallFormParms";
        /// <summary>
        /// The event log source
        /// </summary>
        public const string EventLogSource = "MbcsCentral";
        /// <summary>
        /// The event log name
        /// </summary>
        public const string EventLogName = "MbcsCentral";
        /// <summary>
        /// The sp backup database
        /// </summary>
        public const string SP_BACKUP_DB = "sp_BackupDatabases";
        /// <summary>
        /// The vintasoft regcode
        /// </summary>
        public const string VINTASOFT_REGCODE = "iM81plxE6CVsskOI1dptaZER42PRcNC3IK1T3t45UafUusLPat58U5IVcZELYpdKOPwPhsJWKxvezfCzqAJJk19kXwd3sgyA+QWFYogBUEMrY8PF/Q1Ax1mxVzYHBd/No/oyAM3ARJk2dcDMCbmZGX0OgO05sSI0piBWArMt+SZs";
        /// <summary>
        /// The vintasoft email
        /// </summary>
        public const string VINTASOFT_EMAIL = "mike@bartmon.com";
        /// <summary>
        /// The vintasoft user
        /// </summary>
        public const string VINTASOFT_USER = "Michael Bartmon";

        /// <summary>
        /// The menu font size
        /// </summary>
        public static double menuFontSize = 24.0;
        // *********************************
        // * Background Task configuration *
        // *********************************

        // 1 minute
        /// <summary>
        /// The wait interval
        /// </summary>
        public static TimeSpan WAIT_INTERVAL = new TimeSpan(0, 0, 30);
        // Tenant Names
        /// <summary>
        /// The tenant names timer
        /// </summary>
        public static System.Threading.Timer tenantNamesTimer;
        // 10 minutes
        /// <summary>
        /// The tenant names interval
        /// </summary>
        public const long TENANT_NAMES_INTERVAL = 10 * 60 * 1000;
        /// <summary>
        /// The tenant name in delegate
        /// </summary>
        public static bool tenantNameInDelegate = false;
        /// <summary>
        /// The tenant names
        /// </summary>
        public static MbcsCentral.WinPM.Tenants[] tenantNames = null;
        /// <summary>
        /// The tenant number list
        /// </summary>
        public static string[] TenantNumberList = {"","","",""};

        // Application Constants
        /// <summary>
        /// The appname
        /// </summary>
        public const string APPNAME = "MbcsCentral";
        /// <summary>
        /// The application main reg key
        /// </summary>
        public const string APP_MAIN_REG_KEY = "SOFTWARE\\MBCS\\" + APPNAME;
        /// <summary>
        /// The application wintrm reg
        /// </summary>
        public const string APP_WINTRM_REG = "SOFTWARE\\MBCS\\WinTRM\\Options\\DB Locations";
        /// <summary>
        /// The application wintrm reg key
        /// </summary>
        public const string APP_WINTRM_REG_KEY = "WinTrmPath";
        /// <summary>
        /// The application wintrm
        /// </summary>
        public const string APP_WINTRM = "wintrm.exe";
        /// <summary>
        /// The application wintrm name
        /// </summary>
        public const string APP_WINTRM_NAME = "WinTRM";
        /// <summary>
        /// The application wintrm database prefix
        /// </summary>
        public const string APP_WINTRM_DB_PREFIX = "trm_";
        /// <summary>
        /// The application winpm
        /// </summary>
        public const string APP_WINPM = "winpm.exe";
        /// <summary>
        /// The application winpm name
        /// </summary>
        public const string APP_WINPM_NAME = "WinPM";
        /// <summary>
        /// The application winpm database prefix
        /// </summary>
        public const string APP_WINPM_DB_PREFIX = "pm_";
        /// <summary>
        /// The application wingl
        /// </summary>
        public const string APP_WINGL = "wingl.exe";
        /// <summary>
        /// The application wingl name
        /// </summary>
        public const string APP_WINGL_NAME = "WinGL";
        /// <summary>
        /// The application wingl database prefix
        /// </summary>
        public const string APP_WINGL_DB_PREFIX = "gl_";
        /// <summary>
        /// The application winpr
        /// </summary>
        public const string APP_WINPR = "winpr.exe";
        /// <summary>
        /// The application winpr name
        /// </summary>
        public const string APP_WINPR_NAME = "WinPR";
        /// <summary>
        /// The application winpr database prefix
        /// </summary>
        public const string APP_WINPR_DB_PREFIX = "pr_";
        /// <summary>
        /// The application windoc
        /// </summary>
        public const string APP_WINDOC = "windoc.exe";
        /// <summary>
        /// The application windoc name
        /// </summary>
        public const string APP_WINDOC_NAME = "WinDoc";
        /// <summary>
        /// The application windoc database prefix
        /// </summary>
        public const string APP_WINDOC_DB_PREFIX = "doc_";
        /// <summary>
        /// The application leasemaker
        /// </summary>
        public const string APP_LEASEMAKER = "leasemaker.exe";
        /// <summary>
        /// The application leasemaker name
        /// </summary>
        public const string APP_LEASEMAKER_NAME = "LeaseMaker";
        /// <summary>
        /// The application leasemaker database prefix
        /// </summary>
        public const string APP_LEASEMAKER_DB_PREFIX = "lm_";
        /// <summary>
        /// The masteruser
        /// </summary>
        public const long MASTERUSER = 0;
        /// <summary>
        /// The adminuser
        /// </summary>
        public const int ADMINUSER = 9;

        // Parameter Constants
        /// <summary>
        /// The parm connectionstring
        /// </summary>
        public const string PARM_CONNECTIONSTRING = "connectionString";
        /// <summary>
        /// The parm wintrm flag
        /// </summary>
        public const string PARM_WINTRM_FLAG = "wintrmFlag";
        /// <summary>
        /// The parm wintrm connectionstring
        /// </summary>
        public const string PARM_WINTRM_CONNECTIONSTRING = "alertsConnectionString";
        /// <summary>
        /// The parm windoc flag
        /// </summary>
        public const string PARM_WINDOC_FLAG = "windocFlag";

        /// <summary>
        /// The parm windoc connectionstring
        /// </summary>
        public const string PARM_WINDOC_CONNECTIONSTRING = "windocConnectionString";

        /// <summary>
        /// The database copy status
        /// </summary>
        public static frmProgressWindow.ProgressData DbCopyStatus;

        // ******************
        // E-Mail Constants
        // ******************
        /// <summary>
        /// Enum MAIL_TAGS_ENUM
        /// </summary>
        public enum MAIL_TAGS_ENUM
        {
            /// <summary>
            /// The tenant direct
            /// </summary>
            TenantDirect,
            /// <summary>
            /// The tenant late notice
            /// </summary>
            TenantLateNotice,
            /// <summary>
            /// The ach notices
            /// </summary>
            ACHNotices,
            /// <summary>
            /// The rent statements
            /// </summary>
            RentStatements
        }
        /// <summary>
        /// The mail tags
        /// </summary>
        public static string[] MAIL_TAGS = { "TenantDirect", "TenantLateNotice", "ACHNotices", "RentStatements" };
        /// <summary>
        /// The mail filename
        /// </summary>
        public static string mailFilename = "{0}{1:00000}_{2:00000}_{3:yyyyMMddhhmmss}.{4}"; // Tenant Seq number, 0, data/time, extension
        /// <summary>
        /// The email folder
        /// </summary>
        public const string EMAIL_FOLDER = "Emails\\";
        /// <summary>
        /// The email folder in
        /// </summary>
        public const string EMAIL_FOLDER_IN = "In\\";
        /// <summary>
        /// The email folder out
        /// </summary>
        public const string EMAIL_FOLDER_OUT = "Out\\";
        /// <summary>
        /// The email latenotice
        /// </summary>
        public const string EMAIL_LATENOTICE = "LateNotice";
        /// <summary>
        /// Enum NoticeInsertionFields
        /// </summary>
        public enum NoticeInsertionFields
        {
            /// <summary>
            /// The pay date
            /// </summary>
            PayDate,
            /// <summary>
            /// The invoice
            /// </summary>
            Invoice,
            /// <summary>
            /// The amount
            /// </summary>
            Amount,
            /// <summary>
            /// The account
            /// </summary>
            Account,
            /// <summary>
            /// The company
            /// </summary>
            Company,
            /// <summary>
            /// The vendor name
            /// </summary>
            VendorName
        };
        /// <summary>
        /// The notice insertion field names
        /// </summary>
        public static string[] NoticeInsertionFieldNames = { "[PayDate]", "[Invoice]", "[Amount]", "[Account]", "[Company]", "[VendorName]" };


        [System.STAThreadAttribute()]
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        /// <summary>
        /// GlobalShared.Main Application Startup Method
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <remarks><b>Retrieves application path and stores it in the global variable GlobalShared.appPath.
        /// <para>Instantiates Central Log.</para><para>Determines if application is runing in the cloud. DEPRECATED</para><para>Passes control to MainWindow for continued startup processing</para></b></remarks>
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (args.Length > 0)
                GlobalShared.userName = args[0];
            if (args.Length > 1)
                GlobalShared.password = args[1];
            MbcsCentral.App app = new MbcsCentral.App();
            GlobalShared.appPath = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!GlobalShared.appPath.EndsWith("\\"))
            {
                GlobalShared.appPath += "\\";
            }
            GlobalShared.baseAppPath = GlobalShared.appPath;

            MbcsCentral.GlobalShared.CentralLog = MbcsUtils.LogClass.instance;
            GlobalShared.CentralLog.EventSource = GlobalShared.EventLogSource;
            GlobalShared.CentralLog.EventLog = GlobalShared.EventLogName;
            GlobalShared.CentralLog.Client = "Startup";
            GlobalShared.CentralLog.logFileName = GlobalShared.baseAppPath + GlobalShared.logFilePath + MbcsCentral.GlobalShared.logFileName;

            // Runing in the Cloud or Locally
            GlobalShared.CloudApp = false;
            if (File.Exists(GlobalShared.baseAppPath + GlobalShared.CONFIG_CLOUD_FILE_NAME))
            {
                MbcsUtils.Parameters p = new MbcsUtils.Parameters(GlobalShared.baseAppPath + "CloudApp.config");
                p.getConfigs();
                string asCloud = "";
                asCloud = p.GetString("CloudApp");
                string AccountKey = p.GetString("AzureStorageKey");
                if (asCloud == null || asCloud.Trim().Equals("") || asCloud.ToLower().Trim() == "false" || AccountKey == null || AccountKey.Trim().Equals(""))
                {
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "Found CloudApp.config", false);
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "asCloud=" + asCloud, false);
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "Running as local App", false);
                    GlobalShared.clientFolderName = GlobalShared.CONFIG_LOCAL_USER_FOLDER_NAME;
                }
                else
                {
                    GlobalShared.CloudApp = true;
                    GlobalShared.clientFolderName = GlobalShared.clientName;
                    string connectionkey = "storageAccountKey";
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "Found CloudApp.config", false);
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "asCloud=" + asCloud, false);
                }
            }
            else
            {
                GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "Did not find CloudApp.config", false);
                GlobalShared.clientFolderName = GlobalShared.CONFIG_LOCAL_USER_FOLDER_NAME;
            }

            GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "About to load MainWindow", false);
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
            var size = HelperMethods.GetHostingScreenSize(screen.DeviceName);
            Screen_Height = size.Height;
            Screen_Width = size.Width;

            MainWindow frm = null;
            frm = new MainWindow();
            try
            {
            }
            catch (Exception ex)
            {
                GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "Error loading MainWindow", false);
                System.Windows.Application.Current.Shutdown();
            }

            try
            {
                frm.ShowDialog();
            }
            catch (AggregateException ax)
            { }
           catch (Exception ex) 
           {
                MessageBoxCustom.Show("Unhandled exception, shutting down: " + ex.Message + "\r\n\n" + ex.StackTrace);
               GlobalShared.CentralLog.Log((int)LogClass.logType.ErrorCondition, "Unhandled exception, shutting down: " + ex.Message + "\r\n\n" + ex.StackTrace, false );
               System.Windows.Application.Current.Shutdown();
           }
        }

        /// <summary>
        /// Finds the running process.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns>System.Int32.</returns>
        public static int FindRunningProcess (string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes != null && processes.Length > 0)
                return processes[0].Id;
            else
                return 0;
        }

        /// <summary>
        /// Closes the running process.
        /// </summary>
        /// <param name="pid">The pid.</param>
        /// <param name="kill">if set to <c>true</c> [kill].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CloseRunningProcess(int pid, bool kill = false)
        {
            Process process = null;
            try
            {
                process = Process.GetProcessById(pid);
            }
            catch (ArgumentException ex)
            {
                return true;
            }

            if (kill)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(5000);
                    return true;
                }
                catch (Exception ex) { }
                return false;
            }
            else
            {
                int count = 0;

                try
                {
                    using (process)
                    {
                        while (!process.HasExited && count < 5)
                        {
                            process.CloseMainWindow();
                            Thread.Sleep(2000);
                            count++;
                        }
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());

        }

        /// <summary>
        /// Selects the index of the cell by.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <exception cref="System.ArgumentException">The SelectionUnit of the DataGrid must be set to Cell.</exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void SelectCellByIndex(DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell))
                throw new ArgumentException("The SelectionUnit of the DataGrid must be set to Cell.");

            if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

            if (columnIndex < 0 || columnIndex > (dataGrid.Columns.Count - 1))
                throw new ArgumentException(string.Format("{0} is an invalid column index.", columnIndex));

            dataGrid.SelectedCells.Clear();

            object item = dataGrid.Items[rowIndex]; //=Product X
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                dataGrid.ScrollIntoView(item);
                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            if (row != null)
            {
                DataGridCell cell = GetCell(dataGrid, row, columnIndex);
                if (cell != null)
                {
                    //cell.IsSelected = true;
                    try
                    {
                        //DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(cell);
                        //dataGrid.SelectedCells.Add(dataGridCellInfo);
                        dataGrid.CurrentCell = new DataGridCellInfo(cell);
                    }
                    catch { }
                    cell.Focus();
                    dataGrid.BeginEdit();
                }
            }
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="rowContainer">The row container.</param>
        /// <param name="column">The column.</param>
        /// <returns>DataGridCell.</returns>
        public static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the visual child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>T.</returns>
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        /// <summary>
        /// Validates the routing number.
        /// </summary>
        /// <param name="num">The number.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool validateRoutingNumber(string num )
        {
            // Run through each digit and calculate the total.

            int n = 0;
            for (int i = 0; i < num.Length; i += 3)
            {
                try
                {
                    n += int.Parse(num.Substring(i, 1)) * 3
                      + int.Parse(num.Substring(i + 1, 1)) * 7
                      + int.Parse(num.Substring(i + 2, 1));
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            // If the resulting sum is an even multiple of ten (but not zero),
            // the aba routing number is good.
            if (n != 0 && n % 10 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

namespace Tips
{
    /// <summary>
    /// Class StaticConstants.
    /// </summary>
    public class StaticConstants
    {
        /// <summary>
        /// The hold tip
        /// </summary>
        public static readonly string HoldTip = "Checking this will keep the value as long as the screen is not EXITED.";
        /// <summary>
        /// The save tip
        /// </summary>
        public static readonly string SaveTip = "Checkning this will save the value permanently.";
    }

}
