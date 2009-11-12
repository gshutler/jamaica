using System;
using Jamaica.Handlers;
using NHibernate;
using NHibernate.Criterion;
using OpenRasta.Configuration;
using OpenRasta.Web;
using OR.Throwaway.Domain;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    public class PostListHandler : Handler
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<PostListResource>()
                .AtUri("/posts/latest")
                .And.AtUri("/posts/page/{page}")
                .HandledBy<PostListHandler>()
                .RenderedByAspx("~/Views/Posts.aspx");
        }

        private readonly ISession session;
        private const int PageSize = 5;

        public PostListHandler(ISession session)
        {
            this.session = session;
        }

        public OperationResult Get()
        {
            return Get(1);
        }

        public OperationResult Get(int page)
        {
            var totalPosts = session.CreateCriteria<Post>()
                .SetProjection(Projections.Count("Id"))
                .UniqueResult<int>();

            var pages = (totalPosts + PageSize - 1) / PageSize;

            if (page != 1 && page > pages) return NotFound();

            var posts = session.CreateCriteria<Post>()
                .AddOrder(Order.Desc("Id"))
                .SetFirstResult((page - 1) * PageSize)
                .SetMaxResults(PageSize)
                .List<Post>();

            return OK(new PostListResource
                          {
                              Posts = posts,
                              Page = page,
                              Pages = Math.Max(pages, 1)
                          });
        }
    }
}