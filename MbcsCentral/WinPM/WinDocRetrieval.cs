using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Controls;

namespace MbcsCentral.WinPM
{
    public static class WinDocRetrieval 
    {
        static OleDbConnection CN = null;
        //static RootList DocList = new RootList();
        static OleDbDataAdapter da;
        static readonly object dummynode = null;
        static TreeView lst;

        static TopCategory top1 = null;
        static TopCategory top2 = null;
        //static SubDocument SubDoc;
        //static SubSubDocument SubSubDoc;
        static SubDocument SubDoc1;
        static SubSubDocument SubSubDoc1;
        static SubDocument SubDoc2;
        static SubSubDocument SubSubDoc2;
        static List<TopCategory> Root = new List<TopCategory>();

        public static void getDocuments(Tenants tenant, TreeView tree,  bool AllInv = false, bool All = false)
        {
            Root.Clear();
            lst = tree;
            object[] parms = {tenant, AllInv, All};
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += GetDocuments;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync(parms);
            }
        }

        private static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                lst.ItemsSource = Root;        
            }

        }
        public static void GetDocuments(object sender, DoWorkEventArgs e)
        {
            object[] parms =  (object[])e.Argument;
            Tenants tenant = (Tenants)parms[0];
            bool AllInv = (bool)parms[1];
            bool All = (bool)parms[2];
            DateTime fromDate = GlobalShared.NULL_DATE;
            Control ctrl = Control.load(GlobalShared.user.Account.clientId);
            if (ctrl.WindocDatabase.Trim().Equals(""))
               return;
            //lst.Items.Clear();
            if (AllInv)
                fromDate = GlobalShared.NULL_DATE;
            else
            {
                try
                {
                    fromDate = tenant.MoveinDate.AddMonths(-6);
                }
                catch (ArgumentOutOfRangeException)
                {
                    fromDate = GlobalShared.NULL_DATE;
                }
            }
            string WindocConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + ctrl.WindocDatabase + ";Mode=Share Deny None";
            int i;
            string dbname = ctrl.WindocDatabase;
            if (dbname == null || dbname.Trim() == "")
                return;
            if (tenant.MoveinDate > GlobalShared.NULL_DATE && !AllInv)
            {
                try
                {
                    fromDate = tenant.MoveinDate.AddMonths(-6);
                }
                catch (ArgumentOutOfRangeException)
                {
                    fromDate = GlobalShared.NULL_DATE;
                }
            }
            else
                fromDate = GlobalShared.NULL_DATE;
            string xsql;
            CN = new OleDbConnection(WindocConnectionString);
            try
            {
                CN.Open();
            }
            catch (OleDbException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,"Could not open connection for documents: " + ex.Message, false);
                return;
            }
            if (All)
            {
                xsql = "SELECT DISTINCT  document_id FROM Documents";
                xsql += " WHERE page_number = 1 AND (tenant_seq = " + tenant.SequenceNumber.ToString();
                xsql += " OR (building = '" + tenant.BuildingNumber + "' AND tenant = '" + tenant.TenantNumber.Trim() + "'))";
                xsql += " OR (invoice LIKE '%" + tenant.BuildingNumber + tenant.TenantNumber.Trim() + "%')";
                //xsql += " ORDER BY group_id_1, description, date DESC";
            }
           else
            {
                xsql = "SELECT DISTINCT document_id FROM Documents";
                xsql += " WHERE page_number = 1 AND tenant_seq = " + tenant.SequenceNumber.ToString();
                xsql += " OR (tenant_seq = 0 AND building = '" + tenant.BuildingNumber + "' AND tenant = '" + tenant.TenantNumber.Trim() + "' AND date >= #" + fromDate.ToString("MM/dd/yyyy") + "#)";
                xsql += " OR (invoice LIKE '%" + tenant.BuildingNumber + tenant.TenantNumber.Trim() + "%' AND date >= #" + fromDate.ToString("MM/dd/yyyy") + "#)";
                //xsql += " ORDER BY group_id_1, description, date DESC";
            }
            da = new OleDbDataAdapter();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = CN;
            cmd.CommandText = "SELECT document_id, group_id_1, group_id_2, description, invoice, vendor, date, tenant_seq " +
                              " FROM Documents" +
                              " WHERE page_number = 1 AND document_id IN (" + xsql + ") ORDER BY group_id_1, description, date DESC";

            DataSet ds = new DataSet("Windoc");
            da.SelectCommand = cmd;
            ds = new DataSet("Windoc");
            try
            {
                da.Fill(ds, "Documents");
            }
            catch (OleDbException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Filling Windoc in Tenants: " + ex.Message, true);
                return;
            }
            catch (SystemException syEx)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Filling Windoc in Tenants: " + syEx.Message, true);
                return;
            }
            try
            {
                CN.Close();
            } catch (OleDbException) {}
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;
            fillDocuments(ds);
        }

        
        //public List<RootList> fillDocuments(DataSet rsd)
        private static void fillDocuments(DataSet rsd)
        {
            string lastGroup = ""; 
            string lastDesc = "";
            DataSet rsg;
            string curDesc = "";
            int p;
            int seq;
            int lastSeq;
            string desc = "";
            string s = "";
            TopCategory t = null;
            TreeViewItem item = null;
            TreeViewItem subItem = null;
            bool catFound = false;
            bool useCat2 = false;

            foreach (DataRow dr in rsd.Tables[0].Rows)
            {

                // Find Root Categories
                #region RootCats
                top1 = Root.Find(x => x.GroupId1 == (int)dr["group_id_1"]);
                top2 = null;
                if (dr["group_id_2"] != DBNull.Value && (int)dr["group_id_2"] > 0)
                {
                    top2 = Root.Find(x => x.GroupId2 == (int)dr["group_id_2"]);
                    if (top2 != null) useCat2 = true;
                }

                if (top1 == null)
                {

                    da.SelectCommand.CommandText = "SELECT name FROM Groups WHERE group_id = " + dr["group_id_1"].ToString();
                    try
                    {
                        rsg = new DataSet();
                        da.Fill(rsg, "Groups");
                        if (rsg != null && rsg.Tables[0].Rows.Count > 0)
                        {
                            top1 = new TopCategory(dr, (string)rsg.Tables[0].Rows[0]["name"]);
                            Root.Add(top1);
                        }
                    }
                    catch (OleDbException) { }
                }
                if (useCat2 && top2 == null)
                {

                    da.SelectCommand.CommandText = "SELECT name FROM Groups WHERE group_id = " + dr["group_id_2"].ToString();
                    try
                    {
                        rsg = new DataSet();
                        da.Fill(rsg, "Groups");
                        if (rsg != null && rsg.Tables[0].Rows.Count > 0)
                        {
                            top2 = new TopCategory(dr, (string)rsg.Tables[0].Rows[0]["name"]);
                            Root.Add(top2);
                        }
                    }
                    catch (OleDbException) { }
                }
                #endregion

                // Tenant Info
                seq = (int)(Convert.IsDBNull(dr["tenant_seq"]) ? 0 : (int)dr["tenant_seq"]);
                if (seq == 0)
                {
                    p = ((string)dr["description"]).IndexOf(':', 0);
                    if (p > 0)
                        curDesc = ((string)dr["description"]).Trim().Substring(0, p);
                    else
                        curDesc = ((string)dr["description"]).Trim();
                }
                if (seq == 0 && !lastDesc.Equals(curDesc))
                {
                    SubDoc1 = new SubDocument(dr, true, curDesc);

                    top1.SubDoc.Add(SubDoc1);
                    if (useCat2)
                    {
                        SubDoc2 = new SubDocument(dr, true, curDesc);
                        top2.SubDoc.Add(SubDoc2);
                    }
                    lastDesc = curDesc;
                }
                desc = dr["description"].ToString();
                p = desc.ToUpper().IndexOf(": CHK", 0, StringComparison.Ordinal);
                if (p == 0)
                    p = desc.ToUpper().IndexOf(":CHK", 0, StringComparison.Ordinal);
                if (p > 0)
                    desc = desc.Substring(0, p);
                s = desc + (((string)dr["invoice"]).Trim() != "" ? ": Inv: " + dr["invoice"] + ": Date: " + dr["date"] : "");
                if (seq == 0)
                {
                    SubSubDoc1 = new SubSubDocument(dr, s);
                    SubDoc1.SubSubDocs.Add(SubSubDoc1);
                    if (useCat2)
                    {
                        SubSubDoc2 = new SubSubDocument(dr, s);
                        SubDoc2.SubSubDocs.Add(SubSubDoc2);

                    }
                }
                else
                {
                    SubDoc1 = new SubDocument(dr, false, s);
                    if (top1 != null) top1.SubDoc.Add(SubDoc1);
                    if (useCat2)
                    {
                        SubDoc2 = new SubDocument(dr, false, s);
                        top2.SubDoc.Add(SubDoc2);

                    }
                }

                useCat2 = false;

                /*

                // Find Root Categories
                #region RootCats
                TopCategory top = Root.Find(x => x.GroupId1 == (int)dr["group_id_1"]);

                //if (!lastGroup.Equals(dr["group_id_1"].ToString())) 
                if (top != null)
                    TopCat = top;
                else
                {

                    da.SelectCommand.CommandText = "SELECT name FROM Groups WHERE group_id = " + dr["group_id_1"].ToString();
                    try
                    {
                        rsg = new DataSet();
                        da.Fill(rsg,"Groups");
                        if (rsg != null && rsg.Tables[0].Rows.Count > 0)
                        {
                            TopCat = new TopCategory(dr,(string)rsg.Tables[0].Rows[0]["name"]);
                            Root.Add(TopCat);                            
                        }
                    }
                    catch (OleDbException){}
                    lastGroup = dr["group_id_1"].ToString();
                }
                #endregion

                // Tenant Info
                #region TenantInfo
                seq = (int)(Convert.IsDBNull(dr["tenant_seq"]) ? 0 : (int)dr["tenant_seq"]);
                if (seq == 0)
                {
                    p = ((string)dr["description"]).IndexOf(':',0);
                    if (p > 0) 
                        curDesc = ((string)dr["description"]).Trim().Substring(0, p);
                    else
                        curDesc = ((string)dr["description"]).Trim();
                }
                if (seq == 0 && !lastDesc.Equals(curDesc))
                {
                    SubDoc = new SubDocument(dr, true, curDesc);
                    
                    TopCat.SubDoc.Add(SubDoc);
                    lastDesc = curDesc;
                }
                desc = dr["description"].ToString();                   
                p = desc.ToUpper().IndexOf(": CHK", 0, StringComparison.Ordinal);
                if (p == 0)
                    p = desc.ToUpper().IndexOf(":CHK", 0, StringComparison.Ordinal);
                if (p > 0)
                    desc = desc.Substring(0, p );
                s = desc + (((string)dr["invoice"]).Trim() != "" ? ": Inv: " + dr["invoice"] + ": Date: " + dr["date"] : "");
                if (seq == 0)
                {
                    SubSubDoc = new SubSubDocument(dr, s);
                    SubDoc.SubSubDocs.Add(SubSubDoc);
                }
                else
                {
                    SubDoc = new SubDocument(dr, false, s);
                    TopCat.SubDoc.Add(SubDoc);
                }
                */
            }
        }
    }

    public class RetrieveDocument : WinDoc.Documents
    {
        public RetrieveDocument() : base(new OleDbConnection())
        {
        }
        public string DisplayName { get; set; }
        public WinDoc.Documents Document { get; set; }
        public bool IsRoot { get; set; }
    }
    public class TopCategory : RetrieveDocument
    {
        public ObservableCollection<SubDocument> SubDoc {get; set;}


        public TopCategory(DataRow dr, string groupName)
        {
            DocumentId = (int)dr["document_id"];
            GroupId1 = (int)dr["group_id_1"];
            if (dr["group_id_2"] != DBNull.Value)
                GroupId2 = (int)dr["group_id_2"];
            Description = (string)dr["description"];
            Invoice = (string)dr["invoice"];
            Vendor = (string)dr["vendor"];
            Date = (System.DateTime)dr["date"];
            TenantSeq = (dr["tenant_seq"] is  System.DBNull ? 0 : (int)dr["tenant_seq"]);
            DisplayName = groupName;
            SubDoc = new ObservableCollection<SubDocument>();
            IsRoot = true;
        }
    }

    public class SubDocument : RetrieveDocument
    {
        public ObservableCollection<SubSubDocument> SubSubDocs {get; set;}
   
        public SubDocument(DataRow dr, bool isRoot, string groupName)
        {
            DocumentId = (int)dr["document_id"];
            GroupId1 = (int)dr["group_id_1"];
            if (dr["group_id_2"] != DBNull.Value)
                GroupId2 = (int)dr["group_id_2"];
            Description = (string)dr["description"];
            Invoice = (string)dr["invoice"];
            Vendor = (string)dr["vendor"];
            Date = (System.DateTime)dr["date"];
            TenantSeq = (dr["tenant_seq"] is System.DBNull ? 0 : (int)dr["tenant_seq"]);
            IsRoot = isRoot;
            DisplayName = groupName;
            SubSubDocs = new ObservableCollection<SubSubDocument>();
        }
    }

    public class SubSubDocument : RetrieveDocument
    {
        public SubSubDocument(DataRow dr, string displayName = "")
        {
            DocumentId = (int)dr["document_id"];
            GroupId1 = (int)dr["group_id_1"];
            if (dr["group_id_2"] != DBNull.Value)
                GroupId2 = (int)dr["group_id_2"];
            Description = (string)dr["description"];
            Invoice = (string)dr["invoice"];
            Vendor = (string)dr["vendor"];
            Date = (System.DateTime)dr["date"];
            TenantSeq = (dr["tenant_seq"] is System.DBNull ? 0 : (int)dr["tenant_seq"]);
            if (displayName == "")
                DisplayName = (string)dr["description"];
            else
                DisplayName = displayName;
        }
    }

    /* NOT REFERENCED
    public class TheDocument : WinDoc.Documents
    {
        public TheDocument() : base(new OleDbConnection())
        { }
        public long DocId {get; set;}
        public int GroupId {get;set;}
        public string Description {get; set;}
        public string Invoice{get; set;}
        public long Vendor{get; set;}
        public DateTime Date{get; set;}
        public long Sequence{ get; set; }
        public bool IsCategory {get; set;}
    }
    */
}
