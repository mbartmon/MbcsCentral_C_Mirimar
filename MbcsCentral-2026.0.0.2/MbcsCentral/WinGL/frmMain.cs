using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	internal partial class frmMain : System.Windows.Forms.Form
	{
//UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
		//Private Declare Function OSWinHelp Lib "user32"  Alias "WinHelpA"(ByVal hwnd As Integer, ByVal HelpFile As String, ByVal wCommand As Short, ByRef dwData As Any) As Short
		object xrpt;
		object pg;
		private void acct_select_Click()
		{

			//UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
			//Load(AcctSelection)
			My.MyProject.Forms.AcctSelection.AccountSelection("AccountRanges", "OperatingStatement", 1, "004_");

		}
		private void BatchInvoiceChecker_Timer()
		{
			object BatchInvoiceChecker = null;

			if (Version >= 1.6) {
				if (WinglGlobal.BatchOn & (!WinglGlobal.BatchInvoiceChecked | WinglGlobal.BatchCheckDate < DateAndTime.Today)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object BatchInvoiceChecker.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					BatchInvoiceChecker.Enabled = false;
					WinglGlobal.BatchInvoiceCheck();
					//UPGRADE_WARNING: Couldn't resolve default property of object BatchInvoiceChecker.Interval. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					BatchInvoiceChecker.Interval = 65300;
					//UPGRADE_WARNING: Couldn't resolve default property of object BatchInvoiceChecker.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					BatchInvoiceChecker.Enabled = true;
				}
			}

		}
		public void check_laser_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.APLaser.Top = 0;
			My.MyProject.Forms.APLaser.Left = 0;
			My.MyProject.Forms.APLaser.Show();

		}

		public void check_post_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			WinglGlobal.batchProcess = false;
			WinglGlobal.Post_Invoices();
			this.Cursor = System.Windows.Forms.Cursors.Default;

		}

		public void check_recon_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.CheckRecon.ShowDialog();

		}

		public void disp_balance_acct_2_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.BalanceByAcctShort.Show();

		}

		public void disp_balance_acct_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			if (WinglGlobal.fbalc < 0)
				WinglGlobal.fbalc = 0;
			if (WinglGlobal.fbalc < WinglGlobal.fbalm) {
				WinglGlobal.fbal[WinglGlobal.fbalc] = new BalanceByAcct();
				WinglGlobal.fbal[WinglGlobal.fbalc].Show();
				WinglGlobal.fbalc = WinglGlobal.fbalc + 1;
			}

		}
		public void disp_balance_budgets_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.BudgetDisp.Show();

		}
		public void disp_balance_budmaint_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.BudgetMaint.Show();

		}
		public void disp_balance_cmp_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (WinglGlobal.fbalc < WinglGlobal.fbalm) {
				WinglGlobal.fbal[WinglGlobal.fbalc] = new CmpBalanceDisp();
				WinglGlobal.fbal[WinglGlobal.fbalc].Show();
				WinglGlobal.fbalc = WinglGlobal.fbalc + 1;
			}

		}
		public void disp_balance_cmpdpt_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (WinglGlobal.fbalc < WinglGlobal.fbalm) {
				WinglGlobal.fbal[WinglGlobal.fbalc] = new BalanceDisp();
				WinglGlobal.fbal[WinglGlobal.fbalc].Show();
				WinglGlobal.fbalc = WinglGlobal.fbalc + 1;
			}

		}
		private void Inet1_StateChanged(short State)
		{
			object x = null;
			object Inet1 = null;

			Debug.Print(State);

			switch (State) {

				case 12:
					//UPGRADE_WARNING: Couldn't resolve default property of object Inet1.GetChunk. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					x = Inet1.GetChunk(1024, InetCtlsObjects.DataTypeConstants.icString);
					while (Strings.Len(x) > 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						pg = pg + x;
						//UPGRADE_WARNING: Couldn't resolve default property of object Inet1.GetChunk. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = Inet1.GetChunk(1024, InetCtlsObjects.DataTypeConstants.icString);
					}
					LogError.MsgBox_Renamed(pg);
					break;
			}

		}
		public void frmMain_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object l = null;
			object xl = null;
			object BatchInvoiceChecker = null;

			string xu = null;
			Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString m = new Microsoft.VisualBasic.Compatibility.VB6.FixedLengthString(2444);

			//UPGRADE_WARNING: Couldn't resolve default property of object BatchInvoiceChecker.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (Conversion.Val(WinGL.My.MyProject.Application.Info.Version.Major + "." + WinGL.My.MyProject.Application.Info.Version.Minor) < 1.5)
				BatchInvoiceChecker.Enabled = false;
			WinglGlobal.fbalc = 0;

			xu = Interaction.Command();
			WinglGlobal.UserPWD = new string(" ", 18);

			if (Strings.Len(xu) > 0) {
				WinglGlobal.userid = xu;
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object xl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xl = 64;
				WinglGlobal.userid = Strings.Space(64);
				//UPGRADE_WARNING: Couldn't resolve default property of object xl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object l. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				l = WinGlobal.GetUserName(WinglGlobal.userid, ref xl);
				//UPGRADE_WARNING: Couldn't resolve default property of object xl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				WinglGlobal.userid = Strings.Left(WinglGlobal.userid, xl - 1);
			}

			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Convert.ToSingle(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainLeft", Convert.ToString(1000))));
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Convert.ToSingle(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainTop", Convert.ToString(1000))));
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Convert.ToSingle(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainWidth", Convert.ToString(6500))));
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Convert.ToSingle(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainHeight", Convert.ToString(6500))));
			//UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			this.WindowState = Convert.ToInt16(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "WindowState", Convert.ToString(Constants.vbNormal)));

			WinglGlobal.PMSDataBase = Interaction.GetSetting("WinPM", "Options", "DefaultDataBase", "\\pmsys\\winpm.mdb");
			WinglGlobal.appath = WinGlobal.GetPath(ref WinglGlobal.PMSDataBase);
			WinglGlobal.DefaultDataBase = Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "DefaultDataBase", WinglGlobal.appath + "wingl.mdb");
			WinglGlobal.DefaultDataBase = WinglGlobal.appath + "wingl.mdb";

			// set log file paths
			WinglGlobal.logBatch = WinglGlobal.appath + WinglGlobal.logPath + "batch-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Now, "yyyymmdd") + ".log";
			WinglGlobal.logTrans = WinglGlobal.appath + WinglGlobal.logPath + "trans-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Now, "yyyymmdd") + ".log";
			WinglGlobal.logInvoice = WinglGlobal.appath + WinglGlobal.logPath + "inv-" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Now, "yyyymmdd") + ".log";

			 // ERROR: Not supported in C#: OnErrorStatement


			FileSystem.MkDir((WinglGlobal.appath + WinglGlobal.logPath));

			DAODBEngine_definst.SystemDB = WinglGlobal.appath + "pmsys.mdw";
			DAODBEngine_definst.DefaultUser = "Admin";
			// UserID
			this.Text = this.Text + " userid <" + WinglGlobal.userid + "> - database <" + WinglGlobal.DefaultDataBase + "> <" + WinglGlobal.appath + ">";

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(WinglGlobal.DefaultDataBase, false, false);
			WinglGlobal.PMSdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(WinglGlobal.PMSDataBase);

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.VersionCheck();

			WinglGlobal.tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			WinglGlobal.GLCashAcct = WinglGlobal.tblControl.Fields["GLCashAccount"].Value;

			mnutaskstasks_recurringinvoices.Enabled = false;
			// Check to see if Batch Invoices have been processed
			if (Conversion.Val(WinGL.My.MyProject.Application.Info.Version.Major + "." + WinGL.My.MyProject.Application.Info.Version.Minor) > 1.5) {
				WinglGlobal.BatchCheckDate = WinglGlobal.tblControl.Fields["BatchDate"].Value;
				if (WinglGlobal.BatchCheckDate >= DateAndTime.Today)
					WinglGlobal.BatchInvoiceChecked = true;
				if (WinglGlobal.tblControl.Fields["BatchOn"].Value) {
					mnutaskstasks_recurringinvoices.Enabled = true;
					WinglGlobal.BatchOn = true;
				}
			}

			WinglGlobal.tblControl.Close();
			goto MDIForm_Load_exit;
			MDIForm_Load_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			MDIForm_Load_exit:


		}
		private void frmMain_FormClosing(System.Object eventSender, System.Windows.Forms.FormClosingEventArgs eventArgs)
		{
			bool Cancel = eventArgs.Cancel;
			System.Windows.Forms.CloseReason UnloadMode = eventArgs.CloseReason;
			WinglGlobal.rptCancel = true;
			eventArgs.Cancel = Cancel;
		}
//UPGRADE_WARNING: Event frmMain.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void frmMain_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.MainTop = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Top);
			WinglGlobal.MainLeft = Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Left);

		}
		private void frmMain_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			if (this.WindowState != System.Windows.Forms.FormWindowState.Minimized & this.WindowState != System.Windows.Forms.FormWindowState.Maximized) {
				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainLeft", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Left)));
				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainTop", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Top)));
				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainWidth", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width)));
				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "MainHeight", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height)));
			}
			if (this.WindowState != System.Windows.Forms.FormWindowState.Minimized) {
				Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "WindowState", Convert.ToString(this.WindowState));
			}
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Settings", "DefaultDataBase", WinglGlobal.DefaultDataBase);

			WinglGlobal.PMSdb.Close();
			WinglGlobal.GLdb.Close();
			DAODBEngine_definst.Workspaces(0).Close();
			goto MDIForm_Unload_exit;
			MDIForm_Unload_error:

			LogError.MsgBox_Renamed("", , "WINGL Shutdown");
			 // ERROR: Not supported in C#: ResumeStatement

			MDIForm_Unload_exit:


		}
		public void mnt_balfwddos_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.BalFwdOld.Show();

		}
		public void mnt_correct_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.frmCorrect.Show();

		}
		public void mnt_jrndesign_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.JournalDesign.Show();

		}
		public void mnuHelpUpdates_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object Inet1 = null;

			return;
			//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			pg = "";
			//UPGRADE_WARNING: Couldn't resolve default property of object Inet1.URL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Inet1.URL = WinglGlobal.UpdateFile;
			//UPGRADE_WARNING: Couldn't resolve default property of object Inet1.OpenURL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			pg = Inet1.OpenURL(, InetCtlsObjects.DataTypeConstants.icString);

			//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (Conversion.Val(pg) <= Version) {
				//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				LogError.MsgBox_Renamed("This program is up to date.  Current version is " + pg);
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object pg. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				LogError.MsgBox_Renamed("This program is not up to date.  Current version is " + pg);
			}

		}
		public void mnuHelpWeb_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object frmWeb = null;

			//UPGRADE_WARNING: Couldn't resolve default property of object frmWeb.Show. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			frmWeb.Show();

		}
		public void mnutasksmaint_company_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.maintenanceOption = WinglGlobal.mntCompany;
			My.MyProject.Forms.frmMaintenance.Top = 0;
			My.MyProject.Forms.frmMaintenance.Left = 0;
			My.MyProject.Forms.frmMaintenance.Show();

		}
		public void mnutasksmaint_department_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.maintenanceOption = WinglGlobal.mntDepartment;
			My.MyProject.Forms.frmMaintenance.Top = 0;
			My.MyProject.Forms.frmMaintenance.Left = 0;
			My.MyProject.Forms.frmMaintenance.Show();

		}
		public void mnutaskstasks_invoices_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.InvoiceBatch = false;
			My.MyProject.Forms.Invoices.Top = 0;
			My.MyProject.Forms.Invoices.Left = 0;
			My.MyProject.Forms.Invoices.Show();

		}
		public void mnutaskstasks_invoiceselection_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.InvoiceSelection.Top = 0;
			My.MyProject.Forms.InvoiceSelection.Left = 0;
			My.MyProject.Forms.InvoiceSelection.Show();

		}
		public void mnutaskstasks_journalnames_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.JournalNames.Top = 0;
			My.MyProject.Forms.JournalNames.Left = 0;
			My.MyProject.Forms.JournalNames.Show();

		}
		public void mnutaskstasks_recurringinvoices_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			//If VERSION < 1.6 Then
			//    MsgBox "This function is not yet available."
			//    Exit Sub
			//End If
			//InvoicesBatch = True
			//InvoicesBatch.Top = 0: InvoicesBatch.Left = 0
			//Invoices.Caption = "Recurring Invoices"
			My.MyProject.Forms.InvoicesBatch.Show();

		}
		public void mnutaskstasks_vendors_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			My.MyProject.Forms.Vendors.Top = 0;
			My.MyProject.Forms.Vendors.Left = 0;
			My.MyProject.Forms.Vendors.Show();

		}
		private void tbToolBar_ButtonClick(System.Object eventSender, System.EventArgs eventArgs)
		{
			System.Windows.Forms.ToolStripItem Button = (System.Windows.Forms.ToolStripItem)eventSender;

			 // ERROR: Not supported in C#: OnErrorStatement

			switch (Button.Name) {
				case "Camera":
					mnudatadata_copy_Click(mnudatadata_copy, new System.EventArgs());
					break;
				case "Disks04":
					mnudatadata_compact_Click(mnudatadata_compact, new System.EventArgs());
					break;
				case "Wrench":
					mnudatadata_repair_Click(mnudatadata_repair, new System.EventArgs());
					break;
				case "Exclem":
					mnudatadata_error_Click(mnudatadata_error, new System.EventArgs());
					break;
				case "Secur02a":
					mnudatadata_security_Click(mnudatadata_security, new System.EventArgs());
					break;
				case "Print":
					break;
				//ToDo: Add 'Print' button code.
				//MsgBox "Add 'Print' button code."
				//tbToolBar.Buttons(9).ButtonMenus
				case "Cut":
					mnuEditCut_Click(mnuEditCut, new System.EventArgs());
					break;
				case "Copy":
					mnuEditCopy_Click(mnuEditCopy, new System.EventArgs());
					break;
				case "Paste":
					mnuEditPaste_Click(mnuEditPaste, new System.EventArgs());
					break;
			}

		}
		public void mnuHelpAbout_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			Microsoft.VisualBasic.Compatibility.VB6.Support.ShowForm(frmAbout, Microsoft.VisualBasic.Compatibility.VB6.FormShowConstants.Modal, this);
		}
		public void mnuHelpSearchForHelpOn_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short nRet = 0;

			//if there is no helpfile for this project display a message to the user
			//you can set the HelpFile for your application in the
			//Project Properties dialog
			//UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
			//If Len(App.HelpFile) = 0 Then
			//MsgBox_Renamed("Unable to display Help Contents. There is no Help associated with this project.", MsgBoxStyle.Information, Me.Text)
			//Else
			//On Error Resume Next
			//UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
			//nRet = OSWinHelp(Me.Handle.ToInt32, App.HelpFile, 261, 0)
			//If Err.Number Then
			//MsgBox_Renamed(Err.Description)
			//End If
			//End If

		}
		public void mnuHelpContents_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short nRet = 0;

			//if there is no helpfile for this project display a message to the user
			//you can set the HelpFile for your application in the
			//Project Properties dialog
			//UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
			//Len(App.HelpFile) = 0 Then
			if (0) {
				LogError.MsgBox_Renamed("Unable to display Help Contents. There is no Help associated with this project.", MsgBoxStyle.Information, this.Text);
			} else {
				 // ERROR: Not supported in C#: OnErrorStatement

				//UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
				//nRet = OSWinHelp(Me.Handle.ToInt32, App.HelpFile, 3, 0)
				if (Err().Number) {
					LogError.MsgBox_Renamed(Err().Description);
				}
			}

		}
		public void mnuWindowTileVertical_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
		}
		public void mnuWindowTileHorizontal_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
		}
		public void mnuWindowCascade_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}
		public void mnutaskstasks_payable_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (LogError.MsgBox_Renamed("Post From Accounts Payable to General Ledger?", MsgBoxStyle.YesNo, "Post From Accounts Payable") == MsgBoxResult.Yes) {
				WinglGlobal.PostAccountsPayable();
			}

		}
		public void mnutaskstasks_receivable_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (LogError.MsgBox_Renamed("Post From Accounts Receivable to General Ledger?", MsgBoxStyle.YesNo, "Post From Accounts Rreceivable") == MsgBoxResult.Yes) {
				WinglGlobal.PostTenantReceivables();
			}

		}
		public void mnutaskstasks_batch_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			My.MyProject.Forms.BatchJrnEdit.Show();
		}
		public void mnutaskstasks_manual_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.GLentry.Show();

		}
		public void mnutaskstasks_company_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			My.MyProject.Forms.CompanyDepartment.Top = 0;
			My.MyProject.Forms.CompanyDepartment.Left = 0;
			My.MyProject.Forms.CompanyDepartment.Show();
		}
		public void mnutaskstasks_categories_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.Categories.Show();

		}
		public void mnuViewOptions_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			My.MyProject.Forms.frmOptions.ShowDialog();
			if (WinglGlobal.BatchOn) {
				mnutaskstasks_recurringinvoices.Enabled = true;
			} else {
				mnutaskstasks_recurringinvoices.Enabled = false;
			}
		}
		public void mnuViewStatusBar_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			mnuViewStatusBar.Checked = !mnuViewStatusBar.Checked;
			sbStatusBar.Visible = mnuViewStatusBar.Checked;
		}
		public void mnuViewToolbar_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			mnuViewToolbar.Checked = !mnuViewToolbar.Checked;
			tbToolBar.Visible = mnuViewToolbar.Checked;
		}
		public void mnuEditPaste_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_ISSUE: Control rtfText could not be resolved because it was within the generic namespace ActiveMDIChild. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//ActiveMDIChild.rtfText.SelRTF = My.Computer.Clipboard.GetText
		}
		public void mnuEditCopy_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_ISSUE: Control rtfText could not be resolved because it was within the generic namespace ActiveMDIChild. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//My.Computer.Clipboard.SetText(ActiveMDIChild.rtfText.SelRTF)
		}
		public void mnuEditCut_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_ISSUE: Control rtfText could not be resolved because it was within the generic namespace ActiveMDIChild. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//My.Computer.Clipboard.SetText(ActiveMDIChild.rtfText.SelRTF)
			//UPGRADE_ISSUE: Control rtfText could not be resolved because it was within the generic namespace ActiveMDIChild. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
			//ActiveMDIChild.rtfText.SelText = vbNullString
		}
		public void mnudatadata_exit_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			this.Close();
			System.Environment.Exit(0);
		}
		public void mnudatadata_security_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			object j = null;
			object i = null;
			object database_security = null;
			int ret = 0;

			string p = null;
			PROCESS_INFORMATION proc = default(PROCESS_INFORMATION);
			STARTUPINFO start = default(STARTUPINFO);

			if (Strings.InStr(Strings.LCase(WinGL.My.MyProject.Application.Info.DirectoryPath), "source") > 0) {
				p = "\\pmsys\\winpmadmin.exe";
			} else {
				p = WinGL.My.MyProject.Application.Info.DirectoryPath + "\\winpmadmin.exe";
			}

			WinglGlobal.GLdb.Close();
			//Initialize the STARTUPINFO structure:
			start.cb = Strings.Len(start);
			// Start the shelled application:
			ret = WinGlobal.CreateProcess(0, p, 0, 0, 1, WinGlobal.NORMAL_PRIORITY_CLASS, 0, 0, ref start, ref proc);
			// Wait for the shelled application to finish:
			ret = WinGlobal.WaitForSingleObject(proc.hProcess, WinGlobal.INFINITE);
			ret = WinGlobal.CloseHandle(proc.hProcess);

			//UPGRADE_WARNING: Couldn't resolve default property of object database_security.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			database_security.Enabled = false;
			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.UserGrp = "";
			for (i = 0; i <= DAODBEngine_definst.Workspaces(0).Users.Count - 1; i++) {
				if (DAODBEngine_definst.Workspaces(0).Users(i).Name == WinglGlobal.userid) {
					for (j = 0; j <= DAODBEngine_definst.Workspaces(0).Users(i).Groups.Count - 1; j++) {
						if (DAODBEngine_definst.Workspaces(0).Users(i).Groups(j).Name == "Admins") {
							WinglGlobal.UserGrp = "Admins";
						}
					}
				}
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object database_security.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			if (WinglGlobal.UserGrp == "Admins")
				database_security.Enabled = true;
			WinglGlobal.GLdb = DAODBEngine_definst.OpenDatabase(WinglGlobal.DefaultDataBase, false, false);
			goto databasesec_exit;
			databasesec_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			databasesec_exit:


		}
		public void mnudatadata_error_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (Strings.InStr(Strings.LCase(WinGL.My.MyProject.Application.Info.DirectoryPath), "source") > 0) {
				Interaction.Shell("\\pmsys\\ErrorLogViewer.exe", AppWinStyle.NormalFocus);
			} else {
				Interaction.Shell(WinGL.My.MyProject.Application.Info.DirectoryPath + "\\ErrorLogViewer.exe", AppWinStyle.NormalFocus);
			}

		}
		public void mnudatadata_repair_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			string toFile = null;
			string fromFile = null;
			string x = null;

			if (LogError.MsgBox_Renamed("You are about to REPAIR a database.  Do you want to continue?  (If you answer 'Yes' be sure that you have a backup copy of the Database.)", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "REPAIR DATABASE") == MsgBoxResult.No) {
				goto databaserepair_exit2;
			}

			WinglGlobal.GLdb.Close();
			databaserepair_from:

			dlgCommonDialogOpen.Title = "Repair Database";
			dlgCommonDialogOpen.DefaultExt = ".MDB";
			//UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dlgCommonDialogOpen.Filter = "Databases (*.mdb) | *.mdb";
			dlgCommonDialogOpen.FileName = WinglGlobal.DefaultDataBase;
			//UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			//dlgCommonDialog.CancelError = True
			 // ERROR: Not supported in C#: OnErrorStatement

			dlgCommonDialogOpen.ShowDialog();
			fromFile = dlgCommonDialogOpen.FileName;

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			DAODBEngine_definst.RepairDatabase(fromFile);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			LogError.MsgBox_Renamed("Database successfully Repaired.  You should now verify that the database is accessible.", MsgBoxStyle.Critical, "REPAIR DATABASE");
			goto databaserepair_exit;
			databaserepair_cancel:

			 // ERROR: Not supported in C#: ResumeStatement

			databaserepair_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "REPAIR DATABASE");
			 // ERROR: Not supported in C#: ResumeStatement

			databaserepair_exit:

			WinglGlobal.GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(WinglGlobal.DefaultDataBase, false, false);
			databaserepair_exit2:

		}
		public void mnudatadata_compact_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			string toFile = null;
			string fromFile = null;
			string x = null;

			if (LogError.MsgBox_Renamed("You are about to COMPACT a database.  Do you want to continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "COMPACT DATABASE") == MsgBoxResult.No) {
				goto databasecompact_exit2;
			}

			WinglGlobal.GLdb.Close();
			databasecompact_from:

			dlgCommonDialogOpen.Title = "Compact Database";
			dlgCommonDialogOpen.DefaultExt = ".MDB";
			//UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dlgCommonDialogOpen.Filter = "Databases (*.mdb) | *.mdb";
			dlgCommonDialogOpen.FileName = WinglGlobal.DefaultDataBase;
			//UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			//dlgCommonDialog.CancelError = True
			 // ERROR: Not supported in C#: OnErrorStatement

			dlgCommonDialogOpen.ShowDialog();
			fromFile = dlgCommonDialogOpen.FileName;

			toFile = WinGlobal.GetPath(ref fromFile) + "new_" + WinGlobal.GetFileName(ref fromFile);

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			DAODBEngine_definst.CompactDatabase(fromFile, toFile);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			if (LogError.MsgBox_Renamed("Database successfully Compacted.  Do you want to keep a copy of the old database?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "COMPACT DATABASE") == MsgBoxResult.Yes) {
				if (LogError.MsgBox_Renamed("Old Database will be renamed to: old_" + WinGlobal.GetFileName(ref fromFile) + ".  Pressing CANCEL will remove the old Database.", MsgBoxStyle.OkCancel + MsgBoxStyle.Critical, "COMPACT DATABASE") == MsgBoxResult.Ok) {
					x = WinGlobal.GetPath(ref fromFile) + "old_" + WinGlobal.GetFileName(ref fromFile);
					FileSystem.Rename(fromFile, x);
				} else {
					FileSystem.Kill(fromFile);
				}
			} else {
				FileSystem.Kill(fromFile);
			}
			FileSystem.Rename(toFile, fromFile);
			goto databasecompact_exit;
			databasecompact_cancel:

			 // ERROR: Not supported in C#: ResumeStatement

			databasecompact_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "COMPACT DATABASE");
			 // ERROR: Not supported in C#: ResumeStatement

			databasecompact_exit:

			WinglGlobal.GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(WinglGlobal.DefaultDataBase, false, false);
			databasecompact_exit2:

		}
		public void mnudatadata_copy_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			string fromFile = null;
			string toFile = null;

			if (LogError.MsgBox_Renamed("You are about to COPY a database.  Do you want to continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "COPY DATABASE") == MsgBoxResult.No) {
				goto databasecopy_exit2;
			}

			WinglGlobal.GLdb.Close();
			databasecopy_from:

			dlgCommonDialogOpen.Title = "Copy FROM";
			dlgCommonDialogOpen.DefaultExt = ".MDB";
			//UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dlgCommonDialogOpen.Filter = "Databases (*.mdb) | *.mdb";
			dlgCommonDialogOpen.FileName = WinglGlobal.DefaultDataBase;
			//UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			//dlgCommonDialog.CancelError = True
			 // ERROR: Not supported in C#: OnErrorStatement

			dlgCommonDialogOpen.ShowDialog();
			fromFile = dlgCommonDialogOpen.FileName;
			databasecopy_to:

			dlgCommonDialogOpen.Title = "Copy TO";
			dlgCommonDialogOpen.DefaultExt = ".MDB";
			//UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			dlgCommonDialogOpen.Filter = "Databases (*.mdb) | *.mdb";
			dlgCommonDialogOpen.FileName = "";
			//UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			//dlgCommonDialog.CancelError = True
			 // ERROR: Not supported in C#: OnErrorStatement

			dlgCommonDialogOpen.ShowDialog();
			toFile = dlgCommonDialogOpen.FileName;

			if (Strings.Trim(fromFile) == Strings.Trim(toFile)) {
				if (LogError.MsgBox_Renamed("FROM file cannot be the same as the TO file.  Do you want to try again?", MsgBoxStyle.YesNo, "COPY DATABASE") == MsgBoxResult.Yes) {
					goto databasecopy_from;
				} else {
					goto databasecopy_exit;
				}
			}

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			FileSystem.FileCopy(fromFile, toFile);
			this.Cursor = System.Windows.Forms.Cursors.Default;

			goto databasecopy_exit;
			databasecopy_cancel:

			 // ERROR: Not supported in C#: ResumeStatement

			databasecopy_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "COPY DATABASE");
			 // ERROR: Not supported in C#: ResumeStatement

			databasecopy_exit:

			WinglGlobal.GLdb = DAODBEngine_definst.Workspaces(0).OpenDatabase(WinglGlobal.DefaultDataBase, false, false);
			databasecopy_exit2:

		}
		public void tbToolBar_ButtonMenuClick(System.Object eventSender, System.EventArgs eventArgs)
		{
			System.Windows.Forms.ToolStripItem ButtonMenu = (System.Windows.Forms.ToolStripMenuItem)eventSender;

			object AccountMaster = null;

			WinglGlobal.rptCancel = false;

			//UPGRADE_ISSUE: MSComctlLib.ButtonMenu property ButtonMenu.key was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			switch (0) {
				//ButtonMenu.key

				case "rpt_acct":
					xrpt = new clsAccountMaster();

					break;
				case "rpt_vendors":
					xrpt = new clsVendorList();

					break;
				case "rpt_company":
					xrpt = new clsCompany();

					break;
				case "rpt_dist":
					xrpt = new clsInvoiceSelection();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.Mode. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.Mode = "A";
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.InvoiceSelection. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.InvoiceSelection();
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_paylist":
					xrpt = new clsInvoiceSelection();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.InvoiceSelection. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.InvoiceSelection();
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_checkreg":
					xrpt = new clsCheckRegister();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.CheckRegister. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.CheckRegister(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_editlist":
					xrpt = new clsTransEditList();
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_ledger":
					xrpt = new clsLedger();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.Ledger. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.Ledger(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_balance":
					xrpt = new clsBalanceSheet();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.BalanceSheet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.BalanceSheet(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_disbrpt":
					xrpt = new clsDisbRpt();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.DisbRpt. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.DisbRpt(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_opstmt":
					xrpt = new clsOperatingStatement();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.OperatingStatement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.OperatingStatement(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_payroll":
					xrpt = new clsPayrollReport();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.PayrollReport. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.PayrollReport();
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_journal":
					xrpt = new clsJournals();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.Journals. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.Journals(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_trial":
					xrpt = new clsTrialBalance();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.TrialBalance. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.TrialBalance(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
				case "rpt_batch":
					My.MyProject.Forms.frmBatch.Show();

					break;
				case "rpt_f1099":
					xrpt = new clsF1099();
					//UPGRADE_WARNING: Couldn't resolve default property of object xrpt.F1099. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					xrpt.F1099(false);
					//UPGRADE_NOTE: Object xrpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					xrpt = null;

					break;
			}

		}
		public void yearend_balances_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.BalanceFwd = true;
			My.MyProject.Forms.CloseYear.Show();

		}
		public void yearend_gl_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.BalanceFwd = false;
			My.MyProject.Forms.CloseYear.Show();

		}
		public void yearend_vendor_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.VendorClose();

		}
	}
}
