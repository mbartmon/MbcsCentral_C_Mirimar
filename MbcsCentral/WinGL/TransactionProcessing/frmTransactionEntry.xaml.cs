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
    /// Interaction logic for frmTransactionEntry.xaml
    /// </summary>
    public partial class frmTransactionEntry : Window
    {
        BatchJrnHeader[] batchHeaders = null;
        Accounts[] accts;
        Department[] depts;
        const int NumberOfEntries = 24;
        Transactiontemp[] Entries = new Transactiontemp[NumberOfEntries];
        ComboBox[] accounts = new ComboBox[NumberOfEntries];
        ComboBox[] departments = new ComboBox[NumberOfEntries];
        NumberEntry[,] vi = new NumberEntry[NumberOfEntries, 2];
        TextBox[] descriptions = new TextBox[NumberOfEntries];
        int currentRow = 0;
        bool tranactionsEntered = false;
        bool HoldBatch = false;
        bool scan = false;
        string WindocLocation = "";
        bool UseAutoOffset = false;
        int AutoOffsetRow;

        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmTransactionEntry(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }


        public frmTransactionEntry()
        {
            InitializeComponent();
        }

        private void frmTransactionEntry_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            // Get scan flag
            Control ctrl = Control.loadOne();
            if (ctrl != null)
            {
                scan = ctrl.Scan;
                WindocLocation = ctrl.WindocLocation.Trim();
            }
            if (scan)
                bdrScan.Visibility = Visibility.Visible;
            else
                bdrScan.Visibility = Visibility.Collapsed;
            
            batchHeaders = BatchJrnHeader.load(true);
            lstBatches.ItemsSource = batchHeaders;
            Company[] companies = Company.load();
            JournalNames[] journals = JournalNames.load();
            lstCompanies.ItemsSource = companies;
            lstJournals.ItemsSource = journals;
            accts = Accounts.load();
            //accounts = ((CollectionViewSource)(this.FindResource("accounts")));
           // accounts.Source = accts;
            for (int i = 0; i < NumberOfEntries; i++)
            {
                Entries[i] = new Transactiontemp();
                Entries[i].Index = i;
            }
            SetGrid(grdTransactions, Entries);
            dtDate.SelectedDate = DateTime.Now;
            dtDate.DisplayDate = DateTime.Now;
        }

        private void frmTransactionEntry_Closed(object sender, EventArgs e)
        {
            if (tranactionsEntered)
            {
                MessageBoxResult result = MessageBoxCustom.Show("Print the Transaction Edit List?", "", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        object[] obj = null;
                        frmReportDispatcher frm = new frmReportDispatcher("MbcsCentral.WinGL.TransactionEditList", obj);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            CalcTotals();
            if (decimal.Parse(txtTotal.Text) != 0)
            {
                if (MessageBoxCustom.Show("Transactions do not balance.  Do you still want to post them?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            bool AllAcctsFound = true;
            bool AllJournalsFound = true;
            for (int i = 0; i < NumberOfEntries; i++)
            {
                if (Entries[i].AccountNumber <= 0 && Entries[i].Amount != 0)
                    AllAcctsFound = false;
                if (Entries[i].Amount != 0 && (Entries[i].Journal == null || Entries[i].Journal.Trim().Equals("")))
                    AllJournalsFound = false;
            }
            if (!AllAcctsFound)
            {
                MessageBoxCustom.Show("Transactions are not completed or none have been entered.  Cannot be saved.");
                return;
            }
            if(!AllJournalsFound)
            {
                if (MessageBoxCustom.Show("One or more transactions have no Journal.  Do you want to save anyway.", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            int chk = 0;
            int.TryParse(txtCheck.Text, out chk);
            if (chk == 0 && txtSource.Text.Trim().Equals(""))
            {
                Control ctrl = Control.loadOne();
                if (ctrl != null)
                {
                    int sNum = ctrl.GlSourceNumber;
                    sNum++;
                    ctrl.GlSourceNumber = sNum;
                    ctrl.save();
                    txtSource.Text = "J-" + sNum.ToString("000000");
                }
                ctrl = null;
            }
            for (int i = 0; i < NumberOfEntries; i++)
            {
                if (!Entries[i].Delete && Entries[i].AccountNumber > 0 && (Entries[i].Debit != 0 || Entries[i].Credit != 0))
                {
                    Entries[i].CompanyNumber = ((Company)lstCompanies.SelectedItem).CompanyNumber;
                    Entries[i].Date = (DateTime)dtDate.SelectedDate;
                    Entries[i].CheckNumber = chk;
                    if (lstJournals.SelectedIndex >= 0)
                        Entries[i].Journal = ((JournalNames)lstJournals.SelectedItem).JournalName;
                    Entries[i].Source = txtSource.Text;
                    if (Entries[i].Credit != 0)
                        Entries[i].Amount = -Entries[i].Credit;
                    else
                        Entries[i].Amount = Entries[i].Debit;
                    Entries[i].save();
                }
            }
            tranactionsEntered = true;

            if (btn.Tag != null &&  btn.Tag.Equals("scan"))
                ScanJournal();

            if (chkHoldBatch.IsChecked.Value)
            {
                HoldBatch = true;
                int i = lstBatches.SelectedIndex;
                if (i >= 0)
                {
                    /*
                    ListBoxItem lsti = (ListBoxItem)(lstBatches.ItemContainerGenerator.ContainerFromIndex(i));
                    if (lsti != null)
                    {
                        lsti.IsSelected = false;
                        lsti.IsSelected = true;
                    }
                    */
                    Batches_SelectionChanged(lstBatches);
                    if (chkHoldCompany.IsChecked.Value)
                    {
                        Companies_SeletecionChanged();
                    }
                }

            }
            else
                Clear();

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear(bool batch = true)
        {
            //lstTransactions.ItemsSource = null;
            int i = lstCompanies.SelectedIndex;
            if (!chkHoldCompany.IsChecked.Value)
                lstCompanies.SelectedIndex = -1;
            lstJournals.SelectedIndex = -1;
            if (batch)
                lstBatches.SelectedIndex = -1;
            txtSource.Text = "";
            txtCheck.Text = "0";
            txtTotal.Text = "0";
            txtDebits.Text = "0";
            txtCredits.Text = "0";
            for (int id = 0; id < NumberOfEntries; id++)
            {
                Entries[id] = null;
                Entries[id] = new Transactiontemp();
                Entries[id].Index = id;
            }
            SetGrid(grdTransactions, Entries);
        }

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Companies_SeletecionChanged();
        }

        private void Companies_SeletecionChanged()
        {
            if (lstCompanies.SelectedIndex >= 0)
            {
                short cmp = ((Company)lstCompanies.SelectedItem).CompanyNumber;
                depts = Department.loadByCompany(cmp);

                if (depts.Length == 1)
                {
                    for (int i = 0; i < NumberOfEntries; i++)
                    {
                        if (Entries[i].AccountNumber > 0)
                        {
                            departments[i].ItemsSource = null;
                            departments[i].ItemsSource = depts;
                            Entries[i].DepartmentNumber = depts[0].DepartmentNumber;
                            departments[i].SelectedIndex = 0;
                        }
                    }
                }
            }     
                
        }

        private void lstAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box != null)
            {
                int row = (int)box.Tag;
                if (row != null && box.SelectedValue != null)
                {
                    Entries[row].AccountNumber = (int)box.SelectedValue;
                    if (depts != null)
                    {
                        departments[row].ItemsSource = depts;
                        Entries[row].DepartmentNumber = depts[0].DepartmentNumber;
                        departments[row].SelectedIndex = 0;
                    }
                }
            }
        }

        private void lstDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box != null)
            {
                int row = (int)box.Tag;
                if (row != null && box.SelectedItem != null)
                    Entries[row].DepartmentNumber = ((Department)box.SelectedItem).DepartmentNumber;
            }

        }

        private void NumberEntry_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            NumberEntry cell = (NumberEntry)sender;
            int row = (int)cell.Tag;
            if (e.Key == Key.Up && row > 0)
            {
                if (cell.Name == "Debit")
                    vi[row - 1, 0].Focus();
                else
                    vi[row - 1, 1].Focus();
                e.Handled = true;
            }
            if (e.Key == Key.Down && row < NumberOfEntries)
            {
                if (cell.Name == "Debit")
                    vi[row + 1, 0].Focus();
                else
                    vi[row + 1, 1].Focus();
                e.Handled = true;
            }
        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal amount = 0;
            NumberEntry cell = (NumberEntry)sender;
            decimal.TryParse(cell.Text, out amount);
            int row = (int)cell.Tag;
            if (cell.Name == "Debit" && amount != 0)
            {
                Entries[row].Debit = amount;
                //vi[row, 0].Text = cell.Text;
                vi[row, 0].Value = amount;
                Entries[row].Credit = 0;
                vi[row , 1].Value = 0;
            }
            else if (cell.Name == "Credit" && amount != 0)
            {
                Entries[row].Credit = amount;
                //vi[row, 1].Text = cell.Text;
                vi[row, 1].Value = amount;
                Entries[row].Debit = 0;
                // vi[row , 0].Text = ".00";
                vi[row, 0].Value = 0;
            }
            CalcTotals();
        }

        private void Number_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal amount = 0;
            NumberEntry cell = (NumberEntry)sender;
            decimal.TryParse(cell.Text, out amount);
            int row = (int)cell.Tag;
            if (cell.Name == "Debit" && amount != 0)
            {
                Entries[row].Debit = amount;
                vi[row, 0].Text = amount.ToString();
                Entries[row].Credit = 0;
                vi[row, 1].Text = "0";
            }
            else if (cell.Name == "Credit" && amount != 0)
            {
                Entries[row].Credit = amount;
                vi[row, 1].Text = amount.ToString();
                Entries[row].Debit = 0;
                vi[row , 0].Text = "0";
            }
            CalcTotals();

        }

        private void CalcTotals()
        {
            Transactiontemp tran;
            decimal total = 0;
            decimal debits = 0;
            decimal credits = 0;
            for (int i = 0; i < NumberOfEntries; i++)
            {
                if (UseAutoOffset)
                {
                    Entries[AutoOffsetRow].Credit = 0;
                    Entries[AutoOffsetRow].Debit = 0;
                }
                tran = (Transactiontemp)Entries[i];
                if (tran != null && !tran.Delete)
                {
                    if (!UseAutoOffset || i != AutoOffsetRow)
                    {
                        debits += tran.Debit;
                        credits += tran.Credit;
                    }
                }
            }
            total = debits - credits;
            if (UseAutoOffset)
            {
                if (total > 0)
                {
                    Entries[AutoOffsetRow].Credit = total + credits;
                    vi[AutoOffsetRow, 1].Text = total.ToString();
                    Entries[AutoOffsetRow].Debit = 0;
                    vi[AutoOffsetRow, 0].Text = 0.ToString();

                    total -= credits;
                    
                }
                else
                {
                    Entries[AutoOffsetRow].Debit = -total - debits;
                    vi[AutoOffsetRow, 0].Text = (-total).ToString();
                    Entries[AutoOffsetRow].Credit = 0;
                    vi[AutoOffsetRow, 1].Text = 0.ToString();

                    debits = -total;
                }
                credits = 0;
                debits = 0;
                total = 0;
                for (int i = 0; i < NumberOfEntries; i++)
                {
                    tran = (Transactiontemp)Entries[i];
                    if (tran != null && !tran.Delete)
                    {
                        //if (!UseAutoOffset || i != AutoOffsetRow)
                        //{
                            debits += tran.Debit;
                            credits += tran.Credit;
                        //}
                    }
                }
                total = debits - credits;
            }
            txtTotal.Text = total.ToString("#######.00");
            txtDebits.Text = debits.ToString("#######.00");
            txtCredits.Text = credits.ToString("#######.00");

        }
        private void CurrentCellChanged(object sender, RoutedEventArgs e)
        {
            Type type = sender.GetType();

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
                ne.SelectAll();
            }
            else
                row = -1;
            if (row > 0)
            {
                if (Entries[row].Description == null || Entries[row].Description.Trim().Equals(""))
                    Entries[row].Description = Entries[row - 1].Description;
                if (Entries[row].DepartmentNumber <= 0)
                {
                    Entries[row].DepartmentNumber = Entries[row - 1].DepartmentNumber;
                    departments[row].SelectedValue = Entries[row - 1].DepartmentNumber;
                }
               
            }

        }

        private T GetFirstChildByType<T>(DependencyObject prop) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(prop); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild((prop), i) as DependencyObject;
                if (child == null)
                    continue;

                T castedProp = child as T;
                if (castedProp != null)
                    return castedProp;

                castedProp = GetFirstChildByType<T>(child);

                if (castedProp != null)
                    return castedProp;
            }
            return null;
        }

        
        /// <summary>
        /// Create the payment grid with all charges and their amounts due
        /// </summary>
        /// <param name="grd">The grid object</param>
        /// <param name="transactions">Array of payment entry objects that contains the charges</param>
        void SetGrid(Grid grd, Transactiontemp[] transactions)
        {
            Border bdr = null;
            grd.Children.Clear();
            int rows = transactions.Length;
            int cols = 5;
            ColumnDefinition cl = new ColumnDefinition() { Width = new GridLength(300) };
            grd.ColumnDefinitions.Add(cl);
            cl = new ColumnDefinition() { Width = new GridLength(200) };
            grd.ColumnDefinitions.Add(cl);
            cl = new ColumnDefinition() { Width = new GridLength(348) };
            grd.ColumnDefinitions.Add(cl);
            cl = new ColumnDefinition() { Width = new GridLength(90) };
            grd.ColumnDefinitions.Add(cl);
            cl = new ColumnDefinition() { Width = new GridLength(95) };
            grd.ColumnDefinitions.Add(cl);
            //grd.Width =  grd.ActualWidth;
            for (int i = 0; i <= rows; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(25);
                rd.Tag = i.ToString();
                grd.RowDefinitions.Add(rd);
            }
            TextBlock col1 = new TextBlock();
            col1.Text = "Account";
            col1.VerticalAlignment = VerticalAlignment.Center;
            col1.HorizontalAlignment = HorizontalAlignment.Center;
            col1.TextAlignment = TextAlignment.Center;
            col1.FontWeight = FontWeights.Bold;
            col1.Width = 300;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            bdr.CornerRadius = new CornerRadius(3, 0, 0, 0);
            Grid.SetColumn(bdr, 0);
            Grid.SetRow(bdr, 0);
            bdr.Child = col1;
            grd.Children.Add(bdr);

            TextBlock col2 = new TextBlock();
            col2.Text = "Department";
            col2.VerticalAlignment = VerticalAlignment.Center;
            col2.HorizontalAlignment = HorizontalAlignment.Center;
            col2.TextAlignment = TextAlignment.Center;
            col2.FontWeight = FontWeights.Bold;
            col2.Width = 200;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 1);
            Grid.SetRow(bdr, 0);
            bdr.Child = col2;
            grd.Children.Add(bdr);

            TextBlock col3 = new TextBlock();
            col3.Text = "Description";
            col3.VerticalAlignment = VerticalAlignment.Center;
            col3.HorizontalAlignment = HorizontalAlignment.Center;
            col3.TextAlignment = TextAlignment.Center;
            col3.FontWeight = FontWeights.Bold;
            col3.Width = 348;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 2);
            Grid.SetRow(bdr, 0);
            bdr.Child = col3;
            grd.Children.Add(bdr);

            TextBlock col5 = new TextBlock();
            col5.Text = "Debits";
            col5.VerticalAlignment = VerticalAlignment.Center;
            col5.HorizontalAlignment = HorizontalAlignment.Center;
            col5.TextAlignment = TextAlignment.Center;
            col5.FontWeight = FontWeights.Bold;
            col5.Width = 95;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, 3);
            Grid.SetRow(bdr, 0);
            bdr.Child = col5;
            grd.Children.Add(bdr);

            TextBlock col6 = new TextBlock();
            col6.Text = "Credits";
            col6.VerticalAlignment = VerticalAlignment.Center;
            col6.HorizontalAlignment = HorizontalAlignment.Center;
            col6.TextAlignment = TextAlignment.Center;
            col6.FontWeight = FontWeights.Bold;
            col6.Width = 96;
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            bdr.CornerRadius = new CornerRadius(0, 3, 0, 0);
            Grid.SetColumn(bdr, 4);
            Grid.SetRow(bdr, 0);
            bdr.Child = col6;
            grd.Children.Add(bdr);

            Binding bind = null;
            Binding[] bind1 = new Binding[rows];
            Binding[] bind2 = new Binding[rows];
            for (int r = 1; r <= rows; r++)
            {
                bind = new Binding("AccountNumber");
                bind.Source = accounts[r - 1];
                bind.Path = new PropertyPath("AccountNumber");
                bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                ComboBox box = new ComboBox();
                box.HorizontalContentAlignment = HorizontalAlignment.Left;
                box.IsTextSearchEnabled = true;
                //box.IsEditable = true;
                //BindingOperations.SetBinding(box, ComboBox.SelectedValueProperty, bind);
                accounts[r - 1] = box;
                accounts[r - 1].HorizontalContentAlignment = HorizontalAlignment.Left;
                accounts[r - 1].HorizontalAlignment = HorizontalAlignment.Left;
                accounts[r - 1].TabIndex = r + cols;
                accounts[r - 1].Width = 300;
                accounts[r - 1].DisplayMemberPath = "Description";
                accounts[r - 1].SelectedValuePath = "Key";
                accounts[r - 1].ItemsSource = accts;
                accounts[r - 1].Tag = r - 1;
                accounts[r - 1].SelectionChanged += lstAccounts_SelectionChanged;
                accounts[r - 1].GotFocus += CurrentCellChanged;
                accounts[r - 1].ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 0);
                Grid.SetRow(bdr, r);
                bdr.Child = accounts[r - 1];
                grd.Children.Add(bdr);

                departments[r - 1] = new ComboBox();
                departments[r - 1].HorizontalContentAlignment = HorizontalAlignment.Left;
                departments[r - 1].TabIndex = r + cols;
                departments[r - 1].Width = 200;
                departments[r - 1].DisplayMemberPath = "Name";
                departments[r - 1].SelectedValuePath = "DepartmentNumber";
                departments[r - 1].Tag = r - 1;
                departments[r - 1].SelectionChanged += lstDepartments_SelectionChanged;
                departments[r - 1].GotFocus += CurrentCellChanged;
                departments[r - 1].ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 1);
                Grid.SetRow(bdr, r);
                bdr.Child = departments[r - 1];
                grd.Children.Add(bdr);

                bind = new Binding("Description");
                bind.Source = transactions[r - 1];
                bind.Path = new PropertyPath("Description");
                bind.Mode = BindingMode.TwoWay;
                bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                descriptions[r - 1] = new TextBox();
                descriptions[r - 1].TabIndex = r + cols;
                BindingOperations.SetBinding(descriptions[r - 1], TextBox.TextProperty, bind);
                descriptions[r - 1].TextAlignment = TextAlignment.Left;
                descriptions[r - 1].VerticalAlignment = VerticalAlignment.Center;
                descriptions[r - 1].Width = 348;
                descriptions[r - 1].Tag = r - 1;
                descriptions[r - 1].GotFocus += CurrentCellChanged;
                descriptions[r - 1].ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 2);
                Grid.SetRow(bdr, r);
                bdr.Child = descriptions[r - 1];
                grd.Children.Add(bdr);

                /*
                bind = new Binding("Description");
                bind.Source = transactions[r - 1];
                bind.Path = new PropertyPath("Description");
                bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                TextBox col = new TextBox();
                col.TabIndex = r + cols;
                BindingOperations.SetBinding(col, TextBox.TextProperty, bind);
                col.TextAlignment = TextAlignment.Left;
                col.VerticalAlignment = VerticalAlignment.Center;
                col.Width = 348;
                col.Tag = r - 1;
                col.GotFocus += CurrentCellChanged;
                col.ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 2);
                Grid.SetRow(bdr, r);
                bdr.Child = col;
                grd.Children.Add(bdr);
                */

                vi[r - 1, 0] = new NumberEntry();
                vi[r - 1, 0].TabIndex = r + cols;
                vi[r - 1, 0].Name = "Debit";
                bind = new Binding("Debit");
                bind.Source = transactions[r - 1];
                bind.StringFormat = "#######.00";
                bind.Path = new PropertyPath("Debit");
                bind.Mode = BindingMode.TwoWay;
                bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(vi[r - 1, 0], NumberEntry.TextProperty, bind);
                vi[r - 1, 0].TextAlignment = TextAlignment.Right;
                vi[r - 1, 0].Padding = new Thickness(0, 0, 8, 0);
                vi[r - 1, 0].VerticalAlignment = VerticalAlignment.Center;
                vi[r - 1, 0].HorizontalAlignment = HorizontalAlignment.Left;
                vi[r - 1, 0].Width = 95;
                vi[r - 1, 0].Tag = r - 1;
                //vi[r - 1, 0].Margin = new Thickness(0, 0, -8, 0);
                //vi[r - 1, 0].TextChanged += NumberEntry_TextChanged;
                
                vi[r - 1, 0].LostFocus += Number_LostFocus;
                vi[r - 1, 0].GotFocus += CurrentCellChanged;
                vi[r - 1, 0].PreviewKeyDown += NumberEntry_PreviewKeyDown;
                vi[r - 1, 0].ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 3);
                Grid.SetRow(bdr, r);
                bdr.Child = vi[r - 1, 0];
                grd.Children.Add(bdr);

                vi[r - 1, 1] = new NumberEntry();
                vi[r - 1, 1].Name = "Credit";
                vi[r - 1, 1].TabIndex = r + cols;
                bind = new Binding("Credit");
                bind.Source = transactions[r - 1];
                bind.StringFormat = "#######.00";
                bind.Path = new PropertyPath("Credit");
                bind.Mode = BindingMode.TwoWay;
                bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(vi[r - 1, 1], NumberEntry.TextProperty, bind);
                vi[r - 1, 1].MaxLength = 10;
                vi[r - 1, 1].Decimals = 2;
                vi[r - 1, 1].Width = 95;
                vi[r - 1, 1].Padding = new Thickness(0, 0, 2, 0);
                vi[r - 1, 1].Tag = r - 1;
                //vi[r - 1, 1].Margin = new Thickness(0, 0, -6, 0);
                //vi[r - 1, 1].TextChanged += NumberEntry_TextChanged;
                vi[r - 1, 1].LostFocus += Number_LostFocus;
                vi[r - 1, 1].GotFocus += CurrentCellChanged;
                vi[r - 1, 1].PreviewKeyDown += NumberEntry_PreviewKeyDown;
                vi[r - 1, 1].ContextMenu = ContextMenuItem(r - 1);
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, 4);
                Grid.SetRow(bdr, r);
                bdr.Child = vi[r - 1, 1];
                grd.Children.Add(bdr);
            }
        }

        private ContextMenu ContextMenuItem(int row)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Header = "Delete Toggle";
            item.Click += Rd_Delete;
            item.Tag = row;
            menu.Items.Add(item);
            return menu;
        }

        private void Rd_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem rd = (MenuItem)sender;
            if (rd != null)
            {
                int row = (int)rd.Tag;

                if (UseAutoOffset && row == AutoOffsetRow)
                {
                    MessageBoxCustom.Show("This row cannot be deleted.", "", MessageBoxButton.OK, this);
                    return;
                }
                Entries[row].Delete = !Entries[row].Delete;
                if (Entries[row].Delete)
                {
                    departments[row].Foreground = Brushes.LightGray;
                    accounts[row].Foreground = Brushes.LightGray;
                    descriptions[row].Foreground = Brushes.LightGray;
                    vi[row, 0].Text = "0";
                    vi[row, 0].Foreground = Brushes.LightGray;
                    vi[row, 0].IsReadOnly = true;
                    vi[row, 1].Text = "0";
                    vi[row, 1].Foreground = Brushes.LightGray;
                    vi[row, 1].IsReadOnly = true;
                }
                else
                {
                    departments[row].Foreground = Brushes.Black;
                    accounts[row].Foreground = Brushes.Black;
                    descriptions[row].Foreground = Brushes.Black;
                    vi[row, 0].Foreground = Brushes.Black;
                    vi[row, 0].IsReadOnly = false;
                    vi[row, 1].Foreground = Brushes.Black;
                    vi[row, 1].IsReadOnly = false;
                }
                CalcTotals();
            }
        }

        /*
        private void Rd_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem rd = (MenuItem)sender;
            if (rd != null)
            {
                int row = (int)rd.Tag;

                GlobalShared.Log.Log((int)LogClass.logType.Info, "vi Before\r\n========", false);
                for (int i = 0; i < vi.Length / 2; i++)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "vi[i,0].Tag = " + vi[i, 0].Tag.ToString() + " / vi[i, 1].Tag = " + vi[i, 1].Tag.ToString(), false);
                }
                if (row == Entries.Length - 1)
                {
                    Entries[row] = new Transactiontemp();
                    Entries[row].Index = row;
                    accounts[row].SelectedIndex = -1;
                    vi[row, 0].Text = "";
                    vi[row, 1].Text = "";
                }
                if (row >= 0 && row < Entries.Length - 1)
                {
                    Binding bind = null;
                    for (int i = row; i < Entries.Length - 1; i++)
                    {
                        Entries[i] = Entries[i + 1];
                        Entries[i].Index = i;
                        departments[i] = departments[i + 1];
                        departments[i].Tag = i;
                        accounts[i] = accounts[i + 1];
                        accounts[i].Tag = i;
                        vi[i, 0] = vi[i + 1, 0];
                        vi[i, 0].Tag = i;
                        //bind = new Binding("Debit");
                        //bind.Source = Entries[i];
                        //bind.StringFormat = "#######.00";
                        //bind.Path = new PropertyPath("Debit");
                        //bind.Mode = BindingMode.TwoWay;
                        //bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        //BindingOperations.SetBinding(vi[i, 0], NumberEntry.TextProperty, bind);
                        vi[i, 1] = vi[i + 1, 1];
                        vi[i, 1].Tag = i;
                        //bind = new Binding("Credit");
                        //bind.Source = Entries[i];
                        //bind.StringFormat = "#######.00";
                        //bind.Path = new PropertyPath("Credit");
                        //bind.Mode = BindingMode.TwoWay;
                        //bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        //BindingOperations.SetBinding(vi[i, 1], NumberEntry.TextProperty, bind);
                        accounts[i].SelectedValue = Entries[i].AccountNumber;
                        if (Entries[i].Description == null)
                        {
                            Entries[i].Description = "";
                            descriptions[i].Text = "";
                        }
                    }
                    Entries[Entries.Length - 1] = new Transactiontemp();
                    vi[Entries.Length - 1, 0].Text = "0";
                    vi[Entries.Length - 1, 1].Text = "0";
                    Entries[Entries.Length - 1].Index = Entries.Length - 1;
                    for (int i = 0; i < NumberOfEntries; i++)
                    {
                        vi[i, 0].Tag = i;
                        vi[i, 1].Tag = i;
                    }
                    accounts[Entries.Length - 1].SelectedIndex = -1;
                }
                //SetGrid( grdTransactions, Entries);
                GlobalShared.Log.Log((int)LogClass.logType.Info, "vi After\r\n========", false);
                for (int i = 0; i < vi.Length / 2; i++)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "vi[i,0].Tag = " + vi[i, 0].Tag.ToString() + " / vi[i, 1].Tag = " + vi[i, 1].Tag.ToString(), false);
                }
                CalcTotals();
            }
        }
        */
        private void lstBatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Batches_SelectionChanged(sender);
        }

        private void Batches_SelectionChanged(object sender)
        {
            UseAutoOffset = false;
            AutoOffsetRow = -1;

            Clear(false);
            BatchJrnHeader header = (BatchJrnHeader)((ComboBox)sender).SelectedItem;
            if (header == null || header.BatchName == null || header.BatchName.Trim().Equals(""))
                return;
            if (!HoldBatch) chkHoldBatch.IsChecked = false;
            HoldBatch = false;
            if (header.Company > 0)
                lstCompanies.SelectedValue = header.Company;
            lstJournals.SelectedValue = header.Journal;
            txtSource.Text = header.Source;
            BatchJrnEntries[] entries = BatchJrnEntries.loadByKey(header.Key);
            if (entries != null && entries.Length > 0)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    for (int r = 0; r < accts.Length; r++)
                    {
                        if (accts[r].Key == entries[i].Account)
                        {
                            Entries[i].AccountNumber = accts[r].Key;
                            accounts[i].SelectedValue = accts[r].Key;
                        }
                        Entries[i].Description = entries[i].Description;
                    }
                    if (header.Company == 0)
                    {
                        departments[i].ItemsSource = null;
                        Entries[i].DepartmentNumber = 0;
                        departments[i].SelectedIndex = -1;
                    }
                    if (entries[i].Amount > 0)
                    {
                        Entries[i].Debit = entries[i].Amount;
                        vi[i, 0].Text = entries[i].Amount.ToString();
                    }
                    else if (entries[i].Amount < 0)
                    {
                        Entries[i].Credit = -entries[i].Amount;
                        vi[i, 1].Text = (-entries[i].Amount).ToString();
                    }
                    if (entries[i].AutoOffset)
                    {

                        Brush color = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF21B721"));
                        UseAutoOffset = true;
                        AutoOffsetRow = i;
                        accounts[i].Foreground = color;
                        departments[i].Foreground = color;
                        descriptions[i].Foreground = color;
                        vi[i, 0].Foreground = color;
                        vi[i, 1].Foreground = color;
                    }
                }
                if (chkHoldCompany.IsChecked.Value && header.Company <= 0)
                    Companies_SeletecionChanged();

            }

        }

        private void ScanJournal()
        {
            if (WindocLocation == null || WindocLocation.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Could not find Windoc Database <" + WindocLocation + ">. Entry will not be scanned.");
                return;
            }
            WinglGlobal.invpGlobal = default;
            int chk = 0;
            if (!txtCheck.Text.Trim().Equals("") && !txtCheck.Text.Trim().Equals("0"))
            {
                int.TryParse(txtCheck.Text, out chk);

                WinglGlobal.invpGlobal.CheckNumber = chk;
                WinglGlobal.invpGlobal.CheckDate = dtDate.SelectedDate.Value;
                WinglGlobal.invpGlobal.invnumber = " ";
                WinglGlobal.invpGlobal.vName = " ";
            }
            else
            {
                WinglGlobal.invpGlobal.invnumber = txtSource.Text;
                WinglGlobal.invpGlobal.vName = txtSource.Text;
            }

            decimal amount = 0;
            decimal.TryParse(txtDebits.Text, out amount);
            WinglGlobal.invpGlobal.amountpaid = amount;

            WinglGlobal.invpGlobal.date = dtDate.SelectedDate.Value;
            WinglGlobal.invpGlobal.dueDate = dtDate.SelectedDate.Value;
            WinglGlobal.invpGlobal.Company = ((Company)lstCompanies.SelectedItem).CompanyNumber;
            WinglGlobal.invpGlobal.Department = Entries[0].DepartmentNumber;

            WinglGlobal.scanImmediate((chk != 0 ? true : false), WinglGlobal.invpGlobal, WindocLocation, true);
        }
    }
}
