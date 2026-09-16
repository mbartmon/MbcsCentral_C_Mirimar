using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinGL
{
    public class BatchInvoiceReport
    {
        public BatchInvoiceReport() { }

        public BatchControl[] Batches { get; set; }

        private Report rpt = null;
        private PrintQueue printer;
        private Invoices[] invoices;
        private string fontname = "Courier New";
        private float fontsize = 10;
        private BatchInvoiceOptions[] opts = BatchInvoiceOptions.Options();
        public void process()
        {
            if (Batches == null || Batches.Length == 0)
                return;

            rpt = new Report("Batch Invoice Report", fontname, fontsize) { BottomMargin = 80, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
            foreach (BatchControl batch in Batches)
            {
                if (batch.Selected)
                {
                    PrintControl(batch);
                    invoices = Invoices.loadForBatch(batch.Key, InvoicesBatch.TABLE);
                    if (invoices == null || invoices.Length == 0)
                    {
                        rpt.PrintPageLine("");
                        rpt.PrintPageLine( "     <NO INVOICES>");
                        rpt.NewPage();
                        return;
                    }
                    PrintInvoices(invoices);
                    rpt.NewPage();
                }
            }
            rpt.DisplayDocument();
        }

        private void PrintControl(BatchControl control)
        {
            rpt.FontBold = true;
            rpt.FontSize = 14;
            rpt.PrintPageLine("BATCH INVOICE REPORT      " + DateTime.Now.ToShortDateString());
            rpt.PrintPageLine("");
            rpt.FontSize = 12;
            string[] header = { "Batch Name", "tab(31)", "Last Processed", "tab(46)", "Invoice Option", "tab(75)", "Batch Number" };
            rpt.PrintPageLine(header);

            string[] line = {control.BatchName, "tab(31)", control.LastDate.ToShortDateString(), "tab(46)", opts[control.InvoiceOption].OptionName, "tab(75)", control.Key.ToString().PadLeft(6) };
            rpt.PrintPageLine(line);
            rpt.PrintPageLine("");
            rpt.FontBold = false;
        }

        private void PrintInvoices(Invoices[] invoices)
        {
            string allocated = "";
            string[] header = {"Vendor", "tab(32)", "Department", "tab(65)", "Invoice Number", "tab(88)", "Date", "tab(99)", "Due", "tab(113)", "Amount", "tab(121)", "Alloc" };
            rpt.FontSize = 10;
            rpt.FontBold = true;
            rpt.PrintPageLine(header);
            rpt.FontBold = false;

            foreach(Invoices invoice in invoices)
            {
                allocated = (string)(invoice.AllocationFlag ? "*" : "");
                string[] line = { invoice.VendorName, "tab(32)", invoice.DepartmentName.Left(32), "tab(65)", invoice.InvoiceNumber.Left( 19), "tab(86)", invoice.Date.ToShortDateString(), "tab(97)", invoice.PayDueDate.ToShortDateString(), "tab(108)", invoice.Amount.ToString("$###,##0.00").PadLeft(11), "tab(121)", allocated };
                rpt.PrintPageLine(line);
                line = null;
            }
        }
    }
}
