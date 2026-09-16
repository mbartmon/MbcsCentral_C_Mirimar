using System;
using System.Collections;
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

namespace MbcsCentral.Email
{
    /// <summary>
    /// Interaction logic for frmChooseEmailAccount.xaml
    /// </summary>
    public partial class frmChooseEmailAccount : Window
    {
        public Email.SmtpConfig[] emails { get; set; }
        public int ID { get; set; } = -1;

        public frmChooseEmailAccount()
        {
            InitializeComponent();
            this.Topmost = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (emails == null || emails.Length == 0)
            {
                MessageBox.Show("No email accounts from which to select.");
                this.Close();
            }
            ArrayList items = new ArrayList();
            foreach (Email.SmtpConfig smtp in emails)
                items.Add(new ListItem(smtp.description.Trim() + " <" + smtp.emailAddress.Trim() + ">", smtp.ID));
            lstEmails.ItemsSource = items;
        }
        private void lstEmails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmdSelect_Click(sender, new RoutedEventArgs());
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (lstEmails.SelectedIndex < 0)
            {
                MessageBox.Show("No email account selected.");
                return;
            }
            ID = lstEmails.SelectedIndex;
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No email account selected.");
            ID = -1;
            this.Close();
        }

    }
}
