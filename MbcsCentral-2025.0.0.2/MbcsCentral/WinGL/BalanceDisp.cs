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
	internal partial class BalanceDisp : System.Windows.Forms.Form
	{
//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		DAO.Recordset tbl;
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Close();

		}
		private void BalanceDisp_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4785);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7935);
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(0 + WinglGlobal.fbalc * 1000);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(0 + WinglGlobal.fbalc * 1000);

			Option1[Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0))].Checked = true;
			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.getAccounts(ref lstAccounts, ref Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0)), ref "", ref false);

			for (i = 1; i <= 4; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//grdBalance.set_ColWidth(i,  , 1200)
			}
			grdBalance.Col = 1;
			grdBalance.Row = 0;
			grdBalance.Text = "Current Year";
			grdBalance.Col = 2;
			grdBalance.Text = "YTD";
			grdBalance.Col = 3;
			grdBalance.Text = "Prior Year";
			grdBalance.Col = 4;
			grdBalance.Text = "YTD";
			grdBalance.Row = 1;
			grdBalance.Col = 0;
			grdBalance.Text = "Jan";
			grdBalance.Row = 2;
			grdBalance.Text = "Feb";
			grdBalance.Row = 3;
			grdBalance.Text = "Mar";
			grdBalance.Row = 4;
			grdBalance.Text = "Apr";
			grdBalance.Row = 5;
			grdBalance.Text = "May";
			grdBalance.Row = 6;
			grdBalance.Text = "Jun";
			grdBalance.Row = 7;
			grdBalance.Text = "Jul";
			grdBalance.Row = 8;
			grdBalance.Text = "Aug";
			grdBalance.Row = 9;
			grdBalance.Text = "Sep";
			grdBalance.Row = 10;
			grdBalance.Text = "Oct";
			grdBalance.Row = 11;
			grdBalance.Text = "Nov";
			grdBalance.Row = 12;
			grdBalance.Text = "Dec";

		}
		private void BalanceDisp_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			//UPGRADE_NOTE: Object fbal() may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			WinglGlobal.fbal[WinglGlobal.fbalc] = null;
			WinglGlobal.fbalc = WinglGlobal.fbalc - 1;

		}
		private void lstAccounts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			short i = 0;
			short j = 0;
			decimal c = default(decimal);
			decimal a = default(decimal);
			decimal p = default(decimal);
			int acct = 0;
			int cmp = 0;
			int dpt = 0;
			int rdate = 0;
			//UPGRADE_WARNING: Arrays in structure tblCmp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			System.DateTime sdate = default(System.DateTime);
			System.DateTime edate = default(System.DateTime);
			DAO.Recordset tblCmp = null;

			for (i = 1; i <= 12; i++) {
				grdBalance.Row = i;
				for (j = 1; j <= 4; j++) {
					grdBalance.Col = j;
					grdBalance.Text = "";
				}
			}
			i = lstCompanies.ListIndex;
			if (i == -1)
				goto lstAccounts_Click_exit;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			cmp = Convert.ToInt32(lstCompanies.ColList);
			i = lstDepartments.ListIndex;
			if (i == -1)
				goto lstAccounts_Click_exit;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			dpt = Convert.ToInt32(lstDepartments.ColList);
			i = lstAccounts.ListIndex;
			lstAccounts.Row = i;
			lstAccounts.Col = 1;
			acct = Convert.ToInt32(lstAccounts.ColList);
			tblCmp = WinglGlobal.GLdb.OpenRecordset("Select * FROM Company WHERE [CompanyNumber] = " + cmp);
			if (!tblCmp.EOF) {
				if (tblCmp.Fields["CurrentFiscalYear"].Value > 0) {
					rdate = tblCmp.Fields["CurrentFiscalYear"].Value;
				} else {
					rdate = DateAndTime.Year(DateAndTime.Today);
				}
				tblCmp.Close();
			} else {
				rdate = DateAndTime.Year(DateAndTime.Today);
			}
			//xsql = "SELECT * FROM Balances WHERE CompanyNumber = " & lstCompanies.ColList & " AND DepartmentNumber = " & lstDepartments.ColList & " AND AccountNumber = " & lstAccounts.ColList
			//Set tbl = GLdb.OpenRecordset(xsql)
			//If tbl.RecordCount > 0 Then
			c = 0;
			p = 0;
			for (i = 1; i <= 12; i++) {
				sdate = DateAndTime.DateSerial(rdate, i, 1);
				edate = DateAndTime.DateSerial(rdate, i + 1, 1 - 1);
				grdBalance.Col = 1;
				grdBalance.Row = i;
				//grdBalance.Text = Format(tbl("C_Month" & Format(i, "00")), "###,###,##0.00")
				a = WinglGlobal.AcctBal(ref cmp, ref acct, ref sdate, ref edate, ref dpt);
				grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,###,##0.00");
				c = c + a;
				grdBalance.Col = 2;
				grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(c, "###,###,##0.00");
				grdBalance.Col = 3;
				sdate = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today) - 1, i, 1);
				edate = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today) - 1, i + 1, 1 - 1);
				a = WinglGlobal.AcctBal(ref cmp, ref acct, ref sdate, ref edate, ref dpt);
				grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,###,##0.00");
				p = p + a;
				grdBalance.Col = 4;
				grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(p, "###,###,##0.00");
			}
			lstAccounts_Click_exit:
			//End If
			//tbl.Close


		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			short i = 0;
			short j = 0;

			for (i = 1; i <= 12; i++) {
				grdBalance.Row = i;
				for (j = 1; j <= 4; j++) {
					grdBalance.Col = j;
					grdBalance.Text = "";
				}
			}

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			if (!string.IsNullOrEmpty(Strings.Trim(lstCompanies.ColList)))
				WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			lstDepartments.CtlText = " ";

			i = lstDepartments.ListIndex;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;

		}
		private void lstDepartments_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			short i = 0;

			i = lstCompanies.ListIndex;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			i = lstDepartments.ListIndex;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			if (lstAccounts.ListIndex >= 0) {
				lstAccounts_ClickEvent(lstAccounts, new System.EventArgs());
			}

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
