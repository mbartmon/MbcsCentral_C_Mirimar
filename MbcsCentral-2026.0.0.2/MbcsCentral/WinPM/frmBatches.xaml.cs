using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmBatches.xaml
    /// </summary>
    public partial class frmBatches : Window
    {
        const string FutureRent = "tabFutureRents";
        const string ChargesEdit = "tabChargesEdit";
        const string RentPayments = "tabRentPayments";
        const string ChargesAdd = "tabChargesAdd";
        const string LateFees = "tabLateFees";

        DateTime bildate;

        //string ChargeQuery = "SELECT pm_tenants.[Building Number], pm_tenants.[Tenant Number], [Tenant Name], [Apartment], [Charge Code], [Amount], [Starting Date], [Ending Date], [Future Date], [Future Amount], [Charge Number] FROM pm_tenants INNER JOIN pm_charges ON pm_tenants[Sequence Number] = pm_charges.[Sequence Number]";
        string ChargeQuery;
        string PaymentQuery;
        string[] ops = {"", "<", "<=", "=", ">=", ">" };
        Tenants[] tens;
        Tenants[] tensPay;
        Charges[] chgs;
        Tenants[] tensAdd;
        Tenants[] tensLateFees;

        Classes classChgEdit;
        ChargesEditParms ChgEditParms = null;

        Classes classLateFees;
        LateFeesParms lateFeesParms = null;

        string clsChgEditName = "ChargesEditParms";
        string clsLateFeesName = "LateFeesParms";

        private long clientId = GlobalShared.user.Account.clientId;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmBatches()
        {
            InitializeComponent();
        }

         public frmBatches(string parm)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
            m_ThisApp = (string) parm;
        }

        private void formBatches_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            Control ctrl = Control.load(clientId);
            bildate = ctrl.BillingDate;
            ctrl = null;
            System.Windows.Data.CollectionViewSource ChgCodes = (System.Windows.Data.CollectionViewSource)this.FindResource("ChargeDescriptions");
            ChargeCodes[] ChgDesc = ChargeCodes.loads(clientId, true);
            ChgCodes.Source = ChgDesc;
            System.Windows.Data.CollectionViewSource ChgCodeNotAll = (System.Windows.Data.CollectionViewSource)this.FindResource("ChargeDescriptionsNotAll");
            ChargeCodes[] ChgDescNotAll = ChargeCodes.loads(clientId);
            ChgCodeNotAll.Source = ChgDescNotAll;
            System.Windows.Data.CollectionViewSource TypeCodes = (System.Windows.Data.CollectionViewSource)this.FindResource("TenantTypeSource");
            Types[] TypeDesc = Types.loadAll(clientId, true);
            TypeCodes.Source = TypeDesc;
            grdChgAdd.Columns[4].Visibility = Visibility.Collapsed;

            classChgEdit = Classes.load(GlobalShared.user.Account.accountId, clsChgEditName);
            if (classChgEdit != null)
                ChgEditParms = (ChargesEditParms)ChargesEditParms.DeSerialize(classChgEdit.value);
            else
            {
                classChgEdit = new Classes();
                ChgEditParms = new ChargesEditParms();
            }
            pnlChargeEdit.DataContext = ChgEditParms;
            lstChgEditPaid.SelectedIndex = ChgEditParms.ChargePaidOp;
            lstChgEditDate.SelectedIndex = ChgEditParms.StartDateOp;
            lstChgEditType.SelectedIndex = ChgEditParms.TenantType;
            lstChgEditCharge.SelectedIndex = ChgEditParms.ChargeCode;

            classLateFees = Classes.load(GlobalShared.user.Account.accountId, clsLateFeesName);
            if (classLateFees != null)
            {
                lateFeesParms = (LateFeesParms)LateFeesParms.DeSerialize(classLateFees.value);
                lstLateFeesCodes.SelectedIndex = lateFeesParms.LateFeeChargeCode;
                chkLateFeesAmount.IsChecked = (bool)(lateFeesParms.UseArrearsAmount);
            }
            else
            {
                classLateFees = new Classes();
                lateFeesParms = new LateFeesParms();
            }
            pnlLateFees.DataContext = lateFeesParms;


            lstFRType.SelectedIndex = 0;
            lstRentPayType.SelectedIndex = -1;
            lstChgAddType.SelectedIndex = -1;
            txtFRStartBuilding.Focus();
            txtFRStartBuilding.SelectAll();
            txtChgEditStartBuilding.SelectAll();
            txtRentPayStartBuilding.SelectAll();
            txtChgAddStartBuilding.SelectAll();
            txtLateFeesStartBuilding.SelectAll();
        }

        private void formBatches_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            ChgEditParms.ChargePaidOp = lstChgEditPaid.SelectedIndex;
            ChgEditParms.StartDateOp = lstChgEditDate.SelectedIndex;
            ChgEditParms.TenantType = lstChgEditType.SelectedIndex;
            ChgEditParms.ChargeCode = lstChgEditType.SelectedIndex;
            classChgEdit.value = ChgEditParms.Serialize();
            classChgEdit.clientId = (int)GlobalShared.user.Account.clientId;
            classChgEdit.acctId = GlobalShared.user.Account.accountId;
            classChgEdit.name = clsChgEditName;
            classChgEdit.save();

            lateFeesParms.LateFeeChargeCode = lstLateFeesCodes.SelectedIndex;
            lateFeesParms.UseArrearsAmount = chkLateFeesAmount.IsChecked.Value;
            classLateFees.value = lateFeesParms.Serialize();
            classLateFees.clientId = (int)GlobalShared.user.Account.clientId;
            classLateFees.acctId = GlobalShared.user.Account.accountId;
            classLateFees.name = clsLateFeesName;
            classLateFees.save();
        }

        private void cmdGetTenants_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            TabItem tab = (TabItem)tabBatches.SelectedItem;

            // **************
            // Future Rents *
            // **************
            if (tab.Name.Equals(FutureRent))
            {
                string StartBuilding = txtFRStartBuilding.Text;
                string EndBuilding = txtFREndBuilding.Text;
                if (lstFRType.SelectedIndex < 0)
                    lstFRType.SelectedIndex = 0;
                _busy.Focus();
                _busy.IsBusy = true;
                string TenantType = ((Types)lstFRType.SelectedItem).Type;
                //worker.DoWork += (o, ea) =>
                {
                        tens = Tenants.loadForBatch(StartBuilding, EndBuilding, clientId, TenantType, true);
                    };
                //worker.RunWorkerCompleted += (o, ea) =>
                    {
                        if (tens != null && tens.Length > 0)
                        {
                            grdFutureRents.DataContext = tens;
                            grdFutureRents.UpdateLayout();
                        }
                        else
                        {
                            grdFutureRents.DataContext = null;
                        }
                        _busy.IsBusy = false;
                    };
                //worker.RunWorkerAsync();

            }
            else if (tab.Name.Equals(ChargesEdit))
            // **************
            // Edit Charges *
            // **************
            {
                chgs = null;
                worker.DoWork += (o, ea) =>
                {
                    chgs = Charges.Query(ChargeQuery, clientId, true);
                };
                worker.RunWorkerCompleted += (o, ea) =>
                    {
                        if (chgs != null && chgs.Length > 0)
                        {
                            grdChgEdit.ItemsSource = chgs;
                            grdChgEdit.UpdateLayout();
                        }
                        else
                            grdChgEdit.DataContext = null;
                        _busy.IsBusy = false;
                    };

                ChargeQuery = Charges.SQL_LOAD_FOR_BATCH_EDIT;
                ChargeQuery += " WHERE " + Charges.TABLE + ".[" + Charges.SQL_COL_BUILDING_NUMBER + "] >= '" + txtChgEditStartBuilding.Text + "' AND " + Charges.TABLE + ".[" + Charges.SQL_COL_BUILDING_NUMBER + "] <= '" + txtChgEditEndBuilding.Text + "'";
                if (lstChgEditType.SelectedIndex > 0)
                    ChargeQuery += " AND pm_tenants.[Type] = '" + lstChgEditType.SelectedValue.ToString().Trim() + "'";
                if (lstChgEditCharge.SelectedIndex > 0)
                    ChargeQuery += " And pm_charges.[Charge Code] = '" + lstChgEditCharge.SelectedValue.ToString().Trim() + "'";
                if (lstChgEditDate.SelectedIndex > 0)
                {
                    if (lstChgEditDate.SelectedIndex <= 2)
                        ChargeQuery += " AND ";
                    else
                        ChargeQuery += " AND ";
                    ChargeQuery += "pm_charges.[Starting Date] " + ops[lstChgEditDate.SelectedIndex] + " '" + ((DateTime)dtChgEditDate.SelectedDate).ToShortDateString() + "'";
                    //if (lstChgEditDate.SelectedIndex <= 2)
                    //    ChargeQuery += " OR pm_charge.[Starting Date] <= '" + GlobalShared.NULL_DATE.ToShortDateString() + "')";
                }
                if (lstChgEditPaid.SelectedIndex > 0)
                {
                    ChargeQuery += " AND pm_charges.[Paid] " + ops[lstChgEditPaid.SelectedIndex] + " " + txtChgEditPaid.Value.ToString(); 
                }
                ChargeQuery += " ORDER BY pm_Charges.[Building Number], pm_Charges.[Tenant Number]";

                _busy.Focus();
                _busy.IsBusy = true;
                worker.RunWorkerAsync();
            }
            else if (tab.Name.Equals(RentPayments))
            // ***************
            // Rent Payments *
            // ***************
            {
                DateTime dt = (DateTime)dtRentPayDate.SelectedDate;
                PaymentQuery = Tenants.SQL_LOAD_FOR_PAYMENTS;               
                PaymentQuery += " WHERE [" + Tenants.SQL_COL_BUILDING_NUMBER + "] >= '" + txtRentPayStartBuilding.Text + "' AND [" + Tenants.SQL_COL_BUILDING_NUMBER + "] <= '" + txtRentPayEndBuilding.Text + "'";
                PaymentQuery += " AND [" + Tenants.SQL_COL_NEW_TENANT_FLAG + "] = 0 ";
                PaymentQuery += " AND [" + Tenants.SQL_COL_CLIENT_ID + "] = " + clientId.ToString();
                if (lstRentPayType.SelectedIndex > 0)
                    PaymentQuery += " AND [" + Tenants.SQL_COL_TYPE + "] = '" + lstRentPayType.SelectedValue.ToString().Trim() +"'";
                PaymentQuery += " ORDER BY [" + Tenants.SQL_COL_BUILDING_NUMBER + "], [" + Tenants.SQL_COL_TENANT_NUMBER + "]";

                worker.DoWork += (o, ea) =>
                    {
                        tensPay = Tenants.Query(PaymentQuery, dt);
                        
                        foreach(Tenants ten in tensPay)
                        {
                            Charges chgs = Charges.loadTotal(ten.SequenceNumber, DateTime.Now.ToShortDateString());
                            ten.TotalCharges = chgs.Total;
                        }
                        
                    };
                worker.RunWorkerCompleted += (o, ea) =>
                    {
                        if (tensPay != null && tensPay.Length > 0)
                        {
                            grdRentPay.DataContext = tensPay;
                            grdRentPay.UpdateLayout();
                        }
                        else
                            grdRentPay.DataContext = null;
                        _busy.IsBusy = false;
                    };

                _busy.Focus();
                _busy.IsBusy = true;
                worker.RunWorkerAsync();
            } else if (tab.Name.Equals(ChargesAdd))
            // *************
            // Add Charges * 
            // *************
            {
                tensAdd = null;
                string type = "";
                decimal tchg;
                decimal Bal;
                decimal arrearsAmount = 0;
                ArrayList list = new ArrayList();
                bool isChecked = false;

                worker.DoWork += (o, ea) =>
                {
                    Tenants[] tens = Tenants.loadForBatchAdd(ChargeQuery);

                    Charges[] chgs;
                    if (tens == null || tens.Length == 0)
                        return;
                    foreach (Tenants ten in tens)
                    {
                        if (isChecked)
                        {
                            tchg = 0;
                            chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                            if (chgs != null && chgs.Length > 0)
                            {
                                foreach (Charges chg in chgs)
                                {
                                    if (chg.StartingDate <= DateTime.Now)
                                        tchg += chg.Amount + chg.Arrears;
                                }
                                chgs = null;
                            }
                            Bal = ten.Rent + ten.Arrears + tchg - ten.Paid;
                            if (Bal < arrearsAmount)
                                continue;
                            ten.TotalArrears = Bal;
                            list.Add(ten);
                        }
                        else
                            list.Add(ten);
                    }
                    tensAdd = (Tenants[])list.ToArray(typeof(Tenants));

                };
                worker.RunWorkerCompleted += (o, ea) => 
                {
                    if (tensAdd != null && tensAdd.Length > 0)
                    {
                        grdChgAdd.ItemsSource = tensAdd;
                        grdChgAdd.UpdateLayout();
                    }
                    else
                        grdChgAdd.ItemsSource = null;
                    _busy.IsBusy = false;
                };

                ChargeQuery = Tenants.SQL_LOAD_FOR_BATCH_ADD;
                ChargeQuery += " WHERE " + "[" + Tenants.SQL_COL_BUILDING_NUMBER + "] >= '" + txtChgAddStartBuilding.Text + "' AND " + "[" + Tenants.SQL_COL_BUILDING_NUMBER + "] <= '" + txtChgAddEndBuilding.Text + "'";
                ChargeQuery += " AND [" + Tenants.SQL_COL_NEW_TENANT_FLAG + "] = 0 " +
                               " AND [" + Tenants.SQL_COL_TYPE + "] <> '#'" +
                               " AND [" + Tenants.SQL_COL_TENANT_NAME + "] NOT LIKE ('*%')";
                if (lstChgAddType.SelectedIndex > 0)
                    ChargeQuery += " AND [" + Tenants.SQL_COL_TYPE + "] = '" + lstChgAddType.SelectedValue.ToString().Trim() + "'";
                //if (chkChgAddAmount.IsChecked.Value)
                //    ChargeQuery += " AND " + "[" + Tenants.SQL_COL_ARREARS + "] > " + txtChgAddAmount.Value.ToString();
                ChargeQuery += " ORDER BY " + "[" + Tenants.SQL_COL_BUILDING_NUMBER + "], " + "[" + Tenants.SQL_COL_TENANT_NUMBER + "]";
                isChecked = chkChgAddAmount.IsChecked.Value;
                decimal.TryParse( txtChgAddAmount.Text, out arrearsAmount);
                _busy.Focus();
                _busy.IsBusy = true;
                worker.RunWorkerAsync();
            }
            else if(tab.Name.Equals(LateFees))
            // ************
            //  Late Fees *
            // ************
            {
                decimal arrears = 0;
                decimal.TryParse(txtLateFeesAmount.Text, out arrears);
                string startBld = txtLateFeesStartBuilding.Text;
                string endBld = txtLateFeesEndBuilding.Text;
                ArrayList list = new ArrayList();
                worker.DoWork += (o, ea) =>
                {
                    Tenants[] tens = Tenants.loadForStatements(startBld, endBld, 1);

                    if (tens != null && tens.Length > 0)
                    {
                        decimal lateFee = 0;
                        foreach (Tenants ten in tens)
                        {
                            if (ten.LateFee != 0 && (ten.Rent + ten.Arrears - ten.Paid >= arrears))
                            {
                                ten.ChargeLateFee = true;
                                ten.CalculatedArrears = ten.Rent + ten.Arrears - ten.Paid;

                                if (ten.LatePercent)
                                {
                                    lateFee = Math.Round((ten.LateFee * (decimal).01) * ten.CalculatedArrears, 2);
                                }
                                else
                                    lateFee = ten.LateFee;
                                ten.CalculatedLateFee = lateFee;
                                list.Add(ten);
                            }
                        }
                        tensLateFees = (Tenants[])list.ToArray(typeof(Tenants));
                    }
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    if (tensLateFees != null && tensLateFees.Length > 0)
                    {
                        grdLateFees.ItemsSource = tensLateFees;
                        grdLateFees.UpdateLayout();
                    }
                    else
                        grdLateFees.ItemsSource = null;
                    _busy.IsBusy = false;
                };
                _busy.Focus();
                _busy.IsBusy = true;
                worker.RunWorkerAsync();
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } BackgroundWorker worker = new BackgroundWorker();
            TabItem tab = (TabItem)tabBatches.SelectedItem;
            bool pf = false;

            if (tab.Name.Equals(FutureRent))
            // **************
            // Future Rents *
            // **************
            {
               if (tens == null)
                    return;
               // worker.DoWork += (o, ea) =>
               //   {
               foreach (Tenants ten in tens)
               {
                    if (ten.Future1.FutureDate > GlobalShared.NULL_DATE && ten.Future1.FutureAmount != 0)
                        ten.Future1.save();
                    if (ten.Future2.FutureDate > GlobalShared.NULL_DATE && ten.Future2.FutureAmount != 0)
                        ten.Future2.save();
                }
               //     };

                //worker.RunWorkerCompleted += (o, ea) =>
                //{
                //    _busy.IsBusy = false;
                MessageBox.Show("Changes Saved.");
                //};

                //_busy.Focus();
                //_busy.IsBusy = true;
                //worker.RunWorkerAsync();
                //while (_busy.IsBusy) { }
            }

            else if (tab.Name.Equals(ChargesEdit))
            // ****************
            // * Edit Charges *
            // ****************
            {
                if (chgs == null)
                    return;
                //worker.DoWork += (o, ea) =>
                //    {
                        foreach (Charges chg in chgs)
                        {
                            if (chg.DeleteCharge)
                            {
                                chg.delete(chg.ChargeNumber.ToString());
                                WinpmGlobal.HistChgUpdate(chg, false, true, chg.HistoryDate);
                            }
                            else if (chg.isChanged)
                            {
 
                                if (chg.StartingDate < chg.HistoryDate)
                                {
                                    WinpmGlobal.HistChgUpdate(chg, false, true, chg.HistoryDate);
                                    chg.HistoryDate = chg.StartingDate;
                                }
                                chg.save();
                                if (chg.StartingDate <= bildate)
                                    WinpmGlobal.HistChgUpdate(chg, false, false, chg.HistoryDate);
                                else
                                    WinpmGlobal.HistChgUpdate(chg, false, true, chg.HistoryDate);
                            }
                        }
                    //};

                //worker.RunWorkerCompleted += (o, ea) =>
                //{
                //    _busy.IsBusy = false;
                    MessageBox.Show("Changes Saved.");
                //};

                //_busy.Focus();
                //_busy.IsBusy = true;
                //worker.RunWorkerAsync();
                //while (_busy.IsBusy) { }
            }

            // *****************
            // * RENT PAYMENTS *
            // *****************
            else if (tab.Name.Equals(RentPayments))
            {
                if (tensPay == null)
                    return;
                frmPaymentEntry frm = new frmPaymentEntry();
          
                //worker.DoWork += (o, ea) =>
                //    {
                        decimal charge;
                        decimal Paid;
                        decimal paidx, x;
                        int j, i;
                        
                        ArrayList pays;
                        PaymentEntries pay;
                        PaymentEntries[] payments;

                        foreach (Tenants ten in tensPay)
                       {
                            if (ten.Post && ten.Payment != 0)
                            {
                                pays = new ArrayList();
                                pay = new PaymentEntries();
                                pay.ChargeNumber = ten.SequenceNumber;
                                pay.Due = ten.Rent + ten.Arrears;
                                 //pay.Paid = ten.Payment;
                                pay.Amount = ten.Payment;
                                pay.Code="=";
                                pays.Add(pay);
			                    Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
			                    i = 0;
			                    if (chgs != null && chgs.Length > 0) {
				                    foreach(Charges chg in chgs) 
                                    {
					                    if (chg.StartingDate <= System.DateTime.Now) 
                                        {
						                    if ((System.DateTime.Now < chg.EndingDate || chg.EndingDate == GlobalShared.NULL_DATE) && chg.Amount + chg.Arrears - chg.Paid != 0)
                                            {
							                    charge = chg.Amount + chg.Arrears - chg.Paid;
							                    if (charge != 0) {
                                                    pay = new PaymentEntries();
								                    i = i + 1;
								                    pay.Due = charge;
								                    pay.ChargeNumber = chg.ChargeNumber;
                                                    pay.Code = chg.ChargeCode;
								                    ((PaymentEntries)pays[0]).Due += chg.Paid;
                                                    pays.Add(pay);
							                    }
						                    }
					                    }
				                    }
			                    }
                                payments = (PaymentEntries[])pays.ToArray(typeof(PaymentEntries));

			                    Paid = ten.Payment;
			                    paidx = Paid;
			                    for (j = 0; j <= i; j++) 
                                {
				                    if (payments[j].Due < 0) 
                                    {
					                    payments[0].Paid += payments[i].Due;
                                        //payments[0].Amount += payments[i].Due;
                            }
                        }
			                    j = 0;
			                    while (j <= i) 
                                {
				                    if (payments[j].Due <= paidx) 
                                    {
					                    payments[j].Paid += payments[j].Due;
                                        //payments[j].Amount += payments[j].Due;
				                    } 
                                    else 
                                    {
					                    payments[j].Paid += paidx;
                                        //payments[j].Amount += paidx;
				                    }
				                    paidx -= payments[j].Paid;
				                    j = j + 1;
			                    }
			                    if (paidx != 0) {
				                    for (j = i; j >= 0; j += -1) 
                                    {
					                    if (payments[j].Due - payments[j].Paid <= 0) {
						                    break; 
					                    }
					                    x = payments[j].Due - payments[j].Paid;
					                    if (x >= paidx) {
						                    payments[j].Paid += paidx;
                                            //payments[j].Amount += paidx;
						                    paidx = 0;
						                    break; 
					                    } else {
						                    payments[j].Paid += x;
                                            //payments[j].Amount += x;
						                    paidx = paidx - x;
					                    }
				                    }
				                    if (paidx != 0) 
                                    {
					                    payments[0].Paid += paidx;
                                       // payments[0].Amount += paidx;
				                    }
			                    }

                                frm.PayentApply( ten.DatePaid.ToString(),false, payments );
			                    pf = true;
		                    }
	                    }
                        //frm.Close();
                    //};

                //worker.RunWorkerCompleted += (o, ea) =>
                //{
                //    _busy.IsBusy = false;
                    MessageBox.Show("Changes Saved.");
                //};

                //_busy.Focus();
                //_busy.IsBusy = true;
                //worker.RunWorkerAsync();
                //while (_busy.IsBusy) { }
            }
            else if (tab.Name.Equals(ChargesAdd))
            // ***************
            // * CHARGES ADD *
            // ***************
            {
                if (tensAdd == null)
                    return;
                string code = lstChgAddCodes.SelectedValue.ToString();
                string type = (string)(optOneTime.IsChecked.Value ? "1" : "2");
                foreach (Tenants ten in tensAdd)                    
                {
                    if (ten.chg.isChanged && ten.chg.Amount != 0)
                    {
                        Charges newCharge = new Charges();
                        newCharge.BuildingNumber = ten.BuildingNumber;
                        newCharge.TenantNumber = ten.TenantNumber;
                        newCharge.SequenceNumber = ten.SequenceNumber;
                        newCharge.ChargeCode = code;
                        newCharge.Type = type;
                        newCharge.Amount = ten.chg.Amount;
                        newCharge.StartingDate = ten.chg.StartingDate;
                        newCharge.EndingDate = ten.chg.EndingDate;
                        newCharge.FutureAmount = ten.chg.FutureAmount;
                        newCharge.FutureDate = ten.chg.FutureDate;
                        newCharge.Comment = ten.chg.Comment;
                        newCharge.OnBill = true;
                        newCharge.ShowOnBill = true;
                        newCharge.ChargeToTenant = true;
                        newCharge.ClientId = clientId;
                        newCharge.HistoryDate = GlobalShared.NULL_DATE;
                        newCharge.save();
                    }
                }

                MessageBox.Show("Changes Saved.");
            }
            else if (tab.Name.Equals(LateFees))
            // ***********
            // LATE FEES *
            // ***********
            {
                if (tensLateFees == null)
                    return;
                if (lstLateFeesCodes.SelectedIndex < 0)
                {
                    MessageBoxCustom.Show("A Charge Code was not selected.");
                    return;
                }
                string code = lstLateFeesCodes.SelectedValue.ToString();
                foreach (Tenants ten in tensLateFees)
                {
                    if (ten.ChargeLateFee && ten.LateFee != 0)
                    {
                        Charges newCharge = new Charges();
                        newCharge.BuildingNumber = ten.BuildingNumber;
                        newCharge.TenantNumber = ten.TenantNumber;
                        newCharge.SequenceNumber = ten.SequenceNumber;
                        newCharge.ChargeCode = code;
                        newCharge.Type = "1";
                        newCharge.Amount = ten.CalculatedLateFee;
                        newCharge.StartingDate = GlobalShared.NULL_DATE;
                        newCharge.EndingDate = GlobalShared.NULL_DATE;
                        newCharge.OnBill = true;
                        newCharge.ShowOnBill = true;
                        newCharge.ChargeToTenant = true;
                        newCharge.Comment = txtLateFeeComment.Text;
                        newCharge.ClientId = clientId;
                        newCharge.HistoryDate = GlobalShared.NULL_DATE;
                        newCharge.save();

                    }
                }
                MessageBox.Show("Changes Saved.");
            }

            cmdClear_Click(sender, e);

            if (pf)
            {
                int drc;
                drc = (int)MessageBox.Show("You have unreported payments.  Print the Payment Register now?", "Payment Register", MessageBoxButton.YesNoCancel);
                switch (drc)
                {
                    case (int)MessageBoxResult.Yes:
                        {
                            frmPaymentRegister form = new frmPaymentRegister();
                            form.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            form.Show();
                            break;
                        }
                    case (int)MessageBoxResult.Cancel:
                        {
                            return;
                            break;
                        }
                }
                WinpmGlobal.calc_Charges(clientId, System.DateTime.Now);
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void moveNextOnMaxlength(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabBatches.SelectedItem;
            if (tab.Name.Equals(FutureRent))
                grdFutureRents.DataContext = null;
            else if (tab.Name.Equals(ChargesEdit))
                grdChgEdit.DataContext = null;
            else if (tab.Name.Equals(RentPayments))
                grdRentPay.DataContext = null;
            else if (tab.Name.Equals(ChargesAdd))
                grdChgAdd.DataContext = null;
        }

        private void NumberEditor_LostFocus(object sender, RoutedEventArgs e)
        {
            // First row amount >< 0 and Apply to All Tenants is checked
            if (grdChgAdd.Items.Count > 0 && ((Tenants)grdChgAdd.Items[0]).chg.Amount != 0 && chkChgAddAll.IsChecked.Value)
            {
                decimal amount = ((Tenants)grdChgAdd.Items[0]).chg.Amount;
                DateTime date = ((Tenants)grdChgAdd.Items[0]).chg.StartingDate;
                DateTime dateEnd = ((Tenants)grdChgAdd.Items[0]).chg.EndingDate;
                string comment = ((Tenants)grdChgAdd.Items[0]).chg.Comment;
                foreach (Tenants ten in tensAdd)
                {
                    ten.chg.Amount = amount;
                    ten.chg.StartingDate = date;
                    ten.chg.EndingDate = dateEnd;
                    ten.chg.Comment = comment;
                }
                grdChgAdd.DataContext = null;
                grdChgAdd.DataContext = tensAdd;
                chkChgAddAll.IsChecked = false;
            }
        }

        private void chkChgAddAll_Click(object sender, RoutedEventArgs e)
        {
            if (chkChgAddAll.IsChecked.Value)
                NumberEditor_LostFocus(sender, e);
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (tensPay != null)
            {
                foreach (Tenants ten in tensPay)
                {
                    ten.Post = true;
                }
            }
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Tenants ten in tensPay)
            {
                ten.Post = false;
            }

        }

        private void DatePickerEx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (chkChgAddAll.IsChecked.Value)
                NumberEditor_LostFocus(sender, e);
        }

        private void chkChgAddAmount_Click(object sender, RoutedEventArgs e)
        {
            if (chkChgAddAmount.IsChecked.Value)
                grdChgAdd.Columns[4].Visibility = Visibility.Visible;
            else
                grdChgAdd.Columns[4].Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Export current tab contents to CSV file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void LateFee_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Tenants ten in tensLateFees)
                ten.ChargeLateFee = true;
        }

        private void LateFee_UnChecked(object sender, RoutedEventArgs e)
        {
            foreach (Tenants ten in tensLateFees)
                ten.ChargeLateFee = false;
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabBatches.SelectedItem;
            System.Windows.Input.Cursor saveCursor = this.Cursor;
            this.Cursor = System.Windows.Input.Cursors.Wait;
            DataGrid grid = null;
            Tenants[] tenants = null;
            switch (tab.Name)
            {
                case FutureRent:
                    grid = grdFutureRents;
                    tenants = tens;
                    break;
                case ChargesEdit:
                    grid = grdChgEdit;
                    break;
                case RentPayments:
                    //return;
                    grid = grdRentPay;
                    tenants = tensPay;
                    break;
                case ChargesAdd:
                    grid = grdChgAdd;
                    tenants = tensAdd;
                    break;
                case LateFees:
                    grid = grdLateFees;
                    tenants = tensLateFees;
                    break;
                default:
                    return;
            }
            //if (tab.Name == FutureRent)
            //{
                if (tenants == null || tenants.Length == 0)
                {
                    MessageBox.Show("There are no records to export.");
                    return;
                }
                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                dlg.Filter = "Comma Separated Value File (*.csv)| *.csv";
                dlg.DefaultExt = ".csv";
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filepath = dlg.FileName;
                    StreamWriter sw;
                    try
                    {
                        sw = new StreamWriter(filepath, false);

                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Uable to open file: " + ex.Message);
                        this.Cursor = saveCursor;
                        return;
                    }
                    bool first = true;
                    ArrayList list = new ArrayList();
                    foreach (DataGridColumn col in grid.Columns)
                    {
                        string name = col.SortMemberPath;
                        if (!name.Trim().Equals(""))
                        {
                            sw.Write((first ? "" : ",") + col.Header + "");
                            list.Add(name);
                            first = false;
                        }
                    }
                    sw.WriteLine();
                    int i;
                    foreach(Tenants ten in tenants)
                    {
                        if (tab.Name.Equals(LateFees) && !ten.ChargeLateFee)
                            continue;
                        first = true;
                        decimal number;
                        bool usequote;
                        string value;
                        PropertyInfo propertyInfo = null;
                        Type type = null;
                        for (i = 0; i < list.Count; i++)
                        {
                            try
                            {
                                var obj = ten[(string)list[i]];
                                type = ten[(string)list[i]].GetType();
                                //var obj = ten[(string)list[i]] as DateTime?;

                                if (obj != null && obj is DateTime)
                                {
                                    value = ((DateTime)obj).ToShortDateString();
                                    usequote = false;
                                }
                                else
                                {
                                    if (obj is decimal && decimal.TryParse(ten[(string)list[i]].ToString(), out number))
                                    {
                                        value = number.ToString("#######0.00");
                                        usequote = false;
                                    }
                                    else
                                    {
                                        value = ten[(string)list[i]].ToString();
                                        usequote = true;
                                    }
                                }
                                if (usequote)
                                    sw.Write((first ? "\"" : ",\"") + value + "\"");
                                else
                                    sw.Write((first ? "" : ",") + value);
                            }
                            catch (Exception ex) { sw.Write(","); }
                            first = false;
                        }
                        sw.WriteLine();
                    }
                    sw.Close();
                }
            //}
            this.Cursor = saveCursor;
        }
        public static bool UseQuotes(object obj)
        {
            // First see if it is a FrameworkElement
            var element = obj as DateTime?;
            if (element != null)
                return false;
            if (Information.IsNumeric(obj))
                return false;
            return true;
        }

        private void txtFRStartBuilding_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }

        private void TabBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabBatches.SelectedItem == null)
                return;
            string tabItem = (tabBatches.SelectedItem as TabItem).Name;
            cmdExport.IsEnabled = false;
            switch (tabItem)
            {
                case "tabFutureRents":
                    cmdExport.IsEnabled = true;
                    txtFRStartBuilding.Focus();
                    txtFRStartBuilding.SelectAll();
                    break;
                case "tabLateFees":
                    cmdExport.IsEnabled = true;
                    break;
                case "tabRentPayments":
                    cmdExport.IsEnabled = true;
                    break;

            }
        }
    }

    public class AddCharges : Tenants
    {
        public AddCharges() { }

        public Charges chg {get; set;}

    }

    [Serializable]
    public class BatchParms 
    {
        public string StartingBuilding { get; set; } = "000";
        public string EndingBuilding { get; set; } = "999";
        public DateTime Date { get; set; } = DateTime.Now;

        public BatchParms()
        {

        }

    }

    [Serializable]
    public class ChargesEditParms : BatchParms
    {
        public int TenantType { get; set; }
        public int ChargeCode { get; set; }
        public int StartDateOp { get; set; }
        public int ChargePaidOp { get; set; }
        public decimal ChargePaid { get; set; }

        public ChargesEditParms() { }

        public string Serialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                StringWriter textWriter = new StringWriter();

                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();

            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving Batch parms: " + ex.Message, true);
                return null;
            }
        }

        public static object DeSerialize(string inputString)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ChargesEditParms));
                StringReader rdr = new StringReader(inputString);
                ChargesEditParms parms = (ChargesEditParms)serializer.Deserialize(rdr);
                return parms;
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error retrieving Batch parms: " + ex.Message, true);
                return null;
            }
        }

    }

    public class LateFeesParms 
    {
        public string StartBuilding { get; set; }
        public string EndBuilding { get; set; }
        public decimal ArrearsAmount { get; set; }
        public bool UseArrearsAmount { get; set; }
        public int LateFeeChargeCode { get; set; }
        public string LateFeeComment { get; set; }

        public string Serialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                StringWriter textWriter = new StringWriter();

                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();

            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving LateFee parms: " + ex.Message, true);
                return null;
            }
        }

        public static object DeSerialize(string inputString)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LateFeesParms));
                StringReader rdr = new StringReader(inputString);
                LateFeesParms parms = (LateFeesParms)serializer.Deserialize(rdr);
                return parms;
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error retrieving LateFee parms: " + ex.Message, true);
                return null;
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error retrieving LateFee parms: " + ex.Message, true);
                return null;

            }
        }

    }
}
