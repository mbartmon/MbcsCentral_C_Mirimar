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
	internal class clsAccountMaster
	{
		private clsReport rpt;
		private short l;
		private short pg;
		private short ln;
//UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Initialize_Renamed()
		{

			rpt = new clsReport();
			rpt.Target = 0;
			rpt.ReportName = "AccountMaster";
			rpt.rptObject = this;
			print_report();

		}
		public clsAccountMaster() : base()
		{
			Class_Initialize_Renamed();
		}
		public void print_report()
		{

			short i = 0;
			string xsql = null;

			rpt.FontName(ref "Courier New");
			pg = 1;
			heading();
			l = 1;
			xsql = "SELECT * FROM Category WHERE SubOf = '0_'";
			WinglGlobal.tblCategory = WinglGlobal.GLdb.OpenRecordset(xsql);
			while (!WinglGlobal.tblCategory.EOF) {
				if (ln > rpt.PageSize - 22) {
					rpt.NewPage();
					heading();
				}
				if (rpt.Target == 1) {
					rpt.FontBold(ref true);
					rpt.Send(Strings.Space(5) + WinglGlobal.tblCategory.Fields["CategoryDescription"].Value);
					rpt.Send();
					rpt.FontBold(ref false);
					ln = ln + 2;
				} else {
					rpt.Send(WinglGlobal.tblCategory.Fields["CategoryDescription"]);
				}
				get_Accounts(ref WinglGlobal.tblCategory.Fields["Key"].Value);
				get_subCategories(ref WinglGlobal.tblCategory.Fields["Key"].Value);
				WinglGlobal.tblCategory.MoveNext();
			}
			WinglGlobal.tblCategory.Close();
			rpt.EndDoc();

		}
		private void heading()
		{

			if (rpt.Target == 1) {
				rpt.FontSize(ref 14);
				rpt.FontBold(ref true);
				rpt.Send();
				rpt.Send(Strings.Space(5) + "Account Master List");
				rpt.FontSize(ref 12);
				rpt.FontBold(ref false);
				rpt.Send(Strings.Space(6) + DateAndTime.Today + "     page " + pg);
				rpt.Send();
				rpt.Send();
				ln = 5 + WinglGlobal.tmargin;
				pg = pg + 1;
			}

		}
		private void get_Accounts(ref string k)
		{
			object dest = null;
			object xsql = null;

			//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblacct = null;
			string s = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Accounts WHERE MinorCategory = '" + k + "' AND active = true ORDER BY [Account Number]";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblacct = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblacct.RecordCount > 0) {
				while (!tblacct.EOF) {
					//s = tblAcct("OldAccountNumber") & "-" & tblAcct("OldSubNumber")
					if (rpt.Target == 1) {
						if (ln > rpt.PageSize - 16) {
							rpt.NewPage();
							heading();
						}
						rpt.Send(Strings.Space(l * 6 + 6) + Strings.Trim(tblacct.Fields["Description"].Value) + " (" + tblacct.Fields["MajorCategory"].Value + tblacct.Fields["MinorCategory"].Value + tblacct.Fields["Account Number"].Value + ")");
						ln = ln + 1;
					} else {
						rpt.Send(Strings.Space(l * 4 + 4) + Strings.Trim(tblacct.Fields["Description"].Value) + " (" + tblacct.Fields["MajorCategory"].Value + tblacct.Fields["MinorCategory"].Value + tblacct.Fields["Account Number"].Value + ")");
					}
					tblacct.MoveNext();
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object dest. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (dest) {
					rpt.Send();
					ln = ln + 1;
				} else {
					rpt.Send(" ");
				}
			}
			tblacct.Close();

		}
		private void get_subCategories(ref string k)
		{
			object xsql = null;

			//UPGRADE_WARNING: Arrays in structure tblcat may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblcat = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Category WHERE SubOf = '" + k + "'";
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblcat = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblcat.RecordCount > 0) {
				while (!tblcat.EOF) {
					if (rpt.Target) {
						if (ln > rpt.PageSize - 20) {
							rpt.NewPage();
							heading();
						}
						rpt.FontUnderline(ref true);
						rpt.Send(Strings.Space(l * 6 + 6) + tblcat.Fields["CategoryDescription"].Value);
						rpt.Send();
						rpt.FontUnderline(ref false);
						ln = ln + 2;
					} else {
						rpt.Send(Strings.Space(l * 4 + 4) + tblcat.Fields["CategoryDescription"].Value);
						rpt.Send(Strings.Space(l * 4 + 4) + "--------------------------------");
					}
					get_Accounts(ref tblcat.Fields["Key"].Value);
					l = l + 1;
					get_subCategories(ref tblcat.Fields["Key"].Value);
					l = l - 1;
					tblcat.MoveNext();
				}
			}
			tblcat.Close();

		}
	}
}
