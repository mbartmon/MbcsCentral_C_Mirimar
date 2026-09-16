using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing.Printing;
using System.Windows.Markup;
namespace MbcsCentral.WinPM
{

	// TODO: Find history from entered Building/Tenant number
	// TODO: Implement printing

	public partial class frmHistoryDisplay
	{

		private int m_tenantId = 0;
		private string m_buildingNumber;
		private string m_tenantNumber;
		private HistoryDisplay[] historyRecords = null;
		private ArrayList historyArray;

		private HistoryList historyList;
        private Histmast[] histMaster;
		private HistChg[] histCharge;

		private HistPay[] histPayment;
		private decimal balance = 0;

		private decimal amountPaid = 0;

        Report rpt = new Report("Tenant History");

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmHistoryDisplay()
        {
            InitializeComponent();
            this.Loaded += frmHistoryDisplay_Loaded;
            this.Closing += frmHistoryDisplay_Closing;
        }

        public frmHistoryDisplay(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmHistoryDisplay_Loaded;
            this.Closing += frmHistoryDisplay_Closing;
        }		
        public int TenantId {
			set { m_tenantId = value; }
		}
		public string BuildingNumber {
			set { m_buildingNumber = value; }
		}
		public string TenantNumber {
			set { m_tenantNumber = value; }
		}


		private void frmHistoryDisplay_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Main.saveForm(this);

		}


		private void frmHistoryDisplay_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);

			bool getHist = false;
			string pkInstalledPrinters = null;

			dtStart.Text = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Now) - 1, DateAndTime.Month(DateAndTime.Now), 1).ToString();
			dtEnd.Text = (string)DateAndTime.Now.Date.ToString();

            // Have we been passed a tenant ID?
			if (m_tenantId > 0) {
				histMaster = new Histmast[1];
				try {
					histMaster[0] = Histmast.load(m_tenantId);
				} catch (System.Data.OleDb.OleDbException ex) {
					return;
				} catch (System.Exception ex) {
					return;
				}
				if ((histMaster != null)) {
					m_tenantId = histMaster[0].SequenceNumber;
					getHist = true;
				}
                // Have we been passed a building and tenant number?
			} else if (m_buildingNumber != null && m_tenantNumber != null && m_buildingNumber.Length > 0 && m_tenantNumber.Length > 0) {
                int seqnum = 0;
                histMaster = Histmast.load(m_buildingNumber, m_tenantNumber);
				if ((histMaster != null)) {
					if (histMaster.Length == 1) {
						getHist = true;
                        m_tenantId = histMaster[0].SequenceNumber;
					} else if (histMaster.Length > 1) {
                        Tenants[] tens = Tenants.load(txtBuildingNumber.Text, txtTenantNumber.Text, GlobalShared.user.Account.clientId);
                        if (tens != null && tens.Length > 1)
                        {
                            frmChooseTenant frmChoose = new frmChooseTenant() { Tenants = tens };
                            frmChoose.ShowDialog();
                            if (frmChoose.SequenceNumber == -1)
                                getHist = false;
                            else
                            {
                                m_tenantId = frmChoose.SequenceNumber;
                                getHist = true;
                            }
                        }
                        else
                        {
                            m_tenantId = histMaster[0].SequenceNumber;
                            getHist = true;
                        }

					} else {
						getHist = false;
					}
				}
			} else {
				getHist = false;
			}

			if (getHist) {
				displayHistory();
			}

            txtBuildingNumber.Focus();

		}


		private void displayHistory()
		{
			getHistory();
			ListCollectionView histView = null;
			if ((historyList != null)) {
				histView = historyList.ListView;
				histView.GroupDescriptions.Add(new PropertyGroupDescription("HistoryMonthYear"));
				grdHistory.DataContext = histView;
			}
			grdHistory.UpdateLayout();
			if (grdHistory.Items.Count > 0) {
				try {
					grdHistory.ScrollIntoView(grdHistory.Items[grdHistory.Items.Count - 1]);
				} catch (System.Exception ex) {
					return;
				}
			}

		}


		private void getHistory()
		{
			int m = 0;
			int yr = 0;
			int lm = 0;
			string d = "";
			ChargeCodes code = null;
			decimal balance = 0;
            DateTime start = (DateTime) dtStart.SelectedDate;
            DateTime end = (DateTime) dtEnd.SelectedDate;
            start = new DateTime( start.Year, start.Month, start.Day, 0, 0, 0 );
            end = new DateTime( end.Year, end.Month, end.Day, 23, 59, 59 );
			if (m_tenantId > 0) {
				historyList = new HistoryList();
				txtTenantNumber.Text = histMaster[0].TenantNumber;
				txtBuildingNumber.Text = histMaster[0].BuildingNumber;
				txtTenantName.Text = histMaster[0].TenantName;
				txtApartment.Text = histMaster[0].Apartment;

				try {
					histCharge = HistChg.loadByTenantIdDate(m_tenantId,start, end);
				} catch (System.Exception ex) {
					return;
				}
				if ((histCharge != null) && histCharge.Length > 0) {
					foreach (HistChg chg in histCharge) {
						m = DateAndTime.Month(chg.ChargeDate);
						// first time through
						if (lm == 0) {
							lm = m;
							d = Strings.Format(m, "00") + "/" + Strings.Format(DateAndTime.Year(chg.ChargeDate), "0000");
						}
						// end of current month
						if (lm != m) {
							getPayments(yr, lm, balance);
							lm = m;
							d = Strings.Format(m, "00") + "/" + Strings.Format(DateAndTime.Year(chg.ChargeDate), "0000");
							balance = 0;
						}
						yr = DateAndTime.Year(chg.ChargeDate);
						code = ChargeCodes.load(chg.Code,GlobalShared.user.Account.clientId);
						balance += chg.Amount + chg.Arrears;
						historyList.Add((int)HistoryDisplay.RowTypes.Charges, chg.ChargeDate, code.ChargeDescription, chg.Amount, chg.Arrears, null, null, balance);
						d = "";
					}
					getPayments(yr, lm, balance);
				}
			}

		}


		private void getPayments(int yr, int lm, decimal balance)
		{
			//Dim hr As HistoryDisplay = historyList.Item(historyList.Count - 1)
			//hr.Balance = balance
			histPayment = HistPay.loadByTenantDate(m_tenantId, DateAndTime.DateSerial(yr, lm, 1), DateAndTime.DateSerial(yr, lm + 1, -1));
			if ((histPayment != null)) {
				amountPaid = 0;
				foreach (HistPay paid in histPayment) {
					amountPaid += paid.Amount;
                    balance -= paid.Amount;
					historyList.Add((int)HistoryDisplay.RowTypes.Payments, paid.PayDate, null, null, null, paid.Amount, paid.PayDate, balance);
				}
				//hr = historyList.Item(historyList.Count - 1)
				//hr.Balance = balance - amountPaid
			}
		}


		private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();

		}


		private void cmdClear_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			dtStart.Text = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Now) - 1, DateAndTime.Month(DateAndTime.Now), 1).ToString();
			dtEnd.Text = DateAndTime.Now.Date.ToString();

			grdHistory.Items.Clear();

			txtBuildingNumber.Text = "";
			txtTenantName.Text = "";
			txtTenantNumber.Text = "";
			txtApartment.Text = "";

			txtTenantNumber.Focus();

		}


		private void cmdGetHistory_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Mouse.SetCursor(Cursors.Wait);
            m_tenantId = 0;
            getHistory();
            /*
			Tenants[] tens = Tenants.load(txtBuildingNumber.Text, txtTenantNumber.Text,GlobalShared.user.Account.clientId);
			if ((tens == null) || tens.Count() == 0) {
				Interaction.MsgBox("Invalid Tenant Number");
				return;
			}

			if (tens.Count() == 1) {
				m_tenantId = tens[0].SequenceNumber;
			} else {
				frmChooseTenant frm = new frmChooseTenant();
				frm.Tenants = tens;
				frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
				frm.ShowDialog();
				if (frm.SequenceNumber == -1) {
					return;
				}
				m_tenantId = frm.SequenceNumber;
			}

			histMaster = new Histmast[1];
			try {
				histMaster[0] = Histmast.load(m_tenantId);
			} catch (System.Data.SqlClient.SqlException ex) {
				Interaction.MsgBox("No History found for selected tenant.");
				return;
			} catch (System.Exception ex) {
				Interaction.MsgBox("No History found for selected tenant.");
				return;
			}
			if ((histMaster != null)) {
				m_tenantId = histMaster[0].SequenceNumber;
			}

			displayHistory();
            */
            Mouse.SetCursor(Cursors.AppStarting);

		}


		private void cmdPrint_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            rpt.SetFont = new Font("Courier New", 14);
            rpt.LeftMargin = (float)8;
            rpt.TopMargin = (float)10;
            rpt.BottomMargin = (float)55;
            rpt.BottomOffset = (float)65;
            object[,] headers = new object[7, 2];
            headers[0,0] = string.Format("History for Tenant: {0}{1}      " + rpt.PageTag, histMaster[0].BuildingNumber, histMaster[0].TenantNumber);
            headers[0,1] = new Font("Courier New", 14, System.Drawing.FontStyle.Bold );
            headers[1,0] = String.Format("              Name: {0}", histMaster[0].TenantName);
            headers[1,1] = new Font("Courier New", 14, System.Drawing.FontStyle.Bold);
            headers[2, 0] = string.Format("              From: {0:MM/yyyy} to {1:MM/yyyy}", dtStart.SelectedDate, dtEnd.SelectedDate);
            headers[2,1] = new Font("Courier New", 14, System.Drawing.FontStyle.Bold);
            headers[4,0] = string.Format("{0:7} {1:18} {2:10} {3:10} {4:10} {5:7} {6:10}", "Date   ", "Type".PadRight(18), "Amount".PadLeft(10), "Arrears".PadLeft(10), "Paid".PadLeft(8), "Date".PadRight(7), "Balance".PadLeft(12));
            headers[4,1] = new Font("Courier New", 14, System.Drawing.FontStyle.Bold);
            rpt.HeaderLines = headers;
            doPrint();
            rpt.DisplayDocument();

		}


		public void doPrint()
		{
            string lastDate = "";
            rpt.PrintHeader();
			bool firstLine = true;

			foreach (HistoryDisplay histRec in historyList) {
				if (string.IsNullOrEmpty(lastDate.Trim()))
					lastDate = histRec.HistoryMonthYear;

				if (rpt.CurrentHeight >= rpt.MaxPageHeight) {
                    rpt.NewPage();
					rpt.PrintHeader();
				}

				if (histRec.HistoryMonthYear != lastDate) {
                    rpt.PrintPageLine("");
					firstLine = true;
					lastDate = histRec.HistoryMonthYear;
				}

				switch (histRec.RowType) {
					case (int)HistoryDisplay.RowTypes.Charges:
						if (firstLine) {
							rpt.PrintPageLine(string.Format("{0:MM/yyyy} {1:-18} {2:10} {3:10} {4:18} {5:10}", histRec.HistoryMonthYear, histRec.HistoryDescription.PadRight(18), Strings.Format(histRec.Amount, "#####0.00").PadLeft(10), Strings.Format(histRec.Arrears, "#####0.00").PadLeft(10), " ".PadRight(18), Strings.Format(histRec.Balance, "#####0.00").PadLeft(10)));
							firstLine = false;
						} else {
							rpt.PrintPageLine( string.Format("{0:7} {1:-18} {2:10} {3:10} {4:18} {5:10}", " ".PadRight(7), histRec.HistoryDescription.PadRight(18), Strings.Format(histRec.Amount, "#####0.00").PadLeft(10), Strings.Format(histRec.Arrears, "#####0.00").PadLeft(10), " ".PadRight(18), Strings.Format(histRec.Balance, "#####0.00").PadLeft(10)));
						}
						break;
					case (int)HistoryDisplay.RowTypes.Payments:
						rpt.PrintPageLine(string.Format("{0:46} {1:10} {2:MM/yyyy}   {3:10}", " ".PadRight(46), Strings.Format(histRec.Paid, "#####0.00").PadLeft(10), histRec.PaidDate, Strings.Format(histRec.Balance, "#####0.00").PadLeft(10)));
						break;
				}
			}
        }

        private void txtBuildingNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuildingNumber.Text.Trim().Length == 3)
                txtTenantNumber.Focus();
        }

	}
}
