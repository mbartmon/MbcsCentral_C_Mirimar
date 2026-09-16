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
	internal partial class BudgetDisp : System.Windows.Forms.Form
	{
		int c;
		int a;
		int d;
//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			if (Cmd.Text == "EXIT") {
				this.Close();
			} else {
				Cmd.Text = "EXIT";
				grdOverview.Visible = true;
				lstCompanies.Enabled = true;
				lstDepartments.Enabled = true;
				Label2.Text = "DOUBLE-CLICK to view monthly detail for an account.";
			}

		}
//******************************************************************************
		private void BudgetDisp_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(5580);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(9120);
			WinglGlobal.GetCompanies(ref this);
			Label2.Text = "DOUBLE-CLICK to view monthly detail for an account.";
			a = -1;
			c = -1;
			d = -1;
			grdOverview.MaxRows = 0;

		}
//******************************************************************************
//UPGRADE_WARNING: Event BudgetDisp.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void BudgetDisp_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			grdOverview.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1725);
			//grdBudgets.Height = Me.Height - 1725
			Cmd.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 900);

		}
//******************************************************************************
		private void lstAccounts_Click()
		{
			object lstAccounts = null;
			//******************************************************************************

			short i = 0;

			//UPGRADE_WARNING: Couldn't resolve default property of object lstAccounts.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstAccounts.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object lstAccounts.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstAccounts.Row = i;
			//UPGRADE_WARNING: Couldn't resolve default property of object lstAccounts.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstAccounts.Col = 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object lstAccounts.ColList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			a = Conversion.Val(lstAccounts.ColList);
			DisplayBudget();

		}
//******************************************************************************
		private void grdOverview_DblClick(System.Object eventSender, AxFPSpread._DSpreadEvents_DblClickEvent eventArgs)
		{
			//******************************************************************************

			//grdOverview.Row = Row
			grdOverview.Col = 5;
			a = Convert.ToInt32(grdOverview.Value);
			DisplayAcctBudget();

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
				c = Conversion.Val(lstCompanies.ColList);
			}
			DisplayBudget();

		}
//******************************************************************************
		private void lstDepartments_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			short i = 0;

			i = lstDepartments.ListIndex;
			lstDepartments.Row = i;
			lstDepartments.Col = 1;
			d = Conversion.Val(lstDepartments.ColList);
			DisplayBudget();

		}
//******************************************************************************
		private void DisplayBudget()
		{
			object m = null;
			object xsql = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tblB may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblA may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblA = null;
			DAO.Recordset tblB = null;
			short r = 0;
			decimal b = default(decimal);
			decimal act = default(decimal);

			if (c == -1 | d == -1)
				return;
			r = 0;
			grdOverview.MaxRows = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT Budgets.*, Accounts.Description FROM Budgets, Accounts WHERE CompanyNumber = " + c + " AND DepartmentNumber = " + d + " AND Accounts.Key = Budgets.AccountNumber ORDER BY Accounts.Description";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblB = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblB.RecordCount > 0) {
				while (!tblB.EOF) {
					r = r + 1;
					grdOverview.MaxRows = r;
					grdOverview.Row = r;
					//Set tblA = GLdb.OpenRecordset("SELECT Description FROM Accounts WHERE Key = " & tblB("AccountNumber"))
					//If tblA.RecordCount > 0 Then
					grdOverview.Col = 1;
					grdOverview.Text = tblB.Fields["Description"].Value;
					b = 0;
					act = 0;
					for (m = 1; m <= 12; m++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						b = b + tblB.Fields["Budget" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value;
						//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						act = act + tblB.Fields["Actual" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value;
					}
					grdOverview.Col = 2;
					grdOverview.Value = Convert.ToString(b);
					grdOverview.Col = 3;
					grdOverview.Value = Convert.ToString(act);
					grdOverview.Col = 4;
					grdOverview.Value = Convert.ToString(b - act);
					grdOverview.Col = 5;
					grdOverview.Value = tblB.Fields["AccountNumber"].Value;
					tblB.MoveNext();
				}
				tblB.Close();
			}
			//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			grdOverview.Refresh();

		}
//******************************************************************************
		private void DisplayAcctBudget()
		{
			object xsql = null;
			object txtCat = null;
			object x2 = null;
			object x = null;
			object x1 = null;
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			short m = 0;
			decimal b = default(decimal);
			decimal act = default(decimal);
			decimal bT = default(decimal);
			decimal aT = default(decimal);

			 // ERROR: Not supported in C#: OnErrorStatement

			grdOverview.Visible = false;
			lstCompanies.Enabled = false;
			lstDepartments.Enabled = false;
			Cmd.Text = "Return";
			if (a == -1 | c == -1 | d == -1)
				return;
			//UPGRADE_WARNING: Couldn't resolve default property of object x1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT CategoryDescription FROM Category WHERE Key = '" + x1 + "'");
			if (tbl.RecordCount > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = tbl.Fields["CategoryDescription"].Value;
				tbl.Close();
				//UPGRADE_WARNING: Couldn't resolve default property of object x2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tbl = WinglGlobal.GLdb.OpenRecordset("SELECT CategoryDescription FROM Category WHERE Key = '" + x2 + "'");
				if (tbl.RecordCount > 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + "/" + tbl.Fields["CategoryDescription"].Value;
					tbl.Close();
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object txtCat.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				txtCat.Text = x;
			}

			for (m = 1; m <= 13; m++) {
				grdBudgets.Row = m;
				grdBudgets.Col = 1;
				grdBudgets.Text = "";
				grdBudgets.Col = 2;
				grdBudgets.Text = "";
				grdBudgets.Col = 3;
				grdBudgets.Text = "";
			}
			bT = 0;
			aT = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + c + " AND DepartmentNumber = ";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + d + " AND AccountNumber = " + a;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tbl.RecordCount > 0) {
				for (m = 1; m <= 12; m++) {
					grdBudgets.Row = m;
					grdBudgets.Col = 1;
					b = tbl.Fields["Budget" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value;
					grdBudgets.Value = Convert.ToString(b);
					grdBudgets.Col = 2;
					act = tbl.Fields["Actual" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(m, "00")].Value;
					grdBudgets.Value = Convert.ToString(act);
					grdBudgets.Col = 3;
					grdBudgets.Value = Convert.ToString(b - act);
					//bT = bT + b: aT = aT + act
				}
				tbl.Close();
			}
			grdOverview.Col = 1;
			Label2.Text = grdOverview.Text;

		}
	}
}
