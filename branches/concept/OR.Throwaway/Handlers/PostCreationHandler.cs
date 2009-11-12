using Jamaica.Security;
using NHibernate;
using OpenRasta.Configuration;
using OpenRasta.Security;
using OpenRasta.Web;
using OR.Throwaway.Domain;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    [RequiresAuthentication]
    public class PostCreationHandler : PostHandlerBase
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<PostCreationResource>()
                .AtUri("/post/create")
                .HandledBy<PostCreationHandler>()
                .RenderedByAspx("~/Views/CreatePost.aspx");
        }

        private readonly User user;

        public PostCreationHandler(ISession session, User user) : base(session)
        {
            this.user = user;
        }

        public OperationResult Get()
        {
            return OK(new PostCreationResource());
        }

        public OperationResult Post(PostCreationResource postCreationResource)
        {
            postCreationResource.Author = user;
            return BindTagsValidateAndRespondAppropriately(postCreationResource, postCreationResource.TagList);
        }
    }
}