using System;
using Microsoft.Extensions.Logging;

namespace CMSCore.Shared.Abstractions.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogError(this ILogger logger, Exception ex, string message = null, params object[] args)
        {
            logger.LogError(default(EventId), ex, message, args);
        }
    }
}