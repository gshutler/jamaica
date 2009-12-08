using System;

namespace Jamaica.TableFootball.Core
{
    public class UriRegistrationAttribute : Attribute
    {
        public UriRegistrationAttribute(string uri, params string[] alternateUris)
        {
            Uri = uri;
            AlternateUris = alternateUris;
        }

        public string Uri { get; private set; }
        public string[] AlternateUris { get; private set; }
    }
}