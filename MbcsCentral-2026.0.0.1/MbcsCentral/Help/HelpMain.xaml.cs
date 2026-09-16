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
    /// Interaction logic for HelpMain.xaml
    /// </summary>
    public partial class HelpMain : Window
    {
        string thisApp = "";

        public HelpMain()
        {
            InitializeComponent();
            ContentsFrame.Navigate(new Contents());
        }

        public HelpMain(string app)
        {
            thisApp = app;
            InitializeComponent();
            ContentsFrame.Navigate(new Contents());
        }
    }
}
