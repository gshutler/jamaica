using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jamaica.NHibernate.Mapping;
using Jamaica.Test;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Jamaica.TableFootball.Specifications
{
    public abstract class IntegrationSpecification : Specification
    {
        static global::NHibernate.Cfg.Configuration configuration;
        static ISessionFactory sessionFactory;
        protected ISession session;

        protected IntegrationSpecification()
        {
            if (configuration == null)
            {
                InitializeNHibernate();
            }
        }

        protected override void CommonContext()
        {
            session = sessionFactory.OpenSession();
            new SchemaExport(configuration).Execute(true, true, false, session.Connection, null);
            InjectDependency(session);
        }

        protected override void TidyUp()
        {
            session.Dispose();
        }

        private static void InitializeNHibernate()
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ShowSql().InMemory)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());

            configuration = fluentConfiguration.BuildConfiguration();
            sessionFactory = configuration.BuildSessionFactory();
        }
    }
}