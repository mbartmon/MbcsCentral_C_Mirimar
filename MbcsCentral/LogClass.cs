using Microsoft.VisualBasic;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;
using MbcsCentral;


/// <summary>
/// Class for creating and writing an application log file
/// </summary>
/// <remarks></remarks>
public sealed class LogClass
{

    public static readonly LogClass instance = new LogClass();

	public static string[] logTypes = new string[] {"INFO", "CONFIG", "WARNING", "ERROR", "DEBUG"};
	/// <summary>
	/// Enumeration that provides log type names
	/// </summary>
	/// <remarks></remarks>
	public enum logType
	{
		Info ,
		Config,
		Warning,
		ErrorCondition,
		Debug
	}
	
	private string m_logFileName = "";
	private int m_logFileRef = -1;
	private bool m_debug = false;
	private int m_maxFrames = 15;
	
	private LogClass()
	{
		
	}

    /*
	public LogClass(string logFileName, bool debug)
	{
		
		m_logFileName = logFileName;
		m_debug = debug;
		
	}
	*/

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
	public System.Windows.Forms.DialogResult Log(int lType, string message, bool logOnly = true, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
	{
		
		if (Open())
		{
			string username = "no user";
			if (MbcsCentral.GlobalShared.user != null)
			{
                username = (string)(MbcsCentral.GlobalShared.user.getName);
			}
			Microsoft.VisualBasic.FileSystem.PrintLine(m_logFileRef, System.DateTime.Now.ToString() + ": " + logTypes[lType] + ": " + username + " " + message);
			if (lType == (int)logType.Debug || m_debug)
			{
				System.Diagnostics.StackFrame[] stackFrames = null;
				stackFrames = new System.Diagnostics.StackTrace().GetFrames();
				int maxCount = 0;
				foreach (System.Diagnostics.StackFrame sf in stackFrames)
				{
                    Microsoft.VisualBasic.FileSystem.Print(m_logFileRef, "   " + sf.ToString());
					maxCount++;
					if (maxCount > m_maxFrames)
					{
						break;
					}
				}
			}
			Microsoft.VisualBasic.FileSystem.FileClose(m_logFileRef);
		}
		if (logOnly)
		{
			return 0;
		}
		else
		{
			return MessageBox.Show(message,title, buttons);
		}
		
	}
	
	private bool Open()
	{
		
		bool result = false;
		try
		{
            m_logFileRef = Microsoft.VisualBasic.FileSystem.FreeFile();
            Microsoft.VisualBasic.FileSystem.FileOpen(m_logFileRef, m_logFileName, OpenMode.Append, OpenAccess.Default, OpenShare.Shared, -1);
			result = true;
		}
		catch
		{
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
	public string logFileName
	{
		get
		{
			return m_logFileName;
		}
		set
		{
			m_logFileName = value.ToString();
		}
	}
	/// <summary>
	/// Internal log file reference
	/// </summary>
	/// <value></value>
	/// <returns>Log file reference</returns>
	/// <remarks></remarks>
	public int logFileRef
	{
		get
		{
			return m_logFileRef;
		}
	}
	/// <summary>
	/// Sets or resets debug mode
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public bool debug
	{
		get
		{
			return m_debug;
		}
		set
		{
			m_debug = System.Convert.ToBoolean(value);
		}
	}
	/// <summary>
	/// Sets maximum number of stack frames to include in debu log entries
	/// </summary>
	/// <value>Maxium number of stack frames</value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int maxFrames
	{
		get
		{
			return m_maxFrames;
		}
		set
		{
			m_maxFrames = System.Convert.ToInt32(value);
		}
	}
}