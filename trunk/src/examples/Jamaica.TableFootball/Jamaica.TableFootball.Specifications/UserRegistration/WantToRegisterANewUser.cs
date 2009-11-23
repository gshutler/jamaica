using System;
using Jamaica.TableFootball.Core.UserRegistration;
using NUnit.Framework;
using OpenRasta.Web;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.UserRegistration
{
    public class WantToRegisterANewUser : Specification
    {
        OperationResult result;

        protected override void Given()
        {
        }

        protected override void When()
        {
            result = Subject<UserRegistrationHandler>().Get();
        }

        [Then]
        public void ResponseIsOk()
        {
            Verify(result, Is.TypeOf<OperationResult.OK>());
        }

        [Then]
        public void GivenAnUserRegistrationResource()
        {
            Verify(result.ResponseResource, Is.TypeOf<UserRegistrationResource>());
        }

        [Then]
        public void UserRegistrationResourceHasNoUsername()
        {
            Verify(result.ResponseResource.As<UserRegistrationResource>().Username.IsNullOrEmpty(), Is.True);
        }

        [Then]
        public void UserRegistrationResourceHasNoPassword()
        {
            Verify(result.ResponseResource.As<UserRegistrationResource>().Password.IsNullOrEmpty(), Is.True);
        }

        [Then]
        public void UserRegistrationResourceHasNoPasswordConfirmation()
        {
            Verify(result.ResponseResource.As<UserRegistrationResource>().PasswordConfirmation.IsNullOrEmpty(), Is.True);
        }
    }
}