using System;
using Jamaica.TableFootball.Core.Authentication.Login;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.Login
{
    public class NameNotRecognised : Specification
    {
        OperationResult result;
        LoginResource loginResource;

        protected override void Given()
        {
            loginResource = new LoginResource
                                {
                                    Name = "UnknownUser",
                                    Password = "Password"
                                };
        }

        protected override void When()
        {
            result = Subject<LoginHandler>().Post(loginResource);
        }

        [Then]
        public void BadRequest()
        {
            Verify(result, Is.InstanceOf<OperationResult.BadRequest>());
        }

        [Then]
        public void LoginResourcePassedBack()
        {
            Verify(result.ResponseResource, Is.SameAs(loginResource));
        }

        [Then]
        public void LoginFailed()
        {
            Verify(loginResource.LoginFailed, Is.True);
        }

        [Then]
        public void UnknownUserErrorMessage()
        {
            Verify(loginResource.ErrorMessage, Is.EqualTo("Name not recognised"));
        }
    }
}