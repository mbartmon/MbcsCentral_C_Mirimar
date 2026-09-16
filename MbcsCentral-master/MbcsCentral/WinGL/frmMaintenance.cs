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
	internal partial class frmMaintenance : System.Windows.Forms.Form
	{
		DAO.Database winpm;
		string[] OUTtablenames = new string[3];
		string[] INtablenames = new string[3];
//UPGRADE_WARNING: Arrays in structure tblOut may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblIn may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		DAO.Recordset tblIn;
		DAO.Recordset tblOut;
//UPGRADE_WARNING: Event Check1.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void Check1_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (Check1.CheckState != 0)
				Check2.CheckState = System.Windows.Forms.CheckState.Unchecked;

		}
//UPGRADE_WARNING: Event Check2.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void Check2_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (Check2.CheckState != 0)
				Check1.CheckState = System.Windows.Forms.CheckState.Unchecked;

		}
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object xsql = null;
			object x = null;

			int i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement


			List1.Items.Clear();
			ProgressBar.Value = 0;

			switch (Index) {

				case 0:
					// GET RECORDS
					if (Interaction.MsgBox("Continue?", MsgBoxStyle.YesNo, "Get Records") == MsgBoxResult.Yes) {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = Interaction.GetSetting("Winpm", "Options", "DefaultDataBase");
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (string.IsNullOrEmpty(Strings.Trim(x))) {
							LogError.MsgBox_Renamed("Invalid Input Database", , "Get Records");
							return;
						}
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						winpm = DAODBEngine_definst.Workspaces(0).OpenDatabase(x);
						if (Check2.CheckState != 0) {
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = "DELETE * FROM " + OUTtablenames[WinglGlobal.maintenanceOption];
							WinglGlobal.GLdb.Execute((xsql));
						}
						tblIn = winpm.OpenRecordset(INtablenames[WinglGlobal.maintenanceOption]);
						tblOut = WinglGlobal.GLdb.OpenRecordset(OUTtablenames[WinglGlobal.maintenanceOption]);
						tblIn.MoveLast();
						ProgressBar.Maximum = tblIn.RecordCount;
						tblIn.MoveFirst();
						i = 0;
						while (!tblIn.EOF) {
							movefields();
							tblIn.MoveNext();
							i = i + 1;
							ProgressBar.Value = i;
						}
					}
					break;

				case 1:
					// EXIT
					this.Close();
					break;

			}
			goto Cmd_Click_exit;
			Cmd_Click_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Get Records");
			 // ERROR: Not supported in C#: ResumeStatement

			Cmd_Click_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			tblIn.Close();
			tblOut.Close();
			winpm.Close();

		}

		private void frmMaintenance_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4530);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7995);

			INtablenames[0] = "Owner";
			INtablenames[1] = "Building";

			OUTtablenames[0] = "Company";
			OUTtablenames[1] = "Department";

			switch (WinglGlobal.maintenanceOption) {

				case WinglGlobal.mntCompany:
					this.Text = "Get Companies From WinPM";
					break;

				case WinglGlobal.mntDepartment:
					this.Text = "Get Departments From Winpm";
					break;

			}

		}
		public void movefields()
		{
			object xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			tblOut.AddNew();
			switch (WinglGlobal.maintenanceOption) {

				case WinglGlobal.mntCompany:
					tblOut.Fields["CompanyNumber"].Value = tblIn.Fields["Owner Number"].Value;
					tblOut.Fields["Name"].Value = tblIn.Fields["Owner Name"].Value;
					tblOut.Fields["StreetAddress"].Value = tblIn.Fields["Address 1"].Value;
					tblOut.Fields["City"].Value = tblIn.Fields["Address 2"].Value;
					tblOut.Fields["FederalID"].Value = tblIn.Fields["FederalID"].Value;
					tblOut.Update();
					// Dupe
					if (Err().Number == 3022) {
						if (Check1.CheckState != 0) {
							Err().Clear();
							tblOut.CancelUpdate();
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = "SELECT * FROM Company WHERE CompanyNumber = " + tblIn.Fields["Owner Number"].Value;
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblOut = WinglGlobal.GLdb.OpenRecordset(xsql);
							tblOut.Edit();
							tblOut.Fields["Name"].Value = tblIn.Fields["Owner Name"].Value;
							tblOut.Fields["StreetAddress"].Value = tblIn.Fields["Address 1"].Value;
							tblOut.Fields["City"].Value = tblIn.Fields["Address 2"].Value;
							tblOut.Fields["FederalID"].Value = tblIn.Fields["FederalID"].Value;
							tblOut.Update();
							if (Err().Number != 0) {
								List1.Items.Add("Could not update Company (" + tblIn.Fields["Owner Number"].Value + "). " + Err().Description);
							} else {
								List1.Items.Add("Company (" + tblIn.Fields["Owner Number"].Value + ") updated");
							}
						} else {
							List1.Items.Add("Company (" + tblIn.Fields["Owner Number"].Value + ") not added.  Would have been a duplicate.");
						}
					} else {
						if (Err().Number != 0) {
							List1.Items.Add("Could not add Company (" + tblIn.Fields["Owner Number"].Value + "). " + Err().Description);
						}
					}
					break;

				case WinglGlobal.mntDepartment:
					tblOut.Fields["Department Number"].Value = tblIn.Fields["Building Number"].Value;
					tblOut.Fields["Name"].Value = tblIn.Fields["Address 1"].Value;
					tblOut.Fields["Street Address"].Value = tblIn.Fields["Address 1"].Value;
					tblOut.Fields["City"].Value = tblIn.Fields["Address 2"].Value;
					tblOut.Fields["Company Number"].Value = tblIn.Fields["Owner Number"].Value;
					tblOut.Update();
					// Dupe
					if (Err().Number == 3022) {
						if (Check1.CheckState != 0) {
							Err().Clear();
							tblOut.CancelUpdate();
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							xsql = "SELECT * FROM Department WHERE [Department Number] = " + tblIn.Fields["Building Number"].Value;
							//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							tblOut = WinglGlobal.GLdb.OpenRecordset(xsql);
							tblOut.Edit();
							tblOut.Fields["Name"].Value = tblIn.Fields["Address 1"].Value;
							tblOut.Fields["Street Address"].Value = tblIn.Fields["Address 1"].Value;
							tblOut.Fields["City"].Value = tblIn.Fields["Address 2"].Value;
							tblOut.Fields["Company Number"].Value = tblIn.Fields["Owner Number"].Value;
							tblOut.Update();
							if (Err().Number != 0) {
								List1.Items.Add("Could not update Department (" + tblIn.Fields["Building Number"].Value + "). " + Err().Description);
							} else {
								List1.Items.Add("Department (" + tblIn.Fields["Building Number"].Value + ") updated");
							}
						} else {
							List1.Items.Add("Department (" + tblIn.Fields["Building Number"].Value + ") not added.  Would have been a duplicate.");
						}
					} else {
						if (Err().Number != 0) {
							List1.Items.Add("Could not add Department (" + tblIn.Fields["Building Number"].Value + "). " + Err().Description);
						}
					}
					break;

			}

		}
//UPGRADE_WARNING: Event frmMaintenance.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void frmMaintenance_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			List1.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(List1.Left) - 100);
			List1.Refresh();
		}
	}
}
