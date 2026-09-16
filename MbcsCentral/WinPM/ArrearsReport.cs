using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace MbcsCentral.WinPM
{
    class ArrearsReport : ReportBase, IDisposable
    {
        private long clientId = GlobalShared.user.Account.clientId;
        Report rpt = null;
        Font fnt = new Font("Courier New", (float)10.5);
        Font headerFont = new Font("Arial", 24);
        Font dateFont = new Font("Arial", 18);
        int page = 1;
        int Arrearscnt;
        int Totalcnt;

        struct ChargeDetail
        {
            public string code;
            public decimal amount;
        };
        List<ChargeDetail> chargeDetails = new List<ChargeDetail>();

        string[] header = {"Tenant#", "tab(10)", "Name", "tab(40)", "Apt", "  Due L    Rent/Mo     Balance    Last Paid"};

        private decimal[] totals = new decimal[2];
        private bool BldFlag;
        private int xseq = 0;
        private string xbld;
        private string xten;
        private decimal tchg;
        private decimal Bal;
        private string xname;
        private string xapt;
        private string xdue;
        private string xtype;
        private string xpager;
        private string xlegal;
        private string xEmail1;
        private string xhome2;
        private string xEmail2;
        private string xbus2;
        private decimal xrent;
        private decimal xarrears;
        private decimal xpaid;
        
        private string startBld;
        private string endBld;
        private decimal minimum;
        private int target;
        private bool addCharges;
        System.Printing.PrintQueue printer;
        public ArrearsReport() {}

        public ArrearsReport(string startBld, string endBld, decimal minimum, int target, System.Printing.PrintQueue printer = null, bool AddCharges = true)
        {
            this.startBld = startBld;
            this.endBld = endBld;
            this.minimum = minimum;
            this.target = target;
            this.printer = printer;
            this.addCharges = AddCharges;
        }

        public void Dispose()
        {
            fnt.Dispose();
            headerFont.Dispose();
            dateFont.Dispose();
            rpt.Dispose();
        }
        public override void Process()
        {
            Charges[] chgs;
            Tenants[] tens = Tenants.loadForStatements(startBld, endBld, clientId);
            if (tens == null || tens.Length == 0)
                return;
            Totalcnt = tens.Length;
            rpt = new Report("Arrears Report1", "Courier New", 8);
            rpt.SetFont = fnt;
            rpt.LineHeight = .15 * 96;
            rpt.LeftMargin = (float) 5;
            rpt.TopMargin = (float)5;
            rpt.BottomMargin = (float)50;
            rpt.BottomOffset = (float)50;
            bool first = true;
            string additionalCharges = "";
            List<string> addList = new List<string>();
            decimal RentDue;
            decimal RentPaid;
            foreach (Tenants ten in tens)
            {
                SetTenant(ten);
                ChargeDetail chgDetail;
                RentDue = ten.Rent + ten.Arrears;
                bool chgFound = false;
                chargeDetails.Clear();
                decimal ChargesPaid = 0;
                tchg = 0;
                chgs = Charges.loadBySeqNum(ten.SequenceNumber);
                if (chgs != null && chgs.Length > 0)
                {
                    foreach (Charges chg in chgs)
                    {
                        if (chg.StartingDate <= DateTime.Now)
                        {
                            tchg += chg.Amount + chg.Arrears;
                            ChargesPaid += chg.Paid;

                            // Additional charges line
                            if (addCharges)
                            {
                                if (chg.Amount + chg.Arrears - chg.Paid != 0)
                                {
                                    if (chargeDetails.Count > 0)
                                    {
                                        chgFound = false;
                                        for (int i = 0; i < chargeDetails.Count; i++)
                                        {
                                            if (chg.ChargeCode.ToLower().Equals(chargeDetails[i].code.ToLower()))
                                            {
                                                chgDetail = chargeDetails[i];
                                                chgDetail.amount += chg.Amount + chg.Arrears - chg.Paid;
                                                chargeDetails[i] = chgDetail;
                                                chgFound = true;
                                                break;
                                            }
                                        }
                                        if (!chgFound)
                                        {
                                            chgDetail = new ChargeDetail();
                                            chgDetail.code = chg.ChargeCode;
                                            chgDetail.amount = chg.Amount + chg.Arrears - chg.Paid;
                                            chargeDetails.Add(chgDetail);
                                        }
                                    }
                                    else
                                    {
                                        chgDetail = new ChargeDetail();
                                        chgDetail.code = chg.ChargeCode;
                                        chgDetail.amount = chg.Amount + chg.Arrears - chg.Paid;
                                        chargeDetails.Add(chgDetail);
                                    }
                                }
                            }
                        }
                    }
                    chgs = null;
                }
                Bal = xrent + xarrears + tchg - xpaid;
                if (Bal < minimum)
                    continue;
                RentPaid = ten.Paid - ChargesPaid;
                addList = null;
                additionalCharges = "";
                if (chargeDetails.Count > 0)
                {
                    if (RentDue - RentPaid != 0)
                        additionalCharges = "Rent:" + (RentDue - RentPaid).ToString("#####0.00").Trim();
                    for (int i = 0; i < chargeDetails.Count; i++)
                    {
                        ChargeCodes codes = ChargeCodes.load(chargeDetails[i].code, 1);
                        if (!additionalCharges.Trim().Equals(""))
                            additionalCharges += ", ";
                        additionalCharges += (codes != null ? codes.ChargeDescription.Left(18).Trim() + ":" : "*:") + (chargeDetails[i].amount).ToString("######.00").Trim();
                    }
                    addList = Main.WrapText(additionalCharges, 570, "Courier New", 11); 
              }
                if (first)
                {
                    Heading();
                    xbld = ten.BuildingNumber;
                    first = false;
                }
                if (!ten.BuildingNumber.Equals(xbld))
                {
                    if (totals[0] != 0)
                        PrintTotals(0);
                    xbld = ten.BuildingNumber;
                    BldFlag = false;
                }
                PrintTenant(addList);
                totals[0] += Bal;
                totals[1] += Bal;
            }
            if (totals[0] != 0)
                PrintTotals(0);
            PrintTotals(1);
            if (target == 0)
                rpt.DisplayDocument();
            else
                rpt.PrintDocument(printer);
        }

        private void PrintTenant(List<string> additionalCharges = null)
        {
            //Bal = xrent + xarrears + tchg - xpaid;
            //if (Bal >= minimum)
            //{
            if (!BldFlag)
                BldHeading();
            Payments pay = WinpmGlobal.GetLastPaid(xseq);
            string[] data1 = { xbld + xten, "tab(10)", xname, "tab(40)", xapt, "tab(44)", xtype, "tab(46)", xdue, "tab(49)", xlegal, "tab(51)", xrent.ToString("###,##0.00").PadLeft(11), "tab(63)", Bal.ToString("###,##0.00").PadLeft(11), (pay != null ? "tab(75)" : ""), (pay != null ? pay.Amount.ToString("###,##0.00").PadLeft(11) : ""), (pay != null ? "tab(87)": ""), (pay != null ? pay.DateofEntry.ToShortDateString() : "") };
            rpt.PrintPageLine(data1);
            if (addCharges && additionalCharges != null)
            {
                foreach (string item in additionalCharges)
                {
                    rpt.PrintPageLine(new String(' ', 10) +  item);
                }
            }
            
            /* Remove 2nd email address   
            if (xEmail2.Trim() != "")
            {
                string[] data2 = {"tab(75)", xEmail2.Trim()};
                rpt.PrintPageLine(data2);
            }
            */

            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
            }
            rpt.PrintPageLine("");
            if (rpt.PrintLine(2,80, null, false))
            {
                rpt.NewPage();
                Heading();
            }
            Arrearscnt++;
            //}
            //else
            //    Bal = 0;
        }

        private void PrintTotals(int n)
        {
            rpt.FontBold = true;
            switch (n)
            {
                case 0:
                    rpt.PrintPageLine("Totals for Building " + xbld,false);
                    break;
                case 1:
                    rpt.PrintPageLine("Totals for Report ", false);
                    rpt.PrintPageLine(" Units: " + Totalcnt + " - In Arrears: " + Arrearscnt, false);
                    break;
            }
            string[] data = {"tab(63)", totals[n].ToString("###,##0.00").PadLeft(11)};
            rpt.PrintPageLine(data);
            rpt.FontBold = false;
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
            }
            totals[n] = 0;
        }

        private void Heading()
        {
            rpt.FontBold = true;
            rpt.PrintPageLine("TENANT ARREARS",headerFont);
            rpt.PrintPageLine("");
            rpt.PrintPageLine(DateTime.Now.ToString("MM/dd/yyyy") + "     page " + (page++).ToString());
            rpt.FontBold=false;
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontUnderline = true;
            rpt.PrintPageLine(header);
            rpt.FontUnderline = false;
        }

        private void BldHeading()
        {
            string OwnerName = "";
            Building bld = Building.loadByBuildingNumber(xbld,clientId);
            Owners own = Owners.loadByOwnerNumber(bld.OwnerNumber,clientId);
            if (own != null)
                OwnerName = own.OwnerName;
            if (rpt.PrintPageLine(""))
            {
                rpt.NewPage();
                Heading();
                rpt.PrintPageLine("");
            }
            string[] data = {"Building Address", "tab(40)", "MDR       Owner Name"};
            rpt.FontBold = true;
            rpt.PrintPageLine(data);
            string[]data2 = {xbld + "      " + bld.Address1, "tab(37)", bld.MultipleDwellingRegistrationNumber, "tab(50)", OwnerName};
            rpt.PrintPageLine(data2);
            rpt.FontBold = false;
            rpt.PrintPageLine("");
            BldFlag = true;
        }

        private void SetTenant(Tenants tbl)
        {
            xseq = tbl.SequenceNumber;
            xten = tbl.TenantNumber;
            xname = (tbl.TenantName == null ? "" : tbl.TenantName);
            xapt = (tbl.Apartment == null ? "" : tbl.Apartment);
            xtype = (tbl.Type == null ? "" : tbl.Type);
            xlegal = (tbl.LegalCode == null ? "" : tbl.LegalCode);
            xrent = tbl.Rent;
            xarrears = tbl.Arrears;
            xpaid = tbl.Paid;
            xEmail1 = (tbl.Email1 == null ? "" : tbl.Email1);
            //xhome2 = (tbl.HomePhone2 == null ? "" : tbl.HomePhone2);
            xEmail2 = (tbl.Email2 == null ? "" : tbl.Email2);
            //xbus2 = (tbl.BusPhone2 == null ? "" : tbl.BusPhone2);
            //xpager = (tbl.Pager == null ? "" : tbl.Pager);
            xdue = (tbl.DueDate == null ? "" : tbl.DueDate);
        }
    }
}
