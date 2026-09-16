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
	internal partial class CompanyDepartment : System.Windows.Forms.Form
	{
		public void Delete_Department()
		{
			object xsql = null;

			short o = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4545);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8265);
			WinglGlobal.tblDepartment.Close();
			 // ERROR: Not supported in C#: OnErrorStatement

			o = Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstDepartments, lstDepartments.SelectedIndex), 3));
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Department WHERE [Department Number] = " + o;
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (WinglGlobal.tblDepartment.RecordCount > 0) {
				WinglGlobal.tblDepartment.Delete();
				GetDepartments(ref o);
			}
			goto Delete_Department_exit;
			Delete_Department_error:

			LogError.MsgBox_Renamed("Error while removing Department: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			Delete_Department_exit:


		}
		public void Delete_Company()
		{
			object xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblCompany.Close();
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			xsql = "SELECT * FROM Company WHERE [CompanyNumber] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3);
			//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset(xsql);
			WinglGlobal.tblCompany.Delete();
			goto Delete_Company_exit;
			Delete_Company_error:

			LogError.MsgBox_Renamed("Error while removing Company: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			Delete_Company_exit:


		}
		public void GetDepartments(ref short o)
		{

			string xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblDepartment.Close();
			 // ERROR: Not supported in C#: OnErrorStatement

			lstDepartments.Items.Clear();
			xsql = "SELECT * FROM Department WHERE [Company Number] =" + Conversion.Str(o);
			WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
			WinglGlobal.tblDepartment.MoveFirst();
			while (!WinglGlobal.tblDepartment.EOF) {
				lstDepartments.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.tblDepartment.Fields["Department Number"].Value, "00#") + " " + WinglGlobal.tblDepartment.Fields["Name"].Value);
				WinglGlobal.tblDepartment.MoveNext();
			}
			goto GetDepartments_exit;
			GetDepartments_error:

			if (Err().Number != 3021) {
				LogError.MsgBox_Renamed("Error while reading Department table: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			GetDepartments_exit:


		}
		private void cmdCompanyDepartment_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdCompanyDepartment.GetIndex(eventSender);
			object n = null;

			string o = null;
			string xsql = null;
			short i = 0;

			switch (Index) {

				case 0:
					// ADD Company
					WinglGlobal.AddFlag = true;
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.tblCompany.Close();
					WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset("Company");
					My.MyProject.Forms.CompanyDetail.Top = this.Top;
					My.MyProject.Forms.CompanyDetail.Left = this.Left;
					My.MyProject.Forms.CompanyDetail.ShowDialog();
					WinglGlobal.GetCompanies(ref this);
					break;

				case 1:
					// REMOVE Company
					if (lstCompanies.SelectedIndex == -1) {
						Interaction.MsgBox("You must select an Company to remove.", , "WinPM");
						goto Cmd_exit;
					}
					o = Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3);
					 // ERROR: Not supported in C#: OnErrorStatement

					xsql = "SELECT [Department Number] FROM Department WHERE [Company Number] = " + o;
					WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset(xsql);
					WinglGlobal.tblDepartment.MoveLast();
					//UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					n = WinglGlobal.tblDepartment.RecordCount;
					WinglGlobal.tblDepartment.Close();
					 // ERROR: Not supported in C#: OnErrorStatement

					//UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (n != 0) {
						Interaction.MsgBox("You must Move or Remove all Departments for this Company before you can Remove the Company record.", , "WinPM");
						goto Cmd_exit;
					}
					Err().Clear();
					if (LogError.MsgBox_Renamed("Do you really want to remove Company #" + o + "?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Company Table") == MsgBoxResult.Yes) {
						Delete_Company();
						WinglGlobal.GetCompanies(ref this);
					}
					break;

				case 2:
					// ADD Department
					if (lstCompanies.SelectedIndex == -1) {
						Interaction.MsgBox("You must select an Company to which to add a Department.", , "WinPM");
						goto Cmd_exit;
					}
					 // ERROR: Not supported in C#: OnErrorStatement

					xsql = "SELECT [CompanyNumber],[Name] FROM Company WHERE [CompanyNumber] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3);
					WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.AddFlag = true;
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.tblDepartment.Close();
					WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset("Department");
					My.MyProject.Forms.DepartmentDetail.Top = this.Top;
					My.MyProject.Forms.DepartmentDetail.Left = this.Left;
					My.MyProject.Forms.DepartmentDetail.Show();
					GetDepartments(ref Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3)));
					break;

				case 3:
					// MOVE Department
					if (lstDepartments.SelectedIndex == -1) {
						Interaction.MsgBox("You must select a Department to move.", , "WinPM");
						goto Cmd_exit;
					}
					xsql = "SELECT [Department Number], [Company Number], [Name] FROM Department WHERE [Department Number] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstDepartments, lstDepartments.SelectedIndex), 3);
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
					My.MyProject.Forms.MoveDpt.ShowDialog();
					GetDepartments(ref Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3)));
					break;


				case 4:
					// REMOVE Department
					if (lstDepartments.SelectedIndex == -1) {
						Interaction.MsgBox("You must select a Department to remove.", , "WinPM");
						goto Cmd_exit;
					}
					o = Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstDepartments, lstDepartments.SelectedIndex), 3);
					Err().Clear();
					if (LogError.MsgBox_Renamed("Do you really want to remove Department #" + o + "?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Department Table") == MsgBoxResult.Yes) {
						if (LogError.MsgBox_Renamed("Are you absolutely certain you want to Remove this Department?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Department Table") == MsgBoxResult.Yes) {
							Delete_Department();
							GetDepartments(ref Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3)));
						}
					}
					break;

				case 5:
					// EXIT
					this.Close();
					break;

				case 6:
					// EDIT Department
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (lstDepartments.SelectedIndex < 0) {
						Interaction.MsgBox("You must select a Department to edit.", , "WinPM");
						goto Cmd_exit;
					}
					lstDepartments_DoubleClick(lstDepartments, new System.EventArgs());
					break;

				case 7:
					// EDIT Company
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (Information.IsDBNull(lstCompanies.SelectedIndex) | lstCompanies.SelectedIndex < 0) {
						Interaction.MsgBox("You must select an Company to edit.", , "WinPM");
						goto Cmd_exit;
					}
					lstCompanies_DoubleClick(lstCompanies, new System.EventArgs());
					break;

			}

			goto Cmd_exit;
			cmd_no_Company:

			LogError.MsgBox_Renamed("Error locating Company: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			cmd_no_Department:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			Cmd_exit:


		}

		private void cmdOwnersBuildings_Click(ref short Index)
		{

		}

		private void CompanyDepartment_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4545);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8265);
			lstDepartments.Items.Clear();
			WinglGlobal.GetCompanies(ref this);

		}
		private void CompanyDepartment_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.tblCompany.Close();
			WinglGlobal.tblDepartment.Close();

		}
		private void lstDepartments_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
		{

			string xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			xsql = "SELECT [CompanyNumber],[Name] FROM Company WHERE [CompanyNumber] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3);
			WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
			xsql = "SELECT * FROM Department WHERE [Department Number] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstDepartments, lstDepartments.SelectedIndex), 3);
			WinglGlobal.tblDepartment = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
			WinglGlobal.AddFlag = false;
			My.MyProject.Forms.DepartmentDetail.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop);
			My.MyProject.Forms.DepartmentDetail.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);
			My.MyProject.Forms.DepartmentDetail.ShowDialog();
			 // ERROR: Not supported in C#: OnErrorStatement

			goto lstDepartmentsdblclick_exit;
			lstDepartmentsdblclick_error:

			LogError.MsgBox_Renamed("Error while reading Department table: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			lstDepartmentsdblclick_exit:

			GetDepartments(ref Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3)));

		}
//UPGRADE_WARNING: Event lstCompanies.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstCompanies_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			GetDepartments(ref Conversion.Val(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3)));

		}
		private void lstCompanies_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
		{

			string xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			xsql = "SELECT * FROM Company WHERE [CompanyNumber] = " + Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, lstCompanies.SelectedIndex), 3);
			WinglGlobal.tblCompany = WinglGlobal.GLdb.OpenRecordset(xsql, DAO.RecordsetTypeEnum.dbOpenDynaset, DAO.RecordsetOptionEnum.dbSeeChanges + DAO.RecordsetOptionEnum.dbConsistent);
			WinglGlobal.AddFlag = false;
			My.MyProject.Forms.CompanyDetail.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop);
			My.MyProject.Forms.CompanyDetail.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);
			My.MyProject.Forms.CompanyDetail.ShowDialog();
			goto lstCompaniesdblclick_exit;
			lstCompaniesdblclick_error:

			LogError.MsgBox_Renamed("Error while reading Company table: ", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			lstCompaniesdblclick_exit:

			WinglGlobal.GetCompanies(ref this);
		}
	}
}
