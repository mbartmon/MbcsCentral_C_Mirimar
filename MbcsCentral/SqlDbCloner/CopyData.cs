using SqlDbCloner.Core.Schema;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SqlDbCloner.Core.Data;

namespace SqlDbCloner
{
    public partial class CopyData : Form
    {
        List<SqlDbCloner.Core.Data.DataObject> list;
        DataTransfer transfer;
        int errorCount;

        public CopyData(List<SqlDbCloner.Core.Data.DataObject> list, DataTransfer transfer)
        {
            InitializeComponent();
            this.list = list;
            this.transfer = transfer;

            foreach (var item in list)
            {
                dataGridView1.Rows.Add(null, item.Table, item.SqlCommand, null);
            }

            label1.Text = "Click on 'Copy' button to start copying below listed SQL objects data";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {            
            btnNext.Enabled = false;
            btnBack.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            DoWork();
            return;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
            label1.Text = "Copying in progress...";
            backgroundWorker1.RunWorkerAsync();
        }

        private void DoWork()
        {
            //MbcsCentral.frmProgressWindow frm = (MbcsCentral.frmProgressWindow)MbcsCentral.GlobalShared.DbCopyStatus;
            string message = "Copying Tables: ";
            double current = 0;
            double max = dataGridView1.Rows.Count;

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {

                if (item.IsNewRow)
                    continue;

                try
                {
                    MbcsCentral.GlobalShared.DbCopyStatus.message = message + item.Cells["Table"];
                    if (item.Cells["SqlCommand"].Value == null || string.IsNullOrWhiteSpace(item.Cells["SqlCommand"].Value.ToString()))
                    {
                        item.Cells["SqlCommand"].Value = "SELECT * FROM " + item.Cells["Table"].Value.ToString() + " WITH(NOLOCK)";
                    }
                    transfer.TrasnferData(item.Cells["Table"].Value.ToString(), item.Cells["SqlCommand"].Value.ToString());
                    item.Cells["Status"].Value = MbcsCentral.Properties.Resources.success;
                }
                catch (Exception exc)
                {
                    item.Cells["Status"].Value = MbcsCentral.Properties.Resources.failure;
                    item.Cells["Error"].Value = exc.Message;
                    if (exc.InnerException != null)
                        item.Cells["Error"].Value = exc.InnerException.Message;
                    errorCount++;
                }
            }

        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = e.ProgressPercentage + " %";
            dataGridView1.Refresh();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            label1.Text = "Operation completed";
            if (errorCount == 0)
                label1.Text += " successfully";
            else
                label1.Text += " with " + errorCount + " errors";
            btnCancel.Text = "Close";
            btnBack.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            double current = 0;
            double max = dataGridView1.Rows.Count;

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                backgroundWorker1.ReportProgress((int)(((++current) / max) * 100.0));

                if (item.IsNewRow)
                    continue;

                if (backgroundWorker1.CancellationPending)
                    break;
                try
                {
                    if (item.Cells["SqlCommand"].Value == null || string.IsNullOrWhiteSpace(item.Cells["SqlCommand"].Value.ToString()))
                    {
                        item.Cells["SqlCommand"].Value = "SELECT * FROM " + item.Cells["Table"].Value.ToString() + " WITH(NOLOCK)";
                    }
                    transfer.TrasnferData(item.Cells["Table"].Value.ToString(), item.Cells["SqlCommand"].Value.ToString());
                    item.Cells["Status"].Value = MbcsCentral.Properties.Resources.success;
                }
                catch (Exception exc)
                {
                    item.Cells["Status"].Value = MbcsCentral.Properties.Resources.failure;
                    item.Cells["Error"].Value = exc.Message;
                    if (exc.InnerException != null)
                        item.Cells["Error"].Value = exc.InnerException.Message;
                    errorCount++;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<int> indexes = new List<int>();
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }
        }

        private void CopyData_Load(object sender, EventArgs e)
        {
            FormOnLoad();
        }

        public void FormOnLoad()
        {
            //dataGridView1.Columns[0].Width = 40;
            ((DataGridViewImageColumn)this.dataGridView1.Columns["Status"]).DefaultCellStyle.NullValue = null;
            btnNext_Click(null, new EventArgs());

        }
    }
}
