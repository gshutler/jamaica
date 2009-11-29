using System;
using Jamaica.TableFootball.Core.Authentication.Login;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.Login
{
    public class RequestingLoginPage : Specification
    {
        OperationResult result;

        protected override void Given()
        {
        }

        protected override void When()
        {
            result = Subject<LoginHandler>().Get();
        }

        [Then]
        public void ResponseOK()
        {
            Verify(result, Is.InstanceOf<OperationResult.OK>());
        }

        [Then]
        public void ResourceEmptyLoginResource()
        {
            Verify(result.ResponseResource.As<LoginResource>().Name.IsNullOrEmpty(), Is.True);
            Verify(result.ResponseResource.As<LoginResource>().Password.IsNullOrEmpty(), Is.True);
        }
    }
}