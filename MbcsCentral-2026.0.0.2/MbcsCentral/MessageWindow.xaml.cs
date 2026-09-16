using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        double time;

        public MessageWindow(string title, string message, double time = 5.0)
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.ShowActivated = true;
            this.Title = title;
            this.Message.Text = message;
            this.time = time;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null); 
            var t = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(time));
            });
            t.Wait();
            this.Close();
        }
    }
}
