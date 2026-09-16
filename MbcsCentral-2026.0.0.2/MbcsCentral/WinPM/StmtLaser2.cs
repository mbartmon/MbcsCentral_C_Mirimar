using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MbcsCentral;
using ExtensionMethods;

namespace MbcsCentral.WinPM
{
    class StmtLaser2 : ReportBase
    {
        private string m_startingBuilding;
        private string m_endingBuilding;
        private string m_startingTenant;
        private string m_endingTenant;
        private bool m_ACHBypass;
        private System.DateTime m_closingdate;
        private System.DateTime m_billingdate;
        private System.Printing.PrintQueue printer;
        Owners tblOwner;
        // Prior Month Payment Variables
        private DateTime PriorStartDate;
        private ArrayList ChargeCodes = new ArrayList();

        StreamWriter sw = null;


        // BillPay Billing File Variables
        string BillFileName = "";
        string[] BillFileHeaders = { "Tenant Code", "Account Number", "Tenant", "Address 1", "Address 2", "City", "State", "Zip", "Due Date", "Amount Due", "Description", "FDI Code" };
        String filepath = "";
        int BillDebitCount = 0;
        int BillCreditCount = 0;
        int BillZeroCount = 0;
        decimal BillDebitTotal = 0;
        decimal BillCreditTotal = 0;

        public bool BillPay { get; set; } = false;
        public bool PrintStmts { get; set; } = true;
        public string BillFileDir { get; set; } = "c:\\testing\\";
        public string BillFilePrefix { get; set; } = "Leemar";
        public string BillFileTag { get; set; } = "";
        public string BillPayDescription { get; set; } = "Rent Payment"; // May be filled in from Statements Messages

        #region NACHA FILE VARIABLES
        // NACHA File Variables
        const int BANK_INDICATOR = 1;
        enum Banks
        {
            Valley = 0,
            ConnectOne = 1
        }
        string NachaFileName = "";
        NACHA_Layout NachaControlBatchNumber;
        NachaBatchTenantHeader batchHeader;
        string NachaHeaderFedId = "";
        string NachaHeaderCompanyName = "";
        string NachaOffsettingTransactionCode = "";
        string NachaCheckingDebit = "";
        string NachaSavingdDebit = "";
        NACHA_Layout[] NachaDetailRecord = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.DetailRecord);
        string NachaDFI;
        int NachaSeq = 1;
        int LastPaymentDay = 0;
        int nachaBatchCount = 0;
        int nachaBatchCountTotal = 0;
        int nachaTotalBatches = 0;
        int nachaBatchHash = 0;
        long nachaFileHash = 0;
        double nachaRecordCount = 0;
        decimal[] nachaByDate = new decimal[31];
        DataTable nachaByBldDate = null;
        // Bstch Control
        decimal nachaTotalDebitAmount = 0;
        decimal nachaTotalCreditAmount = 0;
        //File Control
        decimal nachaFileDebitAmount = 0;
        decimal nachaFileCreditAmount = 0;
        ArrayList NachaProcessed = new ArrayList();
        List<string> NachaBad = new List<string>();


        NACHA_Layout[] NachaControl = null;

        public bool Nacha { get; set; } = false;
        public string NachaFileDir { get; set; } 
        public string NachaFilePrefix { get; set; }
        public string NachaFileOptional { get; set; }
        public char NachaFileTag { get; set; }
        #endregion
        public ArrayList NachaResults { get { return NachaProcessed; } }

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


        public bool LockBox { get; set; } = false;

        public bool AbovePerf { get; set; } = false;

        public bool PrintPriorPayments { get; set; } = false;

        public ArrayList OneByOne { get; set; } = null;

        public bool ACHonly { get;set; } = false;

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

        string fontname = "Courier New";
        float fontsize = 11;

        // Position constants
        const double DetailBoxX = 0;
        const double DetailBoxY = 3;
        const double TopNameX = -7;
        const double TopNameY = 1;
        const double ChgDesc1X = -6.5;
        const double ChgDesc2X = 28;
        const double FirstY = 5.5;
        const double Amt1X = 18.5;
        const double Amt2X = 51.5;
        const double TotalX = 50;
        const double TotalY = 18;
        const double PayToX = 5;
        const double PayToY = 19.5;
        const double MsgAreaX = 6;
        const double MsgAreaY = 21.5;
        const double AbovePerfX1 = 0;
        const double AbovePerfX2 = 40;
        const double AbovePerfY = 36;
        const double AcctX = -5;
        const double AcctY = 52;
        const double ScanLineX = 19.5;
        const double ScanLineY = 47.3;
        const double ScanLineSize = 25;
        const double DueDateX = 44;
        const double DueDateY = 45;
        const double BottomNameX = - 5 ;
        const double BottomNameY = 48;  
        const double LegalCodeY1 = 51;
        const double LegalCode1X = 20;
        const double LegalCodeY2 = 46;
        const double LegalCode2X = 48;
        const double TotalDueX = 42;
        const double TotalDueY = 55;
        const double CorpNameX = - 5;
        const double CorpNameY = 53;
        const double CodeX = 38;
        const double CodeY = 53;
        const int MAX_DESC_SIZE = 28;

        // $Id: StmtLaser.cs,v 1.7 2013/09/12 00:23:09 mike Exp $

        public StmtLaser2()
        {
            if (PrintStmts)
            {
                rpt = new Report(title, fontname, fontsize);
                //rpt.LeftMarginInches = (Single)0.25;
                rpt.TopMarginInches = (Single)0.25;
                rpt.BottomMargin = (Single)0.25;
            }
        }

        public StmtLaser2(string StartBuilding, string StartTenant, string EndBuilding, string EndTenant,
                         DateTime ClosingDate, DateTime BillingDate,bool LockBox, bool abovePerf, System.Printing.PrintQueue printer = null)
        {
            m_startingBuilding = StartBuilding;
            m_startingTenant = StartTenant;
            m_endingBuilding = EndBuilding;
            m_endingTenant = EndTenant;
            m_closingdate = ClosingDate;
            m_billingdate = BillingDate;
           
            this.LockBox = LockBox;
            this.AbovePerf = abovePerf;
            this.printer = printer;
        }

        public override void Process()
        {
            Print();
        }

// Tag:#MessageLines
        public void msg_lines(short t, double l, Tenants ten)
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
            }
            Font fnt = new Font("Arial", 12);

            if (t == 4 && PrintPriorPayments)
            {
                HistPay[] priorPayments = HistPay.loadByTenantDate(ten.SequenceNumber, PriorStartDate, m_closingdate);
                if (priorPayments  != null && priorPayments.Length > 0)
                {
                    string paymentsLine = "";
                    CurrentY = l;
                    foreach(HistPay pay in priorPayments)
                    {
                        //if (ChargeCodes.Contains(pay.Code))
                        //{
                            paymentsLine += (paymentsLine.Length > 0 ? " / " : "Prior Payments: ") + pay.PayDate.ToShortDateString() + " " + pay.Amount.ToString("$###,##0.00");
                        //}
                    }
                    if ( PrintStmts) rpt.PrintPageLine(paymentsLine, -5, CurrentY, fnt);
;                }
            }
            else
            {
                if (t == gen_loc)
                {
                    clr = new System.Drawing.Color();
                    clr = System.Drawing.ColorTranslator.FromWin32(generalMsgColor);
                    if ( PrintStmts) rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
;                    if ( PrintStmts) rpt.FontBold = true;
;                    CurrentY = l;
                    CurrentX = MsgAreaX;
                    if ( PrintStmts) rpt.PrintPageLine(msg_gen[1], CurrentX, CurrentY, fnt);
;                    CurrentX = MsgAreaX;
                    CurrentY++;
                    if ( PrintStmts) rpt.PrintPageLine(msg_gen[2], CurrentX, CurrentY, fnt);
;                    CurrentX = MsgAreaX;
                    CurrentY += 1;
                    if ( PrintStmts) rpt.PrintPageLine(msg_gen[3], CurrentX, CurrentY, fnt);
;                    if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;                    if ( PrintStmts) rpt.FontBold = false;
;                }

                if (t == legal_loc)
                {
                    if (ten.LegalCode != null && dictLegal.ContainsKey(ten.LegalCode.ToLower()))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(legalMsgColor);
                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
;                        if ( PrintStmts) rpt.FontBold = true;
;                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        if ( PrintStmts) rpt.PrintPageLine(dictLegal[ten.LegalCode.ToLower()], CurrentX, CurrentY, fnt);
;                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;                        if ( PrintStmts) rpt.FontBold = false;
;                    }
                }

                if (t == type_loc)
                {
                    if (ten.Type != null && dictType.ContainsKey(ten.Type.ToLower()))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(TypeMsgColor);
                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
;                        if ( PrintStmts) rpt.FontBold = true;
;                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        if ( PrintStmts) rpt.PrintPageLine(dictType[ten.Type.ToLower()], CurrentX, CurrentY, fnt);
;                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;                        if ( PrintStmts) rpt.FontBold = false;
;                    }
                }


                if (t == building_loc)
                {
                    if (ten.BuildingNumber != null && dictBuilding.ContainsKey(ten.BuildingNumber))
                    {
                        clr = new System.Drawing.Color();
                        clr = System.Drawing.ColorTranslator.FromWin32(BuildingMsgColor);
                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
;                        if ( PrintStmts) rpt.FontBold = true;
;                        CurrentY = l;
                        CurrentX = MsgAreaX;
                        if ( PrintStmts) rpt.PrintPageLine(dictBuilding[ten.BuildingNumber], CurrentX, CurrentY, fnt);
;                        if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;                        if ( PrintStmts) rpt.FontBold = false;
;                    }

                }
            }
        }

        public void Print()
		{
            if (PrintStmts && printer == null)
                return;



            if (PrintStmts)
            {
                this.printer.UserPrintTicket.Duplexing = System.Printing.Duplexing.OneSided;
                rpt = new Report(title, fontname, fontsize) { BottomMargin = 0, TopMargin = 0 };
            }
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
                    if (!lg.Code.Trim().Equals(""))
                    dictLegal.Add(lg.Code.ToLower().Trim(), lg.Message);
            TypeMsg[] types = TypeMsg.loadAll(clientId);
            if (types != null && types.Count() > 0)
                foreach (TypeMsg type in types)
                    dictType.Add(type.Type.ToLower().Trim(), type.Message);
            BuildingMsg[] buildingMgs = BuildingMsg.load();
            if (buildingMgs != null && buildingMgs.Length > 0)
                foreach (BuildingMsg msg in buildingMgs)
                    dictBuilding.Add(msg.BuildingNumber.Trim(), msg.Message);
            stmts_process();

            if ( PrintStmts) rpt.PrintDocument(printer);
;		}

        public void stmts_process()
        {
            Building tblBuilding = null;
            string xsql = null;
            string x = "";
            short i = 0;
            int p = 0;
            bool First = false;

            First = true;
            bldkey = "";
            string OwnerBankAccountNumber = "";
            string OwnerOwnerName = "";
            string OwnerBankRoutingNumber = "";

            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting " + (BillPay ? "To Generate Billing File" : " Print Statements"), false);
            Tenants[] tens = Tenants.loadForStatements(m_startingBuilding, m_endingBuilding, clientId, null, OneByOne, Nacha);
            if (tens == null || tens.Count() == 0)
            {
                return;
            }

            // Open BillPay File if needed
            if (BillPay)
            {
                if (!BillFileDir.EndsWith("\\"))
                    BillFileDir += "\\";

                filepath = BillFileDir + BillFilePrefix;
                filepath += "_" + BillingDate.ToString("yyyyMMdd_");
                if (!BillFileTag.Trim().Equals(""))
                    filepath +=  BillFileTag + "_";
                filepath += DateTime.Now.ToString("hhmm") + ".csv";
                try
                {
                    sw = new StreamWriter(filepath, false);

                }
                catch (IOException ex)
                {
                    sw = null;
                    if (MessageBoxCustom.Show("Unable to open BILLING file: " + ex.Message + "\r\n\nContinuing Printing Bills?", "", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
                        return;
                }
                bool first = true;
                foreach(string s in BillFileHeaders)
                {
                    sw.Write((first ? "" : ",") + s);
                    first = false;
                }
                sw.WriteLine();
            }

            #region  NACHA Open File if needed
            // NACHA Open File if needed
            if (Nacha)
            {
                nachaByBldDate = new DataTable();
                DataColumn dcBuilding = new DataColumn("Building");
                dcBuilding.DataType = System.Type.GetType("System.String");
                nachaByBldDate.Columns.Add(dcBuilding);

                DataColumn dcDay = new DataColumn("Day");
                dcDay.DataType = System.Type.GetType("System.Int32");
                nachaByBldDate.Columns.Add(dcDay);

                DataColumn dcAmount = new DataColumn("Amount");
                dcAmount.DataType = System.Type.GetType("System.Decimal");
                nachaByBldDate.Columns.Add(dcAmount);

                NachaControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.ControlData);
                if (NachaControl == null || NachaControl.Length == 0)
                {
                    MessageBoxCustom.Show("NACHA File Layout not found. Cannot continue.");
                    return;
                }
                foreach (NACHA_Layout rec in NachaControl)
                {
                    switch (rec.Position)
                    {
                        case (int)NACHA_Layout.Control_Types.BatchNumber:
                            NachaControlBatchNumber = rec;
                            break;
                        case (int)NACHA_Layout.Control_Types.FileDirectory:
                            NachaFileDir = rec.Contents;
                            break;
                        case (int)NACHA_Layout.Control_Types.FilePrefix:
                            NachaFilePrefix = rec.Contents;
                            break;
                        case (int)NACHA_Layout.Control_Types.FileID:
                            if (rec.Contents == null || rec.Contents.Trim().Equals(""))
                                rec.Contents = " ";
                            NachaFileTag = HelperMethods.IncrementAlphaNumeric(char.Parse(rec.Contents.Substring(0,1) ));
                            rec.Contents = NachaFileTag.ToString();
                            rec.save();
                            break;
                        case (int)NACHA_Layout.Control_Types.OffsettingTransactionCode:
                            NachaOffsettingTransactionCode = rec.Contents.Trim();
                            break;
                        case (int)NACHA_Layout.Control_Types.CheckingDebit:
                            NachaCheckingDebit = rec.Contents.Trim();
                            break;
                        case (int)NACHA_Layout.Control_Types.SavingsDebit:
                            NachaSavingdDebit = rec.Contents.Trim();
                            break;
                    }
                }
                // Write File Header
                if (!NachaFileDir.EndsWith("\\"))
                    NachaFileDir += "\\";

                filepath = NachaFileDir + NachaFilePrefix + "_" + NachaFileOptional;
                filepath += "_" + BillingDate.ToString("yyyyMMdd_");
                if (!NachaFileTag.Equals(' '))
                    filepath += NachaFileTag + "_";
                filepath += DateTime.Now.ToString("hhmm") + ".txt";
                try
                {
                    sw = new StreamWriter(filepath, false);

                }
                catch (IOException ex)
                {
                    sw = null;
                    if (MessageBoxCustom.Show("Unable to open NACHA file: " + ex.Message + "\r\n\nContinuing Printing Bills?", "", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
                        return;
                }
                #endregion

                #region NACHA File Header
                // NACHA File Header
                NACHA_Layout[] headers = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileHeader);
                NachaFileHeader header = new NachaFileHeader();
                foreach(NACHA_Layout rec in headers)
                {
                    switch (rec.Name.Trim())
                    {
                        case NachaFileHeader.COL_RECORD_ID:
                            header.RecordTypeCode = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_PRIORITY_CODE:
                            header.PriorityCode = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_IMMEDIATE_DESITNATION:
                            header.ImmediateDestination = rec.Contents.Trim().PadLeft(10);
                            NachaDFI = rec.Contents.Trim().Left(8);
                            break;
                        case NachaFileHeader.COL_FILE_ID:
                            header.FileId = NachaFileTag.ToString();
                            break;
                        case NachaFileHeader.COL_RECORD_SIZE:
                            header.RecordSize = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_BLOCKING_FACTOR:
                            header.BlockingFactor = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_FORMAT_CODE:
                            header.FormatCode = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_IMMEDIATE_DESTINATION_NAME:
                            header.ImmediateDestinationName = rec.Contents.Trim();
                            break;
                        case NachaFileHeader.COL_IMMEDIATE_ORIGIN:
                            header.ImmediateOrigin = rec.Contents.Trim().PadLeft(10);
                            break;
                        case NachaFileHeader.COL_IMMEDIATE_ORIGIN_NAME:
                            header.ImmediateOriginName = rec.Contents.Trim();
                            break;
                    }
                    header.FileCreationDate = BillingDate.ToString("yyMMdd");
                    header.FileCreationTime = DateTime.Now.TimeOfDay.ToString("hhmm");
                }
                header.RecordTypeCode = headers[0].Contents.Trim();
                nachaRecordCount++;

                sw.WriteLine(NACHA_Layout.record(header.DR));
                header = null;
                #endregion
            }

            NachaBatchControl nachaBatchControl = null;

            foreach (Tenants ten in tens)
            {
                // Do not process if NACHA run and Tenant has not enrolled
                if (Nacha && !ten.UseACH)
                    continue;

                // Do not process NACHA if Tenant Lease Expiration Date is prior to BillingDate
                if (Nacha && ten.LeaseExpirationDate != GlobalShared.NULL_DATE && ten.LeaseExpirationDate <= BillingDate)
                {
                    NachaBad.Add((ten.BuildingNumber + ten.TenantNumber).PadRight(8) + ": Prior Month Lease Expiration Date  <" + ten.LeaseExpirationDate.ToShortDateString() + ">");
                    continue;
                }

                // If Print only ACH then bypass if tenant not ACH
                if (ACHonly && !ten.UseACH)
                    continue;

               // Before start of range?
                if (String.Compare((ten.BuildingNumber + ten.TenantNumber.Trim()), (m_startingBuilding + m_startingTenant.Trim())) < 0)
                    continue;

               // End of range?
                if (String.Compare((ten.BuildingNumber + ten.TenantNumber.Trim()), (m_endingBuilding + m_endingTenant.Trim())) > 0)
                    break;

                // Vacant?
                if (ten.TenantName.StartsWith("*"))
                    continue;

                // Not yet moved-in?
                if (ten.MoveinDate > m_billingdate)
                    continue;

                if (!ten.BuildingNumber.Equals(bldkey)  ||  (Nacha && ten.PaymentDay != LastPaymentDay))
                {
                    bldkey = ten.BuildingNumber;
                    tblBuilding = Building.loadByBuildingNumber(ten.BuildingNumber, clientId);
                    if (tblBuilding == null || tblBuilding.Address1.Trim().Equals(""))
                    {
                        string msg = "Statements/Billing: Tenant bypassed. Building not found: " + ten.BuildingNumber + "-" + ten.TenantNumber;
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, msg, false);
                        continue;
                    }
                    if (tblBuilding != null && !tblBuilding.Address1.Trim().Equals(""))
                    {
                        tblOwner = Owners.loadByOwnerNumber(tblBuilding.OwnerNumber, clientId);
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

                        #region NACHA Company/Batch Header Record + Batch Control initializtion

                        // NACHA Company/Batch Header Record + Batch Control initialization
                        if (Nacha)
                        {
                            // Write Balancing Detail Record and Batch Control Record?
                            if (batchHeader != null )
                            {

                                // Balancing Detail Record
                                NachaDetail nachaDetail = new NachaDetail();
                                foreach (NACHA_Layout rec in NachaDetailRecord)
                                {
                                    switch (rec.Name.Trim())
                                    {
                                        case NachaDetail.COL_RECORD_CODE:
                                            nachaDetail.RecordTypeCode = rec.Contents.Trim();
                                            break;
                                        case NachaDetail.COL_ADDENDA_RECORD_INDICATOR:
                                            nachaDetail.AddendaRecordIndicator = rec.Contents.Trim();
                                            break;
                                    }
                                }
                                string dfi = OwnerBankRoutingNumber;
                                nachaDetail.TransactionCode = NachaOffsettingTransactionCode;
                                nachaDetail.ReceivingDfiIdentification = dfi.Substring(0, 8);
                                nachaDetail.CheckDigit = dfi.Substring(8, 1);
                                switch (BANK_INDICATOR)
                                {
                                    case (int)Banks.Valley:
                                        nachaDetail.DfiAccountNumber = OwnerBankAccountNumber;
                                        break;
                                    case (int)Banks.ConnectOne:
                                        nachaDetail.DfiAccountNumber =  OwnerBankAccountNumber.PadRight(10);

                                        break;
                                }
                                nachaDetail.Amount = (nachaTotalDebitAmount * 100).ToString("0000000000");
                                nachaDetail.IndividualName = OwnerOwnerName.Left(22);
                                nachaDetail.TraceNumber = NachaDFI + (NachaSeq++).ToString("0000000").PadLeft(7, '0');
                                sw.WriteLine(NACHA_Layout.record(nachaDetail.DR));
                                nachaTotalCreditAmount = nachaTotalDebitAmount;
                                nachaDetail = null;
                                nachaBatchCount++;
                                nachaBatchCountTotal++;
                                nachaRecordCount++;
                                int result = 0;
                                int.TryParse(dfi.Substring(0, 8), out result);
                                nachaBatchHash += result;
                                nachaFileHash += result;

                                
                                NACHA_Layout[] BatchControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchControl);
                                foreach(NACHA_Layout rec in BatchControl)
                                {
                                    switch (rec.Name.Trim())
                                    {
                                        case NachaBatchControl.COL_RECORD_CODE:
                                            nachaBatchControl.RecordTypeCode = rec.Contents.Trim();
                                            break;
                                        case NachaBatchControl.COL_SERVICE_CLASS_CODE:
                                            nachaBatchControl.ServiceClassCode = rec.Contents.Trim().Left(3);
                                            break;
                                        case NachaBatchControl.COL_ORIGINATING_DFI_IDENTIFICATION:
                                            nachaBatchControl.OriginatingDfiIdentification = rec.Contents.Trim();
                                            break;
                                    }
                                }
                                nachaBatchControl.EntryAddendaCount = nachaBatchCount.ToString("000000");
                                nachaBatchControl.EntryHash = nachaBatchHash.ToString().Left(10).PadLeft(10,'0');
                                nachaBatchControl.TotalDebitAmount = (nachaTotalDebitAmount * 100).ToString("000000000000");
                                nachaBatchControl.TotalCreditAmount = (nachaTotalCreditAmount * 100).ToString("000000000000");
                                nachaRecordCount++;
                                sw.WriteLine(NACHA_Layout.record(nachaBatchControl.DR));
                                batchHeader = null;
                                nachaBatchControl = null;

                                nachaFileDebitAmount += nachaTotalDebitAmount;
                                nachaFileCreditAmount += nachaTotalCreditAmount;
                                nachaBatchCount = 0;
                                nachaBatchHash = 0;
                                nachaTotalCreditAmount = 0;
                                nachaTotalDebitAmount = 0;
                            }
                            if (ten.PaymentDay == null || ten.PaymentDay == 0)
                                ten.PaymentDay = 1;
                            if (ten.PaymentDay > DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month))
                                ten.PaymentDay = DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month);

                            /*
                            NACHA_Layout[] BatchHeaders = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchHeader);
                            batchHeader = new NachaBatchHeader();
                            nachaBatchControl = new NachaBatchControl();
                            foreach(NACHA_Layout rec in BatchHeaders)
                            {
                                switch (rec.Name.Trim())
                                {
                                    case NachaBatchHeader.COL_RECORD_CODE:
                                        batchHeader.RecordTypeCode = rec.Contents.Trim();
                                        break;
                                    case NachaBatchHeader.COL_SERVICE_CLASS_CODE:
                                        batchHeader.ServiceClassCode = rec.Contents.Trim();
                                        break;
                                    case NachaBatchHeader.COL_COMPANY_NAME:
                                        batchHeader.CompanyName = tblOwner.OwnerName.Left(16,16);
                                        break;
                                    case NachaBatchHeader.COL_COMPANY_DISCRETIONARY_DATA:
                                        batchHeader.CompanyDiscretionaryData = rec.Contents.Left(20, 20);
                                        break;
                                    case NachaBatchHeader.COL_COMPANY_IDENTIFICATION:
                                        batchHeader.CompanyIdentification = "1" + tblOwner.FederalID.Replace("-", "").Trim().Left(9, 9);
                                        nachaBatchControl.ComapanyIdentification = "1" + tblOwner.FederalID.Replace("-","").Trim().Left(9, 9);
                                        break;
                                    case NachaBatchHeader.COL_STANDARD_ENTRY_CLASS_CODE:
                                        batchHeader.StandardEntryClassCode = rec.Contents.Trim().Left(3, 3);
                                        break;
                                    case NachaBatchHeader.COL_COMPANY_ENTRY_DESCRIPTION:
                                        batchHeader.CompanyEntryDescription = rec.Contents.Trim().Left(10, 10);
                                        break;
                                    case NachaBatchHeader.COL_COMPANY_DESCRIPTIVE_DATE:
                                        batchHeader.CompanyDescriptiveDate = BillingDate.ToString("MMddyy");
                                        break;
                                    case NachaBatchHeader.COL_EFFECTIVE_DATE:
                                        batchHeader.EffectiveDate = BillingDate.Year.ToString().Substring(0,2) + BillingDate.Month.ToString("00") + ten.PaymentDay.ToString("00");
                                        break;
                                    case NachaBatchHeader.COL_ORIGINATOR_STATUS_CODE:
                                        batchHeader.OriginatorStatusCode = rec.Contents.Trim();
                                        break;
                                    case NachaBatchHeader.COL_ORIGINATING_DFI_IDENTIICATION:
                                        batchHeader.OriginatingDfiIdentification = rec.Contents.Trim().Left(8, 8);
                                        break;
                                    case NachaBatchHeader.COL_BATCH_NUMBER:
                                        batchHeader.BatchNumber = (++NachaControlBatchNumber.Length).ToString("0000000").PadLeft(7);
                                        nachaBatchControl.BatchNumber = batchHeader.BatchNumber;
                                        NachaControlBatchNumber.save();
                                        break;
                                }
                            }
                            
                            nachaTotalBatches++;
                            nachaRecordCount++;
                            sw.WriteLine(NACHA_Layout.record(batchHeader.DR));
                            */
                            OwnerBankAccountNumber = tblOwner.BankAccountNumber;
                            OwnerOwnerName = tblOwner.OwnerName;
                            OwnerBankRoutingNumber = tblOwner.BankRoutingNumber;

                            #endregion

                            LastPaymentDay = ten.PaymentDay;
                        }
                    }
                    else
                    {
                        string msg = "Statements/Billing: Tenant bypassed. Building not found: " + ten.BuildingNumber + "-" + ten.TenantNumber;
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, msg, false);
                        continue;
                    }
                }
                rent_desc = "RENT              ";
                printApt = true;
                if (ten.Type != null && dictTenantType.ContainsKey(ten.Type.ToLower().Trim()))
                {
                    Types t = dictTenantType[ten.Type.ToLower().Trim()];
                    if (!t.PrintBill)
                        continue;
                    rent_desc = t.BillingName;
                    printApt = t.printApt;
                }
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
                System.Array.Clear(a, 0, a.Length);
                a[0] = ten.TenantName.Trim();
                a[1] = tblBuilding.Address1;
                a[2] = tblBuilding.Address2;
                a[3] = new String(' ', 30);
                if (ten.AlternateBillingAddressFlag && ten.AltBillName != null && !ten.AltBillName.Trim().Equals(""))
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
                            lname = lname.Substring(0, lname.Length-1).Trim();
                        }
                        p = 1;
                        while (p > 0)
                        {
                            p = lname.IndexOf(" ");
                            if (p > 0)
                            {
                                lname = lname.Substring( p + 1);
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
               due = new DateTime (m_billingdate.Year, m_billingdate.Month, (int)(int.Parse(ten.DueDate) <= 0 ? 1 : int.Parse(ten.DueDate)));
                if (ten.LeaseStartDate != null)
                {
                    if (ten.LeaseStartDate.Year == m_billingdate.Year  && ten.LeaseStartDate.Month == m_billingdate.Month)
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
                if (total > 0  || BillPay)
                {

                    if (Nacha && batchHeader == null)
                    {
                        NACHA_Layout[] BatchHeaders = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchHeader);
                        batchHeader = new NachaBatchTenantHeader();
                        nachaBatchControl = new NachaBatchControl();
                        foreach (NACHA_Layout rec in BatchHeaders)
                        {
                            switch (rec.Name.Trim())
                            {
                                case NachaBatchTenantHeader.COL_RECORD_CODE:
                                    batchHeader.RecordTypeCode = rec.Contents.Trim();
                                    break;
                                case NachaBatchTenantHeader.COL_SERVICE_CLASS_CODE:
                                    batchHeader.ServiceClassCode = rec.Contents.Trim();
                                    break;
                                case NachaBatchTenantHeader.COL_COMPANY_NAME:
                                    batchHeader.CompanyName = tblOwner.OwnerName.Left(16, 16);
                                    break;
                                case NachaBatchTenantHeader.COL_COMPANY_DISCRETIONARY_DATA:
                                    batchHeader.CompanyDiscretionaryData = rec.Contents.Left(20, 20);
                                    break;
                                case NachaBatchTenantHeader.COL_COMPANY_IDENTIFICATION:
                                    batchHeader.CompanyIdentification = "1" + tblOwner.FederalID.Replace("-", "").Trim().Left(9, 9);
                                    nachaBatchControl.CompanyIdentification = "1" + tblOwner.FederalID.Replace("-", "").Trim().Left(9, 9);
                                    break;
                                case NachaBatchTenantHeader.COL_STANDARD_ENTRY_CLASS_CODE:
                                    batchHeader.StandardEntryClassCode = rec.Contents.Trim().Left(3, 3);
                                    break;
                                case NachaBatchTenantHeader.COL_COMPANY_ENTRY_DESCRIPTION:
                                    batchHeader.CompanyEntryDescription = rec.Contents.Trim().Left(10, 10);
                                    break;
                                case NachaBatchTenantHeader.COL_COMPANY_DESCRIPTIVE_DATE:
                                    batchHeader.CompanyDescriptiveDate = BillingDate.ToString("MMddyy");
                                    break;
                                case NachaBatchTenantHeader.COL_EFFECTIVE_DATE:
                                    batchHeader.EffectiveDate = BillingDate.Year.ToString().Substring(2, 2) + BillingDate.Month.ToString("00") + ten.PaymentDay.ToString("00");
                                    break;
                                case NachaBatchTenantHeader.COL_ORIGINATOR_STATUS_CODE:
                                    batchHeader.OriginatorStatusCode = rec.Contents.Trim();
                                    break;
                                case NachaBatchTenantHeader.COL_ORIGINATING_DFI_IDENTIICATION:
                                    batchHeader.OriginatingDfiIdentification = rec.Contents.Trim().Left(8, 8);
                                    break;
                                case NachaBatchTenantHeader.COL_BATCH_NUMBER:
                                    batchHeader.BatchNumber = (++NachaControlBatchNumber.Length).ToString("0000000").PadLeft(7);
                                    nachaBatchControl.BatchNumber = batchHeader.BatchNumber;
                                    NachaControlBatchNumber.save();
                                    break;
                            }
                        }

                        nachaTotalBatches++;
                        nachaRecordCount++;
                        sw.WriteLine(NACHA_Layout.record(batchHeader.DR));
                    }

                    // If ACHBypass and Tenant is ACH then Bypass printing
                    if (m_ACHBypass && ten.UseACH)
                        continue;

                    stmts_print(ten, tblBuilding );
                    First = false;
                }
            }
            if (BillPay)
            {
                try
                {
                    sw.Close();
                }
                catch (IOException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error closing BillPay file during statement processing:\r\n   " + ex.Message, false);
                }

                Font mainFont = new Font(fontname, (float)(fontsize * (96.0 / 72.0)), FontStyle.Bold );
                Report rpt = new Report(title, fontname, fontsize) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
                rpt.SetFont = mainFont;
                rpt.PrintPageLine("One Bill Pay Posting (" + filepath + ") on " + DateTime.Now.ToShortDateString());
                rpt.PrintPageLine();
                rpt.PrintPageLine("Debit Balance Count:  " + BillDebitCount.ToString().PadLeft(10));
                rpt.PrintPageLine("Credit Balance Count: " + BillCreditCount.ToString().PadLeft(10));
                rpt.PrintPageLine("Zero Balance Count:   " + BillZeroCount.ToString().PadLeft(10));
                rpt.PrintPageLine("                      ==========");
                rpt.PrintPageLine("Total Count           " + (BillDebitCount + BillCreditCount + BillZeroCount).ToString().PadLeft(10));
                rpt.PrintPageLine();
                rpt.PrintPageLine("Debit Balances:  " + BillDebitTotal.ToString("$##,###,##0.00").PadLeft(14)); 
                rpt.PrintPageLine("Credit Balances: " + BillCreditTotal.ToString("$##,###,##0.00").PadLeft(14));
                rpt.PrintPageLine("                 ==============");
                rpt.PrintPageLine("Total Balances:  " + (BillDebitTotal + BillCreditTotal).ToString("$##,###,##0.00").PadLeft(14));
                rpt.DisplayDocument();
                rpt = null;



            }

            if (Nacha)
            {
                if (batchHeader != null)
                {
                    // Balancing Detail Record
                    NachaDetail nachaDetail = new NachaDetail();
                    foreach (NACHA_Layout rec in NachaDetailRecord)
                    {
                        switch (rec.Name.Trim())
                        {
                            case NachaDetail.COL_RECORD_CODE:
                                nachaDetail.RecordTypeCode = rec.Contents.Trim();
                                break;
                            case NachaDetail.COL_TRANSACTION_CODE:
                                nachaDetail.TransactionCode = rec.Contents.Trim();
                                break;
                            case NachaDetail.COL_ADDENDA_RECORD_INDICATOR:
                                nachaDetail.AddendaRecordIndicator = rec.Contents.Trim();
                                break;
                        }
                    }
                    nachaDetail.TransactionCode = NachaOffsettingTransactionCode;
                    string dfi = tblOwner.BankRoutingNumber;
                    nachaDetail.ReceivingDfiIdentification = dfi.Substring(0, 8);
                    nachaDetail.CheckDigit = dfi.Substring(8, 1);
                    nachaDetail.DfiAccountNumber = tblOwner.BankAccountNumber;
                    nachaDetail.Amount = (nachaTotalDebitAmount * 100).ToString("0000000000");
                    nachaDetail.IndividualName = tblOwner.OwnerName.Left(22);
                    nachaDetail.TraceNumber = NachaDFI + (NachaSeq++).ToString("0000000").PadLeft(7,'0');
                    sw.WriteLine(NACHA_Layout.record(nachaDetail.DR));
                    nachaTotalCreditAmount = nachaTotalDebitAmount;
                    nachaFileDebitAmount += nachaTotalDebitAmount;
                    nachaFileCreditAmount += nachaTotalCreditAmount;
                    nachaBatchCount++;
                    nachaBatchCountTotal++;
                    int result = 0;
                    int.TryParse(dfi.Substring(0, 8), out result);
                    nachaBatchHash += result;
                    nachaFileHash += result;
                    nachaDetail = null;

                    NACHA_Layout[] BatchControl = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.BatchControl);
                    foreach (NACHA_Layout rec in BatchControl)
                    {
                        switch (rec.Name.Trim())
                        {
                            case NachaBatchControl.COL_RECORD_CODE:
                                nachaBatchControl.RecordTypeCode = rec.Contents.Trim();
                                break;
                            case NachaBatchControl.COL_SERVICE_CLASS_CODE:
                                nachaBatchControl.ServiceClassCode = rec.Contents.Trim().Left(3);
                                break;
                            case NachaBatchControl.COL_COMPANY_IDENTIFICATION:
                                nachaBatchControl.CompanyIdentification = rec.Contents.Trim();
                                break;
                            case NachaBatchControl.COL_ORIGINATING_DFI_IDENTIFICATION:
                                nachaBatchControl.OriginatingDfiIdentification = rec.Contents.Trim();
                                break;
                        }
                    }
                    nachaBatchControl.EntryAddendaCount = nachaBatchCount.ToString("000000");
                    nachaBatchControl.EntryHash = nachaBatchHash.ToString().Left(10).PadLeft(10, '0');
                    nachaBatchControl.TotalDebitAmount = (nachaTotalDebitAmount * 100).ToString("000000000000");
                    nachaBatchControl.TotalCreditAmount = (nachaTotalCreditAmount * 100).ToString("000000000000");
                    nachaRecordCount++;
                    sw.WriteLine(NACHA_Layout.record(nachaBatchControl.DR));
                    nachaBatchControl = null;

                }
                // Nacha File Control
                NACHA_Layout[] FileControls = NACHA_Layout.loadByType((int)NACHA_Layout.Record_Types.FileControl);
                NachaFileControl fileControl = new NachaFileControl();
                foreach (NACHA_Layout rec in FileControls)
                {
                    switch (rec.Name.Trim())
                    {
                        case NachaFileControl.COL_RECORD_CODE:
                            fileControl.RecordTypeCode = rec.Contents.Trim();
                            break;

                    }
                }
                nachaRecordCount++;
                fileControl.BatchCount = nachaTotalBatches.ToString("000000");
                fileControl.BlockCount = Math.Ceiling(nachaRecordCount / 10).ToString("000000");
                fileControl.EntryAddendaCount = nachaBatchCountTotal.ToString("00000000");
                fileControl.EntryHash = nachaFileHash.ToString().Left(10).PadLeft(10,'0');
                fileControl.TotalDebitAmount = (nachaFileDebitAmount * 100).ToString("000000000000");
                fileControl.TotalCreditAmount = (nachaFileCreditAmount * 100).ToString("000000000000");
                sw.WriteLine(NACHA_Layout.record(fileControl.DR));
                fileControl = null;
                sw.Close();
                //Printer.EndDoc();
                Font mainFont = new Font(fontname, (float)(fontsize * (96.0 / 72.0)), FontStyle.Bold);
                Report rpt = new Report(title, fontname, fontsize) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
                rpt.SetFont = mainFont;
                string CurrentBuilding = "";
                DateTime CurrentDate = GlobalShared.NULL_DATE;
                decimal CurrentPaid = 0;
                decimal BuildingPaid = 0;
                decimal TotalPaid = 0;
                int TotalTenants = 0;
                rpt.PrintPageLine("ACH File Generated:");
                rpt.PrintPageLine( filepath);
                rpt.PrintPageLine("on " + DateTime.Now.ToShortDateString());
                rpt.PrintPageLine();
                rpt.PrintPageLine();
                rpt.PrintPageLine();
                foreach (Tenants ten in NachaProcessed)
                {
                    if (CurrentBuilding != "" && ten.BuildingNumber != CurrentBuilding )
                    {
                        rpt.PrintPageLine(new String(' ',60) + "===============");
                        rpt.PrintPageLine(new String(' ',37) + "Total for Building " + CurrentBuilding + ": " + BuildingPaid.ToString("$#,###,##0.00").PadLeft(14));
                        rpt.PrintPageLine();
                        rpt.PrintPageLine();
                        if (rpt.PrintPageLine())
                            rpt.NewPage();
                        CurrentBuilding = ten.BuildingNumber;
                        BuildingPaid = 0;
                    }
                    if (CurrentDate > GlobalShared.NULL_DATE && ten.DatePaid != CurrentDate)
                    {
                        //rpt.PrintPageLine(new String(' ', 60) + "===============");
                        //rpt.PrintPageLine(new String(' ', 39) + "Total for " + CurrentDate.ToShortDateString() + ": " + CurrentPaid.ToString("$#,###,##0.00").PadLeft(14));
                        //if (rpt.PrintPageLine())
                        //    rpt.NewPage();
                        CurrentDate = ten.DatePaid;
                        CurrentPaid = 0;
                    }
                    if (rpt.PrintPageLine(ten.BuildingTenant.PadRight(7) + "  " + ten.TenantName.PadRight(32) + "  " + ten.Apartment.PadRight(4) + "  " + ten.DatePaid.ToShortDateString() + "  " + ten.Payment.ToString("$#,###,##0.00").PadLeft(14)))
                    {
                        rpt.NewPage();
                    }
                    CurrentBuilding = ten.BuildingNumber;
                    CurrentDate = ten.DatePaid;
                    CurrentPaid += ten.Payment;
                    BuildingPaid += ten.Payment;
                    TotalPaid += ten.Payment;
                    TotalTenants++;
                }
                rpt.PrintPageLine(new String(' ',60) + "===============");
                rpt.PrintPageLine(new String(' ',37) + "Total for Building " + CurrentBuilding + ": " + BuildingPaid.ToString("$#,###,##0.00").PadLeft(14));
                rpt.PrintPageLine();
                //rpt.PrintPageLine(new String(' ', 60) + "===============");
                //rpt.PrintPageLine(new String(' ', 39) + "Total for " + CurrentDate.ToShortDateString() + ": " + CurrentPaid.ToString("$#,###,##0.00").PadLeft(14));
                //rpt.PrintPageLine();
                rpt.PrintPageLine();
                if (rpt.PrintPageLine())
                    rpt.NewPage();
                rpt.PrintPageLine("Total Tenants Posted: " + TotalTenants.ToString().PadLeft(13));
                rpt.PrintPageLine("Total Records Posted: " + nachaBatchCountTotal.ToString().PadLeft(13));
                rpt.PrintPageLine("Number of Batches:   " + nachaTotalBatches.ToString().PadLeft(14));
                rpt.PrintPageLine("Total Amount Posted: " + TotalPaid.ToString("$##,###,##0.00").PadLeft(14));
                rpt.PrintPageLine();
                if (rpt.PrintPageLine())
                    rpt.NewPage();

                rpt.PrintPageLine("Totals By Payment Date");
                rpt.PrintPageLine("======================");
                for (int c = 1; c < nachaByDate.Length; c++)
                {
                    if (nachaByDate[c] != 0)
                    {
                        if (rpt.PrintPageLine(BillingDate.Month.ToString("00") + "/" + c.ToString("00") + "/" + BillingDate.Year.ToString("0000") + " Total: " + nachaByDate[c].ToString("$##,###,##0.00").PadLeft(17)))
                            rpt.NewPage();
                    }
                }
                rpt.PrintPageLine();
                if (rpt.PrintPageLine())
                    rpt.NewPage();
                DataView dv = nachaByBldDate.DefaultView;
                
                dv.Sort = "Building, Day";
                string lastBld;
                try
                {
                    lastBld = (string)dv[0]["Building"];
                }
                catch
                {
                    lastBld = "";
                }
                rpt.PrintPageLine("Totals by Building/Payment Date");
                if (rpt.PrintPageLine("=============================="))
                    rpt.NewPage();
                foreach(DataRowView drv in dv)
                {
                    if (!drv["Building"].Equals(lastBld))
                    {
                        rpt.PrintPageLine();
                        lastBld = (string) drv["Building"];
                    }
                    if (rpt.PrintPageLine("Bld: " + drv["Building"].ToString() + " " + BillingDate.Month.ToString("00") + "/" + ((int)drv["Day"]).ToString("00") + "/" + BillingDate.Year.ToString("0000") + ((decimal)drv["amount"]).ToString("$#,###,##0.00").PadLeft(14)))
                        rpt.NewPage();
                }
                if (NachaBad.Count > 0)
                {
                    rpt.PrintPageLine();
                    rpt.PrintPageLine();
                    rpt.PrintPageLine("Tenants NOT PROCESSED");
                    rpt.PrintPageLine("---------------------");
                    foreach (string c in NachaBad)
                        rpt.PrintPageLine("   " + c);
                }
                rpt.DisplayDocument();
                rpt = null;
            }
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Completed Print Statements", false);
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
            bool rowFound = false;

			print_template();
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

            if ( PrintStmts) rpt.FontBold = true;
;			CurrentY = TopNameY;
			CurrentX = TopNameX + 3;
			if ( PrintStmts) rpt.PrintPageLine(a[0], CurrentX, CurrentY + 1, fnt);
;			CurrentX = TopNameX + 42;
			if ( PrintStmts) rpt.PrintPageLine(ra[0],CurrentX,CurrentY,fnt);
;

            CurrentX = TopNameX + 49;
            CurrentY = TopNameY + 1;
            if ( PrintStmts) rpt.PrintPageLine(due.ToString("MMMM d, yyyy"), CurrentX, CurrentY, fnt);
;
            // Detail
			j = 1;
			r = 0;
			CurrentX = ChgDesc1X;
			CurrentY = FirstY;
			if ( PrintStmts) rpt.PrintPageLine(rent_desc, CurrentX, CurrentY, fnt);
;			CurrentX = Amt1X;
			if ( PrintStmts) rpt.PrintPageLine(rent.ToString( "$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
;			if (Bal != 0) {
				CurrentX = ChgDesc2X;
				if (Bal > 0) {
				if ( PrintStmts) rpt.FontColor =  System.Windows.Media.Colors.Red;
;				}
				if ( PrintStmts) rpt.PrintPageLine("PREVIOUS BALANCE  ", CurrentX, CurrentY, fnt);
;				CurrentX = Amt2X;
				if ( PrintStmts) rpt.PrintPageLine(Bal.ToString( "$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
;                if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;                j = 0;
				r = r + 1;
			}
			for (i = 0; i <= maxdetail; i++) {
				if (camt[i] != 0) {
					if (j == 1) {
						CurrentX = ChgDesc2X;
						CurrentY = FirstY + r;
						if ( PrintStmts) rpt.PrintPageLine(cdesc[i], CurrentX, CurrentY, fnt);
;						CurrentX = Amt2X;
						j = 0;
						r = r + 1;
					} else {
						CurrentX = ChgDesc1X;
						CurrentY = FirstY + r;
						if ( PrintStmts) rpt.PrintPageLine(cdesc[i], CurrentX, CurrentY, fnt);
;						CurrentX = Amt1X;
						j = 1;
					}
					if ( PrintStmts) rpt.PrintPageLine(camt[i].ToString("$######0.00").PadLeft(10), CurrentX, CurrentY, fnt);
;				}
			}
			if (Bal > 0) {
				CurrentX = 6;
				CurrentY = TotalY - 2;
				if ( PrintStmts) rpt.PrintPageLine("CHARGES SHOWN REFLECT PAYMENTS RECEIVED THRU " + m_closingdate.ToString("MM/dd/yyy"), CurrentX, CurrentY, fnt);
;			}
			CurrentX = TotalX;
			CurrentY = TotalY;
			if ( PrintStmts) rpt.PrintPageLine(total.ToString("$########0.00").PadLeft(12), CurrentX, CurrentY, fnt);
;            fnt = new Font("Arial", 13);
            if ( PrintStmts) rpt.FontBold = false;
;
			CurrentX = PayToX + 20;
            CurrentY = PayToY;
            if ( PrintStmts) rpt.FontBold = true;
;            if ( PrintStmts) rpt.PrintPageLine(paytoname, CurrentX, CurrentY, fnt);
;            if ( PrintStmts) rpt.FontBold = false;
;
			// Messages
			msg_lines( 1,  MsgAreaY, ten);
			msg_lines( 2,  MsgAreaY, ten);
			msg_lines( 3,  MsgAreaY, ten);
            msg_lines(4, MsgAreaY, ten);

            if (AbovePerf)
            {
                CurrentX = AbovePerfX1;
                CurrentY = AbovePerfY;
                if ( PrintStmts) rpt.PrintPageLine(a[0], CurrentX, CurrentY, fnt);
;                CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(a[1], CurrentX, CurrentY, fnt);
;                CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(a[2], CurrentX, CurrentY, fnt);
;                CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(a[3], CurrentX, CurrentY, fnt);
;
                CurrentX = AbovePerfX2;
                CurrentY = AbovePerfY;
                if ( PrintStmts) rpt.PrintPageLine(ra[0], CurrentX, CurrentY, fnt);
;                if (retAdd > 0)
                {
                    CurrentY++;
                    if ( PrintStmts) rpt.PrintPageLine(ra[1], CurrentX, CurrentY, fnt);
;                }
                CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(ra[2], CurrentX, CurrentY, fnt);
;                CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(ra[3], CurrentX, CurrentY, fnt);
;

            }

            // Lower third of statement
            if ( PrintStmts) rpt.FontBold=true;
;			if ( PrintStmts) rpt.PrintPageLine(ten.BuildingNumber + ten.TenantNumber, AcctX + 10, AcctY, fnt);
;			CurrentX = DueDateX +4;
			CurrentY = DueDateY;
			if ( PrintStmts) rpt.PrintPageLine(due.ToString("MMMM d, yyyy"), CurrentX, CurrentY, fnt);
;            if ( PrintStmts) rpt.FontBold = false;
;
			if (!string.IsNullOrEmpty(LegalCode.Trim())) {
                if ( PrintStmts) rpt.PrintLine(LegalCode1X + 6, LegalCodeY1 + 5 , LegalCode1X + 9, LegalCodeY1 + 7, System.Windows.Media.Brushes.Black ,true);
;				// Legal Code Box
				CurrentX = LegalCode1X;
				CurrentY = LegalCodeY1 + 6;
				if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Red;
;                if ( PrintStmts) rpt.FontBold = true;
;				if ( PrintStmts) rpt.PrintPageLine(LegalCode, CurrentX, CurrentY, fnt);
;                if ( PrintStmts) rpt.FontBold = false;
;                if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;
            }
            fnt = new Font("Arial", 24);
            if ( PrintStmts) rpt.FontBold = true;
;			CurrentX = LegalCode2X;
			CurrentY = LegalCodeY2;
            if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Red;
;			if ( PrintStmts) rpt.PrintPageLine(cd, CurrentX, CurrentY, fnt);
;            if ( PrintStmts) rpt.FontColor = System.Windows.Media.Colors.Black;
;            fnt = new Font("Arial", 11);
            if ( PrintStmts) rpt.FontBold = false;
;            fnt = new Font("Courier New", 13);
			if ( PrintStmts) rpt.FontBold = true;
;			CurrentX = BottomNameX;
			CurrentY = BottomNameY;
			if ( PrintStmts) rpt.PrintPageLine(a[0], CurrentX, CurrentY, fnt);
;            CurrentY++;
			if ( PrintStmts) rpt.PrintPageLine(a[1], CurrentX, CurrentY, fnt);
;			CurrentY++;
			if ( PrintStmts) rpt.PrintPageLine(a[2], CurrentX, CurrentY, fnt);
;			CurrentY++;
			if ( PrintStmts) rpt.PrintPageLine(a[3], CurrentX, CurrentY, fnt);
;
            fnt = new Font("Arial", 15);
			if ( PrintStmts) rpt.PrintPageLine(total.ToString("$######,##0.00").PadLeft(14), TotalDueX + 9, TotalDueY + .5, fnt);
;			if ( PrintStmts) rpt.PrintPageLine(paytoname, CorpNameX + 38, CorpNameY + .5, fnt);
;
            // Scan Line
            if (LockBox)
            {
                string TenAccount = (ten.BuildingNumber.Trim().PadLeft(3, '0') + ten.TenantNumber.Trim().PadRight(4, '0'));
                TenAccount += WinpmGlobal.LockBoxCheckDigit(TenAccount).ToString("0");

                string scan = (TenAccount + BillingDate.ToString("0MMddyy") + (total * 100).ToString("0000000")).PadLeft(28, '0');
                fnt = new Font("OCR A Extended", 14);
                if ( PrintStmts) rpt.PrintPageLine(scan, ScanLineX - 1, ScanLineY + .5, fnt);
;            }

            fnt = new Font("Arial", 13);
            CurrentY = CorpNameY + 4;
            if ( PrintStmts) rpt.PrintPageLine("***" + ten.BuildingNumber + ten.TenantNumber + "***" + lname, CorpNameX, CurrentY, fnt);
;            CurrentY++;
            if ( PrintStmts) rpt.PrintPageLine(ra[0], CorpNameX, CurrentY, fnt);
;            // The following 2 lines should be commented for Fishman - depracated
            if (retAdd > 0) {
				CurrentY++;
                if ( PrintStmts) rpt.PrintPageLine(ra[1], CorpNameX, CurrentY, fnt);
;			}
			CurrentY++;
            if ( PrintStmts) rpt.PrintPageLine(ra[2], CorpNameX, CurrentY, fnt);
;			CurrentY++;
            if ( PrintStmts) rpt.PrintPageLine(ra[3], CorpNameX, CurrentY, fnt);
;			CurrentY++;
;
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

;
            // BillPay 
            if (BillPay && bld != null && !bld.Address1.Trim().Equals("") && !bld.Address2.Trim().Equals(""))
            {
                string[] s = WinpmGlobal.ParseBuildingAddress(bld.Address2);
                string line = ten.SequenceNumber.ToString() + "," + ten.BuildingNumber.Trim() + ten.TenantNumber.Trim() + "," +
                              a[0].Trim().Replace(',', ' ') + ",\"" + bld.Address1 + "\",APT " + ten.Apartment + "," + s[0] + "," + s[1] + "," + s[2] + "," + BillingDate.ToShortDateString() +
                              "," + total.ToString("$########0.00") + ",\"" + BillPayDescription.Trim() + "\"," + bld.BuildingNumber.Trim();
                sw.WriteLine(line);
                
                switch (total)
                {
                    case 0:
                        BillZeroCount++;
                        break;
                    default:
                        if (total < 0)
                        {
                            BillCreditCount++;
                            BillCreditTotal += total;
                        }
                        else
                        {
                            BillDebitCount++;
                            BillDebitTotal += total;
                        }
                        break;
                }
            }

            #region NACHA DETAIL RECORD
            if (Nacha)
            {
                string dfi = ten.Bank;
                string acct = ten.InterestInfo;
                if (dfi == null || dfi.Length != 9 || acct == null || acct.Trim().Equals(""))
                {
                    NachaBad.Add((ten.BuildingNumber + ten.TenantNumber).PadRight(8) + ": Invalid bank account information <Routing: " + dfi + "> <Acct: " + acct + ">");
                    return;
                }

                NachaDetail nachaDetail = new NachaDetail();
                foreach (NACHA_Layout rec in NachaDetailRecord)
                {
                    switch (rec.Name.Trim())
                    {
                        case NachaDetail.COL_RECORD_CODE:
                            nachaDetail.RecordTypeCode = rec.Contents.Trim();
                            break;
                        case NachaDetail.COL_ADDENDA_RECORD_INDICATOR:
                            nachaDetail.AddendaRecordIndicator = rec.Contents.Trim();
                            break;
                    }
                }
                if (ten.SavingsAccount)
                    nachaDetail.TransactionCode = NachaSavingdDebit;
                else
                    nachaDetail.TransactionCode = NachaCheckingDebit;
                nachaDetail.ReceivingDfiIdentification = dfi.Substring(0, 8);
                nachaDetail.CheckDigit = dfi.Substring(8, 1);
                nachaDetail.DfiAccountNumber = acct;
                nachaDetail.Amount = (total * 100).ToString("0000000000");
                nachaDetail.IndividualIdentificationNumber = ten.SequenceNumber.ToString();
                nachaDetail.IndividualName = ten.TenantName.Left(22,22);
                nachaDetail.TraceNumber = NachaDFI + (NachaSeq++).ToString("0000000").PadLeft(7, '0');
                sw.WriteLine(NACHA_Layout.record(nachaDetail.DR));
                nachaDetail = null;
                if (ten.PaymentDay < 1 || ten.PaymentDay > DateTime.DaysInMonth(BillingDate.Year, BillingDate.Month))
                    ten.PaymentDay = 1;
                ten.DatePaid = new DateTime(BillingDate.Year, BillingDate.Month, ten.PaymentDay);
                ten.Payment = total;
                NachaProcessed.Add(ten);

                int result = 0;
                int.TryParse(dfi.Substring(0 ,8), out result);
                nachaBatchHash += result;
                nachaFileHash += result;
                nachaRecordCount++;
                nachaBatchCount++;
                nachaBatchCountTotal++;
                if (total < 0)
                {
                    nachaTotalCreditAmount -= total;
                }
                else
                {
                    nachaTotalDebitAmount += total;
                }
                nachaByDate[ten.PaymentDay] += total;
                rowFound = false;
                DataRow dr;
                if (nachaByBldDate.Rows.Count > 0)
                {
                    DataRow[] rows = nachaByBldDate.Select("Building = '" + ten.BuildingNumber + "' AND Day = " + ten.PaymentDay.ToString());
                    if (rows.Length > 0)
                    {
                        dr = rows[0];
                        dr["Amount"] = (decimal)dr["Amount"] + total;
                        rowFound = true;
                    }
                }
                if (!rowFound)
                {
                    dr = nachaByBldDate.NewRow();
                    dr["Building"] = ten.BuildingNumber;
                    dr["Day"] = ten.PaymentDay;
                    dr["Amount"] = total;
                    nachaByBldDate.Rows.Add(dr);
                }
            }
            #endregion
            if (PrintStmts) rpt.NewPage();
        }

        private void WinpmStmtsLaser_FormClosing(System.Object eventSender, System.Windows.Forms.FormClosingEventArgs eventArgs)
        {
        }

        public void print_template()
        {
            System.Windows.Media.Brush lineBrush = System.Windows.Media.Brushes.Black;
            System.Windows.Media.Brush fillBrush;
            Font fnt = new Font("Arial", 12);

            if ( PrintStmts) rpt.FontBold = true;
;
            // Entire Detail Box
            if ( PrintStmts) rpt.PrintLine(0, 3, 67, 17, lineBrush, true);
;            fillBrush = System.Windows.Media.Brushes.Cyan;
            // 1st Charge description
            if ( PrintStmts) rpt.PrintLine(0, 3, 22, 5, lineBrush, true, fillBrush);
;            // 1st Amount
            if ( PrintStmts) rpt.PrintLine(22, 3, 34, 5, lineBrush, true, fillBrush);
;            // 2nd Charge description
            if ( PrintStmts) rpt.PrintLine(34, 3, 56, 5, lineBrush, true, fillBrush);
;            // 2nd Amount
            if ( PrintStmts) rpt.PrintLine(56, 3, 67, 5, lineBrush, true, fillBrush);
;            // Vertical lines for Details box
            if ( PrintStmts) rpt.PrintLine(21.5, 4, 21.5, 16.1);
;            if ( PrintStmts) rpt.PrintLine(33.3, 4, 33.3, 16.1);
;            if ( PrintStmts) rpt.PrintLine(54.9, 4, 54.9, 16.1);
;            // Total Boxes
            if ( PrintStmts) rpt.PrintLine(33, 17, 56, 19, lineBrush, true, fillBrush);
;            if ( PrintStmts) rpt.PrintLine(56, 17, 67, 19, lineBrush, true);
;
            fnt = new Font("Arial", 12);
            if ( PrintStmts) rpt.PrintPageLine("PLEASE MAKE PAYMENTS TO:", 5, 19.5, fnt);
;
            /*
            // Entire Detail Box
            if ( PrintStmts) rpt.PrintLine(DetailBoxX, DetailBoxY, DetailBoxX + 62, DetailBoxY + 14, lineBrush, true);
;            fillBrush = System.Windows.Media.Brushes.Cyan;
            // 1st Charge description
            if ( PrintStmts) rpt.PrintLine(DetailBoxX, DetailBoxY, DetailBoxX + 22, DetailBoxY + 2, lineBrush, true, fillBrush);
;            // 1st Amount
            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 22, DetailBoxY, DetailBoxX + 37, DetailBoxY + 2, lineBrush, true, fillBrush);
;            // 2nd Charge description
            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 37, DetailBoxY, DetailBoxX + 65, DetailBoxY + 2, lineBrush, true, fillBrush);
;            // 2nd Amount
            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 65, DetailBoxY, DetailBoxX + 80, DetailBoxY + 2, lineBrush, true, fillBrush);
;            // Vertical lines for Details box
            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 22, DetailBoxY + 2, DetailBoxX + 22, DetailBoxY + 14);
;            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 37, DetailBoxY + 2, DetailBoxX + 37, DetailBoxY + 14);
;            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 65, DetailBoxY + 2, DetailBoxX + 65, DetailBoxY + 14);
;            // Total Boxes
            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 37, DetailBoxY + 14, DetailBoxX + 68, DetailBoxY + 16, lineBrush, true, fillBrush);
;            if ( PrintStmts) rpt.PrintLine(DetailBoxX + 63, DetailBoxY + 14, DetailBoxX + 85, DetailBoxY + 16, lineBrush, true);
;            */
            fnt = new Font("Arial", 12);
            if ( PrintStmts) rpt.PrintPageLine("PLEASE MAKE PAYMENTS TO:", 5, 19.5, fnt);
;
            // Message area
            //if ( PrintStmts) rpt.PrintLine(0, 25.5, 71, 36.5, lineBrush, true);
;            if ( PrintStmts) rpt.PrintLine(0, 21, 68, 32, lineBrush, true);
;
            if ( PrintStmts) rpt.FontSize = 18;
;            if ( PrintStmts) rpt.PrintPageLine("DO NOT SEND CASH. YOUR CANCELLED CHECK IS YOUR RECEIPT", 7, 33, fnt);
;            fnt = new Font("Wingdings", 16);
            if ( PrintStmts) rpt.PrintPageLine("\"", -6, 43.5, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("\"", 60, 43.5, fnt);
;
            fnt = new Font("Courier New", 11);
            if ( PrintStmts) rpt.PrintPageLine("•  —  •  —  •  —  • PLEASE TEAR AT PERFORATION AND RETURN STUB WITH PAYMENT  •  —  •  —  •  —  •", -5, 43.5, fnt);
;
            fnt = new Font("Arial", 12);
            if ( PrintStmts) rpt.PrintPageLine("DUE DATE:", TopNameX + 42, TopNameY + 1, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("ACCOUNT #:", AcctX, AcctY, fnt);
;                                
            if ( PrintStmts) rpt.PrintPageLine("PLEASE INCLUDE ACCOUNT", 46, 48, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("NUMBER ON CHECK", 46, 49, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("PLEASE MAKE PAYMENTS TO:", CorpNameX + 18, CorpNameY + 1, fnt);
;            // Bottom Total Box
            if ( PrintStmts) rpt.PrintLine(TotalDueX + 6, TotalDueY , TotalDueX + 25, TotalDueY + 2, lineBrush, true);
;        
            // Scan Line Box - Depracated 9/22/18
            //if (LockBox)
            //    if ( PrintStmts) rpt.PrintLine(ScanLineX + 5, ScanLineY , ScanLineX + 5 + ScanLineSize , ScanLineY + 2, lineBrush, true);
;
            if ( PrintStmts) rpt.PrintPageLine("RE:", TopNameX, TopNameY + 1, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("DUE DATE:", DueDateX -5 , DueDateY, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("CHARGES OR CREDITS", -5, 4, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("AMOUNT", 19, 4, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("CHARGES OR CREDITS", 30, 4, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("AMOUNT", 52, 4, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("PAY THIS AMOUNT", 33, TotalY, fnt);
;            if ( PrintStmts) rpt.PrintPageLine("TOTAL DUE", TotalDueX, TotalDueY + .5, fnt);
;            if ( PrintStmts) rpt.FontBold = false;
;
        }
    }
}
