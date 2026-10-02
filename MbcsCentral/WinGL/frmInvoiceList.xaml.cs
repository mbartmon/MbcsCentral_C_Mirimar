using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Xceed;
using Xceed.Wpf.Toolkit;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmInvoiceList.xaml
    /// </summary>
    public partial class frmInvoiceList : Window
    {
        private string invoiceTable = Invoices.TABLE;
        private string distributionTable = InvoiceDistributions.TABLE;
        private Invoices[] _invoices = null;
        private InvoiceDistributions[] _distributions = null;
        string WindocLocation = "";
        bool Highlight = false;
        public Brush RowForegroundGroundBrush { get; set; } = Brushes.Black;
        private bool scan = false;
        const string PROGRAM_REG_KEY = "\\InvoiceList\\";
        const string PROGRAM_REG_SUBKEY = "RowForegroundColor";

        public bool BatchInvoices
        { get; set; } = false;

        public int BatchNumber
        { get; set; } = 0;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmInvoiceList()
        {
            InitializeComponent();
        }
        public frmInvoiceList(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void FrmInvoiceList_OnLoaded(object sender, RoutedEventArgs e)
        {

            invoiceTable = Invoices.BASE_TABLE;
            Control control = Control.loadOne();
            scan = control.Scan;
            Main.getForm(this);
            grdMain.DataContext = this;
            string rowForegroundColor = (string)Main.GetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, PROGRAM_REG_SUBKEY, Brushes.Black.ToString());
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(rowForegroundColor);
            RowForegroundGroundBrush = brush;
            txtPrintedInvoices.Foreground = brush;
            chkHighligt.Background = brush;

            if (BatchInvoices)
            {
                invoiceTable = InvoicesBatch.TABLE;
                distributionTable = InvBatchDistributions.TABLE;
                _invoices = InvoicesBatch.load(invoiceTable);
                _distributions = InvBatchDistributions.load(distributionTable);
            }
            GetInvoices();
            LstInvoices.ColumnFromDisplayIndex(0).SortDirection = ListSortDirection.Ascending;
            // From original: Bypass following code for immediate scanning
            if (scan)
            {
                WindocLocation = control.WindocLocation.Trim();
                /*
                     dbname = GetSetting("Wingl", "Options", "WindocFile", "")
                     If Trim(dbname) <> "" Then
                         Set cnn = New clsConnection
                         rc = cnn.OpenDB("admin", dbname, (adModeShareDenyNone))
                         If rc <> 0 Then
                             MsgBox "Error openning Windoc Database (" & dbname & ") Invoices will not be scanned: " & Err.Description
                             scan = False
                         Else
                             uniqueID = Now()
                             Set rss = New ADODB.Recordset
                             On Error Resume Next
                             Err.Number = 0

                             'PID = Shell(GetSetting("Windoc", "Options", "ProgramPath", "c:\Program Files\MBCS\Windoc\") & "windoc.exe wingl """ & uniqueID & """")
                             PID = Shell(GetPath(GetSetting("Wingl", "Options", "WindocFile", "")) & "windoc.exe wingl """ & uniqueID & """")
                             If PID <= 0 Then
                                 MsgBox "Could not load Windoc. Invoices will not be scanned. PID=" & PID
                                 scan = False
                                 cnn.CloseDB
                             Else
                                 Err.Number = 0
                                 'rss.Open "SELECT * FROM interface", cnn.cn, adOpenDynamic, adLockPessimistic
                                 If Err.Number <> 0 Then
                                     MsgBox "Error openning 'interface' table (_invoices will not be scanned): " & Err.Description
                                     scan = False
                                     cnn.CloseDB
                                 End If
                             End If
                             On Error GoTo Form_Load_error
                             For i = Len(dbname) To 1 Step -1
                                 If Right(dbname, 1) = "\" Then Exit For
                                 dbname = Left(dbname, i - 1)
                             Next
                         End If
                     Else
                         MsgBox "Windoc Database not specified. Invoices will not be scanned."
                         scan = False
                     End If
                 End If
                */
            }
        }

        private void GetInvoices()
        {
            InvoiceDistributions[] invDist = null;
            LstInvoices.ItemsSource = null;
            if (BatchInvoices)
            {
                _invoices = Invoices.loadForBatch(BatchNumber, invoiceTable);
                BatchControl batchCtrl = BatchControl.load(BatchNumber);
                this.Title = "Batch Invoices";
                if (batchCtrl != null)
                    this.Title += " - " + batchCtrl.BatchName;
            }
            else
                _invoices = Invoices.load(invoiceTable);
            _distributions = InvoiceDistributions.load(distributionTable);
            if (_invoices != null)
            {
                foreach (Invoices inv in _invoices)
                {
                    invDist = InvoiceDistributions.loadByInvoice(inv.InvoiceKey);
                    inv.Company = invDist[0].Company;
                    inv.DepartmentName = invDist[0].DepartmentName;
                    if (!Highlight)
                    {
                        inv.PayFlag = 0;
                    }
                    inv.RowForegroundBrush = RowForegroundGroundBrush;
                }  
            }
            LstInvoices.ItemsSource = _invoices;
        }
        private void FrmInvoiceList_OnClosing(object sender, CancelEventArgs e)
        {
            if (BatchInvoices)
            {
                       // redisplay batch window?
            }
            Main.SetRegistryEntry(GlobalShared.APPNAME + PROGRAM_REG_KEY + this.Name, PROGRAM_REG_SUBKEY, RowForegroundGroundBrush.ToString(), (int)MbcsCentral.Registry.DataTypes.String);
            InvoiceDistributions.TABLE = WinglGlobal.BASE_INVOICE_DISTRIBUTION_TABLE;
            Invoices.TABLE = WinglGlobal.BASE_INVOICE_TABLE;
            Main.saveForm(this);
            GC.Collect();
        }

        private void CmdEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (LstInvoices.SelectedIndex >= 0)
            {
                Invoices inv = (Invoices) LstInvoices.SelectedItem;
                frmInvoiceNew frm = new frmInvoiceNew(m_ThisApp) { InvoiceKey = inv.InvoiceKey, IsBatch = BatchInvoices, BatchNumber = BatchNumber};
                frm.ShowDialog();
                GetInvoices();
            }
        }

        private void CmdNew_OnClick(object sender, RoutedEventArgs e)
        {
            Invoices inv = (Invoices)LstInvoices.SelectedItem;
            frmInvoiceNew frm = new frmInvoiceNew(m_ThisApp) { InvoiceKey = -1 , IsBatch = BatchInvoices, BatchNumber = BatchNumber };
            while (frm != null )
            {
                frm.ShowDialog();
                frm = frm.frm;
            }
            GetInvoices();
        }

        private void CmdDelete_OnClick(object sender, RoutedEventArgs e)
        {
// TODO Finish coding Invoice deletion
            if (LstInvoices.SelectedIndex < 0)
            {
                System.Windows.MessageBox.Show("Please select at least one Invoice");
                return;
            }
            string msg = "Delete ";
            if (LstInvoices.SelectedItems.Count == 1)
                msg += "this Invoice?";
            else
                msg += "these " + LstInvoices.SelectedItems.Count.ToString() + " Invoices?";
            
            if (System.Windows.MessageBox.Show(msg, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Invoices inv = null;
                for (int i = 0; i < LstInvoices.SelectedItems.Count; i++)
                {
                    inv = (Invoices)LstInvoices.SelectedItems[i];
                    //GlobalShared.CN.BeginTransaction();
                    InvoiceHistory invHistory = InvoiceHistory.LoadByInvoice(inv.VendorNumber, inv.InvoiceNumber);
                    if (invHistory != null)
                        invHistory.delete(invHistory.Id.ToString());
                    inv.delete(inv.InvoiceKey.ToString());
                    Memos memo = Memos.load(inv.MemoId);
                    if (!BatchInvoices && memo != null)
                        memo.delete(memo.MemoId.ToString());
                    if (scan)
                    {
                        long docNumber = Main.GetDocumentId(inv.VendorNumber, inv.FullInvoiceNumber);
                        if (docNumber > 0)
                        {
                            if (WindocLocation == null || WindocLocation.Trim().Equals(""))
                            {
                                MessageBoxCustom.Show("Could not find Windoc Database <" + WindocLocation + ">. Invoices will not be scanned.");
                                scan = false;
                            }
                            else
                            {
                                if (!WindocLocation.EndsWith("\\", StringComparison.Ordinal))
                                    WindocLocation += "\\";
                                if (MessageBoxCustom.Show("Delete all scanned documents for invoice: #" + inv.InvoiceNumber.Trim() + " $" + inv.Amount.ToString() + " for " + inv.VendorName.Trim() + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocLocation.Trim() + "Windoc.mdb" + ";Mode=Share Deny None";
                                    OleDbConnection CN = new OleDbConnection(WindocConnectionString);
                                    WinDoc.Documents doc = WinDoc.Documents.load(docNumber, CN);
                                    if (doc != null) 
                                    WinglGlobal.deleteDocument(docNumber, WindocLocation.Trim() + "Documents\\", CN);
                                }
                            }
                        }
                    }
                }
                GetInvoices();
            }
        }

        private void CmdReprint_Click(object sender, RoutedEventArgs e)
        {
            if (LstInvoices.SelectedIndex < 0)
            {
                System.Windows.MessageBox.Show("Please select at least one Invoice");
                return;
            }
            string msg = "Reprint checks for ";
            if (LstInvoices.SelectedItems.Count == 1)
                msg += "this Invoice?";
            else
                msg += "these " + LstInvoices.SelectedItems.Count.ToString() + " Invoices?";

            if (System.Windows.MessageBox.Show(msg, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Invoices inv = null;
                for (int i = 0; i < LstInvoices.SelectedItems.Count; i++)
                {
                    inv = (Invoices)LstInvoices.SelectedItems[i];
                    inv.AmountPaid = 0;
                    if (!inv.ACH)
                    {
                        inv.CheckDate = GlobalShared.NULL_DATE;
                        inv.CheckNumber = 0;
                    }
                    inv.PayFlag = 1;
                    inv.save();
                }
                GetInvoices();
            }
        }

        private void CmdExit_OnClick(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            this.Close();
        }

        private void LstInvoices_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid) sender;
            if (dg.SelectedIndex >= 0)
            {
                CmdEdit_OnClick(sender, new RoutedEventArgs());
                e.Handled = true;
            }
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            InvoiceList rpt = new InvoiceList(m_ThisApp);
            rpt.process(_invoices);
        }


        private void cmdFirstOfMonthEdit_Click(object sender, RoutedEventArgs e)
        {
            FirstOfMonthEdit rpt = new FirstOfMonthEdit(m_ThisApp);
            rpt.process();
        }
        private void cmdPrintChecks_Click(object sender, RoutedEventArgs e)
        {
            string xsql = Invoices.SQL_LOAD_ALL + " AND [" + Invoices.SQL_COL_CHECK_NUMBER + "] > 0 ";
            xsql += " ORDER BY VendorName, " + WinGL.Department.SQL_COL_NAME + ", " + Invoices.SQL_COL_INVOICE_KEY;

            Invoices[] SummaryInvoices = Invoices.LoadQuery(xsql, true);
            InvoiceList rpt = new InvoiceList();
            rpt.process(SummaryInvoices, true);

        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetInvoices();
        }

        private void chkHighlight_Click(object sender, RoutedEventArgs e)
        {
            if (chkHighligt.IsChecked.Value)
                Highlight = true;
            else
                Highlight = false;
            GetInvoices();
        }

        private void clrPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            ColorPicker clr = sender as ColorPicker;
            System.Windows.Media.Color c = clrPicker.SelectedColor.Value;
            var converter = new System.Windows.Media.BrushConverter();
            var brush =(Brush)converter.ConvertFromString(c.ToString());
            chkHighligt.Background = brush;
            RowForegroundGroundBrush = brush;
            txtPrintedInvoices.Foreground = brush;
            chkHighligt.Background = brush;
            GetInvoices();
        }
    }
}
