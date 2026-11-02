using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.Compatibility.VB6;
using System.Windows.Documents;

namespace MbcsCentral.WinPM
{
    class StmtLaser : ReportBase
    {
        private string m_startingBuilding;
        private string m_endingBuilding;
        private string m_startingTenant;
        private string m_endingTenant;
        private bool m_ACHBypass;
        private System.DateTime m_closingdate;
        private System.DateTime m_billingdate;
        private System.Printing.PrintQueue printer;
        // Prior Month Payment Variables
        private DateTime PriorStartDate;
        private ArrayList ChargeCodes = new ArrayList();
        public string StartingBuilding
        {
            set { m_startingBuilding = value; }
        }
        public string EndingBuilding
        {
            set { m_endingBuilding = value; }
        }
        public string StartingTenant
        {
            set { m_startingTenant = value; }
        }
        public string EndingTenant
        {
            set { m_endingTenant = value; }
        }
        public System.DateTime ClosingDate
        {
            set { m_closingdate = value; }
        }
        public System.DateTime BillingDate
        {
            set { m_billingdate = value; }
            get { return m_billingdate; }
        }

        public bool LockBoxScanLine { get; set; } = false;

        public bool PrintPriorPayments { get; set; } = false;

        public ArrayList OneByOne { get; set; } = null;
        public bool ACHonly { get; set; } = false;
        public bool EmailStmts { get; set; } = false;
        public Tenants[] PassedTenants { get; set; } = null;
        public int iTen { get; set; } = -1;
        public string PdfFile { get; set; } = "";
         
        static string title = "Tenant Statements";
        Report rpt; 

        long clientId = GlobalShared.user.Account.clientId;

        Dictionary<string, Types> dictTenantType = new Dictionary<string,Types>();
        Dictionary<string, string> dictLegal = new Dictionary<string, string>();
        Dictionary<string, string> dictType = new Dictionary<string, string>();
        Dictionary<string, string> dictBuilding = new Dictionary<string, string>();

        const int maxcharges = 19;

        string telno;
        bool printApt;
        string rent_desc;
        decimal cbal;
        decimal tbal;
        decimal[] camt = new decimal[maxcharges + 1];
        string[] cdesc = new string[maxcharges + 1];
        decimal rent;
        decimal Paid;
        decimal arr;
        decimal Bal;
        short k;
        decimal tchg;
        decimal total;
        decimal rentlegal;
        System.DateTime due;
        int maxdetail;
        decimal chrg;
        string[] a = new string[5];
        string cd;
        string bldkey;
        string[] lines = new string[29];
        bool align;
        string LegalCode;
        string[] msg_gen = new string[4];
        short legal_loc;
        short gen_loc;
        short type_loc;
        short building_loc;
        short payto;
        string paytoname;
        string lname;
        string[] ra = new string[4];
        short retAdd;
        short tf;
        short fi;

        int generalMsgColor;
        int legalMsgColor;
        int TypeMsgColor;
        int BuildingMsgColor;

        NACHA_Layout[] NachaControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
        string NachaMessage = "";
        int NachaMsgArea;
        int NachaTemplateMsg = 5;
        string NachaInsert = "[PayDate]";
        string StarPath = "Asterisk_blue.bmp";
        System.Windows.Controls.Image Star;

        string fontname = "Courier New";
        float fontsize = 11;

        // Position constants
        const double TopNameX = -7;
        const double TopNameY = -4.5;
        const double ChgDesc1X = -6.5;
        const double ChgDesc2X = 28;
        const double FirstY = 3.5;
        const double Amt1X = 18.5;
        const double Amt2X = 51.5;
        const double TotalX = 50;
        const double TotalY = 16;
        const double PayToX = 5;
        const double PayToY = 19.5;
        const double MsgAreaX = 6;
        const double MsgAreaY = 21.5;
        const double AcctX = -5;
        const double AcctY = 43;
        const double ScanLineX = 18;
        const double ScanLineY = 39;
        const double ScanLineSize = 27;
        const double DueDateX = 44;
        const double DueDateY = 36;
        const double BottomNameX = - 5 ;
        const double BottomNameY = 39;
        const double LegalCodeY1 = 48;
        const double LegalCode1X = 25;
        const double LegalCodeY2 = 37;
        const double LegalCode2X = 48;
        const double TotalDueX = 42;
        const double TotalDueY = 46;
        const double CorpNameX = - 5;
        const double CorpNameY = 44;
        const double CodeX = 15;
        const double CodeY = BottomNameY;
        const int MAX_DESC_SIZE = 28;

        // $Id: StmtLaser.cs,v 1.7 2013/09/12 00:23:09 mike Exp $

        public StmtLaser()
        {
            rpt = new Report(title, fontname, fontsize);
            //rpt.LeftMarginInches = (Single)0.25;
            //rpt.TopMarginInches = (Single)0.25;
            rpt.BottomMargin = (Single)0.25;
        }

        public StmtLaser(string StartBuilding, string StartTenant, string EndBuilding, string EndTenant,
                         DateTime ClosingDate, DateTime BillingDate, bool LockBox, System.Printing.PrintQueue printer = null)
        {
            m_startingBuilding = StartBuilding;
            m_startingTenant = StartTenant;
            m_endingBuilding = EndBuilding;
            m_endingTenant = EndTenant;
            m_closingdate = ClosingDate;
            m_billingdate = BillingDate;
            this.LockBoxScanLine = LockBox;
            this.printer = printer;
        }

        public override void Process()
        {
            Print();
        }
        public void msg_lines(short t,  double l, Tenants ten)
		{
			int p = 0;
            double CurrentX;
            double CurrentY;
            System.Drawing.Color clr;

            switch (t)
            {
                case 1:
                    break;
                case 2:
                    l += 3;
                    break;
                case 3:
                    l += 6;
                    break;
                case 4:
                    l += 9;
                    break;
                case 5:
                    return;
                    break;
            }
            Font fnt = new Font("Arial", 12);

            if (t == 4 && PrintPriorPayments)
            {
                HistPay[] priorPayments = HistPay.loadByTenantDate(ten.SequenceNumber, PriorStartDate, m_closingdate);
                if (priorPayments != null && priorPayments.Length > 0)
                {
                    string paymentsLine = "";
                    CurrentY = l;
                    foreach (HistPay pay in priorPayments)
                    {
                        //if (ChargeCodes.Contains(pay.Code))
                        //{
                        paymentsLine += (paymentsLine.Length > 0 ? " / " : "Prior Payments: ") + pay.PayDate.ToShortDateString() + " " + pay.Amount.ToString("$###,##0.00");
                        //}
                    }
                    rpt.PrintPageLine(paymentsLine, -5, CurrentY, fnt);
                }
            }
            else
            {
                if (t == gen_loc)
                {
                    string[] msg = new string[4];
                    for (int i = 1; i <= 3; i++)
                    {
                        if (ten.UseACH && !NachaMessage.Trim().Equals("") && i == NachaMsgArea)

                        {
                            if (ten.PaymentDay < 1 || ten.PaymentDay > DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month))
                                ten.PaymentDay = 1;
                            msg[i] = NachaMessage.Replace(NachaInsert, new DateTime(BillingDate.Year, BillingDate.Month, ten.PaymentDay).ToShortDateString());
                        }
                        else
                            msg[i] = msg_gen[i];
                    }
                    clr = new System.Drawing.Color();
                    clr = System.Drawing.ColorTranslator.FromWin32(generalMsgColor);
                    rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
                    rpt.FontBold = true;
                    CurrentY = l;
                    CurrentX = MsgAreaX;
                    rpt.PrintPageLine(msg[1], CurrentX, CurrentY, fnt);
                    CurrentX = MsgAreaX;
                    CurrentY += 1;
                    rpt.PrintPageLine(msg[2], CurrentX, CurrentY, fnt);
                    CurrentX = MsgAreaX;
                    CurrentY += 1;
                    rpt.PrintPageLine(msg[3], CurrentX, CurrentY, fnt);
                    rpt.FontColor = System.Windows.Media.Colors.Black;
                    rpt.FontBold = false;
                }

                if (t == legal_loc)
                {
                    if (ten.LegalCode != null && dictLegal.ContainsKey(ten.LegalCode.ToLower()))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(legalMsgColor);
                        rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
                        rpt.FontBold = true;
                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        rpt.PrintPageLine(dictLegal[ten.LegalCode.ToLower()], CurrentX, CurrentY, fnt);
                        rpt.FontColor = System.Windows.Media.Colors.Black;
                        rpt.FontBold = false;
                    }
                }

                if (t == type_loc)
                {
                    if (ten.Type != null && dictType.ContainsKey(ten.Type.ToLower()))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(TypeMsgColor);
                        rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
                        rpt.FontBold = true;
                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        rpt.PrintPageLine(dictType[ten.Type.ToLower()], CurrentX, CurrentY, fnt);
                        rpt.FontColor = System.Windows.Media.Colors.Black;
                        rpt.FontBold = false;
                    }
                }


                if (t == building_loc)
                {
                    if (ten.BuildingNumber != null && dictBuilding.ContainsKey(ten.BuildingNumber))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(BuildingMsgColor);
                        rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
                        rpt.FontBold = true;
                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        rpt.PrintPageLine(dictBuilding[ten.BuildingNumber], CurrentX, CurrentY, fnt);
                        rpt.FontColor = System.Windows.Media.Colors.Black;
                        rpt.FontBold = false;
                    }

                }
            }
        }

        public void Print()
		{
            //rpt = new Report(title, fontname, fontsize) { PrintFlag = true, BottomMargin=0 };
            //if (!rpt.ChoosePrinter())
            //{
            //    MessageBox.Show("No printer selected.");
            //    return;
            //}

            if (NachaControl != null && NachaControl.Length >= 8)
            {
                NachaMessage = (string)NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Contents;
                NachaMsgArea = (int)NachaControl[(int)NACHA_Layout.Control_Types.StatementMessage - 1].Length;
            }
            rpt = new Report(title, fontname, fontsize) { BottomMargin = 0 };
            short i = 0;
			short j = 0;
			string DefaultPrinter = null;
			string stmtPrinter = null;

            //Get Tenant Type Control info
            Types[] typs = Types.loadAll(clientId);
			foreach (Types typ in typs)
            {
                try
                {
                    dictTenantType.Add(typ.Type.ToLower().Trim(), typ);
                }
                catch (Exception ex)
                { }
			}

            Control ctrl = Control.load(clientId);
            m_closingdate = ctrl.ClosingDate;
			m_billingdate = ctrl.BillingDate;
            m_ACHBypass = ctrl.ACHBypass;
            PriorStartDate = new DateTime(m_closingdate.Year, m_closingdate.Month, 1);
            // Message data
            if (ctrl.StatementMessage1 != null && ctrl.StatementMessage1.Trim() != "" )
				msg_gen[1] = ctrl.StatementMessage1;
			if (ctrl.StatementMessage2 != null && ctrl.StatementMessage2.Trim() != "" )
				msg_gen[2] = ctrl.StatementMessage2;
				if (ctrl.StatementMessage3 != null && ctrl.StatementMessage3.Trim() != "" )
				msg_gen[3] = ctrl.StatementMessage3;
			gen_loc = (short) (ctrl.GeneralMsgLocation != null ? ctrl.GeneralMsgLocation : 0);
			legal_loc = (short) (ctrl.LegalMsgLocation != null ? ctrl.LegalMsgLocation : 0);
			type_loc = (short) (ctrl.TypeMsgLocation != null ? ctrl.TypeMsgLocation : 0);
            building_loc = (short)(ctrl.BuildingMsgLocation != null ? ctrl.BuildingMsgLocation : 0);

            generalMsgColor = (ctrl.GeneralColor != null ? ctrl.GeneralColor : 0);
            legalMsgColor = (ctrl.LegalColor != null ? ctrl.LegalColor : 0);
            TypeMsgColor = (ctrl.TypeColor != null ? ctrl.TypeColor : 0);
            BuildingMsgColor = (ctrl.BuildingMsgColor != null ? ctrl.BuildingMsgColor : 0);

            //Payto data
            payto = ctrl.PayToFlag;
			if (payto == 1) {
				paytoname = ctrl.PayToName;
			}
			//Return Address
            for (i = 0; i < ra.Length; i++)
                ra[i] = "";
            retAdd = ctrl.ReturnAddressFlag;
			switch (retAdd) {
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

            for (i = 1; i <= 3; i++)
            {
                if (ra[i].EndsWith("\\n"))
					ra[i] = ra[i].Substring(0,ra[i].Length - 2);
			}
       
			//Telephone Number
            tf = (short) ctrl.TelephoneFlag;			
			if (tf == 1) {
				telno = ctrl.TelephoneNumber;
			}

            LegalMsg[] lgl = LegalMsg.loadAll(clientId);
            if (lgl != null && lgl.Count() > 0)
                foreach (LegalMsg lg in lgl)
                {
                    try
                    {
                        dictLegal.Add(lg.Code.ToLower().Trim(), lg.Message);
                    }
                    catch { }
                }
            TypeMsg[] types = TypeMsg.loadAll(clientId);
            if (types != null && types.Count() > 0)
                foreach (TypeMsg type in types)
                {
                    try
                    {
                        dictType.Add(type.Type.ToLower().Trim(), type.Message);
                    }
                    catch { }
                }

            BuildingMsg[] buildingMgs = BuildingMsg.load();
            if (buildingMgs != null && buildingMgs.Length > 0)
                foreach (BuildingMsg msg in buildingMgs)
                    dictBuilding.Add(msg.BuildingNumber.Trim(), msg.Message);
            stmts_process();

            if (!PdfFile.Trim().Equals(""))
            {
                rpt.DocToPdf(PdfFile);
            }
            else
                rpt.PrintDocument(printer);
		}

        public void stmts_process()
        {
            Building tblBuilding = null;
            string xsql = null;
            string x = null;
            short i = 0;
            int p = 0;
            bool First = false;

            First = true;
            bldkey = "";

            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting Print Statements", false);
            Tenants[] tens;
            if (EmailStmts)
                tens = PassedTenants;
            else
                tens = Tenants.loadForStatements(m_startingBuilding, m_endingBuilding, clientId, null, OneByOne);
            if (tens == null || tens.Count() == 0)
            {
                return;
            }
            Tenants ten = null;
            if (EmailStmts)
            {
                if (iTen >= 0)
                    ten = tens[iTen];
            }
            else
            {


                foreach (Tenants aTen in tens)
                {
                    ten = aTen;
                    // If Print only ACH then bypass if tenant not ACH 
                    if (!m_ACHBypass && ((ACHonly && !ten.UseACH) || !ACHonly && ten.UseACH))
                        if (!EmailStmts)
                            continue;
                    // Before start of range?
                    if (String.Compare((ten.BuildingNumber + ten.TenantNumber.Trim()), (m_startingBuilding + m_startingTenant.Trim())) < 0)
                        continue;
                    // End of range?
                    if (String.Compare((ten.BuildingNumber + ten.TenantNumber.Trim()), (m_endingBuilding + m_endingTenant.Trim())) > 0)
                        break;
                    if (EmailStmts && !ten.SendEmail)
                        continue;
                    // Vacant?
                    if (ten.TenantName.StartsWith("*"))
                        continue;
                    if (ten.MoveinDate > m_billingdate)
                        continue;
                    rent_desc = "RENT              ";
                    printApt = true;
                    System.Array.Clear(camt, 0, camt.Length);
                    System.Array.Clear(cdesc, 0, cdesc.Length);
                    cbal = 0;
                    tchg = 0;
                    rent = ten.Rent;
                    arr = ten.Arrears;
                    Paid = ten.Paid;
                    Bal = rent + arr - Paid;
                    k = 0;
                    if (Bal == 0)
                    {
                        maxdetail = maxcharges;
                    }
                    else
                    {
                        maxdetail = maxcharges - 1;
                    }
                    rentlegal = ten.RentLegal;
                    //************************
                    // FIND FUTURE RENT AMOUNT
                    //************************
                    FutureRents[] futures = FutureRents.loadBySequence(ten.SequenceNumber);
                    if (futures != null && futures.Length > 0)
                    {
                        foreach (FutureRents future in futures)
                        {
                            if (future.FutureDate > GlobalShared.NULL_DATE && future.FutureDate <= m_billingdate && future.FutureAmount != 0)
                            {
                                rent = future.FutureAmount;
                                if (future.FutureLegal != 0)
                                    rentlegal = future.FutureLegal;
                                break;
                            }
                        }
                    }


                    if (ten.FutureDate1 > GlobalShared.NULL_DATE && ten.FutureDate1 <= m_billingdate)
                    {
                        rent = ten.FutureRent1;
                        if (ten.FutureLegal1 != 0)
                        {
                            rentlegal = ten.FutureLegal1;
                        }
                    }
                    else if (ten.FutureDate2 > GlobalShared.NULL_DATE && ten.FutureDate2 <= m_billingdate)
                    {
                        rent = ten.FutureRent2;
                        if (ten.FutureLegal2 != 0)
                        {
                            rentlegal = ten.FutureLegal2;
                        }
                    }
                    else if (ten.FutureDate3 > GlobalShared.NULL_DATE && ten.FutureDate3 <= m_billingdate)
                    {
                        rent = ten.FutureRent3;
                        if (ten.FutureLegal3 != 0)
                        {
                            rentlegal = ten.FutureLegal3;
                        }
                    }

                    if (rentlegal > rent)
                    {
                        chrg = rentlegal - rent;
                        rent = rentlegal;
                        k++;
                        camt[k] = -chrg;
                        cdesc[k] = "PREFERENTIAL RENT CREDIT";
                        tchg = -chrg;
                    }

                    // Process Charges

                    Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber, true);
                    if (chgs != null && chgs.Count() > 0)
                    {
                        foreach (Charges chg in chgs)
                        {
                            if (chg.StartingDate > m_billingdate)
                                continue;
                            cbal = chg.Amount + chg.Arrears - chg.Paid;
                            chrg = cbal;

                            // Test for future charge amount
                            if (chg.FutureDate != GlobalShared.NULL_DATE && chg.FutureDate <= m_billingdate)
                            {
                                chg.Amount = chg.FutureAmount;
                            }

                            if (chg.Type.Equals("2") && (chg.StartingDate != m_billingdate))
                            {
                                if (chg.EndingDate <= GlobalShared.NULL_DATE || chg.EndingDate >= m_billingdate)
                                {
                                    chrg = cbal + chg.Amount;
                                }
                            }

                            if (chg.Type.Equals("1"))
                            {
                                if (chg.StartingDate.Month != m_billingdate.Month || chg.StartingDate.Year != m_billingdate.Year)
                                {
                                    if (chg.EndingDate <= GlobalShared.NULL_DATE || chg.EndingDate >= m_billingdate)
                                    {
                                        chrg = chg.Amount + chg.Arrears - chg.Paid;
                                    }
                                }
                            }
                            if (chrg != 0 && chg.ShowOnBill)
                            {
                                k++;
                                if (k < maxdetail)
                                {
                                    camt[k] = chrg;
                                    //ChargeCodes cde = ChargeCodes.load(chg.ChargeCode,clientId);
                                    cdesc[k] = (chg.Description == null ? "" : chg.Description);
                                    if (chg.OnBill)
                                    {
                                        cdesc[k] = cdesc[k] + "[" + chg.Comment + "]";
                                    }
                                    cdesc[k] = cdesc[k].PadRight(MAX_DESC_SIZE).Substring(0, MAX_DESC_SIZE);
                                }
                                else
                                {
                                    camt[maxdetail] = camt[maxdetail] + chrg;
                                    cdesc[maxdetail] = "OTHER CHARGES";
                                }
                            }
                            if (chg.ChargeToTenant)
                            {
                                tchg = tchg + chrg;
                                Bal = Bal + chg.Paid;
                            }
                        }
                    }
                    total = rent + tchg + Bal;
                    if (!ten.BuildingNumber.Equals(bldkey))
                    {
                        bldkey = ten.BuildingNumber;
                        tblBuilding = Building.loadByBuildingNumber(ten.BuildingNumber, clientId);
                        if (tblBuilding != null)
                        {
                            Owners tblOwner = Owners.loadByOwnerNumber(tblBuilding.OwnerNumber, clientId);
                            if (tblOwner != null && payto == 0)
                            {
                                if (string.IsNullOrEmpty(tblOwner.BillingName))
                                {
                                    paytoname = tblOwner.OwnerName;
                                }
                                else
                                {
                                    paytoname = tblOwner.BillingName;
                                }
                            }
                            //for (int j = 0; j < ra.Length; j++)
                            //    ra[j] = "";

                            switch (retAdd)
                            {
                                case 0:
                                    //Owner Name/Address
                                    if (string.IsNullOrEmpty(tblOwner.BillingName))
                                    {
                                        ra[0] = tblOwner.OwnerName;
                                    }
                                    else
                                    {
                                        ra[0] = tblOwner.BillingName;
                                    }
                                    ra[1] = tblOwner.Address1;
                                    ra[2] = tblOwner.Address2;
                                    break;
                                case 1:
                                    //Owner Name/This Address
                                    if (string.IsNullOrEmpty(tblOwner.BillingName))
                                    {
                                        ra[0] = tblOwner.OwnerName;
                                    }
                                    else
                                    {
                                        ra[0] = tblOwner.BillingName;
                                    }
                                    break;
                                case 2:
                                    //This Name/This Address
                                    break;
                            }
                            if (tf == 0)
                            {
                                telno = tblOwner.Telephone;
                            }
                        }
                    }
                    System.Array.Clear(a, 0, a.Length);
                    a[0] = ten.TenantName.Trim();
                    a[1] = tblBuilding.Address1;
                    a[2] = tblBuilding.Address2;
                    a[3] = new String(' ', 30);
                    if (ten.AlternateBillingAddressFlag)
                    {
                        a[0] = (ten.AltBillName == null ? "" : ten.AltBillName.Trim());
                        a[1] = (ten.AltBillAdd1 == null ? "" : ten.AltBillAdd1);
                        a[2] = (ten.AltBillAdd2 == null ? "" : ten.AltBillAdd2);
                        a[3] = (ten.AltBillAdd3 == null ? "" : ten.AltBillAdd3);
                    }
                    if (ten.Type == null || !ten.Type.ToLower().Trim().Equals("b"))
                    {
                        p = a[0].IndexOf(',');
                        if (p > 0)
                        {
                            lname = a[0].Substring(0, p).Trim();
                        }
                        else
                        {
                            lname = a[0].Trim();
                            if (lname.EndsWith("&"))
                            {
                                lname = lname.Substring(0, lname.Length - 1).Trim();
                            }
                            p = 1;
                            while (p > 0)
                            {
                                p = lname.IndexOf(" ");
                                if (p > 0)
                                {
                                    lname = lname.Substring(p + 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        lname = a[0];
                    }
                    a[0] = WinpmGlobal.reverse_names(a[0]);
                    if (ten.DueDate == null || ten.DueDate.Trim().Equals(""))
                    {
                        ten.DueDate = "01";
                    }
                    due = new DateTime(m_billingdate.Year, m_billingdate.Month, (int)(int.Parse(ten.DueDate) <= 0 ? 1 : int.Parse(ten.DueDate)));
                    if (ten.LeaseStartDate != null)
                    {
                        if (ten.LeaseStartDate.Year == m_billingdate.Year && ten.LeaseStartDate.Month == m_billingdate.Month)
                        {
                            due = new DateTime(m_billingdate.Year, m_billingdate.Month, ten.LeaseStartDate.Day);
                        }
                    }
                    cd = "      ";
                    LegalCode = " ";
                    if (Bal > 0)
                    {
                        cd = "*** ***";
                    }
                    if (ten.LegalCode != null && ten.LegalCode.Trim() != "")
                    {
                        cd = "***" + ten.LegalCode + "***";
                        LegalCode = ten.LegalCode;
                    }
                    // Do not print if nothing owed
                    if (total > 0)
                    {
                        stmts_print(ten, tblBuilding);
                        First = false;
                    }

                }
            }

              
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Completed Print Statements", false);

            //Printer.EndDoc();
        }

        public void stmts_print(Tenants ten, Building bld)
		{
			object B = null;
            double CurrentX;
            double CurrentY;

			int j = 0;
			int i = 0;
			int p = 0;
			float r = 0;
			string billcode = null;
			string x = null;
			string zip = null;

			print_template(ten.UseACH, NachaMsgArea, ten.PaymentDay);
			x = m_billingdate.Year.ToString("0000");
			p = int.Parse(x.Substring(0, 2));

			billcode = ten.BuildingNumber + ten.TenantNumber + (total * (decimal)100.0).ToString( "0000000") + cd + (m_billingdate.Month * 100 + p).ToString("0000");
			if (ten.Type != null && ten.Type.Trim() != "B" & printApt) {
				if (!ten.AlternateBillingAddressFlag) {
					a[1] = a[1].Trim() + " - " + ten.Apartment;
				} else {
					if (string.IsNullOrEmpty(a[3].Trim())) {
						a[1] = a[1].Trim() + " - " + ten.Apartment;
					} else {
						a[2] = a[2].Trim() + " - " + ten.Apartment;
					}
				}
			}

            Font fnt = new Font("Courier New", 13);
			// Top 1/3 of statement - Tenant Name/Address & Corporate Name/Address

            rpt.FontBold = true;
			CurrentY = TopNameY;
			CurrentX = TopNameX;
            rpt.PrintPageLine(a[0], CurrentX, CurrentY, fnt);
            CurrentX = TopNameX + 42;
			rpt.PrintPageLine(ra[0],CurrentX,CurrentY,fnt);
            if (ten.UseACH || ten.DirectPay)
            {
                Star = null;
                Star = Report.GetImage(StarPath);
                rpt.PrintImage(Star, CurrentX + 36, TopNameY);
            }


            CurrentX = TopNameX;
            CurrentY += 1;
			rpt.PrintPageLine(a[1],CurrentX, CurrentY, fnt);
			CurrentX = TopNameX + 42;
			rpt.PrintPageLine(ra[1],CurrentX, CurrentY, fnt);
            CurrentX = TopNameX;
            CurrentY += 1;
            rpt.PrintPageLine(a[2], CurrentX, CurrentY, fnt);
            CurrentX = TopNameX + 42;
			rpt.PrintPageLine(ra[2], CurrentX, CurrentY, fnt);
			CurrentX = TopNameX;
            CurrentY += 1;
			rpt.PrintPageLine(a[3], CurrentX, CurrentY, fnt);
			CurrentX = TopNameX + 42;
			rpt.PrintPageLine(ra[3], CurrentX, CurrentY, fnt);
			CurrentX = TopNameX + 42;
            CurrentY += 1;
			rpt.PrintPageLine(telno, CurrentX, CurrentY, fnt);
			CurrentY = TopNameY + 5;
			CurrentX = TopNameX + 5;
			rpt.PrintPageLine(bld.Address1.Trim() + " - " + ten.Apartment,CurrentX, CurrentY, fnt);
            CurrentX = TopNameX + 49;
            CurrentY = TopNameY + 5;
            rpt.PrintPageLine(due.ToString("MMMM d, yyyy"), CurrentX, CurrentY, fnt);

            // Detail
			j = 1;
			r = 0;
			CurrentX = ChgDesc1X;
			CurrentY = FirstY;
			rpt.PrintPageLine(rent_desc, CurrentX, CurrentY, fnt);
			CurrentX = Amt1X;
			rpt.PrintPageLine(rent.ToString( "$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
			if (Bal != 0) {
				CurrentX = ChgDesc2X;
				if (Bal > 0) {
				rpt.FontColor =  System.Windows.Media.Colors.Red;
				}
				rpt.PrintPageLine("PREVIOUS BALANCE  ", CurrentX, CurrentY, fnt);
				CurrentX = Amt2X;
				rpt.PrintPageLine(Bal.ToString( "$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
                rpt.FontColor = System.Windows.Media.Colors.Black;
                j = 0;
				r = r + 1;
			}
			for (i = 0; i <= maxdetail; i++) {
				if (camt[i] != 0) {
					if (j == 1) {
						CurrentX = ChgDesc2X;
						CurrentY = FirstY + r;
						rpt.PrintPageLine(cdesc[i], CurrentX, CurrentY, fnt);
						CurrentX = Amt2X;
						j = 0;
						r = r + 1;
					} else {
						CurrentX = ChgDesc1X;
						CurrentY = FirstY + r;
						rpt.PrintPageLine(cdesc[i], CurrentX, CurrentY, fnt);
						CurrentX = Amt1X;
						j = 1;
					}
					rpt.PrintPageLine(camt[i].ToString("$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
				}
			}
			if (Bal > 0) {
				CurrentX = 6;
				CurrentY = TotalY - 2;
				rpt.PrintPageLine("CHARGES SHOWN REFLECT PAYMENTS RECEIVED THRU " + m_closingdate.ToString("MM/dd/yyy"), CurrentX, CurrentY, fnt);
			}
			CurrentX = TotalX;
			CurrentY = TotalY;
			rpt.PrintPageLine(total.ToString("$########0.00").PadLeft(12), CurrentX, CurrentY, fnt);
            fnt = new Font("Arial", 13);
            rpt.FontBold = false;

            // Depracated?
			//lines[5].Substring(13) = a[0];
			//Strings.Mid(lines[7], 75) = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(billingdate), "##") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Year(billingdate), "##");
			//Strings.Mid(lines[7], 84) = telno;
			//Strings.Mid(lines[7], 102) = Convert.ToString(due);
			//Strings.Mid(lines[8], 13) = a[3];

			CurrentX = PayToX + 20;
            CurrentY = PayToY;
            rpt.FontBold = true;
            // NACHA Tenant?
            if (!ten.UseACH)
                rpt.PrintPageLine(paytoname, CurrentX, CurrentY, fnt);
            rpt.FontBold = false;

			// Messages
			msg_lines( 1,  MsgAreaY, ten);
			msg_lines( 2,  MsgAreaY, ten);
			msg_lines( 3,  MsgAreaY, ten);
            msg_lines( 4, MsgAreaY, ten);

            // Lower third of statement
            rpt.FontBold=true;
			rpt.PrintPageLine(ten.BuildingNumber + ten.TenantNumber, AcctX + 10, AcctY, fnt);
			CurrentX = DueDateX +4;
			CurrentY = DueDateY;
			rpt.PrintPageLine(due.ToString("MMMM d, yyyy"), CurrentX, CurrentY, fnt);
            rpt.FontBold = false;

			if (!string.IsNullOrEmpty(LegalCode.Trim())) {
                rpt.PrintLine(LegalCode1X + 6, LegalCodeY1 + 4 , LegalCode1X + 9, LegalCodeY1 + 6, System.Windows.Media.Brushes.Black ,true);
				// Legal Code Box
				CurrentX = LegalCode1X;
				CurrentY = LegalCodeY1;
				rpt.FontColor = System.Windows.Media.Colors.Red;
                rpt.FontBold = true;
				rpt.PrintPageLine(LegalCode, CurrentX, CurrentY, fnt);
                rpt.FontBold = false;
                rpt.FontColor = System.Windows.Media.Colors.Black;

            }
            fnt = new Font("Arial", 24);
            rpt.FontBold = true;
			CurrentX = LegalCode2X;
			CurrentY = LegalCodeY2;
            rpt.FontColor = System.Windows.Media.Colors.Red;
			rpt.PrintPageLine(cd, CurrentX, CurrentY, fnt);
            rpt.FontColor = System.Windows.Media.Colors.Black;
            fnt = new Font("Arial", 11);
            rpt.FontBold = false;
            fnt = new Font("Courier New", 13);
			rpt.FontBold = true;
			CurrentX = BottomNameX;
			CurrentY = BottomNameY;
			rpt.PrintPageLine(a[0], CurrentX, CurrentY, fnt);
            CurrentY++;
			rpt.PrintPageLine(a[1], CurrentX, CurrentY, fnt);
			CurrentY++;
			rpt.PrintPageLine(a[2], CurrentX, CurrentY, fnt);
			CurrentY++;
			rpt.PrintPageLine(a[3], CurrentX, CurrentY, fnt);

            fnt = new Font("Arial", 15);
			rpt.PrintPageLine(total.ToString("$######,##0.00").PadLeft(14), TotalDueX + 9, TotalDueY, fnt);
            // NACHA Tenant?
			if (!ten.UseACH)
                rpt.PrintPageLine(paytoname, CorpNameX + 35, CorpNameY, fnt);

            if (LockBoxScanLine)
            {
                // Scan Line
                string TenAccount = (ten.BuildingNumber.Trim().PadLeft(3, '0') + ten.TenantNumber.Trim().PadRight(4, '0')).PadLeft(13, '0');
                TenAccount += WinpmGlobal.LockBoxCheckDigit(TenAccount).ToString("0");

                string scan = TenAccount + (total * 100).ToString("0000000");
                fnt = new Font("OCR A Extended", 14);
                rpt.PrintPageLine(scan, ScanLineX, ScanLineY, fnt);
            }

            rpt.FontBold = false;
            fnt = new Font("Arial", 13);
            CurrentY = CorpNameY + 4;
            rpt.PrintPageLine("***" + ten.BuildingNumber + ten.TenantNumber + "***" + lname, CorpNameX, CurrentY, fnt);
			CurrentY++;
            rpt.PrintPageLine(ra[0], CorpNameX, CurrentY, fnt);
			// The following 2 lines should be commented for Fishman - depracated
			if (retAdd > 0) {
				CurrentY++;
                rpt.PrintPageLine(ra[1], CorpNameX, CurrentY, fnt);
			}
			CurrentY++;
            rpt.PrintPageLine(ra[2], CorpNameX, CurrentY, fnt);
			CurrentY++;
            rpt.PrintPageLine(ra[3], CorpNameX, CurrentY, fnt);
			CurrentY++;

            // TODO: Investigate BARCODE fonts
			zip = "";
            int xx;
            if (ra[3].Trim().Length > 0)
            {
                for (i = 1; i < ra[3].Length; i++)
                {
                    if (int.TryParse(ra[3].Substring(i, 1), out xx))
                    {
                        zip = zip + ra[3].Substring(i, 1);
                    }
                }
            }
            else
            {
                for (i = 1; i < ra[2].Length; i++)
                {
                    if (int.TryParse(ra[2].Substring(i, 1), out xx))
                    {
                        zip = zip + ra[2].Substring(i, 1);
                    }
                }
            }

            //rpt.PrintPageLine("/" + zip + "306/", CorpNameX, CurrentY, fnt);

            rpt.NewPage();
            //rpt.PrintDocument();
		}

        private void WinpmStmtsLaser_FormClosing(System.Object eventSender, System.Windows.Forms.FormClosingEventArgs eventArgs)
        {
        }

        public void print_template(bool useNacha = false, int nacha = 0, int PaymentDay = 1)
        {
            System.Windows.Media.Brush lineBrush = System.Windows.Media.Brushes.Black;
            System.Windows.Media.Brush fillBrush;
            Font fnt = new Font("Arial", 12);

            rpt.FontBold = true;

            // Entire Detail Box
            rpt.PrintLine(0, 6, 67, 20, lineBrush, true);
            fillBrush = System.Windows.Media.Brushes.LightGray;
            // 1st Charge description
            rpt.PrintLine(0, 6, 22, 8, lineBrush, true, fillBrush);
            // 1st Net
            rpt.PrintLine(22, 6, 34, 8, lineBrush, true, fillBrush);
            // 2nd Charge description
            rpt.PrintLine(34, 6, 56, 8, lineBrush, true, fillBrush);
            // 2nd Net
            rpt.PrintLine(56, 6, 67, 8, lineBrush, true, fillBrush);
            // Vertical lines for Details box
            rpt.PrintLine(21.5, 7, 21.5, 19.1);
            rpt.PrintLine(33.3, 7, 33.3, 19.1);
            rpt.PrintLine(54.9, 7, 54.9, 19.1);
            // Total Boxes
            rpt.PrintLine(33, 20, 56, 22, lineBrush, true, fillBrush);
            rpt.PrintLine(56, 20, 67, 22, lineBrush, true);

            fnt = new Font("Arial", 12);
            if (useNacha && nacha == NachaTemplateMsg)
            {
                if (PaymentDay < 1 || PaymentDay > DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month))
                    PaymentDay = 1;
                rpt.PrintPageLine(NachaMessage.Replace(NachaInsert, new DateTime(BillingDate.Year, BillingDate.Month, PaymentDay).ToShortDateString()), 5, 19.5, fnt);
            }
            else

                rpt.PrintPageLine("PLEASE MAKE PAYMENTS TO:", 5, 19.5, fnt);

            // Message area
            //rpt.PrintLine(0, 25.5, 71, 36.5, lineBrush, true);
            rpt.PrintLine(0, 25.5, 68, 36.5, lineBrush, true);

            rpt.FontSize = 18;
            rpt.PrintPageLine("DO NOT SEND CASH. YOUR CANCELLED CHECK IS YOUR RECEIPT", 7, 33, fnt);
            fnt = new Font("Wingdings", 16);
            rpt.PrintPageLine("\"", -8, 34, fnt);
            rpt.PrintPageLine("\"", 60, 34, fnt);

            fnt = new Font("Courier New", 11);
            rpt.PrintPageLine("•  —  •  —  •  —  • PLEASE TEAR AT PERFORATION AND RETURN STUB WITH PAYMENT  •  —  •  —  •  —  •", -5, 34, fnt);

            fnt = new Font("Arial", 12);
            rpt.PrintPageLine("DUE DATE:", TopNameX + 42, TopNameY + 5, fnt);
            rpt.PrintPageLine("ACCOUNT #:", AcctX, AcctY, fnt);
                                
            rpt.PrintPageLine("PLEASE INCLUDE ACCOUNT", 46, 40, fnt);
            rpt.PrintPageLine("NUMBER ON CHECK", 46, 41, fnt);

            // Place for NACHA Message
            if (useNacha && nacha == NachaTemplateMsg)
            {
                if (PaymentDay < 1 || PaymentDay > DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month))
                    PaymentDay = 1;
                rpt.PrintPageLine(NachaMessage.Replace(NachaInsert, new DateTime(BillingDate.Year, BillingDate.Month, PaymentDay).ToShortDateString()), CorpNameX + 15, CorpNameY, fnt);
            }
            else
                rpt.PrintPageLine("PLEASE MAKE PAYMENTS TO:", CorpNameX + 15, CorpNameY, fnt);

            // Bottom Total Box
            rpt.PrintLine(TotalDueX + 6, TotalDueY + 4, TotalDueX + 25, TotalDueY + 6, lineBrush, true);

            if (LockBoxScanLine)
            {
                // Scan Line Box
                rpt.PrintLine(ScanLineX + 6, ScanLineY + 4, ScanLineX + 4 + ScanLineSize, ScanLineY + 6, lineBrush, true);
            }

            rpt.PrintPageLine("RE:", -7, 0.5, fnt);
            rpt.PrintPageLine("DUE DATE:", DueDateX -5 , DueDateY, fnt);
            rpt.PrintPageLine("CHARGES OR CREDITS", -5, 2, fnt);
            rpt.PrintPageLine("AMOUNT", 19, 2, fnt);
            rpt.PrintPageLine("CHARGES OR CREDITS", 30, 2, fnt);
            rpt.PrintPageLine("AMOUNT", 52, 2, fnt);
            rpt.PrintPageLine("PAY THIS AMOUNT", 33, TotalY, fnt);
            rpt.PrintPageLine("TOTAL DUE", TotalDueX, TotalDueY, fnt);

            rpt.FontBold = false;

        }
    }
}
