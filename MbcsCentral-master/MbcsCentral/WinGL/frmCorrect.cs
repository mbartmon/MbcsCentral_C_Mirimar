using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MbcsCentral.WinGL
{
	internal partial class frmCorrect : System.Windows.Forms.Form
	{
		Accounts tblAccounts;
		ApChecks[] tblOrphan;
		Vendors tblVend;
		ArrayList missing;
		string PMSDataBase;
		bool found;
		short i;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// Process
					correct();
					this.Close();
					break;

				case 1:
					// Exit
					this.Close();
					break;

			}

		}
		private void frmCorrect_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(5190);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(9585);
			My.MyProject.Forms.frmMessage.Text = "Searching for orphaned transcations.";
			My.MyProject.Forms.frmMessage.Show();
			tblOrphan = WinglGlobal.GLdb.OpenRecordset("SELECT Description, Key FROM Accounts ORDER BY Description");
			XGrid1.TargetList("Accounts");
			while (!tblOrphan.EOF) {
				XGrid1.AddListItem(tblOrphan.Fields["Description"].Value, tblOrphan.Fields["Key"].Value);
				tblOrphan.MoveNext();
			}
			tblOrphan.Close();
			XGrid1.TargetCol(5);
			XGrid1.CellType = XGridLib.xtCellType.xtctDropList;
			XGrid1.DropList = "Accounts";
			XGrid1.DropSelectMode = XGridLib.xtListSelectMode.xtSelectKey;
			XGrid1.DropWidth = 4300;
			XGrid1.DropHeight = 3500;

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			GetOrphanAccts();
			this.Cursor = System.Windows.Forms.Cursors.Default;

			if (i == -1) {
				My.MyProject.Forms.frmMessage.Close();
				LogError.MsgBox_Renamed("There are no orphaned transactions");
				return;
			}

			XGrid1.Rows = Information.UBound(missing, 2) + 1;
			for (i = 0; i <= Information.UBound(missing, 2); i++) {
				XGrid1.TargetCell(0, i);
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//XGrid1.Value = VB6.Format(missing(3, i), "00") & " / " & VB6.Format(missing(4, i), "000")
				XGrid1.TargetCell(1, i);
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//XGrid1.Value = VB6.Format(missing(1, i), "######")
				XGrid1.TargetCell(2, i);
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//XGrid1.Value = VB6.Format(missing(0, i), "##000")
				XGrid1.TargetCell(3, i);
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(2, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//XGrid1.Value = missing(5, i) & ": " & missing(2, i)
				XGrid1.TargetCell(4, i);
				//UPGRADE_WARNING: Couldn't resolve default property of object missing(6, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//XGrid1.Value = missing(6, i)
			}
			//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			XGrid1.Refresh();
			missing = null;
			My.MyProject.Forms.frmMessage.Close();

		}
		public void GetOrphanAccts()
		{
			object j = null;
			object t = null;
			object m = null;
			string vnum;
			string xsql;

			 ApChecks[] tblOrphanVend = null;

			//From the Check Register
			i = -1;
			xsql = "SELECT * FROM APChecks WHERE Sequence <> 0 AND [Vendor/Account] NOT IN (SELECT Key FROM Accounts)";
			tblOrphan = ApChecks.LoadQuery(xsql);
            if (tblOrphan != null && tblOrphan.Length > 0)
            {
                missing = new ArrayList();
                foreach (ApChecks check in tblOrphan)
                {
                    xsql = "SELECT [Vendor/Account] FROM APChecks WHERE Company = " + check.Company;
                    xsql += " AND Department = " + check.Department.ToString() + " AND CheckNumber = ";
                    xsql += check.CheckNumber.ToString() + " AND Sequence = 0";
                    tblOrphanVend = ApChecks.LoadQuery(xsql);
                    if (tblOrphanVend != null && tblOrphanVend.Length > 0)
                    {
                        tblVend = Vendors.load(tblOrphanVend[0].VendorAccount);
                        if (tblVend != null)
                        {
                            vnum = tblVend.VendorName;
                            tblVend = null;
                        }
                    }
                    tblAccounts = Accounts.load(check.VendorAccount);
                    if (tblAccounts != null)
                    {
                        missing.Add(new OrphanedTransactions() { VendorAccount = check.VendorAccount, CheckNumber = check.CheckNumber, Date = check.Date, Company = check.Company, Department = check.Department, Type = "REG", Amount = check.Amount });
                    }
                    else
                    {
                        tblAccounts = null;
                    }
                }
                tblOrphan = null;
            }
			//From the Transactions table
			tblOrphan = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Transactions WHERE AccountNumber NOT IN (SELECT Key FROM Accounts)");
			if (tblOrphan.RecordCount > 0) {
				while (!tblOrphan.EOF) {
					found = false;
					if (i == -1) {
						//ReDim missing(6, 0)
					}
					for (i = 0; i <= Information.UBound(missing, 2); i++) {
						//UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						//If IsNothing(missing(0, i)) Then
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(0, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(0, i) = tblOrphan.Fields("AccountNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(1, i) = tblOrphan.Fields("CheckNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(2, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(2, i) = Trim(tblOrphan.Fields("Description").Value) & ":" & tblOrphan.Fields("Date").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(3, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(3, i) = tblOrphan.Fields("CompanyNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(4, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(4, i) = tblOrphan.Fields("DepartmentNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(5, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(5, i) = "TRN"
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(6, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(6, i) = tblOrphan.Fields("Amount").Value
						//found = True
						break; // TODO: might not be correct. Was : Exit For
						//End If
						//If missing(0, i) = tblOrphan.Fields("AccountNumber").Value Then
						//found = True
						//Exit For
						//End If
					}
					if (!found) {
						//ReDim Preserve missing(6, i)
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(0, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(0, i) = tblOrphan.Fields("AccountNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(1, i) = tblOrphan.Fields("CheckNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(2, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(2, i) = Trim(tblOrphan.Fields("Description").Value) & ":" & tblOrphan.Fields("Date").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(3, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(3, i) = tblOrphan.Fields("CompanyNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(4, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(4, i) = tblOrphan.Fields("DepartmentNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(5, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(5, i) = "TRN"
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(6, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(6, i) = tblOrphan.Fields("Amount").Value
						if (i % 5 == 0)
							My.MyProject.Forms.frmMessage.Text = Strings.Trim(My.MyProject.Forms.frmMessage.Text) + " .";
						found = true;
					}
					tblOrphan.MoveNext();
				}
				tblOrphan.Close();
			}

			//From the Balance table
			//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			m = DateAndTime.Month(DateAndTime.Today);
			tblOrphan = WinglGlobal.GLdb.OpenRecordset("SELECT * FROM Balances WHERE AccountNumber NOT IN (SELECT AccountNumber FROM Accounts)");
			while (!tblOrphan.EOF) {
				if (tblOrphan.RecordCount > 0) {
					found = false;
					//UPGRADE_WARNING: Couldn't resolve default property of object t. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					t = 0;
					//UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					for (j = 1; j <= m; j++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object t. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						t = t + tblOrphan.Fields["C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(j, "00")].Value;
					}
					if (i == -1) {
						//ReDim missing(6, 0)
					}
					for (i = 0; i <= Information.UBound(missing, 2); i++) {
						//UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						//If IsNothing(missing(0, i)) Then
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(0, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(0, i) = tblOrphan.Fields("AccountNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(3, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(3, i) = tblOrphan.Fields("CompanyNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(4, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(4, i) = tblOrphan.Fields("DepartmentNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(5, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(5, i) = "BAL"
						//UPGRADE_WARNING: Couldn't resolve default property of object t. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(6, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(6, i) = t
						//found = True
						//Exit For
						//End If
						//If missing(0, i) = tblOrphan.Fields("AccountNumber").Value Then
						//found = True
						//Exit For
						//End If
					}
					if (!found) {
						//ReDim Preserve missing(6, i)
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(0, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(0, i) = tblOrphan.Fields("AccountNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(3, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(3, i) = tblOrphan.Fields("CompanyNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(4, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(4, i) = tblOrphan.Fields("DepartmentNumber").Value
						//UPGRADE_WARNING: Couldn't resolve default property of object missing(5, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//missing(5, i) = "BAL"
						//found = True
						//If i Mod 5 = 0 Then frmMessage.Text = Trim(frmMessage.Text) & " ."
					}
				} else {
					tblAccounts.Close();
				}
				tblOrphan.MoveNext();
			}
			tblOrphan.Close();

		}
		public void correct()
		{
			object xsql = null;

			//Dim a(2, 37) As Long
			object[] a = null;
			short j = 0;
			short r = 0;

			r = 0;
			for (j = 0; j <= XGrid1.Rows - 1; j++) {
				XGrid1.TargetCell(5, j);
				if (XGrid1.Value > -1) {
					//ReDim Preserve a(2, r)
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object a(1, r). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//a(1, r) = XGrid1.Value
					XGrid1.TargetCell(2, j);
					//UPGRADE_WARNING: Couldn't resolve default property of object XGrid1.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object a(2, r). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//a(2, r) = XGrid1.Value
					r = r + 1;
				}
			}

			if (r == 0) {
				Interaction.MsgBox("You have not associated any orphan transactions with new accounts.");
				return;
			}

			Label1.Visible = true;
			ProgressBar1.Visible = true;
			ProgressBar1.Maximum = Information.UBound(a, 2);
			for (i = 0; i <= Information.UBound(a, 2); i++) {

				//ApChecks
				Label1.Text = "Check Register";
				Label1.Refresh();
				//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblOrphan = GLdb.OpenRecordset("SELECT [Vendor/Account] FROM ApChecks WHERE [Vendor/Account] = " & a(2, i) & " AND Sequence <> 0")
				if (tblOrphan.RecordCount > 0) {
					while (!tblOrphan.EOF) {
						tblOrphan.Edit();
						//UPGRADE_WARNING: Couldn't resolve default property of object a(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//tblOrphan.Fields("Vendor/Account").Value = a(1, i)
						tblOrphan.Update();
						tblOrphan.MoveNext();
					}
					tblOrphan.Close();
				}

				//Transactions
				Label1.Text = "Transactions";
				Label1.Refresh();
				//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblAccounts = GLdb.OpenRecordset("SELECT AccountNumber FROM Transactions WHERE AccountNumber = " & a(2, i))
				if (tblAccounts.RecordCount > 0) {
					while (!tblAccounts.EOF) {
						tblAccounts.Edit();
						//UPGRADE_WARNING: Couldn't resolve default property of object a(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//tblAccounts.Fields("AccountNumber").Value = a(1, i)
						tblAccounts.Update();
						tblAccounts.MoveNext();
					}
					tblAccounts.Close();
				}

				//VendorCategories
				Label1.Text = "Vendor Categories";
				Label1.Refresh();
				//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblAccounts = GLdb.OpenRecordset("SELECT Account FROM VendorCategories WHERE Account = " & a(2, i))
				if (tblAccounts.RecordCount > 0) {
					while (!tblAccounts.EOF) {
						tblAccounts.Edit();
						//UPGRADE_WARNING: Couldn't resolve default property of object a(1, i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//tblAccounts.Fields("Account").Value = a(1, i)
						tblAccounts.Update();
						tblAccounts.MoveNext();
					}
					tblAccounts.Close();
				}

				//Balances
				Label1.Text = "Balances";
				Label1.Refresh();
				//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblAccounts = GLdb.OpenRecordset("SELECT * FROM Balances WHERE AccountNumber = " & a(2, i))
				if (tblAccounts.RecordCount > 0) {
					while (!tblAccounts.EOF) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + tblAccounts.Fields["CompanyNumber"].Value;
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + " AND DepartmentNumber = " + tblAccounts.Fields["DepartmentNumber"].Value;
						//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = xsql & " AND AccountNumber = " & a(1, i)
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tblOrphan = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tblOrphan.RecordCount > 0) {
							tblOrphan.Edit();
							for (j = 3; j <= 14; j++) {
								tblOrphan.Fields[j].Value = tblOrphan.Fields[j].Value + tblAccounts.Fields[j].Value;
							}
							tblOrphan.Update();
							tblOrphan.Close();
						}
						tblAccounts.Delete();
						tblAccounts.MoveNext();
					}
					tblAccounts.Close();
				}

				//Budgets
				Label1.Text = "Budgets";
				Label1.Refresh();
				//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//tblAccounts = GLdb.OpenRecordset("SELECT * FROM Budgets WHERE AccountNumber = " & a(2, i))
				if (tblAccounts.RecordCount > 0) {
					while (!tblAccounts.EOF) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "SELECT * FROM Budgets WHERE CompanyNumber = " + tblAccounts.Fields["CompanyNumber"].Value;
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + " AND DepartmentNumber = " + tblAccounts.Fields["DepartmentNumber"].Value;
						//UPGRADE_WARNING: Couldn't resolve default property of object a(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//xsql = xsql & " AND AccountNumber = " & a(1, i)
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						tblOrphan = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tblOrphan.RecordCount > 0) {
							tblOrphan.Edit();
							for (j = 15; j <= 26; j++) {
								tblOrphan.Fields[j].Value = tblOrphan.Fields[j].Value + tblAccounts.Fields[j].Value;
							}
							tblOrphan.Update();
							tblOrphan.Close();
						}
						tblAccounts.Delete();
						tblAccounts.MoveNext();
					}
					tblAccounts.Close();
				}
				ProgressBar1.Value = i;
				ProgressBar1.Refresh();
			}
			a = null;
			LogError.MsgBox_Renamed("Updates are Done!");

		}

        public class OrphanedTransactions
        {
            public int VendorAccount { get; set; }
            public int CheckNumber { get; set; }
            public DateTime Date { get; set; }
            public int Company { get; set; }
            public int Department { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }

        }
	}
}
