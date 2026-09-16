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
    /// Interaction logic for frmBatchJournalEdit.xaml
    /// </summary>
    public partial class frmBatchJournalEdit : Window
    {
        const int Rows = 24;
        int Columns = 5;
        DataGridCustom grd = null;
        ComboBox[] accounts = null;
        ComboBox[] departments = null;
        TextBox[] descriptions = null;
        NumberEntry[] debits = new NumberEntry[Rows];
        NumberEntry[] credits = new NumberEntry[Rows];
        CheckBox[] offset = null;
        Accounts[] accts = null;
        BatchJrnEntries[] Entries = null;
        Department[] depts = null;
        BatchJrnHeader header = null;
        BatchJrnEntries[] batches = null;
        bool InBatchChange = false;
        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmBatchJournalEdit(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }


        public frmBatchJournalEdit()
        {
            InitializeComponent();
        }

        private void frmBatchJournalEdit_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            BatchJrnHeader[] jrnHeaders = BatchJrnHeader.load();
            lstBatches.ItemsSource = jrnHeaders;
            Company[] companies = Company.load();
            lstCompanies.ItemsSource = companies;
            JournalNames[] journals = JournalNames.load();
            accts = Accounts.load();
            lstJournals.ItemsSource = journals;
            Entries = new BatchJrnEntries[Rows];
            for (int i = 0; i < Rows; i++)
            {
                Entries[i] = new BatchJrnEntries();
                Entries[i].Index = i;
            }
            CreateGrid();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            ComboBox box = (ComboBox)sender;
            for (int i = 0; i < batches.Length; i++)
            {
                Accounts[] accts = (Accounts[])accounts[i].ItemsSource;
                Department[] depts = (Department[])departments[i].ItemsSource;
                for (int j = 0; j < accts.Length; j++)
                {
                    if (accts[j].Key == batches[i].Account)
                    {
                        accounts[i].SelectedIndex = j;
                        break;
                    }
                }
            }
        }

        private void CreateGrid()
        {
            if (grdTransactions != null)
            {
                grdTransactions.Children.Clear();
                grd = null;
                accounts = null;
                departments = null;
                descriptions = null;
                debits = null;
                credits = null;
            }
            ContextMenu[] ctx = new ContextMenu[Rows];
            grd = DataGridCustom.GetDataGridCustom(Columns, Rows, grdTransactions, true);
            grdTransactions.DataContext = null;
            accounts = grd.comboBoxAdd("Accounts", "accounts", "Description", "Key", accts, Entries, "Account", 300);
            departments = grd.comboBoxAdd("Departments", "departments", "Name", "DepartmentNumber", departments, Entries, "Department", 200, true);
            descriptions = grd.textBoxAdd("Description", "description", Entries, "Description", "", 200, true);
            debits = grd.NumberEntryAdd("Debits", "debits", Entries, "Debit", "#######.00", "", 90, true);
            credits = grd.NumberEntryAdd("Credits", "credits", Entries, "Credit", "#######.00", "", 90, true);
            offset = grd.checkBoxAdd("Auto Offset", "chkOffset", Entries, "AutoOffset", 75);
            for (int i = 0; i < Rows; i++)
            {
                MenuItem itmDelete = new MenuItem() { Header = "Delete" };
                itmDelete.Click += ctxDelete;
                itmDelete.Tag = i;
                ctx[i] = new ContextMenu();
                ctx[i].Items.Add(itmDelete);
                accounts[i].GotFocus += CurrentCellChanged;
                accounts[i].SelectionChanged += Combobox_SelectionChanged;
                accounts[i].PreviewKeyDown += OnPreviewKeyDown;
                accounts[i].ContextMenu = ctx[i];
                departments[i].GotFocus += CurrentCellChanged;
                departments[i].SelectionChanged += Combobox_SelectionChanged;
                departments[i].PreviewKeyDown += OnPreviewKeyDown;
                departments[i].ContextMenu = ctx[i];
                descriptions[i].GotFocus += CurrentCellChanged;
                descriptions[i].TextChanged += Description_TextChanged;
                descriptions[i].PreviewKeyDown += OnPreviewKeyDown;
                descriptions[i].ContextMenu = ctx[i];
                debits[i].GotFocus += CurrentCellChanged;
                debits[i].TextChanged += NumberEntry_TextChanged;
                debits[i].PreviewKeyDown += OnPreviewKeyDown;
                debits[i].ContextMenu = ctx[i];
                credits[i].GotFocus += CurrentCellChanged;
                credits[i].TextChanged += NumberEntry_TextChanged;
                credits[i].PreviewKeyDown += OnPreviewKeyDown;
                credits[i].ContextMenu = ctx[i];
                offset[i].Checked += Offset_Checked;
                offset[i].Unchecked += Offset_Unchecked;
                offset[i].ContextMenu = ctx[i];
            }

            grd.Initialize();
        }
        private void frmBatchJournalEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            bool old = (header.Key > 0);
            if (header.save())
            {
                if (old)
                    BatchJrnEntries.Query("DELETE FROM " + BatchJrnEntries.TABLE + " WHERE [" + BatchJrnEntries.SQL_COL_KEY + "] = " + header.Key.ToString(), GlobalShared.CN);
                int key = header.Key;
                for (int i = 0; i < Rows; i++)
                {
                   if (Entries[i].Account > 0 )
                    {
                        Entries[i].Key = key;
                        Entries[i].ID = 0;
                        Entries[i].Amount = (Entries[i].Credit > 0 ? -Entries[i].Credit :  Entries[i].Debit);
                        Entries[i].save();
                    }
                }
            }
            Clear();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            bool changed = false;
            for (int i = 0; i < Rows; i++)
            {
                if (Entries[i].IsChanged && Entries[i].Account > 0 && !Entries[i].Description.Trim().Equals(""))
                {
                    changed = true;
                    break;
                }
            }
            if (changed)
            {
                MessageBoxResult result = MessageBoxCustom.Show("Abandon changes?", "", MessageBoxButton.YesNoCancel);
            }
            this.Close();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            FrameworkElement control = (FrameworkElement)sender;
            if (e.Key == Key.Delete)
            {
                int row = (int)control.Tag;
                if (row >= 0)
                {
                    if (MessageBoxCustom.Show("Delete this transaction row?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Entries[row].Account = 0;
                        Entries[row].Department = 0;
                        Entries[row].Description = "";
                        Entries[row].Debit = 0;
                        Entries[row].Credit = 0;

                        accounts[row].SelectedIndex = -1;
                        departments[row].SelectedIndex = -1;
                        descriptions[row].Text = "";
                        debits[row].Text = ".00";
                        credits[row].Text = ".00";
                       
                    }
                }
            }
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int row = (int)txt.Tag;
            Entries[row].Description = txt.Text;
        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            NumberEntry cell = (NumberEntry)sender;
            int row = (int)cell.Tag;
            if (cell.Name == "debits")
            {
                Entries[row].Debit = decimal.Parse(cell.Text);
                if (decimal.Parse(cell.Text) != 0)
                {
                    Entries[row].Amount = decimal.Parse(cell.Text);
                    Entries[row].Credit = 0;
                    credits[row].Text = ".00";
                }
            }
            else if (cell.Name == "credits")
            {
                Entries[row].Credit = decimal.Parse(cell.Text);
                if (decimal.Parse(cell.Text) != 0)
                {
                    Entries[row].Amount = -decimal.Parse(cell.Text);
                    Entries[row].Debit = 0;
                    debits[row].Text = ".00";
                }
            }

            BatchJrnEntries tran;
            decimal total = 0;
            decimal dbs = 0;
            decimal crd = 0;
            for (int i = 0; i < Rows; i++)
            {
                tran = (BatchJrnEntries)Entries[i];
                if (tran != null)
                {
                    dbs += tran.Debit;
                    crd += tran.Credit;
                }
                total = dbs - crd;
            }
        }

        private void CurrentCellChanged(object sender, RoutedEventArgs e)
        {

            int row = 0; // = lstTransactions.Items.IndexOf(lstTransactions.CurrentItem);

            if (sender is ComboBox)
            {
                ComboBox box = (ComboBox)sender;
                row = (int)box.Tag;
            }
            else if (sender is TextBox)
            {
                TextBox txt = (TextBox)sender;
                row = (int)txt.Tag;
            }
            else if (sender is NumberEntry)
            {
                NumberEntry ne = (NumberEntry)sender;
                row = (int)ne.Tag;
            }
            else
                row = -1;
            if (row > 0)
            {
               if (Entries[row].Description == null || Entries[row].Description.Trim().Equals(""))
                    Entries[row].Description = Entries[row - 1].Description;
                if (Entries[row].Department <= 0)
                {
                    Entries[row].Department = Entries[row - 1].Department;
                    departments[row].SelectedValue = Entries[row - 1].Department;
                }
            }
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompanies.SelectedIndex >= 0)
            {
                if (header == null)
                    return;
                short cmp = ((Company)lstCompanies.SelectedItem).CompanyNumber;
                header.Company = (short)cmp;
                depts = Department.loadByCompany(cmp);
                for (int i = 0; i < Rows; i++)
                    departments[i].ItemsSource = depts;
                if (depts.Length == 1)
                {
                    if (batches != null && batches.Length > 0)
                    {
                        for (int i = 0; i < batches.Length; i++)
                        {
                            Entries[i].Department = depts[0].DepartmentNumber;
                            departments[i].SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InBatchChange)
                return;
            ComboBox box = (ComboBox)sender;
            if (box == null)
                return;
            BoxTag = (int)box.Tag;
            BoxIndex = box.SelectedIndex;
            switch (box.Name)
            {
                case "accounts":
                    Accounts acct = (Accounts)box.SelectedItem;
                    if (acct != null)
                        Entries[BoxTag].Account = acct.Key;
                    break;
                case "departments":
                    Department dept = (Department)box.SelectedItem;
                    if (dept != null)
                        Entries[BoxTag].Department = dept.DepartmentNumber;
                    break;
            }
        }

        private void Offset_Checked(object sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            if (box != null)
            {
                if (box.IsChecked.Value)
                {
                    int i = (int)box.Tag;
                    for (int j = 0; j < Rows; j++)
                        if (j != i)
                        {
                            offset[j].IsChecked = false;
                            Entries[j].AutoOffset = false;
                        }
                        else
                            Entries[j].AutoOffset = true;
                }
            }
        }

        private void Offset_Unchecked(object sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            if (box != null && !box.IsChecked.Value)
            {
                int i = (int)box.Tag;
                Entries[i].AutoOffset = false;
            }
        }
        //private bool InBatchChange = false;
        private void lstBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (InBatchChange)
            {
                InBatchChange = false;
                return;
            }
            */
            InBatchChange = true;
            CreateEntries();
        }

        private void CreateEntries(bool read = true)
        { 
            Clear(true);
            if (read)
            {

                if (lstBatches.SelectedItem != null)
                {
                    header = (BatchJrnHeader)lstBatches.SelectedItem;

                    if (header.Company != null && header.Company > 0)
                    {
                        for (int i = 0; i < lstCompanies.Items.Count; i++)
                        {
                            if (((Company)lstCompanies.Items[i]).CompanyNumber == header.Company)
                            {
                                lstCompanies.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    pnlHeader.DataContext = header;

                    batches = BatchJrnEntries.loadByKey(header.Key);
                }
                else
                    return;
            }
            if (batches != null && batches.Length > 0)
            {
                for (int i = 0; i < batches.Length; i++)
                {
                    Entries[i] = batches[i];
                }
                for (int i = 0; i < batches.Length; i++)
                {
                    descriptions[i].Text = batches[i].Description;
                    debits[i].Text = batches[i].Debit.ToString();
                    credits[i].Text = batches[i].Credit.ToString();
                    Accounts[] accts = (Accounts[])accounts[i].ItemsSource;
                    Department[] depts = (Department[])departments[i].ItemsSource;
                    offset[i].IsChecked = batches[i].AutoOffset;
                    for (int j = 0; j < accts.Length; j++)
                    {
                        if (accts[j].Key == batches[i].Account)
                        {
                            accounts[i].SelectedIndex = j;
                            Entries[i].Account = batches[i].Account;
                            if (Entries[i].Amount > 0)
                            {
                                Entries[i].Debit = Entries[i].Amount;
                                debits[i].Text = Entries[i].Debit.ToString();
                            }
                            else if (Entries[i].Amount < 0)
                            {
                                Entries[i].Credit = -Entries[i].Amount;
                                credits[i].Text = Entries[i].Credit.ToString();
                            }
                            break;
                        }
                    }
                    int idx = accounts[0].SelectedIndex;
                    if (depts != null)
                    {
                        for (int j = 0; j < depts.Length; j++)
                        {
                            if (depts[j].DepartmentNumber == batches[i].Department)
                            {
                                departments[i].SelectedIndex = j;
                                Entries[i].Department = batches[i].Department;
                                break;
                            }
                        }
                    }
                }
                grdTransactions.DataContext = Entries;
            }
            
            InBatchChange = false;
        }

        private void cmdDeleteBatch_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("DELETE this Batch?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (BatchJrnEntries entry in Entries)
                    entry.delete(entry.ID.ToString());
                header.delete(header.Key.ToString());
                Clear();
            }
        }

        private void ctxDelete(object sender, RoutedEventArgs e)
        {
            int row = -1;

            row = (int)((MenuItem)sender).Tag;

            if (row >= 0)
            {
                if (MessageBoxCustom.Show("Delete this row?","", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    grdTransactions.DataContext = null;
                    if (row < batches.Length)
                    {
                        if (row == batches.Length - 1)
                            batches[row] = new BatchJrnEntries();
                        else
                            for (int i = row; i < batches.Length; i++)
                            {
                                if (i < batches.Length - 1)
                                {
                                    batches[i] = batches[i + 1];
                                    batches[i + 1] = new BatchJrnEntries();
                                }
                                /*
                                      Entries[i] = Entries[row + 1];

                                      departments[i] = departments[i + 1];
                                      departments[i].Tag = i;
                                      descriptions[i] = descriptions[i + 1];
                                      descriptions[i].Tag = i;
                                      debits[i] = debits[i + 1];
                                      debits[i].Tag = i;
                                      credits[i] = credits[i + 1];
                                      credits[i].Tag = i;
                                      accounts[i] = accounts[i + 1];
                                      accounts[i].Tag = i;
                                      departments[i + 1] = new ComboBox() { Tag = i + 1 };
                                      departments[i + 1].SelectedIndex = -1;
                                      departments[i + 1].Text = "";
                                      descriptions[i + 1] = new TextBox() { Tag = i + 1 };
                                      descriptions[i + 1].Text = "";
                                      debits[i + 1] = new NumberEntry() { Tag = i + 1 };
                                      debits[i + 1].Text = "";
                                      credits[i + 1] = new NumberEntry() { Tag = i + 1 };
                                      debits[i + 1].Text = "";
                                      accounts[i + 1] = new ComboBox() { Tag = i + 1 };
                                      accounts[i + 1].SelectedIndex = -1;
                                      accounts[i + 1].Text = "";
                                      */

                            }
                    }
                    CreateEntries(false);
                }
            }

        }

        private void Clear(bool EntriesOnly = false)
        {
            if (!EntriesOnly)
                lstBatches.SelectedIndex = -1;
            lstCompanies.SelectedIndex = -1;
            lstJournals.SelectedIndex = -1;
            txtSource.Text = "";
            Entries = null;
            Entries = new BatchJrnEntries[Rows];
            for (int i = 0; i < Rows; i++)
            {
                Entries[i] = new BatchJrnEntries();
                Entries[i].Index = i;
                //accounts[i].SelectedIndex = -1;
                //departments[i].SelectedIndex = -1;
               // descriptions[i].Text = "";
               // debits[i].Text = ".00";
                //credits[i].Text = ".00";
            }
            if (!EntriesOnly)
            {
                lstBatches.ItemsSource = null;
                BatchJrnHeader[] jrnHeaders = BatchJrnHeader.load();
                lstBatches.ItemsSource = jrnHeaders;
            }
            CreateGrid();
        }

        public int BoxTag { get; set; }
        public int BoxIndex { get; set; }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Text = "";
            InputBox.Visibility = Visibility.Visible;
            InputBox.Focus();
            InputTextBox.Focus();
        } 
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            header = new BatchJrnHeader() { BatchName = InputTextBox.Text };
            if (header.save() )
            {
                int key = header.Key;
                lstBatches.ItemsSource = null;
                BatchJrnHeader[] jrnHeaders = BatchJrnHeader.load();
                lstBatches.ItemsSource = jrnHeaders;
                if (jrnHeaders.Length > 0)
                    
                    for (int i = 0; i < jrnHeaders.Length; i++)
                    {
                        if (jrnHeaders[i].Key == key)
                        {
                            lstBatches.SelectedIndex = i;
                            break;
                        }
                    }
            }
            else
                MessageBoxCustom.Show("Unable to create new batch.");
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InputTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Space)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void lstBatches_TextChanged(object sender, TextChangedEventArgs e)
        {
            header.BatchName = lstBatches.Text;
        }
    }
}
