using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MbcsCentral.WinPM;
using Microsoft.VisualBasic;
using CSharp;
namespace MbcsCentral
{
    public partial class frmTenants
    {

        protected int seqNum;
        private int selectedIndex;

        bool NameSearch = false;

        private System.Threading.CancellationTokenSource cts;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmTenants()
        {
            InitializeComponent();
            this.Loaded += frmTenants_Loaded;
        }
        public frmTenants(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
            this.Loaded += frmTenants_Loaded;
        }

        public frmTenants(string parm, System.Threading.CancellationTokenSource cts)
        {
            InitializeComponent();
            this.cts = cts;
            m_ThisApp = (string)parm;
            this.Loaded += frmTenants_Loaded;
        }

        private void frmTenants_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.getForm(this);
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            chkVacant.IsChecked = (bool)((double)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\Options\\", "ShowVacant", 1) == 1 ? true : false);
            getTenantRecords();
            txtBuildingNumber.Focus();
            Mouse.SetCursor(Cursors.Arrow);
        }

        private void getTenantRecords()
        {

            Tenants[] tens;
            tens = Tenants.loadForNames(chkVacant.IsChecked.Value);
            if (tens != null && tens.Length > 0)
            {
                lstTenants.DataContext = null;
                lstTenants.DataContext = tens;
            }
            return;
            if (!MbcsCentral.GlobalShared.tenantNameInDelegate)
            {
                MbcsCentral.GlobalShared.tenantNamesTimer.Change(0, MbcsCentral.GlobalShared.TENANT_NAMES_INTERVAL);
            }
            TimeSpan sTime = DateAndTime.Now.TimeOfDay;
            TimeSpan cTime;
            while (MbcsCentral.GlobalShared.tenantNameInDelegate)
            {
                cTime = DateAndTime.Now.TimeOfDay;
                if (cTime - sTime > MbcsCentral.GlobalShared.WAIT_INTERVAL)
                {
                    Interaction.MsgBox("Timeout waiting for Tenant Records");
                    return;
                }
            }
            if ((MbcsCentral.GlobalShared.tenantNames != null) && MbcsCentral.GlobalShared.tenantNames.Length > 0)
            {
                lstTenants.DataContext = null;
                lstTenants.DataContext = MbcsCentral.GlobalShared.tenantNames;
            }
        }

        private void lstTenants_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstTenants.SelectedValue != null)
            {
                seqNum = (int)Convert.ChangeType(lstTenants.SelectedValue, typeof(int));
                selectedIndex = lstTenants.SelectedIndex;
                if (seqNum > 0)
                {
                    Tenants tenant = Tenants.load(seqNum);
                    if ((tenant != null))
                    {
                        txtBuildingNumber.Text = tenant.BuildingNumber;
                        txtTenantNumber.Text = tenant.TenantNumber;
                        txtName.Text = tenant.TenantName;
                        txtApt.Text = tenant.Apartment;
                    }
                }
            }
        }

        private void cmdEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NameSearch = false;
            if (seqNum > 0)
            {
                Tenants[] tens = CheckTenants(txtBuildingNumber.Text, txtTenantNumber.Text);
                if (tens != null && tens.Length > 1)
                {
                    frmChooseTenant frmChoose = new frmChooseTenant() { Tenants = tens };
                    frmChoose.ShowDialog();
                    if (frmChoose.SequenceNumber == -1)
                        return;
                    seqNum = frmChoose.SequenceNumber;
                }
                if (tens == null || tens.Length == 0)
                {
                    MessageBox.Show("Invalid Tenant Number");
                    return;
                }
                frmTenant frm = new frmTenant(m_ThisApp);
                frm.sequenceNumber = seqNum;
                frm.ShowDialog();
                getTenantRecords();
                lstTenants.SelectedIndex = selectedIndex;
                txtBuildingNumber.Focus();
                txtNameSearch.Text = "";
                return;
            }
            MessageBox.Show("Invalid Tenant Number entered.");
            txtBuildingNumber.Focus();
        }

        private Tenants[] CheckTenants(string bld, string ten)
        {
            //return Tenants.load(bld, ten, GlobalShared.user.Account.clientId);
            return Tenants.load(bld, ten);
        }
        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtBuildingNumber_LostFocus(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (NameSearch) return;
            Tenants l = null;
            for (int i = 0; i <= lstTenants.Items.Count - 1; i++)
            {
                l = (Tenants)lstTenants.Items[i];
                if (l.BuildingNumber.Trim().Equals(txtBuildingNumber.Text.Trim()) & l.TenantNumber.ToLower().Trim().Equals(txtTenantNumber.Text.ToLower().Trim()))
                {
                    lstTenants.SelectedIndex = i;
                    break;
                }
            }
        }

        private void txtTenantNumber_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!NameSearch && e.Key == System.Windows.Input.Key.Return)
            {
                txtBuildingNumber_LostFocus(sender, new RoutedEventArgs());
                cmdEdit_Click(sender, new RoutedEventArgs());
            }
        }

        private void txtName_PreviewTextInput(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string t = txtNameSearch.Text.ToLower();
            Tenants dr = null;
            for (int i = 0; i <= lstTenants.Items.Count - 1; i++)
            {
                dr = (Tenants)lstTenants.Items[i];
                if (dr.TenantName.ToString().ToLower().StartsWith(t))
                {
                    lstTenants.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cmdHistory_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (seqNum > 0)
            {
                frmHistoryDisplay frm = new frmHistoryDisplay();
                frm.TenantId = seqNum;
                frm.Show();
            }
        }

        private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //frmTenant frm = new frmTenant(m_ThisApp);
            //frm.Add = true;
            frmAddTenant frm = new frmAddTenant(m_ThisApp);
            frm.ShowDialog();
            getTenantRecords();
        }

        private void cmdMaint_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuildingNumber.Text.Trim()) & !string.IsNullOrEmpty(txtTenantNumber.Text.Trim()))
            {
                frmMaintenance frm = new frmMaintenance(m_ThisApp);
                frm.BuildingNumber = txtBuildingNumber.Text;
                frm.TenantNumber = txtTenantNumber.Text;
                frm.TenantId = seqNum;
                frm.Show();
            }

        }

        public void Delete_Tenant()
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have have permission to perform this operation.");
                return;
            }

            string xsql = null;
            string x = null;

            Tenants ten = Tenants.load(seqNum);
            if (ten != null)
            {
                TenantsArchive tenArch = ten.copyToArchive();
                tenArch.save();
                ten.delete(seqNum.ToString());
                tenArch = null;
                ten = null;
            }
        }

        public long[] Erase_Tenant()
        {
            long[] counts = { 0, 0, 0 };
            Tenants ten = Tenants.load(seqNum);
            if (ten != null)
            {
                counts = ten.TenantDeleteAll();
                ten = null;
            }
            return counts;
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (seqNum < 1)
            {
                MessageBoxCustom.Show("You must select a Tenant to remove.");
                return;
            }
            if (MessageBoxCustom.Show("Do you really want to remove Tenant #" + txtBuildingNumber.Text + txtTenantNumber.Text + "?  keeping all history?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Delete_Tenant();
                getTenantRecords();
            }
            txtBuildingNumber.Focus();
        }

        private void cmdErase_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (seqNum < 1)
            {
                MessageBoxCustom.Show("You must select a Tenant to erase.");
                return;
            }
            if (MessageBoxCustom.Show("Do you really want to ERASE Tenant #" + txtBuildingNumber.Text + txtTenantNumber.Text + "?  DELETEING all history?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WaitWindow wait = new WaitWindow();
                wait.Item = "Please wait while Tenant records are erased...";
                wait.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                wait.Show();
                long[] counts = { 0, 0, 0 };
                counts = Erase_Tenant();
                wait.Close();
                string[] s = { "ERASE Tenant Completed.", "", counts[0].ToString().PadLeft(5) + " History Charge Records deleted.", counts[1].ToString().PadLeft(5) + " History Payent Records deleted.", counts[2].ToString().PadLeft(5) + " Security Transactions deleted." };
                MessageBoxCustom.Show(s, "");
                getTenantRecords();
            }
            txtBuildingNumber.Focus();

        }

        private void cmdVacate_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (seqNum < 1)
            {
                MessageBox.Show("You must select a Tenant to vacate.");
                return;
            }
            Tenants ten = Tenants.load(seqNum);
            if (ten == null)
            {
                MessageBox.Show("Tenant record not found.");
                return;
            }
            frmVacate frm = new frmVacate();
            frm.TenantNumber = ten.BuildingNumber + ten.TenantNumber;
            frm.Apartment = ten.Apartment;
            frm.TenantName = ten.TenantName;
            frm.Rent = ten.Rent;
            frm.Ten = ten;
            frm.ShowDialog();

            getTenantRecords();
            txtBuildingNumber.Focus();

        }

        private void moveNextOnMaxlength(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);
        }

        private void formTenants_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);
            Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\Options\\", "ShowVacant", (double)(chkVacant.IsChecked.Value ? 1 : 0), (int)Registry.DataTypes.Dword);
            /*
            if (cts != null)
            {
                try
                {
                    cts.Cancel();
                }
                catch { }
                try

                {
                    cts.Token.ThrowIfCancellationRequested();
                }
                catch(System.Threading.Tasks.TaskCanceledException tx)
                {
                  
                }
                catch (OperationCanceledException ex) { cts.Dispose(); }
                
            }
            */
        }

        private void chkVacant_Click(object sender, RoutedEventArgs e)
        {
            getTenantRecords();
        }

        private void cmdLabels_Click(object sender, RoutedEventArgs e)
        {
            frmTenantLabels frm = new frmTenantLabels();
            frm.Show();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NameSearch = false;
            TextBox txt = (TextBox)sender;
            string s = txt.Text.Trim();
            txt.Text = s;
            txt.SelectionStart = 0;
            txt.SelectionLength = s.Length;
        }

        private void txtNameSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            NameSearch = true;
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            string t = txtNameSearch.Text.ToLower();
            Tenants dr = null;
            for (int i = 0; i <= lstTenants.Items.Count - 1; i++)
            {
                dr = (Tenants)lstTenants.Items[i];
                if (dr.TenantName.ToString().ToLower().StartsWith(t))
                {
                    lstTenants.SelectedIndex = i;
                    lstTenants.ScrollIntoView(lstTenants.SelectedItem);
                    break;
                }
            }
        }

        private void cmdCheckABA_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("Check ABA Routing Numbers for ALL Tenants?","", MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
            {
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                list.Add( "Invalid ABA Routing numbers found:");
                list.Add("");
                ItemCollection itms = lstTenants.Items;
                foreach (Tenants ten in itms)
                {
                    Tenants tenant = Tenants.load(ten.SequenceNumber);
                    if (tenant != null && !tenant.Bank.Trim().Equals(""))
                    {
                        if (!GlobalShared.validateRoutingNumber(tenant.Bank))
                        {
                            list.Add(tenant.BuildingTenantNumber.PadLeft(12) + tenant.TenantName.PadLeft(35) + " ABA: " + tenant.Bank);
                        }
                    }
                }
                if (list.Count <= 2)
                    MessageBoxCustom.Show("No invalid ABA Routing numbers found.");
                else
                {
                    MessageBoxCustom.Show((string[])list.ToArray(typeof(string)),"");
                }
            }
        }
    }
}
