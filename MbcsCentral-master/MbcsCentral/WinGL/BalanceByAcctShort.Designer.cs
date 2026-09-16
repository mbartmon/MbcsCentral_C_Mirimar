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
	partial class BalanceByAcctShort
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BalanceByAcctShort() : base()
		{
			FormClosed += BalanceByAcctShort_FormClosed;
			Resize += BalanceByAcctShort_Resize;
			Load += BalanceByAcctShort_Load;
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
		public Microsoft.VisualBasic.PowerPacks.Printing.PrintForm PrintForm1;
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
		public System.Windows.Forms.Button _Command1_1;
		public System.Windows.Forms.Button _Command1_0;
		public System.Windows.Forms.RadioButton _Option1_2;
		public System.Windows.Forms.RadioButton _Option1_1;
		public System.Windows.Forms.RadioButton _Option1_0;
		public System.Windows.Forms.GroupBox Frame1;
		private System.Windows.Forms.ComboBox withEventsField_lstMonths;
		public System.Windows.Forms.ComboBox lstMonths {
			get { return withEventsField_lstMonths; }
			set {
				if (withEventsField_lstMonths != null) {
					withEventsField_lstMonths.SelectedIndexChanged -= lstMonths_SelectedIndexChanged;
				}
				withEventsField_lstMonths = value;
				if (withEventsField_lstMonths != null) {
					withEventsField_lstMonths.SelectedIndexChanged += lstMonths_SelectedIndexChanged;
				}
			}
		}
		public AxMSHierarchicalFlexGridLib.AxMSHFlexGrid grdBalance;
		public System.Windows.Forms.Label _Label1_2;
		public System.Windows.Forms.Label Label2;
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
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label1;
		private Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray withEventsField_Option1;
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray Option1 {
			get { return withEventsField_Option1; }
			set {
				if (withEventsField_Option1 != null) {
					withEventsField_Option1.CheckedChanged -= Option1_CheckedChanged;
				}
				withEventsField_Option1 = value;
				if (withEventsField_Option1 != null) {
					withEventsField_Option1.CheckedChanged += Option1_CheckedChanged;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BalanceByAcctShort));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.PrintForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this);
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this._Command1_1 = new System.Windows.Forms.Button();
			this._Command1_0 = new System.Windows.Forms.Button();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._Option1_2 = new System.Windows.Forms.RadioButton();
			this._Option1_1 = new System.Windows.Forms.RadioButton();
			this._Option1_0 = new System.Windows.Forms.RadioButton();
			this.lstMonths = new System.Windows.Forms.ComboBox();
			this.grdBalance = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
			this._Label1_2 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Command1 = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label1 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.Option1 = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.grdBalance).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Command1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Option1).BeginInit();
			this.Text = "Balances By Account";
			this.ClientSize = new System.Drawing.Size(697, 352);
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
			this.Name = "BalanceByAcctShort";
			lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(450, 19);
			this.lstAccounts.Location = new System.Drawing.Point(54, 7);
			this.lstAccounts.TabIndex = 0;
			this.lstAccounts.Name = "lstAccounts";
			this._Command1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Command1_1;
			this._Command1_1.Text = "PRINT";
			this.AcceptButton = this._Command1_1;
			this._Command1_1.Size = new System.Drawing.Size(81, 33);
			this._Command1_1.Location = new System.Drawing.Point(85, 309);
			this._Command1_1.TabIndex = 10;
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
			this._Command1_0.Text = "EXIT";
			this._Command1_0.Size = new System.Drawing.Size(81, 33);
			this._Command1_0.Location = new System.Drawing.Point(5, 309);
			this._Command1_0.TabIndex = 6;
			this._Command1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Command1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Command1_0.CausesValidation = true;
			this._Command1_0.Enabled = true;
			this._Command1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Command1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Command1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Command1_0.TabStop = true;
			this._Command1_0.Name = "_Command1_0";
			this.Frame1.Text = "By:";
			this.Frame1.Size = new System.Drawing.Size(295, 43);
			this.Frame1.Location = new System.Drawing.Point(0, 24);
			this.Frame1.TabIndex = 2;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this._Option1_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.Text = "Old Acct Number";
			this._Option1_2.Size = new System.Drawing.Size(104, 19);
			this._Option1_2.Location = new System.Drawing.Point(181, 15);
			this._Option1_2.TabIndex = 5;
			this._Option1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_2.BackColor = System.Drawing.SystemColors.Control;
			this._Option1_2.CausesValidation = true;
			this._Option1_2.Enabled = true;
			this._Option1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_2.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_2.TabStop = true;
			this._Option1_2.Checked = false;
			this._Option1_2.Visible = true;
			this._Option1_2.Name = "_Option1_2";
			this._Option1_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.Text = "New Acct Number";
			this._Option1_1.Size = new System.Drawing.Size(113, 19);
			this._Option1_1.Location = new System.Drawing.Point(61, 15);
			this._Option1_1.TabIndex = 4;
			this._Option1_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_1.BackColor = System.Drawing.SystemColors.Control;
			this._Option1_1.CausesValidation = true;
			this._Option1_1.Enabled = true;
			this._Option1_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_1.TabStop = true;
			this._Option1_1.Checked = false;
			this._Option1_1.Visible = true;
			this._Option1_1.Name = "_Option1_1";
			this._Option1_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.Text = "Name";
			this._Option1_0.Size = new System.Drawing.Size(59, 19);
			this._Option1_0.Location = new System.Drawing.Point(5, 15);
			this._Option1_0.TabIndex = 3;
			this._Option1_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Option1_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._Option1_0.BackColor = System.Drawing.SystemColors.Control;
			this._Option1_0.CausesValidation = true;
			this._Option1_0.Enabled = true;
			this._Option1_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Option1_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Option1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Option1_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._Option1_0.TabStop = true;
			this._Option1_0.Checked = false;
			this._Option1_0.Visible = true;
			this._Option1_0.Name = "_Option1_0";
			this.lstMonths.Size = new System.Drawing.Size(128, 21);
			this.lstMonths.Location = new System.Drawing.Point(559, 7);
			this.lstMonths.Items.AddRange(new object[] {
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December"
			});
			this.lstMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMonths.TabIndex = 1;
			this.lstMonths.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstMonths.BackColor = System.Drawing.SystemColors.Window;
			this.lstMonths.CausesValidation = true;
			this.lstMonths.Enabled = true;
			this.lstMonths.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstMonths.IntegralHeight = true;
			this.lstMonths.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstMonths.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstMonths.Sorted = false;
			this.lstMonths.TabStop = true;
			this.lstMonths.Visible = true;
			this.lstMonths.Name = "lstMonths";
			grdBalance.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("grdBalance.OcxState");
			this.grdBalance.Size = new System.Drawing.Size(688, 231);
			this.grdBalance.Location = new System.Drawing.Point(0, 67);
			this.grdBalance.TabIndex = 7;
			this.grdBalance.Name = "grdBalance";
			this._Label1_2.Text = "Account:";
			this._Label1_2.Size = new System.Drawing.Size(51, 17);
			this._Label1_2.Location = new System.Drawing.Point(2, 8);
			this._Label1_2.TabIndex = 9;
			this._Label1_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label1_2.BackColor = System.Drawing.SystemColors.Control;
			this._Label1_2.Enabled = true;
			this._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label1_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label1_2.UseMnemonic = true;
			this._Label1_2.Visible = true;
			this._Label1_2.AutoSize = false;
			this._Label1_2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label1_2.Name = "_Label1_2";
			this.Label2.Text = "Month";
			this.Label2.Size = new System.Drawing.Size(44, 15);
			this.Label2.Location = new System.Drawing.Point(512, 10);
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
			this.Command1.SetIndex(_Command1_1, Convert.ToInt16(1));
			this.Command1.SetIndex(_Command1_0, Convert.ToInt16(0));
			this.Label1.SetIndex(_Label1_2, Convert.ToInt16(2));
			this.Option1.SetIndex(_Option1_2, Convert.ToInt16(2));
			this.Option1.SetIndex(_Option1_1, Convert.ToInt16(1));
			this.Option1.SetIndex(_Option1_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Option1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Command1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.grdBalance).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			this.Controls.Add(lstAccounts);
			this.Controls.Add(_Command1_1);
			this.Controls.Add(_Command1_0);
			this.Controls.Add(Frame1);
			this.Controls.Add(lstMonths);
			this.Controls.Add(grdBalance);
			this.Controls.Add(_Label1_2);
			this.Controls.Add(Label2);
			this.Frame1.Controls.Add(_Option1_2);
			this.Frame1.Controls.Add(_Option1_1);
			this.Frame1.Controls.Add(_Option1_0);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
