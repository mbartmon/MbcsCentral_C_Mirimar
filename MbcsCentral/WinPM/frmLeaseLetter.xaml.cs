using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
//using System.Printing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using MbcsUtils;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmLeaseLetter.xaml
    /// </summary>
    public partial class frmLeaseLetter : Window
    {
        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        LeaseLetterParms parms = null;
        string className = ""; //"LeaseLetterParms";
        Dictionary<string, string> configs = new Dictionary<string, string>();
        DictUtils config;
        Control cont;
        bool OneFlag = false;
        PrinterQueues Printers;
        System.Printing.PrintQueue SelectedPrinter = null;
        System.Printing.PrintCapabilities printerCapabilities;       
        bool CanDuplex = false;
        bool doDuplex = false;
        Classes cls;

        bool CurrentlyPrinting = false;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmLeaseLetter()
        {
            InitializeComponent();
            Expander.Visibility = System.Windows.Visibility.Collapsed;
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = LeaseLetterParms.DeSerializeObject(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new LeaseLetterParms();

            Main.getForm(this);
            this.Width = colMain.Width.Value + colAction.Width.Value + 20;
            MainGrid.DataContext = parms;
            Printers = new PrinterQueues();
            PrinterList.ItemsSource =  Printers.Printers;
         
            for (int i = 0; i < PrinterList.Items.Count; i++)
            {
                if (PrinterList.Items[i].ToString().Equals(parms.PrinterQueue))
                {
                    PrinterList.SelectedIndex = i;
                    SelectedPrinter = Printers.Queue(i);
                    GetPrinterCapabilities(SelectedPrinter);
                    break;
                }
            }
        }
        public frmLeaseLetter(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            Expander.Visibility = System.Windows.Visibility.Collapsed;
            className = this.GetType().Name;
            cls = Classes.load(acctId, className);
            if (cls != null)
                parms = LeaseLetterParms.DeSerializeObject(cls.value);
            else
                cls = new Classes();
            if (parms == null)
                parms = new LeaseLetterParms();

            Main.getForm(this);
            this.Width = colMain.Width.Value + colAction.Width.Value + 20;
            MainGrid.DataContext = parms;
            Printers = new PrinterQueues();
            PrinterList.ItemsSource = Printers.Printers;

            for (int i = 0; i < PrinterList.Items.Count; i++)
            {
                if (PrinterList.Items[i].ToString().Equals(parms.PrinterQueue))
                {
                    PrinterList.SelectedIndex = i;
                    SelectedPrinter = Printers.Queue(i);
                    GetPrinterCapabilities(SelectedPrinter);
                    break;
                }
            }
            doDuplex = !parms.Duplex;
            cmdDuplex_Click(new object(), new RoutedEventArgs());
        }
        private void GetPrinterCapabilities(System.Printing.PrintQueue printer)
        {
            // Make Duplex button always enabled to account for printers that do not report duplex capability properly
            CanDuplex = true;
            cmdDuplex.IsEnabled = true;
            return;

            /*
            printerCapabilities = printer.GetPrintCapabilities();
            if (printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedLongEdge) || printerCapabilities.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedShortEdge))
            {
                CanDuplex = true;
                cmdDuplex.IsEnabled = true;
            }
            else
            {
                CanDuplex = false;
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;
                cmdDuplex.IsEnabled = false;
            }
            */
        }

        private void txtTenantNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmdTenants_Click(object sender, RoutedEventArgs e)
        {
            if (Expander.Visibility == System.Windows.Visibility.Collapsed)
            {
                Expander.Visibility = System.Windows.Visibility.Visible;
                cmdTenants.Content = "<<TENANTS";
                this.Width = colMain.Width.Value + colAction.Width.Value + colTenant.Width.Value + 20;
                cmdAdd.IsEnabled = true;
                OneFlag = true;
                txtTenantNumber.Focus();
            }
            else
            {
                Expander.Visibility = System.Windows.Visibility.Collapsed;
                cmdTenants.Content="TENANTS>>";
                this.Width = colMain.Width.Value + colAction.Width.Value + 20;
                cmdAdd.IsEnabled = false;
                OneFlag = false;
            }

        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void formLeaseLetter_Loaded(object sender, RoutedEventArgs e)
        {
            cont = Control.load(clientId);
            DateTime dt = System.DateTime.Now;
            dtLetter.SelectedDate = dt;
            dtLetter.DisplayDate = dt;
            dtFrom.SelectedDate = new DateTime(dt.Year,dt.Month,1).AddMonths(1).AddDays(-1);
            dtFrom.DisplayDate = dtFrom.SelectedDate.Value;
            dt = new DateTime(dt.Year, dt.Month, 1).AddMonths(4);
            dtTo.SelectedDate = dt.AddDays(-1);
            dtTo.DisplayDate = dtTo.SelectedDate.Value;
            Types[] TypeDesc = Types.loadAll(clientId, true);
            lstType.ItemsSource = TypeDesc;
            pnlPercents.DataContext = cont;
            pnlCharges.DataContext = cont;
            pnlMinimumRents.DataContext = cont;
            pnlMinimumYears.DataContext = cont;
            txtSeniorCharge.DataContext = cont;
            if (doDuplex)
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Red;
            else
                cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;

            DocumentLayoutList[] docs = DocumentLayoutList.LoadByCategory("1", true);
            string name = "";
            lstLeases.ItemsSource = docs;
            for (int i = 0; i < lstLeases.Items.Count; i++ )
            {
                name = ((DocumentLayoutList)lstLeases.Items[i]).DocumentName;
                if (name.Equals(parms.LeaseDocument))
                {
                    lstLeases.SelectedIndex = i;
                    break;
                }
            }
            lstReverse.ItemsSource = docs;
            for (int i = 0; i < lstReverse.Items.Count; i++ )
            {
                name = ((DocumentLayoutList)lstReverse.Items[i]).DocumentName;
                if (name.Equals(parms.LeaseReverse))
                {
                    lstReverse.SelectedIndex = i;
                    break;
                }
            }
            txtStartBuilding.Focus();
        }

        private void formLeaseLetter_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            cmdSave_Click(sender, new RoutedEventArgs());
        }

        private void cmdDuplex_Click(object sender, RoutedEventArgs e)
        {
            if (CanDuplex)
            {
                doDuplex = !doDuplex;
                if (doDuplex)
                    cmdDuplex.Foreground = System.Windows.Media.Brushes.Red;
                else
                    cmdDuplex.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                return;
            }
            if (lstLeases.SelectedIndex > -1)
                parms.LeaseDocument = ((DocumentLayoutList)lstLeases.SelectedItem).DocumentName;
            if (lstReverse.SelectedIndex > -1)
                parms.LeaseReverse = ((DocumentLayoutList)lstReverse.SelectedItem).DocumentName;
            if (PrinterList.SelectedIndex > -1)
                parms.PrinterQueue = PrinterList.SelectedItem.ToString();
            parms.Duplex = doDuplex;
            cls.value = parms.SerializeObject();
            cls.clientId = clientId;
            cls.acctId = acctId;
            cls.name = className;
            cls.save();
            cont.save();
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentlyPrinting)
            {
                StackTrace trace = new StackTrace();

                if (MessageBoxCustom.Show("A batch of letters is printing (called from: " + trace.GetFrame(1).GetMethod().Name + ".\r\n Do you want to start another batch now?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            CurrentlyPrinting = true;
            if (PrinterList.SelectedIndex < 0)
            {
                MessageBox.Show("You have not selected a printer.");
                return;
            }
            Status status = new Status();
            txtStatus.DataContext = status;
            BindingExpression be = txtStatus.GetBindingExpression(TextBlock.TextProperty);

            status.Text = "Preparing to Print";
            be.UpdateTarget();
            ListItem queue = (ListItem)PrinterList.SelectedItem;
            string[] codes = new string[10];
            codes[0] = txtChg0.Text;
            codes[1] = txtChg1.Text;
            codes[2] = txtChg2.Text;
            codes[3] = txtChg3.Text;
            codes[4] = txtChg4.Text;
            codes[5] = txtChg5.Text;
            codes[6] = txtChg6.Text;
            codes[7] = txtChg7.Text;
            codes[8] = txtChg8.Text;
            codes[9] = txtChg9.Text;
            LeaseLetter rpt = new LeaseLetter(GlobalShared.user.hasModifyPermission(m_ThisApp));
            rpt.Queue = Printers.Queue(queue.ValueItem);
            if (lstLeases.SelectedItem != null)
                rpt.LeaseDocument = ((DocumentLayoutList)lstLeases.SelectedItem).DocumentName;
            if (lstReverse.SelectedItem != null)
                rpt.ReverseDocument = ((DocumentLayoutList)lstReverse.SelectedItem).DocumentName;
            rpt.Codes = codes;
            rpt.LetterDate = dtLetter.SelectedDate.Value;
            rpt.BldStart = txtStartBuilding.Text;
            rpt.BldEnd = txtEndBuilding.Text;
            rpt.ExpFrom = dtFrom.SelectedDate.Value;
            rpt.ExpTo = dtTo.SelectedDate.Value;
            rpt.OneYear = decimal.Parse(txtOneYear.Text);
            rpt.TwoYear = decimal.Parse(txtTwoYear.Text);
            rpt.PrefRent = (bool)chkPref.IsChecked.Value;
            rpt.PromptPay = (bool)chkPrompt.IsChecked.Value;
            rpt.BypassHistory = chkHistory.IsChecked.Value;
            rpt.IndividualFlag = OneFlag;
            rpt.SeniorCitizenCode = txtSeniorCharge.Text;
            rpt.GuidelinesUnknown = chkGuidlines.IsChecked.Value;
            rpt.UseTypeLease = chkUseTypeLeases.IsChecked.Value;
            rpt.DuplexEnabled = (bool)(CanDuplex & doDuplex);
            int copies = int.Parse(txtCopies.Text);
            rpt.Copies = (copies == 0 ? 1 : copies);
            
            if (lstLeases.SelectedIndex > -1)
            {
                string filename = ((DocumentLayoutList)lstLeases.SelectedItem).DocumentName;
                rpt.LeaseDocument = filename;
            }
            else
            {
                MessageBox.Show("A Default Lease must be selected.");
                return;
            }
            if (lstReverse.SelectedIndex > -1)
            {
                string filename = ((DocumentLayoutList)lstReverse.SelectedItem).DocumentLocation;
                if (filename != null)
                { 
                    if (filename.StartsWith("*"))
                    {
                        string path = GlobalShared.appPath;
                        if (path.EndsWith("\\"))
                            path = path.Substring(0, path.Length - 1);
                        filename = filename.Replace("*", path + "\\Files");
                    }
                    rpt.ReverseDocument = filename;
                }
            }
            //Types t = null;
            if (lstType.SelectedItems.Count > 0)
            {
                string[] t = new string[lstType.SelectedItems.Count];
                for (int i = 0; i < lstType.SelectedItems.Count; i++)                
                    t[i] = ((Types)lstType.SelectedItems[i]).Type;
                rpt.TypeList = t;
            }
            if (OneFlag)
            {
                if (lstTenants.Items.Count == 0)
                {
                    MessageBox.Show("You have chosen to print individual tenant Lease Renewals but no Tenant Numbers have been entered.");
                    return;
                }

                string[] s = new string[lstTenants.Items.Count];
                for (int i = 0; i < lstTenants.Items.Count; i++)
                    s[i] = (string)lstTenants.Items[i];
                rpt.TenantKeys = s;
            }  
            rpt.UseStatementOptions = chkStatementOption.IsChecked.Value;
            status.Text = "Printing Lease Renewal Letters";
            rpt.PrintLetters();
            status.Text = "DONE";
            CurrentlyPrinting = false;
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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenantNumber.Text.Trim() != "")
                lstTenants.Items.Add(txtTenantNumber.Text.ToUpper());
            txtTenantNumber.Text = "";
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstTenants.SelectedIndex > -1)
                lstTenants.Items.RemoveAt(lstTenants.SelectedIndex);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            lstTenants.Items.Clear();
        }

        private void PrinterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPrinter = Printers.Queue(PrinterList.SelectedIndex);
            GetPrinterCapabilities(SelectedPrinter);
        }

        // TODO: add setting of property MinimumRent
    }

    public static class ExtensionHelpers
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            if (toSerialize != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
                StringWriter textWriter = new StringWriter();

                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
            else return "";

        }

        public static LeaseLetterParms DeSerializeObject(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LeaseLetterParms));
            StringReader rdr = new StringReader(inputString);
            LeaseLetterParms resultingMessage = (LeaseLetterParms)serializer.Deserialize(rdr);
            return resultingMessage;
        }
    }
    public class Status : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string status = "";

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        public string Text
        {
            get { return status; }
            set { status = value;
            OnPropertyChanged("Text");
            }
        }
    }
    [Serializable()]
    public class LeaseLetterParms
    {
        DateTime m_FromDate;
        DateTime m_ToDate;
        bool m_GuideLinesUnkown;
        bool m_UseMinimumIncrease;
        decimal m_MinimumOne;
        decimal m_MinimumTwo;
        int m_MaximumYears;
        decimal m_MaximumRent;
        int m_Copies;
        bool m_StatementOption;
        bool m_PrintPref;
        bool m_PrintPrompt;
        string m_PrinterQueue;
        bool m_UseThisLease;
        string m_LeaseDocument;
        string m_LeaseReverse;
        bool m_Duplex;

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static LeaseLetterParms DeSerializeObject(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LeaseLetterParms));
            StringReader rdr = new StringReader(inputString);
            LeaseLetterParms parms = (LeaseLetterParms)serializer.Deserialize(rdr);
            return parms;
        }

        public void Serialize(string filename)
        {
            try
            {
                Stream stream = File.Open(filename, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Close();
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving Lease Letter parms: " + ex.Message, true);
            }
        }

        public static LeaseLetterParms DeSerialize(string filename)
        {
            try
            {
                Stream stream = File.Open(filename, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = formatter.Deserialize(stream);
                stream.Close();
                return (LeaseLetterParms)obj;
            }
            catch (IOException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error retrieving Lease Letter parms: " + ex.Message,true);
                return null;
            }
        }

        public DateTime FromDate
        {
            get { return m_FromDate; }
            set { m_FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return m_ToDate; }
            set { m_ToDate = value; }
        }
        public bool GuideLinesUnkown
        {
            get { return m_GuideLinesUnkown; }
            set { m_GuideLinesUnkown = value; }
        }
        public bool UseMinimumIncrease
        {
            get { return m_UseMinimumIncrease;}
            set { m_UseMinimumIncrease = value; }
        }
        public decimal MinimumOne
        {
            get { return m_MinimumOne;}
            set { m_MinimumOne = value; }
        }
        public decimal MinimumTwo
        {
            get { return m_MinimumTwo; }
            set { m_MinimumTwo = value; }
        }
        public int MaximumYears
        {
            get { return m_MaximumYears; }
            set { m_MaximumYears = value; }
        }
        public decimal MaximumRent
        {
            get { return m_MaximumRent; }
            set { m_MaximumRent = value; }
        }
        public int Copies
        {
            get { return m_Copies;}
            set { m_Copies = value; }
        }
        public bool StatementOption
        {
            get { return m_StatementOption; }
            set { m_StatementOption = value; }
        }
        public bool PrintPref
        {
            get { return m_PrintPref; }
            set { m_PrintPref = value; }
        }
        public bool PrintPrompt
        {
            get { return m_PrintPrompt; }
            set { m_PrintPrompt = value; }
        }
        public string PrinterQueue
        {
            get { return m_PrinterQueue; }
            set { m_PrinterQueue = value; }
        }

        public bool UseThisLease
        {
            get { return m_UseThisLease; }
            set { m_UseThisLease = value; }
        }

        public string LeaseDocument
        {
            get { return m_LeaseDocument; }
            set { m_LeaseDocument = value; }
        }

        public string LeaseReverse
        {
            get { return m_LeaseReverse; }
            set { m_LeaseReverse = value; }
        }

        public bool Duplex
        {
            get { return m_Duplex; }
            set { m_Duplex = value; }
        }
    }
}
