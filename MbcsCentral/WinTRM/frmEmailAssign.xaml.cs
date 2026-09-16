using System.Collections.Generic;
namespace WinTRM
{

	public class frmEmailAssign
	{


		private List<EmailMessage> msgs;

		private void frmEmailAssign_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Generic.SortedList<string, Chilkat.Email> messages = Utils.getSmtpMessages(new SmtpConfig());
			if ((messages != null)) {
				if (messages.Count > 0) {
					GlobalShared.Log.Log(LogClass.logType.Info, "About to process messages");
					foreach (KeyValuePair<string, Chilkat.Email> msg in messages) {
						if (!string.IsNullOrEmpty(findIssueTag(msg.Value))) {
							// msgs.Add(New EmailMessage(msg.Key, msg.Value))
						}
					}
				}
			} else {
				GlobalShared.Log.Log(LogClass.logType.Info, "No SMTP messages found");
			}

		}
		public frmEmailAssign()
		{
			Loaded += frmEmailAssign_Loaded;
		}
	}
}
