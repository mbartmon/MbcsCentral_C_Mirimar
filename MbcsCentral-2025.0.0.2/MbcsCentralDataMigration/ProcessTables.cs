using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral
{
    public class ProcessTables
    {

        private string m_sourceTable;
        private string m_targetTable;
        private string m_converter;
        private bool m_identity;

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

        public string SourceTable
        {
            get { return m_sourceTable; }
            set {m_sourceTable = value;}
        }
        public string TargetTable
        {
            get { return m_targetTable; }
            set { m_targetTable = value; }
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
    }
}
