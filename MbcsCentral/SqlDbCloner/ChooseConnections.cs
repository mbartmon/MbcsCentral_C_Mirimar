using Microsoft.Data.ConnectionUI;
using SqlDbCloner.Core.Data;
using SqlDbCloner.Core.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqlDbCloner
{
    public partial class ChooseConnections : Form
    {
        public bool DoSchema { get; set; } = true;
        public ChooseConnections()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormOnLoad();
        }

        public void FormOnLoad()
        {
            txtSource.Text = MbcsCentral.Properties.Settings.Default.SourceServer;
            txtDestination.Text = MbcsCentral.Properties.Settings.Default.DestinationServer;
            if (DoSchema)
            {
                isSchema.Checked = true;
                btnNext_Click(null, new EventArgs());
            }
            isSchema.Checked = false;
            isData.Checked = true;
            btnNext_Click(null, new EventArgs());

        }
        private void btnDestination_Click(object sender, EventArgs e)
        {
            txtDestination.Text = GetConnectionString();
        }

        private string GetConnectionString()
        {
            string conn = null;
            using (DataConnectionDialog dialog = new DataConnectionDialog())
            {
                DataSource.AddStandardDataSources(dialog);
                if (DataConnectionDialog.Show(dialog) == System.Windows.Forms.DialogResult.OK)
                {
                    conn = dialog.ConnectionString;
                }
            }
            return conn;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            txtSource.Text = GetConnectionString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MbcsCentral.Properties.Settings.Default.SourceServer = txtSource.Text;
            MbcsCentral.Properties.Settings.Default.DestinationServer = txtDestination.Text;
            MbcsCentral.Properties.Settings.Default.Save();
            
            if (isSchema.Checked)
            {
                SqlTransfer transfer;
                List<SqlObject> items;
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    transfer = new SqlTransfer(MbcsCentral.Properties.Settings.Default.SourceServer, MbcsCentral.Properties.Settings.Default.DestinationServer);
                    items = transfer.SourceObjects;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    this.Cursor = Cursors.Default;
                    return;
                }
                
                if (items != null || items.Count() > 0)
                {
                    this.Hide();
                    SelectSchema next = new SelectSchema(transfer, items);
                    
                    //if (next.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //    this.Show();
                    //else
                        this.Close();
                }
                else
                {
                    MessageBox.Show("No SQL objects found in source database to show");
                }
                this.Cursor = Cursors.Default;
            }
            else if (isData.Checked)
            {
                DataTransfer transfer;
                List<SqlDbCloner.Core.Data.DataObject> items;
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    transfer = new DataTransfer(MbcsCentral.Properties.Settings.Default.SourceServer, MbcsCentral.Properties.Settings.Default.DestinationServer);
                    items = new List<Core.Data.DataObject>();
                    /*
                    foreach (var item in MbcsCentral.Properties.Settings.Default.DataSettings)
                    {
                        var array = item.Split('\t');
                        var itm = new SqlDbCloner.Core.Data.DataObject();
                        itm.Table = array[0];
                        if(array.Count() == 2)
                            itm.SqlCommand = array[1];
                        items.Add(itm);
                    }
                    */
                    if(items.Count == 0)
                        items = transfer.SourceObjects;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (items != null || items.Count() > 0)
                {
                    this.Hide();
                    CopyData next = new CopyData(items, transfer);
                    next.FormOnLoad();
                    //if (next.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //    this.Show();
                    //else
                    //    this.Close();
                }
                else
                {
                    MessageBox.Show("No SQL objects found in source database to proceed");
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Please select your operation to proceed further");
            }
        }
    }
}
