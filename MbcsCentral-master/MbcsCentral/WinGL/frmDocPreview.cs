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
	internal partial class frmDocPreview : System.Windows.Forms.Form
	{
		object picHeight;
		float picWidth;
		float picRatio;
		short pages;

		private void frmDocPreview_KeyDown(System.Object eventSender, System.Windows.Forms.KeyEventArgs eventArgs)
		{
			short KeyCode = eventArgs.KeyCode;
			short Shift = eventArgs.KeyData / 0x10000;

			short change = 0;

			change = scrollBar.Max / pages;
			switch (KeyCode) {
				case System.Windows.Forms.Keys.PageDown:
					if (scrollBar.Value + change < scrollBar.Max) {
						scrollBar.Value = scrollBar.Value + change;
					} else {
						scrollBar.Value = scrollBar.Max;
					}
					break;
				case System.Windows.Forms.Keys.PageUp:
					if (scrollBar.Value - change > 0) {
						scrollBar.Value = scrollBar.Value - change;
					} else {
						scrollBar.Value = 0;
					}
					break;
			}

		}

		private void frmDocPreview_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			//Me.Top = MainTop + 1050: Me.Left = MainLeft + 200
			this.Top = 0;
			this.Left = 0;
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
			picRatio = 11 / 8.5;
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) / picRatio);
			short i = 0;
			string docPath = null;
			System.Windows.Forms.PictureBox p = null;

			docPath = WinGlobal.GetPath(ref Interaction.GetSetting("WinGl", "Options", "WindocFile", ""));
			if (string.IsNullOrEmpty(Strings.Trim(docPath))) {
				LogError.MsgBox_Renamed("Windoc path not found");
				return;
			}
			docPath = docPath + "Documents\\";
			i = 0;
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object rsd.MoveFirst. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			WinglGlobal.rsd.MoveFirst();
			//UPGRADE_WARNING: Couldn't resolve default property of object rsd.EOF. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			while (!WinglGlobal.rsd.EOF) {
				if (i > 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					picScroll.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((i * picHeight) + picHeight);
					pctPreview.Load(i);
					pctPreview[i].Visible = true;
					//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					pctPreview[i].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((i * picHeight) + picHeight);
				} else {
					//UPGRADE_WARNING: Couldn't resolve default property of object rsd(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					this.Text = WinglGlobal.rsd("Description");
				}

				//UPGRADE_WARNING: Couldn't resolve default property of object rsd(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				pctPreview[i].Image = System.Drawing.Image.FromFile(docPath + WinglGlobal.rsd("filename"));
				i = i + 1;
				//UPGRADE_WARNING: Couldn't resolve default property of object rsd.MoveNext. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				WinglGlobal.rsd.MoveNext();
			}
			pages = i;
			this.Text = this.Text + " [" + Conversion.Str(pages) + " pages] - DOUBLE CLICK TO PRINT";
			scrollBar.LargeChange = scrollBar.Max / pages;
			scrollBar.SmallChange = (scrollBar.Max / pages) / 2;

			goto load_exit;
			load_error:

			LogError.MsgBox_Renamed("", , "Document File");
			load_exit:


		}

//UPGRADE_WARNING: Event frmDocPreview.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void frmDocPreview_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			short i = 0;

			scrollBar.Top = 0;
			scrollBar.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(scrollBar.Width) - 200);
			scrollBar.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 550);
			picWidth = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(scrollBar.Width) - 200;
			//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			picHeight = picWidth * picRatio;
			picScroll.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(picWidth);
			//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			picScroll.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY((pctPreview.Count() * picHeight));

			for (i = 0; i <= pctPreview.Count() - 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				pctPreview[i].Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(picHeight);
				pctPreview[i].Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(picWidth);
				pctPreview[i].Left = 0;
				//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				pctPreview[i].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(i * picHeight);
			}

		}

		private void pctPreview_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = pctPreview.GetIndex(eventSender);
			printDocument();
		}

//UPGRADE_ISSUE: PictureBox event picScroll.KeyPress was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
		private void picScroll_KeyPress(ref short KeyAscii)
		{

			switch (KeyAscii) {
				case System.Windows.Forms.Keys.PageDown:
					//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (scrollBar.Value + picHeight < scrollBar.Max) {
						//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						scrollBar.Value = scrollBar.Value + picHeight;
					} else {
						scrollBar.Value = scrollBar.Max;
					}
					break;
				case System.Windows.Forms.Keys.PageUp:
					//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (scrollBar.Value - picHeight > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object picHeight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						scrollBar.Value = scrollBar.Value - picHeight;
					} else {
						scrollBar.Value = 0;
					}
					break;
			}

		}

		private void scrollBar_Change(System.Object eventSender, System.EventArgs eventArgs)
		{
			picScroll.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(-((scrollBar.Value / scrollBar.Max) * Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(picScroll.Height)));
		}

		public void printDocument()
		{
			Printer Printer = new Printer();

			short i = 0;
			Printer prt = null;
			//UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			//dlgPrinter.CancelError = True
			 // ERROR: Not supported in C#: OnErrorStatement

			dlgPrinterPrint.ShowDialog();
			 // ERROR: Not supported in C#: OnErrorStatement

			for (i = 0; i <= pctPreview.Count() - 1; i++) {
				Err().Clear();
				Printer.PaintPicture(pctPreview[i].Image, 10, 10, 11430, 14400);
				if (Err().Number == 0) {
					Printer.NewPage();
				}
			}
			Printer.EndDoc();
			printDocument_exit:

		}
	}
}
