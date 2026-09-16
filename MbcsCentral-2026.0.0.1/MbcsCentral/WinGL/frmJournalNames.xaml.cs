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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmJournalNames.xaml
    /// </summary>
    public partial class frmJournalNames : Window
    {
        JournalNames[] journals = JournalNames.load();
        ArrayList list = new ArrayList();

        private string m_ThisApp;

        public frmJournalNames()
        {
            InitializeComponent();
        }

        public frmJournalNames(string thisApp)
        {
            InitializeComponent();
            m_ThisApp = thisApp;
        }

        private void formJournalNames_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            if (journals != null && journals.Length > 0)
                foreach (JournalNames jrn in journals)
                    list.Add(jrn);
            grdJournals.ItemsSource = list;
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new JournalNames());
            grdJournals.ItemsSource = null;
            grdJournals.ItemsSource = list;
        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
            	MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            JournalNames jrn = (JournalNames)grdJournals.SelectedItem;
            if (jrn != null)
            {
                if (MessageBox.Show("Delete selected Journal?","", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    jrn.Delete = true;
                }

            }
        }

        private void formJournalNames_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
        
            JournalNames jrn;

            for (int i = 0; i < list.Count; i++)
            {
                jrn = (JournalNames)list[i];
                if (jrn.Delete)
                {
                    jrn.IsChanged = false;
                    jrn.delete(jrn.Id.ToString());
                }
                else if (jrn.IsChanged)
                {
                    jrn.IsChanged = false;
                    jrn.save();
                }
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (((JournalNames)list[i]).IsChanged)
                {
                    if (MessageBox.Show("Save your changes?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        cmdSave_Click(sender, e);
                        break;
                    }
                }
            }
            this.Close();
        }
    }
}
