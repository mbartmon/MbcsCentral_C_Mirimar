using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	internal partial class JournalDesign : System.Windows.Forms.Form
	{
		bool AddFlag;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// SAVE
					save_layout();
					clear_layout();
					break;

				case 1:
					// DELETE
					if (Interaction.MsgBox("Delete this LAYOUT?", MsgBoxStyle.YesNo, this.Name) == MsgBoxResult.Yes) {
						lstLayouts.Row = lstLayouts.ListIndex;
						WinglGlobal.GLdb.Execute(("DELETE * FROM Journals WHERE Layout = '" + lstLayouts.ColList + "'"));
						lstLayouts.RemoveItem(lstLayouts.ListIndex);
						clear_layout();
					}
					break;

				case 2:
					// EXIT
					this.Close();
					break;

				case 3:
					// APPLY RANGES
					save_layout();
					apply_range();
					lstLayouts_ClickEvent(lstLayouts, new System.EventArgs());
					break;

			}

		}
		private void JournalDesign_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object idxIndex = null;
			object defTable = null;

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;
			DAO.Field fldField = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2));
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2));
			lstLayouts.AddItem("<new>");
			WinglGlobal.tblJournals = WinglGlobal.GLdb.OpenRecordset("SELECT Layout FROM Journals WHERE Column = 0");
			if (WinglGlobal.tblJournals.RecordCount > 0) {
				while (!WinglGlobal.tblJournals.EOF) {
					lstLayouts.AddItem(WinglGlobal.tblJournals.Fields["Layout"].Value);
					WinglGlobal.tblJournals.MoveNext();
				}
				WinglGlobal.tblJournals.Close();
			}

			goto Form_Load_exit;
			Form_Load_error:

			if (Err().Number == 3078) {
				// create table
				defTable = WinglGlobal.GLdb.CreateTableDef("Journals");
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Layout", DAO.DataTypeEnum.dbText, 16);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Column", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Field", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Range", DAO.DataTypeEnum.dbBoolean);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Type", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Heading", DAO.DataTypeEnum.dbText, 16);
				fldField.AllowZeroLength = true;
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("PrintFlag", DAO.DataTypeEnum.dbBoolean);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = defTable.CreateField("Size", DAO.DataTypeEnum.dbInteger, 2);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Fields.Append(fldField);

				// create index
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.CreateIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex = defTable.CreateIndex("PrimaryKey");
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Unique. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Unique = true;
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Primary. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Primary = true;
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = idxIndex.CreateField("Layout");
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.CreateField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				fldField = idxIndex.CreateField("Column");
				//UPGRADE_WARNING: Couldn't resolve default property of object idxIndex.Fields. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				idxIndex.Fields.Append(fldField);
				//UPGRADE_WARNING: Couldn't resolve default property of object defTable.Indexes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				defTable.Indexes.Append(idxIndex);
				WinglGlobal.GLdb.TableDefs.Append(defTable);
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
				this.Close();
			}
			Form_Load_exit:


		}
		private void lstLayouts_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object lname = null;
			object Columns = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			if (AddFlag)
				goto lstLayouts_Click_exit;
			if (lstLayouts.ListIndex > 0) {
				AddFlag = true;
				clear_layout();
				AddFlag = false;

				lstLayouts.Row = lstLayouts.ListIndex;
				WinglGlobal.tblJournals = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Journals WHERE Layout = '" + lstLayouts.ColList + "'");
				if (WinglGlobal.tblJournals.RecordCount > 0) {
					WinglGlobal.tblJournals.MoveLast();
					//UPGRADE_WARNING: Couldn't resolve default property of object Columns. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					Columns = WinglGlobal.tblJournals.RecordCount;
					WinglGlobal.tblJournals.MoveFirst();
					//UPGRADE_WARNING: Couldn't resolve default property of object Columns. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.Rows = Columns + 1;
					while (!WinglGlobal.tblJournals.EOF) {
						//XGrid1.TargetList "Fields"
						XGrid1.TargetCell(0, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = WinglGlobal.tblJournals.Fields["Field"];

						XGrid1.TargetCell(1, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = "";
						 // ERROR: Not supported in C#: OnErrorStatement

						WinglGlobal.tblAccounts = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM AccountRanges WHERE ReportName = 'Journals:" + lstLayouts.ColList + "' AND ReportSection = " + WinglGlobal.tblJournals.Fields["Column"].Value);
						if (WinglGlobal.tblAccounts.RecordCount > 0) {
							XGrid1.Value = "*******";
						}
						lstLayouts_Click_continue:
						 // ERROR: Not supported in C#: OnErrorStatement

						XGrid1.TargetCell(2, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = WinglGlobal.tblJournals.Fields["Type"];
						XGrid1.TargetCell(3, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = WinglGlobal.tblJournals.Fields["Heading"];
						XGrid1.TargetCell(4, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = WinglGlobal.tblJournals.Fields["PrintFlag"];
						XGrid1.TargetCell(5, WinglGlobal.tblJournals.Fields["Column"].Value);
						XGrid1.Value = WinglGlobal.tblJournals.Fields["Size"];
						WinglGlobal.tblJournals.MoveNext();
					}
					WinglGlobal.tblJournals.Close();
					XGrid1.SelectCell(0, 0, false);
				} else {
					LogError.MsgBox_Renamed("Layout not found: " + lstLayouts.ColList, MsgBoxStyle.Critical, this.Name);
				}
			} else {
				if (lstLayouts.ListIndex == 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object lname. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lname = Interaction.InputBox("Enter Name of New Layout", "Journal Layout", , this.Left, this.Top);
					//UPGRADE_WARNING: Couldn't resolve default property of object lname. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (!string.IsNullOrEmpty(Strings.Trim(lname))) {
						//UPGRADE_WARNING: Couldn't resolve default property of object lname. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstLayouts.AddItem(lname);
						AddFlag = true;
						clear_layout();
						lstLayouts.ListIndex = lstLayouts.ListCount - 1;
						AddFlag = false;
					}
				} else {
					//    VBA.Interaction.MsgBox "Invalid selection.", , Me.Name
				}
			}
			goto lstLayouts_Click_exit;
			lstlayouts_Click_norange:

			 // ERROR: Not supported in C#: ResumeStatement

			lstLayouts_Click_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
			 // ERROR: Not supported in C#: ResumeStatement

			lstLayouts_Click_exit:


		}
		public void clear_layout()
		{

			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			if (!AddFlag)
				lstLayouts.ListIndex = -1;
			XGrid1.Rows = 10;
			for (i = 0; i <= XGrid1.Rows - 1; i++) {
				XGrid1.TargetCell(0, i);
				XGrid1.Value = -1;
				XGrid1.TargetCell(1, i);
				XGrid1.Value = "";
				XGrid1.TargetCell(2, i);
				XGrid1.Value = 0;
				XGrid1.TargetCell(3, i);
				XGrid1.Value = "";
				XGrid1.TargetCell(4, i);
				XGrid1.Value = 1;
				XGrid1.TargetCell(5, i);
				XGrid1.Value = "";
			}

		}
		private void XGrid1_Change(System.Object eventSender, AxXGridLib.XGridEvents_ChangeEvent eventArgs)
		{
			object x = null;

			short s = 0;

			if (eventArgs.col == 0) {
				XGrid1.TargetCell(eventArgs.col, eventArgs.row);

				XGrid1.TargetList("Fields");
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = XGrid1.GetListItemText(XGrid1.FindListKey(XGrid1.Value));
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				s = Conversion.Val(Strings.Mid(x, Strings.InStr(x, "(") + 1));
				XGrid1.TargetCell(5, eventArgs.row);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(XGrid1.Value)) {
					XGrid1.Value = s;
				} else if (Conversion.Val(XGrid1.Value) == 0) {
					XGrid1.Value = s;
				}
			}

		}
		private void XGrid1_ClickCell(System.Object eventSender, AxXGridLib.XGridEvents_ClickCellEvent eventArgs)
		{
			object i = null;

			if (eventArgs.col == 1 & lstLayouts.ListIndex > -1) {
				save_layout();
				My.MyProject.Forms.AcctSelection.AccountSelection("Journals", (lstLayouts.ColList), Convert.ToInt32(eventArgs.row));
				My.MyProject.Forms.AcctSelection.ShowDialog();
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				i = lstLayouts.ListIndex;
				clear_layout();
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstLayouts.ListIndex = i;
				XGrid1.SelectCell(1, eventArgs.row, false);
				eventArgs.cancel = true;
			}

		}
		public void apply_range()
		{

			//UPGRADE_WARNING: Arrays in structure tblrange may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblrange = null;
			int[] accounts = null;
			short i = 0;
			short r = 0;

			tblrange = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM AccountRanges WHERE ReportName = 'Journals:" + Strings.Trim(lstLayouts.ColList) + "' AND ReportSection = 0");
			if (tblrange.RecordCount > 0) {
				tblrange.MoveLast();
				accounts = new int[tblrange.RecordCount];
				tblrange.MoveFirst();
				i = 0;
				while (!tblrange.EOF) {
					accounts[i] = tblrange.Fields["AccountNumber"].Value;
					i = i + 1;
					tblrange.MoveNext();
				}
				tblrange.Close();
				WinglGlobal.GLdb.Execute(("DELETE * FROM AccountRanges WHERE ReportName = 'Journals:" + Strings.Trim(lstLayouts.ColList) + "'"));
				tblrange = WinglGlobal.GLdb.OpenRecordset("AccountRanges");

				for (i = 0; i <= XGrid1.Rows - 1; i++) {
					XGrid1.TargetCell(0, i);
					if (XGrid1.Value > -1) {
						for (r = 0; r <= Information.UBound(accounts, 1); r++) {
							tblrange.AddNew();
							tblrange.Fields["ReportName"].Value = "Journals:" + lstLayouts.ColList;
							tblrange.Fields["ReportSection"].Value = i;
							tblrange.Fields["AccountNumber"].Value = accounts[r];
							tblrange.Update();
						}
					}
				}
				tblrange.Close();
			} else {
				if (Interaction.MsgBox("There are currently no Account Ranges set up for the first field.  Do you want to clear all Account Ranges?", MsgBoxStyle.YesNo, this.Text) == MsgBoxResult.Yes) {
					WinglGlobal.GLdb.Execute(("DELETE * FROM AccountRanges WHERE ReportName = 'Journals:" + Strings.Trim(lstLayouts.ColList) + "'"));
				}
			}

		}
		public void save_layout()
		{
			object i = null;

			lstLayouts.Row = lstLayouts.ListIndex;
			WinglGlobal.GLdb.Execute(("DELETE * FROM Journals WHERE Layout = '" + lstLayouts.ColList + "'"));
			WinglGlobal.tblJournals = WinglGlobal.GLdb.OpenRecordset("Journals");
			for (i = 0; i <= XGrid1.Rows - 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				XGrid1.TargetCell(0, i);
				if (XGrid1.Value > -1 & !string.IsNullOrEmpty(Strings.Trim(XGrid1.Value))) {
					WinglGlobal.tblJournals.AddNew();
					WinglGlobal.tblJournals.Fields["Layout"].Value = lstLayouts.ColList;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblJournals.Fields["Column"].Value = i;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(0, i);
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblJournals.Fields["Field"].Value = XGrid1.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(1, i);
					WinglGlobal.tblJournals.Fields["Range"].Value = (Strings.Left(XGrid1.Value, 1) == "*" ? true : false);
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(2, i);
					WinglGlobal.tblJournals.Fields["Type"].Value = (string.IsNullOrEmpty(Strings.Trim(XGrid1.Value)) ? 0 : XGrid1.Value);
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(3, i);
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblJournals.Fields["Heading"].Value = XGrid1.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(4, i);
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblJournals.Fields["PrintFlag"].Value = XGrid1.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					XGrid1.TargetCell(5, i);
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblJournals.Fields["Size"].Value = XGrid1.Value;
					WinglGlobal.tblJournals.Update();
				}
			}
			WinglGlobal.tblJournals.Close();

		}
	}
}
