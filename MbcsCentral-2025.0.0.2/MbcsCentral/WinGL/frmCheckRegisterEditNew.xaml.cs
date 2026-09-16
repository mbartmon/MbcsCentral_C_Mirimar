using CSharp;
using MbcsCentral.WinDoc;
using System;
using System.Collections;
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
    /// <summary>
    /// Interaction logic for frmCheckRegisterEdit.xaml
    /// </summary>
    public partial class frmCheckRegisterEdit : Window
    {
        string STindex;
        ApChecks check;
        ArrayList checks = new ArrayList();
        Company[] companies;
        Department[] departments;
        Vendors[] vendors;
        Accounts[] accounts;
        InvoiceHistory[] tblHist;
        int[] invNums;
        int OriginalCheckNumber;
        string Journal;
        bool scan;
        short postflag = 0;
        string WindocLocation;
        OleDbConnection CN;
        DocInterface[] rss;
        Documents[] rsd;
        int companyNumber;
        int deptNumber;
        int checkNumber;
        decimal OriginalAmount = 0;
        int newRowIndex = 0;
        CollectionViewSource Departmentsource;

        public frmCheckRegisterEdit(string Index)
        {
            InitializeComponent();
            STindex = Index;
        }

        private void frmCheckRegisterEdit_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int.TryParse(STindex, out index);
            if (index == 0 || (check = ApChecks.load(index)) == null)
            {
                MessageBoxCustom.Show("Could not find specified check");
                return;
            }
            companyNumber = check.Company;
            deptNumber = check.Department;
            checkNumber = check.CheckNumber;
            companies = Company.load();
            departments = Department.load();
            vendors = Vendors.load();
            accounts = Accounts.load();
            DateTime endDate = new DateTime(2999, 12, 31);

            checks = ApChecks.loadForRegisterList(companyNumber, deptNumber, checkNumber, checkNumber, GlobalShared.NULL_DATE, endDate, false);
            OriginalCheckNumber = ((ApChecks)checks[0]).CheckNumber;
            Main.getForm(this);
            WinglGlobal.invpGlobal.amountpaid = check.Amount;
            WinglGlobal.invpGlobal.Company = check.Company;
            WinglGlobal.invpGlobal.Department = check.Department;
            WinglGlobal.invpGlobal.vendor = check.VendorAccount;
            WinglGlobal.invpGlobal.CheckDate = check.Date;
            WinglGlobal.invpGlobal.date = check.Date;
            WinglGlobal.invpGlobal.CheckNumber = check.CheckNumber;
            Journal = check.Journal;
            Vendors vendor = Vendors.load(check.VendorAccount);
            if (vendor != null)
                WinglGlobal.invpGlobal.vName = vendor.VendorName;
            else
                WinglGlobal.invpGlobal.vName = "";

            lstCheckCompany.ItemsSource = companies;
            lstCheckDepartment.ItemsSource = departments;
            lstVendor.ItemsSource = vendors;

            CollectionViewSource Companysource = ((CollectionViewSource)(this.FindResource("companies")));
            Companysource.Source = companies;
            CollectionViewSource Departmentsource = ((CollectionViewSource)(this.FindResource("departments")));
            Departmentsource.Source = departments;
            CollectionViewSource Accountsource = ((CollectionViewSource)(this.FindResource("accounts")));
            Accountsource.Source = accounts;

            foreach (ApChecks chk in checks)
            {
                chk.CompanyList = companies;
            }

            LoadAllocations();
            this.DataContext = check;
            lstAllocations.ItemsSource = checks;

            Control[] control = Control.load();
            if (control != null && control.Length > 0)
            {
                // Why use this?
                postflag = control[0].GLMethod;
                // going to set to 1
                postflag = 1;

                scan = control[0].Scan;
                if (!scan)
                {
                    cmdScan.Visibility = Visibility.Collapsed;
                    cmdDisplay.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WindocLocation = control[0].WindocLocation.Trim();
                    if (WindocLocation == null || WindocLocation.Trim().Equals(""))
                    {
                        MessageBoxCustom.Show("Could not find Windoc Database <" + WindocLocation + ">. Invoices will not be scanned.");
                        scan = false;
                    }
                    else
                    {
                        if (!WindocLocation.EndsWith("\\"))
                            WindocLocation += "\\";
                        string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + WindocLocation.Trim() + "Windoc.mdb" + ";Mode=Share Deny None";
                        CN = new OleDbConnection(WindocConnectionString);
                    }

                }
                control[0].save();
            }
            control = null;
            if (scan)
            {
                string xsql = "SELECT DISTINCT InvoiceNumber FROM InvoiceHistory WHERE CheckNumber = " + check.CheckNumber + " AND VendorNumber = " + check.VendorAccount;
                xsql += " AND Company = " + check.Company + " AND Department = " + check.DistDepartment;
                InvoiceHistory[] tblHist = InvoiceHistory.LoadByCheck(check.CheckNumber, check.VendorAccount, check.Company, check.Department);
                if (tblHist != null && tblHist.Length > 0)
                {
                    WinglGlobal.invpGlobal.invnumber = tblHist[0].FullInvoiceNumber;
                    xsql = "SELECT * FROM " + Documents.TABLE + " WHERE " + Documents.SQL_COL_VENDOR + "='" + check.VendorAccount.ToString() + "' AND " + Documents.SQL_COL_INVOICE + " IN ('" + tblHist[0].FullInvoiceNumber.Trim() + "'";
                    for (int i = 1; i < tblHist.Length; i++)
                    {
                        xsql += ", '" + tblHist[i].FullInvoiceNumber.Trim() + "'";
                    }
                    xsql += ")";
                    tblHist = null;


                    rsd = Documents.LoadQuery(xsql, CN);
                    if (rsd != null && rsd.Length > 0)
                    {
                        cmdDisplay.Visibility = Visibility.Visible;
                        cmdEditDoc.Visibility = Visibility.Visible;
                        cmdScan.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        rss = DocInterface.load(CN);
                        if (rss != null)
                        {
                            cmdScan.Visibility = Visibility.Visible;
                            cmdDisplay.Visibility = Visibility.Collapsed;
                            cmdEditDoc.Visibility = Visibility.Collapsed;

                        }
                        else
                        {
                            MessageBoxCustom.Show("Error openning document interface table.  Invoices cannot be scanned.");
                            scan = false;
                            cmdScan.Visibility = Visibility.Collapsed;
                            cmdDisplay.Visibility = Visibility.Collapsed;
                            cmdEditDoc.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                else
                {
                    cmdDisplay.Visibility = Visibility.Collapsed;
                    cmdScan.Visibility = Visibility.Collapsed;
                    cmdEditDoc.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                cmdScan.Visibility = Visibility.Collapsed;
                cmdDisplay.Visibility = Visibility.Collapsed;
                cmdEditDoc.Visibility = Visibility.Collapsed;
            }
            check.Amount = CalcTotal();

            tblHist = InvoiceHistory.LoadByCheck(check.CheckNumber, check.VendorAccount, check.Company, check.Department);
            if (tblHist != null && tblHist.Length > 0)
            {
                grdInvoices.ItemsSource = tblHist;
            }

        }

        private void LoadAllocations()
        {

            if (checks.Count > 0)
            {
                Department[] depts = null;
                int i = 0;
                foreach (ApChecks check in checks)
                {
                    if (check.DistCompany != null && check.DistCompany != 0)
                    {
                        //((ApChecks)checks[i]).DistCompany = check.DistCompany;
                        //((ApChecks)checks[i]).CompanyName = check.CompanyName;
                        //((ApChecks)checks[i]).CompanyList = companies;
                        Department[] departments = Department.loadByCompany(check.DistCompany);
                        //departments = Department.loadByCompany(check.DistCompany);
                        if (departments == null || departments.Length == 0)
                        {
                            ((ApChecks)checks[i]).DistDepartment = 0;
                        }
                        else
                        {
                            ((ApChecks)checks[i]).DepartmentList = departments;
                            ((ApChecks)checks[i]).SelectedDepartment = departments[0];
                        }

                        i++;
                        //depts = Department.loadByCompany(check.DistCompany);
                        //((ApChecks)check).DepartmentList = depts;
                    }
                }
            }
            lstAllocations.ItemsSource = (ApChecks[])checks.ToArray(typeof(ApChecks));
            OriginalAmount = CalcTotal();

        }

        private void frmCheckRegisterEdit_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        //******************************************************************************
        private void SaveCheck()
        {
            //******************************************************************************

            int CashAcct = 0;
            string xsql = null;
            short t = 0;
            short i = 0;
            short j = 0;
            short seq = 0;
            Transactiontemp[] tblTransTemp = null;
            Transactions[] tblTrans;
            decimal diff = default(decimal);
            bool found = false;
            bool CashPosted = false;
            string vName = null;
            string Journal = "";
            string Source = "";
            Transactions Trans;
            //WinglGlobal.test_type tc = default(test_type);
            //DAO.Recordset tbl = null;
            short m = 0;

            if (check.CheckNumber <= 0)
            {
                MessageBoxCustom.Show("Check number must be entered.");
                return;
            }

            decimal net = 0;
            decimal total = 0;
            decimal.TryParse(txtNet.Text, out net);
            decimal.TryParse(txtTotal.Text, out total);
            if (net != total)
            {
                MessageBoxCustom.Show("Check total does not equal check net. Cannot save");
                return;
            }

            found = false;
            xsql = "SELECT * FROM " + Transactiontemp.TABLE + " WHERE " + Transactiontemp.SQL_COL_CHECK_NUMBER + "= " + check.ID.ToString() + " AND " + Transactiontemp.SQL_COL_COMPANY_NUMBER + " = " + check.Company.ToString();
            xsql = xsql + " AND DepartmentNumber = " + check.Department.ToString();
            tblTransTemp = Transactiontemp.load(xsql);
            if (tblTransTemp != null && tblTransTemp.Length > 0)
            {
                found = true;
                tblTransTemp = null;
            }
            else
            {
                xsql = "SELECT * FROM " + ApTrans.TABLE + " WHERE " + ApTrans.SQL_COL_CHECK_NUMBER + " = " + check.ID.ToString() + " AND " + ApTrans.SQL_COL_COMPANY + " = " + check.Company.ToString();
                xsql = xsql + " AND " + ApTrans.SQL_COL_COMPANY + " = " + check.Department.ToString();
                ApTrans[] aptrans = ApTrans.load(xsql);
                if (aptrans != null && aptrans.Length > 0)
                {
                    found = true;
                    aptrans = null;
                }
            }
            if (found)
            {
                MessageBoxCustom.Show("This check cannot be changed. First post G/L transactions.");
                return;
            }

            Vendors vend = (Vendors)lstVendor.SelectedItem;
            if (vend == null)
            {
                MessageBoxCustom.Show("Vendor not selected. Cannot save check.");
                return;
            }
            int vnd = vend.VendorNumber;
            vName = vend.VendorName; ;

            if (WinglGlobal.cr.details != null)
                System.Array.Clear(WinglGlobal.cr.details, 0, WinglGlobal.cr.details.Length);
            t = 0;

            Control control = Control.loadOne();
            if (control == null || control.GLCashAccount == 0)
            {
                MessageBoxCustom.Show("Could not find GL Cash Account. Check will not be saved.");
                return;
            }
            CashAcct = control.GLCashAccount;
            control = null;

            if (tblHist != null && tblHist.Length > 0)
            {
                foreach(InvoiceHistory inv in tblHist)
                {
                    if (inv.IsChanged)
                        inv.save();
                }
            }

            // Find transactions
            xsql = "SELECT * FROM " + Transactions.TABLE + " WHERE " + Transactions.SQL_COL_COMPANY_NUMBER + " = " + check.Company.ToString() + " AND " + Transactions.SQL_COL_DEPARTMENT_NUMBER + " = " + check.Department.ToString() + " AND " + Transactions.SQL_COL_CHECK_NUMBER + " = " + OriginalCheckNumber.ToString();
            tblTrans = Transactions.load(xsql);
            if (tblTrans != null && tblTrans.Length > 0)
            {
                Journal = tblTrans[0].Journal;
                Source = tblTrans[0].Source;
                int l = checks.Count;
                WinglGlobal.cr.details = null;
                WinglGlobal.cr.details = new WinglGlobal.details_type[tblTrans.Length];
                foreach (Transactions trans in tblTrans)
                {
                    if (trans.Description.ToLower().Contains(vName.ToLower()))
                    {
                        WinglGlobal.cr.details[t].AccountNumber = trans.AccountNumber;
                        WinglGlobal.cr.details[t].amount = trans.Amount;
                        WinglGlobal.cr.details[t].CompanyNumber = trans.CompanyNumber;
                        WinglGlobal.cr.details[t].CheckNumber = trans.CheckNumber;
                        WinglGlobal.cr.details[t].date = trans.Date;
                        WinglGlobal.cr.details[t].FiscalYear = trans.FiscalYear;
                        WinglGlobal.cr.details[t].VendorNumber = (trans.VendorNumber == null ? "" : trans.VendorNumber);
                        WinglGlobal.cr.details[t].DepartmentNumber = trans.DepartmentNumber;
                        WinglGlobal.cr.details[t].Description = trans.Description;
                        WinglGlobal.cr.details[t].Source = trans.Source;
                        WinglGlobal.cr.details[t].Journal = trans.Journal;
                        WinglGlobal.cr.details[t].IndexNumber = trans.IndexNumber;
                        WinglGlobal.cr.details[t].Flag = false;
                        t++;
                    }
                }
                if (l > tblTrans.Length)
                {
                    Array.Resize<WinglGlobal.details_type>(ref WinglGlobal.cr.details, WinglGlobal.cr.details.Length + 1);

                    WinglGlobal.cr.details[WinglGlobal.cr.details.Length - 1].Description = WinglGlobal.cr.details[WinglGlobal.cr.details.Length - 2].Description;
                }
                tblTrans = null;
            }


            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    // DELETE all current register entries for current check including master record
                    xsql = "DELETE FROM " + ApChecks.TABLE + " WHERE " + ApChecks.SQL_COL_COMPANY + " = " + check.Company.ToString() + " AND " + ApChecks.SQL_COL_DEPARTMENT + " = " + check.Department.ToString() + " AND " + ApChecks.SQL_COL_CHECK_NUMBER + " = " + OriginalCheckNumber.ToString();
                    ApChecks.Query(xsql, GlobalShared.CN);

                    decimal CheckTotal = CalcTotal();
                    // Create new master record
                    ApChecks tblChecks = new ApChecks();
                    tblChecks.Company = check.Company;
                    tblChecks.Department = check.Department;
                    tblChecks.CheckNumber = int.Parse(txtCheckNumber.Text);
                    tblChecks.Sequence = seq;
                    tblChecks.Date = (DateTime)dtCheck.SelectedDate;
                    tblChecks.Amount = CheckTotal;
                    tblChecks.VendorAccount = vnd;
                    tblChecks.Journal = Journal;
                    tblChecks.ReconFlag = check.ReconFlag;

                    tblChecks.save();

                    //******************************************************************************************
                    // Match G/L transactions against check allocations, changing transactions where necessary *
                    //******************************************************************************************

                    Transactions.DeleteByCheck(check.Company, check.Department, OriginalCheckNumber);

                    // Save check allocations and match against G/L transactions

                    string tranDescription = "Transaction modified in CheckRegister Edit";
                    seq = 0; t = 0;
                    foreach (ApChecks chk in checks)
                    {
                        if (chk.Amount != 0 && !chk.Delete)
                        {
                            tblChecks = new ApChecks();
                            tblChecks.Company = check.Company;
                            tblChecks.Department = check.Department;
                            tblChecks.CheckNumber = check.CheckNumber;
                            tblChecks.Sequence = ++seq;
                            tblChecks.Date = (DateTime)dtCheck.SelectedDate;
                            tblChecks.DistCompany = check.Company;
                            tblChecks.DistDepartment = check.Department;
                            tblChecks.VendorAccount = chk.VendorAccount;
                            tblChecks.Amount = chk.Amount;
                            tblChecks.Journal = Journal;
                            tblChecks.ReconFlag = check.ReconFlag;
                            tblChecks.save();

                            Trans = new Transactions();
                            Trans.CompanyNumber = chk.Company;
                            Trans.DepartmentNumber = chk.DistDepartment;
                            Trans.AccountNumber = chk.VendorAccount;
                            Trans.CheckNumber = check.CheckNumber;
                            Trans.FiscalYear = (WinglGlobal.cr.details != null ? WinglGlobal.cr.details[0].FiscalYear : (short)DateTime.Now.Year);
                            Trans.Date = (DateTime)dtCheck.SelectedDate;
                            Trans.Journal = Journal;
                            Trans.Source = Source;
                            Trans.Description = (WinglGlobal.cr.details != null && WinglGlobal.cr.details[t].Description != null ? WinglGlobal.cr.details[t++].Description : tranDescription);
                            Trans.VendorNumber = check.VendorAccount.ToString();
                            Trans.Amount = chk.Amount;
                            Trans.save();
                            Trans = null;

                            if (postflag == 0)
                                PostCredit(Trans, chk, CashAcct, Journal, Source);
                            //else
                            //    CheckTotal += chk.Amount;
                        }
                    }

                    if (postflag == 1)
                    {
                        check.Amount = CheckTotal;
                        PostCredit(new Transactions(), check, CashAcct, Journal, Source);
                        /*
                        Trans = new Transactions();
                        Trans.CompanyNumber = check.Company;
                        Trans.DepartmentNumber = check.Department;
                        Trans.AccountNumber = CashAcct;
                        Trans.CheckNumber = check.CheckNumber;
                        Trans.FiscalYear = WinglGlobal.cr.details[0].FiscalYear;
                        Trans.Date = check.Date;
                        Trans.Journal = Journal;
                        Trans.Source = Source;
                        Trans.Description = WinglGlobal.cr.details[0].Description;
                        Trans.VendorNumber = check.VendorAccount.ToString();
                        Trans.Amount = check.Amount;
                        Trans.save();
                        Trans = null;
                        */
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {

                }
            }
            this.Close();
        }

        private void PostCredit(Transactions Trans, ApChecks check, int cashAcct, string journal, string source)
        {
            Trans = new Transactions();
            string tranDescription = "Transaction modified in CheckRegister Edit";
            Trans.CompanyNumber = check.Company;
            Trans.DepartmentNumber = check.Department;
            Trans.AccountNumber = cashAcct;
            Trans.CheckNumber = check.CheckNumber;
            Trans.FiscalYear = (WinglGlobal.cr.details != null ? WinglGlobal.cr.details[0].FiscalYear : (short)DateTime.Now.Year);
            Trans.Date = check.Date;
            Trans.Journal = journal;
            Trans.Source = source;
            Trans.Description = (WinglGlobal.cr.details != null && WinglGlobal.cr.details[0].Description != null ? WinglGlobal.cr.details[0].Description : tranDescription);
            Trans.VendorNumber = check.VendorAccount.ToString();
            Trans.Amount = -check.Amount;
            Trans.save();
            Trans = null;

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalAmount != CalcTotal())
            {
                if (MessageBoxCustom.Show("Current check total: " + CalcTotal().ToString("###,###,##0.00") + " does not equal original amount: " + OriginalAmount.ToString("###,###,##0,00") + "\r\n\r\nSave anyway?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            SaveCheck();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdScan_Click(object sender, RoutedEventArgs e)
        {
            WinglGlobal.scanImmediate(true, WinglGlobal.invpGlobal, WindocLocation);
        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            string docIDs = "";
            foreach (Documents doc in rsd)
                docIDs += doc.DocumentId.ToString() + " ";
            bool r;
            System.Diagnostics.Process exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = WindocLocation.Trim() + "\\WindocViewer.exe";
            exProc.StartInfo.Arguments = docIDs;
            try
            {
                r = exProc.Start();
            }
            catch (System.Exception)
            {
            }
        }

        private decimal CalcTotal()
        {
            decimal total = 0;
            foreach (ApChecks checkRow in checks)
            {
                if (checkRow.Sequence > 0 && !checkRow.Delete)
                {
                    total += checkRow.Amount;
                }
            }
            txtTotal.Text = total.ToString("$###,##0.00");
            return total;
        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcTotal();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxCustom.Show("Delete this check?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string xsql = "DELETE FROM " + ApChecks.TABLE + " WHERE [" + ApChecks.SQL_COL_COMPANY + "] = " + companyNumber.ToString() +
                    " AND [" + ApChecks.SQL_COL_DEPARTMENT + "] = " + deptNumber.ToString() + " AND [" + ApChecks.SQL_COL_CHECK_NUMBER + "] = " + checkNumber.ToString();
                ApChecks.Query(xsql, GlobalShared.CN);
                xsql = "DELETE FROM " + ApTrans.TABLE + " WHERE [" + ApTrans.SQL_COL_DATE + "] = '" + check.Date.ToString() +
                    "' AND [" + ApTrans.SQL_COL_CHECK_NUMBER + "] = " + checkNumber.ToString();
                ApTrans.Query(xsql, GlobalShared.CN);
                xsql = "DELETE FROM " + Transactions.TABLE + " WHERE [" + Transactions.SQL_COL_COMPANY_NUMBER + "] = " + companyNumber.ToString() +
                    " AND [" + Transactions.SQL_COL_DEPARTMENT_NUMBER + "] = " + deptNumber.ToString() + " AND [" +
                    Transactions.SQL_COL_CHECK_NUMBER + "] = " + checkNumber.ToString();
                Transactions.Query(xsql, GlobalShared.CN);
                WinglGlobal.deleteDocuments(checkNumber, check.VendorAccount, (short)companyNumber, (short)deptNumber);
                this.Close();
            }
        }

        private void cmdAddLine_Click(object sender, RoutedEventArgs e)
        {
            ApChecks newCheck = new ApChecks();
            int i = checks.Count;
            newCheck.CheckNumber = checkNumber;
            newCheck.Sequence = (short)(i + 1);
            newCheck.Company = check.Company;
            newCheck.CompanyName = check.CompanyName;
            newCheck.DistDepartment = check.Department;
            newCheck.DepartmentName = check.DepartmentName;
            departments = Department.loadByCompany(check.Company);
            if (departments == null || departments.Length == 0)
            {
                newCheck.DistDepartment = 0;
            }
            else
            {
                newCheck.DepartmentList = departments;
                newCheck.SelectedDepartment = departments[0];
                newCheck.DistDepartment = departments[0].DepartmentNumber;

            }
            //newCheck.DepartmentList = ((ApChecks)checks[i - 1]).DepartmentList;


            if (i >= 1)
            {
                /*
                       check.Company = ((ApChecks)checks[i - 1]).DistCompany;
                       check.CompanyName = ((ApChecks)checks[i - 1]).CompanyName;
                       check.SelectedCompany = ((ApChecks)checks[i - 1]).SelectedCompany;
                       check.DistDepartment = ((ApChecks)checks[i - 1]).DistDepartment;
                       check.DepartmentName = ((ApChecks)checks[i - 1]).DepartmentName;
                       check.DepartmentList = ((ApChecks)checks[i - 1]).DepartmentList;
                       check.SelectedDepartment = ((ApChecks)checks[i - 1]).SelectedDepartment;
                       */

            }
            checks.Add(newCheck);
            lstAllocations.ItemsSource = null;
            lstAllocations.ItemsSource = (ApChecks[])checks.ToArray(typeof(ApChecks));

        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lstAllocations.SelectedIndex;
            if (index < 0)
            {
                MessageBoxCustom.Show("An Allocation has not been selected to toggle deletion.", "", MessageBoxButton.OK, this);
                return;
            }
            ApChecks check = (ApChecks)lstAllocations.SelectedItem;
            CalcTotal();
        }

        private void CompanySelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            int n = lstAllocations.SelectedIndex;
            if (n < 0 || box.SelectedItem == null || box == null)
                return;


            switch (box.Tag)
            {
                case "ListCompanyDist":
                    Company company = (Company)box.SelectedItem;
                    if (company != null)
                    {
                        ((ApChecks)checks[n]).DistCompany = ((Company)box.SelectedItem).CompanyNumber;
                        Department[] departments = Department.loadByCompany(company.CompanyNumber);
                        departments = Department.loadByCompany(company.CompanyNumber);
                        if (departments == null || departments.Length == 0)
                        {
                            ((ApChecks)checks[n]).DistDepartment = 0;
                        }
                        else
                        {
                            ((ApChecks)checks[n]).DepartmentList = departments;
                            ((ApChecks)checks[n]).SelectedDepartment = departments[0];
                            ((ApChecks)checks[n]).DistDepartment = departments[0].DepartmentNumber;

                        }
                    }
                    break;

                case "ListDepartmentsDist":
                    ((ApChecks)checks[n]).DepartmentName = ((Department)box.SelectedItem).Name;
                    ((ApChecks)checks[n]).DistDepartment = ((Department)box.SelectedItem).DepartmentNumber;
                    break;
            }
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

        private void CmdEditDoc_Click(object sender, RoutedEventArgs e)
        {
            Main.scanEdit(rsd[0].DocumentId, WindocLocation);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string s = txt.Text.Trim();
            txt.Text = s;
            txt.SelectionStart = 0;
            txt.SelectionLength = s.Length;
        }


    }

}
