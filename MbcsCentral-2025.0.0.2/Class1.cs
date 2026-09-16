using System;

public class Class1
{
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
        bool useCat2;
        SubDocument SubDoc1;
        SubSubDocument SubSubDoc;
        SubDocument SubDoc2;
        SubSubDocument SubSubDoc;

        foreach (DataRow dr in rsd.Tables[0].Rows)
        {

            // Find Root Categories
            #region RootCats
            TopCategory top1 = Root.Find(x => x.GroupId1 == (int)dr["group_id_1"]);
            TopCategory top2;
            if ((int)dr["group_id_2"] > 0)
            {
                top2 = Root.Find(x => x.GroupId1 == (int)dr["group_id_2"]);
                useCat2 = true;
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
                        TopCat = new TopCategory(dr, (string)rsg.Tables[0].Rows[0]["name"]);
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
                        TopCat = new TopCategory(dr, (string)rsg.Tables[0].Rows[0]["name"]);
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
                    top2.SubDoc.Add(Subdoc2);
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
                top1.SubDoc.Add(SubDoc1);
                if (useCat2)
                {
                    SubDoc2 = new SubDocument(dr, false, s);
                    top2.SubDoc.Add(SubDoc2);

                }
            }
        }
    }
}
