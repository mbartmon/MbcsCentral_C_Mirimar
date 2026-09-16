using System.ComponentModel;
namespace WinTRM
{

	public class TriggerData : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;

		private string m_schedName;
		private string m_triggerName;
		private string m_triggerGroup;
		private string m_jobName;
		private string m_jobGroup;
		private string m_description;
		private DateTimeOffset m_nextFireTime;
		private DateTimeOffset m_prevFireTime;
		private int m_priority;
		private string m_triggerState;
		private string m_triggerType;
		private DateTimeOffset m_startTime;
		private DateTimeOffset m_endTime;
		private Quartz.TriggerKey m_triggerKey;
		private Quartz.Impl.Triggers.CronTriggerImpl m_trigger;

		private bool m_changed = false;

		public TriggerData()
		{
		}

		public TriggerData(string schedName, string triggerName, string triggerGroup, string jobName, string jobGroup, string description, DateTimeOffset nextFireTime, DateTimeOffset prevFireTime, int priority, string triggerState,

		string triggerType, DateTimeOffset startTime, DateTimeOffset endTime, Quartz.TriggerKey triggerKey, Quartz.Impl.Triggers.CronTriggerImpl trigger)
		{
			m_schedName = schedName;
			m_triggerName = triggerName;
			m_jobName = jobName;
			m_jobGroup = jobGroup;
			m_description = description;
			m_nextFireTime = nextFireTime.ToLocalTime;
			m_prevFireTime = ((prevFireTime == null) ? new DateTimeOffset(1/1/1900 00:00:00) : prevFireTime.ToLocalTime);
			m_priority = priority;
			m_triggerState = triggerState;
			m_triggerType = triggerType;
			m_startTime = startTime.ToLocalTime;
			m_endTime = endTime.ToLocalTime;
			m_triggerKey = triggerKey;
			m_trigger = trigger;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_changed = true;
		}

		// Public Properties
		public string SchedName {
			get { return m_schedName; }
			set {
				m_schedName = value;
				OnPropertyChanged("SchedName");
			}
		}
		public string TriggerName {
			get { return m_triggerName; }
			set {
				m_triggerName = value;
				OnPropertyChanged("TriggerName");
			}
		}
		public string TriggerGroup {
			get { return m_triggerGroup; }
			set {
				m_triggerGroup = value;
				OnPropertyChanged("TriggerGroup");
			}
		}
		public string JobName {
			get { return m_jobName; }
			set {
				m_jobName = value;
				OnPropertyChanged("JobName");
			}
		}
		public string JobGroup {
			get { return m_jobGroup; }
			set {
				m_jobGroup = value;
				OnPropertyChanged("JobGroup");
			}
		}
		public string Description {
			get { return m_description; }
			set {
				m_description = value;
				OnPropertyChanged("Description");
			}
		}
		public DateTime NextFireTime {
			get { return ((m_nextFireTime == null) ? new DateTime(1900, 1, 1) : m_nextFireTime.DateTime); }
			set {
				m_nextFireTime = new DateTimeOffset(value, new TimeSpan(-6, 0, 0));
				OnPropertyChanged("NextFireTime");
			}
		}
		public DateTimeOffset PrevFireTime {
			get { return ((m_prevFireTime == null) ? new DateTimeOffset(1/1/1900 00:00:00) : m_prevFireTime); }
			set {
				m_prevFireTime = value;
				OnPropertyChanged("PrevFireTime");
			}
		}
		public int Priority {
			get { return m_priority; }
			set {
				m_priority = value;
				OnPropertyChanged("Priority");
			}
		}
		public string TriggerState {
			get { return m_triggerState; }
			set {
				m_triggerState = value;
				OnPropertyChanged("TriggerState");
			}
		}
		public string TriggerType {
			get { return m_triggerType; }
			set {
				m_triggerType = value;
				OnPropertyChanged("TriggerType");
			}
		}
		public DateTimeOffset StartTime {
			get { return m_startTime; }
			set {
				m_startTime = value;
				OnPropertyChanged("StartTime");
			}
		}
		public DateTime EndTime {
			get { return m_endTime.DateTime; }
			set {
				m_endTime = new DateTimeOffset(value, new TimeSpan(-6, 0, 0));
				OnPropertyChanged("EndTime");
			}
		}
		public Quartz.TriggerKey TriggerKey {
			get { return m_triggerKey; }
			set { m_triggerKey = value; }
		}
		public Quartz.Impl.Triggers.CronTriggerImpl Trigger {
			get { return m_trigger; }
			set { m_trigger = value; }
		}
	}
}
