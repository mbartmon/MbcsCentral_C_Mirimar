using System;
using System.Collections.Generic;
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


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmInvoiceNumber.xaml
    /// </summary>
    public partial class frmInvoiceNumber : Window
    {
        public frmInvoiceNumber()
        {
            InitializeComponent();
        }

        private void frmInvoiceNumber_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            this.BringIntoView();
            this.Topmost = true;
            lblManual.Visibility = Visibility.Collapsed;
            txtManual.Visibility = Visibility.Collapsed;

            if (PrePaid)
                pnlCheck.Visibility = Visibility.Visible;
            else
                pnlCheck.Visibility = Visibility.Collapsed;

            switch (EntryType)
                {
                case (int)BatchInvoiceOptions.InvoiceOptions.Manual:
                    txtTitle.Text += " - Manual Entry";
                    pnlInvoice.Visibility = Visibility.Collapsed;
                    lblDepartment.Visibility = Visibility.Collapsed;
                    lblManual.Visibility = Visibility.Visible;
                    txtManual.Visibility = Visibility.Visible;
                    txtManual.Focus();
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DptDate:
                    txtTitle.Text += " - Department + Date";
                    
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DescDate:
                    txtTitle.Text += " - Invoice Number + Date";
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.ManDate:
                    txtTitle.Text += " - Manual Entry + Date";
                    lblDepartment.Visibility = Visibility.Collapsed;
                    lblManual.Visibility = Visibility.Visible;
                    txtManual.Visibility = Visibility.Visible;
                    txtManual.Focus();
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DescManual:
                    txtTitle.Text += " - Description + Manual Entry";
                    pnlInvoice.Visibility = Visibility.Collapsed;
                    lblManual.Visibility = Visibility.Visible;
                    txtManual.Visibility = Visibility.Visible;
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.Desc2Year:
                    txtTitle.Text = " - Invoice Number + 2 Digit Year";
                    break;
                default:
                    return;
            }

        }

        private void frmInvoiceNumber_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Caller.invDistFlag = true;
            this.Close();
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            Caller.invDateFlag = (chkDate.IsChecked.Value ? true : false);
            Caller.invCheckFlag = (chkCheck.IsChecked.Value ? true : false);
            this.Close();
        }

        // Properties
        public int EntryType { get; set; }
        public int CheckNumber { get { return int.Parse(txtCheck.Text); }  set { txtCheck.Text = value.ToString(); } } 
        public DateTime CheckDate { get { return (DateTime)dtCheck.SelectedDate; } set { dtCheck.SelectedDate = value; } }
        public bool PrePaid { get; set; }
        public string Memo { get; set; } = "";
        public frmBatchInvoices Caller { get; set; }
        public bool CancelRun { get; set; }
        public bool Skip { get; set; }

        private void cmdSkip_Click(object sender, RoutedEventArgs e)
        {
            switch (EntryType)
            {
                case (int)BatchInvoiceOptions.InvoiceOptions.Manual:
                    txtManual.Text = "";
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DptDate:
                    dtInvoice.SelectedDate = GlobalShared.NULL_DATE;
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DescDate:
                    dtInvoice.SelectedDate = GlobalShared.NULL_DATE;
                    break;
                case (int)BatchInvoiceOptions.InvoiceOptions.DescManual:
                    txtManual.Text = "";
                    break;
            }
            Skip = true;
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelRun = true;
            txtManual.Text = "";
            dtInvoice.SelectedDate = GlobalShared.NULL_DATE;
            this.Close();
        }
    }
}
