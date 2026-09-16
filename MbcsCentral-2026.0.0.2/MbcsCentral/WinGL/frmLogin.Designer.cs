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
	partial class frmLogin
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmLogin() : base()
		{
			Load += frmLogin_Load;
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
		private System.Windows.Forms.Button withEventsField_cmdCancel;
		public System.Windows.Forms.Button cmdCancel {
			get { return withEventsField_cmdCancel; }
			set {
				if (withEventsField_cmdCancel != null) {
					withEventsField_cmdCancel.Click -= cmdCancel_Click;
				}
				withEventsField_cmdCancel = value;
				if (withEventsField_cmdCancel != null) {
					withEventsField_cmdCancel.Click += cmdCancel_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_cmdOK;
		public System.Windows.Forms.Button cmdOK {
			get { return withEventsField_cmdOK; }
			set {
				if (withEventsField_cmdOK != null) {
					withEventsField_cmdOK.Click -= cmdOK_Click;
				}
				withEventsField_cmdOK = value;
				if (withEventsField_cmdOK != null) {
					withEventsField_cmdOK.Click += cmdOK_Click;
				}
			}
		}
		public System.Windows.Forms.TextBox txtPassword;
		public System.Windows.Forms.TextBox txtUserName;
		public System.Windows.Forms.Label _lblLabels_1;
		public System.Windows.Forms.Label _lblLabels_0;
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray lblLabels;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmLogin));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this._lblLabels_1 = new System.Windows.Forms.Label();
			this._lblLabels_0 = new System.Windows.Forms.Label();
			this.lblLabels = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lblLabels).BeginInit();
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Text = "Login";
			this.ClientSize = new System.Drawing.Size(250, 106);
			this.Location = new System.Drawing.Point(3, 22);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "Login";
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ControlBox = true;
			this.Enabled = true;
			this.KeyPreview = false;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmLogin";
			this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this.cmdCancel;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Size = new System.Drawing.Size(76, 24);
			this.cmdCancel.Location = new System.Drawing.Point(140, 68);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Tag = "Cancel";
			this.cmdCancel.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCancel.CausesValidation = true;
			this.cmdCancel.Enabled = true;
			this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdCancel.TabStop = true;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdOK.Text = "OK";
			this.AcceptButton = this.cmdOK;
			this.cmdOK.Size = new System.Drawing.Size(76, 24);
			this.cmdOK.Location = new System.Drawing.Point(33, 68);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Tag = "OK";
			this.cmdOK.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.CausesValidation = true;
			this.cmdOK.Enabled = true;
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.TabStop = true;
			this.cmdOK.Name = "cmdOK";
			this.txtPassword.AutoSize = false;
			this.txtPassword.Size = new System.Drawing.Size(155, 19);
			this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.txtPassword.Location = new System.Drawing.Point(87, 35);
			this.txtPassword.PasswordChar = Strings.ChrW(42);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtPassword.AcceptsReturn = true;
			this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
			this.txtPassword.CausesValidation = true;
			this.txtPassword.Enabled = true;
			this.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtPassword.HideSelection = true;
			this.txtPassword.ReadOnly = false;
			this.txtPassword.MaxLength = 0;
			this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtPassword.Multiline = false;
			this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtPassword.TabStop = true;
			this.txtPassword.Visible = true;
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtPassword.Name = "txtPassword";
			this.txtUserName.AutoSize = false;
			this.txtUserName.Size = new System.Drawing.Size(155, 19);
			this.txtUserName.Location = new System.Drawing.Point(87, 9);
			this.txtUserName.TabIndex = 3;
			this.txtUserName.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtUserName.AcceptsReturn = true;
			this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
			this.txtUserName.CausesValidation = true;
			this.txtUserName.Enabled = true;
			this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtUserName.HideSelection = true;
			this.txtUserName.ReadOnly = false;
			this.txtUserName.MaxLength = 0;
			this.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtUserName.Multiline = false;
			this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtUserName.TabStop = true;
			this.txtUserName.Visible = true;
			this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtUserName.Name = "txtUserName";
			this._lblLabels_1.Text = "&Password:";
			this._lblLabels_1.Size = new System.Drawing.Size(72, 17);
			this._lblLabels_1.Location = new System.Drawing.Point(7, 36);
			this._lblLabels_1.TabIndex = 0;
			this._lblLabels_1.Tag = "&Password:";
			this._lblLabels_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_1.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_1.Enabled = true;
			this._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_1.UseMnemonic = true;
			this._lblLabels_1.Visible = true;
			this._lblLabels_1.AutoSize = false;
			this._lblLabels_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_1.Name = "_lblLabels_1";
			this._lblLabels_0.Text = "&User Name:";
			this._lblLabels_0.Size = new System.Drawing.Size(72, 17);
			this._lblLabels_0.Location = new System.Drawing.Point(7, 10);
			this._lblLabels_0.TabIndex = 2;
			this._lblLabels_0.Tag = "&User Name:";
			this._lblLabels_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_0.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_0.Enabled = true;
			this._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_0.UseMnemonic = true;
			this._lblLabels_0.Visible = true;
			this._lblLabels_0.AutoSize = false;
			this._lblLabels_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_0.Name = "_lblLabels_0";
			this.Controls.Add(cmdCancel);
			this.Controls.Add(cmdOK);
			this.Controls.Add(txtPassword);
			this.Controls.Add(txtUserName);
			this.Controls.Add(_lblLabels_1);
			this.Controls.Add(_lblLabels_0);
			this.lblLabels.SetIndex(_lblLabels_1, Convert.ToInt16(1));
			this.lblLabels.SetIndex(_lblLabels_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.lblLabels).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
