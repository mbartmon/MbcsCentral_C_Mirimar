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
	partial class BalFwdOld
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BalFwdOld() : base()
		{
			Resize += BalFwdOld_Resize;
			Load += BalFwdOld_Load;
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
		public System.Windows.Forms.CheckBox chkTran;
		public System.Windows.Forms.CheckBox chkOpen;
		public AxMSComCtl2.AxDTPicker dtDate;
		public System.Windows.Forms.TextBox txtDescription;
		public System.Windows.Forms.ProgressBar ProgressBar2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.CheckBox chkPrior;
		public System.Windows.Forms.ListBox lstCompanies;
		public System.Windows.Forms.ProgressBar ProgressBar1;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label lblCurrent;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BalFwdOld));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstJournals = new AxLpLib.AxfpCombo();
			this.chkTran = new System.Windows.Forms.CheckBox();
			this.chkOpen = new System.Windows.Forms.CheckBox();
			this.dtDate = new AxMSComCtl2.AxDTPicker();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.ProgressBar2 = new System.Windows.Forms.ProgressBar();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.chkPrior = new System.Windows.Forms.CheckBox();
			this.lstCompanies = new System.Windows.Forms.ListBox();
			this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
			this.Label5 = new System.Windows.Forms.Label();
			this.lblCurrent = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstJournals).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Bring Balances Forward From DOS Version";
			this.ClientSize = new System.Drawing.Size(578, 297);
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
			this.Name = "BalFwdOld";
			lstJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstJournals.OcxState");
			this.lstJournals.Size = new System.Drawing.Size(217, 19);
			this.lstJournals.Location = new System.Drawing.Point(309, 5);
			this.lstJournals.TabIndex = 1;
			this.lstJournals.Name = "lstJournals";
			this.chkTran.Text = "Move PRIOR YEAR Transactions to new Transaction Table";
			this.chkTran.Size = new System.Drawing.Size(309, 18);
			this.chkTran.Location = new System.Drawing.Point(271, 110);
			this.chkTran.TabIndex = 16;
			this.chkTran.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkTran.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkTran.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkTran.BackColor = System.Drawing.SystemColors.Control;
			this.chkTran.CausesValidation = true;
			this.chkTran.Enabled = true;
			this.chkTran.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkTran.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkTran.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkTran.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkTran.TabStop = true;
			this.chkTran.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkTran.Visible = true;
			this.chkTran.Name = "chkTran";
			this.chkOpen.Text = "Create Opennig Balance Transactions";
			this.chkOpen.Size = new System.Drawing.Size(269, 18);
			this.chkOpen.Location = new System.Drawing.Point(271, 88);
			this.chkOpen.TabIndex = 15;
			this.chkOpen.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkOpen.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkOpen.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkOpen.BackColor = System.Drawing.SystemColors.Control;
			this.chkOpen.CausesValidation = true;
			this.chkOpen.Enabled = true;
			this.chkOpen.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkOpen.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkOpen.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkOpen.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkOpen.TabStop = true;
			this.chkOpen.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkOpen.Visible = true;
			this.chkOpen.Name = "chkOpen";
			dtDate.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtDate.OcxState");
			this.dtDate.Size = new System.Drawing.Size(100, 21);
			this.dtDate.Location = new System.Drawing.Point(360, 135);
			this.dtDate.TabIndex = 14;
			this.dtDate.Name = "dtDate";
			this.txtDescription.AutoSize = false;
			this.txtDescription.Size = new System.Drawing.Size(269, 21);
			this.txtDescription.Location = new System.Drawing.Point(270, 41);
			this.txtDescription.TabIndex = 2;
			this.txtDescription.Text = "Openning Balance";
			this.txtDescription.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtDescription.AcceptsReturn = true;
			this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
			this.txtDescription.CausesValidation = true;
			this.txtDescription.Enabled = true;
			this.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDescription.HideSelection = true;
			this.txtDescription.ReadOnly = false;
			this.txtDescription.MaxLength = 0;
			this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDescription.Multiline = false;
			this.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtDescription.TabStop = true;
			this.txtDescription.Visible = true;
			this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtDescription.Name = "txtDescription";
			this.ProgressBar2.Size = new System.Drawing.Size(300, 22);
			this.ProgressBar2.Location = new System.Drawing.Point(271, 268);
			this.ProgressBar2.TabIndex = 7;
			this.ProgressBar2.Name = "ProgressBar2";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(421, 169);
			this._Cmd_1.TabIndex = 6;
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
			this._Cmd_0.Text = "PROCESS";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(325, 169);
			this._Cmd_0.TabIndex = 5;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.chkPrior.Text = "Move PRIOR YEAR Balances to new Balance Table";
			this.chkPrior.Size = new System.Drawing.Size(273, 18);
			this.chkPrior.Location = new System.Drawing.Point(271, 66);
			this.chkPrior.TabIndex = 3;
			this.chkPrior.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkPrior.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkPrior.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkPrior.BackColor = System.Drawing.SystemColors.Control;
			this.chkPrior.CausesValidation = true;
			this.chkPrior.Enabled = true;
			this.chkPrior.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkPrior.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkPrior.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkPrior.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkPrior.TabStop = true;
			this.chkPrior.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkPrior.Visible = true;
			this.chkPrior.Name = "chkPrior";
			this.lstCompanies.Size = new System.Drawing.Size(251, 267);
			this.lstCompanies.Location = new System.Drawing.Point(7, 24);
			this.lstCompanies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstCompanies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lstCompanies.BackColor = System.Drawing.SystemColors.Window;
			this.lstCompanies.CausesValidation = true;
			this.lstCompanies.Enabled = true;
			this.lstCompanies.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstCompanies.IntegralHeight = true;
			this.lstCompanies.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstCompanies.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstCompanies.Sorted = false;
			this.lstCompanies.TabStop = true;
			this.lstCompanies.Visible = true;
			this.lstCompanies.MultiColumn = false;
			this.lstCompanies.Name = "lstCompanies";
			this.ProgressBar1.Size = new System.Drawing.Size(300, 22);
			this.ProgressBar1.Location = new System.Drawing.Point(271, 226);
			this.ProgressBar1.TabIndex = 11;
			this.ProgressBar1.Name = "ProgressBar1";
			this.Label5.Text = "Transaction Date:";
			this.Label5.Size = new System.Drawing.Size(98, 16);
			this.Label5.Location = new System.Drawing.Point(270, 139);
			this.Label5.TabIndex = 13;
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
			this.lblCurrent.Text = " ";
			this.lblCurrent.Size = new System.Drawing.Size(299, 13);
			this.lblCurrent.Location = new System.Drawing.Point(270, 212);
			this.lblCurrent.TabIndex = 12;
			this.lblCurrent.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblCurrent.BackColor = System.Drawing.SystemColors.Control;
			this.lblCurrent.Enabled = true;
			this.lblCurrent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCurrent.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCurrent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCurrent.UseMnemonic = true;
			this.lblCurrent.Visible = true;
			this.lblCurrent.AutoSize = false;
			this.lblCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblCurrent.Name = "lblCurrent";
			this.Label4.Text = "Transaction Description:";
			this.Label4.Size = new System.Drawing.Size(120, 16);
			this.Label4.Location = new System.Drawing.Point(270, 27);
			this.Label4.TabIndex = 10;
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
			this.Label3.Size = new System.Drawing.Size(40, 16);
			this.Label3.Location = new System.Drawing.Point(270, 6);
			this.Label3.TabIndex = 9;
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
			this.Label2.Text = "Overall Progress:";
			this.Label2.Size = new System.Drawing.Size(81, 18);
			this.Label2.Location = new System.Drawing.Point(270, 252);
			this.Label2.TabIndex = 8;
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
			this.Label1.Text = "Choose one or more of the following:";
			this.Label1.Size = new System.Drawing.Size(248, 18);
			this.Label1.Location = new System.Drawing.Point(8, 8);
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
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstJournals).EndInit();
			this.Controls.Add(lstJournals);
			this.Controls.Add(chkTran);
			this.Controls.Add(chkOpen);
			this.Controls.Add(dtDate);
			this.Controls.Add(txtDescription);
			this.Controls.Add(ProgressBar2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(chkPrior);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(ProgressBar1);
			this.Controls.Add(Label5);
			this.Controls.Add(lblCurrent);
			this.Controls.Add(Label4);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
