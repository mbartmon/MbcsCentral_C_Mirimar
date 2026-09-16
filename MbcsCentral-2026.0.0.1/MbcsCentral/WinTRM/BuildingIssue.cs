using System.Drawing.Printing;
using System.Reflection;
namespace WinTRM
{

	public class BuildingIssue : IssueReport
	{

		private const float locationX = 0.6;
		private float locationY = 0.2;
		private const float vendorX = 3.8;

		private float vendorY = 0.2;
		private const float reportedX = 0.6;
		private float reportedY = 0.4;
		private const float assignedX = 4.0;

		private float assignedY = 0.4;
		private const float statusX = 0.6;
		private float statusY = 0.6;
		private const float priorityX = 2.8;

		private float priorityY = 0.6;


		public BuildingIssue() : base()
		{
			if (_recordKey <= 0)
				return;

			prDoc.BeginPrint += printStart;
			prDoc.PrintPage += printPages;
			issue = Issues.load(_recordKey);
			if ((issue == null))
				return;
			issueUnits = IssueTenants.loadByNote(_recordKey);
			nts = Notes.loadByIssueReport(issue.issueId);
			nMax = 0;
			nRow = 0;
			if ((nts != null)) {
				if (nts.Length > 0) {
					nMax = nts.Length - 1;
				}
			}
			doPrint();

		}


		private void doPrint()
		{
			switch (_printMode) {
				case 0:
					prDiag.Show();
					prDiag.BringToFront();
					break;
				case 1:
					prDoc.Print();

					break;
			}

		}


		public void printPages(object sender, PrintPageEventArgs e)
		{
			e.HasMorePages = false;
			printNewPage(sender, e);

			unitTxtY += 0.4f;
			notesY = notesYStart;
			e.Graphics.DrawString("Notes:", bodyFont, br, notesX, unitTxtY);

			string[] noteLines = null;
			while (nRow < nts.Length) {
				note = nts(nRow);
				noteLines = buildNoteLines(note.getnote);
				if ((noteLines != null)) {
					if (unitTxtY + (noteLines.Length * 0.2f) > maxVert) {
						e.HasMorePages = true;
						unitTxtY = theLine;
						return;
					}
					Accounts account = Accounts.load(issue.getAccountId);
					e.Graphics.DrawString(note.gettimestamp + " / " + ((account == null) ? "" : account.getName).ToString, bodyFont, br, notesX + 1f, unitTxtY);
					unitTxtY += 0.2f;
					for (int i = 0; i <= noteLines.Length - 1; i++) {
						e.Graphics.DrawString(noteLines(i), bodyFont, br, notesX + 1f, unitTxtY);
						unitTxtY += 0.2f;
					}
					e.Graphics.DrawString("====================", bodyFont, br, notesX + 1f, unitTxtY);
					unitTxtY += 0.4f;
				}
				nRow += 1;
			}
			nRow = 0;

		}


		public void printStart(object sender, PrintEventArgs e)
		{
			headerFont = new System.Drawing.Font("Courier New", 14, System.Drawing.FontStyle.Bold);
			bodyFont = new System.Drawing.Font("Courier New", 12);

			br = System.Drawing.Brushes.Black;

			pg = 1;

		}


		public void printNewPage(object sender, PrintPageEventArgs e)
		{
			e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Inch;
			e.Graphics.DrawRectangle(p, 0.5f, 0.5f, 7.5f, 10f);
			e.Graphics.DrawLine(p, Convert.ToSingle(0.5), Convert.ToSingle(1.0), Convert.ToSingle(8.0), Convert.ToSingle(1.0));
			e.Graphics.DrawString(Strings.Format(Now(), "MM/dd/yyy"), bodyFont, br, dateX, dateY);
			e.Graphics.DrawString("Building Issue", headerFont, br, Convert.ToSingle(3.2), Convert.ToSingle(0.7));
			e.Graphics.DrawString("Page " + pg, bodyFont, br, pageNoX, pageNoY);
			pg += 1;
			e.Graphics.DrawString("Issue ID", bodyFont, br, issueLblX, issueLblY);
			e.Graphics.DrawRectangle(p, 1.5f, 1.1f, 0.75f, 0.25f);
			e.Graphics.DrawString(_recordKey, bodyFont, br, issueIdX, issueIdY);
			e.Graphics.DrawString("Subject", bodyFont, br, subjectLblX, subjectLblY);
			e.Graphics.DrawRectangle(p, 3.67f, 1.1f, 4f, 0.25f);
			e.Graphics.DrawString(issue.subject, bodyFont, br, subjectTxtX, subjectTxtY);
			e.Graphics.DrawString("Category:", bodyFont, br, categoryX, categoryY);
			e.Graphics.DrawString(issue.getCategory, bodyFont, br, headerCommonX, categoryY);
			e.Graphics.DrawString("Buildings:", bodyFont, br, unitsLblX, unitsLblY);
			unitTxtY = unitTxtYStart;
			if ((issueUnits != null)) {
				int i = 0;
				Building bldg = null;
				foreach (IssueTenants bld in issueUnits) {
					bldg = Building.load(bld.buildingNumber);
					if ((bldg != null)) {
						unitTxtY += i * 0.2;
						e.Graphics.DrawString(bldg.BuildingNumber + "-> " + bldg.Address1, bodyFont, br, headerCommonX, unitTxtY);
						i += 1;
					}
					bldg = null;
				}
			}
			unitTxtY += 0.4f;
			e.Graphics.DrawString("Location:", bodyFont, br, locationX, unitTxtY);
			BuildingLocation location = BuildingLocation.load(issue.getLocationId);
			e.Graphics.DrawString(((location == null) ? "" : location.Location), bodyFont, br, headerCommonX, unitTxtY);
			e.Graphics.DrawString("Vendor:", bodyFont, br, vendorX, unitTxtY);
			Vendors vendor = Vendors.load(issue.getVendorId);
			e.Graphics.DrawString(((vendor == null) ? "" : vendor.VendorName), bodyFont, br, vendorX + 1f, unitTxtY);
			unitTxtY += 0.2f;
			e.Graphics.DrawString("Reported: ", bodyFont, br, reportedX, unitTxtY);
			e.Graphics.DrawString((issue.getReported > 1/1/1900 00:00:00 ? issue.getReported : ""), bodyFont, br, reportedX + 1f, unitTxtY);
			e.Graphics.DrawString("Assigned:", bodyFont, br, assignedX, unitTxtY);
			e.Graphics.DrawString((issue.getAssigned > 1/1/1900 00:00:00 ? issue.getAssigned : ""), bodyFont, br, assignedX + 1f, unitTxtY);
			unitTxtY += 0.4f;
			e.Graphics.DrawLine(p, 0.5f, unitTxtY, 8f, unitTxtY);
			theLine = unitTxtY;

		}
	}
}
