using System;
using System.ComponentModel;

namespace MbcsCentral.WinGL 
{
    public class VendorAllocations : INotifyPropertyChanged
    {
 
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get { return m_id; }
        }

        public Int16 company
        { 
            get {return m_company;} 
            set {
                m_company = value;
                OnPropertyChanged("company");
            } 
        }

        public Int16 order
        {
            get { return m_order; }
            set { m_order = value;
                OnPropertyChanged("order");
            }
        }
        public Int16 department
        {   get { return m_department; }
            set
            {
                m_department = value;
                OnPropertyChanged("department"); 
            } 
        }
        public int account
        {   get { return m_account; }
            set {
                m_account = value;
                OnPropertyChanged("account"); 
            } 
        }
        public decimal amount
        {   get { return m_amount; }
            set {
                m_amount = value;
                OnPropertyChanged("amount"); 
            } 
        }
        public string journal
        {   get { return m_journal; } 
            set {
                m_journal = value;
                OnPropertyChanged("journal"); 
            } 
        }
        public string companyName
        { get { return m_CompanyName; } set { m_CompanyName = value; OnPropertyChanged(("companyName")); } }
        public string departmentName
        { get { return m_DepartmentName; } set { m_DepartmentName = value; OnPropertyChanged("departmentName"); } }
        public string accountName
        { get { return m_AccountName; } set { m_AccountName = value; OnPropertyChanged("accountName"); } }
        /// <summary>
		/// Has record been modified?
		/// </summary>
	    public bool IsChanged

        {get; set;}

        private int m_id;
        private Int16 m_company;
        private Int16 m_department;
        private Int16 m_order;
        private int m_account;
        private decimal m_amount;
        private string m_journal;
        private string m_CompanyName;
        private string m_DepartmentName;
        private string m_AccountName;

        public VendorAllocations() { }

        public VendorAllocations(int id, Int16 company,Int16 order, Int16 department, int account, Decimal amount, string journal, string companyname, string departmentname, string accountname)
        {
            this.m_id = id;
            this.company = company;
            this.order = order;
            this.department = department;
            this.account = account;
            this.amount = amount;
            this.journal = journal;
            this.companyName = companyname;
            this.departmentName = departmentname;
            this.accountName = accountname;
        }


        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                IsChanged = true;
            }
        }

    }
}
