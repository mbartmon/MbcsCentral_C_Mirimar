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
	partial class frmTrialBalance
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmTrialBalance() : base()
		{
			FormClosed += frmTrialBalance_FormClosed;
			Load += frmTrialBalance_Load;
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
		private AxLpLib.AxfpCombo withEventsField_lstCompanies;
		public AxLpLib.AxfpCombo lstCompanies {
			get { return withEventsField_lstCompanies; }
			set {
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent -= lstCompanies_ClickEvent;
				}
				withEventsField_lstCompanies = value;
				if (withEventsField_lstCompanies != null) {
					withEventsField_lstCompanies.ClickEvent += lstCompanies_ClickEvent;
				}
			}
		}
		public AxLpLib.AxfpCombo lstDepartments;
		public System.Windows.Forms.CheckBox chkXWorkArea;
		public System.Windows.Forms.CheckBox chkZero;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.ComboBox lstMonth;
		public System.Windows.Forms.RadioButton _optYear_0;
		public System.Windows.Forms.RadioButton _optYear_1;
		public System.Windows.Forms.GroupBox Frame1;
		private System.Windows.Forms.CheckBox withEventsField_chkConsolidated;
		public System.Windows.Forms.CheckBox chkConsolidated {
			get { return withEventsField_chkConsolidated; }
			set {
				if (withEventsField_chkConsolidated != null) {
					withEventsField_chkConsolidated.CheckStateChanged -= chkConsolidated_CheckStateChanged;
				}
				withEventsField_chkConsolidated = value;
				if (withEventsField_chkConsolidated != null) {
					withEventsField_chkConsolidated.CheckStateChanged += chkConsolidated_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label5;
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
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray optYear;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmTrialBalance));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.chkXWorkArea = new System.Windows.Forms.CheckBox();
			this.chkZero = new System.Windows.Forms.CheckBox();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this.lstMonth = new System.Windows.Forms.ComboBox();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._optYear_0 = new System.Windows.Forms.RadioButton();
			this._optYear_1 = new System.Windows.Forms.RadioButton();
			this.chkConsolidated = new System.Windows.Forms.CheckBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.optYear = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.optYear).BeginInit();
			this.Text = "Trial Balance";
			this.ClientSize = new System.Drawing.Size(447, 215);
			this.Location = new System.Drawing.Point(4, 23);
			this.ControlBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
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
			this.Name = "frmTrialBalance";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(264, 19);
			this.lstCompanies.Location = new System.Drawing.Point(88, 18);
			this.lstCompanies.TabIndex = 0;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(264, 19);
			this.lstDepartments.Location = new System.Drawing.Point(177, 79);
			this.lstDepartments.TabIndex = 2;
			this.lstDepartments.Name = "lstDepartments";
			this.chkXWorkArea.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkXWorkArea.Text = "Print Extended Work Area";
			this.chkXWorkArea.Size = new System.Drawing.Size(155, 19);
			this.chkXWorkArea.Location = new System.Drawing.Point(170, 117);
			this.chkXWorkArea.TabIndex = 4;
			this.chkXWorkArea.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkXWorkArea.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkXWorkArea.BackColor = System.Drawing.SystemColors.Control;
			this.chkXWorkArea.CausesValidation = true;
			this.chkXWorkArea.Enabled = true;
			this.chkXWorkArea.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkXWorkArea.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkXWorkArea.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkXWorkArea.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkXWorkArea.TabStop = true;
			this.chkXWorkArea.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkXWorkArea.Visible = true;
			this.chkXWorkArea.Name = "chkXWorkArea";
			this.chkZero.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkZero.Text = "Print Zero Balances";
			this.chkZero.Size = new System.Drawing.Size(121, 19);
			this.chkZero.Location = new System.Drawing.Point(9, 117);
			this.chkZero.TabIndex = 3;
			this.chkZero.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkZero.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkZero.BackColor = System.Drawing.SystemColors.Control;
			this.chkZero.CausesValidation = true;
			this.chkZero.Enabled = true;
			this.chkZero.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkZero.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkZero.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkZero.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkZero.TabStop = true;
			this.chkZero.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkZero.Visible = true;
			this.chkZero.Name = "chkZero";
			this._Cmd_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_0.Text = "DISPLAY";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(276, 151);
			this._Cmd_0.TabIndex = 8;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(360, 150);
			this._Cmd_1.TabIndex = 9;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.CausesValidation = true;
			this._Cmd_1.Enabled = true;
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.TabStop = true;
			this._Cmd_1.Name = "_Cmd_1";
			this.lstMonth.Size = new System.Drawing.Size(48, 21);
			this.lstMonth.Location = new System.Drawing.Point(109, 153);
			this.lstMonth.Items.AddRange(new object[] {
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec"
			});
			this.lstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMonth.TabIndex = 5;
			this.lstMonth.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstMonth.BackColor = System.Drawing.SystemColors.Window;
			this.lstMonth.CausesValidation = true;
			this.lstMonth.Enabled = true;
			this.lstMonth.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstMonth.IntegralHeight = true;
			this.lstMonth.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstMonth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstMonth.Sorted = false;
			this.lstMonth.TabStop = true;
			this.lstMonth.Visible = true;
			this.lstMonth.Name = "lstMonth";
			this.Frame1.Text = " ";
			this.Frame1.Size = new System.Drawing.Size(102, 49);
			this.Frame1.Location = new System.Drawing.Point(166, 142);
			this.Frame1.TabIndex = 12;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this._optYear_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optYear_0.Text = "Current Year";
			this._optYear_0.Size = new System.Drawing.Size(95, 15);
			this._optYear_0.Location = new System.Drawing.Point(3, 10);
			this._optYear_0.TabIndex = 6;
			this._optYear_0.Checked = true;
			this._optYear_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optYear_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optYear_0.BackColor = System.Drawing.SystemColors.Control;
			this._optYear_0.CausesValidation = true;
			this._optYear_0.Enabled = true;
			this._optYear_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optYear_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._optYear_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optYear_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optYear_0.TabStop = true;
			this._optYear_0.Visible = true;
			this._optYear_0.Name = "_optYear_0";
			this._optYear_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optYear_1.Text = "Prior Year";
			this._optYear_1.Size = new System.Drawing.Size(95, 15);
			this._optYear_1.Location = new System.Drawing.Point(3, 28);
			this._optYear_1.TabIndex = 7;
			this._optYear_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optYear_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optYear_1.BackColor = System.Drawing.SystemColors.Control;
			this._optYear_1.CausesValidation = true;
			this._optYear_1.Enabled = true;
			this._optYear_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optYear_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._optYear_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optYear_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optYear_1.TabStop = true;
			this._optYear_1.Checked = false;
			this._optYear_1.Visible = true;
			this._optYear_1.Name = "_optYear_1";
			this.chkConsolidated.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkConsolidated.Text = "Consolidated Report";
			this.chkConsolidated.Size = new System.Drawing.Size(121, 19);
			this.chkConsolidated.Location = new System.Drawing.Point(9, 47);
			this.chkConsolidated.TabIndex = 1;
			this.chkConsolidated.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkConsolidated.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkConsolidated.BackColor = System.Drawing.SystemColors.Control;
			this.chkConsolidated.CausesValidation = true;
			this.chkConsolidated.Enabled = true;
			this.chkConsolidated.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkConsolidated.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkConsolidated.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkConsolidated.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkConsolidated.TabStop = true;
			this.chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkConsolidated.Visible = true;
			this.chkConsolidated.Name = "chkConsolidated";
			this.Label4.Text = "Report Month/Year:";
			this.Label4.Size = new System.Drawing.Size(98, 16);
			this.Label4.Location = new System.Drawing.Point(9, 155);
			this.Label4.TabIndex = 14;
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
			this.Label5.Text = "/";
			this.Label5.Size = new System.Drawing.Size(8, 15);
			this.Label5.Location = new System.Drawing.Point(158, 156);
			this.Label5.TabIndex = 13;
			this.Label5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Label5.BackColor = System.Drawing.SystemColors.Control;
			this.Label5.Enabled = true;
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.UseMnemonic = true;
			this.Label5.Visible = true;
			this.Label5.AutoSize = false;
			this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Label5.Name = "Label5";
			this.Label2.Text = "Department ( if not consolidated )";
			this.Label2.Size = new System.Drawing.Size(161, 15);
			this.Label2.Location = new System.Drawing.Point(9, 82);
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
			this.Label1.Text = "Company:";
			this.Label1.Size = new System.Drawing.Size(49, 16);
			this.Label1.Location = new System.Drawing.Point(9, 21);
			this.Label1.TabIndex = 10;
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
			this.Controls.Add(lstCompanies);
			this.Controls.Add(lstDepartments);
			this.Controls.Add(chkXWorkArea);
			this.Controls.Add(chkZero);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(lstMonth);
			this.Controls.Add(Frame1);
			this.Controls.Add(chkConsolidated);
			this.Controls.Add(Label4);
			this.Controls.Add(Label5);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Frame1.Controls.Add(_optYear_0);
			this.Frame1.Controls.Add(_optYear_1);
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.optYear.SetIndex(_optYear_0, Convert.ToInt16(0));
			this.optYear.SetIndex(_optYear_1, Convert.ToInt16(1));
			((System.ComponentModel.ISupportInitialize)this.optYear).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
