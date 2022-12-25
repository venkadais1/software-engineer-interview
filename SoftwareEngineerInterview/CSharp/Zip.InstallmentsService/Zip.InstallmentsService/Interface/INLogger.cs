namespace Zip.InstallmentsService.Interface
{
    public interface INLogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
