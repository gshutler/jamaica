using FluentNHibernate.Mapping;
using Jamaica.Security;

namespace Jamaica.NHibernate.Mapping
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(role => role.Id);
            Map(role => role.Name);
        }
    }
}