 using System.ComponentModel;

namespace MbcsCentral.WinPM
{
    public class PaymentEntries : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // fields
        string m_code;
        string m_description = "";
        decimal m_due;
        decimal m_paid;
        int m_chargeNumber;
        decimal m_amount;
        string m_notes;
        bool m_isChanged = false;

        private void OnPropertyChanged( string info )
        {
            if (PropertyChanged != null)
            {
                PropertyChanged( this, new PropertyChangedEventArgs( info ) );
            }
            m_isChanged = true;
        }

        // Properties
        public string Code
        {
            get { return m_code; }
            set { m_code = value;
            OnPropertyChanged( "Code" );
            }
        }
        public string Description
        {
            get { return m_description; }
            set { m_description = value;
            OnPropertyChanged( "Description" );
            }
        }
        public decimal Due
        {
            get { return m_due; }
            set { m_due = value;
            OnPropertyChanged( "Due" );
            }
        }
        public decimal Paid
        {
            get { return m_paid; }
            set { m_paid = value;
            OnPropertyChanged( "Paid" );
            }
        }

        public string Notes
        {
            get { return m_notes; }
            set
            {
                m_notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public int ChargeNumber
        {
            get { return m_chargeNumber; }
            set { m_chargeNumber = value; }
        }

        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }
    }
}
