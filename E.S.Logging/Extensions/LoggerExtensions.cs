using System;
using System.Collections.Generic;
using E.S.Logging.Enums;
using Microsoft.Extensions.Logging;

namespace E.S.Logging.Extensions
{
    public static class LoggerExtensions
    {
        private static string format =
            "{System} - Key:{Key} Status:{Status} Operation:{Operation} User:{LoggedInUser} UserMessage:{UserMessage}";
        
        public static void LogOperationWithExtraFormat(this ILogger logger,
            LogLevel logLevel,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage,
            Exception exception,
            string extraFormat = null,
            params string[] extraParam)
        {
            var newFormat = format;
            
            if (!string.IsNullOrEmpty(extraFormat))
            {
                newFormat += " " + extraFormat;
            }

            var param = new List<object>();
            param.Add(loggerSystem);
            param.Add(key);
            param.Add(loggerStatus.ToString());
            param.Add(loggerOperation);
            param.Add(loggedInUserName);
            param.Add(userMessage);
            
            if (extraParam != null)
            {
                param.AddRange(extraParam);
            }

            logger.Log(logLevel,
                exception,
                newFormat,
                param.ToArray());
        }
        
        public static void LogErrorOperationWithExtraFormat(this ILogger logger,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage,
            Exception exception,
            string extraFormat = null,
            params string[] extraParam) =>
            logger.LogOperationWithExtraFormat(LogLevel.Error,
                loggerStatus,
                loggerSystem,
                loggerOperation,
                key,
                loggedInUserName,
                userMessage,
                exception, 
                extraFormat,
                extraParam);
        
        public static void LogOperation(this ILogger logger,
            LogLevel logLevel,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage,
            Exception exception = null)
        {
            var param = new List<object>();
            param.Add(loggerSystem);
            param.Add(key);
            param.Add(loggerStatus.ToString());
            param.Add(loggerOperation);
            param.Add(loggedInUserName);
            param.Add(userMessage);
            
            logger.Log(logLevel,
                exception,
                format,
                param.ToArray());
        }

        public static void LogErrorOperation(this ILogger logger,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage,
            Exception exception) =>
            logger.LogOperation(LogLevel.Error,
                loggerStatus,
                loggerSystem,
                loggerOperation,
                key,
                loggedInUserName,
                userMessage,
                exception);

        public static void LogInformationOperation(this ILogger logger,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage) =>
            logger.LogOperation(LogLevel.Information,
                loggerStatus,
                loggerSystem,
                loggerOperation,
                key,
                loggedInUserName,
                userMessage);

        public static void LogWarningOperation(this ILogger logger,
            LoggerStatusEnum loggerStatus,
            string loggerSystem,
            string loggerOperation,
            string key,
            string loggedInUserName,
            string userMessage) =>
            logger.LogOperation(LogLevel.Warning,
                loggerStatus,
                loggerSystem,
                loggerOperation,
                key,
                loggedInUserName,
                userMessage);
    }
}