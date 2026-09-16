using MbcsCentral;
using System.Windows.Input;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VIBlend.WPF.Controls;
using System.Linq;
//using CSharp.Input;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmPaymentEntry.xaml
    /// </summary>
    public partial class frmPaymentEntry : Window
    {
        Control ctrl = Control.load(GlobalShared.user.Account.clientId);


        ArrayList entries;
        ArrayList pays;
        PaymentEntries[] payments;
        Tenants[] tenants;
        Tenants tenant;
        int tenantId;
        DateTime c_date = DateTime.Now;
        DateTime PaidDate;
        decimal Paid;
        string Notes;
        decimal balance;
        string secCode = "";
        bool noTenant;
        long clientId = GlobalShared.user.Account.clientId;
        string SecurityCode = "";
        string m_ThisApp = "";

        const int MAX_TEMP_NAME_LENGTH = 30;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmPaymentEntry()
        {
            try
            {
                InitializeComponent();
                if (ctrl != null)
                    secCode = ctrl.SecurityCode;
                if (secCode == null || secCode.Trim().Equals(""))
                {
                    chkSecurity.IsEnabled = false;
                }
            }
            catch { }
        }
        public frmPaymentEntry(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
            if (ctrl != null)
                secCode = ctrl.SecurityCode;
            if (secCode == null || secCode.Trim().Equals(""))
            {
                if (MessageBox.Show("A Rent Security Charge code has not been assigned.\nIf you continue, no Security payments can be entered.\nAdvise your system administrator.\nDo you want to continue to Payment Entries?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    this.Close();
                chkSecurity.IsEnabled = false;
            }
            GlobalShared.Log.Log((int)LogClass.logType.Info, "frmPaymentEntry initialized.", false);
        }
        private void txtBuildingNumber_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);
        }

        private void formPaymentEntry_Loaded_1(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            DateTime billDate = ctrl.BillingDate.Date;
            // if withing last 5 days of month
            //    message 
            DateTime testDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).AddDays(-4);
            if (billDate > DateTime.Now.Date)
            {
                MessageBoxCustom.Show("Rent has been rolled. Billing date will be used to calculate rent and charges due.");
                dtDatePaid.DisplayDate = billDate;
                dtDatePaid.Text = billDate.ToString();
                dtDatePaid.SelectedDate = billDate;
                chkNotSystemDate.IsChecked = true;
            }
            else
            {
                if (DateTime.Now >= testDate)
                    MessageBox.Show("IMPORTANT: Current date is withing 5 days of End of Month.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                dtDatePaid.DisplayDate = DateTime.Now;
                dtDatePaid.Text = DateTime.Now.ToString();
                dtDatePaid.SelectedDate = DateTime.Now;
            }
            txtBuildingNumber.Focus();
        }

        private void txtTenantNumber_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (noTenant)
            {
                noTenant = false;
                return;
            }
            tenants = Tenants.load(txtBuildingNumber.Text, txtTenantNumber.Text, clientId);
            if (tenants == null)
            {
                MessageBox.Show("Invalid Tenant Number");
                noTenant = true;
                txtTenantNumber.Focus();
                return;
            }
            tenantId = tenants[0].SequenceNumber;
            if (tenants.Length == 1)
            {
                tenantId = tenants[0].SequenceNumber;
            }
            else
            {
                try
                {
                    frmChooseTenant frm = new frmChooseTenant();
                    frm.Tenants = tenants;
                    frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    frm.ShowDialog();
                    if (frm.SequenceNumber == -1)
                    {
                        return;
                    }
                    tenantId = frm.SequenceNumber;
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "PayEntries: " + ex.Message, false);
                }
            }
            tenant = Tenants.loadForPaymentEntry(tenantId);
            if (tenant.LegalCode != null && !tenant.LegalCode.Trim().Equals(""))
            {
                ChargeCodes lglMsg = ChargeCodes.load(tenant.LegalCode, GlobalShared.user.Client.clientId);
                if (lglMsg != null)
                {
                    MessageBoxCustom.Show("Tenant has LEGAL CODE: <" + tenant.LegalCode + "> " + lglMsg.ChargeDescription, "", MessageBoxButton.OK, this);
                }
            }
            MainGrid.DataContext = tenant;
            getTenant();
        }

        private void getTenant()
        {
            decimal chgBalance = 0;
            decimal chgPaid = 0;
            decimal Rent = 0;
            if (tenant != null)
            {
                Payments[] PaidAmounts = Payments.loadBySequence(tenantId);
                if (entries != null)
                    entries = null;
                entries = new ArrayList();
                if (pays != null)
                    pays = null;
                pays = new ArrayList();
                PaymentEntries pay = new PaymentEntries();
                Types tblType = Types.load(clientId, "=");
                if (tblType != null)
                {
                    if (tblType.BillingName != null && tblType.BillingName.Trim().Length > 0)
                        pay.Description = tblType.BillingName;
                    else
                        pay.Description = "RENT";
                }
                else
                    pay.Description = "RENT";
                // Find rent payments
                decimal tenantPaid = 0;
                if (PaidAmounts != null && PaidAmounts.Length > 0)
                    foreach (Payments paidamount in PaidAmounts)
                        //if (paidamount.SecurityCode.Equals("="))
                        tenantPaid += paidamount.Amount;
                Rent = tenant.Rent;
                // Find future rent if chkNotSystemDate is checked.
                if (chkNotSystemDate.IsChecked.Value)
                {
                    FutureRents[] rents = FutureRents.loadBySequence(tenant.SequenceNumber);
                    if (rents != null && rents.Length > 0)
                    {
                        foreach (FutureRents rent in rents)
                        {
                            if (c_date.Date >= rent.FutureDate.Date && rent.FutureAmount > 0)
                            {
                                Rent = rent.FutureAmount;
                                break;
                            }
                        }
                        rents = null;
                    }
                }
                balance = Rent + tenant.Arrears - tenantPaid; //tenant.Paid;
                pay.Code = "=";
                pay.Due = balance;
                pay.Amount = Rent;
                pays.Add(pay);

                Charges[] chgs = Charges.loadBySeqNum(tenantId);
                ChargeCodes cd;
                if (chgs != null && chgs.Length > 0)
                {
                    decimal chargeAmount = 0;
                    foreach (Charges chg in chgs)
                    {
                        if (chg.StartingDate == null || chg.StartingDate <= c_date)
                        {

                            pay = new PaymentEntries();
                            pay.ChargeNumber = (int)chg.ChargeNumber;
                            pay.Code = chg.ChargeCode;
                            chargeAmount = chg.Amount;
                            // Find future charge if chkNotSystemDate is checked.
                            if (chkNotSystemDate.IsChecked.Value && chg.FutureDate != null && chg.FutureDate != GlobalShared.NULL_DATE && c_date >= chg.FutureDate)
                                chargeAmount = chg.FutureAmount;
                            cd = ChargeCodes.load(chg.ChargeCode, clientId);
                            if (cd != null)
                                pay.Description = cd.ChargeDescription;
                            else
                                pay.Description = "*";
                            if (chg.OnBill)
                                pay.Description += "[" + chg.Comment + "]";
                            pay.Due = chargeAmount + chg.Arrears - chg.Paid;
                            pay.Amount = pay.Due;
                            chgBalance += pay.Due;
                            // balance += pay.Due;
                            pays.Add(pay);
                            // tenantPaid -= chg.Paid;
                            chgPaid -= chg.Paid;

                        }
                    }
                    ((PaymentEntries)pays[0]).Due = Rent + tenant.Arrears - tenantPaid - chgPaid;
                }
                balance = Rent + tenant.Arrears - tenantPaid + chgBalance - chgPaid;
                txtDue.Text = balance.ToString("######0.00");
                txtReceived.Text = balance.ToString("######0.00");
                payments = (PaymentEntries[])pays.ToArray(typeof(PaymentEntries));

                SetGrid(grdPayments, payments);
                payent_alloc();
                txtReceived.Focus();
            }
        }

        /// <summary>
        /// Create the payment grid with all charges and their amounts due
        /// </summary>
        /// <param name="grd">The grid object</param>
        /// <param name="payments">Array of payment entry objects that contains the charges</param>
        void SetGrid(Grid grd, PaymentEntries[] payments)
        {
            Border bdr = null;
            grd.Children.Clear();
            int rows = payments.Length;
            int cols = 3;
            for (int i = 0; i <= rows; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(25);
                grd.RowDefinitions.Add(rd);
            }
            TextBlock col1 = new TextBlock();
            col1.Text = "Description";
            col1.VerticalAlignment = VerticalAlignment.Center;
            col1.HorizontalAlignment = HorizontalAlignment.Center;
            col1.TextAlignment = TextAlignment.Center;
            col1.FontWeight = FontWeights.Bold;
            col1.Width = 240;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 0);
            Grid.SetRow(bdr, 0);
            bdr.Child = col1;
            grdPayments.Children.Add(bdr);

            TextBlock col2 = new TextBlock();
            col2.Text = "Due";
            col2.VerticalAlignment = VerticalAlignment.Center;
            col2.HorizontalAlignment = HorizontalAlignment.Center;
            col2.TextAlignment = TextAlignment.Center;
            col2.FontWeight = FontWeights.Bold;
            col2.Width = 100;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 1);
            Grid.SetRow(bdr, 0);
            bdr.Child = col2;
            grdPayments.Children.Add(bdr);

            TextBlock col3 = new TextBlock();
            col3.Text = "Paid";
            col3.VerticalAlignment = VerticalAlignment.Center;
            col3.HorizontalAlignment = HorizontalAlignment.Center;
            col3.TextAlignment = TextAlignment.Center;
            col3.FontWeight = FontWeights.Bold;
            col3.Width = 100;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 2);
            Grid.SetRow(bdr, 0);
            bdr.Child = col3;
            grdPayments.Children.Add(bdr);

            TextBlock col4 = new TextBlock();
            col4.Text = "Notes";
            col4.VerticalAlignment = VerticalAlignment.Center;
            col4.HorizontalAlignment = HorizontalAlignment.Center;
            col4.TextAlignment = TextAlignment.Center;
            col4.FontWeight = FontWeights.Bold;
            col4.Width = 100;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 3);
            Grid.SetRow(bdr, 0);
            bdr.Child = col4;
            grdPayments.Children.Add(bdr);

            Binding bind = null;
            for (int r = 1; r <= rows; r++)
            {
                bind = new Binding("Description");
                bind.Source = payments[r - 1];
                TextBlock col = new TextBlock();
                BindingOperations.SetBinding(col, TextBlock.TextProperty, bind);
                col.TextAlignment = TextAlignment.Left;
                col.VerticalAlignment = VerticalAlignment.Center;
                col.Width = 240;
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 0);
                Grid.SetRow(bdr, r);
                bdr.Child = col;
                grdPayments.Children.Add(bdr);

                bind = new Binding("Due");
                bind.Source = payments[r - 1];
                bind.Converter = new MbcsCentral.CurrencyPaddingConverter();
                bind.ConverterParameter = "10";
                col = new TextBlock();
                BindingOperations.SetBinding(col, TextBlock.TextProperty, bind);
                col.TextAlignment = TextAlignment.Right;
                col.VerticalAlignment = VerticalAlignment.Center;
                col.HorizontalAlignment = HorizontalAlignment.Left;
                col.Width = 100;
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 1);
                Grid.SetRow(bdr, r);
                bdr.Child = col;
                grdPayments.Children.Add(bdr);


                NumberEntry vi = new NumberEntry();
                bind = new Binding();
                bind.Path = new PropertyPath("Paid");
                bind.Source = payments[r - 1];
                bind.StringFormat = "######0.00";
                BindingOperations.SetBinding(vi, TextBox.TextProperty, bind);
                vi.MaxLength = 10;
                vi.Decimals = 2;
                vi.Width = 100;
                vi.LostFocus += NumberEditor_LostFocus;
                vi.PreviewKeyDown += PaidBlock_PreviewKeyDown;
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 2);
                Grid.SetRow(bdr, r);
                bdr.Child = vi;
                grdPayments.Children.Add(bdr);
                // Notes
                TextBox notes = new TextBox();
                bind = new Binding();
                bind.Path = new PropertyPath("Notes");
                bind.Source = payments[r - 1];
                BindingOperations.SetBinding(notes, TextBox.TextProperty, bind);
                notes.MaxLength = 200;
                notes.Width = 400;

                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 3);
                Grid.SetRow(bdr, r);
                bdr.Child = notes;
                grdPayments.Children.Add(bdr);
            }
        }

        /*
        void SetGridNew
            (MbcsGrid grd, PaymentEntries[] payments)
        {
            TextBlockEditor description = new TextBlockEditor() { BindingName = "Description", Header = "Description", Width = 240, BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = 2 };
            grd.AddColumn(description);
            TextBlockEditor due = new TextBlockEditor() { BindingName = "Due", Header = "Due", Width = 100, BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = 2 };
            grd.AddColumn(due);
            CSharp.Input.NumberEditor paid = new CSharp.Input.NumberEditor() { BindingName = "Paid", Header = "Paid", Width = 100, MaxLength = 10, Decimals = 2, BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
            grd.AddColumn(paid);
            grd.DataItems = payments;
        }
        */

        void payent_alloc()
        {

            Paid = 0;
            decimal.TryParse(txtReceived.Text, out Paid);
            decimal paidx = 0;
            decimal x;
            int j = 0;
            bool found = false;
            if (payments == null || payments.Length == 0)
                return;


            foreach (PaymentEntries pay in payments)
            {
                pay.Paid = 0;
            }
            j = 0;
            paidx = Paid;
            if (chkSecurity.IsChecked.Value)
            {
                Array.Resize(ref payments, 1);
                payments[0].Description = "SECURITY";
                payments[0].Paid = Paid;
                payments[0].Due = Paid;
                SetGrid(grdPayments, payments);
            }
            else //if (paidx != 0) // Regular payment
            {
                found = false;
                for (j = 0; j < payments.Length; j++)
                {
                    if (Paid == payments[j].Due)
                    {
                        payments[j].Paid = Paid;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    paidx = Paid;
                    for (j = 0; j < payments.Length; j++)
                    {
                        if (payments[j].Due < 0)
                        {
                            payments[0].Paid += payments[j].Due;
                        }
                    }
                    j = 0;
                    while (j < payments.Length)
                    {
                        if (payments[j].Due < paidx)
                        {
                            payments[j].Paid += payments[j].Due;
                        }
                        else
                        {
                            payments[j].Paid += paidx;
                        }
                        paidx -= payments[j].Paid;
                        j = j + 1;
                    }
                    if (paidx != 0)
                    {
                        for (j = payments.Length - 1; j >= 0; j--)
                        {
                            if (payments[j].Due - payments[j].Paid <= 0)
                                break;
                            x = payments[j].Due - payments[j].Paid;
                            if (x >= paidx)
                            {
                                payments[j].Paid += paidx;
                                paidx = 0;
                                break;
                            }
                            else
                            {
                                payments[j].Paid += x;
                                paidx = paidx - x;
                            }
                        }
                        if (paidx != 0)
                        {
                            payments[0].Paid += paidx;
                        }
                    }
                }
                /*
                if (paidx < 0)
                {
                    //payments[0].Paid = paidx;
                    
                    for (j = payments.Length - 1; j >= 0; j--)
                    {
                        if (payments[j].Due != 0)
                        {
                            payments[j].Paid = -payments[j].Due;
                            paidx += payments[j].Due;
                        }
                    }
                    if (paidx != 0)
                        payments[0].Paid += paidx;
                    found = true;
                }
                if (!found)
                {
                    j = 0;
                    while (j < payments.Length)
                    {
                        if (payments[j].Due < paidx)
                            payments[j].Paid += payments[j].Due;
                        else if (payments[j].Due != 0)
                            payments[j].Paid += paidx;
                        paidx -= payments[j].Paid;
                        j++;
                    }
                    if (paidx != 0)
                    {
                        payments[0].Paid += paidx;
                    }
                }
                */
            }
            grdPayments.DataContext = null;
            grdPayments.DataContext = payments;
            NumberEditor_LostFocus(new object(), new RoutedEventArgs());
        }

        private void payent_save(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (decimal.Parse(txtReceived.Text) != decimal.Parse(txtTotal.Text))
            {
                MessageBox.Show("Total Allocated does not equal Amount Received.");
                return;
            }
            PayentApply(dtDatePaid.SelectedDate.ToString(), chkSecurity.IsChecked.Value);
            cmdClear_Click_1(sender, e);
        }

        public void PayentApply(string payDate, bool Security, PaymentEntries[] pays = null, bool lockBox = false)
        {
            // Called from outside this module?

            if (pays != null)
            {
                payments = pays;
                tenantId = payments[0].ChargeNumber;
                Notes = payments[0].Notes;
                Paid = payments[0].Amount;
                PaidDate = DateTime.Parse(payDate);
                tenant = Tenants.loadForPaymentEntry(tenantId);
            }
            else
                PaidDate = dtDatePaid.SelectedDate.Value;


            short i;
            Charges chg = null;

            if (tenant == null)
            {
                MessageBox.Show("Error loading Tenant record. <Sequence Number> = " + tenantId.ToString());
            }
            else
            {
                tenant.Paid += Paid;
                tenant.LastPaidDate = PaidDate;
                if (tenant.TenantNumber.Length == 4 && tenant.TenantNumber.EndsWith("B"))
                {
                    tenant.MBR += Paid;
                }
                if (Security)
                {
                    tenant.Security += Paid;
                    payent_sec(Paid);
                    if (!tenant.saveForPaymentEntry())
                        MessageBox.Show("Error updating Security amount in Tenant Record.");
                }
                else
                {
                    tenant.saveForPaymentEntry();
                    if (payments[0].Paid != 0)
                    {
                        //payent_pay(payments[0].Paid, "=", tenant);
                        payent_temp(payments[0].Paid, "=", tenant, payments[0].Notes, lockBox);
                        //Paid -= payments[0].Paid;
                    }
                    for (i = 1; i < payments.Length; i++)
                    {
                        if (payments[i].Paid != 0)
                        {
                            /*
                            chg = Charges.load((int)payments[i].ChargeNumber);
                            if (chg.ChargeCode == secCode)
                            {
                                tenant.Security += payments[i].Paid;
                                tenant.saveForPaymentEntry();
                                payent_sec(payments[i].Paid);
                            }
                            else
                            {
                                chg.Paid += payments[i].Paid;
                                chg.save();
                                payent_pay(payments[i].Paid, payments[i].Code, tenant);
                                payent_temp(payments[i].Paid, payments[i].Code, tenant);
                            }
                            Paid = Paid - payments[i].Paid;
                            if (tenant.LegalCode == chg.ChargeCode)
                            {
                                if (chg.Amount + chg.Arrears - chg.Paid == 0)
                                {
                                    tenant.LegalCode = " ";
                                    tenant.saveForPaymentEntry();
                                }
                            }
                            */
                            chg = Charges.load((int)payments[i].ChargeNumber);
                            chg.Paid += payments[i].Paid;
                            chg.save();
                            payent_temp(payments[i].Paid, payments[i].Code, tenant, payments[i].Notes, lockBox);
                            if (tenant.LegalCode == chg.ChargeCode)
                            {
                                if (chg.Amount + chg.Arrears - chg.Paid == 0)
                                {
                                    tenant.LegalCode = " ";
                                    tenant.saveForPaymentEntry();
                                }
                            }
                            if (chg.ChargeCode == secCode)
                            {
                                tenant.Security += payments[i].Paid;
                                tenant.saveForPaymentEntry();
                                payent_pay(payments[i].Paid, payments[i].Code, tenant, payments[i].Notes);
                                Paid = Paid - payments[i].Paid;
                            }

                        }
                    }
                    if (Paid != 0)
                    {
                        payent_pay(Paid, "=", tenant, payments.Count() > 0 ? payments[0].Notes : null);
                    }
                }

            }


            tenant = null;
            chg = null;
            if (pays == null)
                cmdClear_Click_1(new object(), new RoutedEventArgs());
        }

        void payent_sec(decimal paid)
        {

            bool updateTenants = false;
            Charges chg = null;

            Charges[] chgs = Charges.loadForSecurity(tenantId, secCode, dtDatePaid.SelectedDate.Value);
            if (chgs == null || chgs.Length <= 0)
            {
                chg = new Charges();
                chg.BuildingNumber = tenant.BuildingNumber;
                chg.TenantNumber = tenant.TenantNumber;
                chg.ChargeCode = secCode;
                chg.Type = "1";
                chg.Amount = paid;
                chg.Arrears = 0;
                chg.Paid = Paid;
                chg.StartingDate = GlobalShared.NULL_DATE;
                chg.EndingDate = GlobalShared.NULL_DATE;
                chg.FutureAmount = 0;
                chg.FutureDate = GlobalShared.NULL_DATE;
                chg.SequenceNumber = (int)tenantId;
                chg.HistoryDate = PaidDate;
                chg.ChargeToTenant = true;
                chg.ShowOnBill = true;
                chg.ClientId = clientId;
                chg.save();
                updateTenants = true;
            }
            else
            {
                chg = chgs[0];
                chg.Paid += paid;
                chg.save();
                updateTenants = false;
            }
            WinpmGlobal.HistChgUpdate(chg, false, false, PaidDate);
            if (updateTenants)
            {
                tenant.TotalCharges += chg.Amount + chg.Arrears;
                tenant.saveForPaymentEntry();
            }
            payent_pay(paid, secCode, tenant, Notes);
            payent_temp(paid, secCode, tenant, Notes);
        }

        void payent_pay(decimal a, string c, Tenants tblTenant, string notes)
        {

            short errorcount;
            Payments tblPayments = new Payments();
            errorcount = 1;
            tblPayments.BuildingNumber = tenant.BuildingNumber;
            tblPayments.TenantNumber = tenant.TenantNumber;
            tblPayments.DateofEntry = PaidDate;
            tblPayments.Amount = a;
            tblPayments.SecurityCode = c;
            tblPayments.SequenceNumber = tenant.SequenceNumber;
            tblPayments.Notes = notes;
            tblPayments.save();

            //if (chkSecurity.IsEnabled && chkSecurity.IsChecked.Value && c == secCode)
            if (c == secCode)
            {
                WinpmGlobal.SecTrans(tblPayments);
            }

            // History Payments
            Histmast histMast = Histmast.load(tblTenant.SequenceNumber);
            if (histMast == null)
            {
                if (!WinpmGlobal.Create_HistMast(tblTenant))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Tenant has no History Master Record and one could not be created.", System.Windows.Forms.MessageBoxButtons.OK, "");
                    return;
                }
            }
            HistPay tblHistPay = new HistPay();
            tblHistPay.BuildingNumber = tblTenant.BuildingNumber;
            tblHistPay.TenantNumber = tblTenant.TenantNumber;
            tblHistPay.SequenceNumber = tblTenant.SequenceNumber;
            tblHistPay.PayDate = PaidDate;
            tblHistPay.Amount = a;
            tblHistPay.Code = c;
            tblHistPay.Notes = notes;
            tblHistPay.save();
        }

        void payent_temp(decimal a, string c, Tenants tblTenant, string notes, bool lockBox = false)
        {

            Paytemp tblPayTemp = new Paytemp();
            //if (lockBox)
            //    tblPayTemp.UserID = WinpmGlobal.LOCKBOX_USERNAME;
            //else
            tblPayTemp.UserID = GlobalShared.user.Account.accountId.ToString();
            tblPayTemp.BuildingNumber = tblTenant.BuildingNumber;
            tblPayTemp.TenantNumber = tblTenant.TenantNumber;
            tblPayTemp.Date = PaidDate;
            tblPayTemp.Amount = a;
            tblPayTemp.Code = c;
            tblPayTemp.Notes = notes;
            string name = (lockBox ? "* " : "") + tblTenant.TenantName;
            if (name.Length > MAX_TEMP_NAME_LENGTH)
                name = name.Substring(0, MAX_TEMP_NAME_LENGTH);
            tblPayTemp.Name = name;
            tblPayTemp.Apartment = tblTenant.Apartment;
            if (!tblPayTemp.save())
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error writing PayTemp file.", System.Windows.Forms.MessageBoxButtons.OK, "");
            }

        }

        private void formPaymentEntry_Closed_1(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Paytemp[] pmts = Paytemp.loadByUser();
            if (pmts != null && pmts.Length > 0)
            {
                int drc;
                drc = (int)MessageBox.Show("You have " + pmts.Length.ToString() + " unreported payments.  Print the Payment Register now?", "Payment Register", MessageBoxButton.YesNoCancel);
                switch (drc)
                {
                    case (int)MessageBoxResult.Yes:
                        {
                            frmPaymentRegister frm = new frmPaymentRegister();
                            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            frm.Show();
                            break;
                        }
                    case (int)MessageBoxResult.Cancel:
                        {
                            return;
                            break;
                        }
                }
            }
        }

        private void chkSecurity_LostFocus_1(object sender, RoutedEventArgs e)
        {
            payent_alloc();
            grdPayments.Focus();
        }

        private void txtReceived_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                txtReceived_LostFocus(sender, new RoutedEventArgs());
            }
            else if (e.Key == Key.Tab)
            {
                ClearPaid();
                payent_alloc();

            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                int p = txtReceived.Text.IndexOf("-");
                if (p >= 0)
                    txtReceived.SelectionStart = p + 1;
                /*
                decimal number = 0;

                if (!decimal.TryParse(txtReceived.Text, out number))
                    number = 0;
                if (txtReceived.Text.Trim().Equals("-"))
                    txtReceived.Text = "";
                else if (txtReceived.Text.Trim().Equals("") || number == 0)
                {
                    txtReceived.Text = "-";
                }
                else
                {
                    number *= -1;
                }
                txtReceived.SelectionStart = (number < 0 || txtReceived.Text.Trim().Equals("-") ? 1 : 0);
                txtReceived.SelectionLength = txtReceived.Text.Length;
                */

            }
        }

        private void txtReceived_LostFocus(object sender, RoutedEventArgs e)
        {
            ClearPaid();
            payent_alloc();

            try
            {
                grdPayments.Focus();
                var obj = grdPayments.Children[6];
                if (obj != null)
                {
                    ((Border)obj).Child.Focus();
                }
            }
            catch { }
        }

        private void ClearPaid()
        {
            if (payments != null && payments.Length > 0)
                for (int i = 1; i < payments.Length; i++)
                    payments[i].Paid = 0;
        }

        private void txtTenantNumber_PreviewKeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return || e.Key == System.Windows.Input.Key.Tab)
                txtReceived.Focus();
        }

        private void NumberEditor_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal total = 0;
            decimal pay = 0;
            Border bdr = null;
            for (int c = 6; c < grdPayments.Children.Count; c += 4)
            {
                bdr = (Border)grdPayments.Children[c];
                if (bdr != null)
                {
                    var obj = bdr.Child;
                    if (obj != null)
                    {
                        var entry = (NumberEntry)obj;
                        var objtext = ((NumberEntry)obj).Text;
                        pay = Decimal.Parse(((NumberEntry)obj).Text);
                        total += pay;
                    }
                }
            }
            txtTotal.Text = total.ToString("######0.00");
        }

        private void PaidBlock_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            NumberEntry num = (NumberEntry)sender;
            if ((e.Key == Key.Tab || e.Key == Key.Delete) && (num.Text.Trim().Equals("") || num.Text.Trim().Equals("-") || num.Text.Trim().Equals(".")))
                num.Text = "0";
            //NumberEditor_LostFocus(sender, new RoutedEventArgs());
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                payent_save(sender, new RoutedEventArgs());
            }
        }

        private void cmdExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click_1(object sender, RoutedEventArgs e)
        {
            tenant = null;
            txtBuildingNumber.Text = "";
            txtTenantNumber.Text = "";
            txtTenantName.Text = "";
            txtApartment.Text = "";
            txtDue.Text = string.Format("{0:######0.00}", 0);
            txtReceived.Text = string.Format("{0:######0.00}", 0);
            txtTotal.Text = string.Format("{0:######0.00}", 0);
            chkSecurity.IsChecked = false;
            grdPayments.Children.Clear();
            txtBuildingNumber.Focus();
            cmdSave.IsDefault = false;
        }

        private void cmdReset_Click_1(object sender, RoutedEventArgs e)
        {
            txtReceived.Text = txtDue.Text;
            getTenant();
            payent_alloc();
            grdPayments.Focus();
        }

        private void txtReceived_GotFocus(object sender, RoutedEventArgs e)
        {
            txtReceived.SelectionStart = 0;
            txtReceived.SelectionLength = txtReceived.Text.Length;
            cmdSave.IsDefault = false;
        }

        private void NumberEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            //NumberEditor num = (NumberEditor)sender;
            //num.SelectionStart = 0;
            //num.SelectionLength = num.Text.Length;
        }

        private void NumberEditor_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up || e.Key == System.Windows.Input.Key.Left)
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            if (e.Key == Key.Down || e.Key == Key.Right)
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                NumberEntry vi = (NumberEntry)sender;
                if (vi != null)
                    if (vi.Value == 0)
                        vi.Text = "-.00";

            }
        }

        private void grdPayments_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Up && e.Key != System.Windows.Input.Key.Down)

                grdPayments = (Grid)sender;
        }

        private void txtReceived_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                txtReceived_LostFocus(sender, new RoutedEventArgs());
            }
            else if (e.Key == Key.Tab)
            {
                ClearPaid();
                payent_alloc();

            }
        }

        private void dtDatePaid_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkNotSystemDate.IsChecked.Value)
                c_date = (DateTime)dtDatePaid.SelectedDate;
            else
                c_date = DateTime.Now.Date;
            getTenant();
        }

        private void chkNotSystemDate_Checked(object sender, RoutedEventArgs e)
        {
            if (chkNotSystemDate.IsChecked.Value)
                c_date = (DateTime)dtDatePaid.SelectedDate;
            else
                c_date = DateTime.Now.Date;
            getTenant();
        }
    }
}