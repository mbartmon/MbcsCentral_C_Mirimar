using System;
using System.Windows;
using System.Collections.Generic;
using MbcsWpfControls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Input;
using System.IO;
using Microsoft.VisualBasic;
using System.Diagnostics;
using ExtensionMethods;
using System.Windows.Threading;
using Vintasoft.Twain;

namespace MbcsCentral.WinPM
{
	public partial class frmTenant
	{

		protected Tenants tenant;
        protected Tenants tenantOriginal;
		protected Occupants[] occupant;
		protected Legal[] legals;
        protected Email.EmailMessage[] emailMessages;
		protected int curOccupant;
		protected int curLegal;
        protected bool Alerts;
        protected bool Scan;
        protected string WinDocPath;
        System.Windows.Data.CollectionViewSource FutureRentsView;
        FutureRents[] futures = new FutureRents[0];
		protected TenantCalcFields[] TenantCalcSource = new TenantCalcFields[1];
		// Properties
		protected string m_buildingNumber;
		protected string m_tenantNumber;
		protected int m_sequenceNumber;
		protected bool m_Add = false;
        protected DateTime OrigExpirationDate = GlobalShared.NULL_DATE;

        //List<TopCategory> TopCat;

        string mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.TenantDirect];

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmTenant()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }
        public frmTenant(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += Window_Loaded;
        }
		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            GlobalShared.Log.Log((int)LogClass.logType.Info, "frmTenant loaded.",false);
            Main.getForm(this);
            GlobalShared.IsChanged = false;
            Control[] control = Control.load();
            if (control != null && control.Length > 0)
            {
                Alerts = control[0].alertMessages;
                Scan = control[0].scan;
                if (Scan)
                {
                    WinDocPath = control[0].WindocDatabase;
                    Scan = Main.VerifyWindocLocation(WinDocPath);
                    /*
                    if (WinDocPath == null || WinDocPath.Trim().Equals(""))
                        Scan = false;
                    else
                    {
                        WinDocPath = WinDocPath.Substring(0, WinDocPath.LastIndexOf("\\"));

                        int xtimestimeout = 5;
                        while (!Directory.Exists(WinDocPath) && (xtimestimeout > 0))
                        {
                            System.Threading.Thread.Sleep(3000);
                            xtimestimeout -= 1;
                        }
                        if (!Directory.Exists(WinDocPath))
                        {
                            MessageBox.Show("Unable to connect to WinDOC at <" + WinDocPath + ">");
                            Scan = false;
                        }
                    }
                    */
                }
            }
            control = null;

            System.Windows.Data.CollectionViewSource TenantCalcView = (System.Windows.Data.CollectionViewSource)this.FindResource("TenantCalcView");
			TenantCalcSource[0] = new TenantCalcFields();
			TenantCalcView.Source = TenantCalcSource;

            ChargeCodes[] LegalCodes = ChargeCodes.loadLegal(1,false,false,true);
            CollectionViewSource LegalCodeSource = (CollectionViewSource)this.FindResource("LegalCodesSource");
            LegalCodeSource.Source = LegalCodes;

            try
            {
                if (m_Add)
                {
                    this.Title = "ADD Tenant Record";
                    m_sequenceNumber = 0;
                    tenant = new Tenants();
                    tenant.ClientId = GlobalShared.user.Account.clientId;
                    Alerts = false;
                    Scan = false;
                    txtBuildingNumber.Focus();
                }
                else
                {
                    this.Title = "EDIT Tenant Record";
                    tenant = Tenants.load(m_sequenceNumber, true);
                    if ((tenant != null))
                    {
                        this.Title = String.Format("Tenant Record for: {0}-{1} {2} Apartment: {3}", tenant.BuildingNumber, tenant.TenantNumber, tenant.TenantName, tenant.Apartment);      
                        if (!Information.IsDBNull(tenant.LeaseExpirationDate))
                        {
                            // Highlight Lease Expiration Date if needed
                            if (tenant.LeaseExpirationDate > MbcsCentral.GlobalShared.NULL_DATE)
                            {
                                DateTime date = DateTime.Now;
                                if (DateTime.Compare(tenant.LeaseExpirationDate, date) < 0)
                                    LeaseFrame.BorderBrush = System.Windows.Media.Brushes.Red;  // '& vbblink;
                                else
                                    if (DateTime.Compare(tenant.LeaseExpirationDate, date.AddDays(30)) <= 0)
                                        LeaseFrame.BorderBrush = System.Windows.Media.Brushes.Yellow;
                                    else
                                        if (DateTime.Compare(tenant.LeaseExpirationDate, date.AddDays(90)) <= 0)
                                            LeaseFrame.BorderBrush = System.Windows.Media.Brushes.Green;
                           }
                            OrigExpirationDate = tenant.LeaseExpirationDate;
                       }
                        FutureRentsView = (System.Windows.Data.CollectionViewSource)this.FindResource("FutureRentsView");
                        futures = FutureRents.loadBySequence(tenant.SequenceNumber);
                        FutureRentsView.Source = futures;
                        // Populate grids
                        occupant = Occupants.loadBySequenceNumber(tenant.SequenceNumber);
                        if ((occupant != null))
                        {
                            grdOccupants.DataContext = occupant;
                        }
                        GetLegals();
                        emailMessages = Email.Utils.GetEmails(GlobalShared.appPath + GlobalShared.EMAIL_FOLDER + GlobalShared.EMAIL_FOLDER_OUT, "*", tenant.SequenceNumber, 0);
                        lstOutgoingEmails.ItemsSource = emailMessages;
                        TenantCalcs();
                        tenantOriginal = tenant.Clone();
                        if (Alerts) 
                        {
                            //showAlerts = False
                            //Call GetAlerts(tblTenant("Sequence Number"), nts)
                           // Call FillAlerts
                           // Call setViewed(tblTenant("Sequence Number"))
                        }
                        else
                        {
                            tabAlerts.IsEnabled = false;
                        }
                        if (Scan)
                        {
                            lstDocuments.ItemsSource = null;
                            WinDocRetrieval.getDocuments(tenant, lstDocuments, chkAll.IsChecked.Value, chkAllInv.IsChecked.Value);
                        }
                        txtTenantName.Focus();
                        
                    }
                    else
                    {
                        MessageBox.Show("Tenant not found.");
                        this.Close();
                    }
                    GetBillingName();
                }
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
                GlobalShared.Log.Log((int)LogClass.logType.Debug, ex.StackTrace, false);
                this.Close();
            }
            tenant.isChanged = false;
			MainGrid.DataContext = tenant;
            chkNotification_Checked(null, new RoutedEventArgs());
            //if (!Scan)
            //    tabDocuments.IsEnabled = false;

        }

        private void GetLegals()
        {
            legals = Legal.loadById(tenant.SequenceNumber);
            if ((legals != null))
            {
                for (int i = 0; i <= legals.Length - 1; i++)
                {
                    legals[i].AnLPChildrenGrp += i;
                    legals[i].AnWGChildrenGrp += i;
                    legals[i].AnWGInstalledGrp += i;
                    legals[i].AnWGMaintenanceGrp += i;
                    legals[i].AnWGWantGrp += i;
                    legals[i].RnWGWantGrp += i;
                    legals[i].RnLPChildrenGrp += i;
                    legals[i].RnWGChildrenGrp += i;

                }
                grdLegal.ItemsSource = legals;
            }

        }
        void GetBillingName()
        {
            if (tenant != null)
            {
                Types t = Types.load(GlobalShared.user.Account.clientId, tenant.Type.Trim());
                if (t != null && t.BillingName.Trim() != "")
                    MonthlyRent.Content = t.BillingName;
            }

        }
		protected DataGridTemplateColumn createRadioButtonControl(string colName, long id)
		{

			Binding bi = new Binding("IsChecked");
			FrameworkElementFactory fac = new FrameworkElementFactory(typeof(RadioButton));
			DataTemplate cellTemp = new DataTemplate();
			PropertyPath propPath = null;
			var col = new DataGridTemplateColumn();
			bi.Mode = BindingMode.TwoWay;
			propPath = new PropertyPath(colName, null);
			bi.Path = propPath;
			fac.SetValue(RadioButton.IsCheckedProperty, bi);
			fac.SetValue(RadioButton.GroupNameProperty, string.Format("{0}_{1}", colName, id));
			cellTemp.VisualTree = fac;
			col.CellTemplate = cellTemp;
			return col;

		}

		private void saveTenant(object sender, RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            if (!m_Add)
            {
                if (!tenantOriginal.TenantName.Trim().Equals(tenant.TenantName.Trim()) || 
                    !tenantOriginal.Apartment.Trim().Equals(tenant.Apartment.Trim()) ||
                    !tenantOriginal.TenantNumber.Trim().Equals(tenant.TenantNumber.Trim()) ||
                    !tenantOriginal.BuildingNumber.Trim().Equals(tenant.BuildingNumber.Trim()))
                {
 	 	 	 		if (MessageBoxCustom.Show("Building Number and/or Tenant Number and/or Name and/or Apartment has changed.\r\n\n" + 
                        "Original Building: <" + tenantOriginal.BuildingNumber + ">, New Building: <" + tenant.BuildingNumber +">\r\n" +
                        "Original Number: <" + tenantOriginal.TenantNumber + ">, New Number: <" + tenant.TenantNumber + ">\r\n" + 
                        "Original Name: <" + tenantOriginal.TenantName + ">, New Name: <" + tenant.TenantName + ">\r\n" + 
                        "Original Apartment <" + tenantOriginal.Apartment + ">, New Apartment: <" + tenant.Apartment + ">\r\n\n" + 
                        "Do you want to save the changes anyway?", "Do you want to save the changes anyway?  ", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
 	 	 	 		    return;
 	 	 	 	} 
            }
            /*  Deprecated
            for (int i = 1; i < 4; i++)
            {
                if ((decimal)tenant["FutureRent" + i.ToString()] == 0)
                {
                    tenant["FutureLegal" + i.ToString()] = (decimal)0;
                    tenant["FutureDate" + i.ToString()] = GlobalShared.NULL_DATE;
                }
            }
             */
            if (futures != null && futures.Length > 0)
            {
                foreach (FutureRents future in futures)
                    future.save();
            }
            /* May not be necessary 4/4/16
            if (tenant.isHighRentVacancyPeriodMonthly)
                tenant.ExemptHighRentVacanyPeriod = 0;
            else
                tenant.ExemptHighRentVacanyPeriod = 1;
                */

            if (tenant.LeaseExpirationDate != OrigExpirationDate)
                tenant.LeaseLetterFlag = false;
            tenant.save();

            if (m_Add)
            {
                // Create new histmast
                Histmast hist = new Histmast();
                hist.BuildingNumber = tenant.BuildingNumber;
                hist.TenantNumber = tenant.TenantNumber;
                hist.TenantName = tenant.TenantName;
                hist.SequenceNumber = tenant.SequenceNumber;
                hist.Apartment = tenant.Apartment;
                hist.save();

                // Setup initial histchg
                //
                // If move in date is current month, create history rent charge
                if (tenant.MoveinDate.Month == DateTime.Now.Month && tenant.MoveinDate.Year == DateTime.Now.Year)
                {
                    HistChg histchg = new HistChg();
                    histchg.BuildingNumber = tenant.BuildingNumber;
                    histchg.TenantNumber = tenant.TenantNumber;
                    histchg.SequenceNumber = tenant.SequenceNumber;
                    histchg.Amount = tenant.Rent;
                    histchg.Arrears = 0;
                    histchg.ChargeDate = DateTime.Now;
                    histchg.Code = "=";
                    histchg.ChargeNumber = 0;
                    histchg.save();
                }

                // if stFlag create new Security Tracker records
            }
			if ((occupant != null)) {
				foreach (Occupants o in occupant) {
					if (o.isChanged)
                    {
                        o.ID = tenant.SequenceNumber;
						o.save();
                    }
				}
			}
			if ((legals != null)) {
				foreach (Legal l in legals) {
					if (l.isChanged)
                    {
                        l.ID = tenant.SequenceNumber;
						l.save();
                    }
				}
			}
			this.Close();

		}


		private void exitApp(object sender, RoutedEventArgs e)
		{
            if (GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                if ((tenant != null) && tenant.isChanged)
                {
				    switch (Interaction.MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)) {
					    case MsgBoxResult.Yes:
						    tenant.save();
						    break;
					    case MsgBoxResult.Cancel:
						    return;

						    break;
					    case MsgBoxResult.No:

						    break;
				    }
			    }
            }
            GlobalShared.IsChanged = true;
            if (tenant != null)
                WinpmGlobal.AddToLastTenantsList(tenant.BuildingNumber, tenant.TenantNumber, tenant.Apartment, tenant.SequenceNumber);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() => Main.FillLastTenantsInMenu()));
            //Main.FillLastTenantsInMenu();
            Main.saveForm(this);
			this.Close();

		}


		private void amountLostFocus(object sender, RoutedEventArgs e)
		{
			TenantCalcs();
            if (m_Add && tenant != null)
            {
                if (txtLegalRent.Value == 0)
                    tenant.RentLegal = tenant.Rent;
            }
		}


		private void TenantCalcs()
		{
			    if ((tenant == null))
				return;
			try {
				decimal chgCurrent = 0;
				decimal chgArrears = 0;
				decimal chgCurOne = 0;
				decimal chgArrOne = 0;
				decimal chgCurRec = 0;
				decimal chgArrRec = 0;
				decimal chgPaid = default(decimal);
				decimal chgPaidOne = default(decimal);
				decimal chgPaidRec = default(decimal);
				//txtRentTotal.Text = tenant.Rent + tenant.Arrears
				TenantCalcSource[0].TotalRent = tenant.Rent + tenant.Arrears;
				if (!m_Add) {
					WinpmGlobal.Charges_get(tenant.SequenceNumber, ref chgCurrent, ref chgArrears, ref chgPaid, ref chgCurOne, ref chgArrOne, ref chgPaidOne, ref chgCurRec, ref chgArrRec, ref chgPaidRec);
				}
				// One-Time Charges
				TenantCalcSource[0].CurrentOneCharges = chgCurOne;
				TenantCalcSource[0].ArrearsOneCharges = chgArrOne;
				TenantCalcSource[0].TotalOneCharges = chgCurOne + chgArrOne;
				// Recurring Charges
				TenantCalcSource[0].CurrentRecCharges = chgCurRec;
				TenantCalcSource[0].ArrearsRecCharges = chgArrRec;
				TenantCalcSource[0].TotalRecCharges = chgCurRec + chgArrRec;

				TenantCalcSource[0].CurrentTotal = tenant.Rent + chgCurrent;
				TenantCalcSource[0].ArrearsTotal = tenant.Arrears + chgArrears;
				TenantCalcSource[0].TotalTotal = TenantCalcSource[0].TotalRent + chgCurrent + chgArrears;
				TenantCalcSource[0].Balance = TenantCalcSource[0].TotalTotal - tenant.Paid;
			} catch (System.Exception ex) {
			}


        }


        public void SetDateIfNull(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DatePicker dt = (DatePicker)sender;

			if (DateAndTime.DateValue(dt.Text) == new DateTime(1900,1,1,0,0,0)) {
				dt.Text = DateAndTime.Now.Date.ToString();
			}

		}

        /*
		public void MoveInChanged(object sender, RoutedEventArgs e)
		{
			DatePicker obj = (DatePicker)sender;
			if (DateTime.Parse(obj.Text) > MbcsCentral.GlobalShared.NULL_DATE) {
				//chkMoveIn.IsChecked = true;
			}
		}

		public void LeaseStartChanged(object sender, RoutedEventArgs e)
		{
			DatePicker obj = (DatePicker)sender;
			if (DateTime.Parse(obj.Text) > MbcsCentral.GlobalShared.NULL_DATE) {
				chkLeaseStart.IsChecked = true;
			}
		}

		public void LeaseExpiresChanged(object sender, RoutedEventArgs e)
		{
			DatePicker obj = (DatePicker)sender;
			if (DateTime.Parse(obj.Text) > MbcsCentral.GlobalShared.NULL_DATE) {
				chkLeaseExpires.IsChecked = true;
			}
		}
        */

		public void AddOccupantsClick(object sender, RoutedEventArgs e)
		{
			grdOccupants.DataContext = null;
			Array.Resize(ref occupant, occupant.Length + 1);
			occupant[occupant.Length - 1] = new Occupants();
			occupant[occupant.Length - 1].ID = tenant.SequenceNumber;
			occupant[occupant.Length - 1].isChanged = false;
            occupant[occupant.Length - 1].DOB = DateTime.Now;
            grdOccupants.DataContext = occupant;

		}


		public void AddLegalClick(object sender, RoutedEventArgs e)
		{
			grdLegal.ItemsSource = null;
			Array.Resize(ref legals, legals.Length + 1);
            int i = legals.Length - 1;
            legals[i] = new Legal();
			legals[i].ID = tenant.SequenceNumber;
			legals[i].isChanged = false;
            legals[i].Year = (short)DateTime.Now.Year;
            legals[i].AnLPChildrenGrp += i;
            legals[i].AnWGChildrenGrp += i;
            legals[i].AnWGWantGrp += i;
            legals[i].RnWGWantGrp += i;
            legals[i].AnWGInstalledGrp += i;
            legals[i].AnWGMaintenanceGrp += i;
            legals[i].RnLPChildrenGrp += i;
            legals[i].RnWGChildrenGrp += i;
            grdLegal.ItemsSource = legals;

		}

        private void grdLegal_LostFocus(object sender, RoutedEventArgs e)
        {
            IInputElement ctrl = FocusManager.GetFocusedElement(this) as IInputElement;
            if (ctrl != null && ctrl.GetType() != typeof(DataGridCell))
                MessageBox.Show("outside!");
        }
        public void grdOccupantsSelectionChanged(object sender, RoutedEventArgs e)
		{
			curOccupant = grdOccupants.SelectedIndex;
		}

		public void grdLegalDeleteClick(object sender, RoutedEventArgs e)
		{
            curLegal = grdLegal.SelectedIndex;
            if (curLegal >= 0)  
            {
                MessageBoxResult rc = MessageBox.Show("Delete this year?","",MessageBoxButton.YesNo);
                if (rc == MessageBoxResult.Yes)
                {
                    Legal l = (Legal)grdLegal.Items[curLegal];
                    if (l.delete())
                    {
                        grdLegal.ItemsSource = null;
                        GetLegals();
                    }
                }
			}
		}
		public void grdLegalSelectionChanged(object sender, RoutedEventArgs e)
		{
			curLegal = grdLegal.SelectedIndex;
		}

		public void deleteOccupantClick(object sender, RoutedEventArgs e)
		{
			if (curOccupant >= 0 && Interaction.MsgBox("Delete this occupant?",MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
				Occupants o = (Occupants)grdOccupants.Items[curOccupant];
				if (o.delete()) {
                    occupant = Occupants.loadBySequenceNumber(tenant.SequenceNumber);
                    if ((occupant != null))
                    {
                        grdOccupants.DataContext = null;
                        grdOccupants.DataContext = occupant;
                    }
                    //grdOccupants.Items.RemoveAt(curOccupant);
				}
			}

		}

		public string buildingNumber {
			get { return m_buildingNumber; }
			set { m_buildingNumber = value; }
		}
		public string tenantNumber {
			get { return m_tenantNumber; }
			set { m_tenantNumber = value; }
		}
		public int sequenceNumber {
			set { m_sequenceNumber = value; }
		}
		public bool Add {
			set { m_Add = value; }
		}

        /* Can be deleted
		private void dtMoveIn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DatePicker dt = (DatePicker)sender;

			if (DateAndTime.DateValue(dt.Text) == new DateTime(1900,1,1,0,0,0)) {
				dt.Text = DateAndTime.Now.Date.ToString();
			}


		}
        */

		private void Date_PreviewMouseDown(object sender,EventArgs e)
		{
            if (sender.GetType() == typeof(DatePickerEx))
            {
                DatePickerEx dt = (DatePickerEx)sender;
                if (dt.DisplayDate <= MbcsCentral.GlobalShared.NULL_DATE)
                {
                    dt.DisplayDate = DateAndTime.Now.Date;
                }
            }
            else
            {
                DatePicker dt = (DatePicker)sender;
                if (dt.DisplayDate <= MbcsCentral.GlobalShared.NULL_DATE)
                {
                    dt.DisplayDate = DateAndTime.Now.Date;
                }

            }


        }

        /* Can be deleted
        private void DhcrDate_PreviewMouseDown(object sender, EventArgs e)
        {
            DatePickerEx dt = (DatePickerEx)sender;

            if (dt.DisplayDate <= MbcsCentral.GlobalShared.NULL_DATE)
            {
                dt.DisplayDate = DateAndTime.Now.Date;
            }

        }
        */

        public void grdLegal_LoadingRow(System.Object sender, System.Windows.Controls.DataGridRowEventArgs e)
		{
			DataGridRow dr = e.Row;


			if ((sender) is RadioButton) {
				RadioButton rb = (RadioButton)sender;
				if (!string.IsNullOrEmpty(rb.GroupName.Trim())) {
					rb.GroupName += "1";
				}
			}

		}


		private void RadioButton_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb = (RadioButton)sender;

			BindingExpression be = GetBindingExpression(RadioButton.IsCheckedProperty);

		}

		private void RadioButton_Checked(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb = (RadioButton)sender;

			BindingExpression be = GetBindingExpression(RadioButton.IsCheckedProperty);

		}


		private void txtTenantNumber_LostFocus(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (m_Add) {
				Tenants[] ten = Tenants.load(txtBuildingNumber.Text, txtTenantNumber.Text, GlobalShared.user.Account.clientId);
				if ((ten != null) && ten.Length > 0) {
					if (MessageBox.Show("There is an existing Tenant: " + ten[0].TenantName + " - " + ten[0].Apartment + ".  Do you wish to replace this tenant?", "Tenant Table", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
						tenant.SequenceNumber = 0;
						tenant.ReplacingTenant = ten[0].SequenceNumber;
						tenant.Apartment = ten[0].Apartment;
						tenant.Rooms = ten[0].Rooms;
						tenant.SquareFeet = ten[0].SquareFeet;
						tenant.NewTenantFlag = true;
						txtTenantName.Focus();
					} else {
						this.Close();
					}
				}
             
			}

		}


		private void cmdImpMaint_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtBuildingNumber.Text.Trim()) & !string.IsNullOrEmpty(txtTenantNumber.Text.Trim())) {
				frmMaintenance frm = new frmMaintenance(m_ThisApp);
				frm.BuildingNumber = txtBuildingNumber.Text;
				frm.TenantNumber = txtTenantNumber.Text;
				frm.TenantId = tenant.SequenceNumber;
				frm.Show();
			}
		}


		private void cmdChargesDetail_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (m_Add)
                tenant.save();
			frmCharges frm = new frmCharges(m_ThisApp);
			frm.Tenant = tenant;
			frm.ShowDialog();
            TenantCalcs();
		}

        private void dtDhcrDateOne_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            DatePicker dt = (DatePicker)sender;

            if (DateAndTime.DateValue(dt.Text) == new DateTime(1900, 1, 1, 0, 0, 0))
            {
                dt.DisplayDate = DateAndTime.Now.Date;
            }

        }

        private void cmdPaidDetail_Click_1( object sender, RoutedEventArgs e )
        {
            frmPaymentDetails frm = new frmPaymentDetails();
            frm.TenantId = tenant.SequenceNumber;
            frm.TenantName = tenant.TenantName;
            frm.ShowDialog();
        }

        private void CmdHistory_OnClick(object sender, RoutedEventArgs e)
        {
            frmHistoryDisplay frm = new frmHistoryDisplay();
            frm.TenantId = tenant.SequenceNumber;
            frm.Show();

        }

        private void dtMoveIn_PreviewMouseDown(object sender, EventArgs e)
        {
            DatePickerEx dt = (DatePickerEx)sender;

            if (dt.DisplayDate <= MbcsCentral.GlobalShared.NULL_DATE)
            {
                dt.DisplayDate = DateAndTime.Now.Date;
            }
        }

        private void LeaseStartChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LeaseExpiresChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            //Main.PrintWindow(this, System.Printing.PageOrientation.Landscape);
            Main.PrintWindowBackground(this, System.Printing.PageOrientation.Landscape);
        }

        private void chkDhcrDate_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (!chk.IsChecked.Value)
            {
                switch (chk.Name)
                {
                    case "chkDhcrDateOne":
                        dtDhcrDateOne.SelectedDate = GlobalShared.NULL_DATE;
                        dtDhcrDateOne.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        break;
                    case "chkDhcrDateTwo":
                        dtDhcrDateTwo.SelectedDate = GlobalShared.NULL_DATE;
                        dtDhcrDateTwo.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        break;
                    case "chkExemptionEffectiveDate":
                        dtExemptionEffectiveDate.SelectedDate = GlobalShared.NULL_DATE;
                        dtExemptionEffectiveDate.Text = GlobalShared.NULL_DATE.ToShortDateString();
                        break;
                }
            }
        }

        private void chkLateFee_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (!chk.IsChecked.Value)
                tenant.LateFee = 0;
        }

        private void txtType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!txtType.Text.Trim().Equals(""))
                GetBillingName();
        }
        private void txtBuildingNumber_LostFocus(object sender, RoutedEventArgs e)
        {

            Building bld = null;
            if (txtBuildingNumber.Text != null && txtBuildingNumber.Text.Trim() != "")
                bld = Building.loadByBuildingNumber(txtBuildingNumber.Text, GlobalShared.user.Account.clientId);
            if (bld == null)
            {
                MessageBox.Show("Invalid Building Number entered.");
                txtBuildingNumber.Text = "";
                txtBuildingNumber.Focus();
            }
            else
                bld = null;
        }

         /// <summary>
         /// This is used to avoid having tenant.IsChanged
         /// to be true after initial display of tenant record.
         /// Was happening due to some necessary date field manipulations
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void formTenant_ContentRendered(object sender, EventArgs e)
        {
            if (tenant != null)
                tenant.isChanged = false;

        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            int idx = grdFutureRents.SelectedIndex;
            if (idx >= 0)
            {
                if (MessageBox.Show("Delete this Future Rent?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    FutureRents future = (FutureRents)grdFutureRents.SelectedItem;
                    future.delete(future.ID.ToString());
                }
            }
            futures = FutureRents.loadBySequence(tenant.SequenceNumber);
            FutureRentsView.Source = futures;
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            Array.Resize<FutureRents>(ref futures, (futures == null ? 1 : futures.Length + 1));
            futures[futures.Length - 1] = new FutureRents();
            futures[futures.Length - 1].FutureDate = DateTime.Now;
            futures[futures.Length - 1].TenantSequence = tenant.SequenceNumber;
            FutureRentsView.Source = futures;
        }

        private void lstDocuments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RetrieveDocument item = (RetrieveDocument)lstDocuments.SelectedItem;
            //TreeViewItem item = (TreeViewItem)lstDocuments.SelectedItem;
            if (item != null && item.DocumentId > 0 && !item.IsRoot && !item.DisplayName.Trim().Equals(""))
                //if (item != null && !TreeViewItemProps.GetIsRootLevel(item) && item.Tag != null)
            {
                Process exProc;
                exProc = new System.Diagnostics.Process();
                string path = WinDocPath.Replace("\\windoc.mdb", "");
                exProc.StartInfo.FileName = path + "\\WindocViewer.exe";
                exProc.StartInfo.Arguments = item.DocumentId.ToString();
                //exProc.EnableRaisingEvents = true;
                //exProc.Exited += new System.EventHandler(ProcessExited);
                try
                {
                    exProc.Start();
                }
                catch (System.Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Loading WindocViewer < " + WinDocPath + "\\WindocViewer.exe" + ">: " + ex.Message,System.Windows.Forms.MessageBoxButtons.OK);
                }

            }
        }

        private void chkAllInv_Click(object sender, RoutedEventArgs e)
        {
            lstDocuments.ItemsSource = null;
            WinDocRetrieval.getDocuments(tenant, lstDocuments, (bool)chkAllInv.IsChecked.Value, (bool)chkAll.IsChecked.Value);
        }

        private void chkAll_Click(object sender, RoutedEventArgs e)
        {
            lstDocuments.ItemsSource = null;
            WinDocRetrieval.getDocuments(tenant, lstDocuments, (bool) chkAllInv.IsChecked.Value, (bool)chkAll.IsChecked.Value);

        }

        private void tabTenant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tabDHCR.IsSelected)
                {
                    
                    GlobalShared.IsChanged = tenant.isChanged;
                }
            }
            catch { }

        }

        private void cmdSecurity_Click(object sender, RoutedEventArgs e)
        {
            frmSecTrans frm = new frmSecTrans(m_ThisApp) { SequenceNumber = tenant.SequenceNumber };
            frm.ShowDialog();
        }

        private void cmdMail_Click(object sender, RoutedEventArgs e)
        {
            string message;
            Email.SmtpConfig[] smtps = Email.SmtpConfig.loadByAccount((int)GlobalShared.user.Account.accountId, mailTag);
            Email.SmtpConfig smtp = null;
            string emailName = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\EmailNames\\", "General", " ");
            if (emailName == null || emailName.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Email Name is not configured in WimPM Options.");
                return;
            }
            string fromEmail = "";
            if (smtps == null || smtps.Length == 0)
            {
                if (emailName != null && !emailName.Trim().Equals(""))
                {
                    smtp = Email.SmtpConfig.loadByDescription(emailName);
                    if (smtp != null)
                    {
                        fromEmail = GlobalShared.user.Account.email;
                        Array.Resize(ref smtps, 1);
                        smtps[0] = smtp; 
                    }
                }
            }
            if (smtps == null || smtps.Length == 0)
            { 
                if (!Email.Utils.GetEmailConfig(mailTag, out message, out smtp, out fromEmail))
                {
                    MessageBox.Show("Unable to process Email message: " + message);
                    return;
                }
            }
            string[] fromAddresses = new string[smtps.Length];
            string[] displayNames = new string[smtps.Length];
            int index = 0;
            for (int s = 0; s < smtps.Length; s++) 
            {
                fromAddresses[s] = smtps[s].emailAddress;
                displayNames[s] = smtps[s].DisplayName;
            }
            if (smtps.Length > 1)
            {
                Email.frmChooseEmailAccount frm = new Email.frmChooseEmailAccount() { emails = smtps };
                frm.ShowDialog();
                if (frm.ID == -1)
                    return;
                index = frm.ID;
                smtp = smtps[index];
            }
            smtp = smtps[index];
            fromEmail = fromAddresses[index];
            MbcsMail.Smtp.Config smtpConfig = new MbcsMail.Smtp.Config() { description = smtp.description, emailAddress = fromEmail, userName = smtp.userName, password = smtp.password, server = smtp.server, port = smtp.port, SSL = smtp.SSL, TSL = smtp.TSL, authentication = smtp.authentication };
            string[] ToAddresses = { tenant.Email1, tenant.Email2 };
            try
            {
                TwainGlobalSettings twainGlobalSettings1 = new TwainGlobalSettings();
                twainGlobalSettings1.Register(GlobalShared.VINTASOFT_USER, GlobalShared.VINTASOFT_EMAIL, GlobalShared.VINTASOFT_REGCODE);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Unable to register VintaSoft Twain. Scanning will not be available: " + ex.Message);
            }

            MbcsMail.frmContent msgForm = new MbcsMail.frmContent();
            msgForm.Smtp = smtpConfig;
            msgForm.FromAddress = new string[] { fromAddresses[index] };
            msgForm.ReplyTo = fromEmail;
            msgForm.ToAddress = ToAddresses;
            msgForm.DisplayName = displayNames[index];
            msgForm.ShowDialog();
            if (msgForm.Success)
            {
                string fileName = GlobalShared.appPath + "Emails\\Out\\" + String.Format(GlobalShared.mailFilename, WinpmGlobal.EmailTypeFilenames[(int)WinpmGlobal.EmailTypes.TenantDirect] + "_", tenant.SequenceNumber, 0, DateTime.Now, "pdf");
                Email.Utils.SaveEmail(fromEmail, displayNames[index], ToAddresses[index], msgForm.Subject, msgForm.Content, fileName.Replace(".pdf", ".txt"), DateTime.Now.ToShortDateString(), msgForm.ReturnReceipt);
            }
        }

        private void chkHighRentVacancy_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.IsChecked.Value)
            {
                optHRMonthly.IsChecked = true; // Set to Monthly
                tenant.isHighRentVacancyPeriodMonthly = true;
            }
            
        }

        private void cmdMailConfig_Click(object sender, RoutedEventArgs e)
        {
            Email.frmSmtpConfigurator frm = new Email.frmSmtpConfigurator() { AccountId = (int)GlobalShared.user.Account.accountId };
            frm.ShowDialog();
        }

        private void ctxEmailDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBox lst = lstOutgoingEmails;
            if (lst != null && lst.SelectedIndex > -1)
            {
                if (MessageBox.Show("Delete " + lst.SelectedItems.Count.ToString() + " email message(s)?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Email.EmailMessage msg;
                    for (int i = 0; i < lst.SelectedItems.Count; i++)
                    {
                        msg = (Email.EmailMessage)lst.SelectedItems[i];
                        if (msg != null && !msg.FileName.Trim().Equals(""))
                        {
                            File.Delete(msg.FileName);
                        }
                    }
                    lstOutgoingEmails.ItemsSource = null;
                    emailMessages = Email.Utils.GetEmails(GlobalShared.appPath + GlobalShared.EMAIL_FOLDER + GlobalShared.EMAIL_FOLDER_OUT, "*", tenant.SequenceNumber, 0);
                    lstOutgoingEmails.ItemsSource = emailMessages;
                }
            }
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void RadioButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            if (btn.IsChecked.Value)
                btn.IsChecked = false;

        }

        private void txtRentArrears_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            amountLostFocus(sender, new RoutedEventArgs());
        }

        private void dtLeaseExpires_SelectedDateChanged(object sender, EventArgs e)
        {
            /*
            if (!loading)
            {
                tenant.LeaseLetterFlag = false;
                tenant.LowRentFlag = false;
            }
            loading = false;
            */

        }

        private void cmdPrintOutgoingEmails_Click(object sender, RoutedEventArgs e)
        {
            ItemCollection items = lstOutgoingEmails.Items;
            if (items != null && items.Count > 0)
            {
                Font fnt = new Font("Courier New", (float)10.5);
                Report rpt = new Report("Email List", "Courier New", 12);
                rpt.SetFont = fnt;
                rpt.LineHeight = .15 * 96;
                rpt.LeftMargin = (float)5;
                rpt.TopMargin = (float)5;
                rpt.BottomMargin = (float)50;
                rpt.BottomOffset = (float)55;
                rpt.FontSize = 12;
                EmailOutHeading(rpt);
                foreach (Email.EmailMessage message in items)
                {
                    string[] msg = message.Message.Replace('\n', ' ').Replace('\r', ' ').Wrap(64);
                    string s = message.Date.PadRight(12) + message.FromAddress.PadRight(28) + message.Subject.PadRight(35) + "      " + message.EmailType.PadRight(18);
                    if (rpt.PrintPageLine(s))
                    {
                        rpt.NewPage();
                        EmailOutHeading(rpt);

                    }
                    if (rpt.PrintPageLine())
                    {
                        rpt.NewPage();
                        EmailOutHeading(rpt);
                    }
                    for (int i = 0; i < msg.Length; i++)
                        rpt.PrintPageLine(" ".PadLeft(35) + msg[i].Trim());

                    if (rpt.PrintPageLine())
                    {
                        rpt.NewPage();
                        EmailOutHeading(rpt);
                    }
                    else
                    {
                        if (rpt.PrintPageLine(new string('=', 100)))
                        {
                            rpt.NewPage();
                            EmailOutHeading(rpt);
                        }
                    }
                }
                rpt.DisplayDocument();
            }
        }
        private void EmailOutHeading(Report rpt)
        {
            rpt.PrintPageLine("OUTGOING EMAILS for: " + tenant.BuildingTenant + " '" + tenant.TenantName + "' Apt: " + tenant.Apartment + "        " + DateTime.Now.ToShortDateString());
            rpt.PrintPageLine();
            rpt.PrintPageLine("Date".PadRight(12) + "From".PadRight(28) + "Subject".PadRight(35) + "      " + "Type") ;
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string('=', 100));
        }

        private void chkNotification_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSubmittedOne.IsChecked.Value || chkInformedOne.IsChecked.Value)
            {
                dtDhcrNotificationDateOne.IsEnabled = true;
                if (dtDhcrNotificationDateOne.SelectedDate == null)
                {
                    if (dtDhcrNotificationDateOne.DisplayDate != null)
                        dtDhcrNotificationDateOne.SelectedDate = dtDhcrNotificationDateOne.DisplayDate;
                }
                else if (dtDhcrNotificationDateOne.SelectedDate.Value <= GlobalShared.NULL_DATE)
                    dtDhcrNotificationDateOne.SelectedDate = DateTime.Now;
            }
            else
                dtDhcrNotificationDateOne.IsEnabled = false;

            if (chkSubmittedTwo.IsChecked.Value || chkInformedTwo.IsChecked.Value)
            {
                dtDhcrNotificationDateTwo.IsEnabled = true;
                if (dtDhcrNotificationDateTwo.SelectedDate == null)
                {
                    if (dtDhcrNotificationDateTwo.DisplayDate != null)
                        dtDhcrNotificationDateTwo.SelectedDate = dtDhcrNotificationDateTwo.DisplayDate;
                }
                else if (dtDhcrNotificationDateTwo.SelectedDate.Value <= GlobalShared.NULL_DATE)
                    dtDhcrNotificationDateTwo.SelectedDate = DateTime.Now;
            }
            else
                dtDhcrNotificationDateTwo.IsEnabled = false;

        }

        private void dtDhcrNotificationDate_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker dtPicker = sender as DatePicker;
            if (dtPicker.DisplayDateStart == null)
                dtPicker.DisplayDateStart = DateTime.Now.AddYears(-1);
        }

        private void txtABA_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtABA.Text.Trim().Equals(""))
                return;
            bool result = GlobalShared.validateRoutingNumber(txtABA.Text);
            if (!result)
            {
                MessageBoxCustom.Show("Entered ABA Routing Number is invalid.");
                //txtABA.Focus();
            }

        }

        private void chkXrfTested_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (!chk.IsChecked.Value)
                dtXrfTestedDate.SelectedDate = GlobalShared.NULL_DATE;
        }

        private void chkXrfExempt_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (!chk.IsChecked.Value)
                dtXrfExemptDate.SelectedDate = GlobalShared.NULL_DATE;
        }

        private void chkXrfWorkComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (!chk.IsChecked.Value)
                dtXrfWorkCompleteDate.SelectedDate = GlobalShared.NULL_DATE;

        }
    }


    class EmailItem
    {
        public string Date { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
