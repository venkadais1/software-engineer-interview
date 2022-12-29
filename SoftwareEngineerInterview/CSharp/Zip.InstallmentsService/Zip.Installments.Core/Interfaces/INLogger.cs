namespace Zip.Installments.Core.Interface
{
    /// <summary>
    ///     The declaration of N logger 
    /// </summary>
    public interface INLogger
    {
        /// <summary>
        ///     Log in the form of information
        /// </summary>
        /// <param name="message">message to log</param>
        void LogInfo(string message);

        /// <summary>
        ///     Log in the form of warning
        /// </summary>
        /// <param name="message">message to log</param>
        void LogWarning(string message);

        /// <summary>
        ///     Log in the form of debug
        /// </summary>
        /// <param name="message">message to log</param>
        void LogDebug(string message);

        /// <summary>
        ///     Log in the form of error
        /// </summary>
        /// <param name="message">message to log</param>
        void LogError(string message);

        void LowTrace(string message);

        void LowShallow(string message); 
    }
}
