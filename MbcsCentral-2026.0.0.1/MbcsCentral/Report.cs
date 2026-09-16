 using System;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Media;
using System.Drawing.Printing;
using Spire.Pdf;
using System.Linq;

namespace MbcsCentral
{
    /// <summary>
    /// This class does the work of creating a FixedDocment,
    /// accepting text lines and controlling the building and
    /// printing of pages.
    /// </summary>
    class Report : IDisposable
    {
        // Constants
        public const double LINE_HEIGHT = (Single) 0.135 * 96; // (Single)0.167 * 96;
        public const Single CHAR_HEIGHT = (Single)0.167 * 96;
        public const Single CHAR_WIDTH = (Single)0.106  * 96;
        public const Single TOP_MARGIN = (Single)0.75 * 96;
        public const Single BOTTOM_MARGIN = (Single)0.75 * 96;
        public const Single LEFT_MARGIN = (Single)0.75 * 96;
        public const Single RIGHT_MARGIN = (Single) 0;
        public const Single MAX_PORTRAIT_HEIGHT = (Single)11 * 96 ;
        public const Single MAX_LANDSCAPE_HEIGHT = (Single)8.5 * 96 ;
        public int LinesPerPage { get; set; } = 0;
        private int ln = 0;
        private int totalLinesPrinted = 0;

        private System.Windows.Media.Color DEFAULT_COLOR = System.Windows.Media.Colors.Black;
        public enum Orientation { Portrait, Landscape };
        public const string PAGE_TAG = "<pg>";
        public const string DATE_TAG = "<date>";

        PrintDialogx pd;
        double CurrentLineWidth;

      // Fields
        private FixedDocument doc;
        private FixedPage page;

        private double m_CurrentHeight = 0;
        private double m_LineHeight = LINE_HEIGHT;
        private Single m_TopMargin = TOP_MARGIN;
        private Single m_BottomMargin = BOTTOM_MARGIN;
        private Single m_LeftMargin = LEFT_MARGIN;
        private Single m_RightMargin = RIGHT_MARGIN;
        private int m_Orientation = (int)Orientation.Portrait;
        private Single m_MaxPageHeight = MAX_PORTRAIT_HEIGHT - TOP_MARGIN - BOTTOM_MARGIN;
        private string m_Title;
        private object[,] m_HeaderLines;
        private int pgNumber = 1;
        private System.Drawing.FontFamily fontFamily;
        private Font font;
        float fontSize;
        bool m_fontBold = false;
        bool m_fontUnderline = false;
        private System.Windows.Media.Color m_fontColor;
        private PrinterQueues _Printers;
        private bool m_duplex = false;
        private bool m_Borderless = false;
        Single pageHeight = MAX_PORTRAIT_HEIGHT;
        Single pageWidth = MAX_LANDSCAPE_HEIGHT;

        // Constructors
        /// <summary>
        /// No-parm constructor
        /// </summary>
        public Report(Single pageWidth = 0)
        { 
            this.doc = new FixedDocument();
            this.doc.DocumentPaginator.PageSize = new System.Windows.Size((double)pageWidth, (double)pageHeight);
            this.page = new FixedPage();
            this.page.Height = pageHeight;
            if (pageWidth != 0)
                this.pageWidth = pageWidth;
            this.page.Width = this.pageWidth;
            this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            m_fontColor = DEFAULT_COLOR;
            //_Printers = new PrinterQueues();
            m_CurrentHeight = (double)m_TopMargin;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Report title for display</param>
        public Report(string title, Single pageWidth = 0)
        {
            this.m_Title=title;
            this.doc = new FixedDocument();
            this.page = new FixedPage();
            this.page.Height = pageHeight;
            if (pageWidth != 0)
                this.pageWidth = pageWidth;
            this.page.Width = this.pageWidth;
            this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            m_fontColor = DEFAULT_COLOR;
            //_Printers = new PrinterQueues();
            m_CurrentHeight = (double)m_TopMargin;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Report display title</param>
        /// <param name="fontName">String literal name of base system font</param>
        /// <param name="fontSize">Size of base font in points</param>
        public Report(string title,string fontName,float fontSize, Single pageWidth = 0)
        {
            this.m_Title = title;
            fontFamily = new System.Drawing.FontFamily(fontName);
            this.fontSize = (float)(fontSize * (96.0 / 72.0));
            font = new Font(fontFamily, fontSize, System.Drawing.FontStyle.Regular);
            this.doc = new FixedDocument();
            this.page = new FixedPage();
            this.page.Height = pageHeight;
            if (pageWidth != 0)
                this.pageWidth = pageWidth;
            this.page.Width = this.pageWidth;
            this.page.Margin = new Thickness((double)m_LeftMargin , (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            m_fontColor = DEFAULT_COLOR;
            //_Printers = new PrinterQueues();
            m_CurrentHeight = (double)m_TopMargin;
        }
        // Methods 

        public void Dispose()
        {
            font.Dispose();
            fontFamily.Dispose();

        }
        /// <summary>
        /// Takes HeaderLines and places them in current page 
        /// applying fonts, if present.
        /// </summary>
        public void PrintHeader()
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            Font fnt;
            string s = "";
            m_CurrentHeight = m_TopMargin;
            for (int i = 0; i < m_HeaderLines.GetLength(0) -1; i++)
            {
                TextBlock txt = new TextBlock();
                s = (string)(m_HeaderLines[i, 0] != null ? (string)m_HeaderLines[i, 0] : "");
                s = s.Replace(PAGE_TAG, pgNumber.ToString()).Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); 
                txt.Text =s;
                if (m_HeaderLines[i, 1] != null)
                    fnt = (Font) m_HeaderLines[i, 1];
                else
                    fnt = font;
                txt.FontFamily = new System.Windows.Media.FontFamily(fnt.FontFamily.Name);
                txt.FontSize = fnt.SizeInPoints;
                if (fnt.Bold)
                    txt.FontWeight = System.Windows.FontWeights.Bold;
                else
                    txt.FontWeight = System.Windows.FontWeights.Regular;
                if (fnt.Underline || m_fontUnderline)
                    txt.TextDecorations = Underline();
                FixedPage.SetTop( txt, m_CurrentHeight);
                FixedPage.SetLeft(txt,m_LeftMargin);
                FixedPage.SetRight(txt,m_RightMargin);
                txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
                page.Children.Add(txt);
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
        }

        /// <summary>
        /// Adds a line to the current fixed page
        /// </summary>
        /// <param name="line">The string contents to be added to current page</param>
        /// <returns>true if maximum lines have been reached</returns>
        public bool PrintPageLine( string line = "", bool linefeed = true)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            TextBlock txt = new TextBlock();
            txt.Width = Double.NaN; 
            string s = line;
            if (line != null) { s = line.Replace(PAGE_TAG, pgNumber.ToString()).Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
            txt.Text = s;
            txt.FontFamily = new System.Windows.Media.FontFamily( font.FontFamily.Name );
            txt.FontSize = fontSize; //  font.SizeInPoints;
            if (font.Bold || m_fontBold)
                txt.FontWeight = System.Windows.FontWeights.Bold;
            else
                txt.FontWeight = System.Windows.FontWeights.Regular;
            if (font.Underline || m_fontUnderline)
            {
                txt.TextDecorations = Underline();
            }
            txt.Foreground = new System.Windows.Media.SolidColorBrush( m_fontColor);
            FixedPage.SetTop( txt, m_CurrentHeight );
            FixedPage.SetLeft(txt,m_LeftMargin + CurrentLineWidth);
            txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            //FixedPage.SetRight(txt,m_RightMargin);
            if (linefeed)
            {
                CurrentLineWidth = 0;
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            else
            {
                txt.Measure(new System.Windows.Size(400,1000));

                System.Drawing.Size proposedSize = new System.Drawing.Size(int.MaxValue, int.MaxValue);
                System.Windows.Forms.TextFormatFlags flags = System.Windows.Forms.TextFormatFlags.NoPadding;
                System.Drawing.Size ressize = System.Windows.Forms.TextRenderer.MeasureText(txt.Text, font, proposedSize, flags);

                //txt.Arrange();
                CurrentLineWidth += (ressize.Width * 0.75);
            }
            page.Children.Add(txt);
            if (LinesPerPage > 0 && ln >= LinesPerPage)
                return true;
            else
                return (bool) (m_CurrentHeight + BottomOffset >= m_MaxPageHeight ? true : false);
       }

       /// <summary>
       /// Adds a line to the current page applying a font
       /// </summary>
       /// <param name="line">The string contents to be added to current page</param>
       /// <param name="fnt">A font object to be applied to the line</param>
       /// <returns></returns>
        public bool PrintPageLine( string line, Font fnt, bool linefeed = true )
       {
           if (PrintFlag && !PrinterChosen)
               ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            TextBlock txt = new TextBlock();
            txt.Width = Double.NaN;
            string s = line;
            if (line != null) { s = line.Replace( PAGE_TAG, pgNumber.ToString() ); }
            if (line != null) { s = line.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
            txt.Text = s;
            if (fnt != null)
            {
                txt.FontFamily = new System.Windows.Media.FontFamily(fnt.FontFamily.Name);
                txt.FontSize = fnt.SizeInPoints;
                if (fnt.Bold)
                    txt.FontWeight = System.Windows.FontWeights.Bold;
                else
                    txt.FontWeight = System.Windows.FontWeights.Regular;
            }
            if (font.Underline || m_fontUnderline)
            {
                txt.TextDecorations = Underline();
            }
            txt.Foreground = new System.Windows.Media.SolidColorBrush(m_fontColor);
            FixedPage.SetTop(txt, m_CurrentHeight);
            FixedPage.SetLeft( txt, m_LeftMargin + CurrentLineWidth );
            FixedPage.SetRight( txt, m_RightMargin );
            txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            page.Children.Add(txt);
            if (linefeed)
            {
                CurrentLineWidth = 0;
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            else
            {
                txt.Measure(new System.Windows.Size(400, 1000));
                CurrentLineWidth += (txt.DesiredSize.Width);
            }
            if (LinesPerPage > 0 && ln >= LinesPerPage)
                return true;
            else
                return (bool) (m_CurrentHeight + BottomOffset >= m_MaxPageHeight ? true : false);
        }

        /// <summary>
        /// Adds a line of text at a precise position
        /// </summary>
        /// <param name="line">The string contents to be added to current page</param>
        /// <param name="left">The horizontal position by line number</param>
        /// <param name="top">The vertical position in characters</param>
        /// <param name="fnt">A font object to be applied to the line</param>
        /// <returns>Always true</returns>
        public bool PrintPageLine(string line, double left, double top, Font fnt, bool linefeed = true)
        {
            double fntSize;
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            TextBlock txt = new TextBlock();
            string s = line;
            if (line != null) { s = line.Replace(PAGE_TAG, pgNumber.ToString()); }
            if (line != null) { s = line.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
            txt.Text = s;
            if (fnt == null)
            {
                fnt = font;
                fntSize = FontSize;
            }
            else
                fntSize = fnt.Size;
            txt.FontFamily = new System.Windows.Media.FontFamily(fnt.FontFamily.Name);
            txt.FontSize = fntSize; // fnt.SizeInPoints;
            if (m_fontBold)
                txt.FontWeight = System.Windows.FontWeights.Bold;
            else
                txt.FontWeight = System.Windows.FontWeights.Regular;
            if (m_fontUnderline)         // font.Underline ||
            {
                txt.TextDecorations = Underline();
            }
            txt.Foreground = new System.Windows.Media.SolidColorBrush(m_fontColor);
            FixedPage.SetTop(txt, top * CHAR_HEIGHT);
            FixedPage.SetLeft(txt, left*CHAR_WIDTH);
            FixedPage.SetRight(txt, m_RightMargin);
            txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            page.Children.Add(txt);
            if (linefeed)
            {
                CurrentLineWidth = 0;
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            else
            {
                txt.Measure(new System.Windows.Size(400, 1000));
                CurrentLineWidth += (txt.DesiredSize.Width);
            }
            if (LinesPerPage > 0 && ln >= LinesPerPage)
                return true;
            else
                return (bool)(m_CurrentHeight + BottomOffset >= m_MaxPageHeight ? true : false);
        }

        /// <summary>
        /// Adds a line of text at a precise position
        /// </summary>
        /// <param name="line">The string contents to be added to current page</param>
        /// <param name="left">The horizontal position in inches</param>
        /// <param name="top">The vertical position in inches</param>
        /// <returns>nothing</returns>
        public void PrintPageLine(string line, double left, double top)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            TextBlock txt = new TextBlock();
            string s = line;
            if (line != null) { s = line.Replace(PAGE_TAG, pgNumber.ToString()); }
            if (line != null) { s = line.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
            txt.Text = s;
            txt.FontFamily = new System.Windows.Media.FontFamily(font.FontFamily.Name);
            txt.FontSize = fontSize; // font.SizeInPoints;
            if (m_fontBold)
                txt.FontWeight = System.Windows.FontWeights.Bold;
            else
                txt.FontWeight = System.Windows.FontWeights.Regular;
            if (m_fontUnderline)         // font.Underline ||
            {
                txt.TextDecorations = Underline();
            }
            txt.Foreground = new System.Windows.Media.SolidColorBrush(m_fontColor);
            FixedPage.SetTop(txt, top * 96);
            FixedPage.SetLeft(txt, left * 96);
            FixedPage.SetRight(txt, m_RightMargin);
            txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            page.Children.Add(txt);
        }

        /// <summary>
        /// Takes an array of string values that can, optionally, contain
        /// tab values in the forms of "tab(n)" to precisely position the
        /// next value in the array.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>True if MaxPageHeight has been reached</returns>
        public bool PrintPageLine(string[] lines, bool linefeed = true, double topOffset = 0)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            string s = "";
            int ptr = 0;
            foreach (string line in lines)
            {
                if (line != null && line.Length > 4  && line.ToLower().Substring(0,4).Equals("tab("))
                {
                    ptr = int.Parse(line.Substring(4, line.IndexOf(")")-4));
                    if (s.Length <= ptr)
                    {
                        s = s.PadRight(ptr);
                    }
                    else
                    {
                        s = s.Substring(0,ptr);
                    }
                }
                else
                    s += line;
            }
            TextBlock txt = new TextBlock();
            if (s != null) { s = s.Replace( PAGE_TAG, pgNumber.ToString() ); }
            if (s != null) { s = s.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
            txt.Text = s;
            txt.FontFamily = new System.Windows.Media.FontFamily( font.FontFamily.Name );
            txt.FontSize = fontSize; // font.SizeInPoints;
            if (font.Bold || m_fontBold)
                txt.FontWeight = System.Windows.FontWeights.Bold;
            else
                txt.FontWeight = System.Windows.FontWeights.Regular;
            if (font.Underline || m_fontUnderline)
            {
                txt.TextDecorations = Underline();
            }
            txt.Foreground = new System.Windows.Media.SolidColorBrush(m_fontColor);
            FixedPage.SetTop(txt, m_CurrentHeight);
            FixedPage.SetLeft( txt, m_LeftMargin );
            FixedPage.SetRight( txt, m_RightMargin );
            txt.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin + topOffset, (double)m_RightMargin, (double)m_BottomMargin);
            page.Children.Add(txt);
            if (linefeed)
            {
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            if (LinesPerPage > 0 && ln >= LinesPerPage)
                return true;
            else
                return (bool)( m_CurrentHeight + BottomOffset >= m_MaxPageHeight ? true : false );
        }

        /// <summary>
        /// Takes an array of string values that can, optionally, contain
        /// tab values in the forms of "tab(n)" to precisely position the
        /// next value in the array.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>True if MaxPageHeight has been reached</returns>
        public bool PrintPageLine(string[] lines, System.Windows.Media.Brush color, bool linefeed = true)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            string s = "";
            int ptr = 0;
            StackPanel panel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal, Height = m_LineHeight };
            TextBlock txt = null;
            foreach (string line in lines)
            {
                s = line;
                if (line != null && line.Length > 4 && line.ToLower().Substring(0, 4).Equals("tab("))
                {
                    ptr = int.Parse(line.Substring(4, line.IndexOf(")") - 4));
                }
                else
                {
                    s = line.Replace(PAGE_TAG, pgNumber.ToString());
                    s = line.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy"));
                    txt = new TextBlock();
                    txt.FontFamily = new System.Windows.Media.FontFamily(font.FontFamily.Name);
                    txt.FontSize = fontSize; // font.SizeInPoints;
                    if (font.Bold || m_fontBold)
                        txt.FontWeight = System.Windows.FontWeights.Bold;
                    else
                        txt.FontWeight = System.Windows.FontWeights.Regular;
                    if (font.Underline || m_fontUnderline)
                    {
                        txt.TextDecorations = Underline();
                    }
                    txt.Foreground = new System.Windows.Media.SolidColorBrush(m_fontColor);
                    txt.Text = s;
                    if (ptr >= 0)
                        txt.Margin = new Thickness(0,0,ptr,0);
                    else if (ptr < 0)
                        txt.Margin = new Thickness(-ptr,0,0,0);
                    txt.Background = color;
                    panel.Children.Add(txt);
                    ptr = 0;
                }
            }

            FixedPage.SetTop(panel, m_CurrentHeight);
            FixedPage.SetLeft(panel, m_LeftMargin);
            FixedPage.SetRight(panel, m_RightMargin);
            panel.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            page.Children.Add(panel);
            if (linefeed)
            {
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            if (LinesPerPage > 0 && ln >= LinesPerPage)
                return true;
            else
                return (bool)(m_CurrentHeight + BottomOffset >= m_MaxPageHeight ? true : false);
        }

        /// <summary>
        /// Prints a Contorls.Image at specified location on Page
        /// </summary>
        /// <param name="img">The image to print</param>
        /// <param name="x1">Horizontal location</param>
        /// <param name="y1">Verticle location</param>
        /// <param name="linefeed"></param>
        public void PrintImage(System.Windows.Controls.Image img, double x1, double y1,bool linefeed = false)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }
            FixedPage.SetTop(img, y1);
            FixedPage.SetLeft(img, x1  * (CHAR_WIDTH - (CHAR_WIDTH * .08)));
            page.Children.Add(img);
            if (linefeed)
            {
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
        }
        /// <summary>
        /// Add a line or box graphic to the page
        /// </summary>
        /// <param name="x1">left starting position in characters</param>
        /// <param name="y1">top starting position in characters</param>
        /// <param name="x2">right ending position in characters</param>
        /// <param name="y2">bottom ending position in characters</param>
        /// <param name="lineColor">Color of lines</param>
        /// <param name="box">if true, draws a rectangle</param>
        /// <param name="fillColor">fill color of rectangle</param>
        public void PrintLine(double x1, double y1, double x2, double y2, 
                              System.Windows.Media.Brush lineColor = null,
                              bool box = false, 
                              System.Windows.Media.Brush fillColor = null )
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            System.Windows.Media.Brush lColor;
            System.Windows.Media.Brush fColor;
            if (lineColor == null)
                lColor = System.Windows.Media.Brushes.Black;
            else
                lColor = lineColor;
            if (fillColor == null)
                fColor = System.Windows.Media.Brushes.White;
            else
                fColor = fillColor;

            if (box) 
            {
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
                rect.Height = y2*CHAR_HEIGHT - y1*CHAR_HEIGHT;
                rect.Width = x2*CHAR_WIDTH - x1*CHAR_WIDTH;
                rect.Stroke = lColor;
                rect.Fill = fColor;
                FixedPage.SetTop(rect, y1*CHAR_HEIGHT);
                FixedPage.SetLeft(rect, x1*CHAR_WIDTH);
                page.Children.Add(rect);
            } else {
                Line line = new Line();
                line.Stroke = lColor;
                line.X1 = x1 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
                line.Y1 = y1 * (CHAR_HEIGHT + (CHAR_HEIGHT * .02));
                line.X2 = x2 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
                line.Y2 = y2 * (CHAR_HEIGHT + (CHAR_HEIGHT * .02));
                FixedPage.SetTop(line, y1);
                FixedPage.SetLeft(line, x1);
                page.Children.Add(line);
            }
        }

                /// <summary>
        /// Add a line or box graphic to the page using margin properties
        /// </summary>
        /// <param name="x1">left starting position in characters</param>
        /// <param name="y1">top starting position in characters</param>
        /// <param name="x2">right ending position in characters</param>
        /// <param name="y2">bottom ending position in characters</param>
        /// <param name="lineColor">Color of lines</param>
        /// <param name="box">if true, draws a rectangle</param>
        /// <param name="fillColor">fill color of rectangle</param>
        public void PrintLineWithMargins(double x1, double y1, double x2, double y2, 
                              System.Windows.Media.Brush lineColor = null,
                              bool box = false, 
                              System.Windows.Media.Brush fillColor = null )
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            System.Windows.Media.Brush lColor;
            System.Windows.Media.Brush fColor;
            if (lineColor == null)
                lColor = System.Windows.Media.Brushes.Black;
            else
                lColor = lineColor;
            if (fillColor == null)
                fColor = System.Windows.Media.Brushes.White;
            else
                fColor = fillColor;

            if (box) 
            {
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
                rect.Height = y2*CHAR_HEIGHT - y1*CHAR_HEIGHT;
                rect.Width = x2*CHAR_WIDTH - x1*CHAR_WIDTH;
                rect.Stroke = lColor;
                rect.Fill = fColor;
                FixedPage.SetTop(rect, y1*CHAR_HEIGHT + m_TopMargin);
                FixedPage.SetLeft(rect, x1*CHAR_WIDTH + m_LeftMargin);
                page.Children.Add(rect);
            } else {
                Line line = new Line();
                line.Stroke = lColor;
                line.X1 = x1 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
                line.Y1 = y1 * (CHAR_HEIGHT + (CHAR_HEIGHT * .02));
                line.X2 = x2 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
                line.Y2 = y2 * (CHAR_HEIGHT + (CHAR_HEIGHT * .02));
                FixedPage.SetTop(line, y1 + m_TopMargin);
                FixedPage.SetLeft(line, x1 + m_LeftMargin);
                page.Children.Add(line);
            }
        }

        /// <summary>
        /// Add a line to the page at the CurrentHeight
        /// then advance a line
        /// </summary>
        /// <param name="x1">left starting position in characters</param>
        /// <param name="x2">right ending position in characters</param>
        /// <param name="lineColor">Color of line</param>
        public bool PrintLine(double x1, double x2, System.Windows.Media.Brush lineColor = null, bool linefeed = true)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            System.Windows.Media.Brush lColor;
            System.Windows.Media.Brush fColor;
            if (lineColor == null)
                lColor = System.Windows.Media.Brushes.Black;
            else
                lColor = lineColor;

            Line line = new Line();
            line.Stroke = lColor;
            line.X1 = x1 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
            line.X2 = x2 * (CHAR_WIDTH - (CHAR_WIDTH * .08));
            line.Y1 = m_CurrentHeight;
            line.Y2 = m_CurrentHeight + .15;
            FixedPage.SetTop(line, (m_CurrentHeight - m_TopMargin)/240);
            FixedPage.SetLeft(line, x1);
            page.Children.Add(line);
            if (linefeed)
            {
                m_CurrentHeight += m_LineHeight;
                ln++;
            }
            return (bool)(m_CurrentHeight >= m_MaxPageHeight ? true : false);
        }

        
        /// <summary>
        /// Loads image file
        /// </summary>
        /// <param name="imagefile">Path to image file</param>
        /// <returns>Image as Controls.Image</returns>
        public static System.Windows.Controls.Image GetImage(string imagefile)
        {
            try
            {
                BitmapImage bitImg = new BitmapImage();
                FileStream fs = File.OpenRead(imagefile);
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                fs.Close();
                bitImg.BeginInit();
                bitImg.StreamSource = ms;
                bitImg.EndInit();
                bitImg.Freeze();
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                i.Source = bitImg;
                return i;
            }
            catch
            {
                return null;
            }
        }

        public void AddPageBackground(ImageBrush image)
        {
            page.Background = image;

            if (page == null)
            {
                CreateNewPage();

            }

        }

        public void AddImage(System.Windows.Controls.Image img, bool skipPage = false)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();

            if (page == null)
            {
                CreateNewPage();

            }

            System.Windows.Controls.Image i = new System.Windows.Controls.Image();
            i.Source = img.Source;
            if (skipPage)
            {
                this.NewPage();
                CreateNewPage();

            }
            page.Children.Add(i);
        }

        public void AddImage(Canvas img, bool skipPage = false)
        {
            if (PrintFlag && !PrinterChosen)
                ChoosePrinter();
            if (skipPage)
                this.NewPage();

            if (page == null)
            {
                CreateNewPage();

            }

            page.Children.Add(img);
        }

        public void AddPdf(string filename, bool skipPage = false)
        {
            /*
            try
            {
                filename = System.IO.Path.GetTempPath();
                filename += "TempPDF_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            }
            catch (IOException ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                    message += "\r\n\r\n" + ex.InnerException.Message;
                MessageBoxCustom.Show("There was an error creating attachment file: " + message);
                return;
            }
            */
            //FileStream  fs = new FileStream(filename, FileMode.Create);
            using (var stream = new MemoryStream())
            using (Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument())
            {
                pdf.LoadFromFile(filename);
                System.Drawing.Image gdiImg = pdf.SaveAsImage(0);
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                {
                    gdiImg.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.Position = 0;

                    System.Windows.Media.Imaging.BitmapImage bmpImgage = new System.Windows.Media.Imaging.BitmapImage();
                    bmpImgage.BeginInit();
                    bmpImgage.CacheOption = BitmapCacheOption.OnLoad;
                    bmpImgage.StreamSource = stream;
                    try
                    {
                        bmpImgage.EndInit();
                    } catch (Exception ex)
                    {

                    }
                    bmpImgage.Freeze();
                    img.Source = bmpImgage;
                }

                /*
                gdiImg.Save("c:\\Temp\\image");
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();

                //convert System.Drawing.Image to WPF image
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
                IntPtr hBitmap = bmp.GetHbitmap();
                System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                img.Source = WpfBitmap;
                */

                img.Width = 835;
                img.Height = 1079;
                img.Stretch = System.Windows.Media.Stretch.Fill;


                if (PrintFlag && !PrinterChosen)
                    ChoosePrinter();
                if (skipPage)
                    this.NewPage();

                if (page == null)
                {
                    CreateNewPage();

                }

                /*
                for (int i = 1; i < pdf.Pages.Count + 1; i++)
                {
                    var src = pdf.Save(i);
                    //bmp = SourceToBitmap(source);
                    src.Save(string.Format("results-{0}.png", i), ImageFormat.Jpeg);
                }
                    System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                ImageSourceConverter c = new ImageSourceConverter();
                i.Source = (ImageSource)c.ConvertFrom(img);
                */
                page.Children.Add(img);
                //gdiImg.Dispose();
                img = null;
                //bmp.Dispose();
                //WpfBitmap = null;
            }
        }

        /// <summary>
        /// Adds the current page to the FixedDocument and
        /// creates a new page.
        /// </summary>
        public void NewPage(  bool EndDocument = false, bool newDocument = false)
        {
            if (PrintFlag && !PrinterChosen && !EndDocument)
                ChoosePrinter();
            if (page != null)
            {
                //System.Windows.Size sz = new System.Windows.Size(8.5 * 96, 11 * 96);
                //page.Measure(sz);
                //page.Arrange(new System.Windows.Rect(new System.Windows.Point((double)m_LeftMargin, (double)m_TopMargin),   sz));
                //page.UpdateLayout();
                PageContent page1Content = new PageContent();
                ((System.Windows.Markup.IAddChild) page1Content).AddChild( page );
                doc.Pages.Add( page1Content );
                page = null;
            }
            if (!EndDocument)
            {
                if (newDocument)
                {
                    doc = null;
                    doc = new FixedDocument();
                }
                pgNumber++;
                m_CurrentHeight = (double)m_TopMargin;
                totalLinesPrinted += ln;
                ln = 0;
            }
        }

        private  void CreateNewPage()
        {
            page = new FixedPage();
            this.page.Height = pageHeight;
            this.page.Width = pageWidth;
            this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);

        }

        public void NewDocument()
        {
            doc = null;
            doc = new FixedDocument();
            ln = 0;
            totalLinesPrinted = 0;
        }

    /// <summary>
    /// Uses the frmReport window to display the document
    /// </summary>
    /// <param name="DoNotDisplay">true to not call frmReport. Use DOCUMENT property to get the FixedDocument.</param>
    public void DisplayDocument(bool DoNotDisplay = false)
        {
            if (page != null && page.Children.Count > 0)
            {
                PageContent page1Content = new PageContent();
                ((System.Windows.Markup.IAddChild) page1Content).AddChild( page );
                doc.Pages.Add( page1Content );
            }
           if (!DoNotDisplay)
            {
                frmReport frm = new frmReport(doc, m_Title, this.PageOrientation);
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                frm.ShowDialog();
                if (page != null)
                    page.Children.Clear();
                this.doc = null;
                this.page = null;
                this.doc = new FixedDocument();
                this.page = new FixedPage();
                page.Height = pageHeight;
                page.Width = pageWidth;
            }
        }

        public bool ChoosePrinter(PrintQueue printer = null)
        {
            pd = new PrintDialogx();
            if (!PrinterChosen && printer == null)
            {
                Window wndw = new Window();
                wndw.Title = "Choose Printer";
                wndw.Height = (double)600;
                wndw.Width = (double)600;
                wndw.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                wndw.Show();
                //pd.UserPageRangeEnabled = true;
                if ((bool)pd.ShowDialog(wndw) != true)
                {
                    wndw.Close();
                    return false;
                }
                wndw.Close();
            }
            else
            {
                pd.PrintQueue = printer;
            }
            /*
            pd.PrintTicket.Duplexing = Duplexing.OneSided;
            pd.PrintQueue.UserPrintTicket.Duplexing = Duplexing.OneSided;
            if (m_duplex)
            {
                pd.PrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
                pd.PrintQueue.UserPrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
            }
            */
            pd.PrintTicket.PageBorderless = m_Borderless ? System.Printing.PageBorderless.Borderless : System.Printing.PageBorderless.None ;
            pd.PrintTicket.PageOrientation = (PageOrientation == 1 ? System.Printing.PageOrientation.Landscape : System.Printing.PageOrientation.Portrait);
            PrinterChosen = true;
            return true;
        }
        /// <summary>
        /// Prints the document
        /// </summary>
        public void PrintDocument(PrintQueue printer = null)
        {
            if (page != null && page.Children !=null)
            {
                try
                {
                    PageContent page1Content = new PageContent();
                    ((System.Windows.Markup.IAddChild)page1Content).AddChild(page);
                    doc.Pages.Add(page1Content);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "In Report.PrintDocument: " + ex.Message, false);
                }
            }
            if (!PrinterChosen)
                ChoosePrinter(printer);
            if (!PrinterChosen)
                return;
            try
            {
                pd.PrintDocument(doc.DocumentPaginator, m_Title);
                doc = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Report Error: " + ex.Message);
            }
            this.doc = null;
            this.page = null;
            this.doc = new FixedDocument();
            this.page = new FixedPage();
            page.Height = pageHeight;
            page.Width = pageWidth;
        }

        public void FinishDocument()
        {
            if (page != null && page.Children != null)
            {
                try
                {
                    PageContent page1Content = new PageContent();
                    ((System.Windows.Markup.IAddChild)page1Content).AddChild(page);
                    doc.Pages.Add(page1Content);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "In Report.PrintDocument: " + ex.Message, false);
                }
            }

        }

        public bool SetPrinter()
        {
            Window wndw = new Window();
            wndw.Title = "Choose Printer";
            wndw.Show();
            pd = new PrintDialogx();
            //pd.UserPageRangeEnabled = true;
            //return (bool)pd.ShowDialog(wndw);
            return false;
        }

   
        // Utility functions
        private TextDecorationCollection Underline()
        {
            TextDecoration textDecorations = new TextDecoration();
            textDecorations.Pen = new System.Windows.Media.Pen( System.Windows.Media.Brushes.Black, 1 );
            textDecorations.PenThicknessUnit = TextDecorationUnit.FontRecommended;
            TextDecorationCollection collection = new TextDecorationCollection();
            collection.Add( textDecorations );
            return collection;
        }

        /// <summary>
        /// Converts FixedDocument to XPS
        /// </summary>
        /// <param name="doc">FixedDocument</param>
        /// <returns>MemoryStream containing XPS document</returns>
        public static MemoryStream ConvertToXps(FixedDocument doc = null)
        {
            if (doc == null)
                return null;
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create);
            XpsDocument xpsd = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsd);
            writer.Write(doc.DocumentPaginator);
            xpsd.Close();
            package.Close();
            return ms;

        }

        public void PrintFromXps(PrintQueue printer)
        {
            PrintQueue defaultPrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            PrintSystemJobInfo info = printer.AddJob("check", "c:\\Testing\\MbcsCentral\\Wingl\\checks.xps",false);

        }

        /// <summary>
        /// Creates specified pdf file from current document
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool DocToPdf(string filename)
        {
            string xpsFile = filename.Replace(".pdf", ".xps");
            try
            {
                var paginator = doc.DocumentPaginator;
                var xpsDocument = new XpsDocument(xpsFile, FileAccess.Write);
                var documentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                documentWriter.Write(paginator);
                xpsDocument.Close();
                PdfSharp.Xps.XpsConverter.Convert(xpsFile, filename, 0);
            }catch (Exception exc) { return false; }
            try
            {
                FileInfo file = new FileInfo(xpsFile);
                file.Delete();
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to delete '" + xpsFile + "':" + ex.Message, false);
            }
            finally
            {
                this.doc = null;
                this.page = null;
                this.doc = new FixedDocument();
                this.page = new FixedPage();
                page.Height = pageHeight;
                page.Width = pageWidth;
            }
            return true;
        }

        /// <summary>
        /// Creates specified pdf file from passed fixed document
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>true if successful</returns>
        public static bool DocToPdf(FixedDocument doc, string filename)
        {
            string xpsFile = filename.Replace(".pdf", ".xps");
            try
            {
                if (doc.Pages.Count > 0)
                {
                    var paginator = doc.DocumentPaginator;
                    var xpsDocument = new XpsDocument(xpsFile, FileAccess.Write);
                    var documentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                    documentWriter.Write(paginator);
                    xpsDocument.Close();
                    PdfSharp.Xps.XpsConverter.Convert(xpsFile, filename, 0);
                }
            }
            catch (Exception exc)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to create '" + filename + "':" + exc.Message, false);
                LastError = exc.Message.Replace("FixedDocument must contain at least one FixedPage", "No Data in document");
                return false;
            }
            finally
            {
                try
                {
                    FileInfo file = new FileInfo(xpsFile);
                    file.Delete();
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to delete '" + xpsFile + "':" + ex.Message, false);
                }
            }
            return true;
        }

        /// <summary>
        /// Saves an XPS MemoryStream to a PDF file
        /// </summary>
        /// <param name="ms">MemoryStream containing XPS document</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveToPdf(MemoryStream ms, string fileName)
        {
            try
            {
                PdfSharp.Xps.XpsModel.XpsDocument pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(ms);
                //PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, fileName, 0);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Report.SaveToPdf: " + ex.Message, false);
                return false;
            }
            return true;      
        }

        public static void CanvasToPDF(Canvas canvas, BitmapImage background, string filename)
        {
            Canvas doc = canvas;
            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            fixedPage.Children.Add(doc);
            if (background != null)
            {
                Border _reportBorder = new Border() { BorderBrush = System.Windows.Media.Brushes.Transparent, BorderThickness = new Thickness(0) };
                _reportBorder.Width = 400;
                _reportBorder.Height = 400;
                System.Windows.Size pageSize = new System.Windows.Size(850, 1100);
                System.Windows.Controls.Image img = new System.Windows.Controls.Image() { Stretch = System.Windows.Media.Stretch.None };
                img.Source = background;
                _reportBorder.Child = img;
                _reportBorder.Measure(pageSize);
                _reportBorder.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));

            }
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);

            DocToPdf(fixedDoc, filename);
        }

        /*
        public static void CanvasToPDF(Canvas canvas, string filename)
        {
            var rtb = new RenderTargetBitmap(
             (int)850, //width
             (int)1100, //height
             300, //dpi x
             300, //dpi y
             System.Windows.Media.PixelFormats.Pbgra32 // pixelformat
             );
            rtb.Render(canvas);

            SaveRTBAsPNG(rtb, filename);
        }
        */
        private static void SaveRTBAsPNG(RenderTargetBitmap bmp, string filename)
        {
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }

        public double GetNextLinePostion(double current)
        {
            return current + m_LineHeight;
        }



        // Properties
        /// <summary>
        /// The current page location
        /// </summary>
        public double CurrentHeight
        {
            get { return m_CurrentHeight;}
            set {m_CurrentHeight = value;}
        }
       /// <summary>
       /// Height of each line in device indpendent units
       /// </summary>
        public double LineHeight
        {
            get {return m_LineHeight;}
            set {m_LineHeight = value;}
        }
        /// <summary>
        /// Height of each line in inches
        /// </summary>
        public double LineHeightInches
        {
            get { return m_LineHeight / 96; }
            set { m_LineHeight= value * 96; }
        }
        /// <summary>
        /// Number of lines printed on currrent page
        /// </summary>
        public int LinesPrinted
        {
            get { return ln; }
        }
        public int TotalLinesPrinted
        {
            get { return totalLinesPrinted + ln; }
        }
        /// <summary>
        /// Top Margin in device independent units
        /// </summary>
        public Single TopMargin
        {
            get {return m_TopMargin;}
            set 
            {
                m_TopMargin=value;
                m_CurrentHeight = value;
                m_MaxPageHeight = (m_Orientation == 0 ?  MAX_PORTRAIT_HEIGHT - m_TopMargin - m_BottomMargin: MAX_LANDSCAPE_HEIGHT - m_TopMargin - m_LeftMargin);
                if (page != null)
                    this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// Top Margin in inches
        /// </summary>
        public Single TopMarginInches
        {
            get { return m_TopMargin / 96; }
            set
            {
                m_TopMargin = value * 96;
                m_CurrentHeight = value * 96;
                m_MaxPageHeight = (m_Orientation == 0 ? MAX_PORTRAIT_HEIGHT - m_TopMargin - m_BottomMargin : MAX_LANDSCAPE_HEIGHT - m_TopMargin - m_LeftMargin);
                if (page != null)
                    this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// Bottom Margin in device independent units
        /// </summary>
        public Single BottomMargin
        {
            get {return m_BottomMargin;}
            set {
                m_BottomMargin=value;
                m_MaxPageHeight = (m_Orientation == 0 ? MAX_PORTRAIT_HEIGHT - m_TopMargin - m_BottomMargin : MAX_LANDSCAPE_HEIGHT - m_TopMargin - m_LeftMargin);
                if (page != null)
                    this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// If non-zero adjusts actual last print line
        /// up (negative) or down(positive)
        /// </summary>
        public Single BottomOffset
        {
            get;
            set;
        }
        /// <summary>
        /// Left Margin in device independent units
        /// </summary>
        public Single LeftMargin
        {
            get {return  m_LeftMargin;}
            set {m_LeftMargin=value;
            if (page != null)
                this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// Left Margin in inches
        /// </summary>
        public Single LeftMarginInches
        {
            get { return m_LeftMargin / 96; }
            set { m_LeftMargin = value * 96;
            if (page != null)
                this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// Right Margin in device independent units
        /// </summary>
        public Single RightMargin
        {
            get {return m_RightMargin;}
            set {m_RightMargin=value;
            if (page != null)
                this.page.Margin = new Thickness((double)m_LeftMargin, (double)m_TopMargin, (double)m_RightMargin, (double)m_BottomMargin);
            }
        }
        /// <summary>
        /// Page Orientation.
        /// 
        /// 0 = Portrait, 1 = Landscape
        /// </summary>
        public int PageOrientation
        {
            get {return m_Orientation;}
            set {
                if (value >= (int)Orientation.Portrait && value <= (int)Orientation.Landscape)
                {
                    m_Orientation = value;
                    switch (value)
                    {
                        case (int)Orientation.Portrait:
                            m_MaxPageHeight = MAX_PORTRAIT_HEIGHT;
                            pageHeight = MAX_PORTRAIT_HEIGHT;
                            pageWidth = MAX_LANDSCAPE_HEIGHT;
                            break;
                        case (int) Orientation.Landscape:
                            m_MaxPageHeight = MAX_LANDSCAPE_HEIGHT;
                            pageHeight = MAX_LANDSCAPE_HEIGHT;
                            pageWidth = MAX_PORTRAIT_HEIGHT;
                            break;
                    }
                    if (page != null)
                    {
                        page.Height = pageHeight;
                        page.Width = pageWidth;
                    }
                }
                else
                    throw new ArgumentException("Value must be between " + (int)Orientation.Portrait + " and " + (int)Orientation.Landscape);
            }
        }
        /// <summary>
        /// Maximum height of a printed page in device independent units.
        /// 
        /// Generally set internally
        /// </summary>
        public Single MaxPageHeight
        {
            get {return m_MaxPageHeight;}
            set {m_MaxPageHeight=value;}
        }
        /// <summary>
        /// Header lines to be used by PrintHeader method.
        /// 
        /// Takes a two-dimensional object array:
        /// Dimension 1 = header text
        /// Dimension 2 - font to apply to that line, null to use base font
        /// </summary>
        public object[,] HeaderLines
        {
            get {return m_HeaderLines;}
            set {m_HeaderLines=value;}
        }
        /// <summary>
        /// Current page number which can be specified in a print or header
        /// line by using the PAGE_TAG of pg>
        /// </summary>
        public int PageNumber
        {
            get {return pgNumber;}
            set {pgNumber=value;}
        }
        /// <summary>
        /// Read Only.  Returns the PAGE_TAG
        /// </summary>
        public string PageTag
        {
            get { return PAGE_TAG; }
        }
        public Font SetFont
        {
            set { font = value;
            fontSize = font.SizeInPoints;
            }
            get { return font;}
        }
        public bool FontBold
        {
            get { return m_fontBold; }
            set { m_fontBold = value; }
        }
        public bool FontUnderline
        {
            get { return m_fontUnderline; }
            set { m_fontUnderline = value; }
        }
        public System.Windows.Media.Color FontColor
        {
            get { return m_fontColor; }
            set { m_fontColor = value; }
        }
        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        public bool Duplex
        {
            get { return m_duplex; }
            set { m_duplex = value; }
        }
        public bool Borderless
        {
            get { return m_Borderless; }
            set { m_Borderless = value; }
        }
        public bool PrintFlag
        { get; set; }

        public bool PrinterChosen
        { get; set; }

        public int PageWidth { set { pageWidth = value; } }

        /// <summary>
        /// Returns the FixedDocument produced by this class.
        /// </summary>
        public FixedDocument Document
        {
            get { return doc; }
        }

        public static string LastError { get; set; }

    }

    public class PrinterQueues
    {
        PrintQueueCollection _Printers;
        ArrayList items = new ArrayList();

        public PrinterQueues()
        {
            _Printers = new PrintServer().GetPrintQueues(new[] {
                EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections
            });
            int i = 0;
            foreach (PrintQueue server in _Printers)
            {
                items.Add(new ListItem(server.Name, i++));
            }
        }

        public ArrayList Printers
        {
            get { return items; }
        }

        public PrintQueue Queue(long index)
        {
            if (index >= 0)
            {
                //PrintQueue q = _Printers.ElementAt<PrintQueue>(index);

                long i = 0;
                foreach (PrintQueue q in _Printers)
                    if (i++ == index)
                    {
                        return q;
                    }
                return null;
            }
            else
                return null;
            //     */
            //    return q;
            //}
        }
    }
    
    /// <summary>
    /// <para>&#160;</para>
    /// A class that facilitates the processing of background reports.
    /// <para>&#160;</para>
    /// Usage:
    /// <para>&#160;</para>
    /// The class for the report to be printed must inherit the abstract class: ReportBase<para/>
    /// and contain a method as follows:<para/>
    /// <example>public override void Process(){}</example><para/> 
    /// that either kicks off the report processing or calls the method that does.
    /// <para>&#160;</para>
    /// The class or window that initiates the report first encapsulates any constructor<para/>
    /// variables in an object array: <example> object[] parms = {arg1, arg2, arg3};</example>.<para/>
    /// These must be the same type and in the same order as the report class' constructor<para/>
    /// argument list.
    /// <para>&#160;</para>
    /// Then the initiating class/window makes the call: <code> StartReport.Start(reportClass, parms)</code><para/>
    /// where 'reportClass' is the fully qualified report class such as:<para/> 
    /// <example>StartReport.Start("MbcsCentral.WinPM.StatusReport", parms)</example>
    ///
    /// </summary>
    public class StartReport
    {

        /// <summary>
        /// Sets up variables for report, create report thread and start it
        /// </summary>
        /// <param name="rptClass">The full Namespace of the report object to process</param>
        /// <param name="parms">Array of arguments that match a constructor in the report object</param>
        public static void Start(string rptClass, object parms)
        {
            object[] data = { rptClass, parms };
            string[] title = null;
            title = rptClass.Split('.');
            Thread ProcessReport = new Thread(ReportThread);
            ProcessReport.TrySetApartmentState(ApartmentState.STA);
            ProcessReport.Start(data);

            Window win = new MessageWindow((title != null ? title[title.Length - 1] : "Report"), "Your report is being processed and should be ready soon." + Environment.NewLine + "You may continue working.");
            win.Show();

        }
        /// <summary>
        /// This is the entry point for the report thread
        /// Notice that it instantiates the class for the report
        /// and calls the abstract method Process which must, of course,
        /// exist in the report class and be the starting method there.
        /// </summary>
        /// <param name="data">Array of arguments for the report object.
        /// The first argument is the name of the report class to process.</param>
        private static void ReportThread(object data)
        {
            var o = data as object[];
            string rptClass =  (string)o[0];
            object[] parms = (object[])o[1];
            CallClassParms callClass = new CallClassParms(rptClass, parms);
            ReportBase rpt = (ReportBase)callClass.returnObject(parms);
            rpt.Process();
        }
    }

    public abstract class ReportBase
    {
        public ReportBase(){}

        public abstract void Process();
    }
}
