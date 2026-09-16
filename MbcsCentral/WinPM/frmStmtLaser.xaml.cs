using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using MbcsCentral.WinGL;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmStmtLaser.xaml
    /// </summary>
    public partial class frmStmtLaser : Window
    {
        Control ctrl;
        private string m_ThisApp;
        string baseFontName = "Courier New";
        float baseFontSize = (float)8.0;

        const string MAIN_REGISTRY_KEY = "MbcsCentral\\Forms\\formStmtLaser\\";
        const string SUB_REGISTRY_KEY = "Form";
        const string SUB_REGISTRY_ADDRESS = "Address";

        const string PROGRAM_RESGISTRY_KEY = "\\StmtMessages\\";
        const string PRIOR_PAYMENTS_KEY = "PriorPayments";
        private bool printPriorPayments = false;

        const string BILLING_DESCRIPTIONS = "BillingDescriptions";

        const string NACHA_FILE_PATH = "\\NachaFilePath\\";
        const string NACHA_STMT_MSG = "\\NachaStmtMsg\\";

        public bool OneFlag { get; set; } = false;
        public bool PrintStmts { get; set; } = true;
        public bool BillingFile { get; set; } = false;
        public bool NachaFile { get; set; } = false;
        public bool EmailStmts { get; set; } = false;

        protected string NachaFilePath = "";

        protected NACHA_Layout[] NachaControl;
        protected int[] nachaAreas = { 0, 1, 2, 3, 4, 5 };
        static TextBox txtBldFrom = null;
        static TextBox txtBldTo = null;

        const string BILLING_FILE_PREFIX = "BillingFilePrefix";
        const string BILLING_FILE_TAG = "BillingFileTag";
        const string BILLING_FILE_LOCATION = "BillingFileLocation";
        string[] msgs = null;
        BillDescriptions[] billDescriptions = null;

        frmWait frmwait = new frmWait();

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmStmtLaser()
        {
            InitializeComponent();
        }
        public frmStmtLaser(string parm, string billfile = "")
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
            if (billfile.Trim().ToLower().Equals("b"))
            {
                BillingFile = true;
                PrintStmts = false;
            }
            if (billfile.Trim().ToLower().Equals("n"))
            {
                NachaFile = true;
                PrintStmts = false;
            }
        }

        private void formStmtLaser_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            //this.Width = colMain.Width.Value + colMiddle.Width.Value + 20;
            //Expander.Visibility = Visibility.Collapsed;
            lstTenants.DataContext = this;
            ctrl =  Control.load(GlobalShared.user.Account.clientId);
            mainGrid.DataContext = ctrl;
            txtStartBuilding.Focus();

            object obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_KEY,"1");
            if (obj != null && obj.ToString().Trim().Equals("2"))
                chkLockBox.IsChecked = true;
            else
                chkLockBox.IsChecked = false;

            obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_ADDRESS, "0");
            if (obj != null && obj.ToString().Trim().Equals("1"))
                chkAddressLine.IsChecked = true;
            else
                chkAddressLine.IsChecked = false;

            obj = Main.GetRegistryEntry(GlobalShared.APPNAME + PROGRAM_RESGISTRY_KEY, PRIOR_PAYMENTS_KEY, "0", true);
            if (obj != null && obj.ToString().Trim().Equals("1"))
                printPriorPayments = true;
            else
                printPriorPayments = false;

            obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_PREFIX);
            if (obj != null)
                txtBillFilePrefix.Text = obj.ToString();

            obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_TAG);
            if (obj != null)
                txtBillingFileTag.Text = obj.ToString();

            obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_LOCATION);
            if (obj != null)
                txtBillingingFileLocation.Text = obj.ToString();

            obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_DESCRIPTIONS);
            if (obj != null)
            {
                msgs = obj.ToString().Split(',');

            }

            Tenants[] tens = Tenants.loadForNames(false, false);
            if (tens != null && tens.Length > 0)
            {
                /*
                lstTenants.ItemsSource = null;
                lstTenants.ItemsSource = tens;
                */
                lstTenants.Items.Clear();
                foreach (Tenants ten in tens)
                    lstTenants.Items.Add(ten);
            }
            if (BillingFile)
            {
                this.Title = "Generate Billing File";
                chkLockBox.Visibility = Visibility.Hidden;
                chkAddressLine.Visibility = Visibility.Hidden;
                bdrBill1.Visibility = Visibility.Visible;
                bdrBill2.Visibility = Visibility.Visible;
                bdrBill3.Visibility = Visibility.Collapsed;
                bdrBill4.Visibility = Visibility.Collapsed;
                BillingFile = true;
                PrintStmts = false;
                cmdPrint.Visibility = Visibility.Hidden;
                Control ctrl = Control.load(GlobalShared.user.Client.clientId);
                if (ctrl != null)
                {
                    billDescriptions = new BillDescriptions[3];
                    billDescriptions[0] = new BillDescriptions() { Description = ctrl.StatementMessage1 };
                    billDescriptions[1] = new BillDescriptions() { Description = ctrl.StatementMessage2 };
                    billDescriptions[2] = new BillDescriptions() { Description = ctrl.StatementMessage3 };
                    if (msgs != null && msgs.Length > 0)
                    {
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            int n = -1;
                            int.TryParse(msgs[i], out n);
                            if (n > 0 && n <= billDescriptions.Length)
                                billDescriptions[n - 1].Use = true;
                        }
                    }
                    grdDescription.ItemsSource = billDescriptions;
                }
            }
            else if (NachaFile)
            {
                this.Title = "Generate ACH File";
                chkLockBox.Visibility = Visibility.Hidden;
                chkAddressLine.Visibility = Visibility.Hidden;
                bdrBill1.Visibility = Visibility.Collapsed;
                bdrBill2.Visibility = Visibility.Collapsed;
                bdrBill3.Visibility = Visibility.Visible;
                bdrBill4.Visibility = Visibility.Visible;
                NachaFile = true;
                PrintStmts = false;
                cmdPrint.Visibility = Visibility.Hidden;

                NachaControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
                if (NachaControl == null || NachaControl.Length == 0)
                {
                    MessageBoxCustom.Show("The NACHA File Layout does not exist.  Cannot continue.");
                    this.Close();
                }
                txtNachaFilePrefix.Text = (string)NachaControl[(int)NACHA_Layout.Control_Types.FilePrefix - 1].Contents;
                txtNachaFileLocation.Text = (string)NachaControl[(int)NACHA_Layout.Control_Types.FileDirectory - 1].Contents;
                txtStatementMessage.Text = (string)NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Contents;
                int position = (int)NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Length;
                lstMsgArea.ItemsSource = nachaAreas;
                int selectedValue = (position >= 0 && position <= 5 ? position : 0);
                
                var itm = lstMsgArea.ItemContainerGenerator.ContainerFromItem(selectedValue) as TreeViewItem;
                if (itm != null)
                    itm.IsSelected = true;
                
               //txtNachaFileTag.Text = (string)NachaControl[(int)NACHA_Layout.Control_Types.FileID].Contents;
            }
            else
            {
                bdrBill1.Visibility = Visibility.Collapsed;
                bdrBill2.Visibility = Visibility.Collapsed;
                bdrBill3.Visibility = Visibility.Collapsed;
                bdrBill4.Visibility = Visibility.Collapsed;
                Grid.SetRowSpan(Expander, 5);
                BillingFile = false;
                PrintStmts = true;
                cmdEmail.Visibility = Visibility.Visible;
            }

            // TODO:  retrieve form number from registry
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            var cursor = this.Cursor;
            this.Cursor = Cursors.Wait;
            /*
                       StmtLaser stmt = new StmtLaser();
                       stmt.StartingBuilding = txtStartBuilding.Text;
                       stmt.StartingTenant = txtStartTenant.Text;
                       stmt.EndingBuilding = txtEndBuilding.Text;
                       stmt.EndingTenant = txtEndTenant.Text;
                       stmt.ClosingDate = (DateTime)dtClosingDate.SelectedDate;
                       stmt.BillingDate = (DateTime)dtClosingDate.SelectedDate;
                        */
            ctrl.save();
            //stmt.Print();

            System.Printing.PrintQueue queue = null;
            if (PrintStmts)
            {
                frmPrintDialog pd = new frmPrintDialog(this.Name);
                //pd.DefaultPrinter = parms.DefualtPrinter;
                pd.Owner = this;
                pd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                pd.ShowDialog();
                queue = pd.Queue;
            }
            //object[] parms = {txtStartBuilding.Text, txtStartTenant.Text, 
            //                   txtEndBuilding.Text, txtEndTenant.Text, 
            //                   (DateTime)dtClosingDate.SelectedDate, (DateTime)dtClosingDate.SelectedDate};
            //StartReport.Start("MbcsCentral.WinPM.StmtLaser", parms);

            if (BillingFile)
            {
                if (txtBillFilePrefix.Text.Trim().Equals(""))
                {
                    MessageBoxCustom.Show("Billing File Prefix MUST be entered.");
                    this.Cursor = cursor;
                    return;
                }
                if (txtBillingingFileLocation.Text.Trim().Equals(""))
                {
                    MessageBoxCustom.Show("Billing File Location MUST be entered.");
                    this.Cursor = cursor;
                    return;
                }
                if (!FindBillingFiles())
                {
                    return;
                    this.Cursor = cursor;
                }
                }
                if (OneFlag && lstTenants.SelectedItems.Count == 0)
            {
                MessageBoxCustom.Show("INDIVIDUAL TENANTS chosen but no tenants have been selected.");
                this.Cursor = cursor;
                return;
            }

            Status.Text = " Please Wait...";
            ArrayList list = null;

            if (OneFlag)
            {
                list = new ArrayList();
                for (int i = 0; i < lstTenants.SelectedItems.Count; i++)
                {
                    list.Add(((Tenants)lstTenants.SelectedItems[i]).SequenceNumber);
                }
            }

            string BillFileDescription = "";
            if (billDescriptions != null && billDescriptions.Length > 0)
            {
                foreach (BillDescriptions desc in billDescriptions)
                {
                    if (desc.Use)
                        BillFileDescription += desc.Description + " ";
                }
                
            }

            if (!chkAddressLine.IsChecked.Value && !BillingFile && !NachaFile) // BillingFile added for Leemar
            {
                
                StmtLaser rpt = new StmtLaser(txtStartBuilding.Text, txtStartTenant.Text,
                                txtEndBuilding.Text, txtEndTenant.Text,
                                (DateTime)dtClosingDate.SelectedDate, (DateTime)dtClosingDate.SelectedDate,
                                chkLockBox.IsChecked.Value, queue) { OneByOne = list, ACHonly = chkACH.IsChecked.Value, EmailStmts = this.EmailStmts};
                rpt.PrintPriorPayments = printPriorPayments;
                rpt.Process();
                rpt = null;
                EmailStmts = false; // reset EmailStmts for next run
            }
            else
            {
                if (NachaFile)
                {
                    if (!NachaFilePath.Trim().Equals(""))
                        Main.SetRegistryEntry(MAIN_REGISTRY_KEY, NACHA_FILE_PATH, NachaFilePath, (int)Registry.DataTypes.String);

                    NachaControl[(int)NACHA_Layout.Control_Types.FileDirectory - 1].Contents = txtNachaFileLocation.Text;
                    NachaControl[(int)NACHA_Layout.Control_Types.FilePrefix - 1].Contents = txtNachaFilePrefix.Text;
                    NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Contents = txtStatementMessage.Text;
                    NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Length = (int)lstMsgArea.SelectedItem;
                    foreach (NACHA_Layout rec in NachaControl)
                        rec.save();
                }

                StmtLaser2 rpt2 = new StmtLaser2(txtStartBuilding.Text, txtStartTenant.Text,
                    txtEndBuilding.Text, txtEndTenant.Text,
                    (DateTime)dtClosingDate.SelectedDate, (DateTime)dtClosingDate.SelectedDate,
                    chkLockBox.IsChecked.Value, chkAddressLine.IsChecked.Value, queue) 
                            { OneByOne = list, ACHonly = chkACH.IsChecked.Value,
                              Nacha = this.NachaFile, NachaFileDir = txtNachaFileLocation.Text, NachaFilePrefix = txtNachaFilePrefix.Text, NachaFileOptional = txtNachaFileTag.Text,
                              BillPay = this.BillingFile, BillFileDir = txtBillingingFileLocation.Text,
                              BillFilePrefix = txtBillFilePrefix.Text, BillFileTag = txtBillingFileTag.Text,
                              PrintStmts = PrintStmts, BillPayDescription = BillFileDescription.Trim()};
                rpt2.PrintPriorPayments = printPriorPayments;
                rpt2.Process();
                NachaFilePath = "UserConfig\\Files\\" + txtNachaFilePrefix.Text + "_" + txtNachaFileTag.Text  + "_" + dtBillingDate.SelectedDate.Value.ToString("yyyyMMdd_") + rpt2.NachaFileTag + ".dat";
                if (NachaFile)
                {
                    ArrayList NachaProcessed = rpt2.NachaResults;
                    if (NachaProcessed != null && NachaProcessed.Count > 0)
                    {
                        TextWriter tw = new StreamWriter(NachaFilePath, false);
                        Tenants[] tenants = (Tenants[])NachaProcessed.ToArray(typeof(Tenants));
                        try
                        {
                            foreach (Tenants ten in tenants)
                                tw.WriteLine(ten.Serialize());
                        }
                        catch (Exception ex)
                        {
                            MessageBoxCustom.Show("Error writing NACHA results to disk: " + ex.Message);
                        }
                        finally
                        {
                            tw.Close();
                        }
                    }
                    this.Close();
                }
            }

            Status.Text = "";
            this.Cursor = cursor;
        }

        public static void PostNachaResults(string action)
        {
            string NachaFilePath = "";
            action = action.ToUpper();

            NACHA_Layout[] nachas = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
            if (nachas == null || nachas.Length == 0)
            {
                MessageBoxCustom.Show("Unable to retrieve ACH Control records.  Cannot continue.");
                return;
            }
            string NachaFilePrefix = (string)nachas[(int)NACHA_Layout.Control_Types.FilePrefix - 1].Contents;
            if (NachaFilePrefix == null || NachaFilePrefix.Trim().Equals(""))
            {
                MessageBoxCustom.Show("Unable to retrieve ACH File Prefix. Cannot continue.");
                return;
            }

            if (action == "P" || action == "N")
                NachaFilePath = "UserConfig\\Files\\";
            else
                NachaFilePath= (string)nachas[(int)NACHA_Layout.Control_Types.FileDirectory - 1].Contents;
            string[] files = Directory.GetFiles(NachaFilePath, NachaFilePrefix + "*");
            if (files == null || files.Length == 0)
            {
                MessageBoxCustom.Show("No ACH result files found.  There are no payments to post.");
                return;
            }
            Window frm = new Window();
            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frm.Title = "Choose ACH results to " + (action == "P" ? "POST" : "NOTIFY");
            frm.Height = 400;
            frm.Width = 500;
            Grid grd = new Grid();
            grd.RowDefinitions.Add(new RowDefinition());
            if (action == "P")
            {
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0) });

            }
            else if (action == "N")
            {
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                StackPanel pnlBuildings = new StackPanel() { Orientation = Orientation.Horizontal };
                TextBlock txt0 = new TextBlock() { Text = "Building From ", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10,0,0,0), FontSize = 14, FontWeight = FontWeights.Bold };
                pnlBuildings.Children.Add(txt0);
                txtBldFrom = new TextBox() { Width = 50, VerticalAlignment = VerticalAlignment.Center, Text = "000", FontSize = 14 };
                pnlBuildings.Children.Add(txtBldFrom);
                TextBlock txt1 = new TextBlock() { Text = " To ", VerticalAlignment = VerticalAlignment.Center, FontSize = 14, FontWeight = FontWeights.Bold };
                pnlBuildings.Children.Add(txt1);
                txtBldTo = new TextBox() { Width = 50, VerticalAlignment = VerticalAlignment.Center, Text = "999", FontSize = 14 };
                pnlBuildings.Children.Add(txtBldTo);
                Grid.SetRow(pnlBuildings, 1);
                grd.Children.Add(pnlBuildings);
            }
            grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

            ListBox lst = new ListBox();
            lst.Tag = action;
            List<string> ls = files.ToList();
            lst.ItemsSource = ls;

            frm.Tag = lst;

            StackPanel pnl = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0,0,0,5) };
            //if (action == "P")
            {
                Button cmdPost = new Button() { Content = (action == "P" ? "POST" : "NOTIFY"), Margin = new Thickness(10, 0, 0, 0) };
                cmdPost.Tag = frm;
                cmdPost.Click += NachaPost;
                pnl.Children.Add(cmdPost);
            }
            Button cmdDelete = new Button() { Content = "DELETE", Margin = new Thickness(10, 0, 0, 0) };
            cmdDelete.Click += NachaDelete;
            cmdDelete.Tag = frm;
            Button cmdExit = new Button() { Content = "EXIT", Margin = new Thickness(10, 0, 0, 0) };
            cmdExit.Click += NachaExit;
            cmdExit.Tag = frm;

            pnl.Children.Add(cmdDelete);
            pnl.Children.Add(cmdExit);

            Grid.SetRow(pnl, 2);
            grd.Children.Add(pnl);

            Border bdr = new Border();
            bdr.BorderBrush = Brushes.Blue;
            bdr.BorderThickness = new Thickness(3, 3, 3, 3);
            bdr.CornerRadius = new CornerRadius(2);
            bdr.Child = lst;

            Grid.SetRow(bdr, 0);
            grd.Children.Add(bdr);

            frm.Content = grd;
            
            frm.ShowDialog();
        }

        private static void NachaPost(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            Window frm = (Window)button.Tag;
            if (frm == null)
                return;
            ListBox lst = (ListBox)frm.Tag;
            if (lst == null)
                return;
            if (lst.SelectedItem == null)
            {
                MessageBoxCustom.Show("No ACH result file selected.");
                return;
            }
            string action = lst.Tag as string;
            if (MessageBoxCustom.Show((action == "P" ? "Post" : "Notify of") + " ACH Payments from: " + lst.SelectedItem + "?", (action == "P" ? "POST" : "NOTIFY OF") +  " ACH PAYMENTS", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Control ctrl = Control.load(GlobalShared.user.Account.clientId);
                string filepath = (string)lst.SelectedItem;
                //object obj = Main.GetRegistryEntry(MAIN_REGISTRY_KEY, NACHA_FILE_PATH, "");
                //if (obj == null || obj.ToString().Equals("") || !File.Exists(obj.ToString()))
                //{
                //    MessageBoxCustom.Show("NACHA results file not found.");
                //    return;
                //}
                //filepath = obj.ToString();
                TextReader tr = new StreamReader(filepath);
                ArrayList NachaProcessed = new ArrayList();
                string line = tr.ReadLine();
                while (line != null)
                {
                    NachaProcessed.Add(Tenants.DeSerialize(line));
                    line = tr.ReadLine();
                }
                tr.Close();
                Tenants[] tenants = (Tenants[])NachaProcessed.ToArray(typeof(Tenants));

                if (action == "P")
                {
                    ArrayList over = WinpmGlobal.ApplyRentPayments(tenants, true);
                }
                else
                {
                    foreach (Tenants ten in tenants)
                    {
                        if (!ten.Email1.Trim().Equals("") || !ten.Email2.Trim().Equals(""))
                        {
                            if (String.Compare(ten.BuildingNumber.Trim(), txtBldFrom.Text.Trim()) >= 0 && String.Compare(ten.BuildingNumber.Trim() ,txtBldTo.Text.Trim()) <= 0)
                            {
                                ten.CanSendEmail = true;
                                ten.SendEmail = true;
                                if (!ten.Email1.Trim().Equals(""))
                                {
                                    ten.UseEmail1 = true;
                                    ten.IsEmail1 = true;
                                }
                                else
                                    ten.UseEmail2 = true;
                                if (!ten.Email2.Trim().Equals(""))
                                {
                                    ten.IsEmail2 = true;
                                    ten.UseEmail2 = true;
                                }
                            }
                        }

                    }

                    frmTenantDocuments frmDocuments = new frmTenantDocuments() { AchNotices = true, PassedTenants = tenants };
                    frmDocuments.DoAch();
                    /*
                    frmNoticesEmail frmNotices = new frmNoticesEmail() { tenants = tenants, LateNotices = false };
                    frmNotices.ShowDialog();
                    */
                    return;
                }

                try
                {
                    File.Delete(filepath);
                }
                catch (PathTooLongException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "NACHA result file deletion: Path Too Long <" + filepath + ">", false);
                }
                catch (UnauthorizedAccessException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "NACHA result file deletion: User not authorized <" + filepath + ">", false);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "NACHA result file deletion: Other Error <" + filepath + ">\r\n   " + ex.Message, false);
                }
                Paytemp[] pmts = Paytemp.loadByUser();
                if (pmts != null && pmts.Length > 0)
                {
                    int drc;
                    frmPaymentRegister frmRegister = new frmPaymentRegister() { LockBox = LockboxHistory.FileType[(int)LockboxHistory.PaymentType.NACHA] };
                    frmRegister.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    frmRegister.ShowDialog();
                }
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    lst.Items.Remove(lst.SelectedItems[0]);
                }
                frm.InvalidateVisual();
            }
            
        }

        private static void NachaDelete(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;
            Window frm = (Window)button.Tag;
            if (frm == null)
                return;
            ListBox lst = (ListBox)frm.Tag;
            if (lst == null)
                return;

            if (lst.SelectedItem == null)
            {
                MessageBoxCustom.Show("No ACH result file selected for Deletion.");
                return;
            }
            string filepath = (string)lst.SelectedItem;
            List<string> files = (List<string>)lst.ItemsSource;
            int i = lst.SelectedIndex;
            if (MessageBoxCustom.Show("Delete ACH result file <" + filepath +">?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);

                    files.RemoveAt(i);
                    lst.ItemsSource = null;
                    lst.ItemsSource = files;
                }
                //frm.InvalidateVisual();
            }
        }

        private static void NachaExit(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            Window frm = (Window)button.Tag;
            if (frm != null)
                frm.Close();
        }
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtTenantSelect_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();            
        }

        private void txtTenantSelect_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
        }

        private void txtStartBuilding_TextChanged(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);

        }

        private void txtEndBuilding_TextChanged(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);

        }

        private void formStmtLaser_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);

            string form = (chkLockBox.IsChecked.Value ? "2" : "1");
            Main.SetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_KEY, form, (int)Registry.DataTypes.String);
            string address = (chkAddressLine.IsChecked.Value ? "1" : "0");
            if (BillingFile)
            {
                Main.SetRegistryEntry(MAIN_REGISTRY_KEY, SUB_REGISTRY_ADDRESS, address, (int)Registry.DataTypes.String);
                Main.SetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_PREFIX, txtBillFilePrefix.Text.Trim(), (int)Registry.DataTypes.String);
                Main.SetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_TAG, txtBillingFileTag.Text.Trim(), (int)Registry.DataTypes.String);
                Main.SetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_FILE_LOCATION, txtBillingingFileLocation.Text.Trim(), (int)Registry.DataTypes.String);
                string c = "";
                for (int i = 0; i < billDescriptions.Length; i++)
                {
                    if (billDescriptions[i].Use)
                    {
                        if (!c.Trim().Equals(""))
                            c += ",";
                        c += (i + 1).ToString();
                    }
                }
                Main.SetRegistryEntry(MAIN_REGISTRY_KEY, BILLING_DESCRIPTIONS, c, (int)Registry.DataTypes.String);
            }


        }

        private void cmdTenants_Click(object sender, RoutedEventArgs e)
        {
            if (!OneFlag)
            {
                //Expander.Visibility = System.Windows.Visibility.Visible;
                cmdTenants.Content = "<<INDIVIDUAL TENANTS";
                //this.Width = colMain.Width.Value + colMiddle.Width.Value + colTenant.Width.Value + 20;
                OneFlag = true;
                lstTenants.IsEnabled = true;
                lstTenants.Visibility = Visibility.Visible;
                lstTenants.Focus();
            }
            else
            {
                //Expander.Visibility = Visibility.Collapsed;
                cmdTenants.Content = "INDIVIDUAL TENANTS>>";
                //this.Width = colMain.Width.Value + colMiddle.Width.Value + 20;
                OneFlag = false;
                lstTenants.IsEnabled = false;
                lstTenants.Visibility = Visibility.Hidden;

            }
        }

        private void cmdBillingBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.SelectedPath = txtBillingingFileLocation.Text;
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            txtBillingingFileLocation.Text = dlg.SelectedPath;
            txtNachaFileLocation.Text = dlg.SelectedPath;
        }

        bool FindBillingFiles()
        {
            System.Collections.ObjectModel.ReadOnlyCollection<string> files = FileSystem.GetFiles(txtBillingingFileLocation.Text, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, txtBillFilePrefix.Text + "*");

            if (files != null && files.Count > 0)
            {
                int p1 = -1;
                int p2 = -1;
                string name = "";
                string date = String.Format("{0}{1}{2}", dtBillingDate.SelectedDate.Value.Year.ToString("0000"), dtBillingDate.SelectedDate.Value.Month.ToString("00"), dtBillingDate.SelectedDate.Value.Day.ToString("00"));
                List<string> dupes = new List<string>();
                string msg = "";
                if (files != null && files.Count > 0)
                {
                    foreach (string file in files)
                    {
                        p1 = file.IndexOf('_');
                        if (p1 >= 0)
                        {
                            name = file.Substring(p1 + 1);
                            p2 = name.IndexOf("_");
                            if (p2 >= 0)
                            {
                                name = name.Substring(0, p2);
                                if (name.Equals(date))
                                {
                                    dupes.Add(file);
                                    msg += file + "\r\n";
                                }
                            }
                        }
                    }
                }
                if (dupes.Count > 0)
                {
                    if (MessageBoxCustom.Show("Possible duplicate billing files (Yes to continue):\r\n\n" + msg,"",MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void cmdNachaManageFiles_Click(object sender, RoutedEventArgs e)
        {
            //PostNachaResults("M", txtNachaFileLocation.Text); // "M" = Manage ACH Files
            PostNachaResults("M");

        }

        private void cmdEmail_Click(object sender, RoutedEventArgs e)
        {
            // To do add the get list of tenant records: select all tenants without ACH but with e-mail addresses.
            EmailStmts = true;
            Tenants[] tens = Tenants.loadNonACH();
            frmTenantDocuments frmEmail = new frmTenantDocuments() { EmailStmts = true };
            frmEmail.EmailStmts = true;
            foreach (Tenants ten in tens)
            {
                if (!ten.Email1.Trim().Equals("") || !ten.Email2.Trim().Equals(""))
                {
                    ten.CanSendEmail = true;
                    ten.SendEmail = true;
                    if (!ten.Email1.Trim().Equals(""))
                    {
                        ten.UseEmail1 = true;
                        ten.IsEmail1 = true;
                    }
                    else
                        ten.UseEmail2 = true;
                    if (!ten.Email2.Trim().Equals(""))
                    {
                        ten.IsEmail2 = true;
                        ten.UseEmail2 = true;
                    }
                }
            }
            frmEmail.PassedTenants = tens;
            frmEmail.DoAch();
            this.Close();
            return;
        }
    }

    public class BillDescriptions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private string description = "";
        private bool use = false;

        public bool Use
        {
            get { return use; }
            set
            {
                OnPropertyChanged("Use");
                use = value;
            }
        }
        public string Description
        {
            get { return description; }
            set {
                OnPropertyChanged("Description");
                description = value; 
            }
        }
    }
}
