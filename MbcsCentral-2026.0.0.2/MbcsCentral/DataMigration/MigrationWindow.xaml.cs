using System;
using System.Collections;
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
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Xml;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MigrationWindow : Window
    {
        private string m_accessConnectionString;
        private string m_sqlConnectionString;
        private long m_clientId;
        private bool m_deleteFirst;
        private string m_TableFile;
        public string AccessConnectionString
        {
            set { m_accessConnectionString = value; }
        }
        public string SqlConnectionString
        {
            set { m_sqlConnectionString = value; }
        }
        public long ClientId
        {
            set { m_clientId = value; }
        }
        public bool DeleteFirst
        {
            set { m_deleteFirst = value; }
        }
        public string TableFile
        {
            set { m_TableFile = value; }
        }

        ObservableCollection<ProcessTables> tables = new ObservableCollection<ProcessTables>();

        public MigrationWindow()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            /*
            Program.m_AccessConnectionString = m_accessConnectionString;
            Program.sqlConnectionString = m_sqlConnectionString;
            Program.clientId = m_clientId;
            Program.deleteFirst = m_deleteFirst;
            Program.tables = tables;
            Program.importTables();
             * */

            frmProgress frm = new frmProgress();
            frm.AccessConnectionString = m_accessConnectionString;
            frm.SqlConnectionString = m_sqlConnectionString;
            frm.ClientId = m_clientId;
            frm.DeleteFirst = m_deleteFirst;
            frm.Tables = tables;
            frm.Show();

        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            string sourceTable = "";
            string targetTable = "";
            string converter = "";
            bool identity = false;

            this.Focus();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlReader = XmlReader.Create(GlobalShared.baseAppPath + m_TableFile, settings);
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
                        try
                        {
                            tables.Add(new ProcessTables(sourceTable, targetTable, converter, identity));
                        }
                        catch (SystemException ex)
                        {

                        }
                        sourceTable = "";
                        targetTable = "";
                    }
                }
            }
            xmlReader.Close();
            try
            {
                //tables = ((ProcessTables[])(list.ToArray(typeof(ProcessTables))));
                lstTables.ItemsSource = tables;
            }
            catch (SystemException ex)
            {

            }
        }

        private void cmdSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (ProcessTables tbl in tables)
            {
                tbl.IsChecked = true;
            }
        }

        private void cmdSelectNone_Click(object sender, RoutedEventArgs e)
        {
            foreach (ProcessTables tbl in tables)
            {
                tbl.IsChecked = false;
            }
        }
    }
}
