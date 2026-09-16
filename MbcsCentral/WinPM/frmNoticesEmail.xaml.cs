using MbcsMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmNoticesEmail.xaml
    /// </summary>
    public partial class frmNoticesEmail : Window
    {
        string ClassName;
        Classes cls;
        Classes[] documentClasses;
        string TenantClass = "";
        LateNoticeEmailParms parms = null;
        LateNoticeEmailParms[] documentParms = null;
        int clientId = (int)GlobalShared.user.Client.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        bool cancel = true;
        const string MESSAGE_PREFIX = "DocumentMessages:";

        public bool rentNotices { get; set; } = false;

        string thisApp;

        public frmNoticesEmail(string app)
        {
            thisApp = app;
            InitializeComponent();
            if (rentNotices)
                this.Title = "Rent Notices Email List";
        }

        public frmNoticesEmail()
        {
            InitializeComponent();
        }

        private void  cmdSend_Click(object sender, RoutedEventArgs e)
        {
            string address = lstReplyTo.Text.Trim();
            if (!chkReplyTo.IsChecked.Value)
                address = "";
            
            if (!address.Equals("") && !TestEmail.IsEmail(address))
            {
                MessageBoxCustom.Show("Invalid Reply-To Email Address.");
                return;
            }
            ReturnAddress = "<" + address + ">";

            cancel = false;
            Main.saveForm(this);
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            tenants = null;
            this.Close();
        }

        private void formNoticesEmail_Loaded(object sender, RoutedEventArgs e)
        {
            if (tenants == null)
            {
                MessageBox.Show("No Tenant names were passed.");
                this.Close();
            }
            Main.getForm(this);
            if (TenantEmailStmts)
                this.Title = "Rent Statements Email List";

            this.Title += "   <" + EmailAccount + ">";

            lstTenants.ItemsSource = tenants;

            if (LateNotices)
            {
                pnlSavedMessages.Visibility = Visibility.Collapsed;
                ClassName = this.GetType().Name;
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = LateNoticeEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes() { name = ClassName };
            }
            else if (TenantACH)
            {
                pnlSavedMessages.Visibility = Visibility.Collapsed;
                ClassName = this.GetType().Name + "_AchTenants";
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = LateNoticeEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes() { name = ClassName };
            }
            else if (TenantEmailStmts)
            {
                pnlSavedMessages.Visibility = Visibility.Collapsed;
                ClassName = this.GetType().Name + "_EmailStmts";
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = LateNoticeEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes() { name = ClassName };

                //
                //
                foreach (Tenants ten in tenants)
                {
                    if (!ten.Email1.Trim().Equals("") || !ten.Email2.Trim().Equals(""))
                    {
                        //if (String.Compare(ten.BuildingNumber.Trim(), txtBldFrom.Text.Trim()) >= 0 && String.Compare(ten.BuildingNumber.Trim(), txtBldTo.Text.Trim()) <= 0)
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
                //
                //
            }
            else
            {
                ClassName = this.GetType().Name + "_Tenants";
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = LateNoticeEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes() { name = ClassName };
            }

            if (parms == null)
                parms = new LateNoticeEmailParms();

            if (!LateNotices && !TenantACH && !TenantEmailStmts)
            {
                ClassName = MESSAGE_PREFIX;
                documentClasses = Classes.loadMultiple(MESSAGE_PREFIX, (int)acctId);
                lstMessages.Items.Clear();
                lstMessages.Items.Add("");
                if (documentClasses != null && documentClasses.Length > 0 && pnlSavedMessages.IsEnabled)
                {
                    if (documentParms == null)
                        documentParms = new LateNoticeEmailParms[documentClasses.Length];
                    else
                        Array.Resize<LateNoticeEmailParms>(ref documentParms, documentClasses.Length);
                    for (int i = 0; i < documentClasses.Length; i++)
                    {
                        documentParms[i] = LateNoticeEmailParms.DeSerialize(documentClasses[i].value);
                        try
                        {
                            lstMessages.Items.Add(documentClasses[i]);
                        }
                        catch (Exception ex) { }
                    }
                }
                this.Title = "Tenant Document Email List" + "   <" + EmailAccount + ">";
             }
            double emailCount = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, 0);
            double emailDelay = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, 0);
            txtLimit.Text = emailCount.ToString();
            txtDelay.Text = emailDelay.ToString();
            parms.DisplayName = this.displayname;
            EmailParms.DataContext = parms;
            lstInsertionFields.ItemsSource = WinpmGlobal.NoticeInsertionFieldNames;
        }

        private void formNoticesEmail_Closed(object sender, EventArgs e)
        {
            if (cancel)
                tenants = null;

            string address = lstReplyTo.Text;
            bool found = false;
            foreach (string s in lstReplyTo.Items)
            {
                if (s.Equals(address))
                    found = true;
            }
            if (!found)
            {
                if (lstReplyTo.SelectedIndex == -1)
                {
                    if (parms.ReturnAddresses == null)
                        parms.ReturnAddresses = new string[0];
                    string[] addresses = parms.ReturnAddresses;
                    Array.Resize(ref addresses, parms.ReturnAddresses.Length + 1);
                    addresses[addresses.Length - 1] = address;
                    parms.ReturnAddresses = addresses;
                }
            }
            cls.value = parms.SerializeObject();
            cls.clientId = clientId;
            cls.acctId = acctId;
            //cls.name = ClassName;
            cls.save();
            double result = 0;
            double.TryParse(txtLimit.Text, out result);
            Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, result, (int)Registry.DataTypes.Dword);
            this.BatchLimit = result;
            result = 0;
            double.TryParse(txtDelay.Text, out result);
            Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, result, (int)Registry.DataTypes.Dword);
            this.BatchDelay = result;
            

        }

        public bool LateNotices { get; set; } = false;
        public Tenants[] tenants { get; set; }
        public string EmailAccount { get; set; }

        public string EmailSubject { get { return parms.EmailSubject; } }
        public string Message { get { return parms.Message; } }
        public bool ReturnReceipt { get { return parms.ReturnReceipt; } }
        public string DisplayName { get { return parms.DisplayName; }  }
        public bool AlsoPrint { get { return parms.AlsoPrint; }  }
        public string ReturnAddress { get; set; }
        public bool Test { get; set; }


        // NOT Late Notices
        public bool TenantACH { get; set; } = false;
        public string displayname { get; set; }
        public string MessageName { get; set; }
        public bool SendCopy { get; set; }
        public double BatchLimit { get; set; }
        public double BatchDelay { get; set; }
        public bool TenantEmailStmts { get; set; } = false;

        private void cmdTest_Click(object sender, RoutedEventArgs e)
        {
            this.Test = true;
            cmdSend_Click(sender, e);
        }

        private void CmdSave_Click(object sender, RoutedEventArgs e)
        {
            int i = lstMessages.SelectedIndex;
            if (i > 0)
            {
                documentClasses[i - 1].value = parms.SerializeObject();
                documentClasses[i - 1].save();
            }
            else
            {
                string name = "";
                MessageBoxCustom.Show("New Message Name: ", out name, 32, null, this);
                parms.MessageName = name;
                cls = new Classes();
                cls.clientId = clientId;
                cls.acctId = acctId;
                cls.name = MESSAGE_PREFIX + name;
                cls.value = parms.SerializeObject();
                cls.save();

                if (documentClasses == null)
                    documentClasses = new Classes[1];
                else
                    Array.Resize<Classes>(ref documentClasses, documentClasses.Length + 1);
                documentClasses[documentClasses.Length - 1] = cls;
                if (documentParms == null)
                    documentParms = new LateNoticeEmailParms[1];
                else
                    Array.Resize<LateNoticeEmailParms>(ref documentParms, documentParms.Length + 1);
                documentParms[documentParms.Length - 1] = parms;
                lstMessages.Items.Add(parms.MessageName);
            }
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = lstMessages.SelectedIndex;
            if (i < 1)
            {
                MessageBoxCustom.Show("No message has been selected.");
                return;
            }
            if (MessageBoxCustom.Show("Delete current message?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                return;


        }

        private void LstMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = lstMessages.SelectedIndex;
            if (i >= 0)
            {
                if (i == 0)
                {
                    parms = new LateNoticeEmailParms();
                    EmailParms.DataContext = parms;
                }
                else
                {
                    Classes cls = (Classes)lstMessages.SelectedItem;
                    parms = LateNoticeEmailParms.DeSerialize(cls.value);
                    EmailParms.DataContext = parms;
                }
            }
        }

        private void ChkSendToMe_Click(object sender, RoutedEventArgs e)
        {
            SendCopy = chkSendToMe.IsChecked.Value;
        }

        private void chkSendAll_Checked(object sender, RoutedEventArgs e)
        {
            if (tenants == null)
                return;
            bool check = false;
            if (chkSendAll.IsChecked.Value)
                check = true;
            foreach (Tenants ten in tenants)
                ten.SendEmail = check;

        }

        private void chkEmail1All_Checked(object sender, RoutedEventArgs e)
        {
            if (tenants == null)
                return;
            bool check = false;
            if (chkEmail1All.IsChecked.Value)
                check = true;
            foreach (Tenants ten in tenants)
                if (ten.IsEmail1)
                    ten.UseEmail1 = check;

        }

        private void chkEmail2All_Checked(object sender, RoutedEventArgs e)
        {
            if (tenants == null)
                return;
            bool check = false;
            if (chkEmail2All.IsChecked.Value)
                check = true;
            foreach (Tenants ten in tenants)
                if (ten.IsEmail2)
                    ten.UseEmail2 = check;

        }
    }

    [Serializable]
    public class LateNoticeEmailParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LateNoticeEmailParms()
        { }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool alsoPrint = false;
        public string EmailSubject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string DisplayName { get; set; }
        public string[] ReturnAddresses { get; set; }   
        public bool AlsoPrint
                    {
                        get { return alsoPrint; }
                        set { alsoPrint = value; OnPropertyChanged("AlsoPrint"); }
                    }

        public string MessageName { get; set; }
        public bool SendCopy { get; set; }
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
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving  LateNoticeEmailParms: " + ex.Message, true);
            }
        }

        public static LateNoticeEmailParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LateNoticeEmailParms));
            StringReader rdr = new StringReader(inputString);
            LateNoticeEmailParms parms = (LateNoticeEmailParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    /*
    [Serializable]
    public class TenantAchNoticeEmailParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TenantAchNoticeEmailParms()
        { }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool alsoPrint = false;
        public string EmailSubject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string DisplayName { get; set; }
        public bool AlsoPrint
        {
            get { return alsoPrint; }
            set { alsoPrint = value; OnPropertyChanged("AlsoPrint"); }
        }

        public string MessageName { get; set; }
        public bool SendCopy { get; set; }

        public string[] ReturnAddresses { get; set; }
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
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving  TenantAchNoticeEmailParms: " + ex.Message, true);
            }
        }

        public static TenantAchNoticeEmailParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TenantAchNoticeEmailParms));
            StringReader rdr = new StringReader(inputString);
            TenantAchNoticeEmailParms parms = (TenantAchNoticeEmailParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    [Serializable]
    public class TenantNoticeEmailParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TenantNoticeEmailParms()
        { }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool alsoPrint = false;
        public string EmailSubject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string DisplayName { get; set; }
        public bool AlsoPrint
        {
            get { return alsoPrint; }
            set { alsoPrint = value; OnPropertyChanged("AlsoPrint"); }
        }

        public string MessageName { get; set; }
        public bool SendCopy { get; set; }

        public string[] ReturnAddresses { get; set; }
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
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving  TenantNoticeEmailParms: " + ex.Message, true);
            }
        }

        public static TenantNoticeEmailParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TenantNoticeEmailParms));
            StringReader rdr = new StringReader(inputString);
            TenantNoticeEmailParms parms = (TenantNoticeEmailParms)serializer.Deserialize(rdr);
            return parms;
        }
    }
    */
}
