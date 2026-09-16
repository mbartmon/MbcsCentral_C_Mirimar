using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmPreferredRent.xaml
    /// </summary>
    public partial class frmPreferredRent : Window
    {

        long clientId = GlobalShared.user.Account.clientId;
        Tenants[] tens;
        static DateTime cDate = System.DateTime.Now;
        DateTime startDate = (new DateTime(cDate.Year, cDate.Month, 1).AddMonths(4)).AddDays(-1);
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmPreferredRent()
        {
            InitializeComponent();
            dtLeaseDate.DisplayDate = startDate;
            dtLeaseDate.Text = startDate.ToString();
            dtLeaseDate.SelectedDate = startDate;
        }
        public frmPreferredRent(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void cmdGet_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            grdTenants.DataContext = null;
            if (dtLeaseDate.SelectedDate == null)
            {
                MessageBox.Show("Please select an Expiration Date");
                return;
            }
            string startBuilding = txtStartingBuilding.Text;
            string endBuilding = txtEndingBuilding.Text;
            DateTime leaseDate = (DateTime)dtLeaseDate.SelectedDate;
            worker.DoWork += (o, ea) => 
                {
                    tens = Tenants.loadForStatements(startBuilding, endBuilding, clientId, (DateTime?)leaseDate);
                };
            worker.RunWorkerCompleted += (o, ea) =>
                {
                    grdTenants.DataContext = tens;
                    _busy.IsBusy = false;
                };
            _busy.Focus();
            _busy.IsBusy = true;
            worker.RunWorkerAsync();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (tens == null || tens.Length == 0)
                return;
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            
            foreach (Tenants ten in tens)
            {
                if (ten.isChanged)
                {
                    ten.save();
                    ten.isChanged = false;
                }
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tenants ten = (Tenants)grdTenants.SelectedItem;
            if (ten != null)
            {
                Tenants oten = Tenants.load(ten.SequenceNumber);
                ten.ProposedRent = oten.ProposedRent;
                ten.ProposedRent2 = oten.ProposedRent2;
                ten.ProposedRent2_1 = oten.ProposedRent2_1;
                ten.PromptPay1 = oten.PromptPay1;
                ten.PromptPay2 = oten.PromptPay2;
                grdTenants.Items.Refresh();
            }
        }

        private void formPreferredRent_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            txtStartingBuilding.Focus();
        }

        private void formPreferredRent_Closing(object sender, CancelEventArgs e)
        {
            bool changed = false;
            if (tens != null)
                foreach (Tenants ten in tens)
                {
                    if (ten.isChanged)
                    {
                        changed = true;
                        break;
                    }
                }
            if (changed)
            {
                var rc = MessageBox.Show("You have made changes. Do you want to save them now?", "", MessageBoxButton.YesNoCancel);
                if (rc == MessageBoxResult.Yes)
                    cmdSave_Click(sender, new RoutedEventArgs());
                if (rc == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
            Main.saveForm(this);
        }

        private void txtStartingBuilding_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }
    }
}
