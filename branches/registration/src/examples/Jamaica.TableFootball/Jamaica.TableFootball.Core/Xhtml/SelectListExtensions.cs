using System;
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
            return xhtml.Select(propertyName, selectList.Items.ToDictionary(item => item.Value, item => item.Display));
        }
    }
}