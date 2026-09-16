using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.IO;
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


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmProcedureEdit.xaml
    /// </summary>
    public partial class frmProcedureEdit : Window
    {
        private frmProcedures callingForm;
        private StoredQueries currentQuery;
        private bool adding = false;
        private StoredQueries[] StQueries;

        public frmProcedureEdit(frmProcedures frm)
        {
            InitializeComponent();
            callingForm = frm;
        }

        private void frmProcedureEdit_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            StQueries = StoredQueries.load();
            lstQueries.ItemsSource = StQueries;

        }

        private void frmProcedureEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            StoredQuery query = null;
            if (!adding)
            {
                if (lstQueries.SelectedItem == null)
                {
                    MessageBoxCustom.Show("No query is being edited.", "", MessageBoxButton.OK, this);
                    return;
                }
                //query = (StoredQuery)lstQueries.SelectedItem;
            }
            //else
                query = (StoredQuery)grdMain.DataContext;
            List<string> errors = ValidateSQL(txtQuery.Text);
            if (errors != null && errors.Count > 0)
            {
                string msg = "ERROR(S) Found in query\r\n\n";
                for (int i = 0; i < errors.Count; i++)
                {
                    msg += (i + 1).ToString("##") + " : " + errors[i] + "\r\n";
                }
                msg += "\r\n\nSave anyway?";
                if (MessageBoxCustom.Show(msg, "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                   return;
            }

            StoredQueries sql = new StoredQueries() { ID = query.ID, Name = query.Name, Parameters = query.Parms, Query = query.Query };
            if (!sql.save())
                return;
            currentQuery = sql;
            MessageBoxCustom.Show("Query saved.", "", MessageBoxButton.OK, this);
            StQueries = StoredQueries.load();
            lstQueries.ItemsSource = null;
            if (StQueries == null || StQueries.Length == 0)
            {
                MessageBoxCustom.Show("Could not find Queries: ", "", MessageBoxButton.OK, this);
                return;
            }
            lstQueries.ItemsSource = StQueries;
            adding = false;
            cmdTest.IsEnabled = true;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            string qName = "";
            MessageBoxCustom.Show("Enter NAME for new query: ", out qName, 50, null, this);
            if (qName.Equals(""))
                return;
            StoredQuery query = new StoredQuery() { ID = 0, Name = qName, Parms = "" };
            grdMain.DataContext = null;
            grdMain.DataContext = query;
            Array.Resize<StoredQueries>(ref StQueries, StQueries.Length + 1);
            StQueries[StQueries.Length - 1] = new StoredQueries() { ID = 0, Name = qName, Parameters = "" };
            lstQueries.ItemsSource = null;
            lstQueries.ItemsSource = StQueries;
            lstQueries.SelectedIndex = lstQueries.Items.Count - 1;
            adding = true;
            cmdTest.IsEnabled = false;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstQueries.SelectedItem == null)
            {
                MessageBoxCustom.Show("You have not selected a query for deletion.");
                return;
            }
            StoredQueries query = (StoredQueries)lstQueries.SelectedItem;
            if (MessageBoxCustom.Show("Delete query <" + query.Name + "> ?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                query.delete(query.ID.ToString());
                StQueries = StoredQueries.load();
                lstQueries.ItemsSource = null;
                if (StQueries == null || StQueries.Length == 0)
                {
                    MessageBoxCustom.Show("Could not find Queries: ", "", MessageBoxButton.OK, this);
                    return;
                }
                lstQueries.ItemsSource = StQueries;
                Clear();
                cmdTest.IsEnabled = false;
            }
        }

        private void Clear()
        {
            txtQuery.Text = "";
            txtParm1.Text = "";
            txtParm2.Text = "";
            txtParm3.Text = "";
            txtParm4.Text = "";
            txtParm5.Text = "";
            lstQueries.SelectedIndex = -1;
            cmdTest.IsEnabled = false;
        }

        private List<string> ValidateSQL(string query)
        {
            TSql100Parser parser = new TSql100Parser(true);
            TSqlFragment fragment;
            IList<ParseError> errors;
            fragment = parser.Parse(new StringReader(query), out errors);
            if (errors != null && errors.Count > 0)
            {
                List<string> errorList = new List<string>();
                foreach (var error in errors)
                {
                    errorList.Add(error.Message);
                }
                return errorList;
            }

            return null;
        }

        private void cmdValidate_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = ValidateSQL(txtQuery.Text);
            if (errors != null && errors.Count > 0)
            {
                string msg = "ERROR(S) Found in query\r\n\n";
                for (int i = 0; i < errors.Count; i++)
                {
                    msg += (i+1).ToString("##") + " : " + errors[i] + "\r\n";
                }
                MessageBoxCustom.Show(msg, "", MessageBoxButton.OK, this);
            }
            else
                MessageBoxCustom.Show("Query is VALID", "", MessageBoxButton.OK, this);

        }

        private void lstQueries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox list = (ComboBox)sender;
            if (list == null || list.SelectedIndex < 0)
                return;
            StoredQueries query = (StoredQueries)list.SelectedItem;
            currentQuery = query;
            StoredQuery sql = new StoredQuery() { ID = query.ID, Name = query.Name, Parms = query.Parameters, Query = query.Query };
            grdMain.DataContext = sql;
            adding = false;
            cmdTest.IsEnabled = true;
        }

        private void cmdTest_Click(object sender, RoutedEventArgs e)
        {
            callingForm.CalledFromEditor = true;
            callingForm.TestQuery(currentQuery);
        }

        private void cmdBuilder_Click(object sender, RoutedEventArgs e)
        {
            frmQueryBuilder frm = new frmQueryBuilder();
            frm.ShowDialog();
            lstQueries.ItemsSource = null;
            StQueries = StoredQueries.load();
            lstQueries.ItemsSource = StQueries;
        }

        private void cmdHelp_Click(object sender, RoutedEventArgs e)
        {
            Help.frmHelp frm = new Help.frmHelp();
            frm.PageName = "Help/QueryEditor.xaml";
            frm.Show();
        }
    }

    public class StoredQuery
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Parm1 { get; set; } = "";
        public string Parm2 { get; set; } = "";
        public string Parm3 { get; set; } = "";
        public string Parm4 { get; set; } = "";
        public string Parm5 { get; set; } = "";
        public string Query { get; set; } = "";

        public string Parms
        {
            set
            {
                int c = 0;
                string[] s = value.Split(',');
                if (s != null)
                {
                    c = s.Length;
                    for (int i = 0; i < c; i++)
                    {
                        switch(i)
                        {
                            case 0:
                                Parm1 = s[i];
                                break;
                            case 1:
                                Parm2 = s[i];
                                break;
                            case 2:
                                Parm3 = s[i];
                                break;
                            case 3:
                                Parm4 = s[i];
                                break;
                            case 4:
                                Parm5 = s[i];
                                break;
                        }
                    }
                }
                for ( int i = c; i < 5; i++ )
                {
                    switch (c)
                    {
                        case 0:
                            Parm1 = "";
                            break;
                        case 1:
                            Parm2 = "";
                            break;
                        case 2:
                            Parm3 = "";
                            break;
                        case 3:
                            Parm4 = "";
                            break;
                        case 4:
                            Parm5 = "";
                            break;
                    }

                }
            }

            get
            {
                string s = "";
                if (!Parm1.Trim().Equals(""))
                    s = Parm1;
                if (!Parm2.Trim().Equals(""))
                    s += (!s.Trim().Equals("") ? "," + Parm2 : Parm2);
                if (!Parm3.Trim().Equals(""))
                    s += (!s.Trim().Equals("") ? "," + Parm3 : Parm3);
                if (!Parm4.Trim().Equals(""))
                    s += (!s.Trim().Equals("") ? "," + Parm4 : Parm4);
                if (!Parm5.Trim().Equals(""))
                    s += (!s.Trim().Equals("") ? "," + Parm5 : Parm5);

                return s;
            }
        }
    }
}
