using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmReportDistpatcher.xaml
    /// </summary>
    public partial class frmReportDispatcher : Window
    {


        private string m_ThisApp;

        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmReportDispatcher() { }
        public frmReportDispatcher(string parm, string report)
        {
            InitializeComponent();
            m_ThisApp = parm;
            object[] parms = { };
            Invoke(report, parms);
        }

        public frmReportDispatcher(string parm, string report, object[] parms)
        {
            InitializeComponent();
            m_ThisApp = parm;
            Invoke(report, parms);
        }

        public frmReportDispatcher(string report, object[] parms)
        {
            InitializeComponent();
            Invoke(report, parms);
        }

        public void Invoke(string report, object[] parms)
        {
            CallClassParms callForm = new CallClassParms(report, parms);
            object obj = callForm.returnObject(parms);
            this.Close();
            //object frm = (callForm.type)obj;
        }
    }
}
