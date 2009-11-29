using OpenRasta.Web.Markup.Modules;
using OpenRasta.Web.Markup;

namespace Jamaica.TableFootball
{
    public static class XhtmlExtensions
    {
        public static IInputTextElement ClassIf(this IInputTextElement textElement, bool predicate, string cssClass)
        {
            if (predicate)
            {
                return textElement.Class(cssClass);
            }
            return textElement;
        }
    }
}