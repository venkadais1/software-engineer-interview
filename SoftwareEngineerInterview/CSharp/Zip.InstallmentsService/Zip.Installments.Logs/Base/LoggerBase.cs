using Zip.Installments.Core.Interface;

namespace Zip.Installments.Logs.Base
{
    public abstract class LoggerBase : INLogger
    {
        public abstract void LogDebug(string message);

        public abstract void LogError(string message);

        public abstract void LogInfo(string message);

        public abstract void LogWarning(string message);

        public abstract void LowTrace(string message);

        public abstract void LowShallow(string message);
    }
}
