using NLog;
using Zip.Installments.Logs.Base;

namespace Zip.Installments.Logs.Logging
{
    /// <summary>
    ///     The definition of N logger 
    /// </summary>
    public class NLogger : LoggerBase
    {

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        ///     Log in the form of debug
        /// </summary>
        /// <param name="message">message to log</param>
        public override void LogDebug(string message) => logger.Debug(message);

        /// <summary>
        ///     Log in the form of error
        /// </summary>
        /// <param name="message">message to log</param>
        public override void LogError(string message) => logger.Error(message);

        /// <summary>
        ///     Log in the form of information
        /// </summary>
        /// <param name="message">message to log</param>
        public override void LogInfo(string message) => logger.Info(message);

        /// <summary>
        ///     Log in the form of warning
        /// </summary>
        /// <param name="message">message to log</param>
        public override void LogWarning(string message) => logger.Warn(message);

        public override void LowShallow(string message)
        {
            throw new NotImplementedException();
        }

        public override void LowTrace(string message)
        {
            throw new NotImplementedException();
        }
    }
}
