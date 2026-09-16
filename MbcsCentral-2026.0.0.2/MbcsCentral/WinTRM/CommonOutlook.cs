using System;
using System.Collections.Generic;
using System.Text;
using OutlookApp = Microsoft.Office.Interop.Outlook;

namespace WinTRM.Common.Outlook
{

	// Provides functionality so Outlook can be used to generate Emails.  Use the
	// write event to log the email.
	// Email outlookMsg = new Email();
	// outlookMsg.Message.Subject = "Foo bar";
	// outlookMsg.Show();
	public class Email
	{
		// Gets the current instance of Outlook

		Microsoft.Office.Interop.Outlook.Application _outlookInstance;

		private OutlookApp.MailItem _message;
		public OutlookApp.MailItem Message {
			get { return this._message; }
			set { this._message = value; }
		}

		// Constructor, gets current outlook instance
		public Email()
		{
			try {
				_outlookInstance = new Microsoft.Office.Interop.Outlook.Application();
			} catch (Exception ex) {
			}

		}


		private void Initialize()
		{
			// create a blank email
			_message = _outlookInstance.CreateItem(OutlookApp.OlItemType.olMailItem);
			// wire up the write event for logging
			_message.Write += new Microsoft.Office.Interop.Outlook.ItemEvents_10_WriteEventHandler(Message_Write);

		}

		//  Used for logging after the end user presses the send
		//  button in Outlook.  If you need to log the email that was
		//  sent to a web service or something else, fill this in.  This is
		//  called after the email is sent via Outlook.
		public void Message_Write(ref bool Cancel)
		{
			// ADD LOGGING HERE IF YOU NEED IT
		}

		// Displays the outlook screen and shows the email message.

		public void Show()
		{
			_message.Display(false);

		}


		public void getFolders(ref ArrayList folders)
		{
			OutlookApp.Folder root = _outlookInstance.Application.Session.DefaultStore.GetRootFolder();
			enumerateFolders(root, ref folders);

		}


		protected void enumerateFolders(OutlookApp.Folder folder, ref ArrayList folders)
		{
			OutlookApp.Folders childFolders = folder.Folders;
			if (childFolders.Count > 0) {
				foreach (OutlookApp.Folder childFolder in childFolders) {
					// Write the folder path.
					try {
						folders.@add(childFolder.Name);
					} catch (Exception ex) {
					}
					// Call EnumerateFolders using childFolder.
					enumerateFolders(childFolder, ref folders);
				}
			}

		}

	}
}
