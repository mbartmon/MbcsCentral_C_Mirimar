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
	partial class BudgetDisp
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BudgetDisp() : base()
		{
			Resize += BudgetDisp_Resize;
			Load += BudgetDisp_Load;
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
		private AxFPSpread.AxvaSpread withEventsField_grdOverview;
		public AxFPSpread.AxvaSpread grdOverview {
			get { return withEventsField_grdOverview; }
			set {
				if (withEventsField_grdOverview != null) {
					withEventsField_grdOverview.DblClick -= grdOverview_DblClick;
				}
				withEventsField_grdOverview = value;
				if (withEventsField_grdOverview != null) {
					withEventsField_grdOverview.DblClick += grdOverview_DblClick;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_Cmd;
		public System.Windows.Forms.Button Cmd {
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
		public AxFPSpread.AxvaSpread grdBudgets;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label _Label1_1;
		public System.Windows.Forms.Label _Label1_0;
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BudgetDisp));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.grdOverview = new AxFPSpread.AxvaSpread();
			this.Cmd = new System.Windows.Forms.Button();
			this.grdBudgets = new AxFPSpread.AxvaSpread();
			this.Label2 = new System.Windows.Forms.Label();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdOverview).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdBudgets).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			this.Text = "Budget Display";
			this.ClientSize = new System.Drawing.Size(600, 345);
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
			this.Name = "BudgetDisp";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(249, 19);
			this.lstDepartments.Location = new System.Drawing.Point(352, 8);
			this.lstDepartments.TabIndex = 3;
			this.lstDepartments.Name = "lstDepartments";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(241, 19);
			this.lstCompanies.Location = new System.Drawing.Point(48, 8);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			grdOverview.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdOverview.OcxState");
			this.grdOverview.Size = new System.Drawing.Size(593, 257);
			this.grdOverview.Location = new System.Drawing.Point(8, 48);
			this.grdOverview.TabIndex = 6;
			this.grdOverview.Name = "grdOverview";
			this.Cmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this.Cmd;
			this.Cmd.Text = "EXIT";
			this.Cmd.Size = new System.Drawing.Size(81, 33);
			this.Cmd.Location = new System.Drawing.Point(8, 312);
			this.Cmd.TabIndex = 5;
			this.Cmd.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Cmd.BackColor = System.Drawing.SystemColors.Control;
			this.Cmd.CausesValidation = true;
			this.Cmd.Enabled = true;
			this.Cmd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Cmd.Cursor = System.Windows.Forms.Cursors.Default;
			this.Cmd.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Cmd.TabStop = true;
			this.Cmd.Name = "Cmd";
			grdBudgets.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdBudgets.OcxState");
			this.grdBudgets.Size = new System.Drawing.Size(489, 257);
			this.grdBudgets.Location = new System.Drawing.Point(8, 48);
			this.grdBudgets.TabIndex = 4;
			this.grdBudgets.Name = "grdBudgets";
			this.Label2.BackColor = System.Drawing.SystemColors.Info;
			this.Label2.Size = new System.Drawing.Size(393, 17);
			this.Label2.Location = new System.Drawing.Point(8, 32);
			this.Label2.TabIndex = 7;
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
			this._Label1_1.Text = "Department";
			this._Label1_1.Size = new System.Drawing.Size(57, 17);
			this._Label1_1.Location = new System.Drawing.Point(296, 8);
			this._Label1_1.TabIndex = 2;
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
			this._Label1_0.Text = "Company";
			this._Label1_0.Size = new System.Drawing.Size(49, 17);
			this._Label1_0.Location = new System.Drawing.Point(0, 8);
			this._Label1_0.TabIndex = 1;
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
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdBudgets).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdOverview).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(grdOverview);
			this.Controls.Add(Cmd);
			this.Controls.Add(grdBudgets);
			this.Controls.Add(Label2);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(_Label1_0);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
