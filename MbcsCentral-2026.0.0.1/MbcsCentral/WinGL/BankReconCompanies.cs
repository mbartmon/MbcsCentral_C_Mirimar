using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MbcsCentral.WinGL
{
    public class BankReconCompaniesList
    {
        private List<BankReconCompanies> major;

        public BankReconCompaniesList()
        {
            major = new List<BankReconCompanies>();
        }

        public List<BankReconCompaniesList> Major { get; set; }

        public List<BankReconCompanies> GetReconRecords(string shortName, bool ActiveOnly = false)
        {

            DataTable DT = new DataTable();
            DataColumn dc = new DataColumn("ID", System.Type.GetType("System.Int32"));
            DT.Columns.Add(dc);
            dc = new DataColumn("Company", System.Type.GetType("System.String"));
            DT.Columns.Add(dc);
            dc = new DataColumn("Department", System.Type.GetType("System.String"));
            DT.Columns.Add(dc);
            dc = new DataColumn("Date", System.Type.GetType("System.DateTime"));
            DT.Columns.Add(dc);
            dc = new DataColumn("Completed", System.Type.GetType("System.Boolean"));
            DT.Columns.Add(dc);
            DataTable savedDT = null;
            string[] name;

            Classes[] classes = Classes.loadMultiple(shortName);
            if (classes != null && classes.Length > 0)
            {
                ArrayList classNames = new ArrayList();
                Department dept = null;
                foreach (Classes cls in classes)
                {
                    name = cls.name.Split(':');
                    classNames.Add(new ListItem(cls.name.Replace(shortName + ":", ""), cls.ID, cls.value));
                    if (name.Length == 3)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<BankReconciliation>));
                        StringReader rdr = new StringReader(cls.value);
                        try
                        {
                            List<BankReconciliation> rec = (List<BankReconciliation>)serializer.Deserialize(rdr);
                            DataRow dr = DT.NewRow();
                            dr["ID"] = cls.ID;
                            dr["Company"] = name[1];
                            dept = Department.loadByDepartment(rec[0].DepartmentNumber);
                            if (dept == null)
                                dr["Deaprtment"] = "[NO DEPARTMENT]";
                            else
                                dr["Department"] = dept.Name;
                            dr["Date"] = Convert.ToDateTime(name[2]);
                            dr["Completed"] = ((BankReconciliation)rec[0]).Completed;
                            DT.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                if (DT == null || DT.Rows.Count == 0)
                    return null;

                DT.DefaultView.Sort = "Company ASC, Department ASC, Date DESC";
                savedDT = DT.DefaultView.ToTable();
            }
            else
                return null;

            BankReconCompanies reconCompany = null;
            BankReconDepartments reconDept = null;
            BankReconRecords reconRecord = null;

            string lastCompany = "";
            string lastDepartment = "";

            if (savedDT == null || savedDT.Rows.Count == 0)
                return null;

            foreach (DataRow dr in savedDT.Rows)
            {
                reconRecord = new BankReconRecords((int)dr["ID"], ((DateTime)dr["Date"]).Date, (bool)dr["Completed"]);
                if (!lastCompany.Equals((string)dr["Company"]))
                {
                    reconCompany = new BankReconCompanies((string)dr["Company"]);
                    reconCompany.Completed = reconRecord.Completed;
                    reconDept = new BankReconDepartments((string)dr["Department"]);
                    reconDept.Completed = reconRecord.Completed;
                    reconDept.ReconRecords.Add(reconRecord);
                    reconCompany.ReconDepartments.Add(reconDept);
                    lastCompany = (string)dr["Company"];
                    lastDepartment = (string)dr["Department"];
                    major.Add(reconCompany);
                }
                else if (!lastDepartment.Equals((string)dr["Department"]))
                {
                    reconDept = new BankReconDepartments((string)dr["Department"]);
                    reconDept.Completed = reconRecord.Completed;
                    reconDept.ReconRecords.Add(reconRecord);
                    reconCompany.ReconDepartments.Add(reconDept);
                    lastDepartment = (string)dr["Department"];
                }
                else
                    reconDept.ReconRecords.Add(reconRecord);

            }
            return major;
        }
    }

    public class BankReconCompanies : BankReconBase
    {
        public string CompanyName { get; set; }
        public bool Completed { get; set; }
        public ObservableCollection<BankReconDepartments> ReconDepartments {get;set;}


        public BankReconCompanies()
        {
            Level = 2;
            ReconDepartments = new ObservableCollection<BankReconDepartments>();

        }

        public BankReconCompanies(string company)
        {
            Level = 2;
            ReconDepartments = new ObservableCollection<BankReconDepartments>();
            CompanyName = company;

        }
    }
    public class BankReconDepartments : BankReconBase
    { 
        public string DepartmentName { get; set; }
        public bool Completed { get; set; }
        public ObservableCollection<BankReconRecords> ReconRecords { get; set; }

        public BankReconDepartments()
        {
            Level = 1;
            ReconRecords = new ObservableCollection<BankReconRecords>();

        }

        public BankReconDepartments(string department)
        {
            Level = 1;
            DepartmentName = department;
            ReconRecords = new ObservableCollection<BankReconRecords>();
        }
    }

    public class BankReconRecords : BankReconBase
    {
        public BankReconRecords()
        {
            this.Level = 0;
        }

        public BankReconRecords(int id, DateTime date, bool completed)
        {
            Level = 0;

            this.ID = id;
            this.Date = date.Date;
            this.Completed = completed;
        }
        public long ID { get; set; }
        public DateTime Date { get; set; }
        // flag to allow multi-selection in the UI for multiple record deletion
        public bool Selected { get; set; }
        public bool Completed { get; set; }
    }

    public class BankReconBase
    {
        public int Level { get; set; }
    }

    
}
