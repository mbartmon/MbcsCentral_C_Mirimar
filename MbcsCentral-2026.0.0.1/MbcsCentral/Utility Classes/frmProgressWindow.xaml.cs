using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for frmProgressWindow.xaml
    /// </summary>
    public partial class frmProgressWindow : Window
    {
        public struct ProgressData
        {
            public bool cancel;
            public string database;
            public string from;
            public string to;
            public string toDB;

            public string message;
        }

        public ProgressData progressdata;

        public frmProgressWindow(ProgressData prog)
        {
            InitializeComponent();

            progressdata = prog;
            txtDatabase.Text = prog.database;
            txtFrom.Text = prog.from;
            txtTo.Text = prog.to;
            txtToDB.Text = prog.toDB;
            txtProgress.Text = prog.message;
        }

        private void frmProgressWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 50;
            this.Topmost = true;
            loop();
        }
        private void loop()
        {
            try
            {
                while (!progressdata.cancel)
                {
                    //txtProgress.Text = progressdata.message;
                    Refresh();
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException ex)
            {
                Thread.ResetAbort();
            }
            this.Close();

        }

        private delegate void NoArgDelegate();

        public void Refresh()
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, (NoArgDelegate)delegate { });
        }
        private void frmProgressWindow_Closed(object sender, EventArgs e)
        {
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtToDB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = "";
        }

    }
}
