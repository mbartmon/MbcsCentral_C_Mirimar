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
	partial class AccountDetail
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public AccountDetail() : base()
		{
			Load += AccountDetail_Load;
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
		public System.Windows.Forms.CheckBox chkActive;
		public System.Windows.Forms.ComboBox lstCategories;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public AxMBText.AxTextField txtDescription;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label3;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AccountDetail));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.lstCategories = new System.Windows.Forms.ComboBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.txtDescription = new AxMBText.AxTextField();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Account";
			this.ClientSize = new System.Drawing.Size(449, 150);
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
			this.Name = "AccountDetail";
			this.chkActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkActive.Text = "Active?";
			this.chkActive.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkActive.Size = new System.Drawing.Size(73, 33);
			this.chkActive.Location = new System.Drawing.Point(8, 80);
			this.chkActive.TabIndex = 6;
			this.chkActive.Tag = "active";
			this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkActive.BackColor = System.Drawing.SystemColors.Control;
			this.chkActive.CausesValidation = true;
			this.chkActive.Enabled = true;
			this.chkActive.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkActive.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkActive.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkActive.TabStop = true;
			this.chkActive.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkActive.Visible = true;
			this.chkActive.Name = "chkActive";
			this.lstCategories.Size = new System.Drawing.Size(376, 21);
			this.lstCategories.Location = new System.Drawing.Point(64, 56);
			this.lstCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstCategories.TabIndex = 1;
			this.lstCategories.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstCategories.BackColor = System.Drawing.SystemColors.Window;
			this.lstCategories.CausesValidation = true;
			this.lstCategories.Enabled = true;
			this.lstCategories.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstCategories.IntegralHeight = true;
			this.lstCategories.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstCategories.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstCategories.Sorted = false;
			this.lstCategories.TabStop = true;
			this.lstCategories.Visible = true;
			this.lstCategories.Name = "lstCategories";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(361, 112);
			this._Cmd_1.TabIndex = 4;
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
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(275, 112);
			this._Cmd_0.TabIndex = 2;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			txtDescription.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtDescription.OcxState");
			this.txtDescription.Size = new System.Drawing.Size(359, 29);
			this.txtDescription.Location = new System.Drawing.Point(80, 9);
			this.txtDescription.TabIndex = 0;
			this.txtDescription.Name = "txtDescription";
			this.Label2.Text = "Description:";
			this.Label2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.Size = new System.Drawing.Size(65, 12);
			this.Label2.Location = new System.Drawing.Point(7, 18);
			this.Label2.TabIndex = 5;
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
			this.Label3.Text = "Category:";
			this.Label3.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.Size = new System.Drawing.Size(60, 15);
			this.Label3.Location = new System.Drawing.Point(7, 58);
			this.Label3.TabIndex = 3;
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
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
			this.Controls.Add(chkActive);
			this.Controls.Add(lstCategories);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(txtDescription);
			this.Controls.Add(Label2);
			this.Controls.Add(Label3);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
