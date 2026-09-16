using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ExtensionMethods;


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmBatchReports.xaml
    /// </summary>
    public partial class frmBatchReports : Window
    {
        private List<string> ReportNames;
        private List<FixedDocument> Documents;
        BatchReportsParms parms;
        Classes cls;
        PrinterQueues Printers;
        System.Printing.PrintQueue SelectedPrinter = null;
        string thisApp;
        ArrayList ItemList;
        int batchKey = 0;
        BatchRptList BatchList = null;
        BatchReports[] reports;
        bool AdjustDates = false;
        DateTime newStartDate;
        DateTime newEndDate;
        int newMonth;
        int newYear;
        bool tabReportsLast = false;
        bool email;
        int target;
        MbcsCentral.Email.frmEmailAttachment frmEmail = null;
        public bool wait { get; set; }

        public frmBatchReports(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        private void frmBatchReports_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            cls = Classes.load(this);
            if (cls != null)
                parms = BatchReportsParms.DeSerialize(cls.value);
            else
            {
                parms = new BatchReportsParms();
                cls = new Classes();
                cls.name = this.GetType().Name;
            }
            this.DataContext = parms;
            dtStartDate.SelectedDate = parms.FromDate;
            dtEndDate.SelectedDate = parms.ToDate;
            lstMonths.SelectedIndex = parms.ReportMonth;
            Printers = new PrinterQueues();
            lstPrinter.ItemsSource = Printers.Printers;

            for (int i = 0; i < lstPrinter.Items.Count; i++)
            {
                if (lstPrinter.Items[i].ToString().Equals(parms.DefaultPrinter))
                {
                    lstPrinter.SelectedIndex = i;
                    SelectedPrinter = Printers.Queue(i);
                    break;
                }
            }
        }

        private void frmBatchReports_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (lstPrinter.SelectedIndex >= 0)
                parms.DefaultPrinter = (string)((ListItem)lstPrinter.SelectedItem).DisplayItem;
            parms.FromDate = (DateTime)dtStartDate.SelectedDate;
            parms.ToDate = (DateTime)dtEndDate.SelectedDate;
            parms.ReportMonth = lstMonths.SelectedIndex;
            if (lstYears.SelectedIndex > - 1)
                parms.ReportYear = (int)lstYears.SelectedItem;
            cls.value = parms.Serialize();
            cls.save();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(thisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            if (BatchList == null)
            {
                InputTextBox.Text = "";
                InputBox.Visibility = Visibility.Visible;
                InputBox.Focus();
                InputTextBox.Focus();
                return;
            }
            else
            {
                string xsql = "DELETE FROM " + BatchReports.TABLE + " WHERE " + BatchReports.SQL_COL_BATCH_KEY + " = " + BatchList.Key.ToString();
                BatchReports.Query(xsql, GlobalShared.CN);
            }
            if (BatchList != null)
            {
                int c = 0;
                int key = BatchList.Key;
                foreach (ListItem item in lstReports.Items)
                {
                    if (item.Value != null && !((string)item.Value).Trim().Equals(""))
                    {
                        BatchReports rpt = new BatchReports()
                        { BatchKey = key, ReportType = item.DisplayItem.Substring(0, 2), Report = (string)item.Value };
                        rpt.save();
                        c++;
                    }
                }
                MessageBoxCustom.Show(c.ToString() + " report items saved.");
            }
            BatchList = null;
            lstReports.ItemsSource = null;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdEmail_Click(object sender, RoutedEventArgs e)
        {
            //cmdPrint_Click("EMAIL", e);
            if (MessageBoxCustom.Show("Ready to email this batch?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                return;
            email = true;
            target = 3;
            string[] ReportInfo = { BatchList.ListName };
            frmEmail = new MbcsCentral.Email.frmEmailAttachment() { ReportInfo = ReportInfo };
            frmEmail.Batch = true;
            frmEmail.BatchReports = this;
            frmEmail.Topmost = true;
            frmEmail.ShowDialog();
       }

        public void StartReportProcessing()
        {
            cmdPrint_Click("EMAIL", new RoutedEventArgs());
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            //ArrayList addresses = null;
            List<FixedDocument> documents = null;
            if (target != 3)
            {
                if (MessageBoxCustom.Show("Ready to print this batch?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                    return;
                if (lstPrinter.SelectedIndex < 0)
                {
                    MessageBoxCustom.Show("A printer must be selected.");
                    return;
                }
            }

            ReportNames = new List<string>();
            Documents = new List<FixedDocument>();
            FixedDocument doc = null;
            System.Printing.PrintQueue queue = Printers.Queue(lstPrinter.SelectedIndex);
            if (!email && chkDuplex.IsChecked.Value)
            {
                queue.DefaultPrintTicket.Duplexing = System.Printing.Duplexing.TwoSidedLongEdge;
                queue.UserPrintTicket.Duplexing = System.Printing.Duplexing.TwoSidedLongEdge;
            }
            else if (!email)
            {
                queue.DefaultPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
                queue.UserPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
            }
            foreach (ListItem item in lstReports.Items)
            {
                switch (item.DisplayItem.Substring(0, 2))
                {
                    case "BS":
                        BalanceSheet rptBs = BalanceSheet.DeSerialize(item.Value.ToString());
                        rptBs.Target = target;
                        doc = rptBs.print_report(queue);
                        if (email)
                        {
                            ReportNames.Add("BalanceSheet_" + rptBs.CompanyName.Trim() + "_" + new DateTime(2000, rptBs.Month + 1,1 ).ToString("MMM"));
                            Documents.Add(doc);
                        }
                        break;
                    case "CR":
                        CheckRegister rptCR = CheckRegister.DeSerialize(item.Value.ToString());
                        rptCR.Printer = lstPrinter.SelectedItem.ToString();
                        rptCR.Target = target;
                        if (email)
                            doc = rptCR.print_report(target);
                        else
                            doc = rptCR.print_report(queue);
                        if (email)
                        {
                            ReportNames.Add("CheckRegister_" + rptCR.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                    case "GL":
                        GeneralLedgerReport rptGL = GeneralLedgerReport.DeSerialize(item.Value.ToString());
                        rptGL.Printer = lstPrinter.SelectedItem.ToString();
                        rptGL.Target = target;
                        try
                        {
                            doc = rptGL.process(queue);
                        }
                        catch(Exception ex)
                        {
                            //GlobalShared.Log.Log((int)LogClass.logType.Info, "ERROR: " + ex.Message, false);

                        }
                        //GlobalShared.Log.Log((int)LogClass.logType.Info, "GL: Doc assigned", false);
                        if (email)
                        {
                            ReportNames.Add("GeneralLedger_" + rptGL.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                    case "JR":
                        JournalReport rptJr = JournalReport.DeSerialize(item.Value.ToString());
                        rptJr.Printer = lstPrinter.SelectedItem.ToString();
                        rptJr.Target = target;
                        if (email)
                            doc = rptJr.process(target);
                        else
                            rptJr.process(queue);
                        if (email)
                        {
                            ReportNames.Add(rptJr.JournalName + "_" + rptJr.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                    case "OS":
                        OperatingStatement rptOs = OperatingStatement.DeSerialize(item.Value.ToString());
                        rptOs.BatchReport = true;
                        rptOs.Target = target;
                        //if (email)
                        //    doc = rptOs.process(target);
                        //else
                            doc = rptOs.process(queue);
                        if (email)
                        {
                            ReportNames.Add(rptOs.StatementName + "_" + rptOs.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                    case "TB":
                        TrialBalance rptTb = TrialBalance.DeSerialize(item.Value.ToString());
                        rptTb.BatchReport = true;
                        if (email)
                            doc = rptTb.print_report(target);
                        else
                            rptTb.print_report(queue);
                        if (email)
                        {
                            ReportNames.Add("TrialBalance_" + rptTb.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                    case "DR":
                        DisbursementReport rptDr = DisbursementReport.DeSerialize(item.Value.ToString());
                        rptDr.BatchReport = true;
                        rptDr.Target = target;
                        if (email)
                            rptDr.print_report(target);
                        else
                            rptDr.print_report(queue);
                        if (email)
                        {
                            ReportNames.Add("DisbursementReport_" + rptDr.CompanyName.Trim());
                            Documents.Add(doc);
                        }
                        break;
                }
                ListViewItem itm = (ListViewItem)lstReports.ItemContainerGenerator.ContainerFromItem(item);
                itm.Foreground = Brushes.LightGray;
            }
            if (!email)
            {
                queue.DefaultPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
                queue.UserPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
            }
            if (email)
            {
                frmEmail.ReportNames = (string[])ReportNames.ToArray();
                frmEmail.Documents = Documents;
                //frmEmail.MessageSend();
                //frmEmail.ShowDialog();
            }
        }

        private void cmdRecall_Click(object sender, RoutedEventArgs e)
        {
            tabReportsList.Focus();
        }

        private void cmdCheckRegister_Click(object sender, RoutedEventArgs e)
        {
            frmCheckRegSelection frm = new frmCheckRegSelection() { Batch = true, BatchKey = batchKey, BatchType = "CR", newBatch = true };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("CR", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;

            }
        }

        private void cmdGeneralLedger_Click(object sender, RoutedEventArgs e)
        {
            frmGeneralLedger frm = new frmGeneralLedger() { Batch = true, BatchKey = batchKey, BatchType = "GL", NewBatch = true };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("GL", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;
            }
        }

        private void cmdOperatingStatement_Click(object sender, RoutedEventArgs e)
        {
            frmOperatingStatement frm = new frmOperatingStatement() { Batch = true, BatchKey = batchKey, BatchType = "OS", NewBatch = true };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("OS", (string)item.Value);
                ItemList.Add(item);
                lstReportList.ItemsSource = ItemList;
            }
        }

        private void cmdBalanceSheet_Click(object sender, RoutedEventArgs e)
        {
            frmBalanceSheet frm = new frmBalanceSheet() { Batch = true, BatchKey = batchKey, NewBatch = true, BatchType = "BS" };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("BS", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;
            }

        }

        private void cmdJournalReport_Click(object sender, RoutedEventArgs e)
        {
            frmJournalReport frm = new frmJournalReport() { Batch = true, BatchKey = batchKey, BatchType = "JR" , NewBatch = true};
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("JR", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;
            }
        }

        private void cmdTrialBalance_Click(object sender, RoutedEventArgs e)
        {
            frmTrialBalance frm = new frmTrialBalance() { Batch = true, BatchKey = batchKey, BatchType = "TB", NewBatch = true };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("TB", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;
            }
        }

        private void cmdDisbursements_Click(object sender, RoutedEventArgs e)
        {
            frmDisbursementReport frm = new frmDisbursementReport() { Batch = true, BatchKey = batchKey, BatchType = "DR", NewBatch = true };
            frm.ShowDialog();
            if (!frm.Cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                ListItem item = new ListItem();
                item.ValueItem = (long)frm.BatchReport.ID;
                item.Value = frm.BatchReport.Report;
                item.DisplayItem = DisplayName("DR", (string)item.Value);
                ItemList.Add(item);
                lstReports.ItemsSource = ItemList;
            }
        }

        private void UnpackReports()
        {
            lstReports.ItemsSource = null;
            if (ItemList == null)
                ItemList = new ArrayList();
            else
                ItemList.Clear();
            foreach (BatchReports rpt in reports)
            {
                ListItem item = new ListItem();
                if (AdjustDates)
                    AdjustReportDates(rpt);
                item.Value = rpt.Report;
                item.ValueItem = (long)rpt.ID;
                item.DisplayItem = DisplayName(rpt.ReportType, (string)item.Value);
                ItemList.Add(item);
            }
            txtTitle.Text = "Batch Reporting <" + BatchList.ListName + ">";
            lstReports.ItemsSource = ItemList;
        }

        private void AdjustReportDates(BatchReports rpt)
        {
            switch (rpt.ReportType)
            {
                case "BS":
                    BalanceSheet bs = BalanceSheet.DeSerialize(rpt.Report);
                    if (bs != null)
                    {
                        bs.setDates(newMonth , newYear);
                        rpt.Report = bs.Serialize();
                        rpt.save();
                    }
                    break;
                case "CR":
                    CheckRegister cr = CheckRegister.DeSerialize(rpt.Report);
                    if (cr != null)
                    {
                        cr.setDates(newStartDate, newEndDate);
                        rpt.Report = cr.Serialize();
                        rpt.save();
                    }
                    break;
                case "GL":
                    GeneralLedgerReport gl = GeneralLedgerReport.DeSerialize(rpt.Report);
                    if (gl != null)
                    {
                        gl.setDates(newStartDate, newEndDate);
                        rpt.Report = gl.Serialize();
                        rpt.save();
                    }
                    break;
                case "JR":
                    JournalReport jr = JournalReport.DeSerialize(rpt.Report);
                    if (jr != null)
                    {
                        jr.setDates(newStartDate, newEndDate);
                        rpt.Report = jr.Serialize();
                        rpt.save();
                    }
                    break;
                case "OS":
                    OperatingStatement os = OperatingStatement.DeSerialize(rpt.Report);
                    if (os != null)
                    {
                        os.setDates(newMonth, newYear);
                        rpt.Report = os.Serialize();
                        rpt.save();
                    }
                    break;
                case "TB":
                    TrialBalance tb = TrialBalance.DeSerialize(rpt.Report);
                    if (tb != null)
                    {
                        tb.setDates(newMonth, newYear);
                        rpt.Report = tb.Serialize();
                        rpt.save();
                    }
                    break;
                case "DR":
                    DisbursementReport dr = DisbursementReport.DeSerialize(rpt.Report);
                    if (dr != null)
                    {
                        dr.setDates(newMonth, newYear);
                        rpt.Report = dr.Serialize();
                        rpt.save();
                    }
                    break;
            }
        }

        private string DisplayName(string type, string report)
        {
            string s = "";
            switch (type.ToUpper())
            {
                case "BS":
                    BalanceSheet bs = BalanceSheet.DeSerialize(report);
                    int year = bs.Year == 0 || bs.Year == DateTime.Now.Year ? DateTime.Now.Year : DateTime.Now.Year - 1;
                    int month = bs.Month >= 0 && bs.Month <= 11 ? bs.Month : DateTime.Now.Month - 1;
                    s = "BS: " + bs.CompanyName.Left(20) + ", " + new DateTime(DateTime.Now.Year, month + 1, 1).ToString("MMMM") + " " + year.ToString("##00") + ", using: " + (string)(bs.PLAccountName.Trim().Equals("") ? "NO P/L Account" : bs.PLAccountName);
                    break;
                case "CR":
                    CheckRegister cr = CheckRegister.DeSerialize(report);
                    s = "CR: " + cr.CompanyName.Left(20) + "/" + cr.DepartmentName.Left(20) + ", " + cr.StartDate.ToString("MM/dd/yyyy") + "-" + cr.EndDate.ToString("MM/dd/yyyy");
                    break;
                case "GL":
                    GeneralLedgerReport gl = GeneralLedgerReport.DeSerialize(report);
                    s = "GL: " + gl.CompanyName.Left(20) + "/" + gl.DepartmentName.Left(20) + ", " + gl.StartDate.ToShortDateString() + "-" + gl.EndDate.ToShortDateString();
                    break;
                case "JR":
                    JournalReport jr = JournalReport.DeSerialize(report);
                    s = "JR: " + jr.CompanyName.Left(20) + "/" + jr.DepartmentName.Left(20) + ", " + jr.StartDate.ToShortDateString() + "-" + jr.EndDate.ToShortDateString() + ", " + jr.Layout;
                    break;
                case "OS":
                    OperatingStatement os = OperatingStatement.DeSerialize(report);
                    s = "OS: " + os.CompanyName.Left(20) + ", " + os.StatementName.Left(25) + ", " + new DateTime(os.Year, os.Month, 1).ToString("MMMM yyyy");
                    break;
                case "TB":
                    TrialBalance tb = TrialBalance.DeSerialize(report);
                    s = "TB: " + tb.CompanyName.Left(20) + "/" + tb.DepartmentName.Left(20) + ", " + new DateTime(tb.Year, tb.Month, 1).ToString("MMMM yyyy");
                    break;
                case "DR":
                    DisbursementReport dr = DisbursementReport.DeSerialize(report);
                    s = "DR: " + dr.CompanyName.Left(20) + "/" + dr.DepartmentName.Left(20) + ", " + new DateTime(dr.rptYear, dr.rptMonth, 1).ToString("MMMM yyyy");
                    break;
            }

            return s;
        }

        private string CompanyName(int id)
        {
            Company cmp = Company.loadByCompany(id);
            if (cmp != null)
                return cmp.Name;
            else
                return "*CMP NOT FOUND*";
            ;
        }

        private string DepartmentName(int id)
        {
            Department dpt = Department.loadByDepartment((short)id);
            if (dpt != null)
                return dpt.Name;
            else
                return "*DPT NOT FOUND*"
;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tab = (TabControl)sender;
            if (tabReportsList.IsSelected && !tabReportsLast)
            {
                lstReportList.ItemsSource = null;
                BatchRptList[] ReportList = BatchRptList.load();
                lstReportList.ItemsSource = ReportList;
                tabReportsLast = true;
            }
            else
                tabReportsLast = false;

        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Trim().Equals(""))
            {
                MessageBoxCustom.Show("A Batch Report Name must be entered.");
                return;
            }
            BatchList = new BatchRptList() { ListName = InputTextBox.Text };
            if (!BatchList.save())
            {
                BatchList = null;
                MessageBoxCustom.Show("Unable to create new batch.");
            }
            else
                cmdSave_Click(sender, e);

            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InputTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Space)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (lstReportList.SelectedIndex >= 0)
            {
                ListViewItem_MouseDoubleClick(lstReportList.SelectedItem, null);
                /*
                lstReports.ItemsSource = null;
                BatchRptList rpt = (BatchRptList)lstReportList.SelectedItem;
                if (rpt != null)
                {
                    reports = BatchReports.loadByKey(rpt.Key);
                    UnpackReports();
                    tabReports.Focus();
                }
                */
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BatchList = (BatchRptList)lstReportList.SelectedItem;
            if (BatchList != null)
            {
                target = 1;
                //BatchList = (BatchRptList)item;
                reports = BatchReports.loadByKey(BatchList.Key);
                batchKey = BatchList.Key;
                if (chkAdjustDates.IsChecked.Value)
                {
                    bdrDates.Visibility = Visibility.Visible;
                    //dtStartDate.DisplayDate = new DateTime(DateTime.Now.Year, 1, 1);
                    //dtStartDate.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);

                    //dtEndDate.DisplayDate = DateTime.Now;
                    //dtEndDate.SelectedDate = DateTime.Now;

                    //lstMonths.SelectedIndex = DateTime.Now.Month - 1;
                    lstYears.Items.Clear();
                    int j = 0;
                    for (int i = DateTime.Now.Year - 25; i < DateTime.Now.Year + 1; i++)
                    {
                        lstYears.Items.Add(i);
                        if (i == parms.ReportYear)
                            lstYears.SelectedIndex = j;
                        else if (lstYears.SelectedIndex < 0 && i == DateTime.Now.Year)
                            lstYears.SelectedIndex = j;
                        j++;
                    }
                    dtStartDate.Focus();
                }
                else
                {
                    UnpackReports();
                    tabReports.Focus();
                }
            }
        }

        private void ctxEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstReports.SelectedItem == null || lstReports.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("A report was not selected.");
                return;
            }
            System.Windows.Window frm = null;
            bool cancel = false;
            object report = null;
            ListItem item = (ListItem)lstReports.SelectedItem;
            string ReportType = ((string)(item.DisplayItem)).Substring(0, 2);
            switch (ReportType)
            {
                case "BS":
                    frm = new frmBalanceSheet() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmBalanceSheet)frm).Cancel;
                    report = ((frmBalanceSheet)frm).BatchReport.Report;
                    batchKey = ((frmBalanceSheet)frm).BatchReport.BatchKey;
                    break;
                case "CR":
                    frm = new frmCheckRegSelection() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmCheckRegSelection)frm).Cancel;
                    report = ((frmCheckRegSelection)frm).BatchReport.Report;
                    batchKey = ((frmCheckRegSelection)frm).BatchReport.BatchKey;
                    break;
                case "GL":
                    frm = new frmGeneralLedger() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmGeneralLedger)frm).Cancel;
                    report = ((frmGeneralLedger)frm).BatchReport.Report;
                    batchKey = ((frmGeneralLedger)frm).BatchReport.BatchKey;
                    break;
                case "JR":
                    frm = new frmJournalReport() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmJournalReport)frm).Cancel;
                    report = ((frmJournalReport)frm).BatchReport.Report;
                    batchKey = ((frmJournalReport)frm).BatchReport.BatchKey;
                    break;
                case "OS":
                    frm = new frmOperatingStatement() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmOperatingStatement)frm).Cancel;
                    report = ((frmOperatingStatement)frm).BatchReport.Report;
                    batchKey = ((frmOperatingStatement)frm).BatchReport.BatchKey;
                    break;
                case "TB":
                    frm = new frmTrialBalance() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmTrialBalance)frm).Cancel;
                    report = ((frmTrialBalance)frm).BatchReport.Report;
                    batchKey = ((frmTrialBalance)frm).BatchReport.BatchKey;
                    break;
                case "DR":
                    frm = new frmDisbursementReport() { Batch = true, BatchKey = (int)item.ValueItem, BatchType = ReportType };
                    frm.ShowDialog();
                    cancel = ((frmDisbursementReport)frm).Cancel;
                    report = ((frmDisbursementReport)frm).BatchReport.Report;
                    batchKey = ((frmDisbursementReport)frm).BatchReport.BatchKey;
                    break;
            }
            if (!cancel)
            {
                if (ItemList == null)
                    ItemList = new ArrayList();
                lstReports.ItemsSource = null;
                item.Value = report;
                item.DisplayItem = DisplayName(ReportType, (string)item.Value);
                reports = BatchReports.loadByKey(batchKey);
                UnpackReports();
            }
        }
        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstReports.SelectedItem == null || lstReports.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("A report was not selected.");
                return;
            }
            if (MessageBoxCustom.Show("Delete this report?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (lstReports.SelectedItem == null || lstReports.SelectedIndex < 0)
                {
                    MessageBoxCustom.Show("A report was not selected.");
                    return;
                }
                int idx = lstReports.SelectedIndex;
                bool result = reports[idx].delete(reports[idx].ID.ToString());
                if (result)
                {
                    reports = BatchReports.loadByKey(reports[idx].BatchKey);
                    UnpackReports();
                }
                else
                    MessageBoxCustom.Show("Unable to delete the report.");
            }

        }

        //Height="{Binding ElementName=bdrList, Path=Height}"

        private void lstReports_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ctxEdit_Click(sender, new RoutedEventArgs());
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            BatchList = null;
            reports = null;
            if (ItemList != null)
                ItemList.Clear();
            lstReports.ItemsSource = null;
            txtTitle.Text = "Batch Reporting";
        }

        private void cmdAdjSave_Click(object sender, RoutedEventArgs e)
        {
            AdjustDates = true;
            newStartDate = (DateTime)dtStartDate.SelectedDate;
            newEndDate = (DateTime)dtEndDate.SelectedDate;
            newMonth = lstMonths.SelectedIndex + 1;
            newYear = (int)lstYears.SelectedItem;
            parms.FromDate = newStartDate;
            parms.ToDate = newEndDate;
            parms.ReportMonth = newMonth;
            parms.ReportYear = newYear;
            UnpackReports();
            tabReports.Focus();
            bdrDates.Visibility = Visibility.Collapsed;
        }

        private void cmdAdjCancel_Click(object sender, RoutedEventArgs e)
        {
            AdjustDates = false;
            //UnpackReports();
            //tabReports.Focus();
            bdrDates.Visibility = Visibility.Collapsed;
        }

        private void lstReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstReportList.SelectedIndex;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstReportList.SelectedIndex > -1)
            {
                if (MessageBoxCustom.Show("Delete the selected Report Batch?", "",null,MessageBoxButton.YesNo, this) == MessageBoxResult.Yes)
                {
                    BatchRptList rpt = (BatchRptList)lstReportList.SelectedItem;
                    rpt.delete(rpt.Key.ToString());
                    lstReportList.ItemsSource = null;
                    BatchRptList[] ReportList = BatchRptList.load();
                    lstReportList.ItemsSource = ReportList;
                    tabReportsLast = true;
                }
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            tabReports.Focus();
        }
    }
}
