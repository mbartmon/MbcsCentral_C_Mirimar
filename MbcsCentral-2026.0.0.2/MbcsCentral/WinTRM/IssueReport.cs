using System.Drawing.Printing;
using System.Windows.Forms;
namespace WinTRM
{

	public class IssueReport
	{

		protected int _printMode;
		protected string _printerName;

		protected long _recordKey;
		protected Issues issue;
		protected IssueTenants[] issueUnits;
		protected Notes[] nts;
		protected Notes note;
		protected int nRow;

		protected int nMax;
			// 10 chars/inch x 6 inches
		protected int notesCharsPerLine = 60;


		protected System.Windows.Forms.PrintPreviewDialog prDiag;
		protected PrintDocument prDoc;
		protected int pg;
		protected System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Black, Convert.ToSingle(0.05));
		protected System.Drawing.Font headerFont;
		protected System.Drawing.Font bodyFont;

		protected System.Drawing.Brush br;
		protected const float dateX = 0.6;
		protected const float dateY = 0.7;
		protected const float headerX = 3.0;
		protected const float headerY = 0.7;
		protected const float pageNoX = 6.75;

		protected const float pageNoY = 0.7;
		protected const float issueLblX = 0.6;
		protected const float issueLblY = 1.125;
		protected const float issueIdX = 1.51;

		protected const float issueIdY = 1.125;
		protected const float subjectLblX = 2.8;
		protected const float subjectLblY = 1.125;
		protected const float subjectTxtX = 3.675;

		protected const float subjectTxtY = 1.125;
		protected const float categoryX = 0.6;

		protected float categoryY = 1.55;
		protected const float unitsLblX = 0.6;
		protected const float unitsLblY = 1.75;
		protected const float unitTxtX = 1.7;
		protected const float unitTxtYStart = 1.75;

		protected float unitTxtY;
		protected const float notesX = 0.6;
		protected const float notesYStart = 0f;

		protected float notesY;
		protected float theLine;
		protected float headerCommonX = 1.75;

		protected float maxVert = 9.5;


		public IssueReport()
		{
			_printMode = GlobalShared.args(0);
			_printerName = GlobalShared.args(1);
			_recordKey = GlobalShared.args(2);

			if (_printMode == 1 & string.IsNullOrEmpty(_printerName.Trim))
				return;
			prDoc = new PrintDocument();
			prDoc.PrinterSettings.PrinterName = _printerName;

			switch (_printMode) {
				case 0:
					prDiag = new System.Windows.Forms.PrintPreviewDialog();
					prDiag.Document = prDoc;
					System.Drawing.Rectangle rect = Forms.Screen.PrimaryScreen.WorkingArea;
					prDiag.Height = rect.Height * 0.75;
					prDiag.Width = prDiag.Height * (8.5 / 11);
					prDiag.StartPosition = FormStartPosition.CenterScreen;
					break;
				case 1:
					break;
			}

		}


		public string[] buildNoteLines(string txt)
		{

			ArrayList lineArray = new ArrayList();
			string curLine = "";
			string[] lines = null;
			string[] words = txt.Split(" ");
			string x = "";

			foreach (string s in words) {
				if (curLine.Length + s.Length > notesCharsPerLine) {
					lineArray.Add(curLine);
					curLine = "";
				}
				curLine += s + " ";
			}
			if (curLine.Length > 0) {
				lineArray.Add(curLine);
			}

			if ((lineArray != null)) {
				if (lineArray.Count > 0) {
					lines = lineArray.ToArray(typeof(string));
				}
			}

			return lines;

		}
	}
}
