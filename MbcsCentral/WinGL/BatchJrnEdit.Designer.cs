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
	partial class BatchJrnEdit
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BatchJrnEdit() : base()
		{
			Resize += BatchJrnEdit_Resize;
			Load += BatchJrnEdit_Load;
			Activated += BatchJrnEdit_Activated;
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
		private AxLpLib.AxfpCombo withEventsField_lstBatch;
		public AxLpLib.AxfpCombo lstBatch {
			get { return withEventsField_lstBatch; }
			set {
				if (withEventsField_lstBatch != null) {
					withEventsField_lstBatch.ClickEvent -= lstBatch_ClickEvent;
				}
				withEventsField_lstBatch = value;
				if (withEventsField_lstBatch != null) {
					withEventsField_lstBatch.ClickEvent += lstBatch_ClickEvent;
				}
			}
		}
		public AxLpLib.AxfpCombo lstAccounts;
		public AxLpLib.AxfpCombo lstDepartments;
		public AxLpLib.AxfpCombo lstJournals;
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
		public System.Windows.Forms.Button _cmdBatch_3;
		public System.Windows.Forms.Button _cmdBatch_2;
		public System.Windows.Forms.TextBox txtBatch;
		public System.Windows.Forms.Button _cmdBatch_1;
		public System.Windows.Forms.Button _cmdBatch_0;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_2;
		public AxMBText.AxTextField txtSource;
		private AxFPSpread.AxvaSpread withEventsField_grdEntries;
		public AxFPSpread.AxvaSpread grdEntries {
			get { return withEventsField_grdEntries; }
			set {
				if (withEventsField_grdEntries != null) {
					withEventsField_grdEntries.Change -= grdEntries_Change;
					withEventsField_grdEntries.KeyDownEvent -= grdEntries_KeyDownEvent;
					withEventsField_grdEntries.Leave -= grdEntries_Leave;
					withEventsField_grdEntries.LeaveCell -= grdEntries_LeaveCell;
				}
				withEventsField_grdEntries = value;
				if (withEventsField_grdEntries != null) {
					withEventsField_grdEntries.Change += grdEntries_Change;
					withEventsField_grdEntries.KeyDownEvent += grdEntries_KeyDownEvent;
					withEventsField_grdEntries.Leave += grdEntries_Leave;
					withEventsField_grdEntries.LeaveCell += grdEntries_LeaveCell;
				}
			}
		}
		public AxMBText.AxTextField txtTotal;
		public System.Windows.Forms.Label lblBatch;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
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
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_cmdBatch;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray cmdBatch {
			get { return withEventsField_cmdBatch; }
			set {
				if (withEventsField_cmdBatch != null) {
					withEventsField_cmdBatch.Click -= cmdBatch_Click;
				}
				withEventsField_cmdBatch = value;
				if (withEventsField_cmdBatch != null) {
					withEventsField_cmdBatch.Click += cmdBatch_Click;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BatchJrnEdit));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstBatch = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstJournals = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this._cmdBatch_3 = new System.Windows.Forms.Button();
			this._cmdBatch_2 = new System.Windows.Forms.Button();
			this.txtBatch = new System.Windows.Forms.TextBox();
			this._cmdBatch_1 = new System.Windows.Forms.Button();
			this._cmdBatch_0 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this.txtSource = new AxMBText.AxTextField();
			this.grdEntries = new AxFPSpread.AxvaSpread();
			this.txtTotal = new AxMBText.AxTextField();
			this.lblBatch = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.cmdBatch = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstBatch).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdEntries).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cmdBatch).BeginInit();
			this.Text = "Batch Journal Entry Maintenance";
			this.ClientSize = new System.Drawing.Size(709, 343);
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
			this.Name = "BatchJrnEdit";
			lstBatch.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstBatch.OcxState");
			this.lstBatch.Size = new System.Drawing.Size(91, 19);
			this.lstBatch.Location = new System.Drawing.Point(53, 3);
			this.lstBatch.TabIndex = 0;
			this.lstBatch.Name = "lstBatch";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(113, 19);
			this.lstAccounts.Location = new System.Drawing.Point(42, 77);
			this.lstAccounts.TabIndex = 8;
			this.lstAccounts.Name = "lstAccounts";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(113, 19);
			this.lstDepartments.Location = new System.Drawing.Point(178, 75);
			this.lstDepartments.TabIndex = 7;
			this.lstDepartments.Name = "lstDepartments";
			lstJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstJournals.OcxState");
			this.lstJournals.Size = new System.Drawing.Size(215, 19);
			this.lstJournals.Location = new System.Drawing.Point(53, 47);
			this.lstJournals.TabIndex = 2;
			this.lstJournals.Name = "lstJournals";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(286, 19);
			this.lstCompanies.Location = new System.Drawing.Point(53, 26);
			this.lstCompanies.TabIndex = 1;
			this.lstCompanies.Name = "lstCompanies";
			this._cmdBatch_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdBatch_3.Text = "CANCEL";
			this._cmdBatch_3.Size = new System.Drawing.Size(53, 18);
			this._cmdBatch_3.Location = new System.Drawing.Point(506, 3);
			this._cmdBatch_3.TabIndex = 21;
			this._cmdBatch_3.Visible = false;
			this._cmdBatch_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdBatch_3.BackColor = System.Drawing.SystemColors.Control;
			this._cmdBatch_3.CausesValidation = true;
			this._cmdBatch_3.Enabled = true;
			this._cmdBatch_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdBatch_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdBatch_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdBatch_3.TabStop = true;
			this._cmdBatch_3.Name = "_cmdBatch_3";
			this._cmdBatch_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdBatch_2.Text = "SAVE";
			this._cmdBatch_2.Size = new System.Drawing.Size(53, 18);
			this._cmdBatch_2.Location = new System.Drawing.Point(450, 3);
			this._cmdBatch_2.TabIndex = 20;
			this._cmdBatch_2.Visible = false;
			this._cmdBatch_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdBatch_2.BackColor = System.Drawing.SystemColors.Control;
			this._cmdBatch_2.CausesValidation = true;
			this._cmdBatch_2.Enabled = true;
			this._cmdBatch_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdBatch_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdBatch_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdBatch_2.TabStop = true;
			this._cmdBatch_2.Name = "_cmdBatch_2";
			this.txtBatch.AutoSize = false;
			this.txtBatch.Size = new System.Drawing.Size(86, 19);
			this.txtBatch.Location = new System.Drawing.Point(361, 3);
			this.txtBatch.TabIndex = 19;
			this.txtBatch.Visible = false;
			this.txtBatch.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtBatch.AcceptsReturn = true;
			this.txtBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtBatch.BackColor = System.Drawing.SystemColors.Window;
			this.txtBatch.CausesValidation = true;
			this.txtBatch.Enabled = true;
			this.txtBatch.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtBatch.HideSelection = true;
			this.txtBatch.ReadOnly = false;
			this.txtBatch.MaxLength = 0;
			this.txtBatch.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtBatch.Multiline = false;
			this.txtBatch.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtBatch.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtBatch.TabStop = true;
			this.txtBatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtBatch.Name = "txtBatch";
			this._cmdBatch_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdBatch_1.Text = "ADD NEW";
			this._cmdBatch_1.Size = new System.Drawing.Size(67, 18);
			this._cmdBatch_1.Location = new System.Drawing.Point(217, 3);
			this._cmdBatch_1.TabIndex = 17;
			this._cmdBatch_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdBatch_1.BackColor = System.Drawing.SystemColors.Control;
			this._cmdBatch_1.CausesValidation = true;
			this._cmdBatch_1.Enabled = true;
			this._cmdBatch_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdBatch_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdBatch_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdBatch_1.TabStop = true;
			this._cmdBatch_1.Name = "_cmdBatch_1";
			this._cmdBatch_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdBatch_0.Text = "DELETE";
			this._cmdBatch_0.Size = new System.Drawing.Size(67, 18);
			this._cmdBatch_0.Location = new System.Drawing.Point(147, 3);
			this._cmdBatch_0.TabIndex = 16;
			this._cmdBatch_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdBatch_0.BackColor = System.Drawing.SystemColors.Control;
			this._cmdBatch_0.CausesValidation = true;
			this._cmdBatch_0.Enabled = true;
			this._cmdBatch_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdBatch_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdBatch_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdBatch_0.TabStop = true;
			this._cmdBatch_0.Name = "_cmdBatch_0";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_0.Text = "SAVE";
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(5, 300);
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
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "CLEAR";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(86, 300);
			this._Cmd_1.TabIndex = 11;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.TabStop = true;
			this._Cmd_1.Name = "_Cmd_1";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "EXIT";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(167, 300);
			this._Cmd_2.TabIndex = 10;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			txtSource.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtSource.OcxState");
			this.txtSource.Size = new System.Drawing.Size(230, 29);
			this.txtSource.Location = new System.Drawing.Point(351, 45);
			this.txtSource.TabIndex = 3;
			this.txtSource.Name = "txtSource";
			grdEntries.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdEntries.OcxState");
			this.grdEntries.Size = new System.Drawing.Size(697, 217);
			this.grdEntries.Location = new System.Drawing.Point(2, 75);
			this.grdEntries.TabIndex = 9;
			this.grdEntries.Name = "grdEntries";
			txtTotal.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtTotal.OcxState");
			this.txtTotal.Size = new System.Drawing.Size(107, 19);
			this.txtTotal.Location = new System.Drawing.Point(574, 294);
			this.txtTotal.TabIndex = 13;
			this.txtTotal.Name = "txtTotal";
			this.lblBatch.Text = "Batch Name";
			this.lblBatch.Size = new System.Drawing.Size(65, 12);
			this.lblBatch.Location = new System.Drawing.Point(298, 4);
			this.lblBatch.TabIndex = 18;
			this.lblBatch.Visible = false;
			this.lblBatch.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblBatch.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblBatch.BackColor = System.Drawing.SystemColors.Control;
			this.lblBatch.Enabled = true;
			this.lblBatch.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBatch.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblBatch.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblBatch.UseMnemonic = true;
			this.lblBatch.AutoSize = false;
			this.lblBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblBatch.Name = "lblBatch";
			this.Label2.Text = "Batch";
			this.Label2.Size = new System.Drawing.Size(42, 12);
			this.Label2.Location = new System.Drawing.Point(2, 6);
			this.Label2.TabIndex = 15;
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
			this.Label5.BackColor = System.Drawing.SystemColors.Info;
			this.Label5.Text = "To delete a transaction line: select the line and press DEL";
			this.Label5.Size = new System.Drawing.Size(281, 17);
			this.Label5.Location = new System.Drawing.Point(265, 305);
			this.Label5.TabIndex = 14;
			this.Label5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label5.Enabled = true;
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.UseMnemonic = true;
			this.Label5.Visible = true;
			this.Label5.AutoSize = false;
			this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label5.Name = "Label5";
			this.Label4.Text = "Source:";
			this.Label4.Size = new System.Drawing.Size(41, 18);
			this.Label4.Location = new System.Drawing.Point(305, 51);
			this.Label4.TabIndex = 6;
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
			this.Label3.Text = "Journal:";
			this.Label3.Size = new System.Drawing.Size(39, 18);
			this.Label3.Location = new System.Drawing.Point(0, 47);
			this.Label3.TabIndex = 5;
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
			this.Label1.Text = "Company:";
			this.Label1.Size = new System.Drawing.Size(52, 14);
			this.Label1.Location = new System.Drawing.Point(0, 26);
			this.Label1.TabIndex = 4;
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
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.cmdBatch.SetIndex(_cmdBatch_3, Convert.ToInt16(3));
			this.cmdBatch.SetIndex(_cmdBatch_2, Convert.ToInt16(2));
			this.cmdBatch.SetIndex(_cmdBatch_1, Convert.ToInt16(1));
			this.cmdBatch.SetIndex(_cmdBatch_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.cmdBatch).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdEntries).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtSource).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstBatch).EndInit();
			this.Controls.Add(lstBatch);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstJournals);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(_cmdBatch_3);
			this.Controls.Add(_cmdBatch_2);
			this.Controls.Add(txtBatch);
			this.Controls.Add(_cmdBatch_1);
			this.Controls.Add(_cmdBatch_0);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(txtSource);
			this.Controls.Add(grdEntries);
			this.Controls.Add(txtTotal);
			this.Controls.Add(lblBatch);
			this.Controls.Add(Label2);
			this.Controls.Add(Label5);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
