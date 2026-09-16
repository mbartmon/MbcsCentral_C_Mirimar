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
using System.Windows.Forms;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmExternalApplications.xaml
    /// </summary>
    public partial class frmExternalApplications : Window
    {
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        private ExternalApplications[] apps = null;
        System.Windows.Data.CollectionViewSource AppsViewSource;
        public frmExternalApplications()
        {
            InitializeComponent();
            AppsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("AppsViewSource");
        }
        public frmExternalApplications(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
           AppsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("AppsViewSource");
        }
        private void FormExternalApplications_Loaded(object sender, RoutedEventArgs e)
        {

            Main.getForm(this);
           
            apps = ExternalApplications.load();
            if (apps != null)
                AppsViewSource.Source = apps;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (ExternalApplications app in AppsDataGrid.Items)
            {
                if (app.IsChanged)
                {
                    if (app.Delete)
                        app.delete(app.Id.ToString());
                    else
                        app.save();
                }
            }
            Main.buildMenu(GlobalShared.mainWindow);
            this.Close();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                System.Windows.MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            int i = AppsDataGrid.SelectedIndex;
            if (i > -1)
            {
                if (System.Windows.MessageBox.Show("Delete this Application?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ExternalApplications app = (ExternalApplications)AppsDataGrid.SelectedItem;
                    app.Delete = true;
                }
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                System.Windows.MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            int c = -1;
            int i = 0;
            if (apps != null)
            {
                i = apps.Length;
                foreach (ExternalApplications app in apps)
                {
                    if (app.Sequence > c)
                        c = app.Sequence;
                }
            }
            Array.Resize<ExternalApplications>(ref apps, ++i);
            apps[i - 1] = new ExternalApplications();
            apps[i - 1].Sequence = ++c;
            AppsViewSource.Source = apps;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FormExternalApplications_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);
        }

        private void externalApplicationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        // For Datagrid single click editing
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.DataGridCell cell = sender as System.Windows.Controls.DataGridCell;
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                System.Windows.Controls.DataGrid dataGrid = FindVisualParent<System.Windows.Controls.DataGrid>(cell);
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

        private void cmdPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog frm = new FolderBrowserDialog();
            frm.ShowDialog();
            ExternalApplications app = (ExternalApplications)AppsDataGrid.SelectedItem;
            app.ApplicationPath = frm.SelectedPath;
        }

        private void cmdReorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Array.Sort(apps, ExternalApplications.Sort);
                AppsDataGrid.DataContext = null;
                AppsDataGrid.DataContext = apps;
            }
            catch { }
        } 

    }
}
