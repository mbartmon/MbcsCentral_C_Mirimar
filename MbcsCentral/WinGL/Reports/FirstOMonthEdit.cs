using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MbcsCentral.WinGL
{
    public class FirstOfMonthEdit
    {
        Report rpt;
        private string fontname = "Courier New";
        private float fontsize = 12;
        string myApp = "";

        decimal totalAmount = 0;
        decimal totalReportAmount = 0;
    
    
   

        public FirstOfMonthEdit() { }
        public FirstOfMonthEdit(string app)
        {
            myApp = app;
        }
        public void process()
        {
            Invoices[] invoices = Invoices.load();
            InvoiceDistributions[] distributions = null;
            if (invoices == null || invoices.Length == 0)
            {
                MessageBoxCustom.Show("No invoices to print.");
                return;
            }

            rpt = new Report("First of Month Edit", fontname, fontsize) { LeftMargin = 10, TopMargin = 10, LinesPerPage = 70, BottomMargin = 80,  BottomOffset = 100 };
            rpt.PageOrientation = (int)Report.Orientation.Portrait;
            header();

            foreach (Invoices inv in invoices)
            {
                distributions = InvoiceDistributions.loadByInvoice(inv.InvoiceKey);
                if (distributions == null || distributions.Length == 0)
                    continue;
                rpt.FontSize = 16;
                rpt.FontColor = Colors.Blue;
                rpt.PrintPageLine(inv.VendorName.ToUpper());
                rpt.FontColor = Colors.Black;
                rpt.FontSize = fontsize;

                foreach (InvoiceDistributions dist in distributions)
                {
                    if (rpt.PrintPageLine(new string[] { "tab(26)", dist.DepartmentName, "tab(55)", dist.AccountName, "tab(84)", dist.Amount.ToString("$###,###,###.00").LeftPad(15,15)}))
                    {
                        PageSkip(true);                    
                        header();
                    }
                    totalAmount += dist.Amount;
                }

                rpt.FontSize = 10;
                rpt.FontBold = true;
                rpt.PrintPageLine(new string[] { "Summary for ", inv.VendorName.ToUpper(), "tab(70)", "(", distributions.Length.ToString(), " detail records)" });
                rpt.PrintPageLine(new string[] { "Sum", "tab(110)", totalAmount.ToString("#######.##") });
                rpt.FontSize = fontsize;
                rpt.FontBold = false;

                totalReportAmount += totalAmount;
                totalAmount = 0;
            }
            rpt.FontBold = true;
            rpt.PrintPageLine();
            rpt.PrintPageLine(new string[] {"Grand Total ", "tab(90)", totalReportAmount.ToString("########.##") });
            rpt.DisplayDocument();
        }
        private void header()
        {
            rpt.FontColor = Colors.Blue;
            rpt.FontSize = 18;
            rpt.FontBold = true;
            rpt.PrintPageLine("1st of Month Edit   ".PadRight(45) + Report.DATE_TAG + "   page " + Report.PAGE_TAG);
            rpt.PrintPageLine(); 
            rpt.PrintPageLine(new string('_', 65 ));
            rpt.PrintPageLine();
            rpt.FontSize = 16;
            rpt.FontUnderline = true;
            rpt.PrintPageLine(new string[] { "VendorName", "tab(19)", "Street Address", "tab(41)", "Description", "tab(68)", "Amount" });
            rpt.PrintPageLine();
            rpt.FontBold = false;
            rpt.FontColor = Colors.Black;
            rpt.FontSize = fontsize;
            rpt.FontUnderline = false;
        }

        private void PageSkip(bool heading = true, bool detail = true)
        {
            //*************
            //* Page Skip *
            //*************
            //PageContent page1Content = new PageContent();
            //((System.Windows.Markup.IAddChild)page1Content).AddChild(page);
            //doc.Pages.Add(page1Content);
            rpt.NewPage();
            rpt.CurrentHeight = 0;
            if (heading)
            {
                rpt.CurrentHeight = rpt.TopMargin;
            }
        }
    }
}
