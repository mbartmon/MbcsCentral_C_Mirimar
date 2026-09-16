using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class InvBatchDistributions : InvoiceDistributions
{
    public static string TABLE = "gl_InvBatchDistributions";
    static InvBatchDistributions()
    {
        GlobalShared.Table = "gl_InvBatchDistributions";
    }

	public InvBatchDistributions() : base()
	{
	}

	public InvBatchDistributions(int InvoiceKey,short Company,short Department,int Account,int Batch,int Id,decimal Amount,string Journal) : 
			base( InvoiceKey, Company, Department, Account, Batch, Id, Amount, Journal)
	{
	}
}
}
