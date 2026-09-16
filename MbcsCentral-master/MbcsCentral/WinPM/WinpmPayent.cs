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
using Microsoft.VisualBasic.PowerPacks;
namespace WinPM
{
	internal partial class WinpmPayent : System.Windows.Forms.Form
	{
		decimal balance;
		string[] ddesc = new string[51];
		bool firsttime;
		decimal[] dchg = new decimal[51];
		System.DateTime c_date;
		short nc;
		short pf;
		bool payentLocked;
//UPGRADE_WARNING: Arrays in structure tblTypes may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		dao.Recordset tblTypes;
//UPGRADE_WARNING: Arrays in structure tblHistPay may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblCodes may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
//UPGRADE_WARNING: Arrays in structure tblPayTemp may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		dao.Recordset tblPayTemp;
		dao.Recordset tblCodes;
		dao.Recordset tblHistPay;
		const string secCode = "S";

		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Command1.GetIndex(eventSender);

			short i = 0;
			string xsql = null;
			clsPayReg rpt = null;

			 // ERROR: Not supported in C#: OnErrorStatement


			switch (Index) {

				case 0:
					// SAVE
					xgrid1.Col = 3;
					xgrid1.Row = 1;
					if (!payent_match()) {
						Interaction.Beep();
						LogError.MsgBox_Renamed("Total Allocated does not equal Amount Paid...");
						goto Command1_Click_exit;
					}
					payent_save();
					payent_clear();
					break;

				case 1:
					// CLEAR
					payent_clear();
					break;

				case 2:
					// RESET
					for (i = 0; i <= Information.UBound(WinpmGlobal.damount); i++) {
						WinpmGlobal.damount[i] = 0;
					}

					get_tenant();
					payent_alloc();
					for (i = 1; i <= nc; i++) {
						xgrid1.Col = 3;
						xgrid1.Row = i;
						xgrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.damount[i], "#####0.00");
					}

					break;

				case 3:
					// EXIT
					xsql = "SELECT * FROM Paytemp WHERE [UserID] = '" + WinpmGlobal.UserID + "'";
					tblPayTemp = WinpmGlobal.PMSdb.OpenRecordset(xsql);
					if (tblPayTemp.RecordCount > 0) {
						if (LogError.MsgBox_Renamed("Print the Payment Register now?", MsgBoxStyle.YesNo, "Payment Entries") == MsgBoxResult.Yes) {
							//CurrentReport = rptPayReg
							//WinpmReport.Show
							rpt = new clsPayReg();
							WinpmGlobal.rptCancel = false;
							rpt.PayReg();
							//UPGRADE_NOTE: Object rpt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							rpt = null;
						}
					}
					this.Close();
					goto Command1_Click_exit;
					break;

			}
			goto Command1_Click_exit;
			Command1_Click_error:

			LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			Command1_Click_exit:


		}
		public void payent_clear()
		{

			short i = 0;


			for (i = 0; i <= Information.UBound(ddesc); i++) {
				ddesc[i] = "";
				WinpmGlobal.ddue[i] = 0;
				WinpmGlobal.damount[i] = 0;
				//UPGRADE_WARNING: Couldn't resolve default property of object dcode(i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				WinpmGlobal.dcode[i] = "";
			}
			xgrid1.MaxRows = 0;
			txtPayent[0].Text = "";
			txtPayent[1].Text = "";
			txtAmount.Text = "";
			for (i = 6; i <= 8; i++) {
				Label1[i].Text = "";
			}
			Label1[10].Text = "";
			sflag.CheckState = System.Windows.Forms.CheckState.Unchecked;
			txtPayent[0].Focus();
			 // ERROR: Not supported in C#: OnErrorStatement

			WinpmGlobal.tblTenant.CancelUpdate();
			WinpmGlobal.tblTenant.Close();

		}

		private void WinpmPayent_Load(System.Object eventSender, System.EventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(5265);
			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(8565);
			c_date = DateAndTime.Today;
			xgrid1.MaxRows = 0;

			if (WinpmGlobal.stflag) {
				WinpmGlobal.tblSTTrans = WinpmGlobal.PMSdb.OpenRecordset("STTrans");
				WinpmGlobal.tblSTTenants = WinpmGlobal.PMSdb.OpenRecordset("STTenants");
			}
			//Set tblTenant = PMSdb.OpenRecordset("Tenants")
			WinpmGlobal.tblPayments = WinpmGlobal.PMSdb.OpenRecordset("Payments");
			tblTypes = WinpmGlobal.PMSdb.OpenRecordset("Types");
			tblCodes = WinpmGlobal.PMSdb.OpenRecordset("Codes");
			tblPayTemp = WinpmGlobal.PMSdb.OpenRecordset("Paytemp");
			tblHistPay = WinpmGlobal.PMSdb.OpenRecordset("HistPay");

			//tblTenant.Index = "PrimaryKey"
			tblCodes.Index = "PrimaryKey";
			tblTypes.Index = "PrimaryKey";
			txtPayent[3].Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(DateAndTime.Today), "00") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Day(DateAndTime.Today), "00") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Year(DateAndTime.Today), "0000");
			goto Form_Load_exit;
			Form_Load_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "Payment Entry");
			 // ERROR: Not supported in C#: ResumeStatement

			Form_Load_exit:


		}
		private void WinpmPayent_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			WinpmGlobal.tblTenant.Close();
			WinpmGlobal.tblPayments.Close();
			tblTypes.Close();
			tblCodes.Close();
			tblPayTemp.Close();
			tblHistPay.Close();

		}

		private void txtAmount_Enter(System.Object eventSender, System.EventArgs eventArgs)
		{

			txtAmount.SelectionStart = 0;
			//UPGRADE_WARNING: TextBox property txtAmount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			txtAmount.SelectionLength = txtAmount.MaxLength;

		}

		private void txtAmount_KeyPress(System.Object eventSender, System.Windows.Forms.KeyPressEventArgs eventArgs)
		{
			short KeyAscii = Strings.Asc(eventArgs.KeyChar);

			if (KeyAscii == 13 & Label1[6].Text != "* Invalid Tenant Number *") {
				xgrid1.Focus();
				if (payentLocked)
					txtPayent[0].Focus();
			}

			if (Label1[6].Text == "* Invalid Tenant Number *") {
				txtPayent[0].Focus();
			}
			eventArgs.KeyChar = Strings.Chr(KeyAscii);
			if (KeyAscii == 0) {
				eventArgs.Handled = true;
			}
		}

		private void txtAmount_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{

			//UPGRADE_WARNING: TextBox property txtAmount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			txtAmount.Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Val(txtAmount.Text), "######0.00"), ref (txtAmount.MaxLength));
			WinpmGlobal.Paid = Conversion.Val(txtAmount.Text);

		}

		private void txtPayent_Enter(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = txtPayent.GetIndex(eventSender);
			txtPayent[Index].SelectionStart = 0;
			txtPayent[Index].SelectionLength = txtPayent[Index].MaxLength;

		}
		private void txtPayent_KeyPress(System.Object eventSender, System.Windows.Forms.KeyPressEventArgs eventArgs)
		{
			short KeyAscii = Strings.Asc(eventArgs.KeyChar);
			short Index = txtPayent.GetIndex(eventSender);

			switch (Index) {

				case 0:
					if (KeyAscii == 13) {
						txtPayent[1].Focus();
						get_tenant();
						if (payentLocked)
							txtPayent[0].Focus();
					}
					break;

				case 1:
					if (KeyAscii == 13) {
						txtAmount.Focus();
						if (payentLocked)
							txtPayent[0].Focus();
					}
					break;

				case 3:
					if (KeyAscii == 13 & Label1[6].Text != "* Invalid Tenant Number *") {
						xgrid1.Focus();
						if (payentLocked)
							txtPayent[0].Focus();
					}

					if (Label1[6].Text == "* Invalid Tenant Number *") {
						txtPayent[0].Focus();
					}
					break;

			}

			eventArgs.KeyChar = Strings.Chr(KeyAscii);
			if (KeyAscii == 0) {
				eventArgs.Handled = true;
			}
		}
		public void get_tenant()
		{

			short i = 0;
			string x = null;
			string xsql = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			payentLocked = false;
			xsql = "SELECT * FROM Tenants WHERE [Building Number] = '" + txtPayent[0].Text + "' AND [Tenant Number] = '" + txtPayent[1].Text + "'";
			WinpmGlobal.tblTenant = WinpmGlobal.PMSdb.OpenRecordset(xsql, Microsoft.Office.Interop.Access.Dao.RecordsetTypeEnum.dbOpenDynaset, Microsoft.Office.Interop.Access.Dao.RecordsetOptionEnum.dbSeeChanges, Microsoft.Office.Interop.Access.Dao.LockTypeEnum.dbOptimistic);
			if (WinpmGlobal.tblTenant.RecordCount > 0) {
				WinpmGlobal.tblTenant.MoveLast();
				if (WinpmGlobal.tblTenant.RecordCount > 1) {
					WinpmGlobal.tblTemp = WinpmGlobal.tblTenant;
					My.MyProject.Forms.WinpmChoose.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(this.Left) + 100);
					My.MyProject.Forms.WinpmChoose.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(this.Top) + 700);
					My.MyProject.Forms.WinpmChoose.ShowDialog();
					WinpmGlobal.tblTenant.MoveFirst();
					//UPGRADE_WARNING: Couldn't resolve default property of object ReturnValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					for (i = 1; i <= WinpmGlobal.ReturnValue - 1; i++) {
						WinpmGlobal.tblTenant.MoveNext();
					}
				}
			} else {
				Label1[6].Text = "* Invalid Tenant Number *";
				payentLocked = true;
				goto get_tenant_exit;
			}
			 // ERROR: Not supported in C#: OnErrorStatement

			Label1[6].Text = WinpmGlobal.tblTenant.Fields("Tenant Name").Value;
			Label1[7].Text = WinpmGlobal.tblTenant.Fields("Apartment").Value;
			ddesc[0] = "RENT";
			tblTypes.Seek("=", WinpmGlobal.tblTenant.Fields("Type"));
			if (!tblTypes.NoMatch) {
				x = tblTypes.Fields("Billing Name").Value;
				if (!string.IsNullOrEmpty(Strings.Trim(x))) {
					ddesc[0] = tblTypes.Fields("Billing Name").Value;
				}
			}
			balance = WinpmGlobal.tblTenant.Fields("Rent").Value + WinpmGlobal.tblTenant.Fields("Arrears").Value - WinpmGlobal.tblTenant.Fields("Paid").Value;
			WinpmGlobal.ddue[0] = balance;
			nc = 1;
			xsql = "SELECT * FROM Charges WHERE [Building Number] = '" + WinpmGlobal.tblTenant.Fields("Building Number").Value + "' AND [Tenant Number] = '" + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + "' AND [Sequence Number] = " + WinpmGlobal.tblTenant.Fields("Sequence Number").Value;
			WinpmGlobal.tblCharges = WinpmGlobal.PMSdb.OpenRecordset(xsql, Microsoft.Office.Interop.Access.Dao.RecordsetTypeEnum.dbOpenDynaset, Microsoft.Office.Interop.Access.Dao.RecordsetOptionEnum.dbSeeChanges + Microsoft.Office.Interop.Access.Dao.RecordsetOptionEnum.dbConsistent, Microsoft.Office.Interop.Access.Dao.LockTypeEnum.dbPessimistic);
			if (!WinpmGlobal.tblCharges.NoMatch) {
				while (!WinpmGlobal.tblCharges.EOF) {
					//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					if (Information.IsDBNull(WinpmGlobal.tblCharges.Fields("Starting Date").Value) | WinpmGlobal.tblCharges.Fields("Starting Date").Value <= c_date) {
						//If IsNull(tblCharges("Ending Date")) Or (c_date < tblCharges("Ending Date") And tblCharges("Amount") + tblCharges("Arrears") - tblCharges("Paid") <> 0) Then
						//If tblCharges("Amount") + tblCharges("Arrears") - tblCharges("Paid") <> 0 Then
						//UPGRADE_WARNING: Couldn't resolve default property of object dcode(nc). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinpmGlobal.dcode[nc] = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinpmGlobal.tblCharges.Bookmark);
						tblCodes.Seek("=", WinpmGlobal.tblCharges.Fields("Charge Code"));
						if (!tblCodes.NoMatch) {
							ddesc[nc] = Strings.Trim(tblCodes.Fields("Charge Description").Value);
						} else {
							ddesc[nc] = "*";
						}
						if (WinpmGlobal.tblCharges.Fields("OnBill").Value) {
							ddesc[nc] = ddesc[nc] + "[" + Strings.Trim(WinpmGlobal.tblCharges.Fields("Comment").Value) + "]";
						}
						WinpmGlobal.ddue[nc] = WinpmGlobal.tblCharges.Fields("Amount").Value + WinpmGlobal.tblCharges.Fields("Arrears").Value - WinpmGlobal.tblCharges.Fields("Paid").Value;
						balance = balance + WinpmGlobal.ddue[nc] + WinpmGlobal.tblCharges.Fields("Paid").Value;
						WinpmGlobal.ddue[0] = WinpmGlobal.ddue[0] + WinpmGlobal.tblCharges.Fields("Paid").Value;
						dchg[nc] = WinpmGlobal.ddue[nc];
						nc = nc + 1;
					} else {
						//dcode(nc) = tblCharges.Bookmark
						//tblCodes.Seek "=", tblCharges("Charge Code")
						//If Not tblCodes.NoMatch Then
						//    ddesc(nc) = tblCodes("Charge Description")
						//Else
						//    ddesc(nc) = "*"
						//End If
						//ddue(nc) = 0
						//dchg(nc) = tblCharges("amount") + tblCharges("Arrears") - tblCharges("Paid")
						//nc = nc + 1
					}
					WinpmGlobal.tblCharges.MoveNext();
				}
				Label1[8].Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(balance, "#####0.00"), ref 15);
				WinpmGlobal.Paid = balance;
				//UPGRADE_WARNING: TextBox property txtAmount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
				txtAmount.Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, WinpmGlobal.cm_format(ref txtAmount)), ref (txtAmount.MaxLength));
				//UPGRADE_WARNING: TextBox property txtAmount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
				Label1[8].Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, WinpmGlobal.cm_format(ref txtAmount)), ref (txtAmount.MaxLength));

				xgrid1.MaxRows = nc;
				for (i = 1; i <= nc; i++) {
					xgrid1.Col = 1;
					xgrid1.Row = i;
					xgrid1.Value = ddesc[i - 1];
					xgrid1.Col = 2;
					xgrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.ddue[i - 1], "#####0.00");
					xgrid1.Col = 3;
					xgrid1.Value = Convert.ToString(0);
				}
				x = WinpmGlobal.tblTenant.Fields("Legal Code").Value;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Strings.Trim(x) > " " & !Information.IsDBNull(x)) {
					Interaction.Beep();
					Interaction.MsgBox("Tenant has a LEGAL CODE of <" + x + ">.", MsgBoxStyle.OkOnly, "Payment Entry");
				}
			}
			WinpmGlobal.tblTenant.Edit();
			firsttime = true;
			goto get_tenant_exit;
			get_tenant_error:

			if (Err().Number == 3260 | Err().Number == 3262 | Err().Number == 3008) {
				Interaction.MsgBox("Tenant Record in use.");
				payentLocked = true;
			} else {
				LogError.MsgBox_Renamed(Conversion.ErrorToString(), MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			}
			 // ERROR: Not supported in C#: ResumeStatement

			get_tenant_exit:


		}
		private void txtPayent_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = txtPayent.GetIndex(eventSender);

			short i = 0;

			switch (Index) {
				case 1:
					// Tenant Number
					get_tenant();
					txtAmount.Focus();
					if (payentLocked)
						txtPayent[0].Focus();
					break;

			}

		}

		private void XGrid1_Change(System.Object eventSender, AxFPSpread._DSpreadEvents_ChangeEvent eventArgs)
		{

			decimal total = default(decimal);
			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement


			total = 0;
			for (i = 1; i <= xgrid1.MaxRows; i++) {
				xgrid1.Col = 3;
				xgrid1.Row = i;
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(xgrid1.Value)) {
					if (Conversion.Val(xgrid1.Value) < 0) {
						xgrid1.ForeColor = System.Drawing.Color.Red;
					} else {
						xgrid1.ForeColor = System.Drawing.Color.Black;
					}
				}
				 // ERROR: Not supported in C#: OnErrorStatement

				total = total + Conversion.Val(xgrid1.Value);
				 // ERROR: Not supported in C#: OnErrorStatement

			}
			Label1[10].Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(total, "#####0.00"), ref 16);
			if (total < 0) {
				Label1[10].ForeColor = System.Drawing.Color.Red;
			} else {
				Label1[10].ForeColor = System.Drawing.Color.Black;
			}
			goto Xgrid1_Change_exit;
			XGrid1_Change_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "Payment Entries");
			 // ERROR: Not supported in C#: ResumeStatement

			Xgrid1_Change_exit:
			//



		}

		private void xgrid1_EditModeEvent(System.Object eventSender, AxFPSpread._DSpreadEvents_EditModeEvent eventArgs)
		{

			if (eventArgs.col == 3 & eventArgs.mode == 1) {
				xgrid1.Value = "";
			}

		}

		private void XGrid1_Enter(System.Object eventSender, System.EventArgs eventArgs)
		{

			short i = 0;

			if (firsttime) {
				payent_alloc();
				firsttime = false;
			}
			for (i = 1; i <= nc; i++) {
				xgrid1.Col = 3;
				xgrid1.Row = i;
				xgrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.damount[i - 1], "#####0.00");
			}
			XGrid1_Change(xgrid1, new AxFPSpread._DSpreadEvents_ChangeEvent(0, 0));
			xgrid1.SetActiveCell(3, 1);

		}

		private void xgrid1_KeyDownEvent(System.Object eventSender, AxFPSpread._DSpreadEvents_KeyDownEvent eventArgs)
		{

			object ce = null;
			object cs = null;
			object rs = null;
			object re = null;
			int nr = 0;
			int i = 0;

			switch (eventArgs.keyCode) {

				case 9:
					// TAB
					xgrid1.GetSelection(i, ref cs, ref rs, ref ce, ref re);
					//UPGRADE_WARNING: Couldn't resolve default property of object rs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (rs < nc) {
						//UPGRADE_WARNING: Couldn't resolve default property of object rs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						nr = rs + 1;
					} else {
						nr = 1;
					}
					xgrid1.SetActiveCell(3, nr);
					break;

				case 13:
					// ENTER
					Command1[0].Focus();
					Command1_Click(Command1[0], new System.EventArgs());
					break;

			}

		}


		private void XGrid1_Leave(System.Object eventSender, System.EventArgs eventArgs)
		{

			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			for (i = 1; i <= nc; i++) {
				xgrid1.Col = 3;
				xgrid1.Row = i;
				WinpmGlobal.damount[i] = Conversion.Val(xgrid1.Value);
			}

		}
		private void XGrid1_Selecting(ref short Col, ref short Row, ref bool Extending, ref bool Cancel)
		{

			if (Col < 3 | Col > 3 | Extending) {
				Cancel = true;
			}

		}
		public void payent_alloc()
		{

			decimal paidx = default(decimal);
			decimal x = default(decimal);
			short j = 0;
			bool found = false;

			 // ERROR: Not supported in C#: OnErrorStatement

			for (j = 0; j <= nc - 1; j++) {
				WinpmGlobal.damount[j] = 0;
			}
			if (sflag.CheckState == 1) {
				xgrid1.MaxRows = 1;
				nc = 1;
				ddesc[0] = "SECURITY";
				WinpmGlobal.ddue[0] = WinpmGlobal.Paid;
				WinpmGlobal.damount[0] = WinpmGlobal.Paid;
				xgrid1.Col = 1;
				xgrid1.Row = 1;
				xgrid1.Value = ddesc[0];
				xgrid1.Col = 2;
				xgrid1.Row = 1;
				xgrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "#####0.00");
				xgrid1.Col = 3;
				xgrid1.Row = 1;
				xgrid1.Value = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "#####0.00");
			} else {
				found = false;
				for (j = 0; j <= xgrid1.MaxRows - 1; j++) {
					if (WinpmGlobal.Paid == WinpmGlobal.ddue[j]) {
						WinpmGlobal.damount[j] = WinpmGlobal.Paid;
						found = true;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				if (!found) {
					paidx = WinpmGlobal.Paid;
					for (j = 0; j <= xgrid1.MaxRows - 1; j++) {
						if (WinpmGlobal.ddue[j] < 0) {
							WinpmGlobal.damount[0] = WinpmGlobal.damount[0] + WinpmGlobal.ddue[j];
						}
					}
					j = 0;
					while (j <= xgrid1.MaxRows) {
						if (WinpmGlobal.ddue[j] < paidx) {
							WinpmGlobal.damount[j] = WinpmGlobal.damount[j] + WinpmGlobal.ddue[j];
						} else {
							WinpmGlobal.damount[j] = WinpmGlobal.damount[j] + paidx;
						}
						paidx = paidx - WinpmGlobal.damount[j];
						j = j + 1;
					}
					if (paidx != 0) {
						for (j = xgrid1.MaxRows; j >= j == 0; j += -1) {
							if (WinpmGlobal.ddue[j] - WinpmGlobal.damount[j] <= 0) {
								break; // TODO: might not be correct. Was : Exit For
							}
							x = WinpmGlobal.ddue[j] - WinpmGlobal.damount[j];
							if (x >= paidx) {
								WinpmGlobal.damount[j] = WinpmGlobal.damount[j] + paidx;
								paidx = 0;
								break; // TODO: might not be correct. Was : Exit For
							} else {
								WinpmGlobal.damount[j] = WinpmGlobal.damount[j] + x;
								paidx = paidx - x;
							}
						}
						if (paidx != 0) {
							WinpmGlobal.damount[0] = WinpmGlobal.damount[0] + paidx;
						}
					}
				}
			}
			Label1[10].Text = WinpmGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "#####0.00"), ref 15);
			Label1[10].Refresh();
			goto payent_alloc_exit;
			payent_alloc_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "Payment Entry");
			 // ERROR: Not supported in C#: ResumeStatement

			payent_alloc_exit:


		}
		public bool payent_match()
		{
			bool functionReturnValue = false;

			decimal x = default(decimal);
			decimal z = default(decimal);
			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			x = 0;
			for (i = 1; i <= nc; i++) {
				xgrid1.Col = 3;
				xgrid1.Row = i;
				//On Error Resume Next
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (Information.IsDBNull(xgrid1.Value)) {
					z = 0;
				} else {
					z = Conversion.Val(xgrid1.Value);
				}
				WinpmGlobal.damount[i - 1] = z;
				x = x + z;
				//On Error GoTo payent_match_error
			}
			if (x != WinpmGlobal.Paid) {
				functionReturnValue = false;
			} else {
				functionReturnValue = true;
			}
			goto payent_match_exit;
			payent_match_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, "Payment Entry");
			 // ERROR: Not supported in C#: ResumeStatement

			payent_match_exit:
			return functionReturnValue;


		}
		public void payent_save()
		{

			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			//DBEngine.Workspaces(0).BeginTrans
			WinpmGlobal.tblTenant.Edit();
			WinpmGlobal.tblTenant.Fields("Paid").Value = WinpmGlobal.tblTenant.Fields("Paid").Value + WinpmGlobal.Paid;
			WinpmGlobal.tblTenant.Fields("Last Paid Date").Value = txtPayent[3].Text;
			if (Strings.Mid(WinpmGlobal.tblTenant.Fields("Tenant Number").Value, 4, 1) == "B") {
				WinpmGlobal.tblTenant.Fields("MBR").Value = WinpmGlobal.tblTenant.Fields("MBR").Value + WinpmGlobal.Paid;
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_save (1): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
			if (sflag.CheckState == 1) {
				WinpmGlobal.tblTenant.Fields("Security").Value = WinpmGlobal.tblTenant.Fields("Security").Value + WinpmGlobal.Paid;
				WinpmGlobal.tblTenant.Update();
				//DBEngine.Workspaces(0).CommitTrans
				//DBEngine.Workspaces(0).BeginTrans
				payent_sec();
			} else {
				WinpmGlobal.tblTenant.Update();
				//DBEngine.Workspaces(0).CommitTrans
				//DBEngine.Workspaces(0).BeginTrans
				if (WinpmGlobal.damount[0] != 0) {
					payent_temp(ref WinpmGlobal.damount[0], ref "=");
				}
				for (i = 1; i <= nc - 1; i++) {
					if (WinpmGlobal.damount[i] != 0) {
						//UPGRADE_WARNING: Couldn't resolve default property of object dcode(i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WinpmGlobal.tblCharges.Bookmark = WinpmGlobal.dcode[i];
						WinpmGlobal.tblCharges.Edit();
						WinpmGlobal.tblCharges.Fields("Paid").Value = WinpmGlobal.tblCharges.Fields("Paid").Value + WinpmGlobal.damount[i];
						WinpmGlobal.tblCharges.Update();
						payent_temp(ref WinpmGlobal.damount[i], ref WinpmGlobal.tblCharges.Fields("Charge Code").Value);
						WinpmGlobal.tblTenant.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinpmGlobal.tblTenant.LastModified);
						if (WinpmGlobal.tblTenant.Fields("Legal Code").Value == WinpmGlobal.tblCharges.Fields("Charge Code").Value) {
							if (WinpmGlobal.tblCharges.Fields("Amount").Value + WinpmGlobal.tblCharges.Fields("Arrears").Value - WinpmGlobal.tblCharges.Fields("Paid").Value == 0) {
								WinpmGlobal.tblTenant.Edit();
								WinpmGlobal.tblTenant.Fields("Legal Code").Value = " ";
								WinpmGlobal.tblTenant.Update();
							}
						}
						if (WinpmGlobal.tblCharges.Fields("Charge Code").Value == secCode) {
							WinpmGlobal.tblTenant.Edit();
							WinpmGlobal.tblTenant.Fields("Security").Value = WinpmGlobal.tblTenant.Fields("Security").Value + WinpmGlobal.damount[i];
							WinpmGlobal.tblTenant.Update();
							payent_pay(ref WinpmGlobal.damount[i], ref WinpmGlobal.tblCharges.Fields("Charge Code").Value);
							WinpmGlobal.Paid = WinpmGlobal.Paid - WinpmGlobal.damount[i];
						}
					}
				}
				//DBEngine.Workspaces(0).CommitTrans
				//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				LogError.LogError("PAYENT payent_save (2): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
				if (WinpmGlobal.Paid != 0) {
					//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					LogError.LogError("PAYENT payent_save (3): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
					payent_pay(ref WinpmGlobal.Paid, ref "=");
					//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					LogError.LogError("PAYENT payent_save (4): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(WinpmGlobal.Paid, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
				}
			}
			pf = 1;
			goto payent_save_exit;
			payent_save_error:

			if (Err().Number == 3260 | Err().Number == 3027) {
				Interaction.MsgBox("Cannot save payment.  Tenant Record in use", , WinpmGlobal.sProcID);
			} else {
				LogError.MsgBox_Renamed("Error updating TENANTS file.  ", MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			}

			//On Error Resume Next
			//DBEngine.Workspaces(0).Rollback
			 // ERROR: Not supported in C#: ResumeStatement

			payent_save_exit:

			 // ERROR: Not supported in C#: OnErrorStatement

			WinpmGlobal.tblTenant.Close();

		}
		public void payent_sec()
		{

			string xsql = null;
			bool updateTenants = false;

			 // ERROR: Not supported in C#: OnErrorStatement

			xsql = "SELECT * FROM Charges WHERE [Sequence Number] = " + WinpmGlobal.tblTenant.Fields("Sequence Number").Value;
			xsql = xsql + " AND [Charge Code] = 'S' AND [Type] = '1' AND [Starting Date] < #" + txtPayent[3].Text + "#";
			Err().Clear();
			//UPGRADE_NOTE: Object tblCharges may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			WinpmGlobal.tblCharges = null;
			Err().Clear();
			WinpmGlobal.tblCharges = WinpmGlobal.PMSdb.OpenRecordset(xsql);
			Err().Clear();
			if (WinpmGlobal.tblCharges.RecordCount <= 0) {
				Err().Clear();
				//Set tblCharges = PMSdb.OpenRecordset("Charges")
				WinpmGlobal.tblCharges.AddNew();
				WinpmGlobal.tblCharges.Fields("Building Number").Value = WinpmGlobal.tblTenant.Fields("Building Number").Value;
				WinpmGlobal.tblCharges.Fields("Tenant Number").Value = WinpmGlobal.tblTenant.Fields("Tenant Number").Value;
				WinpmGlobal.tblCharges.Fields("Charge Code").Value = secCode;
				WinpmGlobal.tblCharges.Fields("Type").Value = "1";
				WinpmGlobal.tblCharges.Fields("Amount").Value = WinpmGlobal.Paid;
				WinpmGlobal.tblCharges.Fields("Arrears").Value = 0;
				WinpmGlobal.tblCharges.Fields("Paid").Value = WinpmGlobal.Paid;
				WinpmGlobal.tblCharges.Fields("Future Amount").Value = 0;
				WinpmGlobal.tblCharges.Fields("Sequence Number").Value = WinpmGlobal.tblTenant.Fields("Sequence Number").Value;
				WinpmGlobal.tblCharges.Fields("History Date").Value = txtPayent[3].Text;
				WinpmGlobal.tblCharges.Fields("ChargeToTenant").Value = true;
				WinpmGlobal.tblCharges.Fields("ShowOnBill").Value = true;
				updateTenants = true;
			} else {
				WinpmGlobal.tblCharges.Edit();
				WinpmGlobal.tblCharges.Fields("Paid").Value = WinpmGlobal.tblCharges.Fields("Paid").Value + WinpmGlobal.Paid;
				updateTenants = false;
			}
			WinpmGlobal.tblCharges.Update();
			if (Err().Number != 0) {
				LogError.MsgBox_Renamed("Error writing CHARGES file.  ", MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			} else {
				WinpmGlobal.tblCharges.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinpmGlobal.tblCharges.LastModified);
				WinpmGlobal.HistChgUpdate(ref WinpmGlobal.tblCharges, ref false, ref false, ref Convert.ToDateTime(txtPayent[3].Text));
				if (updateTenants) {
					//xsql = "SELECT [Total Charges] FROM Tenants WHERE [Sequence Number] = " & tblCharges("Sequence Number")
					//Set tblTemp = PMSdb.OpenRecordset(xsql)
					//If tblTemp.RecordCount > 0 Then
					//tblTemp.Edit
					WinpmGlobal.tblTenant.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinpmGlobal.tblTenant.LastModified);
					WinpmGlobal.tblTenant.Edit();
					WinpmGlobal.tblTenant.Fields("Total Charges").Value = WinpmGlobal.tblTenant.Fields("Total Charges").Value + WinpmGlobal.tblCharges.Fields("Amount").Value + WinpmGlobal.tblCharges.Fields("Arrears").Value;
					WinpmGlobal.tblTenant.Update();
					//tblTemp.Update
					//If Err <> 0 Then
					//    MsgBox "Error writing Tenants file.  ", vbCritical, sProcID
					//End If
					//End If
				}
			}
			payent_pay(ref WinpmGlobal.Paid, ref secCode);
			payent_temp(ref WinpmGlobal.Paid, ref secCode);

		}
		public void payent_pay(ref decimal a, ref string c)
		{

			short errorcount = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_pay (1): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
			errorcount = 1;
			WinpmGlobal.tblPayments.AddNew();
			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_pay (2): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
			WinpmGlobal.tblPayments.Fields("Building Number").Value = WinpmGlobal.tblTenant.Fields("Building Number").Value;
			WinpmGlobal.tblPayments.Fields("Tenant Number").Value = WinpmGlobal.tblTenant.Fields("Tenant Number").Value;
			WinpmGlobal.tblPayments.Fields("Date of Entry").Value = txtPayent[3].Text;
			WinpmGlobal.tblPayments.Fields("Amount").Value = a;
			WinpmGlobal.tblPayments.Fields("Security Code").Value = c;
			WinpmGlobal.tblPayments.Fields("Sequence Number").Value = WinpmGlobal.tblTenant.Fields("Sequence Number").Value;
			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_pay (3): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,##0.00"), 0, ref WinpmGlobal.sProcID);
			WinpmGlobal.tblPayments.Update();
			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_pay (4): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - " + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(a, "###,##0.00"), 0, ref WinpmGlobal.sProcID);

			if (WinpmGlobal.stflag & c == secCode) {
				WinpmGlobal.tblPayments.Bookmark = Microsoft.VisualBasic.Compatibility.VB6.Support.CopyArray(WinpmGlobal.tblPayments.LastModified);
				WinpmGlobal.SecTrans();
			}

			// History Payments
			errorcount = 0;
			tblHistPay.AddNew();
			tblHistPay.Fields("Building Number").Value = WinpmGlobal.tblTenant.Fields("Building Number").Value;
			tblHistPay.Fields("Tenant Number").Value = WinpmGlobal.tblTenant.Fields("Tenant Number").Value;
			tblHistPay.Fields("Sequence Number").Value = WinpmGlobal.tblTenant.Fields("Sequence Number").Value;
			tblHistPay.Fields("Date").Value = txtPayent[3].Text;
			tblHistPay.Fields("Amount").Value = a;
			tblHistPay.Fields("Code").Value = c;
			tblHistPay.Update();
			goto histpay_exit;
			histpay_error:

			//UPGRADE_WARNING: Couldn't resolve default property of object LogError.LogError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			LogError.LogError("PAYENT payent_pay (5): " + WinpmGlobal.tblTenant.Fields("Building Number").Value + WinpmGlobal.tblTenant.Fields("Tenant Number").Value + " - ERROR: " + Err().Number + ":" + Err().Description, 0, ref WinpmGlobal.sProcID);
			if (Err().Number == 3201) {
				if (errorcount == 0) {
					WinpmGlobal.Create_HistMast(ref WinpmGlobal.tblTenant);
					errorcount = 1;
					 // ERROR: Not supported in C#: ResumeStatement

				} else {
					LogError.MsgBox_Renamed("Error writing PAYMENTS file. (" + WinpmGlobal.tblPayments.Fields("Building Number").Value + WinpmGlobal.tblPayments.Fields("Tenant Number").Value + ")", MsgBoxStyle.Critical, WinpmGlobal.sProcID);
					 // ERROR: Not supported in C#: ResumeStatement

				}
			} else if (Err().Number == 3420 & errorcount == 1) {
				errorcount = 0;
				WinpmGlobal.tblPayments = WinpmGlobal.PMSdb.OpenRecordset("Payments");
				 // ERROR: Not supported in C#: ResumeStatement

			} else {
				LogError.MsgBox_Renamed("Error saving payment", MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			}
			histpay_exit:


		}
		public void payent_temp(ref decimal a, ref string c)
		{

			 // ERROR: Not supported in C#: OnErrorStatement

			tblPayTemp.AddNew();
			tblPayTemp.Fields("UserID").Value = WinpmGlobal.UserID;
			tblPayTemp.Fields("Building Number").Value = WinpmGlobal.tblTenant.Fields("Building Number").Value;
			tblPayTemp.Fields("Tenant Number").Value = WinpmGlobal.tblTenant.Fields("Tenant Number").Value;
			tblPayTemp.Fields("Date").Value = txtPayent[3].Text;
			tblPayTemp.Fields("Amount").Value = a;
			tblPayTemp.Fields("Code").Value = c;
			tblPayTemp.Fields("Name").Value = WinpmGlobal.tblTenant.Fields("Tenant Name").Value;
			tblPayTemp.Fields("Apartment").Value = WinpmGlobal.tblTenant.Fields("Apartment").Value;
			tblPayTemp.Update();
			if (Err().Number != 0) {
				LogError.MsgBox_Renamed("Error writing PayTemp file.  ", MsgBoxStyle.Critical, WinpmGlobal.sProcID);
			}

		}

		private void xgrid1_SelChange(System.Object eventSender, AxFPSpread._DSpreadEvents_SelChangeEvent eventArgs)
		{

			int r = 0;

			r = eventArgs.blockRow;
		}
	}
}
