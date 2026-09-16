using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;

// This file was created by the VB to C# converter (SharpDevelop 4.4.1.9729).
// It contains classes for supporting the VB "My" namespace in C#.
// If the VB application does not use the "My" namespace, or if you removed the usage
// after the conversion to C#, you can delete this file.

namespace WinGL.My
{
	sealed partial class MyProject
	{
		[ThreadStatic] static MyApplication application;
		
		public static MyApplication Application {
			[DebuggerStepThrough]
			get {
				if (application == null)
					application = new MyApplication();
				return application;
			}
		}
		
		[ThreadStatic] static MyComputer computer;
		
		public static MyComputer Computer {
			[DebuggerStepThrough]
			get {
				if (computer == null)
					computer = new MyComputer();
				return computer;
			}
		}
		
		[ThreadStatic] static User user;
		
		public static User User {
			[DebuggerStepThrough]
			get {
				if (user == null)
					user = new User();
				return user;
			}
		}
		
		[ThreadStatic] static MyForms forms;
		
		public static MyForms Forms {
			[DebuggerStepThrough]
			get {
				if (forms == null)
					forms = new MyForms();
				return forms;
			}
		}
		
		internal sealed class MyForms
		{
			global::WinGL.frmPayrollReport frmPayrollReport_instance;
			bool frmPayrollReport_isCreating;
			public global::WinGL.frmPayrollReport frmPayrollReport {
				[DebuggerStepThrough] get { return GetForm(ref frmPayrollReport_instance, ref frmPayrollReport_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmPayrollReport_instance, value); }
			}
			
			global::WinGL.AccountDetail AccountDetail_instance;
			bool AccountDetail_isCreating;
			public global::WinGL.AccountDetail AccountDetail {
				[DebuggerStepThrough] get { return GetForm(ref AccountDetail_instance, ref AccountDetail_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref AccountDetail_instance, value); }
			}
			
			global::WinGL.frmSplash frmSplash_instance;
			bool frmSplash_isCreating;
			public global::WinGL.frmSplash frmSplash {
				[DebuggerStepThrough] get { return GetForm(ref frmSplash_instance, ref frmSplash_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmSplash_instance, value); }
			}
			
			global::WinGL.AcctSelection AcctSelection_instance;
			bool AcctSelection_isCreating;
			public global::WinGL.AcctSelection AcctSelection {
				[DebuggerStepThrough] get { return GetForm(ref AcctSelection_instance, ref AcctSelection_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref AcctSelection_instance, value); }
			}
			
			global::WinGL.frmTrialBalance frmTrialBalance_instance;
			bool frmTrialBalance_isCreating;
			public global::WinGL.frmTrialBalance frmTrialBalance {
				[DebuggerStepThrough] get { return GetForm(ref frmTrialBalance_instance, ref frmTrialBalance_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmTrialBalance_instance, value); }
			}
			
			global::WinGL.GLentry GLentry_instance;
			bool GLentry_isCreating;
			public global::WinGL.GLentry GLentry {
				[DebuggerStepThrough] get { return GetForm(ref GLentry_instance, ref GLentry_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref GLentry_instance, value); }
			}
			
			global::WinGL.APLaser APLaser_instance;
			bool APLaser_isCreating;
			public global::WinGL.APLaser APLaser {
				[DebuggerStepThrough] get { return GetForm(ref APLaser_instance, ref APLaser_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref APLaser_instance, value); }
			}
			
			global::WinGL.Invoice Invoice_instance;
			bool Invoice_isCreating;
			public global::WinGL.Invoice Invoice {
				[DebuggerStepThrough] get { return GetForm(ref Invoice_instance, ref Invoice_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref Invoice_instance, value); }
			}
			
			global::WinGL.CmpBalanceDisp CmpBalanceDisp_instance;
			bool CmpBalanceDisp_isCreating;
			public global::WinGL.CmpBalanceDisp CmpBalanceDisp {
				[DebuggerStepThrough] get { return GetForm(ref CmpBalanceDisp_instance, ref CmpBalanceDisp_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CmpBalanceDisp_instance, value); }
			}
			
			global::WinGL.InvoiceDistributions InvoiceDistributions_instance;
			bool InvoiceDistributions_isCreating;
			public global::WinGL.InvoiceDistributions InvoiceDistributions {
				[DebuggerStepThrough] get { return GetForm(ref InvoiceDistributions_instance, ref InvoiceDistributions_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref InvoiceDistributions_instance, value); }
			}
			
			global::WinGL.BalanceByAcct BalanceByAcct_instance;
			bool BalanceByAcct_isCreating;
			public global::WinGL.BalanceByAcct BalanceByAcct {
				[DebuggerStepThrough] get { return GetForm(ref BalanceByAcct_instance, ref BalanceByAcct_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BalanceByAcct_instance, value); }
			}
			
			global::WinGL.BalanceByAcctShort BalanceByAcctShort_instance;
			bool BalanceByAcctShort_isCreating;
			public global::WinGL.BalanceByAcctShort BalanceByAcctShort {
				[DebuggerStepThrough] get { return GetForm(ref BalanceByAcctShort_instance, ref BalanceByAcctShort_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BalanceByAcctShort_instance, value); }
			}
			
			global::WinGL.InvoiceNumber InvoiceNumber_instance;
			bool InvoiceNumber_isCreating;
			public global::WinGL.InvoiceNumber InvoiceNumber {
				[DebuggerStepThrough] get { return GetForm(ref InvoiceNumber_instance, ref InvoiceNumber_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref InvoiceNumber_instance, value); }
			}
			
			global::WinGL.Invoices Invoices_instance;
			bool Invoices_isCreating;
			public global::WinGL.Invoices Invoices {
				[DebuggerStepThrough] get { return GetForm(ref Invoices_instance, ref Invoices_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref Invoices_instance, value); }
			}
			
			global::WinGL.CompanyDepartment CompanyDepartment_instance;
			bool CompanyDepartment_isCreating;
			public global::WinGL.CompanyDepartment CompanyDepartment {
				[DebuggerStepThrough] get { return GetForm(ref CompanyDepartment_instance, ref CompanyDepartment_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CompanyDepartment_instance, value); }
			}
			
			global::WinGL.CompanyDetail CompanyDetail_instance;
			bool CompanyDetail_isCreating;
			public global::WinGL.CompanyDetail CompanyDetail {
				[DebuggerStepThrough] get { return GetForm(ref CompanyDetail_instance, ref CompanyDetail_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CompanyDetail_instance, value); }
			}
			
			global::WinGL.DepartmentDetail DepartmentDetail_instance;
			bool DepartmentDetail_isCreating;
			public global::WinGL.DepartmentDetail DepartmentDetail {
				[DebuggerStepThrough] get { return GetForm(ref DepartmentDetail_instance, ref DepartmentDetail_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref DepartmentDetail_instance, value); }
			}
			
			global::WinGL.BalanceDisp BalanceDisp_instance;
			bool BalanceDisp_isCreating;
			public global::WinGL.BalanceDisp BalanceDisp {
				[DebuggerStepThrough] get { return GetForm(ref BalanceDisp_instance, ref BalanceDisp_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BalanceDisp_instance, value); }
			}
			
			global::WinGL.edtTransEditList edtTransEditList_instance;
			bool edtTransEditList_isCreating;
			public global::WinGL.edtTransEditList edtTransEditList {
				[DebuggerStepThrough] get { return GetForm(ref edtTransEditList_instance, ref edtTransEditList_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref edtTransEditList_instance, value); }
			}
			
			global::WinGL.frmAbout frmAbout_instance;
			bool frmAbout_isCreating;
			public global::WinGL.frmAbout frmAbout {
				[DebuggerStepThrough] get { return GetForm(ref frmAbout_instance, ref frmAbout_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmAbout_instance, value); }
			}
			
			global::WinGL.BalFwdOld BalFwdOld_instance;
			bool BalFwdOld_isCreating;
			public global::WinGL.BalFwdOld BalFwdOld {
				[DebuggerStepThrough] get { return GetForm(ref BalFwdOld_instance, ref BalFwdOld_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BalFwdOld_instance, value); }
			}
			
			global::WinGL.InvoicesBatch InvoicesBatch_instance;
			bool InvoicesBatch_isCreating;
			public global::WinGL.InvoicesBatch InvoicesBatch {
				[DebuggerStepThrough] get { return GetForm(ref InvoicesBatch_instance, ref InvoicesBatch_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref InvoicesBatch_instance, value); }
			}
			
			global::WinGL.frmBalanceSheet frmBalanceSheet_instance;
			bool frmBalanceSheet_isCreating;
			public global::WinGL.frmBalanceSheet frmBalanceSheet {
				[DebuggerStepThrough] get { return GetForm(ref frmBalanceSheet_instance, ref frmBalanceSheet_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmBalanceSheet_instance, value); }
			}
			
			global::WinGL.frmBatch frmBatch_instance;
			bool frmBatch_isCreating;
			public global::WinGL.frmBatch frmBatch {
				[DebuggerStepThrough] get { return GetForm(ref frmBatch_instance, ref frmBatch_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmBatch_instance, value); }
			}
			
			global::WinGL.InvoiceSelection InvoiceSelection_instance;
			bool InvoiceSelection_isCreating;
			public global::WinGL.InvoiceSelection InvoiceSelection {
				[DebuggerStepThrough] get { return GetForm(ref InvoiceSelection_instance, ref InvoiceSelection_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref InvoiceSelection_instance, value); }
			}
			
			global::WinGL.frmCheckRegister frmCheckRegister_instance;
			bool frmCheckRegister_isCreating;
			public global::WinGL.frmCheckRegister frmCheckRegister {
				[DebuggerStepThrough] get { return GetForm(ref frmCheckRegister_instance, ref frmCheckRegister_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmCheckRegister_instance, value); }
			}
			
			global::WinGL.JournalDesign JournalDesign_instance;
			bool JournalDesign_isCreating;
			public global::WinGL.JournalDesign JournalDesign {
				[DebuggerStepThrough] get { return GetForm(ref JournalDesign_instance, ref JournalDesign_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref JournalDesign_instance, value); }
			}
			
			global::WinGL.frmCorrect frmCorrect_instance;
			bool frmCorrect_isCreating;
			public global::WinGL.frmCorrect frmCorrect {
				[DebuggerStepThrough] get { return GetForm(ref frmCorrect_instance, ref frmCorrect_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmCorrect_instance, value); }
			}
			
			global::WinGL.BatchDates BatchDates_instance;
			bool BatchDates_isCreating;
			public global::WinGL.BatchDates BatchDates {
				[DebuggerStepThrough] get { return GetForm(ref BatchDates_instance, ref BatchDates_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BatchDates_instance, value); }
			}
			
			global::WinGL.frmDisbRpt frmDisbRpt_instance;
			bool frmDisbRpt_isCreating;
			public global::WinGL.frmDisbRpt frmDisbRpt {
				[DebuggerStepThrough] get { return GetForm(ref frmDisbRpt_instance, ref frmDisbRpt_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmDisbRpt_instance, value); }
			}
			
			global::WinGL.frmDocPreview frmDocPreview_instance;
			bool frmDocPreview_isCreating;
			public global::WinGL.frmDocPreview frmDocPreview {
				[DebuggerStepThrough] get { return GetForm(ref frmDocPreview_instance, ref frmDocPreview_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmDocPreview_instance, value); }
			}
			
			global::WinGL.JournalNames JournalNames_instance;
			bool JournalNames_isCreating;
			public global::WinGL.JournalNames JournalNames {
				[DebuggerStepThrough] get { return GetForm(ref JournalNames_instance, ref JournalNames_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref JournalNames_instance, value); }
			}
			
			global::WinGL.BatchJrnEdit BatchJrnEdit_instance;
			bool BatchJrnEdit_isCreating;
			public global::WinGL.BatchJrnEdit BatchJrnEdit {
				[DebuggerStepThrough] get { return GetForm(ref BatchJrnEdit_instance, ref BatchJrnEdit_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BatchJrnEdit_instance, value); }
			}
			
			global::WinGL.LaserChk LaserChk_instance;
			bool LaserChk_isCreating;
			public global::WinGL.LaserChk LaserChk {
				[DebuggerStepThrough] get { return GetForm(ref LaserChk_instance, ref LaserChk_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref LaserChk_instance, value); }
			}
			
			global::WinGL.LaserMsg LaserMsg_instance;
			bool LaserMsg_isCreating;
			public global::WinGL.LaserMsg LaserMsg {
				[DebuggerStepThrough] get { return GetForm(ref LaserMsg_instance, ref LaserMsg_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref LaserMsg_instance, value); }
			}
			
			global::WinGL.frmF1099 frmF1099_instance;
			bool frmF1099_isCreating;
			public global::WinGL.frmF1099 frmF1099 {
				[DebuggerStepThrough] get { return GetForm(ref frmF1099_instance, ref frmF1099_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmF1099_instance, value); }
			}
			
			global::WinGL.BatchList BatchList_instance;
			bool BatchList_isCreating;
			public global::WinGL.BatchList BatchList {
				[DebuggerStepThrough] get { return GetForm(ref BatchList_instance, ref BatchList_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BatchList_instance, value); }
			}
			
			global::WinGL.MoveDpt MoveDpt_instance;
			bool MoveDpt_isCreating;
			public global::WinGL.MoveDpt MoveDpt {
				[DebuggerStepThrough] get { return GetForm(ref MoveDpt_instance, ref MoveDpt_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref MoveDpt_instance, value); }
			}
			
			global::WinGL.BudgetDisp BudgetDisp_instance;
			bool BudgetDisp_isCreating;
			public global::WinGL.BudgetDisp BudgetDisp {
				[DebuggerStepThrough] get { return GetForm(ref BudgetDisp_instance, ref BudgetDisp_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BudgetDisp_instance, value); }
			}
			
			global::WinGL.frmJournals frmJournals_instance;
			bool frmJournals_isCreating;
			public global::WinGL.frmJournals frmJournals {
				[DebuggerStepThrough] get { return GetForm(ref frmJournals_instance, ref frmJournals_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmJournals_instance, value); }
			}
			
			global::WinGL.BudgetMaint BudgetMaint_instance;
			bool BudgetMaint_isCreating;
			public global::WinGL.BudgetMaint BudgetMaint {
				[DebuggerStepThrough] get { return GetForm(ref BudgetMaint_instance, ref BudgetMaint_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref BudgetMaint_instance, value); }
			}
			
			global::WinGL.frmLedger frmLedger_instance;
			bool frmLedger_isCreating;
			public global::WinGL.frmLedger frmLedger {
				[DebuggerStepThrough] get { return GetForm(ref frmLedger_instance, ref frmLedger_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmLedger_instance, value); }
			}
			
			global::WinGL.frmLogin frmLogin_instance;
			bool frmLogin_isCreating;
			public global::WinGL.frmLogin frmLogin {
				[DebuggerStepThrough] get { return GetForm(ref frmLogin_instance, ref frmLogin_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmLogin_instance, value); }
			}
			
			global::WinGL.frmMain frmMain_instance;
			bool frmMain_isCreating;
			public global::WinGL.frmMain frmMain {
				[DebuggerStepThrough] get { return GetForm(ref frmMain_instance, ref frmMain_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmMain_instance, value); }
			}
			
			global::WinGL.rptMaster rptMaster_instance;
			bool rptMaster_isCreating;
			public global::WinGL.rptMaster rptMaster {
				[DebuggerStepThrough] get { return GetForm(ref rptMaster_instance, ref rptMaster_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref rptMaster_instance, value); }
			}
			
			global::WinGL.VendorCategories VendorCategories_instance;
			bool VendorCategories_isCreating;
			public global::WinGL.VendorCategories VendorCategories {
				[DebuggerStepThrough] get { return GetForm(ref VendorCategories_instance, ref VendorCategories_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref VendorCategories_instance, value); }
			}
			
			global::WinGL.Categories Categories_instance;
			bool Categories_isCreating;
			public global::WinGL.Categories Categories {
				[DebuggerStepThrough] get { return GetForm(ref Categories_instance, ref Categories_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref Categories_instance, value); }
			}
			
			global::WinGL.VendorDetail VendorDetail_instance;
			bool VendorDetail_isCreating;
			public global::WinGL.VendorDetail VendorDetail {
				[DebuggerStepThrough] get { return GetForm(ref VendorDetail_instance, ref VendorDetail_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref VendorDetail_instance, value); }
			}
			
			global::WinGL.frmMaintenance frmMaintenance_instance;
			bool frmMaintenance_isCreating;
			public global::WinGL.frmMaintenance frmMaintenance {
				[DebuggerStepThrough] get { return GetForm(ref frmMaintenance_instance, ref frmMaintenance_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmMaintenance_instance, value); }
			}
			
			global::WinGL.frmMessage frmMessage_instance;
			bool frmMessage_isCreating;
			public global::WinGL.frmMessage frmMessage {
				[DebuggerStepThrough] get { return GetForm(ref frmMessage_instance, ref frmMessage_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmMessage_instance, value); }
			}
			
			global::WinGL.Vendors Vendors_instance;
			bool Vendors_isCreating;
			public global::WinGL.Vendors Vendors {
				[DebuggerStepThrough] get { return GetForm(ref Vendors_instance, ref Vendors_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref Vendors_instance, value); }
			}
			
			global::WinGL.CategoryDetail CategoryDetail_instance;
			bool CategoryDetail_isCreating;
			public global::WinGL.CategoryDetail CategoryDetail {
				[DebuggerStepThrough] get { return GetForm(ref CategoryDetail_instance, ref CategoryDetail_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CategoryDetail_instance, value); }
			}
			
			global::WinGL.frmOperatingStatement frmOperatingStatement_instance;
			bool frmOperatingStatement_isCreating;
			public global::WinGL.frmOperatingStatement frmOperatingStatement {
				[DebuggerStepThrough] get { return GetForm(ref frmOperatingStatement_instance, ref frmOperatingStatement_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmOperatingStatement_instance, value); }
			}
			
			global::WinGL.CheckRecon CheckRecon_instance;
			bool CheckRecon_isCreating;
			public global::WinGL.CheckRecon CheckRecon {
				[DebuggerStepThrough] get { return GetForm(ref CheckRecon_instance, ref CheckRecon_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CheckRecon_instance, value); }
			}
			
			global::WinGL.frmOptions frmOptions_instance;
			bool frmOptions_isCreating;
			public global::WinGL.frmOptions frmOptions {
				[DebuggerStepThrough] get { return GetForm(ref frmOptions_instance, ref frmOptions_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref frmOptions_instance, value); }
			}
			
			global::WinGL.CheckRegister CheckRegister_instance;
			bool CheckRegister_isCreating;
			public global::WinGL.CheckRegister CheckRegister {
				[DebuggerStepThrough] get { return GetForm(ref CheckRegister_instance, ref CheckRegister_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CheckRegister_instance, value); }
			}
			
			global::WinGL.ChkRecJrn ChkRecJrn_instance;
			bool ChkRecJrn_isCreating;
			public global::WinGL.ChkRecJrn ChkRecJrn {
				[DebuggerStepThrough] get { return GetForm(ref ChkRecJrn_instance, ref ChkRecJrn_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref ChkRecJrn_instance, value); }
			}
			
			global::WinGL.CloseYear CloseYear_instance;
			bool CloseYear_isCreating;
			public global::WinGL.CloseYear CloseYear {
				[DebuggerStepThrough] get { return GetForm(ref CloseYear_instance, ref CloseYear_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref CloseYear_instance, value); }
			}
			
			[DebuggerStepThrough]
			static T GetForm<T>(ref T instance, ref bool isCreating) where T : Form, new()
			{
				if (instance == null || instance.IsDisposed) {
					if (isCreating) {
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
					}
					isCreating = true;
					try {
						instance = new T();
					} catch (System.Reflection.TargetInvocationException ex) {
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", new string[] { ex.InnerException.Message }), ex.InnerException);
					} finally {
						isCreating = false;
					}
				}
				return instance;
			}
			
			[DebuggerStepThrough]
			static void SetForm<T>(ref T instance, T value) where T : Form
			{
				if (instance != value) {
					if (value == null) {
						instance.Dispose();
						instance = null;
					} else {
						throw new ArgumentException("Property can only be set to null");
					}
				}
			}
		}
	}
	
	partial class MyApplication : WindowsFormsApplicationBase
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Application.SetCompatibleTextRenderingDefault(UseCompatibleTextRendering);
			MyProject.Application.Run(args);
		}
	}
	
	partial class MyComputer : Computer
	{
	}
}
