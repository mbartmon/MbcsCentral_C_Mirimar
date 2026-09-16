using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmQueryBuilder.xaml
    /// </summary>
    public partial class frmQueryBuilder : Window
    {
        string columnDefs = "SELECT   ColumnName = COLUMN_NAME, DataType = DATA_TYPE, MaxLength = ISNULL(CHARACTER_MAXIMUM_LENGTH,0), " +
                            "IsNullable = IS_NULLABLE, ColumnDefault = ISNULL(COLUMN_DEFAULT,'') FROM INFORMATION_SCHEMA.COLUMNS " +
                            "WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '<TABLENAME>' ORDER BY ORDINAL_POSITION ";

        const int NUMBER_OF_FIELDS = 10;
        string CurrentTable;
        string CurrentColumn;
        Point startPoint;
        Point currentPoint;
        bool IsDragging = false;
        ListView lstFields;
        FieldDef[] fields1;
        FieldDef[] fields2;

        QueryProcessor.Where where = new QueryProcessor.Where();
        string key = "";

        public frmQueryBuilder()
        {
            InitializeComponent();
        }

        private void frmQueryBuilder_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            List<string> list = new List<string>();
            string sql = "SELECT TABLE_NAME FROM " + GlobalShared.InitialCatalog + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME LIKE 'gl_%' OR TABLE_NAME LIKE 'pm_%' ORDER BY TABLE_NAME";
            DataSet ds = clsDB.Query(sql, GlobalShared.CN);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    list.Add((string)dr["TABLE_NAME"]);
                lstTables.ItemsSource = list;

            }

            for (int i = 0; i < NUMBER_OF_FIELDS; i++)
            {

            }
            bdrComplexIndex.Visibility = Visibility.Collapsed;
        }

        private void frmQueryBuilder_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            string newQuery = null;
            string result = "";
            MessageBoxCustom.Show("Give this query a name", out result, 50);
            if (result == null || result.Trim().Equals(""))
            {
                MessageBoxCustom.Show("You have not given the query a name.  Will not be saved.", "", MessageBoxButton.OK, this);
                return;
            }
            StoredQueries q = StoredQueries.load(result);
            if (q != null)
            {
                MessageBoxResult r = MessageBoxCustom.Show("Query with that name already exits.\r\n\nOverwrite that query?", "", MessageBoxButton.YesNo, this);
                if (r == MessageBoxResult.No)
                    return;
            }
            else
                q = new StoredQueries() { Name = result };

            string parameters = "";
            newQuery = BuildQuery(out parameters);
            if (newQuery.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Cannot save empty query.", "", MessageBoxButton.OK, this);
            }
            q.Parameters = parameters;
            q.Query = newQuery;

            if (!q.save())
            {
                MessageBoxCustom.Show("Query could not be saved.", "", MessageBoxButton.OK, this);
            }
            else
                MessageBoxCustom.Show("Query successfully saved.", "", MessageBoxButton.OK, this);
                
        }

        private string BuildQuery(out string parameters)
        {
            string table1 = txtFields1.Text;
            string table2 = txtFields2.Text;
            bool anyFields = false;
            if (table1.Trim().Equals("") && table2.Trim().Equals(""))
            {
                MessageBoxCustom.Show("No table(s) have been selected.", "", MessageBoxButton.OK, this);
                parameters = "";
                return "";
            }
            string query = "SELECT ";
            parameters = "";

          bool containsPrompts = false;

            foreach (FieldDef d in Fields1.Items)
            {
                if (d.Show)
                {
                    query += "[" + table1.Trim() + "].[" + d.Name + "]";
                    if (!d.Alias.Trim().Equals(""))
                        query += " " + d.Alias;
                    query += ", ";
                    anyFields = true;
                }
            }
            if (!anyFields)
            {
                MessageBoxCustom.Show("No fields have been chosen in table: " + table1, "", MessageBoxButton.OK, this);
                parameters = "";
                return "";
            }
            if (!table2.Trim().Equals(""))
            {
                foreach (FieldDef d in Fields2.Items)
                {
                    if (d.Show)
                    {
                        query += "[" + table2.Trim() + "].[" + d.Name + "]";
                        if (!d.Alias.Trim().Equals(""))
                            query += " " + d.Alias;
                        query += ", ";
                    }
                }
            }
            if (query.EndsWith(", "))
                query = query.Substring(0, query.LastIndexOf(','));

            query += " FROM " + table1 + (!table2.Trim().Equals("") ? ", " + table2 : "");

            if (!IndexField.Text.Trim().Equals(""))
            {
                query += " WHERE";
                string[] indexes = IndexField.Text.Split('/');
                query += " [" + table1 + "]." + indexes[0] + " = [" + table2 + "]." + (indexes.Length == 1 ? IndexField.Text : indexes[1]);
            }
            else if (table2.Trim().Equals(")"))
            {
                if (MessageBoxCustom.Show("There is no index field.  Cannot include " + table2 + " in query.\r\n\nSave with only " + table1 + "?","", MessageBoxButton.YesNo,this) == MessageBoxResult.No)
                {
                    parameters = "";
                    return "";
                }
            }

            if (ContainsPrompts(Fields1))
            {
                string t = "";
                string p = "";
                if (!query.Contains("WHERE"))
                    query += " WHERE ";
                foreach (FieldDef d in Fields1.Items)
                {
                    if (d.Prompt)
                    {
                        t = ((string)d.ColumnDef.ItemArray[1]).ToLower();
                        if (!query.EndsWith("WHERE "))
                            query += " AND ";
                        query += "[" + table1 + "].[" + d.Name + "] " + (t.Contains("date") || d.Range ? " " : "=");
                        p = d.Name.Replace("[", "").Replace("]", "").Replace(" ", "");
                        if (d.Range)
                        {
                            string s;
                            if (d.IsDate)
                            {
                                s = " BETWEEN @From" + p + " AND @To" + p ;
                            }
                            else
                            {
                                s = " BETWEEN @From" + p + " AND @To" + p;

                            }
                            query += s;
                            parameters += "From" + p + "," + "To" + p + ",";
                        }
                        else
                        {
                            query += "@" + p;
                            parameters += p + ",";
                        }
                    }

                }
                if (query.EndsWith("WHERE "))
                    query = query.Substring(0, query.Length - 6);
                if (query.EndsWith("AND "))
                    query = query.Substring(0, query.Length - 4);
                if (parameters.EndsWith(","))
                    parameters = parameters.Substring(0, parameters.Length - 1);
            }

            if(ContainsSorts(Fields1))
            {
                query += " ORDER BY ";
                foreach (FieldDef d in Fields1.Items)
                {
                    if (d.Sort)
                    {
                        query += "[" + table1 + "].[" + d.Name + "], ";
                    }

                }
                if (query.EndsWith(", "))
                    query = query.Substring(0, query.Length - 2);

            }

            return query;
        }

        private bool ContainsPrompts(ListView list)
        {
            FieldDef d = null;
            for(int i = 0; i < list.Items.Count; i++)
            {
                d = (FieldDef)list.Items[i];
                if (d.Prompt)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsSorts(ListView list)
        {
            FieldDef d = null;
            for (int i = 0; i < list.Items.Count; i++)
            {
                d = (FieldDef)list.Items[i];
                if (d.Sort)
                {
                    return true;
                }
            }
            return false;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdTables_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void lstTables_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            Point MousePos = e.GetPosition(null);
            Vector diff = startPoint - MousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListBox list = (ListBox)sender;
                ListBoxItem item = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                if (item == null)
                    return;
                fields = new FieldDef();
                DataSet ds = clsDB.Query(columnDefs.Replace("<TABLENAME>", (string)item.Content), GlobalShared.CN);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return;
                fields.ColumnCollection = ds.Tables[0].Rows;
                DataObject dragData = new DataObject("myFormat", fields);
                DragDrop.DoDragDrop(item, dragData, DragDropEffects.All);
            }
            */
        }

        private void lstTables_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void pnlTest_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                //e.Effects = DragDropEffects.None;
            }
        }
        private void pnlTest_Drop(object sender, DragEventArgs e)
        {
            //Point pt = e.GetPosition(pnlTest);
            /*
            if (CurrentField.Item.Index < 0)
                CurrentField.Item.Index = FieldCount++;
            CurrentField.Item.Top = (Decimal)pt.Y;
            CurrentField.Item.Left = (Decimal)pt.X;
            pnlTest.Children.Add(CurrentField);
            Canvas.SetLeft(CurrentField, pt.X);
            Canvas.SetTop(CurrentField, pt.Y);
                        */

        }


        private void grdTables_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                //e.Effects = DragDropEffects.None;
            }

        }
        private void grdTables_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                //DbTableFields fields = e.Data.GetData("myFormat") as DbTableFields;
                Canvas panel = sender as Canvas;
                //panel.Children.Add(fields);
            }

        }

        private void lstTables_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox list = (ListBox)sender;
            ListBoxItem item = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

            DataSet ds = clsDB.Query(columnDefs.Replace("<TABLENAME>", (string)item.Content), GlobalShared.CN);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;
            if (txtFields1.Text.Trim().Equals(""))
            {
                fields1 = SetColumns(ds.Tables[0].Rows);
                txtFields1.Text = (string)item.Content;
                Fields1.ItemsSource = fields1;
                FillIndexes(fields1, 1);
            }
            else if (txtFields2.Text.Trim().Equals(""))
            {
                fields2 = SetColumns(ds.Tables[0].Rows);
                txtFields2.Text = (string)item.Content;
                Fields2.ItemsSource = fields2;
                FillIndexes(fields2, 2);
            }
            else
            {
                MessageBoxCustom.Show("Both Tables are in use.  Remove one first to replace it.","" ,MessageBoxButton.OK,this);
            }

            if (!txtFields1.Text.Trim().Equals("") && !txtFields2.Text.Trim().Equals(""))
            {
                string s = txtFields1.Text.Trim() + "/" + txtFields2.Text.Trim();
                key = where.GetKey(s);
                IndexField.Text = key;
                cmdSave.IsEnabled = true;
            }
        }

        private void FillIndexes(FieldDef[] fields, int column = 1)
        {
            FieldDef[] flds = new FieldDef[fields.Length + 1];
            flds[0] = new FieldDef() { Name = " " };
            for (int i = 0;  i < fields.Length; i++)
                flds[i + 1] = fields[i];
            switch(column)
            {
                case 1:
                    lstIndex1a.ItemsSource = flds;
                    lstIndex2a.ItemsSource = flds;
                    lstIndex3a.ItemsSource = flds;
                    break;
                case 2:
                    lstIndex1b.ItemsSource = flds;
                    lstIndex2b.ItemsSource = flds;
                    lstIndex3b.ItemsSource = flds;
                    break;
            }
        }

        public FieldDef[] SetColumns(DataRowCollection rows)
        {
            if (rows != null)
            {
                ArrayList columns = new ArrayList();
                bool d = false;
                foreach (DataRow column in rows)
                {
                    d = ((string)column.ItemArray[1]).ToLower().Contains("date");
                    columns.Add(new FieldDef() { Name = (string)column["ColumnName"], IsDate = d, ColumnDef = column });
                }
                return ((FieldDef[])columns.ToArray(typeof(FieldDef)));

            }
            return null;
        }

        private void lstFields_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is TextBlock)
            {
                try
                {
                    CurrentField = null;
                    CurrentField = (FieldDef)GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstFields);
                    FieldDef field = (FieldDef)lstFields.SelectedItem;
                    if (CurrentField != null)
                    {
                        ListBoxItem item = (ListBoxItem)GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstFields);
                        //CurrentField = new Field(ctxMenu) { Content = field.FieldName, fontInfo = CurrentDefaultFont };
                        //CurrentField.Item = new DocumentLayoutFields() { FieldId = field.ID, DocumentId = doc.ID, Size = field.Size };
                        //CurrentField.PreviewKeyDown += pnlDocument_KeyDown;
                        startPoint = e.GetPosition(null);
                    }
                }
                catch { }
            }
        }

        private void ListFields_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ListBox && e.OriginalSource is TextBlock)
            {
                CurrentColumn = ((TextBlock)e.OriginalSource).Text;
                startPoint = e.GetPosition(null);
                CurrentField = (FieldDef)((ListBox)e.Source).SelectedItem;
                CurrentTable = CurrentField.TableName;
            }

        }

        private void ListFields_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentField == null) return;
            Point position = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging && e.OriginalSource is TextBlock)
            {

                if (Math.Abs(position.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e, sender);
                }
            }

        }

        public static object GetListBoxItemClicked(FrameworkElement sender, ListBox list, double? x = null, double? y = null)
        {
            Point p = new Point();
            if (x == null)
                p = ((sender as FrameworkElement)).TranslatePoint(new Point(0, 0), list);
            else
            {
                p.X = (double)x;
                p.Y = (double)y;
            }
            DependencyObject obj = list.InputHitTest(p) as DependencyObject;
            while (obj != null && !(obj is ListBoxItem))
                obj = VisualTreeHelper.GetParent(obj);
            return obj;
        }

       /* private void pnlTest_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Source is DbTableFields && e.OriginalSource is TextBlock)
            {
                CurrentTable = ((DbTableFields)e.Source).TableName;
                CurrentColumn = ((TextBlock)e.OriginalSource).Text;
                startPoint = e.GetPosition(null);
                CurrentField = (FieldDef)((DbTableFields)e.Source).FieldList.SelectedItem;
            }
        }
        */

        private void pnlTest_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging && e.OriginalSource is TextBlock)
            {

                if (Math.Abs(position.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e, sender);
                }
            }

        }

        private void StartDrag(MouseEventArgs e, object sender)
        {
            if (CurrentField != null)
            {
                IsDragging = true;
                DataObject data = new DataObject(CurrentField);
                IsDragging = true;
                DragDropEffects de = DragDrop.DoDragDrop(lstFields, data, DragDropEffects.All);
                IsDragging = false;
            }
        }


        private void pnlTest_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key;
            if (CurrentField != null)
                key = e.Key;
        }

        private void pnlTest_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*
            currentPoint = e.MouseDevice.GetPosition(pnlTest);
            object obj = GetCanvasBoxItemClicked((FrameworkElement)sender, pnlTest, currentPoint.X, currentPoint.Y);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
            }
            */

        }

        public static object GetCanvasBoxItemClicked(FrameworkElement sender, Canvas pnl, double? x = null, double? y = null)
        {
            Point p = new Point();
            if (x == null)
                p = ((sender as FrameworkElement)).TranslatePoint(new Point(0, 0), pnl);
            else
            {
                p.X = (double)x;
                p.Y = (double)y;
            }
            DependencyObject obj = pnl.InputHitTest(p) as DependencyObject;
            while (obj != null && !(obj is Field))
                obj = VisualTreeHelper.GetParent(obj);
            return obj;
        }

        public FieldDef CurrentField { get; set; }

        private void CtxFields1_Click(object sender, RoutedEventArgs e)
        {
            txtFields1.Text = "";
            IndexField.Text = "";
            Fields1.ItemsSource = null;
            lstIndex1a.ItemsSource = null;
            lstIndex2a.ItemsSource = null;
            lstIndex3a.ItemsSource = null;
            //cmdSave.IsEnabled = false;
        }

        private void CtxFields2_Click(object sender, RoutedEventArgs e)
        {
            txtFields2.Text = "";
            IndexField.Text = "";
            Fields2.ItemsSource = null;
            lstIndex1b.ItemsSource = null;
            lstIndex2b.ItemsSource = null;
            lstIndex3b.ItemsSource = null;
            //cmdSave.IsEnabled = false;
        }

        private void ChkFields1All_Checked(object sender, RoutedEventArgs e)
        {
            bool set = chkFields1All.IsChecked.Value;
            foreach (FieldDef d in fields1)
                d.Show = set;
            Fields1.ItemsSource = null;
            Fields1.ItemsSource = fields1;
        }

        private void ChkFields2All_Checked(object sender, RoutedEventArgs e)
        {
            bool set = chkFields2All.IsChecked.Value;
            foreach (FieldDef d in fields2)
                d.Show = set;
            Fields2.ItemsSource = null;
            Fields2.ItemsSource = fields2;
        }

        private void chkComplexIndex_Checked(object sender, RoutedEventArgs e)
        {
            if (chkComplexIndex.IsChecked.Value)
            {
                bdrComplexIndex.Visibility = Visibility.Visible;
                bdrSimpleIndex.Visibility = Visibility.Collapsed;
            }    
            else
            {
                bdrComplexIndex.Visibility = Visibility.Collapsed;
                bdrSimpleIndex.Visibility = Visibility.Visible;
            }
        }
    }
    public class FieldDef
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public bool Sort { get; set; }
        public bool Show { get; set; } = true;
        public bool Prompt { get; set; }
        public bool Range { get; set; }
        public string Alias { get; set; } = "";
        public string Criteria { get; set; } = "";
        public string Or { get; set; } = "";
        public Boolean IsDate { get; set; } = false;
        public DataRow ColumnDef { get; set; }
    }

}
