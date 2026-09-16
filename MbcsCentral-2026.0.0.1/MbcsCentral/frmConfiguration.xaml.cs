using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MbcsUtils;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmConfiguration.xaml
    /// </summary>
    public partial class frmConfiguration : Window
    {
        int i;
        Parms[] parms;

        public frmConfiguration()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parms = new Parms[Main.parms.GetDict.Count];
            foreach (var pair in Main.parms.GetDict)
            {
                parms[i] = new Parms();
                parms[i].Key = pair.Key;
                parms[i++].Value = pair.Value;
            }
            grdParms.ItemsSource = parms;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dict = Main.parms.GetDict;
            foreach (Parms parm in parms)
            {
                if (dict.ContainsKey(parm.Key))
                    dict[parm.Key] = parm.Value;
                else
                    dict.Add(parm.Key, parm.Value);
            }
            Main.parms.setConfigs();
            this.Close();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            int i = parms.Count() + 1;
            Array.Resize(ref parms, i );
            parms[i-1] = new Parms();
            grdParms.ItemsSource = null;
            grdParms.ItemsSource = parms;
        }
    }

    public class Parms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string m_key;
        string m_value;

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public string Key
        {
            get { return m_key; }
            set 
            {
                m_key = value;
                OnPropertyChanged("Key");
            }
        }
        public string Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                OnPropertyChanged("Value");
            }
        }
    }
}
