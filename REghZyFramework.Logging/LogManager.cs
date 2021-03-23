using System;

namespace REghZyFramework.Logging
{
    /// <summary>
    /// Used for "managing" loggers, e.g. creating them. Can also be used for logging raw information too
    /// </summary>
    public static class LogManager
    {
        private static Action<DateTime, string, string, Exception, LogSeverity> LogCallback;

        public static void Initialise(Action<DateTime, string, string, Exception, LogSeverity> log)
        {
            LogManager.LogCallback = log;
        }

        public static Logger CreateLogger(string name = "Unnamed Logger")
        {
            return new Logger(LogCallback)
            {
                Name = name
            };
        }

        public static void Log(string generalDescription)
        {
            Log("General", generalDescription, DateTime.Now, null);
        }

        /// <summary>
        /// DateTime 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        public static void Log(string head, string description)
        {
            Log(head, description, DateTime.Now, null);
        }

        public static void Log(string head, string description, DateTime date)
        {
            Log(head, description, date, null);
        }

        /// <summary>
        /// Logs the information to the logger window in which it will look like:
        /// <code>
        ///     DateTime | [<paramref name="head"/>] <paramref name="description"/>
        /// </code>
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="exception"></param>
        public static void Log(string head, string description, DateTime date, Exception exception, LogSeverity severity = LogSeverity.Information)
        {
            LogCallback(date, head, description, exception, severity);
        }

        /// <summary>
        /// Logs the information to the console
        /// </summary>
        /// <param name="information"></param>
        public static void LogConsole(string head, string information, LogSeverity severity = LogSeverity.Information)
        {
            string time = DateTime.Now.ToString("T");
            string severe = severity.CanPrintFormat() ? $" {severity.GetFormatted()} " : " ";
            Console.WriteLine($"{time}{severe}[{head}] {information}");
        }

        /// <summary>
        /// Logs an exception to the console with detailed exception information
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="exception"></param>
        public static void LogExceptionConsole(string head, Exception exception, bool printExceptionStacktrace = true, LogSeverity severity = LogSeverity.Warning)
        {
            string dateTime = DateTime.Now.ToString("T");
            string severe = severity.CanPrintFormat() ? $" {severity.GetFormatted()} " : " ";
            Console.WriteLine($"{dateTime}{severe}[{head}] An exception has occoured");
            if (exception != null)
            {
                Console.WriteLine($"Exception: {exception.Message}");
                if (printExceptionStacktrace)
                {
                    Console.WriteLine($"Stacktrace:\n{exception.StackTrace}");
                }
            }
        }
    }
}