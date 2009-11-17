using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jamaica.Test;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Jamaica.NHibernate.Specifications
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
            Inject(session);
        }

        protected override void TidyUp()
        {
            session.Dispose();
        }

        private static void InitializeNHibernate()
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ShowSql().InMemory)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
                
            configuration = fluentConfiguration.BuildConfiguration();
            sessionFactory = configuration.BuildSessionFactory();
        }
    }
}