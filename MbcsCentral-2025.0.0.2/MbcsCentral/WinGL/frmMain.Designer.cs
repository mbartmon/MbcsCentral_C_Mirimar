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
	partial class frmMain
	{
		#region "Windows Form Designer generated code "
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmMain() : base()
		{
			FormClosed += frmMain_FormClosed;
			Resize += frmMain_Resize;
			FormClosing += frmMain_FormClosing;
			Load += frmMain_Load;
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
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_copy;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_copy {
			get { return withEventsField_mnudatadata_copy; }
			set {
				if (withEventsField_mnudatadata_copy != null) {
					withEventsField_mnudatadata_copy.Click -= mnudatadata_copy_Click;
				}
				withEventsField_mnudatadata_copy = value;
				if (withEventsField_mnudatadata_copy != null) {
					withEventsField_mnudatadata_copy.Click += mnudatadata_copy_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_compact;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_compact {
			get { return withEventsField_mnudatadata_compact; }
			set {
				if (withEventsField_mnudatadata_compact != null) {
					withEventsField_mnudatadata_compact.Click -= mnudatadata_compact_Click;
				}
				withEventsField_mnudatadata_compact = value;
				if (withEventsField_mnudatadata_compact != null) {
					withEventsField_mnudatadata_compact.Click += mnudatadata_compact_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_repair;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_repair {
			get { return withEventsField_mnudatadata_repair; }
			set {
				if (withEventsField_mnudatadata_repair != null) {
					withEventsField_mnudatadata_repair.Click -= mnudatadata_repair_Click;
				}
				withEventsField_mnudatadata_repair = value;
				if (withEventsField_mnudatadata_repair != null) {
					withEventsField_mnudatadata_repair.Click += mnudatadata_repair_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnudataBar0;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_error;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_error {
			get { return withEventsField_mnudatadata_error; }
			set {
				if (withEventsField_mnudatadata_error != null) {
					withEventsField_mnudatadata_error.Click -= mnudatadata_error_Click;
				}
				withEventsField_mnudatadata_error = value;
				if (withEventsField_mnudatadata_error != null) {
					withEventsField_mnudatadata_error.Click += mnudatadata_error_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnudataBar1;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_security;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_security {
			get { return withEventsField_mnudatadata_security; }
			set {
				if (withEventsField_mnudatadata_security != null) {
					withEventsField_mnudatadata_security.Click -= mnudatadata_security_Click;
				}
				withEventsField_mnudatadata_security = value;
				if (withEventsField_mnudatadata_security != null) {
					withEventsField_mnudatadata_security.Click += mnudatadata_security_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnudataBar2;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnudatadata_exit;
		public System.Windows.Forms.ToolStripMenuItem mnudatadata_exit {
			get { return withEventsField_mnudatadata_exit; }
			set {
				if (withEventsField_mnudatadata_exit != null) {
					withEventsField_mnudatadata_exit.Click -= mnudatadata_exit_Click;
				}
				withEventsField_mnudatadata_exit = value;
				if (withEventsField_mnudatadata_exit != null) {
					withEventsField_mnudatadata_exit.Click += mnudatadata_exit_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnudata;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuEditCut;
		public System.Windows.Forms.ToolStripMenuItem mnuEditCut {
			get { return withEventsField_mnuEditCut; }
			set {
				if (withEventsField_mnuEditCut != null) {
					withEventsField_mnuEditCut.Click -= mnuEditCut_Click;
				}
				withEventsField_mnuEditCut = value;
				if (withEventsField_mnuEditCut != null) {
					withEventsField_mnuEditCut.Click += mnuEditCut_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuEditCopy;
		public System.Windows.Forms.ToolStripMenuItem mnuEditCopy {
			get { return withEventsField_mnuEditCopy; }
			set {
				if (withEventsField_mnuEditCopy != null) {
					withEventsField_mnuEditCopy.Click -= mnuEditCopy_Click;
				}
				withEventsField_mnuEditCopy = value;
				if (withEventsField_mnuEditCopy != null) {
					withEventsField_mnuEditCopy.Click += mnuEditCopy_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuEditPaste;
		public System.Windows.Forms.ToolStripMenuItem mnuEditPaste {
			get { return withEventsField_mnuEditPaste; }
			set {
				if (withEventsField_mnuEditPaste != null) {
					withEventsField_mnuEditPaste.Click -= mnuEditPaste_Click;
				}
				withEventsField_mnuEditPaste = value;
				if (withEventsField_mnuEditPaste != null) {
					withEventsField_mnuEditPaste.Click += mnuEditPaste_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuViewToolbar;
		public System.Windows.Forms.ToolStripMenuItem mnuViewToolbar {
			get { return withEventsField_mnuViewToolbar; }
			set {
				if (withEventsField_mnuViewToolbar != null) {
					withEventsField_mnuViewToolbar.Click -= mnuViewToolbar_Click;
				}
				withEventsField_mnuViewToolbar = value;
				if (withEventsField_mnuViewToolbar != null) {
					withEventsField_mnuViewToolbar.Click += mnuViewToolbar_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuViewStatusBar;
		public System.Windows.Forms.ToolStripMenuItem mnuViewStatusBar {
			get { return withEventsField_mnuViewStatusBar; }
			set {
				if (withEventsField_mnuViewStatusBar != null) {
					withEventsField_mnuViewStatusBar.Click -= mnuViewStatusBar_Click;
				}
				withEventsField_mnuViewStatusBar = value;
				if (withEventsField_mnuViewStatusBar != null) {
					withEventsField_mnuViewStatusBar.Click += mnuViewStatusBar_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnuViewBar0;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuViewOptions;
		public System.Windows.Forms.ToolStripMenuItem mnuViewOptions {
			get { return withEventsField_mnuViewOptions; }
			set {
				if (withEventsField_mnuViewOptions != null) {
					withEventsField_mnuViewOptions.Click -= mnuViewOptions_Click;
				}
				withEventsField_mnuViewOptions = value;
				if (withEventsField_mnuViewOptions != null) {
					withEventsField_mnuViewOptions.Click += mnuViewOptions_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_vendors;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_vendors {
			get { return withEventsField_mnutaskstasks_vendors; }
			set {
				if (withEventsField_mnutaskstasks_vendors != null) {
					withEventsField_mnutaskstasks_vendors.Click -= mnutaskstasks_vendors_Click;
				}
				withEventsField_mnutaskstasks_vendors = value;
				if (withEventsField_mnutaskstasks_vendors != null) {
					withEventsField_mnutaskstasks_vendors.Click += mnutaskstasks_vendors_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_categories;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_categories {
			get { return withEventsField_mnutaskstasks_categories; }
			set {
				if (withEventsField_mnutaskstasks_categories != null) {
					withEventsField_mnutaskstasks_categories.Click -= mnutaskstasks_categories_Click;
				}
				withEventsField_mnutaskstasks_categories = value;
				if (withEventsField_mnutaskstasks_categories != null) {
					withEventsField_mnutaskstasks_categories.Click += mnutaskstasks_categories_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_company;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_company {
			get { return withEventsField_mnutaskstasks_company; }
			set {
				if (withEventsField_mnutaskstasks_company != null) {
					withEventsField_mnutaskstasks_company.Click -= mnutaskstasks_company_Click;
				}
				withEventsField_mnutaskstasks_company = value;
				if (withEventsField_mnutaskstasks_company != null) {
					withEventsField_mnutaskstasks_company.Click += mnutaskstasks_company_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_journalnames;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_journalnames {
			get { return withEventsField_mnutaskstasks_journalnames; }
			set {
				if (withEventsField_mnutaskstasks_journalnames != null) {
					withEventsField_mnutaskstasks_journalnames.Click -= mnutaskstasks_journalnames_Click;
				}
				withEventsField_mnutaskstasks_journalnames = value;
				if (withEventsField_mnutaskstasks_journalnames != null) {
					withEventsField_mnutaskstasks_journalnames.Click += mnutaskstasks_journalnames_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_recurringinvoices;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_recurringinvoices {
			get { return withEventsField_mnutaskstasks_recurringinvoices; }
			set {
				if (withEventsField_mnutaskstasks_recurringinvoices != null) {
					withEventsField_mnutaskstasks_recurringinvoices.Click -= mnutaskstasks_recurringinvoices_Click;
				}
				withEventsField_mnutaskstasks_recurringinvoices = value;
				if (withEventsField_mnutaskstasks_recurringinvoices != null) {
					withEventsField_mnutaskstasks_recurringinvoices.Click += mnutaskstasks_recurringinvoices_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnutasksBar2;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_invoices;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_invoices {
			get { return withEventsField_mnutaskstasks_invoices; }
			set {
				if (withEventsField_mnutaskstasks_invoices != null) {
					withEventsField_mnutaskstasks_invoices.Click -= mnutaskstasks_invoices_Click;
				}
				withEventsField_mnutaskstasks_invoices = value;
				if (withEventsField_mnutaskstasks_invoices != null) {
					withEventsField_mnutaskstasks_invoices.Click += mnutaskstasks_invoices_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_invoiceselection;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_invoiceselection {
			get { return withEventsField_mnutaskstasks_invoiceselection; }
			set {
				if (withEventsField_mnutaskstasks_invoiceselection != null) {
					withEventsField_mnutaskstasks_invoiceselection.Click -= mnutaskstasks_invoiceselection_Click;
				}
				withEventsField_mnutaskstasks_invoiceselection = value;
				if (withEventsField_mnutaskstasks_invoiceselection != null) {
					withEventsField_mnutaskstasks_invoiceselection.Click += mnutaskstasks_invoiceselection_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_check_laser;
		public System.Windows.Forms.ToolStripMenuItem check_laser {
			get { return withEventsField_check_laser; }
			set {
				if (withEventsField_check_laser != null) {
					withEventsField_check_laser.Click -= check_laser_Click;
				}
				withEventsField_check_laser = value;
				if (withEventsField_check_laser != null) {
					withEventsField_check_laser.Click += check_laser_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_check_post;
		public System.Windows.Forms.ToolStripMenuItem check_post {
			get { return withEventsField_check_post; }
			set {
				if (withEventsField_check_post != null) {
					withEventsField_check_post.Click -= check_post_Click;
				}
				withEventsField_check_post = value;
				if (withEventsField_check_post != null) {
					withEventsField_check_post.Click += check_post_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_check_recon;
		public System.Windows.Forms.ToolStripMenuItem check_recon {
			get { return withEventsField_check_recon; }
			set {
				if (withEventsField_check_recon != null) {
					withEventsField_check_recon.Click -= check_recon_Click;
				}
				withEventsField_check_recon = value;
				if (withEventsField_check_recon != null) {
					withEventsField_check_recon.Click += check_recon_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem check;
		public System.Windows.Forms.ToolStripSeparator mnutasksBar0;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_manual;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_manual {
			get { return withEventsField_mnutaskstasks_manual; }
			set {
				if (withEventsField_mnutaskstasks_manual != null) {
					withEventsField_mnutaskstasks_manual.Click -= mnutaskstasks_manual_Click;
				}
				withEventsField_mnutaskstasks_manual = value;
				if (withEventsField_mnutaskstasks_manual != null) {
					withEventsField_mnutaskstasks_manual.Click += mnutaskstasks_manual_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_batch;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_batch {
			get { return withEventsField_mnutaskstasks_batch; }
			set {
				if (withEventsField_mnutaskstasks_batch != null) {
					withEventsField_mnutaskstasks_batch.Click -= mnutaskstasks_batch_Click;
				}
				withEventsField_mnutaskstasks_batch = value;
				if (withEventsField_mnutaskstasks_batch != null) {
					withEventsField_mnutaskstasks_batch.Click += mnutaskstasks_batch_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_receivable;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_receivable {
			get { return withEventsField_mnutaskstasks_receivable; }
			set {
				if (withEventsField_mnutaskstasks_receivable != null) {
					withEventsField_mnutaskstasks_receivable.Click -= mnutaskstasks_receivable_Click;
				}
				withEventsField_mnutaskstasks_receivable = value;
				if (withEventsField_mnutaskstasks_receivable != null) {
					withEventsField_mnutaskstasks_receivable.Click += mnutaskstasks_receivable_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutaskstasks_payable;
		public System.Windows.Forms.ToolStripMenuItem mnutaskstasks_payable {
			get { return withEventsField_mnutaskstasks_payable; }
			set {
				if (withEventsField_mnutaskstasks_payable != null) {
					withEventsField_mnutaskstasks_payable.Click -= mnutaskstasks_payable_Click;
				}
				withEventsField_mnutaskstasks_payable = value;
				if (withEventsField_mnutaskstasks_payable != null) {
					withEventsField_mnutaskstasks_payable.Click += mnutaskstasks_payable_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnutasksBar1;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_cmp;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_cmp {
			get { return withEventsField_disp_balance_cmp; }
			set {
				if (withEventsField_disp_balance_cmp != null) {
					withEventsField_disp_balance_cmp.Click -= disp_balance_cmp_Click;
				}
				withEventsField_disp_balance_cmp = value;
				if (withEventsField_disp_balance_cmp != null) {
					withEventsField_disp_balance_cmp.Click += disp_balance_cmp_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_cmpdpt;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_cmpdpt {
			get { return withEventsField_disp_balance_cmpdpt; }
			set {
				if (withEventsField_disp_balance_cmpdpt != null) {
					withEventsField_disp_balance_cmpdpt.Click -= disp_balance_cmpdpt_Click;
				}
				withEventsField_disp_balance_cmpdpt = value;
				if (withEventsField_disp_balance_cmpdpt != null) {
					withEventsField_disp_balance_cmpdpt.Click += disp_balance_cmpdpt_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_acct;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_acct {
			get { return withEventsField_disp_balance_acct; }
			set {
				if (withEventsField_disp_balance_acct != null) {
					withEventsField_disp_balance_acct.Click -= disp_balance_acct_Click;
				}
				withEventsField_disp_balance_acct = value;
				if (withEventsField_disp_balance_acct != null) {
					withEventsField_disp_balance_acct.Click += disp_balance_acct_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_acct_2;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_acct_2 {
			get { return withEventsField_disp_balance_acct_2; }
			set {
				if (withEventsField_disp_balance_acct_2 != null) {
					withEventsField_disp_balance_acct_2.Click -= disp_balance_acct_2_Click;
				}
				withEventsField_disp_balance_acct_2 = value;
				if (withEventsField_disp_balance_acct_2 != null) {
					withEventsField_disp_balance_acct_2.Click += disp_balance_acct_2_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem disp_balances;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_budgets;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_budgets {
			get { return withEventsField_disp_balance_budgets; }
			set {
				if (withEventsField_disp_balance_budgets != null) {
					withEventsField_disp_balance_budgets.Click -= disp_balance_budgets_Click;
				}
				withEventsField_disp_balance_budgets = value;
				if (withEventsField_disp_balance_budgets != null) {
					withEventsField_disp_balance_budgets.Click += disp_balance_budgets_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_disp_balance_budmaint;
		public System.Windows.Forms.ToolStripMenuItem disp_balance_budmaint {
			get { return withEventsField_disp_balance_budmaint; }
			set {
				if (withEventsField_disp_balance_budmaint != null) {
					withEventsField_disp_balance_budmaint.Click -= disp_balance_budmaint_Click;
				}
				withEventsField_disp_balance_budmaint = value;
				if (withEventsField_disp_balance_budmaint != null) {
					withEventsField_disp_balance_budmaint.Click += disp_balance_budmaint_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnutasksBar3;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutasksmaint_company;
		public System.Windows.Forms.ToolStripMenuItem mnutasksmaint_company {
			get { return withEventsField_mnutasksmaint_company; }
			set {
				if (withEventsField_mnutasksmaint_company != null) {
					withEventsField_mnutasksmaint_company.Click -= mnutasksmaint_company_Click;
				}
				withEventsField_mnutasksmaint_company = value;
				if (withEventsField_mnutasksmaint_company != null) {
					withEventsField_mnutasksmaint_company.Click += mnutasksmaint_company_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnutasksmaint_department;
		public System.Windows.Forms.ToolStripMenuItem mnutasksmaint_department {
			get { return withEventsField_mnutasksmaint_department; }
			set {
				if (withEventsField_mnutasksmaint_department != null) {
					withEventsField_mnutasksmaint_department.Click -= mnutasksmaint_department_Click;
				}
				withEventsField_mnutasksmaint_department = value;
				if (withEventsField_mnutasksmaint_department != null) {
					withEventsField_mnutasksmaint_department.Click += mnutasksmaint_department_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnt_balfwddos;
		public System.Windows.Forms.ToolStripMenuItem mnt_balfwddos {
			get { return withEventsField_mnt_balfwddos; }
			set {
				if (withEventsField_mnt_balfwddos != null) {
					withEventsField_mnt_balfwddos.Click -= mnt_balfwddos_Click;
				}
				withEventsField_mnt_balfwddos = value;
				if (withEventsField_mnt_balfwddos != null) {
					withEventsField_mnt_balfwddos.Click += mnt_balfwddos_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnt_jrndesign;
		public System.Windows.Forms.ToolStripMenuItem mnt_jrndesign {
			get { return withEventsField_mnt_jrndesign; }
			set {
				if (withEventsField_mnt_jrndesign != null) {
					withEventsField_mnt_jrndesign.Click -= mnt_jrndesign_Click;
				}
				withEventsField_mnt_jrndesign = value;
				if (withEventsField_mnt_jrndesign != null) {
					withEventsField_mnt_jrndesign.Click += mnt_jrndesign_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnt_correct;
		public System.Windows.Forms.ToolStripMenuItem mnt_correct {
			get { return withEventsField_mnt_correct; }
			set {
				if (withEventsField_mnt_correct != null) {
					withEventsField_mnt_correct.Click -= mnt_correct_Click;
				}
				withEventsField_mnt_correct = value;
				if (withEventsField_mnt_correct != null) {
					withEventsField_mnt_correct.Click += mnt_correct_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnutasksmaint;
		public System.Windows.Forms.ToolStripSeparator mnutasksBar4;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_yearend_vendor;
		public System.Windows.Forms.ToolStripMenuItem yearend_vendor {
			get { return withEventsField_yearend_vendor; }
			set {
				if (withEventsField_yearend_vendor != null) {
					withEventsField_yearend_vendor.Click -= yearend_vendor_Click;
				}
				withEventsField_yearend_vendor = value;
				if (withEventsField_yearend_vendor != null) {
					withEventsField_yearend_vendor.Click += yearend_vendor_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_yearend_gl;
		public System.Windows.Forms.ToolStripMenuItem yearend_gl {
			get { return withEventsField_yearend_gl; }
			set {
				if (withEventsField_yearend_gl != null) {
					withEventsField_yearend_gl.Click -= yearend_gl_Click;
				}
				withEventsField_yearend_gl = value;
				if (withEventsField_yearend_gl != null) {
					withEventsField_yearend_gl.Click += yearend_gl_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_yearend_balances;
		public System.Windows.Forms.ToolStripMenuItem yearend_balances {
			get { return withEventsField_yearend_balances; }
			set {
				if (withEventsField_yearend_balances != null) {
					withEventsField_yearend_balances.Click -= yearend_balances_Click;
				}
				withEventsField_yearend_balances = value;
				if (withEventsField_yearend_balances != null) {
					withEventsField_yearend_balances.Click += yearend_balances_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem yearend;
		public System.Windows.Forms.ToolStripMenuItem mnutasks;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuWindowCascade;
		public System.Windows.Forms.ToolStripMenuItem mnuWindowCascade {
			get { return withEventsField_mnuWindowCascade; }
			set {
				if (withEventsField_mnuWindowCascade != null) {
					withEventsField_mnuWindowCascade.Click -= mnuWindowCascade_Click;
				}
				withEventsField_mnuWindowCascade = value;
				if (withEventsField_mnuWindowCascade != null) {
					withEventsField_mnuWindowCascade.Click += mnuWindowCascade_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuWindowTileHorizontal;
		public System.Windows.Forms.ToolStripMenuItem mnuWindowTileHorizontal {
			get { return withEventsField_mnuWindowTileHorizontal; }
			set {
				if (withEventsField_mnuWindowTileHorizontal != null) {
					withEventsField_mnuWindowTileHorizontal.Click -= mnuWindowTileHorizontal_Click;
				}
				withEventsField_mnuWindowTileHorizontal = value;
				if (withEventsField_mnuWindowTileHorizontal != null) {
					withEventsField_mnuWindowTileHorizontal.Click += mnuWindowTileHorizontal_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuWindowTileVertical;
		public System.Windows.Forms.ToolStripMenuItem mnuWindowTileVertical {
			get { return withEventsField_mnuWindowTileVertical; }
			set {
				if (withEventsField_mnuWindowTileVertical != null) {
					withEventsField_mnuWindowTileVertical.Click -= mnuWindowTileVertical_Click;
				}
				withEventsField_mnuWindowTileVertical = value;
				if (withEventsField_mnuWindowTileVertical != null) {
					withEventsField_mnuWindowTileVertical.Click += mnuWindowTileVertical_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnuWindow;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuHelpContents;
		public System.Windows.Forms.ToolStripMenuItem mnuHelpContents {
			get { return withEventsField_mnuHelpContents; }
			set {
				if (withEventsField_mnuHelpContents != null) {
					withEventsField_mnuHelpContents.Click -= mnuHelpContents_Click;
				}
				withEventsField_mnuHelpContents = value;
				if (withEventsField_mnuHelpContents != null) {
					withEventsField_mnuHelpContents.Click += mnuHelpContents_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuHelpSearchForHelpOn;
		public System.Windows.Forms.ToolStripMenuItem mnuHelpSearchForHelpOn {
			get { return withEventsField_mnuHelpSearchForHelpOn; }
			set {
				if (withEventsField_mnuHelpSearchForHelpOn != null) {
					withEventsField_mnuHelpSearchForHelpOn.Click -= mnuHelpSearchForHelpOn_Click;
				}
				withEventsField_mnuHelpSearchForHelpOn = value;
				if (withEventsField_mnuHelpSearchForHelpOn != null) {
					withEventsField_mnuHelpSearchForHelpOn.Click += mnuHelpSearchForHelpOn_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripSeparator mnuHelpBar0;
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuHelpWeb;
		public System.Windows.Forms.ToolStripMenuItem mnuHelpWeb {
			get { return withEventsField_mnuHelpWeb; }
			set {
				if (withEventsField_mnuHelpWeb != null) {
					withEventsField_mnuHelpWeb.Click -= mnuHelpWeb_Click;
				}
				withEventsField_mnuHelpWeb = value;
				if (withEventsField_mnuHelpWeb != null) {
					withEventsField_mnuHelpWeb.Click += mnuHelpWeb_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuHelpUpdates;
		public System.Windows.Forms.ToolStripMenuItem mnuHelpUpdates {
			get { return withEventsField_mnuHelpUpdates; }
			set {
				if (withEventsField_mnuHelpUpdates != null) {
					withEventsField_mnuHelpUpdates.Click -= mnuHelpUpdates_Click;
				}
				withEventsField_mnuHelpUpdates = value;
				if (withEventsField_mnuHelpUpdates != null) {
					withEventsField_mnuHelpUpdates.Click += mnuHelpUpdates_Click;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField_mnuHelpAbout;
		public System.Windows.Forms.ToolStripMenuItem mnuHelpAbout {
			get { return withEventsField_mnuHelpAbout; }
			set {
				if (withEventsField_mnuHelpAbout != null) {
					withEventsField_mnuHelpAbout.Click -= mnuHelpAbout_Click;
				}
				withEventsField_mnuHelpAbout = value;
				if (withEventsField_mnuHelpAbout != null) {
					withEventsField_mnuHelpAbout.Click += mnuHelpAbout_Click;
				}
			}
		}
		public System.Windows.Forms.ToolStripMenuItem mnuHelp;
		public System.Windows.Forms.ToolStripMenuItem popups_vendors_categories;
		public System.Windows.Forms.ToolStripMenuItem popups_vendors;
		public System.Windows.Forms.ToolStripMenuItem popups;
		public System.Windows.Forms.MenuStrip MainMenu1;
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button1;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button1 {
			get { return withEventsField__tbToolBar_Button1; }
			set {
				if (withEventsField__tbToolBar_Button1 != null) {
					withEventsField__tbToolBar_Button1.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button1 = value;
				if (withEventsField__tbToolBar_Button1 != null) {
					withEventsField__tbToolBar_Button1.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button2;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button2 {
			get { return withEventsField__tbToolBar_Button2; }
			set {
				if (withEventsField__tbToolBar_Button2 != null) {
					withEventsField__tbToolBar_Button2.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button2 = value;
				if (withEventsField__tbToolBar_Button2 != null) {
					withEventsField__tbToolBar_Button2.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button3;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button3 {
			get { return withEventsField__tbToolBar_Button3; }
			set {
				if (withEventsField__tbToolBar_Button3 != null) {
					withEventsField__tbToolBar_Button3.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button3 = value;
				if (withEventsField__tbToolBar_Button3 != null) {
					withEventsField__tbToolBar_Button3.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripSeparator withEventsField__tbToolBar_Button4;
		public System.Windows.Forms.ToolStripSeparator _tbToolBar_Button4 {
			get { return withEventsField__tbToolBar_Button4; }
			set {
				if (withEventsField__tbToolBar_Button4 != null) {
					withEventsField__tbToolBar_Button4.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button4 = value;
				if (withEventsField__tbToolBar_Button4 != null) {
					withEventsField__tbToolBar_Button4.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button5;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button5 {
			get { return withEventsField__tbToolBar_Button5; }
			set {
				if (withEventsField__tbToolBar_Button5 != null) {
					withEventsField__tbToolBar_Button5.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button5 = value;
				if (withEventsField__tbToolBar_Button5 != null) {
					withEventsField__tbToolBar_Button5.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripSeparator withEventsField__tbToolBar_Button6;
		public System.Windows.Forms.ToolStripSeparator _tbToolBar_Button6 {
			get { return withEventsField__tbToolBar_Button6; }
			set {
				if (withEventsField__tbToolBar_Button6 != null) {
					withEventsField__tbToolBar_Button6.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button6 = value;
				if (withEventsField__tbToolBar_Button6 != null) {
					withEventsField__tbToolBar_Button6.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button7;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button7 {
			get { return withEventsField__tbToolBar_Button7; }
			set {
				if (withEventsField__tbToolBar_Button7 != null) {
					withEventsField__tbToolBar_Button7.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button7 = value;
				if (withEventsField__tbToolBar_Button7 != null) {
					withEventsField__tbToolBar_Button7.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripSeparator withEventsField__tbToolBar_Button8;
		public System.Windows.Forms.ToolStripSeparator _tbToolBar_Button8 {
			get { return withEventsField__tbToolBar_Button8; }
			set {
				if (withEventsField__tbToolBar_Button8 != null) {
					withEventsField__tbToolBar_Button8.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button8 = value;
				if (withEventsField__tbToolBar_Button8 != null) {
					withEventsField__tbToolBar_Button8.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu1;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu1 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu1; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu1 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu1.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu1 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu1 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu1.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu2;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu2 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu2; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu2 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu2.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu2 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu2 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu2.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu3;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu3 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu3; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu3 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu3.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu3 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu3 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu3.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu4;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu4 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu4; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu4 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu4.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu4 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu4 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu4.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu5;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu5 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu5; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu5 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu5.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu5 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu5 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu5.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu6;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu6 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu6; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu6 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu6.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu6 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu6 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu6.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu7;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu7 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu7; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu7 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu7.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu7 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu7 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu7.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu8;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu8 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu8; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu8 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu8.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu8 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu8 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu8.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu9;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu9 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu9; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu9 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu9.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu9 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu9 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu9.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu10;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu10 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu10; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu10 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu10.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu10 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu10 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu10.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu11;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu11 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu11; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu11 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu11.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu11 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu11 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu11.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu12;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu12 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu12; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu12 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu12.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu12 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu12 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu12.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu13;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu13 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu13; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu13 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu13.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu13 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu13 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu13.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu14;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu14 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu14; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu14 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu14.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu14 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu14 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu14.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu15;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu15 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu15; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu15 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu15.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu15 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu15 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu15.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu16;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu16 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu16; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu16 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu16.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu16 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu16 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu16.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu17;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu17 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu17; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu17 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu17.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu17 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu17 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu17.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu18;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu18 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu18; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu18 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu18.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu18 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu18 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu18.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu19;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu19 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu19; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu19 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu19.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu19 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu19 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu19.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripMenuItem withEventsField__tbToolBar_Button9_ButtonMenu20;
		public System.Windows.Forms.ToolStripMenuItem _tbToolBar_Button9_ButtonMenu20 {
			get { return withEventsField__tbToolBar_Button9_ButtonMenu20; }
			set {
				if (withEventsField__tbToolBar_Button9_ButtonMenu20 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu20.Click -= tbToolBar_ButtonMenuClick;
				}
				withEventsField__tbToolBar_Button9_ButtonMenu20 = value;
				if (withEventsField__tbToolBar_Button9_ButtonMenu20 != null) {
					withEventsField__tbToolBar_Button9_ButtonMenu20.Click += tbToolBar_ButtonMenuClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripDropDownButton withEventsField__tbToolBar_Button9;
		public System.Windows.Forms.ToolStripDropDownButton _tbToolBar_Button9 {
			get { return withEventsField__tbToolBar_Button9; }
			set {
				if (withEventsField__tbToolBar_Button9 != null) {
					withEventsField__tbToolBar_Button9.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button9 = value;
				if (withEventsField__tbToolBar_Button9 != null) {
					withEventsField__tbToolBar_Button9.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripSeparator withEventsField__tbToolBar_Button10;
		public System.Windows.Forms.ToolStripSeparator _tbToolBar_Button10 {
			get { return withEventsField__tbToolBar_Button10; }
			set {
				if (withEventsField__tbToolBar_Button10 != null) {
					withEventsField__tbToolBar_Button10.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button10 = value;
				if (withEventsField__tbToolBar_Button10 != null) {
					withEventsField__tbToolBar_Button10.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button11;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button11 {
			get { return withEventsField__tbToolBar_Button11; }
			set {
				if (withEventsField__tbToolBar_Button11 != null) {
					withEventsField__tbToolBar_Button11.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button11 = value;
				if (withEventsField__tbToolBar_Button11 != null) {
					withEventsField__tbToolBar_Button11.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button12;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button12 {
			get { return withEventsField__tbToolBar_Button12; }
			set {
				if (withEventsField__tbToolBar_Button12 != null) {
					withEventsField__tbToolBar_Button12.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button12 = value;
				if (withEventsField__tbToolBar_Button12 != null) {
					withEventsField__tbToolBar_Button12.Click += tbToolBar_ButtonClick;
				}
			}
		}
		private System.Windows.Forms.ToolStripButton withEventsField__tbToolBar_Button13;
		public System.Windows.Forms.ToolStripButton _tbToolBar_Button13 {
			get { return withEventsField__tbToolBar_Button13; }
			set {
				if (withEventsField__tbToolBar_Button13 != null) {
					withEventsField__tbToolBar_Button13.Click -= tbToolBar_ButtonClick;
				}
				withEventsField__tbToolBar_Button13 = value;
				if (withEventsField__tbToolBar_Button13 != null) {
					withEventsField__tbToolBar_Button13.Click += tbToolBar_ButtonClick;
				}
			}
		}
		public System.Windows.Forms.ToolStrip tbToolBar;
		public System.Windows.Forms.ToolStripStatusLabel _sbStatusBar_Panel1;
		public System.Windows.Forms.ToolStripStatusLabel _sbStatusBar_Panel2;
		public System.Windows.Forms.ToolStripStatusLabel _sbStatusBar_Panel3;
		public System.Windows.Forms.StatusStrip sbStatusBar;
		public System.Windows.Forms.OpenFileDialog dlgCommonDialogOpen;
		public System.Windows.Forms.ImageList imlToolbarIcons;
//NOTE: The following procedure is required by the Windows Form Designer
//It can be modified using the Windows Form Designer.
//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.IsMdiContainer = true;
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(components);
			this.MainMenu1 = new System.Windows.Forms.MenuStrip();
			this.mnudata = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudatadata_copy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudatadata_compact = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudatadata_repair = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudataBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.mnudatadata_error = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudataBar1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnudatadata_security = new System.Windows.Forms.ToolStripMenuItem();
			this.mnudataBar2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnudatadata_exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasks = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_vendors = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_categories = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_company = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_journalnames = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_recurringinvoices = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksBar2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnutaskstasks_invoices = new System.Windows.Forms.ToolStripMenuItem();
			this.check = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_invoiceselection = new System.Windows.Forms.ToolStripMenuItem();
			this.check_laser = new System.Windows.Forms.ToolStripMenuItem();
			this.check_post = new System.Windows.Forms.ToolStripMenuItem();
			this.check_recon = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.mnutaskstasks_manual = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_batch = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_receivable = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutaskstasks_payable = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksBar1 = new System.Windows.Forms.ToolStripSeparator();
			this.disp_balances = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_cmp = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_cmpdpt = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_acct = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_acct_2 = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_budgets = new System.Windows.Forms.ToolStripMenuItem();
			this.disp_balance_budmaint = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksBar3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnutasksmaint = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksmaint_company = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksmaint_department = new System.Windows.Forms.ToolStripMenuItem();
			this.mnt_balfwddos = new System.Windows.Forms.ToolStripMenuItem();
			this.mnt_jrndesign = new System.Windows.Forms.ToolStripMenuItem();
			this.mnt_correct = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutasksBar4 = new System.Windows.Forms.ToolStripSeparator();
			this.yearend = new System.Windows.Forms.ToolStripMenuItem();
			this.yearend_vendor = new System.Windows.Forms.ToolStripMenuItem();
			this.yearend_gl = new System.Windows.Forms.ToolStripMenuItem();
			this.yearend_balances = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowTileVertical = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelpSearchForHelpOn = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelpBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuHelpWeb = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelpUpdates = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.popups = new System.Windows.Forms.ToolStripMenuItem();
			this.popups_vendors = new System.Windows.Forms.ToolStripMenuItem();
			this.popups_vendors_categories = new System.Windows.Forms.ToolStripMenuItem();
			this.tbToolBar = new System.Windows.Forms.ToolStrip();
			this._tbToolBar_Button1 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button2 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button3 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button4 = new System.Windows.Forms.ToolStripSeparator();
			this._tbToolBar_Button5 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button6 = new System.Windows.Forms.ToolStripSeparator();
			this._tbToolBar_Button7 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button8 = new System.Windows.Forms.ToolStripSeparator();
			this._tbToolBar_Button9 = new System.Windows.Forms.ToolStripDropDownButton();
			this._tbToolBar_Button9_ButtonMenu1 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu2 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu3 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu4 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu5 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu6 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu7 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu8 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu9 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu10 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu11 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu12 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu13 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu14 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu15 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu16 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu17 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu18 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu19 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button9_ButtonMenu20 = new System.Windows.Forms.ToolStripMenuItem();
			this._tbToolBar_Button10 = new System.Windows.Forms.ToolStripSeparator();
			this._tbToolBar_Button11 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button12 = new System.Windows.Forms.ToolStripButton();
			this._tbToolBar_Button13 = new System.Windows.Forms.ToolStripButton();
			this.sbStatusBar = new System.Windows.Forms.StatusStrip();
			this._sbStatusBar_Panel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this._sbStatusBar_Panel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this._sbStatusBar_Panel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgCommonDialogOpen = new System.Windows.Forms.OpenFileDialog();
			this.imlToolbarIcons = new System.Windows.Forms.ImageList();
			this.MainMenu1.SuspendLayout();
			this.tbToolBar.SuspendLayout();
			this.sbStatusBar.SuspendLayout();
			this.SuspendLayout();
			this.ToolTip1.Active = true;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.Text = "WinGL";
			this.ClientSize = new System.Drawing.Size(375, 290);
			this.Location = new System.Drawing.Point(15, 57);
			this.Icon = (System.Drawing.Icon)resources.GetObject("frmMain.Icon");
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			this.Enabled = true;
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			this.Name = "frmMain";
			this.mnudata.Name = "mnudata";
			this.mnudata.Text = "&Data";
			this.mnudata.Checked = false;
			this.mnudata.Enabled = true;
			this.mnudata.Visible = true;
			this.mnudatadata_copy.Name = "mnudatadata_copy";
			this.mnudatadata_copy.Text = "&Copy Database";
			this.mnudatadata_copy.Checked = false;
			this.mnudatadata_copy.Enabled = true;
			this.mnudatadata_copy.Visible = true;
			this.mnudatadata_compact.Name = "mnudatadata_compact";
			this.mnudatadata_compact.Text = "C&ompact Database";
			this.mnudatadata_compact.Checked = false;
			this.mnudatadata_compact.Enabled = true;
			this.mnudatadata_compact.Visible = true;
			this.mnudatadata_repair.Name = "mnudatadata_repair";
			this.mnudatadata_repair.Text = "&Repair Database";
			this.mnudatadata_repair.Checked = false;
			this.mnudatadata_repair.Enabled = true;
			this.mnudatadata_repair.Visible = true;
			this.mnudataBar0.Enabled = true;
			this.mnudataBar0.Visible = true;
			this.mnudataBar0.Name = "mnudataBar0";
			this.mnudatadata_error.Name = "mnudatadata_error";
			this.mnudatadata_error.Text = "View &Error Log";
			this.mnudatadata_error.Checked = false;
			this.mnudatadata_error.Enabled = true;
			this.mnudatadata_error.Visible = true;
			this.mnudataBar1.Enabled = true;
			this.mnudataBar1.Visible = true;
			this.mnudataBar1.Name = "mnudataBar1";
			this.mnudatadata_security.Name = "mnudatadata_security";
			this.mnudatadata_security.Text = "User &Security";
			this.mnudatadata_security.Checked = false;
			this.mnudatadata_security.Enabled = true;
			this.mnudatadata_security.Visible = true;
			this.mnudataBar2.Enabled = true;
			this.mnudataBar2.Visible = true;
			this.mnudataBar2.Name = "mnudataBar2";
			this.mnudatadata_exit.Name = "mnudatadata_exit";
			this.mnudatadata_exit.Text = "&Exit";
			this.mnudatadata_exit.Checked = false;
			this.mnudatadata_exit.Enabled = true;
			this.mnudatadata_exit.Visible = true;
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Text = "&Edit";
			this.mnuEdit.Checked = false;
			this.mnuEdit.Enabled = true;
			this.mnuEdit.Visible = true;
			this.mnuEditCut.Name = "mnuEditCut";
			this.mnuEditCut.Text = "Cu&t";
			this.mnuEditCut.ShortcutKeys = (System.Windows.Forms.Keys)System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X;
			this.mnuEditCut.Checked = false;
			this.mnuEditCut.Enabled = true;
			this.mnuEditCut.Visible = true;
			this.mnuEditCopy.Name = "mnuEditCopy";
			this.mnuEditCopy.Text = "&Copy";
			this.mnuEditCopy.ShortcutKeys = (System.Windows.Forms.Keys)System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
			this.mnuEditCopy.Checked = false;
			this.mnuEditCopy.Enabled = true;
			this.mnuEditCopy.Visible = true;
			this.mnuEditPaste.Name = "mnuEditPaste";
			this.mnuEditPaste.Text = "&Paste";
			this.mnuEditPaste.ShortcutKeys = (System.Windows.Forms.Keys)System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V;
			this.mnuEditPaste.Checked = false;
			this.mnuEditPaste.Enabled = true;
			this.mnuEditPaste.Visible = true;
			this.mnuView.Name = "mnuView";
			this.mnuView.Text = "&View";
			this.mnuView.Checked = false;
			this.mnuView.Enabled = true;
			this.mnuView.Visible = true;
			this.mnuViewToolbar.Name = "mnuViewToolbar";
			this.mnuViewToolbar.Text = "&Toolbar";
			this.mnuViewToolbar.Checked = true;
			this.mnuViewToolbar.Enabled = true;
			this.mnuViewToolbar.Visible = true;
			this.mnuViewStatusBar.Name = "mnuViewStatusBar";
			this.mnuViewStatusBar.Text = "Status &Bar";
			this.mnuViewStatusBar.Checked = true;
			this.mnuViewStatusBar.Enabled = true;
			this.mnuViewStatusBar.Visible = true;
			this.mnuViewBar0.Enabled = true;
			this.mnuViewBar0.Visible = true;
			this.mnuViewBar0.Name = "mnuViewBar0";
			this.mnuViewOptions.Name = "mnuViewOptions";
			this.mnuViewOptions.Text = "&Options...";
			this.mnuViewOptions.Checked = false;
			this.mnuViewOptions.Enabled = true;
			this.mnuViewOptions.Visible = true;
			this.mnutasks.Name = "mnutasks";
			this.mnutasks.Text = "&Tasks";
			this.mnutasks.Checked = false;
			this.mnutasks.Enabled = true;
			this.mnutasks.Visible = true;
			this.mnutaskstasks_vendors.Name = "mnutaskstasks_vendors";
			this.mnutaskstasks_vendors.Text = "&Vendors";
			this.mnutaskstasks_vendors.Checked = false;
			this.mnutaskstasks_vendors.Enabled = true;
			this.mnutaskstasks_vendors.Visible = true;
			this.mnutaskstasks_categories.Name = "mnutaskstasks_categories";
			this.mnutaskstasks_categories.Text = "C&ategrories and Accounts";
			this.mnutaskstasks_categories.Checked = false;
			this.mnutaskstasks_categories.Enabled = true;
			this.mnutaskstasks_categories.Visible = true;
			this.mnutaskstasks_company.Name = "mnutaskstasks_company";
			this.mnutaskstasks_company.Text = "&Company/Department Master";
			this.mnutaskstasks_company.Checked = false;
			this.mnutaskstasks_company.Enabled = true;
			this.mnutaskstasks_company.Visible = true;
			this.mnutaskstasks_journalnames.Name = "mnutaskstasks_journalnames";
			this.mnutaskstasks_journalnames.Text = "&Journal Names";
			this.mnutaskstasks_journalnames.Checked = false;
			this.mnutaskstasks_journalnames.Enabled = true;
			this.mnutaskstasks_journalnames.Visible = true;
			this.mnutaskstasks_recurringinvoices.Name = "mnutaskstasks_recurringinvoices";
			this.mnutaskstasks_recurringinvoices.Text = "&Batch Invoices";
			this.mnutaskstasks_recurringinvoices.Checked = false;
			this.mnutaskstasks_recurringinvoices.Enabled = true;
			this.mnutaskstasks_recurringinvoices.Visible = true;
			this.mnutasksBar2.Enabled = true;
			this.mnutasksBar2.Visible = true;
			this.mnutasksBar2.Name = "mnutasksBar2";
			this.mnutaskstasks_invoices.Name = "mnutaskstasks_invoices";
			this.mnutaskstasks_invoices.Text = "Invoice Entry and Edit";
			this.mnutaskstasks_invoices.Checked = false;
			this.mnutaskstasks_invoices.Enabled = true;
			this.mnutaskstasks_invoices.Visible = true;
			this.check.Name = "check";
			this.check.Text = "A/P &Check Processing";
			this.check.Checked = false;
			this.check.Enabled = true;
			this.check.Visible = true;
			this.mnutaskstasks_invoiceselection.Name = "mnutaskstasks_invoiceselection";
			this.mnutaskstasks_invoiceselection.Text = "Invoice Payment Selection";
			this.mnutaskstasks_invoiceselection.Checked = false;
			this.mnutaskstasks_invoiceselection.Enabled = true;
			this.mnutaskstasks_invoiceselection.Visible = true;
			this.check_laser.Name = "check_laser";
			this.check_laser.Text = "Print A/P Checks";
			this.check_laser.Checked = false;
			this.check_laser.Enabled = true;
			this.check_laser.Visible = true;
			this.check_post.Name = "check_post";
			this.check_post.Text = "Post Invoices";
			this.check_post.Checked = false;
			this.check_post.Enabled = true;
			this.check_post.Visible = true;
			this.check_recon.Name = "check_recon";
			this.check_recon.Text = "Check Reconciliation";
			this.check_recon.Checked = false;
			this.check_recon.Enabled = true;
			this.check_recon.Visible = true;
			this.mnutasksBar0.Enabled = true;
			this.mnutasksBar0.Visible = true;
			this.mnutasksBar0.Name = "mnutasksBar0";
			this.mnutaskstasks_manual.Name = "mnutaskstasks_manual";
			this.mnutaskstasks_manual.Text = "&Manual Journal Entries";
			this.mnutaskstasks_manual.Checked = false;
			this.mnutaskstasks_manual.Enabled = true;
			this.mnutaskstasks_manual.Visible = true;
			this.mnutaskstasks_batch.Name = "mnutaskstasks_batch";
			this.mnutaskstasks_batch.Text = "&Edit Batch Journal Entries";
			this.mnutaskstasks_batch.Checked = false;
			this.mnutaskstasks_batch.Enabled = true;
			this.mnutaskstasks_batch.Visible = true;
			this.mnutaskstasks_receivable.Name = "mnutaskstasks_receivable";
			this.mnutaskstasks_receivable.Text = "Load From Accounts &Receivable";
			this.mnutaskstasks_receivable.Checked = false;
			this.mnutaskstasks_receivable.Enabled = true;
			this.mnutaskstasks_receivable.Visible = true;
			this.mnutaskstasks_payable.Name = "mnutaskstasks_payable";
			this.mnutaskstasks_payable.Text = "Load From Accounts &Payable";
			this.mnutaskstasks_payable.Checked = false;
			this.mnutaskstasks_payable.Enabled = true;
			this.mnutaskstasks_payable.Visible = true;
			this.mnutasksBar1.Enabled = true;
			this.mnutasksBar1.Visible = true;
			this.mnutasksBar1.Name = "mnutasksBar1";
			this.disp_balances.Name = "disp_balances";
			this.disp_balances.Text = "Display Account Balances";
			this.disp_balances.Checked = false;
			this.disp_balances.Enabled = true;
			this.disp_balances.Visible = true;
			this.disp_balance_cmp.Name = "disp_balance_cmp";
			this.disp_balance_cmp.Text = "Balances By Company";
			this.disp_balance_cmp.Checked = false;
			this.disp_balance_cmp.Enabled = true;
			this.disp_balance_cmp.Visible = true;
			this.disp_balance_cmpdpt.Name = "disp_balance_cmpdpt";
			this.disp_balance_cmpdpt.Text = "Balances By Company/Department";
			this.disp_balance_cmpdpt.Checked = false;
			this.disp_balance_cmpdpt.Enabled = true;
			this.disp_balance_cmpdpt.Visible = true;
			this.disp_balance_acct.Name = "disp_balance_acct";
			this.disp_balance_acct.Text = "Balances By Account";
			this.disp_balance_acct.Checked = false;
			this.disp_balance_acct.Enabled = true;
			this.disp_balance_acct.Visible = true;
			this.disp_balance_acct_2.Name = "disp_balance_acct_2";
			this.disp_balance_acct_2.Text = "Balances By Account - 2 Years";
			this.disp_balance_acct_2.Checked = false;
			this.disp_balance_acct_2.Enabled = true;
			this.disp_balance_acct_2.Visible = true;
			this.disp_balance_budgets.Name = "disp_balance_budgets";
			this.disp_balance_budgets.Text = "Display Budgets";
			this.disp_balance_budgets.Checked = false;
			this.disp_balance_budgets.Enabled = true;
			this.disp_balance_budgets.Visible = true;
			this.disp_balance_budmaint.Name = "disp_balance_budmaint";
			this.disp_balance_budmaint.Text = "Edit Budgets";
			this.disp_balance_budmaint.Checked = false;
			this.disp_balance_budmaint.Enabled = true;
			this.disp_balance_budmaint.Visible = true;
			this.mnutasksBar3.Enabled = true;
			this.mnutasksBar3.Visible = true;
			this.mnutasksBar3.Name = "mnutasksBar3";
			this.mnutasksmaint.Name = "mnutasksmaint";
			this.mnutasksmaint.Text = "Maintenance &Tasks";
			this.mnutasksmaint.Checked = false;
			this.mnutasksmaint.Enabled = true;
			this.mnutasksmaint.Visible = true;
			this.mnutasksmaint_company.Name = "mnutasksmaint_company";
			this.mnutasksmaint_company.Text = "Get Companies From WinPM";
			this.mnutasksmaint_company.Checked = false;
			this.mnutasksmaint_company.Enabled = true;
			this.mnutasksmaint_company.Visible = true;
			this.mnutasksmaint_department.Name = "mnutasksmaint_department";
			this.mnutasksmaint_department.Text = "Get Departments From WinPM";
			this.mnutasksmaint_department.Checked = false;
			this.mnutasksmaint_department.Enabled = true;
			this.mnutasksmaint_department.Visible = true;
			this.mnt_balfwddos.Name = "mnt_balfwddos";
			this.mnt_balfwddos.Text = "Bring Balances Forward From DOS Version";
			this.mnt_balfwddos.Checked = false;
			this.mnt_balfwddos.Enabled = true;
			this.mnt_balfwddos.Visible = true;
			this.mnt_jrndesign.Name = "mnt_jrndesign";
			this.mnt_jrndesign.Text = "Journal Reports Design";
			this.mnt_jrndesign.Checked = false;
			this.mnt_jrndesign.Enabled = true;
			this.mnt_jrndesign.Visible = true;
			this.mnt_correct.Name = "mnt_correct";
			this.mnt_correct.Text = "Update Orphaned Transactions";
			this.mnt_correct.Checked = false;
			this.mnt_correct.Enabled = true;
			this.mnt_correct.Visible = true;
			this.mnutasksBar4.Enabled = true;
			this.mnutasksBar4.Visible = true;
			this.mnutasksBar4.Name = "mnutasksBar4";
			this.yearend.Name = "yearend";
			this.yearend.Text = "Year End";
			this.yearend.Checked = false;
			this.yearend.Enabled = true;
			this.yearend.Visible = true;
			this.yearend_vendor.Name = "yearend_vendor";
			this.yearend_vendor.Text = "Close Vendor Year";
			this.yearend_vendor.Checked = false;
			this.yearend_vendor.Enabled = true;
			this.yearend_vendor.Visible = true;
			this.yearend_gl.Name = "yearend_gl";
			this.yearend_gl.Text = "Close G/L Year";
			this.yearend_gl.Checked = false;
			this.yearend_gl.Enabled = true;
			this.yearend_gl.Visible = true;
			this.yearend_balances.Name = "yearend_balances";
			this.yearend_balances.Text = "Bring Prior Balances Forward";
			this.yearend_balances.Checked = false;
			this.yearend_balances.Enabled = true;
			this.yearend_balances.Visible = true;
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Text = "&Window";
			this.mnuWindow.Checked = false;
			this.mnuWindow.Enabled = true;
			this.mnuWindow.Visible = true;
			this.mnuWindowCascade.Name = "mnuWindowCascade";
			this.mnuWindowCascade.Text = "&Cascade";
			this.mnuWindowCascade.Checked = false;
			this.mnuWindowCascade.Enabled = true;
			this.mnuWindowCascade.Visible = true;
			this.mnuWindowTileHorizontal.Name = "mnuWindowTileHorizontal";
			this.mnuWindowTileHorizontal.Text = "Tile &Horizontal";
			this.mnuWindowTileHorizontal.Checked = false;
			this.mnuWindowTileHorizontal.Enabled = true;
			this.mnuWindowTileHorizontal.Visible = true;
			this.mnuWindowTileVertical.Name = "mnuWindowTileVertical";
			this.mnuWindowTileVertical.Text = "Tile &Vertical";
			this.mnuWindowTileVertical.Checked = false;
			this.mnuWindowTileVertical.Enabled = true;
			this.mnuWindowTileVertical.Visible = true;
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Text = "&Help";
			this.mnuHelp.Checked = false;
			this.mnuHelp.Enabled = true;
			this.mnuHelp.Visible = true;
			this.mnuHelpContents.Name = "mnuHelpContents";
			this.mnuHelpContents.Text = "&Contents";
			this.mnuHelpContents.Checked = false;
			this.mnuHelpContents.Enabled = true;
			this.mnuHelpContents.Visible = true;
			this.mnuHelpSearchForHelpOn.Name = "mnuHelpSearchForHelpOn";
			this.mnuHelpSearchForHelpOn.Text = "&Search For Help On...";
			this.mnuHelpSearchForHelpOn.Checked = false;
			this.mnuHelpSearchForHelpOn.Enabled = true;
			this.mnuHelpSearchForHelpOn.Visible = true;
			this.mnuHelpBar0.Enabled = true;
			this.mnuHelpBar0.Visible = true;
			this.mnuHelpBar0.Name = "mnuHelpBar0";
			this.mnuHelpWeb.Name = "mnuHelpWeb";
			this.mnuHelpWeb.Text = "The MBCS Web Site";
			this.mnuHelpWeb.Checked = false;
			this.mnuHelpWeb.Enabled = true;
			this.mnuHelpWeb.Visible = true;
			this.mnuHelpUpdates.Name = "mnuHelpUpdates";
			this.mnuHelpUpdates.Text = "Check for Updates";
			this.mnuHelpUpdates.Checked = false;
			this.mnuHelpUpdates.Enabled = true;
			this.mnuHelpUpdates.Visible = true;
			this.mnuHelpAbout.Name = "mnuHelpAbout";
			this.mnuHelpAbout.Text = "&About ";
			this.mnuHelpAbout.Checked = false;
			this.mnuHelpAbout.Enabled = true;
			this.mnuHelpAbout.Visible = true;
			this.popups.Name = "popups";
			this.popups.Text = "PopUps";
			this.popups.Visible = false;
			this.popups.Checked = false;
			this.popups.Enabled = true;
			this.popups_vendors.Name = "popups_vendors";
			this.popups_vendors.Text = "Vendors";
			this.popups_vendors.Checked = false;
			this.popups_vendors.Enabled = true;
			this.popups_vendors.Visible = true;
			this.popups_vendors_categories.Name = "popups_vendors_categories";
			this.popups_vendors_categories.Text = "Vendor Categories";
			this.popups_vendors_categories.Checked = false;
			this.popups_vendors_categories.Enabled = true;
			this.popups_vendors_categories.Visible = true;
			this.tbToolBar.ShowItemToolTips = true;
			this.tbToolBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.tbToolBar.Size = new System.Drawing.Size(375, 28);
			this.tbToolBar.Location = new System.Drawing.Point(0, 24);
			this.tbToolBar.TabIndex = 1;
			this.tbToolBar.ImageList = imlToolbarIcons;
			this.tbToolBar.Name = "tbToolBar";
			this._tbToolBar_Button1.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button1.AutoSize = false;
			this._tbToolBar_Button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button1.Name = "Camera";
			this._tbToolBar_Button1.ToolTipText = "Copy Database";
			this._tbToolBar_Button1.ImageKey = "Camera";
			this._tbToolBar_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button2.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button2.AutoSize = false;
			this._tbToolBar_Button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button2.Name = "Disks04";
			this._tbToolBar_Button2.ToolTipText = "Compact Database";
			this._tbToolBar_Button2.ImageKey = "Disks04";
			this._tbToolBar_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button3.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button3.AutoSize = false;
			this._tbToolBar_Button3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button3.Name = "Wrench";
			this._tbToolBar_Button3.ToolTipText = "Repair Database";
			this._tbToolBar_Button3.ImageKey = "Wrench";
			this._tbToolBar_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button4.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button4.AutoSize = false;
			this._tbToolBar_Button4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button5.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button5.AutoSize = false;
			this._tbToolBar_Button5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button5.Name = "Exclem";
			this._tbToolBar_Button5.ToolTipText = "Error Log";
			this._tbToolBar_Button5.ImageKey = "Exclem";
			this._tbToolBar_Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button6.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button6.AutoSize = false;
			this._tbToolBar_Button6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button7.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button7.AutoSize = false;
			this._tbToolBar_Button7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button7.Name = "Secur02a";
			this._tbToolBar_Button7.ToolTipText = "User Security";
			this._tbToolBar_Button7.ImageKey = "Secur02a";
			this._tbToolBar_Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button8.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button8.AutoSize = false;
			this._tbToolBar_Button8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button9.Size = new System.Drawing.Size(37, 22);
			this._tbToolBar_Button9.AutoSize = false;
			this._tbToolBar_Button9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button9.Name = "Print";
			this._tbToolBar_Button9.ToolTipText = "Print Reports";
			this._tbToolBar_Button9.ImageKey = "Print";
			this._tbToolBar_Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button9_ButtonMenu1.Text = "Account Master List";
			this._tbToolBar_Button9_ButtonMenu3.Text = "Vendor Master List";
			this._tbToolBar_Button9_ButtonMenu4.Text = "Company Master List";
			this._tbToolBar_Button9_ButtonMenu5.Enabled = 0;
			this._tbToolBar_Button9_ButtonMenu5.Text = "===============";
			this._tbToolBar_Button9_ButtonMenu6.Text = "Distribution Report";
			this._tbToolBar_Button9_ButtonMenu7.Text = "Invoice Payment List";
			this._tbToolBar_Button9_ButtonMenu8.Text = "Check Register";
			this._tbToolBar_Button9_ButtonMenu9.Text = "Payroll Report";
			this._tbToolBar_Button9_ButtonMenu10.Text = "1099 Report";
			this._tbToolBar_Button9_ButtonMenu11.Enabled = 0;
			this._tbToolBar_Button9_ButtonMenu11.Text = "===============";
			this._tbToolBar_Button9_ButtonMenu12.Text = "Transaction Edit List";
			this._tbToolBar_Button9_ButtonMenu13.Text = "General Ledger";
			this._tbToolBar_Button9_ButtonMenu14.Text = "Operating Statement";
			this._tbToolBar_Button9_ButtonMenu15.Text = "Balance Sheet";
			this._tbToolBar_Button9_ButtonMenu16.Text = "Journal Reports";
			this._tbToolBar_Button9_ButtonMenu17.Text = "Disbursement Report";
			this._tbToolBar_Button9_ButtonMenu18.Text = "Trial Balance";
			this._tbToolBar_Button9_ButtonMenu19.Enabled = 0;
			this._tbToolBar_Button9_ButtonMenu19.Text = "===============";
			this._tbToolBar_Button9_ButtonMenu20.Text = "Batch Reporting";
			this._tbToolBar_Button10.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button10.AutoSize = false;
			this._tbToolBar_Button10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button11.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button11.AutoSize = false;
			this._tbToolBar_Button11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button11.Name = "Cut";
			this._tbToolBar_Button11.ToolTipText = "Cut";
			this._tbToolBar_Button11.ImageKey = "Cut";
			this._tbToolBar_Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button12.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button12.AutoSize = false;
			this._tbToolBar_Button12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button12.Name = "Copy";
			this._tbToolBar_Button12.ToolTipText = "Copy";
			this._tbToolBar_Button12.ImageKey = "Copy";
			this._tbToolBar_Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this._tbToolBar_Button13.Size = new System.Drawing.Size(24, 22);
			this._tbToolBar_Button13.AutoSize = false;
			this._tbToolBar_Button13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this._tbToolBar_Button13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this._tbToolBar_Button13.Name = "Paste";
			this._tbToolBar_Button13.ToolTipText = "Paste";
			this._tbToolBar_Button13.ImageKey = "Paste";
			this._tbToolBar_Button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.sbStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.sbStatusBar.Size = new System.Drawing.Size(375, 18);
			this.sbStatusBar.Location = new System.Drawing.Point(0, 272);
			this.sbStatusBar.TabIndex = 0;
			this.sbStatusBar.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.sbStatusBar.Name = "sbStatusBar";
			this._sbStatusBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this._sbStatusBar_Panel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._sbStatusBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._sbStatusBar_Panel1.Size = new System.Drawing.Size(162, 18);
			this._sbStatusBar_Panel1.Text = "Status";
			this._sbStatusBar_Panel1.Spring = true;
			this._sbStatusBar_Panel1.AutoSize = true;
			this._sbStatusBar_Panel1.BorderSides = (System.Windows.Forms.ToolStripStatusLabelBorderSides)System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
			this._sbStatusBar_Panel1.Margin = new System.Windows.Forms.Padding(0);
			this._sbStatusBar_Panel1.AutoSize = false;
			this._sbStatusBar_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this._sbStatusBar_Panel2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._sbStatusBar_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._sbStatusBar_Panel2.AutoSize = true;
			this._sbStatusBar_Panel2.BorderSides = (System.Windows.Forms.ToolStripStatusLabelBorderSides)System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
			this._sbStatusBar_Panel2.Margin = new System.Windows.Forms.Padding(0);
			this._sbStatusBar_Panel2.Size = new System.Drawing.Size(96, 18);
			this._sbStatusBar_Panel2.AutoSize = false;
			this._sbStatusBar_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this._sbStatusBar_Panel3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._sbStatusBar_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._sbStatusBar_Panel3.AutoSize = true;
			this._sbStatusBar_Panel3.BorderSides = (System.Windows.Forms.ToolStripStatusLabelBorderSides)System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
			this._sbStatusBar_Panel3.Margin = new System.Windows.Forms.Padding(0);
			this._sbStatusBar_Panel3.Size = new System.Drawing.Size(96, 18);
			this._sbStatusBar_Panel3.AutoSize = false;
			this.imlToolbarIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlToolbarIcons.TransparentColor = System.Drawing.Color.FromArgb(192, 192, 192);
			this.imlToolbarIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imlToolbarIcons.ImageStream");
			this.imlToolbarIcons.Images.SetKeyName(0, "Camera");
			this.imlToolbarIcons.Images.SetKeyName(1, "Disks04");
			this.imlToolbarIcons.Images.SetKeyName(2, "Wrench");
			this.imlToolbarIcons.Images.SetKeyName(3, "Exclem");
			this.imlToolbarIcons.Images.SetKeyName(4, "Secur02a");
			this.imlToolbarIcons.Images.SetKeyName(5, "Print");
			this.imlToolbarIcons.Images.SetKeyName(6, "Cut");
			this.imlToolbarIcons.Images.SetKeyName(7, "Copy");
			this.imlToolbarIcons.Images.SetKeyName(8, "Paste");
			this.Controls.Add(tbToolBar);
			this.Controls.Add(sbStatusBar);
			this.tbToolBar.Items.Add(_tbToolBar_Button1);
			this.tbToolBar.Items.Add(_tbToolBar_Button2);
			this.tbToolBar.Items.Add(_tbToolBar_Button3);
			this.tbToolBar.Items.Add(_tbToolBar_Button4);
			this.tbToolBar.Items.Add(_tbToolBar_Button5);
			this.tbToolBar.Items.Add(_tbToolBar_Button6);
			this.tbToolBar.Items.Add(_tbToolBar_Button7);
			this.tbToolBar.Items.Add(_tbToolBar_Button8);
			this.tbToolBar.Items.Add(_tbToolBar_Button9);
			this.tbToolBar.Items.Add(_tbToolBar_Button10);
			this.tbToolBar.Items.Add(_tbToolBar_Button11);
			this.tbToolBar.Items.Add(_tbToolBar_Button12);
			this.tbToolBar.Items.Add(_tbToolBar_Button13);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu1);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu2);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu3);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu4);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu5);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu6);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu7);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu8);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu9);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu10);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu11);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu12);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu13);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu14);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu15);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu16);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu17);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu18);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu19);
			this._tbToolBar_Button9.DropDownItems.Add(_tbToolBar_Button9_ButtonMenu20);
			this.sbStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this._sbStatusBar_Panel1 });
			this.sbStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this._sbStatusBar_Panel2 });
			this.sbStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this._sbStatusBar_Panel3 });
			this.mnudata.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.mnuEdit.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.mnuView.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.mnutasks.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.mnuWindow.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.mnuHelp.MergeAction = System.Windows.Forms.MergeAction.Remove;
			this.popups.MergeAction = System.Windows.Forms.MergeAction.Remove;
			MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnudata,
				this.mnuEdit,
				this.mnuView,
				this.mnutasks,
				this.mnuWindow,
				this.mnuHelp,
				this.popups
			});
			mnudata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnudatadata_copy,
				this.mnudatadata_compact,
				this.mnudatadata_repair,
				this.mnudataBar0,
				this.mnudatadata_error,
				this.mnudataBar1,
				this.mnudatadata_security,
				this.mnudataBar2,
				this.mnudatadata_exit
			});
			mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnuEditCut,
				this.mnuEditCopy,
				this.mnuEditPaste
			});
			mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnuViewToolbar,
				this.mnuViewStatusBar,
				this.mnuViewBar0,
				this.mnuViewOptions
			});
			mnutasks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnutaskstasks_vendors,
				this.mnutaskstasks_categories,
				this.mnutaskstasks_company,
				this.mnutaskstasks_journalnames,
				this.mnutaskstasks_recurringinvoices,
				this.mnutasksBar2,
				this.mnutaskstasks_invoices,
				this.check,
				this.mnutasksBar0,
				this.mnutaskstasks_manual,
				this.mnutaskstasks_batch,
				this.mnutaskstasks_receivable,
				this.mnutaskstasks_payable,
				this.mnutasksBar1,
				this.disp_balances,
				this.disp_balance_budgets,
				this.disp_balance_budmaint,
				this.mnutasksBar3,
				this.mnutasksmaint,
				this.mnutasksBar4,
				this.yearend
			});
			check.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnutaskstasks_invoiceselection,
				this.check_laser,
				this.check_post,
				this.check_recon
			});
			disp_balances.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.disp_balance_cmp,
				this.disp_balance_cmpdpt,
				this.disp_balance_acct,
				this.disp_balance_acct_2
			});
			mnutasksmaint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnutasksmaint_company,
				this.mnutasksmaint_department,
				this.mnt_balfwddos,
				this.mnt_jrndesign,
				this.mnt_correct
			});
			yearend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.yearend_vendor,
				this.yearend_gl,
				this.yearend_balances
			});
			mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnuWindowCascade,
				this.mnuWindowTileHorizontal,
				this.mnuWindowTileVertical
			});
			mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnuHelpContents,
				this.mnuHelpSearchForHelpOn,
				this.mnuHelpBar0,
				this.mnuHelpWeb,
				this.mnuHelpUpdates,
				this.mnuHelpAbout
			});
			popups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.popups_vendors });
			popups_vendors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.popups_vendors_categories });
			this.Controls.Add(MainMenu1);
			this.MainMenu1.ResumeLayout(false);
			this.tbToolBar.ResumeLayout(false);
			this.sbStatusBar.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
	}
}
