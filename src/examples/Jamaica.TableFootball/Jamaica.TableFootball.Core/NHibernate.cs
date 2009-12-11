using System;
using System.Configuration;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jamaica.NHibernate.Mapping;
using Jamaica.TableFootball.Core.Mappings;
using NHibernate;

namespace Jamaica.TableFootball.Core
{
    public static class NHibernate
    {
        public static string ConnectionString()
        {
            var connectionSetting = ConfigurationManager.ConnectionStrings["TableFootball"];

            if (connectionSetting == null)
            {
                return string.Format("Data Source={0};Version=3;",
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                  "tablefootball.db"));
            }

            return connectionSetting.ConnectionString;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(ConnectionString()))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                    .AddFromAssemblyOf<ParticipantMap>())
                .BuildSessionFactory();
        }
    }
}