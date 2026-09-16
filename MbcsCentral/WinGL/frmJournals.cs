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
	internal partial class frmJournals : System.Windows.Forms.Form
	{
//* $Header$
//* $Log$
//* Revision 1.1  2009/08/21 12:36:13  mike
//* Re-import
//*
//* Committed on the Free edition of March Hare Software CVSNT Server.
//* Upgrade to CVS Suite for more features and support:
//* http://march-hare.com/cvsnt/
//*
//* Revision 1.2  2008/06/07 01:53:07  mike
//* Copied from wingl folder
//*
		public clsJournals clas;
//UPGRADE_WARNING: Event chkConsolidate.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkConsolidate_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkConsolidate.CheckState != 0) {
				chkSingle.CheckState = System.Windows.Forms.CheckState.Unchecked;
				chkSingle.Enabled = false;
				lstDepartments.Enabled = false;
			} else {
				chkSingle.Enabled = true;
			}

		}
//UPGRADE_WARNING: Event chkSingle.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void chkSingle_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (chkSingle.CheckState != 0) {
				lstDepartments.Enabled = true;
				chkConsolidate.CheckState = System.Windows.Forms.CheckState.Unchecked;
				chkConsolidate.Enabled = false;
			} else {
				lstDepartments.Enabled = false;
				chkConsolidate.Enabled = true;
			}

		}
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object i = null;

			switch (Index) {

				case 0:
					// PRINT
					if (lstCompanies.ListIndex == -1) {
						Interaction.MsgBox("You must select a Company to print.", , this.Name);
						return;
					}
					if (chkSingle.CheckState == 1 & lstDepartments.ListIndex == -1) {
						Interaction.MsgBox("You must select a Department to print.", , this.Name);
						return;
					}
					if (lstJournals.SelectedIndex == -1) {
						Interaction.MsgBox("You must select a Journal to print.", , this.Name);
						return;
					}
					if (lstLayouts.SelectedIndex == -1) {
						Interaction.MsgBox("You must select a Layout to print.", , this.Name);
						return;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object dtEnd.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object dtStart.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (dtStart.Value > dtEnd.Value) {
						Interaction.MsgBox("Start Date must be less than or equal to End Date.", , this.Name);
						return;
					}

					lstCompanies.Row = lstCompanies.ListIndex;
					lstCompanies.Col = 0;
					clas.CompanyName = lstCompanies.ColList;
					lstCompanies.Col = 1;
					clas.Company = lstCompanies.ColList;

					clas.Consolidate = (chkConsolidate.CheckState == 0 ? false : true);

					clas.Department = Convert.ToString(-1);

					if (!clas.Consolidate) {
						if (chkSingle.CheckState == 1) {
							lstDepartments.Row = lstDepartments.ListIndex;
							lstDepartments.Col = 0;
							clas.DepartmentName = lstDepartments.ColList;
							lstDepartments.Col = 1;
							clas.Department = lstDepartments.ColList;
						}
					}

					clas.JournalName = lstJournals.Text;

					clas.layout = lstLayouts.Text;

					clas.SourceSearch = (chkSource.CheckState == 0 ? false : true);

					clas.PrintTotals = (chkTotal.CheckState == 0 ? false : true);

					clas.ReportName = txtTitle.Text;

					//UPGRADE_WARNING: Couldn't resolve default property of object dtStart.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.StartDate = dtStart.Value;

					//UPGRADE_WARNING: Couldn't resolve default property of object dtEnd.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.EndDate = dtEnd.Value;

					clas.Cancel = false;
					break;

				case 1:
					// CANCEL
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

				case 2:
					My.MyProject.Forms.JournalDesign.ShowDialog();
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					i = lstLayouts.SelectedIndex;
					lstLayouts.Items.Clear();
					WinglGlobal.tblJournals = WinglGlobal.GLdb.OpenRecordset("SELECT Layout FROM Journals WHERE Column = 1");
					if (WinglGlobal.tblJournals.RecordCount > 0) {
						while (!WinglGlobal.tblJournals.EOF) {
							lstLayouts.Items.Add(WinglGlobal.tblJournals.Fields["Layout"].Value);
							WinglGlobal.tblJournals.MoveNext();
						}
						WinglGlobal.tblJournals.Close();
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstLayouts.SelectedIndex = i;
					return;

					break;

			}
			this.Close();

		}
		private void frmJournals_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);
			WinglGlobal.GetCompanies(ref this);
			WinglGlobal.GetJournals(ref this);
			dtStart.Value = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), 1, 1);
			dtEnd.Value = DateAndTime.Today;
			WinglGlobal.tblJournals = WinglGlobal.GLdb.OpenRecordset("SELECT Layout FROM Journals WHERE Column = 0");
			if (WinglGlobal.tblJournals.RecordCount > 0) {
				while (!WinglGlobal.tblJournals.EOF) {
					lstLayouts.Items.Add(WinglGlobal.tblJournals.Fields["Layout"].Value);
					WinglGlobal.tblJournals.MoveNext();
				}
				WinglGlobal.tblJournals.Close();
			}
			lstCompanies.ListIndex = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Company", "-1"));
			chkConsolidate.CheckState = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Consolidate", "0"));
			chkSingle.CheckState = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Single", "0"));
			lstDepartments.ListIndex = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Department", "-1"));
			lstJournals.SelectedIndex = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "JournalName", "-1"));
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Layout", "-1"));
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstLayouts.SelectedIndex = (i < lstLayouts.Items.Count ? i : -1);
			chkSource.CheckState = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Source", "0"));
			chkTotal.CheckState = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "Total", "0"));
			txtTitle.Text = Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "ReportTitle", "");
			//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dtStart.Value = DateAndTime.DateValue(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "StartDate", Convert.ToString(DateAndTime.Today)));
			//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dtEnd.Value = DateAndTime.DateValue(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Journal", "EndDate", Convert.ToString(DateAndTime.Today)));

			WinglGlobal.RecallForm(ref this);

			if (Information.IsNumeric(clas.Company)) {
				if (Convert.ToDouble(clas.Company) > 0) {
					lstCompanies.SearchText = clas.Company;
					lstCompanies.ColumnSearch = 1;
					lstCompanies.Action = LpLib.enumAction.ActionSearch;
					lstCompanies.ListIndex = lstCompanies.SearchIndex;
					chkConsolidate.CheckState = (clas.Consolidate ? 1 : 0);
					if (!chkConsolidate.CheckState & lstCompanies.ListIndex > -1) {
						lstCompanies.Col = 1;
						lstCompanies.Row = lstCompanies.ListIndex;
						WinglGlobal.GetDepartments(ref this, ref Conversion.Val(lstCompanies.ColList));
						if (Convert.ToDouble(clas.Department) > 0) {
							chkSingle.CheckState = System.Windows.Forms.CheckState.Checked;
							lstDepartments.SearchText = clas.Department;
							lstDepartments.ColumnSearch = 1;
							lstDepartments.Action = LpLib.enumAction.ActionSearch;
							lstDepartments.ListIndex = lstDepartments.SearchIndex;
							chkSingle.CheckState = System.Windows.Forms.CheckState.Checked;
						}
					}
					for (i = 0; i <= lstJournals.Items.Count - 1; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstJournals, i) == clas.JournalName) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstJournals.SelectedIndex = i;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					for (i = 0; i <= lstLayouts.Items.Count - 1; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstLayouts, i) == clas.layout) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstLayouts.SelectedIndex = i;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					chkSource.CheckState = (clas.SourceSearch ? 1 : 0);
					chkTotal.CheckState = (clas.PrintTotals ? 1 : 0);
					txtTitle.Text = clas.ReportName;
					dtStart.Value = clas.StartDate;
					dtEnd.Value = clas.EndDate;
				}
			}

			goto Form_Load_exit;
			Form_Load_error:

			if (Err().Number == 3078) {
				clas.Cancel = true;
				if (Interaction.MsgBox("You must create at least one Journal Layout before you can run this report.  Do you want to do that now?", MsgBoxStyle.YesNo, this.Name) == MsgBoxResult.Yes) {
					My.MyProject.Forms.JournalDesign.Show();
				}
			} else {
				LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
				 // ERROR: Not supported in C#: ResumeStatement

				//Resume Form_Load_exit
			}
			this.Close();
			Form_Load_exit:


		}
		private void frmJournals_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			//SaveSetting App.Title, "Journal", "Company", CStr(lstCompanies.ListIndex)
			//SaveSetting App.Title, "Journal", "Consolidate", CStr(chkConsolidate.Value)
			//SaveSetting App.Title, "Journal", "Single", CStr(chkSingle.Value)
			//SaveSetting App.Title, "Journal", "Department", CStr(lstDepartments.ListIndex)
			//SaveSetting App.Title, "Journal", "JournalName", CStr(lstJournals.ListIndex)
			//SaveSetting App.Title, "Journal", "Layout", CStr(lstLayouts.ListIndex)
			//SaveSetting App.Title, "Journal", "Source", CStr(chkSource.Value)
			//SaveSetting App.Title, "Journal", "Total", CStr(chkTotal.Value)
			//SaveSetting App.Title, "Journal", "ReportTitle", txtTitle.Text
			//SaveSetting App.Title, "Journal", "StartDate", CStr(dtStart.Value)
			//SaveSetting App.Title, "Journal", "EndDate", CStr(dtEnd.Value)

			WinglGlobal.StoreForm(ref this);

		}

		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			lstCompanies.Row = lstCompanies.ListIndex;
			lstCompanies.Col = 1;
			WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
			if (chkSingle.CheckState & lstDepartments.ListIndex == -1) {
				lstDepartments.ListIndex = 0;
			}

		}
		private void lstDepartments_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{

			chkConsolidate.CheckState = System.Windows.Forms.CheckState.Unchecked;
			chkConsolidate.Enabled = false;
			chkSingle.Enabled = true;
			chkSingle.CheckState = System.Windows.Forms.CheckState.Checked;

		}
	}
}
