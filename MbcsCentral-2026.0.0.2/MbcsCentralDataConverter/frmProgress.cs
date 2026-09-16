using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MbcsCentral
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        public void write(string s, bool newItem = true)
        {
            int NumberOfItems = lstProgress.ClientSize.Height / lstProgress.ItemHeight;
            int itemNumber = 0;
            if (!newItem)
            {
                itemNumber = lstProgress.Items.Count - 1;
                lstProgress.Items[itemNumber] = s;
            }
            else
            {
                lstProgress.Items.Add( s );
            }
            //f (lstProgress.TopIndex == lstProgress.Items.Count - NumberOfItems - 1)
            {
                //The item at the top when you can just see the bottom item
                lstProgress.TopIndex = lstProgress.Items.Count - NumberOfItems + 1;
            }
            lstProgress.Refresh();
        }
    }
}
