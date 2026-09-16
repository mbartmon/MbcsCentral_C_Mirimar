using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmCategoriesAccounts.xaml
    /// </summary>
    public partial class frmCategoriesAccounts : Window
    {
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmCategoriesAccounts()
        {
            InitializeComponent();
        }

        public frmCategoriesAccounts(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }

        Point _startPoint;
        bool IsDragging = false;

        Category currentCat = null;

        Accounts currentAcct;

        bool CategoryChanged;
        bool AccountChanged;
        int NewType;
        enum NewTypeEnum
        {
            MainCategory,
            SubCategory,
            SubSubCategory,
            Account,
            AddAccount
        }
        string NewDescription;
        bool editNameOnly = false;
        MajorCategoryList major;
        List<MajorCategories> majorList;
        Collection<Category> majorCats = new Collection<Category>();

        private void formCategorieAccounts_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            get_nodes();
            majorList = major.GetCategories();
            lstCategories.ItemsSource = majorList;
        }

        private void get_nodes()
        {

            lstCategories.Items.Clear(); 

            major = new MajorCategoryList();       
        }

        private void formCategorieAccounts_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CategoryChanged && GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxResult mr = MessageBox.Show("One or more Categories Changed. Do you wish to save them?", "", MessageBoxButton.YesNoCancel);
                switch (mr)
                {
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, new RoutedEventArgs());
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
            if (AccountChanged && GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxResult mr = MessageBox.Show("One or more changes made to Accounts.  Do you wish to save them?", "", MessageBoxButton.YesNoCancel);
                switch (mr)
                {
                    case MessageBoxResult.Yes:
                        cmdSaveAccount_Click(lstAccounts, new RoutedEventArgs());
                        AccountChanged = false;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        return;
                }
                AccountChanged = false;
            }

            Main.saveForm(this);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to delete.");
                return;
            }
            // Check for current and prior year transactions before deleting.  If found, prohibit delete.
            if (Transactions.FindCurrentPriorTrans(currentCat))
             {
                  MessageBox.Show("Category contains Curent and or Prior year transactions and cannot be deleted.", "");
                  return;
             }
            
            if (currentCat is MajorCategories || currentCat is SubCat)
            {
                if (MessageBox.Show("All sub-categories, accounts and transactions will also be deleted. Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            int idx;
            if (currentCat is MajorCategories)
            {

                ObservableCollection<SubCat> SubList = ((MajorCategories)currentCat).SubCats;
                ObservableCollection<SubSubCat> SubSubList;
                if (SubList == null)
                {
                    DeleteTransactions(currentCat.Key);
                    majorList.Remove((MajorCategories)currentCat);
                }
                else
                {
                    foreach (SubCat sub in SubList)
                    {
                        SubSubList = ((SubCat)sub).SubSubCats;
                        if (SubSubList != null)
                        {
                            foreach (SubSubCat subsub in SubSubList)
                            {
                                idx = sub.SubSubCats.IndexOf(subsub);
                                sub.SubSubCats.RemoveAt(idx);
                                DeleteTransactions(subsub.Key);
                            }
                        }
                        idx = SubList.IndexOf(sub);
                        SubList.RemoveAt(idx);
                        DeleteTransactions(sub.Key);
                    }
                }
                DeleteTransactions(currentCat.Key);
                idx = majorList.IndexOf((MajorCategories)currentCat);
                majorList.RemoveAt(idx);
            } 
            else if(currentCat is SubCat)
            {
                ObservableCollection<SubSubCat> slist1 = (ObservableCollection<SubSubCat>)((SubCat)currentCat).SubSubCats;
                if (slist1 != null)
                {
                    foreach (SubSubCat subSub in slist1)
                    {
                        DeleteTransactions(subSub.Key);
                    }
                    ObservableCollection<SubCat> slist2 = (ObservableCollection<SubCat>)((MajorCategories)currentCat.Parent).SubCats;
                    idx = slist2.IndexOf((SubCat)currentCat);
                    if (idx >= 0)
                        slist2.RemoveAt(idx);
                    DeleteTransactions(currentCat.Key);
                }
            }
            else // currentCat is SubSubCat
            {
                ObservableCollection<SubSubCat> slist = (ObservableCollection<SubSubCat>)((SubCat)currentCat.Parent).SubSubCats;
                idx = slist.IndexOf((SubSubCat)currentCat);
                if (idx >= 0)
                    slist.RemoveAt(idx);
                DeleteTransactions(currentCat.Key);
            }
            CategoryChanged = true;
        }

        private void DeleteTransactions(string key)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to delete.");
                return;
            }
            Accounts[] accts = Accounts.loadByCategory(key);
            if (accts != null)
            {
                foreach (Accounts acct in accts)
                {
                    Transactions.DeleteByAccount(acct.AccountNumber);
                    acct.delete(acct.AccountNumber.ToString());
                }
            }
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdEditAccount_Click(object sender, RoutedEventArgs e)
        {
            NewType = (int)NewTypeEnum.Account;
            InputTextBox.Text = currentAcct.Description;
            Grid.SetRow(InputBox, 1);
            Grid.SetColumn(InputBox, 2);
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputTextBox.Focus();

        }

        private void cmdAddAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to add.");
                return;
            }
            if (currentCat == null)
            {
                MessageBox.Show("You must first choose a Category for the new Account.");
                return;
            }
            if (MessageBox.Show("New Account will immediately be added to the Database.  Continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            NewType = (int)NewTypeEnum.AddAccount;
            InputTextBox.Text = "";
            Grid.SetRow(InputBox, 1);
            Grid.SetColumn(InputBox, 2);
            InputTextLabel.Text = "Account Name";
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputTextBox.Focus();
            InputBox.Focus();
        }

        private void cmdDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to delete.");
                return;
            }
            if (currentAcct != null)
            {
                int year = DateTime.Now.Year;
                DateTime start = new DateTime(year - 1, 1, 1);
                DateTime end = new DateTime(year, 12, 31, 23, 59, 59);
                if (Transactions.loadByAccountDates(currentAcct.AccountNumber, start, end) != null)
                {
                    MessageBox.Show("Account contains Curent and or Prior year transactions and cannot be deleted.");
                    return;
                }
                if (MessageBox.Show("Delete this account?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Transactions.DeleteByAccount(currentAcct.AccountNumber);
                    currentAcct.delete(currentAcct.Key.ToString());
                }
                lstAccounts.ItemsSource = null;

                // Retrieve Accounts for current category
                Accounts[] accts = Accounts.loadByCategory((string)currentCat.Key, true, false);
                if (accts != null)
                {
                    lstAccounts.ItemsSource = accts;
                }
            }
            else
                MessageBox.Show("No Account selected. Try again.");
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            int si = lstAccounts.SelectedIndex;
            if (si > 0)
            {
                Accounts[] accts = new Accounts[lstAccounts.Items.Count];
                for (int i = 0; i < lstAccounts.Items.Count; i++)
                    accts[i] = (Accounts)lstAccounts.Items[i];
                Accounts acct = (Accounts)lstAccounts.SelectedItem;
                Accounts temp = (Accounts)lstAccounts.Items[lstAccounts.SelectedIndex - 1];
                acct.AccountNumber = (short)(lstAccounts.SelectedIndex - 1);
                temp.AccountNumber = (short)(lstAccounts.SelectedIndex);
                accts[lstAccounts.SelectedIndex - 1] = acct;
                accts[lstAccounts.SelectedIndex] = temp;
                lstAccounts.ItemsSource = null;
                lstAccounts.ItemsSource = accts;
                accts = null;
                lstAccounts.SelectedIndex = si - 1;
                AccountChanged = true;
            }
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            int si = lstAccounts.SelectedIndex;
            if (si < lstAccounts.Items.Count- 1)
            {
                Accounts[] accts = new Accounts[lstAccounts.Items.Count];
                for (int i = 0; i < lstAccounts.Items.Count; i++)
                    accts[i] = (Accounts)lstAccounts.Items[i];
                Accounts acct = (Accounts)lstAccounts.SelectedItem;
                Accounts temp = (Accounts)lstAccounts.Items[lstAccounts.SelectedIndex + 1];
                acct.AccountNumber = (short)(lstAccounts.SelectedIndex + 1);
                temp.AccountNumber = (short)(lstAccounts.SelectedIndex);
                accts[lstAccounts.SelectedIndex  + 1] = acct;
                accts[lstAccounts.SelectedIndex] = temp;
                lstAccounts.ItemsSource = null;
                lstAccounts.ItemsSource = accts;
                accts = null;
                lstAccounts.SelectedIndex = si + 1;
                AccountChanged = true;
            }

        }

        /*
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            if (lstAccounts.SelectedIndex < lstAccounts.Items.Count - 1)
            {
                DataView vw = (DataView)lstAccounts.ItemsSource;
                int i = lstAccounts.SelectedIndex;
                DataRowView drS = (DataRowView)lstAccounts.SelectedItem;
                short s = (short)drS.Row[Accounts.SQL_COL_ACCOUNT_NUMBER];
                lstAccounts.SelectedIndex = ++i;
                DataRowView drT = (DataRowView)lstAccounts.SelectedItem;
                drS.Row[Accounts.SQL_COL_ACCOUNT_NUMBER] = drT.Row[Accounts.SQL_COL_ACCOUNT_NUMBER];
                drT.Row[Accounts.SQL_COL_ACCOUNT_NUMBER] = s;
                lstAccounts.ItemsSource = null;
                vw.Sort = Accounts.SQL_COL_ACCOUNT_NUMBER;
                lstAccounts.ItemsSource = vw;
                lstAccounts.SelectedIndex = i;
                AccountChanged = true;
            }

        }
        */
        private void CategoriesAddRoot_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetRow(InputBox, 1);
            Grid.SetColumn(InputBox, 0);
            InputTextLabel.Text = "Category Name";
            InputTextBox.Text = "";
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputTextBox.Focus();
            NewType = (int)NewTypeEnum.MainCategory;
        }
        private void CategoriesAddRoot()
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to add.");
                return;
            }
            Category cat = new Category();
            cat.CatType = (int)Category.CatTypes.Major;
            cat.CategoryDescription = InputTextBox.Text;
            string key = MajorCategoryList.GetHighestKey(majorList);
            int k;
            try
            {
                k = int.Parse(key.Replace("_", ""));
            }
            catch
            {
                return;
            }
            key = string.Format("{0:000}_", ++k);
            short s = MajorCategoryList.GetHighestSequence(majorList);
            cat.SubOf = "0_";
            cat.Key = key;
            cat.Sequence = ++s;

            InsertRow(majorList, cat, null);
        }

        private void CategoriesAddSub_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to add.");
                return;
            }
            if (currentCat == null)
            {
                MessageBox.Show("You must first select a major category.");
                return;
            }
            Grid.SetRow(InputBox, 1);
            Grid.SetColumn(InputBox, 0);
            InputTextLabel.Text = "Category Name";
            InputTextBox.Text = "";
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputTextBox.Focus();
            NewType = (int)NewTypeEnum.SubCategory;
        }

        private void CategoriesAddSub()
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to add.");
                return;
            }
            Category newSub = new Category();
            newSub.CategoryDescription = InputTextBox.Text;
            if (currentCat != null && currentCat.CatType != (int)Category.CatTypes.SubSub)
            {
                newSub.SubOf = currentCat.Key;
                newSub.CatType = currentCat.CatType + 1;
                newSub.Parent = currentCat;
            }
            else
                return;

            //*****
            string key = MajorCategoryList.GetHighestKey(majorList);

            int k;
            try
            {
                k = int.Parse(key.Replace("_", ""));
            }
            catch
            {
                return;
            }
            key = string.Format("{0:000}_", ++k);
            newSub.Key = key;

            //*******
            short s = MajorCategoryList.GetHighestSequence(majorList);

            newSub.Sequence = ++s;

            //*******
            InsertRow(majorList, newSub, currentCat);
        }

        private void InsertRow(List<MajorCategories> mlist, Category insertRow, Category targetRow)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            if (targetRow == null || targetRow.SubOf.Equals("0_"))
            {
                if (targetRow != null)
                {
                    insertRow.SubOf = targetRow.Key;
                    ((MajorCategories)(targetRow)).SubCats.Add(new SubCat(insertRow));
                }
                else
                {
                    mlist.Add(new MajorCategories(insertRow));
                }
                lstCategories.ItemsSource = null;
                MajorCategoryList.RenumberCategories(ref mlist);
                lstCategories.ItemsSource = mlist;
            }
            else if (NewType == (int)NewTypeEnum.SubCategory)
            {
                insertRow.Parent = targetRow;
                ((SubCat)targetRow).SubSubCats.Add(new SubSubCat(insertRow));
                insertRow.Parent = targetRow;
                Int16 src = (Int16)insertRow.Sequence;
                Int16 dst = (Int16)targetRow.Sequence;
                Int16 i;
                if (src > dst)
                {
                    foreach (MajorCategories cat in mlist)
                    {
                        i = cat.Sequence;
                        if (i > dst)
                            cat.Sequence = ++i;
                        if (cat.SubCats != null)
                        {
                            foreach (SubCat scat in cat.SubCats)
                            {
                                i = scat.Sequence;
                                if (i > dst)
                                    scat.Sequence = ++i;
                                if (scat.SubSubCats != null)
                                {
                                    foreach (SubSubCat sscat in scat.SubSubCats)
                                    {
                                        i = sscat.Sequence;
                                        if (i > dst)
                                            sscat.Sequence = ++i;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (MajorCategories cat in mlist)
                    {
                        i = cat.Sequence;
                        if (i > src && i <= dst)
                            cat.Sequence = ++i;
                        if (cat.SubCats != null)
                        {
                            foreach (SubCat scat in cat.SubCats)
                            {
                                i = scat.Sequence;
                                if (i > dst)
                                    scat.Sequence = ++i;
                                if (scat.SubSubCats != null)
                                {
                                    foreach (SubSubCat sscat in scat.SubSubCats)
                                    {
                                        i = sscat.Sequence;
                                        if (i > dst)
                                            sscat.Sequence = ++i;
                                    }
                                }
                            }
                        }
                    }

                }
            }
                 
            lstCategories.ItemsSource = null;
            MajorCategoryList.RenumberCategories(ref mlist);
            lstCategories.ItemsSource = mlist;
            CategoryChanged = true;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to save.");
                return;
            }
            bool changed = false;
            if (Category.deleteAll()) // will this work?
            {
                foreach (MajorCategories cat in majorList)
                {
                    cat.Id = 0;
                    cat.save();
                    if (cat.SubCats != null)
                    {
                        foreach (SubCat scat in cat.SubCats)
                        {
                            scat.Id = 0;
                            scat.save();
                            if (scat.SubSubCats != null)
                            {
                                foreach (SubSubCat sscat in scat.SubSubCats)
                                {
                                    sscat.Id = 0;
                                    sscat.save();
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Category changes saved.");
            }
            CategoryChanged = false;
        }

        private void cmdSaveAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to save.");
                return;
            }
            bool changed = false;
            foreach (Accounts acct in lstAccounts.Items)
            {
                if (acct.IsChanged)
                {
                    acct.save();
                    changed = true;
                }
            }
            MessageBox.Show((changed ? "Account changes saved." : "No Account changes to save."));
            AccountChanged = false;
        }

        // 
        // Drag and Drop Methods
        //
        private void lstCategories_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           // return;
            currentCat = null;
            if (e.OriginalSource is TextBlock)
            {
                try
                {
                    object obj = GetTreeViewItemClicked((FrameworkElement)e.OriginalSource, lstCategories);
                    if (obj != null)
                    {
                        TreeViewItem item = (TreeViewItem)GetTreeViewItemClicked((FrameworkElement)e.OriginalSource, lstCategories);
                        if (item.Header is MajorCategories)
                        {
                            currentCat = item.Header as Category;
                        }
                        else if (item.Header is SubCat)
                        {
                            currentCat = item.Header as SubCat;
                        }
                        else if (item.Header is SubSubCat)
                        {
                            currentCat = item.Header as SubSubCat;
                        }

                    }
                    //if (obj == null || currentCat.SubOf.Equals("0_"))
                     //   return;
                }
                catch (Exception ex) { }

                if (currentCat != null)
                {
                    if (AccountChanged)
                    {
                        MessageBoxResult mr = MessageBox.Show("One or more changes made to Accounts.  Do you wish to save them?", "", MessageBoxButton.YesNoCancel);
                        switch (mr)
                        {
                            case MessageBoxResult.Yes:
                                cmdSaveAccount_Click(lstAccounts, new RoutedEventArgs());
                                AccountChanged = false;
                                break;
                            case MessageBoxResult.Cancel:
                                return;
                        }
                        AccountChanged = false;
                    }
                    lstAccounts.ItemsSource = null;

                    // Retrieve Accounts for current category
                    lstAccounts.ItemsSource = null;
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "1 - Retrieve Account Record.", false);
                    Accounts[] accts = Accounts.loadByCategory((string)currentCat.Key, true, false);
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "2 - Account Record Retrieved.", false);
                    if (accts != null && accts.Length > 0)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "3 - Got " + accts.Length.ToString() + " Accounts.", false);
                        lstAccounts.ItemsSource = accts;
                    }
                    _startPoint = e.GetPosition(null);
                }
            }
        }

        private object GetTreeViewItemClicked(FrameworkElement sender, TreeView treeView, double? x = null, double? y = null)
        {
            Point p = new Point();
            if (x == null)
                p = ((sender as FrameworkElement)).TranslatePoint(new Point(0, 0), treeView);
            else
            {
                p.X = (double)x;
                p.Y = (double)y;
            }
            DependencyObject obj = treeView.InputHitTest(p) as DependencyObject;
            while (obj != null && !(obj is TreeViewItem))
                obj = VisualTreeHelper.GetParent(obj);
            return obj; // as TreeViewItem;
        }

        private void lstCategories_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging  && e.OriginalSource is TextBlock)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e, sender);

                }
            }   
        }

        private void lstAccounts_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            currentAcct = null;
            if (e.OriginalSource is TextBlock)
            {
                try
                {
                    object obj = GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstAccounts);
                    if (obj != null)
                    {
                        ListBoxItem item = (ListBoxItem)GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstAccounts);
                        currentAcct = (Accounts)item.Content;
                    }
                }
                catch { }
            }
        }
        
        private object GetListBoxItemClicked(FrameworkElement sender, ListBox list, double? x = null, double? y = null)
        {
            Point p = new Point();
            if (x == null)
                p = ((sender as FrameworkElement)).TranslatePoint(new Point(0, 0), list);
            else
            {
                p.X = (double)x;
                p.Y = (double)y;
            }
            DependencyObject obj = list.InputHitTest(p) as DependencyObject;
            while (obj != null && !(obj is ListBoxItem))
                obj = VisualTreeHelper.GetParent(obj);
            return obj;
        }

        private void StartDrag(MouseEventArgs e, object sender)
        {
            if (currentCat != null)
            {
                IsDragging = true;
                if (sender is TreeView)
                {
                    DataObject data = new DataObject(currentCat);
                    DragDropEffects de = DragDrop.DoDragDrop(this.lstCategories, data, DragDropEffects.Move);
                }
                else if (currentAcct != null)
                {
                    DataObject data = new DataObject(DataFormats.Serializable ,currentAcct);
                    DragDropEffects de = DragDrop.DoDragDrop(this.lstAccounts, data, DragDropEffects.Move);

                }
                IsDragging = false;
            }
        }

        private void lstCategories_Drop(object sender, DragEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            // Row on which to drop original row (Target)
            Point pt = e.GetPosition(lstCategories);
            object obj = GetTreeViewItemClicked(lstCategories as FrameworkElement, lstCategories, pt.X, pt.Y);
            if (obj == null)
                return;

            Category dropCat = (Category)((TreeViewItem)obj).Header;

            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                if (currentAcct != null)
                {
                    if (MessageBox.Show("Moving account to another category will be saved immediately. Do you wish to proceed?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                    if (dropCat.SubOf.Equals("0_"))
                    {
                        MessageBox.Show("Account cannot be assigned to a Major Category.");
                        return;
                    }
                    currentAcct.MajorCategory = dropCat.SubOf;
                    currentAcct.MinorCategory = dropCat.Key;
                    string catPath = "";
                    if (dropCat.Parent is MajorCategories)
                    {
                        catPath = ((MajorCategories)dropCat.Parent).CategoryDescription + "/" + dropCat.CategoryDescription;
                    }
                    else
                    {
                        MajorCategories major = (MajorCategories) ((SubCat)dropCat.Parent).Parent;
                        catPath = major.CategoryDescription + "/" + ((SubCat)dropCat.Parent).CategoryDescription + "/" + dropCat.CategoryDescription;
                    }
                    currentAcct.CategoryPath = catPath;

                    currentAcct.save();
                    Accounts[] accts = Accounts.loadByCategory((string)currentCat.Key);
                    if (accts != null)
                    {
                        lstAccounts.ItemsSource = accts;
                    }
                }
            }
            else
            {

                if (currentCat == dropCat)
                {
                    MessageBox.Show("Cannot drop item onto itself");
                    return;
                }

                // if target row is a top category, make source row a sub of it
                // What to do if source is a top category?  Make it the next top category.  If dropped inside (on a sub) make it a sub of this top category.
                if (dropCat != null && currentCat != null)
                {
                    if (dropCat.SubOf.Equals("0_"))
                    {
                        int idxD;
                        int idxC;
                        idxD = majorList.IndexOf((MajorCategories)dropCat, 0, majorList.Count);
                        if (currentCat.SubOf.Equals("0_"))
                        {
                            // Move major category from its current position to one that is after the current drop location 
                            MessageBoxResult rc = MessageBox.Show("Make dragged item a subCategory of the drop location?", "", MessageBoxButton.YesNoCancel);
                            idxC = majorList.IndexOf((MajorCategories)currentCat, 0, majorList.Count);
                            switch (rc)
                            {
                                case MessageBoxResult.Yes:
                                    rc = MessageBox.Show("Keep all SubSubCategories of the original MajorCategory?", "", MessageBoxButton.YesNoCancel);
                                    bool keep = false;
                                    switch (rc)
                                    {
                                        case MessageBoxResult.Yes:
                                            keep = true;
                                            break;
                                        case MessageBoxResult.Cancel:
                                            return;
                                            break;
                                    }
                                    majorList.RemoveAt(idxC);
                                    currentCat.SubOf = dropCat.Key;
                                    currentCat.Parent = dropCat;
                                    SubCat newCat = ((MajorCategories)currentCat).DemoteToSub(keep);
                                    ((MajorCategories)dropCat).SubCats.Add(newCat);
                                    CategoryChanged = true;
                                    break;
                                case MessageBoxResult.No:
                                    majorList.RemoveAt(idxC);
                                    majorList.Insert(idxD, (MajorCategories)currentCat);
                                    CategoryChanged = true;
                                    break;

                                case MessageBoxResult.Cancel:
                                    return;
                                    break;
                            }

                        }
                        else
                        {
                            // Source catetgory (currentCat) is a SubCat or SubSub being dropped onto a MajorCat (dropCat)
                            if (currentCat is SubCat)
                            {

                                ObservableCollection<SubCat> slist = (ObservableCollection<SubCat>)((MajorCategories)currentCat.Parent).SubCats;
                                idxC = slist.IndexOf((SubCat)currentCat);
                                // Promote?
                                MessageBoxResult rc = MessageBox.Show("Make a subCategory of the drop Location?", "", MessageBoxButton.YesNoCancel);
                                switch (rc)
                                {
                                    case MessageBoxResult.Yes:
                                        slist.RemoveAt(idxC);
                                        currentCat.SubOf = dropCat.Key;
                                        currentCat.Parent = dropCat;
                                        ((MajorCategories)dropCat).SubCats.Add((SubCat)currentCat);
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.No:
                                        slist.RemoveAt(idxC);
                                        currentCat.SubOf = "0_";
                                        MajorCategories newCat = ((SubCat)currentCat).PromoteToMajor();
                                        majorList.Insert(idxD + 1, newCat);
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.Cancel:
                                        return;
                                        break;
                                }
                            }
                            else  // currentCat is a SubSubCat
                            {
                                ObservableCollection<SubSubCat> slist = (ObservableCollection<SubSubCat>)((SubCat)currentCat.Parent).SubSubCats;
                                idxC = slist.IndexOf((SubSubCat)currentCat);

                                // Promote?
                                MessageBoxResult rc = MessageBox.Show("Make a subCategory of the drop Location?", "", MessageBoxButton.YesNoCancel);
                                switch (rc)
                                {
                                    case MessageBoxResult.Yes:
                                        slist.RemoveAt(idxC);
                                        currentCat.SubOf = dropCat.Key;
                                        currentCat.Parent = dropCat;
                                        ((MajorCategories)dropCat).SubCats.Add(new SubCat(currentCat));
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.No:
                                        slist.RemoveAt(idxC);
                                        currentCat.SubOf = "0_";
                                        majorCats.Insert(0, new SubCat(currentCat));
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.Cancel:
                                        return;
                                        break;
                                }
                            }
                        }
                        lstCategories.ItemsSource = null;
                        //RenumberCategories(ref vw);
                        lstCategories.ItemsSource = majorList;
                    }
                    else  // Both source and drop items are sub or subsub categories
                    {
                        ObservableCollection<SubCat> slist1;
                        ObservableCollection<SubSubCat> slist2;
                        if (dropCat is SubCat)
                        {
                            if (currentCat is SubCat)
                            {
                                slist1 = (ObservableCollection<SubCat>) ((MajorCategories) currentCat.Parent).SubCats;
                                MessageBoxResult rc = MessageBox.Show("Make a subCategory of the drop Location?", "",
                                    MessageBoxButton.YesNoCancel);
                                switch (rc)
                                {
                                    case MessageBoxResult.Yes:
                                        slist1.Remove((SubCat) currentCat);
                                        currentCat.SubOf = dropCat.Key;
                                        currentCat.Parent = dropCat;
                                        SubSubCat newCat = new SubSubCat(currentCat);
                                            //((SubCat)currentCat).DemoteToSubSub();
                                        ((SubCat) dropCat).SubSubCats.Add(newCat);
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.No:
                                        currentCat.SubOf = dropCat.SubOf;
                                        slist1.Remove((SubCat) currentCat);
                                        currentCat.Parent = dropCat.Parent;
                                        ((MajorCategories) dropCat.Parent).SubCats.Add((SubCat) currentCat);
                                        CategoryChanged = true;
                                        break;
                                    case MessageBoxResult.Cancel:
                                        return;
                                        break;
                                }
                            }
                            else // currentCat is SubSubCat
                            {
                                slist2 = (ObservableCollection<SubSubCat>) ((SubCat) currentCat.Parent).SubSubCats;
                                MessageBoxResult rc = MessageBox.Show("Make a subCategory of the drop Location?", "",
                                    MessageBoxButton.YesNoCancel);
                                try
                                {
                                    switch (rc)
                                    {
                                        case MessageBoxResult.Yes:
                                            slist2.Remove((SubSubCat) currentCat);
                                            currentCat.SubOf = dropCat.Key;
                                            currentCat.Parent = dropCat;
                                            ((SubCat) dropCat).SubSubCats.Add((SubSubCat) currentCat);
                                            CategoryChanged = true;
                                            break;
                                        case MessageBoxResult.No:
                                            slist2.Remove((SubSubCat) currentCat);
                                            currentCat.SubOf = dropCat.SubOf;
                                            currentCat.Parent = dropCat.Parent;
                                            ((MajorCategories) dropCat.Parent).SubCats.Add((SubCat) currentCat);
                                            CategoryChanged = true;
                                            break;
                                        case MessageBoxResult.Cancel:
                                            return;
                                            break;
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                        else  // Target is a SubSubCat
                        {
                            if (currentCat is SubCat)
                            {
                                slist1 = (ObservableCollection<SubCat>)((MajorCategories)currentCat.Parent).SubCats;
                                MessageBoxResult rc = MessageBox.Show("If the item that you are moving contains sub-categories, they will be removed.  Continue?", "", MessageBoxButton.YesNo);
                                switch (rc)
                                {
                                    case MessageBoxResult.Yes:
                                        slist1.Remove((SubCat)currentCat);
                                        currentCat.SubOf = dropCat.SubOf;
                                        currentCat.Parent = dropCat.Parent;
                                        SubSubCat newCat = new SubSubCat(currentCat);
                                        ((SubCat)dropCat.Parent).SubSubCats.Add(newCat);
                                        CategoryChanged = true;
                                       break;
                                    case MessageBoxResult.No:
                                        break;
                                }
                            }
                            else // Current Cat is SubSub
                            {
                                ObservableCollection<SubSubCat> slist3 = (ObservableCollection<SubSubCat>)((SubCat)currentCat.Parent).SubSubCats;
                                slist3.Remove((SubSubCat)currentCat);
                                ((SubCat)dropCat.Parent).SubSubCats.Add((SubSubCat)currentCat);
                            }
                        }
                    }
                }
            }
        }
        /*
        private void RenumberCategories(ref DataView vw)
        {
             
            DataRow[] drs = vw.Table.Select("Sequence > 0", "Sequence ASC");
            DataTable dt = drs.CopyToDataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            vw = null;
            vw = GetCategories(ds);
        }
         */

        private void YesButton_Click(object sender, RoutedEventArgs e)
 	 	{
 	 	 	InputBox.Visibility = System.Windows.Visibility.Collapsed;
            NewDescription = InputTextBox.Text;
            if (NewDescription != null && !NewDescription.Trim().Equals(""))
            {
                switch (NewType)
                {
                    case (int)NewTypeEnum.Account:
                        currentAcct.Description = InputTextBox.Text;
                        AccountChanged = true;
                        break;
                    case (int)NewTypeEnum.MainCategory:
                        if (editNameOnly)
                            Rename();
                        else
                            CategoriesAddRoot();
                        break;
                    case (int)NewTypeEnum.SubCategory:
                        if (editNameOnly)
                            Rename();
                        else
                            CategoriesAddSub();
                        break;
                    case(int)NewTypeEnum.SubSubCategory:
                        Rename();
                        break;
                    case (int)NewTypeEnum.AddAccount:
                        Accounts acct = new Accounts();
                        acct.Description = InputTextBox.Text;
                        acct.AccountNumber = 0;
                        acct.active = true;
                        acct.MinorCategory = currentCat.Key;
                        acct.MajorCategory = currentCat.SubOf;
                 
                        if (currentCat is MajorCategories)
                        {
                            acct.CategoryPath = currentCat.CategoryDescription;  
                        }
                        else if (currentCat is SubCat)
                        {
                            acct.CategoryPath = ((MajorCategories)currentCat.Parent).CategoryDescription + "/" + currentCat.CategoryDescription;
                        }
                        else
                        {
                            SubCat subcat = (SubCat)currentCat.Parent;
                            MajorCategories majorcat = (MajorCategories)subcat.Parent;
                            acct.CategoryPath = majorcat.CategoryDescription + "/" + subcat.CategoryDescription + "/" + currentCat.CategoryDescription;
                        }
                        acct.save();
                        lstAccounts.ItemsSource = null;

                        // Retrieve Accounts for current category
                        Accounts[] accts = Accounts.loadByCategory((string)currentCat.Key, true, false);
                        if (accts != null)
                        {
                            lstAccounts.ItemsSource = accts;
                        }
                        break;
                }
            }
            editNameOnly = false;
 	 	}
 	 	 		
        private void NoButton_Click(object sender, RoutedEventArgs e)
 	 	{
 	 	 	InputBox.Visibility = System.Windows.Visibility.Collapsed;
 	 	}

        private void RenameCatgory_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            editNameOnly = true;
            if (currentCat is MajorCategories)
            {
                NewType = (int)NewTypeEnum.MainCategory;
                InputTextLabel.Text = "Category Name";
            }
            else if (currentCat is SubCat)
            {
                NewType = (int)NewTypeEnum.SubCategory;
                InputTextLabel.Text = "Sub Category Name";
            }
            else
            {
                NewType = (int)NewTypeEnum.SubSubCategory;
                InputTextLabel.Text = "Sub Sub Category Name";
            }

            Grid.SetRow(InputBox, 1);
            Grid.SetColumn(InputBox, 0);
            InputTextBox.Text = currentCat.CategoryDescription;
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputTextBox.Focus();
        }

        private void Rename()
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            currentCat.CategoryDescription = InputTextBox.Text;
        }

        private void AccountsEdit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AccountsStatus_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            currentAcct.active = !currentAcct.active;
            AccountChanged = true;
        }

        private void CmdPrint_OnClick(object sender, RoutedEventArgs e)
        {
            AccountMasterReport.process();
        }
    }
}
