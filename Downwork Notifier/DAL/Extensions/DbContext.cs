using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Downwork_Notifier.DAL.Extensions
{
    public static class DbContextEx
    {
        public static void LogToConsole(this DbContext context)
        {
            var loggerFactory = context.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new DbLoggerProvider());
            //loggerFactory.AddConsole(LogLevel.Debug);
        }
    }
}
