using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM
{

    public class LockBoxProcessReport
    {

        Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 12;
        string myApp = "";

        public string FileName { get; set; }

        public LockBoxProcessReport() { }

        public LockBoxProcessReport(string app)
        {
            myApp = app;
        }

        public void process(System.Windows.Controls.ListBox list, bool transfer = false)
        {
            if (list.Items.Count == 0)
            {
                MessageBoxCustom.Show("Nothing to print.");
                return;
            }

            string Title = (transfer ? "Lock Box Data File" : "Lock Box Data File Processing Report");
            
            rpt = new Report("Lock Box Data File Processing Report", fontname, fontsize) { LeftMargin = 10, TopMargin = 10, LinesPerPage = 65};
            header();

            foreach (string line in list.Items)
            {
                if (rpt.PrintPageLine(line))
                {
                    rpt.NewPage();
                    header();
                }
            }
            rpt.DisplayDocument();
        }

        private void header()
        {
            rpt.PrintPageLine("LockBox Processing Report  " + Report.DATE_TAG + "   page " + Report.PAGE_TAG);
            rpt.PrintPageLine("LockBox File Name: " + FileName);
            rpt.PrintPageLine();
        }
    }
}
