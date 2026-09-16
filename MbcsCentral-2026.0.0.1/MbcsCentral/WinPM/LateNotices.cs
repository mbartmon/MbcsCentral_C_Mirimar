using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Controls;
using ExtensionMethods;

namespace MbcsCentral.WinPM
{
    class LateNotices : ReportBase
    {
        struct ChargeDetail
        {
            public string code;
            public decimal amount;
            public int key;
        };
        List<ChargeDetail> chargeDetails = new List<ChargeDetail>();

        double TopY;
        long clientId = GlobalShared.user.Account.clientId;
        Report rpt = new Report("LateNotices", "Courier New", 12);
        string telnumber = "";
        Tenants[] tens;
 		decimal Bal;
        string[] a = { "", "", "", "", "" };
        string[] o = { "", "", "", "", "" };
        int ly;

        string startTenant;
        string startBuilding;
        string endTenant;
        string endBuilding;
        string legal1 = "";
        string legal2 = "";
        string LateFeeDesc = "";
        string LateFeeCode = "";
        int topMargin;
        double leftMargin;
        float fishmanTopOffset = 9;
        float fishmanLeftOffset = (float)2.5;
        decimal amount;
        DateTime date;
        int lasertype;
        // EMAIL 
        bool email = false;
        bool AlsoPrint = false;
        Window results;
        Grid grid;
        ListBox lstSent;
        ListBox lstNotSent;
        TextBox txtNumberSent;
        TextBox txtNumberNotSent;
        ArrayList emailsSent = new ArrayList();
        ArrayList emailsNotSent = new ArrayList();

        private bool Debug = false;

        System.Printing.PrintQueue printer;

        public LateNotices() { }
        public LateNotices(string startBuiding, string startTenant, string endBuilding, 
                            string endTenant, string legal1, string legal2,int topMargin, 
                            double leftMargin, decimal amount, DateTime date, int laserType,
                            bool email = false, Email.SmtpConfig smtp = null,
                            string fromEmail = "", string subject = "", string message = "")
        {
            this.startBuilding = startBuiding;
            this.startTenant = startTenant;
            this.endBuilding = endBuilding;
            this.endTenant = endTenant;
            this.legal1 = legal1;
            this.legal2 = legal2;
            this.topMargin = topMargin;
            this.leftMargin = leftMargin;
            this.amount = amount;
            this.date = date;
            this.lasertype = laserType;
            this.email = email;
            this.Smtp = smtp;
            this.FromEmail = fromEmail;
            this.EmailSubject = subject;
            this.Message = message;

        }

        public override void Process()
        {
            PrintNotices(null);
        }

        public void PrintNotices(Tenants[] tenants)
	    {
            object d = null;
            Main.parms.getItem("debug", ref d);
            if (d != null && d.ToString().Equals("true"))
                Debug = true;

            if (Debug)
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting PrintNotices", false);

		    short retAdd;
            short telflag;
	        string xsql;
		    string xbld = "";
		    bool align;
            Building tblBuilding = null;
		    align = true;
            Control tblControl = Control.load(clientId);
            bool printed = false;

            rpt.TopMargin = 10;
            rpt.LeftMargin = 0;
            rpt.BottomMargin = 0;
            rpt.Borderless = true;
		    retAdd = tblControl.ReturnAddressFlag;
		    switch (retAdd) {
			    case 0:
				    //Owner Name/Address
				    break;
			    case 1:
				    //Owner Name/This Address
				    o[2] = tblControl.ReturnAddressA1;
				    o[3] = tblControl.ReturnAddressA2;
				    o[4] = tblControl.ReturnAddressA3;
				    break;
			    case 2:
				    //This Name/This Address
				    o[1] = tblControl.ReturnAddressBname;
				    o[2] = tblControl.ReturnAddressB1;
				    o[3] = tblControl.ReturnAddressB2;
				    o[4] = tblControl.ReturnAddressB3;
				    break;
		    }
		    telflag = tblControl.TelephoneFlag;
		    switch (telflag) {
			    case 0:
				    break;
			    default:
				    telnumber = tblControl.TelephoneNumber;
				    break;
		    }

            Classes classLateFees;
            LateFeesParms lateFeesParms = null;

            string clsLateFeesName = "LateFeesParms";

            ChargeCodes[] ChgDescNotAll = ChargeCodes.loads(clientId);
            if (ChgDescNotAll != null && ChgDescNotAll.Length > 0)
            {
                classLateFees = Classes.load(GlobalShared.user.Account.accountId, clsLateFeesName);
                if (classLateFees != null)
                {
                    lateFeesParms = (LateFeesParms)LateFeesParms.DeSerialize(classLateFees.value);
                    LateFeeCode = ChgDescNotAll[lateFeesParms.LateFeeChargeCode].ChargeCode;
                    LateFeeDesc = ChgDescNotAll[lateFeesParms.LateFeeChargeCode].ChargeDescription;
                }
                else
                {
                    classLateFees = new Classes();
                    lateFeesParms = new LateFeesParms();
                }
            }


            tens = tenants;
            //tens = Tenants.loadForStatements(startBuilding, endBuilding, clientId);
            if (tens == null || tens.Length == 0)
            {
                return;    
            }
            if (lasertype != 0)
            {
			    ly = 0;
                rpt.TopMargin = topMargin + fishmanTopOffset;
                rpt.LeftMargin = (float)leftMargin + fishmanLeftOffset;
		    }
            int emailsSentCount = 0;
            int emailsNotSentCount = 0;
            if (email)
            {
                if (Debug)
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting Email Setup", false);
                results = new Window();
                results.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                results.Title = "EMAILING LATE NOTICES - Please Wait...";
                results.Width = 750;
                results.Height = 450;
                results.Background = System.Windows.Media.Brushes.Bisque;
                TextBlock.SetFontWeight(results, FontWeights.Bold);
                TextBlock.SetFontSize(results, 14);

                Border border = new Border() { BorderBrush = System.Windows.Media.Brushes.Blue, BorderThickness = new Thickness(3), CornerRadius = new CornerRadius(5) };

                grid = new Grid();
                TextBlock.SetFontSize(grid, 12);
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                border.Child = grid;

                Border bdr1 = new Border();
                bdr1.BorderBrush = System.Windows.Media.Brushes.Blue;
                bdr1.BorderThickness = new Thickness(2);
                Grid.SetRow(bdr1, 0);
                grid.Children.Add(bdr1);

                StackPanel pnlTop = new StackPanel();
                pnlTop.Orientation = Orientation.Horizontal;
                pnlTop.Background = System.Windows.Media.Brushes.AliceBlue;

                bdr1.Child = pnlTop;

                TextBlock txt1 = new TextBlock();
                txt1.HorizontalAlignment = HorizontalAlignment.Left;
                txt1.VerticalAlignment = VerticalAlignment.Center;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.FontWeight = FontWeights.Bold;
                txt1.Background = System.Windows.Media.Brushes.AliceBlue;
                txt1.Margin = new Thickness(10, 0, 0, 0);
                txt1.Text = "EMAILS SENT";
                pnlTop.Children.Add(txt1);

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

                Border bdr2 = new Border();
                bdr2.BorderBrush = System.Windows.Media.Brushes.Blue;
                bdr2.BorderThickness = new Thickness(2);
                Grid.SetRow(bdr2, 2);
                grid.Children.Add(bdr2);

                StackPanel pnlBottom = new StackPanel();
                pnlBottom.Orientation = Orientation.Horizontal;
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
                lstNotSent.ItemsSource = emailsNotSent;
                Grid.SetRow(lstNotSent, 3);
                grid.Children.Add(lstNotSent);

                StackPanel pnlCommand = new StackPanel();
                pnlCommand.Orientation = Orientation.Horizontal;
                Grid.SetRow(pnlCommand, 4);
                grid.Children.Add(pnlCommand);

                Button cmdPrint = new Button() { Content = "PRINT REPORT", Height = 30 };
                cmdPrint.Click += cmdPrint_Click;
                pnlCommand.Children.Add(cmdPrint);

                Button cmdClose = new Button() { Content = "CLOSE", Height = 30, Margin = new Thickness(10,0,0,0) };
                cmdClose.Click += cmdClose_Click;
                pnlCommand.Children.Add(cmdClose);

                results.Content = border;
                results.Show();
                results.InvalidateVisual();
                results.UpdateLayout();
            }

            bool inserting;
            string fileName = "";
            ChargeCodes codes = null;
            string additionalCharges = "";
            List <string> addList = new List<string>();
            decimal RentDue;
            decimal ChargesPaid;
            decimal RentPaid;

            if (Debug)
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting Tenant Loop", false);

            foreach (Tenants tblTenant in tens)
            {
			    if ((string.Compare(tblTenant.TenantNumber.Trim(), startTenant.Trim()) > 0 || tblTenant.TenantNumber.Trim().Equals(startTenant.Trim())) && (string.Compare(tblTenant.TenantNumber, endTenant) < 0 || tblTenant.TenantNumber.Trim().Equals(endTenant.Trim())))
                {
				    if ((tblTenant.TenantNumber.Length < 4 || !tblTenant.TenantNumber.Substring(6).Equals("B")) && !tblTenant.TenantName.StartsWith("*"))
                    {
                        ChargeDetail chgDetail;
                        bool chgFound = false;
                        chargeDetails.Clear();
                        addList.Clear();
                        additionalCharges = "";
                        Bal = tblTenant.Rent + tblTenant.Arrears - tblTenant.Paid;
                        RentDue = tblTenant.Rent + tblTenant.Arrears;
                        if (rentNotices)
                        {
                            RentDue += tblTenant.Rent;
                            Bal += tblTenant.Rent;
                        }
                        ChargesPaid = 0;
					    Charges[] chgs = Charges.loadBySeqNum(tblTenant.SequenceNumber);
					    if (chgs != null && chgs.Length >0) 
                        {
						    foreach (Charges tblCharges in chgs)
                            {
                                if (tblCharges.StartingDate <= DateTime.Now)
                                {
                                    if (tblCharges.EndingDate <= GlobalShared.NULL_DATE || tblCharges.EndingDate > DateTime.Now)
                                    {
                                        Bal = Bal + tblCharges.Amount + tblCharges.Arrears; //- tblCharges.Paid;
                                        if (rentNotices)
                                            Bal += tblCharges.Amount;
                                        ChargesPaid += tblCharges.Paid;
                                        if (tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid != 0)
                                        {
                                            if (chargeDetails.Count > 0)
                                            {
                                                chgFound = false;
                                                for (int i = 0; i < chargeDetails.Count; i++)
                                                {
                                                    if (!tblCharges.ChargeCode.Equals(LateFeeCode) && tblCharges.ChargeCode.ToLower().Equals(chargeDetails[i].code.ToLower()))
                                                    {
                                                        chgDetail = chargeDetails[i];
                                                        chgDetail.amount += tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                                        if (rentNotices)
                                                            chgDetail.amount += tblCharges.Amount;
                                                        chgDetail.key = tblCharges.ChargeNumber;
                                                        chargeDetails[i] = chgDetail;
                                                        chgFound = true;
                                                        break;
                                                    }
                                                }
                                                if (!chgFound )
                                                {
                                                    chgDetail = new ChargeDetail();
                                                    //chgDetail.code = tblCharges.ChargeCode + ":";
                                                    chgDetail.code = tblCharges.ChargeCode;
                                                    chgDetail.amount = tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                                    if (rentNotices)
                                                        chgDetail.amount += tblCharges.Amount;
                                                    chgDetail.key = tblCharges.ChargeNumber;
                                                    chargeDetails.Add(chgDetail);
                                                }
                                            }
                                            else
                                            {
                                                chgDetail = new ChargeDetail();
                                                chgDetail.code = tblCharges.ChargeCode;
                                                chgDetail.amount = tblCharges.Amount + tblCharges.Arrears - tblCharges.Paid;
                                                if (rentNotices)
                                                    chgDetail.amount += tblCharges.Amount;
                                                chargeDetails.Add(chgDetail);
                                            }
                                        }
                                    }
                                }
						    }

                        }
                        if (Bal >= amount) 
                        {
                            List<ChargeDetail> lateList = new List<ChargeDetail>();
                            RentPaid = tblTenant.Paid - ChargesPaid;
                            if (chargeDetails.Count > 0)
                            {
                                if (RentDue - RentPaid != 0)
                                    additionalCharges = "Rent:" + (RentDue - RentPaid).ToString("#####0.00").Trim();
                                for (int i = 0; i < chargeDetails.Count; i++)
                                {
                                    codes = ChargeCodes.load(chargeDetails[i].code, 1);
                                    if (chargeDetails[i].code.Equals(LateFeeCode))
                                    {
                                        ChargeDetail detail = new ChargeDetail();
                                        detail.code = chargeDetails[i].code;
                                        detail.key = chargeDetails[i].key;
                                        detail.amount = chargeDetails[i].amount;
                                        lateList.Add(detail);
                                    }
                                    else 
                                    {
                                       if (!additionalCharges.Trim().Equals(""))
                                            additionalCharges += ", ";
                                        additionalCharges += (codes != null ? codes.ChargeDescription.Left(18).Trim() + ":" : "*:") + (chargeDetails[i].amount).ToString("######.00").Trim();
                                    }
                                }
                                if (lateList.Count == 1)
                                {
                                    if (!additionalCharges.Trim().Equals(""))
                                        additionalCharges += ", ";
                                    additionalCharges += LateFeeDesc.Left(18).Trim() + (lateList[0].amount).ToString("######.00").Trim();
                                }
                                else if (lateList.Count > 1)
                                {
                                    int maxKey = 0;
                                    int maxIndex = -1;
                                    decimal priorAmounts = 0;
                                    for (int i = 0; i < lateList.Count; i++)
                                    {
                                        if (lateList[i].key > maxKey)
                                        {
                                            maxKey = lateList[i].key;
                                            maxIndex = i;
                                        }
                                    }
                                    for (int i = 0; i < lateList.Count; i++)
                                    {
                                        if (i != maxIndex)
                                            priorAmounts += lateList[i].amount;
                                    }
                                    if (priorAmounts != 0)
                                    {
                                        if (!additionalCharges.Trim().Equals(""))
                                            additionalCharges += ", ";
                                        additionalCharges += "Prior Late Fees:" + (priorAmounts).ToString("######.00").Trim();
                                        if (maxIndex >= 0 && lateList[maxIndex].amount != 0)
                                        {
                                            if (!additionalCharges.Trim().Equals(""))
                                                additionalCharges += ", ";
                                            additionalCharges += LateFeeDesc.Left(18).Trim() + ":" + (lateList[maxIndex].amount).ToString("######.00").Trim();

                                        }
                                    }

                                }
                            }
                            addList = Main.WrapText(additionalCharges, 190, "Courier New", 11); if (!xbld.Equals( tblTenant.BuildingNumber)) 
                            {
                                    tblBuilding = Building.loadByBuildingNumber(tblTenant.BuildingNumber, clientId);
							    if (tblBuilding != null) 
                                {
								    if (retAdd < 2 || telflag == 0) 
                                    {
									    Owners tblOwner = Owners.loadByOwnerNumber( tblBuilding.OwnerNumber, clientId);
									    if (tblOwner != null) {
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
							    xbld = tblTenant.BuildingNumber;
						    }
                            for (int j = 0; j < a.Length; j++)
                                a[j] = "";
						    a[0] = WinpmGlobal.reverse_names(tblTenant.TenantName);
                            a[1] = a[0];
						    if (lasertype == 2) 
                            {
							    a[1] = (tblTenant.AltBillName == null ? "" : tblTenant.AltBillName.Trim());
							    a[2] = (tblTenant.AltBillAdd1 == null ? "" : tblTenant.AltBillAdd1.Trim());
						    }
						    if (tblTenant.AlternateBillingAddressFlag) 
                            {
							    a[1] = tblTenant.AltBillName.Trim();
                                a[0] = a[1];
							    a[2] = tblTenant.AltBillAdd1.Trim();
							    a[3] = tblTenant.AltBillAdd2.Trim();
							    a[4] = tblTenant.AltBillAdd3.Trim();
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
                            if (lasertype != 0)
                            {
                                if (!email)
                                    print_laser(tblTenant, tblBuilding, addList);

                                if (email && tblTenant.SendEmail && (!tblTenant.Email1.Trim().Equals("") || !tblTenant.Email2.Trim().Equals("")))
                                {
                                    if (!Main.CheckEmailDirectories())
                                        return;
                                    print_laser(tblTenant, tblBuilding, addList);
                                    fileName = GlobalShared.appPath + "Emails\\Out\\" + String.Format(GlobalShared.mailFilename, WinpmGlobal.EmailTypeFilenames[(int)WinpmGlobal.EmailTypes.LateNotice] + "_", tblTenant.SequenceNumber, 0, this.date, "pdf");
                                    if (!rpt.DocToPdf(fileName))
                                    {
                                        MessageBox.Show("Unable to create LateNotice attachment. See log for more information.");
                                        return;
                                    }
                                    // Insert fields in subject and message
                                    string subject = this.EmailSubject;
                                    string message = this.Message;
                                    string displayname = this.DisplayName;
                                    for (int i = 0; i < WinpmGlobal.NoticeInsertionFieldNames.Length; i++)
                                    {
                                        InsertEmailField(ref subject, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], tblTenant.Apartment, Bal.ToString("$###,###0.00"), tblTenant.BuildingTenant);
                                        InsertEmailField(ref message, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], tblTenant.Apartment, Bal.ToString("$###,###0.00"), tblTenant.BuildingTenant);
                                        InsertEmailField(ref displayname, WinpmGlobal.NoticeInsertionFieldNames[i], a[0], o[1], tblTenant.Apartment, Bal.ToString("$###,###0.00"), tblTenant.BuildingTenant);
                                    }
                                    if (this.Test)
                                    {
                                        if (Email.Utils.TestEmail(FromEmail, tblTenant.BuildingNumber, tblTenant.TenantNumber, tblTenant.TenantName, Smtp, FromEmail, displayname, tblTenant.UseEmail2 ? tblTenant.Email2 : tblTenant.Email1, subject, message, "", fileName, false, ReturnReceipt))
                                        {
                                            emailsSentCount += 1;
                                            emailsSent.Add(tblTenant.BuildingTenantNumber + "|" + tblTenant.TenantName + "|" + (string)(tblTenant.UseEmail2 ? tblTenant.Email2 : tblTenant.Email1) + "|" + Bal.ToString("$#######.00"));
                                            lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                            string rc = Email.Utils.SaveEmail(FromEmail, displayname, tblTenant.UseEmail2 ? tblTenant.Email2 : tblTenant.Email1, subject, message, fileName.Replace(".pdf", ".txt"), date.ToShortDateString(), ReturnReceipt);
                                            if (rc != null)
                                            {
                                                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Late Notice Email: " + rc, false);
                                            }
                                        }
                                        else
                                        {
                                            emailsNotSentCount += 1;
                                            emailsNotSent.Add(tblTenant.BuildingTenantNumber + "| " + tblTenant.TenantName + "| " + (string)(tblTenant.UseEmail2 ? tblTenant.Email2 : tblTenant.Email1) + "|" + Bal.ToString("$#######.00"));
                                            lstNotSent.ScrollIntoView(emailsSent[emailsNotSent.Count - 1]);
                                        }
                                    }
                                    else
                                    {
                                        ArrayList list;
                                        // Send to Email 1
                                        if (tblTenant.UseEmail1)
                                        {
                                            list = Email.Utils.SendEmail(Smtp, FromEmail, displayname, tblTenant.Email1, subject, message, "", fileName, tblTenant.UseEmail2, ReturnReceipt);
                                            if (list == null || list.Count == 0)
                                            {
                                                emailsSentCount += 1;
                                                emailsSent.Add(tblTenant.BuildingTenantNumber + "|" + tblTenant.TenantName + "|" + tblTenant.Email1 + "|" + Bal.ToString("$#######.00"));
                                                lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                                string rc = Email.Utils.SaveEmail(FromEmail, displayname, tblTenant.Email1, subject, message, fileName.Replace(".pdf", ".txt"), date.ToShortDateString(), ReturnReceipt);
                                                if (rc != null)
                                                {
                                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Late Notice Email: " + rc, false);
                                                }
                                            }
                                            else
                                            {
                                                emailsNotSentCount += 1;
                                                emailsNotSent.Add(tblTenant.BuildingTenantNumber + "| " + tblTenant.TenantName + "| " + tblTenant.Email1 + "| " + Bal.ToString("$#######.00") + "|" + list[0].ToString());
                                                lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);
                                            }
                                        }
                                        // Send to Email 2
                                        if (tblTenant.UseEmail2)
                                        {
                                            list = Email.Utils.SendEmail(Smtp, FromEmail, displayname, tblTenant.Email2, subject, message, "", fileName, false, ReturnReceipt);
                                            if (list == null || list.Count == 0)
                                            {
                                                emailsSentCount += 1;
                                                emailsSent.Add(tblTenant.BuildingTenantNumber + "|" + tblTenant.TenantName + "|" + tblTenant.Email2 + "|" + Bal.ToString("$#######.00"));
                                                lstSent.ScrollIntoView(emailsSent[emailsSent.Count - 1]);
                                                string rc = Email.Utils.SaveEmail(FromEmail, displayname, tblTenant.Email2, subject, message, fileName.Replace(".pdf", ".txt"), date.ToShortDateString(), ReturnReceipt);
                                                if (rc != null)
                                                {
                                                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Saving Late Notice Email: " + rc, false);
                                                }
                                            }
                                            else
                                            {
                                                emailsNotSentCount += 1;
                                                emailsNotSent.Add(tblTenant.BuildingTenantNumber + "| " + tblTenant.TenantName + "| " + tblTenant.Email2 + "| " + Bal.ToString("$#######.00") + "|" + list[0].ToString());
                                                lstNotSent.ScrollIntoView(emailsNotSent[emailsNotSent.Count - 1]);
                                            }
                                        }
                                    }
                                }
                                printed = true;
                            }
                            else
                            {
                                if (topMargin != 0)
                                rpt.PrintPageLine(date.ToString("MM/dd/yy ") + tblTenant.BuildingNumber + tblTenant.TenantNumber + tblTenant.TenantName.Substring(0, 16) + tblBuilding.Address1.Substring(0, 30) + "  " + tblTenant.Apartment + " " + Bal.ToString("######.00").PadLeft(9), null);
                                for (int k = 1; k <= 11; k++)
                                {
                                    rpt.PrintPageLine("", null);
                                }
                                string[] s = { "tab(3)", a[1], "tab(40)", o[1] };
                                rpt.PrintPageLine(s);
                                string[] s1 = { "tab(3)", a[2], "tab(40)", o[2] };
                                rpt.PrintPageLine(s1);
                                string[] s2 = { "tab(3)", a[3], "tab(40)", o[3] };
                                rpt.PrintPageLine(s2);
                                string[] s3 = { "tab(3)", a[4], "tab(40)", o[4] };
                                rpt.PrintPageLine(s3);
                            }
					    }
				    }
			    }
		    }
            if (printed)
            {
                if (!email)
                    rpt.PrintDocument();
                else
                {
                    txtNumberSent.Text = emailsSentCount.ToString().PadLeft(8);
                    txtNumberNotSent.Text = emailsNotSentCount.ToString().PadLeft(8);
                    results.Title = "EMAILS SENT";
                }
            }
            if (email && AlsoPrint)
            {
                email = false;
                PrintNotices(tenants);
            }

        }

        void InsertEmailField(ref string content, string field, string TenantName, string OwnerName, string Apartment, string Balance, string TenantNumber)
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
        }
        void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string[] s;

            if (emailsSent.Count > 0 || emailsNotSent.Count > 0)
            {
                Report rpt = new Report("Late Notice Emails", "Courier New", 12);
                rpt.LeftMargin = 10;
                rpt.TopMargin = 10;
                rpt.PrintPageLine("LATE NOTICE EMAIL REPORT");
                rpt.PrintPageLine(DateTime.Now.ToShortDateString());
                rpt.PrintPageLine("");rpt.PrintPageLine("");
                rpt.PrintPageLine("Tenant#    Name                            Email Address             DUE");
                rpt.PrintPageLine("");
                rpt.PrintPageLine("SENT NOTICES - Count: " + emailsSent.Count.ToString());
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
                                rpt.PrintPageLine(s[2].PadRight(21), false);
                                rpt.PrintPageLine(s[3]);
;                            }
                        }
                    }
                }
                else
                    rpt.PrintPageLine("***** NONE *****");

                rpt.PrintPageLine(""); rpt.PrintPageLine("");

                rpt.PrintPageLine("NOTICES NOT SENT - Count: " + emailsNotSent.Count.ToString());
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
                                rpt.PrintPageLine(s[2].PadRight(21), false);
                                rpt.PrintPageLine(s[3]);
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
        
        void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            results.Close();
        }

        public Tenants[] RetrieveTenants()
        {
            ArrayList list = new ArrayList();

            tens = Tenants.loadForStatements(startBuilding, endBuilding, clientId);
            if (tens == null || tens.Length == 0)
            {
                return null;
            }
            if (lasertype != 0)
            {
                ly = 0;
                rpt.TopMargin = topMargin + fishmanTopOffset;
                rpt.LeftMargin = (float)leftMargin + fishmanLeftOffset;
            }

            foreach (Tenants tblTenant in tens)
            {
                if ((string.Compare(tblTenant.TenantNumber.Trim(), startTenant.Trim()) > 0 || tblTenant.TenantNumber.Trim().Equals(startTenant.Trim())) && (string.Compare(tblTenant.TenantNumber, endTenant) < 0 || tblTenant.TenantNumber.Trim().Equals(endTenant.Trim())))
                {
                    if ((tblTenant.TenantNumber.Length < 4 || !tblTenant.TenantNumber.Substring(6).Equals("B")) && !tblTenant.TenantName.StartsWith("*"))
                    {
                        Bal = tblTenant.Rent + tblTenant.Arrears - tblTenant.Paid;
                        Charges[] chgs = Charges.loadBySeqNum(tblTenant.SequenceNumber);
                        if (chgs != null && chgs.Length > 0)
                        {
                            foreach (Charges tblCharges in chgs)
                            {
                                if (tblCharges.StartingDate <= DateTime.Now)
                                    if (tblCharges.EndingDate <= GlobalShared.NULL_DATE || tblCharges.EndingDate > DateTime.Now)
                                        Bal = Bal + tblCharges.Amount + tblCharges.Arrears;
                            }
                        }
                        if (Bal >= amount)
                        {
                            if (!tblTenant.Email1.Trim().Equals("") || !tblTenant.Email2.Trim().Equals(""))
                            {
                                tblTenant.CanSendEmail = true;
                                tblTenant.SendEmail = true;
                                if (!tblTenant.Email1.Trim().Equals(""))
                                {
                                    tblTenant.UseEmail1 = true;
                                    tblTenant.IsEmail1 = true;
                                }
                                else
                                    tblTenant.UseEmail2 = true;
                                if (!tblTenant.Email2.Trim().Equals(""))
                                    tblTenant.IsEmail2 = true;
                            }
                            list.Add(tblTenant);
                        }
                    }
                }
            }
            if (list.Count > 0)
            {
                try
                {
                    return (Tenants[])list.ToArray(typeof(Tenants));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
        private void print_laser(Tenants tblTenant, Building tblBuilding, List<string> additionalCharges = null)
        {
            if (Debug)
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting print_laser", false);
            string name = WinpmGlobal.reverse_names(tblTenant.TenantName);
            float fontsize;
            if (ly == 0)
                print_template();

            switch (lasertype)
            {
                case 1:
                    try
                    {
                        rpt.FontBold = true;
                        //rpt.PrintPageLine(o[1].Trim() + ", " + o[2].Trim() + ", " + o[3].Trim() + ", " + o[4].Trim() + "," + telnumber, 2, ly, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(date.ToString("MM/dd/yy"), 2, ly + 4, null);
                        rpt.PrintPageLine(tblTenant.BuildingNumber + tblTenant.TenantNumber, 9, ly + 4, null);

                        if (!tblTenant.LegalCode.Trim().Equals(""))
                        {
                            float size = rpt.FontSize;
                            rpt.FontSize = 18;
                            rpt.FontColor = System.Windows.Media.Colors.Red;
                            rpt.PrintPageLine(tblTenant.LegalCode, 20, ly + 4, null);
                            rpt.FontColor = System.Windows.Media.Colors.Black;
                            rpt.FontSize = size;
                        }

                        // Scan Line
                        string TenAccount = (tblTenant.BuildingNumber.Trim().PadLeft(3, '0') + tblTenant.TenantNumber.Trim().PadRight(4, '0'));
                        TenAccount += WinpmGlobal.LockBoxCheckDigit(TenAccount).ToString("0");

                        string scan = (TenAccount + date.ToString("0MMddyy") + (Bal * 100).ToString("0000000")).PadLeft(28, '0');
                        Font fnt = new Font("OCR A Extended", 14);
                        rpt.PrintPageLine(scan, 35, ly + 3, fnt);

                        //rpt.PrintPageLine(name.Substring(0, (name.Length < 27 ? name.Length : 27)), 16, ly + 4, null);
                        //rpt.PrintPageLine(tblBuilding.Address1.Substring(0, (tblBuilding.Address1.Length < 29 ? tblBuilding.Address1.Length : 29)), 38, ly + 4, null);
                        rpt.PrintPageLine(tblBuilding.Address1.Substring(0, (tblBuilding.Address1.Length < 29 ? tblBuilding.Address1.Length : 29)), 55, ly + 6, null);

                        rpt.PrintPageLine(tblTenant.Apartment.PadRight(4), 64, ly + 3, null);
                        rpt.PrintPageLine(Bal.ToString("$#######.00").PadLeft(15), 66, ly + 3, null);
                        fontsize = rpt.FontSize;
                        rpt.FontSize = 16;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(o[1], 30, ly + 11, null);
                        //rpt.FontBold = false;
                        rpt.FontSize = fontsize;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[1], 45, ly + 12, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(o[1], 11, ly + 12, null);
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[2].Trim(), 45, ly + 13, null);
                        if (a[4].Trim().Equals(""))
                            rpt.PrintPageLine(" - " + tblTenant.Apartment.Trim(), (a[2].Trim().Length / 3) * 45 + 4, ly + 13, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(o[2], 11, ly + 13, null);
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[3].Trim(), 45, ly + 14, null);
                        if (!a[4].Trim().Equals(""))
                            rpt.PrintPageLine(" - " + tblTenant.Apartment.Trim(), (a[3].Trim().Length / 3) * 45 + 4, ly + 14, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(o[3], 11, ly + 14, null);
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[4], 45, ly + 15, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(o[4], 11, ly + 15, null);
                        rpt.FontBold = false;
                        /*
                        if (additionalCharges != null && additionalCharges.Count > 0)
                        {
                            double y = ly + 12;
                            foreach (string item in additionalCharges)
                            {
                                rpt.PrintPageLine(item, 44.5, y, null);
                                y += 1;
                            }
                        }
                        rpt.FontBold = false;
                        */
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Printing LateNotice Body (2): " + ex.Message, false);
                    }

                    break;
                case 2:
                    try
                    {
                        rpt.FontBold = true;
                        rpt.PrintPageLine(o[1].Trim() + ", " + o[2].Trim() + ", " + o[3].Trim() + ", " + o[4].Trim() + "," + telnumber, 2, ly, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(date.ToString("MM/dd/yy"), 2, ly + 4, null);
                        rpt.PrintPageLine(tblTenant.BuildingNumber + tblTenant.TenantNumber, 9, ly + 4, null);
                        rpt.PrintPageLine(name.Substring(0, (name.Length < 27 ? name.Length : 27)), 16, ly + 4, null);
                        rpt.PrintPageLine(tblBuilding.Address1.Substring(0, (tblBuilding.Address1.Length < 29 ? tblBuilding.Address1.Length : 29)), 38, ly + 4, null);
                        rpt.PrintPageLine(tblTenant.Apartment.PadRight(4), 62, ly + 4, null);
                        rpt.PrintPageLine(Bal.ToString("$#######.00").PadLeft(9), 69, ly + 4, null);
                        fontsize = rpt.FontSize;
                        rpt.FontSize = 16;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(o[1], 30, ly + 11, null);
                        //rpt.FontBold = false;
                        rpt.FontSize = fontsize;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[1].Trim(), 14, ly + 13, null);
                        if (tblTenant.LegalCode.Trim().ToUpper().Equals(legal1.ToUpper()) || tblTenant.LegalCode.Trim().ToUpper().Equals(legal2.ToUpper()))
                        {
                            rpt.FontSize = 16;
                            rpt.PrintPageLine("NO DISCOUNT ALLOWED", 45, ly + 6, null);
                            rpt.FontSize = fontsize;
                            //rpt.PrintPageLine("NO DISCOUNT ALLOWED", 48, ly + 13, null);
                        }
                        rpt.PrintPageLine(a[2].Trim() + (a[4].Trim().Equals("") ? " - " + tblTenant.Apartment.Trim() : ""), 14, ly + 14, null);
                        rpt.PrintPageLine(a[3].Trim() + (!a[4].Trim().Equals("") ? " - " + tblTenant.Apartment.Trim() : ""), 14, ly + 15, null);
                        rpt.PrintPageLine(a[4].Trim(), 14, ly + 16, null);
                        if (additionalCharges != null && additionalCharges.Count > 0)
                        {
                            double y = ly + 12;
                            foreach (string item in additionalCharges)
                            {
                                rpt.PrintPageLine(item, 44.5, y, null);
                                y += 1;
                            }
                        }
                        rpt.FontBold = false;
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Printing LateNotice Body (2): " + ex.Message, false);
                    }

                    break;
                case 3:
                    try
                    {
                        rpt.FontBold = true;
                        rpt.PrintPageLine(o[1].Trim() + ", " + o[2].Trim() + ", " + o[3].Trim() + ", " + o[4].Trim() + "," + telnumber, 2, ly, null);
                        //rpt.FontBold = false;
                        rpt.PrintPageLine(date.ToString("MM/dd/yy"), 2, ly + 4, null);
                        rpt.PrintPageLine(tblTenant.BuildingNumber + tblTenant.TenantNumber, 9, ly + 4, null);
                        rpt.PrintPageLine(name.Substring(0, (name.Length < 27 ? name.Length : 27)), 16, ly + 4, null);
                        rpt.PrintPageLine(tblBuilding.Address1.Substring(0, (tblBuilding.Address1.Length < 29 ? tblBuilding.Address1.Length : 29)), 38, ly + 4, null);
                        rpt.PrintPageLine(tblTenant.Apartment.PadRight(4), 62, ly + 4, null);
                        rpt.PrintPageLine(Bal.ToString("$#######.00").PadLeft(9), 69, ly + 4, null);
                        fontsize = rpt.FontSize;
                        rpt.FontSize = 16;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(o[1], 30, ly + 11, null);
                        //rpt.FontBold = false;
                        rpt.FontSize = fontsize;
                        //rpt.FontBold = true;
                        rpt.PrintPageLine(a[1].Trim(), 14, ly + 13, null);
                        if (tblTenant.LegalCode.Trim().ToUpper().Equals(legal1.ToUpper()) || tblTenant.LegalCode.Trim().ToUpper().Equals(legal2.ToUpper()))
                        {
                            rpt.FontSize = 16;
                            rpt.PrintPageLine("NO DISCOUNT ALLOWED", 45, ly + 6, null);
                            rpt.FontSize = fontsize;
                            //rpt.PrintPageLine("NO DISCOUNT ALLOWED", 48, ly + 13, null);
                        }
                        rpt.PrintPageLine(a[2].Trim() + (a[4].Trim().Equals("") ? " - " + tblTenant.Apartment.Trim() : ""), 14, ly + 14, null);
                        rpt.PrintPageLine(a[3].Trim() + (!a[4].Trim().Equals("") ? " - " + tblTenant.Apartment.Trim() : ""), 14, ly + 15, null);
                        rpt.PrintPageLine(a[4].Trim(), 14, ly + 16, null);
                        if (additionalCharges != null && additionalCharges.Count > 0)
                        {
                            double y = ly + 12;
                            foreach (string item in additionalCharges)
                            {
                                rpt.PrintPageLine(item, 44.5, y, null);
                                y += 1;
                            }
                        }
                        rpt.FontBold = false;
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Printing RentNotice Body (3): " + ex.Message, false);
                    }

                    break;

            }
            ly = ly + 22;
            if (ly > 44 || email)
            {    
                rpt.NewPage();
                ly = 0;
            }
        }                
	private void print_template()
	{
            if (Debug)
                GlobalShared.Log.Log((int)LogClass.logType.Debug, "Starting print_template", false);
            TopY = 0;
                    
        Font mainFont = rpt.SetFont;
        rpt.SetFont = new Font("Arial",12);
            //Printer.DrawWidth = 12;

        int c = 2;
            if (email)
                c = 0;
        switch (lasertype) 
        {
			case 1:
                    //Headers
                    try
                    {
                        for (int i = 0; i <= c; i++)
                        {
                            TopY = (22 * i + 1);

                            rpt.FontSize = 12;

                            // Date
                            rpt.PrintLineWithMargins(1, TopY, 8, TopY + 2, null, true);
                            rpt.PrintLineWithMargins(1, TopY + 2, 8, TopY + 4, null, true);
                            rpt.PrintPageLine("DATE", 3, TopY + 1, null);

                            // Tenant ID
                            rpt.PrintLineWithMargins(8, TopY, 15, TopY + 2, null, true);
                            rpt.PrintLineWithMargins(8, TopY + 2, 15, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT ID", 8, TopY + 1, null);
                           
                            // Tenant Name
                            //rpt.PrintLineWithMargins(15, TopY, 37, TopY + 2, null, true);
                            //rpt.PrintLineWithMargins(15, TopY + 2, 37, TopY + 4, null, true);
                            //rpt.PrintPageLine("TENANT NAME", 21, TopY + 1, null);                            

                            // Apt
                            rpt.PrintLineWithMargins(63, TopY , 67, TopY + 2 , null, true);
                            rpt.PrintLineWithMargins(63, TopY + 2, 67, TopY + 4, null, true);
                            rpt.PrintPageLine("APT", 64, TopY, null);
                            
                            // Amount Due
                            rpt.PrintLineWithMargins(67, TopY, 77, TopY + 2 , null, true);
                            rpt.PrintLineWithMargins(67, TopY + 2, 77, TopY + 4, null, true);
                            rpt.PrintPageLine("AMOUNT DUE", 68, TopY  , null);

                            // Premises
                            //rpt.PrintLineWithMargins(54, TopY + 4, 77, TopY + 5, null, true);
                            rpt.PrintLineWithMargins(54, TopY + 5, 77, TopY + 6, null, true);
                            rpt.PrintPageLine("PREMISES", 61, TopY + 4, null);
                            //rpt.PrintLineWithMargins(80, TopY, 83, TopY + 6, null, true);
                            /*
                            rpt.PrintLineWithMargins(37, TopY + 2, 61, TopY + 4, null, true);
                            rpt.PrintPageLine("PREMISES", 45, TopY + 1, null);
                            */

                            rpt.FontSize = 18;
                            rpt.PrintPageLine("NOTICE OF", 9, TopY + 5, null);
                            rpt.FontBold = true;
                            rpt.FontSize = 21;
                            rpt.PrintPageLine("PAST DUE MONIES", 21, TopY + 5, null);
                            rpt.FontSize = 18;
                            rpt.FontBold = false;
                            rpt.PrintPageLine("YOUR PAYMENT HAS NOT BEEN RECEIVED AND IS NOW PAST DUE.", 9, TopY + 7, null);
                            rpt.PrintPageLine("TO AVOID FUTURE ACTION, IMMEDIATE PAYMENT WILL BE APPRECIATED.", 9, TopY + 8, null);
                            rpt.FontSize = 16;
                            rpt.PrintPageLine("Please make payment to:", 10, TopY + 10, null);
                            // make company name much larger
                            rpt.PrintLineWithMargins(10, TopY + 11, 43, TopY + 16, null, true);
                            rpt.PrintLineWithMargins(44, TopY + 11, 73, TopY + 16, null, true);

                            rpt.FontSize = 12;
                            //If i < 2 Then
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "print_temlate (1): " + ex.Message, false);

                    }
                    break;

			case 2:
                    //Headers
                    try
                    {
                        for (int i = 0; i <= c; i++)
                        {
                            TopY = (22 * i + 1);

                            rpt.FontSize = 12;
                            rpt.PrintLineWithMargins(1, TopY, 8, TopY + 2, null, true);
                            // Date
                            rpt.PrintLineWithMargins(1, TopY + 2, 8, TopY + 4, null, true);
                            rpt.PrintPageLine("DATE", 3, TopY + 1, null);
                            rpt.PrintLineWithMargins(8, TopY, 15, TopY + 2, null, true);
                            // Tenant ID
                            rpt.PrintLineWithMargins(8, TopY + 2, 15, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT ID", 8, TopY + 1, null);
                            rpt.PrintLineWithMargins(15, TopY, 37, TopY + 2, null, true);
                            // Tenant Name
                            rpt.PrintLineWithMargins(15, TopY + 2, 37, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT NAME", 21, TopY + 1, null);
                            rpt.PrintLineWithMargins(37, TopY, 61, TopY + 2, null, true);
                            // Premises
                            rpt.PrintLineWithMargins(37, TopY + 2, 61, TopY + 4, null, true);
                            rpt.PrintPageLine("PREMISES", 45, TopY + 1, null);
                            rpt.PrintLineWithMargins(61, TopY, 65, TopY + 2, null, true);
                            // Apt
                            rpt.PrintLineWithMargins(61, TopY + 2, 65, TopY + 4, null, true);
                            rpt.PrintPageLine("APT", 62, TopY + 1, null);
                            rpt.PrintLineWithMargins(65, TopY, 77, TopY + 2, null, true);
                            // Amount Due
                            rpt.PrintLineWithMargins(65, TopY + 2, 77, TopY + 4, null, true);
                            rpt.PrintPageLine("AMOUNT DUE", 67, TopY + 1, null);

                            rpt.FontSize = 18;
                            rpt.PrintPageLine("NOTICE OF", 9, TopY + 5, null);
                            rpt.FontBold = true;
                            rpt.FontSize = 21;
                            rpt.PrintPageLine("PAST DUE RENT", 21, TopY + 5, null);
                            rpt.FontSize = 18;
                            rpt.FontBold = false;
                            rpt.PrintPageLine("YOUR RENT HAS NOT BEEN RECEIVED AND IS NOW PAST DUE.", 9, TopY + 7, null);
                            rpt.PrintPageLine("TO AVOID FUTURE ACTION, IMMEDIATE PAYMENT WILL BE APPRECIATED.", 9, TopY + 8, null);
                            rpt.FontSize = 16;
                            rpt.PrintPageLine("Please make payment to:", 10, TopY + 10, null);
                            // make company name much larger
                            rpt.PrintLineWithMargins(10, TopY + 11, 43, TopY + 16, null, true);
                            rpt.PrintLineWithMargins(44, TopY + 11, 73, TopY + 16, null, true);

                            rpt.FontSize = 12;
                            //If i < 2 Then
                        }
                    }
                    catch (Exception ex)
                    {
                            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "print_temlate (2): " + ex.Message, false);

                    }
                    break;
                case 3:
                    //Headers
                    try
                    {
                        for (int i = 0; i <= c; i++)
                        {
                            TopY = (22 * i + 1);

                            rpt.FontSize = 12;
                            rpt.PrintLineWithMargins(1, TopY, 8, TopY + 2, null, true);
                            // Date
                            rpt.PrintLineWithMargins(1, TopY + 2, 8, TopY + 4, null, true);
                            rpt.PrintPageLine("DATE", 3, TopY + 1, null);
                            rpt.PrintLineWithMargins(8, TopY, 15, TopY + 2, null, true);
                            // Tenant ID
                            rpt.PrintLineWithMargins(8, TopY + 2, 15, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT ID", 8, TopY + 1, null);
                            rpt.PrintLineWithMargins(15, TopY, 37, TopY + 2, null, true);
                            // Tenant Name
                            rpt.PrintLineWithMargins(15, TopY + 2, 37, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT NAME", 21, TopY + 1, null);
                            rpt.PrintLineWithMargins(37, TopY, 61, TopY + 2, null, true);
                            // Premises
                            rpt.PrintLineWithMargins(37, TopY + 2, 61, TopY + 4, null, true);
                            rpt.PrintPageLine("PREMISES", 45, TopY + 1, null);
                            rpt.PrintLineWithMargins(61, TopY, 65, TopY + 2, null, true);
                            // Apt
                            rpt.PrintLineWithMargins(61, TopY + 2, 65, TopY + 4, null, true);
                            rpt.PrintPageLine("APT", 62, TopY + 1, null);
                            rpt.PrintLineWithMargins(65, TopY, 77, TopY + 2, null, true);
                            // Amount Due
                            rpt.PrintLineWithMargins(65, TopY + 2, 77, TopY + 4, null, true);
                            rpt.PrintPageLine("AMOUNT DUE", 67, TopY + 1, null);

                            rpt.FontSize = 18;
                            rpt.PrintPageLine("NOTICE OF", 9, TopY + 5, null);
                            rpt.FontBold = true;
                            rpt.FontSize = 21;
                            rpt.PrintPageLine("RENT DUE", 21, TopY + 5, null);
                            rpt.FontSize = 18;
                            rpt.FontBold = false;
                            rpt.PrintPageLine("YOUR RENT IS NOW DUE.", 9, TopY + 7, null);
                            rpt.PrintPageLine("IMMEDIATE PAYMENT WILL BE APPRECIATED.", 9, TopY + 8, null);
                            rpt.FontSize = 16;
                            rpt.PrintPageLine("Please make payment to:", 10, TopY + 10, null);
                            // make company name much larger
                            rpt.PrintLineWithMargins(10, TopY + 11, 43, TopY + 16, null, true);
                            rpt.PrintLineWithMargins(44, TopY + 11, 73, TopY + 16, null, true);

                            rpt.FontSize = 12;
                            //If i < 2 Then
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "print_temlate (2): " + ex.Message, false);

                    }
                    break;
                default:
                    //Headers
                    try
                    {
                        for (int i = 0; i <= c; i++)
                        {
                            TopY = (22 * i + 1);

                            rpt.FontSize = 12;

                            rpt.PrintLineWithMargins(1.5, TopY, 8.5, TopY + 2, null, true);
                            // Date
                            rpt.PrintLineWithMargins(1.5, TopY + 2, 8.5, TopY + 4, null, true);
                            rpt.PrintPageLine("DATE", 3, TopY + 1, null);
                            rpt.PrintLineWithMargins(8.5, TopY, 12.5, TopY + 2, null, true);
                            // Tenant ID
                            rpt.PrintLineWithMargins(8.5, TopY + 2, 12.5, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT ID", 9, TopY + 1, null);
                            rpt.PrintLineWithMargins(12.5, TopY, 37.5, TopY + 2, null, true);
                            // Tenant Name
                            rpt.PrintLineWithMargins(12.5, TopY + 2, 37.5, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT NAME", 22, TopY + 1, null);
                            rpt.PrintLineWithMargins(37.5, TopY, 61.5, TopY + 2, null, true);
                            // Premises
                            rpt.PrintLineWithMargins(37.5, TopY + 2, 61.5, TopY + 4, null, true);
                            rpt.PrintPageLine("PREMISES", 45, TopY + 1, null);
                            rpt.PrintLineWithMargins(61.5, TopY, 65.5, TopY + 2, null, true);
                            // Apt
                            rpt.PrintLineWithMargins(61.5, TopY + 2, 65.5, TopY + 4, null, true);
                            rpt.PrintPageLine("APT", 62, TopY + 1, null);
                            rpt.PrintLineWithMargins(65.5, TopY, 77.5, TopY + 2, null, true);
                            // Amount Due
                            rpt.PrintLineWithMargins(65.5, TopY + 2, 77.5, TopY + 4, null, true);
                            rpt.PrintPageLine("AMOUNT DUE", 67, TopY + 1, null);


                            /*
                            rpt.PrintLineWithMargins( 1.5, TopY, 8.5, TopY + 2, null, true);
                            // Date
                            rpt.PrintLineWithMargins( 1.5, TopY + 2, 8.5, TopY + 4, null, true);
                            rpt.PrintPageLine("DATE", 3, TopY + 1, null);
                            rpt.PrintLineWithMargins(8.5, TopY, 19.5, TopY + 2, null, true);
                            // Tenant ID
                            rpt.PrintLineWithMargins(8.5, TopY + 2, 19.5, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT ID", 9, TopY + 1, null);
                            rpt.PrintLineWithMargins(18.5, TopY, 37.5, TopY + 2, null, true);
                            // Tenant Name
                            rpt.PrintLineWithMargins(18.5, TopY + 2, 37.5, TopY + 4, null, true);
                            rpt.PrintPageLine("TENANT NAME", 22, TopY + 1, null);
                            rpt.PrintLineWithMargins(37.5, TopY, 61.5, TopY + 2, null, true);
                            // Premises
                            rpt.PrintLineWithMargins(37.5, TopY + 2, 61.5, TopY + 4, null, true);
                            rpt.PrintPageLine("PREMISES", 45, TopY + 1, null);
                            rpt.PrintLineWithMargins(61.5, TopY, 65.5, TopY + 2, null, true);
                            // Apt
                            rpt.PrintLineWithMargins(61.5, TopY + 2, 65.5, TopY + 4, null, true);
                            rpt.PrintPageLine("APT", 62, TopY + 1, null);
                            rpt.PrintLineWithMargins(65.5, TopY, 77.5, TopY + 2, null, true);
                            // Amount Due
                            rpt.PrintLineWithMargins(65.5, TopY + 2, 77.5, TopY + 4, null, true);
                            rpt.PrintPageLine("AMOUNT DUE", 67, TopY + 1, null);
                            */

                            rpt.FontSize = 24;
                            rpt.FontBold = true;
                            rpt.PrintPageLine("TAKE NOTICE:", 18, TopY + 5, null);
                            rpt.PrintPageLine("THE AMOUNT DUE SHOWN ABOVE HAS NOT", 34, TopY + 5, null);
                            rpt.PrintPageLine("BEEN PAID. TO AVOID LEGAL ACTION", 34, TopY + 6, null);
                            rpt.PrintPageLine("PLEASE SEND YOUR CHECK WITHOUT DELAY.", 34, TopY + 7, null);
                            rpt.PrintPageLine("THANK YOU.", 55, TopY + 8, null);
                            rpt.FontBold = false;
                            rpt.FontSize = 9;
                            rpt.PrintPageLine("PAYMENTS MADE WITHIN LAST 3 DAYS MAY NOT BE REFLECTED ON THIS NOTICE.", 21, TopY + 11, null);

                            rpt.PrintPageLine("PLEASE REFER INQUIRIES TO:", 50, TopY + 12, null);

                            rpt.PrintLineWithMargins(1.5, TopY + 13, 40.5, TopY + 18, null, true);
                            rpt.PrintLineWithMargins(42.5, TopY + 13, 77.5, TopY + 18, null, true);

                            rpt.FontSize = 12;
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "print_temlate (1): " + ex.Message, false);

                    }
                    break;
		}

		rpt.FontSize  = 10;
		rpt.SetFont  = mainFont;
	}
                              
        public string StartBuilding
        {
            set {startBuilding = value;}
        }
        public string StartTenant
        {
            set{startTenant = value;}
        }
        public string EndBuilding
        {
            set{endBuilding = value;}
        }
        public string EndTenant
        {
            set{endTenant = value;}
        }
        public int TopMargin
        {
            set{topMargin = value;}
        }
        public double LeftMargin
        {
            set{leftMargin = value;}
        }
        public decimal Amount
        {
            set {amount = value;}
        }
        public DateTime Date
        {
            set{date = value;}
        }
        public int LaserType
        {
            set{lasertype = value;}
        }

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
        public bool Test { get; set; }

        public bool rentNotices { get; set; } = false;

    }
}
