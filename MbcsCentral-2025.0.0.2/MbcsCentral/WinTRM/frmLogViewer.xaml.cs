using System;
using System.IO;
using System.Xaml;
using System.Windows.Xps.Packaging;
using Microsoft.VisualBasic;
using System.Security.Permissions;
namespace WinTRM
{

	public class frmLogViewer
	{

		string _logFile = GlobalShared.appPath + GlobalShared.logFileName;
		FileSystemWatcher fileWatcher = new FileSystemWatcher();

		string log;

		private void frmLog_Closed(object sender, System.EventArgs e)
		{
			Utils.saveForm(this);

		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

		private void frmLog_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Drawing.Rectangle rect = Utils.getForm(this);
			this.Left = rect.Left;
			this.Top = rect.Top;
			this.Width = rect.Width;
			this.Height = rect.Height;

			this.Title = "Log Viewer <" + _logFile + ">";
			onChanged(null, null);
			fileWatcher.Path = System.IO.Path.GetDirectoryName(_logFile);
			fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fileWatcher.Filter = System.IO.Path.GetFileName(_logFile);
			fileWatcher.Changed += onChanged;
			fileWatcher.EnableRaisingEvents = true;

		}


		private void textChanged()
		{
			txtLog.Text = log;
			txtLog.ScrollToEnd();

		}

		private void onChanged(object source, FileSystemEventArgs e)
		{
			try {
				StreamReader sr = new StreamReader(_logFile);
				log = sr.ReadToEnd;
				sr.Close();

				txtLog.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(textChanged));
			} catch {
			}
		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			log = null;
			txtLog.Text = "";
			this.Close();

		}

		public string logFile {
			get { return _logFile; }
			set { _logFile = value; }
		}


		private void cmdClear_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			File.Delete(_logFile);
			txtLog.Text = "";

		}
		public frmLogViewer()
		{
			Loaded += frmLog_Loaded;
			Closed += frmLog_Closed;
		}
	}
}
