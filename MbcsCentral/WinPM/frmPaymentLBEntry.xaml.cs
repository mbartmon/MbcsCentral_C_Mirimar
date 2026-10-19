using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmPaymentLBEntry.xaml
    /// </summary>
    public partial class frmPaymentLBEntry : Window
    {
        // The OCR Scan Line:
        // ZZZZZZBBBTTTTDDDDDDAAAAAAA
        // Z = Zero Fill
        // B = Building number (6, 3)
        // T = Tenant number   (9, 4)
        // D = Payment date    (13, 6)
        // A = Net Due      (19, 7)
        /*
        int SCANSTART = 11
        int BLD = 6;
        int TEN = 9;
        int DATE = 13;
        int AMT = 19;
        */

        int PayType = 0;

        // Lockbox
        int LB_SCANSTART = 28;
        int LB_SCANLINE_LENGTH = 28;
        int LB_BATCH = 1;
        int LB_BLD = 5;
        int LB_TEN = 8;
        int LB_MONTH = 24;
        int LB_DAY = 26;
        int LB_AMT = 11;
        int LB_AMT_LENGTH = 9;
        const string SUB_REGISTRY_KEY_LB  = "LastProcessedFile";

        // AR
        int AR_SCANLINE_LENGTH = 281;
        int AR_SCANSTART = 0;
        int AR_BATCH = 24;
        int AR_BLD = 25;
        int AR_TEN = -1;
        int AR_MONTH = 80;
        int AR_DAY = 82;
        int AR_AMT = 43;
        int AR_AMT_LENGTH = 8;
        const string SUB_REGISTRY_KEY_AR = "LastProcessedAR";

        // RPS
        int RP_SCANLINE_LENGTH = 28;
        int RP_SCANSTART = 28;
        int RP_BATCH = 1;
        int RP_BLD = 5;
        int RP_TEN = 8;
        int RP_MONTH = 24;
        int RP_DAY = 26;
        int RP_AMT = 11;
        int RP_AMT_LENGTH = 9;
        const string SUB_REGISTRY_KEY_RPS = "LastProcessedRPS";


        // Current Run
        int SCANLINE_LENGTH;
        int SCANSTART;
        int BATCH;
        int BLD;
        int TEN;
        int MONTH;
        int DAY;
        int AMT;
        int AMT_LENGTH;

        DateTime ProcessDate = DateTime.Now;
        ArrayList Batches;
        frmReport Historyfrm;
        private string thisApp;

        ArrayList list = new ArrayList();

        const string MAIN_REGISTRY_KEY = "MbcsCentral\\Forms\\formPaymentLBEntry";
        const string SUB_REGISTRY_KEY = "Form";
        const string SUB_REGISTRY_ADDRESS = "UseLBDate";
        string SUB_REGISTRY_FILE = "LastProcessedFile";

        public string LastProcessedFile { get; set; } = "";

        public frmPaymentLBEntry()
        {
            InitializeComponent();
        }

        public frmPaymentLBEntry(string app, string paytype = "0")
        {
            thisApp = app;
            InitializeComponent();
            PayType = int.Parse(paytype);
        }

        private void frmPaymentLBEntry_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            switch (PayType)
            {
                case (int)LockboxHistory.PaymentType.Lockbox:
                    SCANLINE_LENGTH = LB_SCANLINE_LENGTH;
                    SCANSTART = LB_SCANSTART;
                    BATCH = LB_BATCH;
                    BLD = LB_BLD;
                    TEN = LB_TEN;
                    MONTH = LB_MONTH;
                    DAY = LB_DAY;
                    AMT = LB_AMT;
                    AMT_LENGTH = LB_AMT_LENGTH;
                    SUB_REGISTRY_FILE = SUB_REGISTRY_KEY_LB;

                    break;

                case (int)LockboxHistory.PaymentType.AR:
                    SCANLINE_LENGTH = AR_SCANLINE_LENGTH;
                    SCANSTART = AR_SCANSTART;
                    BATCH = AR_BATCH;
                    BLD = AR_BLD;
                    TEN = AR_TEN;
                    MONTH = AR_MONTH;
                    DAY = AR_DAY;
                    AMT = AR_AMT;
                    AMT_LENGTH = AR_AMT_LENGTH;
                    SUB_REGISTRY_FILE = SUB_REGISTRY_KEY_AR;

                    tabTransfer.Header = "Remittance File";
                    chkFileDate.Content = "Use Remittance Deposit Date";
                    lblLastFile.Text = "Remittance File:";
                    this.Title = "Remittance Processing";
                    txtBanner.Text = "Remittance Processing";

                    break;

                case (int)LockboxHistory.PaymentType.RPS:
                    SCANLINE_LENGTH = RP_SCANLINE_LENGTH;
                    SCANSTART = RP_SCANSTART;
                    BATCH = RP_BATCH;
                    BLD = RP_BLD;
                    TEN = RP_TEN;
                    MONTH = RP_MONTH;
                    DAY = RP_DAY;
                    AMT = RP_AMT;
                    AMT_LENGTH = RP_AMT_LENGTH;

                    SUB_REGISTRY_FILE = SUB_REGISTRY_KEY_RPS;
                    tabTransfer.Header = "RPS File";
                    chkFileDate.Content = "Use RPS Deposit Date";
                    lblLastFile.Text = "RPS File:";
                    this.Title = "RPS Processing";
                    txtBanner.Text = "RPS Processing";

                    break;
            }

            dtProcessingDate.SelectedDate = DateTime.Now;
            dtProcessingDate.Text = DateTime.Now.ToShortDateString();

            object obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_KEY, "1", true);
            if (obj != null && obj.ToString().Trim().Equals("2"))
                chkFileDate.IsChecked = true;
            else
                chkFileDate.IsChecked = false;

            LastProcessedFile = (string) Main.GetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_FILE, "", true);
            txtLastFile.Text = LastProcessedFile;

        }

        private void frmPaymentLBEntry_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            string form = (chkFileDate.IsChecked.Value ? "2" : "1");
            Main.SetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_KEY, form, (int)Registry.DataTypes.String, true);
            Main.SetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_FILE, LastProcessedFile, (int)Registry.DataTypes.String, true);
            if (Historyfrm != null)
                Historyfrm.Close();
            Paytemp[] pmts = Paytemp.loadByUser();
            if (pmts != null && pmts.Length > 0)
            {
                int drc;
                drc = (int)MessageBox.Show("You have " + pmts.Length.ToString() + " unreported payments.  Print the Payment Register now?", "Payment Register", MessageBoxButton.YesNoCancel);
                switch (drc)
                {
                    case (int)MessageBoxResult.Yes:
                        {
                            frmPaymentRegister frm = new frmPaymentRegister() { LockBox = LockboxHistory.FileType[PayType] };
                            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            frm.Show();
                            break;
                        }
                    case (int)MessageBoxResult.Cancel:
                        {
                            return;
                            break;
                        }
                }
            }
        }

        private bool ProcessFile(string file)
        {
            if (LastProcessedFile.ToLower().Equals(file.Substring(file.LastIndexOf("\\")).ToLower()))
            {
                if (MessageBoxCustom.Show("The specified " + LockboxHistory.FileType[PayType] + "file has already been processed.  Process anyway?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
            }
            StreamReader sr;
            try
            {
                sr = new StreamReader(file);
            }
            catch (IOException iox)
            {
                MessageBoxCustom.Show("Error opening " + LockboxHistory.FileType[PayType] + " file: " + iox.Message, "", MessageBoxButton.OK, this);
                return false;
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show("Error processing " + LockboxHistory.FileType[PayType] + " file: " + ex.Message, "", MessageBoxButton.OK, this);
                return false;
            }

            if (chkFileDate.IsChecked.Value)
                ProcessDate = DateTime.Now;
            else
                ProcessDate = (DateTime)dtProcessingDate.SelectedDate;

            string line = "";
            string scanLine = "";
            int year = 0;
            int month = 0;
            int day = 0;
            decimal amount = 0;
            decimal totalAmount = 0;
            decimal lostAmount = 0;
            Batches = new ArrayList();
            decimal BatchTotal = 0;
            int batchcount = 0;
            string LastBatch = "";
            string CurrentBatch = "";
            int sequence = 0;
            string building = "";
            string tenant = "";

            // Record Counter
            int rcount = 0;
            // Processed Record Counter
            int pcount = 0;
            // ERROR COUNTERS
            int SeqMismatch = 0;
            ArrayList seqMismatchList = new ArrayList();
            int ZeroAmount = 0;
            ArrayList zeroAmountList = new ArrayList();
            int ScanLineShort = 0;
            ArrayList scanLineShortList = new ArrayList();
            ArrayList ScanLines = new ArrayList();

            while ((line = sr.ReadLine()) != null)
            {
                if (PayType == (int)LockboxHistory.PaymentType.AR )
                {
                    if (line.Substring(0, 1).Equals("5"))
                    {
                        CurrentBatch = line.Substring(BATCH, 7);
                        continue;
                    }
                    if (line.Substring(0, 1).Equals("8"))
                    {
                        if (!LastBatch.Trim().Equals(""))
                        {
                            Batches.Add(LastBatch + ": " + BatchTotal.ToString("###,###,##0.00").PadLeft(15) + " - " + batchcount);
                            BatchTotal = 0;
                            batchcount = 0;
                        }
                        LastBatch = CurrentBatch;
                        continue;

                    }
                }
                
                if (line.Substring(0, 1).Equals("6")) // Detail Record
                {
                    if (PayType != (int)LockboxHistory.PaymentType.AR)
                    {
                        CurrentBatch = line.Substring(1, 3);
                        if (!CurrentBatch.Trim().Equals(LastBatch))
                        {
                            if (!LastBatch.Trim().Equals(""))
                            {
                                Batches.Add(LastBatch + ": " + BatchTotal.ToString("###,###,##0.00").PadLeft(15) + " - " + batchcount);
                                BatchTotal = 0;
                                batchcount = 0;
                            }
                            LastBatch = CurrentBatch;
                        }
                    }
                    rcount++;
                    batchcount++;
                    scanLine = line.Substring(SCANSTART, SCANLINE_LENGTH);
                    //scanLine = line;
                    if (scanLine.Trim().Length < SCANLINE_LENGTH)
                    {
                        scanLine = scanLine.PadRight(SCANLINE_LENGTH);
                        // ScanLineShort++;
                        //scanLineShortList.Add("ScanLine for record: " + rcount.ToString().PadLeft(5) + " is too short: " + scanLine.Trim().Length.ToString() + " / " + SCANLINE_LENGTH.ToString());
                        //continue;
                    }
                    ScanLines.Add(String.Format("{0,5}: {1}", rcount.ToString() , scanLine.Trim()));
                    // This may be corrected to include leading zero
                    string s = scanLine.Substring(BLD, 7).Trim();
                    if (s.Length < 6)
                    {
                        s = "0" + s;
                    }
                    s = s.PadRight(7);
                    building = s.Substring(0, 3);
                    tenant = s.Substring(3, 4);
                    if (tenant.Substring(3, 1).Equals("0"))
                        tenant = tenant.Substring(0, 3);

                    // No sequence number in this version
                    //sequence = 0;
                    //int.TryParse(scanLine.Substring(0, 6), out sequence);

                    //Tenants ten = Tenants.load(sequence);
                    Tenants[] ten = Tenants.load(building, tenant);


                    year = 0;
                    month = 0;
                    day = 0;

                    string amt = scanLine.Substring(AMT, AMT_LENGTH) + "." + scanLine.Substring(AMT + AMT_LENGTH, 2);
                    amount = 0;
                    decimal.TryParse(amt, out amount);
                    if (amount == 0)
                    {
                        ZeroAmount++;
                        zeroAmountList.Add("Rec # " + rcount.ToString().PadLeft(5) + "Tenant# " + building.Trim() + tenant.Trim() + " has zero amount.");

                        continue;
                    }
                    BatchTotal += amount;
                    if (ten == null || ten.Length == 0)
                    {
                        // report original building and tenant number
                        SeqMismatch++;
                        seqMismatchList.Add("Rec # " + rcount.ToString().PadLeft(5) + " Tenant# " + building.Trim() + tenant.Trim() + " not found. Amount: " + amount.ToString("$###,###,##0.00"));
                        lostAmount += amount;
                        continue;
                    }
                    if (chkFileDate.IsChecked.Value)
                    {
                        //int.TryParse("20" + (scanLine.Substring(13, 2)), out year);
                        int.TryParse(scanLine.Substring(MONTH, 2), out month);
                        int.TryParse(scanLine.Substring(DAY, 2), out day);
                        if (month == 0 || day == 0)
                        {
                            ten[0].DatePaid = (DateTime)dtProcessingDate.SelectedDate;
                        }
                        else
                        {
                            ten[0].DatePaid = new DateTime(DateTime.Now.Year, month, day);
                        }

                    }
                    else
                        ten[0].DatePaid = (DateTime)dtProcessingDate.SelectedDate;

                    ten[0].Payment = amount;
                    totalAmount += amount;
                    list.Add(ten[0]);
                    pcount++;
                }
            }
            sr.Close();
            if (list.Count <= 0)
            {
                MessageBoxCustom.Show("No valid records found out of " + rcount.ToString() + " records found.", "", MessageBoxButton.OK, this);
                return false;
            }
            for (int i = 0; i < ScanLines.Count; i++)
                lstTransfer.Items.Add((string)ScanLines[i]);

            Batches.Add(LastBatch + ": " + BatchTotal.ToString("###,###,##0.00").PadLeft(15) + " - " + batchcount);
            lstReport.Items.Add(LockboxHistory.FileType[PayType] + " DATA PROCESSING REPORT - " + txtLockBoxFile.Text);
            lstReport.Items.Add("==============================");
            lstReport.Items.Add(" ");
            lstReport.Items.Add("Records Found: " + rcount.ToString() + " / Records Processed: " + pcount.ToString());
            lstReport.Items.Add("Amount Processed: " + totalAmount.ToString("$###,###,##0.00") + " / Amount not processed: " + lostAmount.ToString("$###,###,##0.00"));
            if (lostAmount != 0)
                lstReport.Items.Add("Total with difference: " + (totalAmount + lostAmount).ToString("$###,###,##0.00"));
            lstReport.Items.Add("");
            lstReport.Items.Add("Batch Totals");
            lstReport.Items.Add("------------");
            foreach (string b in Batches)
                lstReport.Items.Add(b);
            lstReport.Items.Add("");
            if (ScanLineShort > 0)
            {
                lstReport.Items.Add("Invalid scan lines found.");
                lstReport.Items.Add("----------------------");
                for (int i = 0; i < scanLineShortList.Count; i++)
                    lstReport.Items.Add((string)scanLineShortList[i]);
                lstReport.Items.Add("=======================");
                lstReport.Items.Add("");
            }

            if (SeqMismatch > 0)
            {
                lstReport.Items.Add("Tenants not found.");
                lstReport.Items.Add("----------------------");
                for (int i = 0; i < seqMismatchList.Count; i++)
                    lstReport.Items.Add((string)seqMismatchList[i]);
                lstReport.Items.Add("=======================");
                lstReport.Items.Add("");
            }
            if (ZeroAmount > 0)
            {
                lstReport.Items.Add("Zero amount records found.");
                lstReport.Items.Add("----------------------");
                for (int i = 0; i < zeroAmountList.Count; i++)
                    lstReport.Items.Add((string)zeroAmountList[i]);
                lstReport.Items.Add("=======================");
                lstReport.Items.Add("");
            }
            lstReport.Items.Add("");
            lstReport.Items.Add("Processing Complete.");

            cmdPrint.Visibility = Visibility.Visible;
            tabReport.Focus();
            LockboxHistory[] history = LockboxHistory.loadByDateRange(ProcessDate, ProcessDate, PayType, true);
            if (history != null && history.Length > 0)
            {
                bool found = false;
                LockboxHistory.BatchDetails_struct[] details = LockboxHistory.GetBatchDetails(Batches);

                Report rpt = new Report("LockBoxCompare", "Courier New", (float)12.0) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
                rpt.PrintPageLine(LockboxHistory.FileType[PayType] + " HISTORICAL POSTINGS for: " + ProcessDate.Month.ToString() + "/" + ProcessDate.Year.ToString());
                rpt.PrintPageLine("=======================================================");
                rpt.PrintPageLine();
                foreach(LockboxHistory hist in history)
                {
                    rpt.PrintPageLine(hist.FileName);
                    LockboxHistory.BatchDetails_struct[] items = hist.BatchDetails;
                    bool match = false;

                    rpt.PrintPageLine();
                    foreach(LockboxHistory.BatchDetails_struct item in items)
                    {
                        for (int i = 0; i < details.Length; i++)
                        {
                            if (details[i].BatchNumber.Equals(item.BatchNumber))
                            {
                                match = true;
                                found = true;
                            }
                        }
 

                        rpt.PrintPageLine(new string(' ', 5), false);
                        rpt.PrintPageLine("Batch: " + item.BatchNumber.PadLeft(10) + " Amount: " + item.Amount.ToString("$#,###,##0.00").Trim().PadRight(14) + "  " + "Count: " + item.Count.Trim().PadRight(4), false);
                        if (match)
                        {
                            rpt.FontBold = true;
                            rpt.PrintPageLine("*POSSIBLE MATCH*", false);
                            rpt.FontBold = false;
                        }
                        rpt.PrintPageLine();
                        
                    }
                    rpt.PrintPageLine("=========================================================");
                    rpt.PrintPageLine();
                }
                rpt.DisplayDocument(true);
                System.Windows.Documents.FixedDocument doc = rpt.Document;
                Historyfrm = new frmReport(doc);
                Historyfrm.Top = 0;
                Historyfrm.Show();
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
                var size = HelperMethods.GetHostingScreenSize(screen.DeviceName);
                Historyfrm.Owner = this;
                
                //Historyfrm.ShowDialog();
                //rpt.DisplayDocument();
                if (found)
                    if (MessageBoxCustom.Show("Possible matches to previous posts found.  Do you want to continue processing?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        Historyfrm.Close();
                        tabProcess.IsEnabled = true;
                        cmdSave.Content = "PROCESS";
                        tabProcess.Focus();
                        return false;
                    }
            }       
            cmdSave.Content = "POST";
            cmdCancel.Visibility = Visibility.Visible;
            tabProcess.IsEnabled = false;

            return true;

        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdLockBoxFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                txtLockBoxFile.Text = dlg.FileName;
            }
        }
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            bool TransferReport = Tab.SelectedIndex == 2;
            LockBoxProcessReport rpt = new LockBoxProcessReport() { FileName = txtLockBoxFile.Text };
            ListBox list = (TransferReport ? lstTransfer : lstReport);
            rpt.process(list, TransferReport);
        }
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(thisApp))
            {

                MessageBoxCustom.Show("You are not authorized for this function.", "", MessageBoxButton.OK, this);
                return;
            }
            if (cmdSave.Content.ToString().ToLower().Equals("process"))
            {
                if (txtLockBoxFile.Text.Trim().Equals(""))
                {
                    MessageBoxCustom.Show(LockboxHistory.FileType[PayType] + " file has not been specified.", "", MessageBoxButton.OK, this);
                    return;

                }

                if (!File.Exists(txtLockBoxFile.Text))
                {
                    MessageBoxCustom.Show("The specified LockBox file cannot be found.", "", MessageBoxButton.OK, this);

                    return;
                }

                if (txtLockBoxFile.Text.Substring(txtLockBoxFile.Text.LastIndexOf("\\") + 1).Equals(txtLastFile.Text))
                {
                    if (MessageBoxCustom.Show("File for processing has already been processed.  Do you want to process anyway?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }
                ProcessFile(txtLockBoxFile.Text);
            }
            else if (cmdSave.Content.ToString().ToLower().Equals("post"))
            {
                if (Historyfrm != null)
                    Historyfrm.Close();
                cmdCancel.Visibility = Visibility.Collapsed;
                Tenants[] tenants = (Tenants[])list.ToArray(typeof(Tenants));

                ArrayList over = WinpmGlobal.ApplyRentPayments(tenants, true);
                if (over != null && over.Count > 0)
                {
                    lstTransfer.Items.Clear();
                    for (int i = 0; i < over.Count; i++)
                        lstTransfer.Items.Add(((Tenants)over[i]).BuildingNumber + ((Tenants)over[i]).TenantNumber + " Balance: " + ((Tenants)over[i]).Paid.ToString() + "  Paid: " + ((Tenants)over[i]).Payment.ToString());
                    tabTransfer.Focus();
                }
                LastProcessedFile = txtLockBoxFile.Text.Substring(txtLockBoxFile.Text.LastIndexOf("\\") + 1);
                LockboxHistory history = new LockboxHistory();
                history.Date = ProcessDate;
                history.TimeStamp = DateTime.Now;
                history.User = GlobalShared.user.Account.login;
                history.FileName = LastProcessedFile;
                history.Batches = (string)Batches[0];
                history.Type = PayType;
                for(int i = 1; i < Batches.Count; i++)
                {
                    history.Batches += "|" + (string)Batches[i];
                }
                history.save();
                
                cmdSave.IsEnabled = false;
                tabProcess.IsEnabled = true;
                cmdSave.Content = "PROCESS";
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            tabProcess.IsEnabled = true;
            cmdSave.Content = "PROCESS";
            cmdCancel.Visibility = Visibility.Collapsed;
            tabProcess.Focus();

        }
    }
    public class PaymentLine
    {
        public PaymentLine()
        {

        }

        public string RecordType { get; set; }
        public string BatchNumber { get; set; }
        public string ItemNumber { get; set; }
        public string ScanLine { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime DepositDate { get; set; }
        public string Filler { get; set; }
    }
}
