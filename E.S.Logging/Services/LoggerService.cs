using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E.S.Data.Query.Context.Extensions;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.DataAccess.Interfaces;
using E.S.Logging.DomainModels;
using E.S.Logging.Interfaces;
using E.S.Logging.Models;

namespace E.S.Logging.Services;

public class LoggerService : ILoggerService
{
    private readonly IDataAccessQuery _dataAccessQuery;
    private readonly IRepositoryService<LogDomainModel> _logRepositoryService;

    public LoggerService(IRepositoryService<LogDomainModel> logRepositoryService, IDataAccessQuery dataAccessQuery)
    {
        _logRepositoryService = logRepositoryService;
        _dataAccessQuery = dataAccessQuery;
    }

    public async Task<IEnumerable<LogSearchResult>> SearchLogsAsync(LogSearchRequest request)
    {
        var logBuilder = _dataAccessQuery.SelectQuery<LogDomainModel>();

        logBuilder = logBuilder.WithSelectAllFields(false);

        logBuilder = logBuilder.Where(nameof(LogDomainModel.TimeStamp), ">=", request.Start.ToString("yyyy-MM-dd"),
            true);
        logBuilder = logBuilder.Where(nameof(LogDomainModel.TimeStamp), "<=", request.End.ToString("yyyy-MM-dd"), true);

        if (!string.IsNullOrEmpty(request.Level))
            logBuilder = logBuilder.Where(nameof(LogDomainModel.Level), request.Level);

        if (!string.IsNullOrEmpty(request.System))
            logBuilder = logBuilder.Where(nameof(LogDomainModel.System), request.System);

        if (!string.IsNullOrEmpty(request.SubSystem))
            logBuilder = logBuilder.Where(nameof(LogDomainModel.SubSystem), request.SubSystem);

        if (!string.IsNullOrEmpty(request.Operation))
            logBuilder = logBuilder.Where(nameof(LogDomainModel.Operation), request.Operation);

        logBuilder = logBuilder.OrderDesc(nameof(LogDomainModel.TimeStamp));

        var logs = (await logBuilder.ListAsync<LogDomainModel>()).ToList();

        var filteredLogs = logs
            .Where(w => string.IsNullOrEmpty(request.LoggedInUser) ||
                        (w.LoggedInUser?.Contains(request.LoggedInUser, StringComparison.InvariantCultureIgnoreCase) ?? false))
            .Where(w => string.IsNullOrEmpty(request.Key) ||
                        (w.Key?.Contains(request.Key, StringComparison.InvariantCultureIgnoreCase) ?? false))
            .Where(w => string.IsNullOrEmpty(request.Status) ||
                        (w.Status?.Contains(request.Status, StringComparison.InvariantCultureIgnoreCase) ?? false))
            .Where(w => w.RequestPath != "/hub" && w.RequestPath != "/hub/negotiate")
            .Select(s => new LogSearchResult
            {
                Date = s.TimeStamp,
                Exception = s.Exception,
                Key = s.Key,
                Level = s.Level,
                Message = s.UserMessage ?? s.Message,
                Operation = s.Operation,
                Status = s.Status,
                System = s.System,
                SubSystem = s.SubSystem,
                RequestPath = s.RequestPath,
                LoggedInUser = s.LoggedInUser
            }).ToList();

        return filteredLogs;
    }
}