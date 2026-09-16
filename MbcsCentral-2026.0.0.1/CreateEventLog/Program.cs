using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CreateEventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string ApplicationName = args[0];
            string LogName = args[1];
            if (args.Length >= 2 && !EventLog.SourceExists(ApplicationName))
            {
                try
                {
                    System.Diagnostics.EventLog.CreateEventSource(ApplicationName, LogName);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("CreateEentLog: " + ex.Message);
                }
            }

        }
    }
}
