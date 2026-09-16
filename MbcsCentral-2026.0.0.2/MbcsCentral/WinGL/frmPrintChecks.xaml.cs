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
using System.Printing;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmPrintChecks.xaml
    /// </summary>
    public partial class frmPrintChecks : Window
    {
        //PrinterQueues Printers;
        PrintQueueCollection Printers;
        PrintQueue printer;
        string DefaultPrinter;
        const string PROGRAM_REG_KEY = "\\CheckPrinting\\";
        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public bool NonACH { get; set; } = true;

        public frmPrintChecks(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }

        public frmPrintChecks()
        {
            InitializeComponent();
        }

        private void frmPrintChecks_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            Main.getForm(this);
            DefaultPrinter = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "Printer", "");
            string ChangeCheckNumbers = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "ChangeCheckNumbers", "1");
            chkChangeCheckNumber.IsChecked = (bool)(ChangeCheckNumbers == "0" ? true: false);
            double d = (double)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "VoucherOption", 1);
            switch ((int)d)
            {
                case 0:
                    optBuildingAddress.IsChecked = true;
                    break;
                case 1:
                    optFirstLine.IsChecked = true;
                    break;
                case 2:
                    optPayroll.IsChecked = true;
                    break;
            }
            //Printers = new PrinterQueues();
            Printers = new PrintServer().GetPrintQueues(new[] {
                EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections
            });
            lstPrinters.ItemsSource = Printers;
            for (int i = 0; i < lstPrinters.Items.Count; i++)
            {
                if (((System.Printing.PrintQueue)lstPrinters.Items[i]).FullName.Equals(DefaultPrinter))
                {
                    lstPrinters.SelectedIndex = i;
                    break;
                }
            }
            dtCheckDate.SelectedDate = DateTime.Now;

        }

        private void formPrintChecks_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "Printer", DefaultPrinter, (int)MbcsCentral.Registry.DataTypes.String);
            Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "ChangeCheckNumbers", (chkChangeCheckNumber.IsChecked.Value ? "0" : "1"), (int)MbcsCentral.Registry.DataTypes.String);
            int d = 0;
            if (optBuildingAddress.IsChecked.Value)
                d = 0;
            else if (optFirstLine.IsChecked.Value)
                d = 1;
            else if (optPayroll.IsChecked.Value)
                d = 2;
            Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "VoucherOption", d, (int)MbcsCentral.Registry.DataTypes.Dword);
            Control[] tblControl = Control.load();
            if (tblControl != null && tblControl.Length > 0)
            {
                if (optBuildingAddress.IsChecked.Value)
                    tblControl[0].CheckVoucher = 0;
                else if (optFirstLine.IsChecked.Value)
                    tblControl[0].CheckVoucher = 1;
                else if (optPayroll.IsChecked.Value)
                    tblControl[0].CheckVoucher = 2;
            }
            tblControl[0].CheckChange = (short)(chkChangeCheckNumber.IsChecked.Value ? 0 : 1);
            tblControl[0].save();

        }

        // Print Checks
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            int payroll = 0;
            int vi;

            // Print
            if (lstPrinters.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a printer.");
                return;
            }
            printer = (System.Printing.PrintQueue)lstPrinters.SelectedItem;
            if (optBuildingAddress.IsChecked.Value)
                payroll = 0;
            if (optFirstLine.IsChecked.Value)
                payroll = 2;
            if (optPayroll.IsChecked.Value)
                payroll = 1;
            vi = lstCheckFormat.SelectedIndex;
            if (vi < 0)
            {
                MessageBox.Show("You must select a check format.", "Print A/P Checks");
                return;
            }
            //check number
            bool iflag = chkChangeCheckNumber.IsChecked.Value;
            DateTime tdate = (DateTime)dtCheckDate.SelectedDate;
            DefaultPrinter = printer.FullName;

            frmAPLaser checks = new frmAPLaser() { printer = printer, CheckDate = tdate, payroll = payroll, Iflag = (short)(iflag ? 0 : 1)};
            checks.Owner = this;
            checks.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            checks.ShowDialog();
            this.Close();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void formPrintChecks_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            DefaultPrinter = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "Printer", "");
            string ChangeCheckNumbers = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "ChangeCheckNumbers", "1");
            chkChangeCheckNumber.IsChecked = (bool)(ChangeCheckNumbers == "0" ? true : false);
            double d = (double)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, "VoucherOption", 1);
            switch ((int)d)
            {
                case 0:
                    optBuildingAddress.IsChecked = true;
                    break;
                case 1:
                    optFirstLine.IsChecked = true;
                    break;
                case 2:
                    optPayroll.IsChecked = true;
                    break;
            }
            //Printers = new PrinterQueues();
            Printers = new PrintServer().GetPrintQueues(new[] {
                EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections
            });
            lstPrinters.ItemsSource = Printers;
            for (int i = 0; i < lstPrinters.Items.Count; i++)
            {
                if (((System.Printing.PrintQueue)lstPrinters.Items[i]).FullName.Equals(DefaultPrinter))
                {
                    lstPrinters.SelectedIndex = i;
                    break;
                }
            }
            dtCheckDate.SelectedDate = DateTime.Now;


        }

    }
}
