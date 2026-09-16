using SqlDbCloner.Core.Schema;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace SqlDbCloner
{
    public partial class CopySchema : Form
    {
        List<SqlObject> list;
        SqlTransfer transfer;
        int errorCount;

        public CopySchema(List<SqlObject> list, SqlTransfer transfer)
        {
            InitializeComponent();
            this.list = list;
            this.transfer = transfer;
            dataGridView1.DataSource = list;
            label1.Text = "Click on 'Copy' button to start copying below listed SQL objects";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {            
            btnNext.Enabled = false;
            btnBack.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            transfer.Refresh();
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

            string message = "Creating Schema Objects: ";
            double current = 0;
            double max = list.Count;

            if (CopyConstraints)
                max += (list.Where(i => i.Type == "Table").Count() * 3.0);

            foreach (var item in list)
            {
                //frm.txtProgress.Text = message + item.Name;
                MbcsCentral.GlobalShared.DbCopyStatus.message = message + item.Name;

                try
                {
                    if (DropBeforeCreate)
                    {
                        transfer.DropAndCreateObject(item.Object);
                    }
                    else
                    {
                        transfer.CreateObject(item.Object);
                    }
                    item.Status = MbcsCentral.Properties.Resources.success;
                }
                catch (Exception exc)
                {
                    item.Status = MbcsCentral.Properties.Resources.failure;
                    item.Error = exc.Message;
                    if (exc.InnerException != null)
                        item.Error = exc.InnerException.Message;
                    errorCount++;
                }

            }

            if (CopyConstraints)
            {
                foreach (var item in list.Where(i => i.Type == "Table"))
                {
                    transfer.ApplyIndexes(item.Object);
                }

                foreach (var item in list.Where(i => i.Type == "Table"))
                {
                    transfer.ApplyForeignKeys(item.Object);
                }

                foreach (var item in list.Where(i => i.Type == "Table"))
                {
                    transfer.ApplyChecks(item.Object);
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
            double max = list.Count;

            if (CopyConstraints)
                max += (list.Where(i => i.Type == "Table").Count() * 3.0);

            foreach (var item in list)
            {
                if (backgroundWorker1.CancellationPending)
                    break;
                try
                {
                    if (DropBeforeCreate)
                    {
                        transfer.DropAndCreateObject(item.Object);
                    }
                    else
                    {
                        transfer.CreateObject(item.Object);
                    }
                    item.Status = MbcsCentral.Properties.Resources.success;
                }
                catch (Exception exc)
                {
                    item.Status = MbcsCentral.Properties.Resources.failure;
                    item.Error = exc.Message;
                    if (exc.InnerException != null)
                        item.Error = exc.InnerException.Message;
                    errorCount++;
                }
                
                backgroundWorker1.ReportProgress((int)(((++current)/max)*100.0));
            }

            if (CopyConstraints)
            {
                foreach (var item in list.Where(i => i.Type == "Table"))
                {                    
                    transfer.ApplyIndexes(item.Object);
                    backgroundWorker1.ReportProgress((int)(((++current) / max) * 100.0));
                }

                foreach (var item in list.Where(i => i.Type == "Table"))
                {
                    transfer.ApplyForeignKeys(item.Object);
                    backgroundWorker1.ReportProgress((int)(((++current) / max) * 100.0));
                }

                foreach (var item in list.Where(i => i.Type == "Table"))
                {
                    transfer.ApplyChecks(item.Object);
                    backgroundWorker1.ReportProgress((int)(((++current) / max) * 100.0));
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

        public bool DropBeforeCreate { get; set; }

        public bool CopyConstraints { get; set; }

        private void CopySchema_Load(object sender, EventArgs e)
        {
            FormOnload();
        }

        public void FormOnload()
        {
            //dataGridView1.Columns[0].Width = 40;
            ((DataGridViewImageColumn)this.dataGridView1.Columns["Status"]).DefaultCellStyle.NullValue = null;
            btnNext_Click(null, new EventArgs());
        }
    }
}
