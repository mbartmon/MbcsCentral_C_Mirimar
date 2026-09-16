using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using Microsoft.VisualBasic.PowerPacks;
namespace WinGL
{
	internal partial class frmPayrollReport : System.Windows.Forms.Form
	{
		public clsPayrollReport clas;

//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		DAO.Recordset tbl;
		short NumAccounts;

		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object i = null;

			switch (Index) {

				case 0:
					// PRINT
					tbl = WinglGlobal.GLdb.OpenRecordset("PRaccounts");
					tbl.Edit();
					for (i = 0; i <= NumAccounts - 1; i++) {
						lstAccounts[i].Row = lstAccounts[i].ListIndex;
						lstAccounts[i].Col = 1;
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tbl.Fields["Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value = (string.IsNullOrEmpty(Strings.Trim(lstAccounts[i].ColList)) ? -1 : lstAccounts[i].ColList);
					}

					tbl.Update();
					tbl.Close();
					//UPGRADE_WARNING: Couldn't resolve default property of object dtStartDate.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.StartDate = dtStartDate.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtEndDate.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					clas.EndDate = dtEndDate.Value;
					clas.YTD = (chkYTD.CheckState == 1 ? true : false);
					break;

				case 1:
					// CANCEL
					clas.Cancel = true;
					WinglGlobal.rptCancel = true;
					break;

			}

			this.Close();

		}
		private void frmPayrollReport_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object q = null;

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			DAO.Recordset tbl = null;
			DAO.TableDef defTable = null;
			DAO.Field fldField = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2);

			//UPGRADE_WARNING: Couldn't resolve default property of object q. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			q = Conversion.Fix(Convert.ToInt16(DateAndTime.Month(DateAndTime.Today) - 1) / 3) + 1;

			//UPGRADE_WARNING: Couldn't resolve default property of object q. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_WARNING: DateValue has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dtStartDate.Value = DateAndTime.DateValue(Convert.ToString((q - 1) * 3 + 1) + "/01/" + Convert.ToString(DateAndTime.Year(DateAndTime.Today)));
			dtEndDate.Value = DateAndTime.Today;

			 // ERROR: Not supported in C#: OnErrorStatement


			NumAccounts = lstAccounts.Count();

			for (i = 0; i <= NumAccounts - 1; i++) 
            {
				WinglGlobal.getAccounts(lstAccounts[i], 0);
				lstAccounts[i].AddItem("         ", 0);
			}

			tbl = WinglGlobal.GLdb.OpenRecordset("PRaccounts");
			for (i = 0; i <= NumAccounts - 1; i++) 
            {
				lstAccounts[i].SearchText = tbl.Fields["Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value;
				lstAccounts[i].ColumnSearch = 1;
				lstAccounts[i].Action = LpLib.enumAction.ActionSearch;
				if (lstAccounts[i].SearchIndex > -1)
                {
					lstAccounts[i].ListIndex = lstAccounts[i].SearchIndex;
				}
			}
			tbl.Close();

			WinglGlobal.RecallForm(ref this);

			goto Form_Load_exit;
			Form_Load_error:

			if (Err().Number == 3078) 
            {
				// create table
				defTable = WinglGlobal.GLdb.CreateTableDef("PRaccounts");
				for (i = 0; i <= NumAccounts - 1; i++) 
                {
					fldField = defTable.CreateField("Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00"), DAO.DataTypeEnum.dbLong);
					defTable.Fields.Append(fldField);
				}
				WinglGlobal.GLdb.TableDefs.Append(defTable);

				tbl = WinglGlobal.GLdb.OpenRecordset("PRaccounts");
				tbl.AddNew();
				for (i = 0; i <= NumAccounts - 1; i++) 
                {
					tbl.Fields["Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value = -1;
				}
				tbl.Update();
				tbl.Close();
				 // ERROR: Not supported in C#: ResumeStatement

			} else 
            {
				LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, this.Name);
				 // ERROR: Not supported in C#: ResumeStatement

			}
		}
		private void frmPayrollReport_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			object i = null;

			tbl = WinglGlobal.GLdb.OpenRecordset("PRaccounts");
			tbl.Edit();
			for (i = 0; i <= NumAccounts - 1; i++) {
				lstAccounts[i].Row = lstAccounts[i].ListIndex;
				lstAccounts[i].Col = 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tbl.Fields["Account" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value = (string.IsNullOrEmpty(Strings.Trim(lstAccounts[i].ColList)) ? -1 : lstAccounts[i].ColList);
			}
			tbl.Update();
			tbl.Close();

			WinglGlobal.StoreForm(ref this);

		}
	}
}
