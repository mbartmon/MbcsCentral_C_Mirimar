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
	partial class DepartmentDetail
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public DepartmentDetail() : base()
		{
			Load += DepartmentDetail_Load;
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
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Button _Cmd_1;
		public AxMBText.AxTextField _txtFields_0;
		public AxMBText.AxTextField _txtFields_1;
		public AxMBText.AxTextField _txtFields_2;
		public AxMBText.AxTextField _txtFields_3;
		public AxMBText.AxTextField _txtFields_4;
		public AxMBText.AxTextField _txtFields_5;
		public AxMBText.AxTextField _txtFields_6;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label _lblLabels_6;
		public System.Windows.Forms.Label _lblLabels_5;
		public System.Windows.Forms.Label _lblLabels_4;
		public System.Windows.Forms.Label _lblLabels_3;
		public System.Windows.Forms.Label _lblLabels_2;
		public System.Windows.Forms.Label _lblLabels_1;
		public System.Windows.Forms.Label _lblLabels_0;
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
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray lblLabels;
		private AxTextFieldArray withEventsField_txtFields;
		public AxTextFieldArray txtFields {
			get { return withEventsField_txtFields; }
			set {
				if (withEventsField_txtFields != null) {
					withEventsField_txtFields.Leave -= txtFields_Leave;
				}
				withEventsField_txtFields = value;
				if (withEventsField_txtFields != null) {
					withEventsField_txtFields.Leave += txtFields_Leave;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DepartmentDetail));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._Cmd_0 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._txtFields_0 = new AxMBText.AxTextField();
			this._txtFields_1 = new AxMBText.AxTextField();
			this._txtFields_2 = new AxMBText.AxTextField();
			this._txtFields_3 = new AxMBText.AxTextField();
			this._txtFields_4 = new AxMBText.AxTextField();
			this._txtFields_5 = new AxMBText.AxTextField();
			this._txtFields_6 = new AxMBText.AxTextField();
			this.Label1 = new System.Windows.Forms.Label();
			this._lblLabels_6 = new System.Windows.Forms.Label();
			this._lblLabels_5 = new System.Windows.Forms.Label();
			this._lblLabels_4 = new System.Windows.Forms.Label();
			this._lblLabels_3 = new System.Windows.Forms.Label();
			this._lblLabels_2 = new System.Windows.Forms.Label();
			this._lblLabels_1 = new System.Windows.Forms.Label();
			this._lblLabels_0 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.lblLabels = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.txtFields = new AxTextFieldArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this._txtFields_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lblLabels).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtFields).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Text = "Department";
			this.ClientSize = new System.Drawing.Size(462, 214);
			this.Location = new System.Drawing.Point(73, 22);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ControlBox = true;
			this.Enabled = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "DepartmentDetail";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_0.Text = "SAVE";
			this._Cmd_0.Size = new System.Drawing.Size(57, 25);
			this._Cmd_0.Location = new System.Drawing.Point(16, 176);
			this._Cmd_0.TabIndex = 15;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "EXIT";
			this.AcceptButton = this._Cmd_1;
			this._Cmd_1.Size = new System.Drawing.Size(57, 25);
			this._Cmd_1.Location = new System.Drawing.Point(88, 176);
			this._Cmd_1.TabIndex = 14;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.TabStop = true;
			this._Cmd_1.Name = "_Cmd_1";
			_txtFields_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_0.OcxState");
			this._txtFields_0.Size = new System.Drawing.Size(49, 25);
			this._txtFields_0.Location = new System.Drawing.Point(136, 0);
			this._txtFields_0.TabIndex = 7;
			this._txtFields_0.Name = "_txtFields_0";
			_txtFields_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_1.OcxState");
			this._txtFields_1.Size = new System.Drawing.Size(313, 25);
			this._txtFields_1.Location = new System.Drawing.Point(136, 24);
			this._txtFields_1.TabIndex = 8;
			this._txtFields_1.Name = "_txtFields_1";
			_txtFields_2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_2.OcxState");
			this._txtFields_2.Size = new System.Drawing.Size(313, 25);
			this._txtFields_2.Location = new System.Drawing.Point(136, 48);
			this._txtFields_2.TabIndex = 9;
			this._txtFields_2.Name = "_txtFields_2";
			_txtFields_3.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_3.OcxState");
			this._txtFields_3.Size = new System.Drawing.Size(313, 25);
			this._txtFields_3.Location = new System.Drawing.Point(136, 72);
			this._txtFields_3.TabIndex = 10;
			this._txtFields_3.Name = "_txtFields_3";
			_txtFields_4.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_4.OcxState");
			this._txtFields_4.Size = new System.Drawing.Size(41, 25);
			this._txtFields_4.Location = new System.Drawing.Point(136, 96);
			this._txtFields_4.TabIndex = 11;
			this._txtFields_4.Name = "_txtFields_4";
			_txtFields_5.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_5.OcxState");
			this._txtFields_5.Size = new System.Drawing.Size(97, 25);
			this._txtFields_5.Location = new System.Drawing.Point(136, 120);
			this._txtFields_5.TabIndex = 12;
			this._txtFields_5.Name = "_txtFields_5";
			_txtFields_6.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtFields_6.OcxState");
			this._txtFields_6.Size = new System.Drawing.Size(49, 25);
			this._txtFields_6.Location = new System.Drawing.Point(136, 144);
			this._txtFields_6.TabIndex = 13;
			this._txtFields_6.Name = "_txtFields_6";
			this.Label1.Text = "Label1";
			this.Label1.Size = new System.Drawing.Size(257, 17);
			this.Label1.Location = new System.Drawing.Point(192, 152);
			this.Label1.TabIndex = 16;
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
			this._lblLabels_6.Text = "Company Number:";
			this._lblLabels_6.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_6.Location = new System.Drawing.Point(8, 144);
			this._lblLabels_6.TabIndex = 6;
			this._lblLabels_6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_6.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_6.Enabled = true;
			this._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_6.UseMnemonic = true;
			this._lblLabels_6.Visible = true;
			this._lblLabels_6.AutoSize = false;
			this._lblLabels_6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_6.Name = "_lblLabels_6";
			this._lblLabels_5.Text = "Zip:";
			this._lblLabels_5.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_5.Location = new System.Drawing.Point(8, 120);
			this._lblLabels_5.TabIndex = 5;
			this._lblLabels_5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_5.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_5.Enabled = true;
			this._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_5.UseMnemonic = true;
			this._lblLabels_5.Visible = true;
			this._lblLabels_5.AutoSize = false;
			this._lblLabels_5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_5.Name = "_lblLabels_5";
			this._lblLabels_4.Text = "State:";
			this._lblLabels_4.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_4.Location = new System.Drawing.Point(8, 96);
			this._lblLabels_4.TabIndex = 4;
			this._lblLabels_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_4.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_4.Enabled = true;
			this._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_4.UseMnemonic = true;
			this._lblLabels_4.Visible = true;
			this._lblLabels_4.AutoSize = false;
			this._lblLabels_4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_4.Name = "_lblLabels_4";
			this._lblLabels_3.Text = "City:";
			this._lblLabels_3.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_3.Location = new System.Drawing.Point(8, 72);
			this._lblLabels_3.TabIndex = 3;
			this._lblLabels_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_3.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_3.Enabled = true;
			this._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_3.UseMnemonic = true;
			this._lblLabels_3.Visible = true;
			this._lblLabels_3.AutoSize = false;
			this._lblLabels_3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_3.Name = "_lblLabels_3";
			this._lblLabels_2.Text = "Street Address:";
			this._lblLabels_2.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_2.Location = new System.Drawing.Point(8, 47);
			this._lblLabels_2.TabIndex = 2;
			this._lblLabels_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._lblLabels_2.BackColor = System.Drawing.SystemColors.Control;
			this._lblLabels_2.Enabled = true;
			this._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._lblLabels_2.UseMnemonic = true;
			this._lblLabels_2.Visible = true;
			this._lblLabels_2.AutoSize = false;
			this._lblLabels_2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lblLabels_2.Name = "_lblLabels_2";
			this._lblLabels_1.Text = "Name:";
			this._lblLabels_1.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_1.Location = new System.Drawing.Point(8, 26);
			this._lblLabels_1.TabIndex = 1;
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
			this._lblLabels_0.Text = "Department Number:";
			this._lblLabels_0.Size = new System.Drawing.Size(121, 17);
			this._lblLabels_0.Location = new System.Drawing.Point(8, 4);
			this._lblLabels_0.TabIndex = 0;
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
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_txtFields_0);
			this.Controls.Add(_txtFields_1);
			this.Controls.Add(_txtFields_2);
			this.Controls.Add(_txtFields_3);
			this.Controls.Add(_txtFields_4);
			this.Controls.Add(_txtFields_5);
			this.Controls.Add(_txtFields_6);
			this.Controls.Add(Label1);
			this.Controls.Add(_lblLabels_6);
			this.Controls.Add(_lblLabels_5);
			this.Controls.Add(_lblLabels_4);
			this.Controls.Add(_lblLabels_3);
			this.Controls.Add(_lblLabels_2);
			this.Controls.Add(_lblLabels_1);
			this.Controls.Add(_lblLabels_0);
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.lblLabels.SetIndex(_lblLabels_6, Convert.ToInt16(6));
			this.lblLabels.SetIndex(_lblLabels_5, Convert.ToInt16(5));
			this.lblLabels.SetIndex(_lblLabels_4, Convert.ToInt16(4));
			this.lblLabels.SetIndex(_lblLabels_3, Convert.ToInt16(3));
			this.lblLabels.SetIndex(_lblLabels_2, Convert.ToInt16(2));
			this.lblLabels.SetIndex(_lblLabels_1, Convert.ToInt16(1));
			this.lblLabels.SetIndex(_lblLabels_0, Convert.ToInt16(0));
			this.txtFields.SetIndex(_txtFields_0, Convert.ToInt16(0));
			this.txtFields.SetIndex(_txtFields_1, Convert.ToInt16(1));
			this.txtFields.SetIndex(_txtFields_2, Convert.ToInt16(2));
			this.txtFields.SetIndex(_txtFields_3, Convert.ToInt16(3));
			this.txtFields.SetIndex(_txtFields_4, Convert.ToInt16(4));
			this.txtFields.SetIndex(_txtFields_5, Convert.ToInt16(5));
			this.txtFields.SetIndex(_txtFields_6, Convert.ToInt16(6));
			((System.ComponentModel.ISupportInitialize)this.txtFields).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lblLabels).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_6).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_5).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_4).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_3).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_2).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtFields_0).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
