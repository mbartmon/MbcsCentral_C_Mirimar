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
	partial class frmF1099
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmF1099() : base()
		{
			FormClosed += frmF1099_FormClosed;
			Load += frmF1099_Load;
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
		public AxLpLib.AxfpList lstVendPrint;
		private AxLpLib.AxfpCombo withEventsField_lstVendors;
		public AxLpLib.AxfpCombo lstVendors {
			get { return withEventsField_lstVendors; }
			set {
				if (withEventsField_lstVendors != null) {
					withEventsField_lstVendors.ClickEvent -= lstVendors_ClickEvent;
					withEventsField_lstVendors.DblClick -= lstVendors_DblClick;
				}
				withEventsField_lstVendors = value;
				if (withEventsField_lstVendors != null) {
					withEventsField_lstVendors.ClickEvent += lstVendors_ClickEvent;
					withEventsField_lstVendors.DblClick += lstVendors_DblClick;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkAddress;
		public System.Windows.Forms.RadioButton _optOrder_1;
		public System.Windows.Forms.RadioButton _optOrder_0;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.Button _Command1_0;
		public System.Windows.Forms.Button _Command1_1;
		public System.Windows.Forms.Button _Command1_2;
		private System.Windows.Forms.Button withEventsField_Remove;
		public System.Windows.Forms.Button Remove {
			get { return withEventsField_Remove; }
			set {
				if (withEventsField_Remove != null) {
					withEventsField_Remove.Click -= Remove_Click;
				}
				withEventsField_Remove = value;
				if (withEventsField_Remove != null) {
					withEventsField_Remove.Click += Remove_Click;
				}
			}
		}
		public System.Windows.Forms.Button ClearList;
		private System.Windows.Forms.Button withEventsField_AddVendor;
		public System.Windows.Forms.Button AddVendor {
			get { return withEventsField_AddVendor; }
			set {
				if (withEventsField_AddVendor != null) {
					withEventsField_AddVendor.Click -= AddVendor_Click;
				}
				withEventsField_AddVendor = value;
				if (withEventsField_AddVendor != null) {
					withEventsField_AddVendor.Click += AddVendor_Click;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkDetail;
		public AxMSComCtl2.AxDTPicker _dtDate_0;
		public AxMSComCtl2.AxDTPicker _dtDate_1;
		public System.Windows.Forms.OpenFileDialog dlgWordOpen;
		public System.Windows.Forms.SaveFileDialog dlgWordSave;
		public System.Windows.Forms.FontDialog dlgWordFont;
		public System.Windows.Forms.ColorDialog dlgWordColor;
		public System.Windows.Forms.PrintDialog dlgWordPrint;
		public Microsoft.VisualBasic.PowerPacks.LineShape Line2;
		public System.Windows.Forms.Label Label16;
		public Microsoft.VisualBasic.PowerPacks.LineShape Line3;
		public Microsoft.VisualBasic.PowerPacks.LineShape Line4;
		public System.Windows.Forms.Label Label17;
		public System.Windows.Forms.Label Label18;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Command1;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Command1 {
			get { return withEventsField_Command1; }
			set {
				if (withEventsField_Command1 != null) {
					withEventsField_Command1.Click -= Command1_Click;
				}
				withEventsField_Command1 = value;
				if (withEventsField_Command1 != null) {
					withEventsField_Command1.Click += Command1_Click;
				}
			}
		}
		public AxDTPickerArray dtDate;
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray optOrder;
		public Microsoft.VisualBasic.PowerPacks.ShapeContainer ShapeContainer1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmF1099));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.ShapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.lstVendPrint = new AxLpLib.AxfpList();
			this.lstVendors = new AxLpLib.AxfpCombo();
			this.chkAddress = new System.Windows.Forms.CheckBox();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._optOrder_1 = new System.Windows.Forms.RadioButton();
			this._optOrder_0 = new System.Windows.Forms.RadioButton();
			this._Command1_0 = new System.Windows.Forms.Button();
			this._Command1_1 = new System.Windows.Forms.Button();
			this._Command1_2 = new System.Windows.Forms.Button();
			this.Remove = new System.Windows.Forms.Button();
			this.ClearList = new System.Windows.Forms.Button();
			this.AddVendor = new System.Windows.Forms.Button();
			this.chkDetail = new System.Windows.Forms.CheckBox();
			this._dtDate_0 = new AxMSComCtl2.AxDTPicker();
			this._dtDate_1 = new AxMSComCtl2.AxDTPicker();
			this.dlgWordOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgWordSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgWordFont = new System.Windows.Forms.FontDialog();
			this.dlgWordColor = new System.Windows.Forms.ColorDialog();
			this.dlgWordPrint = new System.Windows.Forms.PrintDialog();
			this.Line2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.Label16 = new System.Windows.Forms.Label();
			this.Line3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.Line4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.Label17 = new System.Windows.Forms.Label();
			this.Label18 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Command1 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.dtDate = new AxDTPickerArray(components);
			this.optOrder = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstVendPrint).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendors).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._dtDate_0).BeginInit();
			((System.ComponentModel.ISupportInitialize)this._dtDate_1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Command1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.optOrder).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "1099 Forms";
			this.ClientSize = new System.Drawing.Size(598, 230);
			this.Location = new System.Drawing.Point(4, 38);
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
			this.Name = "frmF1099";
			lstVendPrint.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstVendPrint.OcxState");
			this.lstVendPrint.Size = new System.Drawing.Size(225, 147);
			this.lstVendPrint.Location = new System.Drawing.Point(368, 56);
			this.lstVendPrint.TabIndex = 14;
			this.lstVendPrint.Name = "lstVendPrint";
			lstVendors.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstVendors.OcxState");
			this.lstVendors.Size = new System.Drawing.Size(225, 19);
			this.lstVendors.Location = new System.Drawing.Point(368, 32);
			this.lstVendors.TabIndex = 15;
			this.lstVendors.Name = "lstVendors";
			this.chkAddress.Text = "Print Vendor Address";
			this.chkAddress.Size = new System.Drawing.Size(121, 25);
			this.chkAddress.Location = new System.Drawing.Point(8, 200);
			this.chkAddress.TabIndex = 19;
			this.chkAddress.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkAddress.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkAddress.BackColor = System.Drawing.SystemColors.Control;
			this.chkAddress.CausesValidation = true;
			this.chkAddress.Enabled = true;
			this.chkAddress.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkAddress.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkAddress.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkAddress.TabStop = true;
			this.chkAddress.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkAddress.Visible = true;
			this.chkAddress.Name = "chkAddress";
			this.Frame1.Text = "Report Order";
			this.Frame1.Size = new System.Drawing.Size(165, 64);
			this.Frame1.Location = new System.Drawing.Point(6, 128);
			this.Frame1.TabIndex = 16;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this._optOrder_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optOrder_1.Text = "By Company";
			this._optOrder_1.Size = new System.Drawing.Size(124, 14);
			this._optOrder_1.Location = new System.Drawing.Point(12, 39);
			this._optOrder_1.TabIndex = 18;
			this._optOrder_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optOrder_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optOrder_1.BackColor = System.Drawing.SystemColors.Control;
			this._optOrder_1.CausesValidation = true;
			this._optOrder_1.Enabled = true;
			this._optOrder_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optOrder_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._optOrder_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optOrder_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optOrder_1.TabStop = true;
			this._optOrder_1.Checked = false;
			this._optOrder_1.Visible = true;
			this._optOrder_1.Name = "_optOrder_1";
			this._optOrder_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optOrder_0.Text = "By Vendor";
			this._optOrder_0.Size = new System.Drawing.Size(124, 14);
			this._optOrder_0.Location = new System.Drawing.Point(12, 20);
			this._optOrder_0.TabIndex = 17;
			this._optOrder_0.Checked = true;
			this._optOrder_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optOrder_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optOrder_0.BackColor = System.Drawing.SystemColors.Control;
			this._optOrder_0.CausesValidation = true;
			this._optOrder_0.Enabled = true;
			this._optOrder_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optOrder_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._optOrder_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optOrder_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optOrder_0.TabStop = true;
			this._optOrder_0.Visible = true;
			this._optOrder_0.Name = "_optOrder_0";
			this._Command1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Command1_0.Text = "DISPLAY";
			this.AcceptButton = this._Command1_0;
			this._Command1_0.Size = new System.Drawing.Size(81, 33);
			this._Command1_0.Location = new System.Drawing.Point(188, 41);
			this._Command1_0.TabIndex = 10;
			this._Command1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_0.CausesValidation = true;
			this._Command1_0.Enabled = true;
			this._Command1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_0.TabStop = true;
			this._Command1_0.Name = "_Command1_0";
			this._Command1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Command1_1.Text = "VENDORS >>";
			this._Command1_1.Size = new System.Drawing.Size(81, 33);
			this._Command1_1.Location = new System.Drawing.Point(188, 80);
			this._Command1_1.TabIndex = 9;
			this._Command1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_1.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_1.CausesValidation = true;
			this._Command1_1.Enabled = true;
			this._Command1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_1.TabStop = true;
			this._Command1_1.Name = "_Command1_1";
			this._Command1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Command1_2;
			this._Command1_2.Text = "EXIT";
			this._Command1_2.Size = new System.Drawing.Size(81, 33);
			this._Command1_2.Location = new System.Drawing.Point(188, 120);
			this._Command1_2.TabIndex = 8;
			this._Command1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_2.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_2.CausesValidation = true;
			this._Command1_2.Enabled = true;
			this._Command1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_2.TabStop = true;
			this._Command1_2.Name = "_Command1_2";
			this.Remove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Remove.Text = "REMOVE";
			this.Remove.Size = new System.Drawing.Size(81, 25);
			this.Remove.Location = new System.Drawing.Point(279, 88);
			this.Remove.TabIndex = 7;
			this.Remove.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Remove.BackColor = System.Drawing.SystemColors.Control;
			this.Remove.CausesValidation = true;
			this.Remove.Enabled = true;
			this.Remove.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Remove.Cursor = System.Windows.Forms.Cursors.Default;
			this.Remove.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Remove.TabStop = true;
			this.Remove.Name = "Remove";
			this.ClearList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ClearList.Text = "CLEAR";
			this.ClearList.Size = new System.Drawing.Size(81, 25);
			this.ClearList.Location = new System.Drawing.Point(279, 120);
			this.ClearList.TabIndex = 6;
			this.ClearList.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.ClearList.BackColor = System.Drawing.SystemColors.Control;
			this.ClearList.CausesValidation = true;
			this.ClearList.Enabled = true;
			this.ClearList.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ClearList.Cursor = System.Windows.Forms.Cursors.Default;
			this.ClearList.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ClearList.TabStop = true;
			this.ClearList.Name = "ClearList";
			this.AddVendor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AddVendor.Text = "ADD";
			this.AddVendor.Size = new System.Drawing.Size(81, 25);
			this.AddVendor.Location = new System.Drawing.Point(279, 56);
			this.AddVendor.TabIndex = 5;
			this.AddVendor.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AddVendor.BackColor = System.Drawing.SystemColors.Control;
			this.AddVendor.CausesValidation = true;
			this.AddVendor.Enabled = true;
			this.AddVendor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.AddVendor.Cursor = System.Windows.Forms.Cursors.Default;
			this.AddVendor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.AddVendor.TabStop = true;
			this.AddVendor.Name = "AddVendor";
			this.chkDetail.Text = "Print Detail";
			this.chkDetail.Size = new System.Drawing.Size(89, 17);
			this.chkDetail.Location = new System.Drawing.Point(8, 104);
			this.chkDetail.TabIndex = 4;
			this.chkDetail.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkDetail.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkDetail.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkDetail.BackColor = System.Drawing.SystemColors.Control;
			this.chkDetail.CausesValidation = true;
			this.chkDetail.Enabled = true;
			this.chkDetail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkDetail.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkDetail.TabStop = true;
			this.chkDetail.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkDetail.Visible = true;
			this.chkDetail.Name = "chkDetail";
			_dtDate_0.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_dtDate_0.OcxState");
			this._dtDate_0.Size = new System.Drawing.Size(97, 25);
			this._dtDate_0.Location = new System.Drawing.Point(80, 16);
			this._dtDate_0.TabIndex = 2;
			this._dtDate_0.Name = "_dtDate_0";
			_dtDate_1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("_dtDate_1.OcxState");
			this._dtDate_1.Size = new System.Drawing.Size(97, 25);
			this._dtDate_1.Location = new System.Drawing.Point(80, 56);
			this._dtDate_1.TabIndex = 3;
			this._dtDate_1.Name = "_dtDate_1";
			this.dlgWordOpen.DefaultExt = ".exe";
			this.dlgWordOpen.Title = "Locate MS Word";
			this.Line2.BorderWidth = 5;
			this.Line2.X1 = 184;
			this.Line2.X2 = 184;
			this.Line2.Y1 = -14;
			this.Line2.Y2 = 232;
			this.Line2.BorderColor = System.Drawing.SystemColors.WindowText;
			this.Line2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			this.Line2.Visible = true;
			this.Line2.Name = "Line2";
			this.Label16.Text = "ACTIONS";
			this.Label16.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label16.Size = new System.Drawing.Size(68, 14);
			this.Label16.Location = new System.Drawing.Point(189, 1);
			this.Label16.TabIndex = 13;
			this.Label16.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label16.BackColor = System.Drawing.SystemColors.Control;
			this.Label16.Enabled = true;
			this.Label16.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label16.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label16.UseMnemonic = true;
			this.Label16.Visible = true;
			this.Label16.AutoSize = false;
			this.Label16.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label16.Name = "Label16";
			this.Line3.BorderWidth = 3;
			this.Line3.X1 = 185;
			this.Line3.X2 = 440;
			this.Line3.Y1 = 21;
			this.Line3.Y2 = 21;
			this.Line3.BorderColor = System.Drawing.SystemColors.WindowText;
			this.Line3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			this.Line3.Visible = true;
			this.Line3.Name = "Line3";
			this.Line4.BorderWidth = 4;
			this.Line4.X1 = 272;
			this.Line4.X2 = 272;
			this.Line4.Y1 = -16;
			this.Line4.Y2 = 232;
			this.Line4.BorderColor = System.Drawing.SystemColors.WindowText;
			this.Line4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			this.Line4.Visible = true;
			this.Line4.Name = "Line4";
			this.Label17.Text = "Vendor:";
			this.Label17.Size = new System.Drawing.Size(81, 16);
			this.Label17.Location = new System.Drawing.Point(280, 33);
			this.Label17.TabIndex = 12;
			this.Label17.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label17.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label17.BackColor = System.Drawing.SystemColors.Control;
			this.Label17.Enabled = true;
			this.Label17.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label17.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label17.UseMnemonic = true;
			this.Label17.Visible = true;
			this.Label17.AutoSize = false;
			this.Label17.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label17.Name = "Label17";
			this.Label18.Text = "INDIVIDUAL VENDORS";
			this.Label18.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label18.Size = new System.Drawing.Size(163, 17);
			this.Label18.Location = new System.Drawing.Point(280, 1);
			this.Label18.TabIndex = 11;
			this.Label18.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label18.BackColor = System.Drawing.SystemColors.Control;
			this.Label18.Enabled = true;
			this.Label18.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label18.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label18.UseMnemonic = true;
			this.Label18.Visible = true;
			this.Label18.AutoSize = false;
			this.Label18.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label18.Name = "Label18";
			this.Label2.Text = "Ending Date:";
			this.Label2.Size = new System.Drawing.Size(81, 17);
			this.Label2.Location = new System.Drawing.Point(8, 64);
			this.Label2.TabIndex = 1;
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
			this.Label1.Text = "Starting Date:";
			this.Label1.Size = new System.Drawing.Size(81, 17);
			this.Label1.Location = new System.Drawing.Point(8, 24);
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
			this.Controls.Add(lstVendPrint);
			this.Controls.Add(lstVendors);
			this.Controls.Add(chkAddress);
			this.Controls.Add(Frame1);
			this.Controls.Add(_Command1_0);
			this.Controls.Add(_Command1_1);
			this.Controls.Add(_Command1_2);
			this.Controls.Add(Remove);
			this.Controls.Add(ClearList);
			this.Controls.Add(AddVendor);
			this.Controls.Add(chkDetail);
			this.Controls.Add(_dtDate_0);
			this.Controls.Add(_dtDate_1);
			this.ShapeContainer1.Shapes.Add(Line2);
			this.Controls.Add(Label16);
			this.ShapeContainer1.Shapes.Add(Line3);
			this.ShapeContainer1.Shapes.Add(Line4);
			this.Controls.Add(Label17);
			this.Controls.Add(Label18);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Controls.Add(ShapeContainer1);
			this.Frame1.Controls.Add(_optOrder_1);
			this.Frame1.Controls.Add(_optOrder_0);
			this.Command1.SetIndex(_Command1_0, Convert.ToInt16(0));
			this.Command1.SetIndex(_Command1_1, Convert.ToInt16(1));
			this.Command1.SetIndex(_Command1_2, Convert.ToInt16(2));
			this.dtDate.SetIndex(_dtDate_0, Convert.ToInt16(0));
			this.dtDate.SetIndex(_dtDate_1, Convert.ToInt16(1));
			this.optOrder.SetIndex(_optOrder_1, Convert.ToInt16(1));
			this.optOrder.SetIndex(_optOrder_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.optOrder).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Command1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._dtDate_1).EndInit();
			((System.ComponentModel.ISupportInitialize)this._dtDate_0).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendors).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstVendPrint).EndInit();
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
