namespace EppendorfAuthApi.Core;

public interface ILoggingService
{
    void LogInfo(string message);

    void LogInfo(string message, params object[] propertyValues);
}