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
	partial class Invoice
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public Invoice() : base()
		{
			FormClosed += Invoice_FormClosed;
			Load += Invoice_Load;
			Activated += Invoice_Activated;
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
		private AxLpLib.AxfpCombo withEventsField_lstDepartments;
		public AxLpLib.AxfpCombo lstDepartments {
			get { return withEventsField_lstDepartments; }
			set {
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.ClickEvent -= lstDepartments_ClickEvent;
					withEventsField_lstDepartments.Leave -= lstDepartments_Leave;
				}
				withEventsField_lstDepartments = value;
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.ClickEvent += lstDepartments_ClickEvent;
					withEventsField_lstDepartments.Leave += lstDepartments_Leave;
				}
			}
		}
		private AxLpLib.AxfpCombo withEventsField_lstCompanies;
		public AxLpLib.AxfpCombo lstCompanies {
			get { return withEventsField_lstCompanies; }
			set {
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent -= lstCompanies_ClickEvent;
					withEventsField_lstCompanies.Leave -= lstCompanies_Leave;
				}
				withEventsField_lstCompanies = value;
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent += lstCompanies_ClickEvent;
					withEventsField_lstCompanies.Leave += lstCompanies_Leave;
				}
			}
		}
		private AxLpLib.AxfpCombo withEventsField_lstAccounts;
		public AxLpLib.AxfpCombo lstAccounts {
			get { return withEventsField_lstAccounts; }
			set {
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.Change -= lstAccounts_Change;
				}
				withEventsField_lstAccounts = value;
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.Change += lstAccounts_Change;
				}
			}
		}
		private AxLpLib.AxfpCombo withEventsField_lstJournals;
		public AxLpLib.AxfpCombo lstJournals {
			get { return withEventsField_lstJournals; }
			set {
				if (withEventsField_lstJournals != null) {
					withEventsField_lstJournals.ClickEvent -= lstJournals_ClickEvent;
				}
				withEventsField_lstJournals = value;
				if (withEventsField_lstJournals != null) {
					withEventsField_lstJournals.ClickEvent += lstJournals_ClickEvent;
				}
			}
		}
		private AxLpLib.AxfpCombo withEventsField_lstVendors;
		public AxLpLib.AxfpCombo lstVendors {
			get { return withEventsField_lstVendors; }
			set {
				if (withEventsField_lstVendors != null) {
					withEventsField_lstVendors.Change -= lstVendors_Change;
					withEventsField_lstVendors.ClickEvent -= lstVendors_ClickEvent;
				}
				withEventsField_lstVendors = value;
				if (withEventsField_lstVendors != null) {
					withEventsField_lstVendors.Change += lstVendors_Change;
					withEventsField_lstVendors.ClickEvent += lstVendors_ClickEvent;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkScan;
		public System.Windows.Forms.Button _cmdDoc_1;
		public System.Windows.Forms.Button _cmdDoc_0;
		public System.Windows.Forms.ComboBox lstFreq;
		public System.Windows.Forms.RadioButton _Option1_0;
		public System.Windows.Forms.RadioButton _Option1_1;
		public System.Windows.Forms.RadioButton _Option1_2;
		public System.Windows.Forms.GroupBox Frame6;
		private System.Windows.Forms.TextBox withEventsField_txtMemo;
		public System.Windows.Forms.TextBox txtMemo {
			get { return withEventsField_txtMemo; }
			set {
				if (withEventsField_txtMemo != null) {
					withEventsField_txtMemo.KeyPress -= txtMemo_KeyPress;
				}
				withEventsField_txtMemo = value;
				if (withEventsField_txtMemo != null) {
					withEventsField_txtMemo.KeyPress += txtMemo_KeyPress;
				}
			}
		}
		private System.Windows.Forms.ComboBox withEventsField_lstChkGrp;
		public System.Windows.Forms.ComboBox lstChkGrp {
			get { return withEventsField_lstChkGrp; }
			set {
				if (withEventsField_lstChkGrp != null) {
					withEventsField_lstChkGrp.SelectedIndexChanged -= lstChkGrp_SelectedIndexChanged;
					withEventsField_lstChkGrp.Leave -= lstChkGrp_Leave;
				}
				withEventsField_lstChkGrp = value;
				if (withEventsField_lstChkGrp != null) {
					withEventsField_lstChkGrp.SelectedIndexChanged += lstChkGrp_SelectedIndexChanged;
					withEventsField_lstChkGrp.Leave += lstChkGrp_Leave;
				}
			}
		}
		public System.Windows.Forms.GroupBox Frame5;
		public System.Windows.Forms.Button _Cmd_2;
		private System.Windows.Forms.ComboBox withEventsField_lstInvoices;
		public System.Windows.Forms.ComboBox lstInvoices {
			get { return withEventsField_lstInvoices; }
			set {
				if (withEventsField_lstInvoices != null) {
					withEventsField_lstInvoices.SelectedIndexChanged -= lstInvoices_SelectedIndexChanged;
				}
				withEventsField_lstInvoices = value;
				if (withEventsField_lstInvoices != null) {
					withEventsField_lstInvoices.SelectedIndexChanged += lstInvoices_SelectedIndexChanged;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMBText.AxTextField _txtInvoice_4;
		public AxMBText.AxTextField _txtInvoice_5;
		public System.Windows.Forms.Label Label17;
		public System.Windows.Forms.Label Label16;
		public System.Windows.Forms.Panel Frame4;
		public System.Windows.Forms.ComboBox lstPayFlag;
		public System.Windows.Forms.GroupBox Frame3;
		private System.Windows.Forms.Button withEventsField_cmdDist;
		public System.Windows.Forms.Button cmdDist {
			get { return withEventsField_cmdDist; }
			set {
				if (withEventsField_cmdDist != null) {
					withEventsField_cmdDist.Click -= cmdDist_Click;
				}
				withEventsField_cmdDist = value;
				if (withEventsField_cmdDist != null) {
					withEventsField_cmdDist.Click += cmdDist_Click;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkSplit;
		public System.Windows.Forms.Label Label18;
		public System.Windows.Forms.Label Label15;
		public System.Windows.Forms.GroupBox Frame2;
		private System.Windows.Forms.CheckBox withEventsField_chkEFT;
		public System.Windows.Forms.CheckBox chkEFT {
			get { return withEventsField_chkEFT; }
			set {
				if (withEventsField_chkEFT != null) {
					withEventsField_chkEFT.CheckStateChanged -= chkEFT_CheckStateChanged;
				}
				withEventsField_chkEFT = value;
				if (withEventsField_chkEFT != null) {
					withEventsField_chkEFT.CheckStateChanged += chkEFT_CheckStateChanged;
				}
			}
		}
		public AxMBText.AxTextField _txtInvoice_3;
		public AxMSComCtl2.AxDTPicker _dtVendors_2;
		public System.Windows.Forms.Label Label12;
		public System.Windows.Forms.Label Label11;
		public System.Windows.Forms.GroupBox Frame1;
		public AxMSComCtl2.AxDTPicker _dtVendors_0;
		public AxMBText.AxTextField _txtInvoice_0;
		public AxMSComCtl2.AxDTPicker _dtVendors_1;
		public AxMBText.AxTextField _txtInvoice_1;
		public AxMBText.AxTextField _txtInvoice_2;
		public System.Windows.Forms.Label lblComment;
		public System.Windows.Forms.Label Label19;
		public System.Windows.Forms.Label Label13;
		public System.Windows.Forms.Label Label14;
		public System.Windows.Forms.Label Label10;
		public System.Windows.Forms.Label Label9;
		public System.Windows.Forms.Label Label8;
		public System.Windows.Forms.Label Label7;
		public System.Windows.Forms.Label Label6;
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
		private Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray withEventsField_Option1;
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray Option1 {
			get { return withEventsField_Option1; }
			set {
				if (withEventsField_Option1 != null) {
					withEventsField_Option1.CheckedChanged -= Option1_CheckedChanged;
				}
				withEventsField_Option1 = value;
				if (withEventsField_Option1 != null) {
					withEventsField_Option1.CheckedChanged += Option1_CheckedChanged;
				}
			}
		}
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
		private AxDTPickerArray withEventsField_dtVendors;
		public AxDTPickerArray dtVendors {
			get { return withEventsField_dtVendors; }
			set {
				if (withEventsField_dtVendors != null) {
					withEventsField_dtVendors.Change -= dtVendors_Change;
				}
				withEventsField_dtVendors = value;
				if (withEventsField_dtVendors != null) {
					withEventsField_dtVendors.Change += dtVendors_Change;
				}
			}
		}
		private AxTextFieldArray withEventsField_txtInvoice;
		public AxTextFieldArray txtInvoice {
			get { return withEventsField_txtInvoice; }
			set {
				if (withEventsField_txtInvoice != null) {
					withEventsField_txtInvoice.KeyPressEvent -= txtInvoice_KeyPressEvent;
				}
				withEventsField_txtInvoice = value;
				if (withEventsField_txtInvoice != null) {
					withEventsField_txtInvoice.KeyPressEvent += txtInvoice_KeyPressEvent;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Invoice));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstJournals = new AxLpLib.AxfpCombo();
			this.lstVendors = new AxLpLib.AxfpCombo();
			this.chkScan = new System.Windows.Forms.CheckBox();
			this._cmdDoc_1 = new System.Windows.Forms.Button();
			this._cmdDoc_0 = new System.Windows.Forms.Button();
			this.lstFreq = new System.Windows.Forms.ComboBox();
			this.Frame6 = new System.Windows.Forms.GroupBox();
			this._Option1_0 = new System.Windows.Forms.RadioButton();
			this._Option1_1 = new System.Windows.Forms.RadioButton();
			this._Option1_2 = new System.Windows.Forms.RadioButton();
			this.Frame5 = new System.Windows.Forms.GroupBox();
			this.txtMemo = new System.Windows.Forms.TextBox();
			this.lstChkGrp = new System.Windows.Forms.ComboBox();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this.lstInvoices = new System.Windows.Forms.ComboBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.Frame4 = new System.Windows.Forms.Panel();
			this._txtInvoice_4 = new AxMBText.AxTextField();
			this._txtInvoice_5 = new AxMBText.AxTextField();
			this.Label17 = new System.Windows.Forms.Label();
			this.Label16 = new System.Windows.Forms.Label();
			this.Frame3 = new System.Windows.Forms.GroupBox();
			this.lstPayFlag = new System.Windows.Forms.ComboBox();
			this.Frame2 = new System.Windows.Forms.GroupBox();
			this.cmdDist = new System.Windows.Forms.Button();
			this.chkSplit = new System.Windows.Forms.CheckBox();
			this.Label18 = new System.Windows.Forms.Label();
			this.Label15 = new System.Windows.Forms.Label();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this.chkEFT = new System.Windows.Forms.CheckBox();
			this._txtInvoice_3 = new AxMBText.AxTextField();
			this._dtVendors_2 = new AxMSComCtl2.AxDTPicker();
			this.Label12 = new System.Windows.Forms.Label();
			this.Label11 = new System.Windows.Forms.Label();
			this._dtVendors_0 = new AxMSComCtl2.AxDTPicker();
			this._txtInvoice_0 = new AxMBText.AxTextField();
			this._dtVendors_1 = new AxMSComCtl2.AxDTPicker();
			this._txtInvoice_1 = new AxMBText.AxTextField();
			this._txtInvoice_2 = new AxMBText.AxTextField();
			this.lblComment = new System.Windows.Forms.Label();
			this.Label19 = new System.Windows.Forms.Label();
			this.Label13 = new System.Windows.Forms.Label();
			this.Label14 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label8 = new System.Windows.Forms.Label();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Option1 = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.cmdDoc = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.dtVendors = new AxDTPickerArray(components);
			this.txtInvoice = new AxTextFieldArray(components);
			this.Frame6.SuspendLayout();
			this.Frame5.SuspendLayout();
			this.Frame4.SuspendLayout();
			this.Frame3.SuspendLayout();
			this.Frame2.SuspendLayout();
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendors).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Option1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cmdDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtVendors).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtInvoice).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Text = "Invoice";
			this.ClientSize = new System.Drawing.Size(576, 373);
			this.Location = new System.Drawing.Point(11, 23);
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
			this.Name = "Invoice";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(282, 19);
			this.lstDepartments.Location = new System.Drawing.Point(95, 181);
			this.lstDepartments.TabIndex = 8;
			this.lstDepartments.Name = "lstDepartments";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(282, 19);
			this.lstCompanies.Location = new System.Drawing.Point(95, 163);
			this.lstCompanies.TabIndex = 7;
			this.lstCompanies.Name = "lstCompanies";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(275, 19);
			this.lstAccounts.Location = new System.Drawing.Point(91, 256);
			this.lstAccounts.TabIndex = 10;
			this.lstAccounts.Name = "lstAccounts";
			lstJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstJournals.OcxState");
			this.lstJournals.Size = new System.Drawing.Size(275, 19);
			this.lstJournals.Location = new System.Drawing.Point(91, 275);
			this.lstJournals.TabIndex = 11;
			this.lstJournals.Name = "lstJournals";
			lstVendors.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstVendors.OcxState");
			this.lstVendors.Size = new System.Drawing.Size(288, 19);
			this.lstVendors.Location = new System.Drawing.Point(95, 2);
			this.lstVendors.TabIndex = 0;
			this.lstVendors.Name = "lstVendors";
			this.chkScan.Text = "Scan Invoice";
			this.chkScan.Size = new System.Drawing.Size(113, 17);
			this.chkScan.Location = new System.Drawing.Point(320, 304);
			this.chkScan.TabIndex = 56;
			this.chkScan.CheckState = System.Windows.Forms.CheckState.Checked;
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
			this.chkScan.Visible = true;
			this.chkScan.Name = "chkScan";
			this._cmdDoc_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdDoc_1.BackColor = System.Drawing.Color.Cyan;
			this._cmdDoc_1.Text = "Display Invoice";
			this._cmdDoc_1.Size = new System.Drawing.Size(113, 25);
			this._cmdDoc_1.Location = new System.Drawing.Point(424, 168);
			this._cmdDoc_1.TabIndex = 55;
			this._cmdDoc_1.Visible = false;
			this._cmdDoc_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdDoc_1.CausesValidation = true;
			this._cmdDoc_1.Enabled = true;
			this._cmdDoc_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdDoc_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdDoc_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdDoc_1.TabStop = true;
			this._cmdDoc_1.Name = "_cmdDoc_1";
			this._cmdDoc_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdDoc_0.BackColor = System.Drawing.Color.Cyan;
			this._cmdDoc_0.Text = "Scan Invoice";
			this._cmdDoc_0.Size = new System.Drawing.Size(113, 25);
			this._cmdDoc_0.Location = new System.Drawing.Point(424, 144);
			this._cmdDoc_0.TabIndex = 54;
			this._cmdDoc_0.Visible = false;
			this._cmdDoc_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdDoc_0.CausesValidation = true;
			this._cmdDoc_0.Enabled = true;
			this._cmdDoc_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdDoc_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdDoc_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdDoc_0.TabStop = true;
			this._cmdDoc_0.Name = "_cmdDoc_0";
			this.lstFreq.Size = new System.Drawing.Size(105, 21);
			this.lstFreq.Location = new System.Drawing.Point(440, 107);
			this.lstFreq.Items.AddRange(new object[] {
				"On-Demand",
				"Weekly",
				"Semi Monthly",
				"Monthly",
				"Bi-Monthly",
				"Quarterly",
				"Yearly"
			});
			this.lstFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstFreq.TabIndex = 50;
			this.lstFreq.Visible = false;
			this.lstFreq.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstFreq.BackColor = System.Drawing.SystemColors.Window;
			this.lstFreq.CausesValidation = true;
			this.lstFreq.Enabled = true;
			this.lstFreq.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstFreq.IntegralHeight = true;
			this.lstFreq.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstFreq.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstFreq.Sorted = false;
			this.lstFreq.TabStop = true;
			this.lstFreq.Name = "lstFreq";
			this.Frame6.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Frame6.Text = "Account By:";
			this.Frame6.Size = new System.Drawing.Size(113, 72);
			this.Frame6.Location = new System.Drawing.Point(456, 239);
			this.Frame6.TabIndex = 46;
			this.Frame6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame6.Enabled = true;
			this.Frame6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame6.Visible = true;
			this.Frame6.Padding = new System.Windows.Forms.Padding(0);
			this.Frame6.Name = "Frame6";
			this._Option1_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this._Option1_0.Text = "Name";
			this._Option1_0.Size = new System.Drawing.Size(59, 19);
			this._Option1_0.Location = new System.Drawing.Point(0, 12);
			this._Option1_0.TabIndex = 49;
			this._Option1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.CausesValidation = true;
			this._Option1_0.Enabled = true;
			this._Option1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_0.TabStop = true;
			this._Option1_0.Checked = false;
			this._Option1_0.Visible = true;
			this._Option1_0.Name = "_Option1_0";
			this._Option1_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this._Option1_1.Text = "New Acct Number";
			this._Option1_1.Size = new System.Drawing.Size(108, 19);
			this._Option1_1.Location = new System.Drawing.Point(0, 31);
			this._Option1_1.TabIndex = 48;
			this._Option1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.CausesValidation = true;
			this._Option1_1.Enabled = true;
			this._Option1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_1.TabStop = true;
			this._Option1_1.Checked = false;
			this._Option1_1.Visible = true;
			this._Option1_1.Name = "_Option1_1";
			this._Option1_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this._Option1_2.Text = "Old Acct Number";
			this._Option1_2.Size = new System.Drawing.Size(110, 19);
			this._Option1_2.Location = new System.Drawing.Point(0, 50);
			this._Option1_2.TabIndex = 47;
			this._Option1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.CausesValidation = true;
			this._Option1_2.Enabled = true;
			this._Option1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_2.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_2.TabStop = true;
			this._Option1_2.Checked = false;
			this._Option1_2.Visible = true;
			this._Option1_2.Name = "_Option1_2";
			this.Frame5.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Frame5.Text = "Check Group / Memo Line";
			this.Frame5.Size = new System.Drawing.Size(560, 38);
			this.Frame5.Location = new System.Drawing.Point(9, 201);
			this.Frame5.TabIndex = 43;
			this.Frame5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame5.Enabled = true;
			this.Frame5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame5.Visible = true;
			this.Frame5.Padding = new System.Windows.Forms.Padding(0);
			this.Frame5.Name = "Frame5";
			this.txtMemo.AutoSize = false;
			this.txtMemo.Size = new System.Drawing.Size(491, 22);
			this.txtMemo.Location = new System.Drawing.Point(52, 12);
			this.txtMemo.MaxLength = 60;
			this.txtMemo.TabIndex = 51;
			this.txtMemo.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtMemo.AcceptsReturn = true;
			this.txtMemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtMemo.BackColor = System.Drawing.SystemColors.Window;
			this.txtMemo.CausesValidation = true;
			this.txtMemo.Enabled = true;
			this.txtMemo.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtMemo.HideSelection = true;
			this.txtMemo.ReadOnly = false;
			this.txtMemo.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMemo.Multiline = false;
			this.txtMemo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtMemo.TabStop = true;
			this.txtMemo.Visible = true;
			this.txtMemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtMemo.Name = "txtMemo";
			this.lstChkGrp.Size = new System.Drawing.Size(45, 21);
			this.lstChkGrp.Location = new System.Drawing.Point(4, 13);
			this.lstChkGrp.TabIndex = 9;
			this.lstChkGrp.Text = " 0";
			this.lstChkGrp.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstChkGrp.BackColor = System.Drawing.SystemColors.Window;
			this.lstChkGrp.CausesValidation = true;
			this.lstChkGrp.Enabled = true;
			this.lstChkGrp.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstChkGrp.IntegralHeight = true;
			this.lstChkGrp.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstChkGrp.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstChkGrp.Sorted = false;
			this.lstChkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstChkGrp.TabStop = true;
			this.lstChkGrp.Visible = true;
			this.lstChkGrp.Name = "lstChkGrp";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_2;
			this._Cmd_2.Text = "EXIT";
			this._Cmd_2.Size = new System.Drawing.Size(65, 33);
			this._Cmd_2.Location = new System.Drawing.Point(498, 328);
			this._Cmd_2.TabIndex = 22;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			this.lstInvoices.Size = new System.Drawing.Size(241, 21);
			this.lstInvoices.Location = new System.Drawing.Point(95, 22);
			this.lstInvoices.TabIndex = 1;
			this.lstInvoices.Text = " ";
			this.lstInvoices.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstInvoices.BackColor = System.Drawing.SystemColors.Window;
			this.lstInvoices.CausesValidation = true;
			this.lstInvoices.Enabled = true;
			this.lstInvoices.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstInvoices.IntegralHeight = true;
			this.lstInvoices.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstInvoices.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstInvoices.Sorted = false;
			this.lstInvoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstInvoices.TabStop = true;
			this.lstInvoices.Visible = true;
			this.lstInvoices.Name = "lstInvoices";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "SAVE";
			this.AcceptButton = this._Cmd_1;
			this._Cmd_1.Size = new System.Drawing.Size(65, 33);
			this._Cmd_1.Location = new System.Drawing.Point(433, 328);
			this._Cmd_1.TabIndex = 21;
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
			this._Cmd_0.Text = "NEW";
			this._Cmd_0.Size = new System.Drawing.Size(65, 33);
			this._Cmd_0.Location = new System.Drawing.Point(368, 328);
			this._Cmd_0.TabIndex = 20;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.Frame4.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this.Frame4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Frame4.Size = new System.Drawing.Size(201, 65);
			this.Frame4.Location = new System.Drawing.Point(368, 41);
			this.Frame4.TabIndex = 39;
			this.Frame4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame4.Enabled = true;
			this.Frame4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame4.Visible = true;
			this.Frame4.Name = "Frame4";
			_txtInvoice_4.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_4.OcxState");
			this._txtInvoice_4.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_4.Location = new System.Drawing.Point(88, 7);
			this._txtInvoice_4.TabIndex = 17;
			this._txtInvoice_4.Name = "_txtInvoice_4";
			_txtInvoice_5.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_5.OcxState");
			this._txtInvoice_5.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_5.Location = new System.Drawing.Point(88, 32);
			this._txtInvoice_5.TabIndex = 18;
			this._txtInvoice_5.Name = "_txtInvoice_5";
			this.Label17.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this.Label17.Text = "Amount Paid:";
			this.Label17.Size = new System.Drawing.Size(73, 17);
			this.Label17.Location = new System.Drawing.Point(8, 40);
			this.Label17.TabIndex = 41;
			this.Label17.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label17.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label17.Enabled = true;
			this.Label17.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label17.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label17.UseMnemonic = true;
			this.Label17.Visible = true;
			this.Label17.AutoSize = false;
			this.Label17.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label17.Name = "Label17";
			this.Label16.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this.Label16.Text = "Amount to Pay:";
			this.Label16.Size = new System.Drawing.Size(73, 17);
			this.Label16.Location = new System.Drawing.Point(8, 16);
			this.Label16.TabIndex = 40;
			this.Label16.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label16.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label16.Enabled = true;
			this.Label16.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label16.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label16.UseMnemonic = true;
			this.Label16.Visible = true;
			this.Label16.AutoSize = false;
			this.Label16.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label16.Name = "Label16";
			this.Frame3.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this.Frame3.Text = "Pay Flag";
			this.Frame3.Size = new System.Drawing.Size(145, 41);
			this.Frame3.Location = new System.Drawing.Point(424, 0);
			this.Frame3.TabIndex = 38;
			this.Frame3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame3.Enabled = true;
			this.Frame3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame3.Visible = true;
			this.Frame3.Padding = new System.Windows.Forms.Padding(0);
			this.Frame3.Name = "Frame3";
			this.lstPayFlag.Size = new System.Drawing.Size(129, 21);
			this.lstPayFlag.Location = new System.Drawing.Point(8, 16);
			this.lstPayFlag.Items.AddRange(new object[] {
				"0 = Don't Pay",
				"1 = Pay",
				"2 = Pay - Force Disc",
				"3 =<not selected>",
				"4 = Paid"
			});
			this.lstPayFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstPayFlag.TabIndex = 16;
			this.lstPayFlag.Tag = "PayFlag";
			this.lstPayFlag.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstPayFlag.BackColor = System.Drawing.SystemColors.Window;
			this.lstPayFlag.CausesValidation = true;
			this.lstPayFlag.Enabled = true;
			this.lstPayFlag.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstPayFlag.IntegralHeight = true;
			this.lstPayFlag.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstPayFlag.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstPayFlag.Sorted = false;
			this.lstPayFlag.TabStop = true;
			this.lstPayFlag.Visible = true;
			this.lstPayFlag.Name = "lstPayFlag";
			this.Frame2.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Frame2.Text = "Distributions";
			this.Frame2.Size = new System.Drawing.Size(447, 60);
			this.Frame2.Location = new System.Drawing.Point(9, 240);
			this.Frame2.TabIndex = 36;
			this.Frame2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame2.Enabled = true;
			this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame2.Visible = true;
			this.Frame2.Padding = new System.Windows.Forms.Padding(0);
			this.Frame2.Name = "Frame2";
			this.cmdDist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdDist.Text = "Show Splits";
			this.cmdDist.Size = new System.Drawing.Size(73, 25);
			this.cmdDist.Location = new System.Drawing.Point(365, 31);
			this.cmdDist.TabIndex = 12;
			this.cmdDist.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdDist.BackColor = System.Drawing.SystemColors.Control;
			this.cmdDist.CausesValidation = true;
			this.cmdDist.Enabled = true;
			this.cmdDist.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdDist.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdDist.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdDist.TabStop = true;
			this.cmdDist.Name = "cmdDist";
			this.chkSplit.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.chkSplit.Text = "Split";
			this.chkSplit.Size = new System.Drawing.Size(41, 17);
			this.chkSplit.Location = new System.Drawing.Point(366, 10);
			this.chkSplit.TabIndex = 19;
			this.chkSplit.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkSplit.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkSplit.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkSplit.CausesValidation = true;
			this.chkSplit.Enabled = true;
			this.chkSplit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSplit.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSplit.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSplit.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkSplit.TabStop = true;
			this.chkSplit.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkSplit.Visible = true;
			this.chkSplit.Name = "chkSplit";
			this.Label18.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Label18.Text = "Journal";
			this.Label18.Size = new System.Drawing.Size(57, 17);
			this.Label18.Location = new System.Drawing.Point(6, 33);
			this.Label18.TabIndex = 42;
			this.Label18.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label18.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label18.Enabled = true;
			this.Label18.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label18.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label18.UseMnemonic = true;
			this.Label18.Visible = true;
			this.Label18.AutoSize = false;
			this.Label18.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label18.Name = "Label18";
			this.Label15.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Label15.Text = "Account";
			this.Label15.Size = new System.Drawing.Size(57, 17);
			this.Label15.Location = new System.Drawing.Point(6, 16);
			this.Label15.TabIndex = 37;
			this.Label15.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label15.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label15.Enabled = true;
			this.Label15.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label15.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label15.UseMnemonic = true;
			this.Label15.Visible = true;
			this.Label15.AutoSize = false;
			this.Label15.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label15.Name = "Label15";
			this.Frame1.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Frame1.Text = "Pre-Paid Check Data";
			this.Frame1.Size = new System.Drawing.Size(301, 70);
			this.Frame1.Location = new System.Drawing.Point(9, 304);
			this.Frame1.TabIndex = 33;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this.chkEFT.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.chkEFT.Text = "EFT";
			this.chkEFT.Size = new System.Drawing.Size(73, 13);
			this.chkEFT.Location = new System.Drawing.Point(192, 8);
			this.chkEFT.TabIndex = 13;
			this.chkEFT.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkEFT.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkEFT.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkEFT.CausesValidation = true;
			this.chkEFT.Enabled = true;
			this.chkEFT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkEFT.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkEFT.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkEFT.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkEFT.TabStop = true;
			this.chkEFT.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkEFT.Visible = true;
			this.chkEFT.Name = "chkEFT";
			_txtInvoice_3.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_3.OcxState");
			this._txtInvoice_3.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_3.Location = new System.Drawing.Point(53, 31);
			this._txtInvoice_3.TabIndex = 14;
			this._txtInvoice_3.Name = "_txtInvoice_3";
			_dtVendors_2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_dtVendors_2.OcxState");
			this._dtVendors_2.Size = new System.Drawing.Size(103, 25);
			this._dtVendors_2.Location = new System.Drawing.Point(189, 31);
			this._dtVendors_2.TabIndex = 15;
			this._dtVendors_2.Name = "_dtVendors_2";
			this.Label12.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Label12.Text = "Date:";
			this.Label12.Size = new System.Drawing.Size(33, 17);
			this.Label12.Location = new System.Drawing.Point(160, 40);
			this.Label12.TabIndex = 35;
			this.Label12.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label12.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label12.Enabled = true;
			this.Label12.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label12.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label12.UseMnemonic = true;
			this.Label12.Visible = true;
			this.Label12.AutoSize = false;
			this.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label12.Name = "Label12";
			this.Label11.BackColor = System.Drawing.Color.FromArgb(128, 255, 255);
			this.Label11.Text = "Number:";
			this.Label11.Size = new System.Drawing.Size(41, 17);
			this.Label11.Location = new System.Drawing.Point(5, 38);
			this.Label11.TabIndex = 34;
			this.Label11.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label11.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label11.Enabled = true;
			this.Label11.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label11.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label11.UseMnemonic = true;
			this.Label11.Visible = true;
			this.Label11.AutoSize = false;
			this.Label11.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label11.Name = "Label11";
			_dtVendors_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_dtVendors_0.OcxState");
			this._dtVendors_0.Size = new System.Drawing.Size(103, 25);
			this._dtVendors_0.Location = new System.Drawing.Point(95, 42);
			this._dtVendors_0.TabIndex = 2;
			this._dtVendors_0.Name = "_dtVendors_0";
			_txtInvoice_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_0.OcxState");
			this._txtInvoice_0.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_0.Location = new System.Drawing.Point(95, 92);
			this._txtInvoice_0.TabIndex = 4;
			this._txtInvoice_0.Name = "_txtInvoice_0";
			_dtVendors_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_dtVendors_1.OcxState");
			this._dtVendors_1.Size = new System.Drawing.Size(103, 25);
			this._dtVendors_1.Location = new System.Drawing.Point(95, 68);
			this._dtVendors_1.TabIndex = 3;
			this._dtVendors_1.Name = "_dtVendors_1";
			_txtInvoice_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_1.OcxState");
			this._txtInvoice_1.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_1.Location = new System.Drawing.Point(95, 116);
			this._txtInvoice_1.TabIndex = 5;
			this._txtInvoice_1.Name = "_txtInvoice_1";
			_txtInvoice_2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_txtInvoice_2.OcxState");
			this._txtInvoice_2.Size = new System.Drawing.Size(97, 25);
			this._txtInvoice_2.Location = new System.Drawing.Point(95, 140);
			this._txtInvoice_2.TabIndex = 6;
			this._txtInvoice_2.Name = "_txtInvoice_2";
			this.lblComment.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.lblComment.Size = new System.Drawing.Size(193, 65);
			this.lblComment.Location = new System.Drawing.Point(380, 129);
			this.lblComment.TabIndex = 53;
			this.lblComment.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblComment.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblComment.Enabled = true;
			this.lblComment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblComment.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblComment.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblComment.UseMnemonic = true;
			this.lblComment.Visible = true;
			this.lblComment.AutoSize = false;
			this.lblComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblComment.Name = "lblComment";
			this.Label19.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label19.Text = "Last Paid:";
			this.Label19.Size = new System.Drawing.Size(49, 17);
			this.Label19.Location = new System.Drawing.Point(208, 52);
			this.Label19.TabIndex = 52;
			this.Label19.Visible = false;
			this.Label19.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label19.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label19.Enabled = true;
			this.Label19.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label19.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label19.UseMnemonic = true;
			this.Label19.AutoSize = false;
			this.Label19.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label19.Name = "Label19";
			this.Label13.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label13.Text = "Company";
			this.Label13.Size = new System.Drawing.Size(49, 17);
			this.Label13.Location = new System.Drawing.Point(10, 167);
			this.Label13.TabIndex = 45;
			this.Label13.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label13.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label13.Enabled = true;
			this.Label13.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label13.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label13.UseMnemonic = true;
			this.Label13.Visible = true;
			this.Label13.AutoSize = false;
			this.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label13.Name = "Label13";
			this.Label14.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label14.Text = "Department";
			this.Label14.Size = new System.Drawing.Size(57, 17);
			this.Label14.Location = new System.Drawing.Point(10, 187);
			this.Label14.TabIndex = 44;
			this.Label14.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label14.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label14.Enabled = true;
			this.Label14.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label14.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label14.UseMnemonic = true;
			this.Label14.Visible = true;
			this.Label14.AutoSize = false;
			this.Label14.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label14.Name = "Label14";
			this.Label10.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label10.Text = "(FULL Amount Due)";
			this.Label10.Size = new System.Drawing.Size(169, 17);
			this.Label10.Location = new System.Drawing.Point(200, 96);
			this.Label10.TabIndex = 32;
			this.Label10.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label10.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label10.Enabled = true;
			this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label10.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label10.UseMnemonic = true;
			this.Label10.Visible = true;
			this.Label10.AutoSize = false;
			this.Label10.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label10.Name = "Label10";
			this.Label9.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label9.Text = "(amount if pre-paid; % if not pre-paid)";
			this.Label9.Size = new System.Drawing.Size(177, 18);
			this.Label9.Location = new System.Drawing.Point(200, 144);
			this.Label9.TabIndex = 31;
			this.Label9.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label9.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label9.Enabled = true;
			this.Label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label9.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label9.UseMnemonic = true;
			this.Label9.Visible = true;
			this.Label9.AutoSize = false;
			this.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label9.Name = "Label9";
			this.Label8.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label8.Text = "(Amount NOT Subject to Discount)";
			this.Label8.Size = new System.Drawing.Size(169, 17);
			this.Label8.Location = new System.Drawing.Point(200, 120);
			this.Label8.TabIndex = 30;
			this.Label8.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label8.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label8.Enabled = true;
			this.Label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label8.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label8.UseMnemonic = true;
			this.Label8.Visible = true;
			this.Label8.AutoSize = false;
			this.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label8.Name = "Label8";
			this.Label7.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label7.Text = "Discount";
			this.Label7.Size = new System.Drawing.Size(81, 17);
			this.Label7.Location = new System.Drawing.Point(10, 146);
			this.Label7.TabIndex = 29;
			this.Label7.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label7.Enabled = true;
			this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label7.UseMnemonic = true;
			this.Label7.Visible = true;
			this.Label7.AutoSize = false;
			this.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label7.Name = "Label7";
			this.Label6.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label6.Text = "Allowance";
			this.Label6.Size = new System.Drawing.Size(81, 17);
			this.Label6.Location = new System.Drawing.Point(10, 124);
			this.Label6.TabIndex = 28;
			this.Label6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label6.Enabled = true;
			this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label6.UseMnemonic = true;
			this.Label6.Visible = true;
			this.Label6.AutoSize = false;
			this.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label6.Name = "Label6";
			this.Label5.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label5.Text = "Invoice Amount";
			this.Label5.Size = new System.Drawing.Size(81, 17);
			this.Label5.Location = new System.Drawing.Point(10, 100);
			this.Label5.TabIndex = 27;
			this.Label5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label5.Enabled = true;
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.UseMnemonic = true;
			this.Label5.Visible = true;
			this.Label5.AutoSize = false;
			this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label5.Name = "Label5";
			this.Label4.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label4.Text = "Due Date:";
			this.Label4.Size = new System.Drawing.Size(73, 17);
			this.Label4.Location = new System.Drawing.Point(10, 76);
			this.Label4.TabIndex = 26;
			this.Label4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label4.Enabled = true;
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.UseMnemonic = true;
			this.Label4.Visible = true;
			this.Label4.AutoSize = false;
			this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label4.Name = "Label4";
			this.Label3.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label3.Text = "Invoice Date:";
			this.Label3.Size = new System.Drawing.Size(73, 17);
			this.Label3.Location = new System.Drawing.Point(10, 52);
			this.Label3.TabIndex = 25;
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
			this.Label2.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label2.Text = "Invoice Number";
			this.Label2.Size = new System.Drawing.Size(81, 17);
			this.Label2.Location = new System.Drawing.Point(10, 30);
			this.Label2.TabIndex = 24;
			this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label2.Enabled = true;
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.UseMnemonic = true;
			this.Label2.Visible = true;
			this.Label2.AutoSize = false;
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label2.Name = "Label2";
			this.Label1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.Label1.Text = "Vendor:";
			this.Label1.Size = new System.Drawing.Size(41, 17);
			this.Label1.Location = new System.Drawing.Point(10, 9);
			this.Label1.TabIndex = 23;
			this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label1.Enabled = true;
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.UseMnemonic = true;
			this.Label1.Visible = true;
			this.Label1.AutoSize = false;
			this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label1.Name = "Label1";
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(lstJournals);
			this.Controls.Add(lstVendors);
			this.Controls.Add(chkScan);
			this.Controls.Add(_cmdDoc_1);
			this.Controls.Add(_cmdDoc_0);
			this.Controls.Add(lstFreq);
			this.Controls.Add(Frame6);
			this.Controls.Add(Frame5);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(lstInvoices);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(Frame4);
			this.Controls.Add(Frame3);
			this.Controls.Add(Frame2);
			this.Controls.Add(Frame1);
			this.Controls.Add(_dtVendors_0);
			this.Controls.Add(_txtInvoice_0);
			this.Controls.Add(_dtVendors_1);
			this.Controls.Add(_txtInvoice_1);
			this.Controls.Add(_txtInvoice_2);
			this.Controls.Add(lblComment);
			this.Controls.Add(Label19);
			this.Controls.Add(Label13);
			this.Controls.Add(Label14);
			this.Controls.Add(Label10);
			this.Controls.Add(Label9);
			this.Controls.Add(Label8);
			this.Controls.Add(Label7);
			this.Controls.Add(Label6);
			this.Controls.Add(Label5);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Frame6.Controls.Add(_Option1_0);
			this.Frame6.Controls.Add(_Option1_1);
			this.Frame6.Controls.Add(_Option1_2);
			this.Frame5.Controls.Add(txtMemo);
			this.Frame5.Controls.Add(lstChkGrp);
			this.Frame4.Controls.Add(_txtInvoice_4);
			this.Frame4.Controls.Add(_txtInvoice_5);
			this.Frame4.Controls.Add(Label17);
			this.Frame4.Controls.Add(Label16);
			this.Frame3.Controls.Add(lstPayFlag);
			this.Frame2.Controls.Add(cmdDist);
			this.Frame2.Controls.Add(chkSplit);
			this.Frame2.Controls.Add(Label18);
			this.Frame2.Controls.Add(Label15);
			this.Frame1.Controls.Add(chkEFT);
			this.Frame1.Controls.Add(_txtInvoice_3);
			this.Frame1.Controls.Add(_dtVendors_2);
			this.Frame1.Controls.Add(Label12);
			this.Frame1.Controls.Add(Label11);
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Option1.SetIndex(_Option1_0, Convert.ToInt16(0));
			this.Option1.SetIndex(_Option1_1, Convert.ToInt16(1));
			this.Option1.SetIndex(_Option1_2, Convert.ToInt16(2));
			this.cmdDoc.SetIndex(_cmdDoc_1, Convert.ToInt16(1));
			this.cmdDoc.SetIndex(_cmdDoc_0, Convert.ToInt16(0));
			this.dtVendors.SetIndex(_dtVendors_2, Convert.ToInt16(2));
			this.dtVendors.SetIndex(_dtVendors_0, Convert.ToInt16(0));
			this.dtVendors.SetIndex(_dtVendors_1, Convert.ToInt16(1));
			this.txtInvoice.SetIndex(_txtInvoice_4, Convert.ToInt16(4));
			this.txtInvoice.SetIndex(_txtInvoice_5, Convert.ToInt16(5));
			this.txtInvoice.SetIndex(_txtInvoice_3, Convert.ToInt16(3));
			this.txtInvoice.SetIndex(_txtInvoice_0, Convert.ToInt16(0));
			this.txtInvoice.SetIndex(_txtInvoice_1, Convert.ToInt16(1));
			this.txtInvoice.SetIndex(_txtInvoice_2, Convert.ToInt16(2));
			((System.ComponentModel.ISupportInitialize)this.txtInvoice).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtVendors).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cmdDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Option1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_2).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_0).EndInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_0).EndInit();
			((System.ComponentModel.ISupportInitialize)this._dtVendors_2).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_3).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_5).EndInit();
			((System.ComponentModel.ISupportInitialize)this._txtInvoice_4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendors).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			this.Frame6.ResumeLayout(false);
			this.Frame5.ResumeLayout(false);
			this.Frame4.ResumeLayout(false);
			this.Frame3.ResumeLayout(false);
			this.Frame2.ResumeLayout(false);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
