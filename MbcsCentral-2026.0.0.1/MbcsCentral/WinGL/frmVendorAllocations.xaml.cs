using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmVendorAllocations.xaml
    /// </summary>
    public partial class frmVendorAllocations : Window
    {
        //private ArrayList allocationList;
        VendorCategories[] allocationList;
        private VendorCategories allocation;
        private ArrayList deletion_ids = new ArrayList();

        public int VendorNumber
        { get; set; }
        public string VendorName
        { get; set; }

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        
        public frmVendorAllocations()
        {
            InitializeComponent();
        }

        public frmVendorAllocations(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }

        private void formVendorAllocations_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            txtVendorName.Text = VendorName;

            Company[] companies = Company.load(true);
            System.Windows.Data.CollectionViewSource companiesSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("companies")));
            companiesSource.Source = companies;

            Accounts[] accounts = Accounts.load(true);
            System.Windows.Data.CollectionViewSource accountsSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accounts")));
            accountsSource.Source = accounts;

            JournalNames[] journals = JournalNames.load(true);
            System.Windows.Data.CollectionViewSource journalsSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("journals")));
            journalsSource.Source = journals;

            allocationList = VendorCategories.loadByVendor(VendorNumber, true);
            if (allocationList != null && allocationList.Length > 0)
            {
                grdAllocations.ItemsSource = allocationList;
            }
        }
        private VendorCategories[] RemoveAt(VendorCategories[] source, int index)
        {
            VendorCategories[] dest = new VendorCategories[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
        private void formVendorAllocations_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);   
        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            if (grdAllocations.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Delete this allocation?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int i = grdAllocations.SelectedIndex;
                    deletion_ids.Add(((VendorCategories)allocationList[i]).Id);
                    grdAllocations.ItemsSource = null;
                    allocationList = RemoveAt(allocationList, i);
                    grdAllocations.ItemsSource = allocationList;
                }

            }
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            if (allocationList == null)
            {
                allocationList = new VendorCategories[0];
            }
            Array.Resize<VendorCategories>(ref allocationList, allocationList.Length + 1);
            allocationList[allocationList.Length - 1] = new VendorCategories();
            allocationList[allocationList.Length - 1].VendorNumber = VendorNumber;
            grdAllocations.ItemsSource = null;
            grdAllocations.ItemsSource = allocationList;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            if (deletion_ids.Count > 0)
                for (int i = 0; i < deletion_ids.Count; i++)
                    VendorCategories.delete((int)deletion_ids[i]);

            if (grdAllocations.Items.Count > 0)
            {
                VendorCategories v;
                foreach (VendorCategories dr in grdAllocations.Items)
                {
                    dr.save();
                    //v = new VendorCategories(VendorNumber, dr.company, dr.department, dr.account, dr.Id, dr.amount, dr.journal);
                    //v.save();
                }
            }
            this.Close();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            var obj = sender;

        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdAllocations.SelectedIndex;
            if (n >= allocationList.Length || n < 0 || c.SelectedItem == null)
                return;

            switch (c.Name)
            {
                case "lstAccounts" :
                    ((VendorCategories) allocationList[n]).AccountName = ((Accounts) c.SelectedItem).Description;
                    ((VendorCategories) allocationList[n]).Account = ((Accounts) c.SelectedItem).Key;
                    break;
                case "lstDepartments" :
                    ((VendorCategories)allocationList[n]).DepartmentName = ((Department)c.SelectedItem).Name;
                    ((VendorCategories)allocationList[n]).Department = ((Department)c.SelectedItem).DepartmentNumber;
                    break;
                case "lstCompanies" :
                    ((VendorCategories) allocationList[n]).CompanyName =  ((Company) c.SelectedItem).Name;
                    ((VendorCategories) allocationList[n]).Company =  ((Company) c.SelectedItem).CompanyNumber;
                    short cmp = ((Company) c.SelectedItem).CompanyNumber;
                    Department[] departments = Department.loadByCompany(cmp, true);
                    System.Windows.Data.CollectionViewSource departmentsSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("departments")));
                    departmentsSource.Source = departments;
                    break;
            }
        }

        //
        // SINGLE CLICK EDITING
        //
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                DataGridCell cell = sender as DataGridCell;
                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            if (deletion_ids.Count > 0)
                for (int i = 0; i < deletion_ids.Count; i++)
                    VendorCategories.delete((int)deletion_ids[i]);

            if (grdAllocations.Items.Count > 0)
            {
                VendorCategories v;
                foreach (VendorCategories dr in grdAllocations.Items)
                {
                    dr.save();
                }
                grdAllocations.ItemsSource = null;
                allocationList = VendorCategories.loadByVendor(VendorNumber, true);
                if (allocationList != null && allocationList.Length > 0)
                {
                    grdAllocations.ItemsSource = allocationList;
                }
            }

        }
    }
}
