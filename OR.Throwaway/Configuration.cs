#region License
/* Authors:
 *      Sebastien Lambla (seb@serialseb.com)
 * Copyright:
 *      (C) 2007-2009 Caffeine IT & naughtyProd Ltd (http://www.caffeine-it.com)
 * License:
 *      This file is distributed under the terms of the MIT License found at the end of this file.
 */
#endregion

using System;
using System.Configuration;
using Jamaica.Handlers;
using Jamaica.NHibernate;
using Jamaica.Security;
using Migrator.Framework.Loggers;
using NHibernate;
using OpenRasta.Configuration;
using OpenRasta.Diagnostics;
using OpenRasta.Web.UriDecorators;
using OR.Throwaway.DataAccess;
using OpenRasta.DI;

namespace OR.Throwaway
{
    public class Configuration : IConfigurationSource
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
        }

        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                MigrateDatabaseToLatestVersion(ResourceSpace.Uses.Resolver.Resolve<ILogger>());

                ResourceSpace.Uses.UriDecorator<HttpMethodOverrideUriDecorator>();
                ResourceSpace.Uses.Resolver
                    .AddDependencyInstance(typeof(ISessionFactory), NHibernateConfiguration.CreateSessionFactory(),
                    DependencyLifetime.Singleton);

                ResourceSpace.Uses.JamaicanHandlerRegistration();
                ResourceSpace.Uses.JamaicanNHibernateManagement();
                ResourceSpace.Uses.JamaicanCookieAuthentication();
             }
        }

        private static void MigrateDatabaseToLatestVersion(ILogger log)
        {
            var runner = new Migrator.Migrator("SQLite", ConnectionString(),
                                               typeof (Migrations.Tags).Assembly);

            runner.Logger = new Logger(true, new[] { new OpenRastaLogWriter(log) });

            using(log.Operation(runner, "Database migration"))
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

#region Full license
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion