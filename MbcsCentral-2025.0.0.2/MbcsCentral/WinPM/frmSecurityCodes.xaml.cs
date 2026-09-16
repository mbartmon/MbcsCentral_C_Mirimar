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
    /// Interaction logic for frmSecurityCodes.xaml
    /// </summary>
    public partial class frmSecurityCodes : Window
    {
        STTrans[] trans;
        STCodes[] codes;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmSecurityCodes()
        {
            InitializeComponent();
        }
        public frmSecurityCodes(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }

        private void FormSecurityCodes_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            codes = STCodes.load();
            grdCodes.ItemsSource = codes;
        }

        private void FormSecurityCodes_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            int i = grdCodes.SelectedIndex;
            if (i > -1)
            {
                if (MessageBox.Show("Delete this Security Code?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    STCodes code = (STCodes)grdCodes.Items[i];
                    code.delete(code.Key.ToString());
                    grdCodes.ItemsSource = null;
                    codes = STCodes.load();
                    grdCodes.ItemsSource = codes;
                }
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Array.Resize<STCodes>(ref codes, codes.Length + 1);
            codes[codes.Length - 1] = new STCodes() { IsNew = "True", isChanged = true};
            grdCodes.ItemsSource = null;
            grdCodes.ItemsSource = codes;
            grdCodes.SelectedIndex = grdCodes.Items.Count - 1;
            grdCodes.SelectedItem = grdCodes.Items[grdCodes.SelectedIndex];
            grdCodes.ScrollIntoView(grdCodes.SelectedItem);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            foreach(STCodes code in codes)
            {
                if (code.isChanged && (!code.Code.Trim().Equals("") && !code.Code.Trim().Equals("")))
                    code.save();
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
