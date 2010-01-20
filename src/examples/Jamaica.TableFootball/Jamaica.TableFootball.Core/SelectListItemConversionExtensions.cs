using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Xhtml;

namespace Jamaica.TableFootball.Core
{
    public static class SelectListItemConversionExtensions
    {
        public static IEnumerable<SelectListItem> AsSelectListItems(this IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                yield return new SelectListItem(user.Name);
            }
        }
    }
}