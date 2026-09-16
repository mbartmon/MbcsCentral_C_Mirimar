using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;


namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for FormControl.xaml
    /// </summary>
    public partial class FormControl : Window
    {
        ArrayList GlList;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        private Control[] control = null;

        public FormControl()
        {
            InitializeComponent();
        }
        public FormControl(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            control = Control.load();
            if (control == null || control.Length == 0)
            {
                MessageBox.Show("Control Record not found.");
                this.Close();
                return;
            }
            Tabcontrol.DataContext = control[0];
            DateDhcr.SelectedDate = DateTime.Now.AddYears(-5);
            if (control[0].UseWingl && !control[0].WinglDatabase.Trim().Equals(WinpmGlobal.WINGL_LOCATION_TAG))
                chkGlExternal.IsChecked = true;

            GlList = GLTable.loadList();
            grdGlTable.ItemsSource = (GlTableList[])GlList.ToArray(typeof(GlTableList));

            Building[] buildings = Building.load(GlobalShared.user.Account.clientId);
            System.Windows.Data.CollectionViewSource BuildingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Buildings")));
            BuildingViewSource.Source = buildings;
            System.Collections.Generic.List<Building> buildingList = new List<Building>();
            foreach (Building bld in buildings)
                buildingList.Add(bld);
            System.Windows.Data.CollectionViewSource ChargesCodeSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ChargeCodes")));
            ChargeCodes[] codes = ChargeCodes.loads(GlobalShared.user.Account.clientId);
            ChargesCodeSource.Source = codes;
            //grdFrom.ItemsSource = buildingList;
            //grdTo.ItemsSource = buildingList;
            //Email.SmtpConfig[] smtpConfigs = Email.SmtpConfig.load();
            Email.SmtpConfig[] smtpConfigs = Email.SmtpConfig.loadByAccount((int)GlobalShared.user.Account.accountId);
            lstEmail.ItemsSource = smtpConfigs;

            string emailName = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\EmailNames\\", "General", " ");
            if (emailName != null)
                for (int i = 0; i < lstEmail.Items.Count; i++)
                    if (((Email.SmtpConfig)lstEmail.Items[i]).description.ToString().ToLower().Equals(emailName.ToLower()))
                        lstEmail.SelectedIndex = i;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (chkGL.IsChecked.Value && !chkGlExternal.IsChecked.Value)
                control[0].WinglDatabase = WinpmGlobal.WINGL_LOCATION_TAG;
            if (control[0].isChanged)
            {
                MessageBoxResult result = MessageBox.Show("Save changes?", "", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        CmdSave_Click(null, new RoutedEventArgs());
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        return;
                        break;
                }
            }
            if (lstEmail.SelectedIndex >= 0)
            {
                string emailName = ((Email.SmtpConfig)lstEmail.SelectedItem).description;
                Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\EmailNames\\", "TenantDirect", emailName, (int)Registry.DataTypes.String );
            }
            Main.saveForm(this);
        }

        private void CmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to modify this data.");
                return;
            }

            if (control[0].isChanged)
                control[0].save();
            control[0].isChanged = false;
            foreach (GlTableList gl in GlList)
            {
                gl.save();
            }
            Main.setMainWindowTitle();
            if (sender != null)
                this.Close();
        }

        private void CmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmdWindoc_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MS Access files | *.mdb";
            if (control[0].WindocDatabase.Trim() != "")
            {
                dlg.FileName = control[0].WindocDatabase;
                if (!File.Exists(dlg.FileName) || !Directory.Exists(dlg.FileName))
                {
                    dlg.FileName = "";
                }
                else
                    dlg.InitialDirectory = control[0].WindocDatabase.Substring(0, control[0].WindocDatabase.LastIndexOf("\\"));
            }
            if (dlg.ShowDialog() == true)
            {
                control[0].WindocDatabase = dlg.FileName;
                CheckDocs.IsChecked = true;
            }
            
        }

        private void CmdDhcr_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear DHCR Data older than or equal to " + ((DateTime)DateDhcr.SelectedDate).ToShortDateString() + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            MessageBox.Show("DHCR clearing will work in the background. You can close the options window.");
            WinpmGlobal.clearDHCR((DateTime)DateDhcr.SelectedDate);
        }

        private void cmdGlBrowse_Click(object sender, RoutedEventArgs e)
        {

             OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MS Access files | *.mdb";
            if (control[0].WinglDatabase != null && control[0].WinglDatabase.Trim() != "" && !control[0].WinglDatabase.Trim().Equals(WinpmGlobal.WINGL_LOCATION_TAG))
            {
                dlg.FileName = control[0].WinglDatabase;
                dlg.InitialDirectory = control[0].WinglDatabase.Substring(0, control[0].WinglDatabase.LastIndexOf("\\"));
            }
            bool rc = false;
            try
            {
                rc = (bool)dlg.ShowDialog();
            }
            catch
            {
                dlg.InitialDirectory = "c:\\";
                rc = (bool)dlg.ShowDialog();
            }
            if (rc == true)
            {
                control[0].WinglDatabase = dlg.FileName;
                CheckDocs.IsChecked = true;
            }
            
       }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstBuildingFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdGlTable.SelectedIndex;
            if (n >= GlList.Count || n < 0 || c.SelectedItem == null)
                return;
            ((GlTableList)GlList[n]).From = ((Building)c.SelectedItem).BuildingNumber;
            ((GlTableList)GlList[n]).FromBuilding = ((Building)c.SelectedItem).FullName;
        }
        private void lstBuildingTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdGlTable.SelectedIndex;
            if (n >= GlList.Count || n < 0 || c.SelectedItem == null)
                return;
            ((GlTableList)GlList[n]).To = ((Building)c.SelectedItem).BuildingNumber;
            ((GlTableList)GlList[n]).ToBuilding = ((Building)c.SelectedItem).FullName;
        }    
    }
}
