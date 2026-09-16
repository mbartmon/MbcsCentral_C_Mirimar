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
	partial class CompanyDepartment
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public CompanyDepartment() : base()
		{
			FormClosed += CompanyDepartment_FormClosed;
			Load += CompanyDepartment_Load;
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
		public System.Windows.Forms.Button _cmdCompanyDepartment_1;
		public System.Windows.Forms.Button _cmdCompanyDepartment_7;
		public System.Windows.Forms.Button _cmdCompanyDepartment_6;
		public System.Windows.Forms.Button _cmdCompanyDepartment_5;
		public System.Windows.Forms.Button _cmdCompanyDepartment_3;
		public System.Windows.Forms.Button _cmdCompanyDepartment_4;
		public System.Windows.Forms.Button _cmdCompanyDepartment_2;
		private System.Windows.Forms.ListBox withEventsField_lstDepartments;
		public System.Windows.Forms.ListBox lstDepartments {
			get { return withEventsField_lstDepartments; }
			set {
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.DoubleClick -= lstDepartments_DoubleClick;
				}
				withEventsField_lstDepartments = value;
				if (withEventsField_lstDepartments != null) {
					withEventsField_lstDepartments.DoubleClick += lstDepartments_DoubleClick;
				}
			}
		}
		public System.Windows.Forms.Button _cmdCompanyDepartment_0;
		private System.Windows.Forms.ListBox withEventsField_lstCompanies;
		public System.Windows.Forms.ListBox lstCompanies {
			get { return withEventsField_lstCompanies; }
			set {
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.SelectedIndexChanged -= lstCompanies_SelectedIndexChanged;
					withEventsField_lstCompanies.DoubleClick -= lstCompanies_DoubleClick;
				}
				withEventsField_lstCompanies = value;
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.SelectedIndexChanged += lstCompanies_SelectedIndexChanged;
					withEventsField_lstCompanies.DoubleClick += lstCompanies_DoubleClick;
				}
			}
		}
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		private Microsoft.VisualBasic.Compatibility.VB6.ButtonArray withEventsField_cmdCompanyDepartment;
		public Microsoft.VisualBasic.Compatibility.VB6.ButtonArray cmdCompanyDepartment {
			get { return withEventsField_cmdCompanyDepartment; }
			set {
				if (withEventsField_cmdCompanyDepartment != null) {
					withEventsField_cmdCompanyDepartment.Click -= cmdCompanyDepartment_Click;
				}
				withEventsField_cmdCompanyDepartment = value;
				if (withEventsField_cmdCompanyDepartment != null) {
					withEventsField_cmdCompanyDepartment.Click += cmdCompanyDepartment_Click;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CompanyDepartment));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this._cmdCompanyDepartment_1 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_7 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_6 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_5 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_3 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_4 = new System.Windows.Forms.Button();
			this._cmdCompanyDepartment_2 = new System.Windows.Forms.Button();
			this.lstDepartments = new System.Windows.Forms.ListBox();
			this._cmdCompanyDepartment_0 = new System.Windows.Forms.Button();
			this.lstCompanies = new System.Windows.Forms.ListBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdCompanyDepartment = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.cmdCompanyDepartment).BeginInit();
			this.BackColor = System.Drawing.SystemColors.Desktop;
			this.Text = "Companies and Departments";
			this.ClientSize = new System.Drawing.Size(544, 276);
			this.Location = new System.Drawing.Point(77, 85);
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
			this.Name = "CompanyDepartment";
			this._cmdCompanyDepartment_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_1.Text = "Remove Company";
			this._cmdCompanyDepartment_1.Size = new System.Drawing.Size(105, 33);
			this._cmdCompanyDepartment_1.Location = new System.Drawing.Point(216, 64);
			this._cmdCompanyDepartment_1.TabIndex = 11;
			this._cmdCompanyDepartment_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_1.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_1.CausesValidation = true;
			this._cmdCompanyDepartment_1.Enabled = true;
			this._cmdCompanyDepartment_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_1.TabStop = true;
			this._cmdCompanyDepartment_1.Name = "_cmdCompanyDepartment_1";
			this._cmdCompanyDepartment_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_7.Text = "Edit Company";
			this._cmdCompanyDepartment_7.Size = new System.Drawing.Size(105, 33);
			this._cmdCompanyDepartment_7.Location = new System.Drawing.Point(56, 231);
			this._cmdCompanyDepartment_7.TabIndex = 10;
			this._cmdCompanyDepartment_7.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_7.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_7.CausesValidation = true;
			this._cmdCompanyDepartment_7.Enabled = true;
			this._cmdCompanyDepartment_7.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_7.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_7.TabStop = true;
			this._cmdCompanyDepartment_7.Name = "_cmdCompanyDepartment_7";
			this._cmdCompanyDepartment_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_6.Text = "Edit Department";
			this._cmdCompanyDepartment_6.Size = new System.Drawing.Size(105, 33);
			this._cmdCompanyDepartment_6.Location = new System.Drawing.Point(384, 232);
			this._cmdCompanyDepartment_6.TabIndex = 9;
			this._cmdCompanyDepartment_6.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_6.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_6.CausesValidation = true;
			this._cmdCompanyDepartment_6.Enabled = true;
			this._cmdCompanyDepartment_6.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_6.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_6.TabStop = true;
			this._cmdCompanyDepartment_6.Name = "_cmdCompanyDepartment_6";
			this._cmdCompanyDepartment_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_5.Text = "EXIT";
			this._cmdCompanyDepartment_5.Size = new System.Drawing.Size(105, 33);
			this._cmdCompanyDepartment_5.Location = new System.Drawing.Point(216, 232);
			this._cmdCompanyDepartment_5.TabIndex = 8;
			this._cmdCompanyDepartment_5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_5.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_5.CausesValidation = true;
			this._cmdCompanyDepartment_5.Enabled = true;
			this._cmdCompanyDepartment_5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_5.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_5.TabStop = true;
			this._cmdCompanyDepartment_5.Name = "_cmdCompanyDepartment_5";
			this._cmdCompanyDepartment_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_3.Text = "Move Department";
			this._cmdCompanyDepartment_3.Size = new System.Drawing.Size(113, 33);
			this._cmdCompanyDepartment_3.Location = new System.Drawing.Point(216, 145);
			this._cmdCompanyDepartment_3.TabIndex = 6;
			this._cmdCompanyDepartment_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_3.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_3.CausesValidation = true;
			this._cmdCompanyDepartment_3.Enabled = true;
			this._cmdCompanyDepartment_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_3.TabStop = true;
			this._cmdCompanyDepartment_3.Name = "_cmdCompanyDepartment_3";
			this._cmdCompanyDepartment_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_4.Text = "Remove Department";
			this._cmdCompanyDepartment_4.Size = new System.Drawing.Size(113, 33);
			this._cmdCompanyDepartment_4.Location = new System.Drawing.Point(216, 178);
			this._cmdCompanyDepartment_4.TabIndex = 5;
			this._cmdCompanyDepartment_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_4.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_4.CausesValidation = true;
			this._cmdCompanyDepartment_4.Enabled = true;
			this._cmdCompanyDepartment_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_4.TabStop = true;
			this._cmdCompanyDepartment_4.Name = "_cmdCompanyDepartment_4";
			this._cmdCompanyDepartment_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_2.Text = "Add Department";
			this._cmdCompanyDepartment_2.Size = new System.Drawing.Size(113, 33);
			this._cmdCompanyDepartment_2.Location = new System.Drawing.Point(216, 112);
			this._cmdCompanyDepartment_2.TabIndex = 4;
			this._cmdCompanyDepartment_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_2.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_2.CausesValidation = true;
			this._cmdCompanyDepartment_2.Enabled = true;
			this._cmdCompanyDepartment_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_2.TabStop = true;
			this._cmdCompanyDepartment_2.Name = "_cmdCompanyDepartment_2";
			this.lstDepartments.Size = new System.Drawing.Size(209, 199);
			this.lstDepartments.Location = new System.Drawing.Point(329, 27);
			this.lstDepartments.Items.AddRange(new object[] { "lstDepartments" });
			this.lstDepartments.TabIndex = 3;
			this.lstDepartments.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstDepartments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lstDepartments.BackColor = System.Drawing.SystemColors.Window;
			this.lstDepartments.CausesValidation = true;
			this.lstDepartments.Enabled = true;
			this.lstDepartments.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstDepartments.IntegralHeight = true;
			this.lstDepartments.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstDepartments.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lstDepartments.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstDepartments.Sorted = false;
			this.lstDepartments.TabStop = true;
			this.lstDepartments.Visible = true;
			this.lstDepartments.MultiColumn = false;
			this.lstDepartments.Name = "lstDepartments";
			this._cmdCompanyDepartment_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._cmdCompanyDepartment_0.Text = "Add New Company";
			this._cmdCompanyDepartment_0.Size = new System.Drawing.Size(105, 33);
			this._cmdCompanyDepartment_0.Location = new System.Drawing.Point(216, 32);
			this._cmdCompanyDepartment_0.TabIndex = 2;
			this._cmdCompanyDepartment_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._cmdCompanyDepartment_0.BackColor = System.Drawing.SystemColors.Control;
			this._cmdCompanyDepartment_0.CausesValidation = true;
			this._cmdCompanyDepartment_0.Enabled = true;
			this._cmdCompanyDepartment_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._cmdCompanyDepartment_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._cmdCompanyDepartment_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cmdCompanyDepartment_0.TabStop = true;
			this._cmdCompanyDepartment_0.Name = "_cmdCompanyDepartment_0";
			this.lstCompanies.Size = new System.Drawing.Size(206, 199);
			this.lstCompanies.Location = new System.Drawing.Point(3, 27);
			this.lstCompanies.Items.AddRange(new object[] { "lstCompanies" });
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
			this.Label2.BackColor = System.Drawing.SystemColors.Desktop;
			this.Label2.Text = "Departments:";
			this.Label2.Size = new System.Drawing.Size(89, 14);
			this.Label2.Location = new System.Drawing.Point(339, 12);
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
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label2.Name = "Label2";
			this.Label1.BackColor = System.Drawing.SystemColors.Desktop;
			this.Label1.Text = "Companies:";
			this.Label1.Size = new System.Drawing.Size(81, 17);
			this.Label1.Location = new System.Drawing.Point(8, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label1.Enabled = true;
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.UseMnemonic = true;
			this.Label1.Visible = true;
			this.Label1.AutoSize = false;
			this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label1.Name = "Label1";
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_1, Convert.ToInt16(1));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_7, Convert.ToInt16(7));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_6, Convert.ToInt16(6));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_5, Convert.ToInt16(5));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_3, Convert.ToInt16(3));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_4, Convert.ToInt16(4));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_2, Convert.ToInt16(2));
			this.cmdCompanyDepartment.SetIndex(_cmdCompanyDepartment_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.cmdCompanyDepartment).EndInit();
			this.Controls.Add(_cmdCompanyDepartment_1);
			this.Controls.Add(_cmdCompanyDepartment_7);
			this.Controls.Add(_cmdCompanyDepartment_6);
			this.Controls.Add(_cmdCompanyDepartment_5);
			this.Controls.Add(_cmdCompanyDepartment_3);
			this.Controls.Add(_cmdCompanyDepartment_4);
			this.Controls.Add(_cmdCompanyDepartment_2);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(_cmdCompanyDepartment_0);
			this.Controls.Add(lstCompanies);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
