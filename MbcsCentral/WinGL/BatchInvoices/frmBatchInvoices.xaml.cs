using CSharp;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    public partial class frmBatchInvoices : Window
    {
        public bool invDateFlag { get; set; }
        public bool invDistFlag { get; set; }
        public bool invCheckFlag { get; set; }
        private bool usePrivate = false;

        BatchControl[] batches;
        bool scanBatch;
        string WindocConnectionString;
        static OleDbConnection CN = null;
        bool InvoiceFlag;
        bool BatchPaid;
        bool PrePaid;
        bool gotCheckNumber;
        int MemoId; 

        public frmBatchInvoices()
        {
            InitializeComponent();
        }


        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmBatchInvoices(string parm)
        {
            InitializeComponent();
            m_ThisApp = parm;
        }


        private void frmBatchInvoices_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            if (GlobalShared.user.hasAccessPermission(m_ThisApp + WinglGlobal.USE_PRIVATE))
            {
                usePrivate = true;
                lstBatches.Columns[5].Visibility = Visibility.Visible;
            }

            Main.getForm(this);
            batches = BatchControl.load(usePrivate);
            lstBatches.ItemsSource = batches;
            BatchInvoiceOptions opt = new BatchInvoiceOptions();
            BatchInvoiceOptions[] opts = BatchInvoiceOptions.Options();
            CollectionViewSource options = (System.Windows.Data.CollectionViewSource)this.FindResource("batchOptions");
            options.Source = opts;
        }

        private void frmBatchInvoices_Closed(object sender, EventArgs e)
        {
            cmdClose_Click(sender, new RoutedEventArgs());
            Main.saveForm(this);
            InvoiceDistributions.TABLE = WinglGlobal.BASE_INVOICE_DISTRIBUTION_TABLE;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            foreach (BatchControl batch in batches)
            {
                if (batch.Deleted)
                {

                }
                else if (batch.IsChanged)
                    batch.save();
            }
            this.Close();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            InvoicesBatch inv = new InvoicesBatch();
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            ComboBox c = (ComboBox)sender;
            if (n < 0 || c.SelectedItem == null)
                return;
            batches[n].InvoiceOption = (short)((BatchInvoiceOptions)c.SelectedItem).Index;

        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            frmInvoiceList frm;
            BatchControl batch;
            if (lstBatches.SelectedIndex >= 0)
            {
                batch = (BatchControl)lstBatches.SelectedItem;
                frm = new frmInvoiceList(m_ThisApp);
                frm.BatchInvoices = true;
                frm.BatchNumber = batch.Key;
                frm.Show();
                batch.Selected = false;
            }
            else
            {
               
                for(int i = 0; i < lstBatches.Items.Count; i++)
                {
                    batch = (BatchControl)lstBatches.Items[i];
                    if (batch.Selected)
                    {
                        frm = new frmInvoiceList(m_ThisApp);
                        frm.BatchInvoices = true;
                        frm.BatchNumber = batch.Key;
                        frm.ShowDialog();
                        batch.Selected = false;
                    }
                }
            }
            //lstBatches.ItemsSource = null;
            //batches = BatchControl.load();
            //lstBatches.ItemsSource = batches;
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            lstBatches.Visibility = Visibility.Collapsed;
            InputTextBox.Text = "";
            InputBox.Visibility = System.Windows.Visibility.Visible;
            InputBox.Focus();
            InputTextBox.Focus();
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstBatches.SelectedIndex >= 0)
            {
                if (MessageBoxCustom.Show("Remove this Batch?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    BatchControl batch = (BatchControl)lstBatches.SelectedItem;
                    batch.delete(batch.Key.ToString());
                    batches = BatchControl.load();
                    lstBatches.ItemsSource = null;
                    lstBatches.ItemsSource = batches;
                }
            }
        }

        private void cmdProcess_Click_1(object sender, RoutedEventArgs e)
        {
            int count = 0;
            bool found = false;
            BatchControl batch;
            string memo = txtMemo.Text;
            for (int r = 0; r <= lstBatches.Items.Count - 1; r++)
            {
                batch = (BatchControl)lstBatches.Items[r];
                if (batch.Selected)
                {
                    found = true;
                    count++; 
                }
            }

            if (!found)
            {
                MessageBoxCustom.Show("You must select at least one batch for processing.");
            }
            else
            {
                if (count > 1 && !txtMemo.Text.Trim().Equals(""))
                {
                    if (MessageBoxCustom.Show("Multiple batches have been selected.  Memo will be ignored. Continue processing?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                    memo = "";
                }
                if (MessageBoxCustom.Show("Process the selected batches?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                if (chkScan.IsChecked.Value)
                {
                    scanBatch = true;
                }
                else
                {
                    scanBatch = false;
                }
                for (int r = 0; r < lstBatches.Items.Count; r++)
                {
                    batch = (BatchControl)lstBatches.Items[r];
                    if (batch.Selected)
                    {
                        batch.Selected = false;
                        if (batch.LastDate >= DateTime.Now.AddDays(-7))
                        {
                            if (MessageBoxCustom.Show("This batch has been processed within the past 7 days. \r\nDo you still wish to process this batch?","", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            {
                                break;
                            }
                        }
                        ProcessInvoices(batch, memo);
                        batch.Selected = false;
                        //invDateFlag = false;
                    }

                }
                txtMemo.Text = "";
            }
            
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            BatchControl batch = new BatchControl() { BatchName = InputTextBox.Text, LastDate = GlobalShared.NULL_DATE, InvoiceOption = -1 };
            if (batch.save())
            {
                int key = batch.Key;
                lstBatches.ItemsSource = null;
                batches = BatchControl.load();
                lstBatches.ItemsSource = batches;
            }
            else
                MessageBoxCustom.Show("Unable to create new batch.");
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstBatches.Visibility = Visibility.Visible;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstBatches.Visibility = Visibility.Visible;
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

//******************************************************************************
		private void ProcessInvoices(BatchControl batch, string memo = "")
		{
            
			object bm = null;
			string x = "";
			int seq;
			int y;
			int d;
			int m;
			string xsql = "";
			string UserName = "";
            int InvoiceOption;
            string CurrentBatch;
            int BatchNumber;
			object rc = null;
            DateTime invdate;
            DateTime CheckDate =  GlobalShared.NULL_DATE;
            int CheckNumber = 0;
            string invnumber = "";
            string[,] dists = new string[6, 1];
            //******************************************************************************

            Invoices[] tblBatch = null;
			short i = 0;
			short cnt = 0;
            bool Scan = false;
            bool cancel = false;
            bool skip = false;
            string WinDocPath = "";
            InvoiceDistributions[] tblBatchDist = null;
			InvoiceDistributions tblDist = null;
            CheckNumbers ChkNumber = null;
			Vendors tblVend = null;
            string uniqueID = "";

            Control[] control = Control.load();
            if (control != null && control.Length > 0)
            {
                //Alerts = control[0].;
                if (scanBatch && control[0].Scan)
                    Scan = true;
                if (Scan)
                {
                    uniqueID = DateTime.Now.ToString("MMddyyyyhhmm");
                    WinDocPath = control[0].WindocLocation.Trim();
                    if (!WinDocPath.Trim().Equals(""))
                    {
                        WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WinDocPath + "\\Windoc.mdb" + ";Mode=Share Deny None";
                        CN = new OleDbConnection(WindocConnectionString);
                        try
                        {
                            CN.Open();

                        }
                        catch (OleDbException ex)
                        {
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error openning Windoc Database (" + WinDocPath + ") Invoices will not be scanned: " + ex.Message, System.Windows.Forms.MessageBoxButtons.OK);
                            Scan = false;
                        }
                        finally
                        {
                            try { CN.Close(); } catch { }
                        }
                        if (Scan)
                        {
                            WinglGlobal.uniqueID = Convert.ToString(DateTime.Now);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Windoc Database not specified. Invoices will not be scanned.");
                        Scan = false;
                    }
                }
			}
			WinglGlobal.InvoiceBatch = false;
			this.Cursor = System.Windows.Input.Cursors.Wait;
			CurrentBatch = batch.BatchName;
			InvoiceOption = batch.InvoiceOption;
			BatchNumber = batch.Key;
			GlobalShared.Log.Log((int)LogClass.logType.Info, "********************************************", false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "* User: " + UserName + ", Processing Batch: " + CurrentBatch + ", Invoice Option: " + InvoiceOption.ToString(), false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "********************************************", false);
            tblBatch = InvoicesBatch.loadForBatch(batch.Key, InvoicesBatch.TABLE);
			short c = 0;
            System.Data.DataRow dr;
            int Size = 32;
			bool pp = false;
            bool inactive = false; // if true, process inactive vendor
			if (tblBatch != null && tblBatch.Length > 0) {
				cnt = 0;
				Invoices tbl = new Invoices();
                dr = tbl.GetColumn("InvoiceNumber");
                if (dr != null)
                {
                    Size = (int)dr[2];
                }
                Invoices checkInvoice;
                foreach (Invoices invoice in tblBatch)
                {
                    inactive = false;
                    Vendors vendor = Vendors.load(invoice.VendorNumber);
                    if (vendor == null)
                    {
                        MessageBoxCustom.Show("Invoice '" + invoice.Description + ".\r\nVendor number " + invoice.VendorNumber.ToString() + " is no longer in the database.\r\nSkipping invoice.");
                        continue;
                    }
                    else if(vendor.Inactive)
                    {
                        MessageBoxResult result = MessageBoxCustom.Show("The vendor <" + vendor.VendorName + "> is marked INACTIVE.\r\n\r\n" +
                                                                        "Process anyway, reactivating vendor = YES\r\n\r\n" +
                                                                        "Process anyway, leaving vendor inactive = NO\r\n\r\n" +
                                                                        "Do not process this invoice = CANCEL", "", MessageBoxButton.YesNoCancel);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                vendor.Inactive = false;
                                vendor.save();
                                break;
                            case MessageBoxResult.No:
                                inactive = true;
                                break;
                            default:
                                continue;
                                break;
                        }
                    }

                    WinglGlobal.invpGlobal = default(WinglGlobal.PostInvoice_type);

                    tbl = new Invoices();
                    //invoice = tbl;
                    CopyObjects.CopyProperties( invoice, tbl);
                    tbl.SetTable( "gl_Invoices");
					if (chkInvoiceDate.IsChecked.Value) {
						tbl.Date = (DateTime)dtInvoices.SelectedDate;
					} else {
						tbl.Date = DateTime.Today;
					}
					if (chkPayDate.IsChecked.Value) 
                    {
						tbl.PayDueDate = (DateTime)dtPayDate.SelectedDate;
                        if (tbl.ACH)
                            tbl.CheckDate = tbl.PayDueDate;
					} else {
						tbl.PayDueDate = DateTime.Today;
					}
					invdate = tbl.Date;
					m = tbl.Date.Month;
					d = tbl.Date.Day;
					y = tbl.Date.Year;
					seq = 1;
					tbl.PayFlag = (short)(tbl.PayFlag == (int)Invoices.PayFlags.EFT ? (int)Invoices.PayFlags.EFT : 0);
					MemoLine(tbl);

					PrePaid = false;
					BatchPaid = false;
					if (tbl.CheckNumber > 0 || tbl.CheckDate > GlobalShared.NULL_DATE || tbl.EFT || tbl.ACH)
                    {
                        if (tbl.EFT || tbl.ACH)
                        {
                            CheckNumber = WinglGlobal.GetCheckNumber(tbl.Company, tbl.Department, tbl.ACH);
                            tbl.CheckNumber = CheckNumber;
                            if (tbl.ACH)
                            {
                                if (vendor.Corporation)
                                    tbl.ACH_TYPE = NACHA_Layout.Sec_Codes[(int)NACHA_Layout.Sec_Code_Types.CCD];
                                else
                                    tbl.ACH_TYPE = NACHA_Layout.Sec_Codes[(int)NACHA_Layout.Sec_Code_Types.PPD];

                            }
                            ChkNumber = null;
                        }
						else if (tbl.CheckNumber > 0)
                        {
							PrePaid = true;
                            CheckNumber = tbl.CheckNumber;

                            if (invCheckFlag)
                                CheckNumber++;
						}
                        else
                        {
							BatchPaid = true;
							gotCheckNumber = false;
						}
					}
                    bool processed = false;
                    frmInvoiceNumber frmInvNumber = null;
                    frmInvoiceNew InvoiceForm = new frmInvoiceNew(m_ThisApp);

                    while (!processed)
                    {
                        frmInvNumber = new frmInvoiceNumber();
                        frmInvNumber.Caller = this;
                        frmInvNumber.CheckNumber = tbl.CheckNumber;
                        if (chkPayDate.IsChecked.Value)
                            frmInvNumber.dtCheck.SelectedDate = dtPayDate.SelectedDate;
                        else
                            frmInvNumber.CheckDate = tbl.CheckDate;
                        switch (InvoiceOption)
                        {
                            case (int)BatchInvoiceOptions.InvoiceOptions.Date:
                                // Date + Auto Seq
                                tblVend = Vendors.load(tbl.VendorNumber);
                                if (tblVend != null)
                                {
                                    x = tblVend.VendorName;
                                    WinglGlobal.vName = tblVend.VendorName;
                                    tblVend = null;
                                }
                                else
                                {
                                    x = "* not found *";
                                }
                                if (PrePaid && !invCheckFlag)
                                {
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.PrePaid = true;
                                    frmInvNumber.lblDepartment.Visibility = Visibility.Collapsed;
                                    frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                    frmInvNumber.ShowDialog();
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    skip = frmInvNumber.Skip;
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    CheckDate = frmInvNumber.CheckDate;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = tbl.PayDueDate;
                                }
                                tbl.InvoiceNumber = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y * 10000 + m * 100 + d, "00000000") + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "");
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.DescAutoDate:
                                // Description + Date + Auto Seq
                                tblVend = Vendors.load(tbl.VendorNumber);
                                if (tblVend != null)
                                {
                                    x = tblVend.VendorName;
                                    WinglGlobal.vName = tblVend.VendorName;
                                    tblVend = null;
                                }
                                else
                                {
                                    x = "* not found *";
                                }
                                if (PrePaid && !invCheckFlag)
                                {
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.PrePaid = true;
                                    frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                    frmInvNumber.ShowDialog();
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    skip = frmInvNumber.Skip;
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    CheckDate = frmInvNumber.CheckDate;
                                    invnumber = frmInvNumber.txtManual.Text;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                tbl.InvoiceNumber = tbl.InvoiceNumber + frmInvNumber.txtManual.Text + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "");
                                tbl.Suffix = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y * 10000 + m * 100 + d, "00000000");
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.Desc:
                                // Description + Auto Seq
                                Vendors.load(tbl.VendorNumber);
                                if (tblVend != null)
                                {
                                    x = tblVend.VendorName;
                                    WinglGlobal.vName = tblVend.VendorName;
                                    tblVend = null;
                                }
                                else
                                {
                                    x = "* not found *";
                                }
                                if (PrePaid && !invCheckFlag)
                                {
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.PrePaid = true;
                                    frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                    frmInvNumber.ShowDialog();
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    skip = frmInvNumber.Skip;
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    CheckDate = frmInvNumber.CheckDate;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                tbl.InvoiceNumber = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y * 10000 + m * 100 + d, "00000000") + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "");
                                tbl.InvoiceNumber = tbl.InvoiceNumber.Left(11, 11) + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "");
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.Manual:
                                // Manual
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    frmInvNumber.PrePaid = PrePaid;
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                    frmInvNumber.lblManual.Text = "Manual Entry";
                                    frmInvNumber.ShowDialog();
                                    /*
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    */
                                    skip = frmInvNumber.Skip;
                                    invnumber = frmInvNumber.txtManual.Text;
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    CheckDate = frmInvNumber.CheckDate;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                tbl.InvoiceNumber = invnumber + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "").Left(Size);
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.ManDate:
                                // Manual + Date
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    if (!invDateFlag || (PrePaid && !invCheckFlag))
                                    {
                                        frmInvNumber.PrePaid = PrePaid;
                                        frmInvNumber.CheckNumber = CheckNumber;
                                        frmInvNumber.EntryType = InvoiceOption;
                                        frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                        frmInvNumber.dtInvoice.SelectedDate = invdate;
                                        frmInvNumber.Label1.Text = "Manual Entry";
                                        frmInvNumber.Label2.Text = "Date";
                                        frmInvNumber.ShowDialog();
                                        if (frmInvNumber.dtInvoice.SelectedDate == null || frmInvNumber.dtInvoice.SelectedDate <= GlobalShared.NULL_DATE)
                                        {
                                            frmInvNumber.Close();
                                            processed = true;
                                            cancel = true;
                                            continue;
                                        }
                                        skip = frmInvNumber.Skip;
                                        invnumber = frmInvNumber.txtManual.Text;
                                        invdate = (DateTime)frmInvNumber.dtInvoice.SelectedDate;
                                        CheckNumber = frmInvNumber.CheckNumber;
                                        CheckDate = frmInvNumber.CheckDate;
                                        frmInvNumber.Close();
                                    }
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                // MBCSCEN-49
                                //tbl.InvoiceNumber = invnumber.Trim() + invdate.ToString("MMddyy") + (seq > 1 ? "-" + seq.ToString("#####") : "");
                                tbl.InvoiceNumber = invnumber.Trim() + (seq > 1 ? "-" + seq.ToString("#####") : "");
                                tbl.Suffix = invdate.ToString("MMddyy");
                                tbl.Date = invdate;
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.DptDate:
                                // Department + Manual Date
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    if (!invDateFlag || (PrePaid && !invCheckFlag))
                                    {
                                        frmInvNumber.PrePaid = PrePaid;
                                        frmInvNumber.CheckNumber = CheckNumber;
                                        frmInvNumber.EntryType = InvoiceOption;
                                        frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                        frmInvNumber.lblDepartment.Text = tbl.Department.ToString();
                                        frmInvNumber.dtInvoice.SelectedDate = invdate;
                                        frmInvNumber.Label1.Text = "Department";
                                        frmInvNumber.Label2.Text = "Date";
                                        frmInvNumber.ShowDialog();
                                        if (frmInvNumber.dtInvoice.SelectedDate == null || (DateTime)frmInvNumber.dtInvoice.SelectedDate <= GlobalShared.NULL_DATE)
                                        {
                                            frmInvNumber.Close();
                                            processed = true;
                                            cancel = true;
                                            continue;
                                        }
                                        skip = frmInvNumber.Skip;
                                        invdate = (DateTime)frmInvNumber.dtInvoice.SelectedDate;
                                        CheckNumber = frmInvNumber.CheckNumber;
                                        CheckDate = frmInvNumber.CheckDate;
                                        frmInvNumber.Close();
                                    }
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                tbl.InvoiceNumber = tbl.Department.ToString("000") +  (seq > 1 ? "-" + seq.ToString("#####") : "");
                                tbl.Suffix = invdate.ToString("MMddyy");
                                tbl.Date = invdate;
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.DescDate:
                                // Description + Manual Date
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    if (!invDateFlag || (PrePaid && !invCheckFlag))
                                    {
                                        frmInvNumber.PrePaid = PrePaid;
                                        frmInvNumber.CheckNumber = CheckNumber;
                                        frmInvNumber.EntryType = InvoiceOption;
                                        frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                        frmInvNumber.lblDepartment.Text = tbl.InvoiceNumber;
                                        frmInvNumber.dtInvoice.SelectedDate = invdate;
                                        frmInvNumber.Label1.Text = "Description";
                                        frmInvNumber.Label2.Text = "Date";
                                        frmInvNumber.ShowDialog();
                                        if (frmInvNumber.dtInvoice.SelectedDate == null || frmInvNumber.dtInvoice.SelectedDate <= GlobalShared.NULL_DATE)
                                        {
                                            frmInvNumber.Close();
                                            processed = true;
                                            cancel = true;
                                            continue;
                                        }
                                        skip = frmInvNumber.Skip;
                                        invdate = (DateTime)frmInvNumber.dtInvoice.SelectedDate;
                                        CheckNumber = frmInvNumber.CheckNumber;
                                        CheckDate = frmInvNumber.CheckDate;
                                        frmInvNumber.Close();
                                    }
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                // MBCSCEN-49
                                //tbl.InvoiceNumber = tbl.InvoiceNumber.ToString() + invdate.ToString("MMddyy") + (seq > 1 ? "-" + seq.ToString("#####") : "").Left(Size);
                                tbl.InvoiceNumber = tbl.InvoiceNumber + (seq > 1 ? "-" + seq.ToString("#####") : "").Left(Size);
                                tbl.Suffix = invdate.ToString("MMddyy");
                                tbl.Date = invdate;
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.DescManual:
                                // Description + Manual Entry
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    frmInvNumber.PrePaid = PrePaid;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";

                                    frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                    frmInvNumber.lblDepartment.Text = tbl.InvoiceNumber;
                                    frmInvNumber.Label1.Text = "Description";
                                    frmInvNumber.Label2.Text = "Manual Entry";
                                    frmInvNumber.ShowDialog();
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    skip = frmInvNumber.Skip;
                                    invnumber = frmInvNumber.txtManual.Text;
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    CheckDate = frmInvNumber.CheckDate;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                tbl.InvoiceNumber = tbl.InvoiceNumber + invnumber + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "").Left(Size);
                                tbl.Date = invdate;
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.DescMonthYear:
                            case (int)BatchInvoiceOptions.InvoiceOptions.DescMonthDayYear:
                                // Description + Month/Year
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    if (!invDateFlag || (PrePaid && !invCheckFlag))
                                    {
                                        frmInvNumber.PrePaid = PrePaid;
                                        frmInvNumber.CheckNumber = CheckNumber;
                                        frmInvNumber.EntryType = InvoiceOption;
                                        frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                        frmInvNumber.lblDepartment.Text = tbl.InvoiceNumber;
                                        frmInvNumber.dtInvoice.SelectedDate = invdate;
                                        frmInvNumber.Label1.Text = "Description";
                                        frmInvNumber.Label2.Text = "Date";
                                        frmInvNumber.ShowDialog();
                                        if (frmInvNumber.dtInvoice.SelectedDate == null || frmInvNumber.dtInvoice.SelectedDate <= GlobalShared.NULL_DATE)
                                        {
                                            frmInvNumber.Close();
                                            processed = true;
                                            cancel = true;
                                            continue;
                                        }
                                        skip = frmInvNumber.Skip;
                                        invdate = (DateTime)frmInvNumber.dtInvoice.SelectedDate;
                                        CheckNumber = frmInvNumber.CheckNumber;
                                        CheckDate = frmInvNumber.CheckDate;
                                        frmInvNumber.Close();
                                    }
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                // MBCSCEN-49
                                //tbl.InvoiceNumber = tbl.InvoiceNumber + invdate.ToString("MMyy") + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "").Left(Size);
                                tbl.InvoiceNumber = tbl.InvoiceNumber + (seq > 1 ? "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####") : "").Left(Size);
                                switch (InvoiceOption)
                                {
                                    case (int)BatchInvoiceOptions.InvoiceOptions.DescMonthYear:
                                        tbl.Suffix = invdate.ToString("MMyy");
                                        break;
                                    case (int)BatchInvoiceOptions.InvoiceOptions.DescMonthDayYear:
                                        tbl.Suffix = invdate.ToString("MMddyy");
                                        break;
                                }
                                tbl.Date = invdate;
                                break;
                            case (int)BatchInvoiceOptions.InvoiceOptions.Desc2Year:
                                // Description + 2 digit year
                                if (seq <= 1 || (PrePaid && !invCheckFlag))
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    if (!invDateFlag || (PrePaid && !invCheckFlag))
                                    {
                                        frmInvNumber.PrePaid = PrePaid;
                                        frmInvNumber.CheckNumber = CheckNumber;
                                        frmInvNumber.EntryType = InvoiceOption;
                                        frmInvNumber.lblInvoice.Text = "Cmp/Dpt: " + tbl.Company + "/" + tbl.Department + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Vendor: " + x + "(" + tbl.VendorNumber + ")" + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Description: " + tbl.InvoiceNumber + "\r\n";
                                        frmInvNumber.lblInvoice.Text = frmInvNumber.lblInvoice.Text + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Amount, "###,###,###.00");
                                        frmInvNumber.lblDepartment.Text = tbl.InvoiceNumber;
                                        frmInvNumber.dtInvoice.SelectedDate = invdate;
                                        frmInvNumber.Label1.Text = "Description";
                                        frmInvNumber.Label2.Text = "Date";
                                        frmInvNumber.ShowDialog();
                                        if (frmInvNumber.dtInvoice.SelectedDate == null || frmInvNumber.dtInvoice.SelectedDate <= GlobalShared.NULL_DATE)
                                        {
                                            frmInvNumber.Close();
                                            processed = true;
                                            cancel = true;
                                            continue;
                                        }
                                        skip = frmInvNumber.Skip;
                                        invdate = (DateTime)frmInvNumber.dtInvoice.SelectedDate;
                                        CheckNumber = frmInvNumber.CheckNumber;
                                        CheckDate = frmInvNumber.CheckDate;
                                        frmInvNumber.Close();
                                    }
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                }
                                // MBCSCEN-49
                                //tbl.InvoiceNumber = tbl.InvoiceNumber + invdate.ToString("yy") + (seq > 1 ? "-" + seq.ToString("#####") : "").Left(Size);
                                tbl.InvoiceNumber = tbl.InvoiceNumber + (seq > 1 ? "-" + seq.ToString("#####") : "").Left(Size);
                                tbl.Suffix = invdate.ToString("yy");
                                tbl.Date = invdate;
                                break;
                            default:
                                if (PrePaid && !invCheckFlag)
                                {
                                    tblVend = Vendors.load(tbl.VendorNumber);
                                    if (tblVend != null)
                                    {
                                        x = tblVend.VendorName;
                                        WinglGlobal.vName = tblVend.VendorName;
                                        tblVend = null;
                                    }
                                    else
                                    {
                                        x = "* not found *";
                                    }
                                    frmInvNumber.EntryType = InvoiceOption;
                                    frmInvNumber.CheckNumber = CheckNumber;
                                    frmInvNumber.PrePaid = true;
                                    frmInvNumber.ShowDialog();
                                    if (string.IsNullOrEmpty(frmInvNumber.txtManual.Text))
                                    {
                                        frmInvNumber.Close();
                                        processed = true;
                                        cancel = true;
                                        continue;
                                    }
                                    CheckNumber = frmInvNumber.CheckNumber;
                                    skip = frmInvNumber.Skip;
                                    CheckDate = frmInvNumber.CheckDate;
                                    frmInvNumber.Close();
                                }
                                if (PrePaid)
                                {
                                    tbl.CheckNumber = CheckNumber;
                                    tbl.CheckDate = CheckDate;
                                    frmInvNumber.PrePaid = true;
                                }
                                break;
                        }


                        if (!cancel && !skip)
                        {
                            if (BatchPaid && !gotCheckNumber)
                            {
                                tbl.CheckNumber = WinglGlobal.GetCheckNumber(tbl.Company, tbl.Department);
                                tbl.CheckDate = tbl.PayDueDate;
                                tbl.Company = invoice.Company;
                                gotCheckNumber = true;
                                frmInvNumber.PrePaid = true;
                            }
                            if (tbl.EFT)
                                tbl.CheckDate = CheckDate;
                            string invoiceNumber = tbl.InvoiceNumber;
                            if (!memo.Trim().Equals(""))
                            {
                                Memos memoLine = new Memos() { CheckGroup = tbl.CheckGroup, VendorNumber = tbl.VendorNumber, CompanyNumber = tbl.Company, DepartmentNumber = tbl.Department, BatchNumber = tbl.Batch, Memo = memo };
                                if (memoLine.save())
                                    tbl.MemoId = memoLine.MemoId;
                            }
                            while (!tbl.save())
                            {
                                if (tbl.IsDupe)
                                {
                                    seq++;
                                    tbl.InvoiceNumber = invoiceNumber + "-" + seq.ToString();
                                }
                                else
                                {
                                    MessageBoxCustom.Show("Unable to save invoice.");
                                    return;
                                }
                            }
                            processed = true;

                            //
                            c = 0;
                            tblBatchDist = InvoiceDistributions.loadByInvoice(tbl.InvoiceKey);
                            if (tblBatchDist != null && tblBatchDist.Length > 0)
                                InvoiceDistributions.DeleteAll(tblBatchDist);
                            tblBatchDist = InvoiceDistributions.loadByInvoice(invoice.InvoiceKey, InvBatchDistributions.TABLE);
                            if (tblBatchDist != null && tblBatchDist.Length > 0)
                            {
                                dists = new string[6, 1];
                                foreach (InvoiceDistributions batchDist in tblBatchDist)
                                {
                                    tblDist = new InvoiceDistributions();
                                    CopyObjects.CopyProperties(batchDist, tblDist);
                                    tblDist.SetTable("gl_InvoiceDistributions");
                                    tblDist.InvoiceKey = tbl.InvoiceKey;
                                    c++;
                                    dists = ResizeArray<string>(dists, 6, c + 1);
                                    dists[1, c] = tblDist.Company.ToString();
                                    dists[2, c] = tblDist.Department.ToString();
                                    dists[3, c] = tblDist.Amount.ToString("###,###,##0.00");
                                    dists[4, c] = tblDist.Account.ToString();
                                    if (tblDist.Journal == null)
                                    {
                                        dists[5, c] = tblDist.Journal;
                                    }
                                    else
                                    {
                                        dists[5, c] = "";
                                    }
                                    tblDist.save();
                                }
                                tblDist = null;
                                //tblBatchDist.Close
                            }
                            tblBatchDist = null;
                            WinglGlobal.invpGlobal.amountpaid = tbl.Amount - tbl.Discount;
                            WinglGlobal.invpGlobal.amounttopay = tbl.AmountToPay;
                            WinglGlobal.invpGlobal.Company = tbl.Company;
                            WinglGlobal.invpGlobal.Department = tbl.Department;
                            WinglGlobal.invpGlobal.vendor = tbl.VendorNumber;
                            WinglGlobal.invpGlobal.vName = x;
                            WinglGlobal.invpGlobal.invnumber = tbl.FullInvoiceNumber;
                            if (tbl.CheckDate != null)
                            {
                                WinglGlobal.invpGlobal.CheckDate = tbl.CheckDate;
                            }
                            if (tbl.Date != null)
                            {
                                WinglGlobal.invpGlobal.date = tbl.Date;
                            }
                            if (tbl.PayDueDate != null)
                            {
                                WinglGlobal.invpGlobal.dueDate = tbl.PayDueDate;
                            }
                            if (invDistFlag)
                            {
                                // Invoices tblinvoices = new Invoices();
                                //CopyObjects.CopyProperties(tbl, tblinvoices);
                                InvoiceFlag = true;
                                InvoiceForm.BatchProcessing = true;
                                InvoiceForm.Tag = "Report";
                                InvoiceForm.InvoiceKey = tbl.InvoiceKey;
                                InvoiceForm.InactiveVendorsOnly = !inactive;
                                InvoiceForm.ShowAgain = false;
                                InvoiceForm.ScanChecked = chkScan.IsChecked.Value;
                                InvoiceForm.BringIntoView();
                                InvoiceForm.Topmost = true;
                                InvoiceForm.ShowDialog();
                                invDistFlag = false;
                                InvoiceFlag = false;
                            }

                            /*
                            // Check to verify that invoice has a non-zero amount
                            checkInvoice = Invoices.load(tbl.InvoiceKey);
                            if (checkInvoice != null)
                            {
                                if (checkInvoice.Net == 0)
                                {
                                    if (MessageBoxCustom.Show("This invoice has a ZERO amount and will not be processed. Continue with processing?\r\n\r\nBatch: " + batch.BatchName +
                                                            "\r\nVendor: " + tbl.VendorName +
                                                            "\r\nInvoice: " + tbl.InvoiceNumber, "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                    {
                                        cancel = true;
                                    }
                                    checkInvoice.delete(checkInvoice.InvoiceKey.ToString());
                                    InvoiceDistributions[] checkDists = InvoiceDistributions.loadByInvoice(checkInvoice.InvoiceKey);
                                    if (checkDists != null && checkDists.Length > 0)
                                    {
                                        foreach (InvoiceDistributions dist in checkDists)
                                            dist.delete(dist.Id.ToString());
                                    }
                                    checkInvoice = null; checkDists = null;
                                    continue;
                                }
                            }
                            */

                            GlobalShared.Log.Log((int)LogClass.logType.Info, "Vendor: " + WinglGlobal.vName.Trim() + ", Invoice #: " + WinglGlobal.invpGlobal.invnumber + ", Amount: " + WinglGlobal.invpGlobal.amountpaid.ToString("##,###,##0.00"), false);

                            for (i = 1; i < dists.GetLength(1); i++)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.Info, "   Comp: " + dists[1, i] + ", Dept: " + dists[2, i] + ", Amt: " + dists[3, i] + ", Acct: " + dists[4, i] + ", Jrl: " + dists[5, i], false);
                            }

                            if (scanBatch)
                            {
                                WinglGlobal.scanInvoice(uniqueID, WinglGlobal.invpGlobal, true, PrePaid || BatchPaid, true, WinDocPath.Trim() + "\\windoc.mdb");
                            }
                            cnt++;
                        }
                        else
                            processed = true;
                    }
                    if (frmInvNumber.CancelRun)
                        break;
                    skip = false;
                    cancel = false;
				}
                tblBatch = null;
                if (cnt > 0)
                {
                    batch.LastDate = DateTime.Now;
                    batch.save();
                }
				MessageBoxCustom.Show(cnt + " Invoices processed in batch: " + CurrentBatch);
                if (cnt > 0 && scanBatch)
                {
                    bool r;
                    int p = GlobalShared.FindRunningProcess("windoc");
                    if (p > 0)
                    {
                        if (MessageBoxCustom.Show("Windoc is already running. Close it now?.", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (!GlobalShared.CloseRunningProcess(p))
                            {
                                MessageBoxCustom.Show("Unable to close Windoc.");
                                return;
                            }
                        }
                    }
                    System.Diagnostics.Process exProc = new System.Diagnostics.Process();
                    exProc.StartInfo.FileName = WinDocPath.Trim() + "\\windoc.exe";
                    exProc.StartInfo.Arguments = String.Format("{0} \"{1}\"", "wingl-batch ", uniqueID );
                    try
                    {
                        r = exProc.Start();
                    }
                    catch (System.Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error starting WinDOC: " + ex.Message, false);
                    }
                }
            } else {
				MessageBoxCustom.Show("No invoices found in batch: " + CurrentBatch);
                this.Cursor = System.Windows.Input.Cursors.Arrow;
                return;
            }
            this.Cursor = System.Windows.Input.Cursors.Arrow;

        }

        protected T[,] ResizeArray<T>(T[,] original, int x, int y)
        {
            T[,] newArray = new T[x, y];
            int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (int i = 0; i < minY; ++i)
                Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);

            return newArray;
        }
        private void MemoLine(Invoices tbl)
        {
            object i = null;

            MemoId = 0;
            Memos[] tblMemo = Memos.loadByVendorCompDept((long)-tbl.VendorNumber, tbl.Company, tbl.Department, (int)tbl.CheckGroup, tbl.Batch);
            if (tblMemo != null && tblMemo.Length > 0)
            {
                Memos tblMemoNew = new Memos();
                CSharp.CopyObjects.CopyProperties(tblMemo[0], tblMemoNew);
                tblMemoNew.VendorNumber = -tblMemo[0].VendorNumber;
                tblMemoNew.MemoId = 0;

                if (!tblMemoNew.save())
                // Is this even necessary?????
                {
                    if (tblMemoNew != null)
                    {
                        MemoId = tblMemo[0].MemoId;
                    }
                }
                else
                    MemoId = tblMemoNew.MemoId;

                tblMemoNew = null;
                tbl.MemoId = MemoId;
            }
            tblMemo = null;

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            BatchInvoiceReport rpt = new BatchInvoiceReport() { Batches = batches };
            rpt.process();
        }
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                            cell.IsSelected = true;
                    }
                    else
                    {
                        DataGridRow row = FindVisualParent<DataGridRow>(cell);
                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }
        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }


    }
}
