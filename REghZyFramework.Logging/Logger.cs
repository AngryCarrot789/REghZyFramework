using System;

namespace REghZyFramework.Logging
{
    public class Logger
    {
        private Action<DateTime, string, string, Exception, LogSeverity> LogMessageCallback { get; set; }

        /// <summary>
        /// The name of the logger
        /// <para>
        ///     e.g, Application, FileIO, CrashHandler, etc
        /// </para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a logger instance where all logged messages are 'send' to the given callback function
        /// </summary>
        /// <param name="callback"></param>
        public Logger(Action<DateTime, string, string, Exception, LogSeverity> callback)
        {
            LogMessageCallback = callback;
        }

        /// <summary>
        /// Logs the information along with the logger name
        /// </summary>
        /// <param name="information"></param>
        public void Log(string information, LogSeverity severity = LogSeverity.Information)
        {
            LogMessageCallback?.Invoke(DateTime.Now, Name, information, null, severity);
        }

        /// <summary>
        /// Logs the information
        /// </summary>
        /// <param name="information"></param>
        public void Log(string head, string information, LogSeverity severity = LogSeverity.Information)
        {
            LogMessageCallback?.Invoke(DateTime.Now, head, information, null, severity);
        }

        /// <summary>
        /// Logs an exception to the console with detailed exception information
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="exception"></param>
        public void LogException(Exception exception, LogSeverity severity = LogSeverity.Warning)
        {
            LogMessageCallback?.Invoke(DateTime.Now, Name, "An exception has occoured", exception, severity);
        }

        /// <summary>
        /// Logs an exception to the console with detailed exception information
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="exception"></param>
        public void LogException(string information, Exception exception, LogSeverity severity = LogSeverity.Warning)
        {
            LogMessageCallback?.Invoke(DateTime.Now, Name, information, exception, severity);
        }

        /// <summary>
        /// Logs the information to the console
        /// </summary>
        /// <param name="information"></param>
        public void LogConsole(string information, LogSeverity severity = LogSeverity.Information)
        {
            string time = DateTime.Now.ToString("T");
            string severe = severity.CanPrintFormat() ? $" {severity.GetFormatted()} " : " ";
            Console.WriteLine($"{time}{severe}[{Name}] {information}");
        }

        /// <summary>
        /// Logs an exception to the console with detailed exception information
        /// </summary>
        /// <param name="head"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="exception"></param>
        public void LogExceptionConsole(Exception exception, bool printExceptionStacktrace = true, LogSeverity severity = LogSeverity.Warning)
        {
            string dateTime = DateTime.Now.ToString("T");
            string severe = severity.CanPrintFormat() ? $" {severity.GetFormatted()} " : " ";
            Console.WriteLine($"{dateTime}{severe}[{Name}] An exception has occoured");
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
