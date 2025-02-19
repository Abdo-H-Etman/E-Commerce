using System;

namespace Ecommerce.Application.Interfaces;

public interface ILoggerManager
{
    void LogInfo(string message);
    void LogError(string message);
    void LogDebug(string message);
    void LogWarn(string message);
}
