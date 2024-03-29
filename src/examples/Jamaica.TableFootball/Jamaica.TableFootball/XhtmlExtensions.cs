using System;
using System.Web;
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

        public static string ToJavascriptDateString(this DateTime date)
        {
            return date.ToString("ddd MMMM dd, yyyy");
        }

        public static string HtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public static string TwoDecimalPlaces(this decimal value)
        {
            return value.ToString("N2");
        }
    }
}