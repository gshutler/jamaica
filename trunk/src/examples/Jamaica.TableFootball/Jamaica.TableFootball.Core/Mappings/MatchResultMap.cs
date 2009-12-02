using FluentNHibernate.Mapping;

namespace Jamaica.TableFootball.Core.Mappings
{
    public class MatchResultMap : ClassMap<MatchResult>
    {
        public MatchResultMap()
        {
            Id(x => x.Id);
            Map(x => x.Date);
            References(x => x.Victor).Cascade.All();
            References(x => x.Opponent).Cascade.All();
        }
    }
}