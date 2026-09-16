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
	internal partial class Categories : System.Windows.Forms.Form
	{
			// Holds the index of a Node
		short mnIndex;
			// Flag that signals a Drag Drop operation.
		bool mbIndrag;
			// Item that is being dragged.
		object moDragNode;
		bool nodesChanged;
		bool acctChanged;
		short nodeSeq;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object xsql = null;

			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			i = lstCategories.SelectedNode.Index;
			if (Err().Number == 91) {
				Interaction.MsgBox("You must first select a base or sub category");
				return;
			}

			 // ERROR: Not supported in C#: OnErrorStatement

			switch (Index) {

				case 0:
					// EDIT Category
					if (lstCategories.SelectedNode.Parent == null) {
						Interaction.MsgBox("You cannot EDIT a base category");
						goto Cmd_Click_exit;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Category WHERE Key = '" + lstCategories.SelectedNode.Name + "'";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblCategory = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (WinglGlobal.tblCategory.RecordCount > 0) {
						WinglGlobal.AddFlag = false;
						My.MyProject.Forms.CategoryDetail.Show();
						nodesChanged = true;
					}
					break;

				case 1:
					// ADD Category
					WinglGlobal.AddFlag = true;
					My.MyProject.Forms.CategoryDetail.Show();
					nodesChanged = true;
					break;

				case 2:
					// DELETE Category
					if (lstCategories.SelectedNode.Parent == null) {
						Interaction.MsgBox("You cannot DELETE a base category");
						goto Cmd_Click_exit;
					}
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Accounts WHERE MinorCategory = '" + lstCategories.SelectedNode.Name + "'";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblAccounts = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (WinglGlobal.tblAccounts.RecordCount > 0) {
						Interaction.MsgBox("Category in use by Account Master.  Cannot be deleted.");
						goto Cmd_Click_exit;
					}
					if (LogError.MsgBox_Renamed("Delete Category: '" + lstCategories.SelectedNode.Text + "'", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Categories") == MsgBoxResult.Yes) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "DELETE * FROM Category WHERE Key = '" + lstCategories.SelectedNode.Name + "'";
						WinglGlobal.GLdb.Execute((xsql));
						get_nodes();
						nodesChanged = true;
					}
					break;

				case 3:
					// EDIT Account
					if (lstAccounts.SelectedIndex < 0) {
						Interaction.MsgBox("You must select an account to edit.");
						goto Cmd_Click_exit;
					}
					WinglGlobal.AddFlag = false;
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Category WHERE Key = '" + lstCategories.SelectedNode.Name + "'";
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblCategory = WinglGlobal.GLdb.OpenRecordset(xsql);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = "SELECT * FROM Accounts WHERE MajorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 52, 4);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + "' AND MinorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 56, 4);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xsql = xsql + "' AND [Account Number] = " + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 60);
					//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.tblAccounts = WinglGlobal.GLdb.OpenRecordset(xsql, , DAO.LockTypeEnum.dbOptimistic);
					if (WinglGlobal.tblAccounts.RecordCount > 0) {
						My.MyProject.Forms.AccountDetail.Show();
					}
					break;

				case 4:
					// ADD Account
					WinglGlobal.AddFlag = true;
					My.MyProject.Forms.AccountDetail.Show();
					break;

				case 5:
					// DELETE Account
					if (lstAccounts.SelectedIndex < 0) {
						Interaction.MsgBox("You must select an account to delete.");
						goto Cmd_Click_exit;
					}
					if (LogError.MsgBox_Renamed("Delete Account: '" + Strings.Trim(Strings.Left(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 50)) + "'", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Accounts") == MsgBoxResult.Yes) {
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "DELETE * FROM Accounts WHERE MajorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 52, 4);
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + "' AND MinorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 56, 4);
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = xsql + "' AND [Account Number] = " + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 60);
						WinglGlobal.GLdb.Execute((xsql));
						get_Accounts(ref Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, lstAccounts.SelectedIndex), 56, 4));
					}
					break;

			}

			goto Cmd_Click_exit;
			Cmd_Click_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, "Categories");
			 // ERROR: Not supported in C#: ResumeStatement

			Cmd_Click_exit:


		}
		private void cmdClose_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Close();

		}
		private void Categories_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Top = 0;
			this.Left = 0;
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6780);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(9180);
			get_nodes();
			nodesChanged = false;

		}
		public void get_nodes()
		{

			System.Windows.Forms.TreeNode nodex = null;
			string ky = null;
			string desc = null;
			short subof = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			lstCategories.Nodes.Clear();
			//Clear the TreeView of any nodes
			WinglGlobal.tblCategory = WinglGlobal.GLdb.OpenRecordset("Category");
			//make sure there are records in the table
			if (WinglGlobal.tblCategory.RecordCount > 0) {
				WinglGlobal.tblCategory.MoveFirst();
				while (WinglGlobal.tblCategory.EOF == false) {
					if (Conversion.Val(WinglGlobal.tblCategory.Fields["Key"].Value) > WinglGlobal.HighKey)
						WinglGlobal.HighKey = Conversion.Val(WinglGlobal.tblCategory.Fields["Key"].Value);
					//All root nodes have 0_ in the parent field
					if (Strings.Trim(WinglGlobal.tblCategory.Fields["SubOf"].Value) == "0_") {
						nodex = lstCategories.Nodes.Add(Strings.Trim(WinglGlobal.tblCategory.Fields["Key"].Value), Strings.Trim(WinglGlobal.tblCategory.Fields["CategoryDescription"].Value));
					//All child nodes will have the parent key stored in the parent field
					} else {
						//UPGRADE_WARNING: Add method behavior has changed Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DBD08912-7C17-401D-9BE9-BA85E7772B99"'
						nodex = lstCategories.Nodes.Find(Strings.Trim(WinglGlobal.tblCategory.Fields["SubOf"].Value), true)[0].Nodes.Add(Strings.Trim(WinglGlobal.tblCategory.Fields["Key"].Value), Strings.Trim(WinglGlobal.tblCategory.Fields["CategoryDescription"].Value));
						nodex.EnsureVisible();
						//expend the TreeView so all nodes are visible
					}
					WinglGlobal.tblCategory.MoveNext();
				}
			}

			goto get_nodes_exit;
			get_nodes_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			get_nodes_exit:


		}
		private void Categories_FormClosing(System.Object eventSender, System.Windows.Forms.FormClosingEventArgs eventArgs)
		{
			bool Cancel = eventArgs.Cancel;
			System.Windows.Forms.CloseReason UnloadMode = eventArgs.CloseReason;

			if (acctChanged)
				lstAccounts_Leave(lstAccounts, new System.EventArgs());
			if (nodesChanged)
				WriteToTable();

			eventArgs.Cancel = Cancel;
		}
//UPGRADE_WARNING: Event Categories.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void Categories_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			//On Error Resume Next
			//lstCategories.Width = Me.Width - 1620
			//lstCategories.Height = Me.Height - 435

			//For i = 0 To Cmd.Count - 1
			//    Cmd(i).Left = Me.Width - 1515
			//Next
			//cmdClose.Left = Me.Width - 1455

		}
		private void lstAccounts_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
		{

			Cmd_Click(Cmd[3], new System.EventArgs());

		}
		private void lstAccounts_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			short i = 0;
			string x = null;
			string xsql = null;
			DAO.Recordset tbl = null;

			if (acctChanged) {
				x = Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, 0), 56, 4);
				xsql = "SELECT * FROM Accounts WHERE MinorCategory = '" + x + "'";
				tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tbl.RecordCount > 0) {
					while (!tbl.EOF) {
						tbl.Edit();
						tbl.Fields["Account Number"].Value = tbl.Fields["Account Number"].Value * -1;
						tbl.Update();
						tbl.MoveNext();
					}
					tbl.Close();
					for (i = 0; i <= lstAccounts.Items.Count - 1; i++) {
						xsql = "SELECT * FROM Accounts WHERE MajorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i), 52, 4);
						xsql = xsql + "' AND MinorCategory = '" + Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i), 56, 4);
						xsql = xsql + "' AND [Account Number] = " + Conversion.Val(Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i), 60)) * -1;
						tbl = WinglGlobal.GLdb.OpenRecordset(xsql, , DAO.LockTypeEnum.dbOptimistic);
						if (tbl.RecordCount > 0) {
							tbl.Edit();
							//tbl("Description") = Trim(Left(lstAccounts.List(i), 50))
							//tbl("MinorCategory") = x
							//tbl("MajorCategory") = Mid(lstAccounts.List(i), 52, 4)
							tbl.Fields["Account Number"].Value = i + 1;
							tbl.Update();
							tbl.Close();
						}
					}
				}
				get_Accounts(ref x);
				acctChanged = false;
			}

		}
		private void lstCategories_AfterLabelEdit(System.Object eventSender, System.Windows.Forms.NodeLabelEditEventArgs eventArgs)
		{
			bool Cancel = eventArgs.CancelEdit;
			string NewString = eventArgs.Label;

			nodesChanged = true;

		}
		private void lstCategories_BeforeLabelEdit(System.Object eventSender, System.Windows.Forms.NodeLabelEditEventArgs eventArgs)
		{
			bool Cancel = eventArgs.CancelEdit;

			if (lstCategories.SelectedNode.Parent == null) {
				Interaction.MsgBox("Cannot Edit BASE Category.");
				Cancel = 1;
			}

		}
//UPGRADE_ISSUE: MSComctlLib.TreeView event lstCategories.DragDrop was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
		private void lstCategories_DragDrop(ref System.Windows.Forms.Control Source, ref float x, ref float y)
		{
			// If user didn't move mouse or released it over an invalid area.

			//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//lstCategories.DropHighlight Is Nothing Then
			if (0) {
				mbIndrag = false;
				return;
			} else {
				// Set dragged node's parent property to the target node.
				 // ERROR: Not supported in C#: OnErrorStatement

				// To prevent circular errors.
				//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//UPGRADE_WARNING: Couldn't resolve default property of object moDragNode.Parent. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//moDragNode.Parent = lstCategories.DropHighlight
				//UPGRADE_ISSUE: Form method Categories.Cls was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//Cls()
				//UPGRADE_WARNING: Couldn't resolve default property of object moDragNode.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//UPGRADE_ISSUE: Form method Categories.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//Print(lstCategories.DropHighlight.Text & " is parent of " & moDragNode.Text)
				// Release the DropHighlight reference.
				//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//UPGRADE_NOTE: Object lstCategories.DropHighlight may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				//lstCategories.DropHighlight = Nothing
				mbIndrag = false;
				//UPGRADE_NOTE: Object moDragNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				moDragNode = null;
				nodesChanged = true;
				return;
				// Exit if no errors occured.
			}
			checkerror:

			// Define constants to represent Visual Basic errors code.
			const int CircularError = 35614;
			string msg = null;
			if (Err().Number == CircularError) {
				msg = "A node can't be made a child of its own children.";

				// Display the message box with an exclamation mark icon
				// and with OK and Cancel buttons.
				if (Interaction.MsgBox(msg, Convert.ToInt16(MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel)) == MsgBoxResult.Ok) {
					// Release the DropHighlight reference.
					mbIndrag = false;
					//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
					//UPGRADE_NOTE: Object lstCategories.DropHighlight may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					//lstCategories.DropHighlight = Nothing
					return;
				}
			}

		}
//UPGRADE_ISSUE: MSComctlLib.TreeView event lstCategories.DragOver was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
		private void lstCategories_DragOver(ref System.Windows.Forms.Control Source, ref float x, ref float y, ref short State)
		{
			if (mbIndrag == true) {
				// Set DropHighlight to the mouse's coordinates.
				//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//lstCategories.DropHighlight = lstCategories.GetNodeAt(x, y)
			}
		}
		private void lstCategories_MouseDown(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
		{
			short Button = eventArgs.Button / 0x100000;
			short Shift = System.Windows.Forms.Control.ModifierKeys / 0x10000;
			float x = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(eventArgs.X);
			float y = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(eventArgs.Y);
			//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//lstCategories.DropHighlight = lstCategories.GetNodeAt(x, y)
			//Make sure we are over a Node
			//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//If Not lstCategories.DropHighlight Is Nothing Then
			//Set the Node we are on to be the selected Node
			//if we don't do this it will not be the selected node
			//until we finish clicking on the Node
			//lstCategories.SelectedNode = lstCategories.GetNodeAt(x, y)
			//moDragNode = lstCategories.SelectedNode ' Set the item being dragged.
			//End If
			//UPGRADE_ISSUE: MSComctlLib.TreeView property lstCategories.DropHighlight was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//UPGRADE_NOTE: Object lstCategories.DropHighlight may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			//lstCategories.DropHighlight = Nothing
		}
		private void lstCategories_MouseMove(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
		{
			short Button = eventArgs.Button / 0x100000;
			short Shift = System.Windows.Forms.Control.ModifierKeys / 0x10000;
			float x = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(eventArgs.X);
			float y = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(eventArgs.Y);
			// Signal a Drag operation.
			if (Button == Microsoft.VisualBasic.Compatibility.VB6.MouseButtonConstants.LeftButton) {
				if (lstCategories.SelectedNode.Parent == null) {
					Interaction.MsgBox("Cannot move Base category");
					return;
				}
				mbIndrag = true;
				// Set the flag to true.
				// Set the drag icon with the CreateDragImage method.
				//lstCategories.DragIcon = lstCategories.SelectedItem.CreateDragImage
				//UPGRADE_ISSUE: Constant vbBeginDrag was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
				//UPGRADE_ISSUE: MSComctlLib.TreeView method lstCategories.Drag was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				//lstCategories.Drag(vbBeginDrag) ' Drag operation.
			}

		}
		public void WriteToTable()
		{
			//Writes the Node information from the TreeView into a table
			short i = 0;
			short iTmp = 0;
			short iIndex = 0;

			//If tblCategory.RecordCount > 0 Then
			// Delete any records that may be in the table
			//    tblCategory.MoveFirst
			//    Do While tblCategory.EOF = False
			//        tblCategory.Delete
			//        tblCategory.MoveNext
			//    Loop
			//End If

			WinglGlobal.GLdb.Execute(("DELETE * FROM Category"));
			nodeSeq = 1;

			GetFirstParent();
			//Find a root node in the treeview
			//get the index of the root node that is at the top of the treeview
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//UPGRADE_ISSUE: MSComctlLib.Node property lstCategories.Nodes.FirstSibling was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//iIndex = lstCategories.Nodes.Item(mnIndex).FirstSibling.Index
			iTmp = iIndex;
			WinglGlobal.tblCategory.AddNew();
			WinglGlobal.tblCategory.Fields["SubOf"].Value = "0_";
			//this is a root node
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			WinglGlobal.tblCategory.Fields["Key"].Value = lstCategories.Nodes[iIndex].Name;
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			WinglGlobal.tblCategory.Fields["CategoryDescription"].Value = lstCategories.Nodes[iIndex].Text;
			WinglGlobal.tblCategory.Fields["Sequence"].Value = nodeSeq;
			nodeSeq = nodeSeq + 1;
			WinglGlobal.tblCategory.Update();
			//If the Node has Children call the sub that writes the children
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			if (lstCategories.Nodes[iIndex].GetNodeCount(false) > 0) {
				WriteChild(iIndex);
			}

			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//UPGRADE_ISSUE: MSComctlLib.Node property lstCategories.Nodes.LastSibling was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//lstCategories.Nodes.Item(iTmp).LastSibling.Index
			while (iIndex != 0) {
				//loop through all the root nodes
				WinglGlobal.tblCategory.AddNew();
				WinglGlobal.tblCategory.Fields["SubOf"].Value = "0_";
				//this is a root node
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				WinglGlobal.tblCategory.Fields["Key"].Value = lstCategories.Nodes[iIndex].NextNode.Name;
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				WinglGlobal.tblCategory.Fields["CategoryDescription"].Value = lstCategories.Nodes[iIndex].NextNode.Text;
				WinglGlobal.tblCategory.Fields["Sequence"].Value = nodeSeq;
				nodeSeq = nodeSeq + 1;
				WinglGlobal.tblCategory.Update();
				//If the Node has Children call the sub that writes the children
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				if (lstCategories.Nodes[iIndex].NextNode.GetNodeCount(false) > 0) {
					//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					WriteChild(lstCategories.Nodes[iIndex].NextNode.Index);
				}
				// Move to the Next root Node
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				iIndex = lstCategories.Nodes[iIndex].NextNode.Index;
			}
		}
		private void WriteChild(short iNodeIndex)
		{
			// Write the child nodes to the table. This sub uses recursion
			// to loop through the child nodes. It receives the Index of
			// the node that has the children
			short i = 0;
			short iTempIndex = 0;
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//UPGRADE_ISSUE: MSComctlLib.Node property lstCategories.Nodes.Child.FirstSibling was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			//iTempIndex = lstCategories.Nodes.Item(iNodeIndex).FirstNode.FirstSibling.Index
			//Loop through all a Parents Child Nodes
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			for (i = 1; i <= lstCategories.Nodes[iNodeIndex].GetNodeCount(false); i++) {
				WinglGlobal.tblCategory.AddNew();
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				WinglGlobal.tblCategory.Fields["SubOf"].Value = lstCategories.Nodes[iTempIndex].Parent.Name;
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				WinglGlobal.tblCategory.Fields["Key"].Value = lstCategories.Nodes[iTempIndex].Name;
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				WinglGlobal.tblCategory.Fields["CategoryDescription"].Value = lstCategories.Nodes[iTempIndex].Text;
				WinglGlobal.tblCategory.Fields["Sequence"].Value = nodeSeq;
				nodeSeq = nodeSeq + 1;
				WinglGlobal.tblCategory.Update();
				// If the Node we are on has a child call the Sub again
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				if (lstCategories.Nodes[iTempIndex].GetNodeCount(false) > 0) {
					WriteChild((iTempIndex));
				}
				// If we are not on the last child move to the next child Node
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				if (i != lstCategories.Nodes[iNodeIndex].GetNodeCount(false)) {
					//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					iTempIndex = lstCategories.Nodes[iTempIndex].NextNode.Index;
				}
			}
		}
		private void GetFirstParent()
		{
			//Find the first parent node in the TreeView
			 // ERROR: Not supported in C#: OnErrorStatement

			short i = 0;
			short nTmp = 0;

			for (i = 1; i <= lstCategories.Nodes.Count; i++) {
				//This will give an error if there is no parent
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				nTmp = lstCategories.Nodes[i].Parent.Index;
			}
			return;
			myerr:

			mnIndex = i;
			return;
		}
		private string GetNextKey()
		{
			string functionReturnValue = null;
			//Returns a new key value for each Node being added to the TreeView
			//This algorithm is very simple and will limit you to adding a total of 999 nodes
			//Each node needs a unique key. If you allow users to remove Nodes you can't use
			//the Nodes count +1 as the key for a new node.

			string sNewKey = null;
			short iHold = 0;
			short i = 0;
			 // ERROR: Not supported in C#: OnErrorStatement

			//The next line will return error #35600 if there are no Nodes in the TreeView
			//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			iHold = Conversion.Val(lstCategories.Nodes[1].Name);
			for (i = 1; i <= lstCategories.Nodes.Count; i++) {
				//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				if (Conversion.Val(lstCategories.Nodes[i].Name) > iHold) {
					//UPGRADE_WARNING: Lower bound of collection lstCategories.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					iHold = Conversion.Val(lstCategories.Nodes[i].Name);
				}
			}
			iHold = iHold + 1;
			sNewKey = Convert.ToString(iHold) + "_";
			functionReturnValue = sNewKey;
			return functionReturnValue;
			myerr:
			//Return a unique key
			//Because the TreeView is empty return a 1 for the key of the first Node
			functionReturnValue = "1_";
			return functionReturnValue;
			return functionReturnValue;
		}
		public void get_Accounts(ref string c)
		{

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			string xsql = null;
			DAO.Recordset tbl = null;

			lstAccounts.Items.Clear();

			xsql = "SELECT * FROM Accounts WHERE [MinorCategory] = '" + c + "' ORDER BY [Account Number]";
			tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tbl.RecordCount > 0) {
				while (!tbl.EOF) {
					lstAccounts.Items.Add((tbl.Fields["Active"].Value ? "* " : "  ") + WinGlobal.rtpad(ref tbl.Fields["Description"].Value, ref 49) + tbl.Fields["MajorCategory"].Value + tbl.Fields["MinorCategory"].Value + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl.Fields["Account Number"].Value, "#####"));
					tbl.MoveNext();
				}
				tbl.Close();
			}

		}
		private void lstCategories_NodeClick(System.Object eventSender, System.Windows.Forms.TreeNodeMouseClickEventArgs eventArgs)
		{
			System.Windows.Forms.TreeNode Node = eventArgs.Node;

			lstAccounts_Leave(lstAccounts, new System.EventArgs());
			get_Accounts(ref lstCategories.SelectedNode.Name);

		}
//UPGRADE_ISSUE: VBControlExtender event UpDown1.DownClick was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
		private void UpDown1_DownClick()
		{

			short i = 0;
			string x = null;

			i = lstAccounts.SelectedIndex;

			if (i >= 0 & i < lstAccounts.Items.Count - 1) {
				x = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i + 1);
				Microsoft.VisualBasic.Compatibility.VB6.Support.SetItemString(lstAccounts, i + 1, Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i));
				Microsoft.VisualBasic.Compatibility.VB6.Support.SetItemString(lstAccounts, i, x);
				lstAccounts.SelectedIndex = i + 1;
				lstAccounts.Refresh();
				acctChanged = true;
			}

		}
//UPGRADE_ISSUE: VBControlExtender event UpDown1.UpClick was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
		private void UpDown1_UpClick()
		{

			short i = 0;
			string x = null;

			i = lstAccounts.SelectedIndex;

			if (i > 0) {
				x = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i - 1);
				Microsoft.VisualBasic.Compatibility.VB6.Support.SetItemString(lstAccounts, i - 1, Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstAccounts, i));
				Microsoft.VisualBasic.Compatibility.VB6.Support.SetItemString(lstAccounts, i, x);
				lstAccounts.SelectedIndex = i - 1;
				lstAccounts.Refresh();
				acctChanged = true;
			}

		}
	}
}
