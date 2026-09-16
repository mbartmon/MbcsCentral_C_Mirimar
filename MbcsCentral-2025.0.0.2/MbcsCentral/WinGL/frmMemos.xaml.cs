using System;
using System.Collections;
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
    /// Interaction logic for frmMemos.xaml
    /// </summary>
    public partial class frmMemos : Window
    {
        // Properties
        public long VendorNumber { get; set; }
        public string VendorName { get; set; }
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        // Local Variables
        private ArrayList memolist = null;

        public frmMemos()
        {
            InitializeComponent();
        }
        public frmMemos (string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }

        private void formMemos_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            txtVendorName.Text = VendorName;

            memolist = Memos.loadByVendorList(VendorNumber);
            if (memolist != null)
                grdMemos.ItemsSource = (Memos[])memolist.ToArray(typeof(Memos));

            Company[] companies = Company.load(true);
            System.Windows.Data.CollectionViewSource companiesSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("companies")));
            companiesSource.Source = companies;

        }
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            foreach (Memos memo in memolist)
            {
                if (memo.Delete)
                {
                    memo.delete(memo.MemoId.ToString());
                    memo.Delete = false;
                    memo.IsChanged = false;
                }
                else if (memo.IsChanged)
                {
                    memo.save();
                    memo.IsChanged = false;
                }
            }
                this.Close();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void formMemos_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            bool changed = false;

            if (memolist == null)
                return;

            foreach (Memos memo in memolist)
            {
                if (memo.IsChanged || memo.Delete)
                {
                    changed = true;
                }
            }
            if (changed)
            {
                MessageBoxResult result = MessageBox.Show("There are unsaved changes. Save them now?", "", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, new RoutedEventArgs());
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                        break;
                }
            }
        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
	            MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            int index = grdMemos.SelectedIndex;
            /*
            if (index >= 0)
            {
                Memos memo = (Memos)grdMemos.SelectedItem;
                memo.Delete = !memo.Delete;
            }
            */
            if (grdMemos.SelectedItems.Count > 0)
            {
                for (int i = 0; i < grdMemos.SelectedItems.Count; i++)
                {
                    Memos memo = (Memos)grdMemos.SelectedItems[i];
                    memo.Delete = !memo.Delete;
                }
            }
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            if (memolist == null)
                memolist = new ArrayList();
            grdMemos.ItemsSource = null;
            Memos m = new Memos() { VendorNumber = (int)VendorNumber };
            memolist.Add( m);
            grdMemos.ItemsSource = (Memos[])memolist.ToArray(typeof(Memos));
        }


        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdMemos.SelectedIndex;
            if (n >= memolist.Count || n < 0 || c.SelectedItem == null || c.SelectedValue == null)
                return;

            switch (c.Name)
            {
                case "lstDepartments":
                    ((Memos)memolist[n]).DepartmentName = ((Department)c.SelectedItem).Name;
                    ((Memos)memolist[n]).DepartmentNumber = ((Department)c.SelectedItem).DepartmentNumber;
                    break;
                case "lstCompanies":
                    ((Memos)memolist[n]).CompanyName = ((Company)c.SelectedItem).Name;
                    ((Memos)memolist[n]).CompanyNumber = ((Company)c.SelectedItem).CompanyNumber;
                    short cmp = ((Company)c.SelectedItem).CompanyNumber;
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

        private void lstDepartments_DropDownOpened(object sender, EventArgs e)
        {
        }

        private void lstDepartments_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            c.SelectedIndex = 1;
        }

    }
}
