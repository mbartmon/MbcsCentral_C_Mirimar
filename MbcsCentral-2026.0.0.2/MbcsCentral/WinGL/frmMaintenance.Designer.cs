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
	partial class frmMaintenance
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmMaintenance() : base()
		{
			Resize += frmMaintenance_Resize;
			Load += frmMaintenance_Load;
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = My.MyProject.Forms.frmMain;
			My.MyProject.Forms.frmMain.Show();
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
		public System.Windows.Forms.ProgressBar ProgressBar;
		public System.Windows.Forms.ListBox List1;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		private System.Windows.Forms.CheckBox withEventsField_Check2;
		public System.Windows.Forms.CheckBox Check2 {
			get { return withEventsField_Check2; }
			set {
				if (withEventsField_Check2 != null) {
					withEventsField_Check2.CheckStateChanged -= Check2_CheckStateChanged;
				}
				withEventsField_Check2 = value;
				if (withEventsField_Check2 != null) {
					withEventsField_Check2.CheckStateChanged += Check2_CheckStateChanged;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_Check1;
		public System.Windows.Forms.CheckBox Check1 {
			get { return withEventsField_Check1; }
			set {
				if (withEventsField_Check1 != null) {
					withEventsField_Check1.CheckStateChanged -= Check1_CheckStateChanged;
				}
				withEventsField_Check1 = value;
				if (withEventsField_Check1 != null) {
					withEventsField_Check1.CheckStateChanged += Check1_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.Label Label1;
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMaintenance));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.List1 = new System.Windows.Forms.ListBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.Check2 = new System.Windows.Forms.CheckBox();
			this.Check1 = new System.Windows.Forms.CheckBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Form1";
			this.ClientSize = new System.Drawing.Size(525, 275);
			this.Location = new System.Drawing.Point(4, 23);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
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
			this.Name = "frmMaintenance";
			this.ProgressBar.Size = new System.Drawing.Size(153, 25);
			this.ProgressBar.Location = new System.Drawing.Point(0, 192);
			this.ProgressBar.TabIndex = 6;
			this.ProgressBar.Name = "ProgressBar";
			this.List1.Size = new System.Drawing.Size(361, 241);
			this.List1.Location = new System.Drawing.Point(160, 24);
			this.List1.TabIndex = 4;
			this.List1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.List1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.List1.BackColor = System.Drawing.SystemColors.Window;
			this.List1.CausesValidation = true;
			this.List1.Enabled = true;
			this.List1.ForeColor = System.Drawing.SystemColors.WindowText;
			this.List1.IntegralHeight = true;
			this.List1.Cursor = System.Windows.Forms.Cursors.Default;
			this.List1.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.List1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.List1.Sorted = false;
			this.List1.TabStop = true;
			this.List1.Visible = true;
			this.List1.MultiColumn = false;
			this.List1.Name = "List1";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "EXIT";
			this._Cmd_1.Size = new System.Drawing.Size(97, 33);
			this._Cmd_1.Location = new System.Drawing.Point(16, 128);
			this._Cmd_1.TabIndex = 3;
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
			this._Cmd_0.Text = "GET RECORDS";
			this._Cmd_0.Size = new System.Drawing.Size(97, 33);
			this._Cmd_0.Location = new System.Drawing.Point(16, 72);
			this._Cmd_0.TabIndex = 2;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.Check2.Text = "Clear All Existing Records";
			this.Check2.Size = new System.Drawing.Size(153, 17);
			this.Check2.Location = new System.Drawing.Point(16, 40);
			this.Check2.TabIndex = 1;
			this.Check2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Check2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Check2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.Check2.BackColor = System.Drawing.SystemColors.Control;
			this.Check2.CausesValidation = true;
			this.Check2.Enabled = true;
			this.Check2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Check2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Check2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Check2.Appearance = System.Windows.Forms.Appearance.Normal;
			this.Check2.TabStop = true;
			this.Check2.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.Check2.Visible = true;
			this.Check2.Name = "Check2";
			this.Check1.Text = "Replace Existing Records";
			this.Check1.Size = new System.Drawing.Size(145, 17);
			this.Check1.Location = new System.Drawing.Point(16, 16);
			this.Check1.TabIndex = 0;
			this.Check1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Check1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Check1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.Check1.BackColor = System.Drawing.SystemColors.Control;
			this.Check1.CausesValidation = true;
			this.Check1.Enabled = true;
			this.Check1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Check1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Check1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Check1.Appearance = System.Windows.Forms.Appearance.Normal;
			this.Check1.TabStop = true;
			this.Check1.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.Check1.Visible = true;
			this.Check1.Name = "Check1";
			this.Label1.Text = "Status Messages:";
			this.Label1.Size = new System.Drawing.Size(265, 17);
			this.Label1.Location = new System.Drawing.Point(160, 8);
			this.Label1.TabIndex = 5;
			this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Enabled = true;
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.UseMnemonic = true;
			this.Label1.Visible = true;
			this.Label1.AutoSize = false;
			this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label1.Name = "Label1";
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			this.Controls.Add(ProgressBar);
			this.Controls.Add(List1);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(Check2);
			this.Controls.Add(Check1);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
