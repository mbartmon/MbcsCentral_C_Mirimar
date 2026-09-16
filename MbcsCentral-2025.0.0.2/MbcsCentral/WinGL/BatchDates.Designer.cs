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
	partial class BatchDates
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public BatchDates() : base()
		{
			FormClosed += BatchDates_FormClosed;
			Load += BatchDates_Load;
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
		public System.Windows.Forms.ComboBox lstYear;
		public System.Windows.Forms.Button _Cmd_1;
		public System.Windows.Forms.Button _Cmd_0;
		public System.Windows.Forms.ComboBox lstMonth;
		public AxMSComCtl2.AxDTPicker dtStart;
		public AxMSComCtl2.AxDTPicker dtEnd;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BatchDates));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.lstYear = new System.Windows.Forms.ComboBox();
			this._Cmd_1 = new System.Windows.Forms.Button();
			this._Cmd_0 = new System.Windows.Forms.Button();
			this.lstMonth = new System.Windows.Forms.ComboBox();
			this.dtStart = new AxMSComCtl2.AxDTPicker();
			this.dtEnd = new AxMSComCtl2.AxDTPicker();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Cmd = new Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components);
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			((System.ComponentModel.ISupportInitialize)this.dtStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Cmd).BeginInit();
			this.Text = "Adjust Batch Reports Dates";
			this.ClientSize = new System.Drawing.Size(296, 160);
			this.Location = new System.Drawing.Point(4, 23);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
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
			this.Name = "BatchDates";
			this.lstYear.Size = new System.Drawing.Size(93, 21);
			this.lstYear.Location = new System.Drawing.Point(192, 74);
			this.lstYear.Items.AddRange(new object[] {
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
			this.lstYear.TabIndex = 8;
			this.lstYear.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lstYear.BackColor = System.Drawing.SystemColors.Window;
			this.lstYear.CausesValidation = true;
			this.lstYear.Enabled = true;
			this.lstYear.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstYear.IntegralHeight = true;
			this.lstYear.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstYear.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstYear.Sorted = false;
			this.lstYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstYear.TabStop = true;
			this.lstYear.Visible = true;
			this.lstYear.Name = "lstYear";
			this._Cmd_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CancelButton = this._Cmd_1;
			this._Cmd_1.Text = "CANCEL";
			this._Cmd_1.Size = new System.Drawing.Size(81, 33);
			this._Cmd_1.Location = new System.Drawing.Point(102, 121);
			this._Cmd_1.TabIndex = 7;
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
			this.AcceptButton = this._Cmd_0;
			this._Cmd_0.Size = new System.Drawing.Size(81, 33);
			this._Cmd_0.Location = new System.Drawing.Point(13, 121);
			this._Cmd_0.TabIndex = 6;
			this._Cmd_0.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this._Cmd_0.BackColor = System.Drawing.SystemColors.Control;
			this._Cmd_0.CausesValidation = true;
			this._Cmd_0.Enabled = true;
			this._Cmd_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Cmd_0.Cursor = System.Windows.Forms.Cursors.Default;
			this._Cmd_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Cmd_0.TabStop = true;
			this._Cmd_0.Name = "_Cmd_0";
			this.lstMonth.Size = new System.Drawing.Size(93, 21);
			this.lstMonth.Location = new System.Drawing.Point(93, 74);
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
			this.lstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.lstMonth.TabStop = true;
			this.lstMonth.Visible = true;
			this.lstMonth.Name = "lstMonth";
			dtStart.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtStart.OcxState");
			this.dtStart.Size = new System.Drawing.Size(94, 24);
			this.dtStart.Location = new System.Drawing.Point(93, 11);
			this.dtStart.TabIndex = 3;
			this.dtStart.Name = "dtStart";
			dtEnd.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("dtEnd.OcxState");
			this.dtEnd.Size = new System.Drawing.Size(94, 24);
			this.dtEnd.Location = new System.Drawing.Point(93, 41);
			this.dtEnd.TabIndex = 4;
			this.dtEnd.Name = "dtEnd";
			this.Label3.Text = "Month/Year:";
			this.Label3.Size = new System.Drawing.Size(60, 17);
			this.Label3.Location = new System.Drawing.Point(16, 76);
			this.Label3.TabIndex = 2;
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
			this.Label2.Text = "Ending Date:";
			this.Label2.Size = new System.Drawing.Size(81, 17);
			this.Label2.Location = new System.Drawing.Point(16, 44);
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
			this.Label1.Text = "Starting Date:";
			this.Label1.Size = new System.Drawing.Size(71, 17);
			this.Label1.Location = new System.Drawing.Point(16, 14);
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
			this.Controls.Add(lstYear);
			this.Controls.Add(_Cmd_1);
			this.Controls.Add(_Cmd_0);
			this.Controls.Add(lstMonth);
			this.Controls.Add(dtStart);
			this.Controls.Add(dtEnd);
			this.Controls.Add(Label3);
			this.Controls.Add(Label2);
			this.Controls.Add(Label1);
			this.Cmd.SetIndex(_Cmd_1, Convert.ToInt16(1));
			this.Cmd.SetIndex(_Cmd_0, Convert.ToInt16(0));
			((System.ComponentModel.ISupportInitialize)this.Cmd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtEnd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dtStart).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
