using System.Globalization;
using Xceed.Wpf.Toolkit;
using WinTRM.schedules;
namespace WinTRM
{
	public class frmSchedule
	{

		private Grid[] oftenOpts = new Grid[often.Length];
		private ComboBox withEventsField_lstDayOrdinal;
		internal ComboBox lstDayOrdinal {
			get { return withEventsField_lstDayOrdinal; }
			set {
				if (withEventsField_lstDayOrdinal != null) {
					withEventsField_lstDayOrdinal.SelectionChanged -= lstDayOrdinal_SelectionChanged;
				}
				withEventsField_lstDayOrdinal = value;
				if (withEventsField_lstDayOrdinal != null) {
					withEventsField_lstDayOrdinal.SelectionChanged += lstDayOrdinal_SelectionChanged;
				}
			}
		}
		private ComboBox withEventsField_lstDayOfWeek;
		internal ComboBox lstDayOfWeek {
			get { return withEventsField_lstDayOfWeek; }
			set {
				if (withEventsField_lstDayOfWeek != null) {
					withEventsField_lstDayOfWeek.SelectionChanged -= lstDayOfWeek_SelectionChanged;
				}
				withEventsField_lstDayOfWeek = value;
				if (withEventsField_lstDayOfWeek != null) {
					withEventsField_lstDayOfWeek.SelectionChanged += lstDayOfWeek_SelectionChanged;
				}
			}
		}
		internal RadioButton optOnDate;
		internal RadioButton optOnDay;
		private RadioButton withEventsField_optLastDay;
		internal RadioButton optLastDay {
			get { return withEventsField_optLastDay; }
			set {
				if (withEventsField_optLastDay != null) {
					withEventsField_optLastDay.Checked -= optLastDay_Checked;
				}
				withEventsField_optLastDay = value;
				if (withEventsField_optLastDay != null) {
					withEventsField_optLastDay.Checked += optLastDay_Checked;
				}
			}
		}
		internal RadioButton optThisThatDay;
		internal RadioButton optThisLastDay;
		internal IntegerUpDown upDnThisOne;
		internal IntegerUpDown upDnThatOne;
		internal IntegerUpDown upDnThisTwo;
		private bool dayContentFlag = false;
		private bool dateChangeFlag = false;

		private bool loadingFlag = true;
		public ScheduleByAccount schedByAcct;
		public long scheduleId = 0;
		private long acctId;
		private schedules sched = null;

		private Int16 selOpt = 0;

		private void frmSchedule_Initialized(object sender, System.EventArgs e)
		{
		}


		private void setUpExistingSchedule()
		{
			txtName.Text = sched.getschedule_name;
			dtNextDue.Value = sched.getstart_date;
			//dtNextDue.Value = sched.getstart_date
			//dtTime.Time = Format(sched.getstart_date, "hh:mm tt")
			selOpt = sched.getfrequency;
			switch (selOpt) {
				case HowOften.Monthly:
					if (sched.dayToProcess2 == -1) {
						optOnDate.IsChecked = true;
					//optLastDay.IsChecked Then
					} else if (sched.dayToProcess2 == 99) {
						optLastDay.IsChecked = true;
					} else {
						optOnDay.IsChecked = true;
						lstDayOrdinal.SelectedIndex = sched.dayToProcess;
						lstDayOfWeek.SelectedIndex = sched.dayToProcess2;
					}
					break;
				case HowOften.SemiMonthly:
					if (sched.getday_to_process_2 < 99) {
						optThisThatDay.IsChecked = true;
						upDnThisOne.Value = sched.getday_to_process;
						upDnThatOne.Value = sched.getday_to_process_2;
					} else {
						optThisLastDay.IsChecked = true;
						upDnThisTwo.Value = sched.getday_to_process;
					}
					break;
			}
			lstHowOften.SelectedIndex = selOpt;

		}


		private void setOftenOpts()
		{
			// Once - empty grid
			oftenOpts(0) = new Grid();
			Label lbl = new Label {
				Name = "lbl0",
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			oftenOpts(0).Children.Add(lbl);

			// Daily
			oftenOpts(1) = new Grid();
			lbl = new Label {
				Name = "lbl1",
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			oftenOpts(1).Children.Add(lbl);

			// Weekly
			oftenOpts(2) = new Grid();
			lbl = new Label {
				Name = "lbl2",
				Content = "Every week on",
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			oftenOpts(2).Children.Add(lbl);

			// Bi-Weekly
			oftenOpts(3) = new Grid();
			lbl = new Label {
				Name = "lbl3",
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			oftenOpts(3).Children.Add(lbl);

			// Monthly
			oftenOpts(4) = new Grid();

			Windows.Controls.RowDefinition row = new Windows.Controls.RowDefinition();
			oftenOpts(4).RowDefinitions.Add(row);
			row = new Windows.Controls.RowDefinition();
			oftenOpts(4).RowDefinitions.Add(row);
			row = new Windows.Controls.RowDefinition();
			oftenOpts(4).RowDefinitions.Add(row);

			Windows.Controls.ColumnDefinition col = new Windows.Controls.ColumnDefinition { Width = new GridLength(37) };
			oftenOpts(4).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(1.85, GridUnitType.Star) };
			oftenOpts(4).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) };
			oftenOpts(4).ColumnDefinitions.Add(col);

			optOnDate = new RadioButton();
			optOnDate.Content = "The";
			optOnDate.IsChecked = true;
			Windows.Controls.Grid.SetRow(optOnDate, 0);
			Windows.Controls.Grid.SetColumnSpan(optOnDate, 3);

			optOnDay = new RadioButton();
			optOnDay.Content = "The ";
			Windows.Controls.Grid.SetRow(optOnDay, 1);

			optLastDay = new RadioButton();
			optLastDay.Content = "The last day of every month";
			Windows.Controls.Grid.SetRow(optLastDay, 2);
			Windows.Controls.Grid.SetColumnSpan(optLastDay, 3);

			oftenOpts(4).Children.Add(optOnDate);
			oftenOpts(4).Children.Add(optOnDay);
			oftenOpts(4).Children.Add(optLastDay);

			// Semi-Monthly
			oftenOpts(5) = new Grid();
			row = new Windows.Controls.RowDefinition();
			oftenOpts(5).RowDefinitions.Add(row);
			row = new Windows.Controls.RowDefinition();
			oftenOpts(5).RowDefinitions.Add(row);

			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star) };
			oftenOpts(5).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
			oftenOpts(5).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) };
			oftenOpts(5).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
			oftenOpts(5).ColumnDefinitions.Add(col);
			col = new Windows.Controls.ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) };
			oftenOpts(5).ColumnDefinitions.Add(col);

			optThisThatDay = new RadioButton {
				Content = "On the",
				VerticalAlignment = VerticalAlignment.Top,
				IsChecked = true
			};
			Windows.Controls.Grid.SetRow(optThisThatDay, 0);
			Windows.Controls.Grid.SetColumn(optThisThatDay, 0);

			optThisLastDay = new RadioButton {
				Content = "On the",
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(optThisLastDay, 1);
			Windows.Controls.Grid.SetColumn(optThisLastDay, 0);

			oftenOpts(5).Children.Add(optThisThatDay);
			oftenOpts(5).Children.Add(optThisLastDay);

			upDnThisOne = new IntegerUpDown {
				Height = 25,
				Minimum = 1,
				Maximum = 31,
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(upDnThisOne, 0);
			Windows.Controls.Grid.SetColumn(upDnThisOne, 1);
			oftenOpts(5).Children.Add(upDnThisOne);
			lbl = new Label {
				Content = "and",
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(lbl, 0);
			Windows.Controls.Grid.SetColumn(lbl, 2);
			oftenOpts(5).Children.Add(lbl);
			upDnThatOne = new IntegerUpDown {
				Height = 25,
				Minimum = 1,
				Maximum = 31,
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(upDnThatOne, 0);
			Windows.Controls.Grid.SetColumn(upDnThatOne, 3);
			oftenOpts(5).Children.Add(upDnThatOne);
			lbl = new Label {
				Content = "days of the month",
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(lbl, 0);
			Windows.Controls.Grid.SetColumn(lbl, 4);
			oftenOpts(5).Children.Add(lbl);
			upDnThisTwo = new IntegerUpDown {
				Height = 25,
				Minimum = 1,
				Maximum = 31,
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(upDnThisTwo, 1);
			Windows.Controls.Grid.SetColumn(upDnThisTwo, 1);
			oftenOpts(5).Children.Add(upDnThisTwo);
			lbl = new Label {
				Content = "and the last day of the month",
				VerticalAlignment = VerticalAlignment.Top
			};
			Windows.Controls.Grid.SetRow(lbl, 1);
			Windows.Controls.Grid.SetColumn(lbl, 2);
			Windows.Controls.Grid.SetColumnSpan(lbl, 3);
			oftenOpts(5).Children.Add(lbl);

			// Two Months
			oftenOpts(6) = new Grid();

			// Quarterly
			oftenOpts(7) = new Grid();

			for (int i = 0; i <= oftenOpts.Length - 1; i++) {
				Windows.Controls.Grid.SetRow(oftenOpts(i), 3);
				Windows.Controls.Grid.SetColumn(oftenOpts(i), 1);
				oftenOpts(i).Visibility = Visibility.Hidden;
				grid.Children.Add(oftenOpts(i));
			}
			lstDayOrdinal = new ComboBox {
				Height = 23,
				VerticalAlignment = VerticalAlignment.Top,
				ItemsSource = {
					"First",
					"Second",
					"Third",
					"Fourth",
					"Last"
				},
				SelectedIndex = 0,
				Name = "lstDayOrdinal"
			};
			Windows.Controls.Grid.SetColumn(lstDayOrdinal, 1);
			Windows.Controls.Grid.SetRow(lstDayOrdinal, 1);
			oftenOpts(4).Children.Add(lstDayOrdinal);
			lstDayOfWeek = new ComboBox {
				Height = 23,
				VerticalAlignment = VerticalAlignment.Top,
				ItemsSource = {
					"Sunday",
					"Monday",
					"Tuesday",
					"Wednesday",
					"Thursday",
					"Friday",
					"Saturday"
				},
				SelectedIndex = 0,
				Name = "lstDayOfWeek"
			};
			Windows.Controls.Grid.SetColumn(lstDayOfWeek, 2);
			Windows.Controls.Grid.SetRow(lstDayOfWeek, 1);
			oftenOpts(4).Children.Add(lstDayOfWeek);

		}

		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtName.Text.Trim)) {
				Interaction.MsgBox("NAME is required");
				return;
			}
			if (scheduleId == 0) {
				sched.setaccount_id(GlobalShared.user.getAccountId());
			}
			sched.setactive(chkActive.IsChecked);
			sched.setschedule_name(txtName.Text);
			//Dim dtTemp As String = dtTime.Time

			sched.setstart_date(dtNextDue.Value);
			sched.setfrequency(selOpt);
			// What are these doing here?
			//schedByAcct.Name = txtName.Text
			//schedByAcct.HowOften = often(selOpt)
			switch (selOpt) {
				case HowOften.Monthly:
					if (optOnDate.IsChecked) {
						sched.dayToProcess = Day(dtNextDue.Value);
						sched.dayToProcess2 = -1;
					} else if (optOnDay.IsChecked) {
						sched.dayToProcess = lstDayOrdinal.SelectedIndex;
						sched.dayToProcess2 = lstDayOfWeek.SelectedIndex;
					} else if (optLastDay.IsChecked) {
						sched.dayToProcess = -1;
						sched.dayToProcess2 = 99;
					}
					break;
				case HowOften.SemiMonthly:
					if (optThisThatDay.IsChecked) {
						sched.setday_to_process(upDnThisOne.Value);
						sched.setday_to_process_2(upDnThatOne.Value);
					} else if (optThisLastDay.IsChecked) {
						sched.setday_to_process(upDnThisTwo.Value);
						sched.setday_to_process_2(99);
					}

					break;
			}
			sched.save();
			this.Close();

		}


		private void lstHowOften_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			selOpt = lstHowOften.SelectedIndex;
			for (Int16 i = 0; i <= oftenOpts.Length - 1; i++) {
				if (i == selOpt) {
					switch (i) {
						case HowOften.Once:
							break;
						case HowOften.Daily:
							break;
						case HowOften.Weekly:
							setNextDateContent();
							break;
						case HowOften.BiWeekly:
							break;
						case HowOften.Monthly:
							setNextDateContent();
							dateChangeFlag = true;
							setDayContent();
							dateChangeFlag = false;
							break;
						case HowOften.SemiMonthly:
							setUpDown();
							break;
						case HowOften.TwoMonths:
							break;
						case HowOften.Quarterly:
							break;
						default:
							break;
					}
					oftenOpts(selOpt).Visibility = Visibility.Visible;
				} else {
					oftenOpts(i).Visibility = Visibility.Hidden;
				}
			}

		}


		public void setNextDateContent()
		{
			switch (selOpt) {
				case HowOften.Weekly:
					Label lbl = oftenOpts(selOpt).Children(0);
					lbl.Content = "Every week on " + dtNextDue.Value.Value.DayOfWeek.ToString;
					// CultureInfo.CurrentCulture.DateTimeFormat.DayNames(dtNextDue.Value.Value.DayOfWeek)
					break;
				case HowOften.BiWeekly:
					Label lbl = oftenOpts(selOpt).Children(0);
					lbl.Content = "Every two weeks on " + dtNextDue.Value.Value.DayOfWeek.ToString;
					// CultureInfo.CurrentCulture.DateTimeFormat.DayNames(dtNextDue.Value.Value.DayOfWeek)
					break;
				case HowOften.Monthly:
					Int16 d = Day(dtNextDue.Value);
					optOnDate.Content = "The " + d;
					if (d >= 10 & d <= 19) {
						optOnDate.Content += "th ";
					} else {
						switch (Conversion.Val(Strings.Right(Strings.Format(d, "##"), 1))) {
							case 1:
								optOnDate.Content += "st ";
								break;
							case 2:
								optOnDate.Content += "nd ";
								break;
							case 3:
								optOnDate.Content += "rd ";
								break;
							case 0:
							case 4:
							case 5:
							case 6:
							case 7:
							case 8:
							case 9:
								optOnDate.Content += "th ";
								break;
							default:
								break;
						}
					}
					optOnDate.Content += "of every month";
					break;
				default:
					break;
			}

		}


		private void setDayContent()
		{
			Int16 dw = dtNextDue.Value.Value.DayOfWeek;
			Int16 d = dtNextDue.Value.Value.Day;
			Int16 doord = d / 8;

			lstDayOrdinal.SelectedIndex = doord;
			lstDayOfWeek.SelectedIndex = dw;

			//setDate()

		}


		private void setDate()
		{
			if (!loadingFlag) {
				Int16 dw = lstDayOfWeek.SelectedIndex;
				Int16 doord = lstDayOrdinal.SelectedIndex;
				Int16 firstDay = DateSerial(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value), 1).DayOfWeek;
				dtNextDue.Value = DateSerial(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value), (((doord) * 7) + 1) + (firstDay <= dw ? dw - firstDay : 7 - (firstDay - dw)));
			}

		}


		private void setEndOn()
		{
			if (sched.noEndDate) {
				lblEndOn.Content = "no end date";
				sched.setend_date(12/31/2999 00:00:00);
			} else if (sched.getnumber_to_process > 0) {
				lblEndOn.Content = string.Format("after {0} times", sched.getnumber_to_process);
				sched.setend_date(12/31/2999 00:00:00);
			} else {
				lblEndOn.Content = Strings.Format(sched.getend_date, "MM/dd/yyyy");
			}

		}


		private void setUpDown()
		{
			if (!loadingFlag) {
				short d = dtNextDue.Value.Value.Day + 1;
				upDnThisOne.Text = Strings.Format(d, "##");
				upDnThatOne.Text = Strings.Format((d + 14 <= System.DateTime.DaysInMonth(dtNextDue.Value.Value.Year, dtNextDue.Value.Value.Month) ? d + 14 : System.DateTime.DaysInMonth(dtNextDue.Value.Value.Year, dtNextDue.Value.Value.Month)), "##");
				upDnThisTwo.Text = Strings.Format(d, "##");
			}

		}

		// Handles dtNextDue.ValueChanged
		private void dtNextDue_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

			if (loadingFlag)
				return;


			if (!dayContentFlag) {
				dateChangeFlag = true;
				setDayContent();
			}

			setNextDateContent();

			dateChangeFlag = false;

		}


		private void lstDayOrdinal_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!dateChangeFlag & !loadingFlag) {
				RadioButton opt = (RadioButton)oftenOpts(HowOften.Monthly).Children(1);
				opt.IsChecked = true;
				dayContentFlag = true;
				setDate();
			}
			dayContentFlag = false;

		}


		private void lstDayOfWeek_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!dateChangeFlag & !loadingFlag) {
				RadioButton opt = (RadioButton)oftenOpts(HowOften.Monthly).Children(1);
				opt.IsChecked = true;
				dayContentFlag = true;
				setDate();
			}
			dayContentFlag = false;

		}


		private void cmdChange_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			frmEndOn frm = new frmEndOn {
				noEndDate = sched.noEndDate,
				endOn = sched.getend_date,
				numberOfTimes = sched.getnumber_to_process
			};
			frm.ShowDialog();
			sched.setend_date(frm.endOn);
			sched.setnumber_to_process(frm.numberOfTimes);
			sched.noEndDate = frm.noEndDate;
			frm.Close();
			frm = null;
			setEndOn();

		}


		private void optLastDay_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			dayContentFlag = true;
			dtNextDue.Value = DateSerial(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value), System.DateTime.DaysInMonth(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value)));
			//dtNextDue.DisplayDate = DateSerial(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value), Date.DaysInMonth(Year(dtNextDue.Value.Value), Month(dtNextDue.Value.Value)))
			dayContentFlag = false;

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

		private void frmSchedule_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//dtTime.Time = Format(Now(), "hh:mm tt")
			lstHowOften.ItemsSource = often;
			setOftenOpts();
			dtNextDue.Value = Now();
			//dtNextDue.Value = Now()
			if (scheduleId == 0) {
				sched = new schedules();
				sched.lastProcessed = 1/1/1900 00:00:00;
			} else {
				sched = schedules.load(scheduleId);
				if ((sched == null)) {
					Interaction.MsgBox("Schedule <" + scheduleId + "> not found");
					this.Close();
				}
				setUpExistingSchedule();
			}
			setEndOn();
			loadingFlag = false;


		}


		private void dtNextDue_ValueChanged1(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
		{
			if (loadingFlag)
				return;


			if (!dayContentFlag) {
				dateChangeFlag = true;
				setDayContent();
			}

			setNextDateContent();

			dateChangeFlag = false;

		}
		public frmSchedule()
		{
			Loaded += frmSchedule_Loaded;
			Initialized += frmSchedule_Initialized;
		}
	}
}
