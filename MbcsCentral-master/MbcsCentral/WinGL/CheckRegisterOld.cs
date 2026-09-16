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
	internal partial class CheckRegister : System.Windows.Forms.Form
	{
		DAO.Recordset tblVend;
		DAO.Recordset tblChecks;
		DAO.Recordset tblComp;
		DAO.Recordset tblDept;
		DAO.Recordset tblTrans;
		int CashAcct;
		DAO.Recordset tblHist;
		short cmp;
		short dpt;
		int chk;
		int vnd;
		int c;
		int d;
		double a;
		bool scan;
		Register_type org;
		bool FormLoading;
		short curYear;

//******************************************************************************
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);
			object xsql = null;
			//******************************************************************************

			WinglGlobal.rptCancel = true;
			switch (Index) {

				case 0:
					// Save
					if (Conversion.Val(txtTotal.CtlText) != Conversion.Val(txtNet.Text)) {
						if (LogError.MsgBox_Renamed("Check net amount has changed.  Do you still want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Check Register") == MsgBoxResult.No) {
							return;
						}
					}
					SaveCheck();
					this.Close();
					break;

				case 1:
					// DELETE
					if (LogError.MsgBox_Renamed("Delete this check?", MsgBoxStyle.YesNo, "Check Register") == MsgBoxResult.Yes) {
						DAODBEngine_definst.Workspaces(0).BeginTrans();
						xsql = "DELETE * FROM APChecks WHERE Company = " + cmp + " AND Department = " + dpt + " AND CheckNumber = " + chk;
						WinglGlobal.GLdb.Execute(xsql, DAO.RecordsetOptionEnum.dbFailOnError);
						xsql = "DELETE * FROM APTrans WHERE Date = #" + dtCheck.Value + "# AND CheckNumber = " + chk;
						WinglGlobal.GLdb.Execute(xsql, DAO.RecordsetOptionEnum.dbFailOnError);
						xsql = "DELETE * FROM Transactions WHERE CompanyNumber = " + cmp + " AND DepartmentNumber = " + dpt + " AND CheckNumber = " + chk;
						WinglGlobal.GLdb.Execute(xsql, DAO.RecordsetOptionEnum.dbFailOnError);
						DAODBEngine_definst.Workspaces(0).CommitTrans();
						WinglGlobal.deleteDocuments(ref chk, ref vnd, ref cmp, ref dpt);
						this.Close();
					}
					break;

				case 2:
					// Cancel
					this.Close();
					break;

			}

		}

		private void cmdDoc_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = cmdDoc.GetIndex(eventSender);

			string docIDs = null;
			short i = 0;
			switch (Index) {

				case 0:
					// Scan
					WinglGlobal.invpGlobal.amountpaid = org.amt;
					WinglGlobal.invpGlobal.Company = org.comp;
					WinglGlobal.invpGlobal.Department = org.dept;
					WinglGlobal.invpGlobal.vendor = Convert.ToInt32(Strings.Trim(Convert.ToString(org.vend)));
					WinglGlobal.invpGlobal.CheckDate = org.date_Renamed;
					WinglGlobal.invpGlobal.CheckNumber = org.chk;
					scanCheck();
					cmdDoc[0].Visible = false;
					break;

				case 1:
					// Display
					 // ERROR: Not supported in C#: OnErrorStatement

					WinglGlobal.rsd.MoveFirst();
					while (!WinglGlobal.rsd.EOF) {
						docIDs = docIDs + WinglGlobal.rsd("document_id") + " ";
						WinglGlobal.rsd.MoveNext();
					}
					Interaction.Shell(WinglGlobal.dbname + "WindocViewer.exe " + docIDs);
					break;

			}

		}

//******************************************************************************
		private void CheckRegister_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{
			object total = null;
			object i = null;
			object xsql = null;
			object x = null;
			//******************************************************************************

			grdCheck.ReDraw = false;
			grdCheck.Visible = false;
			x = this.Tag;
			cmp = Conversion.Val(Strings.Mid(x, 7, 3));
			dpt = Conversion.Val(Strings.Mid(x, 11, 3));
			chk = Conversion.Val(Strings.Left(x, 6));
			xsql = "SELECT * FROM APChecks WHERE Company = " + Strings.Mid(x, 7, 3) + " AND Department = " + Strings.Mid(x, 11, 3) + " AND CheckNumber = " + Strings.Left(x, 6) + " ORDER BY Sequence";
			tblChecks = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblChecks.RecordCount == 0) {
				LogError.MsgBox_Renamed("No Check Records: " + x, , "Check Register");
				this.Close();
				return;
			}
			if (tblChecks.Fields["Sequence"].Value != 0) {
				LogError.MsgBox_Renamed("No Header Record for " + this.Tag, MsgBoxStyle.Critical, "Check Register");
				this.Close();
				return;
			}
			vnd = tblChecks.Fields["Vendor/Account"].Value;
			tblComp = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Company WHERE CompanyNumber = " + Strings.Mid(x, 7, 3));
			if (tblComp.RecordCount > 0) {
				Label1[2].Text = tblComp.Fields["Name"].Value;
			} else {
				Label1[2].Text = "* NOT ON FILE *";
			}
			tblComp.Close();
			tblDept = WinglGlobal.GLdb.OpenRecordset("SELECT Name FROM Department WHERE [Department Number] = " + Strings.Mid(x, 11, 3));
			if (tblDept.RecordCount > 0) {
				Label1[3].Text = tblDept.Fields["Name"].Value;
			} else {
				Label1[3].Text = "* NOT ON FILE *";
			}
			tblDept.Close();
			txtCheck.CtlText = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblChecks.Fields["CheckNumber"].Value, "#####0");
			lstVendors.ColumnSearch = 1;
			lstVendors.SearchText = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tblChecks.Fields["Vendor/Account"].Value, "#####");
			lstVendors.Action = LpLib.enumAction.ActionSearch;
			if (lstVendors.SearchIndex > -1) {
				lstVendors.ListIndex = lstVendors.SearchIndex;
				WinglGlobal.vName = Strings.Left(lstVendors.CtlText, Strings.InStr(lstVendors.CtlText, "[") - 1);
			} else {
				lstVendors.ListIndex = -1;
			}
			dtCheck.Value = tblChecks.Fields["Date"];
			chkRecon.CheckState = (tblChecks.Fields["ReconFlag"].Value ? 1 : 0);

			org.comp = Conversion.Val(Strings.Mid(x, 7, 3));
			org.dept = Conversion.Val(Strings.Mid(x, 11, 4));
			org.chk = Conversion.Val(Strings.Left(x, 6));
			org.date_Renamed = tblChecks.Fields["Date"].Value;
			org.vend = Convert.ToInt32(Strings.Trim(tblChecks.Fields["Vendor/Account"].Value));
			org.amt = tblChecks.Fields["Amount"].Value;

			grdCheck.Col = 2;
			grdCheck.Row = 1;
			grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
			i = 0;
			total = 0;
			tblChecks.MoveNext();
			while (!tblChecks.EOF) {
				i = i + 1;
				if (i > grdCheck.MaxRows) {
					LogError.MsgBox_Renamed("Too many detail records.  The rest are ignored.", , "Check Register");
					break; // TODO: might not be correct. Was : Exit Do
				}

				// Company List
				lstCompanies.SearchText = tblChecks.Fields["DistCompany"].Value;
				lstCompanies.ColumnSearch = 1;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				if (lstCompanies.SearchIndex > -1) {
					lstCompanies.ListIndex = lstCompanies.SearchIndex;
					lstCompanies.Col = 0;
					lstCompanies.Row = lstCompanies.SearchIndex;
					grdCheck.Col = 2;
					grdCheck.Row = i;
					grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
					grdCheck.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
					grdCheck.TypeComboBoxhWnd = lstCompanies.hWnd;
					grdCheck.TypeComboBoxEditable = true;
					grdCheck.Value = Convert.ToString(lstCompanies.SearchIndex);
					grdCheck.Text = lstCompanies.ColList;
					grdCheck.TypeComboBoxEditable = false;
				}

				// Department List
				WinglGlobal.GetDepartments(ref this, ref tblChecks.Fields["DistCompany"].Value);
				lstDepartments.SearchText = tblChecks.Fields["DistDepartment"].Value;
				lstDepartments.ColumnSearch = 1;
				lstDepartments.Action = LpLib.enumAction.ActionSearch;
				if (lstDepartments.SearchIndex > -1) {
					lstDepartments.ListIndex = lstDepartments.SearchIndex;
					lstDepartments.Row = lstDepartments.SearchIndex;
				} else {
					lstDepartments.ListIndex = 0;
				}
				grdCheck.Col = 3;
				grdCheck.Row = i;
				grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
				grdCheck.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
				grdCheck.TypeComboBoxhWnd = lstDepartments.hWnd;
				lstDepartments.Col = 0;
				grdCheck.TypeComboBoxEditable = true;
				grdCheck.SetText(3, i, lstDepartments.ColList);
				grdCheck.TypeComboBoxEditable = false;

				// Account List
				grdCheck.Col = 4;
				grdCheck.Row = i;
				grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
				lstAccounts.SearchText = tblChecks.Fields["Vendor/Account"].Value;
				lstAccounts.ColumnSearch = 1;
				lstAccounts.Action = LpLib.enumAction.ActionSearch;
				if (lstAccounts.SearchIndex > -1) {
					grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
					grdCheck.TypeComboBoxIndex = lstAccounts.SearchIndex;
					lstAccounts.ListIndex = lstAccounts.SearchIndex;
					grdCheck.SetText(4, i, lstAccounts.CtlText);
				}
				grdCheck.Col = 5;
				grdCheck.Row = i;
				grdCheck.SetFloat(5, i, tblChecks.Fields["Amount"].Value);

				org.details(i).AccountNumber = tblChecks.Fields["Vendor/Account"].Value;
				org.details(i).amount = tblChecks.Fields["Amount"].Value;
				org.details(i).CheckNumber = tblChecks.Fields["CheckNumber"].Value;
				org.details(i).CompanyNumber = tblChecks.Fields["DistCompany"].Value;
				org.details(i).date_Renamed = tblChecks.Fields["Date"].Value;
				org.details(i).DepartmentNumber = tblChecks.Fields["DistDepartment"].Value;
				xsql = "SELECT IndexNumber FROM Transactions WHERE CompanyNumber = " + tblChecks.Fields["DistCompany"].Value;
				xsql = xsql + " AND DepartmentNumber = " + tblChecks.Fields["DistDepartment"].Value + " AND AccountNumber = ";
				xsql = xsql + tblChecks.Fields["Vendor/Account"].Value + " AND CheckNumber = " + Conversion.Val(Strings.Left(x, 6)) + " AND Amount = " + tblChecks.Fields["Amount"].Value;
				tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblTrans.RecordCount > 0) {
					org.details(i).IndexNumber = tblTrans.Fields["IndexNumber"].Value;
					grdCheck.Col = 6;
					grdCheck.Value = tblTrans.Fields["IndexNumber"].Value;
				} else {
					org.details(i).IndexNumber = 0;
					grdCheck.Col = 6;
					grdCheck.Value = Convert.ToString(0);
				}
				tblTrans.Close();

				total = total + tblChecks.Fields["Amount"].Value;

				tblChecks.MoveNext();
			}
			txtNet.Text = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#########0.00"), ref 13);
			txtTotal.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#########0.00"), ref 13);
			lstAccounts.ListIndex = 0;
			grdCheck.Col = 2;
			grdCheck.Row = 1;
			grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
			grdCheck.Visible = true;
			grdCheck.ReDraw = true;
			if (scan) {
				WinglGlobal.invpGlobal.date_Renamed = org.date_Renamed;
				xsql = "SELECT DISTINCT InvoiceNumber FROM InvoiceHistory WHERE CheckNumber = " + org.chk + " AND VendorNumber = " + org.vend;
				xsql = xsql + " AND Company = " + org.comp + " AND Department = " + org.dept;
				tblHist = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblHist.RecordCount > 0) {
					WinglGlobal.rsd = new ADODB.Recordset();
					 // ERROR: Not supported in C#: OnErrorStatement

					//invpGlobal.invnumber = Trim(tblHist("InvoiceNumber"))
					//invpGlobal.date = tblHist("Date")
					tblHist.MoveFirst();
					if (!tblHist.EOF) {
						WinglGlobal.invpGlobal.invnumber = tblHist.Fields["InvoiceNumber"].Value;
					}
					xsql = "SELECT * FROM Documents WHERE Vendor = '" + vnd + "' AND Invoice IN ('" + Strings.Trim(tblHist.Fields["InvoiceNumber"].Value) + "'";
					while (!tblHist.EOF) {
						tblHist.MoveNext();
						if (!tblHist.EOF) {
							xsql = xsql + ", '" + Strings.Trim(tblHist.Fields["InvoiceNumber"].Value) + "'";
						}
					}
					xsql = xsql + ")";
					Err().Clear();
					WinglGlobal.rsd.Open(xsql, WinglGlobal.cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic);
					WinglGlobal.rsd.MoveFirst();
					if (Err().Number == 0) {
						cmdDoc[1].Visible = true;
					} else {
						Err().Clear();
						WinglGlobal.rss = new ADODB.Recordset();
						WinglGlobal.rss.Open("SELECT * FROM interface", WinglGlobal.cnn.cn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic);
						if (Err().Number != 0) {
							//MsgBox "Error openning 'interface' table (invoices will not be scanned): " & Err.Description
							scan = false;
							WinglGlobal.rss.Close();
							WinglGlobal.cnn.CloseDB();
						} else {
							cmdDoc[0].Visible = true;
						}
					}
				}
				tblHist.Close();
			}
			FormLoading = false;

		}

		private void CheckRegister_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			WinglGlobal.rsd.Close();

		}

//******************************************************************************
		private void grdCheck_ButtonClicked(System.Object eventSender, AxFPSpread._DSpreadEvents_ButtonClickedEvent eventArgs)
		{
			object i = null;
			//******************************************************************************

			//Col = 1 Then
			if (0) {
				//grdCheck.Col = Col
				//grdCheck.Row = Row
				if (Convert.ToBoolean(grdCheck.Value)) {
					for (i = 2; i <= 5; i++) {
						grdCheck.Col = i;
						grdCheck.Lock = true;
					}
				} else {
					for (i = 2; i <= 5; i++) {
						grdCheck.Col = i;
						grdCheck.Lock = false;
					}
				}
			}
			grdCheck_LeaveCell(grdCheck, new AxFPSpread._DSpreadEvents_LeaveCellEvent(5, 1, 1, 2, false));

		}
//******************************************************************************
		private void CheckRegister_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object rc = null;
			object r = null;
			//******************************************************************************

			short i = 0;
			string x = null;
			string xsql = null;

			FormLoading = true;
			this.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(WinglGlobal.MainTop + 1050);
			this.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(WinglGlobal.MainLeft + 200);

			grdCheck.LockBackColor = System.Drawing.ColorTranslator.FromOle(0xc0c0c0);

			grdCheck.LeftCol = 0;
			WinglGlobal.glPost = false;

			WinglGlobal.tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			CashAcct = (Information.IsDBNull(WinglGlobal.tblControl.Fields["GLCashAccount"].Value) ? -1 : WinglGlobal.tblControl.Fields["GLCashAccount"].Value);
			if (CashAcct == -1) {
				Interaction.MsgBox("Cannot edit checks without a valid Cash Account.");
				this.Close();
			}
			curYear = (Information.IsDBNull(WinglGlobal.tblControl.Fields["CurrentFiscalYear"].Value) ? DateAndTime.Year(DateAndTime.Today) : WinglGlobal.tblControl.Fields["CurrentFiscalYear"].Value);
			if (curYear == 0)
				curYear = DateAndTime.Year(DateAndTime.Today);
			WinglGlobal.tblControl.Close();

			WinglGlobal.GetCompanies(ref this);
			grdCheck.Col = 2;
			for (r = 0; r <= grdCheck.MaxRows - 1; r++) {
				grdCheck.Row = r;
				grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
				grdCheck.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
				grdCheck.TypeComboBoxhWnd = lstCompanies.hWnd;
			}
			lstCompanies.Visible = false;
			WinglGlobal.getAccounts(lstAccounts);
			grdCheck.Col = 4;
			for (r = 0; r <= grdCheck.MaxRows - 1; r++) {
				grdCheck.Row = r;
				grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
				grdCheck.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
				grdCheck.TypeComboBoxhWnd = lstAccounts.hWnd;
			}
			lstAccounts.Visible = false;
			WinglGlobal.GetVendors(this);
			scan = Conversion.Val(Interaction.GetSetting("Wingl", "Options", "Windoc", "0"));
			if (scan) {
				WinglGlobal.dbname = Interaction.GetSetting("Wingl", "Options", "WindocFile", "");
				if (!string.IsNullOrEmpty(Strings.Trim(WinglGlobal.dbname))) {
					WinglGlobal.cnn = new clsConnection();
					rc = WinglGlobal.cnn.OpenDB("admin", WinglGlobal.dbname, ADODB.ConnectModeEnum.adModeShareDenyNone);
					if (rc != 0) {
						//MsgBox "Error openning Windoc Database (" & dbname & ") Invoices will not be scanned: " & Err.Description
						scan = false;
					}
					for (i = Strings.Len(WinglGlobal.dbname); i >= 1; i += -1) {
						if (Strings.Right(WinglGlobal.dbname, 1) == "\\")
							break; // TODO: might not be correct. Was : Exit For
						WinglGlobal.dbname = Strings.Left(WinglGlobal.dbname, i - 1);
					}
				} else {
					//MsgBox "Windoc Database not specified. Invoices will not be scanned."
					scan = false;
				}
			}

		}
//******************************************************************************
		private void CheckRegister_Resize(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			 // ERROR: Not supported in C#: OnErrorStatement

			for (i = 0; i <= Cmd.Count() - 1; i++) {
				Cmd[i].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(Cmd[i].Height) - 650);
			}

			grdCheck.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Height) - 2550);
			grdCheck.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX((Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) <= 11700 ? Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Width) - 165 : 11550));
			Label2.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Height) + 10);
			Label1[13].Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Height) + 50);
			txtTotal.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Top) + Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(grdCheck.Height) + 10);

		}
//******************************************************************************
		private void grdCheck_ComboCloseUp(System.Object eventSender, AxFPSpread._DSpreadEvents_ComboCloseUpEvent eventArgs)
		{
			//******************************************************************************

			short i = 0;

			//If Col <> 2 Then Exit Sub
			i = lstCompanies.ListIndex;
			if (i < 0)
				return;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;

			// Department List
			WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
			lstDepartments.ListIndex = 0;
			//grdCheck.Col = 3
			//grdCheck.Row = Row
			//grdCheck.Action = ActionActiveCell
			//grdCheck.TypeComboBoxhWnd = lstDepartments.hwnd
			//grdCheck.TypeComboBoxCurSel = 0
			if (eventArgs.selChange != -1) {
				lstDepartments.Col = 0;
				lstDepartments.Row = 0;
				//grdCheck.SetText(3, Row, lstDepartments.ColList)
			}

		}
//******************************************************************************
		private void grdCheck_LeaveCell(System.Object eventSender, AxFPSpread._DSpreadEvents_LeaveCellEvent eventArgs)
		{
			object x = null;
			object i = null;
			object total = null;
			//******************************************************************************

			total = 0;
			for (i = 1; i <= grdCheck.MaxRows; i++) {
				grdCheck.Row = i;
				grdCheck.Col = 1;
				if (grdCheck.Value == "0") {
					grdCheck.Col = 5;
					total = total + Conversion.Val(grdCheck.Value);
				}
			}
			txtTotal.CtlText = WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#######0.00"), ref 13);
			//If Col = 3 Then
			//lstDepartments.Row = lstDepartments.ListIndex
			//lstDepartments.Col = 0
			//grdCheck.Col = 3 : grdCheck.Row = Row
			//grdCheck.Text = lstDepartments.ColList
			//End If
			if (eventArgs.newCol == 3) {
				grdCheck.Col = 3;
				grdCheck.Row = eventArgs.newRow;
				x = grdCheck.Text;
				grdCheck.Col = 2;
				lstCompanies.ColumnSearch = 0;
				lstCompanies.SearchText = grdCheck.Text;
				lstCompanies.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch;
				lstCompanies.Action = LpLib.enumAction.ActionSearch;
				lstCompanies.ListIndex = lstCompanies.SearchIndex;
				grdCheck_ComboCloseUp(grdCheck, new AxFPSpread._DSpreadEvents_ComboCloseUpEvent(2, eventArgs.newRow, -1));
				grdCheck.SetText(3, eventArgs.newRow, x);
			}
			grdCheck.Row = eventArgs.newRow;
			grdCheck.Col = eventArgs.newCol;

		}

//******************************************************************************
		private void lstCompanies_ClickEvent(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;
			//******************************************************************************

			i = lstCompanies.ListIndex;
			if (i < 0)
				return;
			lstCompanies.Row = i;
			lstCompanies.Col = 1;

			// Department List
			WinglGlobal.GetDepartments(ref this, ref Convert.ToInt32(lstCompanies.ColList));
			grdCheck.Col = 3;
			//For r = 0 To grdCheck.MAXROWS - 1
			//grdCheck.Row = i
			grdCheck.Action = FPSpread.ActionConstants.ActionActiveCell;
			grdCheck.CellType = FPSpread.CellTypeConstants.CellTypeComboBox;
			grdCheck.TypeComboBoxhWnd = lstDepartments.hWnd;
			//Next

		}
//******************************************************************************
		private void SaveCheck()
		{
			//******************************************************************************

			int CashAcct = 0;
			string xsql = null;
			short t = 0;
			short i = 0;
			short j = 0;
			short seq = 0;
			DAO.Recordset tblTransTemp = null;
			decimal diff = default(decimal);
			bool found = false;
			bool CashPosted = false;
			string vName = null;
			test_type tc = default(test_type);
			DAO.Recordset tbl = null;
			short m = 0;

			found = false;
			xsql = "SELECT * FROM TransactionTemp WHERE CheckNumber = " + chk + " AND CompanyNumber = " + cmp;
			xsql = xsql + " AND DepartmentNumber = " + dpt;
			tblTransTemp = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblTransTemp.RecordCount > 0) {
				found = true;
				tblTransTemp.Close();
			} else {
				xsql = "SELECT * FROM APTrans WHERE CheckNumber = " + chk + " AND Company = " + cmp;
				xsql = xsql + " AND Department = " + dpt;
				tblTransTemp = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tblTransTemp.RecordCount > 0) {
					found = true;
					tblTransTemp.Close();
				}
			}
			if (found) {
				Interaction.MsgBox("This check cannot be changed. First post G/L transactions.");
				return;
			}

			 // ERROR: Not supported in C#: OnErrorStatement

			tblTransTemp.Close();
			tblChecks.Close();
			tblVend.Close();
			tblComp.Close();
			tblDept.Close();

			 // ERROR: Not supported in C#: OnErrorStatement

			lstVendors.Row = lstVendors.ListIndex;
			lstVendors.Col = 1;
			vnd = Conversion.Val(lstVendors.ColList);
			lstVendors.Col = 0;
			i = Strings.InStr(lstVendors.ColList, "[");
			vName = Strings.Trim(Strings.Left(lstVendors.ColList, i - 1));

			System.Array.Clear(WinglGlobal.cr.details, 0, WinglGlobal.cr.details.Length);
			t = 0;

			WinglGlobal.tblControl = WinglGlobal.GLdb.OpenRecordset("Control");
			CashAcct = WinglGlobal.tblControl.Fields["GLCashAccount"].Value;
			WinglGlobal.tblControl.Close();

			// Find transactions
			xsql = "SELECT * FROM Transactions WHERE CheckNumber = " + chk;
			tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
			if (tblTrans.RecordCount > 0) {
				while (!tblTrans.EOF) {
					if (Strings.InStr(Strings.LCase(tblTrans.Fields["Description"].Value), Strings.LCase(vName)) > 0) {
						WinglGlobal.cr.details[t].AccountNumber = tblTrans.Fields["AccountNumber"].Value;
						WinglGlobal.cr.details[t].amount = tblTrans.Fields["Amount"].Value;
						WinglGlobal.cr.details[t].CompanyNumber = tblTrans.Fields["CompanyNumber"].Value;
						WinglGlobal.cr.details[t].CheckNumber = tblTrans.Fields["CheckNumber"].Value;
						WinglGlobal.cr.details[t].date_Renamed = tblTrans.Fields["Date"].Value;
						WinglGlobal.cr.details[t].FiscalYear = tblTrans.Fields["FiscalYear"].Value;
						WinglGlobal.cr.details[t].VendorNumber = (Information.IsDBNull(tblTrans.Fields["VendorNumber"].Value) ? "" : tblTrans.Fields["VendorNumber"].Value);
						WinglGlobal.cr.details[t].DepartmentNumber = tblTrans.Fields["DepartmentNumber"].Value;
						WinglGlobal.cr.details[t].Description = tblTrans.Fields["Description"].Value;
						WinglGlobal.cr.details[t].Source = tblTrans.Fields["Source"].Value;
						WinglGlobal.cr.details[t].Journal = tblTrans.Fields["Journal"].Value;
						WinglGlobal.cr.details[t].MajorCategory = tblTrans.Fields["MajorCategory"].Value;
						WinglGlobal.cr.details[t].MinorCategory = tblTrans.Fields["MinorCategory"].Value;
						WinglGlobal.cr.details[t].IndexNumber = tblTrans.Fields["IndexNumber"].Value;
						WinglGlobal.cr.details[t].Flag = false;
						t = t + 1;
					}
					tblTrans.MoveNext();
				}
				tblTrans.Close();
			}

			DAODBEngine_definst.Workspaces(0).BeginTrans();
			xsql = "DELETE * FROM APChecks WHERE Company = " + cmp + " AND Department = " + dpt + " AND CheckNumber = " + chk;
			WinglGlobal.GLdb.Execute(xsql, DAO.RecordsetOptionEnum.dbFailOnError);


			tblChecks = WinglGlobal.GLdb.OpenRecordset("ApChecks");
			tblChecks.AddNew();
			tblChecks.Fields["Company"].Value = cmp;
			tblChecks.Fields["Department"].Value = dpt;
			tblChecks.Fields["CheckNumber"].Value = Conversion.Val(txtCheck.CtlText);
			tblChecks.Fields["Sequence"].Value = seq;
			tblChecks.Fields["Date"].Value = dtCheck.Value;
			tblChecks.Fields["Amount"].Value = Conversion.Val(txtTotal.CtlText);
			tblChecks.Fields["Vendor/Account"].Value = vnd;
			tblChecks.Fields["ReconFlag"].Value = chkRecon.CheckState;
			tblChecks.Update();

			tblTransTemp = WinglGlobal.GLdb.OpenRecordset("Transactions");

			//******************************************************************************************
			// Match G/L transactions against check allocations, changing transactions where necessary *
			//******************************************************************************************

			// Find unchanged transactions and flag them
			//For i = 0 To t - 1
			//    For j = 1 To grdCheck.MAXROWS
			//        grdCheck.Row = j
			//        grdCheck.Col = 2: c = SearchList(lstCompanies, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 3: d = SearchList(lstDepartments, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 4: a = SearchList(lstAccounts, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 5
			//        If Trim(grdCheck.Text) <> "" And cr.details(i).CheckNumber = chk And cr.details(i).AccountNumber = a Then
			//            If (cr.details(i).CompanyNumber = c And cr.details(i).DepartmentNumber = d) Or (cr.details(i).MajorCategory = c And cr.details(i).MinorCategory = d) Then
			//                If cr.details(i).Amount = grdCheck.Value Then
			//                    cr.details(i).Flag = True
			//                    Exit For
			//                End If
			//            End If
			//        End If
			//    Next
			//Next

			// Now, process all changed transactions
			//CashPosted = False
			//For i = 0 To t - 1
			//    found = False
			//    For j = 1 To grdCheck.MAXROWS
			//        grdCheck.Row = j
			//        grdCheck.Col = 2: c = SearchList(lstCompanies, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 3: d = SearchList(lstDepartments, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 4: a = SearchList(lstAccounts, Left(grdCheck.Text, 50), 1)
			//        grdCheck.Col = 5
			//        If Trim(grdCheck.Text) <> "" And cr.details(i).CheckNumber = chk And cr.details(i).AccountNumber = a Then
			//            If (cr.details(i).CompanyNumber = c And cr.details(i).DepartmentNumber = d) Or (cr.details(i).MajorCategory = c And cr.details(i).MinorCategory = d) Then
			//                found = True
			//                If Not cr.details(i).Flag Then
			//                    cr.details(i).Flag = True
			//                    'Adjust entry
			//                    xsql = "SELECT Amount FROM Transactions WHERE IndexNumber = " & cr.details(i).IndexNumber
			//                    Set tblTransTemp = GLdb.OpenRecordset(xsql)
			//                    If tblTransTemp.RecordCount = 1 Then
			//                        tblTransTemp.Edit
			//                        If cr.details(i).AccountNumber = CashAcct Then
			//                            If Not CashPosted Then
			//                                diff = tblTransTemp("Amount") - Val(txtTotal.Text)
			//                                tblTransTemp("Amount") = -Val(txtTotal.Text)
			//                                CashPosted = True
			//                            Else
			//                                tblTransTemp.Delete
			//                                diff = 0
			//                            End If
			//                        Else
			//                            diff = tblTransTemp("Amount") - grdCheck.Value
			//                            tblTransTemp("Amount") = grdCheck.Value
			//                            cr.details(i).Amount = grdCheck.Value
			//                        End If
			//                        If diff <> 0 Then
			//                            tblTransTemp.update
			//                        End If
			//                        glPost = True
			//                        tblTransTemp.Close
			//                        ' Post difference to Balance table
			//                        If diff <> 0 Then
			//                            Call PostToBalance(c, d, CLng(a), diff, cr.details(i).Date)
			//                        End If
			//                        Exit For
			//                    End If
			//                End If
			//            End If
			//        End If
			//    Next
			//    ' If a transaction was not matched, delete it
			//    If Not found Then
			//        Set tblTransTemp = GLdb.OpenRecordset("SELECT * FROM Transactions WHERE IndexNumber = " & cr.details(i).IndexNumber)
			//        If tblTransTemp.RecordCount = 1 Then
			//            Call PostToBalance(tblTransTemp("CompanyNumber"), tblTransTemp("DepartmentNumber"), tblTransTemp("AccountNumber"), tblTransTemp("Amount"), tblTransTemp("Date"))
			//            tblTransTemp.Delete
			//            tblTransTemp.Close
			//        End If
			//    End If
			//Next

			// Save check allocations and match against G/L transactions
			seq = 0;
			for (i = 1; i <= grdCheck.MaxRows; i++) {
				if (grdCheck.GetFloat(5, i, ref a)) {
					//If a <> 0 Then  01/27/04
					grdCheck.Row = i;
					grdCheck.Col = 1;
					if (grdCheck.Value == "0") {
						seq = seq + 1;
						tblChecks.AddNew();
						tblChecks.Fields["Company"].Value = cmp;
						tblChecks.Fields["Department"].Value = dpt;
						tblChecks.Fields["CheckNumber"].Value = Conversion.Val(txtCheck.CtlText);
						tblChecks.Fields["Sequence"].Value = seq;
						tblChecks.Fields["Date"].Value = dtCheck.Value;

						grdCheck.Col = 2;
						tblChecks.Fields["DistCompany"].Value = WinglGlobal.SearchList(ref lstCompanies, ref Strings.Left(grdCheck.Text, 50), ref 2, ref 1);

						WinglGlobal.GetDepartments(ref this, ref tblChecks.Fields["DistCompany"].Value);
						grdCheck.Col = 3;
						tblChecks.Fields["DistDepartment"].Value = WinglGlobal.SearchList(ref lstDepartments, ref Strings.Left(grdCheck.Text, 50), ref 2, ref 1);

						grdCheck.Col = 4;
						tblChecks.Fields["Vendor/Account"].Value = WinglGlobal.SearchList(ref lstAccounts, ref Strings.Left(grdCheck.Text, 50), ref 2, ref 1);

						tblChecks.Fields["Amount"].Value = a;

						tblChecks.Fields["ReconFlag"].Value = chkRecon.CheckState;

						found = false;
						grdCheck.Col = 6;
						for (j = 1; j <= grdCheck.MaxRows; j++) {
							if (!string.IsNullOrEmpty(grdCheck.Value) & Conversion.Val(grdCheck.Value) != 0) {
								if (org.details(j).IndexNumber == Conversion.Val(grdCheck.Value)) {
									if (org.details(j).CompanyNumber == tblChecks.Fields["DistCompany"].Value & org.details(j).DepartmentNumber == tblChecks.Fields["DistDepartment"].Value & org.details(j).AccountNumber == tblChecks.Fields["Vendor/Account"].Value) {
										diff = 0;
										if (a != org.details(j).amount) {
											diff = a - org.details(j).amount;
											PostToBalance(ref org.details(j).CompanyNumber, ref org.details(j).DepartmentNumber, ref org.details(j).AccountNumber, ref diff, ref org.details(j).date_Renamed);
										}
										xsql = "SELECT Amount FROM Transactions WHERE IndexNumber = " + org.details(j).IndexNumber;
										tblTrans = WinglGlobal.GLdb.OpenRecordset(xsql);
										if (tblTrans.RecordCount > 0) {
											tblTrans.Edit();
											tblTrans.Fields["Amount"].Value = a;
											tblTrans.Update();
										}
										tblTrans.Close();
										org.details(j).IndexNumber = 0;
										found = true;
										break; // TODO: might not be correct. Was : Exit For
									}
								}
							}
						}

						//For j = 0 To t - 1
						//    If cr.details(j).CheckNumber = chk And cr.details(j).AccountNumber = tblChecks("Vendor/Account") Then
						//        If (cr.details(j).CompanyNumber = tblChecks("DistCompany") And cr.details(j).DepartmentNumber = tblChecks("DistDepartment")) Or (cr.details(j).MajorCategory = tblChecks("DistCompany") And cr.details(j).MinorCategory = tblChecks("DistDepartment")) Then
						//            If cr.details(j).Amount = a Then
						//                found = True
						//                Exit For
						//            End If
						//        End If
						//    End If
						//Next
						if (!found) {
							tblTransTemp = WinglGlobal.GLdb.OpenRecordset("Transactions");
							tblTransTemp.AddNew();
							tblTransTemp.Fields["CompanyNumber"].Value = tblChecks.Fields["DistCompany"].Value;
							tblTransTemp.Fields["DepartmentNumber"].Value = tblChecks.Fields["DistDepartment"].Value;
							tblTransTemp.Fields["AccountNumber"].Value = tblChecks.Fields["Vendor/Account"].Value;
							tblTransTemp.Fields["CheckNumber"].Value = Conversion.Val(txtCheck.CtlText);
							tblTransTemp.Fields["FiscalYear"].Value = WinglGlobal.cr.details[0].FiscalYear;
							tblTransTemp.Fields["Date"].Value = dtCheck.Value;
							tblTransTemp.Fields["Journal"].Value = WinglGlobal.cr.details[0].Journal;
							tblTransTemp.Fields["Source"].Value = WinglGlobal.cr.details[0].Source;
							tblTransTemp.Fields["Description"].Value = WinglGlobal.cr.details[0].Description;
							tblTransTemp.Fields["VendorNumber"].Value = vnd;
							tblTransTemp.Fields["Amount"].Value = tblChecks.Fields["Amount"].Value;
							tblTransTemp.Fields["MajorCategory"].Value = WinglGlobal.cr.details[0].MajorCategory;
							tblTransTemp.Fields["MinorCategory"].Value = WinglGlobal.cr.details[0].MinorCategory;
							tblTransTemp.Update();
							tblTransTemp.Close();
							// post amount to Balance Table
							PostToBalance(ref tblChecks.Fields["DistCompany"].Value, ref tblChecks.Fields["DistDepartment"].Value, ref tblChecks.Fields["Vendor/Account"].Value, ref tblChecks.Fields["Amount"].Value, ref (dtCheck.Value));
						}
						tblChecks.Update();
					}
					//End If
				}
			}
			// Delete unused (deleted) transactions)
			for (i = 1; i <= grdCheck.MaxRows; i++) {
				if (org.details(i).IndexNumber != 0) {
					xsql = "SELECT * FROM Transactions WHERE IndexNumber = " + org.details(i).IndexNumber;
					tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
					if (tbl.RecordCount > 0) {
						//Call PostToBalance(tbl("CompanyNumber"), tbl("DepartmentNumber"), tbl("AccountNumber"), -tbl("Amount"), tbl("Date"))
						tbl.Delete();
					}
					tbl.Close();
				}
			}
			bool deleteCash = false;
			deleteCash = false;
			// Delete Cash Account Transactions
			for (i = 0; i <= t - 1; i++) {
				if (WinglGlobal.cr.details[i].AccountNumber == CashAcct & WinglGlobal.cr.details[i].CheckNumber == chk) {
					if (WinglGlobal.cr.details[i].CompanyNumber == cmp & WinglGlobal.cr.details[i].DepartmentNumber == dpt) {
						xsql = "SELECT * FROM Transactions WHERE IndexNumber = " + WinglGlobal.cr.details[i].IndexNumber;
						tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
						if (tbl.RecordCount > 0) {
							//Call PostToBalance(tbl("CompanyNumber"), tbl("DepartmentNumber"), tbl("AccountNumber"), -tbl("Amount"), tbl("Date"))
							tbl.Delete();
							deleteCash = true;
						}
						tbl.Close();
					}
				}
			}
			// IF original Cash Account post was deleted THEN
			//   Post new check amount to cash account
			// ELSE
			//   Post difference, if any, to Cash Account
			decimal cashAmount = default(decimal);
			cashAmount = 0;
			if (deleteCash) {
				cashAmount = -Conversion.Val(txtTotal.CtlText);
			} else if (org.amt != Conversion.Val(txtTotal.CtlText)) {
				cashAmount = -(Conversion.Val(txtTotal.CtlText) - org.amt);
			}
			if (cashAmount != 0) {
				tblTransTemp = WinglGlobal.GLdb.OpenRecordset("Transactions");
				tblTransTemp.AddNew();
				tblTransTemp.Fields["CompanyNumber"].Value = cmp;
				tblTransTemp.Fields["DepartmentNumber"].Value = dpt;
				tblTransTemp.Fields["AccountNumber"].Value = CashAcct;
				tblTransTemp.Fields["CheckNumber"].Value = Conversion.Val(txtCheck.CtlText);
				tblTransTemp.Fields["FiscalYear"].Value = WinglGlobal.cr.details[0].FiscalYear;
				tblTransTemp.Fields["Date"].Value = dtCheck.Value;
				tblTransTemp.Fields["Journal"].Value = WinglGlobal.cr.details[0].Journal;
				tblTransTemp.Fields["Source"].Value = WinglGlobal.cr.details[0].Source;
				tblTransTemp.Fields["Description"].Value = WinglGlobal.cr.details[0].Description;
				tblTransTemp.Fields["VendorNumber"].Value = vnd;
				tblTransTemp.Fields["Amount"].Value = cashAmount;
				tblTransTemp.Fields["MajorCategory"].Value = WinglGlobal.cr.details[0].MajorCategory;
				tblTransTemp.Fields["MinorCategory"].Value = WinglGlobal.cr.details[0].MinorCategory;
				tblTransTemp.Update();
				tblTransTemp.Close();
				PostToBalance(ref Convert.ToInt32(cmp), ref Convert.ToInt32(dpt), ref CashAcct, ref -Conversion.Val(txtTotal.CtlText), ref (dtCheck.Value));
			}

			DAODBEngine_definst.Workspaces(0).CommitTrans();
			goto SaveCheck_exit;
			SaveCheck_error:

			LogError.MsgBox_Renamed(Err().Description, MsgBoxStyle.Critical, "Check Register");
			 // ERROR: Not supported in C#: OnErrorStatement

			DAODBEngine_definst.Workspaces(0).Rollback();
			 // ERROR: Not supported in C#: ResumeStatement

			SaveCheck_exit:


		}
//******************************************************************************
		private void txtCheck_Validating(System.Object eventSender, System.ComponentModel.CancelEventArgs eventArgs)
		{
			bool Cancel = eventArgs.Cancel;
			object xsql = null;
			//******************************************************************************

			DAO.Recordset tbl = null;

			if (Conversion.Val(txtCheck.CtlText) != chk) {
				xsql = "SELECT * FROM Apchecks WHERE Company = " + cmp + " AND ";
				xsql = xsql + " Department = " + dpt + " AND CheckNumber = " + Conversion.Val(txtCheck.CtlText);
				tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tbl.RecordCount > 0) {
					LogError.MsgBox_Renamed("You have chosen a duplicate check number.  Check will not be saved.", MsgBoxStyle.Exclamation, "Check Register");
					txtCheck.CtlText = chk;
					Cancel = true;
					tbl.Close();
				} else {
					if (LogError.MsgBox_Renamed("You have changed the check number. Do you want to keep the new check number?", MsgBoxStyle.YesNo, "Check Register") == MsgBoxResult.No) {
						Cancel = true;
					}
				}
			}

			eventArgs.Cancel = Cancel;
		}
//******************************************************************************
		private void PostToBalance(ref int c, ref int d, ref int a, ref decimal amt, ref System.DateTime dt)
		{
			//******************************************************************************

			// Deprecated
			return;

			string x = null;
			string xsql = null;
			DAO.Recordset tbl = null;

			if (DateAndTime.Year(dt) == curYear) {
				x = "C_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(dt), "00");
			} else {
				if (DateAndTime.Year(dt) == curYear - 1) {
					x = "P_Month" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(dt), "00");
				} else {
					x = "";
				}
			}
			if (!string.IsNullOrEmpty(x)) {
				xsql = "SELECT * FROM Balances WHERE CompanyNumber = " + c + " AND DepartmentNumber = " + d;
				xsql = xsql + " AND AccountNumber = " + a;
				tbl = WinglGlobal.GLdb.OpenRecordset(xsql);
				if (tbl.RecordCount > 0) {
					tbl.Edit();
					tbl.Fields[x].Value = tbl.Fields[x].Value + amt;
					tbl.Update();
				}
				tbl.Close();
				tbl = null;
			}

		}

		public void scanCheck()
		{

			WinglGlobal.uniqueID = Convert.ToString(DateAndTime.Now);
			 // ERROR: Not supported in C#: OnErrorStatement

			Err().Clear();

			WinglGlobal.PID = Interaction.Shell(WinGlobal.GetPath(ref Interaction.GetSetting("Wingl", "Options", "WindocFile", "")) + "windoc.exe wingl-runOnce \"" + WinglGlobal.uniqueID + "\" \"" + WinglGlobal.scanInvoice(ref false, ref true, ref false) + "\"");
			if (WinglGlobal.PID <= 0) {
				LogError.MsgBox_Renamed("Could not load Windoc. Invoices will not be scanned. PID=" + WinglGlobal.PID);
				cmdDoc[0].Visible = false;
				WinglGlobal.cnn.CloseDB();
			} else {
			}

		}
	}
}
