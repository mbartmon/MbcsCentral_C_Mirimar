using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MbcsCentral;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace MbcsCentral.WinGL
{
	public class APLaser 
	{
        public DateTime CheckDate { get; set; }
        public short Iflag { set { iflag = value; } }
        Report rpt;
        Layout Checks3;
        Invoices[] invoices;
        Invoices[] tblCount;
        Invoices tblinvoices;
        InvoiceDistributions[] InvDist;
        Department dept;
        Vendors tblVendors;
        Accounts account;
        CheckNumbers checknumbers;

        short payroll;
		short l;
		short rl;
		short iflag;
		int cn;
		short fiLaser;
		bool printed;
		decimal[] v = new decimal[5];
		string DepartmentName;
		string CompanyName;
		string tdate;
		decimal disc;
		decimal gross;
		decimal net;
		decimal allow;
		int xvnum;
		int xcmp;
		int xdpt;
		short xgrp;
		string xsql;
		short postflag;
		int Batch;
		string desc;
		int r;
		string[] amt_fmt = new string[4];
		short[] amt_len = new short[4];
		short vi;
		string DefaultPrinter;
		bool vndPR;
		bool noIncrement;
		bool aligned;
		bool PRformat;
		short fi;

		public void Process_Checks()
		{
			string dpname;
			int invCount;

			string xsql = null;
			bool first = false;
			short rc = 0;
			int recCount = 0;
			int dr = 0;
			decimal dd = default(decimal);

            invoices = Invoices.LoadForChecks();

			if (invoices == null || invoices.Length == 0)
            {
                MessageBox.Show("No Checks to Print.");
                rc = 1;
				goto Process_Checks_exit;
			}

			/* These lines for continuous forms
			foreach (Printer prt_loopVariable in GlobalModule.Printers) {
				prt = prt_loopVariable;
				if (prt.DeviceName == lstPrinters.Text) {
					Printer = prt;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			DefaultPrinter = Printer.DeviceName;
			Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeName(Printer.Font, "Courier New");
			Printer.Font = Microsoft.VisualBasic.Compatibility.VB6.Support.FontChangeSize(Printer.Font, 12);
            */
			//rl = 15
			rl = 10;

			xcmp = 0;
			xdpt = 0;
			xvnum = 0;
			System.Array.Clear(v, 0, v.Length);

			first = true;

			GlobalShared.Log.Log((int)LogClass.logType.Info,"**************************************************************************************",false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "APCHECK Processing. " + DateAndTime.Now, false);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "**************************************************************************************", false);

			l = 1;
			InvoiceDistributions[] distTbl;
            foreach (Invoices invoice in invoices)
            {
                tblinvoices = invoice;
                if (first)
                    xgrp = tblinvoices.CheckGroup;
				if (tblinvoices.CheckNumber > 0 && !Information.IsDBNull(tblinvoices.CheckDate) && tblinvoices.AllocationFlag)
					continue;
				if (xcmp != tblinvoices.Company) {
					if (!printed & !first)
                    {
						Print_Check();
						clearrecord();
					}
                    xcmp = tblinvoices.Company;
					Checks3.lcmp = xcmp.ToString("00");

					Company company = Company.loadByCompany(xcmp);
					if (company != null)
                    {
						CompanyName = "* Not On File *";
					}
                    else
                    {
						CompanyName = company.Name;
					}
					xdpt = 0;
					xvnum = 0;
					//Checks3.memo = "This is the memo line..."
				}
				if (xdpt != tblinvoices.Department)
                {
					if (!printed & !first)
                    {
						Print_Check();
						clearrecord();
					}
					xdpt = tblinvoices.Department;
					Checks3.ldpt = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xdpt, "000");
					getdpt(xcmp, xdpt);
					aligned = false;
					xvnum = 0;
				}
				if (xvnum != tblinvoices.VendorNumber || xgrp != tblinvoices.CheckGroup)
                {
					//**************************************************************
					//* Account for stub overflow to determine actual check number *
					//**************************************************************
					if (first)
                    {
						first = false;
						clearrecord();
					} else if (!printed)
                    {
						Print_Check();
						clearrecord();
					}

					recCount = cn;
					xsql = "SELECT InvoiceKey FROM Invoices WHERE VendorNumber = " + tblinvoices.VendorNumber + " AND Company = " + tblinvoices.Company;
					xsql = xsql + " AND Department = " + tblinvoices.Department + " AND CheckGroup = " + tblinvoices.CheckGroup;
					xsql = xsql + " AND (PayFlag = 1 OR PayFlag = 2)";
					tblCount = Invoices.LoadQuery(xsql);
					invCount = tblCount.Length;
					r = 0;
					for (int i = 0; i < invCount; i++ )
                    {
						distTbl = InvoiceDistributions.loadByInvoice(tblCount[i].InvoiceKey);
						if (distTbl != null && distTbl.Length > 0)
							r = r + distTbl.Length;
					}
					recCount = cn + Conversion.Int((r - 1) / rl);
					if (recCount > cn)
                    {
						GlobalShared.Log.Log((int)LogClass.logType.Info, "Adjusting chk# from " + cn + " to " + recCount + " due to " + r + " stub items for vendor <" + tblinvoices.VendorNumber + ">", false);
						cn = recCount;
					}
                    else
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "Using chk# " + cn + " with " + r + " stub items for vendor <" + tblinvoices.VendorNumber + ">", false);
					xvnum = tblinvoices.VendorNumber;
					xgrp = tblinvoices.CheckGroup;
					xsql = "SELECT * FROM Vendors WHERE VendorNumber = " + xvnum;
                    tblVendors = Vendors.load(xvnum);
					if (payroll > 0 && tblVendors.Payroll) 
						PRformat = true;
					else 
						PRformat = false;
					vndPR = tblVendors.Payroll;
					Checks3.payee = tblVendors.VendorName.Trim();
					if (tblVendors.Name2 != null && tblVendors.Name2.Trim() != "") 
                    {
						Checks3.addr1 = tblVendors.Name2.Trim();
						Checks3.addr2 = tblVendors.Street.Trim();
						if (tblVendors.City != null && tblVendors.City.Trim() != "" && tblVendors.State != null && tblVendors.State.Trim() != "")
							Checks3.addr3 = tblVendors.City.Trim() + ", " + tblVendors.State.Trim() + " " + tblVendors.Zip;
					}
                    else
                    {
						Checks3.addr1 = tblVendors.Street.Trim();
						if (tblVendors.City != null && tblVendors.City.Trim() != ""  && tblVendors.State != null && tblVendors.State.Trim() != "")
                        {
							Checks3.addr2 = tblVendors.City.Trim() + ", " + tblVendors.State.Trim() + " " + tblVendors.Zip;
						}
						Checks3.addr3 = " ";
					}
					//*********
                    Memos tblMemo = Memos.load(tblinvoices.MemoId);
					if ( tblMemo != null)
                    {
						Checks3.memo = tblMemo.Memo;
						tblMemo.delete(tblMemo.MemoId.ToString());
                        tblMemo = null;
					} else
						Checks3.memo = "";
					Array.Clear(Checks3.voucher, 0, Checks3.voucher.Length);
					printed = false;
                    tblVendors = null;
				}

				gross = tblinvoices.Amount - tblinvoices.AmountPaid;
				if (tblinvoices.DiscountAmount != 0)
                    disc = tblinvoices.DiscountAmount;
                if (tblinvoices.Allowance != 0)
				    allow = tblinvoices.Allowance;
				if (tblinvoices.AmountToPay != 0)
					gross = tblinvoices.AmountToPay;
				net = gross - disc;
				if (gross != 0 && net != 0)
                {
					InvDist = InvoiceDistributions.loadByInvoice(tblinvoices.InvoiceKey);
                    if (InvDist != null && InvDist.Length > 0)
                        dr = InvDist.Length;
                    else
                        dr = 0;
					if (payroll == 0)
                    {
						if (dr == 1)
							dd = disc;
						else
                        { 
							dd = 0;
							recCount = cn + Conversion.Int((dr - 1) / rl);
						}
						foreach (InvoiceDistributions dist in InvDist)
                        {
							dpname = "*";
							dept = Department.loadByDepartment((short)dist.Department);
							if (dept != null)
                            {
                                dpname = dept.Name;
                                dept = null;
							}
							pagechk();
							if (l <= 10)
                            {
								Checks3.voucher[l].vInvoice = tblinvoices.InvoiceNumber;
								Checks3.voucher[l].vDate = tblinvoices.Date.ToString("mm/dd/yy");
								Checks3.voucher[l].vDesc = dpname;
								Checks3.voucher[l].vGross = dist.Amount.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
								Checks3.voucher[l].vDisc = dd.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
								Checks3.voucher[l].vNet = (dist.Amount- dd).ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
							}
							l++;
						}
                        InvDist = null;
					}
                    else
                    {
                        foreach (InvoiceDistributions dist in InvDist)
                        {
							pagechk();
							if (l <= 10)
                            { 
								Checks3.voucher[l].vInvoice = tblinvoices.InvoiceNumber;
								Checks3.voucher[l].vDate = tblinvoices.Date.ToString( "mm/dd/yy");
							}
                            account = Accounts.load(dist.Account);
							if (account != null)
                            {
								if (l <= 10)
                                    Checks3.voucher[l].vDesc = account.Description;
							} else 
								Checks3.voucher[l].vDesc = "*" + dist.Account.ToString("#####0") + "*";
							if (payroll < 1 || !vndPR)
                            {
								if (l <= 10)
                                {
									Checks3.voucher[l].vDisc = disc.ToString( amt_fmt[vi]).PadLeft(amt_len[vi]);
									Checks3.voucher[l].vNet = net.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
									Checks3.voucher[l].vGross = gross.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
								}
                                l++;
								break; // TODO: might not be correct. Was : Exit Do
							} else if (l <= 10)
                                {
									Checks3.voucher[l].vGross = Strings.Space(11);
									Checks3.voucher[l].vDisc = Strings.Space(11);
									Checks3.voucher[l].vNet = dist.Amount.ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);
								}
							l++;
						}
                        InvDist = null;
					}
                }
            }
			tblinvoices.CheckNumber = cn;
			tblinvoices.Date = CheckDate;
			tblinvoices.save();

			v[1] = v[1] + gross;
			v[2] = v[2] + allow;
			v[3] = v[3] + disc;
			v[4] = v[4] + net;
				

			Print_Check();
			rc = 0;
Process_Checks_exit:
			invoices = null;
			FileSystem.FileClose(fiLaser, fi);
			if (rc == 0) {
                //Time to print the check;
					//Printer.EndDoc();
			}

		}
		public void Print_Check()
		{
			object post = null;

			if (l > 0) {
				if (v[4] != 0) {
					Checks3.chkNum = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(cn, "000000");
					Checks3.chkDate = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tdate, "mm/dd/yyyy");
					Checks3.amount = v[4].ToString(amt_fmt[vi]).PadLeft(amt_len[vi]);

					//UPGRADE_WARNING: Couldn't resolve default property of object post. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					post = 1;
				} else {
					Checks3.voidx = "V";
					l = 1;
				}

				printContinuous();

				Checks3.voidx = " ";
				l = 1;

				if (!noIncrement) {

					// Update CheckNumbers table
					cn = cn + 1;
					checknumbers = CheckNumbers.load(xcmp, xdpt);
					if (checknumbers != null) {
                        checknumbers.CheckNumber = cn;
					} else {
                        checknumbers = new CheckNumbers();
                        checknumbers.Company = (short)xcmp;
                        checknumbers.Department = (short)xdpt;
                        checknumbers.CheckNumber = cn;
					}
                    checknumbers.save();
                    checknumbers = null;
				}

				printed = true;
			}

		}
		private void APLaser_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			int i;

			Control[] tblControl = null;
            tblControl = Control.load();
            if (tblControl != null && tblControl.Length > 0)
            {
                postflag = tblControl[0].GLMethod;
                Batch = tblControl[0].GLBatchNumber + 1;
                desc = tblControl[0].GLBatchDesc;
                tblControl[0].GLBatchNumber = Batch;
                tblControl[0].save();
                tblControl = null;
            }
			amt_fmt[0] = "####,##0.00";
			amt_fmt[1] = "####,##0.00";
			amt_fmt[2] = "###,###,##0.00";
			amt_len[0] = 11;
			amt_len[1] = 11;
			amt_len[2] = 14;

			clearrecord();

		}
		public void pagechk()
		{

			decimal v4 = default(decimal);

			if (l > rl) {
				v4 = v[4];
				v[4] = 0;
				noIncrement = true;
				Print_Check();
				noIncrement = false;
				v[4] = v4;
				l = 1;
				printed = false;
				System.Array.Clear(Checks3.voucher, 0, Checks3.voucher.Length);
			}

		}
		public void getdpt(int c, int d)
		{

			string dname = null;

			if (d == 0) {
				dname = "";
			} else {

				Department  department = Department.loadByDepartment((short)d);
				if (department == null)
                {
					dname = "* Not on file *";
				} else {
					dname = department.Name;
				}
			}

			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserChk.Company.CtlText = tblCompany.Fields["Name"].Value;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserMsg.Company.CtlText = tblCompany.Fields["Name"].Value;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserChk.Department.CtlText = dname;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserMsg.Department.CtlText = dname;
			My.MyProject.Forms.LaserChk.CheckDate.Value = tdate;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserMsg.CheckDate.CtlText = tdate;

            checknumbers = CheckNumbers.load((short)c, (short)d);
			if (checknumbers != null) {
				cn = checknumbers.CheckNumber;
			} else {
				cn = 0;
				MessageBox.Show("Check number is 0. Company=" + c.ToString() + ", Department=" + d.ToString());
			}
            checknumbers = null;
            //UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
            My.MyProject.Forms.LaserChk.CheckNumber.CtlText = cn;
			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			My.MyProject.Forms.LaserMsg.CheckNumber.CtlText = cn;

			if (iflag == 0 | cn == 0) {
				My.MyProject.Forms.LaserChk.ShowDialog();
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				My.MyProject.Forms.LaserMsg.CheckNumber.CtlText = My.MyProject.Forms.LaserChk.CheckNumber.CtlText;
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				My.MyProject.Forms.LaserMsg.CheckDate.CtlText = My.MyProject.Forms.LaserChk.CheckDate.Value;
				//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
				cn = Conversion.Val(My.MyProject.Forms.LaserChk.CheckNumber.CtlText);
			}
			My.MyProject.Forms.LaserMsg.Refresh();

			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			tdate = My.MyProject.Forms.LaserMsg.CheckDate.CtlText;

			FileSystem.PrintLine(fi, "Retrieved chk# " + cn + " for dept <" + d + ">");

			goto getdpt_exit;
			getdpt_error:

			FileSystem.PrintLine(fi, "Error retrieving department <" + d + "> record: " + Err().Description);
			LogError.MsgBox_Renamed("Error retrieving department <" + d + "> record: " + Err().Description);
			 // ERROR: Not supported in C#: ResumeStatement

			getdpt_exit:


		}
		public void printContinuous()
		{
			string cent = null;
			double cents = 0;
			double dollars = 0;
			int i;
			//Printer Printer = new Printer();

			// FOR DEBUGING ONLY
			//rpt.PrintPageLine "CHECK # " & cn

			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			l = 0;
			if (Strings.InStr(Strings.LCase(Printer.DeviceName), "printbos") > 0) {
				Printer.Write(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xcmp, "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xdpt, "000"));
				FileSystem.Print(fi, Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xcmp, "00") + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(xdpt, "000"));
				if (PRformat) {
					rpt.PrintPageLine(" P");
				} else {
					rpt.PrintPageLine("");
				}
			}
			for (i = 1; i <= 10; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (Conversion.Val(Checks3.voucher(i).vNet) != 0) {
					if (Strings.LCase(Strings.Left(Printer.DeviceName, 9)) == "printboss") {
						if (!PRformat) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							Printer.Write(WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 20)), ref 20) + " " + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 16));
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							FileSystem.PrintLine(fi, WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 20)), ref 20) + " " + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 16));
						} else {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							if (i == 1) {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								Printer.Write(WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 20)), ref 20) + " " + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 38));
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								FileSystem.PrintLine(fi, WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 20)), ref 20) + " " + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 38));
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								Printer.Write(Strings.Space(30) + Strings.Left(Checks3.voucher(i).vDesc, 38));
								//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								FileSystem.PrintLine(fi, Strings.Space(30) + Strings.Left(Checks3.voucher(i).vDesc, 38));
							}
						}
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						Printer.Write(WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 8)), ref 8) + Strings.Space(13) + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 15) + " ");
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						FileSystem.PrintLine(fi, WinGlobal.rtpad(ref Strings.Trim(Strings.Left(Checks3.voucher(i).vInvoice, 8)), ref 8) + Strings.Space(13) + Checks3.voucher(i).vDate + " " + Strings.Left(Checks3.voucher(i).vDesc, 15) + " ");
					}
					if (!PRformat) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						rpt.PrintPageLine(WinGlobal.lpad(ref Checks3.voucher(i).vGross, ref amt_len[vi]) + WinGlobal.lpad(ref Strings.Right(Checks3.voucher(i).vDisc, amt_len[vi] - 2), ref amt_len[vi] - 2) + "  " + WinGlobal.lpad(ref Checks3.voucher(i).vNet, ref amt_len[vi]));
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						FileSystem.PrintLine(fi, WinGlobal.lpad(ref Checks3.voucher(i).vGross, ref amt_len[vi]) + WinGlobal.lpad(ref Strings.Right(Checks3.voucher(i).vDisc, amt_len[vi] - 2), ref amt_len[vi] - 2) + "  " + WinGlobal.lpad(ref Checks3.voucher(i).vNet, ref amt_len[vi]));
					} else {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						rpt.PrintPageLine(WinGlobal.lpad(ref Checks3.voucher(i).vNet, ref amt_len[vi] + 3));
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						FileSystem.PrintLine(fi, WinGlobal.lpad(ref Checks3.voucher(i).vNet, ref amt_len[vi] + 3));
					}
					l = l + 1;
				}
			}
			for (i = 1; i <= 13 - l; i++) {
				rpt.PrintPageLine("");
			}
			if (!PRformat) {
				rpt.PrintPageLine(FileSystem.TAB(47), WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[1], amt_fmt[vi]), ref amt_len[vi]) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[3], amt_fmt[vi]), ref amt_len[vi] - 2) + "  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], amt_fmt[vi]), ref amt_len[vi]));
				FileSystem.PrintLine(fi, WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[1], amt_fmt[vi]), ref amt_len[vi]) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[3], amt_fmt[vi]), ref amt_len[vi] - 2) + "  " + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], amt_fmt[vi]), ref amt_len[vi]));
			} else {
				rpt.PrintPageLine(FileSystem.TAB(47), Strings.Space(amt_len[vi]) + Strings.Space(amt_len[vi]) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], amt_fmt[vi]), ref amt_len[vi]));
				FileSystem.PrintLine(fi, Strings.Space(amt_len[vi]) + Strings.Space(amt_len[vi]) + WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], amt_fmt[vi]), ref amt_len[vi]));
			}
			for (i = 1; i <= 5; i++) {
				rpt.PrintPageLine("");
			}
			//08/04/00
			if (Strings.LCase(Strings.Left(Printer.DeviceName, 9)) != "printboss") {
				rpt.PrintPageLine("");
				//08/04/00
			}
			//08/04/00
			if (Checks3.voidx == "V") {
				rpt.PrintPageLine(FileSystem.TAB(49), "V O I D    V O I D    V O I D");
			} else {
				rpt.PrintPageLine(FileSystem.TAB(46), Checks3.chkDate + "   " + Checks3.chkNum + spad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], "$####,##0.00"), ref 12));
				FileSystem.PrintLine(fi, Checks3.chkDate + "   " + Checks3.chkNum + spad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(v[4], "$####,##0.00"), ref 12));
			}
			//For i = 1 To 2: rpt.PrintPageLine: Next    08/04/00
			rpt.PrintPageLine("");
			//08/04/00
			//08/04/00
			if (Strings.LCase(Strings.Left(Printer.DeviceName, 9)) == "printboss") {
				rpt.PrintPageLine("");
				//08/04/00
			}
			//08/04/00
			if (Checks3.voidx == "V") {
				rpt.PrintPageLine(FileSystem.TAB(10), "V O I D    V O I D    V O I D");
				FileSystem.PrintLine(fi, "V O I D    V O I D    V O I D");
			} else {
				dollars = Conversion.Fix(v[4] + 0.005);
				cents = v[4] - dollars;
				cent = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(Conversion.Fix(cents * 100 + 0.5), "00");
				rpt.PrintPageLine(FileSystem.TAB(10), WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dollars, "#####,###"), ref 9) + " dollars and " + cent + " cents");
				FileSystem.PrintLine(fi, WinGlobal.lpad(ref Microsoft.VisualBasic.Compatibility.VB6.Support.Format(dollars, "#####,###"), ref 9) + " dollars and " + cent + " cents");
			}
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("       " + Checks3.payee);
			FileSystem.PrintLine(fi, "       " + Checks3.payee);
			rpt.PrintPageLine("       " + Checks3.addr1);
			FileSystem.PrintLine(fi, "       " + Checks3.addr1);
			rpt.PrintPageLine("       " + Checks3.addr2);
			FileSystem.PrintLine(fi, "       " + Checks3.addr2);
			if (!string.IsNullOrEmpty(Strings.Trim(Checks3.addr3))) {
				rpt.PrintPageLine("       " + Checks3.addr3);
				FileSystem.PrintLine(fi, "       " + Checks3.addr3);
			} else {
				rpt.PrintPageLine(" ");
			}
			FileSystem.PrintLine(fi, "============================================");
			FileSystem.PrintLine(fi, "");

			if (Strings.LCase(Strings.Left(Printer.DeviceName, 9)) == "printboss") {
				rpt.PrintPageLine("      " + Checks3.memo);
				Printer.NewPage();
			} else {
				for (i = 1; i <= 4; i++) {
					rpt.PrintPageLine("");
				}
			}
			//Close fi

		}
		private void APLaser_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{
			object i = null;

			//UPGRADE_WARNING: Arrays in structure tblControl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset tblControl = null;

			tblControl = GLdb.OpenRecordset("Control");
			tblControl.MoveFirst();
			tblControl.Edit();
			for (i = 0; i <= 2; i++) {
				if (this.optVoucher[i].Checked) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					tblControl.Fields["CheckVoucher"].Value = i;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			tblControl.Fields["CheckChange"].Value = this.chkChange.CheckState;
			tblControl.Update();
			tblControl.Close();
			//GLdb.Close
			//DBEngine.Workspaces(0).Close
			//DBEngine.SystemDB = appath & "pmsys.mdw"
			//DBEngine.DefaultUser = "Admin" ' UserID
			//Set PMSdb = DBEngine.Workspaces(0).OpenDatabase(PMSDataBase)
			//Set GLdb = DBEngine.Workspaces(0).OpenDatabase(DefaultDataBase, dbConsistent)
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Options", "Blankchk", Convert.ToString(vi));
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Printers", "APCheck", Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstPrinters, lstPrinters.SelectedIndex));

		}
//UPGRADE_WARNING: Event lstVersion.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
//UPGRADE_WARNING: ComboBox event lstVersion.Change was upgraded to lstVersion.TextChanged which has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		private void lstVersion_TextChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstVersion.SelectedIndex == 0) {
				lstPrinters.Enabled = true;
			} else {
				lstPrinters.Enabled = false;
			}

		}
//UPGRADE_WARNING: Event lstVersion.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
		private void lstVersion_SelectedIndexChanged(System.Object eventSender, System.EventArgs eventArgs)
		{

			if (lstVersion.SelectedIndex == 0) {
				lstPrinters.Enabled = true;
			} else {
				lstPrinters.Enabled = false;
			}

		}
		public void clearrecord()
		{

			//UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			System.Array.Clear(v, 0, v.Length);
			System.Array.Clear(Checks3.voucher, 0, Checks3.voucher.Length);
			Checks3.addr1 = " ";
			Checks3.addr2 = " ";
			Checks3.addr3 = " ";
			Checks3.addr4 = " ";
			Checks3.voidx = " ";

		}
	}

    public class voucher
    {
        public string vInvoice { get; set; }
        public string vDate { get; set; }
        public string vDesc { get; set; }
        public string vGross { get; set; }
        public string vDisc { get; set; }
        public string vNet { get; set; }
    }
    public class Layout
    {
        public string layout { get; set; }
        public string lcmp { get; set; }
        public string ldpt { get; set; }
        public string x1 { get; set; }
        public string voidx { get; set; }
        public string chkNum { get; set; }
        public string chkDate { get; set; }
        public string amount { get; set; }
        public string vendor { get; set; }
        public string payee { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string addr4 { get; set; }
        public voucher[] voucher { get; set; }
        public string memo { get; set; }
        public string crlf { get; set; }
}

}
