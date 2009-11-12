using System;
using NHibernate;
using OpenRasta.Configuration;
using OpenRasta.Data;
using OpenRasta.Security;
using OpenRasta.Web;
using OR.Throwaway.Domain;

namespace OR.Throwaway.Handlers
{
    public class PostHandler : PostHandlerBase
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<Post>()
                .AtUri("/post/{id}")
                .And.AtUri("/post/{id}/edit").Named("edit")
                .HandledBy<PostHandler>()
                .RenderedByAspx(new {
                                        index = "~/Views/Post.aspx",
                                        edit = "~/Views/EditPost.aspx"
                                    });
        }

        public PostHandler(ISession session) : base(session)
        {
        }

        public OperationResult Get(int id)
        {
            var post = session.Get<Post>(id);

            if (post == null) return NotFound();

            return OK(post);
        }


        [RequiresAuthentication]
        public OperationResult GetEdit(int id)
        {
            var post = session.Get<Post>(id);

            if (post == null) return NotFound();

            return OK(post);
        }

        [RequiresAuthentication]
        public OperationResult Post(int id, string tagList, ChangeSet<Post> changes)
        {
            var post = session.Get<Post>(id);

            if (post == null) return NotFound();

            changes.Apply(post);
            post.Tags.Clear();

            return BindTagsValidateAndRespondAppropriately(post, tagList);
        }
    }
}