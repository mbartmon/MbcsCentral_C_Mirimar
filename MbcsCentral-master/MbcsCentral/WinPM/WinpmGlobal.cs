 using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace MbcsCentral.WinPM
{
	public static class WinpmGlobal
	{
        // Constants
        public const string WINPM_CONFIG_FILE = "WinPM\\MbcsWinpm.config";
        public const string LEASE_REVERSE_SIDE = "DuplexFile";
        public const string STANDARD_LEASE_LETTER = "StandardLease";
        public const string SP_DELETE_BUILDING = "DeletePmBuilding";
        public const string WINGL_LOCATION_TAG = "[Internal]";
        public const string LOCKBOX_USERNAME = "LockBox";
        public const string EMAIL_BATCH_LIMIT = "MbcsCentral\\WinPM\\Email\\BatchLimit";
        public const string EMAIL_BATCH_LIMIT_COUNT = "Count";
        public const string EMAIL_BATCH_LIMIT_DELAY = "Delay";
        // 2021 Lease Renewal Changes
        //public const string LEASE_SPECIAL_MESSAGE = " ** 2 Year Lease:  1st Year - No Rent Increase. 2nd Year - 1% Rent Increase";
        //public const string PREFERENTIAL_SPECIAL_MESSAGE = " ** 2 Year Lease:  1st Year - No Rent Increase. 2nd Year - 1% Rent Increase";
        public const string LEASE_SPECIAL_MESSAGE = " ** 2 Year Lease - Year 1 - 2.75% $_______________________  /  Year 2 - 3.2% Above Year 1 Rent";
        public const string PREFERENTIAL_SPECIAL_MESSAGE = " ** 1 Year Lease:  No Rent Increase for 1st Six Months";


        public static string[] relationships = {
			"Aunt",
			"Brother",
			"Cousin",
			"Daughter",
			"Father",
			"Friend",
			"GrandDaughter",
			"GrandFather",
			"GrandMother",
			"GrandSon",
			"InLaw",
			"Mother",
			"Nephew",
			"Niece",
			"Other",
			"Significant Other",
			"Self",
			"Sister",
			"Son",
            "Spouse",
			"StepDaughter",
			"StepSon",
			"Uncle"
		};

        public static bool loaded = false;
        public static bool stflag = false;
        public static Control control = null;

        //**********************
        // Email Parameters
        //**********************

        public enum EmailTypes
        {
            LateNotice,
            TenantDirect
        }
        public static string[] EmailTypeFilenames = { "LateNotice", "TenantDirect" };

        // Late Notice Email
        public enum NoticeInsertionFields
        {
            OwnerName,
            Apt,
            TenantName,
            AmountDue,
            TenantNumber,
            PaymentDate
        };
        public static string[] NoticeInsertionFieldNames = { "[OwnerName]", "[Apt]", "[TenantName]", "[AmountDue]", "[TenantNumber]", "[PaymentDate]" };


		public static string[] relationshipDomain {
			get { return relationships; }
		}

        public static void GetConfig()
        {
            if (GlobalShared.user != null)
            {
                control =  Control.load( (int)GlobalShared.user.Account.clientId);
                stflag = control.SecurityTracker;
                loaded = true;
            }
        }

		/// <summary>
		/// Retrieves all tenants and places in a System.Windows.Controls.ListBox
		/// 
		/// Optionally sorts the list
		/// </summary>
		/// <param name="lst">ListBox to populate</param>
		/// <param name="orderBy">Order list by (see remarks)</param>
		/// <remarks>orderBy:
		/// (0) Tenant name
		/// (1) Building/Tenant numbers
		/// (2) Building/Apartment</remarks>
		public static void getTenants(ref System.Windows.Controls.ListBox lst, short orderBy = 0)
		{
			Tenants[] tens = null;
			tens = Tenants.load(orderBy);
			lst.Items.Clear();
			if ((tens != null)) {
				foreach (Tenants ten in tens) {
					lst.Items.Add(new ListItem(ten.TenantName + Strings.Space(31 - ten.TenantName.Length) + ten.BuildingNumber + ten.TenantNumber + Strings.Space(5 - ten.TenantNumber.Length) + ten.Apartment, ten.SequenceNumber));
				}
			}

		}
    
        public static string[] ParseBuildingAddress(string address)
        {
            string[] s = { "", "", "", "" };
            int i = address.IndexOf(',');
            if (i < 0)
            {
                s[0] = address;
                return s;
            }
            s[0] = address.Substring(0, i );
            address = address.Substring(i + 1).Trim();
            i = address.IndexOf(' ');
            if (i < 0)
            {
                s[1] = address;
                return s;
            }
            s[1] = address.Substring(0, i );
            s[2] = address.Substring(i + 1);
            return s;
        }

		public static void Charges_get(int seqnum, ref decimal chgCurrent, ref decimal chgArrears, ref decimal chgPaid, ref decimal chgCurOne, ref decimal chgArrOne, ref decimal chgPaidOne, ref decimal chgCurRec, ref decimal chgArrRec, ref decimal chgPaidRec)
		{
			chgCurrent = 0;
			chgArrears = 0;
			chgPaid = 0;
			chgCurOne = 0;
			chgArrOne = 0;
			chgPaidOne = 0;
			chgCurRec = 0;
			chgArrRec = 0;
			chgPaidRec = 0;
			Charges[] chgs = Charges.loadBySeqNum(seqnum);
			if ((chgs != null)) {
				foreach (Charges chg in chgs) {
					if (Information.IsDBNull(chg.StartingDate) || chg.StartingDate <= DateAndTime.Now) {
						chgCurrent = chgCurrent + chg.Amount;
						chgArrears = chgArrears + chg.Arrears;
						chgPaid = chgPaid + chg.Paid;
						switch (chg.Type) {
							case "1":
								// One Time
								chgCurOne = chgCurOne + chg.Amount;
								chgArrOne = chgArrOne + chg.Arrears;
								chgPaidOne = chgPaidOne + chg.Paid;
								break;
							case "2":
								// Recurring
								chgCurRec = chgCurRec + chg.Amount;
								chgArrRec = chgArrRec + chg.Arrears;
								chgPaidRec = chgPaidRec + chg.Paid;
								break;
						}
					}
				}
			}
			chgs = null;
		}
     
        public static void HistChgUpdate(Charges tbl,  bool openF,  bool deleteR,  System.DateTime dte)
	    {
		    object xsql;

		    short errorcount;
		    Tenants tblTen;

		    errorcount = 0;
            Histmast tblHistMast = Histmast.load( tbl.SequenceNumber );
            if (tblHistMast == null)
            {
                tblTen = Tenants.load( (int)tbl.SequenceNumber );
                if (tblTen == null || !Create_HistMast( tblTen ))
                {
                    GlobalShared.Log.Log( (int)LogClass.logType.ErrorCondition, "Cannot create History Master Record from charge record <\r\n\n" + tbl.BuildingNumber + tbl.TenantNumber + "><" + tbl.SequenceNumber.ToString() + ">" ,false );
                    return;
                }
            }
	 	    HistChg tblHistChg = HistChg.loadByChargeDate(tbl.ChargeNumber,dte);
		    // delete record
		    if (deleteR) {
			    if (tblHistChg != null) {
				    tblHistChg.delete(tblHistChg.HistChgId.ToString());
			    }
			    return;
		    }
            if (tblHistChg == null)
            {
                tblHistChg = new HistChg();

                tblHistChg.TenantNumber = tbl.TenantNumber;
                tblHistChg.BuildingNumber = tbl.BuildingNumber;
                tblHistChg.SequenceNumber = tbl.SequenceNumber;
                tblHistChg.ChargeNumber = tbl.ChargeNumber;
            }
		    tblHistChg.Code = tbl.ChargeCode;
		    tblHistChg.ChargeDate = dte;
		    tblHistChg.Amount = tbl.Amount;
		    tblHistChg.Arrears = tbl.Arrears;
		    tblHistChg.save();
	    }

        public static bool Create_HistMast(Tenants tbl)
        {
            Histmast histMast = new Histmast();
            histMast.BuildingNumber = tbl.BuildingNumber;
            histMast.TenantNumber = tbl.TenantNumber;
            histMast.Apartment = tbl.Apartment;
            histMast.TenantName = tbl.TenantName;
            histMast.SequenceNumber = tbl.SequenceNumber;
            if (histMast.save())
                return true;
            else
                return false;
        }

        public static void calc_Charges(long clientId, DateTime date, frmEOM frm = null)
        {
            double cr = 0;
            if (frm != null)
            {
                frm.progressMsg = "Process 3 of 3";
                frm.Progress = 0;
            }
            Tenants[] tblTenant = Tenants.loadForStatements("000", "ZZZ",clientId);
            if (frm != null)
                frm.prgProgress.Maximum = tblTenant.Count();
            foreach (Tenants ten in tblTenant)
            {
                Charges chg = Charges.loadTotal(ten.SequenceNumber, date.ToString("yyyyMMdd"));
                if (chg != null)
                {
                    ten.TotalCharges = chg.Total;
                }
                if (frm != null)
                {
                    cr = cr + 1;
                    if (cr < frm.prgProgress.Maximum - frm.prgProgress.Maximum / 10)
                        frm.Progress = cr;

                }
            }
            if (frm != null)
                frm.Progress = frm.prgProgress.Maximum;
        }

	    public static void SecTrans(Payments tblPayments)
	    {

		    string x;
		    string xsql;
            string code = "S";
		    int stype;

            Control[] ctl = Control.load();
            if (ctl != null && ctl.Length > 0)
                code = ctl[0].SecurityCode;
            
		    STTrans[] trans = STTrans.loadBySequence(tblPayments.SequenceNumber);
		    if (trans == null || trans.Length == 0) {                
			    x = "I";
		    } else {
			    x = code;
		    }
		    xsql = "SELECT [Key] FROM STCodes WHERE [Code] = '" + x + "'";
		    STCodes tblSTCodes = STCodes.load(x);
		    if (tblSTCodes != null) {
			    stype = tblSTCodes.Key;
		    } else {
			    if (x == "I") {
				    stype = 9;
			    } else {
				    stype = 15;
			    }
		    }
		    STTrans tblSTTrans = new STTrans();
		    tblSTTrans.BuildingNumber = tblPayments.BuildingNumber;
		    tblSTTrans.TenantNumber = tblPayments.TenantNumber.Substring(0, 3);
		    tblSTTrans.SequenceNumber = tblPayments.SequenceNumber;
		    tblSTTrans.Amount = tblPayments.Amount;
		    tblSTTrans.Date = tblPayments.DateofEntry;
		    tblSTTrans.TransType = stype;
            tblSTTrans.ClientId = GlobalShared.user.Account.clientId;
            tblSTTrans.save();

		    STTenants tblSTTenants = STTenants.loadBySequence(tblPayments.SequenceNumber);
		    if (tblSTTenants != null) {
			    tblSTTenants.Amount += tblPayments.Amount;
			    tblSTTenants.save();
		    } else {
			    Tenants tblTemp = Tenants.load((int)tblPayments.SequenceNumber);
                if (tblTemp == null)
                    return;
			    tblSTTenants = new STTenants();
			    tblSTTenants.BuildingNumber = tblTemp.BuildingNumber;
			    tblSTTenants.TenantNumber = tblTemp.TenantNumber.Substring(0, 3);
			    tblSTTenants.SequenceNumber = tblTemp.SequenceNumber;
			    tblSTTenants.TenantName = tblTemp.TenantName;
			    tblSTTenants.Apartment = tblTemp.Apartment;
			    tblSTTenants.SocialSecurityOne = tblTemp.SSNumber1;
			    tblSTTenants.SocialSecurityTwo = tblTemp.SSNumber2;
			    tblSTTenants.Amount = tblPayments.Amount;
                tblSTTenants.ClientId = GlobalShared.user.Account.clientId;
			    tblSTTenants.save();
                tblTemp = null;
		    }
	    }

        public static ArrayList ApplyRentPayments(Tenants[] tensPay, bool lockBox = false)
        {
            ArrayList pays = new ArrayList();
            if (tensPay == null || tensPay.Length == 0)
                return pays;
            decimal charge;
            decimal Paid;
            decimal paidx, x;
            int j, i;

            frmPaymentEntry frm = new frmPaymentEntry();
            PaymentEntries pay;
            PaymentEntries[] payments;

            foreach (Tenants ten in tensPay)
            {
                if (getTenant(ten))
                    pays.Add(ten);
                /*
                if (ten.Post && ten.Payment != 0)
                {
                    pays = new ArrayList();
                    pay = new PaymentEntries();
                    pay.ChargeNumber = ten.SequenceNumber;
                    pay.Due = ten.Rent + ten.Arrears;
                    pay.Paid = ten.Payment;
                    pay.Code = "=";
                    pays.Add(pay);
                    Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                    i = 0;
                    if (chgs != null && chgs.Length > 0)
                    {
                        foreach (Charges chg in chgs)
                        {
                            if (chg.StartingDate <= System.DateTime.Now)
                            {
                                if (System.DateTime.Now < chg.EndingDate && chg.Amount + chg.Arrears - chg.Paid != 0)
                                {
                                    charge = chg.Amount + chg.Arrears - chg.Paid;
                                    if (charge != 0)
                                    {
                                        pay = new PaymentEntries();
                                        i = i + 1;
                                        pay.Due = charge;
                                        pay.ChargeNumber = chg.ChargeNumber;
                                        pay.Code = chg.ChargeCode;
                                        ((PaymentEntries)pays[0]).Due += chg.Paid;
                                        pays.Add(pay);
                                    }
                                }
                            }
                        }
                    }
                    payments = (PaymentEntries[])pays.ToArray(typeof(PaymentEntries));

                    Paid = ten.Payment;
                    paidx = Paid;
                    for (j = 0; j <= i; j++)
                    {
                        if (payments[j].Due < 0)
                        {
                            payments[0].Amount += payments[i].Due;
                        }
                    }
                    j = 0;
                    while (j <= i)
                    {
                        if (payments[j].Due <= paidx)
                        {
                            payments[j].Amount += payments[j].Due;
                        }
                        else
                        {
                            payments[j].Amount = payments[j].Amount + paidx;
                        }
                        paidx = paidx - payments[j].Amount;
                        j = j + 1;
                    }
                    if (paidx != 0)
                    {
                        for (j = i; j >= 0; j += -1)
                        {
                            if (payments[j].Due - payments[j].Amount <= 0)
                            {
                                break;
                            }
                            x = payments[j].Due - payments[j].Amount;
                            if (x >= paidx)
                            {
                                payments[j].Amount = payments[j].Amount + paidx;
                                paidx = 0;
                                break;
                            }
                            else
                            {
                                payments[j].Amount += x;
                                paidx = paidx - x;
                            }
                        }
                        if (paidx != 0)
                        {
                            payments[0].Amount += paidx;
                        }
                    }

                    getTenant(payments);
                    //frm.PayentApply(ten.DatePaid.ToString(), false, payments, lockBox);
                    pf = true;
                   
                }
                */
            }
            frm = null;
            return pays;
        }

        public static Payments GetLastPaid(int tenNumber)
        {
            Control ctrl = Control.load(GlobalShared.user.Account.clientId);

            Payments[] pays = Payments.loadBySequence(tenNumber);
            if (pays != null && pays.Length > 0)
            {
                foreach(Payments pay in pays)
                    if (pay.SecurityCode != ctrl.SecurityCode)
                        return pay;
            }
            return null;
           
        }
         public static bool getTenant(Tenants tenant)
        {
            bool OverBalance = false;
            decimal chgBalance = 0;
            decimal chgPaid = 0;
            decimal Rent = 0;
            decimal tenantPaid = 0;
            ArrayList entries = new ArrayList();
            ArrayList pays = new ArrayList();
            Payments[] PaidAmounts;
            PaymentEntries pay = new PaymentEntries();
            if (tenant != null)
            {
               PaidAmounts = Payments.loadBySequence(tenant.SequenceNumber);
                Types tblType = Types.load(1, "=");
                if (tblType != null)
                {
                    if (tblType.BillingName != null && tblType.BillingName.Trim().Length > 0)
                        pay.Description = tblType.BillingName;
                    else
                        pay.Description = "RENT";
                }
                else
                    pay.Description = "RENT";
                // Find rent payments
                if (PaidAmounts != null && PaidAmounts.Length > 0)
                    foreach (Payments paidamount in PaidAmounts)
                        tenantPaid += paidamount.Amount;
                Rent = tenant.Rent;
                // Find future rent if chkNotSystemDate is checked.
                FutureRents[] rents = FutureRents.loadBySequence(tenant.SequenceNumber);
                if (rents != null && rents.Length > 0)
                {
                    foreach (FutureRents rent in rents)
                    {
                        if (tenant.DatePaid >= rent.FutureDate.Date && rent.FutureAmount > 0)
                        {
                            Rent = rent.FutureAmount;
                            break;
                        }
                    }
                    rents = null;
                }
                decimal balance = Rent + tenant.Arrears - tenantPaid; //tenant.Paid;
                pay.Code = "=";
                pay.Due = balance;
                pay.Amount = tenant.Payment;
                pay.ChargeNumber = tenant.SequenceNumber;
                pays.Add(pay);

                Charges[] chgs = Charges.loadBySeqNum(tenant.SequenceNumber);
                ChargeCodes cd;
                if (chgs != null && chgs.Length > 0)
                {
                    decimal chargeAmount = 0;
                    foreach (Charges chg in chgs)
                    {
                        if (chg.StartingDate == null || chg.StartingDate <= tenant.DatePaid)
                        {

                            pay = new PaymentEntries();
                            pay.ChargeNumber = (int)chg.ChargeNumber;
                            pay.Code = chg.ChargeCode;
                            chargeAmount = chg.Amount;
                            // Find future charge if chkNotSystemDate is checked.
                            if (chg.FutureDate != null && chg.FutureDate != GlobalShared.NULL_DATE && tenant.DatePaid >= chg.FutureDate)
                                chargeAmount = chg.FutureAmount;
                            cd = ChargeCodes.load(chg.ChargeCode, 1);
                            if (cd != null)
                                pay.Description = cd.ChargeDescription;
                            else
                                pay.Description = "*";
                            if (chg.OnBill)
                                pay.Description += "[" + chg.Comment + "]";
                            pay.Due = chargeAmount + chg.Arrears - chg.Paid;
                            pay.Amount = pay.Due;
                            chgBalance += pay.Due;
                            // balance += pay.Due;
                            pays.Add(pay);
                            // tenantPaid -= chg.Paid;
                            chgPaid -= chg.Paid;

                        }
                    }
                    ((PaymentEntries)pays[0]).Due = Rent + tenant.Arrears - tenantPaid - chgPaid;
                }
                balance = Rent + tenant.Arrears - tenantPaid + chgBalance - chgPaid;
                if (tenant.Payment > balance)
                {
                    OverBalance = true;
                    tenant.Paid = balance;
                }
                PaymentEntries[] payments = (PaymentEntries[])pays.ToArray(typeof(PaymentEntries));
                
                payent_alloc(tenant.Payment,tenant.DatePaid.ToShortDateString(), payments);
            }
            return OverBalance;
        }

        public static void payent_alloc(decimal Paid, string date, PaymentEntries[] payments)
        {

            decimal paidx = 0;
            decimal x;
            int j = 0;
            bool found = false;
            if (payments == null || payments.Length == 0)
                return;


            foreach (PaymentEntries pay in payments)
            {
                pay.Paid = 0;
            }
            j = 0;
            paidx = Paid;
            {
                found = false;
                for (j = 0; j < payments.Length; j++)
                {
                    if (Paid == payments[j].Due)
                    {
                        payments[j].Paid = Paid;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    paidx = Paid;
                    for (j = 0; j < payments.Length; j++)
                    {
                        if (payments[j].Due < 0)
                        {
                            payments[0].Paid += payments[j].Due;
                        }
                    }
                    j = 0;
                    while (j < payments.Length)
                    {
                        if (payments[j].Due < paidx)
                        {
                            payments[j].Paid += payments[j].Due;
                        }
                        else
                        {
                            payments[j].Paid += paidx;
                        }
                        paidx -= payments[j].Paid;
                        j = j + 1;
                    }
                    if (paidx != 0)
                    {
                        for (j = payments.Length - 1; j >= 0; j--)
                        {
                            if (payments[j].Due - payments[j].Paid <= 0)
                                break;
                            x = payments[j].Due - payments[j].Paid;
                            if (x >= paidx)
                            {
                                payments[j].Paid += paidx;
                                paidx = 0;
                                break;
                            }
                            else
                            {
                                payments[j].Paid += x;
                                paidx = paidx - x;
                            }
                        }
                        if (paidx != 0)
                        {
                            payments[0].Paid += paidx;
                        }
                    }
                }
            }
            frmPaymentEntry frm = new frmPaymentEntry();
            frm.PayentApply(date, false, payments, true);
        }


        public static int LockBoxCheckDigit(string scanline)
        {
            ArrayList list = new ArrayList();
            int o = 0;
            int s = 0;
            int m = 0;
            string c = "";

            bool x2 = true;

            for (int i = scanline.Length - 1; i >= 0 ; i--)
            {
                o = 0;

                int.TryParse(scanline.Substring(i, 1), out o);
                if (x2)
                    m = 2 * o;
                else
                    m = o;
                 if (m > 9)
                {
                    c = m.ToString("00");
                    m = int.Parse(c.Substring(0, 1)) + int.Parse(c.Substring(1, 1));
                }
                s += m;
                x2 = !x2;
            }
            if (s > 9)
            {
                int r = s % 10;
                c = s.ToString("000");
                s = int.Parse(c.Substring(2, 1));
                if (s == 0)
                    s = 10;
            }
            return 10 - s;

        }

        public static string reverse_names(string s)
        {

            short j = 0;
            short i = 0;
            short p = 0;
            string x = null;

            x = s;
            for (i = 0; i < s.Length; i++)
            {
                if (s.Substring(i, 1) == ",")
                {
                    x = s.Substring(i + 1).Trim();
                    x = x + " " + s.Substring(0, i);
                    break;
                }
            }
            return x;
        }

        public static void resolveNames(ref string[,] names, Tenants ten)
        {
            int z2 = -1;
            int z1 = -1;
            int z = -1;
            string nam;
            int i;
            int y;
            int xx;
            string nam2l;
            string nam1l;
            string nam2f;
            string nam1f;
            string nam2;
            string nam1;
            nam1 = " ";
            nam2 = " ";
            nam1f = " ";
            nam2f = " ";
            nam1l = " ";
            nam2l = " ";
            if (ten.AlternateBillingAddressFlag)
            {
                nam1 = ten.AltBillName;
                xx = nam1.IndexOf("&");
                if (xx > -1)
                    nam1 = nam1.Substring(0, xx);
                nam1 = nam1.TrimEnd();
                if (nam1.ToUpper().EndsWith("JR."))
                {
                    y = nam1.IndexOf(",");
                    if (y > 0)
                        nam1 = nam1.Substring(0, y - 1) + nam1.Substring(y + 1);
                    xx = nam1.Length - 4;
                }
                else
                {
                    xx = nam1.Length;
                }
                for (i = xx - 1; i >= 0; i--)
                {
                    if (nam1.Substring(i, 1) == " ")
                        break; // TODO: might not be correct. Was : Exit For
                }
                if (i < 0)
                    nam1f = nam1;
                else
                {
                    nam1l = nam1.Substring(i + 1);
                    nam1f = nam1.Substring(0, i);
                }

                nam2 = ten.AltBillAdd1;
                nam2 = nam2.TrimEnd();
                if (nam2.Length > 0)
                {
                    if (nam2.ToUpper().EndsWith("JR."))
                    {
                        y = nam2.IndexOf(",");
                        if (y > -1)
                            nam2 = nam2.Substring(0, y) + nam2.Substring( y + 1);
                        xx = nam2.Length - 4;
                    }
                    else
                    {
                        xx = nam2.Length;
                    }
                    for (i = xx - 1; i >= 0; i--)
                    {
                        if (nam2.Substring( i, 1) == " ")
                            break; // TODO: might not be correct. Was : Exit For
                    }
                    if (i > 0)
                    {
                        nam2l = nam2.Substring(i + 1);
                        nam2f = nam2.Substring(0, i);
                    }
                }
            }
            else
            {
                nam = ten.TenantName;
                z = nam.IndexOf( "&");
                if (z == -1)
                {
                    z = nam.ToUpper().IndexOf( " AND ");
                    if (z != -1)
                        z1 = z + 5;
                }
                else
                {
                    z1 = z + 1;
                }
                if (z != -1)
                {
                    nam1 = nam;
                    z2 = nam.IndexOf(",");
                    if (z2 > -1)
                    {
                        nam1l = nam.Substring( z2 + 1);
                        if (nam1.StartsWith( " "))
                            nam1 = nam1.Substring(1);
                        nam1l = nam.Substring(0, z2 );
                        if (z - z2 > 1)
                            nam1f = nam.Substring(z2 + 1, z - z2 - 1);
                        else
                            nam1.Substring(z2);
                        nam2l = nam.Substring(0, z2 );
                        nam2f = nam.Substring( z1);
                    }
                }
                else
                {
                    nam1 = ten.TenantName;
                    i = nam1.IndexOf( ",");
                    if (i > 0)
                    {
                        nam1l = nam1.Substring(0, i);
                        nam1f = nam1.Substring( i + 1).TrimStart();
                    }
                    else
                    {
                        for (i = nam1.Length - 1; i >= 0; i--)
                        {
                            if (nam1.Substring(i, 1) == " ")
                                break; // TODO: might not be correct. Was : Exit For
                        }
                        nam1l = nam1.Substring( i + 1);
                        if (i >= 0)
                            nam1f = nam1.Substring(0, i);
                    }
                }
            }

            nam1f = nam1f.TrimEnd();
            nam2f = nam2f.TrimEnd();

            names[0, 0] = nam1f;
            names[0, 1] = nam1l;
            names[1, 0] = nam2f;
            names[1, 1] = nam2l;

        }

        public static void EditLastViewedTenant( string parm)
        {
            string s = parm.Substring(parm.IndexOf("<")).Replace('<', ' ').Replace('>', ' ');
            int id ;
            if (int.TryParse(s.Trim(), out id))
            {
                frmTenant frm = new frmTenant() {ThisApp = "pm_tenants", sequenceNumber = id, Add = false};
                frm.Show();
            }
        }
        public static void GetLastTenantsList()
        {
            for (int i = 0; i < GlobalShared.TenantNumberList.Length; i++)
            {
                GlobalShared.TenantNumberList[i] = (string)Main.GetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\LastTenantsList\\", "pm_tenant" + i.ToString(), " ");
            }
        }

        public static void SaveLastTenantsList()
        {
            for (int i = 0; i < GlobalShared.TenantNumberList.Length; i++)
            {
                Main.SetRegistryEntry(MbcsCentral.GlobalShared.APPNAME + "\\WinPM\\LastTenantsList\\", "pm_tenant" + i.ToString(), (GlobalShared.TenantNumberList[i].Trim().Equals("") ? "." : GlobalShared.TenantNumberList[i]), (int)MbcsCentral.Registry.DataTypes.String);
            }
        }

        public static void AddToLastTenantsList(string bld, string ten, string apt, long seq)
        {
            string t = string.Format("{0}{1}[APT: {2}]<{3:######}>", bld, ten, apt, seq);
            bool inserted = false;
            for (int i = 0; i < GlobalShared.TenantNumberList.Length; i++)
            {
                if (GlobalShared.TenantNumberList[i].Trim().Equals(t))
                    return;
                if (GlobalShared.TenantNumberList[i].Trim().Equals("") )
                {
                    GlobalShared.TenantNumberList[i] = t;
                    inserted = true;
                    break;
                }
            }
            if (!inserted)
            {
                for (int i = 0; i < 3; i++)
                    GlobalShared.TenantNumberList[i] = GlobalShared.TenantNumberList[i + 1];
                GlobalShared.TenantNumberList[GlobalShared.TenantNumberList.Length - 1] = t;
            }
        }

        public static void clearDHCR(DateTime date)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Tenants[] tenants = Tenants.load();
                foreach (Tenants tenant in tenants)
                {
                    if (tenant.dhcrDateOne.Date <= date.Date)
                        tenant.ClearDHCRFields(0);
                    if (tenant.dhcrDateTwo.Date <= date.Date)
                        tenant.ClearDHCRFields(1);
                }
            }).Start();
        }



        /*  Seemingly unused in original.  Probably used to originally populate DHCR address fields.
 
        ' For DHCR Address of Record conversions
        
            Enum NYCounties
                bronx = 60
                kings = 61
                manhattan = 62
                queens = 63
                richmond = 64
            End Enum   

    Public Function getStreetNames() As Dictionary
            Dim streetNames As New Dictionary
    
            streetNames.Add key:="alley", Item:="ALY"
            streetNames.Add key:="crescent", Item:="CRES"
            streetNames.Add key:="green", Item:="GRN"
            streetNames.Add key:="meadow", Item:="MDW"
            streetNames.Add key:="ridges", Item:="RDGS"
            streetNames.Add key:="valleys", Item:="VLYS"
            streetNames.Add key:="annex", Item:="ANX"
            streetNames.Add key:="crest", Item:="CRST"
            streetNames.Add key:="greens", Item:="GRNS"
            streetNames.Add key:="meadows", Item:="MDWS"
            streetNames.Add key:="river", Item:="RIV"
            streetNames.Add key:="viaduct", Item:="VIA"
            streetNames.Add key:="arcade", Item:="ARC"
            streetNames.Add key:="crossing", Item:="XING"
            streetNames.Add key:="grove", Item:="GRV"
            streetNames.Add key:="mews", Item:="MEWS"
            streetNames.Add key:="road", Item:="RD"
            streetNames.Add key:="view", Item:="VW"
            streetNames.Add key:="avenue", Item:="AVE"
            streetNames.Add key:="crossroad", Item:="XRD"
            streetNames.Add key:="groves", Item:="GRVS"
            streetNames.Add key:="mill", Item:="ML"
            streetNames.Add key:="roads", Item:="RDS"
            streetNames.Add key:="views", Item:="VWS"
            streetNames.Add key:="bayou", Item:="BYU"
            streetNames.Add key:="crossroads", Item:="XRDS"
            streetNames.Add key:="harbor", Item:="HRB"
            streetNames.Add key:="mills", Item:="MLS"
            streetNames.Add key:="route", Item:="RTE"
            streetNames.Add key:="village", Item:="VLG"
            streetNames.Add key:="beach", Item:="BCH"
            streetNames.Add key:="curve", Item:="CURV"
            streetNames.Add key:="harbors", Item:="HRBS"
            streetNames.Add key:="mission", Item:="MSN"
            streetNames.Add key:="row", Item:="ROW"
            streetNames.Add key:="villages", Item:="VLGS"
            streetNames.Add key:="bend", Item:="BND"
            streetNames.Add key:="dale", Item:="DL"
            streetNames.Add key:="haven", Item:="HVN"
            streetNames.Add key:="motorway", Item:="MTWY"
            streetNames.Add key:="rue", Item:="RUE"
            streetNames.Add key:="ville", Item:="VL"
            streetNames.Add key:="bluff", Item:="BLF"
            streetNames.Add key:="dam", Item:="DM"
            streetNames.Add key:="heights", Item:="HTS"
            streetNames.Add key:="mount", Item:="MT"
            streetNames.Add key:="run", Item:="RUN"
            streetNames.Add key:="vista", Item:="VIS"
            streetNames.Add key:="bluffs", Item:="BLFS"
            streetNames.Add key:="divide", Item:="DV"
            streetNames.Add key:="highway", Item:="HWY"
            streetNames.Add key:="mountain", Item:="MTN"
            streetNames.Add key:="shoal", Item:="SHL"
            streetNames.Add key:="walk", Item:="WALK"
            streetNames.Add key:="bottom", Item:="BTM"
            streetNames.Add key:="drive", Item:="DR"
            streetNames.Add key:="hill", Item:="HL"
            streetNames.Add key:="mountains", Item:="MTNS"
            streetNames.Add key:="shoals", Item:="SHLS"
            streetNames.Add key:="wall", Item:="WALL"
            streetNames.Add key:="boulevard", Item:="BLVD"
            streetNames.Add key:="drives", Item:="DRS"
            streetNames.Add key:="hills", Item:="HLS"
            streetNames.Add key:="neck", Item:="NCK"
            streetNames.Add key:="shore", Item:="SHR"
            streetNames.Add key:="way", Item:="WAY"
            streetNames.Add key:="branch", Item:="BR"
            streetNames.Add key:="estate", Item:="EST"
            streetNames.Add key:="hollow", Item:="HOLW"
            streetNames.Add key:="orchard", Item:="ORCH"
            streetNames.Add key:="shores", Item:="SHRS"
            streetNames.Add key:="ways", Item:="WAYS"
            streetNames.Add key:="brook", Item:="BRK"
            streetNames.Add key:="estates", Item:="ESTS"
            streetNames.Add key:="inlet", Item:="INLT"
            streetNames.Add key:="oval", Item:="OVAL"
            streetNames.Add key:="skyway", Item:="SKWY"
            streetNames.Add key:="well", Item:="WL"
            streetNames.Add key:="brooks", Item:="BRKS"
            streetNames.Add key:="expressway", Item:="EXPY"
            streetNames.Add key:="island", Item:="IS"
            streetNames.Add key:="overpass", Item:="OPAS"
            streetNames.Add key:="spring", Item:="SPG"
            streetNames.Add key:="wells", Item:="WLS"
            streetNames.Add key:="burg", Item:="BG"
            streetNames.Add key:="extension", Item:="EXT"
            streetNames.Add key:="islands", Item:="ISS"
            streetNames.Add key:="park", Item:="PARK"
            streetNames.Add key:="springs", Item:="SPGS"
            streetNames.Add key:="burgs", Item:="BGS"
            streetNames.Add key:="extensions", Item:="EXTS"
            streetNames.Add key:="isle", Item:="ISLE"
            streetNames.Add key:="parkway", Item:="PKWY"
            streetNames.Add key:="spur", Item:="SPUR"
            streetNames.Add key:="bypass", Item:="BYP"
            streetNames.Add key:="fall", Item:="FALL"
            streetNames.Add key:="junction", Item:="JCT"
            streetNames.Add key:="passage", Item:="PSGE"
            streetNames.Add key:="square", Item:="SQ"
            streetNames.Add key:="camp", Item:="CP"
            streetNames.Add key:="falls", Item:="FLS"
            streetNames.Add key:="junctions", Item:="JCTS"
            streetNames.Add key:="path", Item:="PATH"
            streetNames.Add key:="squares", Item:="SQS"
            streetNames.Add key:="canyon", Item:="CYN"
            streetNames.Add key:="ferry", Item:="FRY"
            streetNames.Add key:="key", Item:="KY"
            streetNames.Add key:="pike", Item:="PIKE"
            streetNames.Add key:="station", Item:="STA"
            streetNames.Add key:="cape", Item:="CPE"
            streetNames.Add key:="field", Item:="FLD"
            streetNames.Add key:="keys", Item:="KYS"
            streetNames.Add key:="pine", Item:="PNE"
            streetNames.Add key:="stravenue", Item:="STRA"
            streetNames.Add key:="causeway", Item:="CSWY"
            streetNames.Add key:="fields", Item:="FLDS"
            streetNames.Add key:="knoll", Item:="KNL"
            streetNames.Add key:="pines", Item:="PNES"
            streetNames.Add key:="stream", Item:="STRM"
            streetNames.Add key:="center", Item:="CTR"
            streetNames.Add key:="flat", Item:="FLT"
            streetNames.Add key:="knolls", Item:="KNLS"
            streetNames.Add key:="place", Item:="PL"
            streetNames.Add key:="street", Item:="ST"
            streetNames.Add key:="centers", Item:="CTRS"
            streetNames.Add key:="flats", Item:="FLTS"
            streetNames.Add key:="lake", Item:="LK"
            streetNames.Add key:="plain", Item:="PLN"
            streetNames.Add key:="streets", Item:="STS"
            streetNames.Add key:="circle", Item:="CIR"
            streetNames.Add key:="ford", Item:="FRD"
            streetNames.Add key:="lakes", Item:="LKS"
            streetNames.Add key:="plains", Item:="PLNS"
            streetNames.Add key:="summit", Item:="SMT"
            streetNames.Add key:="circles", Item:="CIRS"
            streetNames.Add key:="fords", Item:="FRDS"
            streetNames.Add key:="land", Item:="LAND"
            streetNames.Add key:="plaza", Item:="PLZ"
            streetNames.Add key:="terrace", Item:="TER"
            streetNames.Add key:="cliff", Item:="CLF"
            streetNames.Add key:="forest", Item:="FRST"
            streetNames.Add key:="landing", Item:="LNDG"
            streetNames.Add key:="point", Item:="PT"
            streetNames.Add key:="throughway", Item:="TRWY"
            streetNames.Add key:="cliffs", Item:="CLFS"
            streetNames.Add key:="forge", Item:="FRG"
            streetNames.Add key:="lane", Item:="LN"
            streetNames.Add key:="points", Item:="PTS"
            streetNames.Add key:="trace", Item:="TRCE"
            streetNames.Add key:="club", Item:="CLB"
            streetNames.Add key:="forges", Item:="FRGS"
            streetNames.Add key:="light", Item:="LGT"
            streetNames.Add key:="port", Item:="PRT"
            streetNames.Add key:="track", Item:="TRAK"
            streetNames.Add key:="common", Item:="CMN"
            streetNames.Add key:="fork", Item:="FRK"
            streetNames.Add key:="lights", Item:="LGTS"
            streetNames.Add key:="ports", Item:="PRTS"
            streetNames.Add key:="trafficway", Item:="TRFY"
            streetNames.Add key:="corner", Item:="COR"
            streetNames.Add key:="forks", Item:="FRKS"
            streetNames.Add key:="loaf", Item:="LF"
            streetNames.Add key:="prairie", Item:="PR"
            streetNames.Add key:="trail", Item:="TRL"
            streetNames.Add key:="corners", Item:="CORS"
            streetNames.Add key:="fort", Item:="FT"
            streetNames.Add key:="lock", Item:="LCK"
            streetNames.Add key:="radial", Item:="RADL"
            streetNames.Add key:="trailer", Item:="TRLR"
            streetNames.Add key:="course", Item:="CRSE"
            streetNames.Add key:="freeway", Item:="FWY"
            streetNames.Add key:="locks", Item:="LCKS"
            streetNames.Add key:="ramp", Item:="RAMP"
            streetNames.Add key:="tunnel", Item:="TUNL"
            streetNames.Add key:="court", Item:="CT"
            streetNames.Add key:="garden", Item:="GDN"
            streetNames.Add key:="lodge", Item:="LDG"
            streetNames.Add key:="ranch", Item:="RNCH"
            streetNames.Add key:="turnpike", Item:="TPKE"
            streetNames.Add key:="courts", Item:="CTS"
            streetNames.Add key:="gardens", Item:="GDNS"
            streetNames.Add key:="loop", Item:="LOOP"
            streetNames.Add key:="rapid", Item:="RPD"
            streetNames.Add key:="underpass", Item:="UPAS"
            streetNames.Add key:="cove", Item:="CV"
            streetNames.Add key:="gateway", Item:="GTWY"
            streetNames.Add key:="mall", Item:="MALL"
            streetNames.Add key:="rapids", Item:="RPDS"
            streetNames.Add key:="union", Item:="UN"
            streetNames.Add key:="coves", Item:="CVS"
            streetNames.Add key:="glen", Item:="GLN"
            streetNames.Add key:="manor", Item:="MNR"
            streetNames.Add key:="rest", Item:="RST"
            streetNames.Add key:="unions", Item:="UNS"
            streetNames.Add key:="creek", Item:="CRK"
            streetNames.Add key:="glens", Item:="GLNS"
            streetNames.Add key:="manors", Item:="MNRS"
            streetNames.Add key:="ridge", Item:="RDG"
            streetNames.Add key:="valley", Item:="VLY"
    
            Set getStreetNames = streetNames

            End Function
    
            Public Function getQueensNeighborhoods() As Variant()
    
            Dim queensNeighborhoods() As Variant
            queensNeighborhoods = Array("Arverne", "Astoria", "Astoria Heights", "Auburndale", "Bay Terrace", "Bayside", "Bayswater", "Beechhurst", "Bellaire", "Belle Harbor", "Bellerose", "Blissville", _
            "Breezy Point", "Briarwood", "Broad Channel", "Cambria Heights", "College Point", "Corona", "Ditmars", "Douglaston", "Dutch Kills", "East Elmhurst", "Edgemere", _
            "Electchester", "Elmhurst", "Far Rockaway", "Floral Park", "Flushing", "Flushing Chinatown", "Forest Hills", "Forest Hills Gardens", "Fresh Meadows", "Fresh Pond", _
            "Glen Oaks", "Glendale", "Hamilton Beach", "Hammels", "Hillcrest", "Hollis", "Hollis Hills", "Holliswood", "Howard Beach", "Howard Park", "Hunters Point", "Jackson Heights", "Jamaica", _
            "Jamaica Estates", "Jamaica Hills", "Kew Gardens", "Kew Gardens Hills", "Laurelton", "LeFrak City", "Linden Hill", "Lindenwood", "Little Neck", "Long Island City", "Malba", "Maspeth", _
            "Meadowmere", "Middle Village", "Murray Hill", "Neponsit", "North Corona", "North Shore Towers", "Oakland Gardens", "Old Howard Beach", "Ozone Park", "Pomonok", "Queens Village", _
            "Queensboro Hill", "Queensbridge", "Queensview", "Queensview West", "Ramblersville", "Ravenswood", "Rego Park", "Richmond Hill", "Ridgewood", "Rochdale", "Rockaway Beach", "Rockaway Park", _
            "Rockwood Park", "Rosedale", "Roxbury", "Saint Albans", "Seaside", "South Jamaica", "South Ozone Park", "Springfield Gardens", "Sunnyside", "Sunnyside Gardens", "Tudor Village", "Utopia", _
            "Warnerville", "Whitestone", "Willets Point", "Woodhaven", "Woodside", "Wyckoff Heights")

            getQueensNeighborhoods = queensNeighborhoods

            End Function

         Public Function getCounties(queensNeighborhoods() As Variant) As Dictionary
       
                Dim counties As New Dictionary
                counties.Add key:="bronx", Item:=NYCounties.bronx
                counties.Add key:="kings", Item:=NYCounties.kings
                counties.Add key:="brooklyn", Item:=NYCounties.kings
                counties.Add key:="manhattan", Item:=NYCounties.manhattan
                counties.Add key:="new york", Item:=NYCounties.manhattan
                counties.Add key:="queens", Item:=NYCounties.queens
                For i = 0 To UBound(queensNeighborhoods) - 1
                    counties.Add key:=LCase(queensNeighborhoods(i)), Item:=NYCounties.queens
                Next
                counties.Add key:="richmond", Item:=NYCounties.richmond
                counties.Add key:="staten island", Item:=NYCounties.richmond
        
                Set getCounties = counties

            End Function
            Public Sub parseBuildingAddresses()
        
                Dim queensNeighborhoods() As Variant
                queensNeighborhoods = getQueensNeighborhoods
                Dim streetNames As Dictionary
                Set streetNames = getStreetNames
                Dim counties As Dictionary
                Set counties = getCounties(queensNeighborhoods)
        
                Dim tbl As Recordset, s() As String
                On Error GoTo parseBuildingAddresses_error
                Set tbl = PMSdb.OpenRecordset("Building")
                While Not tbl.EOF
                    If Trim(tbl("Multiple Dwelling Registration Number")) <> "" Then
                        s = parseAddress(tbl("Address 1"), tbl("Address 2"), queensNeighborhoods, streetNames, counties)
                        tbl.Edit
                        tbl("DHCRLow") = s(0)
                        tbl("DHCRHigh") = s(1)
                        tbl("DHCRStreetName") = s(2)
                        tbl("DHCRStreetSuffix") = s(3)
                        tbl("DHCRCity") = s(4)
                        tbl("DHCRZip") = s(5)
                        tbl("DHCRCounty") = s(6)
                        tbl.Update
                    End If
                    tbl.MoveNext
                Wend
                GoTo parseBuildingAddresses_exit
        
        parseBuildingAddresses_error:
            MsgBox ("Error updating Building DHCR Address fields: " & Err.Description)
            Resume parseBuildingAddresses_exit
    
        parseBuildingAddresses_exit:
            On Error Resume Next
            tbl.Close
        
            End Sub
         
    Public Function parseAddress(address1 As String, address2 As String, queensNeighborhoods() As Variant, streetNames As Dictionary, counties As Dictionary) As String()
    Dim s(8) As String
 
    For i = 0 To UBound(s)
        s(i) = " "
    Next
    On Error GoTo parseAddress_error
        ' Address 1
        Dim parts() As String
        parts = Split(Trim(address1), " ")
        s(0) = parts(0)
        s(1) = parts(0)
        If UBound(parts) >= 1 And UBound(parts) <= 2 Then
            s(2) = parts(1)
            If UBound(parts) = 2 Then
                If streetNames.Exists(LCase(parts(2))) Then
                    s(3) = streetNames.Item(LCase(parts(2)))
                Else
                    s(3) = parts(2)
                End If
            End If
        ElseIf UBound(parts) > 2 Then
            s(2) = parts(1) & " " & parts(2)
            If streetNames.Exists(LCase(parts(3))) Then
                s(3) = streetNames.Item(LCase(parts(3)))
            Else
                s(3) = parts(3)
            End If
        End If

        ' Address 2
        Dim addr1 As String
        addr1 = Replace(address2, "N.Y.", "NY")
        Dim addr2 As String
        addr2 = Replace(addr1, "N.J.", "NJ")
        parts = Split(Trim(addr2), ",")
        Dim subParts() As String
        Select Case UBound(parts)
            Case 0
                subParts = Split(Trim(parts(0)), " ")
                Select Case UBound(subParts)
                    Case 0
                        s(4) = parts(0)
                        If counties.Exists(LCase(parts(0))) Then
                            s(6) = counties.Item(LCase(parts(0)))
                        End If
                    Case 1
                        If IsNumeric(subParts(1)) Then
                            s(4) = subParts(0)
                            If counties.Exists(LCase(subParts(0))) Then
                                s(6) = counties.Item(LCase(subParts(0)))
                            End If
                            s(5) = Format(Val(subParts(1)), "#########")
                        Else
                            s(4) = parts(0)
                            If counties.Exists(LCase(subParts(0))) Then
                                s(6) = counties.Item(LCase(subParts(0)))
                            ElseIf counties.Exists(LCase(parts(0))) Then
                                s(6) = counties.Item(LCase(parts(0)))
                            End If
                        End If
                    Case 2
                        If IsNumeric(subParts(2)) Then
                            s(4) = subParts(0) & " " & subParts(1)
                            s(5) = Format(Val(subParts(2)), "#########")
                        Else
                            s(4) = parts(0)
                        End If
                        If counties.Exists(LCase(subParts(0))) Then
                            s(6) = counties.Item(LCase(parts(0)))
                        ElseIf counties.Exists(LCase(subParts(0)) & " " & LCase(subParts(1))) Then
                            s(6) = counties.Item(LCase(subParts(0)) & " " & LCase(subParts(1)))
                        End If
                    Case Is >= 3
                        If IsNumeric(subParts(UBound(subParts) - 1)) Then
                            For i = 0 To UBound(subParts) - 2
                                s(4) = s(4) & subParts(i) & " "
                            Next
                            s(5) = Format(Val(subParts(UBound(subParts) - 1)), "#########")
                        Else
                            s(4) = parts(0)
                        End If
                        If counties.Exists(LCase(subParts(0))) Then
                            s(6) = counties.Item(LCase(parts(0)))
                        ElseIf counties.Exists(LCase(subParts(0)) & " " & LCase(subParts(1))) Then
                            s(6) = counties.Item(LCase(subParts(0)) & " " & LCase(subParts(1)))
                        End If

                End Select
            Case 1
                If counties.Exists(LCase(parts(0))) Then
                    s(6) = counties.Item(LCase(parts(0)))
                End If
                s(4) = parts(0)
                subParts = Split(Trim(parts(1)), " ")
                Select Case UBound(subParts)
                    Case 0
                        If IsNumeric(subParts(0)) Then
                            s(5) = Format(Val(subParts(0)), "#########")
                        Else
                            s(4) = s(4) & ", " & parts(1)
                        End If
                    Case Is >= 1
                        's(1, 4) &= ", " & subParts(0)
                        If IsNumeric(subParts(UBound(subParts))) Then
                            s(5) = Format(Val(subParts(UBound(subParts))), "#########")
                        Else
                            s(4) = s(4) & " " & subParts(UBound(subParts))
                        End If
                End Select
        End Select
        GoTo parseAddress_exit
        
parseAddress_error:
        For i = 0 To UBound(s)
            s(i) = " "
        Next
        Resume parseAddress_exit

parseAddress_exit:
        parseAddress = s

    End Function

        */
    }
}
