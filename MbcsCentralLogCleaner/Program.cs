using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace MbcsCentralLogCleaner
{
    class Program
    {
        private static LogClass Log;
        private static string appPath;
        private static MbcsUtils.Parameters parms;
        private const string CONFIG_FILE = "CloudApp.config";
        private const string THIS_APP_NAME = "MbcsCentralLogCleaner";
        private const string MAIN_APP_NAME = "MbcsCentral";
        private const string LOCAL_USER_DIRECTORY = "UserConfig";
        private const string PARM_KEY = "client_";
        private const string BASE_LOG_NAME = "MbcsCentral";
        private const string BASE_SERVICE_LOG_NAME = "MbcsCentralService";
        private const string BASE_CLEANER_LOG_NAME = "MbcsCentralLogCleaner";
        private const string BASE_BACKUP_LOG_NAME = "MbcsCentralDbBackup";
        private const string MBCSCENTRAL_CONFIG = "MbcsCentral.config";
        private const string DB_USER_NAME = "mbartmon";
        private const string DB_PASSWORD = "Vietnam65";
        static string SqlBaseConnectionString = "Data Source=<source>;Initial Catalog=<catalog>;User Id=<user>;Password=<password>;Encrypt=True;Trusted_Connection=False;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;";
        private const int MAX_LOG_AGE = 30;  // days
        private static DateTime CurrentDate = DateTime.Now;
        private static string cleanRegistry = "delete registry where id not in (select min(id) from registry group by acctid, mainkey, subkey)";
        //private static bool CloudApp;
        private static string[] folders;

        static void Main(string[] args)
        {
            appPath = AppDomain.CurrentDomain.BaseDirectory;
#if DEBUG
            appPath = "e:\\workfiles\\MbcsCentral_C\\MbcsCentral\\Bin\\Debug\\";
#endif
            appPath = appPath.Replace(THIS_APP_NAME, MAIN_APP_NAME);
            if (!appPath.EndsWith("\\"))
                appPath += "\\";
            Log = new LogClass();
            //Log.EventLog = GlobalShared.EventLogName;
            Log.Username = "MbcsCentralLogCleaner";
            Log.logFileName = appPath + "UserConfig\\Logs\\MbcsCentralLogCleaner.log";
            GlobalShared.Log = Log;
            Log.Log((int)LogClass.logType.Config, "Starting MbcsCentralLogCleaner.");
            GetFolderParms();
            if (folders != null && folders.Length > 0)
            {
                ProcessLogFiles();
            }
            CleanTables();
            Log.Log((int)LogClass.logType.Info, "MbcsCentralLogCleaner done.");

        }

        static void ProcessLogFiles()
        {
            string[] files;
            string[] parts;
            Log.Log((int)LogClass.logType.Info, "", false);
            Log.Log((int)LogClass.logType.Info, "*** Processing Log Files ***", false);

            // Application Log
            foreach (string folder in folders)
            {
                Log.Log((int)LogClass.logType.Info, "   <<<" + BASE_LOG_NAME + ">>>");
                if (File.Exists(appPath + folder + "\\Logs\\" + BASE_LOG_NAME + ".log"))
                {
                    Log.Log((int)LogClass.logType.Info, " <" + folder + ">", false);
                    try
                    {
                        File.Move(appPath + folder + "\\Logs\\" + BASE_LOG_NAME + ".log", appPath + folder + "\\Logs\\" + BASE_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                    }
                    catch (IOException ex)
                    {
                        Log.Log((int)LogClass.logType.ErrorCondition, "      Unable to rename log file : " + ex.Message, false);
                        continue;
                    }
                    Log.Log((int)LogClass.logType.Info, "      " + BASE_LOG_NAME + ".log renamed to " + BASE_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log", false);
                }

                // MbcsCentralDbBackup
                Log.Log((int)LogClass.logType.Info, "   <<<" + BASE_BACKUP_LOG_NAME + ">>>");
                if (File.Exists(appPath + folder + "\\Logs\\" + BASE_BACKUP_LOG_NAME + ".log"))
                {
                    Log.Log((int)LogClass.logType.Info, " <" + folder + ">", false);
                    try
                    {
                        File.Move(appPath + folder + "\\Logs\\" + BASE_BACKUP_LOG_NAME + ".log", appPath + folder + "\\Logs\\" + BASE_BACKUP_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                    }
                    catch (IOException ex)
                    {
                        Log.Log((int)LogClass.logType.ErrorCondition, "      Unable to rename log file : " + ex.Message, false);
                        continue;
                    }
                    Log.Log((int)LogClass.logType.Info, "      " + BASE_BACKUP_LOG_NAME + ".log renamed to " + BASE_BACKUP_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log", false);
                }

                // MbcsCentralService Log
                Log.Log((int)LogClass.logType.Info, "   <<<" + BASE_SERVICE_LOG_NAME + ">>>");
                if (File.Exists(appPath + folder + "\\Logs\\" + BASE_SERVICE_LOG_NAME + ".log"))
                {
                    Log.Log((int)LogClass.logType.Info, " <" + folder + ">", false);
                    try
                    {
                        File.Move(appPath + folder + "\\Logs\\" + BASE_SERVICE_LOG_NAME + ".log", appPath + folder + "\\Logs\\" + BASE_SERVICE_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                    }
                    catch (IOException ex)
                    {
                        Log.Log((int)LogClass.logType.ErrorCondition, "      Unable to rename log file : " + ex.Message, false);
                        continue;
                    }
                    Log.Log((int)LogClass.logType.Info, "      " + BASE_SERVICE_LOG_NAME + ".log renamed to " + BASE_SERVICE_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log", false);
                }

                // MbcsCentralLogCleaner Log
                if (File.Exists(appPath + folder + "\\Logs\\" + BASE_CLEANER_LOG_NAME + ".log"))
                {
                    Log.Log((int)LogClass.logType.Info, " <" + folder + ">", false);
                    try
                    {
                        File.Move(appPath + folder + "\\Logs\\" + BASE_CLEANER_LOG_NAME + ".log", appPath + folder + "\\Logs\\" + BASE_CLEANER_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                    }
                    catch (IOException ex)
                    {
                        Log.Log((int)LogClass.logType.ErrorCondition, "   Unable to rename log file : " + ex.Message, false);
                        continue;
                    }
                    Log.Log((int)LogClass.logType.Info, "   " + BASE_CLEANER_LOG_NAME + ".log renamed to " + BASE_CLEANER_LOG_NAME + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log", false);
                }

                int count = 0;
                try
                {
                    files = Directory.GetFiles(appPath + folder + "\\Logs", "*.log");
                }
                catch (IOException ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "      No old log files: " + ex.Message, false);
                    continue;
                }
                foreach (string file in files)
                {
                    parts = file.Split('_');
                    if (parts != null & parts.Length > 1)
                    {
                        parts = parts[parts.Length - 1].Split('.');
                        if (parts != null && parts.Length > 1)
                        {
                            string str = CurrentDate.ToString("yyyyMMdd");
                            string[] format = {"yyyyMMdd"};
                            DateTime date;

                            if (DateTime.TryParseExact(parts[0], 
                                format, 
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, 
                                out date))
                            {
                                if (date < CurrentDate.AddDays(-MAX_LOG_AGE))
                                    try
                                    {
                                        File.Delete(file);
                                        count++;
                                    }
                                    catch (IOException )
                                    {
                                    }
                            }
                        }
                    }
                }
                Log.Log((int)LogClass.logType.Info, "   " + count.ToString() + " log files older than " + MAX_LOG_AGE.ToString() + " days deleted.", false);
            }
        }

        static void CleanTables()
        {

            bool CleanFlag = false;
            short CleanDays;
            DateTime lastRun;
            bool HistFlag = false;
            short HistoryYears;
            bool MaintFlag = false;
            short MaintYears;
            Log.Log((int)LogClass.logType.Info, "Started Clean Tables");
            if ((GlobalShared.ConnectionString = GetDatabaseParms(folders[0])) != null)
            {
                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace("*", ",1433");
                //System.Threading.Thread.Sleep(5000);
                GlobalShared.CN = new SqlConnection(GlobalShared.ConnectionString);

                Log.Log((int)LogClass.logType.Info, "*** Clean Dupe Registry Entries", false);

                clsDB.Query(cleanRegistry, GlobalShared.CN);

                // Check Total Fix
                //UpdateCheckTotals();

            }

            Log.Log((int)LogClass.logType.Info, "*** Started History Clean Run ***", false);
            foreach (string folder in folders)
            {
                Log.Log((int)LogClass.logType.Info, "", false);
                Log.Log((int)LogClass.logType.Info, "Working on <" + folder + ">", false);
                if ((GlobalShared.ConnectionString = GetDatabaseParms(folder)) != null)
                {
                    // Open client database
                    if (GlobalShared.CN.ConnectionString != null)
                    {
                        GlobalShared.CN.ConnectionString = GlobalShared.ConnectionString;
                        try
                        {
                            GlobalShared.CN.Open();
                            Log.Log((int)LogClass.logType.Info, "Successfully connected to Database on 1st attempt: " + GlobalShared.CN.ConnectionString, true);
                        }
                        catch (SqlException ex)
                        {
                            if (!GlobalShared.ConnectionString.Contains(",1433"))
                            {
                                GlobalShared.ConnectionString = GlobalShared.ConnectionString.Replace(";Initial", ",1433;Initial");
                                GlobalShared.CN.ConnectionString = GlobalShared.ConnectionString;
                            }  
                            try
                            {
                                GlobalShared.CN.Open();
                                Log.Log((int)LogClass.logType.Info, "Successfully connected to Database on 2nd attempt: " + GlobalShared.CN.ConnectionString, true);
                            }
                            catch (SqlException qex)
                            {
                                Log.Log((int)LogClass.logType.ErrorCondition,"Error connecting to database: " + qex.Message, false);
                                if (qex.InnerException != null)
                                {
                                    Log.Log((int)LogClass.logType.ErrorCondition,"Inner Exception: " + qex.InnerException.Message,false);
                                    return;
                                }

                            }
                        }
                        catch (System.Exception ex)
                        {
                            Log.Log((int)LogClass.logType.ErrorCondition, "Unable to connect to database: " + ex.Message, false);
                            continue;
                        }
                        finally
                        {
                            try
                            {
                                GlobalShared.CN.Close();
                            }
                            catch { }
                        }

                        // Retrieve History Clean parameters
                        string msg = "";
                        Control[] control = Control.load(out msg);
                        if (control == null || control.Length == 0)
                        {
                            Log.Log((int)LogClass.logType.ErrorCondition, "Could not retrieve control parameters using: " + msg, false);
                            continue;
                        }
                        CleanFlag = control[0].CleanFlag;
                        HistFlag = control[0].HistoryFlag;
                        MaintFlag = control[0].MaintFlag;
                        CleanDays = control[0].CleanDays;
                        HistoryYears = control[0].HistoryYears;
                        MaintYears = control[0].MaintYears;
                        lastRun = control[0].CleanDate;

                        if (!CleanFlag || (!HistFlag && !MaintFlag))
                            continue;
                        lastRun = control[0].CleanDate;
                        CleanDays = control[0].CleanDays;
                        if (lastRun.AddDays(CleanDays) >= CurrentDate)
                            continue;
                        HistoryYears = control[0].HistoryYears;
                        if (HistFlag)
                            CleanHistory(HistoryYears); 
                        MaintYears = control[0].MaintYears;
                        if (MaintFlag)
                            CleanMaint(MaintYears);
                    }
                }
                else
                    Log.Log((int)LogClass.logType.ErrorCondition, "Database parms returned null.", false);
            }

            // Check Total Fix
        }

        static void CleanHistory(short HistoryYears)
        {
            DateTime testDate = CurrentDate.AddYears(-HistoryYears);
            long count = 0;

            count = HistChg.DeleteByDate(testDate);

            Log.Log((int)LogClass.logType.Info, "History Charge Records Deleted = " + count.ToString(), false);

            count = 0;
                count = HistPay.DeleteByDate(testDate);
            Log.Log((int)LogClass.logType.Info, "History Payment Records Deleted = " + count.ToString(), false);
        }

        static void CleanMaint(short MaintYears)
        {
            DateTime testDate = CurrentDate.AddYears(-MaintYears);
            long count = 0;
                count = Maintenance.DeleteByDate(testDate);
            Log.Log((int)LogClass.logType.Info, "Maintenance Records Deleted = " + count.ToString(), false);

            count = 0;
            count = Improvements.DeleteByDate(testDate);
            Log.Log((int)LogClass.logType.Info, "Improvement Records Deleted = " + count.ToString(), false);
        }

        static void UpdateCheckTotals()
        {
            DateTime StartDate = new DateTime(DateTime.Now.Year - 1, 1, 1, 0, 0, 0);
            DateTime EndDate = new DateTime(DateTime.Now.Year + 1, 12, 31, 23, 59, 59);
            ApChecks[] checks = ApChecks.LoadForCheckTotalFix(StartDate, EndDate);
            if (checks != null && checks.Length > 0)
            {
                ApChecks oneCheck = null;
                foreach(ApChecks check in checks)
                {
                    oneCheck = ApChecks.LoadCheckTotal(check.Company, check.Department, check.CheckNumber);
                    if (oneCheck != null)
                    {
                        check.Amount = oneCheck.CheckTotal;
                        check.save();
                    }
                }
            }
        }

        static string GetDatabaseParms(string folder)
        {
            string DatabaseSource = "";
            string InitialCatalog = "";
            string MachineName = "";
            parms = new MbcsUtils.Parameters(appPath + folder + "\\" + MBCSCENTRAL_CONFIG);
            parms.getConfigs();
            object obj = null;
            parms.getItem("baseServer", ref obj);
            DatabaseSource = (string)obj; //  + ",1433";
            parms.getItem("initialCatalog", ref obj);
            InitialCatalog = (string)obj;
            parms.getItem("MachineName", ref obj);
            MachineName = (string)(obj == null || ((string)obj).Contains('*') ? "" : (string)obj + "\\");
            if (DatabaseSource == null || InitialCatalog == null || DatabaseSource.Trim() == "" || InitialCatalog.Trim() == "")
            {
                return null;
            }
            //if (MachineName != null && !MachineName.Trim().Equals(""))
               // DatabaseSource = MachineName + DatabaseSource;
            return SqlBaseConnectionString.Replace("<source>", DatabaseSource).
                                                    Replace("<catalog>", InitialCatalog).
                                                    Replace("<user>", DB_USER_NAME).
                                                    Replace("<password>", DB_PASSWORD);
        }

        static void GetFolderParms()
        {
            if (File.Exists(appPath + CONFIG_FILE))
            {
                try
                {
                    parms = new MbcsUtils.Parameters(appPath + CONFIG_FILE);
                    parms.getConfigs();
                    folders = parms.GetStrings(PARM_KEY);
                }
                catch (Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "Getting Parms: " + ex.Message);
                }
            }
            if (folders == null || folders.Length == 0)
            {
                folders = new String[1];
                folders[0] = LOCAL_USER_DIRECTORY;
            }
        }
    }
}
