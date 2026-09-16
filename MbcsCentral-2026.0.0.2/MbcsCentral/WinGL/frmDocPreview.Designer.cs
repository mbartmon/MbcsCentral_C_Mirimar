using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace WinGL
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class frmDocPreview
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmDocPreview() : base()
		{
			Resize += frmDocPreview_Resize;
			Load += frmDocPreview_Load;
			KeyDown += frmDocPreview_KeyDown;
			//This call is required by the Windows Form Designer.
			InitializeComponent();
		}
//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool Disposing)
		{
			if (Disposing) {
				if ((components != null)) {
					components.Dispose();
				}
			}
			base.Dispose(Disposing);
		}
//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.ToolTip ToolTip1;
		public System.Windows.Forms.PrintDialog dlgPrinterPrint;
		public System.Windows.Forms.PictureBox _pctPreview_0;
		public System.Windows.Forms.Panel picScroll;
		private AxMSComCtl2.AxFlatScrollBar withEventsField_scrollBar;
		public AxMSComCtl2.AxFlatScrollBar scrollBar {
			get { return withEventsField_scrollBar; }
			set {
				if (withEventsField_scrollBar != null) {
					withEventsField_scrollBar.Change -= scrollBar_Change;
				}
				withEventsField_scrollBar = value;
				if (withEventsField_scrollBar != null) {
					withEventsField_scrollBar.Change += scrollBar_Change;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray withEventsField_pctPreview;
		public Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray pctPreview {
			get { return withEventsField_pctPreview; }
			set {
				if (withEventsField_pctPreview != null) {
					withEventsField_pctPreview.DoubleClick -= pctPreview_DoubleClick;
				}
				withEventsField_pctPreview = value;
				if (withEventsField_pctPreview != null) {
					withEventsField_pctPreview.DoubleClick += pctPreview_DoubleClick;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmDocPreview));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.dlgPrinterPrint = new System.Windows.Forms.PrintDialog();
			this.dlgPrinterPrint.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
			this.picScroll = new System.Windows.Forms.Panel();
			this._pctPreview_0 = new System.Windows.Forms.PictureBox();
			this.scrollBar = new AxMSComCtl2.AxFlatScrollBar();
			this.pctPreview = new Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(components);
			this.picScroll.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.scrollBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.pctPreview).BeginInit();
			this.Text = "Form1";
			this.ClientSize = new System.Drawing.Size(612, 689);
			this.Location = new System.Drawing.Point(8, 30);
			this.KeyPreview = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.ControlBox = true;
			this.Enabled = true;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmDocPreview";
			this.picScroll.Size = new System.Drawing.Size(457, 393);
			this.picScroll.Location = new System.Drawing.Point(8, 0);
			this.picScroll.TabIndex = 1;
			this.picScroll.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.picScroll.Dock = System.Windows.Forms.DockStyle.None;
			this.picScroll.BackColor = System.Drawing.SystemColors.Control;
			this.picScroll.CausesValidation = true;
			this.picScroll.Enabled = true;
			this.picScroll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.picScroll.Cursor = System.Windows.Forms.Cursors.Default;
			this.picScroll.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.picScroll.TabStop = true;
			this.picScroll.Visible = true;
			this.picScroll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picScroll.Name = "picScroll";
			this._pctPreview_0.Size = new System.Drawing.Size(209, 57);
			this._pctPreview_0.Location = new System.Drawing.Point(0, 0);
			this._pctPreview_0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this._pctPreview_0.Enabled = true;
			this._pctPreview_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._pctPreview_0.Visible = true;
			this._pctPreview_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._pctPreview_0.Name = "_pctPreview_0";
			scrollBar.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("scrollBar.OcxState");
			this.scrollBar.Size = new System.Drawing.Size(25, 537);
			this.scrollBar.Location = new System.Drawing.Point(472, 0);
			this.scrollBar.TabIndex = 0;
			this.scrollBar.Name = "scrollBar";
			this.Controls.Add(picScroll);
			this.Controls.Add(scrollBar);
			this.picScroll.Controls.Add(_pctPreview_0);
			this.pctPreview.SetIndex(_pctPreview_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.pctPreview).EndInit();
			((System.ComponentModel.ISupportInitialize)this.scrollBar).EndInit();
			this.picScroll.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
