using Jamaica.Pipeline.Contributors;
using OpenRasta.Configuration;
using OpenRasta.Pipeline.Contributors;

namespace Jamaica
{
    public class DependencyRegistrar : DefaultDependencyRegistrar
    {
        protected override void RegisterContributors(OpenRasta.DI.IDependencyResolver resolver)
        {
            PipelineContributorTypes.Remove(typeof(DigestAuthorizerContributor));
            PipelineContributorTypes.Add(typeof(PersistenceHandleContributor));
           
            base.RegisterContributors(resolver);
        }
    }
}