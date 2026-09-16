using System;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MbcsCentral.WinPM
{
    public static class GLTransactions
    {
        public static bool GLproc(Control ctrl)
        {
            bool opened = false;
            string message = "";
            string innermessage = "";

            //Paytemp[] payTemps = Paytemp.loadByUser();
            //if (payTemps == null || payTemps.Length == 0)
            //    return false;
            Paytemp[] PayForView = Paytemp.loadByUser(true);
            if (PayForView == null || PayForView.Length == 0)
                return false;
            DataView payTemps = new DataView( PayForView[0].dataTable);
            payTemps.Sort = Paytemp.SQL_COL_BUILDING_NUMBER + ", " + Paytemp.SQL_COL_DATE;
            //Dim x As String, dt As String, f() As String, fl As Boolean
            string x;
            string dt;
            bool fl;
            //Dim i As Integer, ST As String, BN As String, artrans As artranstype
            //Dim j As Integer, ca() As Variant, acct() As Variant, ix As Integer
            ChargePayment[] ca = new ChargePayment[0];
            TextWriter transW = null;

            if (!GlobalShared.WinGlEnabled)
            {
                // Local path to GL folder
                if ((ctrl.WinglDatabase == null || ctrl.WinglDatabase.Trim().Equals("")) || ctrl.WinglDatabase.Equals("[Internal]"))
                {
                    MessageBoxCustom.Show("WinGL Database location not defined. Cannot post to G/L");
                    return false;
                }
                string glPath = ctrl.WinglDatabase.Substring(0, ctrl.WinglDatabase.LastIndexOf("\\"));
                if (!glPath.Trim().Equals(WinpmGlobal.WINGL_LOCATION_TAG))
                {
                    string artrans = glPath + "\\artrans";
                    while (!opened)
                    {
                        try
                        {
                            transW = new StreamWriter(artrans, true);
                            opened = true;
                        }
                        catch (IOException ex)
                        {
                            message = "IOException - Unable to open ARTRANS: " + ex.Message;
                            if (ex.InnerException != null)
                                innermessage = ex.InnerException.Message;
                            opened = false;
                        }
                        catch (System.Exception ex)
                        {
                            message = "SystemException - Unable to open ARTRANS: " + ex.Message;
                            if (ex.InnerException != null)
                                innermessage = ex.InnerException.Message;
                            opened = false;
                        }
                        if (!opened)
                        {
                            MessageBox.Show(message);
                            if (innermessage != null && !innermessage.Trim().Equals(""))
                                MessageBox.Show(innermessage);
                            if (MessageBox.Show("Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                return false;
                        }
                    }
                }
            }

            // Fill ChargeCode and Building Dictionaries
            ChargeCodes[] chargeCodes = ChargeCodes.loads(GlobalShared.user.Account.clientId);
            Dictionary<string, ChargeCodes> Codes = new Dictionary<string, ChargeCodes>();
            if (chargeCodes != null && chargeCodes.Length > 0)
                foreach (ChargeCodes chg in chargeCodes)
                    Codes.Add(chg.ChargeCode, chg);
            chargeCodes = null;
            Building[] blds = Building.load(GlobalShared.user.Account.clientId);
            Dictionary<string, Building> Buildings = new Dictionary<string, Building>();
            if (blds != null && blds.Length > 0)
                foreach (Building bld in blds)
                    Buildings.Add(bld.BuildingNumber, bld);


            GLTable[] glTab = GLTable.load();
            Dictionary<string, GLTable> f = new Dictionary<string, GLTable>();
            if (glTab != null && glTab.Length > 0)
            {
                foreach (GLTable gl in glTab)
                    if (!f.ContainsKey(gl.From))
                        f.Add(gl.From, gl);
            }
            // Loop through payment entries
            string bldkey = "";
            DateTime payDate = GlobalShared.NULL_DATE;
            bool found = false;
            int l;

            foreach (DataRowView pay in payTemps)
            {
                if (bldkey.Trim() == "")
                    bldkey = (string)pay[Paytemp.SQL_COL_BUILDING_NUMBER];
                if (payDate == GlobalShared.NULL_DATE)
                    payDate = ((DateTime)pay[Paytemp.SQL_COL_DATE]);
                    payDate = ((DateTime)pay[Paytemp.SQL_COL_DATE]).Date;
                if (!pay[Paytemp.SQL_COL_BUILDING_NUMBER].Equals(bldkey) || ((DateTime)pay[Paytemp.SQL_COL_DATE]).Date != payDate.Date)
                {
                    Glproc_bld(ca, Codes, bldkey, Buildings, f, transW);
                    bldkey = (string)pay[Paytemp.SQL_COL_BUILDING_NUMBER];
                    payDate = ((DateTime)pay[Paytemp.SQL_COL_DATE]).Date;
                    ca = null;
                    ca = new ChargePayment[0];
                }
                found = false;
                for (int i = 0; i < ca.Length; i++)
                {
                    if (ca[i].Code.Equals((string)pay[Paytemp.SQL_COL_CODE]))
                    {
                        ca[i].Amount += (decimal)pay[Paytemp.SQL_COL_AMOUNT];
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    l = ca.Length;
                    Array.Resize<ChargePayment>(ref ca, l + 1);
                    ca[l] = new ChargePayment();
                    ca[l].Code = (string)pay[Paytemp.SQL_COL_CODE];
                    ca[l].Amount = (decimal)pay[Paytemp.SQL_COL_AMOUNT];
                    ca[l].Date = (DateTime)pay[Paytemp.SQL_COL_DATE];
                }
                pay.Row.Delete();
            }
            Glproc_bld(ca, Codes, bldkey, Buildings, f, transW);
            payTemps = null;
            if (!GlobalShared.WinGlEnabled)
                transW.Close();
            return true;
        }

        private static void Glproc_bld(ChargePayment[] ca, Dictionary<string, ChargeCodes> Codes,
                                string bldkey, Dictionary<string, Building> Buildings,
                                Dictionary<string, GLTable> f, TextWriter transW)
        {
            string BN;
            Building bld;
            string st;
            decimal amount;
            int acctNum;
            
            if (Buildings.TryGetValue(bldkey, out bld))
            {
                WinGL.ArTrans Trans = null;
                ChargeCodes chg;
                Dictionary<string, decimal> acct = new Dictionary<string, decimal>();
                decimal account;
                GLTable g;
                string glBld = bldkey;
                if (f.TryGetValue(bldkey, out g))
                    glBld = g.To;
                BN = String.Format("{0:00}{1:000}", bld.OwnerNumber, int.Parse(glBld));

                foreach (ChargePayment pay in ca)
                {
                    if (pay.Amount != 0)
                    {
                        if (Codes.TryGetValue(pay.Code, out chg))
                        {
                            if (chg.GLCreditAccount.Trim() != "")
                            {
                                // TODO: Change to account Key on general ledger side
                                if (GlobalShared.WinGlEnabled)
                                {
                                    Trans = new WinGL.ArTrans();
                                    Trans.Company = bld.OwnerNumber;
                                    Trans.Department = short.Parse(glBld);
                                    acctNum = 0;
                                    Int32.TryParse(chg.GLCreditAccount, out acctNum);
                                    Trans.Account = acctNum;
                                    Trans.Date = pay.Date;
                                    Trans.Description = Truncate(chg.ChargeDescription, 18).Trim().PadRight(18);
                                    Trans.Amount = -pay.Amount;
                                    Trans.save();
                                    Trans = null;
                                }
                                else
                                {
                                    acctNum = 0;
                                    Int32.TryParse(chg.GLCreditAccount, out acctNum);
                                    st = String.Format("{0}{1:0000000}{2:MMddyy}{3}{4:00000000.00;-0000000.00}"
                                                    , BN, acctNum * 1000, DateTime.Now, Truncate(chg.ChargeDescription, 18).Trim().PadRight(18), -pay.Amount);
                                    try
                                    {
                                        transW.WriteLine(st);
                                    }
                                    catch (IOException ex)
                                    {
                                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to write AR Transaction <" + st + "> : " + ex.Message, false);
                                        return; // false;
                                    }
                                }
                                if (acct.TryGetValue(chg.GLDebitAccount + ";" + pay.Date.Date.ToString("MM/dd/yyyy"), out amount))
                                {
                                    amount += pay.Amount;
                                    acct[chg.GLDebitAccount + ";" + pay.Date.Date.ToString("MM/dd/yyyy") ] = amount;
                                }
                                else
                                {
                                    acct.Add(chg.GLDebitAccount + ";" + pay.Date.Date.ToString("MM/dd/yyyy"), pay.Amount);
                                }
                            }
                        }
                    }
                }
                if (acct != null && acct.Count > 0)
                {
                    foreach (string key in acct.Keys)
                    {
                        string[] index = key.Split(';');
                        DateTime date;
                        if (index.Length == 1)
                            date = DateTime.Now.Date;
                        else
                            date = DateTime.Parse(index[1]);
                        amount = acct[key];
                        if (amount != 0)
                        {
                            if (GlobalShared.WinGlEnabled)
                            {
                                Trans = new WinGL.ArTrans();
                                Trans.Company = bld.OwnerNumber;
                                Trans.Department = short.Parse(glBld);
                                Trans.Date = date.Date;
                                acctNum = 0;
                                Int32.TryParse(index[0], out acctNum);
                                Trans.Account = acctNum;
                                Trans.Description = "Cash Receipts".PadRight(18);
                                Trans.Amount = amount;
                                Trans.save();
                                Trans = null;
                            }
                            else
                            {
                                string[] acctKey = key.Split(';');
                                acctNum = 0;
                                Int32.TryParse(acctKey[0], out acctNum);
                                st = String.Format("{0}{1:0000000}{2:MMddyy}{3}{4:00000000.00;-0000000.00}"
                                                    , BN, acctNum * 1000, DateTime.Now, "Cash Receipts".PadRight(18), amount);
                                try
                                {
                                    transW.WriteLine(st);
                                }
                                catch (IOException ex)
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to write AR Transaction <" + st + "> : " + ex.Message, false);
                                    return; // false;
                                }
                            }
                        }
                    }
                }
            }

        }
        public static string Truncate(this string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                return value.Substring(0, maxLength);
            }

            return value;
        }
    }
}
