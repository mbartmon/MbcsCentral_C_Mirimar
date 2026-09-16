using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
    public class InvoicesBatch : Invoices
    {
        public static string TABLE = "gl_InvoicesBatch";
        static InvoicesBatch()
        {
        }

	    public InvoicesBatch() : base()
	    {
	    }

	    public InvoicesBatch(DateTime Date,DateTime PayDueDate,DateTime DateLastPaid,DateTime CheckDate,short CheckGroup,short PayFlag,int VendorNumber,int Batch,int CheckNumber,int Company,int Department,int InvoiceKey,decimal Amount,decimal Allowance,decimal Discount,decimal DiscountAmount,decimal AmountPaid,decimal AmountToPay,bool AllocationFlag,string InvoiceNumber,string Description,string Memo) : 
			    base( Date, PayDueDate, DateLastPaid, CheckDate, CheckGroup, PayFlag, VendorNumber, Batch, CheckNumber, Company, Department, InvoiceKey, Amount, Allowance, Discount, DiscountAmount, AmountPaid, AmountToPay, AllocationFlag, InvoiceNumber, Description, Memo)
	    {
	    }
    }
}
