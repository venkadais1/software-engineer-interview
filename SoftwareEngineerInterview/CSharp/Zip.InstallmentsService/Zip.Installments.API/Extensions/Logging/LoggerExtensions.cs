using NLog;
using NLog.Extensions.Logging;
using Zip.InstallmentsService.Interface;
using Zip.InstallmentsService.Logging;

namespace Zip.Installments.API.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static void AddAppLogging(this IServiceCollection services, IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            services.AddLogging((logbuilder) =>
            {
                logbuilder.AddNLog(LogManager.Configuration);
            });
            services.AddSingleton<INLogger, NLogger>();
        }
    }
}
