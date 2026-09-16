// ***********************************************************************
// Assembly         : MbcsCentral
// Author           : mike
// Created          : 11-14-2023
//
// Last Modified By : mike
// Last Modified On : 12-20-2023
// ***********************************************************************
// <copyright file="MainWindow.xaml.cs" company="Michael Bartmon Computer Services LLC">
//     MBCS 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using CSharp;
using Vintasoft.Twain;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        /// <summary>
        /// The type table
        /// </summary>
        private static Dictionary<string, System.Type> typeTable = Main.typeTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Handles the Closing event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Logged Out", false);
            Main.saveForm(this);
            WinPM.WinpmGlobal.SaveLastTenantsList();
            Main.parms.setConfigs();
        }

        //Private Sub Exit_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles [Exit].Click
        //    saveForm(Me)
        //   Me.Close()
        //End Sub
        /// <summary>
        /// Called from GlobalShared.Main for application startup
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        /// <remarks>Starup=MenuBuilder Information
        /// <a href="../MbcsCentral Menu.html">MenuBuild</a></remarks>
        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MbcsCentral.GlobalShared.CentralLog = MbcsUtils.LogClass.instance;
            GlobalShared.CentralLog.EventSource = GlobalShared.EventLogSource;
            GlobalShared.CentralLog.EventLog = GlobalShared.EventLogName;
            GlobalShared.CentralLog.Client = "Startup";
            GlobalShared.CentralLog.logFileName = GlobalShared.baseAppPath + GlobalShared.logFilePath + MbcsCentral.GlobalShared.logFileName;
            GlobalShared.CentralLog.Log((int)LogClass.logType.Info, "MainWindow Loaded", false);
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowState = System.Windows.WindowState.Minimized;
            Startup start = new Startup();
            start.ShowDialog();
            bool valid = false;
            if (GlobalShared.CN == null)
            {
                try
                {
                    this.Close();
                    return;
                }
                catch { }
            }
            string value = (string)Main.GetRegistryEntry(GlobalShared.validationKeyLoc, "Setting", "", true);

#if DEBUG
            valid = true;
#else
            if (value == null || value.Trim() == "")
            {
                if (MessageBox.Show("This program is not registered.  Do you want to register it now?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    try { this.Close(); }
                    catch { }
                }
                frmVerify frm = new frmVerify();
                frm.ShowDialog();
                valid = frm.Valid;
                if (!valid)
                {
                    try { this.Close(); }
                    catch { }
                }
            }
            else
                valid = true;
#endif

            valid = true;

            if (valid)
            {
                if (!CallLogin())
                {

                    Main.GetAllRegistryEntries();
                    GlobalShared.mainWindow = this;
                    GlobalShared.Log.Log((int)LogClass.logType.Config, "Create MENU.", false);
                    Main.buildMenu(this);
                    GlobalShared.Log.Log((int)LogClass.logType.Config, "Set MainWindow session parm.", false);
                    try
                    {
                        Main.mSession.setParam("MainWindow", this);
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
                    }
                    Main.setMainWindowTitle();
                    GlobalShared.Log.Log((int)LogClass.logType.Config, "Retrieve MainWindow state, size, and location.", false);
                    this.WindowState = System.Windows.WindowState.Normal;
                    _busy.IsBusy = false;
                    WinPM.WinpmGlobal.GetLastTenantsList();
                    Main.FillLastTenantsInMenu();
                    WinPM.Control control = WinPM.Control.load(1);
                    if (control != null)
                    {
                        if (control.scan)
                        {
                            try
                            {
                                string WinDocPath = control.WindocDatabase;
                                if (WinDocPath == null || WinDocPath.Trim().Equals(""))
                                {
                                    WinDocPath = WinDocPath.Substring(0, WinDocPath.LastIndexOf("\\"));

                                    int xtimestimeout = 5;
                                    while (!System.IO.Directory.Exists(WinDocPath) && (xtimestimeout > 0))
                                    {
                                        System.Threading.Thread.Sleep(3000);
                                        xtimestimeout -= 1;
                                    }
                                    if (!System.IO.Directory.Exists(WinDocPath))
                                    {
                                        control.scan = false;
                                        control.save();
                                        MessageBox.Show("Unable to connect to WinDOC at <" + WinDocPath + ">");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Checking for Scan: " + ex.Message, false);
                            }
                        }
                    }


                }
            }
            Main.getForm(this);
        }

        /// <summary>
        /// Calls the login.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CallLogin()
        {
            bool Cancelled = false;
            if (GlobalShared.appPath.ToLower().Contains("workfiles"))
            {
                MbcsCentral.GlobalShared.userName = "mike";
                MbcsCentral.GlobalShared.password = "vietnam#65";
                try
                {
                    MbcsCentral.GlobalShared.user = Manager.setUser(MbcsCentral.GlobalShared.userName, MbcsCentral.GlobalShared.password);
                }
                catch (System.Exception)
                {
                }
                if (MbcsCentral.GlobalShared.user == null)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Warning, "Dev-user not found", false);
                }
            }
            GlobalShared.Log.Log((int)LogClass.logType.Info, "userName=" + GlobalShared.userName, false);
            if (!GlobalShared.userName.Trim().Equals(""))
            {
                // User is in AD, setup with no password
                try
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "Attempting Auto Login", false);
                    MbcsCentral.GlobalShared.user = Manager.setUser(MbcsCentral.GlobalShared.userName, "*");
                }
                catch (SystemException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "Auto Login Failed for us er [" + GlobalShared.userName + ": " + ex.Message, false);
                }
            }
            if (GlobalShared.user == null)
            {
                GlobalShared.Log.Log((int)LogClass.logType.Config, "Dev-user not found.  Displaying LogIn window", false);
                frmLogin frm = null;
                while (MbcsCentral.GlobalShared.user == null)
                {
                    frm = null;
                try
                {
                    frm = new frmLogin();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
                    MbcsCentral.GlobalShared.user = null;
                    try
                    {
                        frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        try
                        {
                            frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                            frm.BringIntoView();
                            frm.Focus();
                            frm.Topmost = true;
                            frm.txtUsername.Focus();
                            frm.ShowDialog();
                            if (frm.Cancelled)
                            {
                                Cancelled = frm.Cancelled;
                                Application.Current.Shutdown();
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to display login window: " + ex.Message, false);
                            this.Close();
                            System.Windows.Application.Current.Shutdown();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR 2: " + ex.Message);
                    }
                    if ((MbcsCentral.GlobalShared.userName == null || MbcsCentral.GlobalShared.userName.Trim() == "") && (MbcsCentral.GlobalShared.password == null || MbcsCentral.GlobalShared.password.Trim() == ""))
                    {
                        try { this.Close(); }
                        catch { }
                        return Cancelled;
                    }
                    try
                    {
                        MbcsCentral.GlobalShared.user = Manager.setUser(MbcsCentral.GlobalShared.userName, MbcsCentral.GlobalShared.password);
                    }
                    catch (System.Exception)
                    {
                    }
                    if (MbcsCentral.GlobalShared.user == null)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.Warning, "User <" + MbcsCentral.GlobalShared.userName + "> not found.", false, Microsoft.VisualBasic.MsgBoxStyle.OkOnly);
                        Accounts[] accts = Accounts.load();
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "Current Accounts:", false);
                        if (accts != null)
                        {
                            foreach (Accounts acct in accts)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.Info, "   " + acct.login, false);
                            }
                        }
                    }
                }
            }

            GlobalShared.Log.Username = GlobalShared.userName;
            GlobalShared.Log.Log((int)LogClass.logType.Config, "User [" + GlobalShared.userName + "] logged in.", false);

            // And this is for?
            try
            {
                MbcsCentral.GlobalShared.CN.Close();
            }
            catch (Exception)
            {

            }

            //Building.alterTable();

            // *****************************
            // * Kick off background tasks *
            // *****************************
            /* Deprecated: retrieveTenantNames
            BackgroundTasks.retrieveTenantNames sc = new  BackgroundTasks.retrieveTenantNames();
            System.Threading.TimerCallback TimerDelegate = sc.CheckStatus; // new System.Threading.TimerCallback();
            //Log.Log((int)LogClass.logType.Config, "Initializing TimerDelegate: retrieveTenantNames")
            //create timer that calls a procedure every runInterval, runs as soon as it is created.
            Globals.GlobalShared.tenantNamesTimer = new System.Threading.Timer(TimerDelegate, null, 0, Globals.GlobalShared.TENANT_NAMES_INTERVAL);
            //Log.Log((int)LogClass.logType.Config, "Initializing Threading timer attached to timer delegate")
            */
            // ************************************
            // * End of kick off background tasks *
            // ************************************

            //Main.getApps();

            /*
            GlobalShared.Log.Log((int)LogClass.logType.Config, "Getting internal windows...", false);
            System.Reflection.Assembly assm = System.Reflection.Assembly.GetExecutingAssembly();
            System.Type[] types = assm.GetTypes();
            typeTable = new Dictionary<string, System.Type>();
            foreach (System.Type t in types)
            {
                if (t.Name.StartsWith("frm"))
                {
                    // GlobalShared.Log.Log((int)LogClass.logType.Config, "  " + t.Name);
                    try
                    {
                        typeTable.Add(t.Name, t);
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }
            */
            return Cancelled;
        }

    }
}
