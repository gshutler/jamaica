using System;
using OpenRasta.Web;

namespace Jamaica.Helpers
{
    public static class CreateUri
    {
        public static Uri For<T>() where T : class
        {
            return typeof(T).CreateUri();
        }

        public static Uri For<T>(object additionalProperties)
        {
            return typeof(T).CreateUri(additionalProperties);
        }

        public static Uri For<T>(string uriName)
        {
            return typeof(T).CreateUri(uriName);
        }
    }
}