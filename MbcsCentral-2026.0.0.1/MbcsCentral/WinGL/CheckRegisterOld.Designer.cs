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
	partial class CheckRegister
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public CheckRegister() : base()
		{
			Resize += CheckRegister_Resize;
			Load += CheckRegister_Load;
			FormClosed += CheckRegister_FormClosed;
			Activated += CheckRegister_Activated;
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
		public AxLpLib.AxfpCombo lstVendors;
		public AxLpLib.AxfpCombo lstAccounts;
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
		public System.Windows.Forms.Button _cmdDoc_1;
		public System.Windows.Forms.Button _cmdDoc_0;
		private AxFPSpread.AxvaSpread withEventsField_grdCheck;
		public AxFPSpread.AxvaSpread grdCheck {
			get { return withEventsField_grdCheck; }
			set {
				if (withEventsField_grdCheck != null) {
					withEventsField_grdCheck.ButtonClicked -= grdCheck_ButtonClicked;
					withEventsField_grdCheck.ComboCloseUp -= grdCheck_ComboCloseUp;
					withEventsField_grdCheck.LeaveCell -= grdCheck_LeaveCell;
				}
				withEventsField_grdCheck = value;
				if (withEventsField_grdCheck != null) {
					withEventsField_grdCheck.ButtonClicked += grdCheck_ButtonClicked;
					withEventsField_grdCheck.ComboCloseUp += grdCheck_ComboCloseUp;
					withEventsField_grdCheck.LeaveCell += grdCheck_LeaveCell;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_2;
		private AxMBText.AxTextField withEventsField_txtCheck;
		public AxMBText.AxTextField txtCheck {
			get { return withEventsField_txtCheck; }
			set {
				if (withEventsField_txtCheck != null) {
					withEventsField_txtCheck.Validating -= txtCheck_Validating;
				}
				withEventsField_txtCheck = value;
				if (withEventsField_txtCheck != null) {
					withEventsField_txtCheck.Validating += txtCheck_Validating;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.CheckBox chkRecon;
		public AxMSComCtl2.AxDTPicker dtCheck;
		public AxMBText.AxTextField txtTotal;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label txtNet;
		public System.Windows.Forms.Label _Label1_13;
		public System.Windows.Forms.Label _Label1_12;
		public System.Windows.Forms.Label _Label1_7;
		public System.Windows.Forms.Label _Label1_6;
		public System.Windows.Forms.Label _Label1_4;
		public System.Windows.Forms.Label _Label1_3;
		public System.Windows.Forms.Label _Label1_2;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label _Label1_0;
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
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_cmdDoc;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray cmdDoc {
			get { return withEventsField_cmdDoc; }
			set {
				if (withEventsField_cmdDoc != null) {
					withEventsField_cmdDoc.Click -= cmdDoc_Click;
				}
				withEventsField_cmdDoc = value;
				if (withEventsField_cmdDoc != null) {
					withEventsField_cmdDoc.Click += cmdDoc_Click;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CheckRegister));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstVendors = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this._cmdDoc_1 = new System.Windows.Forms.Button();
			this._cmdDoc_0 = new System.Windows.Forms.Button();
			this.grdCheck = new AxFPSpread.AxvaSpread();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this.txtCheck = new AxMBText.AxTextField();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.chkRecon = new System.Windows.Forms.CheckBox();
			this.dtCheck = new AxMSComCtl2.AxDTPicker();
			this.txtTotal = new AxMBText.AxTextField();
			this.Label2 = new System.Windows.Forms.Label();
			this.txtNet = new System.Windows.Forms.Label();
			this._Label1_13 = new System.Windows.Forms.Label();
			this._Label1_12 = new System.Windows.Forms.Label();
			this._Label1_7 = new System.Windows.Forms.Label();
			this._Label1_6 = new System.Windows.Forms.Label();
			this._Label1_4 = new System.Windows.Forms.Label();
			this._Label1_3 = new System.Windows.Forms.Label();
			this._Label1_2 = new System.Windows.Forms.Label();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.cmdDoc = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstVendors).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cmdDoc).BeginInit();
			this.Text = "Check Entry";
			this.ClientSize = new System.Drawing.Size(772, 343);
			this.Location = new System.Drawing.Point(4, 23);
			this.ControlBox = false;
			this.KeyPreview = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Enabled = true;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "CheckRegister";
			lstVendors.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstVendors.OcxState");
			this.lstVendors.Size = new System.Drawing.Size(223, 19);
			this.lstVendors.Location = new System.Drawing.Point(55, 32);
			this.lstVendors.TabIndex = 0;
			this.lstVendors.Name = "lstVendors";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(189, 22);
			this.lstAccounts.Location = new System.Drawing.Point(384, 56);
			this.lstAccounts.TabIndex = 20;
			this.lstAccounts.Visible = false;
			this.lstAccounts.Name = "lstAccounts";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(189, 22);
			this.lstDepartments.Location = new System.Drawing.Point(192, 56);
			this.lstDepartments.TabIndex = 21;
			this.lstDepartments.Visible = false;
			this.lstDepartments.Name = "lstDepartments";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(189, 22);
			this.lstCompanies.Location = new System.Drawing.Point(0, 56);
			this.lstCompanies.TabIndex = 22;
			this.lstCompanies.Visible = false;
			this.lstCompanies.Name = "lstCompanies";
			this._cmdDoc_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdDoc_1.Text = "Display Invoice";
			this._cmdDoc_1.Size = new System.Drawing.Size(121, 25);
			this._cmdDoc_1.Location = new System.Drawing.Point(648, 56);
			this._cmdDoc_1.TabIndex = 24;
			this._cmdDoc_1.Visible = false;
			this._cmdDoc_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdDoc_1.BackColor = System.Drawing.SystemColors.Control;
			this._cmdDoc_1.CausesValidation = true;
			this._cmdDoc_1.Enabled = true;
			this._cmdDoc_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdDoc_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdDoc_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdDoc_1.TabStop = true;
			this._cmdDoc_1.Name = "_cmdDoc_1";
			this._cmdDoc_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdDoc_0.Text = "SCAN";
			this._cmdDoc_0.Size = new System.Drawing.Size(57, 25);
			this._cmdDoc_0.Location = new System.Drawing.Point(584, 56);
			this._cmdDoc_0.TabIndex = 23;
			this._cmdDoc_0.Visible = false;
			this._cmdDoc_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdDoc_0.BackColor = System.Drawing.SystemColors.Control;
			this._cmdDoc_0.CausesValidation = true;
			this._cmdDoc_0.Enabled = true;
			this._cmdDoc_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdDoc_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdDoc_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdDoc_0.TabStop = true;
			this._cmdDoc_0.Name = "_cmdDoc_0";
			grdCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdCheck.OcxState");
			this.grdCheck.Size = new System.Drawing.Size(770, 185);
			this.grdCheck.Location = new System.Drawing.Point(0, 88);
			this.grdCheck.TabIndex = 19;
			this.grdCheck.Name = "grdCheck";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_2;
			this._Cmd_2.Text = "CANCEL";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(192, 283);
			this._Cmd_2.TabIndex = 17;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			txtCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtCheck.OcxState");
			this.txtCheck.Size = new System.Drawing.Size(108, 29);
			this.txtCheck.Location = new System.Drawing.Point(575, -2);
			this.txtCheck.TabIndex = 15;
			this.txtCheck.Name = "txtCheck";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "DELETE CHECK";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(100, 283);
			this._Cmd_1.TabIndex = 12;
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
			this._Cmd_0.Location = new System.Drawing.Point(8, 283);
			this._Cmd_0.TabIndex = 11;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.chkRecon.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkRecon.Text = "Reconciled?";
			this.chkRecon.Size = new System.Drawing.Size(87, 21);
			this.chkRecon.Location = new System.Drawing.Point(433, 31);
			this.chkRecon.TabIndex = 2;
			this.chkRecon.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkRecon.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkRecon.BackColor = System.Drawing.SystemColors.Control;
			this.chkRecon.CausesValidation = true;
			this.chkRecon.Enabled = true;
			this.chkRecon.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkRecon.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkRecon.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkRecon.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkRecon.TabStop = true;
			this.chkRecon.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkRecon.Visible = true;
			this.chkRecon.Name = "chkRecon";
			dtCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtCheck.OcxState");
			this.dtCheck.Size = new System.Drawing.Size(100, 21);
			this.dtCheck.Location = new System.Drawing.Point(323, 30);
			this.dtCheck.TabIndex = 1;
			this.dtCheck.Name = "dtCheck";
			txtTotal.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtTotal.OcxState");
			this.txtTotal.Size = new System.Drawing.Size(111, 26);
			this.txtTotal.Location = new System.Drawing.Point(642, 272);
			this.txtTotal.TabIndex = 13;
			this.txtTotal.Name = "txtTotal";
			this.Label2.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			this.Label2.Text = "To DELETE a single allocation: check delete box next to allocation and save check.";
			this.Label2.Size = new System.Drawing.Size(169, 49);
			this.Label2.Location = new System.Drawing.Point(296, 280);
			this.Label2.TabIndex = 18;
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
			this.txtNet.BackColor = System.Drawing.SystemColors.Window;
			this.txtNet.Size = new System.Drawing.Size(109, 21);
			this.txtNet.Location = new System.Drawing.Point(576, 31);
			this.txtNet.TabIndex = 16;
			this.txtNet.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtNet.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.txtNet.Enabled = true;
			this.txtNet.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNet.Cursor = System.Windows.Forms.Cursors.Default;
			this.txtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtNet.UseMnemonic = true;
			this.txtNet.Visible = true;
			this.txtNet.AutoSize = false;
			this.txtNet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtNet.Name = "txtNet";
			this._Label1_13.Text = "CHECK TOTAL:";
			this._Label1_13.Size = new System.Drawing.Size(84, 18);
			this._Label1_13.Location = new System.Drawing.Point(538, 280);
			this._Label1_13.TabIndex = 14;
			this._Label1_13.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_13.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_13.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_13.Enabled = true;
			this._Label1_13.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_13.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_13.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_13.UseMnemonic = true;
			this._Label1_13.Visible = true;
			this._Label1_13.AutoSize = false;
			this._Label1_13.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_13.Name = "_Label1_13";
			this._Label1_12.Text = "Net:";
			this._Label1_12.Size = new System.Drawing.Size(25, 18);
			this._Label1_12.Location = new System.Drawing.Point(536, 35);
			this._Label1_12.TabIndex = 10;
			this._Label1_12.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_12.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_12.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_12.Enabled = true;
			this._Label1_12.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_12.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_12.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_12.UseMnemonic = true;
			this._Label1_12.Visible = true;
			this._Label1_12.AutoSize = false;
			this._Label1_12.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_12.Name = "_Label1_12";
			this._Label1_7.Text = "Date:";
			this._Label1_7.Size = new System.Drawing.Size(29, 18);
			this._Label1_7.Location = new System.Drawing.Point(290, 32);
			this._Label1_7.TabIndex = 9;
			this._Label1_7.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_7.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_7.Enabled = true;
			this._Label1_7.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_7.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_7.UseMnemonic = true;
			this._Label1_7.Visible = true;
			this._Label1_7.AutoSize = false;
			this._Label1_7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_7.Name = "_Label1_7";
			this._Label1_6.Text = "Vendor:";
			this._Label1_6.Size = new System.Drawing.Size(42, 18);
			this._Label1_6.Location = new System.Drawing.Point(6, 35);
			this._Label1_6.TabIndex = 8;
			this._Label1_6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_6.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_6.Enabled = true;
			this._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_6.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_6.UseMnemonic = true;
			this._Label1_6.Visible = true;
			this._Label1_6.AutoSize = false;
			this._Label1_6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_6.Name = "_Label1_6";
			this._Label1_4.Text = "Check#:";
			this._Label1_4.Size = new System.Drawing.Size(44, 18);
			this._Label1_4.Location = new System.Drawing.Point(534, 7);
			this._Label1_4.TabIndex = 7;
			this._Label1_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_4.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_4.Enabled = true;
			this._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_4.UseMnemonic = true;
			this._Label1_4.Visible = true;
			this._Label1_4.AutoSize = false;
			this._Label1_4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_4.Name = "_Label1_4";
			this._Label1_3.BackColor = System.Drawing.SystemColors.Window;
			this._Label1_3.Text = " ";
			this._Label1_3.Size = new System.Drawing.Size(205, 21);
			this._Label1_3.Location = new System.Drawing.Point(320, 5);
			this._Label1_3.TabIndex = 6;
			this._Label1_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_3.Enabled = true;
			this._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_3.UseMnemonic = true;
			this._Label1_3.Visible = true;
			this._Label1_3.AutoSize = false;
			this._Label1_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._Label1_3.Name = "_Label1_3";
			this._Label1_2.BackColor = System.Drawing.SystemColors.Window;
			this._Label1_2.Text = " ";
			this._Label1_2.Size = new System.Drawing.Size(205, 21);
			this._Label1_2.Location = new System.Drawing.Point(54, 5);
			this._Label1_2.TabIndex = 5;
			this._Label1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_2.Enabled = true;
			this._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_2.UseMnemonic = true;
			this._Label1_2.Visible = true;
			this._Label1_2.AutoSize = false;
			this._Label1_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._Label1_2.Name = "_Label1_2";
			this._Label1_1.Text = "Department:";
			this._Label1_1.Size = new System.Drawing.Size(62, 18);
			this._Label1_1.Location = new System.Drawing.Point(261, 7);
			this._Label1_1.TabIndex = 4;
			this._Label1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
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
			this._Label1_0.Text = "Company:";
			this._Label1_0.Size = new System.Drawing.Size(50, 18);
			this._Label1_0.Location = new System.Drawing.Point(7, 8);
			this._Label1_0.TabIndex = 3;
			this._Label1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
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
			this.Controls.Add(lstVendors);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(_cmdDoc_1);
			this.Controls.Add(_cmdDoc_0);
			this.Controls.Add(grdCheck);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(txtCheck);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(chkRecon);
			this.Controls.Add(dtCheck);
			this.Controls.Add(txtTotal);
			this.Controls.Add(Label2);
			this.Controls.Add(txtNet);
			this.Controls.Add(_Label1_13);
			this.Controls.Add(_Label1_12);
			this.Controls.Add(_Label1_7);
			this.Controls.Add(_Label1_6);
			this.Controls.Add(_Label1_4);
			this.Controls.Add(_Label1_3);
			this.Controls.Add(_Label1_2);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(_Label1_0);
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Label1.SetIndex(_Label1_13, Convert.ToInt16(13));
			this.Label1.SetIndex(_Label1_12, Convert.ToInt16(12));
			this.Label1.SetIndex(_Label1_7, Convert.ToInt16(7));
			this.Label1.SetIndex(_Label1_6, Convert.ToInt16(6));
			this.Label1.SetIndex(_Label1_4, Convert.ToInt16(4));
			this.Label1.SetIndex(_Label1_3, Convert.ToInt16(3));
			this.Label1.SetIndex(_Label1_2, Convert.ToInt16(2));
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			this.cmdDoc.SetIndex(_cmdDoc_1, Convert.ToInt16(1));
			this.cmdDoc.SetIndex(_cmdDoc_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.cmdDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendors).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
