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
	internal class clsCompany
	{
		private clsReport rpt;
		private short pg;
		private short ln;
		private bool first;
//UPGRADE_ISSUE: Declaration type not supported: Array of fixed-length strings. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="934BD4FF-1FF9-47BD-888F-D411E47E78FA"'
			//*132
		private string[] l = new string[51];
		public void print_report()
		{
			object xcmp = null;

			string xsql = null;
			short i = 0;

			xsql = "SELECT  Department.*, Company.* FROM Department, Company WHERE Company.CompanyNumber = Department.[Company Number] ORDER BY Company.Name";
			WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblCompany.RecordCount == 0)
				return;
			pg = 1;
			heading();
			first = true;
			while (!WinglGlobal.tblCompany.EOF) {
				if (xcmp != WinglGlobal.tblCompany.Fields["CompanyNumber"].Value) {
					if (ln > rpt.PageSize - 12) {
						rpt.NewPage();
						heading();
					} else {
						if (!first) {
							for (i = 0; i <= Information.UBound(l, 1) - 1; i++) {
								if (!string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref l[i])))) {
									rpt.Send(l[i]);
									ln = ln + 1;
								}
							}
							rpt.Send(new string("=", 132));
						} else {
							first = false;
						}
					}
					for (i = 0; i <= Information.UBound(l, 1) - 1; i++) {
						l[i] = Strings.Space(132);
					}
					Strings.Mid(l[0], 6) = WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.Name"]);
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblCompany.Fields["Telephone"].Value)) {
						Strings.Mid(l[0], 57) = WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Telephone"]);
					}
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!Information.IsDBNull(WinglGlobal.tblCompany.Fields["FederalID"].Value)) {
						Strings.Mid(l[0], 78) = WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["FederalID"]);
					}
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					l[1] = Strings.Space(5) + WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["StreetAddress"]), ref 50) + " " + WinGlobal.rtpad(ref (Information.IsDBNull(WinglGlobal.tblCompany.Fields["Fax"].Value) ? "" : WinglGlobal.tblCompany.Fields["Fax"].Value), ref 20) + " " + WinGlobal.rtpad(ref (Information.IsDBNull(WinglGlobal.tblCompany.Fields["StateID"].Value) ? "" : WinglGlobal.tblCompany.Fields["StateID"].Value), ref 11);
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (!string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.City"]))) | !string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.State"]))) | !string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.Zip"]))) | !string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref (Information.IsDBNull(WinglGlobal.tblCompany.Fields["Email"].Value) ? "" : WinglGlobal.tblCompany.Fields["Email"].Value))))) {
						l[2] = Strings.Space(5) + Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.City"])) + ", " + Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.State"])) + " " + WinGlobal.noNulls(ref WinglGlobal.tblCompany.Fields["Company.Zip"]) + " " + WinglGlobal.tblCompany.Fields["Email"].Value;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object xcmp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xcmp = WinglGlobal.tblCompany.Fields["CompanyNumber"].Value;
					i = 0;
				}
				Strings.Mid(l[i], 97) = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblCompany.Fields["Department Number"].Value, "(####)"), ref 6) + " " + WinglGlobal.tblCompany.Fields["Department.Name"].Value;
				i = i + 1;
				WinglGlobal.tblCompany.MoveNext();
			}
			WinglGlobal.tblCompany.Close();
			rpt.EndDoc();

		}
		private void heading()
		{

			rpt.Send();
			rpt.Send(Strings.Space(5) + "Company List");
			rpt.Send(Strings.Space(5) + DateAndTime.Today + "     page " + pg);
			rpt.Send();
			rpt.Send();
			rpt.Send(Strings.Space(5) + "Company Name / Address                         Telephone/Fax/Email     FederalID/StateID    Departments");
			rpt.Send(Strings.Space(5) + "---------------------------------------------------------------------------------------------------------------------------------------------");
			ln = 7 + WinglGlobal.tmargin;
			pg = pg + 1;

		}
//UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Initialize_Renamed()
		{

			rpt = new clsReport();
			rpt.Target = 0;
			rpt.FontName(ref "Courier New");
			rpt.ReportName = "Company";
			rpt.rptObject = this;
			print_report();

		}
		public clsCompany() : base()
		{
			Class_Initialize_Renamed();
		}
	}
}
