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
	partial class frmCheckRegister
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmCheckRegister() : base()
		{
			FormClosed += frmCheckRegister_FormClosed;
			Load += frmCheckRegister_Load;
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
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMSComCtl2.AxDTPicker dtStart;
		public AxMBText.AxTextField txtStartCheck;
		public AxMBText.AxTextField txtEndCheck;
		public AxMSComCtl2.AxDTPicker dtEnd;
		public System.Windows.Forms.Label Label2;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCheckRegister));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.dtStart = new AxMSComCtl2.AxDTPicker();
			this.txtStartCheck = new AxMBText.AxTextField();
			this.txtEndCheck = new AxMBText.AxTextField();
			this.dtEnd = new AxMSComCtl2.AxDTPicker();
			this.Label2 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)this.dtStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtStartCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtEndCheck).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			this.Text = "Check Register";
			this.ClientSize = new System.Drawing.Size(362, 170);
			this.Location = new System.Drawing.Point(4, 23);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.ControlBox = true;
			this.Enabled = true;
			this.KeyPreview = false;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmCheckRegister";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(263, 19);
			this.lstCompanies.Location = new System.Drawing.Point(88, 8);
			this.lstCompanies.TabIndex = 6;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(263, 19);
			this.lstDepartments.Location = new System.Drawing.Point(88, 29);
			this.lstDepartments.TabIndex = 7;
			this.lstDepartments.Name = "lstDepartments";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(271, 128);
			this._Cmd_1.TabIndex = 13;
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
			this._Cmd_0.Location = new System.Drawing.Point(154, 128);
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
			dtStart.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtStart.OcxState");
			this.dtStart.Size = new System.Drawing.Size(100, 28);
			this.dtStart.Location = new System.Drawing.Point(252, 57);
			this.dtStart.TabIndex = 10;
			this.dtStart.Name = "dtStart";
			txtStartCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtStartCheck.OcxState");
			this.txtStartCheck.Size = new System.Drawing.Size(58, 29);
			this.txtStartCheck.Location = new System.Drawing.Point(109, 57);
			this.txtStartCheck.TabIndex = 8;
			this.txtStartCheck.Name = "txtStartCheck";
			txtEndCheck.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtEndCheck.OcxState");
			this.txtEndCheck.Size = new System.Drawing.Size(58, 29);
			this.txtEndCheck.Location = new System.Drawing.Point(109, 86);
			this.txtEndCheck.TabIndex = 9;
			this.txtEndCheck.Name = "txtEndCheck";
			dtEnd.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtEnd.OcxState");
			this.dtEnd.Size = new System.Drawing.Size(100, 28);
			this.dtEnd.Location = new System.Drawing.Point(252, 85);
			this.dtEnd.TabIndex = 11;
			this.dtEnd.Name = "dtEnd";
			this.Label2.Text = "To print all departments for a company, leave Department un-selected.";
			this.Label2.Size = new System.Drawing.Size(134, 46);
			this.Label2.Location = new System.Drawing.Point(9, 121);
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
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label2.Name = "Label2";
			this._Label1_5.Text = "Ending Date:";
			this._Label1_5.Size = new System.Drawing.Size(70, 16);
			this._Label1_5.Location = new System.Drawing.Point(181, 94);
			this._Label1_5.TabIndex = 5;
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
			this._Label1_4.Text = "Starting Date:";
			this._Label1_4.Size = new System.Drawing.Size(70, 16);
			this._Label1_4.Location = new System.Drawing.Point(181, 64);
			this._Label1_4.TabIndex = 4;
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
			this._Label1_3.Text = "Ending Check:";
			this._Label1_3.Size = new System.Drawing.Size(75, 16);
			this._Label1_3.Location = new System.Drawing.Point(33, 94);
			this._Label1_3.TabIndex = 3;
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
			this._Label1_2.Text = "Starting Check:";
			this._Label1_2.Size = new System.Drawing.Size(78, 16);
			this._Label1_2.Location = new System.Drawing.Point(33, 65);
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
			this._Label1_1.Size = new System.Drawing.Size(62, 14);
			this._Label1_1.Location = new System.Drawing.Point(21, 33);
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
			this._Label1_0.Size = new System.Drawing.Size(52, 14);
			this._Label1_0.Location = new System.Drawing.Point(21, 11);
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
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(dtStart);
			this.Controls.Add(txtStartCheck);
			this.Controls.Add(txtEndCheck);
			this.Controls.Add(dtEnd);
			this.Controls.Add(Label2);
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
			((System.ComponentModel.ISupportInitialize)this.dtEnd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtEndCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtStartCheck).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtStart).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
