using FluentNHibernate.Mapping;

namespace Jamaica.TableFootball.Core.Mappings
{
    public class ParticipantMap : ClassMap<Participant>
    {
        public ParticipantMap()
        {
            Id(x => x.Id);
            Map(x => x.Score);
            References(x => x.User);
        }
    }
}