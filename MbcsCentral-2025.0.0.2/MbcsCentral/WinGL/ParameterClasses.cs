using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MbcsCentral.WinGL
{
    public class GeneralLedgerParms
    {
        public short Company { get; set; } = 0;
        public short Department { get; set; } = 0;
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }
        public int Account { get; set; } = 0;
        public DateTime StartDate { get; set; } = GlobalShared.NULL_DATE;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public bool Consolidated { get; set; } = false;
        public bool PrintZeroBalances { get; set; } = false;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string Printer { get; set; }
        public bool SingleDepartment { get; set; } = false;
        public bool SingleAccount { get; set; } = false;
        private DateTime endDate = DateTime.Now;

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static GeneralLedgerParms DeSerializeObject(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GeneralLedgerParms));
            StringReader rdr = new StringReader(inputString);
            GeneralLedgerParms parms = (GeneralLedgerParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    public class CheckRegisterParms
    {
        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static CheckRegisterParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CheckRegisterParms));
            StringReader rdr = new StringReader(inputString);
            CheckRegisterParms parms = (CheckRegisterParms)serializer.Deserialize(rdr);
            return parms;
        }

        public int CompanyNumber { get; set; } = -1;
        public int DepartmentNumber { get; set; } = 1;
        public int StartCheck { get; set; }
        public int EndCheck { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string CompanyName { get; set; } = "";
        public string DepartmentName { get; set; } = "";

    }

    public class JournalReportParms
    {

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static JournalReportParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(JournalReportParms));
            StringReader rdr = new StringReader(inputString);
            JournalReportParms parms = (JournalReportParms)serializer.Deserialize(rdr);
            return parms;
        }

        public short Company { get; set; } = -1;
        public short Department { get; set; } = 1;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string CompanyName { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public bool Consolidate { get; set; }
        public bool PrintSingleDepartment { get; set; }
        public string JournalName { get; set; }
        public string Layout { get; set; }
        public bool SourceSearch { get; set; }
        public bool PrintTotals { get; set; }
        public string ReportTitle { get; set; }

    }

    public class OperatingStatementParms
    {
        public string StatementName { get; set; }
        public short CompanyNumber { get; set; }
        public string CompanyName { get; set; }
        public short DepartmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public bool Consolidated { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Detail { get; set; }
        public int CurrentQtdYtd { get; set; }
        public bool PriorYear { get; set; }
        public bool Percentage { get; set; }
        public bool PrintYtd { get; set; }
        public bool PrintZero { get; set; }
        public bool CondensePrint { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static OperatingStatementParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OperatingStatementParms));
            StringReader rdr = new StringReader(inputString);
            OperatingStatementParms parms = (OperatingStatementParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    public class BalanceSheetParms
    {
        public string PLAccountName { get; set; }
        public string CompanyName { get; set; }
        public bool Cancel { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int PLAccount { get; set; }
        public short Department { get; set; }
        public string DepartmentName { get; set; }
        public bool Consolidated { get; set; }
        public short Company { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static BalanceSheetParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetParms));
            StringReader rdr = new StringReader(inputString);
            BalanceSheetParms parms = (BalanceSheetParms)serializer.Deserialize(rdr);
            return parms;
        }

    }

    public class TrialBalanceParms
    {
        public bool PriorYear { get; set; }
        public bool Cancel { get; set; }
        public bool XWorkArea { get; set; }
        public bool ZeroBalances { get; set; }
        public bool Consolidated { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string DepartmentName { get; set; }
        public short Department { get; set; }
        public string CompanyName { get; set; }
        public short Company { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static TrialBalanceParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TrialBalanceParms));
            StringReader rdr = new StringReader(inputString);
            TrialBalanceParms parms = (TrialBalanceParms)serializer.Deserialize(rdr);
            return parms;
        }

    }

    public class PayrollReportParms
    {
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public bool YTD { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static PayrollReportParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PayrollReportParms));
            StringReader rdr = new StringReader(inputString);
            PayrollReportParms parms = (PayrollReportParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    public class F1099Report
    {
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public bool OrderByVendor { get; set; } = true;
        public bool PrintVendorAddress { get; set; } = false;
        public bool PrintDetail { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static F1099Report DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(F1099Report));
            StringReader rdr = new StringReader(inputString);
            F1099Report parms = (F1099Report)serializer.Deserialize(rdr);
            return parms;
        }
    }

    public class DisbursementReportParms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int balanceAccount;
        private int collectionAccountOne;
        private int collectionAccountTwo;
        private string[] journals = new string[10];

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public int BalanceAccount
        {
            get { return balanceAccount; }
            set { balanceAccount = value; OnPropertyChanged("BalanceAccount"); }
        }
        public int CollectionAccountOne { get { return collectionAccountOne; } set { collectionAccountOne = value; OnPropertyChanged("CollectionAccountOne"); } }
        public int CollectionAccountTwo { get { return collectionAccountTwo; } set { collectionAccountTwo = value; OnPropertyChanged("CollectionAccountTwo"); } }
        public string[] Journals { get { return journals; } set{ journals = value; OnPropertyChanged("Journals"); }}
        public int Year { get; set; }
        public int Month { get; set; }
        public string DepartmentName { get; set; }
        public int Department { get; set; }
        public string CompanyName { get; set; }
        public int Company { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static DisbursementReportParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DisbursementReportParms));
            StringReader rdr = new StringReader(inputString);
            DisbursementReportParms parms = (DisbursementReportParms)serializer.Deserialize(rdr);
            return parms;
        }
    }

    public class BatchReportsParms
    {
        // Parameters
        public string DefaultPrinter { get; set; }
        public bool CondensePrint { get; set; }
        public bool PrintDetail { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ReportMonth { get; set; }
        public int ReportYear { get; set;}
        public bool Duplex { get; set; }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static BatchReportsParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BatchReportsParms));
            StringReader rdr = new StringReader(inputString);
            BatchReportsParms parms = (BatchReportsParms)serializer.Deserialize(rdr);
            return parms;
        }

    }
}
