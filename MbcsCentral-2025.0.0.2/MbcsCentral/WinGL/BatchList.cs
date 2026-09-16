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
	internal partial class BatchList : System.Windows.Forms.Form
	{
//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			//******************************************************************************

			//UPGRADE_WARNING: Arrays in structure tbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tbl = null;

			switch (Index) {

				case 0:
					// OK
					WinglGlobal.BatchListName = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstReports, lstReports.SelectedIndex);
					WinglGlobal.BatchDateAdj = chkDates.CheckState;
					break;
				case 1:
					// Cancel
					WinglGlobal.BatchListName = "";
					break;
				case 2:
					// DELETE
					if (lstReports.SelectedIndex > -1) {
						WinglGlobal.BatchListName = Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstReports, lstReports.SelectedIndex);
						if (Interaction.MsgBox("Delete this Report List?", MsgBoxStyle.YesNo, this.Text) == MsgBoxResult.Yes) {
							//Set tbl = GLdb.OpenRecordset("SELECT FilePath FROM BatchRptSave WHERE ListName = '" & Trim(BatchListName) & "'")
							//If tbl.RecordCount > 0 Then
							//If Dir(tbl("FilePath")) <> "" Then Kill tbl("FilePath")
							//tbl.Close
							//UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (!string.IsNullOrEmpty(FileSystem.Dir(WinGL.My.MyProject.Application.Info.DirectoryPath + "/" + WinglGlobal.BatchListName + ".brs")))
								FileSystem.Kill(WinGL.My.MyProject.Application.Info.DirectoryPath + "/" + WinglGlobal.BatchListName + ".brs");
							//End If
							//GLdb.Execute ("DELETE * FROM BatchRptSave WHERE ListName = '" & Trim(BatchListName) & "'")
							lstReports.Items.RemoveAt(lstReports.SelectedIndex);
							lstReports.Refresh();
						}
					} else {
						Interaction.MsgBox("You must select a Report List to delete.", , this.Text);
					}
					return;

					break;
				case 3:
					// ADD
					WinglGlobal.BatchListName = Strings.Trim(Interaction.InputBox("Name of New Batch List:", "Batch Reporting List", "", Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2), Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2)));
					break;
			}

			this.Close();

		}
//******************************************************************************
		private void BatchList_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			//Dim tbl As Recordset
			string x = null;
			short fi = 0;

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / 2));
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2) - (Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) / 2));
			//Set tbl = GLdb.OpenRecordset("BatchRptSave")
			//UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			x = FileSystem.Dir(WinGL.My.MyProject.Application.Info.DirectoryPath + "/*.brs");
			//If tbl.RecordCount > 0 Then
			//While Not tbl.EOF
			while (!string.IsNullOrEmpty(x)) {
				//lstReports.AddItem tbl("ListName")
				//tbl.MoveNext
				lstReports.Items.Add(Strings.Left(x, Strings.Len(x) - 4));
				//UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				x = FileSystem.Dir();
			}
			//Wend
			//tbl.Close
			//End If

			WinglGlobal.BatchListName = "";

		}
//******************************************************************************
		private void lstReports_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
		{
			//******************************************************************************

			Cmd_Click(Cmd[0], new System.EventArgs());

		}
	}
}
