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
using System.Windows.Forms;
using Microsoft.Win32;


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmOptions.xaml
    /// </summary>
    public partial class frmOptions : Window
    {
        Control control = Control.loadOne();
        Accounts[] accts;
        JournalNames[] journals;

        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmOptions(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        private void frmOptions_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            grdOptions.DataContext = control;

            // Posting
            accts = Accounts.load();
            lstAccounts.ItemsSource = accts;
            journals = JournalNames.load();
            lstJournals.ItemsSource = journals;
            if (control.GLMethod == 0)
                optInvoice.IsChecked = true;
            else
                optBatch.IsChecked = true;
            // Trans Edit List
            SetSortOptions();
        }

        private void frmOptions_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to edit this item.");
                return;
            }
            SaveSortOptions();
            control.save();
            this.Close();
        }

        private void SetSortOptions()
        {
            TransEditSortClass[] sorts = new TransEditSortClass[WinglGlobal.TransEditListSorts.Length];
            for (int i = 0; i < WinglGlobal.TransEditListSorts.Length; i++)
            {
                sorts[i] = new TransEditSortClass();
                sorts[i].Name = WinglGlobal.TransEditListSorts[i];
            }
            lstSorts1.ItemsSource = sorts;
            lstSorts2.ItemsSource = sorts;
            lstSorts3.ItemsSource = sorts;
            lstSorts4.ItemsSource = sorts;
            lstSorts5.ItemsSource = sorts;
            lstSorts6.ItemsSource = sorts;

            if (control.EdListSort1_dir == null || control.EdListSort1_dir.Trim().Equals("") || control.EdListSort1_dir == "ASC" )
                optAsc1.IsChecked = true;
            else
                optDes1.IsChecked = true;
            if (control.EdListSort2_dir == null || control.EdListSort2_dir.Trim().Equals("") || control.EdListSort2_dir == "ASC")
                optAsc2.IsChecked = true;
            else
                optDes2.IsChecked = true;
            if (control.EdListSort3_dir == null || control.EdListSort3_dir.Trim().Equals("") || control.EdListSort3_dir == "ASC")
                optAsc3.IsChecked = true;
            else
                optDes3.IsChecked = true;
            if (control.EdListSort4_dir == null || control.EdListSort4_dir.Trim().Equals("") || control.EdListSort4_dir == "ASC")
                optAsc4.IsChecked = true;
            else
                optDes4.IsChecked = true;
            if (control.EdListSort5_dir == null || control.EdListSort5_dir.Trim().Equals("") || control.EdListSort5_dir == "ASC")
                optAsc5.IsChecked = true;
            else
                optDes5.IsChecked = true;
            if (control.EdListSort6_dir == null || control.EdListSort6_dir.Trim().Equals("") || control.EdListSort6_dir == "ASC")
                optAsc6.IsChecked = true;
            else
                optDes6.IsChecked = true;
        }

        private void SaveSortOptions()
        {
            control.EdListSort1_dir = (optDes1.IsChecked.Value ? "DESC" : "ASC");
            control.EdListSort2_dir = (optDes2.IsChecked.Value ? "DESC" : "ASC");
            control.EdListSort3_dir = (optDes3.IsChecked.Value ? "DESC" : "ASC");
            control.EdListSort4_dir = (optDes4.IsChecked.Value ? "DESC" : "ASC");
            control.EdListSort5_dir = (optDes5.IsChecked.Value ? "DESC" : "ASC");
            control.EdListSort6_dir = (optDes6.IsChecked.Value ? "DESC" : "ASC");

        }
        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void optInvoice_Checked(object sender, RoutedEventArgs e)
        {
            control.GLMethod = 0;
        }

        private void optBatch_Checked(object sender, RoutedEventArgs e)
        {
            control.GLMethod = 1;
        }

        private void CmdWindoc_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (control.WindocLocation.Trim() != "")
            {
                dlg.SelectedPath = control.WindocLocation;
                try
                {
                    dlg.SelectedPath = control.WindocLocation.Substring(0, control.WindocLocation.LastIndexOf("\\"));
                }
                catch { }
            }
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                control.WindocLocation = dlg.SelectedPath;
                CheckDocs.IsChecked = true;
            }


        }
    }
}
