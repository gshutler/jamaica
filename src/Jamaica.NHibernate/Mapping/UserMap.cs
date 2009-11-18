using FluentNHibernate.Mapping;
using Jamaica.Security;

namespace Jamaica.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(user => user.Id);
            Map(user => user.Username);
            Map(user => user.Salt);
            Map(user => user.Hash);
            HasManyToMany(user => user.Roles).Cascade.SaveUpdate();
        }
    }
}