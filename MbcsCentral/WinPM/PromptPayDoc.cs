using System;
using System.Drawing;
using System.Printing;
using System.Windows;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinPM
{
	internal class PromptPayDoc
	{
		public void printForm(string tnam, string nam1, string nam2, Tenants tblTenant, string ownName, string[] bldAddress, string leaseDate, PrintQueue prt)
		{

			System.DateTime lDate = default(System.DateTime);

            object obj = new FontSizeConverter().ConvertFrom("12pt");
            float size = float.Parse(obj.ToString());;
			Font fnt = new Font("Microsoft Sans Serif", size );
			
            Report rpt = new Report("Prompt Pay",fnt.Name,fnt.Size);
            rpt.TopMargin = 10;
            rpt.LeftMargin = 30;
            rpt.SetFont = fnt;
            rpt.LineHeight = .22 * 96;

			string leaseStart = leaseDate.Substring(0, 2) + "/" + leaseDate.Substring(4, 2) + "/" + leaseDate.Substring(8, 4);
			lDate = DateTime.Parse(leaseStart);
            rpt.FontBold = true;
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(52) + "LEASE EXTENSION RIDER");
			rpt.FontBold = false;
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "Landlord:    " + ownName);
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "Tenant:      " + nam1.Trim() + (!string.IsNullOrEmpty(nam2.Trim()) ? " & " + nam2.Trim() : ""));
			rpt.PrintPageLine(Strings.Space(19) + bldAddress[0] + " - Apt: " + tblTenant.Apartment);
			rpt.PrintPageLine(Strings.Space(19) + bldAddress[1]);
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "       This Rider is applicable to lease extension commencing " + lDate.ToString( "MMMM dd, yyyy"));
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "        It is agreed that if tenant's rent is received at the Landlord's office ON OR BEFORE");
			rpt.PrintPageLine(Strings.Space(1) + "the fifth (5th) day of each month, tenant may deduct the following:");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "                 If tenant signs a one (1) year lease - " + tblTenant.PromptPay1.ToString( "$##,##0.00").PadLeft(10));
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "                 If tenant signs a two (2) year lease - " + tblTenant.PromptPay2.ToString( "$##,##0.00").PadLeft( 10));
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "        This clause shall be ", false);   // prt.Write
			rpt.FontBold = true;
			rpt.PrintPageLine("strictly enforced ", false); // prt.Write
			rpt.FontBold = false;
			rpt.PrintPageLine("and there shall be no allowance if rent");
			rpt.PrintPageLine(Strings.Space(1) + "is received after the fifth (5th) day of the month. This agreement applies only to the");
			rpt.PrintPageLine(Strings.Space(1) + "lease extension commencing " + leaseStart + ", and will not apply to future extensions.");
			rpt.PrintPageLine(Strings.Space(1), false);      // prt.Write
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.FontColor=System.Windows.Media.Colors.Red;
			rpt.PrintPageLine(Strings.Space(1) + Strings.Trim(nam1) + " sign here X:", false);   // was prt.write to prevent line feed.
			rpt.FontColor = System.Windows.Media.Colors.Black;
			rpt.PrintPageLine("____________________________________");
			if (!string.IsNullOrEmpty(Strings.Trim(nam2))) {
				rpt.PrintPageLine("");
				rpt.PrintPageLine("");
				rpt.PrintPageLine("");
				rpt.FontColor = System.Windows.Media.Colors.Red;
				rpt.PrintPageLine(Strings.Space(1) + Strings.Trim(nam2) + " sign here X:", false);      // prt.Write
				rpt.FontColor = System.Windows.Media.Colors.Black;
				rpt.PrintPageLine("____________________________________");
			}
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + ownName);
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine("");
			rpt.PrintPageLine(Strings.Space(1) + "____________________________________");
			rpt.PrintPageLine(Strings.Space(1) + "Landlord Signature");
			rpt.PrintDocument(prt);

		}

	}
}
