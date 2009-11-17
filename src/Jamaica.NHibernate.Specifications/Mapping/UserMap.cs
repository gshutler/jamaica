using FluentNHibernate.Mapping;
using Jamaica.Security;

namespace Jamaica.NHibernate.Specifications.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(user => user.Id);
            Map(user => user.Username);
            Map(user => user.Hash);
            HasMany(user => user.Roles);
        }
    }
}