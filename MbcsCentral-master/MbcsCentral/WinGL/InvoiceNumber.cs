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
	internal partial class InvoiceNumber : System.Windows.Forms.Form
	{
		public short EntryType;
		public bool PrePaid;
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// Process
					My.MyProject.Forms.InvoicesBatch.invDateFlag = (chkDate.CheckState != 0 ? true : false);
					My.MyProject.Forms.InvoicesBatch.invCheckFlag = (chkCheck.CheckState != 0 ? true : false);
					break;
				case 1:
					// Cancel
					switch (EntryType) {
						case WinglGlobal.ibManual:
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							txtManual.CtlText = "";
							break;
						case WinglGlobal.ibDptDate:
							//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							dtInvoice.Value = System.DBNull.Value;
							break;
						case WinglGlobal.ibDescDate:
							//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							dtInvoice.Value = System.DBNull.Value;
							break;
						case WinglGlobal.ibDescManual:
							//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
							txtManual.CtlText = "";
							break;
					}
					break;
			}
			this.Hide();

		}
		private void cmdInvoice_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.InvoicesBatch.invDistFlag = true;
			this.Hide();

		}

//UPGRADE_WARNING: Form event InvoiceNumber.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void InvoiceNumber_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (PrePaid) {
				lblCheck.Visible = true;
				txtCheck.Visible = true;
				dtCheck.Visible = true;
				chkCheck.Visible = true;
			} else {
				lblCheck.Visible = false;
				txtCheck.Visible = false;
				dtCheck.Visible = false;
				chkCheck.Visible = false;
			}

			switch (EntryType) {
				case WinglGlobal.ibManual:
					this.Text = this.Text + " - Manual Entry";
					dtInvoice.Visible = false;
					chkDate.Visible = false;
					lblDepartment.Visible = false;
					txtManual.Top = lblDepartment.Top;
					txtManual.Left = lblDepartment.Left;
					txtManual.Visible = true;
					break;
				case WinglGlobal.ibDptDate:
					this.Text = this.Text + " - Department + Date";
					break;
				case WinglGlobal.ibDescDate:
					this.Text = this.Text + " - Invoice Number + Date";
					break;
				case WinglGlobal.ibManDate:
					this.Text = this.Text + " - Manual Entry + Date";
					lblDepartment.Visible = false;
					txtManual.Top = lblDepartment.Top;
					txtManual.Left = lblDepartment.Left;
					txtManual.Width = lblDepartment.Width;
					txtManual.Visible = true;
					txtManual.TabIndex = 0;
					break;
				case WinglGlobal.ibDescManual:
					this.Text = this.Text + " - Description + Manual Entry";
					dtInvoice.Visible = false;
					chkDate.Visible = false;
					txtManual.Top = lblDepartment.Top;
					txtManual.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(lblDepartment.Left) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(lblDepartment.Width) + 50);
					txtManual.Visible = true;
					break;
				case WinglGlobal.ibDesc2Year:
					this.Text = this.Text + " - Invoice Number + 2 digit year";
					break;

			}


		}

		private void InvoiceNumber_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft);

		}
	}
}
