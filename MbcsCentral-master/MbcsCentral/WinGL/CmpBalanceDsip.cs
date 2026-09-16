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
	internal partial class CmpBalanceDisp : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.2  2011/02/10 00:29:36  mike
//* Added Active flag to Accounts to prevent display of inactive accounts in entry screens
//*
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.1  2009/08/21 12:36:12  mike
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
//* Revision 2.15  by: normal  Rev date: 12/24/2002 5:28:14 PM
//*   Added multiple years to Balances by Company and by Account
//*
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:33:44 AM
//*   Initial revision.
//*
//* $Endlog$
//* $Endlog$
		private short cm;
		private short sYear;
		private System.DateTime StartDate;
		private System.DateTime EndDate;
		private int cmp;
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Close();

		}
		private void CmpBalanceDisp_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4695);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7995);
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(0 + WinglGlobal.fbalc * 1000);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(0 + WinglGlobal.fbalc * 1000);

			Option1[Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0))].Checked = true;
			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(ref lstAccounts, ref Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0)), ref "", ref false);

			//grdBalance.set_ColWidth(0,  , 600)
			//grdBalance.set_ColWidth(1,  , 2500)
			//grdBalance.set_ColWidth(2,  , 1200)
			//grdBalance.set_ColWidth(3,  , 1200)
			grdBalance.Col = 0;
			grdBalance.Row = 0;
			grdBalance.Text = "Dpt";
			grdBalance.Col = 1;
			grdBalance.Text = "Name";
			grdBalance.Col = 2;
			grdBalance.Text = "Current";
			grdBalance.Col = 3;
			grdBalance.Text = "Prior";

			cm = DateAndTime.Month(DateAndTime.Today) - 1;
			lstMonths.SelectedIndex = cm;
			lstYears.Items.Add(Convert.ToString(DateAndTime.Year(DateAndTime.Today)));
			lstYears.SelectedIndex = 0;

		}
		private void CmpBalanceDisp_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			//UPGRADE_NOTE: Object fbal() may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			WinglGlobal.fbal[WinglGlobal.fbalc] = null;
			WinglGlobal.fbalc = WinglGlobal.fbalc - 1;

		}
		private void lstAccounts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object xsql = null;
			//UPGRADE_WARNING: Arrays in structure tbldpt may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			short j = 0;
			decimal c = default(decimal);
			decimal p = default(decimal);
			DAO.Recordset tbl = null;
			DAO.Recordset tbldpt = null;
			decimal a = default(decimal);
			int acct = 0;
			int dpt = 0;

			if (cmp <= 0 | sYear <= 0)
				goto lstAccounts_Click_exit;
			if (lstAccounts.ListIndex < 0 | lstMonths.SelectedIndex < 0)
				goto lstAccounts_Click_exit;
			i = lstAccounts.ListIndex;
			lstAccounts.Row = i;
			lstAccounts.Col = 1;
			acct = Convert.ToInt32(lstAccounts.ColList);
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + lstCompanies.ColList + " AND AccountNumber = " + lstAccounts.ColList + " ORDER BY DepartmentNumber";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tbl.RecordCount > 0) {
				tbl.MoveLast();
				grdBalance.Rows = tbl.RecordCount + 1;
				tbl.MoveFirst();
				i = 0;
				while (!tbl.EOF) {
					i = i + 1;
					grdBalance.Row = i;
					grdBalance.Col = 0;
					dpt = tbl.Fields["DepartmentNumber"].Value;
					grdBalance.Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dpt, "0000"), ref 4);
					grdBalance.Col = 1;
					tbldpt = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Department WHERE [Department Number] = " + dpt);
					if (tbldpt.RecordCount > 0) {
						grdBalance.Text = Strings.Trim(tbldpt.Fields["Name"].Value);
					} else {
						grdBalance.Text = "* NOT ON FILE *";
					}
					tbldpt.Close();
					grdBalance.Col = 2;
					a = WinglGlobal.AcctBal(ref cmp, ref acct, ref DateAndTime.DateSerial(sYear, cm + 1, 1), ref DateAndTime.DateSerial(sYear, cm + 2, 1 - 1), ref dpt);
					grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,###,##0.00");
					grdBalance.Col = 3;
					a = WinglGlobal.AcctBal(ref cmp, ref acct, ref DateAndTime.DateSerial(sYear - 1, cm + 1, 1), ref DateAndTime.DateSerial(sYear - 1, cm + 2, 1 - 1), ref dpt);
					grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,###,##0.00");
					tbl.MoveNext();
				}
			}
			tbl.Close();
			lstAccounts_Click_exit:


		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			short j = 0;
			short i = 0;
			short idx = 0;

			for (i = 1; i <= grdBalance.Rows - 1; i++) {
				grdBalance.Row = i;
				for (j = 0; j <= 3; j++) {
					grdBalance.Col = j;
					grdBalance.Text = "";
				}
			}

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			cmp = Convert.ToInt32(lstCompanies.ColList);
			WinglGlobal.TransDateRange(ref cmp, ref StartDate, ref EndDate);
			idx = (lstYears.SelectedIndex == -1 ? 0 : lstYears.SelectedIndex);
			lstYears.Items.Clear();
			for (i = DateAndTime.Year(StartDate); i <= DateAndTime.Year(EndDate); i++) {
				lstYears.Items.Add(Convert.ToString(i));
			}
			lstYears.SelectedIndex = idx;

			lstAccounts_ClickEvent(lstAccounts, new System.EventArgs());
			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;

		}
//UPGRADE_WARNING: Event lstMonths.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstMonths_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstMonths.SelectedIndex > -1)
				cm = lstMonths.SelectedIndex;
			lstAccounts_ClickEvent(lstAccounts, new System.EventArgs());

		}
//UPGRADE_WARNING: Event lstYears.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstYears_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstYears.SelectedIndex > -1)
				sYear = Convert.ToInt16(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstYears, lstYears.SelectedIndex));
			lstAccounts_ClickEvent(lstAccounts, new System.EventArgs());

		}

//UPGRADE_WARNING: Event Option1.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void Option1_CheckedChanged(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (eventSender.Checked) {
				short Index = Option1.GetIndex(eventSender);

				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Conversion.Str(Index));
				WinglGlobal.getAccounts(lstAccounts, Index);

			}
		}
	}
}
