using System;
using System.Collections.Generic;
using E.S.Logging.Constants;
using E.S.Logging.Enums;
using Microsoft.Extensions.Logging;

namespace E.S.Logging.Extensions;

public static class LoggerExtensions
{
    private static readonly string format =
        "{System} - Key:{Key} Status:{Status} Operation:{Operation} User:{LoggedInUser} UserMessage:{UserMessage} SubSystem:{SubSystem}";

    public static void Track(this ILogger logger,
        string subSystem,
        string operation,
        string key,
        string loggedInUser,
        string userMessage)
    {
        logger.LogOperation(LogLevel.Information,
            LoggerStatusEnum.InProgress,
            LoggerConstant.TrackingSystem,
            operation,
            key,
            loggedInUser,
            userMessage,
            subSystem);
    }

    public static void TrackError(this ILogger logger,
        string subSystem,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        Exception ex)
    {
        logger.LogOperation(LogLevel.Error,
            LoggerStatusEnum.InProgress,
            LoggerConstant.TrackingSystem,
            operation,
            key,
            loggedInUser,
            userMessage,
            subSystem, ex);
    }

    public static void LogOperationWithExtraFormat(this ILogger logger,
        LogLevel logLevel,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        Exception exception,
        string extraFormat = null,
        params string[] extraParam)
    {
        var newFormat = format;

        if (!string.IsNullOrEmpty(extraFormat)) newFormat += " " + extraFormat;

        var param = new List<object>();
        param.Add(system);
        param.Add(key);
        param.Add(status.ToString());
        param.Add(operation);
        param.Add(loggedInUser);
        param.Add(userMessage);
        param.Add(null);

        if (extraParam != null) param.AddRange(extraParam);

        logger.Log(logLevel,
            exception,
            newFormat,
            param.ToArray());
    }

    public static void LogErrorOperationWithExtraFormat(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        Exception exception,
        string extraFormat = null,
        params string[] extraParam)
    {
        logger.LogOperationWithExtraFormat(LogLevel.Error,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            exception,
            extraFormat,
            extraParam);
    }

    public static void LogOperation(this ILogger logger,
        LogLevel logLevel,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        string subSystem,
        Exception exception = null)
    {
        var param = new List<object>();
        param.Add(system);
        param.Add(key);
        param.Add(status.ToString());
        param.Add(operation);
        param.Add(loggedInUser);
        param.Add(userMessage);
        param.Add(subSystem);

        logger.Log(logLevel,
            exception,
            format,
            param.ToArray());
    }

    public static void LogErrorOperation(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        Exception exception)
    {
        logger.LogOperation(LogLevel.Error,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            null,
            exception);
    }

    public static void LogInformationOperation(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage)
    {
        logger.LogOperation(LogLevel.Information,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            null);
    }

    public static void LogWarningOperation(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string operation,
        string key,
        string loggedInUser,
        string userMessage)
    {
        logger.LogOperation(LogLevel.Warning,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            null);
    }

    public static void Information(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string subSystem,
        string operation,
        string key,
        string loggedInUser,
        string userMessage)
    {
        logger.LogOperation(LogLevel.Information,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            subSystem);
    }

    public static void Error(this ILogger logger,
        LoggerStatusEnum status,
        string system,
        string subSystem,
        string operation,
        string key,
        string loggedInUser,
        string userMessage,
        Exception exception)
    {
        logger.LogOperation(LogLevel.Error,
            status,
            system,
            operation,
            key,
            loggedInUser,
            userMessage,
            subSystem,
            exception);
    }
}