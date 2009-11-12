using Jamaica.Handlers;
using NHibernate;
using NHibernate.Criterion;
using OpenRasta.Configuration;
using OpenRasta.Web;
using OR.Throwaway.Domain;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    public class TaggedPostsHandler : Handler
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<TaggedPostsResource>()
                .AtUri("/posts/tagged/{name}")
                .HandledBy<TaggedPostsHandler>()
                .RenderedByAspx("~/Views/TaggedPosts.aspx");
        }

        private readonly ISession session;

        public TaggedPostsHandler(ISession session)
        {
            this.session = session;
        }

        public OperationResult Get(string name)
        {
            var tag = session.CreateCriteria<Tag>()
                .Add(Restrictions.Eq("Name", name))
                .UniqueResult<Tag>();

            if (tag == null) return NotFound();

            var taggedPosts = session.CreateQuery(@"
                FROM  Post p
                WHERE EXISTS (FROM p.Tags t WHERE t = :tag)")
                .SetEntity("tag", tag)
                .List<Post>();

            return OK(new TaggedPostsResource
                          {
                              Tag = tag,
                              TaggedPosts = taggedPosts
                          });
        }
    }
}