using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	static class WinglGlobal
	{
		[DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
//* $Header$
//* $Copyright   (c) 2001-2003 Michael Bartmon Computer Services LLC $
//* $Log$
//* Revision 1.14  2010/11/08 22:10:43  mike
//* New document viewer
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.13  2010/06/07 13:04:53  mike
//* Modification of transactions.description caused it to be placed at the end of the table.
//* This in turn broke posting.
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.12  2010/06/03 23:36:13  mike
//* Added deletion of documents upon deletion of check
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.11  2010/06/02 13:20:14  mike
//* *) Problem with Disbursement Report
//* *) Fix for replacement invoice for deleted one not showing up as scanned in Check register
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.9  2010/02/06 23:56:00  mike
//* Misc challanges
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.8  2010/01/04 17:43:32  mike
//* Scanning of batch invoices
//* Scanning of and Display from Check Register
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.7  2009/12/25 03:47:05  mike
//* Scanning for Batch Invoices
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.6  2009/10/12 19:56:39  mike
//* Paid invoice posting to windoc
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.5  2009/09/20 16:53:16  mike
//* Replaced counting invoices to counting allocation items for determining how many check stubs each check will use
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.4  2009/09/01 15:42:12  mike
//* For continuous printing, a multiple stub check was using the wrong check number
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.3  2009/08/24 20:42:25  mike
//* Changed check:  When a check has been posted individually to the Cash Account, the entire new check amount must be posted.
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2009/08/23 03:48:40  mike
//* Edited check should only post to cash account when the net amount has changed.  And then only the difference in the amount.
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.1  2009/08/21 12:36:16  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:06  mike
//* Copied from wingl folder
//*
//*
//*   Auto-added
//*
//* Revision 2.18  by: normal  Rev date: 1/27/2003 10:53:10 AM
//*   A single invoice distribution change was not being saved.
//*
//* Revision 2.17  by: normal  Rev date: 1/24/2003 10:27:06 AM
//*   Check Reconciliation - Bank Error Handling
//*
//* Revision 2.16  by: normal  Rev date: 1/6/2003 2:04:54 PM
//*   Corrected NULL error in General Ledger Report.
//*   Fixed inabiltity to delete Operating Statements
//*
//* Revision 2.15  by: normal  Rev date: 12/24/2002 5:28:30 PM
//*   Added multiple years to Balances by Company and by Account
//*
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:34:02 AM
//*   Initial revision.
//*
//* $Endlog$

		private static extern void Sleep(int dwMilliseconds);

// CONSTANTS
		public static float VERSION;
		public static string SUBVER;

//Public Const WhatsNew = "WHAT'S NEW? - 2/6/2006" & vbCrLf & "Improper opening balances on General Ledger Report"
		public const string HomePage = "http://www.bartmon.com";
		public const string UpdateFile = "http://www.bartmon.com/Files/wingl.txt";
		public const short mntCompany = 0;
		public const short mntDepartment = 1;
		public const short fbalm = 2;
		public const short MAXROWS = 150;
			// used for saving and reloading Farpoint SpreadSheets
		public const string SPext = "SS3";
			// used for skipping lines at the top of report pages
		public const short tmargin = 5;

//*********************************
//* Constants for Batch Invoicing *
//*********************************
		public const short ibDate = 0;
		public const short ibDescAutoDate = 1;
		public const short ibDesc = 2;
		public const short ibManual = 3;
		public const short ibManDate = 4;
		public const short ibDptDate = 5;
		public const short ibDescDate = 6;
		public const short ibDescManual = 7;
		public const short ibDescMonthYear = 8;
		public const short ibDesc2Year = 9;

//**********************************************
//* Check processing definitions and variables *
//**********************************************

		public struct chknums
		{
			public int cmp;
			public int dpt;
			public int cnum;
		}

		public struct Voucher_Layout2
		{
 			public char[] vInvoice;
 			public char[] vDate;
 			public char[] vDesc;
 			public char[] vGross;
 			public char[] vDisc;
 			public char[] vNet;
 			public char[] vX;
		}
		public struct Check_Layout2
		{
 			public char[] layout;
 			public char[] lcmp;
 			public char[] ldpt;
 			public char[] x1;
 			public char[] @void;
 			public char[] chkNum;
 			public char[] chkDate;
 			public char[] amount;
 			public char[] payee;
 			public char[] addr1;
 			public char[] addr2;
 			public char[] addr3;
 			public char[] addr4;
 			public char[] addr5;
 			public char[] memo;
 			public char[] msg;
			[VBFixedArray(20)]
			public Voucher_Layout2[] voucher;
 			public char[] crlf;

 			public void Initialize()
			{
 				voucher = new Voucher_Layout2[21];
			}
		}

		public struct Voucher_Layout
		{
 			public char[] vInvoice;
 			public char[] vDate;
 			public char[] vDesc;
 			public char[] vGross;
 			public char[] vDisc;
 			public char[] vNet;
		}
		public struct Check_Layout
		{
 			public char[] layout;
 			public char[] lcmp;
 			public char[] ldpt;
 			public char[] x1;
 			public char[] @void;
 			public char[] chkNum;
 			public char[] chkDate;
 			public char[] amount;
 			public char[] vendor;
 			public char[] payee;
 			public char[] addr1;
 			public char[] addr2;
 			public char[] addr3;
 			public char[] addr4;
			[VBFixedArray(10)]
			public Voucher_Layout[] voucher;
 			public char[] x2;
 			public char[] crlf;

 			public void Initialize()
			{
 				voucher = new Voucher_Layout[11];
			}
		}

		public struct Voucher_Layout3
		{
 			public char[] vInvoice;
 			public char[] vDate;
 			public char[] vDesc;
 			public char[] vGross;
 			public char[] vDisc;
 			public char[] vNet;
		}
		public struct Check_Layout3
		{
 			public char[] layout;
 			public char[] lcmp;
 			public char[] ldpt;
 			public char[] x1;
 			public char[] @void;
 			public char[] chkNum;
 			public char[] chkDate;
 			public char[] amount;
 			public char[] vendor;
 			public char[] payee;
 			public char[] addr1;
 			public char[] addr2;
 			public char[] addr3;
 			public char[] addr4;
			[VBFixedArray(10)]
			public Voucher_Layout3[] voucher;
 			public char[] memo;
 			public char[] crlf;

 			public void Initialize()
			{
 				voucher = new Voucher_Layout3[11];
			}
		}

//***********************
//* Invoice Posting UDT *
//***********************
		public struct PostInvoice_type
		{
 			public char[] invnumber;
 			public System.DateTime date_Renamed;
			public System.DateTime dueDate;
			public int Company;
			public int Department;
			public int vendor;
			public bool payroll;
			public decimal amountpaid;
			public decimal amounttopay;
			public int Batch;
			public int CheckNumber;
			public System.DateTime datelastpaid;
			public System.DateTime CheckDate;
			public short payflag;
			public string vName;
		}
		public static PostInvoice_type invpGlobal;

//************************************
//* Master Chart Structure variables *
//************************************
		public struct Entry_type
		{
 			public char[] type;
 			public char[] Description;
			public object key;
		}
		public static Entry_type[] LH;
 		public static object[,] CatTotals = new object[6, 3];

//*****************************
//* Receivables Record Layout *
//*****************************
		public struct ARtrans_type
		{
 			public char[] cmp;
 			public char[] dpt;
 			public char[] acct;
 			public char[] date_Renamed;
 			public char[] desc;
 			public char[] amount;
 			public char[] crlf;
		}

//*********************************
//* G/L Disbursement Report Parms *
//*********************************
		public struct DisbRpt_type
		{
 			public char[] balacct;
 				//*10
			public string[] jrns;
 			public char[] colacct1;
 			public char[] colacct2;
		}

//*************************
//* Check Register Layout *
//*************************
		public struct details_type
		{
			public int CompanyNumber;
			public int DepartmentNumber;
			public int AccountNumber;
			public short FiscalYear;
 			public System.DateTime date_Renamed;
 			public char[] Journal;
 			public char[] Source;
 			public char[] Description;
 			public char[] VendorNumber;
			public int CheckNumber;
			public decimal amount;
			public short MajorCategory;
			public short MinorCategory;
			public int IndexNumber;
			public bool Flag;
		}
		public struct Register_type
		{
			public int comp;
			public int dept;
			public int chk;
 			public System.DateTime date_Renamed;
			public int vend;
			public decimal amt;
			[VBFixedArray(WinglGlobal.MAXROWS)]
			public details_type[] details;

 			public void Initialize()
			{
				details = new details_type[WinglGlobal.MAXROWS + 1];
			}
		}
 		public static Register_type cr;

//******************
//* Company Record *
//******************
		public struct Company_Record
		{
			public int CompanyNumber;
 			public char[] Name;
 			public char[] StreetAddress;
 			public char[] City;
 			public char[] State;
 			public char[] Zip;
 			public char[] Telephone;
 			public char[] Fax;
 			public char[] Email;
 			public char[] FederalID;
 			public char[] StateID;
			public byte FiscalYearEnd;
			public short CurrentFiscalYear;
		}
		public static Company_Record CmpRec;

//***************************************************
//* Used for bringing balances over from old system *
//***************************************************
		public struct balance_type
		{
 			public char[] field;
 			public char[] Account;
 			public char[] subacct;
 			public char[] category;
 			public char[] @group;
			[VBFixedArray(34)]
			public double[] amounts;
 			public char[] crlf;

 			public void Initialize()
			{
 				amounts = new double[35];
			}
		}

		public struct test_type
		{
			public string x;
		}

		public enum direction
		{
			up = 38,
			dwn = 40,
			lft = 37,
			rgt = 39
		}

// Flags
		public static bool cashflag;
		public static bool AddFlag;
			// true when invoice batch is being edited
		public static bool InvoiceBatch;
		public static bool BatchInvoiceChecked;
			// true when an invoice batch is being processed
		public static bool batchProcess;
		public static bool BatchRptDelete;
		public static bool BatchOn;
		public static bool BatchDateAdj;
		public static bool rptCancel;
		public static bool BalanceFwd;
		public static bool glPost;
		public static short MainTop;
		public static short MainLeft;

		public static string UserGrp;
		public static string userid;
		public static string UserPWD;
		public static string TempLoc;
		public static bool validuser;
		public static string sProcID;
		public static string PMSDataBase;
		public static frmMain fMainForm;
		public static short maintenanceOption;
		public static string appath;
		public static short fbalc;
		public static int GLCashAcct;
		public static System.Windows.Forms.Form[] fbal = new System.Windows.Forms.Form[fbalm + 1];
		public static int lastCompany;
		public static int lastDepartment;
		public static System.DateTime lastInvDate;
		public static System.DateTime lastPayDate;
		public static int lastVendor;
		public static string[] Journals = new string[11];
 		public static short[,] md = new short[3, 13];
		public static string IDfilename;
		public static string Ifilename;
		public static string CurrentBatch;
		public static System.DateTime BatchCheckDate;
		public static int BatchNumber;

		public static string logPath;
		public static string logBatch;
		public static string logTrans;
		public static string logInvoice;

// Batch Report Variables
			// Used to save batch report parameters
		public static object[] BatchRptSave = new object[17];
		public static string BatchListName;
		public static System.DateTime bStartDate;
		public static System.DateTime bEndDate;
		public static short bMonth;
		public static short bYear;

// DATABASE Variables
		public static string DefaultDataBase;
		public static DAO.Database GLdb;
		public static DAO.Database PMSdb;
   		public static DAO.Recordset tblDepartment;
		public static DAO.Recordset tblCompany;
		public static DAO.Recordset tblAccounts;
   		public static DAO.Recordset tblVendors;
		public static DAO.Recordset tblCategory;
		public static DAO.Recordset tblVendorCats;
   		public static DAO.Recordset tblInvDist;
		public static DAO.Recordset tblinvoices;
		public static DAO.Recordset tblJournals;
   		public static DAO.Recordset tblCheckNumbers;
		public static DAO.Recordset tblMemo;
		public static DAO.Recordset tblControl;
   		public static DAO.Recordset tblVendor2;
		public static DAO.Recordset tblBalances;
		public static DAO.Recordset tblBatch;
			// Holds the current high key in the Category table.
		public static short HighKey;
		public static bool InvoiceFlag;
		public static object crReport;

// Variables added for Windoc interface
		public static bool scan;
		public static string dbname;
		public static object rss;
			//, cnn As clsConnection
		public static object cnn;
		public static string uniqueID;
		public static string vName;
		public static int PID;
			// rsd As ADODB.Recordset
		public static object rsd;

// ADOX Database variables
		public static ADODB.Connection Xconx;
		public static ADODB.Command Xcmd;
		public static ADODB.Recordset Xrs;
		public static string m_MDBdatabase;
		public static string m_MDBtable;

		public static void Main()
		{

			VERSION = Conversion.Val(WinGL.My.MyProject.Application.Info.Version.Major + "." + WinGL.My.MyProject.Application.Info.Version.Minor);
			SUBVER = "." + WinGL.My.MyProject.Application.Info.Version.Revision;
			My.MyProject.Forms.frmSplash.Show();
			My.MyProject.Forms.frmSplash.Refresh();
			md[1, 1] = 31;
			md[2, 1] = 31;
			md[1, 2] = 28;
			md[2, 2] = 29;
			md[1, 3] = 31;
			md[2, 3] = 31;
			md[1, 4] = 30;
			md[2, 4] = 30;
			md[1, 5] = 31;
			md[2, 5] = 31;
			md[1, 6] = 30;
			md[2, 6] = 30;
			md[1, 7] = 31;
			md[2, 7] = 31;
			md[1, 8] = 31;
			md[2, 8] = 31;
			md[1, 9] = 30;
			md[2, 9] = 30;
			md[1, 10] = 31;
			md[2, 10] = 31;
			md[1, 11] = 30;
			md[2, 11] = 30;
			md[1, 12] = 31;
			md[2, 12] = 31;

			logPath = "logs\\";

			fMainForm = new frmMain();
 			fMainForm.Show();
			My.MyProject.Forms.frmSplash.Close();
			System.Windows.Forms.Application.Run(fMainForm);

		}
		public static void GetCompanies(ref System.Windows.Forms.Form f)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstCompanies.Clear()
			tblCompany = GLdb.OpenRecordset("SELECT * FROM Company ORDER BY Name");
			if (tblCompany.RecordCount > 0) {
 				//If f.lstCompanies.Tag = "FP" Then f.lstCompanies.Row = -1
				while (!tblCompany.EOF) {
 					//If f.lstCompanies.Tag <> "FP" Then
 					//f.lstCompanies.AddItem(VB6.Format(tblCompany.Fields("CompanyNumber").Value, "00#") & " " & tblCompany.Fields("Name").Value)
					//Else
 					//f.lstCompanies.InsertRow(rtpad(Left(tblCompany.Fields("Name").Value, 50), 50) & Chr(9) & tblCompany.Fields("CompanyNumber").Value)
					//End If
					tblCompany.MoveNext();
				}
			}
			goto GetCompanies_exit;
			GetCompanies_error:

			if (Err().Number != 3021) {
				LogError.MsgBox_Renamed("Error while reading Companies table: ", MsgBoxStyle.Critical, sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetCompanies_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblCompany.Close();

		}
		public static void GetVendors(ref System.Windows.Forms.Form f, ref bool inactive = false)
		{

			string xsql = null;
 			DAO.Recordset tblVendors = null;

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstVendors.Clear()
			xsql = "SELECT * FROM Vendors";
			if (!inactive) {
				xsql = xsql + " WHERE Inactive >= 0";
			}
			xsql = xsql + " ORDER BY VendorName";
			tblVendors = GLdb.OpenRecordset(xsql);
			if (tblVendors.RecordCount > 0) {
 				//If f.lstVendors.Tag = "FP" Then f.lstVendors.Row = -1
				while (!tblVendors.EOF) {
 					//If f.lstVendors.Tag <> "FP" Then
 					//f.lstVendors.AddItem(VB6.Format(tblVendors.Fields("VendorNumber").Value, "0000# ") & tblVendors.Fields("VendorName").Value)
					//Else
 					//f.lstVendors.InsertRow(Trim(tblVendors.Fields("VendorName").Value) & " [" & tblVendors.Fields("OldNumber").Value & "]" & Chr(9) & tblVendors.Fields("VendorNumber").Value)
					//End If
					tblVendors.MoveNext();
				}
			}
			goto GetVendors_exit;
			GetVendors_error:

			LogError.MsgBox_Renamed("Error while reading Vendors table: ", MsgBoxStyle.Critical, sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			GetVendors_exit:


		}
		public static int GetCheckNumber(ref int cmp, ref int dpt)
		{
			int functionReturnValue = 0;
 			DAO.Recordset tblcheck = null;
			 // ERROR: Not supported in C#: OnErrorStatement

			tblcheck = GLdb.OpenRecordset("SELECT BatchCheck FROM CheckNumbers WHERE Company = " + cmp + " AND Department = " + dpt);
			if (tblcheck.RecordCount > 0) {
				functionReturnValue = tblcheck.Fields[0].Value;
				tblcheck.Edit();
				tblcheck.Fields[0].Value = tblcheck.Fields[0].Value + 1;
				tblcheck.Update();
			} else {
				functionReturnValue = 800000;
				 // ERROR: Not supported in C#: OnErrorStatement

				tblcheck.Close();
				 // ERROR: Not supported in C#: OnErrorStatement

				tblcheck = GLdb.OpenRecordset("CheckNumbers");
				tblcheck.AddNew();
				tblcheck.Fields["Company"].Value = cmp;
				tblcheck.Fields["Department"].Value = dpt;
				tblcheck.Fields["CheckNumber"].Value = 0;
				tblcheck.Fields["BatchCheck"].Value = 800001;
				tblcheck.Update();
			}
			tblcheck.Close();
			goto GetCheckNumber_exit;
			GetCheckNumber_error:

			 // ERROR: Not supported in C#: ResumeStatement

			GetCheckNumber_exit:
			return functionReturnValue;


		}

		public static void GetJrnBatches(ref System.Windows.Forms.Form f)
		{

			string xsql = null;
 			DAO.Recordset tblBatch = null;

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstBatch.Clear()
			xsql = "SELECT * FROM BatchJrnHeader ORDER BY BatchName";
			tblBatch = GLdb.OpenRecordset(xsql);
			if (tblBatch.RecordCount > 0) {
 				//If f.lstBatch.Tag = "FP" Then f.lstBatch.Row = -1
				while (!tblBatch.EOF) {
 					//If f.lstBatch.Tag <> "FP" Then
 					//f.lstBatch.AddItem(VB6.Format(tblBatch.Fields("Key").Value, "0000# ") & tblBatch.Fields("BatchName").Value)
					//Else
 					//f.lstBatch.InsertRow(rtpad(tblBatch.Fields("BatchName").Value, 30) & Chr(9) & tblBatch.Fields("Key").Value)
					//End If
					tblBatch.MoveNext();
				}
			}
			goto GetJrnBatches_exit;
			GetJrnBatches_error:

			LogError.MsgBox_Renamed("Error while reading Batch Journal Header table: ", MsgBoxStyle.Critical, sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			GetJrnBatches_exit:


		}
		public static void GetJrnBatchEntries(ref System.Windows.Forms.Form f)
		{

			string xsql = null;
			int Row = 0;
 			DAO.Recordset tblBatch = null;

			 // ERROR: Not supported in C#: OnErrorStatement

 			xsql = "SELECT * FROM BatchJrnEntries WHERE Key = ";
			//& f.lstBatch.ColList
			tblBatch = GLdb.OpenRecordset(xsql);
			if (tblBatch.RecordCount > 0) {
				Row = 1;
				while (!tblBatch.EOF) {
					var _with1 = f;
 					//.grdEntries.Row = Row
 					//.grdEntries.Col = 1
 					//.lstAccounts.ColumnSearch = 1
 					//.lstAccounts.SearchText = tblBatch.Fields("Account").Value
 					//.lstAccounts.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch
 					//.lstAccounts.Action = LpLib.enumAction.ActionSearch
  					//.grdEntries.TypeComboBoxIndex = .lstAccounts.SearchIndex
 					//.lstAccounts.Row = .lstAccounts.SearchIndex
 					//.lstAccounts.Col = 0
  					//.grdEntries.Text = .lstAccounts.ColList
 					//.grdEntries.Col = 2
 					//.lstDepartments.ColumnSearch = 1
 					//.lstDepartments.SearchText = tblBatch.Fields("Department").Value
 					//.lstDepartments.Action = LpLib.enumAction.ActionSearch
  					//.grdEntries.TypeComboBoxIndex = .lstDepartments.SearchIndex
 					//.lstDepartments.Row = .lstDepartments.SearchIndex
 					//.lstDepartments.Col = 0
  					//.grdEntries.Text = .lstDepartments.ColList
 					//.grdEntries.Col = 3
 					//.grdEntries.Value = tblBatch.Fields("Description").Value
 					//.grdEntries.Col = 4
 					//.grdEntries.Value = lpad(VB6.Format(tblBatch.Fields("Amount").Value, "######0.00"), 11)
					Row = Row + 1;
					tblBatch.MoveNext();
				}
			}
			tblBatch.Close();
 			tblBatch = null;
			goto GetJrnBatchEntries_exit;
			GetJrnBatchEntries_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString() + " While getting Batch Journal Entries");
			 // ERROR: Not supported in C#: ResumeStatement

			GetJrnBatchEntries_exit:


		}
		public static string get_majorCategory(ref string m)
		{

 			DAO.Recordset tblTemp = null;
			string xsql = null;
			string x = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			x = "";
			xsql = "SELECT Key, SubOf FROM Category WHERE Key = '" + m + "'";
			tblTemp = GLdb.OpenRecordset(xsql);
			if (tblTemp.RecordCount > 0) {
				while (tblTemp.Fields["SubOf"].Value != "0_") {
					xsql = "SELECT Key, SubOf FROM Category WHERE Key = '" + tblTemp.Fields["SubOf"].Value + "'";
					tblTemp = GLdb.OpenRecordset(xsql);
					if (tblTemp.RecordCount == 0) {
						goto get_majorCategory_exit;
					}
				}
				x = tblTemp.Fields["Key"].Value;
			}
			goto get_majorCategory_exit;
			get_majorCategory_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			get_majorCategory_exit:

			return x;

		}
		public static void GetDepartments(ref System.Windows.Forms.Form f, ref int c = 0)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstDepartments.Clear()
 			//f.lstDepartments.Text = ""
 			if ((c == null) | c == 0) {
				tblDepartment = GLdb.OpenRecordset("SELECT * FROM Department ORDER BY Name");
			} else {
				tblDepartment = GLdb.OpenRecordset("SELECT * FROM Department WHERE [Company Number] = " + c + " ORDER BY Name");
			}
			if (tblDepartment.RecordCount > 0) {
 				//If f.lstDepartments.Tag = "FP" Then f.lstDepartments.Row = -1
				while (!tblDepartment.EOF) {
 					//If f.lstDepartments.Tag <> "FP" Then
 					//f.lstDepartments.AddItem(VB6.Format(tblDepartment.Fields("Department Number").Value, "000#") & " " & tblDepartment.Fields("Name").Value)
					//Else
 					//f.lstDepartments.InsertRow(rtpad(tblDepartment.Fields("Name").Value, 50) & Chr(9) & tblDepartment.Fields("Department Number").Value)
					//End If
					tblDepartment.MoveNext();
				}
			}
			goto GetDepartments_exit;
			GetDepartments_error:

			if (Err().Number != 3021) {
				LogError.MsgBox_Renamed("Error while reading Departments table: ", MsgBoxStyle.Critical, sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetDepartments_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblDepartment.Close();

		}
		public static int getAccounts(ref System.Windows.Forms.Control lst, ref object t = null, ref string k = "", ref bool activeOnly = true)
		{
			int functionReturnValue = 0;
			object xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			functionReturnValue = 0;
 			//lst.Clear()
 			xsql = "SELECT * FROM Accounts";
 			if (cashflag | ((k != null) & !string.IsNullOrEmpty(Strings.Trim(k))) | activeOnly) {
 				xsql = xsql + " WHERE ";
			}
			if (cashflag) {
 				xsql = xsql + "MajorCategory = '001_'" + (activeOnly ? " AND " : "");
			}
 			if ((k != null) & !string.IsNullOrEmpty(Strings.Trim(k))) {
 				xsql = xsql + "MajorCategory = '" + Strings.Trim(k) + "'" + (activeOnly ? " AND " : "");

			}
 			if (activeOnly)
				xsql = xsql + "active = True";
 			if ((t != null)) {
				switch (t) {
					case 0:
 						xsql = xsql + " ORDER BY Description";
						break;
					case 1:
 						xsql = xsql + " ORDER BY MajorCategory, MinorCategory, [Account Number]";
						break;
					case 2:
 						xsql = xsql + " ORDER BY OldAccountNumber, OldSubNumber";
						break;
				}
			} else {
 				xsql = xsql + " ORDER BY Description";
			}
 			tblAccounts = GLdb.OpenRecordset(xsql);
			if (tblAccounts.RecordCount > 0) {
				tblAccounts.MoveLast();
				functionReturnValue = tblAccounts.RecordCount;
				tblAccounts.MoveFirst();
 				//If lst.Tag = "FP" Then lst.Row = -1
				while (!tblAccounts.EOF) {
					if (lst.Tag != "FP") {
 						//lst.AddItem(VB6.Format(tblAccounts.Fields("Key").Value, "000#") & " " & tblAccounts.Fields("Description").Value)
					} else {
 						if ((t == null)) {
 							//lst.InsertRow(rtpad(tblAccounts.Fields("Description").Value, 50) & Chr(9) & tblAccounts.Fields("Key").Value)
						} else {
							switch (t) {
								case 0:
									break;
 								//lst.InsertRow(rtpad(tblAccounts.Fields("Description").Value, 50) & Chr(9) & tblAccounts.Fields("Key").Value)
								case 1:
									break;
 								//lst.InsertRow(tblAccounts.Fields("MajorCategory").Value & tblAccounts.Fields("MinorCategory").Value & ":" & tblAccounts.Fields("Account Number").Value & " " & tblAccounts.Fields("Description").Value & Chr(9) & tblAccounts.Fields("Key").Value)
								case 2:
									break;
 								//lst.InsertRow(tblAccounts.Fields("OldAccountNumber").Value & "-" & tblAccounts.Fields("OldSubNumber").Value & " " & tblAccounts.Fields("Description").Value & Chr(9) & tblAccounts.Fields("Key").Value)
							}
						}
					}
					tblAccounts.MoveNext();
				}
			}
			goto GetAccounts_exit;
			GetAccounts_error:

			if (Err().Number != 3021) {
				LogError.MsgBox_Renamed("Error while reading Accounts table: ", MsgBoxStyle.Critical, sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetAccounts_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblAccounts.Close();
			return functionReturnValue;

		}
		public static void GetInvoices(ref System.Windows.Forms.Form f, ref int v)
		{
			object xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstInvoices.Clear()
 			xsql = "SELECT * FROM " + Ifilename + " WHERE VendorNumber = " + v;
			if (InvoiceBatch) {
 				xsql = xsql + " AND Batch = " + BatchNumber;
			}
 			tblinvoices = GLdb.OpenRecordset(xsql);
			if (tblinvoices.RecordCount > 0) {
				while (!tblinvoices.EOF) {
 					//f.lstInvoices.AddItem(tblinvoices.Fields("InvoiceNumber"))
					tblinvoices.MoveNext();
				}
			}
			goto GetInvoices_exit;
			GetInvoices_error:

			if (Err().Number != 3021 & Err().Number != 3131) {
				LogError.MsgBox_Renamed("Error while reading Invoices table: ", MsgBoxStyle.Critical, sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetInvoices_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblinvoices.Close();

		}
		public static short GetInvoice(ref int v, ref string i, ref int k = 0)
		{

			string xsql = null;

			xsql = "SELECT * FROM " + Ifilename;
			if (k > 0) {
				xsql = xsql + " WHERE InvoiceKey = " + k;
			} else {
				xsql = xsql + " WHERE VendorNumber = " + v + " AND InvoiceNumber = '" + i + "'";
			}
			tblinvoices = GLdb.OpenRecordset(xsql);
			return tblinvoices.RecordCount;

		}
		public static void GetJournals(ref System.Windows.Forms.Form f)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

 			//f.lstJournals.Clear()
			tblJournals = GLdb.OpenRecordset("SELECT * FROM JournalNames ORDER BY JournalName");
			tblJournals.MoveFirst();
			while (!tblJournals.EOF) {
 				//f.lstJournals.AddItem(Trim(tblJournals.Fields("JournalName").Value))
				tblJournals.MoveNext();
			}
			goto GetJournals_exit;
			GetJournals_error:

			if (Err().Number != 3021) {
				LogError.MsgBox_Renamed("Error while reading Journals table: ", MsgBoxStyle.Critical, sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetJournals_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblJournals.Close();

		}
//******************************************************************************
		public static void Post_Invoices()
		{
			object rc = null;
			//******************************************************************************
			// This routine will interate through the Invoices table and post any invoice
			// with a non-zero check number and valid check date

  			DAO.Recordset tblInvoice = null;
			DAO.Recordset tblControl = null;
			short cnt = 0;
			int Batch = 0;
			string desc = null;
			PostInvoice_type invp = default(PostInvoice_type);
			short postflag = 0;
 			DAO.Recordset tblDist = null;

			//GLdb.Close
			//On Error GoTo post_invoices_db_error
			//Set GLdb = DBEngine.Workspaces(0).OpenDatabase(DefaultDataBase)

			 // ERROR: Not supported in C#: OnErrorStatement

			cnt = 0;
			tblControl = GLdb.OpenRecordset("Control");
			postflag = tblControl.Fields["GLMethod"].Value;
			Batch = tblControl.Fields["GLBatchNumber"].Value + 1;
			desc = tblControl.Fields["GlBatchDesc"].Value;
			GLCashAcct = tblControl.Fields["GLCashAccount"].Value;
			tblControl.Edit();
			tblControl.Fields["GLBatchNumber"].Value = Batch;
			tblControl.Update();
			tblControl.Close();
			scan = Conversion.Val(Interaction.GetSetting("Wingl", "Options", "Windoc", "0"));
			if (scan) {
				dbname = Interaction.GetSetting("Wingl", "Options", "WindocFile", "");
				if (!string.IsNullOrEmpty(Strings.Trim(dbname))) {
					cnn = new clsConnection();
  					rc = cnn.OpenDB("admin", dbname, ADODB.ConnectModeEnum.adModeShareDenyNone);
 					if (rc != 0) {
						LogError.MsgBox_Renamed("Error openning Windoc Database (" + dbname + ") Paid invoices will not be posted to Windoc: " + Err().Description);
						scan = false;
					} else {
						uniqueID = Convert.ToString(DateAndTime.Now);
						rss = new ADODB.Recordset();
						 // ERROR: Not supported in C#: OnErrorStatement

  						rss.Open("SELECT * FROM interface", cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
						if (Err().Number != 0) {
							LogError.MsgBox_Renamed("Error openning 'interface' table (paid invoices will not be posted to Windoc): " + Err().Description);
							scan = false;
 							cnn.CloseDB();
						}
					}
				} else {
					LogError.MsgBox_Renamed("Windoc Database not specified. Paid invoices will not be posted to Windoc.");
					scan = false;
				}
			}

			tblInvoice = GLdb.OpenRecordset("Invoices");
			while (!tblInvoice.EOF) {
 				if (tblInvoice.Fields["CheckNumber"].Value > 0 & !Information.IsDBNull(tblInvoice.Fields["CheckDate"].Value) & tblInvoice.Fields["AllocationFlag"].Value) {
					invp.amountpaid = tblInvoice.Fields["Amount"].Value - tblInvoice.Fields["DiscountAmount"].Value;
					invp.amounttopay = 0;
					invp.Batch = Batch;
					invp.CheckNumber = tblInvoice.Fields["CheckNumber"].Value;
					invp.datelastpaid = tblInvoice.Fields["CheckDate"].Value;
					invp.CheckDate = tblInvoice.Fields["CheckDate"].Value;
					invp.payflag = 4;
					Post_Invoice(ref tblInvoice.Fields["InvoiceKey"].Value, ref 1, ref Batch, ref GLCashAcct, ref desc, ref invp, ref true);
					cnt = cnt + 1;
				}
				tblInvoice.MoveNext();
			}
			tblInvoice.Close();
			tblDist = GLdb.OpenRecordset("InvoiceDistributions");
			while (!tblDist.EOF) {
				tblInvoice = GLdb.OpenRecordset("SELECT * FROM Invoices WHERE InvoiceKey = " + tblDist.Fields["InvoiceKey"].Value);
				if (tblInvoice.RecordCount == 0 | tblInvoice.EOF) {
					tblDist.Delete();
				}
				tblInvoice.Close();
				tblDist.MoveNext();
			}
			tblDist.Close();
			LogError.MsgBox_Renamed(Convert.ToString(cnt) + " Invoices posted.", , "Post Invoices");
			goto post_invoices_exit;
			post_invoices_db_error:

			LogError.MsgBox_Renamed("GL database in use.  Cannot post invoices at this time.");
			GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(DefaultDataBase, false);
			 // ERROR: Not supported in C#: ResumeStatement

			post_invoices_error:

			 // ERROR: Not supported in C#: ResumeStatement

			post_invoices_exit:

			GLdb.Close();
			GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(DefaultDataBase, false);

		}
//******************************************************************************
		public static short Post_Invoice(ref int inv, ref short postflag, ref int Batch, ref int CashAcct, ref string desc, ref PostInvoice_type Invoice, ref bool docOpen)
		{
			object i = null;
			object fi = null;
			//******************************************************************************

			// This routine will take the InvoiceKey of one invoice and do all of the
			// posting: GL, Check Register, etc.
			//
			// Return of 0 is good, -1 means no distribution records were found, and
			//           -2 means another error occured

   			DAO.Recordset tbInvDist = null;
			DAO.Recordset tbInvoice = null;
			DAO.Recordset tbVendors = null;
   			DAO.Recordset tbTran = null;
			DAO.Recordset tbCheckReg = null;
			DAO.Recordset tbInvHist = null;
			string xsql = null;
			short rc = 0;
			short seq = 0;
			decimal totCredit = default(decimal);
			int xdpt = 0;
			int xcmp = 0;
			decimal disc = default(decimal);
			string xjrn = null;
			string GLDesc = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			scan = Conversion.Val(Interaction.GetSetting("Wingl", "Options", "Windoc", "0"));
			if (Invoice.payroll)
				postflag = 1;
			// 09/19/2000 to ensure that a payroll check does not
			postflag = 1;
			// result in each line item producing a credit trans.
 			fi = FreeFile();
 			FileSystem.FileOpen(fi, appath + "aplaser.log", OpenMode.Append);
			totCredit = 0;
			seq = 0;
			tbInvoice = GLdb.OpenRecordset("SELECT * FROM Invoices WHERE InvoiceKey = " + inv);
			if (tbInvoice.RecordCount > 0 & Invoice.payflag == 4 & Invoice.CheckNumber > 0) {
				DAODBEngine_definst.Workspaces(0).BeginTrans();
				xsql = "SELECT VendorName, CurrentPurchases FROM Vendors WHERE VendorNumber = " + tbInvoice.Fields["VendorNumber"].Value;
				tbVendors = GLdb.OpenRecordset(xsql);
				if (tbVendors.RecordCount > 0) {
					GLDesc = Strings.Trim(tbVendors.Fields["VendorName"].Value);
					tbVendors.Edit();
					tbVendors.Fields["CurrentPurchases"].Value = tbVendors.Fields["CurrentPurchases"].Value + Invoice.amountpaid;
					tbVendors.Update();
				} else {
					GLDesc = "Vendor# " + tbInvoice.Fields["VendorNumber"].Value;
				}
				GLDesc = GLDesc + ", Invoice# " + tbInvoice.Fields["InvoiceNumber"].Value;
				tbVendors.Close();
				disc = tbInvoice.Fields["DiscountAmount"].Value;
				xsql = "SELECT * FROM InvoiceDistributions WHERE InvoiceKey = " + inv;
				tbInvDist = GLdb.OpenRecordset(xsql);
				if (tbInvDist.RecordCount > 0) {
 					//GoSub Post_Check
					while (!tbInvDist.EOF) {
						xdpt = tbInvDist.Fields["Department"].Value;
						xcmp = tbInvDist.Fields["Company"].Value;
 						xjrn = WinGlobal.rtpad(ref (Information.IsDBNull(tbInvDist.Fields["Journal"].Value) ? " " : tbInvDist.Fields["Journal"].Value), ref 20);
						if (string.IsNullOrEmpty(Strings.Trim(xjrn)))
							xjrn = "*";
 						//GoSub Post_Debit ' to ApChecks & ApTrans
						// Credit for each debit?
						if (postflag == 0) {
 							//GoSub Post_Credit ' yes
						} else {
							totCredit = totCredit + tbInvDist.Fields["Amount"].Value - disc;
							// no, accumulate credits
						}
						disc = 0;
						//11/26/2000 *******************************************************************
						//* Was not deleting Invoice Distribution records.
						//******************************************************************************
						tbInvDist.Delete();

						tbInvDist.MoveNext();
					}
					if (postflag == 1) {
 						//GoSub Post_Credit
					}
					tbInvoice.Edit();
					tbInvoice.Fields["AmountPaid"].Value = Invoice.amountpaid;
					tbInvoice.Fields["AmountToPay"].Value = Invoice.amounttopay;
					tbInvoice.Fields["Batch"].Value = Invoice.Batch;
					tbInvoice.Fields["CheckNumber"].Value = Invoice.CheckNumber;
					tbInvoice.Fields["DateLastPaid"].Value = Invoice.datelastpaid;
					tbInvoice.Fields["CheckDate"].Value = Invoice.CheckDate;
					tbInvoice.Fields["PayFlag"].Value = Invoice.payflag;
					tbInvoice.Update();
					tbInvoice.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(tbInvoice.LastModified);
					if (tbInvoice.Fields["AmountPaid"].Value == tbInvoice.Fields["Amount"].Value - tbInvoice.Fields["DiscountAmount"].Value) {
 						//GoSub Delete_Invoice
					}
					DAODBEngine_definst.Workspaces(0).CommitTrans();
 					FileSystem.PrintLine(fi, "Commit=   cmp:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.Company, "###0"), ref 4) + " vnd:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.vendor, "####0"), ref 5) + " inv: " + Invoice.invnumber + " chk:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.CheckNumber, "######"), ref 6) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.amountpaid, " ###,###,##0.00"), ref 15));
					rc = 0;
				} else {
					DAODBEngine_definst.Workspaces(0).Rollback();
 					FileSystem.PrintLine(fi, "Rollback= cmp:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.Company, "###0"), ref 4) + " vnd:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.vendor, "####0"), ref 5) + " inv: " + Invoice.invnumber + " chk:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.CheckNumber, "######"), ref 6) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.amountpaid, " ###,###,##0.00"), ref 15));
					xsql = "Invoice# " + Invoice.invnumber + ", Vendor# " + Invoice.vendor;
					xsql = xsql + " Check# " + Invoice.CheckNumber + " NOT POSTED (no distribution records)";
 					LogError.LogError(xsql, -1, sProcID);
					rc = -1;
				}
			}
			goto Post_Invoice_exit;
			Post_Invoice_error:

			DAODBEngine_definst.Workspaces(0).Rollback();
 			FileSystem.PrintLine(fi, "Rollback= cmp:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.Company, "###0"), ref 4) + " vnd:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.vendor, "####0"), ref 5) + " inv: " + Invoice.invnumber + " chk:" + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.CheckNumber, "######"), ref 6) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Invoice.amountpaid, " ###,###,##0.00"), ref 15));
			xsql = "Invoice# " + Invoice.invnumber + ", Vendor# " + Invoice.vendor;
			xsql = xsql + " Check# " + Invoice.CheckNumber + " NOT POSTED (" + Err().Description + ")";
 			LogError.LogError(xsql, -1, sProcID);
			rc = -2;
			 // ERROR: Not supported in C#: ResumeStatement

			Post_Invoice_Check_error:

			if (Err().Number == 3022) {
				tbCheckReg.Fields["sequence"].Value = tbCheckReg.Fields["sequence"].Value + 1;
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				goto Post_Invoice_error;
			}
			Post_Debit:

			// to GL
			tbTran = GLdb.OpenRecordset("ApTrans");
			tbTran.AddNew();
			tbTran.Fields["BatchNumber"].Value = Batch;
			tbTran.Fields["Company"].Value = tbInvDist.Fields["Company"].Value;
			tbTran.Fields["Department"].Value = tbInvDist.Fields["Department"].Value;
			tbTran.Fields["Account"].Value = tbInvDist.Fields["Account"].Value;
			tbTran.Fields["Date"].Value = Invoice.datelastpaid;
			tbTran.Fields["Amount"].Value = tbInvDist.Fields["Amount"].Value - disc;
			tbTran.Fields["Description"].Value = Strings.Left(GLDesc, 60);
			tbTran.Fields["Journal"].Value = xjrn;
			tbTran.Fields["CheckNumber"].Value = Invoice.CheckNumber;
			//tbTran("MajorCategory") = tbInvoice("Company")
			//tbTran("MinorCategory") = tbInvoice("Department")
			tbTran.Update();
			tbTran.Close();

			// to Checks
			tbCheckReg = GLdb.OpenRecordset("ApChecks");
			tbCheckReg.AddNew();
			tbCheckReg.Fields["Company"].Value = tbInvoice.Fields["Company"].Value;
			tbCheckReg.Fields["Department"].Value = tbInvoice.Fields["Department"].Value;
			tbCheckReg.Fields["DistCompany"].Value = tbInvDist.Fields["Company"].Value;
			tbCheckReg.Fields["DistDepartment"].Value = tbInvDist.Fields["Department"].Value;
			tbCheckReg.Fields["CheckNumber"].Value = Invoice.CheckNumber;
			tbCheckReg.Fields["Sequence"].Value = seq;
			tbCheckReg.Fields["Date"].Value = Invoice.datelastpaid;
			tbCheckReg.Fields["Amount"].Value = tbInvDist.Fields["Amount"].Value - disc;
			tbCheckReg.Fields["Vendor/Account"].Value = tbInvDist.Fields["Account"].Value;
			 // ERROR: Not supported in C#: OnErrorStatement

			tbCheckReg.Update();
			 // ERROR: Not supported in C#: OnErrorStatement

			tbCheckReg.Close();
			seq = seq + 1;
 			return 0;
			Post_Credit:

			tbTran = GLdb.OpenRecordset("ApTrans");
			tbTran.AddNew();
			tbTran.Fields["BatchNumber"].Value = Batch;
			tbTran.Fields["Company"].Value = tbInvoice.Fields["Company"].Value;
			tbTran.Fields["Department"].Value = tbInvoice.Fields["Department"].Value;
			tbTran.Fields["Account"].Value = CashAcct;
			tbTran.Fields["Date"].Value = Invoice.datelastpaid;
			if (postflag == 0) {
				tbTran.Fields["Amount"].Value = -(tbInvDist.Fields["Amount"].Value - disc);
			} else {
				tbTran.Fields["Amount"].Value = -(totCredit - disc);
				xsql = desc;
				totCredit = 0;
			}
			tbTran.Fields["Description"].Value = Strings.Left(GLDesc, 60);
			tbTran.Fields["Journal"].Value = xjrn;
			tbTran.Fields["CheckNumber"].Value = Invoice.CheckNumber;
			tbTran.Update();
			tbTran.Close();
 			return 0;
			Post_Check:

			tbCheckReg = GLdb.OpenRecordset("ApChecks");
			tbCheckReg.AddNew();
			tbCheckReg.Fields["Company"].Value = tbInvoice.Fields["Company"].Value;
			tbCheckReg.Fields["Department"].Value = tbInvoice.Fields["Department"].Value;
			tbCheckReg.Fields["CheckNumber"].Value = Invoice.CheckNumber;
			tbCheckReg.Fields["Sequence"].Value = seq;
			tbCheckReg.Fields["Date"].Value = Invoice.CheckDate;
			tbCheckReg.Fields["Amount"].Value = Invoice.amountpaid;
			tbCheckReg.Fields["Vendor/Account"].Value = tbInvoice.Fields["VendorNumber"].Value;
			 // ERROR: Not supported in C#: OnErrorStatement

			tbCheckReg.Update();
			if (Err().Number == 3022) {
				tbCheckReg.Close();
				xsql = "SELECT Amount, Date FROM ApChecks WHERE Company = " + tbInvoice.Fields["Company"].Value + " AND Department = " + tbInvoice.Fields["Department"].Value;
				xsql = xsql + " AND CheckNumber = " + tbInvoice.Fields["CheckNumber"].Value + " AND Sequence = 0";
				tbCheckReg = GLdb.OpenRecordset(xsql);
				if (tbCheckReg.RecordCount > 0) {
					 // ERROR: Not supported in C#: OnErrorStatement

					tbCheckReg.Edit();
					tbCheckReg.Fields["Amount"].Value = tbCheckReg.Fields["Amount"].Value + Invoice.amountpaid;
					tbCheckReg.Fields["Date"].Value = tbInvoice.Fields["CheckDate"].Value;
					tbCheckReg.Update();
				}
			} else {
				if (Err().Number != 0) {
					 // ERROR: Not supported in C#: OnErrorStatement

					 // ERROR: Not supported in C#: ErrorStatement

				}
			}
			tbCheckReg.Close();
			seq = seq + 1;
 			return 0;
			Delete_Invoice:

			tbInvHist = GLdb.OpenRecordset("InvoiceHistory");
			tbInvHist.AddNew();
			for (i = 0; i <= tbInvoice.Fields.Count - 1; i++) {
				tbInvHist.Fields[i].Value = tbInvoice.Fields[i].Value;
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			tbInvHist.Update();
			if (Err().Number == 3022) {

			}
			tbInvHist.Close();
			if (scan & !batchProcess)
				docInvoice(tbInvoice, true, docOpen);
			tbInvoice.Delete();
 			return 0;
			Post_Invoice_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

 			FileSystem.FileClose(fi);
			return rc;

		}
//*****************************************
		public static string scanInvoice(ref bool Batch = false, ref bool PrePaid = false, ref bool db = true)
		{
			//*****************************************
			string s = null;
			 // ERROR: Not supported in C#: OnErrorStatement

			s = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(invpGlobal.Company, "00") + ";" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(invpGlobal.Department, "000") + ";" + (PrePaid ? "paid" : "unpaid") + ";" + Strings.Trim(Conversion.Str(invpGlobal.vendor));
			s = s + ";" + Strings.Trim(invpGlobal.invnumber) + ";" + invpGlobal.date_Renamed;
			s = s + ";" + (PrePaid ? invpGlobal.CheckDate : invpGlobal.dueDate) + ";.bmp;" + Strings.Trim(vName) + ": " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(invpGlobal.amountpaid, "###,###,##0.00") + (PrePaid ? ": CHK " + Conversion.Str(invpGlobal.CheckNumber) : "");
			if (db) {
  				rss.Open("SELECT * FROM interface", cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
				 // ERROR: Not supported in C#: OnErrorStatement

 				rss.AddNew();
 				rss("prog_id") = "wingl" + (Batch ? "-batch" : "");
 				rss("app_id") = uniqueID;
 				rss("desc") = s;
 				rss.update();
 				rss.Close();
			}
			goto scanInvoice_exit;
			scanInvoice_error:

			LogError.MsgBox_Renamed("Error Saving Windoc interface record: " + Err().Description);
			 // ERROR: Not supported in C#: ResumeStatement

			scanInvoice_exit:


			return s;

		}

		public static void docInvoice(DAO.Recordset tbl, ref bool paid, ref bool docOpen, ref bool edit = false, ref string oldInvoice = "")
		{
			object rc = null;
			string s = null;
			 // ERROR: Not supported in C#: OnErrorStatement


			if (!docOpen) {
				dbname = Interaction.GetSetting("Wingl", "Options", "WindocFile", "");
				if (!string.IsNullOrEmpty(Strings.Trim(dbname))) {
					cnn = new clsConnection();
  					rc = cnn.OpenDB("admin", dbname, ADODB.ConnectModeEnum.adModeShareDenyNone);
 					if (rc != 0) {
						goto docInvoice_exit;
					} else {
						uniqueID = Convert.ToString(DateAndTime.Now);
						rss = new ADODB.Recordset();
						 // ERROR: Not supported in C#: OnErrorStatement

  						rss.Open("SELECT * FROM interface", cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
						if (Err().Number != 0) {
 							cnn.CloseDB();
							goto docInvoice_exit;
						}
					}
				} else {
					goto docInvoice_exit;
				}
			}

 			rss.AddNew();
 			rss("prog_id") = "wingl";
 			rss("app_id") = uniqueID;
			if (edit == false) {
				// Add check number to description
				s = tbl.Fields["Company"].Value + ";" + tbl.Fields["Department"].Value + ";" + (paid ? "paid;" : "unpaid;") + tbl.Fields["VendorNumber"].Value;
				s = s + ";" + Strings.Trim(tbl.Fields["InvoiceNumber"].Value) + ";" + tbl.Fields["Date"].Value;
				s = s + ";" + tbl.Fields["PayDueDate"].Value + ";.bmp;" + tbl.Fields["CheckNumber"].Value + ";" + Strings.Trim(vName) + ":" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["Amount"].Value - tbl.Fields["DiscountAmount"].Value, "###,###,##0.00");
			} else {
				s = tbl.Fields["Company"].Value + ";" + tbl.Fields["Department"].Value + ";" + "edit" + ";" + tbl.Fields["VendorNumber"].Value;
				s = s + ";" + Strings.Trim(tbl.Fields["InvoiceNumber"].Value) + ";" + oldInvoice;
			}
 			rss("desc") = s;
 			rss.update();
			if (!docOpen) {
 				cnn.CloseDB();
			}
			goto docInvoice_exit;
			docInvoice_error:

			LogError.MsgBox_Renamed("Error Saving Windoc interface record: " + Err().Description);
			 // ERROR: Not supported in C#: ResumeStatement

			docInvoice_exit:


		}
		public static int getDocument(ref string inv, ref string v)
		{
			int functionReturnValue = 0;
			object xsql = null;

			scan = Conversion.Val(Interaction.GetSetting("Wingl", "Options", "Windoc", "0"));

			functionReturnValue = -1;
			if (scan) {
				 // ERROR: Not supported in C#: OnErrorStatement

				rsd = new ADODB.Recordset();
				 // ERROR: Not supported in C#: OnErrorStatement

				Err().Clear();
 				xsql = "SELECT * FROM Documents WHERE Vendor = '" + v + "' AND Invoice = '" + Strings.Trim(inv) + "'";
  				rsd.Open(xsql, cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic);
 				rsd.MoveFirst();
				if (Err().Number == 0) {
 					functionReturnValue = rsd("document_id");
				} else {
					functionReturnValue = 0;
				}
 				rsd.Close();
			}
			return functionReturnValue;

		}
		public static void deleteDocument(ref int docNum)
		{
			object xsql = null;

			string filename = null;
			string docPath = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			rsd = new ADODB.Recordset();
			 // ERROR: Not supported in C#: OnErrorStatement

			Err().Clear();
			docPath = WinGlobal.GetPath(ref Interaction.GetSetting("WinGl", "Options", "WindocFile", ""));
			if (string.IsNullOrEmpty(Strings.Trim(docPath))) {
				return;
			}
			docPath = docPath + "Documents\\";
 			xsql = "SELECT * FROM Documents WHERE document_id = " + docNum;
  			rsd.Open(xsql, cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic);
 			rsd.MoveFirst();
			if (Err().Number == 0) {
 				while (!rsd.EOF) {
 					filename = docPath + rsd("filename");
					FileSystem.Kill(filename);
 					rsd.MoveNext();
				}
 				rsd.Close();
 				cnn.cn.Execute("DELETE FROM Documents WHERE document_id = " + docNum);
			}

		}

		public static void deleteDocuments(ref int chkNum, ref int vndNum, ref short cmp, ref short dpt)
		{

 			DAO.Recordset tblHist = null;
			string xsql = null;
			int docNum = 0;

			xsql = "SELECT InvoiceNumber,AmountPaid FROM InvoiceHistory WHERE CheckNumber = " + chkNum + " AND VendorNumber = " + vndNum;
			xsql = xsql + " AND Company = " + cmp + " AND Department = " + dpt;
			tblHist = GLdb.OpenRecordset(xsql);
			while (!tblHist.EOF) {
				docNum = getDocument(ref tblHist.Fields["InvoiceNumber"].Value, ref Strings.Trim(Conversion.Str(vndNum)));
				if (docNum > 0) {
					deleteDocument(ref docNum);
				}
				tblHist.MoveNext();
			}
			tblHist.Close();

		}
		public static void PostAccountsPayable()
		{
			object r = null;

  			DAO.Recordset tblApTrans = null;
			DAO.Recordset tblTransTemp = null;
			decimal total = default(decimal);
 			object xrpt = null;
			DAO.Recordset tblControl = null;
			short GLMethod = 0;
			int CashAcct = 0;
			int xcmp = 0;
			int xdpt = 0;
			string xjrn = null;
			string BatchDesc = null;
			System.DateTime xdate = default(System.DateTime);

			 // ERROR: Not supported in C#: OnErrorStatement

			tblControl = GLdb.OpenRecordset("Control");
 			GLMethod = (Information.IsDBNull(tblControl.Fields["GLMethod"].Value) ? 0 : tblControl.Fields["GLMethod"].Value);
			if (GLMethod == 1) {
 				CashAcct = (Information.IsDBNull(tblControl.Fields["GLCashAccount"].Value) ? -1 : tblControl.Fields["GLCashAccount"].Value);
				if (CashAcct == -1) {
					Interaction.MsgBox("Cannot load Accounts Payable with BATCH option without a valid Cash Account.");
					goto PostAccountsPayable_exit;
				}
 				BatchDesc = (Information.IsDBNull(tblControl.Fields["GLBatchDesc"].Value) ? "Payments" : tblControl.Fields["GLBatchDesc"].Value);
			}
			tblControl.Close();

			total = 0;

			tblApTrans = GLdb.OpenRecordset("SELECT * FROM ApTrans ORDER BY Company, Department, Date, Account");
			tblTransTemp = GLdb.OpenRecordset("TransactionTemp");

 			r = tblApTrans.RecordCount;
 			if (r == 0) {
				Interaction.MsgBox("No Transactions to Post.", , "Post Accounts Payable");
				return;
			}
			tblApTrans.MoveFirst();
			xcmp = tblApTrans.Fields["Company"].Value;
			xdpt = tblApTrans.Fields["Department"].Value;
			xdate = tblApTrans.Fields["Date"].Value;
			xjrn = tblApTrans.Fields["Journal"].Value;
			while (!tblApTrans.EOF) {
				if (GLMethod == 1 & (xcmp != tblApTrans.Fields["Company"].Value | xdpt != tblApTrans.Fields["Department"].Value)) {
					if (total != 0) {
						tblTransTemp.AddNew();
						tblTransTemp.Fields["CompanyNumber"].Value = xcmp;
						tblTransTemp.Fields["DepartmentNumber"].Value = xdpt;
						tblTransTemp.Fields["AccountNumber"].Value = CashAcct;
						tblTransTemp.Fields["Date"].Value = xdate;
						tblTransTemp.Fields["Journal"].Value = xjrn;
						tblTransTemp.Fields["Source"].Value = "Accounts Payable";
						tblTransTemp.Fields["Description"].Value = BatchDesc;
						tblTransTemp.Fields["CheckNumber"].Value = 0;
						tblTransTemp.Fields["Amount"].Value = total;
						tblTransTemp.Update();
					}
					xcmp = tblApTrans.Fields["Company"].Value;
					xdpt = tblApTrans.Fields["Department"].Value;
					xdate = tblApTrans.Fields["Date"].Value;
					total = 0;
				}
				DAODBEngine_definst.Workspaces(0).BeginTrans();
				if (GLMethod == 1 & tblApTrans.Fields["Account"].Value == CashAcct) {
					total = total + tblApTrans.Fields["Amount"].Value;
					xjrn = tblApTrans.Fields["Journal"].Value;
				} else {
					tblTransTemp.AddNew();
					tblTransTemp.Fields["CompanyNumber"].Value = tblApTrans.Fields["Company"].Value;
					tblTransTemp.Fields["DepartmentNumber"].Value = tblApTrans.Fields["Department"].Value;
					tblTransTemp.Fields["AccountNumber"].Value = tblApTrans.Fields["Account"].Value;
					tblTransTemp.Fields["Date"].Value = tblApTrans.Fields["Date"].Value;
					tblTransTemp.Fields["Journal"].Value = WinGlobal.lpad(ref WinGlobal.noNulls(ref tblApTrans.Fields["Journal"]), ref 20);
					tblTransTemp.Fields["Source"].Value = "Accounts Payable";
					tblTransTemp.Fields["Description"].Value = tblApTrans.Fields["Description"].Value;
					tblTransTemp.Fields["CheckNumber"].Value = tblApTrans.Fields["CheckNumber"].Value;
					tblTransTemp.Fields["Amount"].Value = tblApTrans.Fields["Amount"].Value;
					tblTransTemp.Update();
				}
				tblApTrans.Delete();
				DAODBEngine_definst.Workspaces(0).CommitTrans();
				tblApTrans.MoveNext();
			}
			if (GLMethod == 1 & total != 0) {
				tblTransTemp.AddNew();
				tblTransTemp.Fields["CompanyNumber"].Value = xcmp;
				tblTransTemp.Fields["DepartmentNumber"].Value = xdpt;
				tblTransTemp.Fields["AccountNumber"].Value = CashAcct;
				tblTransTemp.Fields["Date"].Value = xdate;
				tblTransTemp.Fields["Journal"].Value = xjrn;
				tblTransTemp.Fields["Source"].Value = "Accounts Payable";
				tblTransTemp.Fields["Description"].Value = BatchDesc;
				tblTransTemp.Fields["CheckNumber"].Value = 0;
				tblTransTemp.Fields["Amount"].Value = total;
				tblTransTemp.Update();
			}
			tblApTrans.Close();
			tblTransTemp.Close();

			rptCancel = false;
			xrpt = new clsTransEditList();
 			xrpt = null;

			goto PostAccountsPayable_exit;
			PostAccountsPayable_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), , "Load From Accounts Payable");
			 // ERROR: Not supported in C#: ResumeStatement

			PostAccountsPayable_exit:


		}
		public static void PostTenantReceivables()
		{
			object xsql = null;

 			DAO.Recordset tblTransTemp = null;
			object xrpt = null;
			short fi = 0;
			string jrn = null;
 			ARtrans_type ARtrans = default(ARtrans_type);
			DAO.Recordset tblacct = null;
			int rec = 0;

			tblControl = GLdb.OpenRecordset("Control");
 			jrn = (Information.IsDBNull(tblControl.Fields["GLARJournal"].Value) ? "*" : tblControl.Fields["GLARJournal"].Value);
			tblControl.Close();

			fi = FreeFile();
			 // ERROR: Not supported in C#: OnErrorStatement

			FileSystem.FileOpen(fi, appath + "artrans", OpenMode.Input);
			FileSystem.FileClose(fi);
			 // ERROR: Not supported in C#: OnErrorStatement

			FileSystem.FileOpen(fi, appath + "artrans", OpenMode.Binary);
			tblTransTemp = GLdb.OpenRecordset("TransactionTemp");
			rec = FileSystem.Seek(fi);
 			FileSystem.FileGet(fi, ARtrans);
			while (!FileSystem.EOF(fi)) {
				if (Conversion.Val(ARtrans.amount) != 0) {
					tblTransTemp.AddNew();
					tblTransTemp.Fields["CompanyNumber"].Value = ARtrans.cmp;
					tblTransTemp.Fields["DepartmentNumber"].Value = ARtrans.dpt;
 					if (string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref ARtrans.acct))) | Information.IsDBNull(ARtrans.acct)) {
						tblTransTemp.Fields["AccountNumber"].Value = 0;
					} else {
 						xsql = "SELECT Key FROM Accounts WHERE OldAccountNumber = '" + Strings.Left(ARtrans.acct, 4) + "' AND OldSubNumber = '" + Strings.Right(ARtrans.acct, 3) + "'";
 						tblacct = GLdb.OpenRecordset(xsql);
						if (tblacct.RecordCount > 0) {
							tblTransTemp.Fields["AccountNumber"].Value = tblacct.Fields["Key"].Value;
							tblacct.Close();
						} else {
							tblTransTemp.Fields["AccountNumber"].Value = Conversion.Val(ARtrans.acct);
						}
					}
 					tblTransTemp.Fields["Date"].Value = DateAndTime.DateValue(Strings.Left(ARtrans.date_Renamed, 2) + "/" + Strings.Mid(ARtrans.date_Renamed, 3, 2) + "/" + Strings.Right(ARtrans.date_Renamed, 2));
					tblTransTemp.Fields["Journal"].Value = jrn;
					tblTransTemp.Fields["Source"].Value = "Accounts Receivable";
					tblTransTemp.Fields["Description"].Value = ARtrans.desc;
					tblTransTemp.Fields["Amount"].Value = Conversion.Val(ARtrans.amount);
					tblTransTemp.Update();
					ARtrans.amount = "0";
 					FileSystem.FilePut(fi, ARtrans, rec);
				}
				rec = FileSystem.Seek(fi);
 				FileSystem.FileGet(fi, ARtrans);
			}
			FileSystem.FileClose(fi);
			FileSystem.Kill(appath + "artrans");
			tblTransTemp.Close();
			rptCancel = false;
			xrpt = new clsTransEditList();
 			xrpt = null;
			goto PostAR_exit;
			PostAR_nofile:

			Interaction.MsgBox("There are no transactions to post.", , "Post Receivables");
			 // ERROR: Not supported in C#: ResumeStatement

			PostAR_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), , "Post Receivables");
			 // ERROR: Not supported in C#: ResumeStatement

			PostAR_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			FileSystem.FileClose(fi);
			tblTransTemp.Close();
			tblacct.Close();

		}
		public static void PostTransactions()
		{
			object x = null;
			object xsql = null;
			object i = null;
			object UserName = null;

     			DAO.Recordset tblTrans = null;
			DAO.Recordset tblBalance = null;
			DAO.Recordset tblTransTemp = null;
			DAO.Recordset tblBudget = null;
			DAO.Recordset tblacct = null;
 			DAO.Recordset tblComp = null;
			int errorcount = 0;
			int postcount = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			errorcount = 0;
			postcount = 0;
 			LogError.LogError("********************************************", ref 0, ref "", ref logTrans);
  			LogError.LogError("* User: " + UserName + ", Processing Transactions ", ref 0, ref "", ref logTrans);
 			LogError.LogError("********************************************", ref 0, ref "", ref logTrans);
			tblTransTemp = GLdb.OpenRecordset("TransactionTemp");
			tblTrans = GLdb.OpenRecordset("Transactions");
			while (!tblTransTemp.EOF) {
				tblacct = GLdb.OpenRecordset("SELECT Key FROM Accounts WHERE Key = " + tblTransTemp.Fields["AccountNumber"].Value);
				if (tblacct.RecordCount > 0) {
					DAODBEngine_definst.Workspaces(0).BeginTrans();

					// Transactions
					tblTrans.AddNew();
					for (i = 0; i <= tblTransTemp.Fields.Count - 1; i++) {
 						if (i != 5) {
 							if (i < 8) {
								tblTrans.Fields[i].Value = tblTransTemp.Fields[i].Value;
 							} else if (i == 8) {
 								tblTrans.Fields[i + 6].Value = tblTransTemp.Fields[i].Value;
							} else {
 								tblTrans.Fields[i - 1].Value = tblTransTemp.Fields[i].Value;
							}
						}
					}
					tblTrans.Update();

					// Account Balances
 					xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + tblTransTemp.Fields["CompanyNumber"].Value;
 					xsql = xsql + " AND DepartmentNumber = " + tblTransTemp.Fields["DepartmentNumber"].Value + " AND ";
 					xsql = xsql + "AccountNumber = " + tblTransTemp.Fields["AccountNumber"].Value;
 					tblBalance = GLdb.OpenRecordset(xsql);
					if (tblBalance.RecordCount > 0) {
						tblBalance.Edit();
					} else {
						tblBalance = GLdb.OpenRecordset("Balances");
						tblBalance.AddNew();
						tblBalance.Fields["CompanyNumber"].Value = tblTransTemp.Fields["CompanyNumber"].Value;
						tblBalance.Fields["DepartmentNumber"].Value = tblTransTemp.Fields["DepartmentNumber"].Value;
						tblBalance.Fields["AccountNumber"].Value = tblTransTemp.Fields["AccountNumber"].Value;
					}
					if (tblTransTemp.Fields["Date"].Value < CurrentFiscalYear(ref tblTransTemp.Fields["CompanyNumber"].Value)) {
 						x = "P_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(tblTransTemp.Fields["Date"].Value), "00");
					} else {
 						x = "C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(tblTransTemp.Fields["Date"].Value), "00");
					}
					tblBalance.Fields[x].Value = tblBalance.Fields[x].Value + tblTransTemp.Fields["Amount"].Value;
					tblBalance.Update();
					tblBalance.Close();

					// Budgets
					if (DateAndTime.Year(tblTransTemp.Fields["Date"].Value) < DateAndTime.Year(DateAndTime.Today)) {
 						xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + tblTransTemp.Fields["CompanyNumber"].Value;
 						xsql = xsql + " AND DepartmentNumber = " + tblTransTemp.Fields["DepartmentNumber"].Value + " AND ";
 						xsql = xsql + "AccountNumber = " + tblTransTemp.Fields["AccountNumber"].Value;
 						tblBudget = GLdb.OpenRecordset(xsql);
						if (tblBudget.RecordCount > 0) {
							tblBudget.Edit();
						} else {
							tblBudget = GLdb.OpenRecordset("Budgets");
							tblBudget.AddNew();
							tblBudget.Fields["CompanyNumber"].Value = tblTransTemp.Fields["CompanyNumber"].Value;
							tblBudget.Fields["DepartmentNumber"].Value = tblTransTemp.Fields["DepartmentNumber"].Value;
							tblBudget.Fields["AccountNumber"].Value = tblTransTemp.Fields["AccountNumber"].Value;
						}
 						x = "Actual" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(tblTransTemp.Fields["Date"].Value), "00");
						tblBudget.Fields[x].Value = tblBudget.Fields[x].Value + tblTransTemp.Fields["Amount"].Value;
						tblBudget.Update();
						tblBudget.Close();
					}

					// Delete Temporary Transaction Record
					tblTransTemp.Delete();

					DAODBEngine_definst.Workspaces(0).CommitTrans();
					postcount = postcount + 1;
				} else {
					errorcount = errorcount + 1;
 					LogError.LogError("Unable to find acct# " + tblTransTemp.Fields["AccountNumber"].Value + ", cmp=" + tblTransTemp.Fields["CompanyNumber"].Value + ", dpt=" + tblTransTemp.Fields["DepartmentNumber"].Value + ", vend: " + tblTransTemp.Fields["VendorNumber"].Value + ", chk: " + tblTransTemp.Fields["CheckNumber"].Value + ", amt=" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["Amount"].Value + ", desc: " + tblTransTemp.Fields["Description"].Value, "###,###,##0.00"), ref 0, ref "", ref logTrans);
				}
				tblTransTemp.MoveNext();
			}
			if (errorcount == 0) {
				Interaction.MsgBox("Transaction Posting Complete. " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(postcount, "####0") + " transactions posted.", , "Post Transactions");
			} else {
				Interaction.MsgBox("There were " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(errorcount, "####0") + " errors.  " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(postcount, "####0") + " transactions posted successfully.", , "Post Transactions");
			}
 			LogError.LogError("There were " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(errorcount, "####0") + " errors.  " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(postcount, "####0") + " transactions posted successfully.", ref 0, ref "", ref logTrans);
			goto PostTransactions_exit;
			PostTransactions_error:

			 // ERROR: Not supported in C#: OnErrorStatement

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), , "Post Transactions");
			 // ERROR: Not supported in C#: ResumeStatement

			PostTransactions_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblTransTemp.Close();
			tblTrans.Close();
			tblBalance.Close();
			tblBudget.Close();

		}
		public static string spad(ref string s, ref short l)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			return new string("*", l - Strings.Len(s)) + s;

		}
		public static void getLedgerStructure()
		{
			//***************************************************************************
			//* This routine returns an array that contains the categories and accounts *
			//* in the same sequence as the Category/Account screen. (LH)               *
			//*                                                                         *
			//* It also returns an array that contains the Category descriptions.       *
			//*                                                                         *
			//* IMPORTANT: ERASE LH when finished using it to conserve memory           *
			//***************************************************************************
 			DAO.Recordset tblcat = null;
			short l = 0;
			short m = 0;

			//ReDim LH(5, 10, 250)
			tblcat = GLdb.OpenRecordset("SELECT * FROM Category WHERE SubOf = '0_' ORDER BY Sequence");
			if (tblcat.RecordCount > 0) {
				while (!tblcat.EOF) {
					m = Conversion.Val(tblcat.Fields["Key"].Value);
					//LH(m, 0, 0).type = "C"
					//LH(m, 0, 0).Description = tblcat.Fields("CategoryDescription").Value
 					//LH(m, 0, 0).key = Trim(tblcat.Fields("Key").Value)
 					CatTotals[m, 2] = tblcat.Fields["CategoryDescription"].Value;
					l = 1;
					get_Accounts(tblcat.Fields["Key"].Value, l, m);
					get_subCategories(ref tblcat.Fields["Key"].Value, ref l, ref m);
					tblcat.MoveNext();
				}
			}
			tblcat.Close();

		}
		private static void get_Accounts(ref string k, ref short lv, ref short m, ref bool activeOnly = true)
		{
			object xsql = null;

 			DAO.Recordset tblacct = null;
			string s = null;
			short i = 0;

 			xsql = "SELECT * FROM Accounts WHERE MinorCategory = '" + k + "' " + (activeOnly ? "AND active = True" : "") + " ORDER BY [Account Number]";
 			tblacct = GLdb.OpenRecordset(xsql);
			i = 0;
			if (tblacct.RecordCount > 0) {
				while (!tblacct.EOF) {
					i = i + 1;
					//LH(m, lv, i).type = "A"
					//LH(m, lv, i).Description = Trim(tblacct.Fields("Description").Value)
 					//LH(m, lv, i).key = VB6.Format(tblacct.Fields("Key").Value, "00000000")
					tblacct.MoveNext();
				}
				lv = lv + 1;
			}
			tblacct.Close();

		}
//******************************************************************************
		private static void get_subCategories(ref string k, ref short lv, ref short m)
		{
			object xsql = null;
			//******************************************************************************

 			DAO.Recordset tblcat = null;

 			xsql = "SELECT * FROM Category WHERE SubOf = '" + k + "' ORDER BY Sequence";
 			tblcat = GLdb.OpenRecordset(xsql);
			if (tblcat.RecordCount > 0) {
				while (!tblcat.EOF) {
					//LH(m, lv, 0).type = "S"
					//LH(m, lv, 0).Description = tblcat.Fields("CategoryDescription").Value
 					//LH(m, lv, 0).key = tblcat.Fields("Key").Value
					lv = lv + 1;
					get_Accounts(tblcat.Fields["Key"].Value, lv, m);
					get_subCategories(ref tblcat.Fields["Key"].Value, ref lv, ref m);
					tblcat.MoveNext();
				}
			}
			tblcat.Close();

		}
		public static short leapyear(ref short y)
		{
			short functionReturnValue = 0;

			if (y < 50) {
				y = 2000 + y;
			}
			if (y < 100) {
				y = 1900 + y;
			}
			if (y % 4 == 0) {
				if (y % 400 == 0) {
					if (y % 1000 == 0) {
						functionReturnValue = 2;
					} else {
						functionReturnValue = 1;
					}
				} else {
					functionReturnValue = 2;
				}
			} else {
				functionReturnValue = 1;
			}
			return functionReturnValue;

		}
//******************************************************************************
		public static string fmt(ref decimal a, ref short l)
		{
			//******************************************************************************

			string x = null;
			short i = 0;

			x = "";
			for (i = 1; i <= l - 4; i++) {
				x = x + "#";
			}
			x = x + "0.00";

			return WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, x), ref l);

		}
//******************************************************************************
		public static bool InHistory(ref int vnum, ref string Invoice)
		{
			bool functionReturnValue = false;
			//******************************************************************************

 			DAO.Recordset tbl = null;
			string xsql = null;

			functionReturnValue = false;
			xsql = "SELECT InvoiceHistory.*, Company.Name, Department.Name FROM InvoiceHistory, Company, Department WHERE VendorNumber = " + vnum + " AND InvoiceNumber = '" + Invoice;
			xsql = xsql + "' AND Company.CompanyNumber = InvoiceHistory.Company AND Department.[Department Number] = InvoiceHistory.Department";
			tbl = GLdb.OpenRecordset(xsql);
			if (tbl.RecordCount > 0) {
				xsql = "This invoice may already have been paid to this vendor from:" + Constants.vbLf + Constants.vbLf + "Date: " + tbl.Fields["Date"].Value + Constants.vbLf;
				xsql = xsql + "Check# " + tbl.Fields["CheckNumber"].Value + Constants.vbLf + "Amount: " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["AmountPaid"].Value, "###,###,##0.00");
				xsql = xsql + Constants.vbLf + "Company: " + tbl.Fields["Company.Name"].Value + Constants.vbLf + "Department: " + tbl.Fields["Department.Name"].Value;
				xsql = xsql + Constants.vbLf + Constants.vbLf + "Do you wish to store this invoice anyway?";
				if (Interaction.MsgBox(xsql, MsgBoxStyle.YesNo) == MsgBoxResult.No) {
					functionReturnValue = true;
				}
			}
			return functionReturnValue;

		}
//******************************************************************************
		public static void BatchInvoiceCheck()
		{
			object bm = null;
			object seq = null;
			object y = null;
			object d = null;
			object m = null;
			object i = null;
			//******************************************************************************

  			DAO.Recordset tblBatch = null;
			DAO.Recordset tblpend = null;
  			DAO.Recordset tblBatchDist = null;
			DAO.Recordset tblpenddist = null;
			System.DateTime d1 = default(System.DateTime);
			DAO.Field fld = null;
			short StageOption = 0;
			short InvoiceOption = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			d1 = DateAndTime.Today;
			tblBatch = GLdb.OpenRecordset("Control");
			//If tblbatch("BatchDate") <= DateValue("1/1/2000") Then
			//    VBA.Interaction.MsgBox ("You must set Recurring Invoice Options before Recurring Invoices can be processed")
			//    tblbatch.Close
			//    BatchCheckDate = date
			//    GoTo BatchInvoiceCheck_exit
			//End If
			StageOption = tblBatch.Fields["RecurStaging"].Value;
			InvoiceOption = tblBatch.Fields["RecurInvoice"].Value;
			tblBatch.Edit();
			tblBatch.Fields["BatchDate"].Value = DateAndTime.Today;
			BatchCheckDate = tblBatch.Fields["BatchDate"].Value;
			tblBatch.Update();
			tblBatch.Close();

			tblBatch = GLdb.OpenRecordset("InvoicesBatch");
			if (StageOption == 0) {
				tblpend = GLdb.OpenRecordset("InvoicesPending");
				tblpenddist = GLdb.OpenRecordset("InvPendDistributions");
			} else {
				tblpend = GLdb.OpenRecordset("Invoices");
				tblpenddist = GLdb.OpenRecordset("InvoiceDistributions");
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			while (!tblBatch.EOF) {
				if (tblBatch.Fields["PayDueDate"].Value <= d1) {
 					i = 0;
					DAODBEngine_definst.Workspaces(0).BeginTrans();
 					m = DateAndTime.Month(tblBatch.Fields["PayDueDate"].Value);
 					d = DateAndTime.Day(tblBatch.Fields["PayDueDate"].Value);
 					y = DateAndTime.Year(tblBatch.Fields["PayDueDate"].Value);
					tblpend.AddNew();
					foreach (DAO.Field fld_loopVariable in tblBatch.Fields) {
						fld = fld_loopVariable;
						if (fld.Name != "InvoiceKey") {
							tblpend.Fields[i].Value = tblBatch.Fields[i].Value;
						}
 						i = i + 1;
					}
					tblpend.Fields["PayFlag"].Value = 1;
 					seq = 1;
					addseq:
					switch (InvoiceOption) {
						case 0:
							// Date + Auto Seq
    							tblpend.Fields["InvoiceNumber"].Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y * 10000 + m * 100 + d, "00000000") + "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####");
							break;
						case 1:
							// Description + Date + Auto Seq
    							tblpend.Fields["InvoiceNumber"].Value = Strings.Left(tblpend.Fields["InvoiceNumber"].Value, 11) + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y * 10000 + m * 100 + d, "00000000") + "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####");
							break;
						case 2:
							// Description + Auto Seq
 							tblpend.Fields["InvoiceNumber"].Value = Strings.Left(tblpend.Fields["Memo"].Value, 11) + "-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(seq, "#####");
							break;
						case 3:
							// Manual
							break;
						case 4:
							// Manual + Date
							break;
					}
					tblpend.Update();
 					bm = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(tblpend.LastModified);
 					tblpend.Bookmark = bm;
					tblBatchDist = GLdb.OpenRecordset("SELECT * FROM InvBatchDistributions WHERE InvoiceKey = " + tblpend.Fields["InvoiceKey"].Value);
					if (tblBatchDist.RecordCount > 0) {
						while (!tblBatchDist.EOF) {
							tblpenddist.AddNew();
							for (i = 0; i <= tblBatchDist.Fields.Count - 1; i++) {
								tblpenddist.Fields[i].Value = tblBatchDist.Fields[i].Value;
							}
							tblpenddist.Update();
							tblBatchDist.MoveNext();
						}
					}
					tblBatch.Edit();
					switch (tblBatch.Fields["PayFlag"].Value) {
						case 0:
							// On Demand
							break;
						case 1:
							// Weekly
							tblBatch.Fields["PayDueDate"].Value = tblBatch.Fields["PayDueDate"].Value + 7;
							break;
						case 2:
							// Semi-Weekly
							tblBatch.Fields["PayDueDate"].Value = tblBatch.Fields["PayDueDate"].Value + 14;
							break;
						case 3:
							// Monthly
 							if (m == 12) {
 								m = 1;
 								y = y + 1;
							} else {
 								m = m + 1;
							}
   							tblBatch.Fields["PayDueDate"].Value = DateAndTime.DateSerial(y, m, d);
							break;
						case 4:
							// Bi-Monthly
 							if (m == 11) {
 								m = 1;
 								y = y + 1;
							} else {
 								m = m + 2;
							}
   							tblBatch.Fields["PayDueDate"].Value = DateAndTime.DateSerial(y, m, d);
							break;
						case 5:
							// Quarterly
 							m = m + 3;
 							if (m > 12) {
 								m = 12 - m;
 								y = y + 1;
							}
   							tblBatch.Fields["PayDueDate"].Value = DateAndTime.DateSerial(y, m, d);
							break;
						case 6:
							// Yearly
 							y = y + 1;
   							tblBatch.Fields["PayDueDate"].Value = DateAndTime.DateSerial(y, m, d);
							break;
					}
					tblBatch.Update();
					DAODBEngine_definst.Workspaces(0).CommitTrans();
				}
				tblBatch.MoveNext();
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			tblBatch.Close();
			tblpend.Close();
			tblBatchDist.Close();
			tblpenddist.Close();

			goto BatchInvoiceCheck_exit;
			BatchInvoiceCheck_data:

			if (Err().Number == 0) {
 				seq = seq + 1;
				 // ERROR: Not supported in C#: ResumeStatement

			}
			DAODBEngine_definst.Workspaces(0).Rollback();
			BatchInvoiceCheck_error:
			LogError.MsgBox_Renamed(Err().Description, MsgBoxStyle.Critical, "Batch Invoice Processing");
			 // ERROR: Not supported in C#: ResumeStatement

			BatchInvoiceCheck_exit:

			BatchInvoiceChecked = true;

		}
//******************************************************************************
		public static void VendorClose()
		{
			object MousePointer = null;
			//******************************************************************************

			if (Interaction.MsgBox("Are you sure that you want to close the year for Vendors?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
 				MousePointer = 11;
				tblVendors = GLdb.OpenRecordset("SELECT CurrentPurchases, PriorYearPurchases FROM Vendors");
				while (!tblVendors.EOF) {
					tblVendors.Edit();
					tblVendors.Fields["PriorYearPurchases"].Value = tblVendors.Fields["CurrentPurchases"].Value;
					tblVendors.Fields["CurrentPurchases"].Value = 0;
					tblVendors.Update();
					tblVendors.MoveNext();
				}
				tblVendors.Close();
 				MousePointer = 0;
			}

		}
///******************************************************************************
		public static short Get_Company(ref int c)
		{
			short functionReturnValue = 0;
			///******************************************************************************

 			DAO.Recordset tbl = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			functionReturnValue = 0;
			if (c == CmpRec.CompanyNumber)
				goto Get_Company_exit;
			tbl = GLdb.OpenRecordset("SELECT * FROM Company WHERE CompanyNumber = " + c);
			if (tbl.RecordCount > 0) {
				CmpRec.CompanyNumber = tbl.Fields["CompanyNumber"].Value;
				CmpRec.Name = tbl.Fields["Name"].Value;
				CmpRec.FiscalYearEnd = tbl.Fields["FiscalYearEnd"].Value;
				CmpRec.CurrentFiscalYear = tbl.Fields["CurrentFiscalYear"].Value;
				CmpRec.StreetAddress = tbl.Fields["StreetAddress"].Value;
				CmpRec.City = tbl.Fields["City"].Value;
				CmpRec.State = tbl.Fields["State"].Value;
				CmpRec.Zip = tbl.Fields["Zip"].Value;
				CmpRec.Telephone = WinGlobal.noNulls(ref tbl.Fields["Telephone"]);
 				CmpRec.Fax = (Information.IsDBNull(tbl.Fields["Fax"].Value) ? "" : tbl.Fields["Fax"].Value);
 				CmpRec.Email = (Information.IsDBNull(tbl.Fields["Email"].Value) ? "" : tbl.Fields["Email"].Value);
				CmpRec.FederalID = tbl.Fields["FederalID"].Value;
 				CmpRec.StateID = (Information.IsDBNull(tbl.Fields["StateID"].Value) ? "" : tbl.Fields["StateID"].Value);
				tbl.Close();
			} else {
				functionReturnValue = Err().Number;
			}
			goto Get_Company_exit;
			Get_Company_error:

			functionReturnValue = Err().Number;
			 // ERROR: Not supported in C#: ResumeStatement

			Get_Company_exit:
			return functionReturnValue;


		}
		public static System.DateTime CurrentFiscalYear(ref int cmp = 0)
		{
			object fye = null;
			object cfy = null;

 			DAO.Recordset tbl = null;
			short fys = 0;

 			cfy = DateAndTime.Year(DateAndTime.Today);
 			fye = 12;
			 // ERROR: Not supported in C#: OnErrorStatement

			tbl = GLdb.OpenRecordset("Control");
			if (tbl.RecordCount > 0) {
 				cfy = tbl.Fields["CurrentFiscalYear"].Value;
				tbl.Close();
			}
  			if (!Information.IsDBNull(cmp) & (cmp != null) & cmp > 0) {
				Get_Company(ref cmp);
 				cfy = CmpRec.CurrentFiscalYear;
 				fye = CmpRec.FiscalYearEnd;
			}
 			fys = (fye == 12 ? 1 : fye + 1);
 			if (cfy < 2000)
				cfy = 2000;
 			return DateAndTime.DateSerial(cfy, fys, 1);

		}
		public static System.DateTime CurrentQuarter(ref int cmp, ref System.DateTime dte)
		{
			System.DateTime functionReturnValue = default(System.DateTime);
			object i = null;
			object m = null;
			object fsy = null;
			object y = null;

 			DAO.Recordset tbl = null;
			short qys = 0;
			System.DateTime fys = default(System.DateTime);
			short[] q = new short[5];

			fys = CurrentFiscalYear(ref cmp);
			if (dte >= fys) {
 				y = DateAndTime.Year(fys);
			} else {
 				y = DateAndTime.Year(dte);
			}
  			m = DateAndTime.Month(fsy);
			for (i = 1; i <= 4; i++) {
  				q[i] = m;
 				m = m + 3;
  				m = m % 12;
			}
			for (i = 1; i <= 4; i++) {
  				if (m - q[i] >= 0 & m - q[i] < 3) {
  					functionReturnValue = DateAndTime.DateSerial(y, q[i], 1);
					return functionReturnValue;
				}
			}
			return functionReturnValue;

		}
		public static void TransDateRange(ref int cmp, ref System.DateTime StartDate, ref System.DateTime EndDate)
		{
			object xsql = null;

 			DAO.Recordset tbl = null;

 			xsql = "SELECT DISTINCT Date FROM Transactions";
			if (cmp > 0) {
 				xsql = xsql + " WHERE CompanyNumber = " + cmp;
			}
 			xsql = xsql + " ORDER BY Date";
 			tbl = GLdb.OpenRecordset(xsql);
			if (!tbl.EOF) {
				StartDate = tbl.Fields["Date"].Value;
				tbl.MoveLast();
				if (DateAndTime.Year(DateAndTime.Today) > DateAndTime.Year(tbl.Fields["Date"].Value)) {
					EndDate = DateAndTime.DateSerial(DateAndTime.Year(tbl.Fields["Date"].Value) + 1, 1, 1);
				} else {
					EndDate = tbl.Fields["Date"].Value;
				}
			} else {
 				//StartDate = System.DBNull.Value
 				//EndDate = System.DBNull.Value
			}
			tbl.Close();

		}

		public static decimal AcctBal(ref int cmp, ref int acct, ref System.DateTime StartDate, ref System.DateTime EndDate, ref int dpt = 0)
		{
			decimal functionReturnValue = default(decimal);
			object xsql = null;

 			DAO.Recordset tbltran = null;

 			xsql = "SELECT SUM(Amount) as Balance FROM Transactions WHERE CompanyNumber = " + cmp + " AND ";
 			xsql = xsql + "AccountNumber = " + acct + "AND Date BETWEEN #" + StartDate + "# AND #" + EndDate + "#";
  			if ((dpt != null) & !Information.IsDBNull(dpt) & dpt > 0) {
 				xsql = xsql + " AND DepartmentNumber = " + dpt;
			}

			functionReturnValue = 0;
 			tbltran = GLdb.OpenRecordset(xsql);
			if (tbltran.RecordCount > 0) {
 				functionReturnValue = (Information.IsDBNull(tbltran.Fields["Balance"].Value) ? 0 : tbltran.Fields["Balance"].Value);
				tbltran.Close();
			}
			return functionReturnValue;

		}
		public static decimal CheckTotal(ref int cmp, ref int dpt, ref int chk)
		{
			decimal functionReturnValue = default(decimal);
			object xsql = null;

 			DAO.Recordset tblChecks = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			functionReturnValue = 0;
 			xsql = "SELECT sum(Amount) as CheckTotal FROM ApChecks WHERE Company = " + cmp + " AND ";
 			xsql = xsql + "Department = " + dpt + " AND CheckNumber = " + chk + " AND Sequence <> 0";
 			tblChecks = GLdb.OpenRecordset(xsql);
			if (tblChecks.RecordCount > 0) {
				functionReturnValue = tblChecks.Fields["CheckTotal"].Value;
				tblChecks.Close();
 				xsql = "SELECT Amount FROM ApChecks WHERE Company = " + cmp + " AND ";
 				xsql = xsql + "Department = " + dpt + " AND CheckNumber = " + chk + " AND Sequence = 0";
 				tblChecks = GLdb.OpenRecordset(xsql);
				if (tblChecks.RecordCount > 0) {
					tblChecks.Edit();
					tblChecks.Fields["Amount"].Value = functionReturnValue;
					tblChecks.Update();
					tblChecks.Close();
				}
			}
			goto CheckTotal_exit;
			CheckTotal_error:

			LogError.MsgBox_Renamed("Error occured while reading Check table", MsgBoxStyle.Critical, "CheckTotal");
			 // ERROR: Not supported in C#: ResumeStatement

			CheckTotal_exit:
			return functionReturnValue;


		}
		public static decimal MatchChkTrans(ref int cmp, ref int dpt, ref int chk, ref decimal old_amt, ref decimal new_amt)
		{
			object xsql = null;

 			DAO.Recordset tblTrans = null;

 			xsql = "SELECT * FROM Transactions WHERE CompanyNumber = " + cmp + " AND DepartmentNumber ";
 			xsql = xsql + "= " + dpt + " AND CheckNumber = " + chk + " AND Amount = " + old_amt;
 			tblTrans = GLdb.OpenRecordset(xsql);
			//If tblTrans.RecordCount > 0 Then
			//if tbltrans("


		}
		public static void StoreForm(ref System.Windows.Forms.Form frm)
		{

			System.Windows.Forms.Control c = null;
			object s = null;
			object i = null;
			bool found = false;

			 // ERROR: Not supported in C#: OnErrorStatement

			foreach (Control c_loopVariable in frm.Controls) {
				c = c_loopVariable;

				found = false;
  				i = System.DBNull.Value;

 				if (c is System.Windows.Forms.TextBox | c is AxMBText.AxTextField) {
 					s = c.Text;
					found = true;
 				} else if (c is AxLpLib.AxfpList | c is System.Windows.Forms.ListBox | c is System.Windows.Forms.ComboBox | c is AxLpLib.AxfpCombo) {
  					//s = c.ListIndex
					found = true;
 				} else if (c is System.Windows.Forms.RadioButton | c is System.Windows.Forms.CheckBox) {
  					//s = c.Value
					found = true;
 				} else if (c is AxMSComCtl2.AxDTPicker) {
  					//s = c.Value
					found = true;
				}

				if (found) {
  					//i = c.Index
  					Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + frm.Name, c.Name + "_" + i, s);
				}

			}

		}
		public static void RecallForm(ref System.Windows.Forms.Form frm)
		{

			System.Windows.Forms.Control c = null;
			object i = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			foreach (Control c_loopVariable in frm.Controls) {
				c = c_loopVariable;
  				i = System.DBNull.Value;
  				//i = c.Index
 				if (c is System.Windows.Forms.TextBox | c is AxMBText.AxTextField) {
 					c.Text = Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + frm.Name, c.Name + "_" + i, "");
 				} else if (c is AxLpLib.AxfpList | c is System.Windows.Forms.ListBox | c is System.Windows.Forms.ComboBox | c is AxLpLib.AxfpCombo) {
  					//c.ListIndex = Val(GetSetting(My.Application.Info.Title, "Forms_" & frm.Name, c.Name & "_" & i, "-1"))
 				} else if (c is System.Windows.Forms.CheckBox) {
  					//c.Value = GetSetting(My.Application.Info.Title, "Forms_" & frm.Name, c.Name & "_" & i, "0")
 				} else if (c is System.Windows.Forms.RadioButton) {
  					//c.Value = GetSetting(My.Application.Info.Title, "Forms_" & frm.Name, c.Name & "_" & i, "False")
 				} else if (c is AxMSComCtl2.AxDTPicker) {
  					//c.Value = GetSetting(My.Application.Info.Title, "Forms_" & frm.Name, c.Name & "_" & i, CStr(Today))
				}
			}

		}
		public static void VersionCheck()
		{
			object i = null;
			object x = null;

 			DAO.Recordset tbl = null;
			DAO.TableDef tbldef = null;
			DAO.Field fld = null;
			DAO.Index idx = null;
 			DAO.TableDef oldtbldef = null;
			DAO.Field oldfld = null;
			DAO.Index oldidx = null;
			DAO.Recordset tbl2 = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			ADOX.Catalog ADOXcat = null;
			ADOX.Table MStbl = null;
			ADOX.Column MScol = null;
			Collection tblCollection = null;
			short Batch = 0;
			DAO.TableDefs tds = null;
 			DAO.Recordset tblNew = null;
 			DAO.Recordset tblOld = null;
			if (VERSION >= 6) {
				// Test Memos table for existence of MemoId column
				tbldef = GLdb.TableDefs["Memos"];
				fld = tbldef.Fields["MemoId"];
				if (Err().Number != 0) {
					Err().Clear();
 					tbldef = null;
					//adds columns to existing MDB-database table
					tblCollection = new Collection();
					//establish MDB database and tablename
					m_MDBdatabase = DefaultDataBase;
					m_MDBtable = "MemosNew";

					//make connection to MSAccess Database

					Xconx = new ADODB.Connection();
					Xcmd = new ADODB.Command();
					Xrs = new ADODB.Recordset();
					Xconx = Interaction.CreateObject("ADODB.Connection");
					Xconx.Open("Provider=Microsoft.Jet.OLEDB.4.0;" + "Persist Security Info=False;" + "Data Source=" + m_MDBdatabase);
					Xrs = Interaction.CreateObject("ADODB.Recordset");
					Xrs.CursorLocation = ADODB.CursorLocationEnum.adUseServer;

					//attach MDB and table to catalog
					ADOXcat = new ADOX.Catalog();
					ADOXcat.let_ActiveConnection("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + m_MDBdatabase);
					MStbl = new ADOX.Table();
					tblCollection.Add(m_MDBtable);
					//add columns to existing table
					var _with2 = MStbl;
					_with2.Name = m_MDBtable;
					_with2.Columns.Append("MemoId", ADODB.DataTypeEnum.adInteger);
					_with2.let_ParentCatalog(ADOXcat);
					_with2.Columns("MemoId").Properties("AutoIncrement").Value = true;
					_with2.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "MemoId");
					_with2.Columns.Append("VendorNumber", ADODB.DataTypeEnum.adInteger);
					_with2.Columns.Append("CompanyNumber", ADODB.DataTypeEnum.adInteger);
					_with2.Columns.Append("DepartmentNumber", ADODB.DataTypeEnum.adInteger);
					_with2.Columns.Append("CheckGroup", ADODB.DataTypeEnum.adSmallInt);
					_with2.Columns.Append("Memo", ADODB.DataTypeEnum.adVarWChar, 60);
					_with2.Columns.Append("BatchNumber", ADODB.DataTypeEnum.adInteger);
					ADOXcat.Tables.Append(MStbl);

					//clean up
					//ADOXcat.ActiveConnection.Close
					//Set ADOXcat = Nothing
 					MStbl = null;
 					MScol = null;
 					Xconx = null;
 					Xcmd = null;
 					Xrs = null;


					if (Err().Number == 0) {
						Sleep((5000));
						GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(DefaultDataBase, false, false);
						tblOld = GLdb.OpenRecordset("Memos");
						tblNew = GLdb.OpenRecordset(m_MDBtable);
						tblOld.MoveFirst();
						while (!tblOld.EOF) {
							tblNew.AddNew();
							tblNew.Fields["VendorNumber"].Value = tblOld.Fields["VendorNumber"].Value;
							tblNew.Fields["CompanyNumber"].Value = tblOld.Fields["CompanyNumber"].Value;
							tblNew.Fields["DepartmentNumber"].Value = tblOld.Fields["DepartmentNumber"].Value;
							tblNew.Fields["CheckGroup"].Value = tblOld.Fields["CheckGroup"].Value;
							tblNew.Fields["Memo"].Value = tblOld.Fields["Memo"].Value;
 							if (Information.IsDBNull(tblOld.Fields["BatchNumber"].Value)) {
								Batch = 0;
							} else {
								Batch = tblOld.Fields["BatchNumber"].Value;
							}
							tblNew.Fields["BatchNumber"].Value = Batch;
							tblNew.Update();
							tblOld.MoveNext();
						}
						tblNew.Close();
						tblOld.Close();
						tds = GLdb.TableDefs;
						tds.Delete("Memos");
						tds["MemosNew"].Name = "Memos";

						Err().Clear();
						tbldef = GLdb.TableDefs["Invoices"];
						var _with3 = tbldef;
						fld = _with3.CreateField("MemoId", DAO.DataTypeEnum.dbLong);
						fld.DefaultValue = 0;
						_with3.Fields.Append(fld);
						if (Err().Number == 0) {
							GLdb.Execute(("UPDATE Invoices SET MemoId = 0"));
						}

						Err().Clear();
						tbldef = GLdb.TableDefs["InvoiceHistory"];
						var _with4 = tbldef;
						fld = _with4.CreateField("MemoId", DAO.DataTypeEnum.dbLong);
						fld.DefaultValue = 0;
						_with4.Fields.Append(fld);
						if (Err().Number == 0) {
							GLdb.Execute(("UPDATE InvoiceHistory SET MemoId = 0"));
						}

					}
				}
			}

			if (VERSION >= 5) {
				Err().Clear();
				tbldef = GLdb.TableDefs["PRaccounts"];
				var _with5 = tbldef;
				fld = _with5.CreateField("Account18", DAO.DataTypeEnum.dbLong);
				fld.DefaultValue = 0;
				_with5.Fields.Append(fld);
				fld = _with5.CreateField("Account19", DAO.DataTypeEnum.dbLong);
				fld.DefaultValue = 0;
				_with5.Fields.Append(fld);
				if (Err().Number == 0) {
					GLdb.Execute(("UPDATE PRaccounts SET Account18 = 0, Account19 = 0"));
				}

			}

			if (VERSION >= 4) {
				Err().Clear();
				tbldef = GLdb.TableDefs["Accounts"];
				var _with6 = tbldef;
				fld = _with6.CreateField("active", DAO.DataTypeEnum.dbBoolean);
				_with6.Fields.Append(fld);
				if (Err().Number == 0)
					GLdb.Execute(("UPDATE Accounts SET active = True"));
			}

			if (VERSION > 3 | (VERSION == 3 & Conversion.Val(SUBVER) >= 0.12)) {
				tbldef = GLdb.TableDefs["InvoiceHistory"];
				var _with7 = tbldef;
				_with7.Indexes.Delete(("PrimaryKey"));
				_with7.Indexes.Refresh();
				idx = _with7.CreateIndex("PrimaryKey");
				fld = idx.CreateField("VendorNumber");
 				idx.Fields.Append(fld);
				fld = idx.CreateField("InvoiceNumber");
 				idx.Fields.Append(fld);
				fld = idx.CreateField("Date");
 				idx.Fields.Append(fld);
				fld = idx.CreateField("CheckNumber");
 				idx.Fields.Append(fld);
				idx.Primary = true;
				_with7.Indexes.Append(idx);
			}

			if (VERSION >= 3) {
				tbldef = GLdb.TableDefs["Transactions"];
				if (tbldef.Fields["Description"].Size <= 60) {
					fld = tbldef.CreateField("Temp", DAO.DataTypeEnum.dbText, 128);
					tbldef.Fields.Append(fld);
					//Set tbl2 = GLdb.OpenRecordset("Transactions")
					//While Not tbl2.EOF
					//    tbl2.Edit
					//     tbl2("temp") = noNulls(tbl2("Description"))
					//     tbl2.update
					//     tbl2.MoveNext
					//Wend
					//tbl2.Close
					GLdb.Execute(("UPDATE Transactions SET Temp = Description"));
					tbldef.Fields.Delete("Description");
					tbldef.Fields["Temp"].Name = "Description";
				}
			}

			if (VERSION >= 2.01) {
				//04/02/2002********************************************************************
				//* 1. Corrections to Check Register Maintenance.                              *
				//* 2. Option to verify invoice allocation company/department against check    *
				//*    company/department.                                                     *
				//******************************************************************************
				tbldef = GLdb.TableDefs["Control"];
				fld = tbldef.CreateField("CmpDptMatch", DAO.DataTypeEnum.dbBoolean);
				tbldef.Fields.Append(fld);
			}
			//VERSION 2.24
			if (VERSION >= 2.24) {
				Err().Clear();
				tbldef = GLdb.TableDefs["Vendors"];
				fld = tbldef.CreateField("Inactive", DAO.DataTypeEnum.dbBoolean);
				tbldef.Fields.Append(fld);
			}
			//VERSION 2.22
			//01/29/2004********************************************************************
			//* 1. Batch Pre-checks                                                        *
			//* 2. G/L Batch Journal Entries                                               *
			//******************************************************************************
			if (VERSION >= 2.22) {
				Err().Clear();
				tbldef = GLdb.CreateTableDef("BatchJrnHeader");
				if (Err().Number != 0)
					return;
				var _with8 = tbldef;
				fld = _with8.CreateField("Key", DAO.DataTypeEnum.dbLong);
				fld.Attributes = DAO.FieldAttributeEnum.dbAutoIncrField;
				_with8.Fields.Append(fld);
				_with8.Fields.Append(_with8.CreateField("BatchName", DAO.DataTypeEnum.dbText, 18));
				_with8.Fields.Append(_with8.CreateField("Company", DAO.DataTypeEnum.dbLong));
				_with8.Fields.Append(_with8.CreateField("Journal", DAO.DataTypeEnum.dbText, 32));
				_with8.Fields.Append(_with8.CreateField("Source", DAO.DataTypeEnum.dbText, 32));
				idx = _with8.CreateIndex("PrimaryKey");
				var _with9 = idx;
 				_with9.Fields.Append(_with9.CreateField("Key"));
				_with9.Primary = true;
				_with9.Unique = true;
				tbldef.Indexes.Append(idx);
				GLdb.TableDefs.Append(tbldef);
				if (Err().Number != 0)
					return;
				tbldef = GLdb.CreateTableDef("BatchJrnEntries");
				var _with10 = tbldef;
				_with10.Fields.Append(_with10.CreateField("Key", DAO.DataTypeEnum.dbLong));
				_with10.Fields.Append(_with10.CreateField("Department", DAO.DataTypeEnum.dbLong));
				_with10.Fields.Append(_with10.CreateField("Account", DAO.DataTypeEnum.dbLong));
				_with10.Fields.Append(_with10.CreateField("Description", DAO.DataTypeEnum.dbText, 60));
				_with10.Fields.Append(_with10.CreateField("Amount", DAO.DataTypeEnum.dbCurrency));
				idx = _with10.CreateIndex("Index");
 				idx.Fields.Append(idx.CreateField("Key"));
 				idx.Fields.Append(idx.CreateField("Department"));
 				idx.Fields.Append(idx.CreateField("Account"));
				idx.Unique = false;
				_with10.Indexes.Append(idx);
				GLdb.TableDefs.Append(tbldef);
				if (Err().Number != 0)
					return;
				tbldef = GLdb.TableDefs["CheckNumbers"];
				fld = tbldef.CreateField("BatchCheck", DAO.DataTypeEnum.dbLong);
				tbldef.Fields.Append(fld);
				if (Err().Number != 0)
					return;
				GLdb.Execute(("UPDATE CheckNumbers SET BatchCheck = 800000"));
				GLdb.Execute(("UPDATE InvoicesBatch SET CheckDate = Null"));
			}

			// VERSION 2.21
			//09/08/2003********************************************************************
			//* Vendor comment field added                                                 *
			//******************************************************************************
			if (VERSION >= 2.21) {
				tbldef = GLdb.TableDefs["Vendors"];
				fld = tbldef.CreateField("Comment", DAO.DataTypeEnum.dbText, 128);
				tbldef.Fields.Append(fld);
				if (Err().Number != 0)
					return;
			}

			// VERSION 2.04
			//05/01/2002********************************************************************
			//* Blank pages were being output during batch report processing               *
			//******************************************************************************
			if (VERSION >= 2.03) {
				//04/22/2002********************************************************************
				//* Added Batch number to InvBatchDistributions table so the same vnd/inv #'s  *
				//* can be used in different batches.                                          *
				//******************************************************************************
				tbldef = GLdb.TableDefs["InvBatchDistributions"];
				fld = tbldef.CreateField("Batch", DAO.DataTypeEnum.dbLong);
				tbldef.Fields.Append(fld);
				if (Err().Number != 0)
					return;
			}

			if (VERSION >= 2.01) {
				//04/02/2002********************************************************************
				//* 1. Corrections to Check Register Maintenance.                              *
				//* 2. Option to verify invoice allocation company/department against check    *
				//*    company/department.                                                     *
				//******************************************************************************
				tbldef = GLdb.TableDefs["Control"];
				fld = tbldef.CreateField("CmpDptMatch", DAO.DataTypeEnum.dbBoolean);
				tbldef.Fields.Append(fld);
			}
			//Exit Sub

			if (VERSION >= 2) {
				//10/10/2001********************************************************************
				//* Added Check Reconciliation                                                 *
				//******************************************************************************
				Err().Clear();

				tbldef = GLdb.TableDefs["Transactions"];
				fld = tbldef.CreateField("RecFlag", DAO.DataTypeEnum.dbBoolean);
				tbldef.Fields.Append(fld);
				tbldef = GLdb.TableDefs["Control"];
				fld = tbldef.CreateField("Memo", DAO.DataTypeEnum.dbMemo);
				tbldef.Fields.Append(fld);
				fld = tbldef.CreateField("StmtDate", DAO.DataTypeEnum.dbDate);
				tbldef.Fields.Append(fld);
				fld = tbldef.CreateField("ClearDate", DAO.DataTypeEnum.dbDate);
				tbldef.Fields.Append(fld);
			}

			if (VERSION >= 1.7) {
				//03/24/2001********************************************************************
				//* 1. Added 'InvoiceOption' field to the BatchControl table                   *
				//******************************************************************************
				//07/12/2001********************************************************************
				//* Added 'BatchNumber field to the Memos table                                *
				//******************************************************************************
				Err().Clear();

				tbl = GLdb.OpenRecordset("BatchControl");
				tbl.Close();
				if (Err().Number != 0) {
					Err().Clear();
					tbldef = GLdb.CreateTableDef("BatchControl");
					fld = tbldef.CreateField("Key", DAO.DataTypeEnum.dbLong, 4);
					fld.Attributes = DAO.FieldAttributeEnum.dbAutoIncrField;
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("Name", DAO.DataTypeEnum.dbText, 30);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("LastDate", DAO.DataTypeEnum.dbDate);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("InvoiceOption", DAO.DataTypeEnum.dbInteger, 2);
					tbldef.Fields.Append(fld);
					GLdb.TableDefs.Append(tbldef);
				} else {
					// Add InvoiceOption field to BatchControl
					tbldef = GLdb.TableDefs["BatchControl"];
					fld = tbldef.CreateField("InvoiceOption", DAO.DataTypeEnum.dbInteger, 2);
					tbldef.Fields.Append(fld);
				}
				//07/12/2001********************************************************************
				//* Added BatchNumber to the Memos Table
				//******************************************************************************
				Err().Clear();
				tbl = GLdb.OpenRecordset("Memos");
				tbl.Close();
				if (Err().Number == 0) {
					tbldef = GLdb.TableDefs["Memos"];
					fld = tbldef.CreateField("BatchNumber", DAO.DataTypeEnum.dbLong, 4);
					fld.DefaultValue = 0;
					tbldef.Fields.Append(fld);
					if (Err().Number == 0) {
						Err().Clear();
						tbldef.Indexes.Delete(("PrimaryKey"));
						idx = tbldef.CreateIndex("PrimaryKey");
						var _with11 = idx;
 						_with11.Fields.Append(_with11.CreateField("VendorNumber"));
 						_with11.Fields.Append(_with11.CreateField("CompanyNumber"));
 						_with11.Fields.Append(_with11.CreateField("DepartmentNumber"));
 						_with11.Fields.Append(_with11.CreateField("BatchNumber"));
 						_with11.Fields.Append(_with11.CreateField("CheckGroup"));
						_with11.Unique = true;
						tbldef.Indexes.Append(idx);
					}
				}
				//******************************************************************************
			}
			if (VERSION >= 1.6) {
				//*11/30/2000***************************************************************
				//* 1. Added Recurring Invoices                                            *
				//**************************************************************************
				 // ERROR: Not supported in C#: OnErrorStatement


				tbl = GLdb.OpenRecordset("InvoicesBatch");
				tbl.Close();
				if (Err().Number != 0) {
					oldtbldef = GLdb.TableDefs["Invoices"];
					tbldef = GLdb.CreateTableDef("InvoicesBatch");
					foreach (DAO.Field oldfld_loopVariable in oldtbldef.Fields) {
						oldfld = oldfld_loopVariable;
						Err().Clear();
						fld = tbldef.CreateField(oldfld.Name);
						fld.Attributes = oldfld.Attributes;
 						fld.DefaultValue = oldfld.DefaultValue;
						fld.Required = oldfld.Required;
						fld.Size = oldfld.Size;
						fld.Type = oldfld.Type;
						fld.AllowZeroLength = true;
						tbldef.Fields.Append(fld);
					}
					GLdb.TableDefs.Append(tbldef);
				}
				tbl = GLdb.OpenRecordset("BatchControl");
				tbl.Close();
				if (Err().Number != 0) {
					Err().Clear();
					tbldef = GLdb.CreateTableDef("BatchControl");
					fld = tbldef.CreateField("Key", DAO.DataTypeEnum.dbLong, 4);
					fld.Attributes = DAO.FieldAttributeEnum.dbAutoIncrField;
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("Name", DAO.DataTypeEnum.dbText, 16);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("LastDate", DAO.DataTypeEnum.dbDate);
					tbldef.Fields.Append(fld);
					GLdb.TableDefs.Append(tbldef);
				}
				//Set tbl = GLdb.OpenRecordset("InvoicesPending")
				//tbl.Close
				//If Err.Number <> 0 Then
				//    Err.Number = 0
				//    Set oldtbldef = GLdb.TableDefs("Invoices")
				//    Set tbldef = GLdb.CreateTableDef("InvoicesPending")
				//    For Each oldfld In oldtbldef.Fields
				//        Set fld = tbldef.CreateField(oldfld.Name)
				//        fld.Attributes = oldfld.Attributes
				//        fld.DefaultValue = oldfld.DefaultValue
				//        fld.Required = oldfld.Required
				//        fld.Size = oldfld.Size
				//        fld.type = oldfld.type
				//        fld.AllowZeroLength = True
				//        tbldef.Fields.Append fld
				//    Next
				//    GLdb.TableDefs.Append tbldef
				//End If
				tbl = GLdb.OpenRecordset("InvBatchDistributions");
				tbl.Close();
				if (Err().Number != 0) {
					Err().Clear();
					oldtbldef = GLdb.TableDefs["InvoiceDistributions"];
					tbldef = GLdb.CreateTableDef("InvBatchDistributions");
					foreach (DAO.Field oldfld_loopVariable in oldtbldef.Fields) {
						oldfld = oldfld_loopVariable;
						fld = tbldef.CreateField(oldfld.Name);
						fld.Attributes = oldfld.Attributes;
 						fld.DefaultValue = oldfld.DefaultValue;
						fld.Required = oldfld.Required;
						fld.Size = oldfld.Size;
						fld.Type = oldfld.Type;
						tbldef.Fields.Append(fld);
					}
					foreach (DAO.Index oldidx_loopVariable in oldtbldef.Indexes) {
						oldidx = oldidx_loopVariable;
						idx = oldtbldef.Indexes[oldidx.Name];
 						idx.Fields = oldidx.Fields;
						idx.Primary = oldidx.Primary;
						idx.Required = oldidx.Required;
						idx.Unique = oldidx.Unique;
						tbldef.Indexes.Append(idx);
					}
					GLdb.TableDefs.Append(tbldef);
				}
				//Set tbl = GLdb.OpenRecordset("InvPendDistributions")
				//tbl.Close
				//If Err.Number <> 0 Then
				//    Err.Number = 0
				//    Set oldtbldef = GLdb.TableDefs("InvoiceDistributions")
				//    Set tbldef = GLdb.CreateTableDef("InvPendDistributions")
				//    For Each oldfld In oldtbldef.Fields
				//        Set fld = tbldef.CreateField(oldfld.Name)
				//        fld.Attributes = oldfld.Attributes
				//        fld.DefaultValue = oldfld.DefaultValue
				//        fld.Required = oldfld.Required
				//        fld.Size = oldfld.Size
				//        fld.type = oldfld.type
				//        tbldef.Fields.Append fld
				//    Next
				//    For Each oldidx In oldtbldef.Indexes
				//        Set idx = oldtbldef.Indexes(oldidx.Name)
				//        idx.Fields = oldidx.Fields
				//        idx.Primary = oldidx.Primary
				//        idx.Required = oldidx.Required
				//        idx.Unique = oldidx.Unique
				//        tbldef.Indexes.Append idx
				//    Next
				//    GLdb.TableDefs.Append tbldef
				//End If
				Err().Clear();
				tbl = GLdb.OpenRecordset("Control");
				if (Err().Number != 0) {
					LogError.MsgBox_Renamed("Severe Error Opening Control Table: " + Err().Number, , "WINGL");
					System.Environment.Exit(0);
				}
 				x = tbl.Fields["RecurStaging"].Value;
				tbl.Close();
				if (Err().Number != 0) {
					Err().Clear();
					tbldef = GLdb.TableDefs["Control"];
					fld = tbldef.CreateField("RecurStaging", DAO.DataTypeEnum.dbInteger, 2);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("RecurInvoice", DAO.DataTypeEnum.dbInteger, 2);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("BatchDate", DAO.DataTypeEnum.dbDate);
					tbldef.Fields.Append(fld);
					fld = tbldef.CreateField("BatchOn", DAO.DataTypeEnum.dbBoolean);
					tbldef.Fields.Append(fld);
					tbl = GLdb.OpenRecordset("Control");
					tbl.Edit();
					tbl.Fields["RecurStaging"].Value = 0;
					tbl.Fields["RecurInvoice"].Value = 0;
 					tbl.Fields["BatchDate"].Value = DateAndTime.DateValue("01/01/2000");
					tbl.Fields["BatchOn"].Value = false;
					BatchOn = false;
					tbl.Update();
					tbl.Close();
				}
			}
			if (VERSION == 1.5) {
				//12/19/2000************************************************************************
				//* 1. Added link to MBCS web page on Help menu.
				//* 2. Added 1099 Report                          *
				//* 3. Added Close Vendor Year                    *
				//* 4. Added link to version check on MBCS web site.
				//* 5. Was not deleting Invoice Distribution records when posting invoices *
				//**********************************************************************************
				 // ERROR: Not supported in C#: OnErrorStatement

				Err().Clear();
				tbl = GLdb.OpenRecordset("InvoiceDistributions");
				if (Err().Number == 0) {
					if (tbl.RecordCount > 0) {
						while (!tbl.EOF) {
							tbl2 = GLdb.OpenRecordset("SELECT * FROM Invoices WHERE InvoiceKey = " + tbl.Fields["InvoiceKey"].Value);
							if (tbl2.RecordCount == 0) {
								tbl2.Close();
								GLdb.Execute(("DELETE * FROM InvoiceDistributions WHERE InvoiceKey = " + tbl.Fields["InvoiceKey"].Value));
							}
							tbl.MoveNext();
						}
						tbl.Close();
					}
				}
			}
			if (VERSION == 1.4) {
				//*09/24/2000**************************************
				//* 1. Added Orphaned Transaction update function *
				//*************************************************
			}
			if (VERSION >= 1.2) {
				//*08/12/2000************************************************************
				//* 1. Added ability to batch A/P transactions when posting to G/L      *
				//* 2. Added ability to select sort sequence for Transaction Edit List  *
				//***********************************************************************
				 // ERROR: Not supported in C#: OnErrorStatement

				tbl = GLdb.OpenRecordset("Control");
				if (Err().Number != 0) {
					LogError.MsgBox_Renamed("Severe Error Opening Control Table: " + Err().Number, , "WINGL");
					System.Environment.Exit(0);
				}
 				x = tbl.Fields["EdListSort1_fld"].Value;
				tbl.Close();
				if (Err().Number != 0) {
					Err().Clear();
					tbldef = GLdb.TableDefs["Control"];
					for (i = 1; i <= 6; i++) {
 						fld = tbldef.CreateField("EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_fld", DAO.DataTypeEnum.dbText, 18);
						tbldef.Fields.Append(fld);
 						fld = tbldef.CreateField("EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_dir", DAO.DataTypeEnum.dbText, 4);
						tbldef.Fields.Append(fld);
					}
					tbl = GLdb.OpenRecordset("Control");
					tbl.Edit();
					tbl.Fields["EdListSort1_fld"].Value = "Source";
					tbl.Fields["EdListSort1_dir"].Value = "ASC";
					tbl.Fields["EdListSort2_fld"].Value = "Description";
					tbl.Fields["EdListSort2_dir"].Value = "ASC";
					tbl.Fields["EdListSort3_fld"].Value = "Amount";
					tbl.Fields["EdListSort3_dir"].Value = "DESC";
					tbl.Update();
					tbl.Close();
				}
			}

		}
		public static object SearchList(ref AxLpLib.AxfpCombo lst, ref string s, ref int ColSearch, ref int Col)
		{

			lst.ColumnSearch = ColSearch;
			lst.SearchText = s;
			lst.SearchIgnoreCase = true;
			lst.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
			lst.Action = LpLib.enumAction.ActionSearch;
			lst.Row = lst.SearchIndex;
			lst.Col = Col;
 			return lst.ColList;

		}
	}
}
