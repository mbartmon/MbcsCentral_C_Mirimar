using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
namespace WinGL
{
	internal partial class frmLedger : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.1  2009/08/21 12:36:13  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:07  mike
//* Copied from wingl folder
//*
		public clsLedger clas;
//UPGRADE_WARNING: Event chkAccount.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkAccount_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkAccount.CheckState) {
				lstAccounts.Enabled = true;
			} else {
				lstAccounts.Enabled = false;
			}

		}
//UPGRADE_WARNING: Event chkDepartment.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkDepartment_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkDepartment.CheckState) {
				lstDepartments.Enabled = true;
			} else {
				lstDepartments.Enabled = false;
			}

		}
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// Print
					if (lstCompanies.ListIndex == -1) {
						Interaction.MsgBox("You must select a company to print.", , "General Ledger Report");
						return;
					} else {
						lstCompanies.Col = 1;
						lstCompanies.Row = lstCompanies.ListIndex;
						clas.Company = Convert.ToInt32(lstCompanies.ColList);
						lstCompanies.Col = 0;
						clas.CompanyName = lstCompanies.ColList;
					}
					if (chkDepartment.CheckState & lstDepartments.ListIndex == -1) {
						Interaction.MsgBox("You must select a department to print.", , "General Ledger Report");
						return;
					} else {
						lstDepartments.Col = 1;
						lstDepartments.Row = lstDepartments.ListIndex;
						clas.Department = (chkDepartment.CheckState ? lstDepartments.ColList : -1);
						if (chkDepartment.CheckState) {
							lstDepartments.Col = 0;
							clas.DepartmentName = lstDepartments.ColList;
						}
					}
					if (chkAccount.CheckState & lstAccounts.ListIndex == -1) {
						Interaction.MsgBox("You must select an account to print.", , "General Ledger Report");
						return;
					} else {
						lstAccounts.Col = 1;
						lstAccounts.Row = lstAccounts.ListIndex;
						clas.Account = (chkAccount.CheckState ? lstAccounts.ColList : -1);
					}
					clas.ZeroBalances = chkBalance.CheckState;
					clas.Consolidated = chkConsolidate.CheckState;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtStart._Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.StartDate = dtStart._Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtEnd._Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.EndDate = dtEnd._Value;
					break;

				case 1:
					// Cancel
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();

		}
		private void frmLedger_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);

			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(lstAccounts);
			//dtStart.Value = DateSerial(Year(date), 1, 1)
			//dtEnd.Value = date
			WinglGlobal.RecallForm(ref this);

			if (clas.Company > 0) {
				lstCompanies.SearchText = Convert.ToString(clas.Company);
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				if (lstCompanies.ListIndex > -1 & clas.Department > 0) {
					lstCompanies.Col = 1;
					lstCompanies.Row = lstCompanies.ListIndex;
					WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
					lstDepartments.SearchText = Convert.ToString(clas.Department);
					lstDepartments.ColumnSearch = 1;
					lstDepartments.Action = LpLib.enumAction.ActionSearch;
					lstDepartments.ListIndex = lstDepartments.SearchIndex;
					chkDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
				}
				if (clas.Account > -1) {
					lstAccounts.SearchText = Convert.ToString(clas.Account);
					lstAccounts.ColumnSearch = 1;
					lstAccounts.Action = LpLib.enumAction.ActionSearch;
					lstAccounts.ListIndex = lstAccounts.SearchIndex;
					if (lstAccounts.ListIndex > -1)
						chkAccount.CheckState = System.Windows.Forms.CheckState.Checked;
				}
				chkBalance.CheckState = (clas.ZeroBalances ? 1 : 0);
				chkConsolidate.CheckState = (clas.Consolidated ? 1 : 0);
				dtStart.Value = clas.StartDate;
				dtEnd.Value = clas.EndDate;
			}

		}
		private void frmLedger_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}

		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstCompanies.ListIndex > -1) {
				lstCompanies.Col = 1;
				lstCompanies.Row = lstCompanies.ListIndex;
				WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
			}

		}
	}
}
