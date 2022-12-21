using Microsoft.Extensions.Options;
using NLog;
using Zip.InstallmentsService.Interface;

namespace Zip.InstallmentsService.Logging
{
    public class NLogger : INLogger
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(message);
        public void LogError(string message) => logger.Error(message);
        public void LogInfo(string message) => logger.Info(message);
        public void LogWarning(string message) => logger.Warn(message);
    }
}
