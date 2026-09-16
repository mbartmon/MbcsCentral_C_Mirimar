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

namespace MbcsCentral.Help
{
    /// <summary>
    /// Interaction logic for frmHelp.xaml
    /// </summary>
    public partial class frmHelp : Window
    {
        string thisApp;

        public frmHelp()
        {
            InitializeComponent();

        }

        public frmHelp(string app)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            string uriString = "Help/Contents.xaml?page=" + PageName;
            //FrameMain.Navigate(new System.Uri(uriString, UriKind.RelativeOrAbsolute));
            FrameMain.Navigate(new Contents(PageName));

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        public string PageName { get; set; } = "";
    }
}
