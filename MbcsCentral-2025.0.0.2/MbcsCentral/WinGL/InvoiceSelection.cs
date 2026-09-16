using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace MbcsCentral.WinGL
{
    public class InvoiceSelection
    {
        public DateTime processDate { get; set; }
        public bool forceDiscounts { get; set; } = false;
        public bool clearSelections { get; set; } = false;

        public bool SelectInvoices()
        {
            Invoices[] invoices;
            string xsql = null;
            int r = 0;
            decimal disc = default(decimal);


            invoices = Invoices.load();
            if (invoices == null || invoices.Length == 0)
            {
                return false;
            }
            foreach (Invoices invoice in invoices)
            {
                if (!invoice.EFT && !invoice.ACH)
                {
                    invoice.PayFlag = 0;
                    invoice.save();
                }
            }
            /*
			xsql = "SELECT * FROM Invoices WHERE PayDueDate <= #" + dtDueDate.Value + "# AND ";
			xsql = xsql + " CheckNumber = 0 AND AllocationFlag = True";
			if (optClear[0].Checked == 1) {
				xsql = xsql + " AND PayFlag <= 0";
			}
            */
            DateTime dt = new DateTime(processDate.Year, processDate.Month, processDate.Day, 23, 59, 59);
            invoices = Invoices.LoadForPayment(dt, !clearSelections);
            if (invoices != null && invoices.Length > 0)
            {
                decimal amount;
                foreach (Invoices invoice in invoices)
                {
                    amount = 0;
                    if (invoice.Discount != 0)
                    {
                        if (invoice.AmountToPay != 0)
                        {
                            amount = invoice.AmountToPay - invoice.Allowance;
                            //if (amount < 0) {
                            //	amount = 0;
                            //}
                        }
                        else
                        {
                            amount = invoice.Amount - invoice.Allowance - invoice.AmountPaid;
                        }
                        if (invoice.DiscountDollar)
                            disc = invoice.Discount;
                        else
                            disc = System.Math.Round(amount * (invoice.Discount * (decimal)0.01), 2);
                    }
                    if (!forceDiscounts)
                    {
                        invoice.PayFlag = 1;
                        if (invoice.PayDueDate.Date >= processDate.Date)
                        {
                            invoice.DiscountAmount = disc;
                        }
                        else
                        {
                            invoice.DiscountAmount = 0;
                        }
                    }
                    else
                    {
                        invoice.PayFlag = 2;
                        invoice.DiscountAmount = disc;
                    }
                    if (invoice.Amount - invoice.DiscountAmount - invoice.Allowance - invoice.AmountPaid != 0)
                        invoice.save();
                    disc = 0;
                }
                return true;
            }
            return false;
        }
    }
}
