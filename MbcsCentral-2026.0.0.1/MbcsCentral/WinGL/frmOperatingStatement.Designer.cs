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
	partial class frmOperatingStatement
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmOperatingStatement() : base()
		{
			FormClosed += frmOperatingStatement_FormClosed;
			Load += frmOperatingStatement_Load;
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
		private System.Windows.Forms.ComboBox withEventsField_lstStatement;
		public System.Windows.Forms.ComboBox lstStatement {
			get { return withEventsField_lstStatement; }
			set {
				if (withEventsField_lstStatement != null) {
					withEventsField_lstStatement.Leave -= lstStatement_Leave;
				}
				withEventsField_lstStatement = value;
				if (withEventsField_lstStatement != null) {
					withEventsField_lstStatement.Leave += lstStatement_Leave;
				}
			}
		}
		public System.Windows.Forms.CheckBox chkConsolidated;
		public System.Windows.Forms.Button _Cmd_4;
		public System.Windows.Forms.Button _Cmd_3;
		public System.Windows.Forms.Button _Cmd_2;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		private System.Windows.Forms.CheckBox withEventsField_chkZero;
		public System.Windows.Forms.CheckBox chkZero {
			get { return withEventsField_chkZero; }
			set {
				if (withEventsField_chkZero != null) {
					withEventsField_chkZero.CheckStateChanged -= chkZero_CheckStateChanged;
				}
				withEventsField_chkZero = value;
				if (withEventsField_chkZero != null) {
					withEventsField_chkZero.CheckStateChanged += chkZero_CheckStateChanged;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_chkYTD;
		public System.Windows.Forms.CheckBox chkYTD {
			get { return withEventsField_chkYTD; }
			set {
				if (withEventsField_chkYTD != null) {
					withEventsField_chkYTD.CheckStateChanged -= chkYTD_CheckStateChanged;
				}
				withEventsField_chkYTD = value;
				if (withEventsField_chkYTD != null) {
					withEventsField_chkYTD.CheckStateChanged += chkYTD_CheckStateChanged;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_chkPercent;
		public System.Windows.Forms.CheckBox chkPercent {
			get { return withEventsField_chkPercent; }
			set {
				if (withEventsField_chkPercent != null) {
					withEventsField_chkPercent.CheckStateChanged -= chkPercent_CheckStateChanged;
				}
				withEventsField_chkPercent = value;
				if (withEventsField_chkPercent != null) {
					withEventsField_chkPercent.CheckStateChanged += chkPercent_CheckStateChanged;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_chkPrior;
		public System.Windows.Forms.CheckBox chkPrior {
			get { return withEventsField_chkPrior; }
			set {
				if (withEventsField_chkPrior != null) {
					withEventsField_chkPrior.CheckStateChanged -= chkPrior_CheckStateChanged;
				}
				withEventsField_chkPrior = value;
				if (withEventsField_chkPrior != null) {
					withEventsField_chkPrior.CheckStateChanged += chkPrior_CheckStateChanged;
				}
			}
		}
		public System.Windows.Forms.RadioButton _optCurrent_2;
		public System.Windows.Forms.RadioButton _optCurrent_1;
		public System.Windows.Forms.RadioButton _optCurrent_0;
		public System.Windows.Forms.GroupBox Frame2;
		public System.Windows.Forms.RadioButton _optDetail_2;
		public System.Windows.Forms.RadioButton _optDetail_1;
		public System.Windows.Forms.RadioButton _optDetail_0;
		public System.Windows.Forms.GroupBox Frame1;
		private System.Windows.Forms.Button withEventsField_cmdExpense;
		public System.Windows.Forms.Button cmdExpense {
			get { return withEventsField_cmdExpense; }
			set {
				if (withEventsField_cmdExpense != null) {
					withEventsField_cmdExpense.Click -= cmdExpense_Click;
				}
				withEventsField_cmdExpense = value;
				if (withEventsField_cmdExpense != null) {
					withEventsField_cmdExpense.Click += cmdExpense_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_cmdIncome;
		public System.Windows.Forms.Button cmdIncome {
			get { return withEventsField_cmdIncome; }
			set {
				if (withEventsField_cmdIncome != null) {
					withEventsField_cmdIncome.Click -= cmdIncome_Click;
				}
				withEventsField_cmdIncome = value;
				if (withEventsField_cmdIncome != null) {
					withEventsField_cmdIncome.Click += cmdIncome_Click;
				}
			}
		}
		public System.Windows.Forms.ComboBox lstYear;
		public System.Windows.Forms.ComboBox lstMonth;
		public System.Windows.Forms.Label _Label3_1;
		public System.Windows.Forms.Label _Label3_0;
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
		public Microsoft.VisualBasic.Compatibility.VB6.LabelArray Label3;
		private Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray withEventsField_optCurrent;
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray optCurrent {
			get { return withEventsField_optCurrent; }
			set {
				if (withEventsField_optCurrent != null) {
					withEventsField_optCurrent.CheckedChanged -= optCurrent_CheckedChanged;
				}
				withEventsField_optCurrent = value;
				if (withEventsField_optCurrent != null) {
					withEventsField_optCurrent.CheckedChanged += optCurrent_CheckedChanged;
				}
			}
		}
		private Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray withEventsField_optDetail;
		public Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray optDetail {
			get { return withEventsField_optDetail; }
			set {
				if (withEventsField_optDetail != null) {
					withEventsField_optDetail.CheckedChanged -= optDetail_CheckedChanged;
				}
				withEventsField_optDetail = value;
				if (withEventsField_optDetail != null) {
					withEventsField_optDetail.CheckedChanged += optDetail_CheckedChanged;
				}
			}
		}
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmOperatingStatement));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstCompanies = new AxLpLib.AxfpCombo();
			this.lstDepartments = new AxLpLib.AxfpCombo();
			this.lstStatement = new System.Windows.Forms.ComboBox();
			this.chkConsolidated = new System.Windows.Forms.CheckBox();
			this._Cmd_4 = new System.Windows.Forms.Button();
			this._Cmd_3 = new System.Windows.Forms.Button();
			this._Cmd_2 = new System.Windows.Forms.Button();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.chkZero = new System.Windows.Forms.CheckBox();
			this.chkYTD = new System.Windows.Forms.CheckBox();
			this.chkPercent = new System.Windows.Forms.CheckBox();
			this.chkPrior = new System.Windows.Forms.CheckBox();
			this.Frame2 = new System.Windows.Forms.GroupBox();
			this._optCurrent_2 = new System.Windows.Forms.RadioButton();
			this._optCurrent_1 = new System.Windows.Forms.RadioButton();
			this._optCurrent_0 = new System.Windows.Forms.RadioButton();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this._optDetail_2 = new System.Windows.Forms.RadioButton();
			this._optDetail_1 = new System.Windows.Forms.RadioButton();
			this._optDetail_0 = new System.Windows.Forms.RadioButton();
			this.cmdExpense = new System.Windows.Forms.Button();
			this.cmdIncome = new System.Windows.Forms.Button();
			this.lstYear = new System.Windows.Forms.ComboBox();
			this.lstMonth = new System.Windows.Forms.ComboBox();
			this._Label3_1 = new System.Windows.Forms.Label();
			this._Label3_0 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.Label3 = new Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components);
			this.optCurrent = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.optDetail = new Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components);
			this.Frame2.SuspendLayout();
			this.Frame1.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Label3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.optCurrent).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.optDetail).BeginInit();
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Operating Statement";
			this.ClientSize = new System.Drawing.Size(376, 271);
			this.Location = new System.Drawing.Point(4, 23);
			this.ControlBox = false;
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
			this.Name = "frmOperatingStatement";
			lstCompanies.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstCompanies.OcxState");
			this.lstCompanies.Size = new System.Drawing.Size(217, 19);
			this.lstCompanies.Location = new System.Drawing.Point(66, 42);
			this.lstCompanies.TabIndex = 1;
			this.lstCompanies.Name = "lstCompanies";
			lstDepartments.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("lstDepartments.OcxState");
			this.lstDepartments.Size = new System.Drawing.Size(217, 19);
			this.lstDepartments.Location = new System.Drawing.Point(66, 65);
			this.lstDepartments.TabIndex = 3;
			this.lstDepartments.Name = "lstDepartments";
			this.lstStatement.Size = new System.Drawing.Size(176, 21);
			this.lstStatement.Location = new System.Drawing.Point(103, 8);
			this.lstStatement.TabIndex = 0;
			this.lstStatement.Text = " ";
			this.lstStatement.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstStatement.BackColor = System.Drawing.SystemColors.Window;
			this.lstStatement.CausesValidation = true;
			this.lstStatement.Enabled = true;
			this.lstStatement.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstStatement.IntegralHeight = true;
			this.lstStatement.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstStatement.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstStatement.Sorted = false;
			this.lstStatement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstStatement.TabStop = true;
			this.lstStatement.Visible = true;
			this.lstStatement.Name = "lstStatement";
			this.chkConsolidated.Text = "Consolidated";
			this.chkConsolidated.Size = new System.Drawing.Size(81, 22);
			this.chkConsolidated.Location = new System.Drawing.Point(287, 42);
			this.chkConsolidated.TabIndex = 2;
			this.chkConsolidated.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkConsolidated.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this._Cmd_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_4.Text = "DELETE";
			this._Cmd_4.Size = new System.Drawing.Size(81, 33);
			this._Cmd_4.Location = new System.Drawing.Point(293, 235);
			this._Cmd_4.TabIndex = 26;
			this._Cmd_4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_4.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_4.CausesValidation = true;
			this._Cmd_4.Enabled = true;
			this._Cmd_4.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_4.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_4.TabStop = true;
			this._Cmd_4.Name = "_Cmd_4";
			this._Cmd_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_3.Text = "NEW";
			this._Cmd_3.Size = new System.Drawing.Size(81, 33);
			this._Cmd_3.Location = new System.Drawing.Point(293, 194);
			this._Cmd_3.TabIndex = 25;
			this._Cmd_3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_3.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_3.CausesValidation = true;
			this._Cmd_3.Enabled = true;
			this._Cmd_3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_3.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_3.TabStop = true;
			this._Cmd_3.Name = "_Cmd_3";
			this._Cmd_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._Cmd_2.Text = "CLEAR";
			this._Cmd_2.Size = new System.Drawing.Size(81, 33);
			this._Cmd_2.Location = new System.Drawing.Point(293, 153);
			this._Cmd_2.TabIndex = 24;
			this._Cmd_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_2.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_2.CausesValidation = true;
			this._Cmd_2.Enabled = true;
			this._Cmd_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_2.TabStop = true;
			this._Cmd_2.Name = "_Cmd_2";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(293, 112);
			this._Cmd_1.TabIndex = 19;
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
			this._Cmd_0.Text = "DISPLAY";
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(293, 71);
			this._Cmd_0.TabIndex = 18;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.chkZero.Text = "Print Zero Balances";
			this.chkZero.Size = new System.Drawing.Size(118, 15);
			this.chkZero.Location = new System.Drawing.Point(169, 243);
			this.chkZero.TabIndex = 17;
			this.chkZero.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkZero.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.chkYTD.Text = "Print YTD";
			this.chkYTD.Size = new System.Drawing.Size(97, 15);
			this.chkYTD.Location = new System.Drawing.Point(169, 223);
			this.chkYTD.TabIndex = 16;
			this.chkYTD.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkYTD.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkYTD.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkYTD.BackColor = System.Drawing.SystemColors.Control;
			this.chkYTD.CausesValidation = true;
			this.chkYTD.Enabled = true;
			this.chkYTD.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkYTD.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkYTD.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkYTD.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkYTD.TabStop = true;
			this.chkYTD.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkYTD.Visible = true;
			this.chkYTD.Name = "chkYTD";
			this.chkPercent.Text = "Show Percentage";
			this.chkPercent.Size = new System.Drawing.Size(106, 15);
			this.chkPercent.Location = new System.Drawing.Point(169, 204);
			this.chkPercent.TabIndex = 15;
			this.chkPercent.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkPercent.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkPercent.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkPercent.BackColor = System.Drawing.SystemColors.Control;
			this.chkPercent.CausesValidation = true;
			this.chkPercent.Enabled = true;
			this.chkPercent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkPercent.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkPercent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkPercent.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkPercent.TabStop = true;
			this.chkPercent.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkPercent.Visible = true;
			this.chkPercent.Name = "chkPercent";
			this.chkPrior.Text = "Show Prior Year";
			this.chkPrior.Size = new System.Drawing.Size(97, 15);
			this.chkPrior.Location = new System.Drawing.Point(169, 184);
			this.chkPrior.TabIndex = 14;
			this.chkPrior.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.chkPrior.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chkPrior.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.chkPrior.BackColor = System.Drawing.SystemColors.Control;
			this.chkPrior.CausesValidation = true;
			this.chkPrior.Enabled = true;
			this.chkPrior.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkPrior.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkPrior.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkPrior.Appearance = System.Windows.Forms.Appearance.Normal;
			this.chkPrior.TabStop = true;
			this.chkPrior.CheckState = System.Windows.Forms.CheckState.Unchecked;
			this.chkPrior.Visible = true;
			this.chkPrior.Name = "chkPrior";
			this.Frame2.Size = new System.Drawing.Size(68, 86);
			this.Frame2.Location = new System.Drawing.Point(95, 178);
			this.Frame2.TabIndex = 23;
			this.Frame2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame2.BackColor = System.Drawing.SystemColors.Control;
			this.Frame2.Enabled = true;
			this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame2.Visible = true;
			this.Frame2.Padding = new System.Windows.Forms.Padding(0);
			this.Frame2.Name = "Frame2";
			this._optCurrent_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_2.Text = "YTD";
			this._optCurrent_2.Size = new System.Drawing.Size(45, 22);
			this._optCurrent_2.Location = new System.Drawing.Point(7, 49);
			this._optCurrent_2.TabIndex = 13;
			this._optCurrent_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optCurrent_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_2.BackColor = System.Drawing.SystemColors.Control;
			this._optCurrent_2.CausesValidation = true;
			this._optCurrent_2.Enabled = true;
			this._optCurrent_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optCurrent_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._optCurrent_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optCurrent_2.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optCurrent_2.TabStop = true;
			this._optCurrent_2.Checked = false;
			this._optCurrent_2.Visible = true;
			this._optCurrent_2.Name = "_optCurrent_2";
			this._optCurrent_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_1.Text = "QTD";
			this._optCurrent_1.Size = new System.Drawing.Size(49, 22);
			this._optCurrent_1.Location = new System.Drawing.Point(7, 29);
			this._optCurrent_1.TabIndex = 12;
			this._optCurrent_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optCurrent_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_1.BackColor = System.Drawing.SystemColors.Control;
			this._optCurrent_1.CausesValidation = true;
			this._optCurrent_1.Enabled = true;
			this._optCurrent_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optCurrent_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._optCurrent_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optCurrent_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optCurrent_1.TabStop = true;
			this._optCurrent_1.Checked = false;
			this._optCurrent_1.Visible = true;
			this._optCurrent_1.Name = "_optCurrent_1";
			this._optCurrent_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_0.Text = "Current";
			this._optCurrent_0.Size = new System.Drawing.Size(58, 22);
			this._optCurrent_0.Location = new System.Drawing.Point(7, 9);
			this._optCurrent_0.TabIndex = 11;
			this._optCurrent_0.Checked = true;
			this._optCurrent_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optCurrent_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optCurrent_0.BackColor = System.Drawing.SystemColors.Control;
			this._optCurrent_0.CausesValidation = true;
			this._optCurrent_0.Enabled = true;
			this._optCurrent_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optCurrent_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._optCurrent_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optCurrent_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optCurrent_0.TabStop = true;
			this._optCurrent_0.Visible = true;
			this._optCurrent_0.Name = "_optCurrent_0";
			this.Frame1.Size = new System.Drawing.Size(77, 85);
			this.Frame1.Location = new System.Drawing.Point(14, 178);
			this.Frame1.TabIndex = 22;
			this.Frame1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Enabled = true;
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Visible = true;
			this.Frame1.Padding = new System.Windows.Forms.Padding(0);
			this.Frame1.Name = "Frame1";
			this._optDetail_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_2.Text = "Totals";
			this._optDetail_2.Size = new System.Drawing.Size(64, 19);
			this._optDetail_2.Location = new System.Drawing.Point(7, 53);
			this._optDetail_2.TabIndex = 10;
			this._optDetail_2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optDetail_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_2.BackColor = System.Drawing.SystemColors.Control;
			this._optDetail_2.CausesValidation = true;
			this._optDetail_2.Enabled = true;
			this._optDetail_2.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optDetail_2.Cursor = System.Windows.Forms.Cursors.Default;
			this._optDetail_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optDetail_2.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optDetail_2.TabStop = true;
			this._optDetail_2.Checked = false;
			this._optDetail_2.Visible = true;
			this._optDetail_2.Name = "_optDetail_2";
			this._optDetail_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_1.Text = "Summary";
			this._optDetail_1.Size = new System.Drawing.Size(65, 19);
			this._optDetail_1.Location = new System.Drawing.Point(7, 32);
			this._optDetail_1.TabIndex = 9;
			this._optDetail_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optDetail_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_1.BackColor = System.Drawing.SystemColors.Control;
			this._optDetail_1.CausesValidation = true;
			this._optDetail_1.Enabled = true;
			this._optDetail_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optDetail_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._optDetail_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optDetail_1.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optDetail_1.TabStop = true;
			this._optDetail_1.Checked = false;
			this._optDetail_1.Visible = true;
			this._optDetail_1.Name = "_optDetail_1";
			this._optDetail_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_0.Text = "Detail";
			this._optDetail_0.Size = new System.Drawing.Size(52, 19);
			this._optDetail_0.Location = new System.Drawing.Point(7, 11);
			this._optDetail_0.TabIndex = 8;
			this._optDetail_0.Checked = true;
			this._optDetail_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._optDetail_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._optDetail_0.BackColor = System.Drawing.SystemColors.Control;
			this._optDetail_0.CausesValidation = true;
			this._optDetail_0.Enabled = true;
			this._optDetail_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._optDetail_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._optDetail_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._optDetail_0.Appearance = System.Windows.Forms.Appearance.Normal;
			this._optDetail_0.TabStop = true;
			this._optDetail_0.Visible = true;
			this._optDetail_0.Name = "_optDetail_0";
			this.cmdExpense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdExpense.Text = "EXPENSE ACCOUNTS";
			this.cmdExpense.Size = new System.Drawing.Size(257, 23);
			this.cmdExpense.Location = new System.Drawing.Point(13, 150);
			this.cmdExpense.TabIndex = 7;
			this.cmdExpense.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdExpense.BackColor = System.Drawing.SystemColors.Control;
			this.cmdExpense.CausesValidation = true;
			this.cmdExpense.Enabled = true;
			this.cmdExpense.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdExpense.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdExpense.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdExpense.TabStop = true;
			this.cmdExpense.Name = "cmdExpense";
			this.cmdIncome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmdIncome.Text = "INCOME ACCOUNTS";
			this.cmdIncome.Size = new System.Drawing.Size(257, 23);
			this.cmdIncome.Location = new System.Drawing.Point(13, 122);
			this.cmdIncome.TabIndex = 6;
			this.cmdIncome.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.cmdIncome.BackColor = System.Drawing.SystemColors.Control;
			this.cmdIncome.CausesValidation = true;
			this.cmdIncome.Enabled = true;
			this.cmdIncome.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdIncome.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdIncome.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdIncome.TabStop = true;
			this.cmdIncome.Name = "cmdIncome";
			this.lstYear.Size = new System.Drawing.Size(56, 21);
			this.lstYear.Location = new System.Drawing.Point(161, 95);
			this.lstYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstYear.TabIndex = 5;
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
			this.lstMonth.Size = new System.Drawing.Size(81, 21);
			this.lstMonth.Location = new System.Drawing.Point(77, 95);
			this.lstMonth.Items.AddRange(new object[] {
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
			this.lstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMonth.TabIndex = 4;
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
			this._Label3_1.Text = "Department:";
			this._Label3_1.Size = new System.Drawing.Size(60, 14);
			this._Label3_1.Location = new System.Drawing.Point(5, 66);
			this._Label3_1.TabIndex = 28;
			this._Label3_1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label3_1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label3_1.BackColor = System.Drawing.SystemColors.Control;
			this._Label3_1.Enabled = true;
			this._Label3_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label3_1.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label3_1.UseMnemonic = true;
			this._Label3_1.Visible = true;
			this._Label3_1.AutoSize = false;
			this._Label3_1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label3_1.Name = "_Label3_1";
			this._Label3_0.Text = "Company:";
			this._Label3_0.Size = new System.Drawing.Size(52, 14);
			this._Label3_0.Location = new System.Drawing.Point(5, 44);
			this._Label3_0.TabIndex = 27;
			this._Label3_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Label3_0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this._Label3_0.BackColor = System.Drawing.SystemColors.Control;
			this._Label3_0.Enabled = true;
			this._Label3_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Label3_0.UseMnemonic = true;
			this._Label3_0.Visible = true;
			this._Label3_0.AutoSize = false;
			this._Label3_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._Label3_0.Name = "_Label3_0";
			this.Label2.Text = "Month/Year:";
			this.Label2.Size = new System.Drawing.Size(66, 13);
			this.Label2.Location = new System.Drawing.Point(10, 99);
			this.Label2.TabIndex = 21;
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
			this.Label1.Text = "Statement Name:";
			this.Label1.Size = new System.Drawing.Size(88, 15);
			this.Label1.Location = new System.Drawing.Point(9, 10);
			this.Label1.TabIndex = 20;
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
			this.Controls.Add(lstStatement);
			this.Controls.Add(chkConsolidated);
			this.Controls.Add(_Cmd_4);
			this.Controls.Add(_Cmd_3);
			this.Controls.Add(_Cmd_2);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(chkZero);
			this.Controls.Add(chkYTD);
			this.Controls.Add(chkPercent);
			this.Controls.Add(chkPrior);
			this.Controls.Add(Frame2);
			this.Controls.Add(Frame1);
			this.Controls.Add(cmdExpense);
			this.Controls.Add(cmdIncome);
			this.Controls.Add(lstYear);
			this.Controls.Add(lstMonth);
			this.Controls.Add(_Label3_1);
			this.Controls.Add(_Label3_0);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Frame2.Controls.Add(_optCurrent_2);
			this.Frame2.Controls.Add(_optCurrent_1);
			this.Frame2.Controls.Add(_optCurrent_0);
			this.Frame1.Controls.Add(_optDetail_2);
			this.Frame1.Controls.Add(_optDetail_1);
			this.Frame1.Controls.Add(_optDetail_0);
			this.Cmd.SetIndex(_Cmd_4, Convert.ToInt16(4));
			this.Cmd.SetIndex(_Cmd_3, Convert.ToInt16(3));
			this.Cmd.SetIndex(_Cmd_2, Convert.ToInt16(2));
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			this.Label3.SetIndex(_Label3_1, Convert.ToInt16(1));
			this.Label3.SetIndex(_Label3_0, Convert.ToInt16(0));
			this.optCurrent.SetIndex(_optCurrent_2, Convert.ToInt16(2));
			this.optCurrent.SetIndex(_optCurrent_1, Convert.ToInt16(1));
			this.optCurrent.SetIndex(_optCurrent_0, Convert.ToInt16(0));
			this.optDetail.SetIndex(_optDetail_2, Convert.ToInt16(2));
			this.optDetail.SetIndex(_optDetail_1, Convert.ToInt16(1));
			this.optDetail.SetIndex(_optDetail_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.optDetail).EndInit();
			((System.ComponentModel.ISupportInitialize)this.optCurrent).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Label3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstDepartments).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lstCompanies).EndInit();
			this.Frame2.ResumeLayout(false);
			this.Frame1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
