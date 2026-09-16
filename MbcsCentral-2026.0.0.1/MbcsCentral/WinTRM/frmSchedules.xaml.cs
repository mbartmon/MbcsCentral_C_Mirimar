using System.Data;
namespace WinTRM
{

	public class frmSchedules
	{

		ScheduleByAccount[] scheds;
		long acctId;

		Binding bind = new Binding();

		private void frmSchedules_Initialized(object sender, System.EventArgs e)
		{
			acctId = GlobalShared.user.getAccountId();
			getScheds();

		}


		private void getScheds()
		{
			try {
				grdSchedules.DataContext = null;
			} catch (System.Exception ex) {
			}
			scheds = null;
			scheds = schedules.loadByAccountResolved(acctId);
			if ((scheds != null)) {
				grdSchedules.DataContext = scheds;
				bind.Source = scheds;
				bind.Mode = BindingMode.OneWay;
				grdSchedules.ItemsSource = scheds;
				// grdSchedules.SetBinding(DataGrid.ItemsSourceProperty, bind)
			}

		}

		private void grdSchedules_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEdit_Click(sender, null);
			return;

			Int32 i = grdSchedules.SelectedIndex;
			ScheduleByAccount sched = grdSchedules.SelectedItem;
			if (!Information.IsDBNull(sched.ID)) {
				frmSchedule frm = new frmSchedule();
				frm.scheduleId = sched.ID;
				frm.schedByAcct = sched;
				frm.ShowDialog();
				getScheds();
				//grdSchedules.DataContext = Nothing
				// grdSchedules.DataContext = scheds
				//grdSchedules.SetBinding(DataGrid.ItemsSourceProperty, bind)
				grdSchedules.UpdateLayout();
				grdSchedules.Items.Refresh();
			}

		}


		private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!GlobalShared.user.hasUserPermission()) {
				Interaction.MsgBox("You do not have permission to add Schedules");
				return;
			}
			frmSchedule frm = new frmSchedule();
			frm.Owner = this;
			frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			frm.ShowDialog();
			getScheds();

		}


		private void cmdEdit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ScheduleByAccount s = grdSchedules.SelectedItem;
			if ((s != null)) {
				frmSchedule frm = new frmSchedule();
				frm.Owner = this;
				frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				frm.scheduleId = s.ID;
				frm.schedByAcct = s;
				frm.ShowDialog();
				//getScheds()
				grdSchedules.Items.Refresh();
			}

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void cmdDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!GlobalShared.user.hasUserPermission()) {
				Interaction.MsgBox("You do not have permission to delete Schedules");
				return;
			}
			ScheduleByAccount s = grdSchedules.SelectedItem;
			if ((s != null)) {
				if (Interaction.MsgBox("Delete this schedule?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
					schedules sc = schedules.load(s.ID);
					if ((sc != null)) {
						sc.delete(s.ID);
					}
					getScheds();
				}
			}

		}
		public frmSchedules()
		{
			Initialized += frmSchedules_Initialized;
		}
	}
}
