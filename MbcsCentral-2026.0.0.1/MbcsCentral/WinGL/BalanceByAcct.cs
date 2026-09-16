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
	internal partial class BalanceByAcct : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.2  2011/02/10 00:29:35  mike
//* Added Active flag to Accounts to prevent display of inactive accounts in entry screens
//*
//* Revision 1.1  2010/11/09 12:29:14  mike
//* Re-import
//*
//* Revision 1.1  2009/08/21 12:36:10  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:06  mike
//* Copied from wingl folder
//*
//* Revision 1.1.1.1  2007/09/07 13:29:10  mike
//* Initial checkin
//*
//*
//*   Auto-added
//*
//* Revision 2.15  by: normal  Rev date: 12/24/2002 5:28:28 PM
//*   Added multiple years to Balances by Company and by Account
//*
//* Revision 1.0  by: normal  Rev date: 12/24/2002 8:33:34 AM
//*   Initial revision.
//*
//* $Endlog$
//* $Endlog$
		private short StartYear;
		private short cm;
		private short EndYear;
		private decimal[] totals;
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Command1.GetIndex(eventSender);

			switch (Index) {
				case 0:
					// EXIT
					this.Close();
					break;
				case 1:
					// PRINT
					this.PrintForm1.Print(this, Microsoft.VisualBasic.PowerPacks.Printing.PrintForm.PrintOption.CompatibleModeClientAreaOnly);
					break;
			}

		}
		private void BalanceByAcct_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			object y = null;

			System.DateTime StartDate = default(System.DateTime);
			System.DateTime EndDate = default(System.DateTime);

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(5685);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(10455);
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(0 + WinglGlobal.fbalc * 1000);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(0 + WinglGlobal.fbalc * 1000);

			Option1[Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0))].Checked = true;
			WinglGlobal.getAccounts(ref lstAccounts, ref Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "AcctAccess", Convert.ToString(0)), ref "", ref false);
			WinglGlobal.TransDateRange(ref 0, ref StartDate, ref EndDate);
			StartYear = DateAndTime.Year(StartDate);
			EndYear = DateAndTime.Year(EndDate);

			//grdBalance.set_Cols( , 4 + (EndYear - StartYear))
			//grdBalance.set_ColWidth(0,  , 2600)
			//grdBalance.set_ColWidth(1,  , 600)
			//grdBalance.set_ColWidth(2,  , 2600)
			//grdBalance.set_ColWidth(3,  , 1200)
			//grdBalance.set_ColWidth(4,  , 1200)
			grdBalance.Col = 0;
			grdBalance.Row = 0;
			grdBalance.Text = "Company";
			grdBalance.Col = 1;
			grdBalance.Text = "Dpt";
			grdBalance.Col = 2;
			grdBalance.Text = "Name";
			//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			y = EndYear;
			for (i = 3; i <= grdBalance.get_Cols() - 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdBalance.Col = i;
				//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(y, "0000");
				//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				y = y - 1;
			}

			cm = DateAndTime.Month(DateAndTime.Today) - 1;
			lstMonths.SelectedIndex = cm;

		}
//UPGRADE_WARNING: Event BalanceByAcct.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void BalanceByAcct_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			grdBalance.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 2045);
			grdBalance.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - 255);
			Command1[0].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 900);
			Command1[1].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 900);

		}
		private void BalanceByAcct_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			//UPGRADE_NOTE: Object fbal() may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			WinglGlobal.fbal[WinglGlobal.fbalc] = null;
			WinglGlobal.fbalc = WinglGlobal.fbalc - 1;

		}
		private void lstAccounts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object tpbal = null;
			object tcbal = null;
			object y = null;
			object xsql = null;
			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			short j = 0;
			decimal c = default(decimal);
			decimal p = default(decimal);
			DAO.Recordset tbl = null;
			//UPGRADE_WARNING: Arrays in structure tbldpt may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbldpt = null;
			string xcmpname = null;
			decimal cbal = default(decimal);
			decimal pbal = default(decimal);

			if (lstAccounts.ListIndex < 0 | lstMonths.SelectedIndex < 0)
				goto lstAccounts_Click_exit;
			grdBalance.Rows = 2;
			i = lstAccounts.ListIndex;
			lstAccounts.Row = i;
			lstAccounts.Col = 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT Department.Name, Department.[Department Number], Company.Name, Company.CompanyNumber FROM Department, Company ";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + "WHERE Company.CompanyNumber = Department.[Company Number]";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = xsql + " ORDER BY Company.Name, Department.[Department Number]";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tbl.RecordCount > 0) {
				tbl.MoveLast();
				grdBalance.Rows = tbl.RecordCount + 2;
				grdBalance.Row = grdBalance.Rows - 1;
				grdBalance.Col = 0;
				grdBalance.Text = "TOTALS=================>";
				tbl.MoveFirst();
				i = 0;
				totals = null;
				totals = new decimal[grdBalance.get_Cols() - 2];
				while (!tbl.EOF) {
					i = i + 1;
					grdBalance.Row = i;
					grdBalance.Col = 0;
					if (tbl.Fields["Company.Name"].Value != xcmpname) {
						grdBalance.Text = tbl.Fields["Company.Name"].Value;
						xcmpname = tbl.Fields["Company.Name"].Value;
					} else {
						grdBalance.Text = "";
					}
					grdBalance.CellAlignment = MSHierarchicalFlexGridLib.AlignmentSettings.flexAlignLeftCenter;
					grdBalance.Col = 1;
					grdBalance.Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["Department Number"].Value, "0000"), ref 4);
					grdBalance.Col = 2;
					grdBalance.Text = tbl.Fields["Department.Name"].Value;
					grdBalance.CellAlignment = MSHierarchicalFlexGridLib.AlignmentSettings.flexAlignLeftCenter;
					//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					y = EndYear;
					for (j = 3; j <= grdBalance.get_Cols() - 1; j++) {
						grdBalance.Col = j;
						//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						cbal = WinglGlobal.AcctBal(ref tbl.Fields["CompanyNumber"].Value, ref Convert.ToInt32(lstAccounts.ColList), ref DateAndTime.DateSerial(y, 1, 1), ref DateAndTime.DateSerial(y, cm + 2, 1 - 1), ref tbl.Fields["Department Number"].Value);
						grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cbal, "###,###,##0.00");
						totals[j - 3] = totals[j - 3] + cbal;
						//UPGRADE_WARNING: Couldn't resolve default property of object y. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						y = y - 1;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object tcbal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tcbal = tcbal + cbal;
					//UPGRADE_WARNING: Couldn't resolve default property of object tpbal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tpbal = tpbal + pbal;
					tbl.MoveNext();
				}
				grdBalance.Row = grdBalance.Rows - 1;
				for (i = 3; i <= grdBalance.get_Cols() - 1; i++) {
					grdBalance.Col = i;
					grdBalance.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(totals[i - 3], "###,###,##0.00");
				}
			}
			tbl.Close();
			lstAccounts_Click_exit:


		}
//UPGRADE_WARNING: Event lstMonths.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstMonths_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstMonths.SelectedIndex > -1)
				cm = lstMonths.SelectedIndex;
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
