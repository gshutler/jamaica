using System;

namespace Jamaica.TableFootball.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class UriAttribute : Attribute
    {
        public UriAttribute(string uri)
        {
            Uri = uri;
        }

        public string Uri { get; set; }
    }
}