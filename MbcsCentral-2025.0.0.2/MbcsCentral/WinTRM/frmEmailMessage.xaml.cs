namespace WinTRM
{
	public class frmEmailMessage
	{

		public string fromAddr {
			set { lblFrom.Content = value; }
		}
		public string toAddr {
			set { lblTo.Content = value; }
		}
		public string Bcc {
			set { lblBcc.Content = value; }
		}
		public System.DateTime emailDate {
			set { lblDate.Content = value.ToLongDateString; }
		}
		public string Subject {
			set { lblSubject.Content = value; }
		}
		public string Message {
			set { txtMessage.Text = value; }
		}

	}
}
