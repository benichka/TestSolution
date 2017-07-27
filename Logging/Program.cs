using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// A simple class that shows how to log to Windows event log
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string eventLog = "Application";
            string eventSource = "Logging";
            string eventMessage = "Hello from the Logging application";
            // Create the event source if it does not already exist.
            if (!EventLog.SourceExists(eventSource))
                EventLog.CreateEventSource(eventSource, eventLog);
            // Log the message.
            EventLog.WriteEntry(eventSource, eventMessage);
        }
    }
}
