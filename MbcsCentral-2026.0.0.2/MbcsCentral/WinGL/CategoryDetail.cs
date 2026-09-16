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
	internal partial class CategoryDetail : System.Windows.Forms.Form
	{
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// SAVE
					if (lstCategories.SelectedIndex < 0) {
						Interaction.Beep();
						LogError.MsgBox_Renamed("You must set the Sub-Category");
						return;
					}
					if (WinglGlobal.AddFlag) {
						WinglGlobal.tblCategory.AddNew();
						WinglGlobal.HighKey = WinglGlobal.HighKey + 1;
						WinglGlobal.tblCategory.Fields["Key"].Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinglGlobal.HighKey, "000") + "_";
					} else {
						WinglGlobal.tblCategory.Edit();
					}
					//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					WinglGlobal.tblCategory.Fields["CategoryDescription"].Value = Strings.Trim(txtDescription.CtlText);
					WinglGlobal.tblCategory.Fields["SubOf"].Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(Strings.Mid(lstCategories.Text, 129, 4)), "000") + "_";
					WinglGlobal.tblCategory.Update();
					break;

				case 1:
					// EXIT
					break;

			}

			My.MyProject.Forms.Categories.get_nodes();
			this.Close();

		}
		private void CategoryDetail_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			System.Windows.Forms.TreeNode nodex = null;
			short i = 0;
			short n = 0;

			this.Top = 0;
			this.Left = 0;
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(2670);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(7995);
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
				}
			} else {
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				txtDescription.CtlText = WinglGlobal.tblCategory.Fields["CategoryDescription"].Value;
				for (i = 0; i <= lstCategories.Items.Count - 1; i++) {
					if (Strings.Mid(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i), 129, 4) == WinglGlobal.tblCategory.Fields["SubOf"].Value) {
						lstCategories.SelectedIndex = i;
						lstCategories.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstCategories, i);
						break; // TODO: might not be correct. Was : Exit For
					}
				}
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
