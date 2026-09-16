// ***********************************************************************
// Assembly         : MbcsCentral
// Author           : mike
// Created          : 11-14-2023
//
// Last Modified By : mike
// Last Modified On : 12-22-2023
// ***********************************************************************
// <copyright file="Startup.xaml.cs" company="Michael Bartmon Computer Services LLC">
//     MBCS 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml;

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
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        /// <summary>
        /// The parms
        /// </summary>
        private static MbcsUtils.Parameters parms = Main.parms;
        /// <summary>
        /// The parm file
        /// </summary>
        private static string parmFile = Main.parmFile;
        /// <summary>
        /// The m session
        /// </summary>
        private static MbcsUtils.Parameters mSession = Main.mSession;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(ex.Message);
                MessageBoxCustom.Show(ex.InnerException.Message);
            }
            logProgress("Please wait while the program starts . . .");
            logProgress("");
        }

        /// <summary>
        ///  Queues startUp background process
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logProgress("Start up process initializing");
            logProgress("");
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Version version = assembly.GetName().Version;

            txtHeading.Text = "MbcsCentral - " + version.ToString();
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(startUp));
            }
            catch (ArgumentNullException ex)
            {
                logProgress("Null Argument: " + ex.Message);
                return;
            }
            catch (NotSupportedException ex)
            {
                logProgress("Not Supported: " + ex.Message);
                return;
            }
            catch (ThreadStartException ex)
            {
                logProgress("Start Error: " + ex.Message);
                return;
            }
            catch (ThreadInterruptedException ex)
            {
                logProgress("Interupted Error: " + ex.Message);
                return;
            }
            catch (MissingMethodException ex)
            {
                logProgress("Missing Method: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                logProgress("General Error: " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// Main Start Up method.
        /// <para>Writes progress messages to window.</para>
        /// </summary>
        /// <param name="obj">The object.</param>
        private void startUp(object obj)
        {
            string[] u = new string[4];
            string userName;
            try
            {
                logProgress("Start up process begun");
                logProgress("");
#if DEBUG
                GlobalShared.userName = "mike";
#endif
                logProgress("Retrieving InitialUser");
                logProgress("");
            }
            catch (System.MissingMethodException ex)
            {
                logProgress("Missing Method: " + ex.Message);
                return;

            }
            /// <summary>
            /// Retrieve logged on Windows user
            /// </summary>
            #region RetrieveUser
            try
            {
                u[0] = "Initial user: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ", " + Environment.UserName;
                logProgress("Initial user: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ", " + Environment.UserName);
            }
            catch (Exception ex)
            {
                logProgress("Could not retrieve initial user: " + ex.Message);
            }
            UserPrincipal principal;
            string description = "";
            try
            {
                logProgress("Retrieving PrincipalUser");
                principal = UserPrincipal.Current;
                logProgress("PrincipalUser found: " + principal.Name);
                description = principal.Description;
                if (description != null)
                {
                    logProgress("Found Principal user: " + principal.Name + " <" + description + ">");
                    u[1] = "Found Principal user: " + principal.Name + " <" + description + ">";
                }
                else
                {
                    logProgress("Found Principal user: " + principal.Name + " <NULL>");
                    u[1] = "Found Principal user: " + principal.Name + " <NULL>";
                }
                GlobalShared.userName = Environment.UserName;
            }
            catch (SystemException ex1)
            {
               logProgress("Unable to retrieve Principal account from AD: " + ex1.Message);
            }

            if (description != null && !description.Trim().Equals(""))
                GlobalShared.clientName = description;
            logProgress("Reading startup parameters . . .");
            if (GlobalShared.clientName.Trim() == "")
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            else
            {
                userName = GlobalShared.clientName;
                u[2] =  "User <" + userName + "> passed to application";
                logProgress("User <" + userName + "> passed to application");
                try
                {
                    Impersonator imp = new Impersonator(userName, GlobalShared.password);
                }
                catch (ApplicationException ex)
                {
                    logProgress("Unable to impersonate user: " + userName);
                }
            }
            if (userName.Contains("\\"))
                userName = userName.Substring(userName.IndexOf("\\") + 1);
            u[3] = "Running as user: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ", " + Environment.UserName;
            logProgress("Running as user: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ", " + Environment.UserName);
            #endregion 

            // Get configuration file and instantiate application log
            GlobalShared.appPath += GlobalShared.clientFolderName + "\\"; // (string)regKey.GetValue(GlobalShared.RdpFolder) + "\\";
            logProgress("Retrieved Rdp Folder value: " + GlobalShared.baseAppPath); //(string)regKey.GetValue(GlobalShared.RdpFolder));
            MbcsCentral.GlobalShared.Log = MbcsUtils.LogClass.instance;

            GlobalShared.Log.EventSource = GlobalShared.EventLogSource;
            GlobalShared.Log.EventLog = GlobalShared.EventLogName;
            GlobalShared.Log.Client = GlobalShared.clientName;
            logProgress("Instantiated Log: " + GlobalShared.appPath + GlobalShared.logFilePath + MbcsCentral.GlobalShared.logFileName);
            MbcsCentral.GlobalShared.Log.logFileName = GlobalShared.appPath + GlobalShared.logFilePath + MbcsCentral.GlobalShared.logFileName;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Version version = assembly.GetName().Version;

            GlobalShared.Log.Log((int)LogClass.logType.Config, "**********************", false);
            GlobalShared.Log.Log((int)LogClass.logType.Config, "* Application Startup * Version: " + version.ToString(), false);
            GlobalShared.Log.Log((int)LogClass.logType.Config, "**********************", false);
            if (u != null && u.Length > 0)
                foreach (string st in u)
                    if (st !=null && st.Length > 0)
                        GlobalShared.Log.Log((int)LogClass.logType.Config, st, false);
            GlobalShared.FQN = Main.GetFQDN();
            logProgress("FQDN: " + GlobalShared.FQN);
            GlobalShared.Log.Log((int)LogClass.logType.Config, "FQDN: " + GlobalShared.FQN, false);
            GlobalShared.Log.Log((int)LogClass.logType.Config,"Using Rdp Folder value: " + GlobalShared.appPath, false); //(string)regKey.GetValue(GlobalShared.RdpFolder));
            GlobalShared.Log.Log((int)LogClass.logType.Config, "Application Path = " + GlobalShared.baseAppPath, false);

            // Retrieve application configurations
            GlobalShared.Log.Log((int)LogClass.logType.Config, "About to open ParmFile: " + GlobalShared.appPath + parmFile, false);
            logProgress("About to get configs: " + GlobalShared.appPath + parmFile);
            Main.parms = new MbcsUtils.Parameters(GlobalShared.appPath + parmFile);
            try
            {
                Main.parms.getConfigs();
            } 
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.Config, "Error getting configs. There may be a duplicate name. Cannot continue: " + ex.Message,false);
                return;
            }
            logProgress("Got configs");

            // Retrieve Database parameters
            Main.parms.getItem("databaseSource", ref obj);
            GlobalShared.DatabaseSource = ((string) obj).Replace("**", ",1434").Replace("*", ",1433");
            GlobalShared.DatabaseName = GlobalShared.DatabaseSource.Replace(",1433", "").Replace(",1434", "");
            Main.parms.getItem("initialCatalog", ref obj);
            GlobalShared.InitialCatalog = (string)obj;
            Main.parms.getItem("MachineName", ref obj);
            //GlobalShared.MachineName = (string)(obj == null || ((string)obj).Contains('*') ? "" : (string)obj);
            GlobalShared.MachineName = (string)(obj == null  ? "" : (string)obj);
            if (GlobalShared.DatabaseSource == null || GlobalShared.InitialCatalog == null || GlobalShared.DatabaseSource.Trim() == "" || GlobalShared.InitialCatalog.Trim() == "")
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Database parameters not retrieved.  Cannot continue.", false);
                MessageBox.Show("Database parameters not retrieved.  Cannot continue.");
                GlobalShared.CN = null;
                this.Dispatcher.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            try
            {
                // If this is the first time running the application, machine name must be retrieved and pre-pended to the DatabaseSource
                if (GlobalShared.MachineName.Trim() != "")
                {
                    string dbs = GlobalShared.DatabaseSource.Replace("**", ",1434").Replace("*", ",1433");
                    if (GlobalShared.DatabaseSource.Contains("\\"))
                        dbs = GlobalShared.DatabaseSource.Substring(GlobalShared.DatabaseSource.IndexOf("\\") + 1);
                    GlobalShared.DatabaseSource = (GlobalShared.MachineName.ToLower().Contains("*azure*") ? "" : (GlobalShared.MachineName + "\\")) + dbs;
                    //Main.parms.setItem("databaseSource", GlobalShared.DatabaseSource);
                }

                // Create database connection string
                GlobalShared.SqlConnectionString = GlobalShared.SqlBaseConnectionString.Replace("<source>", GlobalShared.DatabaseSource).
                                                                                        Replace("<catalog>", GlobalShared.InitialCatalog).
                                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                                        Replace("<password>", GlobalShared.DB_PASSWORD);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error building ConnectionString: " + ex.Message, false);
                GlobalShared.Log.Log((int)LogClass.logType.Info, "   ConnectionString Parameters:", false);
                GlobalShared.Log.Log((int)LogClass.logType.Info, "      DataSource=" + GlobalShared.DatabaseSource, false);
                GlobalShared.Log.Log((int)LogClass.logType.Info, "      Catalog=   " + GlobalShared.InitialCatalog, false);
                GlobalShared.Log.Log((int)LogClass.logType.Info, "      User=      " + GlobalShared.DB_USER_NAME + "@" + GlobalShared.DatabaseSource.Substring(0, GlobalShared.DatabaseSource.IndexOf(".")), false);
                logProgress("Could not build ConnectionString for: " + GlobalShared.DatabaseSource + ", Catalog " + GlobalShared.InitialCatalog);
                Thread.Sleep(5000);
                this.Dispatcher.BeginInvoke(new Action(() => Application.Current.Shutdown()));
                return;
            }

            // Retrieve menu font values
            Main.parms.getItem("MenuFontFamily", ref obj);
            string ff = (string)obj;
            if (ff != null)
            {
                MbcsCentral.GlobalShared.menuFontFamliy = ff;
            }
            obj = null;
            Main.parms.getItem("MenuFontSize", ref obj);
            string fs = (string)obj;
            if (fs != null)
            {
                float val;
                float.TryParse(fs, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out val);
                MbcsCentral.GlobalShared.menuFontSize = val;
            }

            // Set session parms
            mSession.setParam("GlobalShared.appPath", GlobalShared.appPath);
            mSession.setParam("UserName", Environment.UserName);
            mSession.setParam("connectionString", MbcsCentral.GlobalShared.SqlConnectionString);

            GlobalShared.Log.Log((int)LogClass.logType.Config, "About to load GlobalProps from :" + mSession.getParam("GlobalShared.appPath") + "GlobalProps.xml", false);
            XmlDocument initProps = new XmlDocument();
            initProps.Load(mSession.getParam("GlobalShared.appPath") + "GlobalProps.xml");
            XmlNodeReader xmlRead = new XmlNodeReader(initProps.SelectSingleNode("global/connectionString"));
            xmlRead.Read();
            xmlRead.Read();
            initProps = null;
            xmlRead.Close();
            GlobalShared.Log.Log((int)LogClass.logType.Config, "Database connectionstring = {" + MbcsCentral.GlobalShared.SqlConnectionString + "}", false);
            bool connected = false;
            short tries = 0;
            short maxtries = 3;
            int waitInterval = 5000;

            // Connect to database
            logProgress("Connecting to database {" + GlobalShared.MachineName + "\\" + GlobalShared.DatabaseSource + "}");
            while (!connected)
            {
                try
                {
                    MbcsCentral.GlobalShared.CN = new SqlConnection(MbcsCentral.GlobalShared.SqlConnectionString);
                    MbcsCentral.GlobalShared.CN.Open();
                    mSession.setParam("CN", MbcsCentral.GlobalShared.CN);
                    GlobalShared.Log.Log((int)LogClass.logType.Config, "Successfully connected to Database", false);
                    connected = true;
                    logProgress("Successfully connected to database.");
                }
                catch (SqlException ex)
                {
                    if (tries < maxtries)
                    {
                        logProgress("Could not connect to database. Trying again. Attempt " + (tries + 1).ToString() + " {" + ex.Message + "]");
                        Thread.Sleep(waitInterval);
                        tries++;
                    }
                    else
                    {
                        if (System.Windows.MessageBox.Show("Unable to connect to DataBase.  Try again?", "DataBase Connection Problem",
                                            MessageBoxButton.YesNo) == MessageBoxResult.No)
                        {
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to connect to database: " + ex.Message, false);
                            GlobalShared.CN = null;
                            this.Dispatcher.BeginInvoke(new Action(() => this.Close()));
                            return;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to connect to database: " + ex.Message,
                                            false, Microsoft.VisualBasic.MsgBoxStyle.OkOnly);
                    GlobalShared.CN = null;
                    this.Dispatcher.BeginInvoke(new Action(() => this.Close()));
                    return;
                }
                finally
                {
                    try
                    {
                        MbcsCentral.GlobalShared.CN.Close();
                        GlobalShared.CN.ConnectionString = GlobalShared.SqlConnectionString;
                    }
                    catch { }
                }
            }

            // Retrieve SQL SERVER backup location
            string sqlCmd = "DECLARE @BackupDirectory NVARCHAR(100) " +
                            "EXEC master..xp_instance_regread @rootkey = 'HKEY_LOCAL_MACHINE'," +
                            "@key = 'Software\\Microsoft\\MSSQLServer\\MSSQLServer'," +
                            "@value_name = 'BackupDirectory', @BackupDirectory = @BackupDirectory OUTPUT; " +
                            "SELECT @BackupDirectory";

            SqlCommand cmd = new SqlCommand(sqlCmd, GlobalShared.CN);
            try
            {
                if (GlobalShared.CN.State == System.Data.ConnectionState.Closed)
                    GlobalShared.CN.Open();
                var row = cmd.ExecuteScalar();
                GlobalShared.DbBackupLocation = (string)row;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to determine DB Backup Location: " + ex.Message,false);
            }
            finally
            {
                try { GlobalShared.CN.Close(); } catch { }
            }

            GlobalShared.Log.Log((int)LogClass.logType.Config, "Attempting login with dev-user", false);
            MbcsCentral.GlobalShared.user = null;
            logProgress("Login...");

            logProgress("DONE.");
            //Thread.Sleep(500);
            this.Dispatcher.BeginInvoke(new Action(() => this.Close()));
        }

        /// <summary>
        /// Class Impersonator.
        /// Implements the <see cref="IDisposable" />
        /// </summary>
        /// <seealso cref="IDisposable" />
        public class Impersonator : IDisposable
         {
            /// <summary>
            /// The impersonated user
            /// </summary>
            private WindowsImpersonationContext _impersonatedUser = null;
            /// <summary>
            /// The user handle
            /// </summary>
            private IntPtr _userHandle;
            /// <summary>
            /// Initializes a new instance of the <see cref="Impersonator"/> class.
            /// </summary>
            /// <param name="user">The user.</param>
            /// <param name="password">The password.</param>
            /// <exception cref="System.ApplicationException">Could not impersonate user</exception>
            public Impersonator(string user, string password)
             {
                 _userHandle = new IntPtr(0);
                 //string user = "fire";
                 string userDomain = ConfigurationManager.AppSettings["MachineDomain"];
                 //string password = "Vietnam#65";
                 bool returnValue = LogonUser(user, userDomain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref _userHandle);
                 if (!returnValue)
                     throw new ApplicationException("Could not impersonate user");
                 WindowsIdentity newId = new WindowsIdentity(_userHandle);
                 _impersonatedUser = newId.Impersonate();
             }
            #region IDisposable Members
            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
             {
                 if (_impersonatedUser != null)
                 {
                     _impersonatedUser.Undo();
                     CloseHandle(_userHandle);
                     _impersonatedUser.Dispose();
                 }
             }
            #endregion
            #region Interop imports/constants
            /// <summary>
            /// The logo N32 logon interactive
            /// </summary>
            public const int LOGON32_LOGON_INTERACTIVE = 2;
            /// <summary>
            /// The logo N32 logon service
            /// </summary>
            public const int LOGON32_LOGON_SERVICE = 3;
            /// <summary>
            /// The logo N32 provider default
            /// </summary>
            public const int LOGON32_PROVIDER_DEFAULT = 0;
            /// <summary>
            /// Logons the user.
            /// </summary>
            /// <param name="lpszUserName">Name of the LPSZ user.</param>
            /// <param name="lpszDomain">The LPSZ domain.</param>
            /// <param name="lpszPassword">The LPSZ password.</param>
            /// <param name="dwLogonType">Type of the dw logon.</param>
            /// <param name="dwLogonProvider">The dw logon provider.</param>
            /// <param name="phToken">The ph token.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
             public static extern bool LogonUser(String lpszUserName, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
            /// <summary>
            /// Closes the handle.
            /// </summary>
            /// <param name="handle">The handle.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
             public extern static bool CloseHandle(IntPtr handle);
             #endregion
         }

        /// <summary>
        /// Delegate logProgressDelegate
        /// </summary>
        /// <param name="s">The s.</param>
        private delegate void logProgressDelegate(string s);

        /// <summary>
        /// Logs the progress.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.Object.</returns>
        private object logProgress(string s)
         {
             if (Messages.CheckAccess())
             {
                 Messages.Items.Add(s);
                 Messages.SelectedIndex = Messages.Items.Count - 1;
                 Messages.ScrollIntoView(Messages.Items[Messages.Items.Count -1]);
                GlobalShared.CentralLog.Log((int)LogClass.logType.Info, s, false);
            }
            else
             {
                 try
                 {
                     Messages.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => logProgress(s)));
                    GlobalShared.CentralLog.Log((int)LogClass.logType.Info, s, false);
                }
                catch { }
             }
             return null;
         }

    }
}

