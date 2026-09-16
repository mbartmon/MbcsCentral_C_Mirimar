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
	partial class BalanceDisp
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BalanceDisp() : base()
		{
			FormClosed += BalanceDisp_FormClosed;
			Load += BalanceDisp_Load;
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
		private System.Windows.Forms.Button withEventsField_Command1;
		public System.Windows.Forms.Button Command1 {
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
		public AxMSHierarchicalFlexGridLib.AxMSHFlexGrid grdBalance;
		public System.Windows.Forms.RadioButton _Option1_2;
		public System.Windows.Forms.RadioButton _Option1_1;
		public System.Windows.Forms.RadioButton _Option1_0;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.Label _Label1_2;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label _Label1_0;
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BalanceDisp));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.Command1 = new System.Windows.Forms.Button();
			this.grdBalance = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._Option1_2 = new System.Windows.Forms.RadioButton();
			this._Option1_1 = new System.Windows.Forms.RadioButton();
			this._Option1_0 = new System.Windows.Forms.RadioButton();
			this._Label1_2 = new System.Windows.Forms.Label();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.Option1 = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdBalance).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Option1).BeginInit();
			this.Text = "Account Balances";
			this.ClientSize = new System.Drawing.Size(521, 292);
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
			this.Name = "BalanceDisp";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(243, 19);
			this.lstCompanies.Location = new System.Drawing.Point(9, 17);
			this.lstCompanies.TabIndex = 3;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(243, 19);
			this.lstDepartments.Location = new System.Drawing.Point(263, 16);
			this.lstDepartments.TabIndex = 4;
			this.lstDepartments.Name = "lstDepartments";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(450, 19);
			this.lstAccounts.Location = new System.Drawing.Point(56, 48);
			this.lstAccounts.TabIndex = 9;
			this.lstAccounts.Name = "lstAccounts";
			this.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this.Command1;
			this.Command1.Text = "EXIT";
			this.AcceptButton = this.Command1;
			this.Command1.Size = new System.Drawing.Size(81, 33);
			this.Command1.Location = new System.Drawing.Point(10, 247);
			this.Command1.TabIndex = 11;
			this.Command1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Command1.BackColor = System.Drawing.SystemColors.Control;
			this.Command1.CausesValidation = true;
			this.Command1.Enabled = true;
			this.Command1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Command1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Command1.TabStop = true;
			this.Command1.Name = "Command1";
			grdBalance.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdBalance.OcxState");
			this.grdBalance.Size = new System.Drawing.Size(388, 212);
			this.grdBalance.Location = new System.Drawing.Point(129, 72);
			this.grdBalance.TabIndex = 10;
			this.grdBalance.Name = "grdBalance";
			this.Frame1.Text = "By:";
			this.Frame1.Size = new System.Drawing.Size(119, 83);
			this.Frame1.Location = new System.Drawing.Point(9, 67);
			this.Frame1.TabIndex = 5;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this._Option1_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.Text = "Old Acct Number";
			this._Option1_2.Size = new System.Drawing.Size(110, 19);
			this._Option1_2.Location = new System.Drawing.Point(5, 58);
			this._Option1_2.TabIndex = 8;
			this._Option1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.BackColor = System.Drawing.SystemColors.Control;
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
			this._Option1_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.Text = "New Acct Number";
			this._Option1_1.Size = new System.Drawing.Size(113, 19);
			this._Option1_1.Location = new System.Drawing.Point(5, 37);
			this._Option1_1.TabIndex = 7;
			this._Option1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.BackColor = System.Drawing.SystemColors.Control;
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
			this._Option1_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.Text = "Name";
			this._Option1_0.Size = new System.Drawing.Size(59, 19);
			this._Option1_0.Location = new System.Drawing.Point(5, 15);
			this._Option1_0.TabIndex = 6;
			this._Option1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.BackColor = System.Drawing.SystemColors.Control;
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
			this._Label1_2.Text = "Account:";
			this._Label1_2.Size = new System.Drawing.Size(51, 17);
			this._Label1_2.Location = new System.Drawing.Point(9, 50);
			this._Label1_2.TabIndex = 2;
			this._Label1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_2.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_2.Enabled = true;
			this._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_2.UseMnemonic = true;
			this._Label1_2.Visible = true;
			this._Label1_2.AutoSize = false;
			this._Label1_2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_2.Name = "_Label1_2";
			this._Label1_1.Text = "Department:";
			this._Label1_1.Size = new System.Drawing.Size(62, 17);
			this._Label1_1.Location = new System.Drawing.Point(265, 1);
			this._Label1_1.TabIndex = 1;
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
			this._Label1_0.Size = new System.Drawing.Size(51, 17);
			this._Label1_0.Location = new System.Drawing.Point(9, 1);
			this._Label1_0.TabIndex = 0;
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
			this.Label1.SetIndex(_Label1_2, Convert.ToInt16(2));
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			this.Option1.SetIndex(_Option1_2, Convert.ToInt16(2));
			this.Option1.SetIndex(_Option1_1, Convert.ToInt16(1));
			this.Option1.SetIndex(_Option1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Option1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdBalance).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(Command1);
			this.Controls.Add(grdBalance);
			this.Controls.Add(Frame1);
			this.Controls.Add(_Label1_2);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(_Label1_0);
			this.Frame1.Controls.Add(_Option1_2);
			this.Frame1.Controls.Add(_Option1_1);
			this.Frame1.Controls.Add(_Option1_0);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
