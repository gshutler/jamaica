using System;
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jamaica.Security;
using NHibernate;
using OR.Throwaway.DataAccess.Mapping;
using OR.Throwaway.Domain;

namespace OR.Throwaway.DataAccess
{
    public static class NHibernateConfiguration
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var autoMappedModels = AutoMap.AssemblyOf<Tag>(TypeIsMarkedForAutoMapping)
                .UseOverridesFromAssemblyOf<PostOverride>();

            var autoMappedUser = AutoMap.AssemblyOf<User>(type => type == typeof (User));

            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(Configuration.ConnectionString()))
                .Mappings(m => m.AutoMappings.Add(autoMappedModels).Add(autoMappedUser))
                .BuildSessionFactory();
        }

        private static bool TypeIsMarkedForAutoMapping(Type type)
        {
            return type.GetInterfaces().Any(inteface => inteface == typeof (IAutoMapped));
        }
    }
}