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
using System.Windows.Controls;

namespace MbcsCentral.Help
{
    /// <summary>
    /// Interaction logic for Contents.xaml
    /// </summary>
    public partial class Contents : Page
    {
        public Contents()
        {
            InitializeComponent();
        }

        public Contents(string page)
        {
            InitializeComponent();
            if (!page.Trim().Equals(""))
            {
                frameContents.Navigate(new Uri(page, UriKind.RelativeOrAbsolute));
                foreach (TreeViewItem item in treeContents.Items)
                    item.IsExpanded = true;
            }
        }

    }

}
