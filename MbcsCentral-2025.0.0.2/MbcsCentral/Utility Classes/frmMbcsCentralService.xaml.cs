using System.ServiceProcess;
using System.Security.Permissions;
using System.Security.Principal;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;

namespace MbcsCentral
{

    public partial class frmMbcsCentralService : Window
    {

        ServiceController MbcsService;
        internal System.Timers.Timer MainTimer = new System.Timers.Timer();
        private const long MAIN_TIMER_DELAY = 0; // startup delay 0 seconds
        private const long MAIN_TIMER_INTERVAL = 5000; // 5 seconds

        internal System.Timers.Timer LogTimer = new System.Timers.Timer();
        private const long LOG_TIMER_DELAY = 10 * 1000; // startup delay 10 seconds
        private const long LOG_TIMER_INTERVAL = 1000; // 1 second
        private bool LogReadCancel = false;
        long lastMaxOffset = 0;
        StreamReader reader = null;
        bool tail = false;
        ArrayList logLines;

        string st;
        RegistryKey regkey;
        string serviceLogFileName = "MbcsCentralService";
        string baseLogName = "UserConfig\\logs\\MbcsCentralService";
        string logRootName = GlobalShared.logFileName.Replace(".log", "");
        //Dictionary<string, string> configs = new Dictionary<string, string>();
        string fldr;
        bool isAdmin = false;
        string appPath = GlobalShared.baseAppPath;
        ArrayList Dates = new ArrayList();
        string logFilename;
        private string fontname = "Courier New";
        private float fontsize = 10;

        public frmMbcsCentralService()
        {
            InitializeComponent();
        }

        string thisApp = "";
        public frmMbcsCentralService(string app)
        {
            thisApp = app;
            InitializeComponent();
        }

        private void frmListener_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try {
                MainTimer.Stop();
            } catch {
            }

            try {
                LogTimer.Stop();
            } catch {
            }
            Main.saveForm(this);

        }


        private void frmListener_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowsIdentity user = null;
            try
            {
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal prin = new WindowsPrincipal(user);
                isAdmin = prin.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            Main.getForm(this);
            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            PrincipalPermission principal = new PrincipalPermission(null, "Administrators");
            try
            {
                principal.Demand();
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }

            MainTimer.Interval = MAIN_TIMER_INTERVAL;
            MbcsService = new ServiceController();
            MbcsService.ServiceName = ServiceParms.MbcsCentralServiceName;
            MbcsService.MachineName = GlobalShared.MachineName;

            MainTimer.Elapsed += getStatus;
            if (ServicesControl.IsServiceInstalled(ServiceParms.MbcsCentralServiceName)) {
                cmdInstall.IsEnabled = false;
                cmdUnInstall.IsEnabled = true;
                MainTimer.Start();
            } else {
                if (!isAdmin)
                    ServiceStatus.Text = "User not an Administrator";
                else
                    ServiceStatus.Text = "Service not installed";
                cmdInstall.IsEnabled = true;
                cmdUnInstall.IsEnabled = false;
                MainTimer.Enabled = false;
            }

            LogTimer.Interval = LOG_TIMER_INTERVAL;
            LogTimer.Elapsed += ReadLog;
            /*
            DateTime curDate = DateTime.Now.Date;
            DateTime thisDate;
            Dates.Add(new ListItem("Today", 0, curDate));
            for (int i = 1; i < 29; i++)
            {
                thisDate = curDate.AddDays(-i);
                Dates.Add(new ListItem(curDate.AddDays(-i).ToShortDateString(), 0, thisDate));
            }
            */
            lstDates.ItemsSource = GetDates(serviceLogFileName, appPath + "UserConfig\\Logs", true);
            lstDates.SelectedIndex = 0;
        }

        private ArrayList GetDates(string baseFileName, string folder, bool AddToday = false)
        {
            string[] files = null;
            string[] parts = null;
            DateTime date;
            string[] format = { "yyyyMMdd" };
            ArrayList list = new ArrayList();

            try
            {
                files = Directory.GetFiles(folder, baseFileName + "_*.log");
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "      No [" + baseFileName + "] log files: " + ex.Message, false);
            }
            if (AddToday)
                list.Add(new ListItem("Today", 0, DateTime.Now.Date));
            foreach (string file in files)
            {
                parts = file.Split('_');
                if (parts != null & parts.Length > 1)
                {
                    parts = parts[parts.Length - 1].Split('.');
                    if (parts != null && parts.Length > 1)
                    {

                        if (DateTime.TryParseExact(parts[0],
                            format,
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out date))
                            if (date > GlobalShared.NULL_DATE)
                            {
                                list.Add(new ListItem(date.ToShortDateString(), 0, date));
                            }
                    }
                }
            }

            return list;

        }
        public void getStatus(Object source, System.Timers.ElapsedEventArgs e)
        {
            MbcsService.Refresh();
            try {
                st = MbcsService.Status.ToString();
            } catch (System.Exception ex) {
                st = "Service not found";
            }
            ServiceStatus.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateStatus));

        }

        public void updateStatus()
        {
            ServiceStatus.Text = st;
        }


        private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*
			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ListenSleepTime), mailInterval * 60 * 1000);
			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadOutlook), readOutlook);
			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadSmtp), readSmtp);
			StreamWriter sw = new StreamWriter(fldr, false);
			foreach (KeyValuePair<string, string> ent in configs) {
				sw.WriteLine(ent.Key + "," + ent.Value);
			}
			sw.Close();

			MessageBoxCustom.Show("Values have been saved but will not take effect util service is restarted.");
            */

        }

        private void cmdStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                MessageBoxCustom.Show("ReStart application 'As Adimistrator' to perform this operation.");
                return;
            }
            MbcsService.Refresh();
            if (MbcsService.Status == ServiceControllerStatus.Stopped)
            {
                try
                {
                    ServiceStatus.Text = "Starting";
                    MbcsService.Start();
                    MbcsService.WaitForStatus(ServiceControllerStatus.Running);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBoxCustom.Show("Could not start the service: " + ex.Message);
                    if (ex.InnerException != null)
                        MessageBoxCustom.Show(ex.InnerException.Message);
                }

            }
        }

        private void cmdStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                MessageBoxCustom.Show("ReStart application 'As Adimistrator' to perform this operation.");
                return;
            }
            MbcsService.Refresh();
            if (MbcsService.Status == ServiceControllerStatus.Running) {
                ServiceStatus.Text = "Stopping";
                MbcsService.Stop();
            }

        }


        private void cmdRESTART_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                MessageBoxCustom.Show("ReStart application 'As Adimistrator' to perform this operation.");
                return;
            }
            MbcsService.Refresh();
            if (MbcsService.Status == ServiceControllerStatus.Running) {
                ServiceStatus.Text = "Stopping";
                MbcsService.Stop();
                try {
                    MbcsService.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 20));
                } catch (System.ServiceProcess.TimeoutException ex) {
                    MessageBoxCustom.Show("Service did not stop in a timely fashion");
                    return;
                }
            }
            MbcsService.Refresh();
            if (MbcsService.Status == ServiceControllerStatus.Stopped) {
                MbcsService.Start();
            }

        }


        private void cmdViewLog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string dateString = "";
            if (lstDates.SelectedIndex == 0)
                dateString = "";
            else if (lstDates.SelectedIndex > 0)
            {
                ListItem item = (ListItem)lstDates.SelectedItem;
                dateString = ((DateTime)item.Value).Date.ToString("_yyyyMMdd");
            }
            logFilename = appPath + baseLogName + dateString + ".log";
            if (reader != null)
            {
                try
                {
                    reader.Close();
                    reader = null;
                }
                catch { }
            }
            try {
                reader = new StreamReader(new FileStream(logFilename,
                                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                //StreamReader sr = new StreamReader(logFilename);
                /*
				LogViewer.Text = "";
				LogViewer.Text = sr.ReadToEnd();
                */
                if (logLines != null && logLines.Count > 0)
                {
                    logLines.Clear();
                }
                else if (logLines == null)
                    logLines = new ArrayList();
                //private Font mainFont;
                Report rpt = new Report(logRootName + " Log", fontname, fontsize, (Single)2100) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)40, BottomOffset = (Single)(45) };
                rpt.PageOrientation = (int)Report.Orientation.Landscape;
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    LogViewer.Items.Add(s);
                    //rpt.PrintPageLine(s);
                }
                lastMaxOffset = reader.BaseStream.Length;
                //sr.Close();
                //rpt.DisplayDocument(true);
                //LogViewer.Document = rpt.Document;
                LogTimer.Start();
                LogViewer.ScrollIntoView(LogViewer.Items[LogViewer.Items.Count - 1]);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Log file not found: " + ex.Message);
            }
            //frmLogViewer frm = new frmLogViewer { logFile = filename };
            //frm.Show();

        }

        private void ReadLog(Object source, System.Timers.ElapsedEventArgs e)
        {

            //if the file size has not changed, idle
            if (reader.BaseStream.Length == lastMaxOffset)
                return;

            //seek to the last max offset
            reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

            //read out of the file until the EOF
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                LogViewer.Dispatcher.Invoke(new Action(() => {
                    LogViewer.Items.Add(line);
                    if (tail)
                        LogViewer.ScrollIntoView(LogViewer.Items[LogViewer.Items.Count - 1]); }));
            }
            //update the last max offset
            lastMaxOffset = reader.BaseStream.Position;
        }

        private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();

        }


        private void cmdRefreshLog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            cmdViewLog_Click(sender, e);

        }


        private void cmdClearLog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*
			File.Delete(GlobalShared.appPath + serviceLogFileName);
			LogViewer.Document = null;
			cmdViewLog_Click(sender, e);
            */

        }


        private void cmdInstall_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                MessageBoxCustom.Show("ReStart application 'As Adimistrator' to perform this operation.");
                return;
            }
            frmWait frm = new frmWait() { WindowStartupLocation = WindowStartupLocation.CenterOwner };
            frm.Show();
            ServicesControl.InstallService(appPath, ServiceParms.MbcsCentralServiceName, "LocalSystem");
            frm.Close();
            frm = null;
            if (ServicesControl.IsServiceInstalled(ServiceParms.MbcsCentralServiceName)) {
                MessageBoxCustom.Show("Service successfully installed.");
                cmdInstall.IsEnabled = false;
                cmdUnInstall.IsEnabled = true;
                MbcsService = null;
                MbcsService = new ServiceController();
                MbcsService.ServiceName = ServiceParms.MbcsCentralServiceName;
                if (!MainTimer.Enabled) {
                    MainTimer.Enabled = true;
                    MainTimer.Start();
                }
            } else {
                MessageBoxCustom.Show(ServiceParms.MbcsCentralServiceName + " - Service NOT installed.");
            }

        }


        private void cmdUnInstall_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isAdmin)
            {
                MessageBoxCustom.Show("ReStart application 'As Adimistrator' to perform this operation.");
                return;
            }
            frmWait frm = new frmWait() { WindowStartupLocation = WindowStartupLocation.CenterOwner };
            frm.Show();
            ServicesControl.UninstallService(ServiceParms.MbcsCentralServiceName);
            frm.Close();
            if (ServicesControl.IsServiceInstalled(ServiceParms.MbcsCentralServiceName)) {
                MessageBoxCustom.Show("Service NOT uninstalled.");
            } else {
                MessageBoxCustom.Show("Service successfully uninstalled.");
                cmdInstall.IsEnabled = true;
                cmdUnInstall.IsEnabled = false;
                ServiceStatus.Text = "Service not installed";
                MainTimer.Stop();
                MainTimer.Enabled = false;
            }
        }

        private void lstDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmdViewLog_Click(sender, new RoutedEventArgs());
        }

        private void chkTail_Checked(object sender, RoutedEventArgs e)
        {
            tail = chkTail.IsChecked.Value;
        }
    }

    public class LogList : INotifyPropertyChanged
    {
        private ArrayList logList;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public LogList()
        {
            logList = new ArrayList();
        }

        public void Add(string item)
        {
            logList.Add(item);
            OnPropertyChanged("List");
        }

        public ArrayList List
        {
            get { return logList; }
        }
    }
}
