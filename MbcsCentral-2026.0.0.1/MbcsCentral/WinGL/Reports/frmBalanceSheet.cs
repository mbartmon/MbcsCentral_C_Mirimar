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
	internal partial class frmBalanceSheet : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
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
		public clsBalanceSheet clas;

//******************************************************************************
//UPGRADE_WARNING: Event chkAccount.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkAccount_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			if (chkAccount.CheckState == 1) {
				lstAccounts.ListIndex = -1;
				lstAccounts.Row = -1;
				//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				lstAccounts.Refresh();
				lstAccounts.Enabled = false;
			} else {
				lstAccounts.Enabled = true;
			}

		}
//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			//******************************************************************************

			switch (Index) {

				case 0:
					// Print
					if (lstCompanies.ListIndex == -1) {
						Interaction.MsgBox("You must select a company to print.", , "Balance Sheet");
						return;
					} else {
						lstCompanies.Col = 1;
						lstCompanies.Row = lstCompanies.ListIndex;
						clas.Company = Convert.ToInt32(lstCompanies.ColList);
						lstCompanies.Col = 0;
						clas.CompanyName = lstCompanies.ColList;
						lstCompanies.Col = 1;
						clas.Company = Conversion.Val(lstCompanies.ColList);
					}
					if (!chkConsolidated.CheckState & lstDepartments.ListIndex == -1) {
						Interaction.MsgBox("You must select a department to print.", , "Balance Sheet");
						return;
					} else {
						lstDepartments.Col = 1;
						lstDepartments.Row = lstDepartments.ListIndex;
						clas.Department = Convert.ToInt32(lstDepartments.ColList);
						clas.Consolidated = chkConsolidated.CheckState;
					}
					if (lstAccounts.ListIndex == -1) {
						if (Interaction.MsgBox("You have not selected a profit/loss account.  Continue?", MsgBoxStyle.YesNo, "Balance Sheet") == MsgBoxResult.No) {
							return;
						}
					}
					lstAccounts.Col = 1;
					lstAccounts.Row = lstAccounts.ListIndex;
					clas.PLAccount = (string.IsNullOrEmpty(Strings.Trim(lstAccounts.ColList)) ? -1 : lstAccounts.ColList);
					lstAccounts.Col = 0;
					clas.PLAccountName = lstAccounts.ColList;
					clas.rptMonth = lstMonth.SelectedIndex + 1;
					clas.rptYear = optYear[0].Checked;
					break;
				case 1:
					// Cancel
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();

		}
		private void frmBalanceSheet_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);

			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(lstAccounts, , "003_");

			WinglGlobal.RecallForm(ref this);

			if (clas.Company > 0) {
				lstCompanies.SearchText = Convert.ToString(clas.Company);
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				if (lstCompanies.ListIndex > -1) {
					lstCompanies.Col = 1;
					lstCompanies.Row = lstCompanies.ListIndex;
					WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
					if (clas.Department > 0) {
						lstDepartments.SearchText = Convert.ToString(clas.Department);
						lstDepartments.ColumnSearch = 1;
						lstDepartments.Action = LpLib.enumAction.ActionSearch;
						lstDepartments.ListIndex = lstDepartments.SearchIndex;
					}
				}
				optYear[0].Checked = (clas.rptYear == 0 ? false : true);
				optYear[1].Checked = (clas.rptYear == 0 ? true : false);
			}
			if (clas.PLAccount > 0) {
				lstAccounts.SearchText = Convert.ToString(clas.PLAccount);
				lstAccounts.ColumnSearch = 1;
				lstAccounts.Action = LpLib.enumAction.ActionSearch;
				lstAccounts.ListIndex = lstAccounts.SearchIndex;
			}
			chkConsolidated.CheckState = clas.Consolidated;
			if (clas.rptMonth > 0) {
				lstMonth.SelectedIndex = clas.rptMonth - 1;
			} else {
				lstMonth.SelectedIndex = DateAndTime.Month(DateAndTime.Today) - 1;
			}

		}
		private void frmBalanceSheet_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}

		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstCompanies.ListIndex > -1) {
				lstCompanies.Col = 1;
				lstCompanies.Row = lstCompanies.ListIndex;
				WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
				lstDepartments.ListIndex = 0;
			}

		}
//UPGRADE_WARNING: Event chkConsolidated.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkConsolidated_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkConsolidated.CheckState) {
				lstDepartments.Enabled = true;
			} else {
				lstDepartments.Enabled = false;
			}

		}
	}
}
