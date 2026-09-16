using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM
{
    class GlTableList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_To;
        private string m_From;
        private string m_ToBuilding;
        private string m_FromBuilding;
        private GLTable m_gl;

        public GlTableList()
        { }

        public GlTableList(string from, string to, string fromBuilding, string toBuilding, GLTable gl)
        {
            m_From = from;
            m_To = to;
            m_FromBuilding = fromBuilding;
            m_ToBuilding = toBuilding;
            m_gl = gl;
        }

        public void save()
        {
            m_gl.From = From;
            m_gl.To = To;
            m_gl.save();
        }
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                IsChanged = true;
            }
        }

        /// <summary>
        /// Has record been modified?
        /// </summary>
        public bool IsChanged
        { get; set; }
        public string To
        {
            get { return m_To; }
            set
            {
                m_To = value;
                OnPropertyChanged("To");
            }
        }
        public string From
        {
            get { return m_From; }
            set
            {
                m_From = value;
                OnPropertyChanged("From");
            }                             
        }
        public string ToBuilding
        {
            get { return m_ToBuilding; }
            set
            {
                m_ToBuilding = value;
                OnPropertyChanged("ToBuilding");
            }
        }
        public string FromBuilding
        {
            get { return m_FromBuilding; }
            set
            {
                m_FromBuilding = value;
                OnPropertyChanged("FromBuilding");
            }
        }
        public GLTable GlObj
        {
            get { return m_gl; }
            set
            {
                m_gl = value;
                OnPropertyChanged("GlObj");
            }
        }
    }
}
