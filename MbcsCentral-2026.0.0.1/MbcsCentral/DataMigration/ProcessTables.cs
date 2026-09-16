using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral
{
    public class ProcessTables : INotifyPropertyChanged
    {

        private string m_sourceTable;
        private string m_targetTable;
        private string m_converter;
        private bool m_identity;
        private bool m_isChecked;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProcessTables()
        {

        }

        public ProcessTables(string sourceTable, string targetTable, string converter, bool identity)
        {
            m_sourceTable = sourceTable;
            m_targetTable = targetTable;
            m_converter = converter;
            m_identity = identity;
        }

        private void RaisePropertyChanged(string propName)
        {
            PropertyChangedEventHandler eh = PropertyChanged;
            if (eh != null)
                eh(this, new PropertyChangedEventArgs(propName));
        }
        public string SourceTable
        {
            get { return m_sourceTable; }
            set {m_sourceTable = value;
                RaisePropertyChanged("SourceTable");
            }
        }
        public string TargetTable
        {
            get { return m_targetTable; }
            set { m_targetTable = value;
            RaisePropertyChanged("TargetTable");
            }
        }
        public string Converter
        {
            get { return m_converter; }
            set { m_converter = value; }
        }
        public bool Identity
        {
            get { return m_identity; }
            set { m_identity = value; }
        }
        public bool IsChecked
        {
            set { m_isChecked = value;
            RaisePropertyChanged("IsChecked");
            }
            get { return m_isChecked; }
        }
        public string DisplayValue
        {
            get { return m_sourceTable + "->" + m_targetTable; }
        }
    }
}
