using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for frmTypeCodes.xaml
    /// </summary>
    public partial class frmTypeCodes : Window
    {
        long clientId = GlobalShared.user.Account.clientId;
        Types[] typs;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmTypeCodes()
        {
            InitializeComponent();
        }
        public frmTypeCodes(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            typs = Types.loadAll(clientId);
            if (typs != null && typs.Length > 0)
                grdTypes.ItemsSource = typs;
            DocumentLayoutList[] docs = DocumentLayoutList.load(false, true);
            System.Windows.Data.CollectionViewSource source = ((System.Windows.Data.CollectionViewSource)(this.FindResource("leases")));
            source.Source = docs;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } foreach (Types typ in typs)
            {
                if (typ.DeleteMe)
                    typ.delete(typ.ID.ToString());
                else if (typ.IsChanged)
                    typ.save();
                grdTypes.ItemsSource = null;
                typs = Types.loadAll(clientId);
                grdTypes.ItemsSource = typs;
            }
            this.Close();        
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            
            Types typ = new Types();
            typ.clientId = clientId;
            try
            {
                Array.Resize<Types>(ref typs, typs.Length + 1);
                typs[typs.Length - 1] = typ;
                grdTypes.ItemsSource = null;
                grdTypes.ItemsSource = typs;
            }
            catch { }
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Main.saveForm(this);
        }

        private void grdTypes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid grd = (DataGrid)(sender);
            grd.ItemsSource = null;
            grd.ItemsSource = typs;

        }

        private void lstLeases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdTypes.SelectedIndex;
            if (n >= typs.Length || n < 0 || c.SelectedItem == null)
                return;

            switch  (c.Name)
            {
                case "lstLeases":
                    ((Types)typs[n]).LeaseName = ((DocumentLayoutList) c.SelectedItem).DocumentName;
                    break;
                case "lstReverse":
                    ((Types)typs[n]).LeaseReverse = ((DocumentLayoutList) c.SelectedItem).DocumentName;
                    break;
            }
        }
    }
}
