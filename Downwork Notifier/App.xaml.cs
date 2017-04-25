using Downwork_Notifier.DAL;
using System.Diagnostics;
using System.Windows;
using static Downwork_Notifier.DAL.Extensions.DbContextEx;
using static Utilities.Extensions.ExceptionEx;

namespace Downwork_Notifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static DownworkContext _dbContext;
        public static DownworkContext DbContext => _dbContext;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
            _dbContext = new DownworkContext();
#if DEBUG
            _dbContext.LogToConsole();
#endif
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                var logMessage = $@"Exception occurred:
------------------------------------------
{e.Exception.GetFullMessage()}
------------------------------------------

StackTrace:
{e.Exception.StackTrace}";
                eventLog.WriteEntry(logMessage, EventLogEntryType.Error);
            }

            e.Handled = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_dbContext != null)
            {
                _dbContext.SaveChanges();
                _dbContext.Dispose();
                _dbContext = null;
            }

            base.OnExit(e);
        }
    }
}
