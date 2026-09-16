using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Drawing.Text;
using System.Windows;
using MbcsUtils;
using MbcsCentral;
using System.Threading;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmStatusReport.xaml
    /// </summary>
    public partial class frmStatusReport : Window
    {
        public bool IsBusy { get; set; }

        string baseFontName = "Courier New";
        float baseFontSize = (float)7.0;

        Dictionary<string, string> configs = new Dictionary<string,string>();
        DictUtils config;
        private StatusReport rpt;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmStatusReport()
        {
            InitializeComponent();
        }
        public frmStatusReport(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void formStatusReport_Loaded_1( object sender, RoutedEventArgs e )
        {
            BorderWait.DataContext = this;
            Main.getForm( this );
            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            lstFontNames.ItemsSource = fontFamilies;
            lstFontNames.DisplayMemberPath = "Name";
            lstFontNames.SelectedValuePath = "Name";


            lstFontSizes.Items.Add( "(none)" );
            lstFontSizes.Items.Add( " 6" );
            lstFontSizes.Items.Add( " 7" );
            lstFontSizes.Items.Add( " 8" );
            lstFontSizes.Items.Add( " 9" );
            lstFontSizes.Items.Add( "10" );
            lstFontSizes.Items.Add( "11" );
            lstFontSizes.Items.Add( "12" );
            lstFontSizes.Items.Add( "13" );
            lstFontSizes.Items.Add( "14" );

            config = new DictUtils(GlobalShared.appPath + WinpmGlobal.WINPM_CONFIG_FILE, configs );
            config.getConfigs();
            config.getItem( GlobalShared.CONFIG_WINPM_CHARGE_SUMMARIES, ref chkChargeSummaries );
            config.getItem(GlobalShared.CONFIG_WINPM_COLLECTED_FROM, ref chkCollectedFrom);
            config.getItem( GlobalShared.CONFIG_WINPM_CONDENSE_PRINT, ref chkCondensePrint );
            config.getItem( GlobalShared.CONFIG_WINPM_SHORT_FORM, ref chkShortForm );
            config.getItem( GlobalShared.CONFIG_WINPM_PRINT_QUALITY, ref chkLetterQuality );
            config.getItem( GlobalShared.CONFIG_WINPM_BOLD_PRINT, ref chkBoldPrint );
            config.getItem(GlobalShared.CONFIG_WINPM_GRAND_TOTALS, ref chkGrandTotals);
            config.getItem(GlobalShared.CONFIG_WINPM_UNIT_TOTALS, ref chkUnitTotals);
            string s = "";
            config.getItem(GlobalShared.CONFIG_WINPM_FONTNAME, ref s );
            foreach (System.Drawing.FontFamily f in lstFontNames.Items)
            {
                if (f.Name.Equals(s))
                {
                   lstFontNames.SelectedIndex = lstFontNames.Items.IndexOf( f );
                   break;
                }
            }
            lstFontNames.Text = s;
            config.getItem(GlobalShared.CONFIG_WINPM_FONTSIZE, ref s );
            lstFontSizes.SelectedIndex = lstFontSizes.Items.IndexOf( s );
            lstFontSizes.Text = s;
            config.getItem(GlobalShared.CONFIG_WINPM_PAGESIZE, ref s );
            lstPageSize.Text = s;

            datReport.DisplayDate = DateTime.Today;
            datReport.SelectedDate = (DateTime)DateTime.Today;
            datReport.Text = DateTime.Today.ToString();

            txtStartingBuildingNumber.Focus();

        }

        private void formStatusReport_Closed_1( object sender, EventArgs e )
        {
            Main.saveForm( this );
            try
            {
                config.setItem( GlobalShared.CONFIG_WINPM_CHARGE_SUMMARIES, chkChargeSummaries );
                config.setItem(GlobalShared.CONFIG_WINPM_COLLECTED_FROM, chkCollectedFrom);
                config.setItem( GlobalShared.CONFIG_WINPM_CONDENSE_PRINT, chkCondensePrint );
                config.setItem( GlobalShared.CONFIG_WINPM_SHORT_FORM, chkShortForm );
                config.setItem( GlobalShared.CONFIG_WINPM_PRINT_QUALITY, chkLetterQuality );
                config.setItem( GlobalShared.CONFIG_WINPM_BOLD_PRINT, chkBoldPrint );
                config.setItem(GlobalShared.CONFIG_WINPM_GRAND_TOTALS, chkGrandTotals);
                config.setItem(GlobalShared.CONFIG_WINPM_UNIT_TOTALS, chkUnitTotals);
                config.setItem( GlobalShared.CONFIG_WINPM_FONTNAME, lstFontNames.Text );
                config.setItem( GlobalShared.CONFIG_WINPM_FONTSIZE, lstFontSizes.Text );
                config.setItem( GlobalShared.CONFIG_WINPM_PAGESIZE, lstPageSize.Text );
                config.setConfigs();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdExit_Click_1( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void cmdDisplay_Click_1( object sender, RoutedEventArgs e )
        {
            this.Cursor = System.Windows.Input.Cursors.Wait;
            try
            {
                setProps(false);
            } catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Calling setProps from frmStatusReport: " + ex.Message, false);
            }
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void cmdPrint_Click_1( object sender, RoutedEventArgs e )
        {
            frmPrintDialog pd = new frmPrintDialog();
            //pd.DefaultPrinter = parms.DefualtPrinter;
            pd.Owner = this;
            pd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            pd.ShowDialog();
            if (pd.Canceled)
                return;
            System.Printing.PrintQueue queue = pd.Queue;
            if (pd.Duplex)
            {
                queue.DefaultPrintTicket.Duplexing = (pd.DuplexType == System.Printing.Duplexing.TwoSidedShortEdge ? System.Printing.Duplexing.TwoSidedShortEdge : System.Printing.Duplexing.TwoSidedLongEdge);
            }
            else
                queue.DefaultPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
            queue.CurrentJobSettings.CurrentPrintTicket.Duplexing = queue.DefaultPrintTicket.Duplexing;
            //parms.DefualtPrinter = pd.DefaultPrinter;
            setProps( true , queue);
        }

        /*************************************************************
         * Run report process on a separate thread.
         * 
         * Consists of 2 methods:
         * 1) Gathers data for particular report, creates thread and
         *    starts it
         * 2) The entry point for the separate thread that instantiates
         *    the class for the report to be processed
         **************************************************************/
        private void setProps(bool target, System.Printing.PrintQueue queue = null)
        {
            float fntSize;
            try
            {
                fntSize = baseFontSize;
                string fntName = baseFontName;
                if (!chkCondensePrint.IsChecked.Value && lstFontSizes.SelectedIndex > -1)
                    fntSize = (float)float.Parse(lstFontSizes.SelectedItem.ToString());
                if (!chkCondensePrint.IsChecked.Value && lstFontNames.SelectedIndex > -1)
                    fntName = lstFontNames.Text;

                //object[] obj = {txtStartingBuildingNumber.Text, txtEndingBuildingNumber.Text,
                //    chkChargeSummaries.IsChecked.Value, chkCondensePrint.IsChecked.Value,
                //    chkBoldPrint.IsChecked.Value, fntName, fntSize, target, (DateTime)datReport.SelectedDate, chkGrandTotals.IsChecked.Value, queue};
                StatusReport rpt = new StatusReport(txtStartingBuildingNumber.Text, txtEndingBuildingNumber.Text,
                    chkChargeSummaries.IsChecked.Value, chkCollectedFrom.IsChecked.Value, chkCondensePrint.IsChecked.Value,
                    chkBoldPrint.IsChecked.Value, fntName, fntSize, target, (DateTime)datReport.SelectedDate, chkGrandTotals.IsChecked.Value, queue)
                { UnitTotals = chkUnitTotals.IsChecked.Value };

                    rpt.Process();
                //StartReport.Start("MbcsCentral.WinPM.StatusReport", obj);
            } catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Setting Properties in frmStatusReport: " + ex.Message, false);
            }
        }

        private void TextBox_GotFocus( object sender, RoutedEventArgs e )
        {
            TextBox txt = (TextBox) sender;
            string s = txt.Text.Trim();
            txt.Text = s;
            txt.SelectionLength = s.Length;
        }

        private void moveNextOnMaxlength( object sender, TextChangedEventArgs e )
        {
            Main.moveNextOnMaxlength( sender, e );
        }

    }
}
