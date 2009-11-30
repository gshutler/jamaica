using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Home;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Home
{
    public class ViewingTheHomePageAsAnAuthenticatedUser : Specification
    {
        User user;
        OperationResult result;

        protected override void Given()
        {
            user = new User("NathanTyson");
            user.SetPassword("forest");

            InjectDependency<ISecurityPrincipal>(user);
        }

        protected override void When()
        {
            result = Subject<HomeHandler>().Get();
        }

        [Then]
        public void ResponseIsOK()
        {
            Verify(result, Is.InstanceOf<OperationResult.OK>());
        }

        [Then]
        public void ReportsAsAuthenticated()
        {
            Verify(result.Response<HomeResource>().Authenticated, Is.True);
        }

        [Then]
        public void SecurityPrincipalIsAuthenticatedUser()
        {
            Verify(result.Response<HomeResource>().SecurityPrincipal, Is.SameAs(user));
        }
    }
}