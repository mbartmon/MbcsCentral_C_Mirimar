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
	internal partial class AccountDetail : System.Windows.Forms.Form
	{
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			string x = null;
			string xsql = null;
			short j = 0;
			short i = 0;
			short c = 0;
			System.Windows.Forms.ListBox lst = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			switch (Index) {

				case 0:
					// SAVE
					if (lstCategories.SelectedIndex < 0) {
						Interaction.Beep();
						Interaction.MsgBox("You must select a Category for this Account.");
						goto Cmd_Click_exit;
					}
					if (WinglGlobal.AddFlag) {
						lst = My.MyProject.Forms.Categories.lstAccounts;
						i = lst.SelectedIndex;
						c = lst.Items.Count;
						x = Strings.Mid(lstCategories.Text, 129);
						if (c > 0) {
							if (i >= 0) {
								if (i == c - 1) {
									//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
									lst.Items.Add(WinGlobal.rtpad(ref (txtDescription.CtlText), ref 51) + WinglGlobal.get_majorCategory(ref Strings.Mid(lstCategories.Text, 129)) + x + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i + 1, "#####"));
									i = -1;
								} else {
									lst.Items.Add(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lst, c - 1));
									for (j = c - 1; j >= i; j += -1) {
										Microsoft.VisualBasic.Compatibility.VB6.Support.SetItemString(lst, j, Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lst, j - 1));
									}
								}
							} else {
								i = c;
							}
						} else {
							i = 0;
						}
						//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
						if (i >= 0)
							lst.Items.Insert(c, WinGlobal.rtpad(ref (txtDescription.CtlText), ref 51) + WinglGlobal.get_majorCategory(ref x) + x + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(i, "#####"));
						//xsql = "DELETE * FROM Accounts WHERE MinorCategory = '" & x & "'"
						//GLdb.Execute (xsql)
						WinglGlobal.tblAccounts = WinglGlobal.GLdb.OpenRecordset("Accounts");
						//For j = 0 To lst.ListCount - 1
						WinglGlobal.tblAccounts.AddNew();
						WinglGlobal.tblAccounts.Fields["Description"].Value = Strings.Trim(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lst, i), 50));
						WinglGlobal.tblAccounts.Fields["MinorCategory"].Value = Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lst, i), 56, 4);
						WinglGlobal.tblAccounts.Fields["MajorCategory"].Value = Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lst, i), 52, 4);
						WinglGlobal.tblAccounts.Fields["Account Number"].Value = c + 1;
						WinglGlobal.tblAccounts.Fields["CategoryPath"].Value = Strings.Trim(Strings.Left(lstCategories.Text, 128));
						WinglGlobal.tblAccounts.Fields["Active"].Value = (chkActive.CheckState == 0 ? false : true);
						WinglGlobal.tblAccounts.Update();
						//Next
					} else {
						WinglGlobal.tblAccounts.Edit();
						x = WinglGlobal.tblAccounts.Fields["MinorCategory"].Value;
						//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
						WinglGlobal.tblAccounts.Fields["Description"].Value = txtDescription.CtlText;
						WinglGlobal.tblAccounts.Fields["MinorCategory"].Value = Strings.Mid(lstCategories.Text, 129);
						WinglGlobal.tblAccounts.Fields["MajorCategory"].Value = WinglGlobal.get_majorCategory(ref WinglGlobal.tblAccounts.Fields["MinorCategory"].Value);
						WinglGlobal.tblAccounts.Fields["CategoryPath"].Value = Strings.Trim(Strings.Left(lstCategories.Text, 128));
						WinglGlobal.tblAccounts.Fields["Active"].Value = (chkActive.CheckState == 0 ? false : true);
						WinglGlobal.tblAccounts.Update();
					}
					My.MyProject.Forms.Categories.get_Accounts(ref x);
					break;

				case 1:
					// CANCEL
					break;

			}
			this.Close();
			goto Cmd_Click_exit;
			Cmd_Click_error:

			if (Err().Number == 3022 & WinglGlobal.AddFlag) {
				WinglGlobal.tblAccounts.Fields["Account Number"].Value = WinglGlobal.tblAccounts.Fields["Account Number"].Value + 1;
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Account Detail");
				 // ERROR: Not supported in C#: ResumeStatement

			}
			Cmd_Click_exit:


		}
		private void AccountDetail_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			System.Windows.Forms.TreeNode nodex = null;
			short i = 0;

			this.Top = 0;
			this.Left = 0;
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(2655);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(6855);
			for (i = 1; i <= My.MyProject.Forms.Categories.lstCategories.Nodes.Count; i++) {
				//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				nodex = My.MyProject.Forms.Categories.lstCategories.Nodes[i];
				if (nodex.Parent == null) {
					//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					lstCategories.Items.Add(WinGlobal.rtpad(ref Strings.Trim(My.MyProject.Forms.Categories.lstCategories.Nodes[i].FullPath), ref 128) + My.MyProject.Forms.Categories.lstCategories.Nodes[i].Name);
					if (nodex.GetNodeCount(false)) {
						get_children(ref nodex);
					}
				}
			}
			if (WinglGlobal.AddFlag) {
				for (i = 0; i <= lstCategories.Items.Count - 1; i++) {
					if (Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i), 129, 4) == My.MyProject.Forms.Categories.lstCategories.SelectedNode.Name) {
						lstCategories.SelectedIndex = i;
						lstCategories.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i);
						break; // TODO: might not be correct. Was : Exit For
					}
					chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
				}
				lstCategories.Enabled = false;
			} else {
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtDescription.CtlText = WinglGlobal.tblAccounts.Fields["Description"].Value;
				for (i = 0; i <= lstCategories.Items.Count - 1; i++) {
					if (Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i), 129, 4) == WinglGlobal.tblCategory.Fields["Key"].Value) {
						lstCategories.SelectedIndex = i;
						lstCategories.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i);
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				chkActive.CheckState = (WinglGlobal.tblAccounts.Fields["Active"].Value ? 1 : 0);
			}

		}
		public void get_children(ref System.Windows.Forms.TreeNode nodex)
		{

			short n = 0;

			lstCategories.Items.Add(WinGlobal.rtpad(ref Strings.Trim(nodex.FirstNode.FullPath), ref 128) + nodex.FirstNode.Name);
			n = nodex.FirstNode.Index;
			//UPGRADE_ISSUE: MSComctlLib.Node property Child.LastSibling was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//While n <> nodex.FirstNode.LastSibling.Index
			//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//lstCategories.Items.Add(rtpad(Trim(Categories.lstCategories.Nodes.Item(n).NextNode.FullPath), 128) & Categories.lstCategories.Nodes.Item(n).NextNode.Name)
			//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//If Categories.lstCategories.Nodes.Item(n).GetNodeCount(False) Then
			//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//Call get_children(Categories.lstCategories.Nodes.Item(n))
			//End If
			//UPGRADE_WARNING: Lower bound of collection Categories.lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//n = Categories.lstCategories.Nodes.Item(n).NextNode.Index
			//End While

		}
	}
}
