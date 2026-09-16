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
	partial class LaserMsg
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public LaserMsg() : base()
		{
			Load += LaserMsg_Load;
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
		public AxMBText.AxTextField Company;
		public AxMBText.AxTextField Department;
		public AxMBText.AxTextField CheckNumber;
		public AxMBText.AxTextField CheckDate;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LaserMsg));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.Company = new AxMBText.AxTextField();
			this.Department = new AxMBText.AxTextField();
			this.CheckNumber = new AxMBText.AxTextField();
			this.CheckDate = new AxMBText.AxTextField();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.Company).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Department).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.CheckNumber).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.CheckDate).BeginInit();
			this.Text = "PRINTING A/P CHECKS";
			this.ClientSize = new System.Drawing.Size(345, 138);
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
			this.Name = "LaserMsg";
			Company.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("Company.OcxState");
			this.Company.Size = new System.Drawing.Size(265, 23);
			this.Company.Location = new System.Drawing.Point(73, 8);
			this.Company.TabIndex = 2;
			this.Company.Name = "Company";
			Department.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("Department.OcxState");
			this.Department.Size = new System.Drawing.Size(265, 23);
			this.Department.Location = new System.Drawing.Point(73, 33);
			this.Department.TabIndex = 3;
			this.Department.Name = "Department";
			CheckNumber.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("CheckNumber.OcxState");
			this.CheckNumber.Size = new System.Drawing.Size(102, 23);
			this.CheckNumber.Location = new System.Drawing.Point(127, 75);
			this.CheckNumber.TabIndex = 6;
			this.CheckNumber.Name = "CheckNumber";
			CheckDate.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("CheckDate.OcxState");
			this.CheckDate.Size = new System.Drawing.Size(102, 23);
			this.CheckDate.Location = new System.Drawing.Point(127, 102);
			this.CheckDate.TabIndex = 7;
			this.CheckDate.Name = "CheckDate";
			this.Label4.Text = "Check Date:";
			this.Label4.Size = new System.Drawing.Size(66, 16);
			this.Label4.Location = new System.Drawing.Point(8, 108);
			this.Label4.TabIndex = 5;
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
			this.Label3.Text = "Check Number:";
			this.Label3.Size = new System.Drawing.Size(119, 16);
			this.Label3.Location = new System.Drawing.Point(8, 80);
			this.Label3.TabIndex = 4;
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
			this.Label2.Text = "Department:";
			this.Label2.Size = new System.Drawing.Size(61, 14);
			this.Label2.Location = new System.Drawing.Point(8, 38);
			this.Label2.TabIndex = 1;
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
			this.Label1.Text = "Company:";
			this.Label1.Size = new System.Drawing.Size(52, 13);
			this.Label1.Location = new System.Drawing.Point(8, 16);
			this.Label1.TabIndex = 0;
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
			((System.ComponentModel.ISupportInitialize)this.CheckDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.CheckNumber).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Department).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Company).EndInit();
			this.Controls.Add(Company);
			this.Controls.Add(Department);
			this.Controls.Add(CheckNumber);
			this.Controls.Add(CheckDate);
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
