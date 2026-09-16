using System;
using System.Collections.Generic;
using System.Data;
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
using System.Collections;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmProcedures.xaml
    /// </summary>
    public partial class frmProcedures : Window
    {
        public bool CalledFromEditor { get; set; }
        frmProcedureEdit frmEdit = null;
        string[] parms;
        StoredQueries Proc;
        ArrayList procs = new ArrayList();
        //ArrayList parms = new ArrayList();
        string ProcName = "";
        DataTable dt = null;
        DatePicker dp1, dp2, dp3, dp4, dp5;
        
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmProcedures()
        {
            InitializeComponent();
        }
    public frmProcedures(string parm)
    {
        InitializeComponent();
        m_ThisApp = (string) parm;
    }
        private void FormProcedures_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            GetQueries();

            /*
            SqlDataAdapter da = new SqlDataAdapter("SELECT [Routine_Name] FROM [INFORMATION_SCHEMA].[ROUTINES]  ORDER BY [Routine_Name]", GlobalShared.SqlConnectionString);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Procedures");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Could not find Queries: " + ex.Message);
                return;
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (((string)dr[0]).Contains("mbcs_"))
                    procs.Add((string)dr[0]);
            }      
            ds = null;
            da = null;
            */

        }

        private void GetQueries()
        {
            lstProcedure.ItemsSource = null;
            StoredQueries[] queries = StoredQueries.load();
            if (queries == null || queries.Length == 0)
            {
                MessageBoxCustom.Show("Could not find Queries: ", "", MessageBoxButton.OK, this);
                return;
            }
            procs.Clear();
            foreach (StoredQueries query in queries)
            {
                procs.Add(query);
            }
            lstProcedure.ItemsSource = procs;

        }
        /*
        private void cmdRunOld_Click(object sender, RoutedEventArgs e)
        {
          
            parms.Clear();
            if (Parm1.IsEnabled && txtParm1.Text.Trim() != "")
            {
                parms.Add(new Parameters() {Name = txtParm1.Text, Value = Parm1.Text});
            }
            if (Parm2.IsEnabled && txtParm2.Text.Trim() != "")
            {
                parms.Add(new Parameters() {Name = txtParm2.Text, Value = Parm2.Text});
            }
            if (Parm3.IsEnabled && txtParm3.Text.Trim() != "")
            {
                parms.Add(new Parameters() {Name = txtParm3.Text, Value = Parm3.Text});
            }
            if (Parm4.IsEnabled && txtParm4.Text.Trim() != "")
            {
                parms.Add(new Parameters() {Name = txtParm4.Text, Value = Parm4.Text});
            }
            if (Parm5.IsEnabled && txtParm5.Text.Trim() != "")
            {
                parms.Add(new Parameters() {Name = txtParm5.Text, Value = Parm5.Text});
            }

            
            
            try
            {

                SqlConnection conn = new SqlConnection(GlobalShared.SqlConnectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                dt = new DataTable();
                
                SqlCommand cmd = new SqlCommand(ProcName, conn);
                foreach (Parameters parm in parms)
                {
                    cmd.Parameters.Add(new SqlParameter(parm.Name, parm.Value));
                }
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                grdResults.ItemsSource = dt.AsDataView();
                cmdPrint.IsEnabled = true;
                cmdExport.IsEnabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error running query: " + ex.Message);
                return;
            }
           
     }
     */

        private void cmdRun_Click(object sender, RoutedEventArgs e)
        {
            string query = Proc.Query;
            int idxS = query.IndexOf("EXEC");
            if (idxS >= 0)
            {
                int idxL = query.IndexOf(';', idxS);
                if (idxL > 0)
                {
                    int length = idxL - idxS;
                    string[] s = query.Substring(idxS, length).Split(' ');
                    if (s.Length == 2)
                    {
                        StoredQueries subQuery = StoredQueries.load(s[1]);
                        if (subQuery != null)
                        {
                            query = query.Substring(0, idxS) + " " + subQuery.Query + query.Substring(idxL);
                        }
                    }
                }
            }
            //query = query.Replace("= @", "=@");
            if (parms != null && parms.Length > 0)
            {
                string t = "";
                if (Parm1.IsEnabled && !txtParm1.Text.Trim().Equals(""))
                {
                    if (txtParm1.Text.ToLower().Contains("date"))
                        t = TransformDate(txtParm1.Text, dp1.SelectedDate.ToString());
                    else
                        t = TransformDate(txtParm1.Text, Parm1.Text);

                    query = query.Replace("@" + txtParm1.Text, t);
                }
                if (Parm2.IsEnabled && !txtParm2.Text.Trim().Equals(""))
                {
                    if (txtParm2.Text.ToLower().Contains("date"))
                        t = TransformDate(txtParm2.Text, dp2.SelectedDate.ToString());
                    else
                        t = TransformDate(txtParm2.Text, Parm2.Text);

                    query = query.Replace("@" + txtParm2.Text, t);

                }
                if (Parm3.IsEnabled && !txtParm3.Text.Trim().Equals(""))
                {
                    if (txtParm3.Text.ToLower().Contains("date"))
                        t = TransformDate(txtParm3.Text, dp3.SelectedDate.ToString());
                    else
                        t = TransformDate(txtParm3.Text, Parm3.Text);

                    query = query.Replace("@" + txtParm3.Text, t);

                }
                if (Parm4.IsEnabled && !txtParm4.Text.Trim().Equals(""))
                {
                    if (txtParm4.Text.ToLower().Contains("date"))
                        t = TransformDate(txtParm4.Text, dp4.SelectedDate.ToString());
                    else
                        t = TransformDate(txtParm4.Text, Parm4.Text);

                    query = query.Replace("@" + txtParm4.Text, t);

                }
                if (Parm5.IsEnabled && !txtParm5.Text.Trim().Equals(""))
                {
                    if (txtParm5.Text.ToLower().Contains("date"))
                        t = TransformDate(txtParm5.Text, dp5.SelectedDate.ToString());
                    else
                        t = TransformDate(txtParm5.Text, Parm5.Text);

                    query = query.Replace("@" + txtParm5.Text, t);

                }
            }

            try
            {
                SqlConnection conn = new SqlConnection(GlobalShared.SqlConnectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                dt = new DataTable();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(dt);
                grdResults.ItemsSource = dt.AsDataView();
                cmdPrint.IsEnabled = true;
                cmdExport.IsEnabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error running query: " + ex.Message);
                return;
            }

        }
    
        private string TransformDate(string p, string t)
        {

            if (p.ToLower().Contains("date"))
            {
                string s = "";
                if (p.ToLower().Contains("from"))
                {
                    try
                    {
                        DateTime d = DateTime.Parse(t);
                        t = "'" + d.ToShortDateString() + " 00:00:00" + "'";
                    }
                    catch (FormatException)
                    {
                        return t;
                    }
                }
                else if (p.ToLower().Contains("to"))
                {
                    try
                    {
                        DateTime d = DateTime.Parse(t);
                        t = "'" + d.ToShortDateString() + " 23:59:59" + "'";
                    }
                    catch (FormatException)
                    {
                        return t;
                    }
                }
                else
                {
                    try
                    {
                        DateTime d = DateTime.Parse(t);
                        t = "BETWEEN '" + d.Date.ToShortDateString() + " 00:00:00' AND '" + d.Date.ToShortDateString() + " 23:59:59'";
                    }
                    catch (FormatException)
                    {
                        return t;
                    }
                }
            }              

            return t;
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            if (CalledFromEditor && frmEdit != null)
            {
                CalledFromEditor = false;
                frmEdit.Focus();
                return;
            }
            this.Close();
        }

        private void ClearParms()
        {
            txtParm1.Text = "";
            txtParm2.Text = "";
            txtParm3.Text = "";
            txtParm4.Text = "";
            txtParm5.Text = "";

            Parm1.Text = "";
            Parm1.IsEnabled = false;
            Parm2.Text = "";
            Parm2.IsEnabled = false;
            Parm3.Text = "";
            Parm3.IsEnabled = false;
            Parm4.Text = "";
            Parm4.IsEnabled = false;
            Parm5.Text = "";
            Parm5.IsEnabled = false;
        }

        private void lstProcedure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmdPrint.IsEnabled = false;
            cmdExport.IsEnabled = false;
            if (lstProcedure.SelectedIndex >= 0)
            {
                ClearParms();
                Proc = (StoredQueries)lstProcedure.SelectedItem;
                ProcName = Proc.Name;
                //SqlConnection conn = new SqlConnection(GlobalShared.SqlConnectionString);
                //SqlCommand cmd = new SqlCommand(ProcName, conn);
                //cmd.CommandType = CommandType.StoredProcedure;

                //conn.Open();
                //SqlCommandBuilder.DeriveParameters(cmd);
                //conn.Close();

                parms = Proc.Parameters.Split(',');
                //int i = 0;
                //foreach (SqlParameter p in cmd.Parameters)

                try
                {
                    grdParms.Children.Remove(dp1);
                    dp1 = null;
                }
                catch { }
                try
                {
                    grdParms.Children.Remove(dp2);
                    dp2 = null;
                }
                catch { }
                try
                {
                    grdParms.Children.Remove(dp3);
                    dp3 = null;
                }
                catch { }
                try
                {
                    grdParms.Children.Remove(dp4);
                    dp4 = null;
                }
                catch { }
                try
                {
                    grdParms.Children.Remove(dp5);
                    dp5 = null;
                }
                catch { }
                for (int i = 0; i < parms.Length; i++)
                {
                    /*
                    if (p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput)
                        switch (i)
                        {
                            case 0:
                                txtParm1.Text = p.ParameterName;
                                Parm1.IsEnabled = true;
                                i++;
                                break;
                            case 1:
                                txtParm2.Text = p.ParameterName;
                                Parm2.IsEnabled = true;
                                i++;
                                break;
                            case 2:
                                txtParm3.Text = p.ParameterName;
                                Parm3.IsEnabled = true;
                                i++;
                                break;
                            case 3:
                                txtParm4.Text = p.ParameterName;
                                Parm4.IsEnabled = true;
                                i++;
                                break;
                           case 4:
                                txtParm5.Text = p.ParameterName;
                                Parm5.IsEnabled = true;
                                i++;
                                break;
                        }
                        */

                    switch (i)
                    {
                        case 0:
                            txtParm1.Text = parms[i];
                            Parm1.IsEnabled = true;
                            if (txtParm1.Text.ToLower().Contains("date") )
                            {
                                dp1 = new DatePicker() { Name="dp1"};
                                grdParms.Children.Add(dp1);
                                Grid.SetColumn(dp1, 1);
                                Grid.SetRow(dp1, 0);
                            }
                            break;
                        case 1:
                            txtParm2.Text = parms[i];
                            Parm2.IsEnabled = true;
                            if (txtParm2.Text.ToLower().Contains("date") && (txtParm2.Text.ToLower().Contains("from") || txtParm2.Text.ToLower().Contains("to")))
                            {
                                dp2 = new DatePicker() { Name="dp2"};
                                grdParms.Children.Add(dp2);
                                Grid.SetColumn(dp2, 1);
                                Grid.SetRow(dp2, 1);
                            }
                            break;
                        case 2:
                            txtParm3.Text = parms[i];
                            Parm3.IsEnabled = true;
                            if (txtParm3.Text.ToLower().Contains("date") && (txtParm3.Text.ToLower().Contains("from") || txtParm3.Text.ToLower().Contains("to")))
                            {
                                dp3 = new DatePicker() { Name="dp3"};
                                grdParms.Children.Add(dp3);
                                Grid.SetColumn(dp3, 1);
                                Grid.SetRow(dp3, 2);
                            }
                            break;
                        case 3:
                            txtParm4.Text = parms[i];
                            Parm4.IsEnabled = true;
                            if (txtParm4.Text.ToLower().Contains("date") && (txtParm4.Text.ToLower().Contains("from") || txtParm4.Text.ToLower().Contains("to")))
                            {
                                dp4 = new DatePicker() { Name="dp4"};
                                grdParms.Children.Add(dp4);
                                Grid.SetColumn(dp4, 1);
                                Grid.SetRow(dp4, 3);
                            }
                            break;
                        case 4:
                            txtParm5.Text = parms[i];
                            Parm5.IsEnabled = true;
                            if (txtParm5.Text.ToLower().Contains("date") && (txtParm5.Text.ToLower().Contains("from") || txtParm5.Text.ToLower().Contains("to")))
                            {
                                dp5 = new DatePicker() { Name="dp5"};
                                grdParms.Children.Add(dp5);
                                Grid.SetColumn(dp5, 1);
                                Grid.SetRow(dp5, 4);
                            }
                            break;
                    }
                }
                /*
                SqlDataAdapter da = new SqlDataAdapter("select * from information_schema.parameters where specific_name='" + proc + "'", GlobalShared.SqlConnectionString);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "Procedure");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Unable to retrieve query parameters: " + ex.Message);
                    return;
                }
                foreach    */
            }

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            object[] items;
            string s;
            int i = 0;
            int pageWidth = 0;
            DataGridLength l;
            ArrayList headers = new ArrayList();
            ArrayList rows = new ArrayList();
            object[,] headerLines = new object[4, 2];
            float[] rowWidths = new float[grdResults.Columns.Count];
            
            System.Drawing.Font header1Font = new System.Drawing.Font("Courier New", 14);

            foreach (DataGridColumn column in grdResults.Columns)
            {
                headers.Add(column.Header.ToString());
                rowWidths[i++] = column.Header.ToString().Length;
            }
            foreach (DataRow row in dt.Rows)
            {
                items = row.ItemArray;
                rows.Add(items);
                for (i = 0; i < items.Length; i++)
                {
                    if (items[i].ToString().Length > rowWidths[i])
                        rowWidths[i] = items[i].ToString().Length;
                }
            }
            headerLines[0, 0] = ProcName.Substring(5) + "   " + Report.DATE_TAG + "   page " + Report.PAGE_TAG ;
            headerLines[0, 1] = header1Font;
            i = 0;
            for (i = 0; i < headers.Count; i++)
            {
                headerLines[2, 0] += ((string)headers[i]).PadRight((int)rowWidths[i] + 1);
                pageWidth += (int)rowWidths[i];
            }
            Report rpt = new Report((string)headerLines[0,0],"Courier New",14);
            
            if (pageWidth > 85)
                rpt.FontSize = 8;
            else
                rpt.FontSize = 12;
            if (pageWidth > 128)
                rpt.PageOrientation = 1;
            System.Drawing.Font header2Font = new System.Drawing.Font("Courier New", rpt.FontSize, System.Drawing.FontStyle.Underline);
            headerLines[2, 1] = header2Font;
            rpt.LeftMargin = 10;
            rpt.TopMargin = 10;
            rpt.HeaderLines = headerLines;
            rpt.PrintHeader();
            string line = "";
            int j;
            for (i = 0; i < rows.Count; i++)
            {
                items = (object[])rows[i];
                for (j = 0; j < headers.Count; j++)
                    line += ((string)items[j].ToString()).PadRight((int)rowWidths[j] + 1);
                if(rpt.PrintPageLine(line) && i < rows.Count - 2)
                {
                    rpt.NewPage();
                    rpt.PrintHeader();
                }
                line = "";
            }
            rpt.DisplayDocument();
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Cursor saveCursor = this.Cursor;
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "Comma Separated Value File (*.csv)| *.csv";
            dlg.DefaultExt = ".csv";
            dlg.OverwritePrompt = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = System.Windows.Input.Cursors.Wait;
                string filepath = dlg.FileName;
                StreamWriter sw;
                try
                {
                    sw = new StreamWriter(filepath, false);

                }
                catch (IOException ex)
                {
                    MessageBox.Show("Unable to open file: " + ex.Message);
                    this.Cursor = saveCursor;
                    return;
                }
                bool first = true;
                ArrayList list = new ArrayList();
                foreach (DataGridColumn col in grdResults.Columns)
                {
                    string name = col.SortMemberPath;
                    if (!name.Trim().Equals(""))
                    {
                        sw.Write((first ? "" : ",") + col.Header + "");
                        list.Add(name);
                        first = false;
                    }
                }
                sw.WriteLine();
                int i;
                foreach (DataRow row in dt.Rows)
                {
                    first = true;
                    decimal number;
                    int amount;
                    bool usequote = false;
                    string value = "";
                    string dataType = "";
                    for (i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            dataType = dt.Columns[i].DataType.Name;
                            if (dataType.Equals("String"))
                            {
                                value = row[i].ToString();
                                usequote = true;
                            }
                            else if (dataType == "DateTime")
                            {
                                var obj = row[i] as DateTime?;
                                if (obj != null)
                                {
                                    value = ((DateTime)obj).ToShortDateString();
                                    usequote = false;
                                }
                            }
                            else if (dataType.ToLower().Contains("int"))
                            {
                                int.TryParse(row[i].ToString(), out amount);
                                value = amount.ToString();

                            }
                            else
                            {
                                if (decimal.TryParse(row[i].ToString(), out number))
                                {
                                    value = number.ToString("#######0.00");
                                    usequote = false;
                                }
                                else
                                {
                                    value = row[i].ToString();
                                    usequote = true;
                                }
                            }
                            if (usequote)
                                sw.Write((first ? "\"" : ",\"") + value + "\"");
                            else
                                sw.Write((first ? "" : ",") + value);
                        }
                        catch (Exception ex) { sw.Write(","); }
                        first = false;
                    }
                    sw.WriteLine();
                }
                sw.Close();

            }
            this.Cursor = saveCursor;
        }


        private void cmdBuilder_Click(object sender, RoutedEventArgs e)
        {
            frmQueryBuilder frm = new frmQueryBuilder();
            frm.ShowDialog();
            GetQueries();

        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            frmEdit = new frmProcedureEdit(this);
            frmEdit.ShowDialog();
            GetQueries();
        }

        private void FormProcedures_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);

        }


        public void TestQuery(StoredQueries query)
        {
            StoredQueries[] queries = StoredQueries.load();
            if (queries == null || queries.Length == 0)
            {
                MessageBoxCustom.Show("Could not find Queries: ", "", MessageBoxButton.OK, this);
                return;
            }
            int index = -1;
            foreach (StoredQueries q in queries)
            {
                procs.Add(q);
                if (q.Name.Equals(query.Name))
                    index = procs.Count - 1;
            }
            lstProcedure.ItemsSource = procs;
            lstProcedure.SelectedIndex = index;
            this.Focus();
            if (query.Parameters == null || query.Parameters.Trim().Equals(""))
                cmdRun_Click(null, new RoutedEventArgs());
        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetQueries();
        }
    }

    public class Parameters
    {
        public string Name {get; set;}
        public string Value {get; set;}
        public Parameters()
        {}
    }
}
