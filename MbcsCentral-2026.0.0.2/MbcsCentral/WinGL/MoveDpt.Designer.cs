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
	partial class MoveDpt
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public MoveDpt() : base()
		{
			Load += MoveDpt_Load;
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
		public System.Windows.Forms.Button _Command1_1;
		public System.Windows.Forms.Button _Command1_0;
		public System.Windows.Forms.ListBox lstCompanies;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label lblDepartment;
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_Command1;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray Command1 {
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
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MoveDpt));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._Command1_1 = new System.Windows.Forms.Button();
			this._Command1_0 = new System.Windows.Forms.Button();
			this.lstCompanies = new System.Windows.Forms.ListBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.lblDepartment = new System.Windows.Forms.Label();
			this.Command1 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.Command1).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Move Department to Another Company";
			this.ClientSize = new System.Drawing.Size(466, 229);
			this.Location = new System.Drawing.Point(131, 259);
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
			this.Name = "MoveDpt";
			this._Command1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Command1_1.Text = "CANCEL";
			this._Command1_1.Size = new System.Drawing.Size(81, 33);
			this._Command1_1.Location = new System.Drawing.Point(72, 160);
			this._Command1_1.TabIndex = 4;
			this._Command1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_1.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_1.CausesValidation = true;
			this._Command1_1.Enabled = true;
			this._Command1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_1.TabStop = true;
			this._Command1_1.Name = "_Command1_1";
			this._Command1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Command1_0.Text = "SAVE";
			this._Command1_0.Size = new System.Drawing.Size(81, 33);
			this._Command1_0.Location = new System.Drawing.Point(72, 96);
			this._Command1_0.TabIndex = 3;
			this._Command1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_0.CausesValidation = true;
			this._Command1_0.Enabled = true;
			this._Command1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_0.TabStop = true;
			this._Command1_0.Name = "_Command1_0";
			this.lstCompanies.Size = new System.Drawing.Size(192, 189);
			this.lstCompanies.Location = new System.Drawing.Point(272, 20);
			this.lstCompanies.Sorted = true;
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstCompanies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lstCompanies.BackColor = System.Drawing.SystemColors.Window;
			this.lstCompanies.CausesValidation = true;
			this.lstCompanies.Enabled = true;
			this.lstCompanies.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstCompanies.IntegralHeight = true;
			this.lstCompanies.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstCompanies.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lstCompanies.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstCompanies.TabStop = true;
			this.lstCompanies.Visible = true;
			this.lstCompanies.MultiColumn = false;
			this.lstCompanies.Name = "lstCompanies";
			this.Label1.Text = "Select new Company for this Department >>>>";
			this.Label1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.Size = new System.Drawing.Size(263, 19);
			this.Label1.Location = new System.Drawing.Point(7, 60);
			this.Label1.TabIndex = 2;
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
			this.lblDepartment.Size = new System.Drawing.Size(231, 33);
			this.lblDepartment.Location = new System.Drawing.Point(7, 17);
			this.lblDepartment.TabIndex = 1;
			this.lblDepartment.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblDepartment.BackColor = System.Drawing.SystemColors.Control;
			this.lblDepartment.Enabled = true;
			this.lblDepartment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDepartment.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblDepartment.UseMnemonic = true;
			this.lblDepartment.Visible = true;
			this.lblDepartment.AutoSize = false;
			this.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDepartment.Name = "lblDepartment";
			this.Controls.Add(_Command1_1);
			this.Controls.Add(_Command1_0);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(Label1);
			this.Controls.Add(lblDepartment);
			this.Command1.SetIndex(_Command1_1, Convert.ToInt16(1));
			this.Command1.SetIndex(_Command1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Command1).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
