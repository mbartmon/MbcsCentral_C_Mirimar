using System;
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

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmGroup.xaml
    /// </summary>
    public partial class frmGroup : Window
    {
        long clientId = GlobalShared.user.Account.clientId;
        Groups grp;

        private long groupId;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmGroup()
        {
            InitializeComponent();
        }
        public frmGroup(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        public long GroupId
        {
            set { groupId = value; }
        }

        private void formGroup_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            if (groupId > 0)
                grp = Groups.load(groupId);
            else
            {
                grp = new Groups();
                grp.clientId = clientId;
            }
            this.DataContext = grp;
            txtName.Focus();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (grp != null)
                grp.save();
            this.Close();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void formGroup_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }
    }
}
