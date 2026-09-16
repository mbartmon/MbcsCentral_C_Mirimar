using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmLogViewer.xaml
    /// </summary>
    public partial class frmLogViewer : Window
    {
        string LogFolder = "UserConfig\\logs\\";
        string LogExtension = ".log";

        string MainLogFile = "MbcsCentral";
        internal Timer MainTimer;
        private const long MAIN_TIMER_DELAY = 0;
        private const long MAIN_TIMER_INTERVAL = 1000; // 1 second
        long lastMainOffset = 0;
        StreamReader mainReader = null;
        string[] MainFiles;

        // ServiceLog
        string ServiceLogFile = "MbcsCentralService";
        internal Timer ServiceTimer;
        private const long SERVICE_TIMER_DELAY = 0;
        private const long  SERVICE_TIMER_INTERVAL = 1000; // 1 second
        long lastServiceOffset = 0;
        StreamReader serviceReader = null;
        string[] ServiceFiles;

        // BackupLog
        string BackupLogFile = "MbcsCentralDbBackup";
        internal Timer BackupTimer;
        private const long BACKUP_TIMER_DELAY = 0;
        private const long BACKUP_TIMER_INTERVAL = 1000; // 1 second
        long lastBackupOffset = 0;
        StreamReader backupReader = null;
        string[] BackupFiles;

        // BackupLog
        string CleanupLogFile = "MbcsCentralLogCleaner";
        internal Timer CleanupTimer;
        private const long CLEANUP_TIMER_DELAY = 0;
        private const long CLEANUP_TIMER_INTERVAL = 1000;
        StreamReader cleanupReader = null;
        string[] CleanupFiles;

        public frmLogViewer()
        {
            InitializeComponent();
        }

        string thisApp;
        public frmLogViewer(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frmLogViewer_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);


            TimerInfo ti = new TimerInfo() { Interval = MAIN_TIMER_INTERVAL, Delay = MAIN_TIMER_DELAY, List = lstMbcsCentral, Reader = mainReader, Tail = chkMbcsCentral};
            TimerCallback tbc = new TimerCallback(ReadLog);
            MainTimer = new Timer(tbc, ti, Timeout.Infinite, Timeout.Infinite);
            GetLog(LogFolder + MainLogFile + LogExtension, ti, MainTimer);
            MainFiles = GetAllDates(MainLogFile);
            lstMainLogs.ItemsSource = MainFiles;

            ti = new TimerInfo() { Interval = SERVICE_TIMER_INTERVAL, Delay = SERVICE_TIMER_DELAY, List = lstMbcsCentralService, Reader = serviceReader, Tail = chkMbcsCentralService };
            tbc = new TimerCallback(ReadLog);
            ServiceTimer = new Timer(tbc, ti, Timeout.Infinite, Timeout.Infinite);
            GetLog(LogFolder + ServiceLogFile + LogExtension, ti, ServiceTimer);
            ServiceFiles = GetAllDates(ServiceLogFile);
            lstServiceLogs.ItemsSource = ServiceFiles;

            ti = new TimerInfo() { Interval = BACKUP_TIMER_INTERVAL, Delay = BACKUP_TIMER_DELAY, List = lstMbcsCentralBackup, Reader = backupReader, Tail = chkMbcsCentralBackup };
            tbc = new TimerCallback(ReadLog);
            BackupTimer = new Timer(tbc, ti, Timeout.Infinite, Timeout.Infinite);
            GetLog(LogFolder + BackupLogFile + LogExtension, ti, BackupTimer);

            ti = new TimerInfo() { Interval = CLEANUP_TIMER_INTERVAL, Delay = CLEANUP_TIMER_DELAY, List = lstMbcsCentralCleanUp, Reader = cleanupReader, Tail = chkMbcsCentralCleanUp };
            tbc = new TimerCallback(ReadLog);
            CleanupTimer = new Timer(tbc, ti, Timeout.Infinite, Timeout.Infinite);
            GetLog(LogFolder + CleanupLogFile + LogExtension, ti, CleanupTimer);
        }

        private void frmLogViewer_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetLog(string logFilename, TimerInfo ti, System.Threading.Timer LogTimer )
        {
            if (ti.Reader != null)
            {
                try
                {
                    ti.Reader.Close();
                    ti.Reader = null;
                }
                catch { }
            }
            try
            {
                ti.Reader = new StreamReader(new FileStream(logFilename,
                                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                while (!ti.Reader.EndOfStream)
                {
                    string s = ti.Reader.ReadLine();
                    ti.List.Items.Add(s);
                }
                ti.LastMaxOffset = ti.Reader.BaseStream.Length;
                LogTimer.Change(0, ti.Interval);
                ti.List.ScrollIntoView(ti.List.Items[ti.List.Items.Count - 1]);
            }
            catch (Exception ex)
            {
                ti.List.Items.Add(logFilename + " file not found: \n" + ex.Message);
            }

        }

        private void ReadLog(Object stateinfo)
        {
            TimerInfo info = (TimerInfo)stateinfo;

            ListBox list = info.List;
            StreamReader reader = info.Reader;

            //if the file size has not changed, idle
            try
            {
                if (reader.BaseStream.Length == info.LastMaxOffset)
                    return;
            }
            catch (Exception ex)
            {
                return;
            }

            //seek to the last max offset
            reader.BaseStream.Seek(info.LastMaxOffset, SeekOrigin.Begin);

            //read out of the file until the EOF
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                list.Dispatcher.Invoke(new Action(() =>
                {
                    list.Items.Add(line);
                    if (info.Tail.IsChecked.Value)
                        list.ScrollIntoView(list.Items[list.Items.Count - 1]);
                }));
            }
            //update the last max offset
            info.LastMaxOffset = reader.BaseStream.Position;
        }

        private string[] GetAllDates(string logFileName)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(LogFolder,  logFileName + "_*.log");
            }
            catch (IOException ex)
            {
                return null;
            }
            string[] results = new string[files.Length + 1];
            Array.Sort(files);
            Array.Reverse(files);
            results[0] = LogFolder + logFileName + ".log";
            for (int i = 0; i < files.Length; i++)
                results[i + 1] = files[i];
            return results;
        }

        private void LstMainLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            string name = box.Name;
            if (box.SelectedIndex > -1)
            {
                string fileName = box.SelectedItem as string;
                TimerInfo ti;
                switch (name)
                {
                    case "lstMbcsCentral":
                        lstMbcsCentral.Items.Clear();
                        ti = new TimerInfo() { Interval = MAIN_TIMER_INTERVAL, Delay = MAIN_TIMER_DELAY, List = lstMbcsCentral, Reader = mainReader, Tail = chkMbcsCentral };
                        GetLog(fileName, ti, MainTimer);
                        break;
                    case "lstServiceLogs":
                        lstMbcsCentralService.Items.Clear();
                        ti = new TimerInfo() { Interval = SERVICE_TIMER_INTERVAL, Delay = SERVICE_TIMER_DELAY, List = lstMbcsCentralService, Reader = serviceReader, Tail = chkMbcsCentralService };
                        GetLog(fileName, ti, ServiceTimer);
                        break;

                }
            }

        }
    }

    public class TimerInfo
    {
        public TimerInfo()
        {

        }
        public TimerInfo(ListBox list, StreamReader reader)
        {
            List = list;
            Reader = reader;
        }

        public ListBox List { get; set; }
        public StreamReader Reader {get; set;}
        public long Delay { get; set; }
        public long Interval { get; set; }
        public long LastMaxOffset { get; set; }
        public CheckBox Tail { get; set; }
    }
}
