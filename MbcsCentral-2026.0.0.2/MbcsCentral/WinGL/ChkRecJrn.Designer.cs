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
	partial class ChkRecJrn
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public ChkRecJrn() : base()
		{
			Activated += ChkRecJrn_Activated;
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
		public AxLpLib.AxfpCombo lstJournals;
		public System.Windows.Forms.CheckBox chkBox;
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
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label lblInfo;
		public System.Windows.Forms.Label Label1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ChkRecJrn));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstJournals = new AxLpLib.AxfpCombo();
			this.chkBox = new System.Windows.Forms.CheckBox();
			this.Command1 = new System.Windows.Forms.Button();
			this.Label2 = new System.Windows.Forms.Label();
			this.lblInfo = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstJournals).BeginInit();
			this.Text = "Check Reconciliation Journal Entry";
			this.ClientSize = new System.Drawing.Size(475, 159);
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
			this.Name = "ChkRecJrn";
			lstJournals.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstJournals.OcxState");
			this.lstJournals.Size = new System.Drawing.Size(217, 19);
			this.lstJournals.Location = new System.Drawing.Point(82, 80);
			this.lstJournals.TabIndex = 0;
			this.lstJournals.Name = "lstJournals";
			this.chkBox.Text = "Use this journal for all subsequent transactions";
			this.chkBox.Size = new System.Drawing.Size(156, 33);
			this.chkBox.Location = new System.Drawing.Point(311, 74);
			this.chkBox.TabIndex = 2;
			this.chkBox.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkBox.BackColor = System.Drawing.SystemColors.Control;
			this.chkBox.CausesValidation = true;
			this.chkBox.Enabled = true;
			this.chkBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkBox.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkBox.TabStop = true;
			this.chkBox.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkBox.Visible = true;
			this.chkBox.Name = "chkBox";
			this.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Command1.Text = "OK";
			this.Command1.Size = new System.Drawing.Size(81, 33);
			this.Command1.Location = new System.Drawing.Point(5, 113);
			this.Command1.TabIndex = 3;
			this.Command1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Command1.BackColor = System.Drawing.SystemColors.Control;
			this.Command1.CausesValidation = true;
			this.Command1.Enabled = true;
			this.Command1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Command1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Command1.TabStop = true;
			this.Command1.Name = "Command1";
			this.Label2.Text = "Journal Name:";
			this.Label2.Size = new System.Drawing.Size(73, 21);
			this.Label2.Location = new System.Drawing.Point(7, 81);
			this.Label2.TabIndex = 5;
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
			this.lblInfo.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			this.lblInfo.Size = new System.Drawing.Size(462, 33);
			this.lblInfo.Location = new System.Drawing.Point(5, 36);
			this.lblInfo.TabIndex = 4;
			this.lblInfo.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblInfo.Enabled = true;
			this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblInfo.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblInfo.UseMnemonic = true;
			this.lblInfo.Visible = true;
			this.lblInfo.AutoSize = false;
			this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblInfo.Name = "lblInfo";
			this.Label1.Text = "Enter the journal name for the following transaction:";
			this.Label1.Size = new System.Drawing.Size(243, 17);
			this.Label1.Location = new System.Drawing.Point(12, 11);
			this.Label1.TabIndex = 1;
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
			this.Controls.Add(lstJournals);
			this.Controls.Add(chkBox);
			this.Controls.Add(Command1);
			this.Controls.Add(Label2);
			this.Controls.Add(lblInfo);
			this.Controls.Add(Label1);
			((System.ComponentModel.ISupportInitialize)this.lstJournals).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
