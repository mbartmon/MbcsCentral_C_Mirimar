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
using System.Collections;
using MbcsCentral.Email;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmAccountManagement.xaml
    /// </summary>
    public partial class frmAccountManagement : Window
    {
        long clientId = GlobalShared.user.Account.clientId;
        long userId;
        Clients client;
        Accounts[] accts;
        Groups[] grps;
        permission[] perms;
        user_permissions[] userPerms;
        ArrayList Emails;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmAccountManagement()
        {
            InitializeComponent();
        }
        public frmAccountManagement(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAccount frm = new frmAccount();
            frm.AccountId = -1;
            frm.ShowDialog();
            accts = Accounts.loadByClientId(clientId);
            lstAccounts.ItemsSource = null;
            if (accts != null && accts.Length > 0)
            {
                lstAccounts.ItemsSource = accts;
            }
        }

        private void AcctEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstAccounts.SelectedIndex < 0)
            {
                MessageBox.Show("No Account Selected.");
                return;
            }
            Accounts acct = (Accounts)lstAccounts.SelectedItem;
            frmAccount frm = new frmAccount();
            frm.AccountId = acct.accountId;
            frm.ShowDialog();
        }

        private void AcctRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (lstAccounts.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure that you want to remove this account and all of its groups and permissions?", "REMOVE ACCOUNT", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Accounts acct = (Accounts)lstAccounts.SelectedItem;
                    if (acct != null)
                    {
                        if (acct.delete())
                        {
                            //  Delete AD account
                        
                            if (GlobalShared.CloudApp)
                            {

                                System.Diagnostics.Process exProc = new System.Diagnostics.Process();
                                exProc.StartInfo.FileName = "CreateADUser.exe";
                                exProc.StartInfo.Arguments = "DE " + GlobalShared.user.Account.login;
                                exProc.Start();
                            }
                            lstAccounts.SelectedItem = null;
                            MessageBox.Show("Account successfully removed.");
                            accts = Accounts.loadByClientId(clientId);
                            lstAccounts.ItemsSource = accts;
                        }
                    }
                }
            }

        }

        private void formAccountManagement_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            client = Clients.load(clientId);
            accts = Accounts.loadByClientId(clientId);
            MailTags[] mailTags = MailTags.load();
            System.Windows.Data.CollectionViewSource mailTagSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("MailTags")));
            mailTagSource.Source = mailTags;
            Emails = new ArrayList();
            if (accts != null && accts.Length > 0)
            {
                lstAccounts.ItemsSource = accts;
            }
            if (client != null)
            {
                UserGrid.DataContext = client;
                Emails = client.EmailsAsList;
                lstEmails.ItemsSource = Emails;
            }
            grps = Groups.loadByClientId(clientId);
            if (grps != null)
                lstGroups.ItemsSource = grps;
            tabCtrl.SelectedIndex = 0;
        }

        private void formAccountManagement_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            client.EmailsFromList = Emails;
            client.save();    
        }

        private void cmdAbandon_Click(object sender, RoutedEventArgs e)
        {
            UserGrid.DataContext = null;
            client = Clients.load(clientId);
            UserGrid.DataContext = client;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAddGruoup_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            frmGroup frm = new frmGroup(m_ThisApp);
            frm.ShowDialog();
            lstGroups.ItemsSource = null;
            grps = Groups.load();
            if (grps != null)
                lstGroups.ItemsSource = grps;
        }

        private void cmdEditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (lstGroups.SelectedIndex < 0)
            {
                MessageBox.Show("No Group selected for editing.");
                return;
            }
            frmGroup frm = new frmGroup();
            Groups grp = (Groups)lstGroups.SelectedItem;
            frm.GroupId = grp.group_id;
            frm.ShowDialog();
            lstGroups.ItemsSource = null;
            grps = Groups.load();
            if (grps != null)
                lstGroups.ItemsSource = grps;
        }

        private void cmdRemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (lstGroups.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure that you want to remove this group?  If you do all accounts that belong to this group will be changed.", "REMOVE GROUP", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Groups grp = (Groups)lstGroups.SelectedItem;
                    if (grp.delete())
                    {
                        lstGroups.ItemsSource = null;
                        grps = Groups.loadByClientId(clientId);
                        lstGroups.ItemsSource = grps;
                        MessageBox.Show("Group successfully removed");
                    }
                }
            }
        }

        private void lstAccountPerms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccountPerms.SelectedIndex >= 0)
            {
                Mouse.SetCursor(  Cursors.Wait);
                userId = ((Accounts)lstAccountPerms.SelectedItem).accountId;
                grdAccountPerms.ItemsSource = null;
                userPerms = GetPermSets(userId, 0);
                grdAccountPerms.ItemsSource = userPerms;
                Mouse.SetCursor(Cursors.Arrow);
            }
        }

        private void lstGroupPerms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstGroupPerms.SelectedIndex >= 0)
            {
                Mouse.SetCursor(Cursors.Wait);
                userId = ((Groups)lstGroupPerms.SelectedItem).group_id;
                grdGroupPerms.ItemsSource = null;
                userPerms = GetPermSets(userId, 1);
                grdGroupPerms.ItemsSource = userPerms;
                Mouse.SetCursor(Cursors.Arrow);
            }
        }

        private user_permissions[] GetPermSets(long id, int userType)
        {
            // userType 0 = account, 1 = group

            ArrayList list = new ArrayList();            
            //permission[] p = permission.load();
            if (perms != null && perms.Length > 0)
            {
                foreach (permission p in perms)
                {
                    user_permissions up = user_permissions.loadByUserPerm(id, p.permission_id, userType);
                    if (up == null)
                    {
                        up = new user_permissions();
                        up.ClientId = clientId;
                        up.Permission_id = p.permission_id;
                        up.User_id = id;
                        up.UserType = userType;
                        up.PermissionType = p.permission_type;
                    }
                    up.Description = p.permission_description;
                    up.Name = p.permission_name;
                    list.Add(up);
                }
                try
                {
                    return ((user_permissions[])(list.ToArray(typeof(user_permissions))));
                } catch (System.Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private void tabPerms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            perms = permission.load();
            TabControl tab = (TabControl)sender;
            string itm = ((TabItem)tab.SelectedItem).Name;
            switch (itm)
            {
                case "tabAccountPerms":
                    lstAccountPerms.ItemsSource = accts;
                    break;
                case "tabGroupPerms":
                    lstGroupPerms.ItemsSource = grps;
                    break;  
            }
        }

        private void cmdPermsSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } user_permissions[] perms = user_permissions.loadByUser(userId);
            if (perms != null && perms.Length > 0)
            {
                foreach (user_permissions perm in perms)
                {
                    perm.delete(perm.Permissions_id.ToString());
                }
            }
            perms = null;
            for (int i = 0; i < grdAccountPerms.Items.Count; i ++)
            {
                user_permissions p = (user_permissions)grdAccountPerms.Items[i];
                if (p.Access || p.Modify || p.Remove || p.Print)
                {
                    user_permissions up = new user_permissions(0, userId, p.Permission_id, p.Permission_type, p.Access, p.Modify, p.Remove, 0, clientId);
                    up.save();
                }
            }
            grdAccountPerms.ItemsSource = null;
            userPerms = GetPermSets(userId, 0);
            grdAccountPerms.ItemsSource = userPerms;
        }

        private void cmdPermsAbandon_Click(object sender, RoutedEventArgs e)
        {
            grdAccountPerms.ItemsSource = null;
            userPerms = GetPermSets(userId, 0);
            grdAccountPerms.ItemsSource = userPerms;

        }

        private void cmdGroupPermsSave_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            user_permissions[] perms = user_permissions.loadByUser(userId, false, 1);
            if (perms != null && perms.Length > 0)
            {
                foreach (user_permissions perm in perms)
                {
                    perm.delete(perm.Permissions_id.ToString());
                }
            }
            perms = null;
            for (int i = 0; i < grdGroupPerms.Items.Count; i++)
            {
                user_permissions p = (user_permissions)grdGroupPerms.Items[i];
                if (p.Access || p.Modify || p.Remove)
                {
                    user_permissions up = new user_permissions(0, userId, p.Permission_id, p.Permission_type, p.Access, p.Modify, p.Remove, 1, clientId);
                    up.save();
                }
            }
            grdGroupPerms.ItemsSource = null;
            userPerms = GetPermSets(userId, 1);
            grdGroupPerms.ItemsSource = userPerms;

        }

        private void cmdGroupPermsAbandon_Click(object sender, RoutedEventArgs e)
        {
            userPerms = null;
            userPerms = GetPermSets(userId, 1);
            grdGroupPerms.ItemsSource = userPerms;
        }

        private void ctxEmailsAdd_Click(object sender, RoutedEventArgs e)
        {
            cmdAddEmail_Click(sender, e);
        }

        private void ctxEmailsDelete_Click(object sender, RoutedEventArgs e)
        {
            cmdDeleteEmail_Click(sender, e);
        }

        private void cmdAddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (Emails == null)
                Emails = new ArrayList();
            Emails.Add(new ClientEmails());
            lstEmails.ItemsSource = null;
            lstEmails.ItemsSource = Emails;
            lstEmails.SelectedIndex = lstEmails.Items.Count - 1;
        }

        private void cmdDeleteEmail_Click(object sender, RoutedEventArgs e)
        {
            int i = lstEmails.SelectedIndex;
            if (i < 0)
            {
                MessageBox.Show("No Email record selected.");
                return;
            }
            lstEmails.ItemsSource = null;
            Emails.Remove(Emails[i]);
            lstEmails.ItemsSource = Emails;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!MbcsMail.TestEmail.IsEmail(txt.Text))
            {
                MessageBox.Show("Invalid E-Mail address.");
                //txt.Focus();
                e.Handled = true;
            }
        }
    }

    public class PermTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AccountTemplate { get; set; }
        public DataTemplate GroupTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                      DependencyObject container)
        {
            var permSet = item as user_permissions;
            if (permSet == null)
                return base.SelectTemplate(item, container);

            if (permSet.UserType == 1)
            {
                return GroupTemplate;
            }
            return AccountTemplate;
        }
    }

}
