using ExtensionMethods;
using GemBox.Document.Drawing;
using MbcsCentral;
using Microsoft.VisualBasic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
//using PdfSharp.Pdf.Printing;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MbcsCentral.WinPM
{
    class NewTenantDocumentsPrint
    {
        PdfSharp.Pdf.PdfDocument document;
        PdfPage page;
        XGraphics gfx;
        XFont font;
        private XTextFormatter txtf;

        Spire.Pdf.PdfDocument pdf;
        PdfPageBase pg;

        Control ctrl;
        System.Windows.Controls.Image template;
        System.Windows.Controls.Image reverse;
        DocumentLayoutFields[] flds;
        //DocumentLayoutFieldMaster[] fields = null;
        List<Field> fields = new List<Field>();
        string subject = "";
        bool printing = false;
        bool printed = false;
        int emailsSentCount = 0;
        int emailsNotSentCount = 0;
        double emailCount = 0;


        struct ChargeDetail
        {
            public string code;
            public decimal amount;
        };
        List<ChargeDetail> chargeDetails = new List<ChargeDetail>();
        MbcsUtils.Parameters parms = new MbcsUtils.Parameters(GlobalShared.appPath + WinpmGlobal.WINPM_CONFIG_FILE);

        decimal Bal;
        decimal amount;
        decimal ChargesPaid;
        string[] a = { "", "", "", "", "" };
        string[] o = { "", "", "", "", "" };
        string telnumber = "";
        DateTime date;

        long clientId = GlobalShared.user.Account.clientId;
        Report rpt;
        string filename = "";
        string reverseFile = "";
        string lastType = "";
        Building building;
        Owners owner;
        Types tenType;
        XFont mainFont = new XFont("Courier New", 12);
        XFont fnt = null;
        string nam1 = null;
        string nam2 = null;
        string firstNam1 = "";
        string lastNam1 = "";
        System.DateTime newStart;
        decimal curRent = 0;
        decimal curLglRent = 0;
        decimal SC = 0;
        string lr2 = null;
        string lr1 = null;
        string tnam = null;
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
        decimal enr1;
        decimal enr2;
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
        bool AchBypass = true;
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
        private decimal LRM1;
        private decimal LRM2;
        private decimal lr;
        private decimal lra;
        private bool m_prefRent;
        private bool m_promptPay;
        private int copies = 1;
        public bool OneFlag { get; set; } = false;
        private string type = "";
        private string bldStart;
        private string bldEnd;
        private DateTime expFrom;
        private DateTime expTo;
        public string[] tenKeys { get; set; }
        private bool guidelinesUnknown = false;
        private bool useStatementOptions = false;
        private PrintQueue queue;
        bool canModify;
        double verticalOffset;
        float horizontalOffset;
        DateTime CurDate = DateTime.Now;
        DateTime BillingDate;
        Tenants[] PassedTenants;

        bool m_email = false;

        Window results;
        Grid grid;
        TextBlock txtSubject;
        ListBox lstSent;
        ListBox lstNotSent;
        TextBox txtNumberSent;
        TextBox txtNumberNotSent;
        ArrayList emailsSent = new ArrayList();
        ArrayList emailsNotSent = new ArrayList();

        private bool Debug = false;

        public bool EmailStmts { get; set; } = false;
        public bool TenantDocuments { get; set; } = false;



        public void PrintLetters(bool suppressBackground = false, bool email = false, Tenants[] tenants = null, bool ACH = false, bool EmailStmts = false)
        {
            m_email = email;
            this.EmailStmts = EmailStmts;
            this.PassedTenants = tenants;
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
            int ic = 0;
            bool riderPrinted = false;
            int addrOption = 0;
            string[] names = null;
            int p = 0;
            string x = null;

            GlobalShared.Log.Log((int)LogClass.logType.Info, "Starting Tenant Documents.", false);

            // If EMAIL, do not suppress background
            if (email)
            {
                suppressBackground = false;
                if (this.Message == null || this.EmailSubject == null || this.DisplayName == null)
                {
                    MessageBoxCustom.Show("The following items are empty:" + (this.EmailSubject == null ? "\r\n  Subject" : "") + (this.Message == null ? "\r\n  Message Body" : "") + (this.DisplayName == null ? "\r\n  Display Name" : ""));
                    return;
                }

            }
            try
            {
                ctrl = Control.load(1);
                AchBypass = ctrl.ACHBypass;
                x = DateTime.Today.ToString();
                short payto = ctrl.PayToFlag;
                if (payto == 1)
                {
                    paytoname = ctrl.PayToName;
                }
                BillingDate = ctrl.BillingDate;
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
                string xsql = "";

                if (m_prefRent)
                {
                    prefRent = new PrefRentDoc();
                }
                if (m_promptPay)
                {
                    promptPay = new PromptPayDoc();
                }

                if (copies <= 0)
                    copies = 1;
                if (tenants == null && tenants.Length == 0)
                {
                    MessageBoxCustom.Show("No Tenants have been selected.");
                    return;
                }

                if (email)
                {
                    if (Debug)
                        GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting Email Setup", false);
                    results = new Window();
                    results.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    results.Title = "EMAILING TENANT DOCUMENTS - Please Wait...";
                    results.Width = 750;
                    results.Height = 450;
                    results.Background = System.Windows.Media.Brushes.Bisque;
                    TextBlock.SetFontWeight(results, FontWeights.Bold);
                    TextBlock.SetFontSize(results, 14);

                    System.Windows.Controls.Border border = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Blue, BorderThickness = new Thickness(3), CornerRadius = new CornerRadius(5) };

                    grid = new Grid();
                    TextBlock.SetFontSize(grid, 12);
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                    border.Child = grid;

                    System.Windows.Controls.Border bdr1 = new System.Windows.Controls.Border();
                    bdr1.BorderBrush = System.Windows.Media.Brushes.Blue;
                    bdr1.BorderThickness = new Thickness(2);
                    Grid.SetRow(bdr1, 0);
                    grid.Children.Add(bdr1);

                    StackPanel pnlTop = new StackPanel();
                    pnlTop.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    pnlTop.Background = System.Windows.Media.Brushes.AliceBlue;

                    bdr1.Child = pnlTop;

                    TextBlock txt1 = new TextBlock();
                    txt1.HorizontalAlignment = HorizontalAlignment.Left;
                    txt1.VerticalAlignment = VerticalAlignment.Center;
                    txt1.TextAlignment = TextAlignment.Center;
                    txt1.FontWeight = FontWeights.Bold;
                    txt1.Background = System.Windows.Media.Brushes.AliceBlue;
                    txt1.Margin = new Thickness(10, 0, 0, 0);
                    txt1.Text = "EMAILS SENT: ";
                    pnlTop.Children.Add(txt1);

                    txtSubject = new TextBlock();
                    txtSubject.Margin = new Thickness(5, 0, 0, 0);
                    txtSubject.VerticalAlignment = VerticalAlignment.Center;
                    txtSubject.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtSubject.FontWeight = FontWeights.Bold;
                    pnlTop.Children.Add(txtSubject);

                    txtNumberSent = new TextBox();
                    txtNumberSent.Width = 48;
                    txtNumberSent.Margin = new Thickness(15, 0, 0, 0);
                    txtNumberSent.VerticalAlignment = VerticalAlignment.Center;
                    txtNumberSent.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtNumberSent.BorderBrush = System.Windows.Media.Brushes.Black;
                    txtNumberSent.BorderThickness = new Thickness(2);
                    pnlTop.Children.Add(txtNumberSent);

                    lstSent = new ListBox();
                    lstSent.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lstSent.VerticalAlignment = VerticalAlignment.Stretch;
                    lstSent.Background = System.Windows.Media.Brushes.LightBlue;
                    lstSent.ItemsSource = emailsSent;
                    Grid.SetRow(lstSent, 1);
                    grid.Children.Add(lstSent);

                    System.Windows.Controls.Border bdr2 = new System.Windows.Controls.Border();
                    bdr2.BorderBrush = System.Windows.Media.Brushes.Blue;
                    bdr2.BorderThickness = new Thickness(2);
                    Grid.SetRow(bdr2, 2);
                    grid.Children.Add(bdr2);

                    StackPanel pnlBottom = new StackPanel();
                    pnlBottom.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    pnlBottom.Background = System.Windows.Media.Brushes.MediumOrchid;

                    bdr2.Child = pnlBottom;

                    TextBlock txt2 = new TextBlock();
                    txt2.HorizontalAlignment = HorizontalAlignment.Stretch;
                    txt2.VerticalAlignment = VerticalAlignment.Stretch;
                    txt2.TextAlignment = TextAlignment.Center;
                    txt2.FontWeight = FontWeights.Bold;
                    txt2.Text = "EMAILS NOT SENT";
                    pnlBottom.Children.Add(txt2);

                    txtNumberNotSent = new TextBox();
                    txtNumberNotSent.Width = 48;
                    txtNumberNotSent.Margin = new Thickness(15, 0, 0, 0);
                    txtNumberNotSent.VerticalAlignment = VerticalAlignment.Center;
                    txtNumberNotSent.Background = System.Windows.Media.Brushes.AliceBlue;
                    txtNumberNotSent.BorderBrush = System.Windows.Media.Brushes.Black;
                    txtNumberNotSent.BorderThickness = new Thickness(2);
                    pnlBottom.Children.Add(txtNumberNotSent);

                    lstNotSent = new ListBox();
                    lstNotSent.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lstNotSent.VerticalAlignment = VerticalAlignment.Stretch;
                    //lstNotSent.ItemsSource = emailsNotSent;
                    Grid.SetRow(lstNotSent, 3);
                    grid.Children.Add(lstNotSent);

                    StackPanel pnlCommand = new StackPanel();
                    pnlCommand.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    Grid.SetRow(pnlCommand, 4);
                    grid.Children.Add(pnlCommand);

                    Button cmdPrint = new Button() { Content = "PRINT REPORT", Height = 30 };
                    cmdPrint.Click += cmdPrint_Click;
                    pnlCommand.Children.Add(cmdPrint);

                    Button cmdClose = new Button() { Content = "CLOSE", Height = 30, Margin = new Thickness(10, 0, 0, 0) };
                    cmdClose.Click += cmdClose_Click;
                    pnlCommand.Children.Add(cmdClose);

                    results.Content = border;
                }
                //foreach (Tenants tenant in tenants)
                int v = 0;

                GlobalShared.Log.Log((int)LogClass.logType.Info, "About to iterate tenants for sending emails.", false);

                for (int t = 0; t < tenants.Length; t++)
                {
                    if ((EmailStmts || ACH || TenantDocuments) && !tenants[t].SendEmail)
                        continue;
                    {
                        printIt(tenants[t], suppressBackground, email, ACH, t);
                    }
                }
                printed = true;

                if (email)
                {

                }
            }
            catch (System.Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                    msg = ex.InnerException.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.PrintLetters: " + ex.Message + "\r\n" + msg, false);

            }
            if (email)
            {
                txtSubject.Text = subject;
                txtNumberSent.Text = emailsSentCount.ToString().PadLeft(8);
                txtNumberNotSent.Text = emailsNotSentCount.ToString().PadLeft(8);
                results.Title = "EMAILS SENT";
                lstSent.ItemsSource = emailsSent;
                lstNotSent.ItemsSource = emailsNotSent;
                results.Topmost = true;
                results.Focus();
                results.Show();
                results.InvalidateVisual();
                results.UpdateLayout();

            }


        }

        public Tenants[] RetrieveTenants()
        {
            Tenants[] tenants;
            if (OneFlag)
            {
                tenants = new Tenants[0];
                string s = null;
                foreach (string key in tenKeys)
                {
                    s = key.PadRight(7);
                    Tenants[] ten = Tenants.load(s.Substring(0, 3), s.Substring(3, 4).Trim(), 1);
                    if (ten != null && ten.Length > 0)
                    {
                        Array.Resize<Tenants>(ref tenants, tenants.Length + 1);
                        tenants[tenants.Length - 1] = ten[0];
                    }
                }
            }
            else
            {
                string xsql = "SELECT * FROM " + Tenants.TABLE + " WHERE [" + Tenants.SQL_COL_BUILDING_NUMBER + "] BETWEEN '" + bldStart + "' AND '" + bldEnd + "'";
                if (UseLeaseDates)
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
                xsql += " AND " + Tenants.SQL_COL_CLIENT_ID + "= 1";
                xsql = xsql + " ORDER BY [" + Tenants.SQL_COL_BUILDING_NUMBER + "], [" + Tenants.SQL_COL_TENANT_NUMBER + "]";
                tenants = Tenants.Query(xsql, GlobalShared.NULL_DATE);
                if (tenants != null) GlobalShared.Log.Log((int)LogClass.logType.Info, "Number of Tenants Selected: " + tenants.Length, false);
                GlobalShared.Log.Log((int)LogClass.logType.Info, xsql, false);
                if (tenants != null && tenants.Length > 0)
                {
                }
                else
                {
                    MessageBox.Show("No tenants selected.");
                }
            }
            if (tenants != null && tenants.Length > 0)
            {
                foreach (Tenants ten in tenants)
                {
                    if (!ten.Email1.Trim().Equals("") || !ten.Email2.Trim().Equals(""))
                    {
                        ten.CanSendEmail = true;
                        ten.SendEmail = true;
                        if (!ten.Email1.Trim().Equals(""))
                        {
                            ten.UseEmail1 = true;
                            ten.IsEmail1 = true;
                        }
                        else
                            ten.UseEmail2 = true;
                        if (!ten.Email2.Trim().Equals(""))
                            ten.IsEmail2 = true;
                    }

                }
            }
            return tenants;
        }

        private void printIt(Tenants ten, bool suppressBackground = false, bool email = false, bool ACH = false, int t = -1)
        {
            int ic = 0;
            tnam = ten.TenantName;
            nam1 = tnam;
            nam2 = "";
            bool riderPrinted = false;
            string additionalCharges = "";
            List<string> addList = new List<string>();
            decimal RentDue;
            decimal RentPaid;
            string xbld = "";
            ChargeCodes xcodes = null;
            //rpt = new Report(DocumentName);
            //rpt.TopMargin = 0;
            //rpt.BottomMargin = 0;
            //rpt.LeftMargin = 0;
            Building tblBuilding = null;
            short telflag = 0;


            if (EmailStmts || (ACH && !AchBypass))

            {
                Control ctrl = Control.load(GlobalShared.user.Client.clientId);
                if (ctrl == null)
                {
                    MessageBoxCustom.Show("Unable to retrieve billing dates. Cannot continue.");
                    return;
                }
                ArrayList list = new ArrayList();
                list.Add(ten.SequenceNumber);
                try
                {
                    filename = Path.GetTempPath();
                    filename += "Statement_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
                }
                catch (IOException ex)
                {
                    string message = ex.Message;
                    if (ex.InnerException != null)
                        message += "\r\n\r\n" + ex.InnerException.Message;
                    MessageBoxCustom.Show("There was an error creating attachment file: " + message);
                    return;
                }
                StmtLaser stmtLaser = new StmtLaser("000", "0000", "999", "ZZZZ", ctrl.ClosingDate, ctrl.BillingDate, false) { OneByOne = list, PdfFile = filename, ACHonly = true, EmailStmts = this.EmailStmts, iTen = t };
                stmtLaser.PassedTenants = this.PassedTenants;
                stmtLaser.Process();
            }
            else
                RetrieveDocument(DocumentName, suppressBackground);

            //if (m_duplexEnabled)
            //{
            //    rpt.Duplex = true;
            // }

            // New name resolution routine
            string[,] names = new string[3, 3];
            WinpmGlobal.resolveNames(ref names, ten);
            nam1 = Strings.Trim(names[0, 0]) + " " + Strings.Trim(names[0, 1]);
            firstNam1 = names[0, 0];
            lastNam1 = names[0, 1];
            nam2 = Strings.Trim(names[1, 0]) + " " + Strings.Trim(names[1, 1]);
            moveindate = DateAndTime.Now;
            oldLease = DateAndTime.DateSerial(2000, 1, 1);
            if (ten.LeaseStartDate > GlobalShared.NULL_DATE)
            {
                oldLease = ten.LeaseStartDate;
            }
            ChargeDetail chgDetail;
            bool chgFound = false;
            chargeDetails.Clear();
            addList.Clear();
            additionalCharges = "";
            Bal = ten.Rent + ten.Arrears - ten.Paid;
            RentDue = ten.Rent + ten.Arrears;
            ChargesPaid = 0;
            Charges[] chgs = Charges.loadBySeqNum(ten.SequenceNumber);
            if (chgs != null && chgs.Length > 0)
            {
                foreach (Charges tblCharges in chgs)
                {
                    if (tblCharges.StartingDate <= DateTime.Now)
                    {
                        if (tblCharges.EndingDate <= GlobalShared.NULL_DATE || tblCharges.EndingDate > DateTime.Now)
                        {
                            Bal = Bal + tblCharges.Amount + tblCharges.Arrears; //- tblCharges.Paid;
                            ChargesPaid += tblCharges.Paid;
                            if (tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid != 0)
                            {
                                if (chargeDetails.Count > 0)
                                {
                                    chgFound = false;
                                    for (int i = 0; i < chargeDetails.Count; i++)
                                    {
                                        if (tblCharges.ChargeCode.ToLower().Equals(chargeDetails[i].code.ToLower()))
                                        {
                                            chgDetail = chargeDetails[i];
                                            chgDetail.amount += tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                            chargeDetails[i] = chgDetail;
                                            chgFound = true;
                                            break;
                                        }
                                    }
                                    if (!chgFound)
                                    {
                                        chgDetail = new ChargeDetail();
                                        chgDetail.code = tblCharges.ChargeCode;
                                        chgDetail.amount = tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                        chargeDetails.Add(chgDetail);
                                    }
                                }
                                else
                                {
                                    chgDetail = new ChargeDetail();
                                    chgDetail.code = tblCharges.ChargeCode;
                                    chgDetail.amount = tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                    chargeDetails.Add(chgDetail);
                                }
                            }
                        }
                    }
                }

            }
            RentPaid = ten.Paid - ChargesPaid;
            if (chargeDetails.Count > 0)
            {
                if (RentDue - RentPaid != 0)
                    additionalCharges = "Rent:" + (RentDue - RentPaid).ToString("#####0.00").Trim();
                for (int i = 0; i < chargeDetails.Count; i++)
                {
                    xcodes = ChargeCodes.load(chargeDetails[i].code, 1);
                    if (!additionalCharges.Trim().Equals(""))
                        additionalCharges += ", ";
                    additionalCharges += (xcodes != null ? xcodes.ChargeDescription.Left(18).Trim() + ":" : "*:") + (chargeDetails[i].amount).ToString("######.00").Trim();
                }
            }
            addList = Main.WrapText(additionalCharges, 190, "Courier New", 11); if (!xbld.Equals(ten.BuildingNumber))
            {
                tblBuilding = Building.loadByBuildingNumber(ten.BuildingNumber, clientId);
                if (tblBuilding != null)
                {
                    if (retAdd < 2 || telflag == 0)
                    {
                        Owners tblOwner = Owners.loadByOwnerNumber(tblBuilding.OwnerNumber, clientId);
                        if (tblOwner != null)
                        {
                            if (retAdd < 2)
                            {
                                o[1] = tblOwner.OwnerName;
                                if (retAdd == 0)
                                {
                                    o[2] = tblOwner.Address1;
                                    o[3] = tblOwner.Address2;
                                    o[4] = "";
                                }
                            }
                            if (telflag == 0)
                                telnumber = tblOwner.Telephone;
                        }
                    }
                }
                xbld = ten.BuildingNumber;
            }
            for (int j = 0; j < a.Length; j++)
                a[j] = "";
            a[0] = WinpmGlobal.reverse_names(ten.TenantName);
            a[1] = a[0];
            if (ten.AlternateBillingAddressFlag)
            {
                a[1] = ten.AltBillName.Trim();
                a[0] = a[1];
                a[2] = ten.AltBillAdd1.Trim();
                a[3] = ten.AltBillAdd2.Trim();
                a[4] = ten.AltBillAdd3.Trim();
            }
            else
            {
                a[3] = tblBuilding.Address1.Trim();
                a[4] = tblBuilding.Address2.Trim();
            }
            if (a[2].Trim() == "")
            {
                a[2] = a[1];
                a[1] = "";
            }
            moveindate = ten.MoveinDate;

            System.Windows.Controls.PrintDialog dlg = new System.Windows.Controls.PrintDialog();

            rpt = new Report(DocumentName);
            rpt.TopMargin = 0;
            rpt.BottomMargin = 0;
            rpt.LeftMargin = 0;
            if (m_duplexEnabled)
            {
                rpt.Duplex = true;
            }

            for (ic = 1; ic <= copies; ic++)
            {
                if (pdf != null)
                    PrintFields(ten);

                if (m_duplexEnabled)
                {
                    queue.DefaultPrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
                    queue.UserPrintTicket.Duplexing = Duplexing.TwoSidedLongEdge;
                }

                dlg.PrintQueue = queue;
                // Document 
                GlobalShared.Log.Log((int)LogClass.logType.Info, "About to print Tenant Dcoument: " + ten.BuildingTenantNumber, false);
                string[] ReportInfo = { EmailStmts ? "Statement_" : "TenantDocumentEmail" };
                if ((!ACH && !EmailStmts && pdf != null))
                {
                    try
                    {
                        filename = Path.GetTempPath();
                        filename += ReportInfo[0].Replace(" ", "") + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
                    }
                    catch (IOException ex)
                    {
                        string message = ex.Message;
                        if (ex.InnerException != null)
                            message += "\r\n\r\n" + ex.InnerException.Message;
                        MessageBoxCustom.Show("There was an error creating attachment file: " + message);
                        return;
                    }
                }
                if (pdf != null || (filename != null && !filename.Trim().Equals("")))
                {
                    if (!ACH && !EmailStmts)
                    {
                        pdf.SaveToFile(filename);
                        System.Threading.Thread.Sleep(500);
                        //rpt.PageOrientation = (int)System.Printing.PageOrientation.Landscape;
                        rpt.AddPdf(filename);

                    }
                }
                else
                    filename = null;
                if (email)
                {
                    if (email && ten.SendEmail && (!ten.Email1.Trim().Equals("") || !ten.Email2.Trim().Equals("")))
                    {
                        if (!Main.CheckEmailDirectories())
                            return;
                        // Insert fields in subject and message
                        subject = this.EmailSubject;
                        string message = this.Message;
                        string displayname = this.DisplayName;
                        string PaymentDate = new DateTime(BillingDate.Year, BillingDate.Month, (ten.PaymentDay > 0 ? ten.PaymentDay : 1)).ToShortDateString();
                        try
                        {
                            for (int i = 0; i < WinpmGlobal.NoticeInsertionFieldNames.Length; i++)
                            {
                                if (!ACH)
                                {
                                    InsertEmailField(ref subject, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, Bal.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                    InsertEmailField(ref message, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, Bal.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                    InsertEmailField(ref displayname, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, Bal.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                }
                                else
                                {
                                    InsertEmailField(ref subject, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, ten.Payment.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                    InsertEmailField(ref message, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, ten.Payment.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                    InsertEmailField(ref displayname, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], ten.Apartment, ten.Payment.ToString("$###,###0.00"), ten.BuildingTenant, PaymentDate);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBoxCustom.Show("Error inserting merge fields: " + ex.Message);

                        }
                        if (this.Test)
                        {
                            if (MbcsCentral.Email.Utils.TestEmail(FromEmail, ten.BuildingNumber, ten.TenantNumber, ten.TenantName, Smtp, FromEmail, displayname, ten.UseEmail2 ? ten.Email2 : ten.Email1, subject, message, "", filename, false, ReturnReceipt))
                            {
                                emailsSentCount++;
                                emailsSent.Add(ten.BuildingTenantNumber + "|" + ten.TenantName + "|" + (string)(ten.UseEmail2 ? ten.Email2 : ten.Email1) + "|" + Bal.ToString("$#######.00"));
                                //lstSent.Items.Add(emailsSent[emailsSent.Count - 1]);
                                lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                string saveFileName = GlobalShared.appPath + "Emails\\Out\\" + String.Format(GlobalShared.mailFilename, WinpmGlobal.EmailTypeFilenames[(int)WinpmGlobal.EmailTypes.TenantDirect] + "_", ten.SequenceNumber, 0, DateTime.Now, "pdf");
                                string rc = MbcsCentral.Email.Utils.SaveEmail(FromEmail, displayname, ten.UseEmail2 ? ten.Email2 : ten.Email1, subject, message, saveFileName.Replace(".pdf", ".txt"), date.ToShortDateString(), ReturnReceipt);
                                if (rc != null)
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Tenant Document Email: " + rc, false);
                                }
                            }
                            else
                            {
                                emailsNotSentCount += 1;
                                emailsNotSent.Add(ten.BuildingTenantNumber + "| " + ten.TenantName + "| " + (string)(ten.UseEmail2 ? ten.Email2 : ten.Email1) + "|" + Bal.ToString("$#######.00"));
                                lstNotSent.ScrollIntoView(emailsSent[emailsNotSent.Count - 1]);
                            }
                        }
                        else
                        {
                            ArrayList list = null;
                            // Send to Email 1
                            if (ten.UseEmail1)
                            {
                                if (this.EmailBatchLimit > 0 && emailCount > this.EmailBatchLimit - 1 && this.EmailBatchDelay > 0)
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.Info, "TenantDocumentsPrint: Reached " + this.EmailBatchLimit + " batch limit. Sleeping for " + this.EmailBatchDelay * 1000 + " seconds", false);
                                    System.Threading.Thread.Sleep((int)EmailBatchDelay * 1000);
                                    emailCount = 0;
                                }
                                // if (File.Exists(filename))
                                //{
                                    list = MbcsCentral.Email.Utils.SendEmail(Smtp, FromEmail, displayname, ten.Email1, subject, message, "", filename, ten.UseEmail2, ReturnReceipt, ReplyTo);

                                    if (list == null || list.Count == 0)
                                    {
                                        emailsSentCount++;
                                        emailsSent.Add(ten.BuildingTenantNumber + " | " + ten.TenantName + " | " + ten.Email1 + "|");
                                        lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                        string saveFileName = GlobalShared.appPath + "Emails\\Out\\" + String.Format(GlobalShared.mailFilename, WinpmGlobal.EmailTypeFilenames[(int)WinpmGlobal.EmailTypes.TenantDirect] + "_", ten.SequenceNumber, 0, DateTime.Now, "pdf");

                                        string rc = MbcsCentral.Email.Utils.SaveEmail(FromEmail, displayname, ten.Email1, subject, message, saveFileName.Replace(".pdf", ".txt"), DateTime.Now.ToShortDateString(), ReturnReceipt);
                                        if (rc != null)
                                        {
                                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Late Notice Email: " + rc, false);
                                        }
                                        emailCount++;
                                    }
                                    else
                                    {
                                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Email for <" + ten.Email1 + "> not sent:\r\n", false);
                                        foreach (string s in list)
                                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, s, false);
                                        GlobalShared.Log.Log((int)LogClass.logType.Info, "", false);
                                        emailsNotSentCount++;
                                        emailsNotSent.Add(ten.BuildingTenantNumber + " | " + ten.TenantName + " | " + ten.Email1 + " | " + Bal.ToString("$#######.00") + "|" + list[0].ToString());
                                        lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);
                                    }
                             }
                            /*
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Email for <" + ten.Email1 + "> not sent:\r\n", false);
                                    emailsNotSentCount++;
                                    emailsNotSent.Add(ten.BuildingTenantNumber + " | " + ten.TenantName + " | " + ten.Email1 + " | " + Bal.ToString("$#######.00"));
                                    lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);

                                }
                            */
                            
                            // Send to Email 2
                            if (ten.UseEmail2)
                            {
                                if (this.EmailBatchLimit > 0 && emailCount > this.EmailBatchLimit && this.EmailBatchDelay > 0)
                                {
                                    System.Threading.Thread.Sleep((int)EmailBatchDelay * 1000);
                                    emailCount = 0;
                                }
                                //if (File.Exists(filename))
                                //{
                                    list = MbcsCentral.Email.Utils.SendEmail(Smtp, FromEmail, displayname, ten.Email2, subject, message, "", filename, false, ReturnReceipt);
                                    if (list == null || list.Count == 0)
                                    {
                                        emailsSentCount++;
                                        emailsSent.Add(ten.BuildingTenantNumber + "|" + ten.TenantName + "|" + ten.Email2 + "|" + Bal.ToString("$#######.00"));
                                        lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                        string saveFileName = GlobalShared.appPath + "Emails\\Out\\" + String.Format(GlobalShared.mailFilename, WinpmGlobal.EmailTypeFilenames[(int)WinpmGlobal.EmailTypes.TenantDirect] + "_", ten.SequenceNumber, 0, DateTime.Now, "pdf");

                                        string rc = MbcsCentral.Email.Utils.SaveEmail(FromEmail, displayname, ten.Email2, subject, message, saveFileName.Replace(".pdf", ".txt"), DateTime.Now.ToShortDateString(), ReturnReceipt);
                                        if (rc != null)
                                        {
                                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Late Notice Email: " + rc, false);
                                        }
                                        emailCount++;
                                    }
                                    else
                                    {
                                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Emai2 for <" + ten.Email1 + "> not sent:\r\n", false);
                                        foreach (string s in list)
                                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, s, false);
                                        GlobalShared.Log.Log((int)LogClass.logType.Info, "", false);
                                        emailsNotSentCount++;
                                        emailsNotSent.Add(ten.BuildingTenantNumber + "| " + ten.TenantName + "| " + ten.Email2 + "| " + Bal.ToString("$#######.00") + "|" + list[0].ToString());
                                        lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);
                                    }
                                /* }
                                else
                                {
                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Email for <" + ten.Email1 + "> not sent:\r\n", false);
                                    emailsNotSentCount++;
                                    emailsNotSent.Add(ten.BuildingTenantNumber + " | " + ten.TenantName + " | " + ten.Email1 + " | " + Bal.ToString("$#######.00"));
                                    lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);

                                }
                                */
                            }

                        }
                    }
                    if (EmailStmts || (ACH && !AchBypass))
                    {
                        if (File.Exists(filename))
                        {
                            try
                            {
                                File.Delete(filename);
                            }
                            catch(Exception ex)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to delete file <" + filename + ">. Error:<"+ex.ToString()+">\r\n", false);

                            }
                        }
                        var xpsFile = filename.Replace(".pdf", ".xps");
                        if (File.Exists(xpsFile))
                        {
                            try
                            {
                                File.Delete(xpsFile);
                            }
                            catch(Exception ex)
                            {
                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to delete file <" + xpsFile + ">. Error:<" + ex.ToString() + ">\r\n", false);
                            }
                        }
                    }
                }
                else
                {
                    rpt.PrintDocument(queue);
                    if (m_duplexEnabled)
                        queue.DefaultPrintTicket.Duplexing = Duplexing.OneSided;
                }
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex) { }

                }

                rpt = null;
            }
        }



        public void PrintFields(Tenants ten)
        {
            Field docField = null;

            Control ctrl = Control.load(1);
            retAdd = ctrl.ReturnAddressFlag;

            //**********************************
            //* Field substitution subroutines *
            //**********************************
            int yr = 0;
            int dY = 0;
            int mn = 0;
            int yyear = 0;
            int ymonth = 0;
            int addrOption = 0;
            if (!bld.Equals(ten.BuildingNumber))
                bld = Getbld(ten, ref owner);

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
            rent = ten.Rent;
            curRent = rent;

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
            //GlobalShared.Log.Log((int)LogClass.logType.Info, "Lease Expiration", false);
            if (ten.LeaseExpirationDate > GlobalShared.NULL_DATE)
            {
                //GlobalShared.Log.Log((int)LogClass.logType.Info, "Get History", false);
                bool useDC = (bool)(tenType == null ? false : tenType.UseDC);

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
            //GlobalShared.Log.Log((int)LogClass.logType.Info, "Additional Charges", false);
            // TOTAL ADDITIONAL CHARGES
            xr = 0;
            ac = 0;
            SC = 0;

            // TOTAL NEW RENT 
            nr1 = curLglRent + ri1 + ria + lra1 + capImp;
            nr2 = curLglRent + ri2 + ria + lra2 + capImp;

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

                    case "DateMonth":
                        // DATE MONTH
                        SetField(docField, "DateMonth", CurDate.ToString("MM"));
                        break;

                    case "DateDay":
                        //DATE DAY
                        SetField(docField, "DateDay", CurDate.ToString("dd"));
                        break;

                    case "DateYear":
                        // DATE YEAR
                        SetField(docField, "DateYear", CurDate.ToString("yyyy"));
                        break;

                    case "TenantName":
                        // NAME
                        SetField(docField, "TenantName", nam1);
                        break;

                    case "TenFirst":
                        // FIRST NAME
                        SetField(docField, "TenFirst", firstNam1);
                        break;

                    case "TenLast":
                        // LAST NAME
                        SetField(docField, "TenLast", lastNam1);
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
                        if (guidelinesUnknown)
                            SetField(docField, "colCchk", "X");
                        break;

                    // ONE YEAR PERCENT
                    case "colC1p":
                        string c = "";
                        if (minTaken)
                            c = "*****";
                        else
                            c = one.ToString("#0.00").PadLeft(5);
                        SetField(docField, "colC1p", c);
                        break;

                    // TWO YEAR PERCENT
                    case "colC2p":
                        if (minTaken)
                            c = "*****";
                        else
                            c = two.ToString("#0.00").PadLeft(5);
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


                    // MINIMUM INCREASE MESSAGE
                    case "MsgArea":
                        if (minTaken)
                        {
                            SetField(docField, "MsgArea", "***RENT GUIDELINES ORDER #44 ALLOWS A MINIMUM INCREASE OF " + minimumIncrease1.ToString("$###") + " (1 YEAR LEASE) OR ");
                            SetField(docField, "MsgArea", minimumIncrease2.ToString("$###") + " (2 YEAR LEASE)", 1);
                            // IF TENANCY HAS COMMENCED SIX (6) OR MORE YEARS AGO"
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
                        if (ProposedRent1 > 0)
                            SetField(docField, "prefRent1", ProposedRent1.ToString("##,##0.00").PadLeft(9));
                        break;
                    case "prefRent2":
                        if (ProposedRent2 > 0)
                            SetField(docField, "prefRent2", ProposedRent2.ToString("##,##0.00").PadLeft(9));
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
            }
        }

        private string Getbld(Tenants ten, ref Owners owner)
        {
            string bld = null;
            building = Building.loadByBuildingNumber(ten.BuildingNumber, 1);
            if (building != null)
            {
                bld = building.BuildingNumber;
                owner = Owners.loadByOwnerNumber(building.OwnerNumber, 1);
            }
            return bld;
        }

        private decimal NewRent(decimal r, decimal p, int years, System.DateTime leaseDate, System.DateTime moveindate, decimal increaseRent, decimal minimumIncrease)
        {
            decimal functionReturnValue = 0;
            DateTime d1 = default(DateTime);
            DateTime d2 = default(DateTime);

            minTaken = false;
            if (leaseDate > GlobalShared.NULL_DATE)
            {
                d1 = leaseDate.AddDays(1);
                d2 = moveindate.AddYears(years);
            }
            if (useMinimumRent && leaseDate > GlobalShared.NULL_DATE && r < increaseRent && d2 <= d1)
            {
                functionReturnValue = minimumIncrease;
                minTaken = true;
            }
            else
            {
                functionReturnValue = (r * p) / 100;
            }
            return functionReturnValue;

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



        private void SetField(Field doc, string name, string value, int linefeeds = 0, System.Windows.Media.Brush fontcolor = null, bool fontbold = false)
        {
            try
            {
                if (doc != null)
                {
                    PdfBrush b = null;
                    PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
                    PdfTrueTypeFont xfont = new PdfTrueTypeFont(new System.Drawing.Font(doc.FontFamily.ToString(), (float)doc.FontSize + 2, System.Drawing.FontStyle.Regular));
                    if (fontcolor != null)
                        b = new PdfSolidBrush((System.Drawing.Color)new System.Drawing.ColorConverter().ConvertFromString(new System.Windows.Media.BrushConverter().ConvertToString(fontcolor)));

                    else if (doc.Item.Class != null && !doc.Item.Class.Trim().Equals(""))
                    {
                        try
                        {
                            ColorFont.FontInfo thisFont = ColorFont.FontInfo.DeSerialize(doc.Item.Class);
                            b = new PdfSolidBrush((System.Drawing.Color)new System.Drawing.ColorConverter().ConvertFromString(new System.Windows.Media.BrushConverter().ConvertToString(thisFont.BrushColor)));

                        }
                        catch (Exception ex) { }
                    }
                    // Convert to XBrush
                    if (b == null)
                        b = new PdfSolidBrush(System.Drawing.Color.Black);

                    doc.Content = value;
                    double width = doc.Item.Size * 5;
                    double height = 10;
                    float left = (float)doc.Item.Left + (float)2.46 + ((float)doc.Item.Left * (float)horizontalOffset) - ((float)doc.Item.Left * (float).15);
                    float top = (float)doc.Item.Top + (float)(m_email ? 0 : 4.5) + ((float)doc.Item.Top * (float)verticalOffset) - ((float)doc.Item.Top * (float).05);
                    RectangleF rect = new RectangleF((float)left, (float)top, (float)width, (float)height);
                    PdfFont fn = new PdfFont(PdfFontFamily.Courier, (float)doc.FontSize);

                    pg.Canvas.DrawString(value, xfont, b, (float)left, (float)top, format);
                    //txtf.DrawString(value, xfont, brush, rect);
                }
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "   LeaseLetter.SetField (" + name + "): " + ex.Message, false);

            }

        }

        void InsertEmailField(ref string content, string field, string TenantName, string OwnerName, string Apartment, string Balance, string TenantNumber, string PaymentDate)
        {
            if (field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.AmountDue]))
                content = content.Replace(field, Balance);
            else if (field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.Apt]))
                content = content.Replace(field, Apartment);
            else if (field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.OwnerName]))
                content = content.Replace(field, OwnerName);
            else if ((field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.TenantName])))
                content = content.Replace(field, TenantName);
            else if ((field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.TenantNumber])))
                content = content.Replace(field, TenantNumber);
            else if ((field.Equals(WinpmGlobal.NoticeInsertionFieldNames[(int)WinpmGlobal.NoticeInsertionFields.PaymentDate])))
                content = content.Replace(field, PaymentDate);

        }


        private void RetrieveDocument(string name, bool suppressBackground = false)
        {
            document = null;
            page = null;
            gfx = null;

            //document = new PdfSharp.Pdf.PdfDocument();
            //page = document.AddPage();
            //PdfPageSettings pdfPageSettings = new PdfPageSettings(0f);
            //gfx = XGraphics.FromPdfPage(page);
            //font = new XFont("Times New Roman", 10, XFontStyle.Bold);
            //txtf = new XTextFormatter(gfx);
            //gfx.MUH = PdfFontEncoding.Unicode;
            //gfx.MFEH = PdfFontEmbedding.Default;
            DocumentLayoutList doc = DocumentLayoutList.loadByDocument(name);
            if (doc != null)
            {
                string filepath = doc.DocumentLocation;
                if (doc.DocumentLocation.StartsWith("*", StringComparison.Ordinal))
                {
                    string path = GlobalShared.appPath;
                    if (path.EndsWith("\\", StringComparison.Ordinal))
                        path = path.Substring(0, path.Length - 1);
                    filepath = filepath.Replace("*", path + "\\Files");
                }
                try
                {
                    if (!suppressBackground)
                    {/*
                        System.Drawing.Image img = System.Drawing.Image.FromFile(filepath);
                        gfx.DrawImage(img, new Rectangle(0, 0, 700, 1040));
                        */
                        pdf = new Spire.Pdf.PdfDocument();
                        PdfMargins pdfMargin = new PdfMargins();
                        pdfMargin.Left = 0f;
                        pdfMargin.Top = 0f;
                        pg = pdf.Pages.Add(new SizeF(700f, 1040), pdfMargin);
                        Spire.Pdf.Graphics.PdfImage pdfimage;
                        pdfimage = Spire.Pdf.Graphics.PdfImage.FromFile(filepath);
                        float width = (float)pdfimage.Width * 0.75f;
                        float height = pdfimage.Height * 0.75f;
                        float x = (pg.Canvas.ClientSize.Width - width) / 2;

                        pg.Canvas.DrawImage(pdfimage, 0, .6f, 690, 1040);

                    }

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
                        //MessageBoxCustom.Show("Unable to retrieve document fields.");
                        return;
                    }
                }
                catch (SystemException ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Could not load Document: " + filepath, true);
                    return;
                }
            }
            else
                return;
        }

        void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            results.Close();
        }

        void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string[] s;

            if (emailsSent.Count > 0 || emailsNotSent.Count > 0)
            {
                Report rpt = new Report("Tenant Document Emails", "Courier New", 12) { LeftMargin = (Single)7, TopMargin = (Single)5, RightMargin = (Single)5, BottomMargin = (Single)50, BottomOffset = (Single)(50) };
                rpt.LeftMargin = 10;
                rpt.TopMargin = 10;
                rpt.PrintPageLine("TENANT DOCUMENT EMAIL REPORT: " + subject);
                rpt.PrintPageLine(DateTime.Now.ToShortDateString());
                rpt.PrintPageLine(""); rpt.PrintPageLine("");
                rpt.PrintPageLine("Tenant#    Name                            Email Address");
                rpt.PrintPageLine("");
                rpt.PrintPageLine("SENT DOCUMENTS - Count: " + emailsSent.Count.ToString());
                rpt.PrintPageLine("============");
                if (emailsSent.Count > 0)
                {
                    for (int i = 0; i < emailsSent.Count; i++)
                    {
                        {
                            s = ((string)emailsSent[i]).Split('|');
                            if (s.Length >= 4)
                            {
                                rpt.PrintPageLine(s[0].PadRight(9), false);
                                rpt.PrintPageLine(s[1].PadRight(32), false);
                                rpt.PrintPageLine(s[2].PadRight(32));
                                ;
                            }
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.PrintPageLine(""); rpt.PrintPageLine("");

                rpt.PrintPageLine("DOCUMENTS NOT SENT - Count: " + emailsNotSent.Count.ToString());
                rpt.PrintPageLine("============");
                if (emailsNotSent.Count > 0)
                {
                    for (int i = 0; i < emailsNotSent.Count; i++)
                    {
                        {
                            s = ((string)emailsNotSent[i]).Split('|');
                            if (s.Length >= 5)
                            {
                                rpt.PrintPageLine(s[0].PadRight(9), false);
                                rpt.PrintPageLine(s[1].PadRight(32), false);
                                rpt.PrintPageLine(s[2].PadRight(21));
                                if (s.Length == 5)
                                    rpt.PrintPageLine(s[4]);
                                if (s.Length == 6)
                                    rpt.PrintPageLine(s[5]);
                                rpt.PrintPageLine("");
                            }
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.DisplayDocument();
            }
        }


        // Properties
        public BitmapImage image { get; set; }
        public string DocumentName { get; set; }
        public bool Email { get; set; } = false;
        public string BldStart
        {
            set { bldStart = value; }
        }
        public string BldEnd
        {
            set { bldEnd = value; }
        }
        public DateTime LetterDate
        {
            set { CurDate = value; }
        }
        public string Type
        {
            set { type = value; }
        }

        public string[] TypeList { get; set; }
        public bool UseLeaseDates { get; set; }
        public DateTime ExpFrom
        {
            set { expFrom = value.Date; }
        }
        public DateTime ExpTo
        {
            set { expTo = value.Date; }
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
            set { OneFlag = value; }
        }
        public decimal OneYear
        {
            set { one = value; }
        }
        public decimal TwoYear
        {
            set { two = value; }
        }
        public bool UseMinimumRent
        {
            set { useMinimumRent = value; }
        }
        public decimal MinimumRent
        {
            set { m_minimumRent = value; }
        }
        public int MinimumYears
        {
            set { m_minimumYears = value; }
        }
        public decimal LowRentOneYear
        {
            set { LRM1 = value; }
        }
        public decimal LowRentTwoYear
        {
            set { LRM2 = value; }
        }
        public bool DuplexEnabled
        {
            set { m_duplexEnabled = value; }
        }
        public bool PrefRent
        {
            set { m_prefRent = value; }
        }
        public bool PromptPay
        {
            set { m_promptPay = value; }
        }
        public bool IndividualFlag
        {
            set { OneFlag = value; }
        }
        public string[] TenantKeys
        {
            set { tenKeys = value; }
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
            set { filename = value; }
        }
        public string ReverseDocument
        {
            set { reverseFile = value; }
        }
        public bool UseTypeLease { get; set; }

        // Email properties
        public Email.SmtpConfig Smtp
        {
            get; set;
        }
        public string FromEmail { get; set; }
        public string DisplayName { get; set; }
        public string EmailSubject { get; set; }
        public string Message { get; set; }
        public bool ReturnReceipt { get; set; }
        public string ReplyTo { get; set; }
        public bool Test { get; set; }

        public double EmailBatchLimit { get; set; }
        public double EmailBatchDelay { get; set; }


    }
}
