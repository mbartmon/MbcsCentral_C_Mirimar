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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmPrintDialog.xaml
    /// </summary>
    public partial class frmPrintDialog : Window
    {
        PrinterQueues Printers;
        System.Printing.PrintQueue SelectedPrinter = null;
        System.Printing.PrintCapabilities printerCapabilities;
        ListItem queue;
        bool CanDuplex = false;
        bool doDuplex = false;
        string callingForm = "";

        public frmPrintDialog(string form = "")
        {
            InitializeComponent();
            callingForm = form;
        }

        private void frmPrintDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (!callingForm.Trim().Equals(""))
                GetFormDefaults(callingForm);
            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            DefaultPrinter = settings.PrinterName;
            Printers = new PrinterQueues();
            PrinterList.ItemsSource = Printers.Printers;
            for (int i = 0; i < PrinterList.Items.Count; i++)
            {
                if (PrinterList.Items[i].ToString().Equals(DefaultPrinter))
                {
                    PrinterList.SelectedIndex = i;
                    SelectedPrinter = Printers.Queue(i);
                    //GetPrinterCapabilities(SelectedPrinter);
                    break;
                }
            }
        }

        private void GetPrinterCapabilities(System.Printing.PrintQueue printer)
        {
            
            printerCapabilities = printer.GetPrintCapabilities();
            if (printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedLongEdge) || printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedShortEdge))
            {
                CanDuplex = true;
                chkDuplex.IsEnabled = true;
            }
            else
            {
                CanDuplex = false;
                chkDuplex.IsEnabled = false;
            }
            
        }

        private void PrinterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedPrinter = Printers.Queue(PrinterList.SelectedIndex);
                //GetPrinterCapabilities(SelectedPrinter);
                DefaultPrinter = SelectedPrinter.FullName;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cmdPrint_Clicked(object sender, RoutedEventArgs e)
        {
            if (PrinterList.SelectedIndex == -1)
            {
                MessageBoxCustom.Show("A printer was not selected.");
                return;
            }
            if (!callingForm.Equals(""))
                SetFormDefaults(callingForm);

            queue = (ListItem)PrinterList.SelectedItem;
            Queue = Printers.Queue(queue.ValueItem);

            if (optPageRange.IsChecked.Value)
            {
                if (ValidatePageRange(txtPageRange.Text))
                {
                    UsePageRange = true;
                    PageRange = txtPageRange.Text;
                }
                else
                {
                    MessageBoxCustom.Show("The page range that you entered is invalid.");
                    return;
                }
            }
            if (chkDuplex.IsChecked.Value)
            {
                if (optLong.IsChecked.Value)
                    DuplexType = System.Printing.Duplexing.TwoSidedLongEdge;
                else
                    DuplexType = System.Printing.Duplexing.TwoSidedShortEdge;
            }
            else
                DuplexType = System.Printing.Duplexing.OneSided;
            Queue.UserPrintTicket.Duplexing = DuplexType;
            Queue.DefaultPrintTicket.Duplexing = DuplexType;
            Canceled = false;
            this.Close();
        }

        private void GetFormDefaults(string form)
        {
            if (form.Trim().Equals(""))
                return;
            try
            {
                DefaultPrinter = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Printers\\" + form, "Printer", "");
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "PrintDialog: Unable to retrieve default printer for <" + form + ">. " + ex.Message, false);
                DefaultPrinter = "";
            }
        }

        private bool ValidatePageRange(string range)
        {
            if (range == null || range.Trim().Equals(""))
                return false;

            int number;
            string[] ranges = range.Split(',');
            foreach (string s in ranges)
            {
                if (s.Contains("-"))
                {
                    string[] subS = s.Split('-');
                    if (subS.Length != 2)
                        return false;
                    number = 0;
                    int.TryParse(subS[0], out number);
                    if (number == 0)
                        return false;
                    int.TryParse(subS[1], out number);
                    if (number == 0)
                        return false;
                }
                else
                {
                    number = 0;
                    int.TryParse(s, out number);
                    if (number == 0)
                        return false;
                }
            }
            return true;
        }
        private void SetFormDefaults(string form)
        {
            if (form.Trim() != "")
                Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\Printers\\" + form, "Printer", DefaultPrinter, (int)MbcsCentral.Registry.DataTypes.String);
        }

        public string DefaultPrinter { get; set; }
        public bool Duplex { get { return chkDuplex.IsChecked.Value; } }
        public System.Printing.Duplexing DuplexType { get; set; }
        public int PageOrientation { get; set; }
        public System.Printing.PrintQueue Queue { get; set; } = null;
        public bool UsePageRange { get; set; } = false;
        public string PageRange { get; set; }
        public bool Canceled { get; set; } = true;

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            grpPages.Visibility = UsePageRange ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtPageRange_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            optPageRange.IsChecked = true;
        }
    }
}

