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
	partial class GLentry
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public GLentry() : base()
		{
			Resize += GLentry_Resize;
			Load += GLentry_Load;
			Activated += GLentry_Activated;
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
		public AxLpLib.AxfpCombo lstAccounts;
		public AxLpLib.AxfpCombo lstDepartments;
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
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMBText.AxTextField txtSource;
		public AxMBText.AxTextField txtCheckNumber;
		public AxMSComCtl2.AxDTPicker dtDate;
		public AxMBText.AxTextField txtTotal;
		public System.Windows.Forms.Label Label6;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label4;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GLentry));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstJournals = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstBatch = new AxLpLib.AxfpCombo();
			this.grdEntries = new AxFPSpread.AxvaSpread();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.txtSource = new AxMBText.AxTextField();
			this.txtCheckNumber = new AxMBText.AxTextField();
			this.dtDate = new AxMSComCtl2.AxDTPicker();
			this.txtTotal = new AxMBText.AxTextField();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstJournals).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstBatch).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdEntries).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtCheckNumber).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Manual Journal Entries";
			this.ClientSize = new System.Drawing.Size(712, 362);
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
			this.Name = "GLentry";
			lstJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstJournals.OcxState");
			this.lstJournals.Size = new System.Drawing.Size(215, 19);
			this.lstJournals.Location = new System.Drawing.Point(59, 32);
			this.lstJournals.TabIndex = 3;
			this.lstJournals.Name = "lstJournals";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(286, 19);
			this.lstCompanies.Location = new System.Drawing.Point(59, 5);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(113, 19);
			this.lstAccounts.Location = new System.Drawing.Point(48, 98);
			this.lstAccounts.TabIndex = 14;
			this.lstAccounts.Name = "lstAccounts";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(113, 19);
			this.lstDepartments.Location = new System.Drawing.Point(184, 96);
			this.lstDepartments.TabIndex = 15;
			this.lstDepartments.Name = "lstDepartments";
			lstBatch.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstBatch.OcxState");
			this.lstBatch.Size = new System.Drawing.Size(403, 19);
			this.lstBatch.Location = new System.Drawing.Point(288, 56);
			this.lstBatch.TabIndex = 17;
			this.lstBatch.Name = "lstBatch";
			grdEntries.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdEntries.OcxState");
			this.grdEntries.Size = new System.Drawing.Size(697, 217);
			this.grdEntries.Location = new System.Drawing.Point(8, 96);
			this.grdEntries.TabIndex = 5;
			this.grdEntries.Name = "grdEntries";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "EXIT";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(169, 324);
			this._Cmd_2.TabIndex = 8;
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
			this._Cmd_1.Text = "CLEAR";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(88, 324);
			this._Cmd_1.TabIndex = 7;
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
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(7, 324);
			this._Cmd_0.TabIndex = 6;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			txtSource.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtSource.OcxState");
			this.txtSource.Size = new System.Drawing.Size(230, 29);
			this.txtSource.Location = new System.Drawing.Point(53, 62);
			this.txtSource.TabIndex = 4;
			this.txtSource.Name = "txtSource";
			txtCheckNumber.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtCheckNumber.OcxState");
			this.txtCheckNumber.Size = new System.Drawing.Size(70, 29);
			this.txtCheckNumber.Location = new System.Drawing.Point(514, 0);
			this.txtCheckNumber.TabIndex = 2;
			this.txtCheckNumber.Name = "txtCheckNumber";
			dtDate.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtDate.OcxState");
			this.dtDate.Size = new System.Drawing.Size(100, 20);
			this.dtDate.Location = new System.Drawing.Point(361, 5);
			this.dtDate.TabIndex = 1;
			this.dtDate.Name = "dtDate";
			txtTotal.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtTotal.OcxState");
			this.txtTotal.Size = new System.Drawing.Size(107, 19);
			this.txtTotal.Location = new System.Drawing.Point(576, 318);
			this.txtTotal.TabIndex = 13;
			this.txtTotal.Name = "txtTotal";
			this.Label6.Text = "Batch";
			this.Label6.Size = new System.Drawing.Size(42, 12);
			this.Label6.Location = new System.Drawing.Point(288, 40);
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
			this.Label5.BackColor = System.Drawing.SystemColors.Info;
			this.Label5.Text = "To delete a transaction line: select the line and press DEL";
			this.Label5.Size = new System.Drawing.Size(281, 17);
			this.Label5.Location = new System.Drawing.Point(267, 329);
			this.Label5.TabIndex = 16;
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
			this.Label1.Text = "Company:";
			this.Label1.Size = new System.Drawing.Size(52, 14);
			this.Label1.Location = new System.Drawing.Point(6, 5);
			this.Label1.TabIndex = 12;
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
			this.Label2.Text = "Check#:";
			this.Label2.Size = new System.Drawing.Size(45, 18);
			this.Label2.Location = new System.Drawing.Point(467, 7);
			this.Label2.TabIndex = 11;
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
			this.Label3.Text = "Journal:";
			this.Label3.Size = new System.Drawing.Size(39, 18);
			this.Label3.Location = new System.Drawing.Point(6, 32);
			this.Label3.TabIndex = 10;
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
			this.Label4.Text = "Source:";
			this.Label4.Size = new System.Drawing.Size(41, 18);
			this.Label4.Location = new System.Drawing.Point(7, 68);
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
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtCheckNumber).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtSource).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdEntries).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstBatch).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).EndInit();
			this.Controls.Add(lstJournals);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstBatch);
			this.Controls.Add(grdEntries);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(txtSource);
			this.Controls.Add(txtCheckNumber);
			this.Controls.Add(dtDate);
			this.Controls.Add(txtTotal);
			this.Controls.Add(Label6);
			this.Controls.Add(Label5);
			this.Controls.Add(Label1);
			this.Controls.Add(Label2);
			this.Controls.Add(Label3);
			this.Controls.Add(Label4);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
