using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6;
namespace WinGL
{
	internal class clsReport
	{
//local variable(s) to hold property value(s)
			//local copy
		private object mvarPrinter;
			//local copy
		private string mvarReportName;
			//local copy
		private short mvarPageSize;
			//local copy
		private short mvarTarget;
			//local copy
		private object mvarrptObject;
			//local copy
		private bool mvarCondensePrint;
		private short mvarFontSize;
		public enum Destination
		{
			rptdisplay = 0,
			rptprinter = 1,
			rptfile = 2
		}

//local private variables
		private short OutFile;
		private bool FirstTime;
		private System.Windows.Forms.Form frmReport;
		private Printer prt;
		private short ptr;
		private string s;
			//local copy
		private bool mvarActivateList;
//local variable(s) to hold property value(s)
			//local copy
		private bool mvarBatch;
		public bool Batch {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Batch
			get { return mvarBatch; }
			set {
				Printer Printer = new Printer();
				object x = null;
				//used when assigning a value to the property, on the left side of an assignment.
				//Syntax: X.Batch = 5
				mvarBatch = value;

				if (My.MyProject.Forms.frmBatch.chkPrinters.CheckState == 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = My.MyProject.Forms.frmBatch.lstPrinters.Text;
					CondensePrint = (My.MyProject.Forms.frmBatch.chkCondense.CheckState == 1 ? true : false);
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = Interaction.GetSetting("WinGL", "Printers", ReportName, Printer.DeviceName);
					CondensePrint = (Interaction.GetSetting("WinGL", "Fonts", ReportName, Convert.ToString(0)) == "0" ? 0 : 1);
				}
				foreach (object prt_loopVariable in GlobalModule.Printers) {
					prt = prt_loopVariable;
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (prt.DeviceName == x) {
						Device = prt;
						break; // TODO: might not be correct. Was : Exit For
					}
				}

			}
		}
		public bool ActivateList {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ActivateList
			get { return mvarActivateList; }
			set {
				//used when assigning a value to the property, on the left side of an assignment.
				//Syntax: X.ActivateList = 5
				 // ERROR: Not supported in C#: OnErrorStatement

				mvarActivateList = value;
				//UPGRADE_ISSUE: Control lblEdit could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//If mvarActivateList Then frmReport.lblEdit.Visible = True
			}
		}
		public bool CondensePrint {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.CondensePrint
			get { return mvarCondensePrint; }
			set {
				Printer Printer = new Printer();
				//used when assigning a value to the property, on the left side of an assignment.
				//Syntax: X.CondensePrint = 5
				mvarCondensePrint = value;
				if (value) {
					Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeName(Printer.Font, "Courier New");
					Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(Printer.Font, 7.5);
					SetPageSize();
				}

			}
		}
		public object rptObject {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.rptObject
			get { return mvarrptObject; }
//used when assigning an Object to the property, on the left side of a Set statement.
//Syntax: Set x.rptObject = Form1
			set { mvarrptObject = value; }
		}
		public short Target {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Target
			get { return mvarTarget; }
			set {
				Printer Printer = new Printer();
				object i = null;
				//used when assigning a value to the property, on the left side of an assignment.
				//Syntax: X.Target = 5
				mvarTarget = value;
				if (mvarTarget != Destination.rptprinter & !WinglGlobal.rptCancel) {
					frmReport = new rptMaster();
					frmReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
					My.MyProject.Forms.frmMessage.Text = "Processing Report . . .";
					My.MyProject.Forms.frmMessage.Show();
				} else if (mvarTarget == Destination.rptprinter) {
					if (WinglGlobal.tmargin > 0) {
						for (i = 1; i <= WinglGlobal.tmargin; i++) {
							Printer.Print();
						}
					}
				}
			}
		}
		public short PageSize {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.PageSize
			get { return mvarPageSize; }
//used when assigning a value to the property, on the left side of an assignment.
//Syntax: X.PageSize = 5
			set { mvarPageSize = value; }
		}
		public string ReportName {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.ReportName
			get { return mvarReportName; }
			set {
				//used when assigning a value to the property, on the left side of an assignment.
				//Syntax: X.ReportName = 5
				mvarReportName = value;
				 // ERROR: Not supported in C#: OnErrorStatement

				if (Target != Destination.rptprinter)
					frmReport.Text = ReportName;

			}
		}
		public object Device {
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Printer
			get { return mvarPrinter; }
			set {
				Printer Printer = new Printer();
				//used when assigning an Object to the property, on the left side of a Set statement.
				//Syntax: Set x.Printer = Form1
				mvarPrinter = value;
				Printer = value;
				SetPageSize();
			}
		}
		private void SetPageSize()
		{
			Printer Printer = new Printer();

			short txtheight = 0;

			txtheight = Printer.TextHeight("Text Height");
			PageSize = Printer.Height / txtheight;
			if (Printer.Font.SizeInPoints == 12) {
				PageSize = 66;
				//Else
				//    If PageSize > 98 Then PageSize = 98
			}

		}
		public void FontUnderline(ref bool FontUnderline)
		{
			Printer Printer = new Printer();

			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//frmReport.lstView.Font.Underline = FontUnderline
				case Destination.rptprinter:
					Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeUnderline(Printer.Font, FontUnderline);
					break;
			}

		}
		public void NewPage()
		{
			object i = null;
			Printer Printer = new Printer();

			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//frmReport.lstView.AddItem("")
				case Destination.rptprinter:
					Printer.NewPage();
					if (WinglGlobal.tmargin > 0) {
						for (i = 1; i <= WinglGlobal.tmargin; i++) {
							Printer.Print();
						}
					}
					break;
				case Destination.rptfile:
					FileSystem.PrintLine(OutFile, Constants.vbFormFeed);
					break;
			}

		}
		public void FontBold(ref bool FontBold)
		{
			Printer Printer = new Printer();

			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//frmReport.lstView.Font.Bold = FontBold
				case Destination.rptprinter:
					Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeBold(Printer.Font, FontBold);
					break;
			}

		}
		public void FontSize(ref object FontSize)
		{
			Printer Printer = new Printer();

			 // ERROR: Not supported in C#: OnErrorStatement

			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//frmReport.lstView.Font.Size = 10
				case Destination.rptprinter:
					if (!CondensePrint) {
						//UPGRADE_WARNING: Couldn't resolve default property of object FontSize. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(Printer.Font, FontSize);
					}
					break;
			}

			SetPageSize();

		}
		public void FontName(ref object FontName)
		{
			Printer Printer = new Printer();

			 // ERROR: Not supported in C#: OnErrorStatement

			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//UPGRADE_WARNING: Couldn't resolve default property of object FontName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//frmReport.lstView.FontName = FontName
				case Destination.rptprinter:
					//UPGRADE_WARNING: Couldn't resolve default property of object FontName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeName(Printer.Font, FontName);
					break;
			}

		}
		public void EndDoc()
		{
			Printer Printer = new Printer();

			switch (Target) {
				case Destination.rptdisplay:
					My.MyProject.Forms.frmMessage.Close();
					break;
				case Destination.rptprinter:
					Printer.EndDoc();
					break;
				case Destination.rptfile:
					FileSystem.FileClose(OutFile);
					break;
			}

		}
//UPGRADE_WARNING: ParamArray Data was changed from ByRef to ByVal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93C6A0DC-8C99-429A-8696-35FC4DCEFCCC"'
		public void Send(params object[] Data)
		{
			Printer Printer = new Printer();
			object sx = null;

			object x = null;
			short p = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			if (FirstTime) {
				//UPGRADE_ISSUE: Control rptObject could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//If Target <> Destination.rptprinter Then frmReport.rptObject = mvarrptObject
				//UPGRADE_ISSUE: Control ctlObject could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//If Target <> Destination.rptprinter Then frmReport.ctlObject = Me
				FirstTime = false;
			}

			if (ptr == 1)
				s = Strings.Space(256);

			foreach (object x_loopVariable in Data) {
				x = x_loopVariable;
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (Strings.LCase(Strings.Left(x, 4)) == "tab(") {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					ptr = Conversion.Val(Strings.Mid(x, 5));
				} else {
					if (Strings.Len(x) > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						Strings.Mid(s, ptr, Strings.Len(x)) = x;
						ptr = ptr + Strings.Len(x);
					}
				}
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object sx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			sx = Strings.RTrim(s);
			//UPGRADE_WARNING: Couldn't resolve default property of object sx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (Strings.Right(sx, 1) == ";") {
				Strings.Mid(s, Strings.Len(sx), 1) = " ";
				ptr = ptr - 1;
				return;
			} else {
				ptr = 1;
			}
			switch (Target) {
				case Destination.rptdisplay:
					break;
				//UPGRADE_ISSUE: Control lstView could not be resolved because it was within the generic namespace Form. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
				//frmReport.lstView.AddItem(s)
				case Destination.rptprinter:
					Printer.Print(s);
					break;
				case Destination.rptfile:
					FileSystem.PrintLine(OutFile, s);
					break;
			}

		}
//UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Initialize_Renamed()
		{
			Printer Printer = new Printer();

			short txtheight = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			FirstTime = true;

			Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeName(Printer.Font, "Courier New");
			Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(Printer.Font, 12);
			SetPageSize();
			ptr = 1;
			WinglGlobal.Ifilename = "Invoices";
			WinglGlobal.IDfilename = "InvoiceDistributions";
			goto Initialize_exit;
			Initialize_error:

			LogError.MsgBox_Renamed(Convert.ToDouble("Error <") + Err().Number + Convert.ToDouble("> ") + Convert.ToDouble(Err().Description));
			Initialize_exit:


		}
		public clsReport() : base()
		{
			Class_Initialize_Renamed();
		}
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			My.MyProject.Forms.frmMessage.Close();
			frmReport.Close();
			//UPGRADE_NOTE: Object frmReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			frmReport = null;
			//UPGRADE_NOTE: Object rptObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rptObject = null;
			WinglGlobal.rptCancel = false;

		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
	}
}
