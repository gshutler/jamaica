using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jamaica.NHibernate.Mapping;
using NHibernate;

namespace Jamaica.TableFootball.Core
{
    public static class NHibernate
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                    .AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}