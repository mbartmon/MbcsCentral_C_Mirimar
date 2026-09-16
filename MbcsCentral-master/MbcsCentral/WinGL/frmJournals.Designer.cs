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
	partial class frmJournals
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmJournals() : base()
		{
			FormClosed += frmJournals_FormClosed;
			Load += frmJournals_Load;
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
		private AxLpLib.AxfpCombo withEventsField_lstDepartments;
		public AxLpLib.AxfpCombo lstDepartments {
			get { return withEventsField_lstDepartments; }
			set {
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.ClickEvent -= lstDepartments_ClickEvent;
				}
				withEventsField_lstDepartments = value;
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.ClickEvent += lstDepartments_ClickEvent;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.CheckBox chkSource;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.CheckBox chkTotal;
		private System.Windows.Forms.CheckBox withEventsField_chkSingle;
		public System.Windows.Forms.CheckBox chkSingle {
			get { return withEventsField_chkSingle; }
			set {
				if (withEventsField_chkSingle != null) {
					withEventsField_chkSingle.CheckStateChanged -= chkSingle_CheckStateChanged;
				}
				withEventsField_chkSingle = value;
				if (withEventsField_chkSingle != null) {
					withEventsField_chkSingle.CheckStateChanged += chkSingle_CheckStateChanged;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_chkConsolidate;
		public System.Windows.Forms.CheckBox chkConsolidate {
			get { return withEventsField_chkConsolidate; }
			set {
				if (withEventsField_chkConsolidate != null) {
					withEventsField_chkConsolidate.CheckStateChanged -= chkConsolidate_CheckStateChanged;
				}
				withEventsField_chkConsolidate = value;
				if (withEventsField_chkConsolidate != null) {
					withEventsField_chkConsolidate.CheckStateChanged += chkConsolidate_CheckStateChanged;
				}
			}
		}
		public AxMSComCtl2.AxDTPicker dtStart;
		public System.Windows.Forms.TextBox txtTitle;
		public System.Windows.Forms.ComboBox lstJournals;
		public System.Windows.Forms.ComboBox lstLayouts;
		public AxMSComCtl2.AxDTPicker dtEnd;
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmJournals));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this.chkSource = new System.Windows.Forms.CheckBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.chkTotal = new System.Windows.Forms.CheckBox();
			this.chkSingle = new System.Windows.Forms.CheckBox();
			this.chkConsolidate = new System.Windows.Forms.CheckBox();
			this.dtStart = new AxMSComCtl2.AxDTPicker();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lstJournals = new System.Windows.Forms.ComboBox();
			this.lstLayouts = new System.Windows.Forms.ComboBox();
			this.dtEnd = new AxMSComCtl2.AxDTPicker();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)this.dtStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Journal Reports";
			this.ClientSize = new System.Drawing.Size(478, 317);
			this.Location = new System.Drawing.Point(4, 23);
			this.ControlBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
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
			this.Name = "frmJournals";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(361, 19);
			this.lstCompanies.Location = new System.Drawing.Point(102, 8);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(361, 19);
			this.lstDepartments.Location = new System.Drawing.Point(102, 82);
			this.lstDepartments.TabIndex = 3;
			this.lstDepartments.Name = "lstDepartments";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_2;
			this._Cmd_2.Text = "EDIT LAYOUT";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(217, 270);
			this._Cmd_2.TabIndex = 20;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			this.chkSource.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSource.Text = "Source Search?";
			this.chkSource.Size = new System.Drawing.Size(97, 20);
			this.chkSource.Location = new System.Drawing.Point(371, 117);
			this.chkSource.TabIndex = 5;
			this.chkSource.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkSource.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkSource.BackColor = System.Drawing.SystemColors.Control;
			this.chkSource.CausesValidation = true;
			this.chkSource.Enabled = true;
			this.chkSource.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSource.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSource.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSource.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkSource.TabStop = true;
			this.chkSource.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkSource.Visible = true;
			this.chkSource.Name = "chkSource";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(116, 270);
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
			this._Cmd_0.Text = "DISPLAY";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(15, 270);
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
			this.chkTotal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkTotal.Text = "Print Totals?";
			this.chkTotal.Size = new System.Drawing.Size(97, 20);
			this.chkTotal.Location = new System.Drawing.Point(371, 154);
			this.chkTotal.TabIndex = 7;
			this.chkTotal.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkTotal.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkTotal.BackColor = System.Drawing.SystemColors.Control;
			this.chkTotal.CausesValidation = true;
			this.chkTotal.Enabled = true;
			this.chkTotal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkTotal.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkTotal.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkTotal.TabStop = true;
			this.chkTotal.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkTotal.Visible = true;
			this.chkTotal.Name = "chkTotal";
			this.chkSingle.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSingle.Text = "Print Single Department?";
			this.chkSingle.Size = new System.Drawing.Size(145, 22);
			this.chkSingle.Location = new System.Drawing.Point(102, 57);
			this.chkSingle.TabIndex = 2;
			this.chkSingle.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkSingle.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkSingle.BackColor = System.Drawing.SystemColors.Control;
			this.chkSingle.CausesValidation = true;
			this.chkSingle.Enabled = true;
			this.chkSingle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSingle.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSingle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSingle.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkSingle.TabStop = true;
			this.chkSingle.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkSingle.Visible = true;
			this.chkSingle.Name = "chkSingle";
			this.chkConsolidate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkConsolidate.Text = "Consolidate Departments?";
			this.chkConsolidate.Size = new System.Drawing.Size(145, 20);
			this.chkConsolidate.Location = new System.Drawing.Point(102, 35);
			this.chkConsolidate.TabIndex = 1;
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
			dtStart.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtStart.OcxState");
			this.dtStart.Size = new System.Drawing.Size(100, 24);
			this.dtStart.Location = new System.Drawing.Point(102, 225);
			this.dtStart.TabIndex = 9;
			this.dtStart.Name = "dtStart";
			this.txtTitle.AutoSize = false;
			this.txtTitle.Size = new System.Drawing.Size(363, 21);
			this.txtTitle.Location = new System.Drawing.Point(102, 189);
			this.txtTitle.MaxLength = 50;
			this.txtTitle.TabIndex = 8;
			this.txtTitle.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtTitle.AcceptsReturn = true;
			this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
			this.txtTitle.CausesValidation = true;
			this.txtTitle.Enabled = true;
			this.txtTitle.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtTitle.HideSelection = true;
			this.txtTitle.ReadOnly = false;
			this.txtTitle.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTitle.Multiline = false;
			this.txtTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtTitle.TabStop = true;
			this.txtTitle.Visible = true;
			this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtTitle.Name = "txtTitle";
			this.lstJournals.Size = new System.Drawing.Size(263, 21);
			this.lstJournals.Location = new System.Drawing.Point(102, 116);
			this.lstJournals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstJournals.TabIndex = 4;
			this.lstJournals.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstJournals.BackColor = System.Drawing.SystemColors.Window;
			this.lstJournals.CausesValidation = true;
			this.lstJournals.Enabled = true;
			this.lstJournals.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstJournals.IntegralHeight = true;
			this.lstJournals.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstJournals.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstJournals.Sorted = false;
			this.lstJournals.TabStop = true;
			this.lstJournals.Visible = true;
			this.lstJournals.Name = "lstJournals";
			this.lstLayouts.Size = new System.Drawing.Size(262, 21);
			this.lstLayouts.Location = new System.Drawing.Point(102, 154);
			this.lstLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstLayouts.TabIndex = 6;
			this.lstLayouts.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstLayouts.BackColor = System.Drawing.SystemColors.Window;
			this.lstLayouts.CausesValidation = true;
			this.lstLayouts.Enabled = true;
			this.lstLayouts.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstLayouts.IntegralHeight = true;
			this.lstLayouts.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstLayouts.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstLayouts.Sorted = false;
			this.lstLayouts.TabStop = true;
			this.lstLayouts.Visible = true;
			this.lstLayouts.Name = "lstLayouts";
			dtEnd.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtEnd.OcxState");
			this.dtEnd.Size = new System.Drawing.Size(100, 24);
			this.dtEnd.Location = new System.Drawing.Point(292, 225);
			this.dtEnd.TabIndex = 10;
			this.dtEnd.Name = "dtEnd";
			this.Label7.Text = "End Date";
			this.Label7.Size = new System.Drawing.Size(81, 16);
			this.Label7.Location = new System.Drawing.Point(224, 230);
			this.Label7.TabIndex = 19;
			this.Label7.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
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
			this.Label6.Text = "Start Date";
			this.Label6.Size = new System.Drawing.Size(81, 16);
			this.Label6.Location = new System.Drawing.Point(15, 230);
			this.Label6.TabIndex = 18;
			this.Label6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
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
			this.Label5.Text = "Report Title";
			this.Label5.Size = new System.Drawing.Size(81, 16);
			this.Label5.Location = new System.Drawing.Point(15, 194);
			this.Label5.TabIndex = 17;
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
			this.Label4.Text = "Layout";
			this.Label4.Size = new System.Drawing.Size(81, 16);
			this.Label4.Location = new System.Drawing.Point(15, 158);
			this.Label4.TabIndex = 16;
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
			this.Label3.Text = "Journal";
			this.Label3.Size = new System.Drawing.Size(81, 16);
			this.Label3.Location = new System.Drawing.Point(15, 120);
			this.Label3.TabIndex = 15;
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
			this.Label2.Text = "Department";
			this.Label2.Size = new System.Drawing.Size(81, 16);
			this.Label2.Location = new System.Drawing.Point(15, 85);
			this.Label2.TabIndex = 14;
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
			this.Label1.Text = "Company";
			this.Label1.Size = new System.Drawing.Size(81, 16);
			this.Label1.Location = new System.Drawing.Point(15, 11);
			this.Label1.TabIndex = 13;
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
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(chkSource);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(chkTotal);
			this.Controls.Add(chkSingle);
			this.Controls.Add(chkConsolidate);
			this.Controls.Add(dtStart);
			this.Controls.Add(txtTitle);
			this.Controls.Add(lstJournals);
			this.Controls.Add(lstLayouts);
			this.Controls.Add(dtEnd);
			this.Controls.Add(Label7);
			this.Controls.Add(Label6);
			this.Controls.Add(Label5);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtStart).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
