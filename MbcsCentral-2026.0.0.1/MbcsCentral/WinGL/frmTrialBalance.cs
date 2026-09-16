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
	internal partial class frmTrialBalance : System.Windows.Forms.Form
	{
		public clsTrialBalance clas;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			switch (Index) {

				case 0:
					// Print
					if (lstCompanies.ListIndex == -1) {
						Interaction.MsgBox("You must select a company to print.", , "Trial Balance");
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
					clas.Consolidated = chkConsolidated.CheckState;
					if (lstDepartments.ListIndex > -1) {
						lstDepartments.Col = 1;
						lstDepartments.Row = lstDepartments.ListIndex;
						clas.Department = Convert.ToInt32(lstDepartments.ColList);
					} else {
						clas.Department = -1;
					}
					clas.ZeroBalances = chkZero.CheckState;
					clas.XWorkArea = chkXWorkArea.CheckState;
					clas.rptMonth = lstMonth.SelectedIndex + 1;
					clas.PriorYear = (optYear[0].Checked == 0 ? true : false);
					clas.rptYear = DateAndTime.Year(DateAndTime.Today);
					if (clas.PriorYear) {
						clas.rptYear = clas.rptYear - 1;
					}
					break;
				case 1:
					// Cancel
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();

		}
		private void frmTrialBalance_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);

			WinglGlobal.GetCompanies(ref this);

			lstMonth.SelectedIndex = DateAndTime.Month(DateAndTime.Today) - 1;

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
					chkZero.CheckState = (clas.ZeroBalances ? 1 : 0);
					chkXWorkArea.CheckState = (clas.XWorkArea ? 1 : 0);
					lstMonth.SelectedIndex = clas.rptMonth - 1;
					optYear[0].Checked = !clas.PriorYear;
					optYear[1].Checked = clas.PriorYear;
				}
			} else {
				WinglGlobal.RecallForm(ref this);
			}

		}
		private void frmTrialBalance_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}

		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstCompanies.ListIndex > -1) {
				lstCompanies.Col = 1;
				lstCompanies.Row = lstCompanies.ListIndex;
				WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
				lstDepartments.ListIndex = -1;
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
