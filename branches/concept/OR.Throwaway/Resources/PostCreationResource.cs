using Jamaica.Security;
using OR.Throwaway.Domain;

namespace OR.Throwaway.Resources
{
    public class PostCreationResource
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TagList { get; set; }
        public User Author { get; set; }

        public static implicit operator Post(PostCreationResource postCreationResource)
        {
            return new Post
                       {
                           Title = postCreationResource.Title,
                           Description = postCreationResource.Description,
                           Author = postCreationResource.Author
                       };
        }
    }
}