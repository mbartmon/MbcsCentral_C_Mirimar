using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
    public class InvoiceList
    {
        Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 8;
        string myApp = "";

        public InvoiceList() { }

        public InvoiceList(string app)
        {
            myApp = app;
        }

        public void process(Invoices[] invoices, bool WithCheckNumbersOnly = false)
        {
            if (invoices == null || invoices.Length == 0)
            {
                MessageBoxCustom.Show("No invoices to print.");
                return;
            }

            int lines = 0; 
            rpt = new Report("Balance Sheet", fontname, fontsize) { LeftMargin = 10, TopMargin = 10, LinesPerPage = 55 };
            rpt.PageOrientation = (int)Report.Orientation.Landscape;
            header();
            foreach (Invoices invoice in invoices)
            {
                if (WithCheckNumbersOnly && invoice.CheckNumber == 0)
                    continue;
                if (rpt.PrintPageLine(new string[] { invoice.VendorName.Left(35), "tab(37)", invoice.DepartmentName, "tab(74)", invoice.FullInvoiceNumber.Left(30), "tab(106)", invoice.Date.ToShortDateString(), "tab(118)", invoice.PayDueDate.ToShortDateString(), "tab(130)", invoice.AmountWithDiscount.ToString("$####,##0.00").Left(11).PadLeft(11), "tab(142)", (invoice.AllocationFlag ? "*" : ""), "tab(144)", invoice.CheckNumber.ToString("#####0"), "tab(151)", (invoice.EFT ? "*" : "") }))
                {
                    if (lines < invoices.Length)
                    {
                        rpt.NewPage();
                        header();
                    }
                }
                lines++;
            }

            rpt.DisplayDocument();
        }

        private void header()
        {
            rpt.PrintPageLine("List of Invoices   " + Report.DATE_TAG + "   page " + Report.PAGE_TAG);
            rpt.PrintPageLine(); rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] {"Vendor", "tab(37)", "Department", "tab(74)","Invoice Number", "tab(106)", "Date" , "tab(118)", "Due", "tab(130)", "Amount", "tab(142)", "*", "tab(144)", "Check", "tab(151)", "EFT" });
            rpt.PrintPageLine(new string('=', 156));
        }
    }
}
