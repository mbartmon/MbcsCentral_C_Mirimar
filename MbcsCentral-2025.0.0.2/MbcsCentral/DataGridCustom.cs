using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MbcsCentral
{
    class DataGridCustom
    {
        ArrayList columns = new ArrayList();
        Border bdr = null;
        Binding bind = null;
        int column = 0;
        int row = 0;

        public static DataGridCustom GetDataGridCustom( int columns, int rows, Grid grid, bool usescrollView = false)
        {
            if (columns == 0 || rows == 0)
            {
                MessageBoxCustom.Show("No columns and/or rows specified.");
                return null;
            }
            if (grid == null)
            {
                MessageBoxCustom.Show("There is no grid definition.");
                return null;
            }
            return new DataGridCustom(columns, rows, grid, usescrollView);

        }
        private DataGridCustom(int columns, int rows, Grid grid, bool usescrollView)
        {
            Columns = columns;
            Rows = rows;
            grd = grid;
            UseScrollView = usescrollView;

        }

        public void Initialize()
        {
            RowDefinition Rdef;
            for (int i = 0; i < Rows + 1; i++)
            {
                Rdef = new RowDefinition() { Height = new GridLength(25) };
                grd.RowDefinitions.Add(Rdef);
            }
            int columnNumber = 0;
            ColumnDefinition Cdef;
            TextBlock txt;
            foreach (GridColumns col in columns)
            {
                Cdef = new ColumnDefinition() { Width = new GridLength(col.Width) };
                grd.ColumnDefinitions.Add(Cdef);
                txt = new TextBlock() { Text = col.Header, Width = col.Width, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold };
                bdr = new Border();
                bdr.BorderBrush = System.Windows.Media.Brushes.Black;
                bdr.BorderThickness = new Thickness(2);
                Grid.SetColumn(bdr, columnNumber++);
                Grid.SetRow(bdr, 0);
                bdr.Child = txt;
                grd.Children.Add(bdr);
            }
            if (UseScrollView)
            {
                ScrollViewer viewer = new ScrollViewer();
                viewer.Content = grd;
            }
        }
        public TextBlock[] textBlockAdd(string Header, string Name, object bindingSource, string bindingPath, string Default = "", int width = 0, bool fontBold = false)
        {
            TextBlock[] txt = new TextBlock[Columns];

            TextBlock header = new TextBlock() { Text = Header, Width = width, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold };
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, column);
            Grid.SetRow(bdr, 0);
            bdr.Child = header;
            grd.Children.Add(bdr);
            for (int i = 0; i < Rows; i++)
            {
                txt[i] = new TextBlock();
                txt[i].Name = Name;
                if (!Default.Equals(""))
                    txt[i].Text = Default;
                if (fontBold)
                    txt[i].FontWeight = FontWeights.Bold;
                if (width > 0)
                    txt[i].Width = width;
                txt[i].Tag = i;
                bind = new Binding() { Source = bindingSource, Path = new PropertyPath(bindingPath), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                txt[i].SetBinding(TextBlock.TextProperty, bind);
                bdr = new Border() { BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
                bdr.Child = txt[i];
                Grid.SetColumn(bdr, column);
                Grid.SetRow(bdr, i + 1);
                grd.Children.Add(bdr);
            }
            column++;
            columns.Add(new GridColumns { Header = Header, Name = Name, Width = width});
            return txt;
        }

        public TextBox[] textBoxAdd(string Header, string Name, object[] bindingSource, string bindingPath, string Default = "", int width = 0, bool fontBold = false)
        {
            row = 0;
            TextBox[] txt= new TextBox[Rows];
            TextBlock header = new TextBlock() { Text = Header, Width = width, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold };
            bdr = new Border();
            bdr.BorderBrush = System.Windows.Media.Brushes.Black;
            bdr.BorderThickness = new Thickness(2);
            Grid.SetColumn(bdr, column);
            Grid.SetRow(bdr, 0);
            bdr.Child = header;
            grd.Children.Add(bdr);
            for (int i = 0; i < Rows; i++)
            {
                txt[i] = new TextBox();
                txt[i].TabIndex = i + Columns;
                txt[i].Name = Name;
                txt[i].TextAlignment = TextAlignment.Left;
                if (!Default.Equals(""))
                    txt[i].Text = Default;
                if (fontBold)
                    txt[i].FontWeight = FontWeights.Bold;
                if (width > 0)
                    txt[i].Width = width;
                txt[i].Tag = i;
                bind = new Binding() { Source = bindingSource[i], Path = new PropertyPath(bindingPath), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                txt[i].SetBinding(TextBox.TextProperty, bind);
                bdr = new Border() { BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
                bdr.Child = txt[i];
                Grid.SetColumn(bdr, column);
                Grid.SetRow(bdr, i + 1);
                grd.Children.Add(bdr);
            }
            column++;
            columns.Add(new GridColumns { Header = Header, Name = Name, Width = width, ColumnType = typeof(TextBox) });
            return txt;
        }

        public ComboBox[] comboBoxAdd(string Header, string Name, string displayMemberPath, string selectedValuePath, IEnumerable itemsSource,object[] bindingSource, string valuePath, int width = 0, bool fontBold = false)
        {
            ComboBox[] box = new ComboBox[Rows];
            TextBlock header = new TextBlock() { Text = Header, Width = width, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold };
            for (int i = 0; i < Rows; i++)
            {
                box[i] = new ComboBox();
                box[i].Name = Name;
                box[i].TabIndex = i + Columns;
                if (fontBold)
                    box[i].FontWeight = FontWeights.Bold;
                if (width > 0)
                    box[i].Width = width;
                box[i].DisplayMemberPath = displayMemberPath;
                box[i].SelectedValuePath = selectedValuePath;
                box[i].ItemsSource = itemsSource;
                box[i].Tag = i;
                box[i].DataContext = bindingSource[i];
                bind = new Binding() {Path = new PropertyPath(valuePath), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                System.Windows.Data.BindingExpressionBase b =  box[i].SetBinding(ComboBox.SelectedValueProperty, bind);
                bdr = new Border() { BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
                bdr.Child = box[i];
                Grid.SetColumn(bdr, column);
                Grid.SetRow(bdr, i + 1);
                grd.Children.Add(bdr);
            }
            column++;
            columns.Add(new GridColumns { Header = Header, Name = Name, Width = width, ColumnType = typeof(TextBox) });
            return box;
        }

        public void ComboBoxSetSource(ComboBox[] boxes, IEnumerable itemsSource)
        {
            for (int i = 0; i < Rows; i++)
                boxes[i].ItemsSource = itemsSource;
        }

        public CheckBox[] checkBoxAdd(string Header, string Name, object[] bindingSource, string valuePath, int width = 0)
        {
            CheckBox[] box = new CheckBox[Rows];
            for (int i = 0; i < Rows; i++)
            {
                box[i] = new CheckBox();
                box[i].TabIndex = i + Columns;
                box[i].Tag = i;
                box[i].DataContext = bindingSource[i];
                box[i].HorizontalAlignment = HorizontalAlignment.Center;
                box[i].VerticalAlignment = VerticalAlignment.Center;
                bind = new Binding() { Path = new PropertyPath(valuePath), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                System.Windows.Data.BindingExpressionBase b = box[i].SetBinding(CheckBox.IsCheckedProperty, bind);
                bdr = new Border() { BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
                bdr.Child = box[i];
                Grid.SetColumn(bdr, column);
                Grid.SetRow(bdr, i + 1);
                grd.Children.Add(bdr);

            }
            column++;
            columns.Add(new GridColumns { Header = Header, Name = Name, Width = width, ColumnType = typeof(TextBox) });
            return box;
        }

        public NumberEntry[] NumberEntryAdd(string Header, string Name, object[] bindingSource, string bindingPath, string stringFormat, string Default = "", int width = 0, bool fontBold = false)
        {
            NumberEntry[] entry = new NumberEntry[Rows];
            TextBlock header = new TextBlock() { Text = Header, Width = width, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold };
            for (int i = 0; i < Rows; i++)
            {
                entry[i] = new NumberEntry() { Name = Name };
                entry[i].TabIndex = i + Columns; 
                if (fontBold)
                    entry[i].FontWeight = FontWeights.Bold;
                if (width > 0)
                    entry[i].Width = width;
                bind = new Binding() { Source = bindingSource[i], Path = new PropertyPath(bindingPath), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                if (stringFormat != null && !stringFormat.Trim().Equals(""))
                    bind.StringFormat = stringFormat;
                entry[i].SetBinding(TextBox.TextProperty, bind);
                entry[i].Tag = i;
                entry[i].Margin = new Thickness(0, 0, -6, 0);
                bdr = new Border() { BorderBrush = System.Windows.Media.Brushes.Black, BorderThickness = new Thickness(2) };
                bdr.Child = entry[i];
                Grid.SetColumn(bdr, column);
                Grid.SetRow(bdr, i + 1);
                grd.Children.Add(bdr);
            }
            column++;
            columns.Add(new GridColumns { Header = Header, Name = Name, Width = width, ColumnType = typeof(NumberEntry) });
            return entry;
        }
        public void Clear()
        {
            grd.Children.Clear();
            row = 0;
            column = 0;
            Initialize();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up || e.Key == System.Windows.Input.Key.Down)
            {
                
            }
        }

        public Grid grd { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public bool UseScrollView { get; set; }

    }

    public class GridColumns
    {
        public Type ColumnType { get; set; }
        public string Header { get; set; } = "";
        public string Name { get; set; } = "";
        public double Width { get; set; }
        public int ArrayDimensionFirst { get; set; } = 0;
        public int ArrayDimensionSecond { get; set; } = 0;
        public FontWeight FontWeight { get; set; } = FontWeights.Normal;
        public int FontSize { get; set; } = 12;
        public bool FontBold { get; set; } = false;

    }
}
