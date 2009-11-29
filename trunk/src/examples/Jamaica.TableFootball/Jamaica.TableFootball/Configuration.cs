using System;
using Jamaica.NHibernate.Configuration;
using Jamaica.TableFootball.Core.Authentication.Login;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using OpenRasta.Configuration;
using Jamaica.Configuration;
using OpenRasta.DI;
using OpenRasta.Diagnostics;

namespace Jamaica.TableFootball
{
    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.Resolver
                    .AddDependencyInstance<ISessionFactory>(Core.NHibernate.CreateSessionFactory(), DependencyLifetime.Singleton);

                ResourceSpace.Uses.CookieAuthentication();
                ResourceSpace.Uses.NHibernatePersistence();

                ResourceSpace.Has.ResourcesOfType<HomeResource>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByAspx("~/Views/HomeView.aspx");

                ResourceSpace.Has.ResourcesOfType<UserRegistrationResource>()
                    .AtUri("/registration")
                    .HandledBy<UserRegistrationHandler>()
                    .RenderedByAspx("~/Views/Authentication/UserRegistration.aspx");

                ResourceSpace.Has.ResourcesOfType<LoginResource>()
                    .AtUri("/login")
                    .HandledBy<LoginHandler>()
                    .RenderedByAspx("~/Views/Authentication/Login.aspx");
            }

            MigrateDatabaseToLatestVersion();
        }

        static void MigrateDatabaseToLatestVersion()
        {
            var log = DependencyManager.GetService<ILogger>();
            var migrator = new Core.Migrations.Runner(log);
            migrator.MigrateDatabaseToLatestVersion();
        }
    }

}