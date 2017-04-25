using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Downwork_Notifier.DAL
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private ILogger _logger = new DbLogger();
        public ILogger CreateLogger(string categoryName) => _logger;

        public void Dispose()
        { }

        private class DbLogger : ILogger
        {
            public DbLogger() { }
            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //Console.WriteLine(formatter(state, exception));
                Debug.WriteLine(formatter(state, exception));
            }

            public IDisposable BeginScope<TState>(TState state) => null;
        }
    }
}
