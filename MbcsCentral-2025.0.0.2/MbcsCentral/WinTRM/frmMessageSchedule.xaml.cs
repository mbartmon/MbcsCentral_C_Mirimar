namespace WinTRM
{
	public class frmMessageSchedule
	{

		private Binding bind = new Binding();
		MessageSchedule msgSched = null;

		public Int32 msgSchedId;

		private void frmMessageSchedule_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Data.CollectionViewSource RecipientMasterViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientMasterViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//RecipientMasterViewSource.Source = [generic data source]
			RecipientMaster[] lists = RecipientMaster.load;
			RecipientMasterViewSource.Source = lists;

			SmtpConfig[] smtpAccts = SmtpConfig.load();
			lstSmtpAccount.ItemsSource = smtpAccts;

			Binding acctBinding = new Binding { Source = Utils.loadAccountList };
			lstAccount.SetBinding(ComboBox.ItemsSourceProperty, acctBinding);
			Binding scheduleBinding = new Binding { Source = Utils.loadScheduleList };
			lstSchedule.SetBinding(ComboBox.ItemsSourceProperty, scheduleBinding);
			Binding contentBinding = new Binding { Source = Utils.loadContentList };
			lstContent.SetBinding(ComboBox.ItemsSourceProperty, contentBinding);
			Binding statusBinding = new Binding { Source = Utils.loadStatusList };
			lstStatus.SetBinding(ComboBox.ItemsSourceProperty, statusBinding);
			for (int i = 0; i <= MessageSchedule.errorConditionOverrideDesc.Length - 1; i++) {
				lstError.Items.Add(new ListItem(MessageSchedule.errorConditionOverrideDesc(i), i));
			}
			lstError.SelectedIndex = 0;

			if (msgSchedId > 0) {
				txtMsgSched.Text = msgSchedId;
				msgSched = MessageSchedule.load(msgSchedId);
				if ((msgSched != null)) {
					foreach (bindAccountList acct in lstAccount.Items) {
						if (acct.id == msgSched.account_id) {
							lstAccount.SelectedItem = acct;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					foreach (bindScheduleList sched in lstSchedule.Items) {
						if (sched.id == msgSched.schedule_id) {
							lstSchedule.SelectedItem = sched;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					foreach (bindContentList cont in lstContent.Items) {
						if (cont.id == msgSched.content_id) {
							lstContent.SelectedItem = cont;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					lstSchedule.SelectedIndex = msgSched.status;
				}
				string[] s = msgSched.recipients.Split(",");
				if ((s != null)) {
					RecipientMaster list = default(RecipientMaster);
					foreach (string x in s) {
						list = RecipientMaster.load(Conversion.Val(x));
						lstListsActive.Items.Add(list);
						list = null;
					}
				}
				SmtpConfig sa = null;
				for (int i = 0; i <= lstSmtpAccount.Items.Count - 1; i++) {
					sa = lstSmtpAccount.Items(i);
					if (sa.ID == msgSched.smtp_acct) {
						lstSmtpAccount.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				if (msgSched.onError >= 0 & msgSched.onError <= MessageSchedule.errorConditionOverrideDesc.Length - 1) {
					lstError.SelectedIndex = msgSched.onError;
				}
			} else {
				msgSched = new MessageSchedule();
				msgSched.created = Now;
				msgSched.last_sent = 1/1/1900 00:00:00;
				foreach (bindAccountList acct in lstAccount.Items) {
					if (acct.id == GlobalShared.user.getAccountId()) {
						lstAccount.SelectedItem = acct;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}
			try {
				mainGrid.DataContext = msgSched;
			} catch (SystemException ex) {
				Interaction.MsgBox(ex.Message);
			}


		}


		private void cmdRecipients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			frmRecipientImports frm = new frmRecipientImports();
			frm.msgSched = msgSched;
			frm.Owner = this;
			frm.WindowStartupLocation = Windows.WindowStartupLocation.CenterOwner;
			frm.ShowDialog();

		}


		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstAccount.SelectedIndex < 0) {
				Interaction.MsgBox("Account not selected.");
				return;
			}
			msgSched.account_id = lstAccount.SelectedValue;
			if (lstContent.SelectedIndex < 0) {
				Interaction.MsgBox("Content not selected.");
				return;
			}
			msgSched.content_id = lstContent.SelectedValue;
			if (string.IsNullOrEmpty(txtDescription.Text.Trim)) {
				Interaction.MsgBox("Description must be filled-in.");
				return;
			}
			msgSched.description = txtDescription.Text;
			if (lstStatus.SelectedIndex < 0) {
				switch (Interaction.MsgBox("Status not set.  Do you want to set it?", MsgBoxStyle.YesNo)) {
					case MsgBoxResult.Yes:
						return;

						break;
				}
			}
			msgSched.status = lstStatus.SelectedValue;
			if (string.IsNullOrEmpty(txtFrom.Text.Trim)) {
				Interaction.MsgBox("A FROM address must be entered.");
				return;
			}
			if (!EmailAddressCheck(txtFrom.Text)) {
				Interaction.MsgBox("Invalid FROM email address.");
				return;
			}
			msgSched.@from = txtFrom.Text;

			schedules sched = schedules.load(msgSched.schedule_id);
			if ((sched == null)) {
				Interaction.MsgBox("Invalid schedule.");
				return;
			}
			msgSched.schedule_id = sched.getschedule_id;

			msgSched.number_to_process = sched.numberToProcess;
			msgSched.day_to_process = sched.dayToProcess;
			msgSched.day_to_process_2 = sched.dayToProcess2;
			msgSched.process_count = 0;
			msgSched.frequency = sched.getfrequency;
			msgSched.no_end_date = sched.noEndDate;
			msgSched.end_date = sched.getend_date;
			msgSched.start_date = sched.getstart_date;
			msgSched.smtp_acct = lstSmtpAccount.SelectedValue;
			msgSched.onError = lstError.SelectedIndex;
			msgSched.IncludeWeekends = sched.IncludeWeekends;
			msgSched.IncludeHolidays = sched.IncludeHolidays;

			msgSched.recipients = "";
			if (lstListsActive.Items.Count > 0) {
				string[] s = new string[lstListsActive.Items.Count];
				RecipientMaster l = default(RecipientMaster);
				for (int i = 0; i <= lstListsActive.Items.Count - 1; i++) {
					l = lstListsActive.Items(i);
					s(i) = l.listId;
				}
				msgSched.recipients = string.Join(",", s);
			}

			if (!msgSched.save) {
				Interaction.MsgBox("Error. Message Delivery Job not saved.");
				return;
			}

			this.Close();

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void cmdListAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstLists.SelectedItems.Count > 0) {
				foreach (RecipientMaster l in lstLists.SelectedItems) {
					if (!lstListsActive.Items.Contains(l)) {
						lstListsActive.Items.Add(l);
					}
				}
			}

		}


		private void cmdListDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			for (int i = 0; i <= lstListsActive.SelectedItems.Count - 1; i++) {
				lstListsActive.Items.Remove(lstListsActive.SelectedItems(0));
			}

		}


		private void cmdMessage_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstContent.SelectedIndex >= -1) {
				frmContent frm = new frmContent();
				frm.StartPosition = Forms.FormStartPosition.CenterScreen;
				frm.setContentId(lstContent.SelectedValue);
				frm.Show();
			}

		}
		public frmMessageSchedule()
		{
			Loaded += frmMessageSchedule_Loaded;
		}
	}
}
