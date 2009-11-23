using System;
using Jamaica.NHibernate.Configuration;
using NHibernate;
using OpenRasta.Configuration;
using Jamaica.TableFootball.Handlers;
using Jamaica.TableFootball.Resources;
using Jamaica.Configuration;
using OpenRasta.DI;

namespace Jamaica.TableFootball
{
    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.Resolver
                    .AddDependencyInstance<ISessionFactory>(
                        Core.NHibernate.CreateSessionFactory(), DependencyLifetime.Singleton);

                ResourceSpace.Uses.CookieAuthentication();
                ResourceSpace.Uses.NHibernatePersistence();

                ResourceSpace.Has.ResourcesOfType<HomeResource>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByAspx("~/Views/HomeView.aspx");

            }
        }
    }

}