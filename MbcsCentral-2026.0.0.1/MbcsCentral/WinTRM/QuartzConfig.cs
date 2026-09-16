using System.Collections.Specialized;
namespace WinTRM
{
	public class QuartzConfig
	{



		private NameValueCollection _properties = new NameValueCollection();

		public QuartzConfig(string instanceName)
		{
			if ((instanceName == null) || string.IsNullOrEmpty(instanceName.Trim)) {
				instanceName = "instance_1";
			}
			_properties.Add("quartz.scheduler.instanceName", instanceName);
			//_properties.Add("quartz.scheduler.instanceId", instanceName)
			_properties.Add("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
			_properties.Add("quartz.jobStore.useProperties", "true");
			_properties.Add("quartz.jobStore.dataSource", "default");
			_properties.Add("quartz.jobStore.tablePrefix", "QRTZ_");

			_properties.Add("quartz.scheduler.exporter.type", "Quartz.Simpl.RemotingSchedulerExporter, Quartz");
			_properties.Add("quartz.scheduler.exporter.port", "555");
			_properties.Add("quartz.scheduler.exporter.bindName", "QuartzScheduler");
			_properties.Add("quartz.scheduler.exporter.channelType", "tcp");

			// if running MS SQL Server we need this
			_properties.Add("quartz.jobStore.lockHandler.type", "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz");
			_properties.Add("quartz.dataSource.default.connectionString", "Data Source=<machineName>\\SQLEXPRESSMBCS;Initial Catalog=WinTrm;User Id=mbcsLogin;Password=mbcs#4Quartz");
			_properties.Add("quartz.dataSource.default.provider", "SqlServer-20");
			_properties.Add("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz");
		}
		public NameValueCollection Properties {
			get { return _properties; }
		}

		public string[] Add {
			set { _properties.Add(value(0), value(1)); }
		}

	}
}
