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
using MbcsCentral.WinPM;

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmNoticesEmail.xaml
    /// </summary>
    public partial class frmVendorsEmail : Window
    {
        string ClassName;
        Classes cls;
        Classes[] documentClasses;
        VendorsEmailParms parms = null;
        VendorsEmailParms[] documentParms = null;
        int clientId = (int)GlobalShared.user.Client.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        bool cancel = true;
        const string MESSAGE_PREFIX = "DocumentMessages:";

        public bool ACH { get; set; } = true;

        string thisApp;

        public frmVendorsEmail(string app)
        {
            thisApp = app;
            InitializeComponent();
        }

        public frmVendorsEmail()
        {
            InitializeComponent();
        }

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            cancel = false;
            Main.saveForm(this);
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            invoices = null;
            this.Close();
        }

        private void formVendorsEmail_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            invoices = Invoices.LoadForChecks(ACH);
            lstVendors.ItemsSource = invoices;
            /*
            if (LateNotices)
            {
                pnlSavedMessages.Visibility = Visibility.Collapsed;
                ClassName = this.GetType().Name;
                cls = Classes.loadByClient(clientId, ClassName);
                if (cls != null)
                    parms = VendorsEmailParms.DeSerialize(cls.value);
                else
                    cls = new Classes();
            }
            else
            */
            if (parms == null)
                parms = new VendorsEmailParms();
            if (!LateNotices)
            {
                ClassName = MESSAGE_PREFIX;
                documentClasses = Classes.loadMultiple(ClassName, (int)acctId);
                lstMessages.Items.Clear();
                lstMessages.Items.Add("");
                if (documentClasses != null && documentClasses.Length > 0 && pnlSavedMessages.IsEnabled)
                {
                    if (documentParms == null)
                        documentParms = new VendorsEmailParms[documentClasses.Length];
                    else
                        Array.Resize<VendorsEmailParms>(ref documentParms, documentClasses.Length);
                    //for (int i = 0; i < documentClasses.Length; i++)
                    for (int i = 0; i < documentClasses.Length; i++)
                    {
                        documentParms[i] = VendorsEmailParms.DeSerialize(documentClasses[i].value);
                        try
                        {
                            lstMessages.Items.Add(documentClasses[i]);
                        } catch (Exception ex) { }
                    }
                }
                parms.DisplayName = this.displayname;
                this.Title = "Tenant Document Email List";
                double emailCount = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, 0);
                double emailDelay = (double)Main.GetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, 0);
                txtLimit.Text = emailCount.ToString();
                txtDelay.Text = emailDelay.ToString();
            }
            EmailParms.DataContext = parms;
            lstInsertionFields.ItemsSource = WinpmGlobal.NoticeInsertionFieldNames;
        }

        private void formVendorsEmail_Closed(object sender, EventArgs e)
        {
            if (cancel)
                invoices = null;
            if (LateNotices)
            {
                cls.value = parms.SerializeObject();
                cls.clientId = clientId;
                cls.acctId = acctId;
                cls.name = ClassName;
                cls.save();
            }
            else
            {
                double result = 0;
                double.TryParse(txtLimit.Text, out result);
                Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_COUNT, result, (int)Registry.DataTypes.Dword);
                this.BatchLimit = result;
                result = 0;
                double.TryParse(txtDelay.Text, out result);
                Main.SetRegistryEntry(WinpmGlobal.EMAIL_BATCH_LIMIT, WinpmGlobal.EMAIL_BATCH_LIMIT_DELAY, result, (int)Registry.DataTypes.Dword);
                this.BatchDelay = result;
            }

        }

        public bool LateNotices { get; set; } = true;
        public Invoices[] invoices { get; set; }
        public string EmailSubject { get { return parms.EmailSubject; } }
        public string Message { get { return parms.Message; } }
        public bool ReturnReceipt { get { return parms.ReturnReceipt; } }
        public string DisplayName { get { return parms.DisplayName; }  }
        public bool AlsoPrint { get { return parms.AlsoPrint; }  }
        public bool Test { get; set; }

        // NOT Late Notices
        public string displayname { get; set; }
        public string MessageName { get; set; }
        public bool SendCopy { get; set; }
        public double BatchLimit { get; set; }
        public double BatchDelay { get; set; }

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
                    documentParms = new VendorsEmailParms[1];
                else
                    Array.Resize<VendorsEmailParms>(ref documentParms, documentParms.Length + 1);
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
                    parms = new VendorsEmailParms();
                    EmailParms.DataContext = parms;
                }
                else
                {
                    Classes cls = (Classes)lstMessages.SelectedItem;
                    parms = VendorsEmailParms.DeSerialize(cls.value);
                    EmailParms.DataContext = parms;
                }
            }
        }

        private void ChkSendToMe_Click(object sender, RoutedEventArgs e)
        {
            SendCopy = chkSendToMe.IsChecked.Value;
        }
    }

    [Serializable]
    public class VendorsEmailParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VendorsEmailParms()
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
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error saving  VendorsEmailParms: " + ex.Message, true);
            }
        }

        public static VendorsEmailParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(VendorsEmailParms));
            StringReader rdr = new StringReader(inputString);
            VendorsEmailParms parms = (VendorsEmailParms)serializer.Deserialize(rdr);
            return parms;
        }
    }
}
