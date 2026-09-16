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
	partial class CloseYear
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public CloseYear() : base()
		{
			FormClosed += CloseYear_FormClosed;
			Load += CloseYear_Load;
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
		private AxLpLib.AxfpCombo withEventsField_lstAccounts;
		public AxLpLib.AxfpCombo lstAccounts {
			get { return withEventsField_lstAccounts; }
			set {
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.ClickEvent -= lstAccounts_ClickEvent;
				}
				withEventsField_lstAccounts = value;
				if (withEventsField_lstAccounts != null) {
					withEventsField_lstAccounts.ClickEvent += lstAccounts_ClickEvent;
				}
			}
		}
		private System.Windows.Forms.ComboBox withEventsField_lstYear;
		public System.Windows.Forms.ComboBox lstYear {
			get { return withEventsField_lstYear; }
			set {
				if (withEventsField_lstYear != null) {
					withEventsField_lstYear.SelectedIndexChanged -= lstYear_SelectedIndexChanged;
				}
				withEventsField_lstYear = value;
				if (withEventsField_lstYear != null) {
					withEventsField_lstYear.SelectedIndexChanged += lstYear_SelectedIndexChanged;
				}
			}
		}
		public System.Windows.Forms.ToolStripStatusLabel _StatBar_Panel1;
		public System.Windows.Forms.StatusStrip StatBar;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		private AxXGridLib.AxXGrid withEventsField_XGrid1;
		public AxXGridLib.AxXGrid XGrid1 {
			get { return withEventsField_XGrid1; }
			set {
				if (withEventsField_XGrid1 != null) {
					withEventsField_XGrid1.ClickCell -= XGrid1_ClickCell;
				}
				withEventsField_XGrid1 = value;
				if (withEventsField_XGrid1 != null) {
					withEventsField_XGrid1.ClickCell += XGrid1_ClickCell;
				}
			}
		}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CloseYear));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstYear = new System.Windows.Forms.ComboBox();
			this.StatBar = new System.Windows.Forms.StatusStrip();
			this._StatBar_Panel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.XGrid1 = new AxXGridLib.AxXGrid();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.StatBar.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.XGrid1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Close Year";
			this.ClientSize = new System.Drawing.Size(562, 384);
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
			this.Name = "CloseYear";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(241, 19);
			this.lstAccounts.Location = new System.Drawing.Point(128, 328);
			this.lstAccounts.TabIndex = 5;
			this.lstAccounts.Name = "lstAccounts";
			this.lstYear.Size = new System.Drawing.Size(81, 21);
			this.lstYear.Location = new System.Drawing.Point(128, 288);
			this.lstYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstYear.TabIndex = 7;
			this.lstYear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstYear.BackColor = System.Drawing.SystemColors.Window;
			this.lstYear.CausesValidation = true;
			this.lstYear.Enabled = true;
			this.lstYear.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstYear.IntegralHeight = true;
			this.lstYear.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstYear.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstYear.Sorted = false;
			this.lstYear.TabStop = true;
			this.lstYear.Visible = true;
			this.lstYear.Name = "lstYear";
			this.StatBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.StatBar.Size = new System.Drawing.Size(562, 25);
			this.StatBar.Location = new System.Drawing.Point(0, 359);
			this.StatBar.TabIndex = 3;
			this.StatBar.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.StatBar.Name = "StatBar";
			this._StatBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this._StatBar_Panel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._StatBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._StatBar_Panel1.BorderSides = (System.Windows.Forms.ToolStripStatusLabelBorderSides)System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
			this._StatBar_Panel1.Margin = new System.Windows.Forms.Padding(0);
			this._StatBar_Panel1.Size = new System.Drawing.Size(96, 25);
			this._StatBar_Panel1.AutoSize = false;
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "EXIT";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(464, 288);
			this._Cmd_1.TabIndex = 2;
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
			this._Cmd_0.Text = "Close Year";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(384, 288);
			this._Cmd_0.TabIndex = 1;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.Label2.Text = "New Fiscal Year:";
			this.Label2.Size = new System.Drawing.Size(81, 17);
			this.Label2.Location = new System.Drawing.Point(8, 296);
			this.Label2.TabIndex = 6;
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
			this.Label1.Text = "Account For Profit/Loss";
			this.Label1.Size = new System.Drawing.Size(113, 17);
			this.Label1.Location = new System.Drawing.Point(8, 328);
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
			XGrid1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("XGrid1.OcxState");
			this.XGrid1.Size = new System.Drawing.Size(537, 273);
			this.XGrid1.Location = new System.Drawing.Point(8, 8);
			this.XGrid1.TabIndex = 0;
			this.XGrid1.Name = "XGrid1";
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.XGrid1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			this.Controls.Add(lstAccounts);
			this.Controls.Add(lstYear);
			this.Controls.Add(StatBar);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Controls.Add(XGrid1);
			this.StatBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this._StatBar_Panel1 });
			this.StatBar.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
