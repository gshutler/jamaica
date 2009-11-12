using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using OR.Throwaway.Domain;

namespace OR.Throwaway.DataAccess.Mapping
{
    public class PostOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.References(x => x.Author)
                .Not.LazyLoad();

            mapping.HasManyToMany(post => post.Tags)
                .Cascade.SaveUpdate().Not.LazyLoad();
        }
    }
}