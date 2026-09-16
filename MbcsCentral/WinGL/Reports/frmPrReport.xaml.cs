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


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmPrReport.xaml
    /// </summary>
    public partial class frmPrReport : Window
    {
        string className = "";
        Classes cls;
        PayrollReportParms parms = null;
        long acctId = GlobalShared.user.Account.accountId;
        int clientId = (int)GlobalShared.user.Account.clientId;

        PRaccounts prAccounts;
        int NumberOfAccounts = 10;
        PayrollReportAccounts[] reportAccounts = new PayrollReportAccounts[9];

        public frmPrReport()
        {
            InitializeComponent();
        }

        string thisApp;

        public frmPrReport (string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frmPrReport_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = PayrollReportParms.DeSerialize(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new PayrollReportParms();
            dtStart.SelectedDate = parms.StartDate;
            dtEnd.SelectedDate = parms.EndDate;
            chkYTD.IsChecked = parms.YTD;
            Accounts[] accounts = Accounts.load(true, false);
            //CollectionViewSource accts = (CollectionViewSource)this.FindResource("accounts");
            //accts.Source = accounts;
            prAccounts = PRaccounts.load();
            if (prAccounts == null)
                prAccounts = new PRaccounts();
            ComboBox box = null;
            int c = 0;
            for (int i = 0; i < 9; i++)
            {
                reportAccounts[i] = new PayrollReportAccounts();
                reportAccounts[i].account1 = Accounts.load(prAccounts["Account" + c.ToString("00")]);
                box = FindChild<ComboBox>(this, "lstAccounts" + c.ToString("00"));
                box.ItemsSource = accounts;
                if (reportAccounts[i].account1 != null)
                    box.SelectedIndex = FindSelection(box, reportAccounts[i].account1.Key);
                c++;
                reportAccounts[i].account2 = Accounts.load(prAccounts["Account" + c.ToString("00")]);
                box = FindChild<ComboBox>(this, "lstAccounts" + c.ToString("00"));
                box.ItemsSource = accounts;
                if (reportAccounts[i].account2 != null)
                    box.SelectedIndex = FindSelection(box, reportAccounts[i].account2.Key);
                c++;
            }
            //grdAccounts.DataContext = reportAccounts;
        }

        private void SaveAccounts()
        {
            ComboBox box = null;
            int c = 0;
            for (int i = 0; i < 9; i++)
            {
                box = FindChild<ComboBox>(this, "lstAccounts" + c.ToString("00"));
                if (box.SelectedIndex >= 0)
                    prAccounts["Account" + c.ToString("00")] = ((Accounts)box.SelectedItem).Key;
                else
                    prAccounts["Account" + c.ToString("00")] = -1;
                c++;
                box = FindChild<ComboBox>(this, "lstAccounts" + c.ToString("00"));
                if (box.SelectedIndex >= 0)
                    prAccounts["Account" + c.ToString("00")] = ((Accounts)box.SelectedItem).Key;
                else
                    prAccounts["Account" + c.ToString("00")] = -1;
                c++;
            }
            prAccounts.save();
        }
        private void frmPrReport_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            SaveAccounts();
            parms.StartDate = (DateTime)dtStart.SelectedDate;
            parms.EndDate = (DateTime)dtEnd.SelectedDate;
            parms.YTD = chkYTD.IsChecked.Value;
            cls.value = parms.Serialize();
            cls.clientId = clientId;
            cls.acctId = acctId;
            cls.name = className;
            cls.save();
            this.Cursor = Cursors.Arrow;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PayrollReport rpt = new PayrollReport() { StartDate = ((DateTime)dtStart.SelectedDate).Date, EndDate = ((DateTime)dtEnd.SelectedDate).Date, YTD = chkYTD.IsChecked.Value };
            rpt.print_report();
            this.Cursor = Cursors.Arrow;
        }

        private int FindSelection(ComboBox box, int key)
        {
            int item = -1;
            if (key < 0)
                return item;
            for (int i = 0; i < box.Items.Count; i++)
            {
                if (((Accounts)box.Items[i]).Key == key)
                {
                    item = i;
                    break;
                }
            }
            return item;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }

    public class PayrollReportAccounts
    {
        public Accounts account1 { get; set; }
        public Accounts account2 { get; set; }
    }
}
