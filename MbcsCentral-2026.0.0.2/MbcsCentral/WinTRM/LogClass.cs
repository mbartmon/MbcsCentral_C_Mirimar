using Microsoft.VisualBasic;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
namespace WinTRM
{

	/// <summary>
	/// Class for creating and writing an application log file
	/// </summary>
	/// <remarks></remarks>
	public class LogClass
	{

		public static string[] logTypes = {
			"INFO",
			"CONFIG",
			"WARNING",
			"ERROR",
			"DEBUG"
		};
		/// <summary>
		/// Enumeration that provides log type names
		/// </summary>
		/// <remarks></remarks>
		public enum logType : int
		{
			Info,
			Config,
			Warning,
			ErrorCondition,
			Debug
		}

		private string m_logFileName = "";
		private int m_logFileRef = -1;
		private bool m_debug = false;

		private int m_maxFrames = 15;

		public LogClass()
		{
		}

		public LogClass(string logFileName, bool debug)
		{
			m_logFileName = logFileName;
			m_debug = debug;

		}

		/// <summary>
		/// Method that writes log entry
		/// </summary>
		/// <param name="lType">Log Type</param>
		/// <param name="message">Message string to write</param>
		/// <param name="logOnly">Log only or also display message box</param>
		/// <param name="buttons">For message box display, which buttons to use</param>
		/// <param name="title">For message box display, title of window</param>
		/// <returns>For message box display, which button was selected</returns>
		/// <remarks></remarks>
		public MsgBoxResult Log(int lType, string message, bool logOnly = true, MsgBoxStyle buttons = null, string title = "")
		{

			if (Open()) {
				string username = "no user";
				if ((GlobalShared.user != null)) {
					username = GlobalShared.user.getName();
				}
				FileSystem.PrintLine(m_logFileRef, Now.ToString + ": " + logTypes(lType) + ": " + username + " " + message);
				if (lType == logType.Debug | m_debug) {
					System.Diagnostics.StackFrame[] stackFrames = null;
					stackFrames = new System.Diagnostics.StackTrace().GetFrames;
					int maxCount = 0;
					foreach (System.Diagnostics.StackFrame sf in stackFrames) {
						FileSystem.Print(m_logFileRef, "   " + sf.ToString);
						maxCount += 1;
						if (maxCount > m_maxFrames)
							break; // TODO: might not be correct. Was : Exit For
					}
				}
				FileSystem.FileClose(m_logFileRef);
			}
			if (logOnly) {
				return null;
			} else {
				return Interaction.MsgBox(message, buttons, title);
			}

		}

		private bool Open()
		{

			bool result = false;
			try {
				m_logFileRef = FileSystem.FreeFile();
				FileSystem.FileOpen(m_logFileRef, m_logFileName, OpenMode.Append, OpenAccess.Default, OpenShare.Shared);
				result = true;
			} catch (IO.IOException ex) {
				result = false;
			} catch (System.Exception ex) {
				result = false;
			}

			return result;

		}
		// Properties
		/// <summary>
		/// Full path of logfile
		/// </summary>
		/// <value>Logfile path</value>
		/// <returns>Logfile path</returns>
		/// <remarks></remarks>
		public string logFileName {
			get { return m_logFileName; }
			set { m_logFileName = value; }
		}
		/// <summary>
		/// Internal log file reference
		/// </summary>
		/// <value></value>
		/// <returns>Log file reference</returns>
		/// <remarks></remarks>
		public int logFileRef {
			get { return m_logFileRef; }
		}
		/// <summary>
		/// Sets or resets debug mode
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool debug {
			get { return m_debug; }
			set { m_debug = value; }
		}
		/// <summary>
		/// Sets maximum number of stack frames to include in debu log entries
		/// </summary>
		/// <value>Maxium number of stack frames</value>
		/// <returns></returns>
		/// <remarks></remarks>
		public int maxFrames {
			get { return m_maxFrames; }
			set { m_maxFrames = value; }
		}
	}
}
