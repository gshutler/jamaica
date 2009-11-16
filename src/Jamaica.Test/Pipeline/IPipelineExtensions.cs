using System.IO;
using OpenRasta.Pipeline;

namespace Jamaica.Test.Pipeline
{
    public static class IPipelineExtensions
    {
        public static int IndexOf<T>(this IPipeline pipeline) where T : IPipelineContributor
        {
            var index = 0;

            foreach (var call in pipeline.CallGraph)
            {
                if (typeof(T).IsAssignableFrom(call.Target.GetType()))
                    return index;
                index++;
            }

            throw new InvalidDataException();
        }
    }
}