using System;
using System.Collections.Generic;
using OR.Throwaway.Domain;

namespace OR.Throwaway.Resources
{
    public class PostListResource
    {
        public IList<Post> Posts { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }

        public IEnumerable<PageLink> PageLinks()
        {
            for (var page = 1; page <= Pages; page++)
            {
                yield return new PageLink(page, Page);
            }
        }

        public class PageLink
        {
            public int Page { get; private set; }
            public bool Current { get; private set; }

            public PageLink(int page, int currentPage)
            {
                Page = page;
                Current = page == currentPage;
            }
        }
    }
}