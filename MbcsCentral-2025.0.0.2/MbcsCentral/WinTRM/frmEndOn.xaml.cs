namespace WinTRM
{
	public class frmEndOn
	{

		private bool m_noEndDate;
		public bool noEndDate {
			get { return m_noEndDate; }
			set { m_noEndDate = value; }
		}
		private System.DateTime m_endOn = null;
		public System.DateTime endOn {
			get { return m_endOn; }
			set { m_endOn = value; }
		}
		private Int16 m_numberOfTimes = 1;
		public Int16 numberOfTimes {
			get { return m_numberOfTimes; }
			set { m_numberOfTimes = value; }
		}
		private bool m_cancel;
		public bool cancel {
			get { return m_cancel; }
		}


		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (optNoEndDate.IsChecked) {
				m_noEndDate = true;
				m_endOn = null;
				m_numberOfTimes = 0;
			}
			if (optEndOn.IsChecked) {
				m_noEndDate = false;
				m_endOn = dtEndOn.SelectedDate;
				m_numberOfTimes = 0;
			}
			if (optEndAfter.IsChecked) {
				m_noEndDate = false;
				m_endOn = null;
				m_numberOfTimes = lstEndAfter.Value;
			}
			m_cancel = false;
			this.Hide();

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			m_cancel = true;
			this.Hide();

		}


		private void frmEndOn_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (m_noEndDate) {
				dtEndOn.SelectedDate = Now;
				dtEndOn.DisplayDate = Now;
				lstEndAfter.Value = 0;
				optNoEndDate.IsChecked = true;
			} else if (m_numberOfTimes > 0) {
				lstEndAfter.Value = m_numberOfTimes;
				dtEndOn.SelectedDate = Now;
				dtEndOn.DisplayDate = Now;
				optEndAfter.IsChecked = true;
			} else if ((m_endOn != null) & m_endOn.Date.Year > 1) {
				dtEndOn.SelectedDate = m_endOn;
				dtEndOn.DisplayDate = m_endOn;
				optEndOn.IsChecked = true;
			} else {
				dtEndOn.SelectedDate = Now;
				dtEndOn.DisplayDate = Now;
				optNoEndDate.IsChecked = true;
			}

		}


		private void dtEndOn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			optEndOn.IsChecked = true;

		}


		private void lstEndAfter_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			optEndAfter.IsChecked = true;

		}
		public frmEndOn()
		{
			Loaded += frmEndOn_Loaded;
		}

	}
}
