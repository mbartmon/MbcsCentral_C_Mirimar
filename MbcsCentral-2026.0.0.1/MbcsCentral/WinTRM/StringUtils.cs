using System.Text.RegularExpressions;
using MBCSsmtp;
using System.IO;
using System;
using System.Collections;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinTrm
{
	//Imports Email.Net.Smtp
	//Imports Email.Net.Common

	static class StringUtils
	{
		public static string wrapRecipIds(int[] ids)
		{
			if (ids.Length <= 0) {
				return null;
			}

			string s = "";
			for (int i = 0; i <= ids.Length - 1; i++) {
				if (i > 0) {
					s += ",";
				}

				s += ids[i].ToString();
			}

			return s;
		}

		public static int[] unwrapRecipIds(string s)
		{
			if (s == null || s.Length == 0) {
				return null;
			}

			char[] d = null;
			d = new char[1];
			d[0] = ',';
			string[] idsStr = s.Split(d);

			int[] ids = new int[idsStr.Length];
			try {
				for (int i = 0; i <= idsStr.Length - 1; i++) {
					ids[i] = int.Parse(idsStr[i]);
				}
			} catch {
			}

			return ids;
		}

		public static bool EmailAddressCheck(string emailAddress)
		{
			bool functionReturnValue = false;

			string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
			Match emailAddressMatch = Regex.Match(emailAddress, pattern);

			if (emailAddressMatch.Success) {
				functionReturnValue = true;
			} else {
				functionReturnValue = false;
			}
			return functionReturnValue;

		}


		public static void parseMailToken(string token, ref string[] values)
		{
			string s = null;
			try {
				s = token.Substring(token.LastIndexOf("<") + 1);
				s = s.Substring(0, s.IndexOf(">") - 1);
				values = s.Split('.');
			} catch (System.Exception ex) {
				values = new string[1];
				return;
			}

		}

		public static string[] parseFilename(string filename)
		{

			string[] tokes = Path.GetFileName(filename).Split('_');
			return tokes;

		}

		public static void outlookUtil()
		{
			Microsoft.Office.Interop.Outlook.Application objOutlook = default(Microsoft.Office.Interop.Outlook.Application);
			objOutlook = new Microsoft.Office.Interop.Outlook.Application();
			Microsoft.Office.Interop.Outlook.NameSpace objNS = objOutlook.Session;

			//---------Write to Shared calendar-----------------

			//Dim objFolder As Microsoft.Office.Interop.Outlook.MAPIFolder = objNS.Folders.Item("Public Folders")
			Microsoft.Office.Interop.Outlook.MAPIFolder objFolder = objNS.Folders["Personal Folders"];

			//objFolder = objFolder.Folders.Item("All Public Folders")
			//objFolder = objFolder.Folders.Item("Boss Calendar")
			objFolder = objFolder.Folders["Calendar"];

			//=========Add appointment to calendar=================
			Microsoft.Office.Interop.Outlook.AppointmentItem objApp = default(Microsoft.Office.Interop.Outlook.AppointmentItem);
			objApp = (Microsoft.Office.Interop.Outlook.AppointmentItem)objFolder.Items[Microsoft.Office.Interop.Outlook.OlItemType.olAppointmentItem];

			var _with1 = objApp;
			_with1.Subject = (" Test appointment");
			_with1.Location = "Main Office";
			_with1.Body = ("test appointment");
			_with1.Start = Convert.ToDateTime("19.5.2009 10:00");
			// NOTICE: This is my locale date format
			// Added a reminder
			_with1.ReminderMinutesBeforeStart = 60;
			_with1.ReminderSet = true;
			// Added duration
			_with1.Duration = 120;
			// Add logged in Windows user
			_with1.RequiredAttendees = Environment.UserName;
			// Save first
			_with1.Save();
			// Send to logged in Windows user
			_with1.Send();
			// NOTICE: Microsoft.Office.Interop.Outlook security settings may prevent this

		}

		public static EmailMessage[] getEmailMessages(bool inbound, long issueId, long account = 0, bool getMsg = false)
		{

			ArrayList email = new ArrayList();
			string dirPath = (inbound ? GlobalShared.mailInDir : GlobalShared.mailOutDir);
			string filenames = string.Format("{1}_{2}_*.txt", (inbound ? GlobalShared.mailInDir : GlobalShared.mailOutDir), (account == 0 ? "*" : account.ToString("00000")), (issueId == 0 ? "*" : issueId.ToString("00000")));
			System.Collections.ObjectModel.ReadOnlyCollection<string> files = Microsoft.VisualBasic.FileIO.FileSystem.GetFiles(dirPath, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, filenames);
			ArrayList sortArray = new ArrayList();
			foreach (string file in files) {
				sortArray.Add(file);
			}
			sortArray.Sort();

			foreach (string filefound in Microsoft.VisualBasic.FileIO.FileSystem.GetFiles(dirPath, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, filenames)) {
				email.Add(parseEmailFile(filefound, getMsg));
			}
			return (EmailMessage[])email.ToArray(typeof(EmailMessage));

		}

		public static EmailMessage parseEmailFile(string filename, bool getMessage = false)
		{

			EmailMessage email = null;

			try {
				string line = null;
				bool inMsg = false;
				string msg = "";
				System.IO.StreamReader sr = new System.IO.StreamReader(filename);
				email = new EmailMessage();
				email.filename = filename;
				while (!sr.EndOfStream) {
					line = sr.ReadLine();
					if (inMsg) {
						msg += line + Constants.vbCrLf;
					} else {
						if (line.ToLower().StartsWith("to:")) {
							email.toAddr = line.Substring(3).Trim();
						}
						if (line.ToLower().StartsWith("from:")) {
							email.fromAddr = line.Substring(5).Trim();
						}
						if (line.ToLower().StartsWith("bcc:")) {
							email.bcc = line.Substring(4).Trim();
						}
						if (line.ToLower().StartsWith("cc:")) {
							email.bcc = line.Substring(3).Trim();
						}
						if (line.ToLower().StartsWith("date:")) {
							email.receiveDate = Convert.ToDateTime(line.Substring(5).Trim());
						}
						if (line.ToLower().StartsWith("subject:")) {
							email.subject = line.Substring(8).Trim();
						}
						if (getMessage & line.ToLower().StartsWith("message:")) {
							inMsg = true;
						}
					}
				}
				if (!string.IsNullOrEmpty(msg.Trim())) {
					email.message = msg;
				}
				sr.Close();
			} catch {
				return null;
			}
			return email;

		}
		public static string saveEmail(Chilkat.Email msg, long issue, string uid)
		{

			string filename = "";
			if ((GlobalShared.mailInDir != null) & !string.IsNullOrEmpty(GlobalShared.mailInDir.Trim())) {
				System.IO.StreamWriter sw = default(System.IO.StreamWriter);
				try {
					filename = string.Format(GlobalShared.popFilename, GlobalShared.mailInDir, MbcsCentral.GlobalShared.user.Account.accountId, issue, DateTime.Now, uid, "txt").Replace("<", "").Replace(">", "");
					sw = new System.IO.StreamWriter(filename, false);
					sw.Write(messageFormat(msg));
					sw.Flush();
				} catch (System.IO.IOException ex) {
					GlobalShared.Log.Log((int)LogClass.logType.Debug, "Unable to save email message <" + filename + ": " + ex.Message, false, System.Windows.Forms.MessageBoxButtons.OK);
				} catch (System.Exception ex) {
					GlobalShared.Log.Log((int)LogClass.logType.Debug, "Error saving email message" + filename + ": " + ex.Message, false, System.Windows.Forms.MessageBoxButtons.OK);
				} finally {
					try {
						sw.Close();
					} catch {
					}
				}
			}
			return filename;

		}

		public static string messageFormat(Chilkat.Email msg)
		{

			string outString = "";
			outString = "From: " + msg.FromAddress + Constants.vbCrLf;
			outString += "To:      ";
			for (int i = 0; i <= msg.NumTo - 1; i++) {
				outString += msg.GetToAddr(i) + ", \"";
			}
			outString += Constants.vbCrLf;
			bool cc = false;
			for (int i = 0; i <= msg.NumCC - 1; i++) {
				if (!cc) {
					outString += "Bcc:   ";
					cc = true;
				}
				outString += msg.GetCcAddr(i) + ", ";
			}
			for (int i = 0; i <= msg.NumBcc - 1; i++) {
				if (!cc) {
					outString += "Bcc:   ";
					cc = true;
				}
				outString += msg.GetBccAddr(i) + ", ";
			}
			outString += "Date:    " + msg.EmailDate + Constants.vbCrLf;
			outString += "Subject: " + msg.Subject + Constants.vbCrLf;
			outString += "Message: " + Constants.vbCrLf + msg.Body + Constants.vbCrLf;
			return outString;

		}
		public static string saveEmail(Chilkat.Email msg, long issue)
		{

			string filename = "";
			if ((GlobalShared.mailOutDir != null) & !string.IsNullOrEmpty(GlobalShared.mailOutDir.Trim())) {
				filename = string.Format(GlobalShared.mailFilename, GlobalShared.mailOutDir, MbcsCentral.GlobalShared.user.Account.accountId, issue, DateTime.Now, "txt");
				System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, false);
				sw.Write(messageFormat(msg));
				sw.Flush();
				sw.Close();
			}
			return filename;

		}

		//Public Function messageFormat(msg As SmtpMessage) As String

		//Dim outString As String = ""
		//outString = "From: " & msg.From.Address & vbCrLf
		//outString &= "To:      "
		//For Each addr As Email.Net.Common.EmailAddress In msg.To
		//outString &= addr.Address & ", """
		//Next
		//outString &= vbCrLf
		//Dim cc As Boolean = False
		//For Each addr As Email.Net.Common.EmailAddress In msg.CarbonCopies
		//If Not cc Then
		//outString &= "Bcc:   "
		//cc = True
		//End If
		//outString &= addr.Address & ", "
		//Next
		//For Each addr As Email.Net.Common.EmailAddress In msg.BlindedCarbonCopies
		//If Not cc Then
		//outString &= "Bcc:   "
		//cc = True
		//End If
		//outString &= addr.Address & ", "
		//Next
		//outString &= "Date:    " & msg.Date & vbCrLf
		//outString &= "Subject: " & msg.Subject & vbCrLf
		//outString &= "Message: " & vbCrLf & msg.Text & vbCrLf
		//Return outString

		//End Function

		public static byte[] StrToByteArray(string str)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			return encoding.GetBytes(str);
		}

		public static string ByteArrayToStr(byte[] dBytes)
		{
			string str = null;
			System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
			str = enc.GetString(dBytes);
			return str;
		}

		//Public Function findIssueTag(msg As Rfc822Message) As String

		//Dim tag As String = "", found As Boolean = False

		//If regx.IsMatch(msg.Subject) Then
		//found = True
		//Return parseIssueTag(msg.Subject)
		//End If
		//If msg.Header.ExtraHeaders.Count > 0 Then
		//For Each hdr As System.Collections.Generic.KeyValuePair(Of String, String) In msg.Header.ExtraHeaders
		//If hdr.Key.Equals(ISSUE_HEADER) Then
		//found = True
		//tag = hdr.Value
		//Exit For
		//End If
		// Next
		//End If
		//Return tag

		//End Function
		public static string findIssueTag(Chilkat.Email msg)
		{

			string tag = "";
			bool found = false;

			if (GlobalShared.regx.IsMatch(msg.Subject)) {
				found = true;
				return parseIssueTag(msg.Subject);
			}
			if (msg.NumHeaderFields > 0) {

				for (int i = 0; i <= msg.NumHeaderFields - 1; i++) {
					if (msg.GetHeaderFieldName(i).Equals(GlobalShared.ISSUE_HEADER)) {
						found = true;
						tag = msg.GetHeaderFieldValue(i);
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}
			return tag;

		}

		public static string parseIssueTag(string s)
		{

			System.Text.RegularExpressions.Match part = GlobalShared.regx.Match(s);
			return part.ToString();

		}

		public static bool searchUID(string uid)
		{

			bool found = false;
			if (!string.IsNullOrEmpty(GlobalShared.mailInDir.Trim())) {
				string[] fnames = null;
				try {
					fnames = Directory.GetFiles(GlobalShared.mailInDir, "*_*_*_" + uid + ".*");
				} catch (IOException ioEx) {
				} catch (System.Exception ex) {
				}
				if ((fnames != null)) {
					foreach (string fname in fnames) {
						if ((fname != null)) {
							if (!string.IsNullOrEmpty(fname.Trim())) {
								found = true;
							}
						}
					}
				}
			}
			return found;

		}

	}
}
