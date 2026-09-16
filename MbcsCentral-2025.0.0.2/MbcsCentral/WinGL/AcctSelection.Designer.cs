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
	partial class AcctSelection
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public AcctSelection() : base()
		{
			Resize += AcctSelection_Resize;
			Load += AcctSelection_Load;
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
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		private AxXGridLib.AxXGrid withEventsField_grdAccounts;
		public AxXGridLib.AxXGrid grdAccounts {
			get { return withEventsField_grdAccounts; }
			set {
				if (withEventsField_grdAccounts != null) {
					withEventsField_grdAccounts.Change -= grdAccounts_Change;
					withEventsField_grdAccounts.ClickCell -= grdAccounts_ClickCell;
				}
				withEventsField_grdAccounts = value;
				if (withEventsField_grdAccounts != null) {
					withEventsField_grdAccounts.Change += grdAccounts_Change;
					withEventsField_grdAccounts.ClickCell += grdAccounts_ClickCell;
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AcctSelection));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.grdAccounts = new AxXGridLib.AxXGrid();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.grdAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Account Selection";
			this.ClientSize = new System.Drawing.Size(542, 345);
			this.Location = new System.Drawing.Point(4, 23);
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
			this.Name = "AcctSelection";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_3;
			this._Cmd_3.Text = "CLEAR ALL";
			this._Cmd_3.Size = new System.Drawing.Size(81, 33);
			this._Cmd_3.Location = new System.Drawing.Point(453, 139);
			this._Cmd_3.TabIndex = 4;
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
			this._Cmd_2.Text = "SELECT ALL";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(453, 107);
			this._Cmd_2.TabIndex = 3;
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
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(453, 54);
			this._Cmd_1.TabIndex = 2;
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
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(453, 10);
			this._Cmd_0.TabIndex = 1;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			grdAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdAccounts.OcxState");
			this.grdAccounts.Size = new System.Drawing.Size(443, 329);
			this.grdAccounts.Location = new System.Drawing.Point(4, 8);
			this.grdAccounts.TabIndex = 0;
			this.grdAccounts.Name = "grdAccounts";
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(grdAccounts);
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdAccounts).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
