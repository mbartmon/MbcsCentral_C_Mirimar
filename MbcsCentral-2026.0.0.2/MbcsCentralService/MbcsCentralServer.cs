using System.Threading;
using System.ServiceProcess;
using System.IO;
using System;
using System.Data.SqlClient;
using MbcsUtils;

public class MbcsCentralServer : ServiceBase
{
    private static LogClass Log;
    private static string appPath;
    private static MbcsUtils.Parameters parms;
    private const string CONFIG_FILE = "CloudApp.config";
    private const string LOCAL_USER_DIRECTORY = "UserConfig";
    private const string PARM_KEY = "client_";
    private const string BASE_LOG_NAME = "MbcsCentral";
    private const string MBCSCENTRAL_CONFIG = "MbcsCentral.config";
    private const string DB_USER_NAME = "mbartmon";
    private const string DB_PASSWORD = "Vietnam65";
    static string SqlBaseConnectionString = "Data Source=<source>;Initial Catalog=<catalog>;User Id=<user>;Password=<password>;Encrypt=True;Trusted_Connection=False;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;";
    private static DateTime CurrentDate = DateTime.Now;
    private static DateTime CleanupTime = new DateTime(2000, 1, 1, 23, 44, 0);
    bool DoDB;
    DbControl DBcontrol = null;
    
    private enum BackupTypes
    {
        Full = 0,
        Differential = 1
    };
    static string databaseName;

    private static SqlConnection CN = null;

    private const string DB_BACKUP_PROGRAM = "MbcsDbAutoBackup.exe";
    private const string FILE_LOG_CLEANUP_PROGRAM = "MbcsCentralLogCleaner.exe";
    private const long MAIN_TIMER_DELAY = 10 * 1000; // startup delay 10 seconds
    private const long MAIN_TIMER_INTERVAL = 15 * 60 * 1000; // 15 minutes
    System.Threading.Timer mainTimer;

    public MbcsCentralServer()
    {
        this.ServiceName = "MbcsCentralService";
        this.CanStop = true;
        this.CanPauseAndContinue = false;
        this.AutoLog = true;
    }

    public static void Main()
    {
        ServiceBase.Run(new MbcsCentralServer());
    }

    protected override void OnStart(string[] args)
    {

        appPath = AppDomain.CurrentDomain.BaseDirectory;
        if (!appPath.EndsWith("\\"))
            appPath += "\\";
        //Thread.Sleep(30000);
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        Log = new LogClass();
        Log.Username = "MbcsCentralService";
        Log.logFileName = appPath + "UserConfig\\Logs\\MbcsCentralService.log";
        Log.Log((int)LogClass.logType.Config, "*** Starting MbcsCentralService ***", true);

        // Ready to proceed
        TimerCallback tcb = new TimerCallback(MainTimer);
        Object stateInfo = null;
        try
        {
            mainTimer = new System.Threading.Timer(tcb, stateInfo, MAIN_TIMER_DELAY, MAIN_TIMER_INTERVAL);
        }
        catch (System.IO.FileNotFoundException IOex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, IOex.Message);
        }
        catch (System.Exception Sex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, Sex.Message);
        }
        Log.Log((int)LogClass.logType.Config, "Ready to monitor system.");
    }

    protected override void OnStop()
    {
        mainTimer.Change(Timeout.Infinite, Timeout.Infinite);
        mainTimer.Dispose();
        Log.Log((int)LogClass.logType.Info, "*** Stopping Service ***");
    }

    private void MainTimer(object stateInfo)
    {
        System.Diagnostics.Process exProc;

        Log.Log((int)LogClass.logType.Info, "<Looking for Tasks>");

        if (ReadAllControlInfo())
        {
            exProc = new System.Diagnostics.Process();
            if (DoDB && TimeToRun(DateTime.Now, DBcontrol.DbBackupTime, DBcontrol.DbBackupLastDate))
            {
                exProc.StartInfo.FileName = appPath + DB_BACKUP_PROGRAM;
                exProc.EnableRaisingEvents = true;
                try
                {
                    exProc.Start();
                    Log.Log((int)LogClass.logType.Info, "      " + DB_BACKUP_PROGRAM + " started.");
                }
                catch (System.Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "      Unable to start " + DB_BACKUP_PROGRAM + ": " + ex.Message);
                }
            }
            else
            {
                Log.Log((int)LogClass.logType.Info, "       " + DB_BACKUP_PROGRAM + ": Not time to start yet (" + DBcontrol.DbBackupTime.ToShortTimeString() + ")");
            }

            if (TimeToRun(DateTime.Now, CleanupTime))
            {
                exProc = new System.Diagnostics.Process();
                exProc.StartInfo.FileName = appPath + FILE_LOG_CLEANUP_PROGRAM;
                exProc.EnableRaisingEvents = true;
                try
                {
                    exProc.Start();
                    Log.Log((int)LogClass.logType.Info, "      " + FILE_LOG_CLEANUP_PROGRAM + " started.");
                }
                catch (System.Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "     Unable to start " + FILE_LOG_CLEANUP_PROGRAM + ": " + ex.Message);
                }
            }
            else
            {
                Log.Log((int)LogClass.logType.Info, "       " + FILE_LOG_CLEANUP_PROGRAM + ": Not time to start yet (" + CleanupTime.ToShortTimeString() + ")");
            }
        }
        else
            Log.Log((int)LogClass.logType.ErrorCondition, "Could not access database.");
    }

    private bool TimeToRun(DateTime CurrentDateTime, DateTime RunTime, DateTime? LastRun = null )
    {
        if (LastRun == null)
        {
            if (CurrentDateTime.TimeOfDay >= RunTime.TimeOfDay)
            return true;
        }
        else
        {
            DateTime last = (DateTime)LastRun;
            if ( CurrentDateTime.Date >= last.Date.AddDays(1) && CurrentDateTime.TimeOfDay >= RunTime.TimeOfDay)
            {
                return true;
            }
        }

        return false;
    }

    private bool ReadAllControlInfo()
    {
        // Ensure we can connect to the Database
        try
        {
            CN = new SqlConnection(GetDatabaseParms());
            GlobalShared.SqlConnectionString = CN.ConnectionString;
        }
        catch (SqlException ex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, "  Could not create SQLConnection: " + ex.Message, true);
            Log.Log((int)LogClass.logType.ErrorCondition, "  " + CN.ConnectionString, true);
            return false;
        }
        catch (Exception ex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, "Create SQLConnection: " + ex.Message, true);
            return false;
        }
        try
        {
            CN.Open();
            Log.Log((int)LogClass.logType.Config, "  Successfully conected to database.", false);
            Log.Log((int)LogClass.logType.Config, "  " + CN.ConnectionString, true);
            CN.Close();
        }
        catch (SqlException ex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, "  Could not open SQLConnection: " + ex.Message, true);
            Log.Log((int)LogClass.logType.ErrorCondition, "  " + CN.ConnectionString, true);
            return false;
        }
        catch (Exception ex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, "Open SQLConnection: " + ex.Message, true);
            return false;
        }

        // Get DBcontrol info for backup
        try
        {
            DoDB = true;
            DBcontrol = DbControl.load();
            if (DBcontrol == null)
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "  Could not retireve DB Control info", true);
                DoDB = false;
            }
            if (DoDB && DoDB && DBcontrol.DbBackupLocation.Trim().Equals(""))
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "  Backup location not set.", true);
                DoDB = false;
            }

            // Get BaseCatalog
            object obj = null;
            parms.getItem("baseCatalog", ref obj);
            databaseName = (string)obj;
            if (obj == null)
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "  The baseCatalog could not be found.", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            Log.Log((int)LogClass.logType.ErrorCondition, "Get DBcontrol info: " + ex.Message, true);
            return false;
        }

        return DoDB;
    }

    static void GetParms()
    {
        if (File.Exists(appPath + CONFIG_FILE))
        {
            try
            {
                parms = new MbcsUtils.Parameters(appPath + CONFIG_FILE);
                parms.getConfigs();
            }
            catch (Exception ex)
            {
                Log.Log((int)LogClass.logType.ErrorCondition, "  Getting Parms: " + ex.Message, true);
            }
        }
    }

    static string GetDatabaseParms()
    {
        string DatabaseSource = "";
        string InitialCatalog = "";
        string MachineName = "";
        parms = null;
        parms = new MbcsUtils.Parameters(appPath + LOCAL_USER_DIRECTORY + "\\" + MBCSCENTRAL_CONFIG);
        parms.getConfigs();
        object obj = null;
        parms.getItem("databaseSource", ref obj);
        DatabaseSource = ((string)obj).Replace("*", ",1433");
        parms.getItem("initialCatalog", ref obj);
        InitialCatalog = (string)obj;
        parms.getItem("MachineName", ref obj);
        MachineName = (string)(obj == null || ((string)obj).Contains("*") ? "" : (string)obj + "\\");
        if (DatabaseSource == null || InitialCatalog == null || DatabaseSource.Trim() == "" || InitialCatalog.Trim() == "")
        {
            return null;
        }
        if (MachineName != null && !MachineName.Trim().Equals(""))
            DatabaseSource = MachineName + DatabaseSource;
        return SqlBaseConnectionString.Replace("<source>", DatabaseSource).
                                                Replace("<catalog>", InitialCatalog).
                                                Replace("<user>", DB_USER_NAME).
                                                Replace("<password>", DB_PASSWORD);
    }
}