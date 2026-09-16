using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	internal partial class Invoice : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.5  2013/09/20 20:42:38  mike
//* Re-write of Memo Line handling:
//* *) Added MemoId Primary key to Memos
//* *) Added MemoId column to Invoices.
//* NOT TESTED
//*
//* Revision 1.4  2012/05/10 18:09:31  mike
//* *) Added scan checkbox to Invoices form
//* *) Many unknown changes
//*
//* Revision 1.3  2011/02/10 00:29:36  mike
//* Added Active flag to Accounts to prevent display of inactive accounts in entry screens
//*
//* Revision 1.2  2010/11/12 18:30:55  mike
//* Added invoice editing for Windoc
//*
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.7  2010/11/09 12:25:27  mike
//* Document display fixes
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.6  2010/02/15 01:05:57  mike
//* WINGLHEAD-3 : Document display, scan, and delete from Invoice display and edit.
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.5  2009/12/25 03:47:05  mike
//* Scanning for Batch Invoices
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.4  2009/10/12 19:56:39  mike
//* Paid invoice posting to windoc
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.3  2009/10/09 20:12:49  mike
//* Scanning changes
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2009/09/24 20:05:56  mike
//* Corrected path used to retrieve Windoc.exe
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.1  2009/08/21 12:36:14  mike
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
//* Revision 2.18  by: normal  Rev date: 1/27/2003 10:53:02 AM
//*   A single invoice distribution change was not being saved.
//*
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:33:56 AM
//*   Initial revision.
//*
//* $Endlog$

//* editFlag is > 0 when editing an existing invoice
//* InvoiceFlag = true indicates an existing invoice

		decimal TotalAmount;
		short editflag;
		bool AllocatedFlag;
		bool distflag;
		bool newFlag;
		bool mvarPrePaid;
		float mvarCheckNumber;
		string mvarCheckDate;
		string oldInvoice;
		int MemoId;

		public bool PrePaid {
			get { return mvarPrePaid; }
			set { mvarPrePaid = value; }
		}
		public float CheckNumber {
			get { return mvarCheckNumber; }
			set { mvarCheckNumber = value; }
		}
		public string CheckDate {
			get { return mvarCheckDate; }
			set { mvarCheckDate = value; }
		}

//UPGRADE_WARNING: Event chkEFT.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkEFT_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkEFT.CheckState) {
				//UPGRADE_NOTE: Enabled was upgraded to CtlEnabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtInvoice[3].CtlEnabled = false;
			} else {
				//UPGRADE_NOTE: Enabled was upgraded to CtlEnabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtInvoice[3].CtlEnabled = true;
			}

		}

//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object fi = null;
			//******************************************************************************

			System.Windows.Forms.Control c = null;
			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement


			switch (Index) {

				case 0:
					// NEW
					WinglGlobal.InvoiceFlag = false;
					clearForm(ref true);
					WinglGlobal.lastVendor = lstVendors.ListIndex;
					WinglGlobal.lastCompany = lstCompanies.ListIndex;
					WinglGlobal.lastDepartment = lstDepartments.ListIndex;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtVendors().Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.lastInvDate = dtVendors[0].Value;
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(dtVendors[1].Value)) {
						//UPGRADE_WARNING: Couldn't resolve default property of object dtVendors().Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.lastPayDate = dtVendors[1].Value;
					}
					editflag = 0;
					this.Close();
					break;

				case 1:
					// SAVE
					if (lstVendors.ListIndex < 0 | string.IsNullOrEmpty(Strings.Trim(lstInvoices.Text))) {
						Interaction.Beep();
						Interaction.MsgBox("No Invoice Number has been entered.","" , "Invoices");
						goto Cmd_Click_exit;
					}

					SaveInvoice();

					lstVendors.Row = lstVendors.ListIndex;
					lstVendors.Col = 0;
					WinglGlobal.InvoiceFlag = true;
					WinglGlobal.lastVendor = lstVendors.ListIndex;
					WinglGlobal.lastCompany = lstCompanies.ListIndex;
					WinglGlobal.lastDepartment = lstDepartments.ListIndex;
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					WinglGlobal.lastInvDate = (Information.IsDBNull(dtVendors[0].Value) ? DateAndTime.DateValue("01/01/1901") : dtVendors[0].Value);
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					WinglGlobal.lastPayDate = (Information.IsDBNull(dtVendors[1].Value) ? DateAndTime.DateValue("01/01/1901") : dtVendors[1].Value);
					this.Close();
					break;

				case 2:
					// EXIT
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.lastVendor = -1;
					WinglGlobal.lastCompany = -1;
					WinglGlobal.lastDepartment = -1;
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					WinglGlobal.lastInvDate = DateAndTime.DateValue("01/01/1901");
					//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					WinglGlobal.lastPayDate = DateAndTime.DateValue("01/01/1901");
					this.Close();
					break;

			}
			goto Cmd_Click_exit;
			Cmd_Click_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Invoices");
			 // ERROR: Not supported in C#: ResumeStatement

			Cmd_Click_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object fi. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			FileSystem.FileClose(fi);
			WinglGlobal.tblinvoices.Close();
			WinglGlobal.tblInvDist.Close();
			WinglGlobal.tblVendorCats.Close();
			WinglGlobal.rptCancel = true;

		}
		public void SaveInvoice()
		{
			object xsql = null;
			object r = null;
			object xjrn = null;

			//UPGRADE_WARNING: Arrays in structure tblVend may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			DAO.Recordset tblVend = null;
			bool noMatch = false;
			int Batch = 0;
			string desc = null;
			PostInvoice_type invp = default(PostInvoice_type);
			short postflag = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			postflag = WinglGlobal.tblControl.Fields["GLMethod"].Value;
			Batch = WinglGlobal.tblControl.Fields["GLBatchNumber"].Value + 1;
			desc = WinglGlobal.tblControl.Fields["GlBatchDesc"].Value;
			WinglGlobal.GLCashAcct = WinglGlobal.tblControl.Fields["GLCashAccount"].Value;
			WinglGlobal.tblControl.Edit();
			WinglGlobal.tblControl.Fields["GLBatchNumber"].Value = Batch;
			WinglGlobal.tblControl.Update();
			WinglGlobal.tblControl.Close();

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			i = lstDepartments.ListIndex;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			if (editflag == 0) {
				 // ERROR: Not supported in C#: OnErrorStatement

				WinglGlobal.tblinvoices.Close();
				WinglGlobal.tblVendorCats.Close();
				WinglGlobal.tblInvDist.Close();
				 // ERROR: Not supported in C#: OnErrorStatement

				WinglGlobal.tblinvoices = WinglGlobal.GLdb.OpenRecordset(WinglGlobal.Ifilename);
				DAODBEngine_definst.BeginTrans();
				WinglGlobal.tblinvoices.AddNew();
			} else {
				DAODBEngine_definst.BeginTrans();
				WinglGlobal.tblinvoices.Edit();
			}
			if (WinglGlobal.InvoiceBatch) {
				WinglGlobal.tblinvoices.Fields["Batch"].Value = WinglGlobal.BatchNumber;
			}
			PackInvoice();
			if (WinglGlobal.InHistory(ref WinglGlobal.tblinvoices.Fields["VendorNumber"].Value, ref WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value)) {
				 // ERROR: Not supported in C#: OnErrorStatement

				DAODBEngine_definst.Rollback();
				goto SaveInvoice_exit;
			}
			WinglGlobal.tblinvoices.Update();
			WinglGlobal.tblinvoices.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblinvoices.LastModified);
			tblVend = WinglGlobal.GLdb.OpenRecordset("SELECT Journal FROM Vendors WHERE VendorNumber = " + WinglGlobal.tblinvoices.Fields["VendorNumber"].Value);
			if (tblVend.RecordCount > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object xjrn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xjrn = tblVend.Fields["Journal"].Value;
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object xjrn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xjrn = "*";
			}

			WinglGlobal.invpGlobal.amountpaid = WinglGlobal.tblinvoices.Fields["Amount"].Value - WinglGlobal.tblinvoices.Fields["DiscountAmount"].Value;
			WinglGlobal.invpGlobal.amounttopay = WinglGlobal.tblinvoices.Fields["AmountToPay"].Value;
			WinglGlobal.invpGlobal.Batch = Batch;
			WinglGlobal.invpGlobal.Company = WinglGlobal.tblinvoices.Fields["Company"].Value;
			WinglGlobal.invpGlobal.Department = WinglGlobal.tblinvoices.Fields["Department"].Value;
			WinglGlobal.invpGlobal.vendor = WinglGlobal.tblinvoices.Fields["VendorNumber"].Value;
			WinglGlobal.invpGlobal.invnumber = WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value;
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			if (!Information.IsDBNull(WinglGlobal.tblinvoices.Fields["Date"].Value)) {
				WinglGlobal.invpGlobal.date_Renamed = WinglGlobal.tblinvoices.Fields["Date"].Value;
			}
			if (WinglGlobal.tblinvoices.Fields["CheckNumber"].Value > 0)
				WinglGlobal.invpGlobal.CheckNumber = WinglGlobal.tblinvoices.Fields["CheckNumber"].Value;
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			if (!Information.IsDBNull(WinglGlobal.tblinvoices.Fields["PayDueDate"].Value)) {
				WinglGlobal.invpGlobal.dueDate = WinglGlobal.tblinvoices.Fields["PayDueDate"].Value;
			}
			// Get Vendor Categories if new invoice
			if (editflag == 0) {
				WinglGlobal.InvoiceFlag = true;
				lstInvoices.Items.Add(WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value);
				lstInvoices.SelectedIndex = lstInvoices.Items.Count - 1;
				WinglGlobal.InvoiceFlag = false;
				//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
				//GoSub AllocateNewInvoice
				dtVendors[0].CheckBox = false;
				// Scan the invoice?
				if (chkScan.CheckState & WinglGlobal.scan & !WinglGlobal.batchProcess) {
					//Call scanInvoice
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					scanImmediate(ref (WinglGlobal.tblinvoices.Fields["CheckNumber"].Value > 0 & !Information.IsDBNull(WinglGlobal.tblinvoices.Fields["CheckDate"].Value) ? true : false));
				}
			//If InvoiceFlag Then
			} else if (!distflag) {
				// Store existing distributions
				//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
				//GoSub AllocateOldInvoice
				if (chkScan.CheckState & WinglGlobal.scan & !WinglGlobal.batchProcess & Strings.Trim(oldInvoice) != Strings.Trim(WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value)) {
					WinglGlobal.docInvoice(WinglGlobal.tblinvoices, ref false, ref false, ref true, ref oldInvoice);
				}
			}
			if (WinglGlobal.tblinvoices.Fields["Company"].Value <= 0 | WinglGlobal.tblinvoices.Fields["Department"].Value <= 0) {
				AllocatedFlag = false;
			} else {
				AllocatedFlag = (TotalAmount == WinglGlobal.tblinvoices.Fields["Amount"].Value ? true : false);
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("Vendor=" + WinglGlobal.tblinvoices.Fields["VendorNumber"].Value + ", Invoice#=" + WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value + ", Batch=" + WinglGlobal.InvoiceBatch + ", Allocated=" + AllocatedFlag + ", TotalAmount=" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(TotalAmount, "###,###,##0.00") + ", InvoiceAmount=" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblinvoices.Fields["Amount"].Value, "###,###,##0.00"), ref 0, ref "Invoice_Save", ref WinglGlobal.logInvoice);
			TotalAmount = WinglGlobal.tblinvoices.Fields["Amount"].Value;
			WinglGlobal.tblinvoices.Edit();
			WinglGlobal.tblinvoices.Fields["AllocationFlag"].Value = AllocatedFlag;
			if (chkEFT.CheckState) {
				WinglGlobal.tblinvoices.Fields["CheckNumber"].Value = WinglGlobal.GetCheckNumber(ref WinglGlobal.tblinvoices.Fields["Company"].Value, ref WinglGlobal.tblinvoices.Fields["Department"].Value);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(WinglGlobal.tblinvoices.Fields["CheckDate"].Value)) {
					WinglGlobal.tblinvoices.Fields["CheckDate"].Value = DateAndTime.Now;
				}
			}
			WinglGlobal.tblinvoices.Update();
			WinglGlobal.tblinvoices.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblinvoices.LastModified);
			DAODBEngine_definst.CommitTrans();
			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblInvDist.Close();
			WinglGlobal.tblVendorCats.Close();
			WinglGlobal.tblinvoices.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblinvoices.LastModified);
			PrePaid = false;
			if (!WinglGlobal.InvoiceBatch) {
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (WinglGlobal.tblinvoices.Fields["CheckNumber"].Value > 0 & !Information.IsDBNull(WinglGlobal.tblinvoices.Fields["CheckDate"].Value) & WinglGlobal.tblinvoices.Fields["AllocationFlag"].Value) {
					WinglGlobal.invpGlobal.datelastpaid = WinglGlobal.tblinvoices.Fields["CheckDate"].Value;
					WinglGlobal.invpGlobal.CheckDate = WinglGlobal.tblinvoices.Fields["CheckDate"].Value;
					WinglGlobal.invpGlobal.CheckNumber = WinglGlobal.tblinvoices.Fields["CheckNumber"].Value;
					PrePaid = true;
					WinglGlobal.invpGlobal.payflag = 4;
					WinglGlobal.Post_Invoice(ref WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value, ref postflag, ref Batch, ref WinglGlobal.GLCashAcct, ref desc, ref WinglGlobal.invpGlobal, ref false);
				}
			}
			editflag = 1;
			distflag = false;

			goto SaveInvoice_exit;
			AllocateNewInvoice:

			//============================================================================
			AllocatedFlag = false;
			TotalAmount = 0;
			WinglGlobal.tblVendorCats = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM VendorCategories WHERE VendorNumber = " + WinglGlobal.tblinvoices.Fields["VendorNumber"].Value);
			WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(WinglGlobal.IDfilename);
			noMatch = true;
			if (WinglGlobal.tblVendorCats.RecordCount > 0) {
				WinglGlobal.tblVendorCats.MoveLast();
				//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				r = WinglGlobal.tblVendorCats.RecordCount;
				//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				chkSplit.CheckState = ((r > 1) ? 1 : 0);
				WinglGlobal.tblVendorCats.MoveFirst();
				while (!WinglGlobal.tblVendorCats.EOF) {
					WinglGlobal.tblInvDist.AddNew();
					WinglGlobal.tblInvDist.Fields["InvoiceKey"].Value = WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
					WinglGlobal.tblInvDist.Fields["Company"].Value = (WinglGlobal.tblVendorCats.Fields["Company"].Value <= 0 ? WinglGlobal.tblinvoices.Fields["Company"].Value : WinglGlobal.tblVendorCats.Fields["Company"].Value);
					WinglGlobal.tblInvDist.Fields["Department"].Value = (WinglGlobal.tblVendorCats.Fields["Department"].Value <= 0 ? WinglGlobal.tblinvoices.Fields["Department"].Value : WinglGlobal.tblVendorCats.Fields["Department"].Value);
					if (noMatch & (WinglGlobal.tblInvDist.Fields["Company"].Value != WinglGlobal.tblinvoices.Fields["Company"].Value) | (WinglGlobal.tblInvDist.Fields["Department"].Value != WinglGlobal.tblinvoices.Fields["Department"].Value)) {
						Interaction.MsgBox("Company/Department numbers do not match between Invoice and Distributions");
						noMatch = false;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (r == 1) {
						WinglGlobal.tblInvDist.Fields["Amount"].Value = WinglGlobal.tblinvoices.Fields["Amount"].Value;
						TotalAmount = WinglGlobal.tblInvDist.Fields["Amount"].Value;
					} else {
						WinglGlobal.tblInvDist.Fields["Amount"].Value = WinglGlobal.tblVendorCats.Fields["Amount"].Value;
						TotalAmount = TotalAmount + WinglGlobal.tblInvDist.Fields["Amount"].Value;
						if (WinglGlobal.tblInvDist.Fields["Company"].Value == 0) {
							WinglGlobal.tblInvDist.Fields["Company"].Value = WinglGlobal.tblinvoices.Fields["Company"].Value;
						}
						if (WinglGlobal.tblInvDist.Fields["Department"].Value == 0) {
							WinglGlobal.tblInvDist.Fields["Department"].Value = WinglGlobal.tblinvoices.Fields["Department"].Value;
						}
					}
					WinglGlobal.tblInvDist.Fields["Account"].Value = WinglGlobal.tblVendorCats.Fields["Account"].Value;
					WinglGlobal.tblInvDist.Fields["Journal"].Value = (string.IsNullOrEmpty(Strings.Trim(WinglGlobal.tblVendorCats.Fields["JournalName"].Value)) ? xjrn : WinglGlobal.tblVendorCats.Fields["JournalName"].Value);
					WinglGlobal.tblInvDist.Update();
					//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (r == 1) {
						lstAccounts.Col = 1;
						for (i = 0; i <= lstAccounts.ListCount - 1; i++) {
							lstAccounts.Row = i;
							if (WinglGlobal.tblVendorCats.Fields["Account"].Value == Conversion.Val(lstAccounts.ColList)) {
								lstAccounts.ListIndex = i;
								//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
								lstAccounts.CtlText = lstAccounts.List[i];
								break; // TODO: might not be correct. Was : Exit For
							}
						}
						for (i = 0; i <= lstJournals.ListCount - 1; i++) {
							lstJournals.Row = i;
							if (Strings.Trim(WinglGlobal.tblVendorCats.Fields["JournalName"].Value) == Strings.Trim(lstJournals.ColList)) {
								lstJournals.ListIndex = i;
								//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
								lstJournals.CtlText = lstJournals.List[i];
								break; // TODO: might not be correct. Was : Exit For
							}
						}
					}
					WinglGlobal.tblVendorCats.MoveNext();
				}
				if (WinglGlobal.tblinvoices.Fields["Amount"].Value == 0) {
					WinglGlobal.tblinvoices.Edit();
					WinglGlobal.tblinvoices.Fields["Amount"].Value = TotalAmount;
					WinglGlobal.tblinvoices.Update();
					WinglGlobal.tblinvoices.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblinvoices.LastModified);
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					txtInvoice[0].CtlText = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(TotalAmount, "#######0.00");
					this.Refresh();
				}
			} else {
				i = lstAccounts.ListIndex;
				if (i > -1) {
					WinglGlobal.tblInvDist.AddNew();
					WinglGlobal.tblInvDist.Fields["InvoiceKey"].Value = WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
					WinglGlobal.tblInvDist.Fields["Company"].Value = WinglGlobal.tblinvoices.Fields["Company"].Value;
					WinglGlobal.tblInvDist.Fields["Department"].Value = WinglGlobal.tblinvoices.Fields["Department"].Value;
					lstAccounts.Row = i;
					lstAccounts.Col = 1;
					WinglGlobal.tblInvDist.Fields["Account"].Value = lstAccounts.ColList;
					WinglGlobal.tblInvDist.Fields["Amount"].Value = WinglGlobal.tblinvoices.Fields["Amount"].Value;
					TotalAmount = WinglGlobal.tblinvoices.Fields["Amount"].Value;
					if (lstJournals.ListIndex > -1) {
						//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
						WinglGlobal.tblInvDist.Fields["Journal"].Value = lstJournals.CtlText;
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object xjrn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.tblInvDist.Fields["Journal"].Value = xjrn;
					}
					WinglGlobal.tblInvDist.Update();
				}
			}
			if (!distflag & TotalAmount != WinglGlobal.tblinvoices.Fields["Amount"].Value) {
				My.MyProject.Forms.InvoiceDistributions.ShowDialog();
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xsql = "SELECT SUM(Amount) as TotalAmount FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
				//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (WinglGlobal.tblInvDist.RecordCount > 0) {
					TotalAmount = WinglGlobal.tblInvDist.Fields["TotalAmount"].Value;
				}
				WinglGlobal.tblInvDist.Close();
				if (!WinglGlobal.InvoiceBatch)
					lstInvoices_SelectedIndexChanged(lstInvoices, new System.EventArgs());
			}
			editflag = 1;
			//UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			return;
			AllocateOldInvoice:

			//=============================================================================

			AllocatedFlag = false;
			TotalAmount = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT SUM(Amount) AS TotalAmount FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			if (WinglGlobal.tblInvDist.RecordCount > 0 & !Information.IsDBNull(WinglGlobal.tblInvDist.Fields["TotalAmount"].Value)) {
				TotalAmount = WinglGlobal.tblInvDist.Fields["TotalAmount"].Value;
			} else {
				TotalAmount = 0;
			}
			if (!distflag) {
				i = lstAccounts.ListIndex;
				if (i > -1) {
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "DELETE * FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
					WinglGlobal.GLdb.Execute((xsql));
					WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(WinglGlobal.IDfilename);
					WinglGlobal.tblInvDist.AddNew();
					WinglGlobal.tblInvDist.Fields["InvoiceKey"].Value = WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
					WinglGlobal.tblInvDist.Fields["Company"].Value = WinglGlobal.tblinvoices.Fields["Company"].Value;
					WinglGlobal.tblInvDist.Fields["Department"].Value = WinglGlobal.tblinvoices.Fields["Department"].Value;
					lstAccounts.Row = i;
					lstAccounts.Col = 1;
					WinglGlobal.tblInvDist.Fields["Account"].Value = lstAccounts.ColList;
					WinglGlobal.tblInvDist.Fields["Amount"].Value = WinglGlobal.tblinvoices.Fields["Amount"].Value;
					TotalAmount = WinglGlobal.tblinvoices.Fields["Amount"].Value;
					if (lstJournals.ListIndex > -1) {
						//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
						WinglGlobal.tblInvDist.Fields["Journal"].Value = lstJournals.CtlText;
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object xjrn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.tblInvDist.Fields["Journal"].Value = xjrn;
					}
					WinglGlobal.tblInvDist.Update();
					WinglGlobal.tblInvDist.Close();
				}
				if (TotalAmount != WinglGlobal.tblinvoices.Fields["Amount"].Value) {
					My.MyProject.Forms.InvoiceDistributions.ShowDialog();
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT SUM(Amount) as TotalAmount FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (WinglGlobal.tblInvDist.RecordCount > 0) {
						TotalAmount = WinglGlobal.tblInvDist.Fields["TotalAmount"].Value;
					}
					WinglGlobal.tblInvDist.Close();
				}
			}
			//UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			return;
			SaveInvoice_error:

			//=============================================================================

			if (Err().Number != 3420) {
				LogError.MsgBox_Renamed("","" , WinglGlobal.sProcID);
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			DAODBEngine_definst.Rollback();
			 // ERROR: Not supported in C#: ResumeStatement

			SaveInvoice_exit:


		}

		public void scanImmediate(ref bool PrePaid)
		{

			WinglGlobal.uniqueID = Convert.ToString(DateAndTime.Now);
			 // ERROR: Not supported in C#: OnErrorStatement

			Err().Clear();

			WinglGlobal.PID = Interaction.Shell(WinGlobal.GetPath(ref Interaction.GetSetting("Wingl", "Options", "WindocFile", "")) + "windoc.exe wingl-runOnce \"" + WinglGlobal.uniqueID + "\" \"" + WinglGlobal.scanInvoice(ref false, ref PrePaid, ref false) + "\"");
			if (WinglGlobal.PID <= 0) {
				LogError.MsgBox_Renamed("Could not load Windoc. Invoices will not be scanned. PID=" + WinglGlobal.PID);
				cmdDoc[0].Visible = false;
				//UPGRADE_WARNING: Couldn't resolve default property of object cnn.CloseDB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				WinglGlobal.cnn.CloseDB();
			}

		}

//******************************************************************************
		private void cmdDist_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object x = null;
			object xsql = null;
			//******************************************************************************

			if (lstVendors.ListIndex < 0 | string.IsNullOrEmpty(Strings.Trim(lstInvoices.Text))) {
				Interaction.Beep();
				Interaction.MsgBox("No Invoice has been entered.", , "Invoices");
				goto cmdDist_Click_exit;
			}

			distflag = true;
			SaveInvoice();
			My.MyProject.Forms.InvoiceDistributions.ShowDialog();
			//distflag = False
			if (!WinglGlobal.InvoiceBatch)
				lstInvoices_SelectedIndexChanged(lstInvoices, new System.EventArgs());
			chkSplit.CheckState = System.Windows.Forms.CheckState.Unchecked;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblInvDist.RecordCount > 0) {
				WinglGlobal.tblInvDist.MoveLast();
				if (WinglGlobal.tblInvDist.RecordCount == 1) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblInvDist.Fields["Account"].Value, "####");
					lstAccounts.ColumnSearch = 1;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstAccounts.SearchText = x;
					lstAccounts.SearchIgnoreCase = true;
					lstAccounts.Action = LpLib.enumAction.ActionSearch;
					if (lstAccounts.SearchIndex >= 0) {
						lstAccounts.ListIndex = lstAccounts.SearchIndex;
					}
					lstJournals.ColumnSearch = 0;
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblInvDist.Fields["Journal"].Value)) {
						lstJournals.SearchText = Strings.Trim(WinglGlobal.tblInvDist.Fields["Journal"].Value);
						lstJournals.SearchIgnoreCase = true;
						lstJournals.Action = LpLib.enumAction.ActionSearch;
						if (lstJournals.SearchIndex >= 0) {
							lstJournals.ListIndex = lstJournals.SearchIndex;
						}
					}
				} else {
					lstAccounts.ListIndex = -1;
					lstJournals.ListIndex = -1;
					chkSplit.CheckState = System.Windows.Forms.CheckState.Checked;
				}
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblInvDist.Close();
			goto cmdDist_Click_exit;
			cmdDist_Click_exit:


		}

		private void cmdDoc_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdDoc.GetIndex(eventSender);
			object Batch = null;

			switch (Index) {
				case 0:
					// Scan
					WinglGlobal.invpGlobal.amountpaid = WinglGlobal.tblinvoices.Fields["Amount"].Value - WinglGlobal.tblinvoices.Fields["Discount"].Value;
					WinglGlobal.invpGlobal.amounttopay = WinglGlobal.tblinvoices.Fields["AmountToPay"].Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object Batch. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.invpGlobal.Batch = Batch;
					WinglGlobal.invpGlobal.Company = WinglGlobal.tblinvoices.Fields["Company"].Value;
					WinglGlobal.invpGlobal.Department = WinglGlobal.tblinvoices.Fields["Department"].Value;
					WinglGlobal.invpGlobal.vendor = WinglGlobal.tblinvoices.Fields["VendorNumber"].Value;
					WinglGlobal.invpGlobal.invnumber = WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value;
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblinvoices.Fields["Date"].Value)) {
						WinglGlobal.invpGlobal.date_Renamed = WinglGlobal.tblinvoices.Fields["Date"].Value;
					}
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblinvoices.Fields["PayDueDate"].Value)) {
						WinglGlobal.invpGlobal.dueDate = WinglGlobal.tblinvoices.Fields["PayDueDate"].Value;
					}
					scanImmediate(ref false);
					break;
				//Call scanInvoice
				case 1:
					// Display
					//frmDocPreview.Show vbModal
					//UPGRADE_WARNING: Couldn't resolve default property of object rsd(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					Interaction.Shell(WinglGlobal.dbname + "WindocViewer.exe " + WinglGlobal.rsd("document_id"));
					break;
			}

		}

//******************************************************************************
		private void dtVendors_Change(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = dtVendors.GetIndex(eventSender);
			object dirty = null;
			//******************************************************************************

			//UPGRADE_WARNING: Couldn't resolve default property of object dirty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			dirty = true;

		}

//UPGRADE_WARNING: Form event Invoice.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void Invoice_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (WinglGlobal.batchProcess) {
				txtInvoice[0].Focus();
			}

		}

//******************************************************************************
		private void Invoice_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object x = null;
			object xsql = null;
			object i = null;
			object rc = null;
			//******************************************************************************

			int v = 0;
			int inkey = 0;
			string inv = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6165);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8730);
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);
			dtVendors[0].Value = DateAndTime.Today;
			dtVendors[1].Value = DateAndTime.Today;
			dtVendors[2].Value = DateAndTime.Today;
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			dtVendors[2].Value = System.DBNull.Value;
			WinglGlobal.scan = Conversion.Val(Interaction.GetSetting("Wingl", "Options", "Windoc", "0"));
			if (WinglGlobal.InvoiceBatch) {
				this.Text = "Batch Invoice ";
				lstFreq.Top = dtVendors[0].Top;
				lstFreq.Left = dtVendors[0].Left;
				lstFreq.TabIndex = dtVendors[0].TabIndex;
				lstFreq.Visible = true;
				dtVendors[0].Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Label19.Left) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(Label19.Width) + 10);
				dtVendors[0].CheckBox = false;
				dtVendors[0].Enabled = true;
				dtVendors[0].Visible = true;
				Label19.Visible = true;
				dtVendors[2].Value = DateAndTime.Today;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				dtVendors[2].Value = System.DBNull.Value;
				Frame3.Visible = false;
				Frame4.Visible = false;
				Frame5.Text = "Memo Line";
				Label2.Text = "Description";
				Label3.Text = "Frequency";
				Label4.Text = "Next Due";
				Label9.Text = "%";
			}
			if (WinglGlobal.InvoiceFlag) {
				v = WinglGlobal.tblinvoices.Fields["VendorNumber"].Value;
				inv = WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value;
				inkey = WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
				oldInvoice = inv;
				editflag = 1;
				if (WinglGlobal.InvoiceBatch) {
					this.Text = this.Text + "EDIT";
				} else if (WinglGlobal.scan) {
					if (string.IsNullOrEmpty(Strings.Trim(WinglGlobal.dbname)) | WinglGlobal.cnn == null) {
						WinglGlobal.dbname = Interaction.GetSetting("Wingl", "Options", "WindocFile", "");
						WinglGlobal.cnn = new clsConnection();
						//UPGRADE_WARNING: Couldn't resolve default property of object cnn.OpenDB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object rc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						rc = WinglGlobal.cnn.OpenDB("admin", WinglGlobal.dbname, ADODB.ConnectModeEnum.adModeShareDenyNone);
						//UPGRADE_WARNING: Couldn't resolve default property of object rc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (rc != 0) {
							//UPGRADE_WARNING: Couldn't resolve default property of object rc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							LogError.MsgBox_Renamed("Error openning Windoc Database (" + WinglGlobal.dbname + ") Invoices will not be scanned: (" + rc + ")");
							WinglGlobal.scan = false;
						}
						for (i = Strings.Len(WinglGlobal.dbname); i >= 1; i += -1) {
							if (Strings.Right(WinglGlobal.dbname, 1) == "\\")
								break; // TODO: might not be correct. Was : Exit For
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							WinglGlobal.dbname = Strings.Left(WinglGlobal.dbname, i - 1);
						}
					}
					WinglGlobal.rsd = new ADODB.Recordset();
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.invpGlobal.invnumber = Strings.Trim(inv);
					WinglGlobal.invpGlobal.date_Renamed = WinglGlobal.tblinvoices.Fields["Date"].Value;
					Err().Clear();
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Documents WHERE Vendor = '" + v + "' AND Invoice = '" + Strings.Trim(inv) + "'";
					//UPGRADE_WARNING: Couldn't resolve default property of object cnn.cn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object rsd.Open. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.rsd.Open(xsql, WinglGlobal.cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic);
					//UPGRADE_WARNING: Couldn't resolve default property of object rsd.MoveFirst. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.rsd.MoveFirst();
					if (Err().Number == 0) {
						cmdDoc[1].Visible = true;
					} else {
						Err().Clear();
						WinglGlobal.rss = new ADODB.Recordset();
						//UPGRADE_WARNING: Couldn't resolve default property of object cnn.cn. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object rss.Open. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.rss.Open("SELECT * FROM interface", WinglGlobal.cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
						if (Err().Number != 0) {
							//MsgBox "Error openning 'interface' table (invoices will not be scanned): " & Err.Description
							WinglGlobal.scan = false;
							//UPGRADE_WARNING: Couldn't resolve default property of object rss.Close. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							WinglGlobal.rss.Close();
							//UPGRADE_WARNING: Couldn't resolve default property of object cnn.CloseDB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							WinglGlobal.cnn.CloseDB();
						} else {
							cmdDoc[0].Visible = true;
						}
					}
				}
			} else {
				editflag = 0;
				if (WinglGlobal.InvoiceBatch) {
					this.Text = this.Text + "ENTRY";
					dtVendors[0].Visible = false;
					Label19.Visible = false;
				}
			}
			for (i = 0; i <= 99; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstChkGrp.Items.Add(WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#0"), ref 2));
			}
			WinglGlobal.GetVendors(ref this, ref false);
			WinglGlobal.GetCompanies(ref this);

			Option1[Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0))].Checked = true;
			WinglGlobal.getAccounts(lstAccounts, Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0)));

			WinglGlobal.GetJournals(ref this);
			if (WinglGlobal.InvoiceFlag) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v, "#####");
				lstVendors.ColumnSearch = 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstVendors.SearchText = x;
				lstVendors.SearchIgnoreCase = true;
				lstVendors.Action = LpLib.enumAction.ActionSearch;
				if (lstVendors.SearchIndex >= 0) {
					lstVendors.ListIndex = lstVendors.SearchIndex;
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					WinglGlobal.vName = lstVendors.CtlText;
					if (WinglGlobal.GetInvoice(ref v, ref inv, ref inkey) > 0) {
						UnpackInvoice();
					}
					for (i = 0; i <= lstInvoices.Items.Count - 1; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstInvoices, i) == inv) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstInvoices.SelectedIndex = i;
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstInvoices.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstInvoices, i);
							break; // TODO: might not be correct. Was : Exit For
						}
					}
				}
				WinglGlobal.InvoiceFlag = false;
			} else {
				if (WinglGlobal.lastVendor != -1) {
					lstVendors.ListIndex = WinglGlobal.lastVendor;
				}
				if (WinglGlobal.lastCompany != -1) {
					lstCompanies.ListIndex = WinglGlobal.lastCompany;
				}
				if (WinglGlobal.lastDepartment != -1) {
					lstDepartments.ListIndex = WinglGlobal.lastDepartment;
				}
				//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				if (WinglGlobal.lastInvDate > DateAndTime.DateValue("01/01/1901"))
					dtVendors[0].Value = WinglGlobal.lastInvDate;
				//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				if (WinglGlobal.lastPayDate > DateAndTime.DateValue("01/01/1901")) {
					dtVendors[1].Value = WinglGlobal.lastPayDate;
				} else {
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					dtVendors[1].Value = System.DBNull.Value;
				}
			}
			goto Form_Load_exit;
			Form_Load_error:

			Interaction.Beep();
			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Invoices");
			 // ERROR: Not supported in C#: ResumeStatement

			Form_Load_exit:

			if (WinglGlobal.InvoiceFlag)
				editflag = 1;

		}

//******************************************************************************
		private void Invoice_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			//******************************************************************************

			if (this.Tag != "Report")
				My.MyProject.Forms.Invoices.GetInvoices();
			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblinvoices.Close();
			WinglGlobal.tblVendors.Close();
			WinglGlobal.tblVendorCats.Close();
			WinglGlobal.tblInvDist.Close();

		}
//******************************************************************************
		private void lstAccounts_Change(System.Object eventSender, System.EventArgs eventArgs)
		{
			object dirty = null;
			//******************************************************************************

			//UPGRADE_WARNING: Couldn't resolve default property of object dirty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			dirty = true;

		}
//******************************************************************************
//UPGRADE_WARNING: Event lstChkGrp.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstChkGrp_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			// Deprecated 9/30/2013

			return;

			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstVendors.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstVendors.Row = i;
			lstVendors.Col = 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstCompanies.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstDepartments.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			txtMemo.Text = "";

			//If lstVendors.ListIndex > -1 And lstCompanies.ListIndex > -1 And lstDepartments.ListIndex > -1 Then
			//   If InvoiceBatch Then
			//      v = -Val(lstVendors.ColList)
			//   Else
			//       v = Val(lstVendors.ColList)
			//   End If
			//   xsql = "SELECT Memo FROM Memos WHERE VendorNumber = " & v & " AND "
			//   xsql = xsql & "CompanyNumber = " & lstCompanies.ColList & " AND "
			//   xsql = xsql & "DepartmentNumber = " & lstDepartments.ColList & " AND "
			//   xsql = xsql & "CheckGroup = " & IIf(lstChkGrp.ListIndex = -1, 0, lstChkGrp.ListIndex) & " AND "
			//   xsql = xsql & "BatchNumber = " & BatchNumber
			//   Set tblMemo = GLdb.OpenRecordset(xsql)
			//   If tblMemo.RecordCount > 0 Then
			//       txtMemo.Text = tblMemo("Memo")
			//   Else
			//       txtMemo.Text = ""
			//   End If
			//End If
			//On Error Resume Next
			//tblMemo.Close

		}
//******************************************************************************
		private void lstChkGrp_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			lstChkGrp_SelectedIndexChanged(lstChkGrp, new System.EventArgs());

		}
//******************************************************************************
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short i = 0;

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			if (Conversion.Val(lstCompanies.ColList) > 0) {
				WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
				lstDepartments.ListIndex = 0;
				i = lstDepartments.ListIndex;
				lstDepartments.Row = i;
				lstDepartments.Col = 1;
				lstChkGrp_SelectedIndexChanged(lstChkGrp, new System.EventArgs());
			}

		}
//******************************************************************************
		private void lstCompanies_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			lstChkGrp_SelectedIndexChanged(lstChkGrp, new System.EventArgs());

		}
//******************************************************************************
		private void lstDepartments_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short i = 0;

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			i = lstDepartments.ListIndex;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			lstChkGrp_SelectedIndexChanged(lstChkGrp, new System.EventArgs());

		}
//******************************************************************************
		private void lstDepartments_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			lstChkGrp_SelectedIndexChanged(lstChkGrp, new System.EventArgs());

		}
//******************************************************************************
//UPGRADE_WARNING: Event lstInvoices.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstInvoices_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short n = 0;
			int v = 0;
			string i = null;

			if (WinglGlobal.InvoiceFlag)
				return;
			n = lstInvoices.SelectedIndex;
			if (n >= 0) {
				lstVendors.Row = lstVendors.ListIndex;
				lstVendors.Col = 1;
				v = Convert.ToInt32(lstVendors.ColList);
				i = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstInvoices, n);
				if (WinglGlobal.GetInvoice(v, i) > 0) {
					clearForm(ref false);
					UnpackInvoice();
					editflag = 1;
				}
			}

		}
//******************************************************************************
		private void clearForm(ref bool t)
		{
			object i = null;
			//******************************************************************************

			if (WinglGlobal.InvoiceFlag)
				return;
			if (t) {
				lstInvoices.SelectedIndex = -1;
				lstInvoices.Text = "";
			}
			for (i = 0; i <= 5; i++) {
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtInvoice[i].CtlText = "";
			}
			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			dtVendors[2].Value = System.DBNull.Value;
			lstVendors.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstVendors.AutoSearch = LpLib.enumAutoSearch.AutoSearchMultipleChar;
			lstCompanies.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstCompanies.AutoSearch = LpLib.enumAutoSearch.AutoSearchMultipleChar;
			lstDepartments.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstDepartments.AutoSearch = LpLib.enumAutoSearch.AutoSearchMultipleChar;
			lstAccounts.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstAccounts.AutoSearch = LpLib.enumAutoSearch.AutoSearchMultipleChar;
			lstJournals.Action = LpLib.enumAction.ActionClearSearchBuffer;
			lstJournals.AutoSearch = LpLib.enumAutoSearch.AutoSearchMultipleChar;

		}
//******************************************************************************
		private void UnpackInvoice()
		{
			object xsql = null;
			//******************************************************************************

			string x = null;
			int v = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			if (WinglGlobal.InvoiceBatch) {
				dtVendors[0].CheckBox = true;
			}
			WinGlobal.move_tbl_scr(ref WinglGlobal.tblinvoices, ref this);
			x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblinvoices.Fields["Company"].Value, "###");
			lstCompanies.ColumnSearch = 1;
			lstCompanies.SearchText = x;
			lstCompanies.SearchIgnoreCase = true;
			lstCompanies.Action = LpLib.enumAction.ActionSearch;
			if (lstCompanies.SearchIndex >= 0) {
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
			}
			x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblinvoices.Fields["Department"].Value, "####");
			lstDepartments.ColumnSearch = 1;
			lstDepartments.SearchText = x;
			lstDepartments.SearchIgnoreCase = true;
			lstDepartments.Action = LpLib.enumAction.ActionSearch;
			if (lstDepartments.SearchIndex >= 0) {
				lstDepartments.ListIndex = lstDepartments.SearchIndex;
			}
			v = WinglGlobal.tblinvoices.Fields["VendorNumber"].Value;
			if (!WinglGlobal.InvoiceBatch) {
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				lstPayFlag.SelectedIndex = (Information.IsDBNull(WinglGlobal.tblinvoices.Fields["PayFlag"].Value) | WinglGlobal.tblinvoices.Fields["PayFlag"].Value > 4 ? -1 : WinglGlobal.tblinvoices.Fields["Payflag"].Value);
			} else {
				lstFreq.SelectedIndex = WinglGlobal.tblinvoices.Fields["PayFlag"].Value;
				v = -v;
			}
			GetComment(ref v);
			lstChkGrp.SelectedIndex = WinglGlobal.tblinvoices.Fields["CheckGroup"].Value;
			//xsql = "SELECT Memo FROM Memos WHERE VendorNumber = " & v & " AND "
			//xsql = xsql & "CompanyNumber = " & tblinvoices("Company") & " AND "
			//xsql = xsql & "DepartmentNumber = " & tblinvoices("Department") & " AND "
			//xsql = xsql & "CheckGroup = " & tblinvoices("CheckGroup") & " AND "
			//xsql = xsql & "BatchNumber = " & tblinvoices("Batch")
			//If Not InvoiceBatch Then
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT Memo, MemoId FROM Memos WHERE MemoId = " + WinglGlobal.tblinvoices.Fields["MemoId"].Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblMemo = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblMemo.RecordCount > 0) {
				txtMemo.Text = WinglGlobal.tblMemo.Fields["Memo"].Value;
				MemoId = WinglGlobal.tblMemo.Fields["MemoId"].Value;
				WinglGlobal.tblMemo.Close();
			} else {
				txtMemo.Text = "";
			}
			//Else
			//    txtMemo.Text = IIf(IsNull(tblInvoices("Memo")), "", tblInvoices("Memo"))
			//End If
			chkSplit.CheckState = System.Windows.Forms.CheckState.Unchecked;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + WinglGlobal.tblinvoices.Fields["InvoiceKey"].Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblInvDist.RecordCount > 0) {
				WinglGlobal.tblInvDist.MoveLast();
				if (WinglGlobal.tblInvDist.RecordCount == 1) {
					x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblInvDist.Fields["Account"].Value, "####");
					lstAccounts.ColumnSearch = 1;
					lstAccounts.SearchText = x;
					lstAccounts.SearchIgnoreCase = true;
					lstAccounts.Action = LpLib.enumAction.ActionSearch;
					if (lstAccounts.SearchIndex >= 0) {
						lstAccounts.ListIndex = lstAccounts.SearchIndex;
					}
					lstJournals.ColumnSearch = 0;
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblInvDist.Fields["Journal"].Value)) {
						lstJournals.SearchText = Strings.Trim(WinglGlobal.tblInvDist.Fields["Journal"].Value);
						lstJournals.SearchIgnoreCase = true;
						lstJournals.Action = LpLib.enumAction.ActionSearch;
						if (lstJournals.SearchIndex >= 0) {
							lstJournals.ListIndex = lstJournals.SearchIndex;
						}
					}
				} else {
					chkSplit.CheckState = System.Windows.Forms.CheckState.Checked;
				}
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblInvDist.Close();
			WinglGlobal.tblMemo.Close();
			if (WinglGlobal.InvoiceBatch) {
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(dtVendors[0].Value)) {
					Label19.Visible = false;
					dtVendors[0].Visible = false;
				} else {
					dtVendors[0].CheckBox = false;
				}
			}
			goto UnpackInvoice_exit;
			UnpackInvoice_error:

			Interaction.Beep();
			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Invoice Edit");
			 // ERROR: Not supported in C#: ResumeStatement

			UnpackInvoice_exit:


		}
//******************************************************************************
		private void PackInvoice()
		{
			object xsql = null;
			object v = null;
			//******************************************************************************

			short i = 0;
			short p = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			WinGlobal.move_scr_tbl(ref WinglGlobal.tblinvoices, ref this);
			i = lstVendors.ListIndex;
			if (i > -1) {
				lstVendors.Row = i;
				lstVendors.Col = 1;
				WinglGlobal.tblinvoices.Fields["VendorNumber"].Value = lstVendors.ColList;
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				p = Strings.InStr(lstVendors.CtlText, "[");
				if (p == 0) {
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					WinglGlobal.vName = lstVendors.CtlText;
				} else {
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					WinglGlobal.vName = Strings.Left(lstVendors.CtlText, p - 1);
				}
			} else {
				Interaction.Beep();
				LogError.MsgBox_Renamed("Vendor must be selected.", , "Invoices");
				goto PackInvoice_exit;
			}
			WinglGlobal.tblinvoices.Fields["InvoiceNumber"].Value = lstInvoices.Text;
			i = lstCompanies.ListIndex;
			if (i > -1) {
				lstCompanies.Row = i;
				lstCompanies.Col = 1;
				WinglGlobal.tblinvoices.Fields["Company"].Value = lstCompanies.ColList;
			} else {
				WinglGlobal.tblinvoices.Fields["Company"].Value = 0;
			}
			i = lstDepartments.ListIndex;
			if (i > -1) {
				lstDepartments.Row = i;
				lstDepartments.Col = 1;
				WinglGlobal.tblinvoices.Fields["Department"].Value = lstDepartments.ColList;
			} else {
				WinglGlobal.tblinvoices.Fields["Department"].Value = 0;
			}
			if (!WinglGlobal.InvoiceBatch) {
				WinglGlobal.tblinvoices.Fields["PayFlag"].Value = (lstPayFlag.SelectedIndex < 0 ? 0 : lstPayFlag.SelectedIndex);
			} else {
				WinglGlobal.tblinvoices.Fields["PayFlag"].Value = (lstFreq.SelectedIndex < 0 ? 0 : lstFreq.SelectedIndex);
			}
			i = lstChkGrp.SelectedIndex;
			WinglGlobal.tblinvoices.Fields["CheckGroup"].Value = (i == -1 ? 0 : i);
			if (WinglGlobal.tblinvoices.Fields["CheckNumber"].Value > 0) {
				WinglGlobal.tblinvoices.Fields["DiscountAmount"].Value = WinglGlobal.tblinvoices.Fields["Discount"].Value;
			} else {
				WinglGlobal.tblinvoices.Fields["DiscountAmount"].Value = System.Math.Round((WinglGlobal.tblinvoices.Fields["Amount"].Value - WinglGlobal.tblinvoices.Fields["Allowance"].Value) * (WinglGlobal.tblinvoices.Fields["Discount"].Value * 0.01), 2);
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object v. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			v = WinglGlobal.tblinvoices.Fields["VendorNumber"].Value;
			if (WinglGlobal.InvoiceBatch) {
				//UPGRADE_WARNING: Couldn't resolve default property of object v. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				v = -v;
			}
			//xsql = "SELECT Memo FROM Memos WHERE VendorNumber = " & v & " AND "
			//xsql = xsql & "CompanyNumber = " & tblinvoices("Company") & " AND "
			//xsql = xsql & "DepartmentNumber = " & tblinvoices("Department") & " AND "
			//xsql = xsql & "CheckGroup = " & tblinvoices("CheckGroup") & " AND "
			//xsql = xsql & "BatchNumber = " & tblinvoices("Batch")
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT Memo, MemoId FROM Memos WHERE MemoId = " + WinglGlobal.tblinvoices.Fields["MemoId"].Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblMemo = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblMemo.RecordCount > 0) {
				if (string.IsNullOrEmpty(Strings.Trim(txtMemo.Text))) {
					WinglGlobal.tblMemo.Delete();
				} else {
					WinglGlobal.tblMemo.Edit();
					WinglGlobal.tblMemo.Fields["Memo"].Value = txtMemo.Text;
					WinglGlobal.tblMemo.Update();
				}
				WinglGlobal.tblMemo.Close();
			} else {
				if (!string.IsNullOrEmpty(Strings.Trim(txtMemo.Text))) {
					WinglGlobal.tblMemo = WinglGlobal.GLdb.OpenRecordset("Memos");
					WinglGlobal.tblMemo.AddNew();
					//UPGRADE_WARNING: Couldn't resolve default property of object v. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblMemo.Fields["VendorNumber"].Value = v;
					WinglGlobal.tblMemo.Fields["CompanyNumber"].Value = WinglGlobal.tblinvoices.Fields["Company"].Value;
					WinglGlobal.tblMemo.Fields["DepartmentNumber"].Value = WinglGlobal.tblinvoices.Fields["Department"].Value;
					WinglGlobal.tblMemo.Fields["CheckGroup"].Value = WinglGlobal.tblinvoices.Fields["CheckGroup"].Value;
					WinglGlobal.tblMemo.Fields["Memo"].Value = txtMemo.Text;
					WinglGlobal.tblMemo.Fields["BatchNumber"].Value = WinglGlobal.tblinvoices.Fields["Batch"].Value;
					WinglGlobal.tblMemo.Update();
					WinglGlobal.tblMemo.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinglGlobal.tblMemo.LastModified);
					MemoId = WinglGlobal.tblMemo.Fields["MemoId"].Value;
					WinglGlobal.tblMemo.Close();
					WinglGlobal.tblinvoices.Fields["MemoId"].Value = MemoId;
				}
			}
			//Else
			//    tblInvoices("Memo") = IIf(IsNull(txtMemo.Text), "", txtMemo.Text)
			//End If
			goto PackInvoice_exit;
			PackInvoice_error:

			Interaction.Beep();
			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Invoice");
			 // ERROR: Not supported in C#: ResumeStatement

			PackInvoice_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblMemo.Close();

		}
//******************************************************************************
		private void lstJournals_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			lstJournals.Row = lstJournals.ListIndex;

		}
//******************************************************************************
		private void lstVendors_Change(System.Object eventSender, System.EventArgs eventArgs)
		{
			object dirty = null;
			//******************************************************************************

			//UPGRADE_WARNING: Couldn't resolve default property of object dirty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			dirty = true;

		}
//******************************************************************************
		private void lstVendors_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short i = 0;
			int v = 0;

			i = lstVendors.ListIndex;
			if (i >= 0) {
				lstVendors.Row = i;
				lstVendors.Col = 1;
				v = Convert.ToInt32(lstVendors.ColList);
				GetComment(ref v);
				WinglGlobal.GetInvoices(ref this, ref v);
				if (editflag == 1) {
					clearForm(ref true);
					if (lstInvoices.Items.Count > 0) {
						lstInvoices.SelectedIndex = 0;
						lstInvoices.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstInvoices, 0);
						lstInvoices.Refresh();
					}
				}
			}

		}
		private void GetComment(ref int v)
		{
			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			string x = null;
			lblComment.Text = "";
			lblComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
			lblComment.BackColor = System.Drawing.ColorTranslator.FromOle(0xffffc0);
			if (v <= 0)
				return;
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT Comment FROM Vendors WHERE VendorNumber = " + v);
			if (tbl.RecordCount > 0) {
				x = Strings.Trim(WinGlobal.noNulls(ref tbl.Fields["Comment"]));
				if (!string.IsNullOrEmpty(x)) {
					lblComment.BackColor = System.Drawing.ColorTranslator.FromOle(0xc0ffff);
					lblComment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					lblComment.Text = x;
				}
			}
			tbl.Close();
			//UPGRADE_NOTE: Object tbl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			tbl = null;
		}
//******************************************************************************
		private void getDistributions(ref int i)
		{
			object x = null;
			//******************************************************************************

			string xsql = null;

			chkSplit.CheckState = System.Windows.Forms.CheckState.Unchecked;
			xsql = "SELECT * FROM " + WinglGlobal.IDfilename + " WHERE InvoiceKey = " + i;
			WinglGlobal.tblInvDist = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblInvDist.RecordCount > 0) {
				WinglGlobal.tblInvDist.MoveLast();
				if (WinglGlobal.tblInvDist.RecordCount == 1) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblInvDist.Fields["Account"].Value, "####");
					lstAccounts.ColumnSearch = 1;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstAccounts.SearchText = x;
					lstAccounts.SearchIgnoreCase = true;
					lstAccounts.Action = LpLib.enumAction.ActionSearch;
					if (lstAccounts.SearchIndex >= 0) {
						lstAccounts.ListIndex = lstAccounts.SearchIndex;
					}
					for (i = 0; i <= lstJournals.ListCount - 1; i++) {
						if (WinglGlobal.tblInvDist.Fields["Journal"].Value == lstJournals.List[i]) {
							lstJournals.SearchIndex = i;
							lstJournals.ListIndex = i;
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							lstJournals.CtlText = lstJournals.List[i];
							break; // TODO: might not be correct. Was : Exit For
						}
					}
				} else {
					chkSplit.CheckState = System.Windows.Forms.CheckState.Checked;
				}
			}

		}
//******************************************************************************
//UPGRADE_WARNING: Event Option1.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void Option1_CheckedChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (eventSender.Checked) {
				short Index = Option1.GetIndex(eventSender);
				//******************************************************************************

				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Conversion.Str(Index));
				WinglGlobal.getAccounts(lstAccounts, Index);

			}
		}
//******************************************************************************
		private void txtInvoice_KeyPressEvent(System.Object eventSender, AxMBText.__TextField_KeyPressEvent eventArgs)
		{
			short Index = txtInvoice.GetIndex(eventSender);
			object dirty = null;
			//******************************************************************************

			//UPGRADE_WARNING: Couldn't resolve default property of object dirty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			dirty = true;

		}
//******************************************************************************
		private void txtMemo_KeyPress(System.Object eventSender, System.Windows.Forms.KeyPressEventArgs eventArgs)
		{
			short KeyAscii = Strings.Asc(eventArgs.KeyChar);
			object dirty = null;
			//******************************************************************************

			//UPGRADE_WARNING: Couldn't resolve default property of object dirty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			dirty = true;

			eventArgs.KeyChar = Strings.Chr(KeyAscii);
			if (KeyAscii == 0) {
				eventArgs.Handled = true;
			}
		}
	}
}
