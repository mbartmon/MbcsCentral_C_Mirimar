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
    public partial class SelectSchema : Form
    {
        SqlTransfer transfer;
        IEnumerable<SqlObject> items;

        public SelectSchema(SqlTransfer transfer, IEnumerable<SqlObject> items)
        {
            InitializeComponent();
            this.transfer = transfer;
            this.items = items;

            var nodes = treeView1.Nodes;
            var root = nodes.Add("All");
            foreach (var item in items.Select(i => i.Type).Distinct())
            {
                var child = root.Nodes.Add(item);
                foreach (var ch in items.Where(i => i.Type == item))
                {
                    child.Nodes.Add(ch.Name);
                }
            }
            root.ExpandAll();
            btnNext_Click(null, new EventArgs());
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            items.ToList().ForEach(i => { i.Status = null; i.Error = string.Empty; });
            
            List<string> checkedItems = new List<string>();

            foreach(TreeNode type in treeView1.Nodes[0].Nodes)
            {
                foreach (TreeNode item in type.Nodes)
                {
                    //if(item.Checked)
                        checkedItems.Add(item.Text);
                }
            }

            if (checkedItems.Count == 0)
            {
                MessageBox.Show("No copyable objects found in source database");
                return;
            }

            IEnumerable<SqlObject> selectedObjects = items.Join(checkedItems, i => i.Name, j => j, (i, j) => { return i; }).ToList();
            this.Hide();
            CopySchema copy = new CopySchema(selectedObjects.ToList(), transfer);
            copy.DropBeforeCreate = true; // dropAndRecreateObjects.Checked;
            copy.CopyConstraints = true; //copyConstraints.Checked;
            copy.FormOnload();
//                this.Show();
            //else
            //    this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
