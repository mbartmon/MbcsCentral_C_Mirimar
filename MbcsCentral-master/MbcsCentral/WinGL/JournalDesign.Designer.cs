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
	partial class JournalDesign
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public JournalDesign() : base()
		{
			Load += JournalDesign_Load;
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
		private AxLpLib.AxfpCombo withEventsField_lstLayouts;
		public AxLpLib.AxfpCombo lstLayouts {
			get { return withEventsField_lstLayouts; }
			set {
				if (withEventsField_lstLayouts != null) {
					withEventsField_lstLayouts.ClickEvent -= lstLayouts_ClickEvent;
				}
				withEventsField_lstLayouts = value;
				if (withEventsField_lstLayouts != null) {
					withEventsField_lstLayouts.ClickEvent += lstLayouts_ClickEvent;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label _Label1_0;
		private AxXGridLib.AxXGrid withEventsField_XGrid1;
		public AxXGridLib.AxXGrid XGrid1 {
			get { return withEventsField_XGrid1; }
			set {
				if (withEventsField_XGrid1 != null) {
					withEventsField_XGrid1.Change -= XGrid1_Change;
					withEventsField_XGrid1.ClickCell -= XGrid1_ClickCell;
				}
				withEventsField_XGrid1 = value;
				if (withEventsField_XGrid1 != null) {
					withEventsField_XGrid1.Change += XGrid1_Change;
					withEventsField_XGrid1.ClickCell += XGrid1_ClickCell;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Cmd;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Cmd {
			get { return withEventsField_Cmd; }
			set {
				if (withEventsField_Cmd != null) {
					withEventsField_Cmd.Click -= Cmd_Click;
				}
				withEventsField_Cmd = value;
				if (withEventsField_Cmd != null) {
					withEventsField_Cmd.Click += Cmd_Click;
				}
			}
		}
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(JournalDesign));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstLayouts = new AxLpLib.AxfpCombo();
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this._Label1_1 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.XGrid1 = new AxXGridLib.AxXGrid();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstLayouts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.XGrid1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Text = "G/L Journal Report Layouts";
			this.ClientSize = new System.Drawing.Size(627, 345);
			this.Location = new System.Drawing.Point(4, 17);
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.ControlBox = true;
			this.Enabled = true;
			this.KeyPreview = false;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "JournalDesign";
			lstLayouts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstLayouts.OcxState");
			this.lstLayouts.Size = new System.Drawing.Size(265, 19);
			this.lstLayouts.Location = new System.Drawing.Point(87, 10);
			this.lstLayouts.TabIndex = 4;
			this.lstLayouts.Name = "lstLayouts";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_3.Text = "APPLY RANGES";
			this._Cmd_3.Size = new System.Drawing.Size(81, 33);
			this._Cmd_3.Location = new System.Drawing.Point(200, 309);
			this._Cmd_3.TabIndex = 8;
			this._Cmd_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_3.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_3.CausesValidation = true;
			this._Cmd_3.Enabled = true;
			this._Cmd_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_3.TabStop = true;
			this._Cmd_3.Name = "_Cmd_3";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "EXIT";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(9, 307);
			this._Cmd_2.TabIndex = 7;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "DELETE";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(9, 271);
			this._Cmd_1.TabIndex = 6;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.TabStop = true;
			this._Cmd_1.Name = "_Cmd_1";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_0.Text = "SAVE";
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(9, 234);
			this._Cmd_0.TabIndex = 5;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this._Label1_1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this._Label1_1.Text = "Press the buton below to apply Range for Item 1 to all of the items.";
			this._Label1_1.Size = new System.Drawing.Size(217, 43);
			this._Label1_1.Location = new System.Drawing.Point(200, 264);
			this._Label1_1.TabIndex = 9;
			this._Label1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_1.Enabled = true;
			this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_1.UseMnemonic = true;
			this._Label1_1.Visible = true;
			this._Label1_1.AutoSize = false;
			this._Label1_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._Label1_1.Name = "_Label1_1";
			this.Label3.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Label3.Text = "Report Layout:";
			this.Label3.Size = new System.Drawing.Size(75, 15);
			this.Label3.Location = new System.Drawing.Point(11, 12);
			this.Label3.TabIndex = 3;
			this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label3.Enabled = true;
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.UseMnemonic = true;
			this.Label3.Visible = true;
			this.Label3.AutoSize = false;
			this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label3.Name = "Label3";
			this.Label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Label2.Text = "If checked, will always print. If not checked, will print only if range specified and record falls within the range.";
			this.Label2.Size = new System.Drawing.Size(80, 111);
			this.Label2.Location = new System.Drawing.Point(487, 230);
			this.Label2.TabIndex = 2;
			this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label2.Enabled = true;
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.UseMnemonic = true;
			this.Label2.Visible = true;
			this.Label2.AutoSize = false;
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label2.Name = "Label2";
			this._Label1_0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this._Label1_0.Text = "Press a button, above, to go to the Account Selection Screen.";
			this._Label1_0.Size = new System.Drawing.Size(217, 34);
			this._Label1_0.Location = new System.Drawing.Point(200, 230);
			this._Label1_0.TabIndex = 1;
			this._Label1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_0.Enabled = true;
			this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_0.UseMnemonic = true;
			this._Label1_0.Visible = true;
			this._Label1_0.AutoSize = false;
			this._Label1_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._Label1_0.Name = "_Label1_0";
			XGrid1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("XGrid1.OcxState");
			this.XGrid1.Size = new System.Drawing.Size(599, 192);
			this.XGrid1.Location = new System.Drawing.Point(9, 36);
			this.XGrid1.TabIndex = 0;
			this.XGrid1.Name = "XGrid1";
			this.Controls.Add(lstLayouts);
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(_Label1_0);
			this.Controls.Add(XGrid1);
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.XGrid1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstLayouts).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
