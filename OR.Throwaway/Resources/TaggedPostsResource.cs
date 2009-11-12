using System.Collections.Generic;
using OR.Throwaway.Domain;

namespace OR.Throwaway.Resources
{
    public class TaggedPostsResource
    {
        public Tag Tag { get; set; }
        public IList<Post> TaggedPosts { get; set; }
    }
}