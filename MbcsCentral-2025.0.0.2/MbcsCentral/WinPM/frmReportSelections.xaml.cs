using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using VIBlend.WPF.Controls;
using Xceed.Wpf.Toolkit;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmReportSelections.xaml
    /// </summary>
    public partial class frmReportSelections : Window
    {
        private int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        private string reportName;
        private int reportKey;
        private decimal minBalance = 7.00M;
        bool AddChg;
        bool ByBuilding;
        bool Alphabetical;
        DateTime dt1;
        DateTime dt2;
        Classes rptClass;
        string className = "";
        ReportParameters parms;
        System.Printing.PrintQueue queue = null;
        private string m_thisApp;
        public string ThisApp
        {
            set { m_thisApp = value; }
        }

        public enum ReportKeys
        {
            OwnerMaster = 0,    // Only Display/Print
            BuildingMaster = 1, // Only Display Print
            TenantMaster = 2,   // By Building Checkbox
            TenantPhoneList = 3,// By Building Checkbox
            SelectChargeRpt = 4,// Charge ComboBox, Skip between buildings Checkbox
            ArrearsReport = 5,  // Minimum Balance NumberEditor, Additonal Arrears CheckBox
            PaymentRegister = 6,// From/To Dates
            VacancyList = 7, 
            LeaseExpiration = 8,// From/To Dates
            PreferredRentWS = 9, // Single Date: Leases Expiring on | PromptPay Credit Charge code
            PreferredRentRpt = 10, // Single Date: Leases Expiring on | PromptPay Credit Charge code
            RentRollForMortgages = 11, // Choose Single Builing
            AgingReport = 12,  // Display Aging Report up to 90 days
        };
        // Must match number of enum values above
        private int MaxKeys = 13;

        // Properties
        private int reporttKey;
        public int ReportKey
        {
            set {ReportKey = value;}
        }
                    
        public frmReportSelections()
        {
            InitializeComponent();
        }

        public frmReportSelections(string parm)
        {
            InitializeComponent();
            int key = int.Parse(parm);
            if (key < 0 || key > MaxKeys - 1)
            {
                System.Windows.MessageBox.Show("Invalid Report Key passed.");
                this.Close();
            }
            reportKey = key;
        }

        public frmReportSelections(string app, string parm)
        {
            InitializeComponent();
            m_thisApp = app;
            int key = int.Parse(parm);
            if (key < 0 || key > MaxKeys - 1)
            {
                System.Windows.MessageBox.Show("Invalid Report Key passed.");
                this.Close();
            }
            reportKey = key;
        }

        private void formReportSelections_Loaded(object sender, RoutedEventArgs e)
        {
            if (reportKey < 0 || reportKey > MaxKeys - 1)
            {
                System.Windows.MessageBox.Show("Invalid Report Key passed.");
                this.Close();
            }

            Main.getForm(this);
            datReport.SelectedDate = DateTime.Now;
            className = this.GetType().Name;
            rptClass = Classes.load(acctId, className);
            if (rptClass != null)
                parms = ReportParameters.DeSerialize(rptClass.value);
            else
            {
                parms = new ReportParameters();
                rptClass = new Classes();
            }
            minBalance = parms.MinimumBalance;
            AddChg = parms.AdditionalCharges;
            ByBuilding = parms.ByBuilding;
            Alphabetical = parms.Alphabetical;
            switch (reportKey)
                {
                case (int)ReportKeys.ArrearsReport:
                        this.Title = "Arrears Report";
                        ArrearsReport();
                        break;
                    
                case (int)ReportKeys.BuildingMaster :
                        this.Title = "Building Master List";
                        BuildingMaster();
                        break;

                case (int)ReportKeys.LeaseExpiration:
                        this.Title = "Lease Expiration Report";
                        LeaseExpiration();
                        break;

                case (int)ReportKeys.OwnerMaster:
                        this.Title = "Owner Master List";
                        OwnerMaster();
                        break;

                case (int)ReportKeys.PaymentRegister:
                        this.Title = "Payment Register";
                        PaymentRegister();
                        break;

                case (int)ReportKeys.SelectChargeRpt:
                        this.Title = "Select Charge Report";
                        SelectChargeRpt();
                        break;

                case (int)ReportKeys.TenantMaster:
                        this.Title = "Tenant Master List";
                        TenantMaster();
                        break;

                case (int)ReportKeys.TenantPhoneList:
                        this.Title = "Tenant Telephone List";
                        TenantPhoneList();
                        break;

                case (int)ReportKeys.VacancyList:
                        this.Title = "Vacancy List";
                        VacancyList();
                        break;
                case (int)ReportKeys.PreferredRentWS:
                        this.Title = "Preferred Rent Worksheet";
                        PreferentialWS();
                        break;
                case (int)ReportKeys.PreferredRentRpt:
                        this.Title = "Preferred Rent Report";
                        PreferredReport();
                        break;
                case (int)ReportKeys.RentRollForMortgages:
                        this.Title = "Rent Roll For Mortgages";
                        RentRollForMortgages();
                        break;
                case (int)ReportKeys.AgingReport:
                    this.Title = "Aging Repot";
                    AgingReport();
                    break;
            }

        }

        // Initialization methods
        private void ArrearsReport()
        {
            CreateNumberEditor(pnlTop, "Minimum Balance", "txtMinimum", 10, 2);
            object num = pnlTop.FindName("txtMinimum");
            ((VIBlend.WPF.Controls.NumberEditor)num).Value = minBalance;
            CreateCheckBox(pnlMiddle, "Print Additional Charges", "chkAddChg", System.Windows.FlowDirection.LeftToRight, false);
            object check = pnlMiddle.FindName("chkAddChg");
            ((System.Windows.Controls.CheckBox)check).IsChecked = AddChg;
        }
         
        private void BuildingMaster()
        {
            lblEndBuilding.Visibility = System.Windows.Visibility.Hidden;
            lblStartBuilding.Visibility = System.Windows.Visibility.Hidden;
            txtEndBuilding.Visibility = System.Windows.Visibility.Hidden;
            txtStartBuilding.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LeaseExpiration()
        {
            CreateDatePicker(pnlTop, "From", "dtFrom", DateTime.Now);
            CreateDatePicker(pnlMiddle, "To:", "dtTo", DateTime.Now.AddMonths(3));

        }

        private void OwnerMaster()
        {
            lblEndBuilding.Visibility = System.Windows.Visibility.Hidden;
            lblStartBuilding.Visibility = System.Windows.Visibility.Hidden;
            txtEndBuilding.Visibility = System.Windows.Visibility.Hidden;
            txtStartBuilding.Visibility = System.Windows.Visibility.Hidden;
        }

        private void PaymentRegister()
        {
            DateTime dt = DateTime.Now;
            CreateDatePicker(pnlTop, "From", "dtFrom", new DateTime(dt.Year, dt.Month, 1));
            CreateDatePicker(pnlMiddle, "To  ", "dtTo", DateTime.Now);
        }

        private void SelectChargeRpt()
        {
            System.Windows.Controls.ComboBox box = new System.Windows.Controls.ComboBox();
            box.DisplayMemberPath = "DisplayDescription";
            box.SelectedValuePath = "ChargeCode";

            ChargeCodes[] chgs = ChargeCodes.loads(clientId, true);
            if (chgs != null || chgs.Length == 0)
                box.ItemsSource = chgs;
            box.Width = 120;
            CreateComboBox(pnlTop, box, "Select Charge", "lstCharges");

            CreateCheckBox(pnlMiddle, "Skip page between buildings?","chkSkipPage",System.Windows.FlowDirection.LeftToRight,false);
        }

        private void TenantMaster()
        {
            CreateCheckBox(pnlMiddle, "By Building", "chkBuilding", System.Windows.FlowDirection.LeftToRight, false);
        }

        private void TenantPhoneList()
        {
            CreateCheckBox(pnlTop, "By Building", "chkBuilding", System.Windows.FlowDirection.LeftToRight, ByBuilding);
            CreateCheckBox(pnlMiddle, "Alphabetical", "chkAlpha", System.Windows.FlowDirection.LeftToRight,Alphabetical);
        }

        private void VacancyList()
        {
            // No additional controls needed.
        }
        private void PreferentialWS()
        {
            DateTime dt = DateTime.Now;   // saved date with default of current
            string date = (string)Main.GetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSDate", dt.ToString());
            CreateDatePicker(pnlTop, "Leases Expiring During Month of","dtLease", DateTime.Parse( date));
            string pc = (string)Main.GetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSCode", "");
            CreateTextBox(pnlMiddle, "Prompt Pay Code", "txtCode", 2, pc, 25);
        }
        private void PreferredReport()
        {
            DateTime dt = DateTime.Now;   // saved date with default of current
            string date = (string)Main.GetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSDate", dt.ToString());
            CreateDatePicker(pnlTop, "Leases Expiring During Month of","dtLease", DateTime.Parse( date));
            string pc = (string)Main.GetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSCode", "");
            CreateTextBox(pnlMiddle, "Preferred Code", "txtCode", 2, pc, 25);        
        }
        private void RentRollForMortgages()
        {
            lblEndBuilding.Visibility = Visibility.Collapsed;
            txtEndBuilding.Visibility = Visibility.Collapsed;
            lblStartBuilding.Text = "Building Number";
        }
        private void AgingReport()
        {
            lblStartBuilding.Text = "Building No";
            lblEndBuilding.Visibility = Visibility.Hidden;
            txtEndBuilding.Visibility = Visibility.Hidden;
        }
        // End of Initialization metods

        private void Process_Click(object sender, RoutedEventArgs e)
        {

            this.Cursor = System.Windows.Input.Cursors.Wait; 
            object cls = null;
            object chk = null;
            object chk2 = null;
            object box = null;
            object num = null;
            //object dt1 = null;                             
            //object dt2 = null;
            object txt = null;
            int how = -1;
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)e.Source;
            if (btn.Name.Equals("cmdExit"))
                this.Close();
            if (btn.Name.Equals("cmdDisplay"))
                how = 0;
            else
            {
                how = 1;
                frmPrintDialog pd = new frmPrintDialog();
                pd.DefaultPrinter = parms.DefualtPrinter;
                pd.Owner = this;
                pd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                pd.ShowDialog();
                queue = pd.Queue;
                parms.DefualtPrinter = pd.DefaultPrinter;
            }

            switch (reportKey)
                {

                case (int)ReportKeys.BuildingMaster:
                        cls = (BuildingMaster)new BuildingMaster(how, queue);
                        ((BuildingMaster)cls).Process();
                        break;
                case (int)ReportKeys.ArrearsReport:
                        num = pnlTop.FindName("txtMinimum");
                        minBalance = (decimal)((VIBlend.WPF.Controls.NumberEditor)num).Value;
                        object addChg = pnlMiddle.FindName("chkAddChg");
                        AddChg = (bool)((System.Windows.Controls.CheckBox)addChg).IsChecked.Value;
                        cls = (ArrearsReport)new ArrearsReport(txtStartBuilding.Text, txtEndBuilding.Text, minBalance, how, queue, AddChg);
                        //object[] parmsArrears = {txtStartBuilding.Text, txtEndBuilding.Text, minBalance, how, queue};
                        ((ArrearsReport)cls).Process();
                        //StartReport.Start("MbcsCentral.WinPM.ArrearsReport", parmsArrears);
                        break;

                case (int)ReportKeys.LeaseExpiration:
                        dt1 = (DateTime)((DatePicker)pnlTop.FindName("dtFrom")).SelectedDate;
                        dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 1);
                        dt2 = (DateTime)((DatePicker)pnlMiddle.FindName("dtTo")).SelectedDate;
                        dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 23, 59, 59);
                        cls = (LeaseExpirationReport)new LeaseExpirationReport(txtStartBuilding.Text, txtEndBuilding.Text, dt1, dt2, datReport.SelectedDate, how, queue);
                        ((LeaseExpirationReport)cls).Process();
                        break;

                case (int)ReportKeys.OwnerMaster:
                        cls = (OwnerMaster)new OwnerMaster(how);
                        ((OwnerMaster)cls).Process(queue);
                        break;

                case (int)ReportKeys.PaymentRegister:
                        dt1 = (DateTime)((DatePicker)pnlTop.FindName("dtFrom")).SelectedDate;
                        dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
                        dt2 = (DateTime)((DatePicker)pnlMiddle.FindName("dtTo")).SelectedDate;
                        dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 23, 59, 59);
                        cls = (PaymentRegister)new PaymentRegister((how == 0 ? false :  true), true);
                        ((PaymentRegister)cls).StartBld = txtStartBuilding.Text;
                        ((PaymentRegister)cls).EndBld = txtEndBuilding.Text;
                        ((PaymentRegister)cls).FromDate = dt1;
                        ((PaymentRegister)cls).ToDate = dt2;
                        ((PaymentRegister)cls).print_report(queue);
                        break;

                case (int)ReportKeys.SelectChargeRpt:
                        box = pnlTop.FindName("lstCharges");
                        chk = pnlMiddle.FindName("chkSkipPage");
                        SelectChargeReport rpt = new SelectChargeReport( txtStartBuilding.Text, txtEndBuilding.Text, (string)((System.Windows.Controls.ComboBox)box).SelectedValue, ((System.Windows.Controls.CheckBox)chk).IsChecked.Value, how, queue );
                        rpt.Process();
                        //StartReport.Start("MbcsCentral.WinPM.SelectChargeReport", parmsSelect);
                        //cls = (SelectChargeReport)new SelectChargeReport(txtStartBuilding.Text, txtEndBuilding.Text, (string)((System.Windows.Controls.ComboBox)box).SelectedValue, ((System.Windows.Controls.CheckBox)chk).IsChecked.Value, how);
                        //((SelectChargeReport)cls).Process();
                        break;

                case (int)ReportKeys.TenantMaster:
                        chk = pnlMiddle.FindName("chkBuilding");
                        cls = (TenantMaster)new TenantMaster(txtStartBuilding.Text, txtEndBuilding.Text, ((System.Windows.Controls.CheckBox)chk).IsChecked.Value, how, queue);
                        ((TenantMaster)cls).Process();
                        break;

                case (int)ReportKeys.TenantPhoneList:
                        chk = pnlTop.FindName("chkBuilding");
                        chk2 = pnlMiddle.FindName("chkAlpha");
                        cls = (TenantPhoneList)new TenantPhoneList(txtStartBuilding.Text, txtEndBuilding.Text, ((System.Windows.Controls.CheckBox)chk).IsChecked.Value, ((System.Windows.Controls.CheckBox)chk2).IsChecked.Value, how, queue);
                        ((TenantPhoneList)cls).Process();
                        ByBuilding = ((System.Windows.Controls.CheckBox)chk).IsChecked.Value;
                        Alphabetical = ((System.Windows.Controls.CheckBox)chk2).IsChecked.Value;
                        break;

                case (int)ReportKeys.VacancyList:
                        cls = (VacancyList)new VacancyList(txtStartBuilding.Text, txtEndBuilding.Text, how, queue);
                        ((VacancyList)cls).Process();
                        break;

                case (int)ReportKeys.PreferredRentWS:
                        dt1 = (DateTime)((DatePicker)pnlTop.FindName("dtLease")).SelectedDate;
                        txt = pnlMiddle.FindName("txtCode");
                        Main.SetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSDate", dt1.ToString(), (int)MbcsCentral.Registry.DataTypes.String);
                        Main.SetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSCode", ((TextBox)txt).Text, (int)MbcsCentral.Registry.DataTypes.String);
                        cls = (PreferredRentWS)new PreferredRentWS(txtStartBuilding.Text, txtEndBuilding.Text, dt1, ((TextBox)txt).Text, how, queue);
                        ((PreferredRentWS)cls).Process();
                        break;

                case (int)ReportKeys.PreferredRentRpt:
                        dt1 = (DateTime)((DatePicker)pnlTop.FindName("dtLease")).SelectedDate;
                        txt = pnlMiddle.FindName("txtCode");
                        Main.SetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSDate", dt1.ToString(), (int)MbcsCentral.Registry.DataTypes.String);
                        Main.SetRegistryEntry(GlobalShared.APP_MAIN_REG_KEY + "Parameters", "PreferentialWSCode", ((TextBox)txt).Text, (int)MbcsCentral.Registry.DataTypes.String);
                        cls = (PreferredReport)new PreferredReport(txtStartBuilding.Text, txtEndBuilding.Text, dt1, ((TextBox)txt).Text, how, queue);
                        ((PreferredReport)cls).Process();
                        break;

                case (int)ReportKeys.RentRollForMortgages:
                    cls = (RentRollForMortgages)new WinPM.RentRollForMortgages(txtStartBuilding.Text, datReport.SelectedDate.Value);
                    ((RentRollForMortgages)cls).Process();
                    break;
                case (int)ReportKeys.AgingReport:
                    cls = (AgingReport)new WinPM.AgingReport(txtStartBuilding.Text, how, queue);
                    ((AgingReport)cls).Process();
                    break;
            }

            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void CreateTextBox(StackPanel pnl, string label, string name, int maxLength, string defaultValue = "", int defaultWidth = 25)
        {
            TextBlock txt = new TextBlock();
            txt.Text = label;
            txt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            pnl.Children.Add(txt);
            TextBox txtBox = new TextBox();
            txtBox.Name = name;
            txtBox.Text = defaultValue;
            txtBox.MaxLength = maxLength;
            txtBox.Width = defaultWidth;
            txtBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            txtBox.Margin = new Thickness(10,0,10,0);
            txtBox.CharacterCasing = CharacterCasing.Upper;
            txtBox.GotFocus += FocusObtained;
            pnl.Children.Add(txtBox);
            pnl.RegisterName(name, txtBox);
        }

        private void CreateDatePicker(StackPanel pnl, string label, string name, DateTime date)
        {
            TextBlock txt = new TextBlock();
            txt.Text = label;
            txt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            pnl.Children.Add(txt);
            DatePicker dt = new DatePicker();
            dt.Name=name;
            dt.SelectedDate = date;
            dt.Text = date.ToString("MM/dd/yyyy");
            dt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            dt.Margin = new Thickness(10, 0, 10, 0);
            pnl.Children.Add(dt);
            pnl.RegisterName(name, dt);
        }

        private void CreateComboBox(StackPanel pnl, System.Windows.Controls.ComboBox box, string label, string name)
        {
            TextBlock txt = new TextBlock();
            txt.Text = label;
            txt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            pnl.Children.Add(txt);
            box.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            box.Margin = new Thickness(10, 0, 10, 0);
            pnl.Children.Add(box);
            pnl.RegisterName(name, box);
        }

        private void CreateCheckBox(StackPanel pnl, string label, string name, FlowDirection flow, bool isChecked)
        {
            System.Windows.Controls.CheckBox chk = new System.Windows.Controls.CheckBox();
            chk.Name = name;
            chk.Content = label;
            chk.FlowDirection = flow;
            chk.IsChecked = isChecked;
            chk.Margin = new Thickness(10,0,10,0);
            pnl.Children.Add(chk);
            pnl.RegisterName(name, chk);
         }

        private void CreateNumberEditor(StackPanel pnl, string label, string name, int maxlength, int decimals)
        {
            TextBlock txt = new TextBlock();
            txt.Text = label;
            txt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            pnl.Children.Add(txt);
            VIBlend.WPF.Controls.NumberEditor num = new VIBlend.WPF.Controls.NumberEditor();
            num.Name = name;
            num.MaxLength = maxlength;
            num.DecimalPlaces = decimals;
            num.Width = 90;
            num.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            num.Margin = new Thickness(10, 0, 10, 0);
            num.GotFocus += FocusObtained;
            pnl.Children.Add(num);
            pnl.RegisterName(name, num);
        }

        private void FocusObtained(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                txt.SelectAll();
            }
            catch { }
        }

        private void formReportSelections_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            parms.MinimumBalance = minBalance;
            parms.AdditionalCharges = AddChg;
            parms.ByBuilding = ByBuilding;
            parms.Alphabetical = Alphabetical;
            rptClass.value = parms.SerializeObject();
            rptClass.clientId = clientId;
            rptClass.acctId = acctId;
            rptClass.name = className;
            rptClass.save();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

    public class ReportParameters : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ReportParameters()
        { }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static ReportParameters DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReportParameters));
            StringReader rdr = new StringReader(inputString);
            ReportParameters parms = (ReportParameters)serializer.Deserialize(rdr);
            return parms;
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            IsChanged = true;
        }

        public decimal MinimumBalance { get; set; }

        public string DefualtPrinter { get; set; }

        public bool AdditionalCharges { get; set; }

        public bool ByBuilding { get; set; }

        public bool Alphabetical { get; set; }

        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged { get; set; }
    }
}
