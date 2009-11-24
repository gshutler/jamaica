using System.Configuration;
using System.Reflection;
using Migrator.Framework.Loggers;
using OpenRasta.Diagnostics;

namespace Jamaica.TableFootball.Core.Migrations
{
    public class Runner
    {
        readonly ILogger log;

        public Runner(ILogger log)
        {
            this.log = log;
        }

        public void MigrateDatabaseToLatestVersion()
        {
            var runner = new Migrator.Migrator("SQLite", ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString, Assembly.GetExecutingAssembly())
                             {
                                 Logger = new Logger(true, new[] {new OpenRastaLogWriter(log)})
                             };

            using (log.Operation(runner, "Database migration"))
            {
                runner.MigrateToLastVersion();
            }
        }

        private class OpenRastaLogWriter : ILogWriter
        {
            private readonly ILogger log;

            public OpenRastaLogWriter(ILogger log)
            {
                this.log = log;
            }

            public void Write(string message, params object[] args)
            {
                log.WriteInfo(message, args);
            }

            public void WriteLine(string message, params object[] args)
            {
                log.WriteInfo(message, args);
            }
        }
    }
}