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
	internal partial class BalFwdOld : System.Windows.Forms.Form
	{
		string jrn;
		public void conv_balance(ref short li)
		{
			object xsql = null;
			string x = null;

			//UPGRADE_WARNING: Arrays in structure tbl2 may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			//UPGRADE_WARNING: Arrays in structure balance may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			string cd = null;
			balance_type balance = default(balance_type);
			DAO.Recordset tbl = null;
			DAO.Recordset tbl2 = null;
			short i = 0;
			string l = null;
			short fi = 0;
			Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString c = new Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString(2);
			Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString d = new Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString(3);

			l = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCompanies, li);
			c.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(Strings.Mid(l, 10, 2)), "00");
			d.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(Strings.Mid(l, 26, 3)), "000");
			x = WinglGlobal.appath + "A" + c.Value + d.Value + ".txt";
			 // ERROR: Not supported in C#: OnErrorStatement

			fi = FreeFile();
			FileSystem.FileOpen(fi, x, OpenMode.Input);
			if (Err().Number == 0) {
				tbl2 = WinglGlobal.GLdb.OpenRecordset("TransactionTemp");
				FileSystem.FileClose(fi);
				FileSystem.FileOpen(fi, x, OpenMode.Binary);
				lblCurrent.Text = l;
				lblCurrent.Refresh();
				ProgressBar1.Maximum = FileSystem.LOF(fi) / Strings.Len(balance);
				ProgressBar1.Value = 0;
				//UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				FileSystem.FileGet(fi, balance);
				while (!FileSystem.EOF(fi)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT Key FROM Accounts WHERE OldAccountNumber = '" + balance.Account + "' AND OldSubNumber = '" + balance.subacct + "'";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tbl.RecordCount > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + c.Value + " AND DepartmentNumber = " + d.Value + " AND AccountNumber = " + tbl.Fields["Key"].Value;
						tbl.Close();
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tbl.RecordCount > 0) {
							if (chkPrior.CheckState != 0) {
								tbl.Edit();
								for (i = 1; i <= 12; i++) {
									tbl.Fields["P_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "00")].Value = balance.amounts(i);
								}
								tbl.Update();
							}
							if (chkOpen.CheckState != 0) {
								tbl2.AddNew();
								tbl2.Fields["CompanyNumber"].Value = Conversion.Val(c.Value);
								tbl2.Fields["DepartmentNumber"].Value = Conversion.Val(d.Value);
								tbl2.Fields["AccountNumber"].Value = tbl.Fields["AccountNumber"].Value;
								//UPGRADE_WARNING: Couldn't resolve default property of object dtDate.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								tbl2.Fields["Date"].Value = dtDate.Value;
								tbl2.Fields["Journal"].Value = jrn;
								tbl2.Fields["Source"].Value = "Prior Year";
								tbl2.Fields["Description"].Value = txtDescription.Text;
								tbl2.Fields["Amount"].Value = 0;
								for (i = 1; i <= 12; i++) {
									tbl2.Fields["Amount"].Value = tbl2.Fields["Amount"].Value + balance.amounts(i);
								}
								if (tbl2.Fields["Amount"].Value != 0) {
									tbl2.Update();
								} else {
									tbl2.CancelUpdate();
								}
							}
						}
					}
					ProgressBar1.Value = ProgressBar1.Value + 1;
					//UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					FileSystem.FileGet(fi, balance);
				}
				FileSystem.FileClose(fi);
			}
			Err().Clear();
			FileSystem.Input(1, ref cd);
			FileSystem.FileClose();
			tbl.Close();
			tbl2.Close();

		}
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			short i = 0;

			switch (Index) {

				case 0:
					// PROCESS
					this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
					ProgressBar2.Maximum = 1;
					ProgressBar2.Value = 0;
					if (lstJournals.ListIndex == -1) {
						jrn = "";
					} else {
						lstJournals.Row = lstJournals.ListIndex;
						jrn = lstJournals.ColList;
					}
					for (i = 0; i <= lstCompanies.Items.Count - 1; i++) {
						if (lstCompanies.GetSelected(Convert.ToInt32(i))) {
							ProgressBar2.Maximum = ProgressBar2.Maximum + 1;
						}
					}

					for (i = 0; i <= lstCompanies.Items.Count - 1; i++) {
						if (lstCompanies.GetSelected(Convert.ToInt32(i))) {
							conv_balance(ref i);
							ProgressBar2.Value = ProgressBar2.Value + 1;
						}
					}

					ProgressBar2.Value = ProgressBar2.Maximum;
					this.Cursor = System.Windows.Forms.Cursors.Default;
					break;

				case 1:
					// CANCEL
					this.Close();
					break;

			}

		}
		private void BalFwdOld_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object d = null;
			object c = null;
			string x = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(4860);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8790);
			this.Top = 0;
			this.Left = 0;

			WinglGlobal.GetJournals(ref this);
			dtDate.Value = DateAndTime.Today;
			//UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			x = FileSystem.Dir(WinglGlobal.appath + "*.txt");

			while (!string.IsNullOrEmpty(Strings.Trim(x))) {
				if (Strings.Len(x) == 10 & Strings.UCase(Strings.Left(x, 1)) == "A") {
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					c = Conversion.Val(Strings.Mid(x, 2, 2));
					//UPGRADE_WARNING: Couldn't resolve default property of object d. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					d = Conversion.Val(Strings.Mid(x, 4, 3));
					//UPGRADE_WARNING: Couldn't resolve default property of object d. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (c > 0 & d > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object d. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstCompanies.Items.Add("Company: " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(c, "##"), ref 2) + "  Department: " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(d, "###"), ref 3));
					}
				}
				//UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				x = FileSystem.Dir();
			}

		}
//UPGRADE_WARNING: Event BalFwdOld.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void BalFwdOld_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			lstCompanies.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 900);

		}
	}
}
