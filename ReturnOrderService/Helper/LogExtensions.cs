namespace ReturnOrderService.Helper
{
    public static class LogExtensions
    {
        public static void LogWithCorrelationId(this ILogger logger, LogLevel logLevel, Guid correlationId, Exception? exception, string? message, params object?[] args)
        {
            using (logger.BeginScope("{correlationId}", correlationId))
            {
                logger.Log(logLevel, exception, message, args);
            }
        }
        public static void LogWithCorrelationId(this ILogger logger, LogLevel logLevel, Guid correlationId, string? message, params object?[] args)
        {
            using (logger.BeginScope("{correlationId}", correlationId))
            {
                logger.Log(logLevel, message, args);
            }
        }
    }
}
