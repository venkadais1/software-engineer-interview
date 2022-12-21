using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
