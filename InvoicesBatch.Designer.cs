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
	partial class InvoicesBatch
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public InvoicesBatch() : base()
		{
			FormClosed += InvoicesBatch_FormClosed;
			Resize += InvoicesBatch_Resize;
			Load += InvoicesBatch_Load;
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
		public System.Windows.Forms.Button _Cmd_4;
		public System.Windows.Forms.CheckBox chkScan;
		public System.Windows.Forms.CheckBox _chkDate_1;
		public System.Windows.Forms.CheckBox _chkDate_0;
		public AxMSComCtl2.AxDTPicker _txtDate_0;
		public AxMSComCtl2.AxDTPicker _txtDate_1;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Label Label1;
		private AxXGridLib.AxXGrid withEventsField_lstBatch;
		public AxXGridLib.AxXGrid lstBatch {
			get { return withEventsField_lstBatch; }
			set {
				if (withEventsField_lstBatch != null) {
					withEventsField_lstBatch.ClickCell -= lstBatch_ClickCell;
					withEventsField_lstBatch.DropSelChange -= lstBatch_DropSelChange;
					withEventsField_lstBatch.Validating -= lstBatch_Validating;
				}
				withEventsField_lstBatch = value;
				if (withEventsField_lstBatch != null) {
					withEventsField_lstBatch.ClickCell += lstBatch_ClickCell;
					withEventsField_lstBatch.DropSelChange += lstBatch_DropSelChange;
					withEventsField_lstBatch.Validating += lstBatch_Validating;
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
		private Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray withEventsField_chkDate;
		public Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray chkDate {
			get { return withEventsField_chkDate; }
			set {
				if (withEventsField_chkDate != null) {
					withEventsField_chkDate.CheckStateChanged -= chkDate_CheckStateChanged;
				}
				withEventsField_chkDate = value;
				if (withEventsField_chkDate != null) {
					withEventsField_chkDate.CheckStateChanged += chkDate_CheckStateChanged;
				}
			}
		}
		public AxDTPickerArray txtDate;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InvoicesBatch));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._Cmd_4 = new System.Windows.Forms.Button();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this.chkScan = new System.Windows.Forms.CheckBox();
			this._chkDate_1 = new System.Windows.Forms.CheckBox();
			this._chkDate_0 = new System.Windows.Forms.CheckBox();
			this._txtDate_0 = new AxMSComCtl2.AxDTPicker();
			this._txtDate_1 = new AxMSComCtl2.AxDTPicker();
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.lstBatch = new AxXGridLib.AxXGrid();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.chkDate = new Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(components);
			this.txtDate = new AxDTPickerArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this._txtDate_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtDate_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstBatch).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkDate).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtDate).BeginInit();
			this.Text = "Invoice Batch List";
			this.ClientSize = new System.Drawing.Size(574, 383);
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
			this.Name = "InvoicesBatch";
			this._Cmd_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_4.Text = "EDIT BATCH";
			this._Cmd_4.Size = new System.Drawing.Size(81, 33);
			this._Cmd_4.Location = new System.Drawing.Point(16, 240);
			this._Cmd_4.TabIndex = 7;
			this._Cmd_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_4.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_4.CausesValidation = true;
			this._Cmd_4.Enabled = true;
			this._Cmd_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_4.TabStop = true;
			this._Cmd_4.Name = "_Cmd_4";
			this.Frame1.Text = "Processing Options";
			this.Frame1.Size = new System.Drawing.Size(289, 89);
			this.Frame1.Location = new System.Drawing.Point(16, 280);
			this.Frame1.TabIndex = 5;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this.chkScan.Text = "Scan Invoices?";
			this.chkScan.Size = new System.Drawing.Size(121, 25);
			this.chkScan.Location = new System.Drawing.Point(8, 56);
			this.chkScan.TabIndex = 12;
			this.chkScan.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkScan.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkScan.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkScan.BackColor = System.Drawing.SystemColors.Control;
			this.chkScan.CausesValidation = true;
			this.chkScan.Enabled = true;
			this.chkScan.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkScan.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkScan.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkScan.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkScan.TabStop = true;
			this.chkScan.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkScan.Visible = true;
			this.chkScan.Name = "chkScan";
			this._chkDate_1.Text = "Assign Payment Due Date";
			this._chkDate_1.Size = new System.Drawing.Size(153, 25);
			this._chkDate_1.Location = new System.Drawing.Point(8, 40);
			this._chkDate_1.TabIndex = 9;
			this._chkDate_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._chkDate_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._chkDate_1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this._chkDate_1.BackColor = System.Drawing.SystemColors.Control;
			this._chkDate_1.CausesValidation = true;
			this._chkDate_1.Enabled = true;
			this._chkDate_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._chkDate_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._chkDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._chkDate_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._chkDate_1.TabStop = true;
			this._chkDate_1.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this._chkDate_1.Visible = true;
			this._chkDate_1.Name = "_chkDate_1";
			this._chkDate_0.Text = "Assign Invoice Date";
			this._chkDate_0.Size = new System.Drawing.Size(153, 25);
			this._chkDate_0.Location = new System.Drawing.Point(8, 16);
			this._chkDate_0.TabIndex = 6;
			this._chkDate_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._chkDate_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._chkDate_0.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this._chkDate_0.BackColor = System.Drawing.SystemColors.Control;
			this._chkDate_0.CausesValidation = true;
			this._chkDate_0.Enabled = true;
			this._chkDate_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._chkDate_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._chkDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._chkDate_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._chkDate_0.TabStop = true;
			this._chkDate_0.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this._chkDate_0.Visible = true;
			this._chkDate_0.Name = "_chkDate_0";
			_txtDate_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtDate_0.OcxState");
			this._txtDate_0.Size = new System.Drawing.Size(97, 25);
			this._txtDate_0.Location = new System.Drawing.Point(160, 24);
			this._txtDate_0.TabIndex = 8;
			this._txtDate_0.Name = "_txtDate_0";
			_txtDate_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtDate_1.OcxState");
			this._txtDate_1.Size = new System.Drawing.Size(97, 25);
			this._txtDate_1.Location = new System.Drawing.Point(160, 48);
			this._txtDate_1.TabIndex = 10;
			this._txtDate_1.Name = "_txtDate_1";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_3.Text = "PROCESS BATCH";
			this._Cmd_3.Size = new System.Drawing.Size(81, 33);
			this._Cmd_3.Location = new System.Drawing.Point(304, 240);
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
			this._Cmd_2.Text = "REMOVE BATCH";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(208, 240);
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
			this._Cmd_1.Text = "ADD A BATCH";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(112, 240);
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
			this._Cmd_0.Text = "EXIT";
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(400, 240);
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
			this.Label1.Text = "NOTE: To have BATCH PROCESSING automatically assign check numbers to invoices, set the Invoice Check Date to any date.";
			this.Label1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.Size = new System.Drawing.Size(241, 97);
			this.Label1.Location = new System.Drawing.Point(320, 280);
			this.Label1.TabIndex = 11;
			this.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Enabled = true;
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.UseMnemonic = true;
			this.Label1.Visible = true;
			this.Label1.AutoSize = false;
			this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label1.Name = "Label1";
			lstBatch.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstBatch.OcxState");
			this.lstBatch.Size = new System.Drawing.Size(553, 225);
			this.lstBatch.Location = new System.Drawing.Point(16, 8);
			this.lstBatch.TabIndex = 0;
			this.lstBatch.Name = "lstBatch";
			this.Cmd.SetIndex(_Cmd_4, Convert.ToInt16(4));
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.chkDate.SetIndex(_chkDate_1, Convert.ToInt16(1));
			this.chkDate.SetIndex(_chkDate_0, Convert.ToInt16(0));
			this.txtDate.SetIndex(_txtDate_0, Convert.ToInt16(0));
			this.txtDate.SetIndex(_txtDate_1, Convert.ToInt16(1));
			((System.ComponentModel.ISupportInitialize)this.txtDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstBatch).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtDate_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtDate_0).EndInit();
			this.Controls.Add(_Cmd_4);
			this.Controls.Add(Frame1);
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(Label1);
			this.Controls.Add(lstBatch);
			this.Frame1.Controls.Add(chkScan);
			this.Frame1.Controls.Add(_chkDate_1);
			this.Frame1.Controls.Add(_chkDate_0);
			this.Frame1.Controls.Add(_txtDate_0);
			this.Frame1.Controls.Add(_txtDate_1);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
