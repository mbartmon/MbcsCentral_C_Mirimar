using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using ExtensionMethods;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for WaitWindow.xaml
    /// </summary>
    public partial class WaitWindow : Window
    {

        public ObservableCollection<Message> Items = new ObservableCollection<Message>();

        public WaitWindow()
        {
            InitializeComponent();
            this.DataContext = Items;
            Messages.ItemsSource = Items;
        }

        public string Heading
        { set { txtHeading.Text = value; } }

        public string Item
        {
            set
            {
                Items.Add(new Message() { Value = value });
                Messages.ScrollIntoView(Messages.Items.Count - 1);
                Messages.ItemsSource = null;
                Messages.ItemsSource = Items;
                Messages.Refresh();
                Messages.Focus();
            }
        }

        public bool CloseButton { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (CloseButton)
                cmdClose.Visibility = Visibility.Visible;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class Message : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Message()
        {
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }        
        string _name;
        
        public string Value
        {
            set
            {
                _name = value;
                OnPropertyChanged("Value");
            }
            get { return _name; }
        }
    }       

}
