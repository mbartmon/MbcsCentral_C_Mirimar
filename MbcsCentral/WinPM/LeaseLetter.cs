using System;
using System.Windows.Controls;
using System.Drawing;
using System.Printing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
	public class LeaseLetter : IDisposable
	{
        MbcsUtils.Parameters parms = new MbcsUtils.Parameters(GlobalShared.appPath + WinpmGlobal.WINPM_CONFIG_FILE);

        long clientId = GlobalShared.user.Account.clientId;
        Report rpt;
        Tenants[] tenants;
        Building building;
        Owners owner;
        Font mainFont = new Font("Courier New", 12);
        Font fnt = null;
        string nam1 = null;
        string nam2 = null;
        System.DateTime newStart;
        decimal curRent = 0;
        decimal curLglRent = 0;
        decimal SC = 0;
        string lr2 = null;
        string lr1 = null;
        string tnam = null;
        string xsql = null;
        string oAddr1 = null;
        string oName = null;
        string oAddr2 = null;
        string newLease = null;
        string[] bldAddr = new string[3];
        string buffer;
        DateTime oldLease;
        System.DateTime leaseDate;
        int p;
		int p2;
		bool endofbuffer;
		short i;
		bool found;
		decimal ria;
		decimal lglRent;
		decimal ProposedRent1;
		decimal ProposedRent2;
        decimal xr = 0;
        decimal rent = 0;
        decimal lra1;
		decimal lra2;
		decimal ri1;
		decimal nr1;
		decimal nr2;
		decimal ri2;
		decimal pri2;
		decimal pri1;
		decimal capImp;
		decimal ac;
		string leasePrinter;
		string address2;
		string TenantName;
		string address1;
		string Address3;
        string bld = "";
		System.Windows.Controls.Image template;
		System.Windows.Controls.Image reverse;
        PrefRentDoc prefRent = null;
        PromptPayDoc promptPay = null;
		bool minTaken;
		System.DateTime moveindate;
		int years;
		decimal minimumIncrease1;
		decimal increaseRent;
		decimal minimumIncrease2;
		bool duplexEnabled;
		string duplexFile;
		int[] months = new int[13];
		string[] ra = new string[4];
		short retAdd;
		short tf;
		string telno;
		string paytoname;

        //*********************************
        //* Print Positions               *
        //*********************************
        double CurrentX = 0;
        double CurrentY = 0;

		private double TenNumX;
		private double TenNumY;
		private double CurDateX;
		private double CurDateY;
		private double TenantNameX;
		private double TenantNameY;
		private double aptX;
		private double OwnAddressX;
		private double OwnAddressY;
        // Item 1
		private double ExpDateX;
		private double ExpDateY;
        // Item 2
		private double colBX;
		private double colBY;
		private double colCchkX;
		private double colCchkY;
		private double colC1pX;
		private double colC1pY;
		private double colC1aX;
		private double colC1aY;
		private double colC2pX;
		private double colC2pY;
		private double colC2aX;
		private double colC2aY;
		private double colDX;
		private double colD1Y;
		private double colD2Y;
		private double colEX;
		private double colE1Y;
		private double colE2Y;
		private double colFX;
		private double colF1Y;
		private double colF2Y;
		private double colFchkX;
		private double colFchkY;
        //Const colGX = 7, colG1Y = 3.825, colG2Y = 4

		private double AddIncX;
		private double AddIncY;
		private double MsgAreaX;
		private double MsgAreaY;
        // Item 3
		private double SecDepX;
		private double SecDepY;
		private double Sec1X;
		private double Sec2X;
		private double SecY;
        // Item 4
		private double AddChgXdesc;
		private double AddChgY;
		private double AddChgXamt;
		private double AddChgXtotal;
        // Item 5
		private double prefRent1X;
		private double prefRent1Y;
		private double prefRent2X;
		private double prefRent2Y;
		private double PrefRentChkYes;
		private double PrefRentChkNo;
		private double PrefRentChkY;
        // Item 6
		private double NewRent1X;
		private double NewRent1Y;
		private double NewRent2X;
		private double NewRent2Y;
		private double NewChgsX;
		private double NewChgsY;
		private double NewTotal1X;
		private double NewTotal1Y;
		private double NewTotal2X;
		private double NewTotal2Y;
        // Item 7
		private double NewLeaseX;
		private double NewLeaseY;
		private double NewExp1X;
		private double NewExp1Y;
		private double NewExp2X;
		private double NewExp2Y;
        // Item 9
		private double SCRentX;
		private double SCRentY;
        // Bottom
		private double FinalRent1X;
		private double FinalRent1Y;
		private double FinalAC1X;
		private double FinalAC1Y;
		private double FinalTotal1X;
		private double FinalTotal1Y;
		private double FinalRent2X;
		private double FinalRent2Y;
		private double FinalAC2X;
		private double FinalAC2Y;
		private double FinalTotal2X;
		private double FinalTotal2Y;
		private double SigLine1X;
		private double SigLine1Y;
		private double SigLine2X;
		private double SigLine2Y;

        // Properties
        private decimal m_minimumRent;
        private int m_minimumYears;
        private bool useMinimumRent;
        private bool m_duplexEnabled = false;
        private string[] codes;
        private string seniorCitizenCode;
        private decimal one;
        private decimal two;                                               
		private decimal LRM1;
		private decimal LRM2;
		private decimal lr;
		private decimal lra;
        private bool m_prefRent;
        private bool m_promptPay;
        private int copies = 1;
        private bool OneFlag;
        private string type = "";
        private string bldStart;
        private string bldEnd;
        private DateTime expFrom;
        private DateTime expTo;
        private string[] tenKeys;
        private bool guidelinesUnknown = false;
        private bool useStatementOptions = false;
        private PrintQueue queue;
        bool canModify;

        public LeaseLetter(bool modify)
        {
            canModify = modify;
            parms.getConfigs();
            if (parms == null)
                return;
            TenNumX = parms.GetDouble("winpm.lease.TenNumX");
            TenNumY = parms.GetDouble("winpm.lease.TenNumY");
            CurDateX = parms.GetDouble("winpm.lease.CurDateX");
            CurDateY = parms.GetDouble("winpm.lease.CurDateY");
            TenantNameX = parms.GetDouble("winpm.lease.TenantNameX");
            TenantNameY = parms.GetDouble("winpm.lease.TenantNameY");
            aptX = parms.GetDouble("winpm.lease.aptX");
            OwnAddressX = parms.GetDouble("winpm.lease.OwnAddressX");
            OwnAddressY = parms.GetDouble("winpm.lease.OwnAddressY");
            // Item 1
            ExpDateX = parms.GetDouble("winpm.lease.ExpDateX");
            ExpDateY = parms.GetDouble("winpm.lease.ExpDateY");
            // Item 2
            colBX = parms.GetDouble("winpm.lease.colBX");
            colBY = parms.GetDouble("winpm.lease.colBY");
            colCchkX = parms.GetDouble("winpm.lease.colCchkX");
            colCchkY = parms.GetDouble("winpm.lease.colCchkY");
            colC1pX = parms.GetDouble("winpm.lease.colC1pX");
            colC1pY = parms.GetDouble("winpm.lease.colC1pY");
            colC1aX = parms.GetDouble("winpm.lease.colC1aX");
            colC1aY = parms.GetDouble("winpm.lease.colC1aY");
            colC2pX = parms.GetDouble("winpm.lease.colC2pX");
            colC2pY = parms.GetDouble("winpm.lease.colC2pY");
            colC2aX = parms.GetDouble("winpm.lease.colC2aX");
            colC2aY = parms.GetDouble("winpm.lease.colC2aY");
            colDX = parms.GetDouble("winpm.lease.colDX");
            colD1Y = parms.GetDouble("winpm.lease.colD1Y");
            colD2Y = parms.GetDouble("winpm.lease.colD2Y");
            colEX = parms.GetDouble("winpm.lease.colEX");
            colE1Y = parms.GetDouble("winpm.lease.colE1Y");
            colE2Y = parms.GetDouble("winpm.lease.colE2Y");
            colFX = parms.GetDouble("winpm.lease.colFX");
            colF1Y = parms.GetDouble("winpm.lease.colF1Y");
            colF2Y = parms.GetDouble("winpm.lease.colF2Y");
            colFchkX = parms.GetDouble("winpm.lease.colFchkX");
            colFchkY = parms.GetDouble("winpm.lease.colFchkY");
            //Const colGX = 7, colG1Y = 3.825, colG2Y = 4

            AddIncX = parms.GetDouble("winpm.lease.AddIncX");
            AddIncY = parms.GetDouble("winpm.lease.AddIncY");
            MsgAreaX = parms.GetDouble("winpm.lease.MsgAreaX");
            MsgAreaY = parms.GetDouble("winpm.lease.MsgAreaY");
            // Item 3
            SecDepX = parms.GetDouble("winpm.lease.SecDepX");
            SecDepY = parms.GetDouble("winpm.lease.SecDepY");
            Sec1X = parms.GetDouble("winpm.lease.Sec1X");
            Sec2X = parms.GetDouble("winpm.lease.Sec2X");
            SecY = parms.GetDouble("winpm.lease.SecY");
            // Item 4
            AddChgXdesc = parms.GetDouble("winpm.lease.AddChgXdesc");
            AddChgY = parms.GetDouble("winpm.lease.AddChgY");
            AddChgXamt = parms.GetDouble("winpm.lease.AddChgXamt");
            AddChgXtotal = parms.GetDouble("winpm.lease.AddChgXtotal");
            // Item 5
            prefRent1X = parms.GetDouble("winpm.lease.prefRent1X");
            prefRent1Y = parms.GetDouble("winpm.lease.prefRent1Y");
            prefRent2X = parms.GetDouble("winpm.lease.prefRent2X");
            prefRent2Y = parms.GetDouble("winpm.lease.prefRent2Y");
            PrefRentChkYes = parms.GetDouble("winpm.lease.PrefRentChkYes");
            PrefRentChkNo = parms.GetDouble("winpm.lease.PrefRentChkNo");
            PrefRentChkY = parms.GetDouble("winpm.lease.PrefRentChkY");
            // Item 6
            NewRent1X = parms.GetDouble("winpm.lease.NewRent1X");
            NewRent1Y = parms.GetDouble("winpm.lease.NewRent1Y");
            NewRent2X = parms.GetDouble("winpm.lease.NewRent2X");
            NewRent2Y = parms.GetDouble("winpm.lease.NewRent2Y");
            NewChgsX = parms.GetDouble("winpm.lease.NewChgsX");
            NewChgsY = parms.GetDouble("winpm.lease.NewChgsY");
            NewTotal1X = parms.GetDouble("winpm.lease.NewTotal1X");
            NewTotal1Y = parms.GetDouble("winpm.lease.NewTotal1Y");
            NewTotal2X = parms.GetDouble("winpm.lease.NewTotal2X");
            NewTotal2Y = parms.GetDouble("winpm.lease.NewTotal2Y");
            // Item 7
            NewLeaseX = parms.GetDouble("winpm.lease.NewLeaseX");
            NewLeaseY = parms.GetDouble("winpm.lease.NewLeaseY");
            NewExp1X = parms.GetDouble("winpm.lease.NewExp1X");
            NewExp1Y = parms.GetDouble("winpm.lease.NewExp1Y");
            NewExp2X = parms.GetDouble("winpm.lease.NewExp2X");
            NewExp2Y = parms.GetDouble("winpm.lease.NewExp2Y");
            // Item 9
            SCRentX = parms.GetDouble("winpm.lease.SCRentX");
            SCRentY = parms.GetDouble("winpm.lease.SCRentY");
            // Bottom
            FinalRent1X = parms.GetDouble("winpm.lease.FinalRent1X");
            FinalRent1Y = parms.GetDouble("winpm.lease.FinalRent1Y");
            FinalAC1X = parms.GetDouble("winpm.lease.FinalAC1X");
            FinalAC1Y = parms.GetDouble("winpm.lease.FinalAC1Y");
            FinalTotal1X = parms.GetDouble("winpm.lease.FinalTotal1X");
            FinalTotal1Y = parms.GetDouble("winpm.lease.FinalTotal1Y");
            FinalRent2X = parms.GetDouble("winpm.lease.FinalRent2X");
            FinalRent2Y = parms.GetDouble("winpm.lease.FinalRent2Y");
            FinalAC2X = parms.GetDouble("winpm.lease.FinalAC2X");
            FinalAC2Y = parms.GetDouble("winpm.lease.FinalAC2Y");
            FinalTotal2X = parms.GetDouble("winpm.lease.FinalTotal2X");
            FinalTotal2Y = parms.GetDouble("winpm.lease.FinalTotal2Y");
            SigLine1X = parms.GetDouble("winpm.lease.SigLine1X");
            SigLine1Y = parms.GetDouble("winpm.lease.SigLine1Y");
            SigLine2X = parms.GetDouble("winpm.lease.SigLine2X");
            SigLine2Y = parms.GetDouble("winpm.lease.SigLine2Y");

        }

        public void Dispose()
        {
            rpt.Dispose();
            mainFont.Dispose();
            fnt.Dispose();
        }

		public void PrintLetters()
		{
			object X2 = null;
			object x1 = null;
			int FD = 0;
			int j = 0;
			int year0 = 0;
			int month0 = 0;

			int fi = 0;
			int i = 0;
			string[] lin = null;
			int pi = 0;
			int li = 0;
			int copies = 0;
			int ic = 0;
			bool riderPrinted = false;
			int addrOption = 0;
			string[] names = null;
			int p = 0;
			string x = null;

            Control ctrl = Control.load(clientId);
            x = DateAndTime.Today.ToString();
            short payto = ctrl.PayToFlag;
            if (payto == 1)
            {
                paytoname = ctrl.PayToName;
            }
            //Return Address
            retAdd = ctrl.ReturnAddressFlag;
            switch (retAdd)
            {
                case 0:
                    //Owner Name/Address
                    break;
                case 1:
                    //Owner Name/This Address
                    ra[1] = ctrl.ReturnAddressA1;
                    ra[2] = ctrl.ReturnAddressA2;
                    ra[3] = ctrl.ReturnAddressA3;
                    break;
                case 2:
                    //This Name/This Address
                    ra[0] = ctrl.ReturnAddressBname;
                    ra[1] = ctrl.ReturnAddressB1;
                    ra[2] = ctrl.ReturnAddressB2;
                    ra[3] = ctrl.ReturnAddressB3;                                                
                    break;
            }
            for (i = 0; i <= 3; i++)
            {
                j = Strings.InStr(ra[i], Constants.vbCrLf);
                if (j > 0)
                {
                    ra[i] = Strings.Left(ra[i], j - 1);
                }
            }
            //Telephone Number
            tf = ctrl.TelephoneFlag;
            if (tf == 1)
            {
                telno = ctrl.TelephoneNumber;
            }
            ctrl = null;
            string filename = "";
            object obj=null;
            Main.parms.getItem(GlobalShared.CONFIG_WINPM_LEASE_LETTER, ref obj);
            filename = GlobalShared.appPath + "WinPM\\Files\\" + (string)obj;
            rpt = new Report("LeaeseLetter",mainFont.Name, mainFont.Size);
            template = Report.GetImage(filename);
            rpt.LineHeight = colC2aY - colC1aY;

			if (m_duplexEnabled) 
            {
                Main.parms.getItem(GlobalShared.CONFIG_WINPM_DUPLEX_FILE, ref obj);
                filename = GlobalShared.appPath + "WinPM\\Files\\" + (string)obj;
                reverse = Report.GetImage(filename);
				if (reverse == null) 
                {
					switch (MessageBox.Show("Duplex printing is enabled but the reverse-side image file cannot be loaded." + Constants.vbCrLf + "Print lease renewal letters anyway?","",MessageBoxButtons.YesNo)) {
						case System.Windows.Forms.DialogResult.Yes:
							m_duplexEnabled = false;
							break;
						case System.Windows.Forms.DialogResult.No:
							return;
							break;
					}
				}  
                else
                    rpt.Duplex = true;

			}

			increaseRent = m_minimumRent;
			years = m_minimumYears;

			string[] fra = new string[4];
			string[] frd = new string[4];
			months[1] = 31;
			months[2] = 28;
			months[3] = 31;
			months[4] = 30;
			months[5] = 31;
			months[6] = 30;
			months[7] = 31;
			months[8] = 31;
			months[9] = 30;
			months[10] = 31;
			months[11] = 30;
			months[12] = 31;
			x = DateAndTime.DateString;
			month0 = DateAndTime.Month(Convert.ToDateTime(x));
			year0 = DateAndTime.Year(Convert.ToDateTime(x));

			if (m_prefRent) {
				prefRent = new PrefRentDoc();
			}
			if (m_promptPay) {
				promptPay = new PromptPayDoc();
			}

			if (copies <= 0)
				copies = 1;

			if (OneFlag)
            {
                string s = null;
                foreach (string key in tenKeys) 
                {
                    s = key.PadRight(7);
                    Tenants[] ten = Tenants.load(s.Substring(0,3), s.Substring(3,4).Trim(), clientId);
				    if (ten != null) {
					    printIt(ten[0]);
				    }
                    ten = null;
			    }
            } else {
			    xsql = "SELECT * FROM " + Tenants.TABLE + " WHERE [" + Tenants.SQL_COL_BUILDING_NUMBER + "] BETWEEN '" + bldStart + "' AND '" + bldEnd + "'";
			    xsql += " AND [" + Tenants.SQL_COL_LEASE_EXPIRATION_DATE+ "] BETWEEN '" + expFrom.ToString() + "' AND '" + expTo.ToString() + "'";
			    if (!type.Trim().Equals("")) 
                {
				    xsql += " AND [" + Tenants.SQL_COL_TYPE + "] = '" + type.Trim() + "'";
			    }
                xsql += " AND " + Tenants.SQL_COL_CLIENT_ID + "= " + clientId.ToString();
			    xsql = xsql + " ORDER BY [" + Tenants.SQL_COL_BUILDING_NUMBER + "], [" + Tenants.SQL_COL_TENANT_NUMBER + "]";
                tenants = Tenants.Query(xsql, GlobalShared.NULL_DATE);
			    if (tenants != null && tenants.Length > 0) 
                {
				    foreach (Tenants tblTenant in tenants) 
                    {
					    Types tblTypes = Types.load(clientId, tblTenant.Type.Trim());
					    if (tblTypes == null || tblTypes.Lease)
					        printIt(tblTenant);

					    tblTypes = null;
				    }
			    } else {
				    MessageBox.Show("No tenants with expiring leases found");
			    }
            }
            // remove next line after testing
            rpt.PrintDocument(queue);
        }

        private void printIt(Tenants ten)
        {
            int ic = 0;
            string tnam = ten.TenantName;
			nam1 = tnam;
			nam2 = "";
            bool riderPrinted = false;

			// New name resolution routine
			string[,] names = new string[3, 3];
			WinpmGlobal.resolveNames(ref names, ten);
			nam1 = Strings.Trim(names[0, 0]) + " " + Strings.Trim(names[0, 1]);
			nam2 = Strings.Trim(names[1, 0]) + " " + Strings.Trim(names[1, 1]);
			moveindate = DateAndTime.Now;
			oldLease = DateAndTime.DateSerial(2000, 1, 1);
			if (ten.LeaseStartDate > GlobalShared.NULL_DATE) {
				oldLease = ten.LeaseStartDate;
			}

			moveindate = ten.MoveinDate;
             
			for (ic = 1; ic <= copies; ic++) {
                PrintFields(ten);
				//rpt.NewPage();
				if (m_duplexEnabled) {
                    rpt.NewPage();
					rpt.AddImage(reverse);
				}
                //rpt.PrintDocument(queue);  commented for testing only
                rpt.NewPage();
			}

            riderPrinted = false;
           
			// Preferential Rent Rider
			if (m_prefRent && (ProposedRent1 > 0 | ProposedRent2 > 0)) {
				//Printer.Duplex = PrinterObjectConstants.vbPRDPSimplex;
				for (ic = 1; ic <= copies; ic++) {
					prefRent.TenantName = tnam;
                    prefRent.Apartment = ten.Apartment;
					prefRent.ProposedRent1 = ProposedRent1;
					prefRent.ProposedRent2 = ProposedRent2;
					prefRent.LegalRent1 = curLglRent + ri1 + ria + lra1 + capImp;
					prefRent.LegalRent2 = curLglRent + ri2 + ria + lra2 + capImp;
					prefRent.Name1 = nam1;
					prefRent.Name2 = nam2;
					prefRent.printForm(newLease, owner.OwnerName, building.Address1, queue);
				}
				riderPrinted = true;
			}
           
			// Prompt Pay Rider
			if (m_promptPay && (ten.PromptPay1 > 0 || ten.PromptPay2 > 0)) {
				//Printer.Duplex = PrinterObjectConstants.vbPRDPSimplex;
				for (i = 1; i <= copies; i++) {
					bldAddr[0] = building.Address1;
					bldAddr[1] = building.Address2;
					promptPay.printForm(tnam, nam1, nam2, ten, owner.OwnerName, bldAddr, newLease, queue);
				}
				riderPrinted = true;
			}

            if (canModify)
            {
                ten.LeaseLetterFlag = true;
                if (lr1 == "Y" | lr2 == "Y")
                    ten.LowRentFlag = true;
                ten.ProposedRent = 0;
                ten.ProposedRent2 = 0;
                ten.PromptPay1 = 0;
                ten.PromptPay2 = 0;

                ten.save();
            }
        }

		private void PrintFields(Tenants ten)
        {
			//**********************************
			//* Field substitution subroutines *
			//**********************************
			int yr = 0;
			int dY = 0;
			int mn = 0;
			int yyear = 0;
			int ymonth = 0;

            // Print the Lease Renewal Template
            rpt.AddImage(template);
            rpt.SetFont = mainFont;

		    // Tenant Number - 05/08/2010
			CurrentX = TenNumX;
			CurrentY = TenNumY;
			rpt.PrintPageLine(ten.BuildingNumber + ten.TenantNumber,CurrentX, CurrentY);

			// CURRENT DATE
			CurrentX = CurDateX;
			CurrentY = CurDateY;
			rpt.PrintPageLine(DateAndTime.Today.ToString("MM/dd/yyyy"),CurrentX,CurrentY);

			// NAME
			CurrentX = TenantNameX;
			CurrentY = TenantNameY;
			rpt.PrintPageLine(nam1, CurrentX, CurrentY);
			if (!string.IsNullOrEmpty(nam2.Trim()))   
            {
                CurrentY = rpt.GetNextLinePostion(CurrentY);
				rpt.PrintPageLine(nam2, CurrentX, CurrentY);
			}

			// ADDRESS 1
			if (!bld.Equals(ten.BuildingNumber))
                getbld(ten);

            CurrentY = rpt.GetNextLinePostion(CurrentY);
			rpt.PrintPageLine(building.Address1.Trim() + "  Apt: " + ten.Apartment.Trim(), CurrentX, CurrentY);

			// APARTMENT
			// Printer.CurrentX = aptX
			// Printer.Print tblTenant("Apartment")

			// ADDRESS 2
            CurrentY = rpt.GetNextLinePostion(CurrentY);
			rpt.PrintPageLine(building.Address2, CurrentX, CurrentY);
			//Printer.Print tblTenant("Building Number") & tblTenant("Tenant Number")

			// owner name
            int addrOption = 0;

			if (!useStatementOptions) {
				addrOption = 0;
			} else {
				addrOption = retAdd;
			}
			switch (addrOption) {
				case 0:
					//Owner Name/Address
					oName = owner.OwnerName;
					oAddr1 = owner.Address1;
					oAddr2 = owner.Address2;
					break;
				case 1:
					//Owner Name/This Address
					oName = owner.OwnerName;
					oAddr1 = ra[1];
					oAddr2 = ra[2];
					break;
				case 2:
					//This Name/This Address
					oName = ra[0];
					oAddr1 = ra[1];
					oAddr2 = ra[2];
					break;
			}
			if (owner != null) {
				CurrentX = OwnAddressX;
				CurrentY = OwnAddressY;
				rpt.PrintPageLine(oName, CurrentX, CurrentY);
				CurrentY = rpt.GetNextLinePostion(CurrentY);
				rpt.PrintPageLine(oAddr1, CurrentX, CurrentY);
				CurrentY = rpt.GetNextLinePostion(CurrentY);
				rpt.PrintPageLine(oAddr2, CurrentX, CurrentY);
			}

			// LEASE EXPIRATION DATE
			if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE) 
            {
				CurrentX = ExpDateX;
				CurrentY = ExpDateY;
				mn = ten.LeaseExpirationDate.Month;
				dY = ten.LeaseExpirationDate.Day;
				yr = ten.LeaseExpirationDate.Year;
				string x = mn.ToString("00") + "    " + dY.ToString("00") + "    " + yr.ToString("0000");
				rpt.PrintPageLine(x, CurrentX, CurrentY);
			}

			// RENT
			decimal rent = ten.Rent;
			decimal curRent = rent;
			lglRent = ten.RentLegal;
			if (lglRent <= 0)
				lglRent = rent;
			curLglRent = lglRent;
			ProposedRent1 = ten.ProposedRent;
			ProposedRent2 = ten.ProposedRent2;
			if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE) 
            {
				newStart = newLeaseStart(ten.LeaseExpirationDate);
				ymonth =newStart.Month;
				yyear = newStart.Year;
				GetHist(ten, ymonth, yyear);
			}
			capImp = curRent - rent;
			if (capImp < 0) {
				capImp = 0;
			} else {
				curLglRent = lglRent - capImp;
			}
			CurrentX = colBX;
			CurrentY = colBY;
			rpt.PrintPageLine(curLglRent.ToString("#,###0.00").PadLeft(9), CurrentX, CurrentY);
			leaseDate = (ten.LeaseExpirationDate > GlobalShared.NULL_DATE ?  ten.LeaseExpirationDate : DateAndTime.Now );
			// ONE YEAR RENT INCREASE
			// ri1 = (lglRent * one) / 100
			ri1 = NewRent(curLglRent, one, years, leaseDate, moveindate, increaseRent, minimumIncrease1);
			pri1 = (ProposedRent1 > 0 ? ProposedRent1 - rent : (rent * one) / 100);
			CurrentX = colC1aX;
			CurrentY = colC1aY;
			rpt.PrintPageLine(ri1.ToString( "##,##0.00").PadLeft(9),CurrentX, CurrentY);

			// TWO YEAR RENT INCREASE
			//ri2 = (lglRent * two) / 100
			ri2 = NewRent(curLglRent, two, years, leaseDate, moveindate, increaseRent, minimumIncrease2);
			pri2 = (ProposedRent2 > 0 ? ProposedRent2 - rent : (rent * two) / 100);
			CurrentX = colC2aX;
			CurrentY = colC2aY;
			rpt.PrintPageLine(ri2.ToString("##,##0.00").PadLeft(9), CurrentX,CurrentY);

			// Guidlines Taken?
			if (guidelinesUnknown) {
				CurrentX = colCchkX;
				CurrentY = colCchkY;
				rpt.PrintPageLine("X", CurrentX, CurrentY);
			}

			// ONE YEAR PERCENT
			CurrentX = colC1pX;
			CurrentY = colC1pY;
			if (minTaken) {
				rpt.PrintPageLine("*****", CurrentX, CurrentY);
			} else {
				rpt.PrintPageLine(one.ToString("#0.00").PadLeft( 5), CurrentX, CurrentY);
			}

			// TWO YEAR PERCENT
			CurrentX = colC2pX;
			CurrentY = colC2pY;
			if (minTaken) {
				rpt.PrintPageLine("*****", CurrentX, CurrentY);
			} else {
				rpt.PrintPageLine(two.ToString("#0.00").PadLeft( 5), CurrentX, CurrentY);
			}
			// Low Rent Addition currently unused - 05/12/2010
			// The column is now used for capital improvment increase
			//
			// LOW RENT ADDITION ONE YEAR
			lra1 = 0;
			xr = lra;
			lr1 = " ";
			if (rent < lr & !LowRent(ten.LowRentFlag, ten)) 
            {
				while ((xr > 0 & rent + ri1 + xr > LRM1)) 
                {
					xr = xr - (decimal)0.01;
				}
				if (xr > 0) 
                {
					lra1 = xr;
					lr1 = "Y";
				    CurrentX = colDX;
					CurrentY = colD1Y;
					rpt.PrintPageLine(xr.ToString("#,##0.00").PadLeft( 8), CurrentX, CurrentY);
				}
			}

			// LOW RENT ADDITION TWO YEARS
			lra2 = 0;
			xr = lra;
			lr2 = " ";
			if (rent < lr & !LowRent(ten.LowRentFlag, ten)) 
            {
				while ((xr > 0 & rent + ri2 + xr > LRM2)) 
                {
					xr = xr - (decimal)0.01;
				}
				if (xr > 0) 
                {
					lra2 = xr;
					lr2 = "Y";
					CurrentX = colDX;
					CurrentY = colD2Y;
					rpt.PrintPageLine(xr.ToString("#,##0.00").PadLeft( 8), CurrentX, CurrentY);
				}
			}

			//RENT INCREASE ADJUSTMENT
			xr = ria;
			if (xr != 0) {
				CurrentX = colDX;
				CurrentY = colD1Y;
				rpt.PrintPageLine(xr.ToString( "#,##0.00").PadLeft(8), CurrentX, CurrentY);
				CurrentY = colD2Y;
				rpt.PrintPageLine(xr.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
			}

			// CAPITAL IMPROVEMENT INCREASE - COLUMN E
			if (capImp > 0) {
				CurrentX = colEX;
				CurrentY = colE1Y;
				rpt.PrintPageLine(capImp.ToString( "#,##0.00").PadLeft( 8), CurrentX, CurrentY);
				CurrentY = colE2Y;
				rpt.PrintPageLine(capImp.ToString( "#,##0.00").PadLeft( 8), CurrentX, CurrentY);
			}


			// TOTAL ADDITIONAL CHARGES
			xr = 0;
			ac = 0;
			SC = 0;
            Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
			if (chgs != null && chgs.Length > 0) {
				foreach (Charges chg in chgs) 
                {
					if (Array.IndexOf(codes, chg.ChargeCode) >= 0 && chg.Type == "2" && chg.Amount != 0) {
						xr = xr +chg.Amount;
					}
				}
			}
			chgs = null;
            if (seniorCitizenCode != null && seniorCitizenCode.Trim() != "")
            {
                chgs = Charges.loadForSecurity(ten.SequenceNumber, seniorCitizenCode, DateTime.Today);
                if (chgs != null)
                    SC = chgs[0].Amount;
                chgs = null;
            }
			if (xr != 0) 
            {
				ac = xr;
				// 05/06/2010
				//Printer.CurrentX = colFX: Printer.CurrentY = colF1Y
				//Printer.Print lpad(Format(xr, "##,##0.00"), 9)
				//Printer.CurrentX = colFX: Printer.CurrentY = colF2Y
				//Printer.Print lpad(Format(xr, "##,##0.00"), 9)
			}

			// Preferential Check Box at top - 05/10/2010
			if (ProposedRent1 > 0 | ProposedRent2 > 0) 
            {
				CurrentX = colFchkX;
				CurrentY = colFchkY;
				rpt.PrintPageLine("X", CurrentX, CurrentY);
			}

			// TOTAL NEW RENT ONE YEAR
			//xr = lglRent + ri1 + ria + ac + lra1 - 5 / 6 / 2010
			xr = curLglRent + ri1 + ria + lra1 + capImp;
			CurrentX = colFX;
			CurrentY = colF1Y;
			rpt.PrintPageLine(xr.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			nr1 = xr;

			// TOTAL NEW RENT TWO YEARS
			//xr = lglRent + ri2 + ria + ac + lra2 - 5 / 6 / 2010
			xr = curLglRent + ri2 + ria + lra2 + capImp;
			CurrentX = colFX;
			CurrentY = colF2Y;
			rpt.PrintPageLine(xr.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			nr2 = xr;

			// ADDITIONAL INCREASE
			for (i = 1; i <= 3; i++) 
            {
				if ((DateTime)ten.GetType().GetProperty("FutureDate"  + i.ToString("0")).GetValue(ten, null) > GlobalShared.NULL_DATE) 
                {
					xr = (decimal)ten.GetType().GetProperty("Future Rent " + i.ToString("0")).GetValue(ten, null);
					xr = xr - rent;
					if (xr > 0) 
                    {
						CurrentX = AddIncX;
						CurrentY = AddIncY;
						rpt.PrintPageLine("Subject to additional $" + xr.ToString("#,##0.00") + " increase on " + ten.GetType().GetProperty("Future Date " + i.ToString("0")).GetValue(ten, null), CurrentX, CurrentY);					
                    }
					break; 
				}
			}

			// SEPARATE CHARGE MESSAGE
			if (ac != 0) {
				CurrentX = AddIncX;
				CurrentY = rpt.GetNextLinePostion(AddIncY);
				rpt.PrintPageLine("Separate charge subject to adjustment by DHCR.", CurrentX, CurrentY);
			}

			// MINIMUM INCREASE MESSAGE
			if (minTaken) {
				CurrentX = MsgAreaX;
				CurrentY = MsgAreaY;
				rpt.PrintPageLine("***RENT GUIDELINES ORDER #44 ALLOWS A MINIMUM INCREASE OF " + minimumIncrease1.ToString("$###") + " (1 YEAR LEASE) OR ", CurrentX, CurrentY);
                CurrentY = rpt.GetNextLinePostion(CurrentY);
				rpt.PrintPageLine(minimumIncrease2.ToString("$###") + " (2 YEAR LEASE)", CurrentX, CurrentY);
				// IF TENANCY HAS COMMENCED SIX (6) OR MORE YEARS AGO"
			}

			// SECURITY
			CurrentX = SecDepX;
			CurrentY = SecDepY;
			rpt.PrintPageLine(ten.Security.ToString("#,##0.00").PadLeft(9), CurrentX, CurrentY);

			// ADD. SECURITY, ONE YEAR
			xr = ((ProposedRent1 > 0 ? ProposedRent1 : curLglRent + ri1 + capImp) * (decimal)ten.SecMultiple - ten.Security);
			if (xr < 0)
				xr = 0;
			rpt.FontColor = System.Windows.Media.Colors.Red;
			rpt.FontBold = true;
			CurrentX = Sec1X;
			CurrentY = SecY;
			rpt.PrintPageLine(xr.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);

			// ADD. SECURITY, TWO YEAR
			xr = ((ProposedRent2 > 0 ? ProposedRent2 : curLglRent + ri2 + capImp) * (decimal)ten.SecMultiple - ten.Security);
			if (xr < 0)
				xr = 0;
			CurrentX = Sec2X;
			CurrentY = SecY;
			rpt.PrintPageLine(xr.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
            rpt.FontColor = System.Windows.Media.Colors.Black;
            rpt.FontBold = false;
			
            // INDIVIDUAL ADDITIONAL CHARGES
			// 05/06/2010
			if (ac != 0) {
				//xsql = "SELECT * FROM Charges WHERE [Sequence Number] = " & tblTenant("Sequence Number")
				//Set tblCharges = PMSdb.OpenRecordset(xsql)
				//If tblCharges.RecordCount > 0 Then
				//    tblCharges.MoveFirst
				//    Do Until tblCharges.EOF
				//        If InStr(codes$, tblCharges("Charge Code")) > 0 And tblCharges("Type") = "2" And tblCharges("Amount") <> 0 Then
				//            xr = tblCharges("Amount")
				//            xsql = "SELECT * FROM Codes WHERE [Charge Code] = '" & tblCharges("Charge Code") & "'"
				//            Set tblCodes = PMSdb.OpenRecordset(xsql)
				//            If tblCodes.RecordCount > 0 Then
				//                cdesc$ = tblCodes("Charge Description")
				//            Else
				//                cdesc$ = "*not found*"
				//            End If
				//            Printer.CurrentX = AddChgXdesc: Printer.CurrentY = AddChgY
				//            Printer.Print cdesc$
				CurrentX = AddChgXamt;
				CurrentY = AddChgY;
				rpt.PrintPageLine(ac.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
				CurrentX = AddChgXtotal;
				CurrentY = AddChgY;
				rpt.PrintPageLine(ac.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
				//           Exit Do
				//       End If
				//       tblCharges.MoveNext
				//   Loop
				//End If
			}

			// Preferred Rent - 05/05/2010
			if (ProposedRent1 > 0 | ProposedRent2 > 0) {
				CurrentX = prefRent1X;
				CurrentY = prefRent1Y;
				rpt.PrintPageLine(ProposedRent1.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
				CurrentX = prefRent2X;
				CurrentY = prefRent2Y;
				rpt.PrintPageLine(ProposedRent2.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
				CurrentX = (!m_prefRent ? PrefRentChkNo : PrefRentChkYes);
				CurrentY = PrefRentChkY;
				rpt.PrintPageLine("X", CurrentX, CurrentY);
			}

			// New Rent (item 6) - 05/07/2010
			//If ac <> 0 Then
			CurrentX = NewChgsX;
			CurrentY = NewChgsY;
			rpt.PrintPageLine(ac.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
			//End If
			nr1 = (ProposedRent1 > 0 ? ProposedRent1 : nr1);
			CurrentX = NewRent1X;
			CurrentY = NewRent1Y;
			rpt.PrintPageLine(nr1.ToString("##,##0.00").PadLeft(9),CurrentX, CurrentY);
			CurrentX = NewTotal1X;
			CurrentY = NewTotal1Y;
			rpt.PrintPageLine((nr1 + ac).ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			nr2 = (ProposedRent2 > 0 ? ProposedRent2 : nr2);
			CurrentX = NewRent2X;
			CurrentY = NewRent2Y;
			rpt.PrintPageLine(nr2.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			CurrentX = NewTotal2X;
			CurrentY = NewTotal2Y;
			rpt.PrintPageLine((nr2 + ac).ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);

			// LEASE START DATE
			if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE) {
				mn = ten.LeaseExpirationDate.Month;
				dY = ten.LeaseExpirationDate.Day;
				yr = ten.LeaseExpirationDate.Year;
				if (dY >= months[mn]) {
					dY = 1;
					mn = mn + 1;
				} else {
					dY = dY + 1;
				}
				if (mn > 12) {
					mn = 1;
					yr = yr + 1;
				}
                string x = mn.ToString("00") + "  " + dY.ToString("00") + "  " + yr.ToString("0000");
				CurrentX = NewLeaseX;
				CurrentY = NewLeaseY;
				rpt.PrintPageLine(x, CurrentX, CurrentY);
                newLease = x;
			}

			// NEW LEASE EXPIRATION DATES
			if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE) {
				mn = ten.LeaseExpirationDate.Month;
				dY = ten.LeaseExpirationDate.Day;
				yr = ten.LeaseExpirationDate.Year;
				// ONE YEAR
                string x = mn.ToString("00") + "  " + dY.ToString("00") + "  " + (yr + 1).ToString("0000");
				CurrentX = NewExp1X;
				CurrentY = NewExp1Y;
				rpt.PrintPageLine(x, CurrentX, CurrentY);
				// TWO YEAR
                x = mn.ToString("00") + "  " + dY.ToString("00") + "  " + (yr + 2).ToString("0000");
				CurrentX = NewExp2X;
				CurrentY = NewExp2Y;
				rpt.PrintPageLine(x, CurrentX, CurrentY);
			}

			// Senior Citizen Rent = 05/08/2010
			if (SC != 0) {
				CurrentX = SCRentX;
				CurrentY = SCRentY;
				rpt.PrintPageLine((rent - System.Math.Abs(SC) + capImp).ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			}

			// Bottom Rent
			CurrentX = FinalRent1X;
			CurrentY = FinalRent1Y;
			rpt.PrintPageLine(nr1.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			CurrentX = FinalAC1X;
			CurrentY = FinalAC1Y;
			rpt.PrintPageLine(ac.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
			CurrentX = FinalTotal1X;
			CurrentY = FinalTotal1Y;
			rpt.FontColor= System.Windows.Media.Colors.Red;
			rpt.PrintPageLine((nr1 + ac).ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			rpt.FontColor= System.Windows.Media.Colors.Black;

			CurrentX = FinalRent2X;
			CurrentY = FinalRent2Y;
			rpt.PrintPageLine(nr2.ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			CurrentX = FinalAC2X;
			CurrentY = FinalAC2Y;
			rpt.PrintPageLine(ac.ToString("#,##0.00").PadLeft(8), CurrentX, CurrentY);
			CurrentX = FinalTotal2X;
			CurrentY = FinalTotal2Y;
			rpt.FontColor= System.Windows.Media.Colors.Red;
			rpt.PrintPageLine((nr2 + ac).ToString("##,##0.00").PadLeft(9), CurrentX, CurrentY);
			rpt.FontColor = System.Windows.Media.Colors.Black;

			// Signature Lines - 05/08/2010
			CurrentX = SigLine1X;
			CurrentY = SigLine1Y;
			rpt.FontColor = System.Windows.Media.Colors.Red;
			rpt.FontBold = true;
			rpt.PrintPageLine(nam1.Trim() + " sign here X:", CurrentX, CurrentY);
			if (!string.IsNullOrEmpty(nam2.Trim())) 
            {
				CurrentX = SigLine2X;
				CurrentY = SigLine2Y;
				rpt.PrintPageLine(nam2.Trim() + " sign here X:", CurrentX, CurrentY);
			}
			rpt.FontColor = System.Windows.Media.Colors.Black;
            rpt.FontBold = false;

        }
		
        private void getbld(Tenants ten)
        {
            building = Building.loadByBuildingNumber(ten.BuildingNumber, clientId);
			if (building != null) 
            {
				bld = building.BuildingNumber;
				owner = Owners.loadByOwnerNumber(building.OwnerNumber, clientId);
			}
        }
		
        private void GetHist(Tenants ten, int ymonth, int yyear)
        {

            DateTime x1;
            DateTime x2;

            if (oldLease > new DateTime(2000, 1, 1))
                if (oldLease.Year == yyear - 1 && oldLease.Month == 8 & oldLease.Day > 1)
                    return;

            if (ymonth < 10)
            {
				x1 = new DateTime(yyear - 1, 9, 1);
				x2 = new DateTime(yyear - 1, 9, 30);
			} else {
				x1 = new DateTime(yyear, 9, 1);
				x2 = new DateTime(yyear, 9, 30);
			}
            HistChg[] hist = HistChg.loadByTenantIdDate(ten.SequenceNumber, x1, x2, "=");
			if (hist != null && hist.Length > 0) 
				if (hist[0].Amount > 0 && rent > hist[0].Amount)
						rent = hist[0].Amount;
		}

		private System.DateTime newLeaseStart(System.DateTime expd)
		{
			int yr = 0;
			int dY = 0;
			int mn = 0;

			mn = expd.Month;
			dY = expd.Day;
			yr = expd.Year;
			if (dY >= months[mn]) {
				dY = 1;
				mn = mn + 1;
			} else {
				dY = dY + 1;
			}
			if (mn > 12) {
				mn = 1;
				yr = yr + 1;
			}
			return new DateTime(yr, mn, dY);

		}
		private decimal NewRent(decimal r, decimal p, int years, System.DateTime leaseDate, System.DateTime moveindate, decimal increaseRent, decimal minimumIncrease)
		{
			decimal functionReturnValue = 0;
			DateTime d1 = default(DateTime);
			DateTime d2 = default(DateTime);

			minTaken = false;
			if (leaseDate > GlobalShared.NULL_DATE) {
                d1 = leaseDate.AddDays(1);
				//d1 = new DateTime(leaseDate.Year, leaseDate.Month, leaseDate.Day + 1);
                d2 = moveindate.AddYears(years);
				//d2 = new DateTime(moveindate.Year + years, moveindate.Month, moveindate.Day);
			}
			if (useMinimumRent && leaseDate > GlobalShared.NULL_DATE && r < increaseRent && d2 <= d1) {
				functionReturnValue = minimumIncrease;
				minTaken = true;
			} else {
				functionReturnValue = (r * p) / 100;
			}
			return functionReturnValue;

		}

		public bool LowRent(bool fr, Tenants ten)
		{
			bool functionReturnValue = false;
			int yyear = 0;
			int ymonth = 0;
			int xyear = 0;
			int xmonth = 0;

			if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE) 
            {
				xmonth = ten.LeaseExpirationDate.Month;
				xyear = ten.LeaseExpirationDate.Year;
				if (ten.LeaseStartDate > GlobalShared.NULL_DATE) 
                {
					ymonth = ten.LeaseStartDate.Month;
					yyear = ten.LeaseStartDate.Year;
					if (xyear - yyear > 1) {
						ten.LowRentFlag = false;
						ten.save();
						functionReturnValue = false;
					} else {
						functionReturnValue = ten.LowRentFlag;
					}
				}
			}
			return functionReturnValue;
		}

        private void print_template()
		{
			//Printer Printer = new Printer();

			 // ERROR: Not supported in C#: OnErrorStatement

			//Printer.ScaleMode = ScaleModeConstants.vbInches;
			//Printer.PaintPicture(template, 0, 0, 8.5, 10.9);
			//Printer.ScaleMode = ScaleModeConstants.vbInches;
		}

		private void print_reverse()
		{
			//Printer Printer = new Printer();

			 // ERROR: Not supported in C#: OnErrorStatement

			//Printer.ScaleMode = ScaleModeConstants.vbInches;
			//Printer.PaintPicture(reverse, 0, 0, 8.5, 11);
			//Printer.ScaleMode = ScaleModeConstants.vbInches;
		}
       
        // Properties
        public string LeaseFile { get; set; }
        public string ReverseFile { get; set; }
        public string BldStart
        {
            set{bldStart=value;}
        }
        public string BldEnd
        {
            set { bldEnd=value;}
        }
        public string Type
        {
            set { type = value; }
        }
        public DateTime ExpFrom
        {
            set {expFrom=value.Date;}
        }
        public DateTime ExpTo
        {
            set{expTo=value.Date;}
        }
        public string[] Codes
        {
            set { codes = value; }
        }
        public string SeniorCitizenCode
        {
            set { seniorCitizenCode = value; }
        }
        public bool IndividualTenants
        {
            set{OneFlag=value;}
        }
        public decimal OneYear
        {
            set{one=value;}
        }
        public decimal TwoYear
        {
            set{two=value;}
        }
        public bool UseMinimumRent
        {
            set {useMinimumRent=value;}
        }
        public decimal MinimumRent
        {
            set { m_minimumRent = value;}
        }
        public int MinimumYears
        {
            set { m_minimumYears = value; }
        }
        public decimal LowRentOneYear
        {
            set{ LRM1 = value;}
        }
        public decimal LowRentTwoYear
        {
            set{LRM2=value;}
        }
        public bool DuplexEnabled
        {
            set {m_duplexEnabled = value;}
        }
        public bool PrefRent
        {
            set {m_prefRent = value;}
        }
        public bool PromptPay
        {
            set {m_promptPay = value;}
        }
        public bool IndividualFlag
        {
            set {OneFlag = value;}
        }
        public string[] TenantKeys
        {
            set {tenKeys = value;}
        }
        public bool UseStatementOptions
        {
            set { useStatementOptions = value; }
        }
        public bool GuidelinesUnknown
        {
            set { guidelinesUnknown = value; }
        }
        public PrintQueue Queue
        {
            set { queue = value; }
        }
	}
}
