using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Controls;
using Email.Net.Common;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmNotices.xaml
    /// </summary>
    public partial class frmNotices : Window
    {
        int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        Classes cls;
        string className = "";
        Parameters parms;

        string mailTag = GlobalShared.MAIL_TAGS[(int)GlobalShared.MAIL_TAGS_ENUM.TenantLateNotice];

        // For Emergency Billing Notices
        bool rentNotices = false;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmNotices()
        {
            InitializeComponent();
        }
        public frmNotices(string parm, string rent = "L")
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            if (rent.Trim().ToLower().Equals("r"))
            {
                rentNotices = true;
                this.Title = "Rent Notices";
            }
        }
        private void formNotices_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            className = className = this.GetType().Name; 
            cls = Classes.load(0, className);
            if (cls != null)
                parms = Parameters.DeSerialize(cls.value);
            else
            {
                parms = new Parameters();
                cls = new Classes();
            }
            if (parms.Form == 0)
                optOne.IsChecked = true;
            else
                optTwo.IsChecked = true;
            MainGrid.DataContext = parms;
            dtDate.SelectedDate = DateTime.Now;

            ChargeCodes[] chgCodes = ChargeCodes.loadLegal(1, false, false, true);
            lstLegal1.ItemsSource = chgCodes;
            lstLegal2.ItemsSource = chgCodes;

            if (rentNotices)
            {
                txtAmount.IsEnabled = false;
                optOne.IsEnabled = false;
                optTwo.IsEnabled = false;
                lstLegal1.IsEnabled = false;
                lstLegal2.IsEnabled = false;
            }

            txtStartBuilding.Focus();
            txtStartBuilding.SelectAll();
        }

        private void formNotices_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
            if (optOne.IsChecked.Value)
                parms.Form = 0;
            else
                parms.Form = 1;

            cls.value = parms.SerializeObject();
            cls.clientId = clientId;
            cls.acctId = 0;
            cls.name = className;
            cls.save();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            int laserType = optOne.IsChecked.Value ? 1 : 2;
            if (rentNotices)
                laserType = 3;
            LateNotices rpt = new LateNotices(txtStartBuilding.Text,
                txtStartTenant.Text,
                txtEndBuilding.Text,
                txtEndTenant.Text,
                parms.Legal1,
                parms.Legal2,
                (int)txtTopMargin.Value,
                (double)txtLeftMargin.Value,
                (rentNotices ? (decimal)1 : (decimal)txtAmount.Value),
                (DateTime)dtDate.SelectedDate,
                laserType,
                false,
                null,
                "")  { rentNotices = this.rentNotices };
            rpt.PrintNotices(rpt.RetrieveTenants());
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FocusObtained(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt != null)
                txt.SelectAll();
        }

        private void cmdEmail_Click(object sender, RoutedEventArgs e)
        {

            int laserType = optOne.IsChecked.Value ? 1 : 2;
            if (rentNotices)
                laserType = 3;
            Email.SmtpConfig smtp;
            string fromEmail;
            string message;
            if (!Email.Utils.GetEmailConfig(mailTag, out message, out smtp, out fromEmail,true))
            {
                MessageBox.Show("Unable to retrieve Email account data <" + mailTag + ">: " + message);
                return;
            }
            if (fromEmail.Trim().Equals(""))
            {
                MessageBox.Show("A FROM address has not been selected.");
            }
            System.Windows.Input.Cursor cursor = this.Cursor;
            this.Cursor = System.Windows.Input.Cursors.Wait;
            MbcsMail.Smtp.Config smtpConfig = new MbcsMail.Smtp.Config() { description = smtp.description, emailAddress = smtp.emailAddress, userName = smtp.userName, password = smtp.password, server = smtp.server, port = smtp.port, SSL = smtp.SSL, TSL = smtp.TSL, authentication = smtp.authentication };
            LateNotices rpt = new LateNotices(txtStartBuilding.Text,
                txtStartTenant.Text,
                txtEndBuilding.Text,
                txtEndTenant.Text,
                parms.Legal1,
                parms.Legal2,
                (int)txtTopMargin.Value,
                (double)txtLeftMargin.Value,
                (rentNotices ? (decimal)1 : (decimal)txtAmount.Value),
                (DateTime)dtDate.SelectedDate,
                laserType,
                true,
                smtp,
                fromEmail) { rentNotices = this.rentNotices };

            frmNoticesEmail frm = new frmNoticesEmail() { rentNotices = this.rentNotices, EmailAccount = smtp.emailAddress, displayname = smtp.description };
            frm.tenants = rpt.RetrieveTenants();
            this.Cursor = cursor;
            frm.ShowDialog();
            if (frm.tenants == null)
            {
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure that you want to send late notices via EMAIL?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                
                rpt.DisplayName = frm.DisplayName;
                rpt.EmailSubject = frm.EmailSubject;
                rpt.Message = frm.Message;
                rpt.ReturnReceipt = frm.ReturnReceipt;
                rpt.Test = frm.Test;
                rpt.PrintNotices(frm.tenants);
                
                return;
            }
        }

        private void moveNextOnMaxlength(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);
        }


    }

    public class Parameters : INotifyPropertyChanged
    {
        double topMargin;
        double leftMargin;
        decimal amount;
        int form;
        string legal1;
        string legal2;
        bool isChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public Parameters()
        {
        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static Parameters DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));
            StringReader rdr = new StringReader(inputString);
            Parameters parms = (Parameters)serializer.Deserialize(rdr);
            return parms;
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            isChanged = true;
        }

        public double TopMargin
        {
            get{return topMargin;}
            set 
            {
                topMargin = value;
                OnPropertyChanged("TopMargin");
            }
        }
        public double LeftMargin
        {
            get{return leftMargin;}
            set 
            {
                leftMargin = value;
                OnPropertyChanged("LeftMargin");
            }
        }
        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public int Form
        {
            get{return form;}
            set
            {
                form = value;
                OnPropertyChanged("Form");
            }
        }
        public string Legal1
        {
            get { return legal1; }
            set
            {
                legal1 = value;
                OnPropertyChanged("Legal1");
            }
        }
        public string Legal2
        {
            get { return legal2; }
            set
            {
                legal2 = value;
                OnPropertyChanged("Legal2");
            }
        }
        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        {
            get{return isChanged;}
            set
            {
                isChanged = value;
            }
        }
    }
}
