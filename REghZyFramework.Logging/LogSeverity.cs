namespace REghZyFramework.Logging
{
    // idk why i gave them bitwise values... eh
    /// <summary>
    /// Specifies how severe a message is that is to be logged
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Log new information, like "Config loaded"
        /// </summary>
        Information = 0,
        /// <summary>
        /// Something mild has happened that maybe wasn't expected
        /// </summary>
        Alert = 1,
        /// <summary>
        /// Something very bad has happened, like an integer overflow; 
        /// very unexpected
        /// </summary>
        Error = 2,
        /// <summary>
        /// Something bad has happened and wasn't really expected, 
        /// or a general exception has occoured that isn't too bad
        /// </summary>
        Warning = 4,
        /// <summary>
        /// Something crucially bad has happened, or an exception has been 
        /// thrown when it shouldn't have (so basically, a bug has occoured)
        /// (similar to an error but a bit worse)
        /// </summary>
        Severe = 8,
        /// <summary>
        /// Something has happened that is so bad it's either unrecoverable, or 
        /// the nature of the event requires the app to crash or forcefully stop,
        /// e.g. a crucial file has been deleted externally that is required
        /// </summary>
        Crash = 16,
    }

    public static class LogSeverityExtensions
    {
        public static string GetReadable(this LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Information:
                    return "Info";
                case LogSeverity.Alert:
                    return "Alert";
                case LogSeverity.Error:
                    return "Error";
                case LogSeverity.Warning:
                    return "WARNING";
                case LogSeverity.Severe:
                    return "SEVERE";
                case LogSeverity.Crash:
                    return "CRASHED";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Gets the readable severity and bracket-ilised it for use in a logger head
        /// </summary>
        /// <param name="severity"></param>
        /// <returns></returns>
        public static string GetFormatted(this LogSeverity severity)
        {
            return $"[{severity.GetReadable()}]";
        }

        public static bool CanPrintFormat(this LogSeverity severity)
        {
            // dont want to disable printing specific severity. 
            // for example, dont print [Info]
            // e.g.:
            // if (severity == LogSeverity.Information)
            // {  
            //     return false; 
            // }

            return true;
        }
    }
}
