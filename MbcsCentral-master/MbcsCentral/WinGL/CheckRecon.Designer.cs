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
	partial class CheckRecon
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public CheckRecon() : base()
		{
			FormClosed += CheckRecon_FormClosed;
			Resize += CheckRecon_Resize;
			Load += CheckRecon_Load;
			Activated += CheckRecon_Activated;
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
		public AxLpLib.AxfpCombo lstDepartments;
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
		private AxLpLib.AxfpCombo withEventsField_lstAccounts;
		public AxLpLib.AxfpCombo lstAccounts {
			get { return withEventsField_lstAccounts; }
			set {
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.ClickEvent -= lstAccounts_ClickEvent;
				}
				withEventsField_lstAccounts = value;
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.ClickEvent += lstAccounts_ClickEvent;
				}
			}
		}
		public System.Windows.Forms.Button _cmdAdj_3;
		public System.Windows.Forms.Button _cmdAdj_2;
		public System.Windows.Forms.Button _cmdAdj_1;
		public System.Windows.Forms.Button _cmdFinish_5;
		public System.Windows.Forms.Button _cmdFinish_4;
		public System.Windows.Forms.OpenFileDialog dlgSaveOpen;
		public System.Windows.Forms.SaveFileDialog dlgSaveSave;
		public System.Windows.Forms.Button _cmdFinish_3;
		public System.Windows.Forms.Button _cmdFinish_2;
		public System.Windows.Forms.Button _cmdFinish_1;
		public System.Windows.Forms.Button _cmdFinish_0;
		public System.Windows.Forms.TextBox _txtTotal_6;
		public System.Windows.Forms.TextBox _txtTotal_5;
		public System.Windows.Forms.TextBox _txtTotal_4;
		public System.Windows.Forms.TextBox _txtTotal_3;
		public System.Windows.Forms.TextBox _txtTotal_2;
		public System.Windows.Forms.TextBox _txtTotal_1;
		public System.Windows.Forms.TextBox _txtTotal_0;
		private AxMSComCtl2.AxDTPicker withEventsField_dtStatement;
		public AxMSComCtl2.AxDTPicker dtStatement {
			get { return withEventsField_dtStatement; }
			set {
				if (withEventsField_dtStatement != null) {
					withEventsField_dtStatement.Change -= dtStatement_Change;
				}
				withEventsField_dtStatement = value;
				if (withEventsField_dtStatement != null) {
					withEventsField_dtStatement.Change += dtStatement_Change;
				}
			}
		}
		public AxMBText.AxTextField _txtBook_0;
		public System.Windows.Forms.Button _cmdAdj_0;
		public AxMSComCtl2.AxDTPicker dtClear;
		private System.Windows.Forms.Button withEventsField_cmdClear;
		public System.Windows.Forms.Button cmdClear {
			get { return withEventsField_cmdClear; }
			set {
				if (withEventsField_cmdClear != null) {
					withEventsField_cmdClear.Click -= cmdClear_Click;
				}
				withEventsField_cmdClear = value;
				if (withEventsField_cmdClear != null) {
					withEventsField_cmdClear.Click += cmdClear_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_cmdChecks;
		public System.Windows.Forms.Button cmdChecks {
			get { return withEventsField_cmdChecks; }
			set {
				if (withEventsField_cmdChecks != null) {
					withEventsField_cmdChecks.Click -= cmdChecks_Click;
				}
				withEventsField_cmdChecks = value;
				if (withEventsField_cmdChecks != null) {
					withEventsField_cmdChecks.Click += cmdChecks_Click;
				}
			}
		}
		private AxFPSpread.AxvaSpread withEventsField_grdChecks;
		public AxFPSpread.AxvaSpread grdChecks {
			get { return withEventsField_grdChecks; }
			set {
				if (withEventsField_grdChecks != null) {
					withEventsField_grdChecks.Change -= grdChecks_Change;
					withEventsField_grdChecks.LeaveCell -= grdChecks_LeaveCell;
					withEventsField_grdChecks.ButtonClicked -= grdChecks_ButtonClicked;
				}
				withEventsField_grdChecks = value;
				if (withEventsField_grdChecks != null) {
					withEventsField_grdChecks.Change += grdChecks_Change;
					withEventsField_grdChecks.LeaveCell += grdChecks_LeaveCell;
					withEventsField_grdChecks.ButtonClicked += grdChecks_ButtonClicked;
				}
			}
		}
		private AxMBText.AxTextField withEventsField_txtStatement;
		public AxMBText.AxTextField txtStatement {
			get { return withEventsField_txtStatement; }
			set {
				if (withEventsField_txtStatement != null) {
					withEventsField_txtStatement.Leave -= txtStatement_Leave;
				}
				withEventsField_txtStatement = value;
				if (withEventsField_txtStatement != null) {
					withEventsField_txtStatement.Leave += txtStatement_Leave;
				}
			}
		}
		public AxMBText.AxTextField _txtBook_1;
		public AxMBText.AxTextField _txtBook_2;
		public System.Windows.Forms.Label Label11;
		public System.Windows.Forms.Label Label6;
		public System.Windows.Forms.Label Label10;
		public System.Windows.Forms.Label Label9;
		public System.Windows.Forms.Label _Label8_1;
		public System.Windows.Forms.Label _Label8_0;
		public System.Windows.Forms.Label Label7;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label _Label1_0;
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label8;
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_cmdAdj;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray cmdAdj {
			get { return withEventsField_cmdAdj; }
			set {
				if (withEventsField_cmdAdj != null) {
					withEventsField_cmdAdj.Click -= cmdAdj_Click;
				}
				withEventsField_cmdAdj = value;
				if (withEventsField_cmdAdj != null) {
					withEventsField_cmdAdj.Click += cmdAdj_Click;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_cmdFinish;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray cmdFinish {
			get { return withEventsField_cmdFinish; }
			set {
				if (withEventsField_cmdFinish != null) {
					withEventsField_cmdFinish.Click -= cmdFinish_Click;
				}
				withEventsField_cmdFinish = value;
				if (withEventsField_cmdFinish != null) {
					withEventsField_cmdFinish.Click += cmdFinish_Click;
				}
			}
		}
		public AxTextFieldArray txtBook;
		public Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray txtTotal;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CheckRecon));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this._cmdAdj_3 = new System.Windows.Forms.Button();
			this._cmdAdj_2 = new System.Windows.Forms.Button();
			this._cmdAdj_1 = new System.Windows.Forms.Button();
			this._cmdFinish_5 = new System.Windows.Forms.Button();
			this._cmdFinish_4 = new System.Windows.Forms.Button();
			this.dlgSaveOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveSave = new System.Windows.Forms.SaveFileDialog();
			this._cmdFinish_3 = new System.Windows.Forms.Button();
			this._cmdFinish_2 = new System.Windows.Forms.Button();
			this._cmdFinish_1 = new System.Windows.Forms.Button();
			this._cmdFinish_0 = new System.Windows.Forms.Button();
			this._txtTotal_6 = new System.Windows.Forms.TextBox();
			this._txtTotal_5 = new System.Windows.Forms.TextBox();
			this._txtTotal_4 = new System.Windows.Forms.TextBox();
			this._txtTotal_3 = new System.Windows.Forms.TextBox();
			this._txtTotal_2 = new System.Windows.Forms.TextBox();
			this._txtTotal_1 = new System.Windows.Forms.TextBox();
			this._txtTotal_0 = new System.Windows.Forms.TextBox();
			this.dtStatement = new AxMSComCtl2.AxDTPicker();
			this._txtBook_0 = new AxMBText.AxTextField();
			this._cmdAdj_0 = new System.Windows.Forms.Button();
			this.dtClear = new AxMSComCtl2.AxDTPicker();
			this.cmdClear = new System.Windows.Forms.Button();
			this.cmdChecks = new System.Windows.Forms.Button();
			this.grdChecks = new AxFPSpread.AxvaSpread();
			this.txtStatement = new AxMBText.AxTextField();
			this._txtBook_1 = new AxMBText.AxTextField();
			this._txtBook_2 = new AxMBText.AxTextField();
			this.Label11 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this._Label8_1 = new System.Windows.Forms.Label();
			this._Label8_0 = new System.Windows.Forms.Label();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.Label8 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.cmdAdj = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.cmdFinish = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.txtBook = new AxTextFieldArray(components);
			this.txtTotal = new Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtStatement).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtClear).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdChecks).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtStatement).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label8).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cmdAdj).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cmdFinish).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtBook).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Bank Reconciliation";
			this.ClientSize = new System.Drawing.Size(658, 425);
			this.Location = new System.Drawing.Point(-14, 5);
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
			this.Name = "CheckRecon";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(205, 19);
			this.lstDepartments.Location = new System.Drawing.Point(68, 21);
			this.lstDepartments.TabIndex = 1;
			this.lstDepartments.Name = "lstDepartments";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(205, 19);
			this.lstCompanies.Location = new System.Drawing.Point(68, 1);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(217, 19);
			this.lstAccounts.Location = new System.Drawing.Point(132, 338);
			this.lstAccounts.TabIndex = 30;
			this.lstAccounts.Name = "lstAccounts";
			this._cmdAdj_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdAdj_3.Text = "Bank Error In-Favor";
			this._cmdAdj_3.Size = new System.Drawing.Size(66, 29);
			this._cmdAdj_3.Location = new System.Drawing.Point(65, 344);
			this._cmdAdj_3.TabIndex = 37;
			this._cmdAdj_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdAdj_3.BackColor = System.Drawing.SystemColors.Control;
			this._cmdAdj_3.CausesValidation = true;
			this._cmdAdj_3.Enabled = true;
			this._cmdAdj_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdAdj_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdAdj_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdAdj_3.TabStop = true;
			this._cmdAdj_3.Name = "_cmdAdj_3";
			this._cmdAdj_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdAdj_2.Text = "Bank Error Against";
			this._cmdAdj_2.Size = new System.Drawing.Size(66, 29);
			this._cmdAdj_2.Location = new System.Drawing.Point(65, 316);
			this._cmdAdj_2.TabIndex = 36;
			this._cmdAdj_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdAdj_2.BackColor = System.Drawing.SystemColors.Control;
			this._cmdAdj_2.CausesValidation = true;
			this._cmdAdj_2.Enabled = true;
			this._cmdAdj_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdAdj_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdAdj_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdAdj_2.TabStop = true;
			this._cmdAdj_2.Name = "_cmdAdj_2";
			this._cmdAdj_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdAdj_1.Text = "Add Debit";
			this._cmdAdj_1.Size = new System.Drawing.Size(66, 29);
			this._cmdAdj_1.Location = new System.Drawing.Point(0, 344);
			this._cmdAdj_1.TabIndex = 35;
			this._cmdAdj_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdAdj_1.BackColor = System.Drawing.SystemColors.Control;
			this._cmdAdj_1.CausesValidation = true;
			this._cmdAdj_1.Enabled = true;
			this._cmdAdj_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdAdj_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdAdj_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdAdj_1.TabStop = true;
			this._cmdAdj_1.Name = "_cmdAdj_1";
			this._cmdFinish_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_5.Text = "EXIT";
			this._cmdFinish_5.Size = new System.Drawing.Size(61, 27);
			this._cmdFinish_5.Location = new System.Drawing.Point(588, 382);
			this._cmdFinish_5.TabIndex = 34;
			this._cmdFinish_5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_5.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_5.CausesValidation = true;
			this._cmdFinish_5.Enabled = true;
			this._cmdFinish_5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_5.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_5.TabStop = true;
			this._cmdFinish_5.Name = "_cmdFinish_5";
			this._cmdFinish_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_4.Text = "REPORT";
			this._cmdFinish_4.Size = new System.Drawing.Size(61, 27);
			this._cmdFinish_4.Location = new System.Drawing.Point(527, 382);
			this._cmdFinish_4.TabIndex = 33;
			this._cmdFinish_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_4.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_4.CausesValidation = true;
			this._cmdFinish_4.Enabled = true;
			this._cmdFinish_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_4.TabStop = true;
			this._cmdFinish_4.Name = "_cmdFinish_4";
			this.dlgSaveOpen.DefaultExt = "SS3";
			this.dlgSaveSave.DefaultExt = "SS3";
			this._cmdFinish_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_3.Text = "EXPORT";
			this._cmdFinish_3.Size = new System.Drawing.Size(63, 27);
			this._cmdFinish_3.Location = new System.Drawing.Point(464, 382);
			this._cmdFinish_3.TabIndex = 32;
			this._cmdFinish_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_3.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_3.CausesValidation = true;
			this._cmdFinish_3.Enabled = true;
			this._cmdFinish_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_3.TabStop = true;
			this._cmdFinish_3.Name = "_cmdFinish_3";
			this._cmdFinish_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_2.Text = "RELOAD";
			this._cmdFinish_2.Size = new System.Drawing.Size(63, 27);
			this._cmdFinish_2.Location = new System.Drawing.Point(401, 382);
			this._cmdFinish_2.TabIndex = 29;
			this._cmdFinish_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_2.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_2.CausesValidation = true;
			this._cmdFinish_2.Enabled = true;
			this._cmdFinish_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_2.TabStop = true;
			this._cmdFinish_2.Name = "_cmdFinish_2";
			this._cmdFinish_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_1.Text = "SAVE";
			this._cmdFinish_1.Size = new System.Drawing.Size(63, 27);
			this._cmdFinish_1.Location = new System.Drawing.Point(338, 382);
			this._cmdFinish_1.TabIndex = 28;
			this._cmdFinish_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_1.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_1.CausesValidation = true;
			this._cmdFinish_1.Enabled = true;
			this._cmdFinish_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_1.TabStop = true;
			this._cmdFinish_1.Name = "_cmdFinish_1";
			this._cmdFinish_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdFinish_0.Text = "ACCEPT";
			this._cmdFinish_0.Size = new System.Drawing.Size(63, 27);
			this._cmdFinish_0.Location = new System.Drawing.Point(275, 382);
			this._cmdFinish_0.TabIndex = 27;
			this._cmdFinish_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdFinish_0.BackColor = System.Drawing.SystemColors.Control;
			this._cmdFinish_0.CausesValidation = true;
			this._cmdFinish_0.Enabled = true;
			this._cmdFinish_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdFinish_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdFinish_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdFinish_0.TabStop = true;
			this._cmdFinish_0.Name = "_cmdFinish_0";
			this._txtTotal_6.AutoSize = false;
			this._txtTotal_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_6.Enabled = false;
			this._txtTotal_6.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_6.Size = new System.Drawing.Size(102, 21);
			this._txtTotal_6.Location = new System.Drawing.Point(552, 359);
			this._txtTotal_6.TabIndex = 25;
			this._txtTotal_6.AcceptsReturn = true;
			this._txtTotal_6.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_6.CausesValidation = true;
			this._txtTotal_6.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_6.HideSelection = true;
			this._txtTotal_6.ReadOnly = false;
			this._txtTotal_6.MaxLength = 0;
			this._txtTotal_6.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_6.Multiline = false;
			this._txtTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_6.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_6.TabStop = true;
			this._txtTotal_6.Visible = true;
			this._txtTotal_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_6.Name = "_txtTotal_6";
			this._txtTotal_5.AutoSize = false;
			this._txtTotal_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_5.Enabled = false;
			this._txtTotal_5.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_5.Size = new System.Drawing.Size(102, 21);
			this._txtTotal_5.Location = new System.Drawing.Point(552, 338);
			this._txtTotal_5.TabIndex = 24;
			this._txtTotal_5.AcceptsReturn = true;
			this._txtTotal_5.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_5.CausesValidation = true;
			this._txtTotal_5.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_5.HideSelection = true;
			this._txtTotal_5.ReadOnly = false;
			this._txtTotal_5.MaxLength = 0;
			this._txtTotal_5.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_5.Multiline = false;
			this._txtTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_5.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_5.TabStop = true;
			this._txtTotal_5.Visible = true;
			this._txtTotal_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_5.Name = "_txtTotal_5";
			this._txtTotal_4.AutoSize = false;
			this._txtTotal_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_4.Enabled = false;
			this._txtTotal_4.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_4.Size = new System.Drawing.Size(102, 21);
			this._txtTotal_4.Location = new System.Drawing.Point(552, 317);
			this._txtTotal_4.TabIndex = 23;
			this._txtTotal_4.AcceptsReturn = true;
			this._txtTotal_4.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_4.CausesValidation = true;
			this._txtTotal_4.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_4.HideSelection = true;
			this._txtTotal_4.ReadOnly = false;
			this._txtTotal_4.MaxLength = 0;
			this._txtTotal_4.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_4.Multiline = false;
			this._txtTotal_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_4.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_4.TabStop = true;
			this._txtTotal_4.Visible = true;
			this._txtTotal_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_4.Name = "_txtTotal_4";
			this._txtTotal_3.AutoSize = false;
			this._txtTotal_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_3.Enabled = false;
			this._txtTotal_3.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_3.Size = new System.Drawing.Size(102, 21);
			this._txtTotal_3.Location = new System.Drawing.Point(552, 296);
			this._txtTotal_3.TabIndex = 22;
			this._txtTotal_3.AcceptsReturn = true;
			this._txtTotal_3.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_3.CausesValidation = true;
			this._txtTotal_3.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_3.HideSelection = true;
			this._txtTotal_3.ReadOnly = false;
			this._txtTotal_3.MaxLength = 0;
			this._txtTotal_3.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_3.Multiline = false;
			this._txtTotal_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_3.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_3.TabStop = true;
			this._txtTotal_3.Visible = true;
			this._txtTotal_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_3.Name = "_txtTotal_3";
			this._txtTotal_2.AutoSize = false;
			this._txtTotal_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_2.Enabled = false;
			this._txtTotal_2.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_2.Size = new System.Drawing.Size(104, 21);
			this._txtTotal_2.Location = new System.Drawing.Point(273, 317);
			this._txtTotal_2.TabIndex = 21;
			this._txtTotal_2.AcceptsReturn = true;
			this._txtTotal_2.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_2.CausesValidation = true;
			this._txtTotal_2.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_2.HideSelection = true;
			this._txtTotal_2.ReadOnly = false;
			this._txtTotal_2.MaxLength = 0;
			this._txtTotal_2.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_2.Multiline = false;
			this._txtTotal_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_2.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_2.TabStop = true;
			this._txtTotal_2.Visible = true;
			this._txtTotal_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_2.Name = "_txtTotal_2";
			this._txtTotal_1.AutoSize = false;
			this._txtTotal_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_1.Enabled = false;
			this._txtTotal_1.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_1.Size = new System.Drawing.Size(104, 21);
			this._txtTotal_1.Location = new System.Drawing.Point(273, 296);
			this._txtTotal_1.TabIndex = 20;
			this._txtTotal_1.AcceptsReturn = true;
			this._txtTotal_1.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_1.CausesValidation = true;
			this._txtTotal_1.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_1.HideSelection = true;
			this._txtTotal_1.ReadOnly = false;
			this._txtTotal_1.MaxLength = 0;
			this._txtTotal_1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_1.Multiline = false;
			this._txtTotal_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_1.TabStop = true;
			this._txtTotal_1.Visible = true;
			this._txtTotal_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_1.Name = "_txtTotal_1";
			this._txtTotal_0.AutoSize = false;
			this._txtTotal_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtTotal_0.Enabled = false;
			this._txtTotal_0.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._txtTotal_0.Size = new System.Drawing.Size(106, 21);
			this._txtTotal_0.Location = new System.Drawing.Point(168, 296);
			this._txtTotal_0.TabIndex = 19;
			this._txtTotal_0.AcceptsReturn = true;
			this._txtTotal_0.BackColor = System.Drawing.SystemColors.Window;
			this._txtTotal_0.CausesValidation = true;
			this._txtTotal_0.ForeColor = System.Drawing.SystemColors.WindowText;
			this._txtTotal_0.HideSelection = true;
			this._txtTotal_0.ReadOnly = false;
			this._txtTotal_0.MaxLength = 0;
			this._txtTotal_0.Cursor = System.Windows.Forms.Cursors.IBeam;
			this._txtTotal_0.Multiline = false;
			this._txtTotal_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtTotal_0.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this._txtTotal_0.TabStop = true;
			this._txtTotal_0.Visible = true;
			this._txtTotal_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._txtTotal_0.Name = "_txtTotal_0";
			dtStatement.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtStatement.OcxState");
			this.dtStatement.Size = new System.Drawing.Size(84, 22);
			this.dtStatement.Location = new System.Drawing.Point(552, 25);
			this.dtStatement.TabIndex = 5;
			this.dtStatement.Name = "dtStatement";
			_txtBook_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtBook_0.OcxState");
			this._txtBook_0.Size = new System.Drawing.Size(107, 24);
			this._txtBook_0.Location = new System.Drawing.Point(368, 1);
			this._txtBook_0.TabIndex = 3;
			this._txtBook_0.Name = "_txtBook_0";
			this._cmdAdj_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdAdj_0.Text = "Add Deposit";
			this._cmdAdj_0.Size = new System.Drawing.Size(66, 29);
			this._cmdAdj_0.Location = new System.Drawing.Point(0, 316);
			this._cmdAdj_0.TabIndex = 9;
			this._cmdAdj_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdAdj_0.BackColor = System.Drawing.SystemColors.Control;
			this._cmdAdj_0.CausesValidation = true;
			this._cmdAdj_0.Enabled = true;
			this._cmdAdj_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdAdj_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdAdj_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdAdj_0.TabStop = true;
			this._cmdAdj_0.Name = "_cmdAdj_0";
			dtClear.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtClear.OcxState");
			this.dtClear.Size = new System.Drawing.Size(100, 27);
			this.dtClear.Location = new System.Drawing.Point(141, 375);
			this.dtClear.TabIndex = 8;
			this.dtClear.Name = "dtClear";
			this.cmdClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdClear.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
			this.cmdClear.Text = "Clear ALL items with date on or before:";
			this.cmdClear.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdClear.Size = new System.Drawing.Size(140, 43);
			this.cmdClear.Location = new System.Drawing.Point(1, 376);
			this.cmdClear.TabIndex = 7;
			this.cmdClear.CausesValidation = true;
			this.cmdClear.Enabled = true;
			this.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdClear.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdClear.TabStop = true;
			this.cmdClear.Name = "cmdClear";
			this.cmdChecks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdChecks.Text = "Retrieve Items";
			this.AcceptButton = this.cmdChecks;
			this.cmdChecks.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdChecks.Size = new System.Drawing.Size(204, 31);
			this.cmdChecks.Location = new System.Drawing.Point(68, 41);
			this.cmdChecks.TabIndex = 2;
			this.cmdChecks.BackColor = System.Drawing.SystemColors.Control;
			this.cmdChecks.CausesValidation = true;
			this.cmdChecks.Enabled = true;
			this.cmdChecks.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdChecks.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdChecks.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdChecks.TabStop = true;
			this.cmdChecks.Name = "cmdChecks";
			grdChecks.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdChecks.OcxState");
			this.grdChecks.Size = new System.Drawing.Size(654, 225);
			this.grdChecks.Location = new System.Drawing.Point(0, 71);
			this.grdChecks.TabIndex = 6;
			this.grdChecks.Name = "grdChecks";
			txtStatement.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtStatement.OcxState");
			this.txtStatement.Size = new System.Drawing.Size(107, 24);
			this.txtStatement.Location = new System.Drawing.Point(551, 1);
			this.txtStatement.TabIndex = 4;
			this.txtStatement.Name = "txtStatement";
			_txtBook_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtBook_1.OcxState");
			this._txtBook_1.Size = new System.Drawing.Size(107, 24);
			this._txtBook_1.Location = new System.Drawing.Point(368, 24);
			this._txtBook_1.TabIndex = 38;
			this._txtBook_1.Name = "_txtBook_1";
			_txtBook_2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtBook_2.OcxState");
			this._txtBook_2.Size = new System.Drawing.Size(107, 24);
			this._txtBook_2.Location = new System.Drawing.Point(368, 46);
			this._txtBook_2.TabIndex = 39;
			this._txtBook_2.Name = "_txtBook_2";
			this.Label11.Text = "Adj Book Bal";
			this.Label11.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label11.Size = new System.Drawing.Size(82, 15);
			this.Label11.Location = new System.Drawing.Point(281, 49);
			this.Label11.TabIndex = 41;
			this.Label11.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label11.BackColor = System.Drawing.SystemColors.Control;
			this.Label11.Enabled = true;
			this.Label11.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label11.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label11.UseMnemonic = true;
			this.Label11.Visible = true;
			this.Label11.AutoSize = false;
			this.Label11.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label11.Name = "Label11";
			this.Label6.Text = "Adjustments";
			this.Label6.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label6.Size = new System.Drawing.Size(82, 15);
			this.Label6.Location = new System.Drawing.Point(281, 28);
			this.Label6.TabIndex = 40;
			this.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label6.BackColor = System.Drawing.SystemColors.Control;
			this.Label6.Enabled = true;
			this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label6.UseMnemonic = true;
			this.Label6.Visible = true;
			this.Label6.AutoSize = false;
			this.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label6.Name = "Label6";
			this.Label10.BackColor = System.Drawing.Color.Yellow;
			this.Label10.Text = "Enter a NON-CASH account for this addition";
			this.Label10.Size = new System.Drawing.Size(218, 16);
			this.Label10.Location = new System.Drawing.Point(131, 359);
			this.Label10.TabIndex = 31;
			this.Label10.Visible = false;
			this.Label10.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label10.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label10.Enabled = true;
			this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label10.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label10.UseMnemonic = true;
			this.Label10.AutoSize = false;
			this.Label10.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label10.Name = "Label10";
			this.Label9.Text = "+ Uncleared Deposits:";
			this.Label9.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label9.Size = new System.Drawing.Size(172, 20);
			this.Label9.Location = new System.Drawing.Point(393, 297);
			this.Label9.TabIndex = 26;
			this.Label9.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label9.BackColor = System.Drawing.SystemColors.Control;
			this.Label9.Enabled = true;
			this.Label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label9.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label9.UseMnemonic = true;
			this.Label9.Visible = true;
			this.Label9.AutoSize = false;
			this.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label9.Name = "Label9";
			this._Label8_1.Text = "Adjusted Statement Balance:";
			this._Label8_1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label8_1.Size = new System.Drawing.Size(203, 20);
			this._Label8_1.Location = new System.Drawing.Point(352, 361);
			this._Label8_1.TabIndex = 18;
			this._Label8_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label8_1.BackColor = System.Drawing.SystemColors.Control;
			this._Label8_1.Enabled = true;
			this._Label8_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label8_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label8_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label8_1.UseMnemonic = true;
			this._Label8_1.Visible = true;
			this._Label8_1.AutoSize = false;
			this._Label8_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label8_1.Name = "_Label8_1";
			this._Label8_0.Text = "- Uncleared Checks:";
			this._Label8_0.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label8_0.Size = new System.Drawing.Size(182, 20);
			this._Label8_0.Location = new System.Drawing.Point(394, 340);
			this._Label8_0.TabIndex = 17;
			this._Label8_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label8_0.BackColor = System.Drawing.SystemColors.Control;
			this._Label8_0.Enabled = true;
			this._Label8_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label8_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label8_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label8_0.UseMnemonic = true;
			this._Label8_0.Visible = true;
			this._Label8_0.AutoSize = false;
			this._Label8_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label8_0.Name = "_Label8_0";
			this.Label7.Text = "- Uncleared Debits:";
			this.Label7.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label7.Size = new System.Drawing.Size(172, 20);
			this.Label7.Location = new System.Drawing.Point(394, 318);
			this.Label7.TabIndex = 16;
			this.Label7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label7.BackColor = System.Drawing.SystemColors.Control;
			this.Label7.Enabled = true;
			this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label7.UseMnemonic = true;
			this.Label7.Visible = true;
			this.Label7.AutoSize = false;
			this.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label7.Name = "Label7";
			this.Label5.Text = "Statement Balance /  Date";
			this.Label5.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.Size = new System.Drawing.Size(62, 40);
			this.Label5.Location = new System.Drawing.Point(484, 4);
			this.Label5.TabIndex = 15;
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
			this.Label4.Text = "Book Balance";
			this.Label4.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label4.Size = new System.Drawing.Size(82, 15);
			this.Label4.Location = new System.Drawing.Point(281, 4);
			this.Label4.TabIndex = 14;
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
			this.Label3.Text = "Difference:";
			this.Label3.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.Size = new System.Drawing.Size(79, 17);
			this.Label3.Location = new System.Drawing.Point(191, 319);
			this.Label3.TabIndex = 13;
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
			this.Label2.Text = "Total of Cleared Items:";
			this.Label2.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.Size = new System.Drawing.Size(162, 19);
			this.Label2.Location = new System.Drawing.Point(6, 298);
			this.Label2.TabIndex = 12;
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
			this._Label1_1.Text = "Department";
			this._Label1_1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_1.Size = new System.Drawing.Size(67, 17);
			this._Label1_1.Location = new System.Drawing.Point(1, 22);
			this._Label1_1.TabIndex = 11;
			this._Label1_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_1.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_1.Enabled = true;
			this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_1.UseMnemonic = true;
			this._Label1_1.Visible = true;
			this._Label1_1.AutoSize = false;
			this._Label1_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_1.Name = "_Label1_1";
			this._Label1_0.Text = "Company";
			this._Label1_0.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_0.Size = new System.Drawing.Size(57, 17);
			this._Label1_0.Location = new System.Drawing.Point(1, 2);
			this._Label1_0.TabIndex = 10;
			this._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_0.Enabled = true;
			this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_0.UseMnemonic = true;
			this._Label1_0.Visible = true;
			this._Label1_0.AutoSize = false;
			this._Label1_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_0.Name = "_Label1_0";
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(_cmdAdj_3);
			this.Controls.Add(_cmdAdj_2);
			this.Controls.Add(_cmdAdj_1);
			this.Controls.Add(_cmdFinish_5);
			this.Controls.Add(_cmdFinish_4);
			this.Controls.Add(_cmdFinish_3);
			this.Controls.Add(_cmdFinish_2);
			this.Controls.Add(_cmdFinish_1);
			this.Controls.Add(_cmdFinish_0);
			this.Controls.Add(_txtTotal_6);
			this.Controls.Add(_txtTotal_5);
			this.Controls.Add(_txtTotal_4);
			this.Controls.Add(_txtTotal_3);
			this.Controls.Add(_txtTotal_2);
			this.Controls.Add(_txtTotal_1);
			this.Controls.Add(_txtTotal_0);
			this.Controls.Add(dtStatement);
			this.Controls.Add(_txtBook_0);
			this.Controls.Add(_cmdAdj_0);
			this.Controls.Add(dtClear);
			this.Controls.Add(cmdClear);
			this.Controls.Add(cmdChecks);
			this.Controls.Add(grdChecks);
			this.Controls.Add(txtStatement);
			this.Controls.Add(_txtBook_1);
			this.Controls.Add(_txtBook_2);
			this.Controls.Add(Label11);
			this.Controls.Add(Label6);
			this.Controls.Add(Label10);
			this.Controls.Add(Label9);
			this.Controls.Add(_Label8_1);
			this.Controls.Add(_Label8_0);
			this.Controls.Add(Label7);
			this.Controls.Add(Label5);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(_Label1_0);
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			this.Label8.SetIndex(_Label8_1, Convert.ToInt16(1));
			this.Label8.SetIndex(_Label8_0, Convert.ToInt16(0));
			this.cmdAdj.SetIndex(_cmdAdj_3, Convert.ToInt16(3));
			this.cmdAdj.SetIndex(_cmdAdj_2, Convert.ToInt16(2));
			this.cmdAdj.SetIndex(_cmdAdj_1, Convert.ToInt16(1));
			this.cmdAdj.SetIndex(_cmdAdj_0, Convert.ToInt16(0));
			this.cmdFinish.SetIndex(_cmdFinish_5, Convert.ToInt16(5));
			this.cmdFinish.SetIndex(_cmdFinish_4, Convert.ToInt16(4));
			this.cmdFinish.SetIndex(_cmdFinish_3, Convert.ToInt16(3));
			this.cmdFinish.SetIndex(_cmdFinish_2, Convert.ToInt16(2));
			this.cmdFinish.SetIndex(_cmdFinish_1, Convert.ToInt16(1));
			this.cmdFinish.SetIndex(_cmdFinish_0, Convert.ToInt16(0));
			this.txtBook.SetIndex(_txtBook_0, Convert.ToInt16(0));
			this.txtBook.SetIndex(_txtBook_1, Convert.ToInt16(1));
			this.txtBook.SetIndex(_txtBook_2, Convert.ToInt16(2));
			this.txtTotal.SetIndex(_txtTotal_6, Convert.ToInt16(6));
			this.txtTotal.SetIndex(_txtTotal_5, Convert.ToInt16(5));
			this.txtTotal.SetIndex(_txtTotal_4, Convert.ToInt16(4));
			this.txtTotal.SetIndex(_txtTotal_3, Convert.ToInt16(3));
			this.txtTotal.SetIndex(_txtTotal_2, Convert.ToInt16(2));
			this.txtTotal.SetIndex(_txtTotal_1, Convert.ToInt16(1));
			this.txtTotal.SetIndex(_txtTotal_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtBook).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cmdFinish).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cmdAdj).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label8).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_2).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtStatement).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdChecks).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtClear).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtBook_0).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtStatement).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
