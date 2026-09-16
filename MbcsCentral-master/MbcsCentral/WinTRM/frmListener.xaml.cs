using System.ServiceProcess;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

using WinTRM.ServicesControl;
namespace WinTRM
{

	public class frmListener
	{

		ServiceController listener;
		internal System.Timers.Timer myTimer = new System.Timers.Timer();
		internal System.Timers.Timer logTimer = new System.Timers.Timer();
		string st;
		RegistryKey regkey;
		long mailInterval;
		bool readOutlook = false;
		bool readSmtp = true;
		string serviceLogFileName = "PopListenerLog.txt";
		Dictionary<string, string> configs = new Dictionary<string, string>();
		string fldr;

		private void frmListener_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try {
				myTimer.Stop();
			} catch {
			}

			try {
				logTimer.Stop();
			} catch {
			}
			Utils.saveForm(this);

		}


		private void frmListener_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Utils.getForm(this);
			myTimer.Interval = 5000;
			fldr = string.Format("{0}\\{1}\\{2}\\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config");
			listener = new ServiceController();
			listener.ServiceName = GlobalShared.listenerService;
			if (IsServiceInstalled(GlobalShared.listenerService)) {
				cmdInstall.IsEnabled = false;
				cmdUnInstall.IsEnabled = true;
				myTimer.Start();
			} else {
				ServiceStatus.Text = "Service not installed";
				cmdInstall.IsEnabled = true;
				cmdUnInstall.IsEnabled = false;
				myTimer.Enabled = false;
			}
			myTimer.Elapsed += getStatus;

			StreamReader fs = default(StreamReader);
			string[] s = null;
			try {
				fs = new StreamReader(fldr);
				if ((fs != null)) {
					while (!fs.EndOfStream) {
						DictUtils.addItem(configs, fs.ReadLine());
					}
				}
			} catch (FileNotFoundException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, fldr + " not found");
			} catch (IOException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error processing " + fldr + ": " + ex.Message);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error processing " + fldr + ": " + ex.Message);
			}
			if ((configs != null)) {
				DictUtils.getItem(configs, GlobalShared.smtpConfigs(smtpItem.ListenSleepTime), ref mailInterval);
				mailInterval = mailInterval / 1000 / 60;
				DictUtils.getItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadOutlook), ref readOutlook);
				DictUtils.getItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadSmtp), ref readSmtp);
			}
			MailRetrieval.Text = mailInterval;
			chkSMTP.IsChecked = readSmtp;
			chkOutlook.IsChecked = readOutlook;

		}


		public void getStatus()
		{
			listener.Refresh();
			try {
				st = listener.Status.ToString;
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
			mailInterval = Conversion.Val(MailRetrieval.Text);
			readOutlook = chkOutlook.IsChecked;
			readSmtp = chkSMTP.IsChecked;

			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ListenSleepTime), mailInterval * 60 * 1000);
			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadOutlook), readOutlook);
			DictUtils.setItem(configs, GlobalShared.smtpConfigs(smtpItem.ReadSmtp), readSmtp);
			StreamWriter sw = new StreamWriter(fldr, false);
			foreach (KeyValuePair<string, string> ent in configs) {
				sw.WriteLine(ent.Key + "," + ent.Value);
			}
			sw.Close();

			Interaction.MsgBox("Values have been saved but will not take effect util service is restarted.");

		}


		private void cmdStart_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			listener.Refresh();
			if (listener.Status == ServiceControllerStatus.Stopped) {
				ServiceStatus.Text = "Starting";
				listener.Start();
			}

		}


		private void cmdStop_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			listener.Refresh();
			if (listener.Status == ServiceControllerStatus.Running) {
				ServiceStatus.Text = "Stopping";
				listener.Stop();
			}

		}


		private void cmdRESTART_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			listener.Refresh();
			if (listener.Status == ServiceControllerStatus.Running) {
				ServiceStatus.Text = "Stopping";
				listener.Stop();
				try {
					listener.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 20));
				} catch (TimeoutException ex) {
					Interaction.MsgBox("Service did not stop in a timely fashion");
					return;
				}
			}
			listener.Refresh();
			if (listener.Status == ServiceControllerStatus.Stopped) {
				listener.Start();
			}

		}


		private void cmdViewLog_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string filename = GlobalShared.appPath + serviceLogFileName;
			try {
				StreamReader sr = new StreamReader(filename);
				LogViewer.Text = "";
				LogViewer.Text = sr.ReadToEnd;
				sr.Close();
			} catch {
			}
			frmLogViewer frm = new frmLogViewer { logFile = filename };
			frm.Show();

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
			File.Delete(GlobalShared.appPath + serviceLogFileName);
			LogViewer.Text = "";
			cmdViewLog_Click(sender, e);

		}


		private void cmdInstall_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmWait frm = new frmWait();
			frm.Show();
			frm.BringToFront();
			frm.Refresh();
			InstallService(string.Format("{0}\\{1}\\{2}\\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm"), GlobalShared.listenerService, "LocalSystem");
			frm.Close();
			frm = null;
			if (IsServiceInstalled(GlobalShared.listenerService)) {
				Interaction.MsgBox("Service successfully installed.");
				cmdInstall.IsEnabled = false;
				cmdUnInstall.IsEnabled = true;
				listener = null;
				listener = new ServiceController();
				listener.ServiceName = GlobalShared.listenerService;
				if (!myTimer.Enabled) {
					myTimer.Enabled = true;
					myTimer.Start();
				}
			} else {
				Interaction.MsgBox(GlobalShared.listenerService + " - Service NOT installed.");
			}

		}


		private void cmdUnInstall_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmWait frm = new frmWait();
			frm.Show();
			frm.BringToFront();
			frm.Refresh();
			UninstallService(GlobalShared.listenerService);
			frm.Close();
			if (IsServiceInstalled(GlobalShared.listenerService)) {
				Interaction.MsgBox("Service NOT uninstalled.");
			} else {
				Interaction.MsgBox("Service successfully uninstalled.");
				cmdInstall.IsEnabled = true;
				cmdUnInstall.IsEnabled = false;
				ServiceStatus.Text = "Service not installed";
				myTimer.Stop();
				myTimer.Enabled = false;
			}
		}
		public frmListener()
		{
			Loaded += frmListener_Loaded;
			Closing += frmListener_Closing;
		}
	}
}
