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
	partial class frmSplash
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmSplash() : base()
		{
			Load += frmSplash_Load;
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
		public System.Windows.Forms.PictureBox picLogo;
		public System.Windows.Forms.Label lblLicenseTo;
		public System.Windows.Forms.Label lblProductName;
		public System.Windows.Forms.Label lblCompanyProduct;
		public System.Windows.Forms.Label lblPlatform;
		public System.Windows.Forms.Label lblVersion;
		public System.Windows.Forms.Label lblCompany;
		public System.Windows.Forms.Label lblCopyright;
		public System.Windows.Forms.GroupBox fraMainFrame;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSplash));
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.fraMainFrame = new System.Windows.Forms.GroupBox();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.lblLicenseTo = new System.Windows.Forms.Label();
			this.lblProductName = new System.Windows.Forms.Label();
			this.lblCompanyProduct = new System.Windows.Forms.Label();
			this.lblPlatform = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCompany = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.fraMainFrame.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.ClientSize = new System.Drawing.Size(497, 314);
			this.Location = new System.Drawing.Point(3, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Visible = false;
			this.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Enabled = true;
			this.KeyPreview = false;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.HelpButton = false;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmSplash";
			this.fraMainFrame.Size = new System.Drawing.Size(492, 306);
			this.fraMainFrame.Location = new System.Drawing.Point(3, -2);
			this.fraMainFrame.TabIndex = 0;
			this.fraMainFrame.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.fraMainFrame.BackColor = System.Drawing.SystemColors.Control;
			this.fraMainFrame.Enabled = true;
			this.fraMainFrame.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fraMainFrame.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fraMainFrame.Visible = true;
			this.fraMainFrame.Padding = new System.Windows.Forms.Padding(0);
			this.fraMainFrame.Name = "fraMainFrame";
			this.picLogo.Size = new System.Drawing.Size(121, 159);
			this.picLogo.Location = new System.Drawing.Point(34, 57);
			this.picLogo.Image = (System.Drawing.Image)resources.GetObject("picLogo.Image");
			this.picLogo.TabIndex = 2;
			this.picLogo.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.picLogo.Dock = System.Windows.Forms.DockStyle.None;
			this.picLogo.BackColor = System.Drawing.SystemColors.Control;
			this.picLogo.CausesValidation = true;
			this.picLogo.Enabled = true;
			this.picLogo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.picLogo.Cursor = System.Windows.Forms.Cursors.Default;
			this.picLogo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.picLogo.TabStop = true;
			this.picLogo.Visible = true;
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
			this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picLogo.Name = "picLogo";
			this.lblLicenseTo.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblLicenseTo.Text = "LicenseTo";
			this.lblLicenseTo.Size = new System.Drawing.Size(457, 17);
			this.lblLicenseTo.Location = new System.Drawing.Point(18, 20);
			this.lblLicenseTo.TabIndex = 1;
			this.lblLicenseTo.Tag = "LicenseTo";
			this.lblLicenseTo.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblLicenseTo.BackColor = System.Drawing.SystemColors.Control;
			this.lblLicenseTo.Enabled = true;
			this.lblLicenseTo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLicenseTo.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblLicenseTo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblLicenseTo.UseMnemonic = true;
			this.lblLicenseTo.Visible = true;
			this.lblLicenseTo.AutoSize = false;
			this.lblLicenseTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblLicenseTo.Name = "lblLicenseTo";
			this.lblProductName.Text = "WINGL";
			this.lblProductName.Font = new System.Drawing.Font("Arial", 29.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblProductName.Size = new System.Drawing.Size(135, 48);
			this.lblProductName.Location = new System.Drawing.Point(178, 80);
			this.lblProductName.TabIndex = 8;
			this.lblProductName.Tag = "Product";
			this.lblProductName.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblProductName.BackColor = System.Drawing.SystemColors.Control;
			this.lblProductName.Enabled = true;
			this.lblProductName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblProductName.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblProductName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblProductName.UseMnemonic = true;
			this.lblProductName.Visible = true;
			this.lblProductName.AutoSize = true;
			this.lblProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblProductName.Name = "lblProductName";
			this.lblCompanyProduct.Text = "Michael Bartmon Computer";
			this.lblCompanyProduct.Font = new System.Drawing.Font("Arial", 18f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblCompanyProduct.Size = new System.Drawing.Size(317, 29);
			this.lblCompanyProduct.Location = new System.Drawing.Point(167, 51);
			this.lblCompanyProduct.TabIndex = 7;
			this.lblCompanyProduct.Tag = "CompanyProduct";
			this.lblCompanyProduct.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblCompanyProduct.BackColor = System.Drawing.SystemColors.Control;
			this.lblCompanyProduct.Enabled = true;
			this.lblCompanyProduct.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCompanyProduct.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCompanyProduct.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCompanyProduct.UseMnemonic = true;
			this.lblCompanyProduct.Visible = true;
			this.lblCompanyProduct.AutoSize = true;
			this.lblCompanyProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblCompanyProduct.Name = "lblCompanyProduct";
			this.lblPlatform.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblPlatform.Text = "Platform";
			this.lblPlatform.Font = new System.Drawing.Font("Arial", 13.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblPlatform.Size = new System.Drawing.Size(76, 24);
			this.lblPlatform.Location = new System.Drawing.Point(391, 160);
			this.lblPlatform.TabIndex = 6;
			this.lblPlatform.Tag = "Platform";
			this.lblPlatform.BackColor = System.Drawing.SystemColors.Control;
			this.lblPlatform.Enabled = true;
			this.lblPlatform.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPlatform.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblPlatform.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblPlatform.UseMnemonic = true;
			this.lblPlatform.Visible = true;
			this.lblPlatform.AutoSize = true;
			this.lblPlatform.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblPlatform.Name = "lblPlatform";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblVersion.Text = "Version";
			this.lblVersion.Font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblVersion.Size = new System.Drawing.Size(62, 20);
			this.lblVersion.Location = new System.Drawing.Point(405, 184);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Tag = "Version";
			this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
			this.lblVersion.Enabled = true;
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblVersion.UseMnemonic = true;
			this.lblVersion.Visible = true;
			this.lblVersion.AutoSize = true;
			this.lblVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblVersion.Name = "lblVersion";
			this.lblCompany.Text = "Michael Bartmon Computer Services";
			this.lblCompany.Size = new System.Drawing.Size(179, 19);
			this.lblCompany.Location = new System.Drawing.Point(290, 240);
			this.lblCompany.TabIndex = 4;
			this.lblCompany.Tag = "Company";
			this.lblCompany.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblCompany.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblCompany.BackColor = System.Drawing.SystemColors.Control;
			this.lblCompany.Enabled = true;
			this.lblCompany.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCompany.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCompany.UseMnemonic = true;
			this.lblCompany.Visible = true;
			this.lblCompany.AutoSize = false;
			this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblCompany.Name = "lblCompany";
			this.lblCopyright.Text = "Copyright 1999";
			this.lblCopyright.Size = new System.Drawing.Size(161, 19);
			this.lblCopyright.Location = new System.Drawing.Point(290, 226);
			this.lblCopyright.TabIndex = 3;
			this.lblCopyright.Tag = "Copyright";
			this.lblCopyright.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblCopyright.BackColor = System.Drawing.SystemColors.Control;
			this.lblCopyright.Enabled = true;
			this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCopyright.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCopyright.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCopyright.UseMnemonic = true;
			this.lblCopyright.Visible = true;
			this.lblCopyright.AutoSize = false;
			this.lblCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblCopyright.Name = "lblCopyright";
			this.Controls.Add(fraMainFrame);
			this.fraMainFrame.Controls.Add(picLogo);
			this.fraMainFrame.Controls.Add(lblLicenseTo);
			this.fraMainFrame.Controls.Add(lblProductName);
			this.fraMainFrame.Controls.Add(lblCompanyProduct);
			this.fraMainFrame.Controls.Add(lblPlatform);
			this.fraMainFrame.Controls.Add(lblVersion);
			this.fraMainFrame.Controls.Add(lblCompany);
			this.fraMainFrame.Controls.Add(lblCopyright);
			this.fraMainFrame.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
