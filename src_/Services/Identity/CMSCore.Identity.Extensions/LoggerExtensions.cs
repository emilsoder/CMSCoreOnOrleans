using Microsoft.Extensions.Logging;
using System;

namespace CMSCore.Identity.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogError(this ILogger logger, Exception ex, string message = null, params object[] args)
        {
            logger.LogError(default(EventId), ex, message, args);
        }
    }
}