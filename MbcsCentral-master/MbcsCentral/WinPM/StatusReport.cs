using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ExtensionMethods;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
    // Grand Totals are zero
    class StatusReport : ReportBase
    {
        static string title = "Status Report";
        Report rpt;
        ChargeCodes tblCodes;
        Building tblBuilding;
        Owners tblOwner;
        Payments[] tblPayments;
        int numHeaders = 12;
        int detailHeaderLine = 9;
        object[,] headers;
        Tenants[] tens;
        Charges[] tblCharges;
        enum NT_Fields
        {
            TenantName = 1,
            Apartment = 2,
            Type = 4,
            AlternateBillingAddresFlag = 6,
            LeaseLetterFlag = 7,
            LegalCode = 8
        }
        enum n_Fields
        {
            Rent = 1,
            Security = 2,
            Arrears = 4,
            Paid = 5,
            Rooms = 10,
            MoveInDate = 12,
            LeaseExpirationDate = 14
        }
        string[] NT = new string[9];
        object[] n = new object[15];
        System.Printing.PrintQueue queue = null;
        string bldStart;
        string bldEnd;
        bool chgSum;
        bool collected;
        decimal BALX, BALTOTAL, BALPAID;
        decimal ARREARS, VAC, Collect;
        decimal curRent, Advance, NW;
        float rms;
        int BALi;
        string sumflag;
        System.DateTime dt = default(System.DateTime);
        string BL;
        short k = 0;
        string codes = null;
        int subflag = 0;
        int MON = 0;
        string oName = null;

        string[,] ch = new string[73, 3];
        decimal[,] c = new decimal[73, 4];
        string[] PD = Enumerable.Repeat(string.Empty, 73).ToArray();
        decimal[] p = new decimal[73];
        decimal[] B = new decimal[9];
        decimal[] Sn = new decimal[9];
        decimal[] G = new decimal[9];
        string[] Monthx = new string[13];

        // Old Charge Summary arrays
        decimal[,] BC = new decimal[37, 3];
        decimal[,] SCn = new decimal[37, 3];
        decimal[,] GC = new decimal[37, 3];
        string[] xx = Enumerable.Repeat(string.Empty, 37).ToArray();
        string[] Gg = Enumerable.Repeat(string.Empty, 37).ToArray();

        // New Charge Summary variables
        SortedDictionary<string, ChargeRecords> CsumB = new SortedDictionary<string, ChargeRecords>();
        SortedDictionary<string, ChargeRecords> CsumS = new SortedDictionary<string, ChargeRecords>();
        SortedDictionary<string, ChargeRecords> CsumG = new SortedDictionary<string, ChargeRecords>();

        decimal[] sg = new decimal[9];
        string eb;
        string t;
        string sb;
        string x;
        short j;
        string[] BT = Enumerable.Repeat(string.Empty, 36).ToArray();
        decimal[,] BTn = new decimal[36, 3];
        string[] ST = Enumerable.Repeat(string.Empty, 36).ToArray();
        decimal[,] STn = new decimal[36, 3];
        string[] GT = new string[36];
        decimal[,] GTn = new decimal[36, 3];
        decimal Adjust;
        string[,] Bal = new string[100, 4];
        decimal[,] BALn = new decimal[100, 6];
        decimal[,] LO = new decimal[16, 3];
        string[,] bga = new string[3, 19];
        System.DateTime curDate;
        System.DateTime sd;
        System.DateTime ed;
        decimal carr;
        decimal camt;
        decimal cpaid;
        string BAD1;
        string RSA;
        string bld;
        string chg;
        string ct;
        string ctype_Renamed;
        string SN;
        string MDR;
        string BAD2;
        short o;
        string Phone;
        string sec;
        string blk;
        string lot;
        string Super;
        string Agt;
        decimal total;
        decimal Paid;
        decimal cpaid1;
        decimal OArrears;
        decimal CHGS;
        decimal cpaid2;
        decimal pamt;
        System.DateTime pdate;
        string FS;
        string PT;
        string pcode;
        int UNITS;
        bool subprinted;
        string SF;
        string PQ;
        string oldFont;
        short l;
        object oldSize;
        object oldQuality;
        decimal[] Cn = new decimal[5];
        decimal[] cg = new decimal[5];
        decimal[] ctn = new decimal[5];
        short LineLimit;
        short MaxLines;
        const int MINLINES = 60;

        // Properties
        bool BP = false;
        bool condensed = false;
        string fontname;
        float fontsize;
        Font mainFont;
        bool printTarget = false;

        public string StartingBuilding
        {
            set { bldStart = value; }
        }
        public string Endingbuilding
        {
            set { bldEnd = value; }
        }
        public bool ChargeSummary
        {
            set { chgSum = value; }
        }

        public bool CollectedBy
        {
            set { collected = value; }
        }
        public bool CondensePrint
        {
            set { condensed = value; }
        }
        public bool BoldPrint
        {
            set { BP = value; }
        }
        public string FontName
        {
            set { fontname = value; }
        }
        public float FontSize
        {
            set { fontsize = value; }
        }
        public bool TargetPrinter
        {
            set { printTarget = value; }
        }
        public bool PrintGrandTotal { get; set; }
        public bool UnitTotals { get; set; }

        public StatusReport()
        {

        }
        public StatusReport(string startBld, string endBld, bool chargeSummary, bool collectedFrom, bool condensePrint,
            bool boldPrint, string fontname, float fontsize, bool targetPrinter, DateTime curDate, bool GrandTotals, System.Printing.PrintQueue queue = null)
        {
            this.bldStart = startBld;
            this.bldEnd = endBld;
            this.chgSum = chargeSummary;
            this.collected = collectedFrom;
            this.condensed = condensePrint;
            this.BP = boldPrint;
            this.PrintGrandTotal = GrandTotals;
            this.fontname = fontname;
            this.fontsize = fontsize;
            this.printTarget = targetPrinter;
            this.curDate = curDate;
            this.queue = queue;
        }


        public override void Process()
        {
            PrintStatus();
        }
        public void PrintStatus()
        {
            headers = new object[numHeaders, 2];
            short hh = 0;
            double BALPAID = 0;
            double BALTOTAL = 0;
            double BALX = 0;
            double Collect = 0;
            int xy = 0;
            decimal VAC = 0;
            decimal curRent = 0;
            decimal ARREARS = 0;
            decimal Advance = 0;
            //decimal NW = 0;
            //float rms = 0;
            decimal balance = 0;
            short m = 0;
            short XO = 0;
            object NewPage = null;
            short f = 0;
            short BALi = 0;
            object xsql = null;
            short i = 0;

            string ln = null;
            string lease = null;

            for (int a = 0; a < 16; a++)
                LO[a, 1] = -1;

            // Set up report object
            mainFont = new Font(fontname, (float)(fontsize * (96.0 / 72.0)), (FontStyle)(BP ? FontStyle.Bold : FontStyle.Regular));
            rpt = new Report(title, fontname, fontsize) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
            rpt.SetFont = mainFont;
            //if (printTarget)
            //    if (!rpt.ChoosePrinter(queue))
            //        return;
            //rpt.PrintFlag = printTarget;
            //rpt.LeftMarginInches = (Single)0.25;
            //rpt.TopMarginInches = (Single) 0.25;
            //rpt.BottomMargin = (Single) 0.25;

            for (i = 1; i <= 12; i++)
            {
                Monthx[i] = String.Format("{0:MMMM}", new DateTime(2000, i, 1));
            }
            dt = curDate;
            MON = DateAndTime.Month(curDate);
            //curDate = Convert.ToDateTime(DateAndTime.DateString);

            // Get Status Report Groups

            try
            {
                Control tbl = Control.load(1);
                subflag = Int32.Parse(tbl.StatusReportGrouping);
                i = 1;
                StatGroups[] tblStat = StatGroups.loads(GlobalShared.user.Account.clientId);
                foreach (StatGroups stat in tblStat)
                {
                    bga[1, i] = stat.FromGroup;
                    bga[2, i] = stat.ToGroup;
                    i++;
                }
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Retrieving Report Grouping from Control table: " + ex.Message, false);
            }
            switch (subflag)
            {
                case 0:
                    // by Owner
                    try
                    {
                        tens = Tenants.loadForStatus(GlobalShared.user.Account.clientId);
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Retrieving Tenants by Owner <clientId - " + GlobalShared.user.Account.clientId.ToString() + ">: " + ex.Message, false);
                    }
                    break;
                case 1:
                case 2:
                    // by building number
                    try
                    {
                        tens = Tenants.load();
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Retrieving Tenants by Building: " + ex.Message, true);
                    }
                    break;
            }
            codes = "";
            try
            {
                ChargeCodes[] chgCodes = ChargeCodes.loads(GlobalShared.user.Account.clientId);
                foreach (ChargeCodes cd in chgCodes)
                    codes += cd.ChargeCode;
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Retrieving Charge Codes: " + ex.Message, true);
            }
            string st = "";
            string name = "";
            DateTime leaseDate;
            // Read Tenant Table
            foreach (Tenants tblTenant in tens)
            {

                try
                {
                    bool printed = false;
                    mapTenants(tblTenant);
                    if (String.Compare(t.Substring(0, 3), bldStart) < 0)
                        continue;
                    if (subflag == 0 && String.Compare(t.Substring(0, 3), bldEnd) > 0)
                        continue;
                    if (String.Compare(t.Substring(0, 3), bldEnd) > 0)
                        break;
                    if (t.Length == 7 && t.Substring(6, 1).Equals("B"))
                    {
                        BALi += 1;
                        Bal[BALi, 1] = (string)t;
                        Bal[BALi, 2] = (string)NT[1];
                        Bal[BALi, 3] = (string)NT[2];
                        BALn[BALi, 1] = (decimal)n[1];
                        BALn[BALi, 2] = (decimal)n[4];
                        BALn[BALi, 3] = (decimal)Adjust;
                        BALn[BALi, 4] = (decimal)n[5];

                        Payments[] pays = Payments.loadBySequence(tblTenant.SequenceNumber);
                        if (pays != null && pays.Length > 0)
                            foreach (Payments pay in pays)
                                BALn[BALi, 5] = (decimal)BALn[BALi, 5] + (decimal)pay.Amount;
                        continue;
                    }
                    if (tblTenant.NewTenantFlag)
                        t.Insert(6, "N");
                    Paid = 0;
                    if (string.IsNullOrEmpty(sb))
                        sb = Strings.Left(t, 3);
                    if (f == 0)
                        goto ReadBldTbl;
                    if (Strings.Left(t, 3) == BL)
                        goto ReadCharges;
                    BldTotals();

                    bool chgSumPrinted = false;
                    sumflag = "B";
                    if (chgSum)
                    {
                        chgSumPrinted = ChgSummary();

                    }
                    for (i = 1; i <= 18; i++)
                    {
                        if (BL == bga[2, i])
                        {
                            subtotals(bga[1, i], bga[2, i], sg, cg);
                            printed = true;
                        }
                        if (Strings.Left(t, 3) == bga[1, i])
                        {
                            for (j = 1; j <= 8; j++)
                            {
                                sg[j] = 0;
                                if (j < 5)
                                    cg[j] = 0;
                            }
                        }
                    }
                    if (printed)
                        PageSkip(false);
                    printed = false;

                    ReadBldTbl:
                    // Read Building Table
                    tblBuilding = Building.loadByBuildingNumber(t.Substring(0, 3), GlobalShared.user.Account.clientId);
                    if (tblBuilding != null)
                    {
                        mapBuilding(tblBuilding);
                        if (f != 0)
                        {

                            sumflag = "S";
                            switch (subflag)
                            {
                                case 0:
                                    //by owner
                                    if (o != XO)
                                    {
                                        subTotal();
                                        if (subprinted)
                                            PageSkip(false);
                                    }
                                    break;
                                case 1:
                                    //by first digit of building number
                                    if (Strings.Left(BL, 1) != Strings.Left(t, 1))
                                    {
                                        subTotal();
                                        if (subprinted)
                                            PageSkip(false);
                                    }
                                    break;
                            }
                        }
                    }

                    // Create Separate document for each building
                    if (printTarget && rpt.Document != null && (rpt.Document.Pages.Count > 0 || rpt.TotalLinesPrinted > 0))
                    {
                        rpt.PrinterChosen = false;
                        rpt.PrintDocument(queue);
                        rpt.NewDocument();
                    }
                    else
                        if (BL != null) PageSkip(false);
                    BL = Strings.Left(t, 3);
                    f = 1;
                    XO = o;
                    eb = BL;

                    // Read Owner Table
                    oName = "*";
                    tblOwner = Owners.loadByOwnerNumber(tblBuilding.OwnerNumber, GlobalShared.user.Account.clientId);
                    if (tblOwner != null)
                    {
                        oName = tblOwner.OwnerName;
                    }
                    tblOwner = null;
                    addHeaders();
                    rpt.PrintHeader();

                    ReadCharges:
                    // Read Charges
                    ChargeRecords chgRec;
                    k = 0;
                    ch[1, 1] = " ";
                    ch[1, 2] = " ";
                    tblCharges = Charges.loadBySeqNum(tblTenant.SequenceNumber);
                    if (tblCharges != null && tblCharges.Length > 0)
                    {
                        foreach (Charges chgs in tblCharges)
                        {
                            if (System.DBNull.Value.Equals(chgs.StartingDate) || chgs.StartingDate <= dt)
                            {
                                mapCharges(chgs);
                                if (sd <= curDate)
                                {
                                    k++;
                                    m = k;
                                    ch[k, 1] = chg;
                                    ch[k, 2] = ctype_Renamed;
                                    c[k, 1] = camt;
                                    c[k, 2] = cpaid;
                                    OArrears = OArrears + carr;
                                    c[k, 3] = carr;
                                    CHGS = CHGS + camt;
                                    cpaid1 = cpaid1 + cpaid;
                                    cpaid2 = cpaid2 + cpaid;
                                    if (chgs.LegalRent)
                                    {
                                        ch[k, 2] = "*";
                                        B[1] = B[1] + camt;
                                    }

                                    // New charge summary accumulation code
                                    if (!CsumB.TryGetValue(chg, out chgRec))
                                    {
                                        chgRec = new ChargeRecords();
                                        CsumB.Add(chg, chgRec);
                                    }
                                    chgRec.Code = chg;
                                    chgRec.Charged += camt;
                                    chgRec.Paid += cpaid;
                                    chgRec.Arrears += carr;
                                    CsumB[chg] = chgRec;
                                }
                            }
                        }
                    }

                    // Read Payments
                    k = 0;
                    tblPayments = Payments.loadBySequence(tblTenant.SequenceNumber);
                    if (tblPayments != null && tblPayments.Length > 0)
                    {
                        foreach (Payments pay in tblPayments)
                        {
                            //mapPayments(pay);
                            k++;
                            p[k] = pay.Amount;
                            if (k > m)
                                m = k;
                            x = pay.DateofEntry.ToString("MM/dd");
                            PD[k] = x;
                            Paid = Paid + pay.Amount;
                        }
                    }
                    tblPayments = null;
                    if (rpt.CurrentHeight >= rpt.MaxPageHeight)
                        PageSkip();
                    if (string.IsNullOrEmpty(NT[8]) | Information.IsDBNull(NT[8]))
                        NT[8] = " ";
                    total = (decimal)n[1] + (decimal)n[4] + OArrears + CHGS;
                    balance = total - Paid;
                    lease = n[14].ToString();
                    if (Strings.Len(lease) == 0)
                        lease = " ";
                    else
                    {
                        leaseDate = DateTime.Parse(lease);
                        if (leaseDate <= GlobalShared.NULL_DATE)
                            lease = " ";
                        else
                            lease = leaseDate.ToString("MM/yy");
                    }
                    if (string.IsNullOrEmpty(PD[1]) | Information.IsDBNull(PD[1]))
                        PD[1] = " ";

                    // Tenant Detail Line
                    if (NT[1].Length > 18)
                        name = NT[1].Substring(0, 18);
                    else
                        name = NT[1];
                    st = t + (tblTenant.NewTenantFlag ? "*" : " ") + NT[2].PadRight(4) + " " + (name.Trim().PadRight(18) + " " + (System.DBNull.Value.Equals(NT[4]) ? "  " : NT[4].Trim().PadRight(2)) + " " + cformat((decimal)n[1], 8) + " " + cformat((decimal)n[4], 8) + " " + NT[8].Left(2,2) + " " + cformat(c[1, 3], 8) + " " + cformat(c[1, 1], 9));
                    st += " " + ch[1, 1].PadRight(2) + " " + ch[1, 2] + " " + cformat(total, 9) + " ";
                    st += cformat(p[1], 9) + " " + PD[1].Trim().PadLeft(5) + " " + cformat(balance, 9) + " ";
                    st += cformat(Convert.ToDecimal(n[2]), 8) + " " + lease.PadLeft(5) + " ";
                    st += String.Format("{0:#0.0}", Convert.ToDecimal(n[10])).PadLeft(4);
                    if (rpt.PrintPageLine(st))
                        PageSkip();
                    p[1] = 0;
                    PD[1] = " ";
                    // Additional Charge and Payment lines
                    for (i = 2; i <= m; i++)
                    {
                        st = "";
                        if (c[i, 1] != 0 | c[i, 3] != 0)
                        {
                            st = String.Format("{0}", new String(' ', 55) + cformat(c[i, 3], 8) + " " + cformat(c[i, 1], 9) + " " + ch[i, 1].PadRight(2) + " " + ch[i, 2]);
                        }
                        if (p[i] != 0)
                        {
                            st += new String(' ', 89 - st.Length) + cformat(p[i], 9) + " " + PD[i];
                            PD[i] = " ";
                        }
                        if (c[i, 1] != 0 | c[i, 3] != 0 | p[i] != 0)
                        {
                            if (rpt.PrintPageLine(st))
                                PageSkip();
                            p[i] = 0;
                        }
                        if (rpt.CurrentHeight >= rpt.MaxPageHeight)
                            PageSkip();
                    }
                    if (tblTenant.NewTenantFlag)
                        goto X03;
                    UNITS++;
                    rms += (float)n[10];
                    for (i = 1; i <= 15; i++)
                    {
                        if (LO[i, 1] == null || (float)LO[i, 1] == -1)
                            goto X01;
                        if ((float)LO[i, 1] == (float)n[10])
                            goto X02;
                    }
                    goto X03;
                    X01:
                    LO[i, 1] = Convert.ToDecimal(n[10]);
                    X02:
                    if (LO[i, 2] != null)
                        LO[i, 2]++;
                    X03:
                    B[1] = B[1] + (decimal)n[1];
                    B[5] = B[5] + total;
                    if (tblTenant.NewTenantFlag)
                    {
                        NW = NW + Paid;
                    }
                    else
                    {
                        B[6] = B[6] + Paid;
                        for (i = 1; i <= 35; i++)
                        {
                            if (string.IsNullOrEmpty(BT[i]) || BT[i] == NT[4])
                                break; // TODO: might not be correct. Was : Exit For
                        }
                        BT[i] = NT[4];
                        BTn[i, 1]++;
                        BTn[i, 2] += Convert.ToDecimal(n[10]);
                        if (Paid - cpaid1 != 0)
                        {
                            total = total - CHGS - OArrears;
                            decimal x = Paid - cpaid1;
                            if (x > total)
                            {
                                Advance = Advance + x - total;
                                x -= (x - total);
                            }
                            if (x >= (decimal)n[4])
                            {
                                ARREARS = ARREARS + (decimal)n[4];
                                x = x - (decimal)n[4];
                            }
                            else
                            {
                                ARREARS = ARREARS + x;
                                x = 0;
                            }
                            if (x >= (decimal)n[1])
                                curRent += (decimal)n[1];
                            else
                                curRent += x;
                        }
                    }
                    if (Strings.InStr(Strings.UCase(NT[1]), "VACANT") > 0)
                        VAC = VAC + (decimal)n[1];
                    B[2] = B[2] + (decimal)n[4];
                    B[3] = B[3] + OArrears;
                    B[4] = B[4] + CHGS;
                    B[7] = B[7] + balance;
                    B[8] = B[8] + (decimal)n[2];
                    Paid = 0;
                    for (i = 1; i <= m; i++)
                    {
                        xy = Strings.InStr(codes, ch[i, 1]) - 1;
                        if (xy > 0 & ch[i, 1] != "=")
                        {
                            try
                            {
                                BC[xy, 1] = BC[xy, 1] + c[i, 1];
                                BC[xy, 2] = BC[xy, 2] + c[i, 2];
                            }
                            catch { }
                        }
                        Array.Clear(ch, 0, ch.Length);
                        ch[i, 1] = " ";
                        ch[i, 2] = " ";
                        c[i, 1] = 0;
                        c[i, 2] = 0;
                        c[i, 3] = 0;
                    }
                    OArrears = 0;
                    CHGS = 0;
                    cpaid1 = 0;
                    Paid = 0;
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message + " / " + tblTenant.BuildingNumber + tblTenant.TenantNumber, false);
                    if (ex.InnerException != null)
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.InnerException.Message, false);

                }

            }
            rptEnd:

            //********************************************************
            // End of report                                         *
            //********************************************************
            BldTotals();
            sumflag = "B";
            if (chgSum)
            {
                PageSkip();
                if (ChgSummary())
                    rpt.NewPage();
            }
            PageSkip(false);
            sumflag = "S";
            subTotal(true);
            if (subprinted)
                rpt.NewPage(false);
            if (PrintGrandTotal)
            {
                sumflag = "G";
                for (i = 1; i <= 35; i++)
                {
                    ST[i] = GT[i];
                    STn[i, 1] = GTn[i, 1];
                    STn[i, 2] = GTn[i, 2];
                    GTn[i, 1] = 0;
                    GTn[i, 2] = 0;
                }
                rpt.PrintPageLine("");
                rpt.PrintPageLine("");
                rpt.PrintPageLine(dt.ToString() + new string(' ', 40) + "GRAND TOTALS FOR REPORT");
                rpt.PrintPageLine("");
                if (subtotals("", "", G, ctn))
                    rpt.NewPage(false);
                subTotal(false);

                for (i = 1; i <= 36; i++)
                {
                    if (i < 9)
                        Sn[i] = G[i];
                    //for (j = 1; j <= 2; j++) {
                    //	BC[i, j] = GC[i, j];
                    //}
                }

                CsumB.Clear();
                ChargeRecords chrg = null;
                foreach (KeyValuePair<string, ChargeRecords> k in CsumG)
                {
                    CsumB.Add(k.Key, (ChargeRecords)k.Value);
                }
                rpt.PrintPageLine("");
                ChgSummary();
                rpt.NewPage();
            }

            // Printer.EndDoc();
            //rpt.Document = doc;
            if (rpt.Document != null && rpt.Document.Pages.Count > 0)
            {
                if (printTarget)
                {
                    //rpt.PrinterChosen = true;
                    rpt.PrintDocument(queue);
                }
                else
                    rpt.DisplayDocument();
            }

            for (i = 1; i <= 36; i++)
            {
                if (i < 9) { Sn[i] = 0; G[i] = 0; }
                if (i <= 35)
                {
                    GT[i] = "";
                    GTn[i, 1] = 0;
                    GTn[i, 2] = 0;
                    ST[i] = "";
                    STn[i, 1] = 0;
                    STn[i, 2] = 0;
                }
                for (j = 1; j <= 2; j++)
                {
                    SCn[i, j] = 0;
                    GC[i, j] = 0;
                }
            }
        }

        private void PageSkip(bool heading = true, bool detail = true)
        {
            //*************
            //* Page Skip *
            //*************
            //PageContent page1Content = new PageContent();
            //((System.Windows.Markup.IAddChild)page1Content).AddChild(page);
            //doc.Pages.Add(page1Content);
            rpt.NewPage();
            rpt.CurrentHeight = 0;
            if (heading)
            {
                rpt.CurrentHeight = rpt.TopMargin;
                sumflag = "B";
                BldHeading(detail);
            }
        }

        private void RptHeading()
        {
            //******************
            //* Report Heading *
            //******************
            rpt.PageNumber = 1;
            addHeaders();
            rpt.PrintHeader();
        }

        private void BldHeading(bool printunderlines = true)
        {
            //********************
            //* Building Heading *
            //********************
            switch (sumflag)
            {
                case "S":
                    rpt.PrintPageLine("");
                    rpt.PrintPageLine(String.Format("{0:MM/dd/yyyy}{1}", dt, new string(' ', 35) + "SUMMARY FOR BUILDINGS " + sb + " to " + eb));
                    rpt.PrintPageLine(new String('=', 133));
                    break;
                case "G":
                    rpt.PrintPageLine("");
                    rpt.PrintPageLine(String.Format("{0:MM/dd/yyyy}{1}", dt, new string(' ', 40) + "GRAND TOTALS FOR REPORT"));
                    rpt.PrintPageLine(new string('=', 133));
                    rpt.PrintPageLine("");
                    l = 4;
                    break;
                default:
                    rpt.PrintPageLine("");
                    rpt.PrintPageLine("Building: " + BL + new string(' ', 45) + "PAGE " + rpt.PageTag + new string(' ', 5) + dt.ToString("MM/dd/yyyy"));
                    rpt.PrintPageLine(new string('-', 133));
                    if (printunderlines)
                    {
                        rpt.PrintPageLine((string)headers[detailHeaderLine, 0]);
                        rpt.PrintPageLine(new string('-', 133));
                    }
                    break;
            }
        }

        private void BldTotals()
        {
            //******************
            // Building Totals *
            //******************

            Collect = B[1] - VAC;
            rpt.PrintPageLine(new string('-', 132));
            rpt.PrintPageLine(String.Format("  TOTALS{0}{1}", new string(' ', 24), cformat(B[1], 10) + "          " + cformat(B[3], 10) + "                " + cformat(B[5], 10) + "                " + cformat(B[7], 10)));
            if (rpt.PrintPageLine(String.Format("{0}{1}", new string(' ', 42), cformat(B[2], 10) + "          " + cformat(B[4], 11) + "               " + cformat(B[6], 10) + "                " + cformat(B[8], 9))))
            {
                PageSkip(false);
            }

            if (BALi != 0)
            {
                rpt.PrintPageLine("");
                rpt.PrintPageLine(new string(' ', 10) + "< FORMER TENANTS >" + new string(' ', 86) + "PAID CURR");
                if (rpt.PrintPageLine(""))
                {
                    PageSkip(false);
                }
                for (int i = 1; i <= BALi; i++)
                {
                    BALX = (decimal)BALn[i, 1] + (decimal)BALn[i, 2] + (decimal)BALn[i, 3] - (decimal)BALn[i, 4];
                    if (rpt.PrintPageLine(Bal[i, 1] + " " + Bal[i, 3] + " " + Strings.Left(Bal[i, 2], 20) + "   " + cformat((decimal)BALn[i, 1], 8) + " " + cformat((decimal)BALn[i, 2], 8) + "   " + cformat((decimal)BALn[i, 3], 8) + "          " + "   " + cformat((decimal)BALn[i, 1] + (decimal)BALn[i, 2] + (decimal)BALn[i, 3], 9) + "  " + cformat((decimal)BALn[i, 4], 9) + "      " + cformat(BALX, 9) + "   " + cformat((decimal)BALn[i, 5], 9)))
                    {
                        PageSkip(false);
                    }
                    BALTOTAL += (decimal)BALn[i, 1] + (decimal)BALn[i, 2] + (decimal)BALn[i, 3] - (decimal)BALn[i, 4];
                    Bal[i, 1] = "";
                    Bal[i, 2] = "";
                    for (j = 1; j <= 5; j++)
                        BALn[i, j] = (decimal)0;
                    BALPAID = BALPAID + (decimal)BALn[i, 5];
                }
                rpt.PrintPageLine(String.Format("{0}{1}", new string(' ', 93), "TOTAL=>  " + cformat(BALTOTAL, 10) + " " + cformat(BALPAID, 10)));
            }
            if (rpt.PrintPageLine(""))
                PageSkip(true, false);
            /* Following commented out as per Moslin as not being useful 10/22/2014 */
            /* Restored as per Leemar as an option 09/28/2018 */
            if (collected)
            {
                rpt.PrintPageLine(String.Format("{0}{1}{2}", new string(' ', 51), "COLLECTED FROM CURRENT TENANTS: ", cformat(B[6], 10)));
                rpt.PrintPageLine(String.Format("{0}{1}{2}", new string(' ', 51), "COLLECTED FROM  FORMER TENANTS: ", cformat(BALPAID, 10)));
                rpt.PrintPageLine(String.Format("{0}{1}{2}", new string(' ', 51), "COLLECTED FROM     NEW TENANTS: ", cformat(NW, 10)));
                if (rpt.PrintPageLine(String.Format("{0}{1}{2}", new string(' ', 51), "COLLECTED FROM     ALL TENANTS: ", cformat(BALPAID + NW + B[6], 10))))
                {
                    PageSkip();
                }
            }
            Cn[1] = Cn[1] + B[6];
            Cn[2] = Cn[2] + BALPAID;
            Cn[3] = Cn[3] + NW;
            Cn[4] = Cn[4] + (BALPAID + NW + B[6]);
            cg[1] = cg[1] + B[6];
            cg[2] = cg[2] + BALPAID;
            cg[3] = cg[3] + NW;
            cg[4] = cg[4] + (BALPAID + NW + B[6]);
            ctn[1] = ctn[1] + B[6];
            ctn[2] = ctn[2] + BALPAID;
            ctn[3] = ctn[3] + NW;
            ctn[4] = ctn[4] + (BALPAID + NW + B[6]);
            for (int i = 1; i <= 8; i++)
            {
                Sn[i] += B[i];
                sg[i] = sg[i] + B[i];
                B[i] = 0;
            }
            if (rpt.PrintPageLine(""))
                PageSkip(true, false);

            string s = "";

            if (UnitTotals)
            {
                s = "   UNITS= " + String.Format("{0:####0}", UNITS) + " ROOMS= " + String.Format("{0:####0.0}", rms);
                //s += String.Format("{0}{1}", new string(' ', 51 - s.Length), "SPECIAL CHGS PAID:" + cformat(cpaid2, 10) + "    ADVANCES PAID:  " + cformat(Advance, 10));
                if (rpt.PrintPageLine(s))
                {
                    PageSkip(true, false);
                }
                /* Following commented out as per Moslin as not being useful 10/22/2014 
                 if ((decimal)BTn[1, 1] != 0)
                     s = BT[1] + " units= " + String.Format("{0:####0}",BTn[1, 1]) + " rooms= " + String.Format("{0:####0.0}", BTn[1, 2]);
                 s += String.Format("{0}{1}", new string(' ', 55 - s.Length), "ARREARS PAID:     " + cformat( ARREARS,  10) + "    VACANCY LOSS:   " + cformat( VAC,  10));
                 rpt.PrintPageLine(s);
                 s = "";
                 if ((decimal)BTn[2, 1] != 0)
                 {
                     s = String.Format("{0}", BT[2] + " units= " + String.Format("{0:####0}", BTn[2, 1]) + " rooms= " + String.Format("{0:####0.0}", BTn[2, 2]));
                 }
                 s += String.Format("{0}{1}", new string(' ', 55 - s.Length), "CURRENT RENT PAID:" + cformat( curRent,  10) + " COLLECTABLE RENT:  " + cformat( Collect,  10));
                 rpt.PrintPageLine(s);
                 s= "";
                 * */
                for (int i = 1; i <= 35; i++)
                {
                    if ((decimal)BTn[i, 1] > 0 || (decimal)BTn[i, 2] > 0)
                    {
                        if (rpt.PrintPageLine(BT[i].PadLeft(2) + " units = " + String.Format("{0:####0}", BTn[i, 1]) + " rooms = " + String.Format("{0:####0.0}", BTn[i, 2])))
                        {
                            PageSkip(true, false);
                        }
                    }
                }
            }
            rpt.PrintPageLine("BUILDING LAYOUT: ");
            s = "";
            for (int i = 1; i <= 15; i++)
            {
                if ((decimal)LO[i, 1] != -1)
                    s += (Strings.Mid(Conversion.Str(LO[i, 2]), 2) + "/" + Strings.Mid(Conversion.Str(LO[i, 1]), 2) + " ");
                LO[i, 1] = -1;
                LO[i, 2] = 0;
            }
            if (rpt.PrintPageLine(s))
                PageSkip(false);
            int k;
            for (j = 1; j <= 35; j++)
            {
                for (k = 1; k <= 35; k++)
                    if (string.IsNullOrEmpty(ST[k]) | ST[k] == BT[j])
                        break; // TODO: might not be correct. Was : Exit For
                ST[k] = BT[j];
                STn[k, 1] += BTn[j, 1];
                STn[k, 2] += BTn[j, 2];
                BT[j] = "";
                BTn[j, 1] = 0;
                BTn[j, 2] = 0;
            }
            ARREARS = 0;
            VAC = 0;
            curRent = 0;
            Advance = 0;
            cpaid2 = 0;
            UNITS = 0;
            rms = 0;
            BALi = 0;
            BALTOTAL = 0;
            BALPAID = 0;
            NW = 0;
        }

        private bool ChgSummary()
        {
            //******************
            //* Charge Summary *
            //******************
            bool printed = false;
            if (chgSum)
            {
                string s = "";
                rpt.PrintPageLine("");
                if (rpt.PrintPageLine("") || rpt.LinesPrinted > MINLINES)
                    PageSkip();
                rpt.PrintPageLine(String.Format("{0}{1}", new string(' ', 50), "<ADDITIONAL CHARGES SUMMARY>"));
                rpt.PrintPageLine("");
                printed = true;
                int l = CsumB.Count;
                string[] st = new string[8];
                string[] pd = new string[8];
                k = 0;
                ChargeRecords chg = null;
                ChargeRecords chgS = null;
                foreach (KeyValuePair<string, ChargeRecords> keypair in CsumB)
                {
                    chg = (ChargeRecords)keypair.Value;
                    tblCodes = ChargeCodes.load(chg.Code, GlobalShared.user.Account.clientId);

                    if (tblCodes != null)
                        chg.Description = (System.DBNull.Value.Equals(tblCodes.ChargeDescription) ? " " : tblCodes.ChargeDescription);
                    st[k] = "tab(" + (k * 16).ToString() + ")";
                    st[k + 1] = (chg.Code + " " + chg.Description);
                    if (st[k + 1].Length > 20)
                        st[k + 1] = st[k + 1].Substring(0, 20);
                    st[k + 1] = st[k + 1].PadRight(20) + chg.Charged.ToString("#####0.00").PadLeft(10);
                    if (chg.Paid != (decimal)0.0)
                    {
                        pd[k] = "tab(" + (k * 16 + 15).ToString() + ")";
                        pd[k + 1] = "paid".PadRight(5) + chg.Paid.ToString("#####0.00").PadLeft(10);
                    }
                    k += 2;
                    if (k > 7)
                    {
                        if (rpt.PrintPageLine(st))
                            PageSkip();
                        rpt.PrintPageLine(pd);
                        rpt.PrintPageLine(new string('-', 130));
                        k = 0;
                        Array.Clear(st, 0, st.Length);
                        Array.Clear(pd, 0, pd.Length);
                    }
                    if (!CsumS.TryGetValue(chg.Code, out chgS))
                    {
                        chgS = new ChargeRecords();
                        chgS.Code = chg.Code;
                    }
                    chgS.Charged += chg.Charged;
                    chgS.Arrears += chg.Arrears;
                    chgS.Paid += chg.Paid;
                    CsumS[chg.Code] = chgS;
                }
                if (k > 0)
                {
                    rpt.PrintPageLine(st);
                    rpt.PrintPageLine(pd);
                    rpt.PrintPageLine(new string('-', 130));
                }
                CsumB.Clear();
                Array.Clear(st, 0, st.Length);
                Array.Clear(pd, 0, pd.Length);
            }
            return printed;
        }

        private void subTotal(bool sub2 = false, bool print = true)
        {

            //**************
            //* Sub Totals *
            //**************
            subprinted = false;
            if (sub2 || (subflag < 2 & eb != sb && !string.IsNullOrEmpty(eb)))
            {
                if (!sub2 && subtotals(sb, eb, Sn, Cn) && print)
                    PageSkip();
            }
                subTotal2:
                subprinted = false;
                if (!sub2 && print && UnitTotals)
                {
                    for (int i = 1; i <= 35; i++)
                    {
                        if ((decimal)STn[i, 1] != 0)
                        {
                            if (rpt.PrintPageLine(ST[i] + " units= " + String.Format("{0:####0}", STn[i, 1]) + "  rooms= " + String.Format("{0:####0.0}", STn[i, 2])))
                                PageSkip();
                        }
                    }
                    subprinted = true;
                }
            for (j = 1; j <= 35; j++)
            {
                for (k = 1; k <= 35; k++)
                {
                    if (string.IsNullOrEmpty(GT[k]) || GT[k] == ST[j])
                        break;
                }
                GT[k] = ST[j];
                GTn[k, 1] += STn[j, 1];
                GTn[k, 2] += STn[j, 2];
                ST[j] = "";
                STn[j, 1] = 0;
                STn[j, 2] = 0;
            }
            for (int i = 1; i <= 8; i++)
            {
                G[i] = G[i] + Sn[i];
                Sn[i] = 0;
                if (i < 5)
                    Cn[i] = 0;
            }
            /*
			for (int i = 1; i <= 36; i++)
			{
				for (j = 1; j <= 2; j++)
				{
					BC[i, j] = SCn[i, j];
					GC[i, j] = GC[i, j] + SCn[i, j];
				}
			} */
            ChargeRecords ch;
            CsumB.Clear();
            foreach (KeyValuePair<string, ChargeRecords> k in CsumS)
            {
                CsumB.Add(k.Key, (ChargeRecords)k.Value);

                if (!CsumG.TryGetValue(k.Key, out ch))
                {
                    ch = new ChargeRecords();
                    ch.Code = k.Key;
                    CsumG.Add(k.Key, ch);
                }
                ch = CsumG[k.Key];
                ch.Charged += (decimal)((ChargeRecords)k.Value).Charged;
                ch.Arrears += (decimal)((ChargeRecords)k.Value).Arrears;
                ch.Paid += (decimal)((ChargeRecords)k.Value).Paid;
                CsumG[k.Key] = ch;
            }

            if (!sub2 && subflag < 2 && eb != sb)
                ChgSummary();

            CsumB.Clear();
            CsumS.Clear();
            /* for (int i = 1; i <= 36; i++)
			{
				for (j = 1; j <= 2; j++)
				{
					BC[i, j] = 0;
					SCn[i, j] = 0;
				}
			} */
            sb = "";


        }

        private void addHeaders()
        {
            rpt.PageNumber = 1;

            headers[1, 0] = String.Format("{0:MM/dd/yyyy}{1}{2}{3}{4}  {5}", dt, new string(' ', 40), "RENT REPORT FOR MONTH OF " + Monthx[MON], new string(' ', 43), "Page", rpt.PageTag);
            headers[1, 1] = mainFont;
            headers[2, 0] = String.Format("{0}{1} {2}", new string(' ', 50), "PREPARED FOR", oName);
            headers[2, 1] = mainFont;
            headers[4, 0] = String.Format("{0} {1}", "BLDG NO:", BL);
            headers[4, 1] = mainFont;
            headers[5, 0] = String.Format("{0} {1}   MDR {2}   BLK {3}   LOT {4}   SEC {5}   AGENT {6}", BAD1, BAD2, MDR, blk, lot, sec, Agt);
            headers[5, 1] = mainFont;
            headers[6, 0] = String.Format("SUPT: {0}/PHONE: {1}", Super, Phone);
            headers[6, 1] = mainFont;
            headers[8, 0] = new string('-', 134);
            headers[8, 1] = mainFont;
            headers[9, 0] = "TENANT# APT NAME               TY  RENT/MO  RNT ARR L   OTH ARR   CHARGES T        TOTAL      PAID  DATE   BALANCE SECURITY LEASE  RMS";
            headers[9, 1] = mainFont;
            headers[10, 0] = new string('-', 134);
            headers[10, 1] = mainFont;

            rpt.HeaderLines = headers;
        }

        public bool subtotals(string sb, string eb, decimal[] sg, decimal[] Cn)
        {
            bool printed;
            if (rpt.PrintPageLine("") || rpt.LinesPrinted > MINLINES)
                PageSkip();
            if (!string.IsNullOrEmpty(sb))
            {
                rpt.PrintPageLine(new string(' ', 50) + "SUBTOTALS FOR BUILDINGS " + sb + " to " + eb);
            }
            rpt.PrintPageLine(new string('-', 133));
            rpt.PrintPageLine((string)headers[detailHeaderLine, 0]);
            rpt.PrintPageLine(new string('-', 133));
            rpt.PrintPageLine("  TOTALS" + new string(' ', 24) + cformat(sg[1], 10) + "            " + cformat(sg[3], 10) + "              " + cformat(sg[5], 10) + "                " + cformat(sg[7], 10));
            printed = rpt.PrintPageLine(new string(' ', 44) + cformat(sg[2], 10) + "          " + cformat(sg[4], 11) + "              " + cformat(sg[6], 10) + "              " + cformat(sg[8], 10));
            if (collected)
            {
                rpt.PrintPageLine("");
                rpt.PrintPageLine(new string(' ', 51) + "COLLECTED FROM CURRENT TENANTS:   " + cformat(Cn[1], 10));
                rpt.PrintPageLine(new string(' ', 51) + "COLLECTED FROM  FORMER TENANTS:   " + cformat(Cn[2], 10));
                rpt.PrintPageLine(new string(' ', 51) + "COLLECTED FROM     NEW TENANTS:   " + cformat(Cn[3], 10));
                printed = (rpt.PrintPageLine(new string(' ', 51) + "COLLECTED FROM     ALL TENANTS:   " + cformat(Cn[4], 10)));
            }
            return printed;
        }

        public void mapTenants(Tenants ten)
        {
            short i = 0;

            try
            {
                t = ten.BuildingNumber + ten.TenantNumber;
                NT[1] = ten.TenantName;
                NT[2] = ten.Apartment;
                NT[4] = (System.DBNull.Value.Equals(ten.Type) ? "  " : ten.Type.Trim());
                NT[6] = ten.AlternateBillingAddressFlag.ToString();
                NT[7] = ten.LeaseLetterFlag.ToString();
                NT[8] = (System.DBNull.Value.Equals(ten.LegalCode) ? " " : ten.LegalCode);
                n[1] = (System.DBNull.Value.Equals(ten.Rent) ? (decimal)0 : (decimal)ten.Rent);
                n[2] = (System.DBNull.Value.Equals(ten.Security) ? (decimal)0 : (decimal)ten.Security);
                n[4] = (System.DBNull.Value.Equals(ten.Arrears) ? (decimal)0 : (decimal)ten.Arrears);
                n[5] = (System.DBNull.Value.Equals(ten.Paid) ? (decimal)0 : (decimal)ten.Paid);
                n[10] = (System.DBNull.Value.Equals(ten.Rooms) ? (Single)0 : (Single)ten.Rooms);
                n[12] = (System.DBNull.Value.Equals(ten.MoveinDate) ? (DateTime)GlobalShared.NULL_DATE : (DateTime)ten.MoveinDate);
                n[14] = (System.DBNull.Value.Equals(ten.LeaseExpirationDate) ? (DateTime)GlobalShared.NULL_DATE : (DateTime)ten.LeaseExpirationDate);
                //Adjust = tblTenant("Adjustment")
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Mapping Tenant record: " + ex.Message, false);
            }
        }

        public void mapBuilding(Building blds)
        {
            bld = blds.BuildingNumber;
            o = blds.OwnerNumber;
            BAD1 = blds.Address1;
            BAD2 = blds.Address2;
            Agt = (Information.IsDBNull(blds.AgentName) ? "" : blds.AgentName);
            blk = (Information.IsDBNull(blds.BlockNumber) ? "" : blds.BlockNumber);
            lot = (Information.IsDBNull(blds.LotNumber) ? "" : blds.LotNumber);
            sec = (Information.IsDBNull(blds.SectionNumber) ? "" : blds.SectionNumber);
            MDR = (Information.IsDBNull(blds.MultipleDwellingRegistrationNumber) ? "" : blds.MultipleDwellingRegistrationNumber);
            Super = (Information.IsDBNull(blds.SuperintendantName) ? "" : blds.SuperintendantName);
            Phone = (Information.IsDBNull(blds.SuperintendantPhone) ? "" : blds.SuperintendantPhone);
        }

        public void mapCharges(Charges chgs)
        {
            ct = chgs.BuildingNumber;
            chg = chgs.ChargeCode;
            ctype_Renamed = chgs.Type;
            camt = chgs.Amount;
            carr = chgs.Arrears;
            cpaid = chgs.Paid;
            sd = (System.DBNull.Value.Equals(chgs.StartingDate) ? new DateTime(1900, 1, 1) : chgs.StartingDate);
            ed = (System.DBNull.Value.Equals(chgs.EndingDate) ? new DateTime(2999, 12, 31) : chgs.EndingDate);
        }

        public void mapPayments(Payments pay)
        {
            PT = pay.BuildingNumber;
            pdate = pay.DateofEntry;
            pamt = pay.Amount;
            pcode = pay.SecurityCode;
        }

        public string cformat(decimal c, short n)
        {
            string functionReturnValue = null;

            string x = null;
            string f = null;

            f = new string('#', n - 4);
            f = f + "0.00";
            x = String.Format("{0:f}", c);
            try
            {
                return new string(' ', n - Strings.Len(x)) + x;
            }
            catch
            {
                return x;
            }
        }

        static bool InitStaticVariableHelper(Microsoft.VisualBasic.CompilerServices.StaticLocalInitFlag flag)
        {
            if (flag.State == 0)
            {
                flag.State = 2;
                return true;
            }
            else if (flag.State == 2)
            {
                throw new Microsoft.VisualBasic.CompilerServices.IncompleteInitialization();
            }
            else
            {
                return false;
            }
        }
    }

    public class ChargeRecords
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Charged { get; set; }
        public decimal Paid { get; set; }
        public decimal Arrears { get; set; }

        public ChargeRecords()
        {

        }
    }
}
