using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
namespace WinTRM
{
	public class frmJobDashboard
	{

		IScheduler sched;

		ISchedulerFactory schedFact;
		System.Windows.Data.CollectionViewSource JobMasterViewSource;

		JobMaster[] jobs;

		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			schedFact = new StdSchedulerFactory();
			sched = schedFact.GetScheduler;
			sched.Start();

			JobMasterViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("JobMasterViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//JobMasterViewSource.Source = [generic data source]
			refreshJobs();

		}


		private void refreshJobs()
		{
			JobMasterViewSource.Source = null;
			jobs = JobMaster.load;
			JobMasterViewSource.Source = jobs;

			txtRefreshed.Text = Now();

		}

		private void cmdRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			refreshJobs();

		}
		public frmJobDashboard()
		{
			Loaded += Window_Loaded;
		}
	}
}
