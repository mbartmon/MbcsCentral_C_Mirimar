using MbcsCentral;
using MbcsCentral.Schema;
using MbcsCentral.Data;
using MbcsCentral.WinPM;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Table = Microsoft.SqlServer.Management.Smo.Table;
using System.Linq;

sealed class Main
{

    public static MbcsUtils.Parameters mSession = new MbcsUtils.Parameters();
    //Public mSession As New Session
    public static MbcsUtils.Parameters parms;
    public static string parmFile = "MbcsCentral.config";
    public static string menuDocument = "MenuFile.xml"; // "MenuSchema.xml";
    public static System.Windows.Controls.Menu mainMenu;
    public static Hashtable menuTable = null;
    public static Dictionary<string, System.Type> typeTable;
    public static user_permissions[] perms;
    public static Dictionary<string, long> RegEntries;

    public static string RemoteServerName = "";
    public static string ConnectRemoteServer = "use master go " +
                                               "exec master.dbo.sp_addlinkedserver " +
                                               "@server = " + RemoteServerName + ", " +
                                               "@srvproduct = N'SQL SERVER'; " +
                                               "go";
    public static string UseLocalCreds = "EXEC master.dbo.sp_addlinkedsrvlogin " +
                                         "@rmtsrvname = " + RemoteServerName + ", " +
                                         "@locallogin = NULL, " +
                                         "@useself = N'True' ;" +
                                         "GO";

    public static System.Diagnostics.Process exProc;
    public static Hashtable curProcs = new Hashtable();
    public static Hashtable curProcsV = new Hashtable();

    private static System.Windows.Window BackgroundWindow = null;

    const UInt32 SWP_NOSIZE = 0x0001;
    const UInt32 SWP_NOMOVE = 0x0002;
    const UInt32 SWP_NOACTIVATE = 0x0010;
    static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public static string GetFQDN()
    {
        GlobalShared.domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
        string hostName = Dns.GetHostName();

        if (!hostName.EndsWith(GlobalShared.domain))  // if hostname does not already include domain name
        {
            hostName += "." + GlobalShared.domain;   // add the domain name part
        }

        return hostName;                    // return the fully qualified name
    }
    public static void buildMenu(MainWindow frm)
    {

        int lastLevel = 0;
        int currentLevel = 0;
        string menuName = "";
        string aPath = "";
        string aName = "";
        string dPath = "";
        string dName = "";
        System.Windows.Controls.MenuItem menuItm = null;
        if (mainMenu != null)
        {
            mainMenu = null;
            menuTable = null;
        }
        menuTable = new Hashtable();
        mainMenu = new System.Windows.Controls.Menu();
        mainMenu.Name = "MainMenu";
        mainMenu.FontFamily = new FontFamily(MbcsCentral.GlobalShared.menuFontFamliy);
        try
        {
            mainMenu.FontSize = MbcsCentral.GlobalShared.menuFontSize;
        }
        catch (SystemException ex)
        {
        }
        mainMenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        mainMenu.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        mainMenu.Height = mainMenu.FontSize + 6.0D;
        mainMenu.Background = new SolidColorBrush(Colors.LightSkyBlue);
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ConformanceLevel = ConformanceLevel.Fragment;
        settings.IgnoreWhitespace = true;
        settings.IgnoreComments = true;
        XmlReader xmlReader = XmlReader.Create(GlobalShared.baseAppPath + menuDocument, settings);
        XmlReader inner;
        while (xmlReader.Read())
        {
            if (xmlReader.Name.ToString().Equals("MenuItem") && xmlReader.NodeType.ToString().Equals("Element"))
            {
                //inner = xmlReader.ReadSubtree();
                //inner.Read();
                menuItm = processMenuItem(xmlReader);
                mainMenu.Items.Add(menuItm);
                Separator sep = new Separator();

                sep.Background = new SolidColorBrush(Colors.White);
                sep.BorderBrush = new SolidColorBrush(Colors.White);
                sep.BorderThickness = new System.Windows.Thickness(2);
                mainMenu.Items.Add(sep);
            }
            else
            {
                if (xmlReader.Name.ToString().Equals("MenuHeader") && xmlReader.NodeType.ToString().Equals("Element"))
                {
                    //inner = xmlReader.ReadSubtree();
                    //inner.Read();
                    menuItm = processMenuHeader(xmlReader, 0);
                    mainMenu.Items.Add(menuItm);
                    Separator sep = new Separator();
                    sep.Background = new SolidColorBrush(Colors.White);
                    sep.BorderBrush = new SolidColorBrush(Colors.White);
                    sep.BorderThickness = new System.Windows.Thickness(2);
                    mainMenu.Items.Add(sep);
                }
            }
        }

        ExternalApplications[] apps = ExternalApplications.load();
        if (apps != null && apps.Length > 0)
        {
            int i = 0;
            foreach (ExternalApplications app in apps)
            {
                if (!menuTable.ContainsKey(app.MenuName))
                {
                    try
                    {
                        if ( i > 0)
                        {
                            Separator sep = new Separator();
                            sep.Background = new SolidColorBrush(Colors.White);
                            sep.BorderBrush = new SolidColorBrush(Colors.White);
                            sep.BorderThickness = new System.Windows.Thickness(2);
                            mainMenu.Items.Add(sep);

                        }
                        menuItm = new MenuItem();
                        if (!app.ApplicationPath.EndsWith("\\"))
                            app.ApplicationPath += "\\";
                        menuTable.Add(app.MenuName, new MenuEntry((int)MenuEntry.MenuTypeEnum.Application, app.ApplicationPath, app.MenuName, "", "", "", app.ApplicationAppName, "", ""));
                        menuItm.AddHandler(System.Windows.Controls.MenuItem.ClickEvent, new RoutedEventHandler(menuItemClick));
                        menuItm.Header = app.MenuContent.Trim();
                        menuItm.Name = app.MenuName;
                        mainMenu.Items.Add(menuItm);
                        i++;
                    }
                    catch { }
                }
            }
        }
        // Insert fixed menu items that must be available if xml file is not
        if (frm != null)
        {

            try
            {
                frm.subGrid.Children.Remove(mainMenu);
            }
            catch (System.Exception)
            {
            }
            frm.subGrid.Children.Add(mainMenu);
        }
    }

    private static System.Windows.Controls.MenuItem processMenuItem(XmlReader xmlReader)
    {
        string menuName = "";
        string menuHeader = "";
        string menuApp = "";
        string aPath = "";
        string aName = "";
        string dPath = "";
        string dName = "";
        int menuType = -1;
        string parameter = "";
        string menuTarget = "";
        string menuClass = "";
        System.Windows.Controls.MenuItem menuItm = null;
        System.Windows.Controls.MenuItem returnItem = null;
        bool inHeader = false;
        bool lastWasEnd = false;
        XmlReader inner;

        while (!xmlReader.EOF)
        {
            if (xmlReader.Name.ToString().Equals("MenuHeader") && xmlReader.NodeType.ToString().Equals("Element"))
            {
                //inner = xmlReader.ReadSubtree();
                //inner.Read();
                menuItm = processMenuHeader(xmlReader, 0);
                mainMenu.Items.Add(menuItm);
            }

            if (xmlReader.Name.ToString().Equals("MenuItem") && xmlReader.NodeType.ToString().Equals("Element"))
            {
                aPath = "";
                aName = "";
                dPath = "";
                dName = "";
                menuName = "";
                menuHeader = "";
                menuType = -1;
                parameter = "";
                menuClass = "";
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (xmlReader.Name.Equals("Type"))
                        {
                            menuType = System.Convert.ToInt32(System.Array.IndexOf(MenuEntry.MenuTypes, xmlReader.Value));
                        }
                        if (xmlReader.Name.ToString().Equals("Name"))
                        {
                            //menuItm.Name = XmlReader.Value
                            menuName = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Content"))
                        {
                            //menuItm.Header = XmlReader.Value
                            menuHeader = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Target"))
                        {
                            menuTarget = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Application"))
                        {
                            menuApp = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Class"))
                        {
                            menuClass = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Parameter"))
                            parameter = (string)xmlReader.Value;
                    }
                }
                if (returnItem == null)
                {
                    returnItem = new System.Windows.Controls.MenuItem();
                }
                if (menuType == (int)MenuEntry.MenuTypeEnum.Separator)
                {
                    returnItem.Header = new System.Windows.Controls.Separator();
                    xmlReader.Read();
                    continue;
                }
                
                returnItem.Name = menuName;
                returnItem.Header = menuHeader;
            }
            if (xmlReader.Name.ToString().Equals("Form"))
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (xmlReader.Name.Equals("Name"))
                        {
                            aName = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Parameter"))
                            parameter = (string)xmlReader.Value;
                    }
                }
            }
            if (xmlReader.Name.ToString().Equals("AppPath"))
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (xmlReader.Name.Equals("Path"))
                        {
                            aPath = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Name"))
                        {
                            aName = (string)xmlReader.Value;
                        }
                    }
                }
            }
            if (xmlReader.Name.ToString().Equals("DataPath"))
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (xmlReader.Name.Equals("Path"))
                        {
                            dPath = (string)xmlReader.Value;
                        }
                        if (xmlReader.Name.Equals("Name"))
                        {
                            dName = (string)xmlReader.Value;
                        }
                    }
                }
            }
            if (xmlReader.Name.ToString().Equals("MenuItem") && xmlReader.NodeType.ToString().Equals("EndElement"))
            {
                if (!menuTable.ContainsKey(menuName))
                {
                    MenuEntry me = new MenuEntry(menuType, aPath, aName, dPath, dName, menuTarget, menuApp, parameter, menuClass);
                    menuTable.Add(menuName, new MenuEntry(menuType, aPath, aName, dPath, dName, menuTarget, menuApp, parameter, menuClass));
                    returnItem.AddHandler(System.Windows.Controls.MenuItem.ClickEvent, new RoutedEventHandler(menuItemClick));
                }
                break;
            }
            xmlReader.Read();

        }

        //xmlReader.Close();
        menuApp = "";
        return returnItem;
    }

    private static System.Windows.Controls.MenuItem processMenuHeader(XmlReader xmlReader, int level)
    {
        string menuName = "";
        string menuHeader = "";
        string menuApp = "";
        string aPath = "";
        string aName = "";
        string dPath = "";
        string dName = "";
        int menuType = -1;
        string menuTarget = "";
        System.Windows.Controls.MenuItem menuItm = null;
        System.Windows.Controls.MenuItem returnItem = null;
        bool inHeader = false;
        bool lastWasEnd = false;
        XmlReader inner;

        level++;
        while (!xmlReader.EOF)
        {
            if (xmlReader.Name.ToString().Equals("MenuItem") && xmlReader.NodeType.ToString().Equals("Element"))
            {
                if (menuItm == null)
                    menuItm = new System.Windows.Controls.MenuItem();
                //inner = xmlReader.ReadSubtree();
                //inner.Read();
                System.Windows.Controls.MenuItem menuItmInner = new System.Windows.Controls.MenuItem();
                menuItmInner = processMenuItem(xmlReader);
                menuItm.Items.Add(menuItmInner);
                xmlReader.Read();
                continue;
            }
            if (xmlReader.Name.ToString().Equals("MenuHeader") && xmlReader.NodeType.ToString().Equals("EndElement"))
            {
                break;
            }
            if (inHeader && xmlReader.Name.ToString().Equals("MenuHeader") && xmlReader.NodeType.ToString().Equals("Element"))
            {
                if (menuItm == null)
                    menuItm = new System.Windows.Controls.MenuItem();
                //inner = xmlReader.ReadSubtree();
                //inner.Read();
                System.Windows.Controls.MenuItem menuItmInner = new System.Windows.Controls.MenuItem();
                menuItmInner = processMenuHeader(xmlReader, level);
                menuItm.Items.Add(menuItmInner);
                xmlReader.Read();
                continue;
            }
            aPath = "";
            aName = "";
            dPath = "";
            dName = "";
            menuName = "";
            menuHeader = "";
            menuType = (int)MenuEntry.MenuTypeEnum.Header;
            inHeader = true;
            if (xmlReader.HasAttributes)
            {
                while (xmlReader.MoveToNextAttribute())
                {
                    if (xmlReader.Name.ToString().Equals("Name"))
                    {
                        //menuItm.Name = XmlReader.Value
                        menuName = (string)xmlReader.Value;
                    }
                    if (xmlReader.Name.Equals("Content"))
                    {
                        //menuItm.Header = XmlReader.Value
                        menuHeader = (string)xmlReader.Value;
                    }
                    if (xmlReader.Name.Equals("Application"))
                    {
                        menuApp = (string)xmlReader.Value;
                    }
                }
            }
            menuItm = new System.Windows.Controls.MenuItem();
            menuItm.Name = menuName;
            menuItm.Header = menuHeader;
            //if (returnItem == null)
            //    returnItem = new System.Windows.Controls.MenuItem();
            //returnItem.Items.Add( menuItm );
            xmlReader.Read();
        }

        return menuItm;
    }

    public static void menuItemClick(object sender, RoutedEventArgs e)
    {
        int subfix = 0;
        // menuTable.Add("1", "value");  UNKNOWN USE
        System.Windows.Controls.MenuItem itm = (System.Windows.Controls.MenuItem)sender;
        MenuEntry menuItm = (MenuEntry)menuTable[itm.Name];  // menuTable(itm.Name);
        string itmName = itm.Name.Trim();
        if (itmName.ToLower().StartsWith("gl"))
        {
            Clients client = Clients.load((long)GlobalShared.user.Account.clientId);
            if (client != null)
            {
                GlobalShared.WinGlEnabled = client.WinGlEnabled;
                client = null;
            }
            if (!GlobalShared.WinGlEnabled)
            {
                MessageBoxCustom.Show("You have not activated the WinGL module.");
                return;
            }
        }
        int p = itmName.IndexOf("0");
        if (p > 0)
            itmName = itmName.Substring(0, p);
        else if (Int32.TryParse(itmName.Substring(itmName.Length - 1, 1), out subfix))
            itmName = itmName.Substring(0, itmName.Length - 1);
        if (menuItm == null)
        {
            return;
        }
        bool loaded = false;
        //System.Runtime.Remoting.ObjectHandle obj = null;
        object obj = null;
        if (menuItm.MenuType == (int)MenuEntry.MenuTypeEnum.Internal)
        {
            if (itmName.ToLower().Equals("exit"))
            {
                string userName = "User Not Found";
                if (GlobalShared.user != null)
                    userName = GlobalShared.user.Account.login;
                GlobalShared.Log.Log((int)LogClass.logType.Info, "Program exited by user <" + userName + ">", false);
                System.Windows.Application.Current.Shutdown();
            }
        }
        else if (menuItm.MenuType == (int)MenuEntry.MenuTypeEnum.Method)
        {
            string subName = (string)menuItm.Target;
            object[] parms = null;
            Type mType = null;
            if (menuItm.MenuClass.Trim().Equals(""))
                mType = typeof(HelperMethods);
            else
                mType = Type.GetType(menuItm.MenuClass);
            if (mType == null)
                return;
            if (menuItm.Parameter.Trim() != "")
            {
                string[] args = menuItm.Parameter.Split(',');
                if (args != null && args.Length > 0)
                {
                    parms = new object[args.Length];
                    for(int pr = 0; pr < args.Length; pr++)
                    {
                        if (args[pr].ToString().ToLower().Equals("true"))
                            parms[pr] = true;
                        else if (args[pr].ToString().ToLower().Equals("false"))
                            parms[pr] = false;
                        else
                            parms[pr] = args[pr];
                    }
                }
            }
            GlobalShared.MethodApp = itmName;
            System.Reflection.MethodInfo myMethod = mType.GetMethod(subName);
            try
            {
                myMethod.Invoke(null, parms);
            }
            catch (Exception ex )
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to call internal method [" + myMethod.Name + "] - " + ex.Message, false);
            }
        }

        // FORM
        else if (menuItm.MenuType == (int)MenuEntry.MenuTypeEnum.Form)
        {
            //if (! Globals.GlobalShared.user.hasAppAccess(menuItm.Application.Trim()))
            Manager.RefreshPermissions();
            if (!MbcsCentral.GlobalShared.user.hasAccessPermission(itmName.Trim()))
            {
                System.Windows.MessageBox.Show("You do not have permission to access this application.");
                return;
            }
            try
            {

                if (menuItm.Parameter.Equals(""))
                {
                    object[] parms = { itmName.Trim() };
                    CallClassParms callForm = new CallClassParms("MbcsCentral." + menuItm.AppName, parms);
                    obj = callForm.returnObject(parms);
                }
                else if (menuItm.Parameter.ToLower().Equals("background"))
                {
                    //System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
                    //System.Threading.CancellationToken token = cts.Token;
                    object[] args = { itmName.Trim(), menuItm.AppName };
                    //task = Task.Run(() => StartThread(args));
                    System.Threading.Thread task = new System.Threading.Thread(StartThread);
                    task.SetApartmentState(System.Threading.ApartmentState.STA);
                    task.IsBackground = true;
                    task.Start(args);
                    return;
                }
                else
                {
                    object[] parms = { itmName.Trim(), menuItm.Parameter };
                    CallClassParms callForm = new CallClassParms("MbcsCentral." + menuItm.AppName, parms);
                    obj = callForm.returnObject(parms);
                }
            }
            catch (TypeLoadException lex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, menuItm.AppName + "(" + menuItm.Parameter + ") Form load error (lex):"  + lex.Message, false);
                return;
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, menuItm.AppName + "(" + menuItm.Parameter + ") Form load error (ex):" + ex.Message, false);
                return;
            }
            try
            {
                Window frm = (Window)obj;

                frm.Show();
            }
            catch (System.Exception ex2)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, menuItm.AppName + " Form load error (ex2):" + ex2.Message, false);

                try
                {
                    System.Windows.Forms.Form frm = (System.Windows.Forms.Form)obj;
                    frm.Show();
                }
                catch (System.Exception ex3)
                {
                    try
                    {
                        System.Runtime.Remoting.ObjectHandle RemotingObj = Activator.CreateInstance("MbcsCentral", "MbcsCentral." + menuItm.AppName);
                        System.Windows.Forms.Form frm = (System.Windows.Forms.Form)RemotingObj.Unwrap();
                        frm.Show();
                    }
                    catch (System.Exception ex4) {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, menuItm.AppName + " Form load error (ex4):" + ex4.Message, false);
                    }
                }
            }
        }
        // APPLICATION
        else if (menuItm.MenuType == (int)MenuEntry.MenuTypeEnum.Application)
        {
            if (curProcs.ContainsKey(menuItm.AppName))
            {
                try
                {
                    loaded = true;
                    exProc = System.Diagnostics.Process.GetProcessById((int)curProcs[menuItm.AppName]);
                    IntPtr hWnd = exProc.MainWindowHandle;
                    Win32Helper Win32Help = new Win32Helper();
                    Win32Helper.SetForegroundWindow(hWnd);
                }
                catch
                {
                    loaded = false;
                    try
                    {
                        curProcs.Remove(menuItm.AppName);
                    }
                    catch
                    {
                    }
                }
            }
            if (!loaded)
            {
                object[] parms = null;
                if (menuItm.Parameter.Trim() != "")
                {
                    string[] args = menuItm.Parameter.Split(',');
                    if (args != null && args.Length > 0)
                    {
                        parms = new object[args.Length];
                        for (int pr = 0; pr < args.Length; pr++)
                        {
                            if (args[pr].ToString().ToLower().Equals("true"))
                                parms[pr] = true;
                            else if (args[pr].ToString().ToLower().Equals("false"))
                                parms[pr] = false;
                            else
                                parms[pr] = args[pr];
                        }
                    }
                }
                exProc = new System.Diagnostics.Process();
                string s = "";
                if (parms != null)
                {
                    s = (string)parms[0];
                    System.Diagnostics.Process.Start(s);
                    return;
                }
                else
                {


                    exProc.StartInfo.FileName = menuItm.AppPath + menuItm.Application.Trim();
                    exProc.EnableRaisingEvents = true;
                    exProc.Exited += new System.EventHandler(ProcessExited);
                }
                try
                {
                    exProc.Start();
                    curProcs.Add(menuItm.AppName, exProc.Id);
                    curProcsV.Add(exProc.Id, menuItm.AppName);
                }
                catch (System.Exception)
                {
                }
            }
            //exProc.Dispose()
        }

    }

    private static void StartThread(object args)
    {

        // TODO: utilize CancellationTokenSource
        object[] a = (object[])args;
        object[] parms = { a[0].ToString() };
        CallClassParms callForm = new CallClassParms("MbcsCentral." + a[1].ToString(), parms);
        object obj = callForm.returnObject(parms);
        Window frm = (Window)obj;

        try
        {
            frm.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
        catch (OperationCanceledException ox)
        {}
        catch (AggregateException ax)
        { }
        catch (Exception ex)
        {

        }

    }
    internal static void ProcessExited(object sender, System.EventArgs e)
    {

        System.Diagnostics.Process proc = (System.Diagnostics.Process)sender;
        try
        {
            curProcs.Remove(curProcsV[proc.Id]);
            curProcsV.Remove(proc.Id);
            proc.Dispose();
        }
        catch (System.Exception)
        {
        }

    }

    public static void getApps()
    {

        GlobalShared.Log.Log((int)LogClass.logType.Config, "Getting external applications.", false);
        GlobalShared.Log.Log((int)LogClass.logType.Config, "  WinPM...", false);
        string winpm = Microsoft.VisualBasic.Interaction.GetSetting(AppName: "Winpm", Section: "Options", Key: "DefaultDataBase");
        if (winpm != null)
        {
            if (winpm.Trim() != "")
            {
                GlobalShared.Log.Log((int)LogClass.logType.Config, "      Found...", false);
                winpm = (string)(System.IO.Path.GetDirectoryName(winpm));
                if (!winpm.EndsWith("\\"))
                {
                    winpm += "\\";
                }
                if ((new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.FileExists(winpm + MbcsCentral.GlobalShared.APP_WINPM))
                {
                    modifyAppMenu(winpm, (string)MbcsCentral.GlobalShared.APP_WINPM, (string)MbcsCentral.GlobalShared.APP_WINPM_NAME);
                }
                if ((new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.FileExists(winpm + MbcsCentral.GlobalShared.APP_WINGL))
                {
                    modifyAppMenu(winpm, (string)MbcsCentral.GlobalShared.APP_WINGL, (string)MbcsCentral.GlobalShared.APP_WINGL_NAME);
                }
                if ((new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.FileExists(winpm + MbcsCentral.GlobalShared.APP_WINPR))
                {
                    modifyAppMenu(winpm, (string)MbcsCentral.GlobalShared.APP_WINPR, (string)MbcsCentral.GlobalShared.APP_WINPR_NAME);
                }
            }
        }

        //RegistryKey regKey;
        GlobalShared.Log.Log((int)LogClass.logType.Config, "  WinTRM...", false);
        //regKey = Registry.CurrentUser.OpenSubKey((string)MbcsCentral.GlobalShared.APP_WINTRM_REG, true);
        //if (regKey != null)
        //{
        GlobalShared.Log.Log((int)LogClass.logType.Config, "      Found...", false);
        string wintrm = Microsoft.VisualBasic.Interaction.GetSetting(AppName: "WinTRM", Section: "Options", Key: "DB Locations");
        if (wintrm != null)
        {
            if (wintrm.Trim() != "")
            {
                if (!wintrm.EndsWith("\\"))
                {
                    wintrm += "\\";
                }
                if ((new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.FileExists(wintrm + MbcsCentral.GlobalShared.APP_WINTRM))
                {
                    modifyAppMenu(wintrm, (string)MbcsCentral.GlobalShared.APP_WINTRM, (string)MbcsCentral.GlobalShared.APP_WINTRM_NAME);
                }
            }
        }
        //}

        GlobalShared.Log.Log((int)LogClass.logType.Config, "  WinDoc...", false);
        string windoc = Microsoft.VisualBasic.Interaction.GetSetting(AppName: "WinDoc", Section: "", Key: "ProgramPath");
        if (windoc != null)
        {
            if (windoc.Trim() != "")
            {
                GlobalShared.Log.Log((int)LogClass.logType.Config, "      Found...", false);
                if (!windoc.EndsWith("\\"))
                {
                    windoc += "\\";
                }
                if ((new Microsoft.VisualBasic.Devices.ServerComputer()).FileSystem.FileExists(windoc + MbcsCentral.GlobalShared.APP_WINDOC))
                {
                    modifyAppMenu(windoc, (string)MbcsCentral.GlobalShared.APP_WINDOC, (string)MbcsCentral.GlobalShared.APP_WINDOC_NAME);
                }
            }
        }

    }

    /* Database methods */
    public static bool SetDatabase(bool Permanent = true)
    {
        SqlConnectionStringBuilder CsBldr = new SqlConnectionStringBuilder();
        try
        {
            // If this is the first time running the application, machine name must be retrieved and pre-pended to the DatabaseSource
            if (GlobalShared.MachineName.Trim() == "")
            {
                string dbs = GlobalShared.DatabaseSource;
                if (GlobalShared.DatabaseSource.Contains("\\"))
                    dbs = GlobalShared.DatabaseSource.Substring(GlobalShared.DatabaseSource.IndexOf("\\") + 1);
                GlobalShared.DatabaseSource = GlobalShared.MachineName + "\\" + dbs;
                if (Permanent)
                    Main.parms.setItem("databaseSource", GlobalShared.DatabaseSource);
            }

            CsBldr.DataSource = GlobalShared.DatabaseSource;
            CsBldr.InitialCatalog = GlobalShared.InitialCatalog;
            CsBldr.Encrypt = true;
            CsBldr.TrustServerCertificate = false;
            // Needed to properly parse DatabaseSource between url and local 
            string source = (GlobalShared.DatabaseSource.Contains(".") ? GlobalShared.DatabaseSource.Substring(0, GlobalShared.DatabaseSource.IndexOf(".")) : GlobalShared.DatabaseSource);
            ;
            CsBldr.UserID = GlobalShared.DB_USER_NAME + "@" + source;
            CsBldr.Password = GlobalShared.DB_PASSWORD;
            CsBldr.IntegratedSecurity = false;
            GlobalShared.SqlConnectionString = CsBldr.ConnectionString;
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
            return false;
        }
        return true;
    }

    #region    SqlSever
    public static bool CreateDatabase(ref SqlConnection conn, string newDatabase, bool drop = false, bool droponly = false, bool cloudDb = false)
    {
        string sql;
        SqlCommand cmd;
        try
        {
            conn.Open();
        }
        catch (SqlException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Creating Database. Initial ConnectionString: " + conn.ConnectionString, false);
            if (conn.ConnectionString.Contains(",1433") || conn.ConnectionString.Contains(",1434"))
            {
                conn.ConnectionString = conn.ConnectionString.Replace(",1433", "").Replace(",1434", "");
                GlobalShared.Log.Log((int)LogClass.logType.Info, "   Changed to: " + conn.ConnectionString, false);
            }
            else
            {
                conn.ConnectionString = conn.ConnectionString.Replace(";Initial", ",1433;Initial");
                GlobalShared.Log.Log((int)LogClass.logType.Info, "   Changed to: " + conn.ConnectionString, false);

            }
            try
            {
                conn.Open();
            }
            catch (SqlException qex)
            {
                MessageBox.Show("Unable to create DB Connection: " + qex.Message);
                if (qex.InnerException != null)
                {
                    MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                }
                return false;
            }
            catch (Exception qex)
            {
                if (conn.ConnectionString.Contains(",1433") || conn.ConnectionString.Contains(",1434"))
                {
                    conn.ConnectionString = conn.ConnectionString.Replace(",1433", "").Replace(",1434", "");
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "   Changed to: " + conn.ConnectionString, false);
                }
                else
                {
                    conn.ConnectionString = conn.ConnectionString.Replace(";Initial", ",1433;Initial");
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "   Changed to: " + conn.ConnectionString, false);

                }
                try
                {
                    conn.Open();
                }
                catch (SqlException sex)
                {
                    MessageBox.Show("Unable to create DB Connection: " + sex.Message);
                    if (qex.InnerException != null)
                    {
                        MessageBox.Show("Inner Exception: " + sex.InnerException.Message);
                    }
                    return false;

                }
            }
        }

        if (drop)
        {
            sql = "USE [master]; ALTER DATABASE[" + newDatabase + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            cmd = new SqlCommand(sql, conn);
            if (!cloudDb)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    if (droponly)
                    {
                        MessageBoxCustom.Show("Unable to SET SINGLE_USER WITH ROLLBACK IMMEDIATE. " + ex.Message);
                        return false;
                    }
                }
            }
            sql = "DROP DATABASE " + newDatabase;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBoxCustom.Show("Database <" + newDatabase + "> successfully DELETED.");
            }
            catch (System.Exception ex)
            {
                //MessageBoxCustom.Show("Unable to drop target database. " + ex.Message);
                if (droponly)
                {
                    return false;
                }
            }
        }
        if (droponly)
            return true;

        sql = "CREATE DATABASE " + newDatabase;
        cmd = new SqlCommand(sql, conn);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CreateDatabase: Error creating database <" + newDatabase + "> on <" + conn.ConnectionString + ">: " + ex.Message, false);
            return false;
        }
        finally
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }

    public static bool BackupDatabase(SqlConnection conn, string DatabaseName, string backupType, string location, out string savedLocation, out string backupFile,bool copy_Only = false, bool overwrite = false)
    {
        savedLocation = "";
        string sql;
        string backupDate = DateTime.Now.ToString("yyyMMdd");
        backupFile = DatabaseName + "_" + backupDate + ".bak";
        string withOptions = (backupType.Equals("F") ? "INIT" : "DIFFERENTIAL") + (copy_Only ? ", COPY_ONLY" : "");
        SqlCommand cmd;
        try
        {
            conn.Open();
        }
        catch (SqlException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "BackupDatabase: Error opening SQL Connection <" + conn.ConnectionString + ">: " + ex.Message, false);
            return false;
        }

        if (!location.EndsWith("\\"))
            location += "\\";
        savedLocation = location + backupFile;

        string backupName = DatabaseName + " backup for " + backupDate;
        sql = "BACKUP DATABASE " + DatabaseName + " TO DISK = '" + savedLocation +
            "' WITH " + withOptions + ", NAME='" + backupName + "'" +
            ", NOSKIP, NOFORMAT";
        cmd = new SqlCommand(sql, conn);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (SqlException sex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "BackupDatabase:<" + DatabaseName + "> on <" + conn.ConnectionString + ">: " + sex.Message, false);
            if (sex.InnerException != null)
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "    " + sex.InnerException.Message, false);
            return false;

        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "BackupDatabase: Error backing up database <" + DatabaseName + "> on <" + conn.ConnectionString + ">: " + ex.Message, false);
            return false;
        }
        finally
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }

    public static bool RestoreDatabase1(string databaseName, DbBackups dbFull, DbBackups dbDiff = null)
    {
        string restoreRecovery = "RESTORE DATABASE " + databaseName;
        string restoreNoRecovery;
        return true;
    }
    public static bool RestoreDatabase(string connection, string DatabaseFullPath, string backUpPath, bool withRecover = true, bool withAlterSingle = true, bool withAlterMulti = true)
    {
        string connectionString = connection;
        SqlConnection sqlcon = new SqlConnection(connectionString);
        if (sqlcon.State == System.Data.ConnectionState.Closed)
            sqlcon.Open();
        using (sqlcon)
        {
            string UseMaster = "USE master";
            try
            {
                SqlCommand UseMasterCommand = new SqlCommand(UseMaster,sqlcon);
                UseMasterCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (sqlcon.State == System.Data.ConnectionState.Open)
                    sqlcon.Close();
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == System.Data.ConnectionState.Open)
                    sqlcon.Close();
                MessageBox.Show(ex.Message);
                return false;
            }
            if (withAlterSingle)
            {
                // The below query will rollback any transaction which is running on that database and brings SQL Server database in a single user mode
                string Alter1 = @"ALTER DATABASE [" + DatabaseFullPath + "] SET Single_User WITH Rollback Immediate";
                SqlCommand Alter1Cmd = new SqlCommand(Alter1, sqlcon);
                try
                {
                    Alter1Cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (sqlcon.State == System.Data.ConnectionState.Open)
                        sqlcon.Close();
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            // The below query will restore database file from disk where backup was taken ....
            string Restore = @"RESTORE DATABASE [" + DatabaseFullPath + "] FROM DISK = N'" +
            backUpPath + @"' WITH  FILE = 1, " + (withRecover ? " RECOVERY" : " NORECOVERY") + ", NOUNLOAD,  STATS = 10";
            try
            {
                SqlCommand RestoreCmd = new SqlCommand(Restore, sqlcon);
                RestoreCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (sqlcon.State == System.Data.ConnectionState.Open)
                    sqlcon.Close();
                MessageBox.Show(ex.Message);
                return false;
            }
            if (withAlterMulti)
            {
                // the below query change the database back to multiuser
                string Alter2 = @"ALTER DATABASE [" + DatabaseFullPath + "] SET Multi_User";
                try
                {
                    SqlCommand Alter2Cmd = new SqlCommand(Alter2, sqlcon);
                    Alter2Cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    if (sqlcon.State == System.Data.ConnectionState.Open)
                        sqlcon.Close();
                }
            }
            return true;
        }
    }

    public static void CopyStoredProcs(System.Data.SqlClient.SqlConnection LocalConnection, System.Data.SqlClient.SqlConnection RemoteConnection)
    {
        Server RemoteServer = new Server(RemoteConnection.DataSource);
        Server LocalServer = new Server(LocalConnection.DataSource);

        Database RemoteDB = RemoteServer.Databases[RemoteConnection.Database];
        Database LocalDB = LocalServer.Databases[LocalConnection.Database];

        StoredProcedure CEHStoredProc;

        foreach (StoredProcedure SP in LocalDB.StoredProcedures)
        {
            if (!SP.IsSystemObject)
            {
                if (RemoteDB.StoredProcedures.Contains(SP.Name))
                {
                    RemoteDB.ExtendedStoredProcedures[SP.Name].Drop();
                }

                CEHStoredProc = new StoredProcedure(RemoteDB, SP.Name);

                CEHStoredProc.TextMode = false;

                foreach (StoredProcedureParameter Parm in SP.Parameters)
                {
                    StoredProcedureParameter lParm = new StoredProcedureParameter(CEHStoredProc, Parm.Name, Parm.DataType);
                    lParm.DefaultValue = Parm.DefaultValue;
                    lParm.IsOutputParameter = Parm.IsOutputParameter;
                    CEHStoredProc.Parameters.Add(lParm);
                }
                CEHStoredProc.TextBody = SP.TextBody;

                if (LocalDB.StoredProcedures.Contains(SP.Name))
                {
                    CEHStoredProc.Alter();
                }
                else
                {
                    CEHStoredProc.Create();
                }

                ObjectPermissionSet spPermissions = new ObjectPermissionSet();
                spPermissions.Execute = true;
                CEHStoredProc.Grant(spPermissions, LocalDB.UserName);
            }
        }

    }

    public static void CopyTables(System.Data.SqlClient.SqlConnection LocalConnection, System.Data.SqlClient.SqlConnection RemoteConnection)
    {
        Server RemoteServer = new Server(RemoteConnection.DataSource);
        Server LocalServer = new Server(LocalConnection.DataSource);

        Database RemoteDB = RemoteServer.Databases[RemoteConnection.Database];
        Database LocalDB = LocalServer.Databases[LocalConnection.Database];

        Table CEHTable;

        foreach (Table tbl in LocalDB.Tables)
        {
            if (!tbl.IsSystemObject)
            {
                if (RemoteDB.Tables.Contains(tbl.Name))
                {
                    try
                    {
                        RemoteDB.Tables[tbl.Name].Drop();
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase: " + ex.Message, false);
                        // Implement your own error handler here
                    }

                }

                CEHTable = new Table(RemoteDB, tbl.Name);

                foreach (Column col in tbl.Columns)
                {
                    Column lCol = new Column(CEHTable, col.Name, col.DataType);

                    if (col.Identity)
                    {
                        lCol.Identity = col.Identity;
                        lCol.IdentityIncrement = col.IdentityIncrement;
                        lCol.IdentitySeed = col.IdentitySeed;
                    }

                    lCol.Nullable = col.Nullable;

                    CEHTable.Columns.Add(lCol);
                }

                try
                {
                    CEHTable.Create();
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase: " + ex.Message, false);
                    // Implement your own error handler here
                }

                try
                {
                    ObjectPermissionSet tblPermissions = new ObjectPermissionSet();
                    tblPermissions.Select = true;
                    CEHTable.Grant(tblPermissions, LocalDB.UserName);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase: " + ex.Message, false);
                    // Implement your own error handler here
                }

            }
        }

    }

    public static void CopyTable(string TableName, System.Data.SqlClient.SqlConnection RemoteConnection, System.Data.SqlClient.SqlConnection LocalConnection)
    {
        System.Data.SqlClient.SqlCommand LocalCommand = new System.Data.SqlClient.SqlCommand("Select * FROM " + TableName, LocalConnection);
        LocalCommand.CommandTimeout = 1600;
        System.Data.SqlClient.SqlDataReader CEHDataReader = LocalCommand.ExecuteReader();
        System.Data.SqlClient.SqlBulkCopy CEHBulkCopy = new System.Data.SqlClient.SqlBulkCopy(RemoteConnection.ConnectionString, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity);
        CEHBulkCopy.DestinationTableName = TableName;
        CEHBulkCopy.BulkCopyTimeout = 1600;


        try
        {
            CEHBulkCopy.WriteToServer(CEHDataReader);
        }
        catch (SqlException exSQL)
        {
            MessageBox.Show(exSQL.Message, "Bulk Copy Error");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Bulk Copy Error - General");
        }
        finally
        {
            CEHDataReader.Close();
            CEHBulkCopy.Close();
        }

    }

    public static bool CopyDatabaseNew(SqlConnection sourceConn, SqlConnection targetConn)
    {
        SqlTransfer sqlTransfer;
        DataTransfer dataTransfer;
        List<SqlObject> sqlItems;
        List<MbcsCentral.Data.DataObject> dataItems;
        try
        {
            sqlTransfer = new SqlTransfer(MbcsCentral.Properties.Settings.Default.SourceServer, MbcsCentral.Properties.Settings.Default.DestinationServer);
            sqlItems = sqlTransfer.SourceObjects;
            if (sqlItems != null || sqlItems.Count > 0)
            {
                CopySchema(sqlItems, sqlTransfer);
            }
        }
        catch (Exception ex)
        {
            MessageBoxCustom.Show(ex.Message);
            return false;
        }

        try
        {

            dataTransfer = new DataTransfer(MbcsCentral.Properties.Settings.Default.SourceServer, MbcsCentral.Properties.Settings.Default.DestinationServer);
            dataItems = new List<MbcsCentral.Data.DataObject>();
            /*
            foreach (var item in MbcsCentral.Properties.Settings.Default.DataSettings)
            {
                var array = item.Split('\t');
                var itm = new MbcsCentral.Data.DataObject();
                itm.Table = array[0];
                if (array.Count() == 2)
                    itm.SqlCommand = array[1];
                dataItems.Add(itm);
            }
            */

            if (dataItems.Count == 0)
                dataItems = dataTransfer.SourceObjects;
               
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
            return false;
        }

        if (dataItems != null || dataItems.Count() > 0)
        {
            CopyData(dataItems, dataTransfer);
        }
        else
        {
            MessageBox.Show("No SQL objects found in source database to proceed");
        }

        return true;
    }

    private static void CopySchema(List<SqlObject> list, SqlTransfer transfer, bool DropBeforeCreate = false, bool CopyConstraints = true)
    {
        double current = 0;
        double max = list.Count;
        double errorCount = 0;

        if (CopyConstraints)
            max += (list.Where(i => i.Type == "Table").Count() * 3.0);

        foreach (var item in list)
        {
            try
            {
                if (DropBeforeCreate)
                {
                    transfer.DropAndCreateObject(item.Object);
                }
                else
                {
                    transfer.CreateObject(item.Object);
                }
                //item.Status = Properties.Resources.success;
            }
            catch (Exception exc)
            {
                //item.Status = Properties.Resources.failure;
                item.Error = exc.Message;
                if (exc.InnerException != null)
                    item.Error = exc.InnerException.Message;
                errorCount++;
            }

        }

        if (CopyConstraints)
        {
            foreach (var item in list.Where(i => i.Type == "Table"))
            {
                transfer.ApplyIndexes(item.Object);
            }

            foreach (var item in list.Where(i => i.Type == "Table"))
            {
                transfer.ApplyForeignKeys(item.Object);
            }

            foreach (var item in list.Where(i => i.Type == "Table"))
            {
                transfer.ApplyChecks(item.Object);
            }
        }

    }

    private static void CopyData(List<MbcsCentral.Data.DataObject> list, DataTransfer transfer)
    {
        double current = 0;
        double max = list.Count;


        foreach (MbcsCentral.Data.DataObject item in list)
        {

            //if (item.IsNewRow)
           //     continue;

            try
            {
                if (item.SqlCommand == null || string.IsNullOrWhiteSpace(item.SqlCommand.ToString()))
                {
                    item.SqlCommand = "SELECT * FROM " + item.Table + " WITH(NOLOCK)";
                }
                transfer.TrasnferData(item.Table, item.SqlCommand);
                item.Status = true;
            }
            catch (Exception exc)
            {
                item.Status = false;
                item.Error = exc.Message;
                if (exc.InnerException != null)
                    item.Error = exc.InnerException.Message;
                //errorCount++;
            }
        }

    }

    public static bool CopyDatabase(SqlConnection sourceConn, SqlConnection tartgetConn)
    {
               Server mySourceServer = new Server(sourceConn.DataSource);
       Server myDestinationServer = new Server(tartgetConn.DataSource);
       //Using windows authentication 
       //mySourceServer.ConnectionContext.LoginSecure = true;
       //Using SQL Server authentication 
       
       mySourceServer.ConnectionContext.LoginSecure = false;
       mySourceServer.ConnectionContext.Login = "mbartmon";
       mySourceServer.ConnectionContext.Password = "Vietnam65";
       try
       {
           mySourceServer.ConnectionContext.Connect();
       }
       catch (Exception ex)
       {
           if (mySourceServer.ConnectionContext.ConnectionString.Contains(",1433") || mySourceServer.ConnectionContext.ConnectionString.Contains(",1434"))
               mySourceServer.ConnectionContext.ConnectionString = mySourceServer.ConnectionContext.ConnectionString.Replace(",1433", "").Replace(",1434", "");
           else
               mySourceServer.ConnectionContext.ConnectionString = mySourceServer.ConnectionContext.ConnectionString.Replace(";Initial", ",1433;Initial");
           try
           {
               mySourceServer.ConnectionContext.Connect();
           }
           catch (Exception qex)
           {
               MessageBox.Show("Unable to create DB Connection: " + qex.Message);
               if (qex.InnerException != null)
               {
                   MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                   return false;
               }
           }
       }
 
       //Using windows authentication 
       //myDestinationServer.ConnectionContext.LoginSecure = true;
       //Using SQL Server authentication 
       myDestinationServer.ConnectionContext.LoginSecure = false;
       myDestinationServer.ConnectionContext.Login = "mbartmon";
       myDestinationServer.ConnectionContext.Password = "Vietnam65";
       myDestinationServer.ConnectionContext.Connect();
       Database dbSourceAW = mySourceServer.Databases[sourceConn.Database];
       Database dbDestinationAW = myDestinationServer.Databases[tartgetConn.Database];
       //Database dbDestinationAW = new Database(myDestinationServer, tartgetConn.Database);
       // Create method will create the database on the specified server 
       //dbDestinationAW.Create();
       GlobalShared.Log.Log((int)LogClass.logType.Info, String.Format("CopyDatabase: Database [{0}] created at [{1}] server.", dbDestinationAW.Name, myDestinationServer.Name),false );       //Create an object of Transfer class and pass
       //reference of source database to its constructor 
       GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: Instantiating Transfer object", false);
       Transfer trsfrDB;
       try
       {
           trsfrDB = new Transfer(dbSourceAW);
       }
       catch (Exception ex)
       {
           GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase error: " + ex.Message, false);
           return false;
       }
       GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: Setting properties on Transfer object.", false);
       trsfrDB.CopyAllObjects = false;
       trsfrDB.CopySchema = true;
       trsfrDB.Options.SchemaQualify = true;
       //Copy all user defined data types from source to destination 
       trsfrDB.CopyAllUserDefinedDataTypes = true;
       //Copy all tables from source to destination 
       trsfrDB.CopyAllTables = true;
       //Copy data of all source tables to destination tables 
       //It actually generates INSERT statement for destination 
       trsfrDB.CopyData = true;
       //Copy all stored procedure from source to destination 
       trsfrDB.CopyAllStoredProcedures = true;
       trsfrDB.Options.WithDependencies = true;
       trsfrDB.Options.DriPrimaryKey = true;
       trsfrDB.Options.DriIndexes = true;
       //specify the destination server name 
       trsfrDB.DestinationServer = myDestinationServer.Name;
       trsfrDB.DestinationLoginSecure = false;
       trsfrDB.DestinationLogin = "mbartmon";
       trsfrDB.DestinationPassword = "Vietnam65";
       //specify the destination database name 
       trsfrDB.DestinationDatabase = dbDestinationAW.Name;
       //TransferData method transfers the schema objects and data 
       //whatever you have specified to destination database 
 
       GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: About to start transfer.", false);
       try
       {
           trsfrDB.TransferData();
       }
       catch (Exception ex)
       {
           var realException = ex.GetType().Name;
           GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase error: " + ex.Message + "{" + realException + "}", false);
           MessageBoxCustom.Show("CopyDatabase error: " + ex.Message + "{" + realException + "} \r\nRefer to Application Log for more information. \r\n"+ DateTime.Now);
 
           GlobalShared.Log.Log((int)LogClass.logType.Debug, "   InnerException: " + ex.InnerException.Message, false);
           
           return false;
       }
        return true;
        /*
        Server mySourceServer = new Server(sourceConn.DataSource);
        Server myDestinationServer = new Server(tartgetConn.DataSource);
        //Using windows authentication 
        //mySourceServer.ConnectionContext.LoginSecure = true;
        //Using SQL Server authentication 
        
        mySourceServer.ConnectionContext.LoginSecure = false; 
        mySourceServer.ConnectionContext.Login = "mbartmon"; 
        mySourceServer.ConnectionContext.Password = "Vietnam65";
        try
        {
            mySourceServer.ConnectionContext.Connect();
        }
        catch (Exception ex)
        {
            if (mySourceServer.ConnectionContext.ConnectionString.Contains(",1433"))
                mySourceServer.ConnectionContext.ConnectionString = mySourceServer.ConnectionContext.ConnectionString.Replace(",1433", "");
            else
                mySourceServer.ConnectionContext.ConnectionString = mySourceServer.ConnectionContext.ConnectionString.Replace(";Initial", ",1433;Initial");
            try
            {
                mySourceServer.ConnectionContext.Connect();
            }
            catch (Exception qex)
            {
                MessageBox.Show("Unable to create DB Connection: " + qex.Message);
                if (qex.InnerException != null)
                {
                    MessageBox.Show("Inner Exception: " + qex.InnerException.Message);
                    return false;
                }
            }
        }

        //Using windows authentication 
        //myDestinationServer.ConnectionContext.LoginSecure = true;
        //Using SQL Server authentication 
        myDestinationServer.ConnectionContext.LoginSecure = false; 
        myDestinationServer.ConnectionContext.Login = "mbartmon"; 
        myDestinationServer.ConnectionContext.Password = "Vietnam65"; 
        myDestinationServer.ConnectionContext.Connect();
        Database dbSourceAW = mySourceServer.Databases[sourceConn.Database];
        Database dbDestinationAW = myDestinationServer.Databases[tartgetConn.Database];
        //Database dbDestinationAW = new Database(myDestinationServer, tartgetConn.Database);
        // Create method will create the database on the specified server 
        //dbDestinationAW.Create();
        GlobalShared.Log.Log((int)LogClass.logType.Info, String.Format("CopyDatabase: Database [{0}] created at [{1}] server.", dbDestinationAW.Name, myDestinationServer.Name),false );       //Create an object of Transfer class and pass
        //reference of source database to its constructor 
        GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: Instantiating Transfer object", false);
        Transfer trsfrDB;
        try
        {
            trsfrDB = new Transfer(dbSourceAW);
        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase error: " + ex.Message, false);
            return false;
        }
        GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: Setting properties on Transfer object.", false);
        //trsfrDB.CopyAllObjects = false;
        trsfrDB.CopySchema = true;
        //trsfrDB.Options.SchemaQualify = true;
        //Copy all user defined data types from source to destination 
        trsfrDB.CopyAllUserDefinedDataTypes = true;
        //Copy all tables from source to destination 
        trsfrDB.CopyAllTables = true;
        //Copy data of all source tables to destination tables 
        //It actually generates INSERT statement for destination 
        trsfrDB.CopyData = true;
        //Copy all stored procedure from source to destination 
        trsfrDB.CopyAllStoredProcedures = true;
        trsfrDB.Options.WithDependencies = true;
        trsfrDB.Options.DriPrimaryKey = true;
        trsfrDB.Options.DriIndexes = true;
        //specify the destination server name 
        trsfrDB.DestinationServer = myDestinationServer.Name;
        trsfrDB.DestinationLoginSecure = true;
        trsfrDB.DestinationLogin = "mbartmon";
        trsfrDB.DestinationPassword = "Vietnam65";
        //specify the destination database name 
        trsfrDB.DestinationDatabase = dbDestinationAW.Name;
        //trsfrDB.DropDestinationObjectsFirst = true;
        trsfrDB.Options.ContinueScriptingOnError = true;
        trsfrDB.CreateTargetDatabase = true;
        //TransferData method transfers the schema objects and data 
        //whatever you have specified to destination database 

        GlobalShared.Log.Log((int)LogClass.logType.Info, "CopyDatabase: About to start transfer.", false);
        try
        {
            trsfrDB.TransferData();
        }
        catch (Exception ex)
        {
            var realException = ex.GetType().Name;
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CopyDatabase error: " + ex.Message + "{" + realException + "}", false);
            MessageBoxCustom.Show("CopyDatabase error: " + ex.Message + "{" + realException + "} \r\nRefer to Application Log for more information. \r\n"+ DateTime.Now);

            GlobalShared.Log.Log((int)LogClass.logType.Debug, "   InnerException: " + ex.InnerException.Message, false);
            
            return false;
        }
        return true;   
        */
    }

    public static void RunSQLCmd(string DestinationServer, string DestinationDatabase, string SqlToExecute, string user, string password, string OutSqlFileName = null, string OutFileName = null)
    {
        using (Process proc = new Process())

        {

            proc.StartInfo.FileName = @"""SqlCmd.exe""";
            proc.StartInfo.Arguments = @"-U" + user + " -P" + password + " -S" + DestinationServer + " -d" + DestinationDatabase + @" -i""" + SqlToExecute;
            if (OutSqlFileName != null)
                proc.StartInfo.Arguments += @""" -o""" + OutSqlFileName + @"""";
            proc.StartInfo.UseShellExecute = false;
            if (OutSqlFileName != null)
            {
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
            }
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            if (OutSqlFileName != null)
            {
                System.IO.File.AppendAllText(OutFileName, proc.StandardOutput.ReadToEnd());
                proc.WaitForExit();
                System.IO.File.AppendAllText(OutFileName, proc.StandardOutput.ReadToEnd());
            }
            proc.Close();

        }
    }

    public static void CreateDBBackup(string dbName, string location, string batchFile = GlobalShared.CONFIG_DB_BACKUP_FILENAME)
    {
        try
        {
            int FileRef = FileSystem.FreeFile();
            FileSystem.FileOpen(FileRef, batchFile, OpenMode.Output, OpenAccess.Default, OpenShare.Shared);
            string cmd = "sqlcmd -S .\\MBCSCENTRALSQL -U " + GlobalShared.DB_USER_NAME + " -P " + GlobalShared.DB_PASSWORD + " -Q \"EXEC sp_BackupDatabases @backupLocation = '" + location +"', @databaseName='" + dbName + "', @backupType='D'\"";
            FileSystem.PrintLine(FileRef, cmd);
            FileSystem.FileClose(FileRef);
        }
        catch (System.Exception ex)
        {
        }
    }

    public static Registry[] RetrieveDbFolders()
    {
        Registry[] folders = null;
        folders = GetRegistryEntries(GlobalShared.CONFIG_DATABASE_REG_KEY, GlobalShared.CONFIG_DATABASE_REG_FOLDER);
        return folders;
    }

    public static Registry[] RetrieveDbNames()
    {
        Registry[] names = null;
        names = GetRegistryEntries(GlobalShared.CONFIG_DATABASE_REG_KEY, GlobalShared.CONFIG_DATABASE_REG_NAME);
        return names;

    }
    #endregion

    #region Email
    public static bool CheckEmailDirectories()
    {
        bool rc = true;
        try
        {
            Directory.CreateDirectory(GlobalShared.appPath + GlobalShared.EMAIL_FOLDER + GlobalShared.EMAIL_FOLDER_IN);
        }
        catch (IOException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories>: Could not find or create folder [" + GlobalShared.EMAIL_FOLDER_IN + "]", System.Windows.Forms.MessageBoxButtons.OK,"",true);
            rc = false;
        }
        catch (UnauthorizedAccessException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories>: User not authorized to create folder [" + GlobalShared.EMAIL_FOLDER_IN + "]", System.Windows.Forms.MessageBoxButtons.OK, "", true);
            rc = false;
        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories> [" + GlobalShared.EMAIL_FOLDER_IN + "]: " + ex.Message, System.Windows.Forms.MessageBoxButtons.OK, "", true);
            rc = false;
        }
        try
        {
            Directory.CreateDirectory(GlobalShared.appPath + GlobalShared.EMAIL_FOLDER + GlobalShared.EMAIL_FOLDER_OUT);
        }
        catch (IOException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories>: Could not find or create folder [" + GlobalShared.EMAIL_FOLDER_OUT + "]", System.Windows.Forms.MessageBoxButtons.OK, "", true);
            rc = false;
        }
        catch (UnauthorizedAccessException ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories>: User not authorized to create folder [" + GlobalShared.EMAIL_FOLDER_OUT + "]", System.Windows.Forms.MessageBoxButtons.OK, "", true);
            rc = false;
        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<CheckEmailDirectories> [" + GlobalShared.EMAIL_FOLDER_OUT + "]: " + ex.Message, System.Windows.Forms.MessageBoxButtons.OK, "", true);
            rc = false;
        }
        return rc;
    }
    #endregion

    private static void modifyAppMenu(string apPath, string apExec, string appName)
    {

        bool modified = false;

        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(GlobalShared.appPath + menuDocument);
        }
        catch (IOException)
        {
        }
        catch (System.Exception)
        {
        }

        XmlNode node;
        node = xmlDoc.DocumentElement;
        XmlNode node2 = null;
        XmlAttributeCollection attrs;
        bool found = false;

        foreach (XmlNode tempLoopVar_node2 in node.ChildNodes)
        {
            node2 = tempLoopVar_node2;
            if (node2.Name == "MenuItem")
            {
                attrs = node2.Attributes;
                foreach (XmlAttribute attr in attrs)
                {
                    if (attr.Name == "Header" && attr.Value.ToLower() == appName.ToLower())
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (node2.Name == "FixedMenus")
            {
                break;
            }
        }

        if (!found)
        {
            XmlElement newNode;
            newNode = xmlDoc.CreateElement("MenuItem");
            newNode.SetAttribute("Header", appName);
            newNode.SetAttribute("Name", appName.ToLower());
            newNode.SetAttribute("Type", "Application");
            string s = "  <Application Name=\"" + appName + "\">" + "\r\n" + "    <GlobalShared.appPath Path=\"" + apPath + "\" Name=\"" + apExec + "\" />" + "\r\n" + "  </Application>";
            try
            {
                newNode.InnerXml = s;
            }
            catch (InvalidOperationException ex)
            {
                s = (string)ex.Message;
            }
            catch (XmlException ex)
            {
                s = (string)ex.Message;
            }
            xmlDoc.DocumentElement.InsertBefore(newNode, node2);
            modified = true;
        }

        if (modified)
        {
            xmlDoc.Save(GlobalShared.appPath + menuDocument);
        }

    }

    public static void setMainWindowTitle()
    {

        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        System.Version version = assembly.GetName().Version;

        MainWindow frm = (MainWindow)Main.mSession.getParam("MainWindow");
        if (frm != null)
        {
            Clients client = null;
            if (GlobalShared.user != null)
                client = Clients.load(GlobalShared.user.Client.clientId);
            if (client == null)
                return;
            MbcsCentral.WinPM.Control control = MbcsCentral.WinPM.Control.load(GlobalShared.user.Client.clientId);
            if (client == null || control == null)
            {
                MessageBoxCustom.Show("Unable to retrieve essential data.  \r\n\nThe database may be incompatible or currupted.");
                return;

            }
            string wingl = "";
            if (client.WinGlEnabled != null && client.WinGlEnabled && !control.UseExternalWinGL && control.UseWingl)
                wingl= " - WinGL -";
            string baseCatalog = Main.parms.getParam("baseCatalog").ToString();
            //string t = Main.parms.getParam("initialCatalog").ToString();
            string t = GlobalShared.InitialCatalog;
            frm.TextTitle.Text = "<" + GlobalShared.userName + ">";
            if (!baseCatalog.ToLower().Equals(t.ToLower()))
                frm.TextTitle.Inlines.Add(new System.Windows.Documents.Run(" [" + t + "]") { Foreground = System.Windows.Media.Brushes.Red, FontWeight = FontWeights.Bold });
            else
                frm.TextTitle.Inlines.Add(" [" + t + "]");
            frm.TextTitle.Inlines.Add(wingl + " " + GlobalShared.DatabaseSource + " Version: " + version.ToString());
            /*
            string[] s = Main.mSession.getParam("connectionString").ToString().Replace("Data Source=", "").Replace("Initial Catalog", "").Split(';');
            if (s.Length > 0)
            {
                int i = 0;
                foreach (string x in s)
                {
                    if (x.ToLower().StartsWith("initial catalog"))
                    {
                        
                    }
                    t += x + ";";
                    i++;
                    if (i > 2)
                    {
                        break;
                    }
                }
            }
            
            frm.TextTitle.Text = "<" + MbcsCentral.GlobalShared.userName + "> [" + t + "] - Version: " + version.ToString();
            */
        }

    }

    public static void saveForm(Window frm)
    {
        //var hwnd = new System.Windows.Interop.WindowInteropHelper(frm).EnsureHandle();
        //var currentMonitor = NativeMethods.MonitorFromWindow(hwnd, NativeMethods.MONITOR_DEFAULTTONEAREST);
        int s = 0;
        switch (frm.WindowState)
        {
            case System.Windows.WindowState.Normal:
                s = (int)System.Windows.WindowState.Normal;
                break;
            case System.Windows.WindowState.Minimized:
                s = (int)System.Windows.WindowState.Minimized;
                break;
            case System.Windows.WindowState.Maximized:
                s = (int)System.Windows.WindowState.Maximized;
                break;
        }
        // Restore if needed - getForm still logs
        GlobalShared.Log.Log((int)LogClass.logType.Info, String.Format("saveForm-IN:<{0}> Top<{1}> Left<{2}> Width<{3}> Height<{4}>", frm.Name, frm.Top.ToString(), frm.Left.ToString(), frm.Width.ToString(), frm.Height.ToString()), false);
        try
        {
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Top", (frm.Top != null && !Double.IsNaN(frm.Top) && frm.Top < GlobalShared.Screen_Height ? (int)frm.Top : 0), (int)MbcsCentral.Registry.DataTypes.Dword);
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Left", (frm.Left != null && !Double.IsNaN(frm.Left) && frm.Left > 0 && frm.Left < GlobalShared.Screen_Width ? (int)frm.Left : 0), (int)MbcsCentral.Registry.DataTypes.Dword);
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Width", (frm.Width != null && !Double.IsNaN(frm.Width) ? (int)frm.Width : 100), (int)MbcsCentral.Registry.DataTypes.Dword);
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Height", (frm.Height != null && !Double.IsNaN(frm.Height) ? (int)frm.Height : 100), (int)MbcsCentral.Registry.DataTypes.Dword);
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "State", s, (int)MbcsCentral.Registry.DataTypes.Dword);
            SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "FontSize", frm.FontSize, (int)MbcsCentral.Registry.DataTypes.Dword);
            //GlobalShared.Log.Log((int)LogClass.logType.Info, String.Format("saveForm-OUT:<{0}> Top<{1}> Left<{2}> Width<{3}> Height<{4}>", frm.Name, (frm.Top != null && !Double.IsNaN(frm.Top) && frm.Top < 1080 ? frm.Top : 0).ToString(), (frm.Left != null && !Double.IsNaN(frm.Left) && frm.Left < 1920 ? frm.Left : 0).ToString(), (frm.Width != null && !Double.IsNaN(frm.Width) ? frm.Width : 100).ToString(), (frm.Height != null && !Double.IsNaN(frm.Height) ? frm.Height : 100).ToString()), false);
        }
        catch (Exception ex) {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "saveForm: " + ex.Message, false);
            if (frm.Width < 0)
                frm.Width = 720;
            if (frm.Height < 0)
                frm.Height = 720;
            if (frm.Top < 0)
                frm.Top = 0;
            if (frm.Left < 0)
                frm.Left = 0;
            try
            {
                SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Top", (frm.Top != null && !Double.IsNaN(frm.Top) && frm.Top < GlobalShared.Screen_Height ? (int)frm.Top : 0), (int)MbcsCentral.Registry.DataTypes.Dword);
                SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Left", (frm.Left != null && !Double.IsNaN(frm.Left) && frm.Left >= 0 && frm.Left < GlobalShared.Screen_Width ? (int)frm.Left : 0), (int)MbcsCentral.Registry.DataTypes.Dword);
                SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Width", (frm.Width != null && !Double.IsNaN(frm.Width) ? (int)frm.Width : 100), (int)MbcsCentral.Registry.DataTypes.Dword);
                SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Height", (frm.Height != null && !Double.IsNaN(frm.Height) ? (int)frm.Height : 100), (int)MbcsCentral.Registry.DataTypes.Dword);

            }
            catch (Exception ex2)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "saveForm: " + ex.Message, false);

            }
        }
    }

    public static void GetAllRegistryEntries()
    {
        string key = "";
        if (RegEntries != null)
            RegEntries.Clear();
        else
            RegEntries = new Dictionary<string, long>();

        try
        {
            MbcsCentral.Registry[] regs = MbcsCentral.Registry.loadByUser(GlobalShared.user.Account.accountId);
            if (regs != null && regs.Length > 0)
            {
                foreach (MbcsCentral.Registry reg in regs)
                {
                    if (!reg.mainKey.EndsWith("\\"))
                        reg.mainKey += "\\";
                    key = reg.mainKey + reg.subKey;
                    if (!RegEntries.ContainsKey(key))
                        RegEntries.Add(key, reg.ID);
                }
            }
            MbcsCentral.Registry[] regsMaster = MbcsCentral.Registry.loadByUser(GlobalShared.MASTERUSER);
            if (regsMaster != null && regsMaster.Length > 0)
            {
                foreach (MbcsCentral.Registry reg in regsMaster)
                {
                    if (!reg.mainKey.EndsWith("\\"))
                        reg.mainKey += "\\";
                    key = reg.mainKey + reg.subKey;
                    if (!RegEntries.ContainsKey(key))
                        RegEntries.Add(key, reg.ID);
                }
            }

        }
        catch { }
    }

    public static void SetRegistryEntry(string keyName, string subKey, object value, int dataType, bool masterUser = false)
    {
        MbcsCentral.Registry reg = null;
        long id;
        string key = keyName;
        if (!key.EndsWith("\\"))
            key += "\\";
        key += subKey;
        if (RegEntries != null && RegEntries.ContainsKey(key))
        {
            if (RegEntries.TryGetValue(key, out id))
            {
                reg = MbcsCentral.Registry.load(id);
                if (reg != null)
                    reg.value = value;
            }
        }
        else
            try
            {
                reg = new MbcsCentral.Registry(dataType, 0, GlobalShared.user.Account.clientId, (long)(masterUser ? GlobalShared.MASTERUSER : GlobalShared.user.Account.accountId), value, keyName, subKey);
            }
            catch { }

        if (reg !=null && reg.save())
            if (!RegEntries.ContainsKey(key))
                RegEntries.Add(key, reg.ID);
    }

    public static void SetRegistryEntries(Registry[] regs, bool masterUser = false)
    {
        Registry reg = null;
        long id;
        if (regs == null || regs.Length == 0)
            return;
        foreach (MbcsCentral.Registry entry in regs)
        {
            string key = entry.mainKey + entry.subKey;
            if (entry != null)
                if (entry.save())
                {
                    if (!RegEntries.ContainsKey(key))
                        RegEntries.Add(key, reg.ID);
                }
                else
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "SetRegistryEntry <:" + key + "> entry.save() == false", false);

            else
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "SetRegistryEntry: <" + key + "> entry == null", false);
        }
    }

    public static object GetRegistryEntry(string keyName, string subKey, object defaultValue = null, bool masterUser = false)
    {
        MbcsCentral.Registry reg;
        if (GlobalShared.user == null)
            return null;
        reg = MbcsCentral.Registry.load(keyName, subKey, (long)(masterUser ? (long) GlobalShared.MASTERUSER : GlobalShared.user.Account.accountId));
        if (reg == null)
        {
            if (defaultValue == null)
                return null;
            if (defaultValue.GetType() == typeof(System.String))
            {
                string s = "";
                s = defaultValue.ToString();
                return s;
            }
            else
            {
                double d;
                d = double.Parse(defaultValue.ToString());
                return d;
            }
        }
        else
        {
            switch (reg.dataType)
            {
                case (int)MbcsCentral.Registry.DataTypes.Dword:
                    return reg.GetValueDouble();
                    break;
                case (int)MbcsCentral.Registry.DataTypes.String:
                    return reg.GetValueString();
            }
            return reg.value;
        } 
    }

    public static Registry[] GetRegistryEntries(string keyName, string subKey, object defaultValue = null, bool masterUser = false)
    {
        MbcsCentral.Registry[] reg;
        if (GlobalShared.user == null)
            return null;
        reg = MbcsCentral.Registry.loadEntries(keyName, subKey, (long)(masterUser ? (long)GlobalShared.MASTERUSER : GlobalShared.user.Account.accountId));
        return reg;

    }

    /// <summary>
    /// Restore to its last postion and size
    /// </summary>
    /// <param name="frm">The window object to restore</param>
    public static void getForm(Window frm, bool size = true)
    {
        double maxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double maxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        try
        {
            frm.Left = (double)GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Left", 0);
            frm.Top = (double)GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Top", 0);
            if (size)
            { 
                frm.Width = (double)(GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Width", 720));
                frm.Height = (double)(GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "Height", 720));
            }
            GlobalShared.Log.Log((int)LogClass.logType.Info, String.Format("getForm<{0}> Top<{1}> Left<{2}> Width<{3}> Height<{4}>", frm.Name, frm.Top.ToString(), frm.Left.ToString(), frm.Width.ToString(), frm.Height.ToString()),false);
            
            if (frm.Top > maxHeight || frm.Top < 0 || frm.Left > maxWidth || frm.Left < 0)
            {
                if (frm.Top > maxHeight - 1 || frm.Top < 0)
                    frm.Top = 0;
                if (frm.Left > maxWidth - 1 || frm.Left < 0)
                    frm.Left = 0;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, String.Format("   Adjusted to Top<{0}> Left<{1}>", frm.Top.ToString(), frm.Left.ToString()), false);
            }
            
            int s = Convert.ToInt32(GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "State", (int)frm.WindowState));
            switch (s)
            {
                case (int)System.Windows.WindowState.Maximized:
                    frm.WindowState = System.Windows.WindowState.Maximized;
                    break;
                case (int)(int)System.Windows.WindowState.Minimized:
                    frm.WindowState = System.Windows.WindowState.Normal;
                    break;
                case (int)System.Windows.WindowState.Normal:
                    frm.WindowState = System.Windows.WindowState.Normal;
                    break;
            }
            frm.Activate();
            frm.Focus();
        }
        catch (System.Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "getForm: " + ex.Message, false);
            if (frm.Width < 0)
                frm.Width = 720;
            if (frm.Height < 0)
                frm.Height = 720;
            if (frm.Top < 0 || frm.Top > maxHeight - 10)
                frm.Top = 0;
            //if (frm.Left < 0 || frm.Left > maxWidth - 10)
            //    frm.Left = 0;
        }
        try
        {
            double f = Double.Parse(GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Forms\\" + frm.Name, "FontSize", 0).ToString());
            if (f > 0)
            {
                frm.FontSize = f;
            }
        }
        catch { }

    }

    public static void moveNextOnMaxlength(object sender, EventArgs e)
    {
        System.Windows.Controls.TextBox txtBox = (System.Windows.Controls.TextBox)sender;

        // Check text input
        if (txtBox.Text.Trim().Length == txtBox.MaxLength)
        {
            UIElement uie = txtBox;
            uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }

    public static List<string> WrapText(string text, double pixels, string fontFamily,
    float emSize)
    {
        string[] originalLines = text.Split(new string[] { " " },
            StringSplitOptions.None);

        List<string> wrappedLines = new List<string>();

        StringBuilder actualLine = new StringBuilder();
        double actualWidth = 0;

        foreach (var item in originalLines)
        {
            FormattedText formatted = new FormattedText(item,
                CultureInfo.CurrentCulture,
                System.Windows.FlowDirection.LeftToRight,
                new Typeface(fontFamily), emSize, Brushes.Black);

            actualLine.Append(item + " ");
            actualWidth += formatted.Width;

            if (actualWidth > pixels)
            {
                wrappedLines.Add(actualLine.ToString());
                actualLine.Clear();
                actualWidth = 0;
            }
        }

        if (actualLine.Length > 0)
            wrappedLines.Add(actualLine.ToString());

        return wrappedLines;
    }

    public static void FillLastTenantsInMenu()
    {
        try
        {
            var items = Main.mainMenu.Items;
            MenuEntry menuEntry;
            foreach (var item in items)
            {
                if (item is MenuItem && ((MenuItem)item).Name.Equals("winpm"))
                {
                    var subitems = ((MenuItem)item).Items;
                    int i = 0;
                    foreach (var subsubitem in subitems)
                    {
                        if (subsubitem is MenuItem && ((MenuItem)subsubitem).Name.Contains("pm_tenant") && !((MenuItem)subsubitem).Name.Contains("pm_tenants"))
                        {
                            MenuItem mnu = (MenuItem)subsubitem;
                            if (!GlobalShared.TenantNumberList[i].Trim().Equals("") && !GlobalShared.TenantNumberList[i].Trim().Equals("."))
                            {
                                mnu.Header = GlobalShared.TenantNumberList[i];
                                mnu.Visibility = Visibility.Visible;
                            }
                            else
                                mnu.Visibility = Visibility.Collapsed;
                            menuEntry = (MenuEntry)menuTable[mnu.Name];
                            if (menuEntry != null)
                            {
                                menuEntry.Parameter = (string)mnu.Header;
                            }
                            i++;
                        }
                    }
                }
            }
        }
        catch { }
    }

    public static void PrintWindow(Window wndw, System.Printing.PageOrientation orientation = System.Printing.PageOrientation.Portrait)
    {
        PrintDialog printDialog = new PrintDialog();
        double top = wndw.Top;
        double left = wndw.Left;
        if (printDialog.ShowDialog().GetValueOrDefault(false))
        {
            Size origSz = new Size(wndw.ActualWidth, wndw.ActualHeight);
            printDialog.PrintTicket.PageOrientation = orientation;
            printDialog.PrintTicket.PageBorderless = System.Printing.PageBorderless.Borderless;
            //get selected printer capabilities
            System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

            //get scale of the print wrt to screen of WPF visual
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / wndw.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                           wndw.ActualHeight);
                                                                                                 
            //Transform the Visual to scale
            wndw.LayoutTransform = new ScaleTransform(scale, scale);

            //get the size of the printer page
            Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            //update the layout of the visual to the printer page size.
            wndw.Measure(sz);
            wndw.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

            //now print the visual to printer to fit on the one page.
            printDialog.PrintVisual(wndw, wndw.Title);
            wndw.LayoutTransform = new ScaleTransform(1 / scale, 1 / scale);
            wndw.Measure(origSz);
            wndw.Arrange(new Rect(new Point(top, left), origSz));
            wndw.InvalidateVisual();
        }
    }

    public static void PrintWindowBackground(Window wndw, System.Printing.PageOrientation orientation = System.Printing.PageOrientation.Portrait)
    {
        PrintDialogx printDialog = new PrintDialogx();
        //double top = wndw.Top;
        //double left = wndw.Left;
        Window window = new Window();
        window.Title = "Choose Printer";
        window.Height = (double)600;
        window.Width = (double)600;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Show();
        if (printDialog.ShowDialog(window)== true)
        {
            //Size origSz = new Size(wndw.ActualWidth, wndw.ActualHeight);
            printDialog.PrintTicket.PageOrientation = orientation;
            printDialog.PrintTicket.PageBorderless = System.Printing.PageBorderless.Borderless;
            //get selected printer capabilities
            System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

            //get scale of the print wrt to screen of WPF visual
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / wndw.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                           wndw.ActualHeight);

            //Transform the Visual to scale
            wndw.LayoutTransform = new ScaleTransform(scale, scale);

            //get the size of the printer page
            Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            //update the layout of the visual to the printer page size.
            wndw.Measure(sz);
            wndw.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

            //now print the visual to printer to fit on the one page.
            printDialog.PrintWindow(printDialog.RenderTargetBitmapExample( wndw));
            //wndw.LayoutTransform = new ScaleTransform(1 / scale, 1 / scale);
            //wndw.Measure(origSz);
            //wndw.Arrange(new Rect(new Point(top, left), origSz));
            wndw.InvalidateVisual();
        }
        window.Close();
    }


    public static bool VerifyWindocLocation(string WinDocPath)
    {
        if (WinDocPath == null || WinDocPath.Trim().Equals(""))
            return false;
        else
        {
            WinDocPath = WinDocPath.Substring(0, WinDocPath.LastIndexOf("\\"));

            int xtimestimeout = 5;
            while (!Directory.Exists(WinDocPath) && (xtimestimeout > 0))
            {
                System.Threading.Thread.Sleep(3000);
                xtimestimeout -= 1;
            }
            if (!Directory.Exists(WinDocPath))
            {
                MessageBox.Show("Unable to connect to WinDOC at <" + WinDocPath + ">");
                return false;
            }
        }
        return true;
    }

    public static System.Data.OleDb.OleDbConnection GetWinDocConnection(string WindocLocation, out string WindocConnectionString)
    {
        WindocConnectionString = "";
        System.Data.OleDb.OleDbConnection CN;
        WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocLocation.Trim() + "\\windoc.mdb" + ";Mode=Share Deny None";
        if (Main.VerifyWindocLocation(WindocLocation))
        {
            return new System.Data.OleDb.OleDbConnection(WindocConnectionString);
        }
        else
            return null;
    }

    public static long GetDocumentId(int invoiceNumber, string vendorNumber)
    {
        MbcsCentral.WinGL.Control control = MbcsCentral.WinGL.Control.loadOne();
        if (control.Scan)
        {
            string WindocLocation = control.WindocLocation.Trim();
            if (WindocLocation == null || WindocLocation.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Could not find Windoc Database <" + WindocLocation + ">. Invoices will not be scanned.");
            }
            else
            {
                if (!WindocLocation.EndsWith("\\"))
                    WindocLocation += "\\";
                string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocLocation.Trim() + "Windoc.mdb" + ";Mode=Share Deny None";
                OleDbConnection CN = new OleDbConnection(WindocConnectionString);
                MbcsCentral.WinDoc.Documents docs = MbcsCentral.WinDoc.Documents.loadByInvoice(invoiceNumber, vendorNumber, CN);
                if (docs != null)
                    return docs.DocumentId;
                else
                    return 0;
            }
        }
        return 0;

    }

    public static void scanEdit(long docId, string WindocLocation)
    {
        bool r;
        int p = GlobalShared.FindRunningProcess("windoc");
        if (p > 0)
        {
            if (MessageBoxCustom.Show("Windoc is already running. Close it now?.", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!GlobalShared.CloseRunningProcess(p))
                {
                    MessageBoxCustom.Show("Unable to close Windoc.");
                    return;
                }
            }
            else
                return;
        }
        string uniqueID = DateTime.Now.ToString("MMddyyyyhhmm");
        System.Diagnostics.Process exProc = new System.Diagnostics.Process();
        exProc.StartInfo.FileName = WindocLocation.Trim() + "\\windoc.exe";
        exProc.StartInfo.Arguments = String.Format("{0} \"{1}\" \"{2}\"", "wingl-rescan", "rescan", docId.ToString());
        try
        {
            r = exProc.Start();
        }
        catch (System.Exception ex)
        {
            MessageBoxCustom.Show("Unable to load WinDoc: " + ex.Message);
            return;
        }
        exProc.WaitForExit(30000);

    }

    public static int DataGridRowIndex(DependencyObject dep)
    {
        // iteratively traverse the visual tree
        while ((dep != null) && !(dep is DataGridCell))
        {
            dep = VisualTreeHelper.GetParent(dep);
        }

        if (dep == null)
            return -1;

        if (dep is DataGridCell)
        {
            DataGridCell cell = dep as DataGridCell;
            // navigate further up the tree
            while ((dep != null) && !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            DataGridRow row = dep as DataGridRow;
            return FindRowIndex(row);
        }
        return -1;
    }
        public static int FindRowIndex(DataGridRow row)
    {
        DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

        int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);

        return index;
    }

    public static void ShowWindowBack(Window window)
    {
        var x = new WindowInteropHelper(window).Handle;
        SetWindowPos(x, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
    }

    public sealed class Win32Helper
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr handle);
        // Leave function empty
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ShowWindow", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr handle, int nCmd);
        // Leave function empty
    }
}

static class  NativeMethods
{
    [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr OpenIcon(long hWnd);
    [DllImport("user32", EntryPoint = "FindWindowA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern long FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr SetForegroundWindow(int hWnd);

    public const Int32 MONITOR_DEFAULTTOPRIMARY = 0x00000001;
    public const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;

    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);
}
