using System.Linq;
using OR.Throwaway.Domain;

namespace OR.Throwaway.Extensions
{
    public static class DisplayExtensions
    {
        public static string TagList(this Post post)
        {
            return string.Join(" ", post.Tags.Select(tag => tag.Name).ToArray());
        }
    }
}