using Microsoft.Data.ConnectionUI;
//using Microsoft.Data.ConnectionUI.Sample.VB.Microsoft.Data.ConnectionUI.VB;
using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Printing.Interop;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Collections.Generic;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using PdfSharp;
using System.Drawing.Imaging;

namespace MbcsCentral
{

    public class HelperMethods
	{
        public static System.Windows.Size GetHostingScreenSize(string devicename)
        {
            var hdc = NativeMethods.CreateDC(devicename, "", "", IntPtr.Zero);

            // Returns Height of the screen
            int DESKTOPVERTRES = NativeMethods.GetDeviceCaps(hdc, (int)NativeMethods.DeviceCap.DESKTOPVERTRES);

            // Returns width of the screen
            int DESKTOPHORZRES = NativeMethods.GetDeviceCaps(hdc, (int)NativeMethods.DeviceCap.DESKTOPHORZRES);

            // Uncomment this code, if user wants to get additional data.
            // int VERTRES = NativeMethods.GetDeviceCaps(hdc, (int)DeviceCap.VERTRES);
            // int LOGPIXELSY = NativeMethods.GetDeviceCaps(hdc, (int)DeviceCap.LOGPIXELSY);
            // int HORZRES = NativeMethods.GetDeviceCaps(hdc, (int)DeviceCap.HORZRES);
            // LogMessage(string.Format(string.Format("VERTRES : {0}, DESKTOPVERTRES: {1}, LOGPIXELSY: {2}, HORZRES: {3}, DESKTOPHORZRES: {4}",
            //    VERTRES,
            //    DESKTOPVERTRES,
            //    LOGPIXELSY,
            //    HORZRES,
            //    DESKTOPHORZRES));

            return new System.Windows.Size(DESKTOPHORZRES, DESKTOPVERTRES);
        }


        public static void ReLogin()
        {
            Users user = MbcsCentral.GlobalShared.user;
            string userName = GlobalShared.userName;
            string password = GlobalShared.password;
            MbcsCentral.GlobalShared.user = null;
            while (MbcsCentral.GlobalShared.user == null)
            {
                frmLogin frm = new frmLogin();
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                frm.ShowDialog();
                if ((MbcsCentral.GlobalShared.userName == null || MbcsCentral.GlobalShared.userName.Trim() == "") && (MbcsCentral.GlobalShared.password == null || MbcsCentral.GlobalShared.password.Trim() == ""))
                {
                    System.Windows.MessageBox.Show("Login unsuccessful.");
                    GlobalShared.userName = userName;
                    GlobalShared.password = password;
                    GlobalShared.user = user;
                    return;
                }
                try
                {
                    MbcsCentral.GlobalShared.user = Manager.setUser(MbcsCentral.GlobalShared.userName, MbcsCentral.GlobalShared.password);
                }
                catch (System.Exception)
                {
                }
                if (MbcsCentral.GlobalShared.user == null)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Warning, "User <" + MbcsCentral.GlobalShared.userName + "> could not be logged in.", false);
                    Accounts[] accts = Accounts.load();
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "Current Accounts:", false);
                    if (accts != null)
                    {
                        foreach (Accounts acct in accts)
                        {
                            GlobalShared.Log.Log((int)LogClass.logType.Info, "   " + acct.login, false);
                        }
                    }
                }
            }
            GlobalShared.Log.Username = GlobalShared.userName;
            GlobalShared.Log.Log((int)LogClass.logType.Config, "User <" + GlobalShared.userName + "> logged in.", false);
            Main.setMainWindowTitle();

        }

        /* Make this a separate application - 2014-04-01
        public static void SetDbConnection()
		{

			DataConnectionDialog dcd = new DataConnectionDialog();
			DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
			dcs.LoadConfiguration(dcd);

			dcd.ConnectionString = MbcsCentral.GlobalShared.SqlConnectionString;

			if (DataConnectionDialog.Show(dcd) == DialogResult.OK) {
				// load tables 
				using (SqlConnection connection = new SqlConnection(dcd.ConnectionString)) {
					connection.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables", connection);

					using (SqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							Console.WriteLine(reader.HasRows);
						}
						MbcsCentral.GlobalShared.SqlConnectionString = dcd.ConnectionString;
					}
					connection.Close();
				}
				Main.parms.setItem("connectionString", MbcsCentral.GlobalShared.SqlConnectionString);
				MbcsCentral.GlobalShared.CN.ConnectionString = MbcsCentral.GlobalShared.SqlConnectionString;
				Main.mSession.setParam("CN", MbcsCentral.GlobalShared.CN);
				Main.mSession.setParam("connectionString", MbcsCentral.GlobalShared.SqlConnectionString);
				Main.setMainWindowTitle();
			}
			dcs.SaveConfiguration(dcd);
		}
        */


        public static void SetMenuFont()
		{
			FontDialog d = new FontDialog();

			Font f = new Font(MbcsCentral.GlobalShared.menuFontFamliy, (float)MbcsCentral.GlobalShared.menuFontSize);
			d.Font = f;
			d.AllowVectorFonts = false;
			d.AllowVerticalFonts = false;
			try {
				if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					f = d.Font;
					System.Windows.Media.FontFamily mf = new System.Windows.Media.FontFamily(f.FontFamily.ToString());
					Main.mainMenu.FontFamily = mf;
					Main.mainMenu.FontSize = f.SizeInPoints;
					Main.mainMenu.Height = Main.mainMenu.FontSize + 6;
					MbcsCentral.GlobalShared.menuFontFamliy = f.FontFamily.ToString();
					MbcsCentral.GlobalShared.menuFontSize = f.SizeInPoints;
					Main.parms.setItem("MenuFontSize", MbcsCentral.GlobalShared.menuFontSize.ToString());
					Main.parms.setItem("MenuFontFamily", MbcsCentral.GlobalShared.menuFontFamliy);
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox("Could not use chosen font.");
			}

		}

        public static  PdfSharp.Pdf.PdfDocument BitmapToPDF(Bitmap bmp)
        {
            PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
            PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage();

            PdfSharp.Drawing.XImage img = PdfSharp.Drawing.XImage.FromBitmapSource(BitmapToImage(bmp));

            pdf.Pages.Add(page);

            PdfSharp.Drawing.XGraphics xgr = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0]);
            xgr.DrawImage(img, 0, 0);

            return pdf;

        }

        public static System.Windows.Media.Imaging.BitmapSource BitmapToImage(Bitmap bmp)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public static int CharToAscii(char c)
        {
            return (int)c;
        }

        public static char IncrementAlphaNumeric(char c)
        {
            int i = (int)c + 1;

            if (i < 48 || i > 90)
                i = 48;

            if (i > 57 && i < 65)
                i = 65;

            if (i > 90)
                i = 48;
            
            return (char)i;
        }
	}

    /// <summary>
    /// Replacement class for System.Windows.PrintDialog
    /// </summary>
    public class PrintDialogx
    {
        #region Fields

        double mPrintableHeight;
        double mPrintableWidth;

        bool mHeightUpdated = false;
        bool mWidthUpdated = false;

        PrintQueue mPrintQueue = null;
        PrintTicket mPrintTicket = null;

        PageRangeSelection mPageRangeSelection = PageRangeSelection.AllPages;
        PageRange mPageRange;

        List<string> ranges = new List<string>();

        #endregion

        #region Private Methods
        /// <summary>
        /// Ensure Queue and Ticket prepared
        /// </summary>
        private void VerifyPrintSettings()
        {
            if (mPrintQueue == null)
                mPrintQueue = DefaultPrintQueue();

            if (mPrintTicket == null)
                mPrintTicket = DefaultPrintTicket();
        }

        /// <summary>
        /// PrintQueue
        /// </summary>
        /// <returns></returns>
        private PrintQueue DefaultPrintQueue()
        {
            new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();
            PrintQueue queue = null;

            try
            {
                queue = new LocalPrintServer().DefaultPrintQueue;
            }
            catch (PrintSystemException)
            {
                queue = null;
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }

            return queue;
        }

        /// <summary>
        /// PrintTicket
        /// </summary>
        /// <param name="printQueue"></param>
        /// <returns></returns>
        private PrintTicket DefaultPrintTicket()
        {
            new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();
            PrintTicket ticket = null;

            try
            {
                if (mPrintQueue != null)
                {
                    ticket = mPrintQueue.UserPrintTicket;
                    if (ticket == null)
                        ticket = mPrintQueue.DefaultPrintTicket;
                }
            }
            catch (PrintSystemException)
            {
                ticket = null;
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }

            if (ticket == null)
                ticket = new PrintTicket();

            return ticket;
        }

        /// <summary>
        /// Set Print Area
        /// </summary>
        private void UpdateArea()
        {
            VerifyPrintSettings();

            PrintCapabilities caps = null;

            if (mPrintQueue != null)
                caps = mPrintQueue.GetPrintCapabilities(mPrintTicket);

            if (((caps != null) && caps.OrientedPageMediaWidth.HasValue) && caps.OrientedPageMediaHeight.HasValue)
            {
                mPrintableWidth = caps.OrientedPageMediaWidth.Value;
                mPrintableHeight = caps.OrientedPageMediaHeight.Value;
            }
            else
            {
                mPrintableWidth = 816.0;
                mPrintableHeight = 1056.0;

                if (((mPrintTicket.PageMediaSize != null) && mPrintTicket.PageMediaSize.Width.HasValue) && mPrintTicket.PageMediaSize.Height.HasValue)
                {
                    mPrintableWidth = mPrintTicket.PageMediaSize.Width.Value;
                    mPrintableHeight = mPrintTicket.PageMediaSize.Height.Value;
                }

                if (mPrintTicket.PageOrientation.HasValue)
                {
                    if (mPrintTicket.PageOrientation.Value == System.Printing.PageOrientation.Landscape || mPrintTicket.PageOrientation.Value == System.Printing.PageOrientation.ReverseLandscape)
                    {
                        double swap = mPrintableWidth;
                        mPrintableWidth = mPrintableHeight;
                        mPrintableHeight = swap;
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        private System.Drawing.Image localImage;

        public System.Drawing.Image RenderTargetBitmapExample(Visual window)
        {

            RenderTargetBitmap bmp = new RenderTargetBitmap(1300, 790, 120, 96, PixelFormats.Pbgra32);
            bmp.Render(window);
            PngBitmapEncoder image = new PngBitmapEncoder();
            image.Frames.Add(BitmapFrame.Create(bmp));
            //System.IO.MemoryStream fs = new System.IO.MemoryStream();
            // image.Save(fs);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            //fs.Close();
            string filename = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
            System.IO.FileStream fileStream = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None, 4096, System.IO.FileOptions.DeleteOnClose);
            image.Save(fileStream);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fileStream);
            fileStream.Close();
            return img;
        }
        public void PrintWindow(System.Drawing.Image window)
        {
            localImage = window;
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += PrintPage;
            pd.Print();
        }
        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = localImage;
            System.Drawing.Point loc = new System.Drawing.Point(0, 0);
            e.Graphics.DrawImage(img, loc);
        }

        /// <summary>
        /// Actually print an Xps Document with previously setup params
        /// </summary>
        /// <param name="paginator">Document to print</param>
        /// <param name="description">Description</param>
        public void PrintDocument(DocumentPaginator paginator, string description)
        {
            if (paginator == null)
                throw new ArgumentNullException("paginator", "No DocumentPaginator to print");

            VerifyPrintSettings();

            //Handle XPS ourself, as their document writer hates our thread
            if (mPrintQueue.FullName.Contains("XPS"))
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "Xps Document (*.xps) | *.xps";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    XpsDocument document = new XpsDocument(sfd.FileName, System.IO.FileAccess.Write);
                    XpsPackagingPolicy packagePolicy = new XpsPackagingPolicy(document);
                    XpsSerializationManager serializationMgr = new XpsSerializationManager(packagePolicy, false);
                    serializationMgr.SaveAsXaml(paginator);

                    document.Close();
                }

                return;
            }

            XpsDocumentWriter writer = null;
            new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();

            try
            {
                mPrintQueue.CurrentJobSettings.Description = description;
                writer = PrintQueue.CreateXpsDocumentWriter(mPrintQueue);
             
                TicketEventHandler handler = new TicketEventHandler(mPrintTicket);
                writer.WritingPrintTicketRequired += new WritingPrintTicketRequiredEventHandler(handler.SetPrintTicket);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }

            if (ranges == null || ranges.Count == 0)
                writer.Write(paginator);
            else
            {
                int startPage = 0;
                int endPage = 0;
                foreach(string range in ranges)
                {
                    if (range.Contains("-"))
                    {
                        string[] s = range.Split('-');
                        startPage = 0;
                        int.TryParse(s[0], out startPage);
                        if (startPage == 0)
                            continue;
                        int.TryParse(s[1], out endPage);
                        if (endPage == 0)
                            continue;
                    }
                    else
                    {
                        startPage = 0;
                        int.TryParse(range, out startPage);
                        if (startPage == 0)
                        continue;
                        endPage = startPage;
                    }
                    DocumentPaginator pages = new PageRangeDocumentPaginator(paginator, new PageRange(startPage, endPage));
                    if (pages != null)
                    {
                        writer = PrintQueue.CreateXpsDocumentWriter(mPrintQueue);
                        TicketEventHandler handler = new TicketEventHandler(mPrintTicket);
                        writer.WritingPrintTicketRequired += new WritingPrintTicketRequiredEventHandler(handler.SetPrintTicket);

                        writer.Write(pages);
                    }
                }
            }

            //Reset
            mPrintableWidth = 0.0;
            mPrintableHeight = 0.0;
            mWidthUpdated = false;
            mHeightUpdated = false;
        }

        /// <summary>
        /// Show Dialog to allow user to adjust printer/settings
        /// </summary>
        /// <param name="window">Window owner</param>
        /// <returns>True is user was ready to print</returns>
        public bool ShowDialog(Window window)
        {
            NativePrintDialog dlg = new NativePrintDialog()
            {
                PrintTicket = mPrintTicket,
                PrintQueue = mPrintQueue,
                MinPage = 1,
                MaxPage = 200,
                PageRangeEnabled = false,
                PageRange = new PageRange(Math.Max(1, mPageRange.PageFrom), Math.Min(200, mPageRange.PageTo)),
                PageRangeSelection = mPageRangeSelection
            };

            uint result = dlg.ShowDialog(window);
            if (result == 1 || result == 2)
            {
                mPrintQueue = dlg.PrintQueue;
                mPrintTicket = dlg.PrintTicket;
                mPageRange = dlg.PageRange;
                mPageRangeSelection = dlg.PageRangeSelection;
            }

            return (result == 1);
        }

        public void AddRange(string range)
        {
            ranges.Add(range);
        }

        #endregion

        #region Properties

        public PageRange PageRange
        {
            get { return mPageRange; }
            set
            {
                if (value.PageTo <= 0 || value.PageFrom <= 0)
                    throw new ArgumentException("PageRange", "PageRange is not valid.");

                mPageRange = value;

                //Switch around if needed
                if (mPageRange.PageFrom > mPageRange.PageTo)
                {
                    int swap = mPageRange.PageFrom;
                    mPageRange.PageFrom = mPageRange.PageTo;
                    mPageRange.PageTo = swap;
                }
            }
        }

        public PageRangeSelection PageRangeSelection
        {
            get { return mPageRangeSelection; }
            set { mPageRangeSelection = value; }
        }

        public double PrintableAreaHeight
        {
            get
            {
                if ((!mWidthUpdated && !mHeightUpdated) || (!mWidthUpdated && mHeightUpdated))
                {
                    mWidthUpdated = false;
                    mHeightUpdated = true;
                    UpdateArea();
                }

                return mPrintableHeight;
            }
        }

        public double PrintableAreaWidth
        {
            get
            {
                if ((!mWidthUpdated && !mHeightUpdated) || (mWidthUpdated && !mHeightUpdated))
                {
                    mWidthUpdated = true;
                    mHeightUpdated = false;
                    UpdateArea();
                }

                return mPrintableWidth;
            }
        }

        public PrintQueue PrintQueue
        {
            get
            {
                VerifyPrintSettings();
                return mPrintQueue;
            }
            set { mPrintQueue = value; }
        }

        public PrintTicket PrintTicket
        {
            get
            {
                VerifyPrintSettings();
                return mPrintTicket;
            }
            set { mPrintTicket = value; }
        }

        #endregion

        #region Internal
        class TicketEventHandler
        {
            private PrintTicket mPrintTicket;

            public TicketEventHandler(PrintTicket printTicket)
            {
                mPrintTicket = printTicket;
            }

            public void SetPrintTicket(object sender, WritingPrintTicketRequiredEventArgs args)
            {
                if (args.CurrentPrintTicketLevel == PrintTicketLevel.FixedDocumentSequencePrintTicket)
                    args.CurrentPrintTicket = mPrintTicket;
            }
        }
        #endregion
    }

    /// <summary>
    /// Native Printing class wrapper. Calls Window's PrinatDlgEx
    /// </summary>
    internal class NativePrintDialog
    {
        #region Fields
        private PrintQueue mPrintQueue = null;
        private PrintTicket mPrintTicket = null;
        private uint mMaxPage = 0x270f;
        private uint mMinPage = 1;
        private PageRange mPageRange;
        private bool mPageRangeEnabled;
        private PageRangeSelection mPageRangeSelection = PageRangeSelection.AllPages;
        #endregion

        #region Public Methods
        /// <summary>
        /// Show Dialog
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        internal uint ShowDialog(Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            using (PrintDlgEx dlg = new PrintDlgEx(helper.Handle, this))
            {
                return dlg.ShowPrintDlgEx();
            }
        }

        #endregion

        #region Properties
        internal PrintQueue PrintQueue
        {
            get { return mPrintQueue; }
            set { mPrintQueue = value; }
        }

        internal PrintTicket PrintTicket
        {
            get { return mPrintTicket; }
            set { mPrintTicket = value; }
        }

        internal uint MinPage
        {
            get { return mMinPage; }
            set { mMinPage = value; }
        }

        internal uint MaxPage
        {
            get { return mMaxPage; }
            set { mMaxPage = value; }
        }

        internal bool PageRangeEnabled
        {
            get { return mPageRangeEnabled; }
            set { mPageRangeEnabled = value; }
        }

        internal PageRange PageRange
        {
            get { return mPageRange; }
            set { mPageRange = value; }
        }

        internal PageRangeSelection PageRangeSelection
        {
            get { return mPageRangeSelection; }
            set { mPageRangeSelection = value; }
        }

        #endregion

        #region Native Class Wrapper
        class PrintDlgEx : IDisposable
        {
            #region Fields
            private IntPtr mPrintDlgExHnd;
            private NativePrintDialog mDialogOwner;
            private IntPtr mWinHandle;
            #endregion

            #region Public/Internal Methods
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="owner">The Owner Handle we will attach to</param>
            /// <param name="dialog"></param>
            internal PrintDlgEx(IntPtr owner, NativePrintDialog dialog)
            {
                mWinHandle = owner;
                mDialogOwner = dialog;
                //GlobalShared.Log.Log((int)LogClass.logType.Info,  "About to allocate PrintDlgExStruct", false);
                try
                {
                    mPrintDlgExHnd = AllocatePrintDlgExStruct();
                }
                catch (System.Exception ex)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Here it is:" + ex.Message, false);
                    GlobalShared.Log.debug = false;
                }
            }

            /// <summary>
            /// Show dialog and return result
            /// </summary>
            /// <returns></returns>
            internal uint ShowPrintDlgEx()
            {
                if (NativeMethods.PrintDlgEx(mPrintDlgExHnd) == 0)
                    return GetResult();
                else
                    return 0;
            }

            /// <summary>
            /// Clean up
            /// </summary>
            ~PrintDlgEx()
            {
                //Make sure object is Disposed
                Dispose(true);
            }

            #endregion

            #region Private Methods
            /// <summary>
            /// Helper method to determine CPU mode
            /// </summary>
            /// <returns></returns>
            bool Is64Bits()
            {
                return (Marshal.SizeOf(IntPtr.Zero) == 8);
            }

            /// <summary>
            /// Allocate memory associated with PRINTDLGEX32 or PRINTDLGEX64 structures
            /// </summary>
            IntPtr AllocatePrintDlgExStruct()
            {
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "In AllocatePrintDlgExStruct", false);
                NativeMethods.PRINTPAGERANGE pageRange;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 1", false);
                IntPtr ptr; // = IntPtr.Zero;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 2", false);
                pageRange.nToPage = (uint)mDialogOwner.PageRange.PageTo;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 3", false);
                pageRange.nFromPage = (uint)mDialogOwner.PageRange.PageFrom;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 4", false);


                try
                {
                    ptr = IntPtr.Zero;
                    //Handle 32 bit case first
                    if (!Is64Bits())
                    {
                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 4a", false);
                        NativeMethods.PRINTDLGEX32 pDlg = new NativeMethods.PRINTDLGEX32();
                        pDlg.hwndOwner = mWinHandle;
                        pDlg.nMinPage = mDialogOwner.MinPage;
                        pDlg.nMaxPage = mDialogOwner.MaxPage;
                        pDlg.Flags = 0x9c0004;

                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 5", false);
                        if (mDialogOwner.PageRangeEnabled)
                        {
                            //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 6", false);
                            pDlg.lpPageRanges = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.PRINTPAGERANGE)));
                            pDlg.nMaxPageRanges = 1;

                            //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 7", false);
                            if (mDialogOwner.PageRangeSelection == PageRangeSelection.UserPages)
                            {
                               // GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 8", false);
                                pDlg.nPageRanges = 1;
                                Marshal.StructureToPtr(pageRange, pDlg.lpPageRanges, false);
                                pDlg.Flags |= 2;
                                //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 9", false);
                            }
                            else
                            {
                                pDlg.nPageRanges = 0;
                            }
                        }
                        else
                        {
                            pDlg.lpPageRanges = IntPtr.Zero;
                            pDlg.nMaxPageRanges = 0;
                            pDlg.Flags |= 8;
                        }

                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 10", false);
                        if (mDialogOwner.PrintQueue != null)
                        {
                            //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 11", false);
                            pDlg.hDevNames = InitializeDevNames(mDialogOwner.PrintQueue.FullName);
                            if (mDialogOwner.PrintTicket != null)
                                pDlg.hDevMode = InitializeDevMode(mDialogOwner.PrintQueue.FullName, mDialogOwner.PrintTicket);
                        }

                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 12", false);
                        ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.PRINTDLGEX32)));
                        Marshal.StructureToPtr(pDlg, ptr, false);
                        return ptr;
                    }

                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 4b", false);
                    //Go with 64 bit structure
                    NativeMethods.PRINTDLGEX64 pDlg64 = new NativeMethods.PRINTDLGEX64();
                    pDlg64.hwndOwner = mWinHandle;
                    pDlg64.nMinPage = mDialogOwner.MinPage;
                    pDlg64.nMaxPage = mDialogOwner.MaxPage;
                    pDlg64.Flags = 0x9c0004;

                   // GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 5", false);
                    if (mDialogOwner.PageRangeEnabled)
                    {
                        pDlg64.lpPageRanges = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.PRINTPAGERANGE)));
                        pDlg64.nMaxPageRanges = 1;

                        if (mDialogOwner.PageRangeSelection == PageRangeSelection.UserPages)
                        {
                            pDlg64.nPageRanges = 1;
                            Marshal.StructureToPtr(pageRange, pDlg64.lpPageRanges, false);
                            pDlg64.Flags |= 2;
                        }
                        else
                        {
                            pDlg64.nPageRanges = 0;
                        }
                    }
                    else
                    {
                        pDlg64.lpPageRanges = IntPtr.Zero;
                        pDlg64.nMaxPageRanges = 0;
                        pDlg64.Flags |= 8;
                    }
                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 6", false);

                    if (mDialogOwner.PrintQueue != null)
                    {
                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 7", false);
                        pDlg64.hDevNames = InitializeDevNames(mDialogOwner.PrintQueue.FullName);

                        if (mDialogOwner.PrintTicket != null)
                            pDlg64.hDevMode = InitializeDevMode(mDialogOwner.PrintQueue.FullName, mDialogOwner.PrintTicket);
                    }
                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 8", false);

                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.PRINTDLGEX64)));
                    Marshal.StructureToPtr(pDlg64, ptr, false);
                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "AllocatePrintDlgExStruct - 9", false);
                    return ptr;
                }
                catch (Exception)
                {   //Free buffer on error
                    ptr = IntPtr.Zero;
                    DeallocatePrintDlgExStruct(ptr);
                    throw;
                }
            }

            /// <summary>
            /// Frees memory associated with PRINTDLGEX32 or PRINTDLGEX64 structures
            /// </summary>
            /// <param name="ptr"></param>
            void DeallocatePrintDlgExStruct(IntPtr ptr)
            {
                if (ptr != IntPtr.Zero)
                {
                    IntPtr hMode = IntPtr.Zero;
                    IntPtr hNames = IntPtr.Zero;
                    IntPtr hPageRanges = IntPtr.Zero;

                    if (Is64Bits())
                    {
                        NativeMethods.PRINTDLGEX64 pDlg = (NativeMethods.PRINTDLGEX64)Marshal.PtrToStructure(ptr, typeof(NativeMethods.PRINTDLGEX64));
                        hMode = pDlg.hDevMode;
                        hNames = pDlg.hDevNames;
                        hPageRanges = pDlg.lpPageRanges;
                    }
                    else
                    {
                        NativeMethods.PRINTDLGEX32 pDlg = (NativeMethods.PRINTDLGEX32)Marshal.PtrToStructure(ptr, typeof(NativeMethods.PRINTDLGEX32));
                        hMode = pDlg.hDevMode;
                        hNames = pDlg.hDevNames;
                        hPageRanges = pDlg.lpPageRanges;
                    }

                    if (hMode != IntPtr.Zero)
                        NativeMethods.GlobalFree(hMode);

                    if (hNames != IntPtr.Zero)
                        NativeMethods.GlobalFree(hNames);

                    if (hPageRanges != IntPtr.Zero)
                        NativeMethods.GlobalFree(hPageRanges);

                    Marshal.FreeHGlobal(ptr);
                }
            }

            /// <summary>
            /// Get the PrintQueue from the PrinterName
            /// </summary>
            /// <param name="printerName"></param>
            /// <returns></returns>
            PrintQueue FindPrintQueue(string printerName)
            {
                EnumeratedPrintQueueTypes[] flag = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections };
                PrintQueueIndexedProperty[] pArray = new PrintQueueIndexedProperty[2];
                pArray[1] = PrintQueueIndexedProperty.QueueAttributes;

                new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();

                try
                {
                    using (LocalPrintServer s = new LocalPrintServer())
                    {
                        foreach (PrintQueue q in s.GetPrintQueues(pArray, flag))
                        {
                            if (printerName.Equals(q.FullName, StringComparison.OrdinalIgnoreCase))
                            {
                                q.InPartialTrust = true;
                                return q;
                            }
                        }
                    }
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }

                return null;
            }

            /// <summary>
            /// Find the PrintTicket for the queue
            /// </summary>
            /// <param name="dModeHnd"></param>
            /// <param name="printQueueName"></param>
            /// <returns></returns>
            PrintTicket FindPrintTicket(IntPtr dModeHnd, string printQueueName)
            {
                byte[] dModeBytes = null;
                IntPtr ptr = IntPtr.Zero;

                try
                {
                    //Convert the native DevMode to a managed array of bytes
                    ptr = NativeMethods.GlobalLock(dModeHnd);
                    NativeMethods.DEVMODE dmode = (NativeMethods.DEVMODE)Marshal.PtrToStructure(ptr, typeof(NativeMethods.DEVMODE));
                    dModeBytes = new byte[dmode.dmSize + dmode.dmDriverExtra];
                    Marshal.Copy(ptr, dModeBytes, 0, dModeBytes.Length);
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                        NativeMethods.GlobalUnlock(dModeHnd);
                }

                new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();

                try
                {
                    //Convert the bytes to a native PrintTicket
                    using (PrintTicketConverter converter = new PrintTicketConverter(printQueueName, PrintTicketConverter.MaxPrintSchemaVersion))
                    {
                        return converter.ConvertDevModeToPrintTicket(dModeBytes);
                    }
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }

            /// <summary>
            /// Creates DevMode structure from Ticket
            /// </summary>
            /// <param name="printerName"></param>
            /// <param name="printTicket"></param>
            /// <returns></returns>
            IntPtr InitializeDevMode(string printerName, PrintTicket printTicket)
            {
                new PrintingPermission(PrintingPermissionLevel.DefaultPrinting).Assert();
                byte[] dMode = null;

                try
                {
                    using (PrintTicketConverter converter = new PrintTicketConverter(printerName, PrintTicketConverter.MaxPrintSchemaVersion))
                    {
                        dMode = converter.ConvertPrintTicketToDevMode(printTicket, BaseDevModeType.UserDefault);
                    }
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }

                IntPtr ptrDevMode = Marshal.AllocHGlobal(dMode.Length);
                Marshal.Copy(dMode, 0, ptrDevMode, dMode.Length);
                return ptrDevMode;
            }

            /// <summary>
            /// Set DevName structure memory
            /// </summary>
            /// <param name="printerName"></param>
            /// <returns></returns>
            IntPtr InitializeDevNames(string printerName)
            {
                IntPtr ptrDevModeNames = IntPtr.Zero;
                char[] strPrintName = printerName.ToCharArray();

                //Allocate native mem
                ptrDevModeNames = Marshal.AllocHGlobal(((strPrintName.Length + 3) * Marshal.SystemDefaultCharSize) + Marshal.SizeOf(typeof(NativeMethods.DEVNAMES)));

                ushort sizeOfDevNames = (ushort)Marshal.SizeOf(typeof(NativeMethods.DEVNAMES));

                //Setup structure
                NativeMethods.DEVNAMES names;
                names.wDeviceOffset = (ushort)(sizeOfDevNames / Marshal.SystemDefaultCharSize);
                names.wDriverOffset = (ushort)((names.wDeviceOffset + strPrintName.Length) + 1);
                names.wOutputOffset = (ushort)(names.wDriverOffset + 1);
                names.wDefault = 0;

                //Convert to native
                Marshal.StructureToPtr(names, ptrDevModeNames, false);
                IntPtr dst = (IntPtr)(((long)ptrDevModeNames) + sizeOfDevNames);
                IntPtr dstOffset = (IntPtr)(((long)dst) + (strPrintName.Length * Marshal.SystemDefaultCharSize));

                byte[] array = new byte[3 * Marshal.SystemDefaultCharSize];
                Array.Clear(array, 0, array.Length);

                //Copy strings
                Marshal.Copy(strPrintName, 0, dst, strPrintName.Length);
                Marshal.Copy(array, 0, dstOffset, array.Length);

                return ptrDevModeNames;
            }

            /// <summary>
            /// Get dwResultAction from Dlg
            /// </summary>
            /// <param name="ptrPrintDlg"></param>
            /// <returns></returns>
            uint GetResultPrintDlgExHnd(IntPtr ptrPrintDlg)
            {
                if (Is64Bits())
                {
                    NativeMethods.PRINTDLGEX64 dlg = (NativeMethods.PRINTDLGEX64)Marshal.PtrToStructure(ptrPrintDlg, typeof(NativeMethods.PRINTDLGEX64));
                    return dlg.dwResultAction;
                }
                else
                {
                    NativeMethods.PRINTDLGEX32 dlg = (NativeMethods.PRINTDLGEX32)Marshal.PtrToStructure(ptrPrintDlg, typeof(NativeMethods.PRINTDLGEX32));
                    return dlg.dwResultAction;
                }
            }

            /// <summary>
            /// Get various settings
            /// </summary>
            /// <param name="nativeBuffer"></param>
            /// <param name="printerName"></param>
            /// <param name="flags"></param>
            /// <param name="pageRange"></param>
            /// <param name="dModeHnd"></param>
            void GetSettings(IntPtr nativeBuffer, out string printerName, out uint flags, out PageRange pageRange, out IntPtr dModeHnd)
            {
                IntPtr dNames = IntPtr.Zero;
                IntPtr pRanges = IntPtr.Zero;

                if (Is64Bits())
                {
                    NativeMethods.PRINTDLGEX64 dlg = (NativeMethods.PRINTDLGEX64)Marshal.PtrToStructure(nativeBuffer, typeof(NativeMethods.PRINTDLGEX64));
                    dModeHnd = dlg.hDevMode;
                    dNames = dlg.hDevNames;
                    flags = dlg.Flags;
                    pRanges = dlg.lpPageRanges;
                }
                else
                {
                    NativeMethods.PRINTDLGEX32 dlg = (NativeMethods.PRINTDLGEX32)Marshal.PtrToStructure(nativeBuffer, typeof(NativeMethods.PRINTDLGEX32));
                    dModeHnd = dlg.hDevMode;
                    dNames = dlg.hDevNames;
                    flags = dlg.Flags;
                    pRanges = dlg.lpPageRanges;
                }

                if (((flags & 2) == 2) && (pRanges != IntPtr.Zero))
                {
                    NativeMethods.PRINTPAGERANGE printRange = (NativeMethods.PRINTPAGERANGE)Marshal.PtrToStructure(pRanges, typeof(NativeMethods.PRINTPAGERANGE));
                    pageRange = new PageRange((int)printRange.nFromPage, (int)printRange.nToPage);
                }
                else
                {
                    pageRange = new PageRange(1);
                }

                if (dNames != IntPtr.Zero)
                {
                    IntPtr ptrDevNames = IntPtr.Zero;
                    try
                    {
                        ptrDevNames = NativeMethods.GlobalLock(dNames);
                        NativeMethods.DEVNAMES devnames = (NativeMethods.DEVNAMES)Marshal.PtrToStructure(ptrDevNames, typeof(NativeMethods.DEVNAMES));
                        printerName = Marshal.PtrToStringAuto((IntPtr)(((int)ptrDevNames) + (devnames.wDeviceOffset * Marshal.SystemDefaultCharSize)));
                    }
                    finally
                    {
                        if (ptrDevNames != IntPtr.Zero)
                            NativeMethods.GlobalUnlock(dNames);
                    }
                }
                else
                {
                    printerName = string.Empty;
                }
            }

            /// <summary>
            /// Get's the result from the dialog
            /// </summary>
            /// <returns></returns>
            uint GetResult()
            {
                if (mPrintDlgExHnd == IntPtr.Zero)
                    return 0;

                uint result = GetResultPrintDlgExHnd(mPrintDlgExHnd);
                if (result == 1 || result == 2)
                {
                    IntPtr dModePtr;
                    string printerName;
                    uint flags;
                    PageRange range;

                    GetSettings(mPrintDlgExHnd, out printerName, out flags, out range, out dModePtr);

                    mDialogOwner.PrintQueue = FindPrintQueue(printerName);
                    mDialogOwner.PrintTicket = FindPrintTicket(dModePtr, printerName);

                    if ((flags & 2) == 2)
                    {
                        if (range.PageFrom > range.PageTo)
                        {
                            int pageTo = range.PageTo;
                            range.PageTo = range.PageFrom;
                            range.PageFrom = pageTo;
                        }

                        mDialogOwner.PageRangeSelection = PageRangeSelection.UserPages;
                        mDialogOwner.PageRange = range;

                        return result;
                    }

                    mDialogOwner.PageRangeSelection = PageRangeSelection.AllPages;
                }

                return result;
            }

            #endregion

            #region IDisposable
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposing && mPrintDlgExHnd != IntPtr.Zero)
                {
                    DeallocatePrintDlgExStruct(mPrintDlgExHnd);
                    mPrintDlgExHnd = IntPtr.Zero;
                }
            }
            #endregion
        }
        #endregion
    }

    /// Encapsulates a DocumentPaginator and allows
    /// to paginate just some specific pages (a "PageRange")
    /// of the encapsulated DocumentPaginator
    ///  (c) Thomas Claudius Huber 2010 
    ///      http://www.thomasclaudiushuber.com
    /// 

    public class PageRangeDocumentPaginator : DocumentPaginator
    {
        private int _startIndex;
        private int _endIndex;
        private DocumentPaginator _paginator;
        public PageRangeDocumentPaginator(
          DocumentPaginator paginator,
          PageRange pageRange)
        {
            _startIndex = pageRange.PageFrom - 1;
            _endIndex = pageRange.PageTo - 1;
            _paginator = paginator;

            // Adjust the _endIndex
            _endIndex = Math.Min(_endIndex, _paginator.PageCount - 1);
        }

        /*
        public override DocumentPage GetPage(int pageNumber)
        {
            // Just return the page from the original
            // paginator by using the "startIndex"
            return _paginator.GetPage(pageNumber + _startIndex);
        }
        */

        public override DocumentPage GetPage(int pageNumber)
        {
            var page = _paginator.GetPage(pageNumber + _startIndex);

            // Create a new ContainerVisual as a new parent for page children
            var cv = new System.Windows.Media.ContainerVisual();
            if (page.Visual is FixedPage)
            {
                foreach (var child in ((FixedPage)page.Visual).Children)
                {
                    // Make a shallow clone of the child using reflection
                    var childClone = (UIElement)child.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(child, null);

                    // Setting the parent of the cloned child to the created ContainerVisual by using Reflection.
                    // WARNING: If we use Add and Remove methods on the FixedPage.Children, for some reason it will
                    //          throw an exception concerning event handlers after the printing job has finished.
                    var parentField = childClone.GetType().GetField("_parent", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    if (parentField != null)
                    {
                        parentField.SetValue(childClone, null);
                        cv.Children.Add(childClone);
                    }
                }

                return new DocumentPage(cv, page.Size, page.BleedBox, page.ContentBox);
            }

            return page;
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get
            {
                if (_startIndex > _paginator.PageCount - 1)
                    return 0;
                if (_startIndex > _endIndex)
                    return 0;

                return _endIndex - _startIndex + 1;
            }
        }

        public override System.Windows.Size PageSize
        {
            get { return _paginator.PageSize; }
            set { _paginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return _paginator.Source; }
        }

    }

    #region Native Method Wrappers
    /// <summary>
    /// Wrap PInvoke calls  structs
    /// </summary>
    internal static class NativeMethods
    {
        #region Native Method Wrappers

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalFree(IntPtr hMem);

        [SuppressUnmanagedCodeSecurity, SecurityCritical, DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        [SuppressUnmanagedCodeSecurity, SecurityCritical, DllImport("kernel32.dll")]
        internal static extern bool GlobalUnlock(IntPtr hMem);

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
        internal static extern int PrintDlgEx(IntPtr pdex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, IntPtr devMode);


        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        #endregion

        #region Native Type Wrappers

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        internal struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        internal struct DEVNAMES
        {
            public ushort wDriverOffset;
            public ushort wDeviceOffset;
            public ushort wOutputOffset;
            public ushort wDefault;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        internal class PRINTDLGEX32 : IDisposable
        {
            public int lStructSize = Marshal.SizeOf(typeof(NativeMethods.PRINTDLGEX32));
            public IntPtr hwndOwner = IntPtr.Zero;
            public IntPtr hDevMode = IntPtr.Zero;
            public IntPtr hDevNames = IntPtr.Zero;
            public IntPtr hDC = IntPtr.Zero;
            public uint Flags;
            public uint Flags2;
            public uint ExclusionFlags;
            public uint nPageRanges;
            public uint nMaxPageRanges;
            public IntPtr lpPageRanges = IntPtr.Zero;
            public uint nMinPage;
            public uint nMaxPage;
            public uint nCopies;
            public IntPtr hInstance = IntPtr.Zero;
            public IntPtr lpPrintTemplateName = IntPtr.Zero;
            public IntPtr lpCallback = IntPtr.Zero;
            public uint nPropertyPages;
            public IntPtr lphPropertyPages = IntPtr.Zero;
            public uint nStartPage = uint.MaxValue;
            public uint dwResultAction;

            public void Dispose()
            {   
               
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 8)]
        internal class PRINTDLGEX64 : IDisposable
        {
            public int lStructSize = Marshal.SizeOf(typeof(NativeMethods.PRINTDLGEX64));
            public IntPtr hwndOwner = IntPtr.Zero;
            public IntPtr hDevMode = IntPtr.Zero;
            public IntPtr hDevNames = IntPtr.Zero;
            public IntPtr hDC = IntPtr.Zero;
            public uint Flags;
            public uint Flags2;
            public uint ExclusionFlags;
            public uint nPageRanges;
            public uint nMaxPageRanges;
            public IntPtr lpPageRanges = IntPtr.Zero;
            public uint nMinPage;
            public uint nMaxPage;
            public uint nCopies;
            public IntPtr hInstance = IntPtr.Zero;
            public IntPtr lpPrintTemplateName = IntPtr.Zero;
            public IntPtr lpCallback = IntPtr.Zero;
            public uint nPropertyPages;
            public IntPtr lphPropertyPages = IntPtr.Zero;
            public uint nStartPage = uint.MaxValue;
            public uint dwResultAction;

            public void Dispose()
            {
              
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        internal struct PRINTPAGERANGE
        {
            public uint nFromPage;
            public uint nToPage;
        }

        public enum DeviceCap
        {
            HORZRES = 8,
            VERTRES = 10,
            DESKTOPVERTRES = 117,
            LOGPIXELSY = 90,
            DESKTOPHORZRES = 118

        }
        #endregion
    }
    #endregion

}
