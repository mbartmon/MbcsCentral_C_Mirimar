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
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Text.RegularExpressions;


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmDatabaseScripts.xaml
    /// </summary>
    public partial class frmDatabaseScripts : Window
    {
        public frmDatabaseScripts()
        {
            InitializeComponent();
        }

        private void frmDatabaseScripts_Loaded(object sender, RoutedEventArgs e)
        {
            string scriptPath = GlobalShared.baseAppPath;
            if (!scriptPath.EndsWith("\\"))
                scriptPath += "\\";
            scriptPath += "DbRepairScripts";
            Main.getForm(this);

            string[] files = Directory.GetFiles(scriptPath, "*.sql", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                System.Array.Sort(files);
                lstScripts.ItemsSource = files;
            }

        }

        private void frmDatabaseScripts_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            string filePath = (string)lstScripts.SelectedItem;
            if (filePath != null && filePath.Length > 0)
            {
                FileInfo file = new FileInfo(filePath);
                string script = file.OpenText().ReadToEnd();
                string[] commands = Regex.Split(script, "^GO", RegexOptions.Multiline);
                SqlConnection con = new SqlConnection(GlobalShared.SqlConnectionString);
                
                Server server = new Server(new ServerConnection(con));
                StringCollection scl = new StringCollection();
                foreach(string t in commands)
                {
                    if (t.Trim().Length > 0)
                        scl.Add(t.Trim());
                }
                try
                {
                    int[] result = server.ConnectionContext.ExecuteNonQuery(scl, ExecutionTypes.ContinueOnError);
                    foreach(int i in result)
                    {
                        MessageBoxCustom.Show(scl[i]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.Show(ex.Message);
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
