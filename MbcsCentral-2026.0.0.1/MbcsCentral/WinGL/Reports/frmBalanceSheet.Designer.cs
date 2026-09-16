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
	partial class frmBalanceSheet
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmBalanceSheet() : base()
		{
			FormClosed += frmBalanceSheet_FormClosed;
			Load += frmBalanceSheet_Load;
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
		public AxLpLib.AxfpCombo lstAccounts;
		public AxLpLib.AxfpCombo lstDepartments;
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
		private System.Windows.Forms.CheckBox withEventsField_chkAccount;
		public System.Windows.Forms.CheckBox chkAccount {
			get { return withEventsField_chkAccount; }
			set {
				if (withEventsField_chkAccount != null) {
					withEventsField_chkAccount.CheckStateChanged -= chkAccount_CheckStateChanged;
				}
				withEventsField_chkAccount = value;
				if (withEventsField_chkAccount != null) {
					withEventsField_chkAccount.CheckStateChanged += chkAccount_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.RadioButton _optYear_1;
		public System.Windows.Forms.RadioButton _optYear_0;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.ComboBox lstMonth;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
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
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
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
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray optYear;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBalanceSheet));
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.lstAccounts = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.chkAccount = new System.Windows.Forms.CheckBox();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._optYear_1 = new System.Windows.Forms.RadioButton();
			this._optYear_0 = new System.Windows.Forms.RadioButton();
			this.lstMonth = new System.Windows.Forms.ComboBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.chkConsolidated = new System.Windows.Forms.CheckBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(this.components);
			this.optYear = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(this.components);
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			this.Frame1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.optYear).BeginInit();
			this.SuspendLayout();
			//
			//lstAccounts
			//
			this.lstAccounts.Location = new System.Drawing.Point(127, 103);
			this.lstAccounts.Name = "lstAccounts";
			this.lstAccounts.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstAccounts.OcxState");
			this.lstAccounts.Size = new System.Drawing.Size(224, 20);
			this.lstAccounts.TabIndex = 6;
			//
			//lstDepartments
			//
			this.lstDepartments.Location = new System.Drawing.Point(170, 71);
			this.lstDepartments.Name = "lstDepartments";
			this.lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(264, 20);
			this.lstDepartments.TabIndex = 4;
			//
			//lstCompanies
			//
			this.lstCompanies.Location = new System.Drawing.Point(64, 6);
			this.lstCompanies.Name = "lstCompanies";
			this.lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(264, 20);
			this.lstCompanies.TabIndex = 1;
			//
			//chkAccount
			//
			this.chkAccount.BackColor = System.Drawing.SystemColors.Control;
			this.chkAccount.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkAccount.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkAccount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkAccount.Location = new System.Drawing.Point(361, 96);
			this.chkAccount.Name = "chkAccount";
			this.chkAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkAccount.Size = new System.Drawing.Size(81, 33);
			this.chkAccount.TabIndex = 15;
			this.chkAccount.Text = "No Account";
			this.chkAccount.UseVisualStyleBackColor = false;
			//
			//Frame1
			//
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Controls.Add(this._optYear_1);
			this.Frame1.Controls.Add(this._optYear_0);
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.Location = new System.Drawing.Point(170, 123);
			this.Frame1.Name = "Frame1";
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Size = new System.Drawing.Size(102, 49);
			this.Frame1.TabIndex = 12;
			this.Frame1.TabStop = false;
			this.Frame1.Text = " ";
			//
			//_optYear_1
			//
			this._optYear_1.BackColor = System.Drawing.SystemColors.Control;
			this._optYear_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._optYear_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optYear_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optYear.SetIndex(this._optYear_1, Convert.ToInt16(1));
			this._optYear_1.Location = new System.Drawing.Point(3, 28);
			this._optYear_1.Name = "_optYear_1";
			this._optYear_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optYear_1.Size = new System.Drawing.Size(95, 15);
			this._optYear_1.TabIndex = 14;
			this._optYear_1.TabStop = true;
			this._optYear_1.Text = "Prior Year";
			this._optYear_1.UseVisualStyleBackColor = false;
			//
			//_optYear_0
			//
			this._optYear_0.BackColor = System.Drawing.SystemColors.Control;
			this._optYear_0.Checked = true;
			this._optYear_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._optYear_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optYear_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optYear.SetIndex(this._optYear_0, Convert.ToInt16(0));
			this._optYear_0.Location = new System.Drawing.Point(3, 10);
			this._optYear_0.Name = "_optYear_0";
			this._optYear_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optYear_0.Size = new System.Drawing.Size(95, 15);
			this._optYear_0.TabIndex = 13;
			this._optYear_0.TabStop = true;
			this._optYear_0.Text = "Current Year";
			this._optYear_0.UseVisualStyleBackColor = false;
			//
			//lstMonth
			//
			this.lstMonth.BackColor = System.Drawing.SystemColors.Window;
			this.lstMonth.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMonth.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstMonth.ForeColor = System.Drawing.SystemColors.WindowText;
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
			this.lstMonth.Location = new System.Drawing.Point(113, 134);
			this.lstMonth.Name = "lstMonth";
			this.lstMonth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstMonth.Size = new System.Drawing.Size(48, 22);
			this.lstMonth.TabIndex = 10;
			//
			//_Cmd_1
			//
			this._Cmd_1.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._Cmd_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Cmd.SetIndex(this._Cmd_1, Convert.ToInt16(1));
			this._Cmd_1.Location = new System.Drawing.Point(364, 131);
			this._Cmd_1.Name = "_Cmd_1";
			this._Cmd_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.TabIndex = 9;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.UseVisualStyleBackColor = false;
			//
			//_Cmd_0
			//
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Cmd.SetIndex(this._Cmd_0, Convert.ToInt16(0));
			this._Cmd_0.Location = new System.Drawing.Point(280, 132);
			this._Cmd_0.Name = "_Cmd_0";
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.TabIndex = 8;
			this._Cmd_0.Text = "DISPLAY";
			this._Cmd_0.UseVisualStyleBackColor = false;
			//
			//chkConsolidated
			//
			this.chkConsolidated.BackColor = System.Drawing.SystemColors.Control;
			this.chkConsolidated.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkConsolidated.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkConsolidated.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkConsolidated.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkConsolidated.Location = new System.Drawing.Point(9, 38);
			this.chkConsolidated.Name = "chkConsolidated";
			this.chkConsolidated.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkConsolidated.Size = new System.Drawing.Size(121, 19);
			this.chkConsolidated.TabIndex = 2;
			this.chkConsolidated.Text = "Consolidated Report";
			this.chkConsolidated.UseVisualStyleBackColor = false;
			//
			//Label5
			//
			this.Label5.BackColor = System.Drawing.SystemColors.Control;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Location = new System.Drawing.Point(162, 137);
			this.Label5.Name = "Label5";
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.Size = new System.Drawing.Size(8, 15);
			this.Label5.TabIndex = 11;
			this.Label5.Text = "/";
			//
			//Label4
			//
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(12, 136);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(98, 16);
			this.Label4.TabIndex = 7;
			this.Label4.Text = "Report Month/Year:";
			//
			//Label3
			//
			this.Label3.BackColor = System.Drawing.SystemColors.Control;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Location = new System.Drawing.Point(12, 105);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(114, 14);
			this.Label3.TabIndex = 5;
			this.Label3.Text = "Account for Profit/Loss:";
			//
			//Label2
			//
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(11, 73);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(161, 15);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "Department ( if not consolidated )";
			//
			//Label1
			//
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(11, 7);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(49, 16);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Company:";
			//
			//Cmd
			//
			//
			//frmBalanceSheet
			//
			this.AcceptButton = this._Cmd_0;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 14f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this._Cmd_1;
			this.ClientSize = new System.Drawing.Size(449, 182);
			this.Controls.Add(this.lstAccounts);
			this.Controls.Add(this.lstDepartments);
			this.Controls.Add(this.lstCompanies);
			this.Controls.Add(this.chkAccount);
			this.Controls.Add(this.Frame1);
			this.Controls.Add(this.lstMonth);
			this.Controls.Add(this._Cmd_1);
			this.Controls.Add(this._Cmd_0);
			this.Controls.Add(this.chkConsolidated);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Location = new System.Drawing.Point(4, 23);
			this.Name = "frmBalanceSheet";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Balance Sheet";
			((System.ComponentModel.ISupportInitialize)this.lstAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.Frame1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.optYear).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
