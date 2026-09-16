using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Printing;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MbcsCentral.WinGL;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmReportList.xaml
    /// </summary>
    public partial class frmReportList : Window
    {
        private List<string> ReportNames;
        private List<FixedDocument> Documents;

        FixedDocument doc = new FixedDocument();
        string[] ReportInfo = null;
        double LINE_HEIGHT = (Single)0.135 * 96; // (Single)0.167 * 96;
        double CurrentLineWidth;
        public Single TOP_MARGIN { get; set; } = (Single)0.75 * 96;
        public Single BOTTOM_MARGIN { get; set; } = (Single)0.75 * 96;
        public Single LEFT_MARGIN { get; set; } = (Single)0.75 * 96;
        Single RIGHT_MARGIN = (Single)0;
        double CURRENT_HEIGHT;

        private CheckRegister checkRegister;
        private bool mailCheckRegister = false;
        public CheckRegister CR
        {
            get { return checkRegister; }
            set 
            { 
                checkRegister = value;
                mailCheckRegister = true;
                
            }
        }

        private TransactionEditList transEditList;
        private bool mailTransEditList = false;
        public TransactionEditList TE
        {
            get { return transEditList; }
            set
            {
                transEditList = value;
                mailTransEditList = true;
            }

        }
        const string NEW_PAGE = "\r\n";
        public const string PAGE_TAG = "<pg>";
        public const string DATE_TAG = "<date>";
        private int pgNumber = 1;

        /// <summary>
        /// Report Displayer that provides a
        /// mechanism to call a class
        /// and pass a specified portion of
        /// the double-clicked line.
        /// <para/> 
        /// EditClass is the FQN of the class to call.
        /// <para/> 
        /// IndexStart is the starting position of the double-clicked line to pass.
        /// <para/> 
        /// IndextLength is the length of the captured line to pass.
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        public frmReportList(string fontName = "Courier New", double fontSize = 12, string ReportName = "", double displaySize = 0)
        {
            InitializeComponent();

            //lstReport.FontFamily = new System.Windows.Media.FontFamily(fontName);
            lstReport.FontSize = fontSize; 
            ReportInfo = ReportName.Split(';');

        }
        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            string line = (string)item.Content;
            if (line != null && line.Length >= IndexLength)
            {
                if (line.Substring(IndexStart, IndexLength).Trim().Equals(""))
                {
                    MessageBoxCustom.Show("Choose the first line of an item to edit.");
                    return;
                }
                CallClassParms parm = new CallClassParms(EditClass, line.Substring(IndexStart, IndexLength));
                if (parm != null)
                {
                    object obj;
                    object[] parms = { line.Substring(IndexStart, IndexLength) };
                    try
                    {
                        obj = parm.returnObject(parms);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCustom.Show("Unable to retrieve editor: " + ex.Message);
                        return;
                    }
                    try
                    {
                        Window frm = (Window)obj;
                        frm.ShowDialog();
                        this.Reprint = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCustom.Show("Unable to load editor: " + ex.Message);
                    }
                }
            }
            else
                MessageBoxCustom.Show("Choose the first line of an item to edit.");
        }
        public string title
        {
            set {
                this.Title = value;
                this.txtTitle.Text = value; }
        }

        public string FontName { get; set; } = "Courier New";
        public int FontSize { get; set; } = 12;
        public bool FontBold { get; set; } = false;
        public int PageSize { get; set; }
        public string EditClass { get; set; }
        public int IndexStart { get; set; }
        public int IndexLength { get; set; }
        public bool Reprint { get; set; }
        public int pageOrientation { get;  set; }
        public PrintQueue printer { get; set; } = null;
        public bool PrintIndex { get; set; } = false;


        private void frmReportList_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
        }

        private void frmReportList_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        public void Add(string line ="", bool bold = false)
        {
            if (line != null)
            {
                if (line != null) { line = line.Replace(PAGE_TAG, pgNumber.ToString()); }
                if (line != null) { line = line.Replace(DATE_TAG, DateTime.Now.ToString("MM/dd/yyyy")); }
                ListViewItem itm = new ListViewItem();
                itm.FontWeight = (FontWeight)(bold ? FontWeights.Bold : FontWeights.Normal);
                itm.Content = line;
                lstReport.Items.Add(line);
            }
        }

        public void NewPage()
        {
            lstReport.Items.Add(NEW_PAGE);
            pgNumber++;
        }

        public void Print()
        {
            cmdPrint_Click(new object(), new RoutedEventArgs());
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lstReport.Items.Count == 0)
                return;

            if (EditClass.Trim().Equals("MbcsCentral.WinGL.frmTransactionEdit"))
                PrintIndex = true;
            FixedPage page = new FixedPage();
            int lines = 0;
            for (int i = 0; i < lstReport.Items.Count; i++)
            {
                if (((string)lstReport.Items[i]).Contains(NEW_PAGE))
                {
                    if (page != null)
                    {
                        PageContent pageContent0 = new PageContent();
                        ((System.Windows.Markup.IAddChild)pageContent0).AddChild(page);
                        doc.Pages.Add(pageContent0);
                    }
                    page = new FixedPage();
                    CURRENT_HEIGHT = (double)TOP_MARGIN;
                    lines = 0;
                }
                TextBlock txt = new TextBlock() { Text = (string)lstReport.Items[i], FontSize = 8 };
                if (!PrintIndex && txt.Text.Length > IndexStart + IndexLength && !txt.Text.Substring(IndexStart,IndexLength - 2).Trim().Equals(""))
                {
                    txt.Text = txt.Text.Substring(0, IndexStart) + new string(' ', IndexLength) + txt.Text.Substring(IndexLength);
                }
                FixedPage.SetTop(txt, TOP_MARGIN + CURRENT_HEIGHT);
                FixedPage.SetLeft(txt, LEFT_MARGIN + CurrentLineWidth);
                FixedPage.SetRight(txt, RIGHT_MARGIN);
                txt.Margin = new Thickness((double)LEFT_MARGIN, (double)TOP_MARGIN, (double)RIGHT_MARGIN, (double)BOTTOM_MARGIN);
                txt.Measure(new System.Windows.Size(400, 1000));
                txt.FontFamily = new System.Windows.Media.FontFamily(FontName);
                txt.FontSize = FontSize; //  font.SizeInPoints;
                                         //txt.Arrange();
                if (FontBold)
                    txt.FontWeight = System.Windows.FontWeights.Bold;
                CurrentLineWidth += (txt.DesiredSize.Width);
                page.Children.Add(txt);
                CurrentLineWidth = 0;
                CURRENT_HEIGHT += LINE_HEIGHT;
                lines++;
            }
            if (page != null)
            {
                PageContent pageContent1 = new PageContent();
                ((System.Windows.Markup.IAddChild)pageContent1).AddChild(page);
                doc.Pages.Add(pageContent1);
            }
            if (doc == null)
                return;

            PrintDialogx pdx = new PrintDialogx();
            if (printer == null)
            {
                frmPrintDialog pd = new frmPrintDialog();
                pd.Owner = this;
                pd.UsePageRange = true;
                pd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                pd.ShowDialog();
                if (pd.Canceled)
                {
                    this.Close();
                    return;
                }
                pdx.PrintQueue = pd.Queue;
                if (pd.Duplex)
                {
                    pdx.PrintTicket.Duplexing = (pd.DuplexType == Duplexing.TwoSidedShortEdge ? Duplexing.TwoSidedShortEdge : Duplexing.TwoSidedLongEdge);
                }
                else
                    pdx.PrintTicket.Duplexing = Duplexing.OneSided;
                if (pd.UsePageRange && pd.PageRange != null && !pd.PageRange.Trim().Equals(""))
                {
                    string[] ranges = pd.PageRange.Split(',');
                    foreach (string range in ranges)
                    {
                        pdx.AddRange(range);
                    }
                }
            }
            else
            {
                pdx.PrintQueue = printer;
                pdx.PrintTicket.Duplexing = Duplexing.OneSided;
                pdx.PrintQueue.UserPrintTicket.Duplexing = Duplexing.OneSided;
                pdx.PrintTicket.PageOrientation = (pageOrientation == 1 ? System.Printing.PageOrientation.Landscape : System.Printing.PageOrientation.Portrait);
            }
            try
            {
                pdx.PrintDocument(doc.DocumentPaginator, this.Title);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Print Document: " + ex.Message);
            }
            finally
            {
                doc = null;
                page = null;
            }
        }

        private void cmdEmail_Click(object sender, RoutedEventArgs e)
        {
            MbcsCentral.Email.frmEmailAttachment frmEmail = null;
            if (mailCheckRegister || mailTransEditList)
            {
                ReportNames = new List<string>();
                Documents = new List<FixedDocument>();
                if (mailCheckRegister)
                {
                    CheckRegister rptCR = CR;
                    doc = rptCR.print_report((int)CheckRegister.Targets.Email);
                    ReportNames.Add("CheckRegister_" + rptCR.CompanyName.Trim());
                }
                else
                {
                    TransactionEditList rptTE = TE;
                    doc = rptTE.print_report((int)TransactionEditList.Targets.Email);
                    ReportNames.Add("TransactionEditList");
                }
            }
            frmEmail = new MbcsCentral.Email.frmEmailAttachment();
            Documents.Add(doc);
            frmEmail.Documents = Documents;
            Email.frmEmailAttachment frm = new Email.frmEmailAttachment() { ReportInfo = this.ReportInfo, Document = doc };
            frm.Show();

        }

        public FixedDocument PrepareDocument()
        {
            FixedDocument doc = new FixedDocument();
            FixedPage page = new FixedPage();
            int lines = 0;
            for (int i = 0; i < lstReport.Items.Count; i++)
            {
                if (((string)lstReport.Items[i]).Contains(NEW_PAGE))
                {
                    if (page != null)
                    {
                        PageContent pageContent0 = new PageContent();
                        ((System.Windows.Markup.IAddChild)pageContent0).AddChild(page);
                        doc.Pages.Add(pageContent0);
                    }
                    page = new FixedPage();
                    CURRENT_HEIGHT = (double)TOP_MARGIN;
                    lines = 0;
                }
                TextBlock txt = new TextBlock() { Text = (string)lstReport.Items[i], FontSize = 8 };
                if (!PrintIndex && txt.Text.Length > IndexStart + IndexLength)
                {
                    txt.Text = txt.Text.Substring(0, IndexStart) + new string(' ', IndexLength) + txt.Text.Substring(IndexLength);
                }
                FixedPage.SetTop(txt, TOP_MARGIN + CURRENT_HEIGHT);
                FixedPage.SetLeft(txt, LEFT_MARGIN + CurrentLineWidth);
                FixedPage.SetRight(txt, RIGHT_MARGIN);
                txt.Margin = new Thickness((double)LEFT_MARGIN, (double)TOP_MARGIN, (double)RIGHT_MARGIN, (double)BOTTOM_MARGIN);
                txt.Measure(new System.Windows.Size(400, 1000));
                txt.FontFamily = new System.Windows.Media.FontFamily(FontName);
                txt.FontSize = FontSize; //  font.SizeInPoints;
                                         //txt.Arrange();
                if (FontBold)
                    txt.FontWeight = System.Windows.FontWeights.Bold;
                CurrentLineWidth += (txt.DesiredSize.Width);
                page.Children.Add(txt);
                CurrentLineWidth = 0;
                CURRENT_HEIGHT += LINE_HEIGHT;
                lines++;
            }
            if (page != null)
            {
                PageContent pageContent1 = new PageContent();
                ((System.Windows.Markup.IAddChild)pageContent1).AddChild(page);
                doc.Pages.Add(pageContent1);
            }
            return doc;
        }
        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FormReportList_Activated(object sender, EventArgs e)
        {

        }
    }
}
