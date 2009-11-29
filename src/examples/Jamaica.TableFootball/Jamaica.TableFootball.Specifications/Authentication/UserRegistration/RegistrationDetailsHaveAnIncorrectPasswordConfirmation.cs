using System;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.UserRegistration
{
    public class RegistrationDetailsHaveAnIncorrectPasswordConfirmation : Specification
    {
        UserRegistrationResource userRegistrationResource;
        OperationResult result;

        protected override void Given()
        {
            userRegistrationResource = new UserRegistrationResource
                                           {
                                               Name = "NathanTyson",
                                               Password = "Forest",
                                               PasswordConfirmation = "Derby"
                                           };
        }

        protected override void When()
        {
            result = Subject<UserRegistrationHandler>().Post(userRegistrationResource);
        }


        [Then]
        public void BadRequest()
        {
            Verify(result, Is.TypeOf<OperationResult.BadRequest>());
        }

        [Then]
        public void UserRegistrationResourceIsSentBack()
        {
            Verify(result.ResponseResource, Is.SameAs(userRegistrationResource));
        }

        [Then]
        public void PasswordErrorAttached()
        {
            Verify(userRegistrationResource.PasswordError, Is.True);
        }

        [Then]
        public void PasswordErrorMessageAttached()
        {
            Verify(userRegistrationResource.PasswordErrorMessage, Is.EqualTo("Confirmation did not match"));
        }
    }
}