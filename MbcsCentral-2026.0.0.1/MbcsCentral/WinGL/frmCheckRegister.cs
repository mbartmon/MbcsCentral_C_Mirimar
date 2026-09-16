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
	internal partial class frmCheckRegister : System.Windows.Forms.Form
	{
		public clsCheckRegister clas;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object i = null;

			switch (Index) {

				case 0:
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					i = lstCompanies.ListIndex;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (i < 0) {
						LogError.MsgBox_Renamed("You must select a company to print.", , "Check Register");
						goto Cmd_Click_exit;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstCompanies.Row = i;
					lstCompanies.Col = 1;
					clas.Company = Convert.ToInt32(lstCompanies.ColList);
					lstCompanies.Col = 0;
					clas.CompanyName = lstCompanies.ColList;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					i = lstDepartments.ListIndex;
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (i < 0) {
						clas.Department = 0;
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstDepartments.Row = i;
						lstDepartments.Col = 1;
						clas.Department = Convert.ToInt32(lstDepartments.ColList);
						lstDepartments.Col = 0;
						clas.DepartmentName = lstDepartments.ColList;
					}
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					clas.StartCheck = Conversion.Val(txtStartCheck.CtlText);
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					clas.EndCheck = Conversion.Val(txtEndCheck.CtlText);
					if (clas.EndCheck == 0)
						clas.EndCheck = 999999;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtStart.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.StartDate = dtStart.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtEnd.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.EndDate = dtEnd.Value;
					break;

				case 1:
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			Interaction.SaveSetting("WinGL", "Dates", "CheckRegStart", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtStart._Value, "mm/dd/yyyy"));
			Interaction.SaveSetting("WinGL", "Dates", "CheckRegEnd", Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dtEnd._Value, "mm/dd/yyyy"));
			this.Close();
			Cmd_Click_exit:


		}
		private void frmCheckRegister_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);
			WinglGlobal.GetCompanies(ref this);
			dtStart.Value = Interaction.GetSetting("WinGL", "Dates", "CheckRegStart", Convert.ToString(DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), DateAndTime.Month(DateAndTime.Today), 1)));
			dtEnd.Value = Interaction.GetSetting("WinGL", "Dates", "CheckRegEnd", Convert.ToString(DateAndTime.Today));

			if (clas.Company > 0) {
				lstCompanies.SearchText = Convert.ToString(clas.Company);
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				if (lstCompanies.ListIndex > -1) {
					lstCompanies.Col = 1;
					lstCompanies.Row = lstCompanies.ListIndex;
					WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
					if (clas.Department > 0) {
						lstDepartments.SearchText = Convert.ToString(clas.Department);
						lstDepartments.ColumnSearch = 1;
						lstDepartments.Action = LpLib.enumAction.ActionSearch;
						lstDepartments.ListIndex = lstDepartments.SearchIndex;
					}
				}
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtStartCheck.CtlText = clas.StartCheck;
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtEndCheck.CtlText = clas.EndCheck;
				dtStart.Value = clas.StartDate;
				dtEnd.Value = clas.EndDate;
			} else {
				WinglGlobal.RecallForm(ref this);
			}

		}
		private void frmCheckRegister_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			i = lstCompanies.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstCompanies.Row = i;
			lstCompanies.Col = 1;
			WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));

			lstDepartments.ListIndex = 0;
			lstDepartments.Row = 0;

		}
	}
}
