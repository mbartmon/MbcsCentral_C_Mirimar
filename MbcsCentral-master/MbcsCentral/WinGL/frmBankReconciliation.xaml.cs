///-------------------------------------------------------------------------------------------------
// file:	WinGL\frmBankReconciliation.xaml.cs
//
// summary:	Implements the form bank reconciliation.xaml class
///-------------------------------------------------------------------------------------------------
using CSharp;
using ExtensionMethods;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml.Serialization;

using Excel = Microsoft.Office.Interop.Excel;

namespace MbcsCentral.WinGL
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interaction logic for frmBankReconciliation.xaml. </summary>
    ///
    /// <remarks>   Mike, 2017-03-07. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public partial class frmBankReconciliation : System.Windows.Window
    {
        private const string Statement = "Statement";
        private const string BankErrorFor = "BnkErr+";
        private const string BankErrorAgainst = "BnkErr-";

        private string ShortName = "BnkRec";
        public string chkJournal { get; set; }

        public bool JournalSet { get; set; }
        private Company[] companies;
        private Accounts[] accounts;
        private JournalNames[] journals;

        private Company company = null;
        private Department department = null;
        private int GlCashAccount;

        private List<BankReconciliation> checks;
        private long CurrentReconClassID = 0;

        private Totals totals;

        private BankReconciliation parms = null;

        private string className = "";
        private Classes cls;

        private bool wasSaved = false;

        private BankReconCompaniesList bankCompanies;
        private List<BankReconCompanies> savedRecons;

        private bool completed = false;

        private string thisApp;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public frmBankReconciliation()
        {
            InitializeComponent();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="app">  The application. </param>
        ///-------------------------------------------------------------------------------------------------

        public frmBankReconciliation(string app)
        {
            InitializeComponent();
            thisApp = app;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by frmBankReconciliation for loaded events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void frmBankReconciliation_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            GlobalShared.Log.Log((int)LogClass.logType.Debug, "frmBankReconciliation_Loaded", false);

            dtDate.SelectedDate = DateTime.Now;
            dtClear.SelectedDate = DateTime.Now.AddDays(-30);
            Control control = null;
            try
            {
                control = Control.loadOne();
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "frmBankReconciliation_Loaded - Control Table: " + ex.Message, false);

            }
            dtDate.SelectedDate = control.StmtDate;
            dtClear.SelectedDate = control.ClearDate;
            GlCashAccount = control.GLCashAccount;
            companies = Company.load(false,false);
            accounts = Accounts.load(true);
            journals = JournalNames.load();
            try
            {
                className = this.GetType().Name;
            } catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "frmBankReconciliation_Loaded - className: " + ex.Message, false);
            }
            if (className == null || className.Trim().Equals(""))
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "frmBankReconciliation_Loaded - className is NULL or EMPTY ", false);


            lstCompanies.ItemsSource = companies;
            CollectionViewSource accts = ((CollectionViewSource)(this.FindResource("accounts")));
            accts.Source = accounts;
            CollectionViewSource journs = ((CollectionViewSource)(this.FindResource("journals")));
            journs.Source = journals;

            // Find saved, unfinished reconciliations

            savedRecons = GetSavedRecons();
            List<BankReconCompanies> openRecons = null;
            if (savedRecons != null && savedRecons.Count > 0)
            {
                foreach (BankReconCompanies cmp in savedRecons)
                {
                    if (!cmp.Completed)
                    {
                        if (openRecons == null)
                            openRecons = new List<BankReconCompanies>();
                        openRecons.Add(cmp);
                    }
                }
            }
            if (openRecons != null)
            {
                if (MessageBoxCustom.Show("Unfinished Bank Reconciliations exist.  Do you want to view them?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    lstRecons.ItemsSource = openRecons;
                    popReload.IsOpen = true;
                }

            }
        /*
            Classes[] classes = Classes.loadMultiple(ShortName);
            if (classes != null && classes.Length > 0)
            {
                if (MessageBoxCustom.Show("Unfinished Bank Reconciliations exist.  Do you want to view them?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ArrayList classNames = new ArrayList();
                    foreach (Classes cls in classes)
                    {
                        classNames.Add(new ListItem(cls.name.Replace(ShortName + ":", ""), cls.ID, cls.value));
                    }

                    //lstRecons.ItemsSource = savedRecons;
                    popReload.IsOpen = true;
                }
            }
            */
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by frmBankReconciliation for closed events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void frmBankReconciliation_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            Control control = null;
            try
            {
                control = Control.loadOne();
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "frmBankReconciliation_Loaded - Control Table: " + ex.Message, false);

            }
            control.StmtDate = dtDate.SelectedDate.Value;
            control.ClearDate = dtClear.SelectedDate.Value;
            control.save();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdSave for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(thisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }
            
            if (company == null)
            {
                return;
            }
            decimal statementAmount = 0;
            decimal.TryParse(txtStatement.Text, out statementAmount);
            decimal bookAmount = 0;
            decimal.TryParse(txtBookBal.Text, out bookAmount);
            checks.Insert(0, new BankReconciliation() { Type = Statement, Date = (DateTime)dtDate.SelectedDate, StatementAmount = statementAmount, BookAmount = bookAmount, Completed = completed });
            completed = false;

            foreach (BankReconciliation recon in checks)
            {
                recon.CompanyNumber = company.CompanyNumber;
                recon.DepartmentNumber = department.DepartmentNumber;
                recon.StatementDate = (DateTime)dtDate.SelectedDate;
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BankReconciliation>), new Type[] { typeof(MbcsCentral.WinGL.BankReconciliation) });
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, checks);

            string ClassName = (ShortName + ":" + company.Name).Left(38) + ": " + ((DateTime)dtDate.SelectedDate).ToShortDateString();
            cls = Classes.load(0, ClassName);
            if (cls == null)
            {
                cls = new Classes();
                cls.name = (ShortName + ":" + company.Name).Left(38) + ": " + ((DateTime)dtDate.SelectedDate).ToShortDateString();
            }
            cls.value = textWriter.ToString();
            cls.clientId = 1;
            cls.acctId = 0;
            cls.save();

            return;
            grdChecks.ItemsSource = null;
            this.DataContext = null;
            totals.TotalBookCleared = 0; totals.TotalStatementCleared = 0;
            //CalculateTotals();
            this.DataContext = null;
            CurrentReconClassID = 0;
            lstCompanies.SelectedIndex = -1;
            company = null;
            department = null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdClose for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdDelete for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            if (n < 0)
                return;
            grdChecks.ItemsSource = null;
            checks.RemoveAt(n);
            this.DataContext = null;
            totals.TotalBookCleared = 0; totals.TotalStatementCleared = 0;
            CalculateTotals();
            this.DataContext = totals;
            grdChecks.ItemsSource = checks;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdRetrieve for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdRetrieve_Click(object sender, RoutedEventArgs e)
        {
            if (company != null)
            {
                if (MessageBoxCustom.Show("There is an unposted Reconciliation.  Do you wish to abandon it?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (lstCompanies.SelectedItem == null || lstDepartments.SelectedItem == null)
            {
                MessageBoxCustom.Show("You must first select both a Company and a Department.");
                return;
            }

            company = (Company)lstCompanies.SelectedItem;
            department = (Department)lstDepartments.SelectedItem;

            totals = null;
            totals = new Totals();
            get_CheckData();
            get_BookBalance();
            CalculateTotals();
            this.DataContext = totals;
            long CurrentReconClassID = 0;
            wasSaved = false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets book balance. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void get_BookBalance()
        {
            //******************************************************************************

            Company tblComp = null;
            int m = 0;
            int i = 0;
            int y = 0;

            if (company == null)
                return;
            tblComp = Company.loadByCompany(company.CompanyNumber);
            m = 12;
            if (tblComp != null)
            {
                if (tblComp.FiscalYearEnd >= 1 && tblComp.FiscalYearEnd <= 12)
                {
                    m = tblComp.FiscalYearEnd;
                }
                tblComp = null;
            }
            m = m + 1;
            if (m > 12)
                m = 1;
            y = ((DateTime)dtDate.SelectedDate).Year;
            if (m > ((DateTime)dtDate.SelectedDate).Month)
                y = y - 1;
            if (totals == null)
                totals = new Totals();

            totals.BookBalance = Transactions.AccountBalance(company.CompanyNumber, GlCashAccount, new DateTime(y, m, 1), (DateTime)dtDate.SelectedDate, department.DepartmentNumber);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets check data. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void get_CheckData()
        {
            Transactions[] Trans = null;
            ApChecks[] apChecks = null;
            object xsql = null;
            object i = null;
            //******************************************************************************

            short r = 0;
            decimal c = default(decimal);

            System.Windows.Input.Cursor cursor = this.Cursor;
            this.Cursor = System.Windows.Input.Cursors.Wait;

            checks = null;
            checks = new List<BankReconciliation>();

            decimal badj = 0;

            string[] opts = { company.CompanyNumber.ToString(), department.DepartmentNumber.ToString(), GlCashAccount.ToString(), dtDate.SelectedDate.ToString() };
            Trans = Transactions.load(true, opts);
            if (Trans != null && Trans.Length > 0)
            {
                foreach (Transactions tblTrans in Trans)
                {
                    BankReconciliation recon = new BankReconciliation();
                    recon.Type = (tblTrans.Amount < 0 ? "DEBIT" : "DEPOSIT");
                    recon.IndexNumber = tblTrans.IndexNumber;
                    recon.Date = tblTrans.Date;
                    recon.BookAmount = tblTrans.Amount;
                    recon.StatementAmount = tblTrans.Amount;
                    recon.Manual = false;
                    recon.Payee = tblTrans.Description;
                    recon.IndexNumber = tblTrans.IndexNumber;
                    recon.ReconFlag = tblTrans.RecFlag;

                    checks.Add(recon);
                }
                Trans = null;
            }
            apChecks = ApChecks.loadForReconciliation(company.CompanyNumber, department.DepartmentNumber, (DateTime)dtDate.SelectedDate);
            if (apChecks != null && apChecks.Length > 0)
            {
                foreach (ApChecks check in apChecks)
                {
                    c = WinglGlobal.CheckTotal(check.Company, check.Department, check.CheckNumber);
                    BankReconciliation recon = new BankReconciliation();
                    recon.Type = check.CheckNumber.ToString();
                    recon.CheckNumber = check.CheckNumber.ToString();
                    recon.IndexNumber = check.ID;
                    recon.Payee = check.VendorName;
                    recon.Date = check.Date;
                    recon.BookAmount = c;
                    recon.StatementAmount = c;
                    recon.ReconFlag = check.ReconFlag;
                    recon.Manual = false;

                    checks.Add(recon);
                }
                apChecks = null;
            }
            grdChecks.ItemsSource = null;
            grdChecks.ItemsSource = checks;
            CalculateTotals();
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the totals. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void CalculateTotals()
        {
            decimal ot = 0; decimal ct = 0; decimal dt = 0; decimal badj = 0;
            decimal amt; decimal bamt;
            bool AdjFlag; bool ClrFlag;
            string type = "";

            foreach (BankReconciliation bal in grdChecks.Items)
            {
                type = bal.Type;
                ClrFlag = bal.ReconFlag;
                bamt = bal.BookAmount;
                amt = bal.StatementAmount;
                AdjFlag = bal.Manual;
                if (ClrFlag)
                {
                    totals.TotalBookCleared += bal.BookAmount;
                    totals.TotalStatementCleared += bal.StatementAmount;
                }
                else
                {
                    totals.UnclearedChecks += bal.BookAmount;
                }
                switch (type)
                {
                    case "DEPOSIT":
                        if (!ClrFlag)
                            ct += amt;
                        else
                            if (amt != bamt)
                            badj += (amt - bamt);
                        if (AdjFlag)
                            badj += badj + amt;
                        break;

                    case "DEBIT":
                        if (!ClrFlag)
                        {
                            dt = dt + amt;
                        }
                        else
                        {
                            if (amt != bamt)
                            {
                                badj = badj + (amt - bamt);
                            }
                        }
                        if (AdjFlag)
                        {
                            badj = badj - amt;
                        }
                        break;

                    case BankErrorAgainst:
                        badj = badj - amt;
                        break;

                    case BankErrorFor:
                        badj = badj + amt;
                        break;

                    default:
                        if (!ClrFlag)
                        {
                            ot = ot + amt;
                        }
                        else
                        {
                            badj = badj + (bamt - amt);
                        }
                        break;
                }
            }
            totals.Adjustments = badj; // WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(badj, "##,###,##0.00"), ref 13);
            totals.AdjBookBalance = totals.BookBalance + totals.Adjustments;
            totals.UnclearedDeposits = ct;
            totals.UnclearedDebits = dt;
            totals.UnclearedChecks = ot;
            totals.AdjStatementBalance = decimal.Parse(txtStatement.Text) + ct - dt - ot;
            totals.TotalDifference = totals.TotalStatementCleared - totals.TotalBookCleared;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by lstCompanies for selection changed events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Selection changed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void lstCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company cmp = (Company)lstCompanies.SelectedItem;
            if (cmp != null)
            {
                lstDepartments.ItemsSource = null;
                Department[] depts = Department.loadByCompany(cmp.CompanyNumber);
                if (depts != null && depts.Length > 0)
                {
                    lstDepartments.ItemsSource = depts;
                    lstDepartments.SelectedIndex = 0;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdAddDeposit for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdAddDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (checks == null)
                return;
            BankReconciliation recon = new BankReconciliation();
            recon.Date = ((DateTime)dtDate.SelectedDate).Date;
            recon.Manual = true;
            recon.Payee = "Deposit";
            recon.Type = "DEPOSIT";
            recon.ReconFlag = true;
            checks.Insert(0, recon);
            grdChecks.ItemsSource = null;
            grdChecks.ItemsSource = checks;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdBankErrorAgainst for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdBankErrorAgainst_Click(object sender, RoutedEventArgs e)
        {
            if (checks == null)
                return;
            BankReconciliation recon = new BankReconciliation();
            recon.Date = ((DateTime)dtDate.SelectedDate).Date;
            recon.Manual = true;
            recon.Payee = "Bank Error Against";
            recon.Type = BankErrorAgainst;
            recon.ReconFlag = true;
            checks.Insert(0, recon);
            grdChecks.ItemsSource = null;
            grdChecks.ItemsSource = checks;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdAddDebit for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdAddDebit_Click(object sender, RoutedEventArgs e)
        {
            if (checks == null)
                return;
            BankReconciliation recon = new BankReconciliation();
            recon.Date = ((DateTime)dtDate.SelectedDate).Date;
            recon.Manual = true;
            recon.Payee = "Debit";
            recon.Type = "DEBIT";
            recon.ReconFlag = true;
            checks.Insert(0, recon);
            grdChecks.ItemsSource = null;
            grdChecks.ItemsSource = checks;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by cmdBankErrorFor for click events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void cmdBankErrorFor_Click(object sender, RoutedEventArgs e)
        {
            if (checks == null)
                return;
            BankReconciliation recon = new BankReconciliation();
            recon.Date = ((DateTime)dtDate.SelectedDate).Date;
            recon.Manual = true;
            recon.Payee = "Bank Error For";
            recon.Type = BankErrorFor;
            recon.ReconFlag = true;
            checks.Insert(0, recon);
            grdChecks.ItemsSource = null;
            grdChecks.ItemsSource = checks;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the routed event. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ///-------------------------------------------------------------------------------------------------

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            //DataGrid grid = (DataGrid)sender;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by NumberEntry for text changed events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Text changed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckBox_Checked(sender, new RoutedEventArgs());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by NumberEntry for lost focus events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void NumberEntry_LostFocus(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            if (n < 0)
                return;
            VIBlend.WPF.Controls.NumberEditor item = (VIBlend.WPF.Controls.NumberEditor)sender;
            if (((BankReconciliation)checks[n]).Manual)
            {
                ((BankReconciliation)checks[n]).StatementAmount = ((BankReconciliation)checks[n]).BookAmount;
                grdChecks.ItemsSource = null;
                grdChecks.ItemsSource = checks;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by CheckBox for checked events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            BankReconciliation recon = (BankReconciliation)grdChecks.SelectedItem;
            //recon.ReconFlag = !recon.ReconFlag;
            if (totals == null)
                totals = new Totals();
            totals.TotalBookCleared = 0;
            totals.TotalStatementCleared = 0;

            this.DataContext = null;
            CalculateTotals();
            this.DataContext = totals;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Event handler. Called by DataGridCell for preview mouse left button down events.
        /// </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Mouse button event information. </param>
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Finds the parent of this item. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="element">  The element. </param>
        ///
        /// <returns>   The found visual parent. </returns>
        ///-------------------------------------------------------------------------------------------------

        private static T FindVisualParent<T>(UIElement element) where T : UIElement
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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ComboBox for selection changed events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Selection changed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            ComboBox c = (ComboBox)sender;
            if (n < 0 || c.SelectedItem == null)
                return;

            switch (c.Name)
            {
                case "lstAccounts":
                    ((BankReconciliation)checks[n]).AccountDescription = ((Accounts)c.SelectedItem).Description;
                    ((BankReconciliation)checks[n]).AccountNumber = -((Accounts)c.SelectedItem).Key;
                    break;

                case "lstJournals":
                    ((BankReconciliation)checks[n]).Journal = ((JournalNames)c.SelectedItem).JournalName;
                    break;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by dtDate for selected date changed events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Selection changed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void dtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            get_BookBalance();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by lstAccounts for lost focus events. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void lstAccounts_LostFocus(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            int n = CSharp.DataGridRowUtils.DataGridRowIndex(dep);
            if (n < 0)
                return;
            ComboBox c = (ComboBox)sender;
            BankReconciliation rec = (BankReconciliation)checks[n];
            if (rec != null & !rec.Type.Equals(BankErrorAgainst) && !rec.Type.Equals(BankErrorFor) && rec.Manual && c.SelectedIndex < 0)
            {
                MessageBoxCustom.Show("A NON-CASH G/L account is required for this entry.");
                c.Focus();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Posts the recon. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool Post_Recon()
        {
            if (company == null)
            {
                MessageBoxCustom.Show("There is no Reconciliation to post.");
                return false;
            }

            decimal tblBudget = 0;
            decimal tblBalance = 0;
            int i;
            string xsql;
            string s;
            string CrLf = "\r\n";
            //******************************************************************************

            short r = 0;
            object x = null;
            object xc = null;
            int AcctNumber = 0;
            decimal BalAmount = default(decimal);
            ApChecks[] tblChecks = null;
            Transactions tblTran = null;
            Transactions[] tblTrans = null;
            int idx = 0;
            decimal orgamt = default(decimal);
            System.DateTime chkDate = default(System.DateTime);
            string ds = null;
            Transactions tbl = null;

            s = "Ready to post Reconciliation." + CrLf + CrLf;
            s = s + "If you have changed the Statement Amount" + CrLf;
            s = s + "for any check you will be shown an edit screen." + CrLf;
            s = s + "If you do NOT make changes there, the item will" + CrLf;
            s = s + "post as originally booked (the change will be" + CrLf;
            s = s + "discarded.\r\n\r\n";
            s = s + "Do you want to continue posting the reconciliation?";
            if (MessageBoxCustom.Show(s, "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return false;

            foreach (BankReconciliation check in checks)
            {
                if (!check.Type.Equals(BankErrorFor) && !check.Type.Equals(BankErrorAgainst) && check.Manual && check.Journal.Trim().Equals(""))
                {
                    MessageBoxCustom.Show("You must select a Journal for this transaction: " + check.Type + ": " + check.Date.ToShortDateString() + ": " + check.Payee + CrLf + check.BookAmount.ToString("$###,###,##0.00"));
                    return false;
                }
            }

            this.Cursor = Cursors.Wait;

            foreach (BankReconciliation check in checks)
            {
                if (check.ReconFlag)
                {
                    chkDate = check.Date;
                    xc = check.Type;
                    idx = check.IndexNumber;
                    //************************************
                    switch (xc)
                    {
                        case "DEPOSIT":
                        case "DEBIT":
                        case BankErrorFor:
                        case BankErrorAgainst:
                            if (idx < 0)
                            {
                                ds = check.Payee;
                                BalAmount = (xc == "DEPOSIT" || xc == BankErrorFor ? -check.StatementAmount : check.StatementAmount);

                                // CREDIT SIDE
                                tblTran = new Transactions();
                                tblTran.CompanyNumber = company.CompanyNumber;
                                tblTran.DepartmentNumber = department.DepartmentNumber;
                                tblTran.AccountNumber = -idx;
                                tblTran.FiscalYear = (short)check.Date.Year;
                                tblTran.Date = check.Date;
                                tblTran.Journal = check.Journal;
                                tblTran.Source = "Bank Reconciliation";
                                tblTran.Description = ds;
                                tblTran.Amount = BalAmount;
                                tblTran.RecFlag = true;
                                tblTran.save();
                                AcctNumber = -idx;

                                // DEBIT SIDE
                                tblTran = new Transactions();
                                tblTran.CompanyNumber = company.CompanyNumber;
                                tblTran.DepartmentNumber = department.DepartmentNumber;
                                tblTran.AccountNumber = GlCashAccount;
                                tblTran.FiscalYear = (short)check.Date.Year;
                                tblTran.Date = check.Date;
                                tblTran.Journal = check.Journal;
                                tblTran.Source = "Bank Reconciliation";
                                tblTran.Description = check.Payee;
                                BalAmount = (xc == "DEPOSIT" | xc == BankErrorFor ? check.StatementAmount : -check.StatementAmount);
                                tblTran.Amount = BalAmount;
                                tblTran.RecFlag = true;
                                tblTran.save();
                                tblTran = null;
                                AcctNumber = GlCashAccount;
                                Post_Budget();
                            }
                            else
                            {
                                orgamt = check.BookAmount;
                                xsql = "SELECT * FROM gl_Transactions WHERE IndexNumber = " + idx;
                                //xsql = xsql + " AND CompanyNumber = " + company.CompanyNumber + " AND Amount = " + orgamt;
                                // COULD BE FAILING ON 'AMOUNT' MATCH !!!!!!!!!!!!!!!!!!
                                tblTrans = Transactions.load(xsql);
                                if (tblTrans != null && tblTrans.Length > 0)
                                {
                                    BalAmount = check.StatementAmount - orgamt;
                                    tblTrans[0].Amount = check.StatementAmount;
                                    tblTrans[0].RecFlag = true;
                                    idx = tblTrans[0].AccountNumber;
                                    tblTrans[0].save();
                                    if (BalAmount != 0)
                                    {
                                        AcctNumber = idx;
                                        Post_Budget();
                                        idx = check.AccountNumber;
                                        if (idx > 0)
                                        {
                                            tbl = new Transactions();
                                            CSharp.CopyObjects.CopyProperties(tblTrans[0], tbl);
                                            tbl.AccountNumber = idx;
                                            tbl.Date = check.Date;
                                            //grdChecks.Col = 4
                                            //BalAmount = grdChecks.Value - orgamt
                                            tbl.Amount = -BalAmount;
                                            tbl.RecFlag = true;
                                            tbl.save();
                                            tbl = null;
                                            AcctNumber = idx;
                                            Post_Budget();
                                        }
                                    }
                                }
                                else
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "RECON: " + xsql, false);
                                }
                                tblTrans = null;
                            }
                            break;

                        default:
                            orgamt = check.BookAmount;
                            xsql = "SELECT * FROM gl_ApChecks WHERE Company = " + company.CompanyNumber;
                            xsql = xsql + " AND Department = " + department.DepartmentNumber + " AND ";
                            xsql = xsql + " CheckNumber = " + check.CheckNumber + " AND Sequence = 0";
                            tblChecks = ApChecks.LoadQuery(xsql);
                            if (tblChecks != null && tblChecks.Length > 0)
                            {
                                tblChecks[0].ReconFlag = true;
                                tblChecks[0].save();
                                tblChecks = null;
                            }
                            else
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "RECON: " + xsql, false);
                            }
                            if (orgamt != check.StatementAmount)
                            {
                                frmCheckRegisterEdit frm = new frmCheckRegisterEdit(check.IndexNumber.ToString());
                                frm.ShowDialog();
                            }
                            break;
                    }
                    //***********************************
                }
            }

            // DELETE SAVE FILE, IF ANY? - - - FileSystem.Kill(WinGL.My.MyProject.Application.Info.DirectoryPath + "\\" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstCompanies.ColList), "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(lstDepartments.ColList), "000_") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtStatement.Value, "yyyymmdd") + "." + WinglGlobal.SPext);
            if (CurrentReconClassID > 0)
            {
                CurrentReconClassID = 0;
            }

            totals = null;
            checks = null;
            company = null;
            department = null;
            txtStatement.Text = "0";
            this.DataContext = totals;
            grdChecks.ItemsSource = checks;
            this.Cursor = Cursors.Arrow;
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Posts the budget. </summary>
        ///
        /// <remarks>   Mike, 2017-03-07. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void Post_Budget()
        {
            /*
            // Budgets
            if (DateAndTime.Year(chkDate) < DateAndTime.Year(DateAndTime.Today)) {
                                xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + lstCompanies.ColList;
                                xsql = xsql + " AND DepartmentNumber = " + lstDepartments.ColList + " AND ";
                                xsql = xsql + "AccountNumber = " + AcctNumber;
                                tblBudget = WinglGlobal.GLdb.OpenRecordset(xsql);
                                if (tblBudget.RecordCount > 0) {
                                        tblBudget.edit();
                } else {
                    tblBudget = WinglGlobal.GLdb.OpenRecordset("Budgets");
                                        tblBudget.AddNew();
                                        tblBudget("CompanyNumber") = lstCompanies.ColList;
                                        tblBudget("DepartmentNumber") = lstDepartments.ColList;
                                        tblBudget("AccountNumber") = AcctNumber;
                }
                                x = "Actual" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(chkDate), "00");
                                tblBudget(x) = tblBudget(x) + BalAmount;
                                tblBudget.update();
                                tblBudget.Close();
            }
            return;
            */
        }

        private void cmdAccept_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            bool success = false;
            MessageBoxButton op = MessageBoxButton.OK;
            string s = "";
            decimal d = txtAdjStatementBalance.Value - txtAdjBookBal.Value;
            if (d < 0)
                i = -1;
            if (d == 0)
                i = 0;
            if (d > 0)
                i = 1;

            switch (i)
            {
                case -1:
                    s = "The reconciliation is NOT successful. \r\n\r\n";
                    s += "The Adjusted Statement Balance is \r\n";
                    s += Math.Abs(d).ToString("##,###,##0.00") + " LESS than the Book Balance.\r\n\r\n";
                    s += "You need to spend more or receive less.";
                    op = MessageBoxButton.OK;
                    break;
                case 0:
                    s = "The reconciliation IS successful.\r\n\r\n";
                    s += "You will now be given the opportunity\r\n";
                    s += "to post the results of the reconciliation\r\n";
                    s += "or abandon them.\r\n";
                    s += "If you choose to post the results, all cleared\r\n";
                    s += "Checks will be permanently marked as having\r\n";
                    s += "been cleared and all Adjustments that have\r\n";
                    s += "been assigned G/L Accounts will be posted\r\n";
                    s += "to the General Ledger.\r\n\r\n";
                    s += "You may also choose to continue with the\r\n";
                    s += "reconciliation, make any changes you wish,\r\n";
                    s += "and return to this point later.\r\n\r\n";
                    s += "Do you want to post the results?";
                    op = MessageBoxButton.YesNo;
                    success = true;
                    break;
                case 1:
                    s = "The reconciliation is NOT successful. \r\n\r\n";
                    s += "The Adjusted Statement Balance is \r\n";
                    s += Math.Abs(d).ToString("##,###,##0.00") + " MORE than the Book Balance.\r\n\r\n";
                    s += "You need to receive more or spend less.";
                    op = MessageBoxButton.OK;
                    break;
            }
            if (MessageBoxCustom.Show(s, "", op, this) == MessageBoxResult.Yes && success)
            {
                if (!wasSaved)
                {
                        //MessageBoxResult rc=  MessageBoxCustom.Show("Reconciliation was not Saved. Do it now?","",MessageBoxButton.YesNoCancel);
                        //switch (rc)
                        //{
                        //    case MessageBoxResult.Yes:
                    foreach (BankReconciliation check in checks)
                    {
                        check.Completed = true;
                    }
                    completed = true;
                    cmdSave_Click(sender, e);
                    //       break;
                    //  case MessageBoxResult.Cancel:
                    //       return;
                    //       break;
                    //}
                }

                Post_Recon();
            }
        }

        private void cmdReload_Click(object sender, RoutedEventArgs e)
        {
            if (company != null)
            {
                if (MessageBoxCustom.Show("There is an unposted Reconciliation.  Do you wish to abandon it?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            // Find saved, unfinished reconciliations
            /*
            Classes[] classes = Classes.loadMultiple(ShortName);
            if (classes != null && classes.Length > 0)
            {
                ArrayList classNames = new ArrayList();
                foreach (Classes cls in classes)
                {
                    classNames.Add(new ListItem(cls.name.Replace(ShortName + ":", ""), cls.ID, cls.value));

                }
 

                lstRecons.ItemsSource = classNames;
                //lstRecons.ItemsSource = savedDT.Rows;
                popReload.IsOpen = true;
                wasExported = false;
            }
            else
            {
                MessageBoxCustom.Show("No unfinished Bank Reconciliations were found.");
                return;
            }
            */
            
            bankCompanies = new BankReconCompaniesList();
            savedRecons = bankCompanies.GetReconRecords(ShortName);
            lstRecons.ItemsSource = savedRecons;
            popReload.IsOpen = true;
        }

        private System.Data.DataTable MakeSavedTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataColumn idCol = new DataColumn();
            idCol.DataType = System.Type.GetType("System.Int64");
            idCol.ColumnName = "ID";
            dt.Columns.Add(idCol);

            DataColumn nameCol = new DataColumn();
            nameCol.DataType = System.Type.GetType("System.String");
            nameCol.ColumnName = "Company";
            dt.Columns.Add(nameCol);

            DataColumn dateCol = new DataColumn();
            dateCol.DataType = System.Type.GetType("System.DateTime");
            dateCol.ColumnName = "Date";
            dt.Columns.Add(dateCol);



            return dt;
        }

        private void cmdDeleteRecon_Click(object sender, RoutedEventArgs e)
        {
            BankReconRecords item = (BankReconRecords)lstRecons.SelectedItem;
            if (item == null)
            {
                MessageBoxCustom.Show("You must first select a saved reconciliation to delete.");
                return;
            }
            if (MessageBoxCustom.Show("Delete this saved reconciliation?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Classes cls = Classes.load(item.ID);
                if (cls != null)
                    cls.delete(cls.ID.ToString());
            }
            savedRecons = GetSavedRecons();
            if (savedRecons != null && savedRecons.Count > 0)

                lstRecons.ItemsSource = savedRecons;
            else
                popReload.IsOpen = false;
        }

        private void cmdRetireveCancel_Click(object sender, RoutedEventArgs e)
        {
            popReload.IsOpen = false;
        }

        private void cmdGetRecon_Click(object sender, RoutedEventArgs e)
        {
            popReload.IsOpen = false;
            if (lstRecons.SelectedItem == null || ((BankReconBase)lstRecons.SelectedItem).Level != 0) // Top Level?
                return;
            BankReconRecords item = (BankReconRecords)lstRecons.SelectedItem;
            if (item == null)
            {
                MessageBoxCustom.Show("A saved Bank Reconciliation was NOT selected.");
                return;
            }
            CurrentReconClassID = item.ID;
            // Continue from here.  Get the class record using ID
            Classes cls = Classes.load(CurrentReconClassID);
            checks = GetRecon(cls.value);
            if (cls == null)
            {
                MessageBoxCustom.Show("Bank Reconcilitation record not found.");
                return;
            }
            company = Company.loadByCompany(((BankReconciliation)checks[0]).CompanyNumber);
            for (int i = 0; i < lstCompanies.Items.Count; i++)
            {
                if (((Company)lstCompanies.Items[i]).CompanyNumber == company.CompanyNumber)
                {
                    lstCompanies.SelectedIndex = i;
                    break;
                }
            }
            department = Department.loadByDepartment(((BankReconciliation)checks[0]).DepartmentNumber);
            for (int i = 0; i < lstDepartments.Items.Count; i++)
            {
                if (((Department)lstDepartments.Items[i]).DepartmentNumber == department.DepartmentNumber)
                {
                    lstDepartments.SelectedIndex = i;
                    break;
                }
            }
            if (checks[0].Type.Equals(Statement))
            {
                txtStatement.Text = ((BankReconciliation)checks[0]).StatementAmount.ToString("#######0.00");
                totals.BookBalance = ((BankReconciliation)checks[0]).BookAmount;
                dtDate.SelectedDate = ((BankReconciliation)checks[0]).StatementDate;
                checks.RemoveAt(0);
            }
            grdChecks.ItemsSource = checks;
            this.DataContext = null;
            if (totals == null)
                totals = new Totals();
            totals.TotalBookCleared = 0; totals.TotalStatementCleared = 0;
            // get_BookBalance();
            CalculateTotals();
            this.DataContext = totals;
            wasSaved = false;
        }

        private void CmdMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecons.SelectedItem == null || ((BankReconBase)lstRecons.SelectedItem).Level != 0) // Top Level?
                return;
            BankReconRecords item = (BankReconRecords)lstRecons.SelectedItem;
            if (item == null)
            {
                MessageBoxCustom.Show("A saved Bank Reconciliation was NOT selected.");
                return;
            }
            CurrentReconClassID = item.ID;
            // Continue from here.  Get the class record using ID
            Classes cls = Classes.load(CurrentReconClassID);
            checks = GetRecon(cls.value);
            if (cls == null)
            {
                MessageBoxCustom.Show("Bank Reconcilitation record not found.");
                return;
            }
            if (checks[0].Type.Equals(Statement))
            {
                ((BankReconciliation)checks[0]).Completed = true;
            }
            else
            {
                MessageBoxCustom.Show("Could not find summary record. Recon not marked COMPLETE");
                return;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BankReconciliation>), new Type[] { typeof(MbcsCentral.WinGL.BankReconciliation) });
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, checks);

            //string ClassName = (ShortName + ":" + company.Name).Left(38) + ": " + ((DateTime)dtDate.SelectedDate).ToShortDateString();
            //cls = Classes.load(0, ClassName);
            //if (cls == null)
            //{
            //    cls = new Classes();
            //    cls.name = (ShortName + ":" + company.Name).Left(38) + ": " + ((DateTime)dtDate.SelectedDate).ToShortDateString();
            //}
            cls.value = textWriter.ToString();
            cls.clientId = 1;
            cls.acctId = 0;
            cls.save();

            savedRecons = GetSavedRecons();
            lstRecons.ItemsSource = null;
            if (savedRecons != null && savedRecons.Count > 0)

                lstRecons.ItemsSource = savedRecons;

        }

        private List<BankReconciliation> GetRecon(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<BankReconciliation>));
            StringReader rdr = new StringReader(inputString);
            return (List<BankReconciliation>)serializer.Deserialize(rdr);
        }

        private void lstRecons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmdGetRecon_Click(sender, new RoutedEventArgs());
        }

        private void grdChecks_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGridRow dr = e.Row;
            BankReconciliation recon = (BankReconciliation)dr.Item;
            if (recon.Manual)
            {
                if (!recon.Type.Equals(BankErrorAgainst) && !recon.Type.Equals(BankErrorFor) && (recon.AccountNumber == null || recon.AccountNumber == 0))
                    MessageBoxCustom.Show("Select a NON-CASH G/L Account for this entry.");
            }
        }

        private void cmdReport_Click(object sender, RoutedEventArgs e)
        {
            CheckReconReport rpt = new CheckReconReport();
            rpt.BookBalance = totals.BookBalance;
            rpt.Company = company.CompanyNumber;
            rpt.CompanyName = company.Name;
            rpt.Department = department.DepartmentNumber;
            rpt.DepartmentName = department.Name;
            rpt.grdChecks = grdChecks;
            decimal amount = 0;
            decimal.TryParse(txtStatement.Text, out amount);
            rpt.StatementAmount = amount;
            rpt.StatementDate = (DateTime)dtDate.SelectedDate;
            rpt.print_report();
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int Row = 2;
            int Col = 1;

            for (int j = 0; j < 9; j++)
            {
                if (!grdChecks.Columns[j].Header.ToString().ToLower().Equals("edit"))
                {
                    Range myRange = (Range)sheet1.Cells[1, Col];
                    sheet1.Cells[1, Col].Font.Bold = true;
                    if (grdChecks.Columns[j].Header.ToString().ToLower().Equals("cleared"))
                        sheet1.Columns[Col++].ColumnWidth = 15;
                    else
                        sheet1.Columns[Col++].ColumnWidth = grdChecks.Columns[j].Width.Value / 4;
                    myRange.Value2 = grdChecks.Columns[j].Header;
                }
            }
            foreach (BankReconciliation recon in checks)
            {
                Col = 1;
                TextBlock a = new TextBlock() { Text = recon.Type };
                Microsoft.Office.Interop.Excel.Range myRangea = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRangea.Value2 = a.Text;

                a = new TextBlock() { Text = recon.Date.ToString("MM/dd/yyyy") };
                Microsoft.Office.Interop.Excel.Range myRangeb = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRangeb.Value2 = a.Text;

                a = new TextBlock() { Text = recon.BookAmount.ToString("#######0.00") };
                Microsoft.Office.Interop.Excel.Range myRangec = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRangec.Value2 = a.Text;

                a = new TextBlock() { Text = recon.StatementAmount.ToString("#######0.00") };
                Microsoft.Office.Interop.Excel.Range myRanged = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRanged.Value2 = a.Text;

                a = new TextBlock() { Text = recon.Payee };
                Microsoft.Office.Interop.Excel.Range myRangee = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRangee.Value2 = a.Text;

                a = new TextBlock() { Text = recon.ReconFlag.ToString() };
                Microsoft.Office.Interop.Excel.Range myRangeg = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                myRangeg.Value2 = a.Text;

                if (recon.Manual)
                {
                    a = new TextBlock() { Text = recon.AccountDescription };
                    Microsoft.Office.Interop.Excel.Range myRangef = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                    myRangef.Value = a.Text;

                    a = new TextBlock() { Text = recon.Journal };
                    Microsoft.Office.Interop.Excel.Range myRangeh = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[Row, Col++];
                    myRangeh.Value2 = a.Text;
                }
                Row++;
            }
            wasSaved = true;
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("Are you sure you want to do this?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DateTime date = dtClear.SelectedDate.Value.Date;
                foreach (BankReconciliation recon in checks)
                {
                    if (recon.Date.Date <= date.Date)
                        recon.ReconFlag = true;
                }
                grdChecks.ItemsSource = null;
                grdChecks.ItemsSource = checks;
            }
        }


        private List<BankReconCompanies> GetSavedRecons()
        {
            bankCompanies = new BankReconCompaniesList();

            bankCompanies = new BankReconCompaniesList();
            return bankCompanies.GetReconRecords(ShortName);
        }

    }


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A totals. </summary>
    ///
    /// <remarks>   Mike, 2017-03-07. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class Totals
    {
        public decimal BookBalance { get; set; }
        public decimal Adjustments { get; set; }
        public decimal AdjBookBalance { get; set; }
        public decimal TotalBookCleared { get; set; }
        public decimal TotalStatementCleared { get; set; }
        public decimal TotalDifference { get; set; }
        public decimal UnclearedDeposits { get; set; }
        public decimal UnclearedDebits { get; set; }
        public decimal UnclearedChecks { get; set; }
        public decimal AdjStatementBalance { get; set; }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A bank reconciliation. </summary>
    ///
    /// <remarks>   Mike, 2017-03-07. </remarks>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public class BankReconciliation
    {
        public string Type { get; set; }
        public bool Manual { get; set; }
        public string CheckNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal BookAmount { get; set; }
        public decimal StatementAmount { get; set; }
        public string Payee { get; set; }
        public bool ReconFlag { get; set; }
        public int IndexNumber { get; set; }
        public string AccountDescription { get; set; }
        public string Journal { get; set; } = "";
        public int AccountNumber { get; set; }
        public DateTime StatementDate { get; set; }
        public short CompanyNumber { get; set; }
        public short DepartmentNumber { get; set; }
        public bool Completed { get; set; } = false;

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static BankReconciliation DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BankReconciliation));
            StringReader rdr = new StringReader(inputString);
            BankReconciliation parms = (BankReconciliation)serializer.Deserialize(rdr);
            return parms;
        }
    }


}