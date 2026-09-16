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
	partial class rptMaster
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public rptMaster() : base()
		{
			FormClosed += rptMaster_FormClosed;
			Resize += rptMaster_Resize;
			Activated += rptMaster_Activated;
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
		private AxLpLib.AxfpList withEventsField_lstView;
		public AxLpLib.AxfpList lstView {
			get { return withEventsField_lstView; }
			set {
				if (withEventsField_lstView != null) {
					withEventsField_lstView.DblClick -= lstView_DblClick;
				}
				withEventsField_lstView = value;
				if (withEventsField_lstView != null) {
					withEventsField_lstView.DblClick += lstView_DblClick;
				}
			}
		}
		public System.Windows.Forms.CheckBox Check1;
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.ComboBox lstPrinters;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Label lblEdit;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptMaster));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstView = new AxLpLib.AxfpList();
			this.Check1 = new System.Windows.Forms.CheckBox();
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this.lstPrinters = new System.Windows.Forms.ComboBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.lblEdit = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstView).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = " ";
			this.ClientSize = new System.Drawing.Size(683, 203);
			this.Location = new System.Drawing.Point(4, 23);
			this.ControlBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Enabled = true;
			this.KeyPreview = false;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowInTaskbar = true;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "rptMaster";
			lstView.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstView.OcxState");
			this.lstView.Size = new System.Drawing.Size(415, 132);
			this.lstView.Location = new System.Drawing.Point(7, 4);
			this.lstView.TabIndex = 6;
			this.lstView.Name = "lstView";
			this.Check1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Check1.Text = "Condense Print?";
			this.Check1.Size = new System.Drawing.Size(98, 19);
			this.Check1.Location = new System.Drawing.Point(193, 181);
			this.Check1.TabIndex = 5;
			this.Check1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Check1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.Check1.BackColor = System.Drawing.SystemColors.Control;
			this.Check1.CausesValidation = true;
			this.Check1.Enabled = true;
			this.Check1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Check1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Check1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Check1.Appearance = System.Windows.Forms.Appearance.Normal;
			this.Check1.TabStop = true;
			this.Check1.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.Check1.Visible = true;
			this.Check1.Name = "Check1";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_3;
			this._Cmd_3.Text = "A";
			this._Cmd_3.Font = new System.Drawing.Font("Arial", 13.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_3.Size = new System.Drawing.Size(28, 33);
			this._Cmd_3.Location = new System.Drawing.Point(162, 155);
			this._Cmd_3.TabIndex = 4;
			this._Cmd_3.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_3.CausesValidation = true;
			this._Cmd_3.Enabled = true;
			this._Cmd_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_3.TabStop = true;
			this._Cmd_3.Name = "_Cmd_3";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "A";
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.Size = new System.Drawing.Size(28, 33);
			this._Cmd_2.Location = new System.Drawing.Point(134, 155);
			this._Cmd_2.TabIndex = 3;
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			this.lstPrinters.Size = new System.Drawing.Size(218, 21);
			this.lstPrinters.Location = new System.Drawing.Point(192, 155);
			this.lstPrinters.TabIndex = 2;
			this.lstPrinters.Text = " ";
			this.lstPrinters.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstPrinters.BackColor = System.Drawing.SystemColors.Window;
			this.lstPrinters.CausesValidation = true;
			this.lstPrinters.Enabled = true;
			this.lstPrinters.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstPrinters.IntegralHeight = true;
			this.lstPrinters.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstPrinters.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstPrinters.Sorted = false;
			this.lstPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstPrinters.TabStop = true;
			this.lstPrinters.Visible = true;
			this.lstPrinters.Name = "lstPrinters";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_1.Text = "CLOSE";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(46, 155);
			this._Cmd_1.TabIndex = 1;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.TabStop = true;
			this._Cmd_1.Name = "_Cmd_1";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(38, 33);
			this._Cmd_0.Location = new System.Drawing.Point(4, 155);
			this._Cmd_0.Image = (System.Drawing.Image)resources.GetObject("_Cmd_0.Image");
			this._Cmd_0.TabIndex = 0;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.lblEdit.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
			this.lblEdit.Text = "DOUBLE-CLICK A LINE TO EDIT";
			this.lblEdit.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblEdit.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
			this.lblEdit.Size = new System.Drawing.Size(199, 19);
			this.lblEdit.Location = new System.Drawing.Point(413, 156);
			this.lblEdit.TabIndex = 7;
			this.lblEdit.Visible = false;
			this.lblEdit.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblEdit.Enabled = true;
			this.lblEdit.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblEdit.UseMnemonic = true;
			this.lblEdit.AutoSize = false;
			this.lblEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblEdit.Name = "lblEdit";
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstView).EndInit();
			this.Controls.Add(lstView);
			this.Controls.Add(Check1);
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(lstPrinters);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(lblEdit);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
