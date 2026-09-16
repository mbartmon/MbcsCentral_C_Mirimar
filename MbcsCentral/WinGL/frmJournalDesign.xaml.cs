using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmJournalDesign.xaml
    /// </summary>
    public partial class frmJournalDesign : Window
    {
        private string m_ThisApp;

        public frmJournalDesign(string thisApp)
        {
            InitializeComponent();
            m_ThisApp = thisApp;
        }

        public frmJournalDesign()
        {
            InitializeComponent();
        }

        private void frmJournalDesign_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            Journals[] journalList = Journals.load(true);
            lstReports.ItemsSource = journalList;
            JournalFields[] fields = JournalFields.BuildFields();
            System.Windows.Data.CollectionViewSource Fields = (System.Windows.Data.CollectionViewSource)this.FindResource("Fields");
            Fields.Source = fields;
            ArrayList JournalFieldTypes = FieldTypes.Fields();
            System.Windows.Data.CollectionViewSource fieldType = (System.Windows.Data.CollectionViewSource)this.FindResource("FieldTypes");
            fieldType.Source = JournalFieldTypes;
        }

        private void frmJournalDesign_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to save data.");
                return;
            }
            if (lstLayout.Items.Count > 0)
            {
                Journals jrn = (Journals)lstLayout.Items[0];
                Journals[] journals = Journals.load(jrn.Layout);
                if (journals != null && journals.Length > 0)
                {
                    foreach (Journals journal in journals)
                        journal.delete(journal.Id.ToString());
                }
                    short i = 0;
                    foreach (Journals j in lstLayout.Items)
                    {
                        jrn = new Journals(i++, j.Field, j.Type, j.Size, 0, j.Range, j.PrintFlag, j.Layout, j.Heading);
                        jrn.save();
                    }
                    lstLayout.ItemsSource = null;
            
            }
            Journals[] journalList = Journals.load(true);
            lstReports.ItemsSource = null;
            lstReports.ItemsSource = journalList;
            lstReports.SelectedIndex = -1;
            txtCurrentLayout.Text = "";
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdRange_Click(object sender, RoutedEventArgs e)
        {
            Journals row = (Journals)lstLayout.SelectedItem;
            frmAccountRanges frm = new frmAccountRanges(m_ThisApp);
            frm.ReportType = "Journals";
            frm.ReportName = row.Layout;
            frm.ReportSection = row.Column;
            frm.ShowDialog();
            row.Range = frm.Saved;
        }

        private void lstReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Journals journal = (Journals)lstReports.SelectedItem;
            if (journal != null)
            {
                lstLayout.ItemsSource = null;
                Journals[] journals = Journals.load(journal.Layout);
                lstLayout.ItemsSource = journals;
                lstReports.SelectedIndex = -1;
                txtCurrentLayout.Text = journal.Layout;
            }
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            Journals[] jrn = (Journals[])lstLayout.ItemsSource;
            Journals[] journals = new Journals[jrn.Length + 1];
            for (short i = 0; i < jrn.Length; i++)
                journals[i] = jrn[i];
            journals[jrn.Length] = new Journals() { Layout = jrn[0].Layout, Column = (short)jrn.Length };
            lstLayout.ItemsSource = null;
            lstLayout.ItemsSource = journals;
        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = lstLayout.SelectedIndex;
            if (i >= 0)
            {
                Journals[] jrn = (Journals[])lstLayout.ItemsSource;
                Journals[] journals = new Journals[jrn.Length - 1];
                int k = -1;
                for (int j = 0; j < jrn.Length; j++)
                {
                    if (j != i)
                    {
                        if (k == -1)
                            k = 0;
                        journals[k++] = jrn[j];
                    }
                }
                lstLayout.ItemsSource = null;
                lstLayout.ItemsSource = journals;
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (MessageBoxCustom.Show("Enter name of new LAYOUT", out name, 16) == MessageBoxResult.OK)
            {
                Journals[] journals = new Journals[1];
                journals[0] = new Journals() { Layout = name, Column = 0, Size = GetFieldSize(WinglGlobal.JournalFields[0])};
                lstLayout.ItemsSource = null;
                lstLayout.ItemsSource = journals;
                txtCurrentLayout.Text = name;

            }
        }

        private void ctxMoveUp_Click(object sender, RoutedEventArgs e)
        {
            int idx = lstLayout.SelectedIndex;
            if (idx > 0)
            {
                Journals[] jrn = (Journals[])lstLayout.ItemsSource;
                Journals swap = jrn[idx];
                jrn[idx] = jrn[idx - 1];
                jrn[idx - 1] = swap;
                for (short i = 0; i < jrn.Length; i++)
                    jrn[i].Column = i;
                lstLayout.ItemsSource = null;
                lstLayout.ItemsSource = jrn;
            }
        }

        private void ctxMoveDown_Click(object sender, RoutedEventArgs e)
        {
            int idx = lstLayout.SelectedIndex;
            if (idx < lstLayout.Items.Count - 1)
            {
                Journals[] jrn = (Journals[])lstLayout.ItemsSource;
                Journals swap = jrn[idx];
                jrn[idx] = jrn[idx + 1];
                jrn[idx + 1] = swap;
                for (short i = 0; i < jrn.Length; i++)
                    jrn[i].Column = i;
                lstLayout.ItemsSource = null;
                lstLayout.ItemsSource = jrn;
            }

        }

        private void lstFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox fields = (ComboBox)sender;
            if (fields.SelectedIndex >= 0)
            {
                JournalFields field = (JournalFields)fields.SelectedItem;
                short size = GetFieldSize(field.Item);
                if (size > 0 && lstLayout.SelectedItem != null)
                {
                    Journals jrn = (Journals)lstLayout.SelectedItem;
                    jrn.Size = size;
                }

            }
        }
        private short GetFieldSize(string s)
        {
            string[] s1 = s.Split('(');
            short size = 0;
            if (s1.Length == 2)
            {
                string[] s2 = s1[1].Split(')');
                short.TryParse(s2[0], out size);
            }
            return size;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("DELETE this Layout: " + txtCurrentLayout.Text ,"",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Journals[] journals = (Journals[])lstLayout.ItemsSource;
                foreach (Journals jrn in journals)
                    jrn.delete(jrn.Id.ToString());
                lstLayout.ItemsSource = null;
                txtCurrentLayout.Text = "";
                journals = Journals.load(true);
                lstLayout.ItemsSource = null;
                lstLayout.ItemsSource = journals;
                lstReports.SelectedIndex = -1;
            }
        }
    }



    public class JournalFields
    {
        public JournalFields()
        {

        }

        public static JournalFields[] BuildFields()
        {
            string[] fields = WinglGlobal.JournalFields;
            ArrayList list = new ArrayList(fields.Length);
            for (short i = 0; i < fields.Length; i++)
                list.Add(new JournalFields() { Item = fields[i] , Field = i });
            return (JournalFields[])list.ToArray(typeof(JournalFields));
        }

        public short Field { get; set; }
        public string Item { get; set; }
    }

    public class FieldTypes
    {
        public FieldTypes() { }

        public static ArrayList Fields()
        {
            ArrayList list = new ArrayList();
            for (short i = 0; i < WinglGlobal.JournalFieldTypes.Length; i++)
                list.Add(new FieldTypes() { Field = i, Name = WinglGlobal.JournalFieldTypes[i] });
            return list;
        }

        public short Field { get; set; }
        public string Name { get; set; }
    }

}
