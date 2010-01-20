using System;
using Jamaica.NHibernate.Configuration;
using Jamaica.TableFootball.Core;
using Jamaica.TableFootball.Core.Authentication.Login;
using Jamaica.TableFootball.Core.Authentication.Logout;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using Jamaica.TableFootball.Core.Home;
using Jamaica.TableFootball.Core.PasswordReset;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Recording.VictoryRecording;
using Jamaica.TableFootball.Core.Reporting;
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

                RegisterTransientService<IScoringSelectListService, ScoringSelectListService>();
                RegisterTransientService<IUserSelectListService, UserSelectListService>();
                RegisterTransientService<IResultReportingService, ResultReportingService>();
                RegisterTransientService<IStatisticsReportingService, StatisticsReportingService>();

                ResourceSpace.Has.ResourcesOfType<HomeResource>()
                    .AtUri("/home").And.AtUri("/")
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

                ResourceSpace.Has.ResourcesOfType<LogoutResource>()
                    .AtUri("/logout")
                    .HandledBy<LogoutHandler>();

                ResourceSpace.Has.ResourcesOfType<VictoryRecordingResource>()
                    .AtUri("/record/victory")
                    .HandledBy<VictoryRecordingHandler>()
                    .RenderedByAspx("~/Views/Recording/VictoryRecording.aspx");

                ResourceSpace.Has.ResourcesOfType<PasswordResetResource>()
                    .AtUri("/aybabtu")
                    .HandledBy<PasswordResetHandler>()
                    .RenderedByAspx("~/Views/PasswordReset.aspx");
            }

            MigrateDatabaseToLatestVersion();
        }

        static void RegisterTransientService<TInterface, TImplementation>() where TImplementation : TInterface
        {
            ResourceSpace.Uses.CustomDependency<TInterface, TImplementation>(DependencyLifetime.Transient);
        }

        static void MigrateDatabaseToLatestVersion()
        {
            var log = DependencyManager.GetService<ILogger>();
            var migrator = new Core.Migrations.Runner(log);
            migrator.MigrateDatabaseToLatestVersion();
        }
    }

}