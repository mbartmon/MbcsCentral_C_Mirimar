using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL
{
    class JournalReportsTable :clsDB
    {

        static string  xsql = "";

        public JournalReportsTable()
        {
        }

        public static JournalReportsTable[] load(string JournalName, int Company, int Department, DateTime StartDate, DateTime EndDate, bool Consolidated, bool SourceSearch)
        {
            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            JournalReportsTable obj = new JournalReportsTable();

            xsql = "SELECT gl_transactions.Date, gl_accounts.Description as AccountDescription, gl_transactions.Amount, gl_transactions.Description as TransactionDescription, gl_transactions.CheckNumber, gl_transactions.Source";
            xsql = xsql + ", gl_department.Name AS DepartmentName, gl_transactions.AccountNumber, gl_transactions.DepartmentNumber AS DepartmentNumber FROM gl_transactions, gl_accounts, gl_department WHERE gl_accounts.[Key] = gl_transactions.AccountNumber AND gl_department.[Department Number] = gl_transactions.DepartmentNumber";
            xsql = xsql + " AND gl_transactions.CompanyNumber = " + Company + " AND gl_transactions.Journal = '" + JournalName.Trim() + "' AND gl_transactions.Date BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
            if (!Consolidated && Department > 0)
            {
                xsql = xsql + " AND gl_transactions.DepartmentNumber = " + Department;
            }
            if (SourceSearch)
            {
                xsql = xsql + " ORDER BY gl_transactions.Source, gl_transactions.DepartmentNumber, gl_transactions.Date"; //, gl_transactions.Amount DESC";
            }
            else
            {
                xsql = xsql + " ORDER BY gl_transactions.DepartmentNumber, gl_transactions.Date, gl_transactions.Amount DESC";
            }

            ds = obj.load(xsql, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                obj = new JournalReportsTable();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (JournalReportsTable[])list.ToArray(typeof(JournalReportsTable));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        protected JournalReportsTable fill (DataRow dr)
        {
            try
            {
                Date = (DateTime)dr["Date"];
                AccountDescription = (string)dr["AccountDescription"];
                Amount = (decimal)dr["Amount"];
                TransactionDescription = (string)dr["TransactionDescription"];
                CheckNumber = (int)dr["CheckNumber"];
                Source = (string)dr["Source"];
                DepartmentName = (string)dr["DepartmentName"];
                AccountNumber = (int)dr["AccountNumber"];
                DepartmentNumber = (short)dr["DepartmentNumber"];
            }
            catch (Exception ex)
            { }
            return this;
        }

        public DateTime Date { get; set; }
        public string AccountDescription { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDescription { get; set; }
        public int CheckNumber { get; set; }
        public string Source { get; set; }
        public string DepartmentName { get; set; }
        public int AccountNumber { get; set; }
        public int DepartmentNumber { get; set; }
    }
}
