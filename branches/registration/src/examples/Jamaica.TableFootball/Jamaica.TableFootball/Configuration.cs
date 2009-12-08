using System;
using System.Linq;
using System.Reflection;
using Jamaica.NHibernate.Configuration;
using Jamaica.TableFootball.Core;
using Jamaica.TableFootball.Core.Authentication.Login;
using Jamaica.TableFootball.Core.Authentication.Logout;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using Jamaica.TableFootball.Core.Home;
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
                RegisterTransientService<IResultReportingService, ResultReportingService>();
                RegisterTransientService<IStatisticsReportingService, StatisticsReportingService>();

                AutoRegisterAssemblyOf<HomeHandler>();

                //ResourceSpace.Has.ResourcesOfType<HomeResource>()
                //    .AtUri("/home").And.AtUri("/")
                //    .HandledBy<HomeHandler>()
                //    .RenderedByAspx("~/Views/Home.aspx");

                //ResourceSpace.Has.ResourcesOfType<UserRegistrationResource>()
                //    .AtUri("/registration")
                //    .HandledBy<UserRegistrationHandler>()
                //    .RenderedByAspx("~/Views/Authentication/UserRegistration.aspx");

                //ResourceSpace.Has.ResourcesOfType<LoginResource>()
                //    .AtUri("/login")
                //    .HandledBy<LoginHandler>()
                //    .RenderedByAspx("~/Views/Authentication/Login.aspx");

                //ResourceSpace.Has.ResourcesOfType<LogoutResource>()
                //    .AtUri("/logout")
                //    .HandledBy<LogoutHandler>();

                //ResourceSpace.Has.ResourcesOfType<VictoryRecordingResource>()
                //    .AtUri("/record/victory")
                //    .HandledBy<VictoryRecordingHandler>()
                //    .RenderedByAspx("~/Views/Recording/VictoryRecording.aspx");
            }

            MigrateDatabaseToLatestVersion();
        }

        static void AutoRegisterAssemblyOf<T>()
        {
            var assembly = typeof (T).Assembly;

            var conventionRegistrations = assembly.GetTypes()
                .Where(type => ConventionRegistration.GetAttribute(type) != null)
                .Select(type => new ConventionRegistration(type, assembly));

            foreach (var resource in conventionRegistrations)
            {
                resource.Register();
            }
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

        class ConventionRegistration
        {
            readonly Type type;
            readonly Assembly assembly;
            readonly UriRegistrationAttribute attribute;

            public ConventionRegistration(Type type, Assembly assembly)
            {
                this.type = type;
                this.assembly = assembly;
                attribute = GetAttribute(type);
            }

            public static UriRegistrationAttribute GetAttribute(Type type)
            {
                return type.GetCustomAttributes(typeof(UriRegistrationAttribute), false).FirstOrDefault() as UriRegistrationAttribute;
            }

            public void Register()
            {
                var registration = ResourceSpace.Has.ResourcesOfType(type);
                var handlerType = assembly.GetType(type.FullName.Replace("Resource", "Handler"));

                var uriDefinition = registration.AtUri(attribute.Uri);

                foreach (var uri in attribute.AlternateUris)
                {
                    uriDefinition.And.AtUri(uri);
                }

                var handlerDefinition = uriDefinition.HandledBy(handlerType);

                handlerDefinition.RenderedByAspx(ViewPath());
            }

            string ViewPath()
            {
                var uriPath = type.Namespace
                    .Replace(assembly.GetName().Name, "")
                    .Replace(".", "/");

                return string.Format("~/Views{0}.aspx", uriPath);
            }
        }
    }

}