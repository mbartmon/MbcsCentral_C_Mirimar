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
	internal partial class AcctSelection : System.Windows.Forms.Form
	{
		string fName;
		string rptName;
		int rptSection;
		DAO.TableDef defTable;
		DAO.Field fldField;
		DAO.Index idxIndex;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			short r = 0;

			switch (Index) {

				case 0:
					// SAVE
					this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.GLdb.Execute(("DELETE * FROM AccountRanges WHERE ReportName = '" + Strings.Trim(fName) + ":" + Strings.Trim(rptName) + "' AND ReportSection = " + rptSection));
					 // ERROR: Not supported in C#: OnErrorStatement

					tbl = WinglGlobal.GLdb.OpenRecordset("AccountRanges");
					fill_in_table:
					for (r = 0; r <= grdAccounts.Rows - 1; r++) {
						grdAccounts.TargetCell(1, r);
						if (grdAccounts.Value == true) {
							tbl.AddNew();
							tbl.Fields[0].Value = Strings.Trim(fName) + ":" + Strings.Trim(rptName);
							tbl.Fields[1].Value = rptSection;
							grdAccounts.TargetCell(2, r);
							//UPGRADE_WARNING: Couldn't resolve default property of object grdAccounts.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tbl.Fields[2].Value = grdAccounts.Value;
							tbl.Update();
						}
					}

					tbl.Close();
					this.Cursor = System.Windows.Forms.Cursors.Default;
					this.Close();
					break;

				case 1:
					// CANCEL
					this.Close();
					break;

				case 2:
					// SELECT ALL
					for (r = 0; r <= grdAccounts.Rows - 1; r++) {
						grdAccounts.TargetCell(1, r);
						grdAccounts.Value = true;
					}

					break;

				case 3:
					// CLEAR ALL
					for (r = 0; r <= grdAccounts.Rows - 1; r++) {
						grdAccounts.TargetCell(1, r);
						grdAccounts.Value = false;
					}

					break;


			}
			goto Cmd_Click_exit;
			no_report_table:

			if (Err().Number == 3078) {
				// create table
				defTable = WinglGlobal.GLdb.CreateTableDef("AccountRanges");
				fldField = defTable.CreateField("ReportName", DAO.DataTypeEnum.dbText, 32);
				defTable.Fields.Append(fldField);
				fldField = defTable.CreateField("ReportSection", DAO.DataTypeEnum.dbLong, 4);
				defTable.Fields.Append(fldField);
				WinglGlobal.GLdb.TableDefs.Append(defTable);
				fldField = defTable.CreateField("AccountNumber", DAO.DataTypeEnum.dbLong, 4);
				defTable.Fields.Append(fldField);
				tbl = WinglGlobal.GLdb.OpenRecordset("AccountRanges");
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
				 // ERROR: Not supported in C#: ResumeStatement

			}
			Cmd_Click_exit:


		}
		private void AcctSelection_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2));
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2));

		}
		public void AccountSelection(ref string tblName, ref string rptNam, ref int rptSect, ref string c1 = "", ref string c2 = "")
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tblacct may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblacct = null;
			DAO.Recordset tbl = null;
			string xsql = null;
			//UPGRADE_WARNING: Arrays in structure tblcat may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short r = 0;
			string xcat = null;
			DAO.Recordset tblcat = null;

			fName = tblName;
			rptName = rptNam;
			rptSection = rptSect;

			this.Text = this.Text + ": " + rptName;

			xsql = "SELECT MajorCategory, Description, Key FROM Accounts";

			//UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
			if ((c1 != null) & !string.IsNullOrEmpty(Strings.Trim(c1))) {
				xsql = xsql + " WHERE MajorCategory = '" + c1 + "'";
			}
			//UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
			if ((c2 != null) & !string.IsNullOrEmpty(Strings.Trim(c2))) {
				xsql = xsql + " OR MajorCategory = '" + c2 + "'";
			}
			xsql = xsql + " ORDER BY MajorCategory, Description";
			tblacct = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblacct.RecordCount != 0) {
				tblacct.MoveLast();
				grdAccounts.Rows = tblacct.RecordCount;
				tblacct.MoveFirst();
				r = 0;
				 // ERROR: Not supported in C#: OnErrorStatement

				while (!tblacct.EOF) {
					if (xcat != tblacct.Fields["MajorCategory"].Value) {
						tblcat = WinglGlobal.GLdb.OpenRecordset("SELECT CategoryDescription FROM Category WHERE Key = '" + tblacct.Fields["MajorCategory"].Value + "'");
						if (tblcat.RecordCount > 0) {
							grdAccounts.TargetCell(0, r);
							grdAccounts.Value = "                     <<" + Strings.Trim(tblcat.Fields["CategoryDescription"].Value) + ">>";
							grdAccounts.FontWeight = 700;
							grdAccounts.BorderBottomStyle = XGridLib.xtCellBorderStyle.xtbsDouble;
							grdAccounts.BorderTopStyle = XGridLib.xtCellBorderStyle.xtbsDouble;
							grdAccounts.BorderLeftStyle = XGridLib.xtCellBorderStyle.xtbsDouble;
							grdAccounts.BorderRightStyle = XGridLib.xtCellBorderStyle.xtbsDouble;
							grdAccounts.BorderBottomColor = System.Drawing.ColorTranslator.FromOle(0xff0000);
							grdAccounts.BorderLeftColor = System.Drawing.ColorTranslator.FromOle(0xff0000);
							grdAccounts.BorderRightColor = System.Drawing.ColorTranslator.FromOle(0xff0000);
							grdAccounts.BorderTopColor = System.Drawing.ColorTranslator.FromOle(0xff0000);
							grdAccounts.FontColor = System.Drawing.ColorTranslator.FromOle(0xff0000);
							grdAccounts.TargetCell(2, r);
							grdAccounts.Value = -r;
							xsql = "SELECT * FROM AccountRanges WHERE ReportName = '" + Strings.Trim(fName) + ":" + Strings.Trim(rptName) + "' ";
							xsql = xsql + "AND ReportSection = " + rptSection + " AND  AccountNumber = " + -r;
							tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
							grdAccounts.TargetCell(1, r);
							if (tbl.RecordCount == 0) {
								grdAccounts.Value = false;
							} else {
								grdAccounts.Value = true;
								tbl.Close();
							}
							xcat = tblacct.Fields["MajorCategory"].Value;
							r = r + 1;
							grdAccounts.Rows = grdAccounts.Rows + 1;
						}
					}
					grdAccounts.TargetCell(0, r);
					grdAccounts.Value = tblacct.Fields["Description"];
					grdAccounts.TargetCell(2, r);
					grdAccounts.Value = tblacct.Fields["Key"];
					xsql = "SELECT * FROM AccountRanges WHERE ReportName = '" + Strings.Trim(fName) + ":" + Strings.Trim(rptName) + "' ";
					xsql = xsql + "AND ReportSection = " + rptSection + " AND  AccountNumber = " + tblacct.Fields["Key"].Value;
					tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
					grdAccounts.TargetCell(1, r);
					if (tbl.RecordCount == 0) {
						grdAccounts.Value = false;
					} else {
						grdAccounts.Value = true;
						tbl.Close();
					}
					r = r + 1;
					tblacct.MoveNext();
				}
				tblacct.Close();
			}
			//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			grdAccounts.Refresh();

		}
//UPGRADE_WARNING: Event AcctSelection.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void AcctSelection_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			grdAccounts.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 600);

		}

		private void grdAccounts_Change(System.Object eventSender, AxXGridLib.XGridEvents_ChangeEvent eventArgs)
		{

			bool v = false;
			short i = 0;

			if (eventArgs.col == 1) {
				grdAccounts.TargetCell(2, eventArgs.row);
				if (grdAccounts.Value <= 0) {
					grdAccounts.TargetCell(1, eventArgs.row);
					//UPGRADE_WARNING: Couldn't resolve default property of object grdAccounts.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					v = grdAccounts.Value;
					for (i = eventArgs.row + 1; i <= grdAccounts.Rows - 1; i++) {
						grdAccounts.TargetCell(2, i);
						if (grdAccounts.Value < 0)
							break; // TODO: might not be correct. Was : Exit For
						grdAccounts.TargetCell(1, i);
						grdAccounts.Value = v;
					}
				}
			}


		}
		private void grdAccounts_ClickCell(System.Object eventSender, AxXGridLib.XGridEvents_ClickCellEvent eventArgs)
		{

			if (eventArgs.col == 1) {
				grdAccounts.TargetCell(eventArgs.col, eventArgs.row);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				grdAccounts.Value = (Information.IsDBNull(grdAccounts.Value) | grdAccounts.Value == false ? true : false);
				eventArgs.cancel = true;
			}


		}
	}
}
