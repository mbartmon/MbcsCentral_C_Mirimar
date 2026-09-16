using System;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Transactions;

namespace MbcsCentral.WinPM
{
    public static class ProcessEOM
    {

    //************************************************************************
    //* 12/30/98 cpaid needed to be zeroed between charges
    //************************************************************************
    //* 07/29/00 Some charges were disappearing
    //************************************************************************
        static long clientId = GlobalShared.user.Account.clientId;
	    static decimal Bal;
	    static decimal cbal;
	    static decimal cpaid;
	    static System.DateTime bildate;
        static System.DateTime clsdate;
	    static System.DateTime LastDate;
        static frmEOM _frmEom;

	    public static void EomReorg(DateTime billingDate, DateTime closingDate, frmEOM frm)
	    {
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting EOM REORG", false);
            bildate = billingDate;
            clsdate = closingDate;
            _frmEom = frm;
		    short i;
		    string kp;
		    string x;
		    string xsql;
		    double cr;

            try
            {
                Tenants[] tblTenant = Tenants.loadForEom(clientId);
                Charges[] tblCharges;

                _frmEom.progressMsg = "Process 1 of 3";
                cr = 0;
                _frmEom.prgProgress.Minimum = 0;
                _frmEom.prgProgress.Maximum = tblTenant.Count();
                foreach (Tenants ten in tblTenant)
                {
                    if (ten.NewTenantFlag)
                    {
                        if (ten.MoveinDate <= GlobalShared.NULL_DATE || ten.MoveinDate <= bildate)
                        {
                            ten.NewTenantFlag = false;
                            ten.save();
                            newTenant(ten);
                        }
                    }
                    if (ten.TenantNumber.Length > 3 && ten.TenantNumber.Substring(3, 1).Equals("B") && !ten.LegalCode.Equals("B"))
                        ten.delete(ten.SequenceNumber.ToString());
                    cr = cr + 1;
                    if (cr < _frmEom.prgProgress.Maximum - _frmEom.prgProgress.Maximum / 10)
                        _frmEom.Progress = cr;
                }
                _frmEom.Progress = _frmEom.prgProgress.Maximum;

                // Gather History Charges
                EomHistChg();

                // Update [Total Charges] in Tenants
                WinpmGlobal.calc_Charges(clientId, bildate, _frmEom);

                Control ctrl = Control.load(clientId);
                ctrl.LastReorgDate = bildate;
                ctrl.save();
                _frmEom.progressMsg = "DONE";
                GlobalShared.Log.Log((int)LogClass.logType.Info, "Completed EOM REORG", false);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "REORG: " + ex.Message, false);
            }

	    }

        static void newTenant(Tenants ten)
        {
		    string NT;
  		    string BN;
		    decimal TCHGS = 0;
		    Tenants fb = null;
		    
            //* ROUTINE TO PROCESS NEW TENANTS
		    // TRANSACTION PROCESSING ?? ?? AccessApplication_definst.DBEngine.Workspaces(0).BeginTrans();
		    NT = ten.TenantNumber;
		    BN = ten.BuildingNumber;
		    fb = null;

            try
            {
                // Delete old tenant or convert to 'B'
                Tenants tblTen = Tenants.load(ten.ReplacingTenant);
                if (tblTen != null)
                {
                    TenantsArchive tenArch = tblTen.copyToArchive();
                    tenArch.NewOnly = true;
                    tenArch.save();
                    if (tblTen.LegalCode != null) {
                        if (tblTen.LegalCode.ToUpper().Equals("B")) {
                            tblTen.TenantNumber = NT.Substring(0, 3) + "B";
                            tblTen.MBR = 0;
                            tblTen.save();
                            fb = tblTen;
                        } else {
                            tblTen.delete(tblTen.SequenceNumber.ToString());
                        }
                    } else {
                        tblTen.delete(tblTen.SequenceNumber.ToString());
                    }
                }

                // Determine total charges of 'B' tenant
                if (fb != null)
                {
                    Charges[] chgs = Charges.loadBySeqNum(tblTen.SequenceNumber);
                    if (chgs != null) {
                        foreach (Charges chg in chgs)
                        {
                            if (chg.TenantNumber.Equals(NT.Substring(0, 4) + "B"))
                            {
                                TCHGS = TCHGS + chg.Amount + chg.Arrears;
                                chg.delete(chg.ChargeNumber.ToString());
                            }
                        }
                    }
                    fb.TotalCharges = TCHGS;
                    tblTen.save();
                    TCHGS = 0;
                }
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "REORG NEWTENANT:" + ex.Message, false);
            }
		    //AccessApplication_definst.DBEngine.Workspaces(0).CommitTrans();
        }

        private static void EomHistChg()
        {
            _frmEom.progressMsg = "Process 2 of 3";
            _frmEom.Progress = 0;
            double cr = 0;
            Charges[] chgs = Charges.load();
            if (chgs != null)
            {
                _frmEom.Progress = 0;
                _frmEom.prgProgress.Maximum = chgs.Count();
                foreach (Charges chg in chgs)
                {
                    WinpmGlobal.HistChgUpdate(chg, true, false, chg.HistoryDate);
                    cr = cr + 1;
                    if (cr < _frmEom.prgProgress.Maximum - _frmEom.prgProgress.Maximum/10)
                        _frmEom.Progress = cr;
                }
                _frmEom.Progress = _frmEom.prgProgress.Maximum;
            }
        }

        public static void EomRoll(DateTime billingDate, DateTime closingDate, frmEOM frm)
	    {

            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting EOM ROLL.", false);

            _frmEom = frm;
            _frmEom.progressMsg = "PROCESSING -STEP 1 of 2";
            _frmEom.Progress = 0;

            bildate = billingDate;
            clsdate = closingDate;
            TenantsSnapShot.MakeSnapShots(clsdate);

            DateTime clsStart = new DateTime(clsdate.Year, clsdate.Month, 1);

		    short i;
		    short errorcount;
		    int rc;
		    int cr;
		    decimal totCbal = 0;

		    errorcount = 0;
		    cr = 0;

            Tenants[] tenants = Tenants.loadForEom(clientId);
            if (tenants != null && tenants.Length > 0)
            {
                foreach (Tenants ten in tenants)
                {
                    _frmEom.prgProgress.Minimum = 0;
                    _frmEom.prgProgress.Maximum = tenants.Count();
                    _frmEom.progressMsg = "PROCESSING -STEP 2 of 2";
                    //DBEngine.Workspaces(0).BeginTrans
                    // 09/08/89
                    if (ten.NewTenantFlag)
                        processReplacedCharges(ten.ReplacingTenant, clsdate);
                    if (!ten.TenantName.Substring(0, 1).Equals("*") && !ten.TenantName.ToUpper().Contains("VACANT"))
                    {
                        if (ten.TenantNumber.Length > 3 && ten.TenantNumber.Substring(3, 1).Equals("B"))
                        {
                            ten.MBR = 0;
                            ten.save();
                            goto R51;
                        }
                        else
                        {
                            Bal = ten.Rent + ten.Arrears - ten.Paid;
                            // This section can never execute!
                            if (ten.TenantNumber.Length > 3 && ten.TenantNumber.Substring(3, 1).Equals("B"))
                            {
                                // Could not find any other reference to this non-existent field
                                // Bal = Bal + ten.Adjust;
                                if (Math.Floor(Bal * 100) <= 0)
                                {
                                    ten.delete(ten.SequenceNumber.ToString());
                                }
                                Bal = Bal + cpaid;
                                cpaid = 0;
                                goto R51;
                            }
                        }
                    }
                    // Ensure that there is a History Master record for this tenant
                    Histmast hmast = Histmast.load(ten.SequenceNumber);
                    if (hmast != null)
                    {
                        hmast.TenantName = ten.TenantName;
                    }
                    else
                    {
                        hmast = new Histmast();
                        hmast.BuildingNumber = ten.BuildingNumber;
                        hmast.TenantNumber = ten.TenantNumber;
                        hmast.SequenceNumber = ten.SequenceNumber;
                        hmast.Apartment = ten.Apartment;
                        hmast.TenantName = ten.TenantName;
                    }
                    hmast.save();

                    //********************************************
                    // UPDATE CLOSING-MONTH RENT AMOUNT IN HISTORY
                    //********************************************
                    HistChg histChg = HistChg.loadForEom(ten.SequenceNumber,clsStart, clsdate);
                    if (histChg == null)
                    {
                        histChg = new HistChg();
                        histChg.TenantNumber = ten.TenantNumber;
                        histChg.BuildingNumber = ten.BuildingNumber;
                        histChg.SequenceNumber = ten.SequenceNumber;
                        histChg.ChargeDate = new DateTime(clsdate.Year, clsdate.Month, 1);
                        histChg.Code = "=";
                    }
                    histChg.Amount = ten.Rent;
                    histChg.Arrears = ten.Arrears;
                    histChg.save();
                    histChg = null;

                    //************************
                    // FIND FUTURE RENT AMOUNT
                    //************************
                    FutureRents[] futures = FutureRents.loadBySequence(ten.SequenceNumber);
                    if (futures != null && futures.Length > 0)
                    {
                        foreach (FutureRents future in futures)
                        {
                            if (future.FutureDate > GlobalShared.NULL_DATE && future.FutureDate <= bildate && future.FutureAmount != 0)
                            {
                                ten.Rent = future.FutureAmount;
                                if (future.FutureLegal != 0)
                                    ten.RentLegal = future.FutureLegal;
                                else if (ten.RentLegal < ten.Rent)
                                    ten.RentLegal = ten.Rent;
                                future.delete(future.ID.ToString());
                                break;
                            }
                        }
                    }
                    /*
                    if (ten.FutureDate1 > GlobalShared.NULL_DATE && ten.FutureDate1 <= bildate && ten.FutureRent1 != 0)
                    {
                        ten.Rent = ten.FutureRent1;
                        if (ten.FutureLegal1 != 0)
                            ten.RentLegal = ten.FutureLegal1;
                        else
                            ten.RentLegal = ten.Rent;
                        ten.FutureRent1 = 0;
                        ten.FutureLegal1 = 0;
                        ten.FutureDate1 = GlobalShared.NULL_DATE;
                    }
                    if (ten.FutureDate2 != GlobalShared.NULL_DATE && ten.FutureDate2 <= bildate && ten.FutureRent2 != 0)
                    {
                        ten.Rent = ten.FutureRent2;
                        if (ten.FutureLegal2 != 0)
                            ten.RentLegal = ten.FutureLegal2;
                        else
                            ten.RentLegal = ten.Rent;
                        ten.FutureRent2 = 0;
                        ten.FutureLegal2 = 0;
                        ten.FutureDate2 = GlobalShared.NULL_DATE;
                    }
                    if (ten.FutureDate3 != GlobalShared.NULL_DATE && ten.FutureDate3 <= bildate && ten.FutureRent3 != 0)
                    {
                        ten.Rent = ten.FutureRent3;
                        if (ten.FutureLegal3 != 0)
                            ten.RentLegal = ten.FutureLegal3;
                        else
                            ten.RentLegal = ten.Rent;
                        ten.FutureRent3 = 0;
                        ten.FutureLegal3 = 0;
                        ten.FutureDate3 = GlobalShared.NULL_DATE;
                    }
                    */
                    //*******************************
                    // PROCESS CHARGES
                    //*******************************
                    cpaid = 0;
                    totCbal = 0;
                    Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                    if (chgs != null)
                    {
                        foreach (Charges tblCharges in chgs)
                        {
                            cbal = 0;
                            if (tblCharges.StartingDate <= bildate)
                            {
                                cpaid = tblCharges.Paid;
                                cbal = tblCharges.Amount + tblCharges.Arrears - cpaid;
                                tblCharges.Paid = 0;
                                if (tblCharges.StartingDate < bildate)
                                {
                                    tblCharges.Arrears = cbal;
                                }
                                //***************
                                // FUTURE CHARGE?
                                //***************
                                if (tblCharges.FutureDate > GlobalShared.NULL_DATE && tblCharges.FutureDate <= bildate && tblCharges.FutureAmount != 0)
                                {
                                    tblCharges.Amount = tblCharges.FutureAmount;
                                    tblCharges.FutureAmount = 0;
                                    tblCharges.StartingDate = tblCharges.FutureDate;
                                    tblCharges.FutureDate = GlobalShared.NULL_DATE;
                                }
                                else
                                {
                                    //*******************************************
                                    // IF ONE-TIME AND NO BALANCE DUE THEN DELETE
                                    //*******************************************
                                    if (tblCharges.Type.Equals("1"))
                                    {
                                        if (cbal == 0 && tblCharges.FutureAmount == 0)
                                        {
                                            tblCharges.delete(tblCharges.ChargeNumber.ToString());
                                            goto R50;
                                        }
                                        if (tblCharges.StartingDate != clsdate)
                                        {
                                            if (cbal == 0)
                                            {
                                                tblCharges.delete(tblCharges.ChargeNumber.ToString());
                                                Bal = Bal + cpaid;
                                                goto R50;
                                            }
                                            if (tblCharges.StartingDate < bildate)
                                            {
                                                tblCharges.Amount = 0;
                                            }
                                        }
                                    }
                                }
                                if (tblCharges.StartingDate < bildate)
                                {
                                    if (tblCharges.EndingDate > GlobalShared.NULL_DATE && tblCharges.EndingDate <= bildate)
                                    {
                                        if (cbal == 0)
                                        {
                                            tblCharges.delete(tblCharges.ChargeNumber.ToString());
                                            goto R50;
                                        }
                                        tblCharges.Amount = 0;
                                    }
                                }
                                totCbal = totCbal + tblCharges.Amount + tblCharges.Arrears;
                                tblCharges.HistoryDate = bildate;
                                tblCharges.save();
                                WinpmGlobal.HistChgUpdate(tblCharges, false, false, bildate);
                            }
                        R50:
                            Bal = Bal + cpaid;
                            cpaid = 0;
                        }
                    }
                R51:
                    // Now do the basic Tenant Account roll
                    if (ten.TenantNumber.Length < 4 || !ten.TenantNumber.Substring(3, 1).Equals("B"))
                    {
                        ten.Arrears = Bal;
                        ten.Paid = 0;
                        ten.LastPaidDate = GlobalShared.NULL_DATE;
                        ten.TotalCharges = totCbal;
                        totCbal = 0;
                        ten.save();

                        //****************************************
                        // CREATE NEW MONTH RENT AMOUNT IN HISTORY
                        //****************************************
                        HistChg tblHistChg = new HistChg();
                        tblHistChg.TenantNumber = ten.TenantNumber;
                        tblHistChg.BuildingNumber = ten.BuildingNumber;
                        tblHistChg.SequenceNumber = ten.SequenceNumber;
                        tblHistChg.ChargeDate = new DateTime(bildate.Year, bildate.Month, 1);
                        tblHistChg.Code = "=";
                        tblHistChg.Amount = ten.Rent;
                        tblHistChg.Arrears = ten.Arrears;
                        tblHistChg.save();
                    }
                    else
                    {
                        if (Bal <= 0)
                            ten.delete(ten.SequenceNumber.ToString());
                    }
                    cbal = 0;
                    Bal = 0;
                    totCbal = 0;
                    cr = cr + 1;
                    if (cr <= _frmEom.prgProgress.Maximum && cr % 10 == 0)
                        _frmEom.Progress = cr;
                }
                _frmEom.Progress = _frmEom.prgProgress.Maximum;
            }

	        // End of Processing
            Payments pay = new Payments();
	        bool result = pay.RemovePayments(clientId);
            GlobalShared.Log.Log((int)LogClass.logType.Info, "EOM Payments clear ran successfully: " + result.ToString(), false);
            pay = null;
	        Control tblControl = Control.load(clientId);
	        if (tblControl != null)
            {
                tblControl.LastRollDate = bildate;
                tblControl.save();
            }
            else
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,"EOM ROLL - Update Control Table was not successful.", true);

            _frmEom.Progress = _frmEom.prgProgress.Maximum;
            _frmEom.progressMsg = "DONE";
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Completed EOM ROLL", false);

	        // PMSdb = AccessApplication_definst.DBEngine.Workspaces(0).OpenDatabase(DefaultDataBase, false);
        }

        private static void  processReplacedCharges(int seq, DateTime clsDate)
        {
            Charges[] chgs = Charges.loadBySeqNum(seq);
            if (chgs != null)
            {
                foreach (Charges tblCharges in chgs)
                {
                    if (tblCharges.StartingDate <= clsDate)
                    {
                        decimal cbal = tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                        if (tblCharges.Type == "1" && cbal == 0 && tblCharges.FutureAmount == 0)
                            tblCharges.delete(tblCharges.ChargeNumber.ToString());
                    }
                }
            }
        }
    }
}