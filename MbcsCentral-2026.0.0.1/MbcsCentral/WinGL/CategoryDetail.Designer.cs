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
	partial class CategoryDetail
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public CategoryDetail() : base()
		{
			Load += CategoryDetail_Load;
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
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.ComboBox lstCategories;
		public AxMBText.AxTextField txtDescription;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CategoryDetail));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.lstCategories = new System.Windows.Forms.ComboBox();
			this.txtDescription = new AxMBText.AxTextField();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Category Detail";
			this.ClientSize = new System.Drawing.Size(521, 151);
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
			this.Name = "CategoryDetail";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "EXIT";
			this.AcceptButton = this._Cmd_1;
			this._Cmd_1.Size = new System.Drawing.Size(57, 25);
			this._Cmd_1.Location = new System.Drawing.Point(16, 120);
			this._Cmd_1.TabIndex = 5;
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
			this._Cmd_0.Size = new System.Drawing.Size(57, 25);
			this._Cmd_0.Location = new System.Drawing.Point(16, 88);
			this._Cmd_0.TabIndex = 4;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.lstCategories.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstCategories.Size = new System.Drawing.Size(425, 22);
			this.lstCategories.Location = new System.Drawing.Point(96, 48);
			this.lstCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstCategories.TabIndex = 3;
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
			txtDescription.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("txtDescription.OcxState");
			this.txtDescription.Size = new System.Drawing.Size(281, 25);
			this.txtDescription.Location = new System.Drawing.Point(96, 8);
			this.txtDescription.TabIndex = 1;
			this.txtDescription.Name = "txtDescription";
			this.Label2.Text = "Sub-Category Of:";
			this.Label2.Size = new System.Drawing.Size(81, 17);
			this.Label2.Location = new System.Drawing.Point(8, 56);
			this.Label2.TabIndex = 2;
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
			this.Label1.Text = "Description:";
			this.Label1.Size = new System.Drawing.Size(57, 17);
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
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(lstCategories);
			this.Controls.Add(txtDescription);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
