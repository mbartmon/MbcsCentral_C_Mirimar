using Microsoft.Office.Interop.Outlook;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop;
using Microsoft.Win32;
//Imports Email.Net.Common.Configurations
//Imports Email.Net.Pop3
//Imports Email.Net.Common
//Imports Email.Net.Common.Collections
//Imports Email.Net.Smtp
using Chilkat;
using System.Windows.Controls;
using System.ServiceProcess;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop;
using MbcsCentral.WinTrm;
using System.Text;

namespace MbcsCentral.WinTrm
{

    public static class Utils
    {

        static System.Windows.Controls.ListBox lst;
        static BindingClasses.bindAccountEmails[] olContacts;


        public static void getTenants(System.Windows.Forms.TreeView lst)
        {
            WinPM.Building[] blds = null;
            WinPM.Tenants[] tenant = null;

            blds = WinPM.Building.load();

            if ((blds != null))
            {
                string bldNumber = "";
                int i = 0;
                foreach (WinPM.Building bld in blds)
                {
                    TreeNode node = default(TreeNode);
                    if (bld.BuildingNumber != bldNumber)
                    {
                        node = new TreeNode();
                        node.BackColor = System.Drawing.Color.LightBlue;
                        node.Text = bld.BuildingNumber + ": " + bld.Address1.TrimEnd();
                        lst.Nodes.Add(node);
                        i += 1;
                        bldNumber = bld.BuildingNumber;
                        tenant = WinPM.Tenants.load(bldNumber);
                    }
                    if ((tenant != null))
                    {
                        WinPM.Tenants ten = null;
                        foreach (WinPM.Tenants ten_loopVariable in tenant)
                        {
                            ten = ten_loopVariable;
                            node = new TreeNode();
                            node.Text = ten.Apartment + ": " + ten.TenantName.TrimEnd();
                            node.Tag = ten.SequenceNumber;
                            lst.Nodes[i].Nodes.Add(node);
                        }
                        tenant = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets hierachical list of buildings with tenant email data
        /// </summary>
        /// <param name="lst">Treeview list object to populate</param>
        /// <returns>True when successful</returns>
        /// <remarks></remarks>
        public static bool getTenants(System.Windows.Controls.TreeView lst)
        {

            WinPM.Building[] blds = null;
            WinPM.Tenants[] tenant = null;

            blds = WinPM.Building.load();

            if ((blds != null) && blds.Length > 0)
            {
                string bldNumber = "";
                int i = 0;
                System.Windows.Controls.TreeViewItem chnode = null;
                System.Windows.Controls.TreeViewItem node = null;
                foreach (WinPM.Building bld in blds)
                {
                    if (bld.BuildingNumber != bldNumber)
                    {
                        node = new System.Windows.Controls.TreeViewItem();
                        // node.Bac = System.Drawing.Color.LightBlue
                        node.Header = "B:" + bld.BuildingNumber + ": " + bld.Address1.TrimEnd();
                        node.Tag = "B:" + bld.BuildingNumber;
                        lst.Items.Add(node);
                        i += 1;
                        bldNumber = bld.BuildingNumber;
                        tenant = WinPM.Tenants.load(bldNumber);
                    }
                    if ((tenant != null))
                    {
                        WinPM.Tenants ten = null;
                        bool found = false;
                        foreach (WinPM.Tenants ten_loopVariable in tenant)
                        {
                            ten = ten_loopVariable;
                            found = false;
                            try
                            {
                                if (!string.IsNullOrEmpty(ten.Email1.Trim()))
                                {
                                    chnode = new System.Windows.Controls.TreeViewItem();
                                    chnode.Header = ten.Apartment + ": " + ten.TenantName.TrimEnd() + " <" + ten.Email1.Trim() + ">";
                                    chnode.Items.Add(ten);
                                    chnode.Tag = ten.SequenceNumber;
                                    node.Items.Add(chnode);
                                    found = true;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "email1-Tenant# " + ten.BuildingNumber + ten.TenantNumber + ": " + ex.Message, false);
                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(ten.Email2.Trim()))
                                {
                                    chnode = new System.Windows.Controls.TreeViewItem();
                                    chnode.Header = ten.Apartment + ": " + ten.TenantName.TrimEnd() + " <" + ten.Email2.Trim() + ">";
                                    chnode.Items.Add(ten);
                                    chnode.Tag = ten.SequenceNumber;
                                    node.Items.Add(chnode);
                                    found = true;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "email2-Tenant# " + ten.BuildingNumber + ten.TenantNumber + ": " + ex.Message, found);
                            }
                            if (!found)
                            {
                                chnode = new System.Windows.Controls.TreeViewItem();
                                chnode.Header = ten.Apartment + ": " + ten.TenantName.TrimEnd() + " <* NO EMAIL *>";
                                chnode.Items.Add(ten);
                                chnode.Tag = ten.SequenceNumber;
                                node.Items.Add(chnode);
                            }
                        }
                    }
                }
            }
            return true;

        }


        public static void getListOfMessages(System.Windows.Forms.TreeView lst, long acctId, int emailby)
        {
            Accounts acct = null;
            WinPM.Tenants tenant = null;
            DataTable dt = new DataTable();
            DataColumn col = default(DataColumn);
            col = new DataColumn("issueId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("accountId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("tenantId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("accountName", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("buildingNumber", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("tenantNumber", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("apartment", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("fromAddr", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("toAddr", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("subject", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("recDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add(col);
            col = new DataColumn("filename", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("message", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            DataRow dr = default(DataRow);
            string[] tokes = null;
            EmailMessage[] msg = StringUtils.getEmailMessages(true, 0, acctId, true);
            foreach (EmailMessage m in msg)
            {
                StringUtils.parseMailToken(m.subject, ref tokes);
                dr = dt.NewRow();
                if (tokes.Length == 3)
                {
                    dr["issueId"] = Convert.ToInt32(tokes[0]);
                    dr["accountId"] = Convert.ToInt32(tokes[1]);
                    acct = Accounts.load(Convert.ToInt32(tokes[1]));
                    if ((acct != null))
                    {
                        dr["accountName"] = acct.Name;
                    }
                    else
                    {
                        dr["accountName"] = "**********";
                    }
                    tenant = MbcsCentral.WinPM.Tenants.load(Convert.ToInt32(tokes[2]));
                    if ((tenant != null))
                    {
                        dr["buildingNumber"] = tenant.BuildingNumber;
                        dr["tenantNumber"] = tenant.TenantNumber;
                        dr["apartment"] = tenant.Apartment;
                    }
                    else
                    {
                        dr["buildingNumber"] = "***";
                        dr["tenantNumber"] = "****";
                        dr["apartment"] = "***";
                    }
                }
                dr["fromAddr"] = m.fromAddr;
                dr["toAddr"] = m.toAddr;
                dr["subject"] = m.subject;
                dr["recDate"] = m.receiveDate;
                dr["filename"] = m.filename;
                dr["message"] = m.message;
                dt.Rows.Add(dr);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = (emailby == 0 ? "issueId desc" : "accountId desc");

            lst.Nodes.Clear();
            TreeNode tNode0 = new TreeNode();
            tNode0.BackColor = System.Drawing.Color.Black;
            tNode0.ForeColor = System.Drawing.Color.White;
            tNode0.Text = "MESSAGES";
            lst.Nodes.Add(tNode0);
            TreeNode tNode1 = new TreeNode();
            tNode1.Text = "In";
            lst.Nodes.Add(tNode1);
            TreeNode dnode = default(TreeNode);
            for (Int16 i = 0; i <= dv.Count - 1; i++)
            {
                dnode = new TreeNode();
                dr = dv[i].Row;
                try
                {
                    dnode.Text = string.Format("{0,5}:{1,-28}: ({2,-3}-{3,-4} {4,-4}) {5} {6}", dr["issueId"], dr["accountName"], dr["buildingNumber"], dr["tenantNumber"], dr["apartment"], dr["subject"], dr["recDate"].ToString());
                }
                catch (System.Exception ex)
                {
                }
                dnode.Tag = dr["filename"];
                dnode.ToolTipText = (string)dr["message"];
                lst.Nodes[1].Nodes.Add(dnode);
            }
            TreeNode tnode2 = new TreeNode();
            tnode2.Text = "Out";
            lst.Nodes.Add(tnode2);
            msg = StringUtils.getEmailMessages(false, 0, acctId, true);
            dt.Clear();
            foreach (EmailMessage m in msg)
            {
                StringUtils.parseMailToken(m.subject, ref tokes);
                dr = dt.NewRow();
                if (tokes.Length == 3)
                {
                    dr["issueId"] = Convert.ToInt32(tokes[0]);
                    dr["accountId"] = Convert.ToInt64(tokes[1]);
                    acct = Accounts.load(Convert.ToInt32(tokes[1]));
                    if ((acct != null))
                    {
                        dr["accountName"] = acct.Name;
                    }
                    else
                    {
                        dr["accountName"] = "**********";

                        tenant = MbcsCentral.WinPM.Tenants.load(Convert.ToInt32(tokes[2]));
                        if ((tenant != null))
                        {
                            dr["buildingNumber"] = tenant.BuildingNumber;
                            dr["tenantNumber"] = tenant.TenantNumber;
                            dr["apartment"] = tenant.Apartment;
                        }
                        else
                        {
                            dr["buildingNumber"] = "***";
                            dr["tenantNumber"] = "****";
                            dr["apartment"] = "***";
                        }
                    }
                    dr["fromAddr"] = m.fromAddr;
                    dr["toAddr"] = m.toAddr;
                    dr["subject"] = m.subject;
                    dr["recDate"] = m.receiveDate;
                    dr["filename"] = m.filename;
                    dr["message"] = m.message;
                    dt.Rows.Add(dr);
                }
                dv = dt.DefaultView;
                dv.Sort = (emailby == 0 ? "issueId desc" : "accountId desc");
                for (Int16 i = 0; i <= dv.Count - 1; i++)
                {
                    dnode = new TreeNode();
                    dr = dv[i].Row;
                    dnode.Text = string.Format("{0,5}:{1,-28}: ({2,-3}-{3,-4} {4,-4}) {5} {6}", dr["issueId"], dr["accountName"], dr["buildingNumber"], dr["tenantNumber"], dr["apartment"], dr["subject"], dr["recDate"].ToString());
                    dnode.Tag = dr["filename"];
                    dnode.ToolTipText = (string)dr["message"];
                    lst.Nodes[2].Nodes.Add(dnode);
                }

            }
        }

        /*
        public static void getListOfMessages(System.Windows.Controls.TreeView lst, long acctId, int emailby)
        {
            Accounts acct = null;
            MbcsCentral.WinPM.Tenants tenant = null;
            IssueTenants[] issues = null;
            DataTable dt = new DataTable();
            DataColumn col = default(DataColumn);
            col = new DataColumn("issueId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("accountId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("tenantId", System.Type.GetType("System.Int32"));
            dt.Columns.Add(col);
            col = new DataColumn("accountName", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("buildingNumber", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("tenantNumber", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("apartment", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("fromAddr", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("toAddr", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("subject", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("recDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add(col);
            col = new DataColumn("filename", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            col = new DataColumn("message", System.Type.GetType("System.String"));
            dt.Columns.Add(col);
            dr = null;
            string[] tokes = null;
            EmailMessage[] msg = StringUtils.getEmailMessages(true, 0, acctId, true);
            foreach (EmailMessage m in msg) {
                StringUtils.parseMailToken(m.subject, ref tokes);
                dr = dt.NewRow();
                if (tokes.Length == 3) {
                    dr["issueId"] = Convert.ToInt64(tokes[0]);
                    dr["accountId"] = Convert.ToInt64(tokes[1]);
                    acct = Accounts.load(Convert.ToInt32(tokes[1]));
                    tenant = MbcsCentral.WinPM.Tenants.load(Convert.ToInt32(tokes[2]));
                } else {
                    tokes = StringUtils.parseFilename(m.filename);
                    if (tokes.Length > 1) {
                        dr["issueId"] = Convert.ToInt64(tokes[1]);
                        dr["accountId"] = Convert.ToInt64(tokes[0]);
                        acct = Accounts.load(Convert.ToInt32(tokes[0]));
                        issues = IssueTenants.loadByNote(Convert.ToInt32(tokes[1]));
                        if ((issues != null)) {
                            try {
                                    tenant = MbcsCentral.WinPM.Tenants.load(issues[0].getSequenceNumber());
                            } catch {
                            }
                        }
                    }
                }
                if ((acct != null)) {
                    dr["accountName"] = acct.getName();
                } else {
                    dr["accountName"] = "**********";
                }
                if ((tenant != null)) {
                    dr["buildingNumber"] = tenant.getBuildingNumber();
                    dr["tenantNumber"] = tenant.getTenantNumber();
                    dr["apartment"] = tenant.getApartment();
                } else {
                    dr["buildingNumber"] = "***";
                    dr["tenantNumber"] = "****";
                    dr["apartment"] = "***";
                }
                dr["fromAddr"] = m.fromAddr;
                dr["toAddr"] = m.toAddr;
                dr["subject"] = m.subject;
                dr["recDate"] = m.receiveDate;
                dr["filename"] = m.filename;
                dr["message"] = m.message;
                dt.Rows.Add(dr);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = (emailby == 0 ? "issueId desc" : "accountId desc");

            lst.Items.Clear();
            TreeViewItem tNode1 = new TreeViewItem();
            tNode1.Header = "In";
            lst.Items.Add(tNode1);
            TreeViewItem dnode = null;
            for (Int16 i = 0; i <= dv.Count - 1; i++) {
                dnode = new TreeViewItem();
                dr = dv[i].Row;
                try {
                    dnode.Header = string.Format("{0,5}:{1,-28}: ({2,-3}-{3,-4} {4,-4}) {5} {6}", dr["issueId"], dr["accountName"], dr["buildingNumber"], dr["tenantNumber"], dr["apartment"], dr["subject"], dr["recDate"].ToString());
                } catch (System.Exception ex) {
                }
                dnode.Tag = dr["filename"];
                dnode.ToolTip = dr["message"];
                tNode1.Items.Add(dnode);
            }
            TreeViewItem tnode2 = new TreeViewItem();
            tnode2.Header = "Out";
            lst.Items.Add(tnode2);
            msg = StringUtils.getEmailMessages(false, 0, acctId, true);
            dt.Clear();
            foreach (EmailMessage m in msg) {
                StringUtils.parseMailToken(m.subject, ref tokes);
                dr = dt.NewRow;
                if (tokes.Length == 3) {
                    dr["issueId"] = Convert.ToInt64(tokes[0]);
                    dr["accountId"] = Convert.ToInt64(tokes[1]);
                    acct = Accounts.load(Convert.ToInt64(tokes[1]));
                    tenant = Tenants.load(Convert.ToInt64(tokes[2]));
                } else {
                    tokes = StringUtils.parseFilename(m.filename);
                    if (tokes.Length > 1) {
                        dr["issueId"] = Convert.ToInt64(tokes[1]);
                        dr["accountId"] = Convert.ToInt64(tokes[0]);
                        acct = Accounts.load(Convert.ToInt64(tokes[0]));
                        issues = IssueTenants.loadByNote(Convert.ToInt64(tokes[1]));
                        if ((issues != null)) {
                            try {
                                tenant = Tenants.load(issues[0].getSequenceNumber());
                            } catch {
                            }
                        }
                    }
                }
                if ((acct != null)) {
                    dr["accountName"] = acct.getName();
                } else {
                    dr["accountName"] = "**********";
                }
                if ((tenant != null)) {
                    dr["buildingNumber"] = tenant.getBuildingNumber();
                    dr["tenantNumber"] = tenant.getTenantNumber();
                    dr["apartment"] = tenant.getApartment();
                } else {
                    dr["buildingNumber"] = "***";
                    dr["tenantNumber"] = "****";
                    dr["apartment"] = "***";
                }
                dr["fromAddr"] = m.fromAddr;
                dr["toAddr"] = m.toAddr;
                dr["subject"] = m.subject;
                dr["recDate"] = m.receiveDate;
                dr["filename"] = m.filename;
                dr["message"] = m.message;
                dt.Rows.Add(dr);
            }
            dv = dt.DefaultView;
            dv.Sort = (emailby == 0 ? "issueId desc" : "accountId desc");
            for (Int16 i = 0; i <= dv.Count - 1; i++) {
                dnode = new TreeViewItem();
                dr = dv[i].Row;
                dnode.Header = string.Format("{0,5}:{1,-28}: ({2,-3}-{3,-4} {4,-4}) {5} {6}", dr["issueId"], dr["accountName"], dr["buildingNumber"], dr["tenantNumber"], dr["apartment"], dr["subject"], dr["recDate"].ToString());
                dnode.Tag = dr["filename"];
                dnode.ToolTip = dr["message"];
                tnode2.Items.Add(dnode);
            }

        }

        public static void getAllNotes(System.Windows.Forms.TreeView lst, ref int field = ShowBy.ctxNotesShowEntered, bool vAll = false)
        {
            Issues[] issue = null;

            if (vAll) {
                issue = Issues.load(field);
            } else {
                issue = Issues.load(GlobalShared.user.getAccountId(), field);
            }
            //Select Case field
            //   Case 0
            //issue = Issues.load
            //   Case Else
            //End Select
            if ((issue != null)) {
                lst.Nodes.Clear();
                TreeNode dnode = new TreeNode {
                    BackColor = System.Drawing.Color.Black,
                    ForeColor = System.Drawing.Color.White,
                    Text = "ISSUES by " + GlobalShared.issuesBy[field]
                };
                lst.Nodes.Add(dnode);
                System.DateTime dt = default(System.DateTime);
                string person = "";
                string temp = null;
                Issues oneIssue = null;
                Accounts account = null;
                int i = 0;
                Tenants tenant = null;
                string tenNumber = "";
                foreach (Issues oneIssue_loopVariable in issue) {
                    oneIssue = oneIssue_loopVariable;
                    switch (field) {
                        case ShowBy.ctxNotesShowEntered:
                            if (Strings.Format(oneIssue.getTimestamp().Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                                dnode = new TreeNode();
                                dt = oneIssue.getTimestamp();
                                dnode.Text = Strings.Format(dt.Date, "MM/dd/yyyy");
                                lst.Nodes.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowDue:
                            if (Strings.Format(oneIssue.duedate.Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                                dnode = new TreeNode();
                                dt = oneIssue.getTimestamp();
                                dnode.Text = Strings.Format(dt.Date, "MM/dd/yyyy");
                                lst.Nodes.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowCreated:
                            account = Accounts.load(oneIssue.accountId);
                            if ((account != null)) {
                                temp = account.getName();
                            } else {
                                temp = "<NO NAME>";
                            }
                            if (!person.Equals(temp)) {
                                dnode = new TreeNode();
                                person = temp;
                                dnode.Text = person;
                                lst.Nodes.Add(dnode);
                                i += 1;
                            }
                            break;
                        case ShowBy.ctxNotesShowResponsible:
                            account = Accounts.load(oneIssue.getPersonResponsible());
                            if ((account != null)) {
                                temp = account.getName();
                            } else {
                                temp = "<NO NAME>";
                            }
                            if (!person.Equals(temp)) {
                                dnode = new TreeNode();
                                person = temp;
                                dnode.Text = person;
                                lst.Nodes.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowCategory:
                            if (oneIssue.category != temp) {
                                dnode = new TreeNode();
                                temp = oneIssue.getCategory();
                                dnode.Text = temp;
                                lst.Nodes.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                    }
                    tenNumber = "";
                    IssueTenants[] tens = IssueTenants.loadByNote(oneIssue.getNoteId());
                    if ((tens != null)) {
                        if (tens.Length > 0) {
                            tenant = Tenants.load(tens[0].getSequenceNumber());
                            if ((tenant != null)) {
                                tenNumber = tenant.getBuildingNumber() + "/" + tenant.getApartment().Trim() + (tens.Length > 1 ? " & OTHERS" : "").ToString();
                            } else {
                                tenNumber = "";
                            }
                        }
                    }
                    dnode = new TreeNode();
                    dnode.Text = tenNumber + ": " + oneIssue.getSubject() + (account == null ? "" : " <" + account.getName() + ">").ToString();
                    dnode.Tag = oneIssue.getNoteId();
                    lst.Nodes[i].Nodes.Add(dnode);
                }
            }

        }


        public static void getAllNotes(System.Windows.Controls.TreeView lst, ref int field = ShowBy.ctxNotesShowEntered, bool vAll = false)
        {
            Issues[] issue = null;

            GlobalShared.Log.Log(LogClass.logType.Info, "About to retrieve all issues.");
            if (vAll) {
                issue = Issues.load(field);
            } else {
                issue = Issues.load(GlobalShared.user.getAccountId(), field);
            }
            GlobalShared.Log.Log(LogClass.logType.Info, issue.Length + " issues retrieved.");
            //Select Case field
            //   Case 0
            //issue = Issues.load
            //   Case Else
            //End Select
            if ((issue != null)) {
                lst.Items.Clear();
                //Dim dnode As New TreeNode() With {.BackColor = System.Drawing.Color.Black, .ForeColor = System.Drawing.Color.White, .Text = "ISSUES by " & issuesBy(field)}
                //lst.Nodes.Add(dnode)
                System.DateTime dt = default(System.DateTime);
                string person = "";
                string temp = null;
                Issues oneIssue = null;
                Accounts account = null;
                int i = 0;
                string tenNumber = "";
                IssueTenants[] tens = null;
                TreeViewItem dnode = null;
                TreeViewItem chnode = null;
                foreach (Issues oneIssue_loopVariable in issue) {
                    oneIssue = oneIssue_loopVariable;
                    if (oneIssue.category.Trim().Equals(""))
                        oneIssue.category = "*";
                    switch (field) {
                        case ShowBy.ctxNotesShowEntered:
                            if (Strings.Format(oneIssue.getTimestamp().Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                                dnode = new TreeViewItem();
                                dt = oneIssue.getTimestamp();
                                dnode.Header = Strings.Format(dt.Date, "MM/dd/yyyy");
                                lst.Items.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowDue:
                            if (Strings.Format(oneIssue.duedate.Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                                dnode = new TreeViewItem();
                                dt = oneIssue.getTimestamp();
                                dnode.Header = Strings.Format(dt.Date, "MM/dd/yyyy");
                                lst.Items.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowCreated:
                            account = Accounts.load(oneIssue.accountId);
                            if ((account != null)) {
                                temp = account.getName();
                            } else {
                                temp = "<NO NAME>";
                            }
                            if (!person.Equals(temp)) {
                                dnode = new TreeViewItem();
                                person = temp;
                                dnode.Header = person;
                                lst.Items.Add(dnode);
                                i += 1;
                            }
                            break;
                        case ShowBy.ctxNotesShowResponsible:
                            account = Accounts.load(oneIssue.getPersonResponsible());
                            if ((account != null)) {
                                temp = account.getName();
                            } else {
                                temp = "<NO NAME>";
                            }
                            if (!person.Equals(temp)) {
                                dnode = new TreeViewItem();
                                person = temp;
                                dnode.Header = person;
                                lst.Items.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                        case ShowBy.ctxNotesShowCategory:
                            if (oneIssue.category != temp) {
                                dnode = new TreeViewItem();
                                temp = oneIssue.getCategory();
                                dnode.Header = temp;
                                lst.Items.Add(dnode);
                                i += 1;
                            }
                            account = Accounts.load(oneIssue.accountId);
                            break;
                    }
                    tens = IssueTenants.loadByNote(oneIssue.getNoteId());
                    tenNumber = "";
                    if ((tens != null)) {
                        if (tens.Length > 0) {
                            if (tens[0].getSequenceNumber() == -1) {
                                //Building
                                tenNumber = "BLD: " + tens[0].buildingNumber + (tens.Length > 1 ? " & OTHERS" : "").ToString();
                            } else {
                                //Tenant
                                tenNumber = tens[0].buildingNumber + "/" + tens[0].apartment.Trim() + (tens.Length > 1 ? " & OTHERS" : "").ToString();
                            }
                        }
                    }
                    chnode = new TreeViewItem();
                    chnode.Header = tenNumber + ": " + oneIssue.getSubject() + (account == null ? "" : " <" + account.getName() + ">").ToString();
                    chnode.Tag = oneIssue.getNoteId();
                    dnode.Items.Add(chnode);
                }
                GlobalShared.Log.Log(LogClass.logType.Info, "Issue list populated.");
            }
        }



        public static void getRecipients(System.Windows.Forms.ListBox lst)
    {
        Accounts[] recips = Accounts.load;

        lst.DataSource = null;
        if ((recips != null)) {
            ArrayList array = new ArrayList();
            Accounts recip = null;
            foreach (Accounts recip_loopVariable in recips) {
                recip = recip_loopVariable;
                array.Add(new ListItem(recip.getName(), recip.getAccountId()));
            }
            lst.DataSource = array;
        }

    }


    public static void getRecipients(System.Windows.Controls.ListBox lst)
    {
        Accounts[] recips = Accounts.load;

        lst.ItemsSource = null;
        if ((recips != null)) {
            ArrayList array = new ArrayList();
            Accounts recip = null;
            foreach (Accounts recip_loopVariable in recips) {
                recip = recip_loopVariable;
                array.Add(new ListItem(recip.getName(), recip.getAccountId()));
            }
            lst.ItemsSource = array;
        }

    }

    public static BindingClasses.bindAccountList[] loadAccountList()
    {

        try {
            ArrayList list = new ArrayList();
            Accounts[] accts = Accounts.load();
            foreach (Accounts acct in accts) {
                list.Add(new BindingClasses.bindAccountList(acct.getAccountId(), acct.getName()));
            }
            return (BindingClasses.bindAccountList[])list.ToArray(typeof(BindingClasses.bindAccountList));
        } catch (SystemException ex) {
            return null;
        }

    }

    public static BindingClasses.bindScheduleList[] loadScheduleList()
    {

        try {
            ArrayList list = new ArrayList();
            schedules[] scheds = schedules.load();
            foreach (schedules sched in scheds) {
                list.Add(new BindingClasses.bindScheduleList(sched.scheduleId, sched.scheduleName));
            }
            return (BindingClasses.bindScheduleList[])list.ToArray(typeof(BindingClasses.bindScheduleList));
        } catch (SystemException ex) {
            return null;
        }

    }

    public static BindingClasses.bindContentList[] loadContentList()
    {

        try {
            ArrayList list = new ArrayList();
            contents[] conts = contents.load();
            foreach (contents cont in conts) {
                list.Add(new BindingClasses.bindContentList(cont.getcontent_id(), cont.getDescription()));
            }
            return (BindingClasses.bindContentList[])list.ToArray(typeof(BindingClasses.bindContentList));
        } catch (SystemException ex) {
            return null;
        }
    }

    public static BindingClasses.bindStatus[] loadStatusList()
    {
        try {
            ArrayList list = new ArrayList();
            Int32 i = 0;
            foreach (string c in MessageSchedule.statusDesc) {
                list.Add(new BindingClasses.bindStatus(i, c));
                i += 1;
            }
            return (BindingClasses.bindStatus[])list.ToArray(typeof(BindingClasses.bindStatus));
        } catch (SystemException ex) {
            return null;
        }
    }
    */
    public static BindingClasses.bindAccountEmails[] getAccountEmails()
    {

        Accounts[] accts = Accounts.load();
        if ((accts != null) & accts.Length > 0) {
            ArrayList list = new ArrayList();
            string[] parsedName = null;
            foreach (Accounts a in accts) {
                
                try {
                    if (!string.IsNullOrEmpty(a.email.Trim())) {
                        list.Add(new BindingClasses.bindAccountEmails(a.accountId, a.firstName, a.lastName + " : Email", "E", a.email));
                    }
                } catch (System.Exception ex) {
                    MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "email-account " + a.Name + ": " + ex.Message, false);
                }
                try {
                    if (!string.IsNullOrEmpty(a.sms.Trim())) {
                        list.Add(new BindingClasses.bindAccountEmails(a.accountId, a.firstName, a.lastName + " : SMS", "S", a.email));
                    }
                } catch (System.Exception ex) {
                    MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "email-account " + a.Name + ": " + ex.Message, false);
                }
            }
            return (BindingClasses.bindAccountEmails[])list.ToArray(typeof(BindingClasses.bindAccountEmails));
        } else {
            return null;
        }

    }
        public static void GetOutlookContacts(System.Windows.Controls.ListBox list)
        {
            lst = list;
            object[] parms = null;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += getContacts;
            worker.RunWorkerCompleted += ContactsWorker_completed;
            if (!worker.IsBusy)
                worker.RunWorkerAsync(parms);

        }

        private static void ContactsWorker_completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "getDocuments background task: " + e.Error.Message, true);
            }
            else if (e.Cancelled)
            {
            }
            else
            {
                lst.ItemsSource = olContacts;
            }

        }

        private static void getContacts(object sender, DoWorkEventArgs e)
        {

            //olContacts = WinTrm.Utils.getOutlookContacts();
            olContacts = AllOutlookContacts();
        }


        //public static BindingClasses.bindAccountEmails[] AllOutlookContacts()
        public static BindingClasses.bindAccountEmails[] AllOutlookContacts()
        {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Creating Outlook objects.", false);
            Microsoft.Office.Interop.Outlook._Application objOL = null;
            Microsoft.Office.Interop.Outlook._NameSpace objNS = null;
            Microsoft.Office.Interop.Outlook.Items objItems = null;
            Microsoft.Office.Interop.Outlook.Folder testFolder = null;
            ArrayList list = new ArrayList();
            try
            { 
            objOL = new Microsoft.Office.Interop.Outlook.Application();
            objNS = objOL.Session;
            }
            catch (System.Exception ex)
            {
            }
            //Microsoft.Office.Interop.Outlook.Folder contactsFolder = objNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts) as Microsoft.Office.Interop.Outlook.OlDefaultFolders;

            int cn = 0;
            int ct = 0;
            int cg = 0;
            Microsoft.Office.Interop.Outlook.Folder contactsFolder = objNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts) as Microsoft.Office.Interop.Outlook.Folder;
            Microsoft.Office.Interop.Outlook.AddressLists addrLists = objNS.AddressLists;
            foreach (Microsoft.Office.Interop.Outlook.AddressList addrList in addrLists)
            {
               testFolder = addrList.GetContactsFolder() as Microsoft.Office.Interop.Outlook.Folder;
                if (testFolder != null)
                {
                    try
                    {
                        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Getting all of the contacts from: " + addrList.Name, false);
                        objItems = testFolder.Items;
                        //objItems = objAddressList.Items;
                    }
                    catch (System.Exception ex)
                    {
                        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
                    }
                    cg = objItems.Count;
                    try
                    {
                        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Add contacts to list.", false);
                        Microsoft.Office.Interop.Outlook.ContactItem contact = null;
                        //for (int j = 0; j <= cg - 1; j++)
                        foreach (object item in testFolder.Items)
                        {
                            try
                            {
                                //contact = objItems[j];
                                //For Each contact As Outlook.ContactItem In objItems
                                ct += 1;
                                Microsoft.Office.Interop.Outlook.ContactItem addr = (Microsoft.Office.Interop.Outlook.ContactItem)item;
                                if ((addr.Email1Address != null && !addr.Email1Address.Trim().Equals("")))
                                {
                                    if (!string.IsNullOrEmpty(addr.Email1Address.Trim()))
                                    {
                                        list.Add(new BindingClasses.bindAccountEmails(0, addr.FirstName, addr.LastName, "E", addr.Email1Address));

                                        cn += 1;
                                    }
                                } 
                            }
                            catch (System.Exception ex)
                            {
                                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,  ex.Message, false);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
                    }
                }
            }
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, cg + " total items in folder.  " + cn + " of " + ct + " contacts added.", false);
            try
            {
                list.Sort();
                return (BindingClasses.bindAccountEmails[])list.ToArray(typeof(BindingClasses.bindAccountEmails));
            }
            catch (System.Exception ex)
            {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error executing DirectCast: " + ex.Message, false);
                return null;
            }

        }

        public static BindingClasses.bindAccountEmails[] getOutlookContacts()
    {

        // Get Outlook Contacts
        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Creating Outlook objects.", false);
        Microsoft.Office.Interop.Outlook._Application objOL = null;
        Microsoft.Office.Interop.Outlook._NameSpace objNS = null;
        Microsoft.Office.Interop.Outlook.Items objItems = null;
        Microsoft.Office.Interop.Outlook.MAPIFolder objAddressList = null;
        ArrayList list = new ArrayList();
        try {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Instantiating Outlook objects.", false);
            objOL = new Microsoft.Office.Interop.Outlook.Application();
            objNS = objOL.Session;
        } catch (System.Exception ex) {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
        }
        try {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Getting the Conacts folder.", false);
            objAddressList = objNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
        } catch (System.Exception ex) {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
        }
        try {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Getting all of the contacts.", false);
            objItems = objAddressList.Items;
        } catch (System.Exception ex) {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
        }
        int cn = 0;
        int ct = 0;
        int cg = objItems.Count;
        try {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, "Add contacts to list.", false);
            Microsoft.Office.Interop.Outlook.ContactItem contact = null;
            for (int j = 0; j <= cg - 1; j++) {
                try {
                    contact = objItems[j];
                    //For Each contact As Outlook.ContactItem In objItems
                    ct += 1;
                    if ((contact.Email1Address != null)) {
                        if (!string.IsNullOrEmpty(contact.Email1Address.Trim())) {
                            list.Add(new BindingClasses.bindAccountEmails(0, contact.FirstName, contact.LastName, "E", contact.Email1Address));

                            cn += 1;
                        }
                    }
                } catch (System.Exception ex) {
                    MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, j + ": " + ex.Message, false);
                }
            }
        } catch (System.Exception ex) {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
        }
        MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Config, cg + " total items in folder.  " + cn + " of " + ct + " contacts added.", false);
        try {
            return (BindingClasses.bindAccountEmails[])list.ToArray(typeof(BindingClasses.bindAccountEmails));
        } catch (System.Exception ex) {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error executing DirectCast: " + ex.Message, false);
            return null;
        }

    }

    /*
    public static void saveForm(Form frm)
    {
        Microsoft.Win32.RegistryKey regkey = null;
        regkey = Registry.CurrentUser.CreateSubKey(GlobalShared.formKeyLoc + frm.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
        regkey.SetValue("Top", frm.Top, RegistryValueKind.DWord);
        regkey.SetValue("Left", frm.Left, RegistryValueKind.DWord);
        regkey.SetValue("Width", frm.Width, RegistryValueKind.DWord);
        regkey.SetValue("Height", frm.Height, RegistryValueKind.DWord);

    }


    public static void saveForm(Window frm)
    {
        Microsoft.Win32.RegistryKey regkey = null;
        regkey = Registry.CurrentUser.CreateSubKey(GlobalShared.formKeyLoc + frm.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
        regkey.SetValue("Top", frm.Top, RegistryValueKind.DWord);
        regkey.SetValue("Left", frm.Left, RegistryValueKind.DWord);
        regkey.SetValue("Width", frm.Width, RegistryValueKind.DWord);
        regkey.SetValue("Height", frm.Height, RegistryValueKind.DWord);

    }
    public static System.Drawing.Rectangle getForm(Form frm)
    {

        System.Drawing.Rectangle rect = default(System.Drawing.Rectangle);
        Microsoft.Win32.RegistryKey regKey = null;

        regKey = Registry.CurrentUser.OpenSubKey(GlobalShared.formKeyLoc + frm.Name, false);
        if (((regKey != null))) {
            rect = new System.Drawing.Rectangle(Conversion.Val(regKey.GetValue("Left", frm.Left)), Conversion.Val(regKey.GetValue("Top", frm.Top)), Conversion.Val(regKey.GetValue("Width", frm.Width)), Conversion.Val(regKey.GetValue("Height", frm.Height)));
        } else {
            rect = new System.Drawing.Rectangle(frm.Left, frm.Top, frm.Width, frm.Height);
        }

        return rect;

    }

    public static System.Drawing.Rectangle getForm(Window frm)
    {

        System.Drawing.Rectangle rect = default(System.Drawing.Rectangle);
        Microsoft.Win32.RegistryKey regKey = null;

        regKey = Registry.CurrentUser.OpenSubKey(GlobalShared.formKeyLoc + frm.Name, false);
        if (((regKey != null))) {
            rect = new System.Drawing.Rectangle(Conversion.Val(regKey.GetValue("Left", frm.Left)), Conversion.Val(regKey.GetValue("Top", frm.Top)), Conversion.Val(regKey.GetValue("Width", frm.Width)), Conversion.Val(regKey.GetValue("Height", frm.Height)));
        } else {
            rect = new System.Drawing.Rectangle(frm.Left, frm.Top, frm.Width, frm.Height);
        }

        return rect;

    }


    public static void saveFormW(Window frm)
    {
        RegistryKey regkey = null;
        regkey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\MBCS\\" + GlobalShared.APPNAME + "\\Forms\\" + frm.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
        regkey.SetValue("Top", frm.Top, RegistryValueKind.DWord);
        regkey.SetValue("Left", frm.Left, RegistryValueKind.DWord);
        regkey.SetValue("Width", frm.Width, RegistryValueKind.DWord);
        regkey.SetValue("Height", frm.Height, RegistryValueKind.DWord);
        regkey.SetValue("State", frm.WindowState, RegistryValueKind.DWord);
        regkey.SetValue("FontSize", frm.FontSize);

    }


    public static void getFormW(Window frm)
    {
        RegistryKey regKey = null;

        regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MBCS\\" + GlobalShared.APPNAME + "\\Forms\\" + frm.Name, false);
        if (((regKey != null))) {
            frm.Left = Conversion.Val(regKey.GetValue("Left", frm.Left));
            frm.Top = Conversion.Val(regKey.GetValue("Top", frm.Top));
            frm.Width = Conversion.Val(regKey.GetValue("Width", frm.Width));
            frm.Height = Conversion.Val(regKey.GetValue("Height", frm.Height));
            frm.WindowState = Conversion.Val(regKey.GetValue("State", frm.WindowState));
            int f = Conversion.Val(regKey.GetValue("FontSize", 0));
            if (f > 0) {
                frm.FontSize = f;
            }
        }

    }
    */

    /// <summary>
    /// Parses a full name string into first and last name
    /// </summary>
    /// <param name="fullName">The full name to parse</param>
    /// <returns>A one or two element string array that contains:
    /// 1) A single element when there is only a single name in fullName
    /// 2) The first and middle names in element 0 and the last name in element 1</returns>
    /// 3) If the name ends with recognizable suffix then the first and middle names in element 0 and the last name and suffix in element 1
    /// <remarks></remarks>
    public static string[] parseName(string fullName)
    {

        string[] c = fullName.Split(',');
        string[] s = null;
        string[] x = new string[2];
        if (c.Length > 1)
            {
            s = new string[2];
            s[0] = c[1];
            s[1] = c[0];
        } else {
            s = fullName.Split(' ');
        }
        switch (s.Length) {
            case 0:
                return null;
            case 1:
                x[0] = fullName;
                x[1] = "";
                return x;
            case 2:
                return s;
            default:
                x[0] = s[0];
                x[1] = s[s.Length - 1];
                string z = x[1].ToLower().TrimEnd('.');
                switch (z) {
                    case "jr":
                    case "sr":
                    case "i":
                    case "ii":
                    case "iii":
                    case "md":
                    case "phd":
                    case "esq":
                        x[1] = s[s.Length - 2] + z;
                        break;
                }
                for (int i = 1; i <= s.Length - 2; i++) {
                    x[0] += " " + s[i];
                }

                return x;
        }

    }

/*
    public static Generic.SortedList<string, Chilkat.Email> getSmtpMessages(SmtpConfig smtpAcct)
    {

        string Username = null;
        string Password = null;
        string Host = null;
        int Port = 0;
        string Email = null;
        bool SSL = false;
        bool TSL = false;
        long authenticationType = 0;

        Chilkat.MailMan mailman = new Chilkat.MailMan();
        bool success = mailman.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
        if (!success) {
            Interaction.MsgBox("Could not initialize email component");
            return null;
        }

        if ((smtpAcct != null)) {
            Username = smtpAcct.in_userName;
            Password = smtpAcct.in_password;
            Host = smtpAcct.in_server;
            Port = smtpAcct.in_port;
            Email = smtpAcct.emailAddress;
            SSL = smtpAcct.in_SSL;
            TSL = smtpAcct.in_TSL;
            authenticationType = smtpAcct.in_authentication;
        } else {
            GlobalShared.Log.Log(LogClass.logType.Warning, "No POP configuration found");
            return null;
        }

        mailman.MailHost = Host;
        //TCP port for connection
        mailman.MailPort = Convert.ToUInt16(Port);
        //Username to login to the POP3 server
        mailman.PopUsername = Username;
        //Password to login to the POP3 server
        mailman.PopPassword = Password;
        // SSL Interaction type
        if (TSL) {
            mailman.StartTLS = true;
        } else if (SSL) {
            mailman.PopSsl = true;
        }

        Chilkat.EmailBundle bundle = null;
        // Login to POP server
        GlobalShared.Log.Log(LogClass.logType.Info, "Attempting to retrieve inbox");
        try {
            //bundle = mailman.CopyMail
            bundle = mailman.GetAllHeaders(0);
        } catch (SystemException ex) {
            GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Unable to retrieve messages: " + ex.Message);
            return null;
        }
        if ((bundle == null)) {
            GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error retrieving mail: " + mailman.LastErrorText);
            return null;
        } else {
            GlobalShared.Log.Log(LogClass.logType.Info, "Successfully retrieved mail");
        }
        System.Collections.Generic.SortedList<string, Chilkat.Email> messages = new System.Collections.Generic.SortedList<string, Chilkat.Email>();
        Chilkat.Email msg = null;
        try {
            //Check if messages already received
            for (int i = 0; i <= bundle.MessageCount - 1; i++) {
                try {
                    msg = null;
                    msg = bundle.GetEmail(i);
                } catch (System.Exception ex) {
                    GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Retrieve Other error: " + ex.Message);
                }
                if ((msg != null)) {
                    if (string.IsNullOrEmpty(StringUtils.findIssueTag(msg).Trim())) {
                        if (!StringUtils.searchUID(msg.Uidl.Replace("<", "").Replace(">", ""))) {
                            // No.  Add to list
                            messages.Add(msg.Uidl, msg);
                        }
                    }
                }
            }
            string cnt = Convert.ToString(messages.Count);
            cnt += " email messages were found";
            GlobalShared.Log.Log(LogClass.logType.Info, cnt);
            //Logout from the server
            mailman.Dispose();
        } catch (IO.IOException ex) {
            GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "IO error: " + ex.Message);
        } catch (System.Exception ex) {
            GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Other error: " + ex.Message);
        }
        return messages;

    }

    public static bool deleteMessage(SmtpConfig smtpAcct, Chilkat.Email msg)
    {

        string Username = null;
        string Password = null;
        string Host = null;
        int Port = 0;
        string Email = null;
        bool SSL = false;
        bool TSL = false;

        Chilkat.MailMan mailman = new Chilkat.MailMan();
        bool success = mailman.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
        if (!success) {
            Interaction.MsgBox("Could not initialize email component");
            return null;
        }

        if ((smtpAcct != null)) {
            Username = smtpAcct.in_userName;
            Password = smtpAcct.in_password;
            Host = smtpAcct.in_server;
            Port = smtpAcct.in_port;
            Email = smtpAcct.emailAddress;
            SSL = smtpAcct.in_SSL;
            TSL = smtpAcct.in_TSL;
        } else {
            GlobalShared.Log.Log(LogClass.logType.Warning, "No POP configuration found");
            return null;
        }

        mailman.MailHost = Host;
        //TCP port for connection
        mailman.MailPort = Convert.ToUInt16(Port);
        //Username to login to the POP3 server
        mailman.PopUsername = Username;
        //Password to login to the POP3 server
        mailman.PopPassword = Password;
        // SSL Interaction type
        if (TSL) {
            mailman.StartTLS = true;
        } else if (SSL) {
            mailman.PopSsl = true;
        }

        if (mailman.DeleteByUidl(msg.Uidl)) {
            return true;
        } else {
            MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, mailman.LastErrorText, false);
        }

    }

    #if !IGNORE 
    public static void getAllNotes(issuesObj parmObj)
    {
        //Public Shared Sub getAllNotes(ByVal lst As System.Windows.Controls.TreeView, Optional ByRef field As Integer = ShowBy.ctxNotesShowEntered, Optional vAll As Boolean = False) 

        Issues[] issue = null;

        GlobalShared.Log.Log(LogClass.logType.Info, "About to retrieve all issues.");
        if (parmObj.vAll) {
            issue = Issues.load(parmObj.field);
        } else {
            issue = Issues.load(GlobalShared.user.getAccountId(), parmObj.field);
        }
        GlobalShared.Log.Log(LogClass.logType.Info, issue.Length + " issues retrieved.");
        //Select Case field
        //   Case 0
        //issue = Issues.load
        //   Case Else
        //End Select
        if ((issue != null)) {
            parmObj.lst.Items.Clear();
            //Dim dnode As New TreeNode() With {.BackColor = System.Drawing.Color.Black, .ForeColor = System.Drawing.Color.White, .Text = "ISSUES by " & issuesBy(field)}
            //lst.Nodes.Add(dnode)
            System.DateTime dt = default(System.DateTime);
            string person = "";
            string temp = null;
            Issues oneIssue = null;
            Accounts account = null;
            int i = 0;
            Tenants tenant = null;
            string tenNumber = "";
            IssueTenants[] tens = null;
            TreeViewItem dnode = null;
            TreeViewItem chnode = null;
            foreach (Issues oneIssue_loopVariable in issue) {
                oneIssue = oneIssue_loopVariable;
                if (oneIssue.category.Trim().Equals(""))
                    oneIssue.category = "*";
                switch (parmObj.field) {
                    case ShowBy.ctxNotesShowEntered:
                        if (Strings.Format(oneIssue.getTimestamp().Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                            dnode = new TreeViewItem();
                            dt = oneIssue.getTimestamp();
                            dnode.Header = Strings.Format(dt.Date, "MM/dd/yyyy");
                            parmObj.lst.Items.Add(dnode);
                            i += 1;
                        }
                        account = Accounts.load(oneIssue.accountId);
                        break;
                    case ShowBy.ctxNotesShowDue:
                        if (Strings.Format(oneIssue.duedate.Date, "mm/dd/yyyy") != Strings.Format(dt.Date, "mm/dd/yyyy")) {
                            dnode = new TreeViewItem();
                            dt = oneIssue.getTimestamp();
                            dnode.Header = Strings.Format(dt.Date, "MM/dd/yyyy");
                            lst.Items.Add(dnode);
                            i += 1;
                        }
                        account = Accounts.load(oneIssue.accountId);
                        break;
                    case ShowBy.ctxNotesShowCreated:
                        account = Accounts.load(oneIssue.accountId);
                        if ((account != null)) {
                            temp = account.getName();
                        } else {
                            temp = "<NO NAME>";
                        }
                        if (!person.Equals(temp)) {
                            dnode = new TreeViewItem();
                            person = temp;
                            dnode.Header = person;
                            lst.Items.Add(dnode);
                            i += 1;
                        }
                        break;
                    case ShowBy.ctxNotesShowResponsible:
                        account = Accounts.load(oneIssue.getPersonResponsible());
                        if ((account != null)) {
                            temp = account.getName();
                        } else {
                            temp = "<NO NAME>";
                        }
                        if (!person.Equals(temp)) {
                            dnode = new TreeViewItem();
                            person = temp;
                            dnode.Header = person;
                            lst.Items.Add(dnode);
                            i += 1;
                        }
                        account = Accounts.load(oneIssue.accountId);
                        break;
                    case ShowBy.ctxNotesShowCategory:
                        if (oneIssue.category != temp) {
                            dnode = new TreeViewItem();
                            temp = oneIssue.getCategory();
                            dnode.Header = temp;
                            lst.Items.Add(dnode);
                            i += 1;
                        }
                        account = Accounts.load(oneIssue.accountId);
                        break;
                }
                tens = IssueTenants.loadByNote(oneIssue.getNoteId());
                tenNumber = "";
                if ((tens != null)) {
                    if (tens.Length > 0) {
                        if (tens[0].getSequenceNumber() == -1) {
                            //Building
                            tenNumber = "BLD: " + tens[0].buildingNumber + (tens.Length > 1 ? " & OTHERS" : "").ToString();
                        } else {
                            //Tenant
                            tenNumber = tens[0].buildingNumber + "/" + tens[0].apartment.Trim() + (tens.Length > 1 ? " & OTHERS" : "").ToString();
                        }
                    }
                }
                chnode = new TreeViewItem();
                chnode.Header = tenNumber + ": " + oneIssue.getSubject() + (account == null ? "" : " <" + account.getName() + ">").ToString();
                chnode.Tag = oneIssue.getNoteId();
                dnode.Items.Add(chnode);
            }
            GlobalShared.Log.Log(LogClass.logType.Info, "Issue list populated.");
        }
    }

    #endif

    }
    */
    }
}
