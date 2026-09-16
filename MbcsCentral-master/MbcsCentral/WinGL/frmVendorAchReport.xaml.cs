using ExtensionMethods;
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


namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmVendorAchReport.xaml
    /// </summary>
    public partial class frmVendorAchReport : Window
    {
        private string NachaFileDir;
        private string NachaFilePrefix;
        private const char NACHA_FILE_DELIMITER = '_';
        private ArrayList vendorList = new ArrayList();
        private DataTable dt = new DataTable();
        private Report rpt;
        int ln = 0;
        int pg = 0;
        int linesPerPage = 60;

        private string thisApp;

        public class VendorAchReport
        {
            public bool IsSelected { get; set; } = true;
            public string FileName { get; set; }
        }

        public DateTime StartDate
        {
            get { return (DateTime)dtFrom.SelectedDate; }
            set { dtFrom.SelectedDate = value; }
        }

        public DateTime EndDate
        {
            get { return (DateTime)dtTo.SelectedDate; }
            set { dtTo.SelectedDate = value; }
        }

        public ArrayList lstFiles;
        private string NachaCheckingCredit;

        public frmVendorAchReport()
        {
            InitializeComponent();
        }

        public frmVendorAchReport(string thisApp)
        {
            InitializeComponent();
            this.thisApp = thisApp;
            if (!GlobalShared.user.hasAccessPermission(thisApp))
            {
                MessageBoxCustom.Show("You do not have permission to use the APP");
                this.Close();
            }
        }

        private void frmVendorAchReport_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            NACHA_Layout[] NachaControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
            foreach (NACHA_Layout rec in NachaControl)
            {
                switch (rec.Position)
                {
                    case (int)NACHA_Layout.Control_Types.VendorFileDirectory:
                        NachaFileDir = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorFilePrefix:
                        NachaFilePrefix = rec.Contents;
                        break;
                    case (int)NACHA_Layout.Control_Types.VendorCheckingCredit:
                        NachaCheckingCredit = rec.Contents.Trim();
                        break;
                }
                DateTime dt = DateTime.Now;
                StartDate = new DateTime(dt.Year, dt.Month, 1);
                EndDate = StartDate.AddMonths(1).AddDays(-1);
                txtFilePath.Text = NachaFileDir;
            }
            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("CompanyNumber");
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("VendorNumber");
            dt.Columns.Add("SEC");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("InvoiceNumber");
            dt.Columns.Add("SourceFile");
            DataColumn colAmount = new DataColumn("colAmount");
            colAmount.DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add(colAmount);

        }

        private void DisplayReport()
        {             
            bool filesFound = false;
            if (lstFiles == null || lstFiles.Count == 0)
            {
                MessageBoxCustom.Show("No ACH result files found.");
                return;
            }
            dt.Rows.Clear();
            foreach (VendorAchReport file in lstFiles)
            {
                if (file.IsSelected)
                {
                    ReadFile(file.FileName, dt);
                    filesFound = true;
                }
            }
            if (!filesFound || dt == null || dt.Rows.Count == 0)
            {
                MessageBoxCustom.Show("No ACH result files selected.");
                return;
            }

            string currentCompany = "";
            string currentDate = "";
            string currentSEC = "";
            string currentVendor = "";
            string currentVendorName = "";
            string currentSourceFile = "";

            decimal vendorTotal = 0;
            decimal secTotal = 0;
            decimal sourceFileTotal = 0;

            string fontname = "Courier New";
            float fontsize = 8;

            rpt = new Report("Vendor Payments", fontname, fontsize) { LeftMargin = (Single)10, TopMargin = (Single)10, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };

            ln = 0; pg = 0;
            DataView view = new DataView(dt);

            //Source File, Company, SEC, Date, Vendor
            view.Sort = "CompanyName ASC,PaymentDate ASC, SEC ASC, SourceFile ASC, VendorNumber ASC";
            DataRowView row = view[0];
            currentCompany = (string)row["CompanyName"];
            currentDate = (string)row["PaymentDate"];
            currentSEC = (string)row["SEC"];
            currentVendor = (string)row["VendorNumber"];
            currentVendorName = (string)row["VendorName"];
            currentSourceFile= (string)row["SourceFile"];

            PrintReportHeader();
            //SourceFileHeader(currentSourceFile);
            CompanySecHeader(currentCompany, currentSEC, currentDate);
            foreach (DataRowView drv in view)
            {
                //if (currentSourceFile != (string)drv["SourceFile"])
                //{
                //    VendorTotalFooter(currentVendorName, vendorTotal);
                //    CompanySecFooter(currentCompany, currentSEC, currentDate, secTotal);
                //    SourceFileFooter(currentSourceFile, sourceFileTotal);
                //    currentSourceFile = (string)drv["SourceFile"];
                //    currentCompany = (string)drv["CompanyName"];
                //    currentSEC = (string)drv["SEC"];
                //    currentDate = (string)drv["PaymentDate"];
                //    //currentVendor = (string)drv["VendorNumber"];
                //    //currentVendorName = (string)drv["VendorName"];
                //    sourceFileTotal = 0;
                //    secTotal = 0;
                //    vendorTotal = 0;
                //    SourceFileHeader(currentSourceFile);
                //    rpt.PrintPageLine("");
                //    CompanySecHeader(currentCompany, currentSEC, currentDate);

                //}
                if (currentCompany != (string)drv["CompanyName"] || currentSEC != (string)drv["SEC"] || (string)currentDate != (string)drv["PaymentDate"] || currentSourceFile != (string)drv["SourceFile"])
                {
                    VendorTotalFooter(currentVendorName, vendorTotal);

                    CompanySecFooter(currentCompany, currentSEC, currentDate, secTotal);

                    currentCompany = (string)drv["CompanyName"];
                    currentSEC = (string)drv["SEC"];
                    currentDate = (string)drv["PaymentDate"];
                    secTotal = 0;
                    currentVendor = (string)drv["VendorNumber"];
                    currentVendorName = (string)drv["VendorName"];
                    currentSourceFile = (string)drv["SourceFile"];
                    vendorTotal = 0;
                    rpt.PrintPageLine("");
                    CompanySecHeader(currentCompany, currentSEC, currentDate);

                }   
                else if (currentVendor != (string)drv["VendorNumber"])
                {
                    VendorTotalFooter(currentVendorName, vendorTotal);
                    currentVendor = (string)drv["VendorNumber"];
                    currentVendorName = (string)drv["VendorName"];
                    vendorTotal = 0;

                }
                vendorTotal += (decimal)drv["colAmount"];
                secTotal += (decimal)drv["colAmount"];
                sourceFileTotal += (decimal)drv["colAmount"];
            }

            if (ln > linesPerPage)
            {
                rpt.NewPage();
                ln = 0;
                PrintReportHeader();
            }
            VendorTotalFooter(currentVendorName, vendorTotal);
            CompanySecFooter(currentCompany, currentSEC, currentDate, secTotal);
            //SourceFileFooter(currentSourceFile, sourceFileTotal);
            rpt.DisplayDocument();
        }

        private void PrintReportHeader ()
        {
            rpt.FontSize = 18;
            rpt.FontBold = true;
            rpt.PrintPageLine("Cumulative Vendor ACH Payments  " + ((DateTime)dtFrom.SelectedDate).ToShortDateString() + " - " + ((DateTime)dtTo.SelectedDate).ToShortDateString() + "  page " + (++pg).ToString());
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            ln += 5;
        }

        private void CompanySecHeader(string companyName, string sec, string date)
        {
            if (ln > linesPerPage)
            { 
                rpt.NewPage();
                ln = 0;
                PrintReportHeader();
            }
            string s = "Company: " + companyName + " SEC: " + sec + " Date: " + date;
            string underline = new string('-', 92);
            rpt.FontBold = true;
            rpt.FontSize = 14;
            rpt.PrintPageLine(s);
            rpt.FontSize = 12;
            rpt.PrintPageLine(underline);
            ln += 2;
        }

        private void SourceFileHeader(string sourceFile)
        {
            if (ln > linesPerPage)
            {
                rpt.NewPage();
                ln = 0;
                PrintReportHeader();
            }
            string s = "Source File: " + sourceFile + "";
            string underline = new string('-', 92);
            rpt.FontBold = true;
            rpt.FontSize = 14;
            rpt.PrintPageLine(s);
            rpt.FontSize = 12;
            rpt.PrintPageLine(underline);
            ln += 2;
        }

        private void SourceFileFooter(string sourceFile, decimal total)
        {
            if (ln > linesPerPage)
            {
                rpt.NewPage();
                ln = 0;
                PrintReportHeader();
            }
            string[] s = { "Total for Source File " + sourceFile ,  "tab(80)", total.ToString("###,##0.00").PadLeft(12) };
            string underline = new string('=', 92);
            rpt.FontBold = true;
            rpt.FontSize = 12;
            rpt.PrintPageLine(underline);
            rpt.PrintPageLine(s);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            ln += 3;
        }

        private void VendorTotalFooter(string vendorName, decimal total)
        {
            if (total == 0)
                return;
            if (ln > linesPerPage)
            {
                rpt.NewPage();
                ln = 0;
                PrintReportHeader(); 
            }
            string[] s = { "   " + vendorName, "tab(80)", total.ToString("###,##0.00").PadLeft(12) };
            rpt.FontSize = 12;
            rpt.PrintPageLine(s);
            rpt.FontSize = 12;
            ln++;
        }

        private void CompanySecFooter(string companyName, string sec, string date, decimal total)
        {
            if (ln > linesPerPage)
            {
                rpt.NewPage();
                ln = 0;
                PrintReportHeader();
            }
            string[] s = { "Total for " + companyName + " SEC: " + sec + " " + date, "tab(80)", total.ToString("###,##0.00").PadLeft(12) };
            string underline = new string('=', 92);
            rpt.FontBold = true;
            rpt.FontSize = 12;
            rpt.PrintPageLine(underline);
            rpt.PrintPageLine(s);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            ln += 3;
        }

        private void ReadFile(string filepath, DataTable dt)
        {
            int standardEntryClassPosition = 0;
            int standardEntryClassLength = 0;
            int vendorNumberPosition = 0;
            int vendorNumberLength = 0;
            int invoicePosition = 0;
            int invoiceLength = 0;
            int amountPosition = 0;
            int amountLength = 0;
            int effectiveDatePosition = 0;
            int effectiveDateLength = 0;
            int companyNamePosition = 0;
            int companyNameLength = 0;
            int companyDiscretionaryDataPosition = 0;
            int companyDiscretionaryDataLength = 0;
            int vendorNumber = 0;
            Company company = null;
            string checkDate = "";
            string serviceClass = "";
            Vendors vendor = null;
            bool vendorFound = false;
            VendorEmail vendorEmail = null;

            NACHA_Layout[] batchHeader = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchVendorHeader);
            foreach (NACHA_Layout rec in batchHeader)
            {
                switch (rec.Name.Trim())
                {
                    case NachaBatchVendorHeader.COL_EFFECTIVE_DATE:
                        effectiveDatePosition = rec.Position;
                        effectiveDateLength = rec.Length;
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_NAME:
                        companyNamePosition = rec.Position;
                        companyNameLength = rec.Length;
                        break;
                    case NachaBatchVendorHeader.COL_COMPANY_DISCRETIONARY_DATA:
                        companyDiscretionaryDataPosition = rec.Position;
                        companyDiscretionaryDataLength += rec.Length;
                        break;
                    case NachaBatchVendorHeader.COL_STANDARD_ENTRY_CLASS_CODE:
                        standardEntryClassPosition = rec.Position;
                        standardEntryClassLength = rec.Length;
                        break;
                }
            }


            NACHA_Layout[] detailRecord = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.DetailRecord);
            foreach (NACHA_Layout rec in detailRecord)
            {
                switch (rec.Name.Trim())
                {
                    case NachaDetail.COL_INDIVIDUAL_IDENTIFICATION_NUMBER:
                        vendorNumberPosition = rec.Position;
                        vendorNumberLength = rec.Length;
                        break;
                    case NachaDetail.COL_AMOUNT:
                        amountPosition = rec.Position;
                        amountLength = rec.Length;
                        break;
                }
            }

            TextReader tr = new StreamReader(filepath);
            string line = tr.ReadLine();
            string date = "";
            string companyName = "";
            string amount = "";
            string SourceFile = System.IO.Path.GetFileName(filepath);
            bool process = true;
            while (line != null)
            {
                if (line.StartsWith("5"))
                {
                    string s = line.Substring(effectiveDatePosition - 1, effectiveDateLength);
                    if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                    {
                        date = s.Substring(2, 2) + "/" + s.Substring(4, 2) + "/" + s.Substring(0, 2);
                        DateTime d = DateTime.Parse(date);
                        if (d.CompareTo(StartDate) < 0 || d.CompareTo(EndDate) > 0)
                        {
                            process = false;
                            line = tr.ReadLine();
                            continue;
                        }
                        process = true;
                    }
                    s = line.Substring(companyDiscretionaryDataPosition - 1, companyDiscretionaryDataLength);
                    if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                    {
                        int companyNumber = 0;
                        int.TryParse(s.Trim(), out companyNumber);
                        company = Company.loadByCompany(companyNumber);
                        if (company != null)
                        {
                            companyName = company.Name;
                        }
                        else
                        {
                            s = line.Substring(companyNamePosition - 1, companyNameLength);
                            if (s != null && s.Length > 0 && !s.Trim().Equals(""))
                                companyName = s.Trim();
                        }
                        serviceClass = line.Substring(standardEntryClassPosition - 1, standardEntryClassLength);
                    }
                }
                if (line.StartsWith("6" + NachaCheckingCredit) && process)
                {
                    vendorNumber = 0;
                    int.TryParse(line.Substring(vendorNumberPosition - 1, vendorNumberLength), out vendorNumber);
                    if (vendorNumber > 0)
                    {
                        vendor = Vendors.load(vendorNumber);
                        if (vendor != null) // && vendor.ACH && vendor.ACHEmail != null && !vendor.ACHEmail.Trim().Equals(""))
                            vendorFound = true;
                    }
                    double d = 0;
                    amount = line.Substring(amountPosition - 1, amountLength).Trim();
                    double.TryParse(amount, out d);
                    amount = (d * 0.01).ToString("#,###,##0.00");

                }
                if (line.StartsWith("7") && vendorFound && process)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["colAmount"] = amount;
                    dataRow["CompanyNumber"] = company.CompanyNumber;
                    dataRow["VendorNumber"] = vendorNumber;
                    dataRow["SEC"] = serviceClass;
                    dataRow["CompanyName"] = companyName;
                    dataRow["VendorName"] = vendor.FullName;
                    dataRow["PaymentDate"] = date;
                    dataRow["SourceFile"] = SourceFile;
                    dt.Rows.Add(dataRow);
                    vendorFound = false;
                }
                line = tr.ReadLine();
            }
            tr.Close();

        }
        private void GetFiles()
        {
            string[] files = Directory.GetFiles(NachaFileDir, NachaFilePrefix + "*.txt");
            if (files == null || files.Length == 0)
            {
                MessageBoxCustom.Show("No ACH result files found.");
                return;
            }

            string start = StartDate.ToString("yyyyMMdd");
            string end = EndDate.ToString("yyyyMMdd");
            lstFiles = null;
            lstFiles = new ArrayList();
            int pos = 0;

            foreach (string file in files)
            {
                for(pos = 0; pos < file.Length; pos++)
                {
                    char c = Convert.ToChar(file.Substring(pos, 1)) ;
                    if (c =='_')
                    {
                        char x = Convert.ToChar(file.Substring(pos + 1,1));
                        if (x == NACHA_FILE_DELIMITER)
                        {
                            pos++;
                        }
                        break;
                    }
                }
                if (pos > 0)
                {
                    string date = file.Substring(pos + 1, 8);
                    if (date.CompareTo(start) >= 0 && date.CompareTo(end) <= 0)
                        lstFiles.Add(new VendorAchReport() { FileName = file});
                }
                else
                {
                    pos = file.IndexOf(NACHA_FILE_DELIMITER);
                    if (pos > 0)
                    {
                        string date = file.Substring(pos + 1, 8);
                        if (date.CompareTo(start) >= 0 && date.CompareTo(end) <= 0)
                            lstFiles.Add(file);

                    }
                }
            }
            if (lstFiles.Count == 0)
            {
                MessageBoxCustom.Show("No ACH result files found for the selected date range.");
                return;
            }
            lstVendorAchReport.ItemsSource = lstFiles;
        }

        private void cmdBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Select the directory containing the ACH result files";
            dlg.SelectedPath = NachaFileDir;
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NachaFileDir = dlg.SelectedPath;
                txtFilePath.Text = NachaFileDir;
            }
        }   

        private void cmdLoad_Click(object sender, RoutedEventArgs e)
        {
            GetFiles();
        }
        private void frmVendorAchReport_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            DisplayReport();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
