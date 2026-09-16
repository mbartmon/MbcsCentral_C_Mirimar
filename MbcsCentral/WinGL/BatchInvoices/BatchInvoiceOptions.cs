using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MbcsCentral.WinGL
{
    public class BatchInvoiceOptions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_name;
        private int m_index;
        public enum InvoiceOptions
        {
            Date = 0,
            DescAutoDate = 1,
            Desc = 2,
            Manual = 3,
            ManDate = 4,
            DptDate = 5,
            DescDate = 6,
            DescManual = 7,
            DescMonthYear = 8,
            Desc2Year = 9,
            DescMonthDayYear = 10
        }

        private string[] InvOptions =
        {
            "Date + Sequence Number",
            "Description + Date + Sequence #",
            "Description + Sequence #",
            "Manual Entry",
            "Manual Entry + Date",
            "Department + Manual Date",
            "Description + Date",
            "Description + Manual Entry",
            "Description + Month/Year",
            "Description + 2 digit year",
            "Description + Month/Day/Year"
        };

        public BatchInvoiceOptions()
        {
        }

        public BatchInvoiceOptions(string name, int index)
        {
            OptionName = name;
            Index = index;    
        }
        public static BatchInvoiceOptions[] Options()
        {
            ArrayList list = new ArrayList();
            list.Add(new BatchInvoiceOptions("Date + Sequence Number", 0));
            list.Add(new BatchInvoiceOptions("Description + Date + Sequence #", 1));
            list.Add(new BatchInvoiceOptions("Description + Sequence #", 2));
            list.Add(new BatchInvoiceOptions("Manual Entry", 3));
            list.Add(new BatchInvoiceOptions("Manual Entry + Date", 4));
            list.Add(new BatchInvoiceOptions("Department + Manual Date", 5));
            list.Add(new BatchInvoiceOptions("Description + Date", 6));
            list.Add(new BatchInvoiceOptions("Description + Manual Entry", 7));
            list.Add(new BatchInvoiceOptions("Description + Month/Year", 8));
            list.Add(new BatchInvoiceOptions("Description + 2 digit year", 9));
            list.Add(new BatchInvoiceOptions("Description Month/Day/Year", 10));
            return (BatchInvoiceOptions[])list.ToArray(typeof(BatchInvoiceOptions));
        }
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string OptionName
        {
            get { return m_name; }
            set { m_name = value;
                OnPropertyChanged("OptionName");
            }
        }
        public int Index
        {
            get { return m_index; }
            set { m_index = value;
                OnPropertyChanged("Index");
            }
        }


    }
}
