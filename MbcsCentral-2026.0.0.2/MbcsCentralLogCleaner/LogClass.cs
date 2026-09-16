using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

namespace MbcsCentralLogCleaner
{
    /// <summary>
    /// Class for creating and writing an application log file
    /// </summary>
    /// <remarks></remarks>
    public class LogClass
    {
        public static readonly LogClass instance = new LogClass();
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
        private string m_user;
        private string m_eventSource = "";
        private string m_eventLog = "";

        private string m_client = "";

        public LogClass()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logFileName">Full path to log file</param>
        /// <param name="debug">Sets global debug mode</param>
        /// <param name="user">User name</param>
        /// <remarks></remarks>

        public LogClass(string logFileName, bool debug, string user = null)
        {
            try
            {
                m_logFileName = logFileName;
                m_debug = debug;
                m_user = user;


            }
            catch (Exception )
            {
            }

        }

        public void Log(int lType, string message, bool LogEvent = false)
        {
            if (Open())
            {
                string username = "no user";
                if ((m_user != null))
                {
                    username = m_user;
                }
                Microsoft.VisualBasic.FileSystem.PrintLine(m_logFileRef, DateAndTime.Now.ToString() + ": " + logTypes[lType] + ": " + username + " " + message);
                if (lType == (int)logType.Debug | m_debug)
                {
                    System.Diagnostics.StackFrame[] stackFrames = null;
                    stackFrames = new System.Diagnostics.StackTrace().GetFrames();
                    int maxCount = 0;
                    foreach (System.Diagnostics.StackFrame sf in stackFrames)
                    {
                        Microsoft.VisualBasic.FileSystem.Print(m_logFileRef, "   " + sf.ToString());
                        maxCount += 1;
                        if (maxCount > m_maxFrames)
                            break; // TODO: might not be correct. Was : Exit For
                    }
                }
                Microsoft.VisualBasic.FileSystem.FileClose(m_logFileRef);
            }
            if (LogEvent & !string.IsNullOrEmpty(Strings.Trim(m_eventSource)) & !string.IsNullOrEmpty(Strings.Trim(m_eventLog)))
            {
                if (!string.IsNullOrEmpty(Strings.Trim(m_client)))
                {
                    message = "{{" + m_client + "}}" + Constants.vbCrLf + message;
                }
                EventLog eLog = new EventLog();
                eLog.Source = m_eventSource;
                eLog.Log = m_eventLog;
                switch (lType)
                {
                    case (int)logType.Config:
                    case (int)logType.Info:
                        eLog.WriteEntry(message, EventLogEntryType.Information, lType + 1);
                        break;
                    case (int)logType.Warning:
                        eLog.WriteEntry(message, EventLogEntryType.Warning, lType + 1);
                        break;
                    case (int)logType.ErrorCondition:
                        eLog.WriteEntry(message, EventLogEntryType.Error, lType + 1);
                        break;
                    case (int)logType.Debug:
                        eLog.WriteEntry("DEBUG: " + message, EventLogEntryType.Information, lType + 1);

                        break;
                }
            }

        }

        private bool Open()
        {

            bool result = false;
            try
            {
                m_logFileRef = Microsoft.VisualBasic.FileSystem.FreeFile();
                Microsoft.VisualBasic.FileSystem.FileOpen(m_logFileRef, m_logFileName, OpenMode.Append, OpenAccess.Default, OpenShare.Shared);
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
            get { return m_logFileName; }
            set { m_logFileName = value; }
        }
        /// <summary>
        /// Internal log file reference
        /// </summary>
        /// <value></value>
        /// <returns>Log file reference</returns>
        /// <remarks></remarks>
        public int logFileRef
        {
            get { return m_logFileRef; }
        }
        /// <summary>
        /// User Name
        /// </summary>
        /// <value></value>
        /// <returns>User Name</returns>
        /// <remarks></remarks>
        public string Username
        {
            get { return m_user; }
            set { m_user = value; }
        }
        /// <summary>
        /// Sets or resets debug mode
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool debug
        {
            get { return m_debug; }
            set { m_debug = value; }
        }
        /// <summary>
        /// Sets maximum number of stack frames to include in debu log entries
        /// </summary>
        /// <value>Maxium number of stack frames</value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int maxFrames
        {
            get { return m_maxFrames; }
            set { m_maxFrames = value; }
        }

        public string EventSource
        {
            get { return m_eventSource; }
            set { m_eventSource = value; }
        }

        public string EventLog
        {
            get { return m_eventLog; }
            set { m_eventLog = value; }
        }

        public string Client
        {
            get { return m_client; }
            set { m_client = value; }
        }

    }
}