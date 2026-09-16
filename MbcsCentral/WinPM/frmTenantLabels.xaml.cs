using System;
using System.Collections.Generic;
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
using CSharp.Labels.Avery;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmTenantLabels.xaml
    /// </summary>
    public partial class frmTenantLabels : Window
    {
        private int clientId = (int)GlobalShared.user.Account.clientId;
        long acctId = GlobalShared.user.Account.accountId;
        Classes rptClass;
        string className = "";
        LabelParameters parms;
        FixedDocument doc;
        string[] LabelTypes = CSharp.Labels.LabelPrint.LabelNames;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmTenantLabels()
        {
            InitializeComponent();
        }
        public frmTenantLabels(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }
        private void FormTenantLabels_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            className = className = this.GetType().Name;
            rptClass = Classes.load(acctId, className);
            if (rptClass != null)
                parms = LabelParameters.DeSerialize(rptClass.value);
            else
            {
                parms = new LabelParameters();
                rptClass = new Classes();
            }
            mainGrid.DataContext = parms;
            //dtLease.SelectedDate = DateTime.Now;
            //dtLease.Ischecked = false;
            lstLabels.SelectedIndex = (int)((double)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\Options", "LabelType", 0));
            ShowTenants.Visibility = System.Windows.Visibility.Collapsed;
            MinimumArrears.IsEnabled = false;
            lstType.IsEnabled = false;
            Types[] TypeDesc = Types.loadAll(GlobalShared.user.Account.clientId, true);
            lstType.ItemsSource = TypeDesc;
            lstLabels.ItemsSource = LabelTypes;
        }

        private FixedDocument GetTenants()
        {
            Tenants[] tens = null;
            string xsql = "SELECT ";
            if (MaxLabels.Value > 0)
                xsql += " TOP " + MaxLabels.Value.ToString();
            xsql += " * FROM " + Tenants.TABLE + " WHERE [" + Tenants.SQL_COL_BUILDING_NUMBER + "] BETWEEN '" + txtStartBuilding.Text + "' AND '" + txtEndBuilding.Text + "'";

            if (RadioArrears.IsChecked.Value)
            {
                xsql += " AND [" + Tenants.SQL_COL_RENT + "] + [" + Tenants.SQL_COL_ARREARS + "] + [" + Tenants.SQL_COL_TOTAL_CHARGES + "] - [" + Tenants.SQL_COL_PAID + "] >= " + MinimumArrears.Value.ToString();
            }
            if (RadioType.IsChecked.Value)
            {
                xsql += " AND [" + Tenants.SQL_COL_TYPE + "] IN (";
                System.Collections.IList items = lstType.SelectedItems;
                string s = "";
                for (int j = 0; j < items.Count; j++)
                {
                    if (s.Trim() != "")
                        s = ", ";
                    s += "'" + ((Types)items[j]).Type + "'";
                    //s += ((string)items[j]).Substring(0, ((string)items[j]).IndexOf(":"));
                    xsql +=  s;
                }
                xsql += ")";
                //s = s.Substring(0, s.IndexOf(":")).Trim();
            }
            if (RadioIndividual.IsChecked.Value)
            {
                if (lstTenants.Items.Count == 0)
                    return null;
                string bldNum;
                string tenNum;
                for (int j = 0; j < lstTenants.Items.Count; j++)
                {
                    Array.Resize<Tenants>(ref tens, j + 1);
                    bldNum = lstTenants.Items[j].ToString().Substring(0,3);
                    tenNum = lstTenants.Items[j].ToString().Substring(3);
                    Tenants[] t = Tenants.load(bldNum, tenNum);
                    tens[j] = t[0];
                }
            }  else
            {
                if (dtLease.Ischecked)
                {
                    xsql += " AND [" + Tenants.SQL_COL_LEASE_EXPIRATION_DATE + "] BETWEEN '" + dtLease.SelectedDate.Date + "' AND '" + (dtLeaseTo.SelectedDate != null ? ((DateTime)dtLeaseTo.SelectedDate).Date : dtLease.SelectedDate.Date) + "'";
                }
                xsql += " ORDER BY [" + Tenants.SQL_COL_BUILDING_NUMBER + "], [" + Tenants.SQL_COL_TENANT_NUMBER + "]";
                tens = Tenants.Query(xsql, GlobalShared.NULL_DATE);
            }

            if (tens == null || tens.Length == 0)
                return null;
            string currentBuilding = "";
            Building bld = null;
            CSharp.Labels.Avery.AveryData[] data = null;
            int i = 0;
            string TenantName = "";
            string AddTenantName = "";
            string Address1 = "";
            string Address2 = "";
            string Address3 = "";
            foreach (Tenants ten in tens)
            {
                if (!RadioIndividual.IsChecked.Value)
                {
                    if ( ten.TenantName.Contains("*") || ten.TenantName.ToLower().Contains("vacant") || (ten.BuildingNumber + ten.TenantNumber).CompareTo(txtStartBuilding.Text + txtStartTenant.Text) == -1)
                        continue;
                    if ((ten.BuildingNumber + ten.TenantNumber).CompareTo(txtEndBuilding.Text + txtEndTenant) > 0)
                        break;
                }
                if (!currentBuilding.Equals(ten.BuildingNumber))
                {
                    bld = Building.loadByBuildingNumber(ten.BuildingNumber, GlobalShared.user.Account.clientId);
                    currentBuilding = bld.BuildingNumber;
                }
                TenantName = "";
                AddTenantName = "";
                Address1 = "";
                Address2 = "";
                Address3 = "";
                TenantName = ten.TenantName;
                if (ten.AdditionalNameFlag && ten.AddName != null && !ten.AddName.Trim().Equals(""))
                    AddTenantName = ten.AddName;
                if (chkAlternateAddress.IsChecked.Value && ten.AlternateBillingAddressFlag)
                {
                    TenantName = ten.AltBillName;
                    if (ten.AltBillAdd1.Trim().Equals(""))
                    {
                        Address1 = ten.AltBillAdd2.Trim();
                        Address2 = ten.AltBillAdd3;
                    }
                    else
                    {
                        Address1 = ten.AltBillAdd1.Trim();
                        Address2 = ten.AltBillAdd2.Trim();
                        Address3 = ten.AltBillAdd3;
                    }
                    if (Address3.Equals(""))
                        Address1 += " - " + ten.Apartment.Trim();
                    else
                        Address2 += " - " + ten.Apartment.Trim();

                }
                else
                {
                    if (!AddTenantName.Trim().Equals(""))
                    {
                        Address1 = AddTenantName;
                        Address2 = bld.Address1.Trim() + " - " + ten.Apartment.Trim();
                        Address3 = bld.Address2;
                    }
                    else
                    {
                        Address1 = bld.Address1.Trim() + " - " + ten.Apartment.Trim();
                        Address2 = bld.Address2;
                    }
                }
                TenantName = WinpmGlobal.reverse_names(TenantName);
                Array.Resize<CSharp.Labels.Avery.AveryData>(ref data, (data == null ? 1 : data.Length + 1));
                data[i] = new CSharp.Labels.Avery.AveryData();
                if (chkPrintTenantNumber.IsChecked.Value)
                {
                    data[i].Line1 = ten.BuildingNumber + ten.TenantNumber;
                    data[i].Line2 = TenantName;
                    data[i].Line3 = Address1;
                    data[i].Line4 = Address2;
                    data[i++].Line5 = Address3;
                }
                else
                {
                    data[i].Line1 = TenantName;
                    data[i].Line2 = Address1;
                    data[i].Line3 = Address2;    
                    data[i++].Line4 = Address3;
                }
            }
            CSharp.Labels.LabelPrint lbls = new CSharp.Labels.LabelPrint(lstLabels.SelectedIndex, data);

            return lbls.Doc;
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

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\Options", "LabelType", lstLabels.SelectedIndex, (int)MbcsCentral.Registry.DataTypes.Dword);
            rptClass.value = parms.SerializeObject();
            rptClass.clientId = clientId;
            rptClass.acctId = acctId;
            rptClass.name = className;
            rptClass.save();
            this.Close();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lstLabels.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a label type.");
                return;
            }
            doc = GetTenants();
            if (doc == null)
            {
                MessageBox.Show("No tenant records retrieved.");
                return;
            }
            MbcsCentral.PrintDialogx pd = new MbcsCentral.PrintDialogx();
            Window wndw = new Window();
            wndw.Title = "Choose Printer";
            wndw.Height = (double)600;
            wndw.Width = (double)600;
            wndw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pd = new PrintDialogx();
            wndw.Show();
            if ((bool)pd.ShowDialog(wndw) != true)
            {
                wndw.Close();
                return;
            }
            wndw.Close();
            pd.PrintDocument(doc.DocumentPaginator, "Tenant Labels");
        }

        private void cmdDisplay_Click(object sender, RoutedEventArgs e)
        {
            if (lstLabels.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a label type.");
                return;
            }
            doc = GetTenants();
            if (doc == null)
            {
                MessageBox.Show("No tenant records retrieved.");
                return;
            }
            frmReport frm = new frmReport(doc, "Tenant Labels");
            frm.Show();
        }

        private void RadioAll_Click(object sender, RoutedEventArgs e)
        {
            string name = ((RadioButton)sender).Name;
            switch (name)
            {
                case "RadioAll":
                    ShowTenants.Visibility = System.Windows.Visibility.Collapsed;
                    lstType.IsEnabled = false;
                    MinimumArrears.IsEnabled = false;
                    break;
                case "RadioArrears":
                    MinimumArrears.IsEnabled = true;
                    lstType.IsEnabled = false;
                    ShowTenants.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "RadioIndividual":
                    ShowTenants.Visibility = System.Windows.Visibility.Visible;
                    cmdAdd.IsEnabled = true;
                    lstType.IsEnabled = false;
                    MinimumArrears.IsEnabled = false;
                    break;
                case "RadioType":
                    lstType.IsEnabled = true;
                    MinimumArrears.IsEnabled = false;
                    ShowTenants.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
        }

    }
    public class LabelParameters : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public LabelParameters()
        { }
        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static LabelParameters DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LabelParameters));
            StringReader rdr = new StringReader(inputString);
            LabelParameters parms = (LabelParameters)serializer.Deserialize(rdr);
            return parms;
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            IsChanged = true;
        }

        public decimal MinimumBalance { get; set; }
        public bool PrintTenantNumber { get; set; }
        public bool UseAlternateAddress { get; set; }


        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged { get; set; }
    }
}
