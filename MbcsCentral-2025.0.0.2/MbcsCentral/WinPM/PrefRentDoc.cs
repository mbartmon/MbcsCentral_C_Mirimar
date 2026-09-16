using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Printing;
using System.Windows.Forms;

namespace MbcsCentral.WinPM
{
	public class PrefRentDoc : IDisposable
	{

//*******************
//* Print Positions *
//*******************
        Font mainFont = new Font("Courier New", 12);
        Report rpt = null;
        private System.Windows.Controls.Image template;
   
        // Print Positions
        const double leaseStartX = 5.5;
		const double leaseStartY = 0.6;
		const double ownNameX = 1.3;
		const double ownNameY = 1.0;
		const double tenNameX = 1.3;
		const double tenNameY = 1.35;
		const double bldAddX = 2.5;
		const double bldAddY = 1.75;
		const double aptX = 6.7;
		const double aptY = 1.75;

		const double prefRent1X = 4.25;
		const double prefRent1Y = 2.90;

		const double prefRent2X = 4.25;
		const double prefRent2Y = 3.20;

        const double Yr2_1X = 2.4;
        const double Yr2_1Y = 8.34;
		const double Yr2_2X = 2.4;
		const double Yr2_2Y = 8.71;

        //

        const double lglRent1Xa = 4.25;
		const double lglRent1Ya = 4.05;

		const double lglRent2Xa = 4.25;
		const double lglRent2Ya = 4.30;

        //

		const double lglRent1Xb = 4.25;
        const double lglRent1Yb = 5.75;

		const double lglRent2Xb = 4.25;
		const double lglRent2Yb = 6.05;

        //

		const double name1X = 1.3;
		const double name1Y = 9.2;
		const double name2X = 1.3;
		const double name2Y = 9.75;

		const double lndNameX = 4.5;
		const double lndNameY = 8.05;

		//
		const double asteriskX = 0;
		const double asteriskY = 0;
		const double specMsgX = 0;
		const double specMsgY = 0;
       
		private string mvartenantNumber;
		private string mvarTenantName;
		private string mvarApartment;
		private decimal mvarProposedRent1;
		private decimal mvarProposedRent2;
		private decimal mvarProposedRent2_1;
		private decimal mvarLegalRent1;
		private decimal mvarLegalRent2;
		private string mvarName1;
		private string mvarName2;

        public void Dispose()
        {
            mainFont.Dispose();
        }
		public string Name2 {
			get { return mvarName2; }
			set { mvarName2 = value; }
		}

		public string Name1 {
			get { return mvarName1; }
			set { mvarName1 = value; }
		}

		public decimal LegalRent2 {
			get { return mvarLegalRent2; }
			set { mvarLegalRent2 = value; }
		}

		public decimal LegalRent1 {
			get { return mvarLegalRent1; }
			set { mvarLegalRent1 = value; }
		}

		public decimal ProposedRent2 {
			get { return mvarProposedRent2; }
			set { mvarProposedRent2 = value; }
		}
		public decimal ProposedRent1 {
			get { return mvarProposedRent1; }
			set { mvarProposedRent1 = value; }
		}
		public decimal ProposedRent2_1
		{
			get { return mvarProposedRent2_1; }
			set { mvarProposedRent2_1 = value; }
		}
		public string Apartment {
			get { return mvarApartment; }
			set { mvarApartment = value; }
		}
		public string TenantName {
			get { return mvarTenantName; }
			set { mvarTenantName = value; }
		}
		public string TenantNumber {
			get { return mvartenantNumber; }
			set { mvartenantNumber = value; }
		}


		public PrefRentDoc()
		{
            string filename = "";
            object obj = null;
            Main.parms.getItem(GlobalShared.CONFIG_WINPM_PREFRENT_FILE, ref obj);
            filename = GlobalShared.appPath + "WinPM\\Files\\" + (string)obj;
            rpt = new Report("PrefRentDoc", mainFont.Name, mainFont.Size);
            rpt.TopMargin = 0;
            rpt.LeftMargin = 0;
            rpt.BottomMargin = 0;
            template = Report.GetImage(filename);
            rpt.LineHeight = 0.15;
        }

        public void printForm(string leaseStart, string ownName, string bldAddress, PrintQueue prt)
		{

            DateTime lDate = default(System.DateTime);

			leaseStart = Strings.Left(leaseStart, 2) + "/" + Strings.Mid(leaseStart, 5, 2) + "/" + Strings.Right(leaseStart, 4);
			lDate = DateTime.Parse(leaseStart);

            rpt.AddImage(template);
            rpt.SetFont = mainFont;

            rpt.FontBold = true;
			rpt.PrintPageLine(lDate.ToString("MMMM dd, yyyy"), leaseStartX, leaseStartY);
			rpt.PrintPageLine(ownName, ownNameX, ownNameY);
			rpt.PrintPageLine(mvarName1.Trim() + (!string.IsNullOrEmpty(mvarName2.Trim()) ? " & " + mvarName2.Trim() : ""), tenNameX, tenNameY);
			rpt.PrintPageLine(bldAddress, bldAddX, bldAddY);
			rpt.PrintPageLine(mvarApartment, aptX, aptY);
			rpt.PrintPageLine(mvarProposedRent1.ToString("##,##0.00").PadLeft(9), prefRent1X, prefRent1Y);
			rpt.PrintPageLine(mvarProposedRent2.ToString("#,##0.00").PadLeft(9), prefRent2X, prefRent2Y);
			rpt.PrintPageLine();
			rpt.PrintPageLine(mvarLegalRent1.ToString("##,##0.00").PadLeft(9), lglRent1Xa, lglRent1Ya);
			rpt.PrintPageLine(mvarLegalRent2.ToString("##,##0.00").PadLeft(9), lglRent2Xa, lglRent2Ya);
			rpt.PrintPageLine(mvarLegalRent1.ToString("##,##0.00").PadLeft(9), lglRent1Xb, lglRent1Yb);
			rpt.PrintPageLine(mvarLegalRent2.ToString("##,##0.00").PadLeft(9), lglRent2Xb, lglRent2Yb);
			rpt.FontColor=System.Windows.Media.Colors.Red;
			rpt.PrintPageLine(mvarName1 + " sign here X:", name1X, name1Y);
			if (!string.IsNullOrEmpty(Strings.Trim(mvarName2))) {
				rpt.PrintPageLine(mvarName2 + " sign here X:", name2X, name2Y);
			}
			rpt.FontColor = System.Windows.Media.Colors.Black;
			//rpt.PrintPageLine(mvarProposedRent2_1.ToString("##,##0.00").PadLeft(9), Yr2_1X, Yr2_1Y);
			//rpt.PrintPageLine(mvarProposedRent2.ToString("##,##0.00").PadLeft(9), Yr2_2X, Yr2_2Y);
			//rpt.PrintPageLine("$_______________", Yr2_2X - 0.07, Yr2_2Y + 0.08);
			rpt.PrintPageLine(ownName, lndNameX, lndNameY);
			rpt.PrintDocument(prt);

			//if (asteriskX > 0 || asteriskY > 0)
			//	rpt.PrintPageLine("**", asteriskX, asteriskY);
			//if (specMsgX > 0 || specMsgY > 0)
			//	rpt.PrintPageLine(WinpmGlobal.PREFERENTIAL_SPECIAL_MESSAGE, specMsgX, specMsgY);

            rpt.FontBold = false;
		}
	}
}
