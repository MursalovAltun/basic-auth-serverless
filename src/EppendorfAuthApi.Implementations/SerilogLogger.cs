using EppendorfAuthApi.Core;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

namespace EppendorfAuthApi.Implementations;

public class SerilogLogger : ILoggingService
{
    private readonly Logger _log;

    public SerilogLogger(string traceId)
    {
        _log = new LoggerConfiguration()
            .WriteTo.Console(new RenderedCompactJsonFormatter())
            .CreateLogger();

        _log.ForContext("TraceId", traceId);
    }

    public void LogInfo(string message)
    {
        _log.Information(message);
    }

    public void LogInfo(string message, params object[] propertyValues)
    {
        _log.Information(message, propertyValues);
    }
}