using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
//using Vintasoft.WpfTwain;
//using Vintasoft.WpfTwain.ImageEncoders;
using Vintasoft.Twain;
using Vintasoft.Twain.ImageEncoders;
using Microsoft.Win32;
using IllusoryStudios.Wpf.LostControls;
using Pdf2Image;
using Spire.Pdf;

namespace MbcsCentral
{

    /// <summary>
    /// Interaction logic for frmDocumentAquire.xaml
    /// </summary>
    public partial class frmDocumentAquire : Window
    {
        /*
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName,
          MoveFileFlags dwFlags);
          */

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
        bool _isImageAcquiring;
        AcquiredImageCollection _images = new AcquiredImageCollection();
        Bitmap bmp;
        string safeFileName = "";
        string saveFileName = "";
        bool canScan = true;
        string scannedFileName = "";
        MemoryStream memory;

        public frmDocumentAquire()
        {
            // Vintasoft Registration
            try
            {
                TwainGlobalSettings twainGlobalSettings1 = new TwainGlobalSettings();
                twainGlobalSettings1.Register(GlobalShared.VINTASOFT_USER, GlobalShared.VINTASOFT_EMAIL, GlobalShared.VINTASOFT_REGCODE);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Unable to register VintaSoft Twain. Scanning will not be available: " + ex.Message);
            }

            InitializeComponent();
        }
        public frmDocumentAquire(string parm)
        {
            // Vintasoft Registration
            try
            {
                TwainGlobalSettings twainGlobalSettings1 = new TwainGlobalSettings();
                twainGlobalSettings1.Register(GlobalShared.VINTASOFT_USER, GlobalShared.VINTASOFT_EMAIL, GlobalShared.VINTASOFT_REGCODE);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Unable to register VintaSoft Twain. Scanning will not be available: " + ex.Message);
            }

            InitializeComponent();
            m_ThisApp = (string)parm;
        }


        private void FormDocumentAquire_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            DocumentCategories[] cats = DocumentCategories.load();
            lstCategory.ItemsSource = cats;
            DocumentLayoutList[] docs = DocumentLayoutList.load(false, true);

            string name = "";
            lstOriginalDocs.ItemsSource = docs;
            try
            {
                deviceManager = new DeviceManager();
                deviceManager.Open();
            }
            catch (TwainInvalidStateException exState)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Cannot start scanner device", true);
                canScan = false;
            }
            catch (TwainDeviceManagerException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "No compatible scanners found", true);
                canScan = false;
            }
            catch (Exception gex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Cannot scan: " + gex.Message, true);

                canScan = false;
            }
            if (canScan && deviceManager.Devices.Count > 0)
            {
                for (int i = 0; i < deviceManager.Devices.Count; i++)
                {
                    lstScanners.Items.Add(deviceManager.Devices[i].Info.ProductName);

                    if (deviceManager.Devices[i] == deviceManager.DefaultDevice)
                        lstScanners.SelectedIndex = i;
                }
            }
            if (canScan) deviceManager.Close();

        }

        public void UpdateImage(BitmapImage image)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(() => this.pnlDocument.Source = image));
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
                    System.Windows.MessageBox.Show(string.Format("Scanner {0} not found", selectedScanner));
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
                    System.Windows.MessageBox.Show(string.Format("Pixel type '{0}' is not supported.", pixelType), "TWAIN device");
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
                    System.Windows.MessageBox.Show(string.Format("Resolution '{0}' is not supported.", resolution), "TWAIN device");
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
                    System.Windows.MessageBox.Show(ex.Message, "TWAIN device");
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

                    System.Windows.MessageBox.Show(ex.Message, "TWAIN device");
                    return;
                }
            }
            finally
            { }
        }

        private void ProcessImage(Device device)
        {
            ViewImage.Width = new System.Windows.GridLength(500);
            AcquiredImage image = device.AcquiredImage;
            //TwainBmpEncoderSettings toBmp = new TwainBmpEncoderSettings();
            //MemoryStream mem = image.GetAsStream(toBmp);
            Bitmap bit;
            bit = image.GetAsBitmap();
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bit.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bit.Width, bit.Height));
            //bmp.StreamSource = mem;

            //ImageBrush ib = new ImageBrush();
            //ib.ImageSource = bs;
            //ib.Stretch = Stretch.Uniform;
            //pnlDocument.Background = bs;
        }
        /// <summary>
        /// Subscribe to the device events.
        /// </summary>
        private void SubscribeToDeviceEvents(Device device)
        {
            //device.StateChanged += new EventHandler<DeviceStateChangedEventArgs>(device_StateChanged);
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
            //device.StateChanged -= new EventHandler<DeviceStateChangedEventArgs>(device_StateChanged);
            device.ImageAcquiringProgress -= new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanCompleted -= new EventHandler(device_ScanCompleted);
            device.ScanCanceled -= new EventHandler(device_ScanCanceled);
            device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            //device.UserInterfaceClosed -= new EventHandler(device_UserInterfaceClosed);
        }


        /*
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
        */

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
            Device device = (Device)sender;
            // image acquistion must be canceled because application's form is closing
            if (_cancelTransferBecauseFormIsClosing)
            {
                // cancel image acquisition
                device.CancelTransfer();
                return;
            }
            //ViewImage.Width = new System.Windows.GridLength(500);
            AcquiredImage image = e.Image;

            image.Deskew(BorderColor.AutoDetect, 5, 5);
            TwainBmpEncoderSettings toBmp = new TwainBmpEncoderSettings();
            MemoryStream mem = image.GetAsStream(toBmp);
            //scannedFileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".bmp";
            //image.SaveToFile(scannedFileName, toBmp);
            memory = mem;
            //imageAquired = new BitmapImage();
            //imageAquired.StreamSource = mem;
            //mageAquired.

            /*
            //ViewImage.Width = new System.Windows.GridLength(450);
            AcquiredImage image =  (AcquiredImage)e.Image;
            image.Deskew(BorderColor.AutoDetect,5,5);
            image.Save("c:\\temp\\test.tif");
            Bitmap bit = image.GetAsBitmap();
            image.Dispose();
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bit.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bit.Width, bit.Height));
            //TwainBmpEncoderSettings toBmp = new TwainBmpEncoderSettings();
            //MemoryStream mem = image.GetAsStream(toBmp);
            //BitmapImage _reducedImage = new BitmapImage();
            /*
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
            pnlDocument.Background = bs;
            //ImageBrush ib = new ImageBrush();
            //ib.ImageSource = bs;
            //ib.Stretch = Stretch.Uniform;
            //pnlDocument.Background = ib;

           */


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
                CmdRefresh_Click(sender, new RoutedEventArgs());

            }
        }

        /// <summary>
        /// Scan is canceled.
        /// </summary>
        private void device_ScanCanceled(object sender, EventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            cmdScanSave.IsEnabled = false;
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
            System.Windows.MessageBox.Show(e.ErrorString, "Scan failed");

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
            if (!canScan)
            {
                MessageBoxCustom.Show("Scanning is not available.  See system log.");
                return;
            }
            if (lstScanners.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("You have not selected a scanner.");
                return;
            }
            AcquireImagesSynchronously((string)lstScanners.SelectedItem);
            cmdScanSave.IsEnabled = true;
            cmdFileSave.IsEnabled = false;
            cmdSaveFromDoc.IsEnabled = false;
        }

        private void FormDocumentAquire_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSaveFromDoc_Click(object sender, RoutedEventArgs e)
        {
            if (lstOriginalDocs.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("An original document has not be selected.");
                return;
            }

            DocumentLayoutList document = (DocumentLayoutList)lstOriginalDocs.SelectedItem;

            if (MessageBoxCustom.Show("Copy from: " + document.DocumentName + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            bool done = false;
            string[] fields = { "Name", "Description" };
            InputBox input = new InputBox("Document Details", "", fields);
            while (!done)
            {
                if (lstOriginalDocs.SelectedIndex < 0)
                {
                    System.Windows.MessageBox.Show("Please select a Document Type");
                    return;
                }
                if (input.Show(this))
                {
                    string name = (string)input.Fields[0].Value;
                    string description = (string)input.Fields[1].Value;
                    if (name == null || name.Trim().Equals(""))
                    {
                        System.Windows.MessageBox.Show("A Document name of 1 to 50 characters must be entered.");
                        continue;
                    }
                    if (name.Length > 50)
                    {
                        System.Windows.MessageBox.Show("Document Name is too long.  Must be 50 characters or less.");
                        continue;
                    }

                    Microsoft.Win32.SaveFileDialog frm = new Microsoft.Win32.SaveFileDialog();

                    frm.DefaultExt = ".bmp";
                    frm.Filter = "BitMap|.bmp";
                    frm.InitialDirectory = GlobalShared.appPath + "Files\\";

                    frm.ShowDialog();
                    if (frm.FileName != null && !frm.FileName.Trim().Equals(""))
                    {
                        string filepath = document.DocumentLocation;
                        if (document.DocumentLocation.StartsWith("*", StringComparison.Ordinal))
                        {
                            string path = GlobalShared.appPath;
                            if (path.EndsWith("\\", StringComparison.Ordinal))
                                path = path.Substring(0, path.Length - 1);
                            filepath = filepath.Replace("*", path + "\\Files");
                        }
                        DocumentLayoutList doc = new DocumentLayoutList();
                        string filename = frm.FileName;
                        File.Copy(filepath, filename);
                        doc.DocumentName = name;
                        doc.DocumentLocation = filename;
                        doc.CategoryId = ((DocumentLayoutList)lstOriginalDocs.SelectedItem).ID;
                        bool FieldsCopied = false;
                        if (doc.save())
                        {
                            if (chkFields.IsChecked.Value)
                            {
                                DocumentLayoutFields[] Docfields = DocumentLayoutFields.loadByDocument(document.ID);
                                if (Docfields != null && Docfields.Length > 0)
                                {
                                    foreach(DocumentLayoutFields DocField in Docfields)
                                    {
                                        DocumentLayoutFields newField = DocField;
                                        newField.ID = 0;
                                        newField.DocumentId = doc.ID;
                                        newField.save();
                                    }
                                    FieldsCopied = true;
                                }
                            }
                            System.Windows.MessageBox.Show("Document " + (FieldsCopied ? " and Fields " : "") + " copied.");
                            cmdSaveFromDoc.IsEnabled = false;
                            done = true;
                        }
                        else
                        {
                            if (System.Windows.MessageBox.Show("Unable to save Scanned Document.  Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                done = true;
                        }
                    }
                }
                else
                    done = true;
            }
        }

        private void cmdScanSave_Click(object sender, RoutedEventArgs e)
        {
            cmdScanSave.IsEnabled = false;
            if (!canScan)
            {
                MessageBoxCustom.Show("Scanning is not available.  See system log.");
                return;
            }
            if (bmp == null)
            {
                System.Windows.MessageBox.Show("Document image cannot be found.");
                return;
            }

            bool done = false;
            string[] fields = { "Name", "Description" };
            InputBox input = new InputBox("Document Details", "", fields);
            while (!done)
            {
                if (lstCategory.SelectedIndex < 0)
                {
                    System.Windows.MessageBox.Show("Please select a Document Type");
                    return;
                }
                if (input.Show(this))
                {
                    string name = (string)input.Fields[0].Value;
                    string description = (string)input.Fields[1].Value;
                    if (name == null || name.Trim().Equals(""))
                    {
                        System.Windows.MessageBox.Show("A Document name of 1 to 50 characters must be entered.");
                        continue;
                    }
                    if (name.Length > 50)
                    {
                        System.Windows.MessageBox.Show("Document Name is too long.  Must be 50 characters or less.");
                        continue;
                    }
                    Microsoft.Win32.SaveFileDialog frm = new Microsoft.Win32.SaveFileDialog();

                    frm.DefaultExt = ".bmp";
                    frm.Filter = "BitMap|.bmp";
                    frm.InitialDirectory = GlobalShared.appPath + "Files\\";

                    frm.ShowDialog();
                    if (frm.FileName != null && !frm.FileName.Trim().Equals(""))
                    {
                        string filename = frm.FileName;
                        bmp.Save(filename);
                        DocumentLayoutList doc = new DocumentLayoutList();
                        doc.DocumentName = name;
                        doc.DocumentLocation = filename;
                        doc.CategoryId = ((DocumentCategories)lstCategory.SelectedItem).ID;
                        if (doc.save())
                        {
                            System.Windows.MessageBox.Show("Scanned Document saved.");
                            cmdScanSave.IsEnabled = false;
                            done = true;
                        }
                        else
                        {
                            if (System.Windows.MessageBox.Show("Unable to save Scanned Document.  Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                done = true;
                        }
                    }
                }
                else
                    done = true;
            }
        }

        private void cmdBrowse_Click(object sender, RoutedEventArgs e)
        {
            cmdScanSave.IsEnabled = false;
            System.Windows.Forms.OpenFileDialog dlgBrowse = new System.Windows.Forms.OpenFileDialog();
            dlgBrowse.Filter = "Graphic Formats (*.bmp;*.gif;*.jpg;*.tif;*.pdf)|*.bmp;*.gif;*.jpg;*.tif;*.pdf|BitImage (*.bmp)|*.bmp|Giff (*.gif)|*.gif|JPEG (*.jpeg)|*.jpg|TIFF (*.tif)|*.tif|PDF (*.pdf|*.pdf|Text (*.txt)|*.txt|All Files (*.*)|*.*";
            dlgBrowse.InitialDirectory = "\\";
            dlgBrowse.Multiselect = false;
            ImageBrush ib = new ImageBrush();
            if (dlgBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlgBrowse.FileName != null && !dlgBrowse.FileName.Trim().Equals(""))
            {
                cmdScanSave.IsEnabled = false;
                txtFile.Text = dlgBrowse.FileName;
                safeFileName = dlgBrowse.SafeFileName;
                System.Windows.Media.Imaging.BitmapImage btMap = new System.Windows.Media.Imaging.BitmapImage();
                if (txtFile.Text.Trim().EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
                {
                    PdfDocument doc = new PdfDocument();
                    try
                    {
                        doc.LoadFromFile(txtFile.Text);
                    }
                    catch (Exception ex)
                    {

                    }
                    Bitmap bit = new Bitmap(doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Bitmap));

                    BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bit.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bit.Width, bit.Height));
                    ib.ImageSource = bs;
                    pnlDocument.Source = bs;
                    bit.Dispose();
                    saveFileName = txtFile.Text.Replace(".PDF", ".bmp").Replace(".pdf", ".bmp").Replace(".Pdf", ".bmp");
                }
                else
                {
                    Uri uri = new Uri(txtFile.Text, UriKind.Absolute);
                    btMap.BeginInit();
                    btMap.UriSource = uri;
                    btMap.EndInit();
                    ib.ImageSource = btMap;
                    saveFileName = txtFile.Text;
                    pnlDocument.Source = btMap;
                }
                ib.Stretch = Stretch.Uniform;
                cmdFileSave.IsEnabled = true;
                cmdScanSave.IsEnabled = false;
                cmdSaveFromDoc.IsEnabled = false;
            }

        }

        private void putImage(string fileName)
        {
            string ext;
            Bitmap raw = null;
            string importFile = null;
            ext = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);

            int i = 0;
            int maxPages = 1;
            //Pdf2Image.Pdf2ImageConverter apdf = null;
            PdfDocument doc = new PdfDocument();

            System.Windows.Window frm = new System.Windows.Window();

            System.Windows.Controls.Label lbl = null;
            switch (ext.ToLower())
            {
                case "pdf":
                    // TODO: Remove all references to Pdf2Image
                    //apdf = new Pdf2Image.Pdf2ImageConverter(fileName);
                    //maxPages = apdf.GetPageCount();
                    doc.LoadFromFile(fileName);
                    maxPages = doc.Pages.Count;
                    if (maxPages > 3)
                        maxPages = 3;
                    importFile = "";
                    if (System.Windows.MessageBox.Show("Unpack and import pages individually?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        frm.Title = "Processing PDF Document";
                        frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        frm.Width = 250;
                        frm.Height = 100;
                        lbl = new System.Windows.Controls.Label();
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
                    Graphics objgraphics = Graphics.FromImage(raw);
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
            while (i < maxPages)
            {
                lbl = (System.Windows.Controls.Label)frm.FindName("lbl");
                this.Cursor = System.Windows.Input.Cursors.Wait;
                if (ext.ToLower().Equals("pdf"))
                {
                    if (importFile.Trim().Equals(""))
                    {

                        //lbl.Content = "Extracting page " + i + " of " + maxPages;
                    }
                    //raw = apdf.GetImage(i, 200, Pdf2Image.Pdf2ImageFormat.BMP_8BIT_COLOR);
                    raw = new Bitmap(doc.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Bitmap));
                }
                else if (!ext.ToLower().Equals("txt"))
                    raw = (Bitmap)System.Drawing.Image.FromFile(fileName);
                SaveImage(fileName, ext, raw);
                raw.Dispose();
                i += 1;
            }
            try
            {
                if (ext.ToLower().Equals("pdf") && importFile.Trim().Equals(""))
                {
                    //apdf = null;
                    frm.Close();
                    frm = null;
                }
            }
            catch (Exception ex)
            {

            }
            this.Cursor = System.Windows.Input.Cursors.Arrow;

        }

        private void SaveImage(string chosenfile, string ext, Bitmap bmp)
        {

            bool done = false;
            string[] fields = { "Name", "Description" };
            InputBox input = new InputBox("Document Details", "", fields);
            while (!done)
            {
                if (lstCategory.SelectedIndex < 0)
                {
                    System.Windows.MessageBox.Show("Please select a Document Type");
                    return;
                }
                if (input.Show(this))
                {
                    string name = (string)input.Fields[0].Value;
                    string description = (string)input.Fields[1].Value;
                    if (name == null || name.Trim().Equals(""))
                    {
                        System.Windows.MessageBox.Show("A Document name of 1 to 50 characters must be entered.");
                        continue;
                    }
                    if (name.Length > 50)
                    {
                        System.Windows.MessageBox.Show("Document Name is too long.  Must be 50 characters or less.");
                        continue;
                    }

                    FileInfo info = new FileInfo(chosenfile);
                    bool exists = info.Exists;
                    MessageBoxResult defaultLocation = System.Windows.MessageBox.Show("Save in default location (recommended)", "", MessageBoxButton.YesNo);
                    string filename;
                    if (defaultLocation == MessageBoxResult.Yes)
                    {
                        filename = GlobalShared.appPath + "Files\\" + safeFileName.Replace(".pdf", ".bmp").Replace(".PDF", ".bmp");
                    }
                    else
                    {

                        Microsoft.Win32.SaveFileDialog frm = new Microsoft.Win32.SaveFileDialog();
                        frm.DefaultExt = ext;
                        frm.Filter = "BitMap|.bmp";
                        frm.FileName = chosenfile;
                        frm.ShowDialog();
                        filename = frm.FileName;
                    }
                    if (filename != null && !filename.Trim().Equals(""))
                    {
                        if (chosenfile.ToLower().Equals(filename.ToLower().Replace(".pdf", ".bmp")))
                        {
                            if (System.Windows.MessageBox.Show("Document with that filename already exists.  Do you want to overwrite it?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                return;
                        }
                        try
                        {
                            Bitmap bit = new Bitmap(bmp);
                            bmp.Dispose();
                            bit.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                        catch (System.Runtime.InteropServices.ExternalException ex)
                        {
                            System.Windows.MessageBox.Show("Could not save document.  Are you attempting to save it to the same location from which you retrieved it? > " + ex.Message);
                            return;
                        }
                        DocumentLayoutList doc = new DocumentLayoutList();
                        doc.DocumentName = name;
                        doc.DocumentLocation = (defaultLocation == MessageBoxResult.Yes ? "*\\" + safeFileName.Replace(".pdf", ".bmp").Replace(".PDF", ".bmp") : filename.Replace(".pdf", ".bmp").Replace(".PDF", ".bmp"));
                        doc.CategoryId = ((DocumentCategories)lstCategory.SelectedItem).ID;
                        if (doc.save())
                        {
                            System.Windows.MessageBox.Show("Document saved.");
                            cmdScanSave.IsEnabled = false;
                            cmdFileSave.IsEnabled = false;
                            done = true;
                        }
                        else
                        {
                            if (System.Windows.MessageBox.Show("Unable to save Document.  Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                done = true;
                        }
                    }
                }
                else
                    done = true;
            }
        }

        private void processImage(Bitmap raw, String source, String importFile = "")
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

        private void cmdHelp_Click(object sender, RoutedEventArgs e)
        {
            Help.HelpMain frm = new Help.HelpMain();
            frm.Show();
        }

        private void cmdFileSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFile.Text.Trim().Equals(""))
            {
                System.Windows.MessageBox.Show("You must Browse for a file.");
                return;
            }
            putImage(txtFile.Text);
        }


        private void CmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (cmdRefresh.Dispatcher.Thread == Thread.CurrentThread)
            {
                //FileStream fileStream =
                //    new FileStream(scannedFileName, FileMode.Open, FileAccess.Read);

                var img = new System.Windows.Media.Imaging.BitmapImage();
                img.BeginInit();
                //img.StreamSource = fileStream;
                img.StreamSource = memory;
                img.EndInit();
                pnlDocument.Source = img;

                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(img));
                    enc.Save(outStream);
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                    bmp = new Bitmap(bitmap);
                }
                //fileStream.Close();
                //fileStream.Dispose();
            }
            else
            {
                cmdRefresh.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                     new EventHandler<RoutedEventArgs>(CmdRefresh_Click), sender, new object[] { e });
            }


        }

        private void lstOriginalDocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstOriginalDocs.SelectedIndex == -1)
                cmdSaveFromDoc.IsEnabled = false;
            else
            {
                cmdSaveFromDoc.IsEnabled = true;
                cmdFileSave.IsEnabled = false;
                cmdScanSave.IsEnabled = false;
            }
        }
    }

    public class ImageContainer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        BitmapImage m_bmp;

        private void OnPropertyChanged(string info)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public BitmapImage bitmapImage
        {
            get { return m_bmp; }
            set { m_bmp = value;
                OnPropertyChanged("bitmapImage");
            }
        }
    }

}
