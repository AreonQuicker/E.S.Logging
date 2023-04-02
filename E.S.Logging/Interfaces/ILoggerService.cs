using System.Collections.Generic;
using System.Threading.Tasks;
using E.S.Logging.Models;

namespace E.S.Logging.Interfaces;

public interface ILoggerService
{
    Task<IEnumerable<LogSearchResult>> SearchLogsAsync(LogSearchRequest request);
}