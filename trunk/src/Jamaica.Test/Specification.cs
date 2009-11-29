using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenRasta.DI;
using OpenRasta.Hosting;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Rhino.Mocks;

namespace Jamaica.Test
{
    [TestFixture]
    public abstract class Specification
    {
        readonly FakeManager fakeManager = new FakeManager();
        ContextScope contextScope;

        [TestFixtureSetUp]
        public void Setup()
        {
            SetupDependencyResolver();
            SetupUriResolution();
            CommonContext();
            Given();
            When();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            contextScope.Dispose();
            TidyUp();
        }

        void SetupDependencyResolver()
        {
            InjectDependency<IDependencyResolver>(new InternalDependencyResolver());
            DependencyManager.SetResolver(Resolver);

            Resolver.AddDependency<IContextStore, AmbientContextStore>();

            contextScope = new ContextScope(new AmbientContext());
        }

        void SetupUriResolution()
        {
            InjectDependency<IUriResolver>(new TemplatedUriResolver());

            Dependency<ICommunicationContext>()
                .Stub(x => x.ApplicationBaseUri)
                .Return(new Uri("http://localhost/", UriKind.Absolute));

            AddDependencyToResolver<ICommunicationContext>();
            AddDependencyToResolver<IUriResolver>();
        }

        protected virtual void CommonContext() {}
        protected abstract void Given();
        protected abstract void When();
        protected virtual void TidyUp() {}

        protected T Dependency<T>()
        {
            return fakeManager.Dependency<T>();
        }

        protected void InjectDependency<T>(T dependency)
        {
            fakeManager.InjectDependency(dependency);
        }

        protected void AddDependencyToResolver<T>()
        {
            Resolver.AddDependencyInstance<T>(Dependency<T>());
        }

        protected T Subject<T>()
        {
            return fakeManager.ConstructedObject<T>();
        }

        protected void Verify<T>(T actual, IResolveConstraint assertion)
        {
            Assert.That(actual, assertion);
        }

        protected InternalDependencyResolver Resolver
        {
            get { return Dependency<IDependencyResolver>() as InternalDependencyResolver; }
        }

        protected IUriResolver UriResolver
        {
            get { return Dependency<IUriResolver>(); }
        }
    }
}