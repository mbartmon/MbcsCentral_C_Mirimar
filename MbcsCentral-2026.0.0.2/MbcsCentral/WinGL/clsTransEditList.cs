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
	internal class clsTransEditList
	{
//UPGRADE_WARNING: Arrays in structure tblTransTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblTransTemp;
		private short pg;
		private short ln;
//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		private DAO.Recordset tblacct;
		private clsReport rpt;
		public void print_report()
		{
			object i = null;

			string a = null;
			string desc = null;
			string desc2 = null;
			string s = null;
			decimal total = default(decimal);
			short cnt = 0;
			//UPGRADE_WARNING: Arrays in structure tblctl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblctl = null;
			string xsql = null;

			tblctl = WinglGlobal.GLdb.OpenRecordset("Control");

			xsql = "SELECT * FROM TransactionTemp";
			if (!string.IsNullOrEmpty(Strings.Trim(tblctl.Fields["EdListSort1_fld"].Value))) {
				xsql = xsql + " ORDER BY ";
				for (i = 1; i <= 6; i++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (!string.IsNullOrEmpty(Strings.Trim(tblctl.Fields["EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_fld"].Value))) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (i > 1) {
							xsql = xsql + ", ";
						}
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + tblctl.Fields["EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_fld"].Value + " " + tblctl.Fields["EdListSort" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#") + "_dir"].Value;
					}
				}
			}
			tblTransTemp = WinglGlobal.GLdb.OpenRecordset(xsql);
			//Set tblTransTemp = GLdb.OpenRecordset("SELECT * FROM TransactionTemp ORDER BY Source, Description, Amount DESC")
			//Set tblTransTemp = GLdb.OpenRecordset("SELECT * FROM TransactionTemp ORDER BY CompanyNumber, DepartmentNumber, CheckNumber, Amount DESC")

			 // ERROR: Not supported in C#: OnErrorStatement

			tblTransTemp.MoveFirst();
			if (Err().Number != 0) {
				Interaction.MsgBox("No Transactions to post.", , "Transaction Edit List");
				//UPGRADE_NOTE: Object rpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				rpt = null;
			} else {
				tblTransTemp.MoveFirst();
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 10);
				pg = 0;
				heading();
				while (!tblTransTemp.EOF) {
					if (ln >= rpt.PageSize - 12) {
						rpt.NewPage();
						heading();
					}
					tblacct = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Accounts WHERE Key = " + tblTransTemp.Fields["AccountNumber"].Value);
					a = "";
					if (tblacct.RecordCount > 0) {
						a = Strings.Left(Strings.Trim(tblacct.Fields["Description"].Value) + " (" + tblacct.Fields["OldAccountNumber"].Value + ")", 43);
					} else {
						a = "*NoAcct*";
					}
					desc2 = "";
					desc = Strings.Trim(tblTransTemp.Fields["Description"].Value);
					if (Strings.Len(desc) > 30) {
						for (i = 31; i >= 1; i += -1) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							if (Strings.Mid(desc, i, 1) == " ") {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								desc2 = Strings.Mid(desc, i + 1);
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								desc = Strings.Left(desc, i - 1);
								break; // TODO: might not be correct. Was : Exit For
							}
						}
					}
					if (rpt.Target == 0) {
						s = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["IndexNumber"].Value, "####### "), ref 8) + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["DepartmentNumber"].Value, "0000 ");
					} else {
						s = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["DepartmentNumber"].Value, "0000 ");
					}
					s = s + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["Date"].Value, "mm/dd/yy ") + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["CheckNumber"].Value, "###### "), ref 7);
					if (rpt.Target == 0) {
						s = s + Strings.Space(23) + WinGlobal.rtpad(ref desc, ref 31);
					} else {
						s = s + Strings.Space(28) + WinGlobal.rtpad(ref desc, ref 31);
					}
					s = s + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblTransTemp.Fields["Amount"].Value, "$###,###,##0.00"), ref 16) + " " + WinGlobal.rtpad(ref WinGlobal.noNulls(ref tblTransTemp.Fields["Journal"]), ref 20);

					rpt.Send(s);
					if (rpt.Target == 0) {
						rpt.Send(Strings.Space(8) + WinGlobal.rtpad(ref a, ref 44) + WinGlobal.rtpad(ref desc2, ref 48) + WinGlobal.rtpad(ref tblTransTemp.Fields["Source"].Value, ref 20));
					} else {
						rpt.Send(Strings.Space(5) + WinGlobal.rtpad(ref a, ref 44) + WinGlobal.rtpad(ref desc2, ref 48) + WinGlobal.rtpad(ref tblTransTemp.Fields["Source"].Value, ref 20));
					}
					ln = ln + 2;
					total = total + tblTransTemp.Fields["Amount"].Value;
					cnt = cnt + 1;
					tblTransTemp.MoveNext();
				}
				if (rpt.Target == 0) {
					rpt.Send(Strings.Space(84) + "===============");
				} else {
					rpt.Send(Strings.Space(81) + "===============");
				}
				if (rpt.Target == 0) {
					rpt.Send(Strings.Space(4) + "Transactions= " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cnt, "#,###"), ref 5) + Strings.Space(54) + "TOTAL= " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "$###,###,##0.00"), ref 15));
				} else {
					rpt.Send(Strings.Space(4) + "Transactions= " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cnt, "#,###"), ref 5) + Strings.Space(51) + "TOTAL= " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "$###,###,##0.00"), ref 15));
				}
				rpt.EndDoc();
				 // ERROR: Not supported in C#: OnErrorStatement

				tblTransTemp.Close();
				tblacct.Close();
			}

		}
		public void heading()
		{

			pg = pg + 1;
			rpt.Send("TRANSACTION EDIT LIST");
			rpt.Send(DateAndTime.Today + " page " + pg);
			rpt.Send();
			if (rpt.Target == 0) {
				rpt.Send(" Index# Dept   Date   Check#                        Description                              Amount Journal/Source");
			} else {
				rpt.Send("Dept   Date   Check#                             Description                              Amount Journal/Source");
			}
			rpt.Send();
			ln = 5 + WinglGlobal.tmargin;

		}
//UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Initialize_Renamed()
		{

			rpt = new clsReport();
			rpt.Target = 0;
			rpt.rptObject = this;
			rpt.ReportName = "TransEditList";
			rpt.ActivateList = true;
			WinglGlobal.rptCancel = true;
			print_report();

		}
		public clsTransEditList() : base()
		{
			Class_Initialize_Renamed();
		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object r = null;

			if (pg > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				r = Interaction.MsgBox("Do you want to post these transactions to the G/L.", MsgBoxStyle.YesNo, "Post G/L Transactions");
				switch (r) {
					case MsgBoxResult.Yes:
						WinglGlobal.PostTransactions();
						break;
					case MsgBoxResult.No:
						return;

						break;
				}
			}

			//UPGRADE_NOTE: Object rpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rpt = null;

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
	}
}
