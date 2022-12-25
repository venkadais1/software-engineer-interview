using NLog;
using Zip.InstallmentsService.Interface;

namespace Zip.InstallmentsService.Logging
{
    /// <summary>
    ///     The definition of N logger 
    /// </summary>
    public class NLogger : INLogger
    {

        private static ILogger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        ///     Log in the form of debug
        /// </summary>
        /// <param name="message">message to log</param>
        public void LogDebug(string message) => logger.Debug(message);

        /// <summary>
        ///     Log in the form of error
        /// </summary>
        /// <param name="message">message to log</param>
        public void LogError(string message) => logger.Error(message);

        /// <summary>
        ///     Log in the form of information
        /// </summary>
        /// <param name="message">message to log</param>
        public void LogInfo(string message) => logger.Info(message);

        /// <summary>
        ///     Log in the form of warning
        /// </summary>
        /// <param name="message">message to log</param>
        public void LogWarning(string message) => logger.Warn(message);
    }
}
