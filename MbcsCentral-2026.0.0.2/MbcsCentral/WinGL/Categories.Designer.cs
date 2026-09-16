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
	partial class Categories
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public Categories() : base()
		{
			Resize += Categories_Resize;
			FormClosing += Categories_FormClosing;
			Load += Categories_Load;
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
		public System.Windows.Forms.Label UpDown1;
		public System.Windows.Forms.Button _Cmd_5;
		public System.Windows.Forms.Button _Cmd_4;
		private System.Windows.Forms.ListBox withEventsField_lstAccounts;
		public System.Windows.Forms.ListBox lstAccounts {
			get { return withEventsField_lstAccounts; }
			set {
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.DoubleClick -= lstAccounts_DoubleClick;
					withEventsField_lstAccounts.Leave -= lstAccounts_Leave;
				}
				withEventsField_lstAccounts = value;
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.DoubleClick += lstAccounts_DoubleClick;
					withEventsField_lstAccounts.Leave += lstAccounts_Leave;
				}
			}
		}
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		private System.Windows.Forms.TreeView withEventsField_lstCategories;
		public System.Windows.Forms.TreeView lstCategories {
			get { return withEventsField_lstCategories; }
			set {
				if (withEventsField_lstCategories != null) {
					withEventsField_lstCategories.AfterLabelEdit -= lstCategories_AfterLabelEdit;
					withEventsField_lstCategories.BeforeLabelEdit -= lstCategories_BeforeLabelEdit;
					withEventsField_lstCategories.MouseDown -= lstCategories_MouseDown;
					withEventsField_lstCategories.MouseMove -= lstCategories_MouseMove;
					withEventsField_lstCategories.NodeMouseClick -= lstCategories_NodeClick;
				}
				withEventsField_lstCategories = value;
				if (withEventsField_lstCategories != null) {
					withEventsField_lstCategories.AfterLabelEdit += lstCategories_AfterLabelEdit;
					withEventsField_lstCategories.BeforeLabelEdit += lstCategories_BeforeLabelEdit;
					withEventsField_lstCategories.MouseDown += lstCategories_MouseDown;
					withEventsField_lstCategories.MouseMove += lstCategories_MouseMove;
					withEventsField_lstCategories.NodeMouseClick += lstCategories_NodeClick;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_cmdClose;
		public System.Windows.Forms.Button cmdClose {
			get { return withEventsField_cmdClose; }
			set {
				if (withEventsField_cmdClose != null) {
					withEventsField_cmdClose.Click -= cmdClose_Click;
				}
				withEventsField_cmdClose = value;
				if (withEventsField_cmdClose != null) {
					withEventsField_cmdClose.Click += cmdClose_Click;
				}
			}
		}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Categories));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.UpDown1 = new System.Windows.Forms.Label();
			this._Cmd_5 = new System.Windows.Forms.Button();
			this._Cmd_4 = new System.Windows.Forms.Button();
			this.lstAccounts = new System.Windows.Forms.ListBox();
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.lstCategories = new System.Windows.Forms.TreeView();
			this.cmdClose = new System.Windows.Forms.Button();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Text = "Categories & Accounts";
			this.ClientSize = new System.Drawing.Size(606, 430);
			this.Location = new System.Drawing.Point(3, 12);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.MinimizeBox = false;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = true;
			this.Enabled = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "Categories";
			this.UpDown1.CausesValidation = false;
			this.UpDown1.Size = new System.Drawing.Size(16, 367);
			this.UpDown1.Location = new System.Drawing.Point(588, 23);
			this.UpDown1.TabIndex = 7;
			this.UpDown1.Text = "UpDown1";
			this.UpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.UpDown1.BackColor = System.Drawing.Color.Red;
			this.UpDown1.Name = "UpDown1";
			this._Cmd_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_5.Text = "DELETE";
			this._Cmd_5.Size = new System.Drawing.Size(81, 25);
			this._Cmd_5.Location = new System.Drawing.Point(521, 399);
			this._Cmd_5.TabIndex = 9;
			this._Cmd_5.TabStop = false;
			this._Cmd_5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_5.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_5.CausesValidation = true;
			this._Cmd_5.Enabled = true;
			this._Cmd_5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_5.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_5.Name = "_Cmd_5";
			this._Cmd_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_4.Text = "ADD";
			this._Cmd_4.Size = new System.Drawing.Size(81, 25);
			this._Cmd_4.Location = new System.Drawing.Point(441, 399);
			this._Cmd_4.TabIndex = 8;
			this._Cmd_4.TabStop = false;
			this._Cmd_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_4.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_4.CausesValidation = true;
			this._Cmd_4.Enabled = true;
			this._Cmd_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_4.Name = "_Cmd_4";
			this.lstAccounts.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.lstAccounts.Font = new System.Drawing.Font("Courier New", 9.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstAccounts.ForeColor = System.Drawing.Color.Black;
			this.lstAccounts.Size = new System.Drawing.Size(307, 369);
			this.lstAccounts.IntegralHeight = false;
			this.lstAccounts.Location = new System.Drawing.Point(281, 22);
			this.lstAccounts.TabIndex = 6;
			this.lstAccounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lstAccounts.CausesValidation = true;
			this.lstAccounts.Enabled = true;
			this.lstAccounts.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstAccounts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lstAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstAccounts.Sorted = false;
			this.lstAccounts.TabStop = true;
			this.lstAccounts.Visible = true;
			this.lstAccounts.MultiColumn = false;
			this.lstAccounts.Name = "lstAccounts";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_3.Text = "EDIT";
			this._Cmd_3.Size = new System.Drawing.Size(81, 25);
			this._Cmd_3.Location = new System.Drawing.Point(361, 399);
			this._Cmd_3.TabIndex = 5;
			this._Cmd_3.TabStop = false;
			this._Cmd_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_3.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_3.CausesValidation = true;
			this._Cmd_3.Enabled = true;
			this._Cmd_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_3.Name = "_Cmd_3";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "DELETE";
			this._Cmd_2.Size = new System.Drawing.Size(81, 25);
			this._Cmd_2.Location = new System.Drawing.Point(166, 399);
			this._Cmd_2.TabIndex = 4;
			this._Cmd_2.TabStop = false;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.Name = "_Cmd_2";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "ADD";
			this._Cmd_1.Size = new System.Drawing.Size(81, 25);
			this._Cmd_1.Location = new System.Drawing.Point(86, 399);
			this._Cmd_1.TabIndex = 3;
			this._Cmd_1.TabStop = false;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.Name = "_Cmd_1";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_0.Text = "EDIT";
			this._Cmd_0.Size = new System.Drawing.Size(81, 25);
			this._Cmd_0.Location = new System.Drawing.Point(7, 399);
			this._Cmd_0.TabIndex = 2;
			this._Cmd_0.TabStop = false;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.Name = "_Cmd_0";
			this.lstCategories.CausesValidation = true;
			this.lstCategories.Size = new System.Drawing.Size(278, 368);
			this.lstCategories.Location = new System.Drawing.Point(4, 22);
			this.lstCategories.TabIndex = 1;
			this.lstCategories.HideSelection = false;
			this.lstCategories.Indent = 42;
			this.lstCategories.LabelEdit = false;
			this.lstCategories.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstCategories.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lstCategories.Name = "lstCategories";
			this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this.cmdClose;
			this.cmdClose.Text = "&Close";
			this.cmdClose.Size = new System.Drawing.Size(64, 23);
			this.cmdClose.Location = new System.Drawing.Point(271, 398);
			this.cmdClose.TabIndex = 0;
			this.cmdClose.TabStop = false;
			this.cmdClose.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdClose.BackColor = System.Drawing.SystemColors.Control;
			this.cmdClose.CausesValidation = true;
			this.cmdClose.Enabled = true;
			this.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdClose.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdClose.Name = "cmdClose";
			this.Label3.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
			this.Label3.Text = "Accounts (* = Active)";
			this.Label3.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.Size = new System.Drawing.Size(128, 17);
			this.Label3.Location = new System.Drawing.Point(357, 3);
			this.Label3.TabIndex = 12;
			this.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label3.Enabled = true;
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.UseMnemonic = true;
			this.Label3.Visible = true;
			this.Label3.AutoSize = false;
			this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label3.Name = "Label3";
			this.Label2.Text = "Label2";
			this.Label2.Size = new System.Drawing.Size(2, 1);
			this.Label2.Location = new System.Drawing.Point(182, 10);
			this.Label2.TabIndex = 11;
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
			this.Label1.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
			this.Label1.Text = "Categories";
			this.Label1.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.Size = new System.Drawing.Size(81, 17);
			this.Label1.Location = new System.Drawing.Point(93, 3);
			this.Label1.TabIndex = 10;
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
			this.Cmd.SetIndex(_Cmd_5, Convert.ToInt16(5));
			this.Cmd.SetIndex(_Cmd_4, Convert.ToInt16(4));
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			this.Controls.Add(UpDown1);
			this.Controls.Add(_Cmd_5);
			this.Controls.Add(_Cmd_4);
			this.Controls.Add(lstAccounts);
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(lstCategories);
			this.Controls.Add(cmdClose);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
