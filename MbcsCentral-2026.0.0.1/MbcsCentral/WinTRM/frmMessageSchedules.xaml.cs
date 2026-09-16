namespace WinTRM
{
	public class frmMessageSchedules
	{

		bindMessageSchedules[] scheds;

		Binding bind = new Binding();

		private void frmMessageSchedules_Initialized(object sender, System.EventArgs e)
		{
			if (!GlobalShared.user.hasAdminPermission()) {
				chkAll.Visibility = Windows.Visibility.Hidden;
			}
			getScheds(GlobalShared.user.getAccountId());

		}


		private void getScheds(Int32 acct)
		{
			scheds = null;
			scheds = MessageSchedule.loadByAccountResolved(acct);
			if ((scheds != null)) {
				try {
					grdSchedules.DataContext = null;
				} catch (System.Exception ex) {
				}
				grdSchedules.DataContext = scheds;
				bind.Source = scheds;
				grdSchedules.ItemsSource = scheds;
			}

		}


		private void cmdEdit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			bindMessageSchedules s = grdSchedules.SelectedItem;
			if ((s != null)) {
				frmMessageSchedule frm = new frmMessageSchedule();
				frm.msgSchedId = s.Id;
				frm.Owner = this;
				frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				frm.ShowDialog();
				getScheds(GlobalShared.user.getAccountId());
			}

		}


		private void chkAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (chkAll.IsChecked) {
				getScheds(0);
			} else {
				getScheds(GlobalShared.user.getAccountId());
			}
			if ((scheds != null)) {
				grdSchedules.DataContext = scheds;
				bind.Source = scheds;
				grdSchedules.ItemsSource = scheds;
			}

			grdSchedules.Items.Refresh();
		}


		private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmMessageSchedule frm = new frmMessageSchedule();
			frm.ShowDialog();
			getScheds(GlobalShared.user.getAccountId());

		}

		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}


		private void cmdDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			bindMessageSchedules s = grdSchedules.SelectedItem;
			if ((s != null)) {
				if (Interaction.MsgBox("Delete this Delivery Schedule?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
					MessageSchedule sch = new MessageSchedule();
					sch.delete(s.Id);
					getScheds(GlobalShared.user.getAccountId());
				}
			}

		}
		public frmMessageSchedules()
		{
			Initialized += frmMessageSchedules_Initialized;
		}
	}
}
