using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using ColorFont;
using MbcsScanAquire;
using CSharp.QueryBuilder;
using Pdf2Image;
using System.Collections;
using ExtensionMethods;


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmDocumentLayout.xaml
    /// </summary>
    public partial class frmDocumentLayout : Window
    {
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public  Field CurrentField { get; set; }
        public bool Select { get; set; } = true;  
        string filepath;
        ImageBrush ib = null;
        DocumentLayoutList doc;
        Dictionary<int, DocumentLayoutFields> FieldCollection = new Dictionary<int, DocumentLayoutFields>();
        int FieldCount = 0;
        bool IsChanged = false;
        Point _startPoint;
        bool IsDragging = false;
        ContextMenu ctxMenu = new ContextMenu();
        Point currentPoint;
        public FontInfo DefaultFont { get; set; }
        private FontInfo CurrentDefaultFont;
        DocumentCategories[] cats;
        DocumentClasses[] classes;
        ArrayList ClassesArray;

        public frmDocumentLayout()
        {
            InitializeComponent();
        }

        public frmDocumentLayout(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            DocumentLayoutList[] docs = DocumentLayoutList.load(true);
            lstDocuments.ItemsSource = docs;
            cats = DocumentCategories.load();
            lstType.ItemsSource = cats;
            classes = DocumentClasses.load();
            ClassesArray = new ArrayList();
            ClassesArray.Add(new DocumentClasses() { Description = "<Default>"});
            foreach (DocumentClasses cls in classes)
            {
                ClassesArray.Add(cls);
            }
            lstClass.ItemsSource = ClassesArray;
            lstFields.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(lstFields_PreviewMouseDown), true);
            MenuItem itm = new MenuItem();
            itm.Header = "Delete";
            itm.Click += new RoutedEventHandler(Field_Delete);
            ctxMenu.Items.Add(itm);
            itm = new MenuItem();
            itm.Header = "Font";
            itm.Click += new RoutedEventHandler(Field_Font);
            ctxMenu.Items.Add(itm);
            itm = new MenuItem();
            itm.Header = "Select";
            itm.Click += new RoutedEventHandler(Field_Select);
            ctxMenu.Items.Add(itm);

            DefaultFont = new FontInfo(new FontFamily("Times New Roman"), 12.0, System.Windows.FontStyles.Normal, System.Windows.FontStretches.Normal, FontWeights.Regular, new SolidColorBrush(Colors.Black));
        }

        private void cmdRetrieve_Click(object sender, RoutedEventArgs e)
        {
            pnlDocument.Children.Clear();
            CurrentDefaultFont = DefaultFont;
            doc = (DocumentLayoutList)lstDocuments.SelectedItem;
            if (doc != null && doc.DocumentLocation != null && !doc.DocumentLocation.Trim().Equals(""))
            {
                if (doc.DefaultFont != null && !doc.DefaultFont.Trim().Equals(""))
                {
                    try
                    {
                        CurrentDefaultFont = FontInfo.DeSerialize(doc.DefaultFont);
                    }
                    catch {
                        CurrentDefaultFont = DefaultFont;
                    }
                }
                SetDefaultFontLabels();
                ib = new ImageBrush();
                filepath = doc.DocumentLocation;
                if (doc.DocumentLocation.StartsWith("*", StringComparison.Ordinal))
                {
                    string path = GlobalShared.appPath;
                    if (path.EndsWith("\\", StringComparison.Ordinal))
                        path = path.Substring(0, path.Length - 1);
                    filepath = filepath.Replace("*", path + "\\Files");
                }
                try
                {
                    //ib.ImageSource = new BitmapImage(new System.Uri(filepath));
                    ib.ImageSource = BitmapFromUri(new System.Uri(filepath));
                    ib.Stretch = Stretch.Fill ;
                    pnlDocument.Background = ib;
                    pnlDocument.Background.Opacity = .4;

                    lstFields.ItemsSource = null;
                    DocumentLayoutFieldMaster[] fields = DocumentLayoutFieldMaster.loadByCategory(doc.CategoryId);
                    if (fields != null)
                    {
                        lstFields.ItemsSource = fields;
                    }
                }
                catch(SystemException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not load Document: " + filepath, true);
                    return;
                }
                if (doc.DocumentClass > -1)
                {
                    lstClass.SelectedIndex = (int)doc.DocumentClass;
                }
                try
                {
                    FieldCount = 0;
                    DocumentLayoutFields[] fields = DocumentLayoutFields.loadByDocument(doc.ID);
                    if (fields != null && fields.Length > 0)
                    {
                        foreach (DocumentLayoutFields fld in fields)
                        {
                            Field field = new Field(ctxMenu) { thisFont = CurrentDefaultFont };
                            field.Item = fld;
                            field.PreviewKeyDown += pnlDocument_KeyDown;
                            field.PreviewKeyDown += pnlDocument_KeyDown;
                            DocumentLayoutFieldMaster master = DocumentLayoutFieldMaster.load(fld.FieldId);
                            if (master != null)
                                field.Content = master.FieldName;
                            if (fld.Class != null && !fld.Class.Trim().Equals(""))
                            {
                                try
                                {
                                    ColorFont.FontInfo thisFont = ColorFont.FontInfo.DeSerialize(fld.Class);
                                    ColorFont.FontInfo.ApplyFont(field, thisFont);
                                }
                                catch { }
                            }
                            field.Item.Index = FieldCount++;
                            pnlDocument.Children.Add(field);
                            Canvas.SetTop(field, (double)fld.Top);
                            Canvas.SetLeft(field, (double)fld.Left);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Could not load Fields.");
                }
            }
        }

        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
        private void SetDefaultFontLabels()
        {
            lblFontFamily.DataContext = null;
            lblFontStyle.DataContext = null;
            lblFontWeight.DataContext = null;
            lblFontSize.DataContext = null;
            lblFontFamily.DataContext = CurrentDefaultFont;
            lblFontStyle.DataContext = CurrentDefaultFont;
            lblFontWeight.DataContext = CurrentDefaultFont;
            lblFontSize.DataContext = CurrentDefaultFont;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }

            if (lstClass.SelectedIndex > -1)
            {
                doc.DocumentClass  = (int)lstClass.SelectedIndex;
            }


            UIElementCollection children = pnlDocument.Children;
            if (children.Count > 0)
            {
                foreach(UIElement element in children)
                {
                    if (element is Field)
                    {
                        Field fld = (Field)element;
                        if (fld.Deleted && fld.Item.ID > 0)
                            fld.Item.delete(fld.Item.ID.ToString());
                        else
                        {
                            try
                            {
                                ColorFont.FontInfo thisFont = ColorFont.FontInfo.GetControlFont(fld);
                                fld.Item.Class = thisFont.Serialize();
                            }
                            catch { }
                            fld.Item.save();
                        }
                    }
                }
                MessageBox.Show(this, "Fields Saved.", "", MessageBoxButton.OK);
            }
            doc.DefaultFont = CurrentDefaultFont.Serialize();
            doc.save();

            IsChanged = false;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void lstFields_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
    
        private void lstFields_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is TextBlock)
            {
                try
                {
                    CurrentField = null;
                    object obj = GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstFields);
                    DocumentLayoutFieldMaster field = (DocumentLayoutFieldMaster)lstFields.SelectedItem;
                    if (obj != null)
                    {
                        ListBoxItem item = (ListBoxItem)GetListBoxItemClicked((FrameworkElement)e.OriginalSource, lstFields);
                        CurrentField = new Field(ctxMenu) {Content = field.FieldName, fontInfo = CurrentDefaultFont};
                        CurrentField.Item = new DocumentLayoutFields() { FieldId = field.ID, DocumentId = doc.ID, Size = field.Size };
                        CurrentField.Item.Class = CurrentDefaultFont.Serialize();
                        CurrentField.PreviewKeyDown += pnlDocument_KeyDown;
                        FontInfo.ApplyFont(CurrentField, CurrentDefaultFont);
                        _startPoint = e.GetPosition(null);
                    }
                }
                catch { }
            }
        }

        private void lstFields_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging && e.OriginalSource is TextBlock)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e, sender);
                }
            }
        }

        private void pnlDocument_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.OriginalSource is Field || e.OriginalSource is Border || e.OriginalSource is DbTableFields) && e.LeftButton == MouseButtonState.Pressed )
            {
                try
                {
                    CurrentField = (Field)GetCanvasBoxItemClicked((FrameworkElement)e.OriginalSource, pnlDocument);
                    if (CurrentField != null)
                    {
                     _startPoint = e.GetPosition(null);
                    }
                    
                }
                catch { }
                
            }
        }
  
        private void pnlDocument_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(this.m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            Point position = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging && e.OriginalSource is Field)
                 if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                   StartDrag(e, sender);
                }

        }

        private void StartDrag(MouseEventArgs e, object sender)
        {
            if (!GlobalShared.user.hasModifyPermission(this.m_ThisApp))
            {
                MessageBox.Show("You do not have permission to edit.");
                return;
            }
            if (CurrentField != null)
            {
                DataObject data = new DataObject(CurrentField);
                IsDragging = true;
                if (CurrentField.Item.Index > 0)
                {
                    DragDropEffects de = DragDrop.DoDragDrop(this.pnlDocument, data, DragDropEffects.Move);
                }
                else
                {
                    DragDropEffects de = DragDrop.DoDragDrop(this.lstFields, data, DragDropEffects.Copy);
                }
                IsDragging = false;
            }
        }

        private void lstFields_Drop(object sender, DragEventArgs e)
        {
            Point pt = e.GetPosition(pnlDocument);
            if (CurrentField.Item.Index < 0)
                CurrentField.Item.Index = FieldCount++;
            CurrentField.Item.Top = (Decimal)pt.Y;
            CurrentField.Item.Left = (Decimal)pt.X;
            pnlDocument.Children.Add(CurrentField);
            Canvas.SetLeft(CurrentField,pt.X);
            Canvas.SetTop(CurrentField,pt.Y);
        }   

        private void Field_Delete(object sender, RoutedEventArgs e)
        {
            object obj = GetCanvasBoxItemClicked((FrameworkElement) sender, pnlDocument);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
                fld.Deleted = true;
                fld.Visibility = System.Windows.Visibility.Collapsed;

            }
        }

        private void Field_Font(object sender, RoutedEventArgs e)
        {
            object obj = GetCanvasBoxItemClicked((FrameworkElement)sender, pnlDocument,currentPoint.X, currentPoint.Y);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
                ColorFont.ColorFontDialog diag = new ColorFontDialog();
                diag.Owner = this;
                diag.Font = FontInfo.GetControlFont(fld);
                if (diag.ShowDialog() == true)
                {
                    FontInfo selectedFont = diag.Font;

                    if (selectedFont != null)
                    {
                        FontInfo.ApplyFont(fld, selectedFont);
                    }
                }
            }
        }

        private void Field_Select(object sender, RoutedEventArgs e)
        {
            object obj = GetCanvasBoxItemClicked((FrameworkElement) sender, pnlDocument,currentPoint.X, currentPoint.Y);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
                if (fld.Selected)
                {
                    fld.Background = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
                    fld.Selected = false;
                }
                else
                {
                    fld.Background = new SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    fld.Selected = true;
                }
            }

        }


        private void pnlDocument_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentPoint = e.MouseDevice.GetPosition(pnlDocument);
            object obj = GetCanvasBoxItemClicked((FrameworkElement)sender, pnlDocument, currentPoint.X, currentPoint.Y);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
            }

        }

        private void cmdChangeDefaultFont_Click(object sender, RoutedEventArgs e)
        {
            ColorFont.ColorFontDialog diag = new ColorFontDialog();
            diag.Owner = this;
            diag.Font = CurrentDefaultFont;
            if (diag.ShowDialog() == true)
            {
                FontInfo selectedFont = diag.Font;

                if (selectedFont != null)
                {
                    CurrentDefaultFont = selectedFont;
                    SetDefaultFontLabels();
                }
            }

        }

        private void cmdAddDocument_Click(object sender, RoutedEventArgs e)
        {

            frmDocumentAquire frm = new frmDocumentAquire(m_ThisApp);
            frm.ShowDialog();
            DocumentLayoutList[] docs = DocumentLayoutList.load(true);
            lstDocuments.ItemsSource = docs;

        }

        private void cmdDocDelete_Click(object sender, RoutedEventArgs e)
        {

            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (ib == null)
            {
                MessageBoxCustom.Show("Retrieve the document then delete it.");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Delete the document file as well?", "", MessageBoxButton.YesNoCancel);
            ib.ImageSource = null;
            ib = null;

            pnlDocument.Background = null;
            pnlDocument.Children.Clear();
            int docId = doc.ID;
            doc.delete(docId.ToString());
            DocumentLayoutFields[] flds = DocumentLayoutFields.loadByDocument(docId);
            if (flds != null && flds.Length > 0)
                foreach (DocumentLayoutFields fld in flds)
                    fld.delete(fld.ID.ToString());
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    FileInfo file = new FileInfo(filepath);
                    file.Delete();
                    break;
            }
            lstDocuments.ItemsSource = null;
            DocumentLayoutList[] docs = DocumentLayoutList.load(true);
            lstDocuments.ItemsSource = docs;

        }

        private void lstFields_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            cmdDocDelete_Click(sender, e);
        }

        private void pnlDocument_KeyDown(object sender, KeyEventArgs e)
        {
            Key key;
            if (CurrentField != null)
                key = e.Key;
        }

        private void cmdHelp_Click(object sender, RoutedEventArgs e)
        {
            Help.frmHelp frm = new Help.frmHelp();
            frm.PageName = "Help/DocumentOverview.xaml";
            frm.Show();
        }

        private void pnlDocument_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentPoint = e.MouseDevice.GetPosition(pnlDocument);
            object obj = GetCanvasBoxItemClicked((FrameworkElement)sender, pnlDocument, currentPoint.X, currentPoint.Y);
            if (obj != null && obj is Field)
            {
                Field fld = (Field)obj;
            }


        }

        private void cmdDocAlignVertically_Click(object sender, RoutedEventArgs e)
        {
            ArrayList fields = new ArrayList();
            foreach (Field fld in pnlDocument.Children)
            {
                if (fld is Field && fld.Selected)
                    fields.Add(fld);
            }
            if (fields != null && fields.Count > 0)
            {
                Field[] flds = (Field[])fields.ToArray(typeof(Field));
                decimal left = flds[0].Item.Left;
               for (int i = 1; i < flds.Length; i++)
                {
                    flds[i].Item.Left = left;
                    Canvas.SetLeft(flds[i], (double)left);
                }
                pnlDocument.Refresh();
            }
            else
            {
                MessageBox.Show("No fields selected.");
            }
        }

        private void cmdDocAlignHorizontally_Click(object sender, RoutedEventArgs e)
        {
            ArrayList fields = new ArrayList();
            foreach (Field fld in pnlDocument.Children)
            {
                if (fld is Field && fld.Selected)
                    fields.Add(fld);
            }
            if (fields != null && fields.Count > 0)
            {
                Field[] flds = (Field[])fields.ToArray(typeof(Field));
                Array.Sort(flds, delegate (Field f1, Field f2) { return f1.Item.Left.CompareTo(f2.Item.Left); });
                decimal top = flds[0].Item.Top;
                for (int i = 1; i < flds.Length; i++)
                {
                    flds[i].Item.Top = top;
                    Canvas.SetTop(flds[i], (double)top);
                }
                pnlDocument.Refresh();
            }
            else
            {
                MessageBox.Show("No fields selected.");
            }

        }

        private void cmdDocClearSelections_Click(object sender, RoutedEventArgs e)
        {
            foreach(Field fld in pnlDocument.Children)
            {
                if (fld is Field && fld.Selected)
                {
                    fld.Selected = false;
                    fld.Background = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
                }
            }
        }
    }
    public class Field : Label
    {
        public FontInfo thisFont { get; set; }
        public Field() { }
        public Field(ContextMenu ctx,FontInfo fontInfom = null, System.Windows.Media.Brush background = null, double fontsize = 0, System.Windows.Media.Brush fontcolor = null, bool fontbold = false)
        {

            thisFont = fontInfo;
            if (thisFont != null)
            {
                FontFamily = thisFont.Family;
                FontStyle = thisFont.Style;
            }
            if (thisFont != null)
                fontsize = thisFont.Size;
            FontSize = (fontsize == 0 ? 10.0 : fontsize);

            if (background != null)
                Background = background;
            if (thisFont != null)
                Foreground = thisFont.BrushColor;
            else if (fontcolor != null)
                Foreground = fontcolor;
            if (thisFont != null)
                FontWeight = fontInfo.Weight;
            else
                FontWeight = (FontWeight == null ? System.Windows.FontWeights.Bold : FontWeight);
            this.ContextMenu = ctx;
        }
        public DocumentLayoutFields Item
        { get; set; }
        public string FieldName
        { get; set; }
        public FontInfo fontInfo { get; set; }
        public string Family { get { return fontInfo.Family.ToString(); } }
        public string Style { get { return fontInfo.Style.ToString(); } }
        public string Weight { get { return fontInfo.Weight.ToString(); } }
        public string Size { get { return fontInfo.Size.ToString(); } }
        public bool Deleted { get; set; }
        public bool Selected { get; set; }
    }
}
