using NLog;
using NLog.Extensions.Logging;
using Zip.InstallmentsService.Interface;
using Zip.InstallmentsService.Logging;

namespace Zip.Installments.API.Extensions.Logging
{
    /// <summary>
    ///     Extensions for Global File Logging using N-Logger
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        ///     Add global application logger
        /// </summary>
        /// <param name="services">Add this extension with <see cref="IServiceCollection"/></param>
        /// <param name="configuration">An instance of <see cref="IConfiguration"/></param>
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
