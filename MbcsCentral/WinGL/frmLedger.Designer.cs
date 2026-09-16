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
	partial class frmLedger
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmLedger() : base()
		{
			FormClosed += frmLedger_FormClosed;
			Load += frmLedger_Load;
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
		private AxLpLib.AxfpCombo withEventsField_lstCompanies;
		public AxLpLib.AxfpCombo lstCompanies {
			get { return withEventsField_lstCompanies; }
			set {
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent -= lstCompanies_ClickEvent;
				}
				withEventsField_lstCompanies = value;
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent += lstCompanies_ClickEvent;
				}
			}
		}
		public AxLpLib.AxfpCombo lstDepartments;
		public AxLpLib.AxfpCombo lstAccounts;
		public System.Windows.Forms.CheckBox chkBalance;
		private System.Windows.Forms.CheckBox withEventsField_chkDepartment;
		public System.Windows.Forms.CheckBox chkDepartment {
			get { return withEventsField_chkDepartment; }
			set {
				if (withEventsField_chkDepartment != null) {
					withEventsField_chkDepartment.CheckStateChanged -= chkDepartment_CheckStateChanged;
				}
				withEventsField_chkDepartment = value;
				if (withEventsField_chkDepartment != null) {
					withEventsField_chkDepartment.CheckStateChanged += chkDepartment_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMSComCtl2.AxDTPicker dtStart;
		private System.Windows.Forms.CheckBox withEventsField_chkAccount;
		public System.Windows.Forms.CheckBox chkAccount {
			get { return withEventsField_chkAccount; }
			set {
				if (withEventsField_chkAccount != null) {
					withEventsField_chkAccount.CheckStateChanged -= chkAccount_CheckStateChanged;
				}
				withEventsField_chkAccount = value;
				if (withEventsField_chkAccount != null) {
					withEventsField_chkAccount.CheckStateChanged += chkAccount_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkConsolidate;
		public AxMSComCtl2.AxDTPicker dtEnd;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmLedger));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.chkBalance = new System.Windows.Forms.CheckBox();
			this.chkDepartment = new System.Windows.Forms.CheckBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.dtStart = new AxMSComCtl2.AxDTPicker();
			this.chkAccount = new System.Windows.Forms.CheckBox();
			this.chkConsolidate = new System.Windows.Forms.CheckBox();
			this.dtEnd = new AxMSComCtl2.AxDTPicker();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "General Ledger Report";
			this.ClientSize = new System.Drawing.Size(355, 280);
			this.Location = new System.Drawing.Point(4, 24);
			this.ControlBox = false;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Enabled = true;
			this.KeyPreview = false;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmLedger";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(268, 18);
			this.lstCompanies.Location = new System.Drawing.Point(71, 8);
			this.lstCompanies.TabIndex = 1;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(268, 18);
			this.lstDepartments.Location = new System.Drawing.Point(71, 93);
			this.lstDepartments.TabIndex = 4;
			this.lstDepartments.Name = "lstDepartments";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(268, 18);
			this.lstAccounts.Location = new System.Drawing.Point(71, 152);
			this.lstAccounts.TabIndex = 7;
			this.lstAccounts.Name = "lstAccounts";
			this.chkBalance.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkBalance.Text = "Print Accounts with 0 balance?";
			this.chkBalance.Size = new System.Drawing.Size(170, 19);
			this.chkBalance.Location = new System.Drawing.Point(168, 122);
			this.chkBalance.TabIndex = 15;
			this.chkBalance.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkBalance.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkBalance.BackColor = System.Drawing.SystemColors.Control;
			this.chkBalance.CausesValidation = true;
			this.chkBalance.Enabled = true;
			this.chkBalance.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkBalance.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkBalance.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkBalance.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkBalance.TabStop = true;
			this.chkBalance.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkBalance.Visible = true;
			this.chkBalance.Name = "chkBalance";
			this.chkDepartment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkDepartment.Text = "Print Single Department?";
			this.chkDepartment.Size = new System.Drawing.Size(147, 19);
			this.chkDepartment.Location = new System.Drawing.Point(9, 67);
			this.chkDepartment.TabIndex = 14;
			this.chkDepartment.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkDepartment.BackColor = System.Drawing.SystemColors.Control;
			this.chkDepartment.CausesValidation = true;
			this.chkDepartment.Enabled = true;
			this.chkDepartment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkDepartment.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkDepartment.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkDepartment.TabStop = true;
			this.chkDepartment.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkDepartment.Visible = true;
			this.chkDepartment.Name = "chkDepartment";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(103, 237);
			this._Cmd_1.TabIndex = 13;
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
			this._Cmd_0.Text = "DISPLAY";
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(8, 237);
			this._Cmd_0.TabIndex = 12;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			dtStart.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtStart.OcxState");
			this.dtStart.Size = new System.Drawing.Size(100, 23);
			this.dtStart.Location = new System.Drawing.Point(70, 196);
			this.dtStart.TabIndex = 8;
			this.dtStart.Name = "dtStart";
			this.chkAccount.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkAccount.Text = "Print Single Account?";
			this.chkAccount.Size = new System.Drawing.Size(147, 19);
			this.chkAccount.Location = new System.Drawing.Point(9, 122);
			this.chkAccount.TabIndex = 5;
			this.chkAccount.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkAccount.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkAccount.BackColor = System.Drawing.SystemColors.Control;
			this.chkAccount.CausesValidation = true;
			this.chkAccount.Enabled = true;
			this.chkAccount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkAccount.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkAccount.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkAccount.TabStop = true;
			this.chkAccount.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkAccount.Visible = true;
			this.chkAccount.Name = "chkAccount";
			this.chkConsolidate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkConsolidate.Text = "Consolidate Departments?";
			this.chkConsolidate.Size = new System.Drawing.Size(147, 19);
			this.chkConsolidate.Location = new System.Drawing.Point(9, 39);
			this.chkConsolidate.TabIndex = 2;
			this.chkConsolidate.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkConsolidate.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkConsolidate.BackColor = System.Drawing.SystemColors.Control;
			this.chkConsolidate.CausesValidation = true;
			this.chkConsolidate.Enabled = true;
			this.chkConsolidate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkConsolidate.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkConsolidate.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkConsolidate.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkConsolidate.TabStop = true;
			this.chkConsolidate.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkConsolidate.Visible = true;
			this.chkConsolidate.Name = "chkConsolidate";
			dtEnd.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtEnd.OcxState");
			this.dtEnd.Size = new System.Drawing.Size(100, 23);
			this.dtEnd.Location = new System.Drawing.Point(239, 196);
			this.dtEnd.TabIndex = 11;
			this.dtEnd.Name = "dtEnd";
			this.Label5.Text = "End Date:";
			this.Label5.Size = new System.Drawing.Size(52, 14);
			this.Label5.Location = new System.Drawing.Point(179, 197);
			this.Label5.TabIndex = 10;
			this.Label5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label5.BackColor = System.Drawing.SystemColors.Control;
			this.Label5.Enabled = true;
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.UseMnemonic = true;
			this.Label5.Visible = true;
			this.Label5.AutoSize = false;
			this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label5.Name = "Label5";
			this.Label4.Text = "Start Date";
			this.Label4.Size = new System.Drawing.Size(52, 14);
			this.Label4.Location = new System.Drawing.Point(9, 197);
			this.Label4.TabIndex = 9;
			this.Label4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Enabled = true;
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.UseMnemonic = true;
			this.Label4.Visible = true;
			this.Label4.AutoSize = false;
			this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label4.Name = "Label4";
			this.Label3.Text = "Account:";
			this.Label3.Size = new System.Drawing.Size(52, 14);
			this.Label3.Location = new System.Drawing.Point(9, 153);
			this.Label3.TabIndex = 6;
			this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label3.BackColor = System.Drawing.SystemColors.Control;
			this.Label3.Enabled = true;
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.UseMnemonic = true;
			this.Label3.Visible = true;
			this.Label3.AutoSize = false;
			this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label3.Name = "Label3";
			this.Label2.Text = "Department:";
			this.Label2.Size = new System.Drawing.Size(69, 14);
			this.Label2.Location = new System.Drawing.Point(9, 95);
			this.Label2.TabIndex = 3;
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
			this.Label1.Text = "Company:";
			this.Label1.Size = new System.Drawing.Size(51, 17);
			this.Label1.Location = new System.Drawing.Point(9, 8);
			this.Label1.TabIndex = 0;
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
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(chkBalance);
			this.Controls.Add(chkDepartment);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(dtStart);
			this.Controls.Add(chkAccount);
			this.Controls.Add(chkConsolidate);
			this.Controls.Add(dtEnd);
			this.Controls.Add(Label5);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtStart).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
