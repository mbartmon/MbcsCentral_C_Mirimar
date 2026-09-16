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
	partial class InvoiceNumber
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public InvoiceNumber() : base()
		{
			Load += InvoiceNumber_Load;
			Activated += InvoiceNumber_Activated;
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
		public System.Windows.Forms.CheckBox chkCheck;
		public AxMBText.AxTextField txtCheck;
		public AxMBText.AxTextField txtManual;
		private System.Windows.Forms.Button withEventsField_cmdInvoice;
		public System.Windows.Forms.Button cmdInvoice {
			get { return withEventsField_cmdInvoice; }
			set {
				if (withEventsField_cmdInvoice != null) {
					withEventsField_cmdInvoice.Click -= cmdInvoice_Click;
				}
				withEventsField_cmdInvoice = value;
				if (withEventsField_cmdInvoice != null) {
					withEventsField_cmdInvoice.Click += cmdInvoice_Click;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkDate;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMSComCtl2.AxDTPicker dtInvoice;
		public AxMSComCtl2.AxDTPicker dtCheck;
		public System.Windows.Forms.Label lblCheck;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label lblDepartment;
		public System.Windows.Forms.Label lblInvoice;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InvoiceNumber));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.chkCheck = new System.Windows.Forms.CheckBox();
			this.txtCheck = new AxMBText.AxTextField();
			this.txtManual = new AxMBText.AxTextField();
			this.cmdInvoice = new System.Windows.Forms.Button();
			this.chkDate = new System.Windows.Forms.CheckBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.dtInvoice = new AxMSComCtl2.AxDTPicker();
			this.dtCheck = new AxMSComCtl2.AxDTPicker();
			this.lblCheck = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.lblDepartment = new System.Windows.Forms.Label();
			this.lblInvoice = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.txtCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtManual).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtInvoice).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Enter Invoice Number";
			this.ClientSize = new System.Drawing.Size(429, 339);
			this.Location = new System.Drawing.Point(4, 23);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
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
			this.Name = "InvoiceNumber";
			this.chkCheck.Text = "Keep DATE && Increment Check Number";
			this.chkCheck.Size = new System.Drawing.Size(145, 25);
			this.chkCheck.Location = new System.Drawing.Point(281, 258);
			this.chkCheck.TabIndex = 13;
			this.chkCheck.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkCheck.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkCheck.BackColor = System.Drawing.SystemColors.Control;
			this.chkCheck.CausesValidation = true;
			this.chkCheck.Enabled = true;
			this.chkCheck.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkCheck.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkCheck.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkCheck.TabStop = true;
			this.chkCheck.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkCheck.Visible = true;
			this.chkCheck.Name = "chkCheck";
			txtCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtCheck.OcxState");
			this.txtCheck.Size = new System.Drawing.Size(74, 29);
			this.txtCheck.Location = new System.Drawing.Point(90, 258);
			this.txtCheck.TabIndex = 10;
			this.txtCheck.Name = "txtCheck";
			txtManual.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtManual.OcxState");
			this.txtManual.Size = new System.Drawing.Size(249, 33);
			this.txtManual.Location = new System.Drawing.Point(168, 152);
			this.txtManual.TabIndex = 2;
			this.txtManual.Visible = false;
			this.txtManual.Name = "txtManual";
			this.cmdInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdInvoice.Text = "Edit Invoice";
			this.cmdInvoice.Size = new System.Drawing.Size(73, 25);
			this.cmdInvoice.Location = new System.Drawing.Point(0, 299);
			this.cmdInvoice.TabIndex = 3;
			this.cmdInvoice.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdInvoice.BackColor = System.Drawing.SystemColors.Control;
			this.cmdInvoice.CausesValidation = true;
			this.cmdInvoice.Enabled = true;
			this.cmdInvoice.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdInvoice.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdInvoice.TabStop = true;
			this.cmdInvoice.Name = "cmdInvoice";
			this.chkDate.Text = "Use DATE for all Invoices";
			this.chkDate.Size = new System.Drawing.Size(145, 25);
			this.chkDate.Location = new System.Drawing.Point(280, 224);
			this.chkDate.TabIndex = 1;
			this.chkDate.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkDate.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkDate.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkDate.BackColor = System.Drawing.SystemColors.Control;
			this.chkDate.CausesValidation = true;
			this.chkDate.Enabled = true;
			this.chkDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkDate.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkDate.TabStop = true;
			this.chkDate.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkDate.Visible = true;
			this.chkDate.Name = "chkDate";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "SKIP INVOICE";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(272, 299);
			this._Cmd_1.TabIndex = 5;
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
			this._Cmd_0.Text = "ACCEPT";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(168, 299);
			this._Cmd_0.TabIndex = 4;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			dtInvoice.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtInvoice.OcxState");
			this.dtInvoice.Size = new System.Drawing.Size(105, 25);
			this.dtInvoice.Location = new System.Drawing.Point(170, 224);
			this.dtInvoice.TabIndex = 0;
			this.dtInvoice.Name = "dtInvoice";
			dtCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtCheck.OcxState");
			this.dtCheck.Size = new System.Drawing.Size(105, 25);
			this.dtCheck.Location = new System.Drawing.Point(170, 260);
			this.dtCheck.TabIndex = 12;
			this.dtCheck.Name = "dtCheck";
			this.lblCheck.Text = "Check #/Date";
			this.lblCheck.Size = new System.Drawing.Size(81, 14);
			this.lblCheck.Location = new System.Drawing.Point(5, 263);
			this.lblCheck.TabIndex = 11;
			this.lblCheck.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblCheck.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblCheck.BackColor = System.Drawing.SystemColors.Control;
			this.lblCheck.Enabled = true;
			this.lblCheck.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCheck.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCheck.UseMnemonic = true;
			this.lblCheck.Visible = true;
			this.lblCheck.AutoSize = false;
			this.lblCheck.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblCheck.Name = "lblCheck";
			this.Label2.Text = " ";
			this.Label2.Size = new System.Drawing.Size(153, 17);
			this.Label2.Location = new System.Drawing.Point(168, 208);
			this.Label2.TabIndex = 9;
			this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Enabled = true;
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.UseMnemonic = true;
			this.Label2.Visible = true;
			this.Label2.AutoSize = false;
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label2.Name = "Label2";
			this.Label1.Text = " ";
			this.Label1.Size = new System.Drawing.Size(153, 17);
			this.Label1.Location = new System.Drawing.Point(0, 208);
			this.Label1.TabIndex = 8;
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
			this.lblDepartment.BackColor = System.Drawing.SystemColors.Window;
			this.lblDepartment.Text = " ";
			this.lblDepartment.Size = new System.Drawing.Size(161, 25);
			this.lblDepartment.Location = new System.Drawing.Point(0, 224);
			this.lblDepartment.TabIndex = 7;
			this.lblDepartment.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblDepartment.Enabled = true;
			this.lblDepartment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDepartment.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblDepartment.UseMnemonic = true;
			this.lblDepartment.Visible = true;
			this.lblDepartment.AutoSize = false;
			this.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDepartment.Name = "lblDepartment";
			this.lblInvoice.BackColor = System.Drawing.SystemColors.Window;
			this.lblInvoice.Text = " ";
			this.lblInvoice.Size = new System.Drawing.Size(409, 185);
			this.lblInvoice.Location = new System.Drawing.Point(8, 8);
			this.lblInvoice.TabIndex = 6;
			this.lblInvoice.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblInvoice.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblInvoice.Enabled = true;
			this.lblInvoice.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblInvoice.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblInvoice.UseMnemonic = true;
			this.lblInvoice.Visible = true;
			this.lblInvoice.AutoSize = false;
			this.lblInvoice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblInvoice.Name = "lblInvoice";
			this.Controls.Add(chkCheck);
			this.Controls.Add(txtCheck);
			this.Controls.Add(txtManual);
			this.Controls.Add(cmdInvoice);
			this.Controls.Add(chkDate);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(dtInvoice);
			this.Controls.Add(dtCheck);
			this.Controls.Add(lblCheck);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Controls.Add(lblDepartment);
			this.Controls.Add(lblInvoice);
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtInvoice).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtManual).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtCheck).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
