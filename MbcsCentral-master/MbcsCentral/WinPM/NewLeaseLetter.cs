//TODO: LeaseLetter Test Plan
//=====================
//TODO: 1) Building start and end.  {OK}
//TODO: 2) Type.  {OK}
//TODO: 3) Lease Expiration Date Range.    {OK}
//TODO: 4) Valid Separate Charge Codes.
//TODO: 5) Senior Citizen Charge.   {Works the same as the old program except that it may be wrong: the SC credit is being subtracted from the existing rent for item 9}
//TODO: 6) Guidelines Unknown.     {OK}
//TODO: 7) Use Minimum Increase.
//TODO: 8) Use Statement Address Option.
//TODO: 9) Pref Rent and Prompt pay checkboxes.
//TODO: 10) Must have status of printing.

using System;
using System.Drawing;
using System.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace MbcsCentral.WinPM
{
	public class LeaseLetter : IDisposable
	{
        Canvas document;
        System.Windows.Controls.Image template;
        System.Windows.Controls.Image reverse;
        DocumentLayoutFields[] flds;
        //DocumentLayoutFieldMaster[] fields = null;
        List<Field> fields = new List<Field>();


        MbcsUtils.Parameters parms = new MbcsUtils.Parameters(GlobalShared.appPath + WinpmGlobal.WINPM_CONFIG_FILE);

        long clientId = GlobalShared.user.Account.clientId;
        Report rpt;
        string filename = "";
        string reverseFile = "";
        string lastType = "";
        Tenants[] tenants;
        Building building;
        Owners owner;
        Types tenType;
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
        decimal ProposedRent2_1;
        decimal xr = 0;
        decimal rent = 0;
        decimal lra1;
		decimal lra2;
		decimal ri1;
		decimal nr1;
		decimal nr2;
        decimal nr2_1;
        decimal enr1;
        decimal enr2;
		decimal ri2;
        decimal ri2_1;
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

        // Properties
        private decimal m_minimumRent;
        private int m_minimumYears;
        private bool useMinimumRent;
        private bool m_duplexEnabled = false;
        private string[] codes;
        private string seniorCitizenCode;
        private decimal one;
        private decimal two;    
        private decimal two_1;
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
        double verticalOffset;
        double horizontalOffset;
        DateTime CurDate = DateTime.Now;

        public LeaseLetter(bool modify)
        {
            verticalOffset = -(35.0 / 1100.0);
            horizontalOffset = 0;

            canModify = modify;
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

            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting LeaseLetter.",false);
            try
            {
                Control ctrl = Control.load(clientId);
                x = DateAndTime.Today.ToString();
                TwoYear1 = (decimal)ctrl.PercentTwoYr1;
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
                object obj = null;
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
                        Tenants[] ten = Tenants.load(s.Substring(0, 3), s.Substring(3, 4).Trim(), clientId);
                        if (ten != null) {
                            printIt(ten[0]);
                        }
                        ten = null;
                    }
                } else {
                    xsql = "SELECT * FROM " + Tenants.TABLE + " WHERE [" + Tenants.SQL_COL_BUILDING_NUMBER + "] BETWEEN '" + bldStart + "' AND '" + bldEnd + "'";
                    xsql += " AND [" + Tenants.SQL_COL_LEASE_EXPIRATION_DATE + "] BETWEEN '" + expFrom.ToString() + "' AND '" + expTo.ToString() + "'";
                    if (TypeList != null && TypeList.Length > 0)
                    {
                        string s = " AND [" + Tenants.SQL_COL_TYPE + "] IN (";
                        bool first = true;
                        for (int ii = 0; ii < TypeList.Length; ii++)
                        {
                            if (!first)
                                s += ", ";
                            s += "'" + TypeList[ii] + "'";
                            first = false;
                        }
                        s += ")";
                        xsql += s;
                    }
                    else if (!type.Trim().Equals(""))
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
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.PrintLetters: " + ex.Message, false);

            }

        }

        private void printIt(Tenants ten)
        {
            int ic = 0;
            string tnam = ten.TenantName;
			nam1 = tnam;
			nam2 = "";
            bool riderPrinted = false;

            rpt = new Report();
            rpt.TopMargin = 0;
            rpt.BottomMargin = 0;
            rpt.LeftMargin = 0;
            try
            {
                document = null;
                document = new Canvas();
                reverse = null;
                reverse = new System.Windows.Controls.Image();
                tenType = Types.load(GlobalShared.user.Account.clientId, ten.Type);
                if (UseTypeLease)
                {
                    if (tenType != null && tenType.LeaseName != null && !tenType.LeaseName.Trim().Equals(""))
                    {
                        if (!RetrieveDocument(tenType.LeaseName))
                        {
                            RetrieveDocument(filename);
                        }
                        if (m_duplexEnabled)
                        {
                            if (tenType.LeaseReverse != null && !tenType.LeaseReverse.Trim().Equals(""))
                            {
                                DocumentLayoutList doc = DocumentLayoutList.loadByDocument(tenType.LeaseReverse);
                                string filepath = doc.DocumentLocation;
                                if (doc.DocumentLocation.StartsWith("*"))
                                {
                                    string path = GlobalShared.appPath;
                                    if (path.EndsWith("\\"))
                                        path = path.Substring(0, path.Length - 1);
                                    filepath = filepath.Replace("*", path + "\\Files");
                                }

                                rpt.Duplex = true;
                                reverse = Report.GetImage(filepath);
                                if (reverse == null || tenType.LeaseReverse.Trim().Equals(""))
                                {
                                    rpt.Duplex = false;
                                }
                            }
                            else
                                rpt.Duplex = false;
                        }

                    }
                        else
                        {
                            RetrieveDocument(filename);
                            if (m_duplexEnabled)
                            {
                                rpt.Duplex = true;
                                reverse = Report.GetImage(reverseFile);
                                if (reverse == null)
                                {
                                    rpt.Duplex = false;
                                }
                            }
                        }
                        lastType = ten.Type.ToLower().Trim();
                    //}
                }
                else
                {
                    RetrieveDocument(filename);

                    if (m_duplexEnabled)
                    {
                        reverse = Report.GetImage(reverseFile);
                        if (reverse == null)
                        {
                            rpt.Duplex = false;
                            /*
                            switch (MessageBox.Show("Duplex printing is enabled but the reverse-side image file cannot be loaded." + Constants.vbCrLf + "Print lease renewal letters anyway?", "", MessageBoxButtons.YesNo))
                            {
                                case System.Windows.Forms.DialogResult.Yes:
                                    m_duplexEnabled = false;
                                    break;
                                case System.Windows.Forms.DialogResult.No:
                                    return;
                                    break;
                            }
                            */
                        }
                        else
                            rpt.Duplex = true;
                    }

                }

                // New name resolution routine
                string[,] names = new string[3, 3];
                WinpmGlobal.resolveNames(ref names, ten);
                nam1 = Strings.Trim(names[0, 0]) + " " + Strings.Trim(names[0, 1]);
                nam2 = Strings.Trim(names[1, 0]) + " " + Strings.Trim(names[1, 1]);
                moveindate = DateAndTime.Now;
                oldLease = DateAndTime.DateSerial(2000, 1, 1);
                if (ten.LeaseStartDate > GlobalShared.NULL_DATE)
                {
                    oldLease = ten.LeaseStartDate;
                }

                moveindate = ten.MoveinDate;

                System.Windows.Controls.PrintDialog dlg = new System.Windows.Controls.PrintDialog();

                for (ic = 1; ic <= copies; ic++)
                {
                    PrintFields(ten);

                    if (m_duplexEnabled)
                    {
                        queue.DefaultPrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
                        queue.UserPrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
                    }

                    dlg.PrintQueue = queue;
                    // Lease Letter
                    rpt.AddImage(document);
                    // Lease Reverse
                    if (rpt != null && rpt.Duplex && reverse != null)
                    {
                        rpt.AddImage(reverse, true);
                    }
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "About to print lease: " + ten.BuildingTenantNumber,false);
                    rpt.PrintDocument(queue);
                    if (ic < copies)
                    {
                        document = null;
                        document = new Canvas();
                        Types type = Types.load(GlobalShared.user.Account.clientId, ten.Type);
                        if (UseTypeLease && type != null && !type.LeaseName.Trim().Equals(""))
                        {
                            if (!RetrieveDocument(type.LeaseName))
                            {
                                RetrieveDocument(filename);
                            }
                        }
                        else
                            RetrieveDocument(filename);

                    }
                    if (m_duplexEnabled)
                        queue.DefaultPrintTicket.Duplexing = Duplexing.OneSided;
                }
                riderPrinted = false;

                // Preferential Rent Rider
                queue.DefaultPrintTicket.Duplexing = Duplexing.OneSided;
                if (m_prefRent && (ProposedRent1 > 0 || ProposedRent2 > 0))
                {
                    for (ic = 1; ic <= copies; ic++)
                    {
                        prefRent.TenantName = tnam;
                        prefRent.Apartment = ten.Apartment;
                        prefRent.ProposedRent1 = ProposedRent1;
                        prefRent.ProposedRent2 = ProposedRent2;
                        prefRent.ProposedRent2_1 = ProposedRent2_1;
                        prefRent.LegalRent1 = curLglRent + ri1 + ria + lra1 + capImp;
                        prefRent.LegalRent2 = curLglRent + ri2 + ria + lra2 + capImp;
                        prefRent.Name1 = nam1;
                        prefRent.Name2 = nam2;
                        if ((newLease == null || newLease.Trim().Equals("")) && ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                        {
                            int mn1 = ten.LeaseExpirationDate.Month;
                            int dY1 = ten.LeaseExpirationDate.Day;
                            int yr1 = ten.LeaseExpirationDate.Year;
                            if (dY1 >= months[mn1])
                            {
                                dY1 = 1;
                                mn1 = mn1 + 1;
                            }
                            else
                            {
                                dY1 = dY1 + 1;
                            }
                            if (mn1 > 12)
                            {
                                mn1 = 1;
                                yr1 = yr1 + 1;
                            }
                            string x = mn1.ToString("00") + "  " + dY1.ToString("00") + "  " + yr1.ToString("0000");
                            newLease = x;
                        }
                        prefRent.printForm(newLease, owner.OwnerName, building.Address1, queue);
                    }
                    riderPrinted = true;
                }

                // Prompt Pay Rider
                if (m_promptPay && (ten.PromptPay1 > 0 || ten.PromptPay2 > 0))
                {
                    for (i = 1; i <= copies; i++)
                    {
                        bldAddr[0] = building.Address1;
                        bldAddr[1] = building.Address2;
                        newLease = NewLeaseDate(ten);
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
                    ten.ProposedRent2_1 = 0;    
                    ten.PromptPay1 = 0;
                    ten.PromptPay2 = 0;

                    ten.save();
                }
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.printIt: " + ex.Message, false);

            }

        }

        private string NewLeaseDate(Tenants ten)
        {
            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
            {
                int mn1 = ten.LeaseExpirationDate.Month;
                int dY1 = ten.LeaseExpirationDate.Day;
                int yr1 = ten.LeaseExpirationDate.Year;
                if (dY1 >= months[mn1])
                {
                    dY1 = 1;
                    mn1 = mn1 + 1;
                }
                else
                {
                    dY1 = dY1 + 1;
                }
                if (mn1 > 12)
                {
                    mn1 = 1;
                    yr1 = yr1 + 1;
                }
                string x = mn1.ToString("00") + "  " + dY1.ToString("00") + "  " + yr1.ToString("0000");
                return x;
            }
            return null;
        }

        private void PrintFields(Tenants ten)
        {
            Field docField = null;
            document.Children.Clear();

			//**********************************
			//* Field substitution subroutines *
			//**********************************
			int yr = 0;
			int dY = 0;
			int mn = 0;
			int yyear = 0;
			int ymonth = 0;
            int addrOption = 0;

            try
            {
                if (!bld.Equals(ten.BuildingNumber))
                    getbld(ten);

                if (!useStatementOptions)
                {
                    addrOption = 0;
                }
                else
                {
                    addrOption = retAdd;
                }
                switch (addrOption)
                {
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
                mn = ten.LeaseExpirationDate.Month;
                dY = ten.LeaseExpirationDate.Day;
                yr = ten.LeaseExpirationDate.Year;

                // RENT
                decimal rent = ten.Rent;
                decimal curRent = rent;

                /* Need to use DCLegal when appropriate
                 * 
                 * 
                */

                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Legal Rent", false);
                lglRent = ten.RentLegal;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "UseDC, DecontrolledLegal", false);
                if (tenType != null && tenType.UseDC != null && ten.DecontrolledLegal != null && tenType.UseDC && ten.DecontrolledLegal != 0)
                    lglRent = ten.DecontrolledLegal;
                curLglRent = lglRent;
                capImp = 0;
                if (lglRent <= 0)
                    lglRent = rent;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Proposed Rent", false);
                ProposedRent1 = ten.ProposedRent;
                ProposedRent2 = ten.ProposedRent2;
                ProposedRent2_1 = ten.ProposedRent2_1;
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Lease Expiration", false);
                if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                {
                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "Get History", false);
                    newStart = newLeaseStart(ten.LeaseExpirationDate);
                    ymonth = newStart.Month;
                    yyear = newStart.Year;
                    bool useDC = (bool)(tenType == null ? false : tenType.UseDC);
                    if (!BypassHistory)
                        GetHist(ten, ymonth, yyear, ref rent, ref lglRent, useDC);

                    //GlobalShared.Log.Log((int)LogClass.logType.Info, "Capital Improvements", false);
                    //*********************************
                    // Capital Improvement calculation
                    //*********************************

                    if (lglRent > 0)
                    {
                        // First test Legal Rents
                        capImp = curLglRent - lglRent;
                        curLglRent = lglRent;

                        // If no Legal Rent difference try current rent
                        if (capImp <= 0)
                            capImp = curRent - rent;
                    }

                    // Make sure that capImp is not negative
                    if (capImp < 0)
                    {
                        capImp = 0;
                    }
                }

                //GlobalShared.Log.Log((int)LogClass.logType.Info,"Lease Date", false);
                // LeaseDate
                leaseDate = (ten.LeaseExpirationDate > GlobalShared.NULL_DATE ? ten.LeaseExpirationDate : DateAndTime.Now);

                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Rent Increases", false);
                // ONE YEAR RENT INCREASE
                ri1 = NewRent(curLglRent, one, years, leaseDate, moveindate, increaseRent, minimumIncrease1);
                pri1 = (ProposedRent1 > 0 ? ProposedRent1 - rent : (rent * one) / 100);
                // TWO YEAR RENT INCREASE
                ri2 = NewRent(curLglRent, two, years, leaseDate, moveindate, increaseRent, minimumIncrease2);
                pri2 = (ProposedRent2 > 0 ? ProposedRent2 - rent : (rent * two) / 100);
                // TWO YEAR RENT INCREASE - 1st Year
                ri2_1 = NewRent(curLglRent, two_1, years, leaseDate, moveindate, increaseRent, minimumIncrease2);
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Additional Charges", false);
                // TOTAL ADDITIONAL CHARGES
                xr = 0;
                ac = 0;
                SC = 0;
                Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                if (chgs != null && chgs.Length > 0)
                {
                    foreach (Charges chg in chgs)
                    {
                        if (Array.IndexOf(codes, chg.ChargeCode) >= 0 && chg.Type == "2" && chg.Amount != 0)
                        {
                            xr = xr + chg.Amount;
                        }
                    }
                }
                chgs = null;
                if (seniorCitizenCode != null && seniorCitizenCode.Trim() != "")
                {
                    chgs = Charges.loadForSecurity(ten.SequenceNumber, seniorCitizenCode, DateTime.Today);
                    if (chgs != null && chgs.Length > 0)
                        SC = chgs[0].Amount;
                    chgs = null;
                }
                if (xr != 0)
                {
                    ac = xr;
                }
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "New and Effective Rent", false);

                // TOTAL NEW RENT 
                nr1 = curLglRent + ri1 + ria + lra1 + capImp;
                nr2 = curLglRent + ri2 + ria + lra2 + capImp;
                nr2_1 = curLglRent + ri2_1 + ria + capImp;

                // Final Effective Rent
                enr1 = (ProposedRent1 > 0 ? ProposedRent1 : nr1);
                enr2 = (ProposedRent2 > 0 ? ProposedRent2 : nr2);
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "About to iterated fields.",false);
                for (int f = 0; f < fields.Count; f++)
                {
                    docField = fields[f];

                    switch (docField.FieldName)
                    {
                        // Tenant Number
                        case "TenNum":
                            SetField(docField, "TenNum", ten.BuildingNumber + ten.TenantNumber);
                            break;

                        case "CurDate":
                            // CURRENT DATE

                            SetField(docField, "CurDate", CurDate.ToString("MM/dd/yyyy"));
                            break;

                        case "TenantName":
                            // NAME
                            SetField(docField, "TenantName", nam1);
                            break;

                        case "TenantName2":
                            if (!string.IsNullOrEmpty(nam2.Trim()))
                            {
                                SetField(docField, "TenantName2", nam2);
                            }
                            break;

                        case "BldAddr1Apt":
                            // ADDRESS 1 With Apartment
                            SetField(docField, "BldAddr1Apt", building.Address1.Trim() + "  Apt: " + ten.Apartment.Trim());
                            break;
                        // ADDRESS 1 Without Apartment
                        case "BldAddr1":
                            SetField(docField, "BldAddr1", building.Address1.Trim());
                            break;
                        case "BldAddr2":
                            // ADDRESS 2
                            SetField(docField, "BldAddr2", building.Address2.Trim());
                            break;
                        // Apartment
                        case "Apt":
                            SetField(docField, "Apt", ten.Apartment.Trim());
                            break;

                        // owner name
                        case "OwnName":
                            if (owner != null)
                                SetField(docField, "OwnName", oName);
                            break;
                        case "OwnAddr1":
                            if (owner != null)
                                SetField(docField, "OwnAddr1", oAddr1);
                            break;
                        case "OwnAddr2":
                            if (owner != null)
                                SetField(docField, "OwnAddr2", oAddr2);
                            break;

                        // LEASE EXPIRATION DATE
                        case "ExpDate":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                                SetField(docField, "ExpDate", ten.LeaseExpirationDate.ToShortDateString());
                            break;
                        case "ExpMonth":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                                SetField(docField, "ExpMonth", mn.ToString("00"));
                            break;
                        case "ExpDay":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                                SetField(docField, "ExpDay", dY.ToString("00"));
                            break;
                        case "ExpYear":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                                SetField(docField, "ExpYear", yr.ToString("0000"));
                            break;

                        // RENT
                        case "Rent":
                            SetField(docField, "Rent", rent.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "colB":
                            SetField(docField, "colB", curLglRent.ToString("##,##0.00").PadLeft(9));
                            break;

                        // ONE YEAR RENT INCREASE
                        case "colC1a":
                            SetField(docField, "colC1a", ri1.ToString("#,##0.00").PadLeft(9));
                            break;

                        // TWO YEAR RENT INCREASE
                        case "colC2a":
                            SetField(docField, "colC2a", ri2.ToString("#,##0.00").PadLeft(9));
                            break;

                        // Guidelines Taken?
                        case "colCchk":

                            //if (guidelinesUnknown)
                            SetField(docField, "colCchk", "**");
                            break;

                        // ONE YEAR PERCENT
                        case "colC1p":
                            string c = "";
                            if (minTaken)
                                c = "*****";
                            else
                                c = one.ToString("#0.000").PadLeft(5);
                            SetField(docField, "colC1p", c);
                            break;

                        // TWO YEAR PERCENT
                        case "colC2p":
                            if (minTaken)
                                c = "*****";
                            else
                                c = two.ToString("#0.000").PadLeft(5);
                            SetField(docField, "colC2p", c);
                            break;

                        case "colD1":
                            if (ria != 0)
                                SetField(docField, "colD1", ria.ToString("#,##0.00").PadLeft(8));
                            break;
                        case "colD2":
                            if (ria != 0)
                                SetField(docField, "colD2", xr.ToString("#,##0.00").PadLeft(8));
                            break;
                        // CAPITAL IMPROVEMENT INCREASE - COLUMN E
                        case "colE1":
                            if (capImp > 0)
                                SetField(docField, "colE1", capImp.ToString("#,##0.00").PadLeft(8));
                            break;
                        case "colE2":
                            if (capImp > 0)
                                SetField(docField, "colE2", capImp.ToString("#,##0.00").PadLeft(8));
                            break;

                        // Preferential Check Box at top
                        case "colFchk":
                            if (ProposedRent1 > 0 | ProposedRent2 > 0)
                                SetField(docField, "colFchk", "X");
                            break;

                        // TOTAL NEW RENT ONE YEAR
                        case "colF1":
                            SetField(docField, "colF1", nr1.ToString("##,##0.00").PadLeft(9));
                            break;

                        // TOTAL NEW RENT TWO YEARS
                        case "colF2":
                            SetField(docField, "colF2", nr2.ToString("##,##0.00").PadLeft(9));
                            break;

                        // ADDITIONAL INCREASE
                        case "AddInc":
                            for (i = 1; i <= 3; i++)
                            {
                                if ((DateTime)ten.GetType().GetProperty("FutureDate" + i.ToString("0")).GetValue(ten, null) > GlobalShared.NULL_DATE)
                                {
                                    xr = (decimal)ten.GetType().GetProperty("FutureRent" + i.ToString("0")).GetValue(ten, null);
                                    xr = xr - rent;
                                    if (xr > 0)
                                    {
                                        // TODO: account for additional line feeds
                                        SetField(docField, "AddInc", "Subject to additional $" + xr.ToString("#,##0.00") + " increase on " + ten.GetType().GetProperty("Future Date " + i.ToString("0")).GetValue(ten, null), i);
                                    }
                                    break;
                                }
                            }
                            // SEPARATE CHARGE MESSAGE
                            if (ac != 0)
                                SetField(docField, "AddInc", "Separate charge subject to adjustment by DHCR.", i);
                            break;


                        // NEW GUIDELINE RENTS
                        case "Guideline1":

                            //SetField(docField, "MsgArea", WinpmGlobal.LEASE_SPECIAL_MESSAGE); // NEW LEASE RENAWAL FIELD 06/19/2020
                            if (ProposedRent1 > 0 || ProposedRent2 > 0 || ProposedRent2_1 > 0)
                            {
                                SetField(docField, "Guideline1", ProposedRent2_1.ToString("##,##0.00").PadLeft(9));
                            }
                            else
                            {
                                SetField(docField, "Guideline1", nr2_1.ToString("##,##0.00").PadLeft(9));
                            }
                            break;
                        case "Guideline2":
                            if (ProposedRent1 > 0 || ProposedRent2 > 0 || ProposedRent2_1 > 0)
                            {
                                SetField(docField, "Guideline2", ProposedRent2.ToString("##,##0.00").PadLeft(9));
                            }
                            else
                            {
                                SetField(docField, "Guideline2", nr2.ToString("##,##0.00").PadLeft(9));
                            }
                            break;

                        // SECURITY
                        case "SecDep":
                            SetField(docField, "SecDep", ten.Security.ToString("#,##0.00").PadLeft(9));
                            break;

                        // ADD. SECURITY, ONE YEAR
                        case "Sec1":
                            xr = ((ProposedRent1 > 0 ? ProposedRent1 : curLglRent + ri1 + capImp) * (decimal)ten.SecMultiple - ten.Security);
                            if (xr < 0)
                                xr = 0;
                            SetField(docField, "Sec1", xr.ToString("##,##0.00").PadLeft(9), 0, System.Windows.Media.Brushes.Red, true);
                            break;

                        // ADD. SECURITY, TWO YEAR
                        case "Sec2":
                            xr = ((ProposedRent2 > 0 ? ProposedRent2 : curLglRent + ri2 + capImp) * (decimal)ten.SecMultiple - ten.Security);
                            if (xr < 0)
                                xr = 0;
                            SetField(docField, "Sec2", xr.ToString("##,##0.00").PadLeft(9), 0, System.Windows.Media.Brushes.Red, true);
                            break;

                        // INDIVIDUAL ADDITIONAL CHARGES
                        case "AddChgAmt":
                            if (ac != 0)
                                SetField(docField, "AddChgAmt", ac.ToString("#,##0.00").PadLeft(8));
                            break;
                        case "AddChgTot":
                                SetField(docField, "AddChgTot", ac.ToString("#,##0.00").PadLeft(8));
                            break;

                        // Preferred Rent 
                        case "prefRent1":
                            if (ProposedRent1 > 0 )
                                SetField(docField, "prefRent1", ProposedRent1.ToString("##,##0.00").PadLeft(9));
                                break;
                        case "prefRent2":
                            if (ProposedRent2 > 0)
                                SetField(docField, "prefRent2", ProposedRent2.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "prefRent2_1":
                            if (ProposedRent2_1 > 0)
                                SetField(docField, "prefRent2_1", ProposedRent2_1.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "ColFchk":
                            if (ProposedRent1 > 0 || ProposedRent2 > 0)
                                SetField(docField, "ColFchk", "X");
                            break;
                        case "prefYes":
                            if (ProposedRent1 > 0 || ProposedRent2 > 0)
                                if (m_prefRent)
                                    SetField(docField, "prefYes", "X");
                            break;
                        case "prefNo":
                            if (ProposedRent1 > 0 || ProposedRent2 > 0)
                                if (!m_prefRent)
                                    SetField(docField, "prefNo", "X");
                            break;

                        // New Rent (item 6)
                        case "NewChgs1":
                            SetField(docField, "NewChgs1", ac.ToString("#,##0.00").PadLeft(8));
                            break;
                        case "NewRent1":
                            SetField(docField, "NewRent1", enr1.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "NewTot1":
                            SetField(docField, "NewTot1", (enr1 + ac).ToString("##,##0.00").PadLeft(9));
                            break;
                        case "NewChgs2":
                            SetField(docField, "NewChgs2", ac.ToString("#,##0.00").PadLeft(8));
                            break;
                        case "NewRent2":
                            SetField(docField, "NewRent2", enr2.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "NewTot2":
                            SetField(docField, "NewTot2", (enr2 + ac).ToString("##,##0.00").PadLeft(9));
                            break;

                        // LEASE START DATE
                        case "NewLease":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                            {
                                string x = NewLeaseDate(ten);
                                SetField(docField, "NewLease", x);
                                newLease = x;
                            }
                            break;

                        // NEW LEASE EXPIRATION DATES
                        case "NewExp1":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                            {
                                int mn1 = ten.LeaseExpirationDate.Month;
                                int dY1 = ten.LeaseExpirationDate.Day;
                                int yr1 = ten.LeaseExpirationDate.Year;
                                // ONE YEAR
                                if (mn1 == 2 && dY1 == 29 && !DateTime.IsLeapYear(yr1 + 1))
                                    dY1 = 28;
                                string x = mn1.ToString("00") + "  " + dY1.ToString("00") + "  " + (yr1 + 1).ToString("0000");
                                SetField(docField, "NewExp1", x);
                            }
                            break;
                        case "NewExp2":
                            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
                            {
                                int mn1 = ten.LeaseExpirationDate.Month;
                                int dY1 = ten.LeaseExpirationDate.Day;
                                int yr1 = ten.LeaseExpirationDate.Year;
                                // TWO YEAR
                                if (mn1 == 2 && dY1 == 29 && !DateTime.IsLeapYear(yr1 + 2))
                                    dY1 = 28;
                                string x = mn1.ToString("00") + "  " + dY1.ToString("00") + "  " + (yr1 + 2).ToString("0000");
                                SetField(docField, "NewExp2", x);
                            }
                            break;

                        // Senior Citizen Rent
                        case "SCRent":
                            if (SC != 0)
                                SetField(docField, "SCRent", (rent - System.Math.Abs(SC) + capImp).ToString("##,##0.00").PadLeft(9));
                            break;

                        case "sprinkler":
                            SetField(docField, "sprinkler", "X");
                            break;

                        // Bottom Rent
                        case "FinalRent1":
                            SetField(docField, "FinalRent1", enr1.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "FinalAC1":
                            SetField(docField, "FinalAC1", ac.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "FinalTot1":
                            SetField(docField, "FinalTot1", (enr1 + ac).ToString("##,##0.00").PadLeft(9), 0, System.Windows.Media.Brushes.Red);
                            break;

                        case "FinalRent2":
                            SetField(docField, "FinalRent2", enr2.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "FinalAC2":
                            SetField(docField, "FinalAC2", ac.ToString("##,##0.00").PadLeft(9));
                            break;
                        case "FinalTot2":
                            SetField(docField, "FinalTot2", (enr2 + ac).ToString("##,##0.00").PadLeft(9), 0, System.Windows.Media.Brushes.Red);
                            break;

                        // Signature Lines 
                        case "SigLine1":
                            SetField(docField, "SigLine1", nam1.Trim() + " sign here X:", 0, System.Windows.Media.Brushes.Red, true);
                            break;
                        case "SigLine2":
                            if (!string.IsNullOrEmpty(nam2.Trim()))
                                SetField(docField, "SigLine2", nam2.Trim() + " sign here X:", 0, System.Windows.Media.Brushes.Red, true);
                            break;

                    }
                    // Low Rent Addition currently unused - 05/12/2010
                    // The column is now used for capital improvement increase
                    //
                    // LOW RENT ADDITION ONE YEAR
                    /*
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
                            SetField(docField, "colD1", xr.ToString("#,##0.00").PadLeft(8));
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
                            SetField(docField, "colD2", xr.ToString("#,##0.00").PadLeft(8));
                        }
                    }
                     */

                    //rpt.FontColor = System.Windows.Media.Colors.Black;
                    //rpt.FontBold = false;
                }
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.PrintFields: " + ex.Message, false);

            }

        }

        private void SetField(Field doc, string name, string value, int linefeeds = 0, System.Windows.Media.Brush fontcolor = null, bool fontbold = false)
        {
            try
            {
                if (doc != null)
                {
                    if (doc.Item.Class != null && !doc.Item.Class.Trim().Equals(""))
                    {
                        try
                        {
                            ColorFont.FontInfo thisFont = ColorFont.FontInfo.DeSerialize(doc.Item.Class);
                            ColorFont.FontInfo.ApplyFont(doc, thisFont);
                        }
                        catch { }
                    }
                    else
                    {

                        if (fontcolor != null)
                            doc.Foreground = System.Windows.Media.Brushes.Black;
                        else
                            doc.Foreground = fontcolor;
                        if (!fontbold)
                            doc.FontWeight = System.Windows.FontWeights.Normal;
                        else
                            doc.FontWeight = System.Windows.FontWeights.Bold;
                    }

                    doc.Content = value;
                    document.Children.Add(doc);
                    Canvas.SetLeft(doc, (double)doc.Item.Left + ((double)doc.Item.Left * horizontalOffset));
                    Canvas.SetTop(doc, (double)doc.Item.Top + ((double)doc.Item.Top * verticalOffset) + ((doc.FontSize * 2) * linefeeds));
                }
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.SetField (" + name + "): " + ex.Message, false);

            }

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
		
        private void GetHist(Tenants ten, int ymonth, int yyear, ref decimal rent, ref decimal legal, bool UseDC = false)
        {

            DateTime x1;
            DateTime x2;

            try
            {
                // Why is this here?
                //if (oldLease > new DateTime(2000, 1, 1))
                    //if (oldLease.Year == yyear - 1 && oldLease.Month == 8 && oldLease.Day > 1)
                    //    return;

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

                TenantsSnapShot snap = TenantsSnapShot.load(ten.SequenceNumber, x1.Month, x1.Year);
                if (snap != null)
                {
                    legal = (UseDC ? snap.DecontrolledLegal : snap.RentLegal );
                }
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.GetHist (" + ten.BuildingNumber + ten.TenantNumber + "): " + ex.Message, false);

            }

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
                d2 = moveindate.AddYears(years);
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
 
        private bool RetrieveDocument(string name, bool IsPath = false)
        {

            document.Children.Clear();
            document.Width = 850 - 30;
            document.Height = 1100 - 35;
            DocumentLayoutList doc = DocumentLayoutList.loadByDocument(name);
            if (doc != null)
            {
                ImageBrush ib = new ImageBrush();
                string filepath = doc.DocumentLocation;
                if (doc.DocumentLocation.StartsWith("*"))
                {
                    string path = GlobalShared.appPath;
                    if (path.EndsWith("\\"))
                        path = path.Substring(0, path.Length - 1);
                    filepath = filepath.Replace("*", path + "\\Files");
                }
                try
                {
                    ib.ImageSource = new BitmapImage(new System.Uri(filepath));
                    ib.Stretch = Stretch.Uniform;
                    document.Background = ib;

                    flds = DocumentLayoutFields.loadByDocument(doc.ID);
                    if (flds != null && flds.Length > 0)
                    {
                        fields.Clear();
                        DocumentLayoutFieldMaster master = null;
                        foreach (DocumentLayoutFields fld in flds)
                        {
                            Field field = new Field(null, null, System.Windows.Media.Brushes.Transparent, (double)10);
                            field.Item = fld;
                            master = DocumentLayoutFieldMaster.load(fld.FieldId);
                            if (master != null)
                                field.FieldName = master.FieldName;
                            fields.Add(field);
                        }
                    }
                    else
                    {
                        MessageBoxCustom.Show("Unable to retrieve document fields.");
                        return false;
                    }
                }
                catch (SystemException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not load Document: " + filepath, true);
                    return false;
                }
            }
            else
                return false;
            return true;
        }

        // Properties
        public string BldStart
        {
            set{bldStart=value;}
        }
        public string BldEnd
        {
            set { bldEnd=value;}
        }
        public DateTime LetterDate
        {
            set { CurDate = value; }
        }
        public string Type
        {
            set { type = value; }
        }

        public string[] TypeList
        { get; set; }
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
        public decimal TwoYear1
        {
            set { two_1 = value; }
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
        public int Copies
        {
            set { this.copies = value; }
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
        public string LeaseDocument
        {
            set {filename = value;}
        }
        public string ReverseDocument
        {
            set { reverseFile = value;}
        }
        public bool UseTypeLease { get; set; }

        public bool BypassHistory { get; set; }
	}
}
