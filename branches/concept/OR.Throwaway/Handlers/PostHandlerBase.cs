using Jamaica.Handlers;
using Jamaica.Validation;
using NHibernate;
using NHibernate.Criterion;
using OpenRasta.Web;
using OR.Throwaway.Domain;
using OR.Throwaway.Extensions;

namespace OR.Throwaway.Handlers
{
    public abstract class PostHandlerBase : Handler
    {
        protected readonly ISession session;

        protected PostHandlerBase(ISession session)
        {
            this.session = session;
        }

        protected OperationResult BindTagsValidateAndRespondAppropriately(Post post, string tagList)
        {
            BindTags(post, tagList);

            var validationErrors = post.ValidationErrors();

            if (validationErrors.Count > 0)
            {
                return BadRequest(post, validationErrors);
            }

            if (!session.Contains(post)) session.Save(post);

            session.Transaction.Commit();

            return SeeOther(post.CreateUri());
        }

        private void BindTags(Post post, string tagList)
        {
            foreach(var tagString in tagList.AsTags())
            {
                var tag = session.CreateCriteria<Tag>()
                    .Add(Restrictions.Eq("Name", tagString))
                    .UniqueResult<Tag>();

                post.Tags.Add(tag ?? new Tag {Name = tagString});
            }
        }
    }
}