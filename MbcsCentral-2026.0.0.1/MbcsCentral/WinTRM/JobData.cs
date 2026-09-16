using Quartz;
using Quartz.Impl;
using Quartz.Collection;
using System.Collections.Generic;
namespace WinTRM
{

	public class JobData
	{

		// Fields
		private string m_jobId;
		private string m_jobAccount;
		private string m_recipLists;
		private string m_from;
		private string m_subject;
		private string m_body;
		private string m_schedName;
		private string m_jobName;
		private string m_jobGroup;
		private string m_description;
		private string m_jobClassName;
		private bool m_isDurable;
		private bool m_isNonCurrent;
		private bool m_isUpdateData;
		private bool m_requestRecovery;
		private JobDataMap m_jobData;
		private bool m_returnReceipt;

		private TriggerData[] m_triggers;

		public JobData()
		{
		}

		public JobData(string schedName, string jobName, string jobGroup, string description, string jobClassName, bool isDurable, bool isNonCurrent, bool isUpdateData, bool requestRecovery, JobDataMap jobData,

		TriggerData[] triggers, string jobId, string jobAccount, string recipLists, string @from, string subject, string body, bool returnReceipt)
		{
			m_schedName = schedName;
			m_jobName = jobName;
			m_jobGroup = jobGroup;
			m_description = description;
			m_jobClassName = jobClassName;
			m_isDurable = isDurable;
			m_isNonCurrent = isNonCurrent;
			m_isUpdateData = isUpdateData;
			m_requestRecovery = requestRecovery;
			m_jobData = jobData;
			m_triggers = triggers;
			if ((m_triggers == null)) {
				getTrigger();
			}
			m_jobId = jobId;
			m_jobAccount = jobAccount;
			m_recipLists = recipLists;
			m_from = @from;
			m_subject = subject;
			m_body = body;
			m_returnReceipt = returnReceipt;

		}


		private void getTrigger()
		{

			try {
				if ((GlobalShared.jobRemoteSched == null)) {
					QuartzConfig QuartzConfig = new QuartzConfig("QuartzScheduler");
					string[] s = {
						"quartz.scheduler.proxy",
						"true"
					};
					QuartzConfig.Add = s;
					s = {
						"quartz.scheduler.proxy.address",
						"tcp://localhost:555/QuartzScheduler"
					};
					QuartzConfig.Add = s;

					ISchedulerFactory schedfact = new StdSchedulerFactory();
					schedfact = new StdSchedulerFactory(QuartzConfig.Properties);
					try {
						GlobalShared.jobRemoteSched = schedfact.GetScheduler();
					} catch (Quartz.SchedulerConfigException ex) {
						return;
					} catch (Quartz.SchedulerException ex) {
						return;
					} catch (System.Exception ex) {
						return;
					}
				}

				JobKey jobkey = new JobKey(m_jobName, m_jobGroup);
				try {
					IList<ITrigger> triggers = GlobalShared.jobRemoteSched.GetTriggersOfJob(jobkey);
					m_triggers = new TriggerData[triggers.Count];
					int i = 0;
					foreach (Quartz.ITrigger t in triggers) {
						m_triggers(i) = new TriggerData(m_schedName, t.Key.Name, t.Key.Group, m_jobName, m_jobGroup, ((t.Description == null) ? "" : t.Description), t.GetNextFireTimeUtc, ((t.GetPreviousFireTimeUtc == null) ? new DateTimeOffset(1/1/1900 00:00:00) : t.GetPreviousFireTimeUtc), t.Priority, "",
						"", t.StartTimeUtc, ((t.EndTimeUtc == null) ? new DateTimeOffset(1/1/1900 00:00:00) : t.EndTimeUtc), t.Key, t);
						i += 1;
					}
				} catch (Quartz.SchedulerConfigException ex) {
					return;
				} catch (Quartz.SchedulerException ex) {
					return;
				} catch (System.Exception ex) {
					return;
				}
			} catch (NullReferenceException ex) {
			} catch (System.Exception ex) {
			}

		}

		// Public Properties
		public string SchedName {
			get { return m_schedName; }
			set { m_schedName = value; }
		}
		public string JobName {
			get { return m_jobName; }
			set { m_jobName = value; }
		}
		public string JobGroup {
			get { return m_jobGroup; }
			set { m_jobGroup = value; }
		}
		public string Description {
			get { return m_description; }
			set { m_description = value; }
		}
		public string JobClassName {
			get { return m_jobClassName; }
			set { m_jobClassName = value; }
		}
		public bool IsDurable {
			get { return m_isDurable; }
			set { m_isDurable = value; }
		}
		public bool IsNonCurrent {
			get { return m_isNonCurrent; }
			set { m_isNonCurrent = value; }
		}
		public bool IsUpdateData {
			get { return m_isUpdateData; }
			set { m_isUpdateData = value; }
		}
		public bool RequestRecovery {
			get { return m_requestRecovery; }
			set { m_requestRecovery = value; }
		}
		public JobDataMap JobData {
			get { return m_jobData; }
			set { m_jobData = value; }
		}
		public TriggerData[] Triggers {
			get {
				if ((m_triggers == null)) {
					getTrigger();
				}
				return m_triggers;
			}
			set { m_triggers = value; }
		}
		public string JobId {
			get { return m_jobId; }
			set { m_jobId = value; }
		}
		public string JobAccount {
			get { return m_jobAccount; }
			set { m_jobAccount = value; }
		}
		public string RecipLists {
			get { return m_recipLists; }
			set { m_recipLists = value; }
		}
		public string From {
			get { return m_from; }
			set { m_from = value; }
		}
		public string Subject {
			get { return m_subject; }
			set { m_subject = value; }
		}
		public string Body {
			get { return m_body; }
			set { m_body = value; }
		}
		public string AccountName {
			get {
				Accounts acct = Accounts.load(Convert.ToInt64(m_jobAccount));
				string acctName = null;
				if ((acct != null)) {
					acctName = acct.getName;
				} else {
					acctName = "";
				}
				acct = null;
				return acctName;
			}
		}
		public JobKey JobKey {
			get { return new JobKey(m_jobName, m_jobGroup); }
		}
		public string BodyName {
			get {
				contents content = contents.load(Convert.ToInt32(m_jobData(GlobalShared.JOB_BODY)));
				if ((content != null)) {
					return content.getDescription;
				} else {
					return "";
				}
			}
		}
		public string BodyContent {
			get {
				contents content = new contents();
				content = contents.load(Convert.ToInt32(m_jobData(GlobalShared.JOB_BODY)));
				if ((content != null)) {
					return content.getcontent;
				} else {
					return null;
				}
			}
		}
		public string MessageDeliverySchedule {
			get {
				MessageSchedule msg = MessageSchedule.load(Convert.ToInt32(m_jobData(GlobalShared.JOB_SCHEDULE)));
				if ((msg != null)) {
					return msg.description;
				} else {
					return "";
				}
			}
		}
		public bool ReturnReceipt {
			get { return m_returnReceipt; }
			set { m_returnReceipt = value; }
		}
	}
}
