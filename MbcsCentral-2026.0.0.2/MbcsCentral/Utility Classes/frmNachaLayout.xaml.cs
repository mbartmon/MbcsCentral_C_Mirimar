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


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmNachaLayout.xaml
    /// </summary>
    public partial class frmNachaLayout : Window
    {
        CollectionViewSource NachaFileHeader;
        NACHA_Layout[] ControlData;
        NACHA_Layout[] FileHeader;
        NACHA_Layout[] BatchHeader;
        NACHA_Layout[] BatchVendorHeader;
        NACHA_Layout[] Detail;
        NACHA_Layout[] Addenda;
        NACHA_Layout[] BatchControl;
        NACHA_Layout[] FileControl;

        CollectionViewSource RequirementsSource;

        public frmNachaLayout()
        {
            InitializeComponent();
        }

        string MyApp = "";

        public frmNachaLayout(string myApp)
        {
            InitializeComponent();
            MyApp = myApp;
        }
        private void frmNachaLayout_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);

            ControlData = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
            grdControlData.ItemsSource = ControlData;
            tabControlData.Tag = (int)NACHA_Layout.Record_Types.ControlData;

            FileHeader = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileHeader);
            grdFileHeader.ItemsSource = FileHeader;
            tabFileHeader.Tag = (int)NACHA_Layout.Record_Types.FileHeader;

            BatchHeader = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchHeader);
            grdBatchHeader.ItemsSource = BatchHeader;
            tabBatchHeader.Tag = (int)NACHA_Layout.Record_Types.BatchHeader;

            BatchVendorHeader = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchVendorHeader);
            grdBatchVendorHeader.ItemsSource = BatchVendorHeader;
            tabBatchVendorHeader.Tag = (int)NACHA_Layout.Record_Types.BatchVendorHeader;

            Detail = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.DetailRecord);
            grdDetailRecord.ItemsSource = Detail;
            tabDetail.Tag = (int)NACHA_Layout.Record_Types.DetailRecord;

            Addenda = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.AddendaRecord);
            grdAddendaRecord.ItemsSource = Addenda;
            tabAddenda.Tag = (int)NACHA_Layout.Record_Types.AddendaRecord;

            BatchControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchControl);
            grdBatchControl.ItemsSource = BatchControl;
            tabBatchControl.Tag = (int)NACHA_Layout.Record_Types.BatchControl;

            FileControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileControl);
            grdFileControl.ItemsSource = FileControl;
            tabFileControl.Tag = (int)NACHA_Layout.Record_Types.FileControl;

            RequirementsSource = (CollectionViewSource)this.FindResource("NachaRequirements");
            RequirementsSource.Source = NACHA_Layout.Requirements;

        }

        private void frmNachaLayout_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            SaveRecords(ControlData);
            SaveRecords(FileHeader);
            SaveRecords(BatchHeader);
            SaveRecords(BatchVendorHeader);
            SaveRecords(Detail);
            SaveRecords(Addenda);
            SaveRecords(BatchControl);
            SaveRecords(FileControl);
        }

        private void SaveRecords(NACHA_Layout[] recs)
        {
            foreach (NACHA_Layout rec in recs)
            {
                if (rec.Delete)
                    rec.delete(rec.Id.ToString());
                else
                    rec.save();
            }

        }
        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabNacha.SelectedItem;
            int recordType = (int)tab.Tag;
            int l = 0;
            int p = 1;
            int field = 1;

            switch (recordType)
            {
                case (int)NACHA_Layout.Record_Types.ControlData:

                    if (ControlData != null && ControlData.Length > 0)
                    {
                        l = ControlData.Length;
                        p = ControlData[l - 1].Position + ControlData[l - 1].Length;
                        field = ControlData[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref ControlData, l + 1);
                    l = ControlData.Length - 1;
                    ControlData[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdControlData.ItemsSource = null;
                    grdControlData.ItemsSource = ControlData;
                    break;

                case (int)NACHA_Layout.Record_Types.FileHeader:
                    if (FileHeader != null && FileHeader.Length > 0)
                    {
                        l = FileHeader.Length;
                        p = FileHeader[l - 1].Position + FileHeader[l - 1].Length;
                        field = FileHeader[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref FileHeader, l + 1);
                    l = FileHeader.Length - 1;
                    FileHeader[l] = new NACHA_Layout() {Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdFileHeader.ItemsSource = null;
                    grdFileHeader.ItemsSource = FileHeader;
                    break;

                case (int)NACHA_Layout.Record_Types.BatchHeader:
                    if (BatchHeader != null && BatchHeader.Length > 0)
                    {
                        l = BatchHeader.Length;
                        p = BatchHeader[l - 1].Position + BatchHeader[l - 1].Length;
                        field = BatchHeader[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref BatchHeader, l + 1);
                    l = BatchHeader.Length - 1;
                    BatchHeader[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdBatchHeader.ItemsSource = null;
                    grdBatchHeader.ItemsSource = BatchHeader;
                    break;

                case (int)NACHA_Layout.Record_Types.BatchVendorHeader:
                    if (BatchVendorHeader != null && BatchVendorHeader.Length > 0)
                    {
                        l = BatchHeader.Length;
                        p = BatchVendorHeader[l - 1].Position + BatchVendorHeader[l - 1].Length;
                        field = BatchVendorHeader[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref BatchVendorHeader, l + 1);
                    l = BatchVendorHeader.Length - 1;
                    BatchVendorHeader[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdBatchVendorHeader.ItemsSource = null;
                    grdBatchVendorHeader.ItemsSource = BatchVendorHeader;
                    break;


                case (int)NACHA_Layout.Record_Types.DetailRecord:
                    if (Detail != null && Detail.Length > 0)
                    {
                        l = Detail.Length;
                        p = Detail[l - 1].Position + Detail[l - 1].Length;
                        field = Detail[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref Detail, l + 1);
                    l = Detail.Length - 1;
                    Detail[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdDetailRecord.ItemsSource = null;
                    grdDetailRecord.ItemsSource = Detail;
                    break;

                case (int)NACHA_Layout.Record_Types.AddendaRecord:
                    if (Addenda != null && Addenda.Length > 0)
                    {
                        l = Addenda.Length;
                        p = Addenda[l - 1].Position + Addenda[l - 1].Length;
                        field = Addenda[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref Addenda, l + 1);
                    l = Addenda.Length - 1;
                    Addenda[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdAddendaRecord.ItemsSource = null;
                    grdAddendaRecord.ItemsSource = Addenda;
                    break;


                case (int)NACHA_Layout.Record_Types.BatchControl:
                    if (BatchControl != null && BatchControl.Length > 0)
                    {
                        l = BatchControl.Length;
                        p = BatchControl[l - 1].Position + BatchControl[l - 1].Length;
                        field = BatchControl[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref BatchControl, l + 1);
                    l = BatchControl.Length - 1;
                    BatchControl[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdBatchControl.ItemsSource = null;
                    grdBatchControl.ItemsSource = BatchControl;
                    break;
                case (int)NACHA_Layout.Record_Types.FileControl:
                    if (FileControl != null && FileControl.Length > 0)
                    {
                        l = FileControl.Length;
                        p = FileControl[l - 1].Position + FileControl[l - 1].Length;
                        field = FileControl[l - 1].Field + 1;
                    }
                    Array.Resize<NACHA_Layout>(ref FileControl, l + 1);
                    l = FileControl.Length - 1;
                    FileControl[l] = new NACHA_Layout() { Field = field, Record_Type = recordType.ToString(), Position = p };
                    grdFileControl.ItemsSource = null;
                    grdFileControl.ItemsSource = FileControl;
                    break;
            }

        }

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabNacha.SelectedItem;
            int recordType = (int)tab.Tag;
            NACHA_Layout rec = null;

            switch (recordType)
            {
                case (int)NACHA_Layout.Record_Types.ControlData:
                    rec = (NACHA_Layout)grdControlData.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.FileHeader:
                    rec = (NACHA_Layout)grdFileHeader.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.BatchHeader:
                    rec = (NACHA_Layout)grdBatchHeader.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.BatchVendorHeader:
                    rec = (NACHA_Layout)grdBatchVendorHeader.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.DetailRecord:
                    rec = (NACHA_Layout)grdDetailRecord.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.AddendaRecord:
                    rec = (NACHA_Layout)grdAddendaRecord.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.BatchControl:
                    rec = (NACHA_Layout)grdBatchControl.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;
                case (int)NACHA_Layout.Record_Types.FileControl:
                    rec = (NACHA_Layout)grdFileControl.SelectedItem;
                    rec.Delete = !rec.Delete;
                    break;

            }
        }

        private void ctxAddVendorControl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ctxDeleteVendorControl_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
