using System.ComponentModel;

namespace MbcsCentral.WinGL
{
    public class VendorMemos : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public short CheckGroup
        {
            get { return m_checkGroup; }
            set
            {
                m_checkGroup = value;
                OnPropertyChanged("CheckGroup");
            }
        }
        public int VendorNumber
        {
            get { return m_vendorNumber; }
            set
            {
                m_vendorNumber = value;
                OnPropertyChanged("VendorNumber");
            }
        }
        public int CompanyNumber
        {
            get { return m_companyNumber; }
            set
            {
                m_companyNumber = value;
                OnPropertyChanged("CompanyNumber");
            }
        }
        public int DepartmentNumber
        {
            get { return m_departmentNumber; }
            set
            {
                m_departmentNumber = value;
                OnPropertyChanged("DepartmentNumber");
            }
        }
        public int BatchNumber
        {
            get { return m_batchNumber; }
            set
            {
                m_batchNumber = value;
                OnPropertyChanged("BatchNumber");
            }
        }
        public int MemoId
        {
            get { return m_memoId; }
        }
        public string Memo
        {
            get { return m_memo; }
            set
            {
                m_memo = value;
                OnPropertyChanged("Memo");
            }
        }
        public bool Delete
        {
            get { return m_Delete; }
            set
            {   m_Delete = value;
                OnPropertyChanged("Delete");
            }
        }
        public string CompanyName
        { get { return m_CompanyName; } set { m_CompanyName = value; OnPropertyChanged(("companyName")); } }
        public string DepartmentName
        { get { return m_DepartmentName; } set { m_DepartmentName = value; OnPropertyChanged("departmentName"); } }
        /// <summary>
		/// Has record been modified?
		/// </summary>
	    public bool IsChanged
        { get; set; }
  
        private int m_vendorNumber;
        private int m_companyNumber;
        private int m_departmentNumber;
        private short m_checkGroup;
        private string m_memo;
        private int m_batchNumber;
        private int m_memoId;
        private string m_CompanyName;
        private string m_DepartmentName;
        private bool m_Delete;

        public VendorMemos() { }

        public VendorMemos(short checkGroup, int vendorNumber, int companyNumber, int departmentNumber, int batchNumber, int id, string memo, string companyName, string departmentName)
        {
            m_memoId = id;
            m_vendorNumber = vendorNumber;
            m_companyNumber = companyNumber;
            m_departmentNumber = departmentNumber;
            m_checkGroup = checkGroup;
            m_batchNumber = batchNumber;
            m_memo = memo;
            m_CompanyName = companyName;
            m_DepartmentName = departmentName;
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
