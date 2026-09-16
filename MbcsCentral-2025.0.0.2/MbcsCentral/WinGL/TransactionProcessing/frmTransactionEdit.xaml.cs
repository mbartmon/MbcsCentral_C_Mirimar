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
    /// Interaction logic for frmTransactionEdit.xaml
    /// </summary>
    public partial class frmTransactionEdit : Window
    {
        string STindex;
        Company[] companies;
        Department[] departments;
        Accounts[] accounts;
        Transactiontemp tran = null;
        Transactions transaction = null;
        int transactionLength = 128;
        public bool temp { get; set; } = true;
        public frmTransactionEdit(string Index)
        {
            InitializeComponent();
            STindex = Index;
        }

        private void frmTransactionEdit_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int.TryParse(STindex, out index);
            if (index == 0 ||  (temp ? (tran = Transactiontemp.load(index)) == null : (transaction = Transactions.load(index)) == null) )
            {
                MessageBoxCustom.Show("Could not find specified transaction");
                return;
            }
            Main.getForm(this);
            companies = Company.load();
            lstCompanies.ItemsSource = companies;
            departments = Department.load();
            lstDepartments.ItemsSource = departments;
            JournalNames[] journals = JournalNames.load();
            lstJournals.ItemsSource = journals;
            string journalName = "";
            if (temp)
                journalName = tran.Journal;
            else
                journalName = transaction.Journal;
            for (int i = 0; i < lstJournals.Items.Count; i++)
            {
                if (((JournalNames)lstJournals.Items[i]).JournalName.Equals(journalName))
                {
                    lstJournals.SelectedIndex = i;
                    break;
                }
            }
            dtDate.DisplayDate = DateTime.Now;
            accounts = Accounts.load();
            lstAccounts.ItemsSource = accounts;
            if (temp)
            {
                this.DataContext = tran;
                tran.IsChanged = false;
            }
            else
            {
                txtDescription.MaxLength = transactionLength;
                this.DataContext = transaction;
                transaction.IsChanged = false;
            }
        }

        private void frmTransactionEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (temp)
            {
                if (lstJournals.SelectedIndex >= 0)
                    tran.Journal = ((JournalNames)lstJournals.SelectedItem).JournalName;
                tran.save();
            }
            else
            {
                if (lstJournals.SelectedIndex >= 0)
                    transaction.Journal = ((JournalNames)lstJournals.SelectedItem).JournalName;
                transaction.save();
            }
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            if ((temp && tran.IsChanged) || (!temp && transaction.IsChanged))
            {
                MessageBoxResult r = MessageBoxCustom.Show("Save Changes?", "", MessageBoxButton.YesNoCancel);
                switch (r)
                {
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, e);
                        break;
                    case MessageBoxResult.Cancel:
                        return;

                }
            }
            this.Close();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("Delete this transaction?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (temp)
                    tran.delete(tran.IndexNumber.ToString());
                else
                    transaction.delete(transaction.IndexNumber.ToString());
                this.Close();
            }
        }
    }
}
