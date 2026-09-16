using System.ServiceProcess;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
namespace MbcsCentral
{

	public partial class frmServices
	{

		ServiceController listener;
		ServiceController creation;
		ServiceController delivery;
		internal System.Timers.Timer myListenerTimer = new System.Timers.Timer();
		internal System.Timers.Timer logListenerTimer = new System.Timers.Timer();
		internal System.Timers.Timer myCreationTimer = new System.Timers.Timer();
		internal System.Timers.Timer logCreationTimer = new System.Timers.Timer();
		internal System.Timers.Timer myDeliveryTimer = new System.Timers.Timer();
		internal System.Timers.Timer logDeliveryTimer = new System.Timers.Timer();
		string stListener;
		string stCreation;
		string stDelivery;
		RegistryKey regkey;
		long mailInterval;
		bool readOutlook = false;
		bool readSmtp = true;
		string serviceLogFileName = "PopListenerLog.txt";
		Dictionary<string, string> configs = new Dictionary<string, string>();
		string fldr;
		FileSystemWatcher fileWatcher = new FileSystemWatcher();
		System.Threading.Timer dbTimer;
		long DB_TIMER_INTERVAL = 30000;
		string _logfile = "*.log";
		string logString = "";
		string logOpt = "";

		List<string> servers;
		public enum JobOpts : int
		{
			All,
			Ready,
			Today,
			Tomorrow
		}

		int JobOpt = 0;
		System.Windows.Data.CollectionViewSource JobDataViewSource;

		System.Windows.Data.CollectionViewSource TriggerDataViewSource;

		private void frmServices_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Utils.saveFormW(this);

			try {
				dbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
				dbTimer.Dispose();
				dbTimer = null;
			} catch {
			}
			try {
				myCreationTimer.Stop();
				myCreationTimer = null;
			} catch {
			}
			try {
				myDeliveryTimer.Stop();
				myDeliveryTimer = null;
			} catch {
			}
			try {
				myListenerTimer.Stop();
				myListenerTimer = null;
			} catch {
			}
			//jobSchedFactory = Nothing
			//jobRemoteSched = Nothing

		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void frmServices_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			fldr = string.Format("{0}\\{1}\\{2}\\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config");

			//ImpersonationUtil.Impersonate("wintrm", "Vietnam65", ".")
			// Configure Creation Service Management
			JobDataViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("JobDataViewSource");
			TriggerDataViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("TriggerDataViewSource");

			configQuartz();

			servers = GetNames();

			lstServers.ItemsSource = servers;

			myCreationTimer.Interval = 10000;
			creation = new ServiceController();
			creation.MachineName = GlobalShared.jobServer;
			creation.ServiceName = GlobalShared.jobCreationService;
			cmdStartCreation.Tag = GlobalShared.jobCreationService;
			cmdStopCreation.Tag = GlobalShared.jobCreationService;
			cmdRESTARTCreation.Tag = GlobalShared.jobCreationService;
			myCreationTimer.Elapsed += getCreationStatus;
			if (IsServiceInstalled(GlobalShared.jobCreationService)) {
				cmdInstallCreation.IsEnabled = false;
				cmdUnInstallCreation.IsEnabled = true;
				myCreationTimer.Start();
			} else {
				CreationStatus.Text = "Service not installed";
				cmdInstallCreation.IsEnabled = true;
				cmdUnInstallCreation.IsEnabled = false;
				myCreationTimer.Enabled = false;
			}

			// Configure Listener Service Management
			myListenerTimer.Interval = 10000;
			listener = new ServiceController();
			listener.ServiceName = GlobalShared.listenerService;
			cmdStartListener.Tag = GlobalShared.listenerService;
			cmdStopListener.Tag = GlobalShared.listenerService;
			cmdRESTARTListener.Tag = GlobalShared.listenerService;
			myListenerTimer.Elapsed += getListenerStatus;
			if (IsServiceInstalled(GlobalShared.listenerService)) {
				cmdInstallListener.IsEnabled = false;
				cmdUnInstallListener.IsEnabled = true;
				myListenerTimer.Start();
			} else {
				ListenerStatus.Text = "Service not installed";
				cmdInstallListener.IsEnabled = true;
				cmdUnInstallListener.IsEnabled = false;
				myListenerTimer.Enabled = false;
			}

			txtServerName.Text = GlobalShared.jobServer;

			//displayJobData()
			//displayJobMaster()
			//displayDeliverySchedules()

			fileWatcher.Path = System.IO.Path.GetDirectoryName(GlobalShared.appPath);
			fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fileWatcher.Filter = System.IO.Path.GetFileName(_logfile);
			fileWatcher.Changed += onChanged;
			fileWatcher.EnableRaisingEvents = true;

			System.Threading.TimerCallback dbTimerDelegate = new System.Threading.TimerCallback(getDisplayData);
			dbTimer = new System.Threading.Timer(dbTimerDelegate);
			dbTimer.Change(DB_TIMER_INTERVAL, DB_TIMER_INTERVAL);

		}


		private void configQuartz()
		{
			GlobalShared.Log.Log(LogClass.logType.Config, "SERVICES - Loading Quartz configuration...");
			for (int i = 0; i <= QuartzConfig.Properties.Count - 1; i++) {
				QuartzConfig.Properties.Item(QuartzConfig.Properties.GetKey(i)) = QuartzConfig.Properties.Get(i).Replace("<machineName>", GlobalShared.jobServer);
				GlobalShared.Log.Log(LogClass.logType.Config, Constants.vbTab + QuartzConfig.Properties.GetKey(i) + " : " + QuartzConfig.Properties.Get(i));
			}
			string[] s = {
				"quartz.scheduler.proxy",
				"true"
			};
			QuartzConfig.Add = s;
			s = {
				"quartz.scheduler.proxy.address",
				"tcp://" + GlobalShared.jobServer + ":555/QuartzScheduler"
			};
			QuartzConfig.Add = s;

		}

		private void onChanged(object source, FileSystemEventArgs e)
		{
			try {
				StreamReader sr = new StreamReader(e.FullPath);
				logString = sr.ReadToEnd;
				sr.Close();

				switch (e.Name) {
					case GlobalShared.jobCreationLogName:
						logOpt = GlobalShared.jobCreationLogName;
						txtJobLogViewer.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(textChanged));
						break;
					case GlobalShared.messageDeliveryLogName:
						logOpt = GlobalShared.messageDeliveryLogName;
						txtDeliveryLogViewer.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(textChanged));
						break;
				}
			} catch (System.IO.IOException ex) {
			}

		}


		private void textChanged()
		{
			switch (logOpt) {
				case GlobalShared.jobCreationLogName:
					txtJobLogViewer.Text = logString;
					txtJobLogViewer.ScrollToEnd();
					scrlJobLog.UpdateLayout();
					scrlJobLog.ScrollToVerticalOffset(txtJobLogViewer.ActualHeight);

					break;
				case GlobalShared.messageDeliveryLogName:
					txtDeliveryLogViewer.Text = logString;
					txtDeliveryLogViewer.ScrollToEnd();
					break;
			}

		}


		public void displayJobData()
		{
			//Load data by setting the CollectionViewSource.Source property:
			long idx = 0;

			if ((JobDataDataGrid == null)) {
				return;
			}
			idx = JobDataDataGrid.SelectedIndex;

			JobData[] jd = getJobData();
			JobDataViewSource.Source = null;
			if ((jd != null) && jd.Length > 0 && (jd(0) != null)) {
				try {
					JobDataViewSource.Source = jd;
				} catch (System.Exception ex) {
				}
			}
			JobDataDataGrid.SelectedIndex = idx;

		}


		public void displayJobMaster()
		{
			System.Windows.Data.CollectionViewSource JobMasterViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("JobMasterViewSource");
			//Load data by setting the CollectionViewSource.Source property:

			long idx = 0;

			idx = JobMasterDataGrid.SelectedIndex;

			jobs = JobMaster.loadDetail;
			JobMasterViewSource.Source = null;
			if ((jobs != null) && jobs.Length > 0) {
				JobMasterViewSource.Source = jobs;
			}
			JobMasterDataGrid.SelectedIndex = idx;

		}

		public void displayDeliverySchedules()
		{
			System.Windows.Data.CollectionViewSource MessageScheduleViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("MessageScheduleViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			long idx = 0;

			idx = MessageScheduleDataGrid.SelectedIndex;

			MessageSchedule[] scheds = null;
			scheds = MessageSchedule.loadDetail;
			MessageScheduleViewSource.Source = null;
			if ((scheds != null) && scheds.Length > 0) {
				MessageScheduleViewSource.Source = scheds;
			}
			MessageScheduleDataGrid.SelectedIndex = idx;

		}

		public JobData[] getJobData()
		{

			if ((GlobalShared.jobRemoteSched == null)) {
				try {
					GlobalShared.jobSchedFactory = new StdSchedulerFactory(QuartzConfig.Properties);
				} catch (Quartz.Impl.AdoJobStore.InvalidConfigurationException ex) {
					Interaction.MsgBox("QUARTZ: " + ex.Message);
					return null;
				} catch (Quartz.Impl.AdoJobStore.LockException ex) {
					Interaction.MsgBox("QUARTZ: " + ex.Message);
					return null;
				} catch (Quartz.Impl.AdoJobStore.NoSuchDelegateException ex) {
					Interaction.MsgBox("QUARTZ: " + ex.Message);
					return null;
				} catch (Quartz.SchedulerConfigException ex) {
					Interaction.MsgBox("QUARTZ: " + ex.Message);
					return null;
				} catch (Quartz.SchedulerException ex) {
					Interaction.MsgBox("QUARTZ: " + ex.Message);
					return null;
				} catch (System.Exception ex) {
					Interaction.MsgBox("SYSTEM: " + ex.Message);
					return null;
				}
				try {
					GlobalShared.jobRemoteSched = GlobalShared.jobSchedFactory.GetScheduler();
				} catch (Quartz.SchedulerConfigException ex) {
					Interaction.MsgBox("The Job Creation Service may not be running. " + ex.Message);
					return null;
				} catch (Quartz.SchedulerException ex) {
					Interaction.MsgBox("The Job Creation Service may not be running. " + ex.Message);
					return null;
				} catch (System.Exception ex) {
					Interaction.MsgBox("The Job Creation Service may not be running. " + ex.Message);
					return null;
				}
			}

			IList JobGroups = GlobalShared.jobRemoteSched.GetJobGroupNames();
			string name = null;
			JobDetailImpl jd = default(JobDetailImpl);
			JobDataMap jobMap = default(JobDataMap);
			MessageSchedule msgSched = default(MessageSchedule);
			bool rr = false;
			JobData jDataOne = default(JobData);
			TriggerData[] jTriggers = null;
			bool use = false;
			JobData[] jData = null;
			int c = 0;
			for (int i = 0; i <= JobGroups.Count - 1; i++) {
				name = Convert.ToString(JobGroups.Item(i));
				Quartz.Collection.ISet<JobKey> keys = GlobalShared.jobRemoteSched.GetJobKeys(GroupMatcher<Quartz.JobKey>.GroupEquals(name));
				foreach (JobKey Key in keys) {
					try {
						jd = GlobalShared.jobRemoteSched.GetJobDetail(Key);
						jobMap = jd.JobDataMap;
						msgSched = MessageSchedule.load(jobMap(GlobalShared.JOB_SCHEDULE));
						if ((msgSched != null)) {
							rr = msgSched.ReturnReceipt;
						} else {
							rr = false;
						}
						jDataOne = new JobData(GlobalShared.jobRemoteSched.SchedulerName, jd.Name, jd.Group, jd.Description, "", jd.Durable, jd.ConcurrentExectionDisallowed, false, jd.RequestsRecovery, jd.JobDataMap,
						null, jobMap(GlobalShared.JOB_ID), jobMap(GlobalShared.JOB_ACCOUNTID), jobMap(GlobalShared.JOB_RECIPIENTLIST), jobMap(GlobalShared.JOB_FROM), jobMap(GlobalShared.JOB_SUBJECT), jobMap(GlobalShared.JOB_BODY), rr);
						jTriggers = jDataOne.Triggers;
						use = false;
						switch (JobOpt) {
							case JobOpts.All:
								use = true;
								break;
							case JobOpts.Ready:
								if ((jTriggers != null)) {
									for (int j = 0; j <= jTriggers.Length - 1; j++) {
										if (jTriggers(j).NextFireTime.Date == Now.Date) {
											use = true;
											break; // TODO: might not be correct. Was : Exit For
										}
									}
								}
								break;
							case JobOpts.Today:
								if ((jTriggers != null)) {
									for (int j = 0; j <= jTriggers.Length - 1; j++) {
										if (jTriggers(j).PrevFireTime.Date == Now.Date) {
											use = true;
											break; // TODO: might not be correct. Was : Exit For
										}
									}
								}
								break;
							case JobOpts.Tomorrow:
								if ((jTriggers != null)) {
									for (int j = 0; j <= jTriggers.Length - 1; j++) {
										if (jTriggers(j).NextFireTime.Date == Now.Date.AddDays(1)) {
											use = true;
											break; // TODO: might not be correct. Was : Exit For
										}
									}
								}
								break;
						}
						if (use) {
							Array.Resize(ref jData, c + 1);
							jData(c) = jDataOne;
							c += 1;
						}
					} catch (Quartz.SchedulerException ex) {
						Interaction.MsgBox(ex.Message);
					} catch (System.Exception ex) {
						Interaction.MsgBox(ex.Message);
					}
				}
			}

			return jData;

		}


		public void getDisplayData()
		{
			dbTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
			JobDataDataGrid.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateDisplayData));

		}


		public void updateDisplayData()
		{
			displayJobData();
			displayJobMaster();
			displayDeliverySchedules();
			dbTimer.Change(DB_TIMER_INTERVAL, DB_TIMER_INTERVAL);

		}


		public void getCreationStatus()
		{
			creation.Refresh();
			try {
				stCreation = creation.Status.ToString;
			} catch (System.Exception ex) {
				stCreation = "Service not found";
			}
			CreationStatus.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateCreationStatus));

		}

		public void updateCreationStatus()
		{
			CreationStatus.Text = stCreation;
		}


		public void getDeliveryStatus()
		{
			delivery.Refresh();
			try {
				stDelivery = delivery.Status.ToString;
			} catch (System.Exception ex) {
				stDelivery = "Service not found";
			}
			DeliveryStatus.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateDeliveryStatus));

		}

		public void updateDeliveryStatus()
		{
			DeliveryStatus.Text = stDelivery;
		}


		public void getListenerStatus()
		{
			listener.Refresh();
			try {
				stListener = listener.Status.ToString;
			} catch (System.Exception ex) {
				stListener = "Service not found";
			}
			ListenerStatus.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(updateListenerStatus));

		}

		public void updateListenerStatus()
		{
			ListenerStatus.Text = stListener;
		}


		private void cmdStart_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			string svcName = btn.Tag;
			switch (svcName) {
				case GlobalShared.jobCreationService:
					creation.Refresh();
					if (creation.Status == ServiceControllerStatus.Stopped) {
						CreationStatus.Text = "Starting";
						creation.Start();
					}
					break;
				case GlobalShared.deliveryService:
					delivery.Refresh();
					if (delivery.Status == ServiceControllerStatus.Stopped) {
						DeliveryStatus.Text = "Starting";
						delivery.Start();
					}
					break;
				case GlobalShared.listenerService:
					listener.Refresh();
					if (listener.Status == ServiceControllerStatus.Stopped) {
						ListenerStatus.Text = "Starting";
						listener.Start();
					}
					break;
			}

		}


		private void cmdStop_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			string svcName = btn.Tag;
			switch (svcName) {
				case GlobalShared.jobCreationService:
					creation.Refresh();
					if (creation.Status == ServiceControllerStatus.Running) {
						CreationStatus.Text = "Stopping";
						creation.Stop();
					}
					break;
				case GlobalShared.deliveryService:
					delivery.Refresh();
					if (delivery.Status == ServiceControllerStatus.Running) {
						DeliveryStatus.Text = "Stopping";
						delivery.Stop();
					}
					break;
				case GlobalShared.listenerService:
					listener.Refresh();
					if (listener.Status == ServiceControllerStatus.Running) {
						ListenerStatus.Text = "Stopping";
						listener.Stop();
					}
					break;
			}
		}


		private void cmdRESTART_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			string svcName = btn.Tag;
			switch (svcName) {
				case GlobalShared.jobCreationService:
					creation.Refresh();
					if (creation.Status == ServiceControllerStatus.Running) {
						CreationStatus.Text = "Stopping";
						creation.Stop();
						try {
							creation.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 20));
						} catch (TimeoutException ex) {
							Interaction.MsgBox("Service did not stop in a timely fashion");
							return;
						}
					}
					creation.Refresh();
					if (creation.Status == ServiceControllerStatus.Stopped) {
						creation.Start();
					}
					break;
				case GlobalShared.deliveryService:
					delivery.Refresh();
					if (delivery.Status == ServiceControllerStatus.Running) {
						DeliveryStatus.Text = "Stopping";
						delivery.Stop();
						try {
							delivery.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 20));
						} catch (TimeoutException ex) {
							Interaction.MsgBox("Service did not stop in a timely fashion");
							return;
						}
					}
					delivery.Refresh();
					if (delivery.Status == ServiceControllerStatus.Stopped) {
						delivery.Start();
					}
					break;
				case GlobalShared.listenerService:
					listener.Refresh();
					if (listener.Status == ServiceControllerStatus.Running) {
						ListenerStatus.Text = "Stopping";
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
					break;
			}

		}


		private void viewLog(string filename, TextBox logViewer, ScrollViewer scrlViewer)
		{
			try {
				StreamReader sr = new StreamReader(filename);
				logViewer.Text = "";
				logViewer.Text = sr.ReadToEnd;
				sr.Close();
				scrlViewer.UpdateLayout();
				scrlViewer.ScrollToVerticalOffset(logViewer.ActualHeight);
			} catch {
			}

		}


		private void Button_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			viewLog(GlobalShared.appPath + GlobalShared.jobCreationLogName, txtJobLogViewer, scrlJobLog);

		}


		private void Button_Click_DeliveryLog(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			viewLog(GlobalShared.appPath + GlobalShared.messageDeliveryLogName, txtDeliveryLogViewer, scrlDeliveryLog);

		}

		private void tabItemJobCreation_GotFocus(System.Object sender, System.Windows.RoutedEventArgs e)
		{
		}


		private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void JobDataDataGrid_SelectionChanged(System.Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			TriggerDataViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("TriggerDataViewSource");
			JobData itm = JobDataDataGrid.SelectedItem;
			if ((itm != null)) {
				TriggerData[] triggers = itm.Triggers;
				TriggerDataViewSource.Source = triggers;
			}

		}


		private void BodyNameColumnClick(object sender, RoutedEventArgs e)
		{
			JobData rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			frmContent frm = new frmContent();
			frm.setContentId(rowdata.Body);
			frm.setAllowEdit(false);
			frm.Show();

		}


		private void MessageDeliveryColumnClick(object sender, RoutedEventArgs e)
		{
			JobData rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			frmMessageSchedule frm = new frmMessageSchedule();
			frm.msgSchedId = Convert.ToInt32(rowdata.JobData(GlobalShared.JOB_SCHEDULE));
			frm.Show();

		}


		private void ContentIdColumnClick(object sender, RoutedEventArgs e)
		{
			JobMaster rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			frmContent frm = new frmContent();
			frm.setContentId(rowdata.contentId);
			frm.setAllowEdit(false);
			frm.Show();

		}


		private void Schedule_idColumnClick(object sender, RoutedEventArgs e)
		{
			MessageSchedule rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			frmSchedule frm = new frmSchedule();
			frm.scheduleId = rowdata.schedule_id;
			frm.Show();

		}


		private void Content_idColumnClick(object sender, RoutedEventArgs e)
		{
			MessageSchedule rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			frmContent frm = new frmContent();
			frm.setContentId(rowdata.content_id);
			frm.setAllowEdit(false);
			frm.Show();

		}


		private void ListIdsColumnClick(object sender, RoutedEventArgs e)
		{
			JobMaster rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			string[] lists = rowdata.listIds.Split(",");
			if ((lists != null) && lists.Length > 0) {
				if (lists.Length > 1) {
					long[] l = new long[lists.Length];
					int i = 0;
					foreach (string lst in lists) {
						l(i) = Convert.ToInt64(lst);
						i += 1;
					}
					frmRecipListSelect frm = new frmRecipListSelect();
					frm.ListIds = l;
					frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen;
					frm.ShowDialog();
				} else {
					if (Information.IsNumeric(rowdata.listIds)) {
						frmRecipientEdit frm = new frmRecipientEdit();
						frm.listId = Convert.ToInt64(rowdata.listIds);
						frm.ShowDialog();
					}
				}
			}

		}


		private void RecipListsColumnClick(object sender, RoutedEventArgs e)
		{
			JobData rowdata = ((Hyperlink)e.OriginalSource).DataContext;

			string[] lists = rowdata.RecipLists.Split(",");
			if ((lists != null) && lists.Length > 0) {
				if (lists.Length > 1) {
					long[] l = new long[lists.Length];
					int i = 0;
					foreach (string lst in lists) {
						l(i) = Convert.ToInt64(lst);
						i += 1;
					}
					frmRecipListSelect frm = new frmRecipListSelect();
					frm.ListIds = l;
					frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen;
					frm.ShowDialog();
				} else {
					if (Information.IsNumeric(rowdata.RecipLists)) {
						frmRecipientEdit frm = new frmRecipientEdit();
						frm.listId = Convert.ToInt64(rowdata.RecipLists);
						frm.ShowDialog();
					}
				}
			}

		}


		private void cmdRefresh_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			displayJobData();

		}


		private void cmdStart_Click_1(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			string jobname = ((Button)sender).Tag.ToString;
			//Dim trigger As ISimpleTrigger = New Triggers.SimpleTriggerImpl("job1", "default", jobname, "Default", New DateTimeOffset(Now), Nothing, 0, New TimeSpan(0))

			JobKey jobKey = new JobKey(jobname, "Default");
			GlobalShared.jobRemoteSched.TriggerJob(jobKey);

		}


		private void ctxJobScheduleGotoJobMaster_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobData jd = (JobData)JobDataDataGrid.SelectedItem;
			foreach (JobMaster job in JobMasterDataGrid.Items) {
				if (job.jobId == jd.JobId) {
					tabItemJobMaster.Focus();
					JobMasterDataGrid.SelectedItem = job;
					JobMasterDataGrid.ScrollIntoView(job);
					return;
				}
			}

		}


		private void cmdRefresh_Click1(object sender, System.Windows.RoutedEventArgs e)
		{
			displayJobData();
			displayJobMaster();
			displayDeliverySchedules();

		}

		private void mnuJobmasterDelete_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			System.Collections.IList jobs = JobMasterDataGrid.SelectedItems;
			if ((jobs != null) && jobs.Count > 0) {
				if (Interaction.MsgBox("Delete these JobMaster records?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
					foreach (JobMaster Job in jobs) {
						Job.delete(Job.jobId);
					}
					displayJobMaster();
				}
			}
		}

		private void mnuJobmasterStatusReady_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobMaster job = JobMasterDataGrid.SelectedItem;
			if (!((job == null) > 0)) {
				job.status = JobMaster.job_status.ready;
				job.save();
				displayJobMaster();
			}
		}


		private void mnuJobMasterProcessed_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobMaster job = JobMasterDataGrid.SelectedItem;
			if (!((job == null) > 0)) {
				job.status = JobMaster.job_status.processed;
				job.save();
				displayJobMaster();
			}
		}


		private void mnuJobMasterPaused_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobMaster job = JobMasterDataGrid.SelectedItem;
			if (!((job == null) > 0)) {
				job.status = JobMaster.job_status.paused;
				job.save();
				displayJobMaster();
			}
		}


		private void ctxJobScheduleTrigger_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobKey jobkey = ((JobData)JobDataDataGrid.SelectedItem).JobKey;

			GlobalShared.jobRemoteSched.TriggerJob(jobkey);

		}


		private void ctxJobSchedulePause_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
		}


		private void ctxJobScheduleDelete_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			JobData jd = (JobData)JobDataDataGrid.SelectedItem;
			JobKey jobkey = jd.JobKey;

			if (Interaction.MsgBox("Delete this Job Schedule?", MsgBoxStyle.YesNo) == MsgBoxResult.No) {
				return;
			}

			GlobalShared.jobRemoteSched.DeleteJob(jobkey);

			foreach (JobMaster job in JobMasterDataGrid.Items) {
				if (job.jobId == jd.JobId) {
					job.delete(job.jobId);
					displayJobData();
					displayJobMaster();
					return;
				}
			}

		}


		private void ctxTriggerSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			TriggerData trigData = (TriggerData)grdTrigger.SelectedItem;

			Quartz.Impl.Triggers.CronTriggerImpl trigger = trigData.Trigger;
			JobKey jobKey = new JobKey(trigData.JobName, trigData.JobGroup);
			JobDetailImpl jd = GlobalShared.jobRemoteSched.GetJobDetail(jobKey);

			// Remove original job
			try {
				GlobalShared.jobRemoteSched.DeleteJob(jobKey);
			} catch (Quartz.JobPersistenceException ex) {
			} catch (Quartz.SchedulerException ex) {
			}

			// Create new trigger
			trigger.CronExpressionString = cronString(trigData.NextFireTime, jd.JobDataMap(GlobalShared.JOB_HOWOFTEN), jd.JobDataMap(GlobalShared.JOB_DAYTOPROCESS_2));
			trigger.StartTimeUtc = new DateTimeOffset(trigData.NextFireTime, new TimeSpan(-6, 0, 0));
			trigger.EndTimeUtc = new DateTimeOffset(trigData.EndTime, new TimeSpan(-6, 0, 0));
			// Schedule new job
			try {
				GlobalShared.jobRemoteSched.ScheduleJob(jd, trigger);
			} catch (Quartz.JobPersistenceException ex) {
			} catch (Quartz.SchedulerException ex) {
			}

			displayJobData();

		}


		private void optJobs_Checked(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (optJobsAll.IsChecked) {
				JobOpt = JobOpts.All;
			} else if (optJobsReady.IsChecked) {
				JobOpt = JobOpts.Ready;
			} else if (optJobsToday.IsChecked) {
				JobOpt = JobOpts.Today;
			} else if (optJobsTomorrow.IsChecked) {
				JobOpt = JobOpts.Tomorrow;
			}
			displayJobData();

		}


		private void lstServers_SelectionChanged(System.Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			txtServerName.Text = lstServers.SelectedItem;

		}


		private void cmdServerRevert_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			txtServerName.Text = GlobalShared.jobServer;

		}


		private void cmdServerApply_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			//TODO: Must verify new servername before allowing it to be changed
			if (!txtServerName.Text.ToLower.Equals(GlobalShared.jobServer.ToLower)) {
				if (testServer(txtServerName.Text)) {
					GlobalShared.jobServer = txtServerName.Text;
					// Now change all jobserver configs
					creation.MachineName = GlobalShared.jobServer;
					delivery.MachineName = GlobalShared.jobServer;
					listener.MachineName = GlobalShared.jobServer;
					QuartzConfig = null;
					QuartzConfig = new QuartzConfig("QuartzScheduler");
				} else {
					txtServerName.Text = GlobalShared.jobServer;
				}
			}

		}

		private bool testServer(string serverName)
		{

			string temp = creation.MachineName;
			creation.MachineName = serverName;
			creation.Refresh();
			try {
				stCreation = creation.Status.ToString;
				creation.MachineName = temp;
			} catch (System.Exception ex) {
				creation.MachineName = temp;
				Interaction.MsgBox("Cannot connect to server: " + ex.Message);
				return false;
			}

			return true;

		}
		public frmServices()
		{
			Loaded += frmServices_Loaded;
			Closing += frmServices_Closing;
		}

	}
}
namespace WinTRM
{

	public class ImpersonationUtil
	{
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		public static extern int LogonUserA(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]

		public static extern int DuplicateToken(IntPtr ExistingTokenHandle, int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);

		public static bool Impersonate(string logon, string password, string domain)
		{
			WindowsIdentity tempWindowsIdentity = default(WindowsIdentity);
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;

			try {

				if (LogonUserA(logon, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0) {
					if (DuplicateToken(token, 2, ref tokenDuplicate) != 0) {
						tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
						impersonationContext = tempWindowsIdentity.Impersonate();
						if ((impersonationContext != null))
							return true;
					}
				} else {
					int rc = Err.LastDllError;

				}
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}

			return false;
		}

		public static void UnImpersonate()
		{
			impersonationContext.Undo();
		}

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_LOGON_NETWORK_CLEARTEXT = 4;
		private const int LOGON32_PROVIDER_DEFAULT = 0;
		private static WindowsImpersonationContext impersonationContext;
	}
}
