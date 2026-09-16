using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmAddTenant.xaml
    /// </summary>
    public partial class frmAddTenant : Window
    {
        public string BuildingNumber { get; set; }

        FutureRents[] futures = new FutureRents[0];
        System.Windows.Data.CollectionViewSource FutureRentsView;
        bool first = true;
        Tenants[] oldTenant;
        bool replacing = false;
        bool dirty = false;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmAddTenant()
        {
            InitializeComponent();
            dtMovein.DisplayDate = DateTime.Now;
            FutureRentsView = (System.Windows.Data.CollectionViewSource)this.FindResource("FutureRentsView");
        }

        public frmAddTenant(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
           dtMovein.DisplayDate = DateTime.Now;
           FutureRentsView = (System.Windows.Data.CollectionViewSource)this.FindResource("FutureRentsView");
        }

        private void formAddTenant_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            Building[] blds = Building.load(GlobalShared.user.Account.clientId);
            if (blds == null || blds.Length == 0)
            {
                MessageBox.Show("There do not appear to be any buildings in the database.\n\nThe building for which to add a tenant must exist first.\n\n Will now close.");
                this.Close();
            }
            lstBuilding.ItemsSource = blds;
            if (BuildingNumber != null && BuildingNumber.Trim() != "")
            {
                Building bld = null;
                for (int i = 0; i < lstBuilding.Items.Count; i++)
                {
                    bld = (Building)lstBuilding.Items[i];
                    if (BuildingNumber.Trim().Equals(bld.BuildingNumber.Trim()))
                    {
                        lstBuilding.SelectedIndex = i;
                        break;
                    }

                }
            }
            Types[] types = Types.loadAll(GlobalShared.user.Account.clientId, false);
            if (types == null || types.Length == 0)
            {
                bool breakout = false;
                while (!breakout)
                {
                    MessageBoxResult result = MessageBox.Show("There are no Tenant Type records.\n\nDo you want to create Type records now?", "", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            frmTypeCodes frm = new frmTypeCodes();
                            frm.ThisApp = m_ThisApp;
                            frm.ShowDialog();
                            types = Types.loadAll(GlobalShared.user.Account.clientId, false);
                            if (types != null && types.Length > 0)
                                breakout = true;
                            break;
                        case MessageBoxResult.Cancel:
                            breakout = true;
                            this.Close();
                            break;
                        case MessageBoxResult.No:
                            breakout = true;
                            break;
                    }
                }
            }

            lstTypes.ItemsSource = types;
        }
        
        private void txtFirstMonthRent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstMonthRent.Value > 0 &&
                txtFirstMonthRent.Value != txtFullMonthRent.Value  && 
                ((DateTime)dtMovein.SelectedDate).Day > 1 && (futures == null || futures.Length == 0))
            {
                int i = futures.Length;
                Array.Resize<FutureRents>(ref futures, i + 1);
                futures[i] = new FutureRents();
                DateTime futureDate = (DateTime)dtMovein.SelectedDate;
                int y = futureDate.Year;
                int m = futureDate.Month + 1;
                if (m > 12)
                {
                    y++;
                    m = 1;
                }
                futureDate = new DateTime(y, m, 1);
                futures[i].FutureDate = futureDate;
                futures[i].FutureAmount = txtFullMonthRent.Value;
                FutureRentsView.Source = futures;
            }
        }

        private void txtFullMonthRent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstMonthRent.Value == 0)
                txtFirstMonthRent.Value = txtFullMonthRent.Value;
        }
        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            int idx = grdFutureRents.SelectedIndex;
            if (idx >= 0)
            {
                if (MessageBox.Show("Delete this Future Rent?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    futures[idx] = null;
                }
            }
            FutureRentsView.Source = futures;
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            Array.Resize<FutureRents>(ref futures, futures.Length + 1);
            futures[futures.Length - 1] = new FutureRents();
            futures[futures.Length - 1].FutureDate = DateTime.Now;
            FutureRentsView.Source = futures;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this operation.");
                return;
            }
            if (lstBuilding.SelectedIndex < 0)
            {
                MessageBox.Show("You have not selected a Building.");
                return;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("You have not entered a Tenant Name");
                return;
            }
         
            if (oldTenant[0].isChanged)
            {
                if (MessageBox.Show("Do you want to save the old tenant's changed Rent amount?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    oldTenant[0].save();
            }
            Tenants tenant = new Tenants();
            tenant.MoveinDate = GlobalShared.NULL_DATE;
            tenant.LeaseStartDate = GlobalShared.NULL_DATE;
            tenant.LeaseExpirationDate = GlobalShared.NULL_DATE;
            tenant.ClientId = GlobalShared.user.Account.clientId;
            tenant.BuildingNumber = ((Building)(lstBuilding.SelectedItem)).BuildingNumber;
            tenant.TenantNumber = txtNumber.Text;
            tenant.TenantName = txtName.Text;
            tenant.Apartment= txtNewApartment.Text;
            tenant.Rooms = oldTenant[0].Rooms;
            tenant.SquareFeet = oldTenant[0].SquareFeet;
            tenant.DueDate = "01";
            tenant.MoveinDate = (DateTime)(dtMovein.SelectedDate == null ? GlobalShared.NULL_DATE : (DateTime)dtMovein.SelectedDate);
            tenant.LeaseExpirationDate = (DateTime)(dtLease.SelectedDate ==  null ? GlobalShared.NULL_DATE : (DateTime)dtLease.SelectedDate);
            tenant.SSNumber1 = txtSSNumber1.Text;
            tenant.SSNumber2 = txtSSNumber2.Text;
            tenant.SecMultiple = 1;
            tenant.XrfTested = oldTenant[0].XrfTested;
            tenant.XrfTestedDate = oldTenant[0].XrfTestedDate;
            tenant.XrfExempt = oldTenant[0].XrfExempt;
            tenant.XrfExemptDate = oldTenant[0].XrfExemptDate;
            tenant.XrfWorkComplete = oldTenant[0].XrfWorkComplete;
            tenant.XrfWorkCompleteDate = oldTenant[0].XrfWorkCompleteDate;
            if (chkDHCR.IsChecked.Value)
                TransferDHCR(oldTenant[0], ref tenant);
            if (dtStart.SelectedDate != null)
                tenant.LeaseStartDate = (DateTime)dtStart.SelectedDate;
            else
                tenant.LeaseStartDate = GlobalShared.NULL_DATE;
            tenant.Type = "";
            if (lstTypes.SelectedIndex > -1)
            {
                tenant.Type = ((Types)lstTypes.SelectedItem).Type;
            }
            tenant.RentLegal = txtLegalRent.Value;
            if (txtFirstMonthRent.Value > 0)
                tenant.Rent = txtFirstMonthRent.Value;
            else

                tenant.Rent = txtFullMonthRent.Value;
            if (replacing && oldTenant != null)
            {
                tenant.ReplacingTenant = oldTenant[0].SequenceNumber;
                tenant.NewTenantFlag = true;
            }
            if (tenant.save())
            {
                // Create new histmast
                Histmast hist = new Histmast();
                hist.BuildingNumber = tenant.BuildingNumber;
                hist.TenantNumber = tenant.TenantNumber;
                hist.TenantName = tenant.TenantName;
                hist.SequenceNumber = tenant.SequenceNumber;
                hist.Apartment = tenant.Apartment;
                hist.save();

                if (futures != null && futures.Length > 0)
                {
                    foreach (FutureRents future in futures)
                    {
                        if (future.FutureDate > GlobalShared.NULL_DATE && future.FutureAmount > 0)
                        {
                            future.TenantSequence = tenant.SequenceNumber;
                            future.save();
                        }
                    }
                }

                // Create new Security Record
                STTenants sten = new STTenants();
                sten.BuildingNumber = tenant.BuildingNumber;
                sten.TenantNumber = tenant.TenantNumber;
                sten.SequenceNumber = tenant.SequenceNumber;
                sten.TenantName = tenant.TenantName;
                sten.Apartment = tenant.Apartment;
                sten.Amount = 0;
                sten.SocialSecurityOne = tenant.SSNumber1;
                sten.SocialSecurityTwo = tenant.SSNumber2;
                sten.Interest = "";
                sten.ClientId = GlobalShared.user.Account.clientId;
                sten.save();

                frmTenant frm = new frmTenant() { sequenceNumber = tenant.SequenceNumber, Add = false };
                frm.ThisApp = m_ThisApp;
                frm.ShowDialog();
                ClearAllFields();
            }
            else
                MessageBox.Show("There was an error saving the New Tenant.");
            dirty = false;
        }

        private void TransferDHCR(Tenants old, ref Tenants add)
        {
            if (old == null || add == null)
                return;

            add.dhcrACOne = old.dhcrACOne;  
            add.dhcrACTwo = old.dhcrACTwo;
            add.dhcrDateOne = old.dhcrDateOne;
            add.dhcrDateTwo = old.dhcrDateTwo;
            add.dhcrDishwasherOne = old.dhcrDishwasherOne;
            add.dhcrDishwasherTwo = add.dhcrDishwasherTwo;
            add.dhcrInformedConsentOne = old.dhcrInformedConsentOne;
            add.dhcrInformedConsentTwo = old.dhcrInformedConsentTwo;
            add.dhcrOtherOne = old.dhcrOtherOne;
            add.dhcrOtherTwo = old.dhcrOtherTwo;
            add.dhcrOtherTextOne = old.dhcrOtherTextOne;
            add.dhcrOtherTextTwo = old.dhcrOtherTextTwo;
            add.dhcrRefrigeratorOne = old.dhcrRefrigeratorOne;
            add.dhcrRefrigeratorTwo = old.dhcrRefrigeratorTwo;
            add.dhcrRentOne = old.dhcrRentOne;  
            add.dhcrRentTwo = old.dhcrRentTwo;
            add.dhcrSubmittedDateOne = old.dhcrSubmittedDateOne;
            add.dhcrSubmittedDateTwo = old.dhcrSubmittedDateTwo;
            add.dhcrTotalCostOne = old.dhcrTotalCostOne;
            add.dhcrTotalCostTwo = old.dhcrTotalCostTwo;
            add.dhcrWindowsOne = old.dhcrWindowsOne;
            add.dhcrWindowsTwo = old.dhcrWindowsTwo;
            add.dhcrInformedConsentOne = old.dhcrInformedConsentOne;
            add.dhcrInformedConsentTwo = old.dhcrInformedConsentTwo;
            add.dhcrSubmittedOne = old.dhcrSubmittedOne;
            add.dhcrSubmittedTwo = old.dhcrSubmittedTwo;
            add.dhcrSubmittedDateOne = old.dhcrSubmittedDateOne;
            add.dhcrSubmittedDateTwo= old.dhcrSubmittedDateTwo;
        }
        private void ClearAllFields()
        {
            oldTenant = null;
            Array.Resize<FutureRents>(ref futures, 0);
            txtNumber.Text = "";
            txtName.Text = "";
            txtNewApartment.Text = "";
            txtLegalRent.Value = 0;
            txtFullMonthRent.Value = 0;
            txtFirstMonthRent.Value = 0;
            txtRooms.Text = "";
            dtLease.Text = "";
            dtMovein.Text = "";
            dtStart.Text = "";
            grdFutureRents.ItemsSource = null;
            grpReplacing.DataContext = null;
        }

        private void lstBuilding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lstBuilding.SelectedIndex >= 0 && txtNumber.Text.Trim() != "")
            {
                replacing = false;
                string bld = ((Building)lstBuilding.SelectedItem).BuildingNumber;
                oldTenant = Tenants.load(bld, txtNumber.Text);
                grpReplacing.DataContext = null;
                if (oldTenant != null && oldTenant.Length > 0)
                {
                    if (oldTenant.Length > 1)
                    {
                        MessageBox.Show("There are already " + oldTenant.Length.ToString() + " tenants with this building/tenant number.");
                        ClearAllFields();
                        txtNumber.Focus();
                        return;
                    }
                    replacing = true;
                    txtOldRent.IsEnabled = true;
                }
                else
                {
                    if (oldTenant == null || oldTenant.Length == 0)
                    {
                        oldTenant = new Tenants[1];
                        oldTenant[0] = new Tenants();
                        oldTenant[0].TenantName = "<* NOT REPLACING A TENANT *>";
                        txtOldRent.IsEnabled = false;
                    }
                }
                grpReplacing.DataContext = null;
                grpReplacing.DataContext = oldTenant[0];
                txtNewApartment.Text = oldTenant[0].Apartment;
                txtRooms.Text = oldTenant[0].Rooms.ToString();
                dirty = true;
            }
        }

        private void txtLegalRent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLegalRent.Value > 0 && txtFullMonthRent.Value == 0)
                txtFullMonthRent.Value = txtLegalRent.Value;
        }

        private void formAddTenant_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            if (dirty)
            {
                if (MessageBox.Show("You have not saved the last tenant. Do you want to save it now?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    cmdSave_Click(sender, e);
            }
            this.Close();
        }

        private void dtMovein_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtStart.SelectedDate == null)
                dtStart.SelectedDate = dtMovein.SelectedDate;
        }
    }
}
