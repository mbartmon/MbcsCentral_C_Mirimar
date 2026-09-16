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
	internal partial class rptMaster : System.Windows.Forms.Form
	{
		short ln;
		short l;
		short pg;
		bool dest;
		string DefaultPrinter;
		public object rptObject;
		public object ctlObject;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			dynamic Printer = null;
			//As New Printer

			string x = null;
			//Dim prt As Printer

			switch (Index) {

				case 0:
					// print
					x = lstPrinters.Text;
					//For	Each prt In Printers
					//If prt.DeviceName = x Then
					//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.Device. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//ctlObject.Device = prt
					//Exit For
					//End If
					//Next prt
					DefaultPrinter = Printer.DeviceName;
					//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.CondensePrint. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					ctlObject.CondensePrint = (Check1.CheckState == 1 ? true : false);
					//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.Target. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					ctlObject.Target = 1;

					//UPGRADE_WARNING: Couldn't resolve default property of object rptObject.print_report. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					rptObject.print_report();

					break;
				case 1:
					// close
					this.Close();
					//UPGRADE_NOTE: Object ctlObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					ctlObject = null;

					break;
				case 2:
					// font smaller
					lstView.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(lstView.Font, lstView.Font.SizeInPoints - 1);
					//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					lstView.Refresh();
					rptMaster_Resize(this, new System.EventArgs());

					break;
				case 3:
					// font larger
					lstView.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(lstView.Font, lstView.Font.SizeInPoints + 1);
					//UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
					//lstView.CtlRefresh()
					rptMaster_Resize(this, new System.EventArgs());

					break;
			}

		}
//UPGRADE_WARNING: Form event rptMaster.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void rptMaster_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			dynamic x = null;
			//As Printer

			//For	Each x In Printers
			//lstPrinters.Items.Add(x.DeviceName)
			//Next x

			DefaultPrinter = Interaction.GetSetting("WinGL", "Printers", this.Text, Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstPrinters, 0));
			Check1.CheckState = (Interaction.GetSetting("WinGL", "Fonts", this.Text, Convert.ToString(0)) == "0" ? 0 : 1);
			for (i = 0; i <= lstPrinters.Items.Count - 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstPrinters, i) == DefaultPrinter) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					lstPrinters.SelectedIndex = i;
					lstPrinters.Text = DefaultPrinter;
					break; // TODO: might not be correct. Was : Exit For
				}
			}

		}
//UPGRADE_WARNING: Event rptMaster.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void rptMaster_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			lstView.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.ClientRectangle.Width) - 100);
			lstView.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.ClientRectangle.Height) - 750);
			for (i = 0; i <= 3; i++) {
				Cmd[i].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1100);
			}
			lstPrinters.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1100);
			Check1.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 750);
			lblEdit.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 1100);
			lstView.Focus();

		}
		private void rptMaster_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			Interaction.SaveSetting("WinGL", "Printers", this.Text, Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstPrinters, lstPrinters.SelectedIndex));
			Interaction.SaveSetting("WinGL", "Fonts", this.Text, Convert.ToString(Check1.CheckState));
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.rptObject. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_NOTE: Object ctlObject.rptObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			ctlObject.rptObject = null;
			//UPGRADE_WARNING: Couldn't resolve default property of object rptObject.rptObject. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_NOTE: Object rptObject.rptObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rptObject.rptObject = null;
			//UPGRADE_NOTE: Object rptObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rptObject = null;
			My.MyProject.Forms.frmMessage.Close();

		}
		private void lstView_DblClick(System.Object eventSender, System.EventArgs eventArgs)
		{
			object xsql = null;
			object p = null;
			object x = null;
			object i = null;

			System.Windows.Forms.Form frm = null;
			bool wait = false;
			int r = 0;

			//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.ActivateList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (!ctlObject.ActivateList) {
				return;
			}

			r = lstView.ListIndex;
			//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (i < 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.ReportName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				LogError.MsgBox_Renamed("No record was selected.", , ctlObject.ReportName);
				return;
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.ReportName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			switch (ctlObject.ReportName) {

				case "TransEditList":
					lstView.Row = r;
					if (!string.IsNullOrEmpty(Strings.Trim(Strings.Mid(lstView.ColList, 1, 7))) & Information.IsNumeric(Strings.Mid(lstView.ColList, 1, 7))) {
						My.MyProject.Forms.edtTransEditList.Tag = Strings.Mid(lstView.ColList, 1, 7);
						My.MyProject.Forms.edtTransEditList.ShowDialog();
					}
					break;

				case "InvoiceSelectionList":
					lstView.Row = r;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = lstView.ColList;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					p = Strings.InStr(x, "(*");
					//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (p == 0) {
						LogError.MsgBox_Renamed("You must select the main invoice line to edit.", , "InvoiceSelectionList");
						return;
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						i = Conversion.Val(Strings.Mid(x, p + 2, 6));
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						xsql = "SELECT * FROM Invoices WHERE InvoiceKey = " + i;
						//UPGRADE_WARNING: Couldn't resolve default property of object xsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinglGlobal.tblinvoices = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (WinglGlobal.tblinvoices.RecordCount == 0) {
							LogError.MsgBox_Renamed("Invalid Invoice.", , "InvoiceSelectionList");
							return;
						} else {
							WinglGlobal.InvoiceFlag = true;
							My.MyProject.Forms.Invoice.Tag = "Report";
							My.MyProject.Forms.Invoice.ShowDialog();
							WinglGlobal.InvoiceFlag = false;
						}
					}
					break;

				case "CheckRegister":
					lstView.Row = r;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = lstView.ColList;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (string.IsNullOrEmpty(Strings.Trim(Strings.Left(x, 10)))) {
						//If p = 0 Then
						LogError.MsgBox_Renamed("You must select the first line of a check entry to edit.", , "CheckRegister");
						return;
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						p = Strings.InStr(x, "/");
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						i = Strings.InStr(98, x, "{");
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						if (i == 0) {
							LogError.MsgBox_Renamed("Cannot obtain department number.", , "CheckRegister");
							return;
						} else {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							My.MyProject.Forms.CheckRegister.Tag = Strings.Mid(x, 5, 6) + Strings.Mid(x, i + 1);
							My.MyProject.Forms.CheckRegister.ShowDialog();
						}
					}
					break;

				default:
					return;

					break;

			}
			lstView.Clear();
			//UPGRADE_WARNING: Couldn't resolve default property of object ctlObject.Target. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ctlObject.Target = 0;
			//UPGRADE_WARNING: Couldn't resolve default property of object rptObject.print_report. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			rptObject.print_report();
			lstView.ListIndex = r;
			lstView.Focus();

		}
	}
}
