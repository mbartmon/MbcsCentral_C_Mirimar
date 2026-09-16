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
	internal class clsJournals
	{
		private Report rpt;
		private object[] lo;
		private short pg;
		private short ln;
		private bool[] cs;
		private short Columns;
		private short mr;
        private JournalReportsTable tblTrans;

		private enum LayoutFields
		{
			lofield = 0,
			lorange = 1,
			lotype = 2,
			loheading = 3,
			loprintflag = 4,
			losize = 5,
			loused = 6
		}
		private enum FieldNames
		{
			fndate = 0,
			fnaccount = 1,
			fnamount = 2,
			fndescription = 3,
			fncheck = 4,
			fnsource = 5,
			fndepartment = 6
		}
//local variable(s) to hold property value(s)
		private bool mvarCancel;
		private string mvarJournalName;
		private string mvarReportName;
		private System.DateTime mvarStartDate;
		private System.DateTime mvarEndDate;
		private string mvarCompany;
		private string mvarCompanyName;
		private string mvarDepartment;
		private string mvarDepartmentName;
		private bool mvarPrintTotals;
		private bool mvarConsolidate;
		private bool mvarSourceSearch;
		private string mvarLayout;

		public string layout {
			get { return mvarLayout; }
			set { mvarLayout = value; }
		}
		public bool SourceSearch {
			get { return mvarSourceSearch; }
			set { mvarSourceSearch = value; }
		}
		public bool Consolidate {
			get { return mvarConsolidate; }
			set { mvarConsolidate = value; }
		}
		public bool PrintTotals {
			get { return mvarPrintTotals; }
			set { mvarPrintTotals = value; }
		}
		public string DepartmentName {
			get { return mvarDepartmentName; }
			set { mvarDepartmentName = value; }
		}
		public string Department {
			get { return mvarDepartment; }
			set { mvarDepartment = value; }
		}
		public string CompanyName {
			get { return mvarCompanyName; }
			set { mvarCompanyName = value; }
		}
		public string Company {
			get { return mvarCompany; }
			set { mvarCompany = value; }
		}
		public System.DateTime EndDate {
			get { return mvarEndDate; }
			set { mvarEndDate = value; }
		}
		public System.DateTime StartDate {
			get { return mvarStartDate; }
			set { mvarStartDate = value; }
		}
		public string ReportName {
			get { return mvarReportName; }
			set { mvarReportName = value; }
		}
		public string JournalName {
			get { return mvarJournalName; }
			set { mvarJournalName = value; }
		}
		public bool Cancel {
			get { return mvarCancel; }
			set { mvarCancel = value; }
		}
		public void print_report()
		{
			object s = null;
			object c = null;
			object x = null;
			object Consolidated = null;

			string xsql = null;
			DAO.Recordset tbl = null;
			decimal[] total = new decimal[10];
			bool first = false;
			AccountRanges tblrange = null;
			bool a = false;
			System.DateTime xdate = default(System.DateTime);
			string xsrc = null;
			int xdpt = 0;
			bool jPrint = false;
			bool jAnyRange = false;
			object jRange = null;


			setup_layout();
			System.Array.Clear(total, 0, total.Length);
			first = true;

			tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblTrans.RecordCount == 0)
				return;
			pg = 0;
			heading();
			while (!tblTrans.EOF) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = Strings.Space(4);

				jPrint = false;
				jAnyRange = false;
				for (c = 0; c <= Columns - 1; c++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, loused). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(c, LayoutFields.loused) Then
					// Determine if this transaction falls within any defined ranges
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(c, LayoutFields.lorange) And Not jAnyRange Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//xsql = "SELECT * FROM AccountRanges WHERE ReportName = 'Journals:" & Trim(layout) & "' AND ReportSection = " & c
					//tblrange = GLdb.OpenRecordset(xsql)
					//If tblrange.RecordCount > 0 Then
					while (!tblrange.EOF) {
						//If tblrange.Fields("AccountNumber").Value = tblTrans.Fields("AccountNumber").Value Then
						//jAnyRange = True
						break; // TODO: might not be correct. Was : Exit Do
						//End If
						tblrange.MoveNext();
					}
					tblrange.Close();
					//End If
					//End If

					// Process Debit/Credit type amount fields to determine if should be printed
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If Not lo(c, LayoutFields.loprintflag) And Not jPrint Then jPrint = True
					if ((mr == 0 | jAnyRange) & !jPrint) {
						//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//If lo(c, LayoutFields.lofield) = FieldNames.fnamount Then
						//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//Select Case lo(c, LayoutFields.lotype)
						//Case 0 ' no type
						// jPrint = True
						//Case 1 ' Credit
						//If tblTrans.Fields(FieldNames.fnamount).Value < 0 Then jPrint = True
						//Case 2 ' Debit
						//If tblTrans.Fields(FieldNames.fnamount).Value >= 0 Then jPrint = True
						//End Select
						//End If
						//End If
					}
				}
				if (!jPrint)
					goto donotprint2;

				for (c = 0; c <= Columns - 1; c++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, loused). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(c, LayoutFields.loused) Then

					// Determine if within range
					//UPGRADE_WARNING: Couldn't resolve default property of object jRange. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					jRange = true;
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, lorange). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(c, LayoutFields.lorange) Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM AccountRanges WHERE ReportName = 'Journals:" + Strings.Trim(layout) + "' AND ReportSection = " + c;
					tblrange = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tblrange.RecordCount > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object jRange. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						jRange = false;
						while (!tblrange.EOF) {
							if (tblrange.Fields["AccountNumber"].Value == tblTrans.Fields["AccountNumber"].Value) {
								//UPGRADE_WARNING: Couldn't resolve default property of object jRange. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								jRange = true;
								break; // TODO: might not be correct. Was : Exit Do
							}
							tblrange.MoveNext();
						}
						tblrange.Close();
					}
					//End If

					if (!jRange) {
						//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//x = x & Space(lo(c, LayoutFields.losize) + 1)
						goto donotprint;
					}
					donotprint:

					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If Not lo(c, LayoutFields.loprintflag) And jAnyRange Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & Space(lo(c, LayoutFields.losize) + 1)
					//GoTo donotprint
					//End If

					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//Select Case lo(c, LayoutFields.lofield)
					//   Case FieldNames.fndate 'DATE
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & rtpad(VB6.Format(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value, "mm/dd/yy"), CShort(lo(c, LayoutFields.losize)))
					//   Case FieldNames.fnamount 'AMOUNT
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, losize). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					// s = lo(c, LayoutFields.losize)
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, lotype). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(c, LayoutFields.lotype) = 1 And tblTrans.Fields(lo(c, LayoutFields.lofield)).Value < 0 Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & lpad(VB6.Format(System.Math.Abs(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value), New String("#", s - 3) & ".00"), CShort(lo(c, LayoutFields.losize)))
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//total(c) = total(c) + System.Math.Abs(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value)
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, lotype). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//ElseIf lo(c, LayoutFields.lotype) = 2 And tblTrans.Fields(lo(c, LayoutFields.lofield)).Value >= 0 Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & lpad(VB6.Format(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value, New String("#", s - 3) & ".00"), CShort(lo(c, LayoutFields.losize)))
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//total(c) = total(c) + tblTrans.Fields(lo(c, LayoutFields.lofield)).Value
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, lotype). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//ElseIf lo(c, LayoutFields.lotype) = 0 Then
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & lpad(VB6.Format(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value, New String("#", s - 3) & ".00"), CShort(lo(c, LayoutFields.losize)))
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//total(c) = total(c) + tblTrans.Fields(lo(c, LayoutFields.lofield)).Value
					//Else
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & Space(lo(c, LayoutFields.losize) + 1)
					//End If
					//   Case Else
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, losize). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & rtpad(Left(tblTrans.Fields(lo(c, LayoutFields.lofield)).Value, lo(c, LayoutFields.losize)), CShort(lo(c, LayoutFields.losize)))
					//End Select
					//End If
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (!string.IsNullOrEmpty(Strings.Trim(x))) {
					if (first) {
						xdate = tblTrans.Fields[FieldNames.fndate].Value;
						xsrc = tblTrans.Fields[FieldNames.fnsource].Value;
						xdpt = tblTrans.Fields["DepartmentNumber"].Value;
						first = false;
					}
					if (!Consolidated & xdpt != tblTrans.Fields["DepartmentNumber"].Value) {
						//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
						//GoSub print_totals
						if (PrintTotals) {
							rpt.NewPage();
							xdpt = tblTrans.Fields["DeparmentNumber"].Value;
							xdate = tblTrans.Fields[FieldNames.fndate].Value;
							xsrc = tblTrans.Fields[FieldNames.fnsource].Value;
							heading();
						}
						if (xdate != tblTrans.Fields[FieldNames.fndate].Value) {
							rpt.Send();
							ln = ln + 1;
							xsrc = tblTrans.Fields[FieldNames.fnsource].Value;
							xdate = tblTrans.Fields[FieldNames.fndate].Value;
						}
						if (SourceSearch & xsrc != tblTrans.Fields[5].Value) {
							rpt.Send();
							ln = ln + 1;
							xsrc = tblTrans.Fields[FieldNames.fnsource].Value;
							xdate = tblTrans.Fields[FieldNames.fndate].Value;
						}
						if (ln > rpt.PageSize - 8) {
							rpt.NewPage();
							heading();
						}
						rpt.Send(x);
						ln = ln + 1;
					}
				}
				donotprint2:
				tblTrans.MoveNext();
			}
			//UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
			//If PrintTotals Then 'GoSub print_totals
			rpt.EndDoc();
			tblTrans.Close();
			goto print_Report_exit;
			print_report_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.ReportName);
			 // ERROR: Not supported in C#: ResumeStatement

			print_totals:

			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(4);
			for (c = 0; c <= Columns - 1; c++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (cs[c]) {
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & " " & New String("=", lo(c, LayoutFields.losize))
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//x = x & Space(lo(c, LayoutFields.losize) + 1)
				}
			}
			rpt.Send(x);
			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			x = Strings.Space(4);
			for (c = 0; c <= Columns - 1; c++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, lofield). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If lo(c, LayoutFields.loused) And lo(c, LayoutFields.lofield) = 2 Then
				//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(c, losize). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//s = lo(c, LayoutFields.losize)
				//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object s. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//x = x & " " & lpad(VB6.Format(total(c), New String("#", s - 3) & ".00"), CShort(lo(c, LayoutFields.losize)))
				//Else
				//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//x = x & " " & Space(lo(c, LayoutFields.losize))
				//End If
			}
			rpt.Send(x);
			//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			System.Array.Clear(total, 0, total.Length);
			//UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			return;
			print_Report_exit:

			return;

		}
		private void heading()
		{
			object Consolidated = null;

			short i = 0;
			short l = 0;
			string x = null;
			pg = pg + 1;
			ln = WinglGlobal.tmargin;
			rpt.Send(Strings.Space(5) + CompanyName);
			rpt.Send(Strings.Space(5) + ReportName);
			if (!Consolidated) {
				rpt.Send(Strings.Space(5) + tblTrans.Fields[FieldNames.fndepartment].Value);
				ln = ln + 1;
			}
			rpt.Send(Strings.Space(5) + StartDate + " to " + EndDate + "            page " + pg);
			rpt.Send();
			x = Strings.Space(5);
			l = 0;
			for (i = 0; i <= Columns - 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(i, loused). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If lo(i, LayoutFields.loused) Then
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(i, lofield). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//If lo(i, LayoutFields.lofield) = 2 Then
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//x = x & lpad(Left(CStr(lo(i, LayoutFields.loheading)), CShort(lo(i, LayoutFields.losize))), CShort(lo(i, LayoutFields.losize))) & " "
				//cs(i) = True
				//Else
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//x = x & rtpad(Left(CStr(lo(i, LayoutFields.loheading)), CShort(lo(i, LayoutFields.losize))), CShort(lo(i, LayoutFields.losize) + 1))
				//cs(i) = False
				//End If
				//UPGRADE_WARNING: Couldn't resolve default property of object lo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//l = l + lo(i, LayoutFields.losize) + 1
				//End If
			}
			rpt.Send(x);
			rpt.Send(Strings.Space(5) + new string("=", l));
			ln = ln + 6;

		}
		private void setup_layout()
		{
			object i = null;

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			lo = null;
			mr = 0;
			tbl = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Journals WHERE Layout = '" + layout + "'");
			if (tbl.RecordCount > 0) {
				tbl.MoveLast();
				Columns = tbl.RecordCount;
				//ReDim lo(Columns - 1, tbl.Fields.Count - 1)
				cs = new bool[Columns];
				tbl.MoveFirst();
				while (!tbl.EOF) {
					for (i = 2; i <= tbl.Fields.Count - 1; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						//UPGRADE_WARNING: Couldn't resolve default property of object lo(tbl(), i - 2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//lo(tbl.Fields("Column").Value, i - 2) = IIf(i = 4, IIf(IsDbNull(tbl.Fields(i).Value), 0, tbl.Fields(i).Value), IIf(IsDbNull(tbl.Fields(i).Value), "", tbl.Fields(i).Value))
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(tbl(), loused). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//lo(tbl.Fields("Column").Value, LayoutFields.loused) = True
					//UPGRADE_WARNING: Couldn't resolve default property of object lo(tbl(Column), lorange). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//If lo(tbl.Fields("Column").Value, LayoutFields.lorange) Then
					//mr = mr + 1
					//End If
					tbl.MoveNext();
				}
				tbl.Close();
			} else {
				Interaction.MsgBox("Layout not found.", , this.ReportName);
			}

		}
		public void Journals(ref bool Batch)
		{

			if (Batch) {
				rpt = new clsReport();
				rpt.Batch = true;
				rpt.Target = 1;
				rpt.ReportName = "Journals";
				rpt.rptObject = this;
				rpt.FontName(ref "Courier New");
				rpt.FontSize(ref 12);
				print_report();
				WinglGlobal.rptCancel = true;
			}

			if (!WinglGlobal.rptCancel) {
				frm = new frmJournals();
				frm.clas = this;
				 // ERROR: Not supported in C#: OnErrorStatement

				frm.ShowDialog();
				//UPGRADE_NOTE: Object frm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				frm = null;
				if (!Cancel) {
					rpt = new clsReport();
					rpt.Target = 0;
					rpt.ReportName = "Journals";
					rpt.rptObject = this;
					rpt.FontName(ref "Courier New");
					rpt.FontSize(ref 12);
					print_report();
				}
			}

		}
		public void Serialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[0] = "JR";
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[1] = JournalName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[2] = ReportName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[3] = StartDate;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[4] = EndDate;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[5] = Company;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[6] = CompanyName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[7] = Department;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[8] = DepartmentName;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[9] = PrintTotals;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(10). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[10] = Consolidate;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(11). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[11] = SourceSearch;
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(12). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.BatchRptSave[12] = layout;

		}
		public void Unserialize()
		{

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			JournalName = WinglGlobal.BatchRptSave[1];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ReportName = WinglGlobal.BatchRptSave[2];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			StartDate = WinglGlobal.BatchRptSave[3];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(4). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			EndDate = WinglGlobal.BatchRptSave[4];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(5). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Company = WinglGlobal.BatchRptSave[5];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(6). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			CompanyName = WinglGlobal.BatchRptSave[6];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(7). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Department = WinglGlobal.BatchRptSave[7];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(8). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			DepartmentName = WinglGlobal.BatchRptSave[8];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(9). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			PrintTotals = WinglGlobal.BatchRptSave[9];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(10). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Consolidate = WinglGlobal.BatchRptSave[10];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(11). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SourceSearch = WinglGlobal.BatchRptSave[11];
			//UPGRADE_WARNING: Couldn't resolve default property of object BatchRptSave(12). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			layout = WinglGlobal.BatchRptSave[12];

		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			object xrpt = null;

			if (!WinglGlobal.rptCancel) {
				xrpt = new clsJournals();
				//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.Journals. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xrpt.Journals(false);
				//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				xrpt = null;
			}

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
		public void setDates()
		{

			StartDate = WinglGlobal.bStartDate;
			EndDate = WinglGlobal.bEndDate;

		}
	}
}
