using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OpenRasta.Web.Markup;
using OpenRasta.Web.Markup.Modules;

namespace Jamaica.TableFootball.Core.Xhtml
{
    public static class SelectListExtensions
    {
        public static ISelectElement Select(this IXhtmlAnchor xhtml, Expression<Func<object>> propertyName, SelectList selectList)
        {
            return xhtml.Select(propertyName, selectList.Items);
        }

        public static ISelectElement Select(this IXhtmlAnchor xhtml, Expression<Func<object>> propertyName, SelectList selectList, string defaultDisplay)
        {
            return xhtml.Select(propertyName, selectList.ItemsWithDefault(defaultDisplay));
        }

        private static ISelectElement Select(this IXhtmlAnchor xhtml, Expression<Func<object>> propertyName, IEnumerable<SelectListItem> selectListItems)
        {
            return xhtml.Select(propertyName, selectListItems.ToDictionary(item => item.Value, item => item.Display));
        }

        private static IEnumerable<SelectListItem> ItemsWithDefault(this SelectList selectList, string defaultDisplay)
        {
            yield return new SelectListItem(defaultDisplay, "");

            foreach (var selectListItem in selectList.Items)
            {
                yield return selectListItem;
            }
        }
    }
}