using ColorFont;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vintasoft.WpfTwain;
using Vintasoft.WpfTwain.ImageEncoders;
using Microsoft.Win32;
using IllusoryStudios.Wpf.LostControls;
using Pdf2Image;

namespace MbcsScan
{
    /// <summary>
    /// Interaction logic for DocumentAquire.xaml
    /// </summary>
    public partial class DocumentAquire : Window
    {
        public object Page { get; set;}
        public new string Name { get; set;}
        public string Description {get; set;}
        public string Location { get; set;}
        public object Type { get; set; }
        public bool Save { get; set;}
        public object[] DocumentType { get; set; }

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        DeviceManager deviceManager;
        /// <summary>
        /// Determines that image acquistion must be canceled because application's form is closing.
        /// </summary>
        bool _cancelTransferBecauseFormIsClosing;
        protected bool _isImageAcquiring = false;
        AcquiredImageCollection _images = new AcquiredImageCollection();
        Bitmap bmp;
        public DocumentAquire()
        {

            InitializeComponent();
            _isImageAcquiring = false;
        }
        public DocumentAquire(string parm)
        {

           InitializeComponent();
            m_ThisApp = (string) parm;
            _isImageAcquiring = false;
        }


        private void FormDocumentAquire_Loaded(object sender, RoutedEventArgs e)
        {
            //DocumentCategories[] cats = DocumentCategories.load();
            lstCategory.ItemsSource = DocumentType;
            try
            {
                deviceManager = new DeviceManager();
                deviceManager.Open();
            }
            catch (TwainDeviceManagerException ex)
            {
                MessageBox.Show("No compatible scanners found: " + ex.Message);
            }   
            if (deviceManager.Devices.Count > 0)
            {
                for (int i = 0; i < deviceManager.Devices.Count; i++)
                {
                    lstScanners.Items.Add(deviceManager.Devices[i].Info.ProductName);

                    if (deviceManager.Devices[i] == deviceManager.DefaultDevice)
                        lstScanners.SelectedIndex = i;
                }
            }
            deviceManager.Close();

        }

        /// <summary>
        /// Acquire images synchronously.
        /// </summary>
        public void AcquireImagesSynchronously(string selectedScanner)
        {
            try
            {
                // open the device manager
                if (deviceManager.State == DeviceManagerState.Closed)
                    deviceManager.Open();

                Device device = deviceManager.Devices.Find(selectedScanner);
                if (device == null)
                {
                    MessageBox.Show(string.Format("Scanner {0} not found",selectedScanner));
                    return;
                }

                // open the device
                SubscribeToDeviceEvents(device);
                device.Open();
                    
                // set acquisition parameters
                device.TransferMode = TransferMode.Memory;
                device.ShowUI = false;
                device.DisableAfterAcquire = true;
                PixelType pixelType = PixelType.BW;
                try
                {
                    if (optGrey.IsChecked.Value)
                        pixelType = PixelType.Gray;
                    else if (optColor.IsChecked.Value)
                        pixelType = PixelType.RGB;

                    device.PixelType = pixelType;
                }
                catch (TwainException)
                {
                    MessageBox.Show(string.Format("Pixel type '{0}' is not supported.", pixelType), "TWAIN device");
                }

                // resolution
                Resolution resolution = new Resolution(300, 300);
                try
                {
                    if (lstResolution.SelectedIndex == 0)
                        resolution = new Resolution(75, 75);
                    else if (lstResolution.SelectedIndex == 1)
                        resolution = new Resolution(100, 100);
                    else if (lstResolution.SelectedIndex == 2)
                        resolution = new Resolution(200, 200);
                    else if (lstResolution.SelectedIndex == 4)
                        resolution = new Resolution(600, 600);

                    device.Resolution = resolution;
                }
                catch (TwainException)
                {
                    MessageBox.Show(string.Format("Resolution '{0}' is not supported.", resolution), "TWAIN device");
                    return;
                }

                // Page size
                try
                {
                    if (lstPageSize.SelectedIndex == 1)
                        device.PageSize = PageSize.USLEGAL;
                    else
                        device.PageSize = PageSize.USLETTER;
                }
                catch (TwainException ex)
                {
                    MessageBox.Show(ex.Message, "TWAIN device");
                    return;
                }
                try
                {
                    // start image acquition process
                    device.Acquire();
                }
                catch (TwainException ex)
                {
                    // specify that image acquisition is finished
                    _isImageAcquiring = false;

                    MessageBox.Show(ex.Message, "TWAIN device");
                    return;
                }
            }
            finally
            {}
        }

        private void ProcessImage(Device device)
        {
            ViewImage.Width = new System.Windows.GridLength(500);
            AcquiredImage image = device.AcquiredImage;
            TwainBmpEncoderSettings toBmp = new TwainBmpEncoderSettings();
            MemoryStream mem = image.GetAsStream(toBmp);
            BitmapImage bmp = new BitmapImage();
            bmp.StreamSource = mem;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = bmp;
            ib.Stretch = Stretch.Uniform;
            pnlDocument.Background = ib;
        }
        /// <summary>
        /// Subscribe to the device events.
        /// </summary>
        private void SubscribeToDeviceEvents(Device device)
        {
            device.StateChanged += new EventHandler<DeviceStateChangedEventArgs>(device_StateChanged);
            device.ImageAcquiringProgress += new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired += new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanCompleted += new EventHandler(device_ScanCompleted);
            device.ScanCanceled += new EventHandler(device_ScanCanceled);
            device.ScanFailed += new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            //device.UserInterfaceClosed += new EventHandler(device_UserInterfaceClosed);
        }

        /// <summary>
        /// Unsubscribe from the device events.
        /// </summary>
        private void UnsubscribeFromDeviceEvents(Device device)
        {
            device.StateChanged -= new EventHandler<DeviceStateChangedEventArgs>(device_StateChanged);
            device.ImageAcquiringProgress -= new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanCompleted -= new EventHandler(device_ScanCompleted);
            device.ScanCanceled -= new EventHandler(device_ScanCanceled);
            device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            //device.UserInterfaceClosed -= new EventHandler(device_UserInterfaceClosed);
        }


        /// <summary>
        /// Device state is changed.
        /// </summary>
        private void device_StateChanged(object sender, DeviceStateChangedEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                Device device = (Device)sender;
                // cancel image acquisition
                device.CancelTransfer();
            }
        }

        /// <summary>
        /// Image acquiring progress is changed.
        /// </summary>
        private void device_ImageAcquiringProgress(object sender, ImageAcquiringProgressEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                Device device = (Device)sender;
                // cancel image acquisition
                device.CancelTransfer();
                return;
            }

        }

        /// <summary>
        /// Image is acquired.
        /// </summary>
        private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                Device device = (Device)sender;
                // cancel image acquisition
                device.CancelTransfer();
                return;
            }

            //ViewImage.Width = new System.Windows.GridLength(450);
            AcquiredImage image =  (AcquiredImage)e.Image;
            image.Deskew(BorderColor.AutoDetect,5,5);
            TwainBmpEncoderSettings toBmp = new TwainBmpEncoderSettings();
            MemoryStream mem = image.GetAsStream(toBmp);
            BitmapImage _reducedImage = new BitmapImage();
            
            //_reducedImage = new BitmapImage();
            _reducedImage.BeginInit();
            //_reducedImage.CacheOption = BitmapCacheOption.None;
            //_reducedImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            //_reducedImage.DecodePixelWidth = 425 ;
            _reducedImage.StreamSource = mem;
            //_reducedImage.Rotation = Rotation.Rotate0;
            _reducedImage.EndInit();
            
            mem.Position = 0;
            bmp = new Bitmap(mem);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = _reducedImage;
            ib.Stretch = Stretch.Uniform;
            pnlDocument.Background = ib;
            cmdScanSave.IsEnabled = true;
        }

        /// <summary>
        /// Scan is completed.
        /// </summary>
        private void device_ScanCompleted(object sender, EventArgs e)
        {
            Device device = (Device)sender;

            // disable device if UI is not shown
            if (device.State == DeviceState.Enabled)
            {
                if (!device.ShowUI)
                    device.Disable();
            }

            // close device if device is opened
            if (device.State == DeviceState.Opened)
                device.Close();

            if (!device.ShowUI)
            {
                // unsubscribe from device events
                UnsubscribeFromDeviceEvents(device);
                // specify that image acquisition is finished
                _isImageAcquiring = false;
                // update UI
            }
        }

        /// <summary>
        /// Scan is canceled.
        /// </summary>
        private void device_ScanCanceled(object sender, EventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                _cancelTransferBecauseFormIsClosing = false;
                // close the application's form
                this.Close();
                return;
            }

            Device device = (Device)sender;

            // close device if device is opened
            if (device.State == DeviceState.Opened)
                device.Close();

            // unsubscribe from device events
            UnsubscribeFromDeviceEvents(device);
            // specify that image acquisition is finished
            _isImageAcquiring = false;
            // update UI
        }

        /// <summary>
        /// Scan is failed.
        /// </summary>
        private void device_ScanFailed(object sender, ScanFailedEventArgs e)
        {
            Device device = (Device)sender;

            // close device if device is opened
            if (device.State == DeviceState.Opened)
                device.Close();

            // show error message
            MessageBox.Show(e.ErrorString, "Scan failed");

            // unsubscribe from device events
            UnsubscribeFromDeviceEvents(device);
            // specify that image acquisition is finished
            _isImageAcquiring = false;
            // update UI
        }
        /// <summary>
        /// Device UI is closed.
        /// </summary>
        private void device_UserInterfaceClosed(object sender, EventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                _cancelTransferBecauseFormIsClosing = false;
                // close the application's form
                this.Close();
                return;
            }

            Device device = (Device)sender;

            // close device if device is opened
            if (device.State == DeviceState.Opened)
                device.Close();

            // unsubscribe from device events
            UnsubscribeFromDeviceEvents(device);
            // specify that image acquisition is finished
            _isImageAcquiring = false;
            // update UI
        }

        private void cmdScan_Click(object sender, RoutedEventArgs e)
        {
            if (lstScanners.SelectedIndex < 0)
            {
                MessageBox.Show("You have not selected a scanner.");
                return;
            }
            AcquireImagesSynchronously((string)lstScanners.SelectedItem);
        }

        private void FormDocumentAquire_Closed(object sender, EventArgs e)
        {
        }

        private void cmdScanSave_Click(object sender, RoutedEventArgs e)
        {
            if (bmp == null)
            {
                MessageBox.Show("There is no image to save.");
                Save = false;
                return;
            }

            bool done = false;
            string[] fields = { "Name", "Description" };
            InputBox input = new InputBox("Scan Details","", fields);
            while (!done)
            {
                if (lstCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a Document Type");
                    continue;
                }
                if (input.Show(this))
                {
                    string name = (string)input.Fields[0].Value;
                    string description = (string)input.Fields[1].Value;
                    if (name == null || name.Trim().Equals(""))
                    {
                        MessageBox.Show("A Page name of 1 to 50 characters must be entered.");
                        continue;
                    }
                    if (name.Length > 50)
                    {
                        MessageBox.Show("Page Name is too long.  Must be 50 characters or less.");
                        continue;
                    }
                    SaveFileDialog frm = new SaveFileDialog();

                    frm.DefaultExt = ".bmp";
                    frm.Filter = "BitMap|.bmp";
                    frm.ShowDialog();
                    if (frm.FileName != null && !frm.FileName.Trim().Equals(""))
                    {
                        string filename = frm.FileName;
                        bmp.Save(filename);

                        Location = filename;
                        Name = name;
                        Description = description;
                        Page = bmp;
                        Save = true;
                        Type = lstCategory.SelectedItem;

                        /* MbcsCentral Specific
                        DocumentLayoutList doc = new DocumentLayoutList();
                        doc.DocumentName = name;
                        doc.DocumentLocation = filename;
                        doc.CategoryId = ((DocumentCategories)lstCategory.SelectedItem).ID;
                        if (doc.save())
                        {
                            MessageBox.Show("Scanned Document saved.");
                            cmdScanSave.IsEnabled = false;
                            done = true;
                        }
                        else
                        {
                            if (MessageBox.Show("Unable to save Scanned Document.  Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                done = true;
                        }
                        */

                    }
                }
                else
                    done = true;
            }
        }

        private void cmdBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlgBrowse = new OpenFileDialog();
            dlgBrowse.Filter = "All Formats|*.bmp;*.gif;*.jpg;*.tif;*.pdf;*.txt|BMP|*.bmp|GIFF|*.gif|JPEG|*.jpg|TIFF|*.tif|PDF|*.pdf|Text|*.txt|All Files|*.*";
            dlgBrowse.InitialDirectory = "\\";
            dlgBrowse.Multiselect = false;
            dlgBrowse.ShowDialog(this);

            if (dlgBrowse.FileName != null && !dlgBrowse.FileName.Trim().Equals(""))
            {
                string filename = dlgBrowse.FileName;
                putImage(filename);
            }

        }

        private void putImage(string fileName)
        {
            string ext;
            Bitmap raw = null;
            string importFile = null;

            ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
            /*
            Dim itm As New LidorSystems.IntegralUI.Lists.ListViewItem
            Dim itmHoverStyle As New LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle
            itmHoverStyle.BackColor = Color.Black
            itmHoverStyle.BorderColor = Color.DarkCyan
            itmHoverStyle.BackFadeColor = Color.Red
            If IsNothing(imageArray) Then
                imageArray = New ArrayOfBitmap
            End If
             */
            int i = 1;
            int maxPages = 1;
            Pdf2Image.Pdf2ImageConverter apdf = null;
            System.Windows.Window frm = new System.Windows.Window();

            Label lbl = null;
            switch (ext.ToLower())
            {
                case "pdf":
                    apdf = new Pdf2Image.Pdf2ImageConverter(fileName);
                    maxPages = apdf.GetPageCount();
                    importFile = "";
                    if (MessageBox.Show("Unpack and import pages individually?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes )
                    {
                        frm.Title = "Processing PDF Document";
                        //frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        frm.Width = 250; 
                        frm.Height = 100;
                        lbl = new Label();
                        lbl.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
                        lbl.FontSize = 16.0;
                        lbl.Name = "lbl";
                        frm.Content = lbl;
                        frm.Show();
                        frm.Focus();
                    }
                    else
                        maxPages = 1;
                    break;
                case "doc":
                case "docx":
                case "txt":
                    importFile = fileName;
                    StreamReader fs = new StreamReader(fileName, false);
                    TextReader tr = fs;
                    System.Drawing.Color fontColor = System.Drawing.Color.Black;
                    System.Drawing.Color backColor = System.Drawing.Color.White;
                    string fontName = "Times New Roman";
                    int fontSize = 10;
                    int height = 1100;
                    int width = 850;
                    raw = new Bitmap(width, height);
                    Graphics objgraphics  = Graphics.FromImage(raw);
                    Font objFont = new Font(fontName, fontSize);
                    //Following PointF object defines where the text will be displayed in the 
                    //specified area of the image
                    PointF objPoint = new PointF(5.0F, 5.0F);
                    SolidBrush objBrushForeColor = new SolidBrush(fontColor);
                    SolidBrush objBrushBackColor = new SolidBrush(backColor);
                    objgraphics.FillRectangle(objBrushBackColor, 0, 0, width, height);
                    string text = tr.ReadLine();
                    while (!fs.EndOfStream)
                    {
                        objgraphics.DrawString(text, objFont, objBrushForeColor, objPoint);
                        objPoint.Y += 15.0F;
                        text = tr.ReadLine();
                    }
                    tr.Close();
                    fs.Close();
                    maxPages = 1;
                    break;
                default:
                    importFile = fileName;
                    break;
            }
            while (i <= maxPages)
            {
                lbl = (Label)frm.FindName("lbl");
                this.Cursor = System.Windows.Input.Cursors.Wait;
                if (ext.ToLower().Equals("pdf"))
                {
                    if (importFile.Trim().Equals(""))
                    {
                        
                        lbl.Content = "Extracting page " + i + " of " + maxPages;
                    }
                    raw = apdf.GetImage(i, 72, Pdf2Image.Pdf2ImageFormat.BMP_24BITCOLOR);
                } else if (!ext.ToLower().Equals("txt"))
                    raw = (Bitmap)Bitmap.FromFile(fileName);
                //processImage(raw, source, importFile);
                raw.Dispose();
                i += 1;
            }
            try
            {
                if (ext.ToLower().Equals("pdf") && importFile.Trim().Equals(""))
                {
                    apdf = null;
                    frm.Close();
                    frm = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = System.Windows.Input.Cursors.Arrow;

        }
        private void processImage(Bitmap raw , String source, String importFile = "")
        {    
            /*
            Dim itm As New LidorSystems.IntegralUI.Lists.ListViewItem, ptr As System.IntPtr
            Dim itmHoverStyle As New LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle
            itmHoverStyle.BackColor = Color.Black
            itmHoverStyle.BorderColor = Color.DarkCyan
            itmHoverStyle.BackFadeColor = Color.Red

            legal = isLegalSize(raw);
            System.Drawing.Graphics g = null;
            Bitmap btmap new Bitmap(raw.Width, raw.Height);
            btmap.SetResolution(raw.HorizontalResolution, raw.VerticalResolution);
            g = Graphics.FromImage(btmap);
            g.DrawImage(raw, 0, 0);
            raw.Dispose();
            imageArray.Add(btmap)
            itm = New LidorSystems.IntegralUI.Lists.ListViewItem()
            itm.LargeImageIndex = imageArray.Count - 1
            itm.Image = btmap
            itm.HoverStyle = itmHoverStyle
            itm.Tag = importFile

            ' imagelist replacement
            itm.LargeImage = btmap.GetThumbnailImage(scanThumb.Width, scanThumb.Height, Nothing, ptr)
            itm.Text = IIf(source = "", IIf(legal, "*L ", "") & "Page #" & Str(itm.LargeImageIndex + 1), source)
            itm.Tag = "."
            lstDocument.Items.Add(itm)
            */
        }

        public class Field : Label
        {
            public FontInfo thisFont { get; set; }
            public Field() { }
            public Field(ContextMenu ctx, FontInfo fontInfom = null, System.Windows.Media.Brush background = null, double fontsize = 0, System.Windows.Media.Brush fontcolor = null, bool fontbold = false)
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
            public string FieldName
            { get; set; }
            public FontInfo fontInfo { get; set; }
            public string Family { get { return fontInfo.Family.ToString(); } }
            public new string Style { get { return fontInfo.Style.ToString(); } }
            public string Weight { get { return fontInfo.Weight.ToString(); } }
            public string Size { get { return fontInfo.Size.ToString(); } }
            public bool Deleted
            { get; set; }
        }

        private void cmdHelp_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
