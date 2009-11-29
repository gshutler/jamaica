using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Authentication.Login;
using NHibernate;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.Login
{
    public class IncorrectPassword : Specification
    {
        LoginResource loginResource;
        OperationResult result;
        User nathanTyson;

        protected override void Given()
        {
            loginResource = new LoginResource
                                {
                                    Name = "NathanTyson",
                                    Password = "Derby"
                                };

            nathanTyson = new User("NathanTyson");
            nathanTyson.SetPassword("Forest");

            Dependency<ISession>()
                .Stub(x => x.Get<User>("NathanTyson"))
                .Return(nathanTyson);
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
        public void IncorrectPasswordErrorMessage()
        {
            Verify(loginResource.ErrorMessage, Is.EqualTo("Incorrect password"));
        }
    }
}