using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.VisualBasic;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmProgress.xaml
    /// </summary>
    public partial class frmProgress : Window
    {
        private string sqlConnectionString = "Data Source=MIKE-PC\\SQLEXPRESSMBCS;Initial Catalog=MbcsCentral;Integrated Security=SSPI;";
        private string accessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=<database>";
        private OleDbConnection oleDb = null;
        private SqlConnection sqlDb = null;
        private long clientId = 1;
        private bool deleteFirst = false;
        private ObservableCollection<ProcessTables> tables;
        public string m_AccessConnectionString;

        public frmProgress()
        {
            InitializeComponent();
        }


        public string AccessConnectionString
        {
            set { m_AccessConnectionString = value; }
        }
        public string SqlConnectionString
        {
            set { sqlConnectionString = value; }
        }
        public long ClientId
        {
            set { clientId = value; }
        }
        public bool DeleteFirst
        {
            set { deleteFirst = value; }
        }
        public ObservableCollection<ProcessTables> Tables
        {
            set { tables = value; }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(importTables));
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void importTables(object obj)
        {
            string sourceTable = "";
            string targetTable = "";
            string converter = "";
            bool identity = false;

            //frm = new frmProgress();
            //frm.Show();
            write("Starting Data Conversion");
            GlobalShared.Log.Log((int)LogClass.logType.Info, "DATA Migration Starting...", false);
            write("========================");
            write("Access Database: " + m_AccessConnectionString);
            write("MS SQL Database:" + sqlConnectionString.Replace("Password=Vietnam65;", ""));
            write("");

            foreach (ProcessTables tbl in tables)
            {
                if (tbl.IsChecked)
                {
                    write("Input Table: " + tbl.SourceTable);
                    write("Output Table: " + tbl.TargetTable);
                    convertTable(tbl.SourceTable, tbl.TargetTable, tbl.Converter, tbl.Identity);
                    sourceTable = "";
                    targetTable = "";
                }
            }
        }

        private void convertTable(string sourceTable, string targetTable, string converter, bool identity)
        {
            long rowCount = 0;

            try
            {
                oleDb = new OleDbConnection(m_AccessConnectionString);
                oleDb.Open();
                write("   Input Table Open");
            }
            catch (OleDbException ex)
            {
                write("   Input Table: " + ex.Message);
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable ("+ sourceTable + "): " + ex.Message, false);
               return;
            }
            catch (Exception ex)
            {
                write("   Input Table: " + ex.Message);
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + sourceTable + "): " + ex.Message, false);
                return;
            }

            try
            {
                sqlDb = new SqlConnection(sqlConnectionString);
                sqlDb.Open();
                write("   Output Table Open");
            }
            catch (SqlException ex)
            {
                write("   Output Table: " + ex.Message);
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                write("   Output Table: " + ex.Message);
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                return;
            }

            Table inTable = new Table(sourceTable, oleDb);
            if (inTable == null)
            {
                write("   Unable to open Input Table");
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable - Unable to open InPut Table (" + sourceTable + ")", false);
                return;
            }
            Table outTable = new Table(targetTable, sqlDb);
            if (outTable == null)
            {
                write("   Unable to open OutputTable");
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable - Unable to open OutputTable (" + targetTable + ")", false);
                return;
            }
            string sql = "SELECT * FROM " + inTable.getName();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, oleDb);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                ds = null;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlDb;
            if (ds != null)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                // Delete first?
                if (deleteFirst)
                {
                    write("   Deleting existing rows.");
                    cmd.CommandText = "DELETE FROM " + outTable.getName(); // + " WHERE clientId = " + clientId.ToString();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                        write("    could not delete existing records. " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        write("    could not delete existing records. " + ex.Message);
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                    }
                }
                else if (outTable.getName().ToLower().Equals("pm_tenantssnapshot"))
                {
                    WinPM.TenantsSnapShot[] tens = WinPM.TenantsSnapShot.load();
                    if (tens != null && tens.Length > 0)
                    {
                        foreach(WinPM.TenantsSnapShot ten in tens)
                        {
                            if (ten.Month == 0 && ten.Year == 0 && ten.LastPaidDate > GlobalShared.NULL_DATE)
                            {
                                ten.Month = ten.LastPaidDate.Month;
                                ten.Year = ten.LastPaidDate.Year;
                                ten.save();
                            }
                        }
                    }
                }

                if (!outTable.getName().ToLower().Equals("pm_tenantssnapshot") || (outTable.getName().ToLower().Equals("pm_tenantssnapshot") && !deleteFirst))
                {
                    if (identity)
                    {
                         cmd.CommandText = "SET IDENTITY_INSERT " + outTable.getName() + " ON";
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            write("   could not SET_INDENTITY ON" + ex.Message);
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                        }
                        catch (Exception ex)
                        {
                            write("   could not SET_INDENTITY ON" + ex.Message);
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable (" + targetTable + "): " + ex.Message, false);
                        }
                    }
                }
                string colGroup = " (";
                string valGroup = " VALUES (";
                write("   Converting Rows");
                /*
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    convertRow(dr, inTable, outTable, ref rowCount, colGroup, valGroup, converter);
                }
                 */
                cmd.CommandType = CommandType.Text;
                long rows = 0;
                sql = "INSERT INTO " + outTable.getName();
                this.write("      columns=<" + colGroup + ">");
                this.write("   " + rowCount.ToString() + " rows transferred.");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    colGroup = " (";
                    valGroup = " (";
                    convertRow(dr, inTable, outTable, ref rowCount, ref colGroup, ref valGroup, converter);
                    if (rows == 0)
                        sql += colGroup + " VALUES ";
                    rows++;
                    if (rows == 10)
                    {
                        try
                        {
                            cmd.CommandText = sql + valGroup;
                            if (cmd.CommandText.EndsWith(", "))
                                cmd.CommandText = cmd.CommandText.Substring(0, sql.Length - 2);
                            cmd.ExecuteNonQuery();
                            write("   " + rowCount.ToString() + " rows transferred.", false);
                        }
                        catch (SqlException ex)
                        {
                            this.write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                            this.write("   " + rowCount.ToString() + " rows transferred.");
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable insert row(" + targetTable + "): " + ex.Message, false);
                        }
                        catch (Exception ex)
                        {
                            write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                            write("   " + rowCount.ToString() + " rows transferred.");
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable insert row(" + targetTable + "): " + ex.Message, false);
                        }
                        sql = "INSERT INTO " + outTable.getName();
                        rows = 0;
                    }
                    else
                    {
                        sql += valGroup + ", ";
                    }
                }
                if (rows > 0)
                {
                    try
                    {
                        if (sql.EndsWith(", "))
                            sql = sql.Substring(0, sql.Length - 2);
                        cmd.CommandText = sql; // +valGroup;
                        cmd.ExecuteNonQuery();
                        write("   " + rowCount.ToString() + " rows transferred.", false);
                    }
                    catch (SqlException ex)
                    {
                        this.write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                        this.write("   " + rowCount.ToString() + " rows transferred.");
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable insert row(" + targetTable + "): " + ex.Message, false);
                    }
                    catch (Exception ex)
                    {
                        write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                        write("   " + rowCount.ToString() + " rows transferred.");
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable insert row(" + targetTable + "): " + ex.Message, false);
                    }
                }

                if (identity)
                {
                    cmd.CommandText = "SET IDENTITY_INSERT " + outTable.getName() + " OFF";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        write("   could not SET_INDENTITY OFF" + ex.Message);
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable identity(" + targetTable + "): " + ex.Message, false);
                    }
                    catch (Exception ex)
                    {
                        write("   could not SET_INDENTITY OFF" + ex.Message);
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable identity(" + targetTable + "): " + ex.Message, false);
                    }
                }

                write("   " + rowCount.ToString() + " rows converted", false);
                ds.Dispose();
                da.Dispose();
                try
                {
                    oleDb.Close();
                    write("   Input Table Closed");
                }
                catch (OleDbException ex)
                {
                    write("   closing Input Table: " + ex.Message);
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable(" + sourceTable + "): " + ex.Message, false);
                }
                if (targetTable.ToLower().Equals("pm_tenants"))
                    MigrateFutureRents(sqlDb);
                if (targetTable.ToLower().Equals("pm_chargecodes"))
                {
                    MbcsCentral.WinPM.Control control = MbcsCentral.WinPM.Control.load(1);
                    if (control != null)
                        if (control.UseWingl && !control.UseExternalWinGL)
                            ChargeCodeGLAccounts(sqlDb);
                }
                if (targetTable.ToLower().Equals("gl_vendors"))
                    OldVendorNumber(sqlDb);
                if (targetTable.ToLower().Equals("gl_vendorcategories"))
                    VendorCategoriesCompany(sqlDb);
                try
                {
                    sqlDb.Close();
                    write("   Output Table Closed");
                }
                catch (SqlException ex)
                {
                    write("   closing Ouput Table: " + ex.Message);
                    return;
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertTable close table(" + targetTable + "): " + ex.Message, false);
                }
            }
        }

        private void convertRow(DataRow dr, Table inTable, Table outTable, ref long rowCount, ref string colGroup, ref string valGroup, string converter)
        {
            columns[] cols = inTable.getCols();
            columns outCol = null;
            string x = "";
            bool converted = false;
            string tableName = outTable.getName();
            string colName = "";

            foreach (columns col in cols)
            {
                outCol = outTable.getColByName(col.getName());
                try
                {
                    colName = outCol.getName();
                }
                catch (NullReferenceException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Table: " + outTable.getName() + " does not have a column named: " + col.getName(), false);
                    continue;
                }

                if (outCol != null)
                {
                    if (tableName.ToLower().Equals("pm_tenantssnapshot") && colName.Equals("ID"))
                        continue;
                    converted = false;
                    colGroup += "[" + outCol.getName() + "],";
                    x = outCol.getDbType().ToString().ToLower();
                    if (converter != null && converter.Length > 0 && converter.Trim() != "")
                    {
                        converted = Convert(converter, col, outTable.getName(), dr);
                    }
                    //if (!converted)
                    //{
                        if (x.Contains("char") || x.Contains("date"))
                            valGroup += "'" + dr[col.getName()].ToString().Replace("'", "").Trim() + "',";
                        else if (x.Contains("bool") || x.Contains("bit"))
                            valGroup += (dr[col.getName()].ToString().ToLower().Contains("true") ? "1," : "0,");
                        else
                        {
                            x = dr[col.getName()].ToString().Replace("'", "").Trim();
                            valGroup += (x == "" || x == null ? "0" : x) + ",";
                        }
                    //}
                }
            }
            if (outTable.getColByName("clientId") != null)
            {
                colGroup += "[clientId],";
                valGroup += clientId.ToString() + ",";
            }

            colGroup = colGroup.Substring(0, colGroup.Length - 1) + ")";
            valGroup = valGroup.Substring(0, valGroup.Length - 1) + ")";
            rowCount++;
        }
 
        
        private void convertRow_orig(DataRow dr, Table inTable, Table outTable, ref long rowCount, string colGroup, string valGroup, string converter)
        {
            columns[] cols = inTable.getCols();
            columns outCol = null;
            string sql = "INSERT INTO " + outTable.getName();
            string x = "";
            bool converted = false;

            foreach (columns col in cols)
            {
                outCol = outTable.getColByName(col.getName());
                if (outCol != null)
                {
                    converted = false;
                    colGroup += "[" + outCol.getName() + "],";
                    x = outCol.getDbType().ToString().ToLower();
                    if (converter != null && converter.Length > 0 && converter.Trim() != "")
                    {
                        converted = Convert(converter, col, outTable.getName(), dr);
                    }
                    if (!converted)
                    {
                        if (x.Contains("char") || x.Contains("date"))
                            valGroup += "'" + dr[col.getName()].ToString().Replace("'", "").Trim() + "',";
                        else if (x.Contains("bool") || x.Contains("bit"))
                            valGroup += (dr[col.getName()].ToString().ToLower().Contains("true") ? "1," : "0,");
                        else
                        {
                            x = dr[col.getName()].ToString().Replace("'", "").Trim();
                            valGroup += (x == "" || x == null ? "0" : x) + ",";
                        }
                    }
                }
            }
            if (outTable.getColByName("clientId") != null)
            {
                colGroup += "[clientId],";
                valGroup += clientId.ToString() + ",";
            }

            colGroup = colGroup.Substring(0, colGroup.Length - 1) + ")";
            valGroup = valGroup.Substring(0, valGroup.Length - 1) + ")";
            sql += colGroup + valGroup;
            if (rowCount == 0)
            {
                this.write("      columns=<" + colGroup + ">");
                this.write("   " + rowCount.ToString() + " rows transferred.");
            }
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlDb;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                rowCount++;
                if (rowCount % 10 == 0)
                    write("   " + rowCount.ToString() + " rows transferred.", false);
            }
            catch (SqlException ex)
            {
                this.write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                this.write("   " + rowCount.ToString() + " rows transferred.");
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convertRow(" + outTable + "): " + ex.Message, false);
            }
            catch (Exception ex)
            {
                write("      Row after " + rowCount.ToString() + ": " + ex.Message);
                write("   " + rowCount.ToString() + " rows transferred.");
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA convert row(" + outTable + "): " + ex.Message, false);
            }
        }

        public static bool Convert(string converter, columns col, string table, DataRow dr)
        {
            DataMigration.ConverterControl target = new DataMigration.ConverterControl();
            switch (table)
            {
                case "gl_Memos":
                    switch (col.getName())
                    {
                        
                        case "VendorNumber":
                            // TODO: REMOVE WHEN RESUMING GL coding
                            /*                    MethodInfo info = typeof(DataMigration.ConverterControl).GetMethod(converter, 
                                                                 BindingFlags.NonPublic | BindingFlags.Instance,
                                                                 null,
                                                                 new[]  {
                                                                     typeof(int).MakeByRefType(),
                                                                     typeof(int).MakeByRefType()
                                                                 },
                                                                 null);
                                               object[] parameters = new object[2];
                                               parameters[0] = (int)dr[col.getName()];
                                               parameters[1] = (int)dr[WinGL.Memos.SQL_COL_BATCH_NUMBER];
                                               info.Invoke(target, parameters);
                                               dr[col.getName()] = (int)parameters[0];
                                               dr[WinGL.Memos.SQL_COL_BATCH_NUMBER] = (int)parameters[1];
                                               */
                            break;
                    }
                    break;
            }
            return true;
        }

        private void MigrateFutureRents(SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM pm_FutureRents";
            cmd.Connection = sqlDb;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }
            MbcsCentral.WinPM.Tenants[] tens = MbcsCentral.WinPM.Tenants.load();
            if (tens != null && tens.Length > 0)
            {
                MbcsCentral.WinPM.FutureRents future = null;
                foreach (MbcsCentral.WinPM.Tenants ten in tens)
                {
                    if (ten.FutureDate1 > GlobalShared.NULL_DATE && ten.FutureRent1 > 0)
                    {
                        future = new MbcsCentral.WinPM.FutureRents();
                        future.TenantSequence = ten.SequenceNumber;
                        future.FutureDate = ten.FutureDate1;
                        future.FutureAmount = ten.FutureRent1;
                        future.FutureLegal = ten.FutureLegal1;
                        future.save();
                    }
                    if (ten.FutureDate2 > GlobalShared.NULL_DATE && ten.FutureRent2 > 0)
                    {
                        future = new MbcsCentral.WinPM.FutureRents();
                        future.TenantSequence = ten.SequenceNumber;
                        future.FutureDate = ten.FutureDate2;
                        future.FutureAmount = ten.FutureRent2;
                        future.FutureLegal = ten.FutureLegal2;
                        future.save();
                    }
                    if (ten.FutureDate3 > GlobalShared.NULL_DATE && ten.FutureRent3 > 0)
                    {
                        future = new MbcsCentral.WinPM.FutureRents();
                        future.TenantSequence = ten.SequenceNumber;
                        future.FutureDate = ten.FutureDate3;
                        future.FutureAmount = ten.FutureRent3;
                        future.FutureLegal = ten.FutureLegal3;
                        future.save();
                    }
                }
            }

        }

        private void ChargeCodeGLAccounts(SqlConnection cn)
        {
            // Gl Account Numbers must be converted to Key from old account number
            MbcsCentral.WinGL.Accounts acct = null;
            MbcsCentral.WinPM.ChargeCodes[] chgs = MbcsCentral.WinPM.ChargeCodes.loads(1);
            if (chgs != null && chgs.Length > 0)
            {
                foreach (MbcsCentral.WinPM.ChargeCodes chg in chgs)
                {
                    try
                    {
                        if (!chg.GLCreditAccount.Trim().Equals("") )
                        {
                            acct = WinGL.Accounts.loadByOldAccount(chg.GLCreditAccount);
                            if (acct != null)
                                chg.GLCreditAccount = acct.Key.ToString();
                        }
                        if (!chg.GLDebitAccount.Trim().Equals(""))
                        {
                            acct = WinGL.Accounts.loadByOldAccount(chg.GLDebitAccount);
                            chg.GLDebitAccount = acct.Key.ToString();
                        }
                        chg.save();
                        acct = null;
                    }
                        catch (SqlException sex)
                    {
                        write("   SQL ERROR converting GL Account Numbers: " + sex.Message);
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA ChargeCodeGLAccounts : " + sex.Message, false);
                    }
                    catch (SystemException ex)
                    {
                        write("   SYSTEM ERROR converting GL Account Numbers: " + ex.Message);
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA ChargeCodeGLAccounts : " + ex.Message, false);
                    }
                }
            }
        }

        void OldVendorNumber(SqlConnection cn)
        {
            MbcsCentral.WinGL.Vendors[] vendors = MbcsCentral.WinGL.Vendors.load();
            if (vendors != null && vendors.Length > 0)
            {
                int number = 0;
                foreach (MbcsCentral.WinGL.Vendors vendor in vendors)
                {

                    if (!int.TryParse(vendor.OldNumber, out number) && !vendor.OldNumber.Trim().Equals(""))
                    {
                        vendor.StateID = vendor.OldNumber;
                        vendor.save();
                    }
                }
            }
        }

        // Convert -1 company numbers to 0
        void VendorCategoriesCompany(SqlConnection cn)
        {
            int companyConversions = 0;
            int journalConversions = 0;

            bool save = false;
            MbcsCentral.WinGL.VendorCategories[] cats = MbcsCentral.WinGL.VendorCategories.load();
            if (cats != null && cats.Length > 0)
            {
                write("      Converting Vendor Categories");
                MbcsCentral.WinGL.Vendors vendor = null;
                foreach (MbcsCentral.WinGL.VendorCategories cat in cats)
                {
                    if (cat.Company == -1)
                    {
                        cat.Company = 0;
                        companyConversions++;
                        save = true;
                    }
                    if (cat.JournalName.Trim().Equals(""))
                    {
                        vendor = MbcsCentral.WinGL.Vendors.load(cat.VendorNumber);
                        if (vendor != null)
                            if (!vendor.Payroll)
                            {
                                cat.JournalName = "PAYMENTS";
                                journalConversions++;
                                save = true;
                            }
                    }
                    if (save)
                        cat.save();
                }
                write("      " + companyConversions.ToString() + " companies converted from -1 to 0");
                write("      " + journalConversions.ToString() + " journalnames converted from blank to 'PAYMENTS'");
            }
        }

        private delegate void writeDelegate(string s, bool newItem);

        private object write(string s, bool newItem = true)
        {
            if (lstProgress.CheckAccess())
            {
                int itemNumber;
                if (newItem)
                    lstProgress.Items.Add(s);
                else
                {
                    itemNumber = lstProgress.Items.Count - 1;
                    lstProgress.Items[itemNumber] = s;
                }
                lstProgress.SelectedIndex = lstProgress.Items.Count - 1;
                lstProgress.ScrollIntoView(lstProgress.Items[lstProgress.Items.Count - 1]);
            }
            else
            {
                try
                {
                    lstProgress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => write(s, newItem)));
                }
                catch (SystemException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "DATA write : " + ex.Message , false);

                }
            }
            return null;

        }

    }
}
