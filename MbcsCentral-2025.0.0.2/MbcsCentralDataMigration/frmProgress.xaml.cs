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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmProgress.xaml
    /// </summary>
    public partial class frmProgress : Window
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        public void write(string s, bool newItem = true)
        {
            //int NumberOfItems = lstProgress.ClientSize.Height / lstProgress.ItemHeight;
            int itemNumber = 0;
            if (!newItem)
            {
                itemNumber = lstProgress.Items.Count - 1;
                lstProgress.Items[itemNumber] = s;
            }
            else
            {
                lstProgress.Items.Add(s);
            }
            //f (lstProgress.TopIndex == lstProgress.Items.Count - NumberOfItems - 1)
            //{
                //The item at the top when you can just see the bottom item
           //     lstProgress.TopIndex = lstProgress.Items.Count - NumberOfItems + 1;
           // }
            //lstProgress.Refresh();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
