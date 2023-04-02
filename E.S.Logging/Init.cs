using E.S.Data.Query.Context.DI;
using E.S.Logging.Interfaces;
using E.S.Logging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E.S.Logging;

public static class Init
{
    public static void AddLogger(this IServiceCollection services)
    {
        services.AddDataQueryContext();
        services.AddScoped<ILoggerService, LoggerService>();
    }
}