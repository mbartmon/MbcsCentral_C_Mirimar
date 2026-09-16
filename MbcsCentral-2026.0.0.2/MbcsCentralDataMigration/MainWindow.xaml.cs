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
using System.Xml;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ProcessTables> tables = new List<ProcessTables>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
           
            Program.importTables();

        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sourceTable = "";
            string targetTable = "";
            string converter = "";
            bool identity = false;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlReader = XmlReader.Create(Program.appPath + Program.TABLE_FILE, settings);
            while (xmlReader.Read())
            {
                if (xmlReader.Name.ToString().Equals("Table") && xmlReader.NodeType.ToString().Equals("Element"))
                {
                    identity = false;
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            if (xmlReader.Name.Equals("Source"))
                                sourceTable = (string)xmlReader.Value;
                            if (xmlReader.Name.Equals("Target"))
                                targetTable = (string)xmlReader.Value;
                            if (xmlReader.Name.Equals("Converter"))
                                converter = (string)xmlReader.Value;
                            if (xmlReader.Name.Equals("Identity"))
                                identity = Boolean.Parse(xmlReader.Value);
                        }
                    }
                    if (!sourceTable.Trim().Equals("") && !targetTable.Trim().Equals(""))
                    {
                        tables.Add(new ProcessTables(sourceTable, targetTable, converter, identity));
                        sourceTable = "";
                        targetTable = "";
                    }
                }
            }
            xmlReader.Close();
            lstTables.ItemsSource = tables;
        }
    }
}
