using System;
using OpenRasta.Pipeline;

namespace Jamaica.Pipeline.Contributors
{
    public static class KnownContributors
    {
        public interface IPersistenceInitialized : IPipelineContributor
        {
        }
    }
}