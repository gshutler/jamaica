using System;
using OpenRasta.DI;
using OpenRasta.Diagnostics;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using OpenRasta.Pipeline.Diagnostics;

namespace Jamaica.Specifications.Pipeline
{
    public static class PipelineGenerator
    {
        public static IPipeline Create(params Type[] contributorTypes)
        {
            var resolver = new InternalDependencyResolver();
            resolver.AddDependency<IPipelineContributor, BootstrapperContributor>();

            foreach (var type in contributorTypes)
                resolver.AddDependency(typeof(IPipelineContributor), type, DependencyLifetime.Singleton);

            var runner = new PipelineRunner(resolver) { PipelineLog = new TraceSourceLogger<PipelineLogSource>() };

            return runner;
        }
    }
}