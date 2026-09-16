using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmAccount.xaml
    /// </summary>
    public partial class frmAccount : Window
    {
        private long clientId = GlobalShared.user.Account.clientId;
        private Accounts acct;
        ArrayList members;
        private bool DragInProgress = false;
        private bool IsChanged = false;
        private string OriginalLogin = "";
        private string OriginalPassword = "";
        System.Diagnostics.Process  exProc = new System.Diagnostics.Process();


        private long acctId; // -1 if adding a new account

        public frmAccount()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            // Fill Account data
            if (acctId > 0)
            {
                acct = Accounts.load(acctId);
                OriginalLogin = acct.login;
                OriginalPassword = acct.password;
            }
            else
            {
                acct = new Accounts();
                acct.clientId = clientId;
            }
            if (acct == null)
            {
                MessageBox.Show("Account record not found.");
                this.Close();
            }
            tabAccount.DataContext = acct;

            // Fill Groups List
            Groups[] grps = Groups.loadByClientId(clientId);
            if (grps != null && grps.Length > 0)
                lstGroups.ItemsSource = grps;
            members = GroupMembers.loadByAccountArray(acct.accountId);
            if (members != null && members.Count > 0)
                lstMembership.ItemsSource = members;
            tabCtrl.SelectedIndex = 0;
        }

        public long AccountId
        {
            set { acctId = value; }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Main.saveForm(this);
        }

        private void cmdAddOne_Click(object sender, RoutedEventArgs e)
        {
            if (lstGroups.SelectedIndex >= 0)
            {
               lstMembership_DoDrop((Groups)lstGroups.SelectedItem);
            }
        }

        private void cmdAddAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lstGroups.Items.Count; i++)
            {
                Groups grp = (Groups)lstGroups.Items[i];
                lstMembership_DoDrop(grp);
            }

        }

        private void cmdDelOne_Click(object sender, RoutedEventArgs e)
        {
            if (lstMembership.SelectedIndex >= 0)
            {
                lstGroups_DoDrop((GroupMembers)lstMembership.SelectedItem);
            }

        }

        private void cmdDelAll_Click(object sender, RoutedEventArgs e)
        {
            lstMembership.ItemsSource = null;
            members.Clear();
            lstMembership.ItemsSource = members;
            IsChanged = true;

        }

        private void lstGroups_MouseMove(object sender, MouseEventArgs e)
        {
            if (DragInProgress)
            {
                DragInProgress = false;
                return;
            }
            ListBox lst = sender as ListBox;
            if (lst != null && lst.SelectedIndex >= 0 && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(lst, lst.SelectedItem, DragDropEffects.Copy);
                DragInProgress = true;
            }
        }

        private void lstMembership_DragEnter(object sender, DragEventArgs e)
        {
                if (!e.Data.GetDataPresent(typeof(Groups)) || sender == e.Source)
                {
                    e.Effects = DragDropEffects.None;
                }
        }

        private void lstMembership_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Groups)))
            {
                Groups grp = e.Data.GetData(typeof(Groups)) as Groups;
                lstMembership_DoDrop(grp);
            }
            DragInProgress = false;
        }

        private void lstMembership_DoDrop(Groups grp)
        {
            GroupMembers itm;
            long GrpId;
                try
                {
                    GrpId = grp.group_id;
                }
                catch { GrpId = -1; }

                for (int i = 0; i < members.Count; i++)
                {
                    itm = (GroupMembers)members[i];
                    if (itm.groupId == grp.group_id)
                        return;
                }
                members.Add(new GroupMembers(grp.group_id, acct.accountId, grp.group_name));
                lstMembership.ItemsSource = null;
                lstMembership.ItemsSource = members;
                IsChanged = true;

        }

        private void lstMembership_MouseMove(object sender, MouseEventArgs e)
        {
            if (DragInProgress)
            {
                DragInProgress = false;
                return;
            }
            ListBox lst = sender as ListBox;
            if (lst != null && lst.SelectedIndex >= 0 && e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(lst, lst.Items[lst.SelectedIndex], DragDropEffects.Move);

        }

        private void lstGroups_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GroupMembers)))
            {
                GroupMembers grp = e.Data.GetData(typeof(GroupMembers)) as GroupMembers;
                lstGroups_DoDrop(grp);
            }
            DragInProgress = false;
        }

        private void lstGroups_DoDrop(GroupMembers grp)
        {
            GroupMembers mem;
            try
            {
                for (int i = 0; i < members.Count; i++)
                {
                    mem = (GroupMembers)members[i];
                    if (mem.groupId == grp.groupId)
                        members.RemoveAt(i);
                }
                lstMembership.ItemsSource = null;
                lstMembership.ItemsSource = members;
                IsChanged = true;
            }
            catch (SystemException ex) { }
        }

        private void lstGroups_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Groups)) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsChanged || acct.IsChanged)
            {
                if (acct.IsChanged)
                {
                    acct.save();

                    if (GlobalShared.CloudApp)
                    {
                        exProc = new System.Diagnostics.Process();
                        exProc.StartInfo.FileName = "CreateADUser.exe";

                        if (acctId == -1)  // new account
                        {
                            exProc.StartInfo.Arguments = "CR " +
                                                         GlobalShared.domain + " " +
                                                         GlobalShared.FQN + " " +
                                                         acct.firstName + " " +
                                                         acct.lastName + " " +
                                                         acct.login + " " +
                                                         acct.password + " " +
                                                         GlobalShared.clientName;
                            exProc.EnableRaisingEvents = true;
                            exProc.Exited += new System.EventHandler(Result);
                            exProc.Start();
                        }
                        else
                        {
                            if (!acct.login.ToLower().Equals(OriginalLogin.ToLower())) // existing account, login changed
                            {
                                exProc.StartInfo.Arguments = " EN " +
                                                             OriginalLogin + " " +
                                                             acct.login;

                                exProc.EnableRaisingEvents = true;
                                exProc.Exited += new System.EventHandler(Result);
                                exProc.Start();
                            }
                            if (!acct.password.Equals(OriginalPassword)) // existing account, password changed  
                            {
                                exProc.StartInfo.Arguments = " EP " +
                                                             acct.login + " " +
                                                             OriginalPassword + " " +
                                                             acct.password;

                                exProc.EnableRaisingEvents = true;
                                exProc.Exited += new System.EventHandler(Result);
                                exProc.Start();
                            }
                        }
                    }
                }
                if (IsChanged)
                {
                    string xsql = "DELETE FROM " + GroupMembers.TABLE + " WHERE " +
                                  GroupMembers.SQL_COL_ACCOUNT_ID + " = " + acct.accountId.ToString();
                    clsDB.Query(xsql,GlobalShared.CN);
                    GroupMembers grp = null;
                    for (int i = 0; i < lstMembership.Items.Count; i++)
                    {
                        lstMembership.SelectedIndex = i;
                        grp = (GroupMembers)lstMembership.SelectedItem;
                        GroupMembers grpMem = new GroupMembers(grp.groupId, acct.accountId);
                        grpMem.save();
                    }
                }
            }
            Button btn = (Button)sender;
            if (btn.Name.Equals("cmdExit"))
                return;
            this.Close();
        }
        internal static void Result(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process proc = (System.Diagnostics.Process)sender;
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Create User powerscript exited with code= " + proc.ExitCode, true);
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            if (IsChanged)
            {
                switch (MessageBox.Show("You have made changes.  Do you want to save your changes first?","",MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Cancel:
                        return;
                        break;
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, e);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            this.Close();
        }

        private void ADUser()
        {

        }
    }
}
