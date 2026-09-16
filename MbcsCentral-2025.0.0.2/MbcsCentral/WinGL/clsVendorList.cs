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
	internal class clsVendorList
	{
		private clsReport rpt;
		private short pg;
		private short ln;
//UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Initialize_Renamed()
		{

			rpt = new clsReport();
			rpt.Target = 0;
			rpt.ReportName = "VendorList";
			rpt.rptObject = this;
			print_report();

		}
		public clsVendorList() : base()
		{
			Class_Initialize_Renamed();
		}
		public void print_report()
		{
			object x = null;
			object l = null;

			short i = 0;
			string xsql = null;

			rpt.FontName(ref "Courier New");
			pg = 1;
			heading();
			//UPGRADE_WARNING: Couldn't resolve default property of object l. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			l = 1;
			WinglGlobal.tblVendors = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Vendors ORDER BY VendorName");
			while (!WinglGlobal.tblVendors.EOF) {
				if (ln > rpt.PageSize - 15) {
					rpt.NewPage();
					heading();
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["VendorName"]), ref 51);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(WinglGlobal.tblVendors.Fields["Telephone"].Value)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + Strings.Space(21);
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Telephone"]), ref 21);
				}
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(WinglGlobal.tblVendors.Fields["FederalID"].Value)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + Strings.Space(23);
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = x + WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["FederalID"]), ref 23);
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = "";
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (WinglGlobal.tblVendors.Fields["Payroll"].Value)
					x = "P";
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (WinglGlobal.tblVendors.Fields["Corporation"].Value)
					x = x + "C";
				rpt.Send("tab(6)", WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["VendorName"]) + (WinglGlobal.tblVendors.Fields["Inactive"].Value ? " *<INACTIVE>*" : ""), "tab(57)", WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Telephone"]), ";");
				rpt.Send("tab(78)", WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["FederalID"]), "tab(102)", x);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Name2"])) & !string.IsNullOrEmpty(Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Name2"])))) {
					rpt.Send("tab(6)", WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Name2"]), ref 50));
					ln = ln + 1;
				}
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				rpt.Send("tab(6)", WinGlobal.rtpad(ref WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Street"]), ref 50) + " " + WinGlobal.rtpad(ref (Information.IsDBNull(WinglGlobal.tblVendors.Fields["Fax"].Value) ? "" : WinglGlobal.tblVendors.Fields["Fax"].Value), ref 20) + " " + WinGlobal.rtpad(ref (Information.IsDBNull(WinglGlobal.tblVendors.Fields["StateID"].Value) ? "" : WinglGlobal.tblVendors.Fields["StateID"].Value), ref 11));
				rpt.Send("tab(6)", Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["City"])) + ", " + Strings.Trim(WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["State"])) + " " + WinGlobal.noNulls(ref WinglGlobal.tblVendors.Fields["Zip"]) + " " + WinglGlobal.tblVendors.Fields["Email"].Value);
				rpt.Send("tab(6)", "---------------------------------------------------------------------------------------------------");
				//rpt.Send
				ln = ln + 5;
				WinglGlobal.tblVendors.MoveNext();
			}
			WinglGlobal.tblVendors.Close();
			rpt.EndDoc();

		}
		private void heading()
		{

			rpt.Send();
			rpt.Send(Strings.Space(5) + "Vendor List");
			rpt.Send(Strings.Space(5) + DateAndTime.Today + "     page " + pg);
			rpt.Send();
			rpt.Send();
			rpt.Send(Strings.Space(5) + "Vendor Name / Address                         Telephone/Fax/Email     FederalID/StateID       Type");
			rpt.Send(Strings.Space(5) + "---------------------------------------------------------------------------------------------------");
			ln = 7 + WinglGlobal.tmargin;
			pg = pg + 1;

		}
	}
}
