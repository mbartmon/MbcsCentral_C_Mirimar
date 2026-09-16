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
	partial class frmDisbRpt
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmDisbRpt() : base()
		{
			FormClosed += frmDisbRpt_FormClosed;
			Load += frmDisbRpt_Load;
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
		public AxLpLib.AxfpCombo lstDepartments;
		public AxLpLib.AxfpCombo lstBalAccount;
		public AxLpLib.AxfpCombo lstColAcct1;
		public AxLpLib.AxfpCombo lstColAcct2;
		public System.Windows.Forms.CheckBox chkYear;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.ComboBox lstMonth;
		public AxXGridLib.AxXGrid grdJournals;
		public System.Windows.Forms.Label _Label1_5;
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmDisbRpt));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstBalAccount = new AxLpLib.AxfpCombo();
			this.lstColAcct1 = new AxLpLib.AxfpCombo();
			this.lstColAcct2 = new AxLpLib.AxfpCombo();
			this.chkYear = new System.Windows.Forms.CheckBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.lstMonth = new System.Windows.Forms.ComboBox();
			this.grdJournals = new AxXGridLib.AxXGrid();
			this._Label1_5 = new System.Windows.Forms.Label();
			this._Label1_4 = new System.Windows.Forms.Label();
			this._Label1_3 = new System.Windows.Forms.Label();
			this._Label1_2 = new System.Windows.Forms.Label();
			this._Label1_1 = new System.Windows.Forms.Label();
			this._Label1_0 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstBalAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstColAcct1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstColAcct2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdJournals).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			this.Text = "Disbursement Report";
			this.ClientSize = new System.Drawing.Size(683, 254);
			this.Location = new System.Drawing.Point(4, 23);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
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
			this.Name = "frmDisbRpt";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(283, 19);
			this.lstCompanies.Location = new System.Drawing.Point(106, 8);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(283, 19);
			this.lstDepartments.Location = new System.Drawing.Point(106, 32);
			this.lstDepartments.TabIndex = 1;
			this.lstDepartments.Name = "lstDepartments";
			lstBalAccount.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstBalAccount.OcxState");
			this.lstBalAccount.Size = new System.Drawing.Size(283, 19);
			this.lstBalAccount.Location = new System.Drawing.Point(106, 86);
			this.lstBalAccount.TabIndex = 3;
			this.lstBalAccount.Name = "lstBalAccount";
			lstColAcct1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstColAcct1.OcxState");
			this.lstColAcct1.Size = new System.Drawing.Size(283, 19);
			this.lstColAcct1.Location = new System.Drawing.Point(106, 119);
			this.lstColAcct1.TabIndex = 4;
			this.lstColAcct1.Name = "lstColAcct1";
			lstColAcct2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstColAcct2.OcxState");
			this.lstColAcct2.Size = new System.Drawing.Size(283, 19);
			this.lstColAcct2.Location = new System.Drawing.Point(394, 119);
			this.lstColAcct2.TabIndex = 5;
			this.lstColAcct2.Name = "lstColAcct2";
			this.chkYear.Text = "Check for prior year report";
			this.chkYear.Size = new System.Drawing.Size(146, 17);
			this.chkYear.Location = new System.Drawing.Point(216, 60);
			this.chkYear.TabIndex = 15;
			this.chkYear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkYear.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkYear.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkYear.BackColor = System.Drawing.SystemColors.Control;
			this.chkYear.CausesValidation = true;
			this.chkYear.Enabled = true;
			this.chkYear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkYear.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkYear.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkYear.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkYear.TabStop = true;
			this.chkYear.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkYear.Visible = true;
			this.chkYear.Name = "chkYear";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(102, 215);
			this._Cmd_1.TabIndex = 8;
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
			this._Cmd_0.Location = new System.Drawing.Point(8, 215);
			this._Cmd_0.TabIndex = 7;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.lstMonth.Size = new System.Drawing.Size(97, 21);
			this.lstMonth.Location = new System.Drawing.Point(105, 57);
			this.lstMonth.Items.AddRange(new object[] {
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December"
			});
			this.lstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMonth.TabIndex = 2;
			this.lstMonth.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstMonth.BackColor = System.Drawing.SystemColors.Window;
			this.lstMonth.CausesValidation = true;
			this.lstMonth.Enabled = true;
			this.lstMonth.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstMonth.IntegralHeight = true;
			this.lstMonth.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstMonth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstMonth.Sorted = false;
			this.lstMonth.TabStop = true;
			this.lstMonth.Visible = true;
			this.lstMonth.Name = "lstMonth";
			grdJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdJournals.OcxState");
			this.grdJournals.Size = new System.Drawing.Size(668, 37);
			this.grdJournals.Location = new System.Drawing.Point(9, 167);
			this.grdJournals.TabIndex = 6;
			this.grdJournals.Name = "grdJournals";
			this._Label1_5.Text = "Journal Names:";
			this._Label1_5.Size = new System.Drawing.Size(80, 16);
			this._Label1_5.Location = new System.Drawing.Point(8, 151);
			this._Label1_5.TabIndex = 14;
			this._Label1_5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_5.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_5.Enabled = true;
			this._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_5.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_5.UseMnemonic = true;
			this._Label1_5.Visible = true;
			this._Label1_5.AutoSize = false;
			this._Label1_5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_5.Name = "_Label1_5";
			this._Label1_4.Text = "Collection Accounts:";
			this._Label1_4.Size = new System.Drawing.Size(102, 16);
			this._Label1_4.Location = new System.Drawing.Point(8, 122);
			this._Label1_4.TabIndex = 13;
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
			this._Label1_3.Text = "Balance Account:";
			this._Label1_3.Size = new System.Drawing.Size(92, 16);
			this._Label1_3.Location = new System.Drawing.Point(8, 90);
			this._Label1_3.TabIndex = 12;
			this._Label1_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_3.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_3.Enabled = true;
			this._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_3.UseMnemonic = true;
			this._Label1_3.Visible = true;
			this._Label1_3.AutoSize = false;
			this._Label1_3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_3.Name = "_Label1_3";
			this._Label1_2.Text = "Month/Year:";
			this._Label1_2.Size = new System.Drawing.Size(74, 16);
			this._Label1_2.Location = new System.Drawing.Point(8, 61);
			this._Label1_2.TabIndex = 11;
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
			this._Label1_1.Size = new System.Drawing.Size(75, 16);
			this._Label1_1.Location = new System.Drawing.Point(8, 35);
			this._Label1_1.TabIndex = 10;
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
			this._Label1_0.Size = new System.Drawing.Size(50, 16);
			this._Label1_0.Location = new System.Drawing.Point(8, 11);
			this._Label1_0.TabIndex = 9;
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
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(lstBalAccount);
			this.Controls.Add(lstColAcct1);
			this.Controls.Add(lstColAcct2);
			this.Controls.Add(chkYear);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(lstMonth);
			this.Controls.Add(grdJournals);
			this.Controls.Add(_Label1_5);
			this.Controls.Add(_Label1_4);
			this.Controls.Add(_Label1_3);
			this.Controls.Add(_Label1_2);
			this.Controls.Add(_Label1_1);
			this.Controls.Add(_Label1_0);
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Label1.SetIndex(_Label1_5, Convert.ToInt16(5));
			this.Label1.SetIndex(_Label1_4, Convert.ToInt16(4));
			this.Label1.SetIndex(_Label1_3, Convert.ToInt16(3));
			this.Label1.SetIndex(_Label1_2, Convert.ToInt16(2));
			this.Label1.SetIndex(_Label1_1, Convert.ToInt16(1));
			this.Label1.SetIndex(_Label1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdJournals).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstColAcct2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstColAcct1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstBalAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
