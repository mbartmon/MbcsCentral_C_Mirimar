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
	partial class BudgetMaint
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BudgetMaint() : base()
		{
			FormClosed += BudgetMaint_FormClosed;
			Resize += BudgetMaint_Resize;
			Load += BudgetMaint_Load;
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
		public System.Windows.Forms.Button _Cmd1_1;
		public System.Windows.Forms.Button _Cmd1_0;
		public AxMBText.AxTextField txtIncrease;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label _Label1_0;
		public System.Windows.Forms.Panel Frame1;
		public System.Windows.Forms.Button _Cmd3_1;
		public System.Windows.Forms.Button _Cmd3_0;
		private AxFPSpread.AxvaSpread withEventsField_grdBudgets;
		public AxFPSpread.AxvaSpread grdBudgets {
			get { return withEventsField_grdBudgets; }
			set {
				if (withEventsField_grdBudgets != null) {
					withEventsField_grdBudgets.ClickEvent -= grdBudgets_ClickEvent;
					withEventsField_grdBudgets.Enter -= grdBudgets_Enter;
					withEventsField_grdBudgets.LeaveCell -= grdBudgets_LeaveCell;
				}
				withEventsField_grdBudgets = value;
				if (withEventsField_grdBudgets != null) {
					withEventsField_grdBudgets.ClickEvent += grdBudgets_ClickEvent;
					withEventsField_grdBudgets.Enter += grdBudgets_Enter;
					withEventsField_grdBudgets.LeaveCell += grdBudgets_LeaveCell;
				}
			}
		}
		private AxFPSpread.AxvaSpread withEventsField_grdAccounts;
		public AxFPSpread.AxvaSpread grdAccounts {
			get { return withEventsField_grdAccounts; }
			set {
				if (withEventsField_grdAccounts != null) {
					withEventsField_grdAccounts.ButtonClicked -= grdAccounts_ButtonClicked;
				}
				withEventsField_grdAccounts = value;
				if (withEventsField_grdAccounts != null) {
					withEventsField_grdAccounts.ButtonClicked += grdAccounts_ButtonClicked;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd2_1;
		public System.Windows.Forms.Button _Cmd2_0;
		private AxFPSpread.AxvaSpread withEventsField_grdCompDept;
		public AxFPSpread.AxvaSpread grdCompDept {
			get { return withEventsField_grdCompDept; }
			set {
				if (withEventsField_grdCompDept != null) {
					withEventsField_grdCompDept.ClickEvent -= grdCompDept_ClickEvent;
					withEventsField_grdCompDept.DblClick -= grdCompDept_DblClick;
					withEventsField_grdCompDept.Enter -= grdCompDept_Enter;
				}
				withEventsField_grdCompDept = value;
				if (withEventsField_grdCompDept != null) {
					withEventsField_grdCompDept.ClickEvent += grdCompDept_ClickEvent;
					withEventsField_grdCompDept.DblClick += grdCompDept_DblClick;
					withEventsField_grdCompDept.Enter += grdCompDept_Enter;
				}
			}
		}
		public System.Windows.Forms.Label Label2;
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Cmd1;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Cmd1 {
			get { return withEventsField_Cmd1; }
			set {
				if (withEventsField_Cmd1 != null) {
					withEventsField_Cmd1.Click -= Cmd1_Click;
				}
				withEventsField_Cmd1 = value;
				if (withEventsField_Cmd1 != null) {
					withEventsField_Cmd1.Click += Cmd1_Click;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Cmd2;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Cmd2 {
			get { return withEventsField_Cmd2; }
			set {
				if (withEventsField_Cmd2 != null) {
					withEventsField_Cmd2.Click -= Cmd2_Click;
				}
				withEventsField_Cmd2 = value;
				if (withEventsField_Cmd2 != null) {
					withEventsField_Cmd2.Click += Cmd2_Click;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Cmd3;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Cmd3 {
			get { return withEventsField_Cmd3; }
			set {
				if (withEventsField_Cmd3 != null) {
					withEventsField_Cmd3.Click -= Cmd3_Click;
				}
				withEventsField_Cmd3 = value;
				if (withEventsField_Cmd3 != null) {
					withEventsField_Cmd3.Click += Cmd3_Click;
				}
			}
		}
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BudgetMaint));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.Frame1 = new System.Windows.Forms.Panel();
			this._Cmd1_1 = new System.Windows.Forms.Button();
			this._Cmd1_0 = new System.Windows.Forms.Button();
			this.txtIncrease = new AxMBText.AxTextField();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this._Cmd3_1 = new System.Windows.Forms.Button();
			this._Cmd3_0 = new System.Windows.Forms.Button();
			this.grdBudgets = new AxFPSpread.AxvaSpread();
			this.grdAccounts = new AxFPSpread.AxvaSpread();
			this._Cmd2_1 = new System.Windows.Forms.Button();
			this._Cmd2_0 = new System.Windows.Forms.Button();
			this.grdCompDept = new AxFPSpread.AxvaSpread();
			this.Label2 = new System.Windows.Forms.Label();
			this.Cmd1 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Cmd2 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Cmd3 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.txtIncrease).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdBudgets).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdCompDept).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.Text = "Change Budgets";
			this.ClientSize = new System.Drawing.Size(617, 399);
			this.Location = new System.Drawing.Point(4, 23);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
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
			this.Name = "BudgetMaint";
			this.Frame1.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Frame1.Size = new System.Drawing.Size(601, 49);
			this.Frame1.Location = new System.Drawing.Point(0, 352);
			this.Frame1.TabIndex = 5;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Name = "Frame1";
			this._Cmd1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd1_1.Text = "ALL Departments";
			this._Cmd1_1.Size = new System.Drawing.Size(73, 33);
			this._Cmd1_1.Location = new System.Drawing.Point(280, 8);
			this._Cmd1_1.TabIndex = 10;
			this._Cmd1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd1_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd1_1.CausesValidation = true;
			this._Cmd1_1.Enabled = true;
			this._Cmd1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd1_1.TabStop = true;
			this._Cmd1_1.Name = "_Cmd1_1";
			this._Cmd1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd1_0.Text = "Selected Department";
			this._Cmd1_0.Size = new System.Drawing.Size(73, 33);
			this._Cmd1_0.Location = new System.Drawing.Point(200, 8);
			this._Cmd1_0.TabIndex = 9;
			this._Cmd1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd1_0.CausesValidation = true;
			this._Cmd1_0.Enabled = true;
			this._Cmd1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd1_0.TabStop = true;
			this._Cmd1_0.Name = "_Cmd1_0";
			txtIncrease.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtIncrease.OcxState");
			this.txtIncrease.Size = new System.Drawing.Size(49, 33);
			this.txtIncrease.Location = new System.Drawing.Point(48, 8);
			this.txtIncrease.TabIndex = 7;
			this.txtIncrease.Name = "txtIncrease";
			this._Label1_1.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this._Label1_1.Text = "% increase to:";
			this._Label1_1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_1.Size = new System.Drawing.Size(89, 17);
			this._Label1_1.Location = new System.Drawing.Point(104, 16);
			this._Label1_1.TabIndex = 8;
			this._Label1_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_1.Enabled = true;
			this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_1.UseMnemonic = true;
			this._Label1_1.Visible = true;
			this._Label1_1.AutoSize = false;
			this._Label1_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_1.Name = "_Label1_1";
			this._Label1_0.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this._Label1_0.Text = "Apply";
			this._Label1_0.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_0.Size = new System.Drawing.Size(41, 17);
			this._Label1_0.Location = new System.Drawing.Point(8, 16);
			this._Label1_0.TabIndex = 6;
			this._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_0.Enabled = true;
			this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_0.UseMnemonic = true;
			this._Label1_0.Visible = true;
			this._Label1_0.AutoSize = false;
			this._Label1_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_0.Name = "_Label1_0";
			this._Cmd3_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd3_1.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this._Cmd3_1.Text = "Divide January Amount by 12 and copy to ALL";
			this._Cmd3_1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd3_1.Size = new System.Drawing.Size(281, 33);
			this._Cmd3_1.Location = new System.Drawing.Point(288, 352);
			this._Cmd3_1.TabIndex = 12;
			this._Cmd3_1.CausesValidation = true;
			this._Cmd3_1.Enabled = true;
			this._Cmd3_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd3_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd3_1.TabStop = true;
			this._Cmd3_1.Name = "_Cmd3_1";
			this._Cmd3_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd3_0.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this._Cmd3_0.Text = "Copy January Amount to ALL";
			this._Cmd3_0.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd3_0.Size = new System.Drawing.Size(281, 33);
			this._Cmd3_0.Location = new System.Drawing.Point(8, 352);
			this._Cmd3_0.TabIndex = 11;
			this._Cmd3_0.CausesValidation = true;
			this._Cmd3_0.Enabled = true;
			this._Cmd3_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd3_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd3_0.TabStop = true;
			this._Cmd3_0.Name = "_Cmd3_0";
			grdBudgets.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdBudgets.OcxState");
			this.grdBudgets.Size = new System.Drawing.Size(601, 305);
			this.grdBudgets.Location = new System.Drawing.Point(8, 40);
			this.grdBudgets.TabIndex = 4;
			this.grdBudgets.Visible = false;
			this.grdBudgets.Name = "grdBudgets";
			grdAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdAccounts.OcxState");
			this.grdAccounts.Size = new System.Drawing.Size(601, 305);
			this.grdAccounts.Location = new System.Drawing.Point(8, 40);
			this.grdAccounts.TabIndex = 3;
			this.grdAccounts.Visible = false;
			this.grdAccounts.Name = "grdAccounts";
			this._Cmd2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd2_1.BackColor = System.Drawing.Color.Green;
			this._Cmd2_1.Text = "EXIT";
			this._Cmd2_1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd2_1.Size = new System.Drawing.Size(41, 33);
			this._Cmd2_1.Location = new System.Drawing.Point(192, 8);
			this._Cmd2_1.TabIndex = 2;
			this._Cmd2_1.CausesValidation = true;
			this._Cmd2_1.Enabled = true;
			this._Cmd2_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd2_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd2_1.TabStop = true;
			this._Cmd2_1.Name = "_Cmd2_1";
			this._Cmd2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd2_0.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			this._Cmd2_0.Text = "EDIT BUDGETS for Selected Company/Department";
			this._Cmd2_0.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd2_0.Size = new System.Drawing.Size(185, 33);
			this._Cmd2_0.Location = new System.Drawing.Point(8, 8);
			this._Cmd2_0.TabIndex = 1;
			this._Cmd2_0.CausesValidation = true;
			this._Cmd2_0.Enabled = true;
			this._Cmd2_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd2_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd2_0.TabStop = true;
			this._Cmd2_0.Name = "_Cmd2_0";
			grdCompDept.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdCompDept.OcxState");
			this.grdCompDept.Size = new System.Drawing.Size(601, 305);
			this.grdCompDept.Location = new System.Drawing.Point(8, 40);
			this.grdCompDept.TabIndex = 0;
			this.grdCompDept.Name = "grdCompDept";
			this.Label2.Size = new System.Drawing.Size(377, 33);
			this.Label2.Location = new System.Drawing.Point(240, 8);
			this.Label2.TabIndex = 13;
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
			this.Cmd1.SetIndex(_Cmd1_1, Convert.ToInt16(1));
			this.Cmd1.SetIndex(_Cmd1_0, Convert.ToInt16(0));
			this.Cmd2.SetIndex(_Cmd2_1, Convert.ToInt16(1));
			this.Cmd2.SetIndex(_Cmd2_0, Convert.ToInt16(0));
			this.Cmd3.SetIndex(_Cmd3_1, Convert.ToInt16(1));
			this.Cmd3.SetIndex(_Cmd3_0, Convert.ToInt16(0));
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdCompDept).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdBudgets).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtIncrease).EndInit();
			this.Controls.Add(Frame1);
			this.Controls.Add(_Cmd3_1);
			this.Controls.Add(_Cmd3_0);
			this.Controls.Add(grdBudgets);
			this.Controls.Add(grdAccounts);
			this.Controls.Add(_Cmd2_1);
			this.Controls.Add(_Cmd2_0);
			this.Controls.Add(grdCompDept);
			this.Controls.Add(Label2);
			this.Frame1.Controls.Add(_Cmd1_1);
			this.Frame1.Controls.Add(_Cmd1_0);
			this.Frame1.Controls.Add(txtIncrease);
			this.Frame1.Controls.Add(_Label1_1);
			this.Frame1.Controls.Add(_Label1_0);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
