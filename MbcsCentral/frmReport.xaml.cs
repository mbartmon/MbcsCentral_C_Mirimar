namespace MbcsCentral
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Printing;
    public partial class frmReport
    {

        FixedDocument doc;
        PrintDialog pd;
        int pageOrientation = 0;
        string[] ReportInfo = null;

        private void frmReport_Closed(object sender, System.EventArgs e)
        {
            Main.saveForm(this);

        }

        private void frmReport_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.getForm(this);
            docViewer.DataContext = doc;
        }


        private void cmdClose_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();

        }

        public frmReport()
        {
            this.Loaded += frmReport_Loaded;
            this.Closed += frmReport_Closed;
        }

        public frmReport(FixedDocument doc, string ReportName = "", int pageorientation = 0)
        {
            InitializeComponent();
            this.Loaded += frmReport_Loaded;
            this.Closed += frmReport_Closed;
            this.pageOrientation = pageorientation;
            ReportInfo = ReportName.Split(';');
            this.Title = (ReportInfo == null || ReportInfo[0].Trim() == "" ? "Report Viewer" : ReportInfo[0].Trim());
            this.doc = doc;
        }

        private void cmdPrint_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            frmPrintDialog pd = new frmPrintDialog();
            pd.Owner = this;
            pd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            pd.UsePageRange = true;
            pd.ShowDialog();

            if (pd.Canceled)
                return;
            PrintDialogx pdx = new PrintDialogx();
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
            pdx.PrintTicket.PageOrientation = (pageOrientation == 1 ? System.Printing.PageOrientation.Landscape : System.Printing.PageOrientation.Portrait);
            try
            {
                pdx.PrintDocument(doc.DocumentPaginator, this.Title);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Print Document: " + ex.Message);
            }
        }

        private void cmdEmail_Click(object sender, RoutedEventArgs e)
        {
            Email.frmEmailAttachment frm = new Email.frmEmailAttachment() { ReportInfo = this.ReportInfo, Document = doc };
            frm.Show();

        }
    }
}
